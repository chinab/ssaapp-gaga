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
    public partial class Popup_EditTerritory : System.Web.UI.Page
    {
        protected static DataSet TerritoryItem;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                MasterData masterDataWS = new MasterData();
                TerritoryItem = masterDataWS.GetTerritory(User.Identity.Name);
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
                DataTable gridTable = new DataTable("Territory");
                gridTable.Columns.Add("Selected");
                gridTable.Columns.Add("No");
                gridTable.Columns.Add("territryID");
                gridTable.Columns.Add("descript");
                gridTable.Columns.Add("Parent");
                DataTable warehouseTable = TerritoryItem.Tables[0];
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
                        dr["territryID"] = row[0].ToString();
                        dr["descript"] = row[1].ToString();
                        dr["Parent"] = row[1].ToString();
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
                List<Territory> list = Territory.extractFromDataSet(TerritoryItem.Tables[0]);
                Territory chosenTerritory = list[Int32.Parse(selectedValue)];
                Session["chosenTerritory"] = chosenTerritory;
                //Session["chosenTerritoryNo"] = Request.QueryString["id"];
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKTerritoryPopup", "Main.okDialogClick('EditTerritoryCallBack');", true);

        }
    }
}