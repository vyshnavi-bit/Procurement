using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
public partial class LoanFinalApproval : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    BLLuser Bllusers = new BLLuser(); 
    DataTable dtt = new DataTable();
    DataTable dttt = new DataTable();
    int i = 2;
    int j = 0;
    int K = 0;
    int L = 0;
    int M = 0;
    string INCCOUNT;
    double FAT;
    string GETDATE;
    string[] plant;
    // string[] getdat;
    string getcommondate;

    string FDATE;
    string TODATE;

    string FDATE1;
    string TODATE1;
    string frmdate;
    string Todate;
    DateTime d1;
    DateTime d2;

    DateTime d11;
    DateTime d22;
    string frmdate1;
    string Todate1;
    DateTime datee1;
    DateTime datee2;
    string GETTID;

    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string plname;
    Datatable dt = new Datatable();
    DataSet ds = new DataSet();
    int maxloan;

    string refno;
    string getAgent;
    string ROUTEID;
    int billdays;
    msg getclass = new msg();

      string getref;
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
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    GetBilldate.Visible = false;
                    Label5.Visible = false;
                    Button2.Visible = false;

                    Button4.Visible = false;
                    getgrid();
                    billdate();
                    GetBilldate.Visible = true;
                    Label5.Visible = true;
                }
                else
                {
                }
            }
            else
            {
                ccode = Session["Company_code"].ToString();
               

            }

        }
        catch
        {


        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        try
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {

                        getref = row.Cells[1].Text;
                        string pppcode = row.Cells[2].Text;

                        string canno = row.Cells[4].Text;
                        double loanamt = Convert.ToDouble(row.Cells[6].Text);
                        int emi = Convert.ToInt16(row.Cells[7].Text);
                        string rateper = row.Cells[8].Text;
                        double totintamt = Convert.ToDouble(row.Cells[9].Text);
                        string totamt = row.Cells[10].Text;
                        string installamt = row.Cells[11].Text;
                        //    GETloanid();
                        string loanid = row.Cells[12].Text;
                        //  string loanid = ViewState["getloann"].ToString();
                        string roudteid = row.Cells[13].Text;
                        //   string frmdate = row.Cells[14].Text;
                        // string todate = row.Cells[15].Text;
                        string Des = row.Cells[16].Text;
                        double prinamt = loanamt / emi;
                        double intamt = totintamt / emi;



                        string frmdate = row.Cells[14].Text;
                        string[] p = frmdate.Split('/');
                        getvald = p[0];
                        getvalm = p[1];
                        getvaly = p[2];
                        FDATE = getvalm + "/" + getvald + "/" + getvaly;

                        string todate = row.Cells[15].Text;
                        string[] p1 = todate.Split('/');
                        getvaldd = p1[0];
                        getvalmm = p1[1];
                        getvalyy = p1[2];

                        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
                        if ((FDATE != string.Empty) && (TODATE != string.Empty))
                        {
                            con = DB.GetConnection();
                            string getupdate;

                            getupdate = "INSERT INTO LoanDetails(  company_code,plant_code,loan_Id,expiredate,route_id,agent_Id,loanamount,balance,inst_amount,dscription,status,principalamount,Interestamount,Instalprincipalamount,InstalInterestamount,NoofInstallment,EntryDate,EntryId,LLoanFlag,loandate)VALUES('1','" + pppcode + "','" + loanid + "','" + System.DateTime.Now + "','" + roudteid + "','" + canno + "','" + totamt + "','" + totamt + "','" + installamt + "','" + Des + "','" + 0 + "','" + loanamt + "','" + totintamt + "','" + prinamt.ToString("F2") + "','" + intamt.ToString("F2") + "','" + emi + "','" + System.DateTime.Now + "','" + roleid + "','" + 0 + "','" + FDATE + "')";
                            SqlCommand cmd = new SqlCommand(getupdate, con);
                            cmd.ExecuteNonQuery();
                            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.update) + "')</script>");
                            string gerup;
                            gerup = "Update LoanRequestEntry set FinalApprovalStaus=1,FinalApprovaldatetime='" + System.DateTime.Now + "' where   plant_code='" + pppcode + "' and agent_id='" + canno + "' and tid='" + getref + "'";
                            SqlCommand cmd11 = new SqlCommand(gerup, con);
                            cmd11.ExecuteNonQuery();

                        }
                        else
                        {

                            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.billdatemiss) + "')</script>");
                        }

                        getgrid();



                    }



                }
            }

        }
        catch
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");

        }
    }



    //public void GETloanid()
    //{
    //    con =DB.GetConnection();
    //    string loan = "select (loan_Id+1) as Loanid  from (select MAX(loan_Id) as loan_Id  from  LoanDetails  where plant_code='" + pcode + "') as loanid  ";
    //    SqlCommand sc = new SqlCommand(loan, con);
    //    SqlDataAdapter da1 = new SqlDataAdapter(sc);
    //    DataSet ds11 = new DataSet();
    //    da1.Fill(ds11, "loan");
    //    ViewState["getloann"] = ds11.Tables[0].Rows[0][0].ToString();
    //}
    public void getgrid()
    {

        con = DB.GetConnection();
      //  string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount,Description   FROM LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus=1 AND FinalApprovalStaus IS NULL";
      //  string loanamt = "select Tid as refid,Plant_code as Pcode,plant_name as pname,Agent_id as AgentId,Agent_name as Name,LoanAmount,Emi,Rateperinterest as Rateperinst,TotalinterestAmount as ToinsteAmt,TotalAmount as TotAmt,InstallAmount as BillRecoveryAmt,LoanId,Route_id as RouteId,FromDate,Todate,Description  from  LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus=1 AND FinalApprovalStaus IS NULL";
        string loanamt = "	select Tid,Plant_code,plant_name,Agent_id,agent_name,LoanAmount,Emi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount,LoanId,Route_id, CONVERT(VARCHAR,FromDate,103) AS FromDate,CONVERT(VARCHAR,Todate,103) AS Todate,Description  from  LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus=1 AND FinalApprovalStaus IS NULL";

        SqlCommand sc = new SqlCommand(loanamt, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet dgh = new DataSet();
        da1.Fill(dgh, "TABLEop");
        GridView2.DataSource = dgh.Tables[0];
        GridView2.DataBind();

        if (dgh.Tables[0].Rows.Count > 0)
        {

            Button2.Visible = true;
            //I = 1;
            Button4.Visible = true;
            GetBilldate.Visible = true;
            Label5.Visible = true;

        }
        else
        {
            Button2.Visible = false;
            Button4.Visible = false;

            GetBilldate.Visible = false;
            Label5.Visible = false;


        }

    }


    public void billdate()
    {

        try
        {

            GetBilldate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT TOP 4  Bill_frmdate,Bill_todate FROM Bill_date GROUP BY  Bill_frmdate,Bill_todate  order by  Bill_frmdate desc  ";
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
                    GetBilldate.Items.Add(frmdate + "-" + Todate);

                }

            }
        }
        catch
        {


        }

    }

  
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
       //  pcode = ddl_Plantname.SelectedValue.ToString();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //getgrid();
        //billdate();
        //GetBilldate.Visible = true;
        //Label5.Visible = true;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

        string date = GetBilldate.Text;
        string[] p = date.Split('/', '-');

        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    refno = row.Cells[1].Text;
                    getAgent = row.Cells[4].Text;
                     string   PLANT = row.Cells[2].Text;
                    string getloanfinaupdate1;
                    con = DB.GetConnection();
                    getloanfinaupdate1 = "Update LoanRequestEntry set FromDate='" + FDATE + "',Todate='" + TODATE + "' WHERE plant_code='" + PLANT + "' and Agent_id='" + getAgent + "' and  Tid='" + refno + "'";
                    SqlCommand cmd11 = new SqlCommand(getloanfinaupdate1, con);
                    cmd11.ExecuteNonQuery();
                    Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.billupdate) + "')</script>");
                }
            }
        }
        getgrid();
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

                HeaderCell2.Text = "Loan Details";
                HeaderCell2.ColumnSpan = 18;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
}