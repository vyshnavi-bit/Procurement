using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text;
using System.Web.Services;


public partial class JSONData : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    DataTable dt = new DataTable();
    DataTable silostock = new DataTable();
    int i = 0;
    int j = 0;
    int K = 0;
    int L = 0;
    int M = 0;
    int N = 0;
    double SUMOPPRO = 0;
    double SUMOPPRO1 = 0;
    double ASSIGNVALUE;
    int JK;
    int I;
    int JHG = 0;
    public static int roleid;
    public string sjson;
    public string jsondata = GetJsonData();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]//webmethod for a static getjsondata function so that the method can be accessed using jquery in aspx page.
    public static string GetJsonData()
    {
        //sql connection
        SqlConnection con = new SqlConnection();
        DbHelper DB = new DbHelper();
        string GETVALUES = "";
        GETVALUES = "sELECT PLANT_CODE,Sum(milk_kg) as Milkkg   FROM   procurement    where  prdate between '4-1-2017' and '4-30-2017' group by PLANT_CODE  order by  PLANT_CODE asc ";
        con = DB.GetConnection();
        SqlCommand cmd1 = new SqlCommand(GETVALUES, con);
        SqlDataAdapter dsp1 = new SqlDataAdapter(cmd1);
        DataTable silostock = new DataTable();
        dsp1.Fill(silostock);
        SqlDataReader dr;
        dr = cmd1.ExecuteReader();

        //create a JSON string to describe the data from the database

        StringBuilder JSON = new StringBuilder();
        string prefix = "";
        JSON.Append("[");
        while (dr.Read())
        {
            JSON.Append(prefix + "{");
            JSON.Append("\"PLANT_CODE\":" + "\"" + dr[0] + "\",");
            JSON.Append("\"Milkkg\":" + dr[1]);
            JSON.Append("}");
            prefix = ",";
        }
        JSON.Append("]");

        return JSON.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write(jsondata);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}