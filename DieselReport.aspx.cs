using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class DieselReport : System.Web.UI.Page
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
                    //lblAddress.Text = Session["Address"].ToString();
                    //lblTitle.Text = Session["TitleName"].ToString();
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
    DataTable Report = new DataTable();
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        SalesDBManager SalesDB = new SalesDBManager();
        //DateTime fromdate = DateTime.Now;
        //DateTime todate = DateTime.Now;
        //string[] datestrig = dtp_FromDate.Text.Split(' ');
        //if (datestrig.Length > 1)
        //{
        //    if (datestrig[0].Split('-').Length > 0)
        //    {
        //        string[] dates = datestrig[0].Split('-');
        //        string[] times = datestrig[1].Split(':');
        //        fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
        //    }
        //}
        //datestrig = dtp_Todate.Text.Split(' ');
        //if (datestrig.Length > 1)
        //{
        //    if (datestrig[0].Split('-').Length > 0)
        //    {
        //        string[] dates = datestrig[0].Split('-');
        //        string[] times = datestrig[1].Split(':');
        //        todate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
        //    }
        //}
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
            Report.Columns.Add("Date");
            //Report.Columns.Add("OpeningReading");
            //Report.Columns.Add("EndingReading");
            //Report.Columns.Add("TotalReading");
            Report.Columns.Add("DiselOPLtrs");
            Report.Columns.Add("Receipt");
            Report.Columns.Add("Hours");
            Report.Columns.Add("Consumption");
            Report.Columns.Add("DiselCLLiters");
            //Report.Columns.Add("PowerOPUnits");
            //Report.Columns.Add("PowerCLUnits");
            //Report.Columns.Add("ConsumptionUnits");
            //Report.Columns.Add("Duming Start Time");
            //Report.Columns.Add("Duming End Time");
            //Report.Columns.Add("MBRT");
            int Plant_Code = Convert.ToInt32(ddlplant.SelectedItem.Value);
            hidepanel.Visible = true;
            //cmd = new SqlCommand("SELECT Tid, GenOpReading, GenClReading, GenTotReading, Date, Plant_Code FROM GensetPowerDiesel WHERE (Plant_Code = @plnat_code) AND (Date BETWEEN @fromdate AND @todate)");
            //cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
            //cmd.Parameters.Add("@todate", GetHighDate(todate));
            //cmd.Parameters.Add("@plnat_code", Plant_Code);
            //DataTable dtsuperwise1 = vdm.SelectQuery(cmd).Tables[0];
            cmd = new SqlCommand("SELECT  Tid, DispOpLtrs, DispReceipt, DispHours, DispConsumption, DispCloLtrs, doe, Plant_Code, date FROM  Diesel WHERE (Plant_Code = @plnat_code) AND (date BETWEEN @fromdate AND @todate)");
            cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
            cmd.Parameters.Add("@todate", GetHighDate(todate));
            cmd.Parameters.Add("@plnat_code", Plant_Code);
             DataTable dtdiesel = vdm.SelectQuery(cmd).Tables[0];
            //cmd = new SqlCommand("SELECT  Tid, PowOpUnit, PowCloUnit, PowConsumpUnit, doe, Plant_Code, date FROM powerdetails1 WHERE (Plant_Code = @plnat_code) AND (date BETWEEN @fromdate AND @todate)");
            //cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
            //cmd.Parameters.Add("@todate", GetHighDate(todate));
            //cmd.Parameters.Add("@plnat_code", Plant_Code);
            DataTable dtpower = vdm.SelectQuery(cmd).Tables[0];
            if (dtpower.Rows.Count > 0)
            {
                double totalhours = 0;
                double totalgenaraterreding = 0;
                double totalconsumption = 0;
                double totalconsumptionunits = 0;
                foreach (DataRow drsuper in dtdiesel.Rows)
                {
                    DataRow newrow = Report.NewRow();
                    newrow["Date"] = drsuper["date"].ToString();
                    //newrow["OpeningReading"] = drsuper["GenOpReading"].ToString();
                    //newrow["EndingReading"] = drsuper["GenClReading"].ToString();
                    //newrow["TotalReading"] = drsuper["GenTotReading"].ToString();
                    //double totalreading = 0;
                    //double.TryParse(drsuper["GenTotReading"].ToString(), out totalreading);
                    //totalgenaraterreding += totalreading;
                    newrow["DiselOPLtrs"] = drsuper["DispOpLtrs"].ToString();
                    newrow["Receipt"] = drsuper["DispReceipt"].ToString();
                    newrow["Hours"] = drsuper["DispHours"].ToString();
                    double hours = 0;
                    double.TryParse(drsuper["DispHours"].ToString(), out hours);
                    totalhours += hours;
                    newrow["Consumption"] = drsuper["DispConsumption"].ToString();
                    double consumption = 0;
                    double.TryParse(drsuper["DispConsumption"].ToString(), out consumption);
                    totalconsumption += consumption;
                    double opening = 0;
                    double.TryParse(drsuper["DispOpLtrs"].ToString(), out opening);
                    double closing=0;
                    closing = opening - consumption;
                    newrow["DiselCLLiters"] = closing;
                    //newrow["PowerOPUnits"] = drsuper["PowOpUnit"].ToString();
                    //newrow["PowerCLUnits"] = drsuper["PowCloUnit"].ToString();
                    //newrow["ConsumptionUnits"] = drsuper["PowConsumpUnit"].ToString();
                    //double consumptionunits = 0;
                    //double.TryParse(drsuper["PowConsumpUnit"].ToString(), out consumptionunits);
                    //totalconsumptionunits += consumptionunits;
                    Report.Rows.Add(newrow);
                }
                DataRow newrow1 = Report.NewRow();
                newrow1["Date"] = "Total";
                //newrow1["TotalReading"] = Math.Round(totalgenaraterreding, 2);
                newrow1["Hours"] = Math.Round(totalhours, 2);
                newrow1["Consumption"] = Math.Round(totalconsumption, 2);
                //newrow1["ConsumptionUnits"] = Math.Round(totalconsumptionunits, 2);
                Report.Rows.Add(newrow1);
                grdReports.DataSource = Report;
                grdReports.DataBind();
                hidepanel.Visible = true;
            }
            else
            {
                lblmsg.Text = "No data found";
                hidepanel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void grdReports_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2.Text = "POWER CONSUMPTION";
            HeaderCell2.ColumnSpan = 5;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            grdReports.Controls[0].Controls.AddAt(0, HeaderRow);

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell3 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell3.Text = "DAILY POWER CONSUMPTION Details";
            HeaderCell3.ColumnSpan = 13;
            HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell3);
            grdReports.Controls[0].Controls.AddAt(0, HeaderRow);

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell4 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell4.Text = "SRI VYSHNAVI DAIRY SPECIALITIES P.LTD";
            HeaderCell4.ColumnSpan = 13;
            HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell4);
            grdReports.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void grdReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void grdReports_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "DISEL DETAILS";
            HeaderCell2.ColumnSpan = 5;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell3 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell3.Text = "DAILY DIESEL Details";
            HeaderCell3.ColumnSpan = 13;
            HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell3);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell4 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell4.Text = "SRI VYSHNAVI DAIRY SPECIALITIES P.LTD";
            HeaderCell4.ColumnSpan = 13;
            HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell4);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void grdReports_RowCreated2(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "POWER CONSUMPTION";
            HeaderCell2.ColumnSpan = 3;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell3.Text = "DAILY POWER CONSUMPTION Details";
            HeaderCell3.ColumnSpan = 13;
            HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell3);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell4 = new TableCell();
            // HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell4.Text = "SRI VYSHNAVI DAIRY SPECIALITIES P.LTD";
            HeaderCell4.ColumnSpan = 13;
            HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell4);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
}