using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class RouteTimeMaintenanceReport : System.Web.UI.Page
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
        }
        else
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master ");
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlplantname.DataSource = dtPlant;
            ddlplantname.DataTextField = "Plant_Name";
            ddlplantname.DataValueField = "Plant_Code";
            ddlplantname.DataBind();
        }
    }
    //private void bindbranches()
    //{
    //    cmd = new SqlCommand("SELECT Plant_Code, Plant_Name, Plant_Address, Plant_PhoneNo, Company_Code, Manager_Name, Mana_PhoneNo, Pmail, Milktype, Active, ladger_dr FROM Plant_Master");
    //    DataTable dttrips = vdm.SelectQuery(cmd).Tables[0];
    //    ddlplantname.DataSource = dttrips;
    //    ddlplantname.DataTextField = "Plant_Name";
    //    ddlplantname.DataValueField = "Plant_Code";
    //    ddlplantname.DataBind();
    //    ddlplantname.ClearSelection();
    //    ddlplantname.Items.Insert(0, new ListItem { Value = "0", Text = "--Select Plant--", Selected = true });
    //    ddlplantname.SelectedValue = "0";
    //}

    double totalmilk;
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        report();

    }
    DataTable collection = new DataTable();
    DataTable Report = new DataTable();
    private void report()
    {
        try
        {
            lblmsg.Text = "";
            string milkopeningbal = string.Empty;
            string milkclosingbal = string.Empty;
            SalesDBManager vdm = new SalesDBManager();
            DateTime fromdate = DateTime.Now;

            string idcno = string.Empty;
            string inworddate = string.Empty;
            double totalinwardqty = 0;
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

            lblFromDate.Text = fromdate.ToString("dd/MMM/yyyy");

            int plantcode = Convert.ToInt32(ddlplantname.SelectedItem.Value);
            int plancode1 = plantcode;
            Report.Columns.Add("Sno");
            //Report.Rows.Add("Date");
            //Report.Columns.Add("Diff");
            //Report.Columns.Add("Session");
            Report.Columns.Add("RouteName");
            Report.Columns.Add("SupervisorName");
            Report.Columns.Add("AM LTRS");
            Report.Columns.Add("Diff");
            Report.Columns.Add("Fat");
            Report.Columns.Add("Snf");
            Report.Columns.Add("Dairy Time");
            Report.Columns.Add("IN TIME");
            Report.Columns.Add("Duming Start Time");
            Report.Columns.Add("Duming End Time");
            Report.Columns.Add("MBRT");
            hidepanel.Visible = true;
            if (plantcode != 0)
            {
                cmd = new SqlCommand("SELECT alltable.SupervisorName, alltable.routeid, alltable.RouteName, pro.PlantCode FROM  (SELECT DISTINCT RouteId AS R, PlantCode  FROM  Lp  WHERE (PlantCode = @plantcode) AND (Prdate BETWEEN @fromdate AND @fromdate)) AS pro LEFT OUTER JOIN  (SELECT  aa.Supervisor_Code, bb.SupervisorName, aa.routeid, CONVERT(nvarchar(50), aa.routeid) + '_' + aa.Route_Name AS RouteName  FROM (SELECT A.Supervisor_Code, B.Route_Name, B.routeid  FROM (SELECT  TId, Company_Code, Supervisor_Code, Route_Id   FROM Supervisor_RouteAllotment  WHERE (Plant_Code = @plantcode)) AS A LEFT OUTER JOIN   (SELECT Route_Name, Route_ID AS routeid  FROM  Route_Master  WHERE (Plant_Code = @plantcode)) AS B ON A.Route_Id = B.routeid) AS aa LEFT OUTER JOIN  (SELECT  Supervisor_Code, SupervisorName, Company_Code, Plant_Code, Added_Date, Dob, Address, Mobile, Bank_name, IfscCode, AccountNumber, Pannumber, Qualification, Description   FROM Supervisor_Details WHERE (Plant_Code = @plantcode)) AS bb ON aa.Supervisor_Code = bb.Supervisor_Code) AS alltable ON pro.R = alltable.routeid WHERE  (alltable.routeid > 0) ORDER BY alltable.SupervisorName");
                cmd.Parameters.Add("@plantcode", plantcode);
                cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
                //cmd.Parameters.Add("@session", "AM");

                DataTable dtsuperwise = vdm.SelectQuery(cmd).Tables[0];
                if (dtsuperwise.Rows.Count > 0)
                {
                    double avgmilkliters1 = 0;
                    double totmilklitrs1 = 0;
                    double totlmilklitrs1 = 0;
                    double totmilklitrs = 0;
                    double totlmilklitrs = 0;
                    double Totalfat = 0;
                    double TotalSnf = 0;
                    double Totalfat1 = 0;
                    double TotalSnf1 = 0;
                    double avgsnf = 0;
                    double avgfat = 0;
                    double avgfat1 = 0;
                    double avgsnf1 = 0;
                    double gtotlmilklitrs = 0;
                    double avgmilkliters = 0;
                    double totaldiffmilkliters = 0;
                    double gtotalamount = 0;
                    double totalamount = 0;
                    double totlmilkkgs = 0;
                    double totaldiffmilkliters1 = 0;
                    double diffmilkliters1 = 0;
                    double gtotaldiffernce = 0;
                    int totalsum = 0;
                    int totalsum1 = 0;
                    cmd = new SqlCommand("SELECT  RouteTimeMaintain.Route_id, RouteTimeMaintain.VehicleSettime, RouteTimeMaintain.VehicleInttime, RouteTimeMaintain.Date, RouteTimeMaintain.Session, RouteTimeMaintain.MBRT,  RouteTimeMaintain.Plant_code FROM    Route_Master INNER JOIN   RouteTimeMaintain ON Route_Master.Route_ID = RouteTimeMaintain.Route_id WHERE (RouteTimeMaintain.Plant_code = @plantcode) AND (RouteTimeMaintain.Date = @fromdate) AND (RouteTimeMaintain.Session=@session)  GROUP BY RouteTimeMaintain.Date, RouteTimeMaintain.VehicleSettime, RouteTimeMaintain.VehicleInttime, RouteTimeMaintain.Date, RouteTimeMaintain.Session,  RouteTimeMaintain.MBRT, RouteTimeMaintain.Plant_code,RouteTimeMaintain.Route_id");
                    cmd.Parameters.Add("@plantcode", plantcode);
                    cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
                    cmd.Parameters.Add("@session", "AM");
                    DataTable dtroute = vdm.SelectQuery(cmd).Tables[0];
                    cmd = new SqlCommand("SELECT PlantCode, Prdate, Fat, Snf, Sessions, Milkkg, COUNT(Fat) AS value, COUNT(Snf) AS value1, StartingTime, EndingTime, RouteId FROM  Lp WHERE (PlantCode = @plantcode) AND (Prdate = @fromdate) AND (RouteId IS NOT NULL) AND (Sessions =@session)  GROUP BY PlantCode, Prdate, Sessions, Fat, Snf, StartingTime, Milkkg, EndingTime, RouteId");
                    cmd.Parameters.Add("@plantcode", plantcode);
                    cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
                    cmd.Parameters.Add("@session", "AM");
                    DataTable dtlivedata = vdm.SelectQuery(cmd).Tables[0];
                    cmd = new SqlCommand("SELECT PlantCode, Prdate, Fat, Snf, Sessions, Milkkg, COUNT(Fat) AS value, COUNT(Snf) AS value1, StartingTime, EndingTime, RouteId FROM  Lp WHERE (PlantCode = @plantcode) AND (Prdate = @fromdate) AND (RouteId IS NOT NULL) AND (Sessions =@session)  GROUP BY PlantCode, Prdate, Sessions, Fat, Snf, StartingTime, Milkkg, EndingTime, RouteId");
                    cmd.Parameters.Add("@plantcode", plantcode);
                    cmd.Parameters.Add("@fromdate", GetLowDate(fromdate).AddDays(-1));
                    cmd.Parameters.Add("@session", "AM");
                    DataTable dtlivedata1 = vdm.SelectQuery(cmd).Tables[0];
                    DateTime dt = DateTime.Now;
                    string prevdate = string.Empty;
                    string session;
                    int prevRouteId = 0;
                    var i = 1;
                    int count = 1;
                    int rowcount = 1;
                    foreach (DataRow drsuper in dtsuperwise.Rows)
                    {
                        DataRow newrow = Report.NewRow();
                        //newrow["PlantCode"] = drsuper["Plant_Code"].ToString();
                        newrow["RouteName"] = drsuper["RouteName"].ToString();
                        newrow["SupervisorName"] = drsuper["SupervisorName"].ToString();
                        newrow["Sno"] = i++.ToString();
                        int presntRouteId = 0;
                        int.TryParse(drsuper["PlantCode"].ToString(), out presntRouteId);
                        int routeid = 0;
                        int.TryParse(drsuper["PlantCode"].ToString(), out routeid);
                        if (presntRouteId == prevRouteId)
                        {
                            foreach (DataRow drmaintain in dtroute.Select("Plant_code='" + drsuper["PlantCode"].ToString() + "' AND Route_id='" + drsuper["routeid"].ToString() + "'"))
                            {
                                newrow["Dairy Time"] = drmaintain["VehicleSettime"].ToString();
                                newrow["IN TIME"] = drmaintain["VehicleInttime"].ToString();
                                newrow["MBRT"] = drmaintain["MBRT"].ToString();
                                //newrow["Date"] = drmaintain["Date"].ToString();
                                // session = drmaintain["Session"].ToString();

                            }
                            foreach (DataRow drlive in dtlivedata.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate) + "'"))
                            {
                                double milkkgs = 0;
                                double.TryParse(drlive["Milkkg"].ToString(), out milkkgs);
                                double milkltrs = milkkgs / 1.03;
                                totmilklitrs += milkltrs;
                                totlmilklitrs += milkltrs;
                                double fat = 0;
                                double.TryParse(drlive["Fat"].ToString(), out fat);
                                Totalfat1 += fat;
                                //Totalfat += fat;
                                double snf = 0;
                                double.TryParse(drlive["Snf"].ToString(), out snf);
                                TotalSnf1 += snf;
                                //TotalSnf += snf;
                                int sum = 0; int sum1 = 0;
                                int.TryParse(drlive["value"].ToString(), out sum);
                                totalsum += sum;
                                int.TryParse(drlive["value1"].ToString(), out sum1);
                                totalsum1 += sum;

                                //newrow["FAT"] = drlive["Fat"].ToString();
                                newrow["Duming Start Time"] = drlive["StartingTime"].ToString();
                                newrow["Duming End Time"] = drlive["EndingTime"].ToString();
                                //newrow["SNF"] = drlive["Snf"].ToString();
                                newrow["AM LTRS"] = Math.Round(totmilklitrs, 2);
                            }

                            foreach (DataRow drlive1 in dtlivedata1.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate).AddDays(-1) + "'"))
                            {
                                double milkkgs1 = 0;
                                double.TryParse(drlive1["Milkkg"].ToString(), out milkkgs1);
                                double milkltrs1 = milkkgs1 / 1.03;
                                totmilklitrs1 += milkltrs1;
                                totlmilklitrs1 += milkltrs1;
                                double diffmilkliters = 0;
                                diffmilkliters = totlmilklitrs - totmilklitrs1;
                                diffmilkliters1 = totlmilklitrs - totmilklitrs1;
                                newrow["Diff"] = Math.Round(diffmilkliters, 2);
                            }
                            avgfat1 = Totalfat1 / totalsum;
                            avgsnf1 = TotalSnf1 / totalsum1;
                            // avgmilkliters = totmilklitrs / totalsum1;
                            avgmilkliters1 = totlmilklitrs1 / totalsum1;
                            //double diffmilkliters1 = avgmilkliters1 - avgmilkliters;
                            //newrow["Diff"] = Math.Round(diffmilkliters1, 2);
                            TotalSnf += avgsnf1;
                            Totalfat += avgfat1;
                            //totaldiffmilkliters += diffmilkliters1;
                            newrow["FAT"] = Math.Round(avgfat1, 2);
                            newrow["SNF"] = Math.Round(avgsnf1, 2);
                            totmilklitrs = 0;
                            diffmilkliters1 = 0;
                            TotalSnf1 = 0;
                            totalsum = 0;
                            totalsum1 = 0;
                            Totalfat1 = 0;
                            avgfat1 = 0;
                            avgsnf1 = 0;
                            totaldiffmilkliters1 = 0;
                            Report.Rows.Add(newrow);
                            rowcount++;
                            DataTable dtin = new DataTable();
                            DataRow[] drr = dtsuperwise.Select("PlantCode='" + presntRouteId + "'");
                            if (drr.Length > 0)
                            {
                                dtin = drr.CopyToDataTable();
                            }
                            int dttotalpocount = dtin.Rows.Count;
                            if (dttotalpocount == rowcount)
                            {
                                DataRow newrow1 = Report.NewRow();
                                newrow1["SupervisorName"] = "Total";
                                avgfat = Totalfat / rowcount;
                                avgsnf = TotalSnf / rowcount;
                                avgmilkliters = totaldiffmilkliters / rowcount;
                                newrow1["FAT"] = Math.Round(avgfat, 2);
                                newrow1["SNF"] = Math.Round(avgsnf, 2);
                                double totaldiffernce = totlmilklitrs - diffmilkliters1;
                                newrow1["AM LTRS"] = Math.Round(totlmilklitrs, 2);
                                //newrow1["Diff"] = Math.Round(totaldiffernce, 2);
                                Report.Rows.Add(newrow1);
                                gtotaldiffernce += totaldiffernce;
                                gtotlmilklitrs += totlmilklitrs;
                                totlmilklitrs = 0;
                                totaldiffernce = 0;
                                rowcount = 1;
                            }
                        }
                        else
                        {
                            prevRouteId = presntRouteId;
                            foreach (DataRow drmaintain in dtroute.Select("Plant_code='" + drsuper["PlantCode"].ToString() + "' AND Route_id='" + drsuper["routeid"].ToString() + "'"))
                            {
                                newrow["Dairy Time"] = drmaintain["VehicleSettime"].ToString();
                                newrow["IN TIME"] = drmaintain["VehicleInttime"].ToString();
                                newrow["MBRT"] = drmaintain["MBRT"].ToString();
                                //  newrow["Date"] = drmaintain["Date"].ToString();
                                // session = drmaintain["Session"].ToString();

                            }
                            foreach (DataRow drlive in dtlivedata.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate) + "'"))
                            {
                                double milkkgs = 0;
                                double.TryParse(drlive["Milkkg"].ToString(), out milkkgs);
                                double milkltrs = milkkgs / 1.03;
                                totmilklitrs += milkltrs;
                                totlmilklitrs += milkltrs;
                                double fat = 0;
                                double.TryParse(drlive["Fat"].ToString(), out fat);
                                Totalfat1 += fat;
                                //Totalfat += fat;
                                double snf = 0;
                                double.TryParse(drlive["Snf"].ToString(), out snf);
                                TotalSnf1 += snf;
                                //TotalSnf += snf;
                                int sum = 0; int sum1 = 0;
                                int.TryParse(drlive["value"].ToString(), out sum);
                                totalsum += sum;
                                int.TryParse(drlive["value1"].ToString(), out sum1);
                                totalsum1 += sum;
                                //newrow["FAT"] = drlive["Fat"].ToString();
                                newrow["Duming Start Time"] = drlive["StartingTime"].ToString();
                                newrow["Duming End Time"] = drlive["EndingTime"].ToString();
                                //newrow["SNF"] = drlive["Snf"].ToString();
                                newrow["AM LTRS"] = Math.Round(totmilklitrs, 2);

                            }
                            foreach (DataRow drlive1 in dtlivedata1.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate).AddDays(-1) + "'"))
                            {
                                double milkkgs1 = 0;
                                double.TryParse(drlive1["Milkkg"].ToString(), out milkkgs1);
                                double milkltrs1 = milkkgs1 / 1.03;
                                totmilklitrs1 += milkltrs1;
                                totlmilklitrs1 += milkltrs1;
                                double diffmilkliters = 0;
                                diffmilkliters = totlmilklitrs - totmilklitrs1;
                                diffmilkliters1 = totlmilklitrs - totmilklitrs1;
                                newrow["Diff"] = Math.Round(diffmilkliters, 2);
                            }
                            avgfat1 = Totalfat1 / totalsum;
                            avgsnf1 = TotalSnf1 / totalsum1;
                            // avgmilkliters = totmilklitrs / totalsum1;
                            avgmilkliters1 = totlmilklitrs1 / totalsum1;
                            //double diffmilkliters1 = avgmilkliters1 - avgmilkliters;
                            //newrow["Diff"] = Math.Round(diffmilkliters1, 2);
                            TotalSnf += avgsnf1;
                            Totalfat += avgfat1;
                            //totaldiffmilkliters += diffmilkliters1;
                            newrow["FAT"] = Math.Round(avgfat1, 2);
                            newrow["SNF"] = Math.Round(avgsnf1, 2);
                            totmilklitrs = 0;
                            diffmilkliters1 = 0;
                            TotalSnf1 = 0;
                            totalsum = 0;
                            totalsum1 = 0;
                            Totalfat1 = 0;
                            avgfat1 = 0;
                            avgsnf1 = 0;
                            totaldiffmilkliters1 = 0;
                            Report.Rows.Add(newrow);

                            DataTable dtin = new DataTable();
                            DataRow[] drr = dtsuperwise.Select("PlantCode='" + prevRouteId + "'");
                            if (drr.Length > 0)
                            {
                                dtin = drr.CopyToDataTable();
                            }
                            int dttotalpocount = dtin.Rows.Count;
                            if (dttotalpocount > 1)
                            {

                            }
                            else
                            {
                                DataRow newrow1 = Report.NewRow();
                                newrow1["SupervisorName"] = "Total";
                                avgfat = Totalfat / rowcount;
                                avgsnf = TotalSnf / rowcount;
                                avgmilkliters = totaldiffmilkliters / rowcount;
                                newrow1["FAT"] = Math.Round(avgfat, 2);
                                newrow1["SNF"] = Math.Round(avgsnf, 2);
                                double totaldiffernce = totlmilklitrs - diffmilkliters1;
                                //newrow1["AM LTRS"] = Math.Round(totlmilklitrs, 2);
                                newrow1["Diff"] = Math.Round(totaldiffernce, 2);
                                Report.Rows.Add(newrow1);
                                gtotaldiffernce += totaldiffernce;
                                gtotlmilklitrs += totlmilklitrs;
                                totlmilklitrs = 0;
                                totaldiffernce = 0;
                                count++;
                                rowcount = 1;
                            }

                        }
                    }

                    //gtotlmilklitrs += totlmilklitrs;
                    //DataRow salesreport1 = Report.NewRow();
                    //salesreport1["Session"] = "Grand Total";
                    //salesreport1["Session"] = "Total";
                    //salesreport1["FAT"] = Math.Round(avgfat, 2);
                    //salesreport1["SNF"] = Math.Round(avgsnf, 2);
                    //salesreport1["AM LTRS"] = Math.Round(totlmilklitrs, 2);
                    //Report.Rows.Add(salesreport1);
                    DataRow salesreport2 = Report.NewRow();
                    // salesreport2["Date"] = GetLowDate(fromdate);
                    Report.Rows.Add(salesreport2);
                    DataRow salesreport3 = Report.NewRow();

                    Report.Rows.Add(salesreport3);
                    grdinworddata1.DataSource = Report;
                    grdinworddata1.DataBind();
                    hidepanel.Visible = true;
                }

            }
            cmd = new SqlCommand("SELECT alltable.SupervisorName, alltable.routeid, alltable.RouteName, pro.PlantCode FROM  (SELECT DISTINCT RouteId AS R, PlantCode  FROM  Lp  WHERE (PlantCode = @plantcode) AND (Prdate BETWEEN @fromdate AND @fromdate) AND (Sessions =@session)) AS pro LEFT OUTER JOIN  (SELECT  aa.Supervisor_Code, bb.SupervisorName, aa.routeid, CONVERT(nvarchar(50), aa.routeid) + '_' + aa.Route_Name AS RouteName  FROM (SELECT A.Supervisor_Code, B.Route_Name, B.routeid  FROM (SELECT  TId, Company_Code, Supervisor_Code, Route_Id   FROM Supervisor_RouteAllotment  WHERE (Plant_Code = @plantcode)) AS A LEFT OUTER JOIN   (SELECT Route_Name, Route_ID AS routeid  FROM  Route_Master  WHERE (Plant_Code = @plantcode)) AS B ON A.Route_Id = B.routeid) AS aa LEFT OUTER JOIN  (SELECT  Supervisor_Code, SupervisorName, Company_Code, Plant_Code, Added_Date, Dob, Address, Mobile, Bank_name, IfscCode, AccountNumber, Pannumber, Qualification, Description   FROM Supervisor_Details WHERE (Plant_Code = @plantcode)) AS bb ON aa.Supervisor_Code = bb.Supervisor_Code) AS alltable ON pro.R = alltable.routeid WHERE  (alltable.routeid > 0) ORDER BY alltable.SupervisorName");
            cmd.Parameters.Add("@plantcode", plancode1);
            cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
            cmd.Parameters.Add("@session", "PM");
            DataTable dtsuperwise1 = vdm.SelectQuery(cmd).Tables[0];
            if (dtsuperwise1.Rows.Count > 0)
            {
                double avgmilkliters1 = 0;
                double totmilklitrs1 = 0;
                double totlmilklitrs1 = 0;
                double totmilklitrs = 0;
                double totlmilklitrs = 0;
                double Totalfat = 0;
                double TotalSnf = 0;
                double Totalfat1 = 0;
                double TotalSnf1 = 0;
                double avgsnf = 0;
                double avgfat = 0;
                double avgfat1 = 0;
                double avgsnf1 = 0;
                double gtotlmilklitrs = 0;
                double avgmilkliters = 0;
                double totaldiffmilkliters = 0;
                double gtotalamount = 0;
                double totalamount = 0;
                double totlmilkkgs = 0;
                double totaldiffmilkliters1 = 0;
                double diffmilkliters1 = 0;
                double gtotaldiffernce = 0;
                int totalsum = 0;
                int totalsum1 = 0;
                cmd = new SqlCommand("SELECT  RouteTimeMaintain.Route_id, RouteTimeMaintain.VehicleSettime, RouteTimeMaintain.VehicleInttime, RouteTimeMaintain.Date, RouteTimeMaintain.Session, RouteTimeMaintain.MBRT,  RouteTimeMaintain.Plant_code FROM    Route_Master INNER JOIN   RouteTimeMaintain ON Route_Master.Route_ID = RouteTimeMaintain.Route_id WHERE (RouteTimeMaintain.Plant_code = @plantcode) AND (RouteTimeMaintain.Date = @fromdate) AND (RouteTimeMaintain.Session=@session)   GROUP BY RouteTimeMaintain.Date, RouteTimeMaintain.VehicleSettime, RouteTimeMaintain.VehicleInttime, RouteTimeMaintain.Date, RouteTimeMaintain.Session,  RouteTimeMaintain.MBRT, RouteTimeMaintain.Plant_code,RouteTimeMaintain.Route_id");
                cmd.Parameters.Add("@plantcode", plantcode);
                cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
                cmd.Parameters.Add("@session", "PM");
                DataTable dtroute = vdm.SelectQuery(cmd).Tables[0];
                cmd = new SqlCommand("SELECT PlantCode, Prdate, Fat, Snf, Sessions, Milkkg, COUNT(Fat) AS value, COUNT(Snf) AS value1, StartingTime, EndingTime, RouteId FROM  Lp WHERE (PlantCode = @plantcode) AND (Prdate = @fromdate) AND (RouteId IS NOT NULL) AND (Sessions =@session)  GROUP BY PlantCode, Prdate, Sessions, Fat, Snf, StartingTime, Milkkg, EndingTime, RouteId");
                cmd.Parameters.Add("@plantcode", plantcode);
                cmd.Parameters.Add("@fromdate", GetLowDate(fromdate));
                cmd.Parameters.Add("@session", "PM");
                DataTable dtlivedata = vdm.SelectQuery(cmd).Tables[0];
                cmd = new SqlCommand("SELECT PlantCode, Prdate, Fat, Snf, Sessions, Milkkg, COUNT(Fat) AS value, COUNT(Snf) AS value1, StartingTime, EndingTime, RouteId FROM  Lp WHERE (PlantCode = @plantcode) AND (Prdate = @fromdate) AND (RouteId IS NOT NULL) AND (Sessions =@session)  GROUP BY PlantCode, Prdate, Sessions, Fat, Snf, StartingTime, Milkkg, EndingTime, RouteId");
                cmd.Parameters.Add("@plantcode", plantcode);
                cmd.Parameters.Add("@fromdate", GetLowDate(fromdate).AddDays(-1));
                cmd.Parameters.Add("@session", "PM");
                DataTable dtlivedata1 = vdm.SelectQuery(cmd).Tables[0];
                DateTime dt = DateTime.Now;
                string prevdate = string.Empty;
                string session;
                int prevRouteId = 0;
                var i = 1;
                int count = 1;
                int rowcount = 1;
                foreach (DataRow drsuper in dtsuperwise1.Rows)
                {
                    DataRow newrow = Report.NewRow();
                    //newrow["PlantCode"] = drsuper["Plant_Code"].ToString();
                    newrow["RouteName"] = drsuper["RouteName"].ToString();
                    newrow["SupervisorName"] = drsuper["SupervisorName"].ToString();
                    newrow["Sno"] = i++.ToString();
                    int presntRouteId = 0;
                    int.TryParse(drsuper["PlantCode"].ToString(), out presntRouteId);
                    int routeid = 0;
                    int.TryParse(drsuper["PlantCode"].ToString(), out routeid);
                    if (presntRouteId == prevRouteId)
                    {
                        foreach (DataRow drmaintain in dtroute.Select("Plant_code='" + drsuper["PlantCode"].ToString() + "' AND Route_id='" + drsuper["routeid"].ToString() + "'"))
                        {
                            newrow["Dairy Time"] = drmaintain["VehicleSettime"].ToString();
                            newrow["IN TIME"] = drmaintain["VehicleInttime"].ToString();
                            newrow["MBRT"] = drmaintain["MBRT"].ToString();
                            //newrow["Date"] = drmaintain["Date"].ToString();
                            // session = drmaintain["Session"].ToString();

                        }
                        foreach (DataRow drlive in dtlivedata.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate) + "'"))
                        {
                            double milkkgs = 0;
                            double.TryParse(drlive["Milkkg"].ToString(), out milkkgs);
                            double milkltrs = milkkgs / 1.03;
                            totmilklitrs += milkltrs;
                            totlmilklitrs += milkltrs;
                            double fat = 0;
                            double.TryParse(drlive["Fat"].ToString(), out fat);
                            Totalfat1 += fat;
                            //Totalfat += fat;
                            double snf = 0;
                            double.TryParse(drlive["Snf"].ToString(), out snf);
                            TotalSnf1 += snf;
                            //TotalSnf += snf;
                            int sum = 0; int sum1 = 0;
                            int.TryParse(drlive["value"].ToString(), out sum);
                            totalsum += sum;
                            int.TryParse(drlive["value1"].ToString(), out sum1);
                            totalsum1 += sum;

                            //newrow["FAT"] = drlive["Fat"].ToString();
                            newrow["Duming Start Time"] = drlive["StartingTime"].ToString();
                            newrow["Duming End Time"] = drlive["EndingTime"].ToString();
                            //newrow["SNF"] = drlive["Snf"].ToString();
                            newrow["AM LTRS"] = Math.Round(totmilklitrs, 2);

                        }

                        foreach (DataRow drlive1 in dtlivedata1.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate).AddDays(-1) + "'"))
                        {
                            double milkkgs1 = 0;
                            double.TryParse(drlive1["Milkkg"].ToString(), out milkkgs1);
                            double milkltrs1 = milkkgs1 / 1.03;
                            totmilklitrs1 += milkltrs1;
                            totlmilklitrs1 += milkltrs1;
                            double diffmilkliters = 0;
                            diffmilkliters = totlmilklitrs - totmilklitrs1;
                            diffmilkliters1 = totlmilklitrs - totmilklitrs1;
                            newrow["Diff"] = Math.Round(diffmilkliters, 2);
                        }
                        avgfat1 = Totalfat1 / totalsum;
                        avgsnf1 = TotalSnf1 / totalsum1;
                        // avgmilkliters = totmilklitrs / totalsum1;
                        avgmilkliters1 = totlmilklitrs1 / totalsum1;
                        //double diffmilkliters1 = avgmilkliters1 - avgmilkliters;
                        //newrow["Diff"] = Math.Round(diffmilkliters1, 2);
                        TotalSnf += avgsnf1;
                        Totalfat += avgfat1;
                        //totaldiffmilkliters += diffmilkliters1;
                        newrow["FAT"] = Math.Round(avgfat1, 2);
                        newrow["SNF"] = Math.Round(avgsnf1, 2);
                        totmilklitrs = 0;
                        diffmilkliters1 = 0;
                        TotalSnf1 = 0;
                        totalsum = 0;
                        totalsum1 = 0;
                        Totalfat1 = 0;
                        avgfat1 = 0;
                        avgsnf1 = 0;
                        totaldiffmilkliters1 = 0;
                        Report.Rows.Add(newrow);
                        rowcount++;
                        DataTable dtin = new DataTable();
                        DataRow[] drr = dtsuperwise1.Select("PlantCode='" + presntRouteId + "'");
                        if (drr.Length > 0)
                        {
                            dtin = drr.CopyToDataTable();
                        }
                        int dttotalpocount = dtin.Rows.Count;
                        if (dttotalpocount == rowcount)
                        {
                            DataRow newrow1 = Report.NewRow();
                            avgfat = Totalfat / rowcount;
                            avgsnf = TotalSnf / rowcount;
                            avgmilkliters = totaldiffmilkliters / rowcount;
                            newrow1["FAT"] = Math.Round(avgfat, 2);
                            newrow1["SNF"] = Math.Round(avgsnf, 2);
                            double totaldiffernce = totlmilklitrs - diffmilkliters1;
                            newrow1["AM LTRS"] = Math.Round(totlmilklitrs, 2);
                            //newrow1["Diff"] = Math.Round(totaldiffernce, 2);
                            Report.Rows.Add(newrow1);
                            gtotaldiffernce += totaldiffernce;
                            gtotlmilklitrs += totlmilklitrs;
                            totlmilklitrs = 0;
                            totaldiffernce = 0;
                            rowcount = 1;
                        }
                    }
                    else
                    {
                        prevRouteId = presntRouteId;
                        foreach (DataRow drmaintain in dtroute.Select("Plant_code='" + drsuper["PlantCode"].ToString() + "' AND Route_id='" + drsuper["routeid"].ToString() + "'"))
                        {
                            newrow["Dairy Time"] = drmaintain["VehicleSettime"].ToString();
                            newrow["IN TIME"] = drmaintain["VehicleInttime"].ToString();
                            newrow["MBRT"] = drmaintain["MBRT"].ToString();
                            //  newrow["Date"] = drmaintain["Date"].ToString();
                            // session = drmaintain["Session"].ToString();
                        }
                        foreach (DataRow drlive in dtlivedata.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate) + "'"))
                        {
                            double milkkgs = 0;
                            double.TryParse(drlive["Milkkg"].ToString(), out milkkgs);
                            double milkltrs = milkkgs / 1.03;
                            totmilklitrs += milkltrs;
                            totlmilklitrs += milkltrs;
                            double fat = 0;
                            double.TryParse(drlive["Fat"].ToString(), out fat);
                            Totalfat1 += fat;
                            //Totalfat += fat;
                            double snf = 0;
                            double.TryParse(drlive["Snf"].ToString(), out snf);
                            TotalSnf1 += snf;
                            //TotalSnf += snf;
                            int sum = 0; int sum1 = 0;
                            int.TryParse(drlive["value"].ToString(), out sum);
                            totalsum += sum;
                            int.TryParse(drlive["value1"].ToString(), out sum1);
                            totalsum1 += sum;
                            //newrow["FAT"] = drlive["Fat"].ToString();
                            newrow["Duming Start Time"] = drlive["StartingTime"].ToString();
                            newrow["Duming End Time"] = drlive["EndingTime"].ToString();
                            //newrow["SNF"] = drlive["Snf"].ToString();
                            newrow["AM LTRS"] = Math.Round(totmilklitrs, 2);
                        }
                        foreach (DataRow drlive1 in dtlivedata1.Select("PlantCode='" + drsuper["PlantCode"].ToString() + "' AND RouteId='" + drsuper["routeid"].ToString() + "' AND Prdate='" + GetLowDate(fromdate).AddDays(-1) + "'"))
                        {
                            double milkkgs1 = 0;
                            double.TryParse(drlive1["Milkkg"].ToString(), out milkkgs1);
                            double milkltrs1 = milkkgs1 / 1.03;
                            totmilklitrs1 += milkltrs1;
                            totlmilklitrs1 += milkltrs1;
                            double diffmilkliters = 0;
                            diffmilkliters = totlmilklitrs - totmilklitrs1;
                            diffmilkliters1 = totlmilklitrs - totmilklitrs1;
                            newrow["Diff"] = Math.Round(diffmilkliters, 2);
                        }
                        avgfat1 = Totalfat1 / totalsum;
                        avgsnf1 = TotalSnf1 / totalsum1;
                        // avgmilkliters = totmilklitrs / totalsum1;
                        avgmilkliters1 = totlmilklitrs1 / totalsum1;
                        //double diffmilkliters1 = avgmilkliters1 - avgmilkliters;
                        //newrow["Diff"] = Math.Round(diffmilkliters1, 2);
                        TotalSnf += avgsnf1;
                        Totalfat += avgfat1;
                        //totaldiffmilkliters += diffmilkliters1;
                        newrow["FAT"] = Math.Round(avgfat1, 2);
                        newrow["SNF"] = Math.Round(avgsnf1, 2);
                        totmilklitrs = 0;
                        diffmilkliters1 = 0;
                        TotalSnf1 = 0;
                        totalsum = 0;
                        totalsum1 = 0;
                        Totalfat1 = 0;
                        avgfat1 = 0;
                        avgsnf1 = 0;
                        totaldiffmilkliters1 = 0;
                        Report.Rows.Add(newrow);

                        DataTable dtin = new DataTable();
                        DataRow[] drr = dtsuperwise1.Select("PlantCode='" + prevRouteId + "'");
                        if (drr.Length > 0)
                        {
                            dtin = drr.CopyToDataTable();
                        }
                        int dttotalpocount = dtin.Rows.Count;
                        if (dttotalpocount > 1)
                        {

                        }
                        else
                        {
                            DataRow newrow1 = Report.NewRow();
                            newrow1["SupervisorName"] = "Total";
                            avgfat = Totalfat / rowcount;
                            avgsnf = TotalSnf / rowcount;
                            avgmilkliters = totaldiffmilkliters / rowcount;
                            newrow1["FAT"] = Math.Round(avgfat, 2);
                            newrow1["SNF"] = Math.Round(avgsnf, 2);
                            double totaldiffernce = totlmilklitrs - diffmilkliters1;
                            newrow1["AM LTRS"] = Math.Round(totlmilklitrs, 2);
                            //newrow1["Diff"] = Math.Round(totaldiffernce, 2);
                            Report.Rows.Add(newrow1);
                            gtotaldiffernce += totaldiffernce;
                            gtotlmilklitrs += totlmilklitrs;
                            totlmilklitrs = 0;
                            totaldiffernce = 0;
                            count++;
                            rowcount = 1;
                        }

                    }
                }
                //gtotlmilklitrs += totlmilklitrs;
                //DataRow salesreport1 = Report.NewRow();
                //salesreport1["Session"] = "Grand Total";
                //salesreport1["Session"] = "Total";
                //salesreport1["FAT"] = Math.Round(avgfat, 2);
                //salesreport1["SNF"] = Math.Round(avgsnf, 2);
                //salesreport1["AM LTRS"] = Math.Round(totlmilklitrs, 2);
                //Report.Rows.Add(salesreport1);
                DataRow salesreport2 = Report.NewRow();
                //salesreport2["Date"] = GetLowDate(fromdate);
                Report.Rows.Add(salesreport2);
                DataRow salesreport3 = Report.NewRow();
                Report.Rows.Add(salesreport3);

                if (Report.Rows.Count > 0)
                {
                    grdinworddata1.DataSource = Report;
                    grdinworddata1.DataBind();
                    hidepanel.Visible = true;
                }
            }


            // lblmsg.Text = "No data were found";
            //  hidepanel.Visible = false;

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void grdReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "Total")
            {
                e.Row.BackColor = System.Drawing.Color.Aquamarine;
                e.Row.Font.Size = FontUnit.Medium;
                e.Row.Font.Bold = true;
            }
        }
    }
    protected void grdinworddata1_DataBinding(object sender, EventArgs e)
    {
        //try
        //{
        //    GridViewGroup First = new GridViewGroup(grdinworddata1, null, "SupervisorName");
        //    // GridViewGroup seconf = new GridViewGroup(grdReports, First, "Department");
        //    //GridViewGroup seconf = new GridViewGroup(grdReports, First, "Location");
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
}