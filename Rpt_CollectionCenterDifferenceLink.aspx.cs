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


public partial class Rpt_CollectionCenterDifferenceLink : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    int ccode = 1, pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string acode1 = string.Empty;
    public static string acode2 = string.Empty;
    string d1 = string.Empty;
    string d2 = string.Empty;
    public static string sd1 = string.Empty;
    public static string sd2 = string.Empty;
    string d11 = string.Empty;
    string d21 = string.Empty;
    DateTime dtt = new DateTime();
    public string Rid = string.Empty;
    public string Aid = string.Empty;
 
    public string frmdate = string.Empty;
    public string Todate = string.Empty;
    int k = 1;

    string addfsecond = "01:01:00";
    string addtsecond = "23:59:59";

    string d4;
    string d5;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    int getcount;
    string agent;
    string cmdass;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
            //    managmobNo = Session["managmobNo"].ToString();
                dtt = System.DateTime.Now;
                txt_FromDate.Text = dtt.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtt.ToString("dd/MM/yyyy");
                Label14.Visible = false;
                ddl_agentid.Visible = false;
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;
               
                Label6.Visible = true;
                ddl_PlantName.Visible = true;

                //Grid info
              
               

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
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;
                //
                Label6.Visible = true;
                ddl_PlantName.Visible = true;         


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

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void loadsingleplant()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void Datefunc()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();        

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            d11 = dt1.ToString("MM/dd/yyyy hh:mm:ss");
            d21 = dt2.ToString("MM/dd/yyyy hh:mm:ss");

            d1 = dt1.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");

            sd1 = dt1.ToString("MM/dd/yyyy");
            sd2 = dt2.ToString("MM/dd/yyyy");


            d4 = d1 + " " + addfsecond;

            d5 = d2 + " " + addtsecond;

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        try
        {

            Datefunc();

            if (CheckBox1.Checked == true)
            {
                Label14.Visible = true;
                ddl_agentid.Visible = true;
                getcount = 1;
              
                GetPlantwiseMilkData1();
                grdLivepro.Visible = false;
                grdLivepro1.Visible = true;

            }
            else
            {
                grdLivepro1.Visible = false;
                grdLivepro.Visible = true;
                getcount = 0;
                GetPlantwiseMilkData();

            }
          // 
           
            gv_Routewisemilk.Visible = false;
            btn_VmccExport.Visible = false;
            btn_Vmccprint.Visible = false;
        }
        catch (Exception ex)
        {

        }
    }

    public void GetPlantwiseMilkData()
    {
        try
        {
            DataTable dt = new DataTable();

            //string cmd = "SELECT Agent_id,CAST(pMilkkg AS DECIMAL(18,2)) AS pMilkkg,CAST(PAfat AS DECIMAL(18,2)) AS PAfat, CAST(PAsnf AS DECIMAL(18,2)) AS PAsnf, CAST(Pfatkg AS DECIMAL(18,2)) AS Pfatkg, CAST(Psnfkg AS DECIMAL(18,2)) AS Psnfkg,CAST(Arate AS DECIMAL(18,2)) AS PArate,CAST( ASamount AS DECIMAL(18,2)) AS pSamount,CAST(CMilkkg AS DECIMAL(18,2)) AS CMilkkg, CAST(CAfat AS DECIMAL(18,2)) AS CAfat, CAST(CAsnf AS DECIMAL(18,2)) AS CAsnf, CAST(Cfatkg AS DECIMAL(18,2)) AS Cfatkg, CAST(Csnfkg AS DECIMAL(18,2)) AS Csnfkg,CAST(Crate AS DECIMAL(18,2)) AS Crate,CAST(CSamount AS DECIMAL(18,2)) AS CSamount,CAST(Diffmkg AS DECIMAL(18,2)) AS Diffmkg, CAST(Difffat AS DECIMAL(18,2)) AS Difffat, CAST(Diffsnf AS DECIMAL(18,2)) AS Diffsnf, CAST(Difffatkg AS DECIMAL(18,2)) AS Difffatkg, CAST(Diffsnfkg AS DECIMAL(18,2)) AS Diffsnfkg,CAST((Arate-Crate) AS DECIMAL(18,2)) AS DiffRate,CAST(DiffSamount AS DECIMAL(18,2)) AS DiffSamount FROM " +
            // " (SELECT f1.Smilkkg AS pMilkkg,f1.Afat AS PAfat,f1.Asnf AS PAsnf,f1.Sfatkg AS Pfatkg,f1.Ssnfkg AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,f1.Samount AS ASamount,f2.Smilkkg AS CMilkkg,f2.Afat AS CAfat,f2.Asnf AS CAsnf,f2.Sfatkg AS Cfatkg,f2.Ssnfkg AS Csnfkg,f2.Samount/(f2.Smilkkg*1.03) AS Crate,f2.Samount AS CSamount,(f2.Smilkkg-f1.Smilkkg) AS Diffmkg,(f2.Afat-f1.Afat) AS Difffat,(f2.Asnf-f1.Asnf) AS Diffsnf,(f2.Sfatkg-f1.Sfatkg) AS Difffatkg,(f2.Ssnfkg-f1.Ssnfkg) AS  Diffsnfkg,(f2.Samount-f1.Samount) AS DiffSamount,f1.Agent_id  FROM " +
            //  " (SELECT SUM(Milk_kg)AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id FROM procurement Where Plant_Code='" + pcode + "' AND ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "') or  (prdate BETWEEN '" + d4 + "' AND '" + d5 + "') )  GROUP BY agent_id) AS f1 " +
            //  " LEFT JOIN " +
            // " (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code,Sum(Samount) AS Samount FROM " +
            // " (SELECT CONVERT(float,milk_kg) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,Agent_id AS agent_code,Amount AS Samount FROM ProducerProcurement WHERE plant_code='" + pcode + "' AND  ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "') or  (prdate BETWEEN '" + d4 + "' AND '" + d5 + "') )) AS t2 Group by t2.agent_code) AS f2 ON f1.Agent_id=f2.agent_code ) AS f3  WHERE f3.CMilkkg IS NOT NULL ORDER BY f3.Agent_id ";

            //string cmd = "SELECT Agent_id,CAST(pMilkkg AS DECIMAL(18,2)) AS pMilkkg,CAST(PAfat AS DECIMAL(18,2)) AS PAfat, CAST(PAsnf AS DECIMAL(18,2)) AS PAsnf, CAST(Pfatkg AS DECIMAL(18,2)) AS Pfatkg, CAST(Psnfkg AS DECIMAL(18,2)) AS Psnfkg,CAST(Arate AS DECIMAL(18,2)) AS PArate,CAST( ASamount AS DECIMAL(18,2)) AS pSamount,CAST(CMilkkg AS DECIMAL(18,2)) AS CMilkkg, CAST(CAfat AS DECIMAL(18,2)) AS CAfat, CAST(CAsnf AS DECIMAL(18,2)) AS CAsnf, CAST(Cfatkg AS DECIMAL(18,2)) AS Cfatkg, CAST(Csnfkg AS DECIMAL(18,2)) AS Csnfkg,CAST(Crate AS DECIMAL(18,2)) AS Crate,CAST(CSamount AS DECIMAL(18,2)) AS CSamount,CAST(Diffmkg AS DECIMAL(18,2)) AS Diffmkg, CAST(Difffat AS DECIMAL(18,2)) AS Difffat, CAST(Diffsnf AS DECIMAL(18,2)) AS Diffsnf, CAST(Difffatkg AS DECIMAL(18,2)) AS Difffatkg, CAST(Diffsnfkg AS DECIMAL(18,2)) AS Diffsnfkg,CAST((Arate-Crate) AS DECIMAL(18,2)) AS DiffRate,CAST(DiffSamount AS DECIMAL(18,2)) AS DiffSamount FROM " +
            //" (SELECT f1.Smilkkg AS pMilkkg,f1.Afat AS PAfat,f1.Asnf AS PAsnf,f1.Sfatkg AS Pfatkg,f1.Ssnfkg AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,f1.Samount AS ASamount,f2.Smilkkg AS CMilkkg,f2.Afat AS CAfat,f2.Asnf AS CAsnf,f2.Sfatkg AS Cfatkg,f2.Ssnfkg AS Csnfkg,f2.Samount/(f2.Smilkkg*1.03) AS Crate,f2.Samount AS CSamount,(f1.Smilkkg -f2.Smilkkg) AS Diffmkg,(f1.Afat - f2.Afat) AS Difffat,(f1.Asnf - f2.Asnf) AS Diffsnf,(f1.Sfatkg - f2.Sfatkg) AS Difffatkg,(f1.Ssnfkg - f2.Ssnfkg) AS  Diffsnfkg,(f1.Samount - f2.Samount-) AS DiffSamount,f1.Agent_id  FROM " +
            // " (SELECT SUM(Milk_ltr) AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id FROM procurement Where Plant_Code='" + pcode + "' AND ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  GROUP BY agent_id) AS f1 " +
            // " LEFT JOIN " + 
            //" (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code,Sum(Samount) AS Samount FROM " +
            //" (SELECT CONVERT(float,Milk_ltr) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,Agent_id AS agent_code,Amount AS Samount FROM ProducerProcurement WHERE plant_code='" + pcode + "' AND  ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )) AS t2 Group by t2.agent_code) AS f2 ON f1.Agent_id=f2.agent_code ) AS f3  WHERE f3.CMilkkg IS NOT NULL ORDER BY f3.Agent_id ";


            string cmd="SELECT Agent_id,CAST(pMilkkg AS DECIMAL(18,2)) AS pMilkkg,CAST(PAfat AS DECIMAL(18,2)) AS PAfat, CAST(PAsnf AS DECIMAL(18,2)) AS PAsnf, CAST(Pfatkg AS DECIMAL(18,2)) AS Pfatkg, CAST(Psnfkg AS DECIMAL(18,2)) AS Psnfkg,CAST(Arate AS DECIMAL(18,2)) AS PArate,CAST( ASamount AS DECIMAL(18,2)) AS pSamount,CAST(CMilkkg AS DECIMAL(18,2)) AS CMilkkg, CAST(CAfat AS DECIMAL(18,2)) AS CAfat, CAST(CAsnf AS DECIMAL(18,2)) AS CAsnf, CAST(Cfatkg AS DECIMAL(18,2)) AS Cfatkg, CAST(Csnfkg AS DECIMAL(18,2)) AS Csnfkg,CAST(Crate AS DECIMAL(18,2)) AS Crate,CAST(CSamount AS DECIMAL(18,2)) AS CSamount,CAST(Diffmkg AS DECIMAL(18,2)) AS Diffmkg, CAST(Difffat AS DECIMAL(18,2)) AS Difffat, CAST(Diffsnf AS DECIMAL(18,2)) AS Diffsnf, CAST(Difffatkg AS DECIMAL(18,2)) AS Difffatkg, CAST(Diffsnfkg AS DECIMAL(18,2)) AS Diffsnfkg,CAST((Arate-Crate) AS DECIMAL(18,2)) AS DiffRate,CAST(DiffSamount AS DECIMAL(18,2)) AS DiffSamount FROM " +
            " (SELECT f1.Smilkkg AS pMilkkg,f1.Afat AS PAfat,f1.Asnf AS PAsnf,f1.Sfatkg AS Pfatkg,f1.Ssnfkg AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,f1.Samount AS ASamount,f2.Smilkkg AS CMilkkg,f2.Afat AS CAfat,f2.Asnf AS CAsnf,f2.Sfatkg AS Cfatkg,f2.Ssnfkg AS Csnfkg,f2.Samount/(f2.Smilkkg*1.03) AS Crate,f2.Samount AS CSamount,(f1.Smilkkg - f2.Smilkkg) AS Diffmkg,(f1.Afat - f2.Afat) AS Difffat,(f1.Asnf- f2.Asnf) AS Diffsnf,(f1.Sfatkg - f2.Sfatkg) AS Difffatkg,(f1.Ssnfkg- f2.Ssnfkg) AS  Diffsnfkg,(f1.Samount - f2.Samount) AS DiffSamount,f1.Agent_id  FROM " +
             " (SELECT SUM(Milk_ltr)AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id FROM procurement Where Plant_Code='" + pcode + "' AND ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  GROUP BY agent_id) AS f1 " +
             " LEFT JOIN " +
            " (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code,Sum(Samount) AS Samount FROM " +
            " (SELECT CONVERT(float,Milk_ltr) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,Agent_id AS agent_code,Amount AS Samount FROM ProducerProcurement WHERE plant_code='" + pcode + "' AND  ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )) AS t2 Group by t2.agent_code) AS f2 ON f1.Agent_id=f2.agent_code ) AS f3  WHERE f3.CMilkkg IS NOT NULL ORDER BY f3.Agent_id ";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;

            if (dt.Rows.Count > 0)
            {
                grdLivepro.DataSource = dt;
                grdLivepro.DataBind();

                grdLivepro.FooterRow.Cells[1].Text = "Total";
                grdLivepro.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                decimal pMilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMilkkg"));
                grdLivepro.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[3].Text = pMilkkg.ToString("N2");

                decimal PAfat = dt.AsEnumerable().Average(row => row.Field<decimal>("CAfat"));
                grdLivepro.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[4].Text = PAfat.ToString("N2");

                decimal PAsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("CAsnf"));
                grdLivepro.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[5].Text = PAsnf.ToString("N2");


                decimal Pfatkg = dt.AsEnumerable().Average(row => row.Field<decimal>("Crate"));
                grdLivepro.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[6].Text = Pfatkg.ToString("N2");

                decimal Psnfkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CSamount"));
                grdLivepro.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[7].Text = Psnfkg.ToString("N2");


                decimal CMilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("pMilkkg"));
                grdLivepro.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[8].Text = CMilkkg.ToString("N2");

                decimal CAfat = dt.AsEnumerable().Average(row => row.Field<decimal>("PAfat"));
                grdLivepro.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[9].Text = CAfat.ToString("N2");

                decimal CAsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("PAsnf"));
                grdLivepro.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[10].Text = CAsnf.ToString("N2");

                decimal Cfatkg = dt.AsEnumerable().Average(row => row.Field<decimal>("PArate"));
                grdLivepro.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[11].Text = Cfatkg.ToString("N2");

                decimal Csnfkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("pSamount"));
                grdLivepro.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[12].Text = Csnfkg.ToString("N2");


                decimal diffkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Diffmkg"));
                grdLivepro.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[13].Text = diffkg.ToString("N2");

                decimal difffat = dt.AsEnumerable().Average(row => row.Field<decimal>("Difffat"));
                grdLivepro.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[14].Text = difffat.ToString("N2");

                decimal diffsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("Diffsnf"));
                grdLivepro.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[15].Text = diffsnf.ToString("N2");

                decimal diffrate = dt.AsEnumerable().Average(row => row.Field<decimal>("DiffRate"));
                grdLivepro.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[16].Text = diffrate.ToString("N2");

                decimal diffamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("DiffSamount"));
                grdLivepro.FooterRow.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro.FooterRow.Cells[17].Text = diffamount.ToString("N2");





            }
            else
            {
                dt = null;
                grdLivepro.DataSource = dt;
                grdLivepro.DataBind();
            }
        }

        catch (Exception ex)
        {
        }
    }

    public void GetPlantwiseMilkData1()
    {
        try
        {
            DataTable dt = new DataTable();





            //string cmd = "SELECT Agent_id,distinct(prdate) as prdate,CAST(pMilkkg AS DECIMAL(18,2)) AS pMilkkg,CAST(PAfat AS DECIMAL(18,2)) AS PAfat, CAST(PAsnf AS DECIMAL(18,2)) AS PAsnf, CAST(Pfatkg AS DECIMAL(18,2)) AS Pfatkg, CAST(Psnfkg AS DECIMAL(18,2)) AS Psnfkg,CAST(Arate AS DECIMAL(18,2)) AS PArate,CAST( ASamount AS DECIMAL(18,2)) AS pSamount,CAST(CMilkkg AS DECIMAL(18,2)) AS CMilkkg, CAST(CAfat AS DECIMAL(18,2)) AS CAfat, CAST(CAsnf AS DECIMAL(18,2)) AS CAsnf, CAST(Cfatkg AS DECIMAL(18,2)) AS Cfatkg, CAST(Csnfkg AS DECIMAL(18,2)) AS Csnfkg,CAST(Crate AS DECIMAL(18,2)) AS Crate,CAST(CSamount AS DECIMAL(18,2)) AS CSamount,CAST(Diffmkg AS DECIMAL(18,2)) AS Diffmkg, CAST(Difffat AS DECIMAL(18,2)) AS Difffat, CAST(Diffsnf AS DECIMAL(18,2)) AS Diffsnf, CAST(Difffatkg AS DECIMAL(18,2)) AS Difffatkg, CAST(Diffsnfkg AS DECIMAL(18,2)) AS Diffsnfkg,CAST((Arate-Crate) AS DECIMAL(18,2)) AS DiffRate,CAST(DiffSamount AS DECIMAL(18,2)) AS DiffSamount FROM " +
            // " (SELECT f1.Smilkkg AS pMilkkg,f1.Afat AS PAfat,f1.Asnf AS PAsnf,f1.Sfatkg AS Pfatkg,f1.Ssnfkg AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,f1.Samount AS ASamount,f2.Smilkkg AS CMilkkg,f2.Afat AS CAfat,f2.Asnf AS CAsnf,f2.Sfatkg AS Cfatkg,f2.Ssnfkg AS Csnfkg,f2.Samount/(f2.Smilkkg*1.03) AS Crate,f2.Samount AS CSamount,(f2.Smilkkg-f1.Smilkkg) AS Diffmkg,(f2.Afat-f1.Afat) AS Difffat,(f2.Asnf-f1.Asnf) AS Diffsnf,(f2.Sfatkg-f1.Sfatkg) AS Difffatkg,(f2.Ssnfkg-f1.Ssnfkg) AS  Diffsnfkg,(f2.Samount-f1.Samount) AS DiffSamount,f1.Agent_id  FROM " +
            //  " (SELECT SUM(Milk_kg)AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id FROM procurement Where Plant_Code='" + pcode + "'   and agent_id='" + ddl_agentid.Text + "' AND ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "') or  (prdate BETWEEN '" + d4 + "' AND '" + d5 + "') )  GROUP BY agent_id) AS f1 " +
            //  " LEFT JOIN " +
            // " (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code,Sum(Samount) AS Samount FROM " +
            // " (SELECT CONVERT(float,milk_kg) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,Agent_id AS agent_code,Amount AS Samount FROM ProducerProcurement WHERE plant_code='" + pcode + "'   and agent_id='" + ddl_agentid.Text + "'  AND  ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "') or  (prdate BETWEEN '" + d4 + "' AND '" + d5 + "') )) AS t2 Group by t2.agent_code) AS f2 ON f1.Agent_id=f2.agent_code ) AS f3  WHERE f3.CMilkkg IS NOT NULL ORDER BY f3.Agent_id ";


          //  string cmd = "select CC.PRDATE AS prdate,sum(cMilkkg) as cMilkkg,sum(cAfat) as cAfat,sum(cAsnf) as cAsnf,sum(cSamount) as cSamount,sum(Milkkg) Milkkg,sum(Afat) as Afat,sum(Asnf) as Asnf,sum(pSamount)  pSamount,sum(Milkkg-cMilkkg) as diffkg,sum(Afat-cAfat) as difffat,sum(Asnf-cAsnf) as diffsnf,sum( pSamount- cSamount) as diffamount           from (select PRDATE ,cast(Milkkg as decimal(18,2)) as Milkkg,cast(Afat as decimal(18,2)) as Afat,cast(Asnf as decimal(18,2)) as Asnf,cast(Sfatkg as decimal(18,2)) as pSfatkg,cast(Ssnfkg as decimal(18,2)) AS pSsnfkg,cast(Samount as decimal(18,2)) as pSamount   from (select  prdate ,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(milk_kg) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_kg)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from Procurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "') or  (prdate BETWEEN '" + d4 + "' AND '" + d5 + "') )  and Agent_id='" + ddl_agentid.Text + "'  group by plant_code,prdate) as aa  group by prdate) as bb  ) as cc left  join (select PRDATE,cast(Milkkg as decimal(18,2)) as cMilkkg,cast(Afat as decimal(18,2)) as cAfat,cast(Asnf as decimal(18,2)) as cAsnf,cast(Sfatkg as decimal(18,2)) as cSfatkg,cast(Ssnfkg as decimal(18,2)) AS cSsnfkg,cast(Samount as decimal(18,2)) as cSamount   from (select prdate,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(milk_kg) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_kg)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from ProducerProcurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "') or  (prdate BETWEEN '" + d4 + "' AND '" + d5 + "') )  and Agent_id='" + ddl_agentid.Text + "'  group by plant_code,prdate) as ss  group by prdate) as dd   )  as ee on cc.PRDATE=ee.PRDATE    group by ee.PRDATE,CC.PRDATE";

           // string cmd = "select CC.PRDATE AS prdate,sum(cMilkkg) as cMilkltr,sum(cAfat) as cAfat,sum(cAsnf) as cAsnf,sum(cSamount) as cSamount,sum(milk_ltr) Milkltr,sum(Afat) as Afat,sum(Asnf) as Asnf,sum(pSamount)  pSamount,sum(Milkkg-cMilkkg) as DiffMltr,sum(Afat-cAfat) as difffat,sum(Asnf-cAsnf) as diffsnf,sum( pSamount- cSamount) as diffamount           from (select PRDATE ,cast(Milkkg as decimal(18,2)) as Milkkg,cast(Afat as decimal(18,2)) as Afat,cast(Asnf as decimal(18,2)) as Asnf,cast(Sfatkg as decimal(18,2)) as pSfatkg,cast(Ssnfkg as decimal(18,2)) AS pSsnfkg,cast(Samount as decimal(18,2)) as pSamount   from (select  prdate ,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from Procurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  and Agent_id='" + ddl_agentid.Text + "'  group by plant_code,prdate) as aa  group by prdate) as bb  ) as cc left  join (select PRDATE,cast(Milkkg as decimal(18,2)) as cMilkkg,cast(Afat as decimal(18,2)) as cAfat,cast(Asnf as decimal(18,2)) as cAsnf,cast(Sfatkg as decimal(18,2)) as cSfatkg,cast(Ssnfkg as decimal(18,2)) AS cSsnfkg,cast(Samount as decimal(18,2)) as cSamount   from (select prdate,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from ProducerProcurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  and Agent_id='" + ddl_agentid.Text + "'  group by plant_code,prdate) as ss  group by prdate) as dd   )  as ee on cc.PRDATE=ee.PRDATE    group by ee.PRDATE,CC.PRDATE";
           // string cmd = "select CC.PRDATE AS prdate,sum(cMilkkg) as cMilkltr,sum(cAfat) as cAfat,sum(cAsnf) as cAsnf,sum(cSamount) as cSamount,sum(Milkkg) Milkltr,sum(Afat) as Afat,sum(Asnf) as Asnf,sum(pSamount)  pSamount,sum(Milkkg-cMilkkg) as DiffMltr,sum(Afat-cAfat) as difffat,sum(Asnf-cAsnf) as diffsnf,sum( pSamount- cSamount) as diffamount           from (select PRDATE ,cast(Milkkg as decimal(18,2)) as Milkkg,cast(Afat as decimal(18,2)) as Afat,cast(Asnf as decimal(18,2)) as Asnf,cast(Sfatkg as decimal(18,2)) as pSfatkg,cast(Ssnfkg as decimal(18,2)) AS pSsnfkg,cast(Samount as decimal(18,2)) as pSamount   from (select  prdate ,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from Procurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' and Agent_id='" + ddl_agentid.Text + "'  group by plant_code,prdate) as aa  group by prdate) as bb  ) as cc left  join (select PRDATE,cast(Milkkg as decimal(18,2)) as cMilkkg,cast(Afat as decimal(18,2)) as cAfat,cast(Asnf as decimal(18,2)) as cAsnf,cast(Sfatkg as decimal(18,2)) as cSfatkg,cast(Ssnfkg as decimal(18,2)) AS cSsnfkg,cast(Samount as decimal(18,2)) as cSamount   from (select prdate,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_ltr)))/100 AS snfkg,sum(Amount) AS Samount  from ProducerProcurement    where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  and Agent_id='" + ddl_agentid.Text + "' group by plant_code,prdate) as ss  group by prdate) as dd   )  as ee on cc.PRDATE=ee.PRDATE    group by ee.PRDATE,CC.PRDATE";     
            //string cmd = "select CC.PRDATE AS prdate,sum(cMilkkg) as cMilkltr,sum(cAfat) as cAfat,sum(cAsnf) as cAsnf,sum(cSamount) as cSamount,sum(Milkkg) Milkltr,sum(Afat) as Afat,sum(Asnf) as Asnf,sum(pSamount)  pSamount,sum(Milkkg-cMilkkg) as DiffMltr,sum(Afat-cAfat) as difffat,sum(Asnf-cAsnf) as diffsnf,sum( pSamount- cSamount) as diffamount           from (select PRDATE ,cast(Milkkg as decimal(18,2)) as Milkkg,cast(Afat as decimal(18,2)) as Afat,cast(Asnf as decimal(18,2)) as Asnf,cast(Sfatkg as decimal(18,2)) as pSfatkg,cast(Ssnfkg as decimal(18,2)) AS pSsnfkg,cast(Samount as decimal(18,2)) as pSamount   from (select  prdate ,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from Procurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "'  AND '" + d21.ToString().Trim() + "' )) and Agent_id='" + ddl_agentid.Text + "'  group by plant_code,prdate) as aa  group by prdate) as bb  ) as cc left  join (select PRDATE,cast(Milkkg as decimal(18,2)) as cMilkkg,cast(Afat as decimal(18,2)) as cAfat,cast(Asnf as decimal(18,2)) as cAsnf,cast(Sfatkg as decimal(18,2)) as cSfatkg,cast(Ssnfkg as decimal(18,2)) AS cSsnfkg,cast(Samount as decimal(18,2)) as cSamount   from (select prdate,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_ltr)))/100 AS snfkg,sum(Amount) AS Samount  from ProducerProcurement    where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  and Agent_id='" + ddl_agentid.Text + "' group by plant_code,prdate) as ss  group by prdate) as dd   )  as ee on cc.PRDATE=ee.PRDATE    group by ee.PRDATE,CC.PRDATE";     

            // // select CC.PRDATE AS prdate,sum(cMilkkg) as cMilkltr,sum(cAfat) as cAfat,sum(cAsnf) as cAsnf,sum(cSamount) as cSamount,sum(Milkkg) Milkltr,sum(Afat) as Afat,sum(Asnf) as Asnf,sum(pSamount)  pSamount,sum(Milkkg-cMilkkg) as DiffMltr,sum(Afat-cAfat) as difffat,sum(Asnf-cAsnf) as diffsnf,sum( pSamount- cSamount) as diffamount           from (select PRDATE ,cast(Milkkg as decimal(18,2)) as Milkkg,cast(Afat as decimal(18,2)) as Afat,cast(Asnf as decimal(18,2)) as Asnf,cast(Sfatkg as decimal(18,2)) as pSfatkg,cast(Ssnfkg as decimal(18,2)) AS pSsnfkg,cast(Samount as decimal(18,2)) as pSamount   from (select  prdate ,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from Procurement   where plant_code='158'  and   ((prdate BETWEEN '02/01/2016' AND '02/02/2016') or (prdate BETWEEN '02/01/2016 12:00:00' AND '02/02/2016 12:00:00')  )  and Agent_id='475'  group by plant_code,prdate) as aa  group by prdate) as bb  ) as cc left  join (select PRDATE,cast(Milkkg as decimal(18,2)) as cMilkkg,cast(Afat as decimal(18,2)) as cAfat,cast(Asnf as decimal(18,2)) as cAsnf,cast(Sfatkg as decimal(18,2)) as cSfatkg,cast(Ssnfkg as decimal(18,2)) AS cSsnfkg,cast(Samount as decimal(18,2)) as cSamount   from (select prdate,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_ltr)))/100 AS snfkg,sum(Amount) AS Samount  from ProducerProcurement   where plant_code='158'  and   ((prdate BETWEEN '02/01/2016' AND '02/02/2016') or (prdate BETWEEN '02/01/2016 12:00:00' AND '02/02/2016 12:00:00')  )  and Agent_id='475'  group by plant_code,prdate) as ss  group by prdate) as dd   )  as ee on cc.PRDATE=ee.PRDATE    group by ee.PRDATE,CC.PRDATE     
            //SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);




            agent = ddl_agentid.Text.ToString();
            int len = agent.Length;
            string assagent;
            string addagent;

            if (len == 3)
            {
                assagent = ddl_agentid.Text.ToString();
                addagent = "0" + ddl_agentid.Text.ToString();
                cmdass = "select CC.PRDATE AS prdate,sum(cMilkkg) as cMilkltr,sum(cAfat) as cAfat,sum(cAsnf) as cAsnf,sum(cSamount) as cSamount,sum(Milkkg) Milkltr,sum(Afat) as Afat,sum(Asnf) as Asnf,sum(pSamount)  pSamount,sum(Milkkg-cMilkkg) as DiffMltr,sum(Afat-cAfat) as difffat,sum(Asnf-cAsnf) as diffsnf,sum( pSamount- cSamount) as diffamount           from (select PRDATE ,cast(Milkkg as decimal(18,2)) as Milkkg,cast(Afat as decimal(18,2)) as Afat,cast(Asnf as decimal(18,2)) as Asnf,cast(Sfatkg as decimal(18,2)) as pSfatkg,cast(Ssnfkg as decimal(18,2)) AS pSsnfkg,cast(Samount as decimal(18,2)) as pSamount   from (select  prdate ,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from Procurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "'  AND '" + d21.ToString().Trim() + "' )) and ((Agent_id='" + assagent + "') or (Agent_id='" + addagent + "'))  group by plant_code,prdate) as aa  group by prdate) as bb  ) as cc left  join (select PRDATE,cast(Milkkg as decimal(18,2)) as cMilkkg,cast(Afat as decimal(18,2)) as cAfat,cast(Asnf as decimal(18,2)) as cAsnf,cast(Sfatkg as decimal(18,2)) as cSfatkg,cast(Ssnfkg as decimal(18,2)) AS cSsnfkg,cast(Samount as decimal(18,2)) as cSamount   from (select prdate,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_ltr)))/100 AS snfkg,sum(Amount) AS Samount  from ProducerProcurement    where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  and ((Agent_id='" + assagent + "') or (Agent_id='" + addagent + "')) group by plant_code,prdate) as ss  group by prdate) as dd   )  as ee on cc.PRDATE=ee.PRDATE    group by ee.PRDATE,CC.PRDATE";
            }
            if (len == 4)
            {
                assagent = ddl_agentid.Text.ToString();
                cmdass = "select CC.PRDATE AS prdate,sum(cMilkkg) as cMilkltr,sum(cAfat) as cAfat,sum(cAsnf) as cAsnf,sum(cSamount) as cSamount,sum(Milkkg) Milkltr,sum(Afat) as Afat,sum(Asnf) as Asnf,sum(pSamount)  pSamount,sum(Milkkg-cMilkkg) as DiffMltr,sum(Afat-cAfat) as difffat,sum(Asnf-cAsnf) as diffsnf,sum( pSamount- cSamount) as diffamount           from (select PRDATE ,cast(Milkkg as decimal(18,2)) as Milkkg,cast(Afat as decimal(18,2)) as Afat,cast(Asnf as decimal(18,2)) as Asnf,cast(Sfatkg as decimal(18,2)) as pSfatkg,cast(Ssnfkg as decimal(18,2)) AS pSsnfkg,cast(Samount as decimal(18,2)) as pSamount   from (select  prdate ,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_kg)))/100 AS snfkg,sum(Amount) AS Samount  from Procurement   where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "'  AND '" + d21.ToString().Trim() + "' )) and ((Agent_id='" + assagent + "') or (Agent_id='" + assagent + "'))  group by plant_code,prdate) as aa  group by prdate) as bb  ) as cc left  join (select PRDATE,cast(Milkkg as decimal(18,2)) as cMilkkg,cast(Afat as decimal(18,2)) as cAfat,cast(Asnf as decimal(18,2)) as cAsnf,cast(Sfatkg as decimal(18,2)) as cSfatkg,cast(Ssnfkg as decimal(18,2)) AS cSsnfkg,cast(Samount as decimal(18,2)) as cSamount   from (select prdate,sum(cast(Milkkg as decimal(18,2))) as Milkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,Sum(Samount) AS Samount  from(Select sum(Milk_ltr) as milk_kg ,convert(varchar,prdate,103) as prdate,sum(CONVERT(float,milk_ltr)) AS Milkkg,sum(CONVERT(float,fat)) AS fat,sum(CONVERT(float,snf)) AS snf,sum((CONVERT(float,fat)* CONVERT(float,milk_kg)))/100 AS fatkg,sum((CONVERT(float,snf)* CONVERT(float,milk_ltr)))/100 AS snfkg,sum(Amount) AS Samount  from ProducerProcurement    where plant_code='" + pcode + "'  and   ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')  )  and ((Agent_id='" + assagent + "') or (Agent_id='" + assagent + "')) group by plant_code,prdate) as ss  group by prdate) as dd   )  as ee on cc.PRDATE=ee.PRDATE    group by ee.PRDATE,CC.PRDATE";
            }


            SqlDataAdapter adp = new SqlDataAdapter(cmdass, connStr);



            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;

            if (dt.Rows.Count > 0)
            {
                grdLivepro1.DataSource = dt;
                grdLivepro1.DataBind();

                grdLivepro1.FooterRow.Cells[1].Text = "Total";
                grdLivepro1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                decimal pMilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("cMilkltr"));
                grdLivepro1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[2].Text = pMilkkg.ToString("N2");

                decimal PAfat = dt.AsEnumerable().Average(row => row.Field<decimal>("CAfat"));
                grdLivepro1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[3].Text = PAfat.ToString("N2");

                decimal PAsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("CAsnf"));
                grdLivepro1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[4].Text = PAsnf.ToString("N2");


                //decimal Pfatkg = dt.AsEnumerable().Average(row => row.Field<decimal>("Crate"));
                //grdLivepro.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //grdLivepro.FooterRow.Cells[6].Text = Pfatkg.ToString("N2");

                decimal Psnfkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CSamount"));
                grdLivepro1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[5].Text = Psnfkg.ToString("N2");


                decimal CMilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkltr"));
                grdLivepro1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[6].Text = CMilkkg.ToString("N2");

                decimal CAfat = dt.AsEnumerable().Average(row => row.Field<decimal>("Afat"));
                grdLivepro1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[7].Text = CAfat.ToString("N2");

                decimal CAsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("Asnf"));
                grdLivepro1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[8].Text = CAsnf.ToString("N2");

                //decimal Cfatkg = dt.AsEnumerable().Average(row => row.Field<decimal>("PArate"));
                //grdLivepro.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                //grdLivepro.FooterRow.Cells[11].Text = Cfatkg.ToString("N2");

                decimal Csnfkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("pSamount"));
                grdLivepro1.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[9].Text = Csnfkg.ToString("N2");




                decimal diffkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("DiffMltr"));
                grdLivepro1.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[10].Text = diffkg.ToString("N2");

                decimal difffat = dt.AsEnumerable().Average(row => row.Field<decimal>("difffat"));
                grdLivepro1.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[11].Text = difffat.ToString("N2");

                decimal diffsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("diffsnf"));
                grdLivepro1.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[12].Text = diffsnf.ToString("N2");

                //decimal diffrate = dt.AsEnumerable().Average(row => row.Field<decimal>("DiffRate"));
                //grdLivepro1.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                //grdLivepro1.FooterRow.Cells[13].Text = diffrate.ToString("N2");

                decimal diffamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("diffamount"));
                grdLivepro1.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                grdLivepro1.FooterRow.Cells[13].Text = diffamount.ToString("N2");




            }
            else
            {
                dt = null;
                grdLivepro1.DataSource = dt;
                grdLivepro1.DataBind();
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            Datefunc();
            string filename;
            if (CheckBox1.Checked == true)
            {

                Response.Clear();
                Response.Buffer = true;
                 filename = "'" +ddl_PlantName.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";

                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    grdLivepro1.AllowPaging = false;
                    GetPlantwiseMilkData1();

                    grdLivepro1.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grdLivepro1.HeaderRow.Cells)
                    {
                        cell.BackColor = grdLivepro1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grdLivepro1.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grdLivepro1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grdLivepro1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grdLivepro1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                    // string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }

            if (CheckBox1.Checked == false)
            {

                Datefunc();
                Response.Clear();
                Response.Buffer = true;
                filename = "'" + ddl_PlantName.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";

                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    grdLivepro.AllowPaging = false;
                    GetPlantwiseMilkData();

                    grdLivepro.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grdLivepro.HeaderRow.Cells)
                    {
                        cell.BackColor = grdLivepro.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grdLivepro.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grdLivepro.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grdLivepro.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grdLivepro.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                    // string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();





                }
            }


        }


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            //Grid info   
            Datefunc();
           
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            acode1 = grdrow.Cells[1].Text;
            acode2 = grdrow.Cells[1].Text;
            GetAgenwiseMilkDetails(ccode.ToString(), acode2);     

            gv_Routewisemilk.Visible = true;
            btn_VmccExport.Visible = true;
            btn_Vmccprint.Visible = true;
           
        }
        catch (Exception ex)
        {

        }
    }

    public void GetAgenwiseMilkDetails(string ccode, string acode1)
    {
        try
        {
            DataTable dt = new DataTable();

    //        string cmd = "SELECT  CAST(f1.Smilkkg AS DECIMAL(18,2))  AS pMilkkg, CAST(f1.Afat AS DECIMAL(18,2)) AS PAfat, CAST(f1.Asnf AS DECIMAL(18,2))  AS PAsnf, CAST(f1.Sfatkg AS DECIMAL(18,2)) AS Pfatkg, CAST(f1.Ssnfkg AS DECIMAL(18,2)) AS Psnfkg,f1.Samount/(f1.Smilkkg*1.03) AS Arate,CAST(f2.Smilkkg AS DECIMAL(18,2))  AS CMilkkg,CAST(f2.Afat AS DECIMAL(18,2))  AS CAfat,CAST(f2.Asnf AS DECIMAL(18,2))  AS CAsnf,CAST(f2.Sfatkg AS DECIMAL(18,2))  AS Cfatkg,CAST(f2.Ssnfkg AS DECIMAL(18,2))  AS Csnfkg,CAST((f1.Smilkkg-f2.Smilkkg) AS DECIMAL(18,2))  AS Diffmkg,CAST((f1.Afat-f2.Afat) AS DECIMAL(18,2))  AS Difffat,CAST((f1.Asnf-f2.Asnf) AS DECIMAL(18,2))  AS Diffsnf,CAST((f1.Sfatkg-f2.Sfatkg) AS DECIMAL(18,2))  AS Difffatkg,CAST((f1.Ssnfkg-f2.Ssnfkg) AS DECIMAL(18,2))  AS  Diffsnfkg,f1.Prdate AS pdate,f1.Sessions AS sess,f1.Agent_id  FROM " +
    // " (SELECT SUM(Milk_kg)AS Smilkkg,(SUM(fat_kg)*100)/SUM(milk_kg) AS Afat,(SUM(snf_kg)*100)/SUM(milk_kg) AS Asnf,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg ,SUM(Amount) AS Samount,Agent_id,Sessions,CONVERT(nvarchar(12),prdate,103) AS Prdate FROM procurement Where Plant_Code='" + pcode + "' AND prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' AND Agent_id='" + acode2.Trim() + "' GROUP BY agent_id,sessions,Prdate) AS f1 " +
    // " LEFT JOIN  " +
    //" (SELECT SUM(Milkkg) AS Smilkkg,((SUM(fatkg)*100)/SUM(Milkkg)) AS Afat,((SUM(snfkg)*100)/SUM(Milkkg)) AS Asnf,SUM(fatkg) AS Sfatkg,Sum(snfkg) AS Ssnfkg,agent_code,shift,prdate FROM " +
    //" (SELECT CONVERT(float,milk_kg) AS Milkkg,CONVERT(float,fat) AS fat,CONVERT(float,snf) AS snf,(CONVERT(float,fat)* CONVERT(float,milk_kg))/100 AS fatkg,(CONVERT(float,snf)* CONVERT(float,milk_kg))/100 AS snfkg,plant_code,agent_code,shift,CONVERT(nvarchar(12),prdate,103) AS Prdate FROM VMCCDPU WHERE plant_code='" + pcode + "' AND agent_code='" + acode2.Trim() + "' AND  prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "') AS t2 Group by t2.agent_code,t2.shift,t2.prdate) AS f2 ON f1.Agent_id=f2.agent_code AND f1.Sessions=f2.shift AND f1.Prdate=f2.prdate";


            string cmd = "SELECT Producer_Id AS producer_code,CONVERT(nvarchar(12),prdate,103) AS prdate,Sessions AS sess,CAST((CONVERT(float,milk_ltr)) AS DECIMAL(18,2)) AS CMilkltr,CAST(( CONVERT(float,fat)) AS DECIMAL(18,1)) AS CAfat,CAST(( CONVERT(float,snf)) AS DECIMAL(18,1)) AS CAsnf,CAST(((CONVERT(float,fat)* CONVERT(float,milk_kg))/100) AS DECIMAL(18,3))  AS Cfatkg,CAST(((CONVERT(float,snf)* CONVERT(float,milk_kg))/100) AS DECIMAL(18,2))  AS Csnfkg,CAST(( CONVERT(float,Rate)) AS DECIMAL(18,1)) AS Rate,CAST(( CONVERT(float,Amount)) AS DECIMAL(18,2)) Amount FROM ProducerProcurement WHERE plant_code='" + pcode + "' AND Agent_id='" + acode2.Trim() + "' AND  ((prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') or (prdate BETWEEN '" + d11.ToString().Trim() + "' AND '" + d21.ToString().Trim() + "')) Order By Producer_Id,prdate,Sessions";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;

            if (dt.Rows.Count > 0)
            {
                gv_Routewisemilk.DataSource = dt;
                gv_Routewisemilk.DataBind();

                gv_Routewisemilk.FooterRow.Cells[1].Text = "Total";
                gv_Routewisemilk.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                decimal pMilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMilkltr"));
                gv_Routewisemilk.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                gv_Routewisemilk.FooterRow.Cells[4].Text = pMilkkg.ToString("N2");

                decimal PAfat = dt.AsEnumerable().Average(row => row.Field<decimal>("CAfat"));
                gv_Routewisemilk.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                gv_Routewisemilk.FooterRow.Cells[5].Text = PAfat.ToString("N2");

                decimal PAsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("CAsnf"));
                gv_Routewisemilk.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                gv_Routewisemilk.FooterRow.Cells[6].Text = PAsnf.ToString("N2");

                decimal Pfatkg = dt.AsEnumerable().Average(row => row.Field<decimal>("Rate"));
                gv_Routewisemilk.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                gv_Routewisemilk.FooterRow.Cells[7].Text = Pfatkg.ToString("N2");

                decimal Psnfkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                gv_Routewisemilk.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                gv_Routewisemilk.FooterRow.Cells[8].Text = Psnfkg.ToString("N2");


                //decimal CMilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMilkkg"));
                //gv_Routewisemilk.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                //gv_Routewisemilk.FooterRow.Cells[8].Text = CMilkkg.ToString("N2");

                //decimal CAfat = dt.AsEnumerable().Average(row => row.Field<decimal>("CAfat"));
                //gv_Routewisemilk.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                //gv_Routewisemilk.FooterRow.Cells[9].Text = CAfat.ToString("N2");

                //decimal CAsnf = dt.AsEnumerable().Average(row => row.Field<decimal>("CAsnf"));
                //gv_Routewisemilk.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                //gv_Routewisemilk.FooterRow.Cells[10].Text = CAsnf.ToString("N2");

                //decimal Cfatkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Cfatkg"));
                //gv_Routewisemilk.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                //gv_Routewisemilk.FooterRow.Cells[11].Text = Cfatkg.ToString("N2");

                //decimal Csnfkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Csnfkg"));
                //gv_Routewisemilk.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                //gv_Routewisemilk.FooterRow.Cells[12].Text = Csnfkg.ToString("N2");



            }
            else
            {
                dt = null;
                gv_Routewisemilk.DataSource = dt;
                gv_Routewisemilk.DataBind();
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void grdLivepro_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Datefunc();

            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow3 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell3 = new TableCell();
                HeaderCell3.Text = "";
                HeaderCell3.ColumnSpan = 3;
                HeaderRow3.Cells.Add(HeaderCell3);
                grdLivepro.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell3.Font.Bold = true;
                HeaderCell3.HorizontalAlign = HorizontalAlign.Center;

                TableCell HeaderCell4 = new TableCell();
                HeaderCell4.Text = "AS per Vmcc Milk";
                HeaderCell4.ColumnSpan = 5;
                HeaderRow3.Cells.Add(HeaderCell4);
                grdLivepro.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell4.Font.Bold = true;
                HeaderCell4.HorizontalAlign = HorizontalAlign.Center;

                TableCell HeaderCell5 = new TableCell();
                HeaderCell5.Text = "AS per Plant Milk";
                HeaderCell5.ColumnSpan = 5;
                HeaderRow3.Cells.Add(HeaderCell5);
                grdLivepro.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell5.Font.Bold = true;
                HeaderCell5.HorizontalAlign = HorizontalAlign.Center;

                TableCell HeaderCell6 = new TableCell();
                HeaderCell6.Text = " Milk Difference";
                HeaderCell6.ColumnSpan = 5;
                HeaderRow3.Cells.Add(HeaderCell6);
                grdLivepro.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell6.Font.Bold = true;
                HeaderCell6.HorizontalAlign = HorizontalAlign.Center;



                GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell21 = new TableCell();
                string s1 = d1.ToString();
                string s2 = d2.ToString();
                HeaderCell21.Text = " Date From:" + sd1.ToString() + "To:" + sd2.ToString();
                HeaderCell21.ColumnSpan = 18;
                HeaderRow1.Cells.Add(HeaderCell21);
                grdLivepro.Controls[0].Controls.AddAt(0, HeaderRow1);
                HeaderCell21.Font.Bold = true;
                HeaderCell21.HorizontalAlign = HorizontalAlign.Center;

                GridViewRow HeaderRow4 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell41 = new TableCell();
                HeaderCell41.Text = "Milk Difference Report For Plant and Vmcc";
                HeaderCell41.ColumnSpan = 18;
                HeaderRow4.Cells.Add(HeaderCell41);
                grdLivepro.Controls[0].Controls.AddAt(0, HeaderRow4);
                HeaderCell41.Font.Bold = true;
                HeaderCell41.HorizontalAlign = HorizontalAlign.Center;


                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                string get = ddl_PlantName.SelectedItem.Text;

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = ddl_PlantName.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 18;
                HeaderRow.Cells.Add(HeaderCell2);
                grdLivepro.Controls[0].Controls.AddAt(0, HeaderRow);
                HeaderCell2.Font.Bold = true;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            }
        }

        catch (Exception ex)
        {
        }

    }

    protected void grdLivepro_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string spilval = string.Empty;
            string[] spilvalarr = new string[2];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
              
               
               
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void gv_Routewisemilk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string spilval = string.Empty;
            string[] spilvalarr = new string[2];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void gv_Routewisemilk_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Datefunc();
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow3 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell3 = new TableCell();
                HeaderCell3.Text = "";
                HeaderCell3.ColumnSpan = 3;
                HeaderRow3.Cells.Add(HeaderCell3);
                gv_Routewisemilk.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell3.Font.Bold = true;
                HeaderCell3.HorizontalAlign = HorizontalAlign.Center;

                TableCell HeaderCell4 = new TableCell();
                HeaderCell4.Text = "AS per Vmcc Milk";
                HeaderCell4.ColumnSpan = 5;
                HeaderRow3.Cells.Add(HeaderCell4);
                gv_Routewisemilk.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell4.Font.Bold = true;
                HeaderCell4.HorizontalAlign = HorizontalAlign.Center;              



                GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell21 = new TableCell();
                HeaderCell21.Text = " Date From:" + sd1.ToString() + "To:" + sd2.ToString();
                HeaderCell21.ColumnSpan = 18;
                HeaderRow1.Cells.Add(HeaderCell21);
                gv_Routewisemilk.Controls[0].Controls.AddAt(0, HeaderRow1);
                HeaderCell21.Font.Bold = true;
                HeaderCell21.HorizontalAlign = HorizontalAlign.Center;

                GridViewRow HeaderRow4 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell41 = new TableCell();
                HeaderCell41.Text = "Milk Difference Report For Plant and Vmcc   ["  + acode2 +   "]"; ;
                HeaderCell41.ColumnSpan = 18;
                HeaderRow4.Cells.Add(HeaderCell41);
                gv_Routewisemilk.Controls[0].Controls.AddAt(0, HeaderRow4);
                HeaderCell41.Font.Bold = true;
                HeaderCell41.HorizontalAlign = HorizontalAlign.Center;


                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                string get = ddl_PlantName.SelectedItem.Text;

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = ddl_PlantName.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 18;
                HeaderRow.Cells.Add(HeaderCell2);
                gv_Routewisemilk.Controls[0].Controls.AddAt(0, HeaderRow);
                HeaderCell2.Font.Bold = true;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;        

            }
        }

        catch (Exception ex)
        {
        }

    }

    protected void btn_VmccExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            //string filename = "'" + ddl_PlantName.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gv_Routewisemilk.AllowPaging = false;
                Datefunc();
               GetAgenwiseMilkDetails(ccode.ToString(), acode2);

                gv_Routewisemilk.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gv_Routewisemilk.HeaderRow.Cells)
                {
                    cell.BackColor = gv_Routewisemilk.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gv_Routewisemilk.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gv_Routewisemilk.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gv_Routewisemilk.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gv_Routewisemilk.RenderControl(hw);

                //style to format numbers to string
                //  string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }



    protected void grdLivepro1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
          //  Datefunc();

            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow3 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell3 = new TableCell();
                HeaderCell3.Text = "";
                HeaderCell3.ColumnSpan = 2;
                HeaderRow3.Cells.Add(HeaderCell3);
                grdLivepro1.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell3.Font.Bold = true;
                HeaderCell3.HorizontalAlign = HorizontalAlign.Center;

                TableCell HeaderCell4 = new TableCell();
                HeaderCell4.Text = "AS per Vmcc Milk";
                HeaderCell4.ColumnSpan = 4;
                HeaderRow3.Cells.Add(HeaderCell4);
                grdLivepro1.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell4.Font.Bold = true;
                HeaderCell4.HorizontalAlign = HorizontalAlign.Center;

                TableCell HeaderCell5 = new TableCell();
                HeaderCell5.Text = "AS per Plant Milk";
                HeaderCell5.ColumnSpan = 5;
                HeaderRow3.Cells.Add(HeaderCell5);
                grdLivepro1.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell5.Font.Bold = true;
                HeaderCell5.HorizontalAlign = HorizontalAlign.Center;

                TableCell HeaderCell6 = new TableCell();
                HeaderCell6.Text = " Milk Difference";
                HeaderCell6.ColumnSpan = 5;
                HeaderRow3.Cells.Add(HeaderCell6);
                grdLivepro1.Controls[0].Controls.AddAt(0, HeaderRow3);
                HeaderCell6.Font.Bold = true;
                HeaderCell6.HorizontalAlign = HorizontalAlign.Center;



                GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell21 = new TableCell();
                string s1 = d1.ToString();
                string s2 = d2.ToString();
                HeaderCell21.Text = " Agent Code="+ddl_agentid.Text+"   Date From:" + sd1.ToString() + "To:" + sd2.ToString();
                HeaderCell21.ColumnSpan = 18;
                HeaderRow1.Cells.Add(HeaderCell21);
                grdLivepro1.Controls[0].Controls.AddAt(0, HeaderRow1);
                HeaderCell21.Font.Bold = true;
                HeaderCell21.HorizontalAlign = HorizontalAlign.Center;

                GridViewRow HeaderRow4 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell41 = new TableCell();
                HeaderCell41.Text = "Milk Difference Report For Plant and Vmcc";
                HeaderCell41.ColumnSpan = 18;
                HeaderRow4.Cells.Add(HeaderCell41);
                grdLivepro1.Controls[0].Controls.AddAt(0, HeaderRow4);
                HeaderCell41.Font.Bold = true;
                HeaderCell41.HorizontalAlign = HorizontalAlign.Center;


                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                string get = ddl_PlantName.SelectedItem.Text;

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = ddl_PlantName.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 18;
                HeaderRow.Cells.Add(HeaderCell2);
                grdLivepro1.Controls[0].Controls.AddAt(0, HeaderRow);
                HeaderCell2.Font.Bold = true;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            }
        }

        catch (Exception ex)
        {
        }
    }


    public void dropsownlist()
    {














        // dtp_DateTime.CustomFormat = "MM/dd/yyyy" + " " + "12:00:00";
        //    //  dtp_DateTime.CustomFormat = "MM/dd/yyyy";

        //   // string datee = dtp_DateTime.Text;
        //  //  DateTime data=datee
        //    // dtp_DateTime.CustomFormat = "MM/dd/yyyy" + " " + "12:00:00";
        //    //  dtp_DateTime.CustomFormat = "MM/dd/yyyy";

        //    DateTime datee1 = DateTime.Parse(dtp_DateTime.Text);
        //    string datee = datee1.ToString("MM/dd/yyyy");
        //    finaldate = datee1.ToString("MM/dd/yyyy") + " " + addsession;
        //    ffseconddate = datee1.ToString("MM/dd/yyyy") + " " + addfsecond;
        //    ftseconddate = datee1.ToString("MM/dd/yyyy") + " " + addtsecond;

        ddl_agentid.Items.Clear();
        string str = "";
        SqlConnection con = new SqlConnection(connStr);
        string strCmd = "select (Agent_id) as Agent_id from  DPUPRODUCERMASTER    where   plant_code='" + pcode + "'  group by Agent_id order by Rand(Agent_id) ";
        SqlCommand Cmd = new SqlCommand(strCmd, con);
        SqlDataAdapter da = new SqlDataAdapter(strCmd, con);
        DataTable df = new DataTable();
        da.Fill(df);

        foreach (DataRow dr in df.Rows)
        {
            ddl_agentid.Items.Add(dr["Agent_id"].ToString());

        }



    }


    protected void grdLivepro1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string spilval = string.Empty;
            string[] spilvalarr = new string[2];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
              
                //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;

            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            grdLivepro1.Visible = true;
            Label14.Visible = true;
            ddl_agentid.Visible = true;
            getcount = 1;
            dropsownlist();

        }
        else
        {
            getcount = 0;
            Label14.Visible = false;
            ddl_agentid.Visible = false;
            grdLivepro1.Visible = false;

        }


    }
}