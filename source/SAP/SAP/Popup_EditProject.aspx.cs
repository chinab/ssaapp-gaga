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
    public partial class Popup_EditProject : System.Web.UI.Page
    {
        protected static DataSet warehousesItems;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                MasterData masterDataWS = new MasterData();
                warehousesItems = masterDataWS.GetProject(User.Identity.Name);
                //this.gridWareHouses.DataSource = warehousesItems.Tables[0];
                //this.gridWareHouses.DataBind();
                //this.lblTest.Text = "Load vendors" + warehousesItems.Tables[0].Rows.Count;
                BindCategories("");
                editWareHouseUpdatePanel.Update();

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<Project> list = Project.extractFromDataSet(warehousesItems.Tables[0]);
                Project chosenProject = list[Int32.Parse(selectedValue)];
                Session["chosenProject"] = chosenProject;
                //Session["chosenItemNo"] = Request.QueryString["id"];
            }
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "12344", "alert('This pops up')", true); 
            //ScriptManager.RegisterClientScriptBlock("", this.GetType(), "script", "alert('Hi');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKWareHousePopup", "Main.okDialogClick('EditProjectCallBack');", true);

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
                DataTable gridTable = new DataTable("WareHouses");
                gridTable.Columns.Add("Selected");
                gridTable.Columns.Add("No");
                gridTable.Columns.Add("Code");
                gridTable.Columns.Add("Name");
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
                        dr["Code"] = row[0].ToString();
                        dr["Name"] = row[1].ToString();
                        
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
    }
}