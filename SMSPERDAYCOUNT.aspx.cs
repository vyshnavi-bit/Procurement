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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class SMSPERDAYCOUNT : System.Web.UI.Page
{

    BLLuser Bllusers = new BLLuser();
    DbHelper db = new DbHelper();
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //DISPLAY = "ARANI BILL  FROM[11-06-2016]TO[20-06-2016] AMOUNT:540000";
                //Response.Write("<marquee>   <span style= 'color:BLUE'>     " + DISPLAY + " </span> </marquee>");
                //	managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");


                Label5.Visible = false;
                //GetprourementIvoiceData();
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
           //     pcode = ddl_Plantcode.SelectedItem.Value;
                //	pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //	managmobNo = Session["managmobNo"].ToString();
                Label5.Visible = false;
                //Response.Write("<marquee>   <span style= 'color:BLUE'>     " + DISPLAY + " </span> </marquee>");
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getgrid();
        Label5.Visible = true;
    }



    public void getgrid()
    {


        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        string str = "";
        str = "SELECT   COUNT(*)  AS COUNT    FROM   procurement  WHERE  PRDATE BETWEEN  '" + d1 + "' AND '" + d2 + "' AND   STATUS=1  ";
        con = db.GetConnection();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader DR = cmd.ExecuteReader();
     
        if(DR.HasRows)
        {
            while (DR.Read())
            {
                Label5.Text = "TOTAL SMS SEND:" +   DR["COUNT"].ToString();
            }

        }
        else
        {

         

        }


    }
}