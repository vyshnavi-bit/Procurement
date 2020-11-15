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

public partial class FatSnfModifycation : System.Web.UI.Page
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
    DataTable dateviews = new DataTable();
    DataTable filename = new DataTable();
    DataTable viewloanrecovery = new DataTable();
    DataTable dtexistsornot = new DataTable();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;

    string getvald1;
    string getvalm1;
    string getvaly1;
    string jvdatest;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    string getid;
    string rroute;
    string fatadd;
    string snfadd;
    string fatsnf;
    string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
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
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    GridView2.Visible = false;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    billdate();
                    ddl_Route.Visible = false;
                    //getii();
                    //gertfilename();
                    //getreport();

                    txt_fat.Visible = false;
                    txt_snf.Visible = false;
                    Label1.Visible = false;
                    Label2.Visible = false;
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
    public void billdate()
    {
        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            
            con = new SqlConnection(connection);
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str;
            if (roleid == 4)
            {
                str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Adminupdate=1  order by  Bill_frmdate desc";
            }
            else
            {

                str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  order by  Bill_frmdate desc";

            }
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
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
    public void LoadPlantcode()
    {
        try
        {
            con = new SqlConnection(connection);
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
    public void getdatefuntion()
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

    }
    protected void btn_get_Click(object sender, EventArgs e)
    {
        try
        {
            getdata();
            GridView2.Visible = true;
        }
        catch
        {


        }
    }
    public void getdata()
    {

        try
        {
            getdatefuntion();
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

            if (Rto_btn.SelectedItem.Value == "0")
            {
                rroute = "Select Agent_id,convert(varchar,prdate,103) as Date,sessions,Fat,Snf  from  procurementimport   where plant_code='" + pcode + "' and route_id='" + ddl_Route.SelectedItem.Value + "'  and prdate between '" + FDATE + "' and '" + TODATE + "'  ORDER BY  Agent_id,prdate,sessions ASC ";
            }
            else
            {
                rroute = "Select Agent_id,convert(varchar,prdate,103) as Date,sessions,Fat,Snf  from  procurementimport   where plant_code='" + pcode + "'   and prdate between '" + FDATE + "' and '" + TODATE + "'  ORDER BY  Agent_id,prdate,sessions ASC ";
            }
            con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(rroute, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtt = new DataTable();
            da.Fill(dtt);
            if (dtt.Rows.Count > 0)
            {
                GridView2.DataSource = dtt;
                GridView2.DataBind();

            }
            else
            {

                GridView2.DataSource = null;
                GridView2.DataBind();
            }

        }
        catch
        {



        }

    }
    protected void btn_export_Click(object sender, EventArgs e)
    {

        billcompleteotnot();
        if (dtexistsornot.Rows.Count < 1)
        {

            try
            {
                getdatefuntion();
                string sttupdate = "";
                if (Rto_fatsnf.SelectedItem.Value == "3")
                {

                    if (rto_plucorminus.SelectedValue == "1")
                    {

                        fatsnf = "fat=fat" + '+' + txt_fat.Text + "," + "snf=snf" + '+' + txt_snf.Text;
                    }
                    if (rto_plucorminus.SelectedValue == "0")
                    {

                        fatsnf = "fat=fat" + '-' + txt_fat.Text + "," + "snf=snf" + '-' + txt_snf.Text;
                    }

                    if (Rto_btn.SelectedItem.Value == "1")
                    {
                        sttupdate = "update procurementimport set  " + fatsnf + "  where   plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND  prdate between '" + FDATE + "' and '" + TODATE + "'";
                    }
                    if (Rto_btn.SelectedItem.Value == "0")
                    {
                        sttupdate = "update procurementimport set  " + fatsnf + "  where   plant_code='" + ddl_Plantname.SelectedItem.Value + "'  AND ROUTE_ID='" + ddl_Route.SelectedItem.Value + "' AND  prdate between '" + FDATE + "' and '" + TODATE + "'";
                    }
                }
                if (Rto_fatsnf.SelectedItem.Value == "2")
                {
                    if (rto_plucorminus.SelectedValue == "1")
                    {
                        snfadd = "snf=snf" + '+' + txt_snf.Text;
                    }
                    if (rto_plucorminus.SelectedValue == "0")
                    {

                        snfadd = "snf=snf" + '-' + txt_snf.Text;
                    }

                    if (Rto_btn.SelectedItem.Value == "1")
                    {
                        sttupdate = "update procurementimport set   " + snfadd + "   where   plant_code='" + ddl_Plantname.SelectedItem.Value + "'  AND  prdate between '" + FDATE + "' and '" + TODATE + "'";
                    }
                    if (Rto_btn.SelectedItem.Value == "1")
                    {
                        sttupdate = "update procurementimport set   " + snfadd + "   where   plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND ROUTE_ID='" + ddl_Route.SelectedItem.Value + "'  AND  prdate between '" + FDATE + "' and '" + TODATE + "'";
                    }
                }
                if (Rto_fatsnf.SelectedItem.Value == "1")
                {


                    if (rto_plucorminus.SelectedValue == "1")
                    {

                        fatadd = "fat=fat" + '+' + txt_fat.Text;
                    }
                    if (rto_plucorminus.SelectedValue == "0")
                    {

                        fatadd = "fat=fat" + '-' + txt_fat.Text;
                    }

                    if (Rto_btn.SelectedItem.Value == "1")
                    {
                        sttupdate = "update procurementimport set  " + fatadd + "       where   plant_code='" + ddl_Plantname.SelectedItem.Value + "'  AND  prdate between '" + FDATE + "' and '" + TODATE + "'";
                    }
                    if (Rto_btn.SelectedItem.Value == "0")
                    {
                        sttupdate = "update procurementimport set  " + fatadd + "       where   plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND ROUTE_ID='" + ddl_Route.SelectedItem.Value + "'  AND  prdate between '" + FDATE + "' and '" + TODATE + "'";

                    }
                }
                con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(sttupdate, con);
                cmd.ExecuteNonQuery();
                txt_fat.Text = "";
                txt_snf.Text = "";
                getdata();
                string mss = "Records Updated SuccessFully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

            }
            catch
            {


            }
        }
        else
        {
            string mss = "Sorry BillProcess Already Completed";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
            txt_fat.Text = "";
            txt_snf.Text = "";

        }
    }
    protected void Rto_btn_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (Rto_btn.SelectedItem.Value == "0")
            {
                ddl_Route.Visible = true;
            }
            else
            {
                ddl_Route.Visible = false;

            }
        }
        catch
        {


        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            billdate();
            getroute();
        }
        catch
        {


        }

    }
    public void getroute()
    {
        try
        {
            con = new SqlConnection(connection);
            getdatefuntion();
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            string rroute = "Select Route_id,Route_Name  from (select   route_id   from  procurementimport   where plant_code='" + pcode + "'  and prdate between '" + FDATE + "' and '" + TODATE + "'  group by route_id) as news left join (Select Route_ID as Rmrouteid,Route_Name  from Route_Master  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' group by  Route_ID,Route_Name    ) as rm on news.Route_id=rm.Rmrouteid   order by Route_id   asc";
            SqlCommand cmd = new SqlCommand(rroute, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddl_Route.DataSource = dt;
                ddl_Route.DataTextField = "Route_Name";
                ddl_Route.DataValueField = "Route_id";
                ddl_Route.DataBind();
            }
            else
            {

            }

        }
        catch
        {


        }


    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getroute();
        }
        catch
        {

        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Rto_fatsnf_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rto_fatsnf.SelectedItem.Value == "3")
            {
                txt_fat.Visible = true;
                txt_snf.Visible = true;
                Label1.Visible = true;
                Label2.Visible = true;
            }
            if (Rto_fatsnf.SelectedItem.Value == "2")
            {
                txt_fat.Visible = false;
                txt_snf.Visible = true;
                Label1.Visible = false;
                Label2.Visible = true;
            }
            if (Rto_fatsnf.SelectedItem.Value == "1")
            {
                txt_fat.Visible = true;
                txt_snf.Visible = false;
                Label1.Visible = true;
                Label2.Visible = false;
            }

        }
        catch
        {


        }

    }
    public void billcompleteotnot()
    {
          getdatefuntion();
          FDATE = getvalm + "/" + getvald + "/" + getvaly;
          TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
          string strr = "Select   *   from paymentData    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and Frm_date='" + FDATE + "'  and To_date='" + TODATE + "'";
          con = new SqlConnection(connection);
          SqlCommand cmd = new SqlCommand(strr, con);
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          dtexistsornot.Rows.Clear();
          da.Fill(dtexistsornot);
    }
}