using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class PlantAndAmountallotementForFinance : System.Web.UI.Page
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
    int count;
    int sno = 1;
    int gridsno;
    string getplcode;
    double getgridview2footersum = 0;
    string PLANTGET;
    int CHECKPOINT = 0;
    int CELLCOUNT = 0;
    int CHECKPOINT1 = 0;
    string plantcodee;
    DataTable SETDATA = new DataTable();
    double GETPAIDTOT = 0;
    double GETBALANCE;
    double Temptot;
    int GETHEAD;
    int TempCol = 0;
    double getcowtotbala = 0;
    double getbufftotbala = 0;
    int COUNTSUBTOT;
    DataTable COUWTOTPAYMENT = new DataTable();
    DataTable COUWBUFFPAYMENT = new DataTable();
    DataTable COUWTOTPAYMENTPAID = new DataTable();
    DataTable COUWBUFFPAYMENTPAID = new DataTable();

    int GRIVIEWCOUNT = 0;//FOR DATACOLUMN VALUE
    int CHECKGR = 0;
    double GETCOWTOTDATECOLUMNVALUE = 0;
    double GETBUFFTOTDATECOLUMNVALUE = 0;
    int j;
    string GETPPNAME;

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
    string GETBANKFILE;
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
                    GridView1.Visible = false;
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

                GETGRIDVIEW();

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
            GETBILLDATE();
            con = DB.GetConnection();
            string GETBILLAMT = "select  floor(SUM(NetAmount)) as Amount   from    Paymentdata    where   Frm_date='" + FDATE + "' and To_date='" + TODATE + "'";
            SqlCommand CMD = new SqlCommand(GETBILLAMT, con);
            SqlDataAdapter DA = new SqlDataAdapter(CMD);
            DA.Fill(OVERALL, ("bILLaMOUNT"));
        }
        catch
        {
        }
    }

    public void GETBILLDATE()
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
    public void GETBILLDATACOW()
    {

        try
        {
            GETBILLDATE();
            con = DB.GetConnection();
            string GETBILLAMT = "select  floor(SUM(NetAmount)) as Amount   from    Paymentdata    where     Plant_code IN (155,156,159,161,163,164,158,162,171,172) AND      Frm_date='" + FDATE + "' and To_date='" + TODATE + "'";
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
            GETBILLDATE();
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
    public void GETPAYDATA()
    {
        try
        {
            GETBILLDATE();
            con = DB.GetConnection();
            string GETBILLAMT1 = "select    floor(SUM(NetAmount)) as Amount     from BankPaymentllotment   where   Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "'  AND FinanceStatus='1' ";
            SqlCommand CMD1 = new SqlCommand(GETBILLAMT1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(CMD1);
            DA1.Fill(OVERALL, ("bANKaMOUNT"));
        }
        catch
        {

        }
    }

    public void GETPAYDATACOW()
    {

        try
        {
            GETBILLDATE();
            con = DB.GetConnection();
            string GETBILLAMT1 = "select    ISNULL(floor(SUM(NetAmount)),0) as Amount     from BankPaymentllotment   where  Plant_code IN (155,156,159,161,163,164,158,162,171,172) AND  Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "'  AND FinanceStatus='1' ";
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
            GETBILLDATE();
            con = DB.GetConnection();
            string GETBILLAMT1 = "select    floor(SUM(NetAmount)) as Amount     from BankPaymentllotment   where   Plant_code IN (157,165,166,167,168,169) AND  Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "'  AND FinanceStatus='1' ";
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
            DTK.Columns.Add("Code");
            DTK.Columns.Add("Name");
            DTK.Columns.Add("BillAmount");
            getcolumnname();
            foreach (DataRow DRcol in OVERALL.Tables[count].Rows)
            {
                string getcolumn = DRcol[0].ToString();

                DTK.Columns.Add(getcolumn);
            }
            DTK.Columns.Add("PaidAmount");
            DTK.Columns.Add("Balance");

            count = count + 1;
            LoadPlantcodecow();
            foreach (DataRow Over in OVERALL.Tables[count].Rows)
            {

                string getpcode = Over[0].ToString();
                string getname = Over[1].ToString();
                DTK.Rows.Add(getpcode, getname);
                //GridView1.Rows[gridsno].Cells[0].Text= sno.ToString();

                gridsno = gridsno + 1;
            }
            LoadPlantcodebuff();

            DTK.Rows.Add("CowTotal");
            //GridView1.Rows[gridsno].Cells[0].Text = sno.ToString();
            count = count + 1;
            foreach (DataRow Over in OVERALL.Tables[count].Rows)
            {
                string getpcode = Over[0].ToString();
                string getname = Over[1].ToString();
                DTK.Rows.Add(getpcode, getname);

                sno = sno + 1;

            }
            DTK.Rows.Add("BuffTotal");
            GridView1.DataSource = DTK;
            GridView1.DataBind();
            if (DTK.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView2.Visible = false;
                GridView3.Visible = false;


            }
            else
            {
                GridView1.Visible = false;


            }

            GETHEAD = GridView1.HeaderRow.Cells.Count;
            Session["GETHEAD"] = GETHEAD;
            foreach (DataRow drf in DTK.Rows)
            {
                int COUNTROWS = 0;
                plantcodee = drf[0].ToString();
                if ((plantcodee != "CowTotal") && (plantcodee != "BuffTotal"))
                {

                    GETHEAD = GridView1.HeaderRow.Cells.Count;
                    for (int i = 4; i < GETHEAD - 2; i++)
                    {
                        //GridView1.Rows[gridsno].Cells[i].Text = GridView1.Columns[i].HeaderText;

                        ViewState["getdate"] = GridView1.HeaderRow.Cells[i].Text;
                        getbankdata();
                        if (CHECKPOINT1 == 0)
                        {
                            count = count + 1;
                        }
                        try
                        {
                            //   double GETDOU = Convert.ToDouble(SETDATA.Tables[count].Rows[COUNTROWS][0]);
                            double GETDOU = Convert.ToDouble(SETDATA.Rows[COUNTROWS][0]);
                            GridView1.Rows[CELLCOUNT].Cells[i].Text = GETDOU.ToString("F2");
                            GETPAIDTOT = GETPAIDTOT + GETDOU;
                            GridView1.Rows[CELLCOUNT].Cells[GETHEAD - 2].Text = GETPAIDTOT.ToString("F2");
                            double gettotamt = Convert.ToDouble(GridView1.Rows[CELLCOUNT].Cells[3].Text);
                            double getbal = (gettotamt - GETPAIDTOT);
                            GridView1.Rows[CELLCOUNT].Cells[GETHEAD - 1].Text = getbal.ToString("f2");


                        }
                        catch
                        {

                            GridView1.Rows[CELLCOUNT].Cells[i].Text = " ".ToString();

                        }

                        CHECKPOINT1 = CHECKPOINT1 + 1;

                    }
                    if (plantcodee != "CowTotal")
                    {



                    }


                }
                GETPAIDTOT = 0;
                CELLCOUNT = CELLCOUNT + 1;



                string GETCCODE = GridView1.Rows[COUNTSUBTOT].Cells[1].Text;
                if (GETCCODE == "CowTotal")
                {
                    LoadPlantcodecowTOTNET();
                    double gettemp1 = Convert.ToDouble(COUWTOTPAYMENT.Rows[0][0]);
                    GridView1.Rows[COUNTSUBTOT].Cells[3].Text = gettemp1.ToString("F2");
                    LoadPlantcodecowPAID();

                    double gettemp = Convert.ToDouble(COUWTOTPAYMENTPAID.Rows[0][0].ToString());
                    GridView1.Rows[COUNTSUBTOT].Cells[DTK.Columns.Count - 1].Text = gettemp.ToString("f2");

                    double billamt = Convert.ToDouble(COUWTOTPAYMENT.Rows[0][0].ToString());
                    double paidamt = Convert.ToDouble(COUWTOTPAYMENTPAID.Rows[0][0].ToString());
                    GridView1.Rows[COUNTSUBTOT].Cells[DTK.Columns.Count].Text = (billamt - paidamt).ToString("f2");


                }
                if (GETCCODE == "BuffTotal")
                {
                    LoadPlantcodeBUFFTOTNET();
                    double gettemp = Convert.ToDouble(COUWBUFFPAYMENT.Rows[0][0].ToString());
                    GridView1.Rows[COUNTSUBTOT].Cells[3].Text = gettemp.ToString("f2");

                    LoadPlantcodeBUFFPAID();

                    double gettemp1 = Convert.ToDouble(COUWBUFFPAYMENTPAID.Rows[0][0]);
                    GridView1.Rows[COUNTSUBTOT].Cells[DTK.Columns.Count - 1].Text = gettemp1.ToString("F2");


                    double billamt = Convert.ToDouble(COUWBUFFPAYMENT.Rows[0][0].ToString());
                    double paidamt = Convert.ToDouble(COUWBUFFPAYMENTPAID.Rows[0][0].ToString());
                    GridView1.Rows[COUNTSUBTOT].Cells[DTK.Columns.Count].Text = (billamt - paidamt).ToString("f2");



                }
                COUNTSUBTOT = COUNTSUBTOT + 1;

            }



            try
            {
                int SETGRIDNO = DTK.Columns.Count;
                int rowscount = DTK.Rows.Count;
                double GETcowBUFFtot;
                for (int i = 4; i < SETGRIDNO - 1; i++)
                {
                    //  string GETPPNAME = GridView1.Rows[GRIVIEWCOUNT].Cells[1].Text;

                    for (j = 0; j < rowscount; j++)
                    {
                        try
                        {
                            GETPPNAME = GridView1.Rows[j].Cells[1].Text;
                            GETcowBUFFtot = Convert.ToDouble(GridView1.Rows[j].Cells[i].Text);
                            GETCOWTOTDATECOLUMNVALUE = GETCOWTOTDATECOLUMNVALUE + GETcowBUFFtot;
                        }
                        catch
                        {
                            //   GETCOWTOTDATECOLUMNVALUE = 0;

                        }


                        if (GETPPNAME == "CowTotal")
                        {

                            GridView1.Rows[j].Cells[i].Text = GETCOWTOTDATECOLUMNVALUE.ToString("f2");
                            GETCOWTOTDATECOLUMNVALUE = 0;

                        }

                        //GETBUFFTOTDATECOLUMNVALUE = GETCOWTOTDATECOLUMNVALUE + GETcowtot;

                        if (GETPPNAME == "BuffTotal")
                        {

                            GridView1.Rows[j].Cells[i].Text = GETCOWTOTDATECOLUMNVALUE.ToString("f2");


                        }




                    }
                    j = 0;

                    GRIVIEWCOUNT = GRIVIEWCOUNT + 1;
                    GETCOWTOTDATECOLUMNVALUE = 0;
                }

            }
            catch
            {


            }

        }
        catch
        {


        }


    }


    public void getbankdata()
    {


        try
        {
           
            string date = ViewState["getdate"].ToString();
            string[] p = date.Split('/');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            //getvaldd = p[3];
            //getvalmm = p[4];
            //getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            //TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

            con = DB.GetConnection();
            string GETBILLAMT1 = "select    ISNULL(floor(SUM(NetAmount)),0) as Amount     from BankPaymentllotment where  plant_code='" + plantcodee + "' and     Added_Date between '" + FDATE + "' and   '" + FDATE + "'  AND FinanceStatus='1' ";
            SqlCommand CMD1 = new SqlCommand(GETBILLAMT1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(CMD1);
            SETDATA.Rows.Clear();
            DA1.Fill(SETDATA);

        }
        catch
        {

        }

    }
    public void LoadPlantcodecow()
    {
        GETBILLDATE();
        con = DB.GetConnection();
        string stt = "select  PM.Plant_Code,Plant_Name  from (SELECT plant_code   FROM Paymentdata  WHERE    Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  AND PLANT_CODE IN (155,156,158,159,161,162,163,164,171,172) group by Plant_Code) as pay left join (select Plant_Code,Plant_Name   from Plant_Master  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(OVERALL, ("COW"));


    }

    public void LoadPlantcodecowTOTNET()
    {
        GETBILLDATE();
        con = DB.GetConnection();
        string stt = "select  ISNULL(floor(SUM(NetAmount)),0) as Amount  from (SELECT  ISNULL(SUM(NETAMOUNT),0) AS NETAMOUNT   FROM Paymentdata  WHERE    Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  AND PLANT_CODE IN (155,156,158,159,161,162,163,164,171,172)) as pay";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);

        DA.Fill(COUWTOTPAYMENT);


    }

    public void LoadPlantcodecowPAID()
    {
        GETBILLDATE();
        con = DB.GetConnection();
        string stt = "select  ISNULL(floor(SUM(NetAmount)),0) as Amount  from (SELECT  ISNULL(SUM(NETAMOUNT),0) AS NETAMOUNT   FROM BankPaymentllotment  WHERE    Billfrmdate ='" + FDATE + "' and Billtodate ='" + TODATE + "'   AND FinanceStatus='1' AND PLANT_CODE IN (155,156,158,159,161,162,163,164,171,172)) as pay";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(COUWTOTPAYMENTPAID);
    }

    public void LoadPlantcodeBUFFTOTNET()
    {
        GETBILLDATE();
        con = DB.GetConnection();
        string stt = "select ISNULL(floor(SUM(NetAmount)),0) as Amount  from (SELECT  ISNULL(SUM(NETAMOUNT),0) AS NETAMOUNT   FROM Paymentdata  WHERE    Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  AND PLANT_CODE IN (157,165,166,167,168,169)) as pay";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);

        DA.Fill(COUWBUFFPAYMENT);


    }
    public void LoadPlantcodebuff()
    {

        con = DB.GetConnection();
        string stt = "select  PM.Plant_Code,Plant_Name  from (SELECT plant_code   FROM Paymentdata  WHERE    Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  AND  Plant_Code IN (157,165,166,167,168,169) group by Plant_Code) as pay left join (select Plant_Code,Plant_Name   from Plant_Master  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(OVERALL, ("BUFF"));


    }

    public void LoadPlantcodeBUFFPAID()
    {
        GETBILLDATE();
        con = DB.GetConnection();
        string stt = "select  ISNULL(floor(SUM(NetAmount)),0) as Amount  from (SELECT  ISNULL(SUM(NETAMOUNT),0) AS NETAMOUNT   FROM BankPaymentllotment  WHERE    Billfrmdate ='" + FDATE + "' and Billtodate ='" + TODATE + "'  AND FinanceStatus='1'  AND PLANT_CODE IN (157,165,166,167,168,169)) as pay";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);

        DA.Fill(COUWBUFFPAYMENTPAID);


    }

    public void getcolumnname()
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
            string gettingcol;
            con = DB.GetConnection();
            gettingcol = "select CONVERT(varchar,Added_Date,103) as date    from BankPaymentllotment where Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "' group by Added_Date   order by convert(datetime,Added_Date,101)";
            SqlCommand cmd2121 = new SqlCommand(gettingcol, con);
            SqlDataAdapter da2121 = new SqlDataAdapter(cmd2121);
            da2121.Fill(OVERALL, ("columnname"));
        }
        catch
        {

        }


    }
    public void getplantwisebillCOW()
    {

        try
        {
            GETBILLDATE();
            string getting;
            getting = "select Plant_Name,Amount,ISNULL(floor(paidAmount),0) AS paidAmount,(ISNULL(Amount,0)- ISNULL(paidAmount,0)) as Balance   from (select plant_name,totamt.Plant_code,totamt.Amount   from (select Plant_code,ISNULL(floor(SUM(NetAmount)),0) as Amount   from    Paymentdata    where  Plant_code IN (155,156,159,161,163,164,158,162,171,172)  AND  Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by Plant_code) as totamt left join (select Plant_Code,Plant_Name   from Plant_Master group by Plant_Code,Plant_Name) as pm on  totamt.Plant_code= pm.Plant_Code) as totpay left  join (select   ISNULL(floor(SUM(NetAmount)),0) as paidAmount,Plant_Code      from BankPaymentllotment   where   Plant_code IN (155,156,159,161,163,164,158,162,171,172) AND   Billfrmdate='" + FDATE + "' and   Billtodate='" + TODATE + "'  AND FinanceStatus='1' group by Plant_Code)as paid on totpay.Plant_code = paid.Plant_Code  order by totpay.Plant_code ASC   ";
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
            GETBILLDATE();
            string getting1;
            getting1 = "select  floor(SUM(NetAmount)) as Amount   from    Paymentdata    where     plant_code='" + PLANTGET + "'  AND  Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by Plant_code";
            SqlCommand cmd2121 = new SqlCommand(getting1, con);
            SqlDataAdapter da2121 = new SqlDataAdapter(cmd2121);
            da2121.Fill(OVERALL, ("allbANKaMOUNTBUFF"));

        }
        catch
        {


        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
      //  GridView1.Visible = false;
        GridView2.Visible = false;
        GridView3.Visible = false;
        GETGRIDVIEW();

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
                try
                {
                    if (CHECKPOINT == 0)
                    {
                        count = count + 1;
                    }


                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;

                    PLANTGET = e.Row.Cells[1].Text;
                    if ((PLANTGET == "CowTotal") || (PLANTGET == "BuffTotal"))
                    {
                        e.Row.BackColor = System.Drawing.Color.Cyan;
                        e.Row.Cells[0].Text = "";
                    }


                    //if ((PLANTGET != "CowTotal")  &&  (PLANTGET != "BuffTotal"))
                    //{
                    getplantwisebillBUFF();
                    string totpay = OVERALL.Tables[count].Rows[CHECKPOINT][0].ToString();
                    Temptot = Convert.ToDouble(totpay);

                    e.Row.Cells[3].Text = Temptot.ToString("f2");
                    getcowtotbala = getcowtotbala + Temptot;
                    getbufftotbala = getbufftotbala + Temptot;

                   
                    CHECKPOINT = CHECKPOINT + 1;
                }
                catch
                {


                }

            }

        }
        catch
        {


        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                TempCol = TempCol + 1;
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Bill  Payment Details For Finance Bill periopd:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell1.ColumnSpan = DTK.Columns.Count + 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell1);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
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

            // GETGRIDVIEW();

            getplantname = GridView1.SelectedRow.Cells[1].Text;
            //    getpcode();

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
            string getagent = "";
            getagent = "select  pm.Plant_Code,Plant_Name,Added_Date,BankFileName,CONVERT(DECIMAL(18,2),NetAmount) AS  NetAmount   from (SELECT plant_code,Added_Date,Floor(SUM(NetAmount)) as NetAmount,BankFileName    FROM BankPaymentllotment  WHERE Plant_Code='" + getplantname + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  AND FinanceStatus='1'  group by Plant_Code,Added_Date,BankFileName) as pay left join (select Plant_Code,Plant_Name   from Plant_Master  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code  order by Added_Date asc  ";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(getagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtf12 = new DataTable();
            da.Fill(dtf12);
            if (dtf12.Rows.Count > 0)
            {
                GridView2.Visible = true;
                GridView2.DataSource = dtf12;
                GridView2.DataBind();
                GridView2.FooterRow.Cells[4].Text = getgridview2footersum.ToString("f2");
                GridView2.FooterRow.HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Font.Bold = true;
                GridView1.Visible = true;

                GridView2.Visible = true;
                GridView3.Visible = false;

            }
            else
            {
                GridView2.Visible = false;
                GridView2.DataSource = null;
                GridView2.DataBind();

            }
        }
        catch
        {


        }
    }



    public void getgridview()
    {


    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }


    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string ppcode = GridView2.SelectedRow.Cells[2].Text;
            string bankfile = GridView2.SelectedRow.Cells[5].Text;

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
            string GET;
            GET = "SELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,NETAMOUNT,BANK_NAME    FROM (sELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,NETAMOUNT,BANK_ID   FROM  BankPaymentllotment    WHERE PLANT_CODE='" + ppcode + "'  AND   BANKFILENAME='" + bankfile + "'  AND  billfrmdate='" + FDATE + "'  and billtodate='" + TODATE + "'   AND FinanceStatus='1' ) AS BANK  LEFT JOIN (sELECT  BANK_NAME,BANK_ID,IFSC_CODE  FROM  BANK_MASTER    WHERE PLANT_CODE='" + ppcode + "'   GROUP BY  BANK_NAME,BANK_ID,IFSC_CODE    ) AS GG ON  BANK.BANK_ID=GG.BANK_ID AND BANK. IFSCCODE =GG.IFSC_CODE";
            SqlCommand cmd = new SqlCommand(GET, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tpr = new DataTable();
            da.Fill(tpr);


            if (tpr.Rows.Count > 0)
            {
                GridView3.DataSource = tpr;
                GridView3.DataBind();
                GridView3.Visible = true;
                GridView2.Visible = true;
            }
            else
            {
                GridView3.DataSource = null;
                GridView3.DataBind();
                GridView3.Visible = false;


            }
        }
        catch
        {

        }



    }
}