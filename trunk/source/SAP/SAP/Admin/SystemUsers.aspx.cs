using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Collections;

namespace SAP.Admin
{
    public partial class SystemUsers : System.Web.UI.Page
    {
        #region Control
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            systemUsersGridView.PageIndexChanged += new EventHandler(systemUsersGridView_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowUserList();
            }
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            systemUsersGridView.PageIndex = 0;
            ShowUserList();
        }

        protected void systemUsersGridView_PageIndexChanged(object sender, EventArgs e)
        {
            ShowUserList();
        }

        protected void SystemUsersGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            ShowUserList();
        }

        protected void SystemUsersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            systemUsersGridView.PageIndex = e.NewPageIndex;
            systemUsersGridView.DataBind();

            ShowUserList();
        }

        protected void AddNewButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetSystemUserEditUrl(""));
        }
        #endregion
        #region functions

        private void ShowRoleList()
        {
            try
            {
                string[] roleNames = Roles.GetAllRoles();
                string userName = string.Empty;
                if (Context.User != null)
                {
                    userName = Context.User.Identity.Name;
                }
            }
            catch (Exception ex)
            {
  
            }
        }

        private void ShowUserList()
        {
            try
            {
                //Show system user as normal
                MembershipUserCollection list = new MembershipUserCollection();
                string userNameFilter = userNameTextBox.Text.Trim().ToUpper();
                MembershipUserCollection users = Membership.GetAllUsers();

                try
                {
                    foreach (MembershipUser user in users)
                    {                        
                        if (string.IsNullOrEmpty(userNameFilter))
                        {
                            list.Add(user);
                        }
                        else if (user.UserName.ToUpper().Contains(userNameFilter))
                        {
                            list.Add(user);
                        }
                    }
                }
                catch { }

                systemUsersGridView.DataSource = list;
                systemUsersGridView.DataBind();
                //DataBindGridView(ConvertToDataTable(list));
            }
            catch (Exception ex)
            {

            }
        }

        private void DataBindGridView(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                systemUsersGridView.DataSource = null;
            }
            else
            {
                systemUsersGridView.DataSource = table;
            }

            systemUsersGridView.DataBind();
        }

        private DataTable ConvertToDataTable(MembershipUserCollection userList)
        {
            DataTable table = new DataTable();
            table.Columns.Add("UserName");
            table.Columns.Add("Email");

            IEnumerator iList = userList.GetEnumerator();
            string currentUserName = "";

            if (HttpContext.Current.User.Identity != null)
            {
                currentUserName = HttpContext.Current.User.Identity.Name.ToLower().Trim();
            }

            while (iList.MoveNext())
            {
                string userName = ((MembershipUser)(iList.Current)).UserName;
                if ((currentUserName != "admin") && (userName.Trim().Equals("admin", StringComparison.OrdinalIgnoreCase) || currentUserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    continue; // remove currentUser to the list 
                }
                string email = ((MembershipUser)(iList.Current)).Email;

                DataRow row = table.NewRow();
                row["UserName"] = userName;
                row["Email"] = email;
                table.Rows.Add(row);
            }
            return table;
        }

        protected string GetSystemUserEditUrl(string userName)
        {
            return string.Format("SystemUserEdit.aspx?UserName={0}", userName);
        }
        #endregion
    }
}