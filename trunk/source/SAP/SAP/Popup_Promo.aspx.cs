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
    public partial class Popup_Promo : System.Web.UI.Page
        {
            protected static DataSet promoCodes;
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    String userId = User.Identity.Name;
                    String itemCode = Request.QueryString["itemCode"];
                    String cardCode = Request.QueryString["cardCode"];
                    Int32 quantity =  geIntFromObject(Request.QueryString["Quantity"]);                    
                    DateTime docDate = DateTime.Now;
                    double amount = getDoubleFormDataRow(Request.QueryString["amount"]);               
                    GetDefault getDefaultWS = new GetDefault();
                    promoCodes = getDefaultWS.GetPromotion(userId, itemCode, cardCode, quantity, docDate, amount);
                    BindCategories("");
                    editPromoCodxeUpdatePanel.Update();

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<Promotion> list = Promotion.extractFromDataSet(promoCodes.Tables[0]);
                Promotion chosenPrmo = list[Int32.Parse(selectedValue)];
                Session["chosenPromo"] = chosenPrmo;
                Session["chosenItemNo"] = Request.QueryString["id"];
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKPromoPopup", "Main.okDialogClick('EditPromoCallBack');", true);

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
                gridTable.Columns.Add("ID");
                gridTable.Columns.Add("ProCode");
                gridTable.Columns.Add("ProName");
                gridTable.Columns.Add("ProValue");
                gridTable.Columns.Add("ProQty");
                gridTable.Columns.Add("ProTrf");
                gridTable.Columns.Add("ItemCode");
                gridTable.Columns.Add("ItemName");
                gridTable.Columns.Add("Condition");
                gridTable.Columns.Add("IsReplace");
                gridTable.Columns.Add("Sole");
                gridTable.Columns.Add("ChkCond");
                gridTable.Columns.Add("HeadDscAmt");
                gridTable.Columns.Add("HeadDscPer");
                gridTable.Columns.Add("WhsCode");

                DataTable vendorsTable = promoCodes.Tables[0];
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
                        dr["ID"] =row[0].ToString();
                        dr["ProCode"] =row[1].ToString();
                        dr["ProName"] =row[2].ToString();
                        dr["ProValue"] =row[3].ToString();
                        dr["ProQty"] =row[4].ToString();
                        dr["ProTrf"] =row[5].ToString();
                        dr["ItemCode"] =row[6].ToString();
                        dr["ItemName"] =row[7].ToString();
                        dr["Condition"] =row[8].ToString();
                        dr["IsReplace"] =row[9].ToString();
                        dr["Sole"] =row[10].ToString();
                        dr["ChkCond"] =row[11].ToString();
                        dr["HeadDscAmt"] =row[12].ToString();
                        dr["HeadDscPer"] =row[13].ToString();
                        dr["WhsCode"] =row[14].ToString();

                        i++;
                        gridTable.Rows.Add(dr);
                    }
                }

                listPromoCodes.DataSource = gridTable;
                listPromoCodes.DataBind();
                editPromoCodxeUpdatePanel.Update();
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

        public double getDoubleFormDataRow(Object input)
        {
            double result = 0.0;
            try
            {
                if (input != null)
                    result = Double.Parse(input.ToString());
            }
            catch (Exception ex)
            {
                result = 0.0;
            }
            return result;
        }
        public Int32 geIntFromObject(Object input)
        {
            Int32 result = 0;
            try
            {
                if (input != null)
                    result = Int32.Parse(input.ToString());
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
    }
}