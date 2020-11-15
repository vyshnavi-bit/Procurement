using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
 
public partial class OrgReport1 : System.Web.UI.Page
{
    DbHelper db = new DbHelper();
    string get;
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindOrganaizationChart();
    }

    private void BindOrganaizationChart()
    {
        StringBuilder str = new StringBuilder();
        DataTable dt = new DataTable();
        con = db.GetConnection();
        try
        {

            string cmd = "select [Tid]  ,[name]  ,[parent]  ,[ToolTip]  from OragantionChart where plant_code='" + Session["TPCK"] + "'";
            SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
            adp.Fill(dt);


            str.Append(@"<script type='text/javascript'> google.charts.load('current', { packages: ['orgchart'] });
                        google.charts.setOnLoadCallback(drawChart);
                       function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Manager');
            data.addColumn('string', 'ToolTip');  
            data.addRows([");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    str.Append(" ['" + dt.Rows[i]["name"].ToString() + "', '" + dt.Rows[i]["parent"].ToString() + "', '" + dt.Rows[i]["ToolTip"].ToString() + "' ]");
                }
                else
                {
                    //   get += "<a href='SMSTESTINGPAGE.aspx'>Link one</a>";
                    str.Append(" ['" + dt.Rows[i]["name"].ToString() + "', '" + dt.Rows[i]["parent"].ToString() + "', '" + dt.Rows[i]["ToolTip"].ToString() + "' ],");

                }
            }
            str.Append("]);");
            str.Append("  var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));");
            str.Append("  chart.draw(data, { allowHtml: true });");
            str.Append("}");
            str.Append("</script>");
            ltrScript.Text = str.ToString();
        }
        catch
        { }
    }
}