using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DALLoanPlant
/// </summary>
public class DALLoanPlant
{
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALLoanPlant()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void InsertLoanPlant(BOLLoanPlant loanpbol,string sql)
    {
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parcompany, parplant, parloanmode;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            parcompany = cmd.Parameters.Add("@Company_Code",SqlDbType.Int);
            parplant = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            parloanmode = cmd.Parameters.Add("@Loan_Mode",SqlDbType.Int);

            parcompany.Value = loanpbol.Companycode;
            parplant.Value = loanpbol.Plantcode;
            parloanmode.Value = loanpbol.Loanmode;

            cmd.ExecuteNonQuery();
        }

    }
    public void insertLstatus(BOLLoanPlant loanpbol, string sql)
    {
        using (con = dbaccess.GetConnection())
        {

            SqlCommand cmd = new SqlCommand();
            SqlParameter cmpcode, plantcode, routeid, lstatus;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmpcode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            plantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            routeid = cmd.Parameters.Add("@Route_Id", SqlDbType.Int);
            lstatus = cmd.Parameters.Add("@Lstatus", SqlDbType.Bit);

            cmpcode.Value = loanpbol.Companycode;
            plantcode.Value = loanpbol.Plantcode;
            routeid.Value = loanpbol.Routeid;
            lstatus.Value = loanpbol.Lstatus;

            cmd.ExecuteNonQuery();

        }
    }

    
}