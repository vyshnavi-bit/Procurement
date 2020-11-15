using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class sappayment : System.Web.UI.Page
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
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable getrefe = new DataTable();
    DataTable WARE = new DataTable();
    DataTable existstable = new DataTable();
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
    string referenceno;
    string warehousecode;


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
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
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
                //  LoadPlantcode();

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
    public void getdata()
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
        con = DB.GetConnection();
        con1 = DB.GetConnectionSAP();
        string sele;
        //sele = "Select  top  2 payagent,payment_mode,frm_date,to_date,netamount,agent_id,vendorcode    from (  Select   *   from (Select   agent_id as payagent,payment_mode,convert(varchar,frm_date,110) as frm_date,convert(varchar,to_date,110) as to_date,Netamount   from  Paymentdata  where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and frm_date='" + FDATE + "'   and  to_date='" + TODATE + "' AND  Netamount > 0 ) as gg left join (Select   agent_id,VendorCode    from agent_master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by   agent_id,VendorCode) as nn on  gg.payagent = nn.agent_id) as lefytside    order   by  rand(payagent) asc  ";
        sele = "Select  payagent,payment_mode,frm_date,to_date,netamount,agent_id,vendorcode    from (  Select   *   from (Select   agent_id as payagent,payment_mode,convert(varchar,frm_date,110) as frm_date,convert(varchar,to_date,110) as to_date,Netamount   from  Paymentdata  where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and frm_date='" + FDATE + "'   and  to_date='" + TODATE + "' AND  Netamount > 0 ) as gg left join (Select   agent_id,VendorCode    from agent_master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by   agent_id,VendorCode) as nn on  gg.payagent = nn.agent_id) as lefytside    order   by  rand(payagent) asc  ";
        SqlCommand CMD = new SqlCommand(sele, con);
        SqlDataAdapter DRF = new SqlDataAdapter(CMD);
        DataTable DTS=new DataTable();
        DTS.Rows.Clear();
        DRF.Fill(DTS);
        if (DTS.Rows.Count > 0)
        {
            foreach (DataRow dgp in DTS.Rows)
            {
                string series = "";
                string agentcode = dgp[0].ToString();
                string payment_mode = dgp[1].ToString();
                if (payment_mode == "BANK")
                {
                    series = "236";

                }
                if (payment_mode == "CASH")
                {
                    series = "234";

                }
                string frm_date = dgp[2].ToString();
                string to_date = dgp[3].ToString();
                ViewState["tdate"] = to_date.ToString();
                string netamount = dgp[4].ToString();
                double TEMPAMT = Convert.ToDouble(netamount);
                string vendorcode = dgp[6].ToString();
                ViewState["vencode"] = vendorcode;
                getagentreference();
                getwarehousecode();
                string REMARKS = agentcode + "-" + payment_mode + "-" + frm_date + "-" + to_date + "-" + vendorcode;
                DateTime getdate=new DateTime();
                getdate = System.DateTime.Now;
                string insert = "";
                string paymentdoc = "PaymentDoc";
                string N = "N";
                string Y = "Y";
                string ocrcode2="P0001";
                if (referenceno != string.Empty)
                {

                  //  insert = "insert into EMROVPM (CreateDate,PaymentDate,DOE,ReferenceNo,CardCode,Remarks,AcctNo,InvoiceNo,PaymentDoc,PaymentMode,PaymentSum,OcrCode,B1Upload,Processed)values('" + getdate + "','" + frm_date + "','" + to_date + "','" + referenceno + "','" + ViewState["vencode"] + "','" + REMARKS + "','1','1','1','" + payment_mode + "','" + TEMPAMT.ToString("F2") + "','" + vendorcode + "','N','N')";
                  //  insert = "insert into EMROVPM (CreateDate,paymentDate,DOE,ReferenceNo,CardCode,Remarks,Status,AcctNo,InvoiceNo,PaymentDoc,PaymentMode,PaymentSum,OcrCode,OcrCode2,B1Upload,Processed,Series) values('" + getdate + "','" + getdate + "','" + getdate + "','" + referenceno + "','" + vendorcode + "','" + REMARKS + "','" + ddl_Plantname.SelectedItem.Value + "','" + ddl_sapbankpayment.SelectedItem.Value + "','" + referenceno + "','" + paymentdoc + "','" + payment_mode + "','" + netamount + "','" + warehousecode + "','" + ocrcode2 + "','" + N + "','" + N + "','" + series + "')";
                    insert = "insert into EMROVPM (CreateDate,paymentDate,DOE,ReferenceNo,CardCode,Remarks,Status,AcctNo,InvoiceNo,PaymentDoc,PaymentMode,PaymentSum,OcrCode,OcrCode2,B1Upload,Processed,Series,ApprovedBy) values('" + frm_date + "','" + getdate + "','" + to_date + "','" + referenceno + "','" + vendorcode + "','" + REMARKS + "','" + ddl_Plantname.SelectedItem.Value + "','" + ddl_sapbankpayment.SelectedItem.Value + "','" + referenceno + "','" + paymentdoc + "','" + payment_mode + "','" + netamount + "','" + warehousecode + "','" + ocrcode2 + "','" + N + "','" + N + "','" + series + "','"+Session["Name"].ToString()+"')";
                    SqlCommand CMD12 = new SqlCommand(insert, con1);
                   CMD12.ExecuteNonQuery();
                }
                else
                {


                }

                

              //  CreateDate = entry

              //  PaymentDate = billdate
                //   DOE = billdate

              //  ReferenceNo  =agentid
                // CardCode = vendorcode
                // Remarks =agent_id + netAMOUNT;BILLFROMDATE AND TODATE
                //AcctNo  = 1

             //   InvoiceNo = 1

              //  PaymentDoc = 1
              //  PaymentMode= BANK;
            //    PaymentSum = NET
              //  OcrCode = 
               // B1Upload = N
                //Processed = Y
               // update_status_paymentdata(ddl_Plantname.SelectedItem.Value, dgp[5].ToString());
            }

            string mss = "Data  Inserted SucessFully";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

        }
        else
        {

        }
        

    }


    public void getagentreference()
    {

        string reference;
        reference = "select   referenceNo    from  EMROPCH  where u_status='" + ddl_Plantname.SelectedItem.Value + "'  and CardCode='" + ViewState["vencode"] + "'  and DocDate='" + ViewState["tdate"] + "'  group by  referenceNo   ";
        con1 = DB.GetConnectionSAP();
        SqlCommand CMDBANKPAY = new SqlCommand(reference, con1);
        SqlDataAdapter dsrr=new SqlDataAdapter(CMDBANKPAY);
        getrefe.Rows.Clear();
        dsrr.Fill(getrefe);
        try
        {
            referenceno = getrefe.Rows[0][0].ToString().Trim();


        }
        catch
        {
            referenceno = "";

        }

    }

    public void getwarehousecode()
    {
        string reference;
     //   string plantcode = ddl_Plantname.SelectedItem.Value;
        reference = "select WHcode from Plant_Master where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'";
        con1 = DB.GetConnectionSAP();
        con = DB.GetConnection();
        SqlCommand CMDBANKPAY = new SqlCommand(reference, con);
        SqlDataAdapter dsrr = new SqlDataAdapter(CMDBANKPAY);
        WARE.Rows.Clear();
        dsrr.Fill(WARE);
        try
        {
            warehousecode = WARE.Rows[0][0].ToString().Trim();
        }
        catch
        {
            warehousecode = "";
        }
    }

    public void update_status_paymentdata(string plantcode,string agentid)
    {
        string reference;
        //string plantcode = ddl_Plantname.SelectedItem.Value;
        reference = "update Paymentdata set Status=1 where Plant_code=" + plantcode + " and Agent_id=" + agentid;
        con1 = DB.GetConnectionSAP();
        con = DB.GetConnection();
        SqlCommand CMD12 = new SqlCommand(reference, con1);
        //CMD12.ExecuteNonQuery();
    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            getdata();
        }
        catch
        {

        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void checkdataexists()
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
        con = DB.GetConnectionSAP();
        string getperiodstr = "";
        getperiodstr = "Select *   from EMROVPM where status='" + ddl_Plantname.SelectedItem.Value + "' AND CreateDate='" + FDATE + "',DOE='" + TODATE + "'";
        SqlCommand cmdperiod = new SqlCommand(getperiodstr, con);
        SqlDataAdapter sall = new SqlDataAdapter(cmdperiod);
        sall.Fill(existstable);
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        checkdataexists();
        if (existstable.Rows.Count > 0)
        {
            getdata();
        }
        else
        {
            string mss = "Data Already  Inserted";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
        }

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
}