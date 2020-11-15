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

public partial class AmountForRoute : System.Web.UI.Page
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

    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime dtm = new DateTime();
    DateTime dtm2 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess = new DbHelper();
    public static int roleid;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
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
              
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_time.Text = DateTime.Now.ToString("hh:mm:ss tt");
                txt_userid.Text = Session["Name"].ToString();              

                gettid();
                getamount();
                LoadPlantName1();
                Menu1();
                btn_save.Visible = false;           
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

           

        }
    }

   
    protected void MChk_PlantName_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }
    private void Menu1()
    {
        if (MChk_PlantName.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
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
                string sqlstr = "SELECT max(tid) as  tid  FROM AdminAmountAllottoplant ";
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
                string sqlstr = "SELECT ISNULL((t1.AdminAmt-t2.AdminPlantAmt),0) AS AvailAmount FROM (SELECT ISNULL(SUM(Amount),0) AS AdminAmt from AdminAllotAmount ) AS t1 LEFT JOIN (SELECT ISNULL(SUM(Amount),0) AS AdminPlantAmt  from AdminAmountAllottoplant) AS t2 ON t1.AdminAmt>0";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                getavil = Convert.ToDouble(ds.Tables[0].Rows[0]["AvailAmount"]);
                Txt_Availamount.Text = getavil.ToString();

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
    protected void Button1_Click(object sender, EventArgs e)
    {


    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_arani_TextChanged(object sender, EventArgs e)
    {



    }
    protected void txt_userid_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_Check_Click(object sender, EventArgs e)
    {
        CheckData();
    }

    private void getagentmasterdatareader()
    {
        SqlDataReader dr;
        string sqlstr = null;
        sqlstr = "SELECT Company_Code,Plant_Code,Bill_frmdate,Bill_todate FROM Bill_date where Company_Code='1' AND plant_Code='" + pcode1 + "' AND CurrentPaymentFlag='1'";
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
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string dd = dt1.ToString("MM/dd/yyyy");
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            //
            try
            {
                //
                double orgamount = 0.0;
                double TotalSelectAmount = 0.0;
                double TotalSelectAmount1 = 0.0;
                double TotalSelectAmount2 = 0.0;
                txt_name.Text = "Accounts";
                getamount();
                orgamount = Convert.ToDouble(Txt_Availamount.Text);
                if (orgamount > 0)
                {
                    if (!(string.IsNullOrEmpty(txt_arani.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_arani.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "155";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','155','" + txt_arani.Text + "','" + dd + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();

                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_arani.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_kaveri.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_kaveri.Text);
                        if (TotalSelectAmount1 > 0)
                        {

                            pcode1 = "156";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','156','" + txt_kaveri.Text + "','" + dd + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_kaveri.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_gud.Text)))
                    { 
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_gud.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "157";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "', '157','" + txt_gud.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_gud.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_wal.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_wal.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "158";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "', '158','" + txt_wal.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_wal.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_vkota.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_vkota.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "159";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "', '159','" + txt_vkota.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_vkota.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_palli.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_palli.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "160";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "', '160','" + txt_palli.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_palli.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_rcpura.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_rcpura.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "161";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "', '161','" + txt_rcpura.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_rcpura.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_bomm.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_bomm.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "162";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "', '162','" + txt_bomm.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_bomm.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_tari.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_tari.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "163";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','163','" + txt_tari.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_tari.Text = "0.0";
                        }
                    }

                    if (!(string.IsNullOrEmpty(txt_kala.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_kala.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "164";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "', '164','" + txt_kala.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_kala.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_cspur.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_cspur.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "165";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','165','" + txt_cspur.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_cspur.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_kon.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_kon.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "166";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','166','" + txt_kon.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_kon.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_kava.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_kava.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "167";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','167','" + txt_kava.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_kava.Text = "0.0";
                        }
                    }
                    if (!(string.IsNullOrEmpty(txt_Gudipalli.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_Gudipalli.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "168";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','168','" + txt_Gudipalli.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
						{
							txt_Gudipalli.Text = "0.0";
						}
					}

                    if (!(string.IsNullOrEmpty(txt_kaligiri.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_kaligiri.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "169";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','169','" + txt_kaligiri.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_kaligiri.Text = "0.0";
                        }
                    }

                    if (!(string.IsNullOrEmpty(txt_gudicow.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_gudicow.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "171";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','168','" + txt_Gudipalli.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_Gudipalli.Text = "0.0";
                        }
                    }

					
                    if (!(string.IsNullOrEmpty(txt_kdpcow.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_kdpcow.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "172";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','172','" + txt_kdpcow.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_kdpcow.Text = "0.0";
                        }
                    }

                    if (!(string.IsNullOrEmpty(txt_allapati.Text)))
                    {
                        TotalSelectAmount1 = 0.0;
                        TotalSelectAmount1 = Convert.ToDouble(txt_allapati.Text);
                        if (TotalSelectAmount1 > 0)
                        {
                            pcode1 = "173";
                            getagentmasterdatareader();
                            cmd.CommandText = "INSERT INTO AdminAmountAllotToPlant (Plant_Name,Plant_code,Amount,Date,Time,UserId,Name,ccode,sampefiled,Billfrmdate,Billtodate) VALUES ('" + ccode + "','173','" + txt_allapati.Text + "','" + dt1 + "','" + txt_time.Text + "','" + txt_userid.Text + "','" + txt_name.Text + "','" + ccode + "','" + ccode + "','" + Frmdate1 + "','" + Todate1 + "' )";
                            cmd.ExecuteNonQuery();
                            gettid();
                            TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                            flag1 = 1;
                        }
                        else
                        {
                            txt_allapati.Text = "0.0";
                        }
                    }

                    //Start Inser
                    TotalSelectAmount2 = Convert.ToDouble(Txt_Availamount.Text) - TotalSelectAmount;
                    if (TotalSelectAmount2 >= 0)
                    {
                        Txt_Availamount.Text = (Convert.ToDouble(Txt_Availamount.Text) - TotalSelectAmount).ToString();
                        flag = 1;
                        if (flag1 == 1)
                        {
                            flag1 = 0;
                            btn_save.Visible = true;

                        }
                        else
                        {
                            flag1 = 0;
                            btn_save.Visible = false;
                        }
                        // WebMsgBox.Show("Please Select the Agents for Remaining Amounts...");
                    }
                    else
                    {
                        Txt_Availamount.Text = orgamount.ToString();
                        flag = 0;
                        btn_save.Visible = false;
                        // WebMsgBox.Show("Please Select the Agents for Givent Amounts Only...");
                    }
                    //End Inser
                }
                else
                {
                    WebMsgBox.Show("Please Check the Available Amounts...");
                }


                //
            }
            catch
            {
                WebMsgBox.Show("Please Check Values");


            }
            //           
           
            WebMsgBox.Show(" DataInserted Successfully");
            conn.Close();
        }

        catch
        {
            WebMsgBox.Show("Please Check Values");
        }

        //flag set
        LoadPlantName1();
        clear();
        flag = 0;
        btn_save.Visible = false;
    }
    private void clear()
    {
        txt_arani.Text = "0.0";
        txt_kaveri.Text = "0.0";
        txt_gud.Text = "0.0";
        txt_wal.Text = "0.0";
        txt_vkota.Text = "0.0";
        txt_palli.Text = "0.0";
        txt_rcpura.Text = "0.0";
        txt_bomm.Text = "0.0";
        txt_tari.Text = "0.0";
        txt_kala.Text = "0.0";
        txt_kava.Text = "0.0";
        txt_kon.Text = "0.0";
        txt_cspur.Text = "0.0";
        txt_Gudipalli.Text = "0.0";
    }

    private void CheckData()
    {
        try
        {
            //
            double orgamount = 0.0;
            double TotalSelectAmount = 0.0;
            double TotalSelectAmount1 = 0.0;
            double TotalSelectAmount2 = 0.0;
            getamount();
            orgamount = Convert.ToDouble(Txt_Availamount.Text);
            if (orgamount > 0)
            {
                if (!(string.IsNullOrEmpty(txt_arani.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_arani.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_arani.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_kaveri.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_kaveri.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_kaveri.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_gud.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_gud.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_gud.Text = "0.0";
                    }
                }

                if (!(string.IsNullOrEmpty(txt_wal.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_wal.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_wal.Text = "0.0";
                    }
                }



                if (!(string.IsNullOrEmpty(txt_vkota.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_vkota.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_vkota.Text = "0.0";
                    }
                }




                if (!(string.IsNullOrEmpty(txt_palli.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_palli.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_palli.Text = "0.0";
                    }
                }

                if (!(string.IsNullOrEmpty(txt_rcpura.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_rcpura.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_rcpura.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_bomm.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_bomm.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_bomm.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_tari.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_tari.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_tari.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_kala.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_kala.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_kala.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_cspur.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_cspur.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_cspur.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_kon.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_kon.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_kon.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_kava.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_kava.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_kava.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_Gudipalli.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_Gudipalli.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_Gudipalli.Text = "0.0";
                    }
				}

               

				if (!(string.IsNullOrEmpty(txt_kaligiri.Text)))
				{
					TotalSelectAmount1 = 0.0;
					TotalSelectAmount1 = Convert.ToDouble(txt_kaligiri.Text);
					if (TotalSelectAmount1 > 0)
					{
						TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
						flag1 = 1;
					}
					else
					{
						txt_kaligiri.Text = "0.0";
					}
				}


                if (!(string.IsNullOrEmpty(txt_gudicow.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_gudicow.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_gudicow.Text = "0.0";
                    }
                }

                if (!(string.IsNullOrEmpty(txt_kdpcow.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_kdpcow.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_kdpcow.Text = "0.0";
                    }
                }

                if (!(string.IsNullOrEmpty(txt_allapati.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_allapati.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_allapati.Text = "0.0";
                    }
                }



























				//Start Inser
                TotalSelectAmount2 = Convert.ToDouble(Txt_Availamount.Text) - TotalSelectAmount;
                if (TotalSelectAmount2 >= 0)
                {
                    Txt_Availamount.Text = (Convert.ToDouble(Txt_Availamount.Text) - TotalSelectAmount).ToString();
                    flag = 1;
                    if (flag1 == 1)
                    {
                        flag1 = 0;
                        btn_save.Visible = true;

                    }
                    else
                    {
                        flag1 = 0;
                        btn_save.Visible = false;
                    }
                    // WebMsgBox.Show("Please Select the Agents for Remaining Amounts...");
                }         
                else
                {
                    Txt_Availamount.Text = orgamount.ToString();
                    flag = 0;
                    btn_save.Visible = false;
                    WebMsgBox.Show("Please Check the Available Amounts ...");
                }
                //End Inser
            }
            else
            {
                WebMsgBox.Show("Please Check the Available Amounts...");
            }


            //
        }
        catch
        {
            WebMsgBox.Show("Please Check Values");


        }
    }

    private void CheckDataandInsert()
    {
        try
        {
            //
            double orgamount = 0.0;
            double TotalSelectAmount = 0.0;
            double TotalSelectAmount1 = 0.0;
            double TotalSelectAmount2 = 0.0;
            orgamount = Convert.ToDouble(Txt_Availamount.Text);
            if (orgamount > 0)
            {
                if (!(string.IsNullOrEmpty(txt_arani.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_arani.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_arani.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_kaveri.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_kaveri.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_kaveri.Text = "0.0";
                    }
                }
                if (!(string.IsNullOrEmpty(txt_gud.Text)))
                {
                    TotalSelectAmount1 = 0.0;
                    TotalSelectAmount1 = Convert.ToDouble(txt_gud.Text);
                    if (TotalSelectAmount1 > 0)
                    {
                        TotalSelectAmount = TotalSelectAmount + TotalSelectAmount1;
                        flag1 = 1;
                    }
                    else
                    {
                        txt_gud.Text = "0.0";
                    }
                }

                //Start Inser
                TotalSelectAmount2 = Convert.ToDouble(Txt_Availamount.Text) - TotalSelectAmount;
                if (TotalSelectAmount2 >= 0)
                {
                    Txt_Availamount.Text = (Convert.ToDouble(Txt_Availamount.Text) - TotalSelectAmount).ToString();
                    flag = 1;
                    if (flag1 == 1)
                    {
                        flag1 = 0;
                        btn_save.Visible = true;

                    }
                    else
                    {
                        flag1 = 0;
                        btn_save.Visible = false;
                    }
                    // WebMsgBox.Show("Please Select the Agents for Remaining Amounts...");
                }
                else
                {
                    Txt_Availamount.Text = orgamount.ToString();
                    flag = 0;
                    btn_save.Visible = false;
                    // WebMsgBox.Show("Please Select the Agents for Givent Amounts Only...");
                }
                //End Inser
            }
            else
            {
                WebMsgBox.Show("Please Check the Available Amounts...");
            }


            //
        }
        catch
        {
            WebMsgBox.Show("Please Check Values");


        }
    }


    private void Loadperdayplantmilk()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Plant_Name";
                CheckBoxList1.DataValueField = "plant_Code";//ROUTE_ID 
                CheckBoxList1.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }


    private void LoadPlantName1()
    {
        try
        {

            DataSet ds = new DataSet();
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[GetPaymentAllot_BalanceAmount]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                if (ds != null)
                {
                    CheckBoxList1.DataSource = ds;
                    CheckBoxList1.DataTextField = "Plant";
                    CheckBoxList1.DataValueField = "Plant";//ROUTE_ID 
                    CheckBoxList1.DataBind();

                    CheckBoxList2.DataSource = ds;
                    CheckBoxList2.DataTextField = "BalanceAmount";
                    CheckBoxList2.DataValueField = "BalanceAmount";//ROUTE_ID 
                    CheckBoxList2.DataBind();

                    CheckBoxList3.DataSource = ds;
                    CheckBoxList3.DataTextField = "Billdate";
                    CheckBoxList3.DataValueField = "Billdate";//ROUTE_ID 
                    CheckBoxList3.DataBind();

                }
                
            }
        }

        catch (Exception ex)
        {
        }
    }
}