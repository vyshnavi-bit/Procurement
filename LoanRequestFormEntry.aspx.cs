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
public partial class LoanRequestFormEntry : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();

    msg getclass = new msg();
    
    SqlConnection con = new SqlConnection();
    DbHelper db = new DbHelper();
    DataTable AGENTLOANCOUNT = new DataTable();
    string[] SINGLEAGENT;
    string PARTICUAGENT;
    string GETLOANID;
    int ii = 0;
    int jj = 6;
    int i = 0;
    int j = 0;
    string[] GETAGENTIDWITHNAME;
   string[] GETAGENTCODE;
   string ADNET;
   int billdays;
   int Route;
   string AGENTID;
   int updateStatus;
   int GETREF;
   string getid;

   DataSet dsloan = new DataSet();
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
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");

                    if (roleid < 3)
                    {
                        loadsingleplant();
                        getagnetdropdown();
                    }

                    if ((roleid >= 3) && (roleid != 9) )
                    {

                        LoadPlantcode();
                    }
                    if (roleid == 9)
                    {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();

                    }

                }
                else
                {


                   
                   
                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                //plant = ddl_Plantname.Text;
                //GET = plant.Split('_');
                pcode = ddl_Plantcode.SelectedItem.Value;
                ViewState["pname"] = ddl_Plantname.SelectedItem.Text.Substring(4);
              
            }
        }

        catch
        {

           

        }
    }

    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantcode.Items.Add("--Select PlantName--");
                ddl_Plantname.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }


    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            GETROUTE();
            GETloanFROMRECOVERYid();
            string STR = ViewState["getloann"].ToString();
            if (STR==string.Empty)
            {
                GETloanid();

            }
            else
            {

               // GETloanFROMRECOVERYid();
            }

             insert();
            getclear();
            GETGRID();
        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.save) + "')</script>");

        }

    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }

    public void insert()
    {
        try
        {
            DateTime dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            con = db.GetConnection();
            string get = "";

            if ((txt_requerst.Text != string.Empty) && (txt_emi.Text != string.Empty))
            {
                get = "Insert Into LoanRequestEntry(Plant_code,plant_name,Agent_id,agent_name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,InsertedBy,ManStatus,TotalinterestAmount,TotalAmount,InstallAmount,Rateperinterest,route_id,Loanid)values('" + pcode + "','" + ViewState["pname"] + "','" + ViewState["AGENTID"].ToString() + "','" + ViewState["ANAME"] + "','" + d1 + "','" + txt_totloan.Text + "','" + txt_CompleteLoan.Text + "','" + txt_pendingloan.Text + "','" + txt_requerst.Text + "','" + txt_emi.Text + "','" + txt_description.Text + "','" + Session["Name"] + "','1','" + txt_totintamount.Text + "' ,'" + txt_totamount.Text + "' ,'" + txt_installAmount.Text + "','" + txt_interpercent.Text + "','" + Route + "','" + ViewState["getloann"] + "')";
                SqlCommand cmd = new SqlCommand(get, con);
                cmd.ExecuteNonQuery();
                getclear();
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.save) + "')</script>");
            }
            else
            {
                
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");


            }
       //     string MSG = "Record Save SuccessFully";
           
        }
        catch
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");

        }

    }


    public void getclear()
    {
        dtm = System.DateTime.Now;
        txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
        txt_totloan.Text = "";
        txt_CompleteLoan.Text = "";
        txt_pendingloan.Text = "";
        txt_requerst.Text = "";
        txt_emi.Text = "";
        txt_description.Text = "";
        txt_installAmount.Text="";
        txt_totamount.Text="";
        txt_totintamount.Text = "";
            

    }


    public void getclear1()
    {
        dtm = System.DateTime.Now;
        txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
        txt_emi.Text = "";
        txt_description.Text = "";
        txt_installAmount.Text = "";
        txt_totamount.Text = "";
        txt_totintamount.Text = "";


    }
    public void GETGRID()
    {
        try
        {
            con = db.GetConnection();
            string loanamt = " SELECT TOP 10  tid as Refid, AM.Agent_Id,Agent_Name,CONVERT(VARCHAR,RequestingDate,103) AS RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description   FROM(  SELECT  tid, Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,InsertedBy   FROM LoanRequestEntry WHERE PLANT_CODE='" + pcode + "') AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='" + pcode + "'  GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id order by Refid DESC";
            SqlCommand sc = new SqlCommand(loanamt, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            DataSet ds = new DataSet();
            da1.Fill(ds, "TABLEop");
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

    public void GETloanid()
    {
        con = db.GetConnection();
        string loan = "select (loan_Id+1) as Loanid  from (select MAX(loan_Id) as loan_Id  from  LoanDetails  where plant_code='"+pcode+"') as loanid  ";
        SqlCommand sc = new SqlCommand(loan, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet ds11 = new DataSet();
        da1.Fill(ds11, "loan");
        ViewState["getloann"] = ds11.Tables[0].Rows[0][0].ToString();
    }


    public void GETloanFROMRECOVERYid()
    {
        try
        {
            con = db.GetConnection();
            //  string loan = "select (loan_Id+1) as Loanid  from (select MAX(loan_Id) as loan_Id  from  LoanDetails  where plant_code='" + pcode + "') as loanid  ";
            string loan = "  select (LoanId+1) as Loanid  from (select MAX(LoanId) as LoanId  from  LoanRequestEntry  where plant_code='" + pcode + "') as loanid ";
            SqlCommand sc = new SqlCommand(loan, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            DataSet ds11 = new DataSet();
            da1.Fill(ds11, "loan");
            ViewState["getloann"] = ds11.Tables[0].Rows[0][0].ToString();
        }
        catch
        {

            ViewState["getloann"] = "NULL";
        }
    }

    public void getloanstatus()
    {

        try
        {

            ADNET = ddl_AgentName.Text;
            GETAGENTCODE = ADNET.Split('_');
            string GETAGENTCODEgettotloan = "";
            string Completedloan = "";
            string pendingloan = "";
            con = db.GetConnection();
            string gettotloan;
            gettotloan = "select COUNT(*) as totLoan   from LoanDetails   where plant_code='" + pcode + "' and agent_Id='" + GETAGENTCODE[0] + "'";
            SqlCommand cmdloan = new SqlCommand(gettotloan, con);
            DataSet dsb = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmdloan);
            da.Fill(dsb, ("TotLoan"));
            txt_totloan.Text = dsb.Tables[0].Rows[0][0].ToString();


            Completedloan = "select COUNT(*) as totLoan   from LoanDetails   where plant_code='" + pcode + "' and agent_Id='" + GETAGENTCODE[0] + "'   and  balance < 1 ";
            SqlCommand cmdcloan = new SqlCommand(Completedloan, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmdcloan);
            da1.Fill(dsb, ("cLoan"));
            txt_CompleteLoan.Text = dsb.Tables[1].Rows[0][0].ToString();


            pendingloan = "select COUNT(*) as totLoan   from LoanDetails   where plant_code='" + pcode + "' and agent_Id='" + GETAGENTCODE[0] + "'   and  balance > 1 ";
            SqlCommand cmdploan = new SqlCommand(pendingloan, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmdploan);
            da2.Fill(dsb, ("pLoan"));
            txt_pendingloan.Text = dsb.Tables[2].Rows[0][0].ToString();
        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }



    }

    public void rmupdate()
    {
        string sty = "";
        con = db.GetConnection();
        sty = "Select ISNULL(RmupdateEmiStatus,0) AS   RmupdateEmiStatus   from   LoanRequestEntry where plant_code='" + pcode + "' and agent_id='" + AGENTID + "' AND  TID='" + GETREF + "' ";
        SqlCommand cmd = new SqlCommand(sty, con);
        DataSet dg = new DataSet();
        SqlDataAdapter dap = new SqlDataAdapter(cmd);
        dap.Fill(dg, ("RmupdateStatus"));

        updateStatus = Convert.ToInt16(dg.Tables[0].Rows[0][0].ToString());

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        getagnetdropdown();
        GETGRID();
    }

    public void getagnetdropdown()
    {
        con = db.GetConnection();
        ddl_AgentName.Items.Clear();
        string str = "";
        str = "select (AgentId  + '_' + Agent_Name) as AgentName    from ( Select CONVERT(varchar,Agent_Id) as AgentId,Agent_Name   from agent_master where plant_code='" + pcode + "') as am ORDER BY RAND(AgentId) ASC";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddl_AgentName.Items.Add((dr["AgentName"]).ToString());

            }

        }
        else
        {


        }


    }
    protected void ddl_AgentName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            getloanstatus();
            string GETID = ddl_AgentName.Text;
            GETAGENTIDWITHNAME = GETID.Split('_');
            getid = GETAGENTIDWITHNAME[0];
            ViewState["AGENTID"] = GETAGENTIDWITHNAME[0].ToString();
            ViewState["ANAME"] = GETAGENTIDWITHNAME[1].ToString();
            getclear1();
            ddl_AgentName.Focus();
            string loanamt = "";
            string cashrecovery = "";
            string DUEpaid = "";
            con = db.GetConnection();
            loanamt = " select ISNULL(loanamount,0) as loanamount   from (SELECT SUM(loanamount) AS loanamount   FROM LoanDetails WHERE plant_code='" + pcode + "'  AND  agent_Id='" + ViewState["AGENTID"] + "') AS LLOAN";
            //loanamt="SELECT SUM(loanamount) AS loanamount   FROM LoanDetails WHERE plant_code='"+pcode+"'  AND agent_Id='"+  ViewState["AGENTID"]+"' ";
            SqlCommand cmdloan = new SqlCommand(loanamt, con);
            SqlDataAdapter daloan = new SqlDataAdapter(cmdloan);
            daloan.Fill(dsloan, ("loanamt"));
            double loanamtt = Convert.ToDouble(dsloan.Tables[0].Rows[0][0]);


            cashrecovery = "select ISNULL(RecoveryAmt,0) as RecoveryAmt   from (SELECT  SUM(LoanDueRecovery_Amount) AS RecoveryAmt  FROM LoanDue_Recovery WHERE plant_code='" + pcode + "'  AND agent_Id='" + ViewState["AGENTID"] + "') as rc";
            SqlCommand cashrecoverycmd = new SqlCommand(cashrecovery, con);
            SqlDataAdapter cashrecoveryda = new SqlDataAdapter(cashrecoverycmd);
            cashrecoveryda.Fill(dsloan, ("loancashrec"));
            double loancashrec = Convert.ToDouble(dsloan.Tables[1].Rows[0][0]);


            DUEpaid = "select ISNULL(Paid_Amount,0) as Paid_Amount   from (SELECT  SUM(Paid_Amount) AS Paid_Amount   FROM Loan_Recovery WHERE plant_code='" + pcode + "'  AND agent_Id='" + ViewState["AGENTID"] + "') as rc";
            SqlCommand cashpaidcmd = new SqlCommand(DUEpaid, con);
            SqlDataAdapter cashpaidcmdda = new SqlDataAdapter(cashpaidcmd);
            cashpaidcmdda.Fill(dsloan, ("loanamtpaid"));
            double loancashpaid = Convert.ToDouble(dsloan.Tables[2].Rows[0][0]);

            double loanamtsum = (loanamtt - loancashrec - loancashpaid);
            txt_closing.Text = loanamtsum.ToString("f2");
        }
        catch
        {



        }
    }


    //AGENTTOTBALANCE
    public void  AGENTbALANCE()
    {


    }


    public void GETROUTE()
    {

        con = db.GetConnection();
        string getperiodstr = "";
        getperiodstr = "Select  Route_id   from agent_master   where plant_code='" + pcode + "' and Agent_Id='" + ViewState["AGENTID"].ToString() + "' ";
        SqlCommand cmdperiod = new SqlCommand(getperiodstr, con);
        SqlDataAdapter da = new SqlDataAdapter(cmdperiod);
        DataSet df1 = new DataSet();
        da.Fill(df1, "Period");
        Route = Convert.ToInt16(df1.Tables[0].Rows[0][0]);


    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GETGRID();
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        GETGRID();
    }
    protected void txt_emi_TextChanged(object sender, EventArgs e)
    {
        try
        {
            period();
            double getloanamt = Convert.ToDouble(txt_requerst.Text);
            double intpercent = Convert.ToDouble(txt_interpercent.Text);

            // cal fromula   10000  amount 1paise inst      for 1 month 100  20 install   20/12  1.66 month 

            //one month amount 
            double interestamount = (getloanamt * intpercent) / 100;
            // Total Installment 20/3  
            double totemi = Convert.ToDouble(txt_emi.Text);
            double totmonth;
            if (billdays == 10)
            {
                totmonth = totemi / 3;

            }
            else
            {

                totmonth = totemi / 2;

            }
            double tottalinterestamount = totmonth * interestamount;
            double tottalinterestandloanamount = tottalinterestamount + getloanamt;
            double instalmentamt = tottalinterestandloanamount / totemi;
            txt_totintamount.Text = tottalinterestamount.ToString("f2");
            txt_installAmount.Text = instalmentamt.ToString("f2");
            txt_totamount.Text = tottalinterestandloanamount.ToString("f2");

            txt_description.Focus();
        }
        catch
        {
           
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");

        }
    }
    public void period()
    {
        con = db.GetConnection();
        string getperiodstr = "";
        getperiodstr = "Select  BillDays   from PlantBillPeriod   where plant_code='" + pcode + "'";
        SqlCommand cmdperiod = new SqlCommand(getperiodstr, con);
        SqlDataAdapter da = new SqlDataAdapter(cmdperiod);
        DataSet df = new DataSet();
        da.Fill(df, "Period");
        billdays = Convert.ToInt16(df.Tables[0].Rows[0][0]);

    }
}