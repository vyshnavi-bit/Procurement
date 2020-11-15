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


public partial class AgentRatechartStatus : System.Web.UI.Page
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                //    pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();


                dtm = System.DateTime.Now;
                txt_date.Text = dtm.ToShortDateString();
                txt_mandate.Text = dtm.ToShortDateString();
                txt_date.Text = dtm.ToString("dd/MM/yyyy");
                txt_mandate.Text = dtm.ToString("dd/MM/yyyy");
              //  gettid();

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
             //   string sqlstr = "select *  FROM procurement WHERE plant_code='" + pcode + "'  and agent_id='"+ddl_agentcode.Text+"'  and prdate between '"+d1+"' and '"+d2+"' order by  tid desc";

                string sqlstr = " Select * from (select prob.Agent_id,proa.Ratechartname,convert(varchar,prob.Prdate,103) as Prdate,prob.RateStatus   from (select distinct(ratechart_id) as Ratechartname FROM procurement WHERE plant_code='" + pcode + "'  and agent_id='" + ddl_agentcode.Text + "' and prdate between '" + d1 + "' and '" + d2 + "' group by ratechart_id)as proa left join ( select * FROM procurement WHERE plant_code='" + pcode + "'  and agent_id='" + ddl_agentcode.Text + "') as  prob on proa.Ratechartname=prob.Ratechart_Id  group by prob.Agent_id,proa.Ratechartname,prob.Prdate,prob.RateStatus ) as procc  ";
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

    protected void txt_date_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        gridview();
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //   ddl_agentcode.Items.Clear();

            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            pcode = ddl_Plantcode.SelectedItem.Value;
            //   gridview();

        }
        else
        {

            // ddl_agentcode.Items.Clear();
            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            pcode = ddl_Plantcode.SelectedItem.Value;
            getagentid();
            //gridview();
        }
    }

    public void getagentid()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from procurementimport where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = " select distinct agent_id from procurementimport where plant_code='" + pcode + "'   order by Agent_id asc   ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddl_agentcode.Items.Add(dr["agent_id"].ToString());


            }


        }

        catch
        {

            WebMsgBox.Show("PLEASE CHECK");

        }



    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridview();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        HtmlForm form = new HtmlForm();
 
    Response.Clear();
 
    Response.Buffer = true;
 
  //  string filename="GridViewExport_"+DateTime.Now.ToString()+".xls";

    string filename = pcode +"-" + "Agent Id:"+ddl_agentcode.Text + "RateChartName:" + DateTime.Now.ToString() + ".xls";
  
    Response.AddHeader("content-disposition",
 
    "attachment;filename="+filename);
 
    Response.Charset = "";
 
    Response.ContentType = "application/vnd.ms-excel";
 
    StringWriter sw = new StringWriter();
 
    HtmlTextWriter hw = new HtmlTextWriter(sw);
    GridView1.AllowPaging = false;
 
    GridView1.DataBind();
 
    form.Controls.Add(GridView1);
 
    this.Controls.Add(form);
 
    form.RenderControl(hw);
 
    //style to format numbers to string
 
    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
 
    Response.Write(style);
 
    Response.Output.Write(sw.ToString());
 
    Response.Flush();
 
    Response.End();




    }
}