using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoanDueRecovery
/// </summary>
public class BOLLoanDueRecovery
{
    private int _loanDueRefid;
    private int _companycode;
    private int _plantcode;
    private int _routeId;
    private int _agentId;
    private DateTime _loanRecoveryDate; 
    private int _loanId;
    private float _loanDueBalance;
    private float _loanDueRecoveryAmount;
    private float _loanBalance;
    private string _remarks;


    private int _deleteflag;


    public BOLLoanDueRecovery()
    {
        _loanDueRefid = 0;
        _companycode = 0;
        _plantcode = 0;
        _routeId = 0;
        _agentId = 0;
        _loanId = 0;
        _loanDueBalance = 0;
        _loanDueRecoveryAmount = 0;
        _loanBalance = 0;
        _remarks = string.Empty;

        _deleteflag = 0;
    }

    public int LoanDueRefid
    {
        get { return this._loanDueRefid; }
        set { this._loanDueRefid = value; }
    }
    public int Companycode
    {
        get { return this._companycode; }
        set { this._companycode = value; }
    }
    public int Plantcode
    {
        get { return this._plantcode; }
        set { this._plantcode = value; }
    }
    public int RouteId
    {
        get { return this._routeId; }
        set { this._routeId = value; }
    }
    public int AgentId
    {
        get { return this._agentId; }
        set { this._agentId = value; }
    }
    public DateTime LoanRecoveryDate
    {
        get { return this._loanRecoveryDate; }
        set { this._loanRecoveryDate = value; }
    }
    public int Loan_Id
    {
        get { return this._loanId; }
        set { this._loanId = value; }
    }
    public float LoanDueBalance
    {
        get { return this._loanDueBalance; }
        set { this._loanDueBalance = value; }
    }
    public float LoanDueRecoveryAmount
    {
        get { return this._loanDueRecoveryAmount; }
        set { this._loanDueRecoveryAmount = value; }
    }
    public float LoanBalance
    {
        get { return this._loanBalance; }
        set { this._loanBalance = value; }
    }
    public string Remarks
    {
        get { return this._remarks; }
        set { this._remarks = value; }
    }


    public int DeleteFlag
    {
        get { return this._deleteflag; }
        set{this._deleteflag=value;}
    }
}