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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Data.SqlClient;

public partial class RatechartPlantwise : System.Web.UI.Page
{
    ReportDocument cr = new ReportDocument();
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    BLLRateChart BLLrate = new BLLRateChart();
    BOLPlantwiseRatechart Bolprate = new BOLPlantwiseRatechart();
    BLLPlantwiseRatechart Blrate = new BLLPlantwiseRatechart();
    public int chlistcount = 0;
    public int Company_code;
    public int plant_Code;
    public string cname;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
             {
                 uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                 Company_code = Convert.ToInt32(Session["Company_code"]);
                 cname = Session["cname"].ToString();
                // plant_Code = Convert.ToInt32(Session["Plant_Code"]);
                
                 LoadPlantName();
                 LoadDdlRatechartCow();
                 LoadDdlRatechartBuff();
                 Plantwiseratechart();
                 plant_Code = Convert.ToInt32(ddl_Plantname.SelectedValue);
             }
             else
             {
                 Server.Transfer("LoginDefault.aspx");
             }
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                Company_code = Convert.ToInt32(Session["Company_code"]);
                cname = Session["cname"].ToString();
                plant_Code = Convert.ToInt32(ddl_Plantname.SelectedValue);
               // plant_Code = Convert.ToInt32(Session["Plant_Code"]);
                Plantwiseratechart();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }

    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    private void LoadPlantName()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst(Company_code.ToString());
            ddl_Plantname.DataSource = ds;
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "plant_Code";
            ddl_Plantname.DataBind();
            if (ddl_Plantname.Items.Count > 0)
            {
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
            }
        }
        catch (Exception ex)
        {
        }

    }
    private void LoadDdlRatechartCow()
    {
        try
        {
            ds = null;
            ds = BLLrate.Loadratechart1(ddl_Plantname.Text, Company_code.ToString());
            ddl_Ratechart.DataSource = ds;
            ddl_Ratechart.DataTextField = "Chart_Name";
            ddl_Ratechart.DataValueField = "Chart_Name";
            ddl_Ratechart.DataBind();
            if (ddl_Ratechart.Items.Count > 0)
            {
            }
            else
            {
                ddl_Ratechart.Items.Add("--Select Ratechartname--");
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void LoadDdlRatechartBuff()
    {
        try
        {
            ds = null;
            ds = BLLrate.LoadratechartBuff(ddl_Plantname.Text, Company_code.ToString());
            ddl_RatechartBuff.DataSource = ds;
            ddl_RatechartBuff.DataTextField = "Chart_Name";
            ddl_RatechartBuff.DataValueField = "Chart_Name";
            ddl_RatechartBuff.DataBind();
            if (ddl_RatechartBuff.Items.Count > 0)
            {
            }
            else
            {
                ddl_RatechartBuff.Items.Add("--Select Ratechartname--");
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_PlantSave_Click(object sender, EventArgs e)
    {
       // if (validates())
        //{
            saveData();
            Plantwiseratechart();
       // }
    }
    private bool validates()
    {
        if (ddl_Plantname.Text == "--Select PlantName--")
        {
            uscMsgBox1.AddMessage("select PlantName", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        if (ddl_Ratechart.Text == "--Select Ratechartname--")
        {
            uscMsgBox1.AddMessage("select Cow Ratechart", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        if (ddl_RatechartBuff.Text == "--Select Ratechartname--")
        {
            uscMsgBox1.AddMessage("select Buffalo Ratechart", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        return true;
    }
    private void saveData()
    {
        try
        {
            string mess = string.Empty;
            SETBO();
            mess = Blrate.InsertPlantwiseratechart(Bolprate);
            string get = mess;
            WebMsgBox.Show(get);
           // uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Success);
        }
        catch (Exception ex)
        {
        }

    }
    private void SETBO()
    {
        Bolprate.Companycode = Company_code;
        Bolprate.Plantcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
        Bolprate.ChartName = ddl_Ratechart.SelectedItem.Value;
        Bolprate.BuffchartName = ddl_RatechartBuff.SelectedItem.Value;

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDdlRatechartCow();
        LoadDdlRatechartBuff();
        Plantwiseratechart();
       
    }

    private void Plantwiseratechart()
    {
        try
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
            cr.Load(Server.MapPath("Crpt_PlantwiseRatechartDisplay.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;


            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];

            t1.Text = Company_code + "_" + cname;
            t2.Text =  ddl_Plantname.SelectedItem.Text;

            
            string str = string.Empty;
            string str1 = string.Empty; 
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            str = "SELECT * FROM (SELECT pcode AS cpcode,RateChartId AS cratechartName,Chart_Type AS ccharttype,Milk_Nature AS cmilknature,Min_Fat AS cminfat,Min_Snf AS cminsnf,CONVERT(NVARCHAR(30),From_Date,103) AS cfrmdate,CONVERT(NVARCHAR(30),To_Date,103) AS ctodate  FROM (SELECT Plant_Code AS pcode,RateChartId FROM PlantwiseRateChart where Plant_Code='" + plant_Code + "') AS t1 LEFT JOIN (SELECT Plant_Code AS pcode1,Chart_Name,Chart_Type,Milk_Nature,State_id,Min_Fat,Min_Snf,From_Date,To_Date FROM Chart_Master where Plant_Code='" + plant_Code + "' and Milk_Nature='Cow') AS t2 ON t1.pcode=t2.pcode1 AND t1.RateChartId=t2.Chart_Name) AS t3 LEFT JOIN (SELECT pcode AS Bpcode,RateChartIdBuff AS BratechartName,Chart_Type AS Bcharttype,Milk_Nature AS Bmilknature,Min_Fat AS Bminfat,Min_Snf AS Bminsnf,CONVERT(NVARCHAR(30),From_Date,103) AS Bfrmdate,CONVERT(NVARCHAR(30),To_Date,103) AS Btodate FROM (SELECT Plant_Code AS pcode,RateChartIdBuff FROM PlantwiseRateChart where Plant_Code='" + plant_Code + "') AS t1 LEFT JOIN (SELECT Plant_Code AS pcode1,Chart_Name,Chart_Type,Milk_Nature,State_id,Min_Fat,Min_Snf,From_Date,To_Date FROM Chart_Master where Plant_Code='" + plant_Code + "' and Milk_Nature='Buffalo') AS t2 ON t1.pcode=t2.pcode1 AND t1.RateChartIdBuff=t2.Chart_Name) AS t4 ON t3.cpcode=t4.Bpcode ";

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }
}
