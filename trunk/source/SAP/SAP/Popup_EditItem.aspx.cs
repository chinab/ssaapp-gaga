using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAP.WebServices;
using System.Data;

namespace SAP
{
    public partial class Popup_EditItem : System.Web.UI.Page
    {
        protected static DataSet itemMasters;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                MasterData masterDataWS = new MasterData();
                itemMasters = masterDataWS.GetItemMasterData(User.Identity.Name);
                //this.gridVendors.DataSource = warehousesItems.Tables[0];
                //this.gridVendors.DataBind();
                //this.lblTest.Text = "Load vendors" + warehousesItems.Tables[0].Rows.Count;
                BindCategories("");
                editItemUpdatePanel.Update();

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<ItemMaster> list = ItemMaster.extractFromDataSet(itemMasters.Tables[0]);
                ItemMaster chosenItem = list[Int32.Parse(selectedValue)];
                Session["chosenItem"] = chosenItem;
                Session["chosenItemNo"] = Request.QueryString["id"];
            }
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "12344", "alert('This pops up')", true); 
            //ScriptManager.RegisterClientScriptBlock("", this.GetType(), "script", "alert('Hi');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKPopup", "Main.okDialogClick('EditItemCallBack');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "ShowLoading","Dialog.showLoader();", true);
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
                DataTable gridTable = new DataTable("Items");
                gridTable.Columns.Add("Selected");
                gridTable.Columns.Add("No");
                gridTable.Columns.Add("Code");
                gridTable.Columns.Add("Name");
                DataTable itemsTable = itemMasters.Tables[0];
                DataRow dr;
                int i = 0;
                foreach (DataRow row in itemsTable.Rows)
                {
                    if (("" + row[0].ToString() + row[1].ToString()).Trim().IndexOf(CategoryFilter.Trim()) >= 0)
                    {
                        dr = gridTable.NewRow();
                        dr["No"] = i.ToString(); 
                        itemsTable.Rows.IndexOf(row);
                        dr["Code"] = row[0].ToString();
                        dr["Name"] = row[1].ToString();                        
                        gridTable.Rows.Add(dr);
                    }
                    i++;
                }
                gridTable.Rows[gridTable.Rows.Count-1]["Selected"] = "checked";
                listItems.DataSource = gridTable;
                listItems.DataBind();
                editItemUpdatePanel.Update();
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
    }
}