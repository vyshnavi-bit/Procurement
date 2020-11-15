using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
public partial class RouteWisePerformence : System.Web.UI.Page
{

	DbHelper dbaccess = new DbHelper();
	string plantname;
	public string ccode;
	public string pcode;
	public string managmobNo;
	public string pname;
	public string cname;
	DataTable dt = new DataTable();
	DataSet ds = new DataSet();
	DataSet ds1 = new DataSet();
	DataSet ds2 = new DataSet();
	DataSet ds3 = new DataSet();

	DbHelper DBaccess = new DbHelper();
	BLLPlantName BllPlant = new BLLPlantName();
	DateTime tdt = new DateTime();
	DateTime dtm = new DateTime();
	DateTime dtm1 = new DateTime();
	string strsql = string.Empty;
	string date;
	string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	string frmdate;
	string Todate;
	DateTime d1;
	DateTime d2;
	DateTime datee1;
	DateTime datee2;
	string getagents;
	string allcharttype;
	string getagents1;
	string allcharttype1;
	int sumres;
	int sumres1;
	int sumres2;
	int i = 0;
	int id = 0;
	int id1 = 2;
	int id2 = 3;
	int iii;


	int ssumres;
	int ssumres1;
	int ssumres2;
	int ii = 0;
	int iid = 0;
	int iid1 = 2;
	int iid2 = 3;
	int iiii;
	int chartassign;
	int chartassign1;
	string[] getdates;
	int count;
	string getda;
	string managername;
	string stt;
	string stt1;
	string stt2;
	string A;
	string B;

	string c;
	string d;
	string g;
	string f;
	string getcal;
	string getcal1;
	string getca3;
    public static int roleid;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (IsPostBack != true)
		{

            if ((Session["Name"] != null) && (Session["pass"] != null))
			{

				ccode = Session["Company_code"].ToString();
				pcode = Session["Plant_Code"].ToString();
				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
			//	managmobNo = Session["managmobNo"].ToString();
				tdt = System.DateTime.Now;
				dtm = System.DateTime.Now;
				txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
				txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
				Label5.Visible = false;
				ddl_BillDate.Visible = false;
				Label4.Visible = false;
				ddl_BillDate1.Visible = false;
				Label6.Visible = false;
                if (roleid < 3)
				{
					LoadSinglePlantName();
				}
				else
				{
					LoadPlantName();
				}
				// pcode = ddl_PlantName.SelectedItem.Value;
			}

		}

