using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Drawing;
using System.IO;
using System.Globalization;

public partial class AgentExcessBankPayment : System.Web.UI.Page
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
                    billdate();
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

                pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {


        }
    }

    public void billdate()
    {

        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  order by  Bill_frmdate desc  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d1.ToString("dd/MM/yyy");
                    Todate = d2.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);
                }
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

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
        con = DB.GetConnection();
        string GETTS = "";
        DataTable dt = new DataTable();
        dt.Columns.Add("Voucher No");
        dt.Columns.Add("Voucher Date");
        dt.Columns.Add("Ledger Name");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Narration");
        // GETTS = "SELECT    JVNO,REPLACE(CONVERT(VARCHAR(9), JvDate, 6), ' ', '-') AS [VchDate],Legdername,Amount,Narration   FROM      TallyloanEntryJvpassAgentWsie   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and Frm_Date='" + FDATE + "'    and To_date='" + TODATE + "'  and UpdateStatus is null ";
        //GETTS = " sELECT (convert(varchar(9),Sno,6) +'_'+convert(varchar(9),plant_code,6)+'_BANKPAY') AS Voucher_No,REPLACE(CONVERT(VARCHAR(9), vchDate, 6), ' ', '-') as Voucher_Date,SupplierName as Legdername, LedgerAmount as Amount,Narration   FROM TallyExcessAmount where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and frm_date='" + FDATE + "' and To_date='" + TODATE + "' ";
//        SqlCommand cmd = new SqlCommand("sELECT (convert(varchar(9),Sno,6) +'_'+convert(varchar(9),plant_code,6)+'_BANKPAY') AS Voucher_No,REPLACE(CONVERT(VARCHAR(9), vchDate, 6), ' ', '-') as Voucher_Date,SupplierName as Legdername, LedgerAmount as Amount,Narration   FROM TallyExcessAmount where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and frm_date='" + FDATE + "' and To_date='" + TODATE + "'", con);
        //SqlCommand cmd = new SqlCommand("Select (sno +'_'+plant_code+'BANKPAY') AS JVno,Voucher_Date,Legdername,Amount,'Being Bank Pay' +'_'+ Amount as ff from (sELECT convert(varchar,Sno) as Sno , convert(varchar,plant_code) as plant_code, REPLACE(CONVERT(VARCHAR(9), vchDate, 6), ' ', '-') as Voucher_Date,SupplierName as Legdername,convert(varchar,LedgerAmount) as Amount,convert(varchar,Narration) as Narration,frm_date,To_date FROM TallyExcessAmount where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and frm_date='" + FDATE + "' and To_date='" + TODATE + "') as fhg", con);
        SqlCommand cmd = new SqlCommand("Select Sno,REPLACE(CONVERT(VARCHAR(9), vchDate, 6), ' ', '-') as Voucher_Date,SupplierName as Legdername,LedgerAmount as Amount,plant_code,convert(varchar,LedgerAmount) as Amount,convert(varchar,Narration) as Narration,REPLACE(CONVERT(VARCHAR(9), frm_date, 6), ' ', '-') as frm_date,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') as To_date FROM TallyExcessAmount where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and frm_date='" + FDATE + "' and To_date='" + TODATE + "' ", con);
        SqlDataAdapter ac = new SqlDataAdapter(cmd);
        DataTable report = new DataTable();
        report.Rows.Clear();
        ac.Fill(report);
        if (report.Rows.Count > 0)
        {
            foreach (DataRow dr in report.Rows)
            {
                DataRow newrow = dt.NewRow();
                newrow["Voucher No"] = dr["Sno"].ToString() + "_" + dr["plant_code"].ToString() + "_BANKPAY";
                newrow["Voucher Date"] = dr["Voucher_Date"].ToString();
                newrow["Ledger Name"] = dr["Legdername"].ToString();
                newrow["Amount"] = dr["Amount"].ToString();
                newrow["Narration"] = "Being the Bank Pay of Rs." + dr["Amount"].ToString() + "/- to " + dr["Legdername"].ToString() + " FromDate: " + dr["frm_date"].ToString() + " ToDate: " + dr["To_date"].ToString() + " for excess milk payment";
                dt.Rows.Add(newrow);
            }
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = "No Records";
            GridView2.DataBind();
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            string date = ddl_BillDate.Text;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + ":Bill Period" + FDATE + "To:" + TODATE + "'.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView2.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch
        {


        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}