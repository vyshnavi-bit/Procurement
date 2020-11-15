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
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;
using System.Web.Services;
using System.Collections.Generic;
public partial class ChartRanges : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    SqlDataReader dr;
    int datasetcount = 0;
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataTable fatkgdt = new DataTable();
    DataTable showagent = new DataTable();
    DataTable milkkg = new DataTable();
    DataTable milkkgts = new DataTable();
    DataTable nilrate = new DataTable();
    DateTime d1;
    DateTime d2;
    string agentcode;
    int countinsertdetails;
    double gettotal;
    double Added_paise;
    string agent;
    string fdate;
    string tdate;
    string ppcode;
    DataTable dttt = new DataTable();   
    DataTable dttt1 = new DataTable();
    DataTable dtttchartname = new DataTable();
    DataTable show = new DataTable();
    DateTime d11;
    DateTime d22;
    string FDATE;
    string TODATE;
    double smilkkg;
    string getmilkkg;
    double nilkgrate;
    double tempgetmilkkg;
    string ccname;
    int serial = 0;
    int countdirst = 1;
    string getquery3;

    int TEMPCOUNTS=0;

    DataTable TEMPAGENTID = new DataTable();

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
                    Session["tempp"] = Convert.ToString(pcode);
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
               
                   
                       //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                       //txt_todate.Text = dtm.ToString("dd/MM/yyyy");
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        billdate();
                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        billdate();
                    }

                }
                else
                {



                }

               
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;
             //   billdate();
                ViewState["pcode"] = pcode.ToString();
 
            }
           
        }

        catch
        {



        }
    }
    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

        datasetcount = datasetcount + 1;
    }
    public void loadsingleplant()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
        datasetcount = datasetcount + 1;
    }
    public void billdate()
    {
        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" +ddl_Plantname.SelectedItem.Value + "'  order by  Bill_frmdate desc  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
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
                HeaderCell2.Text = "Chilling Plant Rate Chart:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 12;
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[7].Text == "&nbsp;")
            {

                e.Row.BackColor = System.Drawing.Color.LightCyan;

            }

            if ((e.Row.Cells[2].Text == "&nbsp;") && (e.Row.Cells[11].Text == "&nbsp;"))
            {

                e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow;

            }

            if ((e.Row.Cells[10].Text == "ZeroRate"))
            {

                e.Row.BackColor = System.Drawing.Color.LightCoral;

            }
            if ((e.Row.Cells[10].Text == "Total"))
            {

                e.Row.BackColor = System.Drawing.Color.LightGreen;

            }

        e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.BackColor = System.Drawing.Color.DarkOrange;
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button10_Click(object sender, EventArgs e)
    {

    }
    protected void Button11_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void txt_todate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
        ViewState["FD"] = FDATE.ToString();
        ViewState["TTODATE"] = TODATE.ToString();
        show.Columns.Add("Sno");
        show.Columns.Add("ChartName");
        show.Columns.Add("ChartType");
        show.Columns.Add("MilkNature");
        show.Columns.Add("Min Fat");
        show.Columns.Add("Min Snf");
        show.Columns.Add("Commission");
        show.Columns.Add("FromRange");
        show.Columns.Add("ToRange");
        show.Columns.Add("Rate");
        show.Columns.Add("Bonus");
        show.Columns.Add("Milkkg");

        TEMPAGENTID.Columns.Add("AGENT_ID");
        TEMPAGENTID.Columns.Add("MILKKG");
        TEMPAGENTID.Columns.Add("TS");

        string getqueryname = "";
        getqueryname = "Select   cchartname   from (Select cchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf  from (Select Ratechart_Id,Plant_Code as ppcode   from Procurement  where  Plant_Code='"+ddl_Plantname.SelectedItem.Value+"'  and Prdate between '"+FDATE+"' and '"+TODATE+"'  group by Ratechart_Id,Plant_Code) as gg left join (Select Chart_Name as cchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code as cmplant   from Chart_Master where Plant_Code='"+ddl_Plantname.SelectedItem.Value+"' group by Chart_Name,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code ) as  cm on gg.ppcode=cm.cmplant and gg.Ratechart_Id=cm.cchartname) as leftside  left join (Select Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code as rcplant   from Rate_Chart  where Plant_Code='"+ddl_Plantname.SelectedItem.Value+"' group by Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code  ) as rc on leftside.ppcode=rc.rcplant and leftside.cchartname=rc.Chart_Name   group by cchartname";
        con = DB.GetConnection();
        SqlCommand cmdname = new SqlCommand(getqueryname, con);
        SqlDataAdapter dstgetqueryname = new SqlDataAdapter(cmdname);
        dtttchartname.Rows.Clear();
        dstgetqueryname.Fill(dtttchartname);
        foreach (DataRow name in dtttchartname.Rows)
        {
            string ChartName = name[0].ToString();
            string getquery = "";
            getquery = "Select   cmchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Comission_Amount   from (Select cmchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf  from (Select Ratechart_Id,Plant_Code as ppcode   from Procurement  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "'  and Ratechart_Id='" + ChartName.ToString() + "' group by Ratechart_Id,Plant_Code) as gg left join (Select Chart_Name as cmchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code as cmplant   from Chart_Master where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' group by Chart_Name,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code ) as  cm on gg.ppcode=cm.cmplant and gg.Ratechart_Id=cm.cmchartname) as leftside  left join (Select Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code as rcplant   from Rate_Chart  where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' group by Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code  ) as rc on leftside.ppcode=rc.rcplant and leftside.cmchartname=rc.Chart_Name";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(getquery, con);
            SqlDataAdapter dst = new SqlDataAdapter(cmd);
            dttt.Rows.Clear();
            dst.Fill(dttt);
            string type1 = dttt.Rows[0][1].ToString();
            string Milk_Nature = dttt.Rows[0][2].ToString();
            string Min_Fat = dttt.Rows[0][3].ToString();
            string Min_Snf = dttt.Rows[0][4].ToString();
            string Comission_Amount = dttt.Rows[0][5].ToString();
            show.Rows.Add("", ChartName, type1, Milk_Nature, Min_Fat, Min_Snf, Comission_Amount, "", "", "", "");
            string getquery1 = "";
            getquery1 = "Select cmchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,From_RangeValue,To_RangeValue,convert(decimal(18,2),Rate) as Rate,Comission_Amount   from (Select cmchartname,ppcode,Chart_Type,Milk_Nature,Min_Fat,Min_Snf  from (Select Ratechart_Id,Plant_Code as ppcode   from Procurement  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "' AND Ratechart_Id='" + ChartName + "'  group by Ratechart_Id,Plant_Code) as gg left join (Select Chart_Name as cmchartname,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code as cmplant   from Chart_Master where Plant_Code='"+ ddl_Plantname.SelectedItem.Value +"' group by Chart_Name,Chart_Type,Milk_Nature,Min_Fat,Min_Snf,Plant_Code ) as  cm on gg.ppcode=cm.cmplant and gg.Ratechart_Id=cm.cmchartname) as leftside  left join (Select Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code as rcplant   from Rate_Chart  where Plant_Code='"+ddl_Plantname.SelectedItem.Value+"' group by Chart_Name,From_RangeValue,To_RangeValue,Rate,Comission_Amount,Bouns_Amount,Plant_Code  ) as rc on leftside.ppcode=rc.rcplant and leftside.cmchartname=rc.Chart_Name";
            con = DB.GetConnection();
            SqlCommand cmd1 = new SqlCommand(getquery1, con);
            SqlDataAdapter dst1 = new SqlDataAdapter(cmd1);
            dttt1.Rows.Clear();
            dst1.Fill(dttt1);
            foreach (DataRow gk in dttt1.Rows)
            {
                ccname = "";
                 ccname = gk[0].ToString();
                 ViewState["CHARTNAME"] = ccname;
                string frange = gk[6].ToString();
                string trange = gk[7].ToString();
                double TEMPfrange = Convert.ToDouble(frange);
                double TEMPtrange = Convert.ToDouble(trange);
                string rate = gk[8].ToString();
                string commi = gk[9].ToString();
                string getquery2 = "";
                if (type1 == "TS")
                {
                    //getquery2 = "Select  convert(decimal(18,2),Sum(Milk_kg)) as kgs  from Procurement  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "' and   Ratechart_Id='" + ccname + "'     and    (( Fat + Snf) >= '" + TEMPfrange + "')    AND (( Fat + Snf)  <= '" + TEMPtrange + "')";

                    getquery2 = "Select  tid,Sum(fat+snf) as ts,convert(decimal(18,2),Sum(Milk_kg)) as kgs,agent_id  from Procurement  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "'    AND  Ratechart_Id='" + ccname + "'     group by tid,agent_id  order by tid  asc";


                    con = DB.GetConnection();
                    SqlCommand cmd2ts = new SqlCommand(getquery2, con);
                    SqlDataAdapter dst2ts = new SqlDataAdapter(cmd2ts);
                    milkkgts.Rows.Clear();
                    dst2ts.Fill(milkkgts);

                }
                if (type1 == "FAT")
                {
                    getquery2 = "Select  convert(decimal(18,2),Sum(Milk_kg)) as kgs    from Procurement  where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "'    and   Ratechart_Id='" + ccname + "'     and    (( Fat) >= '" + frange + "' )   AND (( Fat)  <= '" + trange + "')";

                    con = DB.GetConnection();
                    SqlCommand cmd2 = new SqlCommand(getquery2, con);
                    SqlDataAdapter dst2 = new SqlDataAdapter(cmd2);
                    milkkg.Rows.Clear();
                    dst2.Fill(milkkg);

                }
                if (type1 == "SNF")
                {
                    getquery2 = "Select   convert(decimal(18,2),Sum(Milk_kg)) as kgs   from Procurement  where    Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "'   and   Ratechart_Id='" + ccname + "'     and    (( SNF) >= '" + frange + "'    AND ( SNF)  <= '" + trange + "')";
                }
                if (type1 != "TS")
                {

                    con = DB.GetConnection();
                    SqlCommand cmd2 = new SqlCommand(getquery2, con);
                    SqlDataAdapter dst2 = new SqlDataAdapter(cmd2);
                    milkkg.Rows.Clear();
                    dst2.Fill(milkkg);


                }


                if (type1 == "TS")
                {
                    foreach (DataRow drps in milkkgts.Rows)
                    {
                        double tss = Convert.ToDouble(drps[1]);
                        string temptss = tss.ToString("F2");
                        tss = Convert.ToDouble(temptss);
                        double fromrange = Convert.ToDouble(frange);
                        double totrange = Convert.ToDouble(trange);
                        if ((tss >= fromrange) && (tss <= totrange))
                        {

                            //TEMPCOUNTS = TEMPCOUNTS + 1;

                          double tempmilk = Convert.ToDouble(drps[2]);
                         
                            string GETFIL= (tempgetmilkkg + tempmilk).ToString("F2");
                            tempgetmilkkg = Convert.ToDouble(GETFIL);


                         //TEMPAGENTID.Rows.Add(drps[3].ToString(),drps[2].ToString(),drps[1].ToString());

                        }


                    }
                   
                }

                if (type1 != "TS")
                {


                    try
                    {
                        try
                        {
                            getmilkkg = milkkg.Rows[0][0].ToString();
                        }
                        catch
                        {
                            getmilkkg = "0.0";

                        }
                        tempgetmilkkg = Convert.ToDouble(getmilkkg);
                        smilkkg = smilkkg + tempgetmilkkg;
                    }
                    catch
                    {
                        getmilkkg = "0.0";

                    }

                }

                serial = serial + 1;
                show.Rows.Add(serial, "", "", "", "", "","", frange, trange, rate, commi, tempgetmilkkg.ToString("f2"));
                if (type1 == "TS")
                {
                 //   getquery3 = "Select   convert(decimal(18,2),Sum(Milk_kg)) as kgs   from Procurement  where    Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "'  and   Ratechart_Id='" + ViewState["CHARTNAME"].ToString() + "'     and   (fat < '" + Min_Fat + "' ) and (snf < '" + Min_Snf + "')";

                    getquery3 = "Select   convert(decimal(18,2),Sum(Milk_kg)) as kgs   from Procurement  where    Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate   between '" + FDATE + "' and '" + TODATE + "' and   Ratechart_Id='" + ViewState["CHARTNAME"].ToString() + "'     and  RATE=0 ";
                }
                if (type1 == "FAT")
                {
                    //getquery3 = "Select   convert(decimal(18,2),Sum(Milk_kg)) as kgs   from Procurement  where    Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "'  and   Ratechart_Id='" + ViewState["CHARTNAME"].ToString() + "'     and   fat < '" + Min_Fat + "'";

                    getquery3 = "Select   convert(decimal(18,2),Sum(Milk_kg)) as kgs   from Procurement  where    Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate  between '" + FDATE + "' and '" + TODATE + "' and   Ratechart_Id='" + ViewState["CHARTNAME"].ToString() + "'    and  RATE=0 ";

                }
                if (type1 == "SNF")
                {
                //    getquery3 = "Select   convert(decimal(18,2),Sum(Milk_kg)) as kgs   from Procurement  where    Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Prdate between '" + FDATE + "' and '" + TODATE + "'  and   Ratechart_Id='" + ViewState["CHARTNAME"].ToString() + "'       and    ( snf < '" + Convert.ToDouble(Min_Snf) + "')";
                    getquery3 = "Select   convert(decimal(18,2),Sum(Milk_kg)) as kgs   from Procurement  where    Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and  Prdate between '" + FDATE + "' and '" + TODATE + "'    and   Ratechart_Id='" + ViewState["CHARTNAME"].ToString() + "'      and  RATE=0 ";

                }
                con = DB.GetConnection();
                SqlCommand cmd3 = new SqlCommand(getquery3, con);
                SqlDataAdapter dst3 = new SqlDataAdapter(cmd3);
                nilrate.Rows.Clear();
                dst3.Fill(nilrate);
                string nilkg= nilrate.Rows[0][0].ToString();
                try
                {
                    nilkgrate = Convert.ToDouble(nilkg);
                }
                catch
                {
                    nilkgrate = 0.00;

                }
                if (type1 == "TS")
                {
                    smilkkg = smilkkg + tempgetmilkkg;
                    tempgetmilkkg = 0;
                }
            }
            show.Rows.Add("", "", "", "", "", "", "", "", "","", "ZeroRate", nilkgrate.ToString("F2"));
            show.Rows.Add("", "", "", "", "", "", "", "", "","","Total", (smilkkg + nilkgrate).ToString("F2"));
            //show.Rows.Add("", "", "", "", "", "", "", "", "", "","","");
            smilkkg = 0;
            nilkgrate = 0;
            tempgetmilkkg = 0;
            serial = 0;
            GridView1.DataSource = show;
            GridView1.DataBind();

        }
          if (dttt.Rows.Count > 0)
        {


        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }
    protected void Button7_Click(object sender, EventArgs e)
    {

        try
        {

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + ":Bill Period" + ViewState["FD"] + "To:" + ViewState["TTODATE"] + "'.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch
        {

        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
}