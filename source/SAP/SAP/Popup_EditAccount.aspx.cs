using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;

namespace SAP
{
    public partial class Popup_EditAccount : System.Web.UI.Page
    {
        protected static DataSet warehousesItems;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MasterData masterDataWS = new MasterData();
                warehousesItems = masterDataWS.GetAccountMasterData("", User.Identity.Name);
                BindCategories("");
                editWareHouseUpdatePanel.Update();
            }
        }

        protected void txtCategoryNameHeader_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            BindCategories(text);
        }

        private void BindCategories(string CategoryFilter)
        {

            try
            {
                // Simple created a table to bind with Grid view and populated it with data.
                DataTable gridTable = new DataTable("Account");
                gridTable.Columns.Add("Selected");
                gridTable.Columns.Add("No");
                gridTable.Columns.Add("AcctCode");
                gridTable.Columns.Add("AcctName");
                gridTable.Columns.Add("FrgnName");
                DataTable warehouseTable = warehousesItems.Tables[0];
                DataRow dr;
                int i = 0;
                foreach (DataRow row in warehouseTable.Rows)
                {
                    if (("" + row[0].ToString() + row[1].ToString()).Trim().IndexOf(CategoryFilter.Trim()) >= 0)
                    {
                        dr = gridTable.NewRow();
                        if (i == 0)
                            dr["Selected"] = "checked";
                        else
                            dr["Selected"] = "";
                        dr["No"] = i.ToString(); warehouseTable.Rows.IndexOf(row);
                        dr["AcctCode"] = row[0].ToString();
                        dr["AcctName"] = row[1].ToString();
                        dr["FrgnName"] = row[1].ToString();
                        gridTable.Rows.Add(dr);
                    }
                    i++;
                }

                listWareHouses.DataSource = gridTable;
                listWareHouses.DataBind();
                editWareHouseUpdatePanel.Update();
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindCategories(this.txtFilter.Text);
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<AccountMasterData> list = AccountMasterData.extractFromDataSet(warehousesItems.Tables[0]);
                AccountMasterData chosenAccount = list[Int32.Parse(selectedValue)];
                Session["chosenAccount"] = chosenAccount;
                Session["chosenItemNo"] = Request.QueryString["id"];
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKAccountPopup", "Main.okDialogClick('EditAccountCallBack');", true);
        }
    }
}