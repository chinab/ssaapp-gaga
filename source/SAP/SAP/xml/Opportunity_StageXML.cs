using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAP
{
    public class Opportunity_StageXML
    {
        private String _StartDate;

        public String StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private String _ClosingDate;

        public String ClosingDate
        {
            get { return _ClosingDate; }
            set { _ClosingDate = value; }
        }
        private int _SalesEmployee;

        public int SalesEmployee
        {
            get { return _SalesEmployee; }
            set { _SalesEmployee = value; }
        }
        private int _Stage;

        public int Stage
        {
            get { return _Stage; }
            set { _Stage = value; }
        }
        private double _Percent;

        public double Percent
        {
            get { return _Percent; }
            set { _Percent = value; }
        }
        private Double _PotentialAmt;

        public Double PotentialAmt
        {
            get { return _PotentialAmt; }
            set { _PotentialAmt = value; }
        }
        private double _WeightedAmt;

        public double WeightedAmt
        {
            get { return _WeightedAmt; }
            set { _WeightedAmt = value; }
        }

        private String _DocType;

        public String DocType
        {
            get { return _DocType; }
            set { _DocType = value; }
        }

        private String _ShowBP;

        public String ShowBP
        {
            get { return _ShowBP; }
            set { _ShowBP = value; }
        }

        private int _DocNo;

        public int DocNo
        {
            get { return _DocNo; }
            set { _DocNo = value; }
        }
        public Opportunity_StageXML(String StartDate, String EndDate, int Stage, double Percent,int DocNo
                            ,String ShowBP, String DocType, double PotentialAmt,int SalesEmployee, Double WeightedAmt)
        {
            this._StartDate = String.Format("{0:yyyyMMdd}", DateTime.Parse(StartDate));
            this._ClosingDate = String.Format("{0:yyyyMMdd}", DateTime.Parse(EndDate)); 
            this._Stage = Stage;
            this._SalesEmployee = SalesEmployee;
            this._Percent = Percent;
            this._PotentialAmt = PotentialAmt;
            this._WeightedAmt = WeightedAmt;
            this._DocType = DocType;
            this._DocNo = DocNo;
            this._ShowBP = ShowBP;
        }

        public Opportunity_StageXML()
        {
 
        }
    }
}