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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;
using System.Web.Services;
using System.Collections.Generic;

public partial class MilkpaymentApproval : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    SqlDataReader dr;
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    string FDATE;
    string TODATE;
    DateTime d1;
    DateTime d2;
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    int datasetcount = 0;
    DataTable avilfilename = new DataTable();
    DataTable showgrid = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    dtm = System.DateTime.Now;
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    Session["tempp"] = Convert.ToString(pcode);
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    Bdate();
                    getfilename();
                    getbankfile();
                    }
                    

                }
                else
                {
                    ccode = Session["Company_code"].ToString();


                }

                Button9.Visible = true;
            }
        catch

        {

        }
    }
    public void LoadPlantcode()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
        datasetcount = datasetcount + 1;
    }
    private void Bdate()
    {
        try
        {
            dr = null;
            dr = Billdate(ccode, ddl_Plantname.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                }
            }
            else
            {
                string mss = "No Payment FIle Set";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
            }

        }
        catch (Exception ex)
        {
        }
    }
    public SqlDataReader Billdate(string ccode, string pcode)
    {
        SqlDataReader dr;
        string sqlstr = string.Empty;
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND CurrentPaymentFlag='1' ";
        dr = DB.GetDatareader(sqlstr);
        return dr;
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        try
        {
            update();
            getfilename();
            getbankfile();

            string mss = "Record updated SuccessFully";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
        }
        catch
        {


        }
    }
    public void update()
    {

        try
        {

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string update = "";
            con = DB.GetConnection();
            update = "update BankPaymentllotment set adminapprovalstatus='1'  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "'  AND BankFileName='" + ddl_filename.SelectedItem.Text + "'";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.ExecuteNonQuery();
            string update1 = "";
            update1 = "update AdminAmountForCC set Status='1'  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Frm_date='" + d1 + "' and To_Date='" + d2 + "'  AND Filename='" + ddl_filename.SelectedItem.Text + "'";
            SqlCommand cmd1 = new SqlCommand(update1, con);
            cmd1.ExecuteNonQuery();

        }
        catch
        {


        }
    }
    public void delete()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string delete = "";
            con = DB.GetConnection();
            delete = "delete   from BankPaymentllotment  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "'  AND BankFileName='" + ddl_filename.SelectedItem.Text + "'  AND FinanceStatus NOT IN (1) ";
            SqlCommand cmd = new SqlCommand(delete, con);
            cmd.ExecuteNonQuery();
            string update1 = "";
            update1 = "update AdminAmountForCC set Status='0'  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Frm_date='" + d1 + "' and To_Date='" + d2 + "'  AND BankFileName='" + ddl_filename.SelectedItem.Text + "'";
            SqlCommand cmd1 = new SqlCommand(update1, con);
            cmd1.ExecuteNonQuery();

            string mss = "Record updated SuccessFully";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
        }
        catch
        {


        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pcode = ddl_Plantname.SelectedItem.Value;
            Bdate();
            getfilename();
            getbankfile();

        }
        catch
        {

        }
    }
    public void getfilename()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        string cashinhand = "Select  Filename   from AdminAmountForCC  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + d1 + "' and To_Date='" + d2 + "'   GROUP BY  Filename";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(cashinhand, con);
        SqlDataAdapter dsp = new SqlDataAdapter(cmd);
        avilfilename.Rows.Clear();
        ddl_filename.Items.Clear();
        dsp.Fill(avilfilename);
        try
        {
            if (avilfilename.Rows.Count > 0)
            {
                foreach (DataRow dtfs in avilfilename.Rows)
                {
                    string filen = dtfs[0].ToString();
                    ddl_filename.Items.Add(filen);
                }
            }
        }
        catch
        {
            string mss = "No fileName Ready";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
        }
    }
    public void getbankfile()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string cashinhand = "   Select   agent_id,CONVERT(DECIMAL(18,2),Sum(NetAmount)) AS netamount,adminapprovalstatus   from    BankPaymentllotment   where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "'  AND BankFileName='" + ddl_filename.SelectedItem.Text + "'   GROUP BY AGENT_ID,adminapprovalstatus ORDER BY AGENT_ID ASC";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(cashinhand, con);
            SqlDataAdapter dsp = new SqlDataAdapter(cmd);
            showgrid.Rows.Clear();
            dsp.Fill(showgrid);
            if (showgrid.Rows.Count > 0)
            {
                GridView1.DataSource = showgrid;
                GridView1.DataBind();
            }
            else
            {

            }
        }
        catch
        {

        }

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if(e.Row.Cells[2].Text=="1")
            {
                e.Row.Cells[2].Text = "Completed";
            }

            if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "Completed";
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        try
        {
            delete();
            getfilename();
            getbankfile();
        }
        catch
        {


        }
    }
    protected void ddl_filename_SelectedIndexChanged(object sender, EventArgs e)
    {
        //getfilename();
        getbankfile();
    }
}