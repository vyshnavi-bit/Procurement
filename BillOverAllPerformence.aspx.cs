using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Drawing;

public partial class BillOverAllPerformence : System.Web.UI.Page
{
    string getplantname;
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
    DataTable DTK1 = new DataTable();
    DataTable DTK2 = new DataTable();
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
    DataTable COUWTOTPAYMENT = new DataTable();
    DataTable COUWBUFFPAYMENT = new DataTable();
    DataTable COUWTOTPAYMENTPAID = new DataTable();
    DataTable COUWBUFFPAYMENTPAID = new DataTable();
    DataSet DTG = new DataSet();
    int datasetcount = 0;
    int j;
    DataTable getnewcolumn = new DataTable();
    DataTable addgrid = new DataTable();
    DataTable GETDATETABLE = new DataTable();
    DataTable GETRouteAmount = new DataTable();
    DataTable paidamount = new DataTable();
    int DateColumnCount = 4;
    int gridrowscount;
    string getdates;
    string getrouteid;
    string Convd;
    string Convm;
    string Convy;
    string FinalDate;
    double paidamt;
    double GETTEMPVALUE;
    double GETTOTBILLAMOUNT;
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
                    pnlContents.Visible = false;
                    if (roleid < 3)
                    {
                        loadsingleplant();

                    }

