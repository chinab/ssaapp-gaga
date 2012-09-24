using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;

namespace SAP
{
    public class OpportunityXML
    {
        private const String ROOT_ELEMENT = "BOM";
        private const String BO_ELEMENT = "BO";
        private const String ADMINFO_ELEMENT = "AdmInfo";
        private const String OBJECT_ELEMENT = "Object";
        private const String ROW_ELEMENT = "row";

        private const String CARDCODE_ELEMENT = "CardCode";
        private const String SLPCODE_ELEMENT = "SlpCode";
        private const String CPRCODE_ELEMENT = "CprCode";
        private const String SOURCE_ELEMENT = "Source";
        private const String OPENDATE_ELEMENT = "OpenDate";
        private const String PREDDATE_ELEMENT = "PredDate";
        private const String POTENTIALAMT_ELEMENT = "MaxSumLoc";
        private const String USERID_ELEMENT = "U_UserID";
        private const String OWNER_ELEMENT = "Owner";
        private const String BUYER_ELEMENT = "SlpCode";
        private const String PROJECT_ELEMENT = "PrjCode";

        private const String STARTDATE_ELEMENT = "OpenDate";
        private const String CLOSINGDATE_ELEMENT = "CloseDate";
        private const String SALESEMPLOYEE_ELEMENT = "SlpCode";
        private const String STAGE_ELEMENT = "Step_Id";
        private const String PERCENT_ELEMENT = "ClosePrcnt";
        private const String POTENTIALAMTLINE_ELEMENT = "MaxSumLoc";
        private const String WEIGHTEDAMT_ELEMENT = "WtSumLoc";
        private const String DOCTYPE_ELEMENT = "ObjType";
        private const String SHOWBP_ELEMENT = "DocChkbox";
        private const String DOCNO_ELEMENT = "DocId";

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

        private String _OpenDate;

        public String OpenDate
        {
            get { return _OpenDate; }
            set { _OpenDate = value; }
        }
        private String _PredDate;

        public String PredDate
        {
            get { return _PredDate; }
            set { _PredDate = value; }
        }
        
        private String _CardCode;

        public String CardCode
        {
            get { return _CardCode; }
            set { _CardCode = value; }
        }
        private String _MaxSumLoc;

        public String MaxSumLoc
        {
            get { return _MaxSumLoc; }
            set { _MaxSumLoc = value; }
        }
        

        private List<Opportunity_StageXML> _Stages;

        public OpportunityXML(String adminfo, String opendate, String prededdate,
                                String cardcode,String UserID,String MaxSumLoc)
        {
            this._AdmInfo = adminfo;
            this.OPOR_ELEMENT = "OOPR";
            this.POR1_ELEMENT = "OPR1";
            
            this._OpenDate = String.Format("{0:yyyyMMdd}", DateTime.Parse(opendate));
            this._PredDate = String.Format("{0:yyyyMMdd}", DateTime.Parse(prededdate));// docduedate;
            this._CardCode = cardcode;
            this._UserID = UserID;
            this._MaxSumLoc=MaxSumLoc;
            _Stages = new List<Opportunity_StageXML>();
        }

        public OpportunityXML()
        {
            _Stages = new List<Opportunity_StageXML>();
        }

        public void AddStageLine(Opportunity_StageXML item)
        {
            if (_Stages != null && item != null)
            {
                _Stages.Add(item);
            }
        }

