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
    public partial class PurchaseOrder_EditTaxCode : System.Web.UI.Page
    {
        protected static DataSet taxCodes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                MasterData masterDataWS = new MasterData();
                taxCodes = masterDataWS.GetTaxGroup("1");
                //this.gridTaxCodes.DataSource = warehousesItems.Tables[0];
                //this.gridTaxCodes.DataBind();
                //this.lblTest.Text = "Load vendors" + warehousesItems.Tables[0].Rows.Count;
                BindCategories("");
                editTaxCodeUpdatePanel.Update();

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<TaxGroup> list = TaxGroup.extractFromDataSet(taxCodes.Tables[0]);
                TaxGroup chosenTaxCode = list[Int32.Parse(selectedValue)];
                Session["chosenTaxCode"] = chosenTaxCode;
                Session["chosenItemNo"] = Request.QueryString["id"];
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKTaxCodePopup", "Main.okEditTaxCodeClick();", true);

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
                DataTable gridTable = new DataTable("TaxCodes");
                gridTable.Columns.Add("Selected");
                gridTable.Columns.Add("No");
                gridTable.Columns.Add("Code");
                gridTable.Columns.Add("Name");
                DataTable vendorsTable = taxCodes.Tables[0];
                DataRow dr;
                int i = 0;
                foreach (DataRow row in vendorsTable.Rows)
                {
                    if (("" + row[0].ToString() + row[1].ToString()).Trim().IndexOf(CategoryFilter.Trim()) >= 0)
                    {
                        dr = gridTable.NewRow();
                        if (i == 0)
                            dr["Selected"] = "checked";
                        else
                            dr["Selected"] = "";
                        dr["No"] = i.ToString(); vendorsTable.Rows.IndexOf(row);
                        dr["Code"] = row[0].ToString();
                        dr["Name"] = row[1].ToString();
                        i++;
                        gridTable.Rows.Add(dr);
                    }
                }

                listTaxCodes.DataSource = gridTable;
                listTaxCodes.DataBind();
                editTaxCodeUpdatePanel.Update();
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