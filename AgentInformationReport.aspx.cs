using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
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



public partial class AgentInformationReport : System.Web.UI.Page
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
    int val;
    DateTime date;
    DateTime date1;
    string firstdate;
    string todate;
    string getdate;
    string getstatus;
    string getdate1;
    string getstatus2;
    int countagents;
    int countcompleteagents;
    int countincompleteagents;
    int remainagents;
    int agentinformationtotal;
    string agentimage;
    string agintadhar;
    string agentvoter;
    string agentbank;
    int incomp;
    int gridval;
    string sqlstr;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                Session["Image"] = null;
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
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

                GridView1.Visible = true;

                Button3.Visible = false;


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
          
                Label44.Visible = false;
                Label45.Visible = false;
                Label46.Visible = false;
                Label47.Visible = false;
                Label48.Visible = false;
                Label49.Visible = false;
                Label50.Visible = false;
                Label51.Visible = false;
                Button3.Visible = false;
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
              //  managmobNo = Session["managmobNo"].ToString();
                //ddl_Agentid.SelectedIndex = ddl_Agentid.SelectedIndex;

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


                GridView1.Visible = true;


                Label44.Visible = false;
                Label45.Visible = false;
                Label46.Visible = false;
                Label47.Visible = false;
                Label48.Visible = false;
                Label49.Visible = false;
                Label50.Visible = false;
                Label51.Visible = false;

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
               

                if (Rtoall.Checked == true)
                {
               

                     string sqlstr = "select *  from (select distinct(b.Agent_Id),b.Agent_Name,b.Address,b.BankName,b.AccountNo as BankAccNo,b.IfscNo,b.Image,b.Aadharimage,b.Rationimage,b.voterimage,b.pancardimage,b.Accountimage  from (select Agent_id from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + firstdate + "' and '" + todate + "') as a left join (select  *    from  AgentInformation where   plant_code='" + pcode + "') as b on a.Agent_id=b.Agent_Id ) as a where Agent_Id is not null  order by Agent_Id";


                    SqlCommand COM = new SqlCommand(sqlstr, con);
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
                if (rdosingle.Checked == true)
                {

                   string sqlstr = "select  *    from  AgentInformation where   plant_code='" + pcode + "' and agent_id='" + ddl_Agentid.Text + "' ";

                 //   string sqlstr = "select *  from (select distinct(b.Agent_Id),b.Agent_Name,b.Address,b.BankName,b.AccountNo as BankAccNo,b.IfscNo,b.Image,b.Aadharimage,b.Rationimage,b.voterimage,b.pancardimage,b.Accountimage  from (select Agent_id from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdate1 + "' and '" + getdate + "' and agent_id='" + ddl_Agentid.Text + "') as a left join (select  *    from  AgentInformation where   plant_code='" + pcode + "' and  agent_id='" + ddl_Agentid.Text + "') as b on a.Agent_id=b.Agent_Id ) as a where Agent_Id is not null  order by Agent_Id";

                    SqlCommand COM = new SqlCommand(sqlstr, con);
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

                if (totagents.Checked == true)
                {

                    string sqlst  = "select  *    from  AgentInformation where   plant_code='" + pcode + "' order by agent_id ";

                    //   string sqlstr = "select *  from (select distinct(b.Agent_Id),b.Agent_Name,b.Address,b.BankName,b.AccountNo as BankAccNo,b.IfscNo,b.Image,b.Aadharimage,b.Rationimage,b.voterimage,b.pancardimage,b.Accountimage  from (select Agent_id from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdate1 + "' and '" + getdate + "' and agent_id='" + ddl_Agentid.Text + "') as a left join (select  *    from  AgentInformation where   plant_code='" + pcode + "' and  agent_id='" + ddl_Agentid.Text + "') as b on a.Agent_id=b.Agent_Id ) as a where Agent_Id is not null  order by Agent_Id";

                    SqlCommand COM = new SqlCommand(sqlst, con);
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
        }
        catch
        {



        }


    }

     public void popgridview()
    {



        try
        {
            pcode = ddl_Plantcode.SelectedItem.Value;

            getbilldate();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                


                //        string sqlstr = "select *  from (select distinct(b.Agent_Id),b.Agent_Name,b.Address,b.BankName,b.AccountNo as BankAccNo,b.IfscNo,b.Image,b.Aadharimage,b.Rationimage,b.voterimage,b.pancardimage,b.Accountimage  from (select Agent_id from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + getdate1 + "' and '" + getdate + "') as a left join (select  *    from  AgentInformation where   plant_code='" + pcode + "') as b on a.Agent_id=b.Agent_Id ) as a where Agent_Id is not null  order by Agent_Id";
                if (gridval == 1)
                {
                    sqlstr = "select aaa.Agent_Id,aaa.Agent_Name  from (SELECT  distinct(Agent_id)  as   Agent_id FROM Procurement where  Plant_Code='" + pcode + "' and Prdate between '" + firstdate + "' and '" + todate + "' and Agent_id NOT IN (select    Agent_id  from AgentInformation  where Plant_Code='" + pcode + "')) as aa left join (select agent_id,Agent_Name  from Agent_Master where Plant_Code='" + pcode + "') aaa on aa.Agent_id=aaa.agent_id order by  aaa.Agent_Id ";
                }
                if (gridval == 2)
                {
                   sqlstr = " select Agent_Id,Agent_Name   from (select distinct(b.Agent_Id) as Agentid  from (select Agent_id from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + firstdate + "' and '" + todate + "') as a  left join(select Agent_Id   from AgentInformation   where Plant_Code='" + pcode + "'  and  (image is  null or RationCartno  is  null or voterid is  null  or Aadharimage is  null or accountimage='') ) as b  on a.Agent_id=b.Agent_Id where b.Agent_Id is not null) as aaa left join(select agent_id,Agent_Name  from Agent_Master where Plant_Code='" + pcode + "') as  bbb on aaa.Agentid=bbb.Agent_Id order by  Agent_Id";

                }
                SqlCommand COM = new SqlCommand(sqlstr, con);
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
        //    gridview();

            //getbilldate();
            //counttotagents();
            //completeagets();
            //totagentinsertedcount();

          //  int countincompleteagents;
           // int remainagents;

        //////    Label49.Text =  ( agentinformationtotal - countcompleteagents ).ToString();

        //////     incomp =   Convert.ToInt16(Label49.Text);
        //////    // Label49.Text = (agentinformationtotal).ToString();

        ////// //   Label49.Text = countincompleteagents.ToString();
        ////////    remainagents = (countagents - countcompleteagents + agentinformationtotal);
        //////    Label51.Text = (countagents - agentinformationtotal).ToString();
        //////    int tmp1 = Convert.ToInt16(Label51.Text);

        //////    if(tmp1 < 0)
        //////    {
        //////        Label49.Text = (agentinformationtotal - countcompleteagents).ToString();
        //////        incomp =  Convert.ToInt16(Label49.Text);
        //////    }

        //////    if (Rtoall.Checked == true)
        //////    {

        //////        gridview();


        //////    }
        //////    if (rdosingle.Checked == true)
        //////    {
        //////        getagentid();
        //////        gridview();


        




    }


    public void getbilldate()
    {
        try
        {
            string str = "select top 1 *   from  Bill_date  where   plant_code='" + pcode + "' order by tid desc";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int count = 1;
            int i = 1;
           

                //foreach (DataRow dr in dt.Rows)
                //{
                //    i = i + 0;

                //    date = Convert.ToDateTime(dr["Bill_todate"].ToString());
                //    val = Convert.ToInt16(dr["Status"].ToString());
                //    string getval = date.ToString("MM/dd/yyyy");

                //    if ((i == 1) && (val == 0))
                //    {
                //        getdate = date.ToString("MM/dd/yyyy");
                //        getstatus = val.ToString();
                //        i = i + 1;
                //    }
                //    if ((i == 2) && (val == 1))
                //    {
                //        DateTime date1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                //        getdate1 = date1.ToString("MM/dd/yyy");
                //        getstatus2 = val.ToString();
                //    }

                //}


            foreach (DataRow dr in dt.Rows)
            {
                i = i + 0;

                date = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                val = Convert.ToInt16(dr["Status"].ToString());
                firstdate = date.ToString("MM/dd/yyyy");

                date1 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                val = Convert.ToInt16(dr["Status"].ToString());
                todate = date1.ToString("MM/dd/yyyy");
            }



            
        }
        catch (Exception ex)
        {

            ex.Message.ToString();

        }
    }


    public void counttotagents()
    {
        try
        {
            SqlConnection con = new SqlConnection(connStr);
            string str = "select count(distinct(Agent_id)) as   noofagents from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + firstdate + "' and '" + todate + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    countagents = Convert.ToInt16(dr["noofagents"].ToString());
                    Label45.Text = countagents.ToString();

                    Label45.Visible = true;
                    Label44.Visible = true;

                    Label46.Visible = true;
                    Label47.Visible = true;
                    Label48.Visible = true;
                    Label49.Visible = true;
                    Label50.Visible = true;
                    Label51.Visible = true;
                }


            }
            else
            {
                Label45.Text = "0";

            }
        }

        catch
        {


        }

    }



    public void balanaceagents()
    {
        try
        {
            SqlConnection con = new SqlConnection(connStr);
            string str = "select * from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + firstdate + "' and '" + todate + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    countagents = Convert.ToInt16(dr["noofagents"].ToString());
                    Label45.Text = countagents.ToString();
                }


            }
        }
        catch
        {


        }

    }



    public void completeagets()
    {
        try
        {
            countcompleteagents = 0;
            SqlConnection con = new SqlConnection(connStr);
          //  select *   from AgentInformation   where Plant_Code='" + pcode + "' and ( image!='" + string.Empty + "' or RationCartno!='" + string.Empty + "' or voterid!='" + string.Empty + "') and accountimage!='" + string.Empty + "' ";
        //    string str = " select *   from AgentInformation   where Plant_Code='"+pcode+"' and   image is not null  and( RationCartno  is not null or voterid is not null  or Aadharimage is not null ) and accountimage!=''   ";
            string str = "select distinct(b.Agent_Id) as agent  from (select Agent_id from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + firstdate + "' and '" + todate + "') as a  left join(select Agent_Id   from AgentInformation   where Plant_Code='" + pcode + "' and   image is not null  and( RationCartno  is not null or voterid is not null  or Aadharimage is not null ) and accountimage!='' ) as b  on a.Agent_id=b.Agent_Id where b.Agent_Id is not null ";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    countcompleteagents = countcompleteagents + 1;

                }

                    Label47.Text = countcompleteagents.ToString();
                 //Label46.Visible=true;
                 //Label47.Visible = true;
                 //Label48.Visible = true;
                 //Label49.Visible = true;
                 //Label50.Visible = true;
                 //Label51.Visible = true;

            }
            else
            {
                Label47.Text = "0";


            }
        }
        catch
        {


        }

    }



    public void totagentinsertedcount()
    {
        try
        {
          //  countcompleteagents = 0;
            SqlConnection con = new SqlConnection(connStr);
            //  select *   from AgentInformation   where Plant_Code='" + pcode + "' and ( image!='" + string.Empty + "' or RationCartno!='" + string.Empty + "' or voterid!='" + string.Empty + "') and accountimage!='" + string.Empty + "' ";
         //   string str = " select  count(*) as totcount   from AgentInformation   where Plant_Code='" + pcode + "'";

            string str = "select COUNT(*) as totcount from(select b.Agent_Id  from (select Agent_id from Procurement   where Plant_Code='" + pcode + "' and Prdate between '" + firstdate + "' and '" + todate + "') as a left join(select  Agent_Id   from AgentInformation   where Plant_Code='" + pcode + "'  ) as  b on a.Agent_id=b.Agent_Id group by a.Agent_id,b.Agent_Id )    as ab where ab.Agent_Id is not null ";

            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    agentinformationtotal = Convert.ToInt16(dr["totcount"].ToString());

                }




            }
            else
            {

                agentinformationtotal = 0;

            }
        }
        catch
        {


        }

    }


    //public void incompleteagets()
    //{
    //    try
    //    {
    //        countincompleteagents = 0;
    //        SqlConnection con = new SqlConnection(connStr);
    //        //  select *   from AgentInformation   where Plant_Code='" + pcode + "' and ( image!='" + string.Empty + "' or RationCartno!='" + string.Empty + "' or voterid!='" + string.Empty + "') and accountimage!='" + string.Empty + "' ";
    //      //  string str = " select *   from AgentInformation   where Plant_Code='155' and   image is not null  and( RationCartno  is not null or voterid is not null  or Aadharimage is not null ) and accountimage!=''   ";

    //        string str = "select *   from AgentInformation   where Plant_code='"+pcode+"'  and  ((len(Aadharimage) > 0) or (len(RationCartno) > 0) or (len(voterid) > 0)or (len(Accountimage) > 0) or (len(image) > 0))";

    //        SqlCommand cmd = new SqlCommand(str, con);
    //        con.Open();
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                countincompleteagents = countincompleteagents + 1;

    //            }

    //            Label47.Text = countcompleteagents.ToString();


    //        }
    //    }
    //    catch
    //    {


    //    }

    //}

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button3.Visible = false;
        pcode = ddl_Plantcode.SelectedItem.Value;

        getbilldate();
        counttotagents();
        completeagets();
        totagentinsertedcount();

        try{
         Label49.Text =  ( agentinformationtotal - countcompleteagents ).ToString();

             incomp =   Convert.ToInt16(Label49.Text);
            // Label49.Text = (agentinformationtotal).ToString();

         //   Label49.Text = countincompleteagents.ToString();
        //    remainagents = (countagents - countcompleteagents + agentinformationtotal);
            Label51.Text = (countagents - agentinformationtotal).ToString();
            int tmp1 = Convert.ToInt16(Label51.Text);

            if(tmp1 < 0)
            {
                Label49.Text = (agentinformationtotal - countcompleteagents).ToString();
                incomp =  Convert.ToInt16(Label49.Text);
            }

            if (Rtoall.Checked == true)
            {

                gridview();
                GridView2.Visible = false;

            }
            if (rdosingle.Checked == true)
            {
                getagentid();
                gridview();
                GridView2.Visible = false;

            }

        }
        catch (Exception ex)
        {

            ex.Message.ToString();

        }

        gridview();
        GridView2.Visible = false;
        GridView1.Visible = true;
     
        Label53.Visible = false;
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
        totagents.Checked = false;
        if (Rtoall.Checked == true)
        {
            totagents.Checked = false;
            Label21.Visible = false;
            ddl_Agentid.Visible = false;

            rdosingle.Checked = false;
            Rtoall.Checked = true;
           // gridview();
        }

        if (rdosingle.Checked == true)
        {
            rdosingle.Checked = true;
            Rtoall.Checked = false;
            Label21.Visible = true;
            ddl_Agentid.Visible = true;
            totagents.Checked = false;
        }


    }
    protected void rdosingle_CheckedChanged(object sender, EventArgs e)
    {

        rdosingle.Checked = true;
        Rtoall.Checked = false;
        totagents.Checked = false;
        if (Rtoall.Checked == true)
        {
            totagents.Checked = false;
            Label21.Visible = false;
            ddl_Agentid.Visible = false;
            rdosingle.Checked = false;
            Rtoall.Checked = true;
          //  gridview();

        }

        if (rdosingle.Checked == true)
        {
            totagents.Checked = false;
            rdosingle.Checked = true;
            Rtoall.Checked = false;
            getagentid();
            Label21.Visible = true;
            ddl_Agentid.Visible = true;
          //  gridview();

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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        




            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "PlantName:" + ddl_Plantname.Text;
                HeaderCell2.ColumnSpan = 13;
                HeaderCell2.ForeColor = System.Drawing.Color.Brown;
                HeaderCell2.BackColor = System.Drawing.Color.White;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
                HeaderCell2.Font.Bold = true;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            }

               
    }
    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    Label49.Visible = true;
    //    Label51.Visible = true;
    //    Label49.ForeColor = System.Drawing.Color.Red;
    //    Label51.ForeColor = System.Drawing.Color.Red;
    //    //string s = "Animated Label";
    //    //string[] l;
    //    //int i = 0, j = 0;




    //    //l = new string[s.Length];
    //    //for (int i = 0; i < s.Length; i++)
    //    //    l[i] = s[i].ToString();
    //    ////
    //    ////setting up the 3 label's Location and font properties into same
    //    ////
    //    //Label51.Location = Label51.Location = Label49.Location = new Point(50, 30);
    //    //Label51.Font = Label51.Font = Label51.Font = new System.Drawing.Font("Microsoft Sans Serif",
    //    //    24F,
    //    //    System.Drawing.FontStyle.Bold,
    //    //    System.Drawing.GraphicsUnit.Point,
    //    //    ((byte)(0)));
    //    ////
    //    ////giving different color to the middile label(label2)
    //    ////
    //    //this.label2.ForeColor = System.Drawing.Color.Blue;
    //    ////
    //    //label2.Text = label3.Text = "";
    //}
    //protected void Timer2_Tick(object sender, EventArgs e)
    //{
    //    //Label49.Visible = false;
    //    //Label51.Visible = false;
    //    Label49.ForeColor = System.Drawing.Color.Orange;
    //    Label51.ForeColor = System.Drawing.Color.Orange;
    //}
    protected void Label45_PreRender(object sender, EventArgs e)
    {

    }


    protected void Label49_Load(object sender, EventArgs e)
    {
       
    }
    protected void totagents_CheckedChanged(object sender, EventArgs e)
    {





        rdosingle.Checked = false;
        Rtoall.Checked = false;
        totagents.Checked = true;
        if (totagents.Checked == true)
        {

            Label21.Visible = false;
            ddl_Agentid.Visible = false;
            rdosingle.Checked = false;
            Rtoall.Checked = false;
            totagents.Checked = true;
            gridview();

        }

       
    }
    protected void btnSignin_Click(object sender, EventArgs e)
    {
        popgridview();
    }
    
    protected void Label50_Click(object sender, EventArgs e)
    {

        Button3.Visible = true;
        gridval = 1;
        GridView1.Visible = false;
        GridView2.Visible = true;
        Label53.Text = ddl_Plantname.Text + " Pending Agent List";
      
     //   Label53.Visible = true;
        popgridview();
    }
    protected void Label48_Click(object sender, EventArgs e)
    {
        Button3.Visible = true;
        gridval = 2;
        GridView1.Visible = false;
        GridView2.Visible = true;
        Label53.Text = ddl_Plantname.Text + " Incomplete Agent List";
   
      //  Label53.Visible = true;
        popgridview();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_Plantname.SelectedItem.Text + "' " + "   " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                popgridview();

                GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView2.HeaderRow.Cells)
                {
                    cell.BackColor = GridView2.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
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


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            //  HeaderCell2.Text = "Agents Same Account or Name List";  lbldisplay
            HeaderCell2.Text = Label53.Text;
            HeaderCell2.ColumnSpan = 4;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;


        }
    }
}