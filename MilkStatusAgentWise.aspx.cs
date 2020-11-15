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
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;


public partial class MilkStatusAgentWise : System.Web.UI.Page
{
	public static SqlDataReader dr = null;
	public static string ccode;
	public static string plantcode;
	public static int rid = 0;
	DateTime dt = new DateTime();
	BLLuser Bllusers = new BLLuser();
	BLLroutmaster routmasterBL = new BLLroutmaster();
	string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	DateTime dt0 = new DateTime();
	DateTime dt2 = new DateTime();
	string checkdate;
	string checkdate1;
	string d1;
	string d2;
	int columncount;
    public static int roleid;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (IsPostBack != true)
		{
            if ((Session["Name"] != null) && (Session["pass"] != null))
			{
				ccode = Session["Company_code"].ToString();
				plantcode = Session["Plant_Code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
				dt = System.DateTime.Now;
				txt_FromDate.Text = dt.ToString("dd/MM/yyyy");
				txt_ToDate.Text = dt.ToString("dd/MM/yyyy");
				lbldis.Visible = false;
				//txt_LoanDate.Text = dt.ToString("dd/MM/yyyy");
				//txt_LoanExpireDate.Text = dt.ToString("dd/MM/yyyy");
                if (roleid < 3)
				{
					loadsingleplant();
				}
				else
				{
					LoadPlantcode();
				}



			}
		}

		else
		{
			plantcode = ddl_Plantcode.SelectedItem.Value;
			ccode = Session["Company_code"].ToString();
			lbldis.Visible = false;
			// pcode = ddl_Plantcode.SelectedItem.Value;
			//  rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
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
			lblmsg.Visible = true;
			lblmsg.Text = ex.ToString();
		}
	}
	private void loadsingleplant()
	{
		try
		{
			SqlDataReader dr = null;
			ddl_Plantcode.Items.Clear();
			ddl_Plantname.Items.Clear();
			dr = Bllusers.LoadSinglePlantcode(ccode, plantcode);
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
			lblmsg.Visible = true;
			lblmsg.Text = ex.ToString();
		}
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
        try
        {
            if (RadioButtonList1.SelectedItem.Value == "3")
            {
                getgrid();
            }
            if (RadioButtonList1.SelectedItem.Value == "1")
            {
                getgridAM();
            }
            if (RadioButtonList1.SelectedItem.Value == "2")
            {
                getgridPM();
            }

        }
        catch
        {


        }

	}
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		plantcode = ddl_Plantcode.SelectedItem.Value;
		loadrouteid();


	}
	private void loadrouteid()
	{
		try
		{
			SqlDataReader dr;
			dr = routmasterBL.getroutmasterdatareader(ccode, plantcode);

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
			lblmsg.Visible = true;
			lblmsg.Text = ex.ToString();

		}

	}

	//public void getgrid()
	//{

	//    try
	//    {
	//        dt0 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
	//        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
	//        d1 = dt0.ToString("MM/dd/yyyy");
	//        d2 = dt2.ToString("MM/dd/yyyy");
	//        SqlConnection con = new SqlConnection(connStr);
	//        //   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
	//        string str;

	//        //  str = "select   ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='155' and route_id='9'   and prdate between '1-1-2016' and '1-7-2016' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='155' and route_id='9' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";
	//        str = "select  ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";

	//        SqlCommand cmd = new SqlCommand(str, con);
	//        DataTable dt = new DataTable();
	//        cmd.CommandTimeout = 500;
	//        cmd.CommandType = CommandType.Text;
	//        SqlDataAdapter da = new SqlDataAdapter(cmd);
	//        da.Fill(dt);

	//        DataTable dt1 = new DataTable();
	//        DataColumn dc = new DataColumn();
	//        DataRow dr;
	//        int count = dt.Rows.Count;

	//        if (dt.Rows.Count > 0)
	//        {

	//            dc = new DataColumn("AgentName");
	//            dt1.Columns.Add(dc);
	//            foreach (DataRow dr1 in dt.Rows)
	//            {
	//                columncount = columncount + 1;
	//                object id;
	//                id = dr1[2].ToString();
	//                //   string columnName = "D-" + id;
	//                string columnName = "" + id;
	//                DataColumnCollection columns = dt1.Columns;
	//                if (columns.Contains(columnName))
	//                {

	//                }
	//                else
	//                {
	//                    //   dc = new DataColumn("D-" + id);
	//                    dc = new DataColumn("" + id);
	//                    dt1.Columns.Add(dc);
	//                }
	//            }

	//            if (count > 0)
	//            {
	//                object id2;
	//                id2 = 0;
	//                int idd2 = Convert.ToInt32(id2);

	//                foreach (DataRow dr2 in dt.Rows)
	//                {
	//                    dr = dt1.NewRow();

	//                    object id1;
	//                    id1 = dr2[3].ToString();
	//                    int idd1 = Convert.ToInt32(id1);
	//                    if (idd1 == idd2)
	//                    {

	//                    }
	//                    else
	//                    {
	//                        int cc = 0;
	//                        foreach (DataRow dr3 in dt.Rows)
	//                        {
	//                            object id3;
	//                            id3 = dr3[3].ToString();
	//                            int idd3 = Convert.ToInt32(id3);
	//                            if (idd1 == idd3)
	//                            {
	//                                if (cc == 0)
	//                                {
	//                                    dr[cc] = dr3[1].ToString();
	//                                    cc++;
	//                                    dr[cc] = dr3[4].ToString();
	//                                    cc++;
	//                                }
	//                                else
	//                                {

	//                                    dr[cc] = dr3[4].ToString();
	//                                    cc++;
	//                                }
	//                                idd2 = idd3;
	//                            }
	//                        }
	//                        dt1.Rows.Add(dr);
	//                    }

	//                }
	//            }
	//            GridView1.DataSource = dt1;
	//            GridView1.DataBind();


	//            if (dt1.Rows.Count > 0)
	//            {
	//                int sumcountcheck = 0;
	//                foreach (DataColumn dc1 in dt1.Columns)
	//                {
	//                    if (sumcountcheck == 0)
	//                    {
	//                        GridView1.FooterRow.Cells[0].Text = "Total";
	//                        GridView1.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;

	//                    }
	//                    else if (sumcountcheck > 0)
	//                    {
	//                        decimal Gtotalcommon = 0;

	//                        foreach (DataRow dr1 in dt1.Rows)
	//                        {
	//                            object id;
	//                            id = dr1[sumcountcheck].ToString();
	//                            if (id == "")
	//                            {
	//                                id = 0;
	//                            }
	//                            decimal idd = Convert.ToDecimal(id);
	//                            Gtotalcommon = Gtotalcommon + idd;
	//                            GridView1.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Right;
	//                            GridView1.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
	//                            GridView1.FooterRow.HorizontalAlign = HorizontalAlign.Right;
	//                        }

	//                    }
	//                    else
	//                    {

	//                    }
	//                    sumcountcheck++;
	//                }

	//                GridView1.FooterStyle.ForeColor = System.Drawing.Color.Black;
	//                GridView1.FooterStyle.BackColor = System.Drawing.Color.Silver;
	//                GridView1.FooterStyle.Font.Bold = true;
	//            }


	//            lbldis.Text = "Plant Name:" + ddl_Plantname.Text + " Route Name: " +  ddl_RouteName.Text ;
	//            lblmsg.Visible = false;
	//            lbldis.Visible = true;
	//            //for (int j = 2; j < columncount; j++)
	//            //{
	//            //    double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("milk_kg"));
	//            //    GridView1.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
	//            //    GridView1.FooterRow.Cells[j].Text = milkkg.ToString("N2");

	//            //}



	//            //GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
	//            //GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
	//            //GridView1.FooterRow.Cells[2].Text = "Total";
	//            //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
	//            //GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
	//            //GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;

	//            //GridView1.DataSource = ConvertColumnsAsRows(dt);
	//            //GridView1.DataBind();
	//            //GridView1.HeaderRow.Visible = false;

	//            ////GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[2].Text = "Total";
	//            ////GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
	//            //////GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
	//            //////GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;
	//            ////double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("MilkKg"));
	//            ////GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[3].Text = milkkg.ToString("N2");

	//            ////double milkltr = dt.AsEnumerable().Sum(row => row.Field<double>("MilkLtr"));
	//            ////GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[4].Text = milkltr.ToString("N2");

	//            ////decimal netamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"));
	//            ////GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[5].Text = netamount.ToString("N2");


	//        }
	//        else
	//        {
	//            GridView1.DataSource = null;
	//            GridView1.DataBind();
	//            lblmsg.Text = "NO Data Particular Date or Please Check Your Date";
	//            lblmsg.ForeColor = System.Drawing.Color.Red;
	//            lblmsg.Visible = true;

	//        }

	//    }

	//    catch (Exception ex)
	//    {

	//        lblmsg.Text = ex.Message;
	//        lblmsg.ForeColor = System.Drawing.Color.Red;
	//        lblmsg.Visible = true;
	//    }

	//}


	public void getgrid()
	{

		try
		{
			dt0 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
			d1 = dt0.ToString("MM/dd/yyyy");
			d2 = dt2.ToString("MM/dd/yyyy");
			SqlConnection con = new SqlConnection(connStr);
			//   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
			string str, str1;
			str1 = "select  Distinct(convert(varchar,Prdate,103)) as Date from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by Date";
			DataTable dat = new DataTable();
			SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
			da1.Fill(dat);
			//  str = "select   ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='155' and route_id='9'   and prdate between '1-1-2016' and '1-7-2016' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='155' and route_id='9' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";
			str = "select  ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";

			//SqlCommand cmd = new SqlCommand(str, con);
			DataTable dt = new DataTable();

			SqlDataAdapter da = new SqlDataAdapter(str, con);
			da.Fill(dt);

			DataTable dt1 = new DataTable();
			DataColumn dc = new DataColumn();
			DataRow dr;
			int count = dt.Rows.Count;

			if (dt.Rows.Count > 0)
			{

				dc = new DataColumn("AgentName");
				dt1.Columns.Add(dc);
				foreach (DataRow dr1 in dat.Rows)
				{
					columncount = columncount + 1;
					object id;
					id = dr1[0].ToString();
					//   string columnName = "D-" + id;
					string columnName = "" + id;
					DataColumnCollection columns = dt1.Columns;
					if (columns.Contains(columnName))
					{

					}
					else
					{
						//   dc = new DataColumn("D-" + id);
						dc = new DataColumn("" + id);
						dt1.Columns.Add(dc);
					}
				}

				if (count > 0)
				{
					object id2;
					id2 = 0;
					int idd2 = Convert.ToInt32(id2);

					foreach (DataRow dr2 in dt.Rows)
					{
						dr = dt1.NewRow();

						object id1;
						id1 = dr2[3].ToString();
						int idd1 = Convert.ToInt32(id1);
						if (idd1 == idd2)
						{

						}
						else
						{
							int cc = 0;

							foreach (DataRow dr3 in dt.Rows)
							{
								object id3;
								id3 = dr3[3].ToString();
								int idd3 = Convert.ToInt32(id3);
								if (idd1 == idd3)
								{
									int ff = 0;
									if (cc == 0)
									{
										dr[cc] = dr3[1].ToString();
										cc++;
										//                                      
										foreach (DataRow dr4 in dat.Rows)
										{
											string s = dr4[0].ToString();
											string s1 = dr3[2].ToString();

											if (s == s1)
											{
												ff++;
												dr[ff] = dr3[4].ToString();
											}
											else
											{
												ff++;
											}
										}

										//

										cc++;
									}
									else
									{
										//

										foreach (DataRow dr4 in dat.Rows)
										{
											string s = dr4[0].ToString();
											string s1 = dr3[2].ToString();
											if (s == s1)
											{
												ff++;
												dr[ff] = dr3[4].ToString();
											}
											else
											{
												ff++;
											}

										}

										//
										// dr[ff] = dr3[4].ToString();
										cc++;
									}
									idd2 = idd3;
								}
							}
							dt1.Rows.Add(dr);
						}

					}
				}
				GridView1.DataSource = dt1;
				GridView1.DataBind();


				if (dt1.Rows.Count > 0)
				{
					int sumcountcheck = 0;
					foreach (DataColumn dc1 in dt1.Columns)
					{
						if (sumcountcheck == 0)
						{
							GridView1.FooterRow.Cells[0].Text = "Total";
							GridView1.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;

						}
						else if (sumcountcheck > 0)
						{
							decimal Gtotalcommon = 0;

							foreach (DataRow dr1 in dt1.Rows)
							{
								object id;
								id = dr1[sumcountcheck].ToString();
								if (id == "")
								{
									id = 0;
								}
								decimal idd = Convert.ToDecimal(id);
								Gtotalcommon = Gtotalcommon + idd;
								GridView1.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Right;
								GridView1.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
								GridView1.FooterRow.HorizontalAlign = HorizontalAlign.Right;
							}

						}
						else
						{

						}
						sumcountcheck++;
					}

					GridView1.FooterStyle.ForeColor = System.Drawing.Color.Black;
					GridView1.FooterStyle.BackColor = System.Drawing.Color.Silver;
					GridView1.FooterStyle.Font.Bold = true;
				}


                if (RadioButtonList1.SelectedItem.Value == "3")
                {
                    lbldis.Text = "DayWiSe" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;
                }
                if (RadioButtonList1.SelectedItem.Value == "1")
                {
                    lbldis.Text = "Shift AM" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;


                }

                if (RadioButtonList1.SelectedItem.Value == "2")
                {
                    lbldis.Text = "Shift PM" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;


                }
				//for (int j = 2; j < columncount; j++)
				//{
				//    double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("milk_kg"));
				//    GridView1.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
				//    GridView1.FooterRow.Cells[j].Text = milkkg.ToString("N2");

				//}



				//GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
				//GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
				//GridView1.FooterRow.Cells[2].Text = "Total";
				//GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
				//GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
				//GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;

				//GridView1.DataSource = ConvertColumnsAsRows(dt);
				//GridView1.DataBind();
				//GridView1.HeaderRow.Visible = false;

				////GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
				////GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
				////GridView1.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
				////GridView1.FooterRow.Cells[2].Text = "Total";
				////GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
				//////GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
				//////GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;
				////double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("MilkKg"));
				////GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
				////GridView1.FooterRow.Cells[3].Text = milkkg.ToString("N2");

				////double milkltr = dt.AsEnumerable().Sum(row => row.Field<double>("MilkLtr"));
				////GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
				////GridView1.FooterRow.Cells[4].Text = milkltr.ToString("N2");

				////decimal netamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"));
				////GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
				////GridView1.FooterRow.Cells[5].Text = netamount.ToString("N2");


			}
			else
			{
				GridView1.DataSource = null;
				GridView1.DataBind();
				lblmsg.Text = "NO Data Particular Date or Please Check Your Date";
				lblmsg.ForeColor = System.Drawing.Color.Red;
				lblmsg.Visible = true;

			}

		}

		catch (Exception ex)
		{

			lblmsg.Text = ex.Message;
			lblmsg.ForeColor = System.Drawing.Color.Red;
			lblmsg.Visible = true;
		}

	}

    public void getgridAM()
    {

        try
        {
            dt0 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            d1 = dt0.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");
            SqlConnection con = new SqlConnection(connStr);
            //   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
            string str, str1;
            str1 = "select  Distinct(convert(varchar,Prdate,103)) as Date from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "'  AND Sessions='AM' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by Date";
            DataTable dat = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
            da1.Fill(dat);
            //  str = "select   ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='155' and route_id='9'   and prdate between '1-1-2016' and '1-7-2016' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='155' and route_id='9' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";
            str = "select  ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "'   AND Sessions='AM' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";

            //SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);

            DataTable dt1 = new DataTable();
            DataColumn dc = new DataColumn();
            DataRow dr;
            int count = dt.Rows.Count;

            if (dt.Rows.Count > 0)
            {

                dc = new DataColumn("AgentName");
                dt1.Columns.Add(dc);
                foreach (DataRow dr1 in dat.Rows)
                {
                    columncount = columncount + 1;
                    object id;
                    id = dr1[0].ToString();
                    //   string columnName = "D-" + id;
                    string columnName = "" + id;
                    DataColumnCollection columns = dt1.Columns;
                    if (columns.Contains(columnName))
                    {

                    }
                    else
                    {
                        //   dc = new DataColumn("D-" + id);
                        dc = new DataColumn("" + id);
                        dt1.Columns.Add(dc);
                    }
                }

                if (count > 0)
                {
                    object id2;
                    id2 = 0;
                    int idd2 = Convert.ToInt32(id2);

                    foreach (DataRow dr2 in dt.Rows)
                    {
                        dr = dt1.NewRow();

                        object id1;
                        id1 = dr2[3].ToString();
                        int idd1 = Convert.ToInt32(id1);
                        if (idd1 == idd2)
                        {

                        }
                        else
                        {
                            int cc = 0;

                            foreach (DataRow dr3 in dt.Rows)
                            {
                                object id3;
                                id3 = dr3[3].ToString();
                                int idd3 = Convert.ToInt32(id3);
                                if (idd1 == idd3)
                                {
                                    int ff = 0;
                                    if (cc == 0)
                                    {
                                        dr[cc] = dr3[1].ToString();
                                        cc++;
                                        //                                      
                                        foreach (DataRow dr4 in dat.Rows)
                                        {
                                            string s = dr4[0].ToString();
                                            string s1 = dr3[2].ToString();

                                            if (s == s1)
                                            {
                                                ff++;
                                                dr[ff] = dr3[4].ToString();
                                            }
                                            else
                                            {
                                                ff++;
                                            }
                                        }

                                        //

                                        cc++;
                                    }
                                    else
                                    {
                                        //

                                        foreach (DataRow dr4 in dat.Rows)
                                        {
                                            string s = dr4[0].ToString();
                                            string s1 = dr3[2].ToString();
                                            if (s == s1)
                                            {
                                                ff++;
                                                dr[ff] = dr3[4].ToString();
                                            }
                                            else
                                            {
                                                ff++;
                                            }

                                        }

                                        //
                                        // dr[ff] = dr3[4].ToString();
                                        cc++;
                                    }
                                    idd2 = idd3;
                                }
                            }
                            dt1.Rows.Add(dr);
                        }

                    }
                }
                GridView1.DataSource = dt1;
                GridView1.DataBind();


                if (dt1.Rows.Count > 0)
                {
                    int sumcountcheck = 0;
                    foreach (DataColumn dc1 in dt1.Columns)
                    {
                        if (sumcountcheck == 0)
                        {
                            GridView1.FooterRow.Cells[0].Text = "Total";
                            GridView1.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                        }
                        else if (sumcountcheck > 0)
                        {
                            decimal Gtotalcommon = 0;

                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                object id;
                                id = dr1[sumcountcheck].ToString();
                                if (id == "")
                                {
                                    id = 0;
                                }
                                decimal idd = Convert.ToDecimal(id);
                                Gtotalcommon = Gtotalcommon + idd;
                                GridView1.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
                                GridView1.FooterRow.HorizontalAlign = HorizontalAlign.Right;
                            }

                        }
                        else
                        {

                        }
                        sumcountcheck++;
                    }

                    GridView1.FooterStyle.ForeColor = System.Drawing.Color.Black;
                    GridView1.FooterStyle.BackColor = System.Drawing.Color.Silver;
                    GridView1.FooterStyle.Font.Bold = true;
                }


                if (RadioButtonList1.SelectedItem.Value == "3")
                {
                    lbldis.Text = "DayWiSe" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;
                }
                if (RadioButtonList1.SelectedItem.Value == "1")
                {
                    lbldis.Text = "Shift AM" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;


                }

                if (RadioButtonList1.SelectedItem.Value == "2")
                {
                    lbldis.Text = "Shift PM" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;


                }
                //for (int j = 2; j < columncount; j++)
                //{
                //    double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("milk_kg"));
                //    GridView1.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                //    GridView1.FooterRow.Cells[j].Text = milkkg.ToString("N2");

                //}



                //GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                //GridView1.FooterRow.Cells[2].Text = "Total";
                //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                //GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
                //GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;

                //GridView1.DataSource = ConvertColumnsAsRows(dt);
                //GridView1.DataBind();
                //GridView1.HeaderRow.Visible = false;

                ////GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[2].Text = "Total";
                ////GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                //////GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
                //////GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;
                ////double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("MilkKg"));
                ////GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[3].Text = milkkg.ToString("N2");

                ////double milkltr = dt.AsEnumerable().Sum(row => row.Field<double>("MilkLtr"));
                ////GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[4].Text = milkltr.ToString("N2");

                ////decimal netamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"));
                ////GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[5].Text = netamount.ToString("N2");


            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblmsg.Text = "NO Data Particular Date or Please Check Your Date";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Visible = true;

            }

        }

        catch (Exception ex)
        {

            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
        }

    }

    public void getgridPM()
    {

        try
        {
            dt0 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            d1 = dt0.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");
            SqlConnection con = new SqlConnection(connStr);
            //   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
            string str, str1;
            str1 = "select  Distinct(convert(varchar,Prdate,103)) as Date from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "'  AND Sessions='PM' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by Date";
            DataTable dat = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(str1, con);
            da1.Fill(dat);
            //  str = "select   ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='155' and route_id='9'   and prdate between '1-1-2016' and '1-7-2016' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='155' and route_id='9' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";
            str = "select  ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "'   AND Sessions='PM' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";

            //SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);

            DataTable dt1 = new DataTable();
            DataColumn dc = new DataColumn();
            DataRow dr;
            int count = dt.Rows.Count;

            if (dt.Rows.Count > 0)
            {

                dc = new DataColumn("AgentName");
                dt1.Columns.Add(dc);
                foreach (DataRow dr1 in dat.Rows)
                {
                    columncount = columncount + 1;
                    object id;
                    id = dr1[0].ToString();
                    //   string columnName = "D-" + id;
                    string columnName = "" + id;
                    DataColumnCollection columns = dt1.Columns;
                    if (columns.Contains(columnName))
                    {

                    }
                    else
                    {
                        //   dc = new DataColumn("D-" + id);
                        dc = new DataColumn("" + id);
                        dt1.Columns.Add(dc);
                    }
                }

                if (count > 0)
                {
                    object id2;
                    id2 = 0;
                    int idd2 = Convert.ToInt32(id2);

                    foreach (DataRow dr2 in dt.Rows)
                    {
                        dr = dt1.NewRow();

                        object id1;
                        id1 = dr2[3].ToString();
                        int idd1 = Convert.ToInt32(id1);
                        if (idd1 == idd2)
                        {

                        }
                        else
                        {
                            int cc = 0;

                            foreach (DataRow dr3 in dt.Rows)
                            {
                                object id3;
                                id3 = dr3[3].ToString();
                                int idd3 = Convert.ToInt32(id3);
                                if (idd1 == idd3)
                                {
                                    int ff = 0;
                                    if (cc == 0)
                                    {
                                        dr[cc] = dr3[1].ToString();
                                        cc++;
                                        //                                      
                                        foreach (DataRow dr4 in dat.Rows)
                                        {
                                            string s = dr4[0].ToString();
                                            string s1 = dr3[2].ToString();

                                            if (s == s1)
                                            {
                                                ff++;
                                                dr[ff] = dr3[4].ToString();
                                            }
                                            else
                                            {
                                                ff++;
                                            }
                                        }

                                        //

                                        cc++;
                                    }
                                    else
                                    {
                                        //

                                        foreach (DataRow dr4 in dat.Rows)
                                        {
                                            string s = dr4[0].ToString();
                                            string s1 = dr3[2].ToString();
                                            if (s == s1)
                                            {
                                                ff++;
                                                dr[ff] = dr3[4].ToString();
                                            }
                                            else
                                            {
                                                ff++;
                                            }

                                        }

                                        //
                                        // dr[ff] = dr3[4].ToString();
                                        cc++;
                                    }
                                    idd2 = idd3;
                                }
                            }
                            dt1.Rows.Add(dr);
                        }

                    }
                }
                GridView1.DataSource = dt1;
                GridView1.DataBind();


                if (dt1.Rows.Count > 0)
                {
                    int sumcountcheck = 0;
                    foreach (DataColumn dc1 in dt1.Columns)
                    {
                        if (sumcountcheck == 0)
                        {
                            GridView1.FooterRow.Cells[0].Text = "Total";
                            GridView1.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;

                        }
                        else if (sumcountcheck > 0)
                        {
                            decimal Gtotalcommon = 0;

                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                object id;
                                id = dr1[sumcountcheck].ToString();
                                if (id == "")
                                {
                                    id = 0;
                                }
                                decimal idd = Convert.ToDecimal(id);
                                Gtotalcommon = Gtotalcommon + idd;
                                GridView1.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Right;
                                GridView1.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
                                GridView1.FooterRow.HorizontalAlign = HorizontalAlign.Right;
                            }

                        }
                        else
                        {

                        }
                        sumcountcheck++;
                    }

                    GridView1.FooterStyle.ForeColor = System.Drawing.Color.Black;
                    GridView1.FooterStyle.BackColor = System.Drawing.Color.Silver;
                    GridView1.FooterStyle.Font.Bold = true;
                }


                if (RadioButtonList1.SelectedItem.Value == "3")
                {
                    lbldis.Text = "DayWiSe" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;
                }
                if (RadioButtonList1.SelectedItem.Value == "1")
                {
                    lbldis.Text = "Shift AM" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;


                }

                if (RadioButtonList1.SelectedItem.Value == "2")
                {
                    lbldis.Text = "Shift PM" + "Plant Name:" + ddl_Plantname.Text + " Route Name: " + ddl_RouteName.Text;
                    lblmsg.Visible = false;
                    lbldis.Visible = true;


                }
                //for (int j = 2; j < columncount; j++)
                //{
                //    double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("milk_kg"));
                //    GridView1.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                //    GridView1.FooterRow.Cells[j].Text = milkkg.ToString("N2");

                //}



                //GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                //GridView1.FooterRow.Cells[2].Text = "Total";
                //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                //GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
                //GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;

                //GridView1.DataSource = ConvertColumnsAsRows(dt);
                //GridView1.DataBind();
                //GridView1.HeaderRow.Visible = false;

                ////GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[2].Text = "Total";
                ////GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                //////GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
                //////GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;
                ////double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("MilkKg"));
                ////GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[3].Text = milkkg.ToString("N2");

                ////double milkltr = dt.AsEnumerable().Sum(row => row.Field<double>("MilkLtr"));
                ////GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[4].Text = milkltr.ToString("N2");

                ////decimal netamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"));
                ////GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                ////GridView1.FooterRow.Cells[5].Text = netamount.ToString("N2");


            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblmsg.Text = "NO Data Particular Date or Please Check Your Date";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Visible = true;

            }

        }

        catch (Exception ex)
        {

            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
        }

    }

	//public void getgrid()
	//{

	//    try
	//    {



	//        dt0 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
	//        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
	//        d1 = dt0.ToString("MM/dd/yyyy");
	//        d2 = dt2.ToString("MM/dd/yyyy");
	//        SqlConnection con = new SqlConnection(connStr);
	//        //   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
	//        string str;
	//        //   str = "SELECT  ROW_NUMBER() OVER (ORDER BY ManualDate) AS [Sno], convert(varchar,ManualDate,103) as ManualDate,MannualSession as Session,GivererName as Given,RequsterName AS Request,ReasonForMannual AS ReasonForManual    FROM  MannualSettings  where plant_code='" + pcode + "' and    ManualDate between '" + d1 + "'   and  '" + d2 + "'  order by ManualDate";
	//      //  str = "SELECT Agent_id AS AgentId,Agent_name as AgentName,Smkg MilkKg,Smltr AS MilkLtr,CAST(FLOOR(NetAmount) AS DECIMAL(18,2)) AS NetAmount  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1 + "' AND To_date='" + d2 + "' ORDER BY RAND(Agent_id)";

	//     //   str = "select agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date, Milk_ltr,milk_kg from ( Select Agent_id,prdate,Milk_ltr,milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "' ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master) as am on  pro.Agent_id=am.amAgentid";

	//       // str = "select agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,Milk_ltr,milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "' ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";
	//        str = "select SELECT ROW_NUMBER() OVER(ORDER BY Agent_id ) AS Sno,agent_id + '_' + agent_name as AgentName,convert(varchar,Prdate,103) as Date,Agent_id,milk_kg from ( Select Agent_id,prdate,SUM(Milk_ltr) AS Milk_ltr,SUM(milk_kg) AS Milk_kg   from procurement where plant_code='" + plantcode + "' and route_id='" + rid + "'   and prdate between '" + d1 + "' and '" + d2 + "' GROUP BY Agent_id,prdate ) as pro left join (select Agent_id as amAgentid,Agent_Name  from agent_master where plant_code='" + plantcode + "' and route_id='" + rid + "' ) as am on  pro.Agent_id=am.amAgentid  order by pro.Agent_id,Date";

	//        SqlCommand cmd = new SqlCommand(str, con);
	//        DataTable dt = new DataTable();
	//        cmd.CommandTimeout = 500;
	//        cmd.CommandType = CommandType.Text;
	//        SqlDataAdapter da = new SqlDataAdapter(cmd);
	//        da.Fill(dt);

	//        DataTable dt1 = new DataTable();
	//        DataColumn dc = new DataColumn();
	//        DataRow dr;
	//        int count = dt.Rows.Count;

	//        if (dt.Rows.Count > 0)
	//        {

	//            dc = new DataColumn("AgentName");
	//            dt1.Columns.Add(dc);
	//            foreach (DataRow dr1 in dt.Rows)
	//            {

	//                columncount = columncount + 1;

	//                object id;
	//                id = dr1[1].ToString();
	//             //   string columnName = "D-" + id;
	//                string columnName = "" + id;
	//                DataColumnCollection columns = dt1.Columns;

	//                 if (columns.Contains(columnName))
	//                        {

	//                        }
	//                        else
	//                        {
	//                         //   dc = new DataColumn("D-" + id);
	//                            dc = new DataColumn("" + id);
	//                            dt1.Columns.Add(dc);
	//                        }

	//                    }




	//            if (count > 0)
	//            {
	//                object id2;
	//                id2 = 0;
	//                int idd2 = Convert.ToInt32(id2);

	//                foreach (DataRow dr2 in dt.Rows)
	//                {
	//                    dr = dt1.NewRow();

	//                    object id1;
	//                    id1 = dr2[2].ToString();
	//                    int idd1 = Convert.ToInt32(id1);
	//                    if (idd1 == idd2)
	//                    {

	//                    }
	//                    else
	//                    {
	//                        int cc = 0;
	//                        foreach (DataRow dr3 in dt.Rows)
	//                        {
	//                            object id3;
	//                            id3 = dr3[2].ToString();
	//                            int idd3 = Convert.ToInt32(id3);
	//                            if (idd1 == idd3)
	//                            {
	//                                if (cc == 0)
	//                                {
	//                                    dr[cc] = dr3[0].ToString();
	//                                    cc++;


	//                                    //foreach (DataRow dr4 in dt1.Rows)
	//                                    //{

	//                                    //     checkdate = dr[1].ToString();
	//                                    //     checkdate1 = dr3[1].ToString();

	//                                    //     if (checkdate == checkdate1)
	//                                    //    {
	//                                    //        dr[cc] = dr3[3].ToString();
	//                                    //        // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
	//                                    //        cc++;

	//                                    //    }

	//                                    //}

	//                                    ////dr[cc] = dr3[3].ToString();
	//                                    ////// ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
	//                                    ////cc++;

	//                                }
	//                                else
	//                                {


	//                                    //foreach (DataRow dr4 in dt1.Rows)
	//                                    //{

	//                                    //    checkdate = dr[1].ToString();
	//                                    //    checkdate1 = dr3[1].ToString();

	//                                    //    if (checkdate == checkdate1)
	//                                    //    {
	//                                    //        dr[cc] = dr3[3].ToString();
	//                                    //        // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
	//                                    //        cc++;

	//                                    //    }

	//                                    //}











	//                                    dr[cc] = dr3[3].ToString();
	//                                    // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
	//                                    cc++;

	//                                }
	//                                idd2 = idd3;
	//                            }
	//                        }
	//                        dt1.Rows.Add(dr);
	//                    }

	//                }
	//            }



	//            lbldis.Text = "Plant Name:" +  ddl_Plantname.Text +  "--- Route Name:"  +  ddl_RouteName.Text  ;
	//            lbldis.Visible = true;
	//            GridView1.DataSource = dt1;
	//            GridView1.DataBind();
	//            lblmsg.Visible = false;
	//            //GridView1.DataSource = ConvertColumnsAsRows(dt);
	//            //GridView1.DataBind();
	//            //GridView1.HeaderRow.Visible = false;

	//            ////GridView1.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.Columns[4].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.Columns[5].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[2].Text = "Total";
	//            ////GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
	//            //////GridView1.HeaderStyle.BackColor = System.Drawing.Color.Silver;
	//            //////GridView1.HeaderStyle.ForeColor = System.Drawing.Color.Brown;
	//            ////double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("MilkKg"));
	//            ////GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[3].Text = milkkg.ToString("N2");

	//            ////double milkltr = dt.AsEnumerable().Sum(row => row.Field<double>("MilkLtr"));
	//            ////GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[4].Text = milkltr.ToString("N2");

	//            ////decimal netamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"));
	//            ////GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
	//            ////GridView1.FooterRow.Cells[5].Text = netamount.ToString("N2");


	//        }
	//        else
	//        {
	//            GridView1.DataSource = null;
	//            GridView1.DataBind();
	//            lblmsg.Text = "NO Data Particular Date or Please Check Your Date";
	//            lblmsg.ForeColor = System.Drawing.Color.Red;
	//            lblmsg.Visible = true;

	//        }

	//    }

	//    catch (Exception ex)
	//    {

	//        lblmsg.Text = ex.Message;
	//        lblmsg.ForeColor = System.Drawing.Color.Red;
	//        lblmsg.Visible = true;
	//    }

	//}

	//public DataTable ConvertColumnsAsRows(DataTable dt)
	//{
	//    //DataTable dtnew = new DataTable();
	//    ////Convert all the rows to columns
	//    //for (int i = 0; i <= dt.Rows.Count; i++)
	//    //{
	//    //    dtnew.Columns.Add(Convert.ToString(i));
	//    //}
	//    //DataRow dr;
	//    //// Convert All the Columns to Rows
	//    //for (int j = 0; j < dt.Columns.Count; j++)
	//    //{
	//    //    dr = dtnew.NewRow();
	//    //    dr[0] = dt.Columns[j].ToString();
	//    //    for (int k = 1; k <= dt.Rows.Count; k++)
	//    //        dr[k] = dt.Rows[k - 1][j];
	//    //    dtnew.Rows.Add(dr);
	//    //}
	//    //return dtnew;
	//}
	protected void Button3_Click(object sender, EventArgs e)
	{
		try
		{

			Response.Clear();
			Response.Buffer = true;
			string filename = "'" + ddl_Plantname.Text + "' ----  '" + ddl_RouteName.Text + "' " + DateTime.Now.ToString() + ".xls";

			Response.AddHeader("content-disposition", "attachment;filename=" + filename);
			// Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
			Response.Charset = "";
			Response.ContentType = "application/vnd.ms-excel";
			using (StringWriter sw = new StringWriter())
			{
				HtmlTextWriter hw = new HtmlTextWriter(sw);

				//To Export all pages
				GridView1.AllowPaging = false;
				getgrid();

				GridView1.HeaderRow.BackColor = Color.White;
				foreach (TableCell cell in GridView1.HeaderRow.Cells)
				{
					cell.BackColor = GridView1.HeaderStyle.BackColor;
				}
				foreach (GridViewRow row in GridView1.Rows)
				{
					row.BackColor = Color.White;
					foreach (TableCell cell in row.Cells)
					{
						if (row.RowIndex % 2 == 0)
						{
							cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
						}
						else
						{
							cell.BackColor = GridView1.RowStyle.BackColor;
						}
						cell.CssClass = "textmode";
					}
				}

				GridView1.RenderControl(hw);

				//style to format numbers to string
				string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
				// string style = @"<style> .textmode { } </style>";
				Response.Write(style);
				Response.Output.Write(sw.ToString());
				Response.Flush();
				Response.End();
			}
		}

		catch (Exception ex)
		{

			lblmsg.Text = ex.Message;
			lblmsg.ForeColor = System.Drawing.Color.Red;
			lblmsg.Visible = true;
		}

	}
	protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
		rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
	}
	protected void Button4_Click(object sender, EventArgs e)
	{

	}

	public override void VerifyRenderingInServerForm(Control control)
	{
		//
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		//if (e.Row.RowType == DataControlRowType.DataRow)
		//{
		//    e.Row.Cells[0].CssClass = "gridcss";
		//}

		 
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;


		}


	}
	protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{
		
	}

}