using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class SAP_VerifyData : System.Web.UI.Page
{
    DataTable procuredata = new DataTable();
    DataTable sapformatdata = new DataTable();
    DataTable sapgrnpass = new DataTable();
    DataTable sapinvoice = new DataTable();
    public static int roleid;
    public string ccode;
    public string pcode;
    DbHelper DB = new DbHelper();
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    DataSet DTG = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                LoadPlantcode();
            }
            else
            {

            }
        }
    }

    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            getprocurementdata();
            getsaptransactions();
            getgrndata();
            getinvoicedata();
        }
        catch
        { 
            
        }
    }

    public void getprocurementdata()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            con = DB.GetConnection();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string transactions = "";
            transactions = "SELECT convert(varchar,prdate,103) as Date,sessions  as Sess, count(*) as Rows FROM PROCUREMENT WHERE   plant_code=" + ddl_Plantname.SelectedItem.Value + " AND PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "' group by prdate,sessions order by date,sess";
            SqlCommand cmd = new SqlCommand(transactions, con);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            procuredata.Rows.Clear();
            a1.Fill(procuredata);
            if (procuredata.Rows.Count > 0)
            {
                GridView1.DataSource = procuredata;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = "NoRecords";
                GridView1.DataBind();
            }
        }
        catch
        {

        }
    }

    public void getsaptransactions()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            con = DB.GetConnection();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string transactions = "";
            transactions = "SELECT convert(varchar,Createdate,103) as Date,Sessions  as Sess, count(*) as Rows FROM saptransaction WHERE   plant_code="+ddl_Plantname.SelectedItem.Value+" AND Createdate BETWEEN '"+d1+"' AND '"+d2+"' group by Createdate,Sessions order by date,sess";
            SqlCommand cmd = new SqlCommand(transactions, con);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            sapformatdata.Rows.Clear();
            a1.Fill(sapformatdata);
            if (sapformatdata.Rows.Count > 0)
            {
                GridView2.DataSource = sapformatdata;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = "NoRecords";
                GridView2.DataBind();
            }
        }
        catch
        {

        }
    }

    public void getgrndata()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            con1 = DB.GetConnectionSAP();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string transactions = "";
            transactions = "SELECT convert(varchar,CreateDate,103) as Date,SESSION  as Sess, count(*) as Rows FROM EMROPDN WHERE   U_Status="+ddl_Plantname.SelectedItem.Value+" AND CreateDate BETWEEN '" + d1 + "' AND '" + d2 + "' group by CreateDate,SESSION order by date,sess";
            SqlCommand cmd = new SqlCommand(transactions, con1);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            sapgrnpass.Rows.Clear();
            a1.Fill(sapgrnpass);
            if (sapgrnpass.Rows.Count > 0)
            {
                GridView3.DataSource = sapgrnpass;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = "NoRecords";
                GridView3.DataBind();
            }
        }
        catch
        {

        }
    }

    public void getinvoicedata()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            con1 = DB.GetConnectionSAP();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string transactions = "";
            transactions = "SELECT convert(varchar,CreateDate,103) as Date,SESSION  as Sess, count(*) as Rows FROM EMROPCH WHERE   U_Status="+ddl_Plantname.SelectedItem.Value+" AND CreateDate BETWEEN '"+d1+"' AND '"+d2+"' group by CreateDate,SESSION order by date,sess";
            SqlCommand cmd = new SqlCommand(transactions, con1);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            sapinvoice.Rows.Clear();
            a1.Fill(sapinvoice);
            if (sapinvoice.Rows.Count > 0)
            {
                GridView4.DataSource = sapinvoice;
                GridView4.DataBind();
            }
            else
            {
                GridView4.DataSource = "NoRecords";
                GridView4.DataBind();
            }
        }
        catch
        {

        }
    }

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Procurement Data";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow1.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow1);
            }
        }
        catch
        {

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Sap Formatted Data";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow2.Cells.Add(HeaderCell2);
                GridView2.Controls[0].Controls.AddAt(0, HeaderRow2);
            }
        }
        catch
        {

        }
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow3 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Sap GRN Pushing";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow3.Cells.Add(HeaderCell2);
                GridView3.Controls[0].Controls.AddAt(0, HeaderRow3);
            }
        }
        catch
        {

        }
    }
    protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow3 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Sap Invoice Pushing";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow3.Cells.Add(HeaderCell2);
                GridView4.Controls[0].Controls.AddAt(0, HeaderRow3);
            }
        }
        catch
        {

        }
    }
}