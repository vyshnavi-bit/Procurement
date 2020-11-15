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
public partial class DpuAgentDailySummary : System.Web.UI.Page
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
				gridview();
				getrouteid();

			}

		}
		else
		{


			pcode = ddl_Plantcode.SelectedItem.Value;
			//  LoadPlantcode();
			//   ddl_agentcode.Text = getrouteusingagent;
			gridview();
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


	protected void Button_Click(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			getifsccode();

			gridview();

		}
	}
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{


		if (!IsPostBack)
		{
		//	ddl_agentcode.Items.Clear();

			ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
			pcode = ddl_Plantcode.SelectedItem.Value;
		//	gridview();
			getifsccode();

		}
		else
		{

			ddl_agentcode.Items.Clear();
			ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
			pcode = ddl_Plantcode.SelectedItem.Value;
			getifsccode();
			getagentid();
		//	gridview();
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

			string d11 = d1 + " " + "12:00:00";
			string d21 = d2 + " " + "12:00:00";


			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();

				//string sqlstr = "SELECT tid,agent_id,convert(varchar,prdate,103) as prdate,sessions,Milk_kg,fat,snf FROM ProducerProcurement  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' and agent_id='" + ddl_agentcode.Text + "' order by prdate,sessions ";

                string sqlstr = "select  Tid,pcode,convert(varchar,prdate,103) Date,shift,agent_code,producer_code,sum(Fat) as Fat,sum(snf) as snf,sum(milk_kg) as milk_kg   from (  Select Tid,plant_code as pcode,prdate,shift,agent_code,producer_code,convert(float,fat) as  Fat,convert(float,snf) as  snf,convert(float,milk_kg) as  milk_kg,remarkstatus  from VMCCDPU   where plant_code='" + pcode + "'   and (prdate between '" + d1 + "' and '" + d2 + "' )  or  (prdate between '" + d11 + "' and '" + d21 + "' )) as dd where (fat < 2.5   or snf < 7.0 )  and pcode='" + pcode + "'   and ((Remarkstatus=1) or   (Remarkstatus is null))   group by   Tid,pcode,prdate,shift,agent_code,producer_code order by agent_code,producer_code  ";
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

	public void getagentid()
	{

		try
		{
			//   ddl_agentcode.Items.Clear();
			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection con = new SqlConnection(connStr);
			con.Open();
			//   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
			string str = "select distinct PRODUCER_CODE from DPUPRODUCERMASTER where plant_code='" + pcode + "'  order by PRODUCER_CODE  asc ";
			SqlCommand cmd = new SqlCommand(str, con);
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{

				ddl_agentcode.Items.Add(dr["PRODUCER_CODE"].ToString());


			}


		}

		catch
		{

			WebMsgBox.Show("NO MILK");

		}



	}
	protected void ddl_agentcode_SelectedIndexChanged(object sender, EventArgs e)
	{

		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		pcode = ddl_Plantcode.SelectedItem.Value;
		gridview();

	}
	protected void txt_FromDate_TextChanged(object sender, EventArgs e)
	{

	}
	protected void ddl_Plantname_PreRender(object sender, EventArgs e)
	{

	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridView1.PageIndex = e.NewPageIndex;
		gridview();
	}
	protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	{
		GridView1.EditIndex = -1;
		gridview();
	}
	protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
	{



		GridView1.EditIndex = e.NewEditIndex;
		gridview();

	}
	protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{

		try
		{
			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection conn = new SqlConnection(connStr);

			int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
			GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
			//  Label lblID = (Label)row.FindControl("tid");
			// TextBox bid = (TextBox)row.Cells[0].Controls[0];

		//	TextBox agentid1 = (TextBox)row.Cells[1].Controls[0];
			//  TextBox routeid1 = (TextBox)row.Cells[1].Controls[0];
			// TextBox milknature1 = (TextBox)row.Cells[2].Controls[0];


			//foreach (GridViewRow gvr in GridView1.Rows)
			//{
			//   ddate = GridView1.DataKeys[gvr.RowIndex].Values["prdate"].ToString();

			//}
			//    TextBox prdate1 = (TextBox)row.Cells[2].Controls[0];

			// TextBox prdate1 = ddate;
			//  TextBox shift1 = (TextBox)row.Cells[3].Controls[0];
			TextBox fat = (TextBox)row.Cells[5].Controls[0];
			TextBox snf = (TextBox)row.Cells[6].Controls[0];
			TextBox milk_kg1 = (TextBox)row.Cells[7].Controls[0];
			
		
			GridView1.EditIndex = -1;
			conn.Open();
		
			//   ddl_agentcode.Text = getrouteusingagent;
			//  ddl_agentcode.Text = getrouteusingagent;
		//	getrouteid();

            SqlCommand cmd = new SqlCommand("update VMCCDPU  set fat='" + fat.Text + "',snf='" + snf.Text + "',milk_kg='" + milk_kg1.Text + "',Remarkstatus=1 where tid='" + userid + "' and plant_code='" + pcode + "'", conn);
			cmd.ExecuteNonQuery();
			WebMsgBox.Show("Updated Successfully");
			gridview();
		}
		catch
		{

			WebMsgBox.Show("Please Check updated Data");
		}
		gridview();

	}

	protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void txt_ToDate_TextChanged(object sender, EventArgs e)
	{

	}
	protected void btn_Ok_Click(object sender, EventArgs e)
	{
		 


			gridview();

		 
	}
	public void getrouteid()
	{

		try
		{
			//  ddl_agentcode.Items.Clear();
			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection con = new SqlConnection(connStr);
			con.Open();
			string str = "select distinct(route_id) from Agent_Master where plant_code='" + pcode + "'  and agent_id='" + getrouteusingagent + "'   ";
			SqlCommand cmd = new SqlCommand(str, con);
			SqlDataReader dr = cmd.ExecuteReader();
			while (!dr.Read())
			{
				WebMsgBox.Show("Please Check Your Agent Id");



			}

			routeid = dr["route_id"].ToString();
		}

		catch
		{

			WebMsgBox.Show("Please Check");

		}

	}



	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		//if (e.Row.Cells[2].Text != null && e.Row.Cells[2].Text != "&nbsp;" && e.Row.Cells[2].Text != "")
		//{
		//    e.Row.Cells[2].Attributes.Add("readonly", "false");
		//}
		//if (e.Row.Cells[3].Text != null && e.Row.Cells[3].Text != "&nbsp;" && e.Row.Cells[3].Text != "")
		//{
		//    e.Row.Cells[3].Attributes.Add("readonly", "false");
		//}

	}

	public void getifsccode()
	{

		try
		{
			//   ddl_agentcode.Items.Clear();
			//   string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection con = new SqlConnection(connStr);
			con.Open();
			//   string str = "select * from BANK_MASTER   where plant_code='" + pcode + "' and Bank_id='" + ddl_bankid.Text + "'";

			string str = "select   top 1   convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate from Bill_date   where plant_code='" + pcode + "'   order  by tid desc";
			//  string str= = "select * from Bill_date   where plant_code='" + pcode + "' and Bill_frmdate='"+txt_FromDate.Text+"'    Bill_todate='" + txt_ToDate.Text + "'";
			SqlCommand cmd = new SqlCommand(str, con);
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{

				// ddl_ccode.Items.Add(dr["company_code"].ToString());

				txt_FromDate.Text = dr["Bill_frmdate"].ToString();
				txt_ToDate.Text = dr["Bill_todate"].ToString();

			}


		}

		catch
		{

			WebMsgBox.Show("ERROR");

		}

	}

	protected void ddl_Plantname_TextChanged(object sender, EventArgs e)
	{

	}
}