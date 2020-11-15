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
using iTextSharp.text.xml;



public partial class PlantSessionwiseReport : System.Web.UI.Page
{
    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public int rid;
    public string managmobNo;
    public string pname;
    public string cname;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    //Admin Check Flag
    public int Falg = 0;
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();


    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;

    public int refNo = 0;
    string agentcode;

    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                Lbl_ReportTitle.Visible = false;
                Lbl_RepeortDate.Visible = false;

                if ((Lbl_msg.Text != string.Empty) && (Lbl_msg.Text != "Label"))
                {

                    Lbl_msg.Visible = true;
                }
                else
                {

                    Lbl_msg.Visible = false;

                }
              //  Image1.Visible = false;
                //    Label111.Visible = false;

                //  Label6.Visible = false;
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
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
            //    managmobNo = Session["managmobNo"].ToString();
                ddl_plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
                pcode = ddl_plantcode.SelectedItem.Value;
                Lbl_ReportTitle.Visible = true;
                Lbl_RepeortDate.Visible = true;

                //if ((Lbl_msg.Text != string.Empty) && (Lbl_msg.Text != "Label"))
                //{

                    Lbl_msg.Visible = true;
                //}
                //else
                //{

                  //  Lbl_msg.Visible = false;

                //}
                gridview();

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
            //   Label6.Visible = true;

        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
                ddl_plantcode.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    protected void btn_ok_Click(object sender, EventArgs e)
    {
       // getgrid();
        gridview();
        gridview1();
    }


    //public void getgrid()
    //{
                
    //    DateTime dt1 = new DateTime();
    //    DateTime dt2 = new DateTime();
    //    dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //    dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
    //    string d1 = dt1.ToString("MM/dd/yyyy");
    //    string d2 = dt2.ToString("MM/dd/yyyy");
    //    String connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //    SqlConnection con = new SqlConnection(connection);
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.CommandText = "Get_SameAccountNoListofAgents";
    //    cmd.Connection = con;
    //    cmd.Parameters.Add("@Company_Code", SqlDbType.Int).Value = ccode;
    //    cmd.Parameters.Add("@Plant_Code", SqlDbType.Int).Value = pcode;
    //    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = ddl_Agentname.Text;
    //    cmd.Parameters.Add("@frmdate", SqlDbType.NVarChar).Value = d1;
    //    cmd.Parameters.Add("@todate", SqlDbType.NVarChar).Value = d2;
    //    cmd.Connection = con;
    //    try
    //    {
    //        con.Open();
    //        GridView1.EmptyDataText = "No Records Found";
    //        GridView1.DataSource = cmd.ExecuteReader();
    //        GridView1.DataBind();
    //        //GridView1.HeaderStyle.BackColor = Color.Silver;
    //        //GridView1.HeaderStyle.ForeColor = Color.White;
    //        GridView1.HeaderStyle.BackColor = Color.White;
    //        GridView1.HeaderStyle.ForeColor = Color.Brown;

    //        Lbl_msg.Text = "Plant Name:" + ddl_Plantname.Text;
    //        if (ddl_Agentname.SelectedValue == "1")
    //        {
    //            Lbl_msg11.Text = "Selecting Type:AccountName";
    //            Lbl_msg11.Visible = true;
    //        }
    //        if (ddl_Agentname.SelectedValue == "2")
    //        {

    //            Lbl_msg11.Text = "Selecting Type:Account Number";
    //            Lbl_msg11.Visible = true;
    //        }

    //        if ((Lbl_msg.Text != string.Empty) && (Lbl_msg.Text != "Label"))
    //        {

    //            Lbl_msg.Visible = true;
    //        }
    //        else
    //        {

    //            Lbl_msg.Visible = false;

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //        Lbl_msg.Visible = false;
    //    }
    //    finally
    //    {

    //        con.Close();
    //        con.Dispose();

