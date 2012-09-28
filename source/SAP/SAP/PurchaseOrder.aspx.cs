using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;
using System.Collections;

namespace SAP
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        public static DataTable dtContents;
        private static int idxItem = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtContents = new DataTable();
                dtContents.Columns.Add("No");
                dtContents.Columns.Add("Code");
                dtContents.Columns.Add("CardCode");
                dtContents.Columns.Add("Description");
                dtContents.Columns.Add("Quantity");
                dtContents.Columns.Add("UnitPrice");
                dtContents.Columns.Add("ContractDiscount");
                dtContents.Columns.Add("PriceAfterDiscount");
                dtContents.Columns.Add("Total");
                dtContents.Columns.Add("TaxCode");
                dtContents.Columns.Add("TaxRate");
                dtContents.Columns.Add("Whse");
                dtContents.Columns.Add("QuantityEnable");
                dtContents.Columns.Add("PromoEnable");

                dtContents.Columns.Add("ProfitCode");
                dtContents.Columns.Add("CC1");
                dtContents.Columns.Add("CC2");
                dtContents.Columns.Add("CC3");
                dtContents.Columns.Add("CC4");

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
                //-------------Load Shipping Type----------------
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
                    this.txtDeliveryDate.Text = DateTime.Now.ToShortDateString();
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
                            DataRow dr = dtContents.Rows[itemNo];
                            setDefaultItemValue(dr);
                            dr["No"] = itemNo;
                            dr["Code"] = chosenItem.ItemCode;
                            dr["Description"] = chosenItem.ItemName;
                            dr["CardCode"] = this.txtVendor.Text;
                            dr["Quantity"] = 1;

                            GetDefault defaultWS = new GetDefault();
                            DateTime postingDate = DateTime.Parse(this.txtPostingDate.Text);
                            DataSet defaultInfo = defaultWS.GetDefaultLineInfo(User.Identity.Name, this.txtVendor.Text, chosenItem.ItemCode, 1, postingDate);

                            dr["UnitPrice"] = String.Format("{0:n0}", defaultInfo.Tables[0].Rows[0]["UnitPrice"]);
                            dr["ContractDiscount"] = String.Format("{0:n2}", defaultInfo.Tables[0].Rows[0]["Discount"]);
                            dr["PriceAfterDiscount"] = String.Format("{0:n0}", defaultInfo.Tables[0].Rows[0]["PriceAfDi"]);
                            dr["TaxCode"] = defaultInfo.Tables[0].Rows[0]["TaxCode"];
                            dr["TaxRate"] = defaultInfo.Tables[0].Rows[0]["TaxRate"];
                            dr["Whse"] = defaultInfo.Tables[0].Rows[0]["WhsCode"];

                            //dt.Rows.      
                            updateTableTotalPrice(dtContents);
                            this.lvContents.DataSource = dtContents;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditWareHouseCallBack":
                        WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenWarehouse != null)
                        {
                            // update grid
                            DataRow dr = dtContents.Rows[itemNo];
                            dr["Whse"] = chosenWarehouse.WhsCode;

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
                            dr["Taxcode"] = chosenTaxCode.Code;
                            dr["TaxRate"] = chosenTaxCode.Rate;

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
                            this.txtDeliveryDate.Text = DateTime.Now.ToShortDateString();
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

        protected void setDefaultItemValue(DataRow row)
        {
            row["No"] = dtContents.Rows.Count;
            row["Code"] = "";
            row["Description"] = "";
            row["Quantity"] = "";
            row["UnitPrice"] = "";
            row["ContractDiscount"] = "";
            row["PriceAfterDiscount"] = "";
            row["Total"] = "0.0";
            row["TaxCode"] = "";
            row["TaxRate"] = "";
            row["Whse"] = "";
        }

        public String _collectData()
        {
            try
            {
                DocumentXML objInfo = new DocumentXML("22", this.txtPostingDate.Text, this.txtDeliveryDate.Text, this.txtDocumentDate.Text, this.txtVendor.Text, txtName.Text, User.Identity.Name);

                for (int i = 0; i < dtContents.Rows.Count; i++)
                {
                    DataRow row = dtContents.Rows[i];
                    String itemcode = row["Code"].ToString();

                    if (!String.IsNullOrEmpty(itemcode))
                    {
                        String des = row["Description"].ToString();
                        String quan = row["Quantity"].ToString();
                        String discount = row["ContractDiscount"].ToString();
                        String whscode = row["Whse"].ToString();
                        String vat = row["TaxCode"].ToString();
                        String UnitPrice = row["UnitPrice"].ToString();

                        Document_LineXML objOrder = new Document_LineXML(itemcode, des, geIntFromObject(quan), getDoubleFromObject(discount), whscode, vat, getDoubleFromObject(UnitPrice));
                        objInfo.AddOrderItem(objOrder);
                    }
                }
                return objInfo.ToXMLString();
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
            if (ds.Tables[0].Rows[0]["ErrCode"] != "0")
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
                    updateTableTotalPrice(dtContents);
                    this.lvContents.DataSource = dtContents;
                    this.lvContents.DataBind();

                    break;
                }
            }
        }

        protected void _btnAddRecord_Click(object sender, EventArgs e)
        {
            this.lvContents.InsertItemPosition = InsertItemPosition.FirstItem;
            this.btnAddRecord.Enabled = false;
            this.lvContents.EditIndex = -1;

            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();

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
                            row["Code"] = ((Label)lvi.FindControl("lblCode")).Text;
                            row["Description"] = ((Label)lvi.FindControl("lblDescription")).Text;
                            row["Quantity"] = ((TextBox)lvi.FindControl("txtQuantity")).Text;
                            row["UnitPrice"] = ((Label)lvi.FindControl("lblUnitPrice")).Text;
                            row["ContractDiscount"] = ((Label)lvi.FindControl("lblDiscount")).Text;
                            row["PriceAfterDiscount"] = ((Label)lvi.FindControl("lblPriceAfterDiscount")).Text;
                            row["Total"] = ((Label)lvi.FindControl("lblTotal")).Text;
                            row["TaxCode"] = ((Label)lvi.FindControl("lblTaxcode")).Text;
                            row["Whse"] = ((Label)lvi.FindControl("lblWhse")).Text;
                            row["TaxRate"] = ((Label)lvi.FindControl("lblTaxRate")).Text;
                            row["ProfitCode"] = ((Label)lvi.FindControl("lblProfitCode")).Text;
                            row["CC1"] = ((Label)lvi.FindControl("lblCC1")).Text;
                            row["CC2"] = ((Label)lvi.FindControl("lblCC2")).Text;
                            row["CC3"] = ((Label)lvi.FindControl("lblCC3")).Text;
                            row["CC4"] = ((Label)lvi.FindControl("lblCC4")).Text;
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
                    dtContents.Rows.RemoveAt(i_idx);// code for dummy
                    this.lvContents.EditIndex = -1;
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
            ListViewItem lvi = e.Item;
            //string lblNo = ((Label)lvi.FindControl("lblNo")).Text;
            string lblCode = ((Label)lvi.FindControl("lblCode")).Text;
            string lblDesc = ((Label)lvi.FindControl("lblDescription")).Text;
            string txtQty = ((TextBox)lvi.FindControl("txtQuantity")).Text;
            string lblUPrice = ((TextBox)lvi.FindControl("txtUnitPrice")).Text;
            string lblDisc = ((TextBox)lvi.FindControl("txtDiscount")).Text;
            string lblPriceAftDisc = ((TextBox)lvi.FindControl("txtPriceAfterDiscount")).Text;
            string lblTotal = ((TextBox)lvi.FindControl("txtTotal")).Text;
            string lblTaxcode = ((Label)lvi.FindControl("lblTaxcode")).Text;
            string lblWhse = ((Label)lvi.FindControl("lblWhse")).Text;
            string lblTaxRate = ((Label)lvi.FindControl("lblTaxRate")).Text;
            string lblProfitCode = ((Label)lvi.FindControl("lblProfitCode")).Text;
            string lblCC1 = ((Label)lvi.FindControl("lblCC1")).Text;
            string lblCC2 = ((Label)lvi.FindControl("lblCC2")).Text;
            string lblCC3 = ((Label)lvi.FindControl("lblCC3")).Text;
            string lblCC4 = ((Label)lvi.FindControl("lblCC4")).Text;

            int i_rc = dtContents.Rows.Count;

            dtContents.Rows.Add(i_rc, lblCode, "", lblDesc, txtQty, lblUPrice, lblDisc, lblPriceAftDisc, lblTotal, lblTaxcode, lblTaxRate, lblWhse, "", "", lblProfitCode, lblCC1, lblCC2, lblCC3, lblCC4);
            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();
            this._cancelAddNew();
          
            //DataRow newRow = dtContents.NewRow();
            //setDefaultItemValue(newRow);
            //dtContents.Rows.Add(newRow);
            //this.lvContents.DataSource = dtContents;
            //this.lvContents.DataBind();
            //this._cancelAddNew();
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
        protected void updateTableTotalPrice(DataTable dtInput)
        {
            double orderTotalBeforeDiscount = 0.0;
            double orderTotal = 0.0;
            double taxTotal = 0.0;
            for (int i = 0; i < dtInput.Rows.Count; i++)
            {
                if (!"".Equals(dtInput.Rows[i]["Code"]))
                {
                    updateRowTotalPrice(dtInput, i);
                    double total = getDoubleFromObject(dtInput.Rows[i]["Total"]);
                    double taxRate = getDoubleFromObject(dtInput.Rows[i]["TaxRate"]);
                    if (taxRate == 0)
                        taxRate = 10;
                    double tax = total * taxRate / 100;

                    orderTotalBeforeDiscount += total;
                    taxTotal += tax;
                }
                dtInput.Rows[i]["No"] = i;
            }
            this.txtTotalDiscount.Text = orderTotalBeforeDiscount.ToString();
            this.txtTax.Text = taxTotal.ToString();
            orderTotal = orderTotalBeforeDiscount + taxTotal;
            this.txtTotalPayment.Text = orderTotal.ToString();
        }

        public void updateRowTotalPrice(DataTable dtInput, int rowNumber)
        {
            int quantity = 0;
            double unitPrice = 0.0;
            double discountContract = 0.0;
            double priceAfterDiscount = 0.0;
            double total = 0;
            //double totalBeforeDiscount = 0.0;

            DataRow row = dtInput.Rows[rowNumber];
            quantity = geIntFromObject(row["Quantity"]);
            unitPrice = getDoubleFromObject(row["UnitPrice"]);
            discountContract = getDoubleFromObject(row["ContractDiscount"]);
            priceAfterDiscount = getDoubleFromObject(row["PriceAfterDiscount"]);
            total = getDoubleFromObject(row["Total"]);

            //unitPrice = orgPrice - discountPromo;
            priceAfterDiscount = unitPrice * (100 - discountContract) / 100;
            total = priceAfterDiscount * quantity;

            row["UnitPrice"] = unitPrice;
            row["PriceAfterDiscount"] = priceAfterDiscount;
            row["Total"] = total;

        }

        public double getDoubleFromObject(Object input)
        {
            double result = 0.0;
            try
            {
                if (input != null)
                    result = Double.Parse(input.ToString());
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

        protected void lvContents_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            //ListViewItem lvi = e.Item;
            
        }

        protected void lvContents_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            this.lvContents.EditIndex = -1;
            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();
        }

        protected void lvContents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }
    }
}