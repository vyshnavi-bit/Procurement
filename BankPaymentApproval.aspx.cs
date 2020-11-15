using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
public partial class BankPaymentApproval : System.Web.UI.Page
{
    DbHelper DB = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
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
    string month;
    double milkfootrows;
    double assignfootrows;
    double Bankpaidfootrows;
    double GETBAKBAL;
    double GETALLOT;
    string filename;
    DataTable getexclbank = new DataTable();
    DataTable finalgetexclbank = new DataTable();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                LoadPlantcode();
                billdate();
                lbl_agentid.Visible=false;
                txt_agentid.Visible=false;
                lbl_ifsc.Visible=false;
                txt_ifsc.Visible=false;
                lbl_accno.Visible=false;
                txt_acno.Visible=false;
                lbl_remark.Visible=false;
                txt_txtremarks.Visible = false;
                btn_excelexport.Visible = false;
                //Label9.Visible = false;
                txt_pending.Visible = false;
                Label11.Visible = false;
                txt_tot.Visible = false;
                Label3.Visible = false;
                txt_bank.Visible = false;
                Label5.Visible = false;
                txt_cashamt.Visible = false;
                Label7.Visible = false;
                txt_allote.Visible = false;
                Master_update.Visible = false;
                Label12.Visible = false;
                Remark_update.Visible = false;
                roleid = Convert.ToInt32(Session["Role"].ToString());
                //getroutename();
        
        //billamount();
        //paymentAmount();
        //Bankuploadsfinalized();
       // completepayments();
        btn_update.Visible =false;
       // Panel1.Visible = false;

