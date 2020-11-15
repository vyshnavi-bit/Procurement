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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class Dpupaymentapproval : System.Web.UI.Page
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
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable dateviews = new DataTable();
    DataTable filename = new DataTable();
    DataTable viewloanrecovery = new DataTable();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;

    string getvald1;
    string getvalm1;
    string getvaly1;

    string jvdatest;

    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    string getid;
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
                    GridView3.Visible = false;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    billdate();
                    //getii();
                    //gertfilename();
                    //getreport();

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
    public void billdate()
    {
        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str;
            if (roleid == 4)
            {
                str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  and Adminupdate=1  order by  Bill_frmdate desc";
            }
            else
            {

                str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  order by  Bill_frmdate desc";

            }
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
    public void getdatefuntion()
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


    }
    protected void btn_get_Click(object sender, EventArgs e)
    {
        LoadUploadedFilesDetails();
        GridView3.Visible = false;
    }
    private void LoadUploadedFilesDetails()
    {
        try
        {
            using (con = DB.GetConnection())
            {
                getdatefuntion();
                DataTable dt = new DataTable();
                string str = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY BankFileName) AS serial_no,COUNT(BankFileName) AS ActualNoofROws,UPPER(BankFileName) AS BankFileName,CONVERT(DECIMAL(18,2),SUM(NetAmount)) AS TotalAmount  FROM DpuBankPaymentllotment Where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Billfrmdate='" + FDATE.ToString().Trim() + "' Group By BankFileName ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();


                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    getdatefuntion();
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + ":Bill Period" + FDATE + "To:" + TODATE + "'.xls");
        //    Response.ContentType = "application/excel";
        //    System.IO.StringWriter sw = new System.IO.StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    GridView3.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.End();



        //}
        //catch
        //{


        //}


        try
        {

            Response.Clear();
            Response.Buffer = true;
            //string filename = "'" + ddl_Plantname.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";
            getdatefuntion();
            //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + ":Bill Period" + FDATE + "To:" + TODATE + "'.xls");
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView3.AllowPaging = false;
                LoadUploadedFilesDetails();

                GridView3.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView3.HeaderRow.Cells)
                {
                    cell.BackColor = GridView3.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView3.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
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
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Dpu Milk Payment Reports:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 7;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView2.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {


        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView3.Visible = true;
        //     Button2.Visible = true;

        getid = (GridView2.SelectedRow.Cells[2].Text).ToString();

        getgrid();

    }


    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //ViewState["agentid"] = GridView2.SelectedRow.Cells[1].Text;
            //ViewState["ifsc"] = GridView2.SelectedRow.Cells[3].Text;
            //ViewState["acno"] = GridView2.SelectedRow.Cells[2].Text;
        }
        catch
        {

        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GridView3.Rows)
        {

            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    string AGENTS = row.Cells[1].Text;

                    string agentid = row.Cells[0].Text;
                    string ifsc = row.Cells[3].Text;
                    string acno = row.Cells[2].Text;
                    DateTime DT = DateTime.Now;

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

                    string str = "UPDATE DpuBankPaymentllotment SET status='A', UpdatedUserName='" + Session["Name"].ToString() + "', UpdatedTime='" + DT + "' WHERE Agent_Id='" + agentid + "' AND Billfrmdate='" + FDATE + "' AND  Billtodate='" + TODATE + "' ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        string msg = "Record Updated SuccessFully";
        lblmsg.Text = msg;
    }

    public void getgrid()
    {

        //DateTime dt1 = new DateTime();
        //DateTime dt2 = new DateTime();
        //dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        //d1 = dt1.ToString("MM/dd/yyyy");
        //d2 = dt2.ToString("MM/dd/yyyy");
        getdatefuntion();
        string get;
        con = DB.GetConnection();
        get = "SELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,NETAMOUNT,BANK_NAME,status    FROM (sELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,CONVERT(DECIMAL(18,2),NETAMOUNT) AS NETAMOUNT,BANK_ID, status  FROM  DpuBankPaymentllotment    WHERE PLANT_CODE='" + ddl_Plantname.SelectedItem.Value + "'  AND   BANKFILENAME='" + getid + "'     AND  billfrmdate='" + FDATE + "'  and billtodate='" + TODATE + "') AS BANK  LEFT JOIN (sELECT  BANK_NAME,BANK_ID,IFSC_CODE  FROM  BANK_MASTER    WHERE PLANT_CODE='" + ddl_Plantname.SelectedItem.Value + "' GROUP BY  BANK_NAME,BANK_ID,IFSC_CODE    ) AS GG ON  BANK.BANK_ID=GG.BANK_ID AND BANK. IFSCCODE =GG.IFSC_CODE";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dt.Rows.Clear();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            GridView3.DataSource = dt;
            GridView3.DataBind();
            //GridView3.FooterRow.Cells[4].Text = "TOTAL AMOUNT";
            //decimal milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("NETAMOUNT"));
            //GridView3.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            //GridView3.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            //GridView3.FooterRow.Cells[5].Text = milkkg.ToString("N2");
        }

        else
        {

            GridView3.DataSource = null;
            GridView3.DataBind();
        }

        LoadUploadedFilesDetails();

    }
   

}