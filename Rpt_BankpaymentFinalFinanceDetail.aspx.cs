using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

public partial class Rpt_BankpaymentFinalFinanceDetail : System.Web.UI.Page
{
    int ccode = 1, pcode;
    string sqlstr = string.Empty;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    SqlCommand cmd = new SqlCommand();
    DateTime dat = new DateTime();

    public string managmobNo;
    public string pname;
    public string cname;

    string d1 = string.Empty;
    string d2 = string.Empty;
    int count, count1, count2;
    public static int roleid;
    int cashisenable = 0;
    string Dates;
    string netamount  ;
    string Name;
    int snocount = 0;

    double totpayamt;
    double bankamt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
             //   managmobNo = Session["managmobNo"].ToString();
                dat = System.DateTime.Now;
                txt_FromDate.Text = dat.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dat.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
              //  pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;               
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
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;
              
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

    }

    private void LoadPlantcode()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void loadsingleplant()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void Datefunc()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();


        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        lbldate.Text = "BillDate From:" + dt1.ToString("dd/MM/yyyy") + "To:" + dt2.ToString("dd/MM/yyyy");

        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");

    }
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
      //  GetFinaceDetail();
        GETBANKPAYMENTAMT();
   
    }

    public void GETBANKPAYMENTAMT()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            lbldate.Text = "BillDate From:" + dt1.ToString("dd/MM/yyyy") + "To:" + dt2.ToString("dd/MM/yyyy");
            d1 = dt1.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");
            con = dbaccess.GetConnection();
            DataSet dtg = new DataSet();

            int cashisenable = 1;
            string str1 = "Select    ISNULL(SUM(NetAmount),0) as Netamount     from   Paymentdata  where plant_code='" + pcode + "'  and Frm_date='" + d1 + "' and To_date='" + d2 + "'   and Payment_mode='bank'  ";
            SqlDataAdapter dft1 = new SqlDataAdapter(str1, con);
            dft1.Fill(dtg, ("BankPayment"));

            string str2 = "Select ISNULL(SUM(NetAmount),0) as Netamount    from   Paymentdata  where plant_code='" + pcode + "'  and Frm_date='" + d1 + "' and To_date='" + d2 + "'   and Payment_mode='cash'  ";
            SqlDataAdapter dft2 = new SqlDataAdapter(str2, con);
            dft2.Fill(dtg, ("cash"));


            string str = "Select ISNULL(SUM(NetAmount),0) as Netamount    from   Paymentdata  where plant_code='" + pcode + "'  and Frm_date='" + d1 + "' and To_date='" + d2 + "' ";
            SqlDataAdapter dft = new SqlDataAdapter(str, con);
            dft.Fill(dtg, ("totamt"));

            string str3 = "Select convert(varchar,Added_Date,103)   AS AddedDate, CONVERT(decimal(18,2),SUM(NetAmount)) as Netamount,BankFileName    from   BankPaymentllotment  where plant_code='" + pcode + "'  and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "'   group by Added_Date,BankFileName  order by Added_Date asc ";
            SqlDataAdapter dft3 = new SqlDataAdapter(str3, con);
            dft3.Fill(dtg, ("Bankfilpayment"));
            DataTable dt = new DataTable();
            dt.Columns.Add("AddedDate");
            dt.Columns.Add("Name");
            dt.Columns.Add("Amount");
            foreach (DataRow totamt in dtg.Tables[0].Rows)
            {
                netamount = totamt[0].ToString();
                Name = "Bank Amount";
                dt.Rows.Add("", Name, netamount);
            }


            foreach (DataRow totamt in dtg.Tables[1].Rows)
            {
                netamount = totamt[0].ToString();
                Name = "Cash Amount";
                dt.Rows.Add("", Name, netamount);

            }
            netamount = "";
            Name = "";

            dt.Rows.Add("", Name, "--------------------");

            foreach (DataRow totamt in dtg.Tables[2].Rows)
            {
                
                Name = "Total Bill Amount";
                netamount = totamt[0].ToString();
                totpayamt = Convert.ToDouble(netamount);
                dt.Rows.Add("", Name, netamount);

               
                 


            }

            dt.Rows.Add("", "", "--------------------");

    

            foreach (DataRow totamt in dtg.Tables[3].Rows)
            {

                string[] n;

                Dates = totamt[0].ToString();
                n = Dates.Split(' ');
                Dates = n[0].ToString();
                Name = totamt[2].ToString();
                netamount = totamt[1].ToString();
                double tempbank = Convert.ToDouble(netamount);
                bankamt = bankamt + tempbank;

                dt.Rows.Add(Dates, Name, netamount);

            }
            string paidamt = bankamt.ToString("f2");
            dt.Rows.Add("", "", "--------------------");
            dt.Rows.Add("", "Paid Amount", paidamt);
            dt.Rows.Add("", "", "--------------------");
            string tempfile = (totpayamt - bankamt).ToString("F2");
            dt.Rows.Add("", "Balance Amount", tempfile);


            FinanceDetails1.DataSource = dt;
            FinanceDetails1.DataBind();


        }
        catch
        {


        }

    }

    private void GetFinaceDetail()
    {      
        try
        {
            Datefunc();
            SqlConnection conn = null;
            DataTable dt = new DataTable();
            using (conn = dbaccess.GetConnection())
            {
                SqlCommand sqlCmd = new SqlCommand("[dbo].[Report_BankpaymentFinalFinanceDetail]");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.ToString().Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.ToString().Trim());

                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                adp.Fill(dt);
                FinanceDetails.DataSource = dt;
                FinanceDetails.DataBind();






            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    protected void FinanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;              
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                if (e.Row.Cells[2].Text == "Bill Amount")
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Gray;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[2].Font.Bold = true; 
                }
                else if (e.Row.Cells[2].Text == "---------------")
                {
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;  
                }
                
            }
        }
        catch (Exception ex)
        {
            //Lbl_Errormsg.Visible = true;
            //Lbl_Errormsg.Text = ex.ToString();
        }
    }
    protected void FinanceDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
              string sth = e.Row.Cells[1].Text  ;

              e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
              e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

              if (sth == "&nbsp;")
              {
                  e.Row.Cells[0].Text = "";

              }
              else
              {
                  snocount =  snocount + 1;
                  e.Row.Cells[0].Text = snocount.ToString();

              }

        }

    }
}