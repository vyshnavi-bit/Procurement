using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALLoanDueRecovery
/// </summary>
public class DALLoanDueRecovery
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALLoanDueRecovery()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string InsertLoanDueRecovery(BOLLoanDueRecovery bolonduerecovey, string sql)
    {
        using (con = DBaccess.GetConnection())
        {

            int result = 0;
            string message = string.Empty;
            SqlCommand cmd = new SqlCommand();
            SqlParameter LoanDueRef_Id, Company_code, Plant_code, Route_Id, Agent_Id, LoanRecovery_Date, loan_Id, LoanDue_Balance, LoanDueRecovery_Amount, LoanBalance,Remarks, pmess, press;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            LoanDueRef_Id = cmd.Parameters.Add("@LoanDueRef_Id", SqlDbType.Int);
            Company_code = cmd.Parameters.Add("@Company_code", SqlDbType.Int);
            Plant_code = cmd.Parameters.Add("@Plant_code", SqlDbType.Int);
            Route_Id = cmd.Parameters.Add("@Route_Id", SqlDbType.Int);
            Agent_Id = cmd.Parameters.Add("@Agent_Id", SqlDbType.Int);
            LoanRecovery_Date = cmd.Parameters.Add("@LoanRecovery_Date", SqlDbType.DateTime);
            loan_Id = cmd.Parameters.Add("@loan_Id", SqlDbType.Int);
            LoanDue_Balance = cmd.Parameters.Add("@LoanDue_Balance", SqlDbType.Float);
            LoanDueRecovery_Amount = cmd.Parameters.Add("@LoanDueRecovery_Amount", SqlDbType.Float);
            LoanBalance = cmd.Parameters.Add("@LoanBalance", SqlDbType.Float);
            Remarks = cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar,250);            

            pmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            press = cmd.Parameters.Add("@ress", SqlDbType.Int);
            cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

            LoanDueRef_Id.Value = bolonduerecovey.LoanDueRefid;
            Company_code.Value = bolonduerecovey.Companycode;
            Plant_code.Value = bolonduerecovey.Plantcode;
            Route_Id.Value = bolonduerecovey.RouteId;
            Agent_Id.Value = bolonduerecovey.AgentId;
            LoanRecovery_Date.Value = bolonduerecovey.LoanRecoveryDate;
            loan_Id.Value = bolonduerecovey.Loan_Id;
            LoanDue_Balance.Value = bolonduerecovey.LoanDueBalance;
            LoanDueRecovery_Amount.Value = bolonduerecovey.LoanDueRecoveryAmount;
            LoanBalance.Value = bolonduerecovey.LoanBalance;
            Remarks.Value = bolonduerecovey.Remarks;            

            cmd.ExecuteNonQuery();

            message = (string)cmd.Parameters["@mess"].Value;
            result = (int)cmd.Parameters["@ress"].Value;

            if (result == 1)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            else
            {
                //WebMsgBox.Show(message);
                return message;
            }


        }
    }


    public void DeleteLoanDueRecovery(BOLLoanDueRecovery bolonduerecovey, string sql)
    {
        using (con = DBaccess.GetConnection())
        {            
            SqlCommand cmd = new SqlCommand();
            SqlParameter LoanDueRef_Id, Company_code, Plant_code;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            LoanDueRef_Id = cmd.Parameters.Add("@LoanDueRef_Id", SqlDbType.Int);
            Company_code = cmd.Parameters.Add("@Company_code", SqlDbType.Int);
            Plant_code = cmd.Parameters.Add("@Plant_code", SqlDbType.Int);           

            LoanDueRef_Id.Value = bolonduerecovey.LoanDueRefid;
            Company_code.Value = bolonduerecovey.Companycode;
            Plant_code.Value = bolonduerecovey.Plantcode;           

            cmd.ExecuteNonQuery();          


        }
    }



}