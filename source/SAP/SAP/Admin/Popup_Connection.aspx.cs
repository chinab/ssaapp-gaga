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
    public partial class Popup_Connection : System.Web.UI.Page
    {
        protected static DataSet TerritoryItem;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String str = Request.QueryString["ConnectionString"];
                if (str != null)
                {
                    Array arr = str.Split(';');
                    txtSQLDB.Text = arr.GetValue(0).ToString();
                    txtSAPUser.Text = arr.GetValue(1).ToString();
                    txtSAPPwd.Text = arr.GetValue(2).ToString();
                    txtSQLServer.Text = arr.GetValue(3).ToString();
                    txtSQLUser.Text = arr.GetValue(4).ToString();
                    txtSQLPwd.Text = arr.GetValue(5).ToString();
                    ddlSQLType.SelectedValue = arr.GetValue(6).ToString();
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string selectedValue = Request.Form["MyRadioButton"];
            if (!String.IsNullOrEmpty(selectedValue))
            {
                List<Territory> list = Territory.extractFromDataSet(TerritoryItem.Tables[0]);
                Territory chosenTerritory = list[Int32.Parse(selectedValue)];
                Session["chosenTerritory"] = chosenTerritory;
                //Session["chosenTerritoryNo"] = Request.QueryString["id"];
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OKConnectionPopup", "Main.okDialogClick('EditConnectionCallBack');", true);
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            string str = "";
            str=txtSQLDB.Text + ";" + txtSAPUser.Text+";"+txtSAPPwd.Text+";"+txtSQLServer.Text+";"+txtSQLUser.Text+";"+txtSQLPwd.Text+";"+ddlSQLType.SelectedItem.Value.ToString();

            GetDefault gdf = new GetDefault();
            lblTest.Text= gdf.TestConnection(str);
        }
 
    }
}