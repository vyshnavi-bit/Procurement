using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class ChartComparison : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    DataTable dt = new DataTable();
    DataTable dtt = new DataTable();
    DataTable dttt = new DataTable();
    int i = 2;
    int j = 0;
    int K = 0;
    int L = 0;
    int M = 0;
    string INCCOUNT;
    double FAT;
    string GETDATE;
    string[] plant;
    // string[] getdat;
    string getcommondate;

    string FDATE;
    string TODATE;
    string FDATE1;
    string TODATE1;
    string frmdate;
    string Todate;
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate1;
    string Todate1;
    DateTime datee1;
    DateTime datee2;
    string GETTID;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
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
                    dtm = System.DateTime.Now;
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    //txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                    LoadPlantcode();
                    //DateTime txtMyDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                    //DateTime dttt = DateTime.Parse(txt_FromDate.Text);

                    //DateTime cutime = DateTime.Parse(gettime);
                    //string sff = dttt.AddDays(1).ToString();

                    //GETDATE = txt_FromDate.Text;

                    //DateTime date = txtMyDate.AddDays(-1);

                    //string datee = date.ToString("MM/dd/yyyy");
                    // DateTime DDD = DateTime.ParseExact(date);
                    // Button2.Visible = false;
                }
                else
                {



                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();
                pcode = ddl_Plantcode.SelectedItem.Value;

            }

        }
        catch
        {


        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());



                }
            }
            else
            {
                ddl_Plantcode.Items.Add("--Select PlantName--");
                ddl_Plantname.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception EE)
        {
            string message;
            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;

        billdate();

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {



            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "BillDate:" + FDATE1 + "-" + TODATE1;
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                HeaderCell2 = new TableCell();
                HeaderCell2.Text = "BillDate:" + FDATE + "-" + TODATE;
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);



                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch
        {
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void billdate()
    {

        try
        {

            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + pcode + "'  order by  Bill_frmdate desc  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    GETTID = dr["TID"].ToString();

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


    public void billdate0()
    {

        try
        {


            string date = ddl_BillDate.Text;
            string[] p = date.Split('/','-');

            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];

            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
           
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + pcode + "' and  Bill_frmdate='"+FDATE+"' and Bill_todate='"+TODATE+"'  order by  Bill_frmdate desc  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    GETTID = dr["TID"].ToString();

                    //frmdate = d1.ToString("dd/MM/yyy");
                    //Todate = d2.ToString("dd/MM/yyy");
                    

                }




            }
        }
        catch
        {


        }


    }

    public void billdate1()
    {

        try
        {

            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT TOP 1 Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + pcode + "' AND Tid < '" + GETTID + "' ORDER BY Tid DESC ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {


                    d11 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d22 = Convert.ToDateTime(dr["Bill_todate"].ToString());

                    //frmdate1 = d11.ToString("dd/MM/yyy");

                    //Todate1 = d22.ToString("dd/MM/yyy");

                    FDATE1 = d11.ToString("MM/dd/yyyy");
                    TODATE1 = d22.ToString("MM/dd/yyyy");


                }

              //  ddl_BillDate.Items.Add(frmdate + "-" + Todate);


            }
        }
        catch
        {


        }


    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        billdate0();
        billdate1();

        getgrid();
        getgrid1();
        getgrid2();

        billdate();
    }

    public void getgrid()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "";
            con = DB.GetConnection();
            //   str = "SELECT *   FROM (sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='"+pcode+"' AND Prdate BETWEEN '"++"' AND '"++"' GROUP BY Agent_id,Ratechart_Id    ) AS LL LEFT JOIN ( sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='"++"' AND Prdate BETWEEN '"++"' AND '"++"' GROUP BY Agent_id,Ratechart_Id ) AS RH ON LL.Agent_id=RH.Agent_id ORDER BY RAND(RH.Agent_id)";
          //  str = "SELECT *   FROM (sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE1 + "' AND '" + TODATE1 + "' GROUP BY Agent_id,Ratechart_Id    ) AS LL LEFT JOIN ( sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE + "' AND '" + TODATE + "' GROUP BY Agent_id,Ratechart_Id ) AS RH ON LL.Agent_id=RH.Agent_id ORDER BY RAND(LL.Agent_id)";
            str = "SELECT AGENTID AS agent_id,RATECHART as ratechart_id,Agent_id,Ratechart_Id   FROM ( SELECT DISTINCT(LL.Ratechart_Id) AS RATECHART, LL.Agent_id AS AGENTID,RH.Agent_id,RH.Ratechart_Id   FROM (sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE  Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE1 + "' AND '" + TODATE1 + "' GROUP BY Agent_id,Ratechart_Id   ) AS LL LEFT JOIN (sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE + "' AND '" + TODATE + "' GROUP BY Agent_id,Ratechart_Id ) AS RH ON LL.Agent_id=RH.Agent_id   GROUP BY LL.Agent_id,LL.Ratechart_Id,RH.Agent_id,RH.Ratechart_Id   ) AS HH";
         //   str = "SELECT *   FROM (sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE  Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE1 + "' AND '" + TODATE1 + "' GROUP BY Agent_id,Ratechart_Id    ) AS LL LEFT JOIN ( sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE + "' AND '" + TODATE + "' GROUP BY Agent_id,Ratechart_Id ) AS RH ON LL.Agent_id=RH.Agent_id and ll.Ratechart_Id=rh.Ratechart_Id ORDER BY RAND(LL.Agent_id) asc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        }
        catch
        {


        }
    }
    public void getgrid1()
    {
        try
        {
            //DateTime dt1 = new DateTime();
            //DateTime dt2 = new DateTime();

            //string d1 = dt1.ToString("MM/dd/yyyy");
            //string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "";
            con = DB.GetConnection();
            //   str = "SELECT *   FROM (sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='"+pcode+"' AND Prdate BETWEEN '"++"' AND '"++"' GROUP BY Agent_id,Ratechart_Id    ) AS LL LEFT JOIN ( sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='"++"' AND Prdate BETWEEN '"++"' AND '"++"' GROUP BY Agent_id,Ratechart_Id ) AS RH ON LL.Agent_id=RH.Agent_id ORDER BY RAND(RH.Agent_id)";
            str = "SELECT * FROM(sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE1 + "' AND '" + TODATE1 + "' AND   Agent_id NOT IN (sELECT Agent_id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN  '" + FDATE + "' AND '" + TODATE + "' )) AS GG GROUP BY Agent_id,Ratechart_Id  order by Agent_id";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtt);
            GridView2.DataSource = dtt;
            GridView2.DataBind();
        }
        catch
        {


        }
    }
    public void getgrid2()
    {
        try
        {
            //DateTime dt1 = new DateTime();
            //DateTime dt2 = new DateTime();

            //string d1 = dt1.ToString("MM/dd/yyyy");
            //string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "";
            con = DB.GetConnection();
            //   str = "SELECT *   FROM (sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='"+pcode+"' AND Prdate BETWEEN '"++"' AND '"++"' GROUP BY Agent_id,Ratechart_Id    ) AS LL LEFT JOIN ( sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='"++"' AND Prdate BETWEEN '"++"' AND '"++"' GROUP BY Agent_id,Ratechart_Id ) AS RH ON LL.Agent_id=RH.Agent_id ORDER BY RAND(RH.Agent_id)";
            str = "SELECT * FROM(sELECT Agent_id,Ratechart_Id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE + "' AND '" + TODATE + "' AND   Agent_id NOT IN (sELECT Agent_id    FROM Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + FDATE1 + "' AND '" + TODATE1 + "' )) AS GG GROUP BY Agent_id,Ratechart_Id  order by Agent_id";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dttt);

            GridView3.DataSource = dttt;
            GridView3.DataBind();
            foreach (DataRow dr in dttt.Rows)
            {

                string agent="";
                 string ratechart="";
                string agen1=dr[0].ToString();
                 string ratechart1=dr[1].ToString();

                 dt.Rows.Add(agent, ratechart, agen1, ratechart1);



            }



            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch
        {


        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {

        try
        {



            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "BillDate-- " + FDATE1 + "-" + TODATE1 + "-Stopped Agents";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch
        {
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {



            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "BillDate-- " + FDATE + "-" +  TODATE + "-New Agents";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView3.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch
        {
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}