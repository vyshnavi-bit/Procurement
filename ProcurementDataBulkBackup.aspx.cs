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
using System.Windows.Forms;
using System.Text.RegularExpressions;
public partial class ProcurementDataBulkBackup : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    public string userid;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string strsql2 = string.Empty;
    string connStr2 = ConfigurationManager.ConnectionStrings["AMPSConnectionStringDpu"].ConnectionString;
    string strsql1 = string.Empty;
    string connStr1 = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DataSet ds;
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    string message;
    string globalbank;
    string date;
    DataTable dt = new DataTable();
    DataTable dtt = new DataTable();
    int count1;
    int count2;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                //ccode = Session["Company_code"].ToString();
                //pcode = Session["Plant_Code"].ToString();
                //cname = Session["cname"].ToString();
                //pname = Session["pname"].ToString();
                //managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                txt_frmdate.Text = dtm.ToShortDateString();
             //   txt_todate.Text = dtm.ToShortDateString();
                txt_frmdate.Text = dtm.ToString("dd/MM/yyyy");
            //    txt_todate.Text = dtm.ToString("dd/MM/yyyy");
                msg_lbl.Visible = false;
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
        else
        {


            msg_lbl.Visible = false;

        }
       
      //  txt_frmdate.Text = dtm.ToShortDateString();
      
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        Checkexits();
        if (dtt.Rows.Count < 1)
        {
            GetBackup();
            serverimport();
            Checkexits();
            if (dt.Rows.Count == dtt.Rows.Count)
            {
                msg_lbl.Text = "Successfully imported Data:" + dt.Rows.Count;
                msg_lbl.Visible = true;
                msg_lbl.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                deletedata();
                msg_lbl.Text = "Records Not Inserted";
                msg_lbl.Visible = true;
                msg_lbl.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            msg_lbl.Text = "Already BackUp Exits";
            msg_lbl.Visible = true;
            msg_lbl.ForeColor = System.Drawing.Color.Red;
        }
    }
    public void serverimport()
    {

        try
        {
            if (dt.Rows.Count > 0)
            {
                //  string consString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr1))
                {
                    using (SqlBulkCopy sqlBulkCopy1 = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy1.DestinationTableName = "dbo.Procurement_log1";
                        con.Open();
                        sqlBulkCopy1.WriteToServer(dt);
                        con.Close();

                        int getcount = dt.Rows.Count;
                        string conv = getcount.ToString();
                        msg_lbl.Text = dt.Rows.Count.ToString();
                        msg_lbl.Visible = true;
                    }
                }
            }
        }
        catch (Exception ee)
        {

            msg_lbl.Text = (ee.ToString());

        }

    }
    public void GetBackup()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "";
            dt.Rows.Clear();
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();
       //     str = "select *   from procurement where     prdate between '" + d1 + "' and '" + d1 + "'";
              str = "select *   from procurement where     prdate between '" + d1 + "' and '" + d1 + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            count2 = dt.Rows.Count;
         
        }
        catch(Exception Ex)
        {
             msg_lbl.Text=Ex.Message.ToString();
             msg_lbl.Visible = true;
        }
    }
    public void deletedata()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "";
            dt.Rows.Clear();
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();
            str = "delete from Procurement_log1 where     prdate between '" + d1 + "' and '" + d1 + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();

        }
        catch (Exception Ex)
        {
            msg_lbl.Text = Ex.Message.ToString();
            msg_lbl.Visible = true;
        }
    }
    public void Checkexits()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "";
            dtt.Rows.Clear();
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();
            str = "select *   from Procurement_log1 where     prdate between '" + d1 + "' and '" + d1 + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtt);
             count1 = dtt.Rows.Count;

        }
        catch (Exception Ex)
        {
            msg_lbl.Text = Ex.Message.ToString();
            msg_lbl.Visible = true;
        }
    }
}