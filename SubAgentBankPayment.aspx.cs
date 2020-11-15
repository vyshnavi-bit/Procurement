using System;
using System.Collections;
using System.Configuration;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public partial class SubAgentBankPayment : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    SqlConnection con = new SqlConnection();
    public static int roleid;
    DateTime dtm = new DateTime();
    SqlDataReader dr;
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess = new DbHelper();
    DbHelper DB = new DbHelper();
    DataTable collectbank = new DataTable();
    DataTable collect = new DataTable();
    DataTable collectsubagent = new DataTable();
    double netamount;
    DataSet DTG = new DataSet();
    int datasetcount = 0;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DataTable agent = new DataTable();
    DataTable balan = new DataTable();
    double collectsumamt;
    double getcollectamt;
    double getbal;
    msg MESS = new msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //   managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                LoadPlantcode();
           
                Bdate();
                getagentid();
                GetSelectedRecords.Visible = false;
                Label11.Visible = false;
                avil.Visible = false;
                //
               
               
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
        if (IsPostBack == true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                //   managmobNo = Session["managmobNo"].ToString();

            
               
                // BindBankPaymentData();
                
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

           
        }
    }

    public void LoadPlantcode()
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


    public void loadsingleplant()
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
    private void Bdate()
    {
        try
        {
            dr = null;
            dr = Billdate(ccode,ddl_Plantname.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                }
            }
            else
            {
                txt_FromDate.Text = "10/10/1983";
                txt_ToDate.Text = "10/10/1983";
                WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }
    public SqlDataReader Billdate(string ccode, string pcode)
    {
        SqlDataReader dr;
        string sqlstr = string.Empty;
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND CurrentPaymentFlag='1' ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }


    //protected void BindDataTable()
    //{

    //    string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
    //    string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");
    //    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //    DataSet ds = new DataSet();
    //    SqlConnection con = new SqlConnection(connStr);
    //    con.Open();
    //    string sqlstr = "Select    Agent_id,Sum(NEtamount) as Netamount,Agent_Name,Bank_Name,ifsc_code,Agent_accountNo  FROM paymentdata  where  Plant_Code='"+ddl_Plantname.SelectedItem.Value+"'  and Agent_id='"++"'  and Frm_date='11-21-2016'  and To_date='11-30-2016'  group by  Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Agent_id";
    //    SqlDataAdapter adp = new SqlDataAdapter(sqlstr, con);
    //    adp.Fill(ds);

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddchkCountry.DataSource = ds.Tables[0];
    //        ddchkCountry.DataTextField = "Bank_Name";
    //        ddchkCountry.DataValueField = "Bank_ID";
    //        ddchkCountry.DataBind();
    //        //  ddchkCountry.Items.Add("0");
    //        ddchkCountry.Items.Add(new ListItem("CASH".ToString(), "0".ToString()));

    //    }
    //    //if (ddl_BankName.Items.Count > 0)
    //    //{
    //    //    //int cou = ddl_BankName.Items.Count;
    //    //    //cou = cou + 1;
    //    //    //ddl_BankName.Items[cou].Text = "CASH";
    //    //    //ddl_BankName.Items[cou].Value = "0";
    //    //    ddl_BankName.SelectedIndex = 0;
    //    //}
    //}



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

    public void getpaymentdata()
    {
        try
        {

           
            collect.Columns.Add("Agent_Name");
            collect.Columns.Add("Bank_Name");
            collect.Columns.Add("ifsc_code");
            collect.Columns.Add("Agent_accountNo");
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d11 = dt1.ToString("MM/dd/yyyy");
            string d12 = dt2.ToString("MM/dd/yyyy");
            string getagent;
            con = DB.GetConnection();
            getagent = "Select    Agent_id,FLOOR(Sum(NEtamount)) as Netamount,Agent_Name,Bank_Name,ifsc_code,Agent_accountNo  FROM paymentdata  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Agent_id='" + ddl_getagent.SelectedItem.Value + "'  and Frm_date='" + d11 + "'  and To_date='"+d12+"'  group by  Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Agent_id";
            SqlCommand cmd = new SqlCommand(getagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            collectbank.Rows.Clear();
            da.Fill(collectbank);
            string aid = collectbank.Rows[0][0].ToString();
            netamount = Convert.ToDouble(collectbank.Rows[0][1]);
            getbal =Convert.ToDouble(balan.Rows[0][0]);
            double final = (netamount - getbal);
            txt_milkamount.Text = final.ToString();
            string name = collectbank.Rows[0][2].ToString();
            string bankname = collectbank.Rows[0][3].ToString();
            string ifsc = collectbank.Rows[0][4].ToString();
            string ac = collectbank.Rows[0][5].ToString();
            collect.Rows.Add(name, bankname, ifsc, ac);
           
        }
        catch
        {
        }
    }


    public void getsuagentlist()
    {
        try
        {

            
            string subgetagent;
            con = DB.GetConnection();
            subgetagent = "select  agent_id,SubAgentname,Bank_name,ifsc_code,Accountno   from SubAgentMaster where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and agent_id='" + ddl_getagent.SelectedItem.Value + "' group by  agent_id,SubAgentname,Bank_name,Accountno,ifsc_code ";
            SqlCommand cmd = new SqlCommand(subgetagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            collectsubagent.Rows.Clear();
            da.Fill(collectsubagent);

            foreach (DataRow dvks in collectsubagent.Rows)
            {

                string aid =  dvks[0].ToString();      
                string name = dvks[1].ToString()  ; 
                string bankname = dvks[2].ToString()  ; 
                string ifsc =dvks[3].ToString();
                string ac = dvks[4].ToString();
                collect.Rows.Add(name, bankname, ifsc, ac);
            }

          
        }
        catch
        {
        }
    }


    public void getbala()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d11 = dt1.ToString("MM/dd/yyyy");
        string d12 = dt2.ToString("MM/dd/yyyy");
        string getbalan;
        con = DB.GetConnection();
        getbalan = "Select isnull(Sum(netamount),0) as amount from SubAgnetpaymentdetails  where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and Frm_date='" + d11 + "'  and To_date='" + d12 + "'    and agent_id='" + ddl_getagent.SelectedItem.Value + "'";
        SqlCommand cmd=new SqlCommand(getbalan,con);
        SqlDataAdapter dfs = new SqlDataAdapter(cmd);
        dfs.Fill(balan);
    }

   protected void btn_load_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        getagentid();
        Bdate();
    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_Check_Click(object sender, EventArgs e)
    {

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Bdate();
        getbala();
        getpaymentdata();
        getsuagentlist();
        GridView1.DataSource = collect;
        GridView1.DataBind();
        GetSelectedRecords.Visible = false;
        Label11.Visible = false;
        avil.Visible = false;
        Label11.Text = "";
    }
    protected void GetSelectedRecords_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (GridViewRow row in GridView1.Rows)
            {
                DateTime DF = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                DateTime DTTK = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
                string d11 = DF.ToString("MM/dd/yyyy");
                string d12 = DTTK.ToString("MM/dd/yyyy");
                double textamt;
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        try
                        {
                            TextBox txt = (TextBox)row.FindControl("txtchkamount");
                            string txt1 = txt.Text;
                            textamt = Convert.ToDouble(txt1);
                        }
                        catch
                        {

                            textamt = 0;

                        }
                        //string aid = row.Cells[0].ToString();
                        string name = row.Cells[1].Text;
                        string bankname = row.Cells[2].Text;
                        string ifsc = row.Cells[3].Text;
                        string ac = row.Cells[4].Text;
                        string strinsert;
                        con = DB.GetConnection();
                        strinsert = "Insert Into SubAgnetpaymentdetails(Agent_id,Plant_code,Frm_Date,To_Date,Netamount,Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,AddedDate,AddedUser)values('" + ddl_getagent.SelectedItem.Value + "','" + ddl_Plantname.SelectedItem.Value + "','" + d11 + "','" + d12 + "','" + textamt + "','" + name + "','" + bankname + "','" + ifsc + "','" + ac + "','" + System.DateTime.Now + "','" + Session["Name"].ToString() + "')";
                        SqlCommand cmd = new SqlCommand(strinsert, con);
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            getbala();
            getpaymentdata();
            getsuagentlist();
            GridView1.DataSource = collect;
            GridView1.DataBind();
            Label11.Visible = false;
            avil.Visible = false;
            GetSelectedRecords.Visible = false;
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.save) + "')</script>");
        }
        catch
        {

        }
           
    }
    protected void ddchkCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txt_tot_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnchenck_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {

            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    netamount = Convert.ToDouble(txt_milkamount.Text);
                    double textamt;
                    try
                    {
                        TextBox txt = (TextBox)row.FindControl("txtchkamount");
                        string txt1 = txt.Text;
                        textamt = Convert.ToDouble(txt1);
                    }
                    catch
                    {

                        textamt = 0;

                    }
                    getcollectamt = textamt;
                    collectsumamt = collectsumamt + getcollectamt;
                    Label11.Text ="Selected Amount:"+ collectsumamt.ToString();
                    string str =( netamount - collectsumamt).ToString();
                    avil.Text = "Available Balance:" + str;

                }
            }
        }

        if (netamount >= collectsumamt)
        {
            if ((netamount != 0) && (collectsumamt != 0))
            {
                GetSelectedRecords.Visible = true;
                Label11.Visible = true;
                avil.Visible = true;
            }
            else
            {
                GetSelectedRecords.Visible = false;
                Label11.Visible = false;
                avil.Visible = false;

            }
        }
        else
        {
            GetSelectedRecords.Visible = false;
            Label11.Visible = false;
            avil.Visible = false;
        }
    }
    protected void ddl_getagent_SelectedIndexChanged(object sender, EventArgs e)
    {
        getbala();
        getpaymentdata();
        getsuagentlist();
        GridView1.DataSource = collect;
        GridView1.DataBind();
        GetSelectedRecords.Visible = false;
        Label11.Visible = false;
        avil.Visible = false;
        Label11.Text = "";
    }
}