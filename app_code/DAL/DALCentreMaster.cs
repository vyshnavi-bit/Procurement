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
/// Summary description for DALCentreMaster
/// </summary>
public class DALCentreMaster : DbHelper
{
    
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();    
	public DALCentreMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertCentermaster(BOLCentreMaster centermasterBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parrouteid, paragentid, parcentrecode, parcentrename, parstateid, parproducercode, parproducername, parregistereddate, parstatus,parplantcode,parcompanycode;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            parrouteid = cmd.Parameters.Add("@Route_ID", SqlDbType.Int);
            paragentid = cmd.Parameters.Add("@Agent_ID", SqlDbType.Int);
            parcentrecode = cmd.Parameters.Add("@Centre_Code", SqlDbType.Int);
            parcentrename = cmd.Parameters.Add("@Centre_Name", SqlDbType.NVarChar, 35);
            parstateid = cmd.Parameters.Add("@State_ID", SqlDbType.Int);
            parproducercode = cmd.Parameters.Add("@Producer_Code", SqlDbType.Int);
            parproducername = cmd.Parameters.Add("@Producer_Name", SqlDbType.NVarChar, 35);
            //parratechart = cmd.Parameters.Add("@Rate_Chart", SqlDbType.NVarChar, 15);
            parregistereddate = cmd.Parameters.Add("@Registered_Date", SqlDbType.DateTime, 50);
            parstatus = cmd.Parameters.Add("@Status", SqlDbType.Bit);
            parplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            parcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);

            parrouteid.Value = centermasterBO.RouteId;
            paragentid.Value = centermasterBO.Agent_id;
            parcentrecode.Value = centermasterBO.Centrecode;
            parcentrename.Value = centermasterBO.CentreName;
            parstateid.Value = centermasterBO.State_id;
            parproducercode.Value = centermasterBO.ProducerCode;
            parproducername.Value = centermasterBO.Producername;
           // parratechart.Value = centermasterBO.Ratechart;
            parregistereddate.Value = centermasterBO.RegisteredDate;
            parstatus.Value = centermasterBO.status;
            parplantcode.Value = centermasterBO.Plantcode;
            parcompanycode.Value = centermasterBO.Companycode;

            cmd.ExecuteNonQuery();

        }
    }
}
