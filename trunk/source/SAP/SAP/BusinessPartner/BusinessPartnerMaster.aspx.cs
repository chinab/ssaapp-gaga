using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;
using System.Collections;
using System.Globalization;
using System.Net;

namespace SAP
{
    public partial class BusinessPartnerMaster : System.Web.UI.Page
    {
        public static DataTable dtHeader;
        public static DataTable dtContactPerson;
        public static DataTable dtAddress;
        private GeneralFunctions GF;
        private string DocType = "2";

        #region "Event"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearScreen();
                lvContactPerson.DataSource = dtContactPerson;
                lvContactPerson.DataBind();

                string cardcode = Request.QueryString["cardcode"];
                if (!String.IsNullOrEmpty(cardcode))
                {
                    LoadData(cardcode);
                }
                else
                {
                    //LoadDefault();
                    //dtHeader.Rows.Add("","", "", "", "", "", "", "20121022", "830", "20121022", "930", "N", User.Identity.Name);
                }
            }
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCardCode.Text == "")
            {
                Session["errorMessage"] = "Missing Card Code";
                Session["requestXML"] = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('Missing Card Code','');", true);
            }

            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType, "", false);
            if ((int)ds.Tables[0].Rows[0]["ErrCode"] != 0)
            {
                Session["errorMessage"] = ds.Tables[0].Rows[0]["ErrMsg"];
                Session["requestXML"] = requestXML;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + WebUtility.HtmlEncode(ds.Tables[0].Rows[0]["ErrMsg"].ToString()) + "','');", true);
            }
            else
            {
                Session["successMessage"] = "Operation complete sucessful!";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                   "Main.setMasterMessage('" + "Operation complete sucessful!" + "','');", true);

                ClearScreen();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                              "Dialog.hideLoader();", true);
        }
        protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterData masterDataWS = new MasterData();
            DataSet dsMaster;
            dsMaster = masterDataWS.GetBPGroup(ddlCardType.SelectedValue, User.Identity.Name);
            ddlBPGroup.DataSource = dsMaster.Tables[0];
            ddlBPGroup.DataTextField = "GroupName";
            ddlBPGroup.DataValueField = "GroupCode";
            ddlBPGroup.DataBind();
        }
       
        #endregion

        #region "Functions"
        private void LoadData(string Cardcode)
        {
            if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
            Transaction trx = new Transaction();
            dtHeader = trx.GetMarketingDocument_ReturnDS(DocType, Cardcode, User.Identity.Name).Tables[0];
            if (dtHeader.Rows.Count == 0)
                Cardcode = "";
            dtHeader = trx.GetMarketingDocument_ReturnDS(DocType, Cardcode, User.Identity.Name).Tables[0];

            DataRow dr = dtHeader.Rows[0];
            txtCardCode.Text=dr["CardCode"].ToString();
            txtCardName.Text = dr["CardName"].ToString();
            txtFName.Text=dr["CardFName"].ToString();
            txtLicTradNum.Text=dr["LicTradNum"].ToString() ;
            txtTel1.Text=dr["Phone1"].ToString() ;
            txtTel2.Text=dr["Phone2"].ToString() ;
            txtPassword.Text = dr["Password"].ToString();

            ddlCardType.SelectedValue = dr["CardType"].ToString();
            ddlCurrency.SelectedValue = dr["currency"].ToString();
            ddlBPGroup.SelectedValue = dr["GroupCode"].ToString();
            txtAcctBalance.Text = GF.FormatNumeric(dr["Balance"].ToString(), "SumDec");
            txtOrder.Text = GF.FormatNumeric(dr["OrdersBal"].ToString(), "SumDec");
            txtDelivery.Text = GF.FormatNumeric(dr["DNotesBal"].ToString(), "SumDec"); 
            txtOpportunity.Text = dr["OprCount"].ToString();

            txtWebsite.Text = dr["IntrntSite"].ToString();
            txtEmail.Text = dr["E_mail"].ToString();
            //----------load contact person---------------
            dtContactPerson = trx.GetMarketingDocument_ReturnDS("11", Cardcode, User.Identity.Name).Tables[0];
            lvContactPerson.DataSource = dtContactPerson;
            lvContactPerson.DataBind();

            SetNavigatorURL(txtCardCode.Text);
            SetScreenStatus("Update");
        }
        public String _collectData()
        {
            try
            {
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
                DataRow dr = dtHeader.Rows[0];

                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                dr["CardCode"] = txtCardCode.Text;
                dr["CardName"] = txtCardName.Text;
                dr["CardFName"] = txtFName.Text;
                dr["LicTradNum"] = txtLicTradNum.Text;
                dr["Phone1"] = txtTel1.Text;
                dr["Phone2"] = txtTel2.Text;
                dr["Password"] = txtPassword.Text;

                DocumentXML objInfo = new DocumentXML();
                return objInfo.ToXMLStringFromDS(DocType, dtHeader, null, "");
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void SetScreenStatus(string Status)
        {
            switch (Status)
            {
                case "New":
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "Update":
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    txtCardCode.Enabled = false;
                    break;
                case "Close":
                    btnAdd.Visible = false;
                    btnUpdate.Visible = false;
                    break;
            }
        }
        private void ClearScreen()
        {
            dtHeader = new DataTable();
            dtHeader.Columns.Add("CardCode");
            dtHeader.Columns.Add("CardName");
            dtHeader.Columns.Add("CardFName");
            dtHeader.Columns.Add("LicTradNum");
            dtHeader.Columns.Add("Phone1");
            dtHeader.Columns.Add("Phone2");
            dtHeader.Columns.Add("Password");
            dtHeader.Columns.Add("Notes");
            dtHeader.Columns.Add("U_UserID");
            dtHeader.Rows.Add("", "", "", "", "", "", "", "Generated from SBO WEB", User.Identity.Name);

            dtContactPerson = new DataTable();
            dtContactPerson.Columns.Add("No");
            dtContactPerson.Columns.Add("Name");
            dtContactPerson.Columns.Add("FirstName");
            dtContactPerson.Columns.Add("MiddleName");
            dtContactPerson.Columns.Add("LastName");
            dtContactPerson.Columns.Add("Title");
            dtContactPerson.Columns.Add("Position");
            dtContactPerson.Columns.Add("Address");
            dtContactPerson.Columns.Add("Tel1");
            dtContactPerson.Columns.Add("Tel2");
            dtContactPerson.Columns.Add("Cellolar");
            dtContactPerson.Columns.Add("E_MailL");
            dtContactPerson.Rows.Add(0,"", "", "", "", "", "", "", "", "", "", "");

            MasterData masterDataWS = new MasterData();
            DataSet dsMaster;
            dsMaster = masterDataWS.GetShippingType(User.Identity.Name);
            if (dsMaster!=null)
            {
                ddlShipping.DataSource = dsMaster.Tables[0];
                ddlShipping.DataTextField = "TrnspName";
                ddlShipping.DataValueField = "TrnspCode";
                ddlShipping.DataBind();
            }

            dsMaster = masterDataWS.GetBPGroup(ddlCardType.SelectedValue, User.Identity.Name);
            if (dsMaster.Tables.Count > 0)
            {
                ddlBPGroup.DataSource = dsMaster.Tables[0];
                ddlBPGroup.DataTextField = "GroupName";
                ddlBPGroup.DataValueField = "GroupCode";
                ddlBPGroup.DataBind();
            }
            dsMaster = masterDataWS.GetBPCurrency("", User.Identity.Name);
            if (dsMaster.Tables.Count > 0)
            {
                dsMaster.Tables[0].Rows.Add("##", "All Currency");
                ddlCurrency.DataSource = dsMaster.Tables[0];
                ddlCurrency.DataTextField = "CurrName";
                ddlCurrency.DataValueField = "CurrCode";
                ddlCurrency.DataBind();
            }
            SetNavigatorURL("");
            SetScreenStatus("New");
        }
        protected void SetNavigatorURL(string CurrentKey)
        {
            GetDefault df = new GetDefault();
            DataSet nav = df.GetNextPreviousID(DocType, User.Identity.Name, "OCRD", "cardcode", CurrentKey);
            if (nav != null)
            {
                this.linkFirst.NavigateUrl = "/BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + nav.Tables[0].Rows[0]["Fir"].ToString();
                this.linkNext.NavigateUrl = "/BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + nav.Tables[0].Rows[0]["Nex"].ToString();
                this.linkPrevious.NavigateUrl = "/BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + nav.Tables[0].Rows[0]["Pre"].ToString();
                this.linkLast.NavigateUrl = "/BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + nav.Tables[0].Rows[0]["Las"].ToString();
            }
            this.linkNew.NavigateUrl = "/BusinessPartner/BusinessPartnerMaster.aspx?cardcode=";
        }
        #endregion

        #region "Contact"
        protected void lvContactPerson_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void lvContactPerson_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void lvContactPerson_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {

        }

        protected void lvContactPerson_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void btnAddContact_Click(object sender, EventArgs e)
        {

        }

        protected void ContactPager_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Address"
        protected void btnAddAddress_Click(object sender, EventArgs e)
        {

        }

        protected void lvAddress_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void lvAddress_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void lvAddress_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {

        }

        protected void lvAddress_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void AddressPager_PreRender(object sender, EventArgs e)
        {

        }

        #endregion



    }
}