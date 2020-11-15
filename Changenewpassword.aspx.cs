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
using System.Drawing;
using System.Text;
using System.IO;

public partial class Changenewpassword : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    string strpassword;
    string getpapass;
    string newpass1;
    string newpass2;
    string getid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {

                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                dtm = System.DateTime.Now;
                txt_date.Text = dtm.ToShortDateString();

                txt_date.Text = dtm.ToString("dd/MM/yyyy");




            }

            else
            {
                //dtm = System.DateTime.Now;
                //// dti = System.DateTime.Now;
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyy");

                //  pcode = ddl_Plantcode.SelectedItem.Value;
                //  LoadPlantcode();
                //txt_oldpass.Text = getpapass;
                //getdetailsfromuser();
             //   getdetailsfromuser();

            }

        }
        else
        {


            //    pcode = ddl_Plantcode.SelectedItem.Value;
            //  LoadPlantcode();
            //   ddl_agentcode.Text = getrouteusingagent;
            //    gridview();
            //strpassword = Encryptdata(txt_oldpass.Text);
            //getdetailsfromuser();
          //  txt_oldpass.Text = getpapass;
            //strpassword = Encryptdata(txt_oldpass.Text);
          //  getdetailsfromuser();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if ((txt_uname.Text != string.Empty) && (txt_oldpass.Text != string.Empty) && (txt_newpass.Text != string.Empty) && (txt_confirmpass.Text != string.Empty))
        {

      
        strpassword = Encryptdata(txt_oldpass.Text);
        txt_oldpass.Text = strpassword.ToString();
         getdetailsfromuser();

                  
        
            newpass1 = Encryptdata(txt_newpass.Text);
            txt_newpass.Text = newpass1.ToString();

            newpass2 = Encryptdata(txt_confirmpass.Text);
            txt_confirmpass.Text = newpass1.ToString();
            if ((txt_newpass.Text.Length > 5) && (txt_confirmpass.Text.Length > 5))
            {
                if (newpass1.ToString() == newpass2.ToString())
                {

                    //DateTime dt1 = new DateTime();

                    //dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);


                    //string d1 = dt1.ToString("MM/dd/yyyy");

                    SqlConnection conn = new SqlConnection(connStr);

                    SqlCommand cmd = new SqlCommand();


                    if (getid =="")
                    {

                        cmd.CommandText = "update UserIDInfo  set password='" + newpass2 + "',IsCreatedDate='" +  System.DateTime.Now + "' where first_name='" + getid + "'";
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        WebMsgBox.Show("New Password Updated Successfully");
                        conn.Close();

                        clear();
                    }
                    else
                    {
                        clear();

                        WebMsgBox.Show("Please Check Old Password");
                        conn.Close();
                    }

                }

                else
                {

                    WebMsgBox.Show("New Password Mismatch");

                }
            }
            else
            {
                WebMsgBox.Show("Passwword Lenth Minimum 6 Letters");

                 
            }
        }

        else
        {

            WebMsgBox.Show("Please Check Data");
            txt_oldpass.Focus();

        }


    }
    protected void txt_oldpass_TextChanged(object sender, EventArgs e)
    {

      //  strpassword = Encryptdata(txt_oldpass.Text);


        strpassword = Encryptdata(txt_oldpass.Text);
        txt_oldpass.Text = strpassword.ToString();
                   getdetailsfromuser();

                 
        
      
    }



    private string Encryptdata(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }




    public void getpassencry()
    {

       

    }
    public void getdetailsfromuser()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from procurementimport where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            //string str = "  select top 1  *   from   Add_User where password='" + strpassword + "' and Users_ID='" + txt_uname.Text + "'";   UserIDInfo

            string str = "  select  password,First_Name   from   UserIDInfo where password='" + strpassword + "' and First_Name='" + txt_uname.Text + "'"; 
            SqlCommand cmd = new SqlCommand(str, con);
           
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                while (dr.Read())
                {

                    getpapass = dr["password"].ToString();

                //    getid = Convert.ToInt32(dr["UserId"].ToString());

                    getid = dr["First_Name"].ToString();
                    //txt_oldpass.Text = strpassword;
                    ////   txt_oldpass.Text

               //     txt_oldpass.Text = strpassword;

                    //txt_oldpass.Text = "******";
                    //txt_newpass.Focus();




                }

             
            }
            else

        {

            WebMsgBox.Show("Please Check your Password");
             txt_oldpass.Text = "";
            txt_oldpass.Focus();

        }


        }

        catch
        {

            WebMsgBox.Show("PLEASE CHECK");

        }

        

    }
    protected void txt_newpass_TextChanged(object sender, EventArgs e)
    {
        //newpass1 = Encryptdata(txt_newpass.Text);
        //txt_newpass.Text = newpass1.ToString();
        //txt_newpass.Text = "***********"; 
    }

    public void getnewpass()
    {



    }
    protected void txt_confirmpass_TextChanged(object sender, EventArgs e)
    {
        //newpass2 = Encryptdata(txt_confirmpass.Text);
        //txt_confirmpass.Text = newpass2.ToString();
        //txt_confirmpass.Text = "***********"; 
        
    }
    protected void txt_oldpass_TextChanged1(object sender, EventArgs e)
    {

    }
    public void clear()
    {
        txt_uname.Text = "";
        txt_oldpass.Text = "";
        txt_newpass.Text = "";
        txt_confirmpass.Text="";

    }
}