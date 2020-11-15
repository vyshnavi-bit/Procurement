using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TallyPayments : System.Web.UI.Page
{
     SqlCommand cmd;
    string UserName = "";
    SalesDBManager vdm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["branch"] == null)
        {
            Response.Redirect("LoginDefault.aspx");
        }
        if (!this.IsPostBack)
        {
            if (!Page.IsCallback)
            {
                txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
        cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master ");
        DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
        ddlSalesOffice.DataSource = dtPlant;
        ddlSalesOffice.DataTextField = "Plant_Name";
        ddlSalesOffice.DataValueField = "Plant_Code";
        ddlSalesOffice.DataBind();
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
            DataTable Report = new DataTable();
            Report.Columns.Add("DOE");
            Report.Columns.Add("Ref Receipt");
            Report.Columns.Add("Receipt");
            Report.Columns.Add("Type");
            Report.Columns.Add("Name");
            Report.Columns.Add("Amount").DataType = typeof(Double);
            lbl_selfromdate.Text = fromdate.ToString("dd/MM/yyyy");
            lblRoutName.Text = ddlSalesOffice.SelectedItem.Text;
            DateTime ReportDate = SalesDBManager.GetTime(vdm.conn);
            DateTime dtapril = new DateTime();
            DateTime dtmarch = new DateTime();
            int currentyear = ReportDate.Year;
            int nextyear = ReportDate.Year + 1;
            string pdate = "03/31/" + currentyear + "";
            DateTime dtpdate = Convert.ToDateTime(pdate);
            string cdate = GetLowDate(fromdate).ToString("MM/dd/yyyy");
            DateTime dtcdate = Convert.ToDateTime(cdate);
            if (dtcdate > dtpdate)
            {

            }
            else
            {
                currentyear = currentyear - 1;
                nextyear = currentyear + 1;
            }
            if (ReportDate.Month > 3)
            {
                string apr = "4/1/" + currentyear;
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + nextyear;
                dtmarch = DateTime.Parse(march);
            }
            if (ReportDate.Month <= 3)
            {
                string apr = "4/1/" + (currentyear);
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + (nextyear);
                dtmarch = DateTime.Parse(march);
            }
            Session["xporttype"] = "TallyPayments";
            string ledger = "";
            Session["filename"] = ddlSalesOffice.SelectedItem.Text + " Tally Payments" + fromdate.ToString("dd/MM/yyyy");
            cmd = new SqlCommand("SELECT cashpayables.DOE, cashpayables.VocherID, subpayable.vouchercode, cashpayables.Sno, subpayable.HeadSno, cashpayables.Remarks,cashpayables.Remarks AS Expr1, subpayable.sno AS Expr2, subpayable.Amount, accountheads.HeadName, cashpayables.BranchID, Plant_Master.ladger_dr,Plant_Master.WHcode, Plant_Master.ladger_dr_code FROM cashpayables INNER JOIN subpayable ON cashpayables.Sno = subpayable.RefNo INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno INNER JOIN Plant_Master ON cashpayables.BranchID = Plant_Master.Plant_Code WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.DOE BETWEEN @d1 AND @d2) AND (cashpayables.VoucherType = 'Debit') AND  (cashpayables.Status = 'P')");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(ReportDate));
            DataTable dtAgent = vdm.SelectQuery(cmd).Tables[0];
            if (dtAgent.Rows.Count > 0)
            {
                DataView view = new DataView(dtAgent);
                dtReport = new DataTable();
                dtReport.Columns.Add("Voucher Date");
                dtReport.Columns.Add("Voucher No");
                dtReport.Columns.Add("Voucher Type");
                dtReport.Columns.Add("Ledger (Cr)");
                dtReport.Columns.Add("Ledger (Dr)");
                dtReport.Columns.Add("Amount");
                dtReport.Columns.Add("Narration");
                int i = 1;
                foreach (DataRow branch in dtAgent.Rows)
                {
                    string VoucherNo = "";
                    cmd = new SqlCommand("SELECT  vouchercode,RefNo, HeadDesc, Amount, HeadSno, sno, branchid, paiddate FROM subpayable  WHERE (branchid = @BranchID) AND (paiddate BETWEEN @d1 AND @d2)");
                    cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
                    cmd.Parameters.Add("@d1", GetLowDate(fromdate));
                    cmd.Parameters.Add("@d2", GetLowDate(fromdate));
                    DataTable dtVoucher = vdm.SelectQuery(cmd).Tables[0];
                    if (dtVoucher.Rows.Count > 0)
                    {
                        DataRow[] drvoucher = dtVoucher.Select("branchid='" + ddlSalesOffice.SelectedValue + "' and RefNo='" + branch["sno"].ToString() + "' and HeadSno='" + branch["HeadSno"].ToString() + "'");
                        if (drvoucher.Length > 0)
                        {
                            foreach (DataRow drv in drvoucher)
                            {
                                VoucherNo = drv.ItemArray[0].ToString();
                            }
                        }
                        else
                        {
                            cmd = new SqlCommand("SELECT  ISNULL(MAX(vouchercode), 0) + 1 AS Sno FROM subpayable WHERE (branchid = @branchid)  AND (paiddate BETWEEN @d1 AND @d2)");
                            cmd.Parameters.Add("@branchid", ddlSalesOffice.SelectedValue);
                            cmd.Parameters.Add("@HeadSno", branch["HeadSno"].ToString());
                            cmd.Parameters.Add("@d1", GetLowDate(dtapril.AddDays(-1)));
                            cmd.Parameters.Add("@d2", GetHighDate(dtmarch.AddDays(-1)));
                            DataTable dtvoucherno = vdm.SelectQuery(cmd).Tables[0];
                            VoucherNo = dtvoucherno.Rows[0]["Sno"].ToString();
                            cmd = new SqlCommand("update  subpayable set vouchercode=@vouchercode, paiddate=@paiddate,branchid=@branchid  where (RefNo=@RefNo) AND (HeadSno = @HeadSno)"); ;
                            cmd.Parameters.Add("@vouchercode", VoucherNo);
                            cmd.Parameters.Add("@paiddate", fromdate);
                            cmd.Parameters.Add("@branchid", ddlSalesOffice.SelectedValue);
                            cmd.Parameters.Add("@RefNo", branch["sno"].ToString());
                            cmd.Parameters.Add("@HeadSno", branch["HeadSno"].ToString());
                            vdm.Update(cmd);
                        }
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT ISNULL(MAX(vouchercode), 0) + 1 AS Sno FROM subpayable WHERE (branchid = @branchid)  AND (paiddate BETWEEN @d1 AND @d2)");
                        cmd.Parameters.Add("@branchid", ddlSalesOffice.SelectedValue);
                        cmd.Parameters.Add("@HeadSno", branch["HeadSno"].ToString());
                        cmd.Parameters.Add("@d1", GetLowDate(dtapril.AddDays(-1)));
                        cmd.Parameters.Add("@d2", GetHighDate(dtmarch.AddDays(-1)));
                        DataTable dtvoucherno = vdm.SelectQuery(cmd).Tables[0];
                        VoucherNo = dtvoucherno.Rows[0]["Sno"].ToString();
                        cmd = new SqlCommand("update  subpayable set vouchercode=@vouchercode, paiddate=@paiddate,branchid=@branchid  where  (RefNo=@RefNo) AND (HeadSno = @HeadSno)"); ;
                        cmd.Parameters.Add("@vouchercode", VoucherNo);
                        cmd.Parameters.Add("@paiddate", fromdate);
                        cmd.Parameters.Add("@branchid", ddlSalesOffice.SelectedValue);
                        cmd.Parameters.Add("@RefNo", branch["sno"].ToString());
                        cmd.Parameters.Add("@HeadSno", branch["HeadSno"].ToString());
                        vdm.Update(cmd);
                    }
                    if (VoucherNo == "0")
                    {
                        cmd = new SqlCommand("SELECT IFNULL(MAX(vouchercode), 0) + 1 AS Sno FROM subpayable WHERE (branchid = @branchid)  AND (paiddate BETWEEN @d1 AND @d2)");
                        cmd.Parameters.Add("@branchid", ddlSalesOffice.SelectedValue);
                        cmd.Parameters.Add("@HeadSno", branch["HeadSno"].ToString());
                        cmd.Parameters.Add("@d1", GetLowDate(dtapril.AddDays(-1)));
                        cmd.Parameters.Add("@d2", GetHighDate(dtmarch.AddDays(-1)));
                        DataTable dtvoucherno = vdm.SelectQuery(cmd).Tables[0];
                        VoucherNo = dtvoucherno.Rows[0]["Sno"].ToString();
                        cmd = new SqlCommand("update  subpayable set vouchercode=@vouchercode, paiddate=@paiddate,branchid=@branchid  where  (RefNo=@RefNo) AND (HeadSno = @HeadSno)"); ;
                        cmd.Parameters.Add("@vouchercode", VoucherNo);
                        cmd.Parameters.Add("@paiddate", fromdate);
                        cmd.Parameters.Add("@branchid", ddlSalesOffice.SelectedValue);
                        cmd.Parameters.Add("@RefNo", branch["sno"].ToString());
                        cmd.Parameters.Add("@HeadSno", branch["HeadSno"].ToString());
                        vdm.Update(cmd);
                    }
                    string NewVoucherNo = "0";
                    int countdc = 0;
                    int.TryParse(VoucherNo, out countdc);
                    NewVoucherNo = VoucherNo;
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
                    DataRow newrow = dtReport.NewRow();
                    newrow["Voucher Date"] = fromdate.ToString("dd-MMM-yyyy");
                    string VNO = dtapril.ToString("yy") + dtmarch.ToString("yy") + NewVoucherNo;
                    newrow["Voucher No"] = dtapril.ToString("yy") + dtmarch.ToString("yy") + NewVoucherNo;
                    newrow["Voucher Type"] = "Cash Payment Import";
                    newrow["Ledger (Cr)"] = branch["ladger_dr"].ToString();
                    newrow["Ledger (Dr)"] = branch["HeadName"].ToString();
                    newrow["Amount"] = branch["Amount"].ToString();
                    double invval = 0;
                    newrow["Narration"] = branch["Remarks"].ToString() + ",VoucherID  " + VoucherNo + ",Emp Name  " + Session["EmpName"].ToString();
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
                lblmsg.Text = "No Indent Found";
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
    private string GetSpace(string p)
    {
        int i = 0;
        for (; i < p.Length; i++)
        {
            if (char.IsNumber(p[i]))
            {
                break;
            }
        }
        return p.Substring(0, i) + " " + p.Substring(i, p.Length - i);
    }

}