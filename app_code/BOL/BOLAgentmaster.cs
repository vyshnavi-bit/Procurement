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
/// Summary description for BOLAgentmaster
/// </summary>
public class BOLAgentmaster
{
    private int _agentid;
    private Double _caramt;
    private int _agenttype;
    private string _agentname;
    private int _routeid;
    private DateTime _agdate;


    private int _Companycode;
    private string _centreid;
    private string _Bankid;
    private string _Paymentmode;
    private string _AgentaccountNo;
    private string _AgentMobile;
    private string _milktype;

    private string _ifsccode;
    private Double _splbonusamount;


    public BOLAgentmaster()
    {

        _agentid = 0;
        _agentname = string.Empty;
        _centreid = string.Empty;
        _agdate = System.DateTime.Now;
        _agenttype = 0;
        _caramt = 0.0;
        _routeid = 0;
        _Companycode = 0;
        _Bankid = string.Empty;
        _Paymentmode = string.Empty;
        _AgentaccountNo = string.Empty;
        _AgentMobile = string.Empty;
        _milktype = string.Empty;
        _ifsccode = string.Empty;
        _splbonusamount = 0.0;
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
            this._centreid = value;
        }
    }
    public DateTime AgDate
    {
        get
        {
            return this._agdate;
        }
        set
        {
            this._agdate = value;
        }
    }
    public int AgentType
    {
        get
        {
            return this._agenttype;
        }
        set
        {
            this._agenttype = value;
        }
    }
    public Double CartageAmount
    {
        get
        {
            return this._caramt;
        }
        set
        {
            this._caramt = value;
        }
    }
    public int RouteID
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

    public int Companycode
    {
        get
        {
            return _Companycode;

        }
        set
        {
            this._Companycode = value;


        }

    }
    public string Bankid
    {
        get
        {
            return _Bankid;
        }
        set
        {
            this._Bankid = value;
        }
    }
    public string Paymentmode
    {
        get
        {
            return _Paymentmode;
        }
        set
        {
            this._Paymentmode = value;
        }
    }
    public string AgentaccountNo
    {
        get
        {
            return _AgentaccountNo;
        }
        set
        {
            this._AgentaccountNo = value;
        }
    }
    public string AGENTMOBILE
    {
        get
        {
            return this._AgentMobile;
        }
        set
        {
            this._AgentMobile = value;
        }
    }
    public string Milktype
    {
        get
        {
            return this._milktype;
        }
        set
        {
            this._milktype = value;
        }
    }
    public string Ifsccode
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
    public Double Splbonusamount
    {
        get
        {
            return this._splbonusamount;
        }
        set
        {
            this._splbonusamount = value;
        }
    }
}
