using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class sapcodebulkinsert : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DbHelper DB = new DbHelper();
    SqlConnection con = new SqlConnection();
    DataSet DTG = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                   
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    //  billdate();

                }
                else
                {

                    pname = ddl_Plantname.SelectedItem.Text;


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

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
        //string strr = "";

        //strr = "Select  vendorcode,agent_id,plant_code    from    saplatestvendorcode  order by rand(tid)  ";
        //con = DB.GetConnection();
        //SqlCommand cmd = new SqlCommand(strr, con);
        //SqlDataAdapter dspsap = new SqlDataAdapter(cmd);
        //DataTable getsapcode = new DataTable();
        //getsapcode.Rows.Clear();
        //dspsap.Fill(getsapcode);
        //foreach (DataRow drs in getsapcode.Rows)
        //{
        //    string vendor = drs[0].ToString();
        //    string[] agent = drs[1].ToString().Split('-');
        //    string filteragent = agent[0].ToString();
        //    string loan = agent[1].ToString();
        //    string plant_code = drs[2].ToString().Trim();
        //    string insert = "insert into saplatestvendorcode1(vendorcode,agent_id,loan_id,plant_code) values ('" + vendor + "','" + filteragent + "','" + loan + "','" + plant_code + "')";
        //    con = DB.GetConnection();
        //    SqlCommand cmd12 = new SqlCommand(insert, con);
        //    cmd12.ExecuteNonQuery();

        //}

        //string strr = "";
        //strr = "sELECT   agent_id,loan_id,vendorcode AS saploancode,plant_code   FROM saplatestvendorcode1  where plant_code='"+ddl_Plantname.SelectedItem.Value+"'";
        //con = DB.GetConnection();
        //SqlCommand cmd = new SqlCommand(strr, con);
        //SqlDataAdapter dspsap = new SqlDataAdapter(cmd);
        //DataTable getsapcode = new DataTable();
        //getsapcode.Rows.Clear();
        //dspsap.Fill(getsapcode);
        //foreach (DataRow drs in getsapcode.Rows)
        //{

        //    string agentid = drs[0].ToString();
        //    string loanid = drs[1].ToString();
        //    string sapcode = drs[2].ToString();
        //    string pcode = drs[3].ToString().Trim();
        //    string insert = "update LoanDetails set saploanid='" + sapcode + "' where plant_code='" + pcode + "' and agent_Id='" + agentid + "'    and loan_Id='" + loanid + "'";
        //    con = DB.GetConnection();
        //    SqlCommand cmd12 = new SqlCommand(insert, con);
        //    cmd12.ExecuteNonQuery();
        //}

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}