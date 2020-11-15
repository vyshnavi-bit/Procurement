using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Text;


public partial class PlantDumpingTime1 : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public static string routeid;
    public string routemilksum;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTGGG = new DataSet();
    StringBuilder strP = new StringBuilder();
    StringBuilder strPP = new StringBuilder();
    DataTable DTGchart = new DataTable();
    int h = 0;
    int j = 1;
    int jG = 1;
    int jhp = 0;
    double GETSUM = 0;
    DataSet DTG = new DataSet();
    string d1;
    string d2;
    DataTable gettime = new DataTable();
    DataSet DTGGG12 = new DataSet();
    DataSet news123 = new DataSet();
    DataSet news1234 = new DataSet();
    string getnews11;
    string getnews1;
    int ab = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    dtm = System.DateTime.Now;
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;

                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    if ((roleid >= 3) && (roleid != 9))
                    {
                        LoadPlantcode();
                    }
                    if (roleid == 9)
                    {

                        LoadPlantcode();

                    }
                    ddl_route.Visible = false;
                    Label1.Visible = false;


                }
                else
                {


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();
                pcode = ddl_Plantname.SelectedItem.Value;


                if (rtoroute.Checked == true)
                {
                    routeid = ddl_route.SelectedItem.Value;
                    Label1.Visible = true;
                }


            }
        }
        catch
        {


        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            if (rtoroute.Checked == false)
            {
                weigherStartingtime();
                foreach (DataRow dff in DTGGG.Tables[0].Rows)
                {

                    DateTime StartingTime = Convert.ToDateTime(dff[0]);

                    string GET = StartingTime.ToString("hh:mm");





                    string Plant_Code = dff[1].ToString();
                    string prdate = dff[2].ToString();
                    string Sessions = dff[3].ToString();
                    string SAMPLE = dff[4].ToString();
                    con = DB.GetConnection();
                  //  string str = "insert into DumpTime(WeigherAnalyzerTime,Date,plant_code,sess,status,weighersample,analyzersample) values('" + StartingTime + "','" + prdate + "','" + Plant_Code + "','" + Sessions.TrimEnd() + "','1','" + SAMPLE + "','0')";
                    string str = "insert into DumpTime(WeigherAnalyzerTime,Date,plant_code,sess,status,weighersample) values('" + GET + "','" + prdate + "','" + Plant_Code + "','" + Sessions.TrimEnd() + "','1','" + SAMPLE + "')";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();


                }
                labStartingtime();
                foreach (DataRow dff in DTGGG.Tables[1].Rows)
                {

                     

                    DateTime StartingTime = Convert.ToDateTime(dff[0]);

                    string GET = StartingTime.ToString("hh:mm");



                    //double getnews = Convert.ToDouble("StartingTime");
                  
                    //if ((getnews >= 13.00) && (getnews <= 24))
                    //{
                    //    getnews11 = (getnews - 12).ToString();
                    //}
                    //if (getnews < 1)
                    //{
                    //    getnews11 = (getnews + 12).ToString();


                    
                    string Plant_Code = dff[1].ToString();
                    string prdate = dff[2].ToString();
                    string Sessions = dff[3].ToString();
                    string SAMPLE = dff[4].ToString();

                    con = DB.GetConnection();
                   // string str = "insert into DumpTime(WeigherAnalyzerTime,Date,plant_code,sess,status,analyzersample,weighersample) values('" + StartingTime.TrimEnd() + "','" + prdate + "','" + Plant_Code + "','" + Sessions + "','2','" + SAMPLE + "','0')";
                    string str = "insert into DumpTime(WeigherAnalyzerTime,Date,plant_code,sess,status,analyzersample) values('" + GET + "','" + prdate + "','" + Plant_Code + "','" + Sessions + "','2','" + SAMPLE + "')";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();



                    //   gettime.Rows.Add(StartingTime, Plant_Code, prdate, Sessions);


                }
                GetTIME();

                chart_bind();
            }

            else
            {


                weigherrouteStartingtime();
                foreach (DataRow dff in news1234.Tables[0].Rows)
                {

                    string StartingTime = dff[0].ToString();

                    DateTime StartingTime1 = Convert.ToDateTime(StartingTime);

                    StartingTime = StartingTime1.ToString("hh:mm");



                    string Plant_Code = dff[1].ToString();
                    string prdate = dff[2].ToString();
                    string Sessions = dff[3].ToString();
                    string SAMPLE = dff[4].ToString();
                    string route = dff[5].ToString();
                    con = DB.GetConnection();
                    string str = "insert into DumpTime(WeigherAnalyzerTime,Date,plant_code,sess,status,weighersample,routeid) values('" + StartingTime + "','" + prdate + "','" + Plant_Code + "','" + Sessions.TrimEnd() + "','1','" + SAMPLE + "','" + route + "')";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();



                    //   gettime.Rows.Add(StartingTime, Plant_Code, prdate, Sessions);


                }
                labrouteStartingtime();
                foreach (DataRow dff in news1234.Tables[1].Rows)
                {

                    string StartingTime = dff[0].ToString();





                    DateTime StartingTime1 = Convert.ToDateTime(StartingTime);

                    StartingTime = StartingTime1.ToString("hh:mm");






                    string Plant_Code = dff[1].ToString();
                    string prdate = dff[2].ToString();
                    string Sessions = dff[3].ToString();
                    string SAMPLE = dff[4].ToString();
                    string rout = dff[5].ToString();
                    con = DB.GetConnection();
                    string str = "insert into DumpTime(WeigherAnalyzerTime,Date,plant_code,sess,status,analyzersample,routeid) values('" + StartingTime.TrimEnd() + "','" + prdate + "','" + Plant_Code + "','" + Sessions + "','2','" + SAMPLE + "','" + rout + "')";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();

                }
                GetTIMEroute();
                chart_bind1();
                lt1.Visible = true;
                lt.Visible = true;
                

            }

        }
        catch
        {


        }

    }

    public void   weigherStartingtime()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        con = DB.GetConnection();
        string cmdD = "DELETE    FROM DumpTime";
        SqlCommand CMDD = new SqlCommand(cmdD, con);
        CMDD.ExecuteNonQuery();



       
        con = DB.GetConnection();
        string cmd = " select  StartingTime,PlantCode,convert(varchar,prdate,101) as prdate,Sessions,sampleno from lp where   plantcode='" + pcode + "' and   Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "'    order by  sampleno ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
        adp.Fill(DTGGG, ("weigher"));
        
    }

    public void  labStartingtime()
    {
        DataTable dt1 = new DataTable();
        con = DB.GetConnection();
        string cmd = " select  EndingTime,PlantCode,convert(varchar,prdate,101) as prdate,Sessions,sampleno from lp where   plantcode='" + pcode + "' and   Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "'  order by  sampleno ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
        adp.Fill(DTGGG,("lab"));

    }

    public void weigherrouteStartingtime()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        con = DB.GetConnection();
        string cmdD = "DELETE    FROM DumpTime";
        SqlCommand CMDD = new SqlCommand(cmdD, con);
        CMDD.ExecuteNonQuery();




        con = DB.GetConnection();
        string cmd = " select   StartingTime,PlantCode,convert(varchar,prdate,101) as prdate,Sessions,sampleno,routeid from lp where   plantcode='" + pcode + "' and routeid='"+routeid+"' and  Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "'    order by  sampleno ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
        adp.Fill(news1234, ("weigher"));

    }

    public void labrouteStartingtime()
    {
        DataTable dt1 = new DataTable();
        con = DB.GetConnection();
        string cmd = " select  EndingTime,PlantCode,convert(varchar,prdate,101) as prdate,Sessions,sampleno,routeid from lp where   plantcode='" + pcode + "' and routeid='"+routeid+"' and  Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "'  order by  sampleno ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
        adp.Fill(news1234, ("lab"));

    }

       

    private void  GetTIME()
    {
        DataTable dt1 = new DataTable();
        con = DB.GetConnection();
        string cmd = "select  WeigherAnalyzerTime AS Time,weighersample  as WeiSample,analyzersample as Labsample    from  DumpTime   where plant_code='" + pcode + "' and   date='" + d1 + "' AND sess='" + rdolist.SelectedItem.Text + "'    order by  cast(WeigherAnalyzerTime as time) ASC    ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);

        adp.Fill(news123);
       
    }

    private void GetTIMEroute()
    {

        news1234.Tables[0].Clear();
        news1234.Tables[1].Clear();
       // news1234.Tables[2].Clear();
        DataTable dt1 = new DataTable();
        con = DB.GetConnection();
        string cmd = "select  WeigherAnalyzerTime AS Time,weighersample  as WeiSample,analyzersample as Labsample    from  DumpTime   where plant_code='" + pcode + "' and  routeid='" + routeid + "' and date='" + d1 + "' AND sess='" + rdolist.SelectedItem.Text + "'   order by  cast(WeigherAnalyzerTime as time) ASC  ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);

        adp.Fill(news1234,("TIMEROUTE"));

    }
    private void chart_bind()
    {
        DataTable dt = new DataTable();

        DataTable dt1 = new DataTable();
        try
        {


            strP.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();

            data.addColumn('string', 'Time');
            data.addColumn('number', 'WeiSample');
            data.addColumn('number', 'Labsample');
 
            data.addRows(" + news123.Tables[0].Rows.Count + ");");

            for (int i = 0; i <= news123.Tables[0].Rows.Count - 1; i++)
            {



                //string getweitime = DTGGG.Tables[0].Rows[ab][0].ToString();
                //string getlabtime = DTGGG.Tables[1].Rows[ab][0].ToString();

              //  strP.Append("data.setValue( " + i + "," + 0 + "," + "'" + news123.Tables[0].Rows[i]["Time"].ToString() + "');");

                strP.Append("data.setValue( " + i + "," + 0 + "," + "'" + news123.Tables[0].Rows[i]["Time"].ToString() + "');");
                strP.Append("data.setValue( " + i + "," + 1 + "," + "'" + news123.Tables[0].Rows[i]["WeiSample"].ToString() + "');");
                strP.Append("data.setValue( " + i + "," + 2 + "," + "'" + news123.Tables[0].Rows[i]["Labsample"].ToString() + "');");

                //strP.Append("data.setValue(" + i + "," + 1 + ",'" + DTGGG.Tables[0].Rows[ab][0].ToString() + "') ;");
                //strP.Append("data.setValue(" + i + "," + 2 + ",'" + DTGGG.Tables[1].Rows[ab][0].ToString() + "') ;");


                //strPP.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt1.Rows[i]["EndingTime"].ToString() + "');");
                //strPP.Append("data.setValue(" + i + "," + 1 + "," + dt1.Rows[i]["sampleno"].ToString() + ") ;");

              //  ab = ab + 1;
            }






            if (ddl_charttype.SelectedItem.Text == "BarChart")
            {
                strP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div'));");
              //  strPP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Text == "PieChart")
            {
                strP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div'));");
               // strPP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Text == "LineChart")
            {

                strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
              //  strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");
            }
            if (ddl_charttype.SelectedItem.Text == "ColumnChart")
            {
                strP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div'));");
              //  strPP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Text == "AreaChart")
            {
                strP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div'));");
               // strPP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Text == "SteppedAreaChart")
            {
                strP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div'));");
               // strPP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Text == "---SELECT----")
            {
                strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
              //  strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");

            }


            //string getmsg = "Weigher Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant" + "  Date:" + txt_FromDate.Text + "-Sessions:" + rdolist.SelectedItem.Text + "Total Milkkg:" + GETSUM + "-Total Samples:" + dt.Rows.Count + "";

            //string getmsg1 = "Lab Timing  Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant";
            string getmsg = "Weigher AND Lab Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant" + "  Date:" + txt_FromDate.Text + "-Sessions:" + rdolist.SelectedItem.Text ;
            strP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,title: '" + getmsg + "',");
            strP.Append("hAxis: {title: 'WEIGHING TIME', titleTextStyle: {color: 'green'}}");
            strP.Append("}); }");
            strP.Append("</script>");


            //strPP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,");
            //strPP.Append("hAxis: {title: 'lAB TIME', titleTextStyle: {color: 'green'}}");
            //strPP.Append("}); }");
            //strPP.Append("</script>");

            //strP.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            //strP.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            //strP.Append(" </script>");

            lt.Text = strP.ToString().Replace('*', '"');
            lt.Visible = true;           
            lt1.Visible = false;
           // lt1.Text = strPP.ToString().Replace('*', '"');
        }
        catch
        { }
    }


    private void chart_bind1()
    {
        
        try
        {


            strPP.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();

            data.addColumn('string', 'Time');
            data.addColumn('number', 'WeiSample');
            data.addColumn('number', 'Labsample');
 
            data.addRows(" + news1234.Tables[2].Rows.Count + ");");

            for (int i = 0; i <= news1234.Tables[2].Rows.Count - 1; i++)
            {


                strPP.Append("data.setValue( " + i + "," + 0 + "," + "'" + news1234.Tables[2].Rows[i]["Time"].ToString() + "');");
                strPP.Append("data.setValue( " + i + "," + 1 + "," + "'" + news1234.Tables[2].Rows[i]["WeiSample"].ToString() + "');");
                strPP.Append("data.setValue( " + i + "," + 2 + "," + "'" + news1234.Tables[2].Rows[i]["Labsample"].ToString() + "');");


                //strPP.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt1.Rows[i]["EndingTime"].ToString() + "');");
                //strPP.Append("data.setValue(" + i + "," + 1 + "," + dt1.Rows[i]["sampleno"].ToString() + ") ;");


            }






            if (ddl_charttype.Text == "BarChart")
            {
             //   strP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.Text == "PieChart")
            {
                //strP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.Text == "LineChart")
            {

              //  strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");
            }
            if (ddl_charttype.Text == "ColumnChart")
            {
               // strP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.Text == "AreaChart")
            {
               // strP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div'));");
               strPP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Text == "SteppedAreaChart")
            {
               // strP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div'));");
               strPP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Text == "---SELECT----")
            {
               // strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");

            }


            //string getmsg = "Weigher Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant" + "  Date:" + txt_FromDate.Text + "-Sessions:" + rdolist.SelectedItem.Text + "Total Milkkg:" + GETSUM + "-Total Samples:" + dt.Rows.Count + "";

            //string getmsg1 = "Lab Timing  Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant";
            string getmsg = "Weigher AND Lab Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant" + "  Date:" + txt_FromDate.Text + "-Sessions:" + rdolist.SelectedItem.Text ;
            strPP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,title: '" + getmsg + "',");
           // strPP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,");
            strPP.Append("hAxis: {title: 'WEIGHING TIME', titleTextStyle: {color: 'green'}}");
            strPP.Append("}); }");
            strPP.Append("</script>");


            //strPP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,");
            //strPP.Append("hAxis: {title: 'lAB TIME', titleTextStyle: {color: 'green'}}");
            //strPP.Append("}); }");
            //strPP.Append("</script>");

            //strP.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            //strP.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            //strP.Append(" </script>");
            lt.Visible = false;
            lt1.Visible = true;
            lt1.Text = strPP.ToString().Replace('*', '"');
            
            // lt1.Text = strPP.ToString().Replace('*', '"');
        }
        catch
        { }
    }


    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }


    public void LoadRouteName()
    {

        con = DB.GetConnection();
        string stt = "select RouteId,Route_Name   from (SELECT RouteId FROM Lp where PlantCode  in(" + pcode + ") and RouteId is not null  group by RouteId) as lp left join(SELECT Route_ID,Route_Name FROM Route_Master where plant_code  in(" + pcode + ") group by Route_ID,Route_Name) as rm on lp.RouteId=rm.Route_ID   order by RouteId   asc";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_route.DataSource = DTG.Tables[0];
        ddl_route.DataTextField = "Route_Name";
        ddl_route.DataValueField = "RouteId";
        ddl_route.DataBind();

    }




    protected void Button7_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_charttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        chart_bind();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlroute_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void rtoroute_CheckedChanged(object sender, EventArgs e)
    {
        if (rtoroute.Checked == true)
        {

            ddl_route.Visible = true;
            LoadRouteName();
            routeid = ddl_route.SelectedItem.Value;
            Label1.Visible = true;






        }
        else
        {
            ddl_route.Visible = false;

            routeid = ddl_route.SelectedItem.Value;
            Label1.Visible = false;




        }
        lt.Visible = false;
        lt1.Visible = false;
    }
}