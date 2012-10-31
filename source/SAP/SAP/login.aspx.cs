using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAP
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated == true)
                {
                    GetDefault df = new GetDefault();
                    df.GetConnection(User.Identity.Name);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                          "Dialog.hideLoader();", true);
                      Response.Redirect("Homepage.aspx");
                }
            }
            
        }

      
    }
}
