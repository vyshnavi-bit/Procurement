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
using System.IO;
using System.Collections.Generic;
public partial class PlantOverReport : System.Web.UI.Page
{
    public string pcode;
    public string ccode;
    public string pname;
    public string cname;
    public string frmdate;
 //   public string todate;
    int n = 2;
    string sqlstr = string.Empty;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    DataTable dt = new DataTable();
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();

    DateTime dt1 = new DateTime();
    DateTime dt2 = new DateTime();
    string d1;
    string d2;
    string connSttr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    string fromdate;
    string todate;
    string fdate;
    string tdate;
    int tid;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                ccode = Session["Company_code"].ToString();
                //    pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();


                dtm = System.DateTime.Now;
                LoadPlantcode();
                // gridview();
            }

            else
            {
                //dtm = System.DateTime.Now;
                //// dti = System.DateTime.Now;
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyy");

                pcode = ddl_Plantcode.SelectedItem.Value;
                GetbillDate();
                //  LoadPlantcode();


            }

        }
        else
        {


            pcode = ddl_Plantcode.SelectedItem.Value;



            // GetbillDate();
            //  LoadPlantcode();
            //   ddl_agentcode.Text = getrouteusingagent;

            if (fdate == string.Empty || fdate==null)
            {



            }
            else
            {
                TextBox1.Text = fdate;
                TextBox2.Text = tdate;

                dt1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null);

                d1 = dt1.ToString("MM/dd/yyyy");
                d2 = dt2.ToString("MM/dd/yyyy");
                gridview();

            }






        }

    }




    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
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

    private void GetbillDate()
    {

        try
        {

            ddl_getfdate.Items.Clear();
            ddl_gettdate.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from procurementimport where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = " select top 3   convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate,tid  from Bill_date where plant_code='"+pcode+"'   order by Tid desc ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               ddl_getfdate.Items.Add(dr["Bill_frmdate"].ToString());
               ddl_gettdate.Items.Add(dr["Bill_todate"].ToString());
                   
                ddl_Billdate.Items.Add(dr["Bill_frmdate"].ToString() + "_" + dr["Bill_todate"].ToString());

                 tid = Convert.ToInt16( dr["tid"].ToString());


            }

            //fromdate = dr["Bill_frmdate"].ToString();
            //todate = dr["Bill_todate"].ToString();
        }

        catch
        {

            WebMsgBox.Show("PLEASE CHECK");

        }

    }



    protected void btn_Save_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {


        try
        {
            ddl_Billdate.Items.Clear();
            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            pcode = ddl_Plantcode.SelectedItem.Value;
            GetbillDate();





          //  gridview();

        }
        catch(Exception ex)
        {

            WebMsgBox.Show(ex.ToString()); 

        }
        
    }
    protected void ddl_Billdate_SelectedIndexChanged(object sender, EventArgs e)
    {
         // GetbillDate();

       


         ddl_getfdate.SelectedIndex = ddl_Billdate.SelectedIndex ;

          fdate = ddl_getfdate.SelectedItem.Value ;
          ddl_gettdate.SelectedIndex = ddl_Billdate.SelectedIndex ;
          tdate = ddl_gettdate.SelectedItem.Value ;

          TextBox1.Text = fdate;
          TextBox2.Text = tdate;

          dt1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);
          dt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null);

          d1 = dt1.ToString("MM/dd/yyyy");
          d2 = dt2.ToString("MM/dd/yyyy");





          if (fdate == string.Empty || fdate == null)
          {



          }
          else
          {
              //TextBox1.Text = fdate;
              //TextBox2.Text = tdate;

              //dt1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);
              //dt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null);

              //d1 = dt1.ToString("MM/dd/yyyy");
              //d2 = dt2.ToString("MM/dd/yyyy");
              //gridview();

          }




     //   gridview();
        //lblpcode.Text = pcode;
        //lblname.Text = ddl_Plantname.Text;

        lblpcode.Text = ddl_Plantname.Text;
       // lblname.Text = ddl_Plantname.Text;
               
    }


    public void gridview()
    {
        try
        {


            //TextBox1.Text = fdate;
            //TextBox2.Text = tdate;

            //dt1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null);

            //d1 = dt1.ToString("MM/dd/yyyy");
            //d2 = dt2.ToString("MM/dd/yyyy");



            //TextBox1.Text = fdate;
            //TextBox2.Text = tdate;

            //dt1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null);

            //d1 = dt1.ToString("MM/dd/yyyy");
            //d2 = dt2.ToString("MM/dd/yyyy");

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                //     string sqlstr = "select   Agent_Id,Agent_Name,Route_id,Plant_code   from  Agent_Master where Plant_code='" + pcode + "'  and Agent_id   not in(select Agent_id from Procurement  where  plant_code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "')  order by Agent_Id ";

            //    string sqlstr = " select c.TotMkg,c.TotMltr,c.PerDayLtr,c.Ltrcost,c.Kgfat,c.Avgfat,c.Kgsnf,c.Avgsnf,c.TotalAmount,CAST(c.Avgfat + c.Avgsnf as numeric(10,2)) as Ts,   from (   select   cast(b.totkg as numeric(10,2)) as TotMkg,  cast(b.Totamt as numeric(10,2)) as TotalAmount ,CAST(b.TotMlr as numeric(10,2)) as TotMltr,cast(b.Ltr as numeric(10,2)) as Ltrcost,b.Kgfat,b.Kgsnf,cast(b.Kgfat *100 / totkg as numeric(18,2)) as Avgfat,cast(b.Kgsnf *100 / totkg as numeric(18,2)) as Avgsnf,cast(b.TotMlr /  b.Noofdate  as numeric(18,2)) as  PerDayLtr  from (select  a1.DiffDate+1 as Noofdate, totkg,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,sum(cart+sAmt+Bonus +incen)/(a1.TotMlr) as Ltr,a1.TotMlr,SUM(cart+sAmt+Bonus +incen) as Totamt  from (select  sum(sfatkg) as Kgfat,sum(smkg) as totkg,SUM(smltr) as TotMlr,sum(sAmt) as sAmt,sum(SInsentAmt) as incen,sum(Scaramt) as cart,sum(SSplBonus) as Bonus,SUM(SSnfkg) as Kgsnf,SUM(afat) as sumfat,SUM(asnf) as sumsnf,DATEDIFF(day,'" + d1 + "','" + d2 + "') AS DiffDate  from  paymentdata  where plant_code='" + pcode + "'  and frm_date='" + d1 + "' and to_date= '" + d2 + "' group by  plant_code) as a1   group by  a1.sAmt,a1.totkg,a1.TotMlr,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,a1.DiffDate) as b ) as c";

               // string sqlstr = "select c.TotMkg,c.TotMltr,c.PerDayLtr,c.Ltrcost,c.Kgfat,c.Avgfat,c.Kgsnf,c.Avgsnf,c.TotalAmount,CAST(c.Avgfat + c.Avgsnf as numeric(10,2)) as Ts,convert(varchar,c.Frm_date,103) as Frm_date,convert(varchar,c.To_date,103) as To_date  from (   select   cast(b.totkg as numeric(10,2)) as TotMkg,  cast(b.Totamt as numeric(10,2)) as TotalAmount ,CAST(b.TotMlr as numeric(10,2)) as TotMltr,cast(b.Ltr as numeric(10,2)) as Ltrcost,b.Kgfat,b.Kgsnf,cast(b.Kgfat *100 / totkg as numeric(18,2)) as Avgfat,cast(b.Kgsnf *100 / totkg as numeric(18,2)) as Avgsnf,cast(b.TotMlr /  b.Noofdate  as numeric(18,2)) as  PerDayLtr,b.Frm_date,b.To_date  from (select  a1.DiffDate+1 as Noofdate, totkg,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,sum(cart+sAmt+Bonus +incen)/(a1.TotMlr) as Ltr,a1.TotMlr,SUM(cart+sAmt+Bonus +incen) as Totamt,a1.Frm_date,a1.To_date  from (select  sum(sfatkg) as Kgfat,sum(smkg) as totkg,SUM(smltr) as TotMlr,sum(sAmt) as sAmt,sum(SInsentAmt) as incen,sum(Scaramt) as cart,sum(SSplBonus) as Bonus,SUM(SSnfkg) as Kgsnf,SUM(afat) as sumfat,SUM(asnf) as sumsnf,DATEDIFF(day,'" + d1 + "','" + d2 + "') AS DiffDate,frm_date AS Frm_date ,to_date as To_date  from  paymentdata  where plant_code='"+pcode+"'  and frm_date='" + d1 + "' and to_date= '" + d2 + "' group by  plant_code,Frm_date,To_date) as a1   group by  a1.sAmt,a1.totkg,a1.TotMlr,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,a1.DiffDate,a1.Frm_date,a1.To_date) as b ) as c  ";

              //  string sqlstr = "select c.TotMkg,c.TotMltr,c.PerDayLtr,c.Ltrcost,c.Kgfat,c.Avgfat,c.Kgsnf,c.Avgsnf,c.TotalAmount,CAST(c.Avgfat + c.Avgsnf as numeric(10,2)) as Ts,convert(varchar,c.Frm_date,103) as Frm_date,convert(varchar,c.To_date,103) as To_date,cast(c.roundoff as numeric(10,2)) as roundoff,c.Netpay  from (   select   cast(b.totkg as numeric(10,2)) as TotMkg,  cast(b.Totamt as numeric(10,2)) as TotalAmount ,CAST(b.TotMlr as numeric(10,2)) as TotMltr,cast(b.Ltr as numeric(10,2)) as Ltrcost,b.Kgfat,b.Kgsnf,cast(b.Kgfat *100 / totkg as numeric(18,2)) as Avgfat,cast(b.Kgsnf *100 / totkg as numeric(18,2)) as Avgsnf,cast(b.TotMlr /  b.Noofdate  as numeric(18,2)) as  PerDayLtr,b.Frm_date,b.To_date,b.roundoff,CAST(b.Totamt - b.Roundoff as numeric (18,2))as Netpay  from (select  a1.DiffDate+1 as Noofdate, totkg,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,sum(cart+sAmt+Bonus +incen)/(a1.TotMlr) as Ltr,a1.TotMlr,SUM(cart+sAmt+Bonus +incen) as Totamt,a1.Frm_date,a1.To_date,a1.Roundoff  from (select  sum(sfatkg) as Kgfat,sum(smkg) as totkg,SUM(smltr) as TotMlr,sum(sAmt) as sAmt,sum(SInsentAmt) as incen,sum(Scaramt) as cart,sum(SSplBonus) as Bonus,SUM(SSnfkg) as Kgsnf,SUM(afat) as sumfat,SUM(asnf) as sumsnf,DATEDIFF(day,'" + d1 + "','" + d2 + "') AS DiffDate,frm_date AS Frm_date ,to_date as To_date,SUM(Roundoff) as Roundoff  from  paymentdata  where plant_code='" + pcode + "'  and frm_date='" + d1 + "' and to_date= '" + d2 + "' group by  plant_code,Frm_date,To_date) as a1   group by  a1.sAmt,a1.totkg,a1.TotMlr,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,a1.DiffDate,a1.Frm_date,a1.To_date,a1.Roundoff) as b ) as c  ";

                string sqlstr = " select c.TotMkg,c.TotMltr,c.PerDayLtr,c.Ltrcost,c.Kgfat,c.Avgfat,c.Kgsnf,c.Avgsnf,c.TotalAmount,c.Billadv,c.Ai,c.Feed,c.can,c.recovery,c.others,Sinstamt,ClaimAount ,CAST(c.Avgfat + c.Avgsnf as numeric(10,2)) as Ts,convert(varchar,c.Frm_date,103) as Frm_date,convert(varchar,c.To_date,103) as To_date,cast(c.roundoff as numeric(10,2)) as roundoff,c.Netpay  from (   select   cast(b.totkg as numeric(10,2)) as TotMkg,  cast(b.Totamt as numeric(10,2)) as TotalAmount ,CAST(b.TotMlr as numeric(10,2)) as TotMltr,cast(b.Ltr as numeric(10,2)) as Ltrcost,b.Kgfat,b.Kgsnf,cast(b.Kgfat *100 / totkg as numeric(18,2)) as Avgfat,cast(b.Kgsnf *100 / totkg as numeric(18,2)) as Avgsnf,cast(b.TotMlr /  b.Noofdate  as numeric(18,2)) as  PerDayLtr,b.Frm_date,b.To_date,b.roundoff,CAST(b.Totamt +  b.ClaimAount - b.Billadv   -  b.Ai -  b.Feed - b.can -   b.recovery - b.others - b.Sinstamt  -  b.Roundoff as numeric (18,2))as Netpay,b.Billadv,b.Ai,b.Feed,b.can,b.recovery,b.others,b.Sinstamt,b.ClaimAount   from (select  a1.DiffDate+1 as Noofdate, totkg,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,sum(cart+sAmt+Bonus +incen)/(a1.TotMlr) as Ltr,a1.TotMlr,SUM(cart+sAmt+Bonus +incen) as Totamt,a1.Frm_date,a1.To_date,a1.Roundoff,a1.Billadv,a1.Ai,a1.Feed,a1.can,a1.recovery,a1.others,a1.Sinstamt,a1.ClaimAount   from (select  sum(sfatkg) as Kgfat,sum(smkg) as totkg,SUM(smltr) as TotMlr,sum(sAmt) as sAmt,sum(SInsentAmt) as incen,sum(Scaramt) as cart,sum(SSplBonus) as Bonus,SUM(SSnfkg) as Kgsnf,SUM(afat) as sumfat,SUM(asnf) as sumsnf,DATEDIFF(day,'" + d1 + "','" + d2 + "') AS DiffDate,frm_date AS Frm_date ,to_date as To_date,SUM(Roundoff) as Roundoff,sum(Billadv) as Billadv,SUM(ai) as Ai,SUM(Feed) as Feed,sum(can) as Can,sum(recovery) as recovery,SUM(others) as others,sum(Sinstamt) as Sinstamt,sum(ClaimAount) as ClaimAount    from  paymentdata  where plant_code='" + pcode + "'  and frm_date='" + d1 + "' and to_date= '" + d2 + "' group by  plant_code,Frm_date,To_date) as a1   group by  a1.sAmt,a1.totkg,a1.TotMlr,a1.Kgfat,a1.Kgsnf,a1.sumfat,a1.sumsnf,a1.DiffDate,a1.Frm_date,a1.To_date,a1.Roundoff,a1.Billadv,a1.Ai,a1.Feed,a1.can,a1.recovery,a1.others,a1.Sinstamt,a1.ClaimAount ) as b ) as c  ";

                
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.Visible = true;



                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    GridView1.Visible = false;
                   // WebMsgBox.Show("Data Not Avail Particular Bill Period"); 

                }
            }


        }
        catch
        {


        }




      


    }




    protected void Button2_Click(object sender, EventArgs e)
    {


        HtmlForm form = new HtmlForm();

        Response.Clear();

        Response.Buffer = true;

        //  string filename="GridViewExport_"+DateTime.Now.ToString()+".xls";

        string filename = pcode + "-" + "Agent Id:" + d1 + "RateChartName:" + DateTime.Now.ToString() + ".xls";

        Response.AddHeader("content-disposition",

        "attachment;filename=" + filename);

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;

        GridView1.DataBind();

        form.Controls.Add(GridView1);

        this.Controls.Add(form);

        form.RenderControl(hw);

        //style to format numbers to string

        string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        Response.Write(style);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        ddl_getfdate.SelectedIndex = ddl_Billdate.SelectedIndex;

        fdate = ddl_getfdate.SelectedItem.Value;
        ddl_gettdate.SelectedIndex = ddl_Billdate.SelectedIndex;
        tdate = ddl_gettdate.SelectedItem.Value;

        TextBox1.Text = fdate;
        TextBox2.Text = tdate;

        dt1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null);

        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");





        if (fdate == string.Empty || fdate == null)
        {



        }
        else
        {
            TextBox1.Text = fdate;
            TextBox2.Text = tdate;

            dt1 = DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(TextBox2.Text, "dd/MM/yyyy", null);

            d1 = dt1.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");
            gridview();

        }




        //   gridview();
        //lblpcode.Text = pcode;
        //lblname.Text = ddl_Plantname.Text;

        lblpcode.Text = ddl_Plantname.Text;
    }
}