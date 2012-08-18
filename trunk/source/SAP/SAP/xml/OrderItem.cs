using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAP
{
    public class OrderItem
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
        private float _DiscPrcnt;

        public float DiscPrcnt
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
        private double _PriceAfVAT;

        public double PriceAfVAT
        {
            get { return _PriceAfVAT; }
            set { _PriceAfVAT = value; }
        }

        public OrderItem(String itemcode, String des, int quan, float discount,
                            String whscode, String vat, double vatprice)
        {
            this._ItemCode = itemcode;
            this._Description = des;
            this._Quantity = quan;
            this._DiscPrcnt = discount;
            this._WhsCode = whscode;
            this._VATGroup = vat;
            this._PriceAfVAT = vatprice;
        }

        public OrderItem()
        {
 
        }
    }
}