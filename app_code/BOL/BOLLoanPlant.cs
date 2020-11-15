using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOLLoanPlant
/// </summary>
public class BOLLoanPlant
{
    private int _companycode;
    private int _plantcode;
    private int _loanmode;
    private int _routeid;
    private bool _lstatus;
	public BOLLoanPlant()
	{
        _companycode = 0;
        _plantcode = 0;
        _loanmode = 0;
        _routeid = 0;
        _lstatus = false;
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

    public int Loanmode
    {
        get
        {
            return this._loanmode;
        }
        set
        {
            this._loanmode = value;
        }
    }
    public bool Lstatus
    {
        get
        {
            return this._lstatus;
        }
        set
        {
            this._lstatus = value;
        }
    }
    public int Routeid
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
}