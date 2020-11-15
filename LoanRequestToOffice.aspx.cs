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
using System.Xml.Linq;
using System.Net;
using System.IO;
public partial class LoanRequestToOffice : System.Web.UI.Page
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
    DbHelper db = new DbHelper();
    int I;
    string getplant;
    string getAgent;
    string Date;
    msg getclass = new msg();
    DataTable AGENTLOANCOUNT = new DataTable();
    string[] SINGLEAGENT;
    string PARTICUAGENT;
    string GETLOANID;
    int ii = 0;
    int jj = 6;
    int i = 0;
    int j = 0;
    string[] GETAGENTIDWITHNAME;
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
                    Button2.Visible = false;
                  
                        GETGRID();

                        GridView3.Visible = false;
                        GridView4.Visible = false;
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


    public void GETGRID()
    {
        con = db.GetConnection();


        //  string loanamt = "SELECT  Plant_code,Plant_code as PlantName,Agent_id,Agent_id as AgentName,CONVERT(VARCHAR,RequestingDate,103) AS RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi   FROM LoanRequestEntry    WHERE  ManStatus='1' AND RmStatus=0   order by tid  desc ";
        // string loanamt = "select Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT  Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE PLANT_CODE='"+pcode+"' AND ManStatus=1 AND RmStatus!=1 ) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='"+pcode+"' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='165' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        //   string loanamt = " select Tid AS RefId, Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT Tid,lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT Tid,Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE Plant_code='" + pcode + "' AND ManStatus=1 and  RmStatus NOT IN(1)) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='" + pcode + "' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='" + pcode + "' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount,Description   FROM LoanRequestEntry WHERE     ManStatus=1 and  RmStatus=1  and LoanVerifyChennaiStatus  is null ";
        SqlCommand sc = new SqlCommand(loanamt, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        da1.Fill(ds, "TABLEop");
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {

            Button2.Visible = true;
            Button3.Visible = true;
            I = 1;


        }
        else
        {
            Button2.Visible = false;
            Button3.Visible = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {

                    string ID = row.Cells[2].Text;
                    string pcode = row.Cells[3].Text;
                    string agentid = row.Cells[5].Text;

                    try
                    {



                        string getupdate;
                        con = db.GetConnection();
                        getupdate = "Update LoanRequestEntry set  LoanVerifyDate='" + System.DateTime.Now + "',LoanVerifyChennaiStatus=1  where plant_code='" + pcode + "' and Agent_id='" + agentid + "' and   Tid='" + ID + "'";
                        SqlCommand cmd = new SqlCommand(getupdate, con);
                        cmd.ExecuteNonQuery();
                        Response.Write("<script language='javascript'> alert('" + Server.HtmlEncode(getclass.verified) + "')</script>");

                    }
                    catch (Exception EX)
                    {



                    }

                    GETGRID();
                }
            }
        }

    }
   
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

             ViewState["PPCODE"]      =GridView2.SelectedRow.Cells[3].Text;
            ViewState["AGENTID"]  =GridView2.SelectedRow.Cells[5].Text;
      
        getagetloandetails2();
        getagetloandetails3();
        GridView3.Visible = true;
        GridView4.Visible = true;
    }

    public void getagetloandetails3()
    {
        con = db.GetConnection();
        string str = "";
        str = "   SELECT YEAR = YEAR(PRDATE), Month = UPPER(left(DATENAME(MONTH,PRDATE),3)),          Milkkg = (sum(mILK_KG)),            Milkltr = (sum(Milk_ltr)),         Fat = convert(decimal(18,2),(avg(Fat))),         Snf = convert(decimal(18,2),(avg(snf))),         Amount = (sum(amount)),         Commission = (sum(ComRate)),          AvgRate = convert(decimal(18,2),(avg(Rate))),cartage = convert(decimal(18,1),((sum(CartageAmount))/SUM(Milk_ltr))),           SplBonus =  convert(decimal(18,1),((sum(SplBonusAmount))/SUM(Milk_ltr))),           milknature=milk_nature,         MilkSession = count(* ) FROM    Procurement WHERE   Plant_Code='" +  ViewState["PPCODE"]  + "'  AND  Agent_id='" +  ViewState["AGENTID"] + "' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE),milk_nature ORDER BY YEAR ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "Loanlist12");
        GridView3.DataSource = ds.Tables[0];
        GridView3.DataBind();


    }



    public void getagetloandetails2()
    {

        con = db.GetConnection();
        string op = "select  ( lm.agent_Id) as Canno,CONVERT(VARCHAR,loandate,103) AS LoanDate,loan_Id as LoanId,CONVERT(DECIMAL(18,2),loanamount) AS IssuedAmount,NoofInstallment  as  NoofEmI  from (select  CONVERT(varchar,(agent_Id)) as Agent_Id ,loandate,loan_Id,loanamount,NoofInstallment  from LoanDetails   where Plant_Code='" +  ViewState["PPCODE"]  + "'  AND  Agent_id='" +  ViewState["AGENTID"] + "' ) as lm  left join (select   Agent_Id,Agent_Name    from  Agent_Master where Plant_Code='" +  ViewState["PPCODE"]  + "'  AND  Agent_id='" +  ViewState["AGENTID"] + "') as  am on lm.agent_Id= am.Agent_Id  ORDER BY LoanId  ";
        SqlCommand cmd = new SqlCommand(op, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // da.Fill(
        da.Fill(AGENTLOANCOUNT);
        AGENTLOANCOUNT.Columns.Add("paidAmt");
        AGENTLOANCOUNT.Columns.Add("CashRecAmt");
        AGENTLOANCOUNT.Columns.Add("PaidEmI");
        AGENTLOANCOUNT.Columns.Add("PendingEmI");
        AGENTLOANCOUNT.Columns.Add("Balance");


        if (AGENTLOANCOUNT.Rows.Count > 0)
        {
            GridView4.DataSource = AGENTLOANCOUNT;
            GridView4.DataBind();


        }
        else
        {
            GridView4.DataSource = "";
            GridView4.DataBind();

        }

        foreach (DataRow dr in AGENTLOANCOUNT.Rows)
        {

            string str = dr[0].ToString();
            SINGLEAGENT = str.Split('_');
            PARTICUAGENT = SINGLEAGENT[0];
            GETLOANID = dr[2].ToString();

            string AGENTpaidamount = "Select  isnull(SUM(paid_Amount),0) as paidAmount  from Loan_Recovery   where Plant_Code='" + ViewState["PPCODE"] + "'  AND  Agent_id='" + ViewState["AGENTID"] + "' AND Loan_id='" + GETLOANID + "'";
            SqlCommand sc1 = new SqlCommand(AGENTpaidamount, con);
            SqlDataAdapter da2 = new SqlDataAdapter(sc1);
            DataSet DS1 = new DataSet();
            da2.Fill(DS1, "PAIDLOAN");
            Session["AGENTpaidamount"] = DS1.Tables[0].Rows[0][0].ToString();
            string cashreceipt = "Select  isnull(SUM(LoanDueRecovery_Amount),0) as CashReceived_Amount  from LoanDue_Recovery WHERE   Plant_Code='" + ViewState["PPCODE"] + "'  AND  Agent_id='" + ViewState["AGENTID"] + "'";
            SqlCommand sc2 = new SqlCommand(cashreceipt, con);
            SqlDataAdapter da3 = new SqlDataAdapter(sc2);
            //  DataSet ds2 = new DataSet();
            da3.Fill(DS1, "CASHAMOUNT");
            Session["AGENTcashreceipt"] = DS1.Tables[1].Rows[0][0].ToString();

            double PAID = Convert.ToDouble(Session["AGENTpaidamount"].ToString());
            double CASH = Convert.ToDouble(Session["AGENTcashreceipt"].ToString());

            GridView4.Rows[ii].Cells[jj].Text = PAID.ToString("f2");
            jj = jj + 1;
            GridView4.Rows[ii].Cells[jj].Text = CASH.ToString("f2");
            int install;
            double LOANAMT = Convert.ToDouble(GridView4.Rows[ii].Cells[4].Text);

            try
            {
                install = Convert.ToInt16(GridView4.Rows[ii].Cells[5].Text);
            }
            catch
            {
                install = 0;

            }
            double PAIDAMT = Convert.ToDouble(GridView4.Rows[ii].Cells[6].Text);
            double CASHREC = Convert.ToDouble(GridView4.Rows[ii].Cells[7].Text);

            double pendininst = Convert.ToDouble(LOANAMT / install);
            double addpaiamtCASHREC = (PAIDAMT + CASHREC);
            string GETRES = (LOANAMT - (PAIDAMT + CASHREC)).ToString("f2");
            double getres1 = Convert.ToDouble(GETRES);

            int assingemi = Convert.ToInt32(getres1 / pendininst);

            GridView4.Rows[ii].Cells[8].Text = (install - assingemi).ToString();

            GridView4.Rows[ii].Cells[9].Text = assingemi.ToString();
            GridView4.Rows[ii].Cells[10].Text = GETRES.ToString();

            GridView4.Rows[ii].Cells[10].Font.Bold = true;

            if (getres1 > 1)
            {
                GridView4.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkOrange;

            }

            if (getres1 > 100000.00)
            {
                GridView4.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkRed;

            }
            if (getres1 < 1)
            {
                GridView4.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkGreen;


            }

            ii = ii + 1;
            jj = 6;

        }





        }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string getmil = e.Row.Cells[12].Text;
            if (getmil == "1")
            {


                e.Row.Cells[11].Text = "Cow";
            }
            else
            {
                e.Row.Cells[11].Text = "Buffalo";

            }
        }
    }

protected void  GridView3_RowCreated(object sender, GridViewRowEventArgs e)
{
    try
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

            HeaderCell2.Text = "Milk OverAll  Details";
            HeaderCell2.ColumnSpan = 13;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView3.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    catch (Exception ex)
    {
        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

    }
}
protected void  GridView1_SelectedIndexChanged(object sender, EventArgs e)
{

}
protected void  GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
{

}
protected void  GridView2_RowCreated(object sender, GridViewRowEventArgs e)
{
    try
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

            HeaderCell2.Text = "Loan Entry Details";
            HeaderCell2.ColumnSpan = 19;
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
protected void Button3_Click(object sender, EventArgs e)
{
    GridView3.Visible = false;
    GridView4.Visible = false;
}
protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
{
    try
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

            HeaderCell2.Text = "Loan Running And Stopped Details";
            HeaderCell2.ColumnSpan = 11;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView4.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    catch (Exception ex)
    {
        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

    }
}
protected void GridView2_RowCreated1(object sender, GridViewRowEventArgs e)
{

}
}
