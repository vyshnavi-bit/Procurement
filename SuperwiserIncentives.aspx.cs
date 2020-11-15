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
using System.Net;
using CrystalDecisions.ReportSource;


public partial class SuperwiserIncentives : System.Web.UI.Page
{

    BLLroutmaster routmasterBL = new BLLroutmaster();
    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    public string rid;
    public int btnvalloanupdate = 0;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DateTime dtm = new DateTime();
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

                dtm = System.DateTime.Now;
               // dti = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyy");
                //      txt_FromDate.Text = dtm.ToShortDateString();
                //     txt_ToDate.Text = dtm.ToShortDateString();
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }


                // RoutewisePaymentAbstract();

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
                //  loadrouteid();
                pcode = ddl_Plantcode.SelectedItem.Value;
                //   rid = ddl_RouteID.SelectedItem.Value;
                loadreport();

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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        loadreport();
    }
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        loadreport();
    }


    public void loadreport()
    {


        try
        {
            DataTable custdt = new DataTable();
            DataRow custdr = null;
            cr.Load(Server.MapPath("SUPERWISEINCENTIVES.rpt"));
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
            t2.Text = ddl_Plantname.SelectedItem.Value;
           

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To" + txt_ToDate.Text.Trim();


            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            
            //old workig 4-2-2014  //str = "SELECT * FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid, ISNULL(prdelo.Smkg,0) AS Smkg, ISNULL(prdelo.Smltr,0) AS Smltr, ISNULL(prdelo.AvgFat ,0) AS AvgFat, ISNULL(prdelo.AvgSnf,0) AS AvgSnf, ISNULL(prdelo.AvgRate,0) AS AvgRate, ISNULL(prdelo.Avgclr,0) AS Avgclr, ISNULL(prdelo.Scans,0) AS Scans, ISNULL(prdelo.SAmt,0) AS SAmt, ISNULL(prdelo.ScommAmt,0) AS ScommAmt, ISNULL(prdelo.Scatamt,0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate, ISNULL(prdelo.Sfatkg,0) AS Sfatkg, ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg, ISNULL(prdelo.Billadv,0) AS SBilladv, ISNULL(prdelo.Ai,0) AS SAiamt, ISNULL(prdelo.Feed,0) AS SFeedamt, ISNULL(prdelo.Can,0) AS Scanamt, ISNULL(prdelo.Recovery,0) AS SRecoveryamt, ISNULL(prdelo.others,0) AS Sothers, ISNULL(prdelo.instamt,0) AS Sinstamt, ISNULL(prdelo.balance,0) AS Sbalance, ISNULL(prdelo.LoanAmount,0) AS SLoanAmount, ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SRNetAmt, FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt, ( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound, cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS Routewiseagent LEFT JOIN (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode +"' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS Route ON Routewiseagent.Rid=Route.ARid ORDER BY Routewiseagent.Rid ,Routewiseagent.Aid  ";
            //OLD WORKING 17-5-2014 str = "SELECT * FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid, ISNULL(prdelo.Smkg,0) AS Smkg, ISNULL(prdelo.Smltr,0) AS Smltr, ISNULL(prdelo.AvgFat ,0) AS AvgFat, ISNULL(prdelo.AvgSnf,0) AS AvgSnf, ISNULL(prdelo.AvgRate,0) AS AvgRate, ISNULL(prdelo.Avgclr,0) AS Avgclr, ISNULL(prdelo.Scans,0) AS Scans, ISNULL(prdelo.SAmt,0) AS SAmt, ISNULL(prdelo.ScommAmt,0) AS ScommAmt, ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate, ISNULL(prdelo.Sfatkg,0) AS Sfatkg, ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg, ISNULL(prdelo.Billadv,0) AS SBilladv, ISNULL(prdelo.Ai,0) AS SAiamt, ISNULL(prdelo.Feed,0) AS SFeedamt, ISNULL(prdelo.Can,0) AS Scanamt, ISNULL(prdelo.Recovery,0) AS SRecoveryamt, ISNULL(prdelo.others,0) AS Sothers, ISNULL(prdelo.instamt,0) AS Sinstamt, ISNULL(prdelo.balance,0) AS Sbalance, ISNULL(prdelo.LoanAmount,0) AS SLoanAmount, ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SRNetAmt, FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt, ( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound, cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS Routewiseagent LEFT JOIN (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS Route ON Routewiseagent.Rid=Route.ARid ORDER BY Routewiseagent.Rid ,Routewiseagent.Aid ";

            //31-7-2014 work loandetails  str = "SELECT * FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,ISNULL(prdelo.Smkg,0) AS Smkg, ISNULL(prdelo.Smltr,0) AS Smltr, ISNULL(prdelo.AvgFat ,0) AS AvgFat, ISNULL(prdelo.AvgSnf,0) AS AvgSnf, ISNULL(prdelo.AvgRate,0) AS AvgRate, ISNULL(prdelo.Avgclr,0) AS Avgclr, ISNULL(prdelo.Scans,0) AS Scans, ISNULL(prdelo.SAmt,0) AS SAmt, ISNULL(prdelo.ScommAmt,0) AS ScommAmt, ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate, ISNULL(prdelo.Sfatkg,0) AS Sfatkg, ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg, ISNULL(prdelo.Billadv,0) AS SBilladv, ISNULL(prdelo.Ai,0) AS SAiamt, ISNULL(prdelo.Feed,0) AS SFeedamt, ISNULL(prdelo.Can,0) AS Scanamt, ISNULL(prdelo.Recovery,0) AS SRecoveryamt, ISNULL(prdelo.others,0) AS Sothers, ISNULL(prdelo.instamt,0) AS Sinstamt, ISNULL(prdelo.balance,0) AS Sbalance, ISNULL(prdelo.LoanAmount,0) AS SLoanAmount, ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) +(ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SRNetAmt, FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt, ( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound, cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded  LEFT JOIN  (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS Routewiseagent   LEFT JOIN   (SELECT  G1.ARid,  CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM   (SELECT cart.ARid, ISNULL(prdelo.Smkg,0) AS Smkg,    ISNULL(prdelo.Smltr,0) AS Smltr,  ISNULL(prdelo.AvgFat ,0) AS AvgFat,  ISNULL(prdelo.AvgSnf,0) AS AvgSnf,  ISNULL(prdelo.AvgRate,0) AS AvgRate,  ISNULL(prdelo.Avgclr,0) AS Avgclr,  ISNULL(prdelo.Scans,0) AS Scans,  ISNULL(prdelo.SAmt,0) AS SAmt,  ISNULL(prdelo.ScommAmt,0) AS ScommAmt,  ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,  ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,  ISNULL(prdelo.AvgcRate,0) AS AvgcRate,  ISNULL(prdelo.Sfatkg,0) AS Sfatkg,  ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,  ISNULL(prdelo.Billadv,0) AS SBilladv,  ISNULL(prdelo.Ai,0) AS SAiamt,  ISNULL(prdelo.Feed,0) AS SFeedamt,  ISNULL(prdelo.Can,0) AS Scanamt,  ISNULL(prdelo.Recovery,0) AS SRecoveryamt,  ISNULL(prdelo.others,0) AS Sothers,  ISNULL(prdelo.instamt,0) AS Sinstamt,  ISNULL(prdelo.balance,0) AS Sbalance,  ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,  FLOOR (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))  AS SNetAmt,  ( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (  SELECT * FROM (  SELECT * FROM (  SELECT agent_id AS SproAid,  CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,  CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,  CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,  CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,  CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,  CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,  CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,  CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,  CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,  CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,  CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,  CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,  CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,  CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro  LEFT JOIN   (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS Route ON Routewiseagent.Rid=Route.ARid ORDER BY Routewiseagent.Rid ,Routewiseagent.Aid ";
            // recovery loan
            pcode = ddl_Plantcode.SelectedItem.Value;
           
           //str = "SELECT * FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) +(ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) +ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0)  + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(ROUND(fat_kg,2,1)) AS Sfatkg,CAST(SUM(ROUND(snf_kg,2,1)) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'   GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid ) AS fin  LEFT JOIN (SELECT G1.Rid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround ,CAST(SUM(G1.SClaim) AS DECIMAL(18,2)) AS GSClaim FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) +(ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) +ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM  (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(ROUND(fat_kg,2,1)) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(ROUND(snf_kg,2,1)) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded    LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid ) AS G1 GROUP BY G1.Rid ) AS gf ON fin.Rid=gf.Rid ORDER BY fin.Rid,fin.Aid";
          //work upto 21-11-2014  str = "SELECT Distinct(SRid) AS Route_ID,Route_Name,ROUND(Smltr,2,1) AS STotmltr,ROUND(Smltr/(t4.DDay+1),2,1) AS PerDayQty,ROUND(Sfkg,2,1) AS Sfkg,ROUND(SSkg,2,1) AS SSkg,ROUND((ISNULL(Sfkg,0)*100)/ISNULL(Smkg,0),1,1) AS Afat,ROUND((ISNULL(SSkg,0)*100)/ISNULL(Smkg,0),1,1) AS Asnf,ROUND(ISNULL(TotAmount,0)/ISNULL(Smltr,0),2,1) AS Arate,ROUND(Arate,2,0) AS Mrate,(t4.DDay+1) AS DDays FROM  (SELECT  SRid,SUM((ISNULL(Milk_ltr,0))) AS Smltr,SUM((ISNULL(Milk_kg,0))) AS Smkg,SUM(ISNULL(fat_kg,0)) AS Sfkg,SUM(ISNULL(snf_kg,0)) AS SSkg,SUM(ISNULL(Amount,0))/SUM(ISNULL(Milk_ltr,0)) AS Arate,(SUM(ISNULL(Amount,0))+SUM(ISNULL(SplBonAmt,0))+SUM(ISNULL(cartAmmt,0))+SUM(ISNULL(InsAmt,0))) AS TotAmount,SUM(ISNULL(Amount,0)) AS SMAmount FROM  (SELECT Route_id, Milk_ltr,Milk_kg,Fat,Snf,fat_kg,snf_kg,Amount,Round(SplBonusAmount,2,1) AS SplBonAmt,Round(CartageAmount,2,1) AS cartAmmt,Round(ComRate,2,1) AS InsAmt FROM Procurement  WHERE Company_Code=1 AND Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "') AS t1  LEFT JOIN  (SELECT Route_Name ,Route_Id AS SRid FROM Route_Master WHERE Company_Code=1 AND Plant_Code='" + pcode + "') AS t2 ON t1.Route_id=t2.SRid GROUP BY SRid) AS t3 INNER JOIN  (SELECT DATEDIFF(day,'" + dt1 + "','" + dt2 + "') AS DDay,Route_ID AS Rrid,CONVERT(Varchar(100), Route_ID)+'_'+Route_Name AS Route_Name  FROM Route_Master  WHERE Company_Code=1 AND Plant_Code='" + pcode + "' ) AS t4 ON t3.SRid=t4.Rrid ORDER BY SRid ";              
       
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            
            using (conn = new SqlConnection(dbConnStr))
            {

                SqlCommand sqlCmd = new SqlCommand("dbo.[GetSupervisorInsentives]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;              
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spdate1", d1);
                sqlCmd.Parameters.AddWithValue("@spdate2", d2);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);

                DataColumn col = null;
                col = new DataColumn("SupervisorName");
                custdt.Columns.Add(col);
                col = new DataColumn("Supervisorcode");
                custdt.Columns.Add(col);
                col = new DataColumn("Route_Name");
                custdt.Columns.Add(col);
                col = new DataColumn("Route_ID");
                custdt.Columns.Add(col);
                col = new DataColumn("STotmltr");
                custdt.Columns.Add(col);
                col = new DataColumn("PerDayQty");
                custdt.Columns.Add(col);
                col = new DataColumn("Sfkg");
                custdt.Columns.Add(col);
                col = new DataColumn("SSkg");
                custdt.Columns.Add(col);
                col = new DataColumn("Afat");
                custdt.Columns.Add(col);
                col = new DataColumn("Asnf");
                custdt.Columns.Add(col);
                col = new DataColumn("Arate");
                custdt.Columns.Add(col);
                col = new DataColumn("Mrate");
                custdt.Columns.Add(col);
                col = new DataColumn("DDays");
                custdt.Columns.Add(col);
                col = new DataColumn("FTransAMount");
                custdt.Columns.Add(col);


                object id1;
                id1 = "0";
                int idd1 = Convert.ToInt32(id1);

                //int count1 = dt.Rows.Count;
                //int checkcount = 0;

                foreach (DataRow dr1 in dt.Rows)
                {
                    object id;

                    id = dr1[1].ToString();
                    int idd = Convert.ToInt32(id);
                    custdr = custdt.NewRow();
                    int i = 0;
                    //checkcount++;
                    //if (count1 == checkcount)
                    //{
                    //}


                    if (idd1 == idd)
                    {
                       // custdr["SupervisorNamepcode"] = dr1["SupervisorName"].ToString();
                        custdr["SupervisorName"] = "";
                        custdr["Supervisorcode"] = dr1["Supervisorcode"].ToString();
                        custdr["Route_Name"] = dr1["Route_Name"].ToString();
                        custdr["Route_ID"] = dr1["Route_ID"].ToString();
                        custdr["STotmltr"] = dr1["STotmltr"].ToString();
                        custdr["PerDayQty"] = dr1["PerDayQty"].ToString();
                        custdr["Sfkg"] = dr1["Sfkg"].ToString();
                        custdr["SSkg"] = dr1["SSkg"].ToString();
                        custdr["Afat"] = dr1["Afat"].ToString();
                        custdr["Asnf"] = dr1["Asnf"].ToString();
                        custdr["Arate"] = dr1["Arate"].ToString();
                        custdr["Mrate"] = dr1["Mrate"].ToString();
                        custdr["DDays"] = dr1["DDays"].ToString();
                        custdr["FTransAMount"] = dr1["FTransAMount"].ToString();

                        custdt.Rows.Add(custdr);
                        id1 = dr1["Supervisorcode"].ToString();
                        idd1 = Convert.ToInt32(id1);
                        i++;
                    }
                    else
                    {
                        custdr["SupervisorName"] = dr1["SupervisorName"].ToString();
                        custdr["Supervisorcode"] = dr1["Supervisorcode"].ToString();
                        custdr["Route_Name"] = dr1["Route_Name"].ToString();
                        custdr["Route_ID"] = dr1["Route_ID"].ToString();
                        custdr["STotmltr"] = dr1["STotmltr"].ToString();
                        custdr["PerDayQty"] = dr1["PerDayQty"].ToString();
                        custdr["Sfkg"] = dr1["Sfkg"].ToString();
                        custdr["SSkg"] = dr1["SSkg"].ToString();
                        custdr["Afat"] = dr1["Afat"].ToString();
                        custdr["Asnf"] = dr1["Asnf"].ToString();
                        custdr["Arate"] = dr1["Arate"].ToString();
                        custdr["Mrate"] = dr1["Mrate"].ToString();
                        custdr["DDays"] = dr1["DDays"].ToString();
                        custdr["FTransAMount"] = dr1["FTransAMount"].ToString();



                        custdt.Rows.Add(custdr);
                        id1 = dr1["Supervisorcode"].ToString();
                        idd1 = Convert.ToInt32(id1);
                        i++;
                    }

                }

            }

            cr.SetDataSource(custdt);
            CrystalReportViewer1.ReportSource = cr;           

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