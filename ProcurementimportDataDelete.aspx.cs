using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;
using System.Web.Services;
using System.Collections.Generic;

public partial class ProcurementimportDataDelete : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
   

    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();

    string FDATE;
    string TODATE;
    DateTime d1;
    DateTime d2;

    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    int datasetcount=0;
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
                    Session["tempp"] = Convert.ToString(pcode);
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        getagent();
                    }
                    if ((roleid >= 3) && (roleid != 9))
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        getagent();
                    }
                    if (roleid == 9)
                    {
                        Session["Plant_Code"] = "170".ToString();
                        pcode = "170";
                        loadspecialsingleplant();
                        getagent();
                    }

                }
                else
                {



                }

                Button9.Visible = false;
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;

                ViewState["pcode"] = pcode.ToString();
            }
            Button9.Visible = false;
        }

        catch
        {



        }
    }

    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

        datasetcount =  datasetcount + 1;
    }


    public void loadsingleplant()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
        datasetcount =  datasetcount + 1;
    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            DataTable getdata = new DataTable();
            SqlDataAdapter dsii = new SqlDataAdapter(cmd);
            getdata.Rows.Clear();
            dsii.Fill(getdata);
            ddl_Plantname.DataSource = getdata;
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
            ddl_Plantname.DataBind();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    




    protected void Button6_Click(object sender, EventArgs e)
    {
       getprocurementlist();
        datasetcount=0;

    }



    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]


   // public static List<string> GetCity(string prefixText)
   //{
   //     DataTable dt = new DataTable();
    
   //     SqlConnection CON = new SqlConnection();
   //     DbHelper DB = new DbHelper();
   //     CON = DB.GetConnection();

   //     //SqlCommand cmd = new SqlCommand("select AGENT_ID from PROCUREMENTIMPORT where AGENT_ID like @AGENT_ID+'%'  and @PLANT_CODE='" + pcode + "'  ", CON);
   //     //cmd.Parameters.AddWithValue("@AGENT_ID", prefixText);
   //     //cmd.Parameters.AddWithValue("@plant_code", pcode);


   //     SqlCommand cmd = new SqlCommand("select AGENT_ID from PROCUREMENTIMPORT where AGENT_ID like @AGENT_ID+'%'    ", CON);
   //     cmd.Parameters.AddWithValue("@AGENT_ID", prefixText);
      
   //     SqlDataAdapter adp = new SqlDataAdapter(cmd);       
   //     adp.Fill(dt);
   //     List<string> CityNames = new List<string>();
   //     for (int i = 0; i < dt.Rows.Count; i++)
   //     {
   //         CityNames.Add(dt.Rows[i][0].ToString());
   //     }
   //     return CityNames;
   // }

   
    //public static List<string> SearchCustomers(string prefixText)
    //{
    //    SqlConnection con = new SqlConnection();
    //    DbHelper db = new DbHelper();
    //    con = db.GetConnection();
      
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //          //  cmd.CommandText = "select Agent_id   from  procurementimport where   " + "ContactName like @SearchText + '%'   and  plant_code='" + pcode + "'    ";

    //            cmd.CommandText = "select Agent_id   from  procurementimport where   " + "ContactName like @SearchText + '%'      ";
    //            cmd.Parameters.AddWithValue("@SearchText", prefixText);
    //            cmd.Connection = con;
    //            con.Open();
    //            List<string> customers = new List<string>();
    //            using (SqlDataReader sdr = cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    customers.Add(sdr["ContactName"].ToString());

                       
    //                }
    //            }
                     
    //            return customers;
    //        }
    //    }
    
    protected void Button9_Click(object sender, EventArgs e)
    {
        getupdateagentdetails();
        getprocurementlist();
       
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    public void getupdateagentdetails()
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        DateTime dt1 = new DateTime();
                        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                        string d1 = dt1.ToString("MM/dd/yyyy");
                        string tid = row.Cells[1].Text;
                        string getagentid = row.Cells[2].Text;
                        string session = row.Cells[4].Text.Trim();
                      
                   
                        string doupdate;
                        con = DB.GetConnection();
                        if (RadioButtonList1.SelectedItem.Value == "1")
                        {
                            doupdate = "delete  FROM procurementimport   where plant_code='" + pcode + "'  and agent_id='" + getagentid + "' and prdate='" + d1 + "' and sessions='" + RadioButtonList2.SelectedItem.Text + "' and tid='" + tid + "'";
                        }
                        else
                        {
                            doupdate = "delete  FROM procurementimport   where plant_code='" + pcode + "'   and prdate='" + d1 + "' and sessions='" + RadioButtonList2.SelectedItem.Text + "'";

                        }
                        SqlCommand cmd = new SqlCommand(doupdate, con);
                        cmd.ExecuteNonQuery();
                        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Deleted) + "')</script>");
                      
                    }



                }
            }
        }
        catch
        {

        }
    }


    public void getagent()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            string stt = "SELECT agent_id   FROM (SELECT DISTINCT(agent_id) AS agent_id  FROM PROCUREMENTIMPORT where plant_code='" + pcode + "'  and prdate='" + d1 + "'    and sessions='" + RadioButtonList2.SelectedItem.Text + "'   ) AS UU  ORDER BY RAND(AGENT_ID) ASC  ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("AGETMASTER"));
            ddl_Agentid.DataSource = DTG.Tables[datasetcount];
            ddl_Agentid.DataTextField = "Agent_id";
            ddl_Agentid.DataValueField = "Agent_id";
            ddl_Agentid.DataBind();

            datasetcount = datasetcount + 1;
        }
        catch
        {

        }
    }

    public void getprocurementlist()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            string stt;
            if (RadioButtonList1.SelectedItem.Value == "1")
            {
                stt = "select tid,Agent_id,CONVERT(VARCHAR,prdate,103) AS prdate,Sessions,Milk_kg,Fat,Snf   from  procurementimport where Plant_Code='" + pcode + "'  and prdate='" + d1 + "'  and Agent_id='" + ddl_Agentid.SelectedItem.Text + "' and sessions='" + RadioButtonList2.SelectedItem.Text + "'  ";
            }
            else
            {
                stt = "select tid,Agent_id,CONVERT(VARCHAR,prdate,103) AS prdate,Sessions,Milk_kg,Fat,Snf   from  procurementimport where Plant_Code='" + pcode + "'  and prdate='" + d1 + "'  and sessions='" + RadioButtonList2.SelectedItem.Text + "'  ";
            }
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("PROCUREMENTIMPORT"));
            if (DTG.Tables[datasetcount].Rows.Count > 0)
            {
                GridView1.DataSource = DTG.Tables[datasetcount];
                GridView1.DataBind();
                Button9.Visible = true;
            }
            else
            {
                GridView1.DataSource = "";
                GridView1.DataBind();
                Button9.Visible = false;
            }
        }
        catch
        {


        }

    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtCity_TextChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Value == "1")
        {
            getagent();
            ddl_Agentid.Visible = true;
            Label1.Visible = true;
        }
        else
        {

            ddl_Agentid.Visible = false;
            Label1.Visible = false;
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        getagent();
    }
}