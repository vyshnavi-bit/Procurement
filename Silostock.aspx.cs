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
using System.Collections;
using System.Text;
using System.Web.Services;

public partial class Silostock : System.Web.UI.Page
{

   
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
    DataTable silostock = new DataTable();
    int i = 0;
    int j = 0;
    int K = 0;
    int L = 0;
    int M = 0;
    int N = 0;
    double SUMOPPRO=0;
    double SUMOPPRO1 = 0;
    double ASSIGNVALUE;
    int JK;
    int I;
    int JHG = 0;
    public static int roleid;
     public string sjson;
     public string jsondata = GetJsonData();
    protected void Page_Load(object sender, EventArgs e)
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

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
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
                Button2.Visible = false;
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
    //protected void Button1_Click(object sender, EventArgs e)
    //{
       
    //}
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode,pcode);
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


    public void getgridviewrows()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "";
            con = DB.GetConnection();
            str = "select  convert(varchar,Date,103) as Date     from Stock_Milk  where plant_code='" + pcode + "' and date  between '" + d1 + "' and '" + d2 + "'  ORDER by Date asc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            //foreach (datarow dd in dt.Rows)
            //{

            dt.Columns.Add("OpStock");
            dt.Columns.Add("ProcAM");
            dt.Columns.Add("ProcPM");
            dt.Columns.Add("TotPro");
            dt.Columns.Add("Despatch");
            dt.Columns.Add("Closing");
            dt.Columns.Add("Actual");
            dt.Columns.Add("Diff");

            GridView1.DataSource = dt;
            GridView1.DataBind();


            foreach (DataRow row in dt.Rows)
            {

                string get = GridView1.Rows[i].Cells[1].Text;

                DateTime txtMyDate = DateTime.ParseExact(get, "dd/MM/yyyy", null);
                DateTime date = txtMyDate.AddDays(-1);
                string datee = date.ToString("MM/dd/yyyy");



                str = "select   sum(milkkg) as kg    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee + "' ";
                con = DB.GetConnection();
                SqlCommand cmd1 = new SqlCommand(str, con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        GridView1.Rows[i].Cells[2].Text = dr["kg"].ToString();



                    }
                }

                i = i + 1;
            }

            foreach (DataRow row in dt.Rows)
            {

                string get = GridView1.Rows[j].Cells[1].Text;

                DateTime txtMyDate = DateTime.ParseExact(get, "dd/MM/yyyy", null);
                //    DateTime date = txtMyDate.AddDays(-1);
                DateTime date = txtMyDate;
                string datee = date.ToString("MM/dd/yyyy");
                //   str = "select   sum(milkkg) as kg    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee + "' ";
                string sss;
                sss = "select     isnull(sum(milk_kg),0) as  prMilk    from procurement  where plant_code='" + pcode + "'  and prdatE='" + datee + "' AND SESSIONS='AM'";
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd1 = new SqlCommand(sss, con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {

                            GridView1.Rows[j].Cells[3].Text = dr1["prMilk"].ToString();



                        }
                    }
                }

                j = j + 1;



                string get1 = GridView1.Rows[K].Cells[1].Text;

                DateTime txtMyDate1 = DateTime.ParseExact(get1, "dd/MM/yyyy", null);
                //    DateTime date = txtMyDate.AddDays(-1);
                DateTime date1 = txtMyDate;
                string datee1 = date.ToString("MM/dd/yyyy");
                //   str = "select   sum(milkkg) as kg    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee + "' ";
                string sss1;
                sss1 = "select     isnull(sum(milk_kg),0) as  prMilk    from procuremenTimport  where plant_code='" + pcode + "'  and prdatE='" + datee + "' AND SESSIONS='PM'";
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd11 = new SqlCommand(sss1, con);
                    SqlDataReader dr11 = cmd11.ExecuteReader();
                    if (dr11.HasRows)
                    {
                        while (dr11.Read())
                        {

                            GridView1.Rows[K].Cells[4].Text = dr11["prMilk"].ToString();



                        }
                    }
                }

                K = K + 1;


                string totpro = GridView1.Rows[N].Cells[1].Text;

                DateTime txtMyDatepro = DateTime.ParseExact(totpro, "dd/MM/yyyy", null);
                //    DateTime date = txtMyDate.AddDays(-1);
                DateTime datepro = txtMyDate;
                string datee1pro = date.ToString("MM/dd/yyyy");
                //   str = "select   sum(milkkg) as kg    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee + "' ";
                string sss1pro;
                sss1pro = "select     isnull(sum(milk_kg),0) as  prMilk    from procuremenTimport  where plant_code='" + pcode + "'  and prdatE='" + datee + "'";
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd11pro = new SqlCommand(sss1pro, con);
                    SqlDataReader dr11pro = cmd11pro.ExecuteReader();
                    if (dr11pro.HasRows)
                    {
                        while (dr11pro.Read())
                        {
                            double pro = Convert.ToDouble(dr11pro["prMilk"].ToString());

                            GridView1.Rows[N].Cells[5].Text = pro.ToString("F2");



                        }
                    }
                }

                N = N + 1;





                string get2 = GridView1.Rows[L].Cells[1].Text;

                DateTime txtMyDate2 = DateTime.ParseExact(get2, "dd/MM/yyyy", null);
                //    DateTime date = txtMyDate.AddDays(-1);
                DateTime date2 = txtMyDate;
                string datee2 = date.ToString("MM/dd/yyyy");
                //   str = "select   sum(milkkg) as kg    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee + "' ";
                string sss2;
                sss2 = "select  isnull(sum(milkkg),0) as  DESMilk     from DespatchEntry  where plant_code='" + pcode + "'  and date='" + datee2 + "'";
                //sss2 = "select     isnull(sum(milk_kg),0) as  prMilk    from procurement  where plant_code='" + pcode + "'  and prdatE='" + datee + "' AND SESSIONS='PM'";
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd2 = new SqlCommand(sss2, con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {

                            GridView1.Rows[L].Cells[6].Text = dr2["DESMilk"].ToString();



                        }
                    }
                }

                L = L + 1;





                string get21 = GridView1.Rows[M].Cells[1].Text;

                DateTime txtMyDate21 = DateTime.ParseExact(get2, "dd/MM/yyyy", null);
                //    DateTime date = txtMyDate.AddDays(-1);
                DateTime date21 = txtMyDate;
                string datee21 = date.ToString("MM/dd/yyyy");
                //   str = "select   sum(milkkg) as kg    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee + "' ";
                string sss21;
                //  sss21 = "select  isnull(sum(milkkg),0) as  DESMilk     from DespatchEntry  where plant_code='" + pcode + "'  and date='" + datee2 + "'";

                sss21 = "select   sum(milkkg) as kg    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee21 + "' ";
                //sss2 = "select     isnull(sum(milk_kg),0) as  prMilk    from procurement  where plant_code='" + pcode + "'  and prdatE='" + datee + "' AND SESSIONS='PM'";
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd21 = new SqlCommand(sss21, con);
                    SqlDataReader dr21 = cmd21.ExecuteReader();
                    if (dr21.HasRows)
                    {
                        while (dr21.Read())
                        {

                            GridView1.Rows[M].Cells[7].Text = dr21["kg"].ToString();



                        }
                    }
                }

                M = M + 1;



            }




            int HHH = GridView1.HeaderRow.Cells.Count;
            int HH = GridView1.Rows.Count;

            // foreach (int I = 0; I <= HH; I++)
            foreach (GridViewRow DT in GridView1.Rows)
            {



                for (JK = 2; JK < HHH; JK++)
                {

                    if (JK <= 4)
                    {
                        double ST = Convert.ToDouble(GridView1.Rows[I].Cells[JK].Text);
                        SUMOPPRO = SUMOPPRO + ST;

                    }

                    if (JK == 6)
                    {
                        double ST1 = Convert.ToDouble(GridView1.Rows[I].Cells[JK].Text);
                        SUMOPPRO1 = SUMOPPRO1 + ST1;


                    }



                }



                ASSIGNVALUE = SUMOPPRO - SUMOPPRO1;

                GridView1.Rows[I].Cells[8].Text = ASSIGNVALUE.ToString("f2");

                I = I + 1;

                SUMOPPRO = 0;
                SUMOPPRO1 = 0;











            }


            foreach (GridViewRow DT in GridView1.Rows)
            {

                double CLO = Convert.ToDouble(DT.Cells[7].Text);

                double ACTUA = Convert.ToDouble(DT.Cells[8].Text);

                string GETANS = (ACTUA - CLO).ToString("F2");

                GridView1.Rows[JHG].Cells[9].Text = GETANS.ToString();
                JHG = JHG + 1;

            }
        }
        catch
        {



        }

        

        //}

        //GridView1.DataSource = dt;
        //GridView1.DataBind();


    }

    //public void getSUBHEADNAME()
    //{

    //    string str = "";
    //    con = DB.GetConnection();

    //    //DateTime dt1 = new DateTime();
    //    //DateTime dt2 = new DateTime();
    //    //string d1 = dt1.ToString("MM/dd/yyyy");
    //    //string d2 = dt2.ToString("MM/dd/yyyy");
    //    str = "Select *   from expencesheader";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {
    //            dtpsubhead.Items.Add(dr["SubheaderName"].ToString());



    //        }
    //    }
    //    else
    //    {


    //    }




    //}

    //public void getgridviewrows()
    //{

    //    DateTime dt1 = new DateTime();
    //    DateTime dt2 = new DateTime();
    //    dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //    dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

    //    string d1 = dt1.ToString("MM/dd/yyyy");
    //    string d2 = dt2.ToString("MM/dd/yyyy");
    //    string str = "";
    //    con = DB.GetConnection();
    //    str = "select  convert(varchar,Date,103) as Date     from Stock_Milk  where plant_code='" + pcode + "' and date  between '" + d1 + "' and '" + d2 + "'  ORDER by Date asc";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    //foreach (datarow dd in dt.Rows)
    //    //{

    //    dt.Columns.Add("openingStock");
    //    dt.Columns.Add("AM");
    //    dt.Columns.Add("PM");
    //    dt.Columns.Add("Despatch");
    //    dt.Columns.Add("Closing");
    //    dt.Columns.Add("Diff");

    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();



    //    //}

    //    //GridView1.DataSource = dt;
    //    //GridView1.DataBind();


    //}





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
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddl_Plantname.Text +   ":Stock Detils:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2.ColumnSpan = 10;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;



        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            getgridviewrows();
           // GETSILOREPORT();
        }
        catch(Exception EE)
        {
         
        }

    }




    [WebMethod]//webmethod for a static getjsondata function so that the method can be accessed using jquery in aspx page.
    public static string GetJsonData()
    {
        //sql connection
       SqlConnection con=new SqlConnection();
        DbHelper DB=new DbHelper();
        string GETVALUES = "";
        GETVALUES = "sELECT PLANT_CODE,Sum(milk_kg) as Milkkg   FROM   procurement    where  prdate between '4-1-2017' and '4-30-2017' group by PLANT_CODE  order by  PLANT_CODE asc ";
        con = DB.GetConnection();
        SqlCommand cmd1 = new SqlCommand(GETVALUES, con);
        SqlDataAdapter dsp1 = new SqlDataAdapter(cmd1);
        DataTable silostock=new DataTable();
        dsp1.Fill(silostock);
        SqlDataReader dr;
        dr = cmd1.ExecuteReader();

        //create a JSON string to describe the data from the database

        StringBuilder JSON = new StringBuilder();
        string prefix = "";
        JSON.Append("[");
        while (dr.Read())
        {
            JSON.Append(prefix + "{");
            JSON.Append("\"PLANT_CODE\":" + "\"" + dr[0] + "\",");
            JSON.Append("\"Milkkg\":" + dr[1]);
            JSON.Append("}");
            prefix = ",";
        }
        JSON.Append("]");

        return JSON.ToString();
    }

    
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}