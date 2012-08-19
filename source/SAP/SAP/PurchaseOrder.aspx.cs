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
        public static DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = new DataTable();
                dt.Columns.Add("No");
                dt.Columns.Add("Code");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("Discount");
                dt.Columns.Add("Taxcode");
                dt.Columns.Add("Total");
                dt.Columns.Add("Whse");
                dt.Columns.Add("BlanketAgreement");
                for (int i = 0; i < 5; i++)
                    dt.Rows.Add(i, "", "", "", "", "", "", "", "");
                this.lvContents.DataSource = dt;
                this.lvContents.DataBind();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

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
                            dr["Code"] = chosenItem.ItemCode;
                            dr["Quantity"] = 1;
                            dr["UnitPrice"] = "AUD 250.0";
                            dr["Discount"] = "0.00";
                            dr["Total"] = "250.0";

                            //dt.Rows.
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
                            dr["Whse"] = chosenWarehouse.WhsName;

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
                            this.lblStatus.Text = "Open";
                            this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
                            this.txtDeliveryDate.Text = DateTime.Now.ToShortDateString();
                            this.txtDocumentDate.Text = DateTime.Now.ToShortDateString();

                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public String _collectData()
        {
            try
            {
                PurchaseInfo objInfo = new PurchaseInfo("adminInfo", this.txtPostingDate.Text, this.txtDeliveryDate.Text, this.txtDocumentDate.Text, this.txtVendor.Text, txtName.Text);
                foreach (ListViewDataItem item in this.lvContents.Items)
                {
                    Label lblNo = item.FindControl("lblNo") as Label;
                    Label lblCode = item.FindControl("lblCode") as Label;
                    Label lblQuantity = item.FindControl("lblQuantity") as Label;
                    Label lblPrice = item.FindControl("lblPrice") as Label;
                    Label lblDiscount = item.FindControl("lblDiscount") as Label;
                    Label lblTaxcode = item.FindControl("lblTaxcode") as Label;
                    Label lblTotal = item.FindControl("lblTotal") as Label;
                    Label lblWhse = item.FindControl("lblWhse") as Label;
                    Label lblBlanketAgreement = item.FindControl("lblBlanketAgreement") as Label;
                    if (!String.IsNullOrEmpty(lblCode.Text))
                    {
                        OrderItem objOrder = new OrderItem(lblCode.Text, "", int.Parse(lblQuantity.Text), float.Parse(lblDiscount.Text), lblWhse.Text, "", double.Parse(lblTotal.Text));
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            String results = ts.CreateMarketingDocument(requestXML);
            if(results.IndexOf("Exception") >= 0)
            {
                Session["errorMessages"] = results;
                Session["requestXML"] = requestXML;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKPopupErrors", "Main.openErrorPage();", true);
        }
    }
}