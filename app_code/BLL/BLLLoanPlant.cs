using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BLLLoanPlant
/// </summary>
public class BLLLoanPlant
{
    DALLoanPlant loanpDA = new DALLoanPlant();
    DbHelper dbaccess=new DbHelper();
    public string sqlstr;
	public BLLLoanPlant()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void insertloanplant(BOLLoanPlant loanpBO)
    {
        sqlstr = "Insert_LoanPlantwise";
        loanpDA.InsertLoanPlant(loanpBO, sqlstr);
    }
    public void inserttlstatus(BOLLoanPlant loanpBO)
    {
        string sql = "Edit_Lstatus";
        loanpDA.insertLstatus(loanpBO, sql);
    }
}