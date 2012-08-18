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
<<<<<<< .mine
                        Int32 itemNo = Int32.Parse(Session["chosenItemNo"] as String);
=======
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
>>>>>>> .r27
                        if (chosenItem != null)
                        {
                            // update grid
                            DataRow dr = dt.Rows[itemNo];
                            dr["Code"] = chosenItem.ItemCode;

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
                    case "EditVendorCallBack":
                        BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                        if (chosenPartner != null)
                        {
                            this.txtName.Text = chosenPartner.CardName;
                            this.txtVendor.Text = chosenPartner.CardCode;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        
        public void _collectData()
        {
            try
            {
                PurchaseInfo objInfo = new PurchaseInfo("adminInfo", this.txtPostingDate.Text, this.txtDeliveryDate.Text, this.txtDocumentDate.Text, this.txtVendor.Text, txtName.Text);
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

>>>>>>> .r27
    }
}