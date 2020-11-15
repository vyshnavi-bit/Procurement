using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class SapDataStatus : System.Web.UI.Page
{
    DataTable sapformatdata=new DataTable();
    DataTable sapgrnpass=new DataTable();
    DataTable sapinvoice=new DataTable();
    DataTable Nilagnet = new DataTable();
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
                dtm = System.DateTime.Now;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
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
            getsaptransactions();
            getgrn();
            getgrninvoice();
            getsaptransactionsAgentnil();
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
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            con = DB.GetConnection();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string transactions = "";
            transactions = "SELECT CONVERT(VARCHAR,CreateDate,103) AS Data,SESSIONS,COUNT(*) AS SNO    FROM   saptransaction    WHERE Createdate='" + d1 + "'   AND PLANT_CODE='" + ddl_Plantname.SelectedItem.Value + "'  AND Sessions='"+ddl_shift.SelectedItem.Text+"'  GROUP BY  CreateDate,SESSIONS ";
            SqlCommand cmd = new SqlCommand(transactions, con);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            sapformatdata.Rows.Clear();
            a1.Fill(sapformatdata);
            if (sapformatdata.Rows.Count > 0)
            {
                GridView1.DataSource = sapformatdata;
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
    public void getsaptransactionsAgentnil()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            con = DB.GetConnection();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string transactions = "";
            //    transactions = "Select    proagent,prdate,Sessions,days,month,year,milkltr,MilkKg,Fat,Snf,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Plant_Name,WHcode,Agent_Name,VendorCode,clr   from (Select proagent,prdate,Sessions,days,month,year,MilkKg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Agent_Name,VendorCode,clr  from (sELECT proagent,prdate,Sessions,convert(varchar,days) as days,convert(varchar,month) as month,convert(varchar,year) as year,milkkg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Agent_Name,VendorCode,Plant_Code,clr   FROM (sELECT *    FROM (Select   Tid, convert(varchar,Agent_id,0) AS proagent,convert(varchar,Prdate,101) as prdate,Sessions,DATEPART(day,Prdate) as days ,DATEPART(MONTH,Prdate) as month,FORMAT(prdate,'yy') as year,Sum(Fat) as Fat,Sum(Snf) as Snf,Sum(Milk_kg) as MilkKg,Sum(milk_ltr) as   milkltr,Sum(rate) as Rate,Sum(Amount) as Amount,Sum(Comrate) as Commi,Sum(CartageAmount) as CartAmt,Sum(SplBonusAmount) as SplBon,Plant_Code,Sum(Clr) as clr     from procurement    where plant_code='155'   and  Prdate='2-22-2017'  AND SESSIONS='AM'   group by   Plant_Code,Tid,Agent_id,prdate,Sessions)  AS PRO LEFT JOIN (sELECT Agent_Id,Agent_Name,VendorCode   FROM Agent_Master    WHERE Plant_code='155'   GROUP BY  Agent_Id,Agent_Name,VendorCode) as am on PRO.proagent= am.Agent_Id) AS ONEE WHERE VendorCode IS   NULL)  as rrr) as leftside left join (Select   Plant_Code as pmplantcode,Plant_Name,WHcode   from  Plant_Master    where Plant_Code='155'     group by  Plant_Code,Plant_Name,WHcode) as pmm on Plant_Code=pmm.pmplantcode";
            transactions = "select  (proagent +'-'+ Agent_Name +'-'+  Plant_Name)  as Name,prdate,Sessions   from (Select    proagent,Plant_Name,Agent_Name,prdate,Sessions   from (Select proagent,prdate,Sessions,days,month,year,MilkKg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Agent_Name,VendorCode,clr  from (sELECT proagent,prdate,Sessions,convert(varchar,days) as days,convert(varchar,month) as month,convert(varchar,year) as year,milkkg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Agent_Name,VendorCode,Plant_Code,clr   FROM (sELECT *    FROM (Select   Tid, convert(varchar,Agent_id,0) AS proagent,convert(varchar,Prdate,101) as prdate,Sessions,DATEPART(day,Prdate) as days ,DATEPART(MONTH,Prdate) as month,FORMAT(prdate,'yy') as year,Sum(Fat) as Fat,Sum(Snf) as Snf,Sum(Milk_kg) as MilkKg,Sum(milk_ltr) as   milkltr,Sum(rate) as Rate,Sum(Amount) as Amount,Sum(Comrate) as Commi,Sum(CartageAmount) as CartAmt,Sum(SplBonusAmount) as SplBon,Plant_Code,Sum(Clr) as clr     from procurement    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Prdate='" + d1 + "'  AND SESSIONS='"+ddl_shift.SelectedItem.Text+"'   group by   Plant_Code,Tid,Agent_id,prdate,Sessions)  AS PRO LEFT JOIN (sELECT Agent_Id,Agent_Name,VendorCode   FROM Agent_Master    WHERE Plant_Code='"+ddl_Plantname.SelectedItem.Value+"'  GROUP BY  Agent_Id,Agent_Name,VendorCode) as am on PRO.proagent= am.Agent_Id) AS ONEE WHERE VendorCode IS   NULL)  as rrr) as leftside left join (Select   Plant_Code as pmplantcode,Plant_Name,WHcode   from  Plant_Master    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'     group by  Plant_Code,Plant_Name,WHcode) as pmm on Plant_Code=pmm.pmplantcode    ) as fgf   group by proagent,Plant_Name,Agent_Name,prdate,Sessions  ORDER BY RAND(proagent) ASC";
            SqlCommand cmd = new SqlCommand(transactions, con);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            Nilagnet.Rows.Clear();
            a1.Fill(Nilagnet);
            if (Nilagnet.Rows.Count > 0)
            {
                GridView4.DataSource = Nilagnet;
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

    public void getgrn()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            con1 = DB.GetConnectionSAP();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string transactions = "";
            transactions = "sELECT   CONVERT(VARCHAR,CreateDate,103) AS Data,SESSION,COUNT(*) AS SNO    FROM   EMROPDN    WHERE    u_status='" + ddl_Plantname.SelectedItem.Value + "'  and  Createdate='" + d1 + "'  AND SESSION='"+ddl_shift.SelectedItem.Text+"'   GROUP BY  CreateDate,SESSION";
            SqlCommand cmd = new SqlCommand(transactions, con1);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            sapgrnpass.Rows.Clear();
            a1.Fill(sapgrnpass);
            if (sapgrnpass.Rows.Count > 0)
            {
                GridView2.DataSource = sapgrnpass;
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
    public void getgrninvoice()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            con1 = DB.GetConnectionSAP();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string transactions = "";
            transactions = "sELECT  CONVERT(VARCHAR,CreateDate,103) AS Data,SESSION,COUNT(*) AS SNO    FROM   EMROPCH    WHERE     u_status='" + ddl_Plantname.SelectedItem.Value + "' and  TaxDate='" + d1 + "'   AND SESSION='" + ddl_shift.SelectedItem.Text + "'  GROUP BY  CreateDate,SESSION ";
            SqlCommand cmd = new SqlCommand(transactions, con1);
            SqlDataAdapter a1 = new SqlDataAdapter(cmd);
            sapinvoice.Rows.Clear();
            a1.Fill(sapinvoice);
            if (sapinvoice.Rows.Count > 0)
            {
                GridView3.DataSource = sapinvoice;
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
    public void getapinvoice()
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
                HeaderCell2.Text = "Sap Formatted Data";
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
                HeaderCell2.Text = "Sap Grn Data Pushing";
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
                HeaderCell2.Text = "Sap Invoice Pushing";
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
                HeaderCell2.Text = "Nil Vendor code";
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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}