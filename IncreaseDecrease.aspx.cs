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

public partial class IncreaseDecrease : System.Web.UI.Page
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

                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                LoadPlantcode();
                pcode = ddl_plantcode.SelectedItem.Value;
                pname = ddl_plantName.SelectedItem.Value;

                if (chk_IceDecAbstract.Checked == true)
                {
                    lbl_range.Visible = false;
                    ddl_Type.Visible = false;
                }
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
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_plantcode.SelectedIndex = ddl_plantName.SelectedIndex;
        pcode = ddl_plantcode.SelectedItem.Value;
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantcode.SelectedItem.Value;
        IncreaseDecreaseReport1();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_plantcode.SelectedItem.Value;
            IncreaseDecreaseReport1();

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

            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "IncreaseDecreaseReport.pdf";

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

            MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "IncreaseDecreaseReport.pdf";

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

    private void IncreaseDecreaseReport1()
    {
        try
        {
            if (chk_IceDecAbstract.Checked == true)
            {
                cr.Load(Server.MapPath("Crpt_IncreaseDecrease.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Crpt_IncreaseDecrease.rpt"));
            }
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;


            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];


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
            if (chk_IceDecAbstract.Checked == true)
            {
                str = "SELECT f2.Plant_Code,CONVERT(Nvarchar(35), f2.pdate,103) AS pdate,f2.Sessions,f2.IFat,f2.ISnf,IMkg,f2.Dfat,f2.DSnf,f3.DMkg FROM (SELECT f.Plant_Code,f.pdate,f.Sessions,f.IFat,f.Dfat,f1.ISnf,f1.DSnf FROM (SELECT pro.Plant_Code,pro.pdate,pro.Sessions,ISNULL(t.DFat,0) AS Dfat,ISNULL(t.IFat,0) AS IFat  FROM (SELECT t3.Plant_Code,t3.Prdate,t3.Sessions,ISNULL(t3.IFat,0) AS IFat,ISNULL(t4.DFat,0) AS DFat FROM (SELECT Plant_Code,Prdate,Sessions,SUM(CAST(DIFFFAT AS DECIMAL(18,1))) AS IFat FROM Procurementimport WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Remarkstatus=2  AND (DIFFFAT>=0.01 )  Group by Prdate,Sessions,Plant_Code ) AS t3 LEFT JOIN (SELECT Plant_Code,Prdate,Sessions,SUM(CAST(DIFFFAT AS DECIMAL(18,1))) AS DFat FROM Procurementimport WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Remarkstatus=2  AND (DIFFFAT<=-0.01 )  Group by Prdate,Sessions,Plant_Code )AS t4 ON t3.Plant_Code=t4.Plant_Code  AND t3.Prdate=t4.Prdate AND t3.Sessions=t4.Sessions) AS t RIGHT JOIN (SELECT Distinct(Prdate) AS pdate,Sessions,Plant_Code FROM Procurement WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "') AS pro ON t.Plant_Code=pro.Plant_Code AND t.Sessions=pro.Sessions AND t.Prdate=pro.pdate) AS f INNER JOIN (SELECT pro1.Plant_Code,pro1.pdate,pro1.Sessions,ISNULL(t5.ISnf,0) AS ISnf,ISNULL(t5.DSnf,0) AS DSnf FROM (SELECT t1.Plant_Code,t1.Prdate,t1.Sessions,ISNULL(t1.ISnf,0) AS ISnf,ISNULL(t2.DSnf,0) AS DSnf FROM  (SELECT Plant_Code,Prdate,Sessions,SUM(CAST(DIFFSNF AS DECIMAL(18,1))) AS ISnf FROM Procurementimport WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Remarkstatus=2  AND (DIFFSNF>=0.01 )  Group by Prdate,Sessions,Plant_Code ) AS t1 LEFT JOIN (SELECT Plant_Code,Prdate,Sessions,SUM(CAST(DIFFSNF AS DECIMAL(18,1))) AS DSnf FROM Procurementimport WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Remarkstatus=2  AND (DIFFSNF<=-0.01 )  Group by Prdate,Sessions,Plant_Code )AS t2 ON t1.Plant_Code=t2.Plant_Code AND t1.Prdate=t2.Prdate AND t1.Sessions=t2.Sessions) AS t5 RIGHT JOIN (SELECT Distinct(Prdate) AS pdate,Sessions,Plant_Code FROM Procurement WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "') AS pro1 ON t5.Plant_Code=pro1.Plant_Code AND t5.Sessions=pro1.Sessions AND t5.Prdate=pro1.pdate) AS f1 ON f.Plant_Code=f1.Plant_Code AND f.Sessions=f1.Sessions  AND f.pdate=f1.pdate ) AS f2  INNER JOIN (SELECT pro2.Plant_Code,pro2.pdate,pro2.Sessions,ISNULL(tt5.IMkg,0) AS IMkg,ISNULL(tt5.DMkg,0) AS DMkg FROM (SELECT tt1.Plant_Code,tt1.Prdate,tt1.Sessions,ISNULL(tt1.IMkg,0) AS IMkg,ISNULL(tt2.DMkg,0) AS DMkg FROM  (SELECT Plant_Code,Prdate,Sessions,SUM(CAST(DIFFKG AS DECIMAL(18,1))) AS IMkg FROM Procurementimport WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Remarkstatus=2  AND (DIFFKG>=0.01 )  Group by Prdate,Sessions,Plant_Code ) AS tt1 LEFT JOIN (SELECT Plant_Code,Prdate,Sessions,SUM( CAST(DIFFKG AS DECIMAL(18,1))) AS DMkg FROM Procurementimport WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Remarkstatus=2  AND (DIFFKG<=-0.01 )  Group by Prdate,Sessions,Plant_Code )AS tt2 ON tt1.Plant_Code=tt2.Plant_Code AND tt1.Prdate=tt2.Prdate AND tt1.Sessions=tt2.Sessions) AS tt5 RIGHT JOIN (SELECT Distinct(Prdate) AS pdate,Sessions,Plant_Code FROM Procurement WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "') AS pro2 ON tt5.Plant_Code=pro2.Plant_Code AND tt5.Sessions=pro2.Sessions AND tt5.Prdate=pro2.pdate) AS f3 ON f2.Plant_Code=f3.Plant_Code AND f2.pdate=f3.pdate AND f2.Sessions=f3.Sessions  ORDER BY f3.pdate,f3.Sessions ";
            }
            else
            {
                if (ddl_Type.SelectedItem.Value == "MilkKg")
                {
                    str = "";
                }
                else if (ddl_Type.SelectedItem.Value == "Fat")
                {
                    str = "";
                }
                else
                {
                    str = "";
                }

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

    protected void chk_IceDecAbstract_CheckedChanged(object sender, EventArgs e)
    {
        if(chk_IceDecAbstract.Checked==true)
        {
            lbl_range.Visible = false;
            ddl_Type.Visible = false;
        }
        else
        {
            lbl_range.Visible = true;
            ddl_Type.Visible = true;
        }
    }
    protected void ddl_Type_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}