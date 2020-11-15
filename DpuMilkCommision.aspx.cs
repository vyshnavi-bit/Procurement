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

public partial class DpuMilkCommision : System.Web.UI.Page
{
	public string ccode;
	public string pcode;
	public string pname;
	public string cname;
	public string frmdate;
	public string todate;
	string getrouteusingagent;
	string routeid;
	string dt1;
	string dt2;
	string ddate;
	DateTime dtm = new DateTime();
	BLLuser Bllusers = new BLLuser();
	string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	string agentname;
	string[] getdate;
  string getfrmdate;
	string gettodate;
string comamt;
string milkltr;
string dpumilkltr;
string dpuampount;
string plantmilkltr;
string plantamount;
public static int roleid;
protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
            if ((Session["Name"] != null) && (Session["pass"] != null))
			{
                roleid = Convert.ToInt32(Session["Role"].ToString());
				ccode = Session["Company_code"].ToString();
				//    pcode = Session["Plant_Code"].ToString();
				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();


				dtm = System.DateTime.Now;
				txt_FromDate.Text = dtm.ToShortDateString();
				txt_ToDate.Text = dtm.ToShortDateString();
				txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
				txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
				//  gettid();

				LoadPlantcode();
				gridview();
			}

			else
			{
				//dtm = System.DateTime.Now;
				//// dti = System.DateTime.Now;
				//txt_FromDate.Text = dtm.ToString("dd/MM/yyy");
				//txt_ToDate.Text = dtm.ToString("dd/MM/yyy");

				pcode = ddl_Plantcode.SelectedItem.Value;
				//  LoadPlantcode();
			//	gridview();
				//  getrouteid();

			}

		}
		else
		{


			pcode = ddl_Plantcode.SelectedItem.Value;
			//  LoadPlantcode();
			//   ddl_agentcode.Text = getrouteusingagent;
		//	gridview();
		}

	}
	protected void btn_ok_Click(object sender, EventArgs e)
	{
if(RadioButtonList1.SelectedValue=="1")
{
	gridview();
	GridView1.Visible = true;
GridView2.Visible=false;


}
if (RadioButtonList1.SelectedValue == "2")
{
	gridview1();
	GridView2.Visible = true;
	GridView1.Visible = false;


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

	public void gridview()
	{

		try
		{
			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");

			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
				//  Plant_code,Plant_Name,convert(varchar,PermissionDate,103) as PermissionDate,convert(varchar,ManualDate,103) as ManualDate,MannualSession,RequsterName,GivererName,ReasonForMannual
				//   string sqlstr = "select *  FROM procurement WHERE plant_code='" + pcode + "'  and agent_id='"+ddl_agentcode.Text+"'  and prdate between '"+d1+"' and '"+d2+"' order by  tid desc";

				string sqlstr = " select (FrmDate + ' ' + ToDate) as BillDate,Plant_Code as Milkltr,Plant_Code as CommAmt  from (select CONVERT(varchar,Bill_frmdate,103) as FrmDate,CONVERT(varchar,Bill_todate,103) as ToDate,Plant_Code  from Bill_date where Plant_Code='" + pcode + "'  and (Bill_frmdate between '" + d1 + "' and '" + d2 + "')  and (Bill_todate between '" + d1 + "' and '" + d2 + "')  group by Bill_frmdate,Bill_todate,Plant_Code) as hh";
				// string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
				SqlCommand COM = new SqlCommand(sqlstr, conn);
				//   SqlDataReader DR = COM.ExecuteReader();
				DataTable dt = new DataTable();
				SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
				sqlDa.Fill(dt);
				if (dt.Rows.Count > 0)
				{






					GridView1.DataSource = dt;
					GridView1.DataBind();


				}
				else
				{

					GridView1.DataSource = null;
					GridView1.DataBind();

				}



			}
		}
		catch
		{



		}
	}
	public void gridview1()
	{

		try
		{
			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");

			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
				//  Plant_code,Plant_Name,convert(varchar,PermissionDate,103) as PermissionDate,convert(varchar,ManualDate,103) as ManualDate,MannualSession,RequsterName,GivererName,ReasonForMannual
				//   string sqlstr = "select *  FROM procurement WHERE plant_code='" + pcode + "'  and agent_id='"+ddl_agentcode.Text+"'  and prdate between '"+d1+"' and '"+d2+"' order by  tid desc";

				string sqlstr = " select (FrmDate + ' ' + ToDate) as BillDate,Plant_Code as DpuMilkLtr,Plant_Code as DpuAmount,Plant_Code as PlantMilkLtr,Plant_Code as PlantAmount,Plant_Code as DiffAmount  from (select CONVERT(varchar,Bill_frmdate,103) as FrmDate,CONVERT(varchar,Bill_todate,103) as ToDate,Plant_Code  from Bill_date where Plant_Code='" + pcode + "'  and (Bill_frmdate between '" + d1 + "' and '" + d2 + "')  and (Bill_todate between '" + d1 + "' and '" + d2 + "')  group by Bill_frmdate,Bill_todate,Plant_Code) as hh";
				// string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
				SqlCommand COM = new SqlCommand(sqlstr, conn);
				//   SqlDataReader DR = COM.ExecuteReader();
				DataTable dt = new DataTable();
				SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
				sqlDa.Fill(dt);
				if (dt.Rows.Count > 0)
				{






					GridView2.DataSource = dt;
					GridView2.DataBind();


				}
				else
				{

					GridView2.DataSource = null;
					GridView2.DataBind();

				}



			}
		}
		catch
		{



		}
	}
	protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		pcode = ddl_Plantcode.SelectedItem.Value;
		getagentdetails();
	}

	public void getagentdetails()
	{
		ddl_agentid.Items.Clear();
		SqlConnection con = new SqlConnection(connStr);
		string str = "";
		str = "select *   from Agent_Master   where Plant_code='" + pcode + "' and DpuAgentStatus=1 ";
		con.Open();
		SqlCommand cmd = new SqlCommand(str, con);
		SqlDataReader dr = cmd.ExecuteReader();
		if (dr.HasRows)
		{

			while (dr.Read())
			{

				ddl_agentid.Items.Add(dr["Agent_Id"].ToString());

			}
		}


	}

	public void getagentprocdetails(string getfrmdate,string gettodate)
	{
		SqlConnection con = new SqlConnection(connStr);
		string str = "";
		str = "select  SUM(milk_ltr) as Ltr,SUM(ComRate) as Rate from  Procurement   where Plant_Code='" + pcode + "' and Agent_id='" + ddl_agentid.Text + "'  and Prdate between '"+getfrmdate +"' and '"+gettodate+"'";	
		con.Open();
		SqlCommand cmd = new SqlCommand(str, con);
		SqlDataReader dr = cmd.ExecuteReader();
		if (dr.HasRows)
		{

			while (dr.Read())
			{

				 milkltr = dr["ltr"].ToString();
if(milkltr=="")
{

milkltr= "0.0";

}
				 comamt = dr["Rate"].ToString();

				 if (comamt == "")
				 {

					 comamt = "0.0";

				 }

			}
		}


	}

	public void getdpumilkandamount(string getfrmdate, string gettodate)
	{
		SqlConnection con = new SqlConnection(connStr);
		string str = "";
		str = "select SUM(milk_ltr) as DpuLtr,sum(Amount) as  DpuAmount   from ProducerProcurement   where plant_code='"+pcode+"' and agent_id='"+ddl_agentid.Text+"'  and Prdate between '" + getfrmdate + "' and '" + gettodate + "'";
		con.Open();
		SqlCommand cmd = new SqlCommand(str, con);
		SqlDataReader dr = cmd.ExecuteReader();
		if (dr.HasRows)
		{

			while (dr.Read())
			{

				dpumilkltr = dr["DpuLtr"].ToString();
				dpuampount = dr["DpuAmount"].ToString();


				if (dpumilkltr == "")
				{

					dpumilkltr = "0.0";

				}


				if (dpuampount == "")
				{

					dpuampount = "0.0";

				}

			}
		}


	}

	public void getplantmilkandamount(string getfrmdate, string gettodate)
	{
		SqlConnection con = new SqlConnection(connStr);
		string str = "";
		str = "select SUM(milk_ltr) as plantLtr,sum(Amount) as  plantAmount   from Procurement   where plant_code='" + pcode + "'   and agent_id='" + ddl_agentid.Text + "'   and Prdate between '" + getfrmdate + "' and '" + gettodate + "'";
		con.Open();
		SqlCommand cmd = new SqlCommand(str, con);
		SqlDataReader dr = cmd.ExecuteReader();
		if (dr.HasRows)
		{

			while (dr.Read())
			{

				plantmilkltr = dr["plantLtr"].ToString();
				plantamount = dr["plantAmount"].ToString();

				if (plantmilkltr == "")
				{

					plantmilkltr = "0.0";

				}


				if (plantamount == "")
				{

					plantamount = "0.0";

				}

			}
		}


	}

	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
           
		 string assigndate= e.Row.Cells[1].Text;
		 string checkml = e.Row.Cells[2].Text;
		if(checkml!="")
		{

		 getdate = assigndate.Split('_');  //01/02/2016_10/02/2016
         string valu=getdate[0].Substring(0,2);
		 string valu1 = getdate[0].Substring(3, 2); //01
		 string valu2 = getdate[0].Substring(6, 4);  //
		 string valu3 = getdate[0].Substring(11, 2);
		 string valu4 = getdate[0].Substring(14, 2);
		 string valu5 = getdate[0].Substring(17, 4);

		 getfrmdate = valu1 + "/" + valu + "/" + valu2;
		 gettodate = valu4 + "/" + valu3 + "/" + valu5;
		 getagentprocdetails(getfrmdate, gettodate);

			  e.Row.Cells[2].Text= milkltr.ToString();
			  e.Row.Cells[3].Text = comamt.ToString();
			  double MLTR =Convert.ToDouble(milkltr);
			  double CAMT = Convert.ToDouble(comamt);
			  double TOTAMT = (MLTR) * (CAMT);
			//  e.Row.Cells[4].Text =TOTAMT.ToString("f2");

		}
}
	}



	protected void btn_export_Click(object sender, EventArgs e)
	{
		try
		{

if(RadioButtonList1.SelectedValue=="1")
{
			Response.Clear();
			Response.Buffer = true;
			string filename = "'" + ddl_Plantname.SelectedItem.Text + "' " + DateTime.Now.ToString() + ".xls";

			Response.AddHeader("content-disposition", "attachment;filename=" + filename);
			// Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
			Response.Charset = "";
			Response.ContentType = "application/vnd.ms-excel";
			using (StringWriter sw = new StringWriter())
			{
				HtmlTextWriter hw = new HtmlTextWriter(sw);

				//To Export all pages
				GridView1.AllowPaging = false;
				// getgriddata();
			

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


if (RadioButtonList1.SelectedValue == "2")
{
	Response.Clear();
	Response.Buffer = true;
	string filename = "'" + ddl_Plantname.SelectedItem.Text + "' " + DateTime.Now.ToString() + ".xls";

	Response.AddHeader("content-disposition", "attachment;filename=" + filename);
	// Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
	Response.Charset = "";
	Response.ContentType = "application/vnd.ms-excel";
	using (StringWriter sw = new StringWriter())
	{
		HtmlTextWriter hw = new HtmlTextWriter(sw);

		//To Export all pages
		GridView2.AllowPaging = false;
		// getgriddata();


		GridView1.HeaderRow.BackColor = Color.White;
		foreach (TableCell cell in GridView2.HeaderRow.Cells)
		{
			cell.BackColor = GridView1.HeaderStyle.BackColor;
		}
		foreach (GridViewRow row in GridView2.Rows)
		{
			row.BackColor = Color.White;
			foreach (TableCell cell in row.Cells)
			{
				if (row.RowIndex % 2 == 0)
				{
					cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
				}
				else
				{
					cell.BackColor = GridView2.RowStyle.BackColor;
				}
				cell.CssClass = "textmode";
			}
		}

		GridView2.RenderControl(hw);

		//style to format numbers to string
		string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
		// string style = @"<style> .textmode { } </style>";
		Response.Write(style);
		Response.Output.Write(sw.ToString());
		Response.Flush();
		Response.End();
	}
}



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
	public override void VerifyRenderingInServerForm(Control control)
	{
		/* Verifies that the control is rendered */
	}

	protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{

			string assigndate = e.Row.Cells[1].Text;
			string checkml = e.Row.Cells[2].Text;
			if (checkml != "")
			{

				getdate = assigndate.Split('_');  //01/02/2016_10/02/2016
				string valu = getdate[0].Substring(0, 2);
				string valu1 = getdate[0].Substring(3, 2); //01
				string valu2 = getdate[0].Substring(6, 4);  //
				string valu3 = getdate[0].Substring(11, 2);
				string valu4 = getdate[0].Substring(14, 2);
				string valu5 = getdate[0].Substring(17, 4);

				getfrmdate = valu1 + "/" + valu + "/" + valu2;
				gettodate = valu4 + "/" + valu3 + "/" + valu5;
				getdpumilkandamount(getfrmdate, gettodate);
				getplantmilkandamount(getfrmdate, gettodate);
				e.Row.Cells[2].Text = dpumilkltr.ToString();
				e.Row.Cells[3].Text =dpuampount.ToString();
				e.Row.Cells[4].Text = plantmilkltr.ToString();
				e.Row.Cells[5].Text =plantamount.ToString();

				double MLTR = Convert.ToDouble(plantamount);
				double CAMT = Convert.ToDouble(dpuampount);
				double TOTAMT = (MLTR)- (CAMT);
				e.Row.Cells[6].Text = TOTAMT.ToString("f2");

			}
		}
	}
}