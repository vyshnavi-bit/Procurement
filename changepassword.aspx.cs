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
using System.Text;
public partial class changepassword : System.Web.UI.Page
{
    DbHelper DBclass = new DbHelper();
    string decryptpassword;
    string strpassword;
    string USer_LoginID;
    string dd;
    protected void Page_Load(object sender, EventArgs e)
    {
        
         
    }
    private void decryptdata()
    {
        SqlDataReader dr;
        
        string sqlstr = "SELECT * from UserIDInfo where User_LoginId='" + txtUserName.Text + "'";
        dr = DBclass.GetDatareader(sqlstr);
        if (dr.HasRows)
        {
            dr.Read();
            dd = dr["Password"].ToString().Trim();
            
        }
    }
    private string Decryptdata(string encryptpwd)
    {
        try
        {
            string decryptpwd = string.Empty;

            UTF8Encoding encodepwd = new UTF8Encoding();
            System.Text.Decoder utf8Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        catch
        {
            return null;
        }

    }
    private string Encryptdata(string password)
    {
        try
        {
            string strmsg = string.Empty;
            byte[] encode = new
            byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        catch
        {
            return null;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        decryptdata();
        decryptpassword = dd;
        string cc;
        cc = Decryptdata(decryptpassword);
        strpassword = Encryptdata(TxtNewPassword.Text);
        if (Page.IsValid == true)
        {
            if (TxtOldPassword.Text == "")
            {
                MyClass.MyAlert(this, "Enter old password.", "");
                return;
            }
            if (txtUserName.Text == "")
            {
                MyClass.MyAlert(this, "Enter UserName.", "");
                return;
            }
            if (TxtNewPassword.Text == "")
            {
                MyClass.MyAlert(this, "Enter new password.", "");
                return;
            }
            if (TxtNewPassword.Text != TxtConfirmPassword.Text)
            {
                MyClass.MyAlert(this, "New and confirm Password are not match.","");
                return;
            }

            if (cc == TxtOldPassword.Text)
            {
                //check for old password
                //check login
                SqlConnection con = new SqlConnection();
                using (con = DBclass.GetConnection())
                {

                    SqlDataReader dr;
                    string sqlstr = ("select * FROM UserIDInfo where User_LoginId = '" + txtUserName.Text + "' and Password='" + decryptpassword + "'");
                    dr = DBclass.GetDatareader(sqlstr);
                    if (dr.HasRows)
                    {
                        dr.Read();
                        string sqlst = "Update UserIDInfo Set Password='" + strpassword + "' where User_LoginId='" + txtUserName.Text + "' and Password='" + decryptpassword + "'";
                        SqlCommand cmd = new SqlCommand(sqlst, con);
                        cmd.ExecuteNonQuery();
                        this.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "<script language=\"javaScript\">" + "alert('Password changed successfully!');" + "window.location.href='LoginDefault.aspx';" + "<" + "/script>");

                    }
                    else
                    {
                        MyClass.MyAlert(this, "Login Failed, check your email id and password.", "");
                        return;
                    }
                }

            }
            else
            {
                WebMsgBox.Show("Please Enter your Correct Password!");
                this.Response.Redirect("changepassword.aspx");
            }
        }
    }
}
