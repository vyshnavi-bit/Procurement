using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
 
 

public partial class DpuUploadExcel : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataTable dtt = new DataTable();
    DbHelper db=new DbHelper();
    SqlConnection con=new SqlConnection();
    msg getclass = new msg();
    int converAgentid;
    string Date;
    int  countRows;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridView();
        }
        catch
        {

        }
    }

    public void BindGridView()
    {

        try
        {
            string FilePath = ResolveUrl("~/FILES/"); // Give Upload File Path
            string filename = string.Empty;
            if (FileUploadToServer.HasFile) // Check FileControl has any file or not
            {
                try
                {
                    string[] allowdFile = { ".xls", ".xlsx" };
                    string FileExt = System.IO.Path.GetExtension(FileUploadToServer.PostedFile.FileName).ToLower();// get extensions
                    bool isValidFile = allowdFile.Contains(FileExt);

                    // check if file is valid or not
                    if (!isValidFile)
                    {

                    }
                    else
                    {
                        int FileSize = FileUploadToServer.PostedFile.ContentLength; // get filesize
                        if (FileSize <= 1048576) //1048576 byte = 1MB
                        {
                            filename = Path.GetFileName(Server.MapPath(FileUploadToServer.FileName));// get file name
                            FileUploadToServer.SaveAs(Server.MapPath(FilePath) + filename); // save file to uploads folder
                            string filePath = Server.MapPath(FilePath) + filename;
                            string conStr = "";
                            if (FileExt == ".xls") // check for excel file type
                            {
                                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            }
                            else if (FileExt == ".xlsx")
                            {
                                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            }

                            conStr = String.Format(conStr, filePath, "Yes");
                            OleDbConnection con = new OleDbConnection(conStr);
                            OleDbCommand ExcelCommand = new OleDbCommand();
                            ExcelCommand.Connection = con;
                            con.Open();
                            DataTable ExcelDataSet = new DataTable();
                            ExcelDataSet = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                         
                            if (ExcelDataSet != null && ExcelDataSet.Rows.Count > 0)
                            {
                                string SheetName = ExcelDataSet.Rows[0]["TABLE_NAME"].ToString(); // get sheetname
                                ExcelCommand.CommandText = "SELECT * From [" + SheetName + "]";
                                OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                                ExcelAdapter.SelectCommand = ExcelCommand;
                                ExcelAdapter.Fill(dt);
                            }
                            con.Close();
                            if (dt != null && dt.Rows.Count > 0) // Check if File is Blank or not
                            {
                                GridView1.DataSource = dt;
                                GridView1.DataBind();

                                ViewState["sdtt"] = dt;

                            }
                            else
                            {


                            }
                            FilePath = ResolveUrl("~/FILES/");
                            string fileName = Server.MapPath(FilePath) + filename;
                            FileInfo f = new FileInfo(fileName);
                            if (f.Exists)
                            {
                                f.IsReadOnly = false;
                                f.Delete();
                            }
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }
        catch
        {


        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {



        DataTable dtt = (DataTable)ViewState["sdtt"];
      
        for(int I=0; I <  1 ; I++)
        {


            string GETDATE = dtt.Rows[0][0].ToString();
            string TEMPDate = GETDATE;
            string[] PDate = TEMPDate.Split(' ');
            string CONVERPDate = PDate[0].ToString();
            string[] splitdate = CONVERPDate.Split('-');
            string syear = splitdate[0];
            string sDate = splitdate[1];
            string smonth = splitdate[2];
            Date = sDate + "/" + syear + "/" + smonth;

        }

        string CHEDATA = "";
        con = db.GetConnection();
        CHEDATA = "sELECT *   FROM DpuEkoData WHERE dATE='" + Date + "'";
        SqlCommand CMD = new SqlCommand(CHEDATA, con);
        SqlDataReader dr = CMD.ExecuteReader();

        if (dr.HasRows)
        {
            countRows = 1;
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }
        else
        {
            
             bulkinsert();

        }

         
       

    }


    public void bulkinsert()
    {
        DataTable dtt = (DataTable)ViewState["sdtt"];
        foreach (DataRow DRD in dtt.Rows)
        {

           string TEMPDate= DRD[0].ToString();
           string[] PDate = TEMPDate.Split(' ');
           string CONVERPDate = PDate[0].ToString();
           string[] splitdate = CONVERPDate.Split('-');
           string syear = splitdate[0];
           string sDate = splitdate[1];
           string smonth = splitdate[2];
           string Date = sDate + "/" + syear + "/" + smonth;
           string tempRoute  = DRD[1].ToString();
           string[] Route    = tempRoute.Split('-');
           int conroute      = Convert.ToInt16(Route[0]);
           string tempAgentid    = DRD[2].ToString();
           string[]  Agentid    =  tempAgentid.Split('-');
           string countAgentid      = Agentid[0];
           if (countAgentid.Length ==8)
           {
           string tempconverAgentid = countAgentid.Substring(0, 5);
           converAgentid     =Convert.ToInt16(tempconverAgentid);
           }
           string tempFarmerNum  = DRD[3].ToString();
           string[]  FarmerNum   = tempFarmerNum.Split('-');
           int conFarmerNum = Convert.ToInt16(FarmerNum[0]);
           string Shift          = DRD[4].ToString();
           if(Shift =="M")
           {
               Shift ="AM";
           }
           else
           {
                Shift ="PM";

           }
           string Time           = DRD[5].ToString();
           string QuanMode       = DRD[6].ToString();
           string MilkType       = DRD[8].ToString();
           double Milk_kg        = Convert.ToDouble(DRD[10]);
           double Fat            = Convert.ToDouble(DRD[11]);
           double SNF            = Convert.ToDouble(DRD[13]);
           double KgFat          = Convert.ToDouble(DRD[12]);
           double KgSNF          = Convert.ToDouble(DRD[14]);
           string UserName       = Session["Name"].ToString();
           int Status = 1;
            string STR;
            STR = "Insert Into  DpuEkoData(Pcode,Route_id,Agent_id,ProducerId,Shift,EntryMode,MilkType,Milk_kg,Fat,Snf,FatKg,SnfKg,Date,Time,UserName,Status)Values('164','" + conroute + "','" + converAgentid + "','" + conFarmerNum + "','" + Shift + "','" + QuanMode + "','" + MilkType + "','" + Milk_kg + "','" + Fat + "','" + SNF + "','" + KgFat + "','" + KgSNF + "','" + Date + "','" + Time + "','" + UserName + "','" + Status + "')";
            con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            cmd.ExecuteNonQuery();

       }
        
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

    }
}