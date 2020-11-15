using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BLLLoanDueRecovery
/// </summary>
public class BLLLoanDueRecovery
{
  
    DALLoanDueRecovery dalldr = new DALLoanDueRecovery();
    DbHelper DBaccess = new DbHelper();
    SqlDataReader dr = null;
	public BLLLoanDueRecovery()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string InsertLoanDueRecovery(BOLLoanDueRecovery bolldr)
    {
        string mess=string.Empty;
        string sql = "Insert_LoanDueRecovery";
        mess=dalldr.InsertLoanDueRecovery(bolldr,sql);
        return mess;
    }
    

    public int GetLoanRefNo(string ccode, string pcode)
    {
        int bno = 0;
        string sqlstr = string.Empty;
        try
        {
            sqlstr = "SELECT MAX(LoanDueRef_Id) FROM LoanDue_Recovery WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ";
            bno = (int)DBaccess.ExecuteScalarint(sqlstr);
            bno = ++bno;
            return bno;
        }
        catch (Exception ex)
        {
            bno = 1;
        }
        return bno;
    }

    public SqlDataReader GetAgentLoanId(string ccode, string pcode, string Aid)
    {

        string sqlstr = string.Empty;
        try
        {
            sqlstr = "SELECT TOP 1 loan_Id,CAST(balance AS DECIMAL(18,2)) AS balance,CAST(inst_amount AS DECIMAL(18,2)) AS inst_amount  FROM LoanDetails WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Agent_Id='" + Aid + "' AND balance>1  ORDER BY loan_Id";
            dr = DBaccess.GetDatareader(sqlstr);
        }
        catch (Exception ex)
        {

        }
        return dr;
    }

    public DataTable GetLoanDueRecoveryData(string code,string pcode)
    {
        DataTable dt = new DataTable();
        string sql = string.Empty;
        sql = "SELECT LoanDueRef_Id,Agent_Id,LoanRecovery_Date,loan_Id,LoanDue_Balance,LoanDueRecovery_Amount,LoanBalance,Remarks FROM LoanDue_Recovery WHERE Company_code='" + code + "' AND Plant_code='" + pcode + "' ORDER BY LoanDueRef_Id DESC";
        dt = DBaccess.GetDatatable(sql);
        return dt;
    }

    public void DeleteLoanDueRecoveryData(BOLLoanDueRecovery bolldr)
    {       
        string sql = string.Empty;
        sql = "Delete_LoanDueRecovery";
        dalldr.DeleteLoanDueRecovery(bolldr, sql);
        
    }
}