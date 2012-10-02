using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;
using System.Collections;
using System.Globalization;

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
        //DateFormat
        //DateSep

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
        public string Puntos(string strValor, int intNumDecimales)
        {
            MasterData MD = new MasterData();
            DataSet ds = MD.GetDisplaySetting();



            CultureInfo cf = new CultureInfo("en-GB");
            string strAux = null;
            string strComas = string.Empty;
            string strPuntos = null;

            if (strValor.Length == 0) return "";
            strValor = strValor.Replace(cf.NumberFormat.NumberGroupSeparator, "");
            if (strValor.Contains(cf.NumberFormat.NumberDecimalSeparator))
            {
                strAux = strValor.Substring(0, strValor.LastIndexOf(cf.NumberFormat.NumberDecimalSeparator));
                strComas = strValor.Substring(strValor.LastIndexOf(cf.NumberFormat.NumberDecimalSeparator) + 1);
            }
            else
            {
                strAux = strValor;
            }

            if (strAux.Substring(0, 1) == cf.NumberFormat.NegativeSign)
            {
                strAux = strAux.Substring(1);
            }

            strPuntos = strAux;
            strAux = "";
            while (strPuntos.Length > 3)
            {
                strAux = cf.NumberFormat.NumberGroupSeparator + strPuntos.Substring(strPuntos.Length - 3, 3) + strAux;
                strPuntos = strPuntos.Substring(0, strPuntos.Length - 3);
            }
            if (intNumDecimales > 0)
            {
                if (strValor.Contains(cf.NumberFormat.PercentDecimalSeparator))
                {
                    strComas = cf.NumberFormat.PercentDecimalSeparator + strValor.Substring(strValor.LastIndexOf(cf.NumberFormat.PercentDecimalSeparator) + 1);
                    if (strComas.Length > intNumDecimales)
                    {
                        strComas = strComas.Substring(0, intNumDecimales + 1);
                    }

                }
            }
            strAux = strPuntos + strAux + strComas;

            return strAux;
        }
    }
}