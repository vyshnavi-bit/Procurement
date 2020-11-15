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

public partial class Bankpaymentdetails : System.Web.UI.Page
{
    BLLAgentmaster AgentBL = new BLLAgentmaster();
    public string ccode;
    public string pcode;
    public string Bankid;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

               // dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                LoadBankId();
                Bankid = ddl_BankID.SelectedItem.Value;
                ddl_paymentreport.SelectedIndex = 0;

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");    
                
              
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                Bankid = ddl_BankID.SelectedItem.Value;
                GetBankPaymentdetails();
               
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Bankid = ddl_BankID.SelectedItem.Value;
        GetBankPaymentdetails();
    }
    private void GetBankPaymentdetails()
    {
        try
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            cr.Load(Server.MapPath("Crpt_Bankpaymentdetails1.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            t1.Text = ccode + "_" + cname;
            t2.Text = pcode + "_" + pname;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();





            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");



            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            //str = "SELECT F.proAid,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Bank_Name,F.Payment_mode,F.Agent_AccountNo,F.Ifsc_code FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   INNER JOIN (SELECT Bank_id AS Bid,Bank_Name,Ifsc_code,Company_code,Plant_code FROM BANK_MASTER WHERE company_code='" + ccode + "' AND Plant_code='" + pcode + "') AS Bank ON FF.Bank_id=Bank.Bid ) AS F ORDER BY F.proAid,F.Prdate,F.Sessions ";
            //old working str = "SELECT F.proAid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Bank_Name,F.Payment_mode,F.Agent_AccountNo,F.Ifsc_code FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro LEFT JOIN  (SELECT agent_id AS proAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'   AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_id='" + Bankid + "') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN (SELECT Bank_id AS Bid,Bank_Name,Ifsc_code,Company_code,Plant_code FROM BANK_MASTER WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_id='" + Bankid + "') AS Bank ON FF.Bank_id=Bank.Bid) AS F ORDER BY F.proAid";
            if (ddl_paymentreport.SelectedItem.Value == "All")
            {

                str = "SELECT cart.ARid AS Rid,cart.cartAid AS Aid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,ISNULL(cart.Bank_Id,0) AS Bank_Id,ISNULL(cart.Payment_mode,0) AS Payment_mode,ISNULL(cart.Agent_AccountNo,0) AS Agent_AccountNo,ISNULL(cart.Ifsc_Code,0) AS Ifsc_Code ,ISNULL(cart.Agent_Name,0) AS Agent_Name,ISNULL(cart.Bank_Name,0) AS Bank_Name FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'   AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT * FROM (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Ifsc_Code,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS carts LEFT JOIN  (SELECT Bank_id AS Bdbid,Bank_Name FROM BANK_Details) AS BD ON carts.Bank_Id=BD.Bdbid ) AS cart ON  prdelo.SproAid=cart.cartAid ORDER BY  cart.cartAid,cart.Bank_Id,cart.Ifsc_Code";
            }
            else if (ddl_paymentreport.SelectedItem.Value == "Bank")
            {

                str = "SELECT cart.ARid AS Rid,cart.cartAid AS Aid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,ISNULL(cart.Bank_Id,0) AS Bank_Id,ISNULL(cart.Payment_mode,0) AS Payment_mode,ISNULL(cart.Agent_AccountNo,0) AS Agent_AccountNo,ISNULL(cart.Ifsc_Code,0) AS Ifsc_Code ,ISNULL(cart.Agent_Name,0) AS Agent_Name,ISNULL(cart.Bank_Name,0) AS Bank_Name FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'   AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT * FROM (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Ifsc_Code,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Bank_id > 0 ) AS carts LEFT JOIN  (SELECT Bank_id AS Bdbid,Bank_Name FROM BANK_Details) AS BD ON carts.Bank_Id=BD.Bdbid ) AS cart ON  prdelo.SproAid=cart.cartAid ORDER BY  cart.cartAid,cart.Bank_Id,cart.Ifsc_Code";
            }
            else
            {
                str = "SELECT cart.ARid AS Rid,cart.cartAid AS Aid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,ISNULL(cart.Bank_Id,0) AS Bank_Id,ISNULL(cart.Payment_mode,0) AS Payment_mode,ISNULL(cart.Agent_AccountNo,0) AS Agent_AccountNo,ISNULL(cart.Ifsc_Code,0) AS Ifsc_Code ,ISNULL(cart.Agent_Name,0) AS Agent_Name,ISNULL(cart.Bank_Name,0) AS Bank_Name FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'   AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT * FROM (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Ifsc_Code,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Bank_id =0 ) AS carts LEFT JOIN  (SELECT Bank_id AS Bdbid,Bank_Name FROM BANK_Details) AS BD ON carts.Bank_Id=BD.Bdbid ) AS cart ON  prdelo.SproAid=cart.cartAid ORDER BY  cart.cartAid,cart.Bank_Id,cart.Ifsc_Code";

            }
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
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }
    protected void ddl_BankID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BankName.SelectedIndex = ddl_BankID.SelectedIndex;
        Bankid = ddl_BankID.SelectedItem.Value;
        GetBankPaymentdetails();
    }
    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BankID.SelectedIndex = ddl_BankName.SelectedIndex;
        Bankid = ddl_BankID.SelectedItem.Value;
        GetBankPaymentdetails();
    }
    private void LoadBankId()
    {
        SqlDataReader dr = null;
        dr = AgentBL.GetBankID(ccode, pcode);
        ddl_BankID.Items.Clear();
        ddl_BankName.Items.Clear();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddl_BankID.Items.Add(dr["Bank_ID"].ToString().Trim());
                ddl_BankName.Items.Add(dr["Bank_ID"].ToString().Trim() + "_" + dr["Bank_Name"].ToString().Trim());

                // ddl_BankId.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }


    }
}
