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
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private GeneralFunctions GF= new GeneralFunctions(HttpContext.Current.User.Identity.Name);
        private string DocType = "22";
        private string TblHeaderName = "OPOR";
        private string TblLineName = "POR1";
        private string CurrentPageUrl = "/Purchase/PurchaseOrder.aspx";
        private string HeaderKeyName = "DocEntry";

        private String TargetPageUrl = "/Purchase/GoodsReceiptPO.aspx";

        public static string KeepColumsContent = "";
    #region Functions
        protected void SetNavigatorURL(string CurrentKey)
        {
            GetDefault df = new GetDefault();
            DataSet nav = df.GetNextPreviousID(DocType, User.Identity.Name, TblHeaderName, HeaderKeyName, CurrentKey);
            if (nav != null)
            {
                this.linkFirst.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Fir"].ToString(); ;
                this.linkNext.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Nex"].ToString(); ;
                this.linkPrevious.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Pre"].ToString(); ;
                this.linkLast.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Las"].ToString(); ;
            }
            this.linkNew.NavigateUrl = CurrentPageUrl + "?order_id=";
        }
        protected void LoadData(String orderId, string NewDocType)
        {
            Transaction ts = new Transaction();
            DataSet returnDoc = ts.GetMarketingDocument_ReturnDS(NewDocType, orderId, User.Identity.Name);
            DataTable dtHeader;

            if (returnDoc == null)
                orderId = "1";
            else
                dtHeader = returnDoc.Tables[0];

            returnDoc = ts.GetMarketingDocument_ReturnDS(NewDocType, orderId, User.Identity.Name);

            if (returnDoc == null || returnDoc.Tables.Count<2 )
            {

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                    "Main.setMasterMessage('No record found!','');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
                return;
            }

            dtHeader = returnDoc.Tables[0];
            dtContents = GF.ConvertDataTable_RemoveCols(returnDoc.Tables[1], KeepColumsContent);
            DataRow dr = dtHeader.Rows[0];
            SetNavigatorURL(dr["DocEntry"].ToString());

            this.txtName.Text = dr["CardName"].ToString();
            this.txtVendor.Text = dr["CardCode"].ToString();
            lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtVendor.Text;

            MasterData masterDataWS = new MasterData();
            ddlContactPerson.DataSource = masterDataWS.GetContactPerson(txtVendor.Text, User.Identity.Name).Tables[0];
            ddlContactPerson.DataValueField = "Code";
            ddlContactPerson.DataTextField = "FirstName";
            ddlContactPerson.DataBind();

            this.txtNo.Text = dr["DocEntry"].ToString();
            this.txtStatus.Text = dr["DocStatus"].ToString();
            CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

            DateTime d = Convert.ToDateTime(dr["DocDate"], ivC);
            txtPostingDate.Text = String.Format("{0:MM/dd/yyyy}", d);

            d = Convert.ToDateTime(dr["TaxDate"], ivC);
            txtDocumentDate.Text = String.Format("{0:MM/dd/yyyy}", d);

            d = Convert.ToDateTime(dtHeader.Rows[0]["DocDueDate"], ivC);
            txtDueDate.Text = String.Format("{0:MM/dd/yyyy}", d);

            txtRefNo.Text = dtHeader.Rows[0]["LicTradNum"].ToString();
            txtTotalPayment.Text = dtHeader.Rows[0]["DocTotal"].ToString();

            ddlBuyer.SelectedValue= dr["slpcode"].ToString();
            txtOwnerCode.Text = dr["OwnerCode"].ToString();

            DataTable dtEm= ts.GetMarketingDocument_ReturnDS("171", dr["OwnerCode"].ToString(), User.Identity.Name).Tables[0];
            if (dtEm.Rows.Count>0)
                txtOwner.Text = dtEm.Rows[0]["firstName"].ToString() + " " + dtEm.Rows[0]["middleName"].ToString() + " " + dtEm.Rows[0]["lastName"].ToString();
            
            ddlContactPerson.SelectedValue = dr["CntctCode"].ToString();

            ddlShippingType.SelectedValue= dr["TrnspCode"].ToString();
            txtPayTo.Text =dr["Address"].ToString();
            txtShipTo.Text =dr["Address2"].ToString()  ;
            txtProjectCode.Text =dr["Project"].ToString();
            txtFederalTaxID.Text =dr["LicTradNum"].ToString();

            ddlPaymentTerm.SelectedValue=  dr["GroupNum"].ToString();
            if(!string.IsNullOrEmpty(dr["CancelDate"].ToString()))
                txtCancellationDate.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dr["CancelDate"], ivC));
            if (!string.IsNullOrEmpty(dr["ReqDate"].ToString()))
                txtRequiredDate.Text = String.Format("{0:MM/dd/yyyy}",Convert.ToDateTime(dr["ReqDate"], ivC)); 
            ddlIndicator.SelectedValue= dr["Indicator"].ToString();
            txtOrderNumber.Text=dr["ImportEnt"].ToString();

            txtJournalRemark.Text = dr["JrnlMemo"].ToString();
            txtRemarks.Text = dr["Comments"].ToString();
            txtRefNo.Text = dr["NumAtCard"].ToString();

            foreach (DataRow row in dtContents.Rows)
            {
                updateRowTotalPrice(row);
                if (DocType != NewDocType) //truong hop copy to
                {
                    row["BaseEntry"] = orderId;
                    row["BaseType"] = NewDocType;
                    row["BaseLine"] = row["LineNum"];
                }
            }
            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();
            updateTableTotalPrice();
            
        }       
        protected void ClearScreen()
        {
            dtHeader = new DataTable("OPOR");
            dtHeader.Columns.Add("CardCode");
            dtHeader.Columns.Add("CardName");
            dtHeader.Columns.Add("TaxDate");
            dtHeader.Columns.Add("DocDate");
            dtHeader.Columns.Add("DocDueDate");
            dtHeader.Columns.Add("Comments");
            dtHeader.Columns.Add("JrnlMemo");

            dtHeader.Columns.Add("slpcode");
            dtHeader.Columns.Add("OwnerCode");
            dtHeader.Columns.Add("TrnspCode");
            dtHeader.Columns.Add("Address");
            dtHeader.Columns.Add("Address2");
            dtHeader.Columns.Add("Project");
            dtHeader.Columns.Add("LicTradNum");
            dtHeader.Columns.Add("NumAtCard");
            dtHeader.Columns.Add("GroupNum");
            dtHeader.Columns.Add("CancelDate");
            dtHeader.Columns.Add("ReqDate");
            dtHeader.Columns.Add("Indicator");
            dtHeader.Columns.Add("ImportEnt");

            dtHeader.Columns.Add("U_UserID");
            dtHeader.Rows.Add();

            dtContents = new DataTable("POR1");
            dtContents.Columns.Add("No");
            dtContents.Columns.Add("ItemCode");
            dtContents.Columns.Add("Dscription");
            dtContents.Columns.Add("Quantity");
            dtContents.Columns.Add("PriceBefDi");
            dtContents.Columns.Add("DiscPrcnt");
            dtContents.Columns.Add("Price");
            dtContents.Columns.Add("LineTotal");
            dtContents.Columns.Add("TaxCode");
            dtContents.Columns.Add("VatPrcnt");
            dtContents.Columns.Add("WhsCode");
            dtContents.Columns.Add("OcrCode");
            dtContents.Columns.Add("OcrCode2");
            dtContents.Columns.Add("OcrCode3");
            dtContents.Columns.Add("OcrCode4");
            dtContents.Columns.Add("OcrCode5");
            dtContents.Columns.Add("BaseEntry");
            dtContents.Columns.Add("BaseLine");
            dtContents.Columns.Add("BaseType");
            dtContents.Columns.Add("LineNum");
            KeepColumsContent = GF.BuildKeepColumnStr(dtContents);

            CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            txtPostingDate.Text.ToString(ci);

            txtRefNo.Text = "";
            txtOrderNumber.Text = "";
            txtOwnerCode.Text = "";
            txtOwner.Text = "";
            txtProjectCode.Text = "";
            txtNo.Text = "";
            txtRemarks.Text = "";
            txtJournalRemark.Text = "";
            txtCancellationDate.Text = "";
            txtRequiredDate.Text = "";
            txtFederalTaxID.Text="";
            txtShipTo.Text = "";
            txtPayTo.Text = "";
            txtVendor.Text = "";
            txtName.Text = "";

            dtContents.Clear();
            SetNavigatorURL("0");
            SetScreenStatus("New");
        }
        private void ResetLineNo()
        {
            int i = 0;
            foreach (DataRow row in dtContents.Rows)
                row["No"] = ++i;
        }
        private void SetControlsStatus(string asStatus)
        {
            switch (asStatus)
            {
                case "Add":
                    btnUpdate.Enabled = btnAdd.Enabled = btnAddRecord.Enabled = false;
                    break;
                case "Edit":
                     btnUpdate.Enabled =btnAdd.Enabled = btnAddRecord.Enabled = false;
                    break;
                case "Update":
                     btnUpdate.Enabled =btnAdd.Enabled = btnAddRecord.Enabled = true;
                    break;
                case "Save":
                     btnUpdate.Enabled =btnAdd.Enabled = btnAddRecord.Enabled = true;
                    break;
            }
        }
        public String _collectData()
        {
            try
            {
                //Update table header
                DataRow dr = dtHeader.Rows[0];

                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                dr["DocDate"] = Convert.ToDateTime(txtDueDate.Text, ivC).ToString("yyyyMMdd");
                dr["DocDueDate"] = Convert.ToDateTime(txtDueDate.Text, ivC).ToString("yyyyMMdd");
                dr["TaxDate"] = Convert.ToDateTime(txtDocumentDate.Text, ivC).ToString("yyyyMMdd");

                dr["Comments"] = txtRemarks.Text;
                dr["JrnlMemo"] = txtJournalRemark.Text;
                dr["CardCode"] = txtVendor.Text;
                dr["CardName"] = txtName.Text;

                dr["slpcode"]=ddlBuyer.SelectedItem.Value.ToString();
                dr["OwnerCode"]=txtOwnerCode.Text;
                dr["TrnspCode"]=ddlShippingType.SelectedItem.Value.ToString();
                dr["Address"]=txtPayTo.Text;
                dr["Address2"]=txtShipTo.Text;
                dr["Project"]=txtProjectCode.Text;
                dr["LicTradNum"]=txtFederalTaxID.Text;
                dr["NumAtCard"] = txtRefNo.Text;
                dr["GroupNum"]=ddlPaymentTerm.SelectedItem.Value.ToString();
                if(!string.IsNullOrEmpty(txtCancellationDate.Text))
                    dr["CancelDate"]=Convert.ToDateTime(txtCancellationDate.Text, ivC).ToString("yyyyMMdd");;
                if (!string.IsNullOrEmpty(txtRequiredDate.Text))
                    dr["ReqDate"]=Convert.ToDateTime(txtRequiredDate.Text, ivC).ToString("yyyyMMdd");;
                dr["Indicator"]=ddlIndicator.SelectedItem.Value.ToString();
                dr["ImportEnt"]=txtOrderNumber.Text;
                dr["U_UserID"] = User.Identity.Name;
                Array arrContentsCols = new string[] { "Quantity", "PriceBefDi", "Price", "DiscPrcnt", "LineTotal" }; // Columns need to reset format numeric

                DocumentXML objInfo = new DocumentXML();
                DataSet ds = new DataSet("DS");
                dtHeader.TableName = TblHeaderName;
                dtContents.TableName = TblLineName;
                ds.Tables.Add(dtHeader.Copy());
                ds.Tables.Add(GF.ResetFormatNumeric(dtContents, arrContentsCols).Copy());

                return objInfo.ToXMLStringFromDS(DocType, ds);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + ex.ToString() + "','');", true);
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
                    btnAddRecord.Visible = true;
                    btnCopyTo.Visible = false;
                    break;
                case "Update":
                    btnAdd.Visible = false;
                    btnUpdate.Visible = false;
                    btnAddRecord.Visible = false;
                    btnCopyTo.Visible = true;
                    break;
                case "Close":
                    btnAdd.Visible = false;
                    btnUpdate.Visible = false;
                    btnAddRecord.Visible = false;
                    btnCopyTo.Visible = false;
                    break;
            }
        }
        protected void updateTableTotalPrice()
        {
            double orderTotalBeforeDiscount = 0.0;
            double orderTotal = 0.0;
            double taxTotal = 0.0;
            foreach (DataRow row in dtContents.Rows)
            {
                if (!"".Equals(row["ItemCode"]))
                {
                    double total = GF.Object2Double((object)GF.ResetFormatNumeric(row["LineTotal"].ToString()), "SumDec");
                    double taxRate = GF.Object2Double(row["VatPrcnt"], "RateDec");
                    if (taxRate == 0) taxRate = 10;
                    double tax = total * taxRate / 100;

                    orderTotalBeforeDiscount += total;
                    taxTotal += tax;
                }
            }
            this.txtTotalDiscount.Text = GF.FormatNumeric(orderTotalBeforeDiscount.ToString(), "SumDec");
            this.txtTax.Text = GF.FormatNumeric(taxTotal.ToString(), "SumDec");
            orderTotal = orderTotalBeforeDiscount + taxTotal;
            this.txtTotalPayment.Text = GF.FormatNumeric(orderTotal.ToString(), "SumDec");
        }
        public void updateRowTotalPrice(DataRow row)
        {
            double quantity = 0, unitPrice = 0.0, discount = 0, priceAfterDiscount = 0.0, total = 0;
            quantity = GF.Object2Double(row["Quantity"], "QtyDec");
            unitPrice = GF.Object2Double(row["PriceBefDi"], "PriceDec");
            discount = GF.Object2Double(row["DiscPrcnt"], "PercentDec");

            priceAfterDiscount = GF.Object2Double((Object)(unitPrice * (100 - discount) / 100), "PriceDec");
            total = GF.Object2Double((Object)(priceAfterDiscount * quantity), "SumDec");

            row["PriceBefDi"] = GF.FormatNumeric(unitPrice.ToString(), "PriceDec");
            row["DiscPrcnt"] = GF.FormatNumeric(discount.ToString(), "PercentDec");
            row["Price"] = GF.FormatNumeric(priceAfterDiscount.ToString(), "PriceDec");
            row["LineTotal"] = GF.FormatNumeric(total.ToString(), "SumDec");
            row["Quantity"] = GF.FormatNumeric(quantity.ToString(), "QtyDec");
        }
        private void _cancelAddNew()
        {
            this.lvContents.InsertItemPosition = InsertItemPosition.None;
            this.btnAddRecord.Enabled = true;
            this.lvContents.DataBind();
        }
        protected void LoadDefault()
        {
            //-------------Load Default BP----------------
            //GetDefault getDefaultWS = new GetDefault();
            //DataSet defaultVendor = getDefaultWS.GetDefaultBP(User.Identity.Name, "S");

            ////extract to funtion later
            //if (defaultVendor != null)
            //{
            //    this.txtVendor.Text = defaultVendor.Tables[0].Rows[0]["CardCode"].ToString();
            //    this.txtName.Text = defaultVendor.Tables[0].Rows[0]["CardName"].ToString();

            //    lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtVendor.Text;
            //    MasterData masterDataWS = new MasterData();
            //    ddlContactPerson.DataSource = masterDataWS.GetContactPerson(txtVendor.Text, User.Identity.Name).Tables[0];
            //    ddlContactPerson.DataValueField = "Code";
            //    ddlContactPerson.DataTextField = "FirstName";
            //    ddlContactPerson.DataBind();
            //}
            this.txtStatus.Text = "Open";
            this.txtStatus.Enabled = false;
            this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
            this.txtDueDate.Text = DateTime.Now.ToShortDateString();
            this.txtDocumentDate.Text = DateTime.Now.ToShortDateString();
            this.txtNo.Text = "";
        }
    #endregion

    #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    
                    ClearScreen();

                    MasterData masterDataWS = new MasterData();
                    DataSet ds;
                    GF = new GeneralFunctions(User.Identity.Name);

                    //-------------Load Sales/Buyer Employee----------------
                    ds = masterDataWS.GetSalesBuyerMasterData(User.Identity.Name);
                    ddlBuyer.DataSource = ds.Tables[0];
                    ddlBuyer.DataTextField = "name";
                    ddlBuyer.DataValueField = "code";
                    ddlBuyer.DataBind();

                    //-------------Load Shipping Type---------------------
                    ds = masterDataWS.GetShippingType(User.Identity.Name);
                    ds.Tables[0].Rows.Add("-1", "");
                    ddlShippingType.DataSource = ds.Tables[0];
                    ddlShippingType.DataTextField = "TrnspName";
                    ddlShippingType.DataValueField = "TrnspCode";
                    ddlShippingType.DataBind();
                    ddlShippingType.SelectedValue = "-1";

                    //-------------Load Payment Tern----------------
                    ds = masterDataWS.GetPaymentTerm(User.Identity.Name);
                    ddlPaymentTerm.DataSource = ds.Tables[0];
                    ddlPaymentTerm.DataTextField = "PymntGroup";
                    ddlPaymentTerm.DataValueField = "GroupNum";
                    ddlPaymentTerm.DataBind();

                    //-------------Load Indicator----------------
                    ds = masterDataWS.GetIndicator(User.Identity.Name);
                    ds.Tables[0].Rows.Add("", "");
                    ddlIndicator.DataSource = ds.Tables[0];
                    ddlIndicator.DataTextField = "Name";
                    ddlIndicator.DataValueField = "Code";
                    ddlIndicator.DataBind();
                    ddlIndicator.SelectedValue = "";

                    String orderId = Request.QueryString["order_id"];
                    if (!String.IsNullOrEmpty(orderId))
                    {
                        LoadData(orderId, DocType);
                        if (txtStatus.Text == "C")
                            SetScreenStatus("Close");
                        else if (txtStatus.Text == "O")
                            SetScreenStatus("Update");   
                    }
                    else
                    {
                        LoadDefault();
                        SetScreenStatus("New");
                    }

                    String BaseEntry = Request.QueryString["base_entry"];
                    if (!String.IsNullOrEmpty(BaseEntry))
                    {
                        String BaseType = Request.QueryString["base_type"];
                        LoadData(BaseEntry, BaseType);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            try
            {
                base.OnLoadComplete(e);
                if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
                {
                    Int32 itemNo = 0;

                    switch (this.Request["__EVENTARGUMENT"].ToString())
                    {
                        case "EditItemCallBack":
                            ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;
                            itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                            if (chosenItem != null)
                            {   // update grid
                                dtContents.Rows[itemNo - 1]["ItemCode"] = chosenItem.ItemCode;
                                dtContents.Rows[itemNo - 1]["Dscription"] = chosenItem.ItemName;
                                dtContents.Rows[itemNo - 1]["Quantity"] = 1;

                                GetDefault defaultWS = new GetDefault();
                                DateTime postingDate = DateTime.Parse(this.txtPostingDate.Text);
                                DataSet defaultInfo = defaultWS.GetDefaultLineInfo(User.Identity.Name, this.txtVendor.Text, chosenItem.ItemCode, 1, postingDate);

                                dtContents.Rows[itemNo - 1]["PriceBefDi"] = defaultInfo.Tables[0].Rows[0]["UnitPrice"];
                                dtContents.Rows[itemNo - 1]["DiscPrcnt"] = defaultInfo.Tables[0].Rows[0]["Discount"];
                                dtContents.Rows[itemNo - 1]["PriceBefDi"] = defaultInfo.Tables[0].Rows[0]["PriceAfDi"];

                                dtContents.Rows[itemNo - 1]["TaxCode"] = defaultInfo.Tables[0].Rows[0]["TaxCode"];
                                dtContents.Rows[itemNo - 1]["VatPrcnt"] = defaultInfo.Tables[0].Rows[0]["TaxRate"];
                                dtContents.Rows[itemNo - 1]["WhsCode"] = defaultInfo.Tables[0].Rows[0]["WhsCode"];
                                //dt.Rows.      
                                updateTableTotalPrice();
                                //dtContents.AcceptChanges();
                                this.lvContents.DataSource = dtContents;
                                this.lvContents.DataBind();
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                         "Dialog.hideLoader();", true);
                            }
                            break;
                        case "EditWareHouseCallBack":
                            WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;
                            itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                            if (chosenWarehouse != null)
                            {   // update grid
                                dtContents.Rows[itemNo - 1]["WhsCode"] = chosenWarehouse.WhsCode;

                                dtContents.Rows[itemNo - 1]["Quantity"] = ((TextBox)lvContents.Items[itemNo - 1].FindControl("txtQuantity")).Text;
                                dtContents.Rows[itemNo - 1]["PriceBefDi"] = ((TextBox)lvContents.Items[itemNo - 1].FindControl("txtUnitPrice")).Text;
                                dtContents.Rows[itemNo - 1]["DiscPrcnt"] = ((TextBox)lvContents.Items[itemNo - 1].FindControl("txtDiscount")).Text;
                                //dt.Rows.
                                //dtContents.AcceptChanges();
                                this.lvContents.DataSource = dtContents;
                                this.lvContents.DataBind();
                            } break;
                        case "EditTaxCodeCallBack":
                            TaxGroup chosenTaxCode = Session["chosenTaxCode"] as TaxGroup;
                            itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                            if (chosenTaxCode != null)
                            {
                                // update grid
                                dtContents.Rows[itemNo - 1]["TaxCode"] = chosenTaxCode.Code;
                                dtContents.Rows[itemNo - 1]["VatPrcnt"] = chosenTaxCode.Rate;

                                dtContents.Rows[itemNo - 1]["Quantity"] = ((TextBox)lvContents.Items[itemNo - 1].FindControl("txtQuantity")).Text;
                                dtContents.Rows[itemNo - 1]["PriceBefDi"] = ((TextBox)lvContents.Items[itemNo - 1].FindControl("txtUnitPrice")).Text;
                                dtContents.Rows[itemNo - 1]["DiscPrcnt"] = ((TextBox)lvContents.Items[itemNo - 1].FindControl("txtDiscount")).Text;
                                //dt.Rows.
                                //dtContents.AcceptChanges();
                                this.lvContents.DataSource = dtContents;
                                this.lvContents.DataBind();
                            }
                            break;
                        case "EditBusinessPartnerCallBack":
                            BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                            if (chosenPartner != null)
                            {
                                this.txtName.Text = chosenPartner.CardName;
                                this.txtVendor.Text = chosenPartner.CardCode;
                                lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtVendor.Text;
                                this.txtStatus.Text = "Open";

                                this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
                                this.txtDueDate.Text = DateTime.Now.ToShortDateString();
                                this.txtDocumentDate.Text = DateTime.Now.ToShortDateString();
                                this.txtNo.Text = "";

                                lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtVendor.Text;
                                MasterData masterDataWS = new MasterData();
                                ddlContactPerson.DataSource = masterDataWS.GetContactPerson(txtVendor.Text, User.Identity.Name).Tables[0];
                                ddlContactPerson.DataValueField = "Code";
                                ddlContactPerson.DataTextField = "FirstName";
                                ddlContactPerson.DataBind();
                            }
                            break;
                        case "EditEmployeeCallBack":
                            EmployeeMasterData employee = Session["chosenEmployee"] as EmployeeMasterData;
                            if (employee != null)
                            {
                                this.txtOwner.Text = employee.FirstName + " " + employee.MidName + " " + employee.LastName;
                                this.txtOwnerCode.Text = employee.Code;
                            }
                            break;
                        case "EditProjectCallBack":
                            Project prj = Session["chosenProject"] as Project;
                            if (prj != null)
                            {
                                this.txtProjectCode.Text = prj.PrjCode;
                            }
                            break;
                        case "EditCostCenterCallBack":
                            CostCenter chosenCostCenter = Session["chosenCostCenter"] as CostCenter;
                            itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                            if (chosenCostCenter != null)
                            {
                                // update grid
                                switch (Session["chosenDimesion"].ToString())
                                {
                                    case "1":
                                        dtContents.Rows[itemNo - 1]["OcrCode"] = chosenCostCenter.PrcCode;
                                        break;
                                    case "2":
                                        dtContents.Rows[itemNo - 1]["OcrCode2"] = chosenCostCenter.PrcCode;
                                        break;
                                    case "3":
                                        dtContents.Rows[itemNo - 1]["OcrCode3"] = chosenCostCenter.PrcCode;
                                        break;
                                    case "4":
                                        dtContents.Rows[itemNo - 1]["OcrCode4"] = chosenCostCenter.PrcCode;
                                        break;
                                    case "5":
                                        dtContents.Rows[itemNo - 1]["OcrCode5"] = chosenCostCenter.PrcCode;
                                        break;
                                    default:
                                        break;
                                       
                                }
                                
                                this.lvContents.DataSource = dtContents;
                                this.lvContents.DataBind();
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
                                                        "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }      
        protected void _btnAddRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVendor.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('Missing Vendor','');", true);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                        "Dialog.hideLoader();", true);
                    return;
                }
                int iNo = dtContents.Rows.Count + 1;
                dtContents.Rows.Add();
                dtContents.Rows[iNo-1]["No"] = iNo;

                this.lvContents.DataSource = dtContents;

                int lastpage = this.ProductListPagerCombo.TotalRowCount / this.ProductListPagerCombo.PageSize;
                this.ProductListPagerCombo.SetPageProperties(lastpage * this.ProductListPagerCombo.PageSize, this.ProductListPagerCombo.MaximumRows, false);
                this.lvContents.EditIndex = iNo - 1;
                this.lvContents.DataBind();
                SetControlsStatus("Add");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }
        protected void lvContents_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                ListViewItem lvi = e.Item;
                switch (e.CommandName)
                {
                    case "CancelAddNew":
                        this._cancelAddNew();
                        this.lvContents.DataBind();
                        break;
                    case "CancelUpdate":
                        if (string.IsNullOrEmpty(((Label)lvi.FindControl("lblCode")).Text))
                        {
                            dtContents.Rows.RemoveAt(e.Item.DataItemIndex);
                            ResetLineNo();
                        }
                        this.lvContents.EditIndex = -1;
                        this.lvContents.DataSource = dtContents;
                        this.lvContents.DataBind();
                        SetControlsStatus("Update");
                        break;
                    case "Update":
                        // update new data to dt
                        if (string.IsNullOrEmpty(((Label)lvi.FindControl("lblCode")).Text)) return;

                        Label lblNo = (Label)lvi.FindControl("lblNo");
                        foreach (DataRow row in dtContents.Rows)
                        {
                            if (row["No"].ToString().Equals(lblNo.Text)) // update currentrow into data table
                            {
                                row["ItemCode"] = ((Label)lvi.FindControl("lblCode")).Text;
                                row["Dscription"] = ((Label)lvi.FindControl("lblDescription")).Text;
                                row["Quantity"] = ((TextBox)lvi.FindControl("txtQuantity")).Text;
                                row["PriceBefDi"] = ((TextBox)lvi.FindControl("txtUnitPrice")).Text;
                                row["DiscPrcnt"] = ((TextBox)lvi.FindControl("txtDiscount")).Text;
                                updateRowTotalPrice(row);
                                row["TaxCode"] = ((Label)lvi.FindControl("lblTaxcode")).Text;
                                row["WhsCode"] = ((Label)lvi.FindControl("lblWhse")).Text;
                                row["VatPrcnt"] = ((Label)lvi.FindControl("lblTaxRate")).Text;
                                row["OcrCode"] = ((Label)lvi.FindControl("lblProfitCode")).Text;
                                row["OcrCode2"] = ((Label)lvi.FindControl("lblCC1")).Text;
                                row["OcrCode3"] = ((Label)lvi.FindControl("lblCC2")).Text;
                                row["OcrCode4"] = ((Label)lvi.FindControl("lblCC3")).Text;
                                row["OcrCode5"] = ((Label)lvi.FindControl("lblCC4")).Text;
                                updateTableTotalPrice();
                                break;
                            }
                        }
                        this.lvContents.EditIndex = -1;
                        this.lvContents.DataSource = dtContents;
                        this.lvContents.DataBind();
                        SetControlsStatus("Update");
                        break;

                    case "DeleteItem":
                        // delete data and update dt
                        int i_idx = e.Item.DataItemIndex;
                        dtContents.Rows.RemoveAt(i_idx);
                        this.lvContents.EditIndex = -1;
                        ResetLineNo();
                        this.lvContents.DataSource = dtContents;
                        this.lvContents.DataBind();
                        SetControlsStatus("Update");
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }
        protected void lvContents_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            try
            {
                this.lvContents.EditIndex = e.NewEditIndex;

                // Reset FormatNumeric 
                string lsQty = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["Quantity"].ToString());       // Quantity
                string lsUPrBeDi = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["PriceBefDi"].ToString()); // Unitprice
                string lsPrice = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["Price"].ToString());        // PriceAfterDiscount
                string lsDisPrcnt = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["DiscPrcnt"].ToString()); // DiscPrcnt
                string lsTotal = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["LineTotal"].ToString());    // Total

                dtContents.Rows[e.NewEditIndex]["Quantity"] = lsQty;
                dtContents.Rows[e.NewEditIndex]["PriceBefDi"] = lsUPrBeDi;
                dtContents.Rows[e.NewEditIndex]["Price"] = lsPrice;
                dtContents.Rows[e.NewEditIndex]["LineTotal"] = lsTotal;

                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();
                SetControlsStatus("Edit");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }
        protected void lvContents_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            this._cancelAddNew();
        }
        protected void imgbCancel_CancelAddNew(object sender, EventArgs e)
        {
            this.lvContents.DataSource = dtContents;
            this._cancelAddNew();
        }
        protected void lvContents_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Label lblCode = (Label)lvContents.Items[e.ItemIndex].FindControl("lblCode");
            if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
            {
                e.Cancel = true;
                return;
            }
            this.lvContents.EditIndex = -1;
            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();
        }
        protected void ProductListPagerCombo_PreRender(object sender, EventArgs e)
        {
            lvContents.DataSource = dtContents;
            lvContents.DataBind();
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                String requestXML = _collectData();
                if (requestXML == "") return;

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
                SetControlsStatus("Save");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }
        protected void btnCopyTo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(TargetPageUrl + "?base_type=" + DocType + "&base_entry=" + txtNo.Text);
        }
    #endregion

        
    }
}