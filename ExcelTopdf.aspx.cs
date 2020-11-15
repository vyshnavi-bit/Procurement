using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
public partial class ExcelTopdf : System.Web.UI.Page
{
   
    DataTable DSP = new DataTable();
    int ADDCOLU=0;
    DbHelper db = new DbHelper();
    SqlConnection con = new SqlConnection();
    msg getclass = new msg();
    int converAgentid;
    string Date;
    int countRows;
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;            
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
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
                       
                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        
                    }

                }
                else
                {



                }

                Button10.Visible = false;
            }


            else
            {
                ccode = Session["Company_code"].ToString();
                pcode = ddl_Plantname.SelectedItem.Value;
                ViewState["pcode"] = pcode.ToString();
            }
            Button10.Visible = false;
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
            datasetcount = datasetcount + 1;
        }
        catch
        {


        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

        try
        {
            DataTable dtt = (DataTable)ViewState["sdtt"];

            for (int I = 0; I < 1; I++)
            {
                string GETDATE = dtt.Rows[0][0].ToString();
                string TEMPDate = GETDATE;
                string[] PDate = TEMPDate.Split(' ');
                string CONVERPDate = PDate[0].ToString();
                string[] splitdate = CONVERPDate.Split('-');
                
                //string syear = splitdate[0];
                //string sDate = splitdate[1];
                //string smonth = splitdate[2];
                //Date = sDate + "/" + syear + "/" + smonth;

                string syear = splitdate[2];
                string sDate = splitdate[0];
                string smonth = splitdate[1];
                if (smonth == "Jan")
                {
                    smonth = "01";
                }
                if (smonth == "Feb")
                {
                    smonth = "02";
                }
                if (smonth == "Mar")
                {
                    smonth = "03";
                }
                if (smonth == "Apr")
                {
                    smonth = "04";
                }
                if (smonth == "May")
                {
                    smonth = "05";
                }
                if (smonth == "Jun")
                {
                    smonth = "06";
                }
                if (smonth == "Jul")
                {
                    smonth = "07";
                }
                if (smonth == "Aug")
                {
                    smonth = "08";
                }
                if (smonth == "Sep")
                {
                    smonth = "09";
                }
                if (smonth == "Oct")
                {
                    smonth = "10";
                }
                if (smonth == "Nov")
                {
                    smonth = "11";
                }
                if (smonth == "Dec")
                {
                    smonth = "12";
                }
              

              Date = smonth + "/" + sDate + "/" + syear;
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
                string message = getclass.save;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
            }
        }
        catch
        {


        }
    }
    public void bulkinsert()
    {
        try
        {
            DataTable dtt = (DataTable)ViewState["sdtt"];
            foreach (DataRow DRD in dtt.Rows)
            {
                string TEMPDate = DRD[0].ToString();
                string[] PDate = TEMPDate.Split(' ');
                string CONVERPDate = PDate[0].ToString();
                string[] splitdate = CONVERPDate.Split('-');
                string syear = splitdate[2];
                string sDate = splitdate[0];
                string smonth = splitdate[1];
                if (smonth == "Jan")
                {
                    smonth = "01";
                }
                if (smonth == "Feb")
                {
                    smonth = "02";
                }
                if (smonth == "Mar")
                {
                    smonth = "03";
                }
                if (smonth == "Apr")
                {
                    smonth = "04";
                }
                if (smonth == "May")
                {
                    smonth = "05";
                }
                if (smonth == "Jun")
                {
                    smonth = "06";
                }
                if (smonth == "Jul")
                {
                    smonth = "07";
                }
                if (smonth == "Aug")
                {
                    smonth = "08";
                }
                if (smonth == "Sep")
                {
                    smonth = "09";
                }
                if (smonth == "Oct")
                {
                    smonth = "10";
                }
                if (smonth == "Nov")
                {
                    smonth = "11";
                }
                if (smonth == "Dec")
                {
                    smonth = "12";
                }
                Date = smonth + "/" + sDate + "/" + syear;
                string tempRoute = DRD[1].ToString();
                string[] Route = tempRoute.Split('-');
                int conroute = Convert.ToInt16(Route[0]);
                string tempAgentid = DRD[2].ToString();
                string[] Agentid = tempAgentid.Split('-');
                string countAgentid = Agentid[0];
                if (countAgentid.Length == 8)
                {
                                      

                    string tempconverAgentid;
                    tempconverAgentid = countAgentid.Substring(countAgentid.Length - 5);
                    converAgentid = Convert.ToInt32(tempconverAgentid);
                }
                string tempFarmerNum = DRD[3].ToString();
                string[] FarmerNum = tempFarmerNum.Split('-');
                long  conFarmerNum = Convert.ToInt64(FarmerNum[0]);
                string FINALFORMAR = converAgentid + conFarmerNum.ToString();
                conFarmerNum = Convert.ToInt64(FINALFORMAR);
                string Shift = DRD[4].ToString();
                if (Shift == "M")
                {
                    Shift = "AM";
                }
                else
                {
                    Shift = "PM";

                }
                string Time = DRD[5].ToString();
                string QuanMode = DRD[6].ToString();
                string MilkType = DRD[8].ToString();
                double Milk_kg = Convert.ToDouble(DRD[9]);
                double Fat = Convert.ToDouble(DRD[10]);
                double SNF = Convert.ToDouble(DRD[12]);
                double KgFat = Convert.ToDouble(DRD[11]);
                double KgSNF = Convert.ToDouble(DRD[13]);
                string UserName = Session["Name"].ToString();
                int Status = 1;
                string STR;
                string STR1;
                STR = "Insert Into  DpuEkoData(Pcode,Route_id,Agent_id,ProducerId,Shift,EntryMode,MilkType,Milk_kg,Fat,Snf,FatKg,SnfKg,Date,Time,UserName,Status)Values('" + ddl_Plantname.SelectedItem.Value + "','" + conroute + "','" + converAgentid + "','" + conFarmerNum + "','" + Shift + "','" + QuanMode + "','" + MilkType + "','" + Milk_kg + "','" + Fat + "','" + SNF + "','" + KgFat + "','" + KgSNF + "','" + Date + "','" + Time + "','" + UserName + "','" + Status + "')";
                STR1 = "Insert Into  VMCCDPU(plant_code,prdate,shift,agent_code,producer_code,fat,snf,milk_kg)Values('" + ddl_Plantname.SelectedItem.Value + "','" + Date + "','" + Shift + "','" + converAgentid + "','" + conFarmerNum + "','" + Fat + "','" + SNF + "','" + Milk_kg + "')";
                con = db.GetConnection();
                SqlCommand cmd = new SqlCommand(STR, con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand(STR1, con);
                cmd1.ExecuteNonQuery();
               
            }

            string message = getclass.save;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
        }
        catch
        {
            string message = getclass.Check;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        try
        {
            if (this.fuPdfUpload.HasFile)
            {

                string FilePath = ResolveUrl("~/FILES/");
                string filename = string.Empty;
                filename = Path.GetFileName(Server.MapPath(fuPdfUpload.FileName));// get file name
                fuPdfUpload.SaveAs(Server.MapPath(FilePath) + filename); // save file to uploads folder
                string filePath1 = Server.MapPath(FilePath) + filename;
                ExportPDFToExcel(filePath1);
            }
        }
        catch
        {

        }
    }
    private void ExportPDFToExcel(string file)
    {

        try
        {
            StringBuilder text = new StringBuilder();
            PdfReader pdfReader = new PdfReader(file);
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                using (StringReader reader = new StringReader(currentText))
                {
                    string line;
                    int I = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        I = I + 1;
                        Console.WriteLine(line);
                        if (ADDCOLU == 0)
                        {
                            if (I == 5)
                            {
                                string[] GETLINE = line.Split(' ');
                                DSP.Columns.Add("Date");
                                DSP.Columns.Add("Route");
                                DSP.Columns.Add("VLCC");
                                DSP.Columns.Add("Farmer");
                                DSP.Columns.Add("Shift");
                                DSP.Columns.Add("Time");
                                DSP.Columns.Add("Quantity Mode");
                                DSP.Columns.Add("Measurement Mode");
                                DSP.Columns.Add("Milk Type");
                                DSP.Columns.Add("Quantity");
                                DSP.Columns.Add("Fat");
                                DSP.Columns.Add("KgFat");
                                DSP.Columns.Add("SNF");
                                DSP.Columns.Add("KgSNF");
                                ADDCOLU = 1;

                            }
                            else
                            {

                                if (I > 5)
                                {
                                    string[] GET = line.Split(' ');
                                    if ((GET[0] != "Report".ToString()) && (GET[0] != "Page".ToString()) && (GET[0] != "Powered") && (GET[0] != "INSE".ToString()))
                                    {
                                        string COLLAGENTNAME = GET[2] + GET[3];
                                        DSP.Rows.Add(GET[0], GET[1], COLLAGENTNAME, GET[4], GET[5], GET[6], GET[7], GET[8], GET[9], GET[10], GET[11], GET[12], GET[13], GET[14]);

                                    }
                                }

                            }
                        }

                        else
                        {

                            string[] GET = line.Split(' ');
                            if ((GET[0] != "Report".ToString()) && (GET[0] != "Page".ToString()) && (GET[0] != "Powered") && (GET[0] != "INSE".ToString()))
                            {
                                string COLLAGENTNAME = GET[2] + " " + GET[3];
                                DSP.Rows.Add(GET[0], GET[1], COLLAGENTNAME, GET[4], GET[5], GET[6], GET[7], GET[8], GET[9], GET[10], GET[11], GET[12], GET[13], GET[14]);
                                ViewState["sdtt"] = DSP;
                            }

                        }

                    }
                }
                text.Append(currentText);

            }
            pdfReader.Close();
            GridView1.DataSource = DSP;
            GridView1.DataBind();
        }
        catch
        {

        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {

    }
    protected void Button11_Click(object sender, EventArgs e)
    {

    }
}