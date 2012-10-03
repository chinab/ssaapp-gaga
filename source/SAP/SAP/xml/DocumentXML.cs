using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Data;

namespace SAP
{
    public class DocumentXML
    {
        #region Will be deleted
        private const String ROOT_ELEMENT = "BOM";
        private const String BO_ELEMENT = "BO";
        private const String ADMINFO_ELEMENT = "AdmInfo";
        private const String OBJECT_ELEMENT = "Object";
        private const String ROW_ELEMENT = "row";

        private const String DOCDATE_ELEMENT = "DocDate";
        private const String DOCDUEDATE_ELEMENT = "DocDueDate";
        private const String TAXDATE_ELEMENT = "TaxDate";
        private const String CARDCODE_ELEMENT = "CardCode";
        private const String CARDNAME_ELEMENT = "CardName";
        private const String USERID_ELEMENT = "U_UserID";
        private const String DOCTYPE_ELEMENT = "DocType";

        private const String OWNERCODE_ELEMENT = "OwnerCode";
        private const String BUYER_ELEMENT = "SlpCode";
        private const String PROJECT_ELEMENT = "Project";
        private const String SHIPPING_ELEMENT = "TrnspCode";
        private const String INDICATOR_ELEMENT = "Indicator";
        private const String PAYMENTTERM_ELEMENT = "GroupNum";

        private const String ITEMCODE_ELEMENT = "ItemCode";
        private const String DES_ELEMENT = "Dscription";
        private const String QUANTITY_ELEMENT = "Quantity";
        private const String DISPERCENT_ELEMENT = "DiscPrcnt";
        private const String WHSCODE_ELEMENT = "WhsCode";
        private const String VATGRP_ELEMENT = "VATGroup";
        private const String UNITPRICE_ELEMENT = "Price";

        private const String CC_ELEMENT = "OcrCode";
        private const String CC1_ELEMENT = "OcrCode1";
        private const String CC2_ELEMENT = "OcrCode2";
        private const String CC3_ELEMENT = "OcrCode3";
        private const String CC4_ELEMENT = "OcrCode4";
        private const String GLACCT_ELEMENT = "AcctCode";

        
        private String _OPOR_ELEMENT;

        public String OPOR_ELEMENT
        {
            get { return _OPOR_ELEMENT; }
            set { _OPOR_ELEMENT = value; }
        }

        private String _POR1_ELEMENT;

        public String POR1_ELEMENT
        {
            get { return _POR1_ELEMENT; }
            set { _POR1_ELEMENT = value; }
        }

        private String _AdmInfo;

        public String AdmInfo
        {
            get { return _AdmInfo; }
            set { _AdmInfo = value; }
        }

        private String _UserID;
        public String UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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

        private List<Document_LineXML> _OrderItems;

        public DocumentXML(String adminfo, String docdate, String docduedate, String taxdate,
                                String cardcode, String cardname,String UserID)
        {
            this._AdmInfo = adminfo;
            switch(adminfo)
            {
                case "19": //AP Credit
                    this.OPOR_ELEMENT = "ORPC";
                    this.POR1_ELEMENT = "RPC1";
                    break;
                case "20": //GRPO
                   this.OPOR_ELEMENT = "OPDN";
                   this.POR1_ELEMENT = "PDN1";
                   break;
                case "21": //Goods Return
                   this.OPOR_ELEMENT = "ORPD";
                   this.POR1_ELEMENT = "RPD1";
                   break;
                case "22": //Purchase Order
                   this.OPOR_ELEMENT = "OPOR";
                   this.POR1_ELEMENT = "POR1";
                   break;

                case "13": //AR Invoice
                   this.OPOR_ELEMENT = "OINV";
                   this.POR1_ELEMENT = "INV1";
                   break;
                case "14": //AR Credit
                   this.OPOR_ELEMENT = "ORIN";
                   this.POR1_ELEMENT = "RIN1";
                   break;
                case "15": //Delivery
                    this.OPOR_ELEMENT = "ODLN";
                   this.POR1_ELEMENT = "DLN1";
                   break;

                case "59": //Goods Receipt
                   this.OPOR_ELEMENT = "OIGN";
                   this.POR1_ELEMENT = "IGN1";
                   break;
                case "60": //Goods Issue
                   this.OPOR_ELEMENT = "OIGE";
                   this.POR1_ELEMENT = "IGE1";
                   break;

                case "97": //sales opportunity
                   this.OPOR_ELEMENT = "OOPR";
                   this.POR1_ELEMENT = "OPR1";
                   break;
            }
            this._DocDate = String.Format("{0:yyyyMMdd}", DateTime.Parse(docdate));
            if (docduedate == "")
                docduedate = docdate;
            if (taxdate == "")
                taxdate = docdate;

            this._DocDueDate = String.Format("{0:yyyyMMdd}", DateTime.Parse(docduedate));// docduedate;
            this._TaxDate = String.Format("{0:yyyyMMdd}", DateTime.Parse(taxdate));// taxdate;
            this._CardCode = cardcode;
            this._CardName = cardname;
            this._UserID = UserID;
            _OrderItems = new List<Document_LineXML>();
        }

