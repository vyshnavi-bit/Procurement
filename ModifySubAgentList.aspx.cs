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
using System.Net;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using System.Text;

public partial class ModifySubAgentList : System.Web.UI.Page
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
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable dttp = new DataTable();
    string FDATE;
    string TODATE;
    public string frmdate;
    public string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    SqlDataReader dr;
    int datasetcount = 0;
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataTable fatkgdt = new DataTable();
    DataTable agent = new DataTable();
    string d1;
    string d2;
    DateTime d11;
    DateTime d22;
    string agentcode;
    int countinsertdetails;
    string sttr;

    string planttype;
    string planttypehdfc;
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
                    //   txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        billdate();
                        getagentid();

                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        billdate();
                        getagentid();
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

                ViewState["pcode"] = pcode.ToString();
            }

        }

        catch
        {


        }
    }
    public void getagentid()
    {
        try
        {
            string getagent;
            con = DB.GetConnection();
            getagent = "Select agent_id   from Agent_master  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and subagent='1'  GROUP BY  agent_id ";
            SqlCommand cmd = new SqlCommand(getagent, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            agent.Rows.Clear();
            da.Fill(agent);
            ddl_getagent.DataSource = agent;
            ddl_getagent.DataTextField = "agent_id";
            ddl_getagent.DataValueField = "agent_id";
            ddl_getagent.DataBind();
        }
        catch
        {
        }
    }
    public void loadsingleplant()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
            datasetcount = datasetcount + 1;
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
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
            datasetcount = datasetcount + 1;
        }
        catch
        {

        }
    }
    public void GETBILLDATE()
    {
        try
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
        }
        catch
        {

        }
    }
    public void billdate()
    {
        try
        {

            con.Close();
            con = DB.GetConnection();
            string str;

            str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    d11 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d22 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d11.ToString("dd/MM/yyy");
                    Todate = d22.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);

                }

            }
        }
        catch
        {

        }
    }

    public void billBillpaymentDetails()
    {
        try
        {
            GETBILLDATE();
            con = DB.GetConnection();
            string stt = "";
            stt = "Select  Tid,agent_id,Agent_Name,Bank_Name,ifsc_code,Agent_accountNo,Netamount    from  SubAgnetpaymentdetails    where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Agent_id='" + ddl_getagent.SelectedItem.Value + "'  and Frm_date='" + FDATE + "'  and To_date='" + TODATE + "'";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            if (DTG.Tables[datasetcount].Rows.Count > 0)
            {
                GridView1.DataSource = DTG.Tables[datasetcount];
                GridView1.DataBind();
                GridView1.FooterRow.Cells[6].Text = "Total Amount";
                decimal milkkg = DTG.Tables[datasetcount].AsEnumerable().Sum(row => row.Field<decimal>("Netamount"));
                GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[7].Text = milkkg.ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch
        {

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        billBillpaymentDetails();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        billBillpaymentDetails();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        billBillpaymentDetails();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            con = DB.GetConnection();
            GETBILLDATE();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
           // TextBox agentid = (TextBox)row.Cells[2].Controls[0];
            TextBox agname = (TextBox)row.Cells[3].Controls[0];
            TextBox baname = (TextBox)row.Cells[4].Controls[0];
            TextBox ifsc = (TextBox)row.Cells[5].Controls[0];
            TextBox acc = (TextBox)row.Cells[6].Controls[0];
            TextBox netamt = (TextBox)row.Cells[7].Controls[0];

            string ste = "update   SubAgnetpaymentdetails    set  Agent_Name='" + agname.Text + "',Bank_Name='" + baname.Text + "',ifsc_code='" + ifsc.Text + "',Agent_accountNo='" + acc.Text + "',Netamount ='" + netamt.Text + "'   where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Frm_date='" + FDATE + "'  and To_date='" + TODATE + "' and tid ='" + userid + "'";
            SqlCommand cmd=new SqlCommand(ste,con);
            cmd.ExecuteNonQuery();
          
            //string STT = "Updated SuceeFully";
            //Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(STT.ToString()) + "')</script>");
            GridView1.EditIndex = -1;
            billBillpaymentDetails();
        }
        catch (Exception Ex)
        {
         
        }
        finally
        {
            con.Close();
        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        getagentid();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void Button10_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

        }
    }
}