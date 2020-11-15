using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class VoucherApprovalReport : System.Web.UI.Page
{
    SqlCommand cmd;
    string BranchID = "";
    SalesDBManager vdm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["branch"] == null)
        {
            Response.Redirect("LoginDefault.aspx");
        }
        else
        {
            BranchID = Session["branch"].ToString();
        }
        //vdm = new SalesDBManager();
        if (!this.IsPostBack)
        {
            if (!Page.IsCallback)
            {
                lblTitle.Text = Session["TitleName"].ToString();
                txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                txtTodate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                FillRouteName();

            }
        }
    }
    void FillRouteName()
    {
        try
        {
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
        catch
        {
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GetReport();
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
    DataTable Report = new DataTable();
    void GetReport()
    {
        try
        {
            pnlHide.Visible = true;
            vdm = new SalesDBManager();
            lblmsg.Text = "";
            DateTime fromdate = DateTime.Now;
            string[] fromdatestrig = txtFromdate.Text.Split(' ');
            if (fromdatestrig.Length > 1)
            {
                if (fromdatestrig[0].Split('-').Length > 0)
                {
                    string[] dates = fromdatestrig[0].Split('-');
                    string[] times = fromdatestrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            DateTime Todate = DateTime.Now;
            string[] Todatestrig = txtTodate.Text.Split(' ');
            if (Todatestrig.Length > 1)
            {
                if (Todatestrig[0].Split('-').Length > 0)
                {
                    string[] dates = Todatestrig[0].Split('-');
                    string[] times = Todatestrig[1].Split(':');
                    Todate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            Session["filename"] = "Voucher Approval Report";
            string Status = ddlStatus.SelectedValue;
            if (Status == "ALL")
            {
                cmd = new SqlCommand("SELECT VocherID, onNameof,  CONVERT(VARCHAR(10), DOE, 103) AS DOE, Amount, ApprovedAmount, Remarks, ApprovalRemarks, BranchID, Status,  VoucherType, Sno AS refno FROM  cashpayables WHERE (BranchID = @BranchID) AND (DOE BETWEEN @d1 AND @d2) ORDER BY DOE");

            }
            if (Status == "Raised")
            {
                cmd = new SqlCommand("SELECT VocherID, onNameof, CONVERT(VARCHAR(10), DOE, 103) AS DOE, Amount, ApprovedAmount, Remarks, ApprovalRemarks, BranchID, Status, VoucherType, Sno AS refno FROM cashpayables WHERE (BranchID = @BranchID) AND (DOE BETWEEN @d1 AND @d2) AND (Status = @Status) ORDER BY DOE");
                cmd.Parameters.Add("@Status", 'R');
            }
            if (Status == "Approved")
            {
                cmd = new SqlCommand("SELECT VocherID, onNameof,  CONVERT(VARCHAR(10), DOE, 103) AS DOE, Amount, ApprovedAmount, Remarks, ApprovalRemarks, BranchID, Status, VoucherType, Sno AS refno FROM cashpayables WHERE (BranchID = @BranchID) AND (DOE BETWEEN @d1 AND @d2) AND (Status = @Status) ORDER BY DOE");
                cmd.Parameters.Add("@Status", 'A');
            }
            if (Status == "Rejected")
            {
                cmd = new SqlCommand("SELECT VocherID, onNameof,  CONVERT(VARCHAR(10), DOE, 103) AS DOE, Amount, ApprovedAmount, Remarks, ApprovalRemarks, BranchID, Status, VoucherType, Sno AS refno FROM cashpayables WHERE (BranchID = @BranchID) AND (DOE BETWEEN @d1 AND @d2) AND (Status = @Status) ORDER BY DOE");
                cmd.Parameters.Add("@Status", 'C');
            }
            if (Status == "Paid")
            {
                cmd = new SqlCommand("SELECT VocherID, onNameof,  CONVERT(VARCHAR(10), DOE, 103) AS DOE, Amount, ApprovedAmount, Remarks, ApprovalRemarks, BranchID, Status, VoucherType, Sno AS refno FROM cashpayables WHERE (BranchID = @BranchID) AND (DOE BETWEEN @d1 AND @d2) AND (Status = @Status) ORDER BY DOE");
                cmd.Parameters.Add("@Status", 'P');
            }
            //cmd.Parameters.Add("@BranchID", Session["branch"]);
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(Todate));
            DataTable dtCheque = vdm.SelectQuery(cmd).Tables[0];
            Report = new DataTable();
            Report.Columns.Add("VoucherDate");
            Report.Columns.Add("VoucherID");
            Report.Columns.Add("Voucher Type");
            Report.Columns.Add("Name");
            Report.Columns.Add("Status");
            //Report.Columns.Add("Approval");
            Report.Columns.Add("Amount").DataType = typeof(Double);
            //Report.Columns.Add("Remarks");
            Report.Columns.Add("ApprovedAmount").DataType = typeof(Double);
            Report.Columns.Add("ApprovalRemarks");
            Report.Columns.Add("Head Of Account");
            if (dtCheque.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dtCheque.Rows)
                {
                    DataRow newrow = Report.NewRow();
                    newrow["VoucherDate"] = dr["DOE"].ToString();
                    newrow["VoucherID"] = dr["VocherID"].ToString();
                    newrow["Voucher Type"] = dr["vouchertype"].ToString();
                    newrow["Name"] = dr["onNameof"].ToString();
                    //newrow["Approval"] = dr["EmpName"].ToString();
                    // newrow["Remarks"] = dr["Remarks"].ToString();
                    string ColStatus = dr["Status"].ToString();
                    string ChequeStatus = "";
                    if (ColStatus == "R")
                    {
                        ChequeStatus = "Raised";
                    }
                    if (ColStatus == "A")
                    {
                        ChequeStatus = "Approved";
                    }
                    if (ColStatus == "C")
                    {
                        ChequeStatus = "Rejected";
                    }
                    if (ColStatus == "P")
                    {
                        ChequeStatus = "Paid";
                    }
                    newrow["Status"] = ChequeStatus;
                    double ApprovedAmount = 0;
                    double.TryParse(dr["ApprovedAmount"].ToString(), out ApprovedAmount);
                    double Amount = 0;
                    double.TryParse(dr["Amount"].ToString(), out Amount);
                    newrow["Amount"] = Amount;
                    newrow["ApprovedAmount"] = ApprovedAmount;
                    newrow["ApprovalRemarks"] = dr["ApprovalRemarks"].ToString();
                    cmd = new SqlCommand("SELECT subpayable.RefNo, subpayable.HeadDesc, subpayable.Amount, subpayable.HeadSno, accountheads.HeadName FROM subpayable INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE (subpayable.RefNo = @refno)");
                    cmd.Parameters.Add("@refno", dr["refno"].ToString());
                    DataTable dtheadacc = vdm.SelectQuery(cmd).Tables[0];
                    string head = "";
                    foreach (DataRow drhead in dtheadacc.Rows)
                    {
                        head += drhead["HeadName"].ToString() + "-->" + drhead["Amount"].ToString() + "\r\n";
                    }
                    newrow["Head Of Account"] = head;
                    Report.Rows.Add(newrow);
                }
                DataRow newTotal = Report.NewRow();
                newTotal["Name"] = "Total Amount";
                double val = 0.0;
                foreach (DataColumn dc in Report.Columns)
                {
                    if (dc.DataType == typeof(Double))
                    {
                        val = 0.0;
                        double.TryParse(Report.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out val);
                        newTotal[dc.ToString()] = val;
                    }
                }
                Report.Rows.Add(newTotal);
                grdReports.DataSource = Report;
                grdReports.DataBind();
            }
            else
            {
                lblmsg.Text = "No Vouchers were Found";
                grdReports.DataSource = Report;
                grdReports.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            grdReports.DataSource = Report;
            grdReports.DataBind();
        }
    }
}