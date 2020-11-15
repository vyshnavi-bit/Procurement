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
using System.Windows.Forms;
using System.Text.RegularExpressions;

public partial class AdminApproval : System.Web.UI.Page
{
    string conn = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection();
    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    public string userid;
    DataTable dt = new DataTable();
    DbHelper DBaccess = new DbHelper();


    SqlDataReader dr;
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    BOLbillgenerate BOLBill = new BOLbillgenerate();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLpayment Bllpay = new BLLpayment();
    BLLuser Bllusers = new BLLuser();
    string message;

    string ppcode;
    string ppname;
    string frmdate;
    string todate;

    string dat1;
    string mon1;
    string yea1;

    string dat2;
    string mon2;
    string yea2;


    string convertfDate;
    string convertTDate;
    public static int roleid;
    public static string username;
    DataTable missagent = new DataTable();
    msg MESS = new msg();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        SqlConnection con = new SqlConnection();
        if (IsPostBack != true)
        {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
            username = Session["Name"].ToString();
           // managmobNo = Session["managmobNo"].ToString();
           // userid = Session["User_ID"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
            getdata();
            lblmsg.Visible = false;
            if (roleid < 3)
            {
               // loadsingleplant();
                getdata1();
            }
            else
            {
                getdata();
            }
        }
        else
        {
            if (roleid < 3)
            {
                // loadsingleplant();
                getdata1();
            }
            else
            {
                getdata();

            }
            ccode = Session["Company_code"].ToString();
            lblmsg.Visible = false;
        }
    }

    private void SETBO()
    {
        BOLBill.Companycode = int.Parse(ccode);
        BOLBill.Plantcode = int.Parse(ppcode);
        BOLBill.Route_id = 0;
        //BOLBill.Frmdate = DateTime.Parse(txt_FromDate.Text.Trim());
        //BOLBill.Todate = DateTime.Parse(txt_ToDate.Text.Trim()); convertfDate, string convertTDate
        BOLBill.Frmdate = Convert.ToDateTime(convertfDate);
        BOLBill.Todate = Convert.ToDateTime(convertTDate);
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (roleid > 6)
        {
            lblmsg.Visible = false;
            pcode = GridView1.SelectedRow.Cells[1].Text;
            frmdate = GridView1.SelectedRow.Cells[10].Text;
            string[] ffdateGET = frmdate.Split('_');
            string getfdateGET = ffdateGET[0];
            string gettdate1GET = ffdateGET[1];
            dat1 = getfdateGET.Substring(0, 2);
            mon1 = getfdateGET.Substring(3, 2);
            yea1 = getfdateGET.Substring(6, 4);
            convertfDate = mon1 + "/" + dat1 + "/" + yea1;
            dat2 = gettdate1GET.Substring(0, 2);
            mon2 = gettdate1GET.Substring(3, 2);
            yea2 = gettdate1GET.Substring(6, 4);
            convertTDate = mon2 + "/" + dat2 + "/" + yea2;


            getmissingagent();
            if (missagent.Rows.Count < 1)
            {
                ppcode = GridView1.SelectedRow.Cells[1].Text;
                //    ppname = GridView1.SelectedRow.Cells[0].Text;
                frmdate = GridView1.SelectedRow.Cells[10].Text;
                string[] ffdate = frmdate.Split('_');
                string getfdate = ffdate[0];
                string gettdate1 = ffdate[1];
                dat1 = getfdate.Substring(0, 2);
                mon1 = getfdate.Substring(3, 2);
                yea1 = getfdate.Substring(6, 4);
                convertfDate = mon1 + "/" + dat1 + "/" + yea1;
                dat2 = gettdate1.Substring(0, 2);
                mon2 = gettdate1.Substring(3, 2);
                yea2 = gettdate1.Substring(6, 4);
                convertTDate = mon2 + "/" + dat2 + "/" + yea2;
               
                try
                {
                    // CheckDate and other conditions
                    using (con = DBaccess.GetConnection())
                    {
                        //  pcode = ddl_Plantcode.SelectedItem.Value;
                        string mess = string.Empty;
                        int ress = 0;
                        SqlCommand cmd = new SqlCommand();
                        SqlParameter parcompanycode, parplantcode, parmess, parress;
                        cmd.CommandText = "[dbo].[BillDate_Check]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;

                        parcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
                        parplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);

                        parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 300);
                        cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
                        parress = cmd.Parameters.Add("@ress", SqlDbType.Int);
                        cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

                        parcompanycode.Value = ccode;
                        parplantcode.Value = ppcode;

                        cmd.ExecuteNonQuery();
                        mess = (string)cmd.Parameters["@mess"].Value;
                        ress = (int)cmd.Parameters["@ress"].Value;

                        if (ress == 1)
                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = mess.Trim();
                            //  message = mess;

                        }
                        else
                        {
                            try
                            {
                                SETBO();  //convertTDate
                                //  DEDUCTION
                                BLLBill.GetDeductionForDeductionDeduct(BOLBill);
                                //LOAN
                                BLLBill.GetLoandetailsForLoanDeductadmin(BOLBill);
                                //procespayment(Bllpay.getpaymentdatareader(txt_FromDate.Text, txt_ToDate.Text, ccode, ppcode), txt_FromDate.Text, txt_ToDate.Text);
                                procespayment(Bllpay.getpaymentdatareaderadmin(convertfDate, convertTDate, ccode, ppcode), convertfDate, convertTDate);
                                //   Label4.Visible = true;
                                //    Label4.Text = "Bill proceeding Completed...";

                                message = "Bill proceeding Completed...";
                                lblmsg.Text = "Bill proceeding Completed...";
                                lblmsg.Visible = true;
                                WebMsgBox.Show(message);

                                updatestatus();

                                //   ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + message + "');</script>", false);

                                //  BillD();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //    Label4.Visible = true;
                    //    Label4.Text = ex.ToString();

                    message = ex.ToString();
                }

            }
            else
            {
            //    message = "Some Agent Missing Name Please Check";
                //string script = "window.onload = function(){ alert('"; script += message; script += "')};";
                //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
               // Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(message) + "')</script>");
                lblmsg.Text = "Some Agent Missing Name Please Check";
                lblmsg.Visible = true;
              //  WebMsgBox.Show("Some Agent Missing Name Please Check");
            }
        }

    }
    public void getmissingagent()
    {
        try
        {
            SqlConnection con = new SqlConnection(conn);
            string str = "sELECT procAgentid,Agentname  FROM (Select Agent_id as procAgentid  from PROCUREMENT  where plant_code in('"+pcode+"')  and  Prdate BETWEEN '" + convertfDate + "' AND '" + convertTDate + "'   GROUP BY Agent_id ) AS AGENTT LEFT JOIN (sELECT Agent_id,Agent_Name as Agentname  FROM Agent_Master where plant_code in('"+pcode+"')   GROUP BY Agent_id,Agent_Name ) as am on AGENTT.procAgentid=am.Agent_Id       where ((Agentname is null) or (Agentname=' '))";
       //    string str = "Select *  from Paymentdata where Frm_date='" + convertfDate + "'  and To_date='" + convertTDate + "' and Plant_code='" + pcode + "'  and (Agent_name is null or Agent_name='')";
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //   DataTable missagent = new DataTable();
            da.Fill(missagent);
        }
        catch
        {

        }

    }
    private void procespayment(DataTable dt, string convertfDate, string convertTDate)
    {
        //pcode = ddl_Plantcode.SelectedItem.Value;
        //pname = ddl_Plantname.SelectedItem.Value;
        //DateTime dt1 = new DateTime();
        //DateTime dt2 = new DateTime();
        //dt1 = DateTime.ParseExact(frdate, "dd/MM/yyyy", null);
        //dt2 = DateTime.ParseExact(todate, "dd/MM/yyyy", null);

        //string d1 = dt1.ToString("MM/dd/yyyy");
        //string d2 = dt2.ToString("MM/dd/yyyy");
        DataTable PaymentDT = new DataTable();
        if (dt.Rows.Count > 0)
        {
            PaymentDT = dt;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "PaymentDt2";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = PaymentDT;
            param.Direction = ParameterDirection.Input;
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Insert_paymentdata]");
                conn.Open();
                sqlCmd.CommandTimeout = 400;
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                //sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@spcname", ccode);
                //sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@sppname", ppcode);
                sqlCmd.Parameters.AddWithValue("@spfrdate", convertfDate);
                sqlCmd.Parameters.AddWithValue("@sptodate", convertTDate);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.CommandTimeout = 1800;
            }
        }
    }


    public void getdata()
    {
        SqlConnection con = new SqlConnection(conn);
        //  string str = "Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,Date from AdminApproval";
        dt.Rows.Clear();
        string str = "  select DISTINCT(plant_code),plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,(fdate +'_' + todate) as Date   from (Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status from AdminApproval  where  plant_code not in(160)  ) as aa left join (select plant_code  as pcode,convert(varchar,bill_frmdate,103) as Fdate,convert(varchar,bill_Todate,103) as Todate    from bill_date where status=0)  as bd on aa.plant_code=bd.pcode";
        con.Open();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    public void getdata1()
    {
        SqlConnection con = new SqlConnection(conn);
        //  string str = "Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,Date from AdminApproval";
        dt.Rows.Clear();
        string str = "  select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,(fdate +'_' + todate) as Date   from (Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status from AdminApproval  where  plant_code not in(160) AND plant_code='" + Session["plant_code"] + "'  ) as aa left join (select plant_code  as pcode,convert(varchar,bill_frmdate,103) as Fdate,convert(varchar,bill_Todate,103) as Todate    from bill_date where status=0 and  plant_code='" + Session["plant_code"] + "')  as bd on aa.plant_code=bd.pcode";
        con.Open();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int Data = Convert.ToInt16(e.Row.Cells[3].Text);
            int Loan = Convert.ToInt16(e.Row.Cells[4].Text);
            int deduc = Convert.ToInt16(e.Row.Cells[5].Text);
            int despatch = Convert.ToInt16(e.Row.Cells[6].Text);
            int closing = Convert.ToInt16(e.Row.Cells[7].Text);
            int transport = Convert.ToInt16(e.Row.Cells[8].Text);
            int Status = Convert.ToInt16(e.Row.Cells[9].Text);
            //   string Data =  (e.Row.Cells[8].Text);

            if (Data == 1)
            {
                e.Row.Cells[3].Text = "C";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[3].Font.Bold = true;
            }

            else
            {

                e.Row.Cells[3].Text = "P";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[3].Font.Bold = true;
            }

            if( (Data == 0) &&  (Status==2))
            {
                e.Row.Cells[3].Text = "C";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[3].Font.Bold = true;
            }


            if (Loan == 1)
            {
                e.Row.Cells[4].Text = "C";
                e.Row.Cells[4].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[4].Font.Bold = true;
            }

            else
            {

                e.Row.Cells[4].Text = "P";
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[4].Font.Bold = true;

            }

            if ((Loan == 0) && (Status == 2))
            {
                e.Row.Cells[4].Text = "C";
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[4].Font.Bold = true;
            }

            if (deduc == 1)
            {
                e.Row.Cells[5].Text = "C";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[5].Font.Bold = true;
            }

            else
            {

                e.Row.Cells[5].Text = "P";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[5].Font.Bold = true;
            }
            if ((deduc == 0) && (Status == 2))
            {
                e.Row.Cells[5].Text = "C";
                e.Row.Cells[5].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[5].Font.Bold = true;
            }

            if (despatch == 1)
            {
                e.Row.Cells[6].Text = "C";
                e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[6].Font.Bold = true;
            }

            else
            {

                e.Row.Cells[6].Text = "P";
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[6].Font.Bold = true;
            }


            if ((despatch == 0) && (Status == 2))
            {
                e.Row.Cells[6].Text = "C";
                e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[6].Font.Bold = true;
            }

            if (closing == 1)
            {
                e.Row.Cells[7].Text = "C";
                e.Row.Cells[7].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[7].Font.Bold = true;
            }
            else
            {

                e.Row.Cells[7].Text = "P";
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[7].Font.Bold = true;
            }

            if ((closing == 0) && (Status == 2))
            {
                e.Row.Cells[7].Text = "C";
                e.Row.Cells[7].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[7].Font.Bold = true;
            }


            if (transport == 1)
            {
                e.Row.Cells[8].Text = "C";
                e.Row.Cells[8].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[8].Font.Bold = true;
            }

            else
            {

                e.Row.Cells[8].Text = "P";
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[8].Font.Bold = true;

            }


            if ((transport == 0) && (Status == 2))
            {
                e.Row.Cells[8].Text = "C";
                e.Row.Cells[8].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[8].Font.Bold = true;
            }


            if (Data == 1 && Loan == 1 && deduc == 1 && despatch == 1 && closing == 1 && transport == 1 && Status == 1)
            {


                e.Row.Cells[9].Text = "Ready";
                e.Row.Cells[9].ForeColor = System.Drawing.Color.DarkOrange;
                e.Row.Cells[9].Font.Bold = true;
            }
            //if (Data == 1 && Loan == 1 && deduc == 1 && despatch == 1 && closing == 1 && transport == 1 && Status == 0)
            //{

            //    e.Row.Cells[9].Text = "Pending";
            //    e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
            //    e.Row.Cells[9].Font.Bold = true;
            //}

            if (Data == 0 || Loan == 0 || deduc == 0 || despatch == 0 || closing == 0 || transport == 0 || Status == 0)
            {

                e.Row.Cells[9].Text = "Pending";
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[9].Font.Bold = true;
            }

            if (Data == 1 && Loan == 1 && deduc == 1 && despatch == 1 && closing == 1 && transport == 1 && Status == 2)
            {

                e.Row.Cells[9].Text = "Completed";
                e.Row.Cells[9].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[9].Font.Bold = true;
            }

            if (Data == 0 && Loan == 0 && deduc == 0 && despatch == 0 && closing == 0 && transport == 0 && Status == 2)
            {

                e.Row.Cells[9].Text = "Completed";
                e.Row.Cells[9].ForeColor = System.Drawing.Color.DarkGreen;
                e.Row.Cells[9].Font.Bold = true;
            }




            if (Data == 1 && Loan == 1 && deduc == 1 && despatch == 1 && closing == 1 && transport == 1 && Status==1)
            {
                e.Row.Cells[11].Visible = true;

            }
            else
            {

                e.Row.Cells[11].Visible = false;


            }

        }

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {


        

       
      


    }


    public void msgbox()
    {
        string message;
        message = "Record Deleted SuccessFully";
        string script = "window.onload = function(){ alert('"; script += message; script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

    }

    public void updatestatus()
    {

        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand cmd = new SqlCommand("update AdminApproval set Status='2',buttonviewstatus=2,Data=0,Loanstatus=0,DeductionStatus=0,DespatchStatus=0,ClosingStatus=0,TransportStatus=0 where Plant_code='" + ppcode + "'", conn);
        cmd.ExecuteNonQuery();


    }

    //private void SETBO()
    //{
    //    BOLBill.Companycode = int.Parse(ccode);
    //    BOLBill.Plantcode = int.Parse(pcode);
    //  //  BOLBill.Route_id = int.Parse(rid);
    //    //BOLBill.Frmdate = DateTime.Parse(txt_FromDate.Text.Trim());
    //    //BOLBill.Todate = DateTime.Parse(txt_ToDate.Text.Trim());
    //    BOLBill.Frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //    BOLBill.Todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

    //}
    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    getdata();

    //}
}
