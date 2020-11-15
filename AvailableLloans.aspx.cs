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

public partial class AvailableLloans : System.Web.UI.Page
{
    
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    SqlDataReader dr;
    BLLuser Bllusers = new BLLuser();

    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;

    public string frmdate;
    public string todate;
    public string rid;
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else 
                {
                    LoadPlantcode();
                }

                pcode = ddl_plantName.SelectedItem.Value;
                pname = ddl_plantName.SelectedItem.Text;

                lbl_frmdate.Visible = false;
                txt_FromDate.Visible = false;
                lbl_todate.Visible = false;
                txt_ToDate.Visible = false;                 

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
               //pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
               // pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_plantName.SelectedItem.Value;
                pname = ddl_plantName.SelectedItem.Text;
                LoanAvailableReport();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

    }
    protected void btn_Loanrecoveryreport_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantName.SelectedItem.Value; 
        LoanAvailableReport();
    }

    private void LoanAvailableReport()
    {
        try
        {

           // CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            

            if (chk_Allloan.Checked == true)
            {
                cr.Load(Server.MapPath("Report//Crpt_AvailableLoan1.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Report//Crpt_AvailableLoan.rpt"));
            }
            

            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;
            

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
           

            //t1.Text = ccode + "_" + cname;
            t1.Text =  cname;
            t2.Text = pname;
           
           

            string str = string.Empty;
            string str1 = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            if (chk_CurrentLoan.Checked == true)
            {
                t3.Text = "From  " + txt_FromDate.Text;
                t4.Text = "To  " + txt_ToDate.Text;
                str = "SELECT loan_Id,route_id,agent_Id,CAST(loanamount AS DECIMAL(18,2))AS loanamount,CAST(inst_amount AS DECIMAL(18,2))AS inst_amount,CAST(balance AS DECIMAL(18,2))AS balance,dscription,status,CONVERT(VARCHAR(100),loandate,106) AS loandate,CONVERT(VARCHAR(100),expiredate,106) AS expiredate FROM LoanDetails WHERE company_code='" + ccode + "' AND  plant_code='" + pcode + "'  AND loandate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  ORDER BY agent_Id,loan_Id";
                            
            }
            else
            {
                t3.Text = " ";
                t4.Text = " ";               
                str = "SELECT loan_Id,route_id,agent_Id,CAST(loanamount AS DECIMAL(18,2))AS loanamount,CAST(inst_amount AS DECIMAL(18,2))AS inst_amount,CAST(balance AS DECIMAL(18,2))AS balance,dscription,status,CONVERT(VARCHAR(100),loandate,106) AS loandate,CONVERT(VARCHAR(100),expiredate,106) AS expiredate FROM LoanDetails WHERE company_code='" + ccode + "' AND  plant_code='" + pcode + "' AND balance>1 ORDER BY agent_Id,loan_Id";
            }           
                      
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);          


            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;

            //if (chk_print.Checked == true)
            //{
            //    cr.PrintToPrinter(1, true, 0, 0);
            //}
           // cr.PrintToPrinter(1, true, 0, 0);
            con.Close();

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

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_plantName.DataSource = ds;
                ddl_plantName.DataTextField = "Plant_Name";
                ddl_plantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_plantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void loadsingleplant()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds != null)
            {
                ddl_plantName.DataSource = ds;
                ddl_plantName.DataTextField = "Plant_Name";
                ddl_plantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_plantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }


    private void LoadPlantcodeold()
    {
        try
        {
            SqlDataReader dr = null;
            //ddl_plantcode.Items.Clear();
            ddl_plantName.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_plantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_plantName.Items.Add("--Select PlantName--");
                //ddl_plantcode.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadsingleplantold()
    {
        try
        {
            SqlDataReader dr = null;
            //ddl_plantcode.Items.Clear();
            ddl_plantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_plantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
      

   
    protected void ddl_plantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_plantcode.SelectedIndex = ddl_plantName.SelectedIndex;
        //pcode = ddl_plantcode.SelectedItem.Value;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_plantName.SelectedItem.Value;
            LoanAvailableReport();

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

            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "AvailableLoan.pdf";

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

            MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "AvailableLoan.pdf";

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
    protected void chk_CurrentLoan_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_CurrentLoan.Checked == true)
        {
            lbl_frmdate.Visible = true;
            txt_FromDate.Visible = true;
            lbl_todate.Visible = true;
            txt_ToDate.Visible = true;
        }
        else
        {
            lbl_frmdate.Visible = false;
            txt_FromDate.Visible = false;
            lbl_todate.Visible = false;
            txt_ToDate.Visible = false;
        }
    }
    protected void chk_Allloan_CheckedChanged(object sender, EventArgs e)
    {
      
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