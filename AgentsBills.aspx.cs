using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class AgentsBills : System.Web.UI.Page
{

    SqlDataReader dr;
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    BOLbillgenerate BOLBill = new BOLbillgenerate();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLpayment Bllpay = new BLLpayment();
    BLLuser Bllusers = new BLLuser();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    DbHelper dbaccess = new DbHelper();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string frmdate;
    //public string todate;
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();

    //Admin Check Flag
    public int Falg = 0;
    public int Plantmilktype = 0;
    public static int roleid;
    public static int buttonviewstatus;


    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DateTime d1;
    DateTime d2;

    DateTime d11;
    DateTime d22;
    //string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;

    string DATE;
    string DATE1;
    string DATE2; // JAN

    int  leaf;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["YEAR"] = Session["YEAR"].ToString();
            ViewState["MONTH"] = Session["MONTH"].ToString();
            string MONT = ViewState["MONTH"].ToString();
            ViewState["pkkode"] = Session["pkkode"].ToString();
            ViewState["pkagent"] = Session["Agentidc"].ToString();

              string AGG = ViewState["pkagent"].ToString();

              string[] GETAG = AGG.Split('_');
              ViewState["pkagent"] = GETAG[0];


            ViewState["pname"] = Session["pnametxt"].ToString();
            ViewState["cname"] = Session["cname"].ToString();

            double getyear = Convert.ToDouble(ViewState["YEAR"]);



            if ((getyear % 1) > 0)
            {
                leaf = 0;



            }
            else
            {
                leaf = 1;
            }

            //string DATE = "1-1- " + ViewState["YEAR"];
            //string DATE1 = "1-31- " + ViewState["YEAR"];


            //string DATE = "1-1- " + ViewState["YEAR"];
            //string DATE1 = "1-31- " + ViewState["YEAR"];


            //string DATE = "1-1- " + ViewState["YEAR"];
            //string DATE1 = "1-31- " + ViewState["YEAR"];


            //string DATE = "1-1- " + ViewState["YEAR"];
            //string DATE1 = "1-31- " + ViewState["YEAR"];

            //string DATE = "1-1- " + ViewState["YEAR"];
            //string DATE1 = "1-31- " + ViewState["YEAR"];


            //string DATE = "1-1- " + ViewState["YEAR"];
            //string DATE1 = "1-31- " + ViewState["YEAR"];


            //string DATE = "1-1- " + ViewState["YEAR"];
            //string DATE1 = "1-31- " + ViewState["YEAR"];






            if (MONT == "JAN")
            {
                string jan = "1";
                DATE = jan + "-1- " + ViewState["YEAR"];
                DATE1 = jan + "-31- " + ViewState["YEAR"]; // JAN


            }
            if (MONT == "FEB")
            {
                string feb = "2";
                if (leaf == 1)
                {

                    DATE = feb + "-1- " + ViewState["YEAR"];
                    DATE1 = feb + "-29- " + ViewState["YEAR"]; // feb

                }

                if (leaf == 0)
                {
                    DATE = feb + "-1- " + ViewState["YEAR"];
                    DATE1 = feb + "-28- " + ViewState["YEAR"]; // feb

                }



            }
            if (MONT == "MAR")
            {
                string mar = "3";
                DATE = mar + "-1- " + ViewState["YEAR"];
                DATE1 = mar + "-31- " + ViewState["YEAR"]; // JAN
            }
            if (MONT == "APR")
            {
                string apr = "4";
                DATE = apr + "-1- " + ViewState["YEAR"];
                DATE1 = apr + "-30- " + ViewState["YEAR"]; // JAN
            }
            if (MONT == "MAY")
            {
                string may = "5";
                DATE = may + "-1- " + ViewState["YEAR"];
                DATE1 = may + "-31- " + ViewState["YEAR"]; // JAN
            }
            if (MONT == "JUN")
            {
                string jun = "6";
                DATE = jun + "-1- " + ViewState["YEAR"];
                DATE1 = jun + "-30- " + ViewState["YEAR"]; // JAN
            }
            if (MONT == "JUL")
            {
                string jul = "7";
                DATE = jul + "-1- " + ViewState["YEAR"];
                DATE1 = jul + "-30- " + ViewState["YEAR"]; // JAN
            }
            if (MONT == "AUG")
            {
                string aug = "8";
                DATE = aug + "-1- " + ViewState["YEAR"];
                DATE1 = aug + "-31- " + ViewState["YEAR"]; // JAN
            }

            if (MONT == "SEP")
            {

                string sep = "9";
                DATE = sep + "-1- " + ViewState["YEAR"];
                DATE1 = sep + "-30- " + ViewState["YEAR"]; // JAN

            } if (MONT == "OCT")
            {
                string oct = "10";
                DATE = oct + "-1- " + ViewState["YEAR"];
                DATE1 = oct + "-31- " + ViewState["YEAR"]; // JAN
            }
            if (MONT == "NOV")
            {
                string nov = "11";
                DATE = nov + "-1- " + ViewState["YEAR"];
                DATE1 = nov + "-30- " + ViewState["YEAR"]; // JAN
            }
            if (MONT == "DEC")
            {
                string dec = "12";
                DATE = dec + "-1- " + ViewState["YEAR"];
                DATE1 = dec + "-31- " + ViewState["YEAR"]; // JAN
            }
            billdate();


        }

        else
        {

            LoadBill();

        }
    }

    public void billdate()
    {

        try
        {

            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ViewState["pkkode"] + "'  and Bill_frmdate between '" + DATE + "' and   '" + DATE1 + "'   order by TID asc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());


                    //FDATE = d1.ToString("MM/dd/yyyy");
                    //TODATE = d2.ToString("MM/dd/yyyy");
                    frmdate = d1.ToString("dd/MM/yyy");
                    Todate = d2.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);

                }


            }
        }
        catch
        {


        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
     
        ViewState["FDATE"] = FDATE;
        ViewState["TODATE"] = TODATE;
        ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
        ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;

        LoadBill();

    }

    //private void SETBO()
    //{
    //    BOLBill.Companycode = int.Parse(ccode);
    //    BOLBill.Plantcode = int.Parse(ddl_Plantcode.SelectedItem.Value);
    //    BOLBill.Frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //    BOLBill.Todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
    //}
    private void LoadBill()
    {
        try
        {
          

            //if (Chk_MilkType.Checked == true)
            //{
            //    cr.Load(Server.MapPath("Report//BillDBuff.rpt"));
            //}
            //else
            //{
            //    cr.Load(Server.MapPath("Report//BillD.rpt"));
            //}

           

            pcode = ViewState["pkkode"].ToString();

           
            Plantmilktype = dbaccess.GetPlantMilktype(pcode);
            if (Plantmilktype == 2)
            {
                cr.Load(Server.MapPath("Report//BillDBuff.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Report//BilldForAgent.rpt"));
            }

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
            //t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_phoneno"];                             

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();



            string date = ddl_BillDate.Text;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

            //ViewState["FDATE"] = FDATE;
            //ViewState["TODATE"] = TODATE;
            //ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            //ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;




            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;

            //dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            t1.Text = ViewState["cname"].ToString();
            t2.Text = ViewState["pname"].ToString();
            t3.Text =  ViewState["FDATE1"].ToString();
            t4.Text = "To : " + ViewState["FDATE2"].ToString();

            // t5.Text = managmobNo;

            string d1 =  ViewState["FDATE"].ToString();
            string d2 = ViewState["TODATE"].ToString();

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

       
            if (Plantmilktype == 2)
            {
                str = "SELECT t1.*,t2.*  FROM " +
          " (SELECT  Route_id,Route_name,Agent_id,Agent_name,Bank_id,Bank_name,Agent_accountNo,Ifsc_code,Milktype,Super_PhoneNo,SInsentAmt,Scaramt,SSplBonus,ClaimAount,SLoanAmount,Billadv,Ai,Feed,can,Recovery,others,SLoanClosingbalance,SAmt,TotAdditions,TotDeductions,Sinstamt,UPPER(Words) AS Words,Roundoff,Smkg,Smltr,Sfatkg,SSnfkg,Aclr,Scans ,NetAmount FROM paymentdata WHERE PLANT_CODE='" + ViewState["pkkode"] + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "') AS t1 " +
          " INNER JOIN " +
          " (SELECT Agent_id,CONVERT(VARCHAR,Prdate,103) AS Prdate,Sessions,ISNULL(NoofCans,0) AS NoofCans,ISNULL(Milk_kg,0) AS Milk_kg,ISNULL(Milk_ltr,0) AS Milk_ltr,ISNULL(Fat,0) AS Fat,ISNULL(fat_kg,0) AS fat_kg,ISNULL(Snf,0) AS Snf,ISNULL(snf_kg,0) AS snf_kg,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,ISNULL(ComRate,0) AS ComRate  from Procurement Where Plant_code='" + ViewState["pkkode"] + "' AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' and agent_id='" + ViewState["pkagent"] + "') AS t2 ON t1.Agent_id=t2.Agent_id ORDER BY  t1.Route_id,t1.Agent_id,t2.Prdate,t2.Sessions ";



            }
            else
            {
                str = "select * from ( SELECT *   FROM (SELECT t1.*,t2.*  FROM  (SELECT  Route_id,Route_name,Agent_id as agentid,Agent_name,Bank_id,Bank_name,Agent_accountNo,Ifsc_code,Milktype,Super_PhoneNo,SInsentAmt,Scaramt,SSplBonus,ClaimAount,SLoanAmount,Billadv,Ai,Feed,can,Recovery,others,SLoanClosingbalance,SAmt,TotAdditions,TotDeductions,Sinstamt,UPPER(Words) AS Words,Roundoff,Smkg,Smltr,Sfatkg,SSnfkg,Aclr,Scans ,NetAmount,Plant_code FROM paymentdata WHERE PLANT_CODE='" + pcode + "' AND Frm_date='" + d1 + "' AND To_date='" + d2 + "'  and agent_id='" + ViewState["pkagent"] + "' and agent_id='" + ViewState["pkagent"] + "' ) AS t1  INNER JOIN  (SELECT Agent_id as agentcode,CONVERT(VARCHAR,Prdate,103) AS Prdate,Sessions,ISNULL(NoofCans,0) AS NoofCans,ISNULL(Milk_kg,0) AS Milk_kg,ISNULL(Milk_ltr,0) AS Milk_ltr,ISNULL(Fat,0) AS Fat,ISNULL(fat_kg,0) AS fat_kg,ISNULL(Snf,0) AS Snf,ISNULL(snf_kg,0) AS snf_kg,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,ISNULL(ComRate,0) AS ComRate  from Procurement Where Plant_code='" + ViewState["pkkode"] + "' AND Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' and agent_id='" + ViewState["pkagent"] + "') AS t2 ON t1.agentid=t2.agentcode ) AS LLL LEFT JOIN (SELECT Agent_Id as agent,DpuAgentStatus    FROM Agent_Master WHERE Plant_code='" + ViewState["pkkode"] + "' )as rigg on LLL.agentid=rigg.agent )  as ccc   order by RAND(agentcode),(Route_id),(Prdate),(Sessions) ";
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
    protected void Button7_Click(object sender, EventArgs e)
    {

        Session["COUNT"] = "1";

        Session["DDLPLANT"] = Session["pnametxt"].ToString();
        Session["DDLAGE"] = Session["Agentidc"].ToString();
        Session["pkkode"] = Session["pkkode"].ToString();
        string getag = Session["DDLAGE"].ToString();
        Response.Redirect("AgentMilkDashboard.aspx");

       

       
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
    protected void Button8_Click(object sender, EventArgs e)
    {
        try
        {
            LoadBill();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

            //DateTime frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //string fdate = frmdate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
            //DateTime todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            //string tdate = todate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
         
            string CurrentCreateFolderName = ViewState["FDATE1"] + "_" + ViewState["FDATE2"] + "_" + DateTime.Now.ToString("ddMMyyyy");
            string path = @"C:\BILL VYSHNAVI\" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "Invoice.pdf";
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
            string MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "Invoice.pdf";
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
}