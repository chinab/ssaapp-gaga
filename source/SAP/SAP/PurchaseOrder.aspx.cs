using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;

namespace SAP
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("No");
                dt.Columns.Add("Code");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("Discount");
                dt.Columns.Add("Taxcode");
                dt.Columns.Add("Total");
                dt.Columns.Add("Whse");
                dt.Columns.Add("BlanketAgreement");
                for (int i = 0; i < 5; i ++ )
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
                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditItemCallBack":
                        ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;
                        Int32 itemNo =  Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenItem != null)
                        {
                            // update grid
                            DataTable dt = (DataTable)this.lvContents.DataSource;
                            DataRow dr = dt.Rows[itemNo];
                            dr["Code"] = chosenItem.ItemCode;
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
    }
}