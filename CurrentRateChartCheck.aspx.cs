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


public partial class CurrentRateChartCheck : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    BLLRateChart rateBLL = new BLLRateChart();
    DataTable dt = new DataTable();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess=new DbHelper();
    BLLRateChart bllrate = new BLLRateChart();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();               
                LoadPlantcode();
                pcode = ddl_plantcode.SelectedItem.Value;
                pname = ddl_plantName.SelectedItem.Value;
                LoadRateChart();

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
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = ddl_plantcode.SelectedItem.Value;
                cname = Session["cname"].ToString();
                pname = ddl_plantName.SelectedItem.Value;
              //  managmobNo = Session["managmobNo"].ToString();
               CurentRateChartCheck1();

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

    private void LoadRateChart()
    {

        try
        {
            SqlDataReader dr = null;
             SqlDataReader dr1 = null;
            string Sqlstr = null;
            ddl_ChartName.Items.Clear();
            int bno = 0;
            Sqlstr = "SELECT RateChartPlantRoute_ID FROM RateChartPlantRouteChoose WHERE Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
            bno = (int)dbaccess.ExecuteScalarint(Sqlstr);
            if (bno == 1)
            {
                dr = bllrate.PlantRouteChartIdCow1(ccode, pcode);
                dr1 = bllrate.PlantRouteChartIdBuff2(ccode, pcode);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddl_ChartName.Items.Add(dr["Cratechart"].ToString());         

                    }
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            ddl_ChartName.Items.Add(dr1["Cratechart"].ToString());

                        }
                    }

                }
                else
                {
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            ddl_ChartName.Items.Add(dr1["Cratechart"].ToString());

                        }
                    }
                    else
                    {
                        ddl_ChartName.Items.Add("--No ChartName--");
                    }
                }

            }
            else
            {

                dr = bllrate.RouteChartIdCow1(ccode, pcode);
                dr1 = bllrate.RouteChartIdBuff2(ccode, pcode);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddl_ChartName.Items.Add(dr["Cratechart"].ToString());

                    }
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            ddl_ChartName.Items.Add(dr1["Cratechart"].ToString());

                        }
                    }

                }
                else
                {
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            ddl_ChartName.Items.Add(dr1["Cratechart"].ToString());

                        }
                    }
                    else
                    {
                        ddl_ChartName.Items.Add("--No ChartName--");
                    }
                }


            }

                       
        }

        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }


    protected void btn_ok_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantcode.SelectedItem.Value;
        CurentRateChartCheck1();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_plantcode.SelectedItem.Value;
            CurentRateChartCheck1();

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();       

           
            string path = @"C:\BILL VYSHNAVI\" + ccode + "_" + "_" + pcode +  "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "CurrentRateChart.pdf";

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

            MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + "/" + ccode + "_" + pcode + "_" + "CurrentRateChart.pdf";

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

    private void CurentRateChartCheck1()
    {
        try
        {
            cr.Load(Server.MapPath("Crpt_CurrentRateChartCheck.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
        
            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Chartname"];
           
            t1.Text = ccode + "_" + cname;
            t2.Text = pname;
            t3.Text = ddl_ChartName.SelectedItem.Value;

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            str = "SELECT Table_ID,From_Rangevalue,To_Rangevalue,CAST(Rate AS DECIMAL(18,2)) AS Rate,CAST(Comission_Amount AS DECIMAL(18,2)) AS Comission_Amount,CAST(Bouns_Amount AS DECIMAL(18,2)) AS Bouns_Amount FROM Rate_Chart WHERE Chart_Name='" + ddl_ChartName.SelectedItem.Text + "' AND Plant_code='" + pcode + "' and Company_code='" + ccode + "' ORDER BY Rate";

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
  

    protected void ddl_plantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_plantcode.SelectedIndex = ddl_plantName.SelectedIndex;
        pcode = ddl_plantcode.SelectedItem.Value;
        LoadRateChart();        
        CurentRateChartCheck1();
    }

    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        CurentRateChartCheck1();
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