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



public partial class PlantwiseProcurementDataImport : System.Web.UI.Page
{
    //1_11269930805

    public string ccode;
    public string pcode;
    public string rtbText;
    public string Weightdata;
    public int splitflag = 0;
    string centreid;
    int countuser;
    public static string txtboxagent_id, txtboxagent_name, txtboxweightdata1, txtboxweightdata2;
    //   double rate = 0.0, fat = 0.0, snf = 0.0, amount = 0.0;
    string agentid, sampleno, promilktype1;
    SqlDataReader dr;
    double clr, qty, rcamnt, comrate, comamnt, bonus, bonamnt, comper, drate, amount, prod_comm, tablemilkkg;
    double rate, snf, fat;
    double milkkg, milkltr;
    //   string sno = "0";
    public static string flag;
    public string portname;
    public string baudrate;
    string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand sqlcmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();
    BOLProcurement procurementBO = new BOLProcurement();
    BLLProcurement procurementBL = new BLLProcurement();
    DALProcurement procurementDA = new DALProcurement();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    DbHelper DBclass = new DbHelper();
    BLLPortsetting portBL = new BLLPortsetting();
    BLLRateChart chartmaster = new BLLRateChart();
    BLLroutmaster routeBL = new BLLroutmaster();
    protected System.Timers.Timer tmr_analizer;
    public bool IsPageRefresh;
    string prochartname = string.Empty;
    public int RateChartmode;
    public string companycode;
    public string plantcode;

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



