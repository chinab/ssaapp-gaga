using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Profile;

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

            ListBox listBox = (ListBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("RoleListAll");
            listBox.DataSource = Roles.GetAllRoles();
            listBox.DataBind();

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

        // CreatedUser event is called when a new user is successfully created
        public void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            var profile = ProfileBase.Create(CreateUserWizard1.UserName);
            profile.SetPropertyValue("Country", ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Country")).SelectedValue);
            profile.SetPropertyValue("Gender", ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Gender")).SelectedValue);
            profile.SetPropertyValue("Age", Int32.Parse(((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Age")).Text));
            Roles.AddUserToRole(CreateUserWizard1.UserName, "manager");
            profile.Save();
        }
        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCountry.Text = "";
            txtGender.Text = "";
            txtAge.Text = "";
        }

    }
}