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
/// Summary description for BLLProcurement
/// </summary>
public class BLLProcurement
{
    DALProcurement procurementDA = new DALProcurement();
    DbHelper dbaccess = new DbHelper();
    string sqlstr;
	public BLLProcurement()
	{
        sqlstr = string.Empty;
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertprocurement(BOLProcurement procurementBO)
    {
        string sql = "insert_Procurement";
        procurementDA.insertPProcurement(procurementBO, sql);

    }
    public void setprocurementdata(BOLProcurement prucrementBO)
    {
        string sql = "set_procurementdata";
        procurementDA.setprocurementdata(prucrementBO, sql);
    }
    public SqlDataReader getbuffsubdata(string value)
    {
        SqlDataReader dr;
        string sqlstr = "SELECT * FROM Buffdeduadd WHERE " + value + " BETWEEN rangefrom AND rangeto ";
        dr = procurementDA.GetDatareader(sqlstr);
        return dr;
    }
    public DataTable getProcurementtable()
    {
        string sql = "select * from WeightDemoTable";
        DataTable dt = new DataTable();
        dt = procurementDA.GetDatatable(sql);
        return dt;
    }
    public string getsampleno(string date, string session,string ccode,string pcode)
    {
        sqlstr = "SELECT MAX(sample_No) FROM sampleno WHERE WDate='" + date + "'AND Session='" + session + "' AND Company_Code=" + ccode + " AND Plant_Code=" + pcode + "";
        string sno = procurementDA.ExecuteScalarstr(sqlstr);
        int n = 0;
        if (!(string.IsNullOrEmpty(sno)))
            n = int.Parse(sno);
        n++;
        return n.ToString();
    }
    public SqlDataReader getvaluebysampleno(string sampleno,string ccode,string pcode)
    {
        SqlDataReader dr;
        //string sqlstr = "SELECT * FROM WeightDemoTable WHERE sample_No = '" + sampleno + "'";
        string sqlstr = "SELECT * FROM procurement WHERE sampleno = '" + sampleno + "' AND Company_code='" + ccode + "' AND plant_code='" + pcode + "'";
        dr = procurementDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader sessionsamples(string ccode,string pcode,string date, string session)
    {
        SqlDataReader dr;
        sqlstr = "SELECT sampleno FROM procurement WHERE company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND prdate='" + date + "' AND Sessions='" + session + "'AND fat <= 0 ORDER BY sampleno";
       // sqlstr = "SELECT sample_No FROM WeightDemoTable WHERE WDate='" + date + "'AND Session='" + session + "'AND fat <= 0";

        dr = procurementDA.GetDatareader(sqlstr);
        return dr;
    }
    public bool checkratechart(string rcname, string datenow,string ccode,string pcode)
    {
        string sqlstr, val;
        val = string.Empty;
        sqlstr = "SELECT * FROM Chart_Master WHERE '" + datenow + "' BETWEEN From_Date AND To_Date AND Chart_Name='" + rcname.Trim() + " ' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ";
        val = procurementDA.ExecuteScalarstr(sqlstr);

        if (string.IsNullOrEmpty(val))
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public SqlDataReader getratechartdatareader(string rcval, string ratechart,string ccode,string pcode)
    {
        SqlDataReader dr;
        string sqlstr = "SELECT Rate, Comission_Amount, Bouns_Amount FROM Rate_Chart WHERE '" + rcval + "' BETWEEN From_RangeValue AND To_RangeValue AND Chart_Name='" + ratechart.Trim() + "' AND company_code='" + ccode + "'AND plant_code='" + pcode + "' ";
        dr = procurementDA.GetDatareader(sqlstr);
        return dr;
    }
    
}
