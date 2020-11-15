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


public partial class Rpt_Procurement : System.Web.UI.Page
{
	SqlDataReader dr;
	Bllbillgenerate BLLBill = new Bllbillgenerate();
	BOLbillgenerate BOLBill = new BOLbillgenerate();
	BLLroutmaster routmasterBL = new BLLroutmaster();
	BLLpayment Bllpay = new BLLpayment();
	BLLuser Bllusers = new BLLuser();
	CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

	public string ccode;
	public string pcode;
	public string managmobNo;
	public string pname;
	public string cname;
	public string rid;
	public string frmdate;
	//public string todate;
	public int btnvalloanupdate = 0;
	DateTime dtm = new DateTime();
	//Admin Check Flag
	public int Falg = 0;
	DbHelper DBaccess = new DbHelper();
	SqlConnection con = new SqlConnection();
    public static int roleid;
    public static int buttonviewstatus;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
            if ((Session["Name"] != null) && (Session["pass"] != null))
			{
				btn_Export.Visible = false;
				Button2.Visible = false;
                roleid = Convert.ToInt32(Session["Role"].ToString());
				ccode = Session["Company_code"].ToString();
				pcode = Session["Plant_Code"].ToString();
				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();
				//managmobNo = Session["managmobNo"].ToString();
				dtm = System.DateTime.Now;
				//txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
				//txt_ToDate.Text = dtm.ToString("dd/MM/yyyy"); 
                if (roleid < 3)
				{
					loadsingleplant();
				}
                if (roleid >= 3)
				{
					LoadPlantcode();
				}
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();
                }
                try
                {
                    Bdate();
                    loadrouteid();
                    rid = ddl_RouteID.SelectedItem.Value;
                    // Select_Procurementreport1();
                    Label4.Visible = false;
                }
                catch
                {

                    Label4.Visible = false;
                }
			}
			else
			{

				Server.Transfer("LoginDefault.aspx");
			}

		}
		if (IsPostBack == true)
		{

            if ((Session["Name"] != null) && (Session["pass"] != null))
			{
				btn_Export.Visible = false;
				Button2.Visible = false;
				Label4.Visible = false;
				ccode = Session["Company_code"].ToString();
				pcode = Session["Plant_Code"].ToString();
				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();
			//	managmobNo = Session["managmobNo"].ToString();

				//txt_FromDate.Attributes.Add("txt_FromDate", txt_FromDate.Text.Trim());
				rid = ddl_RouteID.SelectedItem.Value;
				//  BillD();
				//if (btnvalloanupdate == 1)
				//{
				//    Select_Procurementreport1();

				//}
				//else if (btnvalloanupdate == 2)
				//{
				//    Select_Procurementreport2();
				//}
				//else
				//{
				//    Select_Procurementreport1();
				//}
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
			txt_PlantPhoneNo.Items.Clear();
			dr = Bllusers.LoadPlantcode(ccode);
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
					ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
					txt_PlantPhoneNo.Items.Add(dr["Mana_PhoneNo"].ToString());
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
			txt_PlantPhoneNo.Items.Clear();
			dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
					ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
					txt_PlantPhoneNo.Items.Add(dr["Mana_PhoneNo"].ToString());
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
                    if (roleid >=6)
					{
						Falg = 1;
						Button2.Visible = false;
						btn_Export.Visible = false;
					//	int Uid = Convert.ToInt32(Session["User_ID"]);
						btn_Proceed.Visible = true;
                        Button3.Visible=true;
					}
					else
					{
						
                            //Button2.Visible = false;
                            //btn_Export.Visible = false;
						
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
        con = DBaccess.GetConnection();
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

	protected void Button1_Click(object sender, EventArgs e)
	{
		//btnvalloanupdate = 2;
		//rid = ddl_RouteID.SelectedItem.Value;
		//  Select_Procurementreport2();
	}
	private void Select_Procurementreport()
	{
		//ReportDocument crystalReport = new ReportDocument();
		//crystalReport.Load(Server.MapPath("Crpt_procurement.rpt"));
		//crystalReport.SetDatabaseLogon
		//    ("sa", "sa", @"ONEZERO-DA47585", "WebDairy");
		//CrystalReportViewer1.ReportSource = crystalReport;

		//CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
		//SqlConnection con = null;
		//string connection = ConfigurationManager.ConnectionStrings["TEST"].ConnectionString;
		//con = new SqlConnection(connection);
		//string str = "SELECT F.proAid,CONVERT(NVARCHAR,CONVERT(Datetime,F.Prdate,103)) AS prdate,F.Sessions,F.Mltr,F.Milk_kg,F.Fat,F.Snf,F.Clr,F.Rate,F.ComRate,F.Amount,F.Fatkg,F.Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,F.Smltr,F.Smkg,F.Afat,F.Asnf,F.AvRate,F.Aclr,F.Scans,F.SAmt,F.ACRate,F.Sfatkg,F.SSnfkg FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '10-11-2012' AND '10-12-2012' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '10-11-2012' AND '10-12-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1'  GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '12-10-2012' AND '12-20-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS cart ON prodedulon.proAid=cart.cartAid ) AS F   ORDER BY F.proAid,F.Prdate,F.Sessions ";
		//SqlCommand cmd = new SqlCommand();
		//SqlDataAdapter da = new SqlDataAdapter(str, con);
		//DataTable dt = new DataTable();
		//da.Fill(dt);
		//cr.Load(Server.MapPath("Crpt_Procurementt.rpt"));

		// cr.SetDatabaseLogon("sa", "sa", "ONEZEROD-1BCC2C\\MSSQLDATA", "AMPSD");

		//
		//CrystalDecisions.Shared.ConnectionInfo conInfo = new CrystalDecisions.Shared.ConnectionInfo();
		//conInfo.UserID = "sa";
		//conInfo.Password = "sa";
		//conInfo.ServerName = "ONEZEROD-1BCC2C\\MSSQLDATA";
		//conInfo.DatabaseName = "AMPSD";

		//



		//cr.SetDataSource(dt);
		//CrystalReportViewer1.ReportSource = cr;

		//System.IO.MemoryStream stream = (System.IO.MemoryStream)cr.ExportToStream(ExportFormatType.PortableDocFormat);
		//BinaryReader Bin = new BinaryReader(cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
		//Response.ClearContent();
		//Response.ClearHeaders();
		//Response.ContentType = "application/pdf";
		//Response.BinaryWrite(Bin.ReadBytes(Convert.ToInt32(Bin.BaseStream.Length)));
		//Response.Flush();
		//Response.Close();

	}
	protected void Button2_Click(object sender, EventArgs e)
	{
		//old Bill start
		//btnvalloanupdate = 1;
		//rid = ddl_RouteID.SelectedItem.Value;
		//Select_Procurementreport1();
		//old Bill End
		//NEW  Bill start
		// BillD();
	}
	private void SETBO()
	{
		BOLBill.Companycode = int.Parse(ccode);
		BOLBill.Plantcode = int.Parse(ddl_Plantcode.SelectedItem.Value);
		BOLBill.Route_id = int.Parse(rid);
		//BOLBill.Frmdate = DateTime.Parse(txt_FromDate.Text.Trim());
		//BOLBill.Todate = DateTime.Parse(txt_ToDate.Text.Trim());
		BOLBill.Frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
		BOLBill.Todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

	}
	//    private void Select_Procurementreport1()
	//    {
	//        try
	//        {
	//            SETBO();

	//            if (chk_rate.Checked == true)
	//            {
	//              //  cr.Load(Server.MapPath("BillNewBuff.rpt"));
	//                cr.Load(Server.MapPath("Report\\BillDBuff.rpt"));
	//            }
	//            else
	//            {
	//               // cr.Load(Server.MapPath("BillNew.rpt"));
	//                cr.Load(Server.MapPath("Report\\BillD.rpt"));
	//            }
	//            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
	//            CrystalDecisions.CrystalReports.Engine.TextObject t1;
	//            CrystalDecisions.CrystalReports.Engine.TextObject t2;
	//            CrystalDecisions.CrystalReports.Engine.TextObject t3;
	//            CrystalDecisions.CrystalReports.Engine.TextObject t4;
	//            CrystalDecisions.CrystalReports.Engine.TextObject t5;

	//            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
	//            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
	//            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
	//            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
	//            //t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_phoneno"];                             

	//            DateTime dt1 = new DateTime();
	//            DateTime dt2 = new DateTime();

	//            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
	//            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

	//            t1.Text = ccode + "_" + cname;
	//            t2.Text = ddl_Plantname.SelectedItem.Value + "_PhoneNo :" + txt_PlantPhoneNo.Text.Trim();
	//            t3.Text = txt_FromDate.Text.Trim();
	//            t4.Text = "To : " + txt_ToDate.Text.Trim();

	//            // t5.Text = managmobNo;

	//            string d1 = dt1.ToString("MM/dd/yyyy");
	//            string d2 = dt2.ToString("MM/dd/yyyy");

	//            string str = string.Empty;
	//            SqlConnection con = null;
	//            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	//            con = new SqlConnection(connection);
	//            if (chk_Loandeduct.Checked == false)
	//            {

	//                // str = "SELECT F.proAid,CONVERT(NVARCHAR,CONVERT(Datetime,F.Prdate,103)) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '10-11-2012' AND '10-12-2012' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '10-11-2012' AND '10-12-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1'  GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '12-10-2012' AND '12-20-2013' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS cart ON prodedulon.proAid=cart.cartAid ) AS F   ORDER BY F.proAid,F.Prdate,F.Sessions ";
	//                // str = "SELECT F.proAid,CONVERT(NVARCHAR,CONVERT(Datetime,F.Prdate,103)) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + frmdate + "' AND '" + todate + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + frmdate + "' AND '" + todate + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + frmdate + "' AND '" + todate + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "') AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "') AS cart ON prodedulon.proAid=cart.cartAid ) AS F   ORDER BY F.proAid,F.Prdate,F.Sessions ";
	//                // working// str = "SELECT F.proAid,CONVERT(VARCHAR,Prdate,103) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "') AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "') AS cart ON prodedulon.proAid=cart.cartAid ) AS F LEFT JOIN (SELECT route_id,Route_name,plant_code,company_code FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F.Rid=R.route_id  ORDER BY F.proAid,F.Prdate,F.Sessions ORDER BY F.proAid,F.Prdate,F.Sessions ";
	//                //str = "SELECT R.Route_name,F.proAid,CONVERT(VARCHAR,Prdate,103) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(SplBonusAmount AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON prodedulon.proAid=cart.cartAid ) AS F LEFT JOIN (SELECT route_id,Route_name,plant_code,company_code FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F.Rid=R.route_id  ORDER BY F.proAid,R.route_id,F.Prdate,F.Sessions ";
	//                //8_route palli str = "SELECT R.Route_name,F.proAid,CONVERT(VARCHAR,Prdate,103) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(SplBonusAmount AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "' and route_id=8 ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and route_id=8 GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON prodedulon.proAid=cart.cartAid ) AS F LEFT JOIN (SELECT route_id,Route_name,plant_code,company_code FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F.Rid=R.route_id  ORDER BY  F.Prdate,F.Sessions,R.route_id,F.proAid ";
	//                //current work All ROUTE
	//                //DEDUCTION
	//                // BLLBill.GetDeductionForDeductionDeduct(BOLBill);
	//                //LOAN
	//                // BLLBill.GetLoandetailsForLoanDeduct(BOLBill);
	//                //31-7-2014 work str = "SELECT R.Route_name,F.proAid,CONVERT(VARCHAR,Prdate,103) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,NoofCans AS SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "'  ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>1 GROUP BY agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON prodedulon.proAid=cart.cartAid ) AS F LEFT JOIN (SELECT route_id,Route_name,plant_code,company_code FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F.Rid=R.route_id  ORDER BY  F.proAid,F.Prdate,F.Sessions,R.route_id ";
	//                //str = "SELECT R.Route_name,F.proAid,CONVERT(VARCHAR,Prdate,103) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "' and route_id=8 ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' and route_id=8 GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 GROUP BY agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON prodedulon.proAid=cart.cartAid ) AS F LEFT JOIN (SELECT route_id,Route_name,plant_code,company_code FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F.Rid=R.route_id  ORDER BY  F.Prdate,F.Sessions,R.route_id,F.proAid ";
	//                //now working
	//                //str = "SELECT R.Route_name,F.proAid,CONVERT(VARCHAR,Prdate,103) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.balance,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.ClaimAount,0) AS ClaimAount,ISNULL(F.LoanAmount,0) AS LoanAmount,phone_No  FROM " +
	//                //      "(SELECT * FROM (SELECT * FROM  (SELECT * FROM  (SELECT * FROM  " +
	//                //      "(SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,Smltr AS Smltr,Smkg AS Smkg,AvgFat AS Afat,AvgSnf  AS Asnf,AvgRate AS AvRate,Avgclr AS Aclr,Scans AS Scans,SAmt AS SAmt,Avgcrate AS ACRate,Sfatkg AS Sfatkg,SSnfkg AS SSnfkg  FROM  " +
	//                //      "(SELECT Agent_Id AS proAid ,Prdate,Sessions,Milk_ltr AS Mltr,Milk_kg AS Milk_kg,Fat AS Fat,Snf AS  Snf,Clr AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,Rate AS Rate,ComRate AS ComRate,Amount AS Amount,Fat_kg AS Fatkg,Snf_kg AS Snfkg,NoofCans AS SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'   AND Company_Code='" + ccode + "'  ) AS pro  " +
	//                //      "LEFT JOIN  (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'    AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot  " +
	//                //      "LEFT JOIN    (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu " +
	//                //      "LEFT JOIN (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM " +
	//                //      "(SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM " +
	//                //      "(SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'  GROUP BY Agent_id) AS " +
	//                //      "Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + ddl_Plantcode.SelectedItem.Value + "' AND Paid_date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS " +
	//                //      "LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + ddl_Plantcode.SelectedItem.Value + "' AND LoanRecovery_Date between '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid) AS Loan  ON prodedu.proAid=Loan.LoAid) AS pdl " +
	//                //      "LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS ClaimAount  from Voucher_Clear where Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "' AND Clearing_Date BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_Id) AS vou ON pdl.proAid=vou.VouAid )AS pdlv " +
	//                //      "INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'  ) AS cart ON pdlv.proAid=cart.cartAid ) AS F " +
	//                //      "LEFT JOIN   (SELECT route_id,Route_name,plant_code,company_code,phone_No FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "' ) AS R ON F.Rid=R.Route_ID ORDER  BY R.Route_ID,F.proAid,F.Prdate,F.Sessions";                //current work 8 ROUTE


	//                str = "SELECT t1.*,t2.*  FROM " +
	//" (SELECT  Route_id,Route_name,Agent_id,Agent_name,Bank_id,Bank_name,Agent_accountNo,Ifsc_code,Milktype,Super_PhoneNo,SInsentAmt,Scaramt,SSplBonus,ClaimAount,SLoanAmount,Billadv,Ai,Feed,can,Recovery,others,SLoanClosingbalance,SAmt,TotAdditions,TotDeductions,Sinstamt,UPPER(Words) AS Words,Roundoff,Smkg,Smltr,Sfatkg,SSnfkg,Aclr,Scans ,NetAmount FROM paymentdata WHERE PLANT_CODE='" + ddl_Plantcode.SelectedItem.Value.Trim() + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "') AS t1 " +
	//" INNER JOIN " +
	//" (SELECT Agent_id,CONVERT(VARCHAR,Prdate,103) AS Prdate,Sessions,ISNULL(NoofCans,0) AS NoofCans,ISNULL(Milk_kg,0) AS Milk_kg,ISNULL(Milk_ltr,0) AS Milk_ltr,ISNULL(Fat,0) AS Fat,ISNULL(fat_kg,0) AS fat_kg,ISNULL(Snf,0) AS Snf,ISNULL(snf_kg,0) AS snf_kg,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,ISNULL(ComRate,0) AS ComRate  from Procurement Where Plant_code='" + ddl_Plantcode.SelectedItem.Value.Trim() + "' AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "') AS t2 ON t1.Agent_id=t2.Agent_id ORDER BY  t1.Route_id,t1.Agent_id,t2.Prdate,t2.Sessions ";

	//            }
	//            else
	//            {
	//                // str = "SELECT F.proAid,CONVERT(NVARCHAR,CONVERT(Datetime,F.Prdate,103)) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,NoofCans AS SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '10-11-2012' AND '10-12-2012' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '10-11-2012' AND '10-12-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1'  GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '12-10-2012' AND '12-20-2013' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS cart ON prodedulon.proAid=cart.cartAid ) AS F   ORDER BY F.proAid,F.Prdate,F.Sessions ";

	//                // BLLBill.GetLoandetailsForLoanDeduct(BOLBill);
	//            }

	//            //SqlCommand cmd = new SqlCommand("[dbo].[Get_Bill]");
	//            //con.Open();
	//            //cmd.CommandTimeout = 2000;
	//            //cmd.Connection = con;
	//            //cmd.CommandType = CommandType.StoredProcedure;
	//            //cmd.Parameters.AddWithValue("@spccode", ccode);
	//            //cmd.Parameters.AddWithValue("@sppcode", ddl_Plantcode.SelectedItem.Value.Trim());
	//            //cmd.Parameters.AddWithValue("@spfrmdate", d1);
	//            //cmd.Parameters.AddWithValue("@sptodate", d2);
	//            //SqlDataAdapter da = new SqlDataAdapter(cmd);

	//            SqlDataAdapter da = new SqlDataAdapter();
	//            DataTable dt = new DataTable();
	//            da.Fill(dt);

	//            cr.SetDataSource(dt);
	//            CrystalReportViewer1.ReportSource = cr;
	//        }
	//        catch (Exception ex)
	//        {

	//            WebMsgBox.Show(ex.ToString());
	//        }

	//    }

	//private void Select_Procurementreport2()
	//{
	//    try
	//    {
	//        SETBO();

	//        CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

	//        cr.Load(Server.MapPath("BillNew.rpt"));
	//        cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
	//        CrystalDecisions.CrystalReports.Engine.TextObject t1;
	//        CrystalDecisions.CrystalReports.Engine.TextObject t2;
	//        CrystalDecisions.CrystalReports.Engine.TextObject t3;
	//        CrystalDecisions.CrystalReports.Engine.TextObject t4;

	//        t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
	//        t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
	//        t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
	//        t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

	//        t1.Text = ccode + "_ONEZERO";
	//        t2.Text = pcode + "_pondy";
	//        t3.Text = "10-11-2012";
	//        t4.Text = "To 10-12-2012";

	//        string str = string.Empty;
	//        SqlConnection con = null;
	//        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	//        con = new SqlConnection(connection);
	//        if (chk_Loandeduct.Checked == false)
	//        {
	//           // BLLBill.GetLoandetailsForLoanDeduct(BOLBill);
	//            str = "SELECT F.proAid,CONVERT(NVARCHAR,CONVERT(Datetime,F.Prdate,103)) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_FromDate.Text + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'   GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + txt_ToDate.Text + "' AND '" + txt_ToDate.Text + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'   AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prodedulon.proAid=cart.cartAid ) AS F   ORDER BY F.proAid,F.Prdate,F.Sessions ";

	//            //str = "SELECT F.proAid,CONVERT(NVARCHAR,CONVERT(Datetime,F.Prdate,103)) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '10-11-2012' AND '10-12-2012' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '10-11-2012' AND '10-12-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1'  GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '12-10-2012' AND '12-20-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS cart ON prodedulon.proAid=cart.cartAid ) AS F   ORDER BY F.proAid,F.Prdate,F.Sessions ";
	//            //WebMsgBox.Show("Loan Updated");
	//        }
	//        else
	//        {
	//           // str = "SELECT F.proAid,CONVERT(NVARCHAR,CONVERT(Datetime,F.Prdate,103)) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '10-11-2012' AND '10-12-2012' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1' ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '10-11-2012' AND '10-12-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Company_Code='1'  GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '12-10-2012' AND '12-20-2012' AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(inst_amount AS DECIMAL(18,2)) AS instamt,Status FROM LoanDetails WHERE Company_Code='1' AND Plant_Code='111'  AND Route_id='11' AND Balance>0 ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='1' AND Plant_Code='111'  AND Route_id='11') AS cart ON prodedulon.proAid=cart.cartAid ) AS F   ORDER BY F.proAid,F.Prdate,F.Sessions ";

	//        }

	//        SqlCommand cmd = new SqlCommand();
	//        SqlDataAdapter da = new SqlDataAdapter(str, con);
	//        DataTable dt = new DataTable();
	//        da.Fill(dt);


	//        cr.SetDataSource(dt);
	//        CrystalReportViewer1.ReportSource = cr;

	//    }
	//    catch (Exception ex)
	//    {
	//        WebMsgBox.Show(ex.ToString());
	//    }


	//}
	protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_RouteName.SelectedIndex = ddl_RouteID.SelectedIndex;
		rid = ddl_RouteID.SelectedItem.Value;
		// Select_Procurementreport1();
	}
	protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
		rid = ddl_RouteID.SelectedItem.Value;
		//  Select_Procurementreport1();
	}
	private void loadrouteid()
	{
		SqlDataReader dr;


		dr = routmasterBL.getroutmasterdatareader(ccode, ddl_Plantcode.SelectedItem.Value);

		ddl_RouteName.Items.Clear();
		ddl_RouteID.Items.Clear();

		while (dr.Read())
		{
			ddl_RouteName.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
			ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());

		}

	}
	protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
	{

	}
	protected void btn_Export_Click(object sender, EventArgs e)
	{
		//    try
		//    {
		//     //   BillD();
		//       // Select_Procurementreport1();
		//        ExportOptions CrExportOptions;
		//        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
		//        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

		//        DateTime frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
		//        string fdate = frmdate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
		//        DateTime todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
		//        string tdate = todate.ToString("dd" + "_" + "MM" + "_" + "yyyy");

		//        string CurrentCreateFolderName = fdate + "_" + tdate + "_" + DateTime.Now.ToString("ddMMyyyy");
		//        string path = @"C:\BILL VYSHNAVI\" + ccode + "_" + "_" + ddl_Plantcode.SelectedItem.Value + CurrentCreateFolderName + "\\";
		//        if (!Directory.Exists(path))
		//        {
		//            Directory.CreateDirectory(path);
		//        }
		//        CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + ddl_Plantcode.SelectedItem.Value + "_" + "Bill.pdf";
		//        CrExportOptions = cr.ExportOptions;
		//        {
		//            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
		//            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
		//            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
		//            CrExportOptions.FormatOptions = CrFormatTypeOptions;
		//        }
		//        cr.Export();
		//        WebMsgBox.Show("Report Export Successfully...");

		//        //
		//        string MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + ddl_Plantcode.SelectedItem.Value + CurrentCreateFolderName + "/" + ccode + "_" + ddl_Plantcode.SelectedItem.Value + "_" + "Bill.pdf";
		//        System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
		//        if (File.Exists(MFileName.ToString()))
		//        {
		//            //
		//            FileStream sourceFile = new FileStream(file.FullName, FileMode.Open);
		//            float FileSize;
		//            FileSize = sourceFile.Length;
		//            byte[] getContent = new byte[(int)FileSize];
		//            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
		//            sourceFile.Close();
		//            //
		//            Response.ClearContent(); // neded to clear previous (if any) written content
		//            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
		//            Response.AddHeader("Content-Length", file.Length.ToString());
		//            Response.ContentType = "text/plain";
		//            Response.BinaryWrite(getContent);
		//            File.Delete(file.FullName.ToString());
		//            Response.Flush();
		//            Response.End();

		//        }
		//        else
		//        {

		//            Response.Write("File Not Found...");
		//        }
		//        //
		//    }
		//    catch (Exception ex)
		//    {
		//        WebMsgBox.Show("Please Check the ExportPath...");
		//    }
	}
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		txt_PlantPhoneNo.SelectedIndex = ddl_Plantname.SelectedIndex;
		pcode = ddl_Plantcode.SelectedItem.Value;
		Bdate();
		// BillD();
		//Select_Procurementreport1();

	}
	protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantname.SelectedIndex = ddl_Plantcode.SelectedIndex;
	}
	protected void btn_Proceed_Click(object sender, EventArgs e)
	{
		try
		{
			// CheckDate and other conditions
			using (con = DBaccess.GetConnection())
			{
				pcode = ddl_Plantcode.SelectedItem.Value;
				string mess = string.Empty;
				int ress = 0;
				SqlCommand cmd = new SqlCommand();
				SqlParameter parcompanycode, parplantcode, parmess, parress;
				cmd.CommandText = "[dbo].[BillDate_Check]";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = con;

				parcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
				parplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);

				parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 300);
				cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
				parress = cmd.Parameters.Add("@ress", SqlDbType.Int);
				cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

				parcompanycode.Value = ccode;
				parplantcode.Value = pcode;

				cmd.ExecuteNonQuery();
				mess = (string)cmd.Parameters["@mess"].Value;
				ress = (int)cmd.Parameters["@ress"].Value;

				if (ress == 1)
				{
					Label4.Visible = true;
					Label4.Text = mess.Trim();
				}
				else
				{
					SETBO();
					//DEDUCTION
					BLLBill.GetDeductionForDeductionDeduct(BOLBill);
					//LOAN
					BLLBill.GetLoandetailsForLoanDeduct(BOLBill);
					procespayment(Bllpay.getpaymentdatareader(txt_FromDate.Text, txt_ToDate.Text, ccode, pcode), txt_FromDate.Text, txt_ToDate.Text);
					Label4.Visible = true;
					Label4.Text = "Bill proceeding Completed...";

					//  BillD();
				}
			}
			//

		}
		catch (Exception ex)
		{
			Label4.Visible = true;
			Label4.Text = ex.ToString();
		}
	}

	private void procespayment(DataTable dt, string frdate, string todate)
	{
		pcode = ddl_Plantcode.SelectedItem.Value;
		pname = ddl_Plantname.SelectedItem.Value;
		DateTime dt1 = new DateTime();
		DateTime dt2 = new DateTime();
		dt1 = DateTime.ParseExact(frdate, "dd/MM/yyyy", null);
		dt2 = DateTime.ParseExact(todate, "dd/MM/yyyy", null);

		string d1 = dt1.ToString("MM/dd/yyyy");
		string d2 = dt2.ToString("MM/dd/yyyy");
		DataTable PaymentDT = new DataTable();
		if (dt.Rows.Count > 0)
		{
			PaymentDT = dt;
			SqlParameter param = new SqlParameter();
			param.ParameterName = "PaymentDt2";
			param.SqlDbType = SqlDbType.Structured;
			param.Value = PaymentDT;
			param.Direction = ParameterDirection.Input;
			String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection conn = null;
			using (conn = new SqlConnection(dbConnStr))
			{
				SqlCommand sqlCmd = new SqlCommand("dbo.[Insert_paymentdata]");
				conn.Open();
				sqlCmd.CommandTimeout = 300000;
				sqlCmd.Connection = conn;
				sqlCmd.CommandType = CommandType.StoredProcedure;
				sqlCmd.Parameters.Add(param);
				//sqlCmd.Parameters.AddWithValue("@spccode", ccode);
				sqlCmd.Parameters.AddWithValue("@spcname", cname);
				//sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
				sqlCmd.Parameters.AddWithValue("@sppname", pname);
				sqlCmd.Parameters.AddWithValue("@spfrdate", d1);
				sqlCmd.Parameters.AddWithValue("@sptodate", d2);
				sqlCmd.ExecuteNonQuery();
			}
		}


	}

	private void BillD()
	{
		try
		{
			SETBO();

			if (chk_rate.Checked == true)
			{
				cr.Load(Server.MapPath("BillD.rpt"));
			}
			else
			{
				cr.Load(Server.MapPath("BillD.rpt"));
			}
			cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
			CrystalDecisions.CrystalReports.Engine.TextObject t1;
			CrystalDecisions.CrystalReports.Engine.TextObject t2;
			CrystalDecisions.CrystalReports.Engine.TextObject t3;
			CrystalDecisions.CrystalReports.Engine.TextObject t4;
			CrystalDecisions.CrystalReports.Engine.TextObject t5;

			t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
			t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
			t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
			t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
			//t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_phoneno"];                             

			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();

			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

			t1.Text = ccode + "_" + cname;
			t2.Text = ddl_Plantname.SelectedItem.Value + "_PhoneNo :" + txt_PlantPhoneNo.Text.Trim();
			t3.Text = txt_FromDate.Text.Trim();
			t4.Text = "To : " + txt_ToDate.Text.Trim();

			// t5.Text = managmobNo;

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");

			string str = string.Empty;
			SqlConnection con = null;
			string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			con = new SqlConnection(connection);

			str = "SELECT t1.*,t2.*  FROM " +
" (SELECT  Route_id,Route_name,Agent_id,Agent_name,Bank_id,Bank_name,Agent_accountNo,Ifsc_code,Milktype,Super_PhoneNo,SInsentAmt,Scaramt,SSplBonus,ClaimAount,SLoanAmount,Billadv,Ai,Feed,can,Recovery,others,SLoanClosingbalance,SAmt,TotAdditions,TotDeductions,Sinstamt,UPPER(Words) AS Words,Roundoff,Smkg,Smltr,Sfatkg,SSnfkg,Aclr,Scans ,NetAmount FROM paymentdata WHERE PLANT_CODE='" + ddl_Plantcode.SelectedItem.Value.Trim() + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "') AS t1 " +
" INNER JOIN " +
" (SELECT Agent_id,CONVERT(VARCHAR,Prdate,103) AS Prdate,Sessions,ISNULL(NoofCans,0) AS NoofCans,ISNULL(Milk_kg,0) AS Milk_kg,ISNULL(Milk_ltr,0) AS Milk_ltr,ISNULL(Fat,0) AS Fat,ISNULL(fat_kg,0) AS fat_kg,ISNULL(Snf,0) AS Snf,ISNULL(snf_kg,0) AS snf_kg,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,ISNULL(ComRate,0) AS ComRate  from Procurement Where Plant_code='" + ddl_Plantcode.SelectedItem.Value.Trim() + "' AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "') AS t2 ON t1.Agent_id=t2.Agent_id ORDER BY  t1.Route_id,t1.Agent_id,t2.Prdate,t2.Sessions ";


			//SqlCommand cmd = new SqlCommand("[dbo].[Get_Bill]");
			//con.Open();
			//cmd.CommandTimeout = 2000;
			//cmd.Connection = con;
			//cmd.CommandType = CommandType.StoredProcedure;
			//cmd.Parameters.AddWithValue("@spccode", ccode);
			//cmd.Parameters.AddWithValue("@sppcode", ddl_Plantcode.SelectedItem.Value.Trim());
			//cmd.Parameters.AddWithValue("@spfrmdate", d1);
			//cmd.Parameters.AddWithValue("@sptodate", d2);
			//SqlDataAdapter da = new SqlDataAdapter(cmd);

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
	protected void txt_FromDate_TextChanged(object sender, EventArgs e)
	{

	}

	protected void btn_delete_Click(object sender, EventArgs e)
	{
		DateTime dt1 = new DateTime();
		DateTime dt2 = new DateTime();
		dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
		dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
		string d1 = dt1.ToString("MM/dd/yyyy");
		string d2 = dt2.ToString("MM/dd/yyyy");
		String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
		SqlConnection conn = null;
		using (conn = new SqlConnection(dbConnStr))
		{
			 string getval = ddl_Plantname.SelectedItem.ToString();
			string[] get = getval.Split('_');
			string str = "Delete   from paymentdata   where plant_code='" + get[0] + "' AND Frm_date='" + d1 + "' and To_date='" + d2 + "'";
			SqlCommand cmd = new SqlCommand(str, conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			Label4.Visible = true;
			Label4.Text = "Record Deleted SuccessFully...";

		}
	}
	public void msgbox()
	{
		string message;
		message = "Record Deleted SuccessFully";
		string script = "window.onload = function(){ alert('";script += message;		script += "')};";
		ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

	}
}
