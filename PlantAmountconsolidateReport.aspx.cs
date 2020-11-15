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
public partial class PlantAmountconsolidateReport : System.Web.UI.Page
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
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    string FDATE;
    string TODATE;
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    SqlDataReader dr;
    int datasetcount = 0;

    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataTable fatkgdt = new DataTable();

    string d1;
    string d2;
    string agentcode;
    int countinsertdetails;
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
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                  
                    //   txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        Bdate();
                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        Bdate();
                    }

                }
                else
                {



                }

               
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;
                Bdate();
                ViewState["pcode"] = pcode.ToString();
            }
           
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


    public void loadsingleplant()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
        datasetcount = datasetcount + 1;
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        getreport();
    }

    private void Bdate()
    {
        try
        {
            dr = null;
            dr = Billdate(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_todate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                }
            }
            else
            {

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
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND STATUS='0' ";
        dr = DB.GetDatareader(sqlstr);
        return dr;
    }
    public void GETDATE()
    {
        DateTime dt1 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        DateTime dt2 = new DateTime();
        dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        d2 = dt2.ToString("MM/dd/yyyy");

    }

    public void getreport()
    {
        GETDATE();
        con = DB.GetConnection();
        string stt = "Select  convert(decimal(18,2),Sum(KG)) as kg,convert(decimal(18,2),sum(LTR)) as ltr,convert(decimal(18,2),Sum(fat_kg)) as fatkg,convert(decimal(18,2),Sum(snf_kg)) as snfkg, convert(decimal(18,2), Sum(((fat_kg) /kg) * 100)) as avgfat,convert(decimal(18,2), Sum(((snf_kg) /kg) * 100)) as avgsnf,Sum(Amount) as Amount,convert(decimal(18,2),sum(cart)) as cartage,convert(decimal(18,2),sum(spl)) as splbonus,convert(decimal(18,2),Sum(commissions)) as comm,convert(decimal(18,2),Sum( Amount + cart + spl + commissions )) as TotAmount   from (SELECT  sUM(Milk_kg) AS KG,SUM(Milk_ltr) AS LTR,SUM(fat_kg) AS   fat_kg,SUM(snf_kg) AS snf_kg,Sum(Amount) as Amount,sum(CartageAmount) as cart,sum(SplBonusAmount) as spl,sum(ComRate) as commissions   FROM  Procurement    WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   AND PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "') as ff";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DataTable getgrid = new DataTable();
        getgrid.Rows.Clear();
        DA.Fill(getgrid);
        if (getgrid.Rows.Count > 0)
        {
            GridView1.DataSource = getgrid;
            GridView1.DataBind();

        }


     
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "PlantAmountconsolidateReport:" + ddl_Plantname.SelectedItem.Text +  "   Bill Date From Date:" + txt_FromDate.Text + " :To Date:" + txt_todate.Text;
                HeaderCell2.ColumnSpan = 12;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {


        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bdate();
    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}