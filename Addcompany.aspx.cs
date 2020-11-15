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

public partial class Addcompany : System.Web.UI.Page
{


    DbHelper DBclass = new DbHelper();
    BOLuser userBO = new BOLuser();
    BLLuser userBL = new BLLuser();
    SqlConnection con;
    string strpassword;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
            GenerateID();
            txt_fromdate.Text = DateTime.Now.ToShortDateString();
            txtfirstname.Focus();
       
       
    }
    private void setBO()
    {
        userBO.CompanyID = Convert.ToInt32(txtcompanyid.Text);
        userBO.Table_ID = 0;
        userBO.UserID = txtcompanyuserid.Text;
        userBO.Password = strpassword;
        userBO.First_Name = txtfirstname.Text;
        userBO.Last_Name = txtlastname.Text;
        userBO.Hints = "EMPTY";
        userBO.Phone_Number = TxtTeleOff.Text;
        userBO.Mobile_Number = TxtMobile.Text;
        userBO.Email_ID = txtEmailID.Text;
        userBO.Fax = txtFax.Text;
        userBO.Organization_Name = "EMPTY";
        userBO.Address = TxtAddress.Text;
        userBO.City = txtcity.Text;
        userBO.Country = DdlistCountry.Text;
        userBO.NoofUser = 0;
        //userBO.Pin_Code = TextBox13.Text;
        userBO.Registration_Date = Convert.ToDateTime(txt_fromdate.Text);
        //userBO.Conform_Password = TextBox15.Text;
        userBO.Roles = 1;

    }
    public void GenerateID()
    {

        try
        {

            int mx = 0;
            string sqlstr = "select max(Company_ID) from Add_User";
            mx = Convert.ToInt32(DBclass.ExecuteScalarstr(sqlstr));
            //int iid = Convert.ToInt32(mx);
            mx = mx + 1;
            txtcompanyid.Text = mx.ToString();
            txtcompanyuserid.Text = mx.ToString() + "CMPUSER";
        }
        catch
        {
            txtcompanyid.Text = "1001";
            txtcompanyuserid.Text = "1001" + "CMPUSER";
        }

    }
    public void clearr()
    {

        txtfirstname.Text = " ";
        txtlastname.Text = "";
        txtcity.Text = "";
        txtEmailID.Text = "";
        txtFax.Text = " ";
        TxtMobile.Text = " ";
        TxtTeleOff.Text = "";
       
        TxtAddress.Text = "";
        txtPassword.Text = " ";
        txtcompanyuserid.Text = " ";

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
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
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        strpassword = Encryptdata(txtPassword.Text);
        setBO();
        userBL.insertuser(userBO);
        using (con = DBclass.GetConnection())
        {
            string qry = "insert into UserIDInfo(User_LoginId,User_Email,Password,Roles) values ('" + txtcompanyuserid.Text + "','" + txtEmailID.Text + "','" + strpassword + "','Admin') ";
            SqlCommand SqlCom = new SqlCommand(qry, con);
            SqlCom.ExecuteNonQuery();


        }
        clearr();
        GenerateID();
        WebMsgBox.Show("Inserted SucessFully");
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {

    }
}
