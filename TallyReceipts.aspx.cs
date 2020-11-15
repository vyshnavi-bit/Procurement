using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TallyReceipts : System.Web.UI.Page
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
        //UserName = Session["field1"].ToString();
        //vdm = new SalesDBManager();
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
            DataTable Report = new DataTable();
            Report.Columns.Add("DOE");
            Report.Columns.Add("Ref Receipt");
            Report.Columns.Add("Receipt");
            Report.Columns.Add("Type");
            Report.Columns.Add("Name");
            Report.Columns.Add("Remarks");

            Report.Columns.Add("Amount").DataType = typeof(Double);
            lbl_selfromdate.Text = fromdate.ToString("dd/MM/yyyy");
            lblRoutName.Text = ddlSalesOffice.SelectedItem.Text;
            Session["xporttype"] = "TallyReceipts";
            string ledger = "";
            cmd = new SqlCommand("SELECT ladger_dr FROM Plant_Master WHERE (Plant_Code = @Plant_Code)");
            cmd.Parameters.Add("@Plant_Code", ddlSalesOffice.SelectedValue);
            DataTable dtledger = vdm.SelectQuery(cmd).Tables[0];
            if (dtledger.Rows.Count > 0)
            {
                ledger = dtledger.Rows[0]["ladger_dr"].ToString();
            }
            Session["filename"] = ddlSalesOffice.SelectedItem.Text + " Tally Receipts" + fromdate.ToString("dd/MM/yyyy");
            DataTable dtAgent = vdm.SelectQuery(cmd).Tables[0];
            cmd = new SqlCommand("SELECT Branchid, Amount, Remarks, CONVERT(VARCHAR(10), DOE, 103) AS DOE, Receiptno, Name,PaymentType as Type FROM cashcollections WHERE (Branchid = @BranchID) AND (DOE BETWEEN @d1 AND @d2)  AND (CollectionType = 'Cash') ORDER BY DOE");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            cmd.Parameters.Add("@Type", "Others");
            DataTable dtOthers = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtOthers.Rows)
            {
                DataRow newrow = Report.NewRow();
                newrow["DOE"] = dr["DOE"].ToString();
                newrow["Receipt"] = dr["Receiptno"].ToString();
                newrow["Type"] = dr["Type"].ToString();
                newrow["Name"] = dr["Name"].ToString();
                double AmountPaid = 0;
                double.TryParse(dr["Amount"].ToString(), out AmountPaid);
                newrow["Amount"] = AmountPaid;
                newrow["Remarks"] = dr["Remarks"].ToString();
                Report.Rows.Add(newrow);
            }
            string Receiptno = "";
            Receiptno = dtapril.ToString("yy") + dtmarch.ToString("yy");
            if (Report.Rows.Count > 0)
            {
                DataView view = new DataView(Report);
                dtReport = new DataTable();
                dtReport.Columns.Add("Voucher Date");
                dtReport.Columns.Add("Voucher No");
                dtReport.Columns.Add("Voucher Type");
                dtReport.Columns.Add("Ledger (Dr)");
                dtReport.Columns.Add("Ledger (Cr)");
                dtReport.Columns.Add("Amount");
                dtReport.Columns.Add("Narration");
                //DataTable distincttable = view.ToTable(true, "BranchName", "BSno");
                int i = 1;
                foreach (DataRow branch in Report.Rows)
                {
                    DataRow newrow = dtReport.NewRow();
                    newrow["Voucher Date"] = fromdate.ToString("dd-MMM-yyyy");
                    string newreceipt = "0";
                    int countdc = 0;
                    int.TryParse(branch["Receipt"].ToString(), out countdc);
                    if (countdc < 10)
                    {
                        newreceipt = "000" + countdc;
                    }
                    if (countdc >= 10 && countdc <= 99)
                    {
                        newreceipt = "00" + countdc;
                    }
                    if (countdc >= 99 && countdc <= 999)
                    {
                        newreceipt = "0" + countdc;
                    }
                    if (countdc > 999)
                    {
                        newreceipt = "" + countdc;
                    }
                    newrow["Voucher No"] = Receiptno + newreceipt;
                    newrow["Voucher Type"] = "Cash Receipt Import";
                    newrow["Ledger (Dr)"] = ledger;
                    if (branch["Name"].ToString() == "")
                    {
                    }
                    else
                    {
                        newrow["Ledger (Cr)"] = branch["Name"].ToString();
                        newrow["Amount"] = branch["Amount"].ToString();
                        double invval = 0;
                        //newrow["Narration"] = branch["Remarks"].ToString();
                        newrow["Narration"] = "" + branch["Remarks"].ToString() + " vide Receipt No " + branch["Receipt"].ToString() + ",Receipt Date " + fromdate.ToString("dd/MM/yyyy") + ",Emp Name " + Session["EmpName"].ToString();
                        dtReport.Rows.Add(newrow);
                        i++;
                    }
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