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

public partial class RouteWisePaymentDetails : System.Web.UI.Page
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
    DbHelper db = new DbHelper();
    int I;
    string getplant;
    string getAgent;
    string Date;
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    DateTime d1;
    DateTime d2;
    string DATE;
    string DATE1;
    string DATE2; // JAN

    string BANKPAY;
    string cASHPAY;
    string TOTPAY;
    double cashying;

    double getbankam2;
    double getcasham2 ;
    double gettot2;
    double getpaid2;
    double getbalance2;

    double getbankpay;
    double getpaidamt;
    double bankingbalance;
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
                    //    GETGRID();
                    GridView1.Visible = false;

                    if (roleid < 3)
                    {
                        loadsingleplant();
                        
                    
                        //lad.Visible = false;
                    }

                    else
                    {

                        LoadPlantcode();
                        //Button5.Visible = false;
                     
                       
                    }

                    billdate();
                 //   billdate();

                }
                else
                {



                }
               // billdate();
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;

          //     billdate();
                if (roleid > 2)
                {
                    
                   
                    
                }
                else
                {
                  
                   
                    
                }
            }
        }

        catch
        {



        }
    }


    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }


    public void loadsingleplant()
    {
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();
    }

    public void billdate()
    {
        try
        {

          //  ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
           string str;
            //if (roleid > 2)
            //{

            //   str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";
            //  //  str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  and plant_code='" + pcode + "'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

            //}
            //else
            //{
           str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";
      //          str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  and plant_code='" + pcode + "'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";

            //}

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());


                    //FDATE = d1.ToString("MM/dd/yyyy");
                    //TODATE = d2.ToString("MM/dd/yyyy");
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
    protected void Button5_Click(object sender, EventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        billdate();
        getgridview();
        GridView1.Visible = false;
       
    }

    public void getgridview()
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

        string getgrid = "";

        getgrid = "Select      bankcash.Route_ID,Route_Name,Netamount,ISNULL(cashNatamount,0) as CashAmount,ISNULL((Netamount + cashNatamount),0) as Total,0 as PaidAmout,0 as BalanceAmount     from (select  bank.Route_id,Netamount, ISNULL(cashNatamount,0) AS cashNatamount    from ( select Route_id,ISNULL(Sum(NetAmount),0) as Netamount  from Paymentdata  where  Plant_Code='" + pcode + "'  and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  and Payment_mode='bank'  group by Route_id  ) as bank left join (select Route_id,isnull(CONVERT(decimal(18,2),Sum(NetAmount)),0) as cashNatamount  from Paymentdata  where Plant_Code='" + pcode + "'  and Frm_date='" + FDATE + "' and To_date='" + TODATE + "'  and Payment_mode='cash'  group by Route_id) as cash on bank.Route_id=cash.Route_id) as bankcash left join(select Route_ID,Route_Name   from Route_Master  where Plant_Code='" + pcode + "')as rm on bankcash.Route_id=rm.Route_ID  order by  bankcash.Route_ID  asc  "; 

        con = DB.GetConnection();
        SqlCommand sc = new SqlCommand(getgrid, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        da1.Fill(ds, "TABLEop");
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
           

            //GridView2.Columns[5].HeaderText = "Payed Amount";
            //GridView2.Columns[6].HeaderText = "Balance Amount";


            GridView2.FooterRow.Cells[3].Text = (getbankam2 / 2).ToString("f2");
            GridView2.FooterRow.Cells[4].Text = (getcasham2 / 2).ToString("f2");
            GridView2.FooterRow.Cells[5].Text = (gettot2 / 2).ToString("f2");
            GridView2.FooterRow.Cells[6].Text = (getpaid2 / 2) .ToString("f2");
            GridView2.FooterRow.Cells[7].Text = (getbalance2 / 2).ToString("f2");

            GridView2.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;

            GridView2.FooterRow.Cells[3].Font.Bold = true;
            GridView2.FooterRow.Cells[4].Font.Bold = true;
            GridView2.FooterRow.Cells[5].Font.Bold = true;
            GridView2.FooterRow.Cells[6].Font.Bold = true;
            GridView2.FooterRow.Cells[7].Font.Bold = true;
      
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

        }




    }

    public void gettotamout()
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

        //ViewState["FDATE"] = FDATE;
        //ViewState["TODATE"] = TODATE;
        //ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
        //ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
        string STTY = "";
        con = DB.GetConnection();
        STTY = "select totnetamount,banknetamount,cashnetamount,0 as  cashnetamount   from ( select totnetamount,banknetamount,totplantcode  from (SELECT ISNULL(SUM(NetAmount),0) as totnetamount,Plant_code as totplantcode FROM Paymentdata Where Plant_code='" + pcode + "' AND   Frm_date='" + FDATE + "' AND To_date='" + TODATE + "'  group by Plant_code   ) as tot left join (SELECT  ISNULL(SUM(NetAmount),0) as banknetamount,Plant_code as bankplant FROM Paymentdata Where Plant_code='" + pcode + "' AND   Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' and Payment_mode='bank' group by Plant_code ) as bank on tot.totplantcode=bank.bankplant) as totbank left join (SELECT  ISNULL(SUM(NetAmount),0) as cashnetamount,plant_code as cashplantcode FROM Paymentdata Where Plant_code='" + pcode + "' AND   Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' and Payment_mode='CASH'  group by Plant_code) as cash on totbank.totplantcode=cash.cashplantcode";
        SqlCommand cmd = new SqlCommand(STTY, con);
        DataTable dttt = new DataTable();
        SqlDataAdapter dss = new SqlDataAdapter(cmd);
        dss.Fill(dttt);

        TOTPAY = dttt.Rows[0][0].ToString();
        BANKPAY = dttt.Rows[0][1].ToString();
        cASHPAY = dttt.Rows[0][2].ToString();

        Session["TOTttPAY"] = TOTPAY;
        Session["BANKttPAY"] = BANKPAY;
        Session["cASHttPAY"] = cASHPAY;

        if (cASHPAY == string.Empty)
        {

            cASHPAY = "0.0";

        }

    }
    catch
    {

    }


}

    //protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    gettotamout();
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell HeaderCell2 = new TableCell();
    //        HeaderCell2.Text = "Plant Name" + ddl_Plantname.SelectedItem.Text + " Total Bill Amount:" + TOTPAY + "Bank Amount:" + BANKPAY + "Cash Amount:" + cASHPAY;
    //        HeaderCell2.ColumnSpan = 21;
    //        HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
    //        HeaderRow.Cells.Add(HeaderCell2);
    //        GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

    //    }

    //}
   


    //public void getpaymentamount()
    //{
       

    //    try
    //    {

    //        string date = ddl_BillDate.Text;
    //        string[] p = date.Split('/', '-');
    //        getvald = p[0];
    //        getvalm = p[1];
    //        getvaly = p[2];
    //        getvaldd = p[3];
    //        getvalmm = p[4];
    //        getvalyy = p[5];
    //        FDATE = getvalm + "/" + getvald + "/" + getvaly;
    //        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

    //        //ViewState["FDATE"] = FDATE;
    //        //ViewState["TODATE"] = TODATE;
    //        //ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
    //        //ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
        
    //        con = DB.GetConnection();
    //        string getiingpayedamt;
    //        getiingpayedamt = "select   sum(NetAmount) as Netamount    from BankPaymentllotment    where Plant_Code='155'   and Billfrmdate='9-1-2016' and  Billtodate='9-10-2016'   and Agent_Id >= 900 and   Agent_Id < =999";
    //        SqlCommand cmd = new SqlCommand(getiingpayedamt, con);
    //        DataTable dttth = new DataTable();
    //        SqlDataAdapter dssd = new SqlDataAdapter(cmd);
    //        dssd.Fill(dttth);


    //    }
    //    catch
    //    {

    //    }


    //}

    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Sample.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        ////Set AllowPaginf false to export the full data
        
        //GridView2.AllowPaging = false;
        //getgridview();
        ////GridView2.DataBind();
        ////Start the rendering of control here
        //GridView2.RenderBeginTag(hw);
        //GridView2.HeaderRow.RenderControl(hw);
        //foreach (GridViewRow row in GridView2.Rows)
        //{
        //    row.RenderControl(hw);
        //}
        //GridView2.FooterRow.RenderControl(hw);
        //GridView2.RenderEndTag(hw);
        ////Apply some style settimgs
        //GridView2.Caption = "Your caption";
        //GridView2.Style.Add("width", "400px");
        //GridView2.HeaderRow.Style.Add("font-size", "12px");
        //GridView2.HeaderRow.Style.Add("font-weight", "bold");
        //GridView2.Style.Add("border", "1px solid black");
        //GridView2.Style.Add("text-decoration", "none");
        //GridView2.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        //GridView2.Style.Add("font-size", "8px");
        //StringReader sr = new StringReader(sw.ToString());
        ////creating new pdf document
        //Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        ////PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        ////pdfDoc.Open();
        ////htmlparser.Parse(sr);
        ////pdfDoc.Close();
        ////Response.Write(pdfDoc);
        ////Response.End();
        ////Response.Clear();



        //Response.Clear();
        //Response.ClearContent();
        //Response.ClearHeaders();
        //////Response.AddHeader("content-disposition", "attachment;filename=POSA.xls");
        //Response.AddHeader("content-disposition", "attachment;filename=Interview_Evaluation.pdf");
        //Response.ContentType = "application/pdf";
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        //news.RenderControl(htmlWrite);
        //System.IO.StringReader sr = new System.IO.StringReader(stringWrite.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();


        //PdfPTable pdftable = new PdfPTable(GridView2.HeaderRow.Cells.Count);


        //foreach (TableCell  headercell  in GridView2.HeaderRow.Cells)
        //{ 

        //   Font font=new Font();
        //   font.Color=new  BaseColor(GridView2.HeaderStyle.ForeColor);
        //   PdfPCell pdfcell= new PdfPCell(new  Phrase( headercell.Text));
        //   pdftable.AddCell(pdfcell);
       
        //}
        // foreach (GridViewRow gridviewrow   in GridView2.Rows)
        //{ 

        //     foreach(TableCell tablecell in  gridviewrow.Cells)
        //     {

        //         Font font = new Font();
        //         font.Color = new BaseColor(GridView2.RowStyle.ForeColor);

        //          PdfPCell pdfcell= new PdfPCell(new  Phrase(tablecell.Text));

        //          pdftable.AddCell(pdfcell);
                

        //     }
          
       
        //}

        //Document pdfdocument=new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //PdfWriter.GetInstance(pdfdocument, Response.OutputStream);
        //pdfdocument.Open();
        //pdfdocument.Add(pdftable);
        //pdfdocument.Close();


        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Write(pdfdocument);
        //Response.Flush();



        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView2.AllowPaging = false;
        //    this.BindGrid();

            getgridview();

            GridView2.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView2.HeaderRow.Cells)
            {
                cell.BackColor = GridView2.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView2.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView2.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView2.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }



        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        //To Export all pages
        //        GridView2.AllowPaging = false;
        //        getgridview();

        //        GridView2.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.Write(pdfDoc);
        //        Response.End();
        //    }
        //}


    }
    
    
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
         ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
        ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
        gettotamout();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Plant Name--" + ddl_Plantname.SelectedItem.Text + "--Bill periopd:" + ddl_BillDate.SelectedItem.Text + "-- TotalBillAmount:" + Session["TOTttPAY"] + "Bank Amount:" + Session["BANKttPAY"] + "--Cash Amount:" + Session["cASHttPAY"];
            HeaderCell2.ColumnSpan = 9;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            try
            {


                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                string getroute = e.Row.Cells[1].Text;
                string getst = getroute + "00";
                string getend = getroute + "99";

                int congetst = Convert.ToInt16(getst);
                int congetend = Convert.ToInt16(getend);



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

                //ViewState["FDATE"] = FDATE;
                //ViewState["TODATE"] = TODATE;
                //ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
                //ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;

                con = DB.GetConnection();
                string getiingpayedamt;
                getiingpayedamt = "select  ISNULL(sum(NetAmount),0) as Netamount   from BankPaymentllotment    where Plant_Code='" + pcode + "'   and Billfrmdate='" + FDATE + "' and  Billtodate='" + TODATE + "'   AND    FinanceStatus='1'  and Agent_Id >= '" + congetst + "' and   Agent_Id < ='" + congetend + "'";
                SqlCommand cmd = new SqlCommand(getiingpayedamt, con);
                DataTable dttth = new DataTable();
                SqlDataAdapter dssd = new SqlDataAdapter(cmd);
                dssd.Fill(dttth);
                double getpay = Convert.ToDouble(dttth.Rows[0][0]);
                try
                {
                    e.Row.Cells[6].Text = getpay.ToString("F2");
                }
                catch
                {
                    e.Row.Cells[6].Text = "0.00";

                }

               

                getbankpay = Convert.ToDouble(e.Row.Cells[3].Text);
                getpaidamt = Convert.ToDouble(e.Row.Cells[6].Text);

                if (e.Row.Cells[5].Text == "&nbsp;")
                {
                    e.Row.Cells[5].Text = "0.0";

                }

             
                if ((e.Row.Cells[4].Text!=string.Empty) &&  (e.Row.Cells[4].Text!= "&nbsp;"))
                {
                   
                    cashying = Convert.ToDouble(e.Row.Cells[4].Text);

                }
                else
                {
                    e.Row.Cells[4].Text = "0.0";

                   cashying = Convert.ToDouble(e.Row.Cells[4].Text);

                }

                double getbb = Convert.ToDouble(getbankpay + cashying);

                e.Row.Cells[5].Text = getbb.ToString("f2");
                double totbankcash = (getbankpay + cashying);
                bankingbalance = getbankpay - getpaidamt;




               // e.Row.Cells[5].Text = totbankcash.ToString("f2");
                e.Row.Cells[7].Text = bankingbalance.ToString("f2");




                double getbankam = Convert.ToDouble(e.Row.Cells[3].Text);
                double getcasham = Convert.ToDouble(e.Row.Cells[4].Text);
                double gettot = Convert.ToDouble(e.Row.Cells[5].Text);
                double getpaid = Convert.ToDouble(e.Row.Cells[6].Text);
                double getbalance = Convert.ToDouble(e.Row.Cells[7].Text);


                getbankam2 =( getbankam2 + getbankam) ;
                getcasham2 =( getcasham2 + getcasham) ;
                gettot2 = (gettot2 + gettot);
                getpaid2 = (getpaid2 + getpaid);
                getbalance2 = (getbalance2 + getbalance);


               

            }
            catch
            {


            }


        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

        string routeid =  GridView2.SelectedRow.Cells[1].Text;
        string getroute = routeid;
        string getst = getroute + "00";
        string getend = getroute + "99";

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
        string getagent = "";
        getagent = "   select payment.Agent_Id,Agent_name,Payment_mode,Bank_Name,Ifsc_code,Agent_accountNo,TotNetAmount,isnull(Paidamount,0) as Paidamount,((TotNetAmount) - isnull(Paidamount,0))  as Balance         from (select Agent_id,Agent_name,Ifsc_code,Agent_accountNo,Bank_Name,Payment_mode,CONVERT(decimal(18,2), isnull(Sum(NetAmount),0)) as  TotNetAmount,Plant_code  from  Paymentdata   where Plant_code='" + pcode + "'   and Frm_date='" + FDATE + "'   and To_date='" + TODATE + "' and Route_id='" + routeid + "'  group by  Agent_id,Agent_name,Ifsc_code,Agent_accountNo,Bank_Name,Payment_mode,Plant_code) as payment  left join (select CONVERT(decimal(18,2), isnull(Sum(NetAmount),0)) as Paidamount,Plant_Code,Agent_Id  from  BankPaymentllotment   where Plant_code='" + pcode + "'   and  Billfrmdate='" + FDATE + "'   and   Billtodate='" + TODATE + "' AND FinanceStatus='1' and Agent_Id between '" + getst + "' and '" + getend + "'  group by Plant_Code,Agent_Id ) as paid on payment.Plant_code = paid.Plant_Code and payment.Agent_id = paid.Agent_Id";
        con = db.GetConnection();
        SqlCommand cmd = new SqlCommand(getagent, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtf=new DataTable();
        da.Fill(dtf);
        if(dtf.Rows.Count > 0)
        {
        GridView1.Visible = true;
        GridView1.DataSource = dtf;
        GridView1.DataBind();
        }

    }
}