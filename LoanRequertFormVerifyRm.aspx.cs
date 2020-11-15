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

public partial class LoanRequertFormVerifyRm : System.Web.UI.Page
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
    DataTable AGENTLOANCOUNT = new DataTable();
    string[] SINGLEAGENT;
    string PARTICUAGENT;
    string GETLOANID;
    int ii = 0;
    int jj = 6;
    int i = 0;
    int j = 0;
    string[] GETAGENTIDWITHNAME;
    DateTime Date;
    string getdate;
    int plant;
    double getloan;
    int emi;
    string DFDF;
    string GETAGENT;
    string[] ADATE;
    string MM;
    string DD;
    string YY;
    string CONDATE;
    int I;
    int billdays;
    string AGENTID;
    int updateStatus;
    int GETREF;
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
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                 


                    Button2.Visible = false;
                    if (roleid < 3)
                    {

                   
                    }

                    else
                    {
                     
                        GETGRID();

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
                //pcode = ddl_Plantcode.SelectedItem.Value;
                //plant = ddl_Plantname.Text;
                //GET = plant.Split('_');


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
        string loanamt = "SELECT Tid as refid,Plant_code,plant_name,Agent_id,Agent_name,convert(varchar,RequestingDate,103) as RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount,Description   FROM LoanRequestEntry WHERE     ManStatus=1 and  RmStatus  is null";
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


        }
        else
        {
            Button2.Visible = false;
            Button3.Visible = false;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

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
         
                    try
                    {
                      
                      
                       
                           string getupdate;
                           con = db.GetConnection();
                           getupdate = "Update LoanRequestEntry set  RmUpdateTime='" + System.DateTime.Now + "',RmStatus='1'  where plant_code='" + pcode + "' and Agent_id='" + agentid + "' and   Tid='" + ID + "'";
                           SqlCommand cmd = new SqlCommand(getupdate, con);
                           cmd.ExecuteNonQuery();
                           Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.verified) + "')</script>");
                    }
                    catch (Exception EX)
                    {



                    }

                    GETGRID();
                }
            }
        }



    }











    protected void Button3_Click(object sender, EventArgs e)
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

                    try
                    {



                        string getupdate;
                        con = db.GetConnection();
                        getupdate = "Update LoanRequestEntry set  RmUpdateTime='" + System.DateTime.Now + "',RmStatus='2'  where plant_code='" + pcode + "' and Agent_id='" + agentid + "' and   Tid='" + ID + "'";
                        SqlCommand cmd = new SqlCommand(getupdate, con);
                        cmd.ExecuteNonQuery();
                        Response.Write("<script language='javascript'>alert('"+Server.HtmlEncode(getclass.Reject)+"')</script>");

                    }
                    catch (Exception EX)
                    {



                    }

                    GETGRID();
                }
            }
        }

    }
}