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
    public partial class GetBusinessPartner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterData masterDataWS = new MasterData();            
            //masterDataWS.Url = 
            DataSet businessPartners = masterDataWS.GetBusinessPartner("S", User.Identity.Name);
            DataSet itemMasters = masterDataWS.GetItemMasterData(User.Identity.Name);
            DataSet taxGroups = masterDataWS.GetTaxGroup("1", User.Identity.Name);
            DataSet whareHouses = masterDataWS.GetWarehouse(User.Identity.Name);
            //List<BusinessPartner> list = BusinessPartner.extractFromDataSet(warehousesItems.Tables[0]);
            //List<ItemMaster> listItems = ItemMaster.extractFromDataSet(employeeSet.Tables[0]);
            List<WareHouse> whsItems = WareHouse.extractFromDataSet(whareHouses.Tables[0]);
            //String temp = "List count: " + whsItems.Count;
            //txtTest.Text = temp;

        }
    }
}