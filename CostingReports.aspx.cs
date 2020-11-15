using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class CostingReports : System.Web.UI.Page
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
    DataTable getplantlistbuff = new DataTable();
    string plantcode;
    DateTime dtf;
    DateTime minus;
    string datetimeconvertion;
    string daysminus;

    double opfatkg;
    double opsnfkg;
    double opmilkkg;
    double opmilkltr;

    double profatkg;
    double prosnfkg;
    double promilkkg;
    double promilkltr;
    double proAmount;

    double Dispfatkg;
    double Dispsnfkg;
    double Dispmilkkg;
    double Dispmilkltr;
    double transportAmt;
    double brate;

    double tempavgfat ;
    double tempavgsnf;
    double gettotmilkamt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                dtf = System.DateTime.Now;
                getallplantdetails();
           
               
               
            }
            else
            {
               
            }

        }

        catch
        {

        }
    }
    public void getallplantdetails()
    {
        string getdetatails;
        con = DB.GetConnection();
        getdetatails = "Select TOP 5    Plant_Code,Plant_Name,NULL AS milkkg,Null as MilkLtr,Null as Avgfat,Null as Avgsnf,Null as AvgClr,Null as TSTOTAL,Null as TS,Null as Milkvalue,Null as OH,Null as Transport,Null as TotalMilkvalue,Null as Avgrate,Null as  Tsrate,Null as perTs  from Plant_Master   where plant_code  in (155,156,158,159,161,162,163,164) ";
        SqlCommand cmd = new SqlCommand(getdetatails, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        getplantlistbuff.Rows.Clear();
        da.Fill(getplantlistbuff);

        if (getplantlistbuff.Rows.Count > 0)
        {
            GridView1.DataSource = getplantlistbuff;
            GridView1.DataBind();
          

        }

        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
          

        }

    }
    public void getopening()
    {
        try
        {
            datetimeconvertion = dtf.ToString("MM/dd/yyyy");
            string getdetatails;
            con = DB.GetConnection();
            getdetatails = " Select  isnull(sUM(Fat),0) FAT,isnull(sUM(Snf),0) AS Snf,isnull(Sum(MilkKg),0) as Milkkg,isnull(Convert(decimal(18,2),(Sum(MilkKg)/1.03)),0) as milkltr,isnull(Convert(decimal(18,2),(Sum(Fat_Kg)/1.03)),0) as FatKg,isnull(Convert(decimal(18,2),(Sum(Snf_Kg)/1.03)),0) as SnfKg FROM  Stock_openingmilk   where Plant_code='" + plantcode + "'   and datee='" + datetimeconvertion + "'";
            SqlCommand cmd = new SqlCommand(getdetatails, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable getop = new DataTable();
            getop.Rows.Clear();
            da.Fill(getop);

            opfatkg = Convert.ToDouble(getop.Rows[0][4]);
            opsnfkg = Convert.ToDouble(getop.Rows[0][5]);
            opmilkkg = Convert.ToDouble(getop.Rows[0][2]);
            opmilkltr = Convert.ToDouble(getop.Rows[0][3]);

        }
        catch
        {
            opfatkg = 0;
            opsnfkg = 0;
            opmilkkg = 0;
            opmilkltr = 0;

        }



        //string opfat = 
        //string opsnf = 
        //string opmilkkg = 
  
    }
    public void getprocurement()
    {
        try
        {
            string getdetatails;
            con = DB.GetConnection();
            getdetatails = "Select convert(decimal(18,2),(fatkg * 100) / Milkkg) as Avgfat,convert(decimal(18,2),(snfkg * 100) / Milkkg) as Avgsnf,Milkkg,Milkltr,fatkg,snfkg,TotAmt  from (Select  Convert(decimal(18,2),(isnull(Sum(Milk_kg),0)))  as Milkkg,Convert(decimal(18,2),(isnull(Sum((Milk_kg/1.03)),0)))  as Milkltr,Convert(decimal(18,2),(isnull(Sum(fat_kg),0)))  as fatkg,Convert(decimal(18,2),(isnull(Sum(snf_kg),0)))  as snfkg, Convert(decimal(18,2),(isnull(Sum(Amount+ ComRate + SplBonusAmount),0)))  as TotAmt    from Procurement   where Plant_code='" + plantcode + "'   and Prdate='" + datetimeconvertion + "') as proimport";
            SqlCommand cmd = new SqlCommand(getdetatails, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable getpro = new DataTable();
            getpro.Rows.Clear();
            da.Fill(getpro);

            promilkkg = Convert.ToDouble(getpro.Rows[0][2]);
            promilkltr = Convert.ToDouble(getpro.Rows[0][3]);
            profatkg = Convert.ToDouble(getpro.Rows[0][4]);
            prosnfkg = Convert.ToDouble(getpro.Rows[0][5]);
            proAmount = Convert.ToDouble(getpro.Rows[0][6]);
        }
        catch
        {
            promilkkg = 0;
            promilkltr = 0;
            profatkg = 0;
            prosnfkg = 0;
            proAmount = 0;


        }

    }
    public void Despatchnew()
    {
        try
        {
            string getdetatails;
            con = DB.GetConnection();
            getdetatails = "Select   *   from (Select  CONVERT(DECIMAL(18,2),(ISNULL(sUM(Fat),0))) AS Fat,convert(decimal(18,2),ISNULL(avg(Snf),0)) AS Snf,convert(decimal(18,2), ISNULL(avg(MilkKg),0)) AS MilkKg, convert(decimal(18,2), ISNULL(sUM((MilkKg/1.03)),0)) AS milkltr,isnull(Convert(decimal(18,2),(Sum(Fat_Kg)/1.03)),0) as FatKg,isnull(Convert(decimal(18,2),(Sum(Snf_Kg)/1.03)),0) as SnfKg        from Despatchnew   where Plant_code='" + plantcode + "'   and Date='" + datetimeconvertion + "') as news";
            SqlCommand cmd = new SqlCommand(getdetatails, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable getdis = new DataTable();
            getdis.Rows.Clear();
            da.Fill(getdis);

            Dispmilkkg = Convert.ToDouble(getdis.Rows[0][2]);
            Dispmilkltr = Convert.ToDouble(getdis.Rows[0][3]);
            Dispfatkg = Convert.ToDouble(getdis.Rows[0][4]);
            Dispsnfkg = Convert.ToDouble(getdis.Rows[0][5]);
        }
        catch
        {
            Dispmilkkg = 0;
            Dispmilkltr = 0;
            Dispfatkg = 0;
            Dispsnfkg = 0;

        }

    }
    public void transport()
    {
        try
        {
            string getdetatails;
            con = DB.GetConnection();
            getdetatails = "sELECT  ISNULL(CONVERT(DECIMAL(18,2),( ISNULL(sUM(ActualAmount/2),0))),0)  AS ActualAmount from Truck_Present    WHERE  Plant_code='" + plantcode + "' and   Pdate ='" + daysminus + "'";
            SqlCommand cmd = new SqlCommand(getdetatails, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable gettrans = new DataTable();
            gettrans.Rows.Clear();
            da.Fill(gettrans);
            transportAmt = Convert.ToDouble(gettrans.Rows[0][0]);

        }
        catch
        {


        }
    }

protected void  GridView1_RowCreated(object sender, GridViewRowEventArgs e)
{

    if (e.Row.RowType == DataControlRowType.Header)
    {
        GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

        TableCell HeaderCell2 = new TableCell();
        HeaderCell2.Text = "CC  Costing Rate Calculation:";
        HeaderCell2.ColumnSpan = 17;
        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        HeaderRow.Cells.Add(HeaderCell2);
        GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

    }
}
protected void  GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
{
    DateTime dateForButton = DateTime.Now;
    dateForButton = dateForButton.AddDays(-1);  // ERROR: un-representable DateTime
    minus = dtf.AddDays(-1);
    daysminus = minus.ToString("MM/dd/yyyy");
    if (e.Row.RowType == DataControlRowType.DataRow)
    {
         plantcode = e.Row.Cells[1].Text;
         getopening();
         getprocurement();
         Despatchnew();
         
        transport();

         string kg = ((opmilkkg + promilkkg) - Dispmilkkg).ToString("f2");
         e.Row.Cells[3].Text = kg;
         double tempkg = Convert.ToDouble(kg);
         string ltr = ((opmilkltr + promilkltr) - Dispmilkltr).ToString("f2");
         e.Row.Cells[4].Text = ltr;
         double templtr =Convert.ToDouble(ltr);
         double tempfatkg = (( opfatkg +  profatkg ) - Dispfatkg);
         double tempsnfkg = ((opsnfkg + prosnfkg) - Dispsnfkg);
         string avgfat = ((tempfatkg * 100) / tempkg).ToString("f2");
         string avgsnf = ((tempsnfkg * 100) / tempkg).ToString("f2");

         e.Row.Cells[5].Text = avgfat;
         e.Row.Cells[6].Text = avgsnf;
         double tempavgfat = Convert.ToDouble(avgfat);
         double tempavgsnf = Convert.ToDouble(avgsnf);
         string clr = (((tempavgsnf - 0.36) - (tempavgfat * 0.21)) * 4).ToString("f2");
         e.Row.Cells[7].Text = clr;
         e.Row.Cells[8].Text = (tempavgfat + tempavgsnf).ToString();
         double milkvalue = proAmount;
          brate = milkvalue / promilkkg;
         e.Row.Cells[9].Text = ((brate / (tempavgfat + tempavgsnf)) * 100 ).ToString("F2");
         e.Row.Cells[9].ForeColor = System.Drawing.Color.BlueViolet;
         double tempts = (brate / (tempavgfat + tempavgsnf));
         e.Row.Cells[10].Text = (brate * templtr).ToString("F2");
         double tempvalue = (brate * templtr);
         e.Row.Cells[11].Text = (tempkg * 1).ToString("F2");
         e.Row.Cells[12].Text = transportAmt.ToString("f2");
         e.Row.Cells[13].Text =( (tempvalue + transportAmt + (tempkg * 1)).ToString("f2"));  
         gettotmilkamt =  Convert.ToDouble(e.Row.Cells[13].Text);
         e.Row.Cells[14].Text = (gettotmilkamt / templtr).ToString("f2");
         double rate = (gettotmilkamt / templtr);
         e.Row.Cells[15].Text = ((rate / (tempavgfat + tempavgsnf)) * 100).ToString("f2");
         e.Row.Cells[15].ForeColor = System.Drawing.Color.Brown;
         e.Row.Cells[16].Text = ((rate / (tempavgfat + tempavgsnf))).ToString("f2");
    }
}
protected void  GridView2_RowCreated(object sender, GridViewRowEventArgs e)
{

}
protected void  GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
{

}
}