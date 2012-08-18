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
            DataSet businessPartners = masterDataWS.GetBusinessPartner();
            DataSet itemMasters = masterDataWS.GetItemMasterData();
            DataSet taxGroups = masterDataWS.GetTaxGroup("1");
            DataSet whareHouses = masterDataWS.GetWarehouse();
            //List<BusinessPartner> list = BusinessPartner.extractFromDataSet(businessPartners.Tables[0]);
            //List<ItemMaster> listItems = ItemMaster.extractFromDataSet(itemMasters.Tables[0]);
            List<WareHouse> whsItems = WareHouse.extractFromDataSet(whareHouses.Tables[0]);
            //String temp = "List count: " + whsItems.Count;
            //txtTest.Text = temp;

        }
    }
}