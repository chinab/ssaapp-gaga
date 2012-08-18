using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAP
{
    public partial class PurchaseOrder_EditItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblTest.Text = "Load data for item has id: " + Request.QueryString["id"];
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["data"] = "NewData";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKPopup", "Main.okEditItemClick()", true);
        }
    }
}