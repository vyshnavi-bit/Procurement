using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
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
using System.Collections.Generic;

public partial class Frm_PlantwiseMilkmonitoring : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    int ccode = 1, pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string pcode1 = string.Empty;
    string d1 = string.Empty;
    string d2 = string.Empty;
    string d3 = string.Empty;
    string d4 = string.Empty;
    DateTime dtt = new DateTime();
    public string Rid = string.Empty;
    public string Querystr = string.Empty;
    public string Querystr1 = string.Empty;
    public string Querystr2 = string.Empty;
    public string frmdate = string.Empty;
    public string Todate = string.Empty;

    int k = 1;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public static int roleid;
    //Master Degin Querey
    //curr-prev=diff
    //query
    //prev='3-11-2015'
    //curr='2-11-2015'
    //Design
    //prev=curr
    //curr=prev
    //
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
              //  managmobNo = Session["managmobNo"].ToString();
                dtt = System.DateTime.Now;
                txt_FromDate.Text = dtt.ToString("dd/MM/yyyy");
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
                //PreviousBill();
                lbl_Sess.Visible = false;
                ddl_Sessions.Visible = false;
                //period
                lbl_CurrBilldate.Visible = false;
                lbl_PreveBilldate.Visible = false;
                ddl_Current.Visible = false;
                ddl_Previous.Visible = false;
                rdocheck.SelectedIndex = 1;
                Label6.Visible = false;
                ddl_PlantName.Visible = false;

                //Grid info
                Lbl_PlantName.Visible = false;
                lbl_Plantcode.Visible = false;
                lbl_Milkkg.Visible = false;
                Lbl_RouteName.Visible = false;
                Lbl_Routecode.Visible = false;
                Lbl_Amilkkg.Visible = false;

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
                Label6.Visible = false;
                ddl_PlantName.Visible = false;

                if (chk_milk.Checked == true)
                {
                    Querystr = "Where Diff_Milkkg > 0.1";                    
                }
                else
                {
                    Querystr = "Where Diff_Milkkg < 0.1";
                }


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
            DateTime dtp1 = new DateTime();            

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);           

            dtp1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);          

            d1 = dt1.ToString("MM/dd/yyyy");
          

            d3 = dt1.AddDays(-1).ToString("MM/dd/yyyy");

           
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
            if (chk_milk.Checked == true)
            {
                Label12.Text = "Total Milk Information  " + "Increase Decrease Report";
            }
            else
            {
                Label12.Text = "Total Milk Information  " + "Decrease Decrease Report";
            }
            if (rdocheck.SelectedValue != null)
            {
                Datefunc();
                rdocheck_type();

                GetPlantwiseMilkData();

                //Grid info
                Lbl_PlantName.Visible = false;
                lbl_Plantcode.Visible = false;
                lbl_Milkkg.Visible = false;
                Lbl_RouteName.Visible = false;
                Lbl_Routecode.Visible = false;
                Lbl_Amilkkg.Visible = false;

                gv_Routewisemilk.Visible = false;
                Gv_AgentData.Visible = false;
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please,Select Report Type";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            //Grid info
            Lbl_PlantName.Visible = true;
            lbl_Plantcode.Visible = true;
            lbl_Milkkg.Visible = true;
            Lbl_RouteName.Visible = false;
            Lbl_Routecode.Visible = false;
            Lbl_Amilkkg.Visible = false;

            Datefunc();
            rdocheck_type();
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;                    
            pcode1 = grdrow.Cells[10].Text;
            lbl_Plantcode.Text = grdrow.Cells[11].Text; 
            GetRoutewiseMilkData(ccode, pcode1);
           // GetRoutewiseMilkDataNew(ccode, pcode1);
            

            gv_Routewisemilk.Visible = true;
            Gv_AgentData.Visible = false;
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
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
            }
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
    //        string cmd = "SELECT * FROM " +
    //" (SELECT  ppcode,CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg FROM " +
    //" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Plant_Code AS ppcode FROM Procurement Where  "+ Querystr1 +"  Group by Plant_Code) AS t1 " +
    //" LEFT JOIN " +
    //" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Plant_Code AS cpcode FROM Procurement Where  " + Querystr2 + "  Group by Plant_Code) AS t2 ON t1.ppcode=t2.cpcode ) AS t3 " +
    //" LEFT JOIN " +
    //" (SELECT Plant_Code,CONVERT(Nvarchar(15),Plant_Code)+'_'+Plant_Name AS Pname,Manager_Name,Mana_PhoneNo,Milktype FROM Plant_Master Where Active >=1 ) AS t4 ON t3.ppcode=t4.Plant_Code  Order By RAND(Milktype) DESC,RAND(Plant_Code) ";

