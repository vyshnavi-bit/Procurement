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

public partial class RateChartAssignToAgents : System.Web.UI.Page
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
    DataSet dt = new DataSet();
    DataSet dtp = new DataSet();
    int i = 0;
    int j = 0;
    string getplant;
    DataTable DFF = new DataTable();
    DataTable df = new DataTable();
    DataSet DTGGG = new DataSet();
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


                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    billdate();
                    // getagentData();          
                     ddl_cow.Visible =false;
                     ddl_buff.Visible = false;
                     btn_update.Visible = false;

                }
                else
                {

                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                pcode = ddl_Plantname.SelectedItem.Value;
                billdate();
                //  getagentData();

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
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139) and plant_code  in (" + pcode + ") ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
    }

    public void getagentData()
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
        string str = "";
        str = "select  pro.agent_id as CanNo,agent_name as AgentName,cowchartname as CowChart,buffchartname as BuffChart   from ( select agent_id    from procurementimport    where plant_code='" + pcode + "'   and prdate between '" + FDATE + "' and '" + TODATE + "'  group by agent_id) as pro  left join (select   agent_id,Agent_name,cowchartname,buffchartname   from agent_master   where plant_code='" + pcode + "'   group by agent_id ,Agent_name,cowchartname,buffchartname)  as am on pro.agent_id=am.agent_id  order by rand(pro.agent_id)";
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter DAD = new SqlDataAdapter(cmd);
        DAD.Fill(DTG, "TABLEop");
        GridView1.DataSource = DTG.Tables[0];
        GridView1.DataBind();


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

            str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where     plant_code='" + pcode + "'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";



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

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        getagentData();
        getchart();
        ddl_cow.Visible = true;
        ddl_buff.Visible = true;
        btn_update.Visible = true;
    }

    public void getchart()
    {

        string str = "";
        string str1 = "";
        con = DB.GetConnection();
        str = "SELECT Chart_Name FROM Chart_Master where Plant_Code='" + pcode + "' and Milk_Nature='Cow' and active=1";
        SqlCommand cmdd = new SqlCommand(str, con);
        SqlDataAdapter DAA = new SqlDataAdapter(cmdd);
        DTG.Tables.Clear();
        DAA.Fill(DTG);
        ddl_cow.DataSource = DTG.Tables[0];
        ddl_cow.DataTextField = "Chart_Name";
        ddl_cow.DataValueField = "Chart_Name";
        ddl_cow.DataBind();
        str1 = "SELECT Chart_Name FROM Chart_Master where Plant_Code='" + pcode + "' and Milk_Nature='Buffalo' and active=1";
        SqlCommand cmddd = new SqlCommand(str1, con);
        SqlDataAdapter DAAA = new SqlDataAdapter(cmddd);
        DTG.Tables.Clear();
        DAAA.Fill(DTG);
        ddl_buff.DataSource = DTG.Tables[0];
        ddl_buff.DataTextField = "Chart_Name";
        ddl_buff.DataValueField = "Chart_Name";
        ddl_buff.DataBind();


    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;

        }

    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                      string can = row.Cells[1].Text;
                      string RATEUPDATE = "Update Agent_master   set cowchartname='" + ddl_cow.SelectedItem.Text + "',buffchartname='" + ddl_buff.SelectedItem.Text + "'  where plant_code='" + pcode + "' and agent_id='" + can + "'  ";
                      con = DB.GetConnection();
                      SqlCommand cmd111 = new SqlCommand(RATEUPDATE, con);
                      cmd111.ExecuteNonQuery();

                    }



                }

            }

        }
        catch
        {



        }

        getagentData();

    }


    protected void btn_update_Click1(object sender, EventArgs e)
    {

    }
}