        public String ToXMLString()
        {
            StringBuilder XmlString = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(XmlString);

            writer.WriteStartDocument(); // write start doc
            {
                writer.WriteStartElement(OpportunityXML.ROOT_ELEMENT);
                {
                    writer.WriteStartElement(OpportunityXML.BO_ELEMENT);
                    {
                        #region write ADMINFO_ELEMENT
                        writer.WriteStartElement(OpportunityXML.ADMINFO_ELEMENT);
                        {
                            writer.WriteStartElement(OpportunityXML.OBJECT_ELEMENT); // write object tag
                            {
                                writer.WriteString(this._AdmInfo);
                            }
                            writer.WriteEndElement();


                        }
                        writer.WriteEndElement();
                        #endregion

                        #region write OPOR tag
                       // writer.WriteStartElement(PurchaseInfo.OPOR_ELEMENT); // write OPOR tag
                        writer.WriteStartElement(this.OPOR_ELEMENT); 
                        {
                            writer.WriteStartElement(OpportunityXML.ROW_ELEMENT);
                            {
                                writer.WriteStartElement(OpportunityXML.OPENDATE_ELEMENT);
                                {
                                    writer.WriteString(this._OpenDate);
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement(OpportunityXML.PREDDATE_ELEMENT);
                                {
                                    writer.WriteString(this._PredDate);

                                }
                                writer.WriteEndElement();

                                
                                writer.WriteStartElement(OpportunityXML.CARDCODE_ELEMENT);
                                {
                                    writer.WriteString(this._CardCode);
                                }
                                writer.WriteEndElement();

                               writer.WriteStartElement(OpportunityXML.USERID_ELEMENT);
                                {
                                    writer.WriteString(this.UserID);
                                }
                                writer.WriteEndElement();
                                
                                writer.WriteStartElement(OpportunityXML.POTENTIALAMT_ELEMENT);
                                {
                                    writer.WriteString(this._MaxSumLoc);
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
                            if (_Stages != null)
                            {
                                writer.WriteStartElement(this.POR1_ELEMENT);
                                {
                                    for (int i = 0; i < _Stages.Count; i++)
                                    {
                                        //writer.WriteStartElement(PurchaseInfo.OPOR_ELEMENT + (i + 1));
                                        //writer.WriteStartElement(PurchaseInfo.POR1_ELEMENT);

                                        writer.WriteStartElement(OpportunityXML.ROW_ELEMENT);
                                        {
                                            writer.WriteStartElement(OpportunityXML.STARTDATE_ELEMENT); //write item _PrjCode
                                            {
                                                writer.WriteString(this._Stages[i].StartDate);

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(OpportunityXML.CLOSINGDATE_ELEMENT); //write Dscription
                                            {
                                                writer.WriteString(this._Stages[i].ClosingDate);

                                            }
                                            writer.WriteEndElement();

                                            //writer.WriteStartElement(OpportunityXML.SALESEMPLOYEE_ELEMENT); //write Quantity
                                            //{
                                            //    writer.WriteString(Convert.ToString(this._Stages[i].SalesEmployee));

                                            //}
                                            //writer.WriteEndElement();

                                            writer.WriteStartElement(OpportunityXML.STAGE_ELEMENT); //write DiscPrcnt
                                            {
                                                writer.WriteString(Convert.ToString(this._Stages[i].Stage));

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(OpportunityXML.PERCENT_ELEMENT); //write WhsCode
                                            {
                                                writer.WriteString(Convert.ToString(this._Stages[i].Percent));

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(OpportunityXML.WEIGHTEDAMT_ELEMENT); //write VATGroup
                                            {
                                                writer.WriteString(Convert.ToString(this._Stages[i].WeightedAmt));

                                            }
                                            writer.WriteEndElement();

                                            writer.WriteStartElement(OpportunityXML.POTENTIALAMTLINE_ELEMENT); //Unit Price
                                            {
                                                writer.WriteString(Convert.ToString(this._Stages[i].PotentialAmt));

                                            }
                                            writer.WriteEndElement();

                                            //writer.WriteStartElement(OpportunityXML.DOCTYPE_ELEMENT); //Unit Price
                                            //{
                                            //    writer.WriteString(Convert.ToString(this._Stages[i].DocType));

                                            //}
                                            //writer.WriteEndElement();

                                            //writer.WriteStartElement(OpportunityXML.SHOWBP_ELEMENT); //Unit Price
                                            //{
                                            //    writer.WriteString(Convert.ToString(this._Stages[i].ShowBP));

                                            //}
                                            //writer.WriteEndElement();

                                            //writer.WriteStartElement(OpportunityXML.DOCNO_ELEMENT); //Unit Price
                                            //{
                                            //    writer.WriteString(Convert.ToString(this._Stages[i].DocNo));

                                            //}
                                            //writer.WriteEndElement();

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
    }
}