//            string cmd = "SELECT ppcode AS Sno,Manager_Name,Pname,pMkg,CMkg,Diff_Milkkg,pMkg1,CMkg1,Diff_Milkkg1,Plant_Code,Mana_PhoneNo,Milktype, ppcode FROM " +
//" (SELECT * FROM " +
//" (SELECT * FROM  " +
//" (SELECT Plant_Code,CONVERT(Nvarchar(15),Plant_Code)+'_'+Plant_Name AS Pname,Manager_Name ,Mana_PhoneNo ,Milktype  FROM Plant_Master Where Active >=1 ) AS t3  " +
//" LEFT JOIN " +
//"(SELECT * FROM " +
//" (SELECT  ppcode,CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg FROM   " +
//" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Plant_Code AS ppcode FROM Procurement Where  " + Querystr1 + "    Group by Plant_Code) AS t1  " +
//" LEFT JOIN " +
//" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Plant_Code AS cpcode FROM Procurement Where  " + Querystr2 + " Group by Plant_Code) AS t2 ON t1.ppcode=t2.cpcode ) AS t4 Where Diff_Milkkg > 0.1 ) AS F1 ON t3.Plant_Code=F1.ppcode )AS FF1 " +
//" LEFT JOIN " +
//" (SELECT * FROM  " +
//" (SELECT * FROM   " +
//" (SELECT Plant_Code AS pcode1,CONVERT(Nvarchar(15),Plant_Code)+'_'+Plant_Name AS Pname1,Manager_Name AS Manager_Name1,Mana_PhoneNo AS Mana_PhoneNo1,Milktype AS Milktype1 FROM Plant_Master Where Active >=1 ) AS t3  " +
//" LEFT JOIN " +
//" (SELECT * FROM " +
//" (SELECT  ppcode AS ppcode1,CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg1,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg1,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg1 FROM  " +
//" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Plant_Code AS ppcode FROM Procurement Where  " + Querystr1 + "    Group by Plant_Code) AS t1  " +
//" LEFT JOIN " +
//" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Plant_Code AS cpcode FROM Procurement Where " + Querystr2 + " Group by Plant_Code) AS t2 ON t1.ppcode=t2.cpcode ) AS t4 Where Diff_Milkkg1 < 0.1 ) AS F1 ON t3.pcode1=F1.ppcode1 )AS FF2) AS FF3 ON FF1.Plant_Code=FF3.ppcode1) AS DF " +
//" INNER JOIN " +
//" (SELECT DISTINCT(Plant_Code) AS Dplantcode FROM Procurement Where Prdate Between '" + d3 + "' AND '" + d1 + "' ) AS p ON DF.Plant_Code=p.Dplantcode ORDER by Plant_Code  ";
            string cmd = "SELECT ppcode AS Sno,Manager_Name,Pname,pMkg2,CMkg2,Diff_Milkkg,Diff_Milkkg1,Plant_Code,Mana_PhoneNo,Milktype, ppcode,pMkg2,CMkg2,pMkg1,CMkg1,Pname AS Pname1,Iperc,Dperc1 FROM    " +
