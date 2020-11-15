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
/// Summary description for BOLPlantMaster
/// </summary>
public class BOLPlantMaster
{

    private int _plantcode;
    private string _plantname;
    private string _plantaddress;
    private string  _plantphoneno;
    private int  _Companycode;
    private string _managername;
    private string _manaphoneno;
    private string _pmail;
	public BOLPlantMaster()
	{
		//
		// TODO: Add constructor logic here
		//

        _plantcode = 0;
        _plantname = string.Empty;
        _plantaddress = string.Empty;
        _plantphoneno = string.Empty;
        _Companycode = 0;
        _managername = string.Empty;
        _manaphoneno = string.Empty;
        _pmail = string.Empty;
	}
    public string Plantname
    {
        get
        {
            return _plantname;
        }
        set
        {
            this._plantname = value;
        }
    }
    public string Pmail
    {
        get
        {
            return _pmail;
        }
        set
        {
            this._pmail = value;
        }
    }
    public string Plantaddress
    {
        get
        {
            return _plantaddress;

        }
        set
        {
            this._plantaddress = value;
        }
    }
    public string plantphoneno
    {

        get
        {
            return _plantphoneno;

        }
        set
        {
            this._plantphoneno = value;
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
            return _Companycode;
        }
        set
        {
            this._Companycode = value;
        }
    }
    public string Managername
    {
        get
        {
            return _managername;

        }
        set
        {
            this._managername = value;
        }
    }
    public string Manaphoneno
    {
        get
        {
            return _manaphoneno;

        }
        set
        {
            this._manaphoneno = value;
        }
    }
}
