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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TallyLoanJv : System.Web.UI.Page
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
    DataTable showreport = new DataTable();

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
    string FDATE1;
    string TODATE1;
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
    int datach;
    string Tallyno;
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
    //protected void btnInvoke_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(1000);
    //}
    protected void Button5_Click(object sender, EventArgs e)
    {
        //try
        //{


        //    ////System.Threading.Thread.Sleep(5000);
        //    CHECKDATA();

        //    if (datach == 0)
        //    {
        //        inserttojvtable();
        //        inserttojvgenerate();
        //    }
        //    else
        //    {


        //    }
        //}
        //catch
        //{

        //}


        try
        {


            ////System.Threading.Thread.Sleep(5000);
            CHECKDATA();

            if (datach == 0)
            {
                

            //    GETTID();

                inserttojvtable();
                //inserttojvgenerate();
                SHOWTALLYREPORT();
            }
            else
            {
                string mss = "Already Data Inserted";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

            }
        }
        catch
        {

        }
         
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + "'.xls");
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
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }

    public void CHECKDATA()
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

            string checkingdata = "SELECT  * FROM TallyloanEntryJvpass where plant_code='"+pcode+"' and Frm_date='" + ViewState["FDATE"] + "' and To_Date='" + ViewState["TODATE"] + "'";
            con = DB.GetConnection();
            SqlCommand CM = new SqlCommand(checkingdata, con);
            SqlDataAdapter DACM = new SqlDataAdapter(CM);
            DataTable dfc = new DataTable();
            DACM.Fill(dfc);
            if (dfc.Rows.Count > 0)
            {

                datach = 1;

            }
            else
            {
                datach = 0;

            }
        }
        catch
        {


        }

    }
    public void inserttojvtable()
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
             FDATE1 =   getvald  + "/" + getvalm  + "/" + getvaly;
             TODATE1 =    getvaldd  + "/" + getvalmm + "/" + getvalyy;

            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = FDATE1;
            ViewState["TODATE1"] = TODATE1;
            string amv = "";
            string narration;
            con = DB.GetConnection();


            //string amvv = "SELECT aa.Route_id,aa.Paid_Amount,rm.Route_Name   FROM (select    Route_id,SUM(Paid_Amount) as Paid_Amount,Plant_code  from Loan_Recovery where      plant_code='" + pcode + "' and Paid_date between '" + ViewState["FDATE"] + "' and '" + ViewState["TODATE"] + "'     group by Route_id,Plant_code)AS AA  LEFT JOIN (SELECT Route_ID,Route_Name,Plant_Code    FROM  Route_Master WHERE Plant_Code='" + pcode + "' group by Route_ID,Route_Name,Plant_Code) as rm on AA.Route_id=rm.Route_ID and aa.Plant_code=rm.Plant_Code   order by RAND(aa.Route_id) asc   ";
            //SqlCommand CMt = new SqlCommand(amvv, con);
            //SqlDataAdapter DACMt = new SqlDataAdapter(CMt);
            //DACMt.Fill(DTG, ("Getloantot"));

            //int roe = DTG.Tables[0].Rows.Count;

            //for (i = 0; i < roe; i++)
            //{



            //    string Routeid = DTG.Tables[0].Rows[j][0].ToString();
            //    string PaidAmount = DTG.Tables[0].Rows[j][1].ToString();
            //    ViewState["RouteName"] = DTG.Tables[0].Rows[j][2].ToString();
            ////    amv = "select    Agent_id,Loan_id,Paid_Amount,convert(varchar,Paid_date,101) as Paid_date,Route_id,plant_code  from Loan_Recovery where      plant_code='" + pcode + "' and Paid_date between '" + FDATE + "' and '" + TODATE + "'  and Route_id='" + Routeid + "'   order by RAND(Route_id) asc";

            //    //    amv = "select    Agent_id,Loan_id,Paid_Amount,convert(varchar,Paid_date,101) as Paid_date,Route_id,plant_code  from Loan_Recovery where      plant_code='" + pcode + "' and Paid_date between '" + FDATE + "' and '" + TODATE + "'  and Route_id='" + Routeid + "'   order by RAND(Route_id) asc";




               
                amv = "Select   Agent_id,Agent_Name,Paid_date,Plant_code,Loan_id,Paid_Amount,pname    from (Select  *  from  (select    Agent_id,Loan_id,Paid_Amount,convert(varchar,Paid_date,101) as Paid_date,Route_id,plant_code  from Loan_Recovery where     plant_code='" + pcode + "' and Paid_date between '" + FDATE + "' and '" + TODATE + "'   )  as news   left join (Select Agent_id AS amagentid,Agent_Name,Plant_code  as amplantcode   from agent_master where Plant_code='" + pcode + "' group by  Agent_id,Agent_Name,Plant_code) as  am on news.Plant_code=am.amplantcode  AND news.Agent_id=am.amagentid) AS BOTHLEFT LEFT JOIN (sELECT PLANT_CODE as pmplantcode,Plant_Name as pname    FROM Plant_Master    WHERE PLANT_CODE='" + pcode + "') as pmmaster on BOTHLEFT.Plant_code=pmmaster.pmplantcode";
                SqlCommand CM = new SqlCommand(amv, con);
                SqlDataAdapter DACM = new SqlDataAdapter(CM);
                DataTable insert = new DataTable();
                insert.Rows.Clear();
                DACM.Fill(insert);
                GETTID();
                foreach (DataRow sfg in insert.Rows)
                {
                   // Agent_id,Agent_Name,Paid_date,Plant_code,Loan_id,Paid_Amount,pname
                    string agentid = sfg[0].ToString();
                    string Agent_Name = sfg[1].ToString();
                    string Paid_date = sfg[2].ToString();
                    string Plant_code = sfg[3].ToString();
                    string Loan_id = sfg[4].ToString();
                      string PPaid_Amount = sfg[5].ToString();
                    double minusPaid_Amount = Convert.ToDouble(PPaid_Amount);
                    double MPaid_Amount= Convert.ToDouble( (-1) * (minusPaid_Amount) );
                    string MPaidAmount = MPaid_Amount.ToString("f2");
                    try
                    {
                     PPaid_Amount = sfg[5].ToString();
                     minusPaid_Amount = Convert.ToDouble(PPaid_Amount);
                     MPaid_Amount= Convert.ToDouble( (-1) * (minusPaid_Amount) );
                     MPaidAmount = MPaid_Amount.ToString("f2");
                    }
                    catch
                    {
                          MPaidAmount="0";

                    }
                    string pname = sfg[6].ToString();
                    string FrDATE = ViewState["FDATE"].ToString();
                    string TOwDATE = ViewState["TODATE"].ToString();
                    string insertby = Session["Name"].ToString();
                    string time = System.DateTime.Now.ToString();
                    string ledgername =  agentid +  "-" + Agent_Name + "-" +  pname;
                    string loanledgername =  agentid + "-" + Loan_id + "-" + Agent_Name + "-" + pname;
                     narration = "Being Loan Deduction For Period:" + ViewState["FDATE1"] + "TO:" + ViewState["TODATE1"];
                    string value;



                    //string Loan_id = sfg[1].ToString();
                    //string Paid_Amount = sfg[2].ToString();
                    //string Paid_date = sfg[3].ToString();
                    //string Route_id = sfg[4].ToString();
                    //string plant_code = sfg[5].ToString();
                    //string FrDATE = ViewState["FDATE"].ToString();
                    //string TOwDATE = ViewState["TODATE"].ToString();
                    //string insertby = Session["Name"].ToString();
                    //string time = System.DateTime.Now.ToString();
                    //string value;
                    con = DB.GetConnection();


                    if (ddl_Plantname.SelectedItem.Text == "ARANI")
                    {
                        Tallyno = "ARA";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "KAVERIPATNAM")
                    {
                        Tallyno = "KVP";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "GUDLUR")
                    {
                        Tallyno = "GUD";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "WALAJA")
                    {
                        Tallyno = "WAL";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "V_KOTA")
                    {
                        Tallyno = "VKT";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "RCPURAM")
                    {
                        Tallyno = "RCP";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "BOMMASAMUTHIRAM")
                    {
                        Tallyno = "BOMMA";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "TARIGONDA")
                    {
                        Tallyno = "TARI";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "CSPURAM")
                    {
                        Tallyno = "CSP";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "KONDEPI")
                    {
                        Tallyno = "KDP";

                    }

                    if (ddl_Plantname.SelectedItem.Text == "KAVALI")
                    {

                        Tallyno = "KVL";
                    }

                    if (ddl_Plantname.SelectedItem.Text == "GUDIPALLI PADU")
                    {

                        Tallyno = "GUDI";
                    }
                    if (ddl_Plantname.SelectedItem.Text == "KALIGIRI")
                    {
                        Tallyno = "KLG";

                    }
                    string gettp = ViewState["maxtid"] + Tallyno;
                    // value = "insert into TallyloanEntryJvpass(JVNO,TallyVchNo,Loan_id,Agent_id,Paid_Amount,Paid_date,Route_id,plant_code,Frm_Date,To_Date,EntryDate,InsertedBy,route_Name)values('" + ViewState["maxtid"] + "','" + gettp + "','" + Loan_id + "','" + agentid + "','" + Paid_Amount + "','" + Paid_date + "','" + Route_id + "','" + plant_code + "','" + FrDATE + "','" + TOwDATE + "','" + time + "','" + insertby + "','" + ViewState["RouteName"] + "') ";
                    value = "insert into TallyloanEntryJvpass(JVNO,TallyVchNo,Loan_id,Agent_id,Paid_Amount,Paid_date,plant_code,Frm_Date,To_Date,EntryDate,InsertedBy,ledgername,partialledgername,Narration)values ('" + ViewState["maxtid"] + "','" + gettp + "','" + Loan_id + "','" + agentid + "','" + PPaid_Amount + "','" + Paid_date + "','" + Plant_code + "','" + FrDATE + "','" + TOwDATE + "','" + time + "','" + insertby + "','" + ledgername + "','" + loanledgername + "','" + narration + "') ";
                     SqlCommand cmdknp = new SqlCommand(value, con);
                     cmdknp.ExecuteNonQuery();
                     value = "insert into TallyloanEntryJvpass(JVNO,TallyVchNo,Loan_id,Agent_id,Paid_Amount,Paid_date,plant_code,Frm_Date,To_Date,EntryDate,InsertedBy,ledgername,partialledgername,Narration)values ('" + ViewState["maxtid"] + "','" + gettp + "','" + Loan_id + "','" + agentid + "','" + (MPaid_Amount) + "','" + Paid_date + "','" + Plant_code + "','" + FrDATE + "','" + TOwDATE + "','" + time + "','" + insertby + "','" + loanledgername + "','" + ledgername + "','" + narration + "') ";
                     SqlCommand cmdknm = new SqlCommand(value, con);
                     cmdknm.ExecuteNonQuery();


                }
            //    j = j + 1;
            //    DTG.Tables[1].Rows.Clear();
            //}
            //}
        }
        catch
        {


        }

    }


    public void inserttojvgenerate1()
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

            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            string amv = "";
            con = DB.GetConnection();
            string amvv = "";
             amvv = "   select TallyVchNo ,REPLACE(CONVERT(VARCHAR(9), frm_date, 6), ' ', '-') AS Paid_date,route_name,Sum(paid_amount) as paid_amount,route_id,convert(varchar,frm_date,101) as frm_date   from TallyloanEntryJvpass    where   plant_code='" + pcode + "' and frm_date='" + ViewState["FDATE"] + "' and to_date='" + ViewState["TODATE"] + "'   group by route_id ,TallyVchNo,Jvno,Paid_date,route_name,frm_date  order by Jvno asc ";
            SqlCommand CMt = new SqlCommand(amvv, con);
            SqlDataAdapter DACMt = new SqlDataAdapter(CMt);
            DACMt.Fill(DTG, ("Getloantotttt"));

            roe1 = DTG.Tables[0].Rows.Count;

            reporting.Columns.Add("JVNo");
            reporting.Columns.Add("JVDate");
            reporting.Columns.Add("LedgerName");
            reporting.Columns.Add("Amount");
            reporting.Columns.Add("Narration");
            for (jj = 0; jj < roe1; j++)
            {

                string JVNo = DTG.Tables[0].Rows[jj][0].ToString();
                string JVDate = DTG.Tables[0].Rows[jj][1].ToString();
                string LedgerName = DTG.Tables[0].Rows[jj][2].ToString();
                string Amount = DTG.Tables[0].Rows[jj][3].ToString();
                string paidate = DTG.Tables[0].Rows[jj][5].ToString();
                string Routeid = DTG.Tables[0].Rows[jj][4].ToString();
                string Narration = "Being Loan Deduction For Period " + ViewState["FDATE1"] + "TO:" + ViewState["FDATE2"];
                ViewState["Narration"] = Narration;
                string routealter = LedgerName + "Route Milk Bills" + ddl_Plantname.SelectedItem.Text;
                reporting.Rows.Add(JVNo, JVDate, routealter, Amount, Narration);
                amv = "select TallyVchNo,agent_id,REPLACE(CONVERT(VARCHAR(9), paid_date, 6), ' ', '-') AS Paid_date,route_name,(paid_amount) as paid_amount,route_id,loan_id  from TallyloanEntryJvpass    where   plant_code='" + pcode + "' and route_id='" + Routeid + "' and paid_date='" + paidate + "' order by JVNo asc ";
                SqlCommand CM = new SqlCommand(amv, con);
                DataSet DTG11 = new DataSet();
                SqlDataAdapter DACM1 = new SqlDataAdapter(CM);
                DACM1.Fill(DTG11, ("GetAgentfinalloan1"));
                foreach (DataRow sfg in DTG11.Tables[0].Rows)
                {


                    string JVNo1 = DTG11.Tables[0].Rows[ju][0].ToString();
                    string agent1 = DTG11.Tables[0].Rows[ju][1].ToString();
                    string JVDate1 = DTG11.Tables[0].Rows[ju][2].ToString();
                    string LedgerName1 = DTG11.Tables[0].Rows[ju][3].ToString();
                    string Amount1 = DTG11.Tables[0].Rows[ju][4].ToString();
                    string loan_id = DTG11.Tables[0].Rows[ju][6].ToString();
                    double convamt = Convert.ToDouble(Amount1);
                    string Amount2 = ((convamt) * (-1)).ToString();
                    string Routeid1 = DTG11.Tables[0].Rows[ju][0].ToString();
                    string Narration1 = "CanNo:" + agent1 + "-" + loan_id + '-' + ddl_Plantname.SelectedItem.Text;
                    reporting.Rows.Add(JVNo, JVDate1, Narration1, Amount2, ViewState["Narration"]);

                    ju = ju + 1;
                }
                //DTG.Tables[2].Rows.Clear();
                //DTG.Tables[3].Rows.Clear();
                DTG11.Tables[0].Rows.Clear();
                jj = jj + 1;


                ju = 0;


            }
            GridView2.DataSource = reporting;
            GridView2.DataBind();
        }
        catch
        {

        }
    }


    public void inserttojvgenerate()
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

            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            string amv = "";
            con = DB.GetConnection();
            string amvv = "";
            amvv = "   select TallyVchNo ,REPLACE(CONVERT(VARCHAR(9), frm_date, 6), ' ', '-') AS Paid_date,route_name,Sum(paid_amount) as paid_amount,route_id,convert(varchar,frm_date,101) as frm_date   from TallyloanEntryJvpass    where   plant_code='" + pcode + "' and frm_date='" + ViewState["FDATE"] + "' and to_date='" + ViewState["TODATE"] + "'   group by route_id ,TallyVchNo,Jvno,Paid_date,route_name,frm_date  order by Jvno asc ";
            SqlCommand CMt = new SqlCommand(amvv, con);
            SqlDataAdapter DACMt = new SqlDataAdapter(CMt);
            DACMt.Fill(DTG, ("Getloantotttt"));

            roe1 = DTG.Tables[0].Rows.Count;

            reporting.Columns.Add("JVNo");
            reporting.Columns.Add("JVDate");
            reporting.Columns.Add("LedgerName");
            reporting.Columns.Add("Amount");
            reporting.Columns.Add("Narration");
            for (jj = 0; jj < roe1; j++)
            {

                string JVNo = DTG.Tables[2].Rows[jj][0].ToString();
                string JVDate = DTG.Tables[2].Rows[jj][1].ToString();
                string LedgerName = DTG.Tables[2].Rows[jj][2].ToString();
                string Amount = DTG.Tables[2].Rows[jj][3].ToString();
                string paidate = DTG.Tables[2].Rows[jj][5].ToString();
                string Routeid = DTG.Tables[2].Rows[jj][4].ToString();
                string Narration = "Being Loan Deduction For Period " + ViewState["FDATE1"] + "TO:" + ViewState["FDATE2"];
                ViewState["Narration"] = Narration;
                string routealter = LedgerName + "Route Milk Bills" + ddl_Plantname.SelectedItem.Text;
                reporting.Rows.Add(JVNo, JVDate, routealter, Amount, Narration);
                amv = "select TallyVchNo,agent_id,REPLACE(CONVERT(VARCHAR(9), paid_date, 6), ' ', '-') AS Paid_date,route_name,(paid_amount) as paid_amount,route_id,loan_id  from TallyloanEntryJvpass    where   plant_code='" + pcode + "' and route_id='" + Routeid + "' and paid_date='" + paidate + "' order by JVNo asc ";
                SqlCommand CM = new SqlCommand(amv, con);
                SqlDataAdapter DACM = new SqlDataAdapter(CM);

                DACM.Fill(DTG, ("GetAgentfinalloan"));
                foreach (DataRow sfg in DTG.Tables[3].Rows)
                {


                    string JVNo1 = DTG.Tables[3].Rows[ju][0].ToString();
                    string agent1 = DTG.Tables[3].Rows[ju][1].ToString();
                    string JVDate1 = DTG.Tables[3].Rows[ju][2].ToString();
                    string LedgerName1 = DTG.Tables[3].Rows[ju][3].ToString();
                    string Amount1 = DTG.Tables[3].Rows[ju][4].ToString();
                    string loan_id = DTG.Tables[3].Rows[ju][6].ToString();
                    double convamt = Convert.ToDouble(Amount1);
                    string Amount2 = ((convamt) * (-1)).ToString();
                    string Routeid1 = DTG.Tables[3].Rows[ju][0].ToString();
                    string Narration1 = "CanNo:" + agent1 + "-" + loan_id + '-' + ddl_Plantname.SelectedItem.Text;
                    reporting.Rows.Add(JVNo, JVDate1, Narration1, Amount2, ViewState["Narration"]);

                    ju = ju + 1;
                }
                //DTG.Tables[2].Rows.Clear();
                //DTG.Tables[3].Rows.Clear();
                DTG.Tables[3].Rows.Clear();
                jj = jj + 1;


                ju = 0;


            }
            GridView2.DataSource = reporting;
            GridView2.DataBind();
        }
        catch
        {

        }
    }


    public void GETTID()
    {
        try
        {
            con = DB.GetConnection();
            string tid = "Select max(JVNO)  as JVNO  from  TallyloanEntryJvpass WHERE plant_code='" + pcode + "' ";
            SqlCommand dmk = new SqlCommand(tid, con);
            SqlDataReader dr = dmk.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    int gettidd = Convert.ToInt32(dr["JVNO"]);
                    ViewState["maxtid"] = gettidd + 1;

                }

            }

        }
        catch
        {

            ViewState["maxtid"] = 1;

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
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + pcode + "'  order by  Bill_frmdate desc  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());


                    //FDATE = d1.ToString("MM/dd/yyyy");
                    //TODATE = d2.ToString("MM/dd/yyyy");
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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double amtchack = Convert.ToDouble(e.Row.Cells[3].Text);
                if (amtchack > 1)
                {

                    e.Row.BackColor = System.Drawing.Color.Cyan;

                }


            }
        }
        catch
        {

        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        //inserttojvgenerate1();
        SHOWTALLYREPORT();

    }
    public void SHOWTALLYREPORT()
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
        ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
        ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
        ViewState["FDATE"] = FDATE;
        ViewState["TODATE"] = TODATE;
        string GETREPORT = "";
        con = DB.GetConnection();
        GETREPORT = "select TallyVchNo as JVNo,REPLACE(CONVERT(VARCHAR(9), frm_date, 6), ' ', '-') AS JVDate,ledgername as LedgerName,Paid_Amount AS Amount, Narration as Narration  from TallyloanEntryJvpass    where   plant_code='" + pcode + "' and frm_date='" + FDATE + "'  and to_date='" + TODATE + "'  ORDER BY Tid   ASC";
        SqlCommand CMD = new SqlCommand(GETREPORT, con);
        SqlDataAdapter da=new SqlDataAdapter(CMD);
        showreport.Rows.Clear();
        da.Fill(showreport);
        if (showreport.Rows.Count > 0)
        {
            GridView2.DataSource = showreport;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = "NoRecords";
            GridView2.DataBind();


        }

    }
}