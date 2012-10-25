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
       public String ToXMLStringFromDS(String ObjType, DataTable dtHeader, DataTable dtLine, String RemoveColums)
        {
            DataTable ds = dtHeader;
            DataTable ds1 = dtLine;
            Array arr=RemoveColums.Split(';');
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
                                        if (Array.IndexOf(arr, column.ColumnName) < 0)
                                        {
                                            if (column.ColumnName != "No")//phan lon cac table deu co column No nay
                                            {
                                                writer.WriteStartElement(column.ColumnName); //Write Tag
                                                {
                                                    writer.WriteString(row[column].ToString());
                                                }
                                                writer.WriteEndElement();
                                            }
                                        }
                                        
                                    }
                                }
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                        #endregion

                       #region LineXML 1
                        if (ds1 != null)
                        {
                            writer.WriteStartElement(gf.GetLineTableTag(ObjType, 1));
                            {
                                foreach (DataRow row in ds1.Rows)
                                {
                                    writer.WriteStartElement("row");
                                    {
                                        foreach (DataColumn column in ds1.Columns)
                                        {
                                            if (Array.IndexOf(arr, column.ColumnName) < 0)
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
                                    }
                                    writer.WriteEndElement();
                                }
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