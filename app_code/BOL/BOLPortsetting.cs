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
/// Summary description for BOLPortsetting
/// </summary>
public class BOLPortsetting
{
    private int _tid;
    private string _centreid;
    private string _portname;
    private string _mtype;
    private string _baudrate;
    private string _companyid;

	public BOLPortsetting()
	{
        _tid = 0;
        _centreid = string.Empty;
        _portname = string.Empty;
        _baudrate = string.Empty;
        _companyid = string.Empty;
		//
		// TODO: Add constructor logic here
		//
	}
    public int TableID
    {
        get
        {
            return this._tid;
        }
        set
        {
            this._tid = value;
        }
    }
    public string PortName
    {
        get
        {
            return this._portname;
        }
        set
        {
            this._portname = value;
        }
    }
    public string MType
    {
        get
        {
            return this._mtype;
        }
        set
        {
            this._mtype = value;
        }
    }
    public string BaudRate
    {
        get
        {
            return this._baudrate;
        }
        set
        {
            this._baudrate = value;
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
    public string Companyid
    {
        get
        {
            return _companyid;
        }
        set
        {
            this._companyid = value;
        }
    }
}
