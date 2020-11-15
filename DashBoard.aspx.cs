using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InfoSoftGlobal;
using System.Text;
using System.Configuration;


public partial class DashBoard : System.Web.UI.Page
{
    public string ccode, sess;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    string clrdisplay = string.Empty;
    DataSet ds = new DataSet();
    DateTime frmdat = new DateTime();
    DateTime todat = new DateTime();
    BLLPlantName BllPlant = new BLLPlantName();

    SqlConnection con = new SqlConnection();
    DbHelper dbaccess = new DbHelper();

    DataTable dt = new DataTable("Chart");
    DataTable dtAgcou = new DataTable("Chart");
    DataTable dtRemarks = new DataTable("Chart");

    string X, Y;
    string GraphWidth = "600";
    string GraphHeight = "330";
    string[] color = new string[25];

    BLLroutmaster bllrou = new BLLroutmaster();

    StringBuilder str = new StringBuilder();
    StringBuilder str1 = new StringBuilder();
    //Get connection string from web.config
    SqlConnection conn = new SqlConnection();
    SqlConnection conn1 = new SqlConnection("Data Source=223.196.32.30;Initial Catalog=AMPS;User ID=sa;Password=sap@123");

    //Line graph
    DataTable dt1 = new DataTable("Chart");
    DataTable dtRid = new DataTable();
    string GraphWidth1 = "1000";
    string GraphHeight1 = "600";
    string[] color1 = new string[12];

    public static int roleid;

    string PPCODE;
    string RRCODE;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            ccode = "1";
            frmdat = System.DateTime.Now;
            txt_frmdate.Text = frmdat.ToString("dd/MM/yyyy");
            txt_todate.Text = frmdat.ToString("dd/MM/yyyy");
            sess = System.DateTime.Now.ToString("tt");

            Rdl_Plantype.SelectedIndex = 0;

            if((roleid>=3) && (roleid!=9))
            {
            Get_PlantMilkStatus(ccode, Rdl_Plantype.SelectedItem.Value);
            BindChart();
            }

            if (roleid == 9)
            {
                Get_PlantMilkStatus1(ccode, Rdl_Plantype.SelectedItem.Value);
                BindChart();
            }
            Lbl_PlantName.Visible = false;
            lbl_Plantcode.Visible = false;
            Lbl_Errormsg.Visible = false;
            gv_PlantRouteMilkDetail.Visible = false;
            //
            lbl_Milkkg.Visible = false;
          //  Lbl_Amilkkg.Visible = false;

            //session
            rdocheck.SelectedIndex = 0;
            Rdl_Plantype.SelectedIndex = 0;
            lbl_Sess.Visible = true;
            ddl_Sessions.Visible = true;
            //
            if (rdocheck.SelectedValue != null)
            {
                string value = rdocheck.SelectedItem.Value.Trim();

                if (value == "1")
                {

                    Label5.Visible = false;
                    txt_todate.Visible = false;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = true;
                    ddl_Sessions.Visible = true;


                }


                if (value == "2")
                {

                    Label5.Visible = false;
                    txt_todate.Visible = false;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;


                }

                if (value == "3")
                {
                    Label5.Visible = true;
                    txt_todate.Visible = true;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;


                }

            }
            
                   

        }
        else
        {
            ccode = "1";
            Lbl_Errormsg.Visible = false;
            //session
            Label4.Text = "From";
            Label5.Visible = true;
            txt_todate.Visible = true;

            //
            if (rdocheck.SelectedValue != null)
            {
                string value = rdocheck.SelectedItem.Value.Trim();

                if (value == "1")
                {

                    Label5.Visible = false;
                    txt_todate.Visible = false;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = true;
                    ddl_Sessions.Visible = true;


                }


                if (value == "2")
                {

                    Label5.Visible = false;
                    txt_todate.Visible = false;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;


                }

                if (value == "3")
                {
                    Label5.Visible = true;
                    txt_todate.Visible = true;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;


                }

            }
        }
    }
   
  

    
    private void ConfigureColors()
    {
        color[0] = "AFD8F8";
        color[1] = "F6BD0F";
        color[2] = "8BBA00";
        color[3] = "FF8E46";
        color[4] = "008E8E";
        color[5] = "D64646";
        color[6] = "8E468E";
        color[7] = "588526";
        color[8] = "B3AA00";
        color[9] = "008ED6";
        color[10] = "9D080D";
        color[11] = "A186BE";
        color[12] = "A9D8FF";
        color[13] = "F6BDFF";
        color[14] = "8BBA01";
        color[15] = "FF8E45";
        color[16] = "008E85";
        color[17] = "D64645";
        color[18] = "8E4685";
        color[19] = "58852W";
        color[20] = "B3AA10";
        color[21] = "008SD1";
        color[22] = "9D081D";
        color[23] = "A986BE";
        color[24] = "A186BF";

    }

    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        try
        {

             dt.Rows.Clear(); 
             dtAgcou.Rows.Clear(); 
             dtRemarks.Rows.Clear();   
             dt1.Rows.Clear();  
             dtRid.Rows.Clear();

             gv_PlantAgentMilkDetail1.Visible = false;
             gv_PlantRouteMilkDetail.Visible = false;
             LtLine.Visible = false;
             FCLiteralLine.Visible = false;
             gv_PlantMilkDetail.Visible = true;
             FCLiteral1.Visible = false;
             FCLiteral2.Visible = false;
             FCLiteral3.Visible = false;
             lt.Visible = false;
             Lbl_PlantName.Visible=false;
             lbl_Plantcode.Visible=false;
             lbl_Milkkg.Visible = false;
             if ((roleid >= 3) && (roleid != 9))
             {

               Get_PlantMilkStatus(ccode, Rdl_Plantype.SelectedItem.Value);
             }
             if (roleid == 9)
             {

               Get_PlantMilkStatus1(ccode, Rdl_Plantype.SelectedItem.Value);

             }
             //Get_PlantMilkStatus1(ccode, Rdl_Plantype.SelectedItem.Value);
             Get_PlantMilkStatus(ccode, Rdl_Plantype.SelectedItem.Value);

            //BindChart();
            //ConfigureColors();
            //Get_RouteMilkStatus(ccode, pcode);
            //LoadMilkGraph();
            //LoadAgentGraph();
            //LoadremarksGraph();
        }
        catch (Exception ex)
        {

        }
    }

    private void Get_RouteMilkStatus(string ccode, string pcode)
    {
        try
        {
            string str = string.Empty;
            string d1, d2;
            decimal total = 0, Fat = 0, Snf = 0;
            frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            d1 = frmdat.ToString("MM/dd/yyyy");
            d2 = todat.ToString("MM/dd/yyyy");
            DataTable dtr = new DataTable();
            //
            if (rdocheck.SelectedItem.Value == "1")
            {
                if (ddl_Sessions.SelectedItem.Value == "1")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno,RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
 " (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM'  AND Plantcode='" + pcode + "'  GROUP BY Plantcode,Routeid ) AS t1 " +
 " INNER JOIN " +
 " (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
                }
                else
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno, RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
 " (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='PM' AND Plantcode='" + pcode + "'  GROUP BY Plantcode,Routeid ) AS t1 " +
 " INNER JOIN " +
 " (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
                }
            }
            else if (rdocheck.SelectedItem.Value == "2")
            {
                str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno, RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "'  AND Plantcode='" + pcode + "'  GROUP BY Plantcode,Routeid ) AS t1 " +
" INNER JOIN " +
" (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
            }
            else if (rdocheck.SelectedItem.Value == "3")
            {
                str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno, RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plantcode='" + pcode + "'  GROUP BY Plantcode,Routeid ) AS t1 " +
