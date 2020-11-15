using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using System.Text;
public partial class AgnetExcessAmountReports : System.Web.UI.Page
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
    DataTable dttp = new DataTable();
    string FDATE;
    string TODATE;
    public string frmdate;
    public string Todate;
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
    string d1;
    string d2;
    DateTime d11;
    DateTime d22;
    string agentcode;
    int countinsertdetails;
    string sttr;

    string planttype;
    string planttypehdfc;
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
                    //   txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
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

                ViewState["pcode"] = pcode.ToString();
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
        catch
        {

        }
    }
    public void billdate()
    {
        try
        {

            con.Close();
            con = DB.GetConnection();
            string str;

            str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    d11 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d22 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d11.ToString("dd/MM/yyy");
                    Todate = d22.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);

                }

            }
        }
        catch
        {


        }
    }

    public void loadsingleplant()
    {
        try
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
        catch
        {
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        billBillpaymentDetails();
    }
    public void billBillpaymentDetails()
    {
        try
        {
            GETBILLDATE();
            con = DB.GetConnection();
             string stt = "";
            if (RadioButtonList1.SelectedValue == "1")
            {
             
               
               // stt = " select Agent_id,Addedpaise,Fatkg,ExcesAmt,BillAmount,TotalAmount  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id ) as aa where Bank_id in (15) order by RAND(Agent_id) asc";
                stt = " select Agent_id,Addedpaise,Fatkg,ExcesAmt,BillAmount,TotalAmount  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(SUM(totfat_kg),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id ) as aa where Bank_id in (15) order by RAND(Agent_id) asc";
            }
            if (RadioButtonList1.SelectedValue == "2")
            {
                stt = "select Agent_id,Addedpaise,Fatkg,ExcesAmt,BillAmount,TotalAmount  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(SUM(totfat_kg),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id ) as aa where Bank_id not in (15) order by RAND(Agent_id) asc";
            }
            if (RadioButtonList1.SelectedValue == "3")
            {
                stt = "    select Agent_id,Addedpaise,Fatkg,ExcesAmt,BillAmount,TotalAmount  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id, CONVERT(decimal(18,2),isnull(SUM(totfat_kg),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id ) as aa  order by RAND(Agent_id) asc";
            }
         //   string stt = "select  excessAgent_id as   Agent_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            Session["xportdata"] = DTG.Tables[datasetcount];
            if (DTG.Tables[datasetcount].Rows.Count > 0)
            {
                GridView1.DataSource = DTG.Tables[datasetcount];
                GridView1.DataBind();
                GridView1.FooterRow.Cells[2].Text = "Total Amount";

                decimal milkkg = DTG.Tables[datasetcount].AsEnumerable().Sum(row => row.Field<decimal>("ExcesAmt"));
                GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[4].Text = milkkg.ToString("N2");


                decimal BillAmount = DTG.Tables[datasetcount].AsEnumerable().Sum(row => row.Field<decimal>("BillAmount"));
                GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[5].Text = BillAmount.ToString("N2");

                decimal TotalAmount = DTG.Tables[datasetcount].AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));
                GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[6].Text = TotalAmount.ToString("N2");

                GridView1.FooterRow.Cells[4].Font.Bold = true;
                GridView1.FooterRow.Cells[2].Font.Bold = true;
                GridView1.FooterRow.Cells[5].Font.Bold = true;
                GridView1.FooterRow.Cells[6].Font.Bold = true;

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch
        {

        }
    }
    public void GETBILLDATE()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
        }
        catch
        {

        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Agent Excees Amount Adding   For plant:" + ddl_Plantname.SelectedItem.Text + "Bill Date For:" + ddl_BillDate.SelectedItem.Text;
                HeaderCell2.ColumnSpan = 7;
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
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
          
        }
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }


    public void GETEXPORTGRID()
    {
        GETBILLDATE();
        if (RadioButtonList1.SelectedValue == "1")
        {
            sttr = "select lefthand.Agent_id,Agent_Name,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo ) as aa where Bank_id  in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id";
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
           sttr = "select lefthand.Agent_id,Agent_Name,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo ) as aa where Bank_id not in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id";
        }
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(sttr, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dttp);
        if (dttp.Rows.Count > 0)
        {
            GridView2.DataSource = dttp;
            GridView2.DataBind();

        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
       
        //try
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    string filename = "'" + ddl_Plantname.SelectedItem.Text + "'   " + DateTime.Now.ToString() + ".xls";
        //    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //    // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //        //To Export all pages
        //        GridView2.AllowPaging = false;
        //        GETEXPORTGRID();

        //        GridView2.HeaderRow.BackColor = Color.White;

        //        foreach (TableCell cell in GridView2.HeaderRow.Cells)
        //        {
        //            cell.BackColor = GridView2.HeaderStyle.BackColor;
        //        }
        //        foreach (GridViewRow row in GridView2.Rows)
        //        {
        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
        //                }
        //                else
        //                {
        //                    cell.BackColor = GridView2.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        GridView2.RenderControl(hw);
        //        //style to format numbers to string
        //        string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
        //        // string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();

        //    }
        //}
        //catch (Exception ex)
        //{
        //    WebMsgBox.Show(ex.ToString());
        //}
        if (RadioButtonList1.SelectedItem.Value == "1")
        {
            try
            {
                gethdfcfile();
            }
            catch (Exception ex)
            {

            }
        }
        if (RadioButtonList1.SelectedItem.Value == "2")
        {
            try
            {
                gethdfcfiletoothers();
            }
            catch (Exception ex)
            {

            }

        }


     }


    public void gethdfcfile()
    {
        try
        {

            GETBILLDATE();
            int cc;
            string filename;
            con = DB.GetConnection();
          //  string hdfc = "select Agent_accountNo,LEN(Agent_accountNo),'c' aS  ExcesAmt,CAST(ExcesAmt AS DECIMAL(18,2)) AS AMOUNT,'Milk pay' AS   ExcesAmt   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo ) as aa where Bank_id  in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id ";
            string hdfc = "select Agent_accountNo AS ACCOUNT,'C' aS  C,CAST(ExcesAmt AS DECIMAL(18,2)) AS AMOUNT,'Milk pay' AS   NARRATION   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo ) as aa where Bank_id  in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id ";
            SqlCommand CMD = new SqlCommand(hdfc, con);
            SqlDataAdapter DA = new SqlDataAdapter(CMD);
            DataTable HDFC = new DataTable();
            DA.Fill(HDFC);
            cc = HDFC.Rows.Count;
            GridView5.DataSource = HDFC;
            GridView5.DataBind();

            if ((pcode == "157") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "168"))
            {

                planttype = "SVD1";

                planttypehdfc = "SVD1H";



            }


            else
            {

                planttype = "SVD2";

                planttypehdfc = "SVD2H";


            }

            Response.Clear();
            Response.Buffer = true;

            string dat, mon;
            dat = System.DateTime.Now.ToString("dd");
            mon = System.DateTime.Now.ToString("MM");
            filename = dat + mon;
            string rowcount = string.Empty;

            if (cc < 10)
            {
                rowcount = "00" + cc.ToString();
            }
            else if (cc >= 10 && cc < 100)
            {
                rowcount = "0" + cc.ToString();
            }
            else if (cc >= 100)
            {
                rowcount = cc.ToString();
            }

            // Response.AddHeader("content-disposition", "attachment;filename=bms.009");
            //   Response.AddHeader("content-disposition", "attachment;filename=SVD2H" + filename + "." + rowcount );
            Response.AddHeader("content-disposition", "attachment;filename=" + planttypehdfc + filename + "." + rowcount);
            Response.Charset = "";
            Response.ContentType = "application/text";
            GridView5.AllowPaging = false;
            GridView5.DataBind();
            StringBuilder Rowbind = new StringBuilder();


            int L = GridView5.Columns.Count;
            int M = 1;
            for (int k = 0; k < GridView5.Columns.Count; k++)
            {
                if (M == L)
                {
                    Rowbind.Append(GridView5.Columns[k].HeaderText);
                }
                else
                {
                    Rowbind.Append(GridView5.Columns[k].HeaderText + ',');
                }
                M++;

            }
            Rowbind.Append("\r\n");


            for (int i = 0; i < GridView5.Rows.Count; i++)
            {
                int s = GridView5.Columns.Count;
                int j = 1;
                for (int k = 0; k < GridView5.Columns.Count; k++)
                {
                    if (j == s)
                    {
                        Rowbind.Append(GridView5.Rows[i].Cells[k].Text);
                    }
                    else
                    {
                        Rowbind.Append(GridView5.Rows[i].Cells[k].Text + ',');
                    }
                    j++;

                }

                Rowbind.Append("\r\n");
            }
            Response.Output.Write(Rowbind.ToString());
            Response.Flush();
            Response.End();

        }
        catch
        {


        }
    }
    public void gethdfcfiletoothers()
    {

        try
        {

            if ((pcode == "157") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "168"))
        {

            planttype = "SVD1";

            planttypehdfc = "SVD1H";



        }


        else
        {

            planttype = "SVD2";

            planttypehdfc = "SVD2H";


        }

        GETBILLDATE();
        int cc = 0;
        string filename;
        con = DB.GetConnection();
        //  string hdfc = "select Agent_accountNo,LEN(Agent_accountNo),'c' aS  ExcesAmt,CAST(ExcesAmt AS DECIMAL(18,2)) AS AMOUNT,'Milk pay' AS   ExcesAmt   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo ) as aa where Bank_id  in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id ";
        string hdfc = "select    'N' as TranType, Agent_accountNo AS ACCOUNT,ExcesAmt as  AMOUNT,REPLACE(Agent_Name,'.',' ') AS AgentName,Agent_id,CONVERT(NVARCHAR(30),GETDATE(),103) AS PayDate,Ifsc_code as Ifsccode,Bank_Name as BankName,Pmail  from (select lefthand.Agent_id,Agent_Name,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,excessplantcode   from (select Agent_id,Addedpaise,ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,excessplantcode  from (select  excessAgent_id as   Agent_id,Bank_id,Sum(added_paise) as Addedpaise,Sum(totfat_kg) as Fatkg,Sum(ExcessTotAmount) as ExcesAmt,Bank_Name,Ifsc_code,Agent_accountNo,Sum(netamount) AS BillAmount,SUM(netamount + ExcessTotAmount)  as TotalAmount,excessplantcode from(select plant_code as excessplantcode, Agent_id as excessAgent_id,CONVERT(decimal(18,2),isnull(floor(Sum(totfat_kg)),0)) as totfat_kg, (ISNULL(Sum(added_paise),0))  as  added_paise,convert(decimal(18,2),floor(ISNULL(Sum(TotAmount),0))) as ExcessTotAmount   from AgentExcesAmount  where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  group by agent_id,plant_code ) as exceesamt left join(SELECT  agent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,Plant_code,CONVERT(decimal(18,2),floor(isnull(SUM( netamount),0))) as netamount   FROM  Paymentdata      where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "' and To_date='" + TODATE + "' group by Agent_id,Plant_code,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo)as pay on exceesamt.excessplantcode=pay.Plant_code AND  exceesamt.excessAgent_id=pay.Agent_id   group by  excessAgent_id,Bank_id,Bank_Name,Ifsc_code,Agent_accountNo,excessplantcode ) as aa where Bank_id not in (15) ) as lefthand  left join(Select Agent_Id,Agent_Name   from Agent_Master where  plant_code='" + pcode + "' group by Agent_Id,Agent_Name ) as righthand on lefthand.Agent_id=righthand.Agent_Id) as jj left join  (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Pm on jj.excessplantcode=pm.Plant_Code";
        SqlCommand CMD = new SqlCommand(hdfc, con);
        SqlDataAdapter DA = new SqlDataAdapter(CMD);
        DataTable HDFCothers = new DataTable();
        DA.Fill(HDFCothers);
        cc = HDFCothers.Rows.Count;
        GridView6.DataSource = HDFCothers;
        GridView6.DataBind();
        Response.Clear();
        Response.Buffer = true;

        string dat, mon;
        dat = System.DateTime.Now.ToString("dd");
        mon = System.DateTime.Now.ToString("MM");
        filename = dat + mon;
        string rowcount = string.Empty;

        if (cc < 10)
        {
            rowcount = "00" + cc.ToString();
        }
        else if (cc >= 10 && cc < 100)
        {
            rowcount = "0" + cc.ToString();
        }
        else if (cc >= 100)
        {
            rowcount = cc.ToString();
        }

        // Response.AddHeader("content-disposition", "attachment;filename=bms.009");
        //  Response.AddHeader("content-disposition", "attachment;filename=SVD2" + filename + "." + rowcount);
        Response.AddHeader("content-disposition", "attachment;filename=" + planttype + filename + "." + rowcount);
        Response.Charset = "";
        Response.ContentType = "application/text";
        GridView6.AllowPaging = false;
        GridView6.DataBind();
        StringBuilder Rowbind = new StringBuilder();

        //Add Header in UploadFile
        //int L = GridView6.Columns.Count;
        //int M = 1;
        //for (int k = 0; k < GridView6.Columns.Count; k++)
        //{
        //    if (M == L)
        //    {
        //        Rowbind.Append(GridView6.Columns[k].HeaderText);
        //    }
        //    else
        //    {
        //        Rowbind.Append(GridView6.Columns[k].HeaderText + ',');
        //    }
        //    M++;

        //}
        //Rowbind.Append("\r\n");

        //Add Rows in UploadFile
        for (int i = 0; i < GridView6.Rows.Count; i++)
        {
            int s = GridView6.Columns.Count;
            int j = 1;
            for (int k = 0; k < GridView6.Columns.Count; k++)
            {
                if (j == s)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text);
                }
                else if (j == 1)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                }
                else if (j == 4)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',');
                }
                else if (j == 5)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',' + ',');
                }
                else if (j == 6)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                }
                else if (j == 8)
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',' + ',');
                }
                else
                {
                    Rowbind.Append(GridView6.Rows[i].Cells[k].Text + ',');
                }
                j++;

            }

            Rowbind.Append("\r\n");
        }
        Response.Output.Write(Rowbind.ToString());
        Response.Flush();
        Response.End();
        }
        catch
        {

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
        }

    }
    protected void Button11_Click(object sender, EventArgs e)
    {

        try
        {
            Session["filename"] = "Agent Excess Amount Report";
            Response.Redirect("~/exporttoxl.aspx");
          
        }
        catch
        {


        }
    }
    protected void Button11_Click1(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}