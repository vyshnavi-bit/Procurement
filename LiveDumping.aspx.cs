using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.IO;

public partial class LiveDumping : System.Web.UI.Page
{

    string date;
    string sess;
    string ccode = "1";
    double totalMonthlySalaries = 0;
    DateTime frmdat = new DateTime();
    DateTime todat = new DateTime();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection conn1 = new SqlConnection("Data Source=223.196.32.28;Initial Catalog=AMPS;User ID=sa;Password=vyshnavi123");
    public static int roleid;

    //GAUGE
    StringBuilder str = new StringBuilder();
    //LineGraph
    public string pcode1 = string.Empty;
    int asize = 0;
    public string uid;
    protected void Page_Load(object sender, EventArgs e)
  {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                //if (uid == "15013")
                //{
                //   Server.Transfer("LiveDumping1.aspx");
                //}
                //else
                //{

                if((roleid>=3) && (roleid!=9))
                {
                frmdat = System.DateTime.Now;
                txt_fromdate.Text = frmdat.ToString("dd/MM/yyyy");
                getdatestring();
                getdetails();
                Lbl_YMilk.Text = GetYesDayMilk().ToString();
                BindChart();
                }
                if (roleid == 9)
                {
                    frmdat = System.DateTime.Now;
                    txt_fromdate.Text = frmdat.ToString("dd/MM/yyyy");
                    getdatestring();
                    getdetailskuppam();
                    Lbl_YMilk.Text = GetYesDayMilk().ToString();
                    BindChart();

                }

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
             if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                //uid = Session["User_ID"].ToString();
                //if (uid == "15013")
                //{
                //    Server.Transfer("LiveDumping1.aspx");
                //}
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
           
        }

    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        getdatestring();

        getdetails();
      
       // getAnalyzer();
       // getconnectionr();
        GrandTotal();


        ////Label2.Text = System.DateTime.Now.ToString();
        //// Label3.Text = System.DateTime.Now.ToLongDateString();
        Label6.Text = System.DateTime.Now.ToLongTimeString();
       // Label6.Attributes.Add("style", "font-size:50px; color:Red; font-weight:bold;");
       // Label3.Attributes.Add("style", "font-size:50px; color:Red; font-weight:bold;");
        //// Label4.Attributes.Add("style", "font-size:50px; color:white; font-weight:bold;");

