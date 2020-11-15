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
//using System.Activities.Statements;
using System.Workflow.Activities;
using System.Data.Odbc;
using System.Text;



public partial class PlantwiseProcurementDataExport : System.Web.UI.Page
{
    //1_11269930805
    
    public string ccode;
    public string pcode;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
               
               

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();              

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }

        
    }

    
  
  
    protected DataTable RetrieveData1()
    {
        DataTable dt = new DataTable();        
        SqlDataAdapter da=new SqlDataAdapter();

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
        
        {
            if (drop_table.SelectedIndex == 0)
            {
                da = new SqlDataAdapter("SELECT [Agent_Id], [Agent_Name], [Type], [Cartage_Amt], [Company_code], [Plant_code], [Route_id], [AddedDate], [Agent_AccountNo], [Payment_mode], [Bank_Id] FROM [Agent_Master] Where AddedDate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "'", conn);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }

            else if (drop_table.SelectedIndex == 1)
            {
                da = new SqlDataAdapter("SELECT [Route_ID], [Route_Name], [Tot_Distance], [Added_Date], [Status], [Plant_Code], [Company_Code] FROM [Route_Master] Where Added_Date between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "'", conn);
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
            }
            else if (drop_table.SelectedIndex == 2)
            {
                da = new SqlDataAdapter("SELECT * FROM (SELECT DISTINCT(Agent_id) AS Agent_id,Milk_kg,(Milk_kg-Milk_kg) AS DMilk_kg FROM [Procurementimport] Where Plant_Code='161' AND Prdate='12-01-2015' AND Sessions='AM') AS t1 ORDER BY RAND(t1.Agent_id)" , conn);
                //SELECT * FROM(SELECT DISTINCT(Agent_id) AS Agent_id,Milk_kg,(Milk_kg-Milk_kg) AS DMilk_kg FROM [Procurementimport] Where Plant_Code='155' AND Prdate='" + txt_FromDate.Text + "' AND Sessions='AM' ORDER BY RAND(Agent_id)
                // Fill the DataTable with data from SQL Server table.
                da.Fill(dt);
               
            }
            else
            {
            }



        }

        return dt;
    }

        // Export data to an Excel spreadsheet via ADO.NET
        protected void ExportToExcel(string strConn, System.Data.DataTable dtSQL)
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
               
                 if (drop_table.SelectedIndex == 0)
                {
                    // Create a new sheet in the Excel spreadsheet.
                    OleDbCommand cmd = new OleDbCommand("create table Agent_Master(Agent_Id Integer,Agent_Name Varchar(50),Type Integer,Cartage_Amt Double,Company_code Integer,Plant_code Integer,Route_id Integer,Bank_Id Varchar(30),Payment_mode Varchar(20),Agent_AccountNo Varchar(30),AddedDate Date)", conn);

                    // Open the connection.
                    conn.Open();

                    // Execute the OleDbCommand.
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Agent_Master (Agent_Id,Agent_Name,Type,Cartage_Amt,Company_code,Plant_code,Route_id,Bank_Id,Payment_mode,Agent_AccountNo,AddedDate) values (?,?,?,?,?,?,?,?,?,?,?)";

                    // Add the parameters.
                    // cmd.Parameters.Add("Tid", OleDbType.Integer);

                    cmd.Parameters.Add("Agent_Id", OleDbType.Integer, 4, "Agent_Id");
                    cmd.Parameters.Add("Agent_Name", OleDbType.VarChar, 50, "Agent_Name");
                    cmd.Parameters.Add("Type", OleDbType.Integer, 4, "Type");
                    cmd.Parameters.Add("Cartage_Amt", OleDbType.Double, 8, "Cartage_Amt");
                    cmd.Parameters.Add("Company_code", OleDbType.Integer, 4, "Company_code");
                    cmd.Parameters.Add("Plant_code", OleDbType.Integer, 4, "Plant_code");
                    cmd.Parameters.Add("Route_id", OleDbType.Integer, 4, "Route_id");
                    cmd.Parameters.Add("Bank_Id", OleDbType.VarChar, 30, "Bank_Id");
                    cmd.Parameters.Add("Payment_mode", OleDbType.VarChar, 20, "Payment_mode");
                    cmd.Parameters.Add("Agent_AccountNo", OleDbType.VarChar, 30, "Agent_AccountNo");
                    cmd.Parameters.Add("AddedDate", OleDbType.Date, 8, "AddedDate");
                    

                    // Initialize an OleDBDataAdapter object.
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Agent_Master", conn);

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

                 else if (drop_table.SelectedIndex == 1)
                 {
                     // Create a new sheet in the Excel spreadsheet.
                     OleDbCommand cmd = new OleDbCommand("create table Route_Master(Route_ID Integer,Route_Name Varchar(50),Tot_Distance Integer,Added_Date Date,Status Integer,Plant_Code Integer,Company_Code Integer)", conn);

                     // Open the connection.
                     conn.Open();

                     // Execute the OleDbCommand.
                     cmd.ExecuteNonQuery();

                     cmd.CommandText = "INSERT INTO Route_Master (Route_ID,Route_Name,Tot_Distance,Added_Date,Status,Plant_Code,Company_Code) values (?,?,?,?,?,?,?)";

                     // Add the parameters.
                     // cmd.Parameters.Add("Tid", OleDbType.Integer);

                     cmd.Parameters.Add("Route_ID", OleDbType.Integer, 4, "Route_ID");
                     cmd.Parameters.Add("Route_Name", OleDbType.VarChar, 50, "Route_Name");
                     cmd.Parameters.Add("Tot_Distance", OleDbType.Integer, 4, "Tot_Distance");
                     cmd.Parameters.Add("Added_Date", OleDbType.Date, 8, "Added_Date");
                     cmd.Parameters.Add("Status", OleDbType.Integer, 1, "Status");
                     cmd.Parameters.Add("Plant_Code", OleDbType.Integer, 4, "Plant_Code");
                     cmd.Parameters.Add("Company_Code", OleDbType.Integer, 4, "Company_Code");


                     // Initialize an OleDBDataAdapter object.
                     OleDbDataAdapter da = new OleDbDataAdapter("select * from Route_Master", conn);

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
                 else if (drop_table.SelectedIndex == 2)
                 {
                     // Create a new sheet in the Excel spreadsheet.
                     OleDbCommand cmd = new OleDbCommand("create table Procurementimport(Agent_id Varchar(50),Milk_kg Double,DMilk_kg Double)", conn);

                     // Open the connection.
                     conn.Open();

                     // Execute the OleDbCommand.
                     cmd.ExecuteNonQuery();

                     cmd.CommandText = "INSERT INTO Procurementimport (Agent_id,Milk_kg,DMilk_kg) values (?,?,?)";

                     // Add the parameters.
                     // cmd.Parameters.Add("Tid", OleDbType.Integer);


                     cmd.Parameters.Add("Agent_id", OleDbType.VarChar, 50, "Agent_id");
                     cmd.Parameters.Add("Milk_kg", OleDbType.Double, 8, "Milk_kg");
                     cmd.Parameters.Add("DMilk_kg", OleDbType.Double, 8, "DMilk_kg");


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
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strDownloadFileName = "";
        string strExcelConn = "";

        if (rblExtension.SelectedValue == "2003")
        {
            // Excel 97-2003
          //  strDownloadFileName = "~/DownloadFiles/" + drop_table.Text + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            strDownloadFileName = "~/DownloadFiles/" + drop_table.Text + DateTime.Now.ToString("ddMMyyyy") + "_" + 161 + ".xls";
            strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(strDownloadFileName) + ";Extended Properties='Excel 8.0;HDR=Yes'";
        }
        else
        {
            // Excel 2007
            strDownloadFileName = "~/DownloadFiles/" + drop_table.Text + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strDownloadFileName) + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'";
        }

        // Retrieve data from SQL Server table.
        DataTable dtSQL = RetrieveData1();

        // Export data to an Excel spreadsheet.
        ExportToExcel(strExcelConn, dtSQL);

        if (rblDownload.SelectedValue == "Yes")
        {
            hlDownload.Visible = true;

            // Display the download link.
            hlDownload.Text = "Click here to download file.";
            hlDownload.NavigateUrl = strDownloadFileName;
        }
        else
        {
            // Hide the download link.
            hlDownload.Visible = false;
        }
    }
   
    
}
