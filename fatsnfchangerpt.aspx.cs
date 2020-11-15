using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class fatsnfchangerpt : System.Web.UI.Page
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
            if (ddltype.SelectedItem.Value == "Fat Snf Range Wise Quantity")
            {
                Report.Columns.Add("Plant_Name");
                Report.Columns.Add("AgentId");
                Report.Columns.Add("Date");
                Report.Columns.Add("Milkqty").DataType = typeof(double);
                Report.Columns.Add("Fat").DataType = typeof(double);
                Report.Columns.Add("Snf").DataType = typeof(double); 
                Report.Columns.Add("Ts");
                Report.Columns.Add("TsRate");
                Report.Columns.Add("MilkAmount").DataType = typeof(double);
                Report.Columns.Add("ModifyMilkqty").DataType = typeof(double);
                Report.Columns.Add("ModifyFat").DataType = typeof(double);
                Report.Columns.Add("Modifysnf").DataType = typeof(double); 
                Report.Columns.Add("ModifyTs");
                Report.Columns.Add("ModifyTsRate");
                Report.Columns.Add("ModifyAmount").DataType = typeof(double);
                Report.Columns.Add("Difffat").DataType = typeof(double);
                Report.Columns.Add("Diffsnf").DataType = typeof(double); 
                Report.Columns.Add("DiffAmt").DataType = typeof(double); 


                cmd = new SqlCommand("SELECT DISTINCT modifydata.agentid  FROM    modifydata INNER JOIN  Plant_Master ON Plant_Master.Plant_Code = modifydata.plant_code  WHERE        (modifydata.plant_code = @plantcode) AND (modifydata.doe BETWEEN @d1 AND @d2)");
                cmd.Parameters.Add("@d1", GetLowDate(fromdate));
                cmd.Parameters.Add("@d2", GetHighDate(todate));
                cmd.Parameters.Add("@plantcode", Plant_Code);
                DataTable dtagents = vdm.SelectQuery(cmd).Tables[0];


                cmd = new SqlCommand("SELECT Plant_Master.Plant_Name, modifydata.agentid, CONVERT(VARCHAR(10), modifydata.doe, 101) As Date,  modifydata.milkkg, modifydata.fat, modifydata.snf, modifydata.modifykg, modifydata.modifyfat, modifydata.modifysnf, ROUND(modifydata.modifyfat - modifydata.fat, 1) AS difffat,  ROUND(modifydata.modifysnf - modifydata.snf, 1) AS diffsnf, modifydata.plant_code, modifydata.fat + modifydata.snf AS Ts, modifydata.modifyfat + modifydata.modifysnf AS Ts2  FROM modifydata INNER JOIN  Plant_Master ON  Plant_Master.Plant_Code =modifydata.plant_code  WHERE (modifydata.plant_code = @plantcode) AND (modifydata.doe BETWEEN @d1 AND @d2) ORDER BY modifydata.agentid, modifydata.doe");
                cmd.Parameters.Add("@d1", GetLowDate(fromdate));
                cmd.Parameters.Add("@d2", GetHighDate(todate));
                cmd.Parameters.Add("@plantcode", Plant_Code);
                DataTable dtagentmilkdetails = vdm.SelectQuery(cmd).Tables[0];


                cmd = new SqlCommand("Select   cchartname   from (Select cchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf  from (Select Ratechart_Id,Plant_Code as ppcode   from Procurement  where  Plant_Code=@pcode  and Prdate between @d12 and @d13  group by Ratechart_Id,Plant_Code) as gg left join (Select Chart_Name as cchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code as cmplant   from Chart_Master where Plant_Code=@pcode group by Chart_Name,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code ) as  cm on gg.ppcode=cm.cmplant and gg.Ratechart_Id=cm.cchartname) as leftside  left join (Select Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code as rcplant   from Rate_Chart  where Plant_Code=@pcode group by Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code  ) as rc on leftside.ppcode=rc.rcplant and leftside.cchartname=rc.Chart_Name   group by cchartname");
                cmd.Parameters.Add("@d12", GetLowDate(fromdate));
                cmd.Parameters.Add("@d13", GetHighDate(todate));
                cmd.Parameters.Add("@pcode", Plant_Code);
                DataTable dtttchartname = vdm.SelectQuery(cmd).Tables[0];
                if (dtttchartname.Rows.Count > 0)
                {
                    foreach (DataRow name in dtttchartname.Rows)
                    {
                        string ChartName = name[0].ToString();
                        cmd = new SqlCommand("Select   cmchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Comission_Amount   from (Select cmchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf  from (Select Ratechart_Id,Plant_Code as ppcode   from Procurement  where  Plant_Code=@pcode  and Prdate between @d12 and @d13  and Ratechart_Id=@chartname group by Ratechart_Id,Plant_Code) as gg left join (Select Chart_Name as cmchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code as cmplant   from Chart_Master where Plant_Code=@pcode group by Chart_Name,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code ) as  cm on gg.ppcode=cm.cmplant and gg.Ratechart_Id=cm.cmchartname) as leftside  left join (Select Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code as rcplant   from Rate_Chart  where Plant_Code=@pcode group by Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code  ) as rc on leftside.ppcode=rc.rcplant and leftside.cmchartname=rc.Chart_Name");
                        cmd.Parameters.Add("@d12", GetLowDate(fromdate));
                        cmd.Parameters.Add("@d13", GetHighDate(todate));
                        cmd.Parameters.Add("@pcode", Plant_Code);
                        cmd.Parameters.Add("@chartname", ChartName);
                        DataTable dtfatsnf = vdm.SelectQuery(cmd).Tables[0];
                        string type = dtfatsnf.Rows[0][1].ToString();

                        cmd = new SqlCommand("Select cmchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,From_RangeValue,To_RangeValue,convert(decimal(18,2),Rate) as Rate,Comission_Amount   from (Select cmchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf  from (Select Ratechart_Id,Plant_Code as ppcode   from Procurement  where  Plant_Code=@pcode  and Prdate between @d12 and @d13 AND Ratechart_Id=@chartname  group by Ratechart_Id,Plant_Code) as gg left join (Select Chart_Name as cmchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code as cmplant   from Chart_Master where Plant_Code=@pcode group by Chart_Name,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code ) as  cm on gg.ppcode=cm.cmplant and gg.Ratechart_Id=cm.cmchartname) as leftside  left join (Select Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code as rcplant   from Rate_Chart  where Plant_Code=@pcode group by Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code  ) as rc on leftside.ppcode=rc.rcplant and leftside.cmchartname=rc.Chart_Name");
                        cmd.Parameters.Add("@d12", GetLowDate(fromdate));
                        cmd.Parameters.Add("@d13", GetHighDate(todate));
                        cmd.Parameters.Add("@pcode", Plant_Code);
                        cmd.Parameters.Add("@chartname", ChartName);
                        DataTable dtfatsnfrange = vdm.SelectQuery(cmd).Tables[0];


                        foreach (DataRow dragent in dtagents.Rows)
                        {
                            string agent = dragent["agentid"].ToString();

                            foreach (DataRow dr in dtfatsnfrange.Rows)
                            {
                                string fromrange = dr["From_RangeValue"].ToString();
                                string torange = dr["To_RangeValue"].ToString();
                                string Rate = dr["Rate"].ToString();
                                if (type == "TS")
                                {
                                    if (dtagentmilkdetails.Rows.Count > 0)
                                    {
                                        int i = 1;
                                        foreach (DataRow drsale in dtagentmilkdetails.Select("Ts >= '" + fromrange + "' AND Ts <= '" + torange + "' AND agentid = '" + agent + "'"))
                                        {
                                            i++;
                                            DataRow newrow = Report.NewRow();
                                            string plantcode = drsale["plant_code"].ToString();
                                            string plantname = drsale["Plant_Name"].ToString();
                                            string agentid = drsale["agentid"].ToString();
                                            string Date = drsale["Date"].ToString();
                                            string milkkg = drsale["milkkg"].ToString();
                                            string fat = drsale["fat"].ToString();
                                            string snf = drsale["snf"].ToString();
                                            string modifyfat = drsale["modifyfat"].ToString();
                                            string modifysnf = drsale["modifysnf"].ToString();
                                            string modifykg = drsale["modifykg"].ToString();
                                            string difffat = drsale["difffat"].ToString();
                                            string diffsnf = drsale["diffsnf"].ToString();
                                            string ts = drsale["Ts"].ToString();
                                            double tsrate = Convert.ToDouble(Rate);
                                            double tsval = Convert.ToDouble(ts);
                                            double tsamt = tsrate * tsval;
                                            double totalmilkval = Math.Round(tsamt * Convert.ToDouble(milkkg), 2);
                                            newrow["MilkAmount"] = totalmilkval.ToString();
                                            string ts2 = drsale["Ts2"].ToString();
                                            newrow["Plant_Name"] = plantname;
                                            newrow["AgentId"] = agentid;
                                            newrow["Date"] = Date;
                                            newrow["Milkqty"] = milkkg;
                                            newrow["Fat"] = fat;
                                            newrow["Snf"] = snf;
                                            newrow["ModifyFat"] = modifyfat;
                                            newrow["Modifysnf"] = modifysnf;
                                            newrow["ModifyMilkqty"] = modifykg;
                                            newrow["Difffat"] = difffat;
                                            newrow["Diffsnf"] = diffsnf;
                                            newrow["Ts"] = ts;
                                            newrow["TsRate"] = Rate;
                                            newrow["ModifyTs"] = ts2;
                                            string fromrangets = "";
                                            string torangets = "";

                                            if (plantcode == "156")
                                            {
                                                if (Convert.ToDouble(ts2) >= 10 && Convert.ToDouble(ts2) <= 11)
                                                {
                                                    torangets = "11";
                                                    fromrangets = "10";
                                                }
                                                if (Convert.ToDouble(ts2) >= 11.1 && Convert.ToDouble(ts2) <= 11.4)
                                                {
                                                    torangets = "11.4";
                                                    fromrangets = "11.1";
                                                }
                                                if (Convert.ToDouble(ts2) >= 11.5 && Convert.ToDouble(ts2) <= 11.9)
                                                {
                                                    torangets = "11.9";
                                                    fromrangets = "11.5";
                                                }
                                                if (Convert.ToDouble(ts2) >= 12 && Convert.ToDouble(ts2) <= 12.4)
                                                {
                                                    torangets = "12.4";
                                                    fromrangets = "12";
                                                }
                                                if (Convert.ToDouble(ts2) >= 12.5 && Convert.ToDouble(ts2) <= 20)
                                                {
                                                    torangets = "20";
                                                    fromrangets = "12.5";
                                                }
                                            }
                                            else
                                            {
                                                if (plantcode == "156")
                                                {
                                                    if (Convert.ToDouble(ts2) >= 7 && Convert.ToDouble(ts2) <= 7)
                                                    {
                                                        torangets = "7";
                                                        fromrangets = "7";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.1 && Convert.ToDouble(ts2) <= 7.1)
                                                    {
                                                        torangets = "7.1";
                                                        fromrangets = "7.1";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.2 && Convert.ToDouble(ts2) <= 7.2)
                                                    {
                                                        torangets = "7.2";
                                                        fromrangets = "7.2";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.3 && Convert.ToDouble(ts2) <= 7.3)
                                                    {
                                                        torangets = "7.3";
                                                        fromrangets = "7.3";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.4 && Convert.ToDouble(ts2) <= 7.4)
                                                    {
                                                        torangets = "7.4";
                                                        fromrangets = "7.4";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.4 && Convert.ToDouble(ts2) <= 7.4)
                                                    {
                                                        torangets = "7.4";
                                                        fromrangets = "7.4";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.5 && Convert.ToDouble(ts2) <= 7.5)
                                                    {
                                                        torangets = "7.5";
                                                        fromrangets = "7.5";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.6 && Convert.ToDouble(ts2) <= 7.6)
                                                    {
                                                        torangets = "7.6";
                                                        fromrangets = "7.6";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.7 && Convert.ToDouble(ts2) <= 7.7)
                                                    {
                                                        torangets = "7.7";
                                                        fromrangets = "7.7";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.8 && Convert.ToDouble(ts2) <= 7.8)
                                                    {
                                                        torangets = "7.8";
                                                        fromrangets = "7.8";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 7.9 && Convert.ToDouble(ts2) <= 7.9)
                                                    {
                                                        torangets = "7.9";
                                                        fromrangets = "7.9";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 8 && Convert.ToDouble(ts2) <= 8.2)
                                                    {
                                                        torangets = "8.2";
                                                        fromrangets = "8";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 8.3 && Convert.ToDouble(ts2) <= 8.3)
                                                    {
                                                        torangets = "8.3";
                                                        fromrangets = "8.3";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 8.4 && Convert.ToDouble(ts2) <= 8.4)
                                                    {
                                                        torangets = "8.4";
                                                        fromrangets = "8.4";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 8.5 && Convert.ToDouble(ts2) <= 8.9)
                                                    {
                                                        torangets = "8.9";
                                                        fromrangets = "8.5";
                                                    }
                                                }
                                                else
                                                {
                                                    if (Convert.ToDouble(ts2) >= 9.5 && Convert.ToDouble(ts2) <= 9.9)
                                                    {
                                                        torangets = "9.9";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 10 && Convert.ToDouble(ts2) <= 10.5)
                                                    {
                                                        torangets = "10.5";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 10.6 && Convert.ToDouble(ts2) <= 11)
                                                    {
                                                        torangets = "11";
                                                    }

                                                    if (Convert.ToDouble(ts2) >= 11.1 && Convert.ToDouble(ts2) <= 11.5)
                                                    {
                                                        torangets = "11.5";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 11.6 && Convert.ToDouble(ts2) <= 11.9)
                                                    {
                                                        torangets = "11.9";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 12 && Convert.ToDouble(ts2) <= 12.2)
                                                    {
                                                        torangets = "12.2";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 12.3 && Convert.ToDouble(ts2) <= 12.5)
                                                    {
                                                        torangets = "12.5";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 12.6 && Convert.ToDouble(ts2) <= 20)
                                                    {
                                                        torangets = "20";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 20 && Convert.ToDouble(ts2) <= 20.5)
                                                    {
                                                        torangets = "20.5";
                                                    }


                                                    if (Convert.ToDouble(ts2) >= 9.5 && Convert.ToDouble(ts2) <= 9.9)
                                                    {
                                                        fromrangets = "9.5";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 10 && Convert.ToDouble(ts2) <= 10.5)
                                                    {
                                                        fromrangets = "10";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 10.6 && Convert.ToDouble(ts2) <= 11)
                                                    {
                                                        fromrangets = "10.6";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 11.1 && Convert.ToDouble(ts2) <= 11.5)
                                                    {
                                                        fromrangets = "11.1";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 11.6 && Convert.ToDouble(ts2) <= 11.9)
                                                    {
                                                        fromrangets = "11.6";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 12 && Convert.ToDouble(ts2) <= 12.2)
                                                    {
                                                        fromrangets = "12";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 12.3 && Convert.ToDouble(ts2) <= 12.5)
                                                    {
                                                        fromrangets = "12.3";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 12.6 && Convert.ToDouble(ts2) <= 20)
                                                    {
                                                        fromrangets = "12.6";
                                                    }
                                                    if (Convert.ToDouble(ts2) >= 20 && Convert.ToDouble(ts2) <= 20.5)
                                                    {
                                                        fromrangets = "20";
                                                    }
                                                }
                                            }
                                            foreach (DataRow drRs in dtfatsnfrange.Select("From_RangeValue = '" + fromrangets + "' AND To_RangeValue = '" + torangets + "'"))
                                            {
                                                string Ratee = drRs["Rate"].ToString();
                                                newrow["ModifyTsRate"] = Ratee;

                                                double tsrate1 = Convert.ToDouble(Ratee);
                                                double tsval1 = Convert.ToDouble(ts2);
                                                double tsamt1 = tsrate1 * tsval1;
                                                double totalmilkval1 = Math.Round(tsamt1 * Convert.ToDouble(modifykg), 2);
                                                newrow["ModifyAmount"] = totalmilkval1.ToString();
                                                double diffamt = Math.Round(totalmilkval1 - totalmilkval, 2);
                                                newrow["DiffAmt"] = diffamt.ToString();
                                            }
                                            Report.Rows.Add(newrow);
                                        }
                                    }
                                }
                                if (type == "FAT")
                                {
                                    if (dtagentmilkdetails.Rows.Count > 0)
                                    {
                                        int i = 1;
                                        foreach (DataRow drsale in dtagentmilkdetails.Select("fat >= '" + fromrange + "' AND fat <= '" + torange + "' AND agentid = '" + agent + "'"))
                                        {
                                            i++;
                                            DataRow newrow = Report.NewRow();
                                            string plantcode = drsale["plant_code"].ToString();
                                            string plantname = drsale["Plant_Name"].ToString();
                                            string agentid = drsale["agentid"].ToString();
                                            string Date = drsale["Date"].ToString();
                                            string milkkg = drsale["milkkg"].ToString();
                                            string fat = drsale["fat"].ToString();
                                            string snf = drsale["snf"].ToString();
                                            string modifyfat = drsale["modifyfat"].ToString();
                                            string modifysnf = drsale["modifysnf"].ToString();
                                            string modifykg = drsale["modifykg"].ToString();
                                            string difffat = drsale["difffat"].ToString();
                                            string diffsnf = drsale["diffsnf"].ToString();

                                            double tsrate = Convert.ToDouble(Rate);
                                            double tfat = Convert.ToDouble(fat);
                                            double tsamt = tfat * tsrate;
                                            double totalmilkval = Math.Round(tsamt * Convert.ToDouble(milkkg), 2);
                                            newrow["MilkAmount"] = totalmilkval.ToString();
                                            newrow["Plant_Name"] = plantname;
                                            newrow["AgentId"] = agentid;
                                            newrow["Date"] = Date;
                                            newrow["Milkqty"] = milkkg;
                                            newrow["Fat"] = fat;
                                            newrow["Snf"] = snf;
                                            newrow["ModifyFat"] = modifyfat;
                                            newrow["Modifysnf"] = modifysnf;
                                            newrow["ModifyMilkqty"] = modifykg;
                                            newrow["Difffat"] = difffat;
                                            newrow["Diffsnf"] = diffsnf;
                                            newrow["TsRate"] = Rate;
                                            string fromrangets = "";
                                            string torangets = "";
                                            if (Convert.ToDouble(modifyfat) >= 2 && Convert.ToDouble(modifyfat) <= 20)
                                            {
                                                torangets = "20";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 2 && Convert.ToDouble(modifyfat) <= 40)
                                            {
                                                torangets = "20";
                                            }

                                            if (Convert.ToDouble(modifyfat) >= 9.5 && Convert.ToDouble(modifyfat) <= 9.9)
                                            {
                                                torangets = "9.9";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 10 && Convert.ToDouble(modifyfat) <= 10.5)
                                            {
                                                torangets = "10.5";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 10.6 && Convert.ToDouble(modifyfat) <= 11)
                                            {
                                                torangets = "11";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 11.1 && Convert.ToDouble(modifyfat) <= 11.5)
                                            {
                                                torangets = "11.5";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 11.6 && Convert.ToDouble(modifyfat) <= 11.9)
                                            {
                                                torangets = "11.9";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 12 && Convert.ToDouble(modifyfat) <= 12.2)
                                            {
                                                torangets = "12.2";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 12.3 && Convert.ToDouble(modifyfat) <= 12.5)
                                            {
                                                torangets = "12.5";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 12.6 && Convert.ToDouble(modifyfat) <= 20)
                                            {
                                                torangets = "20";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 20 && Convert.ToDouble(modifyfat) <= 20.5)
                                            {
                                                torangets = "20.5";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 9.5 && Convert.ToDouble(modifyfat) <= 9.9)
                                            {
                                                fromrangets = "9.5";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 10 && Convert.ToDouble(modifyfat) <= 10.5)
                                            {
                                                fromrangets = "10";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 10.6 && Convert.ToDouble(modifyfat) <= 11)
                                            {
                                                fromrangets = "10.6";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 11.1 && Convert.ToDouble(modifyfat) <= 11.5)
                                            {
                                                fromrangets = "11.1";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 11.6 && Convert.ToDouble(modifyfat) <= 11.9)
                                            {
                                                fromrangets = "11.6";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 12 && Convert.ToDouble(modifyfat) <= 12.2)
                                            {
                                                fromrangets = "12";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 12.3 && Convert.ToDouble(modifyfat) <= 12.5)
                                            {
                                                fromrangets = "12.3";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 12.6 && Convert.ToDouble(modifyfat) <= 20)
                                            {
                                                fromrangets = "12.6";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 20 && Convert.ToDouble(modifyfat) <= 20.5)
                                            {
                                                fromrangets = "20";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 2 && Convert.ToDouble(modifyfat) <= 20)
                                            {
                                                fromrangets = "2";
                                            }
                                            if (Convert.ToDouble(modifyfat) >= 2 && Convert.ToDouble(modifyfat) <= 40)
                                            {
                                                fromrangets = "2";
                                            }
                                            foreach (DataRow drRs in dtfatsnfrange.Select("From_RangeValue = '" + fromrangets + "' AND To_RangeValue = '" + torangets + "'"))
                                            {
                                                string Ratee = drRs["Rate"].ToString();
                                                newrow["ModifyTsRate"] = Ratee;
                                                double tsrate1 = Convert.ToDouble(Ratee);
                                                double tsval1 = Convert.ToDouble(modifyfat);
                                                double tsamt1 = tsrate1 * tsval1;
                                                double totalmilkval1 = Math.Round(tsamt1 * Convert.ToDouble(modifykg), 2);
                                                newrow["ModifyAmount"] = totalmilkval1.ToString();
                                                double diffamt = Math.Round(totalmilkval1 - totalmilkval, 2);
                                                newrow["DiffAmt"] = diffamt.ToString();
                                            }
                                            Report.Rows.Add(newrow);
                                        }
                                    }
                                }
                                if (type == "SNF")
                                {
                                    if (dtagentmilkdetails.Rows.Count > 0)
                                    {
                                        int i = 1;
                                        foreach (DataRow drsale in dtagentmilkdetails.Select("snf >= '" + fromrange + "' AND snf <= '" + torange + "' AND agentid = '" + agent + "'"))
                                        {
                                            i++;
                                            DataRow newrow = Report.NewRow();
                                            string plantcode = drsale["plant_code"].ToString();
                                            string plantname = drsale["Plant_Name"].ToString();
                                            string agentid = drsale["agentid"].ToString();
                                            string Date = drsale["Date"].ToString();
                                            string milkkg = drsale["milkkg"].ToString();
                                            string fat = drsale["fat"].ToString();
                                            string snf = drsale["snf"].ToString();
                                            string modifyfat = drsale["modifyfat"].ToString();
                                            string modifysnf = drsale["modifysnf"].ToString();
                                            string modifykg = drsale["modifykg"].ToString();
                                            string difffat = drsale["difffat"].ToString();
                                            string diffsnf = drsale["diffsnf"].ToString();

                                            string ts = drsale["Ts"].ToString();
                                            double tsrate = Convert.ToDouble(Rate);
                                            double tsval = Convert.ToDouble(ts);
                                            double tsamt = tsrate * tsval;
                                            double totalmilkval = Math.Round(tsamt * Convert.ToDouble(milkkg), 2);
                                            newrow["MilkAmount"] = totalmilkval.ToString();
                                            string ts2 = drsale["Ts2"].ToString();


                                            newrow["Ts"] = ts;
                                            newrow["Plant_Name"] = plantname;
                                            newrow["AgentId"] = agentid;
                                            newrow["Date"] = Date;
                                            newrow["Milkqty"] = milkkg;
                                            newrow["Fat"] = fat;
                                            newrow["Snf"] = snf;
                                            newrow["ModifyFat"] = modifyfat;
                                            newrow["Modifysnf"] = modifysnf;
                                            newrow["ModifyMilkqty"] = modifykg;
                                            newrow["Difffat"] = difffat;
                                            newrow["Diffsnf"] = diffsnf;
                                            newrow["TsRate"] = Rate;
                                            string fromrangets = "";
                                            string torangets = "";

                                            if (plantcode == "159")
                                            {
                                                if (Convert.ToDouble(modifysnf) >= 7 && Convert.ToDouble(modifysnf) <= 7)
                                                {
                                                    torangets = "7";
                                                    fromrangets = "7";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.1 && Convert.ToDouble(modifysnf) <= 7.1)
                                                {
                                                    torangets = "7.1";
                                                    fromrangets = "7.1";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.2 && Convert.ToDouble(modifysnf) <= 7.2)
                                                {
                                                    torangets = "7.2";
                                                    fromrangets = "7.2";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.3 && Convert.ToDouble(modifysnf) <= 7.3)
                                                {
                                                    torangets = "7.3";
                                                    fromrangets = "7.3";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.4 && Convert.ToDouble(modifysnf) <= 7.4)
                                                {
                                                    torangets = "7.4";
                                                    fromrangets = "7.4";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.4 && Convert.ToDouble(modifysnf) <= 7.4)
                                                {
                                                    torangets = "7.4";
                                                    fromrangets = "7.4";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.5 && Convert.ToDouble(modifysnf) <= 7.5)
                                                {
                                                    torangets = "7.5";
                                                    fromrangets = "7.5";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.6 && Convert.ToDouble(modifysnf) <= 7.6)
                                                {
                                                    torangets = "7.6";
                                                    fromrangets = "7.6";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.7 && Convert.ToDouble(modifysnf) <= 7.7)
                                                {
                                                    torangets = "7.7";
                                                    fromrangets = "7.7";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.8 && Convert.ToDouble(modifysnf) <= 7.8)
                                                {
                                                    torangets = "7.8";
                                                    fromrangets = "7.8";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 7.9 && Convert.ToDouble(modifysnf) <= 7.9)
                                                {
                                                    torangets = "7.9";
                                                    fromrangets = "7.9";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 8 && Convert.ToDouble(modifysnf) <= 8.2)
                                                {
                                                    torangets = "8.2";
                                                    fromrangets = "8";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 8.3 && Convert.ToDouble(modifysnf) <= 8.3)
                                                {
                                                    torangets = "8.3";
                                                    fromrangets = "8.3";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 8.4 && Convert.ToDouble(modifysnf) <= 8.4)
                                                {
                                                    torangets = "8.4";
                                                    fromrangets = "8.4";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 8.5 && Convert.ToDouble(modifysnf) <= 8.9)
                                                {
                                                    torangets = "8.9";
                                                    fromrangets = "8.5";
                                                }
                                            }

                                            else
                                            {


                                                if (Convert.ToDouble(modifysnf) >= 2 && Convert.ToDouble(modifysnf) <= 20)
                                                {
                                                    torangets = "20";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 2 && Convert.ToDouble(modifysnf) <= 40)
                                                {
                                                    torangets = "20";
                                                }

                                                if (Convert.ToDouble(modifysnf) >= 9.5 && Convert.ToDouble(modifysnf) <= 9.9)
                                                {
                                                    torangets = "9.9";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 10 && Convert.ToDouble(modifysnf) <= 10.5)
                                                {
                                                    torangets = "10.5";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 10.6 && Convert.ToDouble(modifysnf) <= 11)
                                                {
                                                    torangets = "11";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 11.1 && Convert.ToDouble(modifysnf) <= 11.5)
                                                {
                                                    torangets = "11.5";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 11.6 && Convert.ToDouble(modifysnf) <= 11.9)
                                                {
                                                    torangets = "11.9";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 12 && Convert.ToDouble(modifysnf) <= 12.2)
                                                {
                                                    torangets = "12.2";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 12.3 && Convert.ToDouble(modifysnf) <= 12.5)
                                                {
                                                    torangets = "12.5";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 12.6 && Convert.ToDouble(modifysnf) <= 20)
                                                {
                                                    torangets = "20";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 20 && Convert.ToDouble(modifysnf) <= 20.5)
                                                {
                                                    torangets = "20.5";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 9.5 && Convert.ToDouble(modifysnf) <= 9.9)
                                                {
                                                    fromrangets = "9.5";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 10 && Convert.ToDouble(modifysnf) <= 10.5)
                                                {
                                                    fromrangets = "10";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 10.6 && Convert.ToDouble(modifysnf) <= 11)
                                                {
                                                    fromrangets = "10.6";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 11.1 && Convert.ToDouble(modifysnf) <= 11.5)
                                                {
                                                    fromrangets = "11.1";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 11.6 && Convert.ToDouble(modifysnf) <= 11.9)
                                                {
                                                    fromrangets = "11.6";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 12 && Convert.ToDouble(modifysnf) <= 12.2)
                                                {
                                                    fromrangets = "12";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 12.3 && Convert.ToDouble(modifysnf) <= 12.5)
                                                {
                                                    fromrangets = "12.3";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 12.6 && Convert.ToDouble(modifysnf) <= 20)
                                                {
                                                    fromrangets = "12.6";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 20 && Convert.ToDouble(modifysnf) <= 20.5)
                                                {
                                                    fromrangets = "20";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 2 && Convert.ToDouble(modifysnf) <= 20)
                                                {
                                                    fromrangets = "2";
                                                }
                                                if (Convert.ToDouble(modifysnf) >= 2 && Convert.ToDouble(modifysnf) <= 40)
                                                {
                                                    fromrangets = "2";
                                                }
                                            }
                                            foreach (DataRow drRs in dtfatsnfrange.Select("From_RangeValue = '" + fromrangets + "' AND To_RangeValue = '" + torangets + "'"))
                                            {
                                                string Ratee = drRs["Rate"].ToString();
                                                newrow["ModifyTsRate"] = Ratee;
                                                double tsrate1 = Convert.ToDouble(Ratee);
                                                double tsval1 = Convert.ToDouble(ts2);
                                                double tsamt1 = tsrate1 * tsval1;
                                                double totalmilkval1 = Math.Round(tsamt1 * Convert.ToDouble(modifykg), 2);
                                                newrow["ModifyAmount"] = totalmilkval1.ToString();
                                                double diffamt = Math.Round(totalmilkval1 - totalmilkval, 2);
                                                newrow["DiffAmt"] = diffamt.ToString();
                                            }
                                            Report.Rows.Add(newrow);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                DataRow newTotal = Report.NewRow();
                newTotal["Date"] = "Total";
                double val = 0.0;
                foreach (DataColumn dc in Report.Columns)
                {
                    if (dc.DataType == typeof(Double))
                    {
                        val = 0.0;
                        double.TryParse(Report.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out val);
                        if (val == 0)
                        {
                        }
                        else
                        {
                            newTotal[dc.ToString()] = val;
                        }
                    }
                }
                Report.Rows.Add(newTotal);
                Session["xportdata"] = Report;
                Session["filename"] = "Fat Snf Modification Details - " + ddlplant.SelectedItem.Text + "";
                grdReports.DataSource = Report;
                grdReports.DataBind();
                hidepanel.Visible = true;
            }
            else
            {
                cmd = new SqlCommand("SELECT Plant_Master.Plant_Name, modifydata.agentid, CONVERT(VARCHAR(10), modifydata.doe, 101) As Date,  modifydata.milkkg, modifydata.fat, modifydata.snf, modifydata.modifykg, modifydata.modifyfat, modifydata.modifysnf, ROUND(modifydata.modifyfat - modifydata.fat, 1) AS difffat,  ROUND(modifydata.modifysnf - modifydata.snf, 1) AS diffsnf, modifydata.plant_code  FROM modifydata INNER JOIN  Plant_Master ON  Plant_Master.Plant_Code =modifydata.plant_code  WHERE (modifydata.plant_code = @plantcode) AND (modifydata.doe BETWEEN @d1 AND @d2) ORDER BY modifydata.agentid, modifydata.doe");
                cmd.Parameters.Add("@d1", GetLowDate(fromdate));
                cmd.Parameters.Add("@d2", GetHighDate(todate));
                cmd.Parameters.Add("@plantcode", Plant_Code);
                DataTable dtsuperwise1 = vdm.SelectQuery(cmd).Tables[0];
                if (dtsuperwise1.Rows.Count > 0)
                {
                    grdReports.DataSource = dtsuperwise1;
                    grdReports.DataBind();
                    hidepanel.Visible = true;
                }
                else
                {
                    lblmsg.Text = "No data found";
                    hidepanel.Visible = false;
                }
            }
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
            GridViewGroup First = new GridViewGroup(grdReports, null, "Plant_Name");
            GridViewGroup second = new GridViewGroup(grdReports, First, "agentid");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}