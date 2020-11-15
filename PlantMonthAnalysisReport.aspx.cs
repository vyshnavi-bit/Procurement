using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class PlantMonthAnalysisReport : System.Web.UI.Page
{
    DbHelper db = new DbHelper();
    SqlConnection con = new SqlConnection();
    string planno;
    DateTime dtm = new DateTime();
    public static int roleid;
    public string ccode;
    public string pcode;
    string msg;
    string nn;
    int CHECKVAL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                dtm = System.DateTime.Now;

                nn = Session["Name"].ToString();
                // nn = "hh";
                //  getgroup();

                //}
            }
           
        }
        else
        {

            getgroup();



        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getSUBGROUPNAME();
        if (CHECKVAL == 0)
        {
            gettotamount();
            getgroup();
            CLEAR();
        }
    }

    public void CLEAR()
    {
       // drpbtn.Items.Clear();
        txt_subheader.Text = "";
    }


    public void gettotamount()
    {



        try
        {
            string str = "";
            con = db.GetConnection();

            str = "insert into expencesheader(Headername,HeaderValue,SubheaderName,Datetime,Insertby)values('" + drpbtn.SelectedItem.Text + "','" + drpbtn.SelectedValue + "','" + txt_subheader.Text + "','" + System.DateTime.Now + "','" + Session["Name"].ToString() + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            string script = "window.onload = function(){ alert('";
            script += "Save successfully";
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            cmd.ExecuteNonQuery();
        }
        catch(Exception ee)
        {
             msg = ee.Message.ToString();
            string script = "window.onload = function(){ alert('";
            script += msg;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

        }




    }

    public void getgroup()
    {

        string str = "";
        con = db.GetConnection();
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        str = "Select *   from expencesheader  WHERE Headername='" + drpbtn.SelectedItem.Text+ "'   order by tid desc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "No Records";
            GridView1.DataBind();
        }


    }

    public void getSUBGROUPNAME()
    {

        string str = "";
        con = db.GetConnection();
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        str = "Select *   from expencesheader  WHERE SubheaderName='" + txt_subheader.Text + "'   order by tid desc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            CHECKVAL = 1;
           
            txt_subheader.Focus();
            string script = "window.onload = function(){ alert('";
            script += " Name Already Exists";
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            cmd.ExecuteNonQuery();
        }
        else
        {
           
            CHECKVAL = 0;
        }


    }
    protected void drpbtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        getgroup();
       

    }
    protected void txt_subheader_TextChanged(object sender, EventArgs e)
    {
        getSUBGROUPNAME();
    }
}