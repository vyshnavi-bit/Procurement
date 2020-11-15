using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class Accounts_TransactionReport : System.Web.UI.Page
{
    
    public string strsql;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string str;
    public string[] strarr = new string[10];
    public string headid = string.Empty;
    public string Groupheadid = string.Empty;
    
    DataSet ds = new DataSet();
    BOLD_Accoun acc = new BOLD_Accoun();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    string Subheadid = string.Empty;
    DateTime tdt = new DateTime();
    BLLPlantName BllPlant = new BLLPlantName();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
           
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
           // managmobNo = Session["managmobNo"].ToString();
            tdt = System.DateTime.Now;
            txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
            txt_ToDate.Text = tdt.ToString("dd/MM/yyy");

            if (roleid < 3)
            {
                LoadSinglePlantName();
            }
            else
            {
                LoadPlantName();
            }
                

            pcode = ddl_PlantName.SelectedItem.Value;
            LoadAccountHead(ccode);
            headid = ddl_HeadName.SelectedItem.Value;
            Iddiv();
            LoadAccountSubHead(ccode, headid, Groupheadid);
            Subheadid = ddl_SubHeadName.SelectedItem.Value;

            //Invisible
            Label1.Visible = true;
            ddl_SubHeadName.Visible = true;
            CheckAll();
            loadreport();
            Label4.Visible = false;
            ddl_Type.Visible = false;
           
        }
        else
        {
            ccode = Session["Company_code"].ToString();
            pcode = ddl_PlantName.SelectedItem.Value;
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
         //   managmobNo = Session["managmobNo"].ToString();
            headid = ddl_HeadName.SelectedItem.Value;
            Iddiv();
            Subheadid = ddl_SubHeadName.SelectedItem.Value;
             CheckAll();
            loadreport();

        }
    }
    private void Iddiv()
    {
        str = headid;
        strarr = str.Split('_');
        if (strarr[0].ToString() != "00")
        {
            Groupheadid = strarr[0];
            headid = strarr[1];
        }
    }
    private void CheckAll()
    {
        if (Chk_All.Checked == true)
        {
            Label1.Visible = false;
            ddl_SubHeadName.Visible = false;
            Label4.Visible = false;
            ddl_Type.Visible = false;
            Label6.Visible = false;
            ddl_HeadName.Visible = false;

            //IOU
            Chk_Iou.Checked = false;
            Chk_Iou.Visible = false;
            Chk_IouAll.Checked = false;
            Chk_IouAll.Visible = false;
        }
        else
        {
            Label1.Visible = true;
            ddl_SubHeadName.Visible = true;
            //Label4.Visible = true;
            //ddl_Type.Visible = true;
            Label6.Visible = true;
            ddl_HeadName.Visible = true;

            //IOU
            Chk_Iou.Visible = true;
            Chk_IouAll.Visible = true;
        }
    }
    private void LoadAccountHead(string Ccode)
    {
        try
        {
            ds = null;
            ds = GetAccountHead(Ccode.ToString());
            ddl_HeadName.DataSource = ds;
            ddl_HeadName.DataTextField = "Head_Name";
            ddl_HeadName.DataValueField = "Head_Id";
            ddl_HeadName.DataBind();          
            if (ddl_HeadName.Items.Count > 0)
            {
            }
            else
            {
                ddl_HeadName.Items.Add("--Add AccountHead Name--");
            }
        }
        catch (Exception ex)
        {
        }
    }
    public DataSet GetAccountHead(string ccode)
    {
        DataSet ds = new DataSet();
        ds = null;
        // strsql = "SELECT Head_Id,CONVERT(nvarchar(50),Head_Id)+'_'+ Head_Name AS Head_Name FROM Account_HeadName Order by Head_Id";
        // strsql = "SELECT Head_Id,Head_Name AS Head_Name FROM Account_HeadName Order by Head_Id";
        // strsql = "SELECT ah.Gid AS GroupHead_ID,ah.Head_Id AS Head_Id,ah.Head_Name AS Head_Name FROM (SELECT GroupHead_ID FROM accounts_groupname) AS gh INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id,Head_Name AS Head_Name FROM Account_HeadName)AS ah ON gh.GroupHead_ID=ah.Gid order by ah.Gid,Head_Id";
        strsql = "SELECT CONVERT(Nvarchar(15),ah.Gid)+'_'+CONVERT(Nvarchar(15),ah.Head_Id) AS Head_Id,ah.Head_Name AS Head_Name FROM (SELECT GroupHead_ID FROM accounts_groupname) AS gh INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id,Head_Name AS Head_Name FROM Account_HeadName)AS ah ON gh.GroupHead_ID=ah.Gid order by ah.Gid,Head_Id";
        ds = DBaccess.GetDataset(strsql);
        return ds;
    }
    private void LoadAccountSubHead(string Ccode, string headid, string groupheadcode)
    {
        try
        {
            ds = null;
            ds = GetAccountSubHead(Ccode, headid, groupheadcode);
            ddl_SubHeadName.DataSource = ds;
            ddl_SubHeadName.DataTextField = "Head_Name";
            ddl_SubHeadName.DataValueField = "Ledger_Id";
            ddl_SubHeadName.DataBind();
            if (ddl_SubHeadName.Items.Count > 0)
            {
            }
            else
            {
                ddl_SubHeadName.Items.Add("--Add AccountSubHead Name--");
            }
        }
        catch (Exception ex)
        {
        }
    }
    public DataSet GetAccountSubHead(string Ccode, string headid, string groupheadcode)
    {
        DataSet ds = new DataSet();
        try
        {
            
            using (con = DBaccess.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Get_Ledger]");
                SqlParameter parcompanycode, parplantcode, parGroupId, parHeadId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                parcompanycode = cmd.Parameters.Add("@spccode", SqlDbType.NVarChar, 15);
                parplantcode = cmd.Parameters.Add("@sppcode", SqlDbType.NVarChar, 15);
                parGroupId = cmd.Parameters.Add("@spGroupId", SqlDbType.NVarChar, 15);
                parHeadId = cmd.Parameters.Add("@spHeadId", SqlDbType.NVarChar, 15);

                parcompanycode.Value = ccode;
                parplantcode.Value = pcode;
                parGroupId.Value = Groupheadid.Trim();
                parHeadId.Value = headid.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
        }
        catch (Exception ex)
        {
            return ds;
        }

    }
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        loadreport();
    }
    public void loadreport()
    {
        try
        {
            DataTable dt = new DataTable();
            if (Chk_All.Checked == true)
            {
                cr.Load(Server.MapPath("Accounts_TransactionReport.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Accounts_TypeReport.rpt"));
            }

            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;
            CrystalDecisions.CrystalReports.Engine.TextObject t5;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
            t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Title"];

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            t1.Text = ccode + "_" + cname;
            t2.Text = ddl_PlantName.SelectedItem.Text;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To" + txt_ToDate.Text.Trim();

            if (Chk_All.Checked == true)
            {
                t5.Text = "Acoounts Transactions Report From";
            }
            else
            {               
                if (Chk_Iou.Checked == true && Chk_IouAll.Checked == true)
                {
                    t3.Text = "";
                    t4.Text = "";
                    t5.Text = "Acoounts IOU Report ";
                }
                else if (Chk_Iou.Checked == true && Chk_IouAll.Checked == false)
                {
                    t5.Text = "Acoounts IOU Report From" + t3.Text + t4.Text;
                }
                else if (Chk_Iou.Checked == false && Chk_IouAll.Checked == false)
                {
                    t5.Text = "Acoounts Transactions Report From";
                }               

            }


            string str = string.Empty;
            pcode = ddl_PlantName.SelectedItem.Value;

            if (Chk_All.Checked == true)
            {
                SqlCommand cmd = new SqlCommand();
                String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AccountsReport]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@companycode", ccode);
                    sqlCmd.Parameters.AddWithValue("@plantcode", pcode);
                    sqlCmd.Parameters.AddWithValue("@fromdate", d1.Trim());
                    sqlCmd.Parameters.AddWithValue("@todate", d2.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    da.Fill(dt);
                }
            }
            else
            {
                //str = "SELECT * FROM Account_Transaction  Where  Head_Id='" + ddl_HeadName.Text.Trim() + "' AND Voucher_Type='" + ddl_Type.Text.Trim() + "' AND EntryDate Between '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                // str = "SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,Amount,Narration,Check_Date,Faviour_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type FROM Account_Transaction  Where Plant_code='" + pcode.Trim() + "'  AND EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' Order By Tid";                
                // str = "SELECT ac.*,cl.LedgerName FROM (SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_code='" + pcode.Trim() + "' AND  EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' ) AS ac INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id AS Hid,Ledger_Id AS Lid,LedgerName FROM ChillingLedger Where Plant_code='" + pcode + "' ) cl ON ac.GroupHead_Id=cl.Gid AND ac.Head_Id=cl.Hid AND ac.SubHead_Id=cl.Lid  Order By Tid";
               // str = "SELECT ac.*,cl.LedgerName FROM (SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_code='" + pcode.Trim() + "' AND  EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' AND Voucher_Type='" + ddl_Type.Text.Trim() + "' ) AS ac INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id AS Hid,Ledger_Id AS Lid,LedgerName FROM ChillingLedger Where Plant_code='" + pcode + "') cl ON ac.GroupHead_Id=cl.Gid AND ac.Head_Id=cl.Hid AND ac.SubHead_Id=cl.Lid  Order by ac.Tid";
                if (Chk_Iou.Checked == true && Chk_IouAll.Checked == true)
                {
                   // str = " SELECT  Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_Code='" + pcode.Trim() + "' AND Voucher_Type='Due'";
                    str = "SELECT ac.*,cl.LedgerName FROM (SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_code='" + pcode + "' AND  Voucher_Type='Due' AND DebitAmount>0 ) AS ac INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id AS Hid,Ledger_Id AS Lid,LedgerName FROM ChillingLedger ) cl ON ac.GroupHead_Id=cl.Gid AND ac.Head_Id=cl.Hid AND ac.SubHead_Id=cl.Lid  Order by ac.Tid";
                }
                else if (Chk_Iou.Checked == true && Chk_IouAll.Checked == false)
                {
                    //str = " SELECT  Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_Code='" + pcode.Trim() + "' AND EntryDate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'  AND Voucher_Type='Due'";
                    str = "SELECT ac.*,cl.LedgerName FROM (SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_code='" + pcode + "' AND  Voucher_Type='Due' AND DebitAmount>0 AND EntryDate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') AS ac INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id AS Hid,Ledger_Id AS Lid,LedgerName FROM ChillingLedger ) cl ON ac.GroupHead_Id=cl.Gid AND ac.Head_Id=cl.Hid AND ac.SubHead_Id=cl.Lid  Order by ac.Tid";
                }
                else if(Chk_Iou.Checked == false && Chk_IouAll.Checked == false)
                {
                    str = "SELECT ac.*,cl.LedgerName FROM (SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_code='" + pcode + "' AND  EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' AND GroupHead_Id='" + Groupheadid.Trim() + "' AND Head_Id='" + headid.Trim() + "' AND SubHead_Id='" + ddl_SubHeadName.SelectedItem.Value.Trim() + "'  ) AS ac INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id AS Hid,Ledger_Id AS Lid,LedgerName FROM ChillingLedger ) cl ON ac.GroupHead_Id=cl.Gid AND ac.Head_Id=cl.Hid AND ac.SubHead_Id=cl.Lid  Order by ac.Tid";
                }
                dt = DBaccess.GetDatatable(str);
                
            }
          
            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }
    

    private void LoadPlantName()
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
    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
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
    protected void Chk_All_CheckedChanged(object sender, EventArgs e)
    {
        CheckAll();
    }
    protected void ddl_HeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        headid = ddl_HeadName.SelectedItem.Value.Trim();
        Iddiv();
     
        LoadAccountSubHead(ccode, headid, Groupheadid);
    }


    //public void loadreport()
    //{
    //    try
    //    {
    //        cr.Load(Server.MapPath("Accounts_TransactionReport.rpt"));
    //        cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
    //        CrystalDecisions.CrystalReports.Engine.TextObject t1;
    //        CrystalDecisions.CrystalReports.Engine.TextObject t2;
    //        CrystalDecisions.CrystalReports.Engine.TextObject t3;
    //        CrystalDecisions.CrystalReports.Engine.TextObject t4;

    //        t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
    //        t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
    //        t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
    //        t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];


    //        t1.Text = ccode + "_" + cname;
    //        t2.Text = ddl_PlantName.SelectedItem.Text;


    //        DateTime dt1 = new DateTime();
    //        DateTime dt2 = new DateTime();

    //        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

    //        string d1 = dt1.ToString("MM/dd/yyyy");
    //        string d2 = dt2.ToString("MM/dd/yyyy");

    //        t3.Text = txt_FromDate.Text.Trim();
    //        t4.Text = "To" + txt_ToDate.Text.Trim();


    //        string str = string.Empty;
    //        SqlConnection con = null;
    //        string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        con = new SqlConnection(connection);

    //        pcode = ddl_PlantName.SelectedItem.Value;
    //        if (Chk_All.Checked == true)
    //        {
    //            //str = "SELECT * FROM Account_Transaction  Where  Head_Id='" + ddl_HeadName.Text.Trim() + "' AND Voucher_Type='" + ddl_Type.Text.Trim() + "' AND EntryDate Between '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
    //            // str = "SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,Amount,Narration,Check_Date,Faviour_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type FROM Account_Transaction  Where Plant_code='" + pcode.Trim() + "'  AND EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' Order By Tid";                
    //            str = "SELECT ac.*,cl.LedgerName FROM (SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_code='" + pcode.Trim() + "' AND  EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' ) AS ac INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id AS Hid,Ledger_Id AS Lid,LedgerName FROM ChillingLedger Where Plant_code='" + pcode + "' ) cl ON ac.GroupHead_Id=cl.Gid AND ac.Head_Id=cl.Hid AND ac.SubHead_Id=cl.Lid  Order By Tid";

    //        }
    //        else
    //        {
    //            // str = "SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,Amount,Narration,Check_Date,Faviour_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type FROM Account_Transaction  Where Plant_code='" + pcode.Trim() + "' AND Head_Id='" + headid.Trim() + "' AND   SubHead_Id='" + ddl_SubHeadName.SelectedItem.Value.Trim() + "'  AND EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' Order By Tid";
    //            str = "SELECT ac.*,cl.LedgerName FROM (SELECT  Tid,Reference_No,Convert(NVARCHAR(25),EntryDate,103) AS EntryDate,Head_Id,SubHead_Id,ModeofEntry,ISNULL(CreditAmount,0) AS CreditAmount,ISNULL(DebitAmount,0) AS DebitAmount,Narration,Check_Date,Faviour_Name,Reference_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,GroupHead_Id FROM Account_Transaction  Where Plant_code='" + pcode.Trim() + "' AND GroupHead_Id='" + Groupheadid + "' AND Head_Id='" + headid + "' AND SubHead_Id='" + ddl_SubHeadName.SelectedItem.Value.Trim() + "' AND  EntryDate Between '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' ) AS ac INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id AS Hid,Ledger_Id AS Lid,LedgerName FROM ChillingLedger Where Plant_code='" + pcode + "' AND GroupHead_ID='" + Groupheadid + "' AND Head_Id='" + headid + "' AND Ledger_Id='" + ddl_SubHeadName.SelectedItem.Value.Trim() + "') cl ON ac.GroupHead_Id=cl.Gid AND ac.Head_Id=cl.Hid AND ac.SubHead_Id=cl.Lid  Order by ac.Tid";
    //        }


    //        SqlCommand cmd = new SqlCommand();
    //        SqlDataAdapter da = new SqlDataAdapter(str, con);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);

    //        cr.SetDataSource(dt);
    //        CrystalReportViewer1.ReportSource = cr;
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}

    protected void ddl_Type_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}