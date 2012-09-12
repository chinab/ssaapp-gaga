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
    public partial class ARInvoice : System.Web.UI.Page
    {
        public static DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = new DataTable();
                dt.Columns.Add("No");
                dt.Columns.Add("Code");
                dt.Columns.Add("CardCode");
                dt.Columns.Add("Description");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("OrgPrice");
                dt.Columns.Add("PromoDiscount");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("ContractDiscount");
                dt.Columns.Add("PriceAfterDiscount");
                dt.Columns.Add("Total");
                dt.Columns.Add("TaxCode");
                dt.Columns.Add("TaxRate");                
                dt.Columns.Add("Whse");
                dt.Columns.Add("PromotionId");
                dt.Columns.Add("PromotionLine");
                dt.Columns.Add("Sole");
                dt.Columns.Add("PromoEnable");
                dt.Columns.Add("QuantityEnable");
                
                for (int i = 0; i < 5; i++)
                    dt.Rows.Add(i, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                
                this.lvContents.DataSource = dt;
                this.lvContents.DataBind();
               

                MasterData masterDataWS = new MasterData();
                DataSet salesBuyers = masterDataWS.GetSalesBuyerMasterData();
                ListItem item = new ListItem();           
                foreach (DataRow row in salesBuyers.Tables[0].Rows)
                {
                    item = new ListItem(row[1].ToString(), row[0].ToString());
                    ddlBuyer.Items.Add(item);
                }

                GetDefault getDefaultWS = new GetDefault();
                DataSet defaultVendor = getDefaultWS.GetDefaultBP(User.Identity.Name, "C");


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
                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditItemCallBack":
                        ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenItem != null)
                        {
                            // update grid
                            DataRow dr = dt.Rows[itemNo];
                            setDefaultItemValue(dr);
                            dr["No"] = itemNo;
                            dr["Code"] = chosenItem.ItemCode;
                            dr["Description"] = chosenItem.ItemName;
                            dr["CardCode"] = this.txtVendor.Text;
                            dr["Quantity"] = 1;

                            GetDefault defaultWS = new GetDefault();                            
                            DateTime postingDate = DateTime.Parse(this.txtPostingDate.Text);
                            DataSet defaultInfo = defaultWS.GetDefaultLineInfo(User.Identity.Name, this.txtVendor.Text, chosenItem.ItemCode, 1, postingDate);

                            dr["UnitPrice"] = defaultInfo.Tables[0].Rows[0]["UnitPrice"];
                            dr["OrgPrice"] = defaultInfo.Tables[0].Rows[0]["UnitPrice"];
                            dr["ContractDiscount"] = defaultInfo.Tables[0].Rows[0]["Discount"];
                            dr["PriceAfterDiscount"] = defaultInfo.Tables[0].Rows[0]["PriceAfDi"];
                            dr["TaxCode"] = defaultInfo.Tables[0].Rows[0]["TaxCode"];
                            dr["TaxRate"] = defaultInfo.Tables[0].Rows[0]["TaxRate"];
                            dr["Whse"] = defaultInfo.Tables[0].Rows[0]["WhsCode"];


                            //dt.Rows.      
                            updateTableTotalPrice(dt);
                            this.lvContents.DataSource = dt;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditWareHouseCallBack":
                        WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenWarehouse != null)
                        {
                            // update grid
                            DataRow dr = dt.Rows[itemNo];
                            dr["Whse"] = chosenWarehouse.WhsCode;

                            //dt.Rows.
                            this.lvContents.DataSource = dt;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditTaxCodeCallBack":
                        TaxGroup chosenTaxCode = Session["chosenTaxCode"] as TaxGroup;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenTaxCode != null)
                        {
                            // update grid
                            DataRow dr = dt.Rows[itemNo];
                            dr["Taxcode"] = chosenTaxCode.Code;
                            dr["TaxRate"] = chosenTaxCode.Rate;

                            //dt.Rows.
                            this.lvContents.DataSource = dt;
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
                    case "EditPromoCallBack":
                        Promotion promo = Session["chosenPromo"] as Promotion;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (promo != null)
                        {
                            // update grid
                            DataRow dr = dt.Rows[itemNo];
                            /*
                            'Logic Apply Promotion:
                            '1. Discount By Promotion = HeadDscAmt + HeadDscPer*UnitPrice/100 + ProValue
                            '2. Unit Price = Unit Price - Discount By Promotion
                            '3. So le = field Sole
                            '4. ProCode = field ProCode
                            '5. Neu ProQty>0: them 1 dong vao grid
                            '    Item Code= Item Code cua dong apply
                            '    Description = Description cua dong apply
                            '    Quantity=ProQty
                            '    Unit Price,Discount,Discount By Promotion  = 0
                            '    Warehouse = WS GetPromotionWarehouse
                            '    U_ProLine = Y
                            '    ProCode = field ProCode
                            '    So le = field Sole
                             * 
                            dt.Columns.Add("No");
                            dt.Columns.Add("Code");
                            dt.Columns.Add("Description");
                            dt.Columns.Add("Quantity");
                            dt.Columns.Add("OrgPrice");
                            dt.Columns.Add("PromoDiscount");
                            dt.Columns.Add("UnitPrice");
                            dt.Columns.Add("ContractDiscount");
                            dt.Columns.Add("PriceAfterDiscount");
                            dt.Columns.Add("Total");
                            dt.Columns.Add("TaxCode");
                            dt.Columns.Add("TaxRate");
                            dt.Columns.Add("Whse");
                            dt.Columns.Add("PromotionId");
                            dt.Columns.Add("PromotionLine");
                            dt.Columns.Add("Sole");
                            */
                            double promoDiscount  =  getDoubleFromObject(promo.HeadDscAmt) +  getDoubleFromObject(promo.HeadDscPer) * getDoubleFromObject(dr["OrgPrice"]) / 100 +  getDoubleFromObject(promo.ProValue);
                            double unitPrice = getDoubleFromObject(dr["UnitPrice"]);
                            dr["PromoDiscount"] = promoDiscount;
                            dr["UnitPrice"] = unitPrice;
                            dr["PromotionId"] = promo.ProCode; 
                            dr["Sole"] = promo.Sole;

                            Int32 ProQty = geIntFromObject(promo.ProQty);
                            if(ProQty >= 1){
                                DataRow newRow = dt.NewRow();
                                setDefaultItemValue(newRow);
                                newRow["Code"] = promo.ItemCode;
                                newRow["Description"] = promo.ItemName;
                                newRow["Quantity"] = promo.ProQty;
                                newRow["PromoDiscount"] = 0;
                                newRow["UnitPrice"] = 0;
                                newRow["ContractDiscount"] = 0;
                                newRow["PromotionId"] = promo.ProCode; 
                                newRow["Sole"] = promo.Sole;
                                newRow["Whse"] = promo.WhsCode;
                                newRow["PromotionLine"] = "Y";
                                

                                dt.Rows.InsertAt(newRow, itemNo + 1);
                                dt.Rows.RemoveAt(dt.Rows.Count - 1); 
                                dt.Rows[itemNo + 1]["PromoEnable"] = "N";                                
								dt.Rows[itemNo + 1]["QuantityEnable"] = "N";
                            }
                            dt.Rows[itemNo]["PromoEnable"] = "N";
							dt.Rows[itemNo]["QuantityEnable"] = "N";
                            dr["Sole"] = promo.Sole;                            
                            //dt.Rows.
                            updateTableTotalPrice(dt);
                            this.lvContents.DataSource = dt;
                            this.lvContents.DataBind();
                            
                        }
                        break;
                        
                        
                    default:
                        break;
                }
            }
            
        }

        protected void setDefaultItemValue(DataRow row){
            row["No"]="";
            row["Code"]="";
            row["Description"]="";
            row["Quantity"]="";
            row["OrgPrice"]="";
            row["PromoDiscount"]="";
            row["UnitPrice"]="";
            row["ContractDiscount"]="";
            row["PriceAfterDiscount"]="";
            row["Total"]="0.0";
            row["TaxCode"]="";
            row["TaxRate"]="";                
            row["Whse"]="";
            row["PromotionId"]="";
            row["PromotionLine"]="";
            row["Sole"]="";
        }
 
        public String _collectData()
        {
            try
            {
                DocumentXML objInfo = new DocumentXML("13", this.txtPostingDate.Text, this.txtDeliveryDate.Text, this.txtDocumentDate.Text, this.txtVendor.Text, txtName.Text, User.Identity.Name);
                
                 for (int i = 0; i < dt.Rows.Count; i++) {

                    DataRow row = dt.Rows[i];
                    String itemcode = row["Code"].ToString();

                    
                    if (!String.IsNullOrEmpty(itemcode))
                    {
                        String des = row["Description"].ToString();
                        String quan = row["Quantity"].ToString();
                        String discount = row["ContractDiscount"].ToString();
                        String whscode = row["Whse"].ToString();
                        String vat = row["TaxCode"].ToString();
                        //String vatprice = row["Total"].ToString();
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
            String results = ts.CreateMarketingDocument(requestXML);
            if (results.IndexOf("Exception") >= 0 && !simulate.Equals("true"))
            {
                Session["errorMessage"] = results;
                Session["requestXML"] = requestXML;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + results + "','');", true);
            }
            else
            {
                Session["successMessage"] = results;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                   "Main.setMasterMessage('" + results + "','');", true);
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
                    dt.Rows[item.DataItemIndex]["Quantity"] = geIntFromObject(txtQuantity.Text);
                    updateTableTotalPrice(dt);
                    this.lvContents.DataSource = dt;
                    this.lvContents.DataBind();
  
                    break;
                }
            }
        }
        #endregion
        
        #region priceCalculation
        protected void updateTableTotalPrice(DataTable dtInput){
            double orderTotalBeforeDiscount = 0.0;
            double orderTotal = 0.0;
            double taxTotal = 0.0;
            for (int i = 0; i < dtInput.Rows.Count; i++) {
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

        public void updateRowTotalPrice(DataTable dtInput, int rowNumber) {
            int quantity = 0;
            double orgPrice = 0.0;
            double discountPromo = 0.0;
            double unitPrice = 0.0;
            double discountContract = 0.0;
            double priceAfterDiscount = 0.0;
            double total = 0;
            //double totalBeforeDiscount = 0.0;

            DataRow row = dtInput.Rows[rowNumber];
            quantity = geIntFromObject(row["Quantity"]);
            orgPrice = getDoubleFromObject(row["OrgPrice"]);
            discountPromo = getDoubleFromObject(row["PromoDiscount"]);
            unitPrice = getDoubleFromObject(row["UnitPrice"]);
            discountContract = getDoubleFromObject(row["ContractDiscount"]);
            priceAfterDiscount = getDoubleFromObject(row["PriceAfterDiscount"]);
            total = getDoubleFromObject(row["Total"]);

            unitPrice = orgPrice - discountPromo;
            priceAfterDiscount = unitPrice * (100 - discountContract) / 100;
            total = priceAfterDiscount * quantity;

            row["UnitPrice"] = unitPrice;
            row["PriceAfterDiscount"] = priceAfterDiscount;
            row["Total"] = total;

        }

        public double getDoubleFromObject(Object input){
            double result = 0.0;
            try{
                if (input != null )
                    result= Double.Parse(input.ToString());
            } catch (Exception ex){
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
    }
}