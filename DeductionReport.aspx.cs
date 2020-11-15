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

public partial class DeductionReport : System.Web.UI.Page
{
    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public int rid;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    //Admin Check Flag
    public int Falg = 0;
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
              //  managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                //txt_FromDate.Text = dtm.ToShortDateString();
                //txt_ToDate.Text = dtm.ToShortDateString();

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                pcode = Session["Plant_Code"].ToString();
                pcode = ddl_plantcode.SelectedItem.Value;
                Bdate();
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
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_plantcode.SelectedItem.Value;
                getdeductiondetail();
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

    private void Bdate()
    {
        try
        {
            dr = null;
            dr = BLLBill.Billdate(ccode, ddl_plantcode.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                    Falg = Convert.ToInt32(dr["ViewStatus"].ToString());
                    if (program.Guser_role >= program.Guser_PermissionId)
                    {
                        Falg = 1;
                        Button1.Visible = true;                        

                    }
                    else
                    {
                        if (Falg == 0)
                        {
                            Button1.Visible = true;
                            
                        }
                        else
                        {
                            Button1.Visible = true;
                            
                        }
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantcode.SelectedItem.Value;
        getdeductiondetail();
    }
    private void getdeductiondetail()
    {
        try
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            cr.Load(Server.MapPath("crpt_deduction.rpt"));
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

            str = "select Tid,company_code,Plant_Code,agent_id,route_id,billadvance,Ai,Feed,can,recovery,others,convert(varchar,deductiondate,103)as deductiondate from deduction_Details where company_code='" + ccode + "' and Plant_Code='" + pcode + "' and deductiondate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' order by agent_id,deductiondate";

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
        Bdate();
        getdeductiondetail();
    }
}