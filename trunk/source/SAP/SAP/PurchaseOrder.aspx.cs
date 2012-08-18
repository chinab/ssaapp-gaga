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
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("Discount");
                dt.Columns.Add("Taxcode");
                dt.Columns.Add("Total");
                dt.Columns.Add("Whse");
                dt.Columns.Add("BlanketAgreement");
                dt.Rows.Add("1", "1", "1", "1", "1", "1", "1", "1");
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
                        string data = Session["data"] as string;
                        if (!string.IsNullOrEmpty(data))
                        {
                            // update grid
                            DataTable dt = new DataTable();
                            dt.Columns.Add("No");
                            dt.Columns.Add("Quantity");
                            dt.Columns.Add("UnitPrice");
                            dt.Columns.Add("Discount");
                            dt.Columns.Add("Taxcode");
                            dt.Columns.Add("Total");
                            dt.Columns.Add("Whse");
                            dt.Columns.Add("BlanketAgreement");
                            dt.Rows.Add("1", "2", "2", "2", "2", "2", "2", "2");
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