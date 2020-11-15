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




public partial class AgentDetails : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    DataSet ds = new DataSet();
    SqlDataAdapter da=new SqlDataAdapter();
  //  SqlDataReader DR = new SqlDataReader();
    SqlCommand CMD=new SqlCommand();
    
    DateTime tdt = new DateTime();
    string strsql = string.Empty;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    int returnvalue;
    string plantname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string rid;
    string path;
    string path1;
    string path2;
    string path3;
    string path4;
    string path5;
    string path6;

    int id=0;
    int id1=0;
    int id2=0;
    int id3=0;
    int id4=0;
    int id5=0;
    int validateval;
    int status;
    byte[] imgbyte = null;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        Button1.Attributes.Add("onclick", "javascript:return validationCheck()");



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
             //   managmobNo = Session["managmobNo"].ToString();
               // LoadPlantcode();

                if (roleid < program.Guser_PermissionId)
                {
                    loadsingleplant();
                    GetRouteID();
                }
                else
                {
                    LoadPlantcode();
                }



                //  pcode = ddl_Plantcode.SelectedItem.Value;
                //  gridview();
                
                GridView1.Visible = true;

                  dtm = System.DateTime.Now;
                txt_joining.Text = dtm.ToShortDateString();
                txt_joining.Text = dtm.ToString("dd/MM/yyyy");
                txt_wedd.Text = dtm.ToString("dd/MM/yyyy");
                txt_dob.Text = dtm.ToString("dd/MM/yyyy");

                 lbl_label.Visible = false;
           

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
            lblmessage.Visible = false;
            lblmessage1.Visible=false;
            lblmessage2.Visible = false;
            lblmessage3.Visible = false;
            lblmessage4.Visible = false;
            lblmessage5.Visible = false;
            Image1.Visible = false;
            Image2.Visible = false;
            Image3.Visible = false;
            Image4.Visible = false;
            Image5.Visible = false;
            Image6.Visible = false;
            

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
             //   GetRouteID();
                
           //     gridview();

       


                GridView1.Visible = true;
                lbl_label.Visible = false;

                lblmessage.Visible = false;
                lblmessage1.Visible = false;
                lblmessage2.Visible = false;
                lblmessage3.Visible = false;
                lblmessage4.Visible = false;
                lblmessage5.Visible = false;
              
                //  getgrid();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }
    }


    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
           
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
              

                string sqlstr = "select top 1  *    from  AgentInformation WHERE plant_code='"+pcode+"'  order by tid desc ";
                SqlCommand COM = new SqlCommand(sqlstr, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.HeaderRow.ForeColor = System.Drawing.Color.White;
                    GridView1.HeaderRow.BackColor = System.Drawing.Color.Brown;

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






    private void BindGridData()
    {
        
    }





    protected void btnUploadImage_Click(object sender, EventArgs e)
    {




     //   checkimageuploads();




        //if ((id==1 && id2 == 2 && id3 == 3 && id4 == 4 )|| id5 == 5)
        //{






            getagentid1();





            try
            {

                       validate();


                        if (validateval == 1)
                        {

                            if (status == 1)
                            {

                                DateTime dt1 = new DateTime();
                                DateTime dt2 = new DateTime();
                                DateTime dt3 = new DateTime();

                                dt1 = DateTime.ParseExact(txt_joining.Text, "dd/MM/yyyy", null);
                                string d1 = dt1.ToString("MM/dd/yyyy");

                                dt2 = DateTime.ParseExact(txt_dob.Text, "dd/MM/yyyy", null);
                                string d2 = dt2.ToString("MM/dd/yyyy");

                                dt3 = DateTime.ParseExact(txt_wedd.Text, "dd/MM/yyyy", null);
                                string d3 = dt3.ToString("MM/dd/yyyy");

                              //path = Server.MapPath("image/") + fileUpd.FileName;

                                path1 = Image1.ImageUrl;
                                path2 = Image2.ImageUrl;
                                path3 = Image3.ImageUrl;
                                path4 = Image4.ImageUrl;
                                path5 = Image5.ImageUrl;

                                path = Image6.ImageUrl;


                                SqlConnection con = new SqlConnection(connStr);
                                //Open The Connection  
                                con.Open();
                                SqlCommand cmd = new SqlCommand("INSERT INTO AgentInformation(Plant_code,agent_name,agent_id,Plant_Name,Route_Id,Route_name,Address,AadharNo,JoiningDate,RationCartNo,VoterId,PanCardNo,AccountNo,BankName,BankAccNo,IfscNo,BranchName,Mobile,GuardianName,image,Aadharimage,Rationimage,voterimage,pancardimage,Accountimage,Email,Dob,MarriageDate) VALUES(@Plant_code,@agent_name,@agent_id,@Plant_Name,@Route_Id,@Route_name,@Address,@AadharNo,@JoiningDate,@RationCartNo,@VoterId,@PanCardNo,@AccountNo,@BankName,@BankAccNo,@IfscNo,@BranchName,@Mobile,@GuardianName,@Image,@Aadharimage,@Rationimage,@voterimage,@pancardimage,@Accountimage,@Email,@Dob,@MarriageDate)", con);
                                cmd.Parameters.Add("@plant_code", pcode);
                                cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                                cmd.Parameters.Add("@agent_name", txt_AgentName.Text);
                                cmd.Parameters.Add("@plant_name", ddl_Plantname.Text);
                                cmd.Parameters.Add("@Route_Id", ddl_routeid.Text);
                                cmd.Parameters.Add("@Route_name", ddl_Routename.Text);
                                cmd.Parameters.Add("@Address", txt_address.Text);
                                cmd.Parameters.Add("@AadharNo", txt_aadthar.Text);
                                cmd.Parameters.Add("@JoiningDate", d1);
                                cmd.Parameters.Add("@RationCartNo", txt_rationno.Text);
                                cmd.Parameters.Add("@VoterId", txt_voteid.Text);
                                cmd.Parameters.Add("@PanCardNo", txt_pancard.Text);
                                cmd.Parameters.Add("@AccountNo", txt_accountno.Text);
                                cmd.Parameters.Add("@BankName", txt_bank.Text);
                                cmd.Parameters.Add("@BankAccNo", txt_accountno.Text);
                                cmd.Parameters.Add("@IfscNo", txt_ifscno.Text);
                                cmd.Parameters.Add("@BranchName", txt_branchname.Text);
                                cmd.Parameters.Add("@Mobile", txt_mobile.Text);
                                cmd.Parameters.Add("@GuardianName", txt_guardian.Text);
                                cmd.Parameters.Add("@Email", Mailid.Text);
                                cmd.Parameters.Add("@Dob", d2);
                                cmd.Parameters.Add("@MarriageDate", d3);

                                //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                              //  cmd.Parameters.AddWithValue("@Image", imgbyte);
                                 cmd.Parameters.AddWithValue("@Image",path); 
                                 cmd.Parameters.AddWithValue("@Aadharimage", path1);
                                cmd.Parameters.AddWithValue("@Rationimage", path2);
                                cmd.Parameters.AddWithValue("@voterimage", path3);
                                cmd.Parameters.AddWithValue("@pancardimage", path4);
                                cmd.Parameters.AddWithValue("@Accountimage", path5);

                                //cmd.Parameters.AddWithValue("@Image", ImgPreview.ImageUrl);


                                //int img = fileUpd.PostedFile.ContentLength;

                                //byte[] msdata = new byte[img];
                                //fileUpd.PostedFile.InputStream.Read(msdata, 0, img);

                                //cmd.Parameters.AddWithValue("@image", msdata);


                                cmd.ExecuteNonQuery();
                                con.Close();
                           
                                //      WebMsgBox.Show("inserted Successfully");
                                lbl_label.Visible = true;
                                lbl_label.ForeColor = System.Drawing.Color.Green;
                                lbl_label.Text = "Inserted Successfully";
                             
                                clear();
                                gridview();
                                GridView1.HeaderRow.ForeColor = System.Drawing.Color.White;
                                GridView1.HeaderRow.BackColor = System.Drawing.Color.Brown;

                                if (program.Guser_role < program.Guser_PermissionId)
                                {
                                    loadsingleplant();
                                    GetRouteID();
                                }
                                else
                                {
                                    LoadPlantcode();
                                }


                                //pcode = ddl_Plantcode.SelectedItem.Value;
                                //GetRouteID();
                                //  ddl_routeid.SelectedIndex =  ddl_Routename.SelectedIndex;
                                rid = ddl_routeid.Text;
                                //rid = ddl_routeid.SelectedIndex;
                                getagentid();
                                getagentname();
                                //if (program.Guser_role < program.Guser_PermissionId)
                                //{
                                //    loadsingleplant();
                                //    getagentid();
                                //}
                                //else
                                //{
                                //    LoadPlantcode();
                                //}
                            }

                            if (status == 0)
                            {

                                DateTime dt1 = new DateTime();
                                DateTime dt2 = new DateTime();
                                DateTime dt3 = new DateTime();

                                dt1 = DateTime.ParseExact(txt_joining.Text, "dd/MM/yyyy", null);
                                string d1 = dt1.ToString("MM/dd/yyyy");

                                dt2 = DateTime.ParseExact(txt_dob.Text, "dd/MM/yyyy", null);
                                string d2 = dt2.ToString("MM/dd/yyyy");

                                dt3 = DateTime.ParseExact(txt_wedd.Text, "dd/MM/yyyy", null);
                                string d3 = dt3.ToString("MM/dd/yyyy");

                                path1 = Image1.ImageUrl;
                                path2 = Image2.ImageUrl;
                                path3 = Image3.ImageUrl;
                                path4 = Image4.ImageUrl;
                                path5 = Image5.ImageUrl;

                                path = Image6.ImageUrl;
                                //path = Server.MapPath("image/") + fileUpd.FileName;

                                //path1 = Server.MapPath("image/") + FileUpload1.FileName;
                                //path2 = Server.MapPath("image/") + FileUpload2.FileName;
                                //path3 = Server.MapPath("image/") + FileUpload3.FileName;
                                //path4 = Server.MapPath("image/") + FileUpload4.FileName;


                                SqlConnection con = new SqlConnection(connStr);
                                //Open The Connection  
                                con.Open();
                                //SqlCommand cmd = new SqlCommand("update  AgentInformation set  Plant_code,agent_name,agent_id,Plant_Name,Route_Id,Route_name,Address,AadharNo,JoiningDate,RationCartNo,VoterId,PanCardNo,AccountNo,BankName,BankAccNo,IfscNo,BranchName,Mobile,GuardianName,image) VALUES(@Plant_code,@agent_name,@agent_id,@Plant_Name,@Route_Id,@Route_name,@Address,@AadharNo,@JoiningDate,@RationCartNo,@VoterId,@PanCardNo,@AccountNo,@BankName,@BankAccNo,@IfscNo,@BranchName,@Mobile,@GuardianName,@Image)", con);
                                //cmd.Parameters.Add("@plant_code", pcode);
                                //cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                                //cmd.Parameters.Add("@agent_name", txt_AgentName.Text);
                                //cmd.Parameters.Add("@plant_name", ddl_Plantname.Text);
                                //cmd.Parameters.Add("@Route_Id", ddl_routeid.Text);
                                //cmd.Parameters.Add("@Route_name", ddl_Routename.Text);
                                //cmd.Parameters.Add("@Address", txt_address.Text);
                                //cmd.Parameters.Add("@AadharNo", txt_aadthar.Text);
                                //cmd.Parameters.Add("@JoiningDate", d1);
                                //cmd.Parameters.Add("@RationCartNo", txt_rationno.Text);
                                //cmd.Parameters.Add("@VoterId", txt_voteid.Text);
                                //cmd.Parameters.Add("@PanCardNo", txt_pancard.Text);
                                //cmd.Parameters.Add("@AccountNo", txt_AgentName.Text);
                                //cmd.Parameters.Add("@BankName", txt_bank.Text);
                                //cmd.Parameters.Add("@BankAccNo", txt_accountno.Text);
                                //cmd.Parameters.Add("@IfscNo", txt_ifscno.Text);
                                //cmd.Parameters.Add("@BranchName", txt_branchname.Text);
                                //cmd.Parameters.Add("@Mobile", txt_mobile.Text);
                                //cmd.Parameters.Add("@GuardianName", txt_guardian.Text);
                                ////   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                                //cmd.Parameters.AddWithValue("@Image", path);

                                //string update = "update  AgentInformation set Address='" + txt_address.Text + "',AadharNo='" + txt_aadthar.Text + "',JoiningDate='" + d1 + "',RationCartNo='" + txt_rationno.Text + "',VoterId='" + txt_voteid.Text + "',PanCardNo='" + txt_pancard.Text + "',GuardianName='" + txt_guardian.Text + "',image='" + path + "' ";

                                string update = "update AgentInformation set agent_id='" + ddl_Agentid.Text + "',Plant_Name='" + ddl_Plantname.Text + "',Route_Id='" + ddl_routeid.Text + "',Route_name='" + ddl_Routename.Text + "',Address='" + txt_address.Text + "',AadharNo='" + txt_aadthar.Text + "',JoiningDate='" + d1 + "',RationCartNo='" + txt_rationno.Text + "',VoterId='" + txt_voteid.Text + "',PanCardNo='" + txt_pancard.Text + "',AccountNo='" + txt_AgentName.Text + "',BankName='" + txt_bank.Text + "',BankAccNo='" + txt_accountno.Text + "',IfscNo='" + txt_ifscno.Text + "',BranchName='" + txt_branchname.Text + "',Mobile='" + txt_mobile.Text + "',GuardianName='" + txt_guardian.Text + "',image='" + path + "',Aadharimage='" + path1 + "',Rationimage='" + path2 + "',voterimage='" + path3 + "',pancardimage='" + path4 + "',Accountimage='" + path5 + "',Email='" + Mailid.Text + "',Dob='" + d2 + "',MarriageDate='" + d3 + "'  where  plant_code='" + pcode + "' and agent_id='" + ddl_Agentid.Text + "' ";

                                SqlCommand cmd = new SqlCommand(update, con);
                                cmd.ExecuteNonQuery();
                                con.Close();
                              
                                //      WebMsgBox.Show("inserted Successfully");
                                lbl_label.Visible = true;
                                lbl_label.ForeColor = System.Drawing.Color.Green;
                                lbl_label.Text = "updated Successfully";
                                clear();
                                gridview();


                                if (program.Guser_role < program.Guser_PermissionId)
                                {
                                    loadsingleplant();
                                    GetRouteID();
                                }
                                else
                                {
                                    LoadPlantcode();
                                }
                              //  GetRouteID();
                                //  ddl_routeid.SelectedIndex =  ddl_Routename.SelectedIndex;
                                rid = ddl_routeid.Text;
                                //rid = ddl_routeid.SelectedIndex;
                                getagentid();
                                getagentname();


                            }


                        }
                        if (validateval == 0)
                        {

                            //WebMsgBox.Show("Please Fill Required Filed");
                            lbl_label.Visible = true;
                            lbl_label.ForeColor = System.Drawing.Color.Red;
                            lbl_label.Text = "Please Fill Required Filed";


                        }



                    }
                    
            catch
            {

                //   WebMsgBox.Show("Please Check Your  Data");
                lbl_label.Visible = true;
                lbl_label.ForeColor = System.Drawing.Color.Red;
                lbl_label.Text = "Please Check Your  Data";


            }



        

        //else
        //{


        //    if (id == 0)
        //    {

        //        imageoversize.Text = "Please Check Your Image";
        //        imageoversize.ForeColor = System.Drawing.Color.Red;
        //    }


        //    if (id2 == 0 && id2 == 0)
        //    {

        //        imageoversize.Text = "Please Check Your Adthar Proof SiZe"  +    "Check Your Image";
        //        imageoversize.ForeColor = System.Drawing.Color.Red;
        //    }

        //    if (id == 0 &&   id2 == 0 && id3==0)
        //    {

        //        imageoversize.Text = "Please Check Your Adthar Proof SiZe" + "Please Check Your Ration Proof SiZe" + "Check Your Image";
        //        imageoversize.ForeColor = System.Drawing.Color.Red;
        //    }


        //    if (id == 0 && id2 == 0 && id3 == 0 && id4 == 0)
        //    {

        //        imageoversize.Text = "Please Check Your Adthar Proof SiZe" + "Ration Proof SiZe" + "Ration Proof SiZe" + "VoterId Size" + "Check Your Image";
        //        imageoversize.ForeColor = System.Drawing.Color.Red;
        //    }

        //    if (id == 0 &&  id2 == 0 && id3 == 0 && id4 == 0 && id5 == 5)
        //    {

        //        imageoversize.Text = "Please Check Your Adthar Proof SiZe" + "Ration Proof SiZe" + "Ration Proof SiZe" + "VoterId Size" + "pancard Size" + "Check Your Image";
        //        imageoversize.ForeColor = System.Drawing.Color.Red;
        //    }




        













    }

       protected void ddl_routename_SelectedIndexChanged(object sender, EventArgs e)
       {
           
           

       }
       protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
       {

       }
       protected void ddl_Plantname_SelectedIndexChanged1(object sender, EventArgs e)
       {
           ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
           pcode = ddl_Plantcode.SelectedItem.Value;
        //  getagentid();

           GetRouteID();
       //  ddl_routeid.SelectedIndex =  ddl_Routename.SelectedIndex;
         rid = ddl_routeid.Text;
         //rid = ddl_routeid.SelectedIndex;
           getagentid();
      
           getagentname();
          // getagentid();
      //     gridview();
       }
       protected void txt_FromDate_TextChanged(object sender, EventArgs e)
       {

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
               string str = "select distinct agent_id from agent_master where plant_code='" + pcode + "'  and route_id='" + rid + "'   order by Agent_id  asc ";
               SqlCommand cmd = new SqlCommand(str, con);
               SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
              {
               while (dr.Read())
               {

                   ddl_Agentid.Items.Add(dr["agent_id"].ToString());
                  // txt_AgentName.Text = dr["Agent_Name"].ToString();


               }
                }
                    else
                    {

                        lbl_label.Visible = true;
                        lbl_label.ForeColor = System.Drawing.Color.Red;
                        lbl_label.Text = "No Agents";


                    }
                


           }

           catch
           {

               lbl_label.Visible = true;
               lbl_label.ForeColor = System.Drawing.Color.Red;
               lbl_label.Text = "Please Check";

           }



       }


       public void getagentid1()
       {

           try
           {
             //  ddl_Agentid.Items.Clear();
               //     txt_AgentName.Text = "";
               string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
               SqlConnection con = new SqlConnection(connStr);
               con.Open();
               //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
               string str = "select * from AgentInformation where plant_code='" + pcode + "'   and  agent_id='"+ddl_Agentid.Text+"' ";
               SqlCommand cmd = new SqlCommand(str, con);
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {

                  //     ddl_Agentid.Items.Add(dr["agent_id"].ToString());
                       // txt_AgentName.Text = dr["Agent_Name"].ToString();
                       status = 0;
                      
                   }
               }
               else
               {

                   //lbl_label.Visible = true;
                   //lbl_label.ForeColor = System.Drawing.Color.Red;
                   //lbl_label.Text = "No Agents";
                       status =1;
               }



           }

           catch
           {

               lbl_label.Visible = true;
               lbl_label.ForeColor = System.Drawing.Color.Red;
               lbl_label.Text = "Please Check";

           }



       }


       public void getagentname()
       {

           try
           {
              
               txt_AgentName.Text = "";
               txt_bank.Text = "";
               txt_accountno.Text = "";
               txt_ifscno.Text = "";
               txt_branchname.Text = "";
               string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
               SqlConnection con = new SqlConnection(connStr);
               con.Open();
               //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            //   string str = "select distinct agent_id,agent_name from agent_master where plant_code='" + pcode + "'  and route_id='" + rid + "' and agent_id='"+ddl_Agentid.Text+"' order by Agent_id  asc ";
               string str = "select top 1 a.Agent_Name,a.Agent_AccountNo as Agent_AccountNo ,a.Ifsc_code as Ifsc_code,b.Bank_Name  as Bank_Name ,b.Branch_Name  as  Branch_Name,a.phone_number  from (select top 1 agent_id,agent_name,Ifsc_code,Agent_AccountNo,Bank_Id,phone_number from agent_master where plant_code='" + pcode + "'  and route_id='" + rid + "' and agent_id='" + ddl_Agentid.Text + "') as a left join(  select Bank_ID,Bank_Name,Branch_Name,Ifsc_code   from BANK_MASTER  ) as b on a.Bank_Id=b.Bank_ID and a.Ifsc_code=b.Ifsc_code";

               SqlCommand cmd = new SqlCommand(str, con);
               SqlDataReader dr = cmd.ExecuteReader();
               while (dr.Read())
               {

                 
                   txt_AgentName.Text = dr["Agent_Name"].ToString();
                   txt_bank.Text=dr["Bank_Name"].ToString();
                   txt_accountno.Text=dr["Agent_AccountNo"].ToString();
                   txt_ifscno.Text=dr["Ifsc_code"].ToString();
                   txt_branchname.Text = dr["Branch_Name"].ToString();
                   txt_mobile.Text = dr["phone_number"].ToString();
                   if (txt_mobile.Text!=string.Empty)
                   {

                         txt_mobile.Text = dr["phone_number"].ToString();

                   }
                   else
                   {
                        txt_mobile.Text = "0";


                   }

               }


           }

           catch
           {

               lbl_label.Visible = true;
               lbl_label.ForeColor = System.Drawing.Color.Red;
               lbl_label.Text = "Please Check";

           }



       }


       public void GetRouteID()
       {

           try
           {
              ddl_Routename.Items.Clear();
               ddl_routeid.Items.Clear();
               string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
               SqlConnection con = new SqlConnection(connStr);
               con.Open();
               //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
               string str = "select distinct route_id,route_name  from Route_Master where plant_code='" + pcode + "'  order by route_id  asc ";
               SqlCommand cmd = new SqlCommand(str, con);
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.HasRows)
               {

                   while (dr.Read())
                   {




                       //  id = dr["route_id"].ToString();

                       ddl_routeid.Items.Add(dr["route_id"].ToString());
                       ddl_Routename.Items.Add(dr["route_id"].ToString() + "_" + dr["route_name"].ToString());



                   }
               }
               else
               {

                 //  WebMsgBox.Show("NO Agent");
                   lbl_label.Visible = true;
                   lbl_label.ForeColor = System.Drawing.Color.Red;
                   lbl_label.Text = "NO Agent";

               }


           }

           catch
           {

             //  WebMsgBox.Show("Please Check Your Data");


               lbl_label.Visible = true;
               lbl_label.ForeColor = System.Drawing.Color.Red;
               lbl_label.Text = "Please Check Your Data";
           }



       }
       protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
       {
           pcode = ddl_Plantcode.SelectedItem.Value;
        //   GetRouteID();
           rid = ddl_routeid.SelectedItem.Value;
           getagentname();
       //    getagentid1();

       }






       protected void ddl_Routename_SelectedIndexChanged(object sender, EventArgs e)
       {

           ddl_routeid.SelectedIndex = ddl_Routename.SelectedIndex;
           rid = ddl_routeid.SelectedItem.Value;
           getagentid();
           getagentname();
       }

       protected void txt_FromDate_TextChanged1(object sender, EventArgs e)
       {

       }
       protected void txt_mgrname15_TextChanged(object sender, EventArgs e)
       {

       }
       protected void btnSubmit_Click1(object sender, EventArgs e)
       {

       }
       protected void btnSubmit_Click2(object sender, EventArgs e)
       {

       }
       protected void Button1_Click(object sender, EventArgs e)
       {
           
           
           
        
       }


       public void clear()
       {
           

           txt_address.Text = "";
           txt_aadthar.Text = "";
           dtm = System.DateTime.Now;
           txt_joining.Text = dtm.ToShortDateString();
           txt_joining.Text = dtm.ToString("dd/MM/yyyy");

          
           ddl_Agentid.Items.Clear();
           txt_AgentName.Text = "";
           txt_rationno.Text = "";
           txt_voteid.Text = "";
           txt_pancard.Text = "";
           txt_AgentName.Text = "";
           txt_bank.Text = "";
           txt_accountno.Text = "";
           txt_ifscno.Text = "";
           txt_branchname.Text = "";
           txt_mobile.Text = "";
           txt_guardian.Text = "";
           //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
           ImgPreview.ImageUrl = null;
           
           Image1.ImageUrl = null;
           Image2.ImageUrl = null;
           Image3.ImageUrl = null;
           Image4.ImageUrl = null;
           Image5.ImageUrl = null;
           Image6.ImageUrl = null;
       }

       private void loadsingleplant()
       {
           try
           {
               SqlDataReader dr = null;
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

       public void validate()
       {

           if ((ddl_Plantname.Text != string.Empty) && (ddl_Routename.Text != string.Empty) && (ddl_Agentid.Text != string.Empty) && (txt_AgentName.Text != string.Empty) && (txt_voteid.Text != string.Empty) && (txt_bank.Text != string.Empty) && (txt_accountno.Text != string.Empty) && (txt_ifscno.Text != string.Empty) && (txt_branchname.Text != string.Empty) && (txt_ifscno.Text != string.Empty) && (txt_branchname.Text != string.Empty) && (txt_ifscno.Text != string.Empty) && (txt_mobile.Text != string.Empty) && (path!= string.Empty))
           {

               validateval = 1;

           }
           else
           {

               validateval = 0;


           }
       }
       protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
       {
           GridView1.PageIndex = e.NewPageIndex;
           gridview();
       }
       protected void LinkButton1_Click(object sender, EventArgs e)
       {
         
       }
       protected void Button2_Click(object sender, EventArgs e)
       {




         




           try
           {
               validate();


               if (validateval == 1)
               {

                   if (status == 0)
                   {


                       DateTime dt1 = new DateTime();
                       DateTime dt2 = new DateTime();
                       DateTime dt3 = new DateTime();
                       dt1 = DateTime.ParseExact(txt_joining.Text, "dd/MM/yyyy", null);
                       string d1 = dt1.ToString("MM/dd/yyyy");







                       //path1 = Image1.ImageUrl;
                       //path2 = Image2.ImageUrl;
                       //path3 = Image3.ImageUrl;
                       //path4 = Image4.ImageUrl;
                       //path5 = Image4.ImageUrl;

                       //path = Image6.ImageUrl;
                     



                       SqlConnection con = new SqlConnection(connStr);
                       //Open The Connection  
                       con.Open();
                       SqlCommand cmd = new SqlCommand("INSERT INTO AgentInformation(Plant_code,agent_name,agent_id,Plant_Name,Route_Id,Route_name,Address,AadharNo,JoiningDate,RationCartNo,VoterId,PanCardNo,AccountNo,BankName,BankAccNo,IfscNo,BranchName,Mobile,GuardianName,image,image1,image2,image3,image4) VALUES(@Plant_code,@agent_name,@agent_id,@Plant_Name,@Route_Id,@Route_name,@Address,@AadharNo,@JoiningDate,@RationCartNo,@VoterId,@PanCardNo,@AccountNo,@BankName,@BankAccNo,@IfscNo,@BranchName,@Mobile,@GuardianName,@Image,@Aadharimage,@Rationimage,@voterimage,@pancardimage)", con);
                       cmd.Parameters.Add("@plant_code", pcode);
                       cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                       cmd.Parameters.Add("@agent_name", txt_AgentName.Text);
                       cmd.Parameters.Add("@plant_name", ddl_Plantname.Text);
                       cmd.Parameters.Add("@Route_Id", ddl_routeid.Text);
                       cmd.Parameters.Add("@Route_name", ddl_Routename.Text);
                       cmd.Parameters.Add("@Address", txt_address.Text);
                       cmd.Parameters.Add("@AadharNo", txt_aadthar.Text);
                       cmd.Parameters.Add("@JoiningDate", d1);
                       cmd.Parameters.Add("@RationCartNo", txt_rationno.Text);
                       cmd.Parameters.Add("@VoterId", txt_voteid.Text);
                       cmd.Parameters.Add("@PanCardNo", txt_pancard.Text);
                       cmd.Parameters.Add("@AccountNo", txt_AgentName.Text);
                       cmd.Parameters.Add("@BankName", txt_bank.Text);
                       cmd.Parameters.Add("@BankAccNo", txt_accountno.Text);
                       cmd.Parameters.Add("@IfscNo", txt_ifscno.Text);
                       cmd.Parameters.Add("@BranchName", txt_branchname.Text);
                       cmd.Parameters.Add("@Mobile", txt_mobile.Text);
                       cmd.Parameters.Add("@GuardianName", txt_guardian.Text);
                       //   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                       cmd.Parameters.AddWithValue("@Image", path);

                    
                       cmd.Parameters.AddWithValue("@Aadharimage", path2);
                       cmd.Parameters.AddWithValue("@Rationimage", path3);
                       cmd.Parameters.AddWithValue("@voterimage", path4);
                       cmd.Parameters.AddWithValue("@pancardimage", path5);


                       cmd.ExecuteNonQuery();
                       con.Close();
                       clear();
                       gridview();
                       //      WebMsgBox.Show("inserted Successfully");
                       lbl_label.Visible = true;
                       lbl_label.ForeColor = System.Drawing.Color.Green;
                       lbl_label.Text = "Inserted Successfully";
                      

                 

                   }


                   if (status ==1)
                   {


                       DateTime dt1 = new DateTime();
                       DateTime dt2 = new DateTime();
                       dt1 = DateTime.ParseExact(txt_joining.Text, "dd/MM/yyyy", null);
                       string d1 = dt1.ToString("MM/dd/yyyy");


                       //path = Server.MapPath("image/") + fileUpd.FileName;
                       //path1 = Server.MapPath("image/") + FileUpload1.FileName;
                       //path2 = Server.MapPath("image/") + FileUpload2.FileName;
                       //path3 = Server.MapPath("image/") + FileUpload3.FileName;
                       //path4 = Server.MapPath("image/") + FileUpload4.FileName;
  



                       SqlConnection con = new SqlConnection(connStr);
                       //Open The Connection  
                       con.Open();
                       //SqlCommand cmd = new SqlCommand("update  AgentInformation set  Plant_code,agent_name,agent_id,Plant_Name,Route_Id,Route_name,Address,AadharNo,JoiningDate,RationCartNo,VoterId,PanCardNo,AccountNo,BankName,BankAccNo,IfscNo,BranchName,Mobile,GuardianName,image) VALUES(@Plant_code,@agent_name,@agent_id,@Plant_Name,@Route_Id,@Route_name,@Address,@AadharNo,@JoiningDate,@RationCartNo,@VoterId,@PanCardNo,@AccountNo,@BankName,@BankAccNo,@IfscNo,@BranchName,@Mobile,@GuardianName,@Image)", con);
                       //cmd.Parameters.Add("@plant_code", pcode);
                       //cmd.Parameters.Add("@agent_id", ddl_Agentid.Text);
                       //cmd.Parameters.Add("@agent_name", txt_AgentName.Text);
                       //cmd.Parameters.Add("@plant_name", ddl_Plantname.Text);
                       //cmd.Parameters.Add("@Route_Id", ddl_routeid.Text);
                       //cmd.Parameters.Add("@Route_name", ddl_Routename.Text);
                       //cmd.Parameters.Add("@Address", txt_address.Text);
                       //cmd.Parameters.Add("@AadharNo", txt_aadthar.Text);
                       //cmd.Parameters.Add("@JoiningDate", d1);
                       //cmd.Parameters.Add("@RationCartNo", txt_rationno.Text);
                       //cmd.Parameters.Add("@VoterId", txt_voteid.Text);
                       //cmd.Parameters.Add("@PanCardNo", txt_pancard.Text);
                       //cmd.Parameters.Add("@AccountNo", txt_AgentName.Text);
                       //cmd.Parameters.Add("@BankName", txt_bank.Text);
                       //cmd.Parameters.Add("@BankAccNo", txt_accountno.Text);
                       //cmd.Parameters.Add("@IfscNo", txt_ifscno.Text);
                       //cmd.Parameters.Add("@BranchName", txt_branchname.Text);
                       //cmd.Parameters.Add("@Mobile", txt_mobile.Text);
                       //cmd.Parameters.Add("@GuardianName", txt_guardian.Text);
                       ////   cmd.Parameters.Add("@Image", "image/" + ImgPreview.ImageUrl);
                       //cmd.Parameters.AddWithValue("@Image", path);



                     
                       //string update = "update  AgentInformation set Address='" + txt_address.Text + "',AadharNo='" + txt_aadthar.Text + "',JoiningDate='" + d1 + "',RationCartNo='" + txt_rationno.Text + "',VoterId='" + txt_voteid.Text + "',PanCardNo='" + txt_pancard.Text + "',GuardianName='" + txt_guardian.Text + "',image='" + path + "' ";

                       string update = "update AgentInformation set agent_id='" + ddl_Agentid.Text + "',Plant_Name='" + ddl_Plantname.Text + "',Route_Id='" + ddl_routeid.Text + "',Route_name='" + ddl_Routename.Text + "',Address='" + txt_address.Text + "',AadharNo='" + txt_aadthar.Text + "',JoiningDate='" + d1 + "',RationCartNo='" + txt_rationno.Text + "',VoterId='" + txt_voteid.Text + "',PanCardNo='" + txt_pancard.Text + "',AccountNo='" + txt_AgentName.Text + "',BankName='" + txt_bank.Text + "',BankAccNo='" + txt_accountno.Text + "',IfscNo='" + txt_ifscno.Text + "',BranchName='" + txt_branchname.Text + "',Mobile='" + txt_mobile.Text + "',GuardianName='" + txt_guardian.Text + "',image='" + path + "',Aadharimage='" + path1 + "',Rationimage='" + path2 + "',voterimage='" + path3 + "',pancardimage='" + path4+ "'  where  plant_code='" + pcode + "' and agent_id='" + ddl_Agentid.Text + "' ";     

                       SqlCommand cmd = new SqlCommand(update, con);
                       cmd.ExecuteNonQuery();
                       con.Close();
                    
                       //      WebMsgBox.Show("inserted Successfully");
                       lbl_label.Visible = true;
                       lbl_label.ForeColor = System.Drawing.Color.Green;
                       lbl_label.Text = "updated Successfully";
                       clear();

                       gridview();


                   }
                  

               }
               if (validateval == 0)
               {

                   //WebMsgBox.Show("Please Fill Required Filed");
                   lbl_label.Visible = true;
                   lbl_label.ForeColor = System.Drawing.Color.Red;
                   lbl_label.Text = "Please Fill Required Filed";


               }

           }

           catch
           {

               //   WebMsgBox.Show("Please Check Your  Data");
               lbl_label.Visible = true;
               lbl_label.ForeColor = System.Drawing.Color.Red;
               lbl_label.Text = "Please Check Your  Data";


           }

           
       }


       public void checkimageuploads()
       {




           //Getting path
            if (fileUpd.HasFile)
           {

               if (fileUpd.PostedFile.ContentLength < 71680)
           {

               path = Server.MapPath("image/") + fileUpd.FileName;
               //Saving image to path
               fileUpd.SaveAs(path);
               //Generating Image preview by reading image from path
               //  ImgPreview.ImageUrl = "image/" + fileUpd.FileName;

               path = "image/" + fileUpd.FileName;

               id = 1;


           }
            }


            if (FileUpload1.HasFile)
            {


                if (FileUpload1.PostedFile.ContentLength < 71680)
                {

                    path1 = Server.MapPath("image/") + FileUpload1.FileName;
                    //Saving image to path
                    FileUpload1.SaveAs(path1);
                    //Generating Image preview by reading image from path
                    //  ImgPreview.ImageUrl = "image/" + fileUpd.FileName;

                    path1 = "image/" + FileUpload1.FileName;

                    id2 = 2;


                }
            }

           if (FileUpload2.HasFile)
           {

               if (FileUpload2.PostedFile.ContentLength < 71680)
               {

                   path2 = Server.MapPath("image/") + FileUpload2.FileName;
                   //Saving image to path
                   FileUpload2.SaveAs(path2);
                   //Generating Image preview by reading image from path
                   //  ImgPreview.ImageUrl = "image/" + fileUpd.FileName;

                   path2 = "image/" + FileUpload2.FileName;

                   id3 = 3;


               }
           }

           if (FileUpload3.HasFile)
           {

               if (FileUpload3.PostedFile.ContentLength < 71680)
               {

                   path3 = Server.MapPath("image/") + FileUpload3.FileName;
                   //Saving image to path
                   FileUpload3.SaveAs(path3);
                   //Generating Image preview by reading image from path
                   //  ImgPreview.ImageUrl = "image/" + fileUpd.FileName;

                   path3 = "image/" + FileUpload3.FileName;

                   id4 = 4;


               }
           }

           if (FileUpload4.HasFile)
           {

               if (FileUpload4.PostedFile.ContentLength < 71680)
               {

                   path4 = Server.MapPath("image/") + FileUpload4.FileName;
                   //Saving image to path
                   FileUpload4.SaveAs(path4);
                   //Generating Image preview by reading image from path
                   //  ImgPreview.ImageUrl = "image/" + fileUpd.FileName;

                   path4 = "image/" + FileUpload4.FileName;

                   id5 = 5;


               }
           }
       }

       protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
       {


           if (e.Row.RowType == DataControlRowType.Header)
           {

               GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
               TableCell HeaderCell2 = new TableCell();
               HeaderCell2.Text = "PlantName:" + ddl_Plantname.Text + "    --------   " + "Route Name: " + ddl_Routename.Text;
               HeaderCell2.ColumnSpan = 15;
               HeaderCell2.ForeColor = System.Drawing.Color.White;
               HeaderCell2.BackColor = System.Drawing.Color.Brown;
              HeaderRow.Cells.Add(HeaderCell2);
               GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
               HeaderCell2.Font.Bold = true;
               HeaderRow.HorizontalAlign = HorizontalAlign.Center;
               ddl_Routename.Items.Clear();
           }

       }


       protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
       {
           if (FileUpload1.FileBytes.Length > 1024)
           {
               args.IsValid = false;
           }
           else
           {
               args.IsValid = true;
           }
       }




       protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
       {
           if (FileUpload2.FileBytes.Length > 10240)
           {
               args.IsValid = false;
           }
           else
           {
               args.IsValid = true;
           }
       }

       protected void Button2_Click1(object sender, EventArgs e)
       {

       }
       protected void Button2_Click2(object sender, EventArgs e)
       {

       }
       protected void upload1_Click(object sender, EventArgs e)
       {

           if (FileUpload1.HasFile)
           {
               //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



               string ext = System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);
               if ((FileUpload1.FileBytes.Length < 71680) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
               {



                   string path = Server.MapPath("image/") + FileUpload1.FileName;
                   FileUpload1.SaveAs(Server.MapPath("image/") + FileUpload1.FileName);

                 //  FileUpload1.SaveAs(path);
                   Image3.ImageUrl = "image/" + FileUpload1.FileName;



                   lblmessage.Text = "File uploaded successfully.";

                   lblmessage.ForeColor = System.Drawing.Color.Green;





               }
               else
               {




                   Image3.ImageUrl = "";

                   lblmessage.Text = "File greater than 70 KB or File Format";
                   lblmessage.ForeColor = System.Drawing.Color.Red;
                  



               }



           }
           else
           {
               lblmessage.Text = "Please select file.";
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



               //    path1 = Server.MapPath("image/") + FileUpload1.FileName;


                   if (File.Exists(Server.MapPath("image/") + FileUpload1.FileName))
                   {





                       lblmessage.Visible = true;
                       lblmessage.Text = "Already Same File Name Available";
                       lblmessage.ForeColor = System.Drawing.Color.Red;



                   }

                   else
                   {

                       path1 = Server.MapPath("image/") + FileUpload1.FileName;

                       FileUpload1.SaveAs(Server.MapPath("image/") + FileUpload1.FileName);

                       //FileUpload1.SaveAs(path1);
                       Image1.ImageUrl = "image/" + FileUpload1.FileName;



                       lblmessage.Text = "File uploaded successfully.";

                       lblmessage.ForeColor = System.Drawing.Color.Green;
                       lblmessage.Visible = false;


                       Image1.Visible = true;


                   }
               }
               else
               {




                   Image1.ImageUrl = "";

                   lblmessage.Text = "File greater than 700 KB or File Format";
                   lblmessage.ForeColor = System.Drawing.Color.Red;

                   lblmessage.Visible = true;


               }



           }
           else
           {
               lblmessage.Visible = true;
               lblmessage.Text = "Please select file.";
               lblmessage.ForeColor = System.Drawing.Color.Red;
           }


       }
       protected void uploadration_Click(object sender, EventArgs e)
       {

           if (FileUpload2.HasFile)
           {
               //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



               string ext = System.IO.Path.GetExtension(this.FileUpload2.PostedFile.FileName);
               if ((FileUpload2.FileBytes.Length < 716800) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
               {


                   if (File.Exists(Server.MapPath("image/") + FileUpload2.FileName))
                   {





                       lblmessage1.Visible = true;
                       lblmessage1.Text = "Already Same File Name Available";
                       lblmessage1.ForeColor = System.Drawing.Color.Red;



                   }

                   else
                   {

                       path2 = Server.MapPath("image/") + FileUpload2.FileName;
                       FileUpload2.SaveAs(Server.MapPath("image/") + FileUpload2.FileName);

                       //FileUpload2.SaveAs(path2);
                       Image2.ImageUrl = "image/" + FileUpload2.FileName;



                       Image2.Visible = true;

                       //lblmessage1.Text = "File uploaded successfully.";

                       //lblmessage1.ForeColor = System.Drawing.Color.Green;

                       lblmessage1.Visible = false;
                   }


               }
               else
               {




                   Image2.ImageUrl = "";

                   lblmessage1.Text = "File greater than 700 KB or File Format";
                   lblmessage1.ForeColor = System.Drawing.Color.Red;

                   lblmessage1.Visible = true;


               }



           }
           else
           {
               lblmessage1.Visible = true;
               lblmessage1.Text = "Please select file.";
               lblmessage1.ForeColor = System.Drawing.Color.Red;
           }


       }
       protected void uploadvoter_Click(object sender, EventArgs e)
       {


           if (FileUpload3.HasFile)
           {
               //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



               string ext = System.IO.Path.GetExtension(this.FileUpload3.PostedFile.FileName);
               if ((FileUpload3.FileBytes.Length < 716800) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
               {



                   path3 = Server.MapPath("image/") + FileUpload3.FileName;

                   if (File.Exists(Server.MapPath("image/") + FileUpload3.FileName))
                   {





                       lblmessage2.Visible = true;
                       lblmessage2.Text = "Already Same File Name Available";
                       lblmessage2.ForeColor = System.Drawing.Color.Red;





                   }


                   else
                   {



                       FileUpload3.SaveAs(Server.MapPath("image/") + FileUpload3.FileName);

                   //    FileUpload3.SaveAs(path3);
                       Image3.ImageUrl = "image/" + FileUpload3.FileName;

                       Image3.Visible = true;


                       //lblmessage2.Text = "File uploaded successfully.";

                       //lblmessage2.ForeColor = System.Drawing.Color.Green;

                       lblmessage2.Visible = false;
                   }


               }
               else
               {




                   Image3.ImageUrl = "";
                   lblmessage2.Text = "File greater than 700 KB or File Format";
                   lblmessage2.ForeColor = System.Drawing.Color.Red;
                   lblmessage2.Visible = true;



               }



           }
           else
           {
               lblmessage2.Visible = true;
               lblmessage2.Text = "Please select file.";
               lblmessage2.ForeColor = System.Drawing.Color.Red;
           }


       }
       protected void uploadpancard_Click(object sender, EventArgs e)
       {

           if (FileUpload4.HasFile)
           {
               //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



               string ext = System.IO.Path.GetExtension(this.FileUpload4.PostedFile.FileName);
               if ((FileUpload4.FileBytes.Length < 716800) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
               {



                    path4 = Server.MapPath("image/") + FileUpload4.FileName;

                    if (File.Exists(Server.MapPath("image/") + FileUpload4.FileName))
                    {





                        lblmessage3.Visible = true;
                        lblmessage3.Text = "Already Same File Name Available";
                        lblmessage3.ForeColor = System.Drawing.Color.Red;





                    }
                    else
                    {


                        FileUpload4.SaveAs(Server.MapPath("image/") + FileUpload4.FileName);

                        FileUpload4.SaveAs(path4);
                        Image4.ImageUrl = "image/" + FileUpload4.FileName;


                        lblmessage3.Visible = false;
                        //lblmessage3.Text = "File uploaded successfully.";

                        //lblmessage3.ForeColor = System.Drawing.Color.Green;


                        Image4.Visible = true;
                    }
             



               }
               else
               {



                   lblmessage3.Visible = true;
                   Image4.ImageUrl = "";
                   lblmessage3.Text = "File greater than 70 KB or File Format";
                   lblmessage3.ForeColor = System.Drawing.Color.Red;




               }



           }
           else
           {
               lblmessage3.Visible = true;
               lblmessage3.Text = "Please select file.";
               lblmessage3.ForeColor = System.Drawing.Color.Red;
           }

       }
       protected void uploadaccount_Click(object sender, EventArgs e)
       {
           if (FileUpload5.HasFile)
           {
               //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



               string ext = System.IO.Path.GetExtension(this.FileUpload5.PostedFile.FileName);
               if ((FileUpload5.FileBytes.Length < 716800) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
               {



                    path6 = Server.MapPath("image/") + FileUpload5.FileName;



                    if (File.Exists(Server.MapPath("image/") + FileUpload5.FileName))
                    {

                        lblmessage4.Visible = true;
                        lblmessage4.Text = "Already Same File Name Available";
                        lblmessage4.ForeColor = System.Drawing.Color.Red;


                    }
                    else
                    {

                        FileUpload5.SaveAs(Server.MapPath("image/") + FileUpload5.FileName);

                        FileUpload5.SaveAs(path6);
                        Image5.ImageUrl = "image/" + FileUpload5.FileName;



                        Image5.Visible = true;

                        //lblmessage4.Text = "File uploaded successfully.";

                        //lblmessage4.ForeColor = System.Drawing.Color.Green;

                        lblmessage4.Visible = false;
                    }



               }
               else
               {




                   Image5.ImageUrl = "";
                   lblmessage4.Visible = true;
                   lblmessage4.Text = "File greater than 700 KB or File Format";
                   lblmessage4.ForeColor = System.Drawing.Color.Red;




               }



           }
           else
           {
               lblmessage4.Visible = true;
               lblmessage4.Text = "Please select file.";
               lblmessage4.ForeColor = System.Drawing.Color.Red;
           }
       }
       protected void uploadimage_Click(object sender, EventArgs e)
       {



           if (fileUpd.HasFile)
           {
               //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



               string ext = System.IO.Path.GetExtension(this.fileUpd.PostedFile.FileName);
               if ((fileUpd.FileBytes.Length < 716800) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
               {



                   path = Server.MapPath("image/") + fileUpd.FileName;

                   if (File.Exists(Server.MapPath("image/") + fileUpd.FileName))
                   {

                       lblmessage5.Visible = true;
                       lblmessage5.Text = "Already Same File Name Available";
                       lblmessage5.ForeColor = System.Drawing.Color.Red;


                   }

                   else
                   {


                       fileUpd.SaveAs(Server.MapPath("image/") + fileUpd.FileName);
                     //  fileUpd.SaveAs(path);
                       Image6.ImageUrl = "image/" + fileUpd.FileName;


                       Image6.Visible = true;
                       //lblmessage5.Text = "File uploaded successfully.";

                       //lblmessage5.ForeColor = System.Drawing.Color.Green;

                       lblmessage5.Visible = false;






                   }









               }
               else
               {




                   Image6.ImageUrl = "";
                   lblmessage5.Visible = true;
                   lblmessage5.Text = "File greater than 700 KB or File Format";
                   lblmessage5.ForeColor = System.Drawing.Color.Red;




               }



           }
           else
           {
               lblmessage5.Visible = true;
               lblmessage5.Text = "Please select file.";
               lblmessage5.ForeColor = System.Drawing.Color.Red;
           }

       }







       //protected void uploadimage_Click(object sender, EventArgs e)
       //{



       //    if (fileUpd.HasFile)
       //    {
       //        //  string filename = System.IO.Path.GetFileName(FileUpload3.FileName);



       //        HttpPostedFile file = fileUpd.PostedFile;
       //        imgbyte = new byte[file.ContentLength];
       //        file.InputStream.Read(imgbyte, 0, file.ContentLength); 



       //        string ext = System.IO.Path.GetExtension(this.fileUpd.PostedFile.FileName);
       //        if ((fileUpd.FileBytes.Length < 716800) && (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg"))
       //        {

       //            fileUpd.SaveAs(Server.MapPath("image/") + fileUpd.FileName);
                               
       //                          Image6.ImageUrl = "image/" + fileUpd.FileName;


       //                           Image6.Visible = true;
       //                        lblmessage5.Text = "File uploaded successfully.";

       //                        lblmessage5.ForeColor = System.Drawing.Color.Green;

       //                          lblmessage5.Visible = false;
                                                        

       //            imgbyte = new byte[file.ContentLength];
       //        }


       //            //path = Server.MapPath("image/") + fileUpd.FileName;

       //            //if (File.Exists(Server.MapPath("image/") + fileUpd.FileName))
       //            //{

       //            //    lblmessage5.Visible = true;
       //            //    lblmessage5.Text = "Already Same File Name Available";
       //            //    lblmessage5.ForeColor = System.Drawing.Color.Red;


       //            //}

       //            //else
       //            //{


       //            //    fileUpd.SaveAs(Server.MapPath("image/") + fileUpd.FileName);
       //            //    fileUpd.SaveAs(path);
       //            //    Image6.ImageUrl = "image/" + fileUpd.FileName;


       //            //    Image6.Visible = true;
       //            //    //lblmessage5.Text = "File uploaded successfully.";

       //            //    //lblmessage5.ForeColor = System.Drawing.Color.Green;

       //            //    lblmessage5.Visible = false;






       //            //}









            
       //        else
       //        {




       //            Image6.ImageUrl = "";
       //            lblmessage5.Visible = true;
       //            lblmessage5.Text = "File greater than 700 KB or File Format";
       //            lblmessage5.ForeColor = System.Drawing.Color.Red;




       //        }



       //    }
       //    else
       //    {
       //        lblmessage5.Visible = true;
       //        lblmessage5.Text = "Please select file.";
       //        lblmessage5.ForeColor = System.Drawing.Color.Red;
       //    }

       //}
       protected void txt_ifscno_TextChanged(object sender, EventArgs e)
       {

       }
}





       

