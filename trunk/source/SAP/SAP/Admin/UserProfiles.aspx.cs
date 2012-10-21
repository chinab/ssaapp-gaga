using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Profile;
using System.Data;

namespace SAP.Admin
{
    public partial class UserProfiles : System.Web.UI.Page
    {
        public static DataTable dtUserProfile;
        public static DataTable dtUserDefault;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadProfile();

                dtUserProfile = new DataTable();
                dtUserProfile.Columns.Add("No");
                dtUserProfile.Columns.Add("RoleName");
                dtUserProfile.Columns.Add("RoleDescription");
                dtUserProfile.Rows.Add("1", "Admin", "Administrators");
                dtUserProfile.Rows.Add("2", "Admin", "Administrators");
                dtUserProfile.Rows.Add("3", "Admin", "Administrators");
                listUserRoles.DataSource = dtUserProfile;
                listUserRoles.DataBind();

                dtUserDefault = new DataTable();
                dtUserDefault.Columns.Add("DefaultCode");
                dtUserDefault.Columns.Add("DefaultValue");
                dtUserDefault.Columns.Add("DefaultByQuery");
                dtUserDefault.Rows.Add("1", "Admin", "Administrators");
                dtUserDefault.Rows.Add("2", "Admin", "Administrators");
                dtUserDefault.Rows.Add("3", "Admin", "Administrators");
                listUserDefault.DataSource = dtUserDefault;
                listUserDefault.DataBind();
            }
        }

        private void loadProfile()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            txtUserId.Text = User.Identity.Name;
            GetDefault getDefaultWS = new GetDefault();
            DataSet defaultVendor = getDefaultWS.GetDefaultBP(User.Identity.Name, "S");

            //extract to funtion later
            if (defaultVendor != null)
            {
                this.txtUserName.Text = defaultVendor.Tables[0].Rows[0]["CardName"].ToString();
            }
            txtPassword.Text = "*************";
            txtPasswordConfirmation.Text = "*************";
            var profile = HttpContext.Current.Profile;            
            txtEmail.Text = profile.GetPropertyValue("Email").ToString();
            txtPhone.Text = profile.GetPropertyValue("Phone").ToString();
            txtRef1.Text = profile.GetPropertyValue("Ref1").ToString();
            txtRef2.Text = profile.GetPropertyValue("Ref2").ToString();
            txtRef3.Text = profile.GetPropertyValue("Ref3").ToString();
            txtRef4.Text = profile.GetPropertyValue("Ref4").ToString();

            //RoleList.DataSource = Roles.GetRolesForUser(User.Identity.Name);
            //RoleList.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            var profile = HttpContext.Current.Profile;
            profile.SetPropertyValue("Email", txtEmail.Text);
            profile.SetPropertyValue("Phone", txtPhone.Text);
            profile.SetPropertyValue("Ref1", txtRef1.Text);
            profile.SetPropertyValue("Ref2", txtRef2.Text);
            profile.SetPropertyValue("Ref3", txtRef3.Text);
            profile.SetPropertyValue("Ref4", txtRef4.Text);
            profile.Save();
            Membership.UpdateUser(user);

        }
    }
}