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

public partial class MixedRatechartsetting : System.Web.UI.Page
{
	BLLAgentmaster AgentBL = new BLLAgentmaster();
	BOLvehicle vehicleBO = new BOLvehicle();
	BLLVehicleDetails vehicleBL = new BLLVehicleDetails();
	DataTable dt = new DataTable();
	DataRow dr;
	string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	string bank;
	int iid;
	int newid;
	string bankid;
	int vehicleno;
	SqlConnection con = new SqlConnection();
	int id;
	string uid;
	string ifsccodefromgrid;
	public int companycode;
	public int plantcode;
	public int ccode;
	public int pcode;
	BLLuser Bllusers = new BLLuser();
	int msgvalue;
	string message;
	int valu;
    public static int roleid;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (IsPostBack != true)
		{
            if ((Session["Name"] != null) && (Session["pass"] != null))
			{
				//uid = Session["User_ID"].ToString();
				//if (Request.QueryString["id"] != null)
                roleid = Convert.ToInt32(Session["Role"].ToString());
				//    Response.Write("querystring passed in: " + Request.QueryString["id"]);

				//else
				//    Response.Write("");
				//uscMsgBox1.MsgBoxAnswered += MessageAnswered;

				ccode = Convert.ToInt32(Session["Company_code"]);
				pcode = Convert.ToInt32(Session["Plant_Code"]);


                if (roleid < 3)
				{
					loadsingleplant();

				//	getgriddata();
				}
				else
				{
					LoadPlantcode();
				//	getgriddata();
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

				ccode = Convert.ToInt32(Session["Company_code"]);
				ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
				pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);

                if (roleid < 3)
               
				{
					ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
					pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);

				}
				else
				{
					ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
					pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);


				}
				//if (charttype.SelectedValue == "1")
				//{
				//    getgriddata1();
				//    GridView2.Visible = true;
				//    GridView1.Visible = false;
				//}
				//else
				//{
				//    getgriddata();
				//    GridView1.Visible = true;
				//    GridView2.Visible = false;

