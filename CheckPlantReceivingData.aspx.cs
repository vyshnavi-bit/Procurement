using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;


public partial class CheckPlantReceivingData : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;   
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
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
             //   managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3)  && (roleid == 9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                }


              //  LoadPlantcode();
                //pcode = ddl_plantcode.SelectedItem.Value;
                //pname = ddl_plantName.SelectedItem.Value;
               
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
                pcode = ddl_plantcode.SelectedItem.Value;
                cname = Session["cname"].ToString();
                pname = ddl_plantName.SelectedItem.Value;
              //  managmobNo = Session["managmobNo"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
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
                    ddl_plantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_plantName.Items.Add("--Select PlantName--");
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
            ddl_plantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_plantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
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
            ddl_plantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_plantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_plantcode.SelectedIndex = ddl_plantName.SelectedIndex;
        pcode = ddl_plantcode.SelectedItem.Value;
    }

    private void SelectDataProcessing()
    {
        try
        {
            cr.Load(Server.MapPath("Crpt_CheckPlantReceivingData.rpt"));           
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;
            CrystalDecisions.CrystalReports.Engine.TextObject t5;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
            t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Title"]; 

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            t1.Text = ccode + "_" + cname;
            t2.Text = pname;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();
           
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            if (chk_DataProcessing.Checked == true)
            {
                t5.Text = "Data Receiving Report";
                //str = "SELECT DISTINCT(Convert(nvarchar, Prdate,103)) AS Prdate,Sessions FROM Procurementimport WHERE Company_Code='"+ ccode +"' AND  Plant_Code='" + pcode + "' and Prdate between  '" + d1 + "' and '" + d2 + "' ORDER BY Prdate,Sessions";
                str = "SELECT DISTINCT(Convert(nvarchar, Prdate,103)) AS Prdate,Sessions,Sample_No,ISNULL(RNotupdate,0) AS RNotupdate,ISNULL(Rupdate,0) AS Rupdate FROM (SELECT Prdate,Sessions,ISNULL(Sample_No,0) AS Sample_No,ISNULL(RNotupdate,0) AS RNotupdate FROM (SELECT COUNT(Agent_id) AS Sample_No,(Convert(nvarchar, Prdate,103)) AS Prdate,Sessions  FROM Procurementimport WHERE Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "' and Prdate between  '" + d1 + "' and '" + d2 + "' GROUP BY Sessions,Prdate ) AS t1 LEFT JOIN (SELECT DISTINCT(Convert(nvarchar, Prdate,103)) AS Prdate1,Sessions AS sess1,('1') AS RNotupdate  FROM Procurementimport WHERE Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "' and Prdate between  '" + d1 + "' and '" + d2 + "' AND Remarkstatus=1)AS t2 ON t1.Prdate=t2.Prdate1 AND t1.Sessions=t2.sess1) AS t3 LEFT JOIN (SELECT DISTINCT(Convert(nvarchar, Prdate,103)) AS Prdate2,Sessions AS sess2,('2') AS Rupdate FROM Procurementimport WHERE Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "' and Prdate between  '" + d1 + "' and '" + d2 + "' AND Remarkstatus=2)t4 ON t3.Prdate=t4.Prdate2 AND t3.Sessions=t4.sess2 ORDER BY Prdate,Sessions";
            }
            else
            {
                t5.Text = "Data Processing Report";
                str = "SELECT DISTINCT(Convert(nvarchar, Prdate,103)) AS Prdate,Sessions FROM Procurement WHERE Plant_Code='" + pcode + "' and Prdate between   '" + d1 + "' and '" + d2 + "' ORDER BY Prdate,Sessions";                
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


    protected void chk_DataProcessing_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
       
        try
        {
            pcode = ddl_plantcode.SelectedItem.Value;
            SelectDataProcessing();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

            DateTime frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string fdate = frmdate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
            DateTime todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string tdate = todate.ToString("dd" + "_" + "MM" + "_" + "yyyy");

            string CurrentCreateFolderName = fdate + "_" + tdate + "_" + DateTime.Now.ToString("ddMMyyyy");
            string path = @"C:\BILL VYSHNAVI\" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (chk_DataProcessing.Checked == true)
            {
                CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "DataReceivingReport.pdf";
            }
            else
            {
                CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "DataProcessingReport.pdf";
            }
            CrExportOptions = cr.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            cr.Export();
            WebMsgBox.Show("Report Export Successfully...");

            //
            string MFileName = string.Empty;
            if (chk_DataProcessing.Checked == true)
            {
                 MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "DataReceivingReport.pdf";
            }
            else
            {
                 MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "DataProcessingReport.pdf";
            }
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(MFileName.ToString()))
            {
                //
                FileStream sourceFile = new FileStream(file.FullName, FileMode.Open);
                float FileSize;
                FileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)FileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                //
                Response.ClearContent(); // neded to clear previous (if any) written content
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.BinaryWrite(getContent);
                File.Delete(file.FullName.ToString());
                Response.Flush();
                Response.End();

            }
            else
            {

                Response.Write("File Not Found...");
            }
            //
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("Please Check the ExportPath...");
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantcode.SelectedItem.Value;
        SelectDataProcessing();
    }
}