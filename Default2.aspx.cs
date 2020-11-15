using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
    }
    DbHelper db = new DbHelper();
    protected void BindData()
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        con = db.GetConnection();
        string cmdstr = "Select sno as noofsample,plantcode,plantname,milkkg, milkltr,fat,snf   from(Select  count(*) as sno, convert(decimal(18,2),Sum(milk_kg)) as milkkg,convert(decimal(18,2),Sum(milk_kg/1.03)) as milkltr,convert(decimal(18,2),Avg(fat)) as fat,convert(decimal(18,2),Avg(snf)) as snf,plant_code as plantcode   from procurement  where prdate='9-25-2017'  group by plant_code) as lp left join (Select plant_code,plant_name as plantname  from  plant_master group by plant_code,plant_name ) as pm on lp.plantcode=pm.plant_code  order by plantcode";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        DataList1.DataSource = ds.Tables[0];
        DataList1.DataBind();
    }
}