        BindChart();
    }

    public void getdatestring()
    {
        string str = string.Empty;
        int sesscount = 0;
        date = System.DateTime.Now.ToShortDateString();
        sess = System.DateTime.Now.ToString("tt");
        str = "SELECT  COUNT(*) AS Counts FROM  lp Where Prdate='" + date + "' AND Sessions='" + sess + "'";
        sesscount = dbaccess.ExecuteScalarint(str);

        if (sesscount > 0)
        {
            sesscount = 0;
        }
        else
        {
            if (sess == "AM")
            {
                sess = "PM";
            }
            else
            {
                sess = "AM";
            }
        }


    }

    public void getdetails1()
    {
        try
        {
            //date = System.DateTime.Now.ToShortDateString();          
            //sess = System.DateTime.Now.ToString("tt");
            SqlConnection con = new SqlConnection(connStr);
            con.Open();        

           
//            string str = "SELECT sno,t1.PlantCode,prdate,Sessions,milkkg,sampleno,fat,snf,Plant_Name,Stime,Etime FROM " +
//" (select * from (Select  ROW_NUMBER() OVER (ORDER BY plantcode) AS [sno],plantcode,convert(varchar,prdate,103) as prdate,sessions,CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,count(*) as sampleno,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where   prdate='" + date + "' and  sessions='" + sess + "' group by  plantcode,prdate,sessions) as a left join (select plant_code,plant_Name  from Plant_Master) as b on a.plantcode=b.Plant_Code ) AS t1 " +
//"  LEFT JOIN  " +
//" (SELECT MIN(StartingTime) AS Stime,MAX(EndingTime) AS Etime,PlantCode  FROM Lp Where   prdate='" + date + "' and  sessions='" + sess + "' GROUP BY PlantCode) AS t2 ON t1.PlantCode=t2.PlantCode ";
            string str = "SELECT sno,t1.PlantCode,CONVERT(Nvarchar(8),Plant_Name) AS Plant_Name,milkkg,sampleno,Stime,Etime,fat,snf FROM " +
" (select * from (Select  ROW_NUMBER() OVER (ORDER BY plantcode) AS [sno],plantcode,convert(varchar,prdate,103) as prdate,sessions,CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,count(*) as sampleno,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where   prdate='" + date + "' and  sessions='" + sess + "' group by  plantcode,prdate,sessions) as a left join (select plant_code,plant_Name  from Plant_Master) as b on a.plantcode=b.Plant_Code ) AS t1 " +
"  LEFT JOIN  " +
" (SELECT MIN(StartingTime) AS Stime,MAX(EndingTime) AS Etime,PlantCode  FROM Lp Where   prdate='" + date + "' and  sessions='" + sess + "' GROUP BY PlantCode) AS t2 ON t1.PlantCode=t2.PlantCode ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdLivepro.DataSource = dt;
            grdLivepro.DataBind();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            int columncoount = dt.Columns.Count;
            if (columncoount > 0)
            {
                //Gridcolor(columncoount);
            }
        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    private void getdetails()
    {
        try
        {
            DataTable resdt = new DataTable();
            SqlConnection conn = null;
            date = System.DateTime.Now.ToShortDateString();
          //  sess = System.DateTime.Now.ToString("tt");
            sess = "AM";

            using (conn = dbaccess.GetConnection())
            {
              //  SqlCommand sqlCmd = new SqlCommand("dbo.Lp_procurementShow");
                SqlCommand sqlCmd = new SqlCommand("[Lp_procurementShowAll]");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(new SqlParameter("@fdate", date));
                sqlCmd.Parameters.Add(new SqlParameter("@session", sess));
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);


                adp.Fill(resdt);
                grdLivepro.DataSource = resdt;
                grdLivepro.DataBind();
                
                int columncoount = resdt.Columns.Count;
                int Rowcoount = resdt.Rows.Count;
                if (columncoount > 0)
                {
                   // Gridcolor(1,Rowcoount, columncoount);
                }

            }
        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    private void getdetailskuppam()
    {
        try
        {
            DataTable resdt = new DataTable();
            SqlConnection conn = null;
            date = System.DateTime.Now.ToShortDateString();
            //  sess = System.DateTime.Now.ToString("tt");
            sess = "AM";

            using (conn = dbaccess.GetConnection())
            {
                //  SqlCommand sqlCmd = new SqlCommand("dbo.Lp_procurementShow");
                SqlCommand sqlCmd = new SqlCommand("[Lp_procurementShowAllFORKUPPAM]");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(new SqlParameter("@fdate", date));
                sqlCmd.Parameters.Add(new SqlParameter("@session", sess));
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                adp.Fill(resdt);
                grdLivepro.DataSource = resdt;
                grdLivepro.DataBind();

                int columncoount = resdt.Columns.Count;
                int Rowcoount = resdt.Rows.Count;
                if (columncoount > 0)
                {
                    // Gridcolor(1,Rowcoount, columncoount);
                }

            }
        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    private void getAnalyzer()
    {
        try
        {
            DataTable resdt = new DataTable();
            SqlConnection conn = null;
            //date = System.DateTime.Now.ToShortDateString();
            //sess = System.DateTime.Now.ToString("tt");

            using (conn = dbaccess.GetConnection())
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.Lp_procurementAnalyzerShow");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(new SqlParameter("@fdate", date));
                sqlCmd.Parameters.Add(new SqlParameter("@session", sess));
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                adp.Fill(resdt);
                GridView1.DataSource = resdt;
                GridView1.DataBind();
                int columncoount = resdt.Columns.Count;
                int Rowcoount = resdt.Rows.Count;
                if (columncoount > 0)
                {
                   // Gridcolor(2,Rowcoount, columncoount);
                }

            }
        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    private void getconnectionr()
    {
        try
        {
            DataTable resdt = new DataTable();
            SqlConnection conn = null;
            //date = System.DateTime.Now.ToShortDateString();
            //sess = System.DateTime.Now.ToString("tt");

            using (conn = dbaccess.GetConnection())
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.Lp_procurementConncetionShow");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(new SqlParameter("@fdate", date));
                sqlCmd.Parameters.Add(new SqlParameter("@session", sess));
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);


                adp.Fill(resdt);

                GridView2.DataSource = resdt;
                GridView2.DataBind();               

            }
        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    private void Gridcolor(int c,int rowcount,int colcount)
    {
        try
        {
           // int rowcount = 0;
            int celcount = 0;
            int[] colr = new int[5] { 22, 27, 31, 44, 46 };

            for (int i = 0; i < rowcount; i++)
            {
                //rowcount = colr[i];
                for (int j = 0; j < colcount; j++)
                {
                    if (c == 1)
                    {
                        grdLivepro.Rows[i].Cells[j].BackColor = Color.White;
                        grdLivepro.Rows[i].Cells[j].ForeColor = Color.Red;
                    }
                    else
                    {
                        GridView1.Rows[i].Cells[j].BackColor = Color.White;
                        GridView1.Rows[i].Cells[j].ForeColor = Color.Green;
                    }
                    celcount++;
                }
            }
        }
        catch (Exception ex)
        {
            //Lbl_Errormsg.Visible = true;
            //Lbl_Errormsg.Text = ex.ToString();
        }
    }


    private DataTable GetData()
    {
        string d1, d2;
        frmdat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
        //todat = DateTime.ParseExact(date, "dd/MM/yyyy", null);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = frmdat.ToString("MM/dd/yyyy");
        DataTable dt = new DataTable();
        string cmd = "SELECT Sessions AS item ,SUM(CAST(Milkkg AS DECIMAL(18,2))) AS value FROM lp  Where Prdate = '" + d1.ToString().Trim() + "'  AND sessions='" + sess + "' GROUP BY Sessions";
        SqlDataAdapter adp = new SqlDataAdapter(cmd, conn1);
        adp.Fill(dt);
        return dt;
    }

    private double TodayDayMilk()
    {
        string d1, d2;
        frmdat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
        //todat = DateTime.ParseExact(date, "dd/MM/yyyy", null);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = frmdat.ToString("MM/dd/yyyy");
        double Rval = 0.0;
        string str = "SELECT SUM(CAST(Milkkg AS DECIMAL(18,2))) AS value FROM lp  Where Prdate ='" + d1.ToString().Trim() + "'  AND sessions='" + sess + "' GROUP BY Sessions";

        Rval = dbaccess.ExecuteScalardouble(str);
        return Rval;
    }
    private double GetYesDayMilk()
    {
        string d1, d2;
        frmdat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
        //todat = DateTime.ParseExact(date, "dd/MM/yyyy", null);
        frmdat = frmdat.AddDays(-1);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = frmdat.ToString("MM/dd/yyyy");
        double Rval = 0.0;
        string str = "SELECT SUM(CAST(Milkkg AS DECIMAL(18,2))) AS value FROM lp  Where Prdate ='" + d1.ToString().Trim() + "'  AND sessions='" + sess + "' GROUP BY Sessions";

        Rval = dbaccess.ExecuteScalardouble(str);
        return Rval;
    }

    private void BindChart()
    {
        
        DataTable dt = new DataTable();
        double Ymilk = GetYesDayMilk();
        Lbl_YMilk.Text = Ymilk.ToString();
        double Tomilk = TodayDayMilk();

        if ((Tomilk) > (Ymilk))
        {
            Ymilk = Tomilk;
        }

        try
        {
            dt = GetData();      

            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*gauge*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'item');
        data.addColumn('number', 'value');      

        data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                //str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["item"].ToString() + "');");
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + "TotalMilkKg" + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["value"].ToString() + ") ;");
            }


            str.Append("var options = {width: 300,min:0,max:" + Ymilk + ", height: 150,redFrom: 80, redTo: 90,yellowFrom:65, yellowTo: 80,minorTicks:5 };");
            // str.Append("var options = {width: 700,min:0,max:100000, height: 350,redFrom: 80, redTo: 90,yellowFrom:65, yellowTo: 80,minorTicks:5 };");
            str.Append(" var chart = new google.visualization.Gauge(document.getElementById('chart_div'));");
            str.Append(" chart.draw(data, options); }");
            str.Append("</script>");
           // lt.Text = "";
            lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');
        }
        catch
        {
        
        
        }
    }


    public void GrandTotal()
    {

        try
        {
            getdatestring();
            Label3.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //date = System.DateTime.Now.ToShortDateString();
            //sess = System.DateTime.Now.ToString("tt");
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //GTotalMilk for Buff
            string strb = "Select  CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='" + date + "' and  sessions='" + sess + "' AND PlantCode IN (157,165,166,167,168) AND fat>0 AND snf>0    group by  sessions";
            SqlCommand cmdb = new SqlCommand(strb, con);
            SqlDataReader drb = null;            
            drb = dbaccess.GetDatareader(strb);
            //
            string strbm = "Select  CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg  from lp  where prdate='" + date + "' and  sessions='" + sess + "' AND PlantCode IN (157,165,166,167,168)    group by  sessions";
            

            if (drb.HasRows)
            {
                drb.Read();
                //totalMilk.Text = "";
                //Afat.Text = "";
                //Asnf.Text = "";
                totalBMilk.Text = dbaccess.ExecuteScalarstr(strbm);
               // totalBMilk.Text = drb["milkkg"].ToString().Trim();
                totalBMilk.Attributes.Add("style", " color:Red; font-weight:bold;");
                ABfat.Text = drb["fat"].ToString().Trim();
                ABfat.Attributes.Add("style", " color:Red; font-weight:bold;");
                ABsnf.Text = drb["snf"].ToString().Trim();
                ABsnf.Attributes.Add("style", " color:Red; font-weight:bold; align:center");

            }
            //GTotalMilk for Cow
            //string strc = "Select  CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='" + date + "' and  sessions='" + sess + "' AND PlantCode IN (155,156,158,159,161,162,163,164) AND fat>0 AND snf>0  group by  sessions";
            string strc = "Select  CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='" + date + "' and  sessions='" + sess + "' AND PlantCode IN (155,156,158,159,161,162,163,164) AND fat>0 AND snf>0   group by  sessions";
            SqlCommand cmdc = new SqlCommand(strb, con);
            SqlDataReader drc = null;
            drc = dbaccess.GetDatareader(strc);
            string strcm = "Select  CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg from lp  where prdate='" + date + "' and  sessions='" + sess + "' AND PlantCode IN (155,156,158,159,161,162,163,164)   group by  sessions";

            if (drc.HasRows)
            {
                drc.Read();
                //totalMilk.Text = "";
                //Afat.Text = "";
                //Asnf.Text = "";
                totalcMilk.Text = dbaccess.ExecuteScalarstr(strcm);
                //totalcMilk.Text = drc["milkkg"].ToString().Trim();
                totalcMilk.Attributes.Add("style", " color:Green; font-weight:bold;");
                Acfat.Text = drc["fat"].ToString().Trim();
                Acfat.Attributes.Add("style", " color:Green; font-weight:bold;");
                Acsnf.Text = drc["snf"].ToString().Trim();
                Acsnf.Attributes.Add("style", " color:Green; font-weight:bold; align:center");

            }

            //GTotalMilk
            string str = "Select  CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='" + date + "' and  sessions='" + sess + "'  group by  sessions";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = null;
            dr = dbaccess.GetDatareader(str);
            if (dr.HasRows)
            {
                dr.Read();
                //totalMilk.Text = "";
                //Afat.Text = "";
                //Asnf.Text = "";

                //Grand Total of milk
                //totalMilk.Text = dr["milkkg"].ToString().Trim();
                //totalMilk.Attributes.Add("style", " color:Brown; font-weight:bold;");
                //Afat.Text = dr["fat"].ToString().Trim();
                //Afat.Attributes.Add("style", " color:Brown; font-weight:bold;");
                //Asnf.Text = dr["snf"].ToString().Trim();
                //Asnf.Attributes.Add("style", " color:Brown; font-weight:bold; align:center");

            }

        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            pcode1 = grdrow.Cells[13].Text;
            Get_RouteMilkStatus(ccode, pcode1);
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkView1_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;

            string Rid = string.Empty;
            Rid = grdrow.Cells[0].Text;
            string[] spilvalarr = new string[2];
            spilvalarr = Rid.Split('_');
            Rid = spilvalarr[0];
            RoutewiseLineGraph(Rid,pcode1);

           
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdLivepro_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {         
           
            //var titleCell = e.Row.Cells[5];
            //titleCell.Controls.Clear();
            //titleCell.Controls.Add(new LinkButton { Text = titleCell.Text, CommandName = "link", CommandArgument = e.Row.Cells[0].ToString() });


            string spilval = string.Empty;
            string[] spilvalarr = new string[2];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                spilval = string.Empty;
                spilval = e.Row.Cells[13].Text;
                spilvalarr = spilval.Split('_');
                spilval = spilvalarr[0];
                e.Row.Cells[13].Visible = false;

                if (spilval == "157" || spilval == "165" || spilval == "166" || spilval == "167" || spilval == "168" || spilval == "169")
                {
                    // e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;
                 
                   
                    // e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;
                    e.Row.Cells[0].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[2].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[3].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[4].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[6].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[9].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[11].BackColor = System.Drawing.Color.YellowGreen;
                  
                   

                    if (e.Row.Cells[12].Text == "Active")
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Green;
                    }
                    else if (e.Row.Cells[12].Text == "Delay")
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                    }
                    else if (e.Row.Cells[12].Text == "completed")
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Orange;
                    }
                    else
                    {
                    }

                }
                else
                {
                    // e.Row.Cells[0].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;

                    // e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;
                    e.Row.Cells[0].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[2].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[3].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[4].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[6].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[9].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.LightSeaGreen;
                    e.Row.Cells[11].BackColor = System.Drawing.Color.LightSeaGreen;

                    if (e.Row.Cells[12].Text == "Active")
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Green;
                    }
                    else if (e.Row.Cells[12].Text == "Delay")
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                    }
                    else if (e.Row.Cells[12].Text == "completed")
                    {
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].BackColor = System.Drawing.Color.Orange;
                    }
                    else
                    {
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Gv_PlantRouteData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        catch (Exception ex)
        {
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string spilval = string.Empty;
        string[] spilvalarr = new string[2];
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            spilval = string.Empty;
            spilval = e.Row.Cells[0].Text;
            spilvalarr = spilval.Split('_');
            spilval = spilvalarr[0];
            if (spilval == "157" || spilval == "165" || spilval == "166" || spilval == "167" || spilval == "168" || spilval == "169")
            {
                 e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
               // e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;
               // e.Row.Cells[1].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[1].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[2].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[3].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[4].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[5].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[6].BackColor = System.Drawing.Color.LightGray;
            }
            else
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Green;
               // e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;

               e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;
               // e.Row.Cells[1].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[1].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[2].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[3].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[4].BackColor = System.Drawing.Color.LightGray;
                e.Row.Cells[5].BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Status = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Status = string.Empty;
            Status = e.Row.Cells[0].Text;
            if (Status == "Active")
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;

            }
            else
            {
                if (Status == "Delay")
                {
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.OrangeRed;
                    e.Row.Cells[0].BackColor = System.Drawing.Color.LightGray;
                }
            }

        }


    }

    private void Get_RouteMilkStatus(string ccode, string pcode)
    {
        try
        {
            getdatestring();
            frmdat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
            //sess = System.DateTime.Now.ToString("tt");
            string d1, d2;
            d1 = frmdat.ToString("MM/dd/yyyy");
            
            decimal total = 0, Fat = 0, Snf = 0;
            frmdat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
          
            d1 = frmdat.ToString("MM/dd/yyyy");
            d2 = todat.ToString("MM/dd/yyyy");
            DataTable dtr = new DataTable();
            // CONVERT(nvarchar(10),Plantcode)+'_'+RouteName AS
            string str = "SELECT CONVERT(nvarchar(10),Plantcode)+'_'+RouteName AS RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
 " (SELECT Routeid,ISNULL(CAST(SUM(Milkkg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf,Plantcode FROM Lp WHERE CompanyCode='" + ccode + "'  AND  prdate ='" + d1.ToString() + "' AND sessions='" + sess + "'  AND Plantcode='" + pcode + "'  GROUP BY Plantcode,Routeid ) AS t1 " +
 " INNER JOIN " +
 " (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Routeid=t2.Rid ORDER BY RAND(Routeid)";
            using (con = dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dtr);
                if (dtr.Rows.Count > 0)
                {
                    Gv_PlantRouteData.DataSource = dtr;
                    Gv_PlantRouteData.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dtr.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dtr.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dtr.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    Gv_PlantRouteData.FooterRow.Cells[0].Text = "Total/Avg";
                    Gv_PlantRouteData.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                   
                    Gv_PlantRouteData.FooterRow.Cells[1].Text = total.ToString("N2");
                    Gv_PlantRouteData.FooterRow.Cells[2].Text = Fat.ToString("f2");
                    Gv_PlantRouteData.FooterRow.Cells[3].Text = Snf.ToString("f2");


                    Gv_PlantRouteData.FooterRow.Font.Bold = true;
                   
                }
                else
                {
                    //gv_PlantMilkDetail.DataSource = dt;
                    //gv_PlantMilkDetail.DataBind();
                    //Lbl_Errormsg.Visible = true;
                    //Lbl_Errormsg.Text = "NO DATA...";
                    Gv_PlantRouteData.DataSource = dtr;
                    Gv_PlantRouteData.DataBind();
                }
                if (dtr.Rows.Count > 0)
                {

                    DataRow rows = dtr.NewRow();
                    rows["RouteName"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");

                    dtr.Rows.Add(rows);

                    Gv_PlantRouteData.DataSource = dtr;
                    Gv_PlantRouteData.DataBind();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    private static DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }

    private void RoutewiseLineGraph( string Rid, string pcode)
    {
        try
        {
            getdatestring();
            string d1;
            frmdat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);           
            d1 = frmdat.ToString("MM/dd/yyyy");
           // sess = System.DateTime.Now.ToString("tt");

            //string query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry1.SelectedItem.Value);
            string query = string.Format("SELECT (REPLACE( Convert(Nvarchar(5),EndingTime), ':', '.' ))AS StartingTime,REPLACE( Convert(Nvarchar(5),LabCaptureTime), ':', '.' )AS LabCaptureTime ,sampleno FROM Lp Where PlantCode='" + pcode1 + "' AND Prdate='" + d1.ToString().Trim() + "' AND RouteId IS NOT NULL AND RouteId='" + Rid + "'   AND sessions='" + sess + "' order by sampleno"); //AND RouteId=6   AND (sampleno > 20 AND  sampleno <40)
            DataTable dtA = GetData(query);

            // ArrayList x = new ArrayList(12);

            string[] x = new string[asize];
            ///decimal[] y = new decimal[asize];
            string[] y = new string[asize];
            decimal[] dArray = new decimal[asize];

            decimal[] y1 = new decimal[asize];

            string strvari = string.Empty;
            string strvariRes = string.Empty;
            string[] strlengharr = new string[4];
            string[] strlengharrRes = new string[4];
            string Loadsampleno = string.Empty;
            string CumLoadsampleno = string.Empty;
            string LoadA1 = string.Empty;
            string LoadA2 = string.Empty;

            string xTimechk = "0";

            string valarr = string.Empty;
            string[] tempvalarr = new string[2];
            int Resvalarr0 = 0;
            int Resvalarr1 = 0;
            string temp = string.Empty;
            //
            //Sametime SampleNo
            DataTable Sametime = new DataTable();
            DataRow RSametime = null;
            DataColumn CSametime = null;
            Sametime.Columns.Add("Wtime", typeof(decimal));
            Sametime.Columns.Add("WSampleNo", typeof(string));
            Sametime.Columns.Add("WCumSampleNo", typeof(string));

            Sametime.Columns.Add("Atime", typeof(decimal));
            Sametime.Columns.Add("ACumSampleNo", typeof(string));

            DataTable RdtA = new DataTable();
            DataRow RdrA = null;
            DataColumn RcolA = null;
            RdtA.Columns.Add("Wtime", typeof(decimal));
            RdtA.Columns.Add("SampleNo", typeof(string));
            RdtA.Columns.Add("A1", typeof(string));
            RdtA.Columns.Add("A2", typeof(string));

            //RcolA = new DataColumn("Wtime");
            //RdtA.Columns.Add(RcolA);
            //RcolA = new DataColumn("SampleNo");
            //RdtA.Columns.Add(RcolA);
            //RcolA = new DataColumn("A1");
            //RdtA.Columns.Add(RcolA);
            //RcolA = new DataColumn("A2");
            //RdtA.Columns.Add(RcolA);


            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][0].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
                //  strvariRes = strlengharrRes[2];          

                if (xTimechk == strvariRes)
                {

                }
                else
                {
                    RdrA = RdtA.NewRow();
                    RdrA[0] = Convert.ToDecimal(strvariRes);
                    RdrA[1] = Loadsampleno;
                    RdrA[2] = "1-" + Loadsampleno;
                    RdrA[3] = "0-0";
                    RdtA.Rows.Add(RdrA);
                    xTimechk = strvariRes;
                }
            }

            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][1].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
                //  strvariRes = strlengharrRes[2];          

                if (xTimechk == strvariRes)
                {

                }
                else
                {
                    RdrA = RdtA.NewRow();
                    RdrA[0] = Convert.ToDecimal(strvariRes);
                    RdrA[1] = Loadsampleno;
                    RdrA[2] = "0-0";
                    RdrA[3] = "1-" + Loadsampleno;
                    RdtA.Rows.Add(RdrA);
                    xTimechk = strvariRes;
                }


            }

            //Load SameTime SampleNo Start
            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][0].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];


                RSametime = Sametime.NewRow();
                RSametime[0] = Convert.ToDecimal(strvariRes);
                RSametime[1] = Loadsampleno;
                //RSametime[2] = CumLoadsampleno;
                Sametime.Rows.Add(RSametime);
                xTimechk = strvariRes;
            }




            for (int i = 0; i < Sametime.Rows.Count; i++)
            {

                strvari = Sametime.Rows[i][0].ToString().Trim();
                CumLoadsampleno = string.Empty;
                DataRow[] result = Sametime.Select("Wtime=" + strvari);
                int coma = 0;
                foreach (DataRow DR1 in result)
                {
                    if (coma == 0)
                    {
                        Loadsampleno = DR1["Wsampleno"].ToString().Trim();
                        CumLoadsampleno = Loadsampleno;
                        coma++;
                    }
                    else
                    {
                        Loadsampleno = DR1["Wsampleno"].ToString().Trim();
                        CumLoadsampleno = CumLoadsampleno + ',' + Loadsampleno;
                    }
                }
                Sametime.Rows[i][2] = CumLoadsampleno;
            }

            CumLoadsampleno = string.Empty;
            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][1].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];

                Sametime.Rows[i][3] = Convert.ToDecimal(strvariRes);
                Sametime.Rows[i][4] = CumLoadsampleno;


            }

            for (int i = 0; i < Sametime.Rows.Count; i++)
            {

                strvari = Sametime.Rows[i][3].ToString().Trim();
                CumLoadsampleno = string.Empty;
                DataRow[] result = Sametime.Select("Atime=" + strvari);
                int coma = 0;
                foreach (DataRow DR1 in result)
                {
                    if (coma == 0)
                    {
                        Loadsampleno = DR1["wsampleno"].ToString().Trim();
                        CumLoadsampleno = Loadsampleno;
                        coma++;
                    }
                    else
                    {
                        Loadsampleno = DR1["wsampleno"].ToString().Trim();
                        CumLoadsampleno = CumLoadsampleno + ',' + Loadsampleno;
                    }
                }
                Sametime.Rows[i][4] = CumLoadsampleno;
            }


            //Result Weight Table Start
            DataView view = new DataView(Sametime);
            DataTable distinctValues = new DataTable();
            distinctValues = view.ToTable(true, "Wtime");


            DataTable ResultWeight = new DataTable();
            DataRow ResultRow = null;
            ResultWeight.Columns.Add("Wtime", typeof(decimal));
            ResultWeight.Columns.Add("WSampleNo", typeof(string));
            strvari = string.Empty;
            for (int j = 0; j < distinctValues.Rows.Count; j++)
            {
                ResultRow = ResultWeight.NewRow();
                strvari = distinctValues.Rows[j][0].ToString().Trim();
                int coma = 0;
                for (int i = 0; i < Sametime.Rows.Count; i++)
                {

                    DataRow[] result = Sametime.Select("Wtime=" + strvari);

                    foreach (DataRow DR1 in result)
                    {
                        if (coma == 0)
                        {
                            ResultRow[0] = DR1[0];
                            ResultRow[1] = DR1[2];
                            ResultWeight.Rows.Add(ResultRow);
                            coma++;
                        }
                        else
                        {

                        }
                    }
                }
            }
            grdweight.DataSource = ResultWeight;
            grdweight.DataBind();
            //Result Weight Table END


            //Result Analyzer Table Start
            DataView Aview = new DataView(Sametime);
            DataTable AdistinctValues = new DataTable();
            AdistinctValues = view.ToTable(true, "Atime");


            DataTable ResultAnalyzer = new DataTable();
            DataRow ResultAnaRow = null;
            ResultAnalyzer.Columns.Add("Atime", typeof(decimal));
            ResultAnalyzer.Columns.Add("ASampleNo", typeof(string));
            strvari = string.Empty;

            for (int j = 0; j < AdistinctValues.Rows.Count; j++)
            {
                ResultAnaRow = ResultAnalyzer.NewRow();
                strvari = AdistinctValues.Rows[j][0].ToString().Trim();
                int coma = 0;
                for (int i = 0; i < Sametime.Rows.Count; i++)
                {

                    DataRow[] result = Sametime.Select("Atime=" + strvari);

                    foreach (DataRow DR1 in result)
                    {
                        if (coma == 0)
                        {
                            ResultAnaRow[0] = DR1[3];
                            ResultAnaRow[1] = DR1[4];
                            ResultAnalyzer.Rows.Add(ResultAnaRow);
                            coma++;
                        }
                        else
                        {

                        }
                    }
                }
            }
            //Result Analyzer Table END
            grdAnalyzer.DataSource = ResultAnalyzer;
            grdAnalyzer.DataBind();

            //Load SameTime SampleNo End


            //Sort Table Value
            //RdtA.DefaultView.Sort = "Wtime ASC ";
            //RdtA = RdtA.DefaultView.ToTable(true);

            // Order by using Storeprocedure
            DataTable custOrderDT = new DataTable();
            custOrderDT = RdtA;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "custOrderDT";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = custOrderDT;
            param.Direction = ParameterDirection.Input;
            SqlConnection conn = null;
            string constr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (conn = new SqlConnection(constr))
            {
                SqlCommand sqlCmd = new SqlCommand("[dbo].[LineChart_OrderBy]");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);

                DataTable Sptable = new DataTable();
                adp.Fill(Sptable);
                RdtA = Sptable;

            }
            //


            asize = RdtA.Rows.Count;
            //Resize Array Value
            Array.Resize(ref x, asize);
            for (int i = 0; i < RdtA.Rows.Count; i++)
            {
                string v = RdtA.Rows[i][0].ToString().Trim();
                //Load X axis Value
                x[i] = v;
            }


            //


            //load A1 Series
            asize = RdtA.Rows.Count;
            //Resize Array Value
            Array.Resize(ref y, asize);

            for (int i = 0; i < RdtA.Rows.Count; i++)
            {
                temp = RdtA.Rows[i][2].ToString().Trim();
                valarr = temp;
                tempvalarr = valarr.Split('-');
                Resvalarr0 = Convert.ToInt32(tempvalarr[0]);
                Resvalarr1 = Convert.ToInt32(tempvalarr[1]);

                if (Resvalarr0 == 0)
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y[i] = "0";
                }
                else
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y[i] = Resvalarr1.ToString();
                }
            }

            //Remove array elemet
            Array.Resize(ref dArray, asize);


            for (int i = 0; i < y.Length; i++)
            {
                //if (y[i].ToString().Trim().Length == 0)
                //{
                //}
                //else
                //{


                //}
                dArray[i] = Convert.ToDecimal(y[i]);
            }



           // LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = "Weigher", Data = dArray });           
           // LineChart1.CategoriesAxis = string.Join(",", x);



            //load A2 Series

            asize = RdtA.Rows.Count;
            // y = new decimal[asize];
            y = new string[asize];
            Array.Resize(ref y1, asize);


            for (int i = 0; i < RdtA.Rows.Count; i++)
            {

                temp = RdtA.Rows[i][3].ToString().Trim();
                valarr = temp;
                tempvalarr = valarr.Split('-');
                Resvalarr0 = Convert.ToInt32(tempvalarr[0]);
                Resvalarr1 = Convert.ToInt32(tempvalarr[1]);

                if (Resvalarr0 == 0)
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y1[i] = 0;
                }
                else
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y1[i] = Resvalarr1;
                }
            }

          //  LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = "Analyzer", Data = y1 });


            //LineChart1.ChartTitle = string.Format("{0} and {1} Order Distribution", ddlCountry1.SelectedItem.Value, ddlCountry2.SelectedItem.Value);


            if (x.Length > 3)
            {
               // LineChart1.ChartWidth = (x.Length * 33).ToString();
                //  LineChart1.ChartHeight = "700px";
                //LineChart1.ChartHeight = (y.Length * 10).ToString();
            }
          //  LineChart1.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }



    protected void grdLivepro_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}