    //    }
    //}


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

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
             //   string sqlstr = " SELECT CONVERT(Nvarchar(25),Prdate,103) AS pdate,Sessions,cast(SUM(Milk_kg)  AS DECIMAL(18,2)) as Milkkg ,cast(SUM(Milk_ltr) AS  decimal(18,2)) as Milkltr,CAST(((SUM(fat_kg)*100)/SUM(Milk_kg)) AS DECIMAL(18,2)) AS Afat,CAST(((SUM(snf_kg)*100)/SUM(Milk_kg)) AS DECIMAL(18,2)) AS ASnf ,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS Ssnfkg,Count(*) AS SampleNo FROM procurement Where Plant_Code='" + pcode + "' AND Prdate Between '" + d1 + "' AND '" + d2 + "' GROUP BY Sessions,Prdate,Plant_Code order by Prdate,Sessions";
              // Lastworking  string sqlstr = "SELECT CONVERT(Nvarchar(25),Prdate,103) AS pdate,Sessions,cast(SUM(Milk_kg)  AS DECIMAL(18,2)) as Milkkg ,cast(SUM(Milk_ltr) AS  decimal(18,2)) as Milkltr,CAST(((SUM(fat_kg)*100)/SUM(Milk_kg)) AS DECIMAL(18,2)) AS Afat,CAST(((SUM(snf_kg)*100)/SUM(Milk_kg)) AS DECIMAL(18,2)) AS ASnf ,cast(SUM(fat_kg) AS  decimal(18,2)) as  Fatkg,cast(SUM(snf_kg) AS decimal(18,2)) Snfkg,Count(*) AS SampleNo FROM procurement Where Plant_Code='" + pcode + "' AND Prdate Between '" + d1 + "' AND '" + d2 + "' GROUP BY Sessions,Prdate,Plant_Code order by Prdate,Sessions ";
                string sqlstr = "SELECT CONVERT(Nvarchar(25),f1.fprdate,103) AS Date,f1.fmkg AS AMkg,f2.Milkkg AS PMkg,(ISNULL(f1.fmkg,0)+ISNULL(f2.Milkkg,0)) AS TotalMkg  FROM " +
" (SELECT p.Plant_Code AS fpcode,A.pdate AS fprdate,A.Sessions AS fsess,A.Milkkg As fmkg FROM " +
" (SELECT Plant_Code FROM plant_master Where plant_code='"+pcode+"') AS p " +
" LEFT JOIN " +
" (SELECT Prdate AS pdate,Sessions,cast(SUM(Milk_kg)  AS DECIMAL(18,2)) as Milkkg,Plant_Code AS pcode FROM procurement Where Plant_Code='" + pcode + "' AND Prdate Between '" + d1 + "' AND '" + d2 + "' AND Sessions='AM' GROUP BY Sessions,Prdate,Plant_Code ) AS A ON p.Plant_Code=A.pcode) AS f1 " +
" INNER JOIN " +
" (SELECT p.Plant_Code,A.pdate,A.Sessions,A.Milkkg FROM " +
" (SELECT Plant_Code FROM plant_master Where plant_code='" + pcode + "') AS p " +
" LEFT JOIN " +
" (SELECT Prdate AS pdate,Sessions,cast(SUM(Milk_kg)  AS DECIMAL(18,2)) as Milkkg,Plant_Code AS pcode FROM procurement Where Plant_Code='" + pcode + "' AND Prdate Between '" + d1 + "' AND '" + d2 + "' AND Sessions='PM' GROUP BY Sessions,Prdate,Plant_Code ) AS A ON p.Plant_Code=A.pcode) AS f2 ON f1.fpcode=f2.Plant_Code AND f1.fprdate=f2.pdate Order By fprdate";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    Lbl_msg.Text = "Plant Name:" + ddl_Plantname.Text;
                    Lbl_ReportTitle.Text = "Sessionwise Milk Details";
                    Lbl_RepeortDate.Text = "From :" + txt_FromDate.Text.Trim() + "To :" + txt_ToDate.Text.Trim();
                    GridView1.HeaderStyle.BackColor = Color.White;
                    GridView1.HeaderStyle.ForeColor = Color.Brown;
                   // Image1.Visible = true;

