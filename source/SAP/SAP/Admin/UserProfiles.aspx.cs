using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace SAP.Admin
{
    public partial class UserProfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadProfile();
            }
        }

        private void loadProfile()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            var profile = HttpContext.Current.Profile;
            txtCountry.Text = profile.GetPropertyValue("Country").ToString();
            txtGender.Text = profile.GetPropertyValue("Gender").ToString();
            txtAge.Text = profile.GetPropertyValue("Age").ToString();
            RoleList.DataSource = Roles.GetRolesForUser(User.Identity.Name);
            RoleList.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            var profile = HttpContext.Current.Profile;
            profile.SetPropertyValue("Country", txtCountry.Text);
            profile.SetPropertyValue("Gender", txtGender.Text);
            profile.SetPropertyValue("Age", Int32.Parse(txtAge.Text));
            profile.Save();
            Membership.UpdateUser(user);

        }

    }
}