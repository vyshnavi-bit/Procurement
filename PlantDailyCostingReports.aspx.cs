using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class PlantDailyCostingReports : System.Web.UI.Page
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
    DataSet DTG = new DataSet();
    DataTable column = new DataTable();
    DataTable collect = new DataTable();
    DataTable ReadData = new DataTable();
    string fd;
    string fm;
    string fy;
    string td;
    string tm;
    string ty;
    string fdate;
    string Tdate;
    double gettransportltr;
    int k = 2;
    double getmilkltr;
    double getmilkkg;
    double Rts;
    double Fatgain;
    double Snfgain;
    double avgfat;
    double avgsnf;
    double FatCostLtrs;
    double SnfCostLtrs;
    double ltrcost;
    int count = 2;


    protected void Page_Load(object sender, EventArgs e)
    {
        
       
        if ((Session["Name"] != null) && (Session["pass"] != null))
        {
            if (IsPostBack != true)
            {
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                LoadPlantcode();
            }
        }
        else
        {

            Response.Redirect("LoginDefault.aspx");
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
            DA.Fill(DTG, ("plantname"));
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            con = DB.GetConnection();

            string str;
            con = DB.GetConnection();
          //  str = "select FrmDate + '_' + Todate as BillDate,Plant_Code  from (SELECT CONVERT(VARCHAR,Bill_frmdate,103) AS FrmDate,CONVERT(VARCHAR,Bill_todate,103) AS ToDate,Plant_Code   FROM Bill_date  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  AND  Bill_frmdate BETWEEN '" + d1 + "' AND '" + d2 + "' GROUP BY Bill_frmdate,Bill_todate,Plant_Code) as df";
            str = "Select convert(varchar,prdate,103) as  Date   from Procurementimport where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  AND  PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "'  group by Prdate  order by convert(datetime,prdate,101)";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable getcol = new DataTable();
            getcol.Rows.Clear();
            column.Rows.Clear();
            da.Fill(getcol);
            column.Columns.Add("Details");
            foreach (DataRow dr in getcol.Rows)
            {
                string get = dr[0].ToString();
                column.Columns.Add(get);
                count = count + 1;
            }
            column.Rows.Add("TotalKgs");
            column.Rows.Add("TotalLitres");
            column.Rows.Add("PerDayLitres");
            column.Rows.Add("LtrCost");
            column.Rows.Add("KgFat");
            column.Rows.Add("AvgFat");
            column.Rows.Add("KgSnf");
            column.Rows.Add("AvgSnf");
            column.Rows.Add("A.Fat+Snf=TS");
            column.Rows.Add("NilPayments");
            column.Rows.Add("MilkValueAmount");
            column.Rows.Add("LoanDeduction");
            column.Rows.Add("LoanPerLtrDeduction");
            column.Rows.Add("Deduction");
            column.Rows.Add("BillAdvanceDeduction");
            column.Rows.Add("NetBillAmount");
            column.Rows.Add("TransportAmount");
            column.Rows.Add("TransportLtrCost");
            column.Rows.Add("TotalSolidTS");
            column.Rows.Add("ProcureValueTSRate");
            column.Rows.Add("TransportTSRate");
            column.Rows.Add("ChillingCostperltr");
            column.Rows.Add("ChillingCostTSRate");
            column.Rows.Add("TotalTSRate");
            column.Rows.Add("KgmilkGain");
            column.Rows.Add("KgFatGain");
            column.Rows.Add("KgSnfGain");
            column.Rows.Add("TotalFat+SnfGain");
            column.Rows.Add("FatGain%PerLtr");
            column.Rows.Add("SnfGain%PerLtr");
            column.Rows.Add("TotalF&SGain%PerLtr");
            column.Rows.Add("AvgFat+GainFat %");
            column.Rows.Add("AvgSnf+GainSnf %");
            column.Rows.Add("TotalAvgFat+SnfGain%");
            column.Rows.Add("FatCost@175");
            column.Rows.Add("SnfCost@150");
            column.Rows.Add("TotalFat+Snf Cost");
            column.Rows.Add("FatCostLtrs");
            column.Rows.Add("SnfCostLtrs");
            column.Rows.Add("TotalFat+SnfCostLtr");
            column.Rows.Add("Fat&SnfPerTS");
            column.Rows.Add("TotalCost");
            column.Rows.Add("NetTSRate");
            GridView1.DataSource = column;
            GridView1.DataBind();
            int colcount = GridView1.HeaderRow.Cells.Count;
            int rowcount = GridView1.Rows.Count;
            for (int i = 2; i < colcount; i++)
            {
                string s = GridView1.HeaderRow.Cells[i].Text.ToString();
                string[] spldate = s.Split('/');
                fd = spldate[0];
                fm = spldate[1];
                string temfy = spldate[2];
                string[] temfy1 = temfy.Split('_');
                fy = temfy1[0].ToString();
                //td = temfy1[1].ToString();
                //tm = spldate[1];
                //ty = spldate[4];
                fdate = fm + "/" + fd + "/" + fy;
                //Tdate = tm + "/" + td + "/" + ty;
                GETDATAFROMSERVER();
                foreach (DataRow dfh in ReadData.Rows)
                {
                    GridView1.Rows[0].Cells[k].Text = dfh[0].ToString();//milkkg
                    getmilkkg = Convert.ToDouble(dfh[0]);
                    GridView1.Rows[1].Cells[k].Text = dfh[1].ToString();//mltr
                    getmilkltr = Convert.ToDouble(dfh[1]);
                    GridView1.Rows[2].Cells[k].Text = dfh[4].ToString();//permltr
                    ltrcost = Convert.ToDouble(dfh[3]);
                    GridView1.Rows[3].Cells[k].Text = dfh[3].ToString();//ltrcost
                    GridView1.Rows[4].Cells[k].Text = dfh[5].ToString();//kgfat
                    GridView1.Rows[5].Cells[k].Text = dfh[7].ToString();//avgfat
                    avgfat = Convert.ToDouble(dfh[7]);
                    GridView1.Rows[6].Cells[k].Text = dfh[6].ToString();//kgsnf
                    GridView1.Rows[7].Cells[k].Text = dfh[8].ToString();//avgsnf
                    avgsnf = Convert.ToDouble(dfh[8]);
                    GridView1.Rows[8].Cells[k].Text = dfh[9].ToString();//ts
                    try
                    {
                        GridView1.Rows[9].Cells[k].Text = dfh[34].ToString();//Nilpayment
                    }
                    catch
                    {
                        GridView1.Rows[9].Cells[k].Text = "0";
                    }
                    GridView1.Rows[10].Cells[k].Text = dfh[2].ToString();//Milkvalue
                    //GridView1.Rows[11].Cells[k].Text = dfh[10].ToString();//Loan
                    GridView1.Rows[11].Cells[k].Text = "0";
                    try
                    {
                        double Getloan = Convert.ToDouble(dfh[10]);
                        double Getloanperltr = Getloan / getmilkltr;
                        GridView1.Rows[12].Cells[k].Text = Getloanperltr.ToString("f2");//Loan Per Ltr Deduction
                    }
                    catch
                    {
                        //double Getloan = 1;
                        //double Getloanperltr = Getloan / getmilkltr;
                        GridView1.Rows[12].Cells[k].Text = "0";
                    }
                    GridView1.Rows[13].Cells[k].Text = "0";
                    GridView1.Rows[14].Cells[k].Text = "0";//billadvance
                    GridView1.Rows[15].Cells[k].Text = dfh[2].ToString();//Netamount
                    GridView1.Rows[16].Cells[k].Text = dfh[32].ToString();//Transportamt
                    double ts;
                    try
                    {
                        double transamt = Convert.ToDouble(dfh[32]);//Transportamt
                        GridView1.Rows[17].Cells[k].Text = (transamt / getmilkltr).ToString("f2");//Transport Ltr Cost
                        gettransportltr = Convert.ToDouble((transamt / getmilkltr));
                        GridView1.Rows[17].Cells[k].Text = gettransportltr.ToString("f2");
                    }
                    catch
                    {

                    }
                    GridView1.Rows[18].Cells[k].Text = dfh[9].ToString();//rts
                    ts = Convert.ToDouble(dfh[14]);
                    Rts = Convert.ToDouble(dfh[9]);
                    GridView1.Rows[19].Cells[k].Text = dfh[14].ToString();//Procure Value  TS Rate
                    GridView1.Rows[20].Cells[k].Text = (gettransportltr / Rts).ToString("f2");  //transport tsrate;
                    double ttranstsrate = (gettransportltr / Rts);
                    GridView1.Rows[21].Cells[k].Text = "1";//Chilling Cost
                    GridView1.Rows[22].Cells[k].Text = (1 / Rts).ToString("f2");//Chilling Cost TS Rate
                    double cillcost = (1 / Rts);
                    //string tottsrate = (ts + (ts / gettransportltr) + (1 / ts)).ToString();
                    string tottsrate = (ts + ttranstsrate + cillcost).ToString("f2");
                    GridView1.Rows[23].Cells[k].Text = tottsrate;// total tsrate
                    GridView1.Rows[24].Cells[k].Text = dfh[28].ToString();//gainmilkkg
                    GridView1.Rows[25].Cells[k].Text = dfh[29].ToString();//gainfatkg
                    GridView1.Rows[26].Cells[k].Text = dfh[30].ToString();//gainSnfkg
                    try
                    {
                        Fatgain = Convert.ToDouble(dfh[29]);
                        Snfgain = Convert.ToDouble(dfh[30]);
                        GridView1.Rows[27].Cells[k].Text = (Fatgain + Snfgain).ToString("f2");
                    }
                    catch
                    {
                    }
                    string tempFAT;
                    string tempsnf;
                    tempFAT = ((Fatgain / getmilkkg) * 100).ToString("F2");
                    tempsnf = ((Snfgain / getmilkkg) * 100).ToString("F2");
                    double fatgainpercent = Convert.ToDouble(tempFAT);
                    double snfgainpercent = Convert.ToDouble(tempsnf);
                    GridView1.Rows[28].Cells[k].Text = tempFAT;
                    GridView1.Rows[29].Cells[k].Text = tempsnf;
                    GridView1.Rows[30].Cells[k].Text = (fatgainpercent + snfgainpercent).ToString("f2");
                    GridView1.Rows[31].Cells[k].Text = (fatgainpercent + avgfat).ToString("f2");
                    GridView1.Rows[32].Cells[k].Text = (snfgainpercent + avgsnf).ToString("f2");
                    GridView1.Rows[33].Cells[k].Text = ((fatgainpercent + avgfat) + (snfgainpercent + avgsnf)).ToString();
                    GridView1.Rows[34].Cells[k].Text = (Fatgain * 175).ToString("f2");
                    FatCostLtrs = ((Fatgain * 175) / getmilkkg);
                    SnfCostLtrs = ((Snfgain * 150) / getmilkkg);
                    GridView1.Rows[35].Cells[k].Text = (Snfgain * 150).ToString("f2");
                    GridView1.Rows[36].Cells[k].Text = ((Fatgain * 175) + (Snfgain * 150)).ToString("f2");
                    GridView1.Rows[37].Cells[k].Text = ((Fatgain * 175) / getmilkkg).ToString("f2"); //Fat Cost Ltrs
                    GridView1.Rows[38].Cells[k].Text = ((Snfgain * 150) / getmilkkg).ToString("f2"); //Snf Cost Ltrs
                    GridView1.Rows[39].Cells[k].Text = (((Fatgain * 175) / getmilkkg) + ((Snfgain * 150) / getmilkkg)).ToString("f2"); //Total Fat + Snf Cost Ltr
                    GridView1.Rows[40].Cells[k].Text = ((FatCostLtrs + SnfCostLtrs) / Rts).ToString("f2"); //Fat & Snf Per TS
                    GridView1.Rows[41].Cells[k].Text = (ltrcost + gettransportltr + 1).ToString("f2"); //
                    double gettottsrate = Convert.ToDouble(tottsrate);
                    GridView1.Rows[42].Cells[k].Text = ((gettottsrate) - ((FatCostLtrs + SnfCostLtrs) / Rts)).ToString("f2");

                }

                k = k + 1;

            }

        }
        catch
        {

        }

    }
    public void GETDATAFROMSERVER()
    {
        try
        {
            //  string GetqueryData = "select *  from (select *  from (SELECT * FROM(select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,CONVERT(decimal(18,2),(Afat + Asnf)) as TS,LoanDeduction,convert(decimal(18,2),(PerDayLitres/LoanDeduction)) as LoanPerLtrDeduction,GFeed,GNetAmount, Convert(decimal(18,2),(LtrCost / CONVERT(decimal(18,2),(Afat + Asnf)))) as procTs,BillAdvance,Plant_code  from (select Smkg,Smltr,MilkValueAmount,convert(decimal(18,2),(MilkValueAmount/Smltr)) as LtrCost,convert(decimal(18,2),(Smltr/DIFFDATE)) as PerDayLitres,fatkg,Snfkg,CAST((((fatkg)*100)/(Smkg)) AS DECIMAL(18,2)) AS Afat,CONVERT(decimal(18,2),(snfkg * 100) /Smkg) as Asnf,LoanDeduction,GFeed,BillAdvance,GNetAmount,DIFFDATE,pay.Plant_code  from(select   isnull((CONVERT(decimal(18,2),Sum(smkg))),0) as Smkg,isnull((CONVERT(decimal(18,2),Sum(Smltr))),0) as Smltr, isnull(convert(decimal(18,2),( SUM(SAmt)+SUM(TotAdditions))),0)    AS MilkValueAmount,isnull((CONVERT(decimal(18,2),Sum(Sfatkg))),0) as fatkg,isnull((CONVERT(decimal(18,2),Sum(SSnfkg))),0) as Snfkg,SUM(SInstAmt) as LoanDeduction,SUM(Feed) AS GFeed,SUM(Billadv) as BillAdvance,SUM(FLOOR(NetAmount)) AS GNetAmount,Plant_code    from Paymentdata where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + fdate + "' and To_date='" + Tdate + "' group by Plant_code) as pay left join (select  top 1    DATEDIFF(DAY,'" + fdate + "','" + Tdate + "')+1 AS DIFFDATE,Plant_code     from Paymentdata where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + fdate + "' and To_date='" + Tdate + "' group by Plant_code) as daycount on pay.Plant_code=daycount.Plant_code) as ss  ) AS LEFFT LEFT JOIN( select opMilkKg,opFat_Kg,opSnf_Kg,promilkkg,profatkg,prosnfkg,DesMilkKg,despfatkg,despsnfkg,clMilkKg,clFat_Kg,clSnf_Kg ,(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg)) GainMilkkg, ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)) Gainfatkg,(  (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ) Gainsnfkg,opening from (select opMilkKg,clMilkKg,opFat_Kg,opSnf_Kg,clFat_Kg,clSnf_Kg,promilkkg,opening,profatkg,prosnfkg  from (select opMilkKg,clMilkKg,opFat_Kg,clFat_Kg,opSnf_Kg,clSnf_Kg,opening  from(SELECT   ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as opMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as opFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as opSnf_Kg,Plant_code as opening  FROM Stock_openingmilk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Datee='" + fdate + "' group by Plant_code) as op left join (SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as clMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as clFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as clSnf_Kg,Plant_code as closing   FROM Stock_Milk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Date='" + Tdate + "' group by Plant_code) as cl on op.opening=cl.closing ) as opcl left join (SELECT  ISNULL(CONVERT(decimal(18,2),Sum(Milk_kg)),0) as promilkkg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as profatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as prosnfkg,Plant_Code as propcode   FROM procurement  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Prdate between '" + fdate + "' and '" + Tdate + "'   group by Plant_Code) as pro on opcl.opening=pro.propcode) as opclpro left join(SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as DesMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as despfatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as despsnfkg,Plant_code as desplantcode  FROM Despatchnew  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Date between '" + fdate + "' and '" + Tdate + "'    group by Plant_code ) as des  on opclpro.opening=des.desplantcode) AS RIGHTT ON LEFFT.Plant_code=RIGHTT.opening) as allleft left join(SELECT Sum(ActualAmount) as Transamt,Plant_Code as Transpcode   FROM  Truck_Present    WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Pdate BETWEEN '" + fdate + "' AND '" + Tdate + "'   group by Plant_Code) as trans on allleft.Plant_code=trans.Transpcode) as asss left join (Select isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Nilmilkkg,Plant_Code as nilpcode   from Procurement    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate between '" + fdate + "' and '" + Tdate + "' and Rate < 1 group by Plant_Code )as nilcode on  asss.opening= nilcode.nilpcode";
        //    string GetqueryData = "select *  from (select *  from (SELECT * FROM(select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,CONVERT(decimal(18,2),(Afat + Asnf)) as TS,LoanDeduction,LoanDeduction AS NILL,GFeed,GNetAmount, Convert(decimal(18,2),(LtrCost / CONVERT(decimal(18,2),(Afat + Asnf)))) as procTs,BillAdvance,Plant_code  from (select Smkg,Smltr,MilkValueAmount,convert(decimal(18,2),(MilkValueAmount/Smltr)) as LtrCost,convert(decimal(18,2),(Smltr/DIFFDATE)) as PerDayLitres,fatkg,Snfkg,CAST((((fatkg)*100)/(Smkg)) AS DECIMAL(18,2)) AS Afat,CONVERT(decimal(18,2),(snfkg * 100) /Smkg) as Asnf,LoanDeduction,GFeed,BillAdvance,GNetAmount,DIFFDATE,pay.Plant_code  from(select   isnull((CONVERT(decimal(18,2),Sum(smkg))),0) as Smkg,isnull((CONVERT(decimal(18,2),Sum(Smltr))),0) as Smltr, isnull(convert(decimal(18,2),( SUM(SAmt)+SUM(TotAdditions))),0)    AS MilkValueAmount,isnull((CONVERT(decimal(18,2),Sum(Sfatkg))),0) as fatkg,isnull((CONVERT(decimal(18,2),Sum(SSnfkg))),0) as Snfkg,SUM(SInstAmt) as LoanDeduction,isnull(SUM(Ai+Feed+can+Recovery+others),0) AS GFeed,SUM(Billadv) as BillAdvance,SUM(FLOOR(NetAmount)) AS GNetAmount,Plant_code    from Paymentdata where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + fdate + "' and To_date='" + Tdate + "' group by Plant_code) as pay left join (select  top 1    DATEDIFF(DAY,'" + fdate + "','" + fdate + "')+1 AS DIFFDATE,Plant_code     from Paymentdata where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + fdate + "' and To_date='" + Tdate + "' group by Plant_code) as daycount on pay.Plant_code=daycount.Plant_code) as ss  ) AS LEFFT LEFT JOIN( select opMilkKg,opFat_Kg,opSnf_Kg,promilkkg,profatkg,prosnfkg,DesMilkKg,despfatkg,despsnfkg,clMilkKg,clFat_Kg,clSnf_Kg ,(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg)) GainMilkkg, ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)) Gainfatkg,(  (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ) Gainsnfkg,opening from (select opMilkKg,clMilkKg,opFat_Kg,opSnf_Kg,clFat_Kg,clSnf_Kg,promilkkg,opening,profatkg,prosnfkg  from (select opMilkKg,clMilkKg,opFat_Kg,clFat_Kg,opSnf_Kg,clSnf_Kg,opening  from(SELECT   ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as opMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as opFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as opSnf_Kg,Plant_code as opening  FROM Stock_openingmilk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Datee='" + fdate + "' group by Plant_code) as op left join (SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as clMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as clFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as clSnf_Kg,Plant_code as closing   FROM Stock_Milk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Date='" + Tdate + "' group by Plant_code) as cl on op.opening=cl.closing ) as opcl left join (SELECT  ISNULL(CONVERT(decimal(18,2),Sum(Milk_kg)),0) as promilkkg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as profatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as prosnfkg,Plant_Code as propcode   FROM procurement  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Prdate between '" + fdate + "' and '" + Tdate + "'   group by Plant_Code) as pro on opcl.opening=pro.propcode) as opclpro left join(SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as DesMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as despfatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as despsnfkg,Plant_code as desplantcode  FROM Despatchnew  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Date between '" + fdate + "' and '" + fdate + "'    group by Plant_code ) as des  on opclpro.opening=des.desplantcode) AS RIGHTT ON LEFFT.Plant_code=RIGHTT.opening) as allleft left join(SELECT Sum(ActualAmount) as Transamt,Plant_Code as Transpcode   FROM  Truck_Present    WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Pdate BETWEEN '" + fdate + "' AND '" + fdate + "'   group by Plant_Code) as trans on allleft.Plant_code=trans.Transpcode) as asss left join (Select isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Nilmilkkg,Plant_Code as nilpcode   from Procurement    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate between '" + fdate + "' and '" + fdate + "' and Rate < 1 group by Plant_Code )as nilcode on  asss.opening= nilcode.nilpcode";

        //    string GetqueryData = "select *  from (select *  from (SELECT * FROM(select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,CONVERT(decimal(18,2),(Afat + Asnf)) as TS,LoanDeduction,LoanDeduction AS NILL,GFeed,GNetAmount, Convert(decimal(18,2),(LtrCost / CONVERT(decimal(18,2),(Afat + Asnf)))) as procTs,BillAdvance,Plant_code  from (select Smkg,Smltr,MilkValueAmount,convert(decimal(18,2),(MilkValueAmount/Smltr)) as LtrCost,convert(decimal(18,2),(Smltr/DIFFDATE)) as PerDayLitres,fatkg,Snfkg,CAST((((fatkg)*100)/(Smkg)) AS DECIMAL(18,2)) AS Afat,CONVERT(decimal(18,2),(snfkg * 100) /Smkg) as Asnf,LoanDeduction,GFeed,BillAdvance,GNetAmount,DIFFDATE,pay.Plant_code  from(select   isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Smkg,isnull((CONVERT(decimal(18,2),Sum(Milk_ltr))),0) as Smltr, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS MilkValueAmount,isnull((CONVERT(decimal(18,2),Sum(fat_kg))),0) as fatkg,isnull((CONVERT(decimal(18,2),Sum(snf_kg))),0) as Snfkg,SUM(0) as LoanDeduction,isnull(SUM(0),0) AS GFeed,SUM(0) as BillAdvance, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS GNetAmount,Plant_code    from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and prdate='" + fdate + "' and Prdate='" + fdate + "' group by Plant_code) as pay left join (select  top 1     DATEDIFF(DAY,'" + fdate + "','" + fdate + "')+1 AS DIFFDATE,Plant_code     from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate='" + fdate + "' and Prdate='" + fdate + "' group by Plant_code) as daycount on pay.Plant_code=daycount.Plant_code) as ss  ) AS LEFFT LEFT JOIN( select opMilkKg,opFat_Kg,opSnf_Kg,promilkkg,profatkg,prosnfkg,DesMilkKg,despfatkg,despsnfkg,clMilkKg,clFat_Kg,clSnf_Kg ,(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg)) GainMilkkg, ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)) Gainfatkg,(  (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ) Gainsnfkg,opening from (select opMilkKg,clMilkKg,opFat_Kg,opSnf_Kg,clFat_Kg,clSnf_Kg,promilkkg,opening,profatkg,prosnfkg  from (select opMilkKg,clMilkKg,opFat_Kg,clFat_Kg,opSnf_Kg,clSnf_Kg,opening  from(SELECT   ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as opMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as opFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as opSnf_Kg,Plant_code as opening  FROM Stock_openingmilk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Datee='" + fdate + "' group by Plant_code) as op left join (SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as clMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as clFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as clSnf_Kg,Plant_code as closing   FROM Stock_Milk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Date='" + fdate + "' group by Plant_code) as cl on op.opening=cl.closing ) as opcl left join (SELECT  ISNULL(CONVERT(decimal(18,2),Sum(Milk_kg)),0) as promilkkg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as profatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as prosnfkg,Plant_Code as propcode   FROM procurement  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Prdate between '" + fdate + "' and '" + fdate + "'   group by Plant_Code) as pro on opcl.opening=pro.propcode) as opclpro left join(SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as DesMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as despfatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as despsnfkg,Plant_code as desplantcode  FROM Despatchnew WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Date between '" + fdate + "' and '" + fdate + "'    group by Plant_code ) as des  on opclpro.opening=des.desplantcode) AS RIGHTT ON LEFFT.Plant_code=RIGHTT.opening) as allleft left join(SELECT Sum(ActualAmount) as Transamt,Plant_Code as Transpcode   FROM  Truck_Present    WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Pdate BETWEEN '" + fdate + "' AND '" + fdate + "'    group by Plant_Code) as trans on allleft.Plant_code=trans.Transpcode) as asss left join (Select isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Nilmilkkg,Plant_Code as nilpcode   from Procurement    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate between '" + fdate + "' and '" + fdate + "' and Rate < 1 group by Plant_Code )as nilcode on  asss.opening= nilcode.nilpcode";
           // string GetqueryData = "select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,TS,LoanDeduction,GFeed,GNetAmount,procTs,BillAdvance,Plant_code,opMilkKg,opFat_Kg,opSnf_Kg,promilkkg,profatkg,prosnfkg,DesMilkKg,despfatkg,despsnfkg,clMilkKg,clFat_Kg,clSnf_Kg,ISNULL(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg),0)  AS GainMilkkg, ISNULL( ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)),0) AS  Gainfatkg,ISNULL(( (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ),0)  AS Gainsnfkg,ISNULL(opening,0) AS opening,Transamt,Transpcode,Nilmilkkg,nilpcode  from (select *  from (SELECT * FROM(select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,CONVERT(decimal(18,2),(Afat + Asnf)) as TS,LoanDeduction,GFeed,GNetAmount, Convert(decimal(18,2),(LtrCost / CONVERT(decimal(18,2),(Afat + Asnf)))) as procTs,BillAdvance,Plant_code  from (select Smkg,Smltr,MilkValueAmount,convert(decimal(18,2),(MilkValueAmount/Smltr)) as LtrCost,convert(decimal(18,2),(Smltr/DIFFDATE)) as PerDayLitres,fatkg,Snfkg,CAST((((fatkg)*100)/(Smkg)) AS DECIMAL(18,2)) AS Afat,CONVERT(decimal(18,2),(snfkg * 100) /Smkg) as Asnf,LoanDeduction,GFeed,BillAdvance,GNetAmount,DIFFDATE,pay.Plant_code  from(select   isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Smkg,isnull((CONVERT(decimal(18,2),Sum(Milk_ltr))),0) as Smltr, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS MilkValueAmount,isnull((CONVERT(decimal(18,2),Sum(fat_kg))),0) as fatkg,isnull((CONVERT(decimal(18,2),Sum(snf_kg))),0) as Snfkg,SUM(0) as LoanDeduction,SUM(0) AS GFeed,SUM(0) as BillAdvance, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS GNetAmount,Plant_code    from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and prdate='" + fdate + "' group by Plant_code) as pay left join (select top 1     DATEDIFF(DAY,'" + fdate + "','" + fdate + "')+1 AS DIFFDATE,Plant_code      from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate='" + fdate + "'  group by Plant_code) as daycount on pay.Plant_code=daycount.Plant_code) as ss  ) AS LEFFT LEFT JOIN( select ISNULL(opMilkKg,0) AS opMilkKg,ISNULL(opFat_Kg,0) AS opFat_Kg,ISNULL(opSnf_Kg,0) AS opSnf_Kg,ISNULL(promilkkg,0) AS promilkkg,ISNULL(profatkg,0) AS prosnfkg,ISNULL(DesMilkKg,0) AS  DesMilkKg,ISNULL(despfatkg,0) AS despfatkg,ISNULL(despsnfkg,0)  AS  despsnfkg,ISNULL(clMilkKg,0) AS clMilkKg,ISNULL(clFat_Kg,0) AS clFat_Kg,ISNULL(clSnf_Kg,0) AS clSnf_Kg,   ISNULL(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg),0)  AS GainMilkkg, ISNULL( ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)),0) AS  Gainfatkg,ISNULL(( (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ),0)  AS Gainsnfkg,ISNULL(opening,0) AS opening from (select opMilkKg,clMilkKg,opFat_Kg,opSnf_Kg,clFat_Kg,clSnf_Kg,promilkkg,opening,profatkg,prosnfkg  from (select opMilkKg,clMilkKg,opFat_Kg,clFat_Kg,opSnf_Kg,clSnf_Kg,opening  from(SELECT   ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as opMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as opFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as opSnf_Kg,Plant_code as opening  FROM Stock_openingmilk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Datee='" + fdate + "' group by Plant_code) as op left join (SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as clMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as clFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as clSnf_Kg,Plant_code as closing   FROM Stock_Milk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Date='" + fdate + "' group by Plant_code) as cl on op.opening=cl.closing ) as opcl left join (SELECT  ISNULL(CONVERT(decimal(18,2),Sum(Milk_kg)),0) as promilkkg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as profatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as prosnfkg,Plant_Code as propcode   FROM procurement  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Prdate='" + fdate + "'   group by Plant_Code) as pro on opcl.opening=pro.propcode) as opclpro left join(SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as DesMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as despfatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as despsnfkg,Plant_code as desplantcode  FROM Despatchnew WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Date='" + fdate + "'      group by Plant_code ) as des  on opclpro.opening=des.desplantcode) AS RIGHTT ON LEFFT.Plant_code=RIGHTT.opening) as allleft left join(SELECT Sum(ActualAmount) as Transamt,Plant_Code as Transpcode   FROM  Truck_Present     WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Pdate='" + fdate + "'     group by Plant_Code) as trans on allleft.Plant_code=trans.Transpcode) as asss left join (Select isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Nilmilkkg,Plant_Code as nilpcode   from Procurement  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate='" + fdate + "'  and Rate < 1 group by Plant_Code )as nilcode on  asss.opening= nilcode.nilpcode";

           // string GetqueryData = "select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,TS,LoanDeduction,GFeed,GNetAmount,procTs,BillAdvance,Plant_code,opMilkKg,opFat_Kg,opSnf_Kg,promilkkg,profatkg,prosnfkg,DesMilkKg,despfatkg,despsnfkg,clMilkKg,clFat_Kg,clSnf_Kg,ISNULL(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg),0)  AS GainMilkkg, ISNULL( ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)),0) AS  Gainfatkg,ISNULL(( (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ),0)  AS Gainsnfkg,ISNULL(opening,0) AS opening,Transamt,Transpcode,Nilmilkkg,nilpcode  from (select *  from (SELECT * FROM(select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,CONVERT(decimal(18,2),(Afat + Asnf)) as TS,LoanDeduction,GFeed,GNetAmount, Convert(decimal(18,2),(LtrCost / CONVERT(decimal(18,2),(Afat + Asnf)))) as procTs,BillAdvance,Plant_code  from (select Smkg,Smltr,MilkValueAmount,convert(decimal(18,2),(MilkValueAmount/Smltr)) as LtrCost,convert(decimal(18,2),(Smltr/DIFFDATE)) as PerDayLitres,fatkg,Snfkg,CAST((((fatkg)*100)/(Smkg)) AS DECIMAL(18,2)) AS Afat,CONVERT(decimal(18,2),(snfkg * 100) /Smkg) as Asnf,LoanDeduction,GFeed,BillAdvance,GNetAmount,DIFFDATE,pay.Plant_code  from (select *  from (SELECT * FROM(select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,CONVERT(decimal(18,2),(Afat + Asnf)) as TS,LoanDeduction,GFeed,GNetAmount, Convert(decimal(18,2),(LtrCost / CONVERT(decimal(18,2),(Afat + Asnf)))) as procTs,BillAdvance,Plant_code  from (select Smkg,Smltr,MilkValueAmount,convert(decimal(18,2),(MilkValueAmount/Smltr)) as LtrCost,convert(decimal(18,2),(Smltr/DIFFDATE)) as PerDayLitres,fatkg,Snfkg,CAST((((fatkg)*100)/(Smkg)) AS DECIMAL(18,2)) AS Afat,CONVERT(decimal(18,2),(snfkg * 100) /Smkg) as Asnf,LoanDeduction,GFeed,BillAdvance,GNetAmount,DIFFDATE,pay.Plant_code  from(select   isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Smkg,isnull((CONVERT(decimal(18,2),Sum(Milk_ltr))),0) as Smltr, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS MilkValueAmount,isnull((CONVERT(decimal(18,2),Sum(fat_kg))),0) as fatkg,isnull((CONVERT(decimal(18,2),Sum(snf_kg))),0) as Snfkg,SUM(0) as LoanDeduction,SUM(0) AS GFeed,SUM(0) as BillAdvance, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS GNetAmount,Plant_code    from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and prdate='" + fdate + "' group by Plant_code) as pay left join (select top 1     DATEDIFF(DAY,'" + fdate + "','" + fdate + "')+1 AS DIFFDATE,Plant_code      from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate='" + fdate + "'  group by Plant_code) as daycount on pay.Plant_code=daycount.Plant_code) as ss  ) AS LEFFT LEFT JOIN( select ISNULL(opMilkKg,0) AS opMilkKg,ISNULL(opFat_Kg,0) AS opFat_Kg,ISNULL(opSnf_Kg,0) AS opSnf_Kg,ISNULL(promilkkg,0) AS promilkkg,ISNULL(profatkg,0) AS profatkg,ISNULL(prosnfkg,0) AS prosnfkg,ISNULL(DesMilkKg,0) AS  DesMilkKg,ISNULL(despfatkg,0) AS despfatkg,ISNULL(despsnfkg,0)  AS  despsnfkg,ISNULL(clMilkKg,0) AS clMilkKg,ISNULL(clFat_Kg,0) AS clFat_Kg,ISNULL(clSnf_Kg,0) AS clSnf_Kg,   ISNULL(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg),0)  AS GainMilkkg, ISNULL( ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)),0) AS  Gainfatkg,ISNULL(( (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ),0)  AS Gainsnfkg,ISNULL(opening,0) AS opening from (select opMilkKg,clMilkKg,opFat_Kg,opSnf_Kg,clFat_Kg,clSnf_Kg,promilkkg,opening,profatkg,prosnfkg  from (select opMilkKg,clMilkKg,opFat_Kg,clFat_Kg,opSnf_Kg,clSnf_Kg,opening  from(SELECT   ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as opMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as opFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as opSnf_Kg,Plant_code as opening  FROM Stock_openingmilk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Datee='" + fdate + "' group by Plant_code) as op left join (SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as clMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as clFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as clSnf_Kg,Plant_code as closing   FROM Stock_Milk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Date='" + fdate + "' group by Plant_code) as cl on op.opening=cl.closing ) as opcl left join (SELECT  ISNULL(CONVERT(decimal(18,2),Sum(Milk_kg)),0) as promilkkg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as profatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as prosnfkg,Plant_Code as propcode   FROM procurement  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Prdate='" + fdate + "'   group by Plant_Code) as pro on opcl.opening=pro.propcode) as opclpro left join(SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as DesMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as despfatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as despsnfkg,Plant_code as desplantcode  FROM Despatchnew WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Date='" + fdate + "'      group by Plant_code ) as des  on opclpro.opening=des.desplantcode) AS RIGHTT ON LEFFT.Plant_code=RIGHTT.opening) as allleft left join(SELECT Sum(ActualAmount) as Transamt,Plant_Code as Transpcode   FROM  Truck_Present     WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Pdate='" + fdate + "'     group by Plant_Code) as trans on allleft.Plant_code=trans.Transpcode) as asss left join (Select isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Nilmilkkg,Plant_Code as nilpcode   from Procurement  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate='" + fdate + "'  and Rate < 1 group by Plant_Code )as nilcode on  asss.opening= nilcode.nilpcode";
            string GetqueryData = "select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,TS,LoanDeduction,GFeed,GNetAmount,procTs,BillAdvance,Plant_code,opMilkKg,opFat_Kg,opSnf_Kg,promilkkg,profatkg,prosnfkg,DesMilkKg,despfatkg,despsnfkg,clMilkKg,clFat_Kg,clSnf_Kg,ISNULL(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg),0)  AS GainMilkkg, ISNULL( ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)),0) AS  Gainfatkg,ISNULL(( (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ),0)  AS Gainsnfkg,ISNULL(opening,0) AS opening,Transamt,Transpcode,Nilmilkkg,nilpcode  from (select *  from (SELECT * FROM(select Smkg,Smltr,MilkValueAmount,LtrCost,PerDayLitres,fatkg,snfkg,Afat,Asnf,CONVERT(decimal(18,2),(Afat + Asnf)) as TS,LoanDeduction,GFeed,GNetAmount, Convert(decimal(18,2),(LtrCost / CONVERT(decimal(18,2),(Afat + Asnf)))) as procTs,BillAdvance,Plant_code  from (select Smkg,Smltr,MilkValueAmount,convert(decimal(18,2),(MilkValueAmount/Smltr)) as LtrCost,convert(decimal(18,2),(Smltr/DIFFDATE)) as PerDayLitres,fatkg,Snfkg,CAST((((fatkg)*100)/(Smkg)) AS DECIMAL(18,2)) AS Afat,CONVERT(decimal(18,2),(snfkg * 100) /Smkg) as Asnf,LoanDeduction,GFeed,BillAdvance,GNetAmount,DIFFDATE,pay.Plant_code  from(select   isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Smkg,isnull((CONVERT(decimal(18,2),Sum(Milk_ltr))),0) as Smltr, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS MilkValueAmount,isnull((CONVERT(decimal(18,2),Sum(fat_kg))),0) as fatkg,isnull((CONVERT(decimal(18,2),Sum(snf_kg))),0) as Snfkg,SUM(0) as LoanDeduction,SUM(0) AS GFeed,SUM(0) as BillAdvance, isnull(convert(decimal(18,2),( SUM(Amount)+SUM(ComRate+CartageAmount+SplBonusAmount))),0)    AS GNetAmount,Plant_code    from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and prdate='" + fdate + "' group by Plant_code) as pay left join (select top 1     DATEDIFF(DAY,'" + fdate + "','" + fdate + "')+1 AS DIFFDATE,Plant_code      from Procurement where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate='" + fdate + "'  group by Plant_code) as daycount on pay.Plant_code=daycount.Plant_code) as ss  ) AS LEFFT LEFT JOIN( select ISNULL(opMilkKg,0) AS opMilkKg,ISNULL(opFat_Kg,0) AS opFat_Kg,ISNULL(opSnf_Kg,0) AS opSnf_Kg,ISNULL(promilkkg,0) AS promilkkg,ISNULL(profatkg,0) AS profatkg,ISNULL(prosnfkg,0) AS prosnfkg,ISNULL(DesMilkKg,0) AS  DesMilkKg,ISNULL(despfatkg,0) AS despfatkg,ISNULL(despsnfkg,0)  AS  despsnfkg,ISNULL(clMilkKg,0) AS clMilkKg,ISNULL(clFat_Kg,0) AS clFat_Kg,ISNULL(clSnf_Kg,0) AS clSnf_Kg,   ISNULL(  (DesMilkKg + clMilkKg)-(promilkkg + opMilkKg),0)  AS GainMilkkg, ISNULL( ( (despfatkg + clFat_Kg)-(profatkg + opFat_Kg)),0) AS  Gainfatkg,ISNULL(( (despsnfkg + clSnf_Kg) -  (prosnfkg + opSnf_Kg)  ),0)  AS Gainsnfkg,ISNULL(opening,0) AS opening from (select opMilkKg,clMilkKg,opFat_Kg,opSnf_Kg,clFat_Kg,clSnf_Kg,promilkkg,opening,profatkg,prosnfkg  from (select opMilkKg,clMilkKg,opFat_Kg,clFat_Kg,opSnf_Kg,clSnf_Kg,opening  from(SELECT   ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as opMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as opFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as opSnf_Kg,Plant_code as opening  FROM Stock_openingmilk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Datee='" + fdate + "' group by Plant_code) as op left join (SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as clMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(Fat_Kg)),0) as clFat_Kg,ISNULL(CONVERT(decimal(18,2),Sum(Snf_Kg)),0) as clSnf_Kg,Plant_code as closing   FROM Stock_Milk  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Date='" + fdate + "' group by Plant_code) as cl on op.opening=cl.closing ) as opcl left join (SELECT  ISNULL(CONVERT(decimal(18,2),Sum(Milk_kg)),0) as promilkkg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as profatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as prosnfkg,Plant_Code as propcode   FROM procurement  WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Prdate='" + fdate + "'   group by Plant_Code) as pro on opcl.opening=pro.propcode) as opclpro left join(SELECT    ISNULL(CONVERT(decimal(18,2),Sum(MilkKg)),0) as DesMilkKg,ISNULL(CONVERT(decimal(18,2),Sum(fat_kg)),0) as despfatkg,ISNULL(CONVERT(decimal(18,2),Sum(snf_kg)),0) as despsnfkg,Plant_code as desplantcode  FROM Despatchnew WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  Date='" + fdate + "'      group by Plant_code ) as des  on opclpro.opening=des.desplantcode) AS RIGHTT ON LEFFT.Plant_code=RIGHTT.opening) as allleft left join(SELECT Sum(ActualAmount) as Transamt,Plant_Code as Transpcode   FROM  Truck_Present     WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Pdate='" + fdate + "'     group by Plant_Code) as trans on allleft.Plant_code=trans.Transpcode) as asss left join (Select isnull((CONVERT(decimal(18,2),Sum(Milk_kg))),0) as Nilmilkkg,Plant_Code as nilpcode   from Procurement  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' and Prdate='" + fdate + "'  and Rate < 1 group by Plant_Code )as nilcode on  asss.opening= nilcode.nilpcode";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(GetqueryData, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ReadData.Rows.Clear();
            ReadData.Columns.Clear();
            da.Fill(ReadData);
        }
        catch
        {
        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].Font.Bold = true;
            string name = e.Row.Cells[1].Text;

            if ((name == "TotalKgs") || (name == "LtrCost") || (name == "AvgFat") || (name == "AvgSnf") || (name == "TransportAmount") || (name == "KgmilkGain") || (name == "KgFatGain") || (name == "KgSnfGain") || (name == "NetTSRate") || (name == "NetBillAmount"))
            {
                //e.Row.BackColor = System.Drawing.Color.Cyan;
                //e.Row.ForeColor = System.Drawing.Color.DeepPink;
                //e.Row.ForeColor = System.Drawing.Color.DeepPink;



                //e.Row.Cells[2].Text = "<blink>" + e.Row.Cells[0].Text.ToString() + "</blink>";
                //e.Row.Cells[2].ForeColor = Color.Red;
                //e.Row.BackColor = System.Drawing.Color.Honeydew;
            }
            else
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "Call", "CallBlink('" + this.GridView1.Rows[1].Cells[0].ClientID + "');", true);
                //e.Row.Cells[2].Text = "<blink>" + e.Row.Cells[0].Text.ToString() + "</blink>";
                //e.Row.Cells[2].ForeColor = Color.Red;
                //e.Row.BackColor = System.Drawing.Color.Yellow;

                //  Label lbl = e.Row.FindControl("sno") as Label;
                //try
                //{
                //    Label lbl = new Label();
                //    lbl.Text = e.Row.Cells[1].Text;
                //    if (lbl.Text=="KgFat")
                //    {
                //        lbl.ForeColor = Color.Green;
                //    }
                //    else
                //    {
                //        lbl.ForeColor = Color.Red;
                //        lbl.CssClass = "blinkytext";
                //    }
                //}
                //catch
                //{

                //}

            }


        }
    }
    protected void Button9_Click(object sender, EventArgs e)
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
                HeaderCell2.Text = "OverAllReport  For plant:" + ddl_Plantname.SelectedItem.Text + "          DateFrom " + txt_FromDate.Text + "To:" + txt_ToDate.Text;
                HeaderCell2.ColumnSpan = count;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {


        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Random rand = new Random();
        int A = rand.Next(0, 255);
        int R = rand.Next(0, 255);
        int G = rand.Next(0, 255);
        int B = rand.Next(0, 255);
        Label1.ForeColor = Color.FromArgb(A, R, G, B);
    }
}