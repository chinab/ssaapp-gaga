﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;

namespace SAP.xml
{
    public class PurchaseInfo
    {
        private static const String ROOT_ELEMENT = "BOM";
        private static const String BO_ELEMENT = "BO";
        private static const String ADMINFO_ELEMENT = "AdmInfo";
        private static const String OBJECT_ELEMENT = "Object";
        private static const String OPOR_ELEMENT = "OPOR";
        private static const String ROW_ELEMENT = "row";
        private static const String DOCDATE_ELEMENT = "DocDate";
        private static const String DOCDUEDATE_ELEMENT = "DocDueDate";
        private static const String TAXDATE_ELEMENT = "TaxDate";
        private static const String CARDCODE_ELEMENT = "CardCode";
        private static const String CARDNAME_ELEMENT = "CardName";
        private static const String POR_ELEMENT = "POR";
        private static const String ITEMCODE_ELEMENT = "ItemCode";
        private static const String DES_ELEMENT = "Dscription";
        private static const String QUANTITY_ELEMENT = "Quantity";
        private static const String DISPERCENT_ELEMENT = "DiscPrcnt";
        private static const String WHSCODE_ELEMENT = "WhsCode";
        private static const String VATGRP_ELEMENT = "VATGroup";
        private static const String PRICEVAT_ELEMENT = "PriceAfVAT";

        private String _AdmInfo;

        public String AdmInfo
        {
            get { return _AdmInfo; }
            set { _AdmInfo = value; }
        }
        private String _DocDate;

        public String DocDate
        {
            get { return _DocDate; }
            set { _DocDate = value; }
        }
        private String _DocDueDate;

        public String DocDueDate
        {
            get { return _DocDueDate; }
            set { _DocDueDate = value; }
        }
        private String _TaxDate;

        public String TaxDate
        {
            get { return _TaxDate; }
            set { _TaxDate = value; }
        }
        private String _CardCode;

        public String CardCode
        {
            get { return _CardCode; }
            set { _CardCode = value; }
        }
        private String _CardName;

        public String CardName
        {
            get { return _CardName; }
            set { _CardName = value; }
        }

        private List<OrderItem> _OrderItems;

        public PurchaseInfo(String adminfo, String docdate, String docduedate, String taxdate,
                                String cardcode, String cardname)
        {
            this._AdmInfo = adminfo;
            this._DocDate = docdate;
            this._DocDueDate = docduedate;
            this._TaxDate = taxdate;
            this._CardCode = cardcode;
            this._CardName = cardname;
            _OrderItems = new List<OrderItem>();
        }

        public PurchaseInfo()
        {
            _OrderItems = new List<OrderItem>();
        }

        public void AddOrderItem(OrderItem item)
        {
            if (_OrderItems != null && item != null)
            {
                _OrderItems.Add(item);
            }
        }

        public String ToXMLString()
        {
            StringBuilder XmlString = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(XmlString);

            writer.WriteStartDocument(); // write start doc
            {
                writer.WriteStartElement(PurchaseInfo.ROOT_ELEMENT);
                {
                    writer.WriteStartElement(PurchaseInfo.BO_ELEMENT);
                    {
                        #region write ADMINFO_ELEMENT
                        writer.WriteStartElement(PurchaseInfo.ADMINFO_ELEMENT);
                        {
                            writer.WriteStartElement(PurchaseInfo.OBJECT_ELEMENT); // write object tag
                            {
                                writer.WriteString(this._AdmInfo);
                            }
                            writer.WriteEndElement();


                        }
                        writer.WriteEndElement();
                        #endregion

                        #region write OPOR tag
                        writer.WriteStartElement(PurchaseInfo.OPOR_ELEMENT); // write OPOR tag
                        {
                            writer.WriteStartElement(PurchaseInfo.ROW_ELEMENT);
                            {
                                writer.WriteStartElement(PurchaseInfo.DOCDATE_ELEMENT);
                                {
                                    writer.WriteString(this._DocDate);
                                    writer.WriteString(this._DocDueDate);
                                    writer.WriteString(this._TaxDate);
                                    writer.WriteString(this._CardCode);
                                    writer.WriteString(this._CardName);

                                }
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        #endregion

                        //write POR items
                        #region write POR items
                        {
                            if (_OrderItems != null)
                            {
                                for (int i = 0; i < _OrderItems.Count; i++)
                                {
                                    writer.WriteStartElement(PurchaseInfo.OPOR_ELEMENT + (i + 1));
                                    {
                                        writer.WriteStartElement(PurchaseInfo.ROW_ELEMENT);
                                        {
                                            writer.WriteStartElement(PurchaseInfo.ITEMCODE_ELEMENT); //write item code
                                            {
                                                writer.WriteString(this._OrderItems[i].ItemCode);

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(PurchaseInfo.DES_ELEMENT); //write Dscription
                                            {
                                                writer.WriteString(this._OrderItems[i].Description);

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(PurchaseInfo.QUANTITY_ELEMENT); //write Quantity
                                            {
                                                writer.WriteString(this._OrderItems[i].Quantity);

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(PurchaseInfo.DISPERCENT_ELEMENT); //write DiscPrcnt
                                            {
                                                writer.WriteString(Convert.ToString(this._OrderItems[i].DiscPrcnt));

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(PurchaseInfo.WHSCODE_ELEMENT); //write WhsCode
                                            {
                                                writer.WriteString(this._OrderItems[i].WhsCode);

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(PurchaseInfo.VATGRP_ELEMENT); //write VATGroup
                                            {
                                                writer.WriteString(this._OrderItems[i].VATGroup);

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(PurchaseInfo.PRICEVAT_ELEMENT); //write VATGroup
                                            {
                                                writer.WriteString(Convert.ToString(this._OrderItems[i].PriceAfVAT));

                                            }
                                            writer.WriteEndElement();

                                        }
                                        writer.WriteEndElement();
                                    }
                                    writer.WriteEndElement();
                                }
                            }
                        }
                        #endregion
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndDocument();

            return XmlString.ToString();
        }
    }
}