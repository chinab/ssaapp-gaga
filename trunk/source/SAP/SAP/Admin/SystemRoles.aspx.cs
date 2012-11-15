﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pyco.Framework;
using System.Data;
using System.Web.Security;

namespace SAP.Admin
{
    public partial class SystemRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            if (WebHelper.GetCurrentUserName().ToLower() != "admin")
            {
                addNewButton.Visible = false;
            }
            ShowRoleList();
        }

        private void ShowRoleList()
        {
            try
            {
                string[] roleNames = Roles.GetAllRoles();
                DataTable roleTable = new DataTable();
                roleTable.Columns.Add("RoleName");
                if (roleNames.Length != 0)
                {
                    for (int i = 0; i < roleNames.Length; i++)
                    {
                        DataRow row = roleTable.NewRow();
                        row["RoleName"] = Server.HtmlEncode(roleNames[i]);
                        roleTable.Rows.Add(row);
                    }
                }

                systemRoleGridView.DataSource = roleTable;
                systemRoleGridView.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }
        }
        protected void systemRoleGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            ShowRoleList();
        }
    }
}