using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOLVoucher
/// </summary>
public class BOLVoucher
{
    private int _companycode;
    private int _plantcode;
    private int _routeid;
    private int _agentid;
    private DateTime _clearingdate;
    private DateTime _inwarddate;
    private string _shift;
    private float _mlrt;
    private float _fat;
    private float _snf;
    private float _clr;
    private float _rate;
    private float _amount;
    private string _remarks;


	public BOLVoucher()
	{
        _companycode = 0;
        _plantcode = 0;
        _routeid = 0;
        _agentid = 0;
        _shift = string.Empty;
        _mlrt = 0;
        _fat = 0;
        _snf = 0;
        _clr = 0;
        _rate = 0;
        _amount = 0;
        _remarks = string.Empty;
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
    public int Agentid
    {
        get { return this._agentid; }
        set { this._agentid = value; }
    }
    public DateTime Clearingdate
    {
        get { return this._clearingdate; }
        set { this._clearingdate = value; }
    }
    public DateTime Inwarddate
    {
        get { return this._inwarddate; }
        set { this._inwarddate = value; }
    }

    public string Shift
    {
        get { return this._shift; }
        set { this._shift = value; }
    }
    public float Mlrt
    {
        get { return this._mlrt; }
        set { this._mlrt = value; }
    }
    public float Fat
    {
        get { return this._fat; }
        set { this._fat = value; }
    }
    public float Snf
    {
        get { return this._snf; }
        set { this._snf = value; }
    }
    public float Clr
    {
        get { return this._clr; }
        set { this._clr = value; }
    }
    public float Rate
    {
        get { return this._rate; }
        set { this._rate = value; }
    }
    public float Amount
    {
        get { return this._amount; }
        set { this._amount = value; }
    }
    public string Remarks
    {
        get { return this._remarks; }
        set { this._remarks = value; }
    }
}