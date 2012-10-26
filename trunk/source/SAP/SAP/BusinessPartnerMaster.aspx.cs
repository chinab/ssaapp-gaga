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
        public static DataTable dt;
        private GeneralFunctions GF;
        private string DocType = "2";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearScreen();
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
        #region "Functions"
        public String _collectData()
        {
            try
            {
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
                DataRow dr = dt.Rows[0];

                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                dr["CardCode"] = txtCardCode.Text;
                dr["CardName"] = txtFName.Text;
                dr["CardFName"] = txtCardCode.Text;
                dr["LicTradNum"] = txtLicTradNum.Text;
                dr["Phone1"] = txtTel1.Text;
                dr["Phone2"] = txtTel2.Text;
                dr["Password"] = txtPassword.Text;

                DocumentXML objInfo = new DocumentXML();
                return objInfo.ToXMLStringFromDS(DocType, dt, null, "");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ClearScreen()
        {
            dt = new DataTable();
            dt.Columns.Add("CardCode");
            dt.Columns.Add("CardName");
            dt.Columns.Add("CardFName");
            dt.Columns.Add("LicTradNum");
            dt.Columns.Add("Phone1");
            dt.Columns.Add("Phone2");
            dt.Columns.Add("Password");
            dt.Columns.Add("Notes");
            dt.Columns.Add("U_UserID");
            dt.Rows.Add("", "", "", "", "", "", "", "Generated from SBO WEB", User.Identity.Name);

            MasterData masterDataWS = new MasterData();
            DataSet dsMaster;
            dsMaster = masterDataWS.GetShippingType(User.Identity.Name);
            ddlShipping.DataSource = dsMaster.Tables[0];
            ddlShipping.DataTextField = "TrnspName";
            ddlShipping.DataValueField = "TrnspCode";
            ddlShipping.DataBind();

            dsMaster = masterDataWS.GetBPGroup(ddlCardType.SelectedValue, User.Identity.Name);
            ddlBPGroup.DataSource = dsMaster.Tables[0];
            ddlBPGroup.DataTextField = "GroupName";
            ddlBPGroup.DataValueField = "GroupCode";
            ddlBPGroup.DataBind();

            dsMaster = masterDataWS.GetBPCurrency("", User.Identity.Name);
            ddlCurrency.DataSource = dsMaster.Tables[0];
            ddlCurrency.DataTextField = "CurrName";
            ddlCurrency.DataValueField = "CurrCode";
            ddlCurrency.DataBind();
        }
        #endregion
        #region "Navigator"
        protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
        {
            
        }
        #endregion

    }
}