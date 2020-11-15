using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BOLvehicle
/// </summary>
public class BOLvehicle
{
    private int _companycode;
    private int _plantcode;
    private string _vehicleno;
    private int _truckid;
    private string _truckname;
    private string _phoneno;
    private int _bankid;
    private string _ifsccode;
    private string _accountnum;
    private double _ltrcost;
    private int _routeid;
    private double _distance;
    private int _sessionid;
    private DateTime _pdate;

    private double _vtsdistance;
    private double _admindistance;

    private int _paymentmode;




	public BOLvehicle()
	{
        _companycode = 0;
        _plantcode = 0;
        _vehicleno = string.Empty;
        _truckid = 0;
        _truckname = string.Empty;
        _phoneno = string.Empty;
        _bankid = 0;
        _ifsccode = string.Empty;
        _accountnum = string.Empty;
        _ltrcost = 0;
        _routeid = 0;
        _distance = 0;
        _sessionid = 0;
        _pdate = System.DateTime.Now;

        _vtsdistance = 0;
        _admindistance = 0;
        _paymentmode = 0;
	}

      public int Companycode
        {
            get
            {
                return this._companycode;
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
                return this._plantcode;
            }
            set
            {
                this._plantcode = value;
            }
        }
        public string Vehicleno
        {
            get
            {
                return this._vehicleno;
            }
            set
            {
                this._vehicleno = value;
            }
        }
        public int Truckid
        {
            get
            {
                return this._truckid;
            }
            set
            {
                this._truckid = value;
            }
        }
        public string Truckname
        {
            get
            {
                return this._truckname;
            }
            set
            {
                this._truckname = value;
            }
        }
        public string Phoneno
        {
            get
            {
                return this._phoneno;
            }
            set
            {
                this._phoneno = value;
            }
        }
        public int Bankid
        {
            get
            {
                return this._bankid;
            }
            set
            {
                this._bankid = value;
            }
        }
        public string IFSC
        {
            get
            {
                return this._ifsccode;
            }
            set
            {
                this._ifsccode = value;
            }
        }
        public string ACCOUNTNUM
        {
            get
            {
                return this._accountnum;
            }
            set
            {
                this._accountnum = value;
            }
        }
        public double Ltrcost
        {
            get
            {
                return this._ltrcost;
            }
            set
            {
                this._ltrcost = value;
            }
        }
        public int Routeid
        {
            get
            {
                return this._routeid;
            }
            set
            {
                this._routeid = value;
            }
        }
        public double Distance
        {
            get
            {
                return this._distance;
            }
            set
            {
                this._distance = value;
            }
        }
        public int Sessionid
        {
            get
            {
                return this._sessionid;
            }
            set
            {
                this._sessionid = value;
            }
        }
        public DateTime Pdate
        {
            get
            {
                return this._pdate;
            }
            set
            {
                this._pdate = value;
            }
        }
        public double Vtsdistance
        {
            get
            {
                return this._vtsdistance;
            }
            set
            {
                this._vtsdistance = value;
            }
        }
        public double Admindistance
        {
            get
            {
                return this._admindistance;
            }
            set
            {
                this._admindistance = value;
            }
        }
        public int Paymentmode
        {
            get
            {
                return this._paymentmode;
            }
            set
            {
                this._paymentmode = value;
            }
        }

    }


