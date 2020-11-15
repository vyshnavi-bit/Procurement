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

public partial class AgentsRemarks : System.Web.UI.Page
{
    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public int rid;
    public string managmobNo;
    public string pname;
    public string cname;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    //Admin Check Flag
    public int Falg = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();


  
  
   
   
  
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();


    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;

    public int refNo = 0;
    string agentcode;

    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
            //    managmobNo = Session["managmobNo"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                //txt_FromDate.Text = dtm.ToShortDateString();
                //txt_ToDate.Text = dtm.ToShortDateString();

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid < 3))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();

                }

                if (CheckBox1.Checked == true)
                {

                    Label7.Visible = true;
                    ddl_Agentname.Visible = true;

                }

                if (CheckBox1.Checked == false)
                {

                    Label7.Visible = false;
                    ddl_Agentname.Visible = false;

                }



                pcode = Session["Plant_Code"].ToString();
                pcode = ddl_plantcode.SelectedItem.Value;
               
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
                ccode = Session["Company_code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
             //   pcode = Session["Plant_Code"].ToString();
                pcode = ddl_plantcode.SelectedItem.Value;
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
            //    managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_plantcode.SelectedItem.Value;
               // agentcode = ddl_Agentid.SelectedItem.Value;
                agentcode = ddl_Agentname.SelectedItem.Value;
                getdeductiondetail();



                if (CheckBox1.Checked == true)
                {

                    Label7.Visible = true;
                    ddl_Agentname.Visible = true;

                }

                if (CheckBox1.Checked == false)
                {

                    Label7.Visible = false;
                    ddl_Agentname.Visible = false;

                }
               
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
                ddl_plantcode.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantcode.SelectedItem.Value;
        getdeductiondetail();
        MemoryStream oStream; // using System.IO
        oStream = (MemoryStream)
        cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(oStream.ToArray());
        Response.End();
    }
    private void getdeductiondetail()
    {
        try
        {

            //            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            cr.Load(Server.MapPath("Reamark.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            t1.Text = ccode + "_" + cname;
            t2.Text = ddl_Plantname.SelectedItem.Value;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");



            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

        //    str = "select Tid,company_code,Plant_Code,agent_id,route_id,billadvance,Ai,Feed,can,recovery,others,convert(varchar,deductiondate,103)as deductiondate from deduction_Details where company_code='" + ccode + "' and Plant_Code='" + pcode + "' and deductiondate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' order by agent_id,deductiondate";


            if (CheckBox1.Checked == true)
            {

                str = "select  Agent_id,convert(varchar,prdate,103) as Date,Sessions,SUM(Milk_kg-DIFFKG)as Milk_Kg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modify_snf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "' and agent_id='" + agentcode + "'    and Prdate between '" + d1 + "' and '" + d2 + "' and (DIFFFAT > 0 or DIFFSNF > 0 or DIFFFAT < 0 or DIFFSNF < 0 ) and  (Remarkstatus=1  or Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by Agent_id,Prdate,Sessions";


            }
            if (CheckBox1.Checked == false)
            {

                str = "select  Agent_id,convert(varchar,prdate,103) as Date,Sessions,SUM(Milk_kg-DIFFKG)as Milk_Kg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modify_snf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "'    and Prdate between '" + d1 + "' and '" + d2 + "' and (DIFFFAT > 0 or DIFFSNF > 0 or DIFFFAT < 0 or DIFFSNF < 0 ) and  (Remarkstatus=1  or Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by Agent_id,Prdate,Sessions";


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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_plantcode.SelectedItem.Value;
        loadagentid();
        //getagentid();
       // getdeductiondetail();
    }


    //public void getagentid()
    //{

    //    try
    //    {
    //        ddl_Agentid.Items.Clear();
    //        //     txt_AgentName.Text = "";
    //        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        SqlConnection con = new SqlConnection(connStr);
    //        con.Open();
    //        //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
    //        string str = "select distinct agent_id,Agent_Name from agent_master where plant_code='" + pcode + "'     order by Agent_id  asc ";
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {

    //             //   ddl_Agentid.Items.Add(dr["agent_id"].ToString());
    //                ddl_Agentid.Items.Add(dr["agent_id"].ToString());
    //                ddl_Agentname.Items.Add(dr["agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
    //                // txt_AgentName.Text = dr["Agent_Name"].ToString();


    //            }
    //        }
    //        else
    //        {

    //            //lbl_label.Visible = true;
    //            //lbl_label.ForeColor = System.Drawing.Color.Red;
    //            //lbl_label.Text = "No Agents";


    //        }



    //    }

    //    catch
    //    {

    //        //lbl_label.Visible = true;
    //        //lbl_label.ForeColor = System.Drawing.Color.Red;
    //        //lbl_label.Text = "Please Check";

    //    }



    //}


    private void loadagentid()
    {
        try
        {

            ds = null;
            ds = GetagentId(ccode.ToString(),pcode);
            if (ds != null)
            {
                ddl_Agentname.DataSource = ds;
                ddl_Agentname.DataTextField = "agent_name";
                ddl_Agentname.DataValueField = "agent_id";//ROUTE_ID 
                ddl_Agentname.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }


    public DataSet GetagentId(string ccode, string pcode)
    {
        ds = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "Select convert(nvarchar(50),Agent_id)+'_'+ Agent_Name as Agent_Name,Agent_id      FROM  Agent_Master  WHERE Company_Code='" + ccode + "' AND plant_Code='" + pcode + "'   order by agent_id asc ";
        ds = DBaccess.GetDataset(sqlstr);
        return ds;
    }



    protected void txt_ToDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Agentname_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantcode.SelectedItem.Value;
        agentcode = ddl_Agentname.SelectedItem.Value;
        getdeductiondetail();
      
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        if (CheckBox1.Checked == true)
        {

            Label7.Visible = true;
            ddl_Agentname.Visible = true;

        }

        if (CheckBox1.Checked == false)
        {

            Label7.Visible = false;
            ddl_Agentname.Visible = false;

        }
    }
}