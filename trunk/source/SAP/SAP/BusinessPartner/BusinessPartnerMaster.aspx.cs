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
        private GeneralFunctions GF = new GeneralFunctions(HttpContext.Current.User.Identity.Name);
        public static DataTable dtHeader;
        public static DataTable dtContactPerson;
        public static DataTable dtAddress;
        private string TblHeaderName = "OCRD";
        private string TblAddress = "CRD1";
        private string TblContact = "OCPR";
        private string DocType = "2";

        #region "Event"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearScreen();
                lvContactPerson.DataSource = dtContactPerson;
                lvContactPerson.DataBind();
                lvAddress.DataSource = dtAddress;
                lvAddress.DataBind();

                MasterData masterDataWS = new MasterData();
                DataSet dsMaster;
                dsMaster = masterDataWS.GetShippingType(User.Identity.Name);
                if (dsMaster != null)
                {
                    dsMaster.Tables[0].Rows.Add("-1", "");
                    ddlShipping.DataSource = dsMaster.Tables[0];
                    ddlShipping.DataTextField = "TrnspName";
                    ddlShipping.DataValueField = "TrnspCode";
                    ddlShipping.DataBind();
                    ddlShipping.SelectedValue = "-1";
                }

                dsMaster = masterDataWS.GetBPGroup(ddlCardType.SelectedValue, User.Identity.Name);
                if (dsMaster.Tables.Count > 0)
                {
                    ddlBPGroup.DataSource = dsMaster.Tables[0];
                    ddlBPGroup.DataTextField = "GroupName";
                    ddlBPGroup.DataValueField = "GroupCode";
                    ddlBPGroup.DataBind();
                }

                dsMaster = masterDataWS.GetIndustry(User.Identity.Name);
                if (dsMaster.Tables.Count > 0)
                {
                    ddlIndustry.DataSource = dsMaster.Tables[0];
                    ddlIndustry.DataTextField = "IndName";
                    ddlIndustry.DataValueField = "IndCode";
                    ddlIndustry.DataBind();
                }

                dsMaster = masterDataWS.GetBPCurrency("", User.Identity.Name);
                if (dsMaster.Tables.Count > 0)
                {
                    dsMaster.Tables[0].Rows.Add("##", "All Currency");
                    ddlCurrency.DataSource = dsMaster.Tables[0];
                    ddlCurrency.DataTextField = "CurrName";
                    ddlCurrency.DataValueField = "CurrCode";
                    ddlCurrency.DataBind();
                    ddlCurrency.Items.FindByValue("##").Selected = true;
                }

                if (ddlCardType.SelectedValue == "S")
                    dsMaster = masterDataWS.GetSeries(User.Identity.Name, DocType, "S");
                else
                    dsMaster = masterDataWS.GetSeries(User.Identity.Name, DocType, "C");
                if (dsMaster.Tables.Count > 0)
                {
                    ddlSeries.DataSource = dsMaster.Tables[0];
                    ddlSeries.DataTextField = "SeriesName";
                    ddlSeries.DataValueField = "Series";
                    ddlSeries.DataBind();
                }

                string cardcode = Request.QueryString["cardcode"];
                if (!String.IsNullOrEmpty(cardcode))
                {
                    LoadData(cardcode);
                }
                else
                {
                    LoadDefault();
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
                    "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ds.Tables[0].Rows[0]["ErrMsg"].ToString()) + "','');", true);
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

            if (ddlCardType.SelectedValue == "S")
                dsMaster = masterDataWS.GetSeries(User.Identity.Name, DocType, "S");
            else
                dsMaster = masterDataWS.GetSeries(User.Identity.Name, DocType, "C");
            if (dsMaster.Tables.Count > 0)
            {
                ddlSeries.DataSource = dsMaster.Tables[0];
                ddlSeries.DataTextField = "SeriesName";
                ddlSeries.DataValueField = "Series";
                ddlSeries.DataBind();
            }
        }
        protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateSeri();
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            try
            {
                base.OnLoadComplete(e);
                if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
                {
                    switch (this.Request["__EVENTARGUMENT"].ToString())
                    {
                       case "EditProjectCallBack":
                            Project prj = Session["chosenProject"] as Project;
                            if (prj != null)
                            {
                                this.txtProject.Text = prj.PrjCode;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ex.ToString()) + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }      
        #endregion

        #region "Functions"
        private void LoadData(string Cardcode)
        {
            try
            {
            Transaction trx = new Transaction();
            dtHeader = trx.GetMarketingDocument_ReturnDS(DocType, Cardcode, User.Identity.Name).Tables[0];
            if (dtHeader.Rows.Count == 0)
                Cardcode = "";
            DataSet ds = trx.GetMarketingDocument_ReturnDS(DocType, Cardcode, User.Identity.Name);
            dtHeader = ds.Tables[0];

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

            txtMobile.Text=dr["Cellular"].ToString();
            txtFax.Text=dr["Fax"].ToString();
            txtEmail.Text=dr["E_Mail"].ToString();
            txtWebsite.Text=dr["IntrntSite"].ToString();
            ddlShipping.SelectedValue = dr["ShipType"].ToString();
            ddlIndustry.SelectedValue = dr["IndustryC"].ToString();
            txtIDNo2.Text=dr["AddID"].ToString();
            txtFederalTax.Text=dr["VatIdUnCmp"].ToString();

            txtAcctBalance.Text = GF.FormatNumeric(dr["Balance"].ToString(), "SumDec");
            if (dr["CardType"].ToString() == "S")
            {
                trOrder.Visible = false;
                trDelivery.Visible = false;
                trOppor.Visible = false;
            }
            else
            {
                trOrder.Visible = true;
                trDelivery.Visible = true;
                trOppor.Visible = true;
                txtOrder.Text = GF.FormatNumeric(dr["OrdersBal"].ToString(), "SumDec");
                txtDelivery.Text = GF.FormatNumeric(dr["DNotesBal"].ToString(), "SumDec");
                txtOpportunity.Text = dr["OprCount"].ToString();            
            }
            

            txtWebsite.Text = dr["IntrntSite"].ToString();
            txtEmail.Text = dr["E_mail"].ToString();

            txtRemark.Text = dr["Free_Text"].ToString();
            txtAlias.Text = dr["AliasName"].ToString();
            txtContactPerson.Text = dr["CntctPrsn"].ToString();

            ddlSeries.SelectedValue = dr["Series"].ToString();
            //----------load contact person---------------
            dtContactPerson = trx.GetMarketingDocument_ReturnDS("11", Cardcode, User.Identity.Name).Tables[0];
            lvContactPerson.DataSource = dtContactPerson;
            lvContactPerson.DataBind();
            
            //----------load address----------------------
            dtAddress =ds.Tables[1];
            lvAddress.DataSource = dtAddress;
            lvAddress.DataBind();

            SetNavigatorURL(txtCardCode.Text);
            SetScreenStatus("Update");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                    "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ex.ToString()) + "','');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }
        public String _collectData()
        {
            try
            {
                DataRow dr = dtHeader.Rows[0];

                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                dr["CardCode"] = txtCardCode.Text;
                dr["CardName"] = txtCardName.Text;
                dr["CardFName"] = txtFName.Text;
                dr["LicTradNum"] = txtLicTradNum.Text;
                dr["Phone1"] = txtTel1.Text;
                dr["Phone2"] = txtTel2.Text;
                dr["Password"] = txtPassword.Text;
                dr["Free_Text"] = txtRemark.Text;
                dr["CardType"] = ddlCardType.SelectedItem.Value.ToString();
                dr["GroupCode"] = ddlBPGroup.SelectedItem.Value.ToString();
                dr["Currency"] = ddlCurrency.SelectedItem.Value.ToString();
                dr["Cellular"] = txtMobile.Text;
                dr["Fax"] = txtFax.Text;
                dr["E_Mail"] = txtEmail.Text;
                dr["IntrntSite"] = txtWebsite.Text;
                if(ddlShipping.SelectedItem!=null)
                    dr["ShipType"] = ddlShipping.SelectedItem.Value.ToString();
                if(ddlIndustry.SelectedItem!=null)
                    dr["IndustryC"] = ddlIndustry.SelectedItem.Value.ToString();
                dr["AddID"] = txtIDNo2.Text;
                dr["VatIdUnCmp"] = txtFederalTax.Text;

                dr["Series"] = ddlSeries.SelectedItem.Value;
                dr["U_UserID"] = User.Identity.Name;
                DocumentXML objInfo = new DocumentXML();
                DataSet ds = new DataSet("DS");
                dtHeader.TableName = TblHeaderName;
                dtAddress.TableName = TblAddress;
                dtContactPerson.TableName = TblContact;
                ds.Tables.Add(dtHeader.Copy());
                ds.Tables.Add(dtAddress.Copy());
                ds.Tables.Add(dtContactPerson.Copy());

                return objInfo.ToXMLStringFromDS(DocType, ds);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ex.ToString()) + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
                return "";
            }
        }
        protected void SetScreenStatus(string Status)
        {
            switch (Status)
            {
                case "New":
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    btnAddAddress.Visible = true;
                    btnAddContact.Visible = true;
                    btnActivity.Visible = false;
                    break;
                case "Update":
                    btnAdd.Visible = false;
                    btnUpdate.Visible = false;
                    txtCardCode.Enabled = false;
                    btnAddAddress.Visible = false;
                    btnAddContact.Visible = false;
                    btnActivity.Visible = true;
                    break;
                case "Close":
                    btnAdd.Visible = false;
                    btnUpdate.Visible = false;
                    txtCardCode.Enabled = false;
                    btnAddAddress.Visible = false;
                    btnAddContact.Visible = false;
                    btnActivity.Visible = true;
                    break;
            }
        }
        private void ClearScreen()
        {
            //------------------create header table-----------------------------
            dtHeader = new DataTable("OCRD");
            dtHeader.Columns.Add("CardType");
            dtHeader.Columns.Add("CardCode");
            dtHeader.Columns.Add("CardName");
            dtHeader.Columns.Add("CardFName");
            dtHeader.Columns.Add("LicTradNum");
            dtHeader.Columns.Add("Phone1");
            dtHeader.Columns.Add("Phone2");
            dtHeader.Columns.Add("Password");
            dtHeader.Columns.Add("Free_Text");
            dtHeader.Columns.Add("GroupCode");
            dtHeader.Columns.Add("Currency");
            dtHeader.Columns.Add("Cellular");
            dtHeader.Columns.Add("Fax");
            dtHeader.Columns.Add("E_Mail");
            dtHeader.Columns.Add("IntrntSite");
            dtHeader.Columns.Add("ShipType");
            dtHeader.Columns.Add("IndustryC");
            dtHeader.Columns.Add("AddID");
            dtHeader.Columns.Add("VatIdUnCmp");
            dtHeader.Columns.Add("Series"); 
            dtHeader.Columns.Add("U_UserID");
            dtHeader.Rows.Add();

            //---------------create contact person table-------------------------
            dtContactPerson = new DataTable("OCPR");
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

            //------------------create address table-------------------
            dtAddress = new DataTable("CRD1");
            dtAddress.Columns.Add("No");
            dtAddress.Columns.Add("Address");
            dtAddress.Columns.Add("CardCode");
            dtAddress.Columns.Add("Street");
            dtAddress.Columns.Add("AdresType");
            dtAddress.Columns.Add("StreetNo");
            dtAddress.Columns.Add("Building");

            //--------------clear text-------------------
            txtCardCode.Text = "";
            txtCardName.Text = "";
            txtFName.Text = "";
            txtLicTradNum.Text = "";
            txtTel1.Text = "";
            txtTel2.Text = "";
            txtPassword.Text = "";
            txtMobile.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtWebsite.Text = "";
            txtIDNo2.Text = "";
            txtFederalTax.Text = "";
            txtAcctBalance.Text ="";
            txtOrder.Text = "";
            txtDelivery.Text = "";
            txtOpportunity.Text = "";
            txtWebsite.Text = "";
            txtEmail.Text = "";
            txtRemark.Text = "";
            txtAlias.Text = "";
            txtContactPerson.Text = "";

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
        protected void LoadDefault()
        {
            if (ddlSeries.Items.FindByText("CRM") != null)
            {
                ddlSeries.Items.FindByText("CRM").Selected = true;
                GenerateSeri();
            }
            
        }
        protected void GenerateSeri()
        {
            MasterData masterDataWS = new MasterData();
            DataSet dsMaster;
            dsMaster = masterDataWS.GetSeries(User.Identity.Name, "2", ddlCardType.SelectedValue);
            DataView dv = new DataView(dsMaster.Tables[0]);
            dv.RowFilter = "Series=" + ddlSeries.SelectedValue;
            if (dv.Count > 0)
            {
                if (!string.IsNullOrEmpty(dv[0]["NextNumber"].ToString()))
                {
                    txtCardCode.Enabled = false;
                    string code = "";
                    code = dv[0]["BeginStr"].ToString();
                    for (int i = 0; i < (int)(dv[0]["NumSize"]) - dv[0]["NextNumber"].ToString().Length; i++)
                        code = code + "0";

                    code = code + dv[0]["NextNumber"].ToString();
                    txtCardCode.Text = code;
                }
                else
                {
                    txtCardCode.Enabled = true;
                    txtCardCode.Text = "";
                }
            }
            else
            {
                txtCardCode.Enabled = true;
                txtCardCode.Text = "";
            }
        }
        #endregion

        #region "Contact"
        private void _cancelAddNewContact()
        {
            this.lvContactPerson.InsertItemPosition = InsertItemPosition.None;
            this.btnAddContact.Enabled = true;
            this.lvContactPerson.DataBind();
        }
        private void SetControlsStatus_Contact(string asStatus)
        {
            switch (asStatus)
            {
                case "Add":
                    btnUpdate.Enabled = btnAdd.Enabled = btnAddContact.Enabled = false;
                    break;
                case "Edit":
                     btnUpdate.Enabled =btnAdd.Enabled = btnAddContact.Enabled = false;
                    break;
                case "Update":
                     btnUpdate.Enabled =btnAdd.Enabled = btnAddContact.Enabled = true;
                    break;
                case "Save":
                     btnUpdate.Enabled =btnAdd.Enabled = btnAddContact.Enabled = true;
                    break;
            }
        }
        private void ResetLineNo_Contact()
        {
            int i = 0;
            foreach (DataRow row in dtContactPerson.Rows)
                row["No"] = ++i;
        }
        protected void lvContactPerson_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                ListViewItem lvi = e.Item;
                switch (e.CommandName)
                {
                    case "CancelAddNew":
                        this._cancelAddNewContact();
                        this.lvContactPerson.DataBind();
                        break;
                    case "CancelUpdate":
                        this.lvContactPerson.EditIndex = -1;
                        this.lvContactPerson.DataSource = dtContactPerson;
                        this.lvContactPerson.DataBind();
                        break;
                    case "Update":
                        // update new data to dt
                        if (string.IsNullOrEmpty(((TextBox)lvi.FindControl("lblName")).Text)) return;

                        Label lblNo = (Label)lvi.FindControl("lblNo");
                        foreach (DataRow row in dtContactPerson.Rows)
                        {
                            if (row["No"].ToString().Equals(lblNo.Text)) // update currentrow into data table
                            {
                                row["Name"] = ((TextBox)lvi.FindControl("lblName")).Text;
                                row["FirstName"] = ((TextBox)lvi.FindControl("lblFirstName")).Text;
                                row["MiddleName"] = ((TextBox)lvi.FindControl("lblMiddleName")).Text;
                                row["LastName"] = ((TextBox)lvi.FindControl("lblLastName")).Text;
                                row["Title"] = ((TextBox)lvi.FindControl("lblTitle")).Text;
                                row["Position"] = ((TextBox)lvi.FindControl("lblPosition")).Text;
                                row["Address"] = ((TextBox)lvi.FindControl("lblAddress")).Text;
                                row["Tel1"] = ((TextBox)lvi.FindControl("lblTel1")).Text;
                                row["Tel2"] = ((TextBox)lvi.FindControl("lblTel2")).Text;
                                row["Cellolar"] = ((TextBox)lvi.FindControl("lblCellolar")).Text;
                                row["E_MailL"] = ((TextBox)lvi.FindControl("lblE_MailL")).Text;
                                break;
                            }
                        }
                        this.lvContactPerson.EditIndex = -1;
                        this.lvContactPerson.DataSource = dtContactPerson;
                        this.lvContactPerson.DataBind();
                        SetControlsStatus_Contact("Update");
                        break;

                    case "DeleteItem":
                        // delete data and update dt
                        int i_idx = e.Item.DataItemIndex;
                        dtContactPerson.Rows.RemoveAt(i_idx);
                        this.lvContactPerson.EditIndex = -1;
                        ResetLineNo_Contact();
                        this.lvContactPerson.DataSource = dtContactPerson;
                        this.lvContactPerson.DataBind();
                        SetControlsStatus_Contact("Update");
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ex.ToString()) + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }

        protected void lvContactPerson_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.lvContactPerson.EditIndex = e.NewEditIndex;
            this.lvContactPerson.DataSource = dtContactPerson;
            this.lvContactPerson.DataBind();
            SetControlsStatus_Contact("Edit");
        }

        protected void lvContactPerson_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            _cancelAddNewContact();
        }

        protected void lvContactPerson_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Label TextBox = (Label)lvContactPerson.Items[e.ItemIndex].FindControl("lblName");
            if (string.IsNullOrEmpty(TextBox.Text))
            {
                e.Cancel = true;
                return;
            }
            this.lvContactPerson.EditIndex = -1;
            this.lvContactPerson.DataSource = dtContactPerson;
            this.lvContactPerson.DataBind();
        }

        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            int iNo = dtContactPerson.Rows.Count + 1;
            dtContactPerson.Rows.Add(iNo, "", "", "", "", "", "", "", "", "", "", "");
            this.lvContactPerson.DataSource = dtContactPerson;

            int lastpage = this.ContactPager.TotalRowCount / this.ContactPager.PageSize;
            this.ContactPager.SetPageProperties(lastpage * this.ContactPager.PageSize, this.ContactPager.MaximumRows, false);
            this.lvContactPerson.EditIndex = iNo - 1;
            this.lvContactPerson.DataBind();
            SetControlsStatus_Contact("Add");
        }

        protected void ContactPager_PreRender(object sender, EventArgs e)
        {
            lvContactPerson.DataSource = dtContactPerson;
            lvContactPerson.DataBind();
        }
        #endregion

        #region "Address"
        private void _cancelAddNewAdd()
        {
            this.lvAddress.InsertItemPosition = InsertItemPosition.None;
            this.btnAddAddress.Enabled = true;
            this.lvAddress.DataBind();
        }
        private void SetControlsStatus_Add(string asStatus)
        {
            switch (asStatus)
            {
                case "Add":
                    btnUpdate.Enabled = btnAdd.Enabled = btnAddAddress.Enabled = false;
                    break;
                case "Edit":
                    btnUpdate.Enabled = btnAdd.Enabled = btnAddAddress.Enabled = false;
                    break;
                case "Update":
                    btnUpdate.Enabled = btnAdd.Enabled = btnAddAddress.Enabled = true;
                    break;
                case "Save":
                    btnUpdate.Enabled = btnAdd.Enabled = btnAddAddress.Enabled = true;
                    break;
            }
        }
        private void ResetLineNo_Add()
        {
            int i = 0;
            foreach (DataRow row in dtAddress.Rows)
                row["No"] = ++i;
        }
        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            int iNo = dtAddress.Rows.Count + 1;
            dtAddress.Rows.Add(iNo, "", "", "B", "", "");
            this.lvAddress.DataSource = dtAddress;

            int lastpage = this.AddressPager.TotalRowCount / this.AddressPager.PageSize;
            this.AddressPager.SetPageProperties(lastpage * this.AddressPager.PageSize, this.AddressPager.MaximumRows, false);
            this.lvAddress.EditIndex = iNo - 1;
            this.lvAddress.DataBind();
            SetControlsStatus_Add("Add");
        }

        protected void lvAddress_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                ListViewItem lvi = e.Item;
                switch (e.CommandName)
                {
                    case "CancelAddNew":
                        this._cancelAddNewAdd();
                        this.lvAddress.DataBind();
                        break;
                    case "CancelUpdate":
                        if (string.IsNullOrEmpty(((TextBox)lvi.FindControl("lblAddress")).Text))
                        {
                            dtAddress.Rows.RemoveAt(e.Item.DataItemIndex);
                            ResetLineNo_Add();
                        }
                        this.lvAddress.EditIndex = -1;
                            this.lvAddress.DataSource = dtAddress;
                            this.lvAddress.DataBind();
                            SetControlsStatus_Add("Update");
                        break;

                    case "Update":
                        // update new data to dt
                        if (string.IsNullOrEmpty(((TextBox)lvi.FindControl("lblAddress")).Text)) return;

                        Label lblNo = (Label)lvi.FindControl("lblNo");
                        foreach (DataRow row in dtAddress.Rows)
                        {
                            if (row["No"].ToString().Equals(lblNo.Text)) // update currentrow into data table
                            {
                                row["Address"] = ((TextBox)lvi.FindControl("lblAddress")).Text;
                                row["CardCode"] = txtCardCode.Text;
                                row["Street"] = ((TextBox)lvi.FindControl("lblStreet")).Text;
                                row["AdresType"] = ((DropDownList)lvi.FindControl("ddlAdresType")).SelectedItem.Value;
                                row["StreetNo"] = ((TextBox)lvi.FindControl("lblStreetNo")).Text;
                                row["Building"] = ((TextBox)lvi.FindControl("lblBuilding")).Text;
                                break;
                            }
                        }
                        this.lvAddress.EditIndex = -1;
                        this.lvAddress.DataSource = dtAddress;
                        this.lvAddress.DataBind();
                        SetControlsStatus_Add("Update");
                        break;

                    case "DeleteItem":
                        // delete data and update dt
                        int i_idx = e.Item.DataItemIndex;
                        dtAddress.Rows.RemoveAt(i_idx);
                        this.lvAddress.EditIndex = -1;
                        ResetLineNo_Add();
                        this.lvAddress.DataSource = dtAddress;
                        this.lvAddress.DataBind();
                        SetControlsStatus_Add("Update");
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ex.ToString()) + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }

        protected void lvAddress_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.lvAddress.EditIndex = e.NewEditIndex;
            this.lvAddress.DataSource = dtAddress;
            this.lvAddress.DataBind();
            SetControlsStatus_Add("Edit");
        }

        protected void lvAddress_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            _cancelAddNewAdd();
        }

        protected void lvAddress_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Label TextBox = (Label)lvAddress.Items[e.ItemIndex].FindControl("lblAddress");
            if (string.IsNullOrEmpty(TextBox.Text))
            {
                e.Cancel = true;
                return;
            }
            this.lvAddress.EditIndex = -1;
            this.lvAddress.DataSource = dtAddress;
            this.lvAddress.DataBind();
        }

        protected void AddressPager_PreRender(object sender, EventArgs e)
        {
            lvAddress.DataSource = dtAddress;
            lvAddress.DataBind();
        }

        #endregion

        protected void btnActivity_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/BusinessPartner/Activity.aspx?CardCode=" + txtCardCode.Text + "&CardName=" + txtCardName.Text);
        }

       



    }
}