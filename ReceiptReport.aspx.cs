using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ReceiptReport : System.Web.UI.Page
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
    protected void grdReports_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = grdReports.Rows[rowIndex];
        string ReceiptNo = row.Cells[2].Text;
        string Type = row.Cells[4].Text;
        Session["ReceiptNo"] = ReceiptNo;
        Session["Type"] = Type;
        Response.Redirect("ReceiptBook.aspx", false);
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
            Session["filename"] = "Receipt Report";
            DataTable Report = new DataTable();
            Report.Columns.Add("DOE");
            Report.Columns.Add("Ref Receipt");
            Report.Columns.Add("Receipt");
            Report.Columns.Add("Type");
            Report.Columns.Add("Name");
            Report.Columns.Add("Amount").DataType = typeof(Double);
            cmd = new SqlCommand("SELECT Sno,CONVERT(VARCHAR(10), cashcollections.DOE, 103) AS DOE,Receiptno,PaymentType as Type, Name, Amount  FROM cashcollections WHERE (Branchid = @BranchID) AND (DOE BETWEEN @d1 AND @d2) ORDER BY DOE");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(Todate));
            DataTable dtOthers = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtOthers.Rows)
            {
                DataRow newrow = Report.NewRow();
                newrow["DOE"] = dr["DOE"].ToString();
                newrow["Ref Receipt"] = dr["Sno"].ToString();
                newrow["Receipt"] = dr["Receiptno"].ToString();
                newrow["Type"] = dr["Type"].ToString();
                newrow["Name"] = dr["Name"].ToString();
                double AmountPaid = 0;
                double.TryParse(dr["Amount"].ToString(), out AmountPaid);
                newrow["Amount"] = AmountPaid;
                Report.Rows.Add(newrow);
            }
            DataView dv = Report.DefaultView;
            dv.Sort = "Receipt ASC";
            DataTable sortedDT = dv.ToTable();
            DataRow newvartical = sortedDT.NewRow();
            newvartical["Name"] = "Total";
            double val = 0.0;
            foreach (DataColumn dc in sortedDT.Columns)
            {
                if (dc.DataType == typeof(Double))
                {
                    val = 0.0;
                    double.TryParse(sortedDT.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out val);
                    newvartical[dc.ToString()] = val;
                }
            }
            sortedDT.Rows.Add(newvartical);
            grdReports.DataSource = sortedDT;
            grdReports.DataBind();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
}