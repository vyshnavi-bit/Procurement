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

public partial class DpuBankPaymentAllotment : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;

    public int rid;
    public int flag = 0;
    public int flag1 = 0;

    SqlDataReader dr;
    BLLuser Bllusers = new BLLuser();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    DateTime dtm = new DateTime();
    DbHelper dbaccess = new DbHelper();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    string[] getbankid1;
    string get;
    int msgcount;
    string producercode;
    double checkamount;
    double loopingamount;
    int msgvalue;
    string amount;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
            //    managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;


                LoadPlantcode();
                GetPlantAllottedAmount();
                if (checkfile.Checked == true)
                {
                    ddl_oldfileName.Visible = true;
                    txt_fileName.Visible = false;
                    Plantname17.Visible = false;
                    Plantname18.Visible = true;
                }
                else
                {
                    ddl_oldfileName.Visible = false;
                    txt_fileName.Visible = true;
                    Plantname17.Visible = true;
                    Plantname18.Visible = false;

                }
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
             //   managmobNo = Session["managmobNo"].ToString();

                pcode = ddl_Plantcode.SelectedItem.Value;
                //   rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                // BindBankPaymentData();
                if (checkfile.Checked == true)
                {
                    ddl_oldfileName.Visible = true;
                    txt_fileName.Visible = false;
                    Plantname17.Visible = false;
                }
                else
                {
                    ddl_oldfileName.Visible = false;
                    txt_fileName.Visible = true;
                    Plantname17.Visible = true;

                }
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    private void GetPlantAllottedAmount()
    {
        try
        {
            pcode = ddl_Plantcode.SelectedItem.Value;
            string df = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string dt = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

   
    
     //   string sqlstr = "SELECT t1.*,t2.*,ISNULL((ISNULL(t1.TotAllotAmount,0)-ISNULL(t2.TotPaymentAmount,0)),0) AS TotAmount FROM (SELECT plant_code,ISNULL(SUM(Amount),0) AS TotAllotAmount FROM DpuAdminAmountAllottoplant Where Plant_code='" + pcode + "' AND  Amount>0  and  billfrmdate='" + fd + "'  and Billtodate='" + td + "'  GROUP By plant_code)AS t1 LEFT JOIN (SELECT ISNULL(plant_code,0) AS Bpcode,ISNUll(SUM(NetAmount),0) AS TotPaymentAmount  FROM DpuBankPaymentllotment Where Plant_Code='" + pcode + "' and Billfrmdate='"+fd+"' and Billtodate='"+td+"'  GROUP By plant_code) AS t2 ON t1.plant_code=t2.Bpcode ";

            string sqlstr = "select Plantcode,ISNULL(SUM(Amount-netamount),0) as amount from (select sum(Amount) as  Amount,Plant_code as pcode   from DpuAdminAmountAllotToPlant  where plant_code='" + pcode + "'   and Billfrmdate='"+df+"'  and  Billtodate='"+dt+"' group by Plant_code ) as a left join (select sum(NetAmount) as  NetAmount,Plant_code as plantcode   from DpuBankPaymentllotment  where plant_code='" + pcode + "'   and Billfrmdate='" + df + "'  and  Billtodate='" + dt + "' group by  Plant_code )as b on a.pcode=b.plantcode  group by Plantcode ";
      

            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd=new SqlCommand(sqlstr,con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

           
           
                while (dr.Read())
                {
                    double getval = Convert.ToDouble(dr["amount"].ToString());
                    txt_Allotamount.Text = getval.ToString("F2");
                }
            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    //protected void btn_load_Click(object sender, EventArgs e)
    //{
    //    //rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
    //    //BindBankPaymentData();
    //    //txt_balance.Text = "";
    //    rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
    //    BindBank();
    //    txt_balance.Text = "";
    //}
    //private void LoadBankId()
    //{
    //    try
    //    {
    //        DataSet ds = null;
    //        //string sqlstr = "SELECT Bank_ID,UPPER(CONVERT(Nvarchar(8),Bank_ID)+'_'+Bank_Name) AS Bank_Name  FROM Bank_Details WHERE Company_code='" + ccode + "'  ORDER BY Bank_ID";
    //        string sqlstr = "SELECT Distinct(Bank_Id),Bank_Name FROM (SELECT Bank_ID,UPPER(CONVERT(Nvarchar(8),Bank_ID)+'_'+Bank_Name) AS Bank_Name  FROM Bank_Details WHERE Company_code='" + ccode + "' ) AS BD INNER JOIN (SELECT Bank_Id AS Bid FROM Agent_Master Where Plant_code='" + pcode + "' ) AS AM ON BD.Bank_id=AM.Bid ORDER BY BD.Bank_id";
    //        ds = dbaccess.GetDataset(sqlstr);
    //        ddl_BankName.Items.Clear();
    //        ddchkCountry.Items.Clear();
    //        if (ds != null)
    //        {
    //            ddl_BankName.DataSource = ds;
    //            ddl_BankName.DataTextField = "Bank_Name";
    //            ddl_BankName.DataValueField = "Bank_ID";
    //            ddl_BankName.DataBind();


    //            ddchkCountry.DataSource = ds;
    //            ddchkCountry.DataTextField = "Bank_Name";
    //            ddchkCountry.DataValueField = "Bank_ID";
    //            ddchkCountry.DataBind();
    //            //  ddchkCountry.Items.Add("0");
    //            ddchkCountry.Items.Add(new ListItem("CASH".ToString(), "0".ToString()));

    //        }
    //        if (ddl_BankName.Items.Count > 0)
    //        {
    //            //int cou = ddl_BankName.Items.Count;
    //            //cou = cou + 1;
    //            //ddl_BankName.Items[cou].Text = "CASH";
    //            //ddl_BankName.Items[cou].Value = "0";
    //            ddl_BankName.SelectedIndex = 0;
    //        }

    //    }
    //    catch (Exception em)
    //    {

    //    }
    //}

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            // else
            //{
            //    ddl_Plantname.Items.Add("--Select PlantName--");
            //}
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


   

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Allotamount.Text = "";
        //     txt_balance.Text = "";
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        Bdate();
        Totamount();
        TotMilkamount();
        Totallotamount();
        GetPlantAllottedAmount();
        Totbanbkallotamount();
        BindData();
        loadagentid();

        //  Loadoldfilename();




    }


    protected void BindData()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(connStr);
        con.Open();
        string sqlstr = "SELECT Distinct(Bank_Id),Bank_Name FROM (SELECT Bank_ID,UPPER(CONVERT(Nvarchar(8),Bank_ID)+'_'+Bank_Name) AS Bank_Name  FROM Bank_Details WHERE Company_code='" + ccode + "' ) AS BD INNER JOIN (SELECT Bank_Id AS Bid FROM DPUPRODUCERMASTER Where Plant_code='" + pcode + "' ) AS AM ON BD.Bank_id=AM.Bid ORDER BY BD.Bank_id";
        SqlDataAdapter adp = new SqlDataAdapter(sqlstr, con);
        adp.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddchkCountry.DataSource = ds.Tables[0];
            ddchkCountry.DataTextField = "Bank_Name";
            ddchkCountry.DataValueField = "Bank_ID";
            ddchkCountry.DataBind();


            //  ddchkCountry.Items.Add("0");
            ddchkCountry.Items.Add(new ListItem("CASH".ToString(), "0".ToString()));

        }
        //if (ddl_BankName.Items.Count > 0)
        //{
        //    //int cou = ddl_BankName.Items.Count;
        //    //cou = cou + 1;
        //    //ddl_BankName.Items[cou].Text = "CASH";
        //    //ddl_BankName.Items[cou].Value = "0";
        //    ddl_BankName.SelectedIndex = 0;
        //}
    }


 
    protected void Chk_Cash_CheckedChanged(object sender, EventArgs e)
    {
        //AllCash();
        //rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        ////BindBankPaymentData();
        //  BindBank();

    }




    protected void ddchkCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_updatestatus_Click(object sender, EventArgs e)
    {
        //  updatefilestatus();
    }



    protected void btn_delete_Click(object sender, EventArgs e)
    {
        //DeleteProblemFiles();
        //Loadoldfilename();
    }
    protected void ddl_oldfilename_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //List<String> CountryID_list = new List<string>();
        //List<String> CountryName_list = new List<string>();

        Label11.Text = "";

        int j = 0;

     
        //  string str;
        for (int i = 0; i < ddchkCountry.Items.Count; i++)
        {

            if (i == 0)
            {
                get = "";
            }

            
            if (ddchkCountry.Items[i].Selected == true)
            {
                Label11.Text = ddchkCountry.Items[i].Text + "<br />";
                string stt = Label11.Text.ToString();
                //Regex rgx = new Regex("[^a-zA-Z]");
                //string  str = rgx.Replace(stt, ",");
                // string getbankid = stt.Substring(0, stt.LastIndexOf('</br>'));
                 getbankid1 = stt.Split('_');



                 get  =  getbankid1[0].ToString()  + ","  +  get  ;
                 txt_fileName123.Text = get.ToString().TrimEnd(',');
                 string gesplit = txt_fileName123.Text;

                 string res = Regex.Replace(gesplit, "CASH<br />", "0");
                 txt_fileName123.Text = res.ToString();

            }

         



        }

        gridview();

    }
   
    public void getproduceramount()
    {



    }



    public void gridview()
    {

        try
        {
            DateTime dt1 = new DateTime();
            string d1 = dt1.ToString("MM/dd/yyyy");

            //string getfdate = txt_FromDate.Text + "00:00:00.000";
            //string gettdate = txt_ToDate.Text + "00:00:00.000";

             string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
             string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

             string getfdate = d11 + " "+ "00:00:00.000";
             string gettdate = d12 + " " + "00:00:00.000";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string assing = txt_fileName123.Text;
             //   string sqlstr = "select Bank_Id,Bank_name,Agent_Id,Ifsc_code,Agent_AccountNo,Producer_Name,Producer_Code,Amount from(select Bank_Id,Bank_name,Agent_Id as AgentId,Ifsc_code,Agent_AccountNo,Producer_Code,agent_id,Producer_Name   from DPUPRODUCERMASTER where Plant_code='" + pcode + "'  and Bank_Id in (" + assing + ") group by Bank_Id,Bank_name,Producer_Code,Producer_Name,Agent_AccountNo,Agent_Id,Ifsc_code )  as A  left join (select SUM(Amount) as Amount,Producer_Id    from ProducerProcurement  where Plant_code='" + pcode + "'  and Amount > 0  and  payflag=1 and Bankstatus=0  and  ((prdate between '" + d11 + "'  and '" + d12 + "') or (prdate between '" + getfdate + "'  and '" + gettdate + "'))  group by Producer_Id) as  b on A.Producer_Code=b.Producer_Id   where Amount is not null  order by Producer_Code asc ";
                string sqlstr = "select Bank_Id,Bank_name,Agent_Id,Ifsc_code,Agent_AccountNo,Producer_Name,Producer_Code,Amount from(select Bank_Id,Bank_name,Agent_Id as AgentId,Ifsc_code,Agent_AccountNo,Producer_Code,agent_id,Producer_Name   from DPUPRODUCERMASTER where Plant_code='" + pcode + "' and  Agent_Id='" + ddl_getagent.Text + "'  and Bank_Id in (" + assing + ") group by Bank_Id,Bank_name,Producer_Code,Producer_Name,Agent_AccountNo,Agent_Id,Ifsc_code )  as A  left join (select SUM(Amount) as Amount,Producer_Id    from ProducerProcurement  where Plant_code='" + pcode + "'  and Amount > 0         and ((Bankstatus=0 )or  (Bankstatus is null))   and  Agent_Id='" + ddl_getagent.Text + "' and  ((prdate between '" + d11 + "'  and '" + d12 + "') or (prdate between '" + getfdate + "'  and '" + gettdate + "'))  group by Producer_Id) as  b on A.Producer_Code=b.Producer_Id   where Amount is not null  order by Producer_Code asc ";
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    //GridView1.Columns[1].Visible = false;
                    //GridView1.Columns[2].Visible = false;
                    //GridView1.Columns[3].Visible = false;
                    //GridView1.Columns[4].Visible = false;

                    GridView1.FooterRow.Cells[5].Text = "Total Amount";
                    double billadv = dt.AsEnumerable().Sum(row => row.Field<double>("Amount"));
                    GridView1.FooterRow.HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[8].Text = billadv.ToString("N2");



                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                  

                   
                }



            }
        }
        catch
        {



        }
    }


    protected void GetSelectedRecords_Click(object sender, EventArgs e)
    {


try
{
         string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
             SqlConnection  con=new SqlConnection(connStr);

        if ((txt_fileName.Text != string.Empty) || (ddl_oldfileName.Text != string.Empty))
        {
            Bdate();

            Totbanbkallotamount();

         //   update();

            //   DataTable dt = new DataTable();
            //   dt.Columns.AddRange(new DataColumn[4] { new DataColumn("BankId"), new DataColumn("Bankname"), new DataColumn("Accountnumber"), new DataColumn("Produce_Name"), new DataColumn("ProducerCode"), new DataColumn("Amount") });
            foreach (GridViewRow row in GridView1.Rows)
            {
                string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
                string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string bankid = row.Cells[1].Text;
                        string bankname = row.Cells[2].Text;
                        string agentid = row.Cells[3].Text;
                        string ifsc = row.Cells[4].Text;
                        string accountno = row.Cells[5].Text;
                        string prodecername = row.Cells[6].Text;
                        producercode = row.Cells[7].Text;
                      //  string amount = row.Cells[8].Text;



                        string[] amount12 = row.Cells[8].Text.Split('.');
                        amount = amount12[0].ToString() + ".00";

                        string frmdate = d11;
                        string todate = d12;

                        string assdate = System.DateTime.Now.ToString();
                        string ADDDATE = Convert.ToDateTime(assdate).ToString("MM/dd/yyy");


                        string str;

                        double assignamount = Convert.ToDouble(amount);

                        checkamount = Convert.ToDouble(txt_Assignamount.Text);
                        checkamount = checkamount + assignamount;
                        double assign = Convert.ToDouble(txt_tot.Text);
                        con.Close();
                        if (checkamount <= assign)
                        {
                            msgvalue = 5; //lessthan Amount
                        }
                        else
                        {
                            msgvalue = 1;

                            msgbox();

                        }
                        // dt.Rows.Add(bankid, bankname, prodecername, producercode, amuount);
                    }
                }
            }

            if (msgvalue == 5)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
                    string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");


                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string bankid = row.Cells[1].Text;
                            string bankname = row.Cells[2].Text;
                            string agentid = row.Cells[3].Text;
                            string ifsc = row.Cells[4].Text;
                            string accountno = row.Cells[5].Text;
                            string prodecername = row.Cells[6].Text;
                            producercode = row.Cells[7].Text;
                          //  string amount = row.Cells[8].Text;


                            string[] amount12 = row.Cells[8].Text.Split('.');
                            amount = amount12[0].ToString() + ".00";
                            string frmdate = d11;
                            string todate = d12;

                            string assdate = System.DateTime.Now.ToString();
                            string ADDDATE = Convert.ToDateTime(assdate).ToString("MM/dd/yyy");


                            string str;

                            double assignamount = Convert.ToDouble(amount);
                            checkamount = checkamount + assignamount;
                            double assign = Convert.ToDouble(txt_tot.Text);
                            con.Close();
                            //if (checkamount < assign)
                            //{
                                if (checkfile.Checked != true)
                                {
                                    con.Open();
                                    str = "insert into DpuBankPaymentllotment(company_code,plant_code,added_date,agent_id,account_no,ifsccode,netamount,pstatus,billfrmdate,billtodate,agent_name,bank_id,bankfilename) values('" + ccode + "','" + pcode + "','" + ADDDATE + "','" + producercode + "','" + accountno + "','" + ifsc + "','" + amount + "','1','" + frmdate + "','" + todate + "','" + prodecername + "','" + bankid + "','" + txt_fileName.Text + "')";
                                    SqlCommand cmd = new SqlCommand(str, con);
                                    cmd.ExecuteNonQuery();
                                    update1();
                                }
                                if (checkfile.Checked == true)
                                {
                                    con.Open();
                                    str = "insert into DpuBankPaymentllotment(company_code,plant_code,added_date,agent_id,account_no,ifsccode,netamount,pstatus,billfrmdate,billtodate,agent_name,bank_id,bankfilename) values('" + ccode + "','" + pcode + "','" + ADDDATE + "','" + producercode + "','" + accountno + "','" + ifsc + "','" + amount + "','1','" + frmdate + "','" + todate + "','" + prodecername + "','" + bankid + "','" + ddl_oldfileName.Text + "')";
                                    SqlCommand cmd = new SqlCommand(str, con);
                                    cmd.ExecuteNonQuery();
                                    update1();
                                }


                                msgvalue = 2;

                                msgbox();

                            //}
                            //else
                            //{
                            //    msgvalue = 1;

                            //    msgbox();

                            //}
                            // dt.Rows.Add(bankid, bankname, prodecername, producercode, amuount);
                        }
                    }
                }
            }

            else
            {
                msgvalue = 1;

                msgbox();


            }
           
           // update1();
        }
        GetPlantAllottedAmount();
        BindData();
        gridview();
        Totamount();
        Totallotamount();
        Totbanbkallotamount();
        //GridView2.DataSource = dt;
        //GridView2.DataBind();
       }
