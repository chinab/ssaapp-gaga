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
                String action = Request.QueryString["action"];
                String name = Request.QueryString["name"];
                String defaultName = User.Identity.Name;
                if (!String.IsNullOrEmpty(name))
                {
                    loadProfile(name);
                    this.btnSaveProfiles.Visible = true;
                    this.btnAddProfiles.Visible = false;
                }
                else if (String.IsNullOrEmpty(action))
                {
                    loadProfile(defaultName);
                    this.btnSaveProfiles.Visible = true;
                    this.btnAddProfiles.Visible = false;
                }
                else
                {
                    this.btnSaveProfiles.Visible = false;
                    this.btnAddProfiles.Visible = true;                    
                    //Set default Value;
                }
                this.txtUserName.Enabled = false;
            }
        }

        private void loadProfile(String userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            txtUserId.Text = userName;
            GetDefault getDefaultWS = new GetDefault();
            DataSet defaultVendor = getDefaultWS.GetDefaultBP(userName, "S");

            //extract to funtion later
            if (defaultVendor != null)
            {
                this.txtUserName.Text = defaultVendor.Tables[0].Rows[0]["CardName"].ToString();
            }
            txtPassword.Attributes.Add("value", "NoChange");
            txtPasswordConfirmation.Attributes.Add("value", "NoChange");
            var profile = HttpContext.Current.Profile;
            txtEmail.Text = profile.GetPropertyValue("Email").ToString();
            txtPhone.Text = profile.GetPropertyValue("Phone").ToString();
            txtRef1.Text = profile.GetPropertyValue("Ref1").ToString();
            txtRef2.Text = profile.GetPropertyValue("Ref2").ToString();
            txtRef3.Text = profile.GetPropertyValue("Ref3").ToString();
            txtRef4.Text = profile.GetPropertyValue("Ref4").ToString();

            

            dtUserDefault = new DataTable();
            dtUserDefault.Columns.Add("DefaultCode");
            dtUserDefault.Columns.Add("DefaultValue");
            dtUserDefault.Columns.Add("DefaultByQuery");
            dtUserDefault.Rows.Add("1", "Admin", "Administrators");
            dtUserDefault.Rows.Add("2", "Admin", "Administrators");
            dtUserDefault.Rows.Add("3", "Admin", "Administrators");
            listUserDefault.DataSource = dtUserDefault;
            listUserDefault.DataBind();



            //RoleList.DataSource = Roles.GetRolesForUser(User.Identity.Name);
            //RoleList.DataBind();
            dtUserProfile = new DataTable();
            dtUserProfile.Columns.Add("Select");
            dtUserProfile.Columns.Add("RoleName");
            dtUserProfile.Columns.Add("RoleDescription");
            String[] roles = Roles.GetAllRoles();
            String[] rolesByUser = Roles.GetRolesForUser(User.Identity.Name);

            foreach (String role in roles)
            {
                String roleDescription = role;
                Boolean roleSelect = false;
                foreach (var userRole in rolesByUser)
                {
                    if (role == userRole)
                        roleSelect = true;
                }
                dtUserProfile.Rows.Add(roleSelect, role, roleDescription);
            }

            listUserRoles.DataSource = dtUserProfile;
            listUserRoles.DataBind();
            for (int i = 0; i < listUserRoles.Items.Count; i++)
            {
                CheckBox chk = listUserRoles.Items[i].FindControl("chkRoleSelected") as CheckBox;
                chk.Checked = Boolean.Parse(dtUserProfile.Rows[i]["Select"].ToString());
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            String message = "";
            if (!String.IsNullOrEmpty(txtUserId.Text) && !String.IsNullOrEmpty(txtUserName.Text) && !String.IsNullOrEmpty(txtPassword.Text) && !String.IsNullOrEmpty(txtEmail.Text))
            {
                if (txtPassword.Text.Equals(txtPasswordConfirmation.Text))
                {
                    try
                    {
                        MembershipUser user = Membership.CreateUser(this.txtUserId.Text, this.txtPassword.Text, this.txtEmail.Text);
                        var profile = ProfileBase.Create(user.UserName);
                        profile.SetPropertyValue("Email", txtEmail.Text);
                        profile.SetPropertyValue("Phone", txtPhone.Text);
                        profile.SetPropertyValue("Ref1", txtRef1.Text);
                        profile.SetPropertyValue("Ref2", txtRef2.Text);
                        profile.SetPropertyValue("Ref3", txtRef3.Text);
                        profile.SetPropertyValue("Ref4", txtRef4.Text);
                        profile.Save();
                        message = "User has been created successul !!!";
                    }
                    catch (Exception )
                    {
                        message = "Invalid Password, Passwords must conform to the password strength requirements !!!";
                    }
                }
                else
                {
                    message = "Password does not match !!!";
                }
            }
            else
            {
                message = "Please enter correct your profile information !!!";

            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('" + message + "','');", true);

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            String message = "";
            if (!String.IsNullOrEmpty(txtPassword.Text))
            {
                if (txtPassword.Text.Equals(txtPasswordConfirmation.Text))
                {
                    MembershipUser user = Membership.GetUser(this.txtUserId.Text);
                    if(!"NoChange".Equals(txtPassword.Text))
                        user.ChangePassword(user.ResetPassword(), txtPassword.Text);
                    //user.ChangePassword(
                    var profile = HttpContext.Current.Profile;
                    profile.SetPropertyValue("Email", txtEmail.Text);
                    profile.SetPropertyValue("Phone", txtPhone.Text);
                    profile.SetPropertyValue("Ref1", txtRef1.Text);
                    profile.SetPropertyValue("Ref2", txtRef2.Text);
                    profile.SetPropertyValue("Ref3", txtRef3.Text);
                    profile.SetPropertyValue("Ref4", txtRef4.Text);
                    profile.Save();
                    Membership.UpdateUser(user);
                    message = "Your information has been updated !!!";
                }
                else
                {
                    message = "Your password confirmation is not matched !!!";
                }
            }
            else
            {
                message = "Please enter correct your profile information !!!";

            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('" + message + "','');", true);

        }
    }
}