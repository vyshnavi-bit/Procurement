using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class AssignDpuAgent : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable getagent = new DataTable();
    DataTable agentinfo = new DataTable();
    DataTable agentlistgrid = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {

                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    agentlist();
              //      agentlistinfor();

                }
                else
                {

                    pname = ddl_Plantname.SelectedItem.Text;


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                pcode = ddl_Plantname.SelectedItem.Value;
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
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
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
    public void agentlist()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT  agent_id FROM agent_master  where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  ORDER BY RAND(agent_id) ASC ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            getagent.Rows.Clear();
            DA.Fill(getagent);
            ddl_agentid.DataSource = getagent;
            ddl_agentid.DataTextField = "agent_id";
            ddl_agentid.DataValueField = "agent_id";
            ddl_agentid.DataBind();


        }
        catch
        {


        }

    }
    //public void agentlistinfor()
    //{
    //    try
    //    {
    //        string agentsub = "";
    //        con = DB.GetConnection();
    //        agentsub = "Select  DpuAgentStatus   from Agent_Master  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and agent_id='" + ddl_agentid.Text + "'  group by  agent_id,agent_name,subAgent  order by   agent_id,agent_name,subAgent";
    //        SqlCommand cmd = new SqlCommand(agentsub, con);
    //        SqlDataAdapter DA = new SqlDataAdapter(cmd);
    //        agentinfo.Rows.Clear();
    //        DA.Fill(agentinfo);
    //        if (agentinfo.Rows.Count > 0)
    //        {
    //            string EE = agentinfo.Rows[0][0].ToString();
    //            if (EE != string.Empty)
    //            {
    //                txt_agent.Text = agentinfo.Rows[0][0].ToString();
    //                //   txt_agent.Enabled = false;
    //            }
    //            else
    //            {
    //                txt_agent.Text = "";
    //                txt_agent.Enabled = true;
    //            }
    //        }
    //        else
    //        {
    //            txt_agent.Text = "";
    //            txt_agent.Enabled = true;
    //        }

    //    }
    //    catch
    //    {


    //    }
    //}
    public void upddatetts()
    {
        try
        {
            string stat = "";
            if (dpustatus.SelectedItem.Value == "1")
            {
                stat = "1";

            }
            if (dpustatus.SelectedItem.Value == "2")
            {
                stat = "0";
            }

            con = DB.GetConnection();
            string stt = "update Agent_Master set DpuAgentStatus='" + stat + "'  where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and agent_id='" + ddl_agentid.SelectedItem.Text + "'";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            string mss = "Dpu Agent Code  Updated successfully";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

        }
        catch
        {


        }

    }

    public void grid()
    {
        try
        {
            string agentsub = "";
            agentsub = "Select  agent_id,agent_name,DpuAgentStatus   from Agent_Master  where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  AND    DpuAgentStatus=1  group by  agent_id,agent_name,DpuAgentStatus  order by   agent_id,agent_name,DpuAgentStatus";
            SqlCommand cmd = new SqlCommand(agentsub, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            agentinfo.Rows.Clear();
            DA.Fill(agentinfo);
            if(agentinfo.Rows.Count > 0)
            {
                GridView2.DataSource = agentinfo;
                GridView2.DataBind();
                
           }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

        }

        }
        catch
        {

        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        upddatetts();
        grid();
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        agentlist();
        grid();
    //    agentlistinfor();
    }
    protected void ddl_agentid_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  agentlistinfor();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}