" INNER JOIN " +
" (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
            }
            else
            {
            }
                        
            using (con = dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dtr);
                if (dtr.Rows.Count > 0)
                {
                    gv_PlantRouteMilkDetail.DataSource = dtr;
                    gv_PlantRouteMilkDetail.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dtr.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dtr.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dtr.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    gv_PlantRouteMilkDetail.FooterRow.Cells[1].Text = "Total/Avg";
                    gv_PlantRouteMilkDetail.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        
                    gv_PlantRouteMilkDetail.FooterRow.Cells[2].Text = total.ToString("N2");
                    gv_PlantRouteMilkDetail.FooterRow.Cells[3].Text = Fat.ToString("f2");
                    gv_PlantRouteMilkDetail.FooterRow.Cells[4].Text = Snf.ToString("f2");

                    gv_PlantRouteMilkDetail.FooterRow.Font.Bold = true;
                    lbl_Milkkg.Visible = true;
                    lbl_Milkkg.Text = total.ToString();
                }
                else
                {
                    //gv_PlantMilkDetail.DataSource = dt;
                    //gv_PlantMilkDetail.DataBind();
                    //Lbl_Errormsg.Visible = true;
                    //Lbl_Errormsg.Text = "NO DATA...";
                    gv_PlantRouteMilkDetail.DataSource = dtr;
                    gv_PlantRouteMilkDetail.DataBind();
                }
                if (dtr.Rows.Count > 0)
                {
                    
                    DataRow rows = dtr.NewRow();                    
                    rows["RouteName"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");

                    dtr.Rows.Add(rows);

                    gv_PlantRouteMilkDetail.DataSource = dtr;
                    gv_PlantRouteMilkDetail.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
          
        }
    }


    private void LoadMilkGraph(string ccode, string pcode ,string RCODE )
    {
        try
        {
            string sqlstr = string.Empty;
            DateTime dtm1 = new DateTime();
            DateTime dtm2 = new DateTime();
            dtm1 = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            dtm2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);

            string d1 = dtm1.ToString("MM/dd/yyyy");
            string d2 = dtm2.ToString("MM/dd/yyyy");


            dt = null;
            // dt = bllrou.GraphMilkSummary(ccode, pcode, d1, d2);
            //
            if (rdocheck.SelectedItem.Value == "1")
            {
                if (ddl_Sessions.SelectedItem.Value == "1")
                {
                    sqlstr = "SELECT t2.Route_ID,t1.Smkg,t1.Afat,t1.ASnf FROM (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "' AND Plantcode='" + pcode + "'   AND RouteId='" + RCODE + "' AND  prdate ='" + d1.ToString().Trim() + "' AND Sessions='AM' GROUP BY Routeid ) AS t1 LEFT JOIN  (SELECT Route_ID AS Rid, CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Route_ID)+'__'+Route_Name)) AS Route_ID  FROM Route_Master WHERE Plant_Code='" + pcode + "' AND ROUTE_ID='" + RCODE + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY t1.Routeid";
                }
                else
                {
                    sqlstr = "SELECT t2.Route_ID,t1.Smkg,t1.Afat,t1.ASnf FROM (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "' AND Plantcode='" + pcode + "' AND RouteId='" + RCODE + "'   AND  prdate ='" + d1.ToString().Trim() + "' AND Sessions='PM' GROUP BY Routeid ) AS t1 LEFT JOIN  (SELECT Route_ID AS Rid, CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Route_ID)+'__'+Route_Name)) AS Route_ID  FROM Route_Master WHERE Plant_Code='" + pcode + "' AND ROUTE_ID='" + RCODE + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY t1.Routeid";
                }
            }
            else if (rdocheck.SelectedItem.Value == "2")
            {
                sqlstr = "SELECT t2.Route_ID,t1.Smkg,t1.Afat,t1.ASnf FROM (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "' AND Plantcode='" + pcode + "' AND   RouteId='" + RCODE + "' AND prdate = '" + d1.ToString().Trim() + "' GROUP BY Routeid ) AS t1 LEFT JOIN  (SELECT Route_ID AS Rid, CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Route_ID)+'__'+Route_Name)) AS Route_ID  FROM Route_Master WHERE Plant_Code='" + pcode + "' AND ROUTE_ID='" + RCODE + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY t1.Routeid";
            }
            else if (rdocheck.SelectedItem.Value == "3")
            {
                sqlstr = "SELECT t2.Route_ID,t1.Smkg,t1.Afat,t1.ASnf FROM (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "' AND Plantcode='" + pcode + "' AND  RouteId='" + RCODE + "' AND  prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'  GROUP BY Routeid ) AS t1 LEFT JOIN  (SELECT Route_ID AS Rid, CONVERT(Nvarchar(13),(CONVERT(Nvarchar(10),Route_ID)+'__'+Route_Name)) AS Route_ID  FROM Route_Master WHERE Plant_Code='" + pcode + "' AND ROUTE_ID='" + RCODE + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY t1.Routeid";
            }
            else
            {
            }
            dt = dbaccess.GetDatatable(sqlstr);

            CreatMilkSummaryGraph();
        }
        catch (Exception ex)
        {
        }

    }


   

    private void CreatMilkSummaryGraph()
    {
         try
        {
        //string strCaption = ddl_reportname.SelectedItem.Text + "Report";
        string strCaption = " Total Milk Report";
     //   string strSubCaption = "For the Period From" + txt_frmdate.Text + " To  " + txt_todate.Text;
        string xAxis = "Route Name";
        string yAxis = "Sum Milkkg";

        //strXML will be used to store the entire XML document generated
        string strXML = null;

        //Generate the graph element
    //    strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='0' 
        strXML = @"<graph caption='" + strCaption + @"' subCaption='" +  @"' decimalPrecision='0' 
                          pieSliceDepth='10' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

        int i = 0;

        foreach (DataRow dr2 in dt.Rows)
        {
            strXML += "<set name='" + dr2[0].ToString() + "' value='" + dr2[1].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + "\tRid:" + dr2["Route_id"].ToString() + " + , +" + "\tSummkg: " + dr2["Smkg"].ToString() + " + , +" + "\tAvgFat:" + dr2["Afat"].ToString() + " + , +" + "\tAvgSnf:" + dr2["Asnf"].ToString() + "'); &quot;/>";
            i++;
        }

        //Finally, close <graph> element
        strXML += "</graph>";


        FCLiteral1.Text = FusionCharts.RenderChartHTML(
            "FusionCharts/FCF_Column3D.swf", // Path to chart's SWF
            "",                              // Leave blank when using Data String method
            strXML,                          // xmlStr contains the chart data
            "mygraph1",                      // Unique chart ID
            GraphWidth, GraphHeight,                   // Width & Height of chart
            false
            );

        }
         catch (Exception ex)
         {
         }

    }

    private void LoadAgentGraph(string ccode, string pcode,string RRCODE )
    {
        try
        {
            DateTime dtm1 = new DateTime();
            DateTime dtm2 = new DateTime();

            dtm1 = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            dtm2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);

            string d1 = dtm1.ToString("MM/dd/yyyy");
            string d2 = dtm2.ToString("MM/dd/yyyy");


            dtAgcou = null;
            dtAgcou = bllrou.GraphAgentCountSummary(ccode, pcode, RRCODE, d1, d2);
            CreateAgentCountSummaryGraph();
        }
        catch (Exception ex)
        {
        }

    }

    private void CreateAgentCountSummaryGraph()
    {
        try
        {
            string strCaption = "Total Agent Count Report";
            string strSubCaption = "For the Plant" + pname;
            string xAxis = "Route Name";
            string yAxis = "Sum Agentcount";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='0' 
                          pieSliceDepth='10' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow dr2 in dtAgcou.Rows)
            {
                strXML += "<set name='" + dr2[0].ToString() + "' value='" + dr2[1].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + "\tRid:" + dr2[0].ToString() + " + , +" + "\tAgentcount: " + dr2[1].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";


            FCLiteral2.Text = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Column3D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );

        }
        catch (Exception ex)
        {
        }
    }

    private void LoadremarksGraph(string ccode, string pcode)
    {
        try
        {
            DateTime dtm1 = new DateTime();
            DateTime dtm2 = new DateTime();
           
            dtm1 = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            dtm2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);

            string d1 = dtm1.ToString("MM/dd/yyyy");
            string d2 = dtm2.ToString("MM/dd/yyyy");

           
            dtRemarks = null;
            dtRemarks = bllrou.GraphRemarkscountSummary(ccode, pcode, d1, d2);

            CreateRemarksCountSummaryGraph();
        }
        catch (Exception ex)
        {
        }

    }

    private void CreateRemarksCountSummaryGraph()
    {
        try
        {
            string strCaption = "Total Remarks Count Report";
            string strSubCaption = "For the Period From" + txt_frmdate.Text + " To  " + txt_todate.Text;
            string xAxis = "Route Name";
            string yAxis = "Sum Remarkscount";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            //Generate the graph element
            strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='0' 
                          pieSliceDepth='10' formatNumberScale='0'
                          xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' rotateNames='1'
                    >";

            int i = 0;

            foreach (DataRow dr2 in dtRemarks.Rows)
            {
                strXML += "<set name='" + dr2[0].ToString() + "' value='" + dr2[1].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + "\tRid:" + dr2[0].ToString() + " + , +" + "\tRemarkscounts: " + dr2[1].ToString() + "'); &quot;/>";
                i++;
            }

            //Finally, close <graph> element
            strXML += "</graph>";


            FCLiteral3.Text = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Column3D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );
        }
        catch (Exception ex)
        {
        }
    }

    private DataTable GetData()
    {
        string d1, d2;
        frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
        todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = todat.ToString("MM/dd/yyyy");
        DataTable dt = new DataTable();
        string cmd = "SELECT Sessions AS item ,SUM(CAST(Milkkg AS DECIMAL(18,2))) AS value FROM lp  Where Prdate between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' AND sessions='AM' GROUP BY Sessions";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, conn1);
        adp.Fill(dt);
        return dt;
    }

    private string TodayDayMilk()
    {
        string d1, d2;
        frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
        todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);      
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = todat.ToString("MM/dd/yyyy");
        string Rval = string.Empty;
        string str = "SELECT SUM(CAST(Milkkg AS DECIMAL(18,2))) AS value FROM lp  Where Prdate between '" + d1.ToString().Trim() + "' AND '" + d1.ToString().Trim() + "' AND sessions='AM' GROUP BY Sessions";
        
        Rval = dbaccess.ExecuteScalarstr(str);       
        return Rval;
    }
    private string GetYesDayMilk()
    {
        string d1, d2;
        frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
        todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        frmdat = todat.AddDays(-1);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = todat.ToString("MM/dd/yyyy");
        string Rval = string.Empty;
        string str = "SELECT SUM(CAST(Milkkg AS DECIMAL(18,2))) AS value FROM lp  Where Prdate between '" + d1.ToString().Trim() + "' AND '" + d1.ToString().Trim() + "' AND sessions='AM' GROUP BY Sessions";

        Rval = dbaccess.ExecuteScalarstr(str);
        return Rval;
    }

    private void BindChart()    
    {
        try
        {
        DataTable dt = new DataTable();
        string Ymilk = GetYesDayMilk();
        string Tomilk = TodayDayMilk();        
       
        if ((Convert.ToDouble(Tomilk)) < (Convert.ToDouble(Ymilk)))
        {
            Ymilk = Tomilk;
        }

       
            dt = GetData();

            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*gauge*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'item');
        data.addColumn('number', 'value');      

        data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                //str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["item"].ToString() + "');");
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + "TotalMilkKg" + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["value"].ToString() + ") ;");
            }

          
            str.Append("var options = {width: 700,min:0,max:" + Ymilk + ", height: 350,redFrom: 80, redTo: 90,yellowFrom:65, yellowTo: 80,minorTicks:5 };");
           // str.Append("var options = {width: 700,min:0,max:100000, height: 350,redFrom: 80, redTo: 90,yellowFrom:65, yellowTo: 80,minorTicks:5 };");
            str.Append(" var chart = new google.visualization.Gauge(document.getElementById('chart_div'));");
            str.Append(" chart.draw(data, options); }");
            str.Append("</script>");
            lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');
        }
        catch
        { }
    }
   

    protected void Rdl_Plantype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Lbl_PlantName.Visible = false;
            lbl_Plantcode.Visible = false;
            gv_PlantRouteMilkDetail.Visible = false;
            FCLiteral1.Visible = false;
            FCLiteral2.Visible = false;
            FCLiteral3.Visible = false;
            LtLine.Visible = false;
            lbl_Milkkg.Visible = false;
          //  Lbl_Amilkkg.Visible = false;
            //Get_PlantMilkStatus(ccode, Rdl_Plantype.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Get_PlantMilkStatus(string ccode, string PlantMilkType)
    {
        try
        {
            decimal total = 0, Fat = 0, Snf = 0;
            int Plantype = Convert.ToInt32(PlantMilkType);
            string d1, d2;
            string str = string.Empty;

            frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            d1 = frmdat.ToString("MM/dd/yyyy");
            d2 = todat.ToString("MM/dd/yyyy");
            DataTable dt = new DataTable();
            if (Plantype == 3)
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND Sessions='AM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                    else
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND Sessions='PM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else if (rdocheck.SelectedItem.Value == "3")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else
                {
                }
                
            }
            else if (Plantype == 2)
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {

                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                    else
                    {

                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='PM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {

                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else if (rdocheck.SelectedItem.Value == "3")
                {

                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else
                {
                }

                
            }
            else
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                        
                    }
                    else
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND Sessions='PM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                        
                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate='" + d1.ToString() + "' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                   
                }
                else if (rdocheck.SelectedItem.Value == "3")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                   
                }
                else
                {
                }


                
            }
            using (con = dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dt.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dt.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    gv_PlantMilkDetail.FooterRow.Cells[1].Text = "Total/Avg";
                    gv_PlantMilkDetail.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    gv_PlantMilkDetail.FooterRow.Cells[2].Text = total.ToString("N2");
                    gv_PlantMilkDetail.FooterRow.Cells[3].Text = Fat.ToString("f2");
                    gv_PlantMilkDetail.FooterRow.Cells[4].Text = Snf.ToString("f1");

                }

                else
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "NO DATA...";
                }

                if (dt.Rows.Count > 0)
                {
                    int rowcount = dt.Rows.Count;
                    DataRow rows = dt.NewRow();
                    rows["Plant"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");

                    dt.Rows.Add(rows);

                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //// color
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].ForeColor = Color.White;
                }
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }

    private void Get_PlantMilkStatus1(string ccode, string PlantMilkType)
    {
        try
        {
            decimal total = 0, Fat = 0, Snf = 0;
            int Plantype = Convert.ToInt32(PlantMilkType);
            string d1, d2;
            string str = string.Empty;

            frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            d1 = frmdat.ToString("MM/dd/yyyy");
            d2 = todat.ToString("MM/dd/yyyy");
            DataTable dt = new DataTable();
            if (Plantype == 3)
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND PlantCode='170'  AND   Sessions='AM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and plant_code='170' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                    else
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND   PlantCode='170' AND Sessions='PM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and plant_code='170' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "'   AND   PlantCode='170'    GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and plant_code='170' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else if (rdocheck.SelectedItem.Value == "3")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  AND   PlantCode='170' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "'  and plant_code='170' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else
                {
                }

            }
            else if (Plantype == 2)
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {

                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM' and PlantCode='170'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "'  and plant_code='170' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                    else
                    {

                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='PM' and PlantCode='170' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "'  and plant_code='170'  AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {

                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' and PlantCode='170' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "'  and plant_code='170' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else if (rdocheck.SelectedItem.Value == "3")
                {

                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' and PlantCode='170' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "'  and plant_code='170' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else
                {
                }


            }
            else
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM' and PlantCode='170'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and Plant_Code='170' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                    }
                    else
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND Sessions='PM' and PlantCode='170'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and Plant_Code='170' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate='" + d1.ToString() + "' and PlantCode='170' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and Plant_Code='170' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                }
                else if (rdocheck.SelectedItem.Value == "3")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' and PlantCode='170'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and Plant_Code='170' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                }
                else
                {
                }



            }
            using (con = dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dt.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dt.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    gv_PlantMilkDetail.FooterRow.Cells[1].Text = "Total/Avg";
                    gv_PlantMilkDetail.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    gv_PlantMilkDetail.FooterRow.Cells[2].Text = total.ToString("N2");
                    gv_PlantMilkDetail.FooterRow.Cells[3].Text = Fat.ToString("f2");
                    gv_PlantMilkDetail.FooterRow.Cells[4].Text = Snf.ToString("f1");

                }

                else
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "NO DATA...";
                }

                if (dt.Rows.Count > 0)
                {
                    int rowcount = dt.Rows.Count;
                    DataRow rows = dt.NewRow();
                    rows["Plant"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");

                    dt.Rows.Add(rows);

                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //// color
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].ForeColor = Color.White;
                }
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }


    private void Get_PlantMilkStatus1(string ccode, string PPCODE, string PlantMilkType)
    {
        try
        {
            decimal total = 0, Fat = 0, Snf = 0;
            int Plantype = Convert.ToInt32(PlantMilkType);
            string d1, d2;
            string str = string.Empty;

            frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            d1 = frmdat.ToString("MM/dd/yyyy");
            d2 = todat.ToString("MM/dd/yyyy");
            DataTable dt = new DataTable();
            if (Plantype == 3)
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE  plantcode='" + PPCODE + "' and  CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND Sessions='AM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE  plant_code='" + PPCODE + "'and Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                    else
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE  plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND Sessions='PM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE  plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE  plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE   plant_code='" + PPCODE + "' and  Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else if (rdocheck.SelectedItem.Value == "3")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE  plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE  plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else
                {
                }

            }
            else if (Plantype == 2)
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {

                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                    else
                    {

                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='PM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {

                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else if (rdocheck.SelectedItem.Value == "3")
                {

                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
                }
                else
                {
                }


            }
            else
            {
                if (rdocheck.SelectedItem.Value == "1")
                {
                    if (ddl_Sessions.SelectedItem.Value == "1")
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE  plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                    }
                    else
                    {
                        str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND Sessions='PM'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                    }
                }
                else if (rdocheck.SelectedItem.Value == "2")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate='" + d1.ToString() + "' GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                }
                else if (rdocheck.SelectedItem.Value == "3")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Plant_Code ) AS Sno ,Convert(Nvarchar(15),pcode) +'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(PlantCode) AS pcode,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE plantcode='" + PPCODE + "' and CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY PlantCode ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE plant_code='" + PPCODE + "' and Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";

                }
                else
                {
                }



            }
            using (con = dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dt.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dt.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    gv_PlantMilkDetail.FooterRow.Cells[1].Text = "Total/Avg";
                    gv_PlantMilkDetail.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    gv_PlantMilkDetail.FooterRow.Cells[2].Text = total.ToString("N2");
                    gv_PlantMilkDetail.FooterRow.Cells[3].Text = Fat.ToString("f2");
                    gv_PlantMilkDetail.FooterRow.Cells[4].Text = Snf.ToString("f1");

                }

                else
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "NO DATA...";
                }

                if (dt.Rows.Count > 0)
                {
                    int rowcount = dt.Rows.Count;
                    DataRow rows = dt.NewRow();
                    rows["Plant"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");

                    dt.Rows.Add(rows);

                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //// color
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].ForeColor = Color.White;
                }
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }

    protected void gv_PlantMilkDetail_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
    {
        string index = string.Empty;
        lbl_Milkkg.Visible = true;
       // Lbl_Amilkkg.Visible = true;
        try
        {
            index = gv_PlantMilkDetail.Rows[e.NewSelectedIndex].Cells[2].Text;
            gv_PlantRouteMilkDetail.Visible = true;
            Lbl_PlantName.Visible = true;
            lbl_Plantcode.Visible = true;
            FCLiteral1.Visible = false;
            FCLiteral2.Visible = false;
            FCLiteral3.Visible = false;
            LtLine.Visible = false;

            Lbl_PlantName.Visible = true;
            lbl_Plantcode.Visible = true;
            lbl_Milkkg.Visible = true;

            lbl_Plantcode.Text = index;
            string[] strarr = new string[2];
            strarr = index.ToString().Split('_');
            index = strarr[0];
            Get_RouteMilkStatus(ccode, index.ToString());

            Get_PlantMilkStatus1(ccode, index, Rdl_Plantype.SelectedItem.Value);

            //
           // BindChart();
        //    ConfigureColors();
            //Get_RouteMilkStatus(ccode, index.ToString());
            //LoadMilkGraph(ccode, index.ToString());
            //LoadAgentGraph(ccode, index.ToString());
            //LoadremarksGraph(ccode, index.ToString());

            // Line graph
            //LoadRouteId(index);
            //LoadGraphData(index);
            //CreateLineGraph();
            //
           
           
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


    private void Get_RouteMilkStatus1(string ccode, string pcode, string RRCODE)
    {
        try
        {
            string str = string.Empty;
            string d1, d2;
            decimal total = 0, Fat = 0, Snf = 0;
            frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            d1 = frmdat.ToString("MM/dd/yyyy");
            d2 = todat.ToString("MM/dd/yyyy");
            DataTable dtr1 = new DataTable();
            //
            if (rdocheck.SelectedItem.Value == "1")
            {
                if (ddl_Sessions.SelectedItem.Value == "1")
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno,RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
 " (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM'  AND Plantcode='" + pcode + "' and Routeid='" + RRCODE + "'  GROUP BY Plantcode,Routeid ) AS t1 " +
 " INNER JOIN " +
 " (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "' and Route_id='" + RRCODE + "' ) AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
                }
                else
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno, RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
 " (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='PM' AND Plantcode='" + pcode + "' and Routeid='" + RRCODE + "'   GROUP BY Plantcode,Routeid ) AS t1 " +
 " INNER JOIN " +
 " (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "' and Route_id='" + RRCODE + "' ) AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
                }
            }
            else if (rdocheck.SelectedItem.Value == "2")
            {
                str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno, RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "'  AND Plantcode='" + pcode + "' and Routeid='" + RRCODE + "'   GROUP BY Plantcode,Routeid ) AS t1 " +
" INNER JOIN " +
" (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "' and Route_id='" + RRCODE + "' ) AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
            }
            else if (rdocheck.SelectedItem.Value == "3")
            {
                str = "SELECT ROW_NUMBER()OVER(order by Routeid ) AS Sno, RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plantcode='" + pcode + "'  and Routeid='" + RRCODE + "'  GROUP BY Plantcode,Routeid ) AS t1 " +
" INNER JOIN " +
" (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "' and Route_id='" + RRCODE + "' ) AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
            }
            else
            {
            }

            using (con = dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dtr1);
                if (dtr1.Rows.Count > 0)
                {
                    gv_PlantRouteMilkDetail.DataSource = dtr1;
                    gv_PlantRouteMilkDetail.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dtr1.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dtr1.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dtr1.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    gv_PlantRouteMilkDetail.FooterRow.Cells[1].Text = "Total/Avg";
                    gv_PlantRouteMilkDetail.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                    gv_PlantRouteMilkDetail.FooterRow.Cells[2].Text = total.ToString("N2");
                    gv_PlantRouteMilkDetail.FooterRow.Cells[3].Text = Fat.ToString("f2");
                    gv_PlantRouteMilkDetail.FooterRow.Cells[4].Text = Snf.ToString("f2");

                    gv_PlantRouteMilkDetail.FooterRow.Font.Bold = true;
                    lbl_Milkkg.Visible = true;
                    lbl_Milkkg.Text = total.ToString();
                }
                else
                {
                    //gv_PlantMilkDetail.DataSource = dt;
                    //gv_PlantMilkDetail.DataBind();
                    //Lbl_Errormsg.Visible = true;
                    //Lbl_Errormsg.Text = "NO DATA...";
                    gv_PlantRouteMilkDetail.DataSource = dtr1;
                    gv_PlantRouteMilkDetail.DataBind();
                }
                if (dtr1.Rows.Count > 0)
                {

                    DataRow rows = dtr1.NewRow();
                    rows["RouteName"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");

                    dtr1.Rows.Add(rows);

                    gv_PlantRouteMilkDetail.DataSource = dtr1;
                    gv_PlantRouteMilkDetail.DataBind();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }


   

    private void Get_AgentMilkStatus(string ccode, string pcode, string Rid)
    {
        try
        {
            string str = string.Empty;
          //  Lbl_Amilkkg.Visible = true;
            string d1, d2;
            decimal total = 0, Fat = 0, Snf = 0;
            frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
            todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            d1 = frmdat.ToString("MM/dd/yyyy");
            d2 = todat.ToString("MM/dd/yyyy");
            DataTable dt = new DataTable();
            //
            if (rdocheck.SelectedItem.Value == "1")
            {
                if (ddl_Sessions.SelectedItem.Value == "1")
                {

                    str = "SELECT ROW_NUMBER()OVER(order by Agentid ) AS Sno ,CONVERT(nvarchar(60),AgentName) AS AgentName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Agentid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM' AND PlantCode='" + pcode + "' AND RouteId='" + Rid + "'  GROUP BY Plantcode,Agentid ) AS t1 " +
" INNER JOIN " +
" (SELECT Agent_Id AS Aid,CONVERT(nvarchar(10),Agent_Id)+'_'+Agent_Name AS AgentName FROM Agent_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Agentid=t2.Aid ORDER BY RAND(Agentid)";
                }
                else
                {
                    str = "SELECT ROW_NUMBER()OVER(order by Agentid ) AS Sno, CONVERT(nvarchar(60),AgentName) AS AgentName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Agentid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND Sessions='AM'  AND PlantCode='" + pcode + "' AND RouteId='" + Rid + "'  GROUP BY Plantcode,Agentid ) AS t1 " +
" INNER JOIN " +
" (SELECT Agent_Id AS Aid,CONVERT(nvarchar(10),Agent_Id)+'_'+Agent_Name AS AgentName FROM Agent_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Agentid=t2.Aid ORDER BY RAND(Agentid)";
                }
            }
            else if (rdocheck.SelectedItem.Value == "2")
            {
                str = "SELECT ROW_NUMBER()OVER(order by Agentid ) AS Sno, CONVERT(nvarchar(60),AgentName) AS AgentName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
  " (SELECT Agentid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "'  AND  prdate = '" + d1.ToString() + "' AND PlantCode='" + pcode + "' AND RouteId='" + Rid + "'  GROUP BY Plantcode,Agentid ) AS t1 " +
  " INNER JOIN " +
  " (SELECT Agent_Id AS Aid,CONVERT(nvarchar(10),Agent_Id)+'_'+Agent_Name AS AgentName FROM Agent_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Agentid=t2.Aid ORDER BY RAND(Agentid)";
            }
            else if (rdocheck.SelectedItem.Value == "3")
            {
                str = "SELECT ROW_NUMBER()OVER(order by Agentid ) AS Sno, CONVERT(nvarchar(60),AgentName) AS AgentName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Agentid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM LP WHERE Companycode='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND PlantCode='" + pcode + "' AND RouteId='" + Rid + "'  GROUP BY Plantcode,Agentid ) AS t1 " +
" INNER JOIN " +
" (SELECT Agent_Id AS Aid,CONVERT(nvarchar(10),Agent_Id)+'_'+Agent_Name AS AgentName FROM Agent_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Agentid=t2.Aid ORDER BY RAND(Agentid)";
            }
            else
            {

            }
             
            using (con = dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gv_PlantAgentMilkDetail1.DataSource = dt;
                    gv_PlantAgentMilkDetail1.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dt.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dt.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    gv_PlantAgentMilkDetail1.FooterRow.Cells[0].Text = "Total/Avg";
                    gv_PlantAgentMilkDetail1.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    gv_PlantAgentMilkDetail1.FooterRow.Cells[1].Text = total.ToString("N2");
                    gv_PlantAgentMilkDetail1.FooterRow.Cells[2].Text = Fat.ToString("f2");
                    gv_PlantAgentMilkDetail1.FooterRow.Cells[3].Text = Snf.ToString("f2");
                }
                else
                {
                    gv_PlantAgentMilkDetail1.DataSource = dt;
                    gv_PlantAgentMilkDetail1.DataBind();
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "NO DATA...";
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow rows = dt.NewRow();
                    rows["AgentName"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");
                    dt.Rows.Add(rows);
                //    Lbl_Amilkkg.Text = total.ToString();
                    gv_PlantAgentMilkDetail1.DataSource = dt;
                    gv_PlantAgentMilkDetail1.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = "NO DATA...";
        }
    }

   
    protected void btn_Aclose_Click(object sender, EventArgs e)
    {
      //  this.ModalPopupExtender2.Hide();
    }
    protected void gv_PlantAgentMilkDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
    }
    protected void gv_PlantRouteMilkDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;

          


        }
    }


    //Line Graph for Plant Start


    private DataTable LoadRouteId(string pcode)
    {
        string d1, d2;

        frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
        todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = todat.ToString("MM/dd/yyyy");

        conn1.Open();
        //string cmd = "select year,purchase,sales,expences from Sales";
        // string cmd = "SELECT Route_id AS year,COUNT(Agent_Id) AS Sales,COUNT(Route_id) AS Expences,COUNT(Route_id) AS purchase FROM Agent_Master where Plant_code=155 GRoup By Route_id    Order By Route_id";
        // string cmd = "SELECT Convert(Nvarchar(7),EndingTime) AS year,(REPLACE( Convert(Nvarchar(2),EndingTime), ':', '' )) AS purchase FROM lp Where PlantCode='" + pcode + "' AND Prdate='" + d1.ToString().Trim() + "'  AND Sessions='AM' AND RouteId='" + Rid + "' ";
        string cmd = "SELECT DISTINCT(RouteId) AS RouteId FROM lp Where PlantCode='" + pcode + "' AND Prdate='" + d1.ToString().Trim() + "'  AND Sessions='AM' AND  RouteId IN(6)    order by RouteId Asc ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, conn1);
        adp.Fill(dtRid);
        conn1.Close();
        return dtRid;
    } 

    private DataTable LoadGraphData( string pcode)
    {
        string d1, d2;

        frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
        todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = todat.ToString("MM/dd/yyyy");
        
        conn1.Open();
        //string cmd = "select year,purchase,sales,expences from Sales";
        // string cmd = "SELECT Route_id AS year,COUNT(Agent_Id) AS Sales,COUNT(Route_id) AS Expences,COUNT(Route_id) AS purchase FROM Agent_Master where Plant_code=155 GRoup By Route_id    Order By Route_id";
       // string cmd = "SELECT Convert(Nvarchar(7),EndingTime) AS year,(REPLACE( Convert(Nvarchar(2),EndingTime), ':', '' )) AS purchase FROM lp Where PlantCode='" + pcode + "' AND Prdate='" + d1.ToString().Trim() + "'  AND Sessions='AM' AND RouteId='" + Rid + "' ";
        // LASt string cmd = "SELECT DISTINCT(Convert(Nvarchar(5),EndingTime)) AS year,(REPLACE( Convert(Nvarchar(5),EndingTime), ':', '.' ))AS purchase,(REPLACE( Convert(Nvarchar(2),EndingTime), ':', '' ))AS sales,RouteId FROM lp Where PlantCode='" + pcode + "' AND Prdate='" + d1.ToString().Trim() + "'  AND Sessions='AM' AND Routeid=9  order by year Asc ";
        string cmd = "SELECT EndingTime AS year,(REPLACE( Convert(Nvarchar(5),EndingTime), ':', '.' ))AS purchase,(REPLACE( Convert(Nvarchar(2),EndingTime), ':', '' )-4)AS sales,RouteId FROM lp Where PlantCode='" + pcode + "' AND Prdate='" + d1.ToString().Trim() + "'  AND Sessions='AM' AND  RouteId IN(6)   order by Tid,year,Routeid Asc ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, conn1);
        adp.Fill(dt1);
        conn1.Close();
        return dt1;
    } 

    private void CreateLineGraph()
    {
        try
        {
            ConfigureColors();

            string strCaption = "Routewise TimeBased Report";
            string strSubCaption = "";
            string xAxis = "capturing timing intervals ";
            string yAxis = "Time Stamp";

            //strXML will be used to store the entire XML document generated
            string strXML = null;

            ////        strXML = @"                
            ////            <graph caption='" + strCaption + @"' 
            ////            subcaption='" + strSubCaption + @"'
            ////            hovercapbg='FFECAA' hovercapborder='F47E00' formatNumberScale='0' decimalPrecision='2' 
            ////            showvalues='0' numdivlines='3' numVdivlines='0' yaxisminvalue='80.00' yaxismaxvalue='80.00'  
            ////            rotateNames='1'
            ////            showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' 
            ////            divLineAlpha='20' alternateHGridAlpha='5' 
            ////            xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' 
            ////            >        
            ////            ";

            //Generate the graph element
            strXML = @"                
            <graph caption='" + strCaption + @"' 
            subcaption='" + strSubCaption + @"'
            hovercapbg='FFECAA' hovercapborder='F47E00' formatNumberScale='0' decimalPrecision='0' 
            showvalues='0' numdivlines='11' numVdivlines='0' yaxisminvalue='1' yaxismaxvalue='13' 
            rotateNames='1'
            showAlternateHGridColor='1' AlternateHGridColor='ff5904' divLineColor='ff5904' 
            divLineAlpha='5' alternateHGridAlpha='6' 
            xAxisName='" + xAxis + @"' yAxisName='" + yAxis + @"' 
            >        
            ";

            string tmp = null;
            tmp = @"<categories font='Arial' fontSize='11' fontColor='000000'>";
            //time start
            object id1v;
            id1v = "0";
            int idd1v = Convert.ToInt32(id1v);

            foreach (DataRow dr1 in dtRid.Rows)
            {
                object id;

                id = dr1[0].ToString();
                int idd = Convert.ToInt32(id);
                int j = 0;

                if (idd1v == idd)
                {


                }
                else
                {
                    DataRow[] result = dt1.Select("RouteId=" + idd);
                    // int strleng = 0;
                    string strvari = string.Empty;
                    string strvariRes = string.Empty;
                    string[] strlengharr = new string[4];
                    string[] strlengharrRes = new string[4];

                    foreach (DataRow DR1 in result)
                    {
                        // length of string
                        strvari = DR1["purchase"].ToString().Trim();
                        strlengharr = strvari.Split('.');
                        strlengharrRes[0] = strlengharr[0];
                        strlengharrRes[1] = ".";
                        strlengharrRes[2] = strlengharr[1];
                        strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
                        //                

                        //tmp += @"<category name='" + DR["year"].ToString().Trim() + @"' />";
                        tmp += @"<category name='" + strvariRes.ToString().Trim() + @"' />";


                    }

                    tmp += @"</categories>";


                    j++;

                }

            }





            //time end


            strXML += tmp;


            //tmp = @"<dataset seriesName='TIME' color='D64646' anchorBorderColor='D64646' anchorBgColor='D64646'>";
            //foreach (DataRow DR in dt1.Rows)
            //{
            //    tmp += @"<set value='" + DR["purchase"].ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + DR["purchase"].ToString() + "'); &quot; />";
            //}

            //tmp += @"</dataset>";


            //


            object id1;
            id1 = "0";
            int idd1 = Convert.ToInt32(id1);
            int i = 0;
            foreach (DataRow dr1 in dtRid.Rows)
            {
                object id;

                id = dr1[0].ToString();
                int idd = Convert.ToInt32(id);

                if (idd1 == idd)
                {
                    DataRow[] result = dt1.Select("RouteId=" + idd);
                    // int strleng = 0;
                    string strvari = string.Empty;
                    string strvariRes = string.Empty;
                    string[] strlengharr = new string[4];
                    string[] strlengharrRes = new string[4];

                    clrdisplay = color[i];
                    tmp += @"<dataset seriesName='" + idd + "' color='" + clrdisplay + "' anchorBorderColor='" + clrdisplay + "' anchorBgColor='" + clrdisplay + "'>";
                    foreach (DataRow DR in result)
                    {
                        // length of string
                        strvari = DR["purchase"].ToString().Trim();
                        strlengharr = strvari.Split('.');
                        strlengharrRes[0] = strlengharr[0];
                        strlengharrRes[1] = ".";
                        strlengharrRes[2] = strlengharr[1];
                        strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
                        //

                        //  tmp += @"<set value='" + DR["purchase"].ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + DR["RouteId"].ToString() + "'); &quot; />";
                        tmp += @"<set value='" + strvariRes.ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + idd.ToString() + "'); &quot; />";
                    }

                    tmp += @"</dataset>";

                    idd1 = idd;


                }
                else
                {
                    DataRow[] result = dt1.Select("RouteId=" + idd);
                    // int strleng = 0;
                    string strvari = string.Empty;
                    clrdisplay = color[i];
                    string strvariRes = string.Empty;
                    string[] strlengharr = new string[4];
                    string[] strlengharrRes = new string[4];
                    tmp += @"<dataset seriesName='" + idd + "' color='" + clrdisplay + "' anchorBorderColor='" + clrdisplay + "' anchorBgColor='" + clrdisplay + "'>";
                    foreach (DataRow DR in result)
                    {
                        // length of string
                        strvari = DR["purchase"].ToString().Trim();
                        strlengharr = strvari.Split('.');
                        strlengharrRes[0] = strlengharr[0];
                        strlengharrRes[1] = ".";
                        strlengharrRes[2] = strlengharr[1];
                        strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
                        //

                        //  tmp += @"<set value='" + DR["purchase"].ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + DR["RouteId"].ToString() + "'); &quot; />";
                        tmp += @"<set value='" + strvariRes.ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + idd.ToString() + "'); &quot; />";
                    }

                    tmp += @"</dataset>";

                    idd1 = idd;


                }
                i++;
            }






            //



            //tmp += @"<dataset seriesName='Sales' color='008E8E' anchorBorderColor='008E8E' anchorBgColor='008E8E'>";
            //foreach (DataRow DR in dt.Rows)
            //{
            //    tmp += @"<set value='" + DR["sales"].ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + DR["sales"].ToString() + "'); &quot; />";
            //}

            //tmp += @"</dataset>";

            //tmp += @"<dataset seriesName='Expences' color='D64646' anchorBorderColor='D64646' anchorBgColor='D64646'>";
            //foreach (DataRow DR in dt.Rows)
            //{
            //    tmp += @"<set value='" + DR["expences"].ToString().Trim() + @"'  link=&quot;JavaScript:myJS('" + DR["year"].ToString() + ", " + DR["expences"].ToString() + "'); &quot; />";
            //}

            //tmp += @"</dataset>";

            strXML += tmp;

            strXML += "</graph>";
            LtLine.Text = "";

            LtLine.Text = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_MSLine.swf",   // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph4",                      // Unique chart ID
                GraphWidth1, GraphHeight1,         // Width & Height of chart
                false
                );
        }
        catch (Exception ex)
        {
        }
    }


    private DataTable LoadGraphDataRoute(string Rid)
    {
        string d1, d2;

        frmdat = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
        todat = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = todat.ToString("MM/dd/yyyy");

        conn1.Open();
        //string cmd = "select year,purchase,sales,expences from Sales";
        // string cmd = "SELECT Route_id AS year,COUNT(Agent_Id) AS Sales,COUNT(Route_id) AS Expences,COUNT(Route_id) AS purchase FROM Agent_Master where Plant_code=155 GRoup By Route_id    Order By Route_id";
        // string cmd = "SELECT Convert(Nvarchar(7),EndingTime) AS year,(REPLACE( Convert(Nvarchar(2),EndingTime), ':', '' )) AS purchase FROM lp Where PlantCode='" + pcode + "' AND Prdate='" + d1.ToString().Trim() + "'  AND Sessions='AM' AND RouteId='" + Rid + "' ";
        string cmd = "SELECT Convert(Nvarchar(5),EndingTime) AS year,(REPLACE( Convert(Nvarchar(2),EndingTime), ':', '' ))AS purchase,RouteId FROM lp Where PlantCode='" + pcode + "' AND Prdate='" + d1.ToString().Trim() + "'  AND Sessions='AM' AND RouteId='" + Rid + "' order by tid Asc ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, conn1);
        adp.Fill(dt1);
        conn1.Close();
        return dt1;
    } 

    //Line Graph for Plant End




    protected void gv_PlantRouteMilkDetail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

       

        string index = string.Empty;       

        try
        {
            pcode = lbl_Plantcode.Text;
            string[] pcodearr = new string[2];
            pcodearr = pcode.ToString().Split('_');
            pcode = pcodearr[0];

            index = gv_PlantRouteMilkDetail.Rows[e.NewSelectedIndex].Cells[2].Text;
         //   Lbl_Routecode.Text = index;
            string[] strarr = new string[2];
            strarr = index.ToString().Split('_');
            index = strarr[0];


            Get_RouteMilkStatus1(ccode, pcode, index);
            Get_AgentMilkStatus(ccode, pcode, index.ToString());


            LoadMilkGraph(ccode, pcode, index.ToString());
            LoadAgentGraph(ccode,pcode, index.ToString());




            gv_PlantAgentMilkDetail1.Visible = true  ;
            gv_PlantMilkDetail.Visible = true;
            LtLine.Visible = true;
            FCLiteralLine.Visible = true;
            FCLiteral1.Visible = true;
            FCLiteral2.Visible = true;
            FCLiteral3.Visible = true;
            lt.Visible = true;

            if (index == "Total/Avg")
            {
              //  this.ModalPopupExtender2.Hide();
            }
            else
            {
               // this.ModalPopupExtender2.Show();
            }

            // Line graph
            //LoadRouteId(index);
            //LoadGraphDataRoute(index);
            //CreateLineGraph();
            //
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    protected void gv_PlantAgentMilkDetail_SelectedIndexChanged(object sender, EventArgs e)
    {

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

                    Label5.Visible = false;
                    txt_todate.Visible = false;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = true;
                    ddl_Sessions.Visible = true;
                    

                }


                if (value == "2")
                {

                    Label5.Visible = false;
                    txt_todate.Visible = false;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;
                   

                }

                if (value == "3")
                {
                    Label5.Visible = true;
                    txt_todate.Visible = true;
                    Label4.Visible = true;
                    txt_frmdate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;
                    

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
    protected void gv_PlantMilkDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string stt = e.Row.Cells[2].Text;


            if (stt == "Total/Avg")
            {

                e.Row.Cells[0].Enabled = false;
            }
           

        }
    }
}