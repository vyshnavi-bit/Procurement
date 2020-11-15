using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
using System.Windows.Forms;
public partial class PlantDataDashBoard : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    msg getclass = new msg();
    SqlConnection con = new SqlConnection();
    DbHelper db = new DbHelper();
    DataTable AGENTLOANCOUNT = new DataTable();
    string[] SINGLEAGENT;
    string PARTICUAGENT;
    string GETLOANID;
    int ii = 0;
    int jj = 6;
    int i = 0;
    int j = 0;
    string[] GETAGENTIDWITHNAME;
    string[] GETAGENTCODE;
    string ADNET;
    int billdays;
    int Route;
    string AGENTID;
    int updateStatus;
    int GETREF;
    string getid;
    DataSet dsloan = new DataSet();

    DataTable collectdatble = new DataTable();
    DataTable dtplant = new DataTable();
    DataTable procurement = new DataTable();
    DataTable closingStock = new DataTable();
    DataTable route = new DataTable();
    DataTable cashcoll = new DataTable();
    DataTable garbertest = new DataTable();
    DataTable LiveData = new DataTable();
    DataTable cmpruntime = new DataTable();
    DataTable milkrechilling = new DataTable();
    DataTable GensetPowerDiesel = new DataTable();
    string sessions;
    String Monthdate;
    string ptcode;
    string ptname;
    string procuimp;
    string stockClosing;
    string timemaintanance;
    string Datime12hours1;
    string Datime24hours1;
    string Datime12hours2;
    string Datime24hours2;
    string cash;
    string Garcbertesting;
    string Live;
    string compress;
    string milkrechil;
    string Genset;
    int k = 0;
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
                    roleid = Convert.ToInt32(Session["Role"].ToString());

                    sessions = dtm.ToString("tt");
                    Monthdate = dtm.ToString("MM/dd/yyyy");
                 
                    
                    getgrid();
                    Datime12hours1 = dtm.AddDays(-1).ToString("MM/dd/yyyy");
                    Datime24hours1 = Datime12hours1 + " " + "23:59:59";
                    Datime12hours2 = dtm.AddDays(-2).ToString("MM/dd/yyyy");
                    Datime24hours2 = Datime12hours2 + " " + "23:59:59";
                    string GETDAA;
                    if (sessions == "AM")
                    {
                        GETDAA = "Date:" + Datime12hours2;

                    }
                    else
                    {
                        GETDAA = "Date:" + Datime12hours1 ;
                    }


                    Label1.Text = "Access Denied Please Call Admin People" + GETDAA;
                }
                else
                {




                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                //plant = ddl_Plantname.Text;
                //GET = plant.Split('_');


            }
             
        }

        catch
        {



        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }



    public void getgrid()
    {

        plantname();


        //collectdatble.Columns.Add("Plantcode");
        //collectdatble.Columns.Add("PlantName");
        //collectdatble.Columns.Add("procureData");
        //collectdatble.Columns.Add("ClosingStock");
        //collectdatble.Columns.Add("RouteTime");
        //collectdatble.Columns.Add("CashCollection");
        //collectdatble.Rows.Clear();
        //collectdatble.Columns.Clear();
        collectdatble.Columns.Add("Plant");
        collectdatble.Columns.Add("Details");
        //collectdatble.Rows.Add("RouteTime","SAP");
        //collectdatble.Rows.Add("CashCollection","SAP");
        //collectdatble.Rows.Add("GarberTesting","SAP");
        //collectdatble.Rows.Add("LiveData","");


        foreach (DataRow pro in dtplant.Rows)
        {

            ptcode = pro[0].ToString();
            ptname = pro[1].ToString();

            //GridView1.Rows[0].Cells[1].Text = ptcode;

            procurementimport();
            if (procurement.Rows.Count > 0)
            {
                procuimp = "Received";

                //  GridView1.Rows[2].Cells[1].Text = procuimp;
            }
            else
            {
                procuimp = "Pending";
            }

                ClosingStock();

                if (closingStock.Rows.Count > 0)
                {
                    stockClosing = "Received";

                }
                else
                {
                    stockClosing = "Pending";

                }
                RouteTimeMaintanance();
                if (route.Rows.Count > 0)
                {
                    timemaintanance = "Received";

                }
                else
                {
                    timemaintanance = "Pending";

                }
                cashcollections();
                if (cashcoll.Rows.Count > 0)
                {
                    cash = "Received";

                }
                else
                {
                    cash = "Pending";

                }
                garbertes();
                if (garbertest.Rows.Count > 0)
                {
                    Garcbertesting = "Received";

                }
                else
                {
                    Garcbertesting = "Pending";

                }


                comprresss();
                if (cmpruntime.Rows.Count > 0)
                {
                    compress = "Received";

                }
                else
                {
                    compress = "Pending";

                }
                Rechilll();
                if (milkrechilling.Rows.Count > 0)
                {
                    milkrechil = "Received";

                }
                else
                {
                    milkrechil = "Pending";

                }

                genseee();
                if (GensetPowerDiesel.Rows.Count > 0)
                {
                    Genset = "Received";

                }
                else
                {
                    Genset = "Pending";

                }

        }
        collectdatble.Rows.Add("Plantcode", ptcode);
        collectdatble.Rows.Add("PlantName", ptname);
        collectdatble.Rows.Add("procureData", procuimp);
        collectdatble.Rows.Add("ClosingStock", stockClosing);
        collectdatble.Rows.Add("RouteTime", timemaintanance);
        collectdatble.Rows.Add("CashCollection", cash);
        collectdatble.Rows.Add("GarberTesting", Garcbertesting);
        collectdatble.Rows.Add("compressor", compress);
        collectdatble.Rows.Add("Rechilling", milkrechil);
        collectdatble.Rows.Add("GensetPowerDiesel", Genset);
        GridView1.DataSource = collectdatble;
        GridView1.DataBind();


    }
    public void plantname()
    {
        try
        {
            con = db.GetConnection();
            string loanamt = "Select  Plant_Code as Plantcode,Plant_Name  as PlantName  from Plant_Master where Plant_Code='" + pcode + "' and Plant_Code not in (160)  order by Plant_Code,plant_name";
            SqlCommand sc = new SqlCommand(loanamt, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            dtplant.Rows.Clear();
            da1.Fill(dtplant);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

    public void procurementimport()
    {
        try
        {
            con = db.GetConnection();
            string procu;
            if (sessions == "AM")
            {
                procu = "SELECT * FROM (SELECT COUNT(*) as ss FROM (Select Plant_Code,Sessions  from  Procurementimport  where Plant_Code = '" + ptcode + "'    and  Prdate= DATEADD(day,-2,'" + Monthdate + "')  group by Plant_Code,Sessions )as ff  ) AS DD  WHERE ss > 1";
            }
            else
            {
                procu = "SELECT * FROM (SELECT COUNT(*) as ss FROM (Select Plant_Code,Sessions  from  Procurementimport  where  Plant_Code = '" + ptcode + "'    and  Prdate= DATEADD(day,-1,'" + Monthdate + "')  group by Plant_Code,Sessions )as ff  ) AS DD  WHERE ss > 1";

            }
            SqlCommand sc = new SqlCommand(procu, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            procurement.Rows.Clear();
            da1.Fill(procurement);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

    public void ClosingStock()
    {
        try
        {
            con = db.GetConnection();
            string procu;
            if (sessions == "AM")
            {
                procu = "SELECT  Date,Sum(MilkKg) as agg from  Stock_Milk  where   Plant_Code='" + ptcode + "'  and  Date = DATEADD(day, -2,'" + Monthdate + "') group by  Date";
            }
            else
            {
                procu = "SELECT  Date,Sum(MilkKg) as agg from  Stock_Milk  where  Plant_Code='" + ptcode + "'  and  Date = DATEADD(day, -1,'" + Monthdate + "') group by  Date";

            }
            SqlCommand sc = new SqlCommand(procu, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            closingStock.Rows.Clear();
            da1.Fill(closingStock);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }
    public void RouteTimeMaintanance()
    {
        try
        {
            con = db.GetConnection();
            string procu;
            if (sessions == "AM")
            {

                procu = "sELECT Date  FROM RouteTimeMaintain WHERE Plant_code='" + ptcode + "'   and  Date = DATEADD(day, -2,'" + Monthdate + "') group by  Date";
            }
            else
            {
                procu = "sELECT Date  FROM RouteTimeMaintain WHERE Plant_code='" + ptcode + "'   and  Date = DATEADD(day, -1,'" + Monthdate + "') group by  Date";

            }
            SqlCommand sc = new SqlCommand(procu, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            route.Rows.Clear();
            da1.Fill(route);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

    public void cashcollections()
    {
        try
        {
            Datime12hours1 = dtm.AddDays(-1).ToString("MM/dd/yyyy");
            Datime24hours1 = Datime12hours1 + " " + "23:59:59";
            Datime12hours2 = dtm.AddDays(-2).ToString("MM/dd/yyyy");
            Datime24hours2 = Datime12hours2 + " " + "23:59:59";
            string Cash;
            if (sessions == "AM")
            {

                Cash = "SELECT *  FROM (select  COUNT(*) AS DD  from collections    where  Branchid='" + ptcode + "' and   PaidDate BETWEEN '" + Datime12hours2 + "' and '" + Datime24hours2 + "' ) AS GG  WHERE DD  > 0 ";
            }
            else
            {
                Cash = "SELECT *  FROM (select  COUNT(*) AS DD from collections     where  Branchid='" + ptcode + "' and  PaidDate BETWEEN '" + Datime12hours1 + "' and '" + Datime24hours1 + "'  ) AS GG  WHERE DD  > 0 ";

            }
            SqlCommand sc = new SqlCommand(Cash, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            cashcoll.Rows.Clear();
            da1.Fill(cashcoll);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }
    public void garbertes()
    {
        try
        {
            con = db.GetConnection();
            string Cash;
            if (sessions == "AM")
            {

                Cash = "select *  from (SELECT COUNT(*) as new FROM  Garbartest  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as sam where new > 1";
            }
            else
            {
                Cash = "select *  from (SELECT COUNT(*) as new FROM  Garbartest  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -1,'" + Monthdate + "')) as sam where new > 1";

            }
            SqlCommand sc = new SqlCommand(Cash, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            garbertest.Rows.Clear();
            da1.Fill(garbertest);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

  

    public void comprresss()
    {
        try
        {
            con = db.GetConnection();
            string str;
            if (sessions == "AM")
            {
                str = " Select *   from (  select  COUNT(*) as sno   from  cmpruntime     WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as gg where sno > 1";

               
            }
            else
            {
                str = " Select *   from (  select  COUNT(*) as sno   from  cmpruntime     WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as gg where sno > 1";

            }
            SqlCommand sc = new SqlCommand(str, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            cmpruntime.Rows.Clear();
            da1.Fill(cmpruntime);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

    public void Rechilll()
    {
        try
        {
            con = db.GetConnection();
            string Cash;
            if (sessions == "AM")
            {

                Cash = "select *  from (SELECT COUNT(*) as new FROM  milkrechilling  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as sam where new > 1";
            }
            else
            {
                Cash = "select *  from (SELECT COUNT(*) as new FROM  milkrechilling  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -1,'" + Monthdate + "')) as sam where new > 1";

            }
            SqlCommand sc = new SqlCommand(Cash, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            milkrechilling.Rows.Clear();
            da1.Fill(milkrechilling);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

    public void genseee()
    {
        try
        {
            con = db.GetConnection();
            string Cash;
            if (sessions == "AM")
            {

                Cash = "select *  from (SELECT COUNT(*) as new FROM  GensetPowerDiesel  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as sam where new > 0";
            }
            else
            {
                Cash = "select *  from (SELECT COUNT(*) as new FROM  GensetPowerDiesel  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -1,'" + Monthdate + "')) as sam where new > 1";

            }
            SqlCommand sc = new SqlCommand(Cash, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            GensetPowerDiesel.Rows.Clear();
            da1.Fill(GensetPowerDiesel);

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
}