" (SELECT * FROM    " +
" (SELECT * FROM   " +
" (SELECT Plant_Code,CONVERT(Nvarchar(15),Plant_Code)+'_'+Plant_Name AS Pname,Manager_Name ,Mana_PhoneNo ,Milktype  FROM Plant_Master Where Active >=1 ) AS t3     " +
" LEFT JOIN    " +
" (SELECT * FROM    " +
" (SELECT  ppcode,CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg,(CAST((CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))/ISNULL(Prev_MilkKg,0))AS DECIMAL(18,2)))*100  AS Iperc FROM      " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Plant_Code AS ppcode FROM Procurement Where  " + Querystr1 + "      Group by Plant_Code) AS t1     " +
" LEFT JOIN   " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Plant_Code AS cpcode FROM Procurement Where  " + Querystr2 + "  Group by Plant_Code) AS t2 ON t1.ppcode=t2.cpcode ) AS t4 Where Diff_Milkkg > 0.1 ) AS F1 ON t3.Plant_Code=F1.ppcode )AS FF1    " +
" LEFT JOIN   " +
" (SELECT * FROM   " +
" (SELECT * FROM      " +
" (SELECT Plant_Code AS pcode1,CONVERT(Nvarchar(15),Plant_Code)+'_'+Plant_Name AS Pname1,Manager_Name AS Manager_Name1,Mana_PhoneNo AS Mana_PhoneNo1,Milktype AS Milktype1 FROM Plant_Master Where Active >=1 ) AS t3     " +
" LEFT JOIN    " +
" (SELECT * FROM    " +
" (SELECT  ppcode AS ppcode1,CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg1,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg1,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg1,(CAST((CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))/ISNULL(Prev_MilkKg,0))AS DECIMAL(18,2)))*100  AS Dperc1 FROM     " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Plant_Code AS ppcode FROM Procurement Where  " + Querystr1 + "      Group by Plant_Code) AS t1     " +
" LEFT JOIN    " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Plant_Code AS cpcode FROM Procurement Where " + Querystr2 + "   Group by Plant_Code) AS t2 ON t1.ppcode=t2.cpcode ) AS t4 Where Diff_Milkkg1 < 0.1 ) AS F1 ON t3.pcode1=F1.ppcode1 )AS FF2) AS FF3 ON FF1.Plant_Code=FF3.ppcode1) AS DF    " +
" INNER JOIN    " +
" (SELECT  ppcode AS ppcode2,CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg2,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg2 FROM     " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Plant_Code AS ppcode FROM Procurement Where  " + Querystr1 + "      Group by Plant_Code) AS t1     " +
" LEFT JOIN    " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Plant_Code AS cpcode FROM Procurement Where  " + Querystr2 + "   Group by Plant_Code) AS t2 ON t1.ppcode=t2.cpcode ) AS pAll ON  DF.plant_code=pAll.ppcode2 ";


            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;

            if (dt.Rows.Count > 0)
            {
                grdLivepro.DataSource = dt;
                grdLivepro.DataBind();

                // grdLivepro.FooterRow.Cells[0].Text = "Total";
                //pMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("pMkg"));
                //grdLivepro.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //grdLivepro.FooterRow.Cells[3].Text = pMkg.ToString("N2");
                //CMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMkg"));
                //grdLivepro.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //grdLivepro.FooterRow.Cells[4].Text = CMkg.ToString("N2");
                //Diff_Milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Diff_Milkkg"));
                //grdLivepro.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //grdLivepro.FooterRow.Cells[5].Text = Diff_Milkkg.ToString("N2");

                //
                if (dt.Rows.Count > 0)
                {
                    int sumcountcheck = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (sumcountcheck == 0)
                        {
                            grdLivepro.FooterRow.Cells[1].Text = "Total";
                            grdLivepro.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                        }
                        else if (sumcountcheck >= 1)
                        {
                            if (sumcountcheck > 2 & sumcountcheck < 7)
                            {
                                decimal Gtotalcommon = 0;
                                foreach (DataRow dr1 in dt.Rows)
                                {
                                    object id;
                                    id = dr1[sumcountcheck].ToString();
                                    if (id == "")
                                    {
                                        id = "0";
                                    }

                                    decimal idd = Convert.ToDecimal(id);
                                    Gtotalcommon = Gtotalcommon + idd;
                                    grdLivepro.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Right;
                                    grdLivepro.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
                                    grdLivepro.FooterRow.HorizontalAlign = HorizontalAlign.Right;

                                }
                            }


                        }
                        else
                        {

                        }
                        sumcountcheck++;
                    }
                }
                //

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

    public void GetRoutewiseMilkData(int ccode,string pcode)
    {
        try
        {
            DataTable dt = new DataTable();
//            string cmd = "SELECT * FROM " +
//" (SELECT  CASE WHEN ROW_NUMBER() OVER(PARTITION BY a3.SupervisorName ORDER BY a3.Supercode) = 1 " +
//  "  THEN a3.SupervisorName ELSE '' END AS 'SupervisorName',Supercode,Mobile,Rid,pMkg,CMkg,Diff_Milkkg FROM " +
//" (SELECT * FROM " +
//" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg,Rid FROM " +
//" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Route_id AS Rid FROM Procurement Where Plant_Code='" + pcode + "' AND   " + Querystr1 + "  Group by Route_id) AS t1 " +
//" LEFT JOIN " +
//" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Route_id FROM Procurement Where Plant_Code='" + pcode + "' AND  " + Querystr2 + "  Group by Route_id) AS t2 ON t1.Rid=t2.Route_id ) AS t3   ) AS t4 " +
//" LEFT JOIN " +                     
//" (SELECT Pcode,Supercode AS Supercode,SupervisorName ,Mobile,Route_Id FROM " +
//" (SELECT Plant_Code AS Pcode,Supervisor_Code AS Supercode,(CONVERT(NVARCHAR(15),Supervisor_Code)+'_'+SupervisorName) AS SupervisorName,Mobile FROM Supervisor_Details where Plant_Code='" + pcode + "') AS a1 " +
//" LEFT JOIN " +
//" (SELECT DISTINCT(supervisor_code),plant_code,Route_id FROM Supervisor_RouteAllotment where Plant_Code='" + pcode + "') AS a2 ON a1.Supercode=a2.Supervisor_Code ) AS a3 ON t4.Rid=a3.Route_Id) AS F1 " +
//" LEFT JOIN " +
//" (SELECT Route_ID,(CONVERT(NVARCHAR(15),Route_ID)+'_'+Route_Name) AS Route_Name,Plant_Code FROM Route_Master WHERE Plant_Code='" + pcode + "' ) AS B1 ON F1.Rid=B1.Route_ID    " + Querystr + "   ORDER BY Supercode,Rid ";

            string cmd = "SELECT  CASE WHEN ROW_NUMBER() OVER(PARTITION BY LF.SupervisorName ORDER BY LF.SupervisorName) = 1    THEN LF.SupervisorName ELSE '' END AS 'SupervisorName',Route_Name,pMkg2,CMkg2,Diff_Milkkg1,Diff_Milkkg,Mobile,Plant_Code,Rid2 AS Rid,pMkg,CMkg,pMkg1,CMkg1,Supercode,Route_Name AS Route_Name1,Iperc,Dperc AS Dperc1 FROM " +
"(SELECT a3.SupervisorName AS SupervisorName,Route_Name,Diff_Milkkg1,Diff_Milkkg,Mobile,Plant_Code,F3.Route_Id AS Rid,pMkg,CMkg,pMkg1,CMkg1,Supercode,Iperc,Dperc1 AS Dperc   FROM " +
" (SELECT FF1.Plant_Code,FF1.Route_ID,FF1.Route_Name,pMkg1,CMkg1,Diff_Milkkg1,pMkg,CMkg,Diff_Milkkg,Iperc,Dperc1 FROM " +
" (SELECT * FROM " +
" (SELECT Route_ID,(CONVERT(NVARCHAR(15),Route_ID)+'_'+Route_Name) AS Route_Name,Plant_Code FROM Route_Master WHERE Plant_Code='" + pcode + "' ) AS R1 " +
" LEFT JOIN " +
" (SELECT * FROM " +
" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg1,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg1,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg1,Rid AS Rid1,(CAST((CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))/ISNULL(Prev_MilkKg,0))AS DECIMAL(18,2)))*100  AS Iperc  FROM " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Route_id AS Rid FROM Procurement Where Plant_Code='" + pcode + "' AND  " + Querystr1 + "    Group by Route_id) AS t1  " +
" LEFT JOIN " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Route_id AS Route_id1 FROM Procurement Where Plant_Code='" + pcode + "' AND  " + Querystr2 + "    Group by Route_id) AS t2 ON t1.Rid=t2.Route_id1 ) AS t3  Where Diff_Milkkg1 > 0.1 ) AS F1 ON R1.Route_ID=f1.Rid1) AS FF1 " +
" LEFT JOIN " +
" (SELECT * FROM " +
" (SELECT Route_ID,(CONVERT(NVARCHAR(15),Route_ID)+'_'+Route_Name) AS Route_Name,Plant_Code FROM Route_Master WHERE Plant_Code='" + pcode + "' ) AS R1 " +
" INNER JOIN " +
" (SELECT * FROM " +
" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg,Rid,(CAST((CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))/ISNULL(Prev_MilkKg,0))AS DECIMAL(18,2)))*100  AS Dperc1 FROM  " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Route_id AS Rid FROM Procurement Where Plant_Code='" + pcode + "' AND  " + Querystr1 + "    Group by Route_id) AS t1  " +
" LEFT JOIN " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Route_id FROM Procurement Where Plant_Code='" + pcode + "' AND  " + Querystr2 + "   Group by Route_id) AS t2 ON t1.Rid=t2.Route_id ) AS t3  Where Diff_Milkkg < 0.1 ) AS F2 ON R1.Route_ID=F2.Rid) AS FF2 ON FF1.Route_ID=FF2.Route_ID ) AS F3 " +
" LEFT JOIN " +
" (SELECT Pcode,Supercode AS Supercode,SupervisorName ,Mobile,Route_id FROM " +
" (SELECT Plant_Code AS Pcode,Supervisor_Code AS Supercode,(CONVERT(NVARCHAR(15),Supervisor_Code)+'_'+SupervisorName) AS SupervisorName,Mobile FROM Supervisor_Details where Plant_Code='" + pcode + "') AS a1  " +
" LEFT JOIN " +
" (SELECT DISTINCT(supervisor_code),plant_code,Route_id FROM Supervisor_RouteAllotment where Plant_Code='" + pcode + "') AS a2 ON a1.Supercode=a2.Supervisor_Code ) AS a3 ON F3.Route_ID=a3.Route_Id )  AS LF " +
" INNER JOIN " +
" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg2,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg2,Rid AS Rid2 FROM  " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Route_id AS Rid FROM Procurement Where Plant_Code='" + pcode + "' AND  " + Querystr1 + "      Group by Route_id) AS t1  " +
" LEFT JOIN  " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Route_id FROM Procurement Where Plant_Code='" + pcode + "' AND  " + Querystr2 + "     Group by Route_id) AS t2 ON t1.Rid=t2.Route_id) AS CurrRid ON LF.Rid=CurrRid.Rid2 Order BY LF.Supercode";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;

            if (dt.Rows.Count > 0)
            {
                gv_Routewisemilk.DataSource = dt;
                gv_Routewisemilk.DataBind();

                //gv_Routewisemilk.FooterRow.Cells[0].Text = "Total";
                //pMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("pMkg"));
                //gv_Routewisemilk.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                //gv_Routewisemilk.FooterRow.Cells[3].Text = pMkg.ToString("N2");
                //CMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMkg"));
                //gv_Routewisemilk.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                //gv_Routewisemilk.FooterRow.Cells[4].Text = CMkg.ToString("N2");
                //Diff_Milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Diff_Milkkg"));
                //gv_Routewisemilk.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                //gv_Routewisemilk.FooterRow.Cells[5].Text = Diff_Milkkg.ToString("N2");


              //  lbl_Milkkg.Text = Diff_Milkkg.ToString("F2").Trim();

                //
                if (dt.Rows.Count > 0)
                {
                    int sumcountcheck = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (sumcountcheck == 0)
                        {
                            gv_Routewisemilk.FooterRow.Cells[1].Text = "Total";
                            gv_Routewisemilk.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                        }
                        else if (sumcountcheck >= 1)
                        {
                            if (sumcountcheck > 1 & sumcountcheck < 6)
                            {
                                decimal Gtotalcommon = 0;
                                foreach (DataRow dr1 in dt.Rows)
                                {
                                    object id;
                                    id = dr1[sumcountcheck].ToString();
                                    if (id == "")
                                    {
                                        id = "0";
                                    }

                                    decimal idd = Convert.ToDecimal(id);
                                    Gtotalcommon = Gtotalcommon + idd;
                                    gv_Routewisemilk.FooterRow.Cells[sumcountcheck+1].HorizontalAlign = HorizontalAlign.Right;
                                    gv_Routewisemilk.FooterRow.Cells[sumcountcheck+1].Text = Gtotalcommon.ToString("N2");
                                    gv_Routewisemilk.FooterRow.HorizontalAlign = HorizontalAlign.Right;

                                }
                            }


                        }
                        else
                        {

                        }
                        sumcountcheck++;
                    }
                }
                //


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

    public void GetRoutewiseMilkDataNew(int ccode, string pcode)
    {
        //gv_RoutewisemilkDC
        try
        {
            DataTable dt = new DataTable();
            string cmd = "SELECT CASE WHEN ROW_NUMBER() OVER(PARTITION BY a3.SupervisorName ORDER BY a3.Supercode) = 1 " +
 "   THEN a3.SupervisorName ELSE '' END AS 'SupervisorName',Supercode,Route_Name,pMkg1,CMkg1,Diff_Milkkg1,pMkg,CMkg,Diff_Milkkg,Mobile,Plant_Code,F3.Route_Id AS Rid  FROM " +
" (SELECT FF1.Plant_Code,FF1.Route_ID,FF1.Route_Name,pMkg1,CMkg1,Diff_Milkkg1,pMkg,CMkg,Diff_Milkkg FROM " +
" (SELECT * FROM " +
" (SELECT Route_ID,(CONVERT(NVARCHAR(15),Route_ID)+'_'+Route_Name) AS Route_Name,Plant_Code FROM Route_Master WHERE Plant_Code='168' ) AS R1 " +
" LEFT JOIN " +
" (SELECT * FROM " +
" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg1,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg1,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg1,Rid AS Rid1  FROM " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Route_id AS Rid FROM Procurement Where Plant_Code='168' AND   Prdate='10/03/2015'    Group by Route_id) AS t1  " +
" LEFT JOIN " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Route_id AS Route_id1 FROM Procurement Where Plant_Code='168' AND  Prdate='10/02/2015'    Group by Route_id) AS t2 ON t1.Rid=t2.Route_id1 ) AS t3  Where Diff_Milkkg1 > 0.1 ) AS F1 ON R1.Route_ID=f1.Rid1) AS FF1 " +
" LEFT JOIN " +
" (SELECT * FROM " +
" (SELECT Route_ID,(CONVERT(NVARCHAR(15),Route_ID)+'_'+Route_Name) AS Route_Name,Plant_Code FROM Route_Master WHERE Plant_Code='168' ) AS R1 " +
" LEFT JOIN " +
" (SELECT * FROM " +
" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg,Rid FROM  " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Route_id AS Rid FROM Procurement Where Plant_Code='168' AND   Prdate='10/03/2015'    Group by Route_id) AS t1  " +
" LEFT JOIN " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Route_id FROM Procurement Where Plant_Code='168' AND  Prdate='10/02/2015'    Group by Route_id) AS t2 ON t1.Rid=t2.Route_id ) AS t3  Where Diff_Milkkg < 0.1 ) AS F2 ON R1.Route_ID=F2.Rid) AS FF2 ON FF1.Route_ID=FF2.Route_ID ) AS F3 " +
" LEFT JOIN " +
" (SELECT Pcode,Supercode AS Supercode,SupervisorName ,Mobile,Route_id FROM " +
" (SELECT Plant_Code AS Pcode,Supervisor_Code AS Supercode,(CONVERT(NVARCHAR(15),Supervisor_Code)+'_'+SupervisorName) AS SupervisorName,Mobile FROM Supervisor_Details where Plant_Code='168') AS a1  " +
" LEFT JOIN " +
" (SELECT DISTINCT(supervisor_code),plant_code,Route_id FROM Supervisor_RouteAllotment where Plant_Code='168') AS a2 ON a1.Supercode=a2.Supervisor_Code ) AS a3 ON F3.Route_ID=a3.Route_Id Order By Supercode,RAND(F3.Route_ID) ";
            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;
            decimal pMkg1 = 0;
            decimal CMkg1 = 0;
            decimal Diff_Milkkg1 = 0;

            if (dt.Rows.Count > 0)
            {
                //GridView1.DataSource = dt;
                //GridView1.DataBind();

                //gv_RoutewisemilkDC.FooterRow.Cells[0].Text = "Total";
                //pMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("pMkg"));
                //gv_RoutewisemilkDC.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                //gv_RoutewisemilkDC.FooterRow.Cells[3].Text = pMkg.ToString("N2");
                //CMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMkg"));
                //gv_RoutewisemilkDC.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                //gv_RoutewisemilkDC.FooterRow.Cells[4].Text = CMkg.ToString("N2");
                //Diff_Milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Diff_Milkkg"));
                //gv_RoutewisemilkDC.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                //gv_RoutewisemilkDC.FooterRow.Cells[5].Text = Diff_Milkkg.ToString("N2");
                //
           
                //pMkg1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("pMkg1"));
                //gv_RoutewisemilkDC.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                //gv_RoutewisemilkDC.FooterRow.Cells[6].Text = pMkg1.ToString("N2");
                //CMkg1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMkg1"));
                //gv_RoutewisemilkDC.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                //gv_RoutewisemilkDC.FooterRow.Cells[7].Text = CMkg1.ToString("N2");
                //Diff_Milkkg1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Diff_Milkkg1"));
                //gv_RoutewisemilkDC.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                //gv_RoutewisemilkDC.FooterRow.Cells[8].Text = Diff_Milkkg1.ToString("N2");


               // lbl_Milkkg.Text = Diff_Milkkg.ToString("F2").Trim();

            }
            else
            {
                dt = null;
                //GridView1.DataSource = dt;
                //GridView1.DataBind();
            }
        }

        catch (Exception ex)
        {
        }
    }


    protected void lnkView1_Click(object sender, EventArgs e)
    {
        try
        {
            //Grid info
            Lbl_PlantName.Visible = true;
            lbl_Plantcode.Visible = true;
            lbl_Milkkg.Visible = true;
            Lbl_RouteName.Visible = true;
            Lbl_Routecode.Visible = true;
            Lbl_Amilkkg.Visible = true;

            Datefunc();
            rdocheck_type();
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;          
            
            Rid = grdrow.Cells[10].Text;
            pcode1 = grdrow.Cells[11].Text;
            Lbl_Routecode.Text = grdrow.Cells[12].Text;
            GetAgentwiseMilkData(ccode, pcode1, Rid);

           
            Gv_AgentData.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    //protected void gv_Routewisemilk_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        string spilval = string.Empty;
    //        string[] spilvalarr = new string[2];
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
    //            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
    //            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
    //            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
    //            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
    //            e.Row.Cells[7].Visible = false;
    //            e.Row.Cells[8].Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    protected void gv_Routewisemilk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string spilval = string.Empty;
            string[] spilvalarr = new string[2];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;    

                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }


    public void GetAgentwiseMilkData(int ccode, string pcode, string Rid)
    {
        try
        {
            DataTable dt = new DataTable();
//            string cmd = "SELECT * FROM " +
//" (SELECT * FROM " +
//" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg,Aid FROM " +
//" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Agent_id AS Aid FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND    " + Querystr1 + "  Group by Agent_id) AS t1 " +
//" LEFT JOIN  " +
//" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Agent_id FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND  " + Querystr2 + "   Group by Agent_id) AS t2 ON t1.Aid=t2.Agent_id ) AS t3 ) AS F1 " +
//" LEFT JOIN " +
//" (SELECT Agent_Id,(CONVERT(NVARCHAR(15),Agent_Id)+'_'+Agent_Name) AS Agent_Name,phone_Number FROM Agent_Master WHERE Plant_Code='" + pcode + "' ) AS B1 ON F1.Aid=B1.Agent_Id   " + Querystr + "  ORDER BY Agent_Id ";

            string cmd = "SELECT DISTINCT(Agent_Id) AS Agent_Id, Agent_Name,pMkg2,CMkg2,Diff_Milkkg,Diff_Milkkg1,pMkg1,CMkg1,phone_Number,pMkg,CMkg,Iperc,Dperc1  FROM " +
" ( SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg2,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg2,Aid AS Aid2 FROM   " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Agent_id AS Aid FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND     " + Querystr1 + "       Group by Agent_id) AS t1  " + 
" LEFT JOIN  " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Agent_id FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND  " + Querystr2 + "     Group by Agent_id) AS t2 ON t1.Aid=t2.Agent_id  ) AS A3 " +
" LEFT JOIN " +
" (SELECT Agent_Name,pMkg,CMkg,Diff_Milkkg,pMkg1,CMkg1,Diff_Milkkg1,phone_Number,Agent_Id,Iperc,Dperc1 FROM " +
" (SELECT * FROM  " +
" (SELECT Agent_Id,(CONVERT(NVARCHAR(15),Agent_Id)+'_'+Agent_Name) AS Agent_Name,phone_Number FROM Agent_Master WHERE Plant_Code='" + pcode + "' ) AS A1 " +
" LEFT JOIN " +
" (SELECT * FROM " +
" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg,Aid,(CAST((CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))/ISNULL(Prev_MilkKg,0))AS DECIMAL(18,2)))*100  AS Iperc FROM  " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Agent_id AS Aid FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND    " + Querystr1 + "   Group by Agent_id) AS t1   " +
" LEFT JOIN  " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Agent_id FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND   " + Querystr2 + "     Group by Agent_id) AS t2 ON t1.Aid=t2.Agent_id ) AS t3  Where Diff_Milkkg > 0.1  ) AS F1 ON A1.Agent_Id=F1.Aid) AS FF1 " +
" LEFT JOIN " +
" (SELECT * FROM  " +
" (SELECT Agent_Id AS Agent_Id1,(CONVERT(NVARCHAR(15),Agent_Id)+'_'+Agent_Name) AS Agent_Name1,phone_Number AS phone_Number1  FROM Agent_Master WHERE Plant_Code='" + pcode + "' ) AS A1 " +
" INNER JOIN " +
" (SELECT * FROM  " +
" (SELECT CAST(ISNULL(Prev_MilkKg,0) AS DECIMAL(18,2)) AS pMkg1,CAST(ISNULL(Curr_MilkKg,0) AS DECIMAL(18,2)) AS CMkg1,CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))  AS Diff_Milkkg1,Aid AS Aid1,(CAST((CAST((ISNULL(Curr_MilkKg,0)-ISNULL(Prev_MilkKg,0)) AS DECIMAL(18,2))/ISNULL(Prev_MilkKg,0))AS DECIMAL(18,2)))*100  AS Dperc1 FROM  " +
" (SELECT SUM(Milk_kg) AS Prev_MilkKg,Agent_id AS Aid FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND     " + Querystr1 + "     Group by Agent_id) AS t1  " +
" LEFT JOIN  " +
" (SELECT SUM(Milk_kg) AS Curr_MilkKg,Agent_id FROM Procurement Where Plant_Code='" + pcode + "' AND Route_id='" + Rid + "' AND  " + Querystr2 + "  Group by Agent_id) AS t2 ON t1.Aid=t2.Agent_id ) AS t3  Where Diff_Milkkg1 < 0.1  ) AS F1 ON A1.Agent_Id1=F1.Aid1) AS FF2 ON FF1.Agent_Id=FF2.Agent_Id1 )LF  ON  A3.Aid2=LF.Agent_Id Order By Agent_Id ";
            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);
            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;
            if (dt.Rows.Count > 0)
            {
                Gv_AgentData.DataSource = dt;
                Gv_AgentData.DataBind();

                //Gv_AgentData.FooterRow.Cells[0].Text = "Total";
                //pMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("pMkg"));
                //Gv_AgentData.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //Gv_AgentData.FooterRow.Cells[2].Text = pMkg.ToString("N2");
                //CMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("CMkg"));
                //Gv_AgentData.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //Gv_AgentData.FooterRow.Cells[3].Text = CMkg.ToString("N2");
                //Diff_Milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Diff_Milkkg"));
                //Gv_AgentData.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //Gv_AgentData.FooterRow.Cells[4].Text = Diff_Milkkg.ToString("N2");

                //Lbl_Amilkkg.Text = Diff_Milkkg.ToString("F2").Trim();


                //
                if (dt.Rows.Count > 0)
                {
                    int sumcountcheck = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (sumcountcheck == 0)
                        {
                            gv_Routewisemilk.FooterRow.Cells[1].Text = "Total";
                            gv_Routewisemilk.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                        }
                        else if (sumcountcheck > 1)
                        {
                            if (sumcountcheck > 1 & sumcountcheck < 6)
                            {
                                decimal Gtotalcommon = 0;
                                foreach (DataRow dr1 in dt.Rows)
                                {
                                    object id;
                                    id = dr1[sumcountcheck].ToString();
                                    if (id == "")
                                    {
                                        id = "0";
                                    }

                                    decimal idd = Convert.ToDecimal(id);
                                    Gtotalcommon = Gtotalcommon + idd;
                                    Gv_AgentData.FooterRow.Cells[sumcountcheck ].HorizontalAlign = HorizontalAlign.Right;
                                    Gv_AgentData.FooterRow.Cells[sumcountcheck ].Text = Gtotalcommon.ToString("N2");
                                    Gv_AgentData.FooterRow.HorizontalAlign = HorizontalAlign.Right;

                                }
                            }


                        }
                        else
                        {

                        }
                        sumcountcheck++;
                    }
                }
                //

            }
            else
            {
                dt = null;
                Gv_AgentData.DataSource = dt;
                Gv_AgentData.DataBind();
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
            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_PlantName.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdLivepro.AllowPaging = false;
               // getgridview();
                //  Get_IncreaseDecreaseAmountDetails();

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


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    
    protected void Gv_AgentData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Brown;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void rdocheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // int i = RadioButtonList1.SelectedIndex;


            if (rdocheck.SelectedValue != null)
            {
                string value = rdocheck.SelectedItem.Value.Trim();

                if (value == "1")
                {                   
                    Label10.Visible = true;
                    txt_FromDate.Visible = true;
                    lbl_Sess.Visible = true;
                    ddl_Sessions.Visible = true;
                    //period
                    lbl_CurrBilldate.Visible = false;
                    lbl_PreveBilldate.Visible = false;
                    ddl_Current.Visible = false;
                    ddl_Previous.Visible = false;

                }


                if (value == "2")
                {


                    Label10.Visible = true;
                    txt_FromDate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;
                    //period
                    lbl_CurrBilldate.Visible = false;
                    lbl_PreveBilldate.Visible = false;
                    ddl_Current.Visible = false;
                    ddl_Previous.Visible = false;

                }

                if (value == "3")
                {
                    Label10.Visible = false;
                    txt_FromDate.Visible = false;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;
                    //period
                    lbl_CurrBilldate.Visible = true;
                    lbl_PreveBilldate.Visible = true;
                    ddl_Current.Visible = true;
                    ddl_Previous.Visible = true;
                    PreviousBill();
                    //
                    Label6.Visible = true;
                    ddl_PlantName.Visible = true;


                }

            }
        }
        catch (Exception Ex)
        {
            Lbl_Errormsg.Text = Ex.Message;
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
        }

    }

    private void rdocheck_type()
    {
        DateTime dt1 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        string curdate = dt1.AddDays(-1).ToString("dd/MM/yyyy");

        frmdate = curdate.Trim();
        Todate = txt_FromDate.Text.Trim();

        try
        {
            // int i = RadioButtonList1.SelectedIndex;
          

            if (rdocheck.SelectedValue != null)
            {
                string value = rdocheck.SelectedItem.Value.Trim();

                if (value == "1")
                {
                    Label10.Visible = true;
                    txt_FromDate.Visible = true;
                    lbl_Sess.Visible = true;
                    ddl_Sessions.Visible = true;

                    Querystr1 = "Prdate='" + d1.ToString().Trim() + "' AND Sessions='" + ddl_Sessions.SelectedItem.Text.Trim() + "'  ";
                    Querystr2 = "Prdate='" + d3.ToString().Trim() + "' AND Sessions='" + ddl_Sessions.SelectedItem.Text.Trim() + "'  ";
                                        

                    lbl_Reporttitle.Text = "FromDate :" + curdate.Trim() +      "&  ToDate :" + txt_FromDate.Text.Trim() + "    Session :" + ddl_Sessions.SelectedItem.Text.Trim();
                }


                if (value == "2")
                {

                    Label10.Visible = true;
                    txt_FromDate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;

                    Querystr1 = "Prdate='" + d1.ToString().Trim() + "'  ";
                    Querystr2 = "Prdate='" + d3.ToString().Trim() + "'  ";

                    lbl_Reporttitle.Text = "FromDate :" + curdate.Trim() + "&  ToDate :" + txt_FromDate.Text.Trim();

                }

                if (value == "3")
                {
                    Label10.Visible = false;
                    txt_FromDate.Visible = false;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;

                    //
                    string currbilldate = string.Empty;
                    string[] currbilldatearr = new string[2];
                    currbilldate = ddl_Current.SelectedItem.Value.Trim();
                    currbilldatearr = currbilldate.Split('_');

                    string prevbilldate = string.Empty;
                    string[] prevbilldatearr = new string[2];
                    prevbilldate = ddl_Previous.SelectedItem.Value.Trim();
                    prevbilldatearr = prevbilldate.Split('_');

                    d1 = currbilldatearr[1].ToString().Trim();
                    d2 = currbilldatearr[1].ToString().Trim();
                    d3 = prevbilldatearr[0].ToString().Trim();

                    Querystr1 = "Prdate Between   CONVERT(date, '" + prevbilldatearr[0].ToString().Trim() + "' ,101) AND  CONVERT(date, '" + prevbilldatearr[1].ToString().Trim() + "' ,101)  ";
                    Querystr2 = "Prdate Between   CONVERT(date, '" + currbilldatearr[0].ToString().Trim() + "' ,101) AND  CONVERT(date, '" + currbilldatearr[1].ToString().Trim() + "' ,101)  ";


                    lbl_Reporttitle.Text = "FromDate :" + ddl_Current.SelectedItem.Value.Trim() + "    ToDate :" + ddl_Previous.SelectedItem.Value.Trim();
               
                }

            }
        }
        catch (Exception Ex)
        {
            Lbl_Errormsg.Text = Ex.Message;
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void PreviousBill()
    {
        try
        {
            string str = string.Empty;
            
            using (con = dbaccess.GetConnection())
            {
                str = "SELECT Descriptions,CONVERT(Nvarchar(11),Bill_frmdate)+'_'+CONVERT(Nvarchar(11),Bill_todate) AS Bdate FROM Bill_date  Where Plant_Code='" + pcode + "'  ORDER BY Tid";
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(ds);
                ddl_Previous.Items.Clear();
                ddl_Current.Items.Clear();
                if (ds != null)
                {
                    ddl_Previous.DataSource = ds;
                    ddl_Previous.DataTextField = "Descriptions";
                    ddl_Previous.DataValueField = "Bdate";
                    ddl_Previous.DataBind();

                    ddl_Current.DataSource = ds;
                    ddl_Current.DataTextField = "Descriptions";
                    ddl_Current.DataValueField = "Bdate";
                    ddl_Current.DataBind();

                }
                else
                {

                }
            }

        }
        catch (Exception ex)
        {
        }
    }



   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdLivepro_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string reporthader = string.Empty;


                GridView HeaderGrid2 = (GridView)sender;
                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCellemp = new TableCell();
                HeaderCellemp.HorizontalAlign = HorizontalAlign.Right;
                HeaderCellemp.Text = "";
                HeaderCellemp.Font.Size = 11;
                HeaderCellemp.ColumnSpan = 3;
                HeaderCellemp.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCellemp);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;


                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.Text = frmdate;
                HeaderCell1.Font.Size = 11;
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCell1);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell2.Text = Todate;
                HeaderCell2.Font.Size = 11;
                HeaderCell2.ColumnSpan = 1;
                HeaderCell2.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCell2);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;

                TableCell HeaderCell3 = new TableCell();
                HeaderCell3.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell3.Text = "";
                HeaderCell3.Font.Size = 11;
                HeaderCell3.ColumnSpan = 5;
                HeaderCell3.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCell3);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;

                grdLivepro.Controls[0].Controls.AddAt(0, HeaderGridRow2);


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
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string reporthader2 = string.Empty;


                GridView HeaderGrid3 = (GridView)sender;
                GridViewRow HeaderGridRow3 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCellemp = new TableCell();
                HeaderCellemp.HorizontalAlign = HorizontalAlign.Right;
                HeaderCellemp.Text = "";
                HeaderCellemp.Font.Size = 11;
                HeaderCellemp.ColumnSpan = 3;
                HeaderCellemp.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow3.Cells.Add(HeaderCellemp);
                HeaderGridRow3.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow3.Font.Bold = true;


                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.Text = frmdate;
                HeaderCell1.Font.Size = 11;
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow3.Cells.Add(HeaderCell1);
                HeaderGridRow3.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow3.Font.Bold = true;

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell2.Text = Todate;
                HeaderCell2.Font.Size = 11;
                HeaderCell2.ColumnSpan = 1;
                HeaderCell2.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow3.Cells.Add(HeaderCell2);
                HeaderGridRow3.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow3.Font.Bold = true;

                TableCell HeaderCell3 = new TableCell();
                HeaderCell3.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell3.Text = "";
                HeaderCell3.Font.Size = 11;
                HeaderCell3.ColumnSpan = 5;
                HeaderCell3.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow3.Cells.Add(HeaderCell3);
                HeaderGridRow3.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow3.Font.Bold = true;

                gv_Routewisemilk.Controls[0].Controls.AddAt(0, HeaderGridRow3);


            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Gv_AgentData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string reporthader3 = string.Empty;

                GridView HeaderGrid2 = (GridView)sender;
                GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCellemp = new TableCell();
                HeaderCellemp.HorizontalAlign = HorizontalAlign.Right;
                HeaderCellemp.Text = "";
                HeaderCellemp.Font.Size = 11;
                HeaderCellemp.ColumnSpan = 2;
                HeaderCellemp.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCellemp);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;


                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.Text = frmdate;
                HeaderCell1.Font.Size = 11;
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCell1);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell2.Text =Todate;
                HeaderCell2.Font.Size = 11;
                HeaderCell2.ColumnSpan = 1;
                HeaderCell2.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCell2);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;

                TableCell HeaderCell3 = new TableCell();
                HeaderCell3.HorizontalAlign = HorizontalAlign.Right;
                HeaderCell3.Text = "";
                HeaderCell3.Font.Size = 11;
                HeaderCell3.ColumnSpan =5;
                HeaderCell3.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                HeaderGridRow2.Cells.Add(HeaderCell3);
                HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
                HeaderGridRow2.Font.Bold = true;

                Gv_AgentData.Controls[0].Controls.AddAt(0, HeaderGridRow2);


            }
        }
        catch (Exception ex)
        {
        }
    }
    
}