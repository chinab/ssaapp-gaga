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

        private static MasterData MD;
        private static DataSet ds;
        private CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
        
        public GeneralFunctions()
        {

        }

        public GeneralFunctions(string asUserName)
        {
            MD = new MasterData();
            ds = MD.GetDisplaySetting(asUserName);
        }

        #region Properties
        public string DateFormatString
        {
            get { return "MM/dd/yyyy"; }
        }
        public string DecSep
        {
            get { return (ds.Tables[0].Rows[0]["DecSep"] == null ? "." : ds.Tables[0].Rows[0]["DecSep"].ToString()); }
        }

        public string ThousSep
        {
            get { return (ds.Tables[0].Rows[0]["ThousSep"] == null ? "," : ds.Tables[0].Rows[0]["ThousSep"].ToString()); }
        }

        public int SumDec
        {
            get { return (ds.Tables[0].Rows[0]["SumDec"] == null ? 0 : int.Parse(ds.Tables[0].Rows[0]["SumDec"].ToString())); }
        }

        public int QtyDec
        {
            get { return (ds.Tables[0].Rows[0]["QtyDec"] == null ? 0 : int.Parse(ds.Tables[0].Rows[0]["QtyDec"].ToString())); }
        }

        public int PriceDec
        {
            get { return (ds.Tables[0].Rows[0]["PriceDec"] == null ? 0 : int.Parse(ds.Tables[0].Rows[0]["PriceDec"].ToString())); }
        }

        public int PercentDec
        {
            get { return (ds.Tables[0].Rows[0]["PercentDec"] == null ? 2 : int.Parse(ds.Tables[0].Rows[0]["PercentDec"].ToString())); }
        }

        public int RateDec
        {
            get { return (ds.Tables[0].Rows[0]["RateDec"] == null ? 2 : int.Parse(ds.Tables[0].Rows[0]["RateDec"].ToString())); }
        }
        #endregion

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
                case "30": //Journal Entry
                    str = "OJDT";
                    break;
                case "191": //Service call
                    str = "OSCL";
                    break;
                case "33": //Activity
                    str = "OCLG";
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
                case "30": //Journal Entry
                    str = "JDT1";
                    break;
                case "191": //Service call
                    str = "OSCL";
                    break;
                case "33": //Activity
                    str = "OCLG";
                    break;
            }
            return str;
        }
        public string BuildKeepColumnStr(DataTable dt)
        {
            string str = "";
            foreach (DataColumn column in dt.Columns)
            {
                str = str + column.ColumnName + ";";
            }
            return str;
        }
        public DataTable ConvertDate_RemoveCols(DataTable dt, string KeepColumns)
        {
            DataTable dt1 = dt;
            Array arr = KeepColumns.Split(';');
            foreach (var column in dt1.Columns.Cast<DataColumn>().ToArray())
            {
                if (Array.IndexOf(arr, column.ColumnName) < 0)
                {//-------neu ko nam trong danh sach column giu lai, thi delete-----------------
                    dt1.Columns.Remove(column);
                }
                else
                {
                    if (column.DataType == typeof(DateTime))
                    {
                        //--------------neu kieu du lieu la ngay, thi convert qua string------------
                        foreach (DataRow row in dt1.Rows)
                        {
                            DateTime d = DateTime.Parse(row[column].ToString());
                            dt1.Columns.Remove(column.ColumnName);
                            dt1.Columns.Add(column.ColumnName, typeof(string));
                            row[column.ColumnName] = String.Format("{0:yyyyMMdd}", d);
                        }
                    }
                }
            }

            return dt1;
        }

        #region Transfer from Obj to Double
        public Double Object2Double(Object Obj)
        {
            double result = 0.0;
            try
            {
                if (Obj != null)
                {
                    if (Obj.ToString().IndexOf(ci.NumberFormat.NumberGroupSeparator) > 0)
                        Obj = Obj.ToString().Replace(ci.NumberFormat.NumberGroupSeparator, ci.NumberFormat.NumberDecimalSeparator);
                    result = Double.Parse(Obj.ToString(), ci);
                }
            }
            catch (Exception ex)
            {
                result = 0.0;
            }
            return result;        
        }
        #endregion

        #region Transfer from Obj & Display Setting Name
        public Double Object2Double(Object Obj, string attName)
        {
            double result=0.0;
            try
            {
                if (Obj != null)
                {
                    if (Obj.ToString().IndexOf(ci.NumberFormat.NumberGroupSeparator) > 0)
                        Obj = Obj.ToString().Replace(ci.NumberFormat.NumberGroupSeparator, ci.NumberFormat.NumberDecimalSeparator);
                    return result = Math.Round(Double.Parse(Obj.ToString(), ci), GetNumDecimals(attName));
                }
            }
            catch (Exception ex)
            {
                result = 0.0;
            }
            return 0.0;
        }
        #endregion

        #region Get Number Decimal of Display Setting Name
        public int GetNumDecimals(string attName)
        {
            switch (attName)
            {
                case "SumDec":
                    return SumDec;
                case "PriceDec":
                    return PriceDec;
                case "QtyDec":
                    return QtyDec;
                case "PercentDec":
                    return PercentDec;
                case "RateDec":
                    return RateDec;
                default: return 0;
            }
        }
        #endregion

        #region FormatNumeric
        public string FormatNumeric(string strValor, string asNumDecimales)
        {
            int intNumDecimales = GetNumDecimals(asNumDecimales);

            string NumberGroupSeparator = this.ThousSep, NegativeSign = "-";
            string PercentDecimalSeparator = this.DecSep, NumberDecimalSeparator = this.DecSep;

            strValor = (Math.Round(Double.Parse(strValor), intNumDecimales)).ToString();

            string strAux = null;
            string strComas = string.Empty;
            string strPuntos = null;

            if (strValor.Length == 0) return "";
            strValor = strValor.Replace(NumberGroupSeparator, NumberDecimalSeparator);
            if (strValor.Contains(NumberDecimalSeparator))
            {
                strAux = strValor.Substring(0, strValor.LastIndexOf(NumberDecimalSeparator));
                strComas = strValor.Substring(strValor.LastIndexOf(NumberDecimalSeparator) + 1);
            }
            else
            {
                strAux = strValor;
            }

            if (strAux.Substring(0, 1) == NegativeSign)
            {
                strAux = strAux.Substring(1);
            }

            strPuntos = strAux;
            strAux = "";
            while (strPuntos.Length > 3)
            {
                strAux = NumberGroupSeparator + strPuntos.Substring(strPuntos.Length - 3, 3) + strAux;
                strPuntos = strPuntos.Substring(0, strPuntos.Length - 3);
            }
            if (intNumDecimales > 0)
            {
                if (strValor.Contains(PercentDecimalSeparator))
                {
                    strComas = PercentDecimalSeparator + strValor.Substring(strValor.LastIndexOf(PercentDecimalSeparator) + 1);
                    if (strComas.Length > intNumDecimales)
                    {
                        strComas = strComas.Substring(0, intNumDecimales + 1);
                    }

                }
            }
            strAux = strPuntos + strAux + strComas;

            return strAux;
        }
        #endregion

        #region Reset format numeric functions
        public string ResetFormatNumeric(string strValor)
        {
            return strValor.Replace(ThousSep, "");
        }

        public DataTable ResetFormatNumeric(DataTable dt, Array arr)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach(string ls in arr)
                {
                    row[ls] = ResetFormatNumeric(row[ls].ToString());
                }
            }
            return dt;
        }
        #endregion
    }
}