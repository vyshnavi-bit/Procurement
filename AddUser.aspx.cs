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

public partial class AddUser : System.Web.UI.Page
{
    string uid,old=string.Empty;
    DataRow drow;
    string centreid;
    DataTable dt = new DataTable();
    int cmpcode, pcode;
    DbHelper DBclass = new DbHelper();
    BLLAdmin AdminBLL = new BLLAdmin();
    BOLAdmin AdminBOL = new BOLAdmin();
    DALAdmin AdminDAL = new DALAdmin();
    DALPlantMaster plantmasterDA = new DALPlantMaster();
    BOLPlantMaster plantmasterBO = new BOLPlantMaster();
    BLLPlantMaster plantmasterBL = new BLLPlantMaster();
   
    BOLuser userBO = new BOLuser();
    BLLuser userBL = new BLLuser();
    BOLLoinfo loginfoBO = new BOLLoinfo();
    DALLoginfo loginfoDA = new DALLoginfo();
    BLLLoginfo loginfoBL = new BLLLoginfo();
    SqlConnection con;
    SqlDataReader dr;
    string strpassword;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            uscMsgBox1.MsgBoxAnswered += MessageAnswered;
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {

                uid = Session["User_ID"].ToString();
                if (Request.QueryString["id"] != null)

                    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                else
                    Response.Write("");
                //companymasterBO.Companycode=Convert.ToInt32(Session["Company_Code"]);
                //plantmasterBO.Plantcode=Convert.ToInt32(Session["Plant_Code"]);
                //Session["Route_ID"] = cmb_RouteID.Text;
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                //loadCompanycode();
                //loadplantcode();
                loadplantID();
                loaduser();
                //Session["Aid"] = txtAgentID.Text;
                //Aid = Convert.ToInt32(Session["Aid"]);
                //Session["Aid"] = Aid;
                //Session["Route_Name"] = cmb_RouteName.Text;
                txt_Username.Focus();

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

                uid = Session["User_ID"].ToString();
                if (Request.QueryString["id"] != null)

                    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                else
                    Response.Write("");

                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                
                txt_Username.Focus();

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

        //GenerateID();
        loadplantID();
        txt_fromdate.Text = DateTime.Now.ToShortDateString();
        txt_Username.Focus();


    }

    
    private void loaduser()
    {

        int dtcount;

        dt = userBL.LoaduserGriddata(pcode, cmpcode);
        dtcount = dt.Rows.Count;
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
   
        }
        
    }
    private void setBO()
    {
        
        userBO.CompanyID = 1;
        userBO.PlantID =Convert.ToInt32( txt_PlantId.Text);
        //loginfoBO.PlantId = Convert.ToInt32(txt_PlantId.Text.Trim());
        userBO.Table_ID = 0;
        userBO.UserID = txt_UserID.Text;
        userBO.Password = strpassword;
        userBO.First_Name = txt_Username.Text;
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
        userBO.Roles =Convert.ToInt32(ddl_role.SelectedItem.Value);
       
    }
   
    public void clearr()
    {

        txt_Username.Text = " ";
        txtlastname.Text = "";
        txtcity.Text = "";
        txtEmailID.Text = "";
        txtFax.Text = " ";
        TxtMobile.Text = " ";
        TxtTeleOff.Text = "";

        TxtAddress.Text = "";
        txtPassword.Text = " ";
        txt_UserID.Text = " ";

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

    public void loadplantID()
    {

        try
        {
           
            SqlDataReader dr = null;
            //string sqlstr = "SELECT Plant_Code FROM Plant_Master Where Company_Code=" + cmpcode + "";

            string sqlstr = "select MAX(User_LoginId) from useridinfo where Company_Id='" + cmpcode + "' and plant_Id='" + pcode + "'";
            //dr = DBclass.GetDatareader(sqlstr);
            int val = Convert.ToInt32(DBclass.ExecuteScalarint(sqlstr)) ;
            if (val == 0)
            {
                
                   
                    {
                        txt_PlantId.Text = pcode.ToString();

                        txt_UserID.Text = pcode + "01";
                    }
                
            }
            else
            {
                val++;
                txt_PlantId.Text = pcode.ToString();

                txt_UserID.Text = val.ToString();
            }


        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        strpassword = Encryptdata(txtPassword.Text);
      
        setBO();
        userBL.insertuser(userBO);
        setBO1();
        loginfoBL.insertloginfo(loginfoBO);
        
        clearr();
        loadplantID();
        loaduser();


        uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
    }
    public void setBO1()
    {

        
        loginfoBO.userloginID = txt_UserID.Text.Trim();
        strpassword=Encryptdata(txtPassword.Text.Trim());
        loginfoBO.Password =strpassword ;
        loginfoBO.roles = Convert.ToInt32(ddl_role.SelectedItem.Value);
        //loginfoBO.CompanyId = 1;
        loginfoBO.CompanyId = cmpcode;
        loginfoBO.PlantId = pcode;
        
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {

    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_PlantId.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }

    protected void txt_PlantId_TextChanged(object sender, EventArgs e)
    {

    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        clearr();
        loadplantID();
        this.loaduser();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (program.Guser_role >= 3)
        {
            int drole = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[2].Text.ToString().Trim()); 

            if (program.Guser_role==drole)
            {
                uscMsgBox1.AddMessage("You can't delete admin record", MessageBoxUsc_Message.enmMessageType.Success);
                
            }
            else
            {
                string uid = GridView1.Rows[e.RowIndex].Cells[1].Text;
                userBL.deleteuser(uid, pcode, cmpcode);
                loginfoBL.deleteinfo(uid, pcode, cmpcode);
                //if (program.Guser_role == 4 && drole==3)
                //{
                //plantmasterBL.deleteplant(pcode, cmpcode);
                //}
                loaduser();
                Response.Redirect(Request.RawUrl);
            }
        }
        else
        {
            uscMsgBox1.AddMessage("Privilage available only administrator", MessageBoxUsc_Message.enmMessageType.Success);
        }
        
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        loaduser();
        this.ModalPopupExtender1.Show();
        
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
    }
    protected void btnCancel_Click(object sender, EventArgs e) 
    {
        GridView1.EditIndex = -1;
        clearr();
        loadplantID();
        this.loaduser();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int index = GridView1.EditIndex;
        GridViewRow row = GridView1.Rows[index];
        old = ((TextBox)row.Cells[5].Controls[0]).Text.ToString().Trim();
        string poppass = Encryptdata(TextBox1.Text);
        if (old == poppass)
        {
            index = GridView1.EditIndex;
            row = GridView1.Rows[index];
            loaduser();
            int count;
            count = dt.Rows.Count;
            drow = dt.Rows[index];

            userBO.Table_ID = 0;
            userBO.UserID = ((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim();
            string strpass = TextBox2.Text;
            string d = Encryptdata(strpass);
            userBO.Password = d;
            userBO.PlantID = pcode;
            userBO.CompanyID = cmpcode;

            userBL.edituser(userBO);

            loginfoBO.userloginID = ((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim();
            string strpass2 = Encryptdata(TextBox2.Text);
            loginfoBO.Password = strpass2;
            loginfoBO.PlantId = pcode;
            loginfoBO.CompanyId = cmpcode;

            loginfoBL.editloginfo(loginfoBO);
            GridView1.EditIndex = -1;
            loaduser();
            uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
        }
        else
        {
            uscMsgBox1.AddMessage("Please enter correct old password", MessageBoxUsc_Message.enmMessageType.Success);
            GridView1.EditIndex = -1;
            clearr();
            loadplantID();
            this.loaduser();
        }
        
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
