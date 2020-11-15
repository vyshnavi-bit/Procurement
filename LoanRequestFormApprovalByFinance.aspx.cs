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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class LoanRequestFormApprovalByFinance : System.Web.UI.Page
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

                }
                else
                {



                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();

                GridView3.Visible = false;

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
 //  HIDE  //   string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount,Description   FROM LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus is null";

      string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,LoanAmount   FROM LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus is null";
     //  string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,LoanAmount   FROM LoanRequestEntry ";
        SqlCommand sc = new SqlCommand(loanamt, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        da1.Fill(ds, "TABLEop");
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {

            Button2.Visible = true;
            I = 1;
            Button3.Visible = true;

        }
        else
        {
            Button2.Visible = false;
            Button3.Visible = false;
        }
    }


    public void GETGRID1()
    {
        con = db.GetConnection();


        //  string loanamt = "SELECT  Plant_code,Plant_code as PlantName,Agent_id,Agent_id as AgentName,CONVERT(VARCHAR,RequestingDate,103) AS RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi   FROM LoanRequestEntry    WHERE  ManStatus='1' AND RmStatus=0   order by tid  desc ";
        // string loanamt = "select Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT  Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE PLANT_CODE='"+pcode+"' AND ManStatus=1 AND RmStatus!=1 ) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='"+pcode+"' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='165' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        //   string loanamt = " select Tid AS RefId, Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT Tid,lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT Tid,Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE Plant_code='" + pcode + "' AND ManStatus=1 and  RmStatus NOT IN(1)) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='" + pcode + "' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='" + pcode + "' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        //  HIDE  //   string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount,Description   FROM LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus is null";

        //     string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,LoanAmount   FROM LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus is null";
        string loanamt = "SELECT AGGG.Plant_code,Plant_name,AGGG.Agent_id,Agent_name,Bank_Name,Agent_AccountNo,AGGG.Ifsc_code,RequestingDate,LoanAmount    FROM (select refid,AGG.Plant_code,Plant_name,AGG.Agent_id,Agent_name,Agent_AccountNo,Ifsc_code,RequestingDate,LoanAmount   from (SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,LoanAmount   FROM LoanRequestEntry WHERE   ManStatus=1 and  RmStatus=1   and LoanVerifyChennaiStatus=1 and FinanceApprovalStatus is null) as agg left join (select   Agent_AccountNo,Ifsc_code,Plant_code,Agent_Id  from Agent_Master  GROUP BY Plant_code,Agent_AccountNo,Ifsc_code,Agent_Id ) AS AM ON AGG.Plant_code=AM.Plant_code AND AGG.Agent_id=AM.Agent_Id) AS AGGG LEFT JOIN (SELECT Bank_Name,Bank_ID,Ifsc_code,Plant_code   FROM BANK_MASTER  GROUP BY  Plant_code,Bank_ID,Ifsc_code,Bank_Name )AS BM ON AGGG.Plant_code=BM.Plant_code AND  AGGG.Ifsc_code=BM.Ifsc_code";
        SqlCommand sc1 = new SqlCommand(loanamt, con);
        SqlDataAdapter da2 = new SqlDataAdapter(sc1);
        DataTable dtt = new DataTable();
        da2.Fill(dtt);
        GridView3.DataSource = dtt;
        GridView3.DataBind();

        if (dtt.Rows.Count > 0)
        {
            GridView3.DataSource = dtt;
            GridView3.DataBind();
            Button2.Visible = true;
            I = 1;


        }
        else
        {
            Button2.Visible = false;
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

                    string ID = row.Cells[1].Text;
                    string pcode = row.Cells[2].Text;
                    string agentid = row.Cells[4].Text;

                    con = db.GetConnection();
                    string proam = "SELECT  Bank_Id,Ifsc_code,Agent_AccountNo   FROM Agent_Master  WHERE Plant_code='" + pcode + "' AND Agent_Id='" + agentid + "'";
                    SqlCommand sc1 = new SqlCommand(proam, con);
                    SqlDataAdapter da2 = new SqlDataAdapter(sc1);
                    DataSet ds1 = new DataSet();
                    da2.Fill(ds1, "PROCAM");
                    ViewState["BANK_ID"] = ds1.Tables[0].Rows[0][0].ToString();
                    ViewState["Ifsc_code"] = ds1.Tables[0].Rows[0][1].ToString();
                    ViewState["Agent_AccountNo"] = ds1.Tables[0].Rows[0][2].ToString();

                    string proam1 = "SELECT  BANK_NAME   FROM Bank_Details  WHERE Bank_id='" + ViewState["BANK_ID"] + "'";
                    SqlCommand sc11 = new SqlCommand(proam1, con);
                    SqlDataAdapter da21 = new SqlDataAdapter(sc11);
                    da21.Fill(ds1, "PROCBANK_NAME");
                    ViewState["BANK_NAME"] = ds1.Tables[1].Rows[0][0].ToString();
                   
                    try
                    {



                        string getupdate;
                        con = db.GetConnection();
                        getupdate = "Update LoanRequestEntry set  FinanceDatetime='" + System.DateTime.Now + "',FinanceApprovalStatus=1,BankName='" + ViewState["BANK_NAME"] + "',Ifsc_code='" + ViewState["Ifsc_code"] + "',Agent_AccountNo='" + ViewState["Agent_AccountNo"] + "'  where plant_code='" + pcode + "' and Agent_id='" + agentid + "' and   Tid='" + ID + "'";
                        SqlCommand cmd = new SqlCommand(getupdate, con);
                        cmd.ExecuteNonQuery();
                        Response.Write("<script language='javascript'>alert('"+Server.HtmlEncode(getclass.verified) +"') </script>");
                    }
                    catch (Exception EX)
                    {



                    }

                    GETGRID();
                }
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }



    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            GridView3.Visible = true;
            GETGRID1();
            GridView3.Visible = true;
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= '" + "AgnetLoan" + "'.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView3.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
}