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
                if (Request.Browser.Browser=="IE")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                          "alert('Not support IE');", true);
                    this.Login1.Enabled = false;
                    return;
                }
                if (User.Identity.IsAuthenticated == true)
                {
                    GetDefault df = new GetDefault();
                    if (df.GetConnection(User.Identity.Name)==0)
                        Response.Redirect("Homepage.aspx");
                    else
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading","alert('Connect to SAP failed!');", true);
                    
                }
            }
            
        }

      
    }
}