                    else
                    {

                        LoadPlantcode();
                        billdate();

                    }

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

    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("singleplant"));
            ddl_Plantname.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
            //   datasetcount = datasetcount + 1;
        }
        catch
        {
        }

    }
    public void loadsingleplant()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("allplant"));
            ddl_Plantname.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
            datasetcount = datasetcount + 1;
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
            string str;

            str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where  PLANT_CODE='"+ddl_Plantname.SelectedItem.Value+"'  AND  Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

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
    public void GETBILLDATE()
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
        }
        catch
        {

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

        TXT_SHOW.Text = ddl_Plantname.SelectedItem.Text +":" + "Plant All Level Details" + "For :" + ddl_BillDate.SelectedItem.Text;
        GETBILLDATE();
        totmilkdetails();
        totmilkbankandcash();
        bankdetails();
        excessdetails();
        loandetails();
        loanrecovery();
        cashreceipt();
        voucheradded();
        totdetuctions();
        dpupayment();
        pnlContents.Visible = true;
      
        //for (int times = 0; times < 500; times++)
        //{
        //    Thread.Sleep(1);
        //    Label29.Text = "HAI TEST";
        //    Thread.Sleep(1);
        //    Label29.Text = " ";

        //}


    }
    public void totmilkdetails()
    {

        string query = "";
        query = "Select  count(*) as sno,Plant_code as totplantcode,Sum(Smkg) as kg,Sum(Smltr) as ltr,Sum(NetAmount) as NetAmount,convert(decimal(18,2),(Sum(Sfatkg) * 100 /Sum(Smkg))) as Avgfat,convert(decimal(18,2),(Sum(SSnfkg) * 100 /Sum(Smkg))) as Avgsnf,convert(decimal(18,2),(Sum(NetAmount) /Sum(Smkg))) as AvgRate  from Paymentdata     where plant_code='"+ddl_Plantname.SelectedItem.Value+"' and  Frm_date='"+FDATE+"' and To_date='"+TODATE+"'    group by     plant_code";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable totmilk = new DataTable();
        da.Fill(totmilk);
        if (totmilk.Rows.Count > 0)
        {
            lbl_count.Text = totmilk.Rows[0][0].ToString();
            //string totplantcode = totmilk.Rows[0][1].ToString();
            txt_milkg.Text = totmilk.Rows[0][2].ToString();
            Label6.Text = totmilk.Rows[0][3].ToString();
            Label8.Text = totmilk.Rows[0][4].ToString();
            Label29.Text  = totmilk.Rows[0][5].ToString();
            Label12.Text = totmilk.Rows[0][6].ToString();
            Label31.Text = totmilk.Rows[0][7].ToString();
        }
        else
        {


        }

    }
    public void totmilkbankandcash()
    {

        string query = "";
        query = "sELECT  sno,NetAmount,ISNULL(CASHsno,0) AS CASHsno,ISNULL(CASHNetAmount,0) AS CASHNetAmount  FROM (Select  count(*) as sno,Plant_code,Sum(Smkg) as kg,Sum(Smltr) as ltr,Sum(NetAmount) as NetAmount,Payment_mode,convert(decimal(18,2),(Sum(Sfatkg) * 100 /Sum(Smkg))) as Avgfat,convert(decimal(18,2),(Sum(SSnfkg) * 100 /Sum(Smkg))) as Avgsnf,convert(decimal(18,2),(Sum(NetAmount) /Sum(Smkg))) as AvgRate  from Paymentdata     where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and  Frm_date='" + FDATE + "' and To_date='" + TODATE + "' and Payment_mode='BANK'   group by     plant_code ,Payment_mode) AS BANK LEFT JOIN (Select  count(*) as CASHsno,Plant_code,Sum(Smkg) as CASHkg,Sum(Smltr) as CASHltr,Sum(NetAmount) as CASHNetAmount,Payment_mode,convert(decimal(18,2),(Sum(Sfatkg) * 100 /Sum(Smkg))) as CASHAvgfat,convert(decimal(18,2),(Sum(SSnfkg) * 100 /Sum(Smkg))) as CASHAvgsnf,convert(decimal(18,2),(Sum(NetAmount) /Sum(Smkg))) as CASHAvgRate  from Paymentdata     where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and  Frm_date='" + FDATE + "' and To_date='" + TODATE + "' and Payment_mode='CASH'   group by     plant_code ,Payment_mode) AS CASH  ON BANK.Plant_code=CASH.Plant_code";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
          Label14.Text =   dtff.Rows[0][0].ToString();
          Label32.Text =  dtff.Rows[0][1].ToString();
          Label34.Text =   dtff.Rows[0][2].ToString();
          Label36.Text =    dtff.Rows[0][3].ToString();
           
        }
        else
        {


        }

    }
    public void bankdetails()
    {
        string query = "";
        query = "Select  convert(decimal(18,2),isnull((TotBankAmount),0)) as TotBankAmount, convert(decimal(18,2),isnull(TotPaidAmount,0)) as TotPaidAmount, convert(decimal(18,2),isnull(TotpendAmount,0)) as TotpendAmount,BankPlantCode from (Select * from (Select  Sum(NetAmount) as TotBankAmount,Plant_Code as BankPlantCode     from BankPaymentllotment   where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'    and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  group by Plant_Code) as totplant  left join (Select  Sum(NetAmount) as TotPaidAmount,Plant_Code as PiadPlantCode    from BankPaymentllotment   where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'    and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "' and  FinanceStatus=1 group by Plant_Code) as paid on totplant.BankPlantCode=paid.PiadPlantCode) as totandpaid  left join (Select    isnull(Sum(NetAmount),0) as TotpendAmount,Plant_Code as pendingPlantCode    from BankPaymentllotment   where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'    and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  and ((FinanceStatus is null) or (FinanceStatus=0))    group by Plant_Code  ) as pend on totandpaid.BankPlantCode=pend.pendingPlantCode";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
             
          Label18.Text =   dtff.Rows[0][0].ToString();
          Label20.Text =  dtff.Rows[0][1].ToString();
          Label38.Text =   dtff.Rows[0][2].ToString();
           
        }
        else
        {

        }
    }
    public void excessdetails()
    {
        string query = "";
        query = "Select count(*) as Sno,isnull(Sum(totAmount),0) as ExcessAmount  from AgentExcesAmount where Plant_code='"+ddl_Plantname.SelectedItem.Value+"'   and  Frm_date='"+FDATE+"' and To_date='"+TODATE+"'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
            Label22.Text = dtff.Rows[0][0].ToString();
            Label24.Text = dtff.Rows[0][1].ToString();
            
        }
        else
        {

        }
    }

    public void loandetails()
    {

        string query = "";
        query = "Select convert(decimal(18,2),isnull(Sum(loanamount),0)) as LoanAmount   from LoanDetails    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and loandate   between '" + FDATE + "' and '" + TODATE + "'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
            Label26.Text = dtff.Rows[0][0].ToString();
        }
        else
        {

        }
    }
      public void loanrecovery()
      {
        string query = "";
        query = "Select  convert(decimal(18,2),ISNULL(Sum(paid_amount),0)) as Paidamount  from Loan_Recovery    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and Paid_date  between '" + FDATE + "' and '" + TODATE + "'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
            Label40.Text = dtff.Rows[0][0].ToString();
        }
        else
        {

        }
    }
    public void cashreceipt()
    {
       
        string query = "";
        query = "Select  convert(decimal(18,2),isnull(Sum(LoanDueRecovery_Amount),0)) as Paidamount  from   LoanDue_Recovery   where plant_code='"+ddl_Plantname.SelectedItem.Value+"'   and LoanRecovery_Date  between '"+FDATE+"' and '"+TODATE+"'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
            Label41.Text = dtff.Rows[0][0].ToString();
        }
        else
        {

        }
    }
    public void voucheradded()
    {

        string query = "";
        query = "Select  isnull(Sum(Amount),0) as Paidamount   from   Voucher_Clear   where plant_code='"+ddl_Plantname.SelectedItem.Value+"'   and Clearing_Date  between '"+FDATE+"' and '"+TODATE+"'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
            Label43.Text = dtff.Rows[0][0].ToString();
        }
        else
        {

        }
    }

    public void totdetuctions() 
    {
        string query = "";
        query = "Select    isnull(Sum(Dm_Amount),0) as TotDeductAmount   from   DeductionDetails_Master   where Dm_Plantcode='"+ddl_Plantname.SelectedItem.Value+"'   and  Dm_FrmDate='"+FDATE+"' and Dm_ToDate='"+TODATE+"'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
            Label49.Text = dtff.Rows[0][0].ToString();
        }
        else
        {

        }
    }
    public void dpupayment()
    {

        string query = "";
        query = "sELECT  COUNT(*) AS sNO,CONVERT(DECIMAL(18,2),ISNULL(Sum(NetAmount),0)) as Amount from   DpuBankPaymentllotment   WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   AND Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtff = new DataTable();
        dtff.Rows.Clear();
        da.Fill(dtff);
        if (dtff.Rows.Count > 0)
        {
            Label45.Text = dtff.Rows[0][0].ToString();
            Label48.Text = dtff.Rows[0][1].ToString();
        }
        else
        {

        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
  
}