                    GridView1.HeaderRow.Cells[0].Font.Bold = true;
                    GridView1.HeaderRow.Cells[0].Font.Size = 12;
                    GridView1.HeaderRow.Cells[1].Font.Bold = true;
                    GridView1.HeaderRow.Cells[1].Font.Size = 12;
                    GridView1.HeaderRow.Cells[2].Font.Bold = true;
                    GridView1.HeaderRow.Cells[2].Font.Size = 12;
                    GridView1.HeaderRow.Cells[3].Font.Bold = true;
                    GridView1.HeaderRow.Cells[3].Font.Size = 12;

                    GridView1.HeaderRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.HeaderRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.HeaderRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                    GridView1.FooterRow.Cells[0].Text = "Total";
                    decimal AMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("AMkg"));
                    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[1].Text = AMkg.ToString("N2");
                    decimal PMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("PMkg"));
                    GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[2].Text = PMkg.ToString("N2");
                    decimal TotalMkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalMkg"));
                    GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[3].Text = TotalMkg.ToString("N2");

                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Lbl_msg.Text = "Plant Name:" + ddl_Plantname.Text;
                    Lbl_ReportTitle.Text = "Sessionwise Milk From :" + txt_FromDate.Text.Trim() + "To :" + txt_ToDate.Text.Trim();
                    GridView1.HeaderStyle.BackColor = Color.White;
                    GridView1.HeaderStyle.ForeColor = Color.Brown;

                }



            }
        }
        catch (Exception ex)
        {
            throw ex;
            Lbl_msg.Visible = false;
        }
        finally
        {

            con.Close();
            con.Dispose();

        }
    }
    public void gridview1()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string getnill = "";
            con = DBaccess.GetConnection();
            getnill = "Select convert(varchar,amprdate,103) as Date,amkg,amltr,amavgfat,amavgsnf,pmkg,pmltr,pmavgfat,pmavgsnf,mkg,mltr,avgfat,avgsnf  from (Select amprdate,amkg,amltr,amsessions,amavgfat,amavgsnf,pmkg,pmltr,pmsessions,pmavgfat,pmavgsnf from (sELECT prdate as amprdate,isnull(convert(decimal(18,2),Sum(milk_kg)),0) as amkg,isnull(convert(decimal(18,2),Sum(milk_ltr)),0) as amltr,Sessions  as amSessions,convert(decimal(18,2),(Sum(fat_kg) * 100 / Sum(milk_kg)),0)  as amAvgfat,convert(decimal(18,2),(Sum(snf_kg) * 100 / Sum(milk_kg)),0)  as amAvgsnf  FROM PROCUREMENT    WHERE PLANT_CODE='" + pcode + "'  and prdate between '" + d1 + "' and '" + d2 + "'  and sessions='Am'  group by prdate,sessions,plant_code  ) as amdata left join (sELECT prdate as pmprdate,isnull(convert(decimal(18,2),Sum(milk_kg)),0) as pmkg,isnull(convert(decimal(18,2),Sum(milk_ltr)),0) as pmltr,Sessions  as pmSessions,convert(decimal(18,2),(Sum(fat_kg) * 100 / Sum(milk_kg)),0)  as pmAvgfat,convert(decimal(18,2),(Sum(snf_kg) * 100 / Sum(milk_kg)),0)  as pmAvgsnf FROM PROCUREMENT    WHERE PLANT_CODE='" + pcode + "'  and prdate between '" + d1 + "' and '" + d2 + "'  and sessions='pm'  group by prdate,sessions,plant_code ) as pmdata on  amdata.amprdate=pmdata.pmprdate) as sess left join(sELECT prdate as prdate,isnull(convert(decimal(18,2),Sum(milk_kg)),0) as mkg,isnull(convert(decimal(18,2),Sum(milk_ltr)),0) as mltr,convert(decimal(18,2),(Sum(fat_kg) * 100 / Sum(milk_kg)),0)  as  Avgfat,convert(decimal(18,2),(Sum(snf_kg) * 100 / Sum(milk_kg)),0)  as  Avgsnf FROM PROCUREMENT    WHERE PLANT_CODE='" + pcode + "'  and prdate between '" + d1 + "' and '" + d2 + "'   group by prdate,plant_code) as rights on sess.amprdate=rights.prdate   order by amprdate asc";
            SqlCommand cmd = new SqlCommand(getnill, con);
            SqlDataAdapter dsp = new SqlDataAdapter(cmd);
            DataTable des = new DataTable();
            des.Rows.Clear();
            dsp.Fill(des);
            if (des.Rows.Count > 0)
            {

                GridView8.DataSource = des;
                GridView8.DataBind();
            }

            
        }
        catch (Exception ex)
        {
            throw ex;
            Lbl_msg.Visible = false;
        }
        finally
        {

            con.Close();
            con.Dispose();

        }
    }

    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_Plantname.Text + "'  " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");


            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView8.AllowPaging = false;
             gridview1();

                GridView8.HeaderRow.BackColor = Color.White;
                //foreach (TableCell cell in GridView1.HeaderRow.Cells)
                //{
                //    cell.BackColor = GridView1.HeaderStyle.BackColor;
                //}
                //foreach (GridViewRow row in GridView1.Rows)
                //{
                //    row.BackColor = Color.White;
                //    foreach (TableCell cell in row.Cells)
                //    {
                //        if (row.RowIndex % 2 == 0)
                //        {
                //            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                //        }
                //        else
                //        {
                //            cell.BackColor = GridView1.RowStyle.BackColor;
                //        }
                //        cell.CssClass = "textmode";
                //    }
                //}

                GridView8.RenderControl(hw);
                //    FileStream Fs = new FileStream(FilePathName, FileMode.Create, Fileaccess.Read);
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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //GridViewRow gvRow = e.Row;
        //if (gvRow.RowType == DataControlRowType.Header)
        //{
        //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell cell0 = new TableCell();
        //    cell0.Text = "AM";
        //    cell0.HorizontalAlign = HorizontalAlign.Center;
        //    cell0.ColumnSpan = 5;
        //    TableCell cell1 = new TableCell();
        //    cell1.Text = "PM";
        //    cell1.HorizontalAlign = HorizontalAlign.Center;
        //    cell1.ColumnSpan = 5;
        //    gvrow.Cells.Add(cell0);
        //    gvrow.Cells.Add(cell1);
        //    GridView1.Controls[0].Controls.AddAt(0, gvrow);

        //}

        try
        {
            string spilval = string.Empty;
            string[] spilvalarr = new string[2];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                
            }
        }
        catch (Exception ex)
        {
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign= HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
        
         

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;

        }


        GridViewRow gvRow = e.Row;
        if (gvRow.RowType == DataControlRowType.Header)
        {
            GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell cell0 = new TableCell();
            cell0.Text = "Milk Details For AM shift";
            cell0.HorizontalAlign = HorizontalAlign.Center;
            cell0.ColumnSpan = 6;

            TableCell cell1 = new TableCell();
            cell1.Text = "Milk Details For pm shift";
            cell1.HorizontalAlign = HorizontalAlign.Center;
            cell1.ColumnSpan = 4;

            TableCell cell2 = new TableCell();
            cell2.Text = "Milk Details For Total Day";
            cell2.HorizontalAlign = HorizontalAlign.Center;
            cell2.ColumnSpan = 4;

            gvrow.Cells.Add(cell0);
            gvrow.Cells.Add(cell1);
            gvrow.Cells.Add(cell2);
            GridView8.Controls[0].Controls.AddAt(0, gvrow);
        }

    }
    protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView8_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    TableCell HeaderCell2 = new TableCell();
        //    HeaderCell2.Text = "Milk Details Am Sessions";
        //    HeaderCell2.ColumnSpan = 5;
        //    HeaderRow.Cells.Add(HeaderCell2);

        //    HeaderCell2 = new TableCell();
        //    HeaderCell2.Text = "Milk Details Am Sessions";
        //    HeaderCell2.ColumnSpan = 4;
        //    HeaderRow.Cells.Add(HeaderCell2);

        //    HeaderCell2 = new TableCell();
        //    HeaderCell2.Text = "Milk Details For Total";
        //    HeaderCell2.ColumnSpan = 4;
        //    HeaderRow.Cells.Add(HeaderCell2);
        //    GridView8.Controls[0].Controls.AddAt(0, HeaderRow);
        //}
 
    }
}