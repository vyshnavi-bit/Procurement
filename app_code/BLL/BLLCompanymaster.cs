using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLCompanymaster
/// </summary>
public class BLLCompanymaster
{
    DALCompanymster companymasterDA = new DALCompanymster();
    string sqlstr;
	public BLLCompanymaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertcompanymaster(BOLCompanyMaster companymasterBO)
    {
        string sql = "Insert_Companymaster";
        companymasterDA.insertCompanymaster(companymasterBO, sql);
      
    }
}