        if (roleid == 4)
        {
            Remark_update.Visible = false;
            Master_update.Visible = false;
            btn_update.Visible = false;
        }
  
            }
            else
            {


            }
        }
        else
        {
            if (roleid == 4)
            {
                Remark_update.Visible = false;
                Master_update.Visible = false;
                btn_update.Visible = false;
            }

        }

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        //billdate();

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
        //billamount();
        ////paymentAmount();
        //paymentAmount1();
        //Bankuploadsfinalized();
        //btn_update.Visible = false;
        //completepayments();


        //GridView1.Visible = true;
        //GridView2.Visible = true;
        //GridView3.Visible = true;
        //GridView4.Visible = true;
        
    }
    

   public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master  WHERE PLANT_CODE > 154 group by    Plant_Code, Plant_Name order by Plant_Code asc ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable dtmmm = new DataTable();
            dtmmm.Rows.Clear();
            DA.Fill(dtmmm);
            ddl_Plantname.DataSource = dtmmm;
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
          
        }
        catch
        {

        }

    }
    public void getroutename()
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

            con = DB.GetConnection();
            string getrouteid = "";
            getrouteid = "select pragentid,Route_Name   from (Select Route_id  as pragentid  from Procurement   where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and prdate between '" + FDATE + "' and '" + TODATE + "'   group by Route_id ) as pm left join (Select Route_ID,Route_Name  from Route_Master   where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' group by  Route_ID,Route_Name ) as rm  on pm.pragentid=rm.Route_ID   order by  RAND(pragentid) asc ";
            SqlCommand cmd = new SqlCommand(getrouteid, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable dtmmm = new DataTable();
            dtmmm.Rows.Clear();
            DA.Fill(dtmmm);
            //ddl_Routename.DataSource = dtmmm;
            //ddl_Routename.DataTextField = "Route_Name";
            //ddl_Routename.DataValueField = "pragentid";
            //ddl_Routename.DataBind();
        }
        catch
        {

        }

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  getroutename();
        billdate();
        FILEpayments();
        //GridView1.Visible = false;
        //GridView2.Visible = false;
        //GridView3.Visible = false;
        //GridView4.Visible = false;
    }
    public void billdate()
    {
        try
        {

            con.Close();
            con = DB.GetConnection();
            string str;
          //  ddl_BillDate.Items.Clear();
            str = "select  Bill_frmdate,Bill_todate   from (select  Bill_frmdate ,Bill_todate   from Bill_date   where    PLANT_CODE='"+ddl_Plantname.SelectedItem.Value+"'     group by Bill_frmdate,Bill_todate ) as aff order  by  Bill_frmdate desc ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            ddl_BillDate.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    d11 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d22 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d11.ToString("dd/MM/yyy");
                    Todate = d22.ToString("dd/MM/yyy");
                 //  
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);
                    //string date = ddl_BillDate.Text;
                    //string[] p = date.Split('/', '-');
                    //getvald = p[0];
                    //getvalm = p[1];
                    //getvaly = p[2];
                    //getvaldd = p[3];
                    //getvalmm = p[4];
                    //getvalyy = p[5];
                    //FDATE = getvalm + "/" + getvald + "/" + getvaly;
                    //TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

                }

            }
        }
        catch
        {

        }
    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Milk Amount:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 6;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
              //  GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string getamt = e.Row.Cells[4].Text;
                e.Row.Cells[4].Text = getamt + ".00";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                double tempmilk = Convert.ToDouble(e.Row.Cells[4].Text);
                milkfootrows = milkfootrows + tempmilk;


                string PAYMODE = e.Row.Cells[5].Text;

                if (PAYMODE == "CASH")
                {
                    e.Row.BackColor = System.Drawing.Color.GreenYellow;
                }
                else
                {


                }

                //      GridView1.FooterRow.Cells[4].Text =(milkfootrows).ToString();

            }
        }
        catch
        {

        }
    }
    protected void GridView1_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Plant Milk Amount:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 5;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
               // GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {

        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Alloted Amount:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 5;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
              //  GridView2.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {

        }
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Paid Amount:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 5;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
               // GridView3.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string getamt1 = "";
            getamt1 = e.Row.Cells[4].Text;
            double getamt = Convert.ToDouble(getamt1);
            e.Row.Cells[4].Text = getamt.ToString("f2");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            double ASStempmilk = Convert.ToDouble(e.Row.Cells[4].Text);
            assignfootrows = assignfootrows + ASStempmilk;
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string getamt2 = "";
            getamt2 = e.Row.Cells[4].Text;
            double getamt = Convert.ToDouble(getamt2);
            e.Row.Cells[4].Text = getamt.ToString("f2");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

            double BANKtempmilk = Convert.ToDouble(e.Row.Cells[4].Text);
            Bankpaidfootrows = Bankpaidfootrows + BANKtempmilk;
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {

        if (roleid != 4)
        {

            try
            {
                if (roleid >= 3)
                {
                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            if (chkRow.Checked)
                            {
                                string AGENTS = row.Cells[1].Text;
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
                                string str = "";
                                str = "Update BankPaymentllotment  set FinanceStatus=1,Remarks='Completed'   where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'  and  ((FinanceStatus is null) or  (FinanceStatus=0))  AND AGENT_ID='" + AGENTS + "' ";
                                SqlCommand cmd = new SqlCommand(str, con);
                                cmd.ExecuteNonQuery();

                                //string str1 = "";
                                //if ((txt_ifsc.Text != string.Empty) && (txt_acno.Text != string.Empty))
                                //{
                                //    str1 = "Update agent_master  set Ifsc_code='" + txt_ifsc.Text + "',Agent_AccountNo='" + txt_acno.Text + "'   where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'       AND AGENT_ID='" + AGENTS + "' ";
                                //    SqlCommand cmd1 = new SqlCommand(str1, con);
                                //    cmd1.ExecuteNonQuery();
                                //}
                            }

                            //GridView1.DataBind();
                            //GridView2.DataBind();
                        }

                    }

                }
                else
                {

                }
                string getdata = "";
                con = DB.GetConnection();
                getdata = "sELECT Netamount,bankNetamount,CashNetamount FROM (sELECT * FROM (Select Sum(netamount) as  Netamount,Plant_code   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' group by Plant_code) AS LIST LEFT JOIN (Select Sum(netamount) as  BANKNetamount,Plant_code AS bankpcode   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' AND Payment_mode='BANK' group by Plant_code ) AS  PAYBANK on LIST.Plant_code=PAYBANK.bankpcode) AS LEFF left join (Select Sum(netamount) as  CashNetamount,Plant_code AS cashpcode   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' AND Payment_mode='CASH' group by Plant_code ) AS  CASH ON LEFF.Plant_code=CASH.cashpcode";
                con = DB.GetConnection();
                SqlCommand cmd12 = new SqlCommand(getdata, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd12);
                DataTable getdatadt = new DataTable();
                getdatadt.Rows.Clear();
                da.Fill(getdatadt);
                txt_tot.Text = "0";
                txt_bank.Text = "0";
                txt_cashamt.Text = "0";
                if (getdatadt.Rows.Count > 0)
                {
                    try
                    {
                        txt_tot.Text = getdatadt.Rows[0][0].ToString();
                    }
                    catch
                    {
                        txt_tot.Text = "0";
                    }

                    try
                    {
                        txt_bank.Text = getdatadt.Rows[0][1].ToString();
                    }
                    catch
                    {
                        txt_bank.Text = "0";
                    }

                    try
                    {
                        txt_cashamt.Text = getdatadt.Rows[0][2].ToString();
                    }
                    catch
                    {
                        txt_cashamt.Text = "0";
                    }
                }

                string getdatabakpay = "";
                con = DB.GetConnection();
                getdatabakpay = "sELECT TotalAmount,NetAmount,convert(decimal(18,2),(TotalAmount- NetAmount)) as PendingAmt  FROM (Select Sum(NetAmount) AS  NetAmount,Plant_Code  from BankPaymentllotment where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Billfrmdate='" + FDATE + "'    and   Billtodate='" + TODATE + "' group by Plant_Code ) AS BP    left join (Select Sum(netamount) as  TotalAmount,Plant_code as payplant   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' AND Payment_mode='BANK' group by Plant_code)as pay on BP.Plant_Code=pay.payplant";
                con = DB.GetConnection();
                SqlCommand cmdbakpay = new SqlCommand(getdatabakpay, con);
                SqlDataAdapter dabakpay = new SqlDataAdapter(cmdbakpay);
                DataTable getdatadtbakpay = new DataTable();
                getdatadtbakpay.Rows.Clear();
                dabakpay.Fill(getdatadtbakpay);
                txt_allote.Text = "";
                txt_pending.Text = "";
                GETBAKBAL = 0;
                GETALLOT = 0;
                if (getdatadtbakpay.Rows.Count > 0)
                {
                    try
                    {
                        double altamt = Convert.ToDouble(getdatadtbakpay.Rows[0][1]);
                        txt_allote.Text = altamt.ToString("F2");
                    }
                    catch
                    {
                        txt_allote.Text = "0";
                    }

                    try
                    {
                        txt_pending.Text = getdatadtbakpay.Rows[0][2].ToString();
                    }
                    catch
                    {
                        txt_pending.Text = "0";
                    }

                    try
                    {
                        GETBAKBAL = Convert.ToDouble(txt_bank.Text);
                        GETALLOT = Convert.ToDouble(txt_allote.Text);
                    }
                    catch
                    {
                        txt_pending.Text = "Pending:" + (GETBAKBAL - GETALLOT).ToString();
                    }


                }

                string getagent = "";
                //getagent = "select   Plant_Name,Updatedtime,BankFileName,CONVERT(DECIMAL(18,2),NetAmount) AS  NetAmount   from (SELECT plant_code,Updatedtime,Floor(SUM(NetAmount)) as NetAmount,BankFileName    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  group by Plant_Code,Updatedtime,BankFileName) as pay left join (select Plant_Code,Plant_Name   from Plant_Master  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code  order by Updatedtime desc  ";
                getagent = "Select  Plant_Name,UpdatedTime,BankFileName,NetAmount,Total,isnull((Total-Pending),0) as Completed,isnull(Pending,0) as Pending  from (Select Plant_Name,UpdatedTime,leff.BankFileName,NetAmount,Total,ISNULL(count,0)  as Pending from( select   Plant_Name,pay.Plant_Code,Updatedtime,BankFileName,CONVERT(DECIMAL(18,2),NetAmount) AS  NetAmount,Total   from (SELECT plant_code,Updatedtime,Floor(SUM(NetAmount)) as NetAmount,BankFileName,count(*) as Total    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  group by Plant_Code,Updatedtime,BankFileName ) as pay left join (select Plant_Code,Plant_Name   from Plant_Master where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code    ) as leff left join (SELECT   Plant_Code,BankFileName,count(*) as count    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  and FinanceStatus=0   group by BankFileName,FinanceStatus,Plant_Code having COUNT(FinanceStatus)> 0) AS RIGHTSS ON leff.Plant_Code=RIGHTSS.Plant_Code  AND  leff.BankFileName =RIGHTSS.BankFileName  group by Plant_Name,UpdatedTime,leff.BankFileName,NetAmount,count,Total ) as ffff   order by Updatedtime desc";
                SqlCommand cmd1 = new SqlCommand(getagent, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dtf = new DataTable();
                dtf.Rows.Clear();
                da1.Fill(dtf);
                if (dtf.Rows.Count > 0)
                {
                    GridView1.Visible = true;
                    GridView1.DataSource = dtf;
                    GridView1.DataBind();
                }
                try
                {

                    string getfilename = GridView1.SelectedRow.Cells[2].Text;
                    con = DB.GetConnection();
                    string stt = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND   BankFileName='" + getfilename + "'   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
                    SqlCommand cmdup = new SqlCommand(stt, con);
                    SqlDataAdapter DA = new SqlDataAdapter(cmdup);
                    DataTable getagentlist = new DataTable();
                    getagentlist.Rows.Clear();
                    DA.Fill(getagentlist);
                    if (getagentlist.Rows.Count > 0)
                    {

                        GridView2.DataSource = getagentlist;
                        GridView2.DataBind();


                    }
                }
                catch
                {


                }


                string msg = "Record Updated SuccessFully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");
                //    // completepayments();
                //Bankuploadsfinalized();
                btn_update.Visible = false;
                lbl_agentid.Visible = false;
                txt_agentid.Visible = false;
                lbl_ifsc.Visible = false;
                txt_ifsc.Visible = false;
                lbl_accno.Visible = false;
                txt_acno.Visible = false;
                lbl_remark.Visible = false;
                txt_txtremarks.Visible = false;
            }
            catch
            {


            }
        }
        else
        {
            string msg = "You Have Np Permission";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");

        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string getamt = e.Row.Cells[6].Text;
            double tempval = Convert.ToDouble(getamt);
            e.Row.Cells[6].Text = tempval.ToString("f2");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[7].Text = "";



        }
    }
    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          
          //  txt_agent.Text = GridView4.SelectedRow.Cells[1].Text;
          //  Panel1.Visible = true;
        }
        catch
        {

        }
    }


    protected void btn_remark_Click(object sender, EventArgs e)
    {
       //if((txt_agent.Text!=string.Empty) && ( txt_desc.Text!=string.Empty))
       // {

            ddl_BillDate.Items.Add(frmdate + "-" + Todate);
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

        //con = DB.GetConnection();
        //string str = "";
        //str = "Update BankPaymentllotment  set Remarks='" + txt_desc.Text + "'   where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND AGENT_ID='" + txt_agent.Text + "' ";
        //SqlCommand cmd = new SqlCommand(str, con);
        //cmd.ExecuteNonQuery();
        //Panel1.Visible = false;
        //completepayments();

        //}
        //else
        //{


        //}
    }
    protected void ddl_Routename_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_BillDate_SelectedIndexChanged1(object sender, EventArgs e)
    {
        FILEpayments();
    }
    public void FILEpayments()
    {
        try
        {
            ddl_BillDate.Items.Add(frmdate + "-" + Todate);
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
            string getrouteid = "";
            getrouteid = "Select    BankFileName    from  BankPaymentllotment   WHERE  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  AND Billfrmdate='"+FDATE+"'   AND Billtodate='"+TODATE+"'   group by BankFileName,UpdatedTime  order by UpdatedTime asc";
            SqlCommand cmd = new SqlCommand(getrouteid, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable dtmmm = new DataTable();
            dtmmm.Rows.Clear();
            DA.Fill(dtmmm);
            //ddl_Routename.DataSource = dtmmm;
            //ddl_Routename.DataTextField = "BankFileName";
            //ddl_Routename.DataValueField = "BankFileName";
            //ddl_Routename.DataBind();
        }
        catch
        {

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Label11.Visible = true;
            txt_tot.Visible = true;
            Label3.Visible = true;
            txt_bank.Visible = true;
            Label5.Visible = true;
            txt_cashamt.Visible = true;
            Label7.Visible = true;
            txt_allote.Visible = true;
            //  Label9.Visible = true;
            txt_pending.Visible = true;
            Label12.Visible = true;
            GridView2.Visible = false;
            lbl_agentid.Visible = false;
            txt_agentid.Visible = false;
            lbl_ifsc.Visible = false;
            txt_ifsc.Visible = false;
            lbl_accno.Visible = false;
            txt_acno.Visible = false;
            lbl_remark.Visible = false;
            txt_txtremarks.Visible = false;
            Master_update.Visible = false;
          
            string date = ddl_BillDate.Text;
            ViewState["BILLD"] = date;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            ViewState["FDATE"] = FDATE;
            ViewState["tDATE"] = TODATE;

            string getdata = "";
            con = DB.GetConnection();
            getdata = "sELECT Netamount,bankNetamount,CashNetamount FROM (sELECT * FROM (Select Sum(netamount) as  Netamount,Plant_code   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' group by Plant_code) AS LIST LEFT JOIN (Select Sum(netamount) as  BANKNetamount,Plant_code AS bankpcode   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' AND Payment_mode='BANK' group by Plant_code ) AS  PAYBANK on LIST.Plant_code=PAYBANK.bankpcode) AS LEFF left join (Select Sum(netamount) as  CashNetamount,Plant_code AS cashpcode   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' AND Payment_mode='CASH' group by Plant_code ) AS  CASH ON LEFF.Plant_code=CASH.cashpcode";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(getdata, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable getdatadt = new DataTable();
            getdatadt.Rows.Clear();
            da.Fill(getdatadt);
            txt_tot.Text = "0";
            txt_bank.Text = "0";
            txt_cashamt.Text = "0";
            if (getdatadt.Rows.Count > 0)
            {
                try
                {
                    txt_tot.Text = getdatadt.Rows[0][0].ToString();
                }
                catch
                {
                    txt_tot.Text = "0";
                }

                try
                {
                    txt_bank.Text = getdatadt.Rows[0][1].ToString();
                }
                catch
                {
                    txt_bank.Text = "0";
                }

                try
                {
                    txt_cashamt.Text = getdatadt.Rows[0][2].ToString();

                   if (txt_cashamt.Text==string.Empty)
                   {
                       txt_cashamt.Text = "0";

                   }
	   
                }
                catch
                {
                    txt_cashamt.Text = "0";
                }
            }
            string getdatabakpay = "";
            con = DB.GetConnection();
            getdatabakpay = "sELECT TotalAmount,NetAmount,convert(decimal(18,2),(TotalAmount- NetAmount)) as PendingAmt  FROM (Select Sum(NetAmount) AS  NetAmount,Plant_Code  from BankPaymentllotment where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Billfrmdate='" + FDATE + "'    and   Billtodate='" + TODATE + "' group by Plant_Code ) AS BP    left join (Select Sum(netamount) as  TotalAmount,Plant_code as payplant   from Paymentdata Where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' AND Payment_mode='BANK' group by Plant_code)as pay on BP.Plant_Code=pay.payplant";
            con = DB.GetConnection();
            SqlCommand cmdbakpay = new SqlCommand(getdatabakpay, con);
            SqlDataAdapter dabakpay = new SqlDataAdapter(cmdbakpay);
            DataTable getdatadtbakpay = new DataTable();
            getdatadtbakpay.Rows.Clear();
            dabakpay.Fill(getdatadtbakpay);
            txt_allote.Text = "0";
            txt_pending.Text = "0";
            GETBAKBAL = 0;
            GETALLOT = 0;
            if (getdatadtbakpay.Rows.Count > 0)
            {
                try
                {
                    double altamt = Convert.ToDouble(getdatadtbakpay.Rows[0][1]);
                    txt_allote.Text = altamt.ToString("F2");
                }
                catch
                {
                    txt_allote.Text = "0";
                }
                try
                {
                    txt_pending.Text = getdatadtbakpay.Rows[0][2].ToString();
                }
                catch
                {
                    txt_pending.Text = "0";
                }
                try
                {
                    GETBAKBAL = Convert.ToDouble(txt_bank.Text);
                    GETALLOT = Convert.ToDouble(txt_allote.Text);
                    txt_pending.Text = "Pending:" + (GETBAKBAL - GETALLOT).ToString("F2");
                }
                catch
                {


                    txt_pending.Text = "Pending:" + 0;
                }


            }

            string getagent = "";
            //getagent = "select   Plant_Name,Updatedtime,BankFileName,CONVERT(DECIMAL(18,2),NetAmount) AS  NetAmount   from (SELECT plant_code,Updatedtime,Floor(SUM(NetAmount)) as NetAmount,BankFileName    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  group by Plant_Code,Updatedtime,BankFileName) as pay left join (select Plant_Code,Plant_Name   from Plant_Master  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code  order by Updatedtime desc  ";
            //    getagent = "Select Plant_Name,UpdatedTime,leff.BankFileName,NetAmount,count as Pending from( select   Plant_Name,pay.Plant_Code,Updatedtime,BankFileName,CONVERT(DECIMAL(18,2),NetAmount) AS  NetAmount   from (SELECT plant_code,Updatedtime,Floor(SUM(NetAmount)) as NetAmount,BankFileName    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  group by Plant_Code,Updatedtime,BankFileName ) as pay left join (select Plant_Code,Plant_Name   from Plant_Master where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code    ) as leff left join (SELECT   Plant_Code,BankFileName,count(*) as count    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  and FinanceStatus=0   group by BankFileName,FinanceStatus,Plant_Code having COUNT(FinanceStatus)> 0) AS RIGHTSS ON leff.Plant_Code=RIGHTSS.Plant_Code  AND  leff.BankFileName =RIGHTSS.BankFileName  group by Plant_Name,UpdatedTime,leff.BankFileName,NetAmount,count  order by Updatedtime desc ";

            getagent = "Select  Plant_Name,UpdatedTime,BankFileName,NetAmount,Total,isnull((Total-Pending),0) as Completed,isnull(Pending,0) as Pending  from (Select Plant_Name,UpdatedTime,leff.BankFileName,NetAmount,Total,ISNULL(count,0)  as Pending from( select   Plant_Name,pay.Plant_Code,Updatedtime,BankFileName,CONVERT(DECIMAL(18,2),NetAmount) AS  NetAmount,Total   from (SELECT plant_code,Updatedtime,Floor(SUM(NetAmount)) as NetAmount,BankFileName,count(*) as Total    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  group by Plant_Code,Updatedtime,BankFileName ) as pay left join (select Plant_Code,Plant_Name   from Plant_Master where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by Plant_Code,Plant_Name )  as pm on pay.Plant_Code=pm.Plant_Code    ) as leff left join (SELECT   Plant_Code,BankFileName,count(*) as count    FROM BankPaymentllotment  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and  Billfrmdate='" + FDATE + "' and Billtodate='" + TODATE + "'  and FinanceStatus=0   group by BankFileName,FinanceStatus,Plant_Code having COUNT(FinanceStatus)> 0) AS RIGHTSS ON leff.Plant_Code=RIGHTSS.Plant_Code  AND  leff.BankFileName =RIGHTSS.BankFileName  group by Plant_Name,UpdatedTime,leff.BankFileName,NetAmount,count,Total ) as ffff   order by Updatedtime desc";
            SqlCommand cmd1 = new SqlCommand(getagent, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dtf = new DataTable();
            dtf.Rows.Clear();
            da1.Fill(dtf);
            if (dtf.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = dtf;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource ="";
                GridView1.DataBind();
            }
        }
        catch
        {



        }
    }
    private void GridView1_CellContentClick(object sender, EventArgs e)
    {

      
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

         
            lbl_agentid.Visible = false;
            txt_agentid.Visible = false;
            lbl_ifsc.Visible = false;
            txt_ifsc.Visible = false;
            lbl_accno.Visible = false;
            txt_acno.Visible = false;
            lbl_remark.Visible = false;
            txt_txtremarks.Visible = false;
            Master_update.Visible = false;
            GridView2.Visible = true;
            string date = ddl_BillDate.Text;
            ViewState["BILLD"] = date;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            ViewState["FDATE"] = FDATE;
            ViewState["tDATE"] = TODATE;
            string getfilename = GridView1.SelectedRow.Cells[3].Text.TrimStart();
            con = DB.GetConnection();
            string stt = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND   BankFileName='" + getfilename.TrimEnd() + "'   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable getagentlist = new DataTable();
            getagentlist.Rows.Clear();
            DA.Fill(getagentlist);
            if (getagentlist.Rows.Count > 0)
            {

                GridView2.DataSource = getagentlist;
                GridView2.DataBind();
                btn_update.Visible = true;

            }
            else
            {
                GridView2.DataSource = "";
                GridView2.DataBind();

            }


            if (roleid == 4)
            {
                Remark_update.Visible = false;
                Master_update.Visible = false;
                btn_update.Visible = false;
            }


        }
        catch
        {

        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lbl_agentid.Visible = true;
            txt_agentid.Visible = true;
            lbl_ifsc.Visible = true;
            txt_ifsc.Visible = true;
            lbl_accno.Visible = true;
            txt_acno.Visible = true;
            lbl_remark.Visible = true;
            txt_txtremarks.Visible = true;
            Master_update.Visible = true;
            Remark_update.Visible = true;
            txt_agentid.Text = GridView2.SelectedRow.Cells[1].Text;
            txt_ifsc.Text = GridView2.SelectedRow.Cells[4].Text;
            txt_acno.Text = GridView2.SelectedRow.Cells[5].Text;
            txt_txtremarks.Text = "";


            if (roleid == 4)
            {
                Remark_update.Visible = false;
                Master_update.Visible = false;
                btn_update.Visible = false;
            }
        }
        catch
        {

        }
    }

    protected void Master_update_Click(object sender, EventArgs e)
    {


        if (roleid != 4)
        {

            try
            {
                btn_excelexport.Visible = true;
                string date = ddl_BillDate.Text;
                ViewState["BILLD"] = date;
                string[] p = date.Split('/', '-');
                getvald = p[0];
                getvalm = p[1];
                getvaly = p[2];
                getvaldd = p[3];
                getvalmm = p[4];
                getvalyy = p[5];
                FDATE = getvalm + "/" + getvald + "/" + getvaly;
                TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
                ViewState["FDATE"] = FDATE;
                ViewState["tDATE"] = TODATE;
                txt_agentid.Text = GridView2.SelectedRow.Cells[1].Text;
                con = DB.GetConnection();
                string str = "";
                if ((txt_ifsc.Text != string.Empty) && (txt_acno.Text != string.Empty))
                {
                    str = "Update BankPaymentllotment  set  ifsccode='" + txt_ifsc.Text + "',account_no='" + txt_acno.Text + "'  where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'  and FinanceStatus='0'  AND AGENT_ID='" + txt_agentid.Text + "' ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                }
                string str1 = "";
                if ((txt_ifsc.Text != string.Empty) && (txt_acno.Text != string.Empty))
                {
                    str1 = "Update agent_master  set Ifsc_code='" + txt_ifsc.Text + "',Agent_AccountNo='" + txt_acno.Text + "'   where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'       AND AGENT_ID='" + txt_agentid.Text + "' ";
                    SqlCommand cmd1 = new SqlCommand(str1, con);
                    cmd1.ExecuteNonQuery();
                }
                string msg = "Record Updated SuccessFully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");
            }
            catch
            {


            }


            //string date12 = ddl_BillDate.Text;
            //ViewState["BILLD"] = date12;
            //string[] p1 = date12.Split('/', '-');
            //getvald = p[0];
            //getvalm = p[1];
            //getvaly = p[2];
            //getvaldd = p[3];
            //getvalmm = p[4];
            //getvalyy = p[5];
            //FDATE = getvalm + "/" + getvald + "/" + getvaly;
            //TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            //ViewState["FDATE"] = FDATE;
            //ViewState["tDATE"] = TODATE;
            //   GridView2.DataSource = getagentlist;
            try
            {
                GridView1.DataBind();
                string getfilename = GridView1.SelectedRow.Cells[2].Text;
                con = DB.GetConnection();
                string stt = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND   BankFileName='" + getfilename + "'   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
                SqlCommand cmd44 = new SqlCommand(stt, con);
                SqlDataAdapter DA = new SqlDataAdapter(cmd44);
                DataTable getagentlist = new DataTable();
                getagentlist.Rows.Clear();
                DA.Fill(getagentlist);
                if (getagentlist.Rows.Count > 0)
                {

                    GridView2.DataSource = getagentlist;
                    GridView2.DataBind();
                    btn_update.Visible = true;

                }
            }
            catch
            {

            }
        }


        else
        {
            string msg = "You Have Np Permission";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");

        }
    }


    protected void Total_Click(object sender, EventArgs e)
    {
        try
        {
            btn_excelexport.Visible = true;
            getdateformat();
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string filename   = grdrow.Cells[2].Text;
            con = DB.GetConnection();
            string stt = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND   BankFileName='" + filename + "'   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable getagentlist = new DataTable();
            getagentlist.Rows.Clear();
            DA.Fill(getagentlist);
            if (getagentlist.Rows.Count > 0)
            {

                GridView2.DataSource = getagentlist;
                GridView2.DataBind();
                btn_update.Visible = true;
                GridView2.Visible = true;

            }
            else
            {
                btn_update.Visible = false;
                GridView2.Visible = false;

            }

            lbl_agentid.Visible=false;
            txt_agentid.Visible=false;
            lbl_ifsc.Visible=false;
            txt_ifsc.Visible=false;
            lbl_accno.Visible=false;
            txt_acno.Visible=false;
            lbl_remark.Visible=false;
            txt_txtremarks.Visible=false;
            Master_update.Visible = false;


        }
        catch (Exception ex)
        {

        }
    }

    protected void Completed_Click(object sender, EventArgs e)
    {
        //try
        //{
            //GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            //pcode1 = grdrow.Cells[13].Text;
            //Get_RouteMilkStatus(ccode, pcode1);
        btn_excelexport.Visible = true;
            try
            {
                getdateformat();
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string filename = grdrow.Cells[2].Text;
                con = DB.GetConnection();
                string stt = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND   BankFileName='" + filename + "' and  FinanceStatus='1'   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
                SqlCommand cmd = new SqlCommand(stt, con);
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataTable getagentlist = new DataTable();
                getagentlist.Rows.Clear();
                DA.Fill(getagentlist);
                if (getagentlist.Rows.Count > 0)
                {

                    GridView2.DataSource = getagentlist;
                    GridView2.DataBind();
                    btn_update.Visible = true;
                    GridView2.Visible = true;

                }
                else
                {
                    btn_update.Visible = false;
                    GridView2.Visible = false;

                }


                lbl_agentid.Visible = false;
                txt_agentid.Visible = false;
                lbl_ifsc.Visible = false;
                txt_ifsc.Visible = false;
                lbl_accno.Visible = false;
                txt_acno.Visible = false;
                lbl_remark.Visible = false;
                txt_txtremarks.Visible = false;
                Master_update.Visible = false;

            }
            catch (Exception ex)
            {

            }


        //}
        //catch (Exception ex)
        //{

        //}
    }

    protected void Pending_Click(object sender, EventArgs e)
    {
        try
        {
            btn_excelexport.Visible = true;

            try
            {
                getdateformat();
                GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                string filename = grdrow.Cells[2].Text;
                con = DB.GetConnection();
                string stt = "select   Agent_Id,Agent_Name,Bank_Name,Ifsccode,Account_no,convert(decimal(18,2),NetAmount) as NetAmount,Remarks  from (select *  from (SELECT *   FROM (Select       Agent_Id as bankagent,Agent_Name,BANK_ID as bankid,Ifsccode,Account_no,NetAmount,Plant_Code,Remarks    from  BankPaymentllotment where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND   BankFileName='" + filename + "' and    ((FinanceStatus='0')  or (FinanceStatus is null))   ) AS NEWS LEFT JOIN (SELECT Bank_id,Bank_Name   FROM Bank_Details GROUP BY  Bank_id,Bank_Name) AS BANK  ON NEWS.bankid= BANK.Bank_id) as news   left join (Select Agent_Id,Route_id   from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by  Agent_Id,Route_id ) as news1 on news.bankagent=news1.Agent_Id  ) as ff where Agent_Id is not null";
                SqlCommand cmd = new SqlCommand(stt, con);
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataTable getagentlist = new DataTable();
                getagentlist.Rows.Clear();
                DA.Fill(getagentlist);
                if (getagentlist.Rows.Count > 0)
                {

                    GridView2.DataSource = getagentlist;
                    GridView2.DataBind();
                    btn_update.Visible = true;
                    GridView2.Visible = true;

                }
                else
                {
                    btn_update.Visible = false;
                    GridView2.Visible = false;

                }


                lbl_agentid.Visible = false;
                txt_agentid.Visible = false;
                lbl_ifsc.Visible = false;
                txt_ifsc.Visible = false;
                lbl_accno.Visible = false;
                txt_acno.Visible = false;
                lbl_remark.Visible = false;
                txt_txtremarks.Visible = false;
                Master_update.Visible = false;

            }
            catch (Exception ex)
            {

            }




        }
        catch (Exception ex)
        {

        }
    }


    public void getdateformat()
    {
        try
        {
            string date = ddl_BillDate.Text;
            ViewState["BILLD"] = date;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            ViewState["FDATE"] = FDATE;
            ViewState["tDATE"] = TODATE;
        }
        catch
        {

        }

    }
    protected void Remark_update_Click(object sender, EventArgs e)
    {

        if (roleid != 4)
        {

            string date = ddl_BillDate.Text;
            ViewState["BILLD"] = date;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            ViewState["FDATE"] = FDATE;
            ViewState["tDATE"] = TODATE;
            txt_agentid.Text = GridView2.SelectedRow.Cells[1].Text;
            con = DB.GetConnection();
            string str = "";
            if (txt_txtremarks.Text != string.Empty)
            {
                str = "Update BankPaymentllotment  set Remarks='" + txt_txtremarks.Text + "'   where     Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and    Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'  and FinanceStatus=0  AND AGENT_ID='" + txt_agentid.Text + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                string msg = "Record Updated SuccessFully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");
            }
            else
            {

                string msg = "Please Check Your Data";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");
            }

        }
        else
        {
            string msg = "You Have Np Permission";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");

        }

       
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void btn_excelexport_Click(object sender, EventArgs e)
    {

        if (roleid != 4)
        {

            finalgetexclbank.Rows.Clear();
            finalgetexclbank.Columns.Clear();
            finalgetexclbank.Columns.Add("AgentId");
            finalgetexclbank.Columns.Add("AgentName");
            finalgetexclbank.Columns.Add("Ifsccode");
            finalgetexclbank.Columns.Add("Accountno");
            finalgetexclbank.Columns.Add("NetAmount");
            foreach (GridViewRow row in GridView2.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string AGENTS = row.Cells[1].Text;
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
                        //  string str = "Select Agent_Id,Agent_Name,Ifsccode,Account_no,(NetAmount+excessTotAmount) as NetAmount  from (Select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount from BankPaymentllotment where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Billfrmdate='" + FDATE + "'    and   Billtodate='" + TODATE + "'  and Agent_Id='" + AGENTS + "') as ff left join  (Select   Agent_Id as ExcessAgent,isnull(Sum(floor(TotAmount)),0) as excessTotAmount   from AgentExcesAmount where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Frm_date='" + FDATE + "'    and   To_date='" + TODATE + "'  and Agent_Id='" + AGENTS + "' group by Agent_Id) as  excess on ff.Agent_Id=excess.ExcessAgent";
                        string str = "sELECT Agent_Id,Agent_Name,Ifsccode,Account_no,(NetAmount + EXNetAmount) AS NetAmount   FROM (Select Agent_Id,Agent_Name,Ifsccode,Account_no,ISNULL(NetAmount,0) AS NetAmount,ISNULL(excessTotAmount,0) AS EXNetAmount  from (Select  Agent_Id,Agent_Name,Ifsccode,Account_no,NetAmount from BankPaymentllotment where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Billfrmdate='" + FDATE + "'    and   Billtodate='" + TODATE + "'  and Agent_Id='" + AGENTS + "') as ff left join  (Select   Agent_Id as ExcessAgent,isnull(Sum(floor(TotAmount)),0) as excessTotAmount   from AgentExcesAmount where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   and Frm_date='" + FDATE + "'    and   To_date='" + TODATE + "'  and Agent_Id='" + AGENTS + "'  group by Agent_Id) as  excess on ff.Agent_Id=excess.ExcessAgent) AS GGG";
                        SqlCommand cmd = new SqlCommand(str, con);
                        SqlDataAdapter dv = new SqlDataAdapter(cmd);
                        getexclbank.Rows.Clear();
                        dv.Fill(getexclbank);
                        if (getexclbank.Rows.Count > 0)
                        {
                            string AgentId = getexclbank.Rows[0][0].ToString();
                            string AgentName = getexclbank.Rows[0][1].ToString();
                            string Ifsccode = getexclbank.Rows[0][2].ToString();
                            string Accountno = getexclbank.Rows[0][3].ToString();
                            string NetAmount = getexclbank.Rows[0][4].ToString();
                            finalgetexclbank.Rows.Add(AgentId, AgentName, Ifsccode, Accountno, NetAmount);

                        }



                    }
                }
            }
            if (getexclbank.Rows.Count > 0)
            {
                GridView3.DataSource = finalgetexclbank;
                GridView3.DataBind();
                GridView3.Visible = true;

            }
            else
            {
                GridView3.Visible = false;
            }

            try
            {
                Response.Clear();
                Response.Buffer = true;
                string filename = "'" + ddl_Plantname.SelectedItem.Text + "'   " + DateTime.Now.ToString() + ".xls";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //To Export all pages
                    //   GridView3.AllowPaging = false;
                    //  GETEXPORTGRID();

                    GridView3.HeaderRow.BackColor = Color.White;

                    foreach (TableCell cell in GridView3.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView3.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow excerow in GridView3.Rows)
                    {
                        excerow.BackColor = Color.White;
                        foreach (TableCell cell in excerow.Cells)
                        {
                            if (excerow.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView3.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView3.RenderControl(hw);
                    //style to format numbers to string
                    string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                    // string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.ToString());
            }
        }
        else
        {
            string msg = "You Have Np Permission";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(msg) + "')</script>");

        }

        
    }
  
}

