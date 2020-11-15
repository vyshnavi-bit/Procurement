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
using System.IO;

/// <summary>
/// Summary description for DALLoginfo
/// </summary>
public class DALLoginfo
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();


	public DALLoginfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InsertLoginfo(BOLLoinfo LoginfoBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parloginId, parpassword, parroles,parcompanyid,parplantid;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;



            parloginId = cmd.Parameters.Add("@User_LoginId",SqlDbType.NVarChar,50);
            parpassword = cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
            parroles = cmd.Parameters.Add("@Roles", SqlDbType.Int);
            parcompanyid = cmd.Parameters.Add("@Company_ID", SqlDbType.Int);
            parplantid = cmd.Parameters.Add("@plant_Id", SqlDbType.Int);


            parloginId.Value = LoginfoBO.userloginID;
            parpassword.Value = LoginfoBO.Password;
            parroles.Value = LoginfoBO.roles;
            parcompanyid.Value = LoginfoBO.CompanyId;
            parplantid.Value = LoginfoBO.PlantId;
            cmd.ExecuteNonQuery();
                


        }


    }

    public void EditLoginfo(BOLLoinfo LoginfoBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parloginId, parpassword, parcompanyid, parplantid;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;



            parloginId = cmd.Parameters.Add("@User_LoginId", SqlDbType.NVarChar, 50);
            parpassword = cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
            parcompanyid = cmd.Parameters.Add("@Company_ID", SqlDbType.Int);
            parplantid = cmd.Parameters.Add("@plant_Id", SqlDbType.Int);


            parloginId.Value = LoginfoBO.userloginID;
            parpassword.Value = LoginfoBO.Password;
            parcompanyid.Value = LoginfoBO.CompanyId;
            parplantid.Value = LoginfoBO.PlantId;
            cmd.ExecuteNonQuery();



        }


    }

    public void deleteInfo(string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }
    }
}
