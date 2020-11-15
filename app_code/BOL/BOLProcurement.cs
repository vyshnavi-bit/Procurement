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
/// Summary description for BOLProcurement
/// </summary>
public class BOLProcurement
{
    //WEIGHER
    private int _tid;
    private int _agentid;
    private DateTime _wdate;
    private string _session;
    private double _milkliter;
    private string _centreid;
    private int _routeid;
    private int _noofcans;
    private double _milkkg;
    private string _cmpid;
    private string _ratechart;
    private string _milknature;
    //sample id as _sno
    private int _sno;
    private string _sampleno;

    private DateTime _milktipstarttime;
    private DateTime _milktipendtime;
    private int _weigheruserid;

    //ANALYZER
    private double _fat;
    private double _snf;
    private double _rate;
    private double _amount;
    private double _clr;
    //comrate [_netrate]
    private double _netrate;
    private double _fatkg;
    private double _snfkg;
    private int _analyzeruserid;

    //TEMP 
    private string _agentname;
    private double _netamount;
    private double _commamnt;
    private double _bonusamnt;
    private int _status;

    //Vehicle
    private int _truck_id;
	public BOLProcurement()
	{
        _tid = 0;
        _agentid = 0;
        _agentname = string.Empty;
        _centreid = string.Empty;
        _milkkg = 0;
        _wdate = System.DateTime.Now;
        _rate = 0;
        _session = string.Empty;
        _milkliter = 0;
        _fat = 0;
        _snf = 0;
        _amount = 0;
        _sampleno = string.Empty;
        _sno = 0;
        _netrate = 0;
        _fatkg = 0;
        _snfkg = 0;
        _clr = 0;
        _netamount = 0;
        _noofcans = 0;
        _routeid = 0;
        _cmpid = string.Empty;
        _truck_id = 0;
        _status = 0;
	}
    public string rate_Chart
    {
        get
        {
            return _ratechart;
        }
        set
        {
            this._ratechart = value;
        }
    }
    public string Company_ID
    {
        get
        {
            return _cmpid;
        }
        set
        {
            this._cmpid= value;
        }
    }
    public int Route_ID
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
    public string milk_Nature
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
    public double comm_Amnt
    {
        get
        {
            return _commamnt;
        }
        set
        {
            this._commamnt = value;
        }
    }
    public double bonus_Amnt
    {
        get
        {
            return _bonusamnt;
        }
        set
        {
            this._bonusamnt = value;
        }
    }
    public int AgentID
    {
        get
        {
            return this._agentid;
        }
        set
        {
            this._agentid = value;
        }
    }
    public string AgentName
    {
        get
        {
            return this._agentname;
        }
        set
        {
            this._agentname = value;
        }
    }
    public string CentreID
    {
        get
        {
            return this._centreid;
        }
        set
        {
            this._centreid=value;
        }
    }
    public double Milk_Kg
    {
        get
        {
            return this._milkkg;
        }
        set
        {
            this._milkkg = value;
        }
    }
    public DateTime WDate
    {
        get
        {
            return this._wdate;
        }
        set
        {
            this._wdate = value;
        }
    }
    public string Session
    {
        get
        {
            return this._session;
        }
        set
        {
            this._session=value;
        }
    }
    public double rate
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
    public int no_Of_Cans
    {
        get
        {
            return _noofcans;
        }
        set
        {
            this._noofcans = value;
        }
    }
    public double milk_Ltr
    {
        get
        {
            return _milkliter;
        }
        set
        {
            this._milkliter = value;
        }
    }
    public double fat
    {
        get
        {
            return _fat;
        }
        set
        {
            this._fat = value;
        }
    }
    public double snf
    {
        get
        {
            return _snf;
        }
        set
        {
            this._snf = value;
        }
    }
    public double amount
    {
        get
        {
            return _amount;
        }
        set
        {
            this._amount = value;
        }
    }
    public string sample_No
    {
        get
        {
            return _sampleno;
        }
        set
        {
            this._sampleno = value;
        }
    }
    public int s_No
    {
        get
        {
            return _sno;
        }
        set
        {
            this._sno = value;
        }
    }
    public double Netrate
    {
        get
        {
            return _netrate;
        }
        set
        {
            this._netrate = value;
        }
    }
    public double FatKg
    {
        get
        {
            return _fatkg;
        }
        set
        {
            this._fatkg = value;
        }
    }
    public double SnfKg
    {
        get
        {
            return _snfkg;
        }
        set
        {
            this._snfkg = value;
        }
    }
    public double Clr
    {
        get
        {
            return _clr;
        }
        set
        {
            this._clr = value;
        }
    }
    public double NetAmount
    {
        get
        {
            return _netamount;
        }
        set
        {
            this._netamount = value;
        }
    }
    public DateTime Milktipstarttime
    {
        get
        {
            return _milktipstarttime;
        }
        set
        {
            this._milktipstarttime=value;   
        }
    }
    public DateTime Milktipendtime
    {
        get
        {
            return _milktipendtime;
        }
        set
        {
            this._milktipendtime = value;
        }
    }
    public int Weigheruser
    {
        get
        {
            return _weigheruserid;
        }
        set 
        {
            this._weigheruserid = value;
        }
    }
    public int Analyzeruser
    {
        get
        {
            return _analyzeruserid;
        }
        set
        {
            this._analyzeruserid = value;
        }
    }
    public int Truck_id
    {
        get
        {
            return this._truck_id;
        }
        set
        {
            this._truck_id = value;
        }
    }
    public int  STATUS
    {
        get
        {
            return this._status;
        }
        set
        {
            this._status= value;
        }
    }
}
