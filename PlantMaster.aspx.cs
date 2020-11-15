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
using System.Text.RegularExpressions;
using System.Text;

public partial class PlantMaster : System.Web.UI.Page
{

    string uid;
    string centreid;
    int cmpcode, pcode;
    DbHelper DBclass = new DbHelper();
    DALPlantMaster plantmasterDA = new DALPlantMaster();
    BOLPlantMaster plantmasterBO = new BOLPlantMaster();
    BLLPlantMaster plantmasterBL = new BLLPlantMaster();
    BLLCompanymaster companymasterBL = new BLLCompanymaster();
    BOLCompanyMaster companymasterBO = new BOLCompanyMaster();
    DALCompanymster companymasterDA = new DALCompanymster();
    BOLuser userBO = new BOLuser();
    BLLuser usersBLL = new BLLuser();
    DALuser userDA = new DALuser();
    BOLLoinfo loginfoBO = new BOLLoinfo();
    DALLoginfo loginfoDA = new DALLoginfo();
    BLLLoginfo loginfoBL = new BLLLoginfo();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            uscMsgBox1.MsgBoxAnswered += MessageAnswered;
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                
                //uid = Session["User_ID"].ToString();
                //if (Request.QueryString["id"] != null)

                //    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                //else
                //    Response.Write("");
                 
              //  pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                loadCompanycode();
                loadplantcode();
                
                txt_Plantname.Focus();

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

                //uid = Session["User_ID"].ToString();
                //if (Request.QueryString["id"] != null)

                //    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                //else
                //    Response.Write("");

                
            //   pcode = Convert.ToInt32(Session["Plant_Code"]);
               cmpcode = Convert.ToInt32(Session["Company_code"]);
                
