using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;


public partial class LocalprocurementDataExportaspx : System.Web.UI.Page
{
    DbHelper dbacess = new DbHelper();
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
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime frmdate = Convert.ToDateTime(txt_FromDate.Text);
            string fdate = frmdate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
            DateTime todate = Convert.ToDateTime(txt_ToDate.Text);
            string tdate = todate.ToString("dd" + "_" + "MM" + "_" + "yyyy");

            string EXlFilename = ccode + "_" + pcode + "_" + fdate + "_" + tdate + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xls";
            string locaexccelfilepath = "E:/Procurementreport/" + EXlFilename;
            // string locaexccelfilepath = Server.MapPath("~/Procurementreport/" + EXlFilename);

            if (File.Exists(locaexccelfilepath))
            {
                File.Delete(locaexccelfilepath);
            }
          //  string Excelconnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + locaexccelfilepath + ";Extended Properties='Excel 8.0;HDR=Yes'");
            string Excelconnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + locaexccelfilepath + ";Extended Properties='Excel 8.0'");

            DataTable dt = new DataTable();
            dt = procurementdata();
            ExportToExcel(Excelconnection, dt);


        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }
    private void ExportToExcel(string Excelconnection1, System.Data.DataTable dt1)
    {
        try
        {
            OleDbConnection olcon = new OleDbConnection(Excelconnection1);
            OleDbCommand olcmd = new OleDbCommand("CREATE TABLE Impprocurement(Tid Integer,Agent_id Varchar(50),Prdate Date,Sessions Varchar(10),Milk_ltr Double,Fat Double,Snf Double,Plant_Code Varchar(50),Route_id Varchar(50),NoofCans Integer,Milk_kg Double,Clr Double,Company_Code Varchar(50),Milk_Nature Integer,SampleId Integer,Sampleno Varchar(30),milk_tip_st_time Date,milk_tip_end_time Date,usr_weigher Varchar(50),usr_analizer varchar(50),fat_kg Double,snf_kg Double,Truck_id Integer,Status integer)", olcon);
            olcon.Open();
            olcmd.ExecuteNonQuery();

            olcmd.CommandText = "INSERT INTO Impprocurement(Tid,Agent_id,Prdate,Sessions,Milk_ltr,Fat,Snf,Plant_Code,Route_id,NoofCans,Milk_kg,Clr,Company_Code,Milk_Nature,SampleId,Sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,Truck_id,Status) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

            olcmd.Parameters.Add("Tid", OleDbType.Integer, 4, "Tid");
            olcmd.Parameters.Add("Agent_id", OleDbType.VarChar, 50, "Agent_id");
            olcmd.Parameters.Add("Prdate", OleDbType.Date, 8, "Prdate");
            olcmd.Parameters.Add("Sessions", OleDbType.VarChar, 10, "Sessions");
            olcmd.Parameters.Add("Milk_ltr", OleDbType.Double, 8, "Milk_ltr");
            olcmd.Parameters.Add("Fat", OleDbType.Double, 8, "Fat");
            olcmd.Parameters.Add("Snf", OleDbType.Double, 8, "Snf");
            olcmd.Parameters.Add("Plant_Code", OleDbType.VarChar, 50, "Plant_Code");
            olcmd.Parameters.Add("Route_id", OleDbType.VarChar, 50, "Route_id");
            olcmd.Parameters.Add("NoofCans", OleDbType.Integer, 4, "NoofCans");
            olcmd.Parameters.Add("Milk_kg", OleDbType.Double, 8, "Milk_kg");
            olcmd.Parameters.Add("Clr", OleDbType.Double, 8, "Clr");
            olcmd.Parameters.Add("Company_Code", OleDbType.VarChar, 50, "Company_Code");
            olcmd.Parameters.Add("Milk_Nature", OleDbType.Integer, 4, "Milk_Nature");
            olcmd.Parameters.Add("SampleId", OleDbType.Integer, 4, "SampleId");
            olcmd.Parameters.Add("Sampleno", OleDbType.VarChar, 30, "Sampleno");
            olcmd.Parameters.Add("milk_tip_st_time", OleDbType.Date, 8, "milk_tip_st_time");
            olcmd.Parameters.Add("milk_tip_end_time", OleDbType.Date, 8, "milk_tip_end_time");
            olcmd.Parameters.Add("usr_weigher", OleDbType.VarChar, 50, "usr_weigher");
            olcmd.Parameters.Add("usr_analizer", OleDbType.VarChar, 50, "usr_analizer");
            olcmd.Parameters.Add("fat_kg", OleDbType.Double, 8, "fat_kg");
            olcmd.Parameters.Add("snf_kg", OleDbType.Double, 8, "snf_kg");
            olcmd.Parameters.Add("Truck_id", OleDbType.Integer, 4, "Truck_id");
            olcmd.Parameters.Add("Status", OleDbType.Integer, 4, "Status");

            OleDbDataAdapter olda = new OleDbDataAdapter("SELECT * FROM Impprocurement", olcon);

            olda.InsertCommand = olcmd;

            foreach (DataRow dr in dt1.Rows)
            {
                dr.SetAdded();
            }

            olda.Update(dt1);
            olcon.Close();
        }
        catch (Exception ex)
        {

        }

    }

    private DataTable procurementdata()
    {
        DataTable dt1 = new DataTable();
        SqlDataAdapter da;
        SqlConnection con = new SqlConnection();
        try
        {

            using (con = dbacess.GetConnection())
            {
                da = new SqlDataAdapter("SELECT Tid,Agent_id,Prdate,Sessions,Milk_ltr,Fat,Snf,Plant_Code,Route_id,NoofCans,Milk_kg,Clr,Company_Code,Milk_Nature,SampleId,Sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,Truck_id,Status  FROM PROCUREMENT WHERE PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND PLANT_CODE='" + pcode + "' AND Company_Code='" + ccode + "'", con);
                da.Fill(dt1);
            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
        return dt1;
    }

}