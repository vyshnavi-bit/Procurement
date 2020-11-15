using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
public partial class TotalMonthlyBillAmount : System.Web.UI.Page
{
    SqlCommand cmd;
    string Branchid = "";
    SalesDBManager vdm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            Branchid = Session["branch"].ToString();
            vdm = new SalesDBManager();
            if (!Page.IsPostBack)
            {
                if (!Page.IsCallback)
                {
                    // dtp_FromDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");

                    lblAddress.Text = Session["Address"].ToString();
                    lblTitle.Text = Session["TitleName"].ToString();
                    bindbranches();
                }
            }
        }
    }
    private DateTime GetLowDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        DT = dt;
        Hour = -dt.Hour;
        Min = -dt.Minute;
        Sec = -dt.Second;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }



    void bindbranches()
    {
        vdm = new SalesDBManager();
        if (Session["LevelType"].ToString() == "2" || Session["LevelType"].ToString() == "1")
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE (Plant_Code = @BranchID)");
            cmd.Parameters.Add("@BranchID", Session["branch"].ToString());
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlplantname.DataSource = dtPlant;
            ddlplantname.DataTextField = "Plant_Name";
            ddlplantname.DataValueField = "Plant_Code";
            ddlplantname.DataBind();
            ddlplantname.ClearSelection();
            ddlplantname.Items.Insert(0, new ListItem { Value = "0", Text = "--Select Agent--", Selected = true });
            ddlplantname.SelectedValue = "0";
        }
        else
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master ");
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlplantname.DataSource = dtPlant;
            ddlplantname.DataTextField = "Plant_Name";
            ddlplantname.DataValueField = "Plant_Code";
            ddlplantname.DataBind();
            ddlplantname.ClearSelection();
            ddlplantname.Items.Insert(0, new ListItem { Value = "0", Text = "--Select Agent--", Selected = true });
            ddlplantname.SelectedValue = "0";
        }
    }
    DataTable Report = new DataTable();
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        Report.Columns.Add("Details");
        try
        {
            string mymonth = ddlmonth.SelectedItem.Value;
            string year = ddlyear.SelectedItem.Value;
            string fromdate; string todate;
            if (mymonth != "00")
            {
                int Year = 0;
                int.TryParse(year, out Year);
                int Month = 0;
                int.TryParse(mymonth, out Month);
                int days = DateTime.DaysInMonth(Year, Month);

                fromdate = mymonth + "/" + 1 + "/" + year;
                todate = mymonth + "/" + days + "/" + year;

                int plantcode = Convert.ToInt32(ddlplantname.SelectedItem.Value);
                int plancode1 = plantcode;
                int newrow = 2;
                int count = 0;
                if (plantcode == 155 || plantcode == 156 || plantcode == 159)
                {
                    DateTime todte = Convert.ToDateTime(todate);
                    todte = todte.AddDays(1);
                    todate = todte.ToString("MM/dd/yyyy");
                }
                //  DateTime FROMdate = fromdate.DateTime.Now();
                if (plantcode != 0)
                {
                    cmd = new SqlCommand("SELECT  Bill_frmdate, Bill_todate FROM Bill_date WHERE (Bill_frmdate BETWEEN @d1 AND @d2) AND (Bill_todate BETWEEN @d1 AND @d2) AND Plant_Code=@Plant_Code  order by  Bill_frmdate");
                    cmd.Parameters.Add("@d1", fromdate);
                    cmd.Parameters.Add("@d2", todate);
                    cmd.Parameters.Add("@Plant_Code", plantcode);
                }
                else
                {
                    cmd = new SqlCommand("SELECT Plant_Code FROM Bill_date WHERE (Bill_frmdate BETWEEN @d1 AND @d2) AND (Bill_todate BETWEEN @d1 AND @d2)  GROUP BY Plant_Code");
                    cmd.Parameters.Add("@d1", fromdate);
                    cmd.Parameters.Add("@d2", todate);
                }
                DataTable dtbilldate = vdm.SelectQuery(cmd).Tables[0];
                if (plantcode != 0)
                {
                    foreach (DataRow dr in dtbilldate.Rows)
                    {

                        string billfrodate = dr["Bill_frmdate"].ToString();
                        string billtodate = dr["Bill_todate"].ToString();
                        Report.Columns.Add(billfrodate + "To" + billtodate);
                    }
                }
                else
                {
                    foreach (DataRow dr in dtbilldate.Rows)
                    {
                        string plantCode = dr["Plant_Code"].ToString();
                        Report.Columns.Add(plantCode);
                    }
                }
                if (dtbilldate.Rows.Count > 0)
                {
                    foreach (DataRow drr in dtbilldate.Rows)
                    {

                        if (plantcode != 0)
                        {
                            cmd = new SqlCommand("SELECT  CAST(ISNULL(summilkkg, 0) AS DECIMAL(18, 2)) AS TotalKgs, CAST(ISNULL(milkltrs, 0) AS DECIMAL(18, 2)) AS TotalLiters, CAST(ISNULL(perdayliters, 0) AS DECIMAL(18, 2)) AS PerDayLtr, CAST(ISNULL(LtrCost, 0) AS DECIMAL(18, 2)) AS LtrCost, CAST(ISNULL(fatkg, 0) AS DECIMAL(18, 2)) AS KgFat,CAST(ISNULL(AvgFat, 0) AS DECIMAL(18, 2)) AS AvgFat,CAST(ISNULL(Snfkg, 0) AS DECIMAL(18, 2)) AS KgSnf,CAST(ISNULL(avgsnf, 0) AS DECIMAL(18, 2)) AS AvgSnf,CAST(ISNULL(TS, 0) AS DECIMAL(18, 2)) AS TS,CAST(ISNULL(AMOUNT, 0) AS DECIMAL(18, 2)) AS MilkValueAmount,CAST(ISNULL(LoanDeduction, 0) AS DECIMAL(18, 2)) AS LoanDeduction,CAST(ISNULL(LoanPerltrDeduction, 0) AS DECIMAL(18, 2)) AS LoanPerltrDeduction,CAST(ISNULL(feed, 0) AS DECIMAL(18, 2)) AS FeedDeduction,CAST(ISNULL(netamount, 0) AS DECIMAL(18, 2)) AS NetBillAmount,CAST(ISNULL(Tamt, 0) AS DECIMAL(18, 2)) AS TransportAmount,CAST(ISNULL(TransportLtrCost, 0) AS DECIMAL(18, 2)) AS TransportLtrCost,CAST(ISNULL(ProcureValueTSRate, 0) AS DECIMAL(18, 2)) AS ProcureValueTSRate,CAST(ISNULL(TransportTSRate, 0) AS DECIMAL(18, 2)) AS TransportTSRate,CAST(ISNULL(1, 0) AS DECIMAL(18, 2)) AS ChillingCostPerLtr,CAST(ISNULL(ChillingCostTsRate, 0) AS DECIMAL(18, 2)) AS ChillingCostTsRate,CAST(ISNULL(TotTsRate, 0) AS DECIMAL(18, 2)) AS TotTsRate,CAST(ISNULL(Gainmilkkg, 0) AS DECIMAL(18, 2)) AS KgMilkGain,CAST(ISNULL(GainFatkg, 0) AS DECIMAL(18, 2)) AS KgFatGain,CAST(ISNULL(GainSnfkg, 0) AS DECIMAL(18, 2)) AS kgSnfGain, CAST(ISNULL(TOtfatSnfgain, 0) AS DECIMAL(18, 2)) AS TotalFatandSnfGain,CAST(ISNULL(FatGainPerltr, 0) AS DECIMAL(18, 2)) AS FatGainPerltr,CAST(ISNULL(SnfGainPerltr, 0) AS DECIMAL(18, 2)) AS SnfGainPerltr,CAST(ISNULL(TotalFAndSGainpercentLtr, 0) AS DECIMAL(18, 2)) AS TotalFAndSGainpercentLtr,CAST(ISNULL(AvgFatWithGainFatperltr, 0) AS DECIMAL(18, 2)) AS AvgFatWithGainFatperltr,CAST(ISNULL(AvgSnfWithGainSnfperltr, 0) AS DECIMAL(18, 2)) AS AvgSnfWithGainSnfperltr,CAST(ISNULL(TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr, 0) AS DECIMAL(18, 2)) AS TotAvgSnfANDFatPerltr,CAST(ISNULL(FatCost, 0) AS DECIMAL(18, 2)) AS FatCost@175,CAST(ISNULL(SnfCost, 0) AS DECIMAL(18, 2)) AS SnfCost@150,CAST(ISNULL(TotFatCostAndSnfCost, 0) AS DECIMAL(18, 2)) AS TotFatCostAndSnfCost,CAST(ISNULL(FatCostLtr, 0) AS DECIMAL(18, 2)) AS FatCostLtr,CAST(ISNULL(SnfCostLtr, 0) AS DECIMAL(18, 2)) AS SnfCostLtr,CAST(ISNULL(TotFatAndSnfCostLtr, 0) AS DECIMAL(18, 2)) AS TotFatAndSnfCostLtr,CAST(ISNULL(FatANdSnfPerLtrTS, 0) AS DECIMAL(18, 2)) AS FatANDSnfPerTS,CAST(ISNULL(TotalCost, 0) AS DECIMAL(18, 2)) AS TotalCost,CAST(ISNULL(NETTsRate, 0) AS DECIMAL(18, 2)) AS NetTsRate   FROM (SELECT table4.Plant_code, table4.Plant_name, table4.summilkkg, table4.milkltrs, table4.perdayliters, table4.fatkg, table4.AvgFat, table4.avgsnf, table4.Snfkg,table4.TS, table4.CLR, table4.LtrCost, table4.LoanDeduction, table4.LoanPerltrDeduction, table4.AMOUNT, table4.feed, table4.Cans, table4.totdeduction,table4.netamount, table4.SUMAMOUNT, table4.Tamt, table4.TransportLtrCost, table4.ProcureValueTSRate, table4.TransportTSRate, Table6_1.Gainmilkkg,Table6_1.pcode, Table6_1.GainFatkg, Table6_1.GainSnfkg, Table6_1.GainFatkg / table4.summilkkg * 100 AS FatGainPerltr,Table6_1.GainFatkg + Table6_1.GainSnfkg AS TOtfatSnfgain, Table6_1.GainSnfkg / table4.summilkkg * 100 AS SnfGainPerltr,Table6_1.GainFatkg / table4.summilkkg * 100 + Table6_1.GainSnfkg / table4.summilkkg * 100 AS TotalFAndSGainpercentLtr,table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100 AS AvgFatWithGainFatperltr,table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100 AS AvgSnfWithGainSnfperltr, (table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100)+ (table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100) AS TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr,Table6_1.GainFatkg * 175 AS FatCost, Table6_1.GainSnfkg * 150 AS SnfCost,Table6_1.GainFatkg * 175 + Table6_1.GainSnfkg * 150 AS TotFatCostAndSnfCost, Table6_1.GainFatkg * 175 / table4.summilkkg AS FatCostLtr,Table6_1.GainSnfkg * 150 / table4.summilkkg AS SnfCostLtr,Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg AS TotFatAndSnfCostLtr,(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS FatANdSnfPerLtrTS,table4.TransportLtrCost + 1 + table4.LtrCost AS TotalCost, 1 / table4.TS AS ChillingCostTsRate,table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate AS TotTsRate,(table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate)-(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS NETTsRate FROM (SELECT Plant_code, Plant_name, summilkkg, milkltrs, perdayliters, fatkg, AvgFat, avgsnf, Snfkg, TS, CLR, LtrCost, LoanDeduction,LoanPerltrDeduction, AMOUNT, feed, Cans, totdeduction, netamount, SUMAMOUNT, Tamt, TransportLtrCost, ProcureValueTSRate,TransportTSRate FROM (SELECT table1.Plant_code, table1.Plant_name, table1.summilkkg, table1.milkltrs, table1.perdayliters, table1.fatkg, table1.AvgFat, table1.avgsnf, table1.Snfkg, table1.TS, table1.CLR, table1.LtrCost, table1.LoanDeduction, table1.LoanPerltrDeduction,table1.AMOUNT, table1.feed, table1.Cans, table1.totdeduction, table1.netamount, table1.SUMAMOUNT, table2.Tamt,table2.Tamt / table1.milkltrs AS TransportLtrCost, table1.LtrCost / table1.TS AS ProcureValueTSRate,table2.Tamt / table1.milkltrs / table1.TS AS TransportTSRate FROM (SELECT Plant_code, Plant_name, SUM(Smkg) AS summilkkg, SUM(Smltr) AS milkltrs, SUM(Smltr) / COUNT(Company_code)  AS perdayliters, SUM(Sfatkg) AS fatkg, SUM(Sfatkg) / SUM(Smkg) * 100 AS AvgFat, SUM(SSnfkg) / SUM(Smkg) * 100 AS avgsnf, SUM(SSnfkg) AS Snfkg, SUM(Sfatkg) / SUM(Smkg) * 100 + SUM(SSnfkg) / SUM(Smkg) * 100 AS TS, SUM(Aclr) AS CLR, SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) / SUM(Smltr) AS LtrCost,SUM(Sinstamt) AS LoanDeduction, SUM(Sinstamt) / SUM(Smltr) AS LoanPerltrDeduction, SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) AS AMOUNT, SUM(Feed) AS feed, SUM(can) AS Cans, SUM(TotDeductions) AS totdeduction, SUM(NetAmount) AS netamount, SUM(SLoanAmount) AS SUMAMOUNT FROM Paymentdata WHERE (Frm_date = @Bill_frmdate) AND (To_date = @Bill_todate) AND (Plant_code = @Plant_code) GROUP BY Plant_name, Plant_code) AS table1 LEFT OUTER JOIN (SELECT Plant_Code, SUM(GrossAmount) AS Tamt FROM Truck_Present WHERE (Plant_Code = @Plant_code) AND (Pdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS table2 ON table1.Plant_code = table2.Plant_Code) AS table3) AS table4 RIGHT OUTER JOIN (SELECT        CAST((ISNULL(t2.dp_MILKKG, 0) + ISNULL(derivedtbl_1.cl_MILKKG, 0)) - (ISNULL(t1.pr_MILKKG, 0) + ISNULL(t3.op_MILKKG, 0)) AS DECIMAL(18, 2)) AS Gainmilkkg, t1.pcode, CAST((ISNULL(t2.dp_FATKG, 0) + ISNULL(derivedtbl_1.cl_FATKG, 0)) - (ISNULL(t1.pr_FATKG, 0) + ISNULL(t3.op_FATKG, 0)) AS DECIMAL(18, 2)) AS GainFatkg, CAST((ISNULL(t2.dp_SNFKG, 0) + ISNULL(derivedtbl_1.cl_SNFKG, 0))-(ISNULL(t1.pr_SNFKG, 0) + ISNULL(t3.op_SNFKG, 0)) AS DECIMAL(18, 2)) AS GainSnfkg FROM (SELECT  Plant_Code AS pcode, SUM(Milk_kg) AS pr_MILKKG, AVG(Fat) AS pr_FAT, AVG(Snf) AS pr_SNF, SUM(fat_kg) AS pr_FATKG,SUM(snf_kg) AS pr_SNFKG, SUM(Rate) AS pr_RATE, SUM(Amount) AS pr_AMOUNT FROM Procurement WHERE (Plant_Code = @Plant_code) AND (Prdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS t1 CROSS JOIN (SELECT SUM(MilkKg) AS dp_MILKKG, AVG(Fat) AS dp_FAT, AVG(Snf) AS dp_SNF, SUM(Fat_Kg) AS dp_FATKG, SUM(Snf_Kg) AS dp_SNFKG, SUM(Rate) AS dp_RATE, SUM(Amount) AS dp_AMOUNT  FROM Despatchnew WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate) AND (Plant_code = @Plant_code)) AS t2 CROSS JOIN (SELECT SUM(MilkKg) AS op_MILKKG, AVG(Fat) AS op_FAT, AVG(Snf) AS op_SNF, SUM(Fat_Kg) AS op_FATKG, SUM(Snf_Kg) AS op_SNFKG, SUM(Rate) AS op_RATE, SUM(Amount) AS op_AMOUNT FROM Stock_openingmilk WHERE (Datee BETWEEN @Bill_frmdate AND @Bill_todate) AND (Plant_code = @Plant_code)) AS t3 CROSS JOIN (SELECT SUM(MilkKg) AS cl_MILKKG, AVG(Fat) AS cl_FAT, AVG(Snf) AS cl_SNF, SUM(Fat_Kg) AS cl_FATKG, SUM(Snf_Kg) AS cl_SNFKG, SUM(Rate) AS cl_RATE, SUM(Amount) AS cl_AMOUNT  FROM Stock_Milk  WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate) AND (Plant_code = @Plant_code)) AS derivedtbl_1) AS Table6_1 ON  Table6_1.pcode = table4.Plant_code) AS Table6");
                            //advance billdate query   cmd = new SqlCommand("SELECT UPPER(LEFT(DATENAME(MONTH, Bill_frmdate), 3)) AS MMM, UPPER(LEFT(DATENAME(MONTH, Bill_todate), 3)) AS MMM1, YEAR(Bill_frmdate) AS fromyear,YEAR(Bill_todate) AS toyear FROM Bill_date WHERE  (Plant_Code = @Plant_Code) AND (YEAR(Bill_frmdate) BETWEEN @d1 AND @d2) AND (YEAR(Bill_todate) BETWEEN @d1 AND @d2) GROUP BY YEAR(Bill_frmdate), DATENAME(MONTH, Bill_frmdate), YEAR(Bill_todate), DATENAME(MONTH, Bill_todate) ORDER BY fromyear");
                            cmd.Parameters.Add("@Bill_frmdate", drr["Bill_frmdate"].ToString());
                            cmd.Parameters.Add("@Bill_todate", drr["Bill_todate"].ToString());
                            cmd.Parameters.Add("@Plant_code", plantcode);
                        }
                        else
                        {
                            cmd = new SqlCommand("SELECT    CAST(ISNULL(summilkkg, 0) AS DECIMAL(18, 2)) AS TotalKgs, CAST(ISNULL(milkltrs, 0) AS DECIMAL(18, 2)) AS TotalLiters, CAST(ISNULL(perdayliters, 0) AS DECIMAL(18, 2)) AS PerDayLtr, CAST(ISNULL(LtrCost, 0) AS DECIMAL(18, 2)) AS LtrCost, CAST(ISNULL(fatkg, 0) AS DECIMAL(18, 2)) AS KgFat,CAST(ISNULL(AvgFat, 0) AS DECIMAL(18, 2)) AS AvgFat,CAST(ISNULL(Snfkg, 0) AS DECIMAL(18, 2)) AS KgSnf,CAST(ISNULL(avgsnf, 0) AS DECIMAL(18, 2)) AS AvgSnf,CAST(ISNULL(TS, 0) AS DECIMAL(18, 2)) AS TS,CAST(ISNULL(AMOUNT, 0) AS DECIMAL(18, 2)) AS MilkValueAmount,CAST(ISNULL(LoanDeduction, 0) AS DECIMAL(18, 2)) AS LoanDeduction,CAST(ISNULL(LoanPerltrDeduction, 0) AS DECIMAL(18, 2)) AS LoanPerltrDeduction,CAST(ISNULL(feed, 0) AS DECIMAL(18, 2)) AS FeedDeduction,CAST(ISNULL(netamount, 0) AS DECIMAL(18, 2)) AS NetBillAmount,CAST(ISNULL(Tamt, 0) AS DECIMAL(18, 2)) AS TransportAmount,CAST(ISNULL(TransportLtrCost, 0) AS DECIMAL(18, 2)) AS TransportLtrCost,CAST(ISNULL(ProcureValueTSRate, 0) AS DECIMAL(18, 2)) AS ProcureValueTSRate,CAST(ISNULL(TransportTSRate, 0) AS DECIMAL(18, 2)) AS TransportTSRate,CAST(ISNULL(1, 0) AS DECIMAL(18, 2)) AS ChillingCostPerLtr,CAST(ISNULL(ChillingCostTsRate, 0) AS DECIMAL(18, 2)) AS ChillingCostTsRate,CAST(ISNULL(TotTsRate, 0) AS DECIMAL(18, 2)) AS TotTsRate,CAST(ISNULL(Gainmilkkg, 0) AS DECIMAL(18, 2)) AS KgMilkGain,CAST(ISNULL(GainFatkg, 0) AS DECIMAL(18, 2)) AS KgFatGain,CAST(ISNULL(GainSnfkg, 0) AS DECIMAL(18, 2)) AS kgSnfGain, CAST(ISNULL(TOtfatSnfgain, 0) AS DECIMAL(18, 2)) AS TotalFatandSnfGain,CAST(ISNULL(FatGainPerltr, 0) AS DECIMAL(18, 2)) AS FatGainPerltr,CAST(ISNULL(SnfGainPerltr, 0) AS DECIMAL(18, 2)) AS SnfGainPerltr,CAST(ISNULL(TotalFAndSGainpercentLtr, 0) AS DECIMAL(18, 2)) AS TotalFAndSGainpercentLtr,CAST(ISNULL(AvgFatWithGainFatperltr, 0) AS DECIMAL(18, 2)) AS AvgFatWithGainFatperltr,CAST(ISNULL(AvgSnfWithGainSnfperltr, 0) AS DECIMAL(18, 2)) AS AvgSnfWithGainSnfperltr,CAST(ISNULL(TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr, 0) AS DECIMAL(18, 2)) AS TotAvgSnfANDFatPerltr,CAST(ISNULL(FatCost, 0) AS DECIMAL(18, 2)) AS FatCost@175,CAST(ISNULL(SnfCost, 0) AS DECIMAL(18, 2)) AS SnfCost@150,CAST(ISNULL(TotFatCostAndSnfCost, 0) AS DECIMAL(18, 2)) AS TotFatCostAndSnfCost,CAST(ISNULL(FatCostLtr, 0) AS DECIMAL(18, 2)) AS FatCostLtr,CAST(ISNULL(SnfCostLtr, 0) AS DECIMAL(18, 2)) AS SnfCostLtr,CAST(ISNULL(TotFatAndSnfCostLtr, 0) AS DECIMAL(18, 2)) AS TotFatAndSnfCostLtr,CAST(ISNULL(FatANdSnfPerLtrTS, 0) AS DECIMAL(18, 2)) AS FatANDSnfPerTS,CAST(ISNULL(TotalCost, 0) AS DECIMAL(18, 2)) AS TotalCost,CAST(ISNULL(NETTsRate, 0) AS DECIMAL(18, 2)) AS NetTsRate  FROM (SELECT table4.Plant_code, table4.Plant_name, table4.summilkkg, table4.milkltrs, table4.perdayliters, table4.fatkg, table4.AvgFat, table4.avgsnf, table4.Snfkg,table4.TS, table4.CLR, table4.LtrCost, table4.LoanDeduction, table4.LoanPerltrDeduction, table4.AMOUNT, table4.feed, table4.Cans, table4.totdeduction,table4.netamount, table4.SUMAMOUNT, table4.Tamt, table4.TransportLtrCost, table4.ProcureValueTSRate, table4.TransportTSRate, Table6_1.Gainmilkkg,Table6_1.pcode, Table6_1.GainFatkg, Table6_1.GainSnfkg, Table6_1.GainFatkg / table4.summilkkg * 100 AS FatGainPerltr,Table6_1.GainFatkg + Table6_1.GainSnfkg AS TOtfatSnfgain, Table6_1.GainSnfkg / table4.summilkkg * 100 AS SnfGainPerltr,Table6_1.GainFatkg / table4.summilkkg * 100 + Table6_1.GainSnfkg / table4.summilkkg * 100 AS TotalFAndSGainpercentLtr,table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100 AS AvgFatWithGainFatperltr,table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100 AS AvgSnfWithGainSnfperltr, (table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100)+ (table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100) AS TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr, Table6_1.GainFatkg * 175 AS FatCost, Table6_1.GainSnfkg * 150 AS SnfCost,Table6_1.GainFatkg * 175 + Table6_1.GainSnfkg * 150 AS TotFatCostAndSnfCost, Table6_1.GainFatkg * 175 / table4.summilkkg AS FatCostLtr, Table6_1.GainSnfkg * 150 / table4.summilkkg AS SnfCostLtr, Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg AS TotFatAndSnfCostLtr,(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS FatANdSnfPerLtrTS,table4.TransportLtrCost + 1 + table4.LtrCost AS TotalCost, 1 / table4.TS AS ChillingCostTsRate,table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate AS TotTsRate,(table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate)-(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS NETTsRate FROM (SELECT Plant_code, Plant_name, summilkkg, milkltrs, perdayliters, fatkg, AvgFat, avgsnf, Snfkg, TS, CLR, LtrCost, LoanDeduction, LoanPerltrDeduction, AMOUNT, feed, Cans, totdeduction, netamount, SUMAMOUNT, Tamt, TransportLtrCost, ProcureValueTSRate,TransportTSRate FROM (SELECT table1.Plant_code, table1.Plant_name, table1.summilkkg, table1.milkltrs, table1.perdayliters, table1.fatkg, table1.AvgFat, table1.avgsnf, table1.Snfkg, table1.TS, table1.CLR, table1.LtrCost, table1.LoanDeduction, table1.LoanPerltrDeduction,table1.AMOUNT, table1.feed, table1.Cans, table1.totdeduction, table1.netamount, table1.SUMAMOUNT, table2.Tamt,table2.Tamt / table1.milkltrs AS TransportLtrCost, table1.LtrCost / table1.TS AS ProcureValueTSRate,table2.Tamt / table1.milkltrs / table1.TS AS TransportTSRate FROM (SELECT Plant_code, Plant_name, SUM(Smkg) AS summilkkg, SUM(Smltr) AS milkltrs, SUM(Smltr) / COUNT(Company_code)  AS perdayliters, SUM(Sfatkg) AS fatkg, SUM(Sfatkg) / SUM(Smkg) * 100 AS AvgFat, SUM(SSnfkg) / SUM(Smkg) * 100 AS avgsnf, SUM(SSnfkg) AS Snfkg, SUM(Sfatkg) / SUM(Smkg) * 100 + SUM(SSnfkg) / SUM(Smkg) * 100 AS TS,SUM(Aclr) AS CLR, SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) / SUM(Smltr) AS LtrCost, SUM(Sinstamt) AS LoanDeduction, SUM(Sinstamt) / SUM(Smltr) AS LoanPerltrDeduction,  SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) AS AMOUNT, SUM(Feed) AS feed, SUM(can) AS Cans, SUM(TotDeductions) AS totdeduction, SUM(NetAmount) AS netamount, SUM(SLoanAmount) AS SUMAMOUNT FROM  Paymentdata WHERE (Frm_date BETWEEN @Bill_frmdate AND @Bill_todate) AND (To_date BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_name, Plant_code) AS table1 RIGHT OUTER JOIN (SELECT  Plant_Code, SUM(GrossAmount) AS Tamt FROM Truck_Present WHERE (Pdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS table2 ON table1.Plant_code = table2.Plant_Code) AS table3) AS table4 RIGHT OUTER JOIN (SELECT CAST((ISNULL(t2.dp_MILKKG, 0) + ISNULL(derivedtbl_1.cl_MILKKG, 0)) - (ISNULL(t1.pr_MILKKG, 0) + ISNULL(t3.op_MILKKG, 0)) AS DECIMAL(18, 2)) AS Gainmilkkg, t1.pcode, CAST((ISNULL(t2.dp_FATKG, 0) + ISNULL(derivedtbl_1.cl_FATKG, 0)) - (ISNULL(t1.pr_FATKG, 0) + ISNULL(t3.op_FATKG, 0)) AS DECIMAL(18, 2)) AS GainFatkg, CAST((ISNULL(t2.dp_SNFKG, 0) + ISNULL(derivedtbl_1.cl_SNFKG, 0)) - (ISNULL(t1.pr_SNFKG, 0) + ISNULL(t3.op_SNFKG, 0)) AS DECIMAL(18, 2)) AS GainSnfkg FROM   (SELECT Plant_Code AS pcode, SUM(Milk_kg) AS pr_MILKKG, AVG(Fat) AS pr_FAT, AVG(Snf) AS pr_SNF, SUM(fat_kg) AS pr_FATKG, SUM(snf_kg) AS pr_SNFKG, SUM(Rate) AS pr_RATE, SUM(Amount) AS pr_AMOUNT  FROM Procurement WHERE (Prdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS t1 CROSS JOIN (SELECT SUM(MilkKg) AS dp_MILKKG, AVG(Fat) AS dp_FAT, AVG(Snf) AS dp_SNF, SUM(Fat_Kg) AS dp_FATKG, SUM(Snf_Kg)  AS dp_SNFKG, SUM(Rate) AS dp_RATE, SUM(Amount) AS dp_AMOUNT FROM Despatchnew WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate)) AS t2 CROSS JOIN (SELECT SUM(MilkKg) AS op_MILKKG, AVG(Fat) AS op_FAT, AVG(Snf) AS op_SNF, SUM(Fat_Kg) AS op_FATKG, SUM(Snf_Kg) AS op_SNFKG, SUM(Rate) AS op_RATE, SUM(Amount) AS op_AMOUNT FROM Stock_openingmilk WHERE (Datee BETWEEN @Bill_frmdate AND @Bill_todate)) AS t3 CROSS JOIN (SELECT SUM(MilkKg) AS cl_MILKKG, AVG(Fat) AS cl_FAT, AVG(Snf) AS cl_SNF, SUM(Fat_Kg) AS cl_FATKG, SUM(Snf_Kg) AS cl_SNFKG, SUM(Rate) AS cl_RATE, SUM(Amount) AS cl_AMOUNT FROM Stock_Milk WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate)) AS derivedtbl_1) AS Table6_1 ON Table6_1.pcode = table4.Plant_code)  AS Table6");
                            cmd.Parameters.Add("@Bill_frmdate", fromdate);
                            cmd.Parameters.Add("@Bill_todate", todate);
                        }

                        DataTable dtdata = vdm.SelectQuery(cmd).Tables[0];
                        DataRow ksdr = null;
                        DataRow ksdr1 = null;
                        string[] strarr1 = new string[] { "TotalKgs", "TotalLiters", "PerDayLtr", "LtrCost", "KgFat", "AvgFat", "KgSnf", "AvgSnf", "TS", "MilkValueAmount", "LoanDeduction", "LoanPerltrDeduction", "FeedDeduction", "NetBillAmount", "TransportAmount", "TransportLtrCost", "ProcureValueTSRate", "TransportTSRate", "ChillingCostPerLtr", "ChillingCostTsRate", "TotTsRate", "KgMilkGain", "KgFatGain", "kgSnfGain", "TotalFatandSnfGain", "FatGainPerltr", "SnfGainPerltr", "TotalFAndSGainpercentLtr", "AvgFatWithGainFatperltr", "AvgSnfWithGainSnfperltr", "TotAvgSnfANDFatPerltr", "FatCost@175", "SnfCost@150", "TotFatCostAndSnfCost", "FatCostLtr", "SnfCostLtr", "TotFatAndSnfCostLtr", "FatANDSnfPerTS", "TotalCost", "NetTsRate" };//52
                        //string[] strarr1 = new string[] { "Total Kgs", "AvgFat ", "AvgSnf", "Total Solid TS(Kg Fat Rate)", "LtrCost", "ProcureVale TSRate", "Transport Ltr Cost", "Transport TS Rate", "ChillCost TSRate", "Total TS Rate", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %",  "Net_TSRate" };//0-23
                        if (count == 0)
                        {
                            for (int i = 0; i < strarr1.Length; i++)
                            {
                                ksdr = Report.NewRow();
                                ksdr[0] = strarr1[i].ToString();
                                int j = 1;
                                foreach (DataRow dr2 in dtdata.Rows)
                                {
                                    ksdr[j] = dr2[i].ToString();
                                    j++;
                                }
                                Report.Rows.Add(ksdr);
                            }
                            count++;
                        }
                        else
                        {
                            if (count == 1)
                            {

                                //ksdr1 = Report.NewRow();

                                // ksdr[1] = strarr1[i].ToString();

                                DataRow dr;
                                foreach (DataRow dr3 in dtdata.Rows)
                                {
                                    for (int i = 0; i < strarr1.Length; i++)
                                    {
                                        dr = Report.Rows[i];// search whole table 
                                        //  dr["Product_name"] = "cde"; //change the name
                                        dr[newrow] = dr3[i].ToString();
                                        // j++;
                                    }

                                }
                                //Report.Rows.Add(ksdr1);
                                newrow++;
                            }
                        }
                    }
                }
                //else
                //{
                //    cmd = new SqlCommand("SELECT Plant_Code FROM Bill_date WHERE (Bill_frmdate BETWEEN @d1 AND @d2) AND (Bill_todate BETWEEN @d1 AND @d2)  GROUP BY Plant_Code");
                //    cmd.Parameters.Add("@d1", fromdate);
                //    cmd.Parameters.Add("@d2", todate);
                //   // cmd.Parameters.Add("@Plant_Code", plantcode);
                //    DataTable dtbilldate = vdm.SelectQuery(cmd).Tables[0];
                //    foreach (DataRow dr in dtbilldate.Rows)
                //    {
                //        string plantCode = dr["Plant_Code"].ToString();
                //        Report.Columns.Add(plantCode);
                //    }
                //    if (dtbilldate.Rows.Count > 0)
                //    {
                //        foreach (DataRow drr in dtbilldate.Rows)
                //        {
                //            cmd = new SqlCommand("SELECT Plant_code, Plant_name, summilkkg, milkltrs, perdayliters, fatkg, AvgFat, avgsnf, Snfkg, TS, CLR, LtrCost, LoanDeduction, LoanPerltrDeduction, AMOUNT, feed, Cans, SUMAMOUNT, Tamt, TransportLtrCost, ProcureValueTSRate, TransportTSRate, Gainmilkkg, pcode, GainFatkg, GainSnfkg, FatGainPerltr,TOtfatSnfgain, SnfGainPerltr, TotalFAndSGainpercentLtr, AvgFatWithGainFatperltr, AvgSnfWithGainSnfperltr, TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr,FatCost, SnfCost, TotFatCostAndSnfCost, FatCostLtr, SnfCostLtr, TotFatAndSnfCostLtr, FatANdSnfPerLtrTS, TotalCost, ChillingCostTsRate, TotTsRate,NETTsRate FROM (SELECT table4.Plant_code, table4.Plant_name, table4.summilkkg, table4.milkltrs, table4.perdayliters, table4.fatkg, table4.AvgFat, table4.avgsnf, table4.Snfkg,table4.TS, table4.CLR, table4.LtrCost, table4.LoanDeduction, table4.LoanPerltrDeduction, table4.AMOUNT, table4.feed, table4.Cans, table4.totdeduction,table4.netamount, table4.SUMAMOUNT, table4.Tamt, table4.TransportLtrCost, table4.ProcureValueTSRate, table4.TransportTSRate, Table6_1.Gainmilkkg,Table6_1.pcode, Table6_1.GainFatkg, Table6_1.GainSnfkg, Table6_1.GainFatkg / table4.summilkkg * 100 AS FatGainPerltr,Table6_1.GainFatkg + Table6_1.GainSnfkg AS TOtfatSnfgain, Table6_1.GainSnfkg / table4.summilkkg * 100 AS SnfGainPerltr,Table6_1.GainFatkg / table4.summilkkg * 100 + Table6_1.GainSnfkg / table4.summilkkg * 100 AS TotalFAndSGainpercentLtr,table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100 AS AvgFatWithGainFatperltr,table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100 AS AvgSnfWithGainSnfperltr, (table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100)+ (table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100) AS TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr, Table6_1.GainFatkg * 175 AS FatCost, Table6_1.GainSnfkg * 150 AS SnfCost,Table6_1.GainFatkg * 175 + Table6_1.GainSnfkg * 150 AS TotFatCostAndSnfCost, Table6_1.GainFatkg * 175 / table4.summilkkg AS FatCostLtr, Table6_1.GainSnfkg * 150 / table4.summilkkg AS SnfCostLtr, Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg AS TotFatAndSnfCostLtr,(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS FatANdSnfPerLtrTS,table4.TransportLtrCost + 1 + table4.LtrCost AS TotalCost, 1 / table4.TS AS ChillingCostTsRate,table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate AS TotTsRate,(table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate)-(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS NETTsRate FROM (SELECT Plant_code, Plant_name, summilkkg, milkltrs, perdayliters, fatkg, AvgFat, avgsnf, Snfkg, TS, CLR, LtrCost, LoanDeduction, LoanPerltrDeduction, AMOUNT, feed, Cans, totdeduction, netamount, SUMAMOUNT, Tamt, TransportLtrCost, ProcureValueTSRate,TransportTSRate FROM (SELECT table1.Plant_code, table1.Plant_name, table1.summilkkg, table1.milkltrs, table1.perdayliters, table1.fatkg, table1.AvgFat, table1.avgsnf, table1.Snfkg, table1.TS, table1.CLR, table1.LtrCost, table1.LoanDeduction, table1.LoanPerltrDeduction,table1.AMOUNT, table1.feed, table1.Cans, table1.totdeduction, table1.netamount, table1.SUMAMOUNT, table2.Tamt,table2.Tamt / table1.milkltrs AS TransportLtrCost, table1.LtrCost / table1.TS AS ProcureValueTSRate,table2.Tamt / table1.milkltrs / table1.TS AS TransportTSRate FROM (SELECT Plant_code, Plant_name, SUM(Smkg) AS summilkkg, SUM(Smltr) AS milkltrs, SUM(Smltr) / COUNT(Company_code)  AS perdayliters, SUM(Sfatkg) AS fatkg, SUM(Sfatkg) / SUM(Smkg) * 100 AS AvgFat, SUM(SSnfkg) / SUM(Smkg) * 100 AS avgsnf, SUM(SSnfkg) AS Snfkg, SUM(Sfatkg) / SUM(Smkg) * 100 + SUM(SSnfkg) / SUM(Smkg) * 100 AS TS,SUM(Aclr) AS CLR, SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) / SUM(Smltr) AS LtrCost, SUM(Sinstamt) AS LoanDeduction, SUM(Sinstamt) / SUM(Smltr) AS LoanPerltrDeduction,  SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) AS AMOUNT, SUM(Feed) AS feed, SUM(can) AS Cans, SUM(TotDeductions) AS totdeduction, SUM(NetAmount) AS netamount, SUM(SLoanAmount) AS SUMAMOUNT FROM  Paymentdata WHERE (Frm_date BETWEEN @Bill_frmdate AND @Bill_todate) AND (To_date BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_name, Plant_code) AS table1 RIGHT OUTER JOIN (SELECT  Plant_Code, SUM(GrossAmount) AS Tamt FROM Truck_Present WHERE (Pdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS table2 ON table1.Plant_code = table2.Plant_Code) AS table3) AS table4 RIGHT OUTER JOIN (SELECT CAST((ISNULL(t2.dp_MILKKG, 0) + ISNULL(derivedtbl_1.cl_MILKKG, 0)) - (ISNULL(t1.pr_MILKKG, 0) + ISNULL(t3.op_MILKKG, 0)) AS DECIMAL(18, 2)) AS Gainmilkkg, t1.pcode, CAST((ISNULL(t2.dp_FATKG, 0) + ISNULL(derivedtbl_1.cl_FATKG, 0)) - (ISNULL(t1.pr_FATKG, 0) + ISNULL(t3.op_FATKG, 0)) AS DECIMAL(18, 2)) AS GainFatkg, CAST((ISNULL(t2.dp_SNFKG, 0) + ISNULL(derivedtbl_1.cl_SNFKG, 0)) - (ISNULL(t1.pr_SNFKG, 0) + ISNULL(t3.op_SNFKG, 0)) AS DECIMAL(18, 2)) AS GainSnfkg FROM   (SELECT Plant_Code AS pcode, SUM(Milk_kg) AS pr_MILKKG, AVG(Fat) AS pr_FAT, AVG(Snf) AS pr_SNF, SUM(fat_kg) AS pr_FATKG, SUM(snf_kg) AS pr_SNFKG, SUM(Rate) AS pr_RATE, SUM(Amount) AS pr_AMOUNT  FROM Procurement WHERE (Prdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS t1 CROSS JOIN (SELECT SUM(MilkKg) AS dp_MILKKG, AVG(Fat) AS dp_FAT, AVG(Snf) AS dp_SNF, SUM(Fat_Kg) AS dp_FATKG, SUM(Snf_Kg)  AS dp_SNFKG, SUM(Rate) AS dp_RATE, SUM(Amount) AS dp_AMOUNT FROM Despatchnew WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate)) AS t2 CROSS JOIN (SELECT SUM(MilkKg) AS op_MILKKG, AVG(Fat) AS op_FAT, AVG(Snf) AS op_SNF, SUM(Fat_Kg) AS op_FATKG, SUM(Snf_Kg) AS op_SNFKG, SUM(Rate) AS op_RATE, SUM(Amount) AS op_AMOUNT FROM Stock_openingmilk WHERE (Datee BETWEEN @Bill_frmdate AND @Bill_todate)) AS t3 CROSS JOIN (SELECT SUM(MilkKg) AS cl_MILKKG, AVG(Fat) AS cl_FAT, AVG(Snf) AS cl_SNF, SUM(Fat_Kg) AS cl_FATKG, SUM(Snf_Kg) AS cl_SNFKG, SUM(Rate) AS cl_RATE, SUM(Amount) AS cl_AMOUNT FROM Stock_Milk WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate)) AS derivedtbl_1) AS Table6_1 ON Table6_1.pcode = table4.Plant_code)  AS Table6");
                //            cmd.Parameters.Add("@Bill_frmdate", fromdate);
                //            cmd.Parameters.Add("@Bill_todate", todate);
                //            //cmd.Parameters.Add("@Plant_code", plantcode);
                //            DataTable dtdata = vdm.SelectQuery(cmd).Tables[0];
                //            DataRow ksdr = null;
                //            DataRow ksdr1 = null;
                //            string[] strarr1 = new string[] { "TotalKgs","TotalLiters","PerDayLtr","LtrCost","KgFat","AvgFat","KgSnf","AvgSnf","TS","MilkValueAmount","LoanDeduction","LoanPerltrDeduction","FeedDeduction","NetBillAmount","TransportAmount","TransportLtrCost","ProcureValueTSRate","TransportTSRate","ChillingCostPerLtr","ChillingCostTsRate","TotTsRate","KgMilkGain","KgFatGain","kgSnfGain","TotalFatandSnfGain","FatGainPerltr","SnfGainPerltr","TotalFAndSGainpercentLtr","AvgFatWithGainFatperltr","AvgSnfWithGainSnfperltr","TotAvgSnfANDFatPerltr","FatCost@175","SnfCost@150","TotFatCostAndSnfCost","FatCostLtr","SnfCostLtr","TotFatAndSnfCostLtr","FatANDSnfPerTS","TotalCost","NetTsRate","Plant_code","Plant_name","totdeduction" };//52
                //            //string[] strarr1 = new string[] { "Total Kgs", "AvgFat ", "AvgSnf", "Total Solid TS(Kg Fat Rate)", "LtrCost", "ProcureVale TSRate", "Transport Ltr Cost", "Transport TS Rate", "ChillCost TSRate", "Total TS Rate", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %",  "Net_TSRate" };//0-23
                //            if (count == 0)
                //            {
                //                for (int i = 0; i < strarr1.Length; i++)
                //                {
                //                    ksdr = Report.NewRow();
                //                    ksdr[0] = strarr1[i].ToString();
                //                    int j = 1;
                //                    foreach (DataRow dr2 in dtdata.Rows)
                //                    {
                //                        ksdr[j] = dr2[i].ToString();
                //                        j++;
                //                    }
                //                    Report.Rows.Add(ksdr);
                //                }
                //                count++;

                //            }
                //            else
                //            {
                //                if (count == 1)
                //                {

                //                    //ksdr1 = Report.NewRow();

                //                    // ksdr[1] = strarr1[i].ToString();

                //                    DataRow dr;
                //                    foreach (DataRow dr3 in dtdata.Rows)
                //                    {
                //                        for (int i = 0; i < strarr1.Length; i++)
                //                        {
                //                            dr = Report.Rows[i];// search whole table 
                //                            //  dr["Product_name"] = "cde"; //change the name
                //                            dr[newrow] = dr3[i].ToString();
                //                            // j++;
                //                        }

                //                    }
                //                    //Report.Rows.Add(ksdr1);
                //                    newrow++;
                //                }

                //            }
                //        }
                //    }
                //}

                grdReports.DataSource = Report;
                grdReports.DataBind();
                hidepanel.Visible = true;
                lblFromDate.Text = mymonth;


            }
            else
            {
                //DateTime today = DateTime.Today;
                //DateTime start = new DateTime(today.Year, today.Month, 1);
                //DateTime end = start.AddMonths(1);
                string[] strarr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                string Fromdate; string Todate;
                for (int k = 0; k < strarr.Length; k++)
                {

                    string mmm = strarr[k].ToString();
                    string YEAR = ddlyear.SelectedItem.Value;
                    int Year = 0;
                    int.TryParse(YEAR, out Year);
                    int MONTH = 0;
                    int.TryParse(mmm, out MONTH);
                    int days = DateTime.DaysInMonth(Year, MONTH);

                    Fromdate = MONTH + "/" + 1 + "/" + Year;
                    Todate = MONTH + "/" + days + "/" + Year;
                    //fromdate = Year;
                    //todate = Year;
                    int plantcode = Convert.ToInt32(ddlplantname.SelectedItem.Value);
                    int plancode1 = plantcode;
                    int newrow1 = 2;
                    int count = 0;
                    //  DateTime FROMdate = fromdate.DateTime.Now();
                    cmd = new SqlCommand("SELECT  Bill_frmdate, Bill_todate FROM Bill_date WHERE (Bill_frmdate BETWEEN @d1 AND @d2) AND (Bill_todate BETWEEN @d1 AND @d2) AND Plant_Code=@Plant_Code  order by  Bill_frmdate");
                    //cmd = new SqlCommand("SELECT UPPER(LEFT(DATENAME(MONTH, Bill_frmdate), 3)) AS MMM, UPPER(LEFT(DATENAME(MONTH, Bill_todate), 3)) AS MMM1, YEAR(Bill_frmdate) AS fromyear,YEAR(Bill_todate) AS toyear FROM Bill_date WHERE  (Plant_Code = @Plant_Code) AND (YEAR(Bill_frmdate) BETWEEN @d1 AND @d2) AND (YEAR(Bill_todate) BETWEEN @d1 AND @d2) GROUP BY YEAR(Bill_frmdate), DATENAME(MONTH, Bill_frmdate), YEAR(Bill_todate), DATENAME(MONTH, Bill_todate) ORDER BY fromyear");
                    cmd.Parameters.Add("@d1", Fromdate);
                    cmd.Parameters.Add("@d2", Todate);
                    cmd.Parameters.Add("@Plant_Code", plantcode);
                    DataTable dtbilldate1 = vdm.SelectQuery(cmd).Tables[0];
                    foreach (DataRow dr in dtbilldate1.Rows)
                    {
                        string billfrodate = dr["Bill_frmdate"].ToString();
                        string billtodate = dr["Bill_todate"].ToString();
                        Report.Columns.Add(billfrodate + "To" + billtodate);
                    }
                    if (dtbilldate1.Rows.Count > 0)
                    {
                        foreach (DataRow drr in dtbilldate1.Rows)
                        {
                            cmd = new SqlCommand("SELECT  CAST(ISNULL(summilkkg, 0) AS DECIMAL(18, 2)) AS TotalKgs, CAST(ISNULL(milkltrs, 0) AS DECIMAL(18, 2)) AS TotalLiters, CAST(ISNULL(perdayliters, 0) AS DECIMAL(18, 2)) AS PerDayLtr, CAST(ISNULL(LtrCost, 0) AS DECIMAL(18, 2)) AS LtrCost, CAST(ISNULL(fatkg, 0) AS DECIMAL(18, 2)) AS KgFat,CAST(ISNULL(AvgFat, 0) AS DECIMAL(18, 2)) AS AvgFat,CAST(ISNULL(Snfkg, 0) AS DECIMAL(18, 2)) AS KgSnf,CAST(ISNULL(avgsnf, 0) AS DECIMAL(18, 2)) AS AvgSnf,CAST(ISNULL(TS, 0) AS DECIMAL(18, 2)) AS TS,CAST(ISNULL(AMOUNT, 0) AS DECIMAL(18, 2)) AS MilkValueAmount,CAST(ISNULL(LoanDeduction, 0) AS DECIMAL(18, 2)) AS LoanDeduction,CAST(ISNULL(LoanPerltrDeduction, 0) AS DECIMAL(18, 2)) AS LoanPerltrDeduction,CAST(ISNULL(feed, 0) AS DECIMAL(18, 2)) AS FeedDeduction,CAST(ISNULL(netamount, 0) AS DECIMAL(18, 2)) AS NetBillAmount,CAST(ISNULL(Tamt, 0) AS DECIMAL(18, 2)) AS TransportAmount,CAST(ISNULL(TransportLtrCost, 0) AS DECIMAL(18, 2)) AS TransportLtrCost,CAST(ISNULL(ProcureValueTSRate, 0) AS DECIMAL(18, 2)) AS ProcureValueTSRate,CAST(ISNULL(TransportTSRate, 0) AS DECIMAL(18, 2)) AS TransportTSRate,CAST(ISNULL(1, 0) AS DECIMAL(18, 2)) AS ChillingCostPerLtr,CAST(ISNULL(ChillingCostTsRate, 0) AS DECIMAL(18, 2)) AS ChillingCostTsRate,CAST(ISNULL(TotTsRate, 0) AS DECIMAL(18, 2)) AS TotTsRate,CAST(ISNULL(Gainmilkkg, 0) AS DECIMAL(18, 2)) AS KgMilkGain,CAST(ISNULL(GainFatkg, 0) AS DECIMAL(18, 2)) AS KgFatGain,CAST(ISNULL(GainSnfkg, 0) AS DECIMAL(18, 2)) AS kgSnfGain, CAST(ISNULL(TOtfatSnfgain, 0) AS DECIMAL(18, 2)) AS TotalFatandSnfGain,CAST(ISNULL(FatGainPerltr, 0) AS DECIMAL(18, 2)) AS FatGainPerltr,CAST(ISNULL(SnfGainPerltr, 0) AS DECIMAL(18, 2)) AS SnfGainPerltr,CAST(ISNULL(TotalFAndSGainpercentLtr, 0) AS DECIMAL(18, 2)) AS TotalFAndSGainpercentLtr,CAST(ISNULL(AvgFatWithGainFatperltr, 0) AS DECIMAL(18, 2)) AS AvgFatWithGainFatperltr,CAST(ISNULL(AvgSnfWithGainSnfperltr, 0) AS DECIMAL(18, 2)) AS AvgSnfWithGainSnfperltr,CAST(ISNULL(TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr, 0) AS DECIMAL(18, 2)) AS TotAvgSnfANDFatPerltr,CAST(ISNULL(FatCost, 0) AS DECIMAL(18, 2)) AS FatCost@175,CAST(ISNULL(SnfCost, 0) AS DECIMAL(18, 2)) AS SnfCost@150,CAST(ISNULL(TotFatCostAndSnfCost, 0) AS DECIMAL(18, 2)) AS TotFatCostAndSnfCost,CAST(ISNULL(FatCostLtr, 0) AS DECIMAL(18, 2)) AS FatCostLtr,CAST(ISNULL(SnfCostLtr, 0) AS DECIMAL(18, 2)) AS SnfCostLtr,CAST(ISNULL(TotFatAndSnfCostLtr, 0) AS DECIMAL(18, 2)) AS TotFatAndSnfCostLtr,CAST(ISNULL(FatANdSnfPerLtrTS, 0) AS DECIMAL(18, 2)) AS FatANDSnfPerTS,CAST(ISNULL(TotalCost, 0) AS DECIMAL(18, 2)) AS TotalCost,CAST(ISNULL(NETTsRate, 0) AS DECIMAL(18, 2)) AS NetTsRate   FROM (SELECT table4.Plant_code, table4.Plant_name, table4.summilkkg, table4.milkltrs, table4.perdayliters, table4.fatkg, table4.AvgFat, table4.avgsnf, table4.Snfkg,table4.TS, table4.CLR, table4.LtrCost, table4.LoanDeduction, table4.LoanPerltrDeduction, table4.AMOUNT, table4.feed, table4.Cans, table4.totdeduction,table4.netamount, table4.SUMAMOUNT, table4.Tamt, table4.TransportLtrCost, table4.ProcureValueTSRate, table4.TransportTSRate, Table6_1.Gainmilkkg,Table6_1.pcode, Table6_1.GainFatkg, Table6_1.GainSnfkg, Table6_1.GainFatkg / table4.summilkkg * 100 AS FatGainPerltr,Table6_1.GainFatkg + Table6_1.GainSnfkg AS TOtfatSnfgain, Table6_1.GainSnfkg / table4.summilkkg * 100 AS SnfGainPerltr,Table6_1.GainFatkg / table4.summilkkg * 100 + Table6_1.GainSnfkg / table4.summilkkg * 100 AS TotalFAndSGainpercentLtr,table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100 AS AvgFatWithGainFatperltr,table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100 AS AvgSnfWithGainSnfperltr, (table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100)+ (table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100) AS TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr,Table6_1.GainFatkg * 175 AS FatCost, Table6_1.GainSnfkg * 150 AS SnfCost,Table6_1.GainFatkg * 175 + Table6_1.GainSnfkg * 150 AS TotFatCostAndSnfCost, Table6_1.GainFatkg * 175 / table4.summilkkg AS FatCostLtr,Table6_1.GainSnfkg * 150 / table4.summilkkg AS SnfCostLtr,Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg AS TotFatAndSnfCostLtr,(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS FatANdSnfPerLtrTS,table4.TransportLtrCost + 1 + table4.LtrCost AS TotalCost, 1 / table4.TS AS ChillingCostTsRate,table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate AS TotTsRate,(table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate)-(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS NETTsRate FROM (SELECT Plant_code, Plant_name, summilkkg, milkltrs, perdayliters, fatkg, AvgFat, avgsnf, Snfkg, TS, CLR, LtrCost, LoanDeduction,LoanPerltrDeduction, AMOUNT, feed, Cans, totdeduction, netamount, SUMAMOUNT, Tamt, TransportLtrCost, ProcureValueTSRate,TransportTSRate FROM (SELECT table1.Plant_code, table1.Plant_name, table1.summilkkg, table1.milkltrs, table1.perdayliters, table1.fatkg, table1.AvgFat, table1.avgsnf, table1.Snfkg, table1.TS, table1.CLR, table1.LtrCost, table1.LoanDeduction, table1.LoanPerltrDeduction,table1.AMOUNT, table1.feed, table1.Cans, table1.totdeduction, table1.netamount, table1.SUMAMOUNT, table2.Tamt,table2.Tamt / table1.milkltrs AS TransportLtrCost, table1.LtrCost / table1.TS AS ProcureValueTSRate,table2.Tamt / table1.milkltrs / table1.TS AS TransportTSRate FROM (SELECT Plant_code, Plant_name, SUM(Smkg) AS summilkkg, SUM(Smltr) AS milkltrs, SUM(Smltr) / COUNT(Company_code)  AS perdayliters, SUM(Sfatkg) AS fatkg, SUM(Sfatkg) / SUM(Smkg) * 100 AS AvgFat, SUM(SSnfkg) / SUM(Smkg) * 100 AS avgsnf, SUM(SSnfkg) AS Snfkg, SUM(Sfatkg) / SUM(Smkg) * 100 + SUM(SSnfkg) / SUM(Smkg) * 100 AS TS, SUM(Aclr) AS CLR, SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) / SUM(Smltr) AS LtrCost,SUM(Sinstamt) AS LoanDeduction, SUM(Sinstamt) / SUM(Smltr) AS LoanPerltrDeduction, SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) AS AMOUNT, SUM(Feed) AS feed, SUM(can) AS Cans, SUM(TotDeductions) AS totdeduction, SUM(NetAmount) AS netamount, SUM(SLoanAmount) AS SUMAMOUNT FROM Paymentdata WHERE (Frm_date = @Bill_frmdate) AND (To_date = @Bill_todate) AND (Plant_code = @Plant_code) GROUP BY Plant_name, Plant_code) AS table1 RIGHT OUTER JOIN (SELECT Plant_Code, SUM(GrossAmount) AS Tamt FROM Truck_Present WHERE (Plant_Code = @Plant_code) AND (Pdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS table2 ON table1.Plant_code = table2.Plant_Code) AS table3) AS table4 RIGHT OUTER JOIN (SELECT        CAST((ISNULL(t2.dp_MILKKG, 0) + ISNULL(derivedtbl_1.cl_MILKKG, 0)) - (ISNULL(t1.pr_MILKKG, 0) + ISNULL(t3.op_MILKKG, 0)) AS DECIMAL(18, 2)) AS Gainmilkkg, t1.pcode, CAST((ISNULL(t2.dp_FATKG, 0) + ISNULL(derivedtbl_1.cl_FATKG, 0)) - (ISNULL(t1.pr_FATKG, 0) + ISNULL(t3.op_FATKG, 0)) AS DECIMAL(18, 2)) AS GainFatkg, CAST((ISNULL(t2.dp_SNFKG, 0) + ISNULL(derivedtbl_1.cl_SNFKG, 0))-(ISNULL(t1.pr_SNFKG, 0) + ISNULL(t3.op_SNFKG, 0)) AS DECIMAL(18, 2)) AS GainSnfkg FROM (SELECT  Plant_Code AS pcode, SUM(Milk_kg) AS pr_MILKKG, AVG(Fat) AS pr_FAT, AVG(Snf) AS pr_SNF, SUM(fat_kg) AS pr_FATKG,SUM(snf_kg) AS pr_SNFKG, SUM(Rate) AS pr_RATE, SUM(Amount) AS pr_AMOUNT FROM Procurement WHERE (Plant_Code = @Plant_code) AND (Prdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS t1 CROSS JOIN (SELECT SUM(MilkKg) AS dp_MILKKG, AVG(Fat) AS dp_FAT, AVG(Snf) AS dp_SNF, SUM(Fat_Kg) AS dp_FATKG, SUM(Snf_Kg) AS dp_SNFKG, SUM(Rate) AS dp_RATE, SUM(Amount) AS dp_AMOUNT  FROM Despatchnew WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate) AND (Plant_code = @Plant_code)) AS t2 CROSS JOIN (SELECT SUM(MilkKg) AS op_MILKKG, AVG(Fat) AS op_FAT, AVG(Snf) AS op_SNF, SUM(Fat_Kg) AS op_FATKG, SUM(Snf_Kg) AS op_SNFKG, SUM(Rate) AS op_RATE, SUM(Amount) AS op_AMOUNT FROM Stock_openingmilk WHERE (Datee BETWEEN @Bill_frmdate AND @Bill_todate) AND (Plant_code = @Plant_code)) AS t3 CROSS JOIN (SELECT SUM(MilkKg) AS cl_MILKKG, AVG(Fat) AS cl_FAT, AVG(Snf) AS cl_SNF, SUM(Fat_Kg) AS cl_FATKG, SUM(Snf_Kg) AS cl_SNFKG, SUM(Rate) AS cl_RATE, SUM(Amount) AS cl_AMOUNT  FROM Stock_Milk  WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate) AND (Plant_code = @Plant_code)) AS derivedtbl_1) AS Table6_1 ON  Table6_1.pcode = table4.Plant_code) AS Table6");
                            //advance billdate query   cmd = new SqlCommand("SELECT UPPER(LEFT(DATENAME(MONTH, Bill_frmdate), 3)) AS MMM, UPPER(LEFT(DATENAME(MONTH, Bill_todate), 3)) AS MMM1, YEAR(Bill_frmdate) AS fromyear,YEAR(Bill_todate) AS toyear FROM Bill_date WHERE  (Plant_Code = @Plant_Code) AND (YEAR(Bill_frmdate) BETWEEN @d1 AND @d2) AND (YEAR(Bill_todate) BETWEEN @d1 AND @d2) GROUP BY YEAR(Bill_frmdate), DATENAME(MONTH, Bill_frmdate), YEAR(Bill_todate), DATENAME(MONTH, Bill_todate) ORDER BY fromyear");
                            cmd.Parameters.Add("@Bill_frmdate", drr["Bill_frmdate"].ToString());
                            cmd.Parameters.Add("@Bill_todate", drr["Bill_todate"].ToString());
                            cmd.Parameters.Add("@Plant_code", plantcode);
                            DataTable dtdata = vdm.SelectQuery(cmd).Tables[0];
                            DataRow ksdr = null;
                            DataRow ksdr1 = null;
                            string[] strarr1 = new string[] { "TotalKgs", "TotalLiters", "PerDayLtr", "LtrCost", "KgFat", "AvgFat", "KgSnf", "AvgSnf", "TS", "MilkValueAmount", "LoanDeduction", "LoanPerltrDeduction", "FeedDeduction", "NetBillAmount", "TransportAmount", "TransportLtrCost", "ProcureValueTSRate", "TransportTSRate", "ChillingCostPerLtr", "ChillingCostTsRate", "TotTsRate", "KgMilkGain", "KgFatGain", "kgSnfGain", "TotalFatandSnfGain", "FatGainPerltr", "SnfGainPerltr", "TotalFAndSGainpercentLtr", "AvgFatWithGainFatperltr", "AvgSnfWithGainSnfperltr", "TotAvgSnfANDFatPerltr", "FatCost@175", "SnfCost@150", "TotFatCostAndSnfCost", "FatCostLtr", "SnfCostLtr", "TotFatAndSnfCostLtr", "FatANDSnfPerTS", "TotalCost", "NetTsRate" };//52
                            //string[] strarr1 = new string[] { "Total Kgs", "AvgFat ", "AvgSnf", "Total Solid TS(Kg Fat Rate)", "LtrCost", "ProcureVale TSRate", "Transport Ltr Cost", "Transport TS Rate", "ChillCost TSRate", "Total TS Rate", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %",  "Net_TSRate" };//0-23
                            if (count == 0)
                            {
                                for (int i = 0; i < strarr1.Length; i++)
                                {
                                    ksdr = Report.NewRow();
                                    ksdr[0] = strarr1[i].ToString();
                                    int j = 1;
                                    foreach (DataRow dr2 in dtdata.Rows)
                                    {
                                        ksdr[j] = dr2[i].ToString();
                                        j++;
                                    }
                                    Report.Rows.Add(ksdr);
                                }
                                count++;

                            }
                            else
                            {
                                if (count == 1)
                                {

                                    //ksdr1 = Report.NewRow();

                                    // ksdr[1] = strarr1[i].ToString();

                                    DataRow dr1;
                                    foreach (DataRow dr3 in dtdata.Rows)
                                    {
                                        for (int i = 0; i < strarr1.Length; i++)
                                        {
                                            dr1 = Report.Rows[i];// search whole table 
                                            //  dr["Product_name"] = "cde"; //change the name
                                            dr1[newrow1] = dr3[i].ToString();
                                            // j++;
                                        }

                                    }
                                    //Report.Rows.Add(ksdr1);
                                    newrow1++;
                                }
                            }
                        }
                    }
                }
                
                //else
                //{
                //    cmd = new SqlCommand("SELECT Plant_Code FROM Bill_date WHERE (Bill_frmdate BETWEEN @d1 AND @d2) AND (Bill_todate BETWEEN @d1 AND @d2)  GROUP BY Plant_Code");
                //    cmd.Parameters.Add("@d1", fromdate);
                //    cmd.Parameters.Add("@d2", todate);
                //   // cmd.Parameters.Add("@Plant_Code", plantcode);
                //    DataTable dtbilldate = vdm.SelectQuery(cmd).Tables[0];
                //    foreach (DataRow dr in dtbilldate.Rows)
                //    {
                //        string plantCode = dr["Plant_Code"].ToString();
                //        Report.Columns.Add(plantCode);
                //    }
                //    if (dtbilldate.Rows.Count > 0)
                //    {
                //        foreach (DataRow drr in dtbilldate.Rows)
                //        {
                //            cmd = new SqlCommand("SELECT Plant_code, Plant_name, summilkkg, milkltrs, perdayliters, fatkg, AvgFat, avgsnf, Snfkg, TS, CLR, LtrCost, LoanDeduction, LoanPerltrDeduction, AMOUNT, feed, Cans, SUMAMOUNT, Tamt, TransportLtrCost, ProcureValueTSRate, TransportTSRate, Gainmilkkg, pcode, GainFatkg, GainSnfkg, FatGainPerltr,TOtfatSnfgain, SnfGainPerltr, TotalFAndSGainpercentLtr, AvgFatWithGainFatperltr, AvgSnfWithGainSnfperltr, TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr,FatCost, SnfCost, TotFatCostAndSnfCost, FatCostLtr, SnfCostLtr, TotFatAndSnfCostLtr, FatANdSnfPerLtrTS, TotalCost, ChillingCostTsRate, TotTsRate,NETTsRate FROM (SELECT table4.Plant_code, table4.Plant_name, table4.summilkkg, table4.milkltrs, table4.perdayliters, table4.fatkg, table4.AvgFat, table4.avgsnf, table4.Snfkg,table4.TS, table4.CLR, table4.LtrCost, table4.LoanDeduction, table4.LoanPerltrDeduction, table4.AMOUNT, table4.feed, table4.Cans, table4.totdeduction,table4.netamount, table4.SUMAMOUNT, table4.Tamt, table4.TransportLtrCost, table4.ProcureValueTSRate, table4.TransportTSRate, Table6_1.Gainmilkkg,Table6_1.pcode, Table6_1.GainFatkg, Table6_1.GainSnfkg, Table6_1.GainFatkg / table4.summilkkg * 100 AS FatGainPerltr,Table6_1.GainFatkg + Table6_1.GainSnfkg AS TOtfatSnfgain, Table6_1.GainSnfkg / table4.summilkkg * 100 AS SnfGainPerltr,Table6_1.GainFatkg / table4.summilkkg * 100 + Table6_1.GainSnfkg / table4.summilkkg * 100 AS TotalFAndSGainpercentLtr,table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100 AS AvgFatWithGainFatperltr,table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100 AS AvgSnfWithGainSnfperltr, (table4.AvgFat + Table6_1.GainFatkg / table4.summilkkg * 100)+ (table4.avgsnf + Table6_1.GainSnfkg / table4.summilkkg * 100) AS TotAvgFatWithGainFatperltrAndAvgSnfWithGainSnfperltr, Table6_1.GainFatkg * 175 AS FatCost, Table6_1.GainSnfkg * 150 AS SnfCost,Table6_1.GainFatkg * 175 + Table6_1.GainSnfkg * 150 AS TotFatCostAndSnfCost, Table6_1.GainFatkg * 175 / table4.summilkkg AS FatCostLtr, Table6_1.GainSnfkg * 150 / table4.summilkkg AS SnfCostLtr, Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg AS TotFatAndSnfCostLtr,(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS FatANdSnfPerLtrTS,table4.TransportLtrCost + 1 + table4.LtrCost AS TotalCost, 1 / table4.TS AS ChillingCostTsRate,table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate AS TotTsRate,(table4.ProcureValueTSRate + table4.TransportLtrCost + 1 + table4.LtrCost + table4.TransportTSRate)-(Table6_1.GainFatkg * 175 / table4.summilkkg + Table6_1.GainSnfkg * 150 / table4.summilkkg) / table4.TS AS NETTsRate FROM (SELECT Plant_code, Plant_name, summilkkg, milkltrs, perdayliters, fatkg, AvgFat, avgsnf, Snfkg, TS, CLR, LtrCost, LoanDeduction, LoanPerltrDeduction, AMOUNT, feed, Cans, totdeduction, netamount, SUMAMOUNT, Tamt, TransportLtrCost, ProcureValueTSRate,TransportTSRate FROM (SELECT table1.Plant_code, table1.Plant_name, table1.summilkkg, table1.milkltrs, table1.perdayliters, table1.fatkg, table1.AvgFat, table1.avgsnf, table1.Snfkg, table1.TS, table1.CLR, table1.LtrCost, table1.LoanDeduction, table1.LoanPerltrDeduction,table1.AMOUNT, table1.feed, table1.Cans, table1.totdeduction, table1.netamount, table1.SUMAMOUNT, table2.Tamt,table2.Tamt / table1.milkltrs AS TransportLtrCost, table1.LtrCost / table1.TS AS ProcureValueTSRate,table2.Tamt / table1.milkltrs / table1.TS AS TransportTSRate FROM (SELECT Plant_code, Plant_name, SUM(Smkg) AS summilkkg, SUM(Smltr) AS milkltrs, SUM(Smltr) / COUNT(Company_code)  AS perdayliters, SUM(Sfatkg) AS fatkg, SUM(Sfatkg) / SUM(Smkg) * 100 AS AvgFat, SUM(SSnfkg) / SUM(Smkg) * 100 AS avgsnf, SUM(SSnfkg) AS Snfkg, SUM(Sfatkg) / SUM(Smkg) * 100 + SUM(SSnfkg) / SUM(Smkg) * 100 AS TS,SUM(Aclr) AS CLR, SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) / SUM(Smltr) AS LtrCost, SUM(Sinstamt) AS LoanDeduction, SUM(Sinstamt) / SUM(Smltr) AS LoanPerltrDeduction,  SUM(SAmt + SInsentAmt + Scaramt + SSplBonus + ClaimAount) AS AMOUNT, SUM(Feed) AS feed, SUM(can) AS Cans, SUM(TotDeductions) AS totdeduction, SUM(NetAmount) AS netamount, SUM(SLoanAmount) AS SUMAMOUNT FROM  Paymentdata WHERE (Frm_date BETWEEN @Bill_frmdate AND @Bill_todate) AND (To_date BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_name, Plant_code) AS table1 RIGHT OUTER JOIN (SELECT  Plant_Code, SUM(GrossAmount) AS Tamt FROM Truck_Present WHERE (Pdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS table2 ON table1.Plant_code = table2.Plant_Code) AS table3) AS table4 RIGHT OUTER JOIN (SELECT CAST((ISNULL(t2.dp_MILKKG, 0) + ISNULL(derivedtbl_1.cl_MILKKG, 0)) - (ISNULL(t1.pr_MILKKG, 0) + ISNULL(t3.op_MILKKG, 0)) AS DECIMAL(18, 2)) AS Gainmilkkg, t1.pcode, CAST((ISNULL(t2.dp_FATKG, 0) + ISNULL(derivedtbl_1.cl_FATKG, 0)) - (ISNULL(t1.pr_FATKG, 0) + ISNULL(t3.op_FATKG, 0)) AS DECIMAL(18, 2)) AS GainFatkg, CAST((ISNULL(t2.dp_SNFKG, 0) + ISNULL(derivedtbl_1.cl_SNFKG, 0)) - (ISNULL(t1.pr_SNFKG, 0) + ISNULL(t3.op_SNFKG, 0)) AS DECIMAL(18, 2)) AS GainSnfkg FROM   (SELECT Plant_Code AS pcode, SUM(Milk_kg) AS pr_MILKKG, AVG(Fat) AS pr_FAT, AVG(Snf) AS pr_SNF, SUM(fat_kg) AS pr_FATKG, SUM(snf_kg) AS pr_SNFKG, SUM(Rate) AS pr_RATE, SUM(Amount) AS pr_AMOUNT  FROM Procurement WHERE (Prdate BETWEEN @Bill_frmdate AND @Bill_todate) GROUP BY Plant_Code) AS t1 CROSS JOIN (SELECT SUM(MilkKg) AS dp_MILKKG, AVG(Fat) AS dp_FAT, AVG(Snf) AS dp_SNF, SUM(Fat_Kg) AS dp_FATKG, SUM(Snf_Kg)  AS dp_SNFKG, SUM(Rate) AS dp_RATE, SUM(Amount) AS dp_AMOUNT FROM Despatchnew WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate)) AS t2 CROSS JOIN (SELECT SUM(MilkKg) AS op_MILKKG, AVG(Fat) AS op_FAT, AVG(Snf) AS op_SNF, SUM(Fat_Kg) AS op_FATKG, SUM(Snf_Kg) AS op_SNFKG, SUM(Rate) AS op_RATE, SUM(Amount) AS op_AMOUNT FROM Stock_openingmilk WHERE (Datee BETWEEN @Bill_frmdate AND @Bill_todate)) AS t3 CROSS JOIN (SELECT SUM(MilkKg) AS cl_MILKKG, AVG(Fat) AS cl_FAT, AVG(Snf) AS cl_SNF, SUM(Fat_Kg) AS cl_FATKG, SUM(Snf_Kg) AS cl_SNFKG, SUM(Rate) AS cl_RATE, SUM(Amount) AS cl_AMOUNT FROM Stock_Milk WHERE (Date BETWEEN @Bill_frmdate AND @Bill_todate)) AS derivedtbl_1) AS Table6_1 ON Table6_1.pcode = table4.Plant_code)  AS Table6");
                //            cmd.Parameters.Add("@Bill_frmdate", fromdate);
                //            cmd.Parameters.Add("@Bill_todate", todate);
                //            //cmd.Parameters.Add("@Plant_code", plantcode);
                //            DataTable dtdata = vdm.SelectQuery(cmd).Tables[0];
                //            DataRow ksdr = null;
                //            DataRow ksdr1 = null;
                //            string[] strarr1 = new string[] { "TotalKgs","TotalLiters","PerDayLtr","LtrCost","KgFat","AvgFat","KgSnf","AvgSnf","TS","MilkValueAmount","LoanDeduction","LoanPerltrDeduction","FeedDeduction","NetBillAmount","TransportAmount","TransportLtrCost","ProcureValueTSRate","TransportTSRate","ChillingCostPerLtr","ChillingCostTsRate","TotTsRate","KgMilkGain","KgFatGain","kgSnfGain","TotalFatandSnfGain","FatGainPerltr","SnfGainPerltr","TotalFAndSGainpercentLtr","AvgFatWithGainFatperltr","AvgSnfWithGainSnfperltr","TotAvgSnfANDFatPerltr","FatCost@175","SnfCost@150","TotFatCostAndSnfCost","FatCostLtr","SnfCostLtr","TotFatAndSnfCostLtr","FatANDSnfPerTS","TotalCost","NetTsRate","Plant_code","Plant_name","totdeduction" };//52
                //            //string[] strarr1 = new string[] { "Total Kgs", "AvgFat ", "AvgSnf", "Total Solid TS(Kg Fat Rate)", "LtrCost", "ProcureVale TSRate", "Transport Ltr Cost", "Transport TS Rate", "ChillCost TSRate", "Total TS Rate", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %",  "Net_TSRate" };//0-23
                //            if (count == 0)
                //            {
                //                for (int i = 0; i < strarr1.Length; i++)
                //                {
                //                    ksdr = Report.NewRow();
                //                    ksdr[0] = strarr1[i].ToString();
                //                    int j = 1;
                //                    foreach (DataRow dr2 in dtdata.Rows)
                //                    {
                //                        ksdr[j] = dr2[i].ToString();
                //                        j++;
                //                    }
                //                    Report.Rows.Add(ksdr);
                //                }
                //                count++;

                //            }
                //            else
                //            {
                //                if (count == 1)
                //                {

                //                    //ksdr1 = Report.NewRow();

                //                    // ksdr[1] = strarr1[i].ToString();

                //                    DataRow dr;
                //                    foreach (DataRow dr3 in dtdata.Rows)
                //                    {
                //                        for (int i = 0; i < strarr1.Length; i++)
                //                        {
                //                            dr = Report.Rows[i];// search whole table 
                //                            //  dr["Product_name"] = "cde"; //change the name
                //                            dr[newrow] = dr3[i].ToString();
                //                            // j++;
                //                        }

                //                    }
                //                    //Report.Rows.Add(ksdr1);
                //                    newrow++;
                //                }

                //            }
                //        }
                //    }
                //}

                grdReports.DataSource = Report;
                grdReports.DataBind();
                hidepanel.Visible = true;
                lblFromDate.Text = mymonth;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void grdReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}