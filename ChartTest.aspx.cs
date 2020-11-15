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
using InfoSoftGlobal;

/// <summary>
/// //Multi Column BarChart
/// </summary>

public partial class ChartTest : System.Web.UI.Page
{
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string query = "select distinct shipcountry from orders";
            string query = "SELECT DISTINCT(Plant_code) AS shipcountry FROM Agent_Master WHERE  Plant_code IN ('155','156','157','158',164)";

            DataTable dt = GetData(query);
            ddlCountry1.DataSource = dt;
            ddlCountry1.DataTextField = "shipcountry";
            ddlCountry1.DataValueField = "shipcountry";
            ddlCountry1.DataBind();

            ddlCountry2.DataSource = dt;
            ddlCountry2.DataTextField = "shipcountry";
            ddlCountry2.DataValueField = "shipcountry";
            ddlCountry2.DataBind();
            ddlCountry2.Items[1].Selected = true;
        }
    }


    protected void Compareold(object sender, EventArgs e)
    {
        //string query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry1.SelectedItem.Value);
        string query = string.Format("SELECT (REPLACE( Convert(Nvarchar(5),EndingTime), ':', '.' ))AS StartingTime ,sampleno FROM Lp Where PlantCode=162 AND Prdate='10-05-2015' AND RouteId IS NOT NULL AND RouteId IN (5)  order by sampleno");
        DataTable dt = GetData(query);

        //string[] x = new string[dt.Rows.Count];
        //decimal[] y = new decimal[dt.Rows.Count];
        string[] x = new string[dt.Rows.Count];
        decimal[] y = new decimal[dt.Rows.Count];
        string strvari = string.Empty;
        string strvariRes = string.Empty;
        string[] strlengharr = new string[4];
        string[] strlengharrRes = new string[4];

        string xTimechk = "0";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strvari = dt.Rows[i][0].ToString().Trim();
            strlengharr = strvari.Split('.');
            strlengharrRes[0] = strlengharr[0];
            strlengharrRes[1] = ".";
            strlengharrRes[2] = strlengharr[1];
            //strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
            strvariRes = strlengharrRes[2];

            //x[i] = dt.Rows[i][0].ToString();

            if (xTimechk == strvariRes)
            {

                y[i] = Convert.ToInt32(dt.Rows[i][1]);
                xTimechk = strvariRes;
            }
            else
            {
                x[i] = strvariRes;
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
                xTimechk = strvariRes;
            }
        }
        LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = ddlCountry1.SelectedItem.Value, Data = y });

        // query = string.Format("select datepart(year, orderdate) Year, count(datepart(year, orderdate)) TotalOrders  from orders where shipcountry = '{0}' group by datepart(year, orderdate)", ddlCountry2.SelectedItem.Value);
        //  query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry2.SelectedItem.Value);
        query = string.Format("SELECT (REPLACE( Convert(Nvarchar(5),EndingTime), ':', '.' ))AS StartingTime ,sampleno FROM Lp Where PlantCode=162 AND Prdate='10-05-2015' AND RouteId IS NOT NULL AND RouteId=5 order by sampleno");
        dt = GetData(query);

        y = new decimal[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = dt.Rows[i][0].ToString();
            y[i] = Convert.ToInt32(dt.Rows[i][1]);
        }
        LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = ddlCountry2.SelectedItem.Value, Data = y });
        LineChart1.CategoriesAxis = string.Join(",", x);
        LineChart1.ChartTitle = string.Format("{0} and {1} Order Distribution", ddlCountry1.SelectedItem.Value, ddlCountry2.SelectedItem.Value);



        if (x.Length > 3)
        {
            LineChart1.ChartWidth = (x.Length * 40).ToString();
            //  LineChart1.ChartHeight = "700px";
            //LineChart1.ChartHeight = (y.Length * 10).ToString();
        }
        LineChart1.Visible = true;
    }




    protected void Compare(object sender, EventArgs e)
    {
        //string query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry1.SelectedItem.Value);
        string query = string.Format("SELECT (REPLACE( Convert(Nvarchar(5),EndingTime), ':', '.' ))AS StartingTime ,sampleno FROM Lp Where PlantCode=162 AND Prdate='10-05-2015' AND RouteId IS NOT NULL AND RouteId=5 AND sessions='AM' order by sampleno");
        DataTable dt = GetData(query);


        //string[] x = new string[dt.Rows.Count];
        //decimal[] y = new decimal[dt.Rows.Count];
        int xcount = dt.Rows.Count;
        xcount = xcount * 2;
        int incc = 0;
        string[] x = new string[xcount];

        decimal[] y = new decimal[dt.Rows.Count];
        string strvari = string.Empty;
        string strvariRes = string.Empty;
        string[] strlengharr = new string[4];
        string[] strlengharrRes = new string[4];

        string xTimechk = "0";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strvari = dt.Rows[i][0].ToString().Trim();
            strlengharr = strvari.Split('.');
            strlengharrRes[0] = strlengharr[0];
            strlengharrRes[1] = ".";
            strlengharrRes[2] = strlengharr[1];
            strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
            //  strvariRes = strlengharrRes[2];

            //x[i] = dt.Rows[i][0].ToString();

            if (xTimechk == strvariRes)
            {
                x[i] = strvariRes;
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
                xTimechk = strvariRes;
            }
            else
            {
                x[i] = strvariRes;
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
                xTimechk = strvariRes;
            }
            incc = i;
        }

        // LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = ddlCountry1.SelectedItem.Value, Data = y });
        // LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries {  Data = y });



        // query = string.Format("select datepart(year, orderdate) Year, count(datepart(year, orderdate)) TotalOrders  from orders where shipcountry = '{0}' group by datepart(year, orderdate)", ddlCountry2.SelectedItem.Value);
        //  query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry2.SelectedItem.Value);
        query = string.Format("SELECT (REPLACE( Convert(Nvarchar(5),LabCaptureTime), ':', '.' ))AS StartingTime ,sampleno FROM Lp Where PlantCode=162 AND Prdate='10-05-2015' AND RouteId IS NOT NULL AND RouteId=5 AND sessions='AM' order by sampleno");
        DataTable dt1 = GetData(query);
        dt1 = GetData(query);

        decimal[] y1 = new decimal[xcount];

        incc = incc + 1;

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            //x[i] = dt.Rows[i][0].ToString();
            //y[i] = Convert.ToInt32(dt.Rows[i][1]);


            strvari = dt1.Rows[i][0].ToString().Trim();
            strlengharr = strvari.Split('.');
            strlengharrRes[0] = strlengharr[0];
            strlengharrRes[1] = ".";
            strlengharrRes[2] = strlengharr[1];
            strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
            //strvariRes = strlengharrRes[2];

            //x[i] = dt.Rows[i][0].ToString();

            if (xTimechk == strvariRes)
            {
                x[incc] = strvariRes;
                y1[incc] = Convert.ToInt32(dt1.Rows[i][1]);
                xTimechk = strvariRes;
                //x[i] = "0";
            }
            else
            {
                x[incc] = strvariRes;
                y1[incc] = Convert.ToInt32(dt1.Rows[i][1]);
                xTimechk = strvariRes;
                // x[i] = "0";
            }
            incc++;
        }

        LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = ddlCountry2.SelectedItem.Value, Data = y1 });
        // LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries {Data = y1 });
        LineChart1.CategoriesAxis = string.Join(",", x);

        LineChart1.ChartTitle = string.Format("{0} and {1} Order Distribution", ddlCountry1.SelectedItem.Value, ddlCountry2.SelectedItem.Value);



        if (x.Length > 3)
        {
            LineChart1.ChartWidth = (x.Length * 40).ToString();
            //  LineChart1.ChartHeight = "700px";
            //LineChart1.ChartHeight = (y.Length * 10).ToString();
        }
        LineChart1.Visible = true;
    }

    protected void Compareorg(object sender, EventArgs e)
    {
        // string query = string.Format("select datepart(year, orderdate) Year, count(datepart(year, orderdate)) TotalOrders  from orders where shipcountry = '{0}' group by datepart(year, orderdate)", ddlCountry1.SelectedItem.Value);
        string query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry1.SelectedItem.Value);
        DataTable dt = GetData(query);

        string[] x = new string[dt.Rows.Count];
        decimal[] y = new decimal[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = dt.Rows[i][0].ToString();
            y[i] = Convert.ToInt32(dt.Rows[i][1]);
        }
        LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = ddlCountry1.SelectedItem.Value, Data = y });

        // query = string.Format("select datepart(year, orderdate) Year, count(datepart(year, orderdate)) TotalOrders  from orders where shipcountry = '{0}' group by datepart(year, orderdate)", ddlCountry2.SelectedItem.Value);
        query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry2.SelectedItem.Value);
        dt = GetData(query);

        y = new decimal[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = dt.Rows[i][0].ToString();
            y[i] = Convert.ToInt32(dt.Rows[i][1]);
        }
        LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = ddlCountry2.SelectedItem.Value, Data = y });
        LineChart1.CategoriesAxis = string.Join(",", x);

        LineChart1.ChartTitle = string.Format("{0} and {1} Order Distribution", ddlCountry1.SelectedItem.Value, ddlCountry2.SelectedItem.Value);
        if (x.Length > 3)
        {
            LineChart1.ChartWidth = (x.Length * 40).ToString();
            //  LineChart1.ChartHeight = "700px";
            //LineChart1.ChartHeight = (y.Length * 10).ToString();
        }
        LineChart1.Visible = true;
    }



    private static DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    
}