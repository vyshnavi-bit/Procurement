using System;
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
using System.Net;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using System.Text;
public partial class SubAgentBankFIle : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable dttp = new DataTable();
    string FDATE;
    string TODATE;
    public string frmdate;
    public string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    SqlDataReader dr;
    int datasetcount = 0;
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataTable fatkgdt = new DataTable();
    DataTable agent = new DataTable();
    string d1;
    string d2;
    DateTime d11;
    DateTime d22;
    string agentcode;
    int countinsertdetails;
    string sttr;

    string planttype;
    string planttypehdfc;
    string paymode;
    string ptype;
    string statement;
    string accountnu;
    string companyname;
    DataTable showreport = new DataTable();
    int GETID;
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
                        billdate();
                        getagentid();

                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        billdate();
                        getagentid();
                    }

                    lbl_ktk.Visible = false;
                    ddl_kotack.Visible = false;
                }
                else
                {


                }

            }
            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;

                ViewState["pcode"] = pcode.ToString();
            }

        }

        catch
        {


        }
    }

    public void getagentid()
    {
        try
        {
            string getagent;
            con = DB.GetConnection();
            getagent = "Select agent_id   from Agent_master  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and subagent='1'  GROUP BY  agent_id ";
            SqlCommand cmd = new SqlCommand(getagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            agent.Rows.Clear();
            da.Fill(agent);
            ddl_getagent.DataSource = agent;
            ddl_getagent.DataTextField = "agent_id";
            ddl_getagent.DataValueField = "agent_id";
            ddl_getagent.DataBind();
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
    public void GETBILLDATE()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
        }
        catch
        {

        }
    }
    public void billdate()
    {
        try
        {

            con.Close();
            con = DB.GetConnection();
            string str;

            str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    d11 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d22 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d11.ToString("dd/MM/yyy");
                    Todate = d22.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);

                }

            }
        }
        catch
        {


        }
    }
    public void convdate()
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

    public void billBillpaymentDetails()
    {
        try
        {
            GETBILLDATE();
            con = DB.GetConnection();
            string stt = "";
            if (RadioButtonList1.SelectedValue == "1")
            {
                // stt = " select Agent_id,Addedpaise,Fatkg,ExcesAmt,BillAmount,TotalAmount  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id ) as aa where Bank_id in (15) order by RAND(Agent_id) asc";
                stt = "Select    Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,FLOOR(Sum(NEtamount)) as Netamount  FROM SubAgnetpaymentdetails  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Agent_id='" + ddl_getagent.SelectedItem.Value + "'  and Frm_date='" + FDATE + "'  and To_date='" + TODATE + "' and  bank_name  in ('HDFC Bank') group by  Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Agent_id";
            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                stt = "Select    Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,FLOOR(Sum(NEtamount)) as Netamount  FROM SubAgnetpaymentdetails  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Agent_id='" + ddl_getagent.SelectedItem.Value + "'  and Frm_date='" + FDATE + "'  and To_date='" + TODATE + "' and  bank_name  not in ('HDFC Bank') group by  Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Agent_id";
            }
            if (RadioButtonList1.SelectedValue == "3")
            {
                stt = "Select    Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,FLOOR(Sum(NEtamount)) as Netamount  FROM SubAgnetpaymentdetails  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Agent_id='" + ddl_getagent.SelectedItem.Value + "'  and Frm_date='" + FDATE + "'  and To_date='" + TODATE + "'  group by  Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Agent_id";
            }
            //   string stt = "select  excessAgent_id as   Agent_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            if (DTG.Tables[datasetcount].Rows.Count > 0)
            {
                GridView1.DataSource = DTG.Tables[datasetcount];
                GridView1.DataBind();
                GridView1.FooterRow.Cells[4].Text = "Total Amount";

                decimal milkkg = DTG.Tables[datasetcount].AsEnumerable().Sum(row => row.Field<decimal>("Netamount"));
                GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[5].Text = milkkg.ToString();


                //decimal BillAmount = DTG.Tables[datasetcount].AsEnumerable().Sum(row => row.Field<decimal>("BillAmount"));
                //GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //GridView1.FooterRow.Cells[5].Text = BillAmount.ToString("N2");

                //decimal TotalAmount = DTG.Tables[datasetcount].AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));
                //GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //GridView1.FooterRow.Cells[6].Text = TotalAmount.ToString("N2");

                //GridView1.FooterRow.Cells[4].Font.Bold = true;
                //GridView1.FooterRow.Cells[2].Font.Bold = true;
                //GridView1.FooterRow.Cells[5].Font.Bold = true;
                //GridView1.FooterRow.Cells[6].Font.Bold = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch
        {

        }
    }
    protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void gethdfcfile()
    {
        try
        {

            GETBILLDATE();
            int cc;
            string filename;
            con = DB.GetConnection();
            //  string hdfc = "select Agent_accountNo,LEN(Agent_accountNo),'c' aS  ExcesAmt,CAST(ExcesAmt AS DECIMAL(18,2)) AS AMOUNT,'Milk pay' AS   ExcesAmt   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo ) as aa where Bank_id  in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id ";
            string hdfc = "select Agent_accountNo AS ACCOUNT,'C' aS  C, CAST(Netamount AS DECIMAL(18,2)) AS AMOUNT,'Milk pay' AS   NARRATION   from (Select    FLOOR(Sum(NEtamount)) as Netamount,Agent_Name,Bank_Name,ifsc_code,Agent_accountNo  FROM SubAgnetpaymentdetails  where  Plant_Code='"+ddl_Plantname.SelectedItem.Value+"'  and Agent_id='"+ddl_getagent.SelectedItem.Value+"'  and Frm_date='"+FDATE+"'  and To_date='"+TODATE+"' and  bank_name  in ('HDFC Bank') group by  Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Agent_id) as get";
            SqlCommand CMD = new SqlCommand(hdfc, con);
            SqlDataAdapter DA = new SqlDataAdapter(CMD);
            DataTable HDFC = new DataTable();
            DA.Fill(HDFC);
            cc = HDFC.Rows.Count;
            GridView5.DataSource = HDFC;
            GridView5.DataBind();

            if ((pcode == "157") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "168") || (pcode == "169"))
            {

                planttype = "SVD1";

                planttypehdfc = "SVD1H";



            }


            else
            {

                planttype = "SVD2";

                planttypehdfc = "SVD2H";


            }

            Response.Clear();
            Response.Buffer = true;

            string dat, mon;
            dat = System.DateTime.Now.ToString("dd");
            mon = System.DateTime.Now.ToString("MM");
            filename = dat + mon;
            string rowcount = string.Empty;

            if (cc < 10)
            {
                rowcount = "00" + cc.ToString();
            }
            else if (cc >= 10 && cc < 100)
            {
                rowcount = "0" + cc.ToString();
            }
            else if (cc >= 100)
            {
                rowcount = cc.ToString();
            }

            // Response.AddHeader("content-disposition", "attachment;filename=bms.009");
            //   Response.AddHeader("content-disposition", "attachment;filename=SVD2H" + filename + "." + rowcount );
            Response.AddHeader("content-disposition", "attachment;filename=" + planttypehdfc + filename + "." + rowcount);
            Response.Charset = "";
            Response.ContentType = "application/text";
            GridView5.AllowPaging = false;
            GridView5.DataBind();
            StringBuilder Rowbind = new StringBuilder();


            int L = GridView5.Columns.Count;
            int M = 1;
            for (int k = 0; k < GridView5.Columns.Count; k++)
            {
                if (M == L)
                {
                    Rowbind.Append(GridView5.Columns[k].HeaderText);
                }
                else
                {
                    Rowbind.Append(GridView5.Columns[k].HeaderText + ',');
                }
                M++;

            }
            Rowbind.Append("\r\n");


            for (int i = 0; i < GridView5.Rows.Count; i++)
            {
                int s = GridView5.Columns.Count;
                int j = 1;
                for (int k = 0; k < GridView5.Columns.Count; k++)
                {
                    if (j == s)
                    {
                        Rowbind.Append(GridView5.Rows[i].Cells[k].Text);
                    }
                    else
                    {
                        Rowbind.Append(GridView5.Rows[i].Cells[k].Text + ',');
                    }
                    j++;

                }

                Rowbind.Append("\r\n");
            }
            Response.Output.Write(Rowbind.ToString());
            Response.Flush();
            Response.End();

        }
        catch
        {


        }
    }
    public void gethdfcfiletoothers()
    {

        try
        {

            if ((pcode == "157") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "168") || (pcode == "169"))
            {

                planttype = "SVD1";

                planttypehdfc = "SVD1H";



            }


            else
            {

                planttype = "SVD2";

                planttypehdfc = "SVD2H";


            }

            GETBILLDATE();
            int cc = 0;
            string filename;
            con = DB.GetConnection();
            //  string hdfc = "select Agent_accountNo,LEN(Agent_accountNo),'c' aS  ExcesAmt,CAST(ExcesAmt AS DECIMAL(18,2)) AS AMOUNT,'Milk pay' AS   ExcesAmt   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo ) as aa where Bank_id  in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id ";
            string hdfc = "select 'N' as TranType, Agent_accountNo AS ACCOUNT,netamount as AMOUNT,REPLACE(Agent_Name,'.',' ') AS AgentName,Agent_id,CONVERT(NVARCHAR(30),GETDATE(),103) AS PayDate,Ifsc_code as Ifsccode,Bank_Name as BankName,Pmail  from (select  agent_id,Agent_Name,Bank_name,Agent_accountNo,ifsc_code,FLOOR(Sum(NEtamount)) as Netamount,plant_code    from SubAgnetpaymentdetails where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and agent_id='" + ddl_getagent.SelectedItem.Value + "' and    Frm_Date='" + FDATE + "'    AND To_Date='"+TODATE+"' and   bank_name not in ('HDFC Bank') group by  agent_id,Agent_Name,Bank_name,Agent_accountNo,ifsc_code,plant_code  ) as subagent left join (Select plant_code as pmplant_code,pmail from Plant_Master WHERE   Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' group by  plant_code,pmail) AS Pm on subagent.plant_code=Pm.pmplant_code";
            SqlCommand CMD = new SqlCommand(hdfc, con);
            SqlDataAdapter DA = new SqlDataAdapter(CMD);
            DataTable HDFCothers = new DataTable();
            DA.Fill(HDFCothers);
            cc = HDFCothers.Rows.Count;
            GridView6.DataSource = HDFCothers;
            GridView6.DataBind();
            Response.Clear();
            Response.Buffer = true;

            string dat, mon;
            dat = System.DateTime.Now.ToString("dd");
            mon = System.DateTime.Now.ToString("MM");
            filename = dat + mon;
            string rowcount = string.Empty;

            if (cc < 10)
            {
                rowcount = "00" + cc.ToString();
            }
            else if (cc >= 10 && cc < 100)
            {
                rowcount = "0" + cc.ToString();
            }
            else if (cc >= 100)
            {
                rowcount = cc.ToString();
            }

            // Response.AddHeader("content-disposition", "attachment;filename=bms.009");
            //  Response.AddHeader("content-disposition", "attachment;filename=SVD2" + filename + "." + rowcount);
            Response.AddHeader("content-disposition", "attachment;filename=" + planttype + filename + "." + rowcount);
            Response.Charset = "";
            Response.ContentType = "application/text";
            GridView6.AllowPaging = false;
            GridView6.DataBind();
            StringBuilder Rowbind = new StringBuilder();

            //Add Header in UploadFile
            //int L = GridView6.Columns.Count;
            //int M = 1;
            //for (int k = 0; k < GridView6.Columns.Count; k++)
            //{
            //    if (M == L)
            //    {
            //        Rowbind.Append(GridView6.Columns[k].HeaderText);
            //    }
            //    else
            //    {
            //        Rowbind.Append(GridView6.Columns[k].HeaderText + ',');
            //    }
            //    M++;

            //}
            //Rowbind.Append("\r\n");

            //Add Rows in UploadFile
            for (int i = 0; i < GridView6.Rows.Count; i++)
            {
                int s = GridView6.Columns.Count;
                int j = 1;
                for (int k = 0; k < GridView6.Columns.Count; k++)
                {
                    if (j == s)
                    {
                        Rowbind.Append(GridView6.Rows[i].Cells[k].Text);
                    }
                    else if (j == 1)
                    {
                        Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                    }
                    else if (j == 4)
                    {
                        Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',');
                    }
                    else if (j == 5)
                    {
                        Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',');
                    }
                    else if (j == 6)
                    {
                        Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                    }
                    else if (j == 8)
                    {
                        Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                    }
                    else
                    {
                        Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',');
                    }
                    j++;

                }

                Rowbind.Append("\r\n");
            }
            Response.Output.Write(Rowbind.ToString());
            Response.Flush();
            Response.End();
        }
        catch
        {

        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        getagentid();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        billBillpaymentDetails();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        GETBILLDATE();
        if (RadioButtonList1.SelectedItem.Value == "1")
        {
            try
            {
                gethdfcfile();
            }
            catch (Exception ex)
            {

            }
        }
        if (RadioButtonList1.SelectedItem.Value == "2")
        {
            try
            {
                gethdfcfiletoothers();
            }
            catch (Exception ex)
            {

            }

        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
               

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                if (RadioButtonList1.SelectedItem.Value == "1")
                {
                    HeaderCell2.Text = "Hdfc Bank Sub Agent Payment For plant:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text + "Agent Id:" + ddl_getagent.SelectedItem.Value;
                }
                else
                {
                    HeaderCell2.Text = "Non Hdfc Bank Sub Agent Payment For plant:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text + "Agent Id:" + ddl_getagent.SelectedItem.Value;
                }
               
                HeaderCell2.ColumnSpan = 6;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {


        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();

                if (RadioButtonList1.SelectedItem.Value == "1")
                {
                    HeaderCell2.Text = "Hdfc Bank Sub Agent Payment For plant:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text + "Agent Id:" + ddl_getagent.SelectedItem.Value;
                }
                else
                {
                    HeaderCell2.Text = "Non Hdfc Bank Sub Agent Payment For plant:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text + "Agent Id:" + ddl_getagent.SelectedItem.Value;
                }
              
                HeaderCell2.ColumnSpan = 6;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {


        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Value != "4")
        {
            lbl_ktk.Visible = false;
            ddl_kotack.Visible = false;

        }
        if (RadioButtonList1.SelectedItem.Value == "4")
        {
            lbl_ktk.Visible = true;
            ddl_kotack.Visible = true;

        }
      

    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        
            
        GridView7.Visible = true;
        getbankdeatils();
     //   getgetbankpayandexcessamt();
        
    }
    public void GETUPDATE()
    {
        convdate();
        string LOCKSTSTUS;
        con = DB.GetConnection();
        LOCKSTSTUS = "SELECT *   FROM  SubAgnetpaymentdetails  WHERE PLANT_CODE='" + pcode + "'   AND  Frm_date='" + FDATE + "'     AND To_date='" + TODATE + "' AND  (UpdatedUser IS NULL or  UpdatedUser=0) ";
        SqlDataAdapter DSP = new SqlDataAdapter(LOCKSTSTUS, con);
        DataTable DTS = new DataTable();
        DSP.Fill(DTS);
        if (DTS.Rows.Count > 0)
        {
            GETID = 0;

        }
        else
        {
            GETID = 1;

        }


    }
    public void getbankdeatils()
    {
        //DateTime dt1 = new DateTime();
        //DateTime dt2 = new DateTime();
        //dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        //string d1 = dt1.ToString("MM/dd/yyyy");
        //string d2 = dt2.ToString("MM/dd/yyyy");
        convdate();

        con = DB.GetConnection();
        //   string getquery = "select Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id   from (select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount,Plant_Code,convert(varchar,Added_Date,103) as Date,Bank_id    from BankPaymentllotment  where plant_code='" + pcode + "'   and    Billfrmdate='" + d1 + "'  and Billtodate='" + d2 + "' AND  BankFileName='" + ddl_Addeddate.SelectedItem.Text + "'  and NetAmount > 0) as ff  left join (Select pmail,Milktype,Plant_Code as pcode   from Plant_Master   where plant_code='" + pcode + "' ) as pm on ff.Plant_Code= pm.pcode  order by Date asc";
        //  string getquery = "sELECT Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id,phone_Number  FROM (select Agent_Id,Agent_Name,Ifsccode,Account_no,convert(decimal(18,0),floor(NetAmount)) as NetAmount,Date,Pmail,Milktype,Plant_Code,Bank_Id   from (select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount,Plant_Code,convert(varchar,Added_Date,103) as Date,Bank_id    from BankPaymentllotment  where plant_code='" + pcode + "'   and    Billfrmdate='" + d1 + "'  and Billtodate='" + d2 + "' AND  BankFileName='" + ddl_Addeddate.SelectedItem.Text + "'  and NetAmount > 0) as ff  left join (Select pmail,Milktype,Plant_Code as pcode   from Plant_Master   where plant_code='" + pcode + "' ) as pm on ff.Plant_Code= pm.pcode   ) AS FF  LEFT JOIN (sELECT Plant_code AS amplantcode,Agent_Id as agent,phone_Number   FROM Agent_Master   WHERE plant_code='" + pcode + "' GROUP BY Plant_code,Agent_Id,phone_Number)  AS AM ON FF.Plant_Code=AM.amplantcode   and FF.Agent_Id=AM.agent";
        string getquery = "Select    hh.Agent_id,Agent_Name,ifsc_code,Agent_accountNo,Netamount,Date,Pmail,Milktype,HH.Plant_code,Bank_Name,phone_Number  from (Select Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Netamount,Date,ll.Agent_id,phone_Number,ll.Plant_code,Bank_Id  from (Select   Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,FLOOR(Sum(NEtamount)) as Netamount,Agent_id,Plant_code,convert(varchar,AddedDate,103) as Date  FROM SubAgnetpaymentdetails  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Agent_id='" + ddl_getagent.SelectedItem.Text+ "'  and Frm_date='"+FDATE+"'  and To_date='"+TODATE+"'  group by  Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Agent_id,Plant_code,AddedDate) as ll left join (Select Agent_Id,Bank_Id,phone_Number,Plant_code   from Agent_Master  where Plant_Code='"+ddl_Plantname.SelectedItem.Value+"' and Agent_id='"+ddl_getagent.SelectedItem.Value+"' group by Agent_Id,Bank_Id,phone_Number,Plant_code)as am on  ll.Plant_code=am.Plant_code) as hh  left join (Select Plant_Code,Plant_Name,Pmail,Milktype   from  Plant_Master where Plant_Code='"+ddl_Plantname.SelectedItem.Value+"' group by  Plant_Code,Plant_Name,Pmail,Milktype ) as pm on hh.Plant_code=pm.Plant_Code";
        SqlCommand cmd = new SqlCommand(getquery, con);
        SqlDataAdapter dsp = new SqlDataAdapter(cmd);
        DataTable getagentrows = new DataTable();
        getagentrows.Rows.Clear();
        dsp.Fill(getagentrows);
        showreport.Rows.Clear();
        showreport.Columns.Clear();
        showreport.Columns.Add("Report");
        foreach (DataRow drg in getagentrows.Rows)
        {
            string getagentid = drg[0].ToString();
            string Agent_Name = drg[1].ToString();
            string Ifsccode = drg[2].ToString();
            string Account_no = drg[3].ToString();
            double NetAmount = Convert.ToDouble(drg[4].ToString());
            string date = drg[5].ToString();
            string pmail = drg[6].ToString();
            string milktype = drg[7].ToString();
            string plcode = drg[8].ToString();
            string bankname= (drg[9]).ToString();
            string mobile = drg[10].ToString();
            string accountnu = ddl_kotack.SelectedItem.Value;
            string companyname = "Vyshnavi Dairy";
            //if (milktype == "1")
            //{
            //    ptype = "SVDSPL";
            //}
            //if (milktype == "2")
            //{
            //    ptype = "SVDPL";
            //}
            string pay = "RPAY";
            if (bankname == "Kotak Mahindra Bank")
            {
                paymode = "IFT";
            }
            if (bankname != "Kotak Mahindra Bank")
            {
                if (NetAmount > 200000)
                {
                    paymode = "RTGS";
                }
                else
                {
                    paymode = "NEFT";
                }
            }
            if ((accountnu == "425044000438") || (accountnu == "328044039913") || (accountnu == "334044049195") || (accountnu == "337044040029"))
            {
                ptype = "SVDSPL";

            } 
            if (accountnu == "334044032411")
            {
                ptype = "SVDPL";

            }
            DateTime dt = new DateTime();
            dt = System.DateTime.Now;
            string condate = dt.ToString("dd/MM/yyyy");
            string[] SPP = ddl_Plantname.SelectedItem.Text.Split('_');
            string LOCALPLANT = SPP[0].ToString();
            statement = ptype + "~" + pay + "~" + paymode + "~~" + condate + "~" + accountnu + "~" + NetAmount + "~~" + Agent_Name + "~" + Ifsccode + "~" + Account_no + "~" + pmail + "~" + mobile + "~" + LOCALPLANT + getagentid + "~" + companyname + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            showreport.Rows.Add(statement);
        }
        if (showreport.Rows.Count > 0)
        {
            GridView7.DataSource = showreport;
            GridView7.DataBind();

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        GETUPDATE();
        if (GETID == 0)
        {
            exportKotack();
            //  getupdatefilelock();

        }
        else
        {
            WebMsgBox.Show("File Already Updated");


        }
    }
    public void exportKotack()
    {

        string txt = string.Empty;

        //foreach (TableCell cell in GridView9.HeaderRow.Cells)
        //{       
        //    txt += cell.Text + "\t\t";
        //}

        //txt += "\r\n";

        foreach (GridViewRow row in GridView7.Rows)
        {
            //Making the space beween cells.
            foreach (TableCell cell in row.Cells)
            {
                //  txt += cell.Text + "\t\t";
                txt += cell.Text;
            }

            txt += "\r\n";
        }

        Response.Clear();
        Response.Buffer = true;
        //here you can give the name of file.
        //string FileName = pcode + txt_FromDate.Text + txt_ToDate.Text + DateTime.Now + ".txt";
        //Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.txt");
        //Response.AddHeader("content-disposition", "attachment;" + FileName);
        DateTime dt = new DateTime();
        dt = System.DateTime.Now;
        string DATEE = dt.ToString("ddMMyy");
        string timee = String.Format("{0:d/M/yyyy HH:mm:ss}", dt);
        string NAME = ddl_Plantname.SelectedItem.Value + ddl_getagent.SelectedItem.Text + DATEE + ".txt";
        Response.AddHeader("content-disposition", "attachment;filename= '" + NAME + "'");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        //FileStream fs = File.Create(txt);
        //File.SetAttributes(txt+".txt",FileAttributes.ReadOnly);
        Response.Flush();
        getupdatefilelock();
        Response.End();

    }
    public void getupdatefilelock()
    {
        //DateTime dt1 = new DateTime();
        //DateTime dt2 = new DateTime();
        //dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        //d1 = dt1.ToString("MM/dd/yyyy");
        //d2 = dt2.ToString("MM/dd/yyyy");
        convdate();
        con = DB.GetConnection();
        string strrtime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string getlock = "update  SubAgnetpaymentdetails set  UpdatedUser='" + 1 + "',UpdatedUserName='" + Session["Name"] + "',UpdatedTime='" + strrtime + "'   where plant_code='" + pcode + "'  and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  ";
        SqlCommand cmd = new SqlCommand(getlock, con);
        cmd.ExecuteNonQuery();
    }


}