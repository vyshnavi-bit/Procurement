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
using System.IO;


public partial class SUPERWISERDETAILS : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string bankcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    string getrouteusingagent;
    string bank;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    int id;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
        {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());

                dtm = System.DateTime.Now;
                txt_dob.Text = dtm.ToShortDateString();
                txt_adddate.Text = dtm.ToShortDateString();

                txt_dob.Text = dtm.ToString("dd/MM/yyyy");
                txt_adddate.Text = dtm.ToString("dd/MM/yyyy");

                if (roleid < 3)
                {
                  //  getcomapnyandplantcode();
                    loadsingleplant();
                    gettid();
                    getbankname();
                    gridview();

                }

                if ((roleid >= 3) && (roleid != 9))
                {

                    getcomapnyandplantcode();
                    LoadPlantcode();
                    gettid();
                    getbankname();
                    gridview();

                }
                if (roleid == 9)
                {

                    getcomapnyandplantcode();
                    loadspecialsingleplant();
                    gettid();
                    getbankname();
                    gridview();

                }
            }

            else
            {
                //dtm = System.DateTime.Now;
                //// dti = System.DateTime.Now;
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyy");

                pcode = ddl_Plantcode.SelectedItem.Value;
                bankcode = ddl_bankid.SelectedValue;
                //  LoadPlantcode();
                //gridview();
                //getrouteid();

            }

        }
        else
        {


      
         //  LoadPlantcode();
           pcode = ddl_Plantcode.SelectedItem.Value;
           bankcode = ddl_bankid.SelectedValue;
           gridview();
        }
    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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
    protected void btn_Ok_Click(object sender, EventArgs e)
    {

        if ((txt_name.Text != string.Empty) && (txt_address.Text != string.Empty) && (txt_mobile.Text != string.Empty) && (txt_account.Text != string.Empty) && (txt_qualifications.Text != string.Empty) && (txt_bank.Text != string.Empty) && (ddl_ifsccode.Text != string.Empty))
        {
            INSERT();
        }
        else
        {

            WebMsgBox.Show("Please Fill Above Empty Fields");

        }
        gridview();
        gettid();
        clear();
    }
    public void clear()
{
  //  ddl_ccode.Items.Clear();
  //  ddl_Plantname.Items.Clear();
    txt_name.Text = "";
    txt_address.Text = "";
    txt_mobile.Text = "";
  //  ddl_bankname.Items.Clear();
  //  ddl_ifsccode.Items.Clear();
    txt_account.Text = "";
    txt_pannumber.Text = "";
    txt_description.Text = "";
    txt_bank.Text = "";
    txt_qualifications.Text = "";
   // txt_dob.Text = dtm.ToString("dd/MM/yyyy");
  //  txt_adddate.Text = dtm.ToString("dd/MM/yyyy");

}
    //public void getagentid()
    //{

    //    try
    //    {
    //        //   ddl_agentcode.Items.Clear();
    //        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        SqlConnection con = new SqlConnection(connStr);
    //        con.Open();
    //        string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {

    //            ddl_agentcode.Items.Add(dr["agent_id"].ToString());


    //        }


    //    }

    //    catch
    //    {

    //        WebMsgBox.Show("NO MILK");

    //    }



    //}
    public void getcomapnyandplantcode()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
          //  string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select distinct company_code from Plant_Master ";
            
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl_ccode.Items.Add(dr["company_code"].ToString());
  
                //ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                //ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
            }

        }

        catch
        {

            WebMsgBox.Show("ERROR");

        }

    }
    public void getbankname()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select * from Bank_Details   order by rand(Bank_id) ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

               // ddl_ccode.Items.Add(dr["company_code"].ToString());
                ddl_bankid.Items.Add(dr["Bank_id"].ToString());
                ddl_bankname.Items.Add(dr["Bank_id"].ToString() + "_" + dr["Bank_name"].ToString());
            }


        }

        catch
        {

            WebMsgBox.Show("ERROR");

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
    public void gettid()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            //SqlDataReader dr = null;
            //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            try
            {
                string sqlstr = "SELECT max(Supervisor_Code) as  tid  FROM Supervisor_Details";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                int id = Convert.ToInt16(ds.Tables[0].Rows[0]["tid"]);
                tid.Text = (id + 1).ToString();
            }
            catch
            {
                tid.Text = "1";


            }
        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        gridview();
    }

    public void INSERT()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_dob.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_adddate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
      //  string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO Supervisor_Details (SupervisorName,Company_Code,Plant_Code,Added_Date,Dob,Address,Mobile,bank_name,IfscCode,AccountNumber,Pannumber,Qualification,Description) VALUES ('" + txt_name.Text + "','" + ddl_ccode.Text + "','" + ddl_Plantcode.Text + "','" + d2 + "','" + d1 + "','" + txt_address.Text + "','" + txt_mobile.Text + "','" + txt_bank.Text + "','" + ddl_ifsccode.Text + "','" + txt_account.Text + "','" + txt_pannumber.Text + "','" + txt_qualifications.Text + "','" + txt_description.Text + "')";

        cmd.Connection = conn;

        conn.Open();

        cmd.ExecuteNonQuery();
        WebMsgBox.Show("inserted Successfully");
        conn.Close();

    }
    protected void ddl_bankname_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        ddl_bankid.SelectedIndex =ddl_bankname.SelectedIndex;
        bankcode= ddl_bankid.SelectedValue;
        getifsccode();
    }

    public void getifsccode()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
         //   string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select * from BANK_MASTER   where plant_code='" + pcode + "' and Bank_id='" + ddl_bankid.Text + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                // ddl_ccode.Items.Add(dr["company_code"].ToString());

              ddl_ifsccode.Items.Add(dr["ifsc_code"].ToString());
               txt_bank.Text=  dr["Bank_name"].ToString();
                
            }


        }

        catch
        {

            WebMsgBox.Show("ERROR");

        }



    }


    protected void txt_dob_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }
    public void gridview()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_dob.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_adddate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sqlstr = "SELECT Supervisor_Code,Plant_Code,SupervisorName,convert(varchar,Dob,103) as Dob,Address,Mobile,Bank_name,IfscCode,AccountNumber,Pannumber  FROM Supervisor_Details WHERE plant_code='" + ddl_Plantcode.SelectedItem.Value + "'  order by  Supervisor_Code desc";
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
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
    protected void ddl_ifsccode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection conn = new SqlConnection(connStr);
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label lbldeleteid = (Label)row.FindControl("Supervisor_Code");
        conn.Open();
        if (program.Guser_role == 4)
        {
            //SqlCommand cmd = new SqlCommand("delete FROM Supervisor_Details where Supervisor_Code='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", conn);
            //cmd.ExecuteNonQuery();
        }
        conn.Close();
        gridview();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridview();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string item = e.Row.Cells[0].Text;
        //    foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
        //    {
        //        if (button.CommandName == "Delete")
        //        {
        //            button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
        //        }
        //    }
        //}
    }
    protected void tid_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_bankname_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        gridview();
    }
    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridview();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            //  Label lblID = (Label)row.FindControl("tid");
            // TextBox bid = (TextBox)row.Cells[0].Controls[0];

           // TextBox agentid1 = (TextBox)row.Cells[1].Controls[0];
            //  TextBox routeid1 = (TextBox)row.Cells[1].Controls[0];
            // TextBox milknature1 = (TextBox)row.Cells[2].Controls[0];


            //foreach (GridViewRow gvr in GridView1.Rows)
            //{
            //   ddate = GridView1.DataKeys[gvr.RowIndex].Values["prdate"].ToString();

            //}
            //    TextBox prdate1 = (TextBox)row.Cells[2].Controls[0];

            // TextBox prdate1 = ddate;
            //  TextBox shift1 = (TextBox)row.Cells[3].Controls[0];
            TextBox supname = (TextBox)row.Cells[2].Controls[0];
            TextBox supmobile = (TextBox)row.Cells[5].Controls[0];
            TextBox supifsc = (TextBox)row.Cells[7].Controls[0];

            TextBox supacc = (TextBox)row.Cells[8].Controls[0];
            TextBox suppan = (TextBox)row.Cells[9].Controls[0];
            // TextBox remark1 = (TextBox)row.Cells[8].Controls[0];
            GridView1.EditIndex = -1;
            conn.Open();
            getrouteusingagent = supifsc.Text;
            //   ddl_agentcode.Text = getrouteusingagent;
            //  ddl_agentcode.Text = getrouteusingagent;
            getrouteid();

            if (id == 1)
            {
                SqlCommand cmd = new SqlCommand("update Supervisor_Details  set SupervisorName='" + supname.Text + "',Mobile='" + supmobile.Text + "',IfscCode='" + supifsc.Text + "',Bank_name='" + bank + "',AccountNumber='" + supacc.Text + "',Pannumber='" + suppan.Text + "' where Supervisor_Code='" + userid + "' and plant_code='" + pcode + "'", conn);
                cmd.ExecuteNonQuery();
                WebMsgBox.Show("Updated Successfully");
            }
            else
            {
                WebMsgBox.Show("Please Check Ifsc code");

            }
            gridview();
        }
        catch
        {

            WebMsgBox.Show("Please Check updated Data");
        }
        gridview();
       
    }

    public void getrouteid()
    {

        try
        {
            //  ddl_agentcode.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select DISTINCT(Bank_name) from bank_master where Plant_code='" + pcode + "'  and ifsc_code='" + getrouteusingagent + "'   ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                
                bank = dr["Bank_name"].ToString();
                id = 1;
            }
            else
            {

             //  WebMsgBox.Show("ERROR");
               id = 0;
               
            }
        }

        catch
        {

            WebMsgBox.Show("Please Check");

        }



    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }


    protected void PrintAllPages(object sender, EventArgs e)
    {
       
    }
}