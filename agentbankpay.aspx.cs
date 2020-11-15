using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Drawing;
using System.IO;
using System.Globalization;

public partial class agentbankpay : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    string month;
    int Checkstatus;
    DataTable tall = new DataTable();
    double ltr;
    string conltr;
    int i = 0;
    int j = 0;
    int jj = 0;
    int ju = 0; //agentadd
    int roe;
    int roe1;
    DataTable reporting = new DataTable();
    DataTable FILENAME = new DataTable();
    DataTable datecount = new DataTable();
    int datach;
    string Tallyno;
    string serialno;
    double totamt;
    string se;
    string aname;
    string uptime;
    string fdate;
    string todate;
    string pcode2;
    string agent;
    string amount;
    string ppname;
    string narration;
    string INSERT = "";
    string bankfilename;
    string getdattt;
    int CHEKKVAL = 0;
    // int tallystatus;
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
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            getpaymentcompletedata();
        }
        catch
        {
        }
    }
   
    public void getdate()
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
        //   string stt = "Select  CONVERT(VARCHAR,Added_Date,101) AS UpdateTime     from   (Select convert(varchar,Agent_Id) as Agent_Id,NetAmount,convert(varchar,Agent_Name) as Agent_Name,Added_Date,Plant_Code,convert(varchar,Billfrmdate,103) as Billfrmdate,convert(varchar,Billtodate,103) as Billtodate from BankPaymentllotment  where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "' AND TallyStatus IS  NULL    ) as banpay   left join (Select Plant_Code as pmplantcode,Plant_Name   from Plant_Master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'      group by     Plant_Code,Plant_Name) as pm   on banpay.Plant_Code=pm.pmplantcode   GROUP BY  Added_Date  ORDER BY   convert(datetime, Added_Date, 103) ASC   ";

        string stt = "Select  CONVERT(VARCHAR,Added_Date,101) AS UpdateTime     from   (Select convert(varchar,Agent_Id) as Agent_Id,NetAmount,convert(varchar,Agent_Name) as Agent_Name,Added_Date,Plant_Code,convert(varchar,Billfrmdate,103) as Billfrmdate,convert(varchar,Billtodate,103) as Billtodate from BankPaymentllotment  where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "' AND TallyStatus='0'     AND FinanceStatus='1'  ) as banpay   left join (Select Plant_Code as pmplantcode,Plant_Name   from Plant_Master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'      group by     Plant_Code,Plant_Name) as pm   on banpay.Plant_Code=pm.pmplantcode   GROUP BY  Added_Date  ORDER BY   convert(datetime, Added_Date, 103) ASC   ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter sgt = new SqlDataAdapter(cmd);
        datecount.Rows.Clear();
        sgt.Fill(datecount);
    }
    public void getpaymentcompletedata()
    {
        DataTable Report = new DataTable();
        Report.Columns.Add("JVNO");
        Report.Columns.Add("VchDate");
        Report.Columns.Add("Legdername");
        Report.Columns.Add("Amount");
        Report.Columns.Add("ExcessAmount");
        Report.Columns.Add("Narration");

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
        string plantcode = ddl_Plantname.SelectedItem.Value;
        string filename = "SELECT Tid, Plant_code, Agent_id, Frm_date, To_date, AddedDate, totfat_kg, Added_paise, TotAmount FROM   AgentExcesAmount  WHERE  (Plant_code = '" + ddl_Plantname.SelectedItem.Value + "') AND (Frm_date = '" + FDATE + "') AND (To_date = '" + TODATE + "')";
       // string filename = "SELECT DISTINCT BankFileName, UpdatedTime FROM   BankPaymentllotment where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'  and  FinanceStatus='1' ORDER BY UpdatedTime";
        SqlCommand CMDt = new SqlCommand(filename, con);
        SqlDataAdapter acc = new SqlDataAdapter(CMDt);
        DataTable dtfiles = new DataTable();
        dtfiles.Rows.Clear();
        acc.Fill(dtfiles);

        string getreport = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks, UpdatedTime, BankFileName  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks, UpdatedTime, BankFileName    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'  and  FinanceStatus='1'   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
        //string getreport = "SELECT Agent_Master.Agent_Name, BankPaymentllotment.Plant_Code, BankPaymentllotment.NetAmount, BankPaymentllotment.UpdatedTime, BankPaymentllotment.BankFileName FROM  BankPaymentllotment INNER JOIN  Agent_Master ON BankPaymentllotment.Agent_Id = Agent_Master.Agent_Id  WHERE (BankPaymentllotment.Plant_Code = '" + ddl_Plantname.SelectedItem.Value + "') AND (BankPaymentllotment.Billfrmdate = '" + FDATE + "') AND (BankPaymentllotment.Billtodate = '" + TODATE + "') AND (BankPaymentllotment.Remarks='Completed')";
        SqlCommand CMt = new SqlCommand(getreport, con);
        SqlDataAdapter ac = new SqlDataAdapter(CMt);
        DataTable report = new DataTable();
        report.Rows.Clear();
        ac.Fill(report);
        if (report.Rows.Count > 0)
        {
            string plantname = ddl_Plantname.SelectedItem.Text;
            int JV = 0;
            foreach (DataRow dr in report.Rows)
            {
                double filetotalamount = 0;
                double agentexcesamount = 0;
                string jvnumber = "";
                string jvvocherdate = "";
                string jvnarration = "";
                JV = JV + 1;
                DataRow newrow = Report.NewRow();
                foreach (DataRow dra in dtfiles.Select("Agent_id='" + dr["Agent_Id"].ToString() + "'"))
                {
                    agentexcesamount = Convert.ToDouble(dra["TotAmount"].ToString());
                }
                string agentname = dr["Agent_Name"].ToString();
                string agentid = dr["Agent_Id"].ToString();
                string ledgername = agentid + "-" + agentname + "-" + plantname;
                double amt = 0;
                double.TryParse(dr["NetAmount"].ToString(), out amt);
                filetotalamount += amt;
                string narration = "Being the Billperiod Of " + plantname + " From:" + FDATE + " To:" + TODATE + "";
                string vdate = dr["UpdatedTime"].ToString();
                DateTime dtvocherdate = Convert.ToDateTime(vdate);
                string vocherdate = dtvocherdate.ToString("dd-MMM-yy");
                jvvocherdate = vocherdate;
                string cdate = dtvocherdate.ToString("dd");
                string cmonth = dtvocherdate.ToString("MMM");
                newrow["VchDate"] = vocherdate;
                newrow["Legdername"] = ledgername;
                newrow["Amount"] = amt;
                newrow["ExcessAmount"] = agentexcesamount;
                newrow["Narration"] = narration;
                jvnarration = narration;
                newrow["JVNO"] = "Bankpay" + "" + cdate + "" + cmonth + "" + JV;
                jvnumber = "Bankpay" + "" + cdate + "" + cmonth + "" + JV;
                Report.Rows.Add(newrow);
            }
            if (Report.Rows.Count > 0)
            {
                GridView2.DataSource = Report;
                GridView2.DataBind();
            }
        }
    }

    public void GETRPTDATA()
    {
        DataTable Report = new DataTable();
        Report.Columns.Add("JVNO");
        Report.Columns.Add("VchDate");
        Report.Columns.Add("Legdername");
        Report.Columns.Add("Amount");
        Report.Columns.Add("Narration");

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
        string plantcode = ddl_Plantname.SelectedItem.Value;
        string filename = "SELECT DISTINCT BankFileName  FROM   BankPaymentllotment where   Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'  and  FinanceStatus='1'";
        SqlCommand CMDt = new SqlCommand(filename, con);
        SqlDataAdapter acc = new SqlDataAdapter(CMDt);
        DataTable dtfiles = new DataTable();
        dtfiles.Rows.Clear();
        acc.Fill(dtfiles);

        string getreport = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks, UpdatedTime, BankFileName  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks, UpdatedTime, BankFileName    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'  and  FinanceStatus='1'   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
        //string getreport = "SELECT Agent_Master.Agent_Name, BankPaymentllotment.Plant_Code, BankPaymentllotment.NetAmount, BankPaymentllotment.UpdatedTime, BankPaymentllotment.BankFileName FROM  BankPaymentllotment INNER JOIN  Agent_Master ON BankPaymentllotment.Agent_Id = Agent_Master.Agent_Id  WHERE (BankPaymentllotment.Plant_Code = '" + ddl_Plantname.SelectedItem.Value + "') AND (BankPaymentllotment.Billfrmdate = '" + FDATE + "') AND (BankPaymentllotment.Billtodate = '" + TODATE + "') AND (BankPaymentllotment.Remarks='Completed')";
        SqlCommand CMt = new SqlCommand(getreport, con);
        SqlDataAdapter ac = new SqlDataAdapter(CMt);
        DataTable report = new DataTable();
        report.Rows.Clear();
        ac.Fill(report);
        if (dtfiles.Rows.Count > 0)
        {
            string plantname = ddl_Plantname.SelectedItem.Text;
            int JV = 0;
            foreach (DataRow dr in dtfiles.Rows)
            {
                double filetotalamount = 0;
                string jvnumber = "";
                string jvvocherdate = "";
                string jvnarration = "";
                JV = JV + 1;
                foreach (DataRow dra in report.Select("BankFileName='" + dr["BankFileName"].ToString() + "'"))
                {
                    DataRow newrow = Report.NewRow();
                    string agentname = dra["Agent_Name"].ToString();
                    string agentid = dra["Agent_Id"].ToString();
                    string ledgername = agentid + "-" + agentname + "-" + plantname;
                    double amt = 0;
                    double.TryParse(dra["NetAmount"].ToString(), out amt);
                    filetotalamount += amt;
                    string narration = "Being the Billperiod Of " + plantname + " From:" + FDATE + " To:" + TODATE + "";
                    string vdate = dra["UpdatedTime"].ToString();
                    DateTime dtvocherdate = Convert.ToDateTime(vdate);
                    string vocherdate = dtvocherdate.ToString("dd-MMM-yy");
                    jvvocherdate = vocherdate;
                    string cdate = dtvocherdate.ToString("dd");
                    string cmonth = dtvocherdate.ToString("MMM");
                    newrow["VchDate"] = vocherdate;
                    newrow["Legdername"] = ledgername;
                    newrow["Amount"] = amt;
                    newrow["Narration"] = narration;
                    jvnarration = narration;
                    newrow["JVNO"] = "Bankpay" + "" + cdate + "" + cmonth + "" + JV;
                    jvnumber = "Bankpay" + "" + cdate + "" + cmonth + "" + JV;
                    Report.Rows.Add(newrow);
                }
                DataRow newrow1 = Report.NewRow();
                newrow1["VchDate"] = jvvocherdate;
                newrow1["Legdername"] = "SVDS.P.LTD.PUNABAKA PLANT";
                string neg = "-";
                newrow1["Amount"] = neg + "" + filetotalamount;
                newrow1["Narration"] = jvnarration;
                newrow1["JVNO"] = jvnumber;
                Report.Rows.Add(newrow1);
            }
            if (Report.Rows.Count > 0)
            {
                GridView2.DataSource = Report;
                GridView2.DataBind();
            }
        }
    }
    public void GETREPORTINGTALLY()
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
        // GETTS = "SELECT    JVNO,REPLACE(CONVERT(VARCHAR(9), JvDate, 6), ' ', '-') AS [VchDate],Legdername,Amount,Narration   FROM      TallyloanEntryJvpassAgentWsie   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and Frm_Date='" + FDATE + "'    and To_date='" + TODATE + "'  and UpdateStatus is null ";
        GETTS = " sELECT   ('BANKPAY' +'_' + JVNO) AS JVNO,VchDate,Legdername,Amount,Narration   FROM (SELECT CONVERT(VARCHAR,JVNO) AS JVNO,REPLACE(CONVERT(VARCHAR(9), JvDate, 6), ' ', '-') AS [VchDate],Legdername,Amount,Narration   FROM      TallyloanEntryJvpassAgentWsie   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and Frm_Date='" + FDATE + "'    and To_date='" + TODATE + "'  and UpdateStatus is null) AS HH";
        SqlCommand cmd = new SqlCommand(GETTS, con);
        SqlDataAdapter abc = new SqlDataAdapter(cmd);
        DataTable views = new DataTable();
        views.Rows.Clear();
        abc.Fill(views);
        if (views.Rows.Count > 0)
        {

            GridView2.DataSource = views;
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

        //}
        //else
        //{
        //    string megg = "Record Already Updated ";
        //    Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(megg) + "')</script>");
        //}




    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        //GETREPORTINGTALLY();
        GETRPTDATA();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    public void gertfilename()
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
            con = DB.GetConnection();
            string stt = "Select   BankFileName   from BankPaymentllotment    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  AND Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'   and  Added_Date='" + getdattt + "'  AND FinanceStatus='1'  group by BankFileName";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            FILENAME.Rows.Clear();
            DA.Fill(FILENAME);
            //if (filename.Rows.Count > 0)
            //{
            //    ddl_filename.DataSource = filename;
            //    ddl_filename.DataTextField = "BankFileName";
            //    ddl_filename.DataValueField = "BankFileName";
            //    ddl_filename.DataBind();
            //}
        }
        catch
        {

        }
    }
}