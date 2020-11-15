using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System;

public partial class LiveMilkAnalyzes : System.Web.UI.Page
{
   
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    string d1;
    DateTime dtm = new DateTime();
    DataSet OVERALL = new DataSet();
    public string frmdate;
    StringBuilder str = new StringBuilder();
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
                   txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    BindChart();
                   
                }
                else
                {


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
            }
              
        }
        catch
        {


        }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }

    private DataTable GetData()
    {
        DateTime dt1 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        con = DB.GetConnection();
        DataTable dte = new DataTable();
        dte.Rows.Clear();
        string STR = "select PlantCode,Plant_Name,milkkg  from (select lrr.PlantCode,milkkg  from (Select PlantCode   from Lp where Prdate ='" + d1 + "' and Sessions='" + RadioButtonList1.SelectedItem.Text + "'  group by PlantCode) as lrr left join (Select PlantCode,convert(decimal(18,2),ISNULL(sum(Milkkg),0)) as milkkg   from Lp where Prdate ='" + d1 + "' and Sessions='" + RadioButtonList1.SelectedItem.Text + "' group by PlantCode) as lpp on lrr.PlantCode=lpp.PlantCode) as news left join (select Plant_Code,Plant_Name  from Plant_Master group by Plant_Code,Plant_Name) as pms on news.PlantCode=pms.Plant_Code ORDER BY milkkg DESC";
        SqlCommand CMD = new SqlCommand(STR, con);
        SqlDataAdapter adp = new SqlDataAdapter(CMD);
        adp.Fill(dte);
        return dte;
    }
     private void BindChart()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = GetData();
            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*gauge*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
       data.addColumn('string', 'Plant_Name');
      data.addColumn('number', 'milkkg');    
     
      data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Plant_Name"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["milkkg"].ToString() + ") ;");
            
            }
            str.Append("var options = {width: 1000, height: 400,redFrom: 0, redTo: 50,yellowFrom:10000, yellowTo: 5000,minorTicks: 5};");
            str.Append(" var chart = new google.visualization.Gauge(document.getElementById('chart_div'));");
            str.Append(" chart.draw(data, options); }");
            str.Append("</script>");
            lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');
        }
        catch
        { }


//        DataTable dt = new DataTable();
//        try
//        {
//            dt = GetData();
//            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*gauge*]});
//        google.setOnLoadCallback(drawChart);
//        function drawChart() {
//           var data = new google.visualization.DataTable();
//           data.addColumn('string', 'item');
//           data.addColumn('number', 'value');      
//           data.addRows(" + dt.Rows.Count + ");");
//            for (int i = 0; i <= dt.Rows.Count - 1; i++)
//            {
//                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Plant_Name"].ToString() + "');");
//                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["milkkg"].ToString() + ") ;");
//            }
//            str.Append("var options = {width: 600, height: 300,redFrom: 90, redTo: 100,yellowFrom:75, yellowTo: 90,minorTicks: 5};");
//            str.Append(" var chart = new google.visualization.Gauge(document.getElementById('chart_div'));");
//            str.Append(" chart.draw(data, options); }");
//            str.Append("</script>");
//            lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');
//        }
//        catch
//        { }



    }
     protected void Button6_Click(object sender, EventArgs e)
     {
         BindChart();
     }
     protected void Button9_Click(object sender, EventArgs e)
     {

     }
}
