using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class DpuDataCheckStatus : System.Web.UI.Page
{

    DbHelper db = new DbHelper();
    public string ccode;
    //  public string pcode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string rid;
    public string frmdate;
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string fdate;
    string tdate;
    int days;
    int totalrow;
    int totalrow1;


    string fdat;

    string fdat1;

    string fres;

    string tdat;

    string tres;

    // 2-2-16 code change
    string d4;
    string d5;

    string addfsecond = "01:01:00";
    string addtsecond = "23:59:59";
    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                dtm1 = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("MM/dd/yyyy");
                txt_ToDate.Text = dtm1.ToString("MM/dd/yyyy");

                LoadPlantcode1();

                Label1.Visible = false;
                Label24.Visible = false;
                Label4.Visible = false;

            }


            else
            {

                Server.Transfer("LoginDefault.aspx");
            }


        }

        if (IsPostBack == true)
        {




            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                //txt_FromDate.Attributes.Add("txt_FromDate", txt_FromDate.Text.Trim());
                // rid = ddl_RouteID.SelectedItem.Value;
                //  LoadPlantcode1();

                if (roleid > 2)
                {
                    // pname = Session["pname"].ToString();
                    //   pcode = Convert.ToInt32(Session["Plant_Code"]);
                    //strr = textbox1.text;
                    //result = strr.substring(0, 3);
                    string ppcode = ddl_Plantname.Text;
                    pcode = ppcode.Substring(0, 3);
                    //  LoadPlantcode1();

                    pname = ddl_Plantname.Text;
                    //   pcode = ddl_Plantcode.Text;
                    // LoadPlantcode1();


                    //    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    //    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");


                }
                else
                {

                    pname = ddl_Plantname.Text;



                }



            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }


    }

    private void LoadPlantcode1()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //  ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    public void getratezero()
    {

        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();


            datecall();




          //  string sqlstr = "SELECT      agent_id as Aid, convert(varchar,prdate,103) as Date,sessions as Sess,fat as Fat,snf as Snf,rate  as Rate  FROM ProducerProcurement WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') or (PRDATE BETWEEN '" + d4 + "' AND '" + d5 + "'  )) AND  RATE=0   order by  Date,sess,aid";
            string sqlstr = "SELECT      agent_id as Aid, convert(varchar,prdate,103) as Date,sessions as Sess,fat as Fat,snf as Snf,rate  as Rate  FROM ProducerProcurement WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') ) AND  RATE=0   order by  Date,sess,aid";

            SqlCommand COM = new SqlCommand(sqlstr, conn);
            //   SqlDataReader DR = COM.ExecuteReader();
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                int totalrow = Convert.ToInt32(GridView1.Rows.Count);
                Label1.Text = " NoofRows: " + totalrow.ToString();
                Label1.ForeColor = System.Drawing.Color.Green;
                Label4.Visible = true;


            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
                int totalrow = Convert.ToInt32(GridView1.Rows.Count);
                Label1.Text = " NoofRows: " + totalrow.ToString();
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Visible = true;

            }
            //GridView1.DataSource = DR;
            //GridView1.DataBind();

        }

    }

    public void getgpsdata()
    {

        try
        {

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                datecall();



                // conn.Open();
                //SqlDataReader dr = null;
                //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                DataTable dt = new DataTable();
                //   string sqlstr = "SELECT        distinct       convert(varchar,prdate,103) as Date,shift    FROM THIRUMALABILLSNEW WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + fres + "' AND '" + tres + "' order by Date,shift ";

               // string sqlstr = "SELECT        distinct       convert(varchar,prdate,103) as Date,shift    FROM THIRUMALABILLSNEW WHERE  plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') or (PRDATE BETWEEN '" + d4 + "' AND '" + d5 + "'  )) order by Date,shift ";
                string sqlstr = "SELECT        distinct       convert(varchar,prdate,103) as Date,shift    FROM VMCCDPU  WHERE  plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') ) order by Date,shift ";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                    int totalrow = Convert.ToInt32(GridView3.Rows.Count);
                    Label24.Text = " NoofRows: " + totalrow.ToString();
                    Label24.ForeColor = System.Drawing.Color.Green;


                }
                //GridView3.DataSource = DR;
                //GridView3.DataBind();



                else
                {

                    GridView3.DataSource = null;
                    GridView3.DataBind();
                    int totalrow = Convert.ToInt32(GridView3.Rows.Count);
                    Label24.Text = " NoofRows: " + totalrow.ToString();
                    Label24.ForeColor = System.Drawing.Color.Green;

                }


            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    public void getreceiveddata()
    {

        try
        {

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                datecall();


                DataTable dt = new DataTable();
              //  string sqlstr = "SELECT        distinct       convert(varchar,prdate,103) as Date,Sessions    FROM ProducerProcurement WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') or (PRDATE BETWEEN '" + d4 + "' AND '" + d5 + "'  )) order by Date,Sessions ";
                string sqlstr = "SELECT        distinct       convert(varchar,prdate,103) as Date,Sessions    FROM ProducerProcurement WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') ) order by Date,Sessions ";

                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView4.DataSource = dt;
                    GridView4.DataBind();
                    int totalrow = Convert.ToInt32(GridView4.Rows.Count);
                    Label4.Text = " NoofRows: " + totalrow.ToString();
                    Label4.ForeColor = System.Drawing.Color.Green;


                }
                else
                {
                    GridView4.DataSource = null;
                    GridView4.DataBind();
                    int totalrow = Convert.ToInt32(GridView4.Rows.Count);
                    Label4.Text = " NoofRows: " + totalrow.ToString();
                    Label4.ForeColor = System.Drawing.Color.Green;


                }

            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }






    }


    public void gpscount()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {

            datecall();
            conn.Open();
            //  string sqlstr = "SELECT     count(*)  FROM THIRUMALABILLSNEW WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
            //string sqlstr = "SELECT     *  FROM THIRUMALABILLSNEW WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') or (PRDATE BETWEEN '" + d4 + "' AND '" + d5 + "'  ))";
            string sqlstr = "SELECT     *  FROM VMCCDPU WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') )";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
            //   SqlDataReader DR = COM.ExecuteReader();
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                totalrow = dt.Rows.Count;
                TextBox5.Text = totalrow.ToString();
                TextBox5.ForeColor = System.Drawing.Color.Green;
            }
            else
            {


                totalrow = dt.Rows.Count;
                TextBox5.Text = totalrow.ToString();
                TextBox5.ForeColor = System.Drawing.Color.Red;
            }
        }
        //GridView1.DataSource = DR;
        //GridView1.DataBind();

    }



    public void receiveddatacount()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            datecall();

            //string sqlstr = "SELECT     *   FROM ProducerProcurement WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') or (PRDATE BETWEEN '" + d4 + "' AND '" + d5 + "'  ))";
            string sqlstr = "SELECT     *   FROM ProducerProcurement WHERE   plant_code='" + pcode + "'       AND ((PRDATE BETWEEN '" + fdat + "' AND '" + tdat + "') or (PRDATE BETWEEN '" + fres + "' AND '" + tres + "') )";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
            //   SqlDataReader DR = COM.ExecuteReader();
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                totalrow1 = dt.Rows.Count;
                TextBox6.Text = totalrow1.ToString();
                TextBox6.ForeColor = System.Drawing.Color.Green;
            }
            else
            {


                totalrow = dt.Rows.Count;
                TextBox6.Text = totalrow1.ToString();
                TextBox6.ForeColor = System.Drawing.Color.Red;
            }
        }
        //GridView1.DataSource = DR;
        //GridView1.DataBind();

    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        Label1.Visible = true;
        Label24.Visible = true;
        Label4.Visible = true;

        getratezero();
        getgpsdata();
        getreceiveddata();
        gpscount();
        receiveddatacount();


        if (TextBox5.Text == TextBox6.Text)
        {

            Label4.ForeColor = System.Drawing.Color.Green;
            TextBox6.ForeColor = System.Drawing.Color.Green;

        }
        else
        {

            Label4.ForeColor = System.Drawing.Color.Red;
            TextBox6.ForeColor = System.Drawing.Color.Red;
        }



    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            GridView1.PageIndex = e.NewPageIndex;
            getratezero();
        }


    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            GridView3.PageIndex = e.NewPageIndex;
            getgpsdata();
        }
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            GridView3.PageIndex = e.NewPageIndex;
            getreceiveddata();
        }
    }


    public void datecall()
    {

        fdat = txt_FromDate.Text.ToString();

        fdat1 = "12:00:00";

        fres = fdat + " " + fdat1;

        tdat = txt_ToDate.Text.ToString();

        tres = tdat + " " + fdat1;


        d4 = fdat + " " + addfsecond;
        d5 = tdat + " " + addtsecond; 


    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox7_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_ToDate_TextChanged(object sender, EventArgs e)
    {

    }
}