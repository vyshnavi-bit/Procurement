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
/// Summary description for DALBankMaster
/// </summary>
public class DALBankMaster:DbHelper
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();    
	public DALBankMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertBankmaster(BOLBankMaster bankmasterBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parbankid,parbankname, parbranchname,parbranchcode,parifsccode,parphonenumber,parplantcode,parcompanycode;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            parbankid = cmd.Parameters.Add("@Bank_ID", SqlDbType.Int);
            parbankname = cmd.Parameters.Add("@Bank_Name", SqlDbType.NVarChar,50);
            parbranchname = cmd.Parameters.Add("@Branch_Name", SqlDbType.NVarChar,50);
           
            parbranchcode = cmd.Parameters.Add("@Branch_Code", SqlDbType.NVarChar,6);
            
            parifsccode = cmd.Parameters.Add("@Ifsc_Code", SqlDbType.NVarChar,11);
            parphonenumber = cmd.Parameters.Add("@Phone_Number", SqlDbType.NVarChar,12);
            parplantcode = cmd.Parameters.Add("@Plant_Code ", SqlDbType.Int);
            parcompanycode = cmd.Parameters.Add("@Company_Code ", SqlDbType.Int);

            parbankid.Value = int.Parse(bankmasterBO.BankID);
            parbankname.Value = bankmasterBO.BankName;
            parbranchname.Value = bankmasterBO.BranchName;
           
            parbranchcode.Value = bankmasterBO.BranchCode;
          
            parifsccode.Value = bankmasterBO.Ifsccode;
            parphonenumber.Value =bankmasterBO.Phonenumber;
           // parphonenumber.Value  = Convert.ToInt64(bankmasterBO.Phonenumber);
            parplantcode.Value =Convert.ToInt32(bankmasterBO.Plantcode);
            parcompanycode.Value = Convert.ToInt32(bankmasterBO.Companycode);
                
                

            cmd.ExecuteNonQuery();

        }
    }
}
