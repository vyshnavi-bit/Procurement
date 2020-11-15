using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
public partial class TotalSummary : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper db = new DbHelper();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    //Admin Check Flag
    public int Falg = 0;
    public static int roleid;
    public static int buttonviewstatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["username"] != null) && (Session["password"] != null))
            {
                pcode = (Session["Plant_code"]).ToString();
                ccode = Session["CompanyCode"].ToString();
                cname = Session["Company_Name"].ToString();
                pname = Session["Plant_Name"].ToString();
                roleid = Convert.ToInt32(Session["roleid"].ToString());
                dtm = System.DateTime.Now;
              //  managmobNo = Session["managmobNo"].ToString();
                getupdatestatus();
                dtm = System.DateTime.Now;
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid!=9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();
                }
                Bdate();
                pcode = ddl_plantcode.SelectedItem.Value;
                pname = ddl_Plantname.SelectedItem.Value;

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
        if (IsPostBack == true)
        {

            if ((Session["username"] != null) && (Session["password"] != null))
            {
                pcode = (Session["Plant_code"]).ToString();
                ccode = Session["CompanyCode"].ToString();
                cname = Session["Company_Name"].ToString();
                pname = Session["Plant_Name"].ToString();
                roleid = Convert.ToInt32(Session["roleid"].ToString());
                dtm = System.DateTime.Now;
                pname = ddl_Plantname.SelectedItem.Value;
                getupdatestatus();
              //  managmobNo = Session["managmobNo"].ToString();
                Totalsummary1();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }

    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
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
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
                ddl_plantcode.Items.Add("--Select Plantcode--");
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
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode,pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

     public void getupdatestatus()
     {

         string str = "";
         str = "Select  *    from  adminapproval   where plant_code='" + pcode + "'";
         SqlConnection con = new SqlConnection();
         con = db.GetConnection();
         SqlCommand cmd = new SqlCommand(str, con);
         SqlDataReader dr = cmd.ExecuteReader();
         if (dr.HasRows)
         {
             while (dr.Read())
             {

                 buttonviewstatus = Convert.ToInt16(dr["buttonviewstatus"].ToString());



             }



         }


     }

    private void Bdate()
    {
        try
        {
            dr = null;
            dr = BLLBill.Billdate(ccode, ddl_plantcode.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                    Falg = Convert.ToInt32(dr["ViewStatus"].ToString());
                    //if (program.Guser_role >= program.Guser_PermissionId)
                    //{
                    //    Falg = 1;
                    //    btn_ok.Visible = true;
                    //    btn_Export.Visible = true;

                    //}
                    //else
                    //{
                    //    if (Falg == 0)
                    //    {
                    //        btn_ok.Visible = false;
                    //        btn_Export.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        btn_ok.Visible = true;
                    //        btn_Export.Visible = true;
                    //    }
                    //}

                    if (roleid > 3)
                    {
                        //  Falg = 1;
                        btn_ok.Visible = true;
                     //   btn_Export.Visible = true;


                    }
                    else
                    {
                        if (buttonviewstatus == 2)
                        {
                            btn_ok.Visible = true;
                          //  btn_Export.Visible = true;
                        }
                        else
                        {

                            btn_ok.Visible = false;
                            btn_Export.Visible = false;
                        }
                    }



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


    private void Totalsummary1()
    {
        try
        {
            //cr.Load(Server.MapPath("Crpt_TotalSummary.rpt"));        
            cr.Load(Server.MapPath("Report\\Crpt_DBankTotalsummary.rpt"));
            cr.SetDatabaseLogon("223.196.32.28", "AMPS");
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
            t1.Text = ccode + "_" + cname;
            t2.Text = ddl_Plantname.SelectedItem.Value;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

         // str = "Select * from (SELECT SUM(SNetAmt) AS TSAmount,Bank_Id FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid, ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)+ ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,ISNULL(cart.Bank_Id,0) AS Bank_Id,ISNULL(cart.Payment_mode,0) AS Payment_mode,ISNULL(cart.Agent_AccountNo,0) AS Agent_AccountNo,ISNULL(cart.Ifsc_Code,0) AS Ifsc_Code ,ISNULL(cart.Agent_Name,0) AS Agent_Name,ISNULL(cart.Bank_Name,0) AS Bank_Name FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.ToString() + "' and '" + d2.ToString() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.ToString() + "' and '" + d2.ToString() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT * FROM (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Ifsc_Code,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS carts LEFT JOIN (SELECT Bank_id AS Bdbid,Bank_Name FROM BANK_Details) AS BD ON carts.Bank_Id=BD.Bdbid ) AS cart ON  prdelo.SproAid=cart.cartAid ) AS t1 GROUP BY t1.Bank_Id ) AS tt1 LEFT JOIN (SELECT Bank_id AS BdBid,Bank_Name FROM Bank_Details where Company_code='" + ccode + "') AS tt2 ON tt1.Bank_Id=tt2.BdBid ORDER BY tt2.BdBid";
            // loan recovery
            //Last working 27-02-2015   str = "SELECT GrBank.GSNetAmt AS TSAmount,GrBank.Bank_Id AS Bank_Id,GrBank.Bank_Id AS BdBid,ISNULL(tt2.Bank_Name,'CASH') AS Bank_Name FROM (SELECT G1.Bank_Id,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround ,CAST(SUM(G1.SClaim) AS DECIMAL(18,2)) AS GSClaim FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) +(ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) +ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.ToString() + "' and '" + d2.ToString() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.ToString() + "' and '" + d2.ToString() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.ToString() + "' and '" + d2.ToString() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid ) AS G1 GROUP BY G1.Bank_Id ) AS GrBank LEFT JOIN  (SELECT Bank_id AS BdBid,Bank_Name FROM Bank_Details where Company_code='" + ccode + "') AS tt2 ON GrBank.Bank_Id=tt2.BdBid ORDER BY tt2.BdBid";
            str = "SELECT F.*,pay.Bank_name FROM " +
" (SELECT Bank_id,SUM(FLOOR(ISNULL(NetAmount,0))) AS NetAmount FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString().Trim() + "' AND To_date='" + d2.ToString().Trim() + "' GROUP BY Bank_id) AS F " +
" INNER JOIN " +
" (SELECT DISTINCT(Bank_id) AS Bid,ISNULL(Bank_name,'CASH') AS Bank_name FROM Paymentdata WHERE Plant_Code='" + pcode + "') AS pay ON F.Bank_id=pay.Bid  ORDER BY pay.Bid";
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("NetAmount"));
                GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[2].Text = milkkg.ToString("N2");
                GridView1.FooterRow.Cells[2].Font.Bold = true;
                GridView1.FooterRow.Cells[1].Text = "Total";
                GridView1.FooterRow.Cells[1].Font.Bold = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
         

            //cr.SetDataSource(dt);
            //CrystalReportViewer1.ReportSource = cr;
        }
        catch (Exception ex)
        {

            WebMsgBox.Show(ex.ToString());
        }

    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        pcode = ddl_plantcode.SelectedItem.Value;
        Totalsummary1();

        ////pdf convertion
        //MemoryStream oStream; // using System.IO
        //oStream = (MemoryStream)
        //cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/pdf";
        //Response.BinaryWrite(oStream.ToArray());
        //Response.End();
        
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_plantcode.SelectedItem.Value;
            Totalsummary1();

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

            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "TotalSummary.pdf";

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

            MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "TotalSummary.pdf";

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
   
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_plantcode.SelectedItem.Value;
        Bdate();
        Totalsummary1();
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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Total Summary Report:" + ddl_Plantname.SelectedItem.Text + ":Bill Date For:" + txt_FromDate.Text +" To:"+txt_ToDate.Text;
            HeaderCell2.ColumnSpan = 3;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "0")
            {
                e.Row.Cells[1].Text = "Cash";
            }

            string str = e.Row.Cells[2].Text + ".00";

            e.Row.Cells[2].Text = str;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

        }

       
    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
}