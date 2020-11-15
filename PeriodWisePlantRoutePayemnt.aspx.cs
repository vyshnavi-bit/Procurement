using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class PeriodWisePlantRoutePayemnt : System.Web.UI.Page
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
                    billdate();
                    if (roleid < 3)
                    {
                        loadsingleplant();

                    }

                    else
                    {

                        LoadPlantcode();

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
    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {
            getroutewisePayment();
            addgrid.Columns.Add("RouteId");
            addgrid.Columns.Add("RouteName");
            addgrid.Columns.Add("TotalBillAmount");
            getpaymentDate();
            foreach (DataRow DR in GETDATETABLE.Rows)
            {

                try
                {
                    string Date = DR[0].ToString();
                    addgrid.Columns.Add(Date);
                    DateColumnCount = DateColumnCount + 1;
                }
                catch
                {

                }

            }
            addgrid.Columns.Add("PayedAmount");
            addgrid.Columns.Add("BalanceAmount");
            foreach (DataRow Dts in DTG.Tables[datasetcount].Rows)
            {
                string RouteId = Dts[1].ToString();
                string RouteName = Dts[2].ToString();
                string Netamount = Dts[3].ToString();

                addgrid.Rows.Add(RouteId, RouteName, Netamount);
            }
            gridrowscount = addgrid.Rows.Count;
            GridView1.DataSource = addgrid;
            GridView1.DataBind();
            for (int i = 4; i <= DateColumnCount; i++)
            {
                getdates = GridView1.HeaderRow.Cells[i].Text;
                string[] CONVERDATE = getdates.Split('/');
                Convd = CONVERDATE[0];
                Convm = CONVERDATE[1];
                Convy = CONVERDATE[2];
                FinalDate = Convm + "/" + Convd + "/" + Convy;
                double temp = 0;
                GETTEMPVALUE = 0;
                double GETTOTPAIDAMOUNT = 0;
                double GETTOTTEMPVAL = 0;
                for (j = 0; j < gridrowscount; j++)
                {

                    getrouteid = GridView1.Rows[j].Cells[1].Text;
                    getdatepayment();
                    try
                    {
                        GETTOTBILLAMOUNT = Convert.ToDouble(GridView1.Rows[j].Cells[3].Text);
                    }
                    catch
                    {
                        GETTOTBILLAMOUNT = 0.0;

                    }
                    GETTOTTEMPVAL = GETTOTTEMPVAL + GETTOTBILLAMOUNT;
                    GridView1.FooterRow.Cells[3].Text = GETTOTTEMPVAL.ToString("F2");
                    GridView1.Rows[j].Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[3].Font.Bold = true;
                    temp = Convert.ToDouble(GETRouteAmount.Rows[0][0]);
                    GridView1.Rows[j].Cells[i].Text = temp.ToString("f2");
                    GETTEMPVALUE = GETTEMPVALUE + temp;
                    GridView1.FooterRow.Cells[i].Text = GETTEMPVALUE.ToString("F2");
                    GridView1.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[i].Font.Bold = true;
                    getoverroutetot();
                    double temp1 = Convert.ToDouble(paidamount.Rows[0][0].ToString());
                    GridView1.Rows[j].Cells[DateColumnCount].Text = temp1.ToString("F2");

                    GETTOTPAIDAMOUNT = GETTOTPAIDAMOUNT + temp1;
                    double GETTOTVAL = Convert.ToDouble(GridView1.Rows[j].Cells[3].Text);
                    GridView1.Rows[j].Cells[DateColumnCount + 1].Text = (GETTOTVAL - temp1).ToString("F2");
                    double GETPAY = (GETTOTVAL - temp1);
                    GridView1.FooterRow.Cells[DateColumnCount].Text = (GETTOTPAIDAMOUNT).ToString("F2");
                    GridView1.FooterRow.Cells[DateColumnCount].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[DateColumnCount].Font.Bold = true;
                    GridView1.Rows[j].Cells[DateColumnCount].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.Rows[j].Cells[DateColumnCount + 1].HorizontalAlign = HorizontalAlign.Right;

                }
                temp = 0;

            }
        }
        catch
        {

        }
    }
    public void getpaymentDate()
    {
        try
        {
            con.Close();
            con = DB.GetConnection();
            string str;
            GETBILLDATE();
           // str = "SELECT   CONVERT(VARCHAR,Added_Date,103) AS Date     FROM  BankPaymentllotment   WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Billfrmdate='" + FDATE + "'   AND  Billtodate='" + TODATE + "'   GROUP BY   Added_Date   ORDER BY convert(datetime,Added_Date,105)";
            str = "select *  from( select distinct(Date) as Date  from (SELECT   CONVERT(VARCHAR,Added_Date,103) AS Date     FROM  BankPaymentllotment   WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Billfrmdate='" + FDATE + "'   AND  Billtodate='" + TODATE + "' AND FinanceStatus='1'   GROUP BY   Added_Date   ) as gg ) as dd ORDER BY convert(datetime,Date,105)";
            SqlCommand CMD = new SqlCommand(str, con);
            SqlDataAdapter DSP = new SqlDataAdapter(CMD);
            DSP.Fill(GETDATETABLE);
        }
        catch
        {
        }

    }

    public void getoverroutetot()
    {

        try
        {
            con.Close();
            con = DB.GetConnection();
            string str;
            GETBILLDATE();
            string convertagent = getrouteid + "00";
            string convertagent1 = getrouteid + "99";
            paidamount.Rows.Clear();
            int agentstart = Convert.ToInt16(convertagent);
            int agentend = Convert.ToInt16(convertagent1);
            str = "SELECT  ISNULL(SUM(NetAmount),0) as NetAmount  FROM  BankPaymentllotment   WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   AND Billfrmdate='" + FDATE + "'   AND  Billtodate='" + TODATE + "'  AND FinanceStatus='1'  and Agent_Id between '" + agentstart + "' and '" + agentend + "'";
            SqlCommand CMD = new SqlCommand(str, con);
            SqlDataAdapter DSP = new SqlDataAdapter(CMD);
            DSP.Fill(paidamount);
        }
        catch
        {

        }
    }
    public void getroutewisePayment()
    {
        try
        {
            GETBILLDATE();
            con = DB.GetConnection();
            string stt = "Select payment.Plant_Code,payment.Route_id,Route_Name,NetAmount   from(select  Plant_code,Route_id,CONVERT(decimal(18,2),isnull(Sum(NetAmount),0)) as NetAmount    from  Paymentdata where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by   Plant_code,Route_id) as payment  left join (Select Plant_Code,Route_ID,Route_Name   from Route_Master  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' group by Plant_Code,Route_ID,Route_Name)as rm on payment.Plant_code=rm.Plant_Code and payment.Route_id=rm.Route_ID order by Route_id";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("getpaymentdetails"));
        }
        catch
        {

        }

    }
    public void getdatepayment()
    {
        try
        {
            con.Close();
            con = DB.GetConnection();
            string str;
            GETBILLDATE();
            string convertagent = getrouteid + "00";
            string convertagent1 = getrouteid + "99";
            GETRouteAmount.Rows.Clear();
            int agentstart = Convert.ToInt16(convertagent);
            int agentend = Convert.ToInt16(convertagent1);
            str = "SELECT  ISNULL(SUM(NetAmount),0) as NetAmount  FROM  BankPaymentllotment   WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   AND Billfrmdate='" + FDATE + "'   AND  Billtodate='" + TODATE + "'  AND Added_Date='" + FinalDate + "' AND  FinanceStatus='1'  and Agent_Id between '" + agentstart + "' and '" + agentend + "'";
            SqlCommand CMD = new SqlCommand(str, con);
            SqlDataAdapter DSP = new SqlDataAdapter(CMD);
            DSP.Fill(GETRouteAmount);
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
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    for (int k = 4; k <= DateColumnCount-2; k++)
        //    {
        //      double temple =    Convert.ToDouble(e.Row.Cells[DateColumnCount].Text);
        //      paidamt = paidamt + paidamt;

        //    }
        //    e.Row.Cells[DateColumnCount - 1].Text = paidamt.ToString("F2");

        //}

    }
    protected void GridView1_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Bill  Payment Details For Finance Bill periopd:" + ddl_BillDate.SelectedItem.Text + "Plant:" + ddl_Plantname.SelectedItem.Text;
                HeaderCell1.ColumnSpan = DateColumnCount + 2;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell1);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {


        }
    }
}