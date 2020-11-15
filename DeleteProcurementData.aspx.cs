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

public partial class DeleteProcurementData : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    DateTime dtm = new DateTime();
    DateTime dtm2 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    string sqlstr;
    SqlCommand deletecmd;
    public static int roleid;
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                rbtLstReportItems.SelectedValue = "1";

              //  dtm2 = System.DateTime.Now;
              //txt_frmdeldate.Text= dtm2.ToShortDateString();
              //txt_Todeldate.Text = dtm2.ToShortDateString();
              //txt_frmdeldate.Text = dtm2.ToString("dd/MM/yyyy");
              //txt_Todeldate.Text = dtm2.ToString("dd/MM/yyyy");
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";

                    loadspecialsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                gridview();
              
            }

            else
            {
                //dtm = System.DateTime.Now;
                //// dti = System.DateTime.Now;
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyy");

                pcode = ddl_Plantcode.SelectedItem.Value;
                //  LoadPlantcode();

                gridview();
            }

        }
        else
        {


            pcode = ddl_Plantcode.SelectedItem.Value;
            gridview();
            //  LoadPlantcode();
            //   ddl_agentcode.Text = getrouteusingagent;
           
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
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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

    protected void btn_Ok_Click(object sender, EventArgs e)
    {
       
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        getifsccode();
        gridview();
    }
    public void getifsccode()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
            //   string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
         //   string str = "select * from BANK_MASTER   where plant_code='" + pcode + "' and Bank_id='" + ddl_bankid.Text + "'";

            string str = "select   top 1   convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate from Bill_date   where plant_code='" + pcode + "'  AND STATUS=0  order  by tid desc";
              //  string str= = "select * from Bill_date   where plant_code='" + pcode + "' and Bill_frmdate='"+txt_FromDate.Text+"'    Bill_todate='" + txt_ToDate.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                // ddl_ccode.Items.Add(dr["company_code"].ToString());

                txt_FromDate.Text=dr["Bill_frmdate"].ToString();
                txt_ToDate.Text= dr["Bill_todate"].ToString();

            }


        }

        catch
        {

            WebMsgBox.Show("ERROR");

        }



    }
    public void gridview()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string SECOND = "12:00:00";
            string RES = d1 + " " + SECOND;
            string RES1 = d2 + " " + SECOND;
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
               
                if (rbtLstReportItems.SelectedValue == "1")
                {
                     sqlstr = "select   distinct convert(varchar,Prdate,103) as Prdate,Plant_Code  from procurement where Plant_Code='" + pcode + "' and Prdate between '" + d1 + "' and '" + d2 + "'    order by prdate";
                }
                if (rbtLstReportItems.SelectedValue == "2")
                {
                    sqlstr = "select   distinct convert(varchar,Prdate,103) as Prdate,Plant_Code  from ProducerProcurement where Plant_Code='" + pcode + "' and     (( Prdate between '" + d1 + "' and '" + d2 + "') OR  (prdate='" + RES + "') OR (prdate='" + RES1 + "'))  order by prdate";

                }
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }



            }
        }
        catch
        {



        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        getifsccode();
        // gridview();
    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_ToDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
         string prddd  = GridView1.DataKeys[e.RowIndex].Value.ToString();

         txt_assigndate.Text = prddd;

         DateTime dt12 = new DateTime();
      
         dt12 = DateTime.ParseExact(txt_assigndate.Text, "dd/MM/yyyy", null);

         string SECOND = "12:00:00";
         string d11 = dt12.ToString("MM/dd/yyyy");
       
        string RES= d11 + " " + SECOND;





         //DateTime dtt = Convert.par(prddd);
         //string d11 = dtt.ToString("MM/dd/yyyy");
         
       //  string d1 = prddd("MM/dd/yyyy");
       ////  string ddd =GridView1.DataKeys["Prdate"].Value.ToString();
        // DataRowView rowView = (DataRowView)GridView1.Rows[0].DataItem;
     //    string name = GridView1.Rows[0].Cells["RowNumber"].Text;
         Label userid = (Label)row.FindControl("plant_code");
            //TextBox pdate = (TextBox)row.Cells[1].Controls[0];
            //TextBox supmobile = (TextBox)row.Cells[2].Controls[0];
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                if (rbtLstReportItems.SelectedValue == "1")
                {
                     deletecmd = new SqlCommand("delete FROM  procurement   where plant_code='" + pcode + "'  and prdate='" + d11 + "'  ", conn);
                }
                if (rbtLstReportItems.SelectedValue == "2")
                {
                    deletecmd = new SqlCommand("delete FROM  ProducerProcurement   where plant_code='" + pcode + "'  and ((prdate='" + d11 + "') OR (prdate='" + RES + "'))   ", conn);

                }
                deletecmd.ExecuteNonQuery();
                 WebMsgBox.Show("Data Deleted SuccessFully");
                conn.Close();
                gridview();
                 
           }
        }
    protected void Button1_Click(object sender, EventArgs e)
    {


       

    }
    protected void PrintGridData(object sender, EventArgs e)
    {
        //GridView1.PagerSettings.Visible = false;
        //GridView1.DataBind();
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //GridView1.RenderControl(hw);
        //string gridHTML = sw.ToString().Replace("\"", "'")
        //    .Replace(System.Environment.NewLine, "");
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.onload = new function(){");
        //sb.Append("var printWin = window.open('', '', 'left=0");
        //sb.Append(",top=0,width=1000,height=600,status=0');");
        //sb.Append("printWin.document.write(\"");
        //sb.Append(gridHTML);
        //sb.Append("\");");
        //sb.Append("printWin.document.close();");
        //sb.Append("printWin.focus();");
        //sb.Append("printWin.print();");
        //sb.Append("printWin.close();};");
        //sb.Append("</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        //GridView1.PagerSettings.Visible = true;
        //GridView1.DataBind();
    }



    protected void Button1_Click1(object sender, EventArgs e)
    {
       //Response.ContentType = "application/pdf";
       // Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.pdf");
       // Response.Cache.SetCacheability(HttpCacheability.NoCache);
       // StringWriter sw = new StringWriter();
       // HtmlTextWriter hw = new HtmlTextWriter(sw);
       // GridView1.RenderControl(hw);
       // StringReader sr = new StringReader(sw.ToString());
       // Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
       // HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
       // PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
       // pdfDoc.Open();
       // htmlparser.Parse(sr);
       // pdfDoc.Close();
       // Response.Write(pdfDoc);
       // Response.End();
       // GridView1.AllowPaging = true;
       // GridView1.DataBind();  
        PdfPTable pdftable = new PdfPTable(GridView1.HeaderRow.Cells.Count);
        foreach (GridViewRow gridviewrow in GridView1.Rows)
        {
            foreach (TableCell tablecell in gridviewrow.Cells)
            {

                ;


                PdfPCell pdfcell = new PdfPCell(new Phrase(tablecell.Text));
                pdftable.AddCell(pdfcell);
            }
        }
            Document pdfdocument = new Document(PageSize.A4);
            PdfWriter.GetInstance(pdfdocument, Response.OutputStream);
            pdfdocument.Open();
            pdfdocument.Add(pdftable);
            pdfdocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=deleteprocu.pdf");
            Response.Write(pdfdocument);
            Response.Flush();
            Response.End();

        

         
    }
   
}