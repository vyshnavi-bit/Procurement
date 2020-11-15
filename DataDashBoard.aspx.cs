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
public partial class DataDashBoard : System.Web.UI.Page
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
    DataSet COLLECT = new DataSet();
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
    int GRIDCOL;
    string compress;
    string milkrechil;
    string Genset;
    int datasetcount = 0;
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
                    if((roleid>=3) && (roleid!=9))
                    {
                    getgrid();
                    }
                    if (roleid == 9)
                    {
                     getgrid1();
                    }
                    Datime12hours1 = dtm.AddDays(-1).ToString("MM/dd/yyyy");
                    Datime24hours1 = Datime12hours1 + " " + "23:59:59";
                    Datime12hours2 = dtm.AddDays(-2).ToString("MM/dd/yyyy");
                    Datime24hours2 = Datime12hours2 + " " + "23:59:59";

                    string getDateSessions;

                    if (sessions == "AM")
                    {
                        getDateSessions=Datime12hours2.ToString();

                    }
                    else
                    {
                          getDateSessions=Datime12hours1.ToString();
                    }
                    Label5.Text = getDateSessions.ToString();
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


    public void getgrid()
    {


        

        plantname();
        //collectdatble.Rows.Clear();
        //collectdatble.Columns.Clear();
        collectdatble.Columns.Add("Plantcode");
        collectdatble.Columns.Add("PlantName");
        collectdatble.Columns.Add("procureData");
        collectdatble.Columns.Add("ClosingStock");
        collectdatble.Columns.Add("RouteTime");
        collectdatble.Columns.Add("CashCollection");
        collectdatble.Columns.Add("GarberTesting");
        collectdatble.Columns.Add("compressor");
        collectdatble.Columns.Add("Rechilling" );
        collectdatble.Columns.Add("GensetPower");
      


        foreach (DataRow pro in dtplant.Rows)
        {
            
            ptcode = pro[0].ToString();
            ptname = pro[1].ToString();
            procurementimport();
            if (COLLECT.Tables[0].Rows.Count > 0)
            {
                procuimp = "Received";


            }
            else
            {
                procuimp = "Pending";

            }

            ClosingStock();


            if (COLLECT.Tables[1].Rows.Count > 0)
            {
                stockClosing = "Received";


            }
            else
            {
                stockClosing = "Pending";

            }
            RouteTimeMaintanance();
            if (COLLECT.Tables[2].Rows.Count > 0)
            {
                timemaintanance = "Received";


            }
            else
            {
                timemaintanance = "Pending";

            }
            cashcollections();
            if (COLLECT.Tables[3].Rows.Count > 0)
            {
                cash = "Received";

            }
            else
            {
                cash = "Pending";

            }

            garbertes();
            if (COLLECT.Tables[4].Rows.Count > 0)
            {
                Garcbertesting = "Received";

            }
            else
            {
                Garcbertesting = "Pending";

            }
            //living();
            //if (LiveData.Rows.Count > 0)
            //{
            //    Live = "Received";

            //}
            //else
            //{
            //    Live = "Pending";

            //}

            comprresss();
            if (COLLECT.Tables[5].Rows.Count > 0)
            {
                compress = "Received";

            }
            else
            {
                compress = "Pending";

            }

            Rechilll();
            if (COLLECT.Tables[6].Rows.Count > 0)
            {
                milkrechil = "Received";

            }
            else
            {
                milkrechil = "Pending";

            }

            genseee();
            if (COLLECT.Tables[7].Rows.Count > 0)
            {
                Genset = "Received";

            }
            else
            {
                Genset = "Pending";

            }


            collectdatble.Rows.Add(ptcode, ptname, procuimp, stockClosing, timemaintanance, cash, Garcbertesting, compress, milkrechil, Genset);
            COLLECT.Tables[0].Rows.Clear();
            COLLECT.Tables[1].Rows.Clear();
            COLLECT.Tables[2].Rows.Clear();
            COLLECT.Tables[3].Rows.Clear();
            COLLECT.Tables[4].Rows.Clear();
            COLLECT.Tables[5].Rows.Clear();
            COLLECT.Tables[6].Rows.Clear();
            COLLECT.Tables[7].Rows.Clear();
        }
         GridView1.DataSource = collectdatble;
         GridView1.DataBind();
         GRIDCOL = GridView1.Columns.Count;
            //if cell is not red make it red

        //foreach (RepeaterItem item2 in DataList1.Items)
        //    {
        //        //if (Convert.ToInt32(item2["RouteTime"].ToString()) > 1)
        //        //{
        //        //    System.Web.UI.WebControls.Label lbl = (System.Web.UI.WebControls.Label)item2.FindControl("lbl1");
        //        //    lbl.Attributes.Add("style", "background-color:Green;");
        //        //}
        //        Label lbl = (Label)item2.FindControl("Label3");
        //        lbl.Attributes.Add("style", "background-color:Green;");
        //    }
         

        
    }

    public void getgrid1()
    {




        plantname1();
        //collectdatble.Rows.Clear();
        //collectdatble.Columns.Clear();
        collectdatble.Columns.Add("Plantcode");
        collectdatble.Columns.Add("PlantName");
        collectdatble.Columns.Add("procureData");
        collectdatble.Columns.Add("ClosingStock");
        collectdatble.Columns.Add("RouteTime");
        collectdatble.Columns.Add("CashCollection");
        collectdatble.Columns.Add("GarberTesting");
        collectdatble.Columns.Add("compressor");
        collectdatble.Columns.Add("Rechilling");
        collectdatble.Columns.Add("GensetPower");



        foreach (DataRow pro in dtplant.Rows)
        {

            ptcode = pro[0].ToString();
            ptname = pro[1].ToString();
            procurementimport();
            if (COLLECT.Tables[0].Rows.Count > 0)
            {
                procuimp = "Received";


            }
            else
            {
                procuimp = "Pending";

            }

            ClosingStock();


            if (COLLECT.Tables[1].Rows.Count > 0)
            {
                stockClosing = "Received";


            }
            else
            {
                stockClosing = "Pending";

            }
            RouteTimeMaintanance();
            if (COLLECT.Tables[2].Rows.Count > 0)
            {
                timemaintanance = "Received";


            }
            else
            {
                timemaintanance = "Pending";

            }
            cashcollections();
            if (COLLECT.Tables[3].Rows.Count > 0)
            {
                cash = "Received";

            }
            else
            {
                cash = "Pending";

            }

            garbertes();
            if (COLLECT.Tables[4].Rows.Count > 0)
            {
                Garcbertesting = "Received";

            }
            else
            {
                Garcbertesting = "Pending";

            }
            //living();
            //if (LiveData.Rows.Count > 0)
            //{
            //    Live = "Received";

            //}
            //else
            //{
            //    Live = "Pending";

            //}

            comprresss();
            if (COLLECT.Tables[5].Rows.Count > 0)
            {
                compress = "Received";

            }
            else
            {
                compress = "Pending";

            }

            Rechilll();
            if (COLLECT.Tables[6].Rows.Count > 0)
            {
                milkrechil = "Received";

            }
            else
            {
                milkrechil = "Pending";

            }

            genseee();
            if (COLLECT.Tables[7].Rows.Count > 0)
            {
                Genset = "Received";

            }
            else
            {
                Genset = "Pending";

            }


            collectdatble.Rows.Add(ptcode, ptname, procuimp, stockClosing, timemaintanance, cash, Garcbertesting, compress, milkrechil, Genset);
            COLLECT.Tables[0].Rows.Clear();
            COLLECT.Tables[1].Rows.Clear();
            COLLECT.Tables[2].Rows.Clear();
            COLLECT.Tables[3].Rows.Clear();
            COLLECT.Tables[4].Rows.Clear();
            COLLECT.Tables[5].Rows.Clear();
            COLLECT.Tables[6].Rows.Clear();
            COLLECT.Tables[7].Rows.Clear();
        }
        GridView1.DataSource = collectdatble;
        GridView1.DataBind();
        GRIDCOL = GridView1.Columns.Count;
        //if cell is not red make it red

        //foreach (RepeaterItem item2 in DataList1.Items)
        //    {
        //        //if (Convert.ToInt32(item2["RouteTime"].ToString()) > 1)
        //        //{
        //        //    System.Web.UI.WebControls.Label lbl = (System.Web.UI.WebControls.Label)item2.FindControl("lbl1");
        //        //    lbl.Attributes.Add("style", "background-color:Green;");
        //        //}
        //        Label lbl = (Label)item2.FindControl("Label3");
        //        lbl.Attributes.Add("style", "background-color:Green;");
        //    }



    }

    public void plantname()
    {
        try
        {
            con = db.GetConnection();
            string loanamt = "Select  Plant_Code as Plantcode,Plant_Name  as PlantName,milktype  from Plant_Master where Plant_Code > 154 and Plant_Code not in (160)  order by milktype,Plant_Code ";
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

    public void plantname1()
    {
        try
        {
            con = db.GetConnection();
            string loanamt = "Select  Plant_Code as Plantcode,Plant_Name  as PlantName,milktype  from Plant_Master where Plant_Code > 154 and Plant_Code  in (170) order by milktype,Plant_Code ";
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

    public void plantnameKPM()
    {
        try
        {
            con = db.GetConnection();
            string loanamt = "Select  Plant_Code as Plantcode,Plant_Name  as PlantName,milktype  from Plant_Master where Plant_Code > 154 and Plant_Code not in (160)  order by milktype,Plant_Code ";
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
            SqlCommand sc1 = new SqlCommand(procu, con);
            SqlDataAdapter da01 = new SqlDataAdapter(sc1);
            //procurement.Rows.Clear();
            //da1.Fill(procurement);
            da01.Fill(COLLECT, "procur");
            datasetcount = datasetcount + 1;

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
          
            string procu;
            if (sessions == "AM")
            {
                procu = "SELECT  Date,Sum(MilkKg) as agg from  Stock_Milk  where   Plant_Code='" + ptcode + "'  and  Date = DATEADD(day, -2,'" + Monthdate + "') group by  Date";
            }
            else
            {
                procu = "SELECT  Date,Sum(MilkKg) as agg from  Stock_Milk  where  Plant_Code='" + ptcode + "'  and  Date = DATEADD(day, -1,'" + Monthdate + "') group by  Date";

            }
            SqlCommand sc2 = new SqlCommand(procu, con);
            SqlDataAdapter da021 = new SqlDataAdapter(sc2);
            //closingStock.Rows.Clear();
            //da1.Fill(closingStock);
            da021.Fill(COLLECT, "Clos");
            datasetcount = datasetcount + 1;

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
           
            string procu;
            if (sessions == "AM")
            {

                procu = "sELECT Date  FROM RouteTimeMaintain WHERE Plant_code='" + ptcode + "'   and  Date = DATEADD(day, -2,'" + Monthdate + "') group by  Date";
            }
            else
            {
                procu = "sELECT Date  FROM RouteTimeMaintain WHERE Plant_code='" + ptcode + "'   and  Date = DATEADD(day, -1,'" + Monthdate + "') group by  Date";

            }
            SqlCommand sc3 = new SqlCommand(procu, con);
            SqlDataAdapter da031 = new SqlDataAdapter(sc3);
            //route.Rows.Clear();
            //da1.Fill(route);
            da031.Fill(COLLECT, "Route");
            datasetcount = datasetcount + 1;

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
                Cash = "SELECT *  FROM (select  COUNT(*) AS DD  from collections    where  Branchid='" + ptcode + "' and  PaidDate BETWEEN '" + Datime12hours1 + "' and '" + Datime24hours1 + "'  ) AS GG  WHERE DD  > 0 ";

            }
            SqlCommand sc4 = new SqlCommand(Cash, con);
            SqlDataAdapter da041 = new SqlDataAdapter(sc4);
            da041.Fill(COLLECT, "cash");
            datasetcount = datasetcount + 1;

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
           
            string Cash;
            if (sessions == "AM")
            {

                Cash = "select *  from (SELECT COUNT(*) as new FROM  Garbartest  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as sam where new > 1";
            }
            else
            {
                Cash = "select *  from (SELECT COUNT(*) as new FROM  Garbartest  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -1,'" + Monthdate + "')) as sam where new > 1";

            }
            SqlCommand sc5 = new SqlCommand(Cash, con);
            SqlDataAdapter da051 = new SqlDataAdapter(sc5);
            //garbertest.Rows.Clear();
            //da1.Fill(garbertest);
            da051.Fill(COLLECT, "gar");
            datasetcount = datasetcount + 1;

        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }
    //public void living()
    //{
    //    try
    //    {
    //        con = db.GetConnection();
    //        string lll;
    //        if (sessions == "AM")
    //        {

    //            lll = "select * from(select COUNT(*) as sno   from (SELECT prdate,Sessions FROM  Lp  WHERE PlantCode='" + ptcode + "' AND Prdate = DATEADD(day, -2,'" + Monthdate + "')  group by prdate,Sessions) as sam  ) as gg where sno >1";
    //        }
    //        else
    //        {
    //            lll = "select * from(select COUNT(*) as sno   from (SELECT prdate,Sessions FROM  Lp  WHERE PlantCode='" + ptcode + "' AND Prdate = DATEADD(day, -1,'" + Monthdate + "')  group by prdate,Sessions) as sam  ) as gg where sno >1";

    //        }
    //        SqlCommand sc = new SqlCommand(lll, con);
    //        SqlDataAdapter da1 = new SqlDataAdapter(sc);
    //        LiveData.Rows.Clear();
    //        da1.Fill(LiveData);

    //    }
    //    catch
    //    {

    //        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
    //    }

    //}

    public void comprresss()
    {
        try
        {
            
            string str;
            if (sessions == "AM")
            {
                str = " Select *   from (  select  COUNT(*) as sno   from  cmpruntime     WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as gg where sno > 1";


            }
            else
            {
                str = " Select *   from (  select  COUNT(*) as sno   from  cmpruntime     WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as gg where sno > 1";

            }
            SqlCommand sc6 = new SqlCommand(str, con);
            SqlDataAdapter da061 = new SqlDataAdapter(sc6);
            //cmpruntime.Rows.Clear();
            //da1.Fill(cmpruntime);
            da061.Fill(COLLECT, "com");
            datasetcount = datasetcount + 1;

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
           
            string Cash;
            if (sessions == "AM")
            {

                Cash = "select *  from (SELECT COUNT(*) as new FROM  milkrechilling  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as sam where new > 1";
            }
            else
            {
                Cash = "select *  from (SELECT COUNT(*) as new FROM  milkrechilling  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -1,'" + Monthdate + "')) as sam where new > 1";

            }
            SqlCommand sc7 = new SqlCommand(Cash, con);
            SqlDataAdapter da071 = new SqlDataAdapter(sc7);
            //milkrechilling.Rows.Clear();
            //da1.Fill(milkrechilling);
            da071.Fill(COLLECT, "Rech");
            datasetcount = datasetcount + 1;

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
            
            string Cash;
            if (sessions == "AM")
            {

                Cash = "select *  from (SELECT COUNT(*) as new FROM  GensetPowerDiesel  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -2,'" + Monthdate + "')) as sam where new > 0";
            }
            else
            {
                Cash = "select *  from (SELECT COUNT(*) as new FROM  GensetPowerDiesel  WHERE Plant_code='" + ptcode + "' AND Date = DATEADD(day, -1,'" + Monthdate + "')) as sam where new > 0";

            }
            SqlCommand sc8 = new SqlCommand(Cash, con);
            SqlDataAdapter da081 = new SqlDataAdapter(sc8);
            //GensetPowerDiesel.Rows.Clear();
            //da1.Fill(GensetPowerDiesel);
            da081.Fill(COLLECT, "gen");
          
        }
        catch
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
        }

    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            e.Item.Attributes.Add("onmouseover", "this.style.background-color='#ccsdcc'");
            e.Item.Attributes.Add("onmouseout", "this.style.background-color='#ffsfff'");

            System.Data.DataRowView drv = (DataRowView)(e.Item.DataItem);
            string PL = DataBinder.Eval(e.Item.DataItem, "Plantcode").ToString();
            string PRO = DataBinder.Eval(e.Item.DataItem, "procureData").ToString();
            string CL = DataBinder.Eval(e.Item.DataItem, "ClosingStock").ToString();
            string ROUT = DataBinder.Eval(e.Item.DataItem, "RouteTime").ToString();
            string CASKC = DataBinder.Eval(e.Item.DataItem, "CashCollection").ToString();
            string GAR = DataBinder.Eval(e.Item.DataItem, "GarberTesting").ToString();
            //string LD = DataBinder.Eval(e.Item.DataItem, "LiveData").ToString();

            //if ((PRO == "Pending") || (CL == "Pending") || (ROUT == "Pending") || (CASKC == "Pending") || (GAR == "Pending") || (LD == "Pending") )
            //{
            //    e.Item.ForeColor = System.Drawing.Color.Red;
            //    e.Item.BackColor = System.Drawing.Color.GreenYellow;
            //}
            //else
            //{
            //    e.Item.BackColor = System.Drawing.Color.HotPink;

            //}
            //            LinkLabel lblItem = e.Item.FindControl("Item") as LinkLabel

            //if(lblitem !=null)
            //{
            //// add properties to it 
            //lblItem.Attributes.Add("onclick", "this.style.background='#eeff00'");

            //}




            //if (PRO == "Pending")
            //{
            //    e.Item.BackColor = System.Drawing.Color.Red;

            //}
            //else
            //{
            //    e.Item.BackColor = System.Drawing.Color.GreenYellow;
            //}

            //if (CL == "Pending")
            //{
            //    e.Item.BackColor = System.Drawing.Color.Red;
            //}
            //else
            //{
            //    e.Item.BackColor = System.Drawing.Color.GreenYellow;
            //}

            //if (ROUT == "Pending")
            //{
            //  //  e.Item.BackColor = System.Drawing.Color.Red;
            //    e.Item.Attributes.Add("style", "background-color:Green;");
            //}
            //else
            //{
            //    e.Item.Attributes.Add("style", "background-color:Green;");
            //}

            //if (CASKC == "Pending")
            //{
            //    e.Item.BackColor = System.Drawing.Color.Red;
            //}
            //else
            //{
            //    e.Item.BackColor = System.Drawing.Color.GreenYellow;
            //}
            //if (GAR == "Pending")
            //{
            //    e.Item.BackColor = System.Drawing.Color.Red;
            //}
            //else
            //{
            //    e.Item.BackColor = System.Drawing.Color.GreenYellow;
            //}

            //if (LD == "Pending")
            //{
            //    e.Item.BackColor = System.Drawing.Color.Red;
            //}
            //else
            //{
            //    e.Item.BackColor = System.Drawing.Color.GreenYellow;
            //}
            //}

        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        Plantcode.Text = GridView1.SelectedRow.Cells[1].Text;
        plant1name.Text = GridView1.SelectedRow.Cells[2].Text;
        popup.Show();

    }
    //public void updatelock()
    //{
    //    con = .GetConnection();
    //    string update;
    //    update = "update Unlock set Lock='0'  where plant_code='" + tempplant + "'";
    //    SqlCommand cmd = new SqlCommand(update, con);
    //    cmd.ExecuteNonQuery();

    //}
    protected void Save_Click(object sender, EventArgs e)
    {
        try
        {
            con = db.GetConnection();
            string update;
            update = "update Unlock set Lock='" + DropDownList1.SelectedItem.Value+ "',UnlockUser='" + Session["Name"].ToString() + "'  where plant_code='" + Plantcode.Text + "'";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.ExecuteNonQuery();
        }
        catch
        {

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            //int get = GridView1.Columns.Count;

            if ((e.Row.Cells[1].Text == "155") || (e.Row.Cells[1].Text == "156") || (e.Row.Cells[1].Text == "158") || (e.Row.Cells[1].Text == "159") || (e.Row.Cells[1].Text == "161") || (e.Row.Cells[1].Text == "162") || (e.Row.Cells[1].Text == "163") || (e.Row.Cells[1].Text == "164") || ((e.Row.Cells[1].Text == "170")))
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;

            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.Aquamarine;
                

            }

            //if ((e.Row.Cells[3].Text == "Pending") || (e.Row.Cells[4].Text == "Pending") || (e.Row.Cells[5].Text == "Pending") || (e.Row.Cells[6].Text == "Pending") || (e.Row.Cells[7].Text == "Pending") || (e.Row.Cells[8].Text == "Pending") || (e.Row.Cells[9].Text == "Pending") || (e.Row.Cells[10].Text == "Pending"))
            //{
            //    e.Row.Cells[3].BackColor = System.Drawing.Color.DarkBlue;
            //    e.Row.Cells[4].BackColor = System.Drawing.Color.DarkBlue;
            //    e.Row.Cells[5].BackColor = System.Drawing.Color.DarkBlue;
            //    e.Row.Cells[6].BackColor = System.Drawing.Color.DarkBlue;
            //    e.Row.Cells[7].BackColor = System.Drawing.Color.DarkBlue;
            //    e.Row.Cells[8].BackColor = System.Drawing.Color.DarkBlue;
            //    e.Row.Cells[9].BackColor = System.Drawing.Color.DarkBlue;
            //    e.Row.Cells[10].BackColor = System.Drawing.Color.DarkBlue;

            //}
            //else
            
         
        }
    }
}