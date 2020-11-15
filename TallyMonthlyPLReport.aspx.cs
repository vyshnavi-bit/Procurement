using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using System.Text;
public partial class TallyMonthlyPLReport : System.Web.UI.Page
{
    DbHelper DB = new DbHelper();
    SqlConnection con = new SqlConnection();
    string planno;
    DateTime dtm = new DateTime();
    public static int roleid;
    public string ccode;
    public string pcode;
    string msg;
    string nn;
    int CHECKVAL;
    string str;
    DataTable COWPLANT = new DataTable();
    DataTable dts = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                dtm = System.DateTime.Now;

                nn = Session["Name"].ToString();


                // nn = "hh";
                //  getgroup();

                //}
            }

        }
        else
        {

            //getgroup();



        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        LoadPlantcode();
        getreports();
  
    }
    public void getreports()
    {
       

        int N = COWPLANT.Rows.Count;
        dts.Columns.Clear();
        dts.Columns.Add("PARTICULARS");
        LoadPlantcode();
        string[] gets;

        dts.Columns.Add("");
        dts.Columns.Add(str);
        dts.Columns.Add("");

        foreach (DataRow sk in COWPLANT.Rows)
        {

          str =sk[1].ToString();
        

        }
        GridView2.DataSource = dts;
        GridView2.DataBind();
    }
    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
       //    string stt = "SELECT Plant_Code as null, Plant_Name,plant_code as null FROM Plant_Master where plant_code   in (155,156,158,159,161,162,163,164) ";
            string stt = "SELECT    plant_code,Plant_Name  FROM Plant_Master where plant_code   in (155,156,158,159,161,162,163,164)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            COWPLANT.Rows.Clear();
            DA.Fill(COWPLANT);
          
        }
        catch
        {


        }

    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}