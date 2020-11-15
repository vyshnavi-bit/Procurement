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
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;
using System.Web.Services;
using System.Collections.Generic;
public partial class AgentsAdditionalCharges : System.Web.UI.Page
{
	public static int roleid;
	public string ccode;
	public string pcode;
	public string managmobNo;
	public string pname;
	public string cname;
	DateTime dtm = new DateTime();
	BLLuser Bllusers = new BLLuser();
	SqlConnection con = new SqlConnection();
	msg getclass = new msg();
	DbHelper DB = new DbHelper();
	DataSet DTG = new DataSet();
	string FDATE;
	string TODATE;
	public string frmdate;
	string Todate;
	string getvald;
	string getvalm;
	string getvaly;
	string getvaldd;
	string getvalmm;
	string getvalyy;
	SqlDataReader dr;
	int datasetcount = 0;

	BLLroutmaster routmasterBL = new BLLroutmaster();
	BLLProcureimport Bllproimp = new BLLProcureimport();
	Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataTable fatkgdt = new DataTable();
    DataTable showagent = new DataTable();
	string d1;
	string d2;
	string agentcode;
    int countinsertdetails;
    double gettotal;
    double Added_paise;
    string agent;
    string fdate;
    string tdate;
    string ppcode;
	protected void Page_Load(object sender, EventArgs e)
	{

		try
		{
			if (!IsPostBack)
			{
				if ((Session["Name"] != null) && (Session["pass"] != null))
				{
					dtm = System.DateTime.Now;
					ccode = Session["Company_code"].ToString();
					pcode = Session["Plant_Code"].ToString();
					Session["tempp"] = Convert.ToString(pcode);
					roleid = Convert.ToInt32(Session["Role"].ToString());
					dtm = System.DateTime.Now;
					ddl_Agentid.Visible = false;
					ddl_routeid.Visible = false;
					Label2.Visible = false;
					Label3.Visible = false;
                    TextBox1.Visible = false;
                    Label4.Visible = false;
                    Button11.Visible = false;
					//   txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
					if (roleid < 3)
					{
						loadsingleplant();
						pcode = ddl_Plantname.SelectedItem.Value;
						Bdate();
					}
					else
					{
						LoadPlantcode();
						pcode = ddl_Plantname.SelectedItem.Value;
						Bdate();
					}

				}
				else
				{



				}

				Button10.Visible = false;
			}


			else
			{
				ccode = Session["Company_code"].ToString();

				pcode = ddl_Plantname.SelectedItem.Value;
				Bdate();
				ViewState["pcode"] = pcode.ToString();

                Button11.Visible = false;
			}
			Button10.Visible = false;
		}

		catch
		{



		}
	}


	public void LoadPlantcode()
	{

		con = DB.GetConnection();
		string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
		SqlCommand cmd = new SqlCommand(stt, con);
		SqlDataAdapter DA = new SqlDataAdapter(cmd);
		DA.Fill(DTG);
		ddl_Plantname.DataSource = DTG.Tables[datasetcount];
		ddl_Plantname.DataTextField = "Plant_Name";
		ddl_Plantname.DataValueField = "Plant_Code";
		ddl_Plantname.DataBind();

		datasetcount = datasetcount + 1;
	}


	public void loadsingleplant()
	{
		con = DB.GetConnection();
		string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
		SqlCommand cmd = new SqlCommand(stt, con);
		SqlDataAdapter DA = new SqlDataAdapter(cmd);
		DA.Fill(DTG);
		ddl_Plantname.DataSource = DTG.Tables[datasetcount];
		ddl_Plantname.DataTextField = "Plant_Name";
		ddl_Plantname.DataValueField = "Plant_Code";
		ddl_Plantname.DataBind();
		datasetcount = datasetcount + 1;
	}
	protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (RadioButtonList1.Text == "1")
		{
			ddl_routeid.Visible = false;
			ddl_Agentid.Visible = false;
			Label2.Visible = false;
			Label3.Visible = false;
			GridView1.DataSource = null;
            GridView1.DataBind();
		}
		if (RadioButtonList1.Text == "2")
		{
			routemaster();
			ddl_routeid.Visible = true;
			ddl_Agentid.Visible = false;
			Label2.Visible = true;
			Label3.Visible = false;
			GridView1.DataSource = null;
            GridView1.DataBind();
		}
		if (RadioButtonList1.Text == "3")
		{
			getagents();
			Label3.Visible = true;
			Label2.Visible = false;
			ddl_routeid.Visible = true;
			ddl_Agentid.Visible = true;
			ddl_routeid.Visible = false;
			GridView1.DataSource = null;
            GridView1.DataBind();
		}

