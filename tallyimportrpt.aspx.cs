using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;

public partial class tallyimportrpt : System.Web.UI.Page
{
    public string rid;
    public string frmdate;
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();
    DbHelper DB = new DbHelper();
    public string companycode;
    public string plantcode;
    DataTable dt = new DataTable();
    BOLDispatch DispatchBOL = new BOLDispatch();
    BLLDispatch DispatchBLL = new BLLDispatch();
    DALDispatch DispatchDAL = new DALDispatch();
    public static int roleid;
    BLLuser Bllusers = new BLLuser();
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    string month;
    int Checkstatus;
    string Tallyno;
    string BILLDATE;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                LoadPlantcode();
                rid = ddl_PlantName.SelectedItem.Value;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }


    }
    DataTable Report = new DataTable();
    DataTable IMPReport = new DataTable();
    protected void Button2_Click(object sender, EventArgs e)
    {
        Report.Columns.Add("ROUTENAME");
        Report.Columns.Add("AGENTID");
        Report.Columns.Add("AGENTNAME");
        Report.Columns.Add("Kgs");
        Report.Columns.Add("Ltrs");
        Report.Columns.Add("MR");
        Report.Columns.Add("Comm");
        Report.Columns.Add("Scart");
        Report.Columns.Add("Splbonus");
        Report.Columns.Add("TotAmount");
        Report.Columns.Add("A/R");
        Report.Columns.Add("Claim");
        Report.Columns.Add("Loan");
        Report.Columns.Add("BalAdv");
        Report.Columns.Add("Mater");
        Report.Columns.Add("Feed");
        Report.Columns.Add("Can");
        Report.Columns.Add("Rnd");
        Report.Columns.Add("Amount");
        Report.Columns.Add("A/F");
        Report.Columns.Add("A/S");
        Report.Columns.Add("T/S");
        Report.Columns.Add("R/TS");
        string fromdate = txt_FromDate.Text;
        string todate = txt_ToDate.Text;

        string ccode = Session["Company_code"].ToString();
        string pcode = ddl_PlantName.SelectedItem.Value;
        SqlConnection con = null;
        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        con = new SqlConnection(connection);
        // str = "SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '08-17-2012' AND '01-18-2013' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '08-17-2012' AND '01-18-2013' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ";

        string strsql = "SELECT F.Route_name, F.SproAid, F.Agent_Name, ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS SInsentAmt,ISNULL(F.Scaramt,0) AS Scaramt,ISNULL(F.AvRate,0) AS SSplBonus,ISNULL(F.ClaimAount,0) AS ClaimAount,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS Sinstamt,ISNULL(F.balance,0) AS SLoanClosingbalance,(ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0)) AS TotAdditions,(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)) AS TotDeductions,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0))) AS NetAmount,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))-(FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))) AS Roundoff,ISNULL(F.CarAmt,0) AS AgentCarAmt,ISNULL(F.LoanAmount,0) AS SLoanAmount FROM " +
               "(SELECT * FROM (SELECT * FROM (SELECT * FROM  (SELECT * FROM  (SELECT * FROM " +
               "(SELECT SproAid,RateChart_id,MilkType,State_id,State_name,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg,CAST(Scaramt AS DECIMAL(18,2)) AS Scaramt FROM  " +
               "(SELECT  agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,SUM(ComRate) AS Avgcrate,SUM(CAST(fat_kg AS DECIMAL(18,2))) AS Sfatkg,SUM(CAST(snf_kg AS DECIMAL(18,2))) AS SSnfkg,SUM(CAST(ISNULL(CartageAmount,0) AS DECIMAL(18,2))) AS Scaramt FROM Procurement WHERE prdate BETWEEN '" + fromdate + "' AND '" + todate + "' AND Plant_Code='" + pcode + "'    AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro   " +
               "INNER JOIN  (SELECT DISTINCT(Agent_Id) AS proAid ,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid,Milk_Nature AS State_id,Milk_Nature AS State_name FROM Procurement WHERE PRDATE BETWEEN '" + fromdate + "' AND '" + todate + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "'  ) AS pro ON Spro.SproAid=pro.proAid ) AS protot  " +
               "LEFT JOIN  (SELECT  Ragent_id AS DAid ,SUM(Rbilladvance) AS Billadv,SUM(RAi) AS Ai,SUM(RFeed) AS Feed,SUM(Rcan) AS can,SUM(Rrecovery) AS Recovery,SUM(Rothers) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + fromdate + "' AND '" + todate + "' AND Rcompany_code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY Ragent_id) AS Dedu ON protot.SproAid=Dedu.DAid )AS prodedu " +
               "LEFT JOIN (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM  " +
               "(SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn " +
               " LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + fromdate + "' AND '" + todate + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  " +
               "LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + fromdate + "' AND '" + todate + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid) AS Loan  ON prodedu.SproAid=Loan.LoAid) AS pdl " +
               "LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS ClaimAount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + fromdate + "' AND '" + todate + "' GROUP BY Agent_Id) AS vou ON pdl.SproAid=vou.VouAid )AS pdlv  " +
               "INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Ifsc_code FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON pdlv.SproAid=cart.cartAid ) AS F2 " +
               "LEFT JOIN  (SELECT route_id,Route_name,plant_code,company_code,phone_No FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F2.Rid=R.Route_ID ) AS F LEFT JOIN " +
               "(SELECT Company_code AS Bccode,Bank_id,Bank_Name  FROM Bank_Details Where Company_Code='" + ccode + "') AS Ban  ON F.Bank_Id=Ban.Bank_id ORDER  BY F.Route_ID,F.SproAid";

        SqlCommand cmd = new SqlCommand();
        cmd.CommandTimeout = 300;
        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            double totnetamount = 0;
            double subTOTALKGS = 0;
            double TOTALKGS = 0;
            double TOTALAMOUNT = 0;
            double SUBTOTALAMOUNT = 0;
            double subTOTALLTRS = 0;
            double subTOTamt = 0;
            double TOTALLTRS = 0;
            string privroute = "";
           
            foreach (DataRow dr in dt.Rows)
            {
                string routename = dr["Route_name"].ToString();
                if (privroute == routename)
                {
                    DataRow newrow = Report.NewRow();
                    newrow["AGENTID"] = dr["SproAid"].ToString();
                    newrow["ROUTENAME"] = dr["Route_name"].ToString();
                    newrow["AGENTNAME"] = dr["Agent_Name"].ToString();
                    newrow["Ltrs"] = dr["Smltr"].ToString();
                    newrow["Kgs"] = dr["Smkg"].ToString();
                    //new fields added 
                    double netamt = Convert.ToDouble(dr["NetAmount"].ToString());
                    totnetamount += netamt;
                    subTOTamt += netamt;
                    double Smkg = Convert.ToDouble(dr["Smkg"].ToString());
                    double Smltr = Convert.ToDouble(dr["Smltr"].ToString());
                    TOTALKGS += Smkg;
                    TOTALLTRS += Smltr;
                    subTOTALKGS += Smkg;
                    subTOTALLTRS += Smltr;
                    double samt = Convert.ToDouble(dr["Samt"].ToString());
                    newrow["Amount"] = dr["Samt"].ToString();
                    newrow["MR"] = Math.Round(samt / Smltr, 2);
                    double SInsentamt = Convert.ToDouble(dr["SInsentamt"].ToString());
                    double Scaramt = Convert.ToDouble(dr["Scaramt"].ToString());
                    double SSplBonus = Convert.ToDouble(dr["SSplBonus"].ToString());
                    double ARATE = (samt + SInsentamt + Scaramt + SSplBonus) / Smltr;
                    newrow["Comm"] = dr["SInsentamt"].ToString();
                    newrow["Scart"] = dr["Scaramt"].ToString();
                    newrow["Splbonus"] = dr["SSplBonus"].ToString();
                    double totadditions = Convert.ToDouble(dr["TotAdditions"].ToString());
                    newrow["TotAmount"] = Math.Round(samt + totadditions, 2);
                    TOTALAMOUNT += Math.Round(samt + totadditions, 2);
                    SUBTOTALAMOUNT += Math.Round(samt + totadditions, 2);
                    newrow["A/R"] = Math.Round(ARATE, 2);

                    newrow["Claim"] = dr["ClaimAount"].ToString();
                    newrow["Loan"] = dr["Sinstamt"].ToString();
                    newrow["BalAdv"] = dr["Billadv"].ToString();
                    newrow["Mater"] = dr["Ai"].ToString();
                    newrow["Feed"] = dr["Feed"].ToString();
                    newrow["Can"] = dr["can"].ToString();
                    newrow["Rnd"] = dr["Roundoff"].ToString();
                    newrow["Amount"] = dr["NetAmount"].ToString();
                    double sfatkg = Convert.ToDouble(dr["Sfatkg"].ToString());
                    double ssnfkg = Convert.ToDouble(dr["SSnfkg"].ToString());
                    double afat = (sfatkg * 100) / Smkg;
                    double asnf = (ssnfkg * 100) / Smkg;
                    newrow["A/F"] = Math.Round(afat, 2);
                    newrow["A/S"] = Math.Round(asnf, 2);
                    double ts = Math.Round((afat + asnf), 2);
                    newrow["T/S"] = ts.ToString();
                    double rts = Math.Round(ARATE / ts, 2);
                    newrow["R/TS"] = rts.ToString();
                    Report.Rows.Add(newrow);
                }
                else
                {
                    if (subTOTALKGS > 0)
                    {
                       
                    }
                    privroute = routename;
                    DataRow newrow2 = Report.NewRow();
                    newrow2["AGENTID"] = dr["SproAid"].ToString();
                    newrow2["ROUTENAME"] = dr["Route_name"].ToString();
                    newrow2["AGENTNAME"] = dr["Agent_Name"].ToString();
                    newrow2["Amount"] = dr["NetAmount"].ToString();
                    newrow2["Kgs"] = dr["Smkg"].ToString();
                    newrow2["Ltrs"] = dr["Smltr"].ToString();
                    double netamtt = Convert.ToDouble(dr["NetAmount"].ToString());
                    totnetamount += netamtt;
                    subTOTamt += netamtt;
                    double Smkgg = Convert.ToDouble(dr["Smkg"].ToString());
                    double Smltrr = Convert.ToDouble(dr["Smltr"].ToString());
                    TOTALKGS += Smkgg;
                    TOTALLTRS += Smltrr;
                    subTOTALKGS += Smkgg;
                    subTOTALLTRS += Smltrr;
                    double samt = Convert.ToDouble(dr["Samt"].ToString());
                    newrow2["Amount"] = dr["Samt"].ToString();
                    newrow2["MR"] = Math.Round(samt / Smltrr, 2);
                    double SInsentamt = Convert.ToDouble(dr["SInsentamt"].ToString());
                    double Scaramt = Convert.ToDouble(dr["Scaramt"].ToString());
                    double SSplBonus = Convert.ToDouble(dr["SSplBonus"].ToString());
                    double ARATE = (samt + SInsentamt + Scaramt + SSplBonus) / Smltrr;
                    newrow2["Comm"] = dr["SInsentamt"].ToString();
                    newrow2["Scart"] = dr["Scaramt"].ToString();
                    newrow2["Splbonus"] = dr["SSplBonus"].ToString();
                    double totadditions = Convert.ToDouble(dr["TotAdditions"].ToString());
                    newrow2["TotAmount"] = Math.Round(samt + totadditions, 2);
                    TOTALAMOUNT += Math.Round(samt + totadditions, 2);
                    SUBTOTALAMOUNT += Math.Round(samt + totadditions, 2);
                    newrow2["A/R"] = Math.Round(ARATE, 2);
                    newrow2["Claim"] = dr["ClaimAount"].ToString();
                    newrow2["Loan"] = dr["Sinstamt"].ToString();
                    newrow2["BalAdv"] = dr["Billadv"].ToString();
                    newrow2["Mater"] = dr["Ai"].ToString();
                    newrow2["Feed"] = dr["Feed"].ToString();
                    newrow2["Can"] = dr["can"].ToString();
                    newrow2["Rnd"] = dr["Roundoff"].ToString();
                    newrow2["Amount"] = dr["NetAmount"].ToString();
                    double sfatkg = Convert.ToDouble(dr["Sfatkg"].ToString());
                    double ssnfkg = Convert.ToDouble(dr["SSnfkg"].ToString());
                    double afat = (sfatkg * 100) / Smkgg;
                    double asnf = (ssnfkg * 100) / Smkgg;
                    newrow2["A/F"] = Math.Round(afat, 2);
                    newrow2["A/S"] = Math.Round(asnf, 2);
                    double ts = Math.Round(afat + asnf, 2);
                    newrow2["T/S"] = ts.ToString();
                    double rts = Math.Round(ARATE / ts, 2);
                    newrow2["R/TS"] = rts.ToString();
                    Report.Rows.Add(newrow2);
                }
            }
        }
        
        if (Report.Rows.Count > 0)
        {
            string[] p = todate.Split('/');
            string year = p[2];
            string day = p[0];
            if (p[1] == "01")
            {
                month = "Jan";
            }
            if (p[1] == "02")
            {
                month = "Feb";
            }
            if (p[1] == "03")
            {
                month = "Mar";
            }
            if (p[1] == "04")
            {
                month = "Apr";
            }
            if (p[1] == "05")
            {
                month = "May";
            }
            if (p[1] == "06")
            {
                month = "June";
            }
            if (p[1] == "07")
            {
                month = "July";
            }
            if (p[1] == "08")
            {
                month = "Aug";
            }
            if (p[1] == "09")
            {
                month = "Sep";
            }
            if (p[1] == "10")
            {
                month = "Oct";
            }

            if (p[1] == "11")
            {
                month = "Nov";
            }
            if (p[1] == "12")
            {
                month = "Dec";
            }
            if (ddl_PlantName.SelectedItem.Text == "ARANI")
            {
                Tallyno = "ARA";
            }
            if (ddl_PlantName.SelectedItem.Text == "KAVERIPATNAM")
            {
                Tallyno = "KVP";
            }
            if (ddl_PlantName.SelectedItem.Text == "GUDLUR")
            {
                Tallyno = "GUD";
            }
            if (ddl_PlantName.SelectedItem.Text == "WALAJA")
            {
                Tallyno = "WAL";
            }
            if (ddl_PlantName.SelectedItem.Text == "V_KOTA")
            {
                Tallyno = "VKT";
            }
            if (ddl_PlantName.SelectedItem.Text == "RCPURAM")
            {
                Tallyno = "RCP";
            }
            if (ddl_PlantName.SelectedItem.Text == "BOMMASAMUTHIRAM")
            {
                Tallyno = "BOMMA";
            }
            if (ddl_PlantName.SelectedItem.Text == "TARIGONDA")
            {
                Tallyno = "TARI";
            }
            if (ddl_PlantName.SelectedItem.Text == "CSPURAM")
            {
                Tallyno = "CSP";
            }
            if (ddl_PlantName.SelectedItem.Text == "KONDEPI")
            {
                Tallyno = "KDP";

            }
            if (ddl_PlantName.SelectedItem.Text == "KAVALI")
            {

                Tallyno = "KVL";
            }
            if (ddl_PlantName.SelectedItem.Text == "GUDIPALLI PADU")
            {

                Tallyno = "GUDI";
            }
            if (ddl_PlantName.SelectedItem.Text == "KALIGIRI")
            {
                Tallyno = "KLG";
            }
            IMPReport.Columns.Add("VchNo");
            IMPReport.Columns.Add("VchDate");
            IMPReport.Columns.Add("SupplierInvNo");
            IMPReport.Columns.Add("SupDate");
            IMPReport.Columns.Add("SupplierName");
            IMPReport.Columns.Add("PurchaseLedger");
            IMPReport.Columns.Add("ItemName");
            IMPReport.Columns.Add("Billedqty");
            IMPReport.Columns.Add("Rate");
            IMPReport.Columns.Add("Amount");
            IMPReport.Columns.Add("ProcurementCortage-ARANI");
            IMPReport.Columns.Add("OtherIncome-ARANI");
            IMPReport.Columns.Add("ProcurementMaterial-Debtor-ARANI");
            IMPReport.Columns.Add("CattleFeedSales-Agents-ARANI");
            IMPReport.Columns.Add("Cans&LidsStock-ARANI");
            IMPReport.Columns.Add("Dpu Maintenance Charges-ARANI");
            IMPReport.Columns.Add("LedgerAmount");
            IMPReport.Columns.Add("Narration");
            double ltr;
            string conltr;
            int i=0;
            foreach (DataRow tally in Report.Rows)
            {
                int vchno = i++;
                DataRow newrow = IMPReport.NewRow();
                newrow["VchNo"] = "" + vchno + " " + Tallyno + "";
               // string tvocherno = vchno.ToString();
                DateTime dtdate = Convert.ToDateTime(todate);
                string vocherdate = dtdate.ToString("dd-MMM-yyyy");
                string vchDate = vocherdate;
                newrow["VchDate"] = vchDate;
                string agentid = tally["AGENTID"].ToString();
                string Suppliername = tally["AGENTNAME"].ToString();
                
                string SupplierInvNo = "" + agentid + "-" + Suppliername + "-" + ddl_PlantName.SelectedItem.Text + "-" + month + " " + year + "";
                newrow["SupplierInvNo"] = SupplierInvNo;
                string supname = "" + agentid + "-" + Suppliername + "-" + ddl_PlantName.SelectedItem.Text;
                newrow["SupplierName"] = supname;

                string Supplierdate = vocherdate;
                newrow["SupDate"] = Supplierdate;
                string Ledgerofpurcharseplant = "Purchase of Milk-" + ddl_PlantName.SelectedItem.Text;
                newrow["PurchaseLedger"] = Ledgerofpurcharseplant;
                double Billedqty = Convert.ToDouble(tally["Kgs"].ToString());
                //ltr = Billedqty / 1.03;
                conltr = tally["Ltrs"].ToString();
                newrow["Billedqty"] = conltr;
                //ViewState["ltr"] = conltr;
                double Rate = Convert.ToDouble(tally["A/R"].ToString());
                double Amount = Convert.ToDouble(tally["TotAmount"].ToString());
                string Cartage = tally["Scart"].ToString();
                string Others = tally["Rnd"].ToString();
                string material = tally["Mater"].ToString();
                string Feed = tally["Feed"].ToString();
                string Canorstock = tally["Can"].ToString();
                string dpu = "";
                string af = tally["A/F"].ToString();
                string ass = tally["A/S"].ToString();
                string ts = tally["T/S"].ToString();
                string rts = tally["R/TS"].ToString();
                string totAmount = tally["Amount"].ToString();
                newrow["Rate"] = Rate;
                newrow["Amount"] = Amount;
                newrow["ProcurementCortage-ARANI"] = Cartage;
                newrow["OtherIncome-ARANI"] = Others;
                newrow["ProcurementMaterial-Debtor-ARANI"] = material;
                newrow["CattleFeedSales-Agents-ARANI"] = Feed;
                newrow["Cans&LidsStock-ARANI"] = Canorstock;
                newrow["Dpu Maintenance Charges-ARANI"] = dpu;
                newrow["LedgerAmount"] = totAmount;
                string ItemName;
                string Kgrate;
                if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                {
                    ItemName = "COW MILK";
                    newrow["ItemName"] = ItemName;
                    double TEMPKG = (Billedqty * 1.03);
                    string STTEMKH = TEMPKG.ToString("F2");
                    TEMPKG = Convert.ToDouble(STTEMKH);
                    string Narration = "Being PurChase Of MilkFrom " + SupplierInvNo + "TotalQty=" + conltr + " Kgs/ltr:" + Billedqty + "A/f:" + af + "A/S:" + ass + "T/S:" + ts + "R/TS:" + rts;
                    newrow["Narration"] = Narration;
                }
                else
                {
                    ItemName = "WHOLE MILK";
                    string Narration = "Being PurChase Of MilkFrom " + SupplierInvNo + "TotalQty=" + conltr + " Kgs/ltr:" + Billedqty + "A/f:" + af + "A/S:" + ass + "Kgfat:" + ts + "KgSnf:" + rts + "Kgrate:";
                    newrow["Narration"] = Narration;
                }

                string Supplierinvno = Suppliername + "(" + getvald + "-" + getvaldd + ")" + month + getvaly;
                BILLDATE = Supplierinvno;
                string gettp = ViewState["maxtid"] + Tallyno;
                IMPReport.Rows.Add(newrow);
            }
        }
        GridView2.DataSource = IMPReport;
        GridView2.DataBind();
        Session["xportdata"] = IMPReport;
        Session["filename"] = "filename " + ddl_PlantName.SelectedItem.Text + ":Bill Period" + fromdate + "To:" + todate + "";
        //hidepanel.Visible = true;
    }

    protected void grdReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[4].Visible = false;
            if (e.Row.Cells.Count > 0)
            {
                if (e.Row.Cells[1].Text == "Total")
                {
                    e.Row.BackColor = System.Drawing.Color.CadetBlue;
                    e.Row.Font.Size = FontUnit.Large;
                    e.Row.Font.Bold = true;

                }
                if (e.Row.Cells[1].Text == "Grand Total")
                {
                    e.Row.BackColor = System.Drawing.Color.Aquamarine;
                    e.Row.Font.Size = FontUnit.Large;
                    e.Row.Font.Bold = true;

                }
            }
        }
    }


    public void LoadPlantcode()
    {
        try
        {
            DataSet DTG = new DataSet();
            SqlConnection con = new SqlConnection();
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_PlantName.DataSource = DTG.Tables[0];
            ddl_PlantName.DataTextField = "Plant_Name";
            ddl_PlantName.DataValueField = "Plant_Code";
            ddl_PlantName.DataBind();
        }
        catch
        {


        }

    }
}