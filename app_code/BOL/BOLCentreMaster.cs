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
/// Summary description for BOLCentreMaster
/// </summary>
public class BOLCentreMaster
{
    private int _routeid;
    private int _agent_id;
    private int _centrecode;
    private string _centrename;
    private int _stateid;
    private int _producercode;
    private string _producername;
    //private string _ratechart;
    private DateTime _registereddate;
    private bool _status;
    private int _plantcode;
    private int _companycode;
	public BOLCentreMaster()
	{
        _routeid = 0;
        _agent_id = 0;
        _centrecode = 0;
        _centrename = string.Empty;
        _stateid = 0;
        _producercode = 0;
        _producername = string.Empty;
       // _ratechart = string.Empty;
        _registereddate = System.DateTime.Now;
        _status = false;
        _companycode = 0;
        _plantcode = 0;
		//
		// TODO: Add constructor logic here
		//
	}
    public int RouteId
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
    public int Agent_id
    {
        get
        {
            return _agent_id;
        }
        set
        {
            this._agent_id = value;
        }
    }
    public int Centrecode
    {
        get
        {
            return _centrecode;
        }
        set
        {
            this._centrecode = value;
        }
    }
    public string CentreName
    {
        get
        {
            return _centrename;
        }
        set
        {
            this._centrename = value;
        }
    }
    public int State_id
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
    public int ProducerCode
    {
        get
        {
            return _producercode;
        }
        set
        {
            this._producercode = value;
        }
    }
    public string Producername
    {
        get
        {
            return _producername;
        }
        set
        {
            this._producername = value;
        }
    }
    //public string Ratechart
    //{
    //    get
    //    {
    //        return _ratechart;
    //    }
    //    set
    //    {
    //        this._ratechart = value;
    //    }
    //}
    public DateTime RegisteredDate
    {
        get
        {
            return _registereddate;
        }
        set
        {
            this._registereddate = value;
        }
    }
    public bool status
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
}
