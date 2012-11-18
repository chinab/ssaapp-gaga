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
    public partial class ARInvoice_VHF : System.Web.UI.Page
    {
        public static DataTable dt;
        public static DataTable dtHeader;
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
                dtHeader.Rows.Add("", "", "", "", "", "From SAP WEB", "Goods Receipts JE Remark",User.Identity.Name);

                dt = new DataTable();
                dt.Columns.Add("No");
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("CardCode");//will be deleted
                dt.Columns.Add("Dscription");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("U_OrgPrice");
                dt.Columns.Add("U_Pro_ByPro");
                dt.Columns.Add("PriceBefDi");
                dt.Columns.Add("DiscPrcnt");
                dt.Columns.Add("Price");
                dt.Columns.Add("TotalDiscount");//will be deleted
                dt.Columns.Add("LineTotal");
                dt.Columns.Add("TaxCode");
                dt.Columns.Add("VatPrcnt");                
                dt.Columns.Add("WhsCode");
                dt.Columns.Add("U_ProID");
                dt.Columns.Add("U_ProLine");
                dt.Columns.Add("U_Pro_SoLe");
                dt.Columns.Add("PromoEnable"); //will be deleted
                dt.Columns.Add("QuantityEnable");//will be deleted
                
                for (int i = 0; i < 2; i++)
                    dt.Rows.Add(i, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","");
                
                this.lvContents.DataSource = dt;
                this.lvContents.DataBind();
               

                MasterData masterDataWS = new MasterData();
                DataSet salesBuyers = masterDataWS.GetSalesBuyerMasterData(User.Identity.Name);
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

                    DataSet contactPersons = masterDataWS.GetContactPerson(defaultVendor.Tables[0].Rows[0]["CardCode"].ToString(), User.Identity.Name);
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
                            dr["ItemCode"] = chosenItem.ItemCode;
                            dr["Dscription"] = chosenItem.ItemName;
                            dr["CardCode"] = this.txtVendor.Text;
                            dr["Quantity"] = 1;

                            GetDefault defaultWS = new GetDefault();                            
                            DateTime postingDate = DateTime.Parse(this.txtPostingDate.Text);
                            DataSet defaultInfo = defaultWS.GetDefaultLineInfo(User.Identity.Name, this.txtVendor.Text, chosenItem.ItemCode, 1, postingDate);

                            dr["PriceBefDi"] = defaultInfo.Tables[0].Rows[0]["UnitPrice"];
                            dr["U_OrgPrice"] = defaultInfo.Tables[0].Rows[0]["UnitPrice"];
                            dr["DiscPrcnt"] = defaultInfo.Tables[0].Rows[0]["Discount"];
                            dr["Price"] = defaultInfo.Tables[0].Rows[0]["PriceAfDi"];
                            dr["TaxCode"] = defaultInfo.Tables[0].Rows[0]["TaxCode"];
                            dr["VatPrcnt"] = defaultInfo.Tables[0].Rows[0]["TaxRate"];
                            dr["WhsCode"] = defaultInfo.Tables[0].Rows[0]["WhsCode"];


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
                            dr["WhsCode"] = chosenWarehouse.WhsCode;

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
                            dr["VatPrcnt"] = chosenTaxCode.Rate;

                            //dt.Rows.
                            this.lvContents.DataSource = dt;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditBusinessPartnerCallBack":
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
                    case "EditPromoCallBack":
                        Promotion promo = Session["chosenPromo"] as Promotion;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (promo != null)
                        {
                            // update grid
                            DataRow dr = dt.Rows[itemNo];
                            
                            double promoDiscount  =  getDoubleFromObject(promo.HeadDscAmt) +  getDoubleFromObject(promo.HeadDscPer) * getDoubleFromObject(dr["U_OrgPrice"]) / 100 +  getDoubleFromObject(promo.ProValue);
                            double unitPrice = getDoubleFromObject(dr["PriceBefDi"]);
                            dr["U_Pro_ByPro"] = promoDiscount;
                            dr["PriceBefDi"] = unitPrice;
                            dr["U_ProId"] = promo.ProCode; 
                            dr["U_Pro_Sole"] = promo.Sole;

                            Int32 ProQty = geIntFromObject(promo.ProQty);
                            if(ProQty >= 1){
                                DataRow newRow = dt.NewRow();
                                setDefaultItemValue(newRow);
                                newRow["ItemCode"] = promo.ItemCode;
                                newRow["Dscription"] = promo.ItemName;
                                newRow["Quantity"] = promo.ProQty;
                                newRow["U_Pro_ByPro"] = 0;
                                newRow["PriceBefDi"] = 0;
                                newRow["U_OrgPrice"] = 0;
                                newRow["DiscPrcnt"] = 0;
                                newRow["U_ProID"] = promo.ProCode;
                                newRow["U_Pro_SoLe"] = promo.Sole;
                                newRow["WhsCode"] = "02";
                                newRow["TaxCode"] = "S2";
                                newRow["U_ProLine"] = "Y";
                                

                                dt.Rows.InsertAt(newRow, itemNo + 1);
                                dt.Rows.RemoveAt(dt.Rows.Count - 1); 
                                dt.Rows[itemNo + 1]["PromoEnable"] = "N";                                
								dt.Rows[itemNo + 1]["QuantityEnable"] = "N";
                            }
                            dt.Rows[itemNo]["PromoEnable"] = "N";
							dt.Rows[itemNo]["QuantityEnable"] = "N";
                            dr["U_Pro_Sole"] = promo.Sole;                            
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
            row["ItemCode"]="";
            row["Dscription"]="";
            row["Quantity"]="";
            row["U_OrgPrice"]="";
            row["U_Pro_ByPro"] = "";
            row["PriceBefDi"] = "";
            row["DiscPrcnt"] = "";
            row["Price"]="";
            row["LineTotal"]="0.0";
            row["TaxCode"]="";
            row["VatPrcnt"] = "";                
            row["WhsCode"]="";
            row["U_ProID"] = "";
            row["U_ProLine"] = "";
            row["U_Pro_Sole"] = "";
        }
 
        public String _collectData()
        {
            try
            {
                //Update table header
                DataRow dr = dtHeader.Rows[0];
                dr["DocDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtPostingDate.Text));
                dr["DocDueDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDeliveryDate.Text));
                dr["TaxDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDocumentDate.Text));
                dr["Comments"] = txtRemark.Text;
                dr["JrnlMemo"] = "";
                dr["CardCode"] = txtVendor.Text;
                dr["CardName"] = txtName.Text;
                //DocumentXML objInfo = new DocumentXML();
                //String RemoveColumn = "No;CardCode;PromoEnable;QuantityEnable;TotalDiscount";
                //return objInfo.ToXMLStringFromDS("13", dtHeader, dt, RemoveColumn);

                DocumentXML objInfo = new DocumentXML();
                DataSet ds = new DataSet("DS");
                ds.Tables.Add(dtHeader);
                ds.Tables.Add(dt);

                return objInfo.ToXMLStringFromDS("13", ds);
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
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, "13", "", false);
            if ((int)ds.Tables[0].Rows[0]["ErrCode"] != 0)
            {
                Session["errorMessage"] = ds.Tables[0].Rows[0]["ErrMsg"];
                Session["requestXML"] = requestXML;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + ds.Tables[0].Rows[0]["ErrMsg"] + "','');", true);
            }
            else
            {
                dt.Clear();
                for (int i = 0; i < 3; i++)
                    dt.Rows.Add(i, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

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
                if (!"".Equals(dtInput.Rows[i]["ItemCode"]))
                {
                    updateRowTotalPrice(dtInput, i);
                    double total = getDoubleFromObject(dtInput.Rows[i]["LineTotal"]);
                    double taxRate = getDoubleFromObject(dtInput.Rows[i]["VatPrcnt"]);
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
            double totalDiscount = 0;

            DataRow row = dtInput.Rows[rowNumber];
            quantity = geIntFromObject(row["Quantity"]);
            orgPrice = getDoubleFromObject(row["U_OrgPrice"]);

            discountPromo = getDoubleFromObject(row["U_Pro_ByPro"]);
            unitPrice = getDoubleFromObject(row["PriceBefDi"]);
            discountContract = getDoubleFromObject(row["DiscPrcnt"]);
            priceAfterDiscount = getDoubleFromObject(row["Price"]);
            total = getDoubleFromObject(row["LineTotal"]);

            unitPrice = orgPrice - discountPromo/quantity;
            priceAfterDiscount = unitPrice * (100 - discountContract) / 100;
            total = priceAfterDiscount * quantity;
            totalDiscount = orgPrice * quantity * discountContract / 100 + discountPromo;

            row["PriceBefDi"] = unitPrice;
            row["Price"] = priceAfterDiscount;
            row["TotalDiscount"] = totalDiscount;

            row["LineTotal"] = orgPrice * quantity - totalDiscount;// total;

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