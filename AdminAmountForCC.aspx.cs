using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminAmountForCC : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    DataSet DTG = new DataSet();
    DbHelper DB = new DbHelper();
    SqlDataReader dr;
    BLLuser Bllusers = new BLLuser();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    DbHelper dbaccess = new DbHelper();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    SqlConnection con = new SqlConnection();
    int datasetcount = 0;
    DataTable serial = new DataTable();
    long snoo;
    DataTable availcash = new DataTable();
    DataTable fileclose = new DataTable();
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


                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        Bdate();
                    }
                    else
                    {
                        LoadPlantcode();
                        pcode = ddl_Plantname.SelectedItem.Value;
                        Bdate();
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

    public void getavailamt()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
     //   string cashinhand = "Select (Amount - Netamount) as Inhand from (Select isnull(Amount,0) as Amount,isnull(NetAmt,0) as Netamount   from ( Select Sum(amount) as Amount,Plant_code  from   AdminAmountForCC   where Plant_Code='" + pcode + "'   and Frm_date='" + d1 + "' and To_Date='" + d2 + "'   group by Plant_code) as admincc  left join (Select    isnull(Sum(NetAmount),0) as NetAmt,Plant_Code as bankplantcode     from    BankPaymentllotment   where Plant_Code='" + pcode + "'   and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "' group by plant_code) as bankpay on  admincc.Plant_code=bankpay.bankplantcode) as cash";
        string cashinhand = "sELECT ISNULL(sUM(Inhand),0) AS Inhand   FROM (Select ISNULL((Amount - Netamount),0) as Inhand from (Select isnull(Amount,0) as Amount,isnull(NetAmt,0) as Netamount   from ( Select isnull(Sum(amount),0) as Amount,Plant_code  from   AdminAmountForCC   where Plant_Code='" + pcode + "'   and Frm_date='" + d1 + "' and To_Date='" + d2 + "'  group by Plant_code) as admincc  left join (Select    isnull(Sum(NetAmount),0) as NetAmt,Plant_Code as bankplantcode     from    BankPaymentllotment   where Plant_Code='" + pcode + "'   and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "' group by plant_code) as bankpay on  admincc.Plant_code=bankpay.bankplantcode) as cash) AS GG";
        con = dbaccess.GetConnection();
        SqlCommand cmd = new SqlCommand(cashinhand, con);
        SqlDataAdapter dsp = new SqlDataAdapter(cmd);
        availcash.Rows.Clear();
        dsp.Fill(availcash);
        if (availcash.Rows.Count > 0)
        {
            txt_cashamt.Text = availcash.Rows[0][0].ToString();
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
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + ddl_Plantname.SelectedItem.Value + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[datasetcount];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
        datasetcount = datasetcount + 1;
    }
    private void Bdate()
    {
        try
        {
            dr = null;
            dr = Billdate(ccode, ddl_Plantname.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                }
            }
            else
            {
                //txt_FromDate.Text = "10/10/1983";
                //txt_ToDate.Text = "10/10/1983";
                //WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }
    public SqlDataReader Billdate(string ccode, string pcode)
    {
        SqlDataReader dr;
        string sqlstr = string.Empty;
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND CurrentPaymentFlag='1' ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
    protected void Savebtn_Click(object sender, EventArgs e)
    {

        GETFILECLOSEORNOT();
        if (fileclose.Rows.Count < 1)
        {
            getserialno();
            //string dt10 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            //string dt20 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");
            DateTime dt1T = System.DateTime.Now;
            DateTime dt2T = System.DateTime.Now;
            dt1T = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2T = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1T.ToString("MM/dd/yyyy");
            string d2 = dt2T.ToString("MM/dd/yyyy");
            //DateTime DFFF = Convert.ToDateTime(dt1T);
            //ViewState["td"] = DFFF.ToString("MM/dd/yyyy");
            string GETMM = Convert.ToDateTime(dt1T).ToString();
            DateTime FF = Convert.ToDateTime(GETMM);
            string GETMM1 = Convert.ToDateTime(dt1T).ToString("dd/MM/yyy");
            DateTime FF1 = Convert.ToDateTime(dt1T);
            string GETFF1 = Convert.ToDateTime(FF1).ToString("MMM");
            string GETMM2 = Convert.ToDateTime(dt2T).ToString();
            DateTime FF2 = Convert.ToDateTime(GETMM2);
            string GETMM12 = Convert.ToDateTime(dt2T).ToString("dd/MM/yyy");
            DateTime FF12 = Convert.ToDateTime(dt2T);
            string GETFF12 = Convert.ToDateTime(FF12).ToString("MMM");
            string SYE = Convert.ToDateTime(FF12).ToString("yy");
            string[] first = GETMM1.Split('/');
            string frm = first[0];
            string[] second = GETMM12.Split('/');
            string too = second[0];
            //string GETMM2 = Convert.ToDateTime(dt2T).ToString("dd/MM/yyy");
            //DateTime FF2 = Convert.ToDateTime(GETMM2);
            //string GETFF2 = Convert.ToDateTime(FF2).ToString("MMM");
            //string SYE = Convert.ToDateTime(FF2).ToString("yy");
            //string frm = first[1];
            //string too = second[1];
            //string yea = Convert.ToDateTime(txt_FromDate.Text).ToString("yy");
            //string dt1 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
            //string dt2 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");
            //string[] first = GETMM1.Split('/');
            //string[] second = GETMM2.Split('/');

            //string frm = first[1];
            //string too = second[1];

            string filename = ddl_Plantname.SelectedItem.Value + GETFF1 + frm + too + SYE + "-" + snoo;
            try
            {
                con = DB.GetConnection();
                string stt = "Insert into AdminAmountForCC(Plant_code,Frm_date,To_Date,Amount,LoginUser,Description,Filename,SerialNo) values('" + ddl_Plantname.SelectedItem.Value + "','" + d1 + "','" + d2 + "','" + tbNumbers.Text + "','" + Session["Name"].ToString() + "','" + txt_description.Text + "','" + filename + "','" + snoo + "') ";
                SqlCommand cmd = new SqlCommand(stt, con);
                cmd.ExecuteNonQuery();
                string mss = "Data Saved  SuccessFully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
                tbNumbers.Text = "";
                txt_description.Text = "";


            }
            catch
            {


            }
            getavailamt();
        }
        else
        {
            string mss = "Some File Are  Updated Pending";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
            tbNumbers.Text = "";
            txt_description.Text = "";

        }
    }
 

    public void GETFILECLOSEORNOT()
    {

        try
        {
            DateTime dt1T = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            DateTime dt2T = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);



            string d1 = dt1T.ToString("MM/dd/yyyy");
            string d2 = dt2T.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            string stt = "SELECT Status FROM  AdminAmountForCC  WHERE Plant_Code = '" + ddl_Plantname.SelectedItem.Value + "'  and Frm_date='" + d1 + "'   and to_date='" + d2 + "'  and status=0";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(fileclose);

        }
        catch
        {


        }
  
    }

    public void getserialno()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT max(SerialNo)+1 FROM  AdminAmountForCC  WHERE Plant_Code = '" + ddl_Plantname.SelectedItem.Value + "'";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(serial);
             snoo = Convert.ToInt32(serial.Rows[0][0].ToString());
        }
        catch
        {
            snoo = 1;
        }

    }

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Bdate();
        getavailamt();
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {

    }
}