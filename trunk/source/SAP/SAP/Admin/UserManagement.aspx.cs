using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Web.Profile;

namespace SAP.Admin
{
    public partial class UserManagement : System.Web.UI.Page
    {
        public static DataTable dtListUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtListUser = new DataTable();
                dtListUser.Columns.Add("No");
                dtListUser.Columns.Add("UserId");
                dtListUser.Columns.Add("UserName");
                dtListUser.Columns.Add("Email");
                dtListUser.Columns.Add("View");

                MembershipUserCollection users = Membership.GetAllUsers();
                ProfileInfoCollection profiles = ProfileManager.GetAllProfiles(ProfileAuthenticationOption.All);
                GetDefault getDefaultWS = new GetDefault();
                
                int No = 0;
                foreach(MembershipUser user in users)
                {
                    ProfileBase webProfile = ProfileBase.Create(user.UserName);

                    No ++;
                    DataSet defaultVendor = getDefaultWS.GetDefaultBP(user.UserName, "S");
                    String userId = user.UserName;
                    String userName = "";
                    if (defaultVendor != null)
                    {
                       userName  = defaultVendor.Tables[0].Rows[0]["CardName"].ToString();
                    }
                    String email = webProfile.GetPropertyValue("Email").ToString();
                    String phone = webProfile.GetPropertyValue("Phone").ToString();
                    String view = "/Admin/UserProfiles.aspx?name=" + userId;
                    dtListUser.Rows.Add(No, userId, userName, email, view);
                }
                listUsers.DataSource = dtListUser;
                listUsers.DataBind();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e) {
            Response.Redirect("/Admin/UserProfiles.aspx?action=new");
        }


    }
}