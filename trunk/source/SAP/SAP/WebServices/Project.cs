using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace SAP.WebServices
{
    public class Project
    {
        private string _PrjCode;

        public string PrjCode
        {
            get { return _PrjCode; }
            set { _PrjCode = value; }
        }

        private string _PrjName;

        public string PrjName
        {
            get { return _PrjName; }
            set { _PrjName = value; }
        }

        public static List<Project> extractFromDataSet(DataTable table)
        {
            List<Project> list = new List<Project>();
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    Project proj = new Project();
                    proj.PrjCode = row[0].ToString();
                    proj.PrjName = row[1].ToString();
                    list.Add(proj);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return list;
        }
    }

}