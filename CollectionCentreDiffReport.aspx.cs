using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.IO;
public partial class CollectionCentreDiffReport : System.Web.UI.Page
{
    DbHelper dbaccess = new DbHelper();
    int plant_code, company_code;
    string sess = string.Empty;
  
    DateTime datetimes = new DateTime();
    DataSet ds = new DataSet();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    BLLuser Bllusers = new BLLuser();
    BLLPlantName BllPlant = new BLLPlantName();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    string message;
    string agent_id;
    public static int roleid;
   // string connStr1 = ConfigurationManager.ConnectionStrings["AMPSConnectionStringDpu"].ConnectionString;
     string connStr2 = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            roleid = Convert.ToInt32(Session["Role"].ToString());
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
           // managmobNo = Session["managmobNo"].ToString();
       
            txt_FromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_ToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
         //   agent_id = ddl_AgentId
             Lbl_Errormsg.Visible = false;
             Label1.Visible = false;
             ddl_AgentId.Visible = false;
             if (roleid < 3)
             {
                 LoadSinglePlantName();
                 getdpuagentlist();
             }
             else
             {

                 LoadPlantName();
                 getdpuagentlist();

             }

           //  GetDiffdata();
        }
        else
        {
            pcode = ddl_PlantName.SelectedItem.Value;
        //   getdpuagentlist();
            roleid = Convert.ToInt32(Session["Role"].ToString());
            if (ddl_AgentId.Text != string.Empty)
            {
                agent_id = ddl_AgentId.Text;
            }
            //if (Chk_single.Checked == true )
            //{


            //    agent_id = ddl_AgentId.SelectedItem.Text;

            //}
            GetDiffdata();
            Lbl_Errormsg.Visible = false;
           
        }
            
    }


    public void getdpuagentlist()
    {
        ddl_AgentId.Items.Clear();
        try
        {
            string str = "";
            SqlConnection con = new SqlConnection(connStr2);
            con.Open();
            str = "select distinct(agent_code) as agent_code   from VMCCDPU  where plant_code='" + pcode + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    ddl_AgentId.Items.Add(dr["agent_code"].ToString());

                }

            }
            else
            {
                messagebox();

            }
        }
        catch (Exception ee)
        {

            message = ee.ToString();
            messagebox();

        }
    }

    public void messagebox()
    {


        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload=function(){");
        sb.Append("alert('");
        sb.Append(message);
        sb.Append("')};");
        sb.Append("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
    }

 protected void  btn_GetDiffdata_Click(object sender, EventArgs e)
{

    if (Chk_single.Checked == true || Chk_All.Checked == true)
    {
        GetDiffdata();
    }
    else
    {
        Lbl_Errormsg.Visible = true;
        Lbl_Errormsg.Text = "Please Select Any One Checkox";
    }


     

}

 private void LoadPlantName()
 {
     try
     {

         ds = null;
         ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
         if (ds != null)
         {
             ddl_PlantName.DataSource = ds;
             ddl_PlantName.DataTextField = "Plant_Name";
             ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
             ddl_PlantName.DataBind();

         }
         else
         {

         }

     }
     catch (Exception ex)
     {
     }
 }
 private void LoadSinglePlantName()
 {
     try
     {

         ds = null;
         ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
         if (ds != null)
         {
             ddl_PlantName.DataSource = ds;
             ddl_PlantName.DataTextField = "Plant_Name";
             ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
             ddl_PlantName.DataBind();

         }
         else
         {

         }

     }
     catch (Exception ex)
     {
     }
 }


 private void GetDiffdata()
 {
     try
     {
         string str = string.Empty;
         if (Chk_All.Checked == true)
         {
             cr.Load(Server.MapPath("Crpt_CCdifferenceALL.rpt"));
         }
         else
         {
             cr.Load(Server.MapPath("Crpt_DpuDifference.rpt"));

         }

         cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
         //CrystalDecisions.CrystalReports.Engine.TextObject t1;
         //CrystalDecisions.CrystalReports.Engine.TextObject t2;
         CrystalDecisions.CrystalReports.Engine.TextObject t3;
         CrystalDecisions.CrystalReports.Engine.TextObject t4;
         //CrystalDecisions.CrystalReports.Engine.TextObject t5;

         //t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
         //t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
         t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
         t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
         //t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_phoneno"];                             

         DateTime dt1 = new DateTime();
         DateTime dt2 = new DateTime();

         dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
         dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

         //t1.Text = ccode + "_" + cname;
         //t2.Text = ddl_Plantname.SelectedItem.Value + "_PhoneNo :" + txt_PlantPhoneNo.Text.Trim();
         t3.Text = txt_FromDate.Text.Trim();
         t4.Text = "To : " + txt_ToDate.Text.Trim();

         // t5.Text = managmobNo;

         //string d1 = dt1.ToString("MM/dd/yyyy");
         string d1 = dt1.ToString("MM/dd/yyyy");
         string d11 = dt1.ToString("MM/dd/yyyy hh:mm:ss");
         string d2 = dt2.ToString("MM/dd/yyyy");
         string d22 = dt2.ToString("MM/dd/yyyy hh:mm:ss");


         SqlConnection con = null;
         string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
         con = new SqlConnection(connection);

         //
         //         str = "SELECT f1.Smilkkg AS pMilkkg,f1.Afat AS PAfat,f1.Asnf AS PAsnf,f1.Sfatkg AS Pfatkg,f1.Ssnfkg AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,f2.Smilkkg AS CMilkkg,f2.Afat AS CAfat,f2.Asnf AS CAsnf,f2.Sfatkg AS Cfatkg,f2.Ssnfkg AS Csnfkg,(f1.Smilkkg-f2.Smilkkg) AS Diffmkg,(f1.Afat-f2.Afat) AS Difffat,(f1.Asnf-f2.Asnf) AS Diffsnf,(f1.Sfatkg-f2.Sfatkg) AS Difffatkg,(f1.Ssnfkg-f2.Ssnfkg) AS  Diffsnfkg  FROM  " +
         //" (SELECT SUM(Milk_kg)AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id   FROM procurement Where Plant_Code='156' AND Sessions='AM' AND prdate='"+d1.ToString().Trim()+"' AND Agent_id='1621' GROUP BY agent_id,sessions) AS f1 " +
         //" LEFT JOIN " +
         //" (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code FROM " +
         //" (SELECT CONVERT(float,milk_kg) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,agent_code,shift,prdate FROM THIRUMALABILLSNEW WHERE plant_code='156' AND agent_code='1621' AND shift='AM' AND  prdate='" + d11.ToString().Trim() + "' ) AS t2 Group by t2.agent_code) AS f2 ON f1.Agent_id=f2.agent_code";
         if (Chk_All.Checked == true)
         {
             str = "SELECT * FROM " +
             " (SELECT f1.Smilkkg AS pMilkkg,f1.Afat AS PAfat,f1.Asnf AS PAsnf,f1.Sfatkg AS Pfatkg,f1.Ssnfkg AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,f2.Smilkkg AS CMilkkg,f2.Afat AS CAfat,f2.Asnf AS CAsnf,f2.Sfatkg AS Cfatkg,f2.Ssnfkg AS Csnfkg,(f1.Smilkkg-f2.Smilkkg) AS Diffmkg,(f1.Afat-f2.Afat) AS Difffat,(f1.Asnf-f2.Asnf) AS Diffsnf,(f1.Sfatkg-f2.Sfatkg) AS Difffatkg,(f1.Ssnfkg-f2.Ssnfkg) AS  Diffsnfkg,f1.Agent_id  FROM " +
              " (SELECT SUM(Milk_kg)AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id FROM procurement Where Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'  GROUP BY agent_id) AS f1 " +
              " LEFT JOIN " +
             " (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code FROM " +
             " (SELECT CONVERT(float,milk_kg) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,agent_code FROM VMCCDPU WHERE plant_code='" + pcode + "' AND  prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d22.ToString().Trim() + "') AS t2 Group by t2.agent_code) AS f2 ON f1.Agent_id=f2.agent_code ) AS f3  WHERE f3.CMilkkg IS NOT NULL ORDER BY f3.Agent_id ";

         }
         else
         {
             str = "SELECT f1.Smilkkg AS pMilkkg,f1.Afat AS PAfat,f1.Asnf AS PAsnf,f1.Sfatkg AS Pfatkg,f1.Ssnfkg AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,f2.Smilkkg AS CMilkkg,f2.Afat AS CAfat,f2.Asnf AS CAsnf,f2.Sfatkg AS Cfatkg,f2.Ssnfkg AS Csnfkg,(f1.Smilkkg-f2.Smilkkg) AS Diffmkg,(f1.Afat-f2.Afat) AS Difffat,(f1.Asnf-f2.Asnf) AS Diffsnf,(f1.Sfatkg-f2.Sfatkg) AS Difffatkg,(f1.Ssnfkg-f2.Ssnfkg) AS  Diffsnfkg,f1.Prdate AS pdate,f1.Sessions AS sess,f1.Agent_id  FROM " +
     " (SELECT SUM(Milk_kg)AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id,Sessions,CONVERT(nvarchar(12),prdate,103) AS Prdate FROM procurement Where Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' AND Agent_id='" + agent_id.Trim() + "' GROUP BY agent_id,sessions,Prdate) AS f1 " +
     " LEFT JOIN  " +
    " (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code,shift,prdate FROM " +
    " (SELECT CONVERT(float,milk_kg) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,agent_code,shift,CONVERT(nvarchar(12),prdate,103) AS Prdate FROM VMCCDPU WHERE plant_code='" + pcode + "' AND agent_code='" + agent_id.Trim() + "' AND  prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d22.ToString().Trim() + "') AS t2 Group by t2.agent_code,t2.shift,t2.prdate) AS f2 ON f1.Agent_id=f2.agent_code AND f1.Sessions=f2.shift AND f1.Prdate=f2.prdate";
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
         Lbl_Errormsg.Visible = true;
         Lbl_Errormsg.Text = ex.ToString();
     }

 }

 protected void Chk_All_CheckedChanged(object sender, EventArgs e)
 {
     try
     {

         if (Chk_All.Checked == true)
         {

             Label1.Visible = false;
             ddl_AgentId.Visible = false;
             Chk_All.Checked = true;
             Chk_single.Checked = false;

         }
     }
     catch(Exception ex)
     {
         Lbl_Errormsg.Visible = true;
         Lbl_Errormsg.Text = ex.ToString();
     }

 }
 protected void Chk_single_CheckedChanged(object sender, EventArgs e)
 {
     try
     {
         if (Chk_single.Checked == true)
         {

             getdpuagentlist();
             Label1.Visible = true;
             ddl_AgentId.Visible = true;
             Chk_All.Checked = false;
             Chk_single.Checked = true;

            agent_id = ddl_AgentId.Text;
             if (agent_id == "")
             {
                 Lbl_Errormsg.Visible = true;
                 Lbl_Errormsg.Text = "No Agents In Particular Date";
             }
         }
     }
     catch (Exception ex)
     {
         Lbl_Errormsg.Visible = true;
         Lbl_Errormsg.Text = ex.ToString();
     }

 }
}