using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class cowupdation : System.Web.UI.Page
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
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    string month;
    int Checkstatus;
    string Tallyno;
    string BILLDATE;
    double ltr;
    string conltr;
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
                    txt_FromDate.Text = dtm.ToString("MM/dd/yyyy");
                    txt_ToDate.Text = dtm.ToString("MM/dd/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                }
                else
                {
                    pname = ddl_Plantname.SelectedItem.Text;
                }
            }
            else
            {
                ccode = Session["Company_code"].ToString();
                pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {
        }
    }

    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            DateTime dtfromdate = Convert.ToDateTime(txt_FromDate.Text);
            DateTime dttodate = Convert.ToDateTime(txt_ToDate.Text);
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (ddl_Plantname.SelectedItem.Value == "168")
            {
                SqlCommand cmd = new SqlCommand("update Procurementimport set Plant_Code='171' where Plant_code='168' AND Prdate BETWEEN '" + dtfromdate + "' AND '" + dttodate + "' AND Fat < '4.5'", conn);
                cmd.ExecuteNonQuery();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update Procurementimport set Plant_Code='172' where Plant_code='166' AND Prdate BETWEEN '" + dtfromdate + "' AND '" + dttodate + "' AND Fat < '4.5'", conn);
                cmd.ExecuteNonQuery();
            }
            lblmsg.Text = "Recards Updated Successfully";
        }
        catch
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }
}