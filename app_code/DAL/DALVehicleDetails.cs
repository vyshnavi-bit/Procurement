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
/// Summary description for DALVehicleDetails
/// </summary>
public class DALVehicleDetails:DbHelper
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    BOLvehicle bolvehicle = new BOLvehicle();
	public DALVehicleDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string insertVdetails(BOLvehicle vehicleBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            int result = 0;
            string message = string.Empty;
            SqlCommand cmd = new SqlCommand();
            SqlParameter cmpcode, plantcode, vehicleno, truckid, truckname, phoneno, bankid, ltrcost, pmess, press, ifsc, accountnum,paymentmode; 
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmpcode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            plantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            vehicleno = cmd.Parameters.Add("@Vehicle_No", SqlDbType.NVarChar, 50);
            truckid = cmd.Parameters.Add("@Truck_Id", SqlDbType.Int);
            truckname = cmd.Parameters.Add("@Truck_Name", SqlDbType.NVarChar, 50);
            phoneno = cmd.Parameters.Add("@Phone_No", SqlDbType.NVarChar, 50);
            bankid = cmd.Parameters.Add("@Bank_Id", SqlDbType.Int);
            ifsc = cmd.Parameters.Add("@ifsc_code", SqlDbType.NVarChar, 50);
            accountnum = cmd.Parameters.Add("@AccountNo", SqlDbType.NVarChar, 50);
            ltrcost = cmd.Parameters.Add("@Ltr_Cost", SqlDbType.Money);           
            paymentmode = cmd.Parameters.Add("@paymentmode", SqlDbType.Int);

            pmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            press = cmd.Parameters.Add("@ress", SqlDbType.Int);
            cmd.Parameters["@ress"].Direction = ParameterDirection.Output;


            cmpcode.Value = vehicleBO.Companycode;
            plantcode.Value = vehicleBO.Plantcode;
            vehicleno.Value = vehicleBO.Vehicleno;
            truckid.Value = vehicleBO.Truckid;
            truckname.Value = vehicleBO.Truckname;
            phoneno.Value = vehicleBO.Phoneno;
            bankid.Value = vehicleBO.Bankid;
            ifsc.Value = vehicleBO.IFSC;
            accountnum.Value = vehicleBO.ACCOUNTNUM;
            ltrcost.Value = vehicleBO.Ltrcost;
            paymentmode.Value = vehicleBO.Paymentmode;

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

    public string UpdateDistanceTruckPresentDAL(BOLvehicle vehicleBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            int result = 0;
            string message = string.Empty;
            SqlCommand cmd = new SqlCommand();
            SqlParameter cmpcode, plantcode, truckid, pdate, Distance, vtsdistanve, admindistance, pmess, press;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmpcode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            plantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            truckid = cmd.Parameters.Add("@Truck_Id", SqlDbType.Int);
            pdate = cmd.Parameters.Add("@pdate", SqlDbType.Date);
            Distance = cmd.Parameters.Add("@modifyDistance", SqlDbType.Float);
            vtsdistanve = cmd.Parameters.Add("@vtsdistanve", SqlDbType.Float);
            admindistance = cmd.Parameters.Add("@admindistance", SqlDbType.Float);

            pmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            press = cmd.Parameters.Add("@ress", SqlDbType.Int);
            cmd.Parameters["@ress"].Direction = ParameterDirection.Output;


            cmpcode.Value = vehicleBO.Companycode;
            plantcode.Value = vehicleBO.Plantcode;
            truckid.Value = vehicleBO.Truckid;
            pdate.Value = vehicleBO.Pdate;
            Distance.Value = vehicleBO.Distance;
            vtsdistanve.Value = vehicleBO.Vtsdistance;
            admindistance.Value = vehicleBO.Admindistance;


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
}
