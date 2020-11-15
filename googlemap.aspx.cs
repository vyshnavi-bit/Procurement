using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class googlemap : System.Web.UI.Page
{
    SqlConnection con=new SqlConnection();
    DbHelper db=new DbHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        ConvertDataTabletoString();
    }


    public string ConvertDataTabletoString()
    {
        DataTable dt = new DataTable();
              con= db.GetConnection();
              string stre = "select title=city,lat=latitude,lng=longitude,Description from googlemap";
              SqlCommand cmd=new SqlCommand(stre,con);               
              SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            
        }
    
}