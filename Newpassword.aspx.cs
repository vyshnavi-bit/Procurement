using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;


public partial class Newpassword : System.Web.UI.Page
{
    DbHelper DB = new DbHelper();
 
   
    public int ccode;
    public int pcode;
    public static int roleid;
    string uid;
    SqlConnection con = new SqlConnection();
    int status;
    int getuserid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                uid = Session["userid"].ToString();
                //if (Request.QueryString["id"] != null)
                roleid = Convert.ToInt32(Session["Role"].ToString());
                //    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                //else
                //    Response.Write("");
                //uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);

            }
        }
    }
    protected void btnchange_Click(object sender, EventArgs e)
    {
        GETUPDATE();
        update();
    }


    public void GETUPDATE()
    {
        
        string STR;
        con=DB.GetConnection();
        STR = "SELECT UserId    FROM newusers WHERE username    COLLATE Latin1_general_CS_AS ='" + txt_user.Text + "' AND  password  COLLATE Latin1_general_CS_AS ='" + txtcurrent.Text + "'";
        SqlCommand cmd = new SqlCommand(STR, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            status = 1;

            while(dr.Read())
            {

                getuserid =Convert.ToInt16(dr["UserId"].ToString());

            }

        }
        else
        {
            status = 0;

        }


    }




    public void update()
    {
        string message;
        if (status == 1)
        {
           
            try
            {
                con = DB.GetConnection();
              
                string stt = "update  newusers set Password='" + txtconfirm.Text + "',addedDate='" + System.DateTime.Now + "'  where   UserId='" + getuserid + "' and username='" + txt_user.Text + "'";
                SqlCommand cmd = new SqlCommand(stt, con);
                cmd.ExecuteNonQuery();
                message = "Update Successfully";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                txt_user.Text = "";
            }
            catch
            {
                message = "Please Check Your Data";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);



            }

        }


    }


}