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
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DALtroute
/// </summary>
public class DALtroute
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    BOLvehicle bolvehicle = new BOLvehicle();
	public DALtroute()
	{
    	//
		// TODO: Add constructor logic here
		//
	}
    public void insertRouteSupervisorAllotment(BOLvehicle vehicleBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {

            SqlCommand cmd = new SqlCommand();
            SqlParameter cmpcode, plantcode, truckid, routeid;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmpcode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            plantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            truckid = cmd.Parameters.Add("@Supervisor_Id", SqlDbType.Int);
            routeid = cmd.Parameters.Add("@Route_Id", SqlDbType.Int);

            cmpcode.Value = vehicleBO.Companycode;
            plantcode.Value = vehicleBO.Plantcode;
            truckid.Value = vehicleBO.Truckid;
            routeid.Value = vehicleBO.Routeid;

            cmd.ExecuteNonQuery();

        }
    }
    public void insertTroute(BOLvehicle vehicleBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {

            SqlCommand cmd = new SqlCommand();
            SqlParameter cmpcode, plantcode, truckid, routeid;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmpcode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            plantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            truckid = cmd.Parameters.Add("@Truck_Id", SqlDbType.Int);
            routeid = cmd.Parameters.Add("@Route_Id", SqlDbType.Int);

            cmpcode.Value = vehicleBO.Companycode;
            plantcode.Value = vehicleBO.Plantcode;
            truckid.Value = vehicleBO.Truckid;
            routeid.Value = vehicleBO.Routeid;           
            
            cmd.ExecuteNonQuery();

        }
    }
    public string insertRroutedistance(BOLvehicle vehicleBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            string message = String.Empty;
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter cmpcode, plantcode, truckid, proutedistance, mess, ress;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmpcode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            plantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            truckid = cmd.Parameters.Add("@Truck_Id", SqlDbType.Int);
            proutedistance = cmd.Parameters.Add("@Route_TotDistance", SqlDbType.Float);
            mess = cmd.Parameters.Add("@mes", SqlDbType.Char, 500);
            cmd.Parameters["@mes"].Direction = ParameterDirection.Output;
            ress = cmd.Parameters.Add("@res", SqlDbType.Int);
            cmd.Parameters["@res"].Direction = ParameterDirection.Output;
                       

            cmpcode.Value = vehicleBO.Companycode;
            plantcode.Value = vehicleBO.Plantcode;
            truckid.Value = vehicleBO.Truckid;
            proutedistance.Value = vehicleBO.Distance;

            cmd.ExecuteNonQuery();

            message = (string)cmd.Parameters["@mes"].Value;
            result = (int)cmd.Parameters["@res"].Value;
            if (result == 1)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            if (result == 2)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            return message;
        }

    }

}
