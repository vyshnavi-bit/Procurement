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
using System.Text;
using InfoSoftGlobal;

public partial class Rpt_AgentmilkgLineGraph : System.Web.UI.Page
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
    //
    StringBuilder str = new StringBuilder();
    DataTable dt = new DataTable("Chart");
    string GraphWidth = "550";
    string GraphHeight = "370";
    string[] color = new string[12];
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
             //   managmobNo = Session["managmobNo"].ToString();
                dtt = System.DateTime.Now;
                txt_FromDate.Text = dtt.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtt.ToString("dd/MM/yyyy");
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

                Lbl_AgentName.Visible = false;
                ddl_AgentNAme.Visible = false;
                FCLiteral1.Visible = false;             

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

            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            d1 = dt1.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");


            d3 = dt1.AddDays(-1).ToString("MM/dd/yyyy");


        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


    private void LoadAgentcode()
    {
        try
        {
            Datefunc();
            DataSet ds = new DataSet();
            string cmd = "SELECT t2.Agent_Id AS Aid,t2.Agent_Name AS Aname FROM " +
" (SELECT DISTINCT(Agent_id) AS Aid  FROM Procurement Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate Between '" + d1.ToString() + "' AND '" + d1.ToString() + "') AS t1  " +
" LEFT JOIN  " +
" (SELECT Agent_Id,CONVERT(Nvarchar(10),Agent_Id)+'_'+Agent_Name AS Agent_Name FROM Agent_Master Where Plant_code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' ) AS t2 ON t1.Aid=t2.Agent_Id ORDER BY RAND(t1.Aid) ";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(ds);
            if (ds != null)
            {
                ddl_AgentNAme.DataSource = ds;
                ddl_AgentNAme.DataTextField = "Aname";
                ddl_AgentNAme.DataValueField = "Aid";
                ddl_AgentNAme.DataBind();
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }


    private void LoadGraph()
    {
        try
        {
            Datefunc();
            DataTable dt = new DataTable();
            string cmd = "SELECT CONVERT(varchar(12),Prdate,103) AS Year,SUM(Milk_kg) AS Sales FROM Procurement Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Agent_id='" + ddl_AgentNAme.SelectedItem.Value.Trim() + "' GROUP BY Agent_id,Prdate  order By prdate";
           // string cmd = "SELECT Agent_id,CONVERT(varchar(12),Prdate,103) AS Prdate,SUM(Milk_kg) AS Milk_kg,SUM(Milk_ltr) AS Milk_ltr FROM Procurement Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Agent_id='" + ddl_AgentNAme.SelectedItem.Value.Trim() + "' GROUP BY Agent_id,Prdate  order By prdate";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Prdate');
        data.addColumn('number', 'Milk_kg');     
 
        data.addRows(" + dt.Rows.Count + ");");

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Prdate"].ToString() + "');");
                    str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Milk_kg"].ToString() + ") ;");
                }

                str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
                str.Append(" chart.draw(data, {width: 750, height: 700, title: 'Company Performance',");
                str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'red'}}");
                str.Append("}); }");
                str.Append("</script>");
              //  lt.Text = str.ToString().Replace('*', '"');
               
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private DataTable LoadGraphData()
    {
        Datefunc();      
        string cmd = "SELECT CONVERT(varchar(12),Prdate,103) AS Year,SUM(Milk_kg) AS Sales FROM Procurement Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Agent_id='" + ddl_AgentNAme.SelectedItem.Value.Trim() + "' GROUP BY Agent_id,Prdate  order By prdate";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
        adp.Fill(dt);      
        return dt;
    }

    private void CreateLineGraph()
    {
        string strCaption = ddl_PlantName.SelectedItem.Text;
        string Def = "     " + "From:" + txt_FromDate.Text + "   " + "To:" + txt_ToDate.Text;
        string strSubCaption = ddl_AgentNAme.SelectedItem.Text + "  " + "Datewise Milk report" + Def;
       
        string xAxis = "Date";
        string yAxis = "Milk_kg";

        //strXML will be used to store the entire XML document generated
        string strXML = null;

        //Generate the graph element
        strXML = @"                
            <graph caption='" + strCaption + @"'            
            subcaption='" + strSubCaption + @"'          
           
            hovercapbg='FFECAA' hovercapborder='F47E00' formatNumberScale='0' decimalPrecision='2' 
            showvalues='0' numdivlines='3' numVdivlines='0' yaxisminvalue='80.00' yaxismaxvalue='80.00'  
            rotateNames='1'
            showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' 
            divLineAlpha='20' alternateHGridAlpha='5' 
            xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' 
            >        
            ";

        string tmp = null;

        tmp = @"<categories font='Arial' fontSize='11' fontColor='000000'>";
        foreach (DataRow DR in dt.Rows)
        {
            tmp += @"<category name='" + DR["year"].ToString().Trim() + @"' />";
        }
        tmp += @"</categories>";

        strXML += tmp;

        tmp = @"<dataset seriesName='Procurement' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>";
        foreach (DataRow DR in dt.Rows)
        {
            tmp += @"<set value='" + DR["sales"].ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + DR["sales"].ToString() + "'); &quot; />";
        }

        tmp += @"</dataset>";

        strXML += tmp;

        strXML += "</graph>";

        FCLiteral1.Text = FusionCharts.RenderChartHTML(
            "FusionCharts/FCF_MSLine.swf",   // Path to chart's SWF
            "",                              // Leave blank when using Data String method
            strXML,                          // xmlStr contains the chart data
            "mygraph4",                      // Unique chart ID
            GraphWidth, GraphHeight,         // Width & Height of chart
            false
            );
    }    



    protected void btn_LoadAgent_Click(object sender, EventArgs e)
    {
        try
        {
            FCLiteral1.Visible = false;
            Lbl_AgentName.Visible = true;
            ddl_AgentNAme.Visible = true;
            LoadAgentcode();
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
            string cmd = "";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);
                    

            if (dt.Rows.Count > 0)
            {
              

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
                //if (dt.Rows.Count > 0)
                //{
                //    int sumcountcheck = 0;
                //    foreach (DataColumn dc in dt.Columns)
                //    {
                //        if (sumcountcheck == 0)
                //        {
                //            grdLivepro.FooterRow.Cells[1].Text = "Total";
                //            grdLivepro.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                //        }
                //        else if (sumcountcheck >= 1)
                //        {
                //            if (sumcountcheck > 2 & sumcountcheck < 7)
                //            {
                //                decimal Gtotalcommon = 0;
                //                foreach (DataRow dr1 in dt.Rows)
                //                {
                //                    object id;
                //                    id = dr1[sumcountcheck].ToString();
                //                    if (id == "")
                //                    {
                //                        id = "0";
                //                    }

                //                    decimal idd = Convert.ToDecimal(id);
                //                    Gtotalcommon = Gtotalcommon + idd;
                //                    grdLivepro.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Right;
                //                    grdLivepro.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
                //                    grdLivepro.FooterRow.HorizontalAlign = HorizontalAlign.Right;

                //                }
                //            }


                //        }
                //        else
                //        {

                //        }
                //        sumcountcheck++;
                //    }
                //}
                //

            }
            else
            {
                dt = null;
               
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        try
        {
            FCLiteral1.Visible = true;
            LoadGraphData();
            CreateLineGraph();
        }
        catch (Exception ex)
        {

        }

    }
    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Lbl_AgentName.Visible = false;
        ddl_AgentNAme.Visible = false;
        FCLiteral1.Visible = false;
    }
}