﻿using System;
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

        public String GetHeaderTableTag(String ObjType)
        {
            String str = "";
            switch (ObjType)
            {
                case "19": //AP Credit
                    str = "ORPC";
                    break;
                case "20": //GRPO
                    str = "OPDN";
                    break;
                case "21": //Goods Return
                    str = "ORPD";
                    break;
                case "22": //Purchase Order
                    str = "OPOR";
                    break;

                case "13": //AR Invoice
                    str = "OINV";
                    break;
                case "14": //AR Credit
                    str = "ORIN";
                    break;
                case "15": //Delivery
                    str = "ODLN";
                    break;

                case "59": //Goods Receipt
                    str = "OIGN";
                    break;
                case "60": //Goods Issue
                    str = "OIGE";
                    break;

                case "97": //sales opportunity
                    str = "OOPR";
                    break;
            }
            return str;
        }
        public String GetLineTableTag(String ObjType, int num)
        {
            String str = "";
            switch (ObjType)
            {
                case "19": //AP Credit
                    str = "RPC1";
                    break;
                case "20": //GRPO
                    str = "PDN1";
                    break;
                case "21": //Goods Return
                    str = "RPD1";
                    break;
                case "22": //Purchase Order
                    str = "POR1";
                    break;

                case "13": //AR Invoice
                    str = "INV1";
                    break;
                case "14": //AR Credit
                    str = "RIN1";
                    break;
                case "15": //Delivery
                    str = "DLN1";
                    break;

                case "59": //Goods Receipt
                    str = "IGN1";
                    break;
                case "60": //Goods Issue
                    str = "IGE1";
                    break;

                case "97": //sales opportunity
                    str = "OPR1";
                    break;
            }
            return str;
        }
    }
}