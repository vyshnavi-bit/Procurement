using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class LedgerWiseTransactions : System.Web.UI.Page
{
    SqlCommand cmd;
    string UserName = "";
    SalesDBManager vdm;
    string BranchID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["branch"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            BranchID = Session["branch"].ToString();
        }
        //vdm = new VehicleDBMgr();
        if (!this.IsPostBack)
        {
            if (!Page.IsCallback)
            {
                FillSalesOffice();
                lblTitle.Text = Session["TitleName"].ToString();
                txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                txtTodate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            }
        }
    }
    void FillSalesOffice()
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
    protected void ddlSalesOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        vdm = new SalesDBManager();
        cmd = new SqlCommand("SELECT accountheads.HeadName, accountheads.Sno FROM subpayable INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno INNER JOIN cashpayables ON subpayable.RefNo = cashpayables.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.VoucherType = 'Debit') GROUP BY accountheads.Sno,accountheads.HeadName ORDER BY accountheads.HeadName,accountheads.Sno");
        cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
        DataTable dtHead = vdm.SelectQuery(cmd).Tables[0];
        ddlHeadOfaccounts.DataSource = dtHead;
        ddlHeadOfaccounts.DataTextField = "HeadName";
        ddlHeadOfaccounts.DataValueField = "Sno";
        ddlHeadOfaccounts.DataBind();
        ddlHeadOfaccounts.Items.Insert(0, new ListItem("Select", "0"));
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
    void GetReport()
    {
        try
        {
            vdm = new SalesDBManager();
            DateTime fromdate = DateTime.Now;
            DataTable Report = new DataTable();
            lblmsg.Text = "";
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
            DateTime todate = DateTime.Now;
            string[] todatestrig = txtTodate.Text.Split(' ');
            if (todatestrig.Length > 1)
            {
                if (todatestrig[0].Split('-').Length > 0)
                {
                    string[] dates = todatestrig[0].Split('-');
                    string[] times = todatestrig[1].Split(':');
                    todate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            Session["filename"] = "Statement of Account";
            lblLedger.Text = ddlHeadOfaccounts.SelectedItem.Text;
            lblbranchname.Text = ddlSalesOffice.SelectedItem.Text;
            lbl_fromDate.Text = txtFromdate.Text;
            lbl_selttodate.Text = txtTodate.Text;
            pnlHide.Visible = true;
            cmd = new SqlCommand("SELECT cashpayables.DOE AS EntryDate, cashpayables.VocherID,  cashpayables.Amount FROM accountheads INNER JOIN subpayable ON accountheads.Sno = subpayable.HeadSno INNER JOIN cashpayables ON subpayable.RefNo = cashpayables.Sno WHERE (cashpayables.BranchID = @BranchID)  AND (subpayable.HeadSno = @HeadSno) and (cashpayables.Status=@Status) AND (cashpayables.DOE between @d1 and @d2)   ORDER BY cashpayables.DOE");
            cmd.Parameters.Add("@Status", 'P');
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(todate));
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@HeadSno", ddlHeadOfaccounts.SelectedValue);
            DataTable dtCredit = vdm.SelectQuery(cmd).Tables[0];
            Report.Columns.Add("Sno");
            Report.Columns.Add("Date");
            Report.Columns.Add("VoucherID");
            Report.Columns.Add("Amount").DataType = typeof(Double);
            if (dtCredit.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dtCredit.Rows)
                {
                    DataRow newrow = Report.NewRow();
                    newrow["Sno"] = i++.ToString();
                    newrow["Date"] = dr["EntryDate"].ToString();
                    newrow["VoucherID"] = dr["VocherID"].ToString();
                    double amount = 0;
                    double.TryParse(dr["Amount"].ToString(),out amount);
                    newrow["Amount"] = amount;
                    Report.Rows.Add(newrow);
                }
                DataRow newvartical = Report.NewRow();
                newvartical["VoucherID"] = "Total";
                double val = 0.0;
                foreach (DataColumn dc in Report.Columns)
                {
                    if (dc.DataType == typeof(Double))
                    {
                        val = 0.0;
                        double.TryParse(Report.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out val);
                        newvartical[dc.ToString()] = val;
                    }
                }
                Report.Rows.Add(newvartical);
                grdReports.DataSource = Report;
                grdReports.DataBind();
            }
            else
            {
                lblmsg.Text = "No Indent Found";
                //grdReports.DataSource = Report;
                //grdReports.DataBind();
                //pnlHide.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
}