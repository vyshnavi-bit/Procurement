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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class RateChartRoutewise : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    BLLroutmaster routeBL = new BLLroutmaster();
    BLLRateChart BLLrate = new BLLRateChart();
    BLLPlantwiseRatechart Brate = new BLLPlantwiseRatechart();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    BOLPlantwiseRatechart Bolprate = new BOLPlantwiseRatechart();
    public int Company_code ;
    public int plant_Code;
    public string cname;
    public  int rid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                Company_code = Convert.ToInt32(Session["Company_code"]);
                plant_Code = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                LoadPlantName();
                plant_Code = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
                Loadrouteid();
                rid = Convert.ToInt32(ddl_RouteName.SelectedItem.Value);
                LoadAgentid();
                LoadDdlRatechart();
                LoadDdlRatechartBuff();

                if (Chk_Agentwise.Checked == true)
                {
                    lbl_AgentName.Visible = true;
                    ddl_AgentName.Visible = true;
                    btn_AgentSave.Visible = true;
                    btn_RouteSave.Visible = false;
                }
                else
                {
                    lbl_AgentName.Visible = false;
                    ddl_AgentName.Visible = false;
                    btn_AgentSave.Visible = false;
                    btn_RouteSave.Visible = true;
                }
                Routewiseratechart();    
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
                plant_Code = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                plant_Code = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
                rid = Convert.ToInt32(ddl_RouteName.SelectedItem.Value);
                Routewiseratechart();    
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

            ddl_Plantname.DataTextField = "Plant_Name" ;
            ddl_Plantname.DataValueField = "plant_Code";
            ddl_Plantname.DataBind();

            if (ddl_Plantname.Items.Count > 0)
            {
                ddl_Ratechart.Items.Add("--Select COWRatechartname--");
                ddl_RatechartBuff.Items.Add("--Select BuffRatechartname--");
            }

            else
            {
                ddl_Plantname.Items.Add("--Select Plantname--");
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void Loadrouteid()
    {
        try
        {
            ds = null;
            ds = routeBL.getroutmasterdatareader2(Company_code.ToString(), ddl_Plantname.Text);
            ddl_RouteName.DataSource = ds;
            
                ddl_RouteName.DataTextField = "Route_Name";
                ddl_RouteName.DataValueField = "Route_ID";
                ddl_RouteName.DataBind();
                if (ddl_RouteName.Items.Count > 0)
                {
                }
                else
                {
                    ddl_RouteName.Items.Add("--Select Routetname--");
                }
        }
        catch (Exception ex)
        {

        }

    }
    private void LoadDdlRatechart()
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
                ddl_Ratechart.Items.Add("--Select CowRatechartname--");
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
                ddl_RatechartBuff.Items.Add("--Select BuffRatechartname--");
            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void btn_RouteSave_Click(object sender, EventArgs e)
    {
      //  if (validates())
       // {
            SaveData();
            Routewiseratechart();

       // }
    }
    private bool validates()
    {
        if (ddl_Plantname.Text == "--Select Plantname--")
        {
            uscMsgBox1.AddMessage("select PlantName", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        if (ddl_RouteName.Text == "--Select Routetname--")
        {
            uscMsgBox1.AddMessage("select Routetname", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        if (ddl_Ratechart.Text == "--Select CowRatechartname--")
        {
            uscMsgBox1.AddMessage("select Cow Ratechart", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        if (ddl_RatechartBuff.Text == "--Select BuffRatechartname--")
        {
            uscMsgBox1.AddMessage("select Buffalo Ratechart", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        if (ddl_AgentName.Text == "--Select Agentname--")
        {
            uscMsgBox1.AddMessage("select Agentname", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }

        return true;
    }
    private void SaveData()
    {
        try
        {
        string mess = string.Empty;
        SETBO();
        if (Chk_Agentwise.Checked == true)
        {
            mess = Brate.InsertPlantAgentwiseratechart(Bolprate);
            //uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Success);
            string get = mess;
            WebMsgBox.Show(get);

        }
        else
        {
            mess = Brate.InsertRoutewiseratechart(Bolprate);
            string get = mess;
            WebMsgBox.Show(get);
            //uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Success);
        }

        
        
        }
        catch (Exception ex)
        {
        }
        
    }
    //private bool validates()
    //{
    //    if (ddl_Plantname.Text == "")
    //    {

    //        return true;
    //    }
    //    return false;
    //}
    private void SETBO()
    {
        try
        {
            Bolprate.Companycode = Company_code;
            Bolprate.Plantcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
            Bolprate.RouteId = Convert.ToInt32(ddl_RouteName.SelectedItem.Value);
            Bolprate.ChartName = ddl_Ratechart.SelectedItem.Value;
            Bolprate.BuffchartName = ddl_RatechartBuff.SelectedItem.Value;
            //
            Bolprate.AgentId = Convert.ToInt32(ddl_AgentId.SelectedItem.Value);
            Bolprate.Curtimestamp = System.DateTime.Now;
        }
        catch (Exception ex)
        {          
          
        }        
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Loadrouteid();
        LoadAgentid();
        ddl_Ratechart.Items.Clear();
        LoadDdlRatechart();
        LoadDdlRatechartBuff();
        Routewiseratechart();
    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAgentid();
        Routewiseratechart();
       // LoadDdlRatechart();
    }
    protected void btn_AgentSave_Click(object sender, EventArgs e)
    {
        if (validates())
        {
            SaveData();
            Routewiseratechart();
        }
    }
    protected void Chk_Agentwise_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_Agentwise.Checked == true)
        {
            lbl_AgentName.Visible = true;
            ddl_AgentName.Visible = true;
            btn_AgentSave.Visible = true;
            btn_RouteSave.Visible = false;
            Chk_Agentwise.Checked = true;
            Routewiseratechart();
        }
        else
        {
            lbl_AgentName.Visible = false;
            ddl_AgentName.Visible = false;
            btn_AgentSave.Visible = false;
            btn_RouteSave.Visible = true;
            Chk_Agentwise.Checked = false;
            Routewiseratechart();
        }
    }
    private void LoadAgentid()
    {
        try
        {
            SqlDataReader dr = null;
            dr = null;
            dr = agentBL.GetAgentId(Company_code.ToString(), plant_Code.ToString(),rid);
            ddl_AgentId.Items.Clear();
            ddl_AgentName.Items.Clear();
            
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_AgentId.Items.Add(dr["Agent_id"].ToString());
                    ddl_AgentName.Items.Add(dr["Agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
                }
            }
            else
            {
                ddl_AgentId.Items.Add("--Select AgentId--");
                ddl_AgentName.Items.Add("--Select Agentname--");
                WebMsgBox.Show("Agent is Not Added");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void ddl_AgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_AgentId.SelectedIndex = ddl_AgentName.SelectedIndex;
    }

    private void Routewiseratechart()
    {
        try
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
            if (Chk_Agentwise.Checked == true)
            {
                cr.Load(Server.MapPath("Crpt_AgentwiseRatechartDisplay.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Crpt_RoutewiseRatechartDisplay.rpt"));
            }           

            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            
            t1.Text = Company_code + "_" + cname;
            t2.Text = ddl_Plantname.SelectedItem.Text;
            

            string str = string.Empty;
            string str1 = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            if (Chk_Agentwise.Checked == true)
            {
				str = "SELECT * FROM (SELECT * FROM (SELECT pcode AS cpcode,AgentRatechart_Idcow AS cratechartName,Chart_Type AS ccharttype,Milk_Nature AS cmilknature,Min_Fat AS cminfat,Min_Snf AS cminsnf,CONVERT(NVARCHAR(30),From_Date,103) AS cfrmdate,CONVERT(NVARCHAR(30),To_Date,103) AS ctodate,Agent_Id AS AgentId  FROM (SELECT Plant_Code AS pcode,Agent_Id,AgentRatechart_Idcow FROM AgentwiseRatechart where Plant_Code='" + plant_Code + "' ) AS t1 LEFT JOIN (SELECT Plant_Code AS pcode1,Chart_Name,Chart_Type,Milk_Nature,State_id,Min_Fat,Min_Snf,From_Date,To_Date FROM Chart_Master where Plant_Code='" + plant_Code + "'   and Active='1' ) AS t2 ON t1.pcode=t2.pcode1 AND t1.AgentRatechart_Idcow=t2.Chart_Name ) AS t3 LEFT JOIN (SELECT pcode AS Bpcode,AgentRatechart_IdBuff AS BratechartName,Chart_Type AS Bcharttype,Milk_Nature AS Bmilknature,Min_Fat AS Bminfat,Min_Snf AS Bminsnf,CONVERT(NVARCHAR(30),From_Date,103) AS Bfrmdate,CONVERT(NVARCHAR(30),To_Date,103) AS Btodate,Agent_Id  AS AId FROM (SELECT Plant_Code AS pcode,Agent_Id,AgentRatechart_IdBuff FROM AgentwiseRatechart where Plant_Code='" + plant_Code + "' ) AS t1 LEFT JOIN (SELECT Plant_Code AS pcode1,Chart_Name,Chart_Type,Milk_Nature,State_id,Min_Fat,Min_Snf,From_Date,To_Date FROM Chart_Master where Plant_Code='" + plant_Code + "' and Active='1' ) AS t2 ON t1.pcode=t2.pcode1 AND t1.AgentRatechart_IdBuff=t2.Chart_Name) AS t4 ON t3.cpcode=t4.Bpcode AND t3.AgentId=t4.AId) AS t5 LEFT JOIN (SELECT pcod,Route_id,Agent_Id AS AgentId,Agent_Name AS AgentName,Route_Name AS Route_Name FROM (SELECT Plant_Code AS pcodeR,Route_ID AS Rid,Route_Name FROM Route_Master WHERE Plant_Code='" + plant_Code + "') AS t6 LEFT JOIN (SELECT Plant_Code AS pcod,Route_ID,Agent_Id,Agent_Name FROM Agent_Master WHERE Plant_Code='" + plant_Code + "') AS t7 ON t6.pcodeR=t7.pcod AND t6.Rid=t7.Route_id) AS t8 ON t5.AgentId=t8.AgentId AND t5.cpcode=t8.pcod ";
            }
            else
            {
				str = "SELECT * FROM (SELECT * FROM (SELECT pcode AS cpcode,Ratechart_Id AS cratechartName,Chart_Type AS ccharttype,Milk_Nature AS cmilknature,Min_Fat AS cminfat,Min_Snf AS cminsnf,CONVERT(NVARCHAR(30),From_Date,103) AS cfrmdate,CONVERT(NVARCHAR(30),To_Date,103) AS ctodate,Route_Id  FROM (SELECT Plant_Code AS pcode,Route_Id,Ratechart_Id FROM RoutewiseRateChart where Plant_Code='" + plant_Code + "' ) AS t1 LEFT JOIN (SELECT Plant_Code AS pcode1,Chart_Name,Chart_Type,Milk_Nature,State_id,Min_Fat,Min_Snf,From_Date,To_Date FROM Chart_Master where Plant_Code='" + plant_Code + "' and Active='1' ) AS t2 ON t1.pcode=t2.pcode1 AND t1.Ratechart_Id=t2.Chart_Name ) AS t3 LEFT JOIN (SELECT pcode AS Bpcode,Ratechart_IdBuff AS BratechartName,Chart_Type AS Bcharttype,Milk_Nature AS Bmilknature,Min_Fat AS Bminfat,Min_Snf AS Bminsnf,CONVERT(NVARCHAR(30),From_Date,103) AS Bfrmdate,CONVERT(NVARCHAR(30),To_Date,103) AS Btodate,Ridd FROM (SELECT Plant_Code AS pcode,Route_Id AS Ridd,Ratechart_IdBuff FROM RoutewiseRateChart where Plant_Code='" + plant_Code + "') AS t1 LEFT JOIN (SELECT Plant_Code AS pcode1,Chart_Name,Chart_Type,Milk_Nature,State_id,Min_Fat,Min_Snf,From_Date,To_Date FROM Chart_Master where Plant_Code='" + plant_Code + "' and Active='1' ) AS t2 ON t1.pcode=t2.pcode1 AND t1.Ratechart_IdBuff=t2.Chart_Name) AS t4 ON t3.cpcode=t4.Bpcode AND t3.Route_Id=t4.Ridd ) AS t5 LEFT JOIN (SELECT Route_Name,Route_ID AS RRid,Plant_Code AS pcod FROM Route_Master WHERE Plant_Code='" + plant_Code + "') AS t6 ON t5.cpcode=t6.pcod AND t5.Route_Id=t6.RRid";
            }   

            

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
