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

public partial class NewLoanVoucher : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con=new SqlConnection();
    DbHelper DB = new DbHelper();
  //  DataTable dt = new DataTable();
    DataSet dt = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
       


        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                 lblTitle.Text = Session["TitleName"].ToString();

                 lblAddress.Text = Session["Address"].ToString();
               
                dtm = System.DateTime.Now;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                pnlContents.Visible = false;
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();

                }

                else
                {

                    LoadPlantcode();
                }
               
            }
            else
            {



            }

        }


        else
        {
            ccode = Session["Company_code"].ToString();
            pcode = ddl_Plantname.SelectedItem.Value;
            pname = ddl_Plantname.SelectedItem.Text;

         
         //   pname = ddl_Plantname.SelectedItem.Text;
        

        }

    }

    public void loadsingleplant()
    {

        con=DB.GetConnection();
        string stt="SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '"+pcode+"'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA=new SqlDataAdapter(cmd);
       
        DA.Fill(dt);
        ddl_Plantname.DataSource = dt.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }

    public void AgentId()
    {

        con = DB.GetConnection();
        string stt = "Select agent_id,agent_Name from LoanRequestEntry   where   PLANT_CODE NOT IN (139,150)  AND    plant_code='" + pcode + "' ORDER BY RAND(AGENT_ID) ASC";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);

        DA.Fill(dt);
        ddl_agentname.DataSource = dt.Tables[0];
        ddl_agentname.DataTextField = "agent_id";
        ddl_agentname.DataValueField = "agent_id";
        ddl_agentname.DataBind();

    }



    public void LoadPlantcode()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE PLANT_CODE NOT IN (139,150) order by Plant_Code";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(dt);
        ddl_Plantname.DataSource = dt.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }


    public void getformview()
    {
        con = DB.GetConnection();
        string stt = "select  Plant_name,Agent_id,Agent_name,CONVERT(VARCHAR,RequestingDate,103) AS RequestingDate,CONVERT(VARCHAR,FromDate,103) AS FromDate,LoanAmount,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount,Emi,LoanId,BankName,Agent_AccountNo,Ifsc_code   from LoanRequestEntry   where Plant_code='" + pcode + "' and Agent_id='" + ddl_agentname.SelectedItem.Value + "' and loanid='"+ddl_loanid.Text+"'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(dt);
        if (dt.Tables[0].Rows.Count > 0)
        {
            FormView1.DataSource = dt.Tables[0];
            FormView1.DataBind();
            pnlContents.Visible = true;
            Label7.Text = ddl_Plantname.SelectedItem.Text;
            Label6.Text = "PlantName:";
            Label7.Visible = true;
            Label6.Visible = true;
            txt_FromDate.Text = System.DateTime.Now.ToString();

        }
        else
        {

            pnlContents.Visible = false;
            Label7.Visible = false;
            Label6.Visible = false;
        }
    }
   
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        getformview();
    }

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AgentId();
            agentid = ddl_agentname.SelectedItem.Value;
            loanId();
        }
        catch
        {


        }
    }

    protected void ddl_agentname_SelectedIndexChanged(object sender, EventArgs e)
    {
        loanId();
    }

    public void loanId()
    {

        con = DB.GetConnection();
        string stt = "Select Loanid from LoanRequestEntry   where   PLANT_CODE NOT IN (139,150)  AND    plant_code='" + pcode + "' and agent_id='" + ddl_agentname.SelectedItem.Value + "' ORDER BY RAND(AGENT_ID) ASC";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DataSet DAP = new DataSet();
        DA.Fill(DAP);
        ddl_loanid.DataSource = DAP.Tables[0];
        ddl_loanid.DataTextField = "Loanid";
        ddl_loanid.DataValueField = "Loanid";
        ddl_loanid.DataBind();

    }
}