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
using System.IO;
using System.Text;


public partial class PlantLossorGain : System.Web.UI.Page
{


    BLLroutmaster routmasterBL = new BLLroutmaster();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public int rid;
    SqlDataReader dr;
    //Admin Check Flag
    public int Falg = 0;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DateTime dtm = new DateTime();
    DataTable dttt = new DataTable();
    DataTable dt = new DataTable();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    SqlDataAdapter da;
    public int Plantmilktype = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    public static int roleid;
    public static int buttonviewstatus;
    string STT1;
    int countgridrows;
    int countgridrows1;
    int countdeductname;
    string[] getstr;
    string[] getstr1;
    string[] aa;
    string getna1 = "";
    int i = 0;
    int J = 3;
    int JJ = 2;
    int JJJ = 2;
    int JK = 2;
    int II;
    string FF;
    string GETPLANT;
    string name;
    DataTable DTT = new DataTable();
    DataTable DTTK = new DataTable();
    string cc;
    string sqlsqlstr;
    double totprocmilk;
    double getval;
    double calc;
    double SUMFOOTER=0;
    double SUMFOOTER1 = 0;
    double gettingvalue;
    int JJH = 2;
    int KKJ = 0;
    int JJC = 2;
    double SUMVALUE;
    double SUMVALUE1;
    string DISPLAY;
    int FOOJJH=0;
    int FOOKKJ = 0;
    int FOOJJC=3;
    DataTable getsledgname=new DataTable();

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
                //	pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //	managmobNo = Session["managmobNo"].ToString();

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
      //  PROCMILK();
        PROCUREMENT();
        gridview1();
        Getrecoveryname();
      //  getgain(); 

    }
    public void PROCUREMENT()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlstr = "select TOP 3 subheadname AS ALLCC    from AccountsEntry  group by headername,subheadname";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(DTTK);
                foreach (DataRow dd in DTTK.Rows)
                {
                    DTTK.Rows.Clear();
                    for(int I=1;I<=3;I++)
                    {
           
                    if (I == 1)
                    {
                         cc = "ProcMilk";
                    }
                    if (I == 2)
                    {
                        cc = "Perdaymilk";
                    }
                    if (I == 3)
                    {
                        cc = "Proc Ts";
                    }
                    DTTK.Rows.Add(cc);
                                          

                    }
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    public void gridview1()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            //string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{

                con = dbaccess.GetConnection();
                string sqlstr = "select subheadname AS ALLCC    from AccountsEntry  group by headername,subheadname";

              //  string sqlstr = "Select   SubheadName   from  AcountsHeader group by  SubheadName";
                SqlCommand COM = new SqlCommand(sqlstr, con);
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    string stt = dr[0].ToString();
                    DTTK.Rows.Add(stt);

              }

            //}
        }
        catch (Exception ex)
        {

        }
    }



    public void Getrecoveryname()
    {
        try
        {

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                //	string str = "select distinct(name)  from (select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY date ORDER BY date desc) = 1   THEN Date ELSE NULL END AS 'Date',StockSubGroup as name,AMt  from (select (Fdate + '_'+ Tdate) as Date ,StockSubGroup,AMt from (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and Dm_FrmDate between '" + d1 + "' and '" + d2 + "'  group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate  ) as dm left join ( SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master group by StockGroupID,StockSubGroupID,StockSubGroup) AS SM ON  DM.Dm_StockGroupId=SM.StockGroupID AND DM.Dm_StockSubGroupId=SM.StockSubGroupID) as aa) as akl";
                string str = "select   plant_code,plant_name   from   plant_master  where plant_code > 154  and plant_code in (155,156,158,159,161,162,163,164,171)";
                SqlCommand COM = new SqlCommand(str, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable getdeducname = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(getdeducname);
                if (getdeducname.Rows.Count > 0)
                {
                    countdeductname = getdeducname.Rows.Count;
                    int l = 0;
                    foreach (DataRow row in getdeducname.Rows)
                    {
                        string value = row[1].ToString();
                        getstr = value.Split('-');
                        string getna = getstr[i];
                        if (getna == "ARANI")
                        {
                            getna = "ARANI";
                        }
                        if (getna == "KAVERIPATNAM")
                        {
                            getna = "KAVERI";
                        }
                        if (getna == "WALAJA")
                        {
                            getna = "WALAJA";
                        }
                        if (getna == "V_KOTA")
                        {
                            getna = "VKOTA";
                        }
                        if (getna == "RCPURAM")
                        {
                            getna = "RCPURAM";
                        }
                        if (getna == "V_KOTA")
                        {
                            getna = "VKOTA";
                        }
                        if (getna == "BOMMASAMUTHIRAM")
                        {

                            getna = "BOMMA";

                        }
                        if (getna == "TARIGONDA")
                        {

                            getna = "TARI";

                        }
                        if (getna == "KALASTHIRI")
                        {

                            getna = "KALASTHI";

                        }



                        string value1 = row[0].ToString();
                        getstr = value1.Split('-');
                        string getna1 = getstr[i];
                        DTTK.Columns.Add(getna);
                        DTTK.Columns.Add(getna1);
                        getna1 = getna1 + "/" + getna;
                        aa = getna1.Split('/');

                    }
                    string geting = "TotalCCPROCU";
                    DTTK.Columns.Add(geting);
                    GridView2.DataSource = DTTK;
                    GridView2.DataBind();
                    foreach (DataRow row in DTTK.Rows)
                    {
                        //int J = 2;
                            countgridrows = GridView2.HeaderRow.Cells.Count-2;
                           
                          //  countgridrows1 = countgridrows - 1;

                            for (int K = 2; K < countgridrows; K++)
                            {


                                GETPLANT = Convert.ToString(GridView2.HeaderRow.Cells[J].Text);
                                name = GridView2.Rows[II].Cells[1].Text;
                                getsapledgername();

                                GETHEADERNAME();
                                foreach (DataRow row1 in DTT.Rows)
                                {

                                    double AMMT = Convert.ToDouble(row1[0]);
                                    GridView2.Rows[II].Cells[JJ].Text = AMMT.ToString();


                                }


                                JJ = JJ + 2;
                                J = J + 2;
                                //if (JJ >= countgridrows)
                                //{

                                //    II = +1;

                                //}

                                K = K + 1;
                            }

                            II = II+1;
                            J = 3;
                            JJ = 2;
                   ///  II = +1;

                    }
                    foreach (DataRow row1 in DTTK.Rows)
                    {

                        int DTTKrows = DTTK.Rows.Count;
                         
                        II = 0;
                        for (int K = 0; K < DTTKrows; K++)
                        {

                            for (int L = 0; L < countgridrows; L++)
                            {

                                if (II == 0)
                                {

                                    totprocmilk = Convert.ToDouble(GridView2.Rows[II].Cells[JJJ].Text);

                                }
                                if (II >= 3)
                                {

                                    if (GridView2.Rows[II].Cells[JJJ].Text != "" && GridView2.Rows[II].Cells[JJJ].Text != null)
                                    {
                                        try
                                        {
                                            getval = Convert.ToDouble(GridView2.Rows[II].Cells[JJJ].Text);

                                            int IJK = II;

                                             
                                            //if (IJK >=3)
                                            //{
                                                                                               
                                            //    SUMFOOTER = SUMFOOTER + getval;

                                                

                                            //    GridView2.FooterRow.Cells[IJK].HorizontalAlign = HorizontalAlign.Right;
                                            //    GridView2.FooterRow.Cells[IJK].Text = SUMFOOTER.ToString("F2");
                                            //}

                                            //GridView2.FooterRow.Cells[JJJ].HorizontalAlign = HorizontalAlign.Right;
                                            //GridView2.FooterRow.Cells[JJJ].Text = SUMFOOTER.ToString("F2");
                                        }
                                        catch
                                        {

                                            getval = 0.00;

                                        }
                                        calc = getval / totprocmilk;
                                        string assign = calc.ToString("f2");
                                        GridView2.Rows[II].Cells[JJJ + 1].Text = assign.ToString();
                                        getval=0;
                                    }
                                }
                                getval = 0;
                            }
                            II = II + 1;
                        }
                        JJJ = JJJ+2;
                    }
                    //dt.Columns.Add(geting);
                    //GridView2.DataSource = dt;
                    //GridView2.DataBind();

                    //GridView2.FooterRow.Cells[countgridrows1].Text = sum3.ToString() + ".00";

                    //int jj = 2;
                    //for (int ii = 1; ii < aa.Length; ii++)
                    //{

                    //    item = aa[ii];


                    //    getfootertot();
                    //    GridView2.FooterRow.Cells[1].Text = "Total";
                    //    GridView2.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                    //    GridView2.FooterRow.Cells[jj].Text = var2;
                    //    GridView2.FooterRow.Cells[jj].HorizontalAlign = HorizontalAlign.Right;
                    //    jj = jj + 1;

                    //    // work with item here
                    //}

                    //for (int h = 2; h < countgridrows1; h++)
                    //{


                    //}
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();

                }
            }



        }


        catch (Exception ex)
        {

        //    catchmsg(ex.ToString());

        }



        foreach (DataRow row1 in DTTK.Rows)
        {
             JJH = DTTK.Rows.Count;

             //JJC=DTTK.Columns.Count;
            
             for (KKJ = 0; KKJ < JJH; KKJ++)
            {


               


                if (KKJ >= 3)
                {
                    try
                    {

                        SUMVALUE = Convert.ToDouble(GridView2.Rows[KKJ].Cells[JJC].Text);
                        SUMFOOTER = SUMVALUE + SUMFOOTER;

                        //SUMVALUE1 = Convert.ToDouble(GridView2.Rows[KKJ].Cells[JJC+1].Text);
                        //SUMFOOTER1 = SUMVALUE1 + SUMFOOTER1;


                       
                    }
                    catch
                    {
                        SUMVALUE = 0;
                        SUMVALUE1 = 0;
                    }


                    try
                    {
                        GridView2.FooterRow.Cells[JJC].HorizontalAlign = HorizontalAlign.Right;
                        GridView2.FooterRow.Cells[JJC].Text = SUMFOOTER.ToString("F2");
                        //if (JJC >= 16)
                        //{
                        //    GridView2.FooterRow.Cells[18 - 1].HorizontalAlign = HorizontalAlign.Right;
                        //    GridView2.FooterRow.Cells[18 - 1].Text = SUMFOOTER1.ToString("F2");
                        //}
                        //else
                        //{
                        //    GridView2.FooterRow.Cells[JJC + 1].HorizontalAlign = HorizontalAlign.Right;
                        //    GridView2.FooterRow.Cells[JJC + 1].Text = SUMFOOTER1.ToString("F2");

                        //}

                    }

                    catch
                    {


                    }
                }


            }
           
            SUMFOOTER = 0;
            SUMFOOTER1 = 0;
           
             KKJ = KKJ + 1;
             JJH = JJH + 1;
             JJC = JJC + 2;
        }

        JJC = JJC + 1;

        SUMFOOTER = 0;
        SUMFOOTER1 = 0;




        foreach (DataRow row1 in DTTK.Rows)
        {
            FOOJJH = DTTK.Rows.Count;

            //JJC=DTTK.Columns.Count;

            for (FOOKKJ = 0; FOOKKJ < FOOJJH; FOOKKJ++)
            {
                if (FOOKKJ >= 3)
                {
                    try
                    {


                        SUMVALUE1 = Convert.ToDouble(GridView2.Rows[FOOKKJ].Cells[FOOJJC].Text);


                        //SUMVALUE = Convert.ToDouble(GridView2.Rows[KKJ].Cells[JJC].Text);
                        SUMFOOTER1 = SUMVALUE1 + SUMFOOTER1;



                    }
                    catch
                    {

                        SUMVALUE1 = 0;
                    }


                    try
                    {


                        GridView2.FooterRow.Cells[FOOJJC].HorizontalAlign = HorizontalAlign.Right;
                        GridView2.FooterRow.Cells[FOOJJC].Text = SUMFOOTER1.ToString("F2");



                    }

                    catch
                    {


                    }
                }


            }


            SUMFOOTER1 = 0;
            FOOKKJ = FOOKKJ + 1;
            FOOJJH = FOOJJH + 1;
            FOOJJC = FOOJJC + 2;
        }



        SUMFOOTER1 = 0;




        foreach (DataRow row1 in DTTK.Rows)
        {
            countgridrows = GridView2.HeaderRow.Cells.Count;

            int kj = 0;
            for (int I = 0; I < countgridrows; I++)
            {

                if (I >= 2)
                {

                    string getplant = GridView2.HeaderRow.Cells[I].Text;


                    if (getplant == "ARANI")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "155")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "P/L Cost";
                    }
                    if (getplant == "KAVERI")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "156")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "P/L Cost"; ;
                    }
                    if (getplant == "WALAJA")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "158")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "P/L Cost";
                    }
                    if (getplant == "VKOTA")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "159")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "PerLtrCost";
                    }
                    if (getplant == "RCPURAM")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "161")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "P/L Cost";
                    }
                    if (getplant == "BOMMA")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "162")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "P/L Cost";
                    }
                    if (getplant == "TARI")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "163")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "P/L Cost";
                    }
                    if (getplant == "KALASTHI")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "Total";
                    }
                    if (getplant == "164")
                    {
                        GridView2.HeaderRow.Cells[I].Text = "P/L Cost";
                    }
                    

                }



            }



        }


    






      











    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       // string STT1;


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    int N = Convert.ToInt16(DTTK.Rows);
        //    for (int I = 1; I <=N; I++)
        //    {




        //    }



        //}

            //string STT = e.Row.Cells[1].Text.ToString();
          

            //if (STT == STT1)
            //{
                
            //        STT1 = STT.ToString();
            //}
            //    else
            //{
            //    if (STT1 != null)
            //    {
            //        GETHEADERNAME();
            //        dt.Rows.Add(dttt);
            //    }
            //    else
            //    {

            //        STT1 = STT.ToString();

            //    }

            //}

               


            //}
           

        



    }



    public void GETHEADERNAME()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                DTT.Columns.Clear();
                DTT.Rows.Clear();
                if (name == "ProcMilk")
                {
                    name = "ProcMilk";
                    sqlsqlstr = "SELECT  TOP 1 MLTR   FROM (SELECT SUM(TOTAMOUNT) AS TOTAMT,SUM(AVGFAT) AS AVGFAT,SUM(AVGSNF) AS AVGSNF,SUM(TOTRTS) AS TOTRTS,SUM(AVGRATE) AS AVGRATE,SUM(MLTR) AS MLTR,SUM(ASMKG) AS SMKG,CONVERT(DECIMAL(18,2),SUM((MLTR)/DAYS)) AS ASMLR,PCODE,DAYS,CONVERT(DECIMAL(18,2),SUM(AVGRATE/TOTRTS)) AS TS  FROM (select  SUM(tot) AS TOTAMOUNT,SUM(AVGFAT) AS AVGFAT,SUM(AVGSNF) AS AVGSNF,SUM(AVGFAT + AVGSNF) AS TOTRTS,CONVERT(DECIMAL(18,2),SUM(tot/MLTR)) AS AVGRATE,SUM(MLTR) AS MLTR,CONVERT(DECIMAL(18,2),SUM(MKG)) ASMKG,PCODE,(DAYS+1)  AS DAYS   from (SELECT sum(TOTAMOUNT) as tot,CONVERT(DECIMAL(18,2),sum(AVGFAT)) AS AVGFAT,CONVERT(DECIMAL(18,2),sum(AVGSNF)) AS AVGSNF,sum(MLTR) as MLTR,avg(mltr) as Avgmltr,avg(MKG) as MKG,PCODE,days   FROM (SELECT   SUM(AMT + COMRATE + SPLAMT + CARTTAGE) AS TOTAMOUNT,(( SUM(FAT_KG) * 100) /  SUM(mkg)) AS Avgfat,(( SUM(snf_KG) * 100) /  SUM(mkg)) AS AVGSNF,SUM(MLTR) AS MLTR,SUM(MKG) AS MKG,PCODE,days FROM  (SELECT   CONVERT(DECIMAL(18,2),SUM(AMOUNT)) AS AMT,CONVERT(DECIMAL(18,2),SUM(COMRATE)) AS COMRATE,CONVERT(DECIMAL(18,2),SUM(SPLBONUSAMOUNT)) AS SPLAMT,CONVERT(DECIMAL(18,2),SUM(CARTAGEAMOUNT)) as CARTTAGE ,CONVERT(DECIMAL(18,2),SUM(mILK_LTR)) AS MLTR,CONVERT(DECIMAL(18,2),SUM(mILK_KG)) AS Mkg,CONVERT(DECIMAL(18,2),SUM(FAT_KG)) AS FAT_KG,CONVERT(DECIMAL(18,2),SUM(snf_KG)) AS snf_KG,PLANT_CODE AS PCODE,DATEDIFF(Day, '"+d1+"', '"+d2+"') as Days  FROM  PROCUREMENT    WHERE  PRDATE BETWEEN  '" + d1 + "' AND '" + d2 + "'  AND PLANT_CODE='" + GETPLANT + "'    GROUP  BY PLANT_CODE) AS GG GROUP BY PCODE,days) AS CC  group by PCODE,days) as dddf GROUP BY PCODE,DAYS) AS GBG GROUP BY  PCODE,DAYS) AS GH  LEFT JOIN ( SELECT  HEADERNAME,sUBHEADNAME,PLANT_CODE, sum(AMOUNT) AS  AMOUNT   FROM   AccountsEntry     WHERE  Date BETWEEN '" + d1 + "' AND '" + d2 + "'  AND PLANT_CODE='" + GETPLANT + "'  GROUP BY HEADERNAME,sUBHEADNAME,PLANT_CODE) AS DF   ON  GH.PCODE=DF.PLANT_CODE  ORDER BY  GH.PCODE  ASC ";
                   
                }
                if (name == "Perdaymilk")
                {
                    name = "Perdaymilk";
                    sqlsqlstr = "";
                    sqlsqlstr = "SELECT  TOP 1 ASMLR   FROM (SELECT SUM(TOTAMOUNT) AS TOTAMT,SUM(AVGFAT) AS AVGFAT,SUM(AVGSNF) AS AVGSNF,SUM(TOTRTS) AS TOTRTS,SUM(AVGRATE) AS AVGRATE,SUM(MLTR) AS MLTR,SUM(ASMKG) AS SMKG,CONVERT(DECIMAL(18,2),SUM((MLTR)/DAYS)) AS ASMLR,PCODE,DAYS,CONVERT(DECIMAL(18,2),SUM(AVGRATE/TOTRTS)) AS TS  FROM (select  SUM(tot) AS TOTAMOUNT,SUM(AVGFAT) AS AVGFAT,SUM(AVGSNF) AS AVGSNF,SUM(AVGFAT + AVGSNF) AS TOTRTS,CONVERT(DECIMAL(18,2),SUM(tot/MLTR)) AS AVGRATE,SUM(MLTR) AS MLTR,CONVERT(DECIMAL(18,2),SUM(MKG)) ASMKG,PCODE,(DAYS+1)  AS DAYS   from (SELECT sum(TOTAMOUNT) as tot,CONVERT(DECIMAL(18,2),sum(AVGFAT)) AS AVGFAT,CONVERT(DECIMAL(18,2),sum(AVGSNF)) AS AVGSNF,sum(MLTR) as MLTR,avg(mltr) as Avgmltr,avg(MKG) as MKG,PCODE,days   FROM (SELECT   SUM(AMT + COMRATE + SPLAMT + CARTTAGE) AS TOTAMOUNT,(( SUM(FAT_KG) * 100) /  SUM(mkg)) AS Avgfat,(( SUM(snf_KG) * 100) /  SUM(mkg)) AS AVGSNF,SUM(MLTR) AS MLTR,SUM(MKG) AS MKG,PCODE,days FROM  (SELECT   CONVERT(DECIMAL(18,2),SUM(AMOUNT)) AS AMT,CONVERT(DECIMAL(18,2),SUM(COMRATE)) AS COMRATE,CONVERT(DECIMAL(18,2),SUM(SPLBONUSAMOUNT)) AS SPLAMT,CONVERT(DECIMAL(18,2),SUM(CARTAGEAMOUNT)) as CARTTAGE ,CONVERT(DECIMAL(18,2),SUM(mILK_LTR)) AS MLTR,CONVERT(DECIMAL(18,2),SUM(mILK_KG)) AS Mkg,CONVERT(DECIMAL(18,2),SUM(FAT_KG)) AS FAT_KG,CONVERT(DECIMAL(18,2),SUM(snf_KG)) AS snf_KG,PLANT_CODE AS PCODE,DATEDIFF(Day, '" + d1 + "', '" + d2 + "') as Days  FROM  PROCUREMENT    WHERE  PRDATE BETWEEN  '" + d1 + "' AND '" + d2 + "'  AND PLANT_CODE='" + GETPLANT + "'    GROUP  BY PLANT_CODE) AS GG GROUP BY PCODE,days) AS CC  group by PCODE,days) as dddf GROUP BY PCODE,DAYS) AS GBG GROUP BY  PCODE,DAYS) AS GH  LEFT JOIN ( SELECT  HEADERNAME,sUBHEADNAME,PLANT_CODE, sum(AMOUNT) AS  AMOUNT   FROM   AccountsEntry     WHERE  Date BETWEEN '" + d1 + "' AND '" + d2 + "'  AND PLANT_CODE='" + GETPLANT + "'  GROUP BY HEADERNAME,sUBHEADNAME,PLANT_CODE) AS DF   ON  GH.PCODE=DF.PLANT_CODE  ORDER BY  GH.PCODE  ASC ";
                   
                }
                if (name == "Proc Ts")
                {
                    name = "Proc Ts";
                    sqlsqlstr = "SELECT TOP 1 TS   FROM (SELECT SUM(TOTAMOUNT) AS TOTAMT,SUM(AVGFAT) AS AVGFAT,SUM(AVGSNF) AS AVGSNF,SUM(TOTRTS) AS TOTRTS,SUM(AVGRATE) AS AVGRATE,SUM(MLTR) AS MLTR,SUM(ASMKG) AS SMKG,CONVERT(DECIMAL(18,2),SUM((MLTR)/DAYS)) AS ASMLR,PCODE,DAYS,CONVERT(DECIMAL(18,2),SUM(AVGRATE/TOTRTS)) AS TS  FROM (select  SUM(tot) AS TOTAMOUNT,SUM(AVGFAT) AS AVGFAT,SUM(AVGSNF) AS AVGSNF,SUM(AVGFAT + AVGSNF) AS TOTRTS,CONVERT(DECIMAL(18,2),SUM(tot/MLTR)) AS AVGRATE,SUM(MLTR) AS MLTR,CONVERT(DECIMAL(18,2),SUM(MKG)) ASMKG,PCODE,(DAYS+1)  AS DAYS   from (SELECT sum(TOTAMOUNT) as tot,CONVERT(DECIMAL(18,2),sum(AVGFAT)) AS AVGFAT,CONVERT(DECIMAL(18,2),sum(AVGSNF)) AS AVGSNF,sum(MLTR) as MLTR,avg(mltr) as Avgmltr,avg(MKG) as MKG,PCODE,days   FROM (SELECT   SUM(AMT + COMRATE + SPLAMT + CARTTAGE) AS TOTAMOUNT,(( SUM(FAT_KG) * 100) /  SUM(mkg)) AS Avgfat,(( SUM(snf_KG) * 100) /  SUM(mkg)) AS AVGSNF,SUM(MLTR) AS MLTR,SUM(MKG) AS MKG,PCODE,days FROM  (SELECT   CONVERT(DECIMAL(18,2),SUM(AMOUNT)) AS AMT,CONVERT(DECIMAL(18,2),SUM(COMRATE)) AS COMRATE,CONVERT(DECIMAL(18,2),SUM(SPLBONUSAMOUNT)) AS SPLAMT,CONVERT(DECIMAL(18,2),SUM(CARTAGEAMOUNT)) as CARTTAGE ,CONVERT(DECIMAL(18,2),SUM(mILK_LTR)) AS MLTR,CONVERT(DECIMAL(18,2),SUM(mILK_KG)) AS Mkg,CONVERT(DECIMAL(18,2),SUM(FAT_KG)) AS FAT_KG,CONVERT(DECIMAL(18,2),SUM(snf_KG)) AS snf_KG,PLANT_CODE AS PCODE,DATEDIFF(Day, '" + d1 + "', '" + d2 + "') as Days  FROM  PROCUREMENT    WHERE  PRDATE BETWEEN  '" + d1 + "' AND '" + d2 + "'  AND PLANT_CODE='" + GETPLANT + "'    GROUP  BY PLANT_CODE) AS GG GROUP BY PCODE,days) AS CC  group by PCODE,days) as dddf GROUP BY PCODE,DAYS) AS GBG GROUP BY  PCODE,DAYS) AS GH  LEFT JOIN ( SELECT  HEADERNAME,sUBHEADNAME,PLANT_CODE, sum(AMOUNT) AS  AMOUNT   FROM   AccountsEntry     WHERE  Date BETWEEN '" + d1 + "' AND '" + d2 + "'  AND PLANT_CODE='" + GETPLANT + "'  GROUP BY HEADERNAME,sUBHEADNAME,PLANT_CODE) AS DF   ON  GH.PCODE=DF.PLANT_CODE  ORDER BY  GH.PCODE  ASC ";
                }
                if ((name != "Proc Ts") && (name != "Perdaymilk") && (name != "ProcMilk"))
                {
                    sqlsqlstr = "select  ISNULL(SUM(AMOUNT),0) AS AMOUNT    from AccountsEntry  WHere subheadname ='" + name + "' AND PLANT_CODE='" + GETPLANT + "'    AND PLANT_CODE IN (155,156,158,159,161,162,163,164) AND Date BETWEEN '" + d1 + "' AND '" + d2 + "' group by plant_code ";
                  //  sqlsqlstr = "Select  isnull(convert(decimal(18,2),Amount),0) as Amount     from (Select   Sno    from   accountheads   where BranchId='" + GETPLANT + "'  and   ledger_code='" + getsledgname.Rows[0][0].ToString() + "')as ff  left join (Select HeadSno,Sum(Amount) as Amount   from subpayable   where BranchId='" + GETPLANT + "' and paiddate  between  '" + d1 + "' and '" + d2 + "'  group by HeadSno )  as tt   on ff.Sno=tt.HeadSno";
                }
                SqlCommand COM = new SqlCommand(sqlsqlstr, conn);
                //DataTable DTT = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(DTT);
             

            }
        }
        catch (Exception ex)
        {



        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "CHILLING PLANT";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "ARANI";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "KAVERI";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "WALAJA";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "VKOTA";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "RCPURAM";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);


            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "BOMMA";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);


            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "TARIGONDA";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);


            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "KALAHASTI";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "TOTAL";
            HeaderCell2.ColumnSpan = 1;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);

            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    public void getsapledgername()
    {
        try{
        con = dbaccess.GetConnection();
        string strdel = "Select SubheadCode    from   AcountsHeader   SubheadName  where SubheadName='" + name + "'";
        SqlCommand cmd = new SqlCommand(strdel, con);
        SqlDataAdapter dswe=new SqlDataAdapter(cmd);
        getsledgname.Rows.Clear();
        dswe.Fill(getsledgname);
        }
        catch
        {

        }

    }
}