        if (RadioButtonList1.Text == "4")
        {
            //getagents();
            //Label3.Visible = true;
            //Label2.Visible = false;
            //ddl_routeid.Visible = true;
            //ddl_Agentid.Visible = true;
            //ddl_routeid.Visible = false;
            //GridView1.DataSource = null;
            //GridView1.DataBind();

            Button11.Visible = true;

            Label3.Visible = false;
            Label2.Visible = false;
            ddl_routeid.Visible = false;
            ddl_Agentid.Visible = false;

            Button11.Visible = true;
        }

	}

	private void Bdate()
	{
		try
		{
			dr = null;
			dr = Billdate(ccode, pcode);
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
					txt_todate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
				}
			}
			else
			{

			}

		}
		catch (Exception ex)
		{
		}
	}

	public SqlDataReader Billdate(string ccode, string pcode)
	{
		SqlDataReader dr;
		string sqlstr = string.Empty;
		sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND CurrentPaymentFlag='1' ";
		dr = DB.GetDatareader(sqlstr);
		return dr;
	}
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{
		Bdate();
	}
	public void routemaster()
	{

		GETDATE();
		con = DB.GetConnection();
		string stt = "select rm.Route_id,Route_Name  from (select Route_id  from Procurementimport   where Plant_Code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "' group by Route_id   ) as asd    left join (select  Route_ID,Route_Name   from Route_Master   where Plant_Code='" + pcode + "'  group by Route_ID,Route_Name) as rm on asd.Route_id=rm.Route_ID   order by rm.Route_id";
		SqlCommand cmd = new SqlCommand(stt, con);
		SqlDataAdapter DA = new SqlDataAdapter(cmd);
		DA.Fill(DTG);
		ddl_routeid.DataSource = DTG.Tables[datasetcount];
		ddl_routeid.DataTextField = "Route_Name";
		ddl_routeid.DataValueField = "Route_id";
		ddl_routeid.DataBind();
		datasetcount = datasetcount + 1;
	}

	public void GETDATE()
	{
		DateTime dt1 = new DateTime();
		dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
		d1 = dt1.ToString("MM/dd/yyyy");
		DateTime dt2 = new DateTime();
		dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
		d2 = dt2.ToString("MM/dd/yyyy");

	}

	public void getagents()
	{
		GETDATE();
		con = DB.GetConnection();
		string stt = "select Agent_id  from (select distinct(Agent_id) as  Agent_id  from Procurementimport   where Plant_Code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "' ) as asd order by RAND (Agent_id) asc ";
		SqlCommand cmd = new SqlCommand(stt, con);
		SqlDataAdapter DA = new SqlDataAdapter(cmd);
		DA.Fill(DTG);
		ddl_Agentid.DataSource = DTG.Tables[datasetcount];
		ddl_Agentid.DataTextField = "Agent_id";
		ddl_Agentid.DataValueField = "Agent_id";
		ddl_Agentid.DataBind();
		datasetcount = datasetcount + 1;
	}
	protected void Button6_Click(object sender, EventArgs e)
	{
		if (RadioButtonList1.SelectedItem.Value == "1")
		{
			GETALL();
		}
		if (RadioButtonList1.SelectedItem.Value == "2")
		{
			GETROUTE();
		}
		if (RadioButtonList1.SelectedItem.Value == "3")
		{
			GETAGENT();
		}
	}
	public void GETALL()
	{
		try
		{
			GETDATE();
			con = DB.GetConnection();
			string stt = "select asd.Agent_id,Agent_Name  from (select Agent_id,Route_id  from Procurementimport   where Plant_Code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "' group by Agent_id,Route_id   ) as asd    left join (select  Agent_Id,Agent_Name   from Agent_Master   where Plant_Code='" + pcode + "'  group by Agent_Id,Agent_Name ) as rm on asd.Agent_id = RM.Agent_Id   GROUP BY asd.Agent_id,asd.Route_id,Agent_Name order by RAND(asd.Agent_id)";
			SqlCommand cmd = new SqlCommand(stt, con);
			SqlDataAdapter DA = new SqlDataAdapter(cmd);
			DA.Fill(DTG, ("PLANT"));

            //string stcmd = "select asd.Agent_id,Agent_Name  from (select Agent_id,Route_id  from Procurementimport   where Plant_Code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "' group by Agent_id,Route_id   ) as asd    left join (select  Agent_Id,Agent_Name   from Agent_Master   where Plant_Code='" + pcode + "'  group by Agent_Id,Agent_Name ) as rm on asd.Agent_id = RM.Agent_Id   GROUP BY asd.Agent_id,asd.Route_id,Agent_Name order by RAND(asd.Agent_id)";
            //SqlCommand cmdd = new SqlCommand(stcmd, con);
            //DataSet ds = new DataSet();
            //SqlDataAdapter dacmd = new SqlDataAdapter(cmd);
            //dacmd.Fill(ds);
            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //}

			if (DTG.Tables[datasetcount].Rows.Count > 0)
			{
				GridView1.DataSource = DTG.Tables[datasetcount];
				Button10.Visible = true;
				GridView1.DataBind();
				datasetcount = datasetcount + 1;
                TextBox1.Visible = true;
                Label4.Visible = true;
                GridView1.Visible = true;
			}
		}
		catch
		{


		}
	}
	public void GETROUTE()
	{
		try
		{
			GETDATE();
			con = DB.GetConnection();
			string stt = "select asd.Agent_id,Agent_Name  from (select Agent_id,Route_id  from Procurementimport   where Plant_Code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "'   and route_id='" + ddl_routeid.SelectedItem.Value + "' group by Agent_id,Route_id   ) as asd    left join (select  Agent_Id,Agent_Name   from Agent_Master   where Plant_Code='" + pcode + "'   and route_id='" + ddl_routeid.SelectedItem.Value + "'  group by Agent_Id,Agent_Name ) as rm on asd.Agent_id = RM.Agent_Id   GROUP BY asd.Agent_id,asd.Route_id,Agent_Name order by RAND(asd.Agent_id)";
			SqlCommand cmd = new SqlCommand(stt, con);
			SqlDataAdapter DA = new SqlDataAdapter(cmd);
			DA.Fill(DTG, ("ROUTE"));

			if (DTG.Tables[datasetcount].Rows.Count > 0)
			{
				GridView1.DataSource = DTG.Tables[datasetcount];
				Button10.Visible = true;
				GridView1.DataBind();
				datasetcount = datasetcount + 1;
                TextBox1.Visible = true;
                Label4.Visible = true;
                GridView1.Visible = true;
			}
		}
		catch
		{

		}
	}
	public void GETAGENT()
	{
		try
		{
			GETDATE();
			con = DB.GetConnection();
			string stt = "select asd.Agent_id,Agent_Name  from (select Agent_id,Route_id  from Procurementimport   where Plant_Code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "' and agent_id='" + ddl_Agentid.SelectedItem.Value + "' group by Agent_id,Route_id   ) as asd    left join (select  Agent_Id,Agent_Name   from Agent_Master   where Plant_Code='" + pcode + "'  and agent_id='" + ddl_Agentid.SelectedItem.Value + "'  group by Agent_Id,Agent_Name ) as rm on asd.Agent_id = RM.Agent_Id   GROUP BY asd.Agent_id,asd.Route_id,Agent_Name order by RAND(asd.Agent_id)";
			SqlCommand cmd = new SqlCommand(stt, con);
			SqlDataAdapter DA = new SqlDataAdapter(cmd);
			DA.Fill(DTG, ("AGENT"));
			if (DTG.Tables[datasetcount].Rows.Count > 0)
			{
				Button10.Visible = true;
				GridView1.DataSource = DTG.Tables[datasetcount];
				GridView1.DataBind();
				datasetcount = datasetcount + 1;
                TextBox1.Visible = true;
                Label4.Visible = true;
                GridView1.Visible = true;
			}
			else
			{
			}
		}
		catch
		{
		}
	}
	protected void Button9_Click(object sender, EventArgs e)
	{

	}
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{

	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{

	}
	protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	protected void txt_todate_TextChanged(object sender, EventArgs e)
	{

	}
	protected void Button10_Click(object sender, EventArgs e)
	{
        updateadditionalcharger();
	}
	public void updateadditionalcharger()
	{
		try
		{
            if (TextBox1.Text != string.Empty)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked)
                        {
                            agentcode = row.Cells[1].Text;
                            GETDATE();
                            gettingfatkg();
                            double getfatkg;
                            double getaddionalcharge;
                            string gettempfatkg;
                            try
                            {
                                 getfatkg = Convert.ToDouble(fatkgdt.Rows[0][0].ToString());
                                 gettempfatkg = getfatkg.ToString("f2");
                                 getfatkg = Convert.ToDouble(gettempfatkg);
                            }
                            catch
                            {
                                getfatkg = 0;
                            }
                            try
                            {
                                getaddionalcharge = Convert.ToDouble(TextBox1.Text);
                            }
                            catch
                            {
                                getaddionalcharge = 0;
                            }
                            gettotal = (getfatkg * getaddionalcharge);
                            if (gettotal > 0)
                            {
                                con = DB.GetConnection();

                                string selectq = "select * from AgentExcesAmount where plant_code='" + pcode + "' AND Frm_date='" + d1 + "' AND To_date='" + d2 + "' AND agent_id = '" + agentcode + "'";
                                SqlCommand cmd1 = new SqlCommand(selectq, con);
                                SqlDataAdapter dsp = new SqlDataAdapter(cmd1);
                                DataTable dt = new DataTable();
                                dsp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    string update;
                                    update = "UPDATE AgentExcesAmount SET TotAmount='" + gettotal + "', totfat_kg='" + getfatkg + "' where plant_code='" + pcode + "' AND Frm_date='" + d1 + "' AND To_date='" + d2 + "' AND agent_id = '" + agentcode + "'";
                                    //where plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + d1 + "' AND To_date='" + d2 + "' AND agent_id = '" + agentcode + "'";  getupdate = "INSERT INTO AgentExcesAmount( plant_code,agent_id,AddedDate,Added_paise,TotAmount,Frm_date,To_date,totfat_kg)VALUES('" + pcode + "','" + agentcode + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + getaddionalcharge + "','" + 0 + "','" + d1 + "','" + d2 + "','" + 0 + "')";
                                    SqlCommand cmd = new SqlCommand(update, con);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    string getupdate;
                                    getupdate = "INSERT INTO AgentExcesAmount( plant_code,agent_id,AddedDate,Added_paise,TotAmount,Frm_date,To_date,totfat_kg)VALUES('" + pcode + "','" + agentcode + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + getaddionalcharge + "','" + gettotal + "','" + d1 + "','" + d2 + "','" + getfatkg + "')";
                                    //  getupdate = "INSERT INTO AgentExcesAmount( plant_code,agent_id,AddedDate,Added_paise,TotAmount,Frm_date,To_date,totfat_kg)VALUES('" + pcode + "','" + agentcode + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + getaddionalcharge + "','" + 0 + "','" + d1 + "','" + d2 + "','" + 0 + "')";
                                    SqlCommand cmd = new SqlCommand(getupdate, con);
                                    cmd.ExecuteNonQuery();
                                }
                                countinsertdetails = 1;
                            }
                            else
                            {
                                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
                                TextBox1.Text = "";
                                GridView1.Visible = false;
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
                TextBox1.Text = "";
                GridView1.Visible = false;
            }
            if (countinsertdetails == 1)
            {
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.billupdate) + "')</script>");
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = true;
                TextBox1.Text = "";
            }
		}
		catch
		{
			Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
            TextBox1.Text = "";
            GridView1.Visible = false;
		}

	}
	public void gettingfatkg()
	{
		try
		{
			GETDATE();
			con = DB.GetConnection();
            string stt = "select  SUM(fat_kg) as fatkg   from  procurement   where plant_code='" + pcode + "' and   prdate  between '" + d1 + "'  and '" + d2 + "'  and Agent_id='" + agentcode + "'";
			SqlCommand cmd = new SqlCommand(stt, con);
            fatkgdt.Rows.Clear();
			SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(fatkgdt);

            if (fatkgdt.Rows.Count > 0)
			{
				Button10.Visible = true;
			}
			else
			{
			}
		}
		catch
		{
		}

	}
    protected void Button11_Click(object sender, EventArgs e)
    {
        string update = "";
        GETDATE();
        con = DB.GetConnection();
        string stt = "select  agent_id,FRM_DATE,To_date,plant_code,Added_paise   from  AgentExcesAmount   where plant_code='" + pcode + "' and   FRM_DATE='" + d1 + "'  and To_date='" + d2 + "'  ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter dsp = new SqlDataAdapter(cmd);
        showagent.Rows.Clear();
        dsp.Fill(showagent);
        foreach (DataRow trs in showagent.Rows)
        {
            try
            {
                agent = trs[0].ToString();
                fdate = trs[1].ToString();
                tdate = trs[2].ToString();
                ppcode = trs[3].ToString();
                Added_paise = Convert.ToDouble(trs[4]);
            }
            catch
            {
            }
            double getfatk;
            con = DB.GetConnection();
            string stt1 = "select  SUM(fat_kg) as fatkg   from  procurement   where plant_code='" + pcode + "' and   prdate  between '" + fdate + "'  and '" + tdate + "'  and Agent_id='" + agent + "'";
            SqlCommand cmd1 = new SqlCommand(stt1, con);
            SqlDataAdapter dsp1 = new SqlDataAdapter(cmd1);
            fatkgdt.Rows.Clear();
            dsp1.Fill(fatkgdt);
            if (fatkgdt.Rows.Count > 0)
            {
                getfatk = Convert.ToDouble(fatkgdt.Rows[0][0]);
                string tot = getfatk.ToString("f2");
                gettotal = (getfatk * Added_paise);
                string fatkg = tot;
                con = DB.GetConnection();
                string stt2 = "update AgentExcesAmount set totfat_kg='" + fatkg + "',TotAmount='" + gettotal + "'  where   plant_code='" + pcode + "' and   FRM_DATE='" + fdate + "'  and To_date='" + tdate + "'  and Agent_id='" + agent + "'  ";
                SqlCommand cmd2 = new SqlCommand(stt2, con);
                cmd2.ExecuteNonQuery();
            }
            else
            {

            }
        }
        string mss = "Record saved SuccessFully";
        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
    }
}