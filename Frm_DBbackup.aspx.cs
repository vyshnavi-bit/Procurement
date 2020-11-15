using System;
using System.Collections;
using System.Configuration;
using System.Data.Common;
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
using System.Data.OleDb;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Workflow.Activities;
using System.Data.Odbc;
using System.Text;
using System.Web.Security;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;



public partial class Frm_DBbackup : System.Web.UI.Page
{
    int ccode = 1, pcode;
    string sqlstr = string.Empty;
   
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    SqlCommand cmd = new SqlCommand();
    DateTime dat = new DateTime();
    DataSet ds = new DataSet();

    public string managmobNo;
    public string pname;
    public string cname;

    string d1 = string.Empty;
    string d2 = string.Empty;
    string filename = string.Empty;
    string ExportType = string.Empty;
    OleDbConnection olecon = new OleDbConnection();
    //Common Fields
   public string Compath = string.Empty;
   public string ComOledbcon = string.Empty;

   public string path = string.Empty;

   protected void Page_Load(object sender, EventArgs e)
   {
       if (!IsPostBack)
       {
           if ((Session["User_ID"] != null) && (Session["Password"] != null))
           {
               ccode = Convert.ToInt32(Session["Company_code"]);
               pcode = Convert.ToInt32(Session["Plant_Code"]);
               cname = Session["cname"].ToString();
               pname = Session["pname"].ToString();
               managmobNo = Session["managmobNo"].ToString();
               dat = System.DateTime.Now;
               txt_FromDate.Text = dat.ToString("dd/MM/yyyy");
               txt_ToDate.Text = dat.ToString("dd/MM/yyyy");
               if (program.Guser_role < program.Guser_PermissionId)
               {
                   loadsingleplant();
               }
               else
               {
                   LoadPlantcode();
               }
               pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);

               Lbl_Errormsg.Visible = false;
               rbtLstReportItems.SelectedIndex = 0;

           }
           else
           {

               Server.Transfer("LoginDefault.aspx");
           }
       }
       else
       {
           if ((Session["User_ID"] != null) && (Session["Password"] != null))
           {
               ccode = Convert.ToInt32(Session["Company_code"]);
               pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
               Lbl_Errormsg.Visible = false;

           }
           else
           {

               Server.Transfer("LoginDefault.aspx");
           }

       }
   }

    private void LoadPlantcode()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void loadsingleplant()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void Datefunc()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            d1 = dt1.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


    private void RpyGroupBoxChange()
    {
        try
        {
            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
                {
                    ExportType = "Procurement";
                    Compath = "~/EXData/procurement/";
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {

                    ExportType = "ProcurementImport";
                    Compath = "~/EXData/procurementimport/";
                  

                }
                else
                {
                }
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Select Report Type";
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


    protected void rbtLstReportItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
                {
                    ExportType = "Procurement";
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {
                    ExportType = "ProcurementImport";
                }
                else
                {
                }
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Select the Type...";
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    //Region Backup
    #region Backup
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        try        
        {
            Datefunc();
            RpyGroupBoxChange();
            CheckBackupFileAvail();           
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = "Please, Check BillPeriod Date...";
        }
    }

    private void CheckBackupFileAvail()
    {
        try
        {
            
            int Backupflag = 0;
            string str = string.Empty;
            string strBackupFileName = string.Empty;
            string Backupfilename = string.Empty;
            Backupfilename = ddl_PlantName.SelectedItem.Value.Trim() + '_' + txt_FromDate.Text.Trim().Replace('/', '.') + '_' + txt_ToDate.Text.Trim().Replace('/', '.') + ".xlsx"; ;
            //  strDeleteFileName = "~/EXData/" + Deletefilename + ".xlsx";            

            DirectoryInfo Dirinfo = new DirectoryInfo(string.Concat(Server.MapPath(Compath.Trim())));            
            FileInfo[] FilInfo = Dirinfo.GetFiles("*.xlsx");//xlsx or xls 
            if (FilInfo != null)
            {
                ddl_TableName.Items.Clear();
                foreach (FileInfo fi in FilInfo)
                {
                    ddl_TableName.Items.Add(fi.ToString().Trim());
                    if (fi.ToString().Trim() == Backupfilename.Trim())
                    {
                        Backupflag = 1;
                    }
                }
            }

            if (Backupflag == 1)
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Success, Already Bacup Availed...";
            }
            else
            {
                GetData();
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }

    public void GetData()
    {
        try
        {           
            string str = string.Empty;
            int Flag = 0;
            str = "SELECT COUNT(*) AS Counts  FROM Bill_date Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Bill_frmdate='" + d1.Trim() + "' AND Bill_todate='" + d2.Trim() + "' ";
            Flag = dbaccess.ExecuteScalarint(str);

            if (Flag > 0)
            {               
                Executemain();
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Check BillPeriod Date...";
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }

    private void Executemain()
    {
        try
        {
            string strDownloadFileName = "";
            string strExcelConn = "";
            filename = ddl_PlantName.SelectedItem.Value.Trim() + '_' + txt_FromDate.Text.Trim().Replace('/', '.') + '_' + txt_ToDate.Text.Trim().Replace('/', '.');
            //// Excel 97-2003
            //strDownloadFileName = "~/DownloadFiles/" + filename + ".xls";
            //strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(strDownloadFileName) + ";Extended Properties='Excel 8.0;HDR=Yes'";
            // Excel 2007
            strDownloadFileName = Compath + filename + ".xlsx";
            strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strDownloadFileName) + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'";
            // Retrieve data from SQL Server table.
            DataTable dtSQL = GetDatafromDatabase();
            // Export data to an Excel spreadsheet.
            ExportToExcel(strExcelConn, dtSQL);

            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = "Success, Bacup Completed...";
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }



    protected DataTable GetDatafromDatabase()
    {

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        using (con = dbaccess.GetConnection())
        {
            if (ExportType == "Procurement")
            {
                da = new SqlDataAdapter("SELECT *  FROM Procurement Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate between '" + d1.Trim() + "' AND '" + d2.Trim() + "' order by Tid", con);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }

            else if (ExportType == "ProcurementImport")
            {
                da = new SqlDataAdapter("SELECT *  FROM Procurementimport Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate between '" + d1.Trim() + "' AND '" + d2.Trim() + "' order by Tid", con);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }
        }

        return dt;
    }

    protected void ExportToExcel(string strConn, System.Data.DataTable dtSQL)
    {
        try
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {

                if (ExportType == "Procurement")
                {
                    // Create a new sheet in the Excel spreadsheet.
                    OleDbCommand cmd = new OleDbCommand("create table Procurement(Tid Integer,Agent_id Varchar(50),Prdate Date,Sessions Varchar(10),Milk_ltr Double,Fat Double,Snf Double,Rate Double,Amount Double,Plant_Code Varchar(50),Route_id Varchar(50),NoofCans Integer,Milk_kg Double,Clr Double,Company_Code Varchar(50),Ratechart_Id Varchar(50),Milk_Nature Integer,ComRate Double,SampleId Integer,Sampleno Varchar(50),milk_tip_st_time Varchar(50),milk_tip_end_time Varchar(50),usr_weigher Varchar(50),usr_analizer Varchar(50),fat_kg Double,snf_kg Double,Truck_id Integer,Status Integer,CartageAmount Double,SplBonusAmount Double,RateStatus Integer)", conn);

                    // Open the connection.
                    conn.Open();

                    // Execute the OleDbCommand.
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Procurement (Tid,Agent_id,Prdate,Sessions,Milk_ltr,Fat,Snf,Rate,Amount,Plant_Code,Route_id,NoofCans,Milk_kg,Clr,Company_Code ,Ratechart_Id ,Milk_Nature,ComRate,SampleId,Sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,Truck_id,Status,CartageAmount,SplBonusAmount,RateStatus) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    
                    // Add the parameters.
                    // cmd.Parameters.Add("Tid", OleDbType.Integer);

                    cmd.Parameters.Add("Tid", OleDbType.Integer, 4, "Tid");
                    cmd.Parameters.Add("Agent_id", OleDbType.VarChar, 50, "Agent_id");
                    cmd.Parameters.Add("Prdate", OleDbType.Date, 8, "Prdate");
                    cmd.Parameters.Add("Sessions", OleDbType.VarChar, 10, "Sessions");
                    cmd.Parameters.Add("Milk_ltr", OleDbType.Double, 8, "Milk_ltr");
                    cmd.Parameters.Add("Fat", OleDbType.Double, 8, "Fat");
                    cmd.Parameters.Add("Snf", OleDbType.Double, 8, "Snf");
                    cmd.Parameters.Add("Rate", OleDbType.Double, 8, "Rate");
                    cmd.Parameters.Add("Amount", OleDbType.Double, 8, "Amount");
                    cmd.Parameters.Add("Plant_Code", OleDbType.VarChar, 50, "Plant_Code");
                    cmd.Parameters.Add("Route_id", OleDbType.VarChar, 50, "Route_id");
                    cmd.Parameters.Add("NoofCans", OleDbType.Integer, 4, "NoofCans");                  
                    cmd.Parameters.Add("Milk_kg", OleDbType.Double, 8, "Milk_kg");
                    cmd.Parameters.Add("Clr", OleDbType.Double, 8, "Clr");
                    cmd.Parameters.Add("Company_Code", OleDbType.VarChar, 50, "Company_Code");
                    cmd.Parameters.Add("Ratechart_Id", OleDbType.VarChar, 50, "Ratechart_Id");
                    cmd.Parameters.Add("Milk_Nature", OleDbType.Integer, 4, "Milk_Nature");
                    cmd.Parameters.Add("ComRate", OleDbType.Double, 8, "ComRate");
                    cmd.Parameters.Add("SampleId", OleDbType.Integer, 4, "SampleId");
                    cmd.Parameters.Add("Sampleno", OleDbType.VarChar, 50, "Sampleno");
                    cmd.Parameters.Add("milk_tip_st_time", OleDbType.VarChar, 50, "milk_tip_st_time");
                    cmd.Parameters.Add("milk_tip_end_time", OleDbType.VarChar, 50, "milk_tip_end_time");
                    cmd.Parameters.Add("usr_weigher", OleDbType.VarChar, 50, "usr_weigher");
                    cmd.Parameters.Add("usr_analizer", OleDbType.VarChar, 50, "usr_analizer");
                    cmd.Parameters.Add("fat_kg", OleDbType.Double, 8, "fat_kg");
                    cmd.Parameters.Add("snf_kg", OleDbType.Double, 8, "snf_kg");
                    cmd.Parameters.Add("Truck_id", OleDbType.Integer, 4, "Truck_id");
                    cmd.Parameters.Add("Status", OleDbType.Integer, 4, "Status");
                    cmd.Parameters.Add("CartageAmount", OleDbType.Double, 8, "CartageAmount");
                    cmd.Parameters.Add("SplBonusAmount", OleDbType.Double, 8, "SplBonusAmount");
                    cmd.Parameters.Add("RateStatus", OleDbType.Integer, 4, "RateStatus");
                    

                    // Initialize an OleDBDataAdapter object.
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Procurement", conn);

                    // Set the InsertCommand of OleDbDataAdapter, 
                    // which is used to insert data.
                    da.InsertCommand = cmd;

                    // Changes the Rowstate()of each DataRow to Added,
                    // so that OleDbDataAdapter will insert the rows.
                    foreach (DataRow dr in dtSQL.Rows)
                    {
                        dr.SetAdded();
                    }

                    // Insert the data into the Excel spreadsheet.
                    da.Update(dtSQL);
                }

                else if (ExportType == "ProcurementImport")
                {
                    // Create a new sheet in the Excel spreadsheet.
                    OleDbCommand cmd = new OleDbCommand("create table Procurementimport(Tid Integer,Agent_id Varchar(50),Prdate Date,Sessions Varchar(50),Milk_ltr Double,Fat Double,Snf Double,Plant_Code Varchar(50),Route_id  Varchar(50),NoofCans Integer,Milk_kg Double,Clr Double,Company_Code Varchar(50),Milk_Nature Integer,SampleId Integer,Sampleno Varchar(50),milk_tip_st_time Varchar(50),milk_tip_end_time Varchar(50),usr_weigher Varchar(50),usr_analizer Varchar(50),fat_kg Double,snf_kg Double,Truck_id Integer,Status Integer,Remark Varchar(100),Remarkstatus Integer,modify_aid Varchar(50),modify_Kg Varchar(50),modify_fat Varchar(50),modify_snf Varchar(50),DIFFKG Double,DIFFFAT Double,DIFFSNF Double) ", conn);
                    // OleDbCommand cmd = new OleDbCommand("create table Procurementimport(Tid Integer,Agent_id Varchar(50),Prdate Date)", conn);

                    // Open the connection.
                    conn.Open();

                    // Execute the OleDbCommand.
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Procurementimport (Tid ,Agent_id ,Prdate ,Sessions ,Milk_ltr ,Fat ,Snf ,Plant_Code ,Route_id ,NoofCans ,Milk_kg ,Clr ,Company_Code ,Milk_Nature ,SampleId ,Sampleno ,milk_tip_st_time ,milk_tip_end_time ,usr_weigher ,usr_analizer ,fat_kg ,snf_kg ,Truck_id ,Status ,Remark ,Remarkstatus,modify_aid ,modify_Kg ,modify_fat ,modify_snf,DIFFKG,DIFFFAT,DIFFSNF) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    // cmd.CommandText = "INSERT INTO Procurementimport (Tid ,Agent_id ,Prdate ) values (?,?,?)";

                    // Add the parameters.
                    // cmd.Parameters.Add("Tid", OleDbType.Integer);

                    cmd.Parameters.Add("Tid", OleDbType.Integer, 4, "Tid");
                    cmd.Parameters.Add("Agent_id", OleDbType.VarChar, 50, "Agent_id");
                    cmd.Parameters.Add("Prdate", OleDbType.Date, 8, "Prdate");
                    cmd.Parameters.Add("Sessions", OleDbType.VarChar, 50, "Sessions");
                    cmd.Parameters.Add("Milk_ltr", OleDbType.Double, 8, "Milk_ltr");
                    cmd.Parameters.Add("Fat", OleDbType.Double, 8, "Fat");
                    cmd.Parameters.Add("Snf", OleDbType.Double, 8, "Snf");
                    cmd.Parameters.Add("Plant_Code", OleDbType.VarChar, 50, "Plant_Code");
                    cmd.Parameters.Add("Route_id", OleDbType.VarChar, 50, "Route_id");
                    cmd.Parameters.Add("NoofCans", OleDbType.Integer, 4, "NoofCans");
                    cmd.Parameters.Add("Milk_kg", OleDbType.Double, 8, "Milk_kg");
                    cmd.Parameters.Add("Clr", OleDbType.Double, 8, "Clr");
                    cmd.Parameters.Add("Company_Code", OleDbType.VarChar, 50, "Company_Code");
                    cmd.Parameters.Add("Milk_Nature", OleDbType.Integer, 4, "Milk_Nature");
                    cmd.Parameters.Add("SampleId", OleDbType.Integer, 4, "SampleId");
                    cmd.Parameters.Add("Sampleno", OleDbType.VarChar, 50, "Sampleno");
                    cmd.Parameters.Add("milk_tip_st_time", OleDbType.VarChar, 50, "milk_tip_st_time");
                    cmd.Parameters.Add("milk_tip_end_time", OleDbType.VarChar, 50, "milk_tip_end_time");
                    cmd.Parameters.Add("usr_weigher", OleDbType.VarChar, 50, "usr_weigher");
                    cmd.Parameters.Add("usr_analizer", OleDbType.VarChar, 50, "usr_analizer");
                    cmd.Parameters.Add("fat_kg", OleDbType.Double, 8, "fat_kg");
                    cmd.Parameters.Add("snf_kg", OleDbType.Double, 8, "snf_kg");
                    cmd.Parameters.Add("Truck_id", OleDbType.Integer, 4, "Truck_id");
                    cmd.Parameters.Add("Status", OleDbType.Integer, 4, "Status");
                    cmd.Parameters.Add("Remark", OleDbType.VarChar, 100, "Remark");
                    cmd.Parameters.Add("Remarkstatus", OleDbType.Integer, 4, "Remarkstatus");
                    cmd.Parameters.Add("modify_aid", OleDbType.VarChar, 50, "modify_aid");
                    cmd.Parameters.Add("modify_Kg", OleDbType.VarChar, 50, "modify_Kg");
                    cmd.Parameters.Add("modify_fat", OleDbType.VarChar, 50, "modify_fat");
                    cmd.Parameters.Add("modify_snf", OleDbType.VarChar, 50, "modify_snf");
                    cmd.Parameters.Add("DIFFKG", OleDbType.Double, 8, "DIFFKG");
                    cmd.Parameters.Add("DIFFFAT", OleDbType.Double, 8, "DIFFFAT");
                    cmd.Parameters.Add("DIFFSNF", OleDbType.Double, 8, "DIFFSNF");

                    // Initialize an OleDBDataAdapter object.
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Procurementimport", conn);

                    // Set the InsertCommand of OleDbDataAdapter, 
                    // which is used to insert data.
                    da.InsertCommand = cmd;

                    // Changes the Rowstate()of each DataRow to Added,
                    // so that OleDbDataAdapter will insert the rows.
                    foreach (DataRow dr in dtSQL.Rows)
                    {
                        dr.SetAdded();
                    }

                    // Insert the data into the Excel spreadsheet.
                    da.Update(dtSQL);
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }

    #endregion Backup

    //Region Delete
    #region Delete
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            Datefunc();
            RpyGroupBoxChange();
            CheckBackupFile();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }

    }

    private void CheckBackupFile()
    {
        try
        {
            
            int Deleteflag = 0;
            string str = string.Empty;
            string strDeleteFileName = string.Empty;
            string Deletefilename = string.Empty;
            Deletefilename = ddl_PlantName.SelectedItem.Value.Trim() + '_' + txt_FromDate.Text.Trim().Replace('/', '.') + '_' + txt_ToDate.Text.Trim().Replace('/', '.') + ".xlsx"; ;
          //  strDeleteFileName = "~/EXData/" + Deletefilename + ".xlsx";
                     

            DirectoryInfo Dirinfo = new DirectoryInfo(string.Concat(Server.MapPath(Compath.Trim())));
            FileInfo[] FilInfo = Dirinfo.GetFiles("*.xlsx");//xlsx or xls 
            if (FilInfo != null)
            {
                ddl_TableName.Items.Clear();
                foreach (FileInfo fi in FilInfo)
                {
                    ddl_TableName.Items.Add(fi.ToString().Trim());
                    if (fi.ToString().Trim() == Deletefilename.Trim())
                    {
                        Deleteflag = 1;
                    }
                }
            }

            if (Deleteflag == 1)
            {

                if (ExportType == "Procurement")
                {
                    str = "Delete From Procurement Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND  Prdate between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' ";                 
                }
                else if (ExportType == "ProcurementImport")
                {
                    str = "Delete From Procurementimport Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND  Prdate between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' ";                 
                }
                dbaccess.ExecuteNonquorey(str);
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Success, Data Deleted...";
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please,Take a BackUp...";
            }

            
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }

    #endregion Delete

    //Region Import
    #region Import
    protected void btn_Import_Click(object sender, EventArgs e)
    {
        try
        {
            Datefunc();
            RpyGroupBoxChange();
            AlreadyDataAvailCheck();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
        
    }

    private void AlreadyDataAvailCheck()
    {
        try
        {           
            string str = string.Empty;
            int Flag = 0;
            if (ExportType == "Procurement")
            {
                str = "SELECT COUNT(*) AS Counts  FROM Procurement Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate Between '" + d1.Trim() + "' AND '" + d2.Trim() + "' ";
            }
            else if (ExportType == "ProcurementImport")
            {
                str = "SELECT COUNT(*) AS Counts  FROM Procurementimport Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate Between '" + d1.Trim() + "' AND '" + d2.Trim() + "' ";
            }

            Flag = dbaccess.ExecuteScalarint(str);

            if (Flag > 0)
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Check Data Already Avail on DataBase...";
            }
            else
            {
                SqlDBImportBackupFile();
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }

    private void SqlDBImportBackupFile()
    {
        try
        {
            int cou = 0;
            int Importflag = 0;
            string str = string.Empty;
            string path = string.Empty;
            string strImportFileName = string.Empty;
            string Importfilename = string.Empty;
            Importfilename = ddl_PlantName.SelectedItem.Value.Trim() + '_' + txt_FromDate.Text.Trim().Replace('/', '.') + '_' + txt_ToDate.Text.Trim().Replace('/', '.') + ".xlsx"; ;
            //  strDeleteFileName = "~/EXData/" + Deletefilename + ".xlsx";

            DirectoryInfo Dirinfo = new DirectoryInfo(string.Concat(Server.MapPath(Compath.Trim())));
            FileInfo[] FilInfo = Dirinfo.GetFiles("*.xlsx");//xlsx or xls 
            if (FilInfo != null)
            {
                ddl_TableName.Items.Clear();
                foreach (FileInfo fi in FilInfo)
                {
                    ddl_TableName.Items.Add(fi.ToString().Trim());
                    if (fi.ToString().Trim() == Importfilename.Trim())
                    {
                        Importflag = 1;
                        path = string.Concat(Server.MapPath(Compath.Trim() + Importfilename.Trim()));
                    }
                }
            }

            if (Importflag == 1)
            {
                if (ExportType == "Procurement")
                {

                    //string Excelconnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    string Excelconnection = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;'", path);


                    olecon.ConnectionString = Excelconnection;

                    OleDbCommand Excmd = new OleDbCommand("select * from Procurement", olecon);
                    olecon.Open();

                    DbDataReader dr = Excmd.ExecuteReader();

                    DataTable dt1 = new DataTable();
                    dt1.Load(dr);
                    cou = dt1.Rows.Count;
                    dr.Close();


                    DbDataReader dr1 = Excmd.ExecuteReader();

                    //string sqlConnectionStringw = @"Data Source=onlinemilktest.in;Initial Catalog=AMPS;user id=bala;password=10solution";
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    // string sqlConnectionStringw = @"Data Source=223.196.32.28;Initial Catalog=AMPS;user id=sa;password=vyshnavi123";
                    SqlBulkCopy bc = new SqlBulkCopy(connStr);

                    //DELETE Procurementimport DATA
                    // string str1 = "SELECT * FROM Procurementimport";
                    str = "DELETE FROM Procurement WHERE Company_Code='" + ccode + "' AND Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'";
                    SqlConnection con = new SqlConnection(connStr);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = str;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //DELETE Procurementimport DATA

                    bc.DestinationTableName = "Procurement";
                    bc.WriteToServer(dr1);
                    dr1.Close();
                    dt1.Dispose();
                    con.Close();
                    olecon.Close();
                    // File.Delete(path);
                }
                else if (ExportType == "ProcurementImport")
                {
                    //string Excelconnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    string Excelconnection = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;'", path);


                    olecon.ConnectionString = Excelconnection;

                    OleDbCommand Excmd = new OleDbCommand("select * from Procurementimport", olecon);
                    olecon.Open();

                    DbDataReader dr = Excmd.ExecuteReader();

                    DataTable dt1 = new DataTable();
                    dt1.Load(dr);
                    cou = dt1.Rows.Count;
                    dr.Close();


                    DbDataReader dr1 = Excmd.ExecuteReader();

                    //string sqlConnectionStringw = @"Data Source=onlinemilktest.in;Initial Catalog=AMPS;user id=bala;password=10solution";
                    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    // string sqlConnectionStringw = @"Data Source=223.196.32.28;Initial Catalog=AMPS;user id=sa;password=vyshnavi123";
                    SqlBulkCopy bc = new SqlBulkCopy(connStr);

                    //DELETE Procurementimport DATA
                    // string str1 = "SELECT * FROM Procurementimport";
                    str = "DELETE FROM Procurementimport WHERE Company_Code='" + ccode + "' AND Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND Prdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'";
                    SqlConnection con = new SqlConnection(connStr);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = str;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //DELETE Procurementimport DATA

                    bc.DestinationTableName = "Procurementimport";
                    bc.WriteToServer(dr1);
                    dr1.Close();
                    dt1.Dispose();
                    con.Close();
                    olecon.Close();
                    // File.Delete(path);

                }
               
               
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Success, Plant:" + ddl_PlantName.SelectedItem.Value + "Procurement Data---  " + cou + "  ---Rows Inserted";
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }

    #endregion Import


}