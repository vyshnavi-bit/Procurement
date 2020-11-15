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
using System.Drawing;
using System.IO;
using System.Text;


public partial class PLANTOVERALLREPORT : System.Web.UI.Page
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
	DataTable dttt = new DataTable();
	DataTable dt = new DataTable();
	DbHelper dbaccess = new DbHelper();
	public int Plantmilktype = 0;
	CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    public static int roleid;
    public static int buttonviewstatus;
    string d1;
    string d2;
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
                getupdatestatus();
			//	managmobNo = Session["managmobNo"].ToString();
				dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                if (roleid < 3)
				{
					loadsingleplant();
				}
                if ((roleid >= 3) && (roleid != 9))
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
                    miltype.Visible = false;
                    mtype.Visible = false;
                }
                catch
                {

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
                //	pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //	managmobNo = Session["managmobNo"].ToString();
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
                    
                    if (roleid > 3)
                    {
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


    public void getupdatestatus()
    {

        string str = "";
        str = "Select  *    from  adminapproval   where plant_code='" + pcode + "'";
        SqlConnection con = new SqlConnection();
        con = dbaccess.GetConnection();
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

	private void GetprourementIvoiceData()
	{
		try
		{
			//if (chk_Buff.Checked == true)
			//{
			//    cr.Load(Server.MapPath("Report//Crpt_DTotalabstractBuff.rpt"));
			//    //cr.Load(Server.MapPath("PLANTOVERREPORTBUFF.rpt"));                
			//}
			//else
			//{
			//    cr.Load(Server.MapPath("Report//Crpt_DTotalabstract.rpt"));
			//    //cr.Load(Server.MapPath("PLANTOVERREPORT.rpt"));
			//}
			pcode = ddl_Plantcode.SelectedItem.Value;
			Plantmilktype = dbaccess.GetPlantMilktype(pcode);
            if (Plantmilktype == 2)
            {
                cr.Load(Server.MapPath("Report//Crpt_DTotalabstractBuff.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Report//Crpt_DTotalabstract.rpt"));
            }

            if ((pcode == "164") && (miltype.Text == "2"))
            {
                cr.Load(Server.MapPath("Report//Crpt_DTotalabstractBuff.rpt"));
            }
            else
            {
                if (pcode == "164")
                {
                    cr.Load(Server.MapPath("Report//Crpt_DTotalabstract.rpt"));
                }


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
            //string d1;
            //string d2;
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

			//    str = "SELECT gproded.SproRid AS Rid,ISNULL(gproded.ScommAmt,0)  AS ScommAmt,ISNULL(gproded.Smltr,0) AS Smltr,ISNULL(gproded.Smkg,0) AS Smkg,ISNULL(gproded.AVGFAT,0) AS Afat,ISNULL(gproded.AVGSNF,0)AS Asnf,ISNULL(gproded.AVGRate,0) AS Arate,ISNULL(gproded.Avgclr,0) AS Aclr,ISNULL(gproded.Scans,0) AS Scans,ISNULL(gproded.SAmt,0) AS SAmt,ISNULL(gproded.AVGcrate,0) AS  Acrate,ISNULL(gproded.Sfatkg ,0) AS Sfkg,ISNULL(gproded.Ssnfkg,0) AS Sskg,ISNULL(gproded.Billadv,0) AS sBilladv,ISNULL(gproded.Ai,0) AS SAi,ISNULL(gproded.Feed,0) AS Sfeed,ISNULL(gproded.can,0) AS Scan,ISNULL(gproded.Recovery,0) AS SRecovery,ISNULL(gproded.others,0) AS Sothers,ISNULL(instamt,0) AS SInstAmt FROM (SELECT gpro.*,gded.* FROM (SELECT Route_id AS SproRid,(CAST(SUM(Comrate) AS DECIMAL(18,2))) AS ScommAmt,(CAST(SUM(Milk_ltr)  AS DECIMAL(18,2))) AS Smltr,(CAST(SUM(Milk_kg) AS DECIMAL(18,2))) AS Smkg,(CAST(AVG(FAT) AS DECIMAL(18,1))) AS AvgFat,(CAST(AVG(SNF)  AS DECIMAL(18,1))) AS AvgSnf,(CAST(AVG(Rate)  AS DECIMAL(18,2))) AS AvgRate,(CAST(AVG(Clr)  AS DECIMAL(18,2))) AS Avgclr,(CAST(SUM(NoofCans)  AS DECIMAL(18,2))) AS Scans,(CAST(SUM(Amount) AS DECIMAL(18,2))) AS SAmt,(CAST(AVG(ComRate)  AS DECIMAL(18,2))) AS Avgcrate,(CAST(SUM(fat_kg) AS DECIMAL(18,2))) AS Sfatkg,(CAST(SUM(snf_kg)  AS DECIMAL(18,2))) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '"+txt_FromDate.Text+"' AND '"+txt_ToDate.Text+"' AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"' GROUP BY Route_id ) AS gpro LEFT JOIN (SELECT  Route_id AS DRid ,(CAST(SUM(Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST(SUM(Ai) AS DECIMAL(18,2))) AS Ai,(CAST(SUM(Feed) AS DECIMAL(18,2))) AS Feed,(CAST(SUM(can) AS DECIMAL(18,2))) AS can,(CAST(SUM(Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST(SUM(others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '"+txt_FromDate.Text+"' AND '"+txt_ToDate.Text+"' AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"' GROUP BY Route_id ) AS gded ON gpro.SproRid=gded.DRid) AS gproded LEFT JOIN (SELECT Route_id AS LoRid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt FROM LoanDetails WHERE Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"'  AND Balance>0 GROUP BY Route_id )  AS glon ON  gproded.SproRid=glon.LoRid";
            
			// previous  code   str = "SELECT  F.*,ROU.RNAME,ROU.STATUS FROM (SELECT gproded.SproRid AS Rid,ISNULL(gproded.ScommAmt,0)  AS ScommAmt,ISNULL(gproded.Smltr,0) AS Smltr,ISNULL(gproded.Smkg,0) AS Smkg,ISNULL(gproded.AVGFAT,0) AS Afat,ISNULL(gproded.AVGSNF,0)AS Asnf,ISNULL(gproded.AVGRate,0) AS Arate,ISNULL(gproded.Avgclr,0) AS Aclr,ISNULL(gproded.Scans,0) AS Scans,ISNULL(gproded.SAmt,0) AS SAmt,ISNULL(gproded.AVGcrate,0) AS  Acrate,ISNULL(gproded.Sfatkg ,0) AS Sfkg,ISNULL(gproded.Ssnfkg,0) AS Sskg,ISNULL(gproded.Billadv,0) AS sBilladv,ISNULL(gproded.Ai,0) AS SAi,ISNULL(gproded.Feed,0) AS Sfeed,ISNULL(gproded.can,0) AS Scan,ISNULL(gproded.Recovery,0) AS SRecovery,ISNULL(gproded.others,0) AS Sothers,ISNULL(instamt,0) AS SInstAmt FROM (SELECT gpro.*,gded.* FROM (SELECT Route_id AS SproRid,(CAST(SUM(Comrate) AS DECIMAL(18,2))) AS ScommAmt,(CAST(SUM(Milk_ltr)  AS DECIMAL(18,2))) AS Smltr,(CAST(SUM(Milk_kg) AS DECIMAL(18,2))) AS Smkg,(CAST(AVG(FAT) AS DECIMAL(18,1))) AS AvgFat,(CAST(AVG(SNF)  AS DECIMAL(18,1))) AS AvgSnf,(CAST(AVG(Rate)  AS DECIMAL(18,2))) AS AvgRate,(CAST(AVG(Clr)  AS DECIMAL(18,2))) AS Avgclr,(CAST(SUM(NoofCans)  AS DECIMAL(18,2))) AS Scans,(CAST(SUM(Amount) AS DECIMAL(18,2))) AS SAmt,(CAST(AVG(ComRate)  AS DECIMAL(18,2))) AS Avgcrate,(CAST(SUM(fat_kg) AS DECIMAL(18,2))) AS Sfatkg,(CAST(SUM(snf_kg)  AS DECIMAL(18,2))) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='1' AND Plant_Code='156' GROUP BY Route_id ) AS gpro LEFT JOIN (SELECT  Route_id AS DRid ,(CAST(SUM(Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST(SUM(Ai) AS DECIMAL(18,2))) AS Ai,(CAST(SUM(Feed) AS DECIMAL(18,2))) AS Feed,(CAST(SUM(can) AS DECIMAL(18,2))) AS can,(CAST(SUM(Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST(SUM(others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '"+d1.Trim()+"' AND Company_Code='1' AND Plant_Code='156' GROUP BY Route_id ) AS gded ON gpro.SproRid=gded.DRid) AS gproded LEFT JOIN (SELECT Route_id AS LoRid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='156'  AND Balance>0 GROUP BY Route_id )  AS glon ON  gproded.SproRid=glon.LoRid ) AS F LEFT JOIN  (SELECT ROUTE_ID as RRID,ROUTE_NAME AS RNAME,STATUS FROM ROUTE_MASTER WHERE COMPANY_CODE=1 AND PLANT_CODE=156 ) as ROU ON F.RID=ROU.RRID ORDER BY F.RID  "; 
			// str = "SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM(SELECT * FROM(SELECT * FROM(SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS SproLEFT JOIN(SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"') AS dedu ON Spro.SproAid=dedu.DAid) AS prodedLEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='"+ccode+"' AND Plant_Code='"+pcode+"') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid ";
			// OLD WORK str = "SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(prdelo.Scatamt,0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid";
			if (chk_Allloan.Checked == true)
			{
				//31-7-2014 work   str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1 ON f1.ARid=rr1.rrid";
				//lastworking 3-3-2015  str = "SELECT * FROM  (SELECT G1.Rid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround ,CAST(SUM(G1.SClaim) AS DECIMAL(18,2)) AS GSClaim FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) +(ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) +ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(ROUND(fat_kg,2,1)) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(ROUND(snf_kg,2,1)) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid ) AS G1 GROUP BY G1.Rid ) AS Grf  LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1  ON Grf.Rid=rr1.rrid  ";
                str = " SELECT F.*,Rout.Route_name FROM " +
" (SELECT Gpay.*,Gpay1.* FROM " +
" (SELECT  Route_id,Plant_code,SUM(Smkg) AS GSmkg,SUM(Smltr) AS GSmltr,SUM(SAmt) AS GSAmt,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate,SUM(SInsentAmt) AS GSInsentAmt,SUM(Scaramt) AS GScaramt,SUM(SSplBonus) AS GSSplBonus,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate,SUM(ClaimAount) AS GClaimAount,SUM(Sinstamt) AS GSinstamt,SUM(Billadv) AS GBilladv,SUM(Ai) AS GAi,SUM(Feed) AS GFeed,SUM(can) AS Gcan,SUM(Recovery) AS GRecv,SUM(others) AS GOthr,SUM(Roundoff) AS GRoundoff,SUM(FLOOR(NetAmount)) AS GNetAmount,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf,SUM(Sfatkg) AS GSfatkg,SUM(SSnfkg) AS GSSnfkg,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString().Trim() + "' AND To_date='" + d2.ToString().Trim() + "'   GROUP BY Route_id,Plant_code) AS Gpay " +
"  LEFT JOIN  " +
" (SELECT  Plant_code AS Pcode,SUM(Smkg) AS GSmkg1,SUM(Smltr) AS GSmltr1,SUM(SAmt) AS GSAmt1,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate1,SUM(SInsentAmt) AS GSInsentAmt1,SUM(Scaramt) AS GScaramt1,SUM(SSplBonus) AS GSSplBonus1,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate1,SUM(ClaimAount) AS GClaimAount1,SUM(Sinstamt) AS GSinstamt1,SUM(Billadv) AS GBilladv1,SUM(Ai) AS GAi1,SUM(Feed) AS GFeed1,SUM(can) AS Gcan1,SUM(Recovery) AS GRecv1,SUM(others) AS GOthr1,SUM(Roundoff) AS GRoundoff1,SUM(FLOOR(NetAmount)) AS GNetAmount1,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat1,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf1,SUM(Sfatkg) AS GSfatkg1,SUM(SSnfkg) AS GSSnfkg1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate1  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString().Trim() + "' AND To_date='" + d2.ToString().Trim() + "'  GROUP BY Plant_code) AS Gpay1 ON Gpay.Plant_code=Gpay1.Pcode ) AS F " +
" INNER JOIN  " +
" (SELECT DISTINCT(Route_id),Route_name FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString().Trim() + "' AND To_date='" + d2.ToString().Trim() + "'    ) AS Rout ON F.Route_id=Rout.Route_id ORDER BY Route_id  ";

                //8-8-2016 above temp hide
//                str = " SELECT F.*,Rout.Route_name FROM " +
//" (SELECT Gpay.*,Gpay1.* FROM " +
//" (SELECT  Route_id,Plant_code,SUM(Smkg) AS GSmkg,SUM(Smltr) AS GSmltr,SUM(SAmt) AS GSAmt,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate,SUM(SInsentAmt) AS GSInsentAmt,SUM(Scaramt) AS GScaramt,SUM(SSplBonus) AS GSSplBonus,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate,SUM(ClaimAount) AS GClaimAount,SUM(Sinstamt) AS GSinstamt,SUM(Billadv) AS GBilladv,SUM(Ai) AS GAi,SUM(Feed) AS GFeed,SUM(can) AS Gcan,SUM(Recovery) AS GRecv,SUM(others) AS GOthr,SUM(Roundoff) AS GRoundoff,SUM(FLOOR(NetAmount)) AS GNetAmount,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf,SUM(Sfatkg) AS GSfatkg,SUM(SSnfkg) AS GSSnfkg,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date between'" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "'   GROUP BY Route_id,Plant_code) AS Gpay " +
//"  LEFT JOIN  " +
//" (SELECT  Plant_code AS Pcode,SUM(Smkg) AS GSmkg1,SUM(Smltr) AS GSmltr1,SUM(SAmt) AS GSAmt1,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate1,SUM(SInsentAmt) AS GSInsentAmt1,SUM(Scaramt) AS GScaramt1,SUM(SSplBonus) AS GSSplBonus1,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate1,SUM(ClaimAount) AS GClaimAount1,SUM(Sinstamt) AS GSinstamt1,SUM(Billadv) AS GBilladv1,SUM(Ai) AS GAi1,SUM(Feed) AS GFeed1,SUM(can) AS Gcan1,SUM(Recovery) AS GRecv1,SUM(others) AS GOthr1,SUM(Roundoff) AS GRoundoff1,SUM(FLOOR(NetAmount)) AS GNetAmount1,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat1,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf1,SUM(Sfatkg) AS GSfatkg1,SUM(SSnfkg) AS GSSnfkg1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate1  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date between'" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "'  GROUP BY Plant_code) AS Gpay1 ON Gpay.Plant_code=Gpay1.Pcode ) AS F " +
//" INNER JOIN  " +
//" (SELECT DISTINCT(Route_id),Route_name FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date   between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'    ) AS Rout ON F.Route_id=Rout.Route_id ORDER BY Route_id  ";




                if (pcode == "164")
                {


                    str = " SELECT F.*,Rout.Route_name FROM " +
" (SELECT Gpay.*,Gpay1.* FROM " +
" (SELECT  Route_id,Plant_code,SUM(Smkg) AS GSmkg,SUM(Smltr) AS GSmltr,SUM(SAmt) AS GSAmt,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate,SUM(SInsentAmt) AS GSInsentAmt,SUM(Scaramt) AS GScaramt,SUM(SSplBonus) AS GSSplBonus,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate,SUM(ClaimAount) AS GClaimAount,SUM(Sinstamt) AS GSinstamt,SUM(Billadv) AS GBilladv,SUM(Ai) AS GAi,SUM(Feed) AS GFeed,SUM(can) AS Gcan,SUM(Recovery) AS GRecv,SUM(others) AS GOthr,SUM(Roundoff) AS GRoundoff,SUM(FLOOR(NetAmount)) AS GNetAmount,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf,SUM(Sfatkg) AS GSfatkg,SUM(SSnfkg) AS GSSnfkg,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString().Trim() + "' AND To_date='" + d2.ToString().Trim() + "'   and milktype='" + miltype.Text + "'  GROUP BY Route_id,Plant_code) AS Gpay " +
"  LEFT JOIN  " +
" (SELECT  Plant_code AS Pcode,SUM(Smkg) AS GSmkg1,SUM(Smltr) AS GSmltr1,SUM(SAmt) AS GSAmt1,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate1,SUM(SInsentAmt) AS GSInsentAmt1,SUM(Scaramt) AS GScaramt1,SUM(SSplBonus) AS GSSplBonus1,SUM(SAmt)+SUM(TotAdditions) AS GTotAdditions1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate1,SUM(ClaimAount) AS GClaimAount1,SUM(Sinstamt) AS GSinstamt1,SUM(Billadv) AS GBilladv1,SUM(Ai) AS GAi1,SUM(Feed) AS GFeed1,SUM(can) AS Gcan1,SUM(Recovery) AS GRecv1,SUM(others) AS GOthr1,SUM(Roundoff) AS GRoundoff1,SUM(FLOOR(NetAmount)) AS GNetAmount1,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAfat1,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,2)) AS GAsnf1,SUM(Sfatkg) AS GSfatkg1,SUM(SSnfkg) AS GSSnfkg1,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate1  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString().Trim() + "' AND To_date='" + d2.ToString().Trim() + "' and milktype='" + miltype.Text + "'   GROUP BY Plant_code) AS Gpay1 ON Gpay.Plant_code=Gpay1.Pcode ) AS F " +
" INNER JOIN  " +
" (SELECT DISTINCT(Route_id),Route_name FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString().Trim() + "' AND To_date='" + d2.ToString().Trim() + "'   and milktype='" + miltype.Text + "'  ) AS Rout ON F.Route_id=Rout.Route_id ORDER BY Route_id  ";



                }





			}
			else
			{
				if (pcode == "160")
				{
					str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Route_id<>8 GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and Route_ID<>8) AS rr1 ON f1.ARid=rr1.rrid";
				}
				else
				{
					str = "SELECT * FROM (SELECT  G1.ARid,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround FROM (SELECT cart.ARid,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt, ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt,ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,FLOOR ((ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)))  AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )-( FLOOR( (ISNULL(prdelo.SAmt,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.ScommAmt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(CartageAmount) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY Agent_id) AS Lon ON proded.SproAid=Lon.LoAid) AS prdelo INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid) AS G1 GROUP BY  G1.ARid) AS f1 LEFT JOIN (SELECT Route_ID as rrid,Route_Name AS Rname FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS rr1 ON f1.ARid=rr1.rrid";
				}
			}
			SqlCommand cmd = new SqlCommand();
			SqlDataAdapter da = new SqlDataAdapter(str, con);
			//DataTable dt = new DataTable();
            dt.Rows.Clear();
			da.Fill(dt);




			cr.SetDataSource(dt);
			CrystalReportViewer1.ReportSource = cr;

			//if (chk_print.Checked == true)
			//{
			//    cr.PrintToPrinter(1, true, 0, 0);
			//}
			con.Close();

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

	public void readdata()
	{

		dttt.Columns.AddRange(new DataColumn[28] { new DataColumn("Plant_code"), new DataColumn("Plant_Name"), new DataColumn("FromDate"), new DataColumn("ToDate"), new DataColumn("Route_Id"), new DataColumn("Route_Name"), new DataColumn("MilkKgs"), new DataColumn("MilkLtr"), new DataColumn("Amount"), new DataColumn("Rate"), new DataColumn("Commrate"), new DataColumn("CartAmt"), new DataColumn("SplBon"), new DataColumn("TotAdd"), new DataColumn("AvgRate"), new DataColumn("Claim"), new DataColumn("BillAdv"), new DataColumn("Material"), new DataColumn("Feed"), new DataColumn("Can"), new DataColumn("Dpu"), new DataColumn("LoanAmt"), new DataColumn("Round"), new DataColumn("NetAmount"), new DataColumn("AvgFat"), new DataColumn("Avgsnf"), new DataColumn("Ts"), new DataColumn("R/TS") });
		foreach (DataRow row in dt.Rows)
		{


			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();

			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

		


			/*pcode */
			string a1 = row[1].ToString();
			string[] plant = ddl_Plantname.Text.Split('_');
			/*PNAME */
			string a2 = plant[1].ToString();

			/*DATE */
			string a3 =  dt1.ToString("dd/MM/yyyy");
		 
			/*DATE */
			string a4 = dt2.ToString("dd/MM/yyyy");
			/*ROUTEID */
			string a6 = row[0].ToString();
			/*ROUTENAME */
			string a7 = row[51].ToString();
			/*Milkg */
			string a8 = row[2].ToString();    
			/*mltr */
			string a9 = row[3].ToString();
			/*Amount */
			string a10 = row[4].ToString();
			/*Rate */
			string a11 = row[5].ToString();
			/*Comm */
			string a12 = row[6].ToString();
			/*cart */
			string a13 = row[7].ToString();
			/*splbon */
			string a14 = row[8].ToString();
			/*totadd */
			string a15 = row[9].ToString();
			/*TRate */
			string a16 = row[10].ToString();
			/*claim */
			string a17 = row[11].ToString();
			/*Badvance */
			string a29 = row[13].ToString();
			/*Materi */
			string a18 = row[14].ToString();
			/*feed */
			string a19 = row[15].ToString();
			/*can */
			string a20 = row[16].ToString();
			/*Dpu */
			string a21 = row[17].ToString();
			/*Loanamt */
			string a22 = row[18].ToString();
			/*Round */
			string a23 = row[19].ToString();
			/*Netamount */
			string a24 = row[20].ToString();
			/*avgfat */
			string a25 = row[21].ToString();
			/*avgsnf */
			string a26 = row[22].ToString();
			///*ts */ string a27 = row[23].ToString();
			/*r/ts */
			// string a28 = row[24].ToString();
			double avff, asnf, totts, avgrate, rts;
			avff = Convert.ToDouble(a25);
			asnf = Convert.ToDouble(a26);
			avgrate = Convert.ToDouble(a16);
			totts = avff + asnf;
			rts = avgrate / totts;

			/*ts */
			string a27 = totts.ToString();
			/*r/ts */
			string a28 = rts.ToString();


			string[] a30 = a28.Split('.');
			int a31 = Convert.ToInt32(a30[0]);
			int a32=Convert.ToInt32( a30[1].Substring(0,2));

			string a33 =  Convert.ToString( a31 ) + '.'  + (a32);
			dttt.Rows.Add(a1, a2, a3, a4, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a29, a18, a19, a20, a21, a22, a23, a24, a25, a26, a27, a33);
		}

		GridView1.DataSource = dttt;
		GridView1.DataBind();



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
           // miltype.Visible = false;
		}
		else
		{
			lbl_RouteName.Visible = false;
			ddl_RouteName.Visible = false;
           // miltype.Visible = true;
		}
	}
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		pcode = ddl_Plantcode.SelectedItem.Value;
		Bdate();
		rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
		GetprourementIvoiceData();
        if (pcode == "164")
        {
            miltype.Visible = true;
            mtype.Visible = true;
        }
	}
	protected void chk_Buff_CheckedChanged(object sender, EventArgs e)
	{

	}



	protected void ExportToExcel_Click(object sender, EventArgs e)
	{

		readdata();

		Response.Clear();
		Response.Buffer = true;
		Response.ClearContent();
		Response.ClearHeaders();
		Response.Charset = "";
		//string FileName = "Tallyreport" + DateTime.Now + ".xls";
		string FileName = "Tallyreport"  + ddl_Plantname.Text + DateTime.Now + ".xls";  
		StringWriter strwritter = new StringWriter();
		HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
		Response.Cache.SetCacheability(HttpCacheability.NoCache);
		Response.ContentType = "application/vnd.ms-excel";
		Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);  
		GridView1.GridLines = GridLines.Both;
		GridView1.HeaderStyle.Font.Bold = true;
		GridView1.RenderControl(htmltextwrtter);
		Response.Write(strwritter.ToString());
		Response.End();      
}


	public override void VerifyRenderingInServerForm(Control control)
	{
		/* Verifies that the control is rendered */
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