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
using System.IO;

public partial class Daily_Report : System.Web.UI.Page
{
    PdfReport Cpdf = new PdfReport();
    string name2 = string.Empty;
    string sess,ViewFilename;
    int periobj,butview;//period object
    string plant_Code = string.Empty;
    //string plant_Code = "1006cmpuser";
    string Company_code = "1";
    string route_id = "11";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                iframShowFiles.Visible = false;
                plant_Code = Session["User_ID"].ToString();
                rd_Am.Checked = true;
                Label1.Visible = false;
                txt_ToDtate.Visible = false;
                popupcal1.Visible = false;
                DateTime today = DateTime.Today; // As DateTime
                txt_FromDate.Text = today.ToString("MM-dd-yyyy");
                txt_ToDtate.Text = today.ToString("MM-dd-yyyy");
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
        }
        

    }
    private void BindGrid( int btnval)
    {
        //DateTime date = System.DateTime.Now;
        //string sess = date.ToString("tt");
        sess = string.Empty;
        string name1 = string.Empty;
        periobj = 0;
        
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        plant_Code = Session["User_ID"].ToString();

        if (rd_Am.Checked == true)
        {
            sess = "AM";
            dt = GetProcurementData(txt_FromDate.Text, sess, route_id, plant_Code, Company_code);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }
        else if (rd_Pm.Checked == true)
        {
            sess = "PM";
            dt = GetProcurementData(txt_FromDate.Text, sess, route_id, plant_Code, Company_code);

        }
        else if (rd_Period.Checked == true)
        {
            periobj = 1;
            sess = "Period";
            dt = GetProcurementDataPeriod(txt_FromDate.Text, txt_ToDtate.Text, route_id, plant_Code, Company_code);

        }
        else
        {
            sess = "Bill";
            dt = GetProcurementData(txt_ToDtate.Text, sess, route_id, plant_Code, Company_code);

        }
        ds.Tables.Add(dt);
        if (dt.Rows.Count > 0)
        {
            ViewFilename = string.Empty;
            if (periobj == 0)
            {
                name1 = Server.MapPath(".") + "/kk/" + plant_Code.Trim() + '_' + txt_FromDate.Text.Trim() + '_' + sess.Trim();
                ViewFilename = plant_Code.Trim() + '_' + txt_FromDate.Text.Trim() + '_' + sess.Trim();
            }
            else
            {
                name1 = Server.MapPath(".") + "/kk/" + plant_Code.Trim() + '_' + txt_FromDate.Text.Trim() + '_' + txt_ToDtate.Text.Trim() + '_' + sess.Trim();
                ViewFilename = plant_Code.Trim() + '_' + txt_FromDate.Text.Trim() + '_' + txt_ToDtate.Text.Trim() + '_' + sess.Trim();
            }

            name2 = Server.MapPath(".") + "/kk/" + "Nasa-logo1.gif";
            SETBO();
            PdfReport pd = new PdfReport(ds, name1, Cpdf);
            if (butview > 1)
            {
                pd.Execute1();
                string fileName = ViewFilename + ".pdf";
                string surverUrl = Request.Url.AbsoluteUri.Split('/')[0] + "//" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                string fileStoreLocation = "kk/";
                string downloadFilePath = surverUrl + fileStoreLocation + fileName;
                iframShowFiles.Attributes.Add("src", downloadFilePath);
            }
            else
            {
                pd.Execute();
            }
         
            name1 = string.Empty;
        }
        else
        {
            iframShowFiles.Visible = false;
            uscMsgBox1.AddMessage("Report Not Found", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    public void SETBO()
    {
        Cpdf.CompanyName = "ONEZERO SOLUTION";
        Cpdf.PlantName = "Mangarai-CC";
        Cpdf.Date1 = Convert.ToDateTime(txt_FromDate.Text);
        Cpdf.Date2 = Convert.ToDateTime(txt_ToDtate.Text);
        Cpdf.Routeid = dl_Routeid.SelectedItem.Text;
        Cpdf.Session = sess.Trim();
        Cpdf.Imagepath = name2;

        Cpdf.PeriodObject = periobj;

    }
    private static DataTable GetProcurementData(string d2, string ses, string rid, string pcode, string ccode)
    {
        SqlConnection con = null;
        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;//AMPSConnectionString//TEST
        con = new SqlConnection(connection);
        //string str = "SELECT Agent_Id,ISNULL(Cans,0) AS Cans,ISNULL(Mltr,0) AS Mltr,ISNULL(Mkg,0) AS Mkg,ISNULL(Fat,0) AS Fat,ISNULL(Snf,0) AS Snf,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,Prdate,Sessions FROM (SELECT Agent_Id AS Agent_Id ,(CAST((NoofCans) AS DECIMAL(18,2))) AS Cans,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Milk_kg) AS DECIMAL(18,2))) AS Mkg ,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2)))AS Snf,(CAST((Clr)AS DECIMAL(18,2)))AS Clr,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,Prdate,Sessions FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Sessions='" + ses + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t1";
        //string str = "SELECT t2.Agent_Id,Cans,Mltr,Mkg,Fat,Snf,Rate,Amount,t3.Agent_Name,Prdate,Sessions,Clr FROM (SELECT Agent_Id,ISNULL(Cans,0) AS Cans,ISNULL(Mltr,0) AS Mltr,ISNULL(Mkg,0) AS Mkg,ISNULL(Fat,0) AS Fat,ISNULL(Snf,0) AS Snf,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,Prdate,Sessions FROM (SELECT Agent_Id AS Agent_Id ,(CAST((NoofCans) AS DECIMAL(18,2))) AS Cans,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Milk_kg) AS DECIMAL(18,2))) AS Mkg ,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2)))AS Snf,(CAST((Clr)AS DECIMAL(18,2)))AS Clr,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,Prdate,Sessions FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Sessions='" + ses + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t1) t2 LEFT JOIN (SELECT * FROM Agent_Master) AS t3 ON t3.Agent_id=t2.Agent_Id ORDER BY t2.Agent_Id";
        //LOCAL TABLE FINAL//          string str = "SELECT t5.Agent_id,ISNULL(t5.Cans,0) AS Cans,ISNULL(t5.Milk_kg,0)AS Milk_kg,ISNULL(t5.Milk_ltr,0) AS Milk_ltr,ISNULL(t5.Fat,0) AS Fat,ISNULL(t5.Snf,0) AS Snf,ISNULL(t5.Rate,0) AS Rate,ISNULL(t5.Amount,0) AS Amount,t5.AgentName,CONVERT(varchar,t5.Prdate ,103) AS Prdate,t5.sessions,ISNULL(t5.Clr,0) AS Clr,ISNULL(t5.SMilk_kg,0) AS SMilk_kg,ISNULL(t5.SMilk_ltr,0) AS SMilk_ltr,ISNULL(t5.AFat,0) AS AFat,ISNULL(t5.ASnf,0) AS ASnf,ISNULL(t5.AClr,0) AS AClr,ISNULL(t5.SCans,0) AS SCans,ISNULL(t5.ARate,0) AS ARate,ISNULL(t5.SAmount,0) AS SAmount FROM (SELECT t3.Agent_id ,CAST(t3.NoofCans AS DECIMAL(18,2)) AS Cans ,CAST(t3.Milk_ltr AS DECIMAL(18,2)) AS Milk_ltr,CAST(t3.Milk_kg AS DECIMAL(18,2)) AS Milk_kg ,CAST(t3.Fat AS DECIMAL(18,2)) AS Fat,CAST(t3.Snf AS DECIMAL(18,2)) AS Snf,CAST(t3.Rate AS DECIMAL(18,2)) AS Rate,CAST(t3.Amount AS DECIMAL(18,2)) AS Amount,t3.AgentName,t3.Prdate,t3.sessions,CAST(t3.Clr AS DECIMAL(18,2)) AS Clr,CAST(SMilk_ltr AS DECIMAL(18,2)) AS SMilk_ltr,CAST(SMilk_kg AS DECIMAL(18,2)) AS SMilk_kg,CAST(AFat AS DECIMAL(18,2)) AS AFat,CAST(ASnf AS DECIMAL(18,2)) AS ASnf,CAST(AClr AS DECIMAL(18,2)) AS AClr,CAST(SCans AS DECIMAL(18,2)) AS SCans,CAST(ARate AS DECIMAL(18,2)) AS ARate,CAST(SAmount AS DECIMAL(18,2)) AS SAmount FROM (SELECT t1.Agent_id,t1.NoofCans,t1.Milk_ltr,t1.Milk_kg,t1.Fat,t1.Snf,t1.Rate,t1.Amount,t2.AgentName,t1.Prdate,t1.sessions,t1.Clr FROM (SELECT * FROM PROCUREMENT WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Sessions='" + ses + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t1 LEFT JOIN (SELECT Agent_Name AS AgentName,Agent_id AS Aid FROM Agent_Master) AS t2 ON t1.Agent_id=t2.Aid) AS t3 LEFT JOIN (SELECT SUM(Milk_ltr) AS SMilk_ltr,SUM(Milk_kg) AS SMilk_kg,AVG(Fat) AS AFat,AVG(Snf) AS ASnf,AVG(Clr) AS AClr,SUM(NoofCans) AS SCans,AVG(Rate) AS ARate,SUM(Amount) AS SAmount FROM PROCUREMENT WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Sessions='" + ses + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t4 ON t3.Agent_id=t3.Agent_id ) AS t5 LEFT JOIN (SELECT COUNT(*) AS Agents FROM PROCUREMENT WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Sessions='" + ses + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t6 ON t5.Agent_id=t5.Agent_id  ORDER BY t5.Agent_id ";
        //SERVER TABLE FINAL//     
        string str = "SELECT t5.Agent_id,ISNULL(t5.Cans,0) AS Cans,ISNULL(t5.Milk_kg,0)AS Milk_kg,ISNULL(t5.Milk_ltr,0) AS Milk_ltr,ISNULL(t5.Fat,0) AS Fat,ISNULL(t5.Snf,0) AS Snf,ISNULL(t5.Rate,0) AS Rate,ISNULL(t5.Amount,0) AS Amount,t5.AgentName,CONVERT(varchar,t5.Prdate ,103) AS Prdate,t5.sessions,ISNULL(t5.Clr,0) AS Clr,ISNULL(t5.SMilk_kg,0) AS SMilk_kg,ISNULL(t5.SMilk_ltr,0) AS SMilk_ltr,ISNULL(t5.AFat,0) AS AFat,ISNULL(t5.ASnf,0) AS ASnf,ISNULL(t5.AClr,0) AS AClr,ISNULL(t5.SCans,0) AS SCans,ISNULL(t5.ARate,0) AS ARate,ISNULL(t5.SAmount,0) AS SAmount FROM (SELECT t3.Agent_id ,CAST(t3.NoofCans AS DECIMAL(18,2)) AS Cans ,CAST(t3.Milk_ltr AS DECIMAL(18,2)) AS Milk_ltr,CAST(t3.Milk_kg AS DECIMAL(18,2)) AS Milk_kg ,CAST(t3.Fat AS DECIMAL(18,2)) AS Fat,CAST(t3.Snf AS DECIMAL(18,2)) AS Snf,CAST(t3.Rate AS DECIMAL(18,2)) AS Rate,CAST(t3.Amount AS DECIMAL(18,2)) AS Amount,t3.AgentName,t3.Prdate,t3.sessions,CAST(t3.Clr AS DECIMAL(18,2)) AS Clr,CAST(SMilk_ltr AS DECIMAL(18,2)) AS SMilk_ltr,CAST(SMilk_kg AS DECIMAL(18,2)) AS SMilk_kg,CAST(AFat AS DECIMAL(18,2)) AS AFat,CAST(ASnf AS DECIMAL(18,2)) AS ASnf,CAST(AClr AS DECIMAL(18,2)) AS AClr,CAST(SCans AS DECIMAL(18,2)) AS SCans,CAST(ARate AS DECIMAL(18,2)) AS ARate,CAST(SAmount AS DECIMAL(18,2)) AS SAmount FROM (SELECT t1.Agent_ID AS Agent_id,t1.No_Of_Cans AS NoofCans,t1.milk_Ltr AS Milk_ltr,t1.milk_Kg AS Milk_kg,t1.fat AS Fat,t1.snf AS Snf,t1.rate AS Rate,t1.amount AS Amount,t2.AgentName,t1.WDate AS Prdate,t1.Session AS sessions,t1.Clr FROM (SELECT * FROM WeightDemoTable WHERE WDate= '" + d2 + "' AND Session='" + ses + "' AND Centre_ID='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t1 LEFT JOIN (SELECT Agent_Name AS AgentName,Agent_id AS Aid FROM SampleAgentDemo) AS t2 ON t1.Agent_id=t2.Aid) AS t3  LEFT JOIN (SELECT SUM(Milk_ltr) AS SMilk_ltr,SUM(Milk_kg) AS SMilk_kg,AVG(Fat) AS AFat,AVG(Snf) AS ASnf,AVG(Clr) AS AClr,SUM(No_Of_Cans) AS SCans,AVG(Rate) AS ARate,SUM(Amount) AS SAmount FROM WeightDemoTable WHERE WDate= '" + d2 + "' AND Session='" + ses + "' AND Centre_ID='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t4 ON t3.Agent_id=t3.Agent_id ) AS t5 LEFT JOIN (SELECT COUNT(*) AS Agents FROM WeightDemoTable WHERE WDate ='" + d2 + "' AND Session='" + ses + "' AND Centre_ID='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t6 ON t5.Agent_id=t5.Agent_id  ORDER BY t5.Agent_id ";
        SqlCommand cmd = new SqlCommand();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        return dt;
    }
    private static DataTable GetProcurementDataPeriod(string d1, string d2, string rid, string pcode, string ccode)
    {
        
        SqlConnection con = null;
        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        con = new SqlConnection(connection);
        //string str = "SELECT Agent_Id,ISNULL(Cans,0) AS Cans,ISNULL(Mltr,0) AS Mltr,ISNULL(Mkg,0) AS Mkg,ISNULL(Fat,0) AS Fat,ISNULL(Snf,0) AS Snf,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,Prdate,Sessions FROM (SELECT Agent_Id AS Agent_Id ,(CAST((NoofCans) AS DECIMAL(18,2))) AS Cans,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Milk_kg) AS DECIMAL(18,2))) AS Mkg ,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2)))AS Snf,(CAST((Clr)AS DECIMAL(18,2)))AS Clr,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,Prdate,Sessions FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Sessions='" + ses + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t1";
        //string str = "SELECT t2.Agent_Id,Cans,Mltr,Mkg,Fat,Snf,Rate,Amount,t3.Agent_Name,Prdate,Sessions,Clr FROM (SELECT Agent_Id,ISNULL(Cans,0) AS Cans,ISNULL(Mltr,0) AS Mltr,ISNULL(Mkg,0) AS Mkg,ISNULL(Fat,0) AS Fat,ISNULL(Snf,0) AS Snf,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,Prdate,Sessions FROM (SELECT Agent_Id AS Agent_Id ,(CAST((NoofCans) AS DECIMAL(18,2))) AS Cans,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Milk_kg) AS DECIMAL(18,2))) AS Mkg ,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2)))AS Snf,(CAST((Clr)AS DECIMAL(18,2)))AS Clr,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,Prdate,Sessions FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Sessions='" + ses + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t1) t2 LEFT JOIN (SELECT * FROM Agent_Master) AS t3 ON t3.Agent_id=t2.Agent_Id ORDER BY t2.Agent_Id";
        //LOCAL TABLE FINAL//         string str = "SELECT t5.Agent_id,ISNULL(t5.Cans,0) AS Cans,ISNULL(t5.Milk_kg,0)AS Milk_kg,ISNULL(t5.Milk_ltr,0) AS Milk_ltr,ISNULL(t5.Fat,0) AS Fat,ISNULL(t5.Snf,0) AS Snf,ISNULL(t5.Rate,0) AS Rate,ISNULL(t5.Amount,0) AS Amount,t5.AgentName,CONVERT(varchar,t5.Prdate ,103) AS Prdate,t5.sessions,ISNULL(t5.Clr,0) AS Clr,ISNULL(t5.SMilk_kg,0) AS SMilk_kg,ISNULL(t5.SMilk_ltr,0) AS SMilk_ltr,ISNULL(t5.AFat,0) AS AFat,ISNULL(t5.ASnf,0) AS ASnf,ISNULL(t5.AClr,0) AS AClr,ISNULL(t5.SCans,0) AS SCans,ISNULL(t5.ARate,0) AS ARate,ISNULL(t5.SAmount,0) AS SAmount FROM (SELECT t3.Agent_id ,CAST(t3.NoofCans AS DECIMAL(18,2)) AS Cans ,CAST(t3.Milk_ltr AS DECIMAL(18,2)) AS Milk_ltr,CAST(t3.Milk_kg AS DECIMAL(18,2)) AS Milk_kg ,CAST(t3.Fat AS DECIMAL(18,2)) AS Fat,CAST(t3.Snf AS DECIMAL(18,2)) AS Snf,CAST(t3.Rate AS DECIMAL(18,2)) AS Rate,CAST(t3.Amount AS DECIMAL(18,2)) AS Amount,t3.AgentName,t3.Prdate,t3.sessions,CAST(t3.Clr AS DECIMAL(18,2)) AS Clr,CAST(SMilk_ltr AS DECIMAL(18,2)) AS SMilk_ltr,CAST(SMilk_kg AS DECIMAL(18,2)) AS SMilk_kg,CAST(AFat AS DECIMAL(18,2)) AS AFat,CAST(ASnf AS DECIMAL(18,2)) AS ASnf,CAST(AClr AS DECIMAL(18,2)) AS AClr,CAST(SCans AS DECIMAL(18,2)) AS SCans,CAST(ARate AS DECIMAL(18,2)) AS ARate,CAST(SAmount AS DECIMAL(18,2)) AS SAmount FROM (SELECT t1.Agent_id,t1.NoofCans,t1.Milk_ltr,t1.Milk_kg,t1.Fat,t1.Snf,t1.Rate,t1.Amount,t2.AgentName,t1.Prdate,t1.sessions,t1.Clr FROM (SELECT Agent_id,SUM(NoofCans) AS NoofCans,SUM(Milk_kg)AS Milk_kg,SUM(Milk_ltr) AS Milk_ltr,AVG(Fat) AS Fat,AVG(Snf) AS Snf,AVG(Rate) AS Rate,SUM(Amount) AS Amount,AVG(Clr) AS Clr,Agent_id AS Prdate,Agent_id AS sessions FROM PROCUREMENT WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "'  AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "' GROUP BY Agent_id) AS t1 LEFT JOIN (SELECT Agent_Name AS AgentName,Agent_id AS Aid FROM Agent_Master) AS t2 ON t1.Agent_id=t2.Aid) AS t3 LEFT JOIN (SELECT SUM(Milk_ltr) AS SMilk_ltr,SUM(Milk_kg) AS SMilk_kg,AVG(Fat) AS AFat,AVG(Snf) AS ASnf,AVG(Clr) AS AClr,SUM(NoofCans) AS SCans,AVG(Rate) AS ARate,SUM(Amount) AS SAmount FROM PROCUREMENT WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "'  AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t4 ON t3.Agent_id=t3.Agent_id ) AS t5 LEFT JOIN (SELECT COUNT(*) AS Agents FROM PROCUREMENT WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "'  AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "') AS t6 ON t5.Agent_id=t5.Agent_id  ORDER BY t5.Agent_id ";
        //SERVER TABLE FINAL//   
        string str = "SELECT t5.Agent_id,ISNULL(t5.Cans,0) AS Cans,ISNULL(t5.Milk_kg,0)AS Milk_kg,ISNULL(t5.Milk_ltr,0) AS Milk_ltr,ISNULL(t5.Fat,0) AS Fat,ISNULL(t5.Snf,0) AS Snf,ISNULL(t5.Rate,0) AS Rate,ISNULL(t5.Amount,0) AS Amount,t5.AgentName,CONVERT(varchar,t5.Prdate ,103) AS Prdate,t5.sessions,ISNULL(t5.Clr,0) AS Clr,ISNULL(t5.SMilk_kg,0) AS SMilk_kg,ISNULL(t5.SMilk_ltr,0) AS SMilk_ltr,ISNULL(t5.AFat,0) AS AFat,ISNULL(t5.ASnf,0) AS ASnf,ISNULL(t5.AClr,0) AS AClr,ISNULL(t5.SCans,0) AS SCans,ISNULL(t5.ARate,0) AS ARate,ISNULL(t5.SAmount,0) AS SAmount FROM (SELECT t3.Agent_id ,CAST(t3.NoofCans AS DECIMAL(18,2)) AS Cans ,CAST(t3.Milk_ltr AS DECIMAL(18,2)) AS Milk_ltr,CAST(t3.Milk_kg AS DECIMAL(18,2)) AS Milk_kg ,CAST(t3.Fat AS DECIMAL(18,2)) AS Fat,CAST(t3.Snf AS DECIMAL(18,2)) AS Snf,CAST(t3.Rate AS DECIMAL(18,2)) AS Rate,CAST(t3.Amount AS DECIMAL(18,2)) AS Amount,t3.AgentName,t3.Prdate,t3.sessions,CAST(t3.Clr AS DECIMAL(18,2)) AS Clr,CAST(SMilk_ltr AS DECIMAL(18,2)) AS SMilk_ltr,CAST(SMilk_kg AS DECIMAL(18,2)) AS SMilk_kg,CAST(AFat AS DECIMAL(18,2)) AS AFat,CAST(ASnf AS DECIMAL(18,2)) AS ASnf,CAST(AClr AS DECIMAL(18,2)) AS AClr,CAST(SCans AS DECIMAL(18,2)) AS SCans,CAST(ARate AS DECIMAL(18,2)) AS ARate,CAST(SAmount AS DECIMAL(18,2)) AS SAmount FROM (SELECT t1.Agent_ID AS Agent_id,t1.NoofCans,t1.milk_Ltr AS Milk_ltr,t1.milk_Kg AS Milk_kg,t1.fat AS Fat,t1.snf AS Snf,t1.rate AS Rate,t1.amount AS Amount,t2.AgentName,t1.Prdate,t1. sessions,t1.Clr FROM (SELECT Agent_id,SUM(No_Of_Cans) AS NoofCans,SUM(Milk_kg)AS Milk_kg,SUM(Milk_ltr) AS Milk_ltr,AVG(Fat) AS Fat,AVG(Snf) AS Snf,AVG(Rate) AS Rate,SUM(Amount) AS Amount,AVG(Clr) AS Clr,Agent_id AS Prdate,Agent_id AS sessions FROM WeightDemoTable WHERE WDate BETWEEN '" + d1 + "' AND '" + d2 + "'  AND Centre_ID='" + pcode + "'  AND Route_id='" + rid + "' AND  Company_Code='" + ccode + "' GROUP BY Agent_id) AS t1 LEFT JOIN (SELECT Agent_Name AS AgentName,Agent_id AS Aid FROM SampleAgentDemo) AS t2 ON t1.Agent_id=t2.Aid) AS t3  LEFT JOIN (SELECT SUM(Milk_ltr) AS SMilk_ltr,SUM(Milk_kg) AS SMilk_kg,AVG(Fat) AS AFat,AVG(Snf) AS ASnf,AVG(Clr) AS AClr,SUM(No_Of_Cans) AS SCans,AVG(Rate) AS ARate,SUM(Amount) AS SAmount FROM WeightDemoTable WHERE WDate BETWEEN '" + d1 + "' AND '" + d2 + "'  AND Centre_ID='" + pcode + "'  AND Route_id='" + rid + "' AND  Company_Code='" + ccode + "') AS t4 ON t3.Agent_id=t3.Agent_id ) AS t5 LEFT JOIN (SELECT COUNT(*) AS Agents FROM WeightDemoTable WHERE WDate BETWEEN '" + d1 + "' AND '" + d2 + "'  AND Centre_ID='" + pcode + "'  AND Route_id='" + rid + "' AND  Company_Code='" + ccode + "') AS t6 ON t5.Agent_id=t5.Agent_id  ORDER BY t5.Agent_id ";
        SqlCommand cmd = new SqlCommand();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        return dt;
    }

    protected void btn_Report_Click(object sender, System.EventArgs e)
    {
        iframShowFiles.Visible = false;
        butview = 1;
        BindGrid(butview);
    }

    protected void rd_Am_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_Am.Checked == true)
        {
            //lbl_FromDate.Visible = true;
            //txt_FromDate.Visible = true;

            Label1.Visible = false;
            txt_ToDtate.Visible = false;
            popupcal1.Visible = false;

            rd_Pm.Checked = false;
            rd_Period.Checked = false;
            rd_Bill.Checked = false;


        }

    }

    protected void rd_Pm_CheckedChanged1(object sender, EventArgs e)
    {
        if (rd_Pm.Checked == true)
        {
            //lbl_FromDate.Visible = true;
            //txt_FromDate.Visible = true;

            Label1.Visible = false;
            txt_ToDtate.Visible = false;
            popupcal1.Visible = false;
            rd_Am.Checked = false;
            rd_Period.Checked = false;
            rd_Bill.Checked = false;


        }

    }
    protected void rd_Period_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_Period.Checked == true)
        {
            //lbl_FromDate.Visible = true;
            //txt_FromDate.Visible = true;

            //lbl_ToDate.Visible = false;
            //txt_ToDtate.Visible = false;           

            rd_Am.Checked = false;
            rd_Pm.Checked = false;
            rd_Bill.Checked = false;
            Label1.Visible = true;
            txt_ToDtate.Visible = true;
            popupcal1.Visible = true;

        }
    }
    protected void rd_Bill_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_Bill.Checked == true)
        {
            //lbl_FromDate.Visible = true;
            //txt_FromDate.Visible = true;

            //lbl_ToDate.Visible = false;
            //txt_ToDtate.Visible = false; 

            rd_Period.Checked = false;
            rd_Am.Checked = false;
            rd_Pm.Checked = false;
        }

    }
    protected void btn_View_Click(object sender, EventArgs e)
    {
        iframShowFiles.Visible = true;
        butview=2;
        BindGrid(butview);

    }
}
