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
            GetDefault df = new GetDefault();
            DataSet ds = df.GetLoginInfo(HttpContext.Current.User.Identity.Name);
            lblCompany.Text=ds.Tables[0].Rows[0]["compnyName"].ToString();
        }

        protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
        {
            //this.ContentPlaceHolder1.FindControl(
        }
        
        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            GetDefault df = new GetDefault();
            df.LogOut("");

        }
        
       
    }
}