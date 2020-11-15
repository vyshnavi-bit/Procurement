using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InfoSoftGlobal;
using System.Data.SqlClient;
using System.Data;

public partial class Frm_ChartRoute : System.Web.UI.Page
{
    public string ccode = string.Empty;
    public string pcode = string.Empty;
    BLLuser Bllusers = new BLLuser();

    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();


    Bllbillgenerate BLLBill = new Bllbillgenerate();
    BOLbillgenerate BOLBill = new BOLbillgenerate();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLpayment Bllpay = new BLLpayment();
    DateTime dtm = new DateTime();
    BLLroutmaster bllrou = new BLLroutmaster();
    DataTable dt = new DataTable("Chart");

    string X, Y;
    string GraphWidth = "600";
    string GraphHeight = "330";
    string[] color = new string[25];

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
                //cname = Session["cname"].ToString();
                //pname = Session["pname"].ToString();
                //managmobNo = Session["managmobNo"].ToString();








                dtm = System.DateTime.Now;
                txt_frmdate.Text = dtm.ToString("dd/MM/yyyy");
                txt_todate.Text = dtm.ToString("dd/MM/yyyy");


                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                else
                {
                    LoadPlantName();
                }

                pcode = ddl_Plantname.SelectedItem.Value;

                //LoadPlantcode();
                //pcode = ddl_plantcode.SelectedItem.Value;

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
             //   pcode = ddl_Plantcode.SelectedItem.Value;

                pcode = ddl_Plantname.SelectedItem.Value;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
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
    private void LoadGraphData()
    {
        DateTime dtm1 = new DateTime();
        DateTime dtm2 = new DateTime();
        dtm1 = DateTime.ParseExact(txt_frmdate.Text, "dd/MM/yyyy", null);
        dtm2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);

