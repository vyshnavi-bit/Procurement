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
using System.IO;
public partial class Default2 : System.Web.UI.Page
{
    SqlDataReader dr;
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    BOLbillgenerate BOLBill = new BOLbillgenerate();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    DbHelper db = new DbHelper();
    public string ccode;
    //  public string pcode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string rid;
    public string frmdate;
    //public string todate;
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    //Admin Check Flag
    public int Falg = 0;
    string fdate;
    string tdate;
    int days;

    int Data;
    int status;
    string ppcode;
    public static int roleid;
    string QUERYVAL;
    protected void Page_Load(object sender, EventArgs e)
    {
           if (IsPostBack != true)
        {
       
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                QUERYVAL = Request.QueryString["CODE"];

                //string abc = "ARANI BILL COMPLETED  TOTAL AMOUNT:5755000";
                //Response.Write("<marquee>" + abc + "</marquee>");

                if (QUERYVAL != string.Empty)
                {
                    pcode = Request.QueryString["CODE"];
                }

                else
                {

                    pcode = Session["Plant_Code"].ToString();

                }

                ccode = Session["Company_code"].ToString();
               
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();


                Button8.Visible = false;
               
              //  managmobNo = Session["managmobNo"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());

               dtm = System.DateTime.Now;
               dtm1 = System.DateTime.Now;
               txt_FromDate.Text = dtm.ToString("MM/dd/yyyy");
               txt_ToDate.Text = dtm1.ToString("MM/dd/yyyy");

                TimeSpan x = dtm1.Subtract(dtm);
                days = x.Days;

                // Bdate();

                //loadrouteid();
                //rid = ddl_RouteID.SelectedItem.Value;

               //GridView1.Visible = false;
                if (roleid == 9)
                {
                    pcode = "170";
                    loadspecialsingleplant();
                }
                else
                {

                    LoadPlantcode1();
                }
            //    GRIDVIEWCODE();
            // GridView1.DataBind();


                GridView5.Visible = false;
              
             
                Label1.Visible = false;
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;

                getadminapprovalstatus();
                // Select_Procurementreport1();
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
              //  pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              
               // managmobNo = Session["managmobNo"].ToString();

                //txt_FromDate.Attributes.Add("txt_FromDate", txt_FromDate.Text.Trim());
                // rid = ddl_RouteID.SelectedItem.Value;
              //  LoadPlantcode1();

                if (roleid < 3)
                {
                    // pname = Session["pname"].ToString();
                    //   pcode = Convert.ToInt32(Session["Plant_Code"]);
                    //strr = textbox1.text;
                    //result = strr.substring(0, 3);
                     ppcode = ddl_Plantname.Text;
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
                    pcode = pname.Substring(0, 3);
                  

                }
                TimeSpan x = dtm1.Subtract(dtm);
                days = x.Days;
                GridView1.DataBind();
                GridView5.Visible = false;

                getadminapprovalstatus();
                GRIDVIEWCODE();
                totsessions();
                overallsesscount();
                sendsms();
                getmilkkg();
              
              
              
                //  totmilkkg();
              
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }

        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        REACHDATA();
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

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void REACHDATA()
    {
        try
        {

             string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
             using (SqlConnection conn = new SqlConnection(connStr))
             {
                 conn.Open();
                 //SqlDataReader dr = null;
                 //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                 DataSet ds = new DataSet();
                 DataSet ds1 = new DataSet();
                 string sqlstr = "SELECT COUNT(PRDATE) AS PRDATE   FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                 SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                 string sqlstr1 = "SELECT COUNT(PRDATE) AS PRDATE   FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                 SqlDataAdapter adp1 = new SqlDataAdapter(sqlstr1, conn);
                 conn.Close();
             }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                TextBox5.Text = "";
                TextBox6.Text = "";
                string sqlstr = "SELECT COUNT(tid) AS tid   FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                TextBox5.Text = ds.Tables[0].Rows[0]["tid"].ToString();
                TextBox5.ForeColor = System.Drawing.Color.Green;

                string sqlstr1 = "SELECT COUNT(tid) AS tid   FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                SqlDataAdapter adp1 = new SqlDataAdapter(sqlstr1, conn);
                adp1.Fill(ds1);
                TextBox6.Text = ds1.Tables[0].Rows[0]["tid"].ToString();

                if (TextBox5.Text == TextBox6.Text)
                {

                    TextBox6.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    TextBox6.ForeColor = System.Drawing.Color.Red;
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
  
    protected void Button7_Click(object sender, EventArgs e)
    {

        pname = ddl_Plantname.Text;
        if (IsPostBack == true)
        {
            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            DateTime df = Convert.ToDateTime(txt_FromDate.Text);
            DateTime dt = Convert.ToDateTime(txt_ToDate.Text);
            TimeSpan x = dt.Subtract(df);
            days = x.Days * 2 + 2;
            GRIDVIEWCODE();
            totsessions();
            overallsesscount();
            totalagents();
            getmilkkg();
            sendsms();
            try
            {

                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //SqlDataReader dr = null;
                    //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                    DataSet ds = new DataSet();
                    DataSet ds1 = new DataSet();
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    string sqlstr = "SELECT COUNT(tid) AS tid   FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                    adp.Fill(ds);
                    TextBox5.Text = ds.Tables[0].Rows[0]["tid"].ToString();
                    TextBox5.ForeColor = System.Drawing.Color.Green;
                    string sqlstr1 = "SELECT COUNT(tid) AS tid   FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                    SqlDataAdapter adp1 = new SqlDataAdapter(sqlstr1, conn);
                    adp1.Fill(ds1);
                    TextBox6.Text = ds1.Tables[0].Rows[0]["tid"].ToString();
                    if (TextBox5.Text == TextBox6.Text)
                    {
                         TextBox6.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        TextBox6.ForeColor = System.Drawing.Color.Red;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.ToString());
            }
            try
            {

                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //SqlDataReader dr = null;
                    //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                    DataSet ds = new DataSet();
                    DataSet ds1 = new DataSet();
                    string sqlstr1 = "SELECT  AGENT_ID   FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND RATE=0     ORDER BY   AGENT_ID";
                    SqlDataAdapter adp1 = new SqlDataAdapter(sqlstr1, conn);
                    adp1.Fill(ds1);
                    conn.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.ToString());
            }
            //     rate zero  -----------------------------------------------------------------------------
            try
            {

                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //SqlDataReader dr = null;
                    //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                    DataSet ds = new DataSet();
                    DataSet ds1 = new DataSet();
                    string sqlstr = "SELECT COUNT(PRDATE) AS PRDATE   FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' and  ROUTE_ID IS NULL";
                    SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                    string sqlstr1 = "SELECT COUNT(PRDATE) AS PRDATE   FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' and  ROUTE_ID IS NULL ";
                    SqlDataAdapter adp1 = new SqlDataAdapter(sqlstr1, conn);
                    adp.Fill(ds);
                    conn.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.ToString());
            }
            //   route id null -------------------------------------------------------------------------------------------//
            try
            {

                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //SqlDataReader dr = null;
                    //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                    DataSet ds = new DataSet();
                    DataSet ds1 = new DataSet();
                    string sqlstr = "SELECT COUNT(PRDATE) AS PRDATE   FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND REMARKSTATUS=1";
                    SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                    adp.Fill(ds);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.ToString());
            }


            //    remaRK   -------------------------------------------------------------------------


            try
            {

                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //SqlDataReader dr = null;
                    //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                    DataSet ds = new DataSet();
                    DataSet ds1 = new DataSet();

                    string sqlstr = "SELECT        distinct      PRDATE,sessions    FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' ";
                    SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);


                    string sqlstr1 = "SELECT DISTINCT PRDATE,SESSIONS   FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                    SqlDataAdapter adp1 = new SqlDataAdapter(sqlstr1, conn);




                    adp.Fill(ds);
                    //ListBox7.DataSource = ds;

                    //ListBox7.DataTextField = "PRDATE";
                    //ListBox7.DataValueField = "PRDATE";
                    //// ListBox7.DataValueField = "sessions";
                    //ListBox7.DataBind();



                    //adp.Fill(ds);
                    //ListBox8.DataSource = ds;

                    //ListBox8.DataTextField = "PRDATE";
                    //ListBox8.DataValueField = "PRDATE";
                    //// ListBox7.DataValueField = "sessions";
                    //ListBox8.DataBind();



                    //adp1.Fill(ds1);
                    //ListBox5.DataSource = ds1;

                    //ListBox5.DataTextField = "PRDATE";
                    //ListBox5.DataValueField = "PRDATE";

                    //ListBox5.DataBind();
                    conn.Close();






                    conn.Close();
                }
                //if (dr.HasRows)
                //{
                //    while (dr.Read())
                //    {
                //        //  ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                //   //     ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                //        string TEE= dr["PRDATE"].ToString(); 

                //        string TEE1 = dr["SESSIONS"].ToString();

                //        TextBox2.Text= TEE + " " + TEE1;

                //    }
                //}
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.ToString());
            }
           

        }
        else
        {

            GRIDVIEWCODE();
            totsessions();
            overallsesscount();
            //  totmilkkg();
            getmilkkg();


        }
       
        if (TextBox5.Text == TextBox6.Text)
        {
            Label4.ForeColor = System.Drawing.Color.Green;

        }
        else
        {
            Label4.ForeColor = System.Drawing.Color.Red;


        }
      
      

    }

    protected void btn_viewdata_click(object sender, EventArgs e)
    {
        pname = ddl_Plantname.Text;
        DataSet ds = new DataSet();
        Label1.Visible = true;
        Label2.Visible = true;
        Label3.Visible = true;
        Label4.Visible = true;
        DateTime df = Convert.ToDateTime(txt_FromDate.Text);
        DateTime dt = Convert.ToDateTime(txt_ToDate.Text);
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string sqlstr = "SELECT Agent_id, Milk_ltr, Milk_kg, Fat, Snf, Clr, Fat_kg AS KgFat, Snf_kg AS KgSnf, PRDATE AS Date    FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
            DataTable mydt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(COM);
            adp.Fill(mydt);
            if (mydt.Rows.Count > 0)
            {
                grdccdata.DataSource = mydt;
                grdccdata.DataBind();
                ModalPopupExtender1.Show();
            }
        }
    }

    protected void btnhide_click(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {
            ModalPopupExtender1.Hide();
        }
    }

    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

        //tempfdate.Text = txt_FromDate.Text;
        //string fdate;

    }
    protected void txt_ToDate_TextChanged(object sender, EventArgs e)
    {
        //temptdate.Text = txt_ToDate.Text;
        //string tdate;
    }

    public void GRIDVIEWCODE()
    {

         string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
         using (SqlConnection conn = new SqlConnection(connStr))
         {
             conn.Open();

             string sqlstr = "SELECT      agent_id as Aid, convert(varchar,prdate,103) as Date,sessions as Sess,fat as Fat,snf as Snf, Milk_ltr, Milk_kg, rate  as Rate  FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' AND  RATE=0   order by  Date,sess,aid";
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
                 Label1.ForeColor = System.Drawing.Color.Red;
             }
             else
             {

                 GridView1.DataSource = null;
                 GridView1.DataBind();
                 int totalrow = Convert.ToInt32(GridView1.Rows.Count);
                 Label1.Text = " NoofRows: " + totalrow.ToString();
                 Label1.ForeColor = System.Drawing.Color.Green;
             }
             //GridView1.DataSource = DR;
             //GridView1.DataBind();
            
         }

    }
    public void totsessions()
    {

        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sqlstr = "SELECT    distinct  convert(varchar,prdate,103) as Date,sessions  as Sess  FROM PROCUREMENTimport WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'    order by date,sess";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
         //   SqlDataReader DR = COM.ExecuteReader();

            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
                int totalrow = Convert.ToInt32(GridView3.Rows.Count);
                Label3.Text = " NoofRows: " + totalrow.ToString();
                Label3.ForeColor = System.Drawing.Color.Green;
                

            }
            //GridView3.DataSource = DR;
            //GridView3.DataBind();
           


            else
            {

                GridView3.DataSource = null;
                GridView3.DataBind();
                int totalrow = Convert.ToInt32(GridView3.Rows.Count);
                Label3.Text = " NoofRows: " + totalrow.ToString();
                Label3.ForeColor = System.Drawing.Color.Green;
               
            }

        }


       
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            DataTable Report = new DataTable();
            Report.Columns.Add("Date");
            Report.Columns.Add("Sess");
            Report.Columns.Add("SMS");
            string sqlstr = "SELECT    distinct  convert(varchar,prdate,103) as Date,sessions  as Sess  FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'    order by date,sess";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
         //   SqlDataReader DR = COM.ExecuteReader();

            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);

            string sqlstr2 = "SELECT    distinct  convert(varchar,Send_date,103) as Date  FROM Message_Histroy WHERE   Plant_code='" + pcode + "'       AND Send_date BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'    order by date";
            SqlCommand COM2 = new SqlCommand(sqlstr2, conn);
            //   SqlDataReader DR = COM.ExecuteReader();

            DataTable dt2 = new DataTable();
            SqlDataAdapter sqlDa2 = new SqlDataAdapter(COM2);
            sqlDa2.Fill(dt2);


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow newdr = Report.NewRow();
                    string date = dr["Date"].ToString();
                    string session = dr["Sess"].ToString();
                    newdr["Date"] = date;
                    newdr["Sess"] = session;
                    foreach (DataRow drr in dt2.Select("Date='" + date + "'"))
                    {
                        string milkltr3 = drr["Date"].ToString();
                        newdr["SMS"] = "OK";
                    }
                    Report.Rows.Add(newdr);
                    //string sms = drp["pr_SNF"].ToString();
                }
            }
            if (Report.Rows.Count > 0)
            {
                GridView4.DataSource = Report;
                GridView4.DataBind();
                int totalrow = Convert.ToInt32(GridView4.Rows.Count);
                Label4.Text = " NoofRows: " + totalrow.ToString();
                Label4.ForeColor = System.Drawing.Color.Red;
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

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sqlstr = "SELECT    agent_id as Aid,convert(varchar,prdate,103) as Date,sessions as Sess,fat as Fat,snf as Snf    FROM PROCUREMENTimport WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'  and remarkstatus=1  order by Date,Sess";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
      //      SqlDataReader DR = COM.ExecuteReader();

            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                int totalrow = Convert.ToInt32(GridView2.Rows.Count);
                Label2.Text = " NoofRows: " + totalrow.ToString();
                Label2.ForeColor = System.Drawing.Color.Red;

            }
            else
            {

                GridView2.DataSource = null;
                GridView2.DataBind();
                int totalrow = Convert.ToInt32(GridView2.Rows.Count);
                Label2.Text = " NoofRows: " + totalrow.ToString();
                Label2.ForeColor = System.Drawing.Color.Green;
            }
            //GridView2.DataSource = DR;
            //GridView2.DataBind();
            //int totalrow = Convert.ToInt32(GridView2.Rows.Count);
            //Label2.Text = " NoofRows: " + totalrow.ToString();
            //Label2.ForeColor = System.Drawing.Color.Red;

        }


    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        if (IsPostBack == true)
        {
            GridView1.PageIndex = e.NewPageIndex;

            GRIDVIEWCODE();
            totsessions();
        }
    }
    public void overallsesscount()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sqlstr = "SELECT    distinct prdate as Date,sessions  as Sess  FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'    order by date,sess";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
          //  SqlDataReader DR = COM.ExecuteReader();

            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView5.DataSource = dt;
                GridView5.DataBind();
                TextBox1.Text = days.ToString();
                TextBox1.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                GridView5.DataSource = null;
                GridView5.DataBind();
                TextBox1.Text = days.ToString();
                TextBox1.ForeColor = System.Drawing.Color.Red;

            }


            //GridView5.DataSource = DR;
            //GridView5.DataBind();
           // int totalrow = Convert.ToInt32(GridView4.Rows.Count);
           //TextBox1.Text =  totalrow.ToString();
           //TextBox1.ForeColor = System.Drawing.Color.Red;
        
           
            GridView5.Visible = false;

        }

    }

    public void totmilkkg()
    {

        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sqlstr = "SELECT    sum(Milk_Kg) as Kg  FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' ";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
            //  SqlDataReader DR = COM.ExecuteReader();

            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView5.DataSource = dt;
                GridView5.DataBind();
            }
            else
            {


                GridView5.DataSource = null;
                GridView5.DataBind();
                TextBox1.Text = days.ToString();
                TextBox1.ForeColor = System.Drawing.Color.Red;

            }
            //GridView5.DataSource = DR;
            //GridView5.DataBind();
            // int totalrow = Convert.ToInt32(GridView4.Rows.Count);
            //TextBox1.Text =  totalrow.ToString();
            //TextBox1.ForeColor = System.Drawing.Color.Red;

        //    TextBox2.Text = days.ToString();
        //    TextBox2.ForeColor = System.Drawing.Color.Red;
            GridView5.Visible = false;

        }









    }

    public void totalagents()
    {



        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            //string sqlstr2 = "select COUNT(*) as sendsms from Message_Histroy where Company_code='1' and Plant_code='" + pcode + "' and Send_date between '"+txt_FromDate.Text+"' and '"+txt_ToDate.Text+"'";
            //SqlCommand COM2 = new SqlCommand(sqlstr2, conn);
            //  SqlDataReader DR = COM.ExecuteReader();


            DataSet ds = new DataSet();
            TextBox7.Text = "";
            string sqlstr = "SELECT    count(sampleno) as totagent  FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "' ";
            SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

            adp.Fill(ds);
            TextBox7.Text = ds.Tables[0].Rows[0]["totagent"].ToString();

                       

        }























       








    }

    public void sendsms()
    {

        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            //string sqlstr2 = "select COUNT(*) as sendsms from Message_Histroy where Company_code='1' and Plant_code='" + pcode + "' and Send_date between '"+txt_FromDate.Text+"' and '"+txt_ToDate.Text+"'";
            //SqlCommand COM2 = new SqlCommand(sqlstr2, conn);
            //  SqlDataReader DR = COM.ExecuteReader();


            DataSet ds = new DataSet();
            TextBox8.Text = "";
            string sqlstr2 = "select COUNT(*) as sendsms from Message_Histroy where Company_code='1' and Plant_code='" + pcode + "' and Send_date between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sqlstr2, conn);

            adp.Fill(ds);
            TextBox8.Text = ds.Tables[0].Rows[0]["sendsms"].ToString();

        }
    }

    public void getmilkkg()
    {

        try
        {
            //TextBox3.Text = "";
            //TextBox4.Text = "";
            //TextBox5.Text = "";
            SqlDataReader dr = null;
            dr = Bllusers.getmilkkg(txt_FromDate.Text,txt_ToDate.Text,pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    TextBox2.Text = dr["kg"].ToString();
                    if (TextBox2.Text == "")
                    {
                        TextBox2.Text = "0";
                    }
                    else
                    {
                        double kg1 = Convert.ToDouble(TextBox2.Text);
                        double finalValue = Math.Round(kg1, 2);
                        TextBox2.Text = finalValue.ToString();
                    }
                    TextBox3.Text = dr["fat"].ToString();
                    if (TextBox3.Text == "")
                    {
                        TextBox3.Text = "0";
                    }
                    else
                    {

                        double fat = Convert.ToDouble(TextBox3.Text);
                        double finalValue1 = Math.Round(fat, 2);
                        TextBox3.Text = finalValue1.ToString();
                    }
                    TextBox4.Text = dr["snf"].ToString();
                    if (TextBox4.Text == "")
                    {
                        TextBox4.Text = "0";
                    }
                    else
                    {
                        double snf = Convert.ToDouble(TextBox4.Text);
                        double finalValue2 = Math.Round(snf, 2);
                        TextBox4.Text = finalValue2.ToString();
                    }
                                       //   ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }


            //TextBox2.Text = "0";
            //TextBox3.Text = "0";
            //TextBox4.Text = "0";
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    protected void grdccdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            grdccdata.PageIndex = e.NewPageIndex;
            pname = ddl_Plantname.Text;
            DataSet ds = new DataSet();
            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            DateTime df = Convert.ToDateTime(txt_FromDate.Text);
            DateTime dt = Convert.ToDateTime(txt_ToDate.Text);
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlstr = "SELECT Agent_id, Milk_ltr, Milk_kg, Fat, Snf, Clr, Fat_kg AS KgFat, Snf_kg AS KgSnf, PRDATE AS Date   FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                DataTable mydt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(COM);
                adp.Fill(mydt);
                if (mydt.Rows.Count > 0)
                {
                    grdccdata.DataSource = mydt;
                    grdccdata.DataBind();
                    ModalPopupExtender1.Show();
                }
            }
        }
        else
        {

            grdccdata.PageIndex = e.NewPageIndex;
            pname = ddl_Plantname.Text;
            DataSet ds = new DataSet();
            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            DateTime df = Convert.ToDateTime(txt_FromDate.Text);
            DateTime dt = Convert.ToDateTime(txt_ToDate.Text);
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlstr = "SELECT Agent_id, Milk_ltr, Milk_kg, Fat, Snf, Clr, Fat_kg AS KgFat, Snf_kg AS KgSnf, PRDATE AS Date    FROM PROCUREMENTIMPORT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + txt_FromDate.Text + "' AND '" + txt_ToDate.Text + "'";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                DataTable mydt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(COM);
                adp.Fill(mydt);
                if (mydt.Rows.Count > 0)
                {
                    grdccdata.DataSource = mydt;
                    grdccdata.DataBind();
                    ModalPopupExtender1.Show();
                }
            }

        }
    }



    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            GridView2.PageIndex = e.NewPageIndex;
            GRIDVIEWCODE();
            totsessions();
        }
        else
        {

            GridView2.PageIndex = e.NewPageIndex;
            GRIDVIEWCODE();
            totsessions();

        }
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            GridView3.PageIndex = e.NewPageIndex;
            GRIDVIEWCODE();
            totsessions();
        }
        else
        {

            GridView3.PageIndex = e.NewPageIndex;
            GRIDVIEWCODE();
            totsessions();

        }
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            GridView4.PageIndex = e.NewPageIndex;
            GRIDVIEWCODE();
            totsessions();
        }
        else
        {

            GridView4.PageIndex = e.NewPageIndex;
            GRIDVIEWCODE();
            totsessions();

        }
    }
    protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox7_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_lock(object sender, EventArgs e)
    {

        //string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        //SqlConnection conn = new SqlConnection(connStr);
        //conn.Open();
        //SqlCommand cmd = new SqlCommand("update AdminApproval set Data='1' where Plant_code='" + pcode + "'", conn);
        //cmd.ExecuteNonQuery();
    }

    public void getadminapprovalstatus()
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string stt = "Select Data,status    from  AdminApproval   where Plant_code='" + pcode + "'  ";
            SqlCommand cmd = new SqlCommand(stt, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                // btn_lock.Visible = false;

                while (dr.Read())
                {

                    Data = Convert.ToInt32(dr["Data"]);
                    status = Convert.ToInt32(dr["status"]);
                }
                //if (Data == 1 && status == 1)
                //{
                //    Button8.Visible = true;
                //}

                //if (Data == 0 && status == 1)
                //{
                //    Button8.Visible = true;
                //}
                //if (Data == 1 && status == 2)
                //{
                //    Button8.Visible = false;
                //}

                //if (Data == 0 && status == 2)
                //{
                //    Button8.Visible = false;
                //}

                if ((status == 1)  && (roleid > 1))
                {
                    Button8.Visible = true;


                    if((Data==1) && (status==1))
                    {


                     //   Button8.Enabled = false;
                        Button8.Enabled = false;

                    }
                    

                }
                else
                {
                    Button8.Visible = false;

                }

            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update AdminApproval set Data='1' where Plant_code='" + pcode + "'", conn);
            cmd.ExecuteNonQuery();
            string message;
            message = "Data Approved SuccessFully";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

        }

        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
}