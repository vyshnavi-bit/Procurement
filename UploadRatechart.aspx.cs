using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using System.Configuration;


public partial class UploadRatechart : System.Web.UI.Page
{
	DataTable dt = new DataTable();
	DataTable dtt = new DataTable();
	DataTable dt1 = new DataTable();
	DataTable dtt1 = new DataTable();
	OleDbConnection Econ;
	SqlConnection con;
	string constr, Query, sqlconn;
	string getoldbcon;
	string str;
	int chartexists;
	int ratechartexists;
	int comparechart;
	string message;
	int msgvalue;
	int rateorchart;
	string str8;
	string str6;
	int count;
	int count1;
	OleDbConnection OleDbcon = new OleDbConnection();
	string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack == true)
		{
			Panel1.Visible = false;
			Panel2.Visible = false;
		}
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{

	}
	protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	{

	}
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{

	}
	protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
	{

	}
	protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{

	}
	protected void btn_Insert_Click(object sender, EventArgs e)
	{
		try
		{
			dtt1.Columns.AddRange(new DataColumn[10] { new DataColumn("Table_ID"), new DataColumn("Chart_Name"), new DataColumn("From_RangeValue"), new DataColumn("To_RangeValue"), new DataColumn("Rate"), new DataColumn("Comission_Amount"), new DataColumn("Bouns_Amount"), new DataColumn("Plant_Code"), new DataColumn("Company_Code"), new DataColumn("Status") });
			foreach (GridViewRow row in GridView1.Rows)
			{
				rateorchart = 1;
				int Table_ID = 1;
				str = row.Cells[1].Text;
				str6 = row.Cells[7].Text;
				if (count1 == 0)
				{
					getchartnameexists();
					getchartnameexistsratechart();
				}
				if ((ratechartexists == 1) && (chartexists == 0))
				{
					count1 = 1;
					string str1 = row.Cells[2].Text;
					string str2 = row.Cells[3].Text;
					string str3 = row.Cells[4].Text;
					string str4 = row.Cells[5].Text;
					string str5 = row.Cells[6].Text;

					string str7 = row.Cells[8].Text;
					string str8 = row.Cells[9].Text;
					dtt1.Rows.Add(Table_ID, str, str1, str2, str3, str4, str5, str6, str7, str8);

				}
				else
				{
					msgvalue = 1;
					msgbox();


				}

			}
			if (chartexists == 0)
			{
				if (dtt1.Rows.Count > 0)
				{
					string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
					using (SqlConnection con = new SqlConnection(connStr))
					{
						using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
						{
							//Set the database table name
							sqlBulkCopy.DestinationTableName = "dbo.Rate_Chart";

							//[OPTIONAL]: Map the DataTable columns with that of the database table
							con.Open();
							sqlBulkCopy.WriteToServer(dtt1);
							con.Close();
							msgvalue = 2;

						}
					}
				}
			}
			else
			{


				msgvalue = 1;
			}
			msgbox();
			GridView1.DataSource = null;
            GridView1.DataBind();
		}
		catch (Exception ex)
		{
			msgvalue = 0;
			message = ex.Message.ToString();

		}
	}
	protected void ddl_PlantName_PreRender(object sender, EventArgs e)
	{

	}
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{

	}

	private void ExcelConn(string FilePath)
	{

		constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
		Econ = new OleDbConnection(constr);

	}
	protected void btnImport_Click(object sender, EventArgs e)
	{
		ImporttoDatatable();



	}
	protected void btn_Insert11_Click(object sender, System.EventArgs e)
	{
		try
		{
			if (RadioButtonList1.SelectedValue == "1")
			{
				dt.Rows.Clear();
				dt1.Rows.Clear();
				Panel2.Visible = true;
				Panel1.Visible = false;

			}
			if (RadioButtonList1.SelectedValue == "2")
			{
				dt.Rows.Clear();
				dt1.Rows.Clear();
				Panel1.Visible = true;
				Panel2.Visible = false;
			}
			GridView1.DataSource = null;
			GridView1.DataBind();
			GridView2.DataSource = null;
			GridView2.DataBind();
		}
		catch (Exception ex)
		{
			msgvalue = 0;
			message = ex.Message.ToString();

		}


	}
	protected void Button2_Click(object sender, System.EventArgs e)
	{
		ImporttoDatatable1();

	}
	protected void btn_Insert3_Click(object sender, System.EventArgs e)
	{
		try
		{
			ImporttoDatatable1();
			dtt.Columns.AddRange(new DataColumn[12] { new DataColumn("Table_ID"), new DataColumn("Chart_Name"), new DataColumn("Chart_Type"), new DataColumn("Milk_Nature"), new DataColumn("State_id"), new DataColumn("Min_Fat"), new DataColumn("Min_Snf"), new DataColumn("From_Date"), new DataColumn("To_Date"), new DataColumn("Plant_Code"), new DataColumn("Company_Code"), new DataColumn("Status") });
			foreach (GridViewRow row in GridView2.Rows)
			{
				rateorchart = 2;
				int Table_ID = 1;
				str = row.Cells[1].Text;
				str8 = row.Cells[9].Text;
				if (count == 0)
				{
					getchartnameexistsratechart();
				}
				if (chartexists != 0)
				{
					count = 1;
					string str1 = row.Cells[2].Text;
					string str2 = row.Cells[3].Text;
					int str3 = Convert.ToInt16(row.Cells[4].Text);
					double str4 = Convert.ToDouble(row.Cells[5].Text);
					double str5 = Convert.ToDouble(row.Cells[6].Text);
					string[] p = row.Cells[7].Text.Split('/');
					string[] pp = row.Cells[7].Text.Split('/');
					string str6 = p[1] + "/" + p[0] + "/" + p[2];
					string str7 = pp[1] + "/" + pp[0] + "/" + pp[2];
					string str9 = row.Cells[10].Text;
					int str10 = Convert.ToInt16(row.Cells[11].Text);
					dtt.Rows.Add(Table_ID, str, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10);
				}
				else
				{
					msgvalue = 1;
					msgbox();


				}

			}


			if (chartexists == 1)
			{
				if (GridView2.Rows.Count > 0)
				{
					string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
					using (SqlConnection con = new SqlConnection(connStr))
					{
						using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
						{
							//Set the database table name
							sqlBulkCopy.DestinationTableName = "dbo.CHART_MASTER";

							//[OPTIONAL]: Map the DataTable columns with that of the database table
							con.Open();
							sqlBulkCopy.WriteToServer(dtt);
							con.Close();
							msgvalue = 2;
						}
					}
				}
			}
			else
			{


				msgvalue = 1;
			}
			msgbox();
			GridView2.DataSource = null;
			GridView2.DataBind();
		}

		catch (Exception ex)
		{
			msgvalue = 0;
			message = ex.Message.ToString();
			//msgbox();
		}
	}


	public void msgbox()
	{


		if (msgvalue == 1)
		{

			message = "RateChart Name Check or Already Available  ";
		}
		if (msgvalue == 2)
		{
			message = "Record Inserted SuccessFully";

		}
		if (msgvalue == 0)
		{
			message = message.ToString();

		}


		string script = "window.onload = function(){ alert('";
		script += message;
		script += "')};";
		ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

	}

	public void ImporttoDatatable()
	{
		try
		{
			string ConStr = "";
			//Extantion of the file upload control saving into ext because   
			//there are two types of extation .xls and .xlsx of Excel   
			string ext = Path.GetExtension(fileupload1.FileName).ToLower();
			//getting the path of the file   
			string path = Server.MapPath("~/Ratechart1/" + fileupload1.FileName);
			//saving the file inside the MyFolder of the server  
			fileupload1.SaveAs(path);
			Label1.Text = fileupload1.FileName + "\'s Data showing into the GridView";
			//checking that extantion is .xls or .xlsx  
			if (ext.Trim() == ".xls")
			{
				//connection string for that file which extantion is .xls  
				ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
			}
			else if (ext.Trim() == ".xlsx")
			{
				//connection string for that file which extantion is .xlsx  
				ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
			}
			//making query  
			string query = "SELECT Chart_Name as ChartName,From_RangeValue as FromRange,To_RangeValue as ToRangee,Rate,Comission_Amount as CommAmt,Bouns_Amount as BonAmt,Plant_Code as plantcode,Company_Code as Ccode,Status FROM [Sheet1$]";
			//Providing connection  
			OleDbConnection conn = new OleDbConnection(ConStr);
			//checking that connection state is closed or not if closed the   
			//open the connection  
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			//create command object  
			OleDbCommand cmd = new OleDbCommand(query, conn);
			// create a data adapter and get the data into dataadapter  
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			//DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			//fill the Excel data to data set  
			da.Fill(dt);
			//set data source of the grid view  
			GridView1.DataSource = dt;
			//binding the gridview  
			GridView1.DataBind();
			//close the connection  
			conn.Close();
		}
		catch (Exception ex)
		{
			msgvalue = 0;
			message = ex.Message.ToString();

		}
	}
	public void ImporttoDatatable1()
	{
		try
		{
			string ConStr = "";
			//Extantion of the file upload control saving into ext because   
			//there are two types of extation .xls and .xlsx of Excel   
			string ext = Path.GetExtension(fileupload3.FileName).ToLower();
			//getting the path of the file   
			string path = Server.MapPath("~/Ratechartchart/" + fileupload3.FileName);
			//saving the file inside the MyFolder of the server  
			fileupload3.SaveAs(path);
			Label1.Text = fileupload3.FileName + "\'s Data showing into the GridView";
			//checking that extantion is .xls or .xlsx  
			if (ext.Trim() == ".xls")
			{
				//connection string for that file which extantion is .xls  
				ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
			}
			else if (ext.Trim() == ".xlsx")
			{
				//connection string for that file which extantion is .xlsx  
				ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
			}
			//making query  
			string query = "SELECT Chart_Name as ChartName,Chart_Type as ChartType,Milk_Nature as Nature,State_id as Stateid,Min_Fat as Minfat,Min_Snf as MinSnf,From_Date as Fromdate,To_Date as Todate,Plant_Code as Plantcode,Company_Code as Ccode,Status FROM [Sheet1$]";
			//Providing connection  
			OleDbConnection conn = new OleDbConnection(ConStr);
			//checking that connection state is closed or not if closed the   
			//open the connection  
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			//create command object  
			OleDbCommand cmd1 = new OleDbCommand(query, conn);
			// create a data adapter and get the data into dataadapter  
			OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
			DataSet ds1 = new DataSet();
			DataTable dt1 = new DataTable();
			//fill the Excel data to data set  
			da1.Fill(dt1);
			//set data source of the grid view  
			GridView2.DataSource = dt1;
			//binding the gridview  
			GridView2.DataBind();
			//close the connection  
			conn.Close();
		}
		catch (Exception ex)
		{
			msgvalue = 0;
			message = ex.Message.ToString();

		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		ImporttoDatatable();
	}
	public void getchartnameexists()
	{
		try
		{
			string query;
			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection con = new SqlConnection(connStr);

			query = "select chart_name  from Rate_Chart   where plant_code='" + str6 + "' and  chart_name='" + str + "'";
			SqlCommand cmd = new SqlCommand(query, con);
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{

				ratechartexists = 0;

			}
			else
			{
				ratechartexists = 1;

			}

		}


		catch (Exception ex)
		{
			msgvalue = 0;
			message = ex.Message.ToString();

		}



	}
	public void getchartnameexistsratechart()
	{
		try
		{
			string query;
			//	string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection con = new SqlConnection(connStr);
			if (rateorchart == 1)
			{
				query = "select chart_name  from chart_master    where   plant_code='" + str6 + "' and  chart_name='" + str + "'";
			}
			else
			{
				query = "select chart_name  from chart_master    where   plant_code='" + str8 + "' and  chart_name='" + str + "'";
			}
			SqlCommand cmd = new SqlCommand(query, con);
			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{

				chartexists = 0;


			}
			else
			{
				chartexists = 1;

			}



		}

		catch (Exception ex)
		{
			msgvalue = 0;
			message = ex.Message.ToString();

		}
	}

	protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
}