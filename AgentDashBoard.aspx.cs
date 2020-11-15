using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;
public partial class AgentDashBoard : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();


    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;
    public int refNo = 0;
    public static int roleid;
    public static string  agentidget;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //    managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;

                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                else
                {
                    LoadPlantName();
                }
                pcode = ddl_PlantName.SelectedItem.Value;

                Label6.Visible = false;
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
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //   managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_PlantName.SelectedItem.Value;



            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
            Label6.Visible = false;

        }
    }


    private void LoadPlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

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

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

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
        getAgentpersonal();

    }

    public void getAgentpersonal()
    {

        try
        {
          //  string str = "SELECT AgentId,AgentName,NULL AS LoanAmount,Null as RecoveryAmount,Null as ReceiptAmount,Null as Balance   FROM (select am.Agent_Id as AgentId,am.Agent_Name  as AgentName,am.Agent_Name AS LoanAmount,am.Agent_Name AS RecoveryAmount,am.Agent_Name AS ReceiptAmount,am.Agent_Name AS Balance  from (Select Agent_Id,Agent_Name    from  agent_Master    where plant_code='"+pcode+"'  group by Agent_Id,Agent_Name  ) as am left join (Select agent_Id    from  LoanDetails    where plant_code='"+pcode+"'  group by agent_Id  ) as ld on ld.Agent_Id=am.agent_Id   where ld.agent_Id is not null) AS AA";
            string str = "select am.Agent_Id as CanNo,Agent_Name,convert(varchar,AddedDate,103) as JoinDate,Route_Name,Address,phone_Number as Mobile from( select Agent_Id,phone_Number,AddedDate    from Agent_Master   where Plant_code='"+pcode+"' and Agent_Id='"+ddl_Agentid.Text+"'  group by Agent_Id,phone_Number,AddedDate) am left join (select Agent_Id,Agent_Name,Route_Name,Address,Mobile    from AgentInformation   where Plant_code='"+pcode+"' and Agent_Id='"+ddl_Agentid.Text+"') as Ai on am.Agent_Id=ai.Agent_Id";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows) 
            {



                string getcan = dr[0].ToString();
                string AgentName = dr[1].ToString();
                string JoinDate = dr[2].ToString();
                string RouteName = dr[3].ToString();
                string Address = dr[4].ToString();
                string phone = dr[5].ToString();
                
                //dt1.Rows.Add(getcan,
                //GridView2.DataSource = dt;
                //GridView2.DataBind();
            }

        }
        catch
        {




        }
    }


    protected void BindData()
    {
        string str = "select am.Agent_Id as CanNo,Agent_Name,convert(varchar,AddedDate,103) as JoinDate,Route_Name,Address,phone_Number as Mobile from( select Agent_Id,phone_Number,AddedDate    from Agent_Master   where Plant_code='" + pcode + "' and Agent_Id='" + ddl_Agentid.Text + "'  group by Agent_Id,phone_Number,AddedDate) am left join (select Agent_Id,Agent_Name,Route_Name,Address,Mobile    from AgentInformation   where Plant_code='" + pcode + "' and Agent_Id='" + ddl_Agentid.Text + "') as Ai on am.Agent_Id=ai.Agent_Id";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(str, con);
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        DataList1.DataSource = dt;
        DataList1.DataBind();
    }
protected void  GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
{

}
protected void  ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
{

}

public void getAgent()
{
    try
    {
        string Getagent = "";
        Getagent = "Select agent_id   from Agent_master   where plant_code='" + pcode + "' and agent_id='" + ddl_Agentid.Text + "'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(Getagent, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {


                ddl_Agentid.Items.Add(dr["agent_id"].ToString());

            }


        }

    }
    catch 
    {



    }

}







    //public void getAgent()
    //{

    //    try
    //    {
    //        string str = "SELECT AgentId,AgentName,NULL AS LoanAmount,Null as RecoveryAmount,Null as ReceiptAmount,Null as Balance   FROM (select am.Agent_Id as AgentId,am.Agent_Name  as AgentName,am.Agent_Name AS LoanAmount,am.Agent_Name AS RecoveryAmount,am.Agent_Name AS ReceiptAmount,am.Agent_Name AS Balance  from (Select Agent_Id,Agent_Name    from  agent_Master    where plant_code='" + pcode + "'  group by Agent_Id,Agent_Name  ) as am left join (Select agent_Id    from  LoanDetails    where plant_code='" + pcode + "'  group by agent_Id  ) as ld on ld.Agent_Id=am.agent_Id   where ld.agent_Id is not null) AS AA";
    //        con = DB.GetConnection();
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        DataTable dt = new DataTable();
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //        if (dt.Rows.Count > 1)
    //        {

    //            GridView2.DataSource = dt;
    //            GridView2.DataBind();
    //        }

    //    }
    //    catch
    //    {




    //    }
    //}
    //protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
    //    pcode = ddl_Plantcode.SelectedItem.Value;
    //}
    //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //}
    protected void btn_print_Click(object sender, EventArgs e)
    {

    }
    protected void btn_export_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
    {
        getAgent();
    }
}