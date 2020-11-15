using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;



public class BOLloan
{

    private int _companycode;
    private int _plantcode;
    
    private DateTime _loandate;
    private DateTime _expdate;
    private int _rout_id;
    private int _agent_id;
    private int _loan_id;
    private double _loanamnt;
    private double _instamnt;
    private string _desc;    

    private double _balance;
    private int _status;

    private double _interesamnt;
    private int _noofInstallment;
    private int _userId;


    public BOLloan()
    {

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

    public int Loan_id
    {
        get
        {
            return _loan_id;
        }
        set
        {
            this._loan_id = value;
        }
    }
    public int agent_id
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
    public int Route_id
    {
        get
        {
            return _rout_id;
        }
        set
        {
            this._rout_id = value;
        }
    }
    public DateTime Loandate
    {
        get
        {
            return _loandate;
        }
        set
        {
            this._loandate = value;
        }
    }
    public DateTime Expiredate
    {
        get
        {
            return _expdate;
        }
        set
        {
            this._expdate = value;
        }
    }
    public double Loanamount
    {
        get
        {
            return _loanamnt;
        }
        set
        {
            this._loanamnt = value;
        }
    }
    public double Instamnt
    {
        get
        {
            return _instamnt;
        }
        set
        {
            this._instamnt = value;
        }
    }
    public double Balance
    {
        get
        {
            return _balance;
        }
        set
        {
            this._balance = value;
        }
    }
    public string Desc
    {
        get
        {
            return _desc;
        }
        set
        {
            this._desc = value;
        }
    }
    public int status
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
    public double InterestAmount
    {
        get
        {
            return _interesamnt;
        }
        set
        {
            this._interesamnt = value;
        }
    }
    public int NoofInstallment
    {
        get
        {
            return _noofInstallment;
        }
        set
        {
            this._noofInstallment = value;
        }
    }
    public int UserId
    {
        get
        {
            return _userId;
        }
        set
        {
            this._userId = value;
        }
    }
}

