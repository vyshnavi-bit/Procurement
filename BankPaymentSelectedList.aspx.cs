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
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Xml;


public partial class BankPaymentSelectedList : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string filename;
    //
    string currentId = string.Empty;
    decimal subTotal = 0;
    decimal total = 0;
    int subTotalRowIndex = 0;
    //
    SqlDataReader dr;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess = new DbHelper();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    SqlConnection con = new SqlConnection();

    string planttype;
    string planttypehdfc;
    string d1, d2;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    public static int roleid;
    string getid;
    int GETID;
    string Agent_Id;
    DataTable dtaddamount = new DataTable();
    string paymode;
    string ptype;
    string statement;
    string accountnu;
    string companyname;
    DataTable showreport = new DataTable();

    double milkpayamt;
    double excesspayamt;


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
                //	managmobNo = Session["managmobNo"].ToString();

                // dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                LoadPlantcode();
                Bdate();

                if (chk_All.Checked == true)
                {
                    lbl_PlantName.Visible = false;
                    ddl_Plantname.Visible = false;
                    lbl_addeddate.Visible = false;
                    ddl_Addeddate.Visible = false;
                }
                this.BindGrid();
                GridView8.Visible = false;
                Button2.Visible = false;

                btn_kotack.Visible=false;
                btn_kotackexport.Visible = false;
                GridView9.Visible = false;
                lbl_ktk.Visible = false;
                ddl_kotack.Visible = false;
                lbl_totamt.Visible = false;
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
                //	managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_Plantcode.SelectedItem.Value;
                //  GetBankPaymentSelectedList();
                this.BindGrid();
                lbl_totamt.Visible = false;
                GridView9.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }

    }

    private void Bdate()
    {
        try
        {
            dr = null;
            dr = Billdate(ccode, ddl_Plantcode.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");

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
    public SqlDataReader Billdate(string ccode, string pcode)
    {
        SqlDataReader dr;
        string sqlstr = string.Empty;
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND CurrentPaymentFlag='1' ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }

    protected void chk_All_CheckedChanged(object sender, EventArgs e)
    {
        Adddate();
        if (chk_All.Checked == true)
        {
            lbl_PlantName.Visible = false;
            ddl_Plantname.Visible = false;
            lbl_addeddate.Visible = false;
            ddl_Addeddate.Visible = false;
        }
        else
        {
            lbl_PlantName.Visible = true;
            ddl_Plantname.Visible = true;
            lbl_addeddate.Visible = true;
            ddl_Addeddate.Visible = true;
        }
    }

    private void Adddate()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            SqlDataReader dr = null;
            ddl_Addeddate.Items.Clear();
            dr = Adddate(ccode, pcode, d1, d2);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Addeddate.Items.Add(dr["BankFileName"].ToString());
                }
            }
            else
            {
                ddl_Addeddate.Items.Add("--select date--");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    public SqlDataReader Adddate(string ccode, string pcode, string d1, string d2)
    {
        SqlDataReader dr = null;
        string sqlstr = "Select DISTINCT(ISNULL(BankFileName,'')) As adddate,BankFileName from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' ORDER BY adddate";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;

    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        Bdate();
        Adddate();
        LoadUploadedFilesDetails();
        Button2.Visible = false;
        GridView8.Visible = false;
        lbl_totamt.Visible = false;
        GridView9.Visible = false;
    }
    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void AllData()
    {
        //try
        //{
        //    string strDownloadFileName = "";
        //    string strExcelConn = "";
        //    DateTime curdate = System.DateTime.Now;
        //    string dd = curdate.ToString("dd");
        //    string mm = curdate.ToString("MM");
        //    string yy = curdate.ToString("yyyy");
        //    string fname = "SVD" + dd + mm + yy;
        //    strDownloadFileName = @"C:/BILL VYSHNAVI/" + fname + ".xls";
        //    string MFileName = @"C:/BILL VYSHNAVI/" + fname + ".xls";
        //    string path = @"C:/BILL VYSHNAVI/";
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
        //    if (File.Exists(strDownloadFileName.ToString()))
        //    {
        //        //File.Delete(file.FullName.ToString());
        //    }
        //    if (File.Exists(strDownloadFileName.ToString()))
        //    {
        //        // File.Delete(file.FullName.ToString());
        //        strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
        //    }
        //    else
        //    {
        //        strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
        //    }
        //    // Retrieve data from SQL Server table.
        //    DataTable dtSQL = RetrieveDataAll();
        //    // Export data to an Excel spreadsheet.
        //    ExportToExcelAll(strExcelConn, dtSQL);

        //    if (File.Exists(MFileName.ToString()))
        //    {
        //        //
        //        FileStream sourceFile = new FileStream(file.FullName, FileMode.Open);
        //        float FileSize;
        //        FileSize = sourceFile.Length;
        //        byte[] getContent = new byte[(int)FileSize];
        //        sourceFile.Read(getContent, 0, (int)sourceFile.Length);
        //        sourceFile.Close();
        //        //
        //        Response.ClearContent(); // neded to clear previous (if any) written content
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
        //        Response.AddHeader("Content-Length", file.Length.ToString());
        //        Response.ContentType = "text/plain";
        //        Response.BinaryWrite(getContent);
        //        File.Delete(file.FullName.ToString());
        //        Response.Flush();
        //        Response.End();

        //    }

        //}
        //catch (Exception ex)
        //{
        //}
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        ExportExcel();
        //AllData();


    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_Plantcode.SelectedItem.Value;
            // GetBankPaymentSelectedList();
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
            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "BankPaymentlist.pdf";
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
            string MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "BankPaymentlist.pdf";
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

    //private void GetBankPaymentSelectedList()
    //{
    //    try
    //    {


    //        cr.Load(Server.MapPath("Crpt_BankPaymentSelectedList.rpt"));
    //        cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
    //        CrystalDecisions.CrystalReports.Engine.TextObject t1;
    //        CrystalDecisions.CrystalReports.Engine.TextObject t2;
    //        CrystalDecisions.CrystalReports.Engine.TextObject t3;
    //        CrystalDecisions.CrystalReports.Engine.TextObject t4;

    //        t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
    //        t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
    //        t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
    //        t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];



    //        DateTime dt1 = new DateTime();
    //        DateTime dt2 = new DateTime();
    //        DateTime dt3 = new DateTime();

    //        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
    //        dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

    //        string d1 = dt1.ToString("MM/dd/yyyy");
    //        string d2 = dt2.ToString("MM/dd/yyyy");
    //        string d3 = dt3.ToString("MM/dd/yyyy");

    //        t1.Text = ccode + "_" + cname;
    //        t2.Text = pcode + "_" + pname;
    //        t3.Text = txt_FromDate.Text.Trim();
    //        t4.Text = "To : " + txt_ToDate.Text.Trim();

    //        string str = string.Empty;
    //        SqlConnection con = null;
    //        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        con = new SqlConnection(connection);

    //        if (chk_All.Checked == true)
    //        {
    //            str = "Select Account_no,Ifsccode,NetAmount,PStatus,agent_id,AgentName AS Agent_Name ,Bank_Id,Pm.plant_code,BankName,Pm.Pmail AS Pmail from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "') AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ) AS Am ON bp.agent_id=Am.Aid ) AS t1 LEFT JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.Ifsccode ";
    //        }
    //        else
    //        {
    //            if (pcode == "160")
    //            {
    //                str = "Select * from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' ORDER BY ifsccode ";
    //            }
    //            else
    //            {
    //                str = "Select Account_no,Ifsccode,NetAmount,PStatus,agent_id,AgentName AS Agent_Name ,Bank_Id,Pm.plant_code,BankName,Pm.Pmail AS Pmail from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND Added_date='" + d3 + "' ) AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ) AS Am ON bp.agent_id=Am.Aid ) AS t1 LEFT JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.Ifsccode ";
    //            }
    //        }
    //        SqlCommand cmd = new SqlCommand();
    //        SqlDataAdapter da = new SqlDataAdapter(str, con);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);

    //        cr.SetDataSource(dt);
    //        CrystalReportViewer1.ReportSource = cr;          
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }


    //}


    protected void btn_Exportcsv_Click(object sender, EventArgs e)
    {
        ExportGridToText();
    }
    private void ExportGridToText()
    {
        pcode = ddl_Plantcode.SelectedItem.Value;
        LoadSBIWhileListGrid();
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=bms.txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        StringBuilder Rowbind = new StringBuilder();

        for (int k = 0; k < GridView1.Columns.Count; k++)
        {
            // Rowbind.Append(GridView1.Columns[k].HeaderText + ' ');
        }
        // Rowbind.Append("\r\n");


        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            int s = GridView1.Columns.Count;
            int j = 1;
            for (int k = 0; k < GridView1.Columns.Count; k++)
            {
                if (j == s)
                {
                    Rowbind.Append(GridView1.Rows[i].Cells[k].Text);
                }
                else
                {
                    Rowbind.Append(GridView1.Rows[i].Cells[k].Text + '#');
                }
                j++;

            }

            Rowbind.Append("\r\n");
        }
        Response.Output.Write(Rowbind.ToString());
        Response.Flush();
        Response.End();

    }
    public void LoadSBIWhileListGrid()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            //  DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            //dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            // string d3 = dt3.ToString("MM/dd/yyyy");
            string d3 = ddl_Addeddate.SelectedItem.Value.Trim().ToString();

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                //string sqlstr = "Select Agent_Name,Account_no,(Company_Code-Company_Code) AS Standard from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND Added_date='" + d3 + "'";
                string sqlstr = "Select UPPER(REPLACE(AgentName,'.',' ')) AS Agent_Name ,Account_no,(Pm.plant_code-Pm.plant_code) AS Standard,Ifsccode,NetAmount,PStatus,agent_id,Bank_Id,BankName,Pm.Pmail AS Pmail from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND BankFileName='" + d3 + "' ) AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ) AS Am ON bp.agent_id=Am.Aid ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "' AND Bank_id='33') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.agent_id";
                string sqlstr1 = "Select Account_no,NetAmount,CONVERT( NVARCHAR(40),Adate,103) AS Adate,NetAmount,agent_id,REPLACE(AgentName,AgentName,'Bms milk pay') AS Standards from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code,Adate from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code,Added_Date AS Adate FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code,Added_Date from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND Added_date='" + d3 + "' ) AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ) AS Am ON bp.agent_id=Am.Aid ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "' AND Bank_id='33') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.agent_id";

                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        }
        catch
        {

        }
    }

    private void ExportGridToTextSBIPaymentAllotmentList()
    {
        pcode = ddl_Plantcode.SelectedItem.Value;
        LoadSBIPaymentAllotmentListGrid();
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=bms1.txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        GridView2.AllowPaging = false;
        GridView2.DataBind();
        StringBuilder Rowbind = new StringBuilder();

        for (int k = 0; k < GridView2.Columns.Count; k++)
        {
            // Rowbind.Append(GridView1.Columns[k].HeaderText + ' ');
        }
        // Rowbind.Append("\r\n");


        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            int s = GridView2.Columns.Count;
            int j = 1;
            int B = 3;
            for (int k = 0; k < GridView2.Columns.Count; k++)
            {
                if (j == s)
                {
                    Rowbind.Append(GridView2.Rows[i].Cells[k].Text);
                }
                else if (j == B)
                {
                    Rowbind.Append(GridView2.Rows[i].Cells[k].Text + "##");
                }
                else
                {
                    Rowbind.Append(GridView2.Rows[i].Cells[k].Text + '#');
                }
                j++;

            }

            Rowbind.Append("\r\n");
        }
        Response.Output.Write(Rowbind.ToString());
        Response.Flush();
        Response.End();

    }

    public void LoadSBIPaymentAllotmentListGrid()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            // DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            // dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            // string d3 = dt3.ToString("MM/dd/yyyy");
            string d3 = ddl_Addeddate.SelectedItem.Value.Trim().ToString();

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sqlstr = "Select Account_no,CAST(NetAmount AS DECIMAL(18,0)) AS NetAmount,CONVERT( NVARCHAR(40),Adate,103) AS Adate,CAST(NetAmount AS DECIMAL(18,0)) AS NetAmount,agent_id,REPLACE(AgentName,AgentName,'Bms milk pay') AS Standards from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code,Adate from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code,Added_Date AS Adate FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code,Added_Date from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND BankFileName='" + d3 + "' ) AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "' ) AS Am ON bp.agent_id=Am.Aid ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "' AND Bank_id='33') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.agent_id";

                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }
        }
        catch
        {

        }
    }

    public void LoadINGListGrid()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string d3 = dt3.ToString("MM/dd/yyyy");

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sqlstr = "Select UPPER(REPLACE(AgentName,'.',' ')) AS BeneficiaryName,BankName AS BeneficiaryBankName,Account_no AS AccountNo,BankName AS BeneficiaryAccountType,Ifsccode AS IFSCCode,NetAmount AS Amount,BankName AS SendertoReceiverInfo ,agent_id AS OwnReferenceNumber,BankName AS Remarks,PStatus,Bank_Id,Pm.Pmail AS Pmail,pnumber from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code,Pnumber from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code,phone_number AS Pnumber FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND Added_date='" + d3 + "' ) AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id,phone_number FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "') AS Am ON bp.agent_id=Am.Aid ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.agent_id ";

                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                }
                else
                {
                    GridView3.DataSource = null;
                    GridView3.DataBind();
                }
            }
        }
        catch
        {

        }
    }


    protected void btn_SbipaymentListcsv0_Click(object sender, EventArgs e)
    {
        ExportGridToTextSBIPaymentAllotmentList();
    }
    protected void btn_ExportIng_Click(object sender, EventArgs e)
    {
        Ing();
    }
    protected DataTable RetrieveData1()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);


            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string d3 = ddl_Addeddate.SelectedItem.Value.ToString();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
            {
                da = new SqlDataAdapter("Select UPPER(REPLACE(AgentName,'.',' ')) AS BeneficiaryName,BankName AS BeneficiaryBankName,Account_no AS AccountNo,BankName AS BeneficiaryAccountType,Ifsccode AS IFSCCode,NetAmount AS Amount,BankName AS SendertoReceiverInfo ,agent_id AS OwnReferenceNumber,BankName AS Remarks,PStatus,Bank_Id,Pm.Pmail AS Pmail,pnumber from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code,Pnumber from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code,phone_number AS Pnumber FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND BankFileName='" + d3 + "' ) AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id,phone_number FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "') AS Am ON bp.agent_id=Am.Aid ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.agent_id ", conn);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    protected void ExportToExcel(string strConn, System.Data.DataTable dtSQL)
    {
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                // Create a new sheet in the Excel spreadsheet.
                OleDbCommand cmd = new OleDbCommand("create table INGBULK(BeneficiaryName  Varchar(28),BeneficiaryBankName Varchar(80),AccountNo Varchar(25),BeneficiaryAccountType Varchar(10),IFSCCode Varchar(15),Amount Double,SendertoReceiverInfo Varchar(60),OwnReferenceNumber Varchar(20),Remarks Varchar(80))", conn);

                // Open the connection.
                conn.Open();

                // Execute the OleDbCommand.
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO INGBULK (BeneficiaryName,BeneficiaryBankName,AccountNo,BeneficiaryAccountType,IFSCCode,Amount,SendertoReceiverInfo,OwnReferenceNumber,Remarks) values (?,?,?,?,?,?,?,?,?)";

                // Add the parameters.
                // cmd.Parameters.Add("Tid", OleDbType.Integer);              
                cmd.Parameters.Add("BeneficiaryName", OleDbType.VarChar, 28, "BeneficiaryName");
                cmd.Parameters.Add("BeneficiaryBankName", OleDbType.VarChar, 80, "BeneficiaryBankName");
                cmd.Parameters.Add("AccountNo", OleDbType.VarChar, 25, "AccountNo");
                cmd.Parameters.Add("BeneficiaryAccountType", OleDbType.VarChar, 10, "BeneficiaryAccountType");
                cmd.Parameters.Add("IFSCCode", OleDbType.VarChar, 15, "IFSCCode");
                cmd.Parameters.Add("Amount", OleDbType.Double, 8, "Amount");
                cmd.Parameters.Add("SendertoReceiverInfo", OleDbType.VarChar, 60, "SendertoReceiverInfo");
                cmd.Parameters.Add("OwnReferenceNumber", OleDbType.VarChar, 20, "OwnReferenceNumber");
                cmd.Parameters.Add("Remarks", OleDbType.VarChar, 20, "Remarks");

                // Initialize an OleDBDataAdapter object.
                OleDbDataAdapter da = new OleDbDataAdapter("select * from INGBULK", conn);

                // Set the InsertCommand of OleDbDataAdapter, 
                // which is used to insert data.
                da.InsertCommand = cmd;
                // Changes the Rowstate()of each DataRow to Added,
                // so that OleDbDataAdapter will insert the rows.
                foreach (DataRow dr in dtSQL.Rows)
                {
                    dr.SetAdded();
                }
                // Insert the data into the Excel spreadsheet.
                da.Update(dtSQL);

            }

        }
        catch (Exception ex)
        {
        }
    }

    protected DataTable RetrieveData2()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            // DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            // dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            //string d3 = dt3.ToString("MM/dd/yyyy");
            string d3 = ddl_Addeddate.SelectedItem.Value.ToString();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
            {
                da = new SqlDataAdapter("Select Account_no AS AccountNo,REPLACE(AgentName,'.',' ') AS Name,NetAmount AS Amount,BankName AS Narration,BankName AS BeneficiaryBankName,BankName AS BeneficiaryAccountType,Ifsccode AS IFSCCode,BankName AS SendertoReceiverInfo ,agent_id AS OwnReferenceNumber,BankName AS Remarks,PStatus,Bank_Id,Pm.Pmail AS Pmail,pnumber from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code,Pnumber from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code,phone_number AS Pnumber FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' AND bankFileName='" + d3 + "' ) AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id,phone_number FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "') AS Am ON bp.agent_id=Am.Aid ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.agent_id ", conn);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    protected void ExportToExcel2(string strConn, System.Data.DataTable dtSQL)
    {
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                // Create a new sheet in the Excel spreadsheet.
                OleDbCommand cmd = new OleDbCommand("create table INGBULKSVD(AccountNo Varchar(25),Name  Varchar(28),Amount Double,Narration Varchar(100))", conn);
                // Open the connection.
                conn.Open();

                // Execute the OleDbCommand.
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO INGBULKSVD (AccountNo,Name,Amount,Narration) values (?,?,?,?)";

                // Add the parameters.
                // cmd.Parameters.Add("Tid", OleDbType.Integer);  
                cmd.Parameters.Add("AccountNo", OleDbType.VarChar, 25, "AccountNo");
                cmd.Parameters.Add("Name", OleDbType.VarChar, 28, "Name");
                cmd.Parameters.Add("Amount", OleDbType.Double, 8, "Amount");
                cmd.Parameters.Add("Narration", OleDbType.VarChar, 28, "Narration");

                // Initialize an OleDBDataAdapter object.
                OleDbDataAdapter da = new OleDbDataAdapter("select * from INGBULKSVD", conn);

                // Set the InsertCommand of OleDbDataAdapter, 
                // which is used to insert data.
                da.InsertCommand = cmd;
                // Changes the Rowstate()of each DataRow to Added,
                // so that OleDbDataAdapter will insert the rows.
                foreach (DataRow dr in dtSQL.Rows)
                {
                    dr.SetAdded();
                }
                // Insert the data into the Excel spreadsheet.
                da.Update(dtSQL);

            }

        }
        catch (Exception ex)
        {
        }
    }

    protected DataTable RetrieveDataAll()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string d3 = dt3.ToString("MM/dd/yyyy");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
            {
                da = new SqlDataAdapter("Select agent_id,REPLACE(AgentName,'.',' ') AS Name,NetAmount AS Amount,Account_no AS AccountNo,BankName AS Narration,BankName AS BeneficiaryBankName,BankName AS BeneficiaryAccountType,Ifsccode AS IFSCCode,BankName AS SendertoReceiverInfo ,BankName AS Remarks,PStatus,Bank_Id,Pm.Pmail AS Pmail,pnumber from (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName,Bank_Id,Bd.Bankname AS BankName,plant_code,Pnumber from (SELECT agent_id,Account_no,Ifsccode,NetAmount,PStatus,AgentName AS AgentName,Bank_Id,plant_code,phone_number AS Pnumber FROM (Select agent_id,Account_no,Ifsccode,NetAmount,PStatus,plant_code from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "') AS Bp LEFT JOIN  (SELECT Agent_Id AS Aid,Agent_Name AS AgentName,Bank_Id,phone_number FROM Agent_Master WHERE  Company_code='" + ccode + "' AND Plant_code='" + pcode + "') AS Am ON bp.agent_id=Am.Aid ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "') AS Bd ON t1.Bank_Id=Bd.Bid ) AS t2 LEFT JOIN  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm ON t2.plant_code=Pm.Plant_Code ORDER BY t2.agent_id ", conn);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    protected void ExportToExcelAll(string strConn, System.Data.DataTable dtSQL)
    {
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                // Create a new sheet in the Excel spreadsheet.
                OleDbCommand cmd = new OleDbCommand("create table ALLData(agent_id Varchar(28),Name Varchar(28),Amount Double)", conn);

                // Open the connection.
                conn.Open();

                // Execute the OleDbCommand.
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO ALLData(agent_id,Name,Amount) values (?,?,?)";

                // Add the parameters.
                // cmd.Parameters.Add("Tid", OleDbType.Integer);  
                cmd.Parameters.Add("agent_id", OleDbType.VarChar, 28, "agent_id");
                cmd.Parameters.Add("Name", OleDbType.VarChar, 28, "Name");
                cmd.Parameters.Add("Amount", OleDbType.Double, 8, "Amount");

                // Initialize an OleDBDataAdapter object.
                OleDbDataAdapter da = new OleDbDataAdapter("select * from ALLData", conn);

                // Set the InsertCommand of OleDbDataAdapter, 
                // which is used to insert data.
                da.InsertCommand = cmd;
                // Changes the Rowstate()of each DataRow to Added,
                // so that OleDbDataAdapter will insert the rows.
                foreach (DataRow dr in dtSQL.Rows)
                {
                    dr.SetAdded();
                }
                // Insert the data into the Excel spreadsheet.
                da.Update(dtSQL);

            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_ExportIngcsv_Click(object sender, EventArgs e)
    {

        Ingcsv();
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."
    }


    private void BindGrid()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        //string query = "SELECT CONVERT(NVARCHAR(20),Added_Date,103) AS OrderID,Agent_Id,Agent_Name AS ProductName,NetAmount AS Price FROM BankPaymentllotment where Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "' And Billfrmdate='" + d1.ToString() + "' AND Billtodate='" + d2.ToString() + "' Order By Added_Date,Agent_Id";
        string query = "SELECT t1.OrderID,t1.Agent_Id,t1.ProductName,t1.Price,UPPER(Bd.Bankname) AS Bankname FROM (SELECT CONVERT(NVARCHAR(20),Added_Date,103) AS OrderID,Agent_Id,UPPER(REPLACE(Agent_Name,'.',' ')) AS ProductName,NetAmount AS Price,Bank_Id FROM BankPaymentllotment where Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "' And Billfrmdate='" + d1.ToString() + "' AND Billtodate='" + d2.ToString() + "' ) AS t1 INNER JOIN  (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "') AS Bd ON t1.Bank_Id=Bd.Bid Order By t1.OrderID,Agent_Id";
        string conString = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView4.DataSource = dt;
                        GridView4.DataBind();
                    }
                }
            }
        }
    }

    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            subTotal = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                //int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["OrderID"]);
                string orderId = dt.Rows[e.Row.RowIndex]["OrderID"].ToString();
                total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Price"]);
                if (orderId != currentId)
                {
                    if (e.Row.RowIndex > 0)
                    {
                        for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                        {
                            subTotal += Convert.ToDecimal(GridView4.Rows[i].Cells[2].Text);
                        }

                        this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
                        subTotalRowIndex = e.Row.RowIndex;
                    }
                    this.AddTitleRow("File Name", orderId.ToString());
                    currentId = orderId;
                }
            }
        }
        catch (Exception ex)
        {
        }

    }
    private void AddTitleRow(string labelText, string value)
    {
        try
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
            // row.Cells.AddRange(new TableCell[1] {new TableCell { Text = labelText+'_'+value, HorizontalAlign = HorizontalAlign.Right},});
            TableCell cell = new TableCell();
            cell.Text = labelText + '_' + value;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 3;
            cell.CssClass = "GroupHeaderStyle";
            row.Cells.Add(cell);
            GridView4.Controls[0].Controls.Add(row);
        }
        catch (Exception ex)
        {
        }
    }

    private void AddTotalRow(string labelText, string value)
    {
        try
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
            row.Cells.AddRange(new TableCell[3] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });
            GridView4.Controls[0].Controls.Add(row);
        }
        catch (Exception ex)
        {
        }
    }
    private void AddAllottedRow(string labelText, string value)
    {
        try
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            row.BackColor = ColorTranslator.FromHtml("#F9F9F9");

            TableCell cell = new TableCell();
            cell.Text = labelText;
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            TableCell cell1 = new TableCell();
            cell1.Text = value;
            cell1.HorizontalAlign = HorizontalAlign.Right;
            row.Cells.Add(cell1);
            GridView4.Controls[0].Controls.Add(row);
        }
        catch (Exception ex)
        {
        }
    }
    private void AddGrandTotalAllottedRow(string labelText, string value)
    {
        try
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            row.BackColor = ColorTranslator.FromHtml("#F9F9F9");

            TableCell cell = new TableCell();
            cell.Text = labelText;
            cell.HorizontalAlign = HorizontalAlign.Left;
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            TableCell cell1 = new TableCell();
            cell1.Text = value;
            cell1.HorizontalAlign = HorizontalAlign.Right;
            row.Cells.Add(cell1);
            GridView4.Controls[0].Controls.Add(row);
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        try
        {
            for (int i = subTotalRowIndex; i < GridView4.Rows.Count; i++)
            {
                subTotal += Convert.ToDecimal(GridView4.Rows[i].Cells[2].Text);
            }
            this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
            this.AddTotalRow("Total", total.ToString("N2"));
            LoadAllotmentDetails();
        }
        catch (Exception ex)
        {
        }
    }
    private void LoadAllotmentDetails()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        string query = "Select CONVERT(NVARCHAR(10),Date,103) AS AllotedDate,Time,CAST(Amount AS DECIMAL(18,2)) AS AllotAmt from AdminAmountAllotToPlant  Where Plant_code='" + ddl_Plantcode.SelectedItem.Value + "' And Billfrmdate='" + d1.ToString() + "' AND Billtodate='" + d2.ToString() + "'  Order By  Date";
        string conString = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        decimal Gtotal = 0;
                        while (dr.Read())
                        {
                            string mes = dr["AllotedDate"].ToString() + '_' + dr["Time"].ToString();
                            decimal val = Convert.ToDecimal(dr["AllotAmt"]);
                            Gtotal = Gtotal + val;
                            this.AddAllottedRow(mes, val.ToString("N2"));
                        }
                        this.AddGrandTotalAllottedRow("Total Allotted Amount", Gtotal.ToString("N2"));
                    }

                }
            }
        }
    }

    private void ExportExcel()
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AllData.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView4.AllowPaging = false;
            // BindGrid();
            GridView4.HeaderRow.Style.Add("background-color", "#FFFFFF");//#3AC0F2.#FFFFFF,#507CD1
            for (int a = 0; a < GridView4.HeaderRow.Cells.Count; a++)
            {
                GridView4.HeaderRow.Cells[a].Style.Add("background-color", "#3AC0F2");
            }
            int j = 1;
            foreach (GridViewRow gvrow in GridView4.Rows)
            {
                gvrow.BackColor = Color.White;
                if (j <= GridView4.Rows.Count)
                {
                    if (j % 2 != 0)
                    {
                        for (int k = 0; k < gvrow.Cells.Count; k++)
                        {
                            gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                        }
                    }
                }
                j++;
            }
            GridView4.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
        }

    }

    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_Hdfc_Click(object sender, EventArgs e)
    {
        HdfcExcel();
    }

    protected DataTable RetrieveDataHdfc()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            //DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            // dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            //string d3 = dt3.ToString("MM/dd/yyyy");
            string d3 = ddl_Addeddate.SelectedItem.Value.ToString();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_HdfcUploadFile]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                sqlCmd.Parameters.AddWithValue("@spdate", d3);
                da = new SqlDataAdapter(sqlCmd);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    protected void ExportToExcelHdfc(string strConn, System.Data.DataTable dtSQL)
    {
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                //// Create a new sheet in the Excel spreadsheet.
                OleDbCommand cmd = new OleDbCommand("create table SALARY(ACCOUNT Varchar(25),C  Varchar(28),AMOUNT Double,NARRATION Varchar(100))", conn);

                // Open the connection.
                conn.Open();

                // Execute the OleDbCommand.
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO SALARY(ACCOUNT,C,AMOUNT,NARRATION) values (?,?,?,?)";

                //// Add the parameters.
                //// cmd.Parameters.Add("Tid", OleDbType.Integer);  
                cmd.Parameters.Add("ACCOUNT", OleDbType.VarChar, 25, "ACCOUNT");
                cmd.Parameters.Add("C", OleDbType.VarChar, 28, "C");
                cmd.Parameters.Add("AMOUNT", OleDbType.Double, 8, "AMOUNT");
                cmd.Parameters.Add("NARRATION", OleDbType.VarChar, 28, "NARRATION");

                // Initialize an OleDBDataAdapter object.
                OleDbDataAdapter da = new OleDbDataAdapter("select * from SALARY", conn);

                // Set the InsertCommand of OleDbDataAdapter, 
                // which is used to insert data.
                da.InsertCommand = cmd;
                // Changes the Rowstate()of each DataRow to Added,
                // so that OleDbDataAdapter will insert the rows.
                foreach (DataRow dr in dtSQL.Rows)
                {
                    dr.SetAdded();
                }
                // Insert the data into the Excel spreadsheet.
                da.Update(dtSQL);
            }

        }
        catch (Exception ex)
        {
        }
    }
    private void Ing()
    {
        try
        {
            string strDownloadFileName = "";
            string strExcelConn = "";
            strDownloadFileName = @"C:/BILL VYSHNAVI/" + "INGBulkFileUpload" + ".xls";
            string MFileName = @"C:/BILL VYSHNAVI/" + "INGBulkFileUpload" + ".xls";
            string path = @"C:/BILL VYSHNAVI/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(strDownloadFileName.ToString()))
            {
                File.Delete(file.FullName.ToString());
            }
            if (File.Exists(strDownloadFileName.ToString()))
            {
                File.Delete(file.FullName.ToString());
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            else
            {
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            // Retrieve data from SQL Server table.
            DataTable dtSQL = RetrieveData1();
            // Export data to an Excel spreadsheet.
            ExportToExcel(strExcelConn, dtSQL);

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

        }
        catch (Exception ex)
        {
        }
    }
    private void Ingcsv()
    {
        try
        {
            string strDownloadFileName = "";
            string strExcelConn = "";
            DateTime curdate = System.DateTime.Now;
            string dd = curdate.ToString("dd");
            string mm = curdate.ToString("MM");
            string yy = curdate.ToString("yyyy");
            string fname = "SVD" + dd + mm + yy;
            strDownloadFileName = @"C:/BILL VYSHNAVI/" + fname + ".xls";
            string MFileName = @"C:/BILL VYSHNAVI/" + fname + ".xls";
            string path = @"C:/BILL VYSHNAVI/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(strDownloadFileName.ToString()))
            {
                //File.Delete(file.FullName.ToString());
            }
            if (File.Exists(strDownloadFileName.ToString()))
            {
                // File.Delete(file.FullName.ToString());
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            else
            {
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            // Retrieve data from SQL Server table.
            DataTable dtSQL = RetrieveData2();
            // Export data to an Excel spreadsheet.
            ExportToExcel2(strExcelConn, dtSQL);

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

        }
        catch (Exception ex)
        {
        }
    }
    private void HdfcExcel()
    {
        try
        {
            string strDownloadFileName = "";
            string strExcelConn = "";
            DateTime curdate = System.DateTime.Now;
            string dd = curdate.ToString("dd");
            string mm = curdate.ToString("MM");
            string yy = curdate.ToString("yyyy");
            string fname = "SVD" + dd + mm + yy;
            // string fname = "Dairy HDFC UPLOAD11 - 20";
            strDownloadFileName = @"C:/BILL VYSHNAVI/" + fname + ".xls";
            string MFileName = @"C:/BILL VYSHNAVI/" + fname + ".xls";
            string path = @"C:/BILL VYSHNAVI/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(strDownloadFileName.ToString()))
            {
                //File.Delete(file.FullName.ToString());
            }
            if (File.Exists(strDownloadFileName.ToString()))
            {
                // File.Delete(file.FullName.ToString());
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            else
            {
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            // Retrieve data from SQL Server table.
            DataTable dtSQL = RetrieveDataHdfc();
            // Export data to an Excel spreadsheet.
            ExportToExcelHdfc(strExcelConn, dtSQL);

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

        }
        catch (Exception ex)
        {
        }
    }
    private void OthersToHdfcExportGridTomacroformat()
    {
        int cc = 0;
        pcode = ddl_Plantcode.SelectedItem.Value;
        cc = LoadOthersToHDFCmacroformatGrid();
        Response.Clear();
        Response.Buffer = true;

        string dat, mon;
        dat = System.DateTime.Now.ToString("dd");
        mon = System.DateTime.Now.ToString("MM");
        filename = dat + mon;
        string rowcount = string.Empty;

        if (cc < 10)
        {
            rowcount = "00" + cc.ToString();
        }
        else if (cc >= 10 && cc < 100)
        {
            rowcount = "0" + cc.ToString();
        }
        else if (cc >= 100)
        {
            rowcount = cc.ToString();
        }

        // Response.AddHeader("content-disposition", "attachment;filename=bms.009");
        //  Response.AddHeader("content-disposition", "attachment;filename=SVD2" + filename + "." + rowcount);
        Response.AddHeader("content-disposition", "attachment;filename=" + planttype + filename + "." + rowcount);
        Response.Charset = "";
        Response.ContentType = "application/text";
        GridView6.AllowPaging = false;
        GridView6.DataBind();
        StringBuilder Rowbind = new StringBuilder();

        //Add Header in UploadFile
        //int L = GridView6.Columns.Count;
        //int M = 1;
        //for (int k = 0; k < GridView6.Columns.Count; k++)
        //{
        //    if (M == L)
        //    {
        //        Rowbind.Append(GridView6.Columns[k].HeaderText);
        //    }
        //    else
        //    {
        //        Rowbind.Append(GridView6.Columns[k].HeaderText + ',');
        //    }
        //    M++;

        //}
        //Rowbind.Append("\r\n");

        //Add Rows in UploadFile
        for (int i = 0; i < GridView6.Rows.Count; i++)
        {
            int s = GridView6.Columns.Count;
            int j = 1;
            for (int k = 0; k < GridView6.Columns.Count; k++)
            {
                if (j == s)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text);
                }
                else if (j == 1)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                }
                else if (j == 4)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',');
                }
                else if (j == 5)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',');
                }
                else if (j == 6)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                }
                else if (j == 8)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                }
                else
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',');
                }
                j++;

            }

            Rowbind.Append("\r\n");
        }
        Response.Output.Write(Rowbind.ToString());
        Response.Flush();
        Response.End();

    }
    public int LoadOthersToHDFCmacroformatGrid()
    {

        DataTable dt = new DataTable();
        int c = 0;
        try
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dtt = new DataTable();
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            //  DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            //dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            // string d3 = dt3.ToString("MM/dd/yyyy");
            string d3 = ddl_Addeddate.SelectedItem.Value.ToString();

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_OthersToHdfcUploadFile]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandTimeout=500;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                sqlCmd.Parameters.AddWithValue("@spdate", d3);
                da = new SqlDataAdapter(sqlCmd);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);

                c = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    dtt.Columns.Add("TranType");
                    dtt.Columns.Add("ACCOUNT");
                    dtt.Columns.Add("AMOUNT");
                    dtt.Columns.Add("AgentName");
                    dtt.Columns.Add("Agent_Id");
                    dtt.Columns.Add("PayDate");
                    dtt.Columns.Add("IfscCode");
                    dtt.Columns.Add("BankName");
                    dtt.Columns.Add("Pmail");
                    foreach (DataRow Drf in dt.Rows)
                    {
                        string gettrantype = Drf[0].ToString();
                        string ACCOUNT = Drf[1].ToString();
                        //string AMOUNT = Drf[2].ToString();
                        string AgentName = Drf[3].ToString();
                        Agent_Id = Drf[4].ToString();
                        string PayDate = Drf[5].ToString();
                        string IfscCode = Drf[6].ToString();
                        string BankName = Drf[7].ToString();
                        string Pmail = Drf[8].ToString();
                        getaddeddetails();
                        double getamt;
                        double getamount;
                        try
                        {
                            getamt = Convert.ToDouble(dtaddamount.Rows[0][0]);
                        }
                        catch
                        {
                            getamt = 0;

                        }
                        try
                        {
                            getamount = Convert.ToDouble(Drf[2].ToString());
                        }
                        catch
                        {
                            getamount = 0;
                        }

                        getamount = getamount + getamt;

                        dtt.Rows.Add(gettrantype, ACCOUNT, getamount, AgentName, Agent_Id, PayDate, IfscCode, BankName, Pmail);
                    }
                    GridView6.DataSource = dtt;
                    GridView6.DataBind();
                }
                else
                {
                    GridView6.DataSource = null;
                    GridView6.DataBind();
                }
            }
            return c;
        }
        catch
        {
            return c;
        }
    }


    public void getaddeddetails()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        //  DateTime dt3 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        //dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        con = dbaccess.GetConnection();
        string stt = "select  CONVERT(decimal(18,2), isnull(FLOOR(SUM(TotAmount)),0)) as  TotAmount  from  AgentExcesAmount   where plant_code='" + pcode + "' and  Frm_date='" + d1 + "'  and To_date='" + d2 + "'  and Agent_id='" + Agent_Id + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        dtaddamount.Rows.Clear();
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(dtaddamount);


    }
    private void HdfcExportGridTomacroformat()
    {
        int cc = 0;
        pcode = ddl_Plantcode.SelectedItem.Value;
        cc = LoadHDFCmacroformatGrid();
        Response.Clear();
        Response.Buffer = true;

        string dat, mon;
        dat = System.DateTime.Now.ToString("dd");
        mon = System.DateTime.Now.ToString("MM");
        filename = dat + mon;
        string rowcount = string.Empty;

        if (cc < 10)
        {
            rowcount = "00" + cc.ToString();
        }
        else if (cc >= 10 && cc < 100)
        {
            rowcount = "0" + cc.ToString();
        }
        else if (cc >= 100)
        {
            rowcount = cc.ToString();
        }

        // Response.AddHeader("content-disposition", "attachment;filename=bms.009");
        //   Response.AddHeader("content-disposition", "attachment;filename=SVD2H" + filename + "." + rowcount );
        Response.AddHeader("content-disposition", "attachment;filename=" + planttypehdfc + filename + "." + rowcount);
        Response.Charset = "";
        Response.ContentType = "application/text";
        GridView5.AllowPaging = false;
        GridView5.DataBind();
        StringBuilder Rowbind = new StringBuilder();


        int L = GridView5.Columns.Count;
        int M = 1;
        for (int k = 0; k < GridView5.Columns.Count; k++)
        {
            if (M == L)
            {
                Rowbind.Append(GridView5.Columns[k].HeaderText);
            }
            else
            {
                Rowbind.Append(GridView5.Columns[k].HeaderText + ',');
            }
            M++;

        }
        Rowbind.Append("\r\n");


        for (int i = 0; i < GridView5.Rows.Count; i++)
        {
            int s = GridView5.Columns.Count;
            int j = 1;
            for (int k = 0; k < GridView5.Columns.Count; k++)
            {
                if (j == s)
                {
                    Rowbind.Append(GridView5.Rows[i].Cells[k].Text);
                }
                else
                {
                    Rowbind.Append(GridView5.Rows[i].Cells[k].Text + ',');
                }
                j++;

            }

            Rowbind.Append("\r\n");
        }
        Response.Output.Write(Rowbind.ToString());
        Response.Flush();
        Response.End();

    }
    public int LoadHDFCmacroformatGrid()
    {
        DataTable dt = new DataTable();
        int c = 0;
        try
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            //  DateTime dt3 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            //dt3 = DateTime.ParseExact(ddl_Addeddate.SelectedItem.Value, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            // string d3 = dt3.ToString("MM/dd/yyyy");
            string d3 = ddl_Addeddate.SelectedItem.Value.ToString();

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_HdfcUploadFile]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                sqlCmd.Parameters.AddWithValue("@spdate", d3);
                da = new SqlDataAdapter(sqlCmd);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
                //foreach (DataRow drs in dt.Rows)
                //{
                //  // string get

                //}
                //SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                // sqlDa.Fill(dt);
                c = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    GridView5.DataSource = dt;
                    GridView5.DataBind();
                }
                else
                {
                    GridView5.DataSource = null;
                    GridView5.DataBind();
                }
            }
            return c;
        }
        catch
        {
            return c;
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {

        try
        {


            GETUPDATE();

            if (GETID == 0)
            {

                getplanttype();
                if (rbtLstReportItems.SelectedItem != null)
                {
                    Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                    if (Lbl_selectedReportItem.Text == "ALL")
                    {
                        getupdatefilelock();
                        ExportExcel();

                    }
                    else if (Lbl_selectedReportItem.Text == "WSbi")
                    {

                        ExportGridToText();
                        getupdatefilelock();
                    }
                    else if (Lbl_selectedReportItem.Text == "PSbi")
                    {
                        getupdatefilelock();
                        ExportGridToTextSBIPaymentAllotmentList();

                    }
                    else if (Lbl_selectedReportItem.Text == "Ing")
                    {
                        Ing();
                        getupdatefilelock();
                    }
                    else if (Lbl_selectedReportItem.Text == "Ing1")
                    {
                        getupdatefilelock();
                        Ingcsv();

                    }
                    else if (Lbl_selectedReportItem.Text == "Hdfc")  //working
                    {
                        getupdatefilelock();
                        HdfcExportGridTomacroformat();

                    }
                    else if (Lbl_selectedReportItem.Text == "Hdfcoth") //working
                    {
                        getupdatefilelock();
                        OthersToHdfcExportGridTomacroformat();
                        HdfcExcel();

                    }
                    else if (Lbl_selectedReportItem.Text == "pendingList")
                    {
                        getupdatefilelock();

                        PendingBankPaymentData();

                    }
                    else
                    {
                        WebMsgBox.Show("Please Check the Selected Items");
                    }

                }
                else
                {

                    WebMsgBox.Show("Please Select the Report Type");
                }

            }


            else
            {
                WebMsgBox.Show("File Already Updated");


            }





        }
        catch
        {




        }
    }


    private void createNode(string pID, string pName, string pPrice, XmlTextWriter writer)
    {

        writer.WriteStartElement("Product");
        writer.WriteStartElement("Product_id");
        writer.WriteString(pID);
        writer.WriteEndElement();
        writer.WriteStartElement("Product_name");
        writer.WriteString(pName);
        writer.WriteEndElement();
        writer.WriteStartElement("Product_price");
        writer.WriteString(pPrice);
        writer.WriteEndElement();
        writer.WriteEndElement();
    }






    public void getplanttype()
    {

        if (RadioButtonList1.Text == "BUFF")
        {

            planttype = "SVD1";

            planttypehdfc = "SVD1H";



        }
        if (RadioButtonList1.Text == "COW")
        {

            planttype = "SVD2";

            planttypehdfc = "SVD2H";



        }


    }

    private void PendingBankPaymentData()
    {
        try
        {
            string strDownloadFileName = "";
            string strExcelConn = "";
            strDownloadFileName = @"C:/BILL VYSHNAVI/" + "PaymentPendingList" + ".xls";
            string MFileName = @"C:/BILL VYSHNAVI/" + "PaymentPendingList" + ".xls";
            string path = @"C:/BILL VYSHNAVI/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(strDownloadFileName.ToString()))
            {
                File.Delete(file.FullName.ToString());
            }
            if (File.Exists(strDownloadFileName.ToString()))
            {
                File.Delete(file.FullName.ToString());
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            else
            {
                strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strDownloadFileName + ";Extended Properties='Excel 8.0;HDR=Yes'";
            }
            // Retrieve data from SQL Server table.
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);


            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            DataTable dtSQL = RetrieveData5(ccode, pcode, d1, d2);
            // Export data to an Excel spreadsheet.
            ExportToExcel5(strExcelConn, dtSQL);

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

        }
        catch (Exception ex)
        {
        }
    }


    protected DataTable RetrieveData5(string cccode1, string pcode1, string dt1, string dt2)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
            {
                da = new SqlDataAdapter("SELECT Route_Name,Agent_Name,Netpay FROM (Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp  RIGHT JOIN  (SELECT *FROM (SELECT cart.ARid AS Rid,cart.cartAid AS proAid,(CONVERT(nvarchar(15),(CONVERT(nvarchar(9),cart.cartAid)+'_'+ cart.Agent_Name))) AS Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS Netpay,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM(SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg FROM Procurement WHERE prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "'  AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "'  AND Clearing_Date BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid ) AS FF  LEFT JOIN  (SELECT Route_id AS RRid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS Rout ON FF.Rid=Rout.RRid) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL ORDER BY F2.Rid,bp.Agent_Id ", conn);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    protected void ExportToExcel5(string strConn, System.Data.DataTable dtSQL)
    {
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                // Create a new sheet in the Excel spreadsheet.
                OleDbCommand cmd = new OleDbCommand("create table pendingList(Route_Name  Varchar(30),Agent_Name Varchar(30),Netpay Double)", conn);

                // Open the connection.
                conn.Open();

                // Execute the OleDbCommand.
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO pendingList (Route_Name,Agent_Name,Netpay) values (?,?,?)";

                // Add the parameters.
                // cmd.Parameters.Add("Tid", OleDbType.Integer);              
                cmd.Parameters.Add("Route_Name", OleDbType.VarChar, 30, "Route_Name");
                cmd.Parameters.Add("Agent_Name", OleDbType.VarChar, 30, "Agent_Name");
                cmd.Parameters.Add("Netpay", OleDbType.Double, 8, "Netpay");

                // Initialize an OleDBDataAdapter object.
                OleDbDataAdapter da = new OleDbDataAdapter("select Route_Name,Agent_Name,Netpay from pendingList", conn);

                // Set the InsertCommand of OleDbDataAdapter, 
                // which is used to insert data.
                da.InsertCommand = cmd;
                // Changes the Rowstate()of each DataRow to Added,
                // so that OleDbDataAdapter will insert the rows.
                foreach (DataRow dr in dtSQL.Rows)
                {
                    dr.SetAdded();
                }
                // Insert the data into the Excel spreadsheet.
                da.Update(dtSQL);

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void Datechange()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");
    }

    private void LoadUploadedFilesDetails()
    {
        try
        {
            using (con = dbaccess.GetConnection())
            {
                Datechange();
                DataTable dt = new DataTable();
                DataTable dtexcess = new DataTable();
                GridView7.Dispose();
                // NAVEEN123

                DataTable Report = new DataTable();
                Report.Columns.Add("serial_no");
                Report.Columns.Add("ActualNoofROws");
                Report.Columns.Add("BankFileName");
                Report.Columns.Add("TotalMilkAmount");
                Report.Columns.Add("TotalExcessAmount");
                Report.Columns.Add("TotalAmount");
                string str = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY BankFileName) AS serial_no,COUNT(BankFileName) AS ActualNoofROws,UPPER(BankFileName) AS BankFileName,SUM(NetAmount) AS TotalAmount FROM BankPaymentllotment Where Plant_Code='" + pcode.Trim() + "' AND Billfrmdate='" + d1.ToString().Trim() + "' Group By BankFileName ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow newrow = Report.NewRow();
                        newrow["serial_no"] = dr["serial_no"].ToString();
                        newrow["ActualNoofROws"] = dr["ActualNoofROws"].ToString();
                        newrow["BankFileName"] = dr["BankFileName"].ToString();
                        newrow["TotalMilkAmount"] = dr["TotalAmount"].ToString();
                        string excessamt = "0";
                        string milkamt = dr["TotalAmount"].ToString();
                        string bankfilename = dr["BankFileName"].ToString();
                        string excess = "Select    NetAmount,ExcessAmt    from (Select   convert(varchar,Sum(NetAmount)) as  NetAmount  , convert(varchar,Sum(Exx)) as ExcessAmt  from (Select   Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),(floor(NetAmount)))  as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number,(ISNULL(ExcessAmt,0)) as Exx  from (sELECT Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number  FROM (select Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id   from (select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount,Plant_Code,convert(varchar,Added_Date,103) as Date,Bank_id    from BankPaymentllotment  where plant_code='" + pcode + "'   and    Billfrmdate='" + d1 + "'  and Billtodate='" + d2 + "' AND  BankFileName='" + bankfilename + "'  and NetAmount > 0) as ff  left join (Select pmail,Milktype,Plant_Code as pcode   from Plant_Master   where plant_code='" + pcode + "' ) as pm on ff.Plant_Code= pm.pcode   ) AS FF  LEFT JOIN (sELECT Plant_code AS amplantcode,Agent_Id as agent,phone_Number   FROM Agent_Master   WHERE plant_code='" + pcode + "' GROUP BY Plant_code,Agent_Id,phone_Number)  AS AM ON FF.Plant_Code=AM.amplantcode   and FF.Agent_Id=AM.agent) as lleft left join (sELECT  Agent_id as exAgentid ,floor(isnull(sUM(TotAmount),0)) AS ExcessAmt   FROM AgentExcesAmount     WHERE Plant_code='" + pcode + "'  AND Frm_date='" + d1 + "'    AND To_date='" + d2 + "'   group by Agent_id) as agentexpay  on lleft.Agent_Id=agentexpay.exAgentid) as spl) as  ff";
                        SqlCommand cmdd = new SqlCommand(excess, con);
                        SqlDataAdapter daa = new SqlDataAdapter(cmdd);
                        daa.Fill(dtexcess);
                        if (dtexcess.Rows.Count > 0)
                        {
                            foreach (DataRow dre in dtexcess.Rows)
                            {
                                newrow["TotalExcessAmount"] = dre["ExcessAmt"].ToString();
                                excessamt = dre["ExcessAmt"].ToString();
                            }
                        }

                        double totmilkamt = Convert.ToDouble(milkamt);
                        double totexcessamt = Convert.ToDouble(excessamt);
                        double totamt = totmilkamt + totexcessamt;
                        newrow["TotalAmount"] = totamt.ToString();
                        Report.Rows.Add(newrow);
                    }
                    GridView7.DataSource = Report;
                    GridView7.DataBind();
                }

                else
                {
                    GridView7.DataSource = null;
                    GridView7.DataBind();


                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridView8.Visible = true;
        Button2.Visible = true;

        getid = (GridView7.SelectedRow.Cells[3].Text).ToString();

        getgrid();





    }


    public void getgrid()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");
        string get;
        con = dbaccess.GetConnection();
        get = "SELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,NETAMOUNT,BANK_NAME    FROM (sELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,NETAMOUNT,BANK_ID   FROM  BankPaymentllotment    WHERE PLANT_CODE='" + pcode + "'  AND   BANKFILENAME='" + getid + "'  AND  billfrmdate='" + d1 + "'  and billtodate='" + d2 + "') AS BANK  LEFT JOIN (sELECT  BANK_NAME,BANK_ID,IFSC_CODE  FROM  BANK_MASTER    WHERE PLANT_CODE='" + pcode + "'   GROUP BY  BANK_NAME,BANK_ID,IFSC_CODE    ) AS GG ON  BANK.BANK_ID=GG.BANK_ID AND BANK. IFSCCODE =GG.IFSC_CODE";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);


        if (dt.Rows.Count > 0)
        {
            GridView8.DataSource = dt;
            GridView8.DataBind();
            GridView8.FooterRow.Cells[3].Text = "TOTAL AMOUNT";
            decimal milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("NETAMOUNT"));
            GridView8.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            GridView8.FooterRow.Cells[5].Text = milkkg.ToString("N2");
        }
        else
        {
            GridView8.DataSource = null;
            GridView8.DataBind();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = txt_FromDate.Text + txt_ToDate.Text + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView8.GridLines = GridLines.Both;
            GridView8.HeaderStyle.Font.Bold = true;
            GridView8.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        catch
        {
            string message;
            message = "Please Check Your Data";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }
    }

    public void getupdatefilelock()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");
        con = dbaccess.GetConnection();
        string strrtime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string getlock = "update  BankPaymentllotment set  UpdatedUser='" + 1 + "',UpdatedUserName='" + Session["Name"] + "',UpdatedTime='" + strrtime + "'   where plant_code='" + pcode + "'  and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "' and BankFileName='" + ddl_Addeddate.SelectedItem.Text + "' ";
        SqlCommand cmd = new SqlCommand(getlock, con);
        cmd.ExecuteNonQuery();
    }
    public void GETUPDATE()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");
        string LOCKSTSTUS;
        con = dbaccess.GetConnection();
        LOCKSTSTUS = "SELECT *   FROM  BankPaymentllotment  WHERE PLANT_CODE='" + pcode + "'   AND  Billfrmdate='" + d1 + "'  and  BankFileName='" + ddl_Addeddate.SelectedItem.Text + "'  AND Billtodate='" + d2 + "' AND  (UpdatedUser IS NULL or  UpdatedUser=0) ";
        SqlDataAdapter DSP = new SqlDataAdapter(LOCKSTSTUS, con);
        DataTable DTS = new DataTable();
        DSP.Fill(DTS);
        if (DTS.Rows.Count > 0)
        {
            GETID = 0;
        }
        else
        {
            GETID = 1;
        }
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    public void getbankdeatils()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        con = dbaccess.GetConnection();
     //   string getquery = "select Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id   from (select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount,Plant_Code,convert(varchar,Added_Date,103) as Date,Bank_id    from BankPaymentllotment  where plant_code='" + pcode + "'   and    Billfrmdate='" + d1 + "'  and Billtodate='" + d2 + "' AND  BankFileName='" + ddl_Addeddate.SelectedItem.Text + "'  and NetAmount > 0) as ff  left join (Select pmail,Milktype,Plant_Code as pcode   from Plant_Master   where plant_code='" + pcode + "' ) as pm on ff.Plant_Code= pm.pcode  order by Date asc";
      //  string getquery = "sELECT Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number  FROM (select Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id   from (select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount,Plant_Code,convert(varchar,Added_Date,103) as Date,Bank_id    from BankPaymentllotment  where plant_code='" + pcode + "'   and    Billfrmdate='" + d1 + "'  and Billtodate='" + d2 + "' AND  BankFileName='" + ddl_Addeddate.SelectedItem.Text + "'  and NetAmount > 0) as ff  left join (Select pmail,Milktype,Plant_Code as pcode   from Plant_Master   where plant_code='" + pcode + "' ) as pm on ff.Plant_Code= pm.pcode   ) AS FF  LEFT JOIN (sELECT Plant_code AS amplantcode,Agent_Id as agent,phone_Number   FROM Agent_Master   WHERE plant_code='" + pcode + "' GROUP BY Plant_code,Agent_Id,phone_Number)  AS AM ON FF.Plant_Code=AM.amplantcode   and FF.Agent_Id=AM.agent";
        string getquery = "Select   Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),(floor(NetAmount)) + (ISNULL(ExcessAmt,0)))   as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number  from (sELECT Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number  FROM (select Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id   from (select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount,Plant_Code,convert(varchar,Added_Date,103) as Date,Bank_id    from BankPaymentllotment  where plant_code='" + pcode + "'   and    Billfrmdate='" + d1 + "'  and Billtodate='" + d2 + "' AND  BankFileName='" + ddl_Addeddate.SelectedItem.Text + "' and NetAmount > 0) as ff  left join (Select pmail,Milktype,Plant_Code as pcode   from Plant_Master   where plant_code='" + pcode + "'  ) as pm on ff.Plant_Code= pm.pcode   ) AS FF  LEFT JOIN (sELECT Plant_code AS amplantcode,Agent_Id as agent,phone_Number   FROM Agent_Master   WHERE plant_code='" + pcode + "' GROUP BY Plant_code,Agent_Id,phone_Number)  AS AM ON FF.Plant_Code=AM.amplantcode   and FF.Agent_Id=AM.agent) as lleft left join (sELECT  Agent_id as exAgentid ,floor(isnull(sUM(TotAmount),0)) AS ExcessAmt   FROM AgentExcesAmount     WHERE plant_code='" + pcode + "'   AND Frm_date='" + d1 + "'    AND To_date='"+d2+"'   group by Agent_id) as agentexpay  on lleft.Agent_Id=agentexpay.exAgentid";
        SqlCommand cmd = new SqlCommand(getquery, con);
        SqlDataAdapter dsp = new SqlDataAdapter(cmd);
        DataTable getagentrows = new DataTable();
        getagentrows.Rows.Clear();
        dsp.Fill(getagentrows);
        showreport.Rows.Clear();
        showreport.Columns.Clear();
        showreport.Columns.Add("Report");
        foreach (DataRow drg in getagentrows.Rows)
        {
            string getagentid = drg[0].ToString();
            string Agent_Name = drg[1].ToString();
            string Ifsccode = drg[2].ToString();
            string Account_no = drg[3].ToString();
            double NetAmount = Convert.ToDouble(drg[4].ToString());
            string date = drg[5].ToString();
            string pmail = drg[6].ToString();
            string milktype = drg[7].ToString();
            string plcode = drg[8].ToString();
            int bankid = Convert.ToInt16(drg[9]);
            string mobile = drg[10].ToString();
            string accountnu = ddl_kotack.SelectedItem.Value;
            string companyname = "Vyshnavi Dairy";
            //if (milktype == "1")
            //{
            //    ptype = "SVDSPL";
            //}
            //if (milktype == "2")
            //{
            //    ptype = "SVDPL";
            //}
            string pay = "RPAY";
            if (bankid == 23)
            {
                paymode = "IFT";
            }
            if (bankid != 23)
            {
                if (NetAmount > 200000)
                {
                    paymode = "RTGS";
                }
                else
                {
                    paymode = "NEFT";  
                }
            }
            if ((accountnu == "425044000438") || (accountnu == "328044039913") || (accountnu == "334044049195") || (accountnu == "337044040029"))
            {
                ptype = "SVDSPL";

            }
            if (accountnu == "334044032411")
            {
                ptype = "SVDPL";

            }
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            string condate = dt.ToString("dd/MM/yyyy");
            string[] SPP = ddl_Plantname.SelectedItem.Text.Split('_');
            string LOCALPLANT = SPP[1].ToString();
            statement = ptype + "~" + pay + "~" + paymode + "~~" + condate + "~" + accountnu + "~" + NetAmount + "~~" + Agent_Name + "~" + Ifsccode + "~" + Account_no + "~" + pmail + "~" + mobile + "~" +LOCALPLANT + getagentid + "~" + companyname + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            showreport.Rows.Add(statement);
        }
        if (showreport.Rows.Count > 0)
        {
            GridView9.DataSource = showreport;
            GridView9.DataBind();

        }
    }
    protected void btn_kotack_Click(object sender, EventArgs e)
    {
        GridView9.Visible = true;
        getbankdeatils();

        getgetbankpayandexcessamt();
        lbl_totamt.Visible = true;
        lbl_totamt.Enabled = false;


    }


    public void getgetbankpayandexcessamt()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        string str="";
        str = "Select    NetAmount,ExcessAmt    from (Select   convert(varchar,Sum(NetAmount)) as  NetAmount  , convert(varchar,Sum(Exx)) as ExcessAmt  from (Select   Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),(floor(NetAmount)))  as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number,(ISNULL(ExcessAmt,0)) as Exx  from (sELECT Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number  FROM (select Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id   from (select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount,Plant_Code,convert(varchar,Added_Date,103) as Date,Bank_id    from BankPaymentllotment  where plant_code='" + pcode + "'   and    Billfrmdate='" + d1 + "'  and Billtodate='" + d2 + "' AND  BankFileName='" + ddl_Addeddate .SelectedItem.Text+ "'  and NetAmount > 0) as ff  left join (Select pmail,Milktype,Plant_Code as pcode   from Plant_Master   where plant_code='"+pcode+"' ) as pm on ff.Plant_Code= pm.pcode   ) AS FF  LEFT JOIN (sELECT Plant_code AS amplantcode,Agent_Id as agent,phone_Number   FROM Agent_Master   WHERE plant_code='"+pcode+"' GROUP BY Plant_code,Agent_Id,phone_Number)  AS AM ON FF.Plant_Code=AM.amplantcode   and FF.Agent_Id=AM.agent) as lleft left join (sELECT  Agent_id as exAgentid ,floor(isnull(sUM(TotAmount),0)) AS ExcessAmt   FROM AgentExcesAmount     WHERE Plant_code='"+pcode+"'  AND Frm_date='"+d1+"'    AND To_date='"+d2+"'   group by Agent_id) as agentexpay  on lleft.Agent_Id=agentexpay.exAgentid) as spl) as  ff";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dsp = new SqlDataAdapter(cmd);
        DataTable milkandexcess = new DataTable();
        dsp.Fill(milkandexcess);
        if (milkandexcess.Rows.Count > 0)
        {
            milkpayamt    =     Convert.ToDouble(milkandexcess.Rows[0][0]);
            excesspayamt  =   Convert.ToDouble(milkandexcess.Rows[0][1]);
            lbl_totamt.Text = "Milk Amount:" + milkpayamt + " Excess Amount:" + excesspayamt + " Total Amount:" + (milkpayamt + excesspayamt).ToString();
        }

    }

    public void ExportGridToText12()
    {
      
        ////Response.Clear();
        ////Response.Buffer = true;
        ////string FileName = pcode + txt_FromDate.Text + txt_ToDate.Text + DateTime.Now + ".txt";
        //////    Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.txt");
        ////Response.AddHeader("content-disposition", "attachment;" + FileName);

        ////Response.Charset = "";
        ////Response.ContentType = "application/text";
        ////getbankdeatils();

        ////StringBuilder Rowbind = new StringBuilder();
        ////for (int k = 0; k < GridView9.Columns.Count; k++)
        ////{

        ////    Rowbind.Append(GridView9.Columns[k].HeaderText + ' ');
        ////}

        ////Rowbind.Append("\r\n");
        ////for (int i = 0; i < GridView9.Rows.Count; i++)
        ////{
        ////    for (int k = 0; k < GridView9.Columns.Count; k++)
        ////    {

        ////        Rowbind.Append(GridView9.Rows[i].Cells[k].Text + ' ');
        ////    }

        ////    Rowbind.Append("\r\n");
        ////}
        ////Response.Output.Write(Rowbind.ToString());
        ////Response.Flush();
        ////Response.End();

        //try
        //{

        //    string txtFile = string.Empty;

        //    //Adding Column Name In Text File.  
        // //   foreach (TableCell cell in GridView9.HeaderRow.Cells)
        // //   {
        // //       txtFile += cell.Text + "\t\t";
        // //   }

        // ////   txtFile += "\r\n";

        //    //Adding Data Column Values in Text File  
        //    foreach (GridViewRow row in GridView9.Rows)
        //    {
        //        foreach (TableCell cell in row.Cells)
        //        {
        //            txtFile += cell.Text + "\t\t";
        //        }
        //        txtFile += "\r\n";
        //    }

        //    Response.Clear();
        //    Response.Buffer = true;
        //   // Response.AddHeader("content-disposition", "attachment;filename=EmployeeData.txtFile");
        //    string FileName = pcode + txt_FromDate.Text + txt_ToDate.Text + DateTime.Now + ".txtFile";
        //    Response.AddHeader("content-disposition", "attachment;" + FileName);
        //    Response.Charset = "";
        //    Response.ContentType = "application/text";
        //    Response.Output.Write(txtFile);
        //    Response.Flush();
        //    Response.End();
        //}
        //catch
        //{


        //}


         


           string txt = string.Empty;

    //foreach (TableCell cell in GridView9.HeaderRow.Cells)
    //{       
    //    txt += cell.Text + "\t\t";
    //}
   
    //txt += "\r\n";

    foreach (GridViewRow row in GridView9.Rows)
    {
        //Making the space beween cells.
        foreach (TableCell cell in row.Cells)
        {          
          //  txt += cell.Text + "\t\t";
            txt += cell.Text ;
        }
       
        txt += "\r\n";
    }
   
    Response.Clear();
    Response.Buffer = true;
    //here you can give the name of file.

    //string FileName = pcode + txt_FromDate.Text + txt_ToDate.Text + DateTime.Now + ".txt";
    //Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.txt");
    //Response.AddHeader("content-disposition", "attachment;" + FileName);

    DateTime dt = new DateTime();
    dt = System.DateTime.Now;
    string DATEE = dt.ToString("ddMMyy");
    string timee=   String.Format("{0:d/M/yyyy HH:mm:ss}", dt);
    string NAME = ddl_Addeddate.Text + DATEE + ".txt";
    Response.AddHeader("content-disposition", "attachment;filename= " + NAME + "");
    Response.Charset = "";
    Response.ContentType = "application/text";
    Response.Output.Write(txt);
    //FileStream fs = File.Create(txt);
    //File.SetAttributes(txt+".txt",FileAttributes.ReadOnly);
    Response.Flush();
    getupdatefilelock();
    Response.End();
   
  
    }
    protected void btn_kotackexport_Click(object sender, EventArgs e)
    {
        GETUPDATE();
          if (GETID == 0)
          {
              ExportGridToText12();
            //  getupdatefilelock();
             
          }
          else
          {
              WebMsgBox.Show("File Already Updated");


          }
    }
    protected void rbtLstReportItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtLstReportItems.SelectedItem.Value == "8")
        {
            btn_kotack.Visible = true;
            btn_kotackexport.Visible = true;
            GridView9.Visible = true;
            lbl_ktk.Visible = true;
            ddl_kotack.Visible = true;
        }
        else
        {
            btn_kotack.Visible = false;
            btn_kotackexport.Visible = false;
            GridView9.Visible = false;
            ddl_kotack.Visible = false;
            lbl_ktk.Visible = false;
            GridView9.DataSource = null;
            GridView9.DataBind();
        }
    }
    protected void ddl_Addeddate_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView9.Visible = false;
    }
    protected void ddl_Addeddate_TextChanged(object sender, EventArgs e)
    {

    }
}