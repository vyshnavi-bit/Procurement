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
using System.Globalization;

public partial class Agentcharttype : System.Web.UI.Page
{

    DbHelper dbaccess = new DbHelper();
    string plantname;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    DataSet ds3 = new DataSet();
   
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    BLLuser Bllusers = new BLLuser();
    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;
    string date;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    string frmdate ;
    string Todate ;
    DateTime d1;
    DateTime d2;
    DateTime datee1;
    DateTime datee2;
    string getagents;
    string allcharttype;
    string getagents1;
    string allcharttype1;
    int sumres;
    int sumres1;
    int sumres2;
    int i = 0;
    int id = 0;
    int id1 = 2;
    int id2 = 3;
    int iii;


    int ssumres;
    int ssumres1;
    int ssumres2;
    int ii = 0;
    int iid = 0;
    int iid1 = 2;
    int iid2 = 3;
    int iiii;
    int chartassign;
    int chartassign1;

    public static int roleid;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;

                Label5.Visible = false;
                ddl_BillDate.Visible = false;
                Label4.Visible = false;
                ddl_BillDate1.Visible = false;
                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                if ((roleid >= 3) && ((roleid != 3)))
                { 
                    LoadPlantName();
                }
                if (roleid==9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();
                }
              }
            }

        else
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
             //   managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_PlantName.SelectedItem.Value;

                plantname = ddl_PlantName.SelectedItem.Text;

                Label5.Visible = false;
                ddl_BillDate.Visible = false;
                Label4.Visible = false;
                ddl_BillDate1.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }
    }


    private void LoadPlantName()
    {
        try
        {

            ds = null;
            ds = LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

                plantname = ddl_PlantName.SelectedItem.Text;

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSinglePlantName()
    {
        try
        {

            ds1 = null;
            ds1 = LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds1 != null)
            {
                ddl_PlantName.DataSource = ds1;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();
                plantname = ddl_PlantName.SelectedItem.Text;
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
             ddl_PlantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                  
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {

        Label5.Visible = true;
        ddl_BillDate.Visible = true;
        Label4.Visible = true;
        ddl_BillDate1.Visible = true;
        
        getagentwisechart();
        getagentwisechart1();
    }


    //public void getplantratechart()
    //{

        
       
    //    DateTime d1 = DateTime.ParseExact(frmdate, "MMddyyyy", CultureInfo.InvariantCulture);
    //    DateTime d2 = DateTime.ParseExact(Todate, "MMddyyyy", CultureInfo.InvariantCulture);

    //    SqlConnection con = new SqlConnection(connStr);

     
    //    string str = " select  Agent_id,RateStatus,Milk_kg,Milk_ltr,Fat,Snf  from procurement  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' and RateStatus=1  group by Agent_id,RateStatus,Milk_kg,Milk_ltr,Fat,Snf order by RateStatus";

    //    SqlCommand cmd = new SqlCommand(str,con);
    //    con.Open();
    //    DataTable dt = new DataTable();
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {

    //        GridView1.DataSource = dt;
    //        GridView1.DataBind();


    //    }

    //    else
    //    {

    //        GridView1.DataSource = null;
    //        GridView1.DataBind();

    //    }

       



    //}


    public void getagentwisechart()
    {

        billdate();

        //date
        //date d1 = date.ParseExact(frmdate, "MMddyyyy  HH:mm tt", CultureInfo.InvariantCulture);

        //DateTime d2 = DateTime.ParseExact(Todate, "MMddyyyy", CultureInfo.InvariantCulture);

        ////DateTime d1 = DateTime.ParseExact(frmdate, "MM/dd/YYYY", null);
        ////DateTime d2 = DateTime.ParseExact(frmdate, "MM/dd/YYYY", null);


        SqlConnection con = new SqlConnection(connStr);

   //     string str = "select  Agent_id,RateStatus  from procurement   where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'   group by Agent_id,RateStatus order by RateStatus   ";
    //    string str = " select  Agent_id,RateStatus,Milk_kg,Milk_ltr,Fat,Snf  from procurement  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'  group by Agent_id,RateStatus,Milk_kg,Milk_ltr,Fat,Snf order by RateStatus";
        string str = "select  Agent_id,ratechart_id,RateStatus  from procurement   where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'   group by Agent_id,ratechart_id,RateStatus order by RateStatus";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {









            GridView1.DataSource = dt;
             GridView1.DataBind();

             if (sumres == 0)
             {
                 GridView1.FooterRow.Cells[2].Text = "";

             }
             else
             {

                 GridView1.FooterRow.Cells[2].Text = sumres.ToString();
             }

             if (sumres1 == 0)
             {
                 GridView1.FooterRow.Cells[4].Text = "";
             }
             else
             {

                 GridView1.FooterRow.Cells[4].Text = sumres1.ToString();

             }

             if (sumres2 == 0)
             {
                 GridView1.FooterRow.Cells[6].Text = "";
             }
             else
             {

                 GridView1.FooterRow.Cells[6].Text = sumres2.ToString();

             }




            

              
                    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell cell0 = new TableCell();
                    if (GridView1.FooterRow.Cells[4].Text == "0")
                    {
                        cell0.Text = "";

                         iii = 0;
                    }
            if(  iii == 1)
            {
                    TableCell cell1 = new TableCell();
                    cell1.Text = "RouteWise";
            }

            if (iii == 0)
            {
                TableCell cell1 = new TableCell();
                cell1.Text = "";
            }

            //}
            GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
            GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
            GridView1.FooterStyle.Font.Bold = true;







        }

        else
        {

            GridView1.DataSource = null;
            GridView1.DataBind();

        }





    }

    public void getagentwisechart1()
    {


        currentbill();

        //date
        //date d1 = date.ParseExact(frmdate, "MMddyyyy  HH:mm tt", CultureInfo.InvariantCulture);

        //DateTime d2 = DateTime.ParseExact(Todate, "MMddyyyy", CultureInfo.InvariantCulture);

        ////DateTime d1 = DateTime.ParseExact(frmdate, "MM/dd/YYYY", null);
        ////DateTime d2 = DateTime.ParseExact(frmdate, "MM/dd/YYYY", null);


        SqlConnection con = new SqlConnection(connStr);

        //     string str = "select  Agent_id,RateStatus  from procurement   where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'   group by Agent_id,RateStatus order by RateStatus   ";
        //    string str = " select  Agent_id,RateStatus,Milk_kg,Milk_ltr,Fat,Snf  from procurement  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'  group by Agent_id,RateStatus,Milk_kg,Milk_ltr,Fat,Snf order by RateStatus";
        string str = "select  Agent_id,ratechart_id,RateStatus  from procurement   where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'   group by Agent_id,ratechart_id,RateStatus order by RateStatus";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {









            GridView2.DataSource = dt;
            GridView2.DataBind();

            if (sumres == 0)
            {
                GridView2.FooterRow.Cells[2].Text = "";

            }
            else
            {

                GridView2.FooterRow.Cells[2].Text = ssumres.ToString();
            }

            if (sumres1 == 0)
            {
                GridView2.FooterRow.Cells[4].Text = "";
            }
            else
            {

                GridView2.FooterRow.Cells[4].Text = ssumres1.ToString();

            }

            if (sumres2 == 0)
            {
                GridView2.FooterRow.Cells[6].Text = "";
            }
            else
            {

                GridView2.FooterRow.Cells[6].Text = ssumres2.ToString();

            }







            GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell cell0 = new TableCell();
            if (GridView2.FooterRow.Cells[4].Text == "0")
            {
                cell0.Text = "";

                iiii = 0;
            }
            if (iiii == 1)
            {
                TableCell cell1 = new TableCell();
                cell1.Text = "RouteWise";
            }

            if (iiii == 0)
            {
                TableCell cell1 = new TableCell();
                cell1.Text = "";
            }

            //}
            GridView2.FooterStyle.ForeColor = System.Drawing.Color.White;
            GridView2.FooterStyle.BackColor = System.Drawing.Color.Brown;
            GridView2.FooterStyle.Font.Bold = true;







        }

        else
        {

            GridView2.DataSource = null;
            GridView2.DataBind();

        }


    }


    public DataSet LoadSinglePlantNameChkLst1(string ccode, string pcode)
    {
        ds2 = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code, CONVERT(NVARCHAR(15),pcode+'_'+Plant_Name) AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
        ds2 = dbaccess.GetDataset(sqlstr);
        return ds2;
    }


    public DataSet LoadPlantNameChkLst1(string ccode)
    {
        ds3 = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code,pcode+'_'+Plant_Name AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "'  ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
        ds3 = dbaccess.GetDataset(sqlstr);
        return ds3;
    }

    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {


    //    billdate();

       
    }


    public void billdate()
    {

        ddl_BillDate.Items.Clear();
        SqlConnection con = new SqlConnection(connStr);

     //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT top 1 Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=1  order by  tid desc  ";
        SqlCommand cmd = new SqlCommand(str,con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {

            while (dr.Read())
            {

           d1  =Convert.ToDateTime(dr["Bill_frmdate"].ToString());
           d2    =Convert.ToDateTime(dr["Bill_todate"].ToString());


           frmdate = d1.ToString("dd/MM/yyy");

           Todate = d2.ToString("dd/MM/yyy");
                

            }

            ddl_BillDate.Items.Add(frmdate + "-" + Todate);


        }


    }

    public void currentbill()
    {

      //  ddl_BillDate.Items.Clear();

        ddl_BillDate1.Items.Clear();
        SqlConnection con = new SqlConnection(connStr);

        //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
        string str = "SELECT top 1 Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + pcode + "' AND Status=0  order by  tid desc  ";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {

            while (dr.Read())
            {

                d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());


                frmdate = d1.ToString("MM/dd/yyy");

                Todate = d2.ToString("MM/dd/yyy");


            }

            ddl_BillDate1.Items.Add(frmdate + "-" + Todate);


        }


    }




    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        //GridViewRow gvRow = e.Row;
        //if (gvRow.RowType == DataControlRowType.Header)
        //{
        //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell cell0 = new TableCell();
        //    cell0.Text = "PlantWise";
        //    cell0.HorizontalAlign = HorizontalAlign.Center;
        //    cell0.ColumnSpan = 3;
        //    TableCell cell1 = new TableCell();
        //    cell1.Text = "RouteWise";
        //    cell1.HorizontalAlign = HorizontalAlign.Center;
        //    cell1.ColumnSpan = 2;


        //    TableCell cell2 = new TableCell();
        //    cell2.Text = "AgentWise";
        //    cell2.HorizontalAlign = HorizontalAlign.Center;
        //    cell2.ColumnSpan = 6;


        //    gvrow.Cells.Add(cell0);
        //    gvrow.Cells.Add(cell1);
        //    gvrow.Cells.Add(cell2);
        //    GridView1.Controls[0].Controls.AddAt(0, gvrow);




     






            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow gvRow = e.Row;

                if (i == 0)
                {
                    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell cell0 = new TableCell();
                    cell0.Text = "PlantWise";
                    cell0.HorizontalAlign = HorizontalAlign.Center;
                    cell0.ColumnSpan = 3;
                    TableCell cell1 = new TableCell();
                    cell1.Text = "RouteWise";
                    cell1.HorizontalAlign = HorizontalAlign.Center;
                    cell1.ColumnSpan = 2;


                    TableCell cell2 = new TableCell();
                    cell2.Text = "AgentWise";
                    cell2.HorizontalAlign = HorizontalAlign.Center;
                    cell2.ColumnSpan = 6;


                    gvrow.Cells.Add(cell0);
                    gvrow.Cells.Add(cell1);
                    gvrow.Cells.Add(cell2);
                    GridView1.Controls[0].Controls.AddAt(0, gvrow);
                    i = 1;
                }
                //GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //TableCell cell0 = new TableCell();
                //cell0.Text = "PlantWise";
                //cell0.HorizontalAlign = HorizontalAlign.Center;
                //cell0.ColumnSpan = 3;
                //TableCell cell1 = new TableCell();
                //cell1.Text = "RouteWise";
                //cell1.HorizontalAlign = HorizontalAlign.Center;
                //cell1.ColumnSpan = 5;


                //TableCell cell2 = new TableCell();
                //cell2.Text = "AgentWise";
                //cell2.HorizontalAlign = HorizontalAlign.Center;
                //cell2.ColumnSpan = 5;


                //gvrow.Cells.Add(cell0);
                //gvrow.Cells.Add(cell1);
                //gvrow.Cells.Add(cell2);
                //GridView1.Controls[0].Controls.AddAt(0, gvrow);



                //GridView HeaderGrid = (GridView)sender;
                //GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //TableCell Cell_Header = new TableCell();
                //Cell_Header.Text = "PLANT";
                //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                //Cell_Header.ColumnSpan = 3;
                //HeaderRow.Cells.Add(Cell_Header);

                //Cell_Header = new TableCell();
                //Cell_Header.Text = "ROUTE ";
                //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                //Cell_Header.ColumnSpan = 2;
                //Cell_Header.RowSpan = 2;
                //HeaderRow.Cells.Add(Cell_Header);

                //Cell_Header = new TableCell();
                //Cell_Header.Text = "AGENT";
                //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                //Cell_Header.ColumnSpan = 2;
                //HeaderRow.Cells.Add(Cell_Header);

                //GridView1.Controls[0].Controls.AddAt(0, HeaderRow);


                //string agentid = e.Row.Cells[1].Text;
                //   int allcharttype = Convert.ToInt16(e.Row.Cells[2].Text);

                //string loanid = e.Row.Cells[2].Text;
                //SqlConnection con = new SqlConnection(connStr);


                //string str = "select  Agent_id,RateStatus  from procurement   where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'   group by Agent_id,RateStatus order by RateStatus";
                //SqlCommand cmd = new SqlCommand(str, con);
                //con.Open();
                //SqlDataReader dr = cmd.ExecuteReader();

                //if (dr.HasRows)
                //{

                //    while (dr.Read())
                //    {

                getagents = e.Row.Cells[1].Text;
                allcharttype =e.Row.Cells[2].Text;
                chartassign = Convert.ToInt16(e.Row.Cells[3].Text);
                //    }
                //}
                if (chartassign == 1)
                {
                    e.Row.Cells[1].Text = getagents;
                    e.Row.Cells[2].Text = allcharttype.ToString();


                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";


                    e.Row.Cells[5].Text = "";
                    e.Row.Cells[6].Text = "";


                    //GridView1.HeaderRow.Cells[3].Text = "";

                    //GridView1.HeaderRow.Cells[4].Text = "";


                    //GridView1.HeaderRow.Cells[5].Text = "";

                    //GridView1.HeaderRow.Cells[6].Text = "";

                   

                    //GridView1.HeaderRow.Cells[1].Visible = true;
                    //GridView1.HeaderRow.Cells[2].Visible = true;
                    //e.Row.Cells[1].Visible = true;
                    //e.Row.Cells[2].Visible = true;
                    //e.Row.Cells[3].Visible = false;
                    //e.Row.Cells[4].Visible = false;
                    //GridView1.HeaderRow.Cells[3].Visible = false;
                    //GridView1.HeaderRow.Cells[4].Visible = false;



                    //e.Row.Cells[5].Visible = false;
                    //e.Row.Cells[6].Visible = false;
                    //GridView1.HeaderRow.Cells[5].Visible = false;
                    //GridView1.HeaderRow.Cells[6].Visible = false;

                     sumres = sumres + 1;

                   
                   
                }

                if (chartassign == 2)
                {
                    e.Row.Cells[3].Text = getagents;
                    e.Row.Cells[4].Text = allcharttype.ToString();

                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "";

                    e.Row.Cells[5].Text = "";
                    e.Row.Cells[6].Text = "";



                    //GridView1.HeaderRow.Cells[1].Text = "";

                    //GridView1.HeaderRow.Cells[2].Text = "";


                    //GridView1.HeaderRow.Cells[5].Text = "";

                    //GridView1.HeaderRow.Cells[6].Text = "";


                    sumres1 = sumres1 + 1;

                    
                    //e.Row.Cells[3].Visible = true;
                    //e.Row.Cells[4].Visible = true;
                    //GridView1.HeaderRow.Cells[3].Visible = true;
                    //GridView1.HeaderRow.Cells[4].Visible = true;



                    //e.Row.Cells[1].Visible = false;
                    //e.Row.Cells[2].Visible = false;
                    //GridView1.HeaderRow.Cells[1].Visible = false;
                    //GridView1.HeaderRow.Cells[2].Visible = false;




                    //e.Row.Cells[5].Visible = false;
                    //e.Row.Cells[6].Visible = false;
                    //GridView1.HeaderRow.Cells[5].Visible = false;
                    //GridView1.HeaderRow.Cells[6].Visible = false;



                   

                }

                if (chartassign == 3)
                {
                    e.Row.Cells[5].Text = getagents;
                    e.Row.Cells[6].Text = allcharttype.ToString();



                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "";


                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";



                    //GridView1.HeaderRow.Cells[1].Text = "";

                    //GridView1.HeaderRow.Cells[2].Text = "";


                    //GridView1.HeaderRow.Cells[3].Text = "";

                    //GridView1.HeaderRow.Cells[4].Text = "";

                    sumres2 = sumres2 + 1;

                    //e.Row.Cells[5].Visible = true;
                    //e.Row.Cells[6].Visible = true;
                    //GridView1.HeaderRow.Cells[5].Visible = true;
                    //GridView1.HeaderRow.Cells[6].Visible = true;



                    //e.Row.Cells[2].Visible = false;
                    //e.Row.Cells[3].Visible = false;
                    //GridView1.HeaderRow.Cells[2].Visible = false;
                    //GridView1.HeaderRow.Cells[3].Visible = false;




                    //e.Row.Cells[1].Visible = false;
                    //e.Row.Cells[2].Visible = false;
                    //GridView1.HeaderRow.Cells[1].Visible = false;
                    //GridView1.HeaderRow.Cells[2].Visible = false;


                }
            }

        
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //GridViewRow gvRow = e.Row;
        //if (gvRow.RowType == DataControlRowType.Header)
        //{
        //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell cell0 = new TableCell();
        //    cell0.Text = "PlantWise";
        //    cell0.HorizontalAlign = HorizontalAlign.Center;
        //    cell0.ColumnSpan = 3;
        //    TableCell cell1 = new TableCell();
        //    cell1.Text = "RouteWise";
        //    cell1.HorizontalAlign = HorizontalAlign.Center;
        //    cell1.ColumnSpan = 2;


        //    TableCell cell2 = new TableCell();
        //    cell2.Text = "AgentWise";
        //    cell2.HorizontalAlign = HorizontalAlign.Center;
        //    cell2.ColumnSpan = 6;


        //    gvrow.Cells.Add(cell0);
        //    gvrow.Cells.Add(cell1);
        //    gvrow.Cells.Add(cell2);
        //    GridView1.Controls[0].Controls.AddAt(0, gvrow);











        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GridViewRow gvRow1 = e.Row;

            if (ii == 0)
            {
                GridViewRow gvrow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell0 = new TableCell();
                cell0.Text = "PlantWise";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 3;
                TableCell cell1 = new TableCell();
                cell1.Text = "RouteWise";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 2;


                TableCell cell2 = new TableCell();
                cell2.Text = "AgentWise";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 6;


                gvrow1.Cells.Add(cell0);
                gvrow1.Cells.Add(cell1);
                gvrow1.Cells.Add(cell2);
                GridView2.Controls[0].Controls.AddAt(0, gvrow1);
                ii = 1;
            }
            //GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell cell0 = new TableCell();
            //cell0.Text = "PlantWise";
            //cell0.HorizontalAlign = HorizontalAlign.Center;
            //cell0.ColumnSpan = 3;
            //TableCell cell1 = new TableCell();
            //cell1.Text = "RouteWise";
            //cell1.HorizontalAlign = HorizontalAlign.Center;
            //cell1.ColumnSpan = 5;


            //TableCell cell2 = new TableCell();
            //cell2.Text = "AgentWise";
            //cell2.HorizontalAlign = HorizontalAlign.Center;
            //cell2.ColumnSpan = 5;


            //gvrow.Cells.Add(cell0);
            //gvrow.Cells.Add(cell1);
            //gvrow.Cells.Add(cell2);
            //GridView1.Controls[0].Controls.AddAt(0, gvrow);



            //GridView HeaderGrid = (GridView)sender;
            //GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell Cell_Header = new TableCell();
            //Cell_Header.Text = "PLANT";
            //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            //Cell_Header.ColumnSpan = 3;
            //HeaderRow.Cells.Add(Cell_Header);

            //Cell_Header = new TableCell();
            //Cell_Header.Text = "ROUTE ";
            //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            //Cell_Header.ColumnSpan = 2;
            //Cell_Header.RowSpan = 2;
            //HeaderRow.Cells.Add(Cell_Header);

            //Cell_Header = new TableCell();
            //Cell_Header.Text = "AGENT";
            //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            //Cell_Header.ColumnSpan = 2;
            //HeaderRow.Cells.Add(Cell_Header);

            //GridView1.Controls[0].Controls.AddAt(0, HeaderRow);


            //string agentid = e.Row.Cells[1].Text;
            //   int allcharttype = Convert.ToInt16(e.Row.Cells[2].Text);

            //string loanid = e.Row.Cells[2].Text;
            //SqlConnection con = new SqlConnection(connStr);


            //string str = "select  Agent_id,RateStatus  from procurement   where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "'   group by Agent_id,RateStatus order by RateStatus";
            //SqlCommand cmd = new SqlCommand(str, con);
            //con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();

            //if (dr.HasRows)
            //{

            //    while (dr.Read())
            //    {

            getagents1 = e.Row.Cells[1].Text;
            allcharttype1 = e.Row.Cells[2].Text;
            chartassign1=Convert.ToInt16(e.Row.Cells[3].Text);

            //    }
            //}
            if (chartassign1 == 1)
            {
                e.Row.Cells[1].Text = getagents1;
                e.Row.Cells[2].Text = allcharttype1.ToString();


                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = "";


                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";


                //GridView1.HeaderRow.Cells[3].Text = "";

                //GridView1.HeaderRow.Cells[4].Text = "";


                //GridView1.HeaderRow.Cells[5].Text = "";

                //GridView1.HeaderRow.Cells[6].Text = "";



                //GridView1.HeaderRow.Cells[1].Visible = true;
                //GridView1.HeaderRow.Cells[2].Visible = true;
                //e.Row.Cells[1].Visible = true;
                //e.Row.Cells[2].Visible = true;
                //e.Row.Cells[3].Visible = false;
                //e.Row.Cells[4].Visible = false;
                //GridView1.HeaderRow.Cells[3].Visible = false;
                //GridView1.HeaderRow.Cells[4].Visible = false;



                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[6].Visible = false;
                //GridView1.HeaderRow.Cells[5].Visible = false;
                //GridView1.HeaderRow.Cells[6].Visible = false;

                ssumres = ssumres + 1;



            }

            if (chartassign1 == 2)
            {
                e.Row.Cells[3].Text = getagents1;
                e.Row.Cells[4].Text = allcharttype1.ToString();

                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";

                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";



                //GridView1.HeaderRow.Cells[1].Text = "";

                //GridView1.HeaderRow.Cells[2].Text = "";


                //GridView1.HeaderRow.Cells[5].Text = "";

                //GridView1.HeaderRow.Cells[6].Text = "";


                ssumres1 = ssumres1 + 1;


                //e.Row.Cells[3].Visible = true;
                //e.Row.Cells[4].Visible = true;
                //GridView1.HeaderRow.Cells[3].Visible = true;
                //GridView1.HeaderRow.Cells[4].Visible = true;



                //e.Row.Cells[1].Visible = false;
                //e.Row.Cells[2].Visible = false;
                //GridView1.HeaderRow.Cells[1].Visible = false;
                //GridView1.HeaderRow.Cells[2].Visible = false;




                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[6].Visible = false;
                //GridView1.HeaderRow.Cells[5].Visible = false;
                //GridView1.HeaderRow.Cells[6].Visible = false;





            }

            if (chartassign1 == 3)
            {
                e.Row.Cells[5].Text = getagents1;
                e.Row.Cells[6].Text = allcharttype1.ToString();



                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";


                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = "";



                //GridView1.HeaderRow.Cells[1].Text = "";

                //GridView1.HeaderRow.Cells[2].Text = "";


                //GridView1.HeaderRow.Cells[3].Text = "";

                //GridView1.HeaderRow.Cells[4].Text = "";

                ssumres2 = ssumres2 + 1;

                //e.Row.Cells[5].Visible = true;
                //e.Row.Cells[6].Visible = true;
                //GridView1.HeaderRow.Cells[5].Visible = true;
                //GridView1.HeaderRow.Cells[6].Visible = true;



                //e.Row.Cells[2].Visible = false;
                //e.Row.Cells[3].Visible = false;
                //GridView1.HeaderRow.Cells[2].Visible = false;
                //GridView1.HeaderRow.Cells[3].Visible = false;




                //e.Row.Cells[1].Visible = false;
                //e.Row.Cells[2].Visible = false;
                //GridView1.HeaderRow.Cells[1].Visible = false;
                //GridView1.HeaderRow.Cells[2].Visible = false;


            }
        }
    }
}