        string d1 = dtm1.ToString("MM/dd/yyyy");
        string d2 = dtm2.ToString("MM/dd/yyyy");
        if (ddl_reportname.SelectedItem.Value == "1")
        {
            if (rd_plant.Checked == true)
            {
                dt = null;
                dt = bllrou.GraphMilkSummaryplantwise(ccode, d1, d2);
            }
            else
            {
                dt = null;
                dt = bllrou.GraphMilkSummary(ccode, pcode, d1, d2);
            }

            CreatMilkSummaryGraph();
        }
        else if (ddl_reportname.SelectedItem.Value == "2")
        {
            if (rd_plant.Checked == true)
            {
                dt = null;
                dt = bllrou.GraphAgentCountSummaryplantwise(ccode, d1, d2);
            }
            else
            {
                dt = null;
             //   dt = bllrou.GraphAgentCountSummary(ccode, pcode, d1, d2);
            }
            CreateAgentCountSummaryGraph();
        }
        else if (ddl_reportname.SelectedItem.Value == "3")
        {
            if (rd_plant.Checked == true)
            {
                dt = null;
                dt = bllrou.GraphRemarkscountSummaryplantwise(ccode, d1, d2);
            }
            else
            {
                dt = null;
                dt = bllrou.GraphRemarkscountSummary(ccode, pcode, d1, d2);
            }
            CreateRemarksCountSummaryGraph();
        }
    }

    private void CreatMilkSummaryGraph()
    {
        string strCaption = ddl_reportname.SelectedItem.Text + "Report";
        string strSubCaption = "For the Period From" + txt_frmdate.Text + " To  " + txt_todate.Text;
        string xAxis = "Route Name";
        string yAxis = "Sum Milkkg";

        //strXML will be used to store the entire XML document generated
        string strXML = null;

        //Generate the graph element
        strXML = @"<graph caption='" + strCaption + @"' subCaption='" + strSubCaption + @"' decimalPrecision='0' 
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

        if (ddl_charttype.SelectedItem.Value == "1")
        {
            FCLiteral1.Text = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Column3D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );
        }
        else
        {
            FCLiteral1.Text = FusionCharts.RenderChartHTML(
           "FusionCharts/FCF_Pie3D.swf", // Path to chart's SWF
           "",                              // Leave blank when using Data String method
           strXML,                          // xmlStr contains the chart data
           "mygraph1",                      // Unique chart ID
           GraphWidth, GraphHeight,                   // Width & Height of chart
           false
           );
        }
    }

    private void CreateAgentCountSummaryGraph()
    {
        string strCaption = ddl_reportname.SelectedItem.Text + "Report";
        string strSubCaption = "For the Plant" + ddl_Plantname.SelectedItem.Value;
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

        foreach (DataRow dr2 in dt.Rows)
        {
            strXML += "<set name='" + dr2[0].ToString() + "' value='" + dr2[1].ToString() + "' color='" + color[1] + @"'  link=&quot;JavaScript:myJS('" + "\tRid:" + dr2[0].ToString() + " + , +" + "\tAgentcount: " + dr2[1].ToString() + "'); &quot;/>";
            i++;
        }

        //Finally, close <graph> element
        strXML += "</graph>";

        if (ddl_charttype.SelectedItem.Value == "1")
        {
            FCLiteral1.Text = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Column3D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );
        }
        else
        {
            FCLiteral1.Text = FusionCharts.RenderChartHTML(
           "FusionCharts/FCF_Pie3D.swf", // Path to chart's SWF
           "",                              // Leave blank when using Data String method
           strXML,                          // xmlStr contains the chart data
           "mygraph1",                      // Unique chart ID
           GraphWidth, GraphHeight,                   // Width & Height of chart
           false
           );
        }
    }


    private void CreateRemarksCountSummaryGraph()
    {
        string strCaption = ddl_reportname.SelectedItem.Text + "Report";
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

        foreach (DataRow dr2 in dt.Rows)
        {
            strXML += "<set name='" + dr2[0].ToString() + "' value='" + dr2[1].ToString() + "' color='" + color[i] + @"'  link=&quot;JavaScript:myJS('" + "\tRid:" + dr2[0].ToString() + " + , +" + "\tRemarkscounts: " + dr2[1].ToString() + "'); &quot;/>";
            i++;
        }

        //Finally, close <graph> element
        strXML += "</graph>";

        if (ddl_charttype.SelectedItem.Value == "1")
        {
            FCLiteral1.Text = FusionCharts.RenderChartHTML(
                "FusionCharts/FCF_Column3D.swf", // Path to chart's SWF
                "",                              // Leave blank when using Data String method
                strXML,                          // xmlStr contains the chart data
                "mygraph1",                      // Unique chart ID
                GraphWidth, GraphHeight,                   // Width & Height of chart
                false
                );
        }
        else
        {
            FCLiteral1.Text = FusionCharts.RenderChartHTML(
           "FusionCharts/FCF_Pie3D.swf", // Path to chart's SWF
           "",                              // Leave blank when using Data String method
           strXML,                          // xmlStr contains the chart data
           "mygraph1",                      // Unique chart ID
           GraphWidth, GraphHeight,                   // Width & Height of chart
           false
           );
        }
    }


    protected void rd_route_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_route.Checked == true)
        {
            rd_plant.Checked = false;
            Lbl_plantname.Visible = true;
            ddl_Plantname.Visible = true;
        }
    }
    protected void rd_plant_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_plant.Checked == true)
        {
            rd_route.Checked = false;
            Lbl_plantname.Visible = false;
            ddl_Plantname.Visible = false;
        }

    }
    protected void ddl_reportname_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       
    }
    protected void ddl_plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
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
    //                ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
    //                ddl_plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}

    //private void LoadPlantcode()
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;
    //        ddl_Plantcode.Items.Clear();
    //        ddl_Plantname.Items.Clear();
    //       // txt_PlantPhoneNo.Items.Clear();
    //        dr = Bllusers.LoadPlantcode(ccode);
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
    //                ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
    //             //   txt_PlantPhoneNo.Items.Add(dr["Mana_PhoneNo"].ToString());
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}
    //private void loadsingleplant()
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;
    //        ddl_Plantcode.Items.Clear();
    //        ddl_Plantname.Items.Clear();
    //      //  txt_PlantPhoneNo.Items.Clear();
    //        dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
    //                ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
    //               // txt_PlantPhoneNo.Items.Add(dr["Mana_PhoneNo"].ToString());
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}



    private void LoadPlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }



    protected void btn_Report_Click(object sender, EventArgs e)
    {

      //  pcode = ddl_Plantname.SelectedItem.Value;
        ConfigureColors();
        LoadGraphData();
    }
}