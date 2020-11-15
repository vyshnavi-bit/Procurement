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
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.util.collections;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using System.IO;
public partial class DpuAmountForRoute : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    public string uid;
    double getavil;
    double getval;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    public int flag = 0;
    public int flag1 = 0;
    public string Frmdate1;
    public string Todate1;
    public string pcode1;
    string date;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime dtm = new DateTime();
    DateTime dtm2 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess = new DbHelper();
    string plantamount;
    string issuingamount;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    int vvv;
    string val1 ;
    string[] val2 ;
    string fdate ;
    string tdate ;
    string plcode;
    double BalAmount ;
    double Amount;
    int msgcount;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                //    pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  uid = Session["User_ID"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_time.Text = DateTime.Now.ToString("hh:mm:ss tt");
                txt_userid.Text = Session["Name"].ToString();

                gettid();
                getamount();
                gridview();
                Panel3.Visible = false;
              //  btn_save.Visible = false;
                //gridview();

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
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  uid = Session["User_ID"].ToString();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
            gridview();
            getamount();
           // gridview();
        }
    }


    public void gettid()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            //SqlDataReader dr = null;
            //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            try
            {
                string sqlstr = "SELECT max(tid) as  tid  FROM DpuAdminAmountAllottoplant ";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                int id = Convert.ToInt16(ds.Tables[0].Rows[0]["tid"]);
                txt_tid.Text = (id + 1).ToString();
            }
            catch
            {
                txt_tid.Text = "1";


            }
        }
    }

    private void getagentmasterdatareader()
    {
        SqlDataReader dr;
        string sqlstr = null;
        sqlstr = "SELECT Company_Code,Plant_Code,Bill_frmdate,Bill_todate FROM Bill_date where Company_Code='1' AND plant_Code='" + pcode1 + "' AND CurrentPaymentFlag='1'    order by  Plant_Code ";
        dr = dbaccess.GetDatareader(sqlstr);
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                Frmdate1 = dr["Bill_frmdate"].ToString();
                Todate1 = dr["Bill_todate"].ToString();
            }
        }

    }
    public void getamount()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            //SqlDataReader dr = null;
            //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            try
            {
                string sqlstr = "SELECT ISNULL((t1.AdminAmt-t2.AdminPlantAmt),0) AS AvailAmount FROM (SELECT ISNULL(SUM(Amount),0) AS AdminAmt from DpuAdminAllotAmount ) AS t1 LEFT JOIN (SELECT ISNULL(SUM(Amount),0) AS AdminPlantAmt  from DpuAdminAmountAllottoplant) AS t2 ON t1.AdminAmt>0";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                getavil = Convert.ToDouble(ds.Tables[0].Rows[0]["AvailAmount"]);
                Txt_Availamount.Text = getavil.ToString("F2");

                if (Txt_Availamount.Text == "")
                {

                    Txt_Availamount.Text = "0";

                }
            }
            catch
            {

                Txt_Availamount.Text = "0";

            }
        }
    }


    public void gridview()
    {

        try
        {
            DateTime dt1 = new DateTime();
            //  DateTime dt2 = new DateTime();
            //  dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);

            //   dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            //    string d2 = dt2.ToString("MM/dd/yyyy");

            dt2 = txt_time.Text;

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

              //  string sqlstr = "select Date,pcode,PlantName,BalAmount,ppcode as Amount  from (select Date,pcode,Plant_Name as PlantName,isnull(Amount,0) as BalAmount,0 as ppcode from (SELECT (FromDate + '_ ' + ToDate) as Date,pcode,Plant_Name FROM ( select   convert(varchar,Bill_frmdate,103) as FromDate,convert(varchar,Bill_todate,103) as ToDate,Plant_Code as pcode from Bill_date where  Plant_Code > 154    and CurrentPaymentFlag=1)  AS AA left join (select plant_code as plantcode,Plant_Name   from  Plant_Master ) as pm on AA.pcode=pm.plantcode) as ppp left join (SELECT SUM(FLOOR(Amount)) as Amount,Plant_Code as ppcode  FROM ProducerProcurement Where Company_code='1' AND  Prdate between '2-11-2016' AND '2-20-2016' group by Plant_Code) as pp on ppp.pcode=pp.ppcode  ) as jj  order by jj.pcode";

               // string sqlstr = "select Date,pcode,PlantName,BalAmount,isnull(CheckAmount,0) as Amount  from (select Date,pcode,Plant_Name as PlantName,isnull(Amount,0) as BalAmount,0 as ppcode from (SELECT (FromDate + '_ ' + ToDate) as Date,pcode,Plant_Name FROM ( select   convert(varchar,Bill_frmdate,103) as FromDate,convert(varchar,Bill_todate,103) as ToDate,Plant_Code as pcode from Bill_date where  Plant_Code > 154    and CurrentPaymentFlag=1)  AS AA left join (select plant_code as plantcode,Plant_Name   from  Plant_Master ) as pm on AA.pcode=pm.plantcode) as ppp left join (SELECT SUM(FLOOR(Amount)) as Amount,Plant_Code as ppcode  FROM ProducerProcurement Where Company_code='1' AND     payflag='1' group by Plant_Code) as pp on ppp.pcode=pp.ppcode  ) as jj  left join  ( select Plant_code,sum(Amount) as CheckAmount   from   DpuAdminAmountAllotToPlant  where   payflag='1' group by Plant_code) as aaaa on jj.pcode=aaaa.Plant_code order by pcode  ";
                //string sqlstr = "select FromDate + '_ ' + Todate as Date,pcode,Plant_Name as PlantName,DpuAmount,CheckAmount   from(select Plant_Code as pcode,convert(varchar,Bill_frmdate,103) as FromDate,CONVERT(varchar,Bill_todate,103) as Todate,Amount as DpuAmount,CheckAmount from (select * from(select Plant_Code,Bill_frmdate,Bill_todate,Amount   from ( select Plant_Code,Bill_frmdate,Bill_todate,SUM(Amount) as Amount from (select Bill_frmdate,Bill_todate,Plant_Code as PlantCode  from  Bill_date  where Plant_Code > 154 and CurrentPaymentFlag=1  group by Bill_frmdate,Bill_todate,Plant_Code) as a inner join (select SUM(Amount) as Amount,Plant_Code,Prdate  from ProducerProcurement    group by Plant_Code,Prdate)   as b on b.Prdate between a.Bill_frmdate and a.Bill_todate  and a.PlantCode=b.Plant_Code   and Amount > 0 and Plant_Code is not null group by Plant_Code,Bill_frmdate,Bill_todate  ) as aa    where Amount > 0    ) as l left join (select billfrmdate,billtodate, Plant_code as dpuplantcode,sum(amount) as CheckAmount   from   DpuAdminAmountAllotToPlant     group by Plant_code,Billfrmdate,Billtodate) as r on l.Plant_Code=r.dpuplantcode and l.Bill_frmdate=r.Billfrmdate  and l.Bill_todate=r.Billtodate)as ab   where ab.CheckAmount > 0) as dd left join (select Plant_Code,Plant_Name  from  Plant_Master group by Plant_Code,Plant_Name) as ed on dd.pcode=ed.Plant_Code";
                string sqlstr = "select FromDate + '_ ' + Todate as Date,pcode,Plant_Name as PlantName,DpuAmount,CheckAmount   from(select Plant_Code as pcode,convert(varchar,Bill_frmdate,103) as FromDate,CONVERT(varchar,Bill_todate,103) as Todate,Amount as DpuAmount,CheckAmount from (select * from(select Plant_Code,Bill_frmdate,Bill_todate,Amount   from ( select Plant_Code,Bill_frmdate,Bill_todate,SUM(Amount) as Amount from (select Bill_frmdate,Bill_todate,Plant_Code as PlantCode  from  Bill_date  where Plant_Code > 154 and CurrentPaymentFlag=1  group by Bill_frmdate,Bill_todate,Plant_Code) as a inner join (select SUM(Amount) as Amount,Plant_Code,Prdate  from ProducerProcurement    group by Plant_Code,Prdate)   as b on b.Prdate between a.Bill_frmdate and a.Bill_todate  and a.PlantCode=b.Plant_Code   and Amount > 0 and Plant_Code is not null group by Plant_Code,Bill_frmdate,Bill_todate  ) as aa    where Amount > 0    ) as l left join (select billfrmdate,billtodate, Plant_code as dpuplantcode,sum(amount) as CheckAmount   from   DpuAdminAmountAllotToPlant     group by Plant_code,Billfrmdate,Billtodate) as r on l.Plant_Code=r.dpuplantcode and l.Bill_frmdate=r.Billfrmdate  and l.Bill_todate=r.Billtodate)as ab  ) as dd left join (select Plant_Code,Plant_Name  from  Plant_Master group by Plant_Code,Plant_Name) as ed on dd.pcode=ed.Plant_Code";
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();





                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }



            }
        }
        catch
        {
           


        }
    }
    protected void Savebtn_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (vvv == 1)
            date = e.Row.Cells[0].Text;
      //   date = GridView1.SelectedRow.Cells[1].Text;




        getagentmasterdatareader();


    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridview();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        gridview();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Panel1.Visible = false;
            Panel3.Visible = true;
             val1 = GridView1.SelectedRow.Cells[0].Text;
             string[] val2 = val1.Split('_');
            // fd.Text = (val2[0]).ToString();

             string first = (val2[0]).ToString();
             string[] val11 = first.Split('/');
             string fmd=val11[1].ToString();
             string fdd = val11[0].ToString();
             string fyyy = val11[2].ToString();
             fd.Text = fmd +"/"+ fdd + "/"+ fyyy;



             string second = (val2[1]).ToString();
             string[] val12 = second.Split('/');
             string tmd = val12[1].ToString();
             string tdd = val12[0].ToString();
             string tyyy = val12[2].ToString();
             txt_daat.Text = tmd + "/" + tdd + "/" + tyyy;






              
             txt_pcode.Text = GridView1.SelectedRow.Cells[1].Text;
             Txtplant.Text  = GridView1.SelectedRow.Cells[2].Text;
             BalAmount = Convert.ToDouble(GridView1.SelectedRow.Cells[3].Text);

             string  getAmount = (GridView1.SelectedRow.Cells[4].Text);
             if (getAmount == "&nbsp;")
             {

                 Amount = 0.0;
             }
             else
             {
                 Amount = Convert.ToDouble(GridView1.SelectedRow.Cells[4].Text);
             }
             double balance = (BalAmount - Amount);

           // Txtbalance.Text = BalAmount.ToString();
             Txtbalance.Text = balance.ToString("F2");
            if (Amount  < 0 )
            {
                Amount = 0;
                Txtamount.Text = Amount.ToString("F2");
            }


           

           
        }
        catch (Exception Ex)
        {

            lbl_msg.Text = Ex.Message;
            lbl_msg.Visible = true;
            lbl_msg.ForeColor = System.Drawing.Color.Red;
        }



    }
    protected void Savebtn1_Click(object sender, EventArgs e)
    {

        try
        {

            if (Txtamount.Text != "")
            {


                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);


                DateTime dt1 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                string dd = dt1.ToString("MM/dd/yyyy");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                double avail = Convert.ToDouble(Txt_Availamount.Text);
               double bal =      Convert.ToDouble(Txtbalance.Text);
               double amount =   Convert.ToDouble(Txtamount.Text);

               if (avail >= amount && amount > 0.0 && bal >= amount)
               {

                   //   cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','155','" + txt_arani.Text + "','" + dd + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                   cmd.CommandText = "INSERT INTO DpuAdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES  ('" + ccode + "','" + txt_pcode.Text + "','" + Txtamount.Text + "','" + dd + "','" + txt_time.Text + "','" + txt_userid.Text + "','Accounts','" + ccode + "','" + ccode + "','" + fd.Text + "','" + txt_daat.Text + "' )";
                   cmd.ExecuteNonQuery();
                   gridview();
                   msgcount = 1;
                   msgbox();
                   getamount();
                   Txtamount.Text = "";
                
                   Panel1.Visible = true;
                   Panel3.Visible = false;
               }

               else
               {
                   msgcount = 2;
                   msgbox();
                   Txtamount.Focus();
               }


            }
            else
            {
                Txtamount.Focus();
            }
        }
        catch(Exception Ex)
        {

            lbl_msg.Text = Ex.Message;
            lbl_msg.Visible = true;
            lbl_msg.ForeColor = System.Drawing.Color.Red;
        }



    }

    public void msgbox()
    {
        string message;
        if (msgcount == 1)
        {
             message = "Your details have been saved successfully.";
        }
        else
        {
             message = "Please Check Your Amount.";

        }
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {

        Panel1.Visible =true ;
        Panel3.Visible = false;
    }
}
