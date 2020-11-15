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
using CrystalDecisions.Shared;
public partial class Rpt_AccountsTotalAbstract : System.Web.UI.Page
{
    BLLroutmaster routmasterBL = new BLLroutmaster();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public int rid;
    SqlDataReader dr;
    //Admin Check Flag
    public int Falg = 0;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DateTime dtm = new DateTime();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    public static int roleid;
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
               // managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
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
                pcode = ddl_Plantcode.SelectedItem.Value;
                Bdate();

                loadrouteid();
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                if (chk_Allloan.Checked == true)
                {
                    lbl_RouteName.Visible = false;
                    ddl_RouteName.Visible = false;

                }

                //GetprourementIvoiceData();
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
             //   managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_Plantcode.SelectedItem.Value;
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                GetAccountTotalSummary();
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
    private void Bdate()
    {
        try
        {
            dr = null;
            dr = BLLBill.Billdate(ccode, ddl_Plantcode.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                    Falg = Convert.ToInt32(dr["ViewStatus"].ToString());
                  
                        Falg = 1;
                        Button1.Visible = true;
                        btn_Export.Visible = true;
                   
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

    private void loadrouteid()
    {
        try
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
        catch (Exception ex)
        {
        }
    }

    private void GetAccountTotalSummary()
    {
        try
        {
            if (chk_Buff.Checked == true)
            {
                
                cr.Load(Server.MapPath("PLANTOVERREPORTBUFF.rpt"));                
            }
            else
            {
               
                cr.Load(Server.MapPath("PLANTOVERREPORT.rpt"));
            }

            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];


            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            t1.Text = ccode + "_" + cname;
            t2.Text = ddl_Plantname.SelectedItem.Value;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            if (chk_Allloan.Checked == true)
            {
                //31-7-2014 work   str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1 ON f1.ARid=rr1.rrid";
                //lastworking 3-3-2015  
                str = "SELECT * FROM  (SELECT G1.Rid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  SUM(G1.Sfatkg) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround ,CAST(SUM(G1.SClaim) AS DECIMAL(18,2)) AS GSClaim FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST(ISNULL(prdelo.Scatamt, 0) AS DECIMAL(18, 2)), 0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS SNetAmt,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Ragent_id AS DAid ,(CAST((SUM(RBilladvance)) AS DECIMAL(18,2))) AS Billadv,(CAST((SUM(RAi)) AS DECIMAL(18,2))) AS Ai,(CAST((SUM(RFeed)) AS DECIMAL(18,2))) AS Feed,(CAST((SUM(Rcan)) AS DECIMAL(18,2))) AS can,(CAST((SUM(RRecovery)) AS DECIMAL(18,2))) AS Recovery,(CAST((SUM(Rothers)) AS DECIMAL(18,2))) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate between '" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "' AND RCompany_Code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY Ragent_id) AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid ) AS G1 GROUP BY G1.Rid ) AS Grf  LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1  ON Grf.Rid=rr1.rrid  ";
               
            }
            else
            {
                if (pcode == "160")
                {
                    str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Route_id<>8 GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Route_ID<>8) AS rr1 ON f1.ARid=rr1.rrid";
                }
                else
                {
                    str = "SELECT * FROM  (SELECT G1.Rid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  SUM(G1.Sfatkg) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround ,CAST(SUM(G1.SClaim) AS DECIMAL(18,2)) AS GSClaim FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS SNetAmt,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid ) AS G1 GROUP BY G1.Rid ) AS Grf  LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1  ON Grf.Rid=rr1.rrid  ";
                }
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        rid = rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        pcode = ddl_Plantcode.SelectedItem.Value;
        GetAccountTotalSummary();
    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        GetAccountTotalSummary();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_Plantcode.SelectedItem.Value;
            GetAccountTotalSummary();
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
            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "TotalAbstract.pdf";
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
            string MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "TotalAbstract.pdf";
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
    protected void chk_Allloan_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_Allloan.Checked == true)
        {
            lbl_RouteName.Visible = false;
            ddl_RouteName.Visible = false;
        }
        else
        {
            lbl_RouteName.Visible = false;
            ddl_RouteName.Visible = false;
        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        Bdate();
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        GetAccountTotalSummary();

    }
    protected void chk_Buff_CheckedChanged(object sender, EventArgs e)
    {

    }


}