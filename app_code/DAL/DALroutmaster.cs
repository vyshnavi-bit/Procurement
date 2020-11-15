using System;
using System.Collections.Generic;
using System.Text;
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


 
public class DALroutmaster : DbHelper
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    public DALroutmaster()
    { }
    public string insertRoutmaster(BOLroutmaster routmasterBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            string message = String.Empty;
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter parroutid, parroutname, partotdistance, paraddeddate, parstatus, parcompanyid, parplantid, parmess, parresult,parlstatus,parphoneno;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            parroutid = cmd.Parameters.Add("@route_Id", SqlDbType.Int);
            parroutname = cmd.Parameters.Add("@route_Name", SqlDbType.NVarChar, 50);
            partotdistance = cmd.Parameters.Add("@tot_Distance", SqlDbType.Int);
            paraddeddate = cmd.Parameters.Add("@added_Date", SqlDbType.DateTime);
            parstatus = cmd.Parameters.Add("@status", SqlDbType.Bit);
            parlstatus = cmd.Parameters.Add("@Lstatus", SqlDbType.Bit);
            parphoneno = cmd.Parameters.Add("@phoneno", SqlDbType.NVarChar, 15);

            parcompanyid = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            parplantid = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);

            parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            parresult = cmd.Parameters.Add("@res", SqlDbType.Int);
            cmd.Parameters["@res"].Direction = ParameterDirection.Output;

            parroutid.Value = routmasterBO.routId;
            parroutname.Value = routmasterBO.routName;
            partotdistance.Value = routmasterBO.totDistance;
            paraddeddate.Value = routmasterBO.addedDate;
            parstatus.Value = routmasterBO.status;
            parlstatus.Value = routmasterBO.status;
            parcompanyid.Value = routmasterBO.Companyid;
            parplantid.Value = routmasterBO.Plantid;
            parphoneno.Value = routmasterBO.Phoneno;

            cmd.ExecuteNonQuery();
            message = (string)cmd.Parameters["@mess"].Value;
            
            result = (int)cmd.Parameters["@res"].Value;
            con.Close();
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
            return message;

        }
    }
    public void deleteRoutmaster(BOLroutmaster routmasterBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parroutid;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;

            parroutid = cmd.Parameters.Add("@rout_id", SqlDbType.Int);

            parroutid.Value = routmasterBO.routId;

            cmd.ExecuteNonQuery();
        }
    }

}

