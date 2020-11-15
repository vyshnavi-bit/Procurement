using System;
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
using System.Net;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using iTextSharp.text.pdf;

public partial class BankFileUpdateRelase : System.Web.UI.Page
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
    DbHelper db = new DbHelper();
    msg msg = new msg();
    int I;
    string getplant;
    string getAgent;
    string Date;
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    DateTime d1;
    DateTime d2;
    string DATE;
    string DATE1;
    string DATE2; // JAN

    string BANKPAY;
    string cASHPAY;
    string TOTPAY;
    double cashying;

    double getbankam2;
    double getcasham2;
    double gettot2;
    double getpaid2;
    double getbalance2;

    double getbankpay;
    double getpaidamt;
    double bankingbalance;
    string get;
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
                    //    GETGRID();
                    

                    if (roleid < 3)
                    {
                        loadsingleplant();


                        //lad.Visible = false;
                    }

                    else
                    {

                        LoadPlantcode();
                        //Button5.Visible = false;


                    }

                    billdate();
                    //   billdate();

                }
                else
                {



                }
                // billdate();
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;

                //     billdate();
                if (roleid > 2)
                {



                }
                else
                {



                }
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
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }

    public void loadsingleplant()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
    }
    public void getrealse()
    {

        try
        {
            string get = "";
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
            if (ddl_bankfilename.SelectedItem.Text != string.Empty)
            {
                string timeupdate = System.DateTime.Now.ToString();
                get = "Update BankPaymentllotment set UpdatedUser='0',ReleasedUserName='" + Session["Name"] + "',ReleaseTime='" + FDATE + "'  where plant_code='" + pcode + "' and Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "' and BankFileName='" + ddl_bankfilename.SelectedItem.Text + "'";
                con = DB.GetConnection();
                SqlCommand cmf = new SqlCommand(get, con);
                cmf.ExecuteNonQuery();

                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg.billupdate) + "')</script>");
            }
            else
            {
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg.Check) + "')</script>");

            }

        }
        catch
        {

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        getrealse();
        Adddate();
    }
    public void billdate()
    {
        try
        {
            con.Close();
            con = DB.GetConnection();
            string str;
            str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";
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
    private void Adddate()
    {
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
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            //ViewState["FDATE"] = FDATE;
            //ViewState["TODATE"] = TODATE;
            //ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            //ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            string GETT = "Select BankFileName from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + FDATE + "' AND billtodate='" + TODATE + "'  and UpdatedUser='1' group by BankFileName ";
            con =DB.GetConnection();
            SqlCommand CMD = new SqlCommand(GETT, con);
            SqlDataAdapter DSQ=new SqlDataAdapter(CMD);
            DataTable DTJ = new DataTable();
            DSQ.Fill(DTJ);
            ddl_bankfilename.DataSource = DTJ;
            ddl_bankfilename.DataTextField = "BankFileName";
            ddl_bankfilename.DataValueField = "BankFileName";
            ddl_bankfilename.DataBind();
         }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        Adddate();
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}