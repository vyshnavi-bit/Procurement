using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.IO;
using System.Net;


public partial class Sms_Report : System.Web.UI.Page
{
    public string ccode;
    public string pcode;   
    public string pname;
    public string cname;
    public string managmobNo;
    DataSet ds = new DataSet();
    BLLuser Bllusers = new BLLuser();
   //AgentBO.AgDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
    DateTime dtm = new DateTime();
    DbHelper dbaccess = new DbHelper();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());

                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if (roleid >= 3)
                {
                    LoadPlantcode();
                }

               
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"]= "170".ToString();
                    pcode = "170";
                }
               

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

        if (IsPostBack == true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();
                Select_Procurementreport1();
               
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
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
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
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
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
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    protected void btn_SmsReport_Click(object sender, EventArgs e)
    {
        Select_Procurementreport1();
    }

    private void Select_Procurementreport1()
    {
        try
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            cr.Load(Server.MapPath("SmsReport.rpt"));
            cr.SetDatabaseLogon("233.196.32.27", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;
            CrystalDecisions.CrystalReports.Engine.TextObject t5;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
            t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Reporttitle"];

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            t1.Text = ccode + "_" + cname;
            //t2.Text = pcode + "_" + pname;
            t2.Text =  ddl_Plantname.Text;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();
            t5.Text = "Sms Report From :";
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string str = string.Empty;
            SqlConnection con = null;
            str = "select CONVERT(VARCHAR,Send_date,103) AS Send_date,CONVERT(VARCHAR,Send_datetime,113) AS Send_datetime,Agent_mobileNo,Message from Message_Histroy where Company_code='" + ccode + "' and Plant_code='" + ddl_Plantcode.Text + "' and Send_date between '" + d1 + "' and '" + d2 + "' ORDER BY Send_datetime";
            using (con = dbaccess.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cr.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = cr;
            }

        }

        catch (Exception ex)
        {
            //WebMsgBox.Show(ex.ToString());
        }

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
    }
   
    
   
}