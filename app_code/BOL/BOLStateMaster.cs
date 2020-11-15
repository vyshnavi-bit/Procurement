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
/// Summary description for BOLStateMaster
/// </summary>
public class BOLStateMaster
{
    private string _stateid;
    private string _statename;
    private int _Companycode;
    private int _Plantcode;

	public BOLStateMaster()
	{
        _stateid = string.Empty;
        _statename = string.Empty;
        _Companycode = 0;
        _Plantcode = 0; 
		//
		// TODO: Add constructor logic here
		//
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
    //public int Plantcode
    //{

    //    get
    //    {

    //        return _Plantcode;


    //    }
    //    set
    //    {
    //        this._Plantcode = value;

    //    }


    //}
    public string stateId
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
    public string stateName
    {
        get
        {
            return _statename;
        }
        set
        {
            this._statename = value;
        }
    }
   
}
