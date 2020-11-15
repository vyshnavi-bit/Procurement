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

public partial class LoanDashBoard : System.Web.UI.Page
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
    DataTable dt = new DataTable();
    DataTable AGENTLOANCOUNT = new DataTable();
    DataTable AGENTLOANLISTWISE = new DataTable();
    string msg;
    Exception ex;
    string plant;
    string[] GET;
    string[] GETagent;
    string[] GETGRIGagent;
    string AGENTIDONLY;
    int i = 0;
    int j = 2;
    int k = 0;
    //SINGLE AGENTSDETAILS//
    string[] SINGLEAGENT;
    string PARTICUAGENT;
    string GETLOANID;
    int ii = 0;
    int jj = 6;
    //SINGLE AGENTDETAILSWISE//

    string[] SINGLEAGENTDETAILS;
    string PARTICUAGENTDETAILS;
    string GETLOANIDDETAILS;
    int iii = 0;
    int jjj = 8;
    DataTable dtloandate = new DataTable();
    DataTable paiddate = new DataTable();
    DataTable cashrec = new DataTable();
    DataTable commandata = new DataTable();

    DataTable paiddateamt = new DataTable();
    DataTable cashrecamt = new DataTable();
    DataTable count1 = new DataTable();
    DataTable count2 = new DataTable();
    string commdate;
    string GETRES;
    double getres1;
    double PAIDAMTBAL;
    double LOANAMT123;
    int JH = 0;
    double paid;
    double CASH;
    int getc = 0;
    int getcc = 1; 
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
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    //txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                    if (roleid < 3)
                    {
                        loadsingleplant();

                    }
                    if (roleid >= 3)
                    {

                        LoadPlantcode();
                    }

                    if (roleid == 9)
                    {
                        loadspecialsingleplant();
                        pcode = "170";
                        Session["Plant_Code"] = "170".ToString();
                    }
                    //DateTime txtMyDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                    ////DateTime dttt = DateTime.Parse(txt_FromDate.Text);

                    ////DateTime cutime = DateTime.Parse(gettime);
                    ////string sff = dttt.AddDays(1).ToString();

                    ////GETDATE = txt_FromDate.Text;

                    //DateTime date = txtMyDate.AddDays(-1);

                    //string datee = date.ToString("MM/dd/yyyy");
                    // DateTime DDD = DateTime.ParseExact(date);
                    Button2.Visible = false;
                    GridView2.Visible = false;
                    GridView1.Visible = true;
                }
                else
                {



                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                plant = ddl_Plantname.Text;
                GET = plant.Split('_');
                pcode = ddl_Plantcode.SelectedItem.Value;

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        plant = ddl_Plantname.Text;
        GET = plant.Split('_');
      //  getagentName();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        GridView2.Visible = false;
        GridView3.Visible = false;
        GridView1.Visible = true;
        getgrid();
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
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
                HeaderCell2.Text = ddl_Plantname.Text + ":Loan Details:";
                HeaderCell2.ColumnSpan = 8;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);



                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    public void getgrid()
    {
        try
        {
            con = DB.GetConnection();
            string op = "select agentId + '_' + Agent_Name as Name   from (select CONVERT(varchar,(agentId)) as agentId , Agent_Name  from (Select agent_Id AS agentId   from LoanDetails   where plant_code='" + GET[0] + "'     group by agent_Id) as ld left join(Select Agent_Id,Agent_Name   from agent_master   where plant_code='" + GET[0] + "'   group by agent_Id,Agent_Name) as am on ld.agentId=am.Agent_Id) as dd ";
            SqlCommand cmd = new SqlCommand(op, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dt.Columns.Add("LoanTotAmount");
            dt.Columns.Add("RecoveryAmount");
            dt.Columns.Add("CashReceiptAmount");
            dt.Columns.Add("Balance");

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = "NoRecordsFound";
                GridView1.DataBind();

            }


            foreach (DataRow dr in dt.Rows)
            {

                string str = dr[0].ToString();
                GETagent = str.Split('_');
                AGENTIDONLY = GETagent[0];


                string loanamt = "Select isnull(SUM(loanamount),0) as LoanAmount  from LoanDetails   where plant_code='" + GET[0] + "'    and agent_Id='" + AGENTIDONLY + "'";
                SqlCommand sc = new SqlCommand(loanamt, con);
                SqlDataAdapter da1 = new SqlDataAdapter(sc);
                DataSet ds = new DataSet();
                da1.Fill(ds, "TABLEop");
                Session["loanamt"] = ds.Tables[0].Rows[0][0].ToString();
                string paidamount = "Select  isnull(SUM(paid_Amount),0) as paidAmount  from Loan_Recovery   where plant_code='" + GET[0] + "'  and agent_Id='" + AGENTIDONLY + "'";
                SqlCommand sc1 = new SqlCommand(paidamount, con);
                SqlDataAdapter da2 = new SqlDataAdapter(sc1);
                da2.Fill(ds, "PROCAM");
                Session["paidamount"] = ds.Tables[1].Rows[0][0].ToString();
                string cashreceipt = "Select  isnull(SUM(LoanDueRecovery_Amount),0) as CashReceived_Amount  from LoanDue_Recovery WHERE  plant_code='" + GET[0] + "'  and agent_Id='" + AGENTIDONLY + "'";
                SqlCommand sc2 = new SqlCommand(cashreceipt, con);
                SqlDataAdapter da3 = new SqlDataAdapter(sc2);
                //  DataSet ds2 = new DataSet();
                da3.Fill(ds, "PROCPM");
                Session["cashreceipt"] = ds.Tables[2].Rows[0][0].ToString();

                double loanamt1 = Convert.ToDouble(Session["loanamt"].ToString());
                double PAID = Convert.ToDouble(Session["paidamount"].ToString());
                double CASH = Convert.ToDouble(Session["cashreceipt"].ToString());

                GridView1.Rows[i].Cells[j].Text = loanamt1.ToString("f2");
                j = j + 1;
                GridView1.Rows[i].Cells[j].Text = PAID.ToString("f2");
                j = j + 1;
                GridView1.Rows[i].Cells[j].Text = CASH.ToString("f2");

                double LOANAMT = Convert.ToDouble(GridView1.Rows[i].Cells[2].Text);
                double PAIDAMT = Convert.ToDouble(GridView1.Rows[i].Cells[3].Text);
                double CASHREC = Convert.ToDouble(GridView1.Rows[i].Cells[4].Text);

                string GETRES = (LOANAMT - (PAIDAMT + CASHREC)).ToString("f2");
                double getres1 = Convert.ToDouble(GETRES);
                GridView1.Rows[i].Cells[5].Text = GETRES.ToString();
                GridView1.Rows[i].Cells[5].Font.Bold = true;
                if (getres1 > 1)
                {
                    GridView1.Rows[i].Cells[5].ForeColor = System.Drawing.Color.DarkOrange;

                }

                if (getres1 > 100000.00)
                {
                    GridView1.Rows[i].Cells[5].ForeColor = System.Drawing.Color.DarkRed;

                }
                if (getres1 < 1)
                {
                    GridView1.Rows[i].Cells[5].ForeColor = System.Drawing.Color.DarkGreen;


                }

                i = i + 1;
                j = 2;


            }

            if (Allloan.Checked == false)
            {

                foreach (GridViewRow GG in GridView1.Rows)
                {

                    double getbal = Convert.ToDouble(GridView1.Rows[getc].Cells[5].Text);

                    if (getbal < 1)
                    {

                        GridView1.Rows[getc].Visible = false;

                    }
                    else
                    {
                        GridView1.Rows[getc].Cells[0].Text = getcc.ToString();
                        getcc = getcc + 1;

                    }


                    getc = getc + 1;
                }
            }



        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }


    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            GridView1.Visible = false;
            Button2.Visible = true;
            GridView2.Visible = true;
            string GETCODE = GridView1.SelectedRow.Cells[1].Text;
            GETGRIGagent = GETCODE.Split('_');

            Session["ID1"] = GETGRIGagent[0];
            Session["NAME1"] = GETGRIGagent[1];

            con = DB.GetConnection();
            string op = "select  ( lm.agent_Id  + '_' + Agent_Name) as AgentName,CONVERT(VARCHAR,loandate,103) AS LoanDate,loan_Id as LoanId,CONVERT(DECIMAL(18,2),loanamount) AS loanamount,NoofInstallment     from (select  CONVERT(varchar,(agent_Id)) as Agent_Id ,loandate,loan_Id,loanamount,NoofInstallment  from LoanDetails   where plant_code='" + GET[0] + "' and agent_Id='" + GETGRIGagent[0] + "' ) as lm  left join (select   Agent_Id,Agent_Name    from  Agent_Master where plant_code='" + GET[0] + "' and agent_Id='" + GETGRIGagent[0] + "') as  am on lm.agent_Id= am.Agent_Id  ORDER BY LoanId  ";
            SqlCommand cmd = new SqlCommand(op, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // da.Fill(
            da.Fill(AGENTLOANCOUNT);
            AGENTLOANCOUNT.Columns.Add("RecoveryAmount");
            AGENTLOANCOUNT.Columns.Add("CashReceiptAmount");
            AGENTLOANCOUNT.Columns.Add("PaidEmI");
            AGENTLOANCOUNT.Columns.Add("PendingEmI");
            AGENTLOANCOUNT.Columns.Add("Balance");


            if (AGENTLOANCOUNT.Rows.Count > 0)
            {
                GridView2.DataSource = AGENTLOANCOUNT;
                GridView2.DataBind();


            }
            else
            {
                GridView2.DataSource = "NoRecordsFound";
                GridView2.DataBind();

            }

            foreach (DataRow dr in AGENTLOANCOUNT.Rows)
            {

                string str = dr[0].ToString();
                SINGLEAGENT = str.Split('_');
                PARTICUAGENT = SINGLEAGENT[0];
                GETLOANID = dr[2].ToString();



                string AGENTpaidamount = "Select  isnull(SUM(paid_Amount),0) as paidAmount  from Loan_Recovery   where plant_code='" + GET[0] + "'  and agent_Id='" + PARTICUAGENT + "' AND Loan_id='" + GETLOANID + "'";
                SqlCommand sc1 = new SqlCommand(AGENTpaidamount, con);
                SqlDataAdapter da2 = new SqlDataAdapter(sc1);
                DataSet DS1 = new DataSet();
                da2.Fill(DS1, "PAIDLOAN");
                Session["AGENTpaidamount"] = DS1.Tables[0].Rows[0][0].ToString();
                string cashreceipt = "Select  isnull(SUM(LoanDueRecovery_Amount),0) as CashReceived_Amount  from LoanDue_Recovery WHERE   plant_code='" + GET[0] + "'  and agent_Id='" + PARTICUAGENT + "' AND Loan_id='" + GETLOANID + "'";
                SqlCommand sc2 = new SqlCommand(cashreceipt, con);
                SqlDataAdapter da3 = new SqlDataAdapter(sc2);
                //  DataSet ds2 = new DataSet();
                da3.Fill(DS1, "CASHAMOUNT");
                Session["AGENTcashreceipt"] = DS1.Tables[1].Rows[0][0].ToString();

                double PAID = Convert.ToDouble(Session["AGENTpaidamount"].ToString());
                double CASH = Convert.ToDouble(Session["AGENTcashreceipt"].ToString());

                GridView2.Rows[ii].Cells[jj].Text = PAID.ToString("f2");
                jj = jj + 1;
                GridView2.Rows[ii].Cells[jj].Text = CASH.ToString("f2");
                int install;
                double LOANAMT = Convert.ToDouble(GridView2.Rows[ii].Cells[4].Text);

                try
                {
                    install = Convert.ToInt16(GridView2.Rows[ii].Cells[5].Text);
                }
                catch
                {
                    install = 0;

                }
                double PAIDAMT = Convert.ToDouble(GridView2.Rows[ii].Cells[6].Text);
                double CASHREC = Convert.ToDouble(GridView2.Rows[ii].Cells[7].Text);

                double pendininst = Convert.ToDouble(LOANAMT / install);
                double addpaiamtCASHREC = (PAIDAMT + CASHREC);
                string GETRES = (LOANAMT - (PAIDAMT + CASHREC)).ToString("f2");
                double getres1 = Convert.ToDouble(GETRES);

                int assingemi = Convert.ToInt32(getres1 / pendininst);

                GridView2.Rows[ii].Cells[8].Text = (install - assingemi).ToString();

                GridView2.Rows[ii].Cells[9].Text = assingemi.ToString();
                GridView2.Rows[ii].Cells[10].Text = GETRES.ToString();

                GridView2.Rows[ii].Cells[10].Font.Bold = true;

                if (getres1 > 1)
                {
                    GridView2.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkOrange;

                }

                if (getres1 > 100000.00)
                {
                    GridView2.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkRed;

                }
                if (getres1 < 1)
                {
                    GridView2.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkGreen;


                }

                ii = ii + 1;
                jj = 6;

            }
        }

        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GridView1.Visible = false;
            Button2.Visible = true;
            GridView2.Visible = true;
            GridView3.Visible = true;

            string GETCODE = GridView2.SelectedRow.Cells[1].Text;
            SINGLEAGENTDETAILS = GETCODE.Split('_');
            Session["ID"] = SINGLEAGENTDETAILS[0];
            Session["NAME2"] = SINGLEAGENTDETAILS[1];
            GETLOANIDDETAILS = GridView2.SelectedRow.Cells[3].Text;
            con = DB.GetConnection();
            string op = "select  ( lm.agent_Id  + '_' + Agent_Name) as AgentName,CONVERT(VARCHAR,loandate,103) AS LoanDate,loan_Id as LoanId,CONVERT(DECIMAL(18,2),loanamount) AS loanamount,NoofInstallment     from (select  CONVERT(varchar,(agent_Id)) as Agent_Id ,loandate,loan_Id,loanamount,NoofInstallment  from LoanDetails   where plant_code='" + GET[0] + "' and agent_Id='" + SINGLEAGENTDETAILS[0] + "' and loan_id='" + GETLOANIDDETAILS + "'  ) as lm  left join (select   Agent_Id,Agent_Name    from  Agent_Master where plant_code='" + GET[0] + "' and agent_Id='" + SINGLEAGENTDETAILS[0] + "') as  am on lm.agent_Id= am.Agent_Id  ORDER BY LoanId  ";
            SqlCommand cmd = new SqlCommand(op, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // da.Fill(
            da.Fill(AGENTLOANLISTWISE);
            AGENTLOANLISTWISE.Columns.Add("paiddate");
            AGENTLOANLISTWISE.Columns.Add("OpBal");
            AGENTLOANLISTWISE.Columns.Add("PaidAmount");
            AGENTLOANLISTWISE.Columns.Add("CashRecAmount");
            //AGENTLOANLISTWISE.Columns.Add("PaidEmI");
            //AGENTLOANLISTWISE.Columns.Add("PendingEmI");
            AGENTLOANLISTWISE.Columns.Add("Balance");




            string getquy = "Select CONVERT(VARCHAR,Paid_date,101) AS Paid_date from Loan_Recovery   where plant_code='" + GET[0] + "'   and agent_Id='" + SINGLEAGENTDETAILS[0] + "' AND  loan_id='" + GETLOANIDDETAILS + "'  ORDER BY  convert(datetime, Paid_date, 103) ASC  ";
            SqlCommand sc1 = new SqlCommand(getquy, con);
            SqlDataAdapter da2 = new SqlDataAdapter(sc1);
            DataSet ds = new DataSet();
            da2.Fill(ds, "PROCAM1");
            count1 = ds.Tables[0];




            string getquy1 = "Select CONVERT(VARCHAR,LoanRecovery_Date,101) AS     date from LoanDue_Recovery   where  plant_code='" + GET[0] + "'   and agent_Id='" + SINGLEAGENTDETAILS[0] + "'  and loan_id='" + GETLOANIDDETAILS + "' ORDER BY convert(datetime, LoanRecovery_Date, 103) ASC  ";
            SqlCommand sc2 = new SqlCommand(getquy1, con);
            SqlDataAdapter da3 = new SqlDataAdapter(sc2);
            //  DataSet ds2 = new DataSet();
            da3.Fill(ds, "PROCPM");
            count2 = ds.Tables[1];


            foreach (DataRow dkm in count1.Rows)
            {

                string getdd = dkm[0].ToString();

                AGENTLOANLISTWISE.Rows.Add(getdd);
            }

            foreach (DataRow dkmm in count2.Rows)
            {

                string getdd1 = dkmm[0].ToString();
                AGENTLOANLISTWISE.Rows.Add(getdd1);
            }





            if (AGENTLOANLISTWISE.Rows.Count > 0)
            {
                GridView3.DataSource = AGENTLOANLISTWISE;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = "NoRecordsFound";
                GridView3.DataBind();

            }

            //string lOANDATE = "Select  loandate  from LoanDetails   where plant_code='" + GET[0] + "'  and agent_Id='" + SINGLEAGENTDETAILS[0] + "' and loan_id='" + GETLOANIDDETAILS + "'";
            //SqlCommand sc = new SqlCommand(lOANDATE, con);
            //SqlDataAdapter da1 = new SqlDataAdapter(sc);
            //DataSet ds = new DataSet();
            //da1.Fill(ds, "TABLEop1");
            //dtloandate = ds.Tables[0];


            string Paiddate = "Select CONVERT(VARCHAR,Paid_date,101) AS Paid_date from Loan_Recovery   where plant_code='" + GET[0] + "'   and agent_Id='" + SINGLEAGENTDETAILS[0] + "' AND  loan_id='" + GETLOANIDDETAILS + "'  ORDER BY     convert(datetime,  Paid_date, 103) ASC ";
            SqlCommand sc11 = new SqlCommand(Paiddate, con);
            SqlDataAdapter da21 = new SqlDataAdapter(sc11);
            DataSet ds1 = new DataSet();
            da21.Fill(ds1, "PROCAM1");
            paiddate = ds1.Tables[0];


            string cashDate = "Select CONVERT(VARCHAR,LoanRecovery_Date,101) AS     date from LoanDue_Recovery   where  plant_code='" + GET[0] + "'   and agent_Id='" + SINGLEAGENTDETAILS[0] + "'  and loan_id='" + GETLOANIDDETAILS + "' ORDER BY   convert(datetime, LoanRecovery_Date, 103) ASC    ";
            SqlCommand sc21 = new SqlCommand(cashDate, con);
            SqlDataAdapter da31 = new SqlDataAdapter(sc21);
            //  DataSet ds2 = new DataSet();
            da31.Fill(ds1, "PROCPM");
            cashrec = ds1.Tables[1];

            commandata.Columns.Add("Date");
            foreach (DataRow r11 in paiddate.Rows)
            {
                string getdate = r11[0].ToString();

                commandata.Rows.Add(getdate);

            }


            foreach (DataRow r111 in cashrec.Rows)
            {
                string getdate = r111[0].ToString();
                commandata.Rows.Add(getdate);
            }
            foreach (DataRow readdata in commandata.Rows)
            {

                commdate = readdata[0].ToString();
                DateTime GETDD = DateTime.Parse(commdate);

                GridView3.Rows[iii].Cells[6].Text = GETDD.ToString("dd/MM/yyyy");



                //string lOANDATEAMT = "Select  loanamount  from LoanDetails   where plant_code='" + GET[0] + "'  and agent_Id='" + SINGLEAGENTDETAILS[0] + "' and loan_id='" + GETLOANIDDETAILS + "' AND loandate ='" + commdate + "'";
                //SqlCommand scamt = new SqlCommand(lOANDATE, con);
                //SqlDataAdapter da1amt = new SqlDataAdapter(scamt);
                //DataSet dsamt = new DataSet();
                //da1amt.Fill(dsamt, "TABLEop");
                //dtloandate = dsamt.Tables[0];
                //Session["loan"] = dsamt.Tables[0].Rows[0][0].ToString();

                string PaiddateAMT = "Select isnull(SUM(paid_Amount),0) as paidAmount  from Loan_Recovery   where plant_code='" + GET[0] + "'   and agent_Id='" + SINGLEAGENTDETAILS[0] + "' and  loan_id='" + GETLOANIDDETAILS + "' and Paid_date='" + commdate + "' ";
                SqlCommand sc1amt = new SqlCommand(PaiddateAMT, con);
                SqlDataAdapter da2amt = new SqlDataAdapter(sc1amt);
                DataSet dsamt = new DataSet();
                da2amt.Fill(dsamt, "AMT");
                //      paiddateamt = dsamt.Tables[0];
                Session["paid"] = dsamt.Tables[0].Rows[0][0].ToString();


                string cashDateAMT = "Select isnull(LoanDueRecovery_Amount,0) as CashReceived_Amount  from LoanDue_Recovery    where plant_code='" + GET[0] + "'   and agent_Id='" + SINGLEAGENTDETAILS[0] + "' and   loan_id='" + GETLOANIDDETAILS + "' and LoanRecovery_Date='" + commdate + "' ";
                SqlCommand sc2amt = new SqlCommand(cashDateAMT, con);
                SqlDataAdapter da3amt = new SqlDataAdapter(sc2amt);
                da3amt.Fill(dsamt, "AMT1");
                //  da3amt.Fill(dsamt, "PROCPM");
                //    cashrec = dsamt.Tables[1];
                try
                {
                    if (cashrec.Rows.Count > 0)
                    {
                        Session["cash"] = dsamt.Tables[1].Rows[0][0].ToString();
                    }
                    else
                    {
                        Session["cash"] = "0.00".ToString();
                    }
                }
                catch
                {

                    Session["cash"] = "0.00".ToString();

                }

                //  double loan = Convert.ToDouble(Session["loan"].ToString());
                paid = Convert.ToDouble(Session["paid"].ToString());
                CASH = Convert.ToDouble(Session["cash"].ToString());



                GridView3.Rows[iii].Cells[jjj].Text = paid.ToString("f2");
                jjj = jjj + 1;
                GridView3.Rows[iii].Cells[jjj].Text = CASH.ToString("f2");


                int install;

                if (iii == 0)
                {
                    double LOANAMT = Convert.ToDouble(GridView3.Rows[iii].Cells[4].Text);
                    GridView3.Rows[iii].Cells[7].Text = LOANAMT.ToString();
                }
                else
                {


                }

                try
                {
                    install = Convert.ToInt16(GridView3.Rows[iii].Cells[5].Text);

                }
                catch
                {
                    install = 0;

                }
                //double PAIDAMT = Convert.ToDouble(GridView3.Rows[iii].Cells[7].Text);
                //double CASHREC = Convert.ToDouble(GridView3.Rows[iii].Cells[8].Text);

                if (iii == 0)
                {
                    LOANAMT123 = Convert.ToDouble(GridView3.Rows[iii].Cells[4].Text);

                }
                else
                {
                    PAIDAMTBAL = Convert.ToDouble(GridView3.Rows[iii - 1].Cells[10].Text);

                    GridView3.Rows[iii].Cells[7].Text = PAIDAMTBAL.ToString();

                }

                //   double pendininst = Convert.ToDouble(LOANAMT / install);
                if (iii == 0)
                {
                    double addpaiamtCASHREC = (paid + CASH);
                    //  string GETRES = (LOANAMT - (PAIDAMT + CASHREC)).ToString("f2");
                    GETRES = (LOANAMT123 - (paid + CASH)).ToString("f2");
                    getres1 = Convert.ToDouble(GETRES);

                    //int assingemi = Convert.ToInt32(getres1 / pendininst);

                    //GridView3.Rows[iii].Cells[8].Text = (install - assingemi).ToString();

                    //GridView3.Rows[iii].Cells[9].Text = assingemi.ToString();
                    GridView3.Rows[iii].Cells[10].Text = GETRES.ToString();

                    GridView3.Rows[iii].Cells[10].Font.Bold = true;
                }
                else
                {
                    double addpaiamtCASHREC = (paid + CASH);
                    //  string GETRES = (LOANAMT - (PAIDAMT + CASHREC)).ToString("f2");
                    GETRES = (PAIDAMTBAL - (paid + CASH)).ToString("f2");
                    getres1 = Convert.ToDouble(GETRES);
                    //int assingemi = Convert.ToInt32(getres1 / pendininst);
                    //GridView3.Rows[iii].Cells[8].Text = (install - assingemi).ToString();
                    //GridView3.Rows[iii].Cells[9].Text = assingemi.ToString();
                    GridView3.Rows[iii].Cells[10].Text = GETRES.ToString();
                    GridView3.Rows[iii].Cells[10].Font.Bold = true;
                }
                if (getres1 > 1)
                {
                    GridView3.Rows[iii].Cells[10].ForeColor = System.Drawing.Color.DarkOrange;

                }

                if (getres1 > 100000.00)
                {
                    GridView3.Rows[iii].Cells[10].ForeColor = System.Drawing.Color.DarkRed;

                }
                if (getres1 < 1)
                {
                    GridView3.Rows[iii].Cells[10].ForeColor = System.Drawing.Color.DarkGreen;

                }
                if (iii != 0)
                {
                    GridView3.Rows[iii].Cells[1].Text = "";
                }

                iii = iii + 1;
                jjj = 8;

            }
            GridView3.Rows[iii].Cells[1].Text = "";
            GridView3.Rows[iii].Cells[0].Text = "";

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
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
                            
                HeaderCell2.Text = ddl_Plantname.Text + ":" + Session["ID1"].ToString() + "_" + Session["NAME1"].ToString() + "Loan Details:";
                HeaderCell2.ColumnSpan = 12;
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
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = ddl_Plantname.Text + ":" + Session["ID"].ToString() + "_" + Session["ID"].ToString() + "Loan Id:" + GETLOANIDDETAILS + "Details:";
                HeaderCell2.ColumnSpan = 11;
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
}