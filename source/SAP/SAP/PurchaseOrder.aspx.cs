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

namespace SAP
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private GeneralFunctions GF;
        private string DocType = "22";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                dtHeader = new DataTable();
                dtHeader.Columns.Add("CardCode");
                dtHeader.Columns.Add("CardName");
                dtHeader.Columns.Add("TaxDate");
                dtHeader.Columns.Add("DocDate");
                dtHeader.Columns.Add("DocDueDate");
                dtHeader.Columns.Add("Comments");
                dtHeader.Columns.Add("JrnlMemo");
                dtHeader.Columns.Add("U_UserID");
                dtHeader.Rows.Add("", "", "", "", "", "From SAP WEB", "Goods Receipts JE Remark", User.Identity.Name);

                dtContents = new DataTable();
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

                CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                //ci.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
                txtPostingDate.Text.ToString(ci);

                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();
                

                MasterData masterDataWS = new MasterData();
                //-------------Load Sales/Buyer Employee----------------
                GF = new GeneralFunctions(User.Identity.Name);
                DataSet salesBuyers = masterDataWS.GetSalesBuyerMasterData(User.Identity.Name);
                ListItem item = new ListItem();
                foreach (DataRow row in salesBuyers.Tables[0].Rows)
                {
                    item = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlBuyer.Items.Add(item);
                }
                //-------------Load Shipping Type---------------------
                DataSet ShippingType = masterDataWS.GetShippingType(User.Identity.Name);
                ListItem itemShipping = new ListItem();
                foreach (DataRow row in ShippingType.Tables[0].Rows)
                {
                    itemShipping = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlShippingType.Items.Add(itemShipping);
                }
                //-------------Load Payment Tern----------------
                DataSet PaymentTerm = masterDataWS.GetPaymentTerm(User.Identity.Name);
                ListItem itemPaymenTerm = new ListItem();
                foreach (DataRow row in PaymentTerm.Tables[0].Rows)
                {
                    itemPaymenTerm = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlPaymentTerm.Items.Add(itemPaymenTerm);
                }
                //-------------Load Indicator----------------
                DataSet Indicator = masterDataWS.GetIndicator(User.Identity.Name);
                ListItem itemIndicator = new ListItem();
                foreach (DataRow row in Indicator.Tables[0].Rows)
                {
                    itemIndicator = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlIndicator.Items.Add(itemIndicator);
                } 

                ClearScreen();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                this._ddlCurency_SelectedIndexChanged(null, null);
            }
            // get callback from popup
            if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
            {
                Int32 itemNo = 0;
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);

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
                    case "EditVendorCallBack":
                        BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                        if (chosenPartner != null)
                        {
                            this.txtName.Text = chosenPartner.CardName;
                            this.txtVendor.Text = chosenPartner.CardCode;
                            this.txtStatus.Text = "Open";
                            this.txtStatus.Enabled = false;

                            this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
                            this.txtDueDate.Text = DateTime.Now.ToShortDateString();
                            this.txtDocumentDate.Text = DateTime.Now.ToShortDateString();
                            this.txtNoFrom.Text = "227";
                            this.txtNoFrom.Enabled = false;
                            this.txtNoTo.Text = "0";
                            this.txtNoTo.Enabled = false;

                            MasterData masterDataWS = new MasterData();
                            DataSet contactPersons = masterDataWS.GetContactPerson(chosenPartner.CardCode, User.Identity.Name);
                            ListItem item = new ListItem();
                            foreach (DataRow row in contactPersons.Tables[0].Rows)
                            {
                                String name = row[1].ToString() + " " + row[2].ToString();
                                item = new ListItem(name, row[1].ToString());
                                if ("Y".Equals(row[0].ToString()))
                                    item.Selected = true;
                                ddlContactPerson.Items.Add(item);
                            }
                        }
                        break;
                    case "EditEmployeeCallBack":
                        EmployeeMasterData employee = Session["chosenEmployee"] as EmployeeMasterData;
                        if (employee != null)
                        {
                            this.txtOwner.Text = employee.FirstName + " " + employee.MidName + " " + employee.LastName;
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        #region Functions
        private int GetNo()
        {
            return dtContents.Rows.Count + 1;
        }

        protected void ClearScreen()
        {
            //-------------Load Default BP----------------
            GetDefault getDefaultWS = new GetDefault();
            DataSet defaultVendor = getDefaultWS.GetDefaultBP(User.Identity.Name, "S");

            //extract to funtion later
            if (defaultVendor != null)
            {
                this.txtVendor.Text = defaultVendor.Tables[0].Rows[0]["CardCode"].ToString();
                this.txtName.Text = defaultVendor.Tables[0].Rows[0]["CardName"].ToString();
                this.txtStatus.Text = "Open";
                this.txtStatus.Enabled = false;
                this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
                this.txtDueDate.Text = DateTime.Now.ToShortDateString();
                this.txtDocumentDate.Text = DateTime.Now.ToShortDateString();
                this.txtNoFrom.Text = "227";
                this.txtNoFrom.Enabled = false;
                this.txtNoTo.Text = "0";
                this.txtNoTo.Enabled = false;
                MasterData masterDataWS = new MasterData();
                DataSet contactPersons = masterDataWS.GetContactPerson(defaultVendor.Tables[0].Rows[0]["CardCode"].ToString(), User.Identity.Name);
                ListItem item = new ListItem();
                foreach (DataRow row in contactPersons.Tables[0].Rows)
                {
                    String name = row[1].ToString() + " " + row[2].ToString();
                    item = new ListItem(name, row[1].ToString());
                    if ("Y".Equals(row[0].ToString()))
                        item.Selected = true;
                    ddlContactPerson.Items.Add(item);
                }
            }
            dtContents.Clear();
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
                    btnAdd.Enabled = btnAddRecord.Enabled = false;
                    btnCopyFrom.Enabled = false;
                    btnCopyTo.Enabled = false;
                    break;
                case "Edit":
                    btnAdd.Enabled = btnAddRecord.Enabled = false;
                    btnCopyFrom.Enabled = btnCopyTo.Enabled = false;
                    break;
                case "Update":
                    btnAdd.Enabled = btnAddRecord.Enabled = true;
                    btnCopyFrom.Enabled = btnCopyTo.Enabled = true;
                    break;
                case "Save":
                    btnAdd.Enabled = btnAddRecord.Enabled = true;
                    btnCopyFrom.Enabled = btnCopyTo.Enabled = true;
                    break;
            }
        }

        public String _collectData()
        {
            try
            {
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
                //Update table header
                DataRow dr = dtHeader.Rows[0];

                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");
                //string ls = Convert.ToDateTime(txtDueDate.Text,ivC).ToString("dd/MM/yyyy");

                dr["DocDate"] = Convert.ToDateTime(txtDueDate.Text, ivC).ToString("yyyyMMdd");//String.Format("{0:yyyyMMdd}", DateTime.Parse(txtPostingDate.Text));
                dr["DocDueDate"] = Convert.ToDateTime(txtDueDate.Text, ivC).ToString("yyyyMMdd");//String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDueDate.Text));
                dr["TaxDate"] = Convert.ToDateTime(txtDocumentDate.Text, ivC).ToString("yyyyMMdd");//String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDocumentDate.Text));

                dr["Comments"] = txtRemarks.Text;
                dr["JrnlMemo"] = txtJournalRemark.Text;
                dr["CardCode"] = txtVendor.Text;
                dr["CardName"] = txtName.Text;
                DocumentXML objInfo = new DocumentXML();
                String RemoveColumn = "No";
                Array arrContentsCols = new string[] { "Quantity", "PriceBefDi", "Price", "DiscPrcnt", "LineTotal" }; // Columns need to reset format numeric
                return objInfo.ToXMLStringFromDS(DocType, dtHeader, GF.ResetFormatNumeric(dtContents, arrContentsCols), RemoveColumn);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Event
        protected void _ddlCurency_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Text");
            this.ddlCurrencyDetail.DataValueField = "ID";
            this.ddlCurrencyDetail.DataTextField = "Text";
            switch (this.ddlCurency.SelectedValue)
            {
                case "3":
                    // get data from ws
                    dt.Rows.Add("1", "$");
                    if (dt.Rows.Count == 1)
                        this.ddlCurrencyDetail.Enabled = false;
                    this.ddlCurrencyDetail.DataSource = dt;
                    this.ddlCurrencyDetail.DataBind();
                    this.ddlCurrencyDetail.Visible = true;
                    break;
                default:
                    this.ddlCurrencyDetail.Visible = false;
                    break;
            }
        }

        protected void _btnAddRecord_Click(object sender, EventArgs e)
        {
            int iNo = GetNo();
            dtContents.Rows.Add(iNo, "", "", "1", "0", "0", "0", "0", "0", "", "", "", "", "", "", "");
            this.lvContents.DataSource = dtContents;

            int lastpage = this.ProductListPagerCombo.TotalRowCount / this.ProductListPagerCombo.PageSize;
            this.ProductListPagerCombo.SetPageProperties(lastpage * this.ProductListPagerCombo.PageSize, this.ProductListPagerCombo.MaximumRows, false);
            this.lvContents.EditIndex = iNo - 1;
            this.lvContents.DataBind();
            SetControlsStatus("Add");
        }

        #region item command
        protected void lvContents_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ListViewItem lvi = e.Item;
            switch (e.CommandName)
            {
                case "CancelAddNew":
                    this._cancelAddNew();
                    this.lvContents.DataBind();
                    break;
                case "CancelUpdate":
                    this.lvContents.EditIndex = -1;
                    this.lvContents.DataSource = dtContents;
                    this.lvContents.DataBind();
                    break;
                case "Update":
                    // update new data to dt
                    if (string.IsNullOrEmpty(((Label)lvi.FindControl("lblCode")).Text)) return;
                   
                    Label lblNo = (Label)lvi.FindControl("lblNo");
                    foreach(DataRow row in dtContents.Rows)
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
                    break;
                    
                default: break;
            }
        }
        #endregion

        #region edit item
        protected void lvContents_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.lvContents.EditIndex = e.NewEditIndex;

            if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
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
        #endregion

        #region insert item
        protected void lvContents_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            this._cancelAddNew();
        }
        #endregion

        protected void imgbCancel_CancelAddNew(object sender, EventArgs e)
        {
            this.lvContents.DataSource = dtContents;
            this._cancelAddNew();
        }

        private void _cancelAddNew()
        {
            this.lvContents.InsertItemPosition = InsertItemPosition.None;
            this.btnAddRecord.Enabled = true;
            this.lvContents.DataBind();
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
            String simulate = System.Configuration.ConfigurationManager.AppSettings["Simulate"];
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType, "", false);
            if ((int)ds.Tables[0].Rows[0]["ErrCode"] != 0)
            {
                Session["errorMessage"] = ds.Tables[0].Rows[0]["ErrMsg"];
                Session["requestXML"] = requestXML;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + ds.Tables[0].Rows[0]["ErrMsg"] + "','');", true);
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
        #endregion

        #region priceCalculation
        protected void updateTableTotalPrice()
        {
            double orderTotalBeforeDiscount = 0.0;
            double orderTotal = 0.0;
            double taxTotal = 0.0;

            foreach(DataRow row in dtContents.Rows)
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
            if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
            double quantity = 0, unitPrice = 0.0, discountContract = 0, priceAfterDiscount = 0.0, total = 0;
            quantity = GF.Object2Double(row["Quantity"], "QtyDec");
            unitPrice = GF.Object2Double(row["PriceBefDi"], "PriceDec");
            discountContract = GF.Object2Double(row["DiscPrcnt"], "PercentDec");

            priceAfterDiscount = GF.Object2Double((Object)(unitPrice * (100 - discountContract) / 100), "PriceDec");
            total = GF.Object2Double((Object)(priceAfterDiscount * quantity), "SumDec");
            row["PriceBefDi"] = GF.FormatNumeric(unitPrice.ToString(), "PriceDec");
            row["DiscPrcnt"] = GF.FormatNumeric(discountContract.ToString(), "PercentDec");
            row["Price"] = GF.FormatNumeric(priceAfterDiscount.ToString(), "PriceDec");
            row["LineTotal"] = GF.FormatNumeric(total.ToString(), "SumDec");
            row["Quantity"] = GF.FormatNumeric(quantity.ToString(), "QtyDec");
        }
        #endregion
    }
}