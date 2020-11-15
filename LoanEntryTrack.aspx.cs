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
public partial class LoanEntryTrack : System.Web.UI.Page
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
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    DateTime d1;
    DateTime d2;
    string DATE;
    string DATE1;
    string DATE2; // JAN


  

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
                //    GETGRID();


                    if (roleid < 3)
                    {
                        loadsingleplant();
                        Button5.Visible = true;
                        ddl_Plantname.Visible = true;
                        lad.Visible = false;
                    }

                    else
                    {
                        
                        LoadPlantcode();
                        Button5.Visible = false;
                        ddl_Plantname.Visible = false;
                        lad.Visible = false;
                    }

                }
                else
                {



                }
                billdate();
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;

                if (roleid > 2)
                {
                    Button5.Visible = false;
                    ddl_Plantname.Visible = false;
                    lad.Visible = false;
                }
                else
                {
                    Button5.Visible = true;
                    ddl_Plantname.Visible = true;
                    lad.Visible = false;
                }
            }
        }

        catch
        {



        }
    }

    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }


    public void loadsingleplant()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }

    public void GETGRID()
    {
        con = db.GetConnection();


        //  string loanamt = "SELECT  Plant_code,Plant_code as PlantName,Agent_id,Agent_id as AgentName,CONVERT(VARCHAR,RequestingDate,103) AS RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi   FROM LoanRequestEntry    WHERE  ManStatus='1' AND RmStatus=0   order by tid  desc ";
        // string loanamt = "select Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT  Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE PLANT_CODE='"+pcode+"' AND ManStatus=1 AND RmStatus!=1 ) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='"+pcode+"' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='165' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        //   string loanamt = " select Tid AS RefId, Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT Tid,lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT Tid,Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE Plant_code='" + pcode + "' AND ManStatus=1 and  RmStatus NOT IN(1)) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='" + pcode + "' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='" + pcode + "' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        string loanamt;
        if (roleid > 3)
        {
            loanamt = "select Tid as  refid,plant_name as pname,Agent_id as Agentid,agent_name as Agentname,RequestingDate as Requestdate,LoanAmount,Emi,TotalinterestAmount as IntrestAmt,TotalAmount as TotAmt,InstallAmount as RecAMt,ManStatus,RmStatus,RmUpdateTime,LoanVerifyChennaiStatus as Officestatus,LoanVerifyDate,FinanceApprovalStatus as FinanaceStatus,FinanceDatetime as FinaceTime,FinalApprovalStaus as AppravalStat,FinalApprovaldatetime as FinalTime,FromDate as Loandate   from  LoanRequestEntry     where RequestingDate  between '" + ViewState["FDATE"] + "'  and '" + ViewState["TODATE"] + "'      ";
        }
        else
        {
            loanamt = "select Tid as  refid,plant_name as pname,Agent_id as Agentid,agent_name as Agentname,RequestingDate as Requestdate,LoanAmount,Emi,TotalinterestAmount as IntrestAmt,TotalAmount as TotAmt,InstallAmount as RecAMt,ManStatus,RmStatus,RmUpdateTime,LoanVerifyChennaiStatus as Officestatus,LoanVerifyDate,FinanceApprovalStatus as FinanaceStatus,FinanceDatetime as FinaceTime,FinalApprovalStaus as AppravalStat,FinalApprovaldatetime as FinalTime,FromDate as Loandate   from  LoanRequestEntry where plant_code='" + pcode + "'   AND RequestingDate  between  '" + ViewState["FDATE"] + "'  and   '" + ViewState["TODATE"] + "'";

        }
        SqlCommand sc = new SqlCommand(loanamt, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        da1.Fill(ds, "TABLEop");
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {


        }
        else
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
            string str;
            if (roleid > 2)
            {

                  str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

            }
            else
            {
                  str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  and plant_code='"+pcode+"'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

            }
;
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Loan Tracking Details:";
            HeaderCell2.ColumnSpan = 21;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

          int manok =   Convert.ToInt16(e.Row.Cells[11].Text);
          string rm = (e.Row.Cells[12].Text);
          string chennaioff = (e.Row.Cells[14].Text);
          string Finance = (e.Row.Cells[16].Text);
          string Final = (e.Row.Cells[18].Text);
          if (manok == 1)
          {

              e.Row.Cells[11].Text = "Verified";

              e.Row.Cells[11].ForeColor = System.Drawing.Color.Green;
          }
          else
          {

              e.Row.Cells[11].Text = "Pending";
              e.Row.Cells[11].ForeColor = System.Drawing.Color.Orange;

          }

          if (rm == "&nbsp;")
          {

              e.Row.Cells[12].Text = "Pending";

              e.Row.Cells[12].ForeColor = System.Drawing.Color.Orange;
          }
          if (rm == "1")
          {

              e.Row.Cells[12].Text = "Verified";
              e.Row.Cells[12].ForeColor = System.Drawing.Color.Green;

          }
          if (rm == "2")
          {

              e.Row.Cells[12].Text = "Reject";
              e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;

          }


          if (chennaioff == "&nbsp;")
          {

              e.Row.Cells[14].Text = "Pending";

              e.Row.Cells[14].ForeColor = System.Drawing.Color.Orange;
          }
          if (chennaioff == "1")
          {

              e.Row.Cells[14].Text = "Verified";
              e.Row.Cells[14].ForeColor = System.Drawing.Color.Green;

          }

          if (Finance == "&nbsp;")
          {

              e.Row.Cells[16].Text = "Pending";

              e.Row.Cells[16].ForeColor = System.Drawing.Color.Orange;
          }
          if (Finance == "1")
          {

              e.Row.Cells[16].Text = "Verified";
              e.Row.Cells[16].ForeColor = System.Drawing.Color.Green;

          }

          if (Final == "&nbsp;")
          {

              e.Row.Cells[18].Text = "Pending";

              e.Row.Cells[18].ForeColor = System.Drawing.Color.Orange;
          }
          if (Final == "1")
          {

              e.Row.Cells[18].Text = "Verified";
              e.Row.Cells[18].ForeColor = System.Drawing.Color.Green;

          }




        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
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
        GETGRID();



        if (roleid < 3)
        {
            
            lad.Visible = false;
        }

        else
        {

           
            lad.Visible = false;
        }


    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}