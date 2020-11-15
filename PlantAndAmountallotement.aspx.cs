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

public partial class PlantAndAmountallotement : System.Web.UI.Page
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
    DbHelper DB = new DbHelper();
    DateTime d1;
    DateTime d2;
    DataSet OVERALL = new DataSet();
    DataTable DTK = new DataTable();
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
    int count;

    string Bill;
    string BankPayment;
    string excessamt;
    string GETDETAILS;

    double getbillamt;
    double getpayamt;
    double getexcessamt;
    double totbal;

    string plantname;
    string totamt;
    string paidmat;
    string Balance;

    string[] GETBANKPAY;
    string[] GETBANKPAY1;

    double GETCOWTOTCALC;
    double TOTCAL;
    string GETCOWTOT;
    string GETCPAID;
    string GETCEXEPAID;
    string GETCbuffEXEPAID;
    double GETCPAIDCALC;
    double GETCPAIDCEXCESC;
    double GETCPAIDCbuffEXCESC;


    double GETbuffTOTCALC;
    double TOTbuffCAL;
    string GETbuffTOT;
    string GETCbuffPAID;
    double GETbuffCPAIDCALC;

    int sno = 1;
    string getplantname;
    string getplcode;
    double getgridview2footersum=0;
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
                    billdate();
                    GridView2.Visible = false;
                    GridView3.Visible = false;
                }
                else
                {



                }

            }
            else
            {
                ccode = Session["Company_code"].ToString();



            }
        }

        catch
        {



        }
    }


    public void GETBILLDATA()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT = "select  floor(SUM(NetAmount)) as Amount   from    Paymentdata    where   Frm_date='" + FDATE + "' and To_date='" + TODATE + "'";
            SqlCommand CMD = new SqlCommand(GETBILLAMT, con);
            SqlDataAdapter DA = new SqlDataAdapter(CMD);
            DA.Fill(OVERALL, ("bILLaMOUNT"));

            //string GETBILLAMT1 = "SELECT        SUM(TotAmount) AS Excessamt  FROM   AgentExcesAmount WHERE (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "')";
            //SqlCommand CMD1 = new SqlCommand(GETBILLAMT, con);
            //SqlDataAdapter DA1 = new SqlDataAdapter(CMD1);
            //DA1.Fill(OVERALL, ("Excessamt"));

        }
        catch
        {
        }



    }

    public void getcowexcessamt()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT = "SELECT SUM(TotAmount) AS ExcesAmount  FROM   AgentExcesAmount WHERE        (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "') AND Plant_code IN (155,156,159,161,163,164,158,162,171,172,173)";
            SqlCommand CMDCOW = new SqlCommand(GETBILLAMT, con);
            SqlDataAdapter DACOW = new SqlDataAdapter(CMDCOW);
            DACOW.Fill(OVERALL, ("ExcesAmount"));
        }
        catch
        {

        }
    }

    public void GETBILLDATACOW()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT = "select  floor(SUM(NetAmount)) as Amount   from    Paymentdata    where     Plant_code IN (155,156,159,161,163,164,158,162,171,172,173) AND      Frm_date='" + FDATE + "' and To_date='" + TODATE + "'";
            SqlCommand CMDCOW = new SqlCommand(GETBILLAMT, con);
            SqlDataAdapter DACOW = new SqlDataAdapter(CMDCOW);
            DACOW.Fill(OVERALL, ("bILLaMOUNTCOWBILL"));
        }
        catch
        {

        }


    }

    public void GETBILLDATABUFF()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT = "select  floor(SUM(NetAmount)) as Amount   from    Paymentdata    where  Plant_code IN (157,165,166,167,168,169) AND   Frm_date='" + FDATE + "' and To_date='" + TODATE + "'";
            SqlCommand CMDBUFF = new SqlCommand(GETBILLAMT, con);
            SqlDataAdapter DABUFF = new SqlDataAdapter(CMDBUFF);
            DABUFF.Fill(OVERALL, ("bILLaMOUNTbuffa"));
        }
        catch
        {

        }


    }


    public void excessamtbuff()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT = "SELECT SUM(TotAmount) AS ExcesAmount  FROM   AgentExcesAmount WHERE        (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "') AND Plant_code IN (157,165,166,167,168,169)";
            //string GETBILLAMT = "select  floor(SUM(NetAmount)) as Amount   from    Paymentdata    where  Plant_code IN (157,165,166,167,168,169) AND   Frm_date='" + FDATE + "' and To_date='" + TODATE + "'";
            SqlCommand CMDBUFF = new SqlCommand(GETBILLAMT, con);
            SqlDataAdapter DABUFF = new SqlDataAdapter(CMDBUFF);
            DABUFF.Fill(OVERALL, ("bILLexcessaMOUNTbuffa"));
        }
        catch
        {

        }


    }


    public void GETPAYDATA()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT1 = "select     ISNULL(floor(SUM(NetAmount)),0) as Amount     from BankPaymentllotment   where   Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "' AND Plant_code IN (155,156,159,161,163,164,158,162,171,172,173) and FinanceStatus='1'";
            SqlCommand CMD1 = new SqlCommand(GETBILLAMT1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(CMD1);
            DA1.Fill(OVERALL, ("bANKaMOUNT"));
        }
        catch
        {

        }
    }

    public void getagentexcessamt()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT1 = "SELECT SUM(TotAmount) AS Excessamt  FROM   AgentExcesAmount WHERE (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "')";
            SqlCommand CMD1 = new SqlCommand(GETBILLAMT1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(CMD1);
            DA1.Fill(OVERALL, ("Excessamt"));
        }
        catch
        {

        }
    }

    public void GETPAYDATACOW()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT1 = "select    ISNULL(floor(SUM(NetAmount)),0) as Amount     from BankPaymentllotment   where  Plant_code IN (155,156,159,161,163,164,158,162,171,172,173) AND  Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "' AND  FinanceStatus='1'";
            SqlCommand CMD1COW = new SqlCommand(GETBILLAMT1, con);
            SqlDataAdapter DA1COW = new SqlDataAdapter(CMD1COW);
            DA1COW.Fill(OVERALL, ("bANKaMOUNTCOWBANKPAY"));
        }
        catch
        {

        }
    }

    public void GETPAYDATABUFF()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            con = DB.GetConnection();
            string GETBILLAMT1 = "select     ISNULL(floor(SUM(NetAmount)),0) as Amount     from BankPaymentllotment   where   Plant_code IN (157,165,166,167,168,169) AND  Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "' AND  FinanceStatus='1'";
            SqlCommand CMD1BUFF = new SqlCommand(GETBILLAMT1, con);
            SqlDataAdapter DA1BUFF = new SqlDataAdapter(CMD1BUFF);
            DA1BUFF.Fill(OVERALL, ("bANKaMOUNTbuffa"));
        }
        catch
        {

        }
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
    public void GETGRIDVIEW()
    {
        try
        {
            DTK.Columns.Add("Name");
            DTK.Columns.Add("BillAmount");
            DTK.Columns.Add("ExcessAmount");
            DTK.Columns.Add("PaidAmount");
            DTK.Columns.Add("Balance");
            GETBILLDATA();
            foreach (DataRow DR in OVERALL.Tables[count].Rows)
            {
                GETDETAILS = "Total Amount";
                Bill = DR[0].ToString();
                Bill = DR[0].ToString();
                getbillamt = Convert.ToDouble(Bill);
                Bill = getbillamt.ToString();
                count = count + 1;
                GETPAYDATA();
                foreach (DataRow DR1 in OVERALL.Tables[count].Rows)
                {
                    BankPayment = (DR1[0]).ToString();
                    getpayamt = Convert.ToDouble(BankPayment);
                    BankPayment = getpayamt.ToString();
                }
                totbal = (getbillamt - getpayamt);
                count = count + 1;
                getagentexcessamt();
                foreach (DataRow DR2 in OVERALL.Tables[count].Rows)
                {
                    excessamt = (DR2[0]).ToString();
                    if (excessamt != "")
                    {
                        getexcessamt = Convert.ToDouble(excessamt);
                    }
                    else
                    {
                        getexcessamt = 0;
                    }
                    excessamt = getexcessamt.ToString();
                }


            }
            // added all the amounts
            DTK.Rows.Add(GETDETAILS, Bill, excessamt, BankPayment, totbal);
            DTK.Rows.Add("", "------", "------", "------");
            GETBILLDATACOW();
            count = count + 1;
            foreach (DataRow DRCOWDATA in OVERALL.Tables[count].Rows)
            {

                GETCOWTOT = DRCOWDATA[0].ToString();
                GETCOWTOTCALC = Convert.ToDouble(GETCOWTOT);
                GETCOWTOT = GETCOWTOTCALC.ToString();
                GETPAYDATACOW();
                count = count + 1;
                foreach (DataRow DR1COW in OVERALL.Tables[count].Rows)
                {
                    GETCPAID = (DR1COW[0]).ToString();
                    GETCPAIDCALC = Convert.ToDouble(GETCPAID);
                    GETCPAID = GETCPAIDCALC.ToString();

                }
                TOTCAL = (GETCOWTOTCALC - GETCPAIDCALC);
                double TGETCOWTOT = Convert.ToDouble(GETCOWTOT);
                double TGETCPAID = Convert.ToDouble(GETCPAID);

                getcowexcessamt();
                count = count + 1;
                foreach (DataRow DR1ex in OVERALL.Tables[count].Rows)
                {
                    GETCEXEPAID = (DR1ex[0]).ToString();
                    if (GETCEXEPAID != "")
                    {
                        GETCPAIDCEXCESC = Convert.ToDouble(GETCEXEPAID);
                    }
                    GETCEXEPAID = GETCPAIDCEXCESC.ToString();

                }
                double TEXCEGETCPAID = Convert.ToDouble(GETCEXEPAID);

                DTK.Rows.Add("Cow CC Total Amount", TGETCOWTOT.ToString("F2"), TEXCEGETCPAID.ToString("F2"), TGETCPAID.ToString("F2"), TOTCAL.ToString("F2"));
            }
            count = count + 1;
            getplantwisebillCOW();
            DTK.Rows.Add("", "------", "------", "------", "------");

            foreach (DataRow DR2 in OVERALL.Tables[count].Rows)
            {

                plantname = DR2[0].ToString();
                totamt = DR2[1].ToString();
                paidmat = DR2[2].ToString();
                if (paidmat == "")
                {
                    paidmat = "0.00";

                }
                GETBANKPAY = paidmat.Split('.');
                paidmat = GETBANKPAY[0] + ".00";
                Balance = DR2[3].ToString();
                double cowexcessamt = 0;
                DTK.Rows.Add(plantname, totamt, cowexcessamt,  paidmat, Balance);
            }
            DTK.Rows.Add("", "------", "------", "------", "------");
            GETBILLDATABUFF();
            count = count + 1;
            foreach (DataRow DRbuffDATA in OVERALL.Tables[count].Rows)
            {

                GETbuffTOT = DRbuffDATA["Amount"].ToString();
                if (GETbuffTOT != "")
                {
                    GETbuffTOTCALC = Convert.ToDouble(GETbuffTOT);
                }
                GETbuffTOT = GETbuffTOTCALC.ToString();
                GETPAYDATABUFF();
                count = count + 1;
                foreach (DataRow DR1buff in OVERALL.Tables[count].Rows)
                {
                    GETCbuffPAID = (DR1buff[0]).ToString();
                    GETbuffCPAIDCALC = Convert.ToDouble(GETCbuffPAID);
                    GETCbuffPAID = GETbuffCPAIDCALC.ToString();

                }
                TOTbuffCAL = (GETbuffTOTCALC - GETbuffCPAIDCALC);
                double TGETbuffTOT = Convert.ToDouble(GETbuffTOT);
                double TGETCbuffPAID = Convert.ToDouble(GETCbuffPAID);

                excessamtbuff();
                count = count + 1;
                foreach (DataRow DR1ex in OVERALL.Tables[count].Rows)
                {
                    GETCbuffEXEPAID = (DR1ex[0]).ToString();
                    if (GETCbuffEXEPAID != "")
                    {
                        GETCPAIDCbuffEXCESC = Convert.ToDouble(GETCbuffEXEPAID);
                    }
                    else
                    {
                        GETCPAIDCbuffEXCESC = 0;
                    }
                    GETCbuffEXEPAID = GETCPAIDCbuffEXCESC.ToString();

                }
                double excessTEXCEGETCPAID = Convert.ToDouble(GETCbuffEXEPAID);


                DTK.Rows.Add("Buffalo CC Total Amount", TGETbuffTOT.ToString("F2"), excessTEXCEGETCPAID.ToString("F2"), TGETCbuffPAID.ToString("F2"), TOTbuffCAL.ToString("F2"));
            }
            count = count + 1;
            getplantwisebillCOW();
            DTK.Rows.Add("", "------", "------", "------");
            //getplantwisebillEXCESSCOW();

            getplantwisebillBUFF();
            foreach (DataRow DR3 in OVERALL.Tables[count].Rows)
            {
                plantname = DR3[0].ToString();
                totamt = DR3[1].ToString();
                paidmat = DR3[2].ToString();
                GETBANKPAY1 = paidmat.Split('.');
                paidmat = GETBANKPAY1[0] + ".00";
                Balance = DR3[3].ToString();
                string plantcode = DR3[4].ToString();
                string date = ddl_BillDate.Text;
                string[] p = date.Split('/', '-');
                string buffexcessamt = "0";
                getvald = p[0];
                getvalm = p[1];
                getvaly = p[2];
                getvaldd = p[3];
                getvalmm = p[4];
                getvalyy = p[5];
                FDATE = getvalm + "/" + getvald + "/" + getvaly;
                TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
                DataSet dtexeces = new DataSet();
                string getting1 = "SELECT SUM(TotAmount) AS ExcesAmount  FROM   AgentExcesAmount WHERE (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "') AND Plant_code='" + plantcode + "'";
                SqlCommand cmd2121 = new SqlCommand(getting1, con);
                SqlDataAdapter da2121 = new SqlDataAdapter(cmd2121);
                da2121.Fill(dtexeces, "excces");
                foreach (DataRow DRex in dtexeces.Tables[0].Rows)
                {
                    buffexcessamt = DRex[0].ToString();
                }
                DTK.Rows.Add(plantname, totamt, buffexcessamt, paidmat, Balance);
            }
            if (DTK.Rows.Count > 0)
            {
                GridView1.DataSource = DTK;
                GridView1.DataBind();
            }
        }
        catch
        {
        }
    }
    public void getplantwisebillCOW()
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
            string getting;
            getting = "select Plant_Name,Amount,ISNULL(floor(paidAmount),0) AS paidAmount,(ISNULL(Amount,0)- ISNULL(paidAmount,0)) as Balance   from (select plant_name,totamt.Plant_code,totamt.Amount   from (select Plant_code,ISNULL(floor(SUM(NetAmount)),0) as Amount   from    Paymentdata    where  Plant_code IN (155,156,159,161,163,164,158,162,171,172,173)  AND  Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by Plant_code) as totamt left join (select Plant_Code,Plant_Name   from Plant_Master group by Plant_Code,Plant_Name) as pm on  totamt.Plant_code= pm.Plant_Code) as totpay left  join (select   ISNULL(floor(SUM(NetAmount)),0) as paidAmount,Plant_Code      from BankPaymentllotment   where   Plant_code IN (155,156,159,161,163,164,158,162,171,172,173) AND   Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "' and FinanceStatus='1' group by Plant_Code)as paid on totpay.Plant_code = paid.Plant_Code  order by totpay.Plant_code ASC   ";
            SqlCommand cmd212 = new SqlCommand(getting, con);
            SqlDataAdapter da212 = new SqlDataAdapter(cmd212);
            da212.Fill(OVERALL, ("allbANKaMOUNT"));
        }
        catch
        {

        }
    }
    public void getplantwisebillBUFF()
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
            string getting1;
         //   getting1 = "select Plant_Name,ISNULL(Amount,0) AS Amount,ISNULL(floor(paidAmount),0) AS paidAmount,ISNULL((Amount- paidAmount),0) as Balance   from (select plant_name,totamt.Plant_code,totamt.Amount   from (select Plant_code,floor(SUM(NetAmount)) as Amount   from    Paymentdata    where  Plant_code IN (157,165,166,167,168,169)  AND  Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by Plant_code) as totamt left join (select Plant_Code,Plant_Name   from Plant_Master group by Plant_Code,Plant_Name) as pm on  totamt.Plant_code= pm.Plant_Code) as totpay left  join (select    floor(SUM(NetAmount)) as paidAmount,Plant_Code      from BankPaymentllotment   where    Plant_code IN (157,165,166,167,168,169) AND   Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "' AND FinanceStatus='1' group by Plant_Code)as paid on totpay.Plant_code = paid.Plant_Code  order by totpay.Plant_code ASC   ";
            getting1 = "sELECT  Plant_Name,  ISNULL(Amount,0) AS Amount,ISNULL(floor(paidAmount),0) AS paidAmount,ISNULL((Amount- paidAmount),0) as Balance, PCODE  FROM (select  PCODE,Plant_Name,ISNULL(Amount,0) AS Amount,ISNULL(floor(paidAmount),0) AS paidAmount   from (select plant_name,totamt.Plant_code AS PCODE,totamt.Amount  from (select Plant_code,ISNULL(floor(SUM(NetAmount)),0) as Amount   from    Paymentdata     where  Plant_code IN (157,165,166,167,168,169)  AND  Frm_date='" + FDATE + "' and To_date='" + TODATE + "'   group by Plant_code) as totamt left join (select Plant_Code,Plant_Name   from Plant_Master group by Plant_Code,Plant_Name) as pm on  totamt.Plant_code= pm.Plant_Code) as totpay left  join (select    ISNULL(floor(SUM(NetAmount)),0) as paidAmount,Plant_Code      from BankPaymentllotment   where    Plant_code IN (157,165,166,167,168,169) AND    Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "'  AND FinanceStatus='1' group by Plant_Code)as paid on totpay.PCODE = paid.Plant_Code    ) AS FDF    order by  PCODE ASC";
            SqlCommand cmd2121 = new SqlCommand(getting1, con);
            SqlDataAdapter da2121 = new SqlDataAdapter(cmd2121);
            da2121.Fill(OVERALL, ("allbANKaMOUNTBUFF"));
        }
        catch
        {
        }
    }

    public void getplantwisebillEXCESSCOW()
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
            string getting;
            getting = "SELECT SUM(TotAmount) AS ExcesAmount, Plant_Name, Plant_code   FROM AgentExcesAmount  WHERE        (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "') AND Plant_code IN (155,156,159,161,163,164,158,162,171,172,173) Group by Plant_Name, Plant_code";
            //getting = "select Plant_Name,Amount,ISNULL(floor(paidAmount),0) AS paidAmount,(ISNULL(Amount,0)- ISNULL(paidAmount,0)) as Balance   from (select plant_name,totamt.Plant_code,totamt.Amount   from (select Plant_code,ISNULL(floor(SUM(NetAmount)),0) as Amount   from    Paymentdata    where  Plant_code IN (155,156,159,161,163,164,158,162)  AND  Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by Plant_code) as totamt left join (select Plant_Code,Plant_Name   from Plant_Master group by Plant_Code,Plant_Name) as pm on  totamt.Plant_code= pm.Plant_Code) as totpay left  join (select   ISNULL(floor(SUM(NetAmount)),0) as paidAmount,Plant_Code      from BankPaymentllotment   where   Plant_code IN (155,156,159,161,163,164,158,162) AND   Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "' and FinanceStatus='1' group by Plant_Code)as paid on totpay.Plant_code = paid.Plant_Code  order by totpay.Plant_code ASC   ";
            SqlCommand cmd212 = new SqlCommand(getting, con);
            SqlDataAdapter da212 = new SqlDataAdapter(cmd212);
            da212.Fill(OVERALL, ("allbANKEXCESSaMOUNT"));
        }
        catch
        {
        }
    }
    public void getplantwisebillEXCESSBUFF()
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
            string getting1;
            getting1 = "SELECT SUM(TotAmount) AS ExcesAmount, Plant_Name, Plant_code   FROM AgentExcesAmount  WHERE        (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "') AND Plant_code IN (157,165,166,167,168,169) Group by Plant_Name, Plant_code";
           // getting1 = "sELECT  Plant_Name,ISNULL(Amount,0) AS Amount,ISNULL(floor(paidAmount),0) AS paidAmount,ISNULL((Amount- paidAmount),0) as Balance  FROM (select  PCODE,Plant_Name,ISNULL(Amount,0) AS Amount,ISNULL(floor(paidAmount),0) AS paidAmount   from (select plant_name,totamt.Plant_code AS PCODE,totamt.Amount  from (select Plant_code,ISNULL(floor(SUM(NetAmount)),0) as Amount   from    Paymentdata     where  Plant_code IN (157,165,166,167,168,169)  AND  Frm_date='" + FDATE + "' and To_date='" + TODATE + "'   group by Plant_code) as totamt left join (select Plant_Code,Plant_Name   from Plant_Master group by Plant_Code,Plant_Name) as pm on  totamt.Plant_code= pm.Plant_Code) as totpay left  join (select    ISNULL(floor(SUM(NetAmount)),0) as paidAmount,Plant_Code      from BankPaymentllotment   where    Plant_code IN (157,165,166,167,168,169) AND    Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "'  AND FinanceStatus='1' group by Plant_Code)as paid on totpay.PCODE = paid.Plant_Code    ) AS FDF    order by  PCODE ASC";
            SqlCommand cmd2121 = new SqlCommand(getting1, con);
            SqlDataAdapter da2121 = new SqlDataAdapter(cmd2121);
            da2121.Fill(OVERALL, ("allbANKaMOUNTEXCESSBUFF"));
        }
        catch
        {
        }
    }


    protected void Button6_Click(object sender, EventArgs e)
    {
        GETGRIDVIEW();
        GridView2.Visible = false;
        GridView3.Visible = false;
    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;


                if ((e.Row.Cells[1].Text == "Total Amount") || (e.Row.Cells[1].Text == "Cow CC Total Amount") || (e.Row.Cells[1].Text == "Buffalo CC Total Amount") || (e.Row.Cells[1].Text == "&nbsp;"))
                {

                    e.Row.Cells[6].Text = "";

                }

                if ((e.Row.Cells[1].Text == "Cow CC Total Amount") || (e.Row.Cells[1].Text == "Buffalo CC Total Amount") || (e.Row.Cells[1].Text == "Total Amount"))
                {

                    e.Row.BackColor = System.Drawing.Color.Cyan;

                }


                if ((e.Row.Cells[1].Text == "Cow CC Total Amount") || (e.Row.Cells[1].Text == "Buffalo CC Total Amount") || (e.Row.Cells[1].Text == "Total Amount") || (e.Row.Cells[1].Text == "&nbsp;"))
                {

                    e.Row.Cells[0].Text = "";


                }
                else
                {
                    e.Row.Cells[0].Text = sno.ToString();
                    sno = sno + 1;

                }




            }
        }
        catch
        {


        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //ViewState["FDATE"] = FDATE;
        //ViewState["tDATE"] = TODATE;
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Bill Payment Deatils For Bill periopd fROM:" + ViewState["FDATE"] + "To Date" + ViewState["tDATE"];
                HeaderCell2.ColumnSpan = 6;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch
        {


        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            GETGRIDVIEW();

            getplantname = GridView1.SelectedRow.Cells[1].Text;
            getpcode();

            string date = ddl_BillDate.Text;
            ViewState["BILLD"] = date;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            ViewState["FDATE"] = FDATE;
            ViewState["tDATE"] = TODATE;
            string getagent = "";
            getagent = "select   Plant_Name,Updatedtime,BankFileName,CONVERT(DECIMAL(18,2),NetAmount) AS  NetAmount   from (SELECT plant_code,Updatedtime,Floor(SUM(NetAmount)) as NetAmount,BankFileName    FROM BankPaymentllotment  WHERE Plant_Code='" + getplcode + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  group by Plant_Code,Updatedtime,BankFileName) as pay left join (select Plant_Code,Plant_Name   from Plant_Master  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code  order by Updatedtime asc  ";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(getagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtf = new DataTable();
            da.Fill(dtf);
            if (dtf.Rows.Count > 0)
            {
                GridView2.Visible = true;
                GridView3.Visible = false;
                GridView2.DataSource = dtf;
                GridView2.DataBind();
                GridView2.FooterRow.Cells[4].Text = getgridview2footersum.ToString("f2");
                GridView2.FooterRow.HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Font.Bold = true;
            }
        }
        catch
        {


        }
    }


    public void getpcode()
    {
        try
        {
            string pcoo = "";
            pcoo = "Select  plant_code   from  Plant_Master    where  plant_name='" + getplantname + "'";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(pcoo, con);
            SqlDataAdapter dfs = new SqlDataAdapter(cmd);
            DataTable tf = new DataTable();
            dfs.Fill(tf);
            if (tf.Rows.Count > 0)
            {

                getplcode = tf.Rows[0][0].ToString();
                ViewState["PLC"] = getplcode;

            }
        }
        catch
        {

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                double getcellvalue4 = Convert.ToDouble(e.Row.Cells[4].Text);
                getgridview2footersum = getgridview2footersum + getcellvalue4;

            }
        }
        catch
        {


        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string routeid = GridView2.SelectedRow.Cells[1].Text;
        //string getroute = routeid;
        //string getst = getroute + "00";
        //string getend = getroute + "99";

        string date = ViewState["BILLD"].ToString();
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
        string getagent = "";

        string filename= GridView2.SelectedRow.Cells[3].Text;

        getagent = "   select payment.Agent_Id,Agent_name,Payment_mode,Bank_Name,Ifsc_code,Agent_accountNo,TotNetAmount,isnull(Paidamount,0) as Paidamount,((TotNetAmount) - isnull(Paidamount,0))  as Balance         from (select Agent_id,Agent_name,Ifsc_code,Agent_accountNo,Bank_Name,Payment_mode,CONVERT(decimal(18,2), isnull(Sum(NetAmount),0)) as  TotNetAmount,Plant_code  from  Paymentdata   where Plant_code='" + ViewState["PLC"] + "'   and Frm_date='" + FDATE + "'   and To_date='" + TODATE + "'  group by  Agent_id,Agent_name,Ifsc_code,Agent_accountNo,Bank_Name,Payment_mode,Plant_code) as payment  left join (select CONVERT(decimal(18,2), isnull(Sum(NetAmount),0)) as Paidamount,Plant_Code,Agent_Id  from  BankPaymentllotment   where Plant_code='" + ViewState["PLC"] + "'   and  Billfrmdate='" + FDATE + "'   and   Billtodate='" + TODATE + "'  AND BankFileName='" + filename + "'  group by Plant_Code,Agent_Id ) as paid on payment.Plant_code = paid.Plant_Code and payment.Agent_id = paid.Agent_Id  WHERE Paidamount > 0";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(getagent, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {

            GridView3.Visible = true;
            GridView3.DataSource = dtff;
            GridView3.DataBind();

            GridView3.FooterRow.Cells[6].Text = "Total Amount";
            decimal billadv = dtff.AsEnumerable().Sum(row => row.Field<decimal>("Paidamount"));
            GridView3.FooterRow.HorizontalAlign = HorizontalAlign.Left;
            GridView3.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            GridView3.FooterRow.Cells[7].Font.Bold = true;
            GridView3.FooterRow.Cells[8].Font.Bold = true;
            GridView3.FooterRow.Cells[8].Text = billadv.ToString("N2");

        }
        else
        {


        }
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //        TableCell HeaderCell2 = new TableCell();
        //        HeaderCell2.Text = "";
        //        HeaderCell2.Text = "Bill Payment Deatils For Bill periopd:" + ddl_BillDate.SelectedItem.Text;
        //        HeaderCell2.ColumnSpan = 6;
        //        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        //        HeaderRow.Cells.Add(HeaderCell2);
        //        GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        //    }
        //}
        //catch
        //{


        //}
    }
}