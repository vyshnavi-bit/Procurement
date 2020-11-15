using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
public partial class DpuDataUploads : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DateTime d1, d2;
    string frmdate, Todate;
    DataSet DTG = new DataSet();
    DateTime dtm = new DateTime();
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    string month;
    DataTable grid = new System.Data.DataTable();
    DataTable checkavaildata = new System.Data.DataTable();
    string strsql2 = string.Empty;
    string DPU = ConfigurationManager.ConnectionStrings["AMPSConnectionStringDpu"].ConnectionString;
    string completeagent;
    string chechdate;
    string sessions;
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
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    
                }
                else
                {
                    pname = ddl_Plantname.SelectedItem.Text;
                }
            }
            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

               
            }
        }
        catch
        {


        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        //String strConnection = "ConnectionString";
        string connectionString = "";
        if (FileUpload1.HasFile)
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string fileLocation = Server.MapPath("~/Files/" + fileName);
            FileUpload1.SaveAs(fileLocation);
            if ((fileExtension == ".xls") || (fileExtension == ".XLS"))
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=no;IMEX=2\"";
            }
            else if ((fileExtension == ".xlsx") ||  (fileExtension == ".XLSX"))
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +  fileLocation  + ";Extended Properties=\"Excel 12.0;HDR=no;IMEX=2\"";
            }
            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
            DataTable dtExcelRecords = new DataTable();
            con.Open();
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
            dAdapter.SelectCommand = cmd;
            dAdapter.Fill(dtExcelRecords);
            grid.Columns.Add("Date");
            grid.Columns.Add("Shift");
            grid.Columns.Add("Agent_id");
            grid.Columns.Add("producer");
            grid.Columns.Add("Milkkg");
            grid.Columns.Add("Fat");
            grid.Columns.Add("Snf");
            grid.Columns.Add("Clr");
            grid.Columns.Add("Milk Nature");
            foreach (DataRow dps in dtExcelRecords.Rows)
            {
             
            
                    string[] Date = dps[0].ToString().Split('.');
                    if(Date[0]!= "Date")
                    {

                    string finaldate = Date[0] + "/" + Date[1] + "/" + Date[2];
                    string shift = dps[1].ToString();
                    if (shift == "M")
                    {
                        shift = "AM";
                    }
                    if (shift == "E")
                    {
                        shift = "PM";
                    }
                    string producercode = dps[2].ToString();
                    if (producercode.Length == 1)
                    {
                        completeagent = ddl_agentid.SelectedItem.Text + "00" + producercode;

                    }
                    if (producercode.Length == 2)
                    {
                        completeagent = ddl_agentid.Text + "0" + producercode;

                    }
                    string qty = dps[3].ToString();
                    string fat = dps[4].ToString();
                    string snf = dps[5].ToString();
                    string clr = dps[6].ToString();
                    string type = dps[7].ToString();
                    grid.Rows.Add(finaldate, shift, ddl_agentid.Text, completeagent, qty, fat, snf, clr, type);
                }

            }
            GridView1.DataSource = grid;
            GridView1.DataBind();

        }
    }
    public void getinsert()
    {


        foreach(GridViewRow readrow in GridView1.Rows)
        {

            string[] gdate = readrow.Cells[0].Text.Split('/');
            string finaldate = gdate[1] + "/" + gdate[0] + "/" + gdate[2];
            string shift = readrow.Cells[1].Text;
            string canno = readrow.Cells[2].Text;
            string producer = readrow.Cells[3].Text;
            string qty = readrow.Cells[4].Text;
            string fat = readrow.Cells[5].Text;
            string snf = readrow.Cells[6].Text;
            string clr = readrow.Cells[7].Text;
            string type = readrow.Cells[8].Text;


            SqlConnection con = new SqlConnection(DPU);

            if (con.State == ConnectionState.Closed)
            {
               
                con.Open();
            }
             
           string str = "Insert into    THIRUMALABILLSNEW(plant_code,prdate,shift,agent_code,producer_code,milk_kg,fat,snf,status) values('" + ddl_Plantname.SelectedItem.Value + "','" + finaldate + "','" + shift + "','" + canno + "','" + producer + "','" + qty + "','" + fat + "','" + snf + "','1') ";
           SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            string megg = "Record inserted";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(megg) + "')</script>");
        }
       

    }
    public void verifydata()
    {
        for (int i = 0; i < 1; i++)
        {

            string[] gdate = GridView1.Rows[0].Cells[0].Text.Split('/');
             chechdate = gdate[1] + "/" + gdate[0] + "/" + gdate[2];
             sessions = GridView1.Rows[0].Cells[1].Text;
        }
        con = DB.GetConnection();
        string getdata = "Select *   from  VMCCDPU where prdate='" + chechdate + "'  and shift='" + sessions + "' and plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and agent_code='"+ddl_agentid.SelectedItem.Text+"'  ";
        SqlCommand cmd = new SqlCommand(getdata, con);
        SqlDataAdapter dspp = new SqlDataAdapter(cmd);
        checkavaildata.Rows.Clear();
        dspp.Fill(checkavaildata);
        
      

    }
    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }

    public void Agentid()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT agent_id  FROM agent_master  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and DpuAgentStatus='1' group by agent_id  order by agent_id asc ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable dts = new DataTable();
            dts.Rows.Clear();
            DA.Fill(dts);
            ddl_agentid.DataSource = dts;
            ddl_agentid.DataTextField = "agent_id";
            ddl_agentid.DataValueField = "agent_id";
            ddl_agentid.DataBind();
        }
        catch
        {

        }
    }


    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        verifydata();
        if (checkavaildata.Rows.Count > 0)
        {
           
        }
        else
        {
            getinsert();

        }

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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        pcode = ddl_Plantname.SelectedItem.Value;
        Agentid();
    }
}