		else
		{

            if ((Session["Name"] != null) && (Session["pass"] != null))
			{
				ccode = Session["Company_code"].ToString();
				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();
				//managmobNo = Session["managmobNo"].ToString();
				pcode = ddl_PlantName.SelectedItem.Value;

				plantname = ddl_PlantName.SelectedItem.Text;

				Label5.Visible = false;
				ddl_BillDate.Visible = false;
				Label4.Visible = false;
				ddl_BillDate1.Visible = false;
				Label6.Visible = false;
			}
			else
			{
				Server.Transfer("LoginDefault.aspx");
			}

		}
	}
	private void LoadPlantName()
	{
		try
		{

			ds = null;
			ds = LoadPlantNameChkLst1(ccode.ToString());
			if (ds != null)
			{
				ddl_PlantName.DataSource = ds;
				ddl_PlantName.DataTextField = "Plant_Name";
				ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
				ddl_PlantName.DataBind();

				plantname = ddl_PlantName.SelectedItem.Text;

			}
			else
			{

			}

		}
		catch (Exception ex)
		{
		}
	}
	private void LoadSinglePlantName()
	{
		try
		{

			ds1 = null;
			ds1 = LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
			if (ds1 != null)
			{
				ddl_PlantName.DataSource = ds1;
				ddl_PlantName.DataTextField = "Plant_Name";
				ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
				ddl_PlantName.DataBind();
				plantname = ddl_PlantName.SelectedItem.Text;
			}
			else
			{

			}

		}
		catch (Exception ex)
		{
		}
	}

	protected void btn_ok_Click(object sender, EventArgs e)
	{
		try
		{
			Label7.Visible = false;

			superwisername();
			billdate();
			if (count == 1)
			{
				getagentwisechart();
				GridView1.Visible = true;
				GridView2.Visible = false;
				GridView3.Visible = false;
			}
			if (count == 2)
			{
				getagentwisechart();
				getagentwisechart1();
				GridView1.Visible = true;
				GridView2.Visible = true;
				GridView3.Visible = false;

			}

			if (count == 3)
			{
				getagentwisechart();
				getagentwisechart1();
				getagentwisechart2();
				GridView1.Visible = true;
				GridView2.Visible = true;
				GridView3.Visible = true;

			}

		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}

	}




	public void getagentwisechart()
	{


		try
		{

			SqlConnection con = new SqlConnection(connStr);

			string str;


			stt = getdates[3];


			//	str = "select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost,Ltr1 as MLtr1,AvgFat1 as Afat1,AvgSnf1 as ASnf1,Rate1,proccost1 as ProTs1,transportcost1 as TCost1,Ltr2 as Mltr2,AvgFat2 as AFat2,AvgSnf2 as ASnf2,rate2,proccost2 as ProTs2,transportcost2 as Tcost2 from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa left join (select SupervisorName as SupervisorName1,RouteName as RouteName1 ,Ltr as  Ltr1,Rate as rate1,AvgFat as AvgFat1,AvgSnf as AvgSnf1,proccost as proccost1,transportcost as  transportcost1 from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,Plant_Code,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt1 + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.Plant_Code  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.Plant_Code and akk.routeid=leftside.Rootid) as bbb on  aaa.RouteName=bbb.RouteName1) as aaaaa left join (select SupervisorName as SupervisorName2,RouteName as RouteName2 ,Ltr as  Ltr2,AvgFat as AvgFat2,AvgSnf as AvgSnf2,proccost as proccost2,transportcost as  transportcost2,Rate as rate2 from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,Plant_Code,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt2 + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.Plant_Code  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.Plant_Code and akk.routeid=leftside.Rootid) as ccccc on aaaaa.RouteName=ccccc.RouteName2  where RouteName is not null and Ltr1 > 0";
			//	str = "  select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='"+pcode+"') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='"+pcode+"') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='"+pcode+"')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(10,2),(Rate) / (CONVERT(decimal(18,2),(AvgFat)) +CONVERT(decimal(18,2), AvgSnf))) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'10') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where  Plant_Code='" + pcode + "' and   Prdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa  ) as aaaaa   where  RouteName is not null and Ltr > 0";
			str = "  select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(10,2),(Rate) / (CONVERT(decimal(18,2),(AvgFat)) +CONVERT(decimal(18,2), AvgSnf))) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'10') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where  Plant_Code='" + pcode + "' and   Prdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa  ) as aaaaa   where  RouteName is not null and Ltr > 0";
			SqlCommand cmd = new SqlCommand(str, con);
			con.Open(); ;
			DataTable dt = new DataTable();
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				GridView1.DataSource = dt;
				GridView1.DataBind();
				GridView1.FooterRow.Cells[1].Text = "Total MilkKg";

				decimal mltr = dt.AsEnumerable().Sum(row => row.Field<decimal>("MLtr"));
				GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
				GridView1.FooterRow.Cells[3].Text = mltr.ToString("N2");


				var avgfat = dt.AsEnumerable().Where(x => x["Afat"] != DBNull.Value).Average(x => x.Field<decimal>("Afat"));
				GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
				GridView1.FooterRow.Cells[4].Text = avgfat.ToString("N2");

				var Asnf = dt.AsEnumerable().Where(x => x["Asnf"] != DBNull.Value).Average(x => x.Field<decimal>("Asnf"));
				GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
				GridView1.FooterRow.Cells[5].Text = Asnf.ToString("N2");

				var rate = dt.AsEnumerable().Where(x => x["Rate"] != DBNull.Value).Average(x => x.Field<decimal>("Rate"));
				GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
				GridView1.FooterRow.Cells[6].Text = rate.ToString("N2");



				var ProTs = dt.AsEnumerable().Where(x => x["ProTs"] != DBNull.Value).Average(x => x.Field<decimal>("ProTs"));
				GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
				GridView1.FooterRow.Cells[7].Text = ProTs.ToString("N2");

				var TCost = dt.AsEnumerable().Where(x => x["TCost"] != DBNull.Value).Average(x => x.Field<decimal>("TCost"));
				GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
				GridView1.FooterRow.Cells[8].Text = TCost.ToString("N2");


			}



			else
			{

				GridView1.DataSource = null;
				GridView1.DataBind();

			}

		}
		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}



	}

	public void getagentwisechart1()
	{


		try
		{

			SqlConnection con = new SqlConnection(connStr);

			string str;


			stt = getdates[6];

			//	stt1 = getdates[6];

			//	str = "select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost,Ltr1 as MLtr1,AvgFat1 as Afat1,AvgSnf1 as ASnf1,Rate1,proccost1 as ProTs1,transportcost1 as TCost1,Ltr2 as Mltr2,AvgFat2 as AFat2,AvgSnf2 as ASnf2,rate2,proccost2 as ProTs2,transportcost2 as Tcost2 from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa left join (select SupervisorName as SupervisorName1,RouteName as RouteName1 ,Ltr as  Ltr1,Rate as rate1,AvgFat as AvgFat1,AvgSnf as AvgSnf1,proccost as proccost1,transportcost as  transportcost1 from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,Plant_Code,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt1 + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.Plant_Code  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.Plant_Code and akk.routeid=leftside.Rootid) as bbb on  aaa.RouteName=bbb.RouteName1) as aaaaa left join (select SupervisorName as SupervisorName2,RouteName as RouteName2 ,Ltr as  Ltr2,AvgFat as AvgFat2,AvgSnf as AvgSnf2,proccost as proccost2,transportcost as  transportcost2,Rate as rate2 from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,Plant_Code,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt2 + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.Plant_Code  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.Plant_Code and akk.routeid=leftside.Rootid) as ccccc on aaaaa.RouteName=ccccc.RouteName2  where RouteName is not null and Ltr1 > 0";
			//	str = "  select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='"+pcode+"') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='"+pcode+"') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='"+pcode+"')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(10,2),(Rate) / (CONVERT(decimal(18,2),(AvgFat)) +CONVERT(decimal(18,2), AvgSnf))) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'10') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where  Plant_Code='" + pcode + "' and   Prdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa  ) as aaaaa   where  RouteName is not null and Ltr > 0";
			str = "  select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(10,2),(Rate) / (CONVERT(decimal(18,2),(AvgFat)) +CONVERT(decimal(18,2), AvgSnf))) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where  Plant_Code='" + pcode + "' and   Prdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa  ) as aaaaa   where  RouteName is not null and Ltr > 0";
			SqlCommand cmd = new SqlCommand(str, con);
			con.Open(); ;
			DataTable dt = new DataTable();
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				GridView2.DataSource = dt;
				GridView2.DataBind();
				GridView2.FooterRow.Cells[1].Text = "Total MilkKg";

				decimal mltr = dt.AsEnumerable().Sum(row => row.Field<decimal>("MLtr"));
				GridView2.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
				GridView2.FooterRow.Cells[3].Text = mltr.ToString("N2");


				var avgfat = dt.AsEnumerable().Where(x => x["Afat"] != DBNull.Value).Average(x => x.Field<decimal>("Afat"));
				GridView2.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
				GridView2.FooterRow.Cells[4].Text = avgfat.ToString("N2");

				var Asnf = dt.AsEnumerable().Where(x => x["Asnf"] != DBNull.Value).Average(x => x.Field<decimal>("Asnf"));
				GridView2.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
				GridView2.FooterRow.Cells[5].Text = Asnf.ToString("N2");

				var rate = dt.AsEnumerable().Where(x => x["Rate"] != DBNull.Value).Average(x => x.Field<decimal>("Rate"));
				GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
				GridView2.FooterRow.Cells[6].Text = rate.ToString("N2");



				var ProTs = dt.AsEnumerable().Where(x => x["ProTs"] != DBNull.Value).Average(x => x.Field<decimal>("ProTs"));
				GridView2.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
				GridView2.FooterRow.Cells[7].Text = ProTs.ToString("N2");

				var TCost = dt.AsEnumerable().Where(x => x["TCost"] != DBNull.Value).Average(x => x.Field<decimal>("TCost"));
				GridView2.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
				GridView2.FooterRow.Cells[8].Text = TCost.ToString("N2");


			}



			else
			{

				GridView2.DataSource = null;
				GridView2.DataBind();

			}

		}
		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}



	}

	public void getagentwisechart2()
	{


		try
		{

			SqlConnection con = new SqlConnection(connStr);

			string str;


			//stt = getdates[3];

			stt = getdates[9];

			//	str = "select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost,Ltr1 as MLtr1,AvgFat1 as Afat1,AvgSnf1 as ASnf1,Rate1,proccost1 as ProTs1,transportcost1 as TCost1,Ltr2 as Mltr2,AvgFat2 as AFat2,AvgSnf2 as ASnf2,rate2,proccost2 as ProTs2,transportcost2 as Tcost2 from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa left join (select SupervisorName as SupervisorName1,RouteName as RouteName1 ,Ltr as  Ltr1,Rate as rate1,AvgFat as AvgFat1,AvgSnf as AvgSnf1,proccost as proccost1,transportcost as  transportcost1 from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,Plant_Code,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt1 + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[4] + "' and '" + getdates[5] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.Plant_Code  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.Plant_Code and akk.routeid=leftside.Rootid) as bbb on  aaa.RouteName=bbb.RouteName1) as aaaaa left join (select SupervisorName as SupervisorName2,RouteName as RouteName2 ,Ltr as  Ltr2,AvgFat as AvgFat2,AvgSnf as AvgSnf2,proccost as proccost2,transportcost as  transportcost2,Rate as rate2 from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,Plant_Code,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(18,2),(Rate) / (AvgFat + AvgSnf)) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt2 + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.Plant_Code  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.Plant_Code and akk.routeid=leftside.Rootid) as ccccc on aaaaa.RouteName=ccccc.RouteName2  where RouteName is not null and Ltr1 > 0";
			//	str = "  select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='"+pcode+"') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='"+pcode+"') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='"+pcode+"')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(10,2),(Rate) / (CONVERT(decimal(18,2),(AvgFat)) +CONVERT(decimal(18,2), AvgSnf))) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'10') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where  Plant_Code='" + pcode + "' and   Prdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[1] + "' and '" + getdates[2] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa  ) as aaaaa   where  RouteName is not null and Ltr > 0";
			str = "  select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY SupervisorName ORDER BY SupervisorName) = 1    THEN SupervisorName ELSE NULL END AS 'SupervisorName',RouteName,Ltr as MLtr,AvgFat as Afat,AvgSnf as Asnf,Rate,proccost as ProTs,transportcost as TCost from (select *  from (select SupervisorName,RouteName,Ltr,Rate,AvgFat,AvgSnf,proccost,transportcost,pcode from (select supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName,Plant_Code from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + pcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid,Plant_Code  FROM  Route_Master WHERE Plant_Code='" + pcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code  ) as akk left join  (select Ltr,Rate,AvgFat,avgsnf,proccost,pcode,CONVERT(decimal(18,2),((transAmount))/(TotMilk)) as transportcost,Rootid  from(select Ltr,Rate,AvgFat,AvgSnf,CONVERT(decimal(10,2),(Rate) / (CONVERT(decimal(18,2),(AvgFat)) +CONVERT(decimal(18,2), AvgSnf))) as proccost,Plantcode as Plantcode,  TotMilk,Route_id from (select  CONVERT(decimal(18,2),SUM(milk_ltr)/'" + stt + "') as Ltr,CONVERT(decimal(18,2),(( SUM(Amount) + sum(comrate) + sum(CartageAmount) + sum(SplBonusAmount)) / SUM(Milk_ltr)) ) as Rate,CONVERT(decimal(18,2),(SUM(fat_kg) * 100)/sum(Milk_kg)) as AvgFat,CONVERT(decimal(18,2),(SUM(snf_kg) * 100)/sum(Milk_kg)) as AvgSnf,Plant_Code as Plantcode,SUM(Milk_ltr) as TotMilk,Route_id   from Procurement   where  Plant_Code='" + pcode + "' and   Prdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_id) as aa) as bb  left join (select CONVERT(decimal(18,2),(SUM(ActualAmount))) as transAmount,Plant_Code as pcode,Route_Id as Rootid   from  Truck_Present   where Plant_Code='" + pcode + "' and   Pdate between '" + getdates[7] + "' and '" + getdates[8] + "' group by Plant_Code,Route_Id) as ccc on  bb.Plantcode=ccc.pcode  and bb.Route_id=ccc.Rootid ) as leftside  on akk.Plant_Code=leftside.pcode and akk.routeid=leftside.Rootid) as aaa  ) as aaaaa   where  RouteName is not null and Ltr > 0";
			SqlCommand cmd = new SqlCommand(str, con);
			con.Open(); ;
			DataTable dt = new DataTable();
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				GridView3.DataSource = dt;
				GridView3.DataBind();
				GridView3.FooterRow.Cells[1].Text = "Total MilkKg";

				decimal mltr = dt.AsEnumerable().Sum(row => row.Field<decimal>("MLtr"));
				GridView3.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
				GridView3.FooterRow.Cells[3].Text = mltr.ToString("N2");


				var avgfat = dt.AsEnumerable().Where(x => x["Afat"] != DBNull.Value).Average(x => x.Field<decimal>("Afat"));
				GridView3.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
				GridView3.FooterRow.Cells[4].Text = avgfat.ToString("N2");

				var Asnf = dt.AsEnumerable().Where(x => x["Asnf"] != DBNull.Value).Average(x => x.Field<decimal>("Asnf"));
				GridView3.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
				GridView3.FooterRow.Cells[5].Text = Asnf.ToString("N2");

				var rate = dt.AsEnumerable().Where(x => x["Rate"] != DBNull.Value).Average(x => x.Field<decimal>("Rate"));
				GridView3.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
				GridView3.FooterRow.Cells[6].Text = rate.ToString("N2");



				var ProTs = dt.AsEnumerable().Where(x => x["ProTs"] != DBNull.Value).Average(x => x.Field<decimal>("ProTs"));
				GridView3.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
				GridView3.FooterRow.Cells[7].Text = ProTs.ToString("N2");

				var TCost = dt.AsEnumerable().Where(x => x["TCost"] != DBNull.Value).Average(x => x.Field<decimal>("TCost"));
				GridView3.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
				GridView3.FooterRow.Cells[8].Text = TCost.ToString("N2");


			}



			else
			{

				GridView3.DataSource = null;
				GridView3.DataBind();

			}
		}
		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}



	}
	public DataSet LoadSinglePlantNameChkLst1(string ccode, string pcode)
	{
		ds2 = null;

		//string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
		// string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
		string sqlstr = "SELECT plant_Code, CONVERT(NVARCHAR(15),pcode+'_'+Plant_Name) AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
		ds2 = dbaccess.GetDataset(sqlstr);
		return ds2;
	}


	public DataSet LoadPlantNameChkLst1(string ccode)
	{
		ds3 = null;

		//string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
		// string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
		string sqlstr = "SELECT plant_Code,pcode+'_'+Plant_Name AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "'  ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
		ds3 = dbaccess.GetDataset(sqlstr);
		return ds3;
	}

	protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
	{


		//    billdate();



	}


	public void billdate()
	{
		try
		{
			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");

			ddl_BillDate.Items.Clear();
			SqlConnection con = new SqlConnection(connStr);
			count = 0;
			//   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
			string str = "select Date + '_' + diff as Currentdate from (select FDate + '_ ' + TDate as Date,convert(varchar,diffday) as diff   from (select CONVERT(varchar,Bill_frmdate,101) as FDate,  CONVERT(varchar,Bill_todate,101) as TDate,Datediff(DAY,Bill_frmdate,Bill_todate)+ 1 as diffday   from   Bill_date   where  Plant_Code='" + pcode + "'  and Bill_frmdate between '" + d1 + "' and '" + d2 + "') as aa) as bb  order by  Currentdate ";
			SqlCommand cmd = new SqlCommand(str, con);
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{

				while (dr.Read())
				{
					count = count + 1;
					string getdat = dr["Currentdate"].ToString();
					getda = getda + '_' + getdat;
					// getdates = getdat.Split('_');
				}
				getdates = getda.Split('_');
			}



		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}

	}


	public void superwisername()
	{

		try
		{
			managername = "";
			Label6.Text = "";
			SqlConnection con = new SqlConnection(connStr);
			count = 0;
			//   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
			string str = "SELECT  top 1  SupervisorName  FROM  Supervisor_Details WHERE Plant_Code='" + pcode + "'   and Description='MANAGER'";
			SqlCommand cmd = new SqlCommand(str, con);
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{

				while (dr.Read())
				{

					managername = dr["SupervisorName"].ToString();
					Label6.Text = managername;
					// getdates = getdat.Split('_');
				}

			}


		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}


	}

	public void currentbill()
	{

		//  ddl_BillDate.Items.Clear();

		try
		{

			ddl_BillDate1.Items.Clear();
			SqlConnection con = new SqlConnection(connStr);

			//   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
			string str = "SELECT top 1 Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + pcode + "' AND Status=0  order by  tid desc  ";
			SqlCommand cmd = new SqlCommand(str, con);
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{

				while (dr.Read())
				{

					d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
					d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());


					frmdate = d1.ToString("MM/dd/yyy");

					Todate = d2.ToString("MM/dd/yyy");


				}

				ddl_BillDate1.Items.Add(frmdate + "-" + Todate);


			}
		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}

	}




	protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{

	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{

		try
		{





			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Cells[7].Text = "";


				double fat = Convert.ToDouble(e.Row.Cells[4].Text);
				double snf = Convert.ToDouble(e.Row.Cells[5].Text);
				double rate = Convert.ToDouble(e.Row.Cells[6].Text);
				double calc = (rate / (fat + snf));
				getcal = calc.ToString();
				string[] araa = getcal.Split('.');
				string getno = araa[0];
				string getno1 = araa[1];
				string getno2 = getno1.Substring(0, 2);
				string getno3 = araa[0] + "." + getno2;
				e.Row.Cells[7].Text = getno3;

				GridView1.FooterStyle.HorizontalAlign = HorizontalAlign.Left;

			}






			A = Convert.ToDateTime(getdates[1]).ToString("dd/MM/yyyy");
			B = Convert.ToDateTime(getdates[2]).ToString("dd/MM/yyyy");




			GridViewRow gvRow = e.Row;
			if (gvRow.RowType == DataControlRowType.Header)
			{




				GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "From Date:" + A + "To Date:" + B;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				gvrow.Cells.Add(cell0);
				GridView1.Controls[0].Controls.AddAt(0, gvrow);



			}



			GridViewRow gvRow1 = e.Row;
			if (gvRow1.RowType == DataControlRowType.Header)
			{





				GridViewRow gvrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "Maneger Name:" + Label6.Text;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				gvrow.Cells.Add(cell0);
				GridView1.Controls[0].Controls.AddAt(0, gvrow);



			}


			GridViewRow gvRow2 = e.Row;
			if (gvRow2.RowType == DataControlRowType.Header)
			{







				GridViewRow gvrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "Plant Name:" + ddl_PlantName.SelectedItem.Text;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				gvrow2.Cells.Add(cell0);
				GridView1.Controls[0].Controls.AddAt(0, gvrow2);



			}


			GridView1.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}



	}

	protected void btn_export_Click(object sender, EventArgs e)
	{
		try
		{

			//    Response.Clear();
			//    Response.Buffer = true;
			//    string filename = "'" + ddl_PlantName.SelectedItem.Text + "' " + " '" + "Managener Name" + managername + "' " + " " + DateTime.Now.ToString() + ".xls";

			//    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
			//    // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
			//    Response.Charset = "";
			//    Response.ContentType = "application/vnd.ms-excel";
			//    using (StringWriter sw = new StringWriter())
			//    {
			//        HtmlTextWriter hw = new HtmlTextWriter(sw);

			//        //To Export all pages
			//        GridView1.AllowPaging = false;
			//        // getgriddata();
			//        superwisername();
			//        billdate();

			//if(count==1)
			//{
			//    getagentwisechart();
			//}
			//    if (count == 2)
			//    {
			//        getagentwisechart();
			//        getagentwisechart1();
			//    }

			//    if (count == 3)
			//    {
			//        getagentwisechart();
			//        getagentwisechart1();
			//        getagentwisechart2();
			//    }





			//        GridView1.HeaderRow.BackColor = Color.White;
			//        foreach (TableCell cell in GridView1.HeaderRow.Cells)
			//        {
			//            cell.BackColor = GridView1.HeaderStyle.BackColor;
			//        }
			//        foreach (GridViewRow row in GridView1.Rows)
			//        {
			//            row.BackColor = Color.White;
			//            foreach (TableCell cell in row.Cells)
			//            {
			//                if (row.RowIndex % 2 == 0)
			//                {
			//                    cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
			//                }
			//                else
			//                {
			//                    cell.BackColor = GridView1.RowStyle.BackColor;
			//                }
			//                cell.CssClass = "textmode";
			//            }
			//        }

			//        GridView1.RenderControl(hw);

			//        //style to format numbers to string
			//        string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
			//        // string style = @"<style> .textmode { } </style>";
			//        Response.Write(style);
			//        Response.Output.Write(sw.ToString());
			//        Response.Flush();
			//        Response.End();
			//    }




			string filename = "'" + ddl_PlantName.SelectedItem.Text + "' " + " '" + "Managener Name" + managername + "' " + " " + DateTime.Now.ToString() + ".xls";

			//    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
			string attachment = "attachment; filename=Top 1.xls" + filename;
			Response.ClearContent();
			Response.AddHeader("content-disposition", attachment);
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType = "application/ms-excel";
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			GridView1.RenderControl(htw);
			GridView2.RenderControl(htw);
			GridView3.RenderControl(htw);
			Response.Write(sw.ToString());
			Response.End();
		}


		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}
	}
	public override void VerifyRenderingInServerForm(Control control)
	{
		/* Verifies that the control is rendered */
	}
	protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		try
		{



			if (count == 1)
			{

				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					e.Row.Cells[7].Text = "";


					double fat = Convert.ToDouble(e.Row.Cells[4].Text);
					double snf = Convert.ToDouble(e.Row.Cells[5].Text);
					double rate = Convert.ToDouble(e.Row.Cells[6].Text);
					double calc = (rate / (fat + snf));
					getcal = calc.ToString();
					string[] araa = getcal.Split('.');
					string getno = araa[0];
					string getno1 = araa[1];
					string getno2 = getno1.Substring(0, 2);
					string getno3 = araa[0] + "." + getno2;
					e.Row.Cells[7].Text = getno3;

					GridView2.FooterStyle.HorizontalAlign = HorizontalAlign.Left;

				}

			}




			A = Convert.ToDateTime(getdates[4]).ToString("dd/MM/yyyy");
			B = Convert.ToDateTime(getdates[5]).ToString("dd/MM/yyyy");




			GridViewRow gvRow = e.Row;
			if (gvRow.RowType == DataControlRowType.Header)
			{




				GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "From Date:" + A + "To Date:" + B;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				gvrow.Cells.Add(cell0);
				GridView2.Controls[0].Controls.AddAt(0, gvrow);



			}



			GridViewRow gvRow1 = e.Row;
			if (gvRow1.RowType == DataControlRowType.Header)
			{





				GridViewRow gvrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "Maneger Name:" + Label6.Text;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				gvrow.Cells.Add(cell0);
				GridView2.Controls[0].Controls.AddAt(0, gvrow);


			}


			GridViewRow gvRow2 = e.Row;
			if (gvRow2.RowType == DataControlRowType.Header)
			{







				GridViewRow gvrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "Plant Name:" + ddl_PlantName.SelectedItem.Text;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				gvrow2.Cells.Add(cell0);
				GridView2.Controls[0].Controls.AddAt(0, gvrow2);



			}


			GridView2.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}


	}
	private void catchmsg(string ex1)
	{

		Label7.ForeColor = System.Drawing.Color.Red;
		Label7.Text = ex1.ToString().Trim();
		Label7.Visible = true;


	}
	protected void txt_ToDate_TextChanged(object sender, EventArgs e)
	{

	}
	protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
	{

	}
	protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
	{

	}
	protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		try
		{





			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Cells[7].Text = "";


				double fat = Convert.ToDouble(e.Row.Cells[4].Text);
				double snf = Convert.ToDouble(e.Row.Cells[5].Text);
				double rate = Convert.ToDouble(e.Row.Cells[6].Text);
				double calc = (rate / (fat + snf));
				getcal = calc.ToString();
				string[] araa = getcal.Split('.');
				string getno = araa[0];
				string getno1 = araa[1];
				string getno2 = getno1.Substring(0, 2);
				string getno3 = araa[0] + "." + getno2;
				e.Row.Cells[7].Text = getno3;

				GridView3.FooterStyle.HorizontalAlign = HorizontalAlign.Left;

			}






			A = Convert.ToDateTime(getdates[7]).ToString("dd/MM/yyyy");
			B = Convert.ToDateTime(getdates[8]).ToString("dd/MM/yyyy");




			GridViewRow gvRow = e.Row;
			if (gvRow.RowType == DataControlRowType.Header)
			{




				GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "From Date:" + A + "To Date:" + B;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				gvrow.Cells.Add(cell0);
				GridView3.Controls[0].Controls.AddAt(0, gvrow);



			}



			GridViewRow gvRow1 = e.Row;
			if (gvRow1.RowType == DataControlRowType.Header)
			{





				GridViewRow gvrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "Maneger Name:" + Label6.Text;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				gvrow.Cells.Add(cell0);
				GridView3.Controls[0].Controls.AddAt(0, gvrow);
			}




			GridViewRow gvRow2 = e.Row;
			if (gvRow2.RowType == DataControlRowType.Header)
			{







				GridViewRow gvrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "Plant Name:" + ddl_PlantName.SelectedItem.Text;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				cell0.ColumnSpan = 9;
				gvrow2.Cells.Add(cell0);
				GridView3.Controls[0].Controls.AddAt(0, gvrow2);



			}


			GridView3.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}


	}
}