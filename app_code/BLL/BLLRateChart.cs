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
/// Summary description for BLLRateChart
/// </summary>
public class BLLRateChart
{
    string Sqlstr = string.Empty;
    DALRateChart ratechartDAL = new DALRateChart();
    BOLRateChart rateBOL = new BOLRateChart();
    DbHelper dbaccess = new DbHelper();
    SqlDataReader dar;

    public BLLRateChart()
    {

    }
    public void InsertRatechartData(BOLRateChart ratechartBOL)
    {

        Sqlstr = "Insert_Ratechart";
        ratechartDAL.InsertData(ratechartBOL, Sqlstr);

    }
    public DataTable LoadRatechartGriddata(string ratechart, string pcode, string ccode)
    {
        DataTable dt = new DataTable();
        //Sqlstr = "SELECT Tid,From_Rangevalue,To_Rangevalue,Rate,Commission_Amount,Bonus_Amount FROM Rate_Chart WHERE Chart_Name='" + ratechart + "'";
        Sqlstr = "SELECT Table_ID,From_Rangevalue,To_Rangevalue,CAST(Rate AS DECIMAL(18,2)) AS Rate,CAST(Comission_Amount AS DECIMAL(18,2)) AS Comission_Amount,CAST(Bouns_Amount AS DECIMAL(18,2)) AS Bouns_Amount FROM Rate_Chart WHERE Chart_Name='" + ratechart + "' AND Plant_code='" + pcode + "' and Company_code='" + ccode + "' ORDER BY Table_ID";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }
    public DataTable LoadRatechartGriddata1(string pcode, string ccode)
    {
        DataTable dt = new DataTable();
        //Sqlstr = "SELECT Tid,From_Rangevalue,To_Rangevalue,Rate,Commission_Amount,Bonus_Amount FROM Rate_Chart WHERE Chart_Name='" + ratechart + "'";
        Sqlstr = "SELECT Table_ID,From_Rangevalue,To_Rangevalue,Rate,Comission_Amount,Bouns_Amount FROM Rate_Chart where Plant_code='" + pcode + "' and Company_code='" + ccode + "' ORDER BY Table_ID";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }
    public void EditUpdateRatechart(BOLRateChart ratechartBOL)
    {

        Sqlstr = "Udate_EditRatechart";
        ratechartDAL.EditUpdateRatechart(ratechartBOL, Sqlstr);

    }

    public SqlDataReader getratechartname1(string milktype, string pcode,string ccode)
    {
        SqlDataReader dr;
        Sqlstr = "SELECT Chart_Name FROM Chart_Master where Milk_Nature='" + milktype + "'and Plant_code='" + pcode + "' AND Company_code='" + ccode + "' ORDER BY Chart_Name DESC";
       dr = ratechartDAL.GetDatareader(Sqlstr);
        return dr;
    }
    public void DlelteRow(BOLRateChart ratechartBOL)
    {
        Sqlstr = "Delete_Ratechart";
        ratechartDAL.DeleteRow(ratechartBOL, Sqlstr);
    }
    public SqlDataReader Loadratechart(string type,string pcode, string ccode)
    {
        // Sqlstr = "SELECT Chart_Name FROM Rate_Chart  Table_ID
     //   Sqlstr = "SELECT Distinct(Chart_Name) AS Chart_Name  FROM Chart_Master where Milk_Nature='" + type + "' and Plant_code='" + pcode + "' and Company_code='" + ccode + "' ORDER BY Chart_Name DESC";

        Sqlstr = "SELECT (Chart_Name) AS Chart_Name  FROM Chart_Master where Milk_Nature='" + type + "' and Plant_code='" + pcode + "' and Company_code='" + ccode + "' ORDER BY Table_ID DESC";
        dar = dbaccess.GetDatareader(Sqlstr);
        return dar;
    }
    public string getratechartvalue(string range, string name)
    {
        name = name.Trim();
        //strsql = "SELECT ratechart.tsRate FROM ratechart INNER JOIN chart_Master ON ratechart.chart_Name = chart_Master.chart_Name WHERE " + range + " BETWEEN range_From AND range_To AND ratechart.chart_Name='" + name + "'";
        //string value = ratechartDAL.ExecuteScalarstr(strsql);
        Sqlstr = "SELECT Rate_Chart.Rate FROM Rate_Chart INNER JOIN Chart_Master ON Rate_Chart.Chart_Name=Chart_Master.Chart_Name WHERE " + range + " BETWEEN From_RangeValue AND TO_RangeValue AND Rate_Chart.Chart_Name='" + name + "'";
        string value = dbaccess.ExecuteScalarstr(Sqlstr);
        return value;
    }
    public DataSet Loadratechart1(string pcode, string ccode)
    {
        DataSet ds = new DataSet();
        ds = null;
		Sqlstr = "SELECT Distinct(Chart_Name) AS Chart_Name  FROM Chart_Master where Plant_code='" + pcode + "' and Company_code='" + ccode + "' and Active='1' and Milk_nature='Cow' ORDER BY Chart_Name DESC";
        ds = dbaccess.GetDataset(Sqlstr);
        return ds;
    }
    public DataSet LoadratechartBuff(string pcode, string ccode)
    {
        DataSet ds = new DataSet();
        ds = null;
		Sqlstr = "SELECT Distinct(Chart_Name) AS Chart_Name  FROM Chart_Master where Plant_code='" + pcode + "' and Company_code='" + ccode + "'  and Active='1' and Milk_nature='Buffalo' ORDER BY Chart_Name DESC";
        ds = dbaccess.GetDataset(Sqlstr);
        return ds;
    }

    public SqlDataReader RateChartmode(string ccode, string pcode)
    {
        SqlDataReader dr;
        dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT * FROM PLANTWISERATECHART WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }
    public SqlDataReader RateChartmodeBuff(string ccode, string pcode,string Rid)
    {
        SqlDataReader dr;
        dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT * FROM ROUTEWISERATECHART WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Route_Id='" + Rid + "' ";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }
    public SqlDataReader PlantRouteChartId(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT *FROM RateChartPlantRouteChoose WHERE Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }
    public SqlDataReader PlantRouteChartIdCow1(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT Distinct(RatechartId) AS Cratechart FROM PlantwiseRateChart WHERE Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }

    public SqlDataReader RouteChartIdCow1(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT Distinct(Ratechart_Id) AS Cratechart FROM RoutewiseRateChart WHERE Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }
    public SqlDataReader PlantRouteChartIdBuff2(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT Distinct(RatechartIdBuff) AS Cratechart FROM PlantwiseRateChart WHERE Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }
    public SqlDataReader RouteChartIdBuff2(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT Distinct(Ratechart_IdBuff) AS Cratechart FROM RoutewiseRateChart WHERE Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }
}
