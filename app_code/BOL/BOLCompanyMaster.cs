using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOLCompanyMaster
/// </summary>
public class BOLCompanyMaster
{
    private int _Tid;
    private int _Companycode;
    private string _Companyname;
    private string _Companyaddress;
    private string _Companyphoneno;
    private string _Tinno;
    private string _Cstno;
    private string _cmail;
	public BOLCompanyMaster()
	{
        _Tid = 0;
        _Companycode = 0;
        _Companyname = string.Empty;
        _Companyaddress = string.Empty;
        _Companyphoneno = string.Empty;
        _Tinno = string.Empty;
        _Cstno = string.Empty;
        _cmail = string.Empty;
		//
		// TODO: Add constructor logic here
		//
	}
    public int tid
    {

        get
        {
            return _Tid;
        }
        set
        {
            this.tid = value;
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
    
    public string Companyname
    {
        get
        {
            return _Companyname;
        }
        set
        {
            this._Companyname = value;
        }
 
    }
    public string Cmail
    {
        get
        {
            return _cmail;
        }
        set
        {
            this._cmail = value;
        }

    }
    public string Companyaddress
    {

        get
        {
            return _Companyaddress;
        }
        set
        {
            this._Companyaddress = value;
        }


    }
    public string Companyphoneno
    {
        get
        {
            return _Companyphoneno;
        }
        set
        {
            this._Companyphoneno = value;
        }
    }
    public string Tinno
    {

        get
        {
            return _Tinno;
        }
        set
        {
            this._Tinno = value;
        }
    }
    public string Cstno
    {
        get { return _Cstno; }
        set { this._Cstno = value; }
    }

}
