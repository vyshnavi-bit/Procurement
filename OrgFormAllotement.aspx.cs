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

public partial class OrgFormAllotement : System.Web.UI.Page
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
    DataTable SUBNAME = new DataTable();
    DataTable MANNAME = new DataTable();
    DataTable sUPERVISOR = new DataTable();
    DataTable SYSTEMNAME = new DataTable();
    DataTable Manager = new DataTable();
    DataTable CasualEdit = new DataTable();
    string FDATE;
    string TODATE;
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    SqlDataReader dr;
    int datasetcount = 0;

    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataTable fatkgdt = new DataTable();
    DataTable Route = new DataTable();
    string d1;
    string d2;
    string agentcode;
    int countinsertdetails;
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
                 
                    //   txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        mANnAME();
                        Routename();
                        getSupername();
                        sysnAME();
                        Supername();
                        casedit();
                     //  caseparantdit();
                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;

                        mANnAME();
                        Routename();
                        getSupername();
                        sysnAME();
                        Supername();
                        casedit();
                    //    caseparantdit();
                    }

                }
                else
                {
                    //LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;

                    mANnAME();
                    Routename();
                    getSupername();
                    sysnAME();
                    Supername();
                    casedit();
                    caseparantdit();

                }

            
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;
            
                ViewState["pcode"] = pcode.ToString();
 
             
            }
           
        }

        

        catch
        {



        }
    }

    public void LoadPlantcode()
    {
        try
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

            ddl_Plantname1.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname1.DataTextField = "Plant_Name";
            ddl_Plantname1.DataValueField = "Plant_Code";
            ddl_Plantname1.DataBind();

            ddl_Plantname2.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname2.DataTextField = "Plant_Name";
            ddl_Plantname2.DataValueField = "Plant_Code";
            ddl_Plantname2.DataBind();

            ddl_Plantname3.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname3.DataTextField = "Plant_Name";
            ddl_Plantname3.DataValueField = "Plant_Code";
            ddl_Plantname3.DataBind();

            ddl_Plantname4.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname4.DataTextField = "Plant_Name";
            ddl_Plantname4.DataValueField = "Plant_Code";
            ddl_Plantname4.DataBind();


            ddl_Plantname5.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname5.DataTextField = "Plant_Name";
            ddl_Plantname5.DataValueField = "Plant_Code";
            ddl_Plantname5.DataBind();

            ddl_Plantname6.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname6.DataTextField = "Plant_Name";
            ddl_Plantname6.DataValueField = "Plant_Code";
            ddl_Plantname6.DataBind();

            ddl_Plantname7.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname7.DataTextField = "Plant_Name";
            ddl_Plantname7.DataValueField = "Plant_Code";
            ddl_Plantname7.DataBind();

            ddl_Plantname8.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname8.DataTextField = "Plant_Name";
            ddl_Plantname8.DataValueField = "Plant_Code";
            ddl_Plantname8.DataBind();

            datasetcount = datasetcount + 1;
        }
        catch
        {

        }
    }


    public void loadsingleplant()
    {
        try
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

        ddl_Plantname1.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname1.DataTextField = "Plant_Name";
        ddl_Plantname1.DataValueField = "Plant_Code";
        ddl_Plantname1.DataBind();

        ddl_Plantname2.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname2.DataTextField = "Plant_Name";
        ddl_Plantname2.DataValueField = "Plant_Code";
        ddl_Plantname2.DataBind();

        ddl_Plantname3.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname3.DataTextField = "Plant_Name";
        ddl_Plantname3.DataValueField = "Plant_Code";
        ddl_Plantname3.DataBind();

        ddl_Plantname4.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname4.DataTextField = "Plant_Name";
        ddl_Plantname4.DataValueField = "Plant_Code";
        ddl_Plantname4.DataBind();

        ddl_Plantname5.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname5.DataTextField = "Plant_Name";
        ddl_Plantname5.DataValueField = "Plant_Code";
        ddl_Plantname5.DataBind();

        ddl_Plantname6.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname6.DataTextField = "Plant_Name";
        ddl_Plantname6.DataValueField = "Plant_Code";
        ddl_Plantname6.DataBind();

        ddl_Plantname7.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname7.DataTextField = "Plant_Name";
        ddl_Plantname7.DataValueField = "Plant_Code";
        ddl_Plantname7.DataBind();

        ddl_Plantname8.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname8.DataTextField = "Plant_Name";
        ddl_Plantname8.DataValueField = "Plant_Code";
        ddl_Plantname8.DataBind();

        datasetcount = datasetcount + 1;
        }
        catch
        {

        }
    }

    public void mANnAME()
    {
        try
        {

        con = DB.GetConnection();
        string stt = "SELECT name  FROM OragantionChart where plant_code='" + ddl_Plantname2.SelectedItem.Value + "' and PARENT='Manager' ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        Manager.Rows.Clear();
        DA.Fill(Manager);
        ManName.DataSource = Manager;
        ManName.DataTextField = "name";
        ManName.DataValueField = "name";
        ManName.DataBind();
        datasetcount = datasetcount + 1;
        }
        catch
        {

        }
       
    }

    public void casedit()
    {
        try
        {

            con = DB.GetConnection();
            string stt = "SELECT name  FROM OragantionChart where plant_code='" + ddl_Plantname8.SelectedItem.Value + "' and url='Casuals'    GROUP BY NAME  ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            Manager.Rows.Clear();
            DA.Fill(Manager);
            CasualName1.DataSource = Manager;
            CasualName1.DataTextField = "name";    
            CasualName1.DataValueField = "name";
            CasualName1.DataBind();

            CasualName2.DataSource = Manager;
            CasualName2.DataTextField = "name";   
            CasualName2.DataValueField = "name";
            CasualName2.DataBind();



            datasetcount = datasetcount + 1;
        }
        catch
        {

        }

    }

    public void caseparantdit()
    {
        try
        {

            con = DB.GetConnection();
            string stt = "SELECT parent  FROM OragantionChart where plant_code='" + ddl_Plantname8.SelectedItem.Value + "' and url='Casuals'    GROUP BY parent  ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            Manager.Rows.Clear();
            DA.Fill(Manager);
            //CasualName1.DataSource = Manager;
            //CasualName1.DataTextField = "name";
            //CasualName1.DataValueField = "name";
            //CasualName1.DataBind();

            CasualName2.DataSource = Manager;
            CasualName2.DataTextField = "parent";
            CasualName2.DataValueField = "parent";
            CasualName2.DataBind();


            datasetcount = datasetcount + 1;
        }
        catch
        {

        }

    }
    public void Routename()
    {
        try
        {
        con = DB.GetConnection();
        string stt = "sELECT Route_ID,Route_Name   FROM  route_master where Plant_Code='"+ddl_Plantname5.SelectedItem.Value+"' group by Route_ID,Route_Name  order by Route_ID ASC";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        Route.Rows.Clear();
        DA.Fill(Route);

        Drop_Routename.DataSource = Route;
        Drop_Routename.DataTextField = "Route_Name";
        Drop_Routename.DataValueField = "Route_ID";
        Drop_Routename.DataBind();

        }
        catch
        {

        }
    //    datasetcount = datasetcount + 1;

    }
    public void Supername()
    {
        try
        {
        con = DB.GetConnection();
        //string stt = "Select Name   from    OragantionChart where plant_code='" + ddl_Plantname5.SelectedItem.Value + "'   and Url='Supervisor'";
        string Stt = "Select Name   from    OragantionChart WHERE plant_code='" + ddl_Plantname5.SelectedItem.Value + "'   and Url='Supervisor'";
        SqlCommand cmd = new SqlCommand(Stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        SUBNAME.Rows.Clear();
        DA.Fill(SUBNAME);
        supervisorr.DataSource = SUBNAME;
        supervisorr.DataTextField = "Name";
        supervisorr.DataValueField = "Name";
        supervisorr.DataBind();
        datasetcount = datasetcount + 1;
        }
        catch
        {

        }
    }

    public void getSupername()
    {
        try
        {
            con = DB.GetConnection();
            //string stt = "Select Name   from    OragantionChart where plant_code='" + ddl_Plantname5.SelectedItem.Value + "'   and Url='Supervisor'";
            string Stt = "select  SupervisorName  as Name  from Supervisor_Details where plant_code='" + ddl_Plantname4.SelectedItem.Value + "' group by SupervisorName";
            SqlCommand cmd = new SqlCommand(Stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            sUPERVISOR.Rows.Clear();
            DA.Fill(sUPERVISOR);
            ddl_superwise.DataSource = sUPERVISOR;
            ddl_superwise.DataTextField = "Name";
            ddl_superwise.DataValueField = "Name";
            ddl_superwise.DataBind();
            datasetcount = datasetcount + 1;
        }
        catch
        {

        }
    }
    public void sysnAME()
    {
        try
        {
        con = DB.GetConnection();
        string stt = "SELECT name  FROM OragantionChart where plant_code='" + ddl_Plantname3.SelectedItem.Value + "' and url='System'  GROUP BY  name ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        SYSTEMNAME.Rows.Clear();
        DA.Fill(SYSTEMNAME);

        ddlsysname.DataSource = SYSTEMNAME;
        ddlsysname.DataTextField = "name";
        ddlsysname.DataValueField = "name";
        ddlsysname.DataBind();
        datasetcount = datasetcount + 1;
        }
        catch
        {

        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        int index = Int32.Parse(e.Item.Value);
        MultiView1.ActiveViewIndex = index;
      
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        try
        {
            string Planttype;
            if ((ddl_Plantname.SelectedItem.Value == "155") || (ddl_Plantname.SelectedItem.Value == "156") || (ddl_Plantname.SelectedItem.Value == "158") || (ddl_Plantname.SelectedItem.Value == "159") || (ddl_Plantname.SelectedItem.Value == "160") || (ddl_Plantname.SelectedItem.Value == "161") || (ddl_Plantname.SelectedItem.Value == "162") || (ddl_Plantname.SelectedItem.Value == "163") || (ddl_Plantname.SelectedItem.Value == "164"))
            {

                Planttype = "Cow Plant";
            }
            else
            {
                Planttype = "Buffalo Plant";

            }
            string saveplant;
            con = DB.GetConnection();
            saveplant = "Insert into OragantionChart(Name,parent,ToolTip,Plant_code) values('" + ddl_Plantname.SelectedItem.Text + "','" + Planttype + "','" + txt_plandesc.Text + "','" + ddl_Plantname.SelectedItem.Value + "')";
            SqlCommand cmd = new SqlCommand(saveplant, con);
            cmd.ExecuteNonQuery();
            string message = "Record Save SuccessFully";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);

            txt_plandesc.Text = "";
        }
        catch
        {

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {

        string saveplant1;
        con = DB.GetConnection();
        saveplant1 = "Insert into OragantionChart(Name,parent,ToolTip,Plant_code) values('" + ddl_manager.SelectedItem.Text + "','" + ddl_Plantname1.SelectedItem.Text + "','" + txt_plandesc1.Text + "','" + ddl_Plantname1.SelectedItem.Value + "')";
        SqlCommand cmd1 = new SqlCommand(saveplant1, con);
        cmd1.ExecuteNonQuery();

        string saveplant;
        con = DB.GetConnection();
        saveplant = "Insert into OragantionChart(Name,parent,ToolTip,Plant_code) values('" + txt_ManagerName.Text + "','" + ddl_manager.SelectedItem.Text + "','" + txt_plandesc1.Text + "','" + ddl_Plantname1.SelectedItem.Value + "')";
        SqlCommand cmd = new SqlCommand(saveplant, con);
        cmd.ExecuteNonQuery();
        string message = "Record Save SuccessFully";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
        mANnAME();
        txt_ManagerName.Text = "";
        txt_plandesc1.Text = "";

        }
        catch
        {

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string saveplant;
            con = DB.GetConnection();
            saveplant = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + txt_sysname.Text + "','" + ManName.SelectedItem.Text + "','" + txt_plandesc2.Text + "','System','" + ddl_Plantname2.SelectedItem.Value + "')";
            SqlCommand cmd = new SqlCommand(saveplant, con);
            cmd.ExecuteNonQuery();
            string message = "Record Save SuccessFully";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
            //txt_sysname.Text = "";
            txt_plandesc2.Text = "";
            sysnAME();
        }
        catch
        {

        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            string saveplant;
            con = DB.GetConnection();
            saveplant = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + sysopname.Text + "','" + ddlsysname.SelectedItem.Text + "','" + txt_empdesc.Text + "','Employee','" + ddl_Plantname3.SelectedItem.Value + "')";
            SqlCommand cmd = new SqlCommand(saveplant, con);
            cmd.ExecuteNonQuery();

            string message = "Record Save SuccessFully";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
            sysopname.Text = "";
            txt_empdesc.Text = "";

           
            sysnAME();
          
          
        }
        catch
        {

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //ADDSUPERVISOR
        try
        {
        string saveplant;
        con = DB.GetConnection();
        saveplant = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + ddl_route.SelectedItem.Text + "','" + ddl_Plantname4.SelectedItem.Text + "','" + txt_supervisordesc.Text + "','Route','" + ddl_Plantname4.SelectedItem.Value + "')";
        SqlCommand cmd = new SqlCommand(saveplant, con);
        cmd.ExecuteNonQuery();

        string saveplant1;
        con = DB.GetConnection();
        saveplant1 = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + ddl_superwise.SelectedItem.Text + "','" + ddl_route.SelectedItem.Text + "','" + txt_supervisordesc.Text + "','Supervisor','" + ddl_Plantname4.SelectedItem.Value + "')";
        SqlCommand cmd1 = new SqlCommand(saveplant1, con);
        cmd1.ExecuteNonQuery();

        string message = "Record Save SuccessFully";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);

        txt_supervisordesc.Text = "";
       
        }
        catch
        {

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
        string saveplant;
        con = DB.GetConnection();
        saveplant = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + Drop_Routename.SelectedItem.Text + "','" + supervisorr.SelectedItem.Text + "','" + txt_employee.Text + "','Route','" + ddl_Plantname5.SelectedItem.Value + "')";
        SqlCommand cmd = new SqlCommand(saveplant, con);
        cmd.ExecuteNonQuery();

        string message = "Record Save SuccessFully";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
        txt_plandesc1.Text = "";

        }
        catch
        {

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
        string saveplant;
        con = DB.GetConnection();
        saveplant = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + dd_casulas.SelectedItem.Text + "','" + ddl_Plantname6.SelectedItem.Text + "','" + txt_supervisordesc.Text + "','" + dd_casulas.SelectedItem.Text + "','" + ddl_Plantname6.SelectedItem.Value + "')";
        SqlCommand cmd = new SqlCommand(saveplant, con);
        cmd.ExecuteNonQuery();

        string saveplant1;
        con = DB.GetConnection();
        saveplant1 = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + Casuals.Text + "','" + dd_casulas.SelectedItem.Text + "','" + txt_Casuladesc.Text + "','" + dd_casulas.SelectedItem.Text + "','" + ddl_Plantname6.SelectedItem.Value + "')";
        SqlCommand cmd1 = new SqlCommand(saveplant1, con);
        cmd1.ExecuteNonQuery();
        string message = "Record Save SuccessFully";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);

        txt_supervisordesc.Text = "";
        Casuals.Text = "";
        txt_Casuladesc.Text = "";
        }
        catch
        {

        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        try
        {
        string saveplant;
        con = DB.GetConnection();
        saveplant = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + DropDownList7.SelectedItem.Text + "','" + ddl_Plantname7.SelectedItem.Text + "','" + Security.Text + "','" + txt_securitydesc.Text + "','" + ddl_Plantname7.SelectedItem.Value + "')";
        SqlCommand cmd = new SqlCommand(saveplant, con);
        cmd.ExecuteNonQuery();

        string saveplant1;
        con = DB.GetConnection();
        saveplant1 = "Insert into OragantionChart(Name,parent,ToolTip,url,Plant_code) values('" + Security.Text + "','" + DropDownList7.SelectedItem.Text + "','" + txt_securitydesc.Text + "','" + txt_securitydesc.Text + "','" + ddl_Plantname7.SelectedItem.Value + "')";
        SqlCommand cmd1 = new SqlCommand(saveplant1, con);
        cmd1.ExecuteNonQuery();

        string message = "Record Save SuccessFully";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
        
        Security.Text="";
        txt_securitydesc.Text = "";


        }
        catch
        {

        }
    }
    protected void ddl_Plantname2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            mANnAME();
        }
        catch
        {

        }
    }
    protected void ddl_Plantname3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            sysnAME();
            Supername();
        }
        catch
        {

        }
    }
    protected void ddl_Plantname5_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        Supername();
        Routename();
        }
        catch
        {

        }   
    }
    protected void ddl_Plantname4_SelectedIndexChanged(object sender, EventArgs e)
    {
        getSupername();
    }
    protected void ddl_Plantname8_SelectedIndexChanged(object sender, EventArgs e)
    {
        casedit();
       // caseparantdit();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        try
        {
            con = DB.GetConnection();
            string UpdateCasuals;
            UpdateCasuals = "Update OragantionChart set Name='" + CasualName1.SelectedItem.Text + "',Parent='" + CasualName2.SelectedItem.Text + "',Tooltip='" + editCasual.Text + "',plant_code='" + ddl_Plantname8.SelectedItem.Value + "'  where plant_code='" + ddl_Plantname8.SelectedItem.Value + "'   and Url='Casuals' AND   Name='" + CasualName1.SelectedItem.Text + "' ";
            SqlCommand cmd = new SqlCommand(UpdateCasuals, con);
            cmd.ExecuteNonQuery();

            string message = "UPDATED Save SuccessFully";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
            editCasual.Text = "";
        }
        catch
        {

        }

    }
}