catch (Exception ex)
{

}

    }
    private int FileNamecheckstatus()
    {
        int count = 0;
        string sqlstr = string.Empty;
        using (SqlConnection conn = new SqlConnection(connStr))
        {

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

            string getfdate = d11 + " " + "00:00:00.000";
            string gettdate = d12 + " " + "00:00:00.000";
            if (checkfile.Checked == true)
            {

            }
            else
            {

                sqlstr = "SELECT * FROM DpuBankPaymentllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' and  BankFileName='" + txt_fileName.Text.ToString().Trim() + "' and    ((Billfrmdate='" + d11 + "' and Billtodate='" + d12 + "')  OR (Billfrmdate='" + getfdate + "' and Billtodate='" + gettdate + "')) ";
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    msgbox();
                    txt_fileName.Text = "";
                    txt_fileName.Focus();


                }
            }

            count = dbaccess.ExecuteScalarint(sqlstr);
            return count;
        }
    }
    private void Bdate()
    {
        try
        {
            string sqlstr;
            sqlstr = "select Bill_frmdate,Bill_todate  from Bill_date where Plant_Code='" + pcode + "' and CurrentPaymentFlag=1";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd=new SqlCommand(sqlstr,con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text =dr["Bill_frmdate"].ToString();
                    txt_ToDate.Text = dr["Bill_todate"].ToString();
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


    private void loadagentid()
    {
        try
        {
            string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");
            ddl_getagent.Items.Clear();
            string sqlstr;
            sqlstr = "select   distinct(agent_id) as  agent_id   from   DPUPRODUCERMASTER   where   Plant_code='" + pcode + "'order by agent_id asc  ";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   ddl_getagent.Items.Add(dr["agent_id"].ToString());
                   
                }
            }
            else
            {
                //txt_FromDate.Text = "10/10/1983";
                //txt_ToDate.Text = "10/10/1983";
                //WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }




    private void Totamount()
    {
        try
        {
            string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

            string sqlstr;
            sqlstr = "select   sum(Amount) as  Amount   from   DpuAdminAmountAllotToPlant   where   Plant_code='"+pcode+"' and Billfrmdate='"+d11+"' and Billtodate='"+d12+"'  ";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   double get =Convert.ToDouble( dr["Amount"].ToString());
                   txt_tot.Text = get.ToString("F2");
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

    private void TotMilkamount()
    {
        try
        {
            string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

            string sqlstr;
            sqlstr = "select SUM(Amount) as Amount   from  producerprocurement   where plant_code='"+pcode+"'   and prdate between '"+d11+"' and '"+d12+"' ";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                     double get = Convert.ToDouble(dr["Amount"].ToString());
                     txt_milkamount.Text = get.ToString("F2");
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

    private void Totallotamount()
    {
        try
        {
            string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

            string sqlstr;
            sqlstr = "select sum(Amount) as Amount  from  DpuAdminAmountAllotToPlant  where plant_code='" + pcode + "'   and Billfrmdate='" + d11 + "'  and  Billtodate='"+d12+"'";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    double get = Convert.ToDouble(dr["Amount"].ToString());
                    txt_tot.Text  = get.ToString("F2");
                   
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
    private void Totbanbkallotamount()
    {
        try
        {
            string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

            string sqlstr;
            sqlstr = "select ISNULL(sum(NETAMOUNT),0) as Amount  from  DpuBankPaymentllotment  where plant_code='" + pcode + "'   and Billfrmdate='" + d11 + "'  and  Billtodate='" + d12 + "'";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   checkamount =Convert.ToDouble(dr["Amount"].ToString());

                   double get = Convert.ToDouble(dr["Amount"].ToString());

                   
                   txt_Assignamount.Text = get.ToString("F2");

                }
            }
            else
            {
                checkamount = 0.0;
                txt_FromDate.Text = "10/10/1983";
                txt_ToDate.Text = "10/10/1983";
                WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }

    //private void Totalassignamount() /*   inserted bankpaymnet table   */
    //{
    //    try
    //    {
    //        string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
    //        string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

    //        string sqlstr;
    //        sqlstr = "select sum(Amount) as Amount  from  DpuBankPaymentllotment  where plant_code='" + pcode + "'   and Billfrmdate='" + d11 + "'  and  Billtodate='" + d12 + "'";
    //        SqlConnection con = new SqlConnection(connStr);
    //        con.Open();
    //        SqlCommand cmd = new SqlCommand(sqlstr, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                checkamount = Convert.ToDouble(dr["Amount"].ToString());
                   
    //            }
    //        }
    //        else
    //        {
    //            checkamount = 0.0;
    //            txt_FromDate.Text = "10/10/1983";
    //            txt_ToDate.Text = "10/10/1983";
    //            WebMsgBox.Show("Please Select the Bill_Date");
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}



    private void getfilename()
    {
        try
        {
            string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

            string getfdate = d11 + " " + "00:00:00.000";
            string gettdate = d12 + " " + "00:00:00.000";
            string sqlstr;
            sqlstr = "Select  distinct(BankFileName) as BankFileName   from  dpuBankPaymentllotment WHERE Plant_Code='" + pcode + "' and  ((Billfrmdate='" + d11 + "' and Billtodate='" + d12 + "') or  (Billfrmdate='" + getfdate + "' and Billtodate='" + gettdate + "'))  and PStatus=1";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_oldfileName.Items.Add(dr["BankFileName"].ToString());
                }
            }
            else
            {
               
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void chec(object sender, EventArgs e)
    {

    }
    protected void checkfile_CheckedChanged(object sender, EventArgs e)
    {
        if (checkfile.Checked == true)
        {
            getfilename();
            txt_fileName.Visible = false;
            Plantname17.Visible=false;
            Plantname18.Visible = true;
        }
        else
        {

            txt_fileName.Visible = true;
            Plantname17.Visible = true;
            Plantname18.Visible = false;
        }
    }
    protected void txt_fileName_TextChanged(object sender, EventArgs e)
    {
        FileNamecheckstatus();
       
    }

    public void msgbox()
    {
        string message;

        if (msgvalue == 1)
        {

            message = "Your Check your Alloted Amount";
        }
        else
        {
            message = "FilenameAlready Available.";
        }
        if (msgvalue == 2)
        {
            message = "Record Inserted SuccessFully";

        }
                
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


      



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
        }
       

    }


    //public void update()
    //{
    //    string sqlstr, sqlstr1;

    //    sqlstr = "update     dpuBankPaymentllotment set  PStatus=0 WHERE Plant_Code='" + pcode + "' and  Billfrmdate not in('" + txt_FromDate.Text + "') and Billtodate not in('" + txt_ToDate.Text + "')  ";
    //    SqlConnection con = new SqlConnection(connStr);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand(sqlstr, con);
    //    cmd.ExecuteNonQuery();
    //    sqlstr1 = "update  ProducerProcurement  set Bankstatus=1 WHERE Plant_Code='" + pcode + "'  and  payflag=1 and prdate between '" + txt_FromDate.Text + "'  and '" + txt_ToDate.Text + "' and Bankstatus=0 ";
    //    SqlCommand cmd1 = new SqlCommand(sqlstr1, con);
    //    cmd1.ExecuteNonQuery();
    //}

    //public void update()
    //{
    //    string sqlstr, sqlstr1;

    //    sqlstr = "update     dpuBankPaymentllotment set  PStatus=0 WHERE Plant_Code='" + pcode + "' and  Billfrmdate not in('" + txt_FromDate.Text + "') and Billtodate not in('" + txt_ToDate.Text + "')  ";
    //    SqlConnection con = new SqlConnection(connStr);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand(sqlstr, con);
    //    //cmd.ExecuteNonQuery();
    //    //sqlstr1 = "update  ProducerProcurement  set Bankstatus=1 WHERE Plant_Code='" + pcode + "'  and  payflag=1 and prdate between '" + txt_FromDate.Text + "'  and '" + txt_ToDate.Text + "' and Bankstatus=0 ";
    //    //SqlCommand cmd1 = new SqlCommand(sqlstr1, con);
    //    //cmd1.ExecuteNonQuery();
    //}


    public void update1()
    {

        string sqlstr, sqlstr1;

        string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
        string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

        string getfdate = d11 +" "+ "00:00:00.000";
        string gettdate = d12 +" "+ "00:00:00.000";
        sqlstr = "update     dpuBankPaymentllotment set  PStatus=0 WHERE Plant_Code='" + pcode + "' and (( Billfrmdate not in('" + txt_FromDate.Text + "') and Billtodate not in('" + txt_ToDate.Text + "') or ( Billfrmdate not in('" + txt_FromDate.Text + "') and Billtodate not in('" + txt_ToDate.Text + "'))  and  PStatus not in(1) and agent_id='" + producercode + "'  ";
        SqlConnection con = new SqlConnection(connStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sqlstr, con);

        sqlstr1 = "update  ProducerProcurement  set Bankstatus=1 WHERE Plant_Code='" + pcode + "'   and ((prdate between '" + d11 + "'  and '" + d12 + "') or (prdate between '" + getfdate + "'  and '" + gettdate + "')) and   Producer_Id='" + producercode + "' and  ((Bankstatus=0) or (Bankstatus is null)) ";
        SqlCommand cmd1 = new SqlCommand(sqlstr1, con);
        cmd1.ExecuteNonQuery();
    }

    //public void update1()
    //{
    //    string sqlstr, sqlstr1;
    //    SqlConnection con = new SqlConnection(connStr);
    //    con.Open();
    //    sqlstr1 = "update  ProducerProcurement  set bankstatus=1 WHERE Plant_Code='" + pcode + "'  and  payflag=1";
    //    SqlCommand cmd1 = new SqlCommand(sqlstr1, con);
    //    cmd1.ExecuteNonQuery();
    //}
    protected void txt_tot_TextChanged(object sender, EventArgs e)
    {

    }
}


    