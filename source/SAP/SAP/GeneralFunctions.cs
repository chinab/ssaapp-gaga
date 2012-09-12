using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;
using System.Collections;

namespace SAP
{
    public class GeneralFunctions
    {
        //dr("CompnyName") = "DMS DEMO"
        //dr("DecSep") = "."
        //dr("ThousSep") = ","
        //dr("SumDec") = 0
        //dr("PriceDec") = 0
        //dr("QtyDec") = 0
        //dr("PercentDec") = 2
        //dr("RateDec") = 2
        public String SAP_Date2String(DateTime date)
        {
            return "";
        }
        public DateTime SAP_String2Date(String date)
        {
            return DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
        }
        public String SAP_NumberFormatString(String Para)
        {
            String str;
            str = "";
            
            return str;
        }

        void GetDisplaySetting()
        {
            MasterData MD = new MasterData();
            DataSet ds = MD.GetDisplaySetting();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                String name = row[0].ToString();
                switch (name)
                {
                    case "":
                        break;
                }
            }
        }
    }
}