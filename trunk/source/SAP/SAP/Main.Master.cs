using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace SAP
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MasterData masterDataWS = new MasterData();
                lblCompany.Text = masterDataWS.GetCompanySetting(HttpContext.Current.User.Identity.Name).Tables[0].Rows[0]["CompanyName"].ToString();
            }
        }

        
        
        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            GetDefault df = new GetDefault();
            df.LogOut("");

        }
        
       
    }
}