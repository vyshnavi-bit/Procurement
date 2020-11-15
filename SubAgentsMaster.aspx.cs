using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
public partial class SubAgentsMaster : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public static string routeid;
    public string routemilksum;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTGGG = new DataSet();
    DataSet DTG = new DataSet();
    DataTable agent = new DataTable();
    DataTable collectbank = new DataTable();
    msg MESS = new msg();
    string Datee;
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
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    txt_subagent.Text = "";
                    txt_accno.Text = "";
                    txt_ifsc.Text = "";
                    Datee = dtm.ToString("dd/MM/yyyy");
                     if (roleid < 3)
                    {
                        loadsingleplant();
                        getagentid();
                        getBannkMaster();
                        Select();
                    }

                    else
                    {

                        LoadPlantcode();
                        getagentid();
                        getBannkMaster();
                        Select();
                    }


                }
                else
                {


                }

            }

            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();
                pcode = ddl_Plantname.SelectedItem.Value;
                Datee = dtm.ToString("dd/MM/yyyy");
            }
        }
        catch
        {


        }
    }
    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }
    public void loadsingleplant()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139) and plant_code  in (" + pcode + ") ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }
    public void getagentid()
    {
        try
        {
            string getagent;
            con = DB.GetConnection();
            getagent = "Select agent_id   from Agent_master  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and subagent='1'  GROUP BY  agent_id ";
            SqlCommand cmd = new SqlCommand(getagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            agent.Rows.Clear();
            da.Fill(agent);
            ddl_Agentid.DataSource = agent;
            ddl_Agentid.DataTextField = "agent_id";
            ddl_Agentid.DataValueField = "agent_id";
            ddl_Agentid.DataBind();
        }
        catch
        {
        }
    }
     public void getBannkMaster()
    {
        try
        {
            string getagent;
            con = DB.GetConnection();
            getagent = "select Bank_name  from BANK_MASTER where plant_code='" + ddl_Plantname.SelectedItem.Value + "' GROUP BY Bank_name ";
            SqlCommand cmd = new SqlCommand(getagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable BANK = new DataTable();
            BANK.Rows.Clear();
            da.Fill(BANK);
            string gets;
            string[] getbank;
            string bankid;
            string bankName;
            foreach (DataRow dr in BANK.Rows)
            {
                gets = dr[0].ToString();
                getbank = gets.Split('_');
                bankid = getbank[0];
                bankName = getbank[1];
                //collectbank.Rows.Add(bankName);
                ddl_bankname.Items.Add(bankName);
            }
        }
        catch
        {

        }
         
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        GETINSERT();
        Select();
    }

    public void GETINSERT()
    {
        try
        {

            dtm = System.DateTime.Now;
            Datee = dtm.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            string INSERT;
            INSERT = "Insert into SubAgentMaster(Agent_id,Plant_code,subAgentname,Bank_name,Accountno,Ifsc_code,Addeddate,AddedBy) values('" + ddl_Agentid.SelectedItem.Value + "','" + ddl_Plantname.SelectedItem.Value + "','" + txt_subagent.Text + "','" + ddl_bankname.SelectedItem.Value + "','" + txt_accno.Text + "','" + txt_ifsc.Text + "','" + Datee + "','" + Session["Name"] + "')";
            SqlCommand CMD = new SqlCommand(INSERT, con);
            CMD.ExecuteNonQuery();
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.save) + "')</script>");
            clear();
        }
        catch
        {

        }
    }

    public void Select()
    {
        try
        {

            dtm = System.DateTime.Now;
            Datee = dtm.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            string INSERT;
            INSERT = "Select sUBAGENTNAME AS AgentName,Bank_name,ifsc_code,Accountno  from SubAgentMaster where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and agent_id='" + ddl_Agentid.SelectedItem.Value + "' order by tid desc ";
            SqlCommand CMD = new SqlCommand(INSERT, con);
            DataTable getlist = new DataTable();
            getlist.Rows.Clear();
            SqlDataAdapter da = new SqlDataAdapter(CMD);
            da.Fill(getlist);
            GridView1.DataSource = getlist;
            GridView1.DataBind();
           
        }
        catch
        {

        }
    }

    public void clear()
    {
        txt_subagent.Text = "";
        txt_accno.Text = "";
        txt_ifsc.Text = "";
    }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        getagentid();
        getBannkMaster();
        Select();
    }
    protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBannkMaster();
        Select();
    }
}