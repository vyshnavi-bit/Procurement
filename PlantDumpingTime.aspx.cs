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

public partial class PlantDumpingTime : System.Web.UI.Page
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

                    LoadPlantcode();

                    ddl_route.Visible = false;
                    Label1.Visible = false;
                    GridView2.Visible = false;

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
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            d1 = dt1.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");
            if (rtoroute.Checked == true)
            {
                chart_bind1();
                GridView2.Visible = true;
                string stt;
                stt = "select  SampleNo,AgentId,Sum(Milkkg) as Kg,StartingTime as WeighingTime,EndingTime as Labtime FROM  lp where    Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'  and Routeid='" + routeid + "'  GROUP BY   PlantCode,Sessions,Prdate,agentid,StartingTime,EndingTime,SampleNo   ORDER BY SampleNo ASC";
                con = DB.GetConnection();
                
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
               
                DataSet DGF = new DataSet();
                DGF.Tables.Clear();
                DA.Fill(DGF, ("sample"));

                if (DGF.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = DGF.Tables[0];
                    GridView2.DataBind();

                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();


                }


            }

            else
            {

                chart_bind();
            }


            lt.Visible = true;
            lt1.Visible = true;

        }
        catch
        {


        }

         //   chart_bind1();
         

    }



    


      private DataTable GetData()
    {
        DataTable dt = new DataTable();
        con = DB.GetConnection();
       // string cmd = " select startim AS STARINGTIME, milkkg   from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,PlantCode,PRDATE, Sessions from lp where  Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text+ "' AND PlantCode='" + pcode + "'   GROUP BY   PlantCode,Sessions,Prdate, StartingTime) as ff   ORDER BY    CAST(startim as time) asc   ";

      //  string cmd = "select startim AS STARINGTIME, milkkg  from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,PlantCode,PRDATE, Sessions from lp where  Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'    GROUP BY   PlantCode,Sessions,Prdate, StartingTime) as ff   ORDER BY    CAST(startim as time) asc  ";

      //  string cmd = "select  ( STARINGTIME +'canNo:'+ agentid) as STARINGTIME, milkkg   from (select   startim AS STARINGTIME ,agentid, milkkg  from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,convert(varchar,agentid) as agentid,PlantCode,PRDATE, Sessions from lp where    Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'  GROUP BY   PlantCode,Sessions,Prdate,agentid,StartingTime) as ff     )   asfgg  ORDER BY    CAST(STARINGTIME as time) asc";

        string cmd = "select  ( STARINGTIME +'-canNo:'+ agentid + '-MKg:'+ milkkg) as STARINGTIME, milkkg   from (select   startim AS STARINGTIME ,agentid,convert(varchar, milkkg) as  milkkg  from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,convert(varchar,agentid) as agentid,PlantCode,PRDATE, Sessions from lp where    Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'  GROUP BY   PlantCode,Sessions,Prdate,agentid,StartingTime) as ff     )   asfgg  ORDER BY    CAST(STARINGTIME as time) asc";



        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
        adp.Fill(dt);
       return dt;       
    }

      private DataTable GetDataroute()
      {
          DataTable dt = new DataTable();
          con = DB.GetConnection();
          // string cmd = " select startim AS STARINGTIME, milkkg   from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,PlantCode,PRDATE, Sessions from lp where  Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text+ "' AND PlantCode='" + pcode + "'   GROUP BY   PlantCode,Sessions,Prdate, StartingTime) as ff   ORDER BY    CAST(startim as time) asc   ";

          //  string cmd = "select startim AS STARINGTIME, milkkg  from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,PlantCode,PRDATE, Sessions from lp where  Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'    GROUP BY   PlantCode,Sessions,Prdate, StartingTime) as ff   ORDER BY    CAST(startim as time) asc  ";

          //  string cmd = "select  ( STARINGTIME +'canNo:'+ agentid) as STARINGTIME, milkkg   from (select   startim AS STARINGTIME ,agentid, milkkg  from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,convert(varchar,agentid) as agentid,PlantCode,PRDATE, Sessions from lp where    Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'  GROUP BY   PlantCode,Sessions,Prdate,agentid,StartingTime) as ff     )   asfgg  ORDER BY    CAST(STARINGTIME as time) asc";

          string cmd = "select  ( STARINGTIME +'-canNo:'+ agentid ) as STARINGTIME, milkkg   from (select   startim AS STARINGTIME ,agentid,convert(varchar, milkkg) as  milkkg  from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,convert(varchar,agentid) as agentid,PlantCode,PRDATE, Sessions from lp where    Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'  and Routeid='"+routeid+"'  GROUP BY   PlantCode,Sessions,Prdate,agentid,StartingTime) as ff     )   asfgg  ORDER BY    CAST(STARINGTIME as time) asc";



          SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
          adp.Fill(dt);
          return dt;
      } 
       private DataTable GetData1()
    {
        DataTable dt1 = new DataTable();
        con = DB.GetConnection();
        string cmd = " select EndingTime as  EndingTime,sampleno from lp where   Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'   order by  sampleno ";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
        adp.Fill(dt1);
       return dt1;       
    }

       private DataTable GetDataroute1()
       {
           DataTable dt1 = new DataTable();
           con = DB.GetConnection();
           string cmd = " select EndingTime as EndingTime,sampleno from lp where   Prdate='" + d1 + "' AND Sessions='" + rdolist.SelectedItem.Text + "' AND PlantCode='" + pcode + "'  and Routeid='" + routeid + "'  order by  sampleno ";
           SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
           adp.Fill(dt1);
           return dt1;
       }
      private void chart_bind()
    {      
        DataTable dt = new DataTable();

        DataTable dt1 = new DataTable();
        try
        {
            dt = GetData();                     
          
            strP.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'STARINGTIME');
            data.addColumn('number', 'milkkg');
        
 
            data.addRows(" + dt.Rows.Count + ");");
 
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                double GR = Convert.ToDouble(dt.Rows[i]["milkkg"]);
                GETSUM = GETSUM + GR;

              //  string STTIME = dt.Rows[i]["STARINGTIME"].ToString();

                //string getwei = dt.Rows[i]["STARINGTIME"].ToString();

                //DateTime StartingTime = Convert.ToDateTime(getwei);
                //string GET = StartingTime.ToString("hh:mm");


               // string CAN = dt.Rows[i]["AgentId"].ToString(); 

             //   string STTCAN = STTIME +"/" + CAN ;


                string HETT = GETSUM.ToString("f2");

                //strP.Append("data.setValue(" + i + "," + 0 + "," + STTCAN + ") ;");
                //strP.Append("data.setValue(" + i + "," + 1 + "," + HETT + ") ;");

                strP.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["STARINGTIME"].ToString() + "');");
                strP.Append("data.setValue(" + i + "," + 1 + ","+ HETT +") ;");
               // strP.Append("data.setValue( " + i + "," + 2 + "," + "'" + dt.Rows[i]["AgentId"].ToString() + "');");
                 
 
            }


            dt1 = GetData1();

            strPP.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();





            data.addColumn('string', 'EndingTime');
            data.addColumn('number', 'sampleno');
 
            data.addRows(" + dt1.Rows.Count + ");");

            for (int i = 0; i <= dt1.Rows.Count - 1; i++)
            {


                string getwei = dt1.Rows[i]["EndingTime"].ToString();

                DateTime StartingTime = Convert.ToDateTime(getwei);
                string GET = StartingTime.ToString("hh:mm");


                strPP.Append("data.setValue( " + i + "," + 0 + "," + "'" + GET + "');");
                strPP.Append("data.setValue(" + i + "," + 1 + "," + dt1.Rows[i]["sampleno"].ToString() + ") ;");

                
            }






            if (ddl_charttype.Text == "BarChart")
            {
                strP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.Text == "PieChart")
            {
                strP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.Text == "LineChart")
            {

                strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");
            }
            if (ddl_charttype.Text == "ColumnChart")
            {
                strP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Value == "AreaChart")
            {
                strP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Value == "SteppedAreaChart")
            {
                strP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div1'));");

            }
            if (ddl_charttype.SelectedItem.Value == "0")
            {
                strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
                strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");

            }


            string getmsg = "Weigher Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant" + "  Date:" + txt_FromDate.Text + "-Sessions:" + rdolist.SelectedItem.Text + "Total Milkkg:" + GETSUM + "-Total Samples:" + dt.Rows.Count + "";

            string getmsg1 = "Lab Timing  Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant";

            strP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,title: '" + getmsg + "',");
            strP.Append("hAxis: {title: 'WEIGHING TIME', titleTextStyle: {color: 'green'}}");
            strP.Append("}); }");
            strP.Append("</script>");


            strPP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,title: '" + getmsg1 + "',");
            strPP.Append("hAxis: {title: 'lAB TIME', titleTextStyle: {color: 'green'}}");
            strPP.Append("}); }");
            strPP.Append("</script>");

            //strP.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            //strP.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            //strP.Append(" </script>");

            lt.Text = strP.ToString().Replace('*', '"');
            lt1.Text = strPP.ToString().Replace('*', '"');   
        }
        catch
        { }   
    }


      private void chart_bind1()
      {
          DataTable dt = new DataTable();

          DataTable dt1 = new DataTable();
          try
          {
              dt = GetDataroute();

              strP.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'STARINGTIME');
            data.addColumn('number', 'milkkg');
        
 
            data.addRows(" + dt.Rows.Count + ");");

              for (int i = 0; i <= dt.Rows.Count - 1; i++)
              {

                  double GR = Convert.ToDouble(dt.Rows[i]["milkkg"]);
                  GETSUM = GETSUM + GR;

                  //  string STTIME = dt.Rows[i]["STARINGTIME"].ToString();


                  // string CAN = dt.Rows[i]["AgentId"].ToString(); 

                  //   string STTCAN = STTIME +"/" + CAN ;


                  routemilksum = GETSUM.ToString("f2");

                  //strP.Append("data.setValue(" + i + "," + 0 + "," + STTCAN + ") ;");
                  //strP.Append("data.setValue(" + i + "," + 1 + "," + HETT + ") ;");

                  strP.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["STARINGTIME"].ToString() + "');");
                  strP.Append("data.setValue( " + i + "," + 1 + "," + "'" + dt.Rows[i]["milkkg"].ToString() + "');");
                  // strP.Append("data.setValue( " + i + "," + 2 + "," + "'" + dt.Rows[i]["AgentId"].ToString() + "');");


              }


              dt1 = GetDataroute1();

              strPP.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();





            data.addColumn('string', 'EndingTime');
            data.addColumn('number', 'sampleno');
 
            data.addRows(" + dt1.Rows.Count + ");");

              for (int i = 0; i <= dt1.Rows.Count - 1; i++)
              {




                  strPP.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt1.Rows[i]["EndingTime"].ToString() + "');");
                  strPP.Append("data.setValue(" + i + "," + 1 + "," + dt1.Rows[i]["sampleno"].ToString() + ") ;");


              }






              if (ddl_charttype.Text == "BarChart")
              {
                  strP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.BarChart(document.getElementById('chart_div1'));");

              }
              if (ddl_charttype.Text == "PieChart")
              {
                  strP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.PieChart(document.getElementById('chart_div1'));");

              }
              if (ddl_charttype.Text == "LineChart")
              {

                  strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");
              }
              if (ddl_charttype.Text == "ColumnChart")
              {
                  strP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div1'));");

              }
              if (ddl_charttype.SelectedItem.Value == "AreaChart")
              {
                  strP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.AreaChart(document.getElementById('chart_div1'));");

              }
              if (ddl_charttype.SelectedItem.Value == "SteppedAreaChart")
              {
                  strP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('chart_div1'));");

              }
              if (ddl_charttype.SelectedItem.Value == "0")
              {
                  strP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div'));");
                  strPP.Append("var chart =   new google.visualization.LineChart(document.getElementById('chart_div1'));");

              }


              string getmsg = "Weigher Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant" + "RouteName:" + ddl_route.SelectedItem.Text + " -Date:" + txt_FromDate.Text + "-Sessions:" + rdolist.SelectedItem.Text + "Total Route Milkkg:" + routemilksum + "-Total Samples:" + dt.Rows.Count + "";

              string getmsg1 = "Lab Timing  Performance For:" + ddl_Plantname.SelectedItem.Text + "---Plant";

              strP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,title: '" + getmsg + "',");
              strP.Append("hAxis: {title: 'WEIGHING TIME', titleTextStyle: {color: 'green'}}");
              strP.Append("}); }");
              strP.Append("</script>");


              strPP.Append("chart.draw(data, {width: 1000, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:3, height: 400, legend: 'bottom',is3D: true,pointSize:5,title: '" + getmsg1 + "',");
              strPP.Append("hAxis: {title: 'lAB TIME', titleTextStyle: {color: 'green'}}");
              strPP.Append("}); }");
              strPP.Append("</script>");

              //strP.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
              //strP.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
              //strP.Append(" </script>");

              lt.Text = strP.ToString().Replace('*', '"');
              lt1.Text = strPP.ToString().Replace('*', '"');
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
      public override void VerifyRenderingInServerForm(Control control)
      {
          /*Tell the compiler that the control is rendered
           * explicitly by overriding the VerifyRenderingInServerForm event.*/
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text  + "'.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView2.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch
        {


        }
    }
}