               txt_Plantname.Focus();
                

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
      //loadbankid();
    }

    
    protected void Button1_Click(object sender, EventArgs e)
    
    {
        int result = UserStatus();
        if (result == 1)
        {

        }
        else if (result == 0)
        {

            Session["User_LoginId"] = centreid;

            uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
        }
        
    }
   
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_companycode.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    public void setBO()
    {
          if (txt_Plantname.Text != string.Empty)
            plantmasterBO.Plantname = txt_Plantname.Text.Trim();
        
        if (txt_plantphoneno.Text != string.Empty)
            plantmasterBO.plantphoneno = txt_plantphoneno.Text.Trim();
        if (txt_plantaddress.Text != string.Empty)
            plantmasterBO.Plantaddress = txt_plantaddress.Text.Trim();
        if (txt_companyname.Text != string.Empty)
            companymasterBO.Companyname = txt_companyname.Text.Trim();
        if (txt_companyaddress.Text != string.Empty)
            companymasterBO.Companyaddress = txt_companyaddress.Text.Trim();
        if (txt_companyphoneno.Text != string.Empty)
            companymasterBO.Companyphoneno = txt_companyphoneno.Text.Trim();
        if (txt_companytinno.Text != string.Empty)
            companymasterBO.Tinno = txt_companytinno.Text.Trim();
        if (txt_companycstno.Text != string.Empty)
            companymasterBO.Cstno = txt_companycstno.Text.Trim();
        if (txt_manager.Text != string.Empty)
            plantmasterBO.Managername = txt_manager.Text.Trim();
        if (txt_manaphone.Text != string.Empty)
            plantmasterBO.Manaphoneno = txt_manaphone.Text.Trim();
        if (txt_pmail.Text != string.Empty)
            plantmasterBO.Pmail = txt_pmail.Text.Trim();
        plantmasterBO.Companycode = cmpcode;
        plantmasterBO.Plantcode = Convert.ToInt32(txt_plantcode.Text.Trim());

        companymasterBO.Companycode = cmpcode;

        /////USERID INFO

    }

    public void setBO1()
    {

        //int Plantno = loginfoBL.GetNoofuser(txt_companycode.Text.Trim(), txt_plantcode.Text.Trim());
        loginfoBO.userloginID = txt_plantcode.Text.Trim() + "01";
        string pass1 = Encryptdata(txt_plantcode.Text.Trim() + "01");
        loginfoBO.Password = pass1;
        loginfoBO.roles =3;
        //loginfoBO.CompanyId = 1;
        loginfoBO.CompanyId = Convert.ToInt32(txt_companycode.Text.Trim());
        loginfoBO.PlantId = Convert.ToInt32(txt_plantcode.Text.Trim());
       
        
        
       // userBO.Hints = "EMPTY";
       
        //userBO.Conform_Password = TextBox15.Text;
       
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

    public void clr()
    {

        //txt_plantcode.Text = "";
        txt_Plantname.Text = "";
        txt_plantaddress.Text = "";
        txt_plantphoneno.Text = "";
        loadplantcode();
        loadCompanycode();
    }
    public void loadgrid()
    {
        gvProducts.DataBind();

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
     {

    }
    public int UserStatus()
    {

       
        if (txt_Plantname.Text == string.Empty)
        {
            uscMsgBox1.AddMessage("Please Enter Plant Name", MessageBoxUsc_Message.enmMessageType.Attention);

            return 1;
        }
        else
        {
            SqlDataReader dr;

            string str = "SELECT * from Plant_Master where Plant_Name='" + txt_Plantname.Text + "' AND company_code=" + cmpcode + "";
            dr = DBclass.GetDatareader(str);
            if (dr.Read())
            {
                uscMsgBox1.AddMessage("Plant Name AlReady Exists", MessageBoxUsc_Message.enmMessageType.Attention);

                return 1;
            }
            else
            {

                if ((txt_companycode.Text != string.Empty) && (txt_companyname.Text != string.Empty))
                {
                    setBO();
                    plantmasterBL.insertPlantmaster(plantmasterBO);
                    setBO1();
                    loginfoBL.insertloginfo(loginfoBO);
                    setBO2();
                    usersBLL.insertuser(userBO);
                    
                    txt_Plantname.Focus();
                    loadplantcode();
                    loadCompanycode();
                    loadgrid();
                    clr();
                    

                }
                else
                {

                    //txt_companycode.Focus();
                    txt_Plantname.Focus();
                   
                }
                return 0;
            }
        }
    }
  
    public void setBO2()
    {

        userBO.CompanyID = Convert.ToInt32(txt_companycode.Text.Trim());
        userBO.PlantID = Convert.ToInt32(txt_plantcode.Text.Trim());
        userBO.Table_ID = 0;
        userBO.UserID = txt_plantcode.Text.Trim() + "01";
        string pass2 = Encryptdata(txt_plantcode.Text.Trim() + "01");
        userBO.Password = pass2;
        userBO.First_Name = txt_Plantname.Text.Trim();
        userBO.Last_Name = txt_Plantname.Text.Trim();
        userBO.Hints = "EMPTY";
        userBO.Phone_Number = txt_plantphoneno.Text;
        userBO.Mobile_Number = txt_manaphone.Text;
        userBO.Email_ID = "EMPTY";
        userBO.Fax = "EMPTY";
        userBO.Organization_Name = txt_companyname.Text.Trim();
        userBO.Address = txt_plantaddress.Text.Trim();
        userBO.City = "EMPTY";
        userBO.Country = "India";
        userBO.NoofUser = 0;
        userBO.Registration_Date = System.DateTime.Now;
        userBO.Roles = 3;
        
    }
    public void loadCompanycode()
    {

        try
        {
            SqlDataReader dr = null;           
            string sqlstr = "SELECT * FROM Company_Master";
            dr = DBclass.GetDatareader(sqlstr);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_companycode.Text = dr["Company_Code"].ToString();
                    txt_companyname.Text = dr["Company_Name"].ToString();
                    txt_companyaddress.Text = dr["Company_Address"].ToString();
                    txt_companyphoneno.Text = dr["Company_PhoneNo"].ToString();
                    txt_cmail.Text = dr["Cmail"].ToString();
                    txt_companytinno.Text = dr["TinNo"].ToString();
                    txt_companycstno.Text = dr["CstNo"].ToString();
                }
            }

            
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }
    public void loadplantcode()
    {

        int mx = 0;
        try
        {
           
            string sqlstr = "SELECT MAX(Plant_Code) FROM Plant_Master WHERE  company_code=" + cmpcode + "";
            mx = Convert.ToInt32(DBclass.ExecuteScalarstr(sqlstr));
            mx = mx + 1;           
            txt_plantcode.Text = mx.ToString();
        }
        catch 
        {
            txt_plantcode.Text = "111";
            //WebMsgBox.Show(ex.ToString());
        }
        
    }





    protected void txt_companyaddress_TextChanged(object sender, EventArgs e)
    {

    }
}