    // Import the data from DataTable to SQL Server via SqlBulkCopy
    protected void SqlBulkCopyImport(DataTable dtExcel)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString1"].ToString()))
        {
            // Open the connection.
            conn.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
            {
                // Specify the destination table name.
                bulkCopy.DestinationTableName = "dbo.procurementimport";

                foreach (DataColumn dc in dtExcel.Columns)
                {

                    // Because the number of the test Excel columns is not 
                    // equal to the number of table columns, we need to map 
                    // columns.
                    bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);

                }

                // Write from the source to the destination.
                bulkCopy.WriteToServer(dtExcel);

            }
        }
    }
    public void savedatas()
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString1"].ToString()))
        {
            // Open the connection.
            conn.Open();
            SqlCommand command = new SqlCommand("ImportProcurement", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@frdate", SqlDbType.DateTime));

            command.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime));
            command.Parameters["@frdate"].Value = Convert.ToDateTime(txt_FromDate.Text);

            command.Parameters["@todate"].Value = Convert.ToDateTime(txt_ToDate.Text);
            int i = command.ExecuteNonQuery();


            SqlCommand cmd = new SqlCommand("UPDATE procurementimport SET Status=2 Where Prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' And Status=0", conn);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
    protected void btn_ImportExcelData_Click(object sender, EventArgs e)
    {
        try
        {
            // Before attempting to import the file, verify
            // that the FileUpload control contains a file.
            if (FileUpload1.HasFile)
            {
                // Get the name of the Excel spreadsheet to upload.
                string strFileName = Server.HtmlEncode(FileUpload1.FileName);

                // Get the extension of the Excel spreadsheet.
                string strExtension = Path.GetExtension(strFileName);

                // Validate the file extension.
                if (strExtension != ".xls" && strExtension != ".xlsx")
                {
                    Response.Write("<script>alert('Please select a Excel spreadsheet to import!');</script>");
                    return;
                }

                // Generate the file name to save.
                // string strUploadFileName = "~/UploadFiles/Procurement" + DateTime.Now.ToString("ddMMyyyy") + strExtension;
                string strUploadFileName = "~/UploadFiles/Procurementimport" + DateTime.Now.ToString("ddMMyyyy") + "_" + 164 + strExtension;


                // Save the Excel spreadsheet on server.
                FileUpload1.SaveAs(Server.MapPath(strUploadFileName));

                // Generate the connection string for Excel file.
                string strExcelConn = "";

                // There is no column name In a Excel spreadsheet. 
                // So we specify "HDR=YES" in the connection string to use 
                // the values in the first row as column names. 
                if (strExtension == ".xls")
                {
                    // Excel 97-2003
                    // strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 8.0;HDR=YES;'";
                    strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 8.0;'";
                }
                else
                {
                    // Excel 2007
                    // strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 12.0 Xml;'";
                }

                // DataTable dtExcel = RetrieveData(strExcelConn);
                DataTable dtExcel = RetrieveDataproimport(strExcelConn);

                //////////////////////////
                int count = dtExcel.Rows.Count;
                if (count > 0)
                {
                    DataTable custDT = new DataTable();
                    DataColumn col = null;
                    col = new DataColumn("Agent_id");
                    custDT.Columns.Add(col);
                    col = new DataColumn("Milk_kg");
                    custDT.Columns.Add(col);
                    col = new DataColumn("DMilk_kg");
                    custDT.Columns.Add(col);

                    for (int i = 0; i <= dtExcel.Rows.Count - 1; i++)
                    {
                        DataRow dr = null;
                        dr = custDT.NewRow();

                        dr[0] = dtExcel.Rows[i][0];
                        dr[1] = dtExcel.Rows[i][1];
                        dr[2] = dtExcel.Rows[i][2];
                        custDT.Rows.Add(dr);
                    }
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "CustDtl";
                    param.SqlDbType = SqlDbType.Structured;
                    param.Value = custDT;
                    param.Direction = ParameterDirection.Input;
                    String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    SqlConnection conn = null;
                    using (conn = new SqlConnection(dbConnStr))
                    {
                        SqlCommand sqlCmd = new SqlCommand("dbo.[Update_Donatemilk]");
                        conn.Open();
                        sqlCmd.Connection = conn;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(param);
                        sqlCmd.Parameters.AddWithValue("@spccode", 1);
                        sqlCmd.Parameters.AddWithValue("@sppcode", 164);
                        sqlCmd.Parameters.AddWithValue("@spdate", txt_FromDate.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@spsess", "AM");
                        sqlCmd.ExecuteNonQuery();
                        WebMsgBox.Show("Donated Milk Updated successfully...");

                    }
                }

                //////////////////////////
                //// Get the row counts before importing.
                //int iStartCount = GetRowCounts();

                //// Import the data.
                //SqlBulkCopyImport(dtExcel);
                //savedatas();
                //// Get the row counts after importing.
                //int iEndCount = GetRowCounts();

                //// Display the number of imported rows. 
                //lblMessages1.Text = Convert.ToString(iEndCount - iStartCount) + " rows were imported into Procurement table";

                //if (rblArchive.SelectedValue == "No")
                //{
                //    // Remove the uploaded Excel spreadsheet from server.
                //    File.Delete(Server.MapPath(strUploadFileName));
                //}
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.Message);
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
    }
    // Get the row counts in SQL Server table.
    protected int GetRowCounts()
    {
        int iRowCount = 0;

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
        {
            SqlCommand cmd = new SqlCommand("select count(*) from procurementimport", conn);
            conn.Open();

            // Execute the SqlCommand and get the row counts.
            iRowCount = (int)cmd.ExecuteScalar();
        }

        return iRowCount;
    }

    // Retrieve data from the Excel spreadsheet.
    protected DataTable RetrieveData(string strConn)
    {
        DataTable dtExcel = new DataTable();

        using (OleDbConnection conn = new OleDbConnection(strConn))
        {

            // Initialize an OleDbDataAdapter object.
            OleDbDataAdapter da = new OleDbDataAdapter("select * from PROCUREMENTIMPORT Where Prdate between #" + txt_FromDate.Text + "# and #" + txt_ToDate.Text + "# And Status=0", conn);

            // Fill the DataTable with data from the Excel spreadsheet.
            da.Fill(dtExcel);
            conn.Open();



            String MyQuery = "UPDATE procurementimport SET Status=1 Where Prdate between ? and ?";

            OleDbCommand MyUpdate = new OleDbCommand(MyQuery, conn);

            // Now, add the parameters in the same order as the "place-holders" are in above command
            OleDbParameter NewParm = new OleDbParameter("prdate", txt_FromDate.Text);
            NewParm.DbType = DbType.DateTime;
            // (or other data type, such as DbType.String, DbType.DateTime, etc)
            MyUpdate.Parameters.Add(NewParm);

            // Now, on to the next set of parameters...
            NewParm = new OleDbParameter("prdate", txt_ToDate.Text);
            NewParm.DbType = DbType.String;
            MyUpdate.Parameters.Add(NewParm);





            // Now, you can do you 
            MyUpdate.ExecuteNonQuery();
            conn.Close();
        }

        return dtExcel;
    }


    // Retrieve data from the Excel spreadsheet.
    protected DataTable RetrieveDataproimport(string strConn)
    {
        DataTable dtExcel = new DataTable();

        using (OleDbConnection conn = new OleDbConnection(strConn))
        {

            // Initialize an OleDbDataAdapter object.
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Procurementimport", conn);

            // Fill the DataTable with data from the Excel spreadsheet.
            da.Fill(dtExcel);
        }

        return dtExcel;
    }





    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Updatedonatemilktopaymentdata_Click(object sender, EventArgs e)
    {
        try
        {
            // Retrieve data from SQL Server table.
            DataTable dtSQL = RetrieveData1();
            //////////////////////////
            int count = dtSQL.Rows.Count;
            if (count > 0)
            {
                DataTable custDT = new DataTable();
                DataColumn col = null;
                col = new DataColumn("Agent_id");
                custDT.Columns.Add(col);
                col = new DataColumn("DMilk_kg");
                custDT.Columns.Add(col);

                for (int i = 0; i <= dtSQL.Rows.Count - 1; i++)
                {
                    DataRow dr = null;
                    dr = custDT.NewRow();

                    dr[0] = dtSQL.Rows[i][0];
                    dr[1] = dtSQL.Rows[i][1];
                    custDT.Rows.Add(dr);
                }
                SqlParameter param = new SqlParameter();
                param.ParameterName = "CustDtl";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = custDT;
                param.Direction = ParameterDirection.Input;
                String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Update_DonatemilkInPaymentdata]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    sqlCmd.Parameters.AddWithValue("@spccode", 1);
                    sqlCmd.Parameters.AddWithValue("@sppcode", 164);
                    sqlCmd.Parameters.AddWithValue("@spdate", txt_FromDate.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    WebMsgBox.Show("Donated Milk Updated successfully in PaymentData...");

                }
            }

            //////////////////////////
        }
        catch (Exception ex)
        {

        }
    }

    protected DataTable RetrieveData1()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ToString()))
        {
            da = new SqlDataAdapter("SELECT AgentId,SUM(DonatedMilk) AS DonatedMilk FROM DonatedMilk Where PlantCode=164 AND DonateDate='12-08-2015'  GROUP BY AgentId", conn);

            // Fill the DataTable with data from SQL Server table.
            da.Fill(dt);
        }

        return dt;
    }


}



    
    

