using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class ChartAssigntoAgent : System.Web.UI.Page
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

    }
    protected void Button5_Click(object sender, EventArgs e)
    {

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

    //public void LoadPlantcode()
    //{

    //    con = DB.GetConnection();
    //    string stt = "SELECT  *    from procurement where plant_code='"+pcode+"'";
    //    SqlCommand cmd = new SqlCommand(stt, con);
    //    SqlDataAdapter DA = new SqlDataAdapter(cmd);
    //    DA.Fill(DTG);
    //    ddl_Plantname.DataSource = DTG.Tables[0];
    //    ddl_Plantname.DataTextField = "Plant_Name";
    //    ddl_Plantname.DataValueField = "Plant_Code";
    //    ddl_Plantname.DataBind();

    //}

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}