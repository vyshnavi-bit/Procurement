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


public partial class PLANTOVERALLREPORT1 : System.Web.UI.Page
{
    BLLroutmaster routmasterBL = new BLLroutmaster();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DbHelper db = new DbHelper();
    public int rid;
    SqlDataReader dr;
    //Admin Check Flag
    public int Falg = 0;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DateTime dtm = new DateTime();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    string getcon = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //SqlConnection con = new SqlConnection();
    string gettid;
    string previousfromdate;
    string prevoistodate;
    public static int roleid;
    public static int buttonviewstatus;
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
              //  managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
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
                pcode = ddl_Plantcode.SelectedItem.Value;
                Bdate();
                try
                {
                    loadrouteid();
                    rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                    if (chk_Allloan.Checked == true)
                    {
                        lbl_RouteName.Visible = false;
                        ddl_RouteName.Visible = false;

                    }
                }
                catch
                {


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
                GetprourementIvoiceData();
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
            dr = Bllusers.LoadSinglePlantcode(ccode,pcode);
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

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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
                    //if (program.Guser_role >= program.Guser_PermissionId)
                    //{
                    //    Falg = 1;
                    //    Button1.Visible = true;
                    //    btn_Export.Visible = true;
                    //}
                    //else
                    //{
                    //    if (Falg == 0)
                    //    {
                    //        Button1.Visible = false;
                    //        btn_Export.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        Button1.Visible = true;
                    //        btn_Export.Visible = true;
                    //    }
                    //}


                    if (roleid > 3)
                    {
                        //  Falg = 1;
                        Button1.Visible = true;
                        btn_Export.Visible = true;


                    }
                    else
                    {
                        if (buttonviewstatus == 2)
                        {
                            Button1.Visible = true;
                            btn_Export.Visible = true;
                        }
                        else
                        {

                            Button1.Visible = false;
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

    private void GetprourementIvoiceData()
    {
        try
        {
            if (chk_Buff.Checked == true)
            {
                cr.Load(Server.MapPath("Report//Crpt_DTotalabstractBuff2.rpt"));
                //cr.Load(Server.MapPath("PLANTOVERREPORTBUFF.rpt"));                
            }
            else
            {
                cr.Load(Server.MapPath("Report//Crpt_DTotalabstract1.rpt"));
                //cr.Load(Server.MapPath("PLANTOVERREPORT.rpt"));
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
            DateTime prevMonth = dt1.AddDays(-1);
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
         //   SqlConnection con = new SqlConnection(getcon);
            string getqury = "SELECT  *   FROM   Bill_date where     Bill_todate='" + prevMonth + "' and  plant_code ='" + pcode + "' order by tid desc ";
            con.Open();
            SqlCommand cmd1 = new SqlCommand(getqury, con);
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    previousfromdate = dr["Bill_frmdate"].ToString();
                    prevoistodate = dr["Bill_todate"].ToString();

                }
            }
            else
            {

                WebMsgBox.Show("Please Check");

            }

            con.Close();
            con.Open();
         //   getbilldateid();
             
           //    str = "SELECT gproded.SproRid AS Rid,ISNULL(gproded.ScommAmt,0)  AS ScommAmt,ISNULL(gproded.Smltr,0) AS Smltr,ISNULL(gproded.Smkg,0) AS Smkg,ISNULL(gproded.AVGFAT,0) AS Afat,ISNULL(gproded.AVGSNF,0)AS Asnf,ISNULL(gproded.AVGRate,0) AS Arate,ISNULL(gproded.Avgclr,0) AS Aclr,ISNULL(gproded.Scans,0) AS Scans,ISNULL(gproded.SAmt,0) AS SAmt,ISNULL(gproded.AVGcrate,0) AS  Acrate,ISNULL(gproded.Sfatkg ,0) AS Sfkg,ISNULL(gproded.Ssnfkg,0) AS Sskg,ISNULL(gproded.Billadv,0) AS sBilladv,ISNULL(gproded.Ai,0) AS SAi,ISNULL(gproded.Feed,0) AS Sfeed,ISNULL(gproded.can,0) AS Scan,ISNULL(gproded.Recovery,0) AS SRecovery,ISNULL(gproded.others,0) AS Sothers,ISNULL(instamt,0) AS SInstAmt FROM (SELECT gpro.*,gded.* FROM (SELECT Route_id AS SproRid,(CAST(SUM(Comrate) AS DECIMAL(18,2))) AS ScommAmt,(CAST(SUM(Milk_ltr)  AS DECIMAL(18,2))) AS Smltr,(CAST(SUM(Milk_kg) AS DECIMAL(18,2))) AS Smkg,(CAST(AVG(FAT) AS DECIMAL(18,1))) AS AvgFat,(CAST(AVG(SNF)  AS DECIMAL(18,1))) AS AvgSnf,(CAST(AVG(Rate)  AS DECIMAL(18,2))) AS AvgRate,(CAST(AVG(Clr)  AS DECIMAL(18,2))) AS Avgclr,(CAST(SUM(NoofCans)  AS DECIMAL(18,2))) AS Scans,(CAST(SUM(Amount) AS DECIMAL(18,2))) AS SAmt,(CAST(AVG(ComRate)  AS DECIMAL(18,2))) AS Avgcrate,(CAST(SUM(fat_kg) AS DECIMAL(18,2))) AS Sfatkg,(CAST(SUM(snf_kg)  AS DECIMAL(18,2))) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '"+txt_FromDate.Text+"' AND '"+txt_ToDate.Text+"' AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"' GROUP BY Route_id ) AS gpro LEFT JOIN (SELECT  Route_id AS DRid ,(CAST(SUM(Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST(SUM(Ai) AS DECIMAL(18,2))) AS Ai,(CAST(SUM(Feed) AS DECIMAL(18,2))) AS Feed,(CAST(SUM(can) AS DECIMAL(18,2))) AS can,(CAST(SUM(Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST(SUM(others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '"+txt_FromDate.Text+"' AND '"+txt_ToDate.Text+"' AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"' GROUP BY Route_id ) AS gded ON gpro.SproRid=gded.DRid) AS gproded LEFT JOIN (SELECT Route_id AS LoRid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt FROM LoanDetails WHERE Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"'  AND Balance>0 GROUP BY Route_id )  AS glon ON  gproded.SproRid=glon.LoRid";

           // previous  code   str = "SELECT  F.*,ROU.RNAME,ROU.STATUS FROM (SELECT gproded.SproRid AS Rid,ISNULL(gproded.ScommAmt,0)  AS ScommAmt,ISNULL(gproded.Smltr,0) AS Smltr,ISNULL(gproded.Smkg,0) AS Smkg,ISNULL(gproded.AVGFAT,0) AS Afat,ISNULL(gproded.AVGSNF,0)AS Asnf,ISNULL(gproded.AVGRate,0) AS Arate,ISNULL(gproded.Avgclr,0) AS Aclr,ISNULL(gproded.Scans,0) AS Scans,ISNULL(gproded.SAmt,0) AS SAmt,ISNULL(gproded.AVGcrate,0) AS  Acrate,ISNULL(gproded.Sfatkg ,0) AS Sfkg,ISNULL(gproded.Ssnfkg,0) AS Sskg,ISNULL(gproded.Billadv,0) AS sBilladv,ISNULL(gproded.Ai,0) AS SAi,ISNULL(gproded.Feed,0) AS Sfeed,ISNULL(gproded.can,0) AS Scan,ISNULL(gproded.Recovery,0) AS SRecovery,ISNULL(gproded.others,0) AS Sothers,ISNULL(instamt,0) AS SInstAmt FROM (SELECT gpro.*,gded.* FROM (SELECT Route_id AS SproRid,(CAST(SUM(Comrate) AS DECIMAL(18,2))) AS ScommAmt,(CAST(SUM(Milk_ltr)  AS DECIMAL(18,2))) AS Smltr,(CAST(SUM(Milk_kg) AS DECIMAL(18,2))) AS Smkg,(CAST(AVG(FAT) AS DECIMAL(18,1))) AS AvgFat,(CAST(AVG(SNF)  AS DECIMAL(18,1))) AS AvgSnf,(CAST(AVG(Rate)  AS DECIMAL(18,2))) AS AvgRate,(CAST(AVG(Clr)  AS DECIMAL(18,2))) AS Avgclr,(CAST(SUM(NoofCans)  AS DECIMAL(18,2))) AS Scans,(CAST(SUM(Amount) AS DECIMAL(18,2))) AS SAmt,(CAST(AVG(ComRate)  AS DECIMAL(18,2))) AS Avgcrate,(CAST(SUM(fat_kg) AS DECIMAL(18,2))) AS Sfatkg,(CAST(SUM(snf_kg)  AS DECIMAL(18,2))) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='1' AND Plant_Code='156' GROUP BY Route_id ) AS gpro LEFT JOIN (SELECT  Route_id AS DRid ,(CAST(SUM(Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST(SUM(Ai) AS DECIMAL(18,2))) AS Ai,(CAST(SUM(Feed) AS DECIMAL(18,2))) AS Feed,(CAST(SUM(can) AS DECIMAL(18,2))) AS can,(CAST(SUM(Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST(SUM(others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '"+d1.Trim()+"' AND Company_Code='1' AND Plant_Code='156' GROUP BY Route_id ) AS gded ON gpro.SproRid=gded.DRid) AS gproded LEFT JOIN (SELECT Route_id AS LoRid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='156'  AND Balance>0 GROUP BY Route_id )  AS glon ON  gproded.SproRid=glon.LoRid ) AS F LEFT JOIN  (SELECT ROUTE_ID as RRID,ROUTE_NAME AS RNAME,STATUS FROM ROUTE_MASTER WHERE COMPANY_CODE=1 AND PLANT_CODE=156 ) as ROU ON F.RID=ROU.RRID ORDER BY F.RID  "; 
           // str = "SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM(SELECT * FROM(SELECT * FROM(SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS SproLEFT JOIN(SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"') AS dedu ON Spro.SproAid=dedu.DAid) AS prodedLEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid ";
           // OLD WORK str = "SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid";
            if (chk_Allloan.Checked == true)
            {
                str = "select *  from (select * from(select * from(SELECT *  FROM (select * from (select * from(select Route_id,Plant_code,SUM(Smkg) AS GSmkg,SUM(Smltr) AS GSmltr,SUM(SAmt) AS GSAmt,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate,SUM(SInsentAmt) AS GSInsentAmt,SUM(Scaramt) AS GScaramt,SUM(SSplBonus) AS GSSplBonus,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate,SUM(ClaimAount) AS GClaimAount,SUM(Sinstamt) AS GSinstamt,SUM(Billadv) AS GBilladv,SUM(Ai) AS GAi,SUM(Feed) AS GFeed,SUM(can) AS Gcan,SUM(Roundoff) AS GRoundoff,SUM(FLOOR(NetAmount)) AS GNetAmount,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf,SUM(Sfatkg) AS GSfatkg,SUM(SSnfkg) AS GSSnfkg,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate  from Paymentdata     Where Plant_code='" + pcode + "' AND Frm_date='" + d1 + "' AND To_date='" + d2 + "' GROUP BY Route_id,Plant_code) as t1  left join (SELECT  Plant_code AS Pcode,SUM(Smkg) AS GSmkg1,SUM(Smltr) AS GSmltr1,SUM(SAmt) AS GSAmt1,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate1,SUM(SInsentAmt) AS GSInsentAmt1,SUM(Scaramt) AS GScaramt1,SUM(SSplBonus) AS GSSplBonus1,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate1,SUM(ClaimAount) AS GClaimAount1,SUM(Sinstamt) AS GSinstamt1,SUM(Billadv) AS GBilladv1,SUM(Ai) AS GAi1,SUM(Feed) AS GFeed1,SUM(can) AS Gcan1,SUM(Roundoff) AS GRoundoff1,SUM(FLOOR(NetAmount)) AS GNetAmount1,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat1,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf1,SUM(Sfatkg) AS GSfatkg1,SUM(SSnfkg) AS GSSnfkg1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate1,(((DATEDIFF(d,Frm_date,To_date)+ 1))) as diffrentdate  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1 + "' AND To_date='" + d2 + "' GROUP BY Plant_code,Frm_date,To_date) as t2 on t1.Plant_code=t2.Pcode) as t3  left join (SELECT DISTINCT(Route_id) as  RRoute_id,Route_name FROM Paymentdata Where Plant_code='" + pcode + "') as t4  on t3.Route_id=t4.RRoute_id ) AS presenttable   left join (select count(tid) as sampleno,plant_code as plantcode from Procurement where Plant_Code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' group by  Plant_Code)  as sample1 on presenttable.Plant_code=sample1.plantcode) as presentcollection)as presentjointrans  left join(select  sum(ActualAmount) as presentactualamount,Plant_Code as presentplantcode from Truck_Present where   Plant_Code='" + pcode + "' and pdate between '" + d1 + "' and '" + d2 + "' group by Plant_Code )  as trans on presentjointrans.plantcode=trans.presentplantcode   ) as totpresent left join (select* from(SELECT *  FROM (select * from (select * from(select Route_id,Plant_code,SUM(Smkg) AS PGSmkg,SUM(Smltr) AS PGSmltr,SUM(SAmt) AS PGSAmt,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS PGMrate,SUM(SInsentAmt) AS PGSInsentAmt,SUM(Scaramt) AS PGScaramt,SUM(SSplBonus) AS PGSSplBonus,SUM(SAmt)+SUM(TotAdditions) AS PGTotAdditions,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS PGArate,SUM(ClaimAount) AS PGClaimAount,SUM(Sinstamt) AS PGSinstamt,SUM(Billadv) AS PGBilladv,SUM(Ai) AS PGAi,SUM(Feed) AS PGFeed,SUM(can) AS PGcan,SUM(Roundoff) AS PGRoundoff,SUM(FLOOR(NetAmount)) AS PGNetAmount,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS PGAfat,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS PGAsnf,SUM(Sfatkg) AS PGSfatkg,SUM(SSnfkg) AS PGSSnfkg,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS PGkgFatrate  from Paymentdata     Where Plant_code='" + pcode + "' AND Frm_date='" + previousfromdate + "' AND To_date='" + prevoistodate + "' GROUP BY Route_id,Plant_code) as t1  left join (SELECT  Plant_code AS Pcode,SUM(Smkg) AS PGSmkg1,SUM(Smltr) AS PGSmltr1,SUM(SAmt) AS PGSAmt1,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS PGMrate1,SUM(SInsentAmt) AS PGSInsentAmt1,SUM(Scaramt) AS PGScaramt1,SUM(SSplBonus) AS PGSSplBonus1,SUM(SAmt)+SUM(TotAdditions) AS PGTotAdditions1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS PGArate1,SUM(ClaimAount) AS PGClaimAount1,SUM(Sinstamt) AS PGSinstamt1,SUM(Billadv) AS PGBilladv1,SUM(Ai) AS PGAi1,SUM(Feed) AS PGFeed1,SUM(can) AS PGcan1,SUM(Roundoff) AS PGRoundoff1,SUM(FLOOR(NetAmount)) AS PGNetAmount1,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS PGAfat1,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS PGAsnf1,SUM(Sfatkg) AS PGSfatkg1,SUM(SSnfkg) AS PGSSnfkg1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS PGkgFatrate1,(((DATEDIFF(d,Frm_date,To_date)+ 1))) as Pdiffrentdate  FROM Paymentdata where Plant_code='" + pcode + "' AND Frm_date='" + previousfromdate + "' AND To_date='" + prevoistodate + "' GROUP BY Plant_code,Frm_date,To_date) as t2 on t1.Plant_code=t2.Pcode) as t3  left join (SELECT DISTINCT(Route_id) as PRoute_id ,Route_name as PRoute_name  FROM Paymentdata Where Plant_code='" + pcode + "') as t4  on t3.Route_id=t4.PRoute_id  ) as previoustable  left join (select count(tid) as psampleno,plant_code as ppplant_code from Procurement where Plant_Code='" + pcode + "' and prdate between '" + previousfromdate + "' and '" + prevoistodate + "' group by  Plant_Code)  as sample2 on previoustable.plant_code=sample2. ppplant_code) as previousjointrans   left join(select  isnull(sum(ActualAmount),0) as previousactualamount,Plant_Code as ppcode from Truck_Present where   Plant_code='" + pcode + "' AND pdate    between '" + previousfromdate + "' AND  '" + prevoistodate + "' group by Plant_Code )  as trans1 on previousjointrans.ppplant_code=trans1.ppcode) as totprevious on totpresent.Plant_code=totprevious.Plant_code  ORDER BY RAND(totpresent.Route_id) ASC ";
            }
            else
            {
                if (pcode == "160")
                {
                    str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Route_id<>8 GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Route_ID<>8) AS rr1 ON f1.ARid=rr1.rrid";
                }
                else
                {
                    //  str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1 ON f1.ARid=rr1.rrid"; isnull((CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2))),0) AS GSinstamt
                  //  str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,isnull((CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)),0) AS GScommAmt,isnull((CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)),0) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1 ON f1.ARid=rr1.rrid";
                    str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt, isnull((CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2))),0) AS GScommAmt,isnull((CAST(SUM(G1.Scatamt) AS DECIMAL(18,2))),0) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='157' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1 ON f1.ARid=rr1.rrid";
                }
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        rid = rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        pcode = ddl_Plantcode.SelectedItem.Value;
        GetprourementIvoiceData();
    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);       
        GetprourementIvoiceData();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_Plantcode.SelectedItem.Value;
            GetprourementIvoiceData();
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
      //  Bdate();
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        GetprourementIvoiceData();
       
    }
    protected void chk_Buff_CheckedChanged(object sender, EventArgs e)
    {

    }


    public void getbilldateid()
    {
           //DateTime dt1 = new DateTime();
           // DateTime dt2 = new DateTime();

           // dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
           // dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

           // string d1 = dt1.ToString("MM/dd/yyyy");
           // string d2 = dt2.ToString("MM/dd/yyyy");
       


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