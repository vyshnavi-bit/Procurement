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
using System.IO;


public partial class CompanyPlantwisereport : System.Web.UI.Page
{
    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public string frmdate;
    public string todate;
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
               // CompanywisePlantreport();
               
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
                pcode = Session["Plant_Code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                if (btnvalloanupdate == 1)
                {
                    CompanywisePlantreport();

                }
                else if (btnvalloanupdate == 2)
                {
                    CompanywisePlantreport();
                }
                else
                {
                    CompanywisePlantreport();
                }
               
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }         

        }
    }

    private void CompanywisePlantreport()
    {
        try
        {


            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            cr.Load(Server.MapPath("Crpt_CompanywisePlantreport.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            //CrystalDecisions.CrystalReports.Engine.TextObject t1;
            //CrystalDecisions.CrystalReports.Engine.TextObject t2;
            //CrystalDecisions.CrystalReports.Engine.TextObject t3;
            //CrystalDecisions.CrystalReports.Engine.TextObject t4;

            //t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            //t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            //t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            //t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            //t1.Text = ccode + "_ONEZERO";
            //t2.Text = pcode + "_pondy";
            //t3.Text = "10-11-2012";
            //t4.Text = "To 10-12-2012";

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            // str = "SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '08-17-2012' AND '01-18-2013' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '08-17-2012' AND '01-18-2013' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ";
            str = "SELECT *,(SAmt+Fin2.ComAmt-Dedu) AS Netpay FROM (SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt,(Billadv+Ai+feed+instamt) AS Dedu FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others,ISNULL(instamt,0) AS instamt  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ) AS tt2 LEFT JOIN (SELECT APCode,SUM(ISNULL(Cartage_Amt,0)) AS Cartage_Amt,SUM(ISNULL(SMltr,0)) AS SMltr,SUM(ISNULL(ComAmt,0)) AS ComAmt  FROM (SELECT DISTINCT(AAid) AS Aiid,APCode,Cartage_Amt,SMltr,CAST((Cartage_Amt*SMltr) AS DECIMAL(18,2)) AS ComAmt FROM (SELECT APCode,ISNULL(Cartage_Amt,0) AS Cartage_Amt,t2.Agent_id AS AAid,ISNULL(t3.Mltrr,0) AS mlt FROM (SELECT Plant_Code AS APCode,Agent_id,Cartage_Amt FROM AGENT_MASTER WHERE Type=0 AND Company_Code='1' AND Cartage_Amt>0 ) AS t2 LEFT JOIN (SELECT ppcode,t1.Mltr AS Mltrr,Aid FROM (SELECT Plant_Code AS ppcode,Milk_Ltr AS Mltr,Agent_id AS Aid FROM Procurement AS P WHERE  Prdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND  Company_Code='1' AND Plant_Code IN (SELECT Plant_Code AS APCode FROM  Agent_Master AS A WHERE Type=0 AND Company_Code='1' AND Cartage_Amt>0 AND A.Plant_Code=p.Plant_Code )) AS t1 )AS t3 ON t2.APCode=t3.ppcode) AS t4 LEFT JOIN (SELECT Plant_Code AS p2pcode,SUM(Milk_ltr) AS SMltr,Agent_id AS p2Aid FROM Procurement AS P2 WHERE  Prdate BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND  Company_Code='1' GROUP BY Plant_Code,Agent_id ) AS t5 ON t4.APCode=t5.p2pcode AND AAid=t5.p2Aid )AS fin GROUP BY APCode) AS fin2 ON tt2.pcode=fin2.APCode ";
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;



            //System.IO.MemoryStream stream = (System.IO.MemoryStream)cr.ExportToStream(ExportFormatType.PortableDocFormat);
            //BinaryReader Bin = new BinaryReader(cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(Bin.ReadBytes(Convert.ToInt32(Bin.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();
           
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }
    protected void btn_CompanywisePlantreport_Click(object sender, EventArgs e)
    {
        CompanywisePlantreport();
    }
    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}
