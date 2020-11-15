using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALCompanymster
/// </summary>
public class DALCompanymster:DbHelper
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();    

	public DALCompanymster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertCompanymaster( BOLCompanyMaster companymasterBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {

            SqlCommand cmd = new SqlCommand();
            SqlParameter  parcompanycode,parcompanyname,partid,parcompanyaddress,parcompanyphoneno,partinno,parcstno,parcmail;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            partid = cmd.Parameters.Add("@Tid ", SqlDbType.Int);
            parcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            parcompanyname = cmd.Parameters.Add("@Company_Name", SqlDbType.NVarChar, 50);
            parcompanyaddress = cmd.Parameters.Add("@Company_Address", SqlDbType.NVarChar, 100);
            parcompanyphoneno = cmd.Parameters.Add("@Company_PhoneNo", SqlDbType.NVarChar, 50);
            partinno = cmd.Parameters.Add("@TinNo ", SqlDbType.NVarChar, 20);
            parcstno = cmd.Parameters.Add("@CstNo", SqlDbType.NVarChar, 20);
            parcmail = cmd.Parameters.Add("@Cmail", SqlDbType.NVarChar, 50);
           
            partid.Value = Convert.ToInt32(companymasterBO.tid);
            parcompanycode.Value = Convert.ToInt32(companymasterBO.Companycode);
            parcompanyname.Value = companymasterBO.Companyname;
            parcompanyaddress.Value = companymasterBO.Companyaddress;
            parcompanyphoneno.Value = companymasterBO.Companyphoneno;
            partinno.Value = companymasterBO.Tinno;
            parcstno.Value = companymasterBO.Cstno;
            parcmail.Value = companymasterBO.Cmail;


           cmd.ExecuteNonQuery();

        }

    }
}
