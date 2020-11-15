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
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;


public partial class TransportRpt : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public int rid;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess = new DbHelper();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                }
                //GetprourementIvoiceData();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
              //  pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
            //    managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_Plantcode.SelectedItem.Value;
                
                GetprourementIvoiceData();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }



    }


    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        pcode = ddl_Plantcode.SelectedItem.Value;
        GetprourementIvoiceData();
    }

    private void GetprourementIvoiceData()
    {
        try
        {
            DataTable custdt = new DataTable();
            DataRow custdr = null;
            DataTable resdt = new DataTable();
          
            pcode = ddl_Plantcode.SelectedItem.Value;
          
            cr.Load(Server.MapPath("crpt_TransportReport.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            t1.Text = ccode + "_" + cname;
            t2.Text = ddl_Plantname.SelectedItem.Value;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            //str = "SELECT  t4.TruckId,t4.Tname AS TruckName,CAST(t4.PerSesKm AS DECIMAL(18,2)) AS PerSesKm,CAST(t4.TotTrip AS DECIMAL(18,2)) AS TotalTrip,CAST(t4.TotalDistance AS DECIMAL(18,2)) AS TotalKm,CAST(t4.Lcost AS DECIMAL(18,2)) AS perKmRate,CAST(Amount AS DECIMAL(18,2)) AS TotAmount,CAST(Smltr AS DECIMAL(18,2)) AS QtyLtr,CAST(t4.PerLtrTCost AS DECIMAL(18,2)) AS PerLtrTCost,CAST((t4.Smltr/TotTrip) AS DECIMAL(18,2)) AS PerSesLtrs FROM (SELECT t3.Trk_Id AS TruckId,t3.Smltr AS Smltr,t3.Lcost,t3.TripCount AS TotTrip,(Distance*t3.TripCount) AS TotalDistance,((Distance*t3.TripCount)*t3.Lcost) AS AMOUNT,(((Distance*t3.TripCount)*t3.Lcost)/Smltr) AS PerLtrTCost,Tname,Distance AS PerSesKm FROM (SELECT t2.Truck_Id AS Trk_Id,t2.Smltr AS Smltr,t2.Ltr_Cost AS Lcost,DayCount AS TripCount,t2.Truck_Name AS Tname FROM (SELECT Smltr,Truck_Id,DayCount,Ltr_Cost,Truck_Name FROM ( SELECT Smltr,Truck_Id,ISNULL(DayCount,0) AS DayCount FROM( (SELECT SUM(Milk_Ltr) AS Smltr ,Truck_Id FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' GROUP BY Truck_Id  ) AS pro LEFT JOIN (SELECT COUNT(Session_Id) AS DayCount,Truck_Id AS tid  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' GROUP BY Truck_Id ) AS Trckpre ON pro.Truck_Id=Trckpre.tid )) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + " ') AS VD ON t1.Truck_Id=VD.VDTrkid) AS t2 ) AS t3 LEFT JOIN (SELECT DISTINCT(Truck_id) AS Trkalotid,Distance  FROM TRUCK_ROUTEDISTANCE WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' )AS TAlot ON t3.Trk_Id=TAlot.Trkalotid) AS t4";
            //str = "SELECT pcode,Truck_id,Truck_Name+Truck_Name1 AS TruckName,(Ltr_Cost + Ltr_Cost1) AS KM_Price,OndayTotDistance AS TotDistance,Smltr AS Smltr1,((TransAmountFix * DDay) +TransAmount1) AS TransAMount,DDay,(OndayTotDistance/DDay) AS PerDay_Distance,CAST((Smltr/DDay) AS DECIMAL(18,2)) AS PerDay_Ltr  FROM (SELECT Truck_id,Smltr,OndayTotDistance,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL(fixed,0) AS fixed,ISNULL(Truck_Name,'') AS Truck_Name,CAST((ISNULL(Ltr_Cost,0)) AS DECIMAL(18,2)) AS TransAmountFix FROM (SELECT Truck_id,Smltr,OndayTotDistance FROM (SELECT SUM(CAST((ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2))) AS Smltr ,Truck_Id FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' GROUP BY Truck_Id  ) AS pro LEFT JOIN (SELECT Truck_Id AS tid,SUM(ISNULL(Tdistance,0)) AS OndayTotDistance  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' GROUP BY Truck_Id) AS Trckpre ON pro.Truck_Id=Trckpre.tid) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=1 ) AS VD  ON t1.Truck_id=VD.VDTrkid) AS F1 LEFT JOIN (SELECT Truck_id AS Truck_id1,Smltr AS Smltr1,OndayTotDistance AS OndayTotDistance1,ISNULL(Ltr_Cost,0) AS Ltr_Cost1,ISNULL(fixed,0) AS fixed1,ISNULL(Truck_Name,'') AS Truck_Name1,CAST((ISNULL(Ltr_Cost,0)*OndayTotDistance) AS DECIMAL(18,2)) AS TransAmount1,pcode,DDay FROM (SELECT Truck_id,Smltr,OndayTotDistance,Plant_Code AS pcode,(DDay+1) AS DDay FROM (SELECT SUM(CAST((ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2))) AS Smltr ,Truck_Id,Plant_Code FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' GROUP BY Truck_Id,Plant_Code  ) AS pro  LEFT JOIN  (SELECT Truck_Id AS tid,SUM(ISNULL(Tdistance,0)) AS OndayTotDistance,datediff(day, '" + txt_FromDate.Text + "','" + txt_ToDate.Text + "') as DDay  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' GROUP BY Truck_Id) AS Trckpre ON pro.Truck_Id=Trckpre.tid) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=0 ) AS VD  ON t1.Truck_id=VD.VDTrkid) AS F2 ON F1.Truck_id=F2.Truck_id1 ORDER BY Truck_id";
            //str = "SELECT * FROM (SELECT pcode,Rid,Truck_id,Truck_Name+Truck_Name1 AS TruckName,(OndayTotDistance/DDay) AS PerDay_Distance,(Ltr_Cost + Ltr_Cost1) AS KM_Price,OndayTotDistance AS TotDistance,((TransAmountFix * DDay) +TransAmount1) AS TransAMount,CAST((((TransAmountFix * DDay) +TransAmount1)*0.02) AS DECIMAL(18,2)) AS Tds,CAST((((TransAmountFix * DDay) +TransAmount1)-(((TransAmountFix * DDay) +TransAmount1)*0.02)) AS DECIMAL(18,2)) AS GrossAmount,Smltr AS Smltr1,CAST((((TransAmountFix * DDay) +TransAmount1)/Smltr1) AS DECIMAL(18,2)) AS PerLtrCost,CAST((Smltr/DDay) AS DECIMAL(18,2)) AS PerDay_Ltr,DDay  FROM (SELECT Truck_id,Smltr,OndayTotDistance,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL(fixed,0) AS fixed,ISNULL(Truck_Name,'') AS Truck_Name,CAST((ISNULL(Ltr_Cost,0)) AS DECIMAL(18,2)) AS TransAmountFix,Rid FROM (SELECT Truck_id,Smltr,OndayTotDistance,Rid FROM (SELECT SUM(CAST((ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2))) AS Smltr ,Truck_Id,Route_id AS Rid FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id,Route_id  ) AS pro LEFT JOIN (SELECT Truck_Id AS tid,SUM(ISNULL(Tdistance,0)) AS OndayTotDistance  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON pro.Truck_Id=Trckpre.tid) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=1 ) AS VD  ON t1.Truck_id=VD.VDTrkid) AS F1 LEFT JOIN (SELECT Truck_id AS Truck_id1,Smltr AS Smltr1,OndayTotDistance AS OndayTotDistance1,ISNULL(Ltr_Cost,0) AS Ltr_Cost1,ISNULL(fixed,0) AS fixed1,ISNULL(Truck_Name,'') AS Truck_Name1,CAST((ISNULL(Ltr_Cost,0)*OndayTotDistance) AS DECIMAL(18,2)) AS TransAmount1,pcode,DDay FROM (SELECT Truck_id,Smltr,OndayTotDistance,Plant_Code AS pcode,(DDay+1) AS DDay FROM (SELECT SUM(CAST((ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2))) AS Smltr ,Truck_Id,Plant_Code FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id,Plant_Code,Route_id  ) AS pro  LEFT JOIN  (SELECT Truck_Id AS tid,SUM(ISNULL(Tdistance,0)) AS OndayTotDistance,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON pro.Truck_Id=Trckpre.tid) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=0 ) AS VD  ON t1.Truck_id=VD.VDTrkid) AS F2 ON F1.Truck_id=F2.Truck_id1 ) AS t1 INNER JOIN (SELECT Route_Name,Route_ID FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS t2 ON t1.Rid= t2.Route_ID ORDER BY Truck_id";
            //Work Fine Auto Method str = "SELECT F1.*,F2.Rid,F2.Route_Name  FROM (SELECT TruckId3 AS pcode,TruckId3 AS Truck_id,CONVERT(Nvarchar(30),TruckId3)+'_'+(Truck_Name3+Truck_Name1) AS TruckName,(TotDistance3)/DDay3 AS PerDay_Distance,(KM_Price3+KM_Price1) AS KM_Price,(TotDistance3) AS TotDistance,(TransAMount3+TransAMount1) AS TransAMount,CAST(((TransAMount3+TransAMount1)*0.02) AS DECIMAL(18,2)) AS Tds,((TransAMount3+TransAMount1)-CAST(((TransAMount3+TransAMount1)*0.02) AS DECIMAL(18,2))) AS GrossAmount,Smltr3 AS Smltr1,CAST(((TransAMount3+TransAMount1)/Smltr3) AS DECIMAL(18,2)) AS PerLtrCost,CAST((Smltr3/DDay3) AS DECIMAL(18,2)) AS PerDay_Ltr,DDay3 AS DDay FROM (SELECT TruckId1 AS TruckId3,(Truck_Name1+Truck_Name2) AS Truck_Name3,Smltr1 AS Smltr3,(Ltr_Cost1+Ltr_Cost2) AS KM_Price3,DDay1 AS DDay3,TotDistance1 AS TotDistance3,(TransAMount1+TransAMount2) AS TransAMount3  FROM (SELECT TraTid AS TruckId1,Smltr AS Smltr1,Ltr_Cost AS Ltr_Cost1,Truck_Name AS Truck_Name1,Fixed AS Fixed1,DDay AS DDay1,ISNULL(TotDistance,0) AS TotDistance1,(Ltr_Cost*ISNULL(TotDistance,0)) AS TransAMount1 FROM  (SELECT TraTid,Smltr,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL('KM_'+Truck_Name,'') AS Truck_Name,ISNULL(Fixed,'1') AS Fixed,ISNULL(DDay,0) AS DDay FROM (SELECT TraTid,ISNULL(Smltr,0) AS Smltr,(DDay+1) AS DDay FROM (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Tra LEFT JOIN (SELECT CAST(SUM(ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2)) AS Smltr ,Route_id,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay   FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Route_id) AS Pro ON Tra.TraRid=Pro.Route_id ) AS t1 LEFT JOIN  (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=0 AND PaymentType=0 ) AS VD ON  t1.TraTid=VD.VDTrkid ) AS  t2 LEFT JOIN (SELECT Truck_Id AS trpreid,SUM(ISNULL(Tdistance,0)) AS TotDistance FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON t2.TraTid=Trckpre.trpreid) AS t3 LEFT JOIN  (SELECT TraTid AS TruckId2,Smltr AS Smltr2,Ltr_Cost AS Ltr_Cost2,Truck_Name AS Truck_Name2,Fixed AS Fixed2,DDay AS DDay2,ISNULL(TotDistance,0) AS TotDistance2,(Ltr_Cost*ISNULL(Smltr,0)) AS TransAMount2 FROM (SELECT TraTid,Smltr,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL('Ltr_'+Truck_Name,'') AS Truck_Name,ISNULL(Fixed,'1') AS Fixed,ISNULL(DDay,0) AS DDay FROM (SELECT TraTid,ISNULL(Smltr,0) AS Smltr,(DDay+1) AS DDay FROM  (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Tra LEFT JOIN (SELECT CAST(SUM(ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2)) AS Smltr ,Route_id,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay   FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Route_id) AS Pro ON Tra.TraRid=Pro.Route_id ) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=0 AND PaymentType=1 ) AS VD ON  t1.TraTid=VD.VDTrkid ) AS  t2 LEFT JOIN (SELECT Truck_Id AS trpreid,SUM(ISNULL(Tdistance,0)) AS TotDistance FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON t2.TraTid=Trckpre.trpreid ) AS t4 ON t3.TruckId1=t4.TruckId2 ) AS t5 LEFT JOIN (SELECT TraTid AS TruckId1,Smltr AS Smltr1,Ltr_Cost AS KM_Price1,Truck_Name AS Truck_Name1,Fixed AS Fixed1,DDay AS DDay1,ISNULL(TotDistance,0) AS TotDistance1,(Ltr_Cost*ISNULL(DDay,0)) AS TransAMount1 FROM (SELECT TraTid,Smltr,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL('FIX_'+Truck_Name,'') AS Truck_Name,ISNULL(Fixed,'1') AS Fixed,ISNULL(DDay,0) AS DDay FROM (SELECT TraTid,ISNULL(Smltr,0) AS Smltr,(DDay+1) AS DDay FROM (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Tra LEFT JOIN (SELECT CAST(SUM(ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2)) AS Smltr ,Route_id,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay   FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Route_id) AS Pro ON Tra.TraRid=Pro.Route_id ) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=1 ) AS VD ON  t1.TraTid=VD.VDTrkid ) AS  t2 LEFT JOIN  (SELECT Truck_Id AS trpreid,SUM(ISNULL(Tdistance,0)) AS TotDistance FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON t2.TraTid=Trckpre.trpreid ) AS t6 ON t5.TruckId3=t6.TruckId1 ) AS F1 LEFT JOIN (SELECT Rid,Truck_Id,Route_Name FROM (SELECT Distinct(Route_Id) AS Rid,Truck_Id FROM Truck_RouteAllotment  Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS TRt1 LEFT JOIN (SELECT Route_ID, CONVERT(Nvarchar(100),Route_ID)+'_'+Route_Name  AS  Route_Name FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Rt2 ON TRt1.Rid=Rt2.Route_ID ) AS F2 ON F1.Truck_id=F2.Truck_Id ORDER BY F2.Rid ";//ORDER BY F1.Truck_id 
            str = "SELECT FTruck.*,R2.Route_ID AS Rid,R2.Route_Name FROM " +
"(SELECT f5.Tid AS pcode,TpTid AS Truck_id,CONVERT(Nvarchar(7),TpTid)+'_'+CONVERT(Nvarchar(7),TName) AS TruckName,(TotDistance/f5.Dday) AS PerDay_Distance,f5.Lcost AS KM_Price,TotDistance  AS TotDistance,TransAMount,Tds,GrossAmount,Smltr1,CAST((TransAMount/Smltr1) AS DECIMAL(18,2)) AS PerLtrCost,CAST((Smltr1/Dday) AS DECIMAL(18,2)) AS PerDay_Ltr,Dday AS DDay FROM " +
"(SELECT Truck_Id AS TpTid,SUM(AdminDistance) AS TotDistance,CAST(SUM(ActualAmount) AS DECIMAL(18,2)) AS TransAMount,SUM(TdsPercentage) AS Tds,SUM(GrossAmount) AS GrossAmount,SUM(MilkLtr) AS Smltr1 from Truck_Present where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' And Pdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND Truck_Id>0 Group By Truck_id ) AS Tp " +
"LEFT JOIN " +
"(SELECT f3.Tid3 AS Tid,(f3.Truck_Name3+f4.Truck_Name4) AS TName,(f3.Ltr_Cost3+f4.Ltr_Cost4) AS Lcost,f4.Dday  FROM " +
"(SELECT f1.Tid AS Tid3,(f1.Truck_Name+f2.Truck_Name1) AS Truck_Name3,(f1.Ltr_Cost+f2.Ltr_Cost1) AS Ltr_Cost3  FROM " +
"(SELECT Tid,ISNULL(r1.Truck_Name,'') AS Truck_Name,ISNULL(r1.Ltr_Cost,0) AS Ltr_Cost FROM " +
"(SELECT DISTINCT(Truck_Id) AS Tid FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1 + "' AND '" + d2 + "') AS t1 " +
"LEFT JOIN " +
"(SELECT Truck_id AS VDTrkid,Ltr_Cost,'Fix'+'_'+CONVERT( Nvarchar(8),Truck_Name) AS Truck_Name,fixed,PaymentType FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed='1' ) AS r1 ON t1.Tid=r1.VDTrkid) f1 " +
"LEFT JOIN " +
"(SELECT Tid AS Tid1,ISNULL(r2.Truck_Name,'') AS  Truck_Name1,ISNULL(r2.Ltr_Cost,0) AS Ltr_Cost1 FROM " +
"(SELECT DISTINCT(Truck_Id) AS Tid FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1 + "' AND '" + d2 + "') AS t2 " +
"LEFT JOIN " +
"(SELECT Truck_id AS VDTrkid,Ltr_Cost,'Ltr'+'_'+CONVERT( Nvarchar(8),Truck_Name) AS Truck_Name,fixed,PaymentType FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed<>'1' AND PaymentType='1' ) AS r2 ON t2.Tid=r2.VDTrkid) AS f2 ON f1.Tid=f2.Tid1) AS f3 " +
"LEFT JOIN " +
"(SELECT Tid AS Tid4,ISNULL(r3.Truck_Name,'') AS  Truck_Name4,ISNULL(r3.Ltr_Cost,0) AS Ltr_Cost4,(Dday1+1) AS Dday FROM " +
"(SELECT DISTINCT(Truck_Id) AS Tid,datediff(Day,'" + d1 + "','" + d2 + "') AS Dday1 FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1 + "' AND '" + d2 + "') AS t3 " +
"LEFT JOIN " +
"(SELECT Truck_id AS VDTrkid,Ltr_Cost,'Km'+'_'+CONVERT( Nvarchar(8),Truck_Name) AS Truck_Name,fixed,PaymentType FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed<>'1' AND PaymentType='0' ) AS r3 ON t3.Tid=r3.VDTrkid) AS f4 ON f3.Tid3=f4.Tid4) AS f5 ON Tp.TpTid=f5.Tid ) AS FTruck " +
"INNER JOIN " +
"(SELECT TraTid,TraRid,Route_ID,CONVERT(NVARCHAR(13),Route_Name) AS Route_Name FROM (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Tra LEFT JOIN (SELECT Route_ID, CONVERT(Nvarchar(100),Route_ID)+'_'+Route_Name  AS  Route_Name FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Rt2 ON Tra.TraRid=Rt2.Route_ID ) AS R2 ON FTruck.Truck_id=R2.TraTid ORDER BY R2.TraTid,R2.Route_ID";
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn col = null;
            col = new DataColumn("pcode");
            custdt.Columns.Add(col);
            col = new DataColumn("Truck_id");
            custdt.Columns.Add(col);
            col = new DataColumn("TruckName");
            custdt.Columns.Add(col);
            col = new DataColumn("PerDay_Distance");
            custdt.Columns.Add(col);
            col = new DataColumn("KM_Price");
            custdt.Columns.Add(col);
            col = new DataColumn("TotDistance");
            custdt.Columns.Add(col);
            col = new DataColumn("TransAMount");
            custdt.Columns.Add(col);
            col = new DataColumn("Tds");
            custdt.Columns.Add(col);
            col = new DataColumn("GrossAmount");
            custdt.Columns.Add(col);
            col = new DataColumn("Smltr1");
            custdt.Columns.Add(col);
            col = new DataColumn("PerLtrCost");
            custdt.Columns.Add(col);
            col = new DataColumn("PerDay_Ltr");
            custdt.Columns.Add(col);
            col = new DataColumn("DDay");
            custdt.Columns.Add(col);
            col = new DataColumn("Rid");
            custdt.Columns.Add(col);
            col = new DataColumn("Route_Name");
            custdt.Columns.Add(col);

            //  Single Truck Allotted for multiple Route
            object id1;
            id1 = "0";
            int idd1 = Convert.ToInt32(id1);
            foreach (DataRow dr1 in dt.Rows)
            {
                object id;

                id = dr1[1].ToString();
                int idd = Convert.ToInt32(id);
                custdr = custdt.NewRow();
                int i = 0;


                //if (idd1 == idd)
                //{
                //    custdr[0] = dr1["pcode"].ToString();
                //    custdr[1] = dr1["Truck_id"].ToString();
                //    custdr[2] = dr1["TruckName"].ToString();
                //    custdr[3] = dr1["PerDay_Distance"].ToString();
                //    custdr[4] = dr1["KM_Price"].ToString();
                //    custdr[5] = dr1["TotDistance"].ToString();
                //    custdr[6] = dr1["TransAMount"].ToString();
                //    custdr[7] = dr1["Tds"].ToString();
                //    custdr[8] = dr1["GrossAmount"].ToString();
                //    // dr1["Smltr1"] = "0";

                //    //custdr[9] = dr1["Smltr1"].ToString();
                //    custdr[9] = "0";
                //    custdr[10] = dr1["PerLtrCost"].ToString();
                //    // custdr[11] = dr1["PerDay_Ltr"].ToString();
                //    custdr[11] = "0";
                //    custdr[12] = dr1["DDay"].ToString();
                //    custdr[13] = dr1["Rid"].ToString();
                //    custdr[14] = dr1["Route_Name"].ToString();
                //    custdt.Rows.Add(custdr);
                //    id1 = dr1["Rid"].ToString();
                //    idd1 = Convert.ToInt32(id1);
                //    i++;
                //}
                //else
                //{
                //    custdr[0] = dr1["pcode"].ToString();
                //    custdr[1] = dr1["Truck_id"].ToString();
                //    custdr[2] = dr1["TruckName"].ToString();
                //    custdr[3] = dr1["PerDay_Distance"].ToString();
                //    custdr[4] = dr1["KM_Price"].ToString();
                //    custdr[5] = dr1["TotDistance"].ToString();
                //    custdr[6] = dr1["TransAMount"].ToString();
                //    custdr[7] = dr1["Tds"].ToString();
                //    custdr[8] = dr1["GrossAmount"].ToString();
                //    custdr[9] = dr1["Smltr1"].ToString();
                //    custdr[10] = dr1["PerLtrCost"].ToString();
                //    custdr[11] = dr1["PerDay_Ltr"].ToString();
                //    custdr[12] = dr1["DDay"].ToString();
                //    custdr[13] = dr1["Rid"].ToString();
                //    custdr[14] = dr1["Route_Name"].ToString();
                //    custdt.Rows.Add(custdr);
                //    id1 = dr1["Rid"].ToString();
                //    idd1 = Convert.ToInt32(id1);
                //    i++;
                //}

                if (idd1 == idd)
                {
                    custdr[0] = dr1["pcode"].ToString();
                    custdr[1] = dr1["Truck_id"].ToString();
                    custdr[2] = dr1["TruckName"].ToString();
                    // custdr[2] = " ";
                    custdr[3] = dr1["PerDay_Distance"].ToString();
                   // custdr[3] = dr1["PerDay_Distance"].ToString();
                    custdr[3] = "0";
                   // custdr[4] = dr1["KM_Price"].ToString();
                    custdr[4] = "0";
                   // custdr[5] = dr1["TotDistance"].ToString();
                    custdr[5] = "0";
                   // custdr[6] = dr1["TransAMount"].ToString();
                    custdr[6] = "0";
                   // custdr[7] = dr1["Tds"].ToString();
                    custdr[7] = "0";
                   // custdr[8] = dr1["GrossAmount"].ToString();
                    custdr[8] = "0";
                    // dr1["Smltr1"] = "0";

                   // custdr[9] = dr1["Smltr1"].ToString();
                    custdr[9] = "0";
                   // custdr[10] = dr1["PerLtrCost"].ToString();
                    custdr[10] = "0";
                    // custdr[11] = dr1["PerDay_Ltr"].ToString();
                    custdr[11] = "0";
                    custdr[12] = dr1["DDay"].ToString();
                    custdr[13] = dr1["Rid"].ToString();
                    custdr[14] = dr1["Route_Name"].ToString();
                    custdt.Rows.Add(custdr);
                    id1 = dr1["Truck_id"].ToString();
                    idd1 = Convert.ToInt32(id1);
                    i++;
                }
                else
                {
                    custdr[0] = dr1["pcode"].ToString();
                    custdr[1] = dr1["Truck_id"].ToString();
                    custdr[2] = dr1["TruckName"].ToString();
                    custdr[3] = dr1["PerDay_Distance"].ToString();
                    custdr[4] = dr1["KM_Price"].ToString();
                    custdr[5] = dr1["TotDistance"].ToString();
                    custdr[6] = dr1["TransAMount"].ToString();
                    custdr[7] = dr1["Tds"].ToString();
                    custdr[8] = dr1["GrossAmount"].ToString();
                    custdr[9] = dr1["Smltr1"].ToString();
                    custdr[10] = dr1["PerLtrCost"].ToString();
                    custdr[11] = dr1["PerDay_Ltr"].ToString();
                    custdr[12] = dr1["DDay"].ToString();
                    custdr[13] = dr1["Rid"].ToString();
                    custdr[14] = dr1["Route_Name"].ToString();
                    custdt.Rows.Add(custdr);
                    id1 = dr1["Truck_id"].ToString();
                    idd1 = Convert.ToInt32(id1);
                    i++;
                }
              

            }
            //  Single Route Allotted for multiple Truck 

            DataView dv = custdt.DefaultView;
            dv.Sort = "Rid Asc";
            custdt = dv.ToTable();
            
            DataTable custdt1 = new DataTable();         
            DataColumn col1 = null;
          
            col1 = new DataColumn("pcode");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("Truck_id");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("TruckName");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("PerDay_Distance");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("KM_Price");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("TotDistance");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("TransAMount");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("Tds");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("GrossAmount");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("Smltr1");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("PerLtrCost");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("PerDay_Ltr");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("DDay");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("Rid");
            custdt1.Columns.Add(col1);
            col1 = new DataColumn("Route_Name");
            custdt1.Columns.Add(col1);           
                     
           
            
            object iRd1;
            iRd1 = "0";
            int iRdd1 = Convert.ToInt32(iRd1);
            foreach (DataRow dr1 in custdt.Rows)
            {
                object id;

                id = dr1[13].ToString();
                int idd = Convert.ToInt32(id);
                custdr = custdt1.NewRow();
                int i = 0;

                if (iRdd1 == idd)
                {
                    custdr[0] = dr1["pcode"].ToString();
                    custdr[1] = dr1["Truck_id"].ToString();
                    custdr[2] = dr1["TruckName"].ToString();
                    custdr[3] = dr1["PerDay_Distance"].ToString();
                    custdr[4] = dr1["KM_Price"].ToString();
                    custdr[5] = dr1["TotDistance"].ToString();
                    custdr[6] = dr1["TransAMount"].ToString();
                    custdr[7] = dr1["Tds"].ToString();
                    custdr[8] = dr1["GrossAmount"].ToString();
                    // dr1["Smltr1"] = "0";

                   // custdr[9] = dr1["Smltr1"].ToString();
                    custdr[9] = "0";
                   // custdr[10] = dr1["PerLtrCost"].ToString();
                    custdr[10] = "0";
                    // custdr[11] = dr1["PerDay_Ltr"].ToString();
                    custdr[11] = "0";
                    custdr[12] = dr1["DDay"].ToString();
                    custdr[13] = dr1["Rid"].ToString();
                    custdr[14] = dr1["Route_Name"].ToString();
                    custdt1.Rows.Add(custdr);
                    iRd1 = dr1["Rid"].ToString();
                    iRdd1 = Convert.ToInt32(iRd1);
                    i++;
                }
                else
                {
                    custdr[0] = dr1["pcode"].ToString();
                    custdr[1] = dr1["Truck_id"].ToString();
                    custdr[2] = dr1["TruckName"].ToString();
                    custdr[3] = dr1["PerDay_Distance"].ToString();
                    custdr[4] = dr1["KM_Price"].ToString();
                    custdr[5] = dr1["TotDistance"].ToString();
                    custdr[6] = dr1["TransAMount"].ToString();
                    custdr[7] = dr1["Tds"].ToString();
                    custdr[8] = dr1["GrossAmount"].ToString();
                    custdr[9] = dr1["Smltr1"].ToString();
                    custdr[10] = dr1["PerLtrCost"].ToString();
                    custdr[11] = dr1["PerDay_Ltr"].ToString();
                    custdr[12] = dr1["DDay"].ToString();
                    custdr[13] = dr1["Rid"].ToString();
                    custdr[14] = dr1["Route_Name"].ToString();
                    custdt1.Rows.Add(custdr);
                    iRd1 = dr1["Rid"].ToString();
                    iRdd1 = Convert.ToInt32(iRd1);
                    i++;
                }

               
            }

            dt = null;
            DataView dv1 = custdt1.DefaultView;
            dv1.Sort = "Truck_id ";
            custdt = null;
            custdt = dv1.ToTable();
            custdt.DefaultView.Sort = "Truck_id,Rid ";
           

            // Order by using Storeprocedure
            DataTable custOrderDT = new DataTable();
            custOrderDT = custdt;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "custOrderDT";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = custOrderDT;
            param.Direction = ParameterDirection.Input;           
            SqlConnection conn = null;
            using (conn = dbaccess.GetConnection())
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Transport_OrderBy]");                
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
               
                adp.Fill(resdt);                             

            }
            //

            cr.SetDataSource(resdt);
            CrystalReportViewer1.ReportSource = cr;
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    
    //private void ViewReport_Click(object sender, EventArgs e)
    //{
    //    rptClientDoc = rpt.ReportClientDocument;
    //    crystalReportViewer1.ReportSource = rpt;

    //    // set up the format export types:
    //    // to set each type allowed
    //    //int myFOpts = (int)(CrystalDecisions.Shared.ViewerExportFormats.RptFormat | CrystalDecisions.Shared.ViewerExportFormats.PdfFormat | CrystalDecisions.Shared.ViewerExportFormats.RptrFormat | CrystalDecisions.Shared.ViewerExportFormats.XLSXFormat );

    //    // to set all types allowed
    //    int myFOpts = (int)(CrystalDecisions.Shared.ViewerExportFormats.AllFormats);

    //    crystalReportViewer1.AllowedExportFormats = myFOpts;

    //}



    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        GetprourementIvoiceData();

    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            GetprourementIvoiceData();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

            DateTime frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string fdate = frmdate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
            DateTime todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string tdate = todate.ToString("dd" + "_" + "MM" + "_" + "yyyy");

            string CurrentCreateFolderName = fdate + "_" + tdate + "_" + DateTime.Now.ToString("ddMMyyyy");
            string path = @"C:\BILL VYSHNAVI\" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
                CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "TransportReport.pdf";
           

            CrExportOptions = cr.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            cr.Export();
            WebMsgBox.Show("Report Export Successfully...");

            //
            string MFileName = string.Empty;

            MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "TransportReport.pdf";
            
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(MFileName.ToString()))
            {
                //
                FileStream sourceFile = new FileStream(file.FullName, FileMode.Open);
                float FileSize;
                FileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)FileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                //
                Response.ClearContent(); // neded to clear previous (if any) written content
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.BinaryWrite(getContent);
                File.Delete(file.FullName.ToString());
                Response.Flush();
                Response.End();

            }
            else
            {

                Response.Write("File Not Found...");
            }
            //
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("Please Check the ExportPath...");
        }
    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        if (cr != null)
        {

            cr.Close();

            cr.Dispose();

            GC.Collect();

        }
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}