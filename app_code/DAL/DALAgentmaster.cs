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
/// Summary description for DALAgentmaster
/// </summary>
public class DALAgentmaster : DbHelper
{

    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALAgentmaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertAgentmaster(BOLAgentmaster agentmasterBO, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parcenterid, paragentid, paragentname, paragdate, partype, parcartage, parrouteid, parcompanycode, parbankid, parpaymentmode, paragentaccNo, paraagentmobile, parmilktype, pifsccode,psplbonusamount;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;


            parcenterid = cmd.Parameters.Add("@Centre_ID", SqlDbType.NVarChar, 50);

            paragentid = cmd.Parameters.Add("@Agent_ID", SqlDbType.Int);
            paragentname = cmd.Parameters.Add("@Agent_Name", SqlDbType.NVarChar, 50);
            paragdate = cmd.Parameters.Add("@AgDate", SqlDbType.DateTime);
            partype = cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50);
            parcartage = cmd.Parameters.Add("@Cartage_Amt", SqlDbType.Float);
            parrouteid = cmd.Parameters.Add("@Route_ID", SqlDbType.Int);

            parcompanycode = cmd.Parameters.Add("companycode", SqlDbType.Int);
            parbankid = cmd.Parameters.Add("Bankid", SqlDbType.NVarChar, 30);
            parpaymentmode = cmd.Parameters.Add("paymentmode", SqlDbType.NVarChar, 20);
            paragentaccNo = cmd.Parameters.Add("AgentaccNo", SqlDbType.NVarChar, 30);
            paraagentmobile = cmd.Parameters.Add("@phone_Number", SqlDbType.NVarChar, 30);
            parmilktype = cmd.Parameters.Add("@Milk_Nature", SqlDbType.NVarChar, 30);
            pifsccode = cmd.Parameters.Add("@Ifsc_code", SqlDbType.NVarChar, 30);
            psplbonusamount = cmd.Parameters.Add("@SplBonusAmount",SqlDbType.Float);

            parcenterid.Value = agentmasterBO.CentreID;
            paragentid.Value = agentmasterBO.AgentID;
            paragentname.Value = agentmasterBO.AgentName;
            paragdate.Value = agentmasterBO.AgDate;
            partype.Value = agentmasterBO.AgentType;
            parcartage.Value = agentmasterBO.CartageAmount;
            parrouteid.Value = agentmasterBO.RouteID;
            parcompanycode.Value = agentmasterBO.Companycode;
            parbankid.Value = agentmasterBO.Bankid;
            parpaymentmode.Value = agentmasterBO.Paymentmode;
            paragentaccNo.Value = agentmasterBO.AgentaccountNo;
            paraagentmobile.Value = agentmasterBO.AGENTMOBILE;
            parmilktype.Value = agentmasterBO.Milktype;
            pifsccode.Value = agentmasterBO.Ifsccode;
            psplbonusamount.Value = agentmasterBO.Splbonusamount;
            cmd.ExecuteNonQuery();

        }
    }
}
