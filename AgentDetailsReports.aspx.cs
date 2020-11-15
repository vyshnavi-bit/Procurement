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
using System.IO;
using System.Collections.Generic;

public partial class AgentDetailsReports : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    DataSet ds = new DataSet();
    DateTime tdt = new DateTime();
    string strsql = string.Empty;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    int returnvalue;
    string plantname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string rid;
    string path;
    int validateval;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Session["Image"] = null;

                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();
                if (roleid < 3)
                {
                    loadsingleplant();
                    //  GetRouteID();
                }
                else
                {
                    LoadPlantcode();
                }
                //  pcode = ddl_Plantcode.SelectedItem.Value;

                getagentid();
                gridview();

                DetailsView2.Visible = true;




                if (rdosingle.Checked == true)
                {

                    Label21.Visible = true;
                    ddl_Agentid.Visible = true;
                }
                else
                {

                    Label21.Visible = false;
                    ddl_Agentid.Visible = false;

                }
                Label8.Visible = false;
                Label14.Visible = false;
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
                //  pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();
                //ddl_Agentid.SelectedIndex = ddl_Agentid.SelectedIndex;
                roleid = Convert.ToInt32(Session["Role"].ToString());
                pcode = ddl_Plantcode.SelectedItem.Value;

                if (rdosingle.Checked == true)
                {

                    Label21.Visible = true;
                    ddl_Agentid.Visible = true;
                }
                else
                {

                    Label21.Visible = false;
                    ddl_Agentid.Visible = false;

                }
                gridview();

                //  getagentid();


                DetailsView2.Visible = true;

                //  getgrid();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }
    }




    public void gridview()
    {



        try
        {


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                //      string sqlstr = "SELECT Plant_Name,PlantType,StateName,PlaceName,Mailid,Mgrname,MgrMobile,PlantPhone,InchargeName,InchargeMobile,analyzermobile,weighermobile,securitymobile  FROM PlantDetails   order by  Tid desc";
                //      string sqlstr = "SELECT Plant_Name as PlantName,PlaceName,Mailid,Mgrname as ManagerName,MgrMobile as ManagerMobile,PlantPhone as PhoneNo,InchargeName,InchargeMobile  FROM PlantDetails   order by  Tid desc";
                //    string sqlstr = "select distinct a.Agent_Id as AgentId,a.AgentRateChartmode  as RateChartType,b.Ratechart_Id as RatechartName from (select   Agent_Id,AgentRateChartmode   from    agent_master   where   plant_code='" + pcode + "' and route_id='" + routeid + "'  ) as a left join(	select  *    from   procurement where Plant_Code='" + pcode + "' and  route_id='" + routeid + "' ) as b on a.Agent_Id=b.Agent_id order by a.Agent_Id asc ";
                //   string sqlstr = "select  a.agent_id,b.agentratechartmode  from( select distinct  agent_id    from    procurement    where   plant_code='" + pcode + "' and route_id='" + routeid + "') as a left join(select *   from   agent_master  where  plant_code='" + pcode + "' and route_id='" + routeid + "') as b on a.agent_id=b.agent_id   order by a.agent_id";

                if (Rtoall.Checked == true)
                {
                    string sqlstr = "select  plant_name,agent_name,agent_id,Route_Id,Route_name,Address,CONVERT(VARCHAR(11),JoiningDate,106) as JoiningDate,BankName,BankAccNo,IfscNo,BranchName,Mobile,GuardianName,image,Aadharimage,Rationimage,voterimage,pancardimage,Accountimage,AadharNo,RationCartNo,VoterId,PanCardNo,CONVERT(VARCHAR(11),Dob,106) as Dob,CONVERT(VARCHAR(11),MarriageDate,106) as MarriageDate    from  AgentInformation where   plant_code='" + pcode + "' order by tid asc ";

                    SqlCommand COM = new SqlCommand(sqlstr, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                    sqlDa.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {

                        DetailsView2.DataSource = dt;
                        DetailsView2.DataBind();
                        Label8.Visible = true;
                        Label14.Visible = true;

                    }
                    else
                    {

                        DetailsView2.DataSource = null;
                        DetailsView2.DataBind();

                    }
                }
                if (rdosingle.Checked == true)
                {

                    string sqlstr = "select  plant_name,agent_name,agent_id,Route_Id,Route_name,Address,CONVERT(VARCHAR(11),JoiningDate,106) as JoiningDate,BankName,BankAccNo,IfscNo,BranchName,Mobile,GuardianName,image,Aadharimage,Rationimage,voterimage,pancardimage,Accountimage,AadharNo,RationCartNo,VoterId,PanCardNo,CONVERT(VARCHAR(11),Dob,106) as Dob,CONVERT(VARCHAR(11),MarriageDate,106) as MarriageDate    from  AgentInformation where   plant_code='" + pcode + "' and agent_id='" + ddl_Agentid.Text + "' ";
                    SqlCommand COM = new SqlCommand(sqlstr, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                    sqlDa.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {

                        DetailsView2.DataSource = dt;
                        DetailsView2.DataBind();
                        Label8.Visible = true;
                        Label14.Visible = true;

                    }
                    else
                    {

                        DetailsView2.DataSource = null;
                        DetailsView2.DataBind();

                    }

                }





            }
        }
        catch
        {



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
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {


    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;



        if (Rtoall.Checked == true)
        {

            gridview();

        }
        if (rdosingle.Checked == true)
        {
            getagentid();
            gridview();


        }






    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {


    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        gridview();

    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
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


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Rtoall_CheckedChanged(object sender, EventArgs e)
    {

        rdosingle.Checked = false;
        Rtoall.Checked = true;
        if (Rtoall.Checked == true)
        {

            Label21.Visible = false;
            ddl_Agentid.Visible = false;

            rdosingle.Checked = false;
            Rtoall.Checked = true;
            gridview();
        }

        if (rdosingle.Checked == true)
        {
            rdosingle.Checked = true;
            Rtoall.Checked = false;
            Label21.Visible = true;
            ddl_Agentid.Visible = true;


        }

    }
    protected void rdosingle_CheckedChanged(object sender, EventArgs e)
    {

        rdosingle.Checked = true;
        Rtoall.Checked = false;

        if (Rtoall.Checked == true)
        {

            Label21.Visible = false;
            ddl_Agentid.Visible = false;
            rdosingle.Checked = false;
            Rtoall.Checked = true;
            gridview();

        }

        if (rdosingle.Checked == true)
        {
            rdosingle.Checked = true;
            Rtoall.Checked = false;
            getagentid();
            Label21.Visible = true;
            ddl_Agentid.Visible = true;
            gridview();

        }
    }
    protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
    {
        // getagentid();
        //   getagentid();
        rdosingle.Checked = true;
        Rtoall.Checked = false;

        Label21.Visible = true;
        ddl_Agentid.Visible = true;
        gridview();
    }



    public void getagentid()
    {

        try
        {
            ddl_Agentid.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select distinct agent_id from agent_master where plant_code='" + pcode + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddl_Agentid.Items.Add(dr["agent_id"].ToString());
                // txt_AgentName.Text = dr["Agent_Name"].ToString();


            }


        }

        catch
        {

            WebMsgBox.Show("NO MILK");

        }



    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentDetails.aspx");
    }

    protected void DetailsView2_DataBound(object sender, EventArgs e)
    {

    }
    protected void DetailsView2_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }
    protected void DetailsView2_DataBound1(object sender, EventArgs e)
    {

    }
    protected void DetailsView2_DataBound2(object sender, EventArgs e)
    {


     

    }
    protected void DetailsView2_ItemCreated(object sender, EventArgs e)
    {

    }
}