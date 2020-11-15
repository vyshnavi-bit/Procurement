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

public partial class OverAllReport : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string managmobNo;
    public int rid;
    BLLuser Bllusers = new BLLuser();
    DateTime dtm = new DateTime();
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                // pcode = Session["Plant_Code"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                //LoadPlantcode();
                LoadPlantName();
                LoadPlantName1();
                Menu1();

                pcode = ddl_Plantname.SelectedItem.Value;
                //GetOverallData();
                if (Chk_Allplant.Checked == true)
                {
                    Label1.Visible = false;
                    ddl_Plantname.Visible = false;
                    Panel3.Visible = true;
                }
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
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;


                GetOverallData();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }



    private void LoadPlantName()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst(ccode.ToString());
            ddl_Plantname.DataSource = ds;
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "plant_Code";
            ddl_Plantname.DataBind();
            if (ddl_Plantname.Items.Count > 0)
            {
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
            }
        }
        catch (Exception ex)
        {
        }

    }




    //private void LoadPlantcode()
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;
    //        dr = Bllusers.LoadPlantcode(ccode);
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
    //                ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}



    protected void Button1_Click(object sender, EventArgs e)
    {
        pcode = ddl_Plantname.SelectedItem.Value;
        Get_AdminPlantdataFinal();
        Update_AdminPlantdataFinal();
        GetOverallData();
    }


    private void Get_AdminPlantdataFinal()
    {
        try
        {
            DateTime dtm1 = new DateTime();
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");

            //
            DataTable dt = new DataTable();
            dt = null;
            int count = 0;
            dt = BllPlant.DTLoadPlantNameChkLst(ccode, d1, d2);
            count = dt.Rows.Count;
            if (count > 0)
            {
                DataTable custDT = new DataTable();
                DataColumn col = null;

                col = new DataColumn("plant_Code");
                custDT.Columns.Add(col);

                for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                {
                    DataRow dr = null;
                    dr = custDT.NewRow();
                    if (CheckBoxList1.Items[i].Selected == true)
                    {
                        dr[0] = CheckBoxList1.Items[i].Value.ToString();
                        custDT.Rows.Add(dr);
                    }

                }
                //


                SqlParameter param = new SqlParameter();
                param.ParameterName = "CustDtPlantcode";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = custDT;
                param.Direction = ParameterDirection.Input;
                String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("[dbo].[GetAdminPlantdataFinal]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    // sqlCmd.Parameters.AddWithValue("@Company_Code", ccode);
                    // sqlCmd.Parameters.AddWithValue("@Plant_Code", ccode);
                    sqlCmd.Parameters.AddWithValue("@spfrdate", d1.Trim());
                    sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                    sqlCmd.ExecuteNonQuery();

                }
            }
        }

        catch (Exception ex)
        {
        }
    }

    private void Update_AdminPlantdataFinal()
    {
        try
        {
            DateTime dtm1 = new DateTime();
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("[dbo].[UpdateAdminPlantdataFinal]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                // sqlCmd.Parameters.AddWithValue("@Company_Code", ccode);
                // sqlCmd.Parameters.AddWithValue("@Plant_Code", ccode);
                // sqlCmd.Parameters.AddWithValue("@frdate", d1.Trim());
                // sqlCmd.Parameters.AddWithValue("@todate", d2.Trim());
                sqlCmd.ExecuteNonQuery();

            }
        }

        catch (Exception ex)
        {
        }
    }
    private void GetOverallData()
    {
        try
        {
            pcode = ddl_Plantname.SelectedItem.Value;
            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            //cr.Load(Server.MapPath("CrpOverall.rpt"));


            if (Chk_Allplant.Checked == true)
            {
                cr.Load(Server.MapPath("Crpt_OverallTrim.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Crpt_NewOverall.rpt"));
            }

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
            if (Chk_Allplant.Checked == true)
            {
                t2.Text = "";
            }
            else
            {
                t2.Text = ddl_Plantname.SelectedItem.Value;
            }
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();


            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            //str = "SELECT  t4.TruckId,t4.Tname AS TruckName,CAST(t4.PerSesKm AS DECIMAL(18,2)) AS PerSesKm,CAST(t4.TotTrip AS DECIMAL(18,2)) AS TotalTrip,CAST(t4.TotalDistance AS DECIMAL(18,2)) AS TotalKm,CAST(t4.Lcost AS DECIMAL(18,2)) AS perKmRate,CAST(Amount AS DECIMAL(18,2)) AS TotAmount,CAST(Smltr AS DECIMAL(18,2)) AS QtyLtr,CAST(t4.PerLtrTCost AS DECIMAL(18,2)) AS PerLtrTCost,CAST((t4.Smltr/TotTrip) AS DECIMAL(18,2)) AS PerSesLtrs FROM (SELECT t3.Trk_Id AS TruckId,t3.Smltr AS Smltr,t3.Lcost,t3.TripCount AS TotTrip,(Distance*t3.TripCount) AS TotalDistance,((Distance*t3.TripCount)*t3.Lcost) AS AMOUNT,(((Distance*t3.TripCount)*t3.Lcost)/Smltr) AS PerLtrTCost,Tname,Distance AS PerSesKm FROM (SELECT t2.Truck_Id AS Trk_Id,t2.Smltr AS Smltr,t2.Ltr_Cost AS Lcost,DayCount AS TripCount,t2.Truck_Name AS Tname FROM (SELECT Smltr,Truck_Id,DayCount,Ltr_Cost,Truck_Name FROM ( SELECT Smltr,Truck_Id,ISNULL(DayCount,0) AS DayCount FROM( (SELECT SUM(Milk_Ltr) AS Smltr ,Truck_Id FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + " AND prdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' GROUP BY Truck_Id  ) AS pro LEFT JOIN (SELECT COUNT(Session_Id) AS DayCount,Truck_Id AS tid  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + " AND Pdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' GROUP BY Truck_Id ) AS Trckpre ON pro.Truck_Id=Trckpre.tid )) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + " ) AS VD ON t1.Truck_Id=VD.VDTrkid) AS t2 ) AS t3 LEFT JOIN (SELECT DISTINCT(Truck_id) AS Trkalotid,Distance  FROM TRUCK_ROUTEDISTANCE WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + " )AS TAlot ON t3.Trk_Id=TAlot.Trkalotid) AS t4";
            // CrpOverall.rpt str = "SELECT * FROM (SELECT pcode,Smltr AS Smltr1,Smkg1,AvgFat1,AvgSnf1,AvgRate1,Avgclr1,Scans1,SAmt1,Avgcrate1,Sfatkg1,Ssnfkg1,Billadv1,Ai1,feed1,can1,recovery1,others1,instamt1,((SAmt1+AvgRate1)-(Dedu)) AS Netpay,Fin2.ComAmt FROM (SELECT pcode AS pcode,Smltr AS Smltr1,Smkg AS Smkg1,AvgFat AS AvgFat1,AvgSnf AS AvgSnf1,AvgRate AS AvgRate1,Avgclr AS Avgclr1,Scans AS Scans1,SAmt AS SAmt1,Avgcrate AS Avgcrate1,Sfatkg AS Sfatkg1,Ssnfkg AS Ssnfkg1,Billadv AS Billadv1,Ai AS Ai1,feed AS feed1,can AS can1,recovery AS recovery1,others AS others1,instamt AS instamt1,(Billadv+Ai+feed+instamt) AS Dedu FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others,ISNULL(instamt,0) AS instamt  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code=" + ccode + " AND Plant_Code=" + pcode + " AND Prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate  BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND Company_Code=" + ccode + " AND plant_code='" + pcode + "' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 AND plant_code='" + pcode + "' GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ) AS tt2 LEFT JOIN (SELECT APCode,SUM(ISNULL(Cartage_Amt,0)) AS Cartage_Amt,SUM(ISNULL(SMltr,0)) AS SMltr,SUM(ISNULL(ComAmt,0)) AS ComAmt  FROM (SELECT DISTINCT(AAid) AS Aiid,APCode,Cartage_Amt,SMltr,CAST((Cartage_Amt*SMltr) AS DECIMAL(18,2)) AS ComAmt FROM (SELECT APCode,ISNULL(Cartage_Amt,0) AS Cartage_Amt,t2.Agent_id AS AAid,ISNULL(t3.Mltrr,0) AS mlt FROM (SELECT Plant_Code AS APCode,Agent_id,Cartage_Amt FROM AGENT_MASTER WHERE Type=0 AND Company_Code='1' AND Cartage_Amt>0 ) AS t2 LEFT JOIN (SELECT ppcode,t1.Mltr AS Mltrr,Aid FROM (SELECT Plant_Code AS ppcode,Milk_Ltr AS Mltr,Agent_id AS Aid FROM Procurement AS P WHERE  Prdate  BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND  Company_Code=" + ccode + " AND Plant_Code IN (SELECT Plant_Code AS APCode FROM  Agent_Master AS A WHERE Type=0 AND Company_Code='" + ccode + "' AND Cartage_Amt>0 AND A.Plant_Code=p.Plant_Code AND Plant_Code='" + pcode + "' )) AS t1 )AS t3 ON t2.APCode=t3.ppcode) AS t4 LEFT JOIN (SELECT Plant_Code AS p2pcode,SUM(Milk_ltr) AS SMltr,Agent_id AS p2Aid FROM Procurement AS P2 WHERE  Prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND  Company_Code=" + ccode + " AND plant_code='" + pcode + "' GROUP BY Plant_Code,Agent_id ) AS t5 ON t4.APCode=t5.p2pcode AND AAid=t5.p2Aid )AS fin GROUP BY APCode) AS fin2 ON tt2.pcode=fin2.APCode ) AS profin INNER JOIN (SELECT tppcode ,ISNULL(SPlantTransAmount,0) AS SPlantTransAmount1,ISNULL(GMilkkg,0) AS GMilkkg1,ISNULL(GFatkg,0) AS GFatkg1,ISNULL(GSnfkg,0) AS GSnfkg1 FROM (SELECT ppcode AS tppcode,SUM(TransAmount) AS SPlantTransAmount FROM (SELECT Distinct(TrkID),pcod AS ppcode,Lcst1,Smltrs1,Daycounts1,Distanc1,(CAST(((Distanc1*Daycounts1)*Lcst1) AS DECIMAL(18,2))) AS TransAmount FROM (SELECT TrkID,pcod,ISNULL(Lcst,0) AS Lcst1,ISNULL(Smltrs,0) AS Smltrs1,ISNULL(Daycounts,0) AS Daycounts1,ISNULL(Distanc,0) AS Distanc1 FROM (SELECT t2.VDTrkid AS TrkID,t2.ppcode AS pcod,CAST(t2.Lrtcst AS DECIMAL(18,2)) AS Lcst,CAST(t2.Smltr AS DECIMAL(18,2)) AS Smltrs,CAST(t3.DayCount AS DECIMAL(18,2)) AS Daycounts,CAST(t3.Distance AS DECIMAL(18,2))AS Distanc FROM (SELECT VDTrkid,milk_Ltr,Lrtcst,Smltr,ppcode FROM (SELECT Ltr_Cost AS Lrtcst,milk_Ltr,ppcode,VDTrkid FROM (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,Plant_Code FROM Vehicle_Details WHERE Company_Code=1 AND Plant_Code='" + pcode + "') AS VD INNER JOIN (SELECT Milk_Ltr ,Plant_code AS ppcode FROM PROCUREMENT WHERE Company_Code=" + ccode + "  AND prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND plant_code='" + pcode + "' ) AS pro ON VD.Plant_Code=pro.ppcode) AS t1 INNER JOIN (SELECT SUM(Milk_Ltr) AS Smltr ,Truck_id,Plant_code AS ppcode1 FROM PROCUREMENT WHERE Company_Code=" + ccode + "  AND prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND plant_code='" + pcode + "' GROUP BY Plant_code,Truck_id ) AS pro1 ON t1.ppcode=pro1.ppcode1 AND t1.VDTrkid=pro1.Truck_id ) AS t2 INNER JOIN (SELECT DISTINCT(tid) AS Truid,DayCount,Plant_Code,Distance FROM (SELECT COUNT(Session_Id) AS DayCount,Truck_Id AS tid,Plant_Code  FROM TRUCK_PRESENT WHERE Company_Code=" + ccode + " AND Pdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND plant_code='" + pcode + "' GROUP BY Plant_Code,Truck_Id ) AS TP INNER JOIN (SELECT Truck_id AS trtid,Distance,plant_code AS TRpcode  FROM TRUCK_ROUTEDISTANCE WHERE Company_Code=1 ) AS RD ON TP.Plant_Code=RD.TRpcode AND TP.tid=RD.trtid) AS t3 ON t2.ppcode=t3.plant_code AND t2.VDTrkid=t3.Truid ) t4 ) AS t5 ) AS t6  GROUP BY ppcode )truckfin INNER JOIN (SELECT Pcode1,CAST(((cl_MILKKG1+db_MILKKG1)-(op_MILKKG+pr_MILKKG1)) AS DECIMAL(18,2)) AS GMilkkg,(pr_FATKG1+op_FATKG)-((dp_FATKG1+cl_FATKG1)) AS GFatkg,((pr_SNFKG1+op_SNFKG)-(dp_SNFKG1+cl_SNFKG1)) AS GSnfkg  FROM (SELECT Pcode1,ISNULL(pr_MILKKG,0) AS pr_MILKKG1,ISNULL(pr_FAT,0) AS pr_FAT1,ISNULL(pr_SNF,0) AS pr_SNF1,ISNULL(pr_FATKG,0) AS pr_FATKG1,ISNULL(pr_SNFKG,0)AS pr_SNFKG1,ISNULL(pr_RATE,0)AS pr_RATE1,ISNULL(pr_AMOUNT,0)AS pr_AMOUNT,op_MILKKG,op_FAT,op_SNF,op_FATKG,op_SNFKG,op_RATE,op_AMOUNT,ISNULL(t2.dp_MILKKG1,0) AS db_MILKKG1,ISNULL(t2.dp_FAT1,0) AS dp_FAT1,ISNULL(t2.dp_SNF1,0) AS dp_SNF1,ISNULL(t2.dp_FATKG1,0) AS dp_FATKG1,ISNULL(t2.dp_SNFKG1,0) AS dp_SNFKG1,ISNULL(t2.dp_RATE1,0) AS dp_RATE1,ISNULL(t2.dp_AMOUNT1,0) AS dp_AMOUNT1,ISNULL(t2.cl_MILKKG1,0) AS cl_MILKKG1,ISNULL(t2.cl_FAT1,0) AS cl_FAT1,ISNULL(t2.cl_SNF1,0) AS cl_SNF1,ISNULL(t2.cl_FATKG1,0) AS cl_FATKG1,ISNULL(t2.cl_SNFKG1,0) AS cl_SNFKG1,ISNULL(t2.cl_RATE1,0) AS cl_RATE1,ISNULL(t2.cl_AMOUNT1,0) AS cl_AMOUNT1 FROM (select procur.pr_MILKKG AS pr_MILKKG,cast(procur.pr_FAT as decimal(18,2)) as pr_FAT,cast(procur.pr_SNF as decimal(18,2)) as pr_SNF ,cast(procur.pr_FATKG as decimal(18,2)) as pr_FATKG,cast(procur.pr_SNFKG as decimal(18,2)) as pr_SNFKG,cast(procur.pr_RATE as decimal(18,2)) as pr_RATE,cast(procur.pr_AMOUNT as decimal(18,2)) as pr_AMOUNT,Plant_code AS Pcode1 , isnull(openstock.op_MILKKG,0) as op_MILKKG,isnull(openstock.op_FAT,0) as op_FAT,isnull(openstock.op_SNF,0)as op_SNF ,ISNULL(openstock.op_FATKG,0) as op_FATKG,ISNULL(openstock.op_SNFKG,0) as op_SNFKG,ISNULL(openstock.op_RATE,0) AS op_RATE,ISNULL(openstock.op_AMOUNT,0) AS op_AMOUNT from  (SELECT  sum(Milk_kg) as pr_MILKKG,AVG(Fat) as pr_FAT,AVG(Snf) as pr_SNF,AVG(fat_kg)as pr_FATKG,avg(snf_kg) as pr_SNFKG,SUM (Amount) as pr_AMOUNT,avg(Rate) as pr_RATE,Plant_code  from Procurement where Prdate BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND plant_code='" + pcode + "'  group by  Plant_code) As  procur LEFT JOIN (select top 1 cast(Milkkg  as decimal(18,2)) as op_MILKKG,cast(Fat as decimal(18,2)) as op_FAT,cast(Snf as decimal(18,2)) as op_SNF,cast(fat_kg as decimal(18,2)) as op_FATKG,cast(snf_kg as decimal(18,2)) as op_SNFKG,(Amount) as op_AMOUNT,(Rate) as op_RATE,Plant_code AS ospcode from Stock_openingmilk where Datee  BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND plant_code='" + pcode + "' order by Datee ASC) AS openstock  on procur.Plant_code= openstock.ospcode ) as joinopenprocure LEFT JOIN (SELECT cl_Plant_code,ISNULL(dp_MILKKG,0)AS dp_MILKKG1,ISNULL(dp_FAT,0)AS dp_FAT1,ISNULL(dp_SNF,0) AS dp_SNF1,ISNULL(dp_FATKG,0) AS dp_FATKG1,ISNULL(dp_SNFKG,0) AS dp_SNFKG1,ISNULL(dp_RATE,0) AS dp_RATE1,ISNULL(dp_AMOUNT,0) AS dp_AMOUNT1,ISNULL(cl_MILKKG,0) AS cl_MILKKG1,ISNULL(cl_FAT,0) AS cl_FAT1,ISNULL(cl_SNF,0) AS cl_SNF1,ISNULL(cl_FATKG,0) AS cl_FATKG1,ISNULL(cl_SNFKG,0)AS cl_SNFKG1,ISNULL(cl_RATE,0) AS cl_RATE1,ISNULL(cl_AMOUNT,0)AS cl_AMOUNT1 FROM (select cl_Plant_code,CAST(despat.dp_MILKKG as decimal(18,2)) dp_MILKKG,CAST(despat.dp_FAT as decimal(18,2)) dp_FAT,CAST(despat.dp_SNF as decimal(18,2)) dp_SNF,CAST(despat.dp_FATKG as decimal(18,2)) dp_FATKG,CAST(despat.dp_SNFKG as decimal(18,2)) as dp_SNFKG,CAST(despat.dp_RATE as decimal(18,2)) as dp_RATE, CAST(despat.dp_AMOUNT as decimal(18,2)) as dp_AMOUNT,cast(closestock.cl_MILKKG as  decimal(18,2)) as  cl_MILKKG,cast(closestock.cl_FAT as  decimal(18,2)) as  cl_FAT,cast(closestock.cl_SNF as  decimal(18,2)) as cl_SNF,cast(closestock.cl_FATKG as  decimal(18,2)) as cl_FATKG,cast(closestock.cl_SNFKG as  decimal(18,2)) as cl_SNFKG,cast(closestock.cl_RATE as  decimal(18,2)) as cl_RATE,cast(closestock.cl_AMOUNT as  decimal(18,2)) as cl_AMOUNT  from (SELECT top 1 MilkKg as cl_MILKKG,Fat as cl_FAT,Snf as cl_SNF,Fat_Kg as cl_FATKG,Snf_Kg as cl_SNFKG,Amount AS cl_AMOUNT,Rate AS cl_RATE,Plant_code as cl_Plant_code FROM Stock_Milk WHERE Date BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND Plant_code='" + pcode + "' ORDER BY Date DESC ) as closestock LEFT JOIN (select sum(MilkKg) as dp_MILKKG,AVG(Fat) as dp_FAT,AVG(Snf) as dp_SNF,avg(Fat_Kg) as dp_FATKG,avg(Snf_Kg) as dp_SNFKG,SUM (Amount) as dp_AMOUNT,avg(Rate) as dp_RATE,Plant_code as dp_Plant_code from Despatchnew where date BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND Plant_code='" + pcode + "'  group by  Plant_code ) as despat  on closestock.cl_Plant_code=despat.dp_Plant_code) AS t1) AS t2 ON  joinopenprocure.Pcode1=t2.cl_Plant_code) AS tfin1 )  tfin2 ON truckfin.tppcode= tfin2.Pcode1 ) AS totfin ON  profin.pcode=totfin.tppcode ";
            //LAST WORKING 17-11-2014  str = "SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT G1.pcode AS pcode1,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  CAST(SUM(G1.Sfatkg) AS DECIMAL(18,2)) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround ,CAST(SUM(G1.SClaim) AS DECIMAL(18,2)) AS GSClaim FROM (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) +(ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + (ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0))) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS SNetAmt,( ( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) +ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- ( FLOOR( (ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) ) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) )  ) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo,pcode FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,CAST(SUM(Milk_kg) AS DECIMAL(18,2)) AS Smkg,CAST(SUM(Milk_ltr) AS DECIMAL(18,2)) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,CAST(SUM(Amount) AS DECIMAL(18,2)) AS SAmt,CAST(SUM(Comrate) AS DECIMAL(18,2)) AS ScommAmt,CAST(SUM(ComRate) AS DECIMAL(18,2)) AS Scatamt,CAST(SUM(SplBonusAmount) AS DECIMAL(18,2)) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,CAST(SUM(fat_kg) AS DECIMAL(18,2)) AS Sfatkg,CAST(SUM(snf_kg) AS DECIMAL(18,2)) AS SSnfkg  FROM Procurement WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid,Plant_code AS pcode  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS cart ON prdelo.SproAid=cart.cartAid ) AS G1 GROUP BY G1.pcode) AS pro LEFT JOIN (SELECT t1.pcode AS pcod, CAST((ISNULL(t1.pr_MILKKG,0)+ ISNULL(t3.op_MILKKG,0)) AS DECIMAL(18,2)) AS Amilkkg , CAST((ISNULL(t1.pr_FATKG,0)+ ISNULL(t3.op_FATKG,0)) AS DECIMAL(18,2) ) AS AFatkg,  CAST((ISNULL(t1.pr_SNFKG,0)+ ISNULL(t3.op_SNFKG,0)) AS DECIMAL(18,2) ) AS ASnfkg, CAST((ISNULL(t1.pr_FAT,0)+ ISNULL(t3.op_FAT,0)) AS DECIMAL(18,2) ) AS AFat, CAST((ISNULL(t1.pr_SNF,0)+ ISNULL(t3.op_SNF,0)) AS DECIMAL(18,2) ) AS ASnf,  CAST((ISNULL(t2.dp_MILKKG,0)+ ISNULL(C.cl_MILKKG,0)) AS DECIMAL(18,2)) AS Bmilkkg, CAST((ISNULL(t2.dp_FATKG,0)+ ISNULL(C.cl_FATKG,0)) AS DECIMAL(18,2)) AS BFatkg, CAST((ISNULL(t2.dp_SNFKG,0)+ ISNULL(C.cl_SNFKG,0)) AS DECIMAL(18,2)) AS BSnfkg, CAST((ISNULL(t2.dp_FAT,0)+ ISNULL(C.cl_FAT,0)) AS DECIMAL(18,2)) AS BFat, CAST((ISNULL(t2.dp_SNF,0)+ ISNULL(C.cl_SNF,0)) AS DECIMAL(18,2)) AS BSnf, CAST((ISNULL(t2.dp_MILKKG,0)+ ISNULL(C.cl_MILKKG,0))-(ISNULL(t1.pr_MILKKG,0)+ ISNULL(t3.op_MILKKG,0)) AS DECIMAL(18,2)) AS Gainmilkkg, CAST((ISNULL(t2.dp_FATKG,0)+ ISNULL(C.cl_FATKG,0))-(ISNULL(t1.pr_FATKG,0)+ ISNULL(t3.op_FATKG,0)) AS DECIMAL(18,2) ) AS GainFatkg, CAST((ISNULL(t2.dp_SNFKG,0)+ ISNULL(C.cl_SNFKG,0))-(ISNULL(t1.pr_SNFKG,0)+ ISNULL(t3.op_SNFKG,0)) AS DECIMAL(18,2) ) AS GainSnfkg  FROM ( SELECT  Plant_Code as pcode,Sum(Milk_kg) as pr_MILKKG,Avg(fat) as pr_FAT,AVG(snf) as pr_SNF,sum(fat_kg)as pr_FATKG,sum(snf_kg) as pr_SNFKG,sum(rate) as pr_RATE,sum(AMOUNT) as pr_AMOUNT FROM Procurement Where prdate between  '" + d1.ToString() + "' AND '" + d2.ToString() + "' and  Plant_Code='" + pcode + "' Group By Plant_Code  )t1,(SELECT Sum(Milkkg) as dp_MILKKG,Avg(fat) as dp_FAT,AVG(snf) as dp_SNF,SUM(Fat_Kg) as dp_FATKG,sum(Snf_Kg) as dp_SNFKG, sum(RATE) as dp_RATE,sum(Amount) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1.ToString() + "' AND '" + d2.ToString() + "' and  Plant_Code='" + pcode + "')t2, (SELECT Sum(Milkkg) as op_MILKKG,Avg(fat) as op_FAT,AVG(snf) as op_SNF,SUM(Fat_Kg) as op_FATKG,sum(Snf_Kg) as op_SNFKG, sum(RATE) as op_RATE,sum(Amount) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1.ToString() + "' and  Plant_Code='" + pcode + "')t3, (SELECT Sum(Milkkg) as cl_MILKKG,Avg(fat) as cl_FAT,AVG(snf) as cl_SNF,SUM(Fat_Kg) as cl_FATKG,sum(Snf_Kg) as cl_SNFKG, sum(RATE) as cl_RATE,sum(Amount) as cl_AMOUNT FROM Stock_Milk Where date = '" + d2.ToString() + "'  and  Plant_Code='" + pcode + "') C) AS LG ON pro.pcode1=LG.pcod) AS Ft3  LEFT JOIN  (SELECT pcode,SUM(Smltr1) AS Smltr1,SUM(TransAMount) AS TransAMount  FROM (SELECT pcode,Rid,Truck_id,Truck_Name+Truck_Name1 AS TruckName,(OndayTotDistance/DDay) AS PerDay_Distance,(Ltr_Cost + Ltr_Cost1) AS KM_Price,OndayTotDistance AS TotDistance,((TransAmountFix * DDay) +TransAmount1) AS TransAMount,CAST((((TransAmountFix * DDay) +TransAmount1)*0.02) AS DECIMAL(18,2)) AS Tds,CAST((((TransAmountFix * DDay) +TransAmount1)-(((TransAmountFix * DDay) +TransAmount1)*0.02)) AS DECIMAL(18,2)) AS GrossAmount,Smltr AS Smltr1,CAST((((TransAmountFix * DDay) +TransAmount1)/Smltr1) AS DECIMAL(18,2)) AS PerLtrCost,CAST((Smltr/DDay) AS DECIMAL(18,2)) AS PerDay_Ltr,DDay  FROM (SELECT Truck_id,Smltr,OndayTotDistance,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL(fixed,0) AS fixed,ISNULL(Truck_Name,'') AS Truck_Name,CAST((ISNULL(Ltr_Cost,0)) AS DECIMAL(18,2)) AS TransAmountFix,Rid FROM (SELECT Truck_id,Smltr,OndayTotDistance,Rid FROM (SELECT SUM(CAST((ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2))) AS Smltr ,Truck_Id,Route_id AS Rid FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id,Route_id  ) AS pro LEFT JOIN (SELECT Truck_Id AS tid,SUM(ISNULL(Tdistance,0)) AS OndayTotDistance  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON pro.Truck_Id=Trckpre.tid) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=1 ) AS VD  ON t1.Truck_id=VD.VDTrkid) AS F1 LEFT JOIN (SELECT Truck_id AS Truck_id1,Smltr AS Smltr1,OndayTotDistance AS OndayTotDistance1,ISNULL(Ltr_Cost,0) AS Ltr_Cost1,ISNULL(fixed,0) AS fixed1,ISNULL(Truck_Name,'') AS Truck_Name1,CAST((ISNULL(Ltr_Cost,0)*OndayTotDistance) AS DECIMAL(18,2)) AS TransAmount1,pcode,DDay FROM (SELECT Truck_id,Smltr,OndayTotDistance,Plant_Code AS pcode,(DDay+1) AS DDay FROM (SELECT SUM(CAST((ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2))) AS Smltr ,Truck_Id,Plant_Code FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id,Plant_Code,Route_id  ) AS pro  LEFT JOIN  (SELECT Truck_Id AS tid,SUM(ISNULL(Tdistance,0)) AS OndayTotDistance,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay  FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON pro.Truck_Id=Trckpre.tid) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=0 ) AS VD  ON t1.Truck_id=VD.VDTrkid) AS F2 ON F1.Truck_id=F2.Truck_id1 ) AS t1 INNER JOIN (SELECT Route_Name,Route_ID FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS t2 ON t1.Rid= t2.Route_ID GROUP BY pcode ) AS Ft4 ON Ft3.pcode1=Ft4.pcode ) AS Ft5 LEFT JOIN (SELECT DD+1 AS DDays FROM (SELECT Top 1 datediff(day, '" + d1.ToString() + "' , '" + d2.ToString() + "') as DD FROM Truck_Present WHERE Plant_Code='" + pcode + "' )  AS D ) AS D1 ON Ft5.pcode1>150";

            if (Chk_Allplant.Checked == true)
            {
                str = "SELECT * FROM AdminDataTable ORDER BY Tid";
            }
            else
            {
                str = "SELECT * FROM " +
    " (SELECT * FROM " +
    " (SELECT * FROM " +
    " (SELECT G1.pcode AS pcode1,CAST(SUM(G1.Smkg)  AS DECIMAL(18,2)) AS GSmkg,  CAST(SUM(G1.Smltr)  AS DECIMAL(18,2)) AS GSmltr,  CAST(AVG(G1.AvgFat) AS DECIMAL(18,1)) AS GAvgFat,  CAST(AVG(G1.AvgSnf) AS DECIMAL(18,1)) AS GAvgSnf,  CAST(AVG(G1.AvgRate) AS DECIMAL(18,1)) AS GAvgRate,  CAST(AVG(G1.Avgclr) AS DECIMAL(18,1)) AS GAvgclr,  CAST(SUM(G1.Scans) AS DECIMAL(18,2)) AS GScans,  CAST(SUM(G1.SAmt) AS DECIMAL(18,2)) AS GSAmt,  CAST(SUM(G1.ScommAmt) AS DECIMAL(18,2)) AS GScommAmt,  CAST(SUM(G1.Scatamt) AS DECIMAL(18,2)) AS GScatamt,  CAST(SUM(G1.Ssplbonamt) AS DECIMAL(18,2)) AS GSsplbonamt,  CAST(AVG(G1.AvgcRate) AS DECIMAL(18,2)) AS GAvgcRate,  SUM(G1.Sfatkg) AS GSfatkg,  CAST(SUM(G1.Ssnfkg) AS DECIMAL(18,2)) AS GSsnfkg,  CAST(SUM(G1.SBilladv) AS DECIMAL(18,2)) AS GSBilladv,  CAST(SUM(G1.SAiamt) AS DECIMAL(18,2)) AS GSAiamt,  CAST(SUM(G1.SFeedamt) AS DECIMAL(18,2)) AS GSFeedamt,  CAST(SUM(G1.Scanamt) AS DECIMAL(18,2)) AS GScanamt,  CAST(SUM(G1.SRecoveryamt) AS DECIMAL(18,2)) AS GSRecoveryamt,  CAST(SUM(G1.Sothers) AS DECIMAL(18,2)) AS GSothers,  CAST(SUM(G1.Sinstamt) AS DECIMAL(18,2)) AS GSinstamt,  CAST(SUM(G1.Sbalance) AS DECIMAL(18,2)) AS GSbalance,  CAST(SUM(G1.SLoanAmount) AS DECIMAL(18,2)) AS GSLoanAmount,  CAST(SUM(G1.SNetAmt) AS DECIMAL(18,2)) AS GSNetAmt,  CAST(SUM(G1.SRound)  AS DECIMAL(18,2)) AS GSround ,CAST(SUM(G1.SClaim) AS DECIMAL(18,2)) AS GSClaim FROM " +
    " (SELECT cart.ARid AS Rid,cart.cartAid AS Aid,cart.Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS SNetAmt,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo,pcode FROM " +
    " (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate  BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid,Plant_code AS pcode  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS cart ON prdelo.SproAid=cart.cartAid ) AS G1 GROUP BY G1.pcode) AS pro " +

    " LEFT JOIN (SELECT  t1.pcode AS pcod, CAST((ISNULL(t1.pr_MILKKG,0)+ ISNULL(t3.op_MILKKG,0)) AS DECIMAL(18,2)) AS Amilkkg , CAST((ISNULL(t1.pr_FATKG,0)+ ISNULL(t3.op_FATKG,0)) AS DECIMAL(18,2) ) AS AFatkg,  CAST((ISNULL(t1.pr_SNFKG,0)+ ISNULL(t3.op_SNFKG,0)) AS DECIMAL(18,2) ) AS ASnfkg, CAST((ISNULL(t1.pr_FAT,0)+ ISNULL(t3.op_FAT,0)) AS DECIMAL(18,2) ) AS AFat, CAST((ISNULL(t1.pr_SNF,0)+ ISNULL(t3.op_SNF,0)) AS DECIMAL(18,2) ) AS ASnf,  CAST((ISNULL(t2.dp_MILKKG,0)+ ISNULL(C.cl_MILKKG,0)) AS DECIMAL(18,2)) AS Bmilkkg, CAST((ISNULL(t2.dp_FATKG,0)+ ISNULL(C.cl_FATKG,0)) AS DECIMAL(18,2)) AS BFatkg, CAST((ISNULL(t2.dp_SNFKG,0)+ ISNULL(C.cl_SNFKG,0)) AS DECIMAL(18,2)) AS BSnfkg, CAST((ISNULL(t2.dp_FAT,0)+ ISNULL(C.cl_FAT,0)) AS DECIMAL(18,2)) AS BFat, CAST((ISNULL(t2.dp_SNF,0)+ ISNULL(C.cl_SNF,0)) AS DECIMAL(18,2)) AS BSnf, CAST((ISNULL(t2.dp_MILKKG,0)+ ISNULL(C.cl_MILKKG,0))-(ISNULL(t1.pr_MILKKG,0)+ ISNULL(t3.op_MILKKG,0)) AS DECIMAL(18,2)) AS Gainmilkkg, CAST((ISNULL(t2.dp_FATKG,0)+ ISNULL(C.cl_FATKG,0))-(ISNULL(t1.pr_FATKG,0)+ ISNULL(t3.op_FATKG,0)) AS DECIMAL(18,2) ) AS GainFatkg, CAST((ISNULL(t2.dp_SNFKG,0)+ ISNULL(C.cl_SNFKG,0))-(ISNULL(t1.pr_SNFKG,0)+ ISNULL(t3.op_SNFKG,0)) AS DECIMAL(18,2) ) AS GainSnfkg  FROM ( " +
    " SELECT Plant_Code as pcode,Sum(Milk_kg) as pr_MILKKG,Avg(fat) as pr_FAT,AVG(snf) as pr_SNF,sum(fat_kg)as pr_FATKG,sum(snf_kg) as pr_SNFKG,sum(rate) as pr_RATE,sum(AMOUNT) as pr_AMOUNT FROM Procurement Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' Group By Plant_Code)t1,(SELECT Sum(Milkkg) as dp_MILKKG,Avg(fat) as dp_FAT,AVG(snf) as dp_SNF,SUM(Fat_Kg) as dp_FATKG,sum(Snf_Kg) as dp_SNFKG, sum(RATE) as dp_RATE,sum(Amount) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1.ToString() + "' AND '" + d2.ToString() + "' and  Plant_Code='" + pcode + "')t2,(SELECT Sum(Milkkg) as op_MILKKG,Avg(fat) as op_FAT,AVG(snf) as op_SNF,SUM(Fat_Kg) as op_FATKG,sum(Snf_Kg) as op_SNFKG, sum(RATE) as op_RATE,sum(Amount) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1.ToString() + "' and  Plant_Code='" + pcode + "') t3,(SELECT Sum(Milkkg) as cl_MILKKG,Avg(fat) as cl_FAT,AVG(snf) as cl_SNF,SUM(Fat_Kg) as cl_FATKG,sum(Snf_Kg) as cl_SNFKG, sum(RATE) as cl_RATE,sum(Amount) as cl_AMOUNT FROM Stock_Milk Where date = '" + d2.ToString() + "'  and  Plant_Code='" + pcode + "') C ) " +
    "  AS LG ON pro.pcode1=LG.pcod) AS Ft3  " +

    " LEFT JOIN  " +

    " (SELECT pcode,Ft6.Smltr1 AS Smltr1,TransAMount FROM " +
    " (SELECT SUM(Ft1.TransAMount) AS TransAMount,Ft1.pcode FROM " +
    " (SELECT F1.*,F2.Rid,F2.Route_Name,F2.pcode  FROM (SELECT TruckId3 AS Truck_id,CONVERT(Nvarchar(30),TruckId3)+'_'+(Truck_Name3+Truck_Name1) AS TruckName,(TotDistance3)/DDay3 AS PerDay_Distance,(KM_Price3+KM_Price1) AS KM_Price,(TotDistance3) AS TotDistance,(TransAMount3+TransAMount1) AS TransAMount,CAST(((TransAMount3+TransAMount1)*0.02) AS DECIMAL(18,2)) AS Tds,((TransAMount3+TransAMount1)-CAST(((TransAMount3+TransAMount1)*0.02) AS DECIMAL(18,2))) AS GrossAmount,Smltr3 AS Smltr1,CAST(((TransAMount3+TransAMount1)/Smltr3) AS DECIMAL(18,2)) AS PerLtrCost,CAST((Smltr3/DDay3) AS DECIMAL(18,2)) AS PerDay_Ltr,DDay3 AS DDay FROM (SELECT TruckId1 AS TruckId3,(Truck_Name1+Truck_Name2) AS Truck_Name3,Smltr1 AS Smltr3,(Ltr_Cost1+Ltr_Cost2) AS KM_Price3,DDay1 AS DDay3,TotDistance1 AS TotDistance3,(TransAMount1+TransAMount2) AS TransAMount3  FROM (SELECT TraTid AS TruckId1,Smltr AS Smltr1,Ltr_Cost AS Ltr_Cost1,Truck_Name AS Truck_Name1,Fixed AS Fixed1,DDay AS DDay1,ISNULL(TotDistance,0) AS TotDistance1,(Ltr_Cost*ISNULL(TotDistance,0)) AS TransAMount1 FROM  (SELECT TraTid,Smltr,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL('KM_'+Truck_Name,'') AS Truck_Name,ISNULL(Fixed,'1') AS Fixed,ISNULL(DDay,0) AS DDay FROM (SELECT TraTid,ISNULL(Smltr,0) AS Smltr,(DDay+1) AS DDay FROM (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Tra LEFT JOIN (SELECT CAST(SUM(ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2)) AS Smltr ,Route_id,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay   FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Route_id) AS Pro ON Tra.TraRid=Pro.Route_id ) AS t1 LEFT JOIN  (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=0 AND PaymentType=0 ) AS VD ON  t1.TraTid=VD.VDTrkid ) AS  t2 LEFT JOIN (SELECT Truck_Id AS trpreid,SUM(ISNULL(Tdistance,0)) AS TotDistance FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON t2.TraTid=Trckpre.trpreid) AS t3 LEFT JOIN  (SELECT TraTid AS TruckId2,Smltr AS Smltr2,Ltr_Cost AS Ltr_Cost2,Truck_Name AS Truck_Name2,Fixed AS Fixed2,DDay AS DDay2,ISNULL(TotDistance,0) AS TotDistance2,(Ltr_Cost*ISNULL(Smltr,0)) AS TransAMount2 FROM (SELECT TraTid,Smltr,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL('Ltr_'+Truck_Name,'') AS Truck_Name,ISNULL(Fixed,'1') AS Fixed,ISNULL(DDay,0) AS DDay FROM (SELECT TraTid,ISNULL(Smltr,0) AS Smltr,(DDay+1) AS DDay FROM  (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Tra LEFT JOIN (SELECT CAST(SUM(ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2)) AS Smltr ,Route_id,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay   FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Route_id) AS Pro ON Tra.TraRid=Pro.Route_id ) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=0 AND PaymentType=1 ) AS VD ON  t1.TraTid=VD.VDTrkid ) AS  t2 LEFT JOIN (SELECT Truck_Id AS trpreid,SUM(ISNULL(Tdistance,0)) AS TotDistance FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON t2.TraTid=Trckpre.trpreid ) AS t4 ON t3.TruckId1=t4.TruckId2 ) AS t5 LEFT JOIN (SELECT TraTid AS TruckId1,Smltr AS Smltr1,Ltr_Cost AS KM_Price1,Truck_Name AS Truck_Name1,Fixed AS Fixed1,DDay AS DDay1,ISNULL(TotDistance,0) AS TotDistance1,(Ltr_Cost*ISNULL(DDay,0)) AS TransAMount1 FROM (SELECT TraTid,Smltr,ISNULL(Ltr_Cost,0) AS Ltr_Cost,ISNULL('FIX_'+Truck_Name,'') AS Truck_Name,ISNULL(Fixed,'1') AS Fixed,ISNULL(DDay,0) AS DDay FROM (SELECT TraTid,ISNULL(Smltr,0) AS Smltr,(DDay+1) AS DDay FROM (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Tra LEFT JOIN (SELECT CAST(SUM(ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2)) AS Smltr ,Route_id,datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DDay   FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Route_id) AS Pro ON Tra.TraRid=Pro.Route_id ) AS t1 LEFT JOIN (SELECT Truck_id AS VDTrkid,Ltr_Cost,Truck_Name,fixed FROM Vehicle_Details WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Fixed=1 ) AS VD ON  t1.TraTid=VD.VDTrkid ) AS  t2 LEFT JOIN  (SELECT Truck_Id AS trpreid,SUM(ISNULL(Tdistance,0)) AS TotDistance FROM TRUCK_PRESENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Pdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Truck_Id) AS Trckpre ON t2.TraTid=Trckpre.trpreid ) AS t6 ON t5.TruckId3=t6.TruckId1 ) AS F1 LEFT JOIN (SELECT Rid,Truck_Id,Route_Name,pcode FROM (SELECT Distinct(Route_Id) AS Rid,Truck_Id FROM Truck_RouteAllotment  Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS TRt1 LEFT JOIN (SELECT Route_ID, CONVERT(Nvarchar(100),Route_ID)+'_'+Route_Name  AS  Route_Name,Plant_Code AS pcode FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Rt2 ON TRt1.Rid=Rt2.Route_ID ) AS F2 ON F1.Truck_id=F2.Truck_Id ) AS FT1 GROUP BY Ft1.pcode) AS Ft5 " +
    " LEFT JOIN " +
    " (SELECT SUM(GetSmltr.Smltr) AS Smltr1,GetSmltr.PPcode AS ppcode1 FROM " +
    " (SELECT ISNULL(Tpr1.Smltr,0) AS Smltr,PPcode FROM " +
    " (SELECT Truck_Id AS TraTid ,Route_Id AS TraRid,Plant_Code AS PPcode  FROM Truck_RouteAllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Ptra1 " +
    " LEFT JOIN " +
    " (SELECT CAST(SUM(ISNULL(Milk_Ltr,0)) AS DECIMAL(18,2)) AS Smltr ,Route_id   FROM PROCUREMENT WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Route_id) AS Tpr1  ON Ptra1.TraTid=Tpr1.Route_id) AS GetSmltr GROUP BY GetSmltr.PPcode) AS Ft6 ON Ft5.pcode=Ft6.ppcode1) AS FT7 ON  Ft3.pcode1=FT7.pcode ) AS Ft8  " +
    " LEFT JOIN (SELECT DD+1 AS DDays FROM (SELECT Top 1 datediff(day, '" + d1.ToString() + "','" + d2.ToString() + "') as DD FROM Truck_Present WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' )  AS D ) AS D1 ON Ft8.pcode1>150";
            }
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }



    protected void Chk_Allplant_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_Allplant.Checked == true)
        {
            Label1.Visible = false;
            ddl_Plantname.Visible = false;
            Panel3.Visible = true;

        }
        else
        {
            Label1.Visible = true;
            ddl_Plantname.Visible = true;
            Panel3.Visible = false;
        }
    }
    private void LoadPlantName1()
    {
        try
        {
            DateTime dtm1 = new DateTime();
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Plant_Name";
                CheckBoxList1.DataValueField = "plant_Code";//ROUTE_ID 
                CheckBoxList1.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void MChk_PlantName_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }
    private void Menu1()
    {
        if (MChk_PlantName.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
        }
    }
    private void CheckBoxListClear()
    {
        CheckBoxList1.Items.Clear();
    }
}