using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
                this.lvContents.DataSource = dt;
                this.lvContents.DataBind();
            }
        }
    }
}