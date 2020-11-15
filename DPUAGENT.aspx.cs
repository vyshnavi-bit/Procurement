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
using System.Windows.Forms;
using System.Text.RegularExpressions;
public partial class DPUAGENT : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    public string userid;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string strsql2 = string.Empty;
    string connStr2 = ConfigurationManager.ConnectionStrings["AMPSConnectionStringDpu"].ConnectionString;
    string strsql1 = string.Empty;
    string connStr1 = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DataSet ds;
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    string message;
    string globalbank;
    string date;
    string getbankid;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
           // managmobNo = Session["managmobNo"].ToString();
         //   userid = Session["User_ID"].ToString();
            dtm = System.DateTime.Now;
            dtm = System.DateTime.Now;

            date = ((dtm).ToString());


            Panel2.Visible = false;

            update.Visible = false;
            if (roleid < 3)
            {
               loadsingleplant();

               ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
               pcode = ddl_Plantcode.SelectedItem.Value;
               getdpuagentlist();
               getdpuproducerlist();


               if (roleid == 7)
               {
                   txt_mcartage1.Enabled = true;
                   txt_mcartage.Enabled = true;

               }
               else
               {

                   txt_mcartage1.Enabled = false;
                   txt_mcartage.Enabled = false;

               }

              
              
            }
            else
            {

                LoadPlantcode();
                if (roleid == 7)
                {
                    txt_mcartage1.Enabled = true;
                    txt_mcartage.Enabled = true;

                }
                else
                {

                    txt_mcartage1.Enabled = false;
                    txt_mcartage.Enabled = false;

                }

            }

            if (Chk_BankAccount.Checked == true)
            {
                show();
            }
            else
            {
                hide();




            }



        }

        else
        {

            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            pcode = ddl_Plantcode.SelectedItem.Value;
            ccode = Session["Company_code"].ToString();
            userid = roleid.ToString();
       //     pcode = ddl_Plantname.SelectedItem.Value;
            lblmsg.Visible = false;
            try
            {
                ddl_bankid.SelectedIndex = ddl_BankName.SelectedIndex;
                getbankid = ddl_bankid.SelectedItem.Value;
            }
            catch
            {

            }
          //  update.Visible = false;
           // Panel2.Visible = false;
            date = dtm.ToString();
            if (Chk_BankAccount.Checked == true)
            {
                show();
            }
            else
            {
                hide();




            }

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

    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        
        getalldetails();


      


        Panel1.Visible = false;
        Panel2.Visible = true;
       
      

    }


    public void updatefunction()
    {
        try
        {
            
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();
            string mtype;
            string updatestr;
             string paymode;
            if (Chk_BankAccount.Checked == false)
            {

              paymode  = "cash";

            }
            else
            {

                  paymode  = "BANK";

            }

            if (rbcow.Checked == true)
                mtype = "cow";
            else
                mtype = "Buffalo";
            updatestr = "Update DPUPRODUCERMASTER set company_code='"+ccode+"',payment_mode='"+paymode+"', Addeddate='" + System.DateTime.Now + "',userid='" + userid + "',Milk_Nature='" + mtype + "',Producer_Name='" + txt_magentname.Text + "',Cartage_Amt='" + txt_mcartage1.Text + "',SplBonus_Amt='" + txt_mcartage.Text + "',Ifsc_code='" + ddl_Ifsc.Text + "',Bank_Id='" + globalbank + "',Bank_Name='" + ddl_BankName.Text + "',Branch_Name='" + ddl_branchname.Text + "',phone_Number='" + txt_magentmobno.Text + "',Agent_AccountNo='" + txt_AgentAccNo.Text + "'  where plant_code='" + pcode + "' and agent_id='" + ddl_agentid.Text + "' and producer_code='" + ddl_producerdetails.Text + "'   ";
            SqlCommand cmd = new SqlCommand(updatestr, con);
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Records Updated SuccessFully";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Visible = true;
        }
        catch(Exception Ex)
        {

            lblmsg.Text = Ex.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Visible = true;
        }
    }


    public void getalldetails()
    {

       // ddl_agentid.Items.Clear();

        ddl_Ifsc.Items.Clear();
        ddl_BankName.Items.Clear();
        droproducer.Items.Clear();
        try
        {
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select  Payment_mode,Milk_Nature,Producer_Code,Producer_Name,phone_Number,Cartage_Amt,SplBonus_Amt,Bank_name,Ifsc_code,branch_name,Agent_AccountNo   from DPUPRODUCERMASTER  where plant_code='" + pcode + "' and    agent_id='" + ddl_agentid.Text + "' and Producer_Code='" + ddl_producerdetails.Text + "' ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    string paytype = dr["Payment_mode"].ToString();

                    if ((paytype == "Cash")  || (paytype == "cash"))
                    {

                        Chk_BankAccount.Checked = false;
                        hide();

                    }

                    else
                    {
                        Chk_BankAccount.Checked = true;
                        show();

                    }
                    if (paytype == "")
                    {

                        Chk_BankAccount.Checked = false;
                        hide();

                    }




                    string getnaure =   dr["Milk_Nature"].ToString();

                    if ((getnaure == "cow") || (getnaure == "Cow"))
                    {
                        rbcow.Checked = true;
                        rbbuff.Checked = false;
                    }
                    else
                    {
                        rbcow.Checked = false;
                        rbbuff.Checked = true;
                    }

                    droproducer.Items.Add(dr["Producer_Code"].ToString());
                    txt_magentname.Text = dr["Producer_Name"].ToString();
                    txt_magentmobno.Text=dr["phone_Number"].ToString();
                    txt_mcartage1.Text=dr["Cartage_Amt"].ToString();


                    if (txt_mcartage1.Text == string.Empty)
                    {
                        txt_mcartage1.Text = "0.0";
                    }
                    txt_mcartage.Text = dr["SplBonus_Amt"].ToString();

                    if (txt_mcartage.Text == string.Empty)
                    {
                        txt_mcartage.Text = "0.0";
                    }

                    
                
                    ddl_BankName.Items.Add(dr["Bank_name"].ToString());
                    ddl_Ifsc.Items.Add(dr["Ifsc_code"].ToString());
                    txt_AgentAccNo.Text = dr["Agent_AccountNo"].ToString();
                    ddl_branchname.Items.Add(dr["Branch_name"].ToString());
                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
                         
      ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
      pcode = ddl_Plantcode.SelectedItem.Value;
      getdpuagentlist();
      getdpuproducerlist();
 
    }





    public void getdpuagentlist()
    {
        ddl_agentid.Items.Clear();
        try
        {
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select distinct(agent_id) as agent_id   from DPUPRODUCERMASTER  where plant_code='" + pcode + "' ";
            
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    ddl_agentid.Items.Add(dr["agent_id"].ToString());

                }

            }
            else
            {
             

            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
           
        }
    }


    public void getbanklist()
    {
      //  ddl_producerdetails.Items.Clear();
       
               
        try
        {
            ddl_BankName.Items.Clear();
            ddl_branchname.Items.Clear();
            ddl_Ifsc.Items.Clear();
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select distinct(bank_Name) as bank_Name,bank_id  from BANK_MASTER  where plant_code='" + pcode + "'    order by bank_Name asc  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    string getbankname = dr["bank_Name"].ToString();

                    string[] assaignbankname = getbankname.Split('_');
                 //   ddl_BankName.Items.Add(assaignbankname[1]);

                    ddl_BankName.Items.Add(dr["bank_Name"].ToString());
                    ddl_bankid.Items.Add(dr["bank_id"].ToString() + "_" + dr["bank_Name"].ToString());



                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }


    public void getbankbranchlist()
    {
        //  ddl_producerdetails.Items.Clear();
       
      


        try
        {
            ddl_branchname.Items.Clear();
            ddl_Ifsc.Items.Clear();

            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select distinct(Branch_name)  as Branch_name  from BANK_MASTER  where plant_code='" + pcode + "'    order by Branch_name asc  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {


                    ddl_branchname.Items.Add(dr["Branch_name"].ToString());
                   

                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }

    public void getbankifsclist()
    {
        //  ddl_producerdetails.Items.Clear();

      



        try
        {
            ddl_Ifsc.Items.Clear();
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select distinct(Ifsc_code) as Ifsc_code  from BANK_MASTER  where plant_code='" + pcode + "'    order by Ifsc_code asc  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                   

                    ddl_Ifsc.Items.Add(dr["Ifsc_code"].ToString());

                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }



    public void getdpuproducerlist()
    {
        
        try
        {
            ddl_producerdetails.Items.Clear();
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select distinct(producer_code) as  producer_code  from DPUPRODUCERMASTER  where plant_code='" + pcode + "' and  agent_id='" + ddl_agentid.Text + "' order by  producer_code asc  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    ddl_producerdetails.Items.Add(dr["producer_code"].ToString());

                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }





    protected void ddl_agentid_SelectedIndexChanged(object sender, EventArgs e)
    {
        getdpuproducerlist();
       
    }
    protected void rbcow_CheckedChanged(object sender, EventArgs e)
    {

        if (rbcow.Checked == true)
        {

            rbcow.Checked = true;
            rbbuff.Checked = false;

        }



    }
    protected void rbbuff_CheckedChanged(object sender, EventArgs e)
    {
        if (rbbuff.Checked == true)
        {

            rbcow.Checked = false;
            rbbuff.Checked = true;

        }
    }
    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        // getbankbranchlist();
         ddl_bankid.SelectedIndex = ddl_BankName.SelectedIndex;
         getbankid = ddl_bankid.SelectedItem.Value;
         string[] getsplit = getbankid.Split('_');
         getbankid = getsplit[0];
        getbankID1();
        getbankbranchlist1();
        getbankifsclist1();
       
    }
    protected void ddl_branchname_SelectedIndexChanged(object sender, EventArgs e)
    {
        getbankID1();
        getbankifsclist1();
    }
    protected void ddl_Ifsc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Chk_BankAccount_CheckedChanged(object sender, EventArgs e)
    {

        if (Chk_BankAccount.Checked == true)
        {
            show();
        }
        else
        {
            hide();



           
        }


    }

    public void hide()
    {
        try
        {


            Label19.Visible = false;
            Label20.Visible = false;
            Label21.Visible = false;
            Label22.Visible = false;
            ddl_BankName.Visible = false;
            ddl_branchname.Visible = false;
            ddl_Ifsc.Visible = false;
            txt_AgentAccNo.Visible = false;

            ddl_BankName.Items.Clear();
            ddl_branchname.Items.Clear();
            ddl_Ifsc.Items.Clear();
            txt_AgentAccNo.Text = "";

        }
        catch (Exception Ex)
        {


        }
    }

    public void show()
    {

        Label19.Visible = true;
        Label20.Visible = true;
        Label21.Visible = true;
        Label22.Visible = true;
        ddl_BankName.Visible = true;
        ddl_branchname.Visible = true;
        ddl_Ifsc.Visible = true;
        txt_AgentAccNo.Visible = true;
    }



    protected void Button5_Click(object sender, EventArgs e)
    {

        getbanklist();
        getbankbranchlist();
        getbankifsclist();

        update.Visible = true;

        

    }


    //public void getbanklist1()
    //{
    //    //  ddl_producerdetails.Items.Clear();
    //    ddl_BankName.Items.Clear();
    //    ddl_branchname.Items.Clear();
    //    ddl_Ifsc.Items.Clear();

    //    try
    //    {
    //        string str = "";
    //        SqlConnection con = new SqlConnection(connStr1);
    //        con.Open();

    //        str = "select distinct(bank_Name) as bank_Name,bank_id  from BANK_MASTER  where plant_code='" + pcode + "'    order by bank_Name asc  ";

    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {

    //            while (dr.Read())
    //            {
    //                string getbankname = dr["bank_Name"].ToString();

    //                string[] assaignbankname = getbankname.Split('_');
    //                ddl_BankName.Items.Add(assaignbankname[1]);



    //            }

    //        }
    //        else
    //        {


    //        }
    //    }
    //    catch (Exception ee)
    //    {

    //        lblmsg.Text = "Please Check Data";
    //        lblmsg.ForeColor = System.Drawing.Color.Red;
    //        lblmsg.Visible = true;

    //    }
    //}


    public void getbankbranchlist1()
    {
        //  ddl_producerdetails.Items.Clear();

        ddl_branchname.Items.Clear();
        ddl_Ifsc.Items.Clear();



        try
        {
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select distinct(Branch_name)  as Branch_name  from BANK_MASTER  where plant_code='" + pcode + "'    and  Bank_id='" + globalbank + "'  order by Branch_name asc  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {


                    ddl_branchname.Items.Add(dr["Branch_name"].ToString());


                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = ee.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }



    public void getbankID1()
    {
        //  ddl_producerdetails.Items.Clear();

      //  ddl_branchname.Items.Clear();
       


        try
        {
            ddl_Ifsc.Items.Clear();

            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();
            string[] getb = getbankid.Split('_');
            str = "select distinct(BANK_ID)  as BANK_ID,BANK_NAME  from Bank_Details  where   BANK_ID='" + getb[0] + "'  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {


                  globalbank =  dr["BANK_ID"].ToString().TrimStart();
                   

                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = ee.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }



    public void getbankifsclist1()
    {
        //  ddl_producerdetails.Items.Clear();

       



        try
        {
            ddl_Ifsc.Items.Clear();
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select distinct(Ifsc_code) as Ifsc_code  from BANK_MASTER  where plant_code='" + pcode + "'  and   bank_id='" + globalbank + "'  and  Branch_name='" + ddl_branchname.Text + "'   order by Ifsc_code asc  ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {


                    ddl_Ifsc.Items.Add(dr["Ifsc_code"].ToString());

                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = ee.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }



    protected void btn_delete_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
      
        Panel2.Visible = false;
    }
    protected void update_Click(object sender, EventArgs e)
    {
        getbankID1();
        getbankifsclist1();
        updatefunction();
        update.Visible = false;
        getalldetails1();
    }

    public void getalldetails1()
    {

        // ddl_agentid.Items.Clear();

     
        
        try
        {
            ddl_Ifsc.Items.Clear();
            ddl_BankName.Items.Clear();
            droproducer.Items.Clear();
            ddl_branchname.Items.Clear();
            txt_magentname.Text = "";
            txt_magentmobno.Text = "";
            txt_mcartage1.Text = "0.0";
            txt_mcartage.Text = "0.0";
            txt_AgentAccNo.Text = "";
            string str = "";
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            str = "select  Payment_mode,Milk_Nature,Producer_Code,Producer_Name,phone_Number,Cartage_Amt,SplBonus_Amt,Bank_Id,Ifsc_code,Agent_AccountNo,Bank_name,Branch_name   from DPUPRODUCERMASTER  where plant_code='" + pcode + "' and  agent_id='" + ddl_agentid.Text + "' and Producer_Code='" + ddl_producerdetails.Text + "' ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    string paytype = dr["Payment_mode"].ToString();

                    if ((paytype == "Cash") || (paytype == "cash"))
                    {

                        Chk_BankAccount.Checked = false;
                        hide();

                    }

                    else
                    {
                        Chk_BankAccount.Checked = true;
                        show();

                    }

                    if (paytype == "")
                    {

                        Chk_BankAccount.Checked = false;
                        hide();

                    }


                    string getnaure = dr["Milk_Nature"].ToString();

                    if ((getnaure == "cow") || (getnaure == "Cow"))
                    {
                        rbcow.Checked = true;
                        rbbuff.Checked = false;
                    }
                    else
                    {
                        rbcow.Checked = false;
                        rbbuff.Checked = true;
                    }

                    droproducer.Items.Add(dr["Producer_Code"].ToString());
                    txt_magentname.Text = dr["Producer_Name"].ToString();
                    txt_magentmobno.Text = dr["phone_Number"].ToString();
                    txt_mcartage1.Text = dr["Cartage_Amt"].ToString();

                    if (txt_mcartage1.Text == string.Empty)
                    {
                        txt_mcartage1.Text = "0.0";
                    }
                    txt_mcartage.Text = dr["SplBonus_Amt"].ToString();

                    if (txt_mcartage.Text == string.Empty)
                    {
                        txt_mcartage.Text = "0.0";
                    }

                    ddl_BankName.Items.Add(dr["Bank_name"].ToString());
                    ddl_Ifsc.Items.Add(dr["Ifsc_code"].ToString());
                    txt_AgentAccNo.Text = dr["Agent_AccountNo"].ToString();
                    ddl_branchname.Items.Add(dr["Branch_name"].ToString());

                }

            }
            else
            {


            }
        }
        catch (Exception ee)
        {

            lblmsg.Text = ee.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("DpuAmoutAllotement.aspx");
    }
}