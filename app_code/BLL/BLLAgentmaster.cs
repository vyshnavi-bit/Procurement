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
/// Summary description for BLLAgentmaster
/// </summary>
public class BLLAgentmaster
{
    DALAgentmaster agentDA = new DALAgentmaster();
    DbHelper dbaccess = new DbHelper();
    string sqlstr;
	public BLLAgentmaster()
	{
        sqlstr = string.Empty;
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertagent(BOLAgentmaster agentBO)
    {
        string sql = "Insert_AgentMaster";
        agentDA.insertAgentmaster(agentBO, sql);
       
    }
    public DataTable getAgentmastertable()
    {
        string sql = "select * from SampleAgentDemo";
        DataTable dt = new DataTable();
        dt = agentDA.GetDatatable(sql);
        return dt;
    }
    public DataTable getAgentmastertablebyrid(string id)
    {
        // string sql = "select t1.*,center_Master.center_name from (SELECT * FROM agent_master WHERE agent_id NOT IN (SELECT agent_id FROM loan_details WHERE status =0 ))as t1,center_Master where t1.center_id = center_master.center_id AND t1.route_Id="+ id +"";
        string sql = "SELECT * FROM Agent_Master WHERE Route_ID=" + id + "ORDER BY Agent_ID,Route_ID";
        DataTable dt = new DataTable();
        dt = agentDA.GetDatatable(sql);
        return dt;
    }
    public DataTable getAgentmastertable1(string id)
    {
        string sql = "select Agent_Master.*,Centre_Master.Centre_Name from Agent_Master,Center_Master where Agent_master.Centre_ID = Centre_Master.Centre_ID AND Agent_Master.Route_ID=" + id + "";
        DataTable dt = new DataTable();
        dt = agentDA.GetDatatable(sql);
        return dt;
    }
    

    public SqlDataReader getagentmasterdatareader()
    {
        SqlDataReader dr;
        sqlstr = "select Centre_ID,Centre_Name,Route_ID,Rate_Chart from Centre_Master";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getagentmasterdatareader1(string rid)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM SampleAgentDemo WHERE Centre_ID ='" + rid + "' ORDER BY Agent_ID DESC";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getagentmasterdatareader3(string rid)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM SampleAgentDemo WHERE Agent_ID ='" + rid + "' ORDER BY Agent_ID DESC";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getagentmasterdatareader2(string rid)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Agent_Master WHERE Agent_Type = 0 AND Route_ID ='" + rid + "'";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getagentmasterdatareader3(string rid, string type)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Agent_Master WHERE Agent_Type ='" + type + "' AND Route_ID ='" + rid + "'";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getproducermaster(string aid)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Centre_Master WHERE Agent_ID ='" + aid + "'";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getagentdatareader1(string agid)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Agent_Master WHERE  Agent_ID ='" + agid + "'";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getagentdatareader(string agid)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Agent_Master WHERE  Agent_ID ='" + agid + "' AND Agent_Type = 0 ";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getagentmasterdatareader1()
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Agent_Master";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getroutid(string id)
    {
        SqlDataReader dr;
        sqlstr = "select Centre_Master.Route_ID,Centre_Master.Rate_Chart,Route_Master.Route_Name from Centre_Master inner join Route_Master on Centre_Master.Route_Id=Route_Master.Route_ID where Centre_ID=" + id + "";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getroutename(string id)
    {
        SqlDataReader dr;
        sqlstr = "select Route_Name from Route_Master where Route_ID=" + id + "";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }

    public SqlDataReader getagentmasterdatareader(string id)
    {
        SqlDataReader dr;
        sqlstr = "select * from Agent_Master WHERE Table_ID=" + id + "";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }

    public SqlDataReader getagentmasterdatareaderbyroutid(string id)
    {
        SqlDataReader dr;
        sqlstr = "select * from Agent_Master WHERE Route_ID =" + id + " and Agent_Type=1";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public int getmaxAgentid(string rid,string ccode,string pcode)
    {
        
        int lno = 0;
        //string sqlstr = "SELECT MAX(Agent_ID) FROM SampleAgentDemo";
        //agentid = agentDA.ExecuteScalarstr(sqlstr);
        //return agentid;
        try
        {

            string sqlstr = "select max(Agent_ID) from Agent_Master where Route_ID='" + rid + "' and Plant_code='" + pcode + "' AND Company_Code='" + ccode + "'";
            lno = (int)agentDA.ExecuteScalarint(sqlstr);
            lno = ++lno;
            return lno;
        }
        catch
        {
            lno = 0;
        }
        return lno;

    }
    public bool checkagent(int agentid, int routid)
    {
        string sql = "SELECT Agent_ID FROM Agent_Master WHERE Agent_ID =" + agentid + " AND Route_ID=" + routid + "";
        if (agentDA.ExecuteScalarint(sql)> 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public SqlDataReader GetBankID(string ccode,string pcode)
    {
        SqlDataReader dr;
        sqlstr = "SELECT DISTINCT Bank_ID,Bank_Name FROM BANK_MASTER WHERE Company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ORDER BY Bank_Name";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader GetBankIDComm(string ccode)
    {
        SqlDataReader dr;
        sqlstr = "SELECT Bank_ID,Bank_Name FROM Bank_Details WHERE Company_code='" + ccode + "' ORDER BY Bank_ID";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader GetAgentId1(string ccode, string pcode, int rid)
    {
        SqlDataReader dr;
        dr = null;
        sqlstr = "SELECT Agent_id,Agent_Name,phone_number,Type,cartage_Amt,payment_mode,bank_id,ifsc_code,agent_AccountNo,Milk_Nature,bm.branch_name,SplBonus_Amt FROM (SELECT Agent_id,Agent_Name,ISNULL(phone_number,0) AS phone_number,ISNULL(Type,0) AS Type,ISNULL(CAST(cartage_Amt AS DECIMAl(18,2)),0) AS cartage_Amt,payment_mode,ISNULL(bank_id,0) AS bank_id,ISNULL(ifsc_code,0) AS ifsc_code,ISNULL(agent_AccountNo,0) AS agent_AccountNo,Milk_Nature,ISNULL(CAST(SplBonus_Amt AS DECIMAl(18,2)),0) AS SplBonus_Amt from agent_master  WHERE TYPE='0' AND Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "') AS am  LEFT JOIN (SELECT DISTINCT(BANK_ID) AS BID,BANK_NAME,Ifsc_code as bmifsc,branch_name FROM BANK_master WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "') AS bm ON am.bank_id=bm.bid and am.ifsc_code=bm.bmifsc order by agent_id ";
       // sqlstr = "SELECT Agent_id,Agent_Name,ISNULL(phone_number,0) AS phone_number,ISNULL(Type,0) AS Type,ISNULL(CAST(cartage_Amt AS DECIMAl(18,2)),0) AS cartage_Amt,payment_mode,ISNULL(bank_id,0) AS bank_id,ISNULL(ifsc_code,0) AS ifsc_code,ISNULL(agent_AccountNo,0) AS agent_AccountNo,Milk_Nature FROM AGENT_MASTER WHERE TYPE='0' AND Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "' ORDER BY Agent_id";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader GetWeightAgentId(string type,string ccode, string pcode, int rid)
    {
        SqlDataReader dr;
        dr = null;
        //sqlstr = "SELECT Agent_id,Agent_Name FROM AGENT_MASTER WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "'";
        sqlstr = "SELECT Agent_id,Agent_Name,TRuck_id FROM (SELECT Agent_id,Agent_Name,Route_id AS Rid  FROM AGENT_MASTER WHERE Milk_Nature='" + type + "' AND Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "') AS AM LEFT JOIN (SELECT TRuck_id,Route_id AS Ridd FROM TRUCK_RouteAllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "')AS VA ON AM.Rid=vA.Ridd ORDER BY Agent_id";
        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader GetAgentId(string ccode, string pcode, int rid)
    {
        SqlDataReader dr;
        dr = null;
        //sqlstr = "SELECT Agent_id,Agent_Name FROM AGENT_MASTER WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "'";
        // work sqlstr = "SELECT Agent_id,Agent_Name,TRuck_id FROM (SELECT Agent_id,Agent_Name,Route_id AS Rid  FROM AGENT_MASTER WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "') AS AM LEFT JOIN (SELECT TRuck_id,Route_id AS Ridd FROM TRUCK_RouteAllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "')AS VA ON AM.Rid=vA.Ridd ORDER BY Agent_id";
        sqlstr = "SELECT DISTINCT(AM.Agent_Id) AS Agent_id,Agent_Name FROM (SELECT Agent_id,Agent_Name,Route_id AS Rid  FROM AGENT_MASTER WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "') AS AM LEFT JOIN (SELECT TRuck_id,Route_id AS Ridd FROM TRUCK_RouteAllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "')AS VA ON AM.Rid=vA.Ridd ORDER BY Agent_id";

        dr = agentDA.GetDatareader(sqlstr);
        return dr;
    }
    public DataSet GetBankID1(string ccode, string pcode)
    {
        DataSet ds=new DataSet();
        sqlstr = "SELECT Bank_ID,Bank_Name FROM BANK_MASTER WHERE Company_code='" + ccode + "' AND Plant_Code='" + pcode + "'";
        ds = agentDA.GetDataset(sqlstr);
        return ds;
    }
}
