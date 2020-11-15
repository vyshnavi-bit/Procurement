using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Newuser : System.Web.UI.Page
{
    DbHelper db = new DbHelper();
    SqlConnection con = new SqlConnection();
    string username;
    string msg;
    int getval;
    BLLuser Bllusers = new BLLuser();
    public string ccode;
    public string plantcode;
    string message;
    DbHelper DB = new DbHelper();
    string getserial;
    string firname;
    string lname;
    string uname;
    string pass;
    string Emil;
    string add;
    string gen;
    int stas;
    int Rol;
    string desc;
    int ppcode;
    string getplant;
    string getplantno;
    int btst;
    int sum1, sum2, sum3, sum4, sum5, sum6, sum7,sum8;
    int UPDATECHE;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                getgrid();
                username = Session["name"].ToString();
                ccode = Session["Company_code"].ToString();
                LoadPlantcode();
                getserachall();
                if (btst == 1) 
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }
                else
                {

                     btnUpdate.Visible = false;
                     btnSave.Visible = true ;
                }
            }


        }

        else
        {
            getgrid();
            getserachall();
            if (btst == 1)
            {
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
            else
            {

                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }
            ccode =Session["Company_code"].ToString();
            username = Session["name"].ToString();
          //  btnUpdate.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        insert();
    }



    private void checkusername()
    {
        try
        {
            SqlDataReader dr;
            string sqlstr = null;
            sqlstr = "SELECT  *      FROM newusers     where username='" + txtUserName.Text + "'";
            dr = DB.GetDatareader(sqlstr);
            if (dr.HasRows)
            {
                message = "User Name Already Exits";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                txtUserName.Text = "";
                txtUserName.Focus();

            }
            else
            {
                txtPwd.Focus();
              

            }

            // Response.Redirect("home.aspx");
        

        }

        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    public void clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtUserName.Text = "";
        txtPwd.Text = "";
        txtRePwd.Text = "";
        txtAdress.Text = "";
      
        txtEmailID.Text = "";
        txt_desc.Text = "";

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

    public void insert() 
    {
        try
        {
            

            if (ddl_Plantname.SelectedValue != "-1")
            {
                con = db.GetConnection();
                string[] p = ddl_Plantname.SelectedValue.Split('_');
                //  string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string insertSql = "INSERT INTO Newusers (FirstName,LastName,UserName,Password,Email,Address,Gender,AddedDate,insertedBy,Description,status,role,Plant_code)values('" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtUserName.Text + "','" + txtPwd.Text + "','" + txtEmailID.Text + "','" + txtAdress.Text + "','" + rdoGender.Text + "','" + System.DateTime.Now + "','" + username + "','" + txt_desc.Text + "','" + drp_status.SelectedValue + "','" + drp_role.SelectedValue + "','" + p[0].ToString() +"')";
                SqlCommand cmd = new SqlCommand(insertSql, con);
                cmd.ExecuteNonQuery();
                getmsg();
                clear();
                getgrid();
            }
            else
            {



                message = "Please Select Plantname.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);



            }
        }
        catch (Exception ee)
        {

            msg = ee.ToString();
            getval = 1;

        }

    
    }

    public void getmsg()
    {
      
        if (getval == 1)
        {
            message = msg;
        }
        else
        {
             message = "Your details have been saved successfully.";

        }
      
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


    }

    public void getgrid()
    {
         con = db.GetConnection();
         string get = "Select UserId,UserName,Gender,Address,Role   from  Newusers order by UserId desc";
        SqlCommand cmd = new SqlCommand(get,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();


    }

    public void getserach()
    {
        con = db.GetConnection();
        string get = "Select UserId,UserName,Gender,Address,Role   from  Newusers where Role='"+search.Text+"'  order by UserId desc";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

       





    }


    public void getserachall()
    {
        con = db.GetConnection();
        string get = "Select  Role   from  Newusers    order by UserId desc";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);


        foreach (DataRow dr in dt.Rows)
        {

            int i = Convert.ToInt16(dr[0]);

            if (i == 9)
            {

                sum8 = sum8 + 1;
                splad.Text = sum8.ToString();
            }
           

            if (i == 7)
            {

                sum7 = sum7 + 1;
                sp.Text = sum7.ToString();
            }

            if (i == 6)
            {

                sum6 = sum6 + 1;
                admin.Text = sum6.ToString();

            }



            if (i == 5)
            {

                sum5 = sum5 + 1;
                man.Text = sum5.ToString();

            }




            if (i == 4)
            {

                sum4 = sum4 + 1;
                acc.Text = sum4.ToString();

            }




            if (i == 3)
            {

                sum3 = sum3 + 1;
                fin.Text = sum3.ToString();

            }

            if (i == 2)
            {

                sum2 = sum2 + 1;
                usr.Text = sum2.ToString();

            }

            if (i == 1)
            {
                sum1 = sum1 + 1;
                end.Text = sum1.ToString();


            }





        }
        
      }



    




   
    //Create SQL connection
  //  SqlConnection con = new SqlConnection(connectionString);
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string role = e.Row.Cells[5].Text;
            if (role == "9")
                e.Row.Cells[5].Text = "Special Administrator";
            if(role=="7")
              e.Row.Cells[5].Text ="Super Administrator";
            if(role=="6")
              e.Row.Cells[5].Text ="Administrator";
             if(role=="5")
            e.Row.Cells[5].Text ="Manager";
            if(role=="4")
             e.Row.Cells[5].Text ="Accounts";
            if(role=="3")
             e.Row.Cells[5].Text ="Finance";
            if(role=="2")
            e.Row.Cells[5].Text ="User";
            if (role == "1")
                e.Row.Cells[5].Text = "End User";

        }
    }
    protected void rdoGender_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void search_TextChanged(object sender, EventArgs e)
    {
        getserach();

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        //plantcode = ddl_Plantcode.SelectedItem.Value;
       
    }
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        if (UPDATECHE != 1)
        {
        }
        else
        {
            checkusername();
        }

        if ((btst == 1)  &&  (UPDATECHE == 1))
        {
            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }
        else
        {

            btnUpdate.Visible = false;
            btnSave.Visible = true;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        getserial =Convert.ToInt32( GridView1.SelectedRow.Cells[1].Text).ToString();
        getvalue();
        btst=1;
        if (btst == 1)
        {
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            UPDATECHE = 1;
        }
        else
        {

            btnUpdate.Visible = false;
            btnSave.Visible = true;
        }
       

    }


    public void getplantname()
    {
        
        con = db.GetConnection();
        string get = "Select *   from  plant_master where plant_code='" + ppcode + "'  order by plant_code desc";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                
                
     
              getplantno =   dr["Plant_Code"].ToString();
              getplant = dr["Plant_Name"].ToString();
             //   allgetplantname();

           


            }



        }
       

    }


   



    public void getvalue()
    {

        con = db.GetConnection();
        string get = "Select *   from  Newusers where UserId='" + getserial + "'  order by UserId desc";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
               
                txtFirstName.Text = dr["FirstName"].ToString();
                txtLastName.Text = dr["LastName"].ToString();
                txtUserName.Text = dr["UserName"].ToString();
                txtPwd.TextMode = TextBoxMode.SingleLine;
                txtPwd.Text = dr["Password"].ToString();
                txtRePwd.TextMode = TextBoxMode.SingleLine;
                txtRePwd.Text = dr["Password"].ToString();
                txtEmailID.Text = dr["Email"].ToString();
                txtAdress.Text = dr["Address"].ToString();
                rdoGender.Text = dr["Gender"].ToString();
                stas = Convert.ToInt32(dr["status"]);
                if (stas == 1)
                {
                    drp_status.SelectedItem.Text = "Active";
                    drp_status.Items.Add("InActive");
                }
                if (stas == 0)
                {

                    drp_status.SelectedItem.Text = "InActive";
                    drp_status.Items.Add("Active");

                }

                drp_status.Items.Add("Active");

              //  drp_status.SelectedItem.Text = drp_status.Text;

               
                Rol = Convert.ToInt32(dr["Role"].ToString());

                if (Rol == 8)

                    drp_role.SelectedItem.Text = "Special Administrator";       

                if (Rol == 7)

                     drp_role.SelectedItem.Text ="Super Administrator";       

                if (Rol ==6)
                     drp_role.SelectedItem.Text="Administrator";
                if (Rol == 5)
                   drp_role.SelectedItem.Text="Manager";
                if (Rol == 4)
                   drp_role.SelectedItem.Text="Accounts";
                if (Rol == 3)
                   drp_role.SelectedItem.Text="Finance";
                if (Rol == 2)
                   drp_role.SelectedItem.Text="User";
                if (Rol == 1)
                  drp_role.SelectedItem.Text="End User";


             //   drp_role.SelectedIndex = 1;

             
                txt_desc.Text = dr["Description"].ToString();
                ppcode = Convert.ToInt16(dr["plant_code"].ToString());
                getplantname();
                LoadPlantcode();
                ddl_Plantcode.SelectedItem.Text = getplantno;
                ddl_Plantname.SelectedItem.Text = getplant;
              //  ddl_Plantname.SelectedValue = "";
                ddl_Plantname.Items.Add("150_VYSHNAVI ADMIN");
              
          //    ddl_Plantname.Items.Add("150_ADMIN"
               
            }
            


        }


    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        try
        {

              getserial =  GridView1.SelectedRow.Cells[1].Text.ToString();
            if (ddl_Plantname.SelectedValue != "-1")
            {
                con = db.GetConnection();
                string[] p = ddl_Plantname.SelectedValue.Split('_');
                if (p[0] != "VYSHNAVI ADMIN")
                {
                    //  string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    string insertSql = "Update  newusers set FirstName='" + txtFirstName.Text + "',LastName='" + txtLastName.Text + "',UserName='" + txtUserName.Text + "',Password='" + txtPwd.Text + "',Email='" + txtEmailID.Text + "',Address='" + txtAdress.Text + "',Gender='" + rdoGender.Text + "',AddedDate='" + System.DateTime.Now + "',insertedBy='" + Session["name"].ToString() + "',Description='" + txt_desc.Text + "',status='" + drp_status.Text + "',role='" + drp_role.SelectedItem.Value + "',Plant_code='" + p[0].ToString() + "'    WHERE   UserId='" + getserial + "' ";
                    SqlCommand cmd = new SqlCommand(insertSql, con);
                    cmd.ExecuteNonQuery();
                    getmsg();
                    clear();
                    getgrid();
                }
                else
                {
                    message = "Select Proper value";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

                }
            }
            else
            {



                message = "Please Select Plantname.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);



            }
        }
        catch (Exception ee)
        {

            msg = ee.ToString();
            getval = 1;

        }
    }
    protected void rdoGender_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        getgrid();
    }
    protected void Newusr_Click(object sender, EventArgs e)
    {
        Response.Redirect("Newuser.aspx");
    }
    protected void txtRePwd_TextChanged(object sender, EventArgs e)
    {

    }
}