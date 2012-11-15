using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace SAP.Admin
{
    public partial class SystemUserEdit : System.Web.UI.Page
    {
        #region Properties
        private bool changePassword = false;
        public bool ChangePassword
        {
            get { return changePassword; }
            set { changePassword = value; }
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.InitValue();
                this.ShowUserDetail();
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid) { return; }

                string userName = Request.QueryString["userName"]; 
                string password = passwordTextbox.Text;
                string roleName = rolesDropDownList.SelectedItem.Value.Trim();
                string email = emailTextbox.Text.Trim();
                string currentUser = Context.User.Identity.Name.ToLower();

                if (userName.ToLower().Trim() == "admin" && currentUser != "admin")
                {
                    return;
                }

                MembershipUser user = Membership.GetUser(userName);
                userName = userNameTextbox.Text.Trim();
                if (user == null && password.Length < 5)
                {
                    return;
                }

                //Add new user
                if (user == null)
                {
                    if (Roles.IsUserInRole(userName, roleName))
                    {
                        return;
                    }

                    if (string.IsNullOrEmpty(email))
                    {
                        WebHelper.AddMemberShipUserWithoutEmail(userName, password, roleName);
                    }
                    else
                    {
                        WebHelper.AddMemberShipUser(userName, password, email, roleName);
                    }


                    user = Membership.GetUser(userName);
                    user.IsApproved = chkActive.Checked;
                    Membership.UpdateUser(user);

                    string loggedData = string.Format("{0}|{1}", userName, roleName);
                }
                else //Edit user
                {
                    string[] roles = Roles.GetRolesForUser(user.UserName);
                    if (!CheckPermitRoles(roles))
                    {

                        return;
                    }

                    if (!ChangePassword && roles.Length > 0 && roles[0].ToLower().Trim() != rolesDropDownList.SelectedItem.Value.ToLower().Trim())
                    {
                        Roles.RemoveUserFromRoles(user.UserName, roles);
                        Roles.AddUserToRole(user.UserName, rolesDropDownList.SelectedItem.Value.ToLower().Trim());
                    }

                    if (!string.IsNullOrEmpty(password))
                    {
                        // Unlock user
                        if (user.IsLockedOut) { user.UnlockUser(); }
                        string currentPassword = user.GetPassword();
                        user.ChangePassword(currentPassword, passwordTextbox.Text.Trim());
                    }

                    user.Email = email;

                    if (String.Compare(user.UserName, userName) == 0)
                    {
                        user.IsApproved = chkActive.Checked;
                        Membership.UpdateUser(user);
                    }
                    else
                    {
                        if (Roles.IsUserInRole(userName, roleName))
                        {
                            return;
                        }

                        // todo: update user name.
                        user = Membership.GetUser(userName);
                    }
                }

                this.Redirect();
            }
            catch (Exception ex)
            {

            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = userNameTextbox.Text.Trim();
                if (userName == "" || userName.ToLower().Trim() == "admin")
                {
                    return;
                }

                MembershipUser deleteUser = Membership.GetUser(userName);
                WebHelper.DeleteMembershipUser(userName);
                this.Redirect();
            }
            catch (Exception ex)
            {
  
            }
        }
        #endregion

        #region Methods
        private void InitValue()
        {
            try
            {
                deleteButton.OnClientClick = "return confirmAction('Do you want do delete this item?')";

                string[] roles = Roles.GetAllRoles();                

                foreach (string role in roles)
                {
                    rolesDropDownList.Items.Add(new ListItem(role, role));
                }

                string userName = string.Empty;
                if (Context.User != null)
                {
                    string currentUser = Request.QueryString["userName"].ToLower().Trim();
                    userName = Context.User.Identity.Name;
                    if (currentUser == userName.ToLower().Trim())
                    {
                        deleteButton.Enabled = false;
                    }
                }

                if (changePassword)
                {
                    string[] rolesOfUser = Roles.GetRolesForUser(userName);
                    ListItem roleItem = rolesDropDownList.Items.FindByValue(rolesOfUser[0]);
                    rolesDropDownList.SelectedIndex = rolesDropDownList.Items.IndexOf(roleItem);
                }
            }
            catch { }
        }

        private void ShowUserDetail()
        {
            try
            {
                deleteButton.Visible = true;
                string userName = Request.QueryString["UserName"];

                if (changePassword)
                {
                    rolesDropDownList.Enabled = false;
                    deleteButton.Visible = false;
                    userName = HttpContext.Current.User.Identity.Name;
                }

                if (userName == "")
                {
                    userNameTextbox.Text = string.Empty;
                    passwordTextbox.Text = string.Empty;
                    userNameTextbox.Enabled = true;
                    userNameTextbox.Focus();
                    deleteButton.Visible = false;
                    return;
                }

                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    return;
                }

                string[] roles = Roles.GetRolesForUser(user.UserName);
                if (roles.Length > 0)
                {
                    rolesDropDownList.SelectedValue = roles[0];
                }

                chkActive.Checked = user.IsApproved;
                lastActivityDateTextBox.Text = user.LastActivityDate.ToString();
                emailTextbox.Text = user.Email;
                userNameTextbox.Text = user.UserName;
                userNameTextbox.Enabled = false;
            }
            catch (Exception ex)
            {
    
            }

        }

        private void SetExtraUserFields(ref MembershipUser user)
        {

        }

        private void BindExtraUserFields(MembershipUser user)
        {

        }

        private bool CheckPermitRoles(string[] roles)
        {
            try
            {
                if (roles == null || roles.Length == 0)
                {
                    return true;
                }

                foreach (ListItem item in rolesDropDownList.Items)
                {
                    if (item.Enabled && WebHelper.IncludeInStringList(item.Text, roles))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        private void Redirect()
        {
            Response.Redirect(changePassword ? "Default.aspx" : "SystemUsers.aspx", false);
        }
        #endregion
    }
}
