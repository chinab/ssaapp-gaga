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
        public GeneralFunctions GF = new GeneralFunctions();

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
                dtHeader.Rows.Add("", "", "", "", "", "From SAP WEB", "Goods Receipts JE Remark");


                dtContents = new DataTable();
                dtContents.Columns.Add("No");
                dtContents.Columns.Add("ItemCode");
                //dtContents.Columns.Add("CardCode");//no need
                dtContents.Columns.Add("Dscription");
                dtContents.Columns.Add("Quantity");
                dtContents.Columns.Add("PriceBefDi");
                dtContents.Columns.Add("DiscPrcnt"); 
                dtContents.Columns.Add("Price");
                dtContents.Columns.Add("LineTotal");
                dtContents.Columns.Add("TaxCode");
                dtContents.Columns.Add("VatPrcnt");
                dtContents.Columns.Add("WhsCode");
                //dtContents.Columns.Add("QuantityEnable");//no need
                //dtContents.Columns.Add("PromoEnable");//no need
                dtContents.Columns.Add("OcrCode");
                dtContents.Columns.Add("OcrCode2");
                dtContents.Columns.Add("OcrCode3");
                dtContents.Columns.Add("OcrCode4");
                dtContents.Columns.Add("OcrCode5");

                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();
                

                MasterData masterDataWS = new MasterData();
                //-------------Load Sales/Buyer Employee----------------
                DataSet salesBuyers = masterDataWS.GetSalesBuyerMasterData();
                ListItem item = new ListItem();
                foreach (DataRow row in salesBuyers.Tables[0].Rows)
                {
                    item = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlBuyer.Items.Add(item);
                }
                //-------------Load Shipping Type---------------------
                DataSet ShippingType = masterDataWS.GetShippingType();
                ListItem itemShipping = new ListItem();
                foreach (DataRow row in ShippingType.Tables[0].Rows)
                {
                    itemShipping = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlShippingType.Items.Add(itemShipping);
                }
                //-------------Load Payment Tern----------------
                DataSet PaymentTerm = masterDataWS.GetPaymentTerm();
                ListItem itemPaymenTerm = new ListItem();
                foreach (DataRow row in PaymentTerm.Tables[0].Rows)
                {
                    itemPaymenTerm = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlPaymentTerm.Items.Add(itemPaymenTerm);
                }
                //-------------Load Indicator----------------
                DataSet Indicator = masterDataWS.GetIndicator();
                ListItem itemIndicator = new ListItem();
                foreach (DataRow row in Indicator.Tables[0].Rows)
                {
                    itemIndicator = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlIndicator.Items.Add(itemIndicator);
                }
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

                    DataSet contactPersons = masterDataWS.GetContactPerson(defaultVendor.Tables[0].Rows[0]["CardCode"].ToString());
                    item = new ListItem();
                    foreach (DataRow row in contactPersons.Tables[0].Rows)
                    {
                        String name = row[1].ToString() + " " + row[2].ToString();
                        item = new ListItem(name, row[1].ToString());
                        if ("Y".Equals(row[0].ToString()))
                            item.Selected = true;
                        ddlContactPerson.Items.Add(item);
                    }
                }
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
                GeneralFunctions Newformat = new GeneralFunctions();
                
                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                        
                    case "EditItemCallBack":
                        ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenItem != null)
                        {
                            // update grid
                            DataRow dr = dtContents.Rows[itemNo-1];
                            //setDefaultItemValue(dr);
                            //dr["No"] = itemNo;
                            dr["ItemCode"] = chosenItem.ItemCode;
                            dr["Dscription"] = chosenItem.ItemName;
                           // dr["CardCode"] = this.txtVendor.Text;
                            dr["Quantity"] = 1;

                            GetDefault defaultWS = new GetDefault();
                            DateTime postingDate = DateTime.Parse(this.txtPostingDate.Text);
                            DataSet defaultInfo = defaultWS.GetDefaultLineInfo(User.Identity.Name, this.txtVendor.Text, chosenItem.ItemCode, 1, postingDate);

                            dr["PriceBefDi"] = String.Format("{0:n0}", defaultInfo.Tables[0].Rows[0]["UnitPrice"]);
                            dr["DiscPrcnt"] = String.Format("{0:n2}", defaultInfo.Tables[0].Rows[0]["Discount"]);
                            dr["Price"] = String.Format("{0:n0}", defaultInfo.Tables[0].Rows[0]["PriceAfDi"]);
                            dr["TaxCode"] = defaultInfo.Tables[0].Rows[0]["TaxCode"];
                            dr["VatPrcnt"] = defaultInfo.Tables[0].Rows[0]["TaxRate"];
                            dr["WhsCode"] = defaultInfo.Tables[0].Rows[0]["WhsCode"];
                            //dt.Rows.      
                            updateTableTotalPrice();
                            this.lvContents.DataSource = dtContents;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditWareHouseCallBack":
                        WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenWarehouse != null)
                        {   // update grid
                            DataRow dr = dtContents.Rows[itemNo];
                            dr["WhsCode"] = chosenWarehouse.WhsCode;
                            //dt.Rows.
                            this.lvContents.DataSource = dtContents;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditTaxCodeCallBack":
                        TaxGroup chosenTaxCode = Session["chosenTaxCode"] as TaxGroup;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenTaxCode != null)
                        {
                            // update grid
                            DataRow dr = dtContents.Rows[itemNo];
                            dr["TaxCode"] = chosenTaxCode.Code;
                            dr["VatPrcnt"] = chosenTaxCode.Rate;

                            //dt.Rows.
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
                            DataSet contactPersons = masterDataWS.GetContactPerson(chosenPartner.CardCode);
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

        private void ResetLineNo()
        {
            int i = 0;
            foreach (DataRow row in dtContents.Rows)
            {
                row["No"] = ++i;
            }
        }

        public String _collectData()
        {
            try
            {
                //Update table header
                DataRow dr = dtHeader.Rows[0];
                dr["DocDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtPostingDate.Text));
                dr["DocDueDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDueDate.Text));
                dr["TaxDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDocumentDate.Text));
                dr["Comments"] = txtRemarks.Text;
                dr["JrnlMemo"] = txtJournalRemark.Text;
                dr["CardCode"] = txtVendor.Text;
                dr["CardName"] = txtName.Text;
                DocumentXML objInfo = new DocumentXML();
                String RemoveColumn = "No";
                return objInfo.ToXMLStringFromDS("22", dtHeader, dtContents, RemoveColumn);

                //foreach (DataRow row in dtContents.Rows)
                //{
                   
                //    String itemcode = row["Code"].ToString();

                //    if (!String.IsNullOrEmpty(itemcode))
                //    {
                //        String des = row["Description"].ToString();
                //        String whscode = row["Whse"].ToString();
                //        String vat = row["TaxCode"].ToString();

                //        Document_LineXML objOrder = new Document_LineXML(itemcode, des, GF.Object2Double((object)GF.ResetFormatNumeric(row["Quantity"].ToString())),
                //                                                                        GF.Object2Double((object)GF.ResetFormatNumeric(row["ContractDiscount"].ToString())), 
                //                                                                        whscode, vat,
                //                                                                        GF.Object2Double((object)GF.ResetFormatNumeric(row["UnitPrice"].ToString())), "");
                //        objInfo.AddOrderItem(objOrder);
                //    }
                //}
                //return objInfo.ToXMLString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected DataTable getDataFromListView(ListView lv)
        {
            DataTable table = new DataTable();
            if (lv.Items.Count >= 1)
            {
                for (int i = 0; i < lv.Items[0].Controls.Count; i++)
                {
                    if (lv.Items[0].Controls[i].GetType() == typeof(Label))
                    {
                        table.Columns.Add("col" + i.ToString());
                    }
                }
            }

            for (int j = 0; j < lv.Items.Count; j++)
            {
                ArrayList valholder = new ArrayList();
                for (int k = 0; k < lv.Items[0].Controls.Count; k++)
                {
                    if (lv.Items[0].Controls[k].GetType() == typeof(Label))
                    {
                        Label tep = lv.Items[0].Controls[k] as Label;
                        valholder.Add(tep.Text);
                    }
                }
                DataRow dr = table.NewRow();
                for (int z = 0; z < valholder.Count; z++)
                {
                    dr[z] = valholder[z].ToString();
                }
                table.Rows.Add(dr);
            }
            return table;
        }

        #region Event
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            String simulate = System.Configuration.ConfigurationManager.AppSettings["Simulate"];
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML);
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
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                              "Dialog.hideLoader();", true);
        }

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

        protected void btnQuantityUpdate_click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.lvContents.Items)
            {
                LinkButton btnQuantityUpdate = item.FindControl("btnQuantityUpdate") as LinkButton;

                if (btnQuantityUpdate != null && btnQuantityUpdate == sender)
                {
                    TextBox txtQuantity = item.FindControl("txtQuantity") as TextBox;
                    Label lblOrgPrice = item.FindControl("lblOrgPrice") as Label;
                    dtContents.Rows[item.DataItemIndex]["Quantity"] = geIntFromObject(txtQuantity.Text);
                    updateTableTotalPrice();
                    this.lvContents.DataSource = dtContents;
                    this.lvContents.DataBind();

                    break;
                }
            }
        }

        protected void _btnAddRecord_Click(object sender, EventArgs e)
        {
            int iNo = GetNo();
            dtContents.Rows.Add(iNo, "", "", "", "1", "0", "0", "0", "0", "", "", "", "", "", "", "");
            this.lvContents.DataSource = dtContents;
            this.lvContents.EditIndex = iNo-1;
            this.lvContents.DataBind();
        }

        private int GetNo()
        {
            return dtContents.Rows.Count + 1;
        }

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
                case "UpdateItem":
                    // update new data to dt
                    Label lblNo = (Label)lvi.FindControl("lblNo");
                    foreach(DataRow row in dtContents.Rows)
                    {
                        if (row["No"].ToString().Equals(lblNo.Text))
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
                    
                default:
                    break;
            }
        }

        protected void lvContents_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.lvContents.EditIndex = e.NewEditIndex;

            string lsQty = dtContents.Rows[e.NewEditIndex]["Quantity"].ToString();
            string lsUPr = dtContents.Rows[e.NewEditIndex]["PriceBefDi"].ToString();
            string lsPriAftDis = dtContents.Rows[e.NewEditIndex]["Price"].ToString();
            string lsTotal = dtContents.Rows[e.NewEditIndex]["LineTotal"].ToString();

            dtContents.Rows[e.NewEditIndex]["Quantity"] = lsQty.Replace(",", "");
            dtContents.Rows[e.NewEditIndex]["PriceBefDi"] = lsUPr.Replace(",", "");
            dtContents.Rows[e.NewEditIndex]["Price"] = lsPriAftDis.Replace(",", "");
            dtContents.Rows[e.NewEditIndex]["LineTotal"] = lsTotal.Replace(",", "");

            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();
        }

        #region edit item

        #endregion

        #region insert item
        protected void lvContents_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            this._cancelAddNew();
        }


        protected void lvContents_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            
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
            double quantity = 0;
            double unitPrice = 0.0;
            double discountContract = 0;
            double priceAfterDiscount = 0.0;
            double total = 0;
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

        public double getDoubleFromObject(Object input)
        {
            CultureInfo cf = System.Threading.Thread.CurrentThread.CurrentUICulture;
            cf.NumberFormat.NumberGroupSeparator = ",";
            cf.NumberFormat.NumberDecimalSeparator = ".";

            double result = 0.0;
            try
            {
                if (input != null)
                    result = Double.Parse(input.ToString(), cf);
            }
            catch (Exception ex)
            {
                result = 0.0;
            }
            return result;
        }

        public Int32 geIntFromObject(Object input)
        {
            Int32 result = 0;
            try
            {
                if (input != null)
                    result = Int32.Parse(input.ToString());
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }


        #endregion

        protected void lvContents_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            this.lvContents.EditIndex = -1;
            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();
        }

        protected void lvContents_ItemCreated(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvContents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            
        }

        protected void ProductListPagerCombo_PreRender(object sender, EventArgs e)
        {
            lvContents.DataSource = dtContents;
            lvContents.DataBind();
        }

     

    }
}