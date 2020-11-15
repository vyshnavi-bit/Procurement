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
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;
using System.Web.Services;
using System.Collections.Generic;
public partial class OrganizationChart : System.Web.UI.Page
{
    
    string get;
   

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    string FDATE;
    string TODATE;
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    SqlDataReader dr;
    int datasetcount = 0;

    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataTable fatkgdt = new DataTable();

    string d1;
    string d2;
    string agentcode;
    int countinsertdetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    dtm = System.DateTime.Now;
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    Session["tempp"] = Convert.ToString(pcode);
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                
                    //   txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        BindOrganaizationChart();
                       
                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        BindOrganaizationChart();
                       
                    }

                }
                else
                {



                }

               
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;
                BindOrganaizationChart();
              
            }
            
        }

        catch
        {



        }

    }

    private void BindOrganaizationChart()
    {
        StringBuilder str = new StringBuilder();
        DataTable dt = new DataTable();
        con=  DB.GetConnection();
        try
        {

            string cmd = "select [Tid]  ,[name]  ,[parent]  ,[ToolTip]  from OragantionChart where plant_code='"+ddl_Plantname.SelectedItem.Value+"'";
            SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
            adp.Fill(dt);

           
            str.Append(@"<script type='text/javascript'> google.charts.load('current', { packages: ['orgchart'] });
                        google.charts.setOnLoadCallback(drawChart);
                       function drawChart() {
       var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Manager');
            data.addColumn('string', 'ToolTip');  
 
        data.addRows([");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    str.Append(" ['" + dt.Rows[i]["name"].ToString() + "', '" + dt.Rows[i]["parent"].ToString() + "', '" + dt.Rows[i]["ToolTip"].ToString() + "' ]");
                }
                else
                {
                 //   get += "<a href='SMSTESTINGPAGE.aspx'>Link one</a>";
                    str.Append(" ['" + dt.Rows[i]["name"].ToString() + "', '" + dt.Rows[i]["parent"].ToString() + "', '" + dt.Rows[i]["ToolTip"].ToString() + "' ],");

                }
            }
            str.Append("]);");

            str.Append("  var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));");
            str.Append("  chart.draw(data, { allowHtml: true });");

            str.Append("}");
            str.Append("</script>");
            ltrScript.Text = str.ToString();
        }
        catch
        { }
    }


    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

        datasetcount = datasetcount + 1;
    }


    public void loadsingleplant()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE      plant_code  not in (150,139) AND Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
        datasetcount = datasetcount + 1;
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
      //  Context.Response.Write("<script> language='javascript'>window.open('OrgReport1.aspx','_newtab');</script>");
        Session["TPCK"] = pcode.ToString();
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button9_Click1(object sender, EventArgs e)
    {
      //  Session["TPCK"] = pcode.ToString();
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        BindOrganaizationChart();
        Session["TPCK"] = ddl_Plantname.SelectedItem.Value;
    }
}