using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class SapApinvoiceUpdate : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    //SqlConnection con = new SqlConnection();
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable sapmrn = new DataTable();
    DataTable charc = new DataTable();
    DataTable snoo = new DataTable();
    DataTable availornot = new DataTable();
    DataTable milktype = new DataTable();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
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
                  //  txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                     billdate();

                }
                else
                {
                    pname = ddl_Plantname.SelectedItem.Text;

                }

            }

            else
            {
                ccode = Session["Company_code"].ToString();
                pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {

        }
    }
    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        getagentprocurementdetails();
    }
    public void getagentprocurementdetails()
    {
         getinvoiceaavail();
        if (availornot.Rows.Count < 1)
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


            DateTime dt1 = new DateTime();
           // dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string getplantcode;
            con = DB.GetConnection();
       //     getplantcode = "Select prdate,Sessions,Rate,Amount,Commi,CartAmt,SplBon,Refenrence,plant_code,proagent   from (Select proagent,prdate,Sessions,days,month,year,MilkKg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Agent_Name,VendorCode,clr,Refenrence  from (sELECT proagent,prdate,Sessions,convert(varchar,days) as days,convert(varchar,month) as month,convert(varchar,year) as year,milkkg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Agent_Name,VendorCode,Plant_Code,clr,Refenrence   FROM (sELECT *    FROM (Select   Tid, convert(varchar,Agent_id,0) AS proagent,convert(varchar,Prdate,101) as prdate,Sessions,DATEPART(day,Prdate) as days ,DATEPART(MONTH,Prdate) as month,FORMAT(prdate,'yy') as year,Sum(Fat) as Fat,Sum(Snf) as Snf,Sum(Milk_kg) as MilkKg,Sum(milk_ltr) as   milkltr,Sum(rate) as Rate,Sum(Amount) as Amount,Sum(Comrate) as Commi,Sum(CartageAmount) as CartAmt,Sum(SplBonusAmount) as SplBon,Plant_Code,Sum(Clr) as clr,Refenrence     from procurement    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and  Prdate='" + d1 + "'  AND SESSIONS='" + ddl_shift.SelectedItem.Text + "'   group by   Plant_Code,Tid,Agent_id,prdate,Sessions,Refenrence)  AS PRO LEFT JOIN (sELECT Agent_Id,Agent_Name,VendorCode   FROM Agent_Master    WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   GROUP BY  Agent_Id,Agent_Name,VendorCode) as am on PRO.proagent= am.Agent_Id) AS ONEE WHERE VendorCode IS  NOT NULL)  as rrr) as leftside left join (Select   Plant_Code as pmplantcode,Plant_Name,WHcode   from  Plant_Master    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'     group by  Plant_Code,Plant_Name,WHcode) as pmm on Plant_Code=pmm.pmplantcode";
         //   getplantcode = "Select prdate,Sessions,Rate,Amount,Commi,CartAmt,SplBon,Refenrence,plant_code,proagent   from (Select proagent,prdate,Sessions,days,month,year,MilkKg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Agent_Name,VendorCode,clr,Refenrence  from (sELECT proagent,prdate,Sessions,convert(varchar,days) as days,convert(varchar,month) as month,convert(varchar,year) as year,milkkg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Agent_Name,VendorCode,Plant_Code,clr,Refenrence   FROM (sELECT *    FROM (Select   Tid, convert(varchar,Agent_id,0) AS proagent,convert(varchar,Prdate,101) as prdate,Sessions,DATEPART(day,Prdate) as days ,DATEPART(MONTH,Prdate) as month,FORMAT(prdate,'yy') as year,Sum(Fat) as Fat,Sum(Snf) as Snf,Sum(Milk_kg) as MilkKg,Sum(milk_ltr) as   milkltr,Sum(rate) as Rate,Sum(Amount) as Amount,Sum(Comrate) as Commi,Sum(CartageAmount) as CartAmt,Sum(SplBonusAmount) as SplBon,Plant_Code,Sum(Clr) as clr,Refenrence     from procurement    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and  prdate between '" + FDATE + "' and '" + TODATE + "'   group by   Plant_Code,Tid,Agent_id,prdate,Sessions,Refenrence)  AS PRO LEFT JOIN (sELECT Agent_Id,Agent_Name,VendorCode   FROM Agent_Master    WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   GROUP BY  Agent_Id,Agent_Name,VendorCode) as am on PRO.proagent= am.Agent_Id) AS ONEE WHERE VendorCode IS  NOT NULL)  as rrr) as leftside left join (Select   Plant_Code as pmplantcode,Plant_Name,WHcode   from  Plant_Master    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'     group by  Plant_Code,Plant_Name,WHcode) as pmm on Plant_Code=pmm.pmplantcode";
            getplantcode = "Select prdate,Sessions,Rate,Amount,Commi,CartAmt,SplBon,Refenrence,plant_code,proagent   from (Select proagent,prdate,Sessions,days,month,year,MilkKg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Agent_Name,VendorCode,clr,Refenrence  from (sELECT proagent,prdate,Sessions,convert(varchar,days) as days,convert(varchar,month) as month,convert(varchar,year) as year,milkkg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Agent_Name,VendorCode,Plant_Code,clr,Refenrence   FROM (sELECT *    FROM (Select   Tid, convert(varchar,Agent_id,0) AS proagent,convert(varchar,Prdate,101) as prdate,Sessions,DATEPART(day,Prdate) as days ,DATEPART(MONTH,Prdate) as month,FORMAT(prdate,'yy') as year,Sum(Fat) as Fat,Sum(Snf) as Snf,Sum(Milk_kg) as MilkKg,Sum(milk_ltr) as   milkltr,Sum(rate) as Rate,Sum(Amount) as Amount,Sum(Comrate) as Commi,Sum(CartageAmount) as CartAmt,Sum(SplBonusAmount) as SplBon,Plant_Code,Sum(Clr) as clr,Refenrence     from procurement    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and  prdate between '" + FDATE + "' and '" + TODATE + "'  group by   Plant_Code,Tid,Agent_id,prdate,Sessions,Refenrence)  AS PRO LEFT JOIN (sELECT Agent_Id,Agent_Name,VendorCode   FROM Agent_Master    WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   GROUP BY  Agent_Id,Agent_Name,VendorCode) as am on PRO.proagent= am.Agent_Id) AS ONEE WHERE VendorCode IS  NOT NULL)  as rrr) as leftside left join (Select   Plant_Code as pmplantcode,Plant_Name,WHcode   from  Plant_Master    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'     group by  Plant_Code,Plant_Name,WHcode) as pmm on Plant_Code=pmm.pmplantcode";
            SqlCommand cmd = new SqlCommand(getplantcode, con);
            SqlDataAdapter dws = new SqlDataAdapter(cmd);
            sapmrn.Rows.Clear();
            dws.Fill(sapmrn);
            foreach (DataRow dio in sapmrn.Rows)
            {
                  string prdate     =  dio[0].ToString();
                  string Sessions   = dio[1].ToString().TrimEnd();
                  string Rate        = dio[2].ToString().TrimEnd();
                  string Amount     = dio[3].ToString();
                  string Commi      = dio[4].ToString();
                  string CartAmt    = dio[5].ToString();
                  string SplBon     = dio[6].ToString();
                  string Refenrence = dio[7].ToString();
                  string plant_code = dio[8].ToString();
                  string proagent   = dio[9].ToString();
                  period();
             
                //string nature =  milktype.Rows[0][0].ToString();
                //if (nature == "1")
                //{

                //}
                //else
                //{
                //   //double temprate =  Convert.ToDouble(Rate);
                //   //Rate = (temprate * (1.03)).ToString("F2");
                //}

                  if (Rate == "0")
                  {
                      Commi = "0";
                  }
                 
                  string getupdate = "update saptransaction Set Rate='" + Rate + "',Cartage='" + CartAmt + "',SplBon='" + SplBon + "',Commissions='" + Commi + "',Amount='" + Amount + "',updateStatus='1'  where Createdate='" + prdate + "' and  Sessions='" + Sessions + "'   and plant_code='" + ddl_Plantname.SelectedItem.Value + "' and agent_id='" + proagent + "' and Updaterefence='" + Refenrence + "'";
                  con = DB.GetConnection();
                  SqlCommand updatecmd = new SqlCommand(getupdate, con);
                  updatecmd.ExecuteNonQuery();
   
            }
            string mss = "Data Updated  SuccessFully";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

        }
        else
        {
            string mss = "Data Already updated";

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

        }
    }
    public void getinvoiceaavail()
    {
        try
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


            con1 = DB.GetConnection();
            string check;
            //  check = "Select    *   from EMROPCH   where    Taxdate='" + d1 + "'  and session='" + ddl_shift.SelectedItem.Text + "'    AND U_Status='" + ddl_Plantname.SelectedItem.Value + "' ";
            check = "Select    *   from saptransaction   where    CreateDate BETWEEN '" + FDATE + "'  AND '" + TODATE + "'  AND      plant_code='" + ddl_Plantname.SelectedItem.Value + "'  AND updateStatus='1'  ";
            SqlCommand cmd = new SqlCommand(check, con1);
            SqlDataAdapter av = new SqlDataAdapter(cmd);
            availornot.Rows.Clear();
            av.Fill(availornot);

        }
        catch
        {


        }
    }
    public void period()
    {
        con = DB.GetConnection();
        string getperiodstr = "";
        getperiodstr = " Select top 1  Milktype    from    Plant_Master where plant_code not in (139,150)  and Plant_Code='"+ddl_Plantname.SelectedItem.Value+"'  group by  Plant_Code,Milktype";
        SqlCommand cmdperiod = new SqlCommand(getperiodstr, con);
        SqlDataAdapter datype = new SqlDataAdapter(cmdperiod);
        milktype.Rows.Clear();
        datype.Fill(milktype);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    public void billdate()
    {

        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  order by  Bill_frmdate desc  ";
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
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}