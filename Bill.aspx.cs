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
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text;


public partial class Bill : System.Web.UI.Page
{
    CreatePdf Cpdf = new CreatePdf();
    string name2 = string.Empty;
    string Company_code = "1";
    string plant_Code = string.Empty;
    string route_id = "11";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                iframShowFiles.Visible = false;
                plant_Code = Session["User_ID"].ToString();
                BindGrid();
                DateTime today = DateTime.Today; // As DateTime
                TextBox3.Text = today.ToString("MM-dd-yyyy");
                TextBox4.Text = today.ToString("MM-dd-yyyy");
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {

                plant_Code = Session["User_ID"].ToString();
            }
        }
      
    }

    private void BindGrid()
    {
        plant_Code = Session["User_ID"].ToString();
        DataTable dt = Getdata(TextBox3.Text, TextBox4.Text, route_id, plant_Code, Company_code);

    }
    private static DataTable Getdata(string d1, string d2, string rid, string pcode, string ccode)
    {


        SqlConnection con = null;
        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        con = new SqlConnection(connection);
        //string str = "SELECT Agent_Id,Route_id,Prdate,Sessions,Noof_cans,Milk_kg,Milk_ltr,Sample_no,Usr_weigher FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "'  ORDER BY Agent_Id,Sessions ";
        //string str = "SELECT Agent_Id,Route_id,Prdate,Sessions,Noof_cans,Milk_kg,Milk_ltr,Sample_no,Usr_weigher FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "'  ORDER BY Agent_Id,Sessions ";
        //OLD// string str = "SELECT Prdate,Sessions,Mltr,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(Rate,0)AS Rate,ISNULL(Amount,0)AS Amount,Agent_Id FROM (SELECT Agent_Id,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "'  ) AS t1  ORDER BY Agent_Id,Sessions ";
        //string str = "SELECT Agent_Id FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "'   ORDER BY Agent_Id";
        //string str = "SELECT Agent_Id FROM (SELECT Agent_Id FROM  Agent_Master WHERE Agent_type=0)AS t1 LEFT JOIN (SELECT Agent_Id AS Aid FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "') AS t2 ON t1.Agent_Id=t2.Aid  ORDER BY Agent_Id ";
        //string str = "SELECT Prdate,Sessions,Mltr,Fat,Snf,Rate,Amount,Aid AS Agent_Id,Ded.No0f_Agent AS No0f_Agent,Ded.Smltr AS Smltr,Ded.Feed AS Feed,Ded.Ai AS Ai  FROM (SELECT t6.Agent_Id AS Aid,Prdate,Sessions,Mltr,Fat,Snf,Rate,Amount FROM (SELECT Agent_Id FROM  Agent_Master WHERE Agent_type=0)AS t6 LEFT JOIN (SELECT Agent_Id AS Aid,Prdate,Sessions,ISNULL(Mltr,0)AS Mltr,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(Rate,0)AS Rate,ISNULL(Amount,0)AS Amount FROM (SELECT Agent_Id,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "'  ) AS t1 ) AS t7 ON t6.Agent_Id=t7.Aid  ) AS t8 LEFT JOIN (SELECT t4.agent_id,Smltr,No0f_Agent,ISNULL(Feed,0) AS Feed,ISNULL(Ai,0) AS Ai FROM (SELECT agent_id,ISNULL(Smltr,0) AS Smltr,No0f_Agent FROM (SELECT  agent_id,(CAST((Smltr) AS  DECIMAL(18,2))) AS Smltr FROM (SELECT agent_id,SUM(Milk_ltr) AS Smltr  FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id)AS t1  ) AS t2 LEFT JOIN (SELECT COUNT(Distinct(agent_id)) AS No0f_Agent FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' GROUP BY prdate ) AS t3 ON 1<2 ) AS t4 LEFT JOIN (SELECT  Agent_id ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' ) AS t5 ON t4.agent_id=t5.Agent_id ) AS Ded ON t8.Aid=Ded.Agent_id ORDER BY Agent_Id,Sessions ";
        //string str = "SELECT Prdate,Sessions,Mltr,Fat,Snf,Rate,Amount,Aid AS AAid,Smltr AS Smltr,ISNULL(t6.Feed,0) AS Feed,ISNULL(t6.Ai,0) AS Ai FROM (SELECT Prdate,Sessions,ISNULL(Mltr,0)AS Mltr,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(Rate,0)AS Rate,ISNULL(Amount,0)AS Amount,Aid FROM (SELECT Agent_Id AS Aid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' ) AS t1  INNER JOIN (SELECT Agent_Id FROM  Agent_Master WHERE Agent_type=0)AS t2 ON t1.Aid=t2.Agent_Id ) AS t3 LEFT JOIN (SELECT  Agent_id AS Aid1 ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' ) AS t4 ON t3.Aid=t4.Aid1 LEFT JOIN (SELECT agent_id,SUM(Milk_ltr) AS Smltr  FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' AND Plant_Code='111'  GROUP BY agent_id) AS t5 ON Aid=t5.agent_id LEFT JOIN (SELECT  Agent_id AS Aidd ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "') AS t6 ON Aid=t6.Aidd";
        //LOCAL TABLE FINAL//        string str = "SELECT Prdate AS Date,Sessions AS Sess,Mltr,Fat,Snf,Rate,Amount,AAid,ISNULL(Smltr,0) AS Smltr,ISNULL(AFat,0) AS AFat,ISNULL(Asnf,0) AS Asnf,ISNULL(Arate,0) AS Arate,ISNULL(SAmt,0) AS SAmt,ISNULL(Feed,0) AS Feed,ISNULL(Ai,0) AS Ai,CarAmt FROM (SELECT Prdate,Sessions,Mltr,Fat,Snf,Rate,Amount,Aid AS AAid,(CAST((Smltr) AS DECIMAL(18,2))) AS Smltr,(CAST((AvgFat) AS DECIMAL(18,2))) AS AFat,(CAST((AvgSnf) AS DECIMAL(18,2))) Asnf ,(CAST((AvgRate) AS DECIMAL(18,2))) AS Arate ,(CAST((SAmt) AS DECIMAL(18,2))) AS SAmt,(CAST((t6.Feed) AS DECIMAL(18,2))) AS Feed ,(CAST((t6.Ai) AS DECIMAL(18,2))) AS Ai,CarAmt FROM (SELECT Prdate,Sessions,ISNULL(Mltr,0)AS Mltr,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(Rate,0)AS Rate,ISNULL(Amount,0)AS Amount,Aid,ISNULL(CarAmt,0)AS CarAmt FROM (SELECT Agent_Id AS Aid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "' ) AS t1 INNER JOIN (SELECT Agent_Id,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Agent_type=0)AS t2 ON t1.Aid=t2.Agent_Id ) AS t3 LEFT JOIN (SELECT  Agent_id AS Aid1 ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "') AS t4 ON t3.Aid=t4.Aid1 LEFT JOIN  (SELECT agent_id,SUM(Milk_ltr) AS Smltr,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,SUM(Amount) AS SAmt  FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "'  GROUP BY agent_id) AS t5 ON Aid=t5.agent_id LEFT JOIN (SELECT  Agent_id AS Aidd ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' ) AS t6 ON Aid=t6.Aidd )AS t7 ";
        //SERVER TABLE FINAL// 
        string str = "SELECT Prdate AS Date,Sessions AS Sess,Mltr,Fat,Snf,Rate,Amount,AAid,ISNULL(Smltr,0) AS Smltr,ISNULL(AFat,0) AS AFat,ISNULL(Asnf,0) AS Asnf,ISNULL(Arate,0) AS Arate,ISNULL(SAmt,0) AS SAmt,ISNULL(Feed,0) AS Feed,ISNULL(Ai,0) AS Ai,CarAmt FROM (SELECT Prdate,Sessions,Mltr,Fat,Snf,Rate,Amount,Aid AS AAid,(CAST((Smltr) AS DECIMAL(18,2))) AS Smltr,(CAST((AvgFat) AS DECIMAL(18,2))) AS AFat,(CAST((AvgSnf) AS DECIMAL(18,2))) Asnf ,(CAST((AvgRate) AS DECIMAL(18,2))) AS Arate ,(CAST((SAmt) AS DECIMAL(18,2))) AS SAmt,(CAST((t6.Feed) AS DECIMAL(18,2))) AS Feed ,(CAST((t6.Ai) AS DECIMAL(18,2))) AS Ai,CarAmt FROM (SELECT Prdate,Sessions,ISNULL(Mltr,0)AS Mltr,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(Rate,0)AS Rate,ISNULL(Amount,0)AS Amount,Aid,ISNULL(CarAmt,0)AS CarAmt FROM (SELECT Agent_Id AS Aid ,WDate AS Prdate,Session AS Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount FROM WeightDemoTable WHERE WDate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Company_Code='" + ccode + "' AND Centre_ID='" + pcode + "') AS t1 INNER JOIN  (SELECT Agent_Id,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  SampleAgentDemo WHERE Agent_type='Bulk')AS t2 ON t1.Aid=t2.Agent_Id ) AS t3 LEFT JOIN (SELECT  Agent_id AS Aid1 ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "') AS t4 ON t3.Aid=t4.Aid1 LEFT JOIN (SELECT agent_id,SUM(Milk_ltr) AS Smltr,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,SUM(Amount) AS SAmt  FROM WeightDemoTable WHERE WDate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Company_Code='" + ccode + "'  AND Centre_ID='" + pcode + "' GROUP BY agent_id) AS t5 ON Aid=t5.agent_id LEFT JOIN (SELECT  Agent_id AS Aidd ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "' ) AS t6 ON Aid=t6.Aidd )AS t7 ORDER BY t7.AAid ";
        SqlCommand cmd = new SqlCommand();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        return dt;


    }
    private static DataTable Getdata1(string d1, string d2, string rid, string pcode, string ccode)
    {
        SqlConnection con = null;
        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        con = new SqlConnection(connection);
        // string str = "SELECT  agent_id,(CAST((weight) AS  DECIMAL(18,2))) AS weigt FROM(SELECT agent_id,SUM(weight) AS      weight  FROM PROCUREMENT GROUP BY agent_id)AS t1  ORDER BY agent_id";
        //string str = "SELECT agent_id,Smltr,No0f_Agent FROM (SELECT  agent_id,(CAST((Smltr) AS  DECIMAL(18,2))) AS Smltr FROM (SELECT agent_id,SUM(Milk_ltr) AS Smltr  FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' GROUP BY agent_id)AS t1  ) AS t2 LEFT JOIN (SELECT COUNT(Distinct(agent_id)) AS No0f_Agent FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' GROUP BY prdate ) AS t3 ON 1<2 ";
        //OLD//string str = "SELECT t4.agent_id,Smltr,No0f_Agent,ISNULL(Feed,0) AS Feed,ISNULL(Ai,0) AS Ai FROM (SELECT agent_id,ISNULL(Smltr,0) AS Smltr,No0f_Agent FROM (SELECT  agent_id,(CAST((Smltr) AS  DECIMAL(18,2))) AS Smltr FROM (SELECT agent_id,SUM(Milk_ltr) AS Smltr  FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id)AS t1  ) AS t2 LEFT JOIN (SELECT COUNT(Distinct(agent_id)) AS No0f_Agent FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' GROUP BY prdate ) AS t3 ON 1<2 ) AS t4 LEFT JOIN (SELECT  Agent_id ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' ) AS t5 ON t4.agent_id=t5.Agent_id ";
        //string str = "SELECT t6.Aid AS Agent_id,t6.Prdate AS Prdate,t6.Sessions AS Sessions,t6.Mltr AS Mltr,t6.Fat AS Fat,t6.Snf AS Snf,t6.Rate AS Rate,t6.Amount AS Amount,Ded.Smltr AS Smltr,Ded.No0f_Agent AS No0f_Agent,Ded.Feed AS Feed,Ded.Ai AS Ai  FROM (SELECT Agent_Id AS Aid,Prdate,Sessions,Mltr,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(Rate,0)AS Rate,ISNULL(Amount,0)AS Amount FROM (SELECT Agent_Id,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount FROM Procurement WHERE PRDATE BETWEEN '07-21-2012' AND '07-22-2012' AND Plant_Code='111'  AND Route_id='11'  ) AS t1   )AS t6 LEFT JOIN (SELECT t4.agent_id,Smltr,No0f_Agent,ISNULL(Feed,0) AS Feed,ISNULL(Ai,0) AS Ai FROM (SELECT agent_id,ISNULL(Smltr,0) AS Smltr,No0f_Agent FROM (SELECT  agent_id,(CAST((Smltr) AS  DECIMAL(18,2))) AS Smltr FROM (SELECT agent_id,SUM(Milk_ltr) AS Smltr  FROM Procurement WHERE prdate BETWEEN '07-21-2012' AND '07-22-2012' AND Plant_Code='111'  AND Route_id='11'  GROUP BY agent_id)AS t1  ) AS t2 LEFT JOIN (SELECT COUNT(Distinct(agent_id)) AS No0f_Agent FROM Procurement WHERE prdate BETWEEN '07-21-2012' AND '07-22-2012' AND Plant_Code='111'  AND Route_id='11' GROUP BY prdate ) AS t3 ON 1<2 ) AS t4 LEFT JOIN (SELECT  Agent_id ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '07-21-2012' AND '07-22-2012' AND Plant_Code='111'  AND Route_id='11' ) AS t5 ON t4.agent_id=t5.Agent_id ) AS Ded ON t6.Aid=Ded.Agent_id ORDER BY Agent_Id,Sessions ";
        //string str = "SELECT Prdate,Sessions,Mltr,Fat,Snf,Rate,Amount,Aid AS Agent_Id,Ded.Smltr AS Smltr,Ded.No0f_Agent AS No0f_Agent,Ded.Feed AS Feed,Ded.Ai AS Ai  FROM (SELECT t6.Agent_Id AS Aid,Prdate,Sessions,Mltr,Fat,Snf,Rate,Amount FROM (SELECT Agent_Id FROM  Agent_Master WHERE Agent_type=0)AS t6 LEFT JOIN (SELECT Agent_Id AS Aid,Prdate,Sessions,ISNULL(Mltr,0)AS Mltr,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(Rate,0)AS Rate,ISNULL(Amount,0)AS Amount FROM (SELECT Agent_Id,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "'  ) AS t1 ) AS t7 ON t6.Agent_Id=t7.Aid  ) AS t8 LEFT JOIN (SELECT t4.agent_id,Smltr,No0f_Agent,ISNULL(Feed,0) AS Feed,ISNULL(Ai,0) AS Ai FROM (SELECT agent_id,ISNULL(Smltr,0) AS Smltr,No0f_Agent FROM (SELECT  agent_id,(CAST((Smltr) AS  DECIMAL(18,2))) AS Smltr FROM (SELECT agent_id,SUM(Milk_ltr) AS Smltr  FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id)AS t1  ) AS t2 LEFT JOIN (SELECT COUNT(Distinct(agent_id)) AS No0f_Agent FROM Procurement WHERE prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' GROUP BY prdate ) AS t3 ON 1<2 ) AS t4 LEFT JOIN (SELECT  Agent_id ,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((Ai) AS DECIMAL(18,2))) AS Ai FROM DEDUCTION WHERE Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' ) AS t5 ON t4.agent_id=t5.Agent_id ) AS Ded ON t8.Aid=Ded.Agent_id ORDER BY Agent_Id,Sessions ";
        //LOCAL TABLE FINAL//         string str = "SELECT t5.Agent_Id AS Agent_Id,t7.No_ofAgent  FROM (SELECT Agent_Id AS TAid FROM  Agent_Master WHERE Agent_type=0)AS t4  INNER JOIN (SELECT t2.Agent_Id AS Agent_Id  FROM (SELECT DISTINCT(Agent_Id) AS Agent_Id FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "') AS t1 LEFT JOIN (SELECT Agent_Id FROM  Agent_Master WHERE Agent_type=0)AS t2 ON t1.Agent_Id=t2.Agent_Id)AS t5 ON t4.TAid=t5.Agent_Id INNER JOIN (SELECT COUNT(*) AS No_ofAgent  FROM (SELECT t5.Agent_Id AS Agent_Id , t5.Agent_Id AS NOF FROM (SELECT Agent_Id AS TAid FROM  Agent_Master WHERE Agent_type=0)AS t4  INNER JOIN (SELECT t2.Agent_Id AS Agent_Id  FROM (SELECT DISTINCT(Agent_Id) AS Agent_Id FROM Procurement WHERE PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "') AS t1  INNER JOIN (SELECT Agent_Id FROM  Agent_Master WHERE Agent_type=0)AS t2 ON t1.Agent_Id=t2.Agent_Id)AS t5 ON t4.TAid=t5.Agent_Id ) AS t6) AS t7 ON 1<2";
        //SERVER TABLE FINAL//     
        string str = "SELECT t5.Agent_Id AS Agent_Id,t7.No_ofAgent  FROM (SELECT Agent_Id AS TAid FROM  SampleAgentDemo WHERE Agent_type='Bulk')AS t4 INNER JOIN (SELECT t2.Agent_Id AS Agent_Id  FROM (SELECT DISTINCT(Agent_Id) AS Agent_Id FROM WeightDemoTable WHERE WDate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Centre_ID='" + pcode + "' AND Company_Code='" + ccode + "') AS t1 LEFT JOIN (SELECT Agent_Id FROM  SampleAgentDemo WHERE Agent_type='Bulk')AS t2 ON t1.Agent_Id=t2.Agent_Id)AS t5 ON t4.TAid=t5.Agent_Id INNER JOIN (SELECT COUNT(*) AS No_ofAgent  FROM (SELECT t5.Agent_Id AS Agent_Id , t5.Agent_Id AS NOF FROM (SELECT Agent_Id AS TAid FROM  SampleAgentDemo WHERE Agent_type='Bulk')AS t4 INNER JOIN (SELECT t2.Agent_Id AS Agent_Id  FROM (SELECT DISTINCT(Agent_Id) AS Agent_Id FROM WeightDemoTable WHERE WDate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Route_id='" + rid + "' AND Centre_ID='" + pcode + "' AND Company_Code='" + ccode + "') AS t1 INNER JOIN (SELECT Agent_Id FROM  SampleAgentDemo WHERE Agent_type='Bulk')AS t2 ON t1.Agent_Id=t2.Agent_Id)AS t5 ON t4.TAid=t5.Agent_Id ) AS t6) AS t7 ON 1<2 ORDER BY t5.Agent_Id ";
        SqlCommand cmd = new SqlCommand();
        con.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(str, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        con.Close();
        return dt1;

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        iframShowFiles.Visible = false;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        plant_Code = Session["User_ID"].ToString();
        dt = Getdata(TextBox3.Text, TextBox4.Text, route_id, plant_Code, Company_code);
        ds.Tables.Add(dt);
        if (dt.Rows.Count > 0)
        {
            ///
            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            dt1 = Getdata1(TextBox3.Text, TextBox4.Text, route_id, plant_Code, Company_code);
            ds1.Tables.Add(dt1);

            ///

            // int i = 1;
            //  for (i = 1; i <= dt1.Rows.Count; i++)
            //{

            int i = 1;
            string na = Convert.ToString(i);
            string name1 = Server.MapPath(".") + "/kk/" + plant_Code.Trim() + "From" + TextBox3.Text.Trim() + "to" + TextBox4.Text.Trim() + '_' + "Bill";
            name2 = Server.MapPath(".") + "/kk/" + "Nasa-logo1.gif";
            //name2 = Server.MapPath(".") + "/kk/" + "onezero .jpg";
            SETBO();
            CreatePdf pd = new CreatePdf(ds, name1, i, ds1, Cpdf);
            pd.Execute();
            name1 = string.Empty;
        }
        else
        {
            
            uscMsgBox1.AddMessage("Report Not Found", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    public void SETBO()
    {
        string route_id1 = "11-Pondicherry";

        Cpdf.CompanyName = "ONEZERO SOLUTION";
        Cpdf.PlantName = "Pondicherry-CC";
        Cpdf.Date1 = Convert.ToDateTime(TextBox3.Text.Trim());
        Cpdf.Date2 = Convert.ToDateTime(TextBox4.Text.Trim());
        Cpdf.Imagepath = name2;
        Cpdf.Routeid = route_id1.ToString();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        iframShowFiles.Visible = true;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        dt = Getdata(TextBox3.Text, TextBox4.Text, route_id, plant_Code, Company_code);
        ds.Tables.Add(dt);
        if (dt.Rows.Count > 0)
        {
            ///
            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            dt1 = Getdata1(TextBox3.Text, TextBox4.Text, route_id, plant_Code, Company_code);
            ds1.Tables.Add(dt1);

            ///

            // int i = 1;
            //  for (i = 1; i <= dt1.Rows.Count; i++)
            //{
            int i = 1;
            string na = Convert.ToString(i);
            string name1 = Server.MapPath(".") + "/kk/" + plant_Code.Trim() + '_' + TextBox3.Text.Trim() + '_' + TextBox4.Text.Trim() + '_' + "Bill";
            name2 = Server.MapPath(".") + "/kk/" + "Nasa-logo1.gif";
            //name2 = Server.MapPath(".") + "/kk/" + "onezero .jpg";
            SETBO();
            CreatePdf pd = new CreatePdf(ds, name1, i, ds1, Cpdf);

            pd.Execute1();

            string fileName = plant_Code.Trim() + '_' + TextBox3.Text.Trim() + '_' + TextBox4.Text.Trim() + '_' + "Bill.pdf";
            string surverUrl = Request.Url.AbsoluteUri.Split('/')[0] + "//" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string fileStoreLocation = "kk/";
            string downloadFilePath = surverUrl + fileStoreLocation + fileName;
            iframShowFiles.Attributes.Add("src", downloadFilePath);
            //object TargetFile = name1 + ".pdf".ToString();
            //System.IO.FileInfo file = new System.IO.FileInfo(TargetFile.ToString());

            //if (File.Exists(TargetFile.ToString()))
            //{
            //    File.Delete(TargetFile.ToString());
            //}
            name1 = string.Empty;
        }
        else
        {
            iframShowFiles.Visible = false;
            WebMsgBox.Show("Report Not Found...");
        }


    }
}
