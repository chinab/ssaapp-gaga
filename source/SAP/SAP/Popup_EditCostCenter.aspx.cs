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
    public partial class Popup_EditCostCenter : System.Web.UI.Page
    {
        protected static DataSet CostCenterItems;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String Dimension = Request.QueryString["Dimension"];
                MasterData masterDataWS = new MasterData();
                CostCenterItems = masterDataWS.GetCostCenter(Int32.Parse(Dimension), User.Identity.Name);
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
                DataTable gridTable = new DataTable("WareHouses");
                gridTable.Columns.Add("Selected");
                gridTable.Columns.Add("No");
                gridTable.Columns.Add("Code");
                gridTable.Columns.Add("Name");
                DataTable warehouseTable = CostCenterItems.Tables[0];
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

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<CostCenter> list = CostCenter.extractFromDataSet(CostCenterItems.Tables[0]);
                CostCenter chosenWareHouse = list[Int32.Parse(selectedValue)];
                Session["chosenCostCenter"] = chosenWareHouse;
                Session["chosenItemNo"] = Request.QueryString["id"];
                Session["chosenDimesion"] = Request.QueryString["Dimension"];
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKCostCenterPopup", "Main.okDialogClick('EditCostCenterCallBack');", true);

        }
    }
}