//  Grand Total Fields

//<tr>
//                 <td width="30%" align="center">
//                 <span style="font-size: 18px; font-weight: bold; color: Brown">Total/Avg</span>
//                </td>
//                <td width="30%" align="center">
//                <%--<span style="font-size: 18px; font-weight: bold; color: #0252aa;"> <asp:Label ID="TMilk" runat="server"  text="TotalMilk:"></asp:Label></span>--%>
//                <span  style="font-size: 26px; font-weight: bold; color: #0252aa; "><asp:Label ID="totalMilk" runat="server" ></asp:Label></span>
//                </td>
//               <td width="30%" align="center">
//                <%--<span style="font-size: 18px; font-weight: bold; color: #0252aa;"><asp:Label ID="Fat" runat="server"  text="Afat:"></asp:Label></span>--%>
//                <span style="font-size: 26px; font-weight: bold; color: #0252aa;"><asp:Label ID="Afat" runat="server"></asp:Label></span>
//                </td>
//               <td width="30%" align="center">
//                <%--<span style="font-size: 18px; font-weight: bold; color: #0252aa;"><asp:Label ID="Snf" runat="server"  text="Asnf:"></asp:Label></span>--%>
//                <span style="font-size: 26px; font-weight: bold; color: #0252aa;"><asp:Label ID="Asnf" runat="server"></asp:Label></span>
//                </td>
//                </tr>