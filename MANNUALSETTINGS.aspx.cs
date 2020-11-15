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

public partial class MANNUALSETTINGS : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                ccode = Session["Company_code"].ToString();
                //    pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());

                dtm = System.DateTime.Now;
                txt_date.Text = dtm.ToShortDateString();
                txt_mandate.Text = dtm.ToShortDateString();
                txt_date.Text = dtm.ToString("dd/MM/yyyy");
                txt_mandate.Text = dtm.ToString("dd/MM/yyyy");
                gettid();

                LoadPlantcode();
                gridview();
            }

            else
            {
                //dtm = System.DateTime.Now;
                //// dti = System.DateTime.Now;
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyy");

                pcode = ddl_Plantcode.SelectedItem.Value;
                //  LoadPlantcode();
                gridview();
              //  getrouteid();

            }

        }
        else
        {


            pcode = ddl_Plantcode.SelectedItem.Value;
            //  LoadPlantcode();
            //   ddl_agentcode.Text = getrouteusingagent;
            gridview();
        }
    }

    public void gettid()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            //SqlDataReader dr = null;
            //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            try
            {
                string sqlstr = "SELECT max(Tid) as  Tid  FROM MannualSettings  ";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                int id = Convert.ToInt16(ds.Tables[0].Rows[0]["tid"]);
                tid.Text = (id + 1).ToString();
            }
            catch
            {
                tid.Text = "1";


            }
        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if ((ddl_Plantname.Text != string.Empty) && (txt_date.Text != string.Empty) && (txt_mandate.Text != string.Empty) && (ddl_sess.Text != string.Empty) && (txt_requst.Text != string.Empty) && (txt_giver.Text != string.Empty))
        {
            INSERT();
        }
        else
        {

            WebMsgBox.Show("Please Fill Above Empty Fields");

        }
        gridview();
        gettid();
        clear();
    }
    public void gridview()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_mandate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
              //  Plant_code,Plant_Name,convert(varchar,PermissionDate,103) as PermissionDate,convert(varchar,ManualDate,103) as ManualDate,MannualSession,RequsterName,GivererName,ReasonForMannual
                string sqlstr = "SELECT Tid,Plant_code,convert(varchar,PermissionDate,103) as PermissionDate,convert(varchar,ManualDate,103) as ManualDate,MannualSession,RequsterName,GivererName,ReasonForMannual  FROM MannualSettings WHERE plant_code='" + pcode + "'  order by  Tid desc";
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }

            }
        }
        catch
        {

        }
    }
    public void clear()
    {

      //  ddl_Plantname.Text = "  --------Select--------";
        ddl_sess.Text = "--------Select--------";
        txt_requst.Text = "";
        txt_giver.Text = "";
        txt_Reason.Text = "";
    }

    public void INSERT()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_mandate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        //  string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);

        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO MannualSettings (Plant_code,Plant_Name,PermissionDate,ManualDate,MannualSession,RequsterName,GivererName,ReasonForMannual) VALUES ('" + ddl_Plantcode.Text + "','" + ddl_Plantname.Text + "','" + d1 + "','" + d2 + "','" + ddl_sess.Text + "','" + txt_requst.Text + "','" + txt_giver.Text + "','" + txt_Reason.Text + "')";
        cmd.Connection = conn;
        conn.Open();
        cmd.ExecuteNonQuery();
        WebMsgBox.Show("inserted Successfully");
        conn.Close();
        gettid();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         //   ddl_agentcode.Items.Clear();

            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            pcode = ddl_Plantcode.SelectedItem.Value;
           gridview();

        }
        else
        {

           // ddl_agentcode.Items.Clear();
            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            pcode = ddl_Plantcode.SelectedItem.Value;
            //getagentid();
           gridview();
        }
    }
}