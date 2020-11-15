using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;


public partial class PlantChart : System.Web.UI.Page
{
    DbHelper db = new DbHelper();
    SqlConnection con = new SqlConnection();
   DataTable dt = new DataTable();
    StringBuilder str = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getgrid();



        } 
    }


        private DataTable GetData()
    {
        DataTable dt = new DataTable();
        con = db.GetConnection();
        string cmd = "select Milkkg,Plant_Name from(  SELECT  PlantCode,Sum(MILKKG) as Milkkg   from lp where     prdate='7-1-2016'  group by PlantCode) as lp left join(select Plant_Code,Plant_Name  from Plant_Master group by Plant_Code,Plant_Name ) as pm on lp.PlantCode=pm.Plant_Code";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
        adp.Fill(dt);
        con.Close();
        return dt;       
    }
 



    public void getgrid()
    {
        
          


                
        try
        {
            dt = GetData();                     
          
            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Plant_Name');
            data.addColumn('number', 'Milkkg');
            data.addColumn('number', 'Milkkg');
            data.addColumn('number', 'Milkkg');
 
            data.addRows(" + dt.Rows.Count + ");");
 
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Plant_Name"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Milkkg"].ToString() + ") ;");
                //str.Append("data.setValue(" + i + "," + 2 + "," + dt.Rows[i]["Milkkg"].ToString() + ") ;");
                //str.Append("data.setValue(" + i + "," + 3 + "," + dt.Rows[i]["Milkkg"].ToString() + ");");
 
            }

            //str.Append ("<graph caption='""'subcaption='""'
            //hovercapbg='FFECAA' hovercapborder='F47E00' formatNumberScale='100' decimalPrecision='2'
            //showvalues='3' numdivlines='5' numVdivlines='10'  yaxisminvalue='1000'  font='Andalus'  fontSize='100'   yaxismaxvalue='80.00'  rotateNames='1'
            //showAlternateHGridColor='15' AlternateHGridColor='FFA500' divLineColor='FFA500' 
            //divLineAlpha='75' alternateHGridAlpha='5'
            //xAxisName='""' yAxisName='""'  ) ";

            //str.Append("   var chart = new google.visualization.LineChart(document.getElementById('chart_div'));");
            //str.Append(" chart.draw(data, {width: 1000, height: 300, title: 'Company Performance',");
            //str.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}}");
            //str.Append("}); }");
            //str.Append("</script>");
            ////str.Append("chart.draw(Data,{numdivlines='5',hovercapborder='F47E00',, formatNumberScale='100', numVdivlines='10',  yaxisminvalue='1000'  font='Andalus')}"); //

            str.Append("var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));");

            str.Append("chart.draw(data, {width: 750, height: 300,  legend: 'bottom',is3D: true,title: 'Performance',"); str.Append("vAxis: {title: 'Year', titleTextStyle: {color: 'green'}}"); str.Append("}); }"); str.Append("</script>");



            lt.Text = str.ToString().Replace('*', '"');        
        }
        catch
        { }   
    


    }

}