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
/// Summary description for BLLStateMaster
/// </summary>
public class BLLStateMaster
{
    DALStateMaster statemasterDA = new DALStateMaster();
    string sqlstr;
	public BLLStateMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertstatemaster(BOLStateMaster statemasterBO)
    {
        string sql = "Insert_StateMaster";
        statemasterDA.insertStatemaster(statemasterBO, sql);

    }

    //public void deletestatemaster(BOLstatemaster statemasterBO)
    //{
    //    string sql = "delete_Statemaster";
    //    statemasterDA.deleteStatemaster(statemasterBO, sql);
    //}
    public void updateagentmaster()
    {

    }
    public DataTable getstatemastertable()
    {
        sqlstr = "select State_ID,State_Name from State_Master";
        DataTable dt = new DataTable();
        dt = statemasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public SqlDataReader getstatemasterdatareader(string id)
    {
        SqlDataReader dr;
        sqlstr = "select * from State_Master WHERE State_ID=" + id + "";
        dr = statemasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getstatemasterdatareaderbyagent(string id, string rout)
    {
        SqlDataReader dr;
        // sqlstr = "SELECT  t1.center_Id, t1.agent_Id, t1.state_Id, t1.rate_Chart AS ratechart, t1.route_Id FROM  (SELECT     agent_Master.agent_Id, agent_Master.center_Id, agent_Master.route_Id, center_Master.state_Id, center_Master.rate_Chart FROM agent_Master INNER JOIN center_Master ON agent_Master.center_Id = center_Master.center_Id) AS t1 INNER JOIN state_Master ON t1.state_Id = state_Master.state_Id WHERE t1.agent_Id = '" + id + "' AND t1.route_Id ='"+ rout +"'";
        sqlstr = "SELECT Centre_ID,Route_Master.State_ID,Agent_ID,Agent_Master.Rate_Chart as rc,Agent_Master.Cartage,Agent_Master.Trucks_ID,Agent_Master.Milknature_Type, Route_Master.Route_ID FROM Agent_Master INNER JOIN Route_Master ON Agent_Master.Route_ID = Route_Master.Route_ID WHERE Agent_Master.Agent_ID = '" + id + "' AND Route_Master.Rout_ID ='" + rout + "'";
        dr = statemasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getstatemasterdatareader1(string  cmpcode, string  pcode)
    {
        SqlDataReader dr;
        sqlstr = "select State_ID,State_Name from State_Master WHERE Company_Code=" + cmpcode + " AND Plant_Code=" + pcode + " ORDER BY State_ID";
        dr = statemasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public int getstatemasterid(int cmpcode, int pcode)
    {
        int lno = 1;
        try
        {
            string sqlstr = "SELECT MAX(State_ID) FROM State_Master  WHERE Company_Code=" + cmpcode + " AND Plant_Code=" + pcode + "";
            lno = (int)statemasterDA.ExecuteScalarint(sqlstr);
            lno = ++lno;
            return lno;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return lno;
    }
    
}
