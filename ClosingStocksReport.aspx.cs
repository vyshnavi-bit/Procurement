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


public partial class ClosingStocksReport : System.Web.UI.Page
{

    SqlDataReader dr;
    public string rid;
    public string frmdate;
    //public string todate;
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();
    // BLLPlantMaster plantmasterBL = new BLLPlantMaster();
    //int cmpcode, pcode;
    public string companycode;
    public string plantcode;
    public string cname;

    //static int savebtn = 0;
    DataTable dt = new DataTable();
    //DbHelper DBclass = new DbHelper();
    BOLDispatch DispatchBOL = new BOLDispatch();
    BLLDispatch DispatchBLL = new BLLDispatch();
    DALDispatch DispatchDAL = new DALDispatch();
    BLLuser Bllusers = new BLLuser();
    public static int roleid;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            // CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                // Plantname = Session["Plant_Name"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                   LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    plantcode = "170";
                }
                // LoadPlantcode();
                rid = ddl_PlantName.SelectedItem.Value;
                // rid = ddl_RouteID.SelectedItem.Value;
                Period_Closing();
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
                companycode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                //plantcode = Session["Plant_Code"].ToString();

                plantcode = ddl_PlantID.SelectedItem.Value;
                roleid = Convert.ToInt32(Session["Role"].ToString());


                rid = ddl_PlantName.SelectedItem.Value;

                Period_Closing();

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        plantcode = ddl_PlantID.SelectedItem.Value;
        Period_Closing();

    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_PlantID.Items.Clear();
            ddl_PlantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(companycode, plantcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

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
            ddl_PlantID.Items.Clear();
            ddl_PlantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(companycode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void Period_Closing()
    {
        try
        {

            plantcode = ddl_PlantID.SelectedItem.Value;
            cr.Load(Server.MapPath("Report\\ClosingStockCrystal.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            t1.Text = companycode + "_" + cname;
            t2.Text = ddl_PlantName.SelectedItem.Value;
            t3.Text = "From " + txt_FromDate.Text.Trim();
            t4.Text = "To  " + txt_ToDate.Text.Trim();

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            // str = "SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '08-17-2012' AND '01-18-2013' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '08-17-2012' AND '01-18-2013' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ";
            str = "select MilkKg as MILKKG,Fat as FAT,Snf as SNF,FAT_KG,SNF_KG,Amount as AMOUNT,Rate as RATE,convert(varchar(10),Date,101) as date from Stock_Milk where date between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' and Plant_code='" + plantcode + "' order by date asc";
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;
            //System.IO.MemoryStream stream = (System.IO.MemoryStream)cr.ExportToStream(ExportFormatType.PortableDocFormat);
            //BinaryReader Bin = new BinaryReader(cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(Bin.ReadBytes(Convert.ToInt32(Bin.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();
            CrystalReportViewer1.RefreshReport();

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_PlantID.Items.Clear();
            ddl_PlantName.Items.Clear();
            dr = Bllusers.LoadPlantcode(companycode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_PlantID.SelectedIndex = ddl_PlantName.SelectedIndex;
        plantcode = ddl_PlantID.SelectedItem.Value;
    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        if (cr != null)
        {

            cr.Close();

            cr.Dispose();

            GC.Collect();

        }
    }
}