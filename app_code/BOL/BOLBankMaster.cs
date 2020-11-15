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
/// Summary description for BOLBankMaster
/// </summary>
public class BOLBankMaster
{
    //private string _tid;
    private string _bankid;
    private string _bankname;
    private string _branchname;
    private string _branchcode;
    private string _ifsccode;
    private string _phonenumber;
    //private int _phonenumber;
   // private string _Companycode;
    private int _Companycode;
    //private string _Plantcode;
    private int _Plantcode;
	public BOLBankMaster()
	{
        //_tid = string.Empty;
       
        _bankname = string.Empty;
        _branchname = string.Empty;
        _bankid = string.Empty;
        _branchcode = string.Empty;
        _ifsccode = string.Empty;
        _phonenumber = string.Empty;
        _Companycode =0;
        _Plantcode =0;
       
		//
		// TODO: Add constructor logic here
		//
	}
    public string BankName
    {
        get
        {
            return _bankname;
        }
        set
        {
            this._bankname = value;
        }
    }

    public string BranchName
    {
        get
        {
            return _branchname;
        }
        set
        {
            this._branchname = value;
        }
    }
    public string BranchCode
    {
        get
        {
            return _branchcode;
        }
        set
        {
            this._branchcode = value;
        }
    }
    public string Ifsccode
    {
        get
        {
            return _ifsccode;
        }
        set
        {
            this._ifsccode = value;
        }
    }
    public string BankID
    {
        get
        {
            return _bankid;
        }
        set
        {
            this._bankid = value;
        }
    }
    public  string Phonenumber
    {

        get
        {
            return _phonenumber;
        }
        set
        {
            this._phonenumber = value;

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
    public int Plantcode
    {

        get
        {

            return _Plantcode;


        }
        set
        {
            this._Plantcode = value;

        }


    }
    
}
