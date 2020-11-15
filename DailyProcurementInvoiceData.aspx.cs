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

public partial class DailyProcurementInvoiceData : System.Web.UI.Page
{
    BLLroutmaster routmasterBL = new BLLroutmaster();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public int rid;
    DateTime dtm = new DateTime();
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
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();

                loadrouteid();
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();

                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                GetprourementIvoiceData();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetprourementIvoiceData();
    }
    private void loadrouteid()
    {
        SqlDataReader dr;


        dr = routmasterBL.getroutmasterdatareader(ccode, pcode);

        ddl_RouteName.Items.Clear();
        ddl_RouteID.Items.Clear();

        while (dr.Read())
        {
            ddl_RouteName.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
            ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());

        }

    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteName.SelectedIndex = ddl_RouteID.SelectedIndex;

    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
    }
    private void GetprourementIvoiceData()
    {
        try
        {
         
            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            cr.Load(Server.MapPath("Crpt_DailyprocurementInvoiceData.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            t1.Text = ccode + "_" + cname;
            t2.Text = pcode + "_" + pname;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            str = "SELECT proAid,prdate,Sessions,ISNULL(Cans,0)AS Cans,ISNULL(Mltr,0)AS Mltr,ISNULL(Milk_kg,0)AS Milk_kg ,ISNULL(Fat,0)AS Fat,ISNULL(Snf,0)AS Snf,ISNULL(CLr,0)AS Clr,proccode,Company_Name,pcode,Plant_Name,Plant_phoneno,Rid,Route_Name,Agent_id,Agent_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS proccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + txt_FromDate.Text.Trim() + "' AND '" + txt_ToDate.Text.Trim() + "' AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "' AND Company_Code='" + ccode + "' ) AS pro LEFT JOIN (SELECT Company_Code AS cccode ,Company_Name FROM Company_Master WHERE Company_Code='" + ccode + "' ) AS comp ON pro.proccode=comp.cccode )AS pc LEFT JOIN (SELECT company_code AS pccode,plant_code,plant_name,plant_address,plant_phoneno FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS plant ON pc.proccode=plant.pccode ) AS pcp ) AS t1 LEFT JOIN (SELECT Agent_id,Agent_Name FROM Agent_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "') AS Agent ON t1.proAid=Agent.Agent_id )AS t2 LEFT JOIN (SELECT Route_id,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "'  AND Route_id='" + rid + "')AS Route ON t2.Rid=Route.Route_id  ORDER BY proAid,prdate,Sessions";           

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
}
