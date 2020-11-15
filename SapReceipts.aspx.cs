using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class SapReceipts : System.Web.UI.Page
{
    SqlCommand cmd;
    string UserName = "";
    SalesDBManager vdm;
    string getledger;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["salestype"] == null)
        //{
        //    Response.Redirect("Login.aspx");
        //}
        //UserName = Session["field1"].ToString();
        //vdm = new SalesDBManager();
        if (!this.IsPostBack)
        {
            if (!Page.IsCallback)
            {
                txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                dtp_Todate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                lblTitle.Text = Session["TitleName"].ToString();
                FillRouteName();
            }
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    { 
        GetReport();
    }
    void FillRouteName()
    {
        vdm = new SalesDBManager();
        vdm = new SalesDBManager();
        if (Session["LevelType"].ToString() == "2" || Session["LevelType"].ToString() == "1")
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE (Plant_Code = @BranchID)");
            cmd.Parameters.Add("@BranchID", Session["branch"].ToString());
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlSalesOffice.DataSource = dtPlant;
            ddlSalesOffice.DataTextField = "Plant_Name";
            ddlSalesOffice.DataValueField = "Plant_Code";
            ddlSalesOffice.DataBind();
        }
        else
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master ");
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlSalesOffice.DataSource = dtPlant;
            ddlSalesOffice.DataTextField = "Plant_Name";
            ddlSalesOffice.DataValueField = "Plant_Code";
            ddlSalesOffice.DataBind();
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
    private DateTime GetHighDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        Hour = 23 - dt.Hour;
        Min = 59 - dt.Minute;
        Sec = 59 - dt.Second;
        DT = dt;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }
    DataTable dtReport = new DataTable();
    void GetReport()
    {
        try
        {
            lblmsg.Text = "";
            pnlHide.Visible = true;
            Session["RouteName"] = ddlSalesOffice.SelectedItem.Text;
            Session["IDate"] = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            vdm = new SalesDBManager();
            DateTime fromdate = DateTime.Now;
            DateTime todate = DateTime.Now;
            string[] dateFromstrig = txtFromdate.Text.Split(' ');
            if (dateFromstrig.Length > 1)
            {
                if (dateFromstrig[0].Split('-').Length > 0)
                {
                    string[] dates = dateFromstrig[0].Split('-');
                    string[] times = dateFromstrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            dateFromstrig = dtp_Todate.Text.Split(' ');
            if (dateFromstrig.Length > 1)
            {
                if (dateFromstrig[0].Split('-').Length > 0)
                {
                    string[] dates = dateFromstrig[0].Split('-');
                    string[] times = dateFromstrig[1].Split(':');
                    todate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            DateTime ReportDate = SalesDBManager.GetTime(vdm.conn);
            DateTime dtapril = new DateTime();
            DateTime dtmarch = new DateTime();
            int currentyear = ReportDate.Year;
            int nextyear = ReportDate.Year + 1;
            if (ReportDate.Month > 3)
            {
                string apr = "4/1/" + currentyear;
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + nextyear;
                dtmarch = DateTime.Parse(march);
            }
            if (ReportDate.Month <= 3)
            {
                string apr = "4/1/" + (currentyear - 1);
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + (nextyear - 1);
                dtmarch = DateTime.Parse(march);
            }
            DataTable dtReport = new DataTable();
            dtReport.Columns.Add("Voucher Date");
            dtReport.Columns.Add("Voucher No");
            dtReport.Columns.Add("whcode");
            dtReport.Columns.Add("Voucher Type");
            dtReport.Columns.Add("Ledger Code");
            dtReport.Columns.Add("Ledger (Dr)");    
            dtReport.Columns.Add("PaidDate");
            dtReport.Columns.Add("Customer Code");
            dtReport.Columns.Add("Ledger (Cr)");
            dtReport.Columns.Add("Amount");
            dtReport.Columns.Add("Narration");
            lbl_selfromdate.Text = fromdate.ToString("dd/MM/yyyy");
            lblRoutName.Text = ddlSalesOffice.SelectedItem.Text;
            Session["xporttype"] = "TallyReceipts";
            Session["filename"] = ddlSalesOffice.SelectedItem.Text + " Tally Receipts" + fromdate.ToString("dd/MM/yyyy");
            cmd = new SqlCommand("SELECT cashreceipts.ReceivedFrom,cashreceipts.BranchId,Plant_Master.ladger_dr_code, cashreceipts.AgentID, cashreceipts.Empid, cashreceipts.Amountpayable, cashreceipts.AmountPaid, cashreceipts.DOE, cashreceipts.Remarks, cashreceipts.Receipt, cashreceipts.PaymentStatus, cashreceipts.TransactionType, cashreceipts.Sno, Plant_Master.Plant_Name,Plant_Master.WHcode, Plant_Master.ladger_dr FROM cashreceipts INNER JOIN Plant_Master ON cashreceipts.BranchId = Plant_Master.Plant_Code WHERE (cashreceipts.DOE BETWEEN @d1 AND @d2) AND (cashreceipts.BranchId = @Branchid)");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(todate));
            DataTable dtothers = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT   cashcollections.LedgerCode,cashcollections.Branchid, Plant_Master.Plant_Name, cashcollections.Name, cashcollections.Amount, cashcollections.Remarks, cashcollections.DOE, cashcollections.Receiptno, cashcollections.PaymentType, cashcollections.CollectionType  FROM cashcollections INNER JOIN Plant_Master ON cashcollections.Branchid = Plant_Master.Plant_Code WHERE (cashcollections.DOE BETWEEN @d1 AND @d2) AND (cashcollections.Branchid = @Branchid)");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(todate));
            DataTable dtreceipts = vdm.SelectQuery(cmd).Tables[0];
            if (dtothers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtothers.Rows)
                {
                    DataRow newrow = dtReport.NewRow();
                    string VoucherNo = dr["Receipt"].ToString();

                    string NewVoucherNo = "0";
                    int countdc = 0;
                    int.TryParse(VoucherNo, out countdc);
                    if (countdc <= 10)
                    {
                        NewVoucherNo = "000" + countdc;
                    }
                    if (countdc >= 10 && countdc <= 99)
                    {
                        NewVoucherNo = "00" + countdc;
                    }
                    if (countdc >= 99 && countdc <= 999)
                    {
                        NewVoucherNo = "0" + countdc;
                    }

                    string Date = dr["DOE"].ToString();
                    DateTime from_date = Convert.ToDateTime(Date);
                    newrow["Voucher Date"] = from_date.ToString("dd-MMM-yyyy");
                    newrow["Voucher No"] = ddlSalesOffice.SelectedValue + dtapril.ToString("yy") + dtmarch.ToString("yy") + NewVoucherNo;
                    int i = 1;
                    newrow["PaidDate"] = from_date.ToString("dd-MMM-yyyy");
                    newrow["whcode"] = dr["WHcode"].ToString();
                    double AmountPaid = 0;
                    double.TryParse(dr["AmountPaid"].ToString(), out AmountPaid);
                    newrow["Amount"] = AmountPaid;
                    newrow["Voucher Date"] = from_date.ToString("dd-MMM-yyyy");
                    string Name = "";
                    foreach (DataRow drcash in dtreceipts.Select("Receiptno='" + dr["Receipt"].ToString() + "' and Branchid='" + dr["BranchId"].ToString() + "'"))
                    {
                        newrow["Voucher Type"] = drcash["CollectionType"].ToString();
                        newrow["Ledger (Cr)"] = drcash["Name"].ToString();
                        Name = drcash["Name"].ToString();
                        //newrow["Customer Code"] = drcash["customercode"].ToString();
                        //getledger = newrow["Customer Code"].ToString();
                        newrow["Customer Code"] = drcash["LedgerCode"].ToString();
                    }
                    newrow["whcode"] = dr["whcode"].ToString();
                    newrow["Ledger Code"] = dr["ladger_dr_code"].ToString();
                    newrow["Ledger (Dr)"] = dr["ladger_dr"].ToString();
                    double invval = 0;
                    newrow["Narration"] = "Being the cash receipt to " + Name + " vide Receipt No " + dr["Receipt"].ToString() + ",Receipt Date " + from_date.ToString("dd/MM/yyyy");
                    dtReport.Rows.Add(newrow);
                    i++;
                }
                grdReports.DataSource = dtReport;
                grdReports.DataBind();
                Session["xportdata"] = dtReport;
            }
            else
            {
                pnlHide.Visible = false;
                lblmsg.Text = "No Data Found";
                grdReports.DataSource = dtReport;
                grdReports.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            grdReports.DataSource = dtReport;
            grdReports.DataBind();
        }
    }
    SqlCommand sqlcmd;
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            vdm = new SalesDBManager();
            DateTime CreateDate = SalesDBManager.GetTime(vdm.conn);
            SAPdbmanger SAPvdm = new SAPdbmanger();
            DateTime fromdate = DateTime.Now;
            DataTable dt = (DataTable)Session["xportdata"];
            string[] dateFromstrig = txtFromdate.Text.Split(' ');
            if (dateFromstrig.Length > 1)
            {
                if (dateFromstrig[0].Split('-').Length > 0)
                {
                    string[] dates = dateFromstrig[0].Split('-');
                    string[] times = dateFromstrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                string PaymentMode = "CASH";
                double amount = 0;
                double.TryParse(dr["Amount"].ToString(), out amount);
                string B1Upload = "N";
                string Processed = "N";
                string customercode = dr["Customer Code"].ToString();
                string date = dr["Voucher Date"].ToString();
                DateTime from_date = Convert.ToDateTime(date);
                if (customercode.Length <= 7)
                {
                    sqlcmd = new SqlCommand("Insert into EMROJDTP (CreateDate, RefDate, DocDate, TransNo, TransCode, AcctCode, AcctName, Debit, Credit, B1Upload, Processed,Ref1,OcrCode,Remarks,series) values (@CreateDate, @RefDate, @DocDate,@TransNo,@TransCode, @AcctCode, @AcctName, @Debit, @Credit, @B1Upload, @Processed,@Ref1,@OcrCode,@Remarks,@series)");
                    sqlcmd.Parameters.Add("@CreateDate", CreateDate);
                    sqlcmd.Parameters.Add("@RefDate", GetLowDate(from_date));
                    sqlcmd.Parameters.Add("@docdate", GetLowDate(from_date));
                    sqlcmd.Parameters.Add("@Ref1", dr["Voucher No"].ToString());
                    string TransCode = "T1";
                    sqlcmd.Parameters.Add("@TransNo", dr["Voucher No"].ToString());
                    sqlcmd.Parameters.Add("@TransCode", TransCode);
                    sqlcmd.Parameters.Add("@AcctCode", dr["Ledger Code"].ToString());
                    sqlcmd.Parameters.Add("@AcctName", dr["Ledger (Dr)"].ToString());
                    double.TryParse(dr["Amount"].ToString(), out amount);
                    sqlcmd.Parameters.Add("@Debit", amount);
                    string Creditamount = "0";
                    sqlcmd.Parameters.Add("@Credit", Creditamount);
                    sqlcmd.Parameters.Add("@B1Upload", B1Upload);
                    sqlcmd.Parameters.Add("@Processed", Processed);
                    sqlcmd.Parameters.Add("@OcrCode", dr["whcode"].ToString());
                    sqlcmd.Parameters.Add("@Remarks", dr["Narration"].ToString());
                    string series = "232";
                    sqlcmd.Parameters.Add("@series", series);
                    SAPvdm.insert(sqlcmd);
                    sqlcmd = new SqlCommand("Insert into EMROJDTP (CreateDate, RefDate, DocDate, TransNo,TransCode, AcctCode, AcctName, Debit, Credit, B1Upload, Processed,Ref1,OcrCode,Remarks,series) values (@CreateDate, @RefDate, @DocDate,@TransNo,@TransCode, @AcctCode, @AcctName, @Debit, @Credit, @B1Upload, @Processed,@Ref1,@OcrCode,@Remarks,@series)");
                    sqlcmd.Parameters.Add("@CreateDate", CreateDate);
                    sqlcmd.Parameters.Add("@RefDate", GetLowDate(from_date));
                    sqlcmd.Parameters.Add("@docdate", GetLowDate(from_date));
                    sqlcmd.Parameters.Add("@Ref1", dr["Voucher No"].ToString());
                    sqlcmd.Parameters.Add("@TransNo", dr["Voucher No"].ToString());
                    sqlcmd.Parameters.Add("@TransCode", TransCode);
                    sqlcmd.Parameters.Add("@AcctCode", dr["Customer Code"].ToString());
                    sqlcmd.Parameters.Add("@AcctName", dr["Ledger (Cr)"].ToString());
                    string Debitamount = "0";
                    sqlcmd.Parameters.Add("@Debit", Debitamount);
                    sqlcmd.Parameters.Add("@Credit", amount);
                    sqlcmd.Parameters.Add("@B1Upload", B1Upload);
                    sqlcmd.Parameters.Add("@Processed", Processed);
                    sqlcmd.Parameters.Add("@OcrCode", dr["whcode"].ToString());
                    sqlcmd.Parameters.Add("@Remarks", dr["Narration"].ToString());
                    sqlcmd.Parameters.Add("@series", series);
                    SAPvdm.insert(sqlcmd);
                }
                else
                {
                    sqlcmd = new SqlCommand("Insert into EMRORCT (CreateDate,PaymentDate,DOE,ReferenceNo,CardCode,Remarks,PaymentMode,PaymentSum,OcrCode,B1Upload,Processed,AcctNo,series) values(@CreateDate,@PaymentDate,@DOE,@ReferenceNo,@CardCode,@Remarks,@PaymentMode,@PaymentSum,@OcrCode,@B1Upload,@Processed,@AcctNo,@series)");
                    sqlcmd.Parameters.Add("@CreateDate", CreateDate);
                    sqlcmd.Parameters.Add("@PaymentDate", GetLowDate(from_date));
                    sqlcmd.Parameters.Add("@DOE", GetLowDate(from_date));
                    sqlcmd.Parameters.Add("@ReferenceNo", dr["Voucher No"].ToString());
                    sqlcmd.Parameters.Add("@CardCode", dr["Customer Code"].ToString());
                    sqlcmd.Parameters.Add("@Remarks", dr["Narration"].ToString());
                    //sqlcmd.Parameters.Add("@InvoiceNo", dr["Voucher No"].ToString());
                    sqlcmd.Parameters.Add("@PaymentMode", PaymentMode);
                    sqlcmd.Parameters.Add("@PaymentSum", amount);
                    sqlcmd.Parameters.Add("@OcrCode", dr["whcode"].ToString());
                    sqlcmd.Parameters.Add("@B1Upload", B1Upload);
                    sqlcmd.Parameters.Add("@Processed", Processed);
                    sqlcmd.Parameters.Add("@AcctNo", dr["Ledger Code"].ToString());
                    string series = "227";
                    sqlcmd.Parameters.Add("@series", series);
                    SAPvdm.insert(sqlcmd);
                }
            }
            //foreach (DataRow dr in dt.Rows)
            //{
            //    string PaymentMode = "CASH";
            //    double amount = 0;
            //    double.TryParse(dr["Amount"].ToString(), out amount);
            //    string B1Upload = "N";
            //    string Processed = "N";
            //    sqlcmd = new SqlCommand("Insert into EMRORCT (CreateDate,PaymentDate,DOE,ReferenceNo,CardCode,Remarks,InvoiceNo,PaymentMode,PaymentSum,OcrCode,B1Upload,Processed,AcctNo) values(@CreateDate,@PaymentDate,@DOE,@ReferenceNo,@CardCode,@Remarks,@InvoiceNo,@PaymentMode,@PaymentSum,@OcrCode,@B1Upload,@Processed,@AcctNo)");
            //    sqlcmd.Parameters.Add("@CreateDate", GetLowDate(fromdate));
            //    sqlcmd.Parameters.Add("@PaymentDate", GetLowDate(fromdate));
            //    sqlcmd.Parameters.Add("@DOE", GetLowDate(fromdate));
            //    sqlcmd.Parameters.Add("@ReferenceNo", dr["Voucher No"].ToString());
            //    sqlcmd.Parameters.Add("@CardCode", dr["Customer Code"].ToString());
            //    sqlcmd.Parameters.Add("@Remarks", dr["Narration"].ToString());
            //    sqlcmd.Parameters.Add("@InvoiceNo", dr["Voucher No"].ToString());
            //    sqlcmd.Parameters.Add("@PaymentMode", PaymentMode);
            //    sqlcmd.Parameters.Add("@PaymentSum", amount);
            //    sqlcmd.Parameters.Add("@OcrCode", dr["whcode"].ToString());
            //    sqlcmd.Parameters.Add("@B1Upload", B1Upload);
            //    sqlcmd.Parameters.Add("@Processed", Processed);
            //    sqlcmd.Parameters.Add("@AcctNo", dr["Ledger Code"].ToString());
            //    SAPvdm.insert(sqlcmd);
            //}
            pnlHide.Visible = false;
            DataTable dtempty = new DataTable();
            grdReports.DataSource = dtempty;
            grdReports.DataBind();
            lblmsg.Text = "Successfully Saved";
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.ToString();
        }
    }
}