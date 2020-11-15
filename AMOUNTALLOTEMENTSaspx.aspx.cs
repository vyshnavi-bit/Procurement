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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.util.collections;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using System.IO;
public partial class AMOUNTALLOTEMENTSaspx : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    public string uid;
    double getavil;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    DateTime dtm = new DateTime();
    DateTime dtm2 = new DateTime();
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
                //    pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                 dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_time.Text = DateTime.Now.ToString("hh:mm:ss tt");
                txt_userid.Text = Session["Name"].ToString();
                //  dtm2 = System.DateTime.Now;
                //txt_frmdeldate.Text= dtm2.ToShortDateString();
                //txt_Todeldate.Text = dtm2.ToShortDateString();
                //txt_frmdeldate.Text = dtm2.ToString("dd/MM/yyyy");
                //txt_Todeldate.Text = dtm2.ToString("dd/MM/yyyy");

                gettid();
                getamount();
                gridview();

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
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                gridview();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
                      
        }
    }


    public void gridview()
    {

        try
        {
            DateTime dt1 = new DateTime();
          //  DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
         //   dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
        //    string d2 = dt2.ToString("MM/dd/yyyy");

         dt2 = txt_time.Text;

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sqlstr = "SELECT Tid,Name,convert(varchar,Date,103) as Date,Time,Amount,Description from AdminAllotAmount   order by tid desc ";
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
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        double getamount1 = 0.0;
        if (!(string.IsNullOrEmpty(txt_amount.Text)))
        {
             getamount1 = Convert.ToDouble(txt_amount.Text);
        }
        else
        {
            txt_amount.Text = "0.0";
        }
        
        getamount();
        if (getavil >= 0)
        {

        }
        else
        {
            getavil = 0.0;
        }
        double sumvar = getavil + getamount1;
        Txt_Availamount.Text = getavil.ToString();
        double totamt = Convert.ToDouble(Txt_Availamount.Text);

        Txt_Availamount.Text = sumvar.ToString();
        txt_name.Text = "Accounts";

        if ((txt_name.Text != string.Empty) && (txt_FromDate.Text != string.Empty) && (txt_time.Text != string.Empty) && (txt_amount.Text != string.Empty) && (txt_description.Text != string.Empty) && (txt_userid.Text != string.Empty))
        {
            INSERT();          
        }
        else
        {
            WebMsgBox.Show("Please Fill Above Empty Fields");
        }
      
        gettid();
        getamount();
        clear();
    }
    public void clear()
    {
        //  ddl_ccode.Items.Clear();
        //  ddl_Plantname.Items.Clear();
        txt_name.Text = "";
        txt_amount.Text = "";
        txt_description.Text = "";
        //  ddl_bankname.Items.Clear();
        //  ddl_ifsccode.Items.Clear();

        // txt_dob.Text = dtm.ToString("dd/MM/yyyy");
        //  txt_adddate.Text = dtm.ToString("dd/MM/yyyy");

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
                string sqlstr = "SELECT max(tid) as  tid  FROM AdminAllotAmount ";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                int id = Convert.ToInt16(ds.Tables[0].Rows[0]["tid"]);
                txt_tid.Text = (id + 1).ToString();
            }
            catch
            {
                txt_tid.Text = "1";


            }
        }
    }
    public void getamount()
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
                string sqlstr = "SELECT ISNULL((t1.AdminAmt-t2.AdminPlantAmt),0) AS AvailAmount FROM (SELECT ISNULL(SUM(Amount),0) AS AdminAmt from AdminAllotAmount ) AS t1 LEFT JOIN (SELECT ISNULL(SUM(Amount),0) AS AdminPlantAmt  from AdminAmountAllottoplant) AS t2 ON t1.AdminAmt>0";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                adp.Fill(ds);
                 getavil = Convert.ToDouble(ds.Tables[0].Rows[0]["AvailAmount"]);
                 Txt_Availamount.Text = getavil.ToString();

                 if (Txt_Availamount.Text == "")
                 {

                     Txt_Availamount.Text = "0";

                 }
            }
            catch
            {

                Txt_Availamount.Text = "0";

            }
        }
    }

    public void INSERT()
    {

        DateTime dt1 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //    dt2 = DateTime.ParseExact(txt_adddate.Text, "dd/MM/yyyy", null);

       // string d1 = dt1.ToString("MM/dd/yyyy");
     //   string d2 = dt2.ToString("MM/dd/yyyy");
        //  string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd = new SqlCommand();
       
       
        
        if (txt_previous.Text == "")
        {
            txt_previous.Text = "0";

        }
        cmd.CommandText = "INSERT INTO AdminAllotAmount (Name,Date,Time,Amount,Description,UserId,PreviousBalance,AvailAmount) VALUES ('" + txt_name.Text + "','" + dt1 + "','" + dt2 + "','" + txt_amount.Text + "','" + txt_description.Text + "','" + txt_userid.Text + "','"+txt_previous.Text+"','" +Txt_Availamount.Text + "' )";

        cmd.Connection = conn;

        conn.Open();

        cmd.ExecuteNonQuery();
        gridview();
        WebMsgBox.Show("Inserted Successfully");
        conn.Close();

    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_previous_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }
   
}