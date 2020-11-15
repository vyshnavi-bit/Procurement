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



public partial class ServerlocalProImport : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string path;
    int ind = 0;
    OleDbConnection olecon = new OleDbConnection();
   

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();

                GetUploadExcelFileName();

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
    protected void btn_ImportExcelData_Click(object sender, EventArgs e)
    {
        try
        {
            string Lpcode = string.Empty;
            string Lcode = string.Empty;
            string Lfrmdate = string.Empty;
            string Ltodate = string.Empty;
            string LFileName = string.Empty;
            string[] LFileNamearr = new string[8];

            if (ddl_TableName.Items.Count > 0)
            {

                path = string.Concat(Server.MapPath("~/EXData/" + ddl_TableName.SelectedItem.Value));
                LFileName = ddl_TableName.SelectedItem.Value;
                LFileNamearr = LFileName.Split('_');
               
                Lcode = LFileNamearr[0].ToString();
                Lpcode = LFileNamearr[1].ToString();
                Lfrmdate = LFileNamearr[3].ToString() + "-" + LFileNamearr[2].ToString() + "-" + LFileNamearr[4].ToString();
                Ltodate = LFileNamearr[6].ToString() + "-" + LFileNamearr[5].ToString() + "-" + LFileNamearr[7].ToString();

                string Excelconnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0", path);

                olecon.ConnectionString = Excelconnection;

                OleDbCommand Excmd = new OleDbCommand("select * from [Procurementimport]", olecon);
                olecon.Open();

                DbDataReader dr = Excmd.ExecuteReader();

                DataTable dt1 = new DataTable();
                dt1.Load(dr);
                int cou = dt1.Rows.Count;
                dr.Close();


                DbDataReader dr1 = Excmd.ExecuteReader();

                //string sqlConnectionStringw = @"Data Source=onlinemilktest.in;Initial Catalog=AMPS;user id=bala;password=10solution";
                string sqlConnectionStringw = @"Data Source=kalidhass;Initial Catalog=AMPS;user id=sa;password=dvyshnavi123";
                SqlBulkCopy bc = new SqlBulkCopy(sqlConnectionStringw);                

                //DELETE Procurementimport DATA
                string str = "SELECT * FROM Procurementimport";
               // string str = "DELETE FROM Procurementimport WHERE Company_Code='" + Lcode + "' AND Plant_Code='" + Lpcode + "' AND Prdate BETWEEN '" + Lfrmdate + "' AND '" + Ltodate + "'";
                SqlConnection con = new SqlConnection(sqlConnectionStringw);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = str;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                //DELETE Procurementimport DATA

                // bc.DestinationTableName = "Impprocurement";
                bc.DestinationTableName = "Procurementimport";
                bc.WriteToServer(dr1);
                dr1.Close();
                dt1.Dispose();
                con.Close();
                // olecon.Close();
                // File.Delete(path);
                // GetUploadExcelFileName();
                //SP

                //SP

                WebMsgBox.Show("Plant:" + Lpcode + "Procurement Data---  " + cou + "  ---Rows Inserted");
            }
            else
            {
                WebMsgBox.Show("Upload a File");
            }

        }
        catch (Exception ex)
        {            
            ind = 1;
        }
        finally
        {
            //string dufilename = ddl_TableName.SelectedItem.Value;
            olecon.Close();           
            File.Delete(path);
            GetUploadExcelFileName();
          
            if (ind == 1)
            {
                TextBox1.Text = "File is Already Inserted";
            }
            else
            {
               // TextBox1.Text = "File is Inserted";
            }

        }
    }
    protected void btn_ExcelFileUpload_Click(object sender, EventArgs e)
    {
        string Filepath = string.Empty;
        try
        {

            if (Excel_FileUpload.FileName != "")
            {
                Filepath = string.Concat(Server.MapPath("~/EXData/" + Excel_FileUpload.FileName.Trim()));
                if (Filepath != null)
                {

                    if (File.Exists(Filepath))
                    {
                        WebMsgBox.Show("File Name already Exists");
                    }
                    else
                    {
                        Excel_FileUpload.SaveAs(Filepath);
                        GetUploadExcelFileName();

                    }
                }
            }
            else
            {
                WebMsgBox.Show("Choose the File to Upload");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void GetUploadExcelFileName()
    {
        try
        {


            DirectoryInfo Dirinfo = new DirectoryInfo(string.Concat(Server.MapPath("~/EXData/")));
            FileInfo[] FilInfo = Dirinfo.GetFiles("*.xls");
            if (FilInfo != null)
            {
                ddl_TableName.Items.Clear();
                foreach (FileInfo fi in FilInfo)
                {
                    ddl_TableName.Items.Add(fi.ToString());
                }
            }
            else
            {
                WebMsgBox.Show("Upload a Import File");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection conn = null;
        string connectionstrig = @"Data Source=onlinemilktest.in; Initial Catalog=AMPS;User Id=bala;Password=10solution";
        conn = new SqlConnection(connectionstrig);
        string username = "SELECT CURRENT_USER";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = username;
        conn.Open();
        object username1 = cmd.ExecuteScalar();
        conn.Close();

    }
}