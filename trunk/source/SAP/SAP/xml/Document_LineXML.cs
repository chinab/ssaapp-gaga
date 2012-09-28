using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAP
{
    public class Document_LineXML
    {
        private String _ItemCode;

        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        private String _Description;

        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private int _Quantity;

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private double _DiscPrcnt;

        public double DiscPrcnt
        {
            get { return _DiscPrcnt; }
            set { _DiscPrcnt = value; }
        }
        private String _WhsCode;

        public String WhsCode
        {
            get { return _WhsCode; }
            set { _WhsCode = value; }
        }
        private String _VATGroup;

        public String VATGroup
        {
            get { return _VATGroup; }
            set { _VATGroup = value; }
        }
        private double _Price;

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        private String _AcctCode;

        public String AcctCode
        {
            get { return _AcctCode; }
            set { _AcctCode = value; }
        }
        public Document_LineXML(String itemcode, String des, int quan, double discount,
                            String whscode, String vat, double UnitPrice,String AcctCode)
        {
            this._ItemCode = itemcode;
            this._Description = des;
            this._Quantity = quan;
            this._DiscPrcnt = discount;
            this._WhsCode = whscode;
            this._VATGroup = vat;
            this._Price = UnitPrice;
            this._AcctCode = AcctCode;
        }

        public Document_LineXML()
        {
 
        }
    }
}