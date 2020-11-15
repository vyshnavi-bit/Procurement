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


public partial class branchsummarychart : System.Web.UI.Page
{


    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DataTable DTT = new DataTable();
    DataTable FT = new DataTable();
    SqlConnection con = new SqlConnection();
    DbHelper db = new DbHelper();
    DataTable AGENTLOANCOUNT = new DataTable();
    string[] SINGLEAGENT;
    string PARTICUAGENT;
    string GETLOANID;
    int ii = 0;
    int jj = 6;
    int i = 0;
    int j = 0;
    string[] GETAGENTIDWITHNAME;
    StringBuilder str = new StringBuilder();
    StringBuilder str1 = new StringBuilder();
    DataSet DTG = new DataSet();
    DbHelper DB = new DbHelper();
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

                    roleid = Convert.ToInt32(Session["Role"].ToString());
                  

                    dtm = System.DateTime.Now;
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                        
                    ddl_Plantname.Visible = false;

                    if (RadioButtonList1.Checked == true)
                    {
                        ddl_Plantname.Visible = true;
                        Label7.Visible = true;
                    }
                    else
                    {

                        ddl_Plantname.Visible = false;
                        Label7.Visible = false;

                    }
                   

                }
                else
                {
                    


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;
                if (RadioButtonList1.Checked == false)
                {
                    ddl_Plantname.Visible = false;

                    Label7.Visible = false;

                }

                else
                {
                    ddl_Plantname.Visible = true;
                    Label7.Visible = true;


                }

            }
        }

        catch
        {



        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.Checked == false)
        {
           

            getchartgrid();

        }

        else
        {
          

            getchartgrid1();


        }

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
    public void getchartgrid()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        con = db.GetConnection();
        string get = "";
        get = "SELECT YEAR = YEAR(PRDATE),        MONTH = MONTH(PRDATE),        MMM = UPPER(left(DATENAME(MONTH,PRDATE),3)),        MILK = (sum(mILK_KG)),        OrderCount = count(* ) FROM    Procurement  WHERE  PRDATE  BETWEEN '" + d1 + "' AND '" + d2 + "' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE)   order by YEAR,MONTH ";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  datatable dt = new datatable();
        da.Fill(DTT);
        FT.Columns.Clear();
        FT.Columns.Add("MONTH");
        FT.Columns.Add("mILKKG");
        //FT.Columns.Add("sesscount");
        foreach (DataRow dr in DTT.Rows)
        {

            string getmonth = dr[2].ToString();
            string milkkg = dr[3].ToString();
            //string totsess = dr[4].ToString();
            //   FT.Rows.Add(getmonth, milkkg, totsess);
            FT.Rows.Add(getmonth, milkkg);
        }

        if (DTT.Rows.Count > 0)
        {

            Label6.Visible = true;
            ddl_charttype.Visible = true;

        }
        str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
                google.setOnLoadCallback(drawChart);
                function drawChart() {
               
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Milk Kg');
                    data.addColumn('number', 'MonthWise');
//                    data.addColumn('number', 'Expenses');
                    data.addRows(" + FT.Rows.Count + ");");

        Int32 i;
        for (i = 0; i <= FT.Rows.Count - 1; i++)
        {
            str.Append("data.setValue( " + i + "," + 0 + "," + "'" + FT.Rows[i]["MONTH"].ToString() + "');");
            str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["mILKKG"].ToString() + ") ;");
            //str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["sesscount"].ToString() + ") ;");
            //str.Append(" data.setValue(" + i + "," + 2 + "," + FT.Rows[i]["expences"].ToString() + ");");
        }
        //  str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});

        // str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
        // 
        // draw(data, { title: "Google Chart demo" });
        if (ddl_charttype.Text == "BarChart")
        {
            str.Append("var chart =   new google.visualization.BarChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.Text == "PieChart")
        {
            str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.Text == "LineChart")
        {

            str.Append("var chart =   new google.visualization.LineChart(document.getElementById('visualization'));");
        }
        if (ddl_charttype.Text == "ColumnChart")
        {
            str.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "AreaChart")
        {
            str.Append("var chart =   new google.visualization.AreaChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "SteppedAreaChart")
        {
            str.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "0")
        {
            str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

        }

            str.Append(" chart.draw(data, {width: 1000, lineWidth:5, height: 400, legend: 'bottom',is3D: true,pointSize:5 ,title: '',");
        
            str.Append("hAxis: {title:   'Month', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");

        //str.Append("</script>");

        lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');

        if (RadioButtonList1.Checked == true)
        {

           
            lt2.Visible = true;
        }
        else
        {
            lt2.Visible = false;

        }


    }

    public void getchartgrid1()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        con = db.GetConnection();
        string get = "";
        get = "SELECT YEAR = YEAR(PRDATE),        MONTH = MONTH(PRDATE),        MMM = UPPER(left(DATENAME(MONTH,PRDATE),3)),        MILK = (sum(mILK_KG)),        OrderCount = count(* ) FROM    Procurement  WHERE  PRDATE  BETWEEN '"+d1+"' AND '"+d2+"'   and plant_code='"+pcode+"' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE)   order by YEAR,MONTH ";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  datatable dt = new datatable();

        da.Fill(DTT);

        FT.Columns.Clear();

        FT.Columns.Add("MONTH");
        FT.Columns.Add("mILKKG");
        //FT.Columns.Add("sesscount");

        foreach (DataRow dr in DTT.Rows)
        {

            string getmonth = dr[2].ToString();
            string milkkg = dr[3].ToString();
            //string totsess = dr[4].ToString();
            //   FT.Rows.Add(getmonth, milkkg, totsess);
            FT.Rows.Add(getmonth, milkkg);
        }

        if (DTT.Rows.Count > 0)
        {

            Label6.Visible = true;
            ddl_charttype.Visible = true;

        }

        str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
                google.setOnLoadCallback(drawChart);
                function drawChart() {
               
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Milk Kg');
                    data.addColumn('number', 'MonthWise');
//                    data.addColumn('number', 'Expenses');
                    data.addRows(" + FT.Rows.Count + ");");

        Int32 i;


        for (i = 0; i <= FT.Rows.Count - 1; i++)
        {
            str.Append("data.setValue( " + i + "," + 0 + "," + "'" + FT.Rows[i]["MONTH"].ToString() + "');");
            str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["mILKKG"].ToString() + ") ;");
            //str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["sesscount"].ToString() + ") ;");
            //str.Append(" data.setValue(" + i + "," + 2 + "," + FT.Rows[i]["expences"].ToString() + ");");
        }
        //  str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});

        // str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
        // 
        // draw(data, { title: "Google Chart demo" });

        if (ddl_charttype.Text == "BarChart")
        {
            str.Append("var chart =   new google.visualization.BarChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.Text == "PieChart")
        {
            str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.Text == "LineChart")
        {

            str.Append("var chart =   new google.visualization.LineChart(document.getElementById('visualization'));");
        }
        if (ddl_charttype.Text == "ColumnChart")
        {
            str.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "AreaChart")
        {
            str.Append("var chart =   new google.visualization.AreaChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "SteppedAreaChart")
        {
            str.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "0")
        {
            str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

        }

        string GETP = ddl_Plantname.SelectedItem.Text  + ":Plant Performence";

        str.Append(" chart.draw(data, {width: 1000, lineWidth:5, height: 500, legend: 'bottom',is3D: true,pointSize:5 ,title: '',");
        str.Append("hAxis: {title:   'Month', titleTextStyle: {color: 'green'}}");
        str.Append("}); }");
        str.Append("</script>");


        lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');
        if (RadioButtonList1.Checked == true)
        {

            lt2.Text = GETP;
            lt2.Visible = true;
        }
        else
        {
            lt2.Visible = false;

        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_charttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.Checked == false)
        {
            ddl_Plantname.Visible = false;

            getchartgrid();

        }

        else
        {
            ddl_Plantname.Visible = false;
            getchartgrid1();
         


        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void RadioButtonList1_CheckedChanged(object sender, EventArgs e)
    {

        if (RadioButtonList1.Checked == true)
        {
            ddl_Plantname.Visible = true;
            Label7.Visible = true;
            LoadPlantcode();
            getchartgrid1();
        }
        else
        {

            ddl_Plantname.Visible = false;
            Label7.Visible = false;
            getchartgrid();

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
  
}