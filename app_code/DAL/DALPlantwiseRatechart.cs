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
/// Summary description for DALPlantwiseRatechart
/// </summary>
public class DALPlantwiseRatechart
{
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALPlantwiseRatechart()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string DInsertPlantwiseratechart(BOLPlantwiseRatechart Rate, string Sql)
    {
        using (con = dbaccess.GetConnection())
        {
            string message = String.Empty;
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter pcompanycode, pplantcode, pratechartcode,pBuffratechartcode, parmess, parresult;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Sql;
            cmd.Connection = con;

            pcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            pplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            pratechartcode = cmd.Parameters.Add("@RateChartId", SqlDbType.NVarChar, 50);
            pBuffratechartcode = cmd.Parameters.Add("@RateChartBuff", SqlDbType.NVarChar, 50);
            parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;

            parresult = cmd.Parameters.Add("@res", SqlDbType.Int);
            cmd.Parameters["@res"].Direction = ParameterDirection.Output;

            pcompanycode.Value = Rate.Companycode;
            pplantcode.Value = Rate.Plantcode;
            pratechartcode.Value = Rate.ChartName;
            pBuffratechartcode.Value = Rate.BuffchartName;

         

            cmd.ExecuteNonQuery();

            message = (string)cmd.Parameters["@mess"].Value;

            result = (int)cmd.Parameters["@res"].Value;

            con.Close();

            if (result == 1)
            {

                //WebMsgBox.Show(message);
                return message;

            }
            else if (result == 2)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            else
            {

                WebMsgBox.Show("Please, Check Connection");

            }

            return message;
        }
    }



    public string DInsertRoutewiseratechart(BOLPlantwiseRatechart Rate, string Sql)
    {
        using (con = dbaccess.GetConnection())
        {
            string message = String.Empty;
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter pcompanycode, pplantcode, pratechartcode,pratechartcodeBuff, parmess, parresult,pRouteId;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Sql;
            cmd.Connection = con;

            pcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            pplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            pratechartcode = cmd.Parameters.Add("@RateChartId", SqlDbType.NVarChar, 50);
            pratechartcodeBuff = cmd.Parameters.Add("@RatechartIdBuff", SqlDbType.NVarChar, 50);

            pRouteId = cmd.Parameters.Add("@RouteId", SqlDbType.Int);

            parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;

            parresult = cmd.Parameters.Add("@res", SqlDbType.Int);
            cmd.Parameters["@res"].Direction = ParameterDirection.Output;

            pcompanycode.Value = Rate.Companycode;
            pplantcode.Value = Rate.Plantcode;
            pratechartcode.Value = Rate.ChartName;
            pratechartcodeBuff.Value = Rate.BuffchartName;

            pRouteId.Value = Rate.RouteId;

            cmd.ExecuteNonQuery();

            message = (string)cmd.Parameters["@mess"].Value;

            result = (int)cmd.Parameters["@res"].Value;

            con.Close();

            if (result == 1)
            {

                //WebMsgBox.Show(message);
                return message;

            }
            else if (result == 2)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            else
            {

                WebMsgBox.Show("Please, Check Connection");

            }

            return message;
        }
    }

    public string DInsertPlantRoutewiseratechart(BOLPlantwiseRatechart Rate, string Sql)
    {
        using (con = dbaccess.GetConnection())
        {
            string message = String.Empty;
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter pcompanycode, pplantcode, pratechartcode,  parmess, parresult;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Sql;
            cmd.Connection = con;

            pcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            pplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            pratechartcode = cmd.Parameters.Add("@RateChartId", SqlDbType.NVarChar, 50);
           
            parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;

            parresult = cmd.Parameters.Add("@res", SqlDbType.Int);
            cmd.Parameters["@res"].Direction = ParameterDirection.Output;

            pcompanycode.Value = Rate.Companycode;
            pplantcode.Value = Rate.Plantcode;
            pratechartcode.Value = Rate.ChartName;
         
            cmd.ExecuteNonQuery();

            message = (string)cmd.Parameters["@mess"].Value;

            result = (int)cmd.Parameters["@res"].Value;

            con.Close();

            if (result == 1)
            {

                //WebMsgBox.Show(message);
                return message;

            }
            else if (result == 2)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            else
            {

                WebMsgBox.Show("Please, Check Connection");

            }
            return message;


        }
    }

    public string DInsertAgentwiseratechart(BOLPlantwiseRatechart Rate, string Sql)
    {
        using (con = dbaccess.GetConnection())
        {
            string message = String.Empty;
            int result = 0;           
            SqlCommand cmd = new SqlCommand();
            SqlParameter pcompanycode, pplantcode, pratechartcode, pratechartcodeBuff, parmess, parresult, puserid,pAgentid,pcurtimestamp;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Sql;
            cmd.Connection = con;

            pcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            pplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            pratechartcode = cmd.Parameters.Add("@RateChartId", SqlDbType.NVarChar, 50);
            pratechartcodeBuff = cmd.Parameters.Add("@RatechartIdBuff", SqlDbType.NVarChar, 50);
            
            pAgentid = cmd.Parameters.Add("@Agent_Id", SqlDbType.Int);
            puserid = cmd.Parameters.Add("@userid", SqlDbType.Int);
            pcurtimestamp = cmd.Parameters.Add("@curtimestamp", SqlDbType.DateTime);

            parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;

            parresult = cmd.Parameters.Add("@res", SqlDbType.Int);
            cmd.Parameters["@res"].Direction = ParameterDirection.Output;

            pcompanycode.Value = Rate.Companycode;
            pplantcode.Value = Rate.Plantcode;
            pratechartcode.Value = Rate.ChartName;
            pratechartcodeBuff.Value = Rate.BuffchartName;
            
            pAgentid.Value = Rate.AgentId;
            puserid.Value = program.Guser_User_Id;
            pcurtimestamp.Value = Rate.Curtimestamp;

           

            cmd.ExecuteNonQuery();

            message = (string)cmd.Parameters["@mess"].Value;

            result = (int)cmd.Parameters["@res"].Value;

            con.Close();

            if (result == 1)
            {

                //WebMsgBox.Show(message);
                return message;

            }
            else if (result == 2)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            else
            {

                WebMsgBox.Show("Please, Check Connection");

            }

            return message;
        }
    }

   
}
