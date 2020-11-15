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
public partial class RemarkDetails : System.Web.UI.Page
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
    DataTable dtk = new DataTable();
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
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                    if((roleid>=3) && (roleid!=9))
                    {
                        LoadPlantcode();
                    }
                    else
                    {
                        Session["Plant_Code"] = "170".ToString();
                        pcode = "170";
                        loadspecialsingleplant();
                    }
                     DateTime txtMyDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                    //DateTime dttt = DateTime.Parse(txt_FromDate.Text);

                    //DateTime cutime = DateTime.Parse(gettime);
                    //string sff = dttt.AddDays(1).ToString();

                    //GETDATE = txt_FromDate.Text;

                    DateTime date = txtMyDate.AddDays(-1);

                    string datee = date.ToString("MM/dd/yyyy");
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

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

        try
        {
            plant = ddl_Plantname.SelectedValue.Split('_');
            pcode = plant[0].ToString();
            getgrid();
        }
        catch
        {

        }
    }


    public void getgrid()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        string str = "";
        con = DB.GetConnection();
        str = "SELECT CONVERT(VARCHAR,PRDATE,103) AS Date    FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE BETWEEN '" + d1 + "' AND '" + d2 + "'   GROUP BY PRDATE";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        dtk.Columns.Add("Date");
        dtk.Columns.Add("+AMCount");
        dtk.Columns.Add("+AMFat");
        dtk.Columns.Add("+AMSnf");
        dtk.Columns.Add("-AMCount");
        dtk.Columns.Add("-AMFat");
        dtk.Columns.Add("-AMSnf");
        dtk.Columns.Add("+PMCount");
        dtk.Columns.Add("+PMPFat");
        dtk.Columns.Add("+PMSnf");
        dtk.Columns.Add("-PMCount");
        dtk.Columns.Add("-PMFat");
        dtk.Columns.Add("-PMSnf");



        foreach (DataRow dr in dt.Rows)
        {

            string getdate = dr[0].ToString();
            dtk.Rows.Add(getdate).ToString();
        }

        GridView1.DataSource = dtk;
        GridView1.DataBind();

    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        try
        {



            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Date";
                HeaderCell2.ColumnSpan = 2;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                HeaderCell2 = new TableCell();
                HeaderCell2.Text = "AM INCREASEDETAILS";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                HeaderCell2 = new TableCell();
                HeaderCell2.Text = "AM DECREASEDETAILS";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                HeaderCell2 = new TableCell();
                HeaderCell2.Text = "PM INCREASEDETAILS";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);

                HeaderCell2 = new TableCell();
                HeaderCell2.Text = "PM DECREASEDETAILS";
                HeaderCell2.ColumnSpan = 3;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);



                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = ddl_Plantname.Text + "-REMARK DEATILS:FROM=" + txt_FromDate.Text + "TO:" + txt_ToDate.Text;
                HeaderCell2.ColumnSpan = 14;
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

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string DTH = (e.Row.Cells[1].Text);



                string[] getdat = DTH.Split('/');
                GETDATE = getdat[1] + "/" + getdat[0] + "/"+ getdat[2];
                con = DB.GetConnection();
                string str = "SELECT   COUNT(*) AS COUNT,SUM(DIFFFAT) AS INCFAT,SUM(DIFFSNF) AS INCSNF  FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE='" + GETDATE + "' And sessions='AM' AND (DIFFFAT > 0  OR  DIFFSNF > 0 )  GROUP BY SESSIONS,PRDATE ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string getcou = dr["COUNT"].ToString();
                    //double INCFAT = Convert.ToDouble(dr["INCFAT"].ToString());
                    //double INCSNF = Convert.ToDouble(dr["INCSNF"].ToString());

                    e.Row.Cells[2].Text = getcou.ToString();
                    //e.Row.Cells[3].Text = INCFAT.ToString("f2");
                    //e.Row.Cells[4].Text = INCSNF.ToString("f2");

                }

                con.Close();
                con = DB.GetConnection();
                string strfat = "SELECT   SUM(DIFFFAT) AS INCFAT,SUM(DIFFSNF) AS INCSNF  FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE='" + GETDATE + "' And sessions='AM' AND (DIFFFAT > 0 )  GROUP BY SESSIONS,PRDATE ";
                SqlCommand cmdfat = new SqlCommand(strfat, con);
                SqlDataReader drfat = cmdfat.ExecuteReader();

                if (drfat.Read())
                {

                    double INCFAT = Convert.ToDouble(drfat["INCFAT"].ToString());



                    e.Row.Cells[3].Text = INCFAT.ToString("f2");


                }


                con.Close();
                con = DB.GetConnection();
                string strsnf = "SELECT    SUM(DIFFSNF) AS INCSNF  FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE='" + GETDATE + "' And sessions='AM' AND (DIFFSNF > 0 )  GROUP BY SESSIONS,PRDATE ";
                SqlCommand cmdsnf = new SqlCommand(strsnf, con);
                SqlDataReader drsnf = cmdsnf.ExecuteReader();

                if (drsnf.Read())
                {

                    double INCSNF = Convert.ToDouble(drsnf["INCSNF"].ToString());



                    e.Row.Cells[4].Text = INCSNF.ToString("f2");


                }
                else
                {
                    e.Row.Cells[4].Text = "0.00";

                }




                con.Close();
                con = DB.GetConnection();
                string str1 = "SELECT   COUNT(*) AS COUNT     FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE ='" + GETDATE + "' AND   (( DIFFFAT < 0)  or ( DIFFSNF < 0))  and SESSIONS='AM'  GROUP BY SESSIONS,PRDATE";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    string getcou = dr1["COUNT"].ToString();

                    //     double DECFAT = Convert.ToDouble(dr1["DECFAT"].ToString());
                    //double DECSNF = Convert.ToDouble(dr1["DECDSNF"].ToString());

                    e.Row.Cells[5].Text = getcou.ToString();
                    //e.Row.Cells[6].Text = DECFAT.ToString("F2");
                    //e.Row.Cells[7].Text = DECSNF.ToString("F2");


                }


                con.Close();
                con = DB.GetConnection();
                string str11 = "SELECT   SUM(DIFFFAT) AS DECFAT     FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE ='" + GETDATE + "' AND   (( DIFFFAT < 0))  and SESSIONS='AM'  GROUP BY SESSIONS,PRDATE";
                SqlCommand cmd11 = new SqlCommand(str11, con);
                SqlDataReader dr11 = cmd11.ExecuteReader();

                if (dr11.Read())
                {

                    double DECFAT = Convert.ToDouble(dr11["DECFAT"].ToString());
                    double DECFATFINAL = DECFAT * (-1);
                    e.Row.Cells[6].Text = DECFATFINAL.ToString("F2");


                }

                else
                {
                    e.Row.Cells[6].Text = "0.00";

                }

                con.Close();
                con = DB.GetConnection();
                string str111 = "SELECT   ISNULL(SUM(DIFFSNF),0) AS DECDSNF     FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE ='" + GETDATE + "' AND   (( DIFFSNF < 0))  and SESSIONS='AM'  GROUP BY SESSIONS,PRDATE";
                SqlCommand cmd111 = new SqlCommand(str111, con);
                SqlDataReader dr111 = cmd111.ExecuteReader();

                if (dr111.Read())
                {

                    double DECSNF = Convert.ToDouble(dr111["DECDSNF"].ToString());

                    double DECSNFFINAL = DECSNF * (-1);
                    e.Row.Cells[7].Text = DECSNFFINAL.ToString("F2");


                }
                else
                {
                    e.Row.Cells[7].Text = "0.00";

                }






                con = DB.GetConnection();
                string strPM = "SELECT   COUNT(*) AS COUNT,SUM(DIFFFAT) AS INCFAT,SUM(DIFFSNF) AS INCSNF  FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE='" + GETDATE + "' And sessions='PM' AND (DIFFFAT > 0  OR  DIFFSNF > 0 )  GROUP BY SESSIONS,PRDATE ";
                SqlCommand cmdPM = new SqlCommand(strPM, con);
                SqlDataReader drPM = cmdPM.ExecuteReader();

                if (drPM.Read())
                {
                    string getcou = drPM["COUNT"].ToString();
                    //double INCFAT = Convert.ToDouble(drPM["INCFAT"].ToString());
                    //double INCSNF = Convert.ToDouble(drPM["INCSNF"].ToString());

                    e.Row.Cells[8].Text = getcou.ToString();
                    //e.Row.Cells[9].Text = INCFAT.ToString("f2");
                    //e.Row.Cells[10].Text = INCSNF.ToString("f2");

                }

                con.Close();
                con = DB.GetConnection();
                string strPMIC = "SELECT  SUM(DIFFFAT) AS INCFAT  FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE='" + GETDATE + "' And sessions='PM' AND (DIFFFAT > 0 )  GROUP BY SESSIONS,PRDATE ";
                SqlCommand cmdPMINC = new SqlCommand(strPMIC, con);
                SqlDataReader drPMINC = cmdPMINC.ExecuteReader();

                if (drPMINC.Read())
                {
                    // string drPMINC = cmdPMINC["COUNT"].ToString();
                    double INCFAT = Convert.ToDouble(drPMINC["INCFAT"].ToString());
                    //double INCSNF = Convert.ToDouble(drPM["INCSNF"].ToString());

                    //  e.Row.Cells[8].Text = getcou.ToString();
                    e.Row.Cells[9].Text = INCFAT.ToString("f2");
                    //e.Row.Cells[10].Text = INCSNF.ToString("f2");

                }

                con.Close();
                con = DB.GetConnection();
                string strPMINC1 = "SELECT  SUM(DIFFSNF) AS INCSNF  FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE='" + GETDATE + "' And sessions='PM' AND (DIFFSNF > 0 )  GROUP BY SESSIONS,PRDATE ";
                SqlCommand cmdPMINC1 = new SqlCommand(strPMINC1, con);
                SqlDataReader drPMINC1 = cmdPMINC1.ExecuteReader();

                if (drPMINC1.Read())
                {
                    // string getcou = drPMINC1["COUNT"].ToString();
                    //double INCFAT = Convert.ToDouble(drPM["INCFAT"].ToString());
                    double INCSNF = Convert.ToDouble(drPMINC1["INCSNF"].ToString());

                    //  e.Row.Cells[8].Text = getcou.ToString();
                    //e.Row.Cells[9].Text = INCFAT.ToString("f2");
                    e.Row.Cells[10].Text = INCSNF.ToString("f2");

                }
                else
                {
                    e.Row.Cells[10].Text = "0.00";


                }



                con.Close();
                con = DB.GetConnection();
                string str1PM = "SELECT   COUNT(*) AS COUNT     FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE ='" + GETDATE + "' AND   (( DIFFFAT < 0)  or ( DIFFSNF < 0))  and SESSIONS='PM'  GROUP BY SESSIONS,PRDATE";
                SqlCommand cmd1PM = new SqlCommand(str1PM, con);
                SqlDataReader dr1PM = cmd1PM.ExecuteReader();

                if (dr1PM.Read())
                {
                    string getcou = dr1PM["COUNT"].ToString();

                    //     double DECFAT = Convert.ToDouble(dr1["DECFAT"].ToString());
                    //double DECSNF = Convert.ToDouble(dr1["DECDSNF"].ToString());

                    e.Row.Cells[11].Text = getcou.ToString();
                    //e.Row.Cells[6].Text = DECFAT.ToString("F2");
                    //e.Row.Cells[7].Text = DECSNF.ToString("F2");


                }


                con.Close();
                con = DB.GetConnection();
                string str11PM = "SELECT   SUM(DIFFFAT) AS DECFAT     FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE ='" + GETDATE + "' AND   (( DIFFFAT < 0))  and SESSIONS='PM'  GROUP BY SESSIONS,PRDATE";
                SqlCommand cmd11PM = new SqlCommand(str11PM, con);
                SqlDataReader dr11PM = cmd11PM.ExecuteReader();

                if (dr11PM.Read())
                {

                    double DECFAT = Convert.ToDouble(dr11PM["DECFAT"].ToString());
                    double DECFATFINAL = DECFAT * (-1);
                    e.Row.Cells[12].Text = DECFATFINAL.ToString("F2");


                }


                con.Close();
                con = DB.GetConnection();
                string str111PM = "SELECT   ISNULL(SUM(DIFFSNF),0) AS DECDSNF     FROM  PROCUREMENTIMPORT    WHERE PLANT_CODE='" + pcode + "'  AND PRDATE ='" + GETDATE + "' AND   (( DIFFSNF < 0))  and SESSIONS='PM'  GROUP BY SESSIONS,PRDATE";
                SqlCommand cmd111PM = new SqlCommand(str111PM, con);
                SqlDataReader dr111PM = cmd111PM.ExecuteReader();

                if (dr111PM.Read())
                {

                    double DECSNF = Convert.ToDouble(dr111PM["DECDSNF"].ToString());

                    double DECSNFFINAL = DECSNF * (-1);
                    e.Row.Cells[13].Text = DECSNFFINAL.ToString("F2");


                }
                else
                {
                    e.Row.Cells[13].Text = "0.00";

                }




            }
        }

        catch
        {


        }
    }
}