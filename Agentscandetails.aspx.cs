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
public partial class Agentscandetails : System.Web.UI.Page
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
    int validateval;
    int mode=0;
    int mode1;
    int total = 0;
    int total1 = 0;
    int tottal2 = 0;
    int tottal3 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {




        if (IsPostBack != true)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {



                ccode = Session["Company_code"].ToString();
                 pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                managmobNo = Session["managmobNo"].ToString();

                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    getagentid();
                }
                else
                {
                    LoadPlantcode();
                }

             //   LoadPlantcode();
                //  pcode = ddl_Plantcode.SelectedItem.Value;
                  gridview();

                GridView1.Visible = true;

                dtm = System.DateTime.Now;
                txt_date.Text = dtm.ToShortDateString();
                txt_date.Text = dtm.ToString("dd/MM/yyyy");
                //    Button1.Visible = false;
                //GridView2.Visible = false;
                //ddl_Plantnam.Visible = false;
                //Button2.Visible = false;
                //Label17.Visible = false;
                //Label18.Visible = false;
                //dtp_buff.Visible = false;
                //dtp_cow.Visible = false;

                //    Label20.Visible = false;


                ddl_cantype.Enabled = false;
                txt_Noofcans.Enabled = false;

                ddl_DpuSys.Enabled = false;
                txt_NoofDpu.Enabled = false;
                lbl_msg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }
        else
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                ccode = Session["Company_code"].ToString();
                //  pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                managmobNo = Session["managmobNo"].ToString();
                //ddl_Agentid.SelectedIndex = ddl_Agentid.SelectedIndex;

                pcode = ddl_Plantcode.SelectedItem.Value;


               getagentid1();

             //   gridview();

             //      getagentid();

                ddl_cantype.Enabled = false;
                txt_Noofcans.Enabled = false;

                ddl_DpuSys.Enabled = false;
                txt_NoofDpu.Enabled = false;
                GridView1.Visible = true;
                lbl_msg.Visible = false;





                if (Rdoissue.Checked == true)
                {
                    if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                    {


                        ddl_cantype.Enabled = true;
                        txt_Noofcans.Enabled = true;

                        ddl_DpuSys.Visible = true;
                        txt_NoofDpu.Visible = true;




                    }
                    else if (chk_can.Checked == true)
                    {


                        ddl_cantype.Enabled = true;
                        txt_Noofcans.Enabled = true;

                        ddl_DpuSys.Enabled = false;
                        txt_NoofDpu.Enabled = false;
                        //ddl_DpuSys.Text = "--------------Select--------------";
                        //txt_NoofDpu.Text = "";

                    }

                    else if (chk_dpu.Checked == true)
                    {

                        ddl_cantype.Enabled = false;
                        txt_Noofcans.Enabled = false;
                        //txt_Noofcans.Text = "";
                        //ddl_cantype.Text = "--------------Select--------------";

                        ddl_DpuSys.Enabled = true;
                        txt_NoofDpu.Enabled = true;


                    }
                }
                if (rdoReceive.Checked == true)
                {

                    if (chk_can.Checked == true)
                    {
                        if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                        {


                            ddl_cantype.Enabled = true;
                            txt_Noofcans.Enabled = true;

                            ddl_DpuSys.Visible = true;
                            txt_NoofDpu.Visible = true;




                        }
                        else if (chk_can.Checked == true)
                        {


                            ddl_cantype.Enabled = true;
                            txt_Noofcans.Enabled = true;

                            //txt_Noofcans.Text = "";
                            //ddl_cantype.Text = "--------------Select--------------";
                            ddl_DpuSys.Enabled = false;
                            txt_NoofDpu.Enabled = false;


                        }

                        else if (chk_dpu.Checked == true)
                        {

                            ddl_cantype.Enabled = false;
                            txt_Noofcans.Enabled = false;
                            ddl_cantype.Text = "";



                            ddl_DpuSys.Enabled = true;
                            txt_NoofDpu.Enabled = true;


                        }
                    }

                    if (chk_dpu.Checked == true)
                    {
                        if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                        {


                            ddl_cantype.Enabled = true;
                            txt_Noofcans.Enabled = true;

                            ddl_DpuSys.Visible = true;
                            txt_NoofDpu.Visible = true;




                        }
                        else if (chk_can.Checked == true)
                        {


                            ddl_cantype.Enabled = true;
                            txt_Noofcans.Enabled = true;

                            //txt_Noofcans.Text = "";
                            //ddl_cantype.Text = "--------------Select--------------";

                            ddl_DpuSys.Enabled = false;
                            txt_NoofDpu.Enabled = false;


                        }

                        else if (chk_dpu.Checked == true)
                        {

                            ddl_cantype.Enabled = false;
                            txt_Noofcans.Enabled = false;

                            ddl_DpuSys.Enabled = true;
                            txt_NoofDpu.Enabled = true;


                        }
                    }

                }






                if (Rdoissue.Checked == true)
                {
                    if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                    {


                        ddl_cantype.Enabled = true;
                        txt_Noofcans.Enabled = true;

                        ddl_DpuSys.Enabled = true;
                        txt_NoofDpu.Enabled = true;




                    }
                    else if (chk_can.Checked == true)
                    {


                        ddl_cantype.Enabled = true;
                        txt_Noofcans.Enabled = true;

                        //txt_Noofcans.Text = "";
                        //ddl_cantype.Text = "--------------Select--------------";

                        ddl_DpuSys.Enabled = false;
                        txt_NoofDpu.Enabled = false;


                    }

                    else if (chk_dpu.Checked == true)
                    {

                        ddl_cantype.Enabled = false;
                        txt_Noofcans.Enabled = false;

                        ddl_DpuSys.Enabled = true;
                        txt_NoofDpu.Enabled = true;


                    }
                }
                if (rdoReceive.Checked == true)
                {


                    if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                    {


                        ddl_cantype.Enabled = true;
                        txt_Noofcans.Enabled = true;

                        ddl_DpuSys.Enabled = true;
                        txt_NoofDpu.Enabled = true;




                    }
                    else if (chk_can.Checked == true)
                    {


                        ddl_cantype.Enabled = true;
                        txt_Noofcans.Enabled = true;

                        ddl_DpuSys.Enabled = false;
                        txt_NoofDpu.Enabled = false;


                    }

                    else if (chk_dpu.Checked == true)
                    {

                        ddl_cantype.Enabled = false;
                        txt_Noofcans.Enabled = false;

                        ddl_DpuSys.Enabled = true;
                        txt_NoofDpu.Enabled = true;


                    }


                    if (chk_dpu.Checked == true)
                    {
                        if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                        {


                            ddl_cantype.Enabled = true;
                            txt_Noofcans.Enabled = true;

                            ddl_DpuSys.Visible = true;
                            txt_NoofDpu.Visible = true;




                        }
                        else if (chk_can.Checked == true)
                        {


                            ddl_cantype.Enabled = true;
                            txt_Noofcans.Enabled = true;
                            //txt_Noofcans.Text = "";
                            //ddl_cantype.Text = "--------------Select--------------";


                            ddl_DpuSys.Enabled = false;
                            txt_NoofDpu.Enabled = false;


                        }

                        else if (chk_dpu.Checked == true)
                        {

                            ddl_cantype.Enabled = false;
                            txt_Noofcans.Enabled = false;

                            ddl_DpuSys.Enabled = true;
                            txt_NoofDpu.Enabled = true;


                        }
                    }

                }










                //  getgrid();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }



    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        validate();
        if (validateval == 1)
        {

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            SqlConnection con = new SqlConnection(connStr);
            //Open The Connection  
            con.Open();












            

            if ((Rdoissue.Checked == true) &&  (chk_can.Checked == true) && (chk_dpu.Checked == true))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO AgentsCanIssuing(Plant_code,Plant_Name,Route_Id,Agent_id,Cantype,DateIssuingOrReceiving,CanIssuing,CanReceiving,IssuerOrRecesiverName,DpuReceiving,DpuIssuing,DpuType)values(@Plant_code,@Plant_Name,@Route_Id,@Agent_id,@Cantype,@DateIssuingOrReceiving,@CanIssuing,@CanReceiving,@IssuerOrRecesiverName,@DpuReceiving,@DpuIssuing,@DpuType)", con);

                cmd.Parameters.Add("@Plant_code", pcode);
                cmd.Parameters.Add("@Plant_Name", ddl_Plantname.Text);
                cmd.Parameters.Add("@Route_Id", routeid);
                cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                cmd.Parameters.Add("@Cantype", ddl_cantype.Text);
                cmd.Parameters.Add("@DateIssuingOrReceiving", d1);
                cmd.Parameters.Add("@CanIssuing", txt_Noofcans.Text);
                cmd.Parameters.Add("@CanReceiving", mode);
                cmd.Parameters.Add("@IssuerOrRecesiverName", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@DpuReceiving", mode);
                cmd.Parameters.Add("@DpuIssuing",  txt_NoofDpu.Text );
                cmd.Parameters.Add("@DpuType", ddl_DpuSys.Text);

                //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_msg.Text = "Inserted SuccessFully";
                lbl_msg.Visible = true;
                gridview();
                clear();
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    getagentid();
                }
                else
                {
                    LoadPlantcode();
                }
            }




            else if ((Rdoissue.Checked == true) && (chk_can.Checked == true))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO AgentsCanIssuing(Plant_code,Plant_Name,Route_Id,Agent_id,DateIssuingOrReceiving,IssuerOrRecesiverName,CanIssuing,CanReceiving,CanType,DpuReceiving,DpuIssuing)values(@Plant_code,@Plant_Name,@Route_Id,@Agent_id,@DateIssuingOrReceiving,@IssuerOrRecesiverName,@CanIssuing,@CanReceiving,@CanType,@DpuReceiving,@DpuIssuing)", con);
                cmd.Parameters.Add("@Plant_code", pcode);
                cmd.Parameters.Add("@Plant_Name", ddl_Plantname.Text);
                cmd.Parameters.Add("@Route_Id", routeid);
                cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                cmd.Parameters.Add("@DateIssuingOrReceiving", d1);
                cmd.Parameters.Add("@IssuerOrRecesiverName", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@CanIssuing",txt_Noofcans.Text );
                cmd.Parameters.Add("@CanReceiving", mode);
                cmd.Parameters.Add("@CanType", ddl_cantype.Text);
                cmd.Parameters.Add("@DpuReceiving", mode); 
                cmd.Parameters.Add("@DpuIssuing",mode);
               //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_msg.Text = "Inserted SuccessFully";
                lbl_msg.Visible = true;
                gridview();
                clear();
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    getagentid();
                }
                else
                {
                    LoadPlantcode();
                }
            }


            else if ((Rdoissue.Checked == true) && (chk_dpu.Checked == true))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO AgentsCanIssuing(Plant_code,Plant_Name,Route_Id,Agent_id,DateIssuingOrReceiving,IssuerOrRecesiverName,DpuReceiving,DpuIssuing,DpuType,CanIssuing,CanReceiving)values(@Plant_code,@Plant_Name,@Route_Id,@Agent_id,@DateIssuingOrReceiving,@IssuerOrRecesiverName,@DpuReceiving,@DpuIssuing,@DpuType,@CanIssuing,@CanReceiving)", con);
                cmd.Parameters.Add("@Plant_code", pcode);
                cmd.Parameters.Add("@Plant_Name", ddl_Plantname.Text);
                cmd.Parameters.Add("@Route_Id", routeid);
                cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                cmd.Parameters.Add("@DateIssuingOrReceiving", d1);
                cmd.Parameters.Add("@IssuerOrRecesiverName", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@DpuReceiving", mode);
                cmd.Parameters.Add("@DpuIssuing", txt_NoofDpu.Text);
                cmd.Parameters.Add("@DpuType", ddl_DpuSys.Text);
                cmd.Parameters.Add("@CanIssuing",mode); 
                cmd.Parameters.Add("@CanReceiving", mode);
                //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_msg.Text = "Inserted SuccessFully";
                lbl_msg.Visible = true;
                gridview();
                clear();
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    getagentid();
                }
                else
                {
                    LoadPlantcode();
                }
            }

            
            else  if ((rdoReceive.Checked == true) && (chk_can.Checked == true) && (chk_dpu.Checked == true))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO AgentsCanIssuing(Plant_code,Plant_Name,Route_Id,Agent_id,CanType,DateIssuingOrReceiving,CanIssuing,CanReceiving,IssuerOrRecesiverName,DpuReceiving,DpuIssuing,DpuType)values(@Plant_code,@Plant_Name,@Route_Id,@Agent_id,@CanType,@DateIssuingOrReceiving,@CanIssuing,@CanReceiving,@IssuerOrRecesiverName,@DpuReceiving,@DpuIssuing,@DpuType)", con);
                cmd.Parameters.Add("@Plant_code", pcode);
                cmd.Parameters.Add("@Plant_Name", ddl_Plantname.Text);
                cmd.Parameters.Add("@Route_Id", routeid);
                cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                cmd.Parameters.Add("@Cantype", ddl_cantype.Text);
                cmd.Parameters.Add("@agent_name", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@DateIssuingOrReceiving", d1);
                cmd.Parameters.Add("@CanIssuing", mode);
                cmd.Parameters.Add("@CanReceiving", txt_Noofcans.Text);
                cmd.Parameters.Add("@IssuerOrRecesiverName", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@DpuReceiving", txt_NoofDpu.Text);
                cmd.Parameters.Add("@DpuIssuing", mode);
                cmd.Parameters.Add("@DpuType", ddl_DpuSys.Text);
                //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_msg.Text = "Inserted SuccessFully";
                lbl_msg.Visible = true;
                gridview();
                clear();
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    getagentid();
                }
                else
                {
                    LoadPlantcode();
                }
            }
            else if ((rdoReceive.Checked == true) && (chk_can.Checked == true) )
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO AgentsCanIssuing(Plant_code,Plant_Name,Route_Id,Agent_id,CanType,DateIssuingOrReceiving,CanIssuing,CanReceiving,IssuerOrRecesiverName,DpuReceiving,DpuIssuing)values(@Plant_code,@Plant_Name,@Route_Id,@Agent_id,@CanType,@DateIssuingOrReceiving,@CanIssuing,@CanReceiving,@IssuerOrRecesiverName,@DpuReceiving,@DpuIssuing)", con);
                cmd.Parameters.Add("@Plant_code", pcode);
                cmd.Parameters.Add("@Plant_Name", ddl_Plantname.Text);
                cmd.Parameters.Add("@Route_Id", routeid);
                cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                cmd.Parameters.Add("@Cantype", ddl_cantype.Text);
                cmd.Parameters.Add("@agent_name", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@DateIssuingOrReceiving", d1);
                cmd.Parameters.Add("@CanIssuing", mode);
                cmd.Parameters.Add("@CanReceiving", txt_Noofcans.Text);
                cmd.Parameters.Add("@IssuerOrRecesiverName", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@DpuReceiving", mode);
                cmd.Parameters.Add("@DpuIssuing", mode);
                //cmd.Parameters.Add("@DpuReceiving", txt_NoofDpu.Text);
                //cmd.Parameters.Add("@DpuIssuing", mode);
                //cmd.Parameters.Add("@DpuType", ddl_DpuSys.Text);
                //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                cmd.ExecuteNonQuery();
                con.Close();
          //      WebMsgBox.Show("inserted Successfully");
                lbl_msg.Text = "Inserted SuccessFully";
                lbl_msg.Visible = true;
                gridview();
                clear();
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    getagentid();
                }
                else
                {
                    LoadPlantcode();
                }
            }
            else if ((rdoReceive.Checked == true) && (chk_dpu.Checked== true))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO AgentsCanIssuing(Plant_code,Plant_Name,Route_Id,Agent_id,DateIssuingOrReceiving,IssuerOrRecesiverName,DpuReceiving,DpuIssuing,DpuType,CanIssuing,CanReceiving)values(@Plant_code,@Plant_Name,@Route_Id,@Agent_id,@DateIssuingOrReceiving,@IssuerOrRecesiverName,@DpuReceiving,@DpuIssuing,@DpuType,@CanIssuing,@CanReceiving)", con);
                cmd.Parameters.Add("@Plant_code", pcode);
                cmd.Parameters.Add("@Plant_Name", ddl_Plantname.Text);
                cmd.Parameters.Add("@Route_Id", routeid);
                cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                cmd.Parameters.Add("@DateIssuingOrReceiving", d1);
                cmd.Parameters.Add("@IssuerOrRecesiverName", txt_Canissuorrecname.Text);
                cmd.Parameters.Add("@DpuReceiving", txt_NoofDpu.Text);
                cmd.Parameters.Add("@DpuIssuing", mode);
                cmd.Parameters.Add("@DpuType", ddl_DpuSys.Text);
                cmd.Parameters.Add("@CanIssuing", mode);
                cmd.Parameters.Add("@CanReceiving", mode);
                //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_msg.Text = "Inserted SuccessFully";
                lbl_msg.Visible = true;
                gridview();
                clear();
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    getagentid();
                }
                else
                {
                    LoadPlantcode();
                }
            }






















        }
        if (validateval == 0)
        {

            WebMsgBox.Show("Please Fill Required Filed");



        }






    }





    public void clear()
    {

        dtm = System.DateTime.Now;
        txt_date.Text = dtm.ToShortDateString();
        txt_date.Text = dtm.ToString("dd/MM/yyyy");
        ddl_cantype.Text = "--------------Select--------------";
        txt_Canissuorrecname.Text = "";
        txt_Noofcans.Text = "";
        ddl_DpuSys.Text = "--------------Select--------------";
        txt_NoofDpu.Text = "";
        Rdoissue.Checked = false;
        rdoReceive.Checked = false;
        chk_can.Checked = false;
        chk_dpu.Checked = false;


    }






    public void validate()
    {

        if (Rdoissue.Checked == true || rdoReceive.Checked == true)
        {

            //if ((ddl_Plantname.Text != string.Empty) && (ddl_Agentid.Text != string.Empty) && (txt_Canissuorrecname.Text != string.Empty) && (txt_date.Text != string.Empty) && (txt_Canissuorrecname.Text != string.Empty) && (txt_Noofcans.Text != string.Empty))
            //{

                validateval = 1;

            //}
            //else
            //{

            //    validateval = 0;


            //}
        }
        else
        {

            WebMsgBox.Show("Please Select Can Mode");

        }
    }




    protected void Rtoall_CheckedChanged(object sender, EventArgs e)
    {
      
    }
    protected void rdosingle_CheckedChanged(object sender, EventArgs e)
    {
        rdoReceive.Checked = true;
        Rdoissue.Checked = false;
        
        if (Rdoissue.Checked == true)
        {
            mode = 1;
            mode1 = 0;
        }
        if (rdoReceive.Checked == true)
        {

            mode = 0;
            mode1 = 1;


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


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                //      string sqlstr = "SELECT Plant_Name,PlantType,StateName,PlaceName,Mailid,Mgrname,MgrMobile,PlantPhone,InchargeName,InchargeMobile,analyzermobile,weighermobile,securitymobile  FROM PlantDetails   order by  Tid desc";
                //      string sqlstr = "SELECT Plant_Name as PlantName,PlaceName,Mailid,Mgrname as ManagerName,MgrMobile as ManagerMobile,PlantPhone as PhoneNo,InchargeName,InchargeMobile  FROM PlantDetails   order by  Tid desc";
                //    string sqlstr = "select distinct a.Agent_Id as AgentId,a.AgentRateChartmode  as RateChartType,b.Ratechart_Id as RatechartName from (select   Agent_Id,AgentRateChartmode   from    agent_master   where   plant_code='" + pcode + "' and route_id='" + routeid + "'  ) as a left join(	select  *    from   procurement where Plant_Code='" + pcode + "' and  route_id='" + routeid + "' ) as b on a.Agent_Id=b.Agent_id order by a.Agent_Id asc ";
                //   string sqlstr = "select  a.agent_id,b.agentratechartmode  from( select distinct  agent_id    from    procurement    where   plant_code='" + pcode + "' and route_id='" + routeid + "') as a left join(select *   from   agent_master  where  plant_code='" + pcode + "' and route_id='" + routeid + "') as b on a.agent_id=b.agent_id   order by a.agent_id";

             //   string sqlstr = "select  Plant_Name,Agent_id,convert(varchar,DateIssuingOrReceiving,103) as DateIssuingOrReceiving,CanIssuing,CanReceiving,isnull(CanType,DpuIssuing,DpuReceiving,DpuType    from  AgentsCanIssuing  where plant_code='" + pcode + "'";
            //    string sqlstr = " select  Plant_Name,Agent_id,convert(varchar,DateIssuingOrReceiving,103) as DateIssuingOrReceiving,CanIssuing,CanReceiving, ISNULL(CanType,0)as CanType,DpuIssuing,DpuReceiving,ISNULL(DpuType,0)as DpuType   from  AgentsCanIssuing  where plant_code= '" + pcode +"'";

                string sqlstr = " select  top 1 Plant_Name,Agent_id,convert(varchar,DateIssuingOrReceiving,103) as DateIssuingOrReceiving,ISNULL(CanIssuing,0)as CanIssuing,ISNULL(CanReceiving,0)as CanReceiving, ISNULL(CanType,0)as CanType,ISNULL(DpuIssuing,0)as DpuIssuing,ISNULL(DpuReceiving,0)as DpuReceiving,ISNULL(DpuType,0)as DpuType   from  AgentsCanIssuing    order by tid desc ";
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
        catch
        {



        }


    }




    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
         getagentid();
         gridview();
        //GetRouteID();
    }



    //public void GetRouteID()
    //{

    //    try
    //    {
    //        ddl_Routename.Items.Clear();
    //        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        SqlConnection con = new SqlConnection(connStr);
    //        con.Open();
    //        //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
    //        string str = "select distinct route_id,route_name  from Route_Master where plant_code='" + pcode + "'  order by route_id  asc ";
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {




    //            //  id = dr["route_id"].ToString();

    //            ddl_routeid.Items.Add(dr["route_id"].ToString());
    //            ddl_Routename.Items.Add(dr["route_id"].ToString() + "_" + dr["route_name"].ToString());



    //        }


    //    }

    //    catch
    //    {

    //        WebMsgBox.Show("NO MILK");

    //    }
        
    //}
    protected void ddl_Routename_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_routeid.SelectedIndex = ddl_Routename.SelectedIndex;
        //rid = ddl_routeid.SelectedItem.Value;
      //  getagentid();
    }


    public void getagentid()
    {

        try
        {
            ddl_Agentid.Items.Clear();
            //     txt_AgentName.Text = "";
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select distinct agent_id from agent_master where plant_code='" + pcode + "' ";
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




    public void getagentid1()
    {

        try
        {
            
            //     txt_AgentName.Text = "";
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select distinct route_id from agent_master where plant_code='" + pcode + "'  and Agent_id='" + ddl_Agentid.Text + "'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

             //   ddl_Agentid.Items.Add(dr["agent_id"].ToString());
                routeid = dr["route_id"].ToString();

                // txt_AgentName.Text = dr["Agent_Name"].ToString();


            }


        }

        catch
        {

            WebMsgBox.Show("NO MILK");

        }



    }

    protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
    {
        getagentid1();

      

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string nn=string.Empty;
        int qty;
        int qty1;
        int qty12;
        int qty123;
      //  string nn1=string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy = (Label)e.Row.FindControl("lblcanissue");
            try
            {
                 qty = Int32.Parse(lblqy.Text);
            }
            catch
            {

                 qty = 0;
            }
            total = total + qty;






         }
        
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalcanissue");
            lblTotalqty.Text = total.ToString();
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy1 = (Label)e.Row.FindControl("lblCanReceiving");
            try
            {
                 qty1 = Int32.Parse(lblqy1.Text);
            }
            catch
            {
                qty1=0;


            }
            total1 = total1 + qty1;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty1 = (Label)e.Row.FindControl("lblTotalCanReceiving");
            lblTotalqty1.Text = total1.ToString();
        }











        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy12 = (Label)e.Row.FindControl("lbldpuissue");
            try
            {
                qty12 = Int32.Parse(lblqy12.Text);
            }
            catch
            {
                qty12 = 0;

            }
            tottal2 = tottal2 + qty12;
           

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty12 = (Label)e.Row.FindControl("lblTotalDpuissue");
            lblTotalqty12.Text = tottal2.ToString();
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy123 = (Label)e.Row.FindControl("lbldpurec");
            try
            {
                qty123 = Int32.Parse(lblqy123.Text);
            }
            catch
            {

                qty123 = 0;

            }

            tottal3 = tottal3 + qty123;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty112 = (Label)e.Row.FindControl("lblTotalDpurec");
            lblTotalqty112.Text = tottal3.ToString();
        }







    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void chk_can_CheckedChanged(object sender, EventArgs e)
    {

        if (Rdoissue.Checked == true)
        {




            if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
            {


                ddl_cantype.Enabled = true;
                txt_Noofcans.Enabled = true;

                ddl_DpuSys.Visible = true;
                txt_NoofDpu.Visible = true;




            }
            else if (chk_can.Checked == true)
            {


                ddl_cantype.Enabled = true;
                txt_Noofcans.Enabled = true;

                ddl_DpuSys.Enabled = false;
                txt_NoofDpu.Enabled = false;


            }

            else if (chk_dpu.Checked == true)
            {

                ddl_cantype.Enabled = false;
                txt_Noofcans.Enabled = false;

                ddl_DpuSys.Enabled = true;
                txt_NoofDpu.Enabled = true;


            }
        }
        if (rdoReceive.Checked == true)
        {

            if (chk_can.Checked == true)
            {
                if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Visible = true;
                    txt_NoofDpu.Visible = true;




                }
                else if (chk_can.Checked == true)
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Enabled = false;
                    txt_NoofDpu.Enabled = false;


                }

                else if (chk_dpu.Checked == true)
                {

                    ddl_cantype.Enabled = false;
                    txt_Noofcans.Enabled = false;

                    ddl_DpuSys.Enabled = true;
                    txt_NoofDpu.Enabled = true;


                }
            }

            if (chk_dpu.Checked == true)
            {
                if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Visible = true;
                    txt_NoofDpu.Visible = true;




                }
                else if (chk_can.Checked == true)
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Enabled = false;
                    txt_NoofDpu.Enabled = false;


                }

                else if (chk_dpu.Checked == true)
                {

                    ddl_cantype.Enabled = false;
                    txt_Noofcans.Enabled = false;

                    ddl_DpuSys.Enabled = true;
                    txt_NoofDpu.Enabled = true;


                }
            }

        }









    }
    protected void chk_dpu_CheckedChanged(object sender, EventArgs e)
    {
        if (Rdoissue.Checked == true)
        {
            if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
            {


                ddl_cantype.Enabled = true;
                txt_Noofcans.Enabled = true;

                ddl_DpuSys.Enabled = true;
                txt_NoofDpu.Enabled = true;




            }
            else if (chk_can.Checked == true)
            {


                ddl_cantype.Enabled = true;
                txt_Noofcans.Enabled = true;

                ddl_DpuSys.Enabled = false;
                txt_NoofDpu.Enabled = false;


            }

            else if (chk_dpu.Checked == true)
            {

                ddl_cantype.Enabled = false;
                txt_Noofcans.Enabled = false;

                ddl_DpuSys.Enabled = true;
                txt_NoofDpu.Enabled = true;


            }
        }
        if (rdoReceive.Checked == true)
        {

           
                if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Enabled = true;
                    txt_NoofDpu.Enabled = true;




                }
                else if (chk_can.Checked == true)
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Enabled = false;
                    txt_NoofDpu.Enabled = false;


                }

                else if (chk_dpu.Checked == true)
                {

                    ddl_cantype.Enabled = false;
                    txt_Noofcans.Enabled = false;

                    ddl_DpuSys.Enabled = true;
                    txt_NoofDpu.Enabled = true;


               }
            

            if (chk_dpu.Checked == true)
            {
                if ((chk_can.Checked == true) && (chk_dpu.Checked == true))
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Visible = true;
                    txt_NoofDpu.Visible = true;




                }
                else if (chk_can.Checked == true)
                {


                    ddl_cantype.Enabled = true;
                    txt_Noofcans.Enabled = true;

                    ddl_DpuSys.Enabled = false;
                    txt_NoofDpu.Enabled = false;


                }

                else if (chk_dpu.Checked == true)
                {

                    ddl_cantype.Enabled = false;
                    txt_Noofcans.Enabled = false;

                    ddl_DpuSys.Enabled = true;
                    txt_NoofDpu.Enabled = true;


                }
            }

        }

    }
    protected void Rdoissue_CheckedChanged(object sender, EventArgs e)
    {
        Rdoissue.Checked = true;
        rdoReceive.Checked = false;

        chk_can.Checked = false;
        chk_dpu.Checked = false;

        if (Rdoissue.Checked == true)
        {
            mode = 1;
            mode1 = 0;


        }
        if (rdoReceive.Checked == true)
        {

            mode = 0;
            mode1 = 1;


        }
    }
    protected void rdoReceive_CheckedChanged(object sender, EventArgs e)
    {
        rdoReceive.Checked = true;
        Rdoissue.Checked = false;

        chk_can.Checked = false;
        chk_dpu.Checked = false;

        if (Rdoissue.Checked == true)
        {
            mode = 1;
            mode1 = 0;
        }
        if (rdoReceive.Checked == true)
        {

            mode = 0;
            mode1 = 1;


        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

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
}