				//}

			}
			else
			{
				Server.Transfer("LoginDefault.aspx");
			}
		}
	}

	//public void getgriddata()
	//{

	//    try
	//    {

	//        SqlConnection con = new SqlConnection(connStr);
	//        con.Open();
	//        string str;
	//        str = "select  Table_ID,Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Status   from  Rate_Chart    where Plant_Code='" + pcode + "'  and chart_name='" + dpu_chart.Text + "' order by Table_ID desc";
	//        SqlCommand cmd = new SqlCommand(str, con);
	//        SqlDataAdapter da = new SqlDataAdapter(cmd);
	//        da.Fill(dt);

	//        if (dt.Rows.Count > 0)
	//        {

	//            GridView1.DataSource = dt;
	//            GridView1.DataBind();
	//            GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
	//            GridView1.HeaderStyle.BackColor = System.Drawing.Color.DarkSlateBlue;
	//            GridView1.HeaderStyle.Font.Bold = true;

	//        }
	//        else
	//        {

	//            GridView1.DataSource = null;
	//            GridView1.DataBind();


	//        }
	//    }


	//    catch (Exception ex)
	//    {

	//        message = ex.Message.ToString();
	//        msgbox();

	//    }
	//    finally
	//    {
	//        con.Close();
	//    }
	//}

	public void getgriddata1()
	{

		try
		{

			SqlConnection con = new SqlConnection(connStr);
			con.Open();
			string str;
			str = "select  Table_ID,Chart_Name,Chart_Type,Min_Fat,Min_Snf,Status,Active   from  Chart_Master      where Plant_Code='" + pcode + "'  and chart_name='"+dpu_chart.Text+"'  order by Table_ID desc";
			SqlCommand cmd = new SqlCommand(str, con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(dt);

			if (dt.Rows.Count > 0)
			{

				GridView2.DataSource = dt;
				GridView2.DataBind();
				GridView2.HeaderStyle.ForeColor = System.Drawing.Color.White;
				GridView2.HeaderStyle.BackColor = System.Drawing.Color.DarkSlateBlue;
				GridView2.HeaderStyle.Font.Bold = true;

			}
			else
			{

				GridView2.DataSource = null;
				GridView2.DataBind();


			}
		}


		catch (Exception ex)
		{

			message = ex.Message.ToString();
			msgbox();

		}
		finally
		{
			con.Close();
		}
	}
	


	private void loadsingleplant()
	{
		try
		{
			SqlDataReader dr = null;
			dr = Bllusers.LoadSinglePlantcode(ccode.ToString(), (pcode).ToString());
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
					ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

				}
			}
		}
		catch (Exception ex)
		{

			message = ex.Message.ToString();
			msgbox();

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
					ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
					ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

				}
			}
		}
		catch (Exception ex)
		{

			message = ex.Message.ToString();
			msgbox();

		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{

	}
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
		pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);
		getratechart();
	}

	public void getratechart()
	{

		try
		{
			dpu_chart.Items.Clear();
			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection con = new SqlConnection(connStr);
			con.Open();
			string str = "select  chart_name   from  chart_master    where Plant_Code='" + pcode + "'  order by Table_ID desc";
			SqlCommand cmd = new SqlCommand(str, con);
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{

					dpu_chart.Items.Add(dr["chart_name"].ToString());
				}

			}
		}

		catch (Exception ex)
		{

			message = ex.Message.ToString();
			msgbox();

		}



	}


	public void msgbox()
	{


		if (msgvalue == 1)
		{

			message = "Your Check your Alloted Amount";
		}
		else
		{
			message = "FilenameAlready Available.";
		}
		if (msgvalue == 2)
		{
			message = "Record Inserted SuccessFully";

		}

		string script = "window.onload = function(){ alert('";
		script += message;
		script += "')};";
		ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

	}
	//protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	//{
	//    GridView1.PageIndex = e.NewPageIndex;
	//    getgriddata();
	//}
	//protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
	//{
	//    GridView1.EditIndex = e.NewEditIndex;
	//    getgriddata();
	//}
	//protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	//{
	//    GridView1.EditIndex = -1;
	//    getgriddata();
	//}
	//protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	//{

	//}
	//protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
	//{
	//    try
	//    {
	//        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	//        SqlConnection conn = new SqlConnection(connStr);

	//        int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
	//        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
	//       TextBox status = (TextBox)row.Cells[8].Controls[0];

	//        string value = (status.Text);
	//    //	GridView1.EditIndex = -1;
	//        if ((value == "1") || (value == "2") || (value == "3") || (value == "4") || (value == "5"))
	//        {
	//            conn.Open();
	//            SqlCommand cmd = new SqlCommand("update   Rate_Chart  set status='" + value + "'   where Plant_Code='" + pcode + "' and table_id='" + userid + "'", conn);
	//            cmd.ExecuteNonQuery();
	//        //	WebMsgBox.Show("Updated Successfully");
	//        }
	//        else
	//        {
	//        //	WebMsgBox.Show("Please Check Your Data");

	//        }
	//        GridView1.EditIndex = -1;
	//        getgriddata();
	//    }
	//    catch (Exception ex)
	//    {

	//        message = ex.Message.ToString();
	//        msgbox();

	//    }
	//}
	protected void btn_Insert_Click(object sender, EventArgs e)
	{
		//if (charttype.SelectedValue == "1")
		//{
		getgriddata1();
		GridView2.Visible = true;
	//	GridView1.Visible = false;
			 
		//}
		//if (charttype.SelectedValue == "2")
		//{
		//    getgriddata();
		//    GridView1.Visible = true;
		//    GridView2.Visible = false;
			 
		//}
	
		//if (charttype.SelectedValue == "3")
		//{
		 
		//    GridView1.Visible = false;
		//    GridView2.Visible = false;
			 

		//}



	}

	protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		GridView2.PageIndex = e.NewPageIndex;
		getgriddata1();
	}
	protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	{
		GridView2.EditIndex = -1;
		getgriddata1();
	}

	
	protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
	{
		GridView2.EditIndex = e.NewEditIndex;
		getgriddata1();
	}
	protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		try
		{
			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			SqlConnection conn = new SqlConnection(connStr);
			int userid = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value.ToString());
			GridViewRow row = (GridViewRow)GridView2.Rows[e.RowIndex];
     		//TextBox status = (TextBox)row.Cells[6].Controls[0];
			CheckBox chk = (CheckBox)row.Cells[7].Controls[0];
			if (chk.Checked)
			{
			valu=1;
			}
			//string value = (status.Text);
		//	GridView2.EditIndex = -1;
			
				conn.Open();
			//	SqlCommand cmd = new SqlCommand("update   chart_master  set status='" + value + "',active='"+valu+"'   where Plant_Code='" + pcode + "' and table_id='" + userid + "'", conn);

				SqlCommand cmd = new SqlCommand("update   chart_master  set  active='" + valu + "'   where Plant_Code='" + pcode + "' and table_id='" + userid + "'", conn);
				cmd.ExecuteNonQuery();
				//WebMsgBox.Show("Updated Successfully");
			

			GridView2.EditIndex = -1;
			getgriddata1();
		}
		catch (Exception ex)
		{

			message = ex.Message.ToString();
			msgbox();

		}
	}
	
	protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		
	}
	protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	{
		
	}
	protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
	{
		
	}
	protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		
	}
	protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		 if (e.Row.RowType == DataControlRowType.DataRow)
            {

				string value = e.Row.Cells[7].Text;

			}
	}
}