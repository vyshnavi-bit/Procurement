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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.IO;


public partial class LoanRecoveryReport : System.Web.UI.Page
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
    public static int roleid;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pcode = Session["Plant_Code"].ToString();
                pname = Session["pname"].ToString();

         //       managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");


                if (roleid < 3)
                {
                    loadsingleplant();
                    pcode = Session["Plant_Code"].ToString();
                    pname = Session["pname"].ToString();
                   
                }
                else
                {
                    LoadPlantcode();
                    pcode = ddl_Plantcode.SelectedItem.Value;
                    pname = ddl_Plantname.SelectedItem.Value;
                }


               


               
                loadrouteid();
                rid = ddl_RouteID.SelectedItem.Value;
                loadagentid();
                LoanrecoveryReport();
                if (chk_Allloan.Checked == true)
                {
                    lbl_RouteName.Visible = false;
                    ddl_RouteName.Visible = false;
                    lbl_AgentName.Visible = false;
                    ddl_AgentName.Visible = false;
                }

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
                cname = Session["cname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                pcode = ddl_Plantcode.SelectedItem.Value;
                pname = ddl_Plantname.SelectedItem.Value;

                // loadrouteid();
                rid = ddl_RouteID.SelectedItem.Value;
                if (btnvalloanupdate == 1)
                {
                    LoanrecoveryReport();

                }
                else if (btnvalloanupdate == 2)
                {
                    LoanrecoveryReport();
                }
                else
                {
                    LoanrecoveryReport();
                }

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
    }
   
    private void LoanrecoveryReport()
    {
        try
        {

          //  CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
            if (chk_Allloan.Checked == true)
            {
                cr.Load(Server.MapPath("Crpt_LoanDeductionledger.rpt"));
            }
            else 
            {
                cr.Load(Server.MapPath("Crt_LoanrecoveryReport.rpt"));
            }

            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            DateTime dt3 = new DateTime();
            DateTime dt4 = new DateTime();

            dt3 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt4 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);


            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            t1.Text = ccode + "_" + cname;
            t2.Text =  pname;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To  " + txt_ToDate.Text.Trim();

            string d1 = dt3.ToString("MM/dd/yyyy");
            string d2 = dt4.ToString("MM/dd/yyyy");

            string str = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            if (chk_Allloan.Checked == true)
            {
                if (chk_CurrentLoan.Checked == true)
                {
                   // str = "SELECT LAid,RecoveryAmount,ClosingBalance,RecpRecoveryAmount,t6.Agent_Name AS Agent_Name FROM (SELECT LDRAid AS LAid,ISNULL(RecpRecoveryAmount,0) AS RecpRecoveryAmount,ISNULL(ClosingBalance1,0) ClosingBalance,ISNULL(t4.RecoveryAmount,0) AS RecoveryAmount FROM (SELECT LDRAid,t2.RecoveryAmount AS RecpRecoveryAmount ,t1.ClosingBalance AS ClosingBalance1  FROM (SELECT agent_Id AS LAid,SUM(CAST(Balance AS DECIMAL(18,2))) AS ClosingBalance FROM LoanDetails WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "'  GROUP BY agent_Id) AS t1  INNER JOIN (SELECT Agent_id AS LDRAid,SUM(CAST(LoanDueRecovery_Amount AS DECIMAL(18,2))) as RecoveryAmount FROM LoanDue_Recovery WHERE LoanRecovery_Date BETWEEN  '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' GROUP BY Agent_Id) AS t2 ON t1.LAid=t2.LDRAid) AS t3  LEFT JOIN  (SELECT Agent_id AS LRAid, SUM(CAST(Paid_Amount AS DECIMAL(18,2))) AS RecoveryAmount FROM Loan_Recovery WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' AND Paid_date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS t4 ON t3.LDRAid=t4.LRAid) AS t5  LEFT JOIN (SELECT Agent_Name,Agent_Id FROM Agent_Master WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' ) AS t6 ON  t5.LAid=t6.Agent_Id  ";                  
                      str="SELECT LDinfo AS LAid,RecoveryAmount,ClosingBalance,RecpRecoveryAmount,t6.Agent_Name AS Agent_Name FROM (SELECT LDLid,LDRAid AS LAid,LDinfo,ISNULL(RecpRecoveryAmount,0) AS RecpRecoveryAmount,ISNULL(ClosingBalance1,0) ClosingBalance,ISNULL(t4.RecoveryAmount,0) AS RecoveryAmount FROM (SELECT LDLid,LDRAid,LDinfo,t2.RecoveryAmount AS RecpRecoveryAmount ,t1.ClosingBalance AS ClosingBalance1  FROM (SELECT loan_Id AS LLid,agent_Id AS LAid,CAST(Balance AS DECIMAL(18,2)) AS ClosingBalance FROM LoanDetails WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "') AS t1  INNER JOIN (SELECT loan_Id AS LDLid,(CONVERT(VARCHAR(11),Agent_id)+'_' + CONVERT(VARCHAR(11),LoanRecovery_Date,106) +'_RefNo:_'+ CONVERT(VARCHAR(11),LoanDueRef_Id) +'_'+ Remarks) AS LDinfo,Agent_Id AS LDRAid,CAST(LoanDueRecovery_Amount AS DECIMAL(18,2)) as RecoveryAmount FROM LoanDue_Recovery WHERE LoanRecovery_Date BETWEEN  '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' ) AS t2 ON t1.LLid=t2.LDLid) AS t3  LEFT JOIN  (SELECT Loan_id AS LRLid,Agent_id AS LRAid, CAST(Paid_Amount AS DECIMAL(18,2)) AS RecoveryAmount FROM Loan_Recovery WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' AND Paid_date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' ) AS t4 ON t3.LDLid=t4.LRLid) AS t5   LEFT JOIN (SELECT Agent_Name,Agent_Id FROM Agent_Master WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' ) AS t6 ON  t5.LAid=t6.Agent_Id ";
                }
                else
                {
                    //str = "SELECT Ccode,Company_Name,Pcode,plant_name,plant_phoneno,Route_id, Aid,Agent_Name,loanid,ISNULL(paid_Amount,0) AS paid_Amount,PaidDate,ISNULL(Balance,0) AS Balance,ISNULL(Openningbalance,0)AS Openningbalance,ISNULL(Closingbalance,0)AS Closingbalance  FROM (SELECT Ccode,Pcode,Company_Name,Route_id, Aid,Agent_Name,loanid,CAST(paid_Amount AS DECIMAL(18,2)) AS paid_Amount,PaidDate,CAST(Balance AS DECIMAL(18,2)) AS Balance,CAST(Openningbalance AS DECIMAL(18,2)) AS Openningbalance,CAST(Closingbalance AS DECIMAL(18,2)) AS Closingbalance FROM (SELECT LRcccode AS Ccode,LRpcode AS Pcode,Route_id,LRAid AS Aid,Agent_Name,LRloanid AS loanid,paid_Amount,PaidDate,Balance,Openningbalance,Closingbalance FROM (SELECT * FROM (SELECT Company_code AS LRcccode,plant_code AS LRpcode,Route_id,loan_id AS LRloanid,Agent_id AS LRAid,Paid_Amount,CONVERT(VARCHAR(11),Paid_date,106) AS PaidDate,Openningbalance,Closingbalance FROM LOAN_RECOVERY WHERE paid_date BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "') AS LR LEFT JOIN (SELECT Balance,loan_id,Agent_id,Company_Code AS Lccode,Plant_Code AS Lpcode FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS LD ON LR.LRloanid=LD.loan_id)AS t1 LEFT JOIN (SELECT Agent_id,Agent_Name,Company_Code AS Accode,Plant_Code AS Apcode FROM Agent_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS AM ON t1.LRAid=AM.Agent_id )AS t1 LEFT JOIN (SELECT Company_Code AS cccode ,Company_Name FROM Company_Master WHERE Company_Code='" + ccode + "')AS com ON t1.Ccode=com.cccode )AS t2 LEFT JOIN (SELECT company_code AS pccode,plant_code,plant_name,plant_address,plant_phoneno FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS plant ON t2.Pcode=plant.plant_code ORDER BY loanid,Openningbalance desc";
                   // str = "SELECT LAid,RecoveryAmount,ClosingBalance,RecpRecoveryAmount,t6.Agent_Name AS Agent_Name FROM (SELECT LAid AS LAid,ISNULL(RecoveryAmount1,0) AS RecoveryAmount,ISNULL(ClosingBalance1,0) ClosingBalance,ISNULL(t4.RecoveryAmount,0) AS RecpRecoveryAmount FROM (SELECT  LAid,t2.RecoveryAmount AS RecoveryAmount1 ,t1.ClosingBalance AS ClosingBalance1  FROM (SELECT agent_Id AS LAid,SUM(CAST(Balance AS DECIMAL(18,2))) AS ClosingBalance FROM LoanDetails WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "'  GROUP BY agent_Id) AS t1 LEFT JOIN (SELECT Agent_id AS LRAid, SUM(CAST(Paid_Amount AS DECIMAL(18,2))) AS RecoveryAmount FROM Loan_Recovery WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' AND Paid_date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS t2 ON t1.LAid=t2.LRAid) AS t3 LEFT JOIN  (SELECT Agent_id AS LDRAid,SUM(CAST(LoanDueRecovery_Amount AS DECIMAL(18,2))) as RecoveryAmount FROM LoanDue_Recovery WHERE LoanRecovery_Date BETWEEN  '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "'  GROUP BY Agent_Id) AS t4 ON t3.LAid=t4.LDRAid) AS t5 LEFT JOIN (SELECT Agent_Name,Agent_Id FROM Agent_Master WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' ) AS t6 ON  t5.LAid=t6.Agent_Id"; 
                    str = "SELECT LAid,RecoveryAmount,ClosingBalance,RecpRecoveryAmount,t6.Agent_Name AS Agent_Name FROM (SELECT LAid AS LAid,ISNULL(RecoveryAmount1,0) AS RecoveryAmount,ISNULL(ClosingBalance1,0) ClosingBalance,ISNULL(t4.RecoveryAmount,0) AS RecpRecoveryAmount FROM (SELECT LRAid AS LAid,t2.RecoveryAmount AS RecoveryAmount1 ,t1.ClosingBalance AS ClosingBalance1  FROM (SELECT agent_Id AS LAid,SUM(CAST(Balance AS DECIMAL(18,2))) AS ClosingBalance FROM LoanDetails WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "'  GROUP BY agent_Id) AS t1 INNER JOIN (SELECT Agent_id AS LRAid, SUM(CAST(Paid_Amount AS DECIMAL(18,2))) AS RecoveryAmount FROM Loan_Recovery WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' AND Paid_date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY Agent_id) AS t2 ON t1.LAid=t2.LRAid) AS t3 LEFT JOIN  (SELECT Agent_id AS LDRAid,SUM(CAST(LoanDueRecovery_Amount AS DECIMAL(18,2))) as RecoveryAmount FROM LoanDue_Recovery WHERE LoanRecovery_Date BETWEEN  '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' GROUP BY Agent_Id) AS t4 ON t3.LAid=t4.LDRAid) AS t5 LEFT JOIN (SELECT Agent_Name,Agent_Id FROM Agent_Master WHERE Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' ) AS t6 ON  t5.LAid=t6.Agent_Id";
                }
            }
            else
            {
                // str = "SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '08-17-2012' AND '01-18-2013' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '08-17-2012' AND '01-18-2013' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ";
                str = "SELECT Ccode,Company_Name,Pcode,plant_name,plant_phoneno,Route_id, Aid,Agent_Name,loanid,ISNULL(paid_Amount,0) AS paid_Amount,PaidDate,ISNULL(Balance,0) AS Balance,ISNULL(Openningbalance,0)AS Openningbalance,ISNULL(Closingbalance,0)AS Closingbalance  FROM (SELECT Ccode,Pcode,Company_Name,Route_id, Aid,Agent_Name,loanid,CAST(paid_Amount AS DECIMAL(18,2)) AS paid_Amount,PaidDate,CAST(Balance AS DECIMAL(18,2)) AS Balance,CAST(Openningbalance AS DECIMAL(18,2)) AS Openningbalance,CAST(Closingbalance AS DECIMAL(18,2)) AS Closingbalance FROM (SELECT LRcccode AS Ccode,LRpcode AS Pcode,Route_id,LRAid AS Aid,Agent_Name,LRloanid AS loanid,paid_Amount,PaidDate,Balance,Openningbalance,Closingbalance FROM (SELECT * FROM (SELECT Company_code AS LRcccode,plant_code AS LRpcode,Route_id,loan_id AS LRloanid,Agent_id AS LRAid,Paid_Amount,CONVERT(VARCHAR(11),Paid_date,106) AS PaidDate,Openningbalance,Closingbalance FROM LOAN_RECOVERY WHERE paid_date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' AND Route_id='" + rid + "' AND Agent_id='" + ddl_AgentID.Text + "'  ) AS LR LEFT JOIN (SELECT Balance,loan_id,Agent_id,Company_Code AS Lccode,Plant_Code AS Lpcode FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS LD ON LR.LRloanid=LD.loan_id)AS t1 LEFT JOIN (SELECT Agent_id,Agent_Name,Company_Code AS Accode,Plant_Code AS Apcode FROM Agent_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS AM ON t1.LRAid=AM.Agent_id )AS t1 LEFT JOIN (SELECT Company_Code AS cccode ,Company_Name FROM Company_Master WHERE Company_Code='" + ccode + "')AS com ON t1.Ccode=com.cccode )AS t2 LEFT JOIN (SELECT company_code AS pccode,plant_code,plant_name,plant_address,plant_phoneno FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS plant ON t2.Pcode=plant.plant_code ORDER BY loanid,Openningbalance desc";
            }
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //
            if (chk_Allloan.Checked == false)
            {

                if (chk_Allloan.Checked == true)
                {
                    str1 = "SELECT Ccode,Company_Name,Pcode,plant_name,plant_phoneno,Route_id, Aid,Agent_Name,loanid,ISNULL(paid_Amount,0) AS paid_Amount,PaidDate,ISNULL(Balance,0) AS Balance,ISNULL(Openningbalance,0)AS Openningbalance,ISNULL(Closingbalance,0)AS Closingbalance  FROM (SELECT Ccode,Pcode,Company_Name,Route_id, Aid,Agent_Name,loanid,CAST(paid_Amount AS DECIMAL(18,2)) AS paid_Amount,PaidDate,CAST(Balance AS DECIMAL(18,2)) AS Balance,CAST(Openningbalance AS DECIMAL(18,2)) AS Openningbalance,CAST(Closingbalance AS DECIMAL(18,2)) AS Closingbalance FROM (SELECT LRcccode AS Ccode,LRpcode AS Pcode,Route_id,LRAid AS Aid,Agent_Name,LRloanid AS loanid,paid_Amount,PaidDate,Balance,Openningbalance,Closingbalance FROM (SELECT * FROM (SELECT Company_code AS LRcccode,plant_code AS LRpcode,Route_id,loan_id AS LRloanid,Agent_id AS LRAid,LoanDueRecovery_Amount as Paid_Amount,(CONVERT(VARCHAR(11),LoanRecovery_Date,106) +'_RefNo:_'+ CONVERT(VARCHAR(11),LoanDueRef_Id) +'_'+ Remarks) AS PaidDate,LoanDue_Balance AS Openningbalance,LoanBalance AS Closingbalance FROM LoanDue_Recovery WHERE LoanRecovery_Date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "') AS LR LEFT JOIN (SELECT Balance,loan_id,Agent_id,Company_Code AS Lccode,Plant_Code AS Lpcode FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS LD ON LR.LRloanid=LD.loan_id)AS t1 LEFT JOIN (SELECT Agent_id,Agent_Name,Company_Code AS Accode,Plant_Code AS Apcode FROM Agent_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS AM ON t1.LRAid=AM.Agent_id )AS t1 LEFT JOIN (SELECT Company_Code AS cccode ,Company_Name FROM Company_Master WHERE Company_Code='" + ccode + "')AS com ON t1.Ccode=com.cccode )AS t2 LEFT JOIN (SELECT company_code AS pccode,plant_code,plant_name,plant_address,plant_phoneno FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS plant ON t2.Pcode=plant.plant_code ORDER BY loanid,Openningbalance desc";
                    
                }
                else
                {
                    // str = "SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '08-17-2012' AND '01-18-2013' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '08-17-2012' AND '01-18-2013' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ";
                    str1 = "SELECT Ccode,Company_Name,Pcode,plant_name,plant_phoneno,Route_id, Aid,Agent_Name,loanid,ISNULL(paid_Amount,0) AS paid_Amount,PaidDate,ISNULL(Balance,0) AS Balance,ISNULL(Openningbalance,0)AS Openningbalance,ISNULL(Closingbalance,0)AS Closingbalance  FROM (SELECT Ccode,Pcode,Company_Name,Route_id, Aid,Agent_Name,loanid,CAST(paid_Amount AS DECIMAL(18,2)) AS paid_Amount,PaidDate,CAST(Balance AS DECIMAL(18,2)) AS Balance,CAST(Openningbalance AS DECIMAL(18,2)) AS Openningbalance,CAST(Closingbalance AS DECIMAL(18,2)) AS Closingbalance FROM (SELECT LRcccode AS Ccode,LRpcode AS Pcode,Route_id,LRAid AS Aid,Agent_Name,LRloanid AS loanid,paid_Amount,PaidDate,Balance,Openningbalance,Closingbalance FROM (SELECT * FROM (SELECT Company_code AS LRcccode,plant_code AS LRpcode,Route_id,loan_id AS LRloanid,Agent_id AS LRAid,LoanDueRecovery_Amount as Paid_Amount,(CONVERT(VARCHAR(11),LoanRecovery_Date,106) +'_RefNo:_'+ CONVERT(VARCHAR(11),LoanDueRef_Id) +'_'+ Remarks) AS PaidDate,LoanDue_Balance AS Openningbalance,LoanBalance AS Closingbalance FROM LoanDue_Recovery WHERE LoanRecovery_Date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' AND Route_id='" + rid + "' AND Agent_id='" + ddl_AgentID.Text + "'  ) AS LR LEFT JOIN (SELECT Balance,loan_id,Agent_id,Company_Code AS Lccode,Plant_Code AS Lpcode FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS LD ON LR.LRloanid=LD.loan_id)AS t1 LEFT JOIN (SELECT Agent_id,Agent_Name,Company_Code AS Accode,Plant_Code AS Apcode FROM Agent_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS AM ON t1.LRAid=AM.Agent_id )AS t1 LEFT JOIN (SELECT Company_Code AS cccode ,Company_Name FROM Company_Master WHERE Company_Code='" + ccode + "')AS com ON t1.Ccode=com.cccode )AS t2 LEFT JOIN (SELECT company_code AS pccode,plant_code,plant_name,plant_address,plant_phoneno FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS plant ON t2.Pcode=plant.plant_code ORDER BY loanid,Openningbalance desc";
                }

                SqlCommand cmd1 = new SqlCommand();
                SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DataRow drTemp = dt.NewRow();
                    drTemp[0] = dt1.Rows[i][0];
                    drTemp[1] = dt1.Rows[i][1];
                    drTemp[2] = dt1.Rows[i][2];
                    drTemp[3] = dt1.Rows[i][3];
                    drTemp[4] = dt1.Rows[i][4];
                    drTemp[5] = dt1.Rows[i][5];
                    drTemp[6] = dt1.Rows[i][6];
                    drTemp[7] = dt1.Rows[i][7];
                    drTemp[8] = dt1.Rows[i][8];
                    drTemp[9] = dt1.Rows[i][9];
                    drTemp[10] = dt1.Rows[i][10];
                    drTemp[11] = dt1.Rows[i][11];
                    drTemp[12] = dt1.Rows[i][12];
                    drTemp[13] = dt1.Rows[i][13];
                    dt.Rows.Add(drTemp);
                }
                //TWO DATATABLE MERGE INTO ONE DATATABLE

                //
                if (chk_Allloan.Checked == true)
                {
                    str2 = "SELECT Ccode,Company_Name,Pcode,plant_name,plant_phoneno,Route_id, Aid,Agent_Name,loanid,ISNULL(paid_Amount,0) AS paid_Amount,PaidDate,ISNULL(Balance,0) AS Balance,ISNULL(Openningbalance,0)AS Openningbalance,ISNULL(Closingbalance,0)AS Closingbalance  FROM (SELECT Ccode,Pcode,Company_Name,Route_id, Aid,Agent_Name,loanid,CAST(paid_Amount AS DECIMAL(18,2)) AS paid_Amount,PaidDate,CAST(Balance AS DECIMAL(18,2)) AS Balance,CAST(Openningbalance AS DECIMAL(18,2)) AS Openningbalance,CAST(Closingbalance AS DECIMAL(18,2)) AS Closingbalance FROM (SELECT LRcccode AS Ccode,LRpcode AS Pcode,Route_id,LRAid AS Aid,Agent_Name,LRloanid AS loanid,paid_Amount,PaidDate,Balance,Openningbalance,Closingbalance FROM (SELECT * FROM (SELECT Company_code AS LRcccode,plant_code AS LRpcode,Route_id,loan_id AS LRloanid,Agent_id AS LRAid,'0' as Paid_Amount,(CONVERT(VARCHAR(100),loandate,106)) AS PaidDate,balance AS Openningbalance,LoanAmount AS Closingbalance FROM LoanDetails WHERE Loanamount=balance AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "') AS LR LEFT JOIN (SELECT Balance,loan_id,Agent_id,Company_Code AS Lccode,Plant_Code AS Lpcode FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS LD ON LR.LRloanid=LD.loan_id)AS t1 LEFT JOIN (SELECT Agent_id,Agent_Name,Company_Code AS Accode,Plant_Code AS Apcode FROM Agent_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS AM ON t1.LRAid=AM.Agent_id )AS t1 LEFT JOIN (SELECT Company_Code AS cccode ,Company_Name FROM Company_Master WHERE Company_Code='" + ccode + "')AS com ON t1.Ccode=com.cccode )AS t2 LEFT JOIN (SELECT company_code AS pccode,plant_code,plant_name,plant_address,plant_phoneno FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS plant ON t2.Pcode=plant.plant_code ORDER BY loanid,Openningbalance desc";
                }
                else
                {
                    // str = "SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '08-17-2012' AND '01-18-2013' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '08-17-2012' AND '01-18-2013' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ";
                    str2 = "SELECT Ccode,Company_Name,Pcode,plant_name,plant_phoneno,Route_id, Aid,Agent_Name,loanid,ISNULL(paid_Amount,0) AS paid_Amount,PaidDate,ISNULL(Balance,0) AS Balance,ISNULL(Openningbalance,0)AS Openningbalance,ISNULL(Closingbalance,0)AS Closingbalance  FROM (SELECT Ccode,Pcode,Company_Name,Route_id, Aid,Agent_Name,loanid,CAST(paid_Amount AS DECIMAL(18,2)) AS paid_Amount,PaidDate,CAST(Balance AS DECIMAL(18,2)) AS Balance,CAST(Openningbalance AS DECIMAL(18,2)) AS Openningbalance,CAST(Closingbalance AS DECIMAL(18,2)) AS Closingbalance FROM (SELECT LRcccode AS Ccode,LRpcode AS Pcode,Route_id,LRAid AS Aid,Agent_Name,LRloanid AS loanid,paid_Amount,PaidDate,Balance,Openningbalance,Closingbalance FROM (SELECT * FROM (SELECT Company_code AS LRcccode,plant_code AS LRpcode,Route_id,loan_id AS LRloanid,Agent_id AS LRAid,'0' as Paid_Amount,(CONVERT(VARCHAR(100),loandate,106)) AS PaidDate,balance AS Openningbalance,LoanAmount AS Closingbalance FROM LoanDetails WHERE Loanamount=balance AND Company_Code='" + ccode.Trim() + "' AND Plant_Code='" + pcode.Trim() + "' AND Route_id='" + rid + "' AND Agent_id='" + ddl_AgentID.Text + "'  ) AS LR LEFT JOIN (SELECT Balance,loan_id,Agent_id,Company_Code AS Lccode,Plant_Code AS Lpcode FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS LD ON LR.LRloanid=LD.loan_id)AS t1 LEFT JOIN (SELECT Agent_id,Agent_Name,Company_Code AS Accode,Plant_Code AS Apcode FROM Agent_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS AM ON t1.LRAid=AM.Agent_id )AS t1 LEFT JOIN (SELECT Company_Code AS cccode ,Company_Name FROM Company_Master WHERE Company_Code='" + ccode + "')AS com ON t1.Ccode=com.cccode )AS t2 LEFT JOIN (SELECT company_code AS pccode,plant_code,plant_name,plant_address,plant_phoneno FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS plant ON t2.Pcode=plant.plant_code ORDER BY loanid,Openningbalance desc";
                }

                SqlCommand cmd2 = new SqlCommand();
                SqlDataAdapter da2 = new SqlDataAdapter(str2, con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    DataRow drTemp1 = dt.NewRow();
                    drTemp1[0] = dt2.Rows[i][0];
                    drTemp1[1] = dt2.Rows[i][1];
                    drTemp1[2] = dt2.Rows[i][2];
                    drTemp1[3] = dt2.Rows[i][3];
                    drTemp1[4] = dt2.Rows[i][4];
                    drTemp1[5] = dt2.Rows[i][5];
                    drTemp1[6] = dt2.Rows[i][6];
                    drTemp1[7] = dt2.Rows[i][7];
                    drTemp1[8] = dt2.Rows[i][8];
                    drTemp1[9] = dt2.Rows[i][9];
                    drTemp1[10] = dt2.Rows[i][10];
                    drTemp1[11] = dt2.Rows[i][11];
                    drTemp1[12] = dt2.Rows[i][12];
                    drTemp1[13] = dt2.Rows[i][13];
                    dt.Rows.Add(drTemp1);
                }
                //THREE DATATABLE MERGE INTO ONE DATATABLE
            }         


            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;
    
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }
    private void loadrouteid()
    {
        SqlDataReader dr;


        dr = routmasterBL.getroutmasterdatareader(ccode, pcode);

        ddl_RouteName.Items.Clear();
        ddl_RouteID.Items.Clear();

        while (dr.Read())
        {
            ddl_RouteName.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
            ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());

        }

    }

    private void loadagentid()
    {
        SqlDataReader dr;

        dr = agentBL.GetAgentId1(ccode, pcode, Convert.ToInt32(rid));

        ddl_AgentID.Items.Clear();
        ddl_AgentName.Items.Clear();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddl_AgentID.Items.Add(dr["Agent_ID"].ToString().Trim());
                ddl_AgentName.Items.Add(dr["Agent_ID"].ToString().Trim() + "-" + dr["Agent_Name"].ToString().Trim());               
            }
        }
        else
        {
            WebMsgBox.Show("Please, Add the Agent");
        }

    }

    protected void btn_Loanrecoveryreport_Click(object sender, EventArgs e)
    {
        pcode = ddl_Plantcode.SelectedItem.Value;
        rid = ddl_RouteID.SelectedItem.Value;
        LoanrecoveryReport();

    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteName.SelectedIndex = ddl_RouteID.SelectedIndex;
        rid = ddl_RouteID.SelectedItem.Value;
        LoanrecoveryReport();
    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = ddl_RouteID.SelectedItem.Value;
        pcode = ddl_Plantcode.SelectedItem.Value;
        LoanrecoveryReport();
        loadagentid();
    }
    protected void ddl_AgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_AgentID.SelectedIndex = ddl_AgentName.SelectedIndex;
        
    }
    protected void chk_Allloan_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_Allloan.Checked == true)
        {
            lbl_RouteName.Visible = false;
            ddl_RouteName.Visible = false;
            lbl_AgentName.Visible = false;
            ddl_AgentName.Visible = false;
        }
        else
        {
            lbl_RouteName.Visible = true;
            ddl_RouteName.Visible = true;
            lbl_AgentName.Visible = true;
            ddl_AgentName.Visible = true;           
        }
       
    }


    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        rid = ddl_RouteID.SelectedItem.Value;
        loadrouteid();
        LoanrecoveryReport();
    
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
                ddl_Plantcode.Items.Add("--Select Plantcode--");
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
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
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
