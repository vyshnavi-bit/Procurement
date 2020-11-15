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
/// Summary description for BOLPlantwiseRatechart
/// </summary>
public class BOLPlantwiseRatechart
{
    private int _companycode;
    private int _plantcode;

    private string _chartName;
    private string _buffchartName;
   //Routewise Ratechart
    private int _routeId;
  // Agentwise Ratechart
    private int _agentId;
    private DateTime _curtimestamp;
  
  

	public BOLPlantwiseRatechart()
	{
        _companycode = 0;
        _plantcode = 0;
        _chartName = string.Empty;
        //
        _routeId = 0;
        //
        _agentId = 0;
        
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
    public string ChartName
    {
        get
        {
            return this._chartName;
        }
        set
        {
            this._chartName = value;
        }
    }
    public string BuffchartName
    {
        get
        {
            return this._buffchartName;
        }
        set
        {
            this._buffchartName = value;
        }
    }

    public int RouteId
    {
        get
        {
            return this._routeId;
        }
        set
        {
            this._routeId = value;
        }
    }

    public int AgentId
    {
        get
        {
            return this._agentId;
        }
        set
        {
            this._agentId = value;
        }
    }
    public DateTime Curtimestamp
    {
        get
        {
            return this._curtimestamp;
        }
        set
        {
            this._curtimestamp = value;
        }
    }

}
