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




public class BLLroutmaster
{
    DALroutmaster routmasterDA = new DALroutmaster();
    string sqlstr;
    public BLLroutmaster()
    {
        sqlstr = string.Empty;
    }
    public string insertroutmaster(BOLroutmaster routmasterBO)
    {
        string mes = string.Empty;
        string sql = "insert_Routmaster";
        mes=routmasterDA.insertRoutmaster(routmasterBO, sql);
        return mes; 

    }

    public void deleteroutmaster(BOLroutmaster routmasterBO)
    {
        string sql = "delete_Routmaster";
        routmasterDA.deleteRoutmaster(routmasterBO, sql);
    }
    public void updateagentmaster()
    {

    }
    public DataTable getroutmastertable()
    {
        sqlstr = "select * from Route_Master ORDER BY Route_ID";
        DataTable dt = new DataTable();
        dt = routmasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public SqlDataReader getstatemasterdatareader()
    {
        SqlDataReader dr;
        sqlstr = "SELECT state_Id,state_Name from state_Master";
        dr = routmasterDA.GetDatareader(sqlstr);
        return dr;

    }
    public SqlDataReader getroutmasterdatareader(string id)
    {
        SqlDataReader dr;
        sqlstr = "select * from rout_Master WHERE tid=" + id + "";
        dr = routmasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getroutmasterdatareader(string ccode,string pcode)
    {
        SqlDataReader dr;
        sqlstr = "Select Route_ID,Route_Name from Route_Master WHERE Company_code='" + ccode + "'  AND PLANT_CODE='"+pcode+"' ORDER BY Route_ID  ";
        //sqlstr = "select * from rout_Master";
        dr = routmasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getroutmasterdatareader1(string id)
    {
        SqlDataReader dr;
        sqlstr = "select * from rout_Master WHERE state_Id=" + id + "";
        dr = routmasterDA.GetDatareader(sqlstr);
        return dr;
    }
   


    public int getrouteid(string ccode,string pcode)
    {
        int bno = 0;        
        try
        {
            sqlstr = "SELECT MAX(Route_ID) FROM Route_Master WHERE Company_code='" + ccode + "' AND plant_Code='" + pcode + "'";
            bno = (int)routmasterDA.ExecuteScalarint(sqlstr);
            bno = ++bno;
            return bno;
            

        }
        catch (Exception ex)
        {
            //bno = 11;
            bno = 1;
        }
        return bno;
    }
    public DataSet getroutmasterdatareader2(string ccode, string pcode)
    {
        DataSet ds = new DataSet();
        ds = null;
        sqlstr = "Select Route_ID,CONVERT(nvarchar(50),Route_ID)+'_'+Route_Name AS Route_Name  from Route_Master WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ORDER BY Route_ID  ";
        //sqlstr = "select * from rout_Master";
        ds = routmasterDA.GetDataset(sqlstr);
        return ds;
    }
    public DataSet getroutmasterdatareader3(string ccode, string pcode)
    {
        DataSet ds = new DataSet();
        ds = null;
        sqlstr = "select Route_ID,CAST(Route_ID as NVARCHAR)+'_'+Route_Name as Route_Name from Route_Master WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ORDER BY ROUTE_ID  ";
        //sqlstr = "select * from rout_Master";
        ds = routmasterDA.GetDataset(sqlstr);
        return ds;
    }
    public SqlDataReader getroutmasterdatareader4(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        sqlstr = "Select Tot_Distance, ROUTE_ID,Route_Name from Route_Master WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ORDER BY ROUTE_ID  ";
        //sqlstr = "select * from rout_Master";
        dr = routmasterDA.GetDatareader(sqlstr);
        return dr;
    }

    public DataTable GraphMilkSummary(string ccode, string pcode, string fdate, string tdate)
    {

        // sqlstr = "SELECT Route_id,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "'  GROUP BY Route_id ORDER BY Route_id";
        sqlstr = "SELECT t2.Route_ID,t1.Smkg,t1.Afat,t1.ASnf FROM (SELECT Route_id,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "'  GROUP BY Route_id ) AS t1 LEFT JOIN  (SELECT Route_ID AS Rid, CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Route_ID)+'__'+Route_Name)) AS Route_ID  FROM Route_Master WHERE Plant_Code='" + pcode + "') AS t2 ON t1.Route_id=t2.Rid ORDER BY t1.Route_id";
        DataTable dt = new DataTable();
        dt = routmasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public DataTable GraphMilkSummaryplantwise(string ccode, string fdate, string tdate)
    {
        //sqlstr = "SELECT DISTINCT(Plant_code) AS Route_id,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "'  GROUP BY Plant_code ORDER BY Plant_code";
        sqlstr = "SELECT Route_id+'_'+Plant_Name AS Route_id ,Smkg,Afat,ASnf FROM (SELECT DISTINCT(Plant_code) AS Route_id,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "'  GROUP BY Plant_code ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' ) AS t2 ON t1.Route_id=t2.Plant_Code ORDER BY Plant_code";
        DataTable dt = new DataTable();
        dt = routmasterDA.GetDatatable(sqlstr);
        return dt;
    }

    public DataTable GraphAgentCountSummary(string ccode, string pcode,string RRCODE  ,string fdate, string tdate)
    {

        // sqlstr = "SELECT route_id,count(agent_id) AS Agentcount FROM Agent_Master  WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "'  GROUP BY Route_id ORDER BY Route_id";
        sqlstr = "SELECT t2.Route_ID,t1.Agentcount FROM (SELECT route_id,count(agent_id) AS Agentcount FROM Agent_Master  WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND  ROUTE_ID='" + RRCODE + "'  GROUP BY Route_id ) AS t1 LEFT JOIN  (SELECT Route_ID AS Rid, CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Route_ID)+'__'+Route_Name)) AS Route_ID  FROM Route_Master WHERE Plant_Code='" + pcode + "'   AND  ROUTE_ID='" + RRCODE + "') AS t2 ON t1.Route_id=t2.Rid ORDER BY t1.Route_id ";
        DataTable dt = new DataTable();
        dt = routmasterDA.GetDatatable(sqlstr);
        return dt;
    }

    
    public DataTable GraphAgentCountSummaryplantwise(string ccode, string fdate, string tdate)
    {

        // sqlstr = "SELECT DISTINCT(Plant_code) AS route_id,count(agent_id) AS Agentcount FROM Agent_Master  WHERE Company_code='" + ccode + "' GROUP BY Plant_code ORDER BY Plant_code";
        sqlstr = "SELECT t2.route_id,t1.Agentcount FROM (SELECT DISTINCT(Plant_code) AS route_id,count(agent_id) AS Agentcount FROM Agent_Master  WHERE Company_code='" + ccode + "' AND Plant_code>149  GROUP BY Plant_code) AS t1 INNER JOIN (SELECT Plant_Code,CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Plant_Code)+'_'+Plant_Name)) AS route_id FROM Plant_Master WHERE Company_Code='" + ccode + "') AS t2 ON t1.route_id=t2.Plant_Code ORDER BY t1.route_id";
        DataTable dt = new DataTable();
        dt = routmasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public DataTable GraphRemarkscountSummary(string ccode, string pcode, string fdate, string tdate)
    {
        //sqlstr = "SELECT Route_id,Count(Remarkstatus) AS Remarkscount FROM procurementimport WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "' AND Remarkstatus=1  GROUP BY Route_id ORDER BY Route_id  ";
        sqlstr = "SELECT t2.route_id,t1.Remarkscount FROM (SELECT Route_id,Count(Remarkstatus) AS Remarkscount FROM procurementimport WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "' AND Remarkstatus=2  GROUP BY Route_id ) AS t1 INNER JOIN (SELECT Route_ID AS Rid, CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Route_ID)+'__'+Route_Name)) AS Route_ID  FROM Route_Master WHERE Plant_Code='" + pcode + "') AS t2 ON t1.route_id=t2.Rid  ORDER BY Route_id ";
        DataTable dt = new DataTable();
        dt = routmasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public DataTable GraphRemarkscountSummaryplantwise(string ccode, string fdate, string tdate)
    {
        //sqlstr = "SELECT DISTINCT(Plant_code) AS Route_id,Count(Remarkstatus) AS Remarkscount FROM procurementimport WHERE Company_code='" + ccode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "' AND Remarkstatus='2'  GROUP BY Plant_code ORDER BY Plant_code  ";
        sqlstr = "SELECT t2.route_id,t1.Remarkscount FROM (SELECT DISTINCT(Plant_code) AS Route_id,Count(Remarkstatus) AS Remarkscount FROM procurementimport WHERE Company_code='" + ccode + "' AND  prdate BETWEEN '" + fdate + "' AND '" + tdate + "' AND Remarkstatus='2'  GROUP BY Plant_code  ) AS t1  INNER JOIN (SELECT Plant_Code,CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Plant_Code)+'_'+Plant_Name)) AS route_id FROM Plant_Master WHERE Company_Code='" + ccode + "') AS t2 ON t1.route_id=t2.Plant_Code ORDER BY Plant_code ";
        DataTable dt = new DataTable();
        dt = routmasterDA.GetDatatable(sqlstr);
        return dt;
    }

}

