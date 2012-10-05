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
    public partial class Popup_EditCustomer : System.Web.UI.Page
    {
        protected static DataSet businessPartners;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                MasterData masterDataWS = new MasterData();
                businessPartners = masterDataWS.GetBusinessPartner("C", User.Identity.Name);
                //this.gridVendors.DataSource = warehousesItems.Tables[0];
                //this.gridVendors.DataBind();
               //this.lblTest.Text = "Load vendors" + warehousesItems.Tables[0].Rows.Count;
                BindCategories("");
                editVendorUpdatePanel.Update();
                
            }
        }
       
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<BusinessPartner> list = BusinessPartner.extractFromDataSet(businessPartners.Tables[0]);
                BusinessPartner chosenPartner = list[Int32.Parse(selectedValue)];
                Session["chosenPartner"] = chosenPartner;
            }
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "12344", "alert('This pops up')", true); 
            //ScriptManager.RegisterClientScriptBlock("", this.GetType(), "script", "alert('Hi');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKVendorPopup", "Main.okDialogClick('EditVendorCallBack');", true);
            
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
                DataTable gridTable = new DataTable("Vendors");
                gridTable.Columns.Add("Selected");
                gridTable.Columns.Add("No");
                gridTable.Columns.Add("Code");
                gridTable.Columns.Add("Name");
                DataTable vendorsTable = businessPartners.Tables[0];
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
                        
                        gridTable.Rows.Add(dr);
                    }
                    i++;
                }

                listVendors.DataSource = gridTable;
                listVendors.DataBind();
                editVendorUpdatePanel.Update();
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