        public DocumentXML()
        {
            _OrderItems = new List<Document_LineXML>();
        }

        public void AddOrderItem(Document_LineXML item)
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
                writer.WriteStartElement(DocumentXML.ROOT_ELEMENT);
                {
                    writer.WriteStartElement(DocumentXML.BO_ELEMENT);
                    {
                        #region write ADMINFO_ELEMENT
                        writer.WriteStartElement(DocumentXML.ADMINFO_ELEMENT);
                        {
                            writer.WriteStartElement(DocumentXML.OBJECT_ELEMENT); // write object tag
                            {
                                writer.WriteString(this._AdmInfo);
                            }
                            writer.WriteEndElement();


                        }
                        writer.WriteEndElement();
                        #endregion

                        #region Header XML
                       // writer.WriteStartElement(PurchaseInfo.OPOR_ELEMENT); // write OPOR tag
                        writer.WriteStartElement(this.OPOR_ELEMENT); 
                        {
                            writer.WriteStartElement(DocumentXML.ROW_ELEMENT);
                            {
                                writer.WriteStartElement(DocumentXML.DOCDATE_ELEMENT);
                                {
                                    writer.WriteString(this._DocDate);
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement(DocumentXML.DOCDUEDATE_ELEMENT);
                                {
                                    writer.WriteString(this._DocDueDate);

                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement(DocumentXML.TAXDATE_ELEMENT);
                                {
                                    writer.WriteString(this._TaxDate);
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement(DocumentXML.CARDCODE_ELEMENT);
                                {
                                    writer.WriteString(this._CardCode);
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement(DocumentXML.CARDNAME_ELEMENT);
                                {
                                    writer.WriteString(this._CardName);
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement(DocumentXML.USERID_ELEMENT);
                                {
                                    writer.WriteString(this.UserID);
                                }
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        #endregion

                        #region Lines XML
                        {
                            if (_OrderItems != null)
                            {
                                writer.WriteStartElement(this.POR1_ELEMENT);
                                {
                                    for (int i = 0; i < _OrderItems.Count; i++)
                                    {

                                        writer.WriteStartElement(DocumentXML.ROW_ELEMENT);
                                            {
                                                writer.WriteStartElement(DocumentXML.ITEMCODE_ELEMENT); //write item _PrjCode
                                                {
                                                    writer.WriteString(this._OrderItems[i].ItemCode);

                                                }
                                                writer.WriteEndElement();

                                                writer.WriteStartElement(DocumentXML.DES_ELEMENT); //write Dscription
                                                {
                                                    writer.WriteString(this._OrderItems[i].Description);

                                                }
                                                writer.WriteEndElement();

                                                writer.WriteStartElement(DocumentXML.QUANTITY_ELEMENT); //write Quantity
                                                {
                                                    writer.WriteString(Convert.ToString(this._OrderItems[i].Quantity));

                                                }
                                                writer.WriteEndElement();

                                                writer.WriteStartElement(DocumentXML.DISPERCENT_ELEMENT); //write DiscPrcnt
                                                {
                                                    writer.WriteString(Convert.ToString(this._OrderItems[i].DiscPrcnt));

                                                }
                                                writer.WriteEndElement();

                                                writer.WriteStartElement(DocumentXML.WHSCODE_ELEMENT); //write WhsCode
                                                {
                                                    writer.WriteString(this._OrderItems[i].WhsCode);

                                                }
                                                writer.WriteEndElement();

                                                writer.WriteStartElement(DocumentXML.VATGRP_ELEMENT); //write VATGroup
                                                {
                                                    writer.WriteString(this._OrderItems[i].VATGroup);

                                                }
                                                writer.WriteEndElement();

                                                writer.WriteStartElement(DocumentXML.UNITPRICE_ELEMENT); //Unit Price
                                                {
                                                    writer.WriteString(Convert.ToString(this._OrderItems[i].Price));

                                                }
                                                writer.WriteEndElement();

                                                if (this._OrderItems[i].AcctCode != "")
                                                {
                                                    writer.WriteStartElement(DocumentXML.GLACCT_ELEMENT); //Unit Price
                                                    {
                                                        writer.WriteString(Convert.ToString(this._OrderItems[i].AcctCode));

                                                    }
                                                    writer.WriteEndElement();
                                                }
                                                
                                            }
                                        writer.WriteEndElement();                                    
                                }
                                }
                                writer.WriteEndElement();
                            }
                        }
                        #endregion
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndDocument();

            writer.Flush();

            return XmlString.ToString();
        }
        #endregion

        public String ToXMLStringFromDS(String ObjType, DataTable ds, DataTable ds1,String RemoveColums)
        {
            //Xoa het cac column ko su dung trong XML (nhung column su dung tam de luu du lieu)
            Array arr=RemoveColums.Split(';');

           // int i=0;
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                foreach (DataColumn column in ds.Columns)
                { 
                    if (column.ColumnName==arr.GetValue(i).ToString())
                    {
                        ds.Columns.RemoveAt(j);
                        break;
                    }
                    j = j + 1;
                }

                j = 0;
                foreach (DataColumn column in ds1.Columns)
                {
                    if (column.ColumnName == arr.GetValue(i).ToString())
                    {
                        ds1.Columns.RemoveAt(j);
                        break;
                    }
                    j = j + 1;
                }
            }
         

            GeneralFunctions gf = new GeneralFunctions();
            StringBuilder XmlString = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(XmlString);
            writer.WriteStartDocument();
            {
                writer.WriteStartElement("BOM");
                {
                    writer.WriteStartElement("BO");
                    {
                        #region write ADMINFO_ELEMENT
                        writer.WriteStartElement("AdmInfo");
                        {
                            writer.WriteStartElement("Object");
                            {
                                writer.WriteString(ObjType);
                            }
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        #endregion

                        #region Header XML
                        foreach (DataRow row in ds.Rows)
                        {
                            writer.WriteStartElement(gf.GetHeaderTableTag(ObjType));
                            {
                                writer.WriteStartElement("row");
                                {
                                    foreach (DataColumn column in ds.Columns)
                                    {
                                        //if (column.ColumnName!="No")
                                        //{
                                            writer.WriteStartElement(column.ColumnName); //Write Tag
                                            {
                                                writer.WriteString(row[column].ToString());
                                            }
                                            writer.WriteEndElement();
                                        //}
                                    }
                                }
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        #endregion

                       #region LineXML 1
                       foreach (DataRow row in ds1.Rows)
                        {
                            writer.WriteStartElement(gf.GetLineTableTag(ObjType,1));
                            {
                                writer.WriteStartElement("row");
                                {
                                    foreach (DataColumn column in ds1.Columns)
                                    {
                                        if (column.ColumnName != "No")
                                        {
                                            writer.WriteStartElement(column.ColumnName); //Write Tag
                                            {
                                                writer.WriteString(row[column].ToString());
                                            }
                                            writer.WriteEndElement();
                                        }
                                    }
                                }
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        #endregion
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndDocument();

            writer.Flush();

            return XmlString.ToString();
        }
       
    }
}