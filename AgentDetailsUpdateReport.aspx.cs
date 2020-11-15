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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class AgentDetailsUpdateReport : System.Web.UI.Page
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
    string str;
    string address,mobile;
    string value;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                Session["Image"] = null;

                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
               // managmobNo = Session["managmobNo"].ToString();
                if (roleid < 3)
                {
                    loadsingleplant();
                    getagentid();
                  //  GetRouteID();
                }
                else
                {
                    LoadPlantcode();
                    getagentid();
                }
                //  pcode = ddl_Plantcode.SelectedItem.Value;
                 gridview();

                GridView1.Visible = true;

               


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

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }


            Label47.Visible = false;
            txtmobile.Visible = false;
            Label48.Visible = false;
            Address.Visible = false;

            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;
            lblmessage.Visible = false;
            lblmsg.Visible = false;

        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                //  pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();
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
                    string sqlstr = "select  *    from  AgentInformation where   plant_code='" + pcode + "' order by tid asc ";

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

        GridView1.PageIndex = e.NewPageIndex;
        gridview();
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
            getagentid();

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
      //  gridview();
    }


    public void GETAGENTMOBILE()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connStr);
        con.Open();
        string STR = "SELECT *    FROM  AGENT_MASTER    WHERE PLANT_CODE='" + pcode + "' AND AGENT_ID='" + ddl_Agentid.Text + "'";

        SqlCommand cmd = new SqlCommand(STR,con);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.HasRows)
        {

            while (dr.Read())
            {

                txtmobile.Text = dr["phone_Number"].ToString();

            }

        }

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
            HeaderCell2.Text = "PlantName:" + ddl_Plantname.Text ;
            HeaderCell2.ColumnSpan = 12;
            HeaderCell2.ForeColor = System.Drawing.Color.Brown;
            HeaderCell2.BackColor = System.Drawing.Color.White;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        }
    }
    protected void uploadthar_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



            string ext = System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);
            if ((FileUpload1.FileBytes.Length < 716800) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
            {



                path = Server.MapPath("image/") + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("image/") + FileUpload1.FileName);

                FileUpload1.SaveAs(path);
                updateimage.ImageUrl = "image/" + FileUpload1.FileName;


                updateimage.Visible = true;
                //lblmessage5.Text = "File uploaded successfully.";

                //lblmessage5.ForeColor = System.Drawing.Color.Green;

                lblmessage.Visible = false;



            }
            else
            {




                updateimage.ImageUrl = "";
                lblmessage.Visible = true;
                lblmessage.Text = "File greater than 700 KB or File Format";
                lblmessage.ForeColor = System.Drawing.Color.Red;




            }



        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please select file.";
            lblmessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        if (ddlupdatetype.SelectedValue != "0")
        {

            if (ddlupdatetype.Text == "1")
            {
                path = updateimage.ImageUrl;

                //   str = "Update AgentInformation  set  image='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";

                value = "Image";

                //    str = "Update AgentInformation  set    "+ value +" ='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";



            }

            if (ddlupdatetype.Text == "2")
            {
                path = updateimage.ImageUrl;
                value = "Aadharimage";

                // str = "Update AgentInformation  set  Aadharimage='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
            }



            if (ddlupdatetype.Text == "3")
            {
                path = updateimage.ImageUrl;
                value = "Rationimage";
                //  str = "Update AgentInformation  set  Rationimage='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
            }




            if (ddlupdatetype.Text == "4")
            {
                path = updateimage.ImageUrl;
                value = "voterimage";
                // str = "Update AgentInformation  set  voterimage='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
            }



            if (ddlupdatetype.Text == "5")
            {
                path = updateimage.ImageUrl;
                value = "pancardimage";
                //  str = "Update AgentInformation  set  pancardimage='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
            }


            if (ddlupdatetype.Text == "6")
            {
                path = updateimage.ImageUrl;
                value = "Accountimage";
                //    str = "Update AgentInformation  set  Accountimage='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";

            }



            if ((ddlupdatetype.SelectedValue == "1") || (ddlupdatetype.SelectedValue == "2") || (ddlupdatetype.SelectedValue == "3") || (ddlupdatetype.SelectedValue == "4") || (ddlupdatetype.SelectedValue == "5") || (ddlupdatetype.SelectedValue == "6"))
            {



                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connStr);
                con.Open();
                path = updateimage.ImageUrl;








                if (path != string.Empty)
                {

                    str = "Update AgentInformation  set    " + value + " ='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    //  str = "Update AgentInformation  set  image='" + path + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Updated SuccessFully";
                    updateimage.ImageUrl = "";
                    Address.Text = "";
                    Address.Visible = false;
                    updateimage.Visible = false;
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                }
                else
                {
                    lblmsg.Text = "Please Select Image";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Visible = true;
                }


            }


            if ((ddlupdatetype.SelectedValue == "7"))
            {
                lblmsg.Text = "";
                if (Address.Text != string.Empty)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connStr);
                    str = "Update AgentInformation  set  Address='" + Address.Text + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";

                    SqlCommand cmd = new SqlCommand(str, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Agent Address Updated SuccessFully";
                    txtmobile.Text = "";
                    Address.Text = "";
                    txtmobile.Visible = false;
                    lblmsg.Visible = false;
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    updateimage.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                    //   ddlupdatetype.Items.Clear();
                    Label48.Visible = false;
                    Address.Visible = false;

                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please Check  Address";
                }
            }




            if ((ddlupdatetype.SelectedValue == "8"))
            {
                lblmsg.Text = "";
                if (txtmobile.Text != string.Empty)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connStr);
                    str = "Update AgentInformation  set  Mobile='" + txtmobile.Text + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Agent Mobile Updated SuccessFully";
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    updateimage.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                    //      ddlupdatetype.Items.Clear();
                    Label48.Visible = false;
                    Address.Visible = false;
                    Label47.Visible = false;
                    txtmobile.Text = "";
                    txtmobile.Visible = false;


                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please Check  Mobile";
                }
            }



            if ((ddlupdatetype.SelectedValue == "9"))
            {
                lblmsg.Text = "";
                if (txtmobile.Text != string.Empty)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connStr);
                    str = "Update AgentInformation  set  AadharNo='" + txtmobile.Text + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Agent AadharNo Updated SuccessFully";
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    updateimage.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                    //       ddlupdatetype.Items.Clear();
                    Label48.Visible = false;
                    Address.Visible = false;
                    Label47.Visible = false;
                    txtmobile.Text = "";
                    txtmobile.Visible = false;


                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please Check  AadharNo";
                }
            }



            if ((ddlupdatetype.SelectedValue == "10"))
            {
                lblmsg.Text = "";
                if (txtmobile.Text != string.Empty)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connStr);
                    str = "Update AgentInformation  set  RationCartNo='" + txtmobile.Text + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Agent RationCartNo Updated SuccessFully";
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    updateimage.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                    //      ddlupdatetype.Items.Clear();
                    Label48.Visible = false;
                    Address.Visible = false;
                    txtmobile.Text = "";
                    txtmobile.Visible = false;

                    Label47.Visible = false;
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please Check  RationCartNo";
                }
            }



            if ((ddlupdatetype.SelectedValue == "11"))
            {
                lblmsg.Text = "";
                if (txtmobile.Text != string.Empty)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connStr);
                    str = "Update AgentInformation  set  VoterId='" + txtmobile.Text + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Agent VoterId Updated SuccessFully";
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    updateimage.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                    //     ddlupdatetype.Items.Clear();
                    Label48.Visible = false;
                    Address.Visible = false;
                    txtmobile.Text = "";
                    txtmobile.Visible = false;
                    Label47.Visible = false;
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please Check  VoterId";
                    lblmsg.Visible = true;
                }
            }



            if ((ddlupdatetype.SelectedValue == "12"))
            {
                lblmsg.Text = "";
                if (txtmobile.Text != string.Empty)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connStr);
                    str = "Update AgentInformation  set  PanCardNo='" + txtmobile.Text + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Agent PanCardNo Updated SuccessFully";
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    updateimage.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                    //    ddlupdatetype.Items.Clear();
                    Label48.Visible = false;
                    Address.Visible = false;
                    txtmobile.Text = "";
                    txtmobile.Visible = false;
                    Label47.Visible = false;

                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please Check  PanCardNo";
                }
            }



            if ((ddlupdatetype.SelectedValue == "13"))
            {
                lblmsg.Text = "";
                if (txtmobile.Text != string.Empty)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(connStr);
                    str = "Update AgentInformation  set  GuardianName='" + txtmobile.Text + "'   where plant_code='" + pcode + "'  and agent_id='" + ddl_Agentid.Text + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Agent GuardianName Updated SuccessFully";
                    FileUpload1.Visible = false;
                    uploadthar.Visible = false;
                    updateimage.Visible = false;
                    lblmsg.Visible = true;
                    gridview();
                    //      ddlupdatetype.Items.Clear();
                    Label48.Visible = false;
                    Address.Visible = false;
                    txtmobile.Text = "";
                    txtmobile.Visible = false;
                    //  lblmsg.Visible = false;
                    Label47.Visible = false;
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please Check  GuardianName";
                }
            }
        }

        else
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Please Check";


        }

    }
    protected void ddlupdatetype_SelectedIndexChanged(object sender, EventArgs e)
    {


        if ((ddlupdatetype.SelectedValue == "1") || (ddlupdatetype.SelectedValue == "2") || (ddlupdatetype.SelectedValue == "3") || (ddlupdatetype.SelectedValue == "4") || (ddlupdatetype.SelectedValue == "5") || (ddlupdatetype.SelectedValue == "6"))
        {




            FileUpload1.Visible = true;
            uploadthar.Visible = true;
            updateimage.Visible = true;

            Label47.Visible = false;
            txtmobile.Visible = false;
            Label48.Visible = false;
            Address.Visible = false;


            lblmsg.Visible = false;

        }

        if (ddlupdatetype.SelectedValue == "7") 
        {




            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;

            Label47.Visible = false;
            txtmobile.Visible = false;
            Label48.Visible = true;
            Address.Visible = true;

            lblmsg.Visible = false;


        }



        if (ddlupdatetype.SelectedValue == "8")
        {


            GETAGENTMOBILE();

            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;
            Label47.Visible = true;
            txtmobile.Visible = true;
            Label48.Visible = false;
            Address.Visible = false;

            Label47.Text = "Mobile No";
            txtmobile.Enabled = false;
            lblmsg.Visible = false;

        }

        if (ddlupdatetype.SelectedValue == "9")
        {


            txtmobile.Enabled = true;

            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;
            lblmsg.Visible = false;



            Label47.Visible = true;
            txtmobile.Visible = true;
            Label48.Visible = false;
            Address.Visible = false;
            Label47.Text = "Aadhaar No";
            txtmobile.Text = "";

        }


        if (ddlupdatetype.SelectedValue == "10")
        {


            txtmobile.Enabled = true;

            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;

            Label47.Visible = true;
            txtmobile.Visible = true;
            Label48.Visible = false;
            Address.Visible = false;

            Label47.Text = "Ration No";
            txtmobile.Text = "";

            lblmsg.Visible = false;
        }

        if (ddlupdatetype.SelectedValue == "11")
        {

            txtmobile.Enabled = true;


            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;
            Label47.Visible = true;
            txtmobile.Visible = true;
            Label48.Visible = false;
            Address.Visible = false;
            lblmsg.Visible = false;
            Label47.Text = "Voter No";

            txtmobile.Text = "";
        }


        if (ddlupdatetype.SelectedValue == "12")
        {


            txtmobile.Enabled = true;

            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;

            Label47.Visible = true;
            txtmobile.Visible = true;
            Label48.Visible = false;
            Address.Visible = false;
            lblmsg.Visible = false;
            Label47.Text = "Pancard No";

            txtmobile.Text = "";
        }
        if (ddlupdatetype.SelectedValue == "13")
        {


            txtmobile.Enabled = true;

            FileUpload1.Visible = false;
            uploadthar.Visible = false;
            updateimage.Visible = false;
            Label47.Visible = true;
            txtmobile.Visible = true;
            Label48.Visible = false;
            Address.Visible = false;
            Label47.Text = "Nominees Name";
            lblmsg.Visible = false;
            txtmobile.Text = "";
            
        }
       

        




    }


  
   
}