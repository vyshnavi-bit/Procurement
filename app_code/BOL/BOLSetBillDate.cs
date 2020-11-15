using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOLSetBillDate
/// </summary>
public class BOLSetBillDate
{
    private int _tid;
    private string _companycode;
    private string _plantcode;
    private DateTime _billfromdate;
    private DateTime _billtodate;
    private int _updatestatus;
    private int _viewstatus;
    private int _status;
    private string _descriptions;
    private int _paymentFlag;

    public BOLSetBillDate()
    {
        _tid = 0;
        _companycode = string.Empty;
        _plantcode = string.Empty;
        _updatestatus = 0;
        _viewstatus = 0;
        _status = 0;
        _paymentFlag = 0;

    }

    public int Tid
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

    public string Companycode
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

    public string Plantcode
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

    public DateTime Billfromdate
    {
        get
        {
            return this._billfromdate;
        }
        set
        {
            this._billfromdate = value;
        }
    }

    public DateTime Billtodate
    {
        get
        {
            return this._billtodate;
        }
        set
        {
            this._billtodate = value;
        }
    }

    public int Updatestatus
    {
        get
        {
            return this._updatestatus;
        }
        set
        {
            this._updatestatus = value;
        }
    }
    public int Viewstatus
    {
        get
        {
            return this._viewstatus;
        }
        set
        {
            this._viewstatus = value;
        }
    }
    public int Status
    {
        get
        {
            return this._status;
        }
        set
        {
            this._status = value;
        }
    }

    public string Descriptions
    {
        get
        {
            return this._descriptions;
        }
        set
        {
            this._descriptions = value;
        }
    }
    public int PaymentFlag
    {
        get
        {
            return this._paymentFlag;
        }
        set
        {
            this._paymentFlag = value;
        }
    }

}