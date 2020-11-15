using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;



  public class BOLdeductiondet
    {
        private int _companycode;
        private int _plantcode;    

        private int _routeid;
        private int _agentid;
        private double _billadvance;
        private double _ai;
        private double _feed;
        private double _can;
        private double _recovery;
        private double _other;
        private DateTime _frdate;
        private DateTime _todate;
        private DateTime _deductiondate;
        private bool _status;
        private string _referencedate;
       
       
       
        public BOLdeductiondet()
        {
            _companycode = 0;
            _plantcode = 0;
            _routeid = 0;          
            _agentid = 0;             
            _billadvance = 0.0;
            _ai = 0.0;
            _feed = 0.0;
            _can = 0.0;
            _recovery = 0.0;
            _other = 0.0;
            _status = false;
            _deductiondate = System.DateTime.Now;
           
        
        }

        public int Companycode
        {
            get
            {
                return _companycode;
            }
            set
            {
                this._companycode = value;

            }
        }
        public int Plantcode
        {
            get
            {
                return _plantcode;
            }
            set
            {
                this._plantcode = value;
            }
        }
        public int Routeid
        {
            get
            {
                return _routeid;
            }
            set
            {
                this._routeid = value;
            }
        }
        public int Agentid
        {
            get
            {
                return _agentid;
            }
            set
            {
                this._agentid = value;
            }
        } 
            
               
        public double Billadvance
        {
            get
            {
                return _billadvance;
            }
            set
            {
                this._billadvance = value;
            }
        }
        public double Ai
        {
            get
            {
                return _ai;
            }
            set
            {
                this._ai = value;
            }
        }
        public double Feed
        {
            get
            {
                return _feed;
            }
            set
            {
                this._feed = value;
            }
        }
        public double Can
        {
            get
            {
                return _can;
            }
            set
            {
                this._can = value;
            }
        }
        public double Recovery
        {
            get
            {
                return _recovery;
            }
            set
            {
                this._recovery = value;
            }
        }
        public double others
        {
            get
            {
                return _other;
            }
            set
            {
                this._other = value;
            }
        }
        public DateTime Frdate
        {
            get
            {
                return _frdate;
            }
            set
            {
                this._frdate = value;
            }
        }
        public DateTime Todate
        {
            get
            {
                return _todate;
            }
            set
            {
                this._todate = value;
            }
        }
        public DateTime DeductionDate
        {
            get
            {
                return _deductiondate;
            }
            set
            {
                this._deductiondate = value;
            }
        }
        public bool Status
        {
            get
            {
                return _status;
            }
            set
            {
                this._status = value;
            }
        }

        public string Referencedate
        {
            get
            {
                return _referencedate;
            }
            set
            {
                this._referencedate = value;
            }
        }

    }
