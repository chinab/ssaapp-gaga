using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.Admin.DAO;


namespace SAP
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //MasterData masterDataWS = new MasterData();
                //lblCompany.Text = masterDataWS.GetCompanySetting(HttpContext.Current.User.Identity.Name).Tables[0].Rows[0]["CompanyName"].ToString();
                AuthorizeUser();
            }
        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            GetDefault df = new GetDefault();
            df.LogOut("");

        }

        private void AuthorizeUser()
        {
            string pageName = Request.Url.Segments[Request.Url.Segments.Length - 1].ToLower().Trim().Replace(".aspx", "");
            if (pageName.StartsWith("login") || pageName.StartsWith("default") || pageName.StartsWith("exception"))
            {
                return;
            }

            string userName = "";

            if (Context.User != null)
            {
                userName = Context.User.Identity.Name.ToLower();
            }

            if (!AuthorizeUser(userName, pageName))
            {
                Response.Redirect("~/PermissionDenied.aspx");
            }
        }

        public static bool AuthorizeUser(string userName, string pageName)
        {
            try
            {
                /*if (userName.ToLower().Trim() == "admin")
                {
                    return true;
                }*/

                if (pageName.Contains("login") || pageName.Contains("permissiondenied"))
                    return true;

                string[] roleName = Roles.GetRolesForUser(userName);
                if (roleName == null || roleName.Length == 0) { return false; }
                RolePermissions roleController = new RolePermissions();
                List<RolePermissions> allowedPages = null;


                if (HttpContext.Current.Session["AllowedPages"] != null)
                {
                    allowedPages = (List<RolePermissions>)HttpContext.Current.Session["AllowedPages"];
                }
                else
                {
                    allowedPages = new List<RolePermissions>();
                    for (int i = 0; i < roleName.Length; i++)
                    {
                        List<RolePermissions> allowedPage = roleController.GetByRolePermissionName(roleName[i]);
                        if (allowedPage != null || allowedPage.Count > 0)
                        {
                            allowedPages.AddRange(allowedPage);
                        }
                    }
                    HttpContext.Current.Session.Add("AllowedPages", allowedPages);
                }

                if (allowedPages != null && allowedPages.Count > 0)
                {
                    foreach (RolePermissions allowedPage in allowedPages)
                    {
                        if (allowedPage.PageName.ToLower().Trim().Contains(pageName))
                        {
                            return (bool)allowedPage.Accessable;
                        }
                    }
                    return false;
                }
                

                return (bool)allowedPages[0].Accessable;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        
       
    }
}