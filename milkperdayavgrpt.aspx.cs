using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

public partial class milkperdayavgrpt : System.Web.UI.Page
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
                    dtp_FromDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    dtp_Todate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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

    //private void bindplant()
    //{
    //    cmd = new SqlCommand("SELECT Plant_Master.Plant_Name, Plant_Master.Plant_Code  FROM Plant_Master");
    //    DataTable dttrips = vdm.SelectQuery(cmd).Tables[0];
    //    ddlplant.DataSource = dttrips;
    //    ddlplant.DataTextField = "Plant_Name";
    //    ddlplant.DataValueField = "Plant_Code";
    //    ddlplant.DataBind();
    //}


    void bindbranches()
    {
        vdm = new SalesDBManager();
        if (Session["LevelType"].ToString() == "2" || Session["LevelType"].ToString() == "1")
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE (Plant_Code = @BranchID)");
            cmd.Parameters.Add("@BranchID", Session["branch"].ToString());
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlplant.DataSource = dtPlant;
            ddlplant.DataTextField = "Plant_Name";
            ddlplant.DataValueField = "Plant_Code";
            ddlplant.DataBind();
        }
        else
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master ");
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlplant.DataSource = dtPlant;
            ddlplant.DataTextField = "Plant_Name";
            ddlplant.DataValueField = "Plant_Code";
            ddlplant.DataBind();
        }
    }
    class HeadClass
    {
        public string month { get; set; }
        public string year { get; set; }
    }
    DataTable Report = new DataTable();
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        SalesDBManager SalesDB = new SalesDBManager();
        try
        {
            lblmsg.Text = "";
            string milkopeningbal = string.Empty;
            string milkclosingbal = string.Empty;
            SalesDBManager vdm = new SalesDBManager();
            DateTime fromdate = DateTime.Now;
            DateTime todate = DateTime.Now;
            string idcno = string.Empty;
            string inworddate = string.Empty;
            double totalissueqty = 0;
            string[] datestrig = dtp_FromDate.Text.Split(' ');
            if (datestrig.Length > 1)
            {
                if (datestrig[0].Split('-').Length > 0)
                {
                    string[] dates = datestrig[0].Split('-');
                    string[] times = datestrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            datestrig = dtp_Todate.Text.Split(' ');
            if (datestrig.Length > 1)
            {
                if (datestrig[0].Split('-').Length > 0)
                {
                    string[] dates = datestrig[0].Split('-');
                    string[] times = datestrig[1].Split(':');
                    todate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            lblFromDate.Text = fromdate.ToString("dd/MM/yyyy");
            lbltodate.Text = todate.ToString("dd/MM/yyyy");
            int Plant_Code = Convert.ToInt32(ddlplant.SelectedItem.Value);
            hidepanel.Visible = true;
            DataTable Report = new DataTable();
            DataTable snfReport = new DataTable();
            Report.Columns.Add("Plant Name");
            Report.Columns.Add("Month");
            Report.Columns.Add("Year");
            Report.Columns.Add("Milkqty").DataType = typeof(double);
            Report.Columns.Add("PerdayAvg_MilkQty").DataType = typeof(double);

            DateTime startDate = fromdate;
            DateTime endDate = todate;
            int months = endDate.Subtract(startDate).Days / 30;

            string presentmonth = fromdate.ToString("MM");
            string year = fromdate.ToString("yyyy");
            int pmonth = Convert.ToInt32(presentmonth);
            int pyear = Convert.ToInt32(year);
            List<HeadClass> HeadClasslist = new List<HeadClass>();
            for (int i = 0; i <= months; i++)
            {
                if (i == 0)
                {
                    pmonth = pmonth + i;
                    pyear = pyear + i;
                }
                else
                {
                    pmonth = pmonth + 1;
                    if (pmonth > 12)
                    {
                        pyear = pyear + 1;
                        pmonth = 1;
                    }
                }
                HeadClass getVehicles = new HeadClass();
                getVehicles.month = pmonth.ToString();
                getVehicles.year = pyear.ToString();
                HeadClasslist.Add(getVehicles);
            }

            foreach (var value in HeadClasslist)
            {
                string mnth = value.month;
                string yr = value.year;
                DateTime frmdt = new DateTime(Convert.ToInt32(yr), Convert.ToInt32(mnth), 1);
                string myfrmdate = frmdt.ToString();
                DateTime mf = Convert.ToDateTime(myfrmdate);
                DateTime Date = frmdt.AddMonths(1).AddDays(-1);
                string mytodate = Date.ToString();
                DateTime mt = Convert.ToDateTime(mytodate);
                cmd = new SqlCommand("SELECT SUM(Milk_kg) AS milkqty,  Plant_Master.Plant_Name  FROM  Procurement INNER JOIN Plant_Master ON Procurement.Plant_Code = Plant_Master.Plant_Code  WHERE (Procurement.Plant_Code = @pcode) AND (Procurement.Prdate BETWEEN @d1 AND @d2) GROUP BY Plant_Master.Plant_Name");
                cmd.Parameters.Add("@d1", GetLowDate(mf));
                cmd.Parameters.Add("@d2", GetHighDate(mt));
                cmd.Parameters.Add("@pcode", Plant_Code);
                DataTable dtqty = vdm.SelectQuery(cmd).Tables[0];
                if (dtqty.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtqty.Rows)
                    {
                        DataRow newdr = Report.NewRow();
                        newdr["Plant Name"] = dr["Plant_Name"].ToString();
                        newdr["Month"] = mf.ToString("MMM");
                        newdr["Year"] = mf.ToString("yyyy");
                        string milkqty = dr["milkqty"].ToString();
                        if (milkqty != "")
                        {
                            double milkq = Convert.ToDouble(milkqty);
                            double avgperday = milkq / 30;
                            newdr["Milkqty"] = Math.Round(milkq, 0);
                            newdr["PerdayAvg_MilkQty"] = Math.Round(avgperday, 0);
                        }
                        Report.Rows.Add(newdr);
                    }
                }
            }
            if (Report.Rows.Count > 0)
            {
                DataRow newTotal = Report.NewRow();
                newTotal["Year"] = "Total";
                double val = 0.0;
                foreach (DataColumn dc in Report.Columns)
                {
                    if (dc.DataType == typeof(Double))
                    {
                        val = 0.0;
                        double.TryParse(Report.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out val);
                        if (val == 0.0)
                        {
                        }
                        else
                        {
                            newTotal[dc.ToString()] = val;
                        }
                    }
                }
                Report.Rows.Add(newTotal);
            }
            Session["xportdata"] = Report;
            Session["filename"] = "Per day Avg milk Details - " + ddlplant.SelectedItem.Text + "";
            grdReports.DataSource = Report;
            grdReports.DataBind();
            hidepanel.Visible = true;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }

    protected void grdReports_DataBinding(object sender, EventArgs e)
    {
        try
        {
            GridViewGroup First = new GridViewGroup(grdReports, null, "Plant Name");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}