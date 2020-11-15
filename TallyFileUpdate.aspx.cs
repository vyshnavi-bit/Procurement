using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Drawing;
using System.IO;
using System.Globalization;
public partial class TallyFileUpdate : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable dateviews = new DataTable();
    DataTable filename = new DataTable();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvald1;
    string getvalm1;
    string getvaly1;
    string jvdatest;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
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
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    billdate();
                    getii();
                    gertfilename();
                    getreport();

                }
                else
                {

                    pname = ddl_Plantname.SelectedItem.Text;

                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();
                pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {


        }
    }


    public void billdate()
    {

        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  order by  Bill_frmdate desc  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d1.ToString("dd/MM/yyy");
                    Todate = d2.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);
                }
            }
        }
        catch
        {

        }
    }
    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)   ORDER BY  Plant_Code ASC";
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
    protected void Button8_Click(object sender, EventArgs e)
    {
      //  gertfilename();
        getreport();
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        //update();

        try
        {
            string date = ddl_BillDate.Text;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvald + "/" + getvalm + "/" + getvaly;
            TODATE = getvaldd + "/" + getvalmm + "/" + getvalyy;
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + ":Bill Period" + FDATE + "To:" + TODATE + "'.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView2.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch
        {


        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    public void update()
    {
        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
        string jvdarte = drpdate.Text;
        string[] p1 = jvdarte.Split('/', '-');
        getvald1 = p1[0];
        getvalm1 = p1[1];
        getvaly1 = p1[2];
        jvdatest = getvalm1 + "/" + getvald1 + "/" + getvaly1;
        string update = "";
        update = "Update  TallyloanEntryJvpassAgentWsie set UpdateStatus='1'  where     plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and Frm_Date='" + FDATE + "'   and To_date='" + TODATE + "'  and UpdateStatus is null   and JvDate='" + jvdatest + "' ";
        con = DB.GetConnection();
        SqlCommand cmd2 = new SqlCommand(update, con);
        cmd2.ExecuteNonQuery();
        string megg="Record Updated";
        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(megg) + "')</script>");
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
        getii();
    }
    public void getii()
    {

        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
        con = DB.GetConnection();
        string strr="";
        strr = "SELECT    convert(varchar, jvDate,103) as jvDate    FROM      TallyloanEntryJvpassAgentWsie  where    frm_date='" + FDATE + "' and to_date='" + TODATE + "'  and  plant_code='" + ddl_Plantname.SelectedItem.Value + "'     GROUP BY  jvDate  ORDER BY   convert(datetime, jvDate, 103) ASC ";
        SqlCommand cmd = new SqlCommand(strr, con);
        SqlDataAdapter sgt = new SqlDataAdapter(cmd);
        dateviews.Rows.Clear();
        sgt.Fill(dateviews);
        if (dateviews.Rows.Count > 0)
        {
            drpdate.DataSource = dateviews;
            drpdate.DataTextField = "jvDate";
            drpdate.DataValueField = "jvDate";
            drpdate.DataBind();
        }
        else
        {
            drpdate.Items.Clear();
          //  drpdate.DataBind();
        }

    }


    public void getreport()
    {
        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
        con = DB.GetConnection();
        string getreport = "";
        DateTime dt1 = new DateTime();
        dt1 = DateTime.ParseExact(drpdate.Text, "dd/MM/yyyy", null);

        string[] P9 = date.Split('/', '-');

        string d1 = dt1.ToString("MM/dd/yyyy");
     //   getreport = "Select  JVNO,REPLACE(CONVERT(VARCHAR(9), jvDate, 6), ' ', '-') AS [VchDate],Legdername,Amount,Narration   from TallyloanEntryJvpassAgentWsie where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and frm_date='" + FDATE + "'   and to_date='" + TODATE + "'   and jvDate='" + d1 + "'  AND BankFileName='"+ddl_filename.SelectedItem.Text+"'";
        getreport = "SELECT ('BANKPAY' +'-'+ JVNO) AS JVNO ,[VchDate],Legdername,Amount,Narration  FROM (Select  CONVERT(VARCHAR,JVNO) AS JVNO ,REPLACE(CONVERT(VARCHAR(9), jvDate, 6), ' ', '-') AS [VchDate],Legdername,Amount,Narration    from TallyloanEntryJvpassAgentWsie where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and frm_date='" + FDATE + "'   and to_date='" + TODATE + "'   and jvDate='" + d1 + "'  AND BankFileName='" + ddl_filename.SelectedItem.Text + "') AS HH";
        SqlCommand CMt = new SqlCommand(getreport, con);
        SqlDataAdapter ac = new SqlDataAdapter(CMt);
        DataTable report = new DataTable();
        report.Rows.Clear();
        ac.Fill(report);
        if (report.Rows.Count > 0)
        {

            GridView2.DataSource = report;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = "No Records";
            GridView2.DataBind();
        }


    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {
     //   billdate();
        getii();
     //   gertfilename();
       // getreport();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
   

    protected void drpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  gertfilename();
        getreport();
    }
    public void gertfilename()
    {
        try
        {

        string date = drpdate.Text;
        string[] p3 = date.Split('/');
        getvald = p3[0];
        getvalm = p3[1];
        getvaly = p3[2];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
            con = DB.GetConnection();
            string stt = "Select   BankFileName   from BankPaymentllotment    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Added_Date='" + FDATE + "'   group by BankFileName";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            filename.Rows.Clear();
            DA.Fill(filename);
           if(filename.Rows.Count > 0)
           {
            ddl_filename.DataSource = filename;
            ddl_filename.DataTextField = "BankFileName";
            ddl_filename.DataValueField = "BankFileName";
            ddl_filename.DataBind();
           }
        }
        catch
        {

        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        gertfilename();
       
    }
}