using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class dailyreport : System.Web.UI.Page
{
    public static SqlDataReader dr = null;
    public static string ccode;
    public static string plantcode;
    public static int rid = 0;
    DateTime dt = new DateTime();
    BLLuser Bllusers = new BLLuser();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DateTime dt0 = new DateTime();
    DateTime dt2 = new DateTime();
    string checkdate;
    string checkdate1;
    string d1;
    string d2;
    int columncount;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dt = System.DateTime.Now;
                txt_FromDate.Text = dt.ToString("dd/MM/yyyy");
                lbldis.Visible = false;
                LoadPlantcode();
            }
        }
        else
        {
            plantcode = ddl_Plantname.SelectedItem.Value;
            ccode = Session["Company_code"].ToString();
            lbldis.Visible = false;
        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantname.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.ToString();
        }
    }

    protected void ddltype_selectedindexchanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Value == "1")
        {
            td1.Visible = false;
            td2.Visible = false;
        }
        else
        {
            td1.Visible = true;
            td2.Visible = true;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            getgrid();
        }
        catch
        {
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
    public void getgrid()
    {

        try
        {
            dt0 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            d1 = dt0.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");

            DateTime lowdate = GetLowDate(Convert.ToDateTime(d1));
            DateTime highdate = GetHighDate(Convert.ToDateTime(d2));

            DateTime ylowdate = lowdate.AddDays(-1);
            DateTime yhighdate = highdate.AddDays(-1);

            SqlConnection con = new SqlConnection(connStr);
            string strAM, strPM, strpam, strppm;
            DataTable Report = new DataTable();
            DataTable Sample = new DataTable();
            DataTable Reportpm = new DataTable();
            DataTable Reportall = new DataTable();
            string PLANTC = ddl_Plantname.SelectedItem.Text;
            string[] words = PLANTC.Split('_');
            string plantcode = words[0];
            //CC WISE
            if (ddltype.SelectedItem.Value == "0" || ddltype.SelectedItem.Value == "2")
            {
                Sample.Columns.Add("SAMP&DIP MILK");

                Report.Columns.Add("Route NAME");
                Report.Columns.Add("AM MILK").DataType = typeof(double);
                Report.Columns.Add("(+/-)").DataType = typeof(double);

                //Report.Columns.Add("KG Fat").DataType = typeof(double);
                //Report.Columns.Add("Fat(+/-)").DataType = typeof(double);

                //Report.Columns.Add("KG Snf").DataType = typeof(double);
                //Report.Columns.Add("Snf(+/-)").DataType = typeof(double);

                Report.Columns.Add("D S Time");
                Report.Columns.Add("D E Time");
                Report.Columns.Add("MBRT");
                Report.Columns.Add("ACIDITY");
                Report.Columns.Add("AM KMs");
                Report.Columns.Add("AM TP Cost");


                Reportpm.Columns.Add("Route NAME");
                Reportpm.Columns.Add("AM MILK").DataType = typeof(double);
                Reportpm.Columns.Add("(+/-)").DataType = typeof(double);
                Reportpm.Columns.Add("DSTime");
                Reportpm.Columns.Add("DETime");
                Reportpm.Columns.Add("MBRT");
                Reportpm.Columns.Add("ACIDITY");
                Reportpm.Columns.Add("PM KMs");
                Reportpm.Columns.Add("PM TP Cost");


                Reportall.Columns.Add("Route NAME");
                Reportall.Columns.Add("MILK").DataType = typeof(double);
                Reportall.Columns.Add("(+/-)").DataType = typeof(double);
                Reportall.Columns.Add("TP Perday");
                Reportall.Columns.Add("TP Cost");
                // route details
                string route = "Select Route_ID,Route_Name, PLANT_CODE from Route_Master WHERE Company_code='" + ccode + "'  AND PLANT_CODE='" + plantcode + "' ORDER BY Route_ID  ";
                DataTable dtr = new DataTable();
                SqlDataAdapter dar = new SqlDataAdapter(route, con);
                dar.Fill(dtr);


                strAM = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code, Route_id, SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE (Plant_Code = '" + plantcode + "') AND (Prdate BETWEEN '" + lowdate + "' AND '" + highdate + "') AND (Sessions = 'AM')  GROUP BY Plant_Code, Route_id, Prdate, Sessions ORDER BY Prdate, Route_id";
                DataTable dtam = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(strAM, con);
                da1.Fill(dtam);

                strPM = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code, Route_id, SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE (Plant_Code = '" + plantcode + "') AND (Prdate BETWEEN '" + lowdate + "' AND '" + highdate + "') AND (Sessions = 'PM')  GROUP BY Plant_Code, Route_id, Prdate, Sessions ORDER BY Prdate, Route_id";
                DataTable dtpm = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(strPM, con);
                da.Fill(dtpm);


                string ysterdayam = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code, Route_id, SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE (Plant_Code = '" + plantcode + "') AND (Prdate BETWEEN '" + ylowdate + "' AND '" + yhighdate + "') AND (Sessions = 'AM')  GROUP BY Plant_Code, Route_id, Prdate, Sessions ORDER BY Prdate, Route_id";
                DataTable dtyyam = new DataTable();
                SqlDataAdapter dayy1 = new SqlDataAdapter(ysterdayam, con);
                dayy1.Fill(dtyyam);
                string ysterdaypm = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code, Route_id, SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE (Plant_Code = '" + plantcode + "') AND (Prdate BETWEEN '" + ylowdate + "' AND '" + yhighdate + "') AND (Sessions = 'PM')  GROUP BY Plant_Code, Route_id, Prdate, Sessions ORDER BY Prdate, Route_id";
                DataTable dtyypm = new DataTable();
                SqlDataAdapter dayy2 = new SqlDataAdapter(ysterdaypm, con);
                dayy2.Fill(dtyypm);


                string strall = "SELECT  SUM(Milk_ltr) AS MILKLTR, Plant_Code, Route_id, SUM(Milk_kg) AS KG FROM   Procurementimport WHERE (Plant_Code = '" + plantcode + "') AND (Prdate BETWEEN '" + lowdate + "' AND '" + highdate + "') GROUP BY Plant_Code, Route_id   ORDER BY  Route_id";
                DataTable dtall = new DataTable();
                SqlDataAdapter daall = new SqlDataAdapter(strall, con);
                daall.Fill(dtall);

                string stryall = "SELECT  SUM(Milk_ltr) AS MILKLTR, Plant_Code, Route_id, SUM(Milk_kg) AS KG FROM   Procurementimport WHERE (Plant_Code = '" + plantcode + "') AND (Prdate BETWEEN '" + ylowdate + "' AND '" + yhighdate + "')  GROUP BY Plant_Code, Route_id ORDER BY  Route_id";
                DataTable dtyall = new DataTable();
                SqlDataAdapter dayall = new SqlDataAdapter(stryall, con);
                dayall.Fill(dtyall);


                string otherdtls = "SELECT Tid, VehicleSettime, VehicleInttime, Plant_code, Route_id, MBRT, Date, Session, acidity  FROM  RouteTimeMaintain  WHERE (Plant_Code = '" + plantcode + "') AND (Date BETWEEN '" + lowdate + "' AND '" + highdate + "')";
                DataTable dtothers = new DataTable();
                SqlDataAdapter daothers = new SqlDataAdapter(otherdtls, con);
                daothers.Fill(dtothers);

                if (dtr.Rows.Count > 0)
                {
                   
                    foreach (DataRow dr in dtr.Rows)
                    {
                        DataRow newrow = Report.NewRow();
                        string routeid = dr["Route_ID"].ToString();
                        string routename = dr["Route_Name"].ToString();
                        newrow["Route NAME"] = routename;
                        
                        string plantScode = dr["PLANT_CODE"].ToString();
                        foreach (DataRow dram in dtam.Select("Route_id='" + routeid + "'"))
                        {
                            double yesterdayammilk = 0;
                            foreach (DataRow dryam in dtyyam.Select("Route_id='" + routeid + "'"))
                            {
                                double.TryParse(dryam["MILKLTR"].ToString(), out yesterdayammilk);
                            }

                            foreach (DataRow dro in dtothers.Select("Route_id='" + routeid + "' AND Session='AM'"))
                            {
                                newrow["D S Time"] = dro["VehicleSettime"].ToString();
                                newrow["D E Time"] = dro["VehicleInttime"].ToString();
                                newrow["MBRT"] = dro["MBRT"].ToString();
                                newrow["ACIDITY"] = dro["acidity"].ToString();
                            }
                            double ammilk = 0;
                            double.TryParse(dram["MILKLTR"].ToString(), out ammilk);
                            double diffmilk = ammilk - yesterdayammilk;
                            newrow["AM MILK"] = ammilk;
                            newrow["(+/-)"] = Math.Round(diffmilk, 0);
                           
                            newrow["AM KMs"] = "";
                            newrow["AM TP Cost"] = "";
                            Report.Rows.Add(newrow);
                        }
                        DataRow newrowPM = Reportpm.NewRow();
                        newrowPM["Route NAME"] = routename;
                        foreach (DataRow drpm in dtpm.Select("Route_id='" + routeid + "'"))
                        {
                            double yesterdaypmmilk = 0;
                            
                            foreach (DataRow drypm in dtyyam.Select("Route_id='" + routeid + "'"))
                            {
                                double.TryParse(drypm["MILKLTR"].ToString(), out yesterdaypmmilk);
                            }
                            foreach (DataRow dro in dtothers.Select("Route_id='" + routeid + "' AND Session='PM'"))
                            {
                                newrowPM["DSTime"] = dro["VehicleSettime"].ToString();
                                newrowPM["DETime"] = dro["VehicleInttime"].ToString();
                                newrowPM["MBRT"] = dro["MBRT"].ToString();
                                newrowPM["ACIDITY"] = dro["acidity"].ToString();
                            }
                            double pmmilk = 0;
                            double.TryParse(drpm["MILKLTR"].ToString(), out pmmilk);
                            double diffmilk = pmmilk - yesterdaypmmilk;
                            newrowPM["AM MILK"] = pmmilk;
                            newrowPM["(+/-)"] = diffmilk;
                            newrowPM["PM KMs"] = "";
                            newrowPM["PM TP Cost"] = "";
                            Reportpm.Rows.Add(newrowPM);
                        }


                        DataRow newall = Reportall.NewRow();
                        newall["Route NAME"] = routename;
                        foreach (DataRow drall in dtall.Select("Route_id='" + routeid + "'"))
                        {
                            double yesterdayallmilk = 0;

                            foreach (DataRow dryall in dtyall.Select("Route_id='" + routeid + "'"))
                            {
                                double.TryParse(dryall["MILKLTR"].ToString(), out yesterdayallmilk);
                            }
                            double milk = 0;
                            double.TryParse(drall["MILKLTR"].ToString(), out milk);
                            double diffmilk = milk - yesterdayallmilk;
                            newall["MILK"] = milk;
                            newall["(+/-)"] = Math.Round(diffmilk, 0);
                            newall["TP Perday"] = "9:30";
                            newall["TP Cost"] = "11:00";
                            Reportall.Rows.Add(newall);
                        }
                    }


                    DataRow newTotal = Report.NewRow();
                    newTotal["Route NAME"] = "Total";
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

                    DataRow newTotalpm = Reportpm.NewRow();
                    newTotalpm["Route NAME"] = "Total";
                    double valpm = 0.0;
                    foreach (DataColumn dc in Reportpm.Columns)
                    {
                        if (dc.DataType == typeof(Double))
                        {
                            valpm = 0.0;
                            double.TryParse(Reportpm.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out valpm);
                            if (valpm == 0.0)
                            {
                            }
                            else
                            {
                                newTotalpm[dc.ToString()] = valpm;
                            }
                        }
                    }
                    Reportpm.Rows.Add(newTotalpm);


                    DataRow newTotalall = Reportall.NewRow();
                    newTotalall["Route NAME"] = "Total";
                    double valall = 0.0;
                    foreach (DataColumn dc in Reportall.Columns)
                    {
                        if (dc.DataType == typeof(Double))
                        {
                            valall = 0.0;
                            double.TryParse(Reportall.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out valall);
                            if (valall == 0.0)
                            {
                            }
                            else
                            {
                                newTotalall[dc.ToString()] = valall;
                            }
                        }
                    }
                    Reportall.Rows.Add(newTotalall);
                }

            }
            // PLANT WISE
            else
            {
                Sample.Columns.Add("SAMP&DIP MILK");

                Report.Columns.Add("CC NAME");
                Report.Columns.Add("AM MILK").DataType = typeof(double);
                Report.Columns.Add("(+/-)").DataType = typeof(double);
                Report.Columns.Add("D S Time");
                Report.Columns.Add("D E Time");
                Report.Columns.Add("MBRT");
                Report.Columns.Add("ACIDITY");
                Report.Columns.Add("AM KMs");
                Report.Columns.Add("AM TP Cost");


                Reportpm.Columns.Add("CC NAME");
                Reportpm.Columns.Add("AM MILK").DataType = typeof(double);
                Reportpm.Columns.Add("(+/-)").DataType = typeof(double);
                Reportpm.Columns.Add("DSTime");
                Reportpm.Columns.Add("DETime");
                Reportpm.Columns.Add("MBRT");
                Reportpm.Columns.Add("ACIDITY");
                Reportpm.Columns.Add("PM KMs");
                Reportpm.Columns.Add("PM TP Cost");


                Reportall.Columns.Add("CC NAME");
                Reportall.Columns.Add("MILK").DataType = typeof(double);
                Reportall.Columns.Add("(+/-)").DataType = typeof(double);
                Reportall.Columns.Add("TP Perday");
                Reportall.Columns.Add("TP Cost");
                // Plant details
                string route = "Select Plant_Code, Plant_Name from  Plant_Master WHERE Company_Code='" + ccode + "' ORDER BY Plant_Code  ";
                DataTable dtr = new DataTable();
                SqlDataAdapter dar = new SqlDataAdapter(route, con);
                dar.Fill(dtr);


                strAM = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code, SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE  (Prdate BETWEEN '" + lowdate + "' AND '" + highdate + "') AND (Sessions = 'AM')  GROUP BY Plant_Code, Prdate, Sessions ORDER BY Prdate,Plant_Code ";
                DataTable dtam = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(strAM, con);
                da1.Fill(dtam);

                strPM = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code, SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE  (Prdate BETWEEN '" + lowdate + "' AND '" + highdate + "') AND (Sessions = 'PM')  GROUP BY Plant_Code,  Prdate, Sessions ORDER BY Prdate, Plant_Code";
                DataTable dtpm = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(strPM, con);
                da.Fill(dtpm);


                string ysterdayam = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code,  SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE  (Prdate BETWEEN '" + ylowdate + "' AND '" + yhighdate + "') AND (Sessions = 'AM')  GROUP BY Plant_Code,  Prdate, Sessions ORDER BY Prdate, Plant_Code";
                DataTable dtyyam = new DataTable();
                SqlDataAdapter dayy1 = new SqlDataAdapter(ysterdayam, con);
                dayy1.Fill(dtyyam);
                string ysterdaypm = "SELECT Sessions, SUM(Milk_ltr) AS MILKLTR, Plant_Code,  SUM(Milk_kg) AS KG, Prdate FROM   Procurementimport WHERE  (Prdate BETWEEN '" + ylowdate + "' AND '" + yhighdate + "') AND (Sessions = 'PM')  GROUP BY Plant_Code,  Prdate, Sessions ORDER BY Prdate, Plant_Code";
                DataTable dtyypm = new DataTable();
                SqlDataAdapter dayy2 = new SqlDataAdapter(ysterdaypm, con);
                dayy2.Fill(dtyypm);


                string strall = "SELECT  SUM(Milk_ltr) AS MILKLTR, Plant_Code, SUM(Milk_kg) AS KG FROM   Procurementimport WHERE (Prdate BETWEEN '" + lowdate + "' AND '" + highdate + "') GROUP BY Plant_Code ORDER BY  Plant_Code";
                DataTable dtall = new DataTable();
                SqlDataAdapter daall = new SqlDataAdapter(strall, con);
                daall.Fill(dtall);

                string stryall = "SELECT  SUM(Milk_ltr) AS MILKLTR, Plant_Code,  SUM(Milk_kg) AS KG FROM   Procurementimport WHERE  (Prdate BETWEEN '" + ylowdate + "' AND '" + yhighdate + "')  GROUP BY Plant_Code ORDER BY  Plant_Code";
                DataTable dtyall = new DataTable();
                SqlDataAdapter dayall = new SqlDataAdapter(stryall, con);
                dayall.Fill(dtyall);


                string otherdtls = "SELECT Tid, VehicleSettime, VehicleInttime, Plant_code, Route_id, MBRT, Date, Session, acidity  FROM  RouteTimeMaintain  WHERE  (Date BETWEEN '" + lowdate + "' AND '" + highdate + "')";
                DataTable dtothers = new DataTable();
                SqlDataAdapter daothers = new SqlDataAdapter(otherdtls, con);
                daothers.Fill(dtothers);

                if (dtr.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtr.Rows)
                    {
                        DataRow newrow = Report.NewRow();
                        string routeid = dr["Plant_Code"].ToString();
                        string routename = dr["Plant_Name"].ToString();
                        newrow["CC NAME"] = routename;

                        string plantScode = dr["Plant_Code"].ToString();
                        foreach (DataRow dram in dtam.Select("Plant_code='" + routeid + "'"))
                        {
                            double yesterdayammilk = 0;
                            foreach (DataRow dryam in dtyyam.Select("Plant_code='" + routeid + "'"))
                            {
                                double.TryParse(dryam["MILKLTR"].ToString(), out yesterdayammilk);
                            }

                            foreach (DataRow dro in dtothers.Select("Plant_code='" + routeid + "' AND Session='AM'"))
                            {
                                newrow["D S Time"] = dro["VehicleSettime"].ToString();
                                newrow["D E Time"] = dro["VehicleInttime"].ToString();
                                newrow["MBRT"] = dro["MBRT"].ToString();
                                newrow["ACIDITY"] = dro["acidity"].ToString();
                            }
                            double ammilk = 0;
                            double.TryParse(dram["MILKLTR"].ToString(), out ammilk);
                            double diffmilk = ammilk - yesterdayammilk;
                            newrow["AM MILK"] = ammilk;
                            newrow["(+/-)"] = Math.Round(diffmilk, 0);

                            newrow["AM KMs"] = "";
                            newrow["AM TP Cost"] = "";
                            Report.Rows.Add(newrow);
                        }
                        DataRow newrowPM = Reportpm.NewRow();
                        newrowPM["CC NAME"] = routename;
                        foreach (DataRow drpm in dtpm.Select("Plant_code='" + routeid + "'"))
                        {
                            double yesterdaypmmilk = 0;

                            foreach (DataRow drypm in dtyyam.Select("Plant_code='" + routeid + "'"))
                            {
                                double.TryParse(drypm["MILKLTR"].ToString(), out yesterdaypmmilk);
                            }
                            foreach (DataRow dro in dtothers.Select("Plant_code='" + routeid + "' AND Session='PM'"))
                            {
                                newrowPM["DSTime"] = dro["VehicleSettime"].ToString();
                                newrowPM["DETime"] = dro["VehicleInttime"].ToString();
                                newrowPM["MBRT"] = dro["MBRT"].ToString();
                                newrowPM["ACIDITY"] = dro["acidity"].ToString();
                            }
                            double pmmilk = 0;
                            double.TryParse(drpm["MILKLTR"].ToString(), out pmmilk);
                            double diffmilk = pmmilk - yesterdaypmmilk;
                            newrowPM["AM MILK"] = pmmilk;
                            newrowPM["(+/-)"] = diffmilk;
                            newrowPM["PM KMs"] = "";
                            newrowPM["PM TP Cost"] = "";
                            Reportpm.Rows.Add(newrowPM);
                        }


                        DataRow newall = Reportall.NewRow();
                        newall["CC NAME"] = routename;
                        foreach (DataRow drall in dtall.Select("Plant_code='" + routeid + "'"))
                        {
                            double yesterdayallmilk = 0;

                            foreach (DataRow dryall in dtyall.Select("Plant_code='" + routeid + "'"))
                            {
                                double.TryParse(dryall["MILKLTR"].ToString(), out yesterdayallmilk);
                            }
                            double milk = 0;
                            double.TryParse(drall["MILKLTR"].ToString(), out milk);
                            double diffmilk = milk - yesterdayallmilk;
                            newall["MILK"] = milk;
                            newall["(+/-)"] = Math.Round(diffmilk, 0);
                            newall["TP Perday"] = "";
                            newall["TP Cost"] = "";
                            Reportall.Rows.Add(newall);
                        }
                    }


                    DataRow newTotal = Report.NewRow();
                    newTotal["CC NAME"] = "Total";
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

                    DataRow newTotalpm = Reportpm.NewRow();
                    newTotalpm["CC NAME"] = "Total";
                    double valpm = 0.0;
                    foreach (DataColumn dc in Reportpm.Columns)
                    {
                        if (dc.DataType == typeof(Double))
                        {
                            valpm = 0.0;
                            double.TryParse(Reportpm.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out valpm);
                            if (valpm == 0.0)
                            {
                            }
                            else
                            {
                                newTotalpm[dc.ToString()] = valpm;
                            }
                        }
                    }
                    Reportpm.Rows.Add(newTotalpm);


                    DataRow newTotalall = Reportall.NewRow();
                    newTotalall["CC NAME"] = "Total";
                    double valall = 0.0;
                    foreach (DataColumn dc in Reportall.Columns)
                    {
                        if (dc.DataType == typeof(Double))
                        {
                            valall = 0.0;
                            double.TryParse(Reportall.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out valall);
                            if (valall == 0.0)
                            {
                            }
                            else
                            {
                                newTotalall[dc.ToString()] = valall;
                            }
                        }
                    }
                    Reportall.Rows.Add(newTotalall);
                }
            }

            DataRow smprow = Sample.NewRow();
            smprow["SAMP&DIP MILK"] = "1";
            Sample.Rows.Add(smprow);
            

            GridView1.DataSource = Report;
            GridView1.DataBind();

            GridView2.DataSource = Reportpm;
            GridView2.DataBind();

            GridView3.DataSource = Reportall;
            GridView3.DataBind();

            GridView4.DataSource = Sample;
            GridView4.DataBind();
        }
        catch (Exception ex)
        {

            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
        }
    }

    
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_Plantname.Text + "'-" + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                getgrid();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                // string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        catch (Exception ex)
        {

            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
        }

    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[4].Visible = false;
            if (e.Row.Cells.Count > 2)
            {
                if (e.Row.Cells[0].Text == "Total")
                {
                    e.Row.BackColor = System.Drawing.Color.Aquamarine;
                    e.Row.Font.Size = FontUnit.Large;
                    e.Row.Font.Bold = true;
                }
            }
        }
    
    
    
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[4].Visible = false;
            if (e.Row.Cells.Count > 2)
            {
                if (e.Row.Cells[0].Text == "Total")
                {
                    e.Row.BackColor = System.Drawing.Color.Aquamarine;
                    e.Row.Font.Size = FontUnit.Large;
                    e.Row.Font.Bold = true;
                }
            }
        }



    }


    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[4].Visible = false;
            if (e.Row.Cells.Count > 2)
            {
                if (e.Row.Cells[0].Text == "Total")
                {
                    e.Row.BackColor = System.Drawing.Color.Aquamarine;
                    e.Row.Font.Size = FontUnit.Large;
                    e.Row.Font.Bold = true;
                }
            }
        }



    }


    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}