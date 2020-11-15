using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using CrystalDecisions.Shared;
using System.IO;

public partial class SiloBalanceReport : System.Web.UI.Page
{
	SqlDataReader dr;
	public string rid;
	public string frmdate;
	public string pname;
	public string cname;
	public string managmobNo;
	//public string todate;
	public int btnvalloanupdate = 0;
	DateTime dtm = new DateTime();
	// BLLPlantMaster plantmasterBL = new BLLPlantMaster();
	//int cmpcode, pcode;
	public string companycode;
	public string Companyname;
	public string plantcode;
	public string Plantname;
	//static int savebtn = 0;
	DataTable dt = new DataTable();
    DbHelper DBclass = new DbHelper();
	BOLDispatch DispatchBOL = new BOLDispatch();
	BLLDispatch DispatchBLL = new BLLDispatch();
	DALDispatch DispatchDAL = new DALDispatch();
	BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    public static int roleid;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
	protected void Page_Load(object sender, EventArgs e)
	{




		if (!IsPostBack)
		{
            if ((Session["Name"] != null) && (Session["pass"] != null))
			{
                roleid = Convert.ToInt32(Session["Role"].ToString());
				// Companyname = Session["Company_Name"].ToString();
				companycode = Session["Company_code"].ToString();
				plantcode = Session["Plant_Code"].ToString();
				//  plantname1 = Session["Plant_Name"].ToString();

				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();
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
                    plantcode = "170";
                    loadspecialsingleplant();
                }
				plantcode = ddl_PlantID.SelectedItem.Value;
				rid = ddl_PlantName.SelectedItem.Value;
				// rid = ddl_RouteID.SelectedItem.Value;
				//Period_Despatchreport();
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
				companycode = Session["Company_code"].ToString();
				plantcode = Session["Plant_Code"].ToString();
				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();
			//	managmobNo = Session["managmobNo"].ToString();
				plantcode = ddl_PlantID.SelectedItem.Value;
				if (RadioButtonList1.SelectedValue == "1")
				{

					Period_Despatchreport1();
				}

				if (RadioButtonList1.SelectedValue == "2")
				{
					Period_Despatchreport();

				}

				if (RadioButtonList1.SelectedValue == "3")
				{
					Period_Despatchreport2();

				}
				//txt_FromDate.Attributes.Add("txt_FromDate", txt_FromDate.Text.Trim());

				//LoadPlantcode();
				//rid = ddl_PlantName.SelectedItem.Value;
				//dtm = System.DateTime.Now;
				//   txt_FromDate.Text = dtm.ToShortDateString();
				//   txt_ToDate.Text = dtm.ToShortDateString();

				// Period_Despatchreport();


				//dtm = System.DateTime.Now;
				//txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
				//txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

				//LoadPlantcode();
				rid = ddl_PlantName.SelectedItem.Value;
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
			ddl_PlantID.Items.Clear();
			ddl_PlantName.Items.Clear();
			dr = Bllusers.LoadPlantcode(companycode.ToString());
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
					ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

				}
			}
			else
			{
				ddl_PlantID.Items.Add("--Select PlantName--");
				ddl_PlantName.Items.Add("--Select Plantcode--");
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
			ddl_PlantID.Items.Clear();
			ddl_PlantName.Items.Clear();
			dr = Bllusers.LoadSinglePlantcode(companycode, plantcode);
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
					ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
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
            ddl_PlantID.Items.Clear();
            ddl_PlantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(companycode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }




	protected void Button2_Click(object sender, EventArgs e)
	{
		plantcode = ddl_PlantID.SelectedItem.Value;
		
		if (RadioButtonList1.SelectedValue == "1")
		{

			Period_Despatchreport1();
		}

		if (RadioButtonList1.SelectedValue == "2")
		{
			Period_Despatchreport();

		}
		if (RadioButtonList1.SelectedValue == "3")
		{
			Period_Despatchreport2();

		}

	}

	private void Period_Despatchreport()
	{
		try
		{


			CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
			cr.Load(Server.MapPath("Report\\Silo.rpt"));
			cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
			CrystalDecisions.CrystalReports.Engine.TextObject t1;
			CrystalDecisions.CrystalReports.Engine.TextObject t2;
			CrystalDecisions.CrystalReports.Engine.TextObject t3;
			CrystalDecisions.CrystalReports.Engine.TextObject t4;
			//   CrystalDecisions.CrystalReports.Engine.TextObject t5;


			t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
			t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
			t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
			t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
			//  t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName1"];

			t1.Text = companycode + "_" + cname;
			t2.Text = ddl_PlantName.SelectedItem.Value;

			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
			t3.Text = "From  " + txt_FromDate.Text.Trim();
			t4.Text = "To  " + txt_ToDate.Text.Trim();

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");

			//   t5.Text = rid;

 
			string str = string.Empty;

			SqlConnection con = null;
			string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			con = new SqlConnection(connection);


			//str = " select * from (select plant_code, isnull(openstock.op_MILKKG,0) as op_MILKKG,isnull(openstock.op_FAT,0) as op_FAT,isnull(openstock.op_SNF,0)as op_SNF ,cast(openstock.op_FATKG as decimal(18,2)) as op_FATKG,cast(openstock.op_SNFKG as decimal(18,2)) as op_SNFKG,openstock.op_RATE,openstock.op_AMOUNT,procur.pr_MILKKG,cast(procur.pr_FAT as decimal(18,2)) as pr_FAT,cast(procur.pr_SNF as decimal(18,2)) as pr_SNF ,cast(procur.pr_FATKG as decimal(18,2)) as pr_FATKG,cast(procur.pr_SNFKG as decimal(18,2)) as pr_SNFKG,cast(procur.pr_RATE as decimal(18,2)) as pr_RATE,cast(procur.pr_AMOUNT as decimal(18,2)) as pr_AMOUNT from (select  sum(Milk_kg) as pr_MILKKG,AVG(Fat) as pr_FAT,AVG(Snf) as pr_SNF,AVG(fat_kg)as pr_FATKG,avg(snf_kg) as pr_SNFKG,SUM (Amount) as pr_AMOUNT,avg(Rate) as pr_RATE  from Procurement where Prdate between '7/31/2013' and '7/31/2013' AND Plant_Code ='113'  group by  Plant_code) As  procur   LEFT JOIN (select top 1 cast(Milkkg  as decimal(18,2)) as op_MILKKG,cast(Fat as decimal(18,2)) as op_FAT,cast(Snf as decimal(18,2)) as op_SNF,cast(fat_kg as decimal(18,2)) as op_FATKG,cast(snf_kg as decimal(18,2)) as op_SNFKG,(Amount) as op_AMOUNT,(Rate) as op_RATE,Plant_code from Stock_openingmilk where Datee  BETWEEN '7/31/2013' AND '7/31/2013' AND Plant_Code ='113' order by Datee desc) AS openstock  on Plant_code > 0 ) as joinopenprocure FULL OUTER JOIN (select cl_Plant_code,CAST(despat.dp_MILKKG as decimal(18,2)) dp_MILKKG,CAST(despat.dp_FAT as decimal(18,2)) dp_FAT,CAST(despat.dp_SNF as decimal(18,2)) dp_SNF,CAST(despat.dp_FATKG as decimal(18,2)) dp_FATKG,CAST(despat.dp_SNFKG as decimal(18,2)) as dp_SNFKG,CAST(despat.dp_RATE as decimal(18,2)) as dp_RATE, CAST(despat.dp_AMOUNT as decimal(18,2)) as dp_AMOUNT,cast(closestock.cl_MILKKG as  decimal(18,2)) as  cl_MILKKG,cast(closestock.cl_FAT as  decimal(18,2)) as  cl_FAT,cast(closestock.cl_SNF as  decimal(18,2)) as cl_SNF,cast(closestock.cl_FATKG as  decimal(18,2)) as cl_FATKG,cast(closestock.cl_SNFKG as  decimal(18,2)) as cl_SNFKG,cast(closestock.cl_RATE as  decimal(18,2)) as cl_RATE,cast(closestock.cl_AMOUNT as  decimal(18,2)) as cl_AMOUNT  from (select sum(MilkKg) as dp_MILKKG,AVG(Fat) as dp_FAT,AVG(Snf) as dp_SNF,avg(Fat_Kg) as dp_FATKG,avg(Snf_Kg) as dp_SNFKG,SUM (Amount) as dp_AMOUNT,avg(Rate) as dp_RATE,Plant_code as dp_Plant_code from Despatchnew where date between  '7/31/2013' AND '7/31/2013' AND Plant_Code ='113'  group by  Plant_code ) as despat  left join (SELECT top 1 MilkKg as cl_MILKKG,Fat as cl_FAT,Snf as cl_SNF,Fat_Kg as cl_FATKG,Snf_Kg as cl_SNFKG,Amount AS cl_AMOUNT,Rate AS cl_RATE,Plant_code as cl_Plant_code FROM Stock_Milk WHERE Date BETWEEN '7/31/2013' AND '7/31/2013' AND Plant_Code ='113'  ORDER BY Date ASC ) as closestock on despat.dp_Plant_code=closestock.cl_Plant_code) as  joindispatclosing on joinopenprocure.Plant_code=joindispatclosing.cl_Plant_code";

			//str = "select * from (select plant_code, isnull(openstock.op_MILKKG,0) as op_MILKKG,isnull(openstock.op_FAT,0) as op_FAT,isnull(openstock.op_SNF,0)as op_SNF ,cast(openstock.op_FATKG as decimal(18,2)) as op_FATKG,cast(openstock.op_SNFKG as decimal(18,2)) as op_SNFKG,openstock.op_RATE,openstock.op_AMOUNT,procur.pr_MILKKG,cast(procur.pr_FAT as decimal(18,2)) as pr_FAT,cast(procur.pr_SNF as decimal(18,2)) as pr_SNF ,cast(procur.pr_FATKG as decimal(18,2)) as pr_FATKG,cast(procur.pr_SNFKG as decimal(18,2)) as pr_SNFKG,cast(procur.pr_RATE as decimal(18,2)) as pr_RATE,cast(procur.pr_AMOUNT as decimal(18,2)) as pr_AMOUNT from (select  sum(Milk_kg) as pr_MILKKG,AVG(Fat) as pr_FAT,AVG(Snf) as pr_SNF,AVG(fat_kg)as pr_FATKG,avg(snf_kg) as pr_SNFKG,SUM (Amount) as pr_AMOUNT,avg(Rate) as pr_RATE  from Procurement where Prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' AND Plant_Code ='" + ddl_PlantID.Text + "'  group by  Plant_code) As  procur   LEFT JOIN (select top 1 cast(Milkkg  as decimal(18,2)) as op_MILKKG,cast(Fat as decimal(18,2)) as op_FAT,cast(Snf as decimal(18,2)) as op_SNF,cast(fat_kg as decimal(18,2)) as op_FATKG,cast(snf_kg as decimal(18,2)) as op_SNFKG,(Amount) as op_AMOUNT,(Rate) as op_RATE,Plant_code from Stock_openingmilk where Datee='" + txt_FromDate.Text + "'  AND Plant_Code ='" + ddl_PlantID.Text + "' order by Datee desc) AS openstock  on Plant_code > 0 ) as joinopenprocure left join(select cl_Plant_code,CAST(despat.dp_MILKKG as decimal(18,2)) dp_MILKKG,CAST(despat.dp_FAT as decimal(18,2)) dp_FAT,CAST(despat.dp_SNF as decimal(18,2)) dp_SNF,CAST(despat.dp_FATKG as decimal(18,2)) dp_FATKG,CAST(despat.dp_SNFKG as decimal(18,2)) as dp_SNFKG,CAST(despat.dp_RATE as decimal(18,2)) as dp_RATE, CAST(despat.dp_AMOUNT as decimal(18,2)) as dp_AMOUNT,cast(closestock.cl_MILKKG as  decimal(18,2)) as  cl_MILKKG,cast(closestock.cl_FAT as  decimal(18,2)) as  cl_FAT,cast(closestock.cl_SNF as  decimal(18,2)) as cl_SNF,cast(closestock.cl_FATKG as  decimal(18,2)) as cl_FATKG,cast(closestock.cl_SNFKG as  decimal(18,2)) as cl_SNFKG,cast(closestock.cl_RATE as  decimal(18,2)) as cl_RATE,cast(closestock.cl_AMOUNT as  decimal(18,2)) as cl_AMOUNT  from(select sum(MilkKg) as dp_MILKKG,AVG(Fat) as dp_FAT,AVG(Snf) as dp_SNF,avg(Fat_Kg) as dp_FATKG,avg(Snf_Kg) as dp_SNFKG,SUM (Amount) as dp_AMOUNT,avg(Rate) as dp_RATE,Plant_code as dp_Plant_code from Despatchnew where date between  '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND Plant_Code ='" + ddl_PlantID.Text + "'  group by  Plant_code ) as despat  left join (SELECT top 1 MilkKg as cl_MILKKG,Fat as cl_FAT,Snf as cl_SNF,Fat_Kg as cl_FATKG,Snf_Kg as cl_SNFKG,Amount AS cl_AMOUNT,Rate AS cl_RATE,Plant_code as cl_Plant_code FROM Stock_Milk WHERE Date='" + txt_ToDate.Text + "' AND Plant_Code ='" + ddl_PlantID.Text + "'  ORDER BY Date ASC ) as closestock on despat.dp_Plant_code=closestock.cl_Plant_code) as  joindispatclosing on joinopenprocure.Plant_code=joindispatclosing.cl_Plant_code";
			str = " SELECT t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT FROM ( SELECT Sum(Milk_kg) as pr_MILKKG,Avg(fat) as pr_FAT,AVG(snf) as pr_SNF,sum(fat_kg)as pr_FATKG,sum(snf_kg) as pr_SNFKG,sum(rate) as pr_RATE,sum(AMOUNT) as pr_AMOUNT FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t1,(SELECT Sum(Milkkg) as dp_MILKKG,Avg(fat) as dp_FAT,AVG(snf) as dp_SNF,SUM(Fat_Kg) as dp_FATKG,sum(Snf_Kg) as dp_SNFKG, sum(RATE) as dp_RATE,sum(Amount) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t2,(SELECT Sum(Milkkg) as op_MILKKG,Avg(fat) as op_FAT,AVG(snf) as op_SNF,SUM(Fat_Kg) as op_FATKG,sum(Snf_Kg) as op_SNFKG, sum(RATE) as op_RATE,sum(Amount) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT Sum(Milkkg) as cl_MILKKG,Avg(fat) as cl_FAT,AVG(snf) as cl_SNF,SUM(Fat_Kg) as cl_FATKG,sum(Snf_Kg) as cl_SNFKG, sum(RATE) as cl_RATE,sum(Amount) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C";
			//str = "  select * from( SELECT  t1.pcode,t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT FROM ( SELECT Sum(Milk_kg) as pr_MILKKG,Avg(fat) as pr_FAT,AVG(snf) as pr_SNF,sum(fat_kg)as pr_FATKG,sum(snf_kg) as pr_SNFKG,sum(rate) as pr_RATE,sum(AMOUNT) as pr_AMOUNT,Plant_Code as pcode FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' group by Plant_Code)t1,(SELECT Sum(Milkkg) as dp_MILKKG,Avg(fat) as dp_FAT,AVG(snf) as dp_SNF,SUM(Fat_Kg) as dp_FATKG,sum(Snf_Kg) as dp_SNFKG, sum(RATE) as dp_RATE,sum(Amount) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t2,(SELECT Sum(Milkkg) as op_MILKKG,Avg(fat) as op_FAT,AVG(snf) as op_SNF,SUM(Fat_Kg) as op_FATKG,sum(Snf_Kg) as op_SNFKG, sum(RATE) as op_RATE,sum(Amount) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT Sum(Milkkg) as cl_MILKKG,Avg(fat) as cl_FAT,AVG(snf) as cl_SNF,SUM(Fat_Kg) as cl_FATKG,sum(Snf_Kg) as cl_SNFKG, sum(RATE) as cl_RATE,sum(Amount) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C) as ak   left join   (  SELECT  convert(decimal(18,2),(SUM(amount +comrate+cartageamount +splbonusamount)/SUM(milk_ltr))) as Rate,Plant_Code   FROM  Procurement    WHERE prdate between  '" + d1 + "' AND '" + d2 + "' group by Plant_Code) as nn  on   ak.pcode=nn.Plant_Code ";
			//  for acknowledgement
			//    str = " SELECT t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT FROM ( SELECT Sum(Milk_kg) as pr_MILKKG,Avg(fat) as pr_FAT,AVG(snf) as pr_SNF,sum(fat_kg)as pr_FATKG,sum(snf_kg) as pr_SNFKG,sum(rate) as pr_RATE,sum(AMOUNT) as pr_AMOUNT FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t1,(SELECT Sum(Ack_milkkg) as dp_MILKKG,Avg(Ack_fat) as dp_FAT,AVG(Ack_snf) as dp_SNF,SUM(Fat_Kg) as dp_FATKG,sum(Snf_Kg) as dp_SNFKG, sum(RATE) as dp_RATE,sum(Amount) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' and Type='ack' )t2,(SELECT Sum(Milkkg) as op_MILKKG,Avg(fat) as op_FAT,AVG(snf) as op_SNF,SUM(Fat_Kg) as op_FATKG,sum(Snf_Kg) as op_SNFKG, sum(RATE) as op_RATE,sum(Amount) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT Sum(Milkkg) as cl_MILKKG,Avg(fat) as cl_FAT,AVG(snf) as cl_SNF,SUM(Fat_Kg) as cl_FATKG,sum(Snf_Kg) as cl_SNFKG, sum(RATE) as cl_RATE,sum(Amount) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C";
			SqlCommand cmd = new SqlCommand();
			SqlDataAdapter da = new SqlDataAdapter(str, con);

			DataTable dt = new DataTable();
			da.Fill(dt);
			cr.SetDataSource(dt);
			CrystalReportViewer1.ReportSource = cr;
            //CrystalReportViewer1.Dispose();
            //cr.Dispose();
            //cr.Close();
		}
		catch (Exception ex)
		{
			WebMsgBox.Show(ex.ToString());
		}


	}

	private void Period_Despatchreport1()
	{
		try
		{
			CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
			cr.Load(Server.MapPath("Report\\Silo1.rpt"));
			cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
			CrystalDecisions.CrystalReports.Engine.TextObject t1;
			CrystalDecisions.CrystalReports.Engine.TextObject t2;
			CrystalDecisions.CrystalReports.Engine.TextObject t3;
			CrystalDecisions.CrystalReports.Engine.TextObject t4;
			//   CrystalDecisions.CrystalReports.Engine.TextObject t5;
			t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
			t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
			t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
			t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
			//  t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName1"];
			t1.Text = companycode + "_" + cname;
			t2.Text = ddl_PlantName.SelectedItem.Value;
			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
			t3.Text = "From  " + txt_FromDate.Text.Trim();
			t4.Text = "To  " + txt_ToDate.Text.Trim();
			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");
			string str = string.Empty;
			SqlConnection con = null;
			string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			con = new SqlConnection(connection);
            if (plantcode == "171")
            {
                plantcode = "168";
            }
            if (plantcode == "172")
            {
                plantcode = "166";
            }
            if (plantcode == "168")
            {
                str = "SELECT ak.pr_MILKKG, ak.pr_FAT, ak.pr_SNF, ak.pr_FATKG, ak.pr_SNFKG, ak.pr_RATE, ak.pr_AMOUNT, ak.dp_MILKKG, ak.dp_FAT, ak.dp_SNF, ak.dp_FATKG, ak.dp_SNFKG, ak.dp_RATE, ak.dp_AMOUNT, ak.op_MILKKG, ak.op_FAT,  ak.op_SNF, ak.op_FATKG, ak.op_SNFKG, ak.op_RATE, ak.op_AMOUNT, ak.cl_MILKKG, ak.cl_FAT, ak.cl_SNF, ak.cl_FATKG, ak.cl_SNFKG, ak.cl_RATE, ak.cl_AMOUNT, ak.Plant_code, ak.Rate FROM            (SELECT        t1.pr_MILKKG, t1.pr_FAT, t1.pr_SNF, t1.pr_FATKG, t1.pr_SNFKG, t1.pr_RATE, t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT, t2.dp_SNF, t2.dp_FATKG, t2.dp_SNFKG, t2.dp_RATE, t2.dp_AMOUNT, t3.op_MILKKG, t3.op_FAT,   t3.op_SNF, t3.op_FATKG, t3.op_SNFKG, t3.op_RATE, t3.op_AMOUNT, C.cl_MILKKG, C.cl_FAT, C.cl_SNF, C.cl_FATKG, C.cl_SNFKG, C.cl_RATE, C.cl_AMOUNT, t2.Plant_code, t1.Rate   FROM            (SELECT        ISNULL(SUM(Milk_kg), 0) AS pr_MILKKG, ISNULL(AVG(Fat), 0) AS pr_FAT, ISNULL(AVG(Snf), 0) AS pr_SNF, ISNULL(SUM(fat_kg), 0) AS pr_FATKG, ISNULL(SUM(snf_kg), 0) AS pr_SNFKG, ISNULL(SUM(Rate),  0) AS pr_RATE, ISNULL(SUM(Amount), 0) AS pr_AMOUNT, CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate  FROM            Procurement WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (168, 171))) AS t1 CROSS JOIN    (SELECT        Plant_code, ISNULL(SUM(MilkKg), 0) AS dp_MILKKG, ISNULL(AVG(Fat), 0) AS dp_FAT, ISNULL(AVG(Snf), 0) AS dp_SNF, ISNULL(SUM(Fat_Kg), 0) AS dp_FATKG, ISNULL(SUM(Snf_Kg), 0) AS dp_SNFKG,   ISNULL(SUM(Rate), 0) AS dp_RATE, ISNULL(SUM(Amount), 0) AS dp_AMOUNT     FROM            Despatchnew   WHERE        (Date BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_code = '" + plantcode + "') GROUP BY Plant_code) AS t2 CROSS JOIN    (SELECT        ISNULL(SUM(MilkKg), 0) AS op_MILKKG, ISNULL(AVG(Fat), 0) AS op_FAT, ISNULL(AVG(Snf), 0) AS op_SNF, ISNULL(SUM(Fat_Kg), 0) AS op_FATKG, ISNULL(SUM(Snf_Kg), 0) AS op_SNFKG,   ISNULL(SUM(Rate), 0) AS op_RATE, ISNULL(SUM(Amount), 0) AS op_AMOUNT  FROM            Stock_openingmilk  WHERE        (Datee = '" + d1 + "') AND (Plant_code = '" + plantcode + "')) AS t3 CROSS JOIN (SELECT        ISNULL(SUM(MilkKg), 0) AS cl_MILKKG, ISNULL(AVG(Fat), 0) AS cl_FAT, ISNULL(AVG(Snf), 0) AS cl_SNF, ISNULL(SUM(Fat_Kg), 0) AS cl_FATKG, ISNULL(SUM(Snf_Kg), 0) AS cl_SNFKG,  ISNULL(SUM(Rate), 0) AS cl_RATE, ISNULL(SUM(Amount), 0) AS cl_AMOUNT FROM            Stock_Milk  WHERE        (Date = '" + d2 + "') AND (Plant_code = '" + plantcode + "')) AS C) AS ak LEFT OUTER JOIN  (SELECT        CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate, Plant_Code FROM            Procurement AS Procurement_1  WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (168, 171)) GROUP BY Plant_Code) AS nn ON ak.Plant_code = nn.Plant_Code";

               // str = " select * from( SELECT  ak.Plant_code, t1.pr_MILKKG, t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT, t2.Plant_code FROM ( SELECT isnull(Sum(Milk_kg),0) as pr_MILKKG,isnull(Avg(fat),0) as pr_FAT,isnull(AVG(snf),0) as pr_SNF,isnull(sum(fat_kg),0)as pr_FATKG,isnull(sum(snf_kg),0) as pr_SNFKG,isnull(sum(rate),0) as pr_RATE,isnull(sum(AMOUNT),0) as pr_AMOUNT FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code IN (168, 171))t1,(SELECT isnull(Sum(Milkkg),0) as dp_MILKKG,isnull(Avg(fat),0) as dp_FAT,isnull(AVG(snf),0) as dp_SNF,isnull(SUM(Fat_Kg),0) as dp_FATKG,isnull(sum(Snf_Kg),0) as dp_SNFKG,isnull(sum(RATE),0) as dp_RATE,isnull(sum(Amount),0) as dp_AMOUNT, Plant_Code FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' GROUP BY Plant_Code)t2,(SELECT isnull(Sum(Milkkg),0) as op_MILKKG,isnull(Avg(fat),0) as op_FAT,isnull(AVG(snf),0) as op_SNF,isnull(SUM(Fat_Kg),0) as op_FATKG,isnull(sum(Snf_Kg),0) as op_SNFKG, isnull(sum(RATE),0) as op_RATE,isnull(sum(Amount),0) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT isnull(Sum(Milkkg),0) as cl_MILKKG,isnull(Avg(fat),0) as cl_FAT,isnull(AVG(snf),0) as cl_SNF,isnull(SUM(Fat_Kg),0) as cl_FATKG,isnull(sum(Snf_Kg),0) as cl_SNFKG,isnull( sum(RATE),0) as cl_RATE,isnull(sum(Amount),0) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C) as ak   left join   (  SELECT  convert(decimal(18,2),isnull((SUM(amount +comrate+cartageamount +splbonusamount)/SUM(milk_ltr)),0)) as Rate,Plant_Code   FROM  Procurement    WHERE prdate between  '" + d1 + "' AND '" + d2 + "' group by Plant_Code) as nn  on   ak.Plant_code=nn.Plant_Code ";
            }
            else if (plantcode == "166")
            {
                str = "SELECT ak.pr_MILKKG, ak.pr_FAT, ak.pr_SNF, ak.pr_FATKG, ak.pr_SNFKG, ak.pr_RATE, ak.pr_AMOUNT, ak.dp_MILKKG, ak.dp_FAT, ak.dp_SNF, ak.dp_FATKG, ak.dp_SNFKG, ak.dp_RATE, ak.dp_AMOUNT, ak.op_MILKKG, ak.op_FAT,  ak.op_SNF, ak.op_FATKG, ak.op_SNFKG, ak.op_RATE, ak.op_AMOUNT, ak.cl_MILKKG, ak.cl_FAT, ak.cl_SNF, ak.cl_FATKG, ak.cl_SNFKG, ak.cl_RATE, ak.cl_AMOUNT, ak.Plant_code, ak.Rate FROM            (SELECT        t1.pr_MILKKG, t1.pr_FAT, t1.pr_SNF, t1.pr_FATKG, t1.pr_SNFKG, t1.pr_RATE, t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT, t2.dp_SNF, t2.dp_FATKG, t2.dp_SNFKG, t2.dp_RATE, t2.dp_AMOUNT, t3.op_MILKKG, t3.op_FAT,   t3.op_SNF, t3.op_FATKG, t3.op_SNFKG, t3.op_RATE, t3.op_AMOUNT, C.cl_MILKKG, C.cl_FAT, C.cl_SNF, C.cl_FATKG, C.cl_SNFKG, C.cl_RATE, C.cl_AMOUNT, t2.Plant_code, t1.Rate   FROM            (SELECT        ISNULL(SUM(Milk_kg), 0) AS pr_MILKKG, ISNULL(AVG(Fat), 0) AS pr_FAT, ISNULL(AVG(Snf), 0) AS pr_SNF, ISNULL(SUM(fat_kg), 0) AS pr_FATKG, ISNULL(SUM(snf_kg), 0) AS pr_SNFKG, ISNULL(SUM(Rate),  0) AS pr_RATE, ISNULL(SUM(Amount), 0) AS pr_AMOUNT, CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate  FROM            Procurement WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (166, 172))) AS t1 CROSS JOIN    (SELECT        Plant_code, ISNULL(SUM(MilkKg), 0) AS dp_MILKKG, ISNULL(AVG(Fat), 0) AS dp_FAT, ISNULL(AVG(Snf), 0) AS dp_SNF, ISNULL(SUM(Fat_Kg), 0) AS dp_FATKG, ISNULL(SUM(Snf_Kg), 0) AS dp_SNFKG,   ISNULL(SUM(Rate), 0) AS dp_RATE, ISNULL(SUM(Amount), 0) AS dp_AMOUNT     FROM            Despatchnew   WHERE        (Date BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_code = '" + plantcode + "') GROUP BY Plant_code) AS t2 CROSS JOIN    (SELECT        ISNULL(SUM(MilkKg), 0) AS op_MILKKG, ISNULL(AVG(Fat), 0) AS op_FAT, ISNULL(AVG(Snf), 0) AS op_SNF, ISNULL(SUM(Fat_Kg), 0) AS op_FATKG, ISNULL(SUM(Snf_Kg), 0) AS op_SNFKG,   ISNULL(SUM(Rate), 0) AS op_RATE, ISNULL(SUM(Amount), 0) AS op_AMOUNT  FROM            Stock_openingmilk  WHERE        (Datee = '" + d1 + "') AND (Plant_code = '" + plantcode + "')) AS t3 CROSS JOIN (SELECT        ISNULL(SUM(MilkKg), 0) AS cl_MILKKG, ISNULL(AVG(Fat), 0) AS cl_FAT, ISNULL(AVG(Snf), 0) AS cl_SNF, ISNULL(SUM(Fat_Kg), 0) AS cl_FATKG, ISNULL(SUM(Snf_Kg), 0) AS cl_SNFKG,  ISNULL(SUM(Rate), 0) AS cl_RATE, ISNULL(SUM(Amount), 0) AS cl_AMOUNT FROM            Stock_Milk  WHERE        (Date = '" + d2 + "') AND (Plant_code = '" + plantcode + "')) AS C) AS ak LEFT OUTER JOIN  (SELECT        CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate, Plant_Code FROM            Procurement AS Procurement_1  WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (166, 172)) GROUP BY Plant_Code) AS nn ON ak.Plant_code = nn.Plant_Code";
            }
            else
            {
                str = " select * from( SELECT  t1.pcode,t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT FROM ( SELECT isnull(Sum(Milk_kg),0) as pr_MILKKG,isnull(Avg(fat),0) as pr_FAT,isnull(AVG(snf),0) as pr_SNF,isnull(sum(fat_kg),0)as pr_FATKG,isnull(sum(snf_kg),0) as pr_SNFKG,isnull(sum(rate),0) as pr_RATE,isnull(sum(AMOUNT),0) as pr_AMOUNT,Plant_Code as pcode FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' group by Plant_Code)t1,(SELECT isnull(Sum(Milkkg),0) as dp_MILKKG,isnull(Avg(fat),0) as dp_FAT,isnull(AVG(snf),0) as dp_SNF,isnull(SUM(Fat_Kg),0) as dp_FATKG,isnull(sum(Snf_Kg),0) as dp_SNFKG,isnull(sum(RATE),0) as dp_RATE,isnull(sum(Amount),0) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t2,(SELECT isnull(Sum(Milkkg),0) as op_MILKKG,isnull(Avg(fat),0) as op_FAT,isnull(AVG(snf),0) as op_SNF,isnull(SUM(Fat_Kg),0) as op_FATKG,isnull(sum(Snf_Kg),0) as op_SNFKG, isnull(sum(RATE),0) as op_RATE,isnull(sum(Amount),0) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT isnull(Sum(Milkkg),0) as cl_MILKKG,isnull(Avg(fat),0) as cl_FAT,isnull(AVG(snf),0) as cl_SNF,isnull(SUM(Fat_Kg),0) as cl_FATKG,isnull(sum(Snf_Kg),0) as cl_SNFKG,isnull( sum(RATE),0) as cl_RATE,isnull(sum(Amount),0) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C) as ak   left join   (  SELECT  convert(decimal(18,2),isnull((SUM(amount +comrate+cartageamount +splbonusamount)/SUM(milk_ltr)),0)) as Rate,Plant_Code   FROM  Procurement    WHERE prdate between  '" + d1 + "' AND '" + d2 + "' group by Plant_Code) as nn  on   ak.pcode=nn.Plant_Code ";
            }
            SqlCommand cmd = new SqlCommand();
			SqlDataAdapter da = new SqlDataAdapter(str, con);
			DataTable dt = new DataTable();
			da.Fill(dt);
            if (dt.Rows.Count > 1)
            {
            }
			cr.SetDataSource(dt);
			CrystalReportViewer1.ReportSource = cr;
		}
		catch (Exception ex)
		{
			WebMsgBox.Show(ex.ToString());
		}
	}

	private void Period_Despatchreport2()
	{
		try
		{
			cr.Load(Server.MapPath("Report\\Silo2.rpt"));
			cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
			CrystalDecisions.CrystalReports.Engine.TextObject t1;
			CrystalDecisions.CrystalReports.Engine.TextObject t2;
			CrystalDecisions.CrystalReports.Engine.TextObject t3;
			CrystalDecisions.CrystalReports.Engine.TextObject t4;
			//   CrystalDecisions.CrystalReports.Engine.TextObject t5;


			t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
			t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
			t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
			t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
			//  t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName1"];

			t1.Text = companycode + "_" + cname;
			t2.Text = ddl_PlantName.SelectedItem.Value;

			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
			t3.Text = "From  " + txt_FromDate.Text.Trim();
			t4.Text = "To  " + txt_ToDate.Text.Trim();

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");
			//   t5.Text = rid;
			string str = string.Empty;
			SqlConnection con = null;
			string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			con = new SqlConnection(connection);
			//  for acknowledgement
			//    str = " SELECT t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT FROM ( SELECT Sum(Milk_kg) as pr_MILKKG,Avg(fat) as pr_FAT,AVG(snf) as pr_SNF,sum(fat_kg)as pr_FATKG,sum(snf_kg) as pr_SNFKG,sum(rate) as pr_RATE,sum(AMOUNT) as pr_AMOUNT FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t1,(SELECT Sum(Ack_milkkg) as dp_MILKKG,Avg(Ack_fat) as dp_FAT,AVG(Ack_snf) as dp_SNF,SUM(Fat_Kg) as dp_FATKG,sum(Snf_Kg) as dp_SNFKG, sum(RATE) as dp_RATE,sum(Amount) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' and Type='ack' )t2,(SELECT Sum(Milkkg) as op_MILKKG,Avg(fat) as op_FAT,AVG(snf) as op_SNF,SUM(Fat_Kg) as op_FATKG,sum(Snf_Kg) as op_SNFKG, sum(RATE) as op_RATE,sum(Amount) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT Sum(Milkkg) as cl_MILKKG,Avg(fat) as cl_FAT,AVG(snf) as cl_SNF,SUM(Fat_Kg) as cl_FATKG,sum(Snf_Kg) as cl_SNFKG, sum(RATE) as cl_RATE,sum(Amount) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C";
			str = " select * from( SELECT  t1.pcode,t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT FROM ( SELECT isnull(Sum(Milk_kg),0) as pr_MILKKG,isnull(Avg(fat),0) as pr_FAT,isnull(AVG(snf),0) as pr_SNF,isnull(sum(fat_kg),0)as pr_FATKG,isnull(sum(snf_kg),0) as pr_SNFKG,isnull(sum(rate),0) as pr_RATE,isnull(sum(AMOUNT),0) as pr_AMOUNT,Plant_Code as pcode FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' group by Plant_Code)t1,(SELECT isnull(Sum(Milkkg),0) as dp_MILKKG,isnull(Avg(fat),0) as dp_FAT,isnull(AVG(snf),0) as dp_SNF,isnull(SUM(Fat_Kg),0) as dp_FATKG,isnull(sum(Snf_Kg),0) as dp_SNFKG,isnull(sum(RATE),0) as dp_RATE,isnull(sum(Amount),0) as dp_AMOUNT FROM DespatchEntry Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t2,(SELECT isnull(Sum(Milkkg),0) as op_MILKKG,isnull(Avg(fat),0) as op_FAT,isnull(AVG(snf),0) as op_SNF,isnull(SUM(Fat_Kg),0) as op_FATKG,isnull(sum(Snf_Kg),0) as op_SNFKG, isnull(sum(RATE),0) as op_RATE,isnull(sum(Amount),0) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT isnull(Sum(Milkkg),0) as cl_MILKKG,isnull(Avg(fat),0) as cl_FAT,isnull(AVG(snf),0) as cl_SNF,isnull(SUM(Fat_Kg),0) as cl_FATKG,isnull(sum(Snf_Kg),0) as cl_SNFKG,isnull( sum(RATE),0) as cl_RATE,isnull(sum(Amount),0) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C) as ak   left join   (  SELECT  convert(decimal(18,2),isnull((SUM(amount +comrate+cartageamount +splbonusamount)/SUM(milk_ltr)),0)) as Rate,Plant_Code   FROM  Procurement    WHERE prdate between  '" + d1 + "' AND '" + d2 + "' group by Plant_Code) as nn  on   ak.pcode=nn.Plant_Code ";
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

	//protected void Button1_Click(object sender, EventArgs e)
	//{

	//    CrystalDecisions.CrystalReports.Engine.ReportDocument cr1 = new ReportDocument();
	//    ExportOptions CrExportOptions;
	//    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
	//    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
	//    CrDiskFileDestinationOptions.DiskFileName = "C:\\SampleReport.pdf";
	//    CrExportOptions = cr1.ExportOptions;
	//    {
	//        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
	//        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
	//        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
	//        CrExportOptions.FormatOptions = CrFormatTypeOptions;
	//    }
	//    cr1.Export();
	//}
	protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_PlantID.SelectedIndex = ddl_PlantName.SelectedIndex;
		plantcode = ddl_PlantID.SelectedItem.Value;
		if (RadioButtonList1.SelectedValue == "1")
		{

			Period_Despatchreport1();
		}

		if (RadioButtonList1.SelectedValue == "2")
		{
			Period_Despatchreport();

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