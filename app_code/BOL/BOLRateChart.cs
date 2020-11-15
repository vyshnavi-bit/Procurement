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
/// Summary description for BOLRateChart
/// </summary>
public class BOLRateChart
{

    private int _tid;
    private string _charttype;
    private string _milknature;
    private int _stateid;
    private double _minfat;
    private double _minsnf;
    private DateTime _fromdate;
    private DateTime _todate;

    private string _chartname;
    private double _fromrangevalue;
    private double _torangrvalue;
    private double _rate;
    private double _commissionamount;
    private double _bonusamount;
    private string _companycode;
    private string _plantcode;
    private int _Flag;
    private string _sess;

    public BOLRateChart()
    {
        _tid = 0;
        _charttype = string.Empty;
        _milknature = string.Empty;
        _stateid = 0;
        _minfat = 0;
        _minsnf = 0;

        _chartname = string.Empty;
        _fromrangevalue = 0;
        _torangrvalue = 0;
        _rate = 0;
        _commissionamount = 0;
        _bonusamount = 0;
        _Flag = 0;
        _sess = string.Empty;

    }
    // RATE CHART DETAILS

    public int Tid
    {
        get
        {
            return _tid;
        }
        set
        {
            this._tid = value;
        }
    }
    public string Charttype
    {
        get
        {
            return _charttype;
        }
        set
        {
            this._charttype = value;
        }
    }
    public string Milknature
    {
        get
        {
            return _milknature;
        }
        set
        {
            this._milknature = value;
        }
    }
    public int Stateid
    {
        get
        {
            return _stateid;
        }
        set
        {
            this._stateid = value;
        }
    }
    public double Minfat
    {
        get
        {
            return _minfat;
        }
        set
        {
            this._minfat = value;
        }
    }
    public double Minsnf
    {
        get
        {
            return _minsnf;
        }
        set
        {
            this._minsnf = value;
        }
    }
    public DateTime Fromdate
    {
        get
        {
            return _fromdate;
        }
        set
        {
            this._fromdate = value;
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


    //RATE CHART RANGE VALUE

    public string Chartname
    {
        get
        {
            return _chartname;
        }
        set
        {
            this._chartname = value;
        }
    }
    public double Fromrangevalue
    {
        get
        {
            return _fromrangevalue;
        }
        set
        {
            this._fromrangevalue = value;
        }
    }
    public double Torangrvalue
    {
        get
        {
            return _torangrvalue;
        }
        set
        {
            this._torangrvalue = value;
        }
    }
    public double Rate
    {
        get
        {
            return _rate;
        }
        set
        {
            this._rate = value;
        }
    }
    public double Commissionamount
    {
        get
        {
            return _commissionamount;
        }
        set
        {
            this._commissionamount = value;
        }
    }
    public double Bonusamount
    {
        get
        {
            return _bonusamount;
        }
        set
        {
            this._bonusamount = value;
        }
    }
    public int Flag
    {
        get
        {
            return _Flag;
        }
        set
        {
            this._Flag = value;
        }
    }
    public string Companycode
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
    public string Plantcode
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

    public string Sess
    {
        get
        {
            return _sess;
        }
        set
        {
            this._sess = value;
        }
    }
}
