using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.IO;
using CrystalDecisions.Shared;
using System.Data.Common;

public partial class Despatch_Rpt : System.Web.UI.Page
{
    SqlDataReader dr;

    public string rid;
    //public string frmdate;
   
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();
    BLLPlantMaster plantmasterBL = new BLLPlantMaster();
    public string companycode;
    public string plantcode;
    public string cname;
    public string Plantname;
    DataTable dt = new DataTable();
    BOLDispatch DispatchBOL = new BOLDispatch();
    BLLDispatch DispatchBLL = new BLLDispatch();
    DALDispatch DispatchDAL = new DALDispatch();
    BLLuser Bllusers = new BLLuser();
    public string frmdate;
  //  int cmpcode, pcode;
    DbHelper DBclass = new DbHelper();

    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    int destachvalue;
    public static int roleid;
       protected void Page_Load(object sender, EventArgs e)
    {

       

        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                if (roleid < 3)
                 {
                     loadsingleplant();
                 }
                if ((roleid >= 3) && (roleid != 9))
                 {
                     LoadPlantcode();
                 }
                if (roleid == 9)
                {

                    Session["Plant_Code"] = "170".ToString();
                    plantcode = "170";
                    loadspecialsingleplant();
                }
                 plantcode = ddl_PlantID.SelectedItem.Value;
                rid = ddl_PlantName.SelectedItem.Value;
               //Period_Despatchreport();
                lblmsg.Visible = false;
                CrystalReportViewer1.Visible = false;
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
        if (IsPostBack == true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
            
                dtm = System.DateTime.Now;
             //   txt_FromDate.Text = dtm.ToShortDateString();
           //     txt_ToDate.Text = dtm.ToShortDateString();
               // LoadPlantcode();
                plantcode = ddl_PlantID.SelectedItem.Value;
                rid = ddl_PlantName.SelectedItem.Value;
                Period_Despatchreport();
                lblmsg.Visible = false;


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
               SqlDataReader dr = null;
               ddl_PlantID.Items.Clear();
               ddl_PlantName.Items.Clear();
               dr = Bllusers.LoadPlantcode(companycode.ToString());
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
                       ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                   }
               }
               else
               {
                   ddl_PlantID.Items.Add("--Select PlantName--");
                   ddl_PlantName.Items.Add("--Select Plantcode--");
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
               ddl_PlantID.Items.Clear();
               ddl_PlantName.Items.Clear();
               dr = Bllusers.LoadSinglePlantcode(companycode, plantcode);
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
                       ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
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
               ddl_PlantID.Items.Clear();
               ddl_PlantName.Items.Clear();
               dr = Bllusers.LoadSinglePlantcode(companycode, "170");
               if (dr.HasRows)
               {
                   while (dr.Read())
                   {
                       ddl_PlantID.Items.Add(dr["Plant_Code"].ToString());
                       ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                   }
               }
           }
           catch (Exception ex)
           {
               WebMsgBox.Show(ex.ToString());
           }
       }


    protected void Button2_Click(object sender, EventArgs e)
    {
        plantcode = ddl_PlantID.SelectedItem.Value;
        if (RdoSelect.SelectedValue == "3")
        {
            DespatchandAcknoledgeDiffReport();
        }
        else
        {
            //   Period_Despatchreport();
            gridview();
        }
    }

    private void Period_Despatchreport()
    {
        try
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();

            cr.Load(Server.MapPath("Report\\DespatchCrystal.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            t1.Text = companycode + "_" + cname;
            t2.Text = ddl_PlantName.SelectedItem.Value;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To  " + txt_ToDate.Text.Trim();

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            // str = "SELECT t1.*,ISNULL(Agnt.CarAmt,0) AS CarAmt FROM (SELECT pcode,ISNULL(Smltr,0) AS Smltr,ISNULL(Smkg,0) AS Smkg,ISNULL(AvgFat,0) AS AvgFat,ISNULL(AvgSnf,0) AS AvgSnf,ISNULL(AvgRate,0) AS AvgRate,ISNULL(Avgclr,0) AS Avgclr,ISNULL(Scans,0) AS Scans,ISNULL(SAmt,0) AS SAmt,ISNULL(Avgcrate,0) AS Avgcrate,ISNULL(Sfatkg,0) AS Sfatkg,ISNULL(Ssnfkg,0) AS Ssnfkg,ISNULL(Billadv,0) AS Billadv,ISNULL(Ai,0) AS Ai,ISNULL(feed,0) AS feed,ISNULL(Can,0) AS Can,ISNULL(Recovery,0) AS Recovery,ISNULL(Others,0)AS Others  FROM (SELECT spropcode AS pcode,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2))AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS AvgFat,CAST(AvgSnf AS DECIMAL(18,2)) AS AvgSnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvgRate,CAST(Avgclr AS DECIMAL(18,2))AS Avgclr,CAST(Scans AS DECIMAL(18,2)) AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS Avgcrate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(Ssnfkg AS DECIMAL(18,2)) AS Ssnfkg,Billadv,Ai,feed,Can,Recovery,Others FROM (SELECT plant_Code AS spropcode,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,AVG(Rate) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE  Company_Code='1' AND Prdate BETWEEN '08-17-2012' AND '01-18-2013' GROUP BY plant_Code ) AS spro LEFT JOIN (SELECT  Plant_code AS dedupcode,SUM(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,SUM(CAST((Ai) AS DECIMAL(18,2))) AS Ai,SUM(CAST((Feed) AS DECIMAL(18,2))) AS Feed,SUM(CAST((can) AS DECIMAL(18,2))) AS can,SUM(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,SUM(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '08-17-2012' AND '01-18-2013' AND Company_Code='1' GROUP BY Plant_code ) AS Dedu ON  spro.spropcode=Dedu.dedupcode) AS produ LEFT JOIN (SELECT Plant_Code AS LonPcode,SUM(CAST(inst_amount AS DECIMAL(18,2))) AS instamt FROM LoanDetails WHERE Company_Code='1' AND Balance>0 GROUP BY Plant_Code) AS londed ON produ.pcode=londed.LonPcode) AS t1 LEFT JOIN (SELECT Plant_Code AS cartPCode,SUM(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt FROM  Agent_Master WHERE Type=0 AND Company_Code='1' GROUP BY Plant_Code) AS Agnt ON t1.pcode=Agnt.cartPCode ";
            // str = "select  sum(milk_Kg) as MILK_KG,avg(fat) as FAT,avg(clr) as CLR,avg(snf) as SNF,avg(rate) as Rate,sum(amount) as Amount,prdate  from bala.Procurement  where prdate between '" + txt_FromDate.Text + "' and '"+txt_ToDate.Text+"' group by prdate";
            str = "select convert(varchar(10),Date,101) as date, sum(MilkKg) as MILKKG,AVG(Fat) as FAT,AVG(Snf) as SNF,avg(clr) as clr,SUM (Amount) as AMOUNT,avg(Rate) as RATE from Despatchnew where date between '" + d1.ToString() + "'   and '" + d2.ToString() + "' and Plant_code='" + plantcode + "' group by date,Plant_From,Plant_To,Session,Date";
            SqlCommand cmd = new SqlCommand();

            //      SELECT CONVERT(VARCHAR(10),@DateTime ,105) AS Date
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;
            //System.IO.MemoryStream stream = (System.IO.MemoryStream)cr.ExportToStream(ExportFormatType.PortableDocFormat);
            //BinaryReader Bin = new BinaryReader(cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.ContentType = "application/pdf";
            //Response.BinaryWrite(Bin.ReadBytes(Convert.ToInt32(Bin.BaseStream.Length)));
            //Response.Flush();
            //Response.Close();

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }


    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_PlantID.SelectedIndex = ddl_PlantName.SelectedIndex;
        plantcode = ddl_PlantID.SelectedItem.Value;
       // Period_Despatchreport();
    }


    //public void gridview()
    //{
    //    DateTime dt1 = new DateTime();
    //    DateTime dt2 = new DateTime();

    //    dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //    dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);


    //    string d1 = dt1.ToString("MM/dd/yyyy");
    //    string d2 = dt2.ToString("MM/dd/yyyy");
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(connStr);
    //          DataTable dt = new DataTable();

    //          string str = "select convert(varchar(10),Date,101) as Date,cast(round(MilkKg,2) as numeric(36,2))     as MilkKg,(Fat) as FAT,(Snf) as SNF, cast(round(clr,2) as numeric(36,2))     as clr  from Despatchnew where date between '" + d1.ToString() + "'   and '" + d2.ToString() + "' and Plant_code='" + plantcode + "' order by  Date,Session";
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            
    //        da.Fill(dt);
    //        GridView1.DataSource = dt;
    //        GridView1.DataBind();


    //        GridView1.FooterRow.Cells[1].Text = "Total MilkKg";

    //        decimal milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("MILKKG"));
    //        GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
    //        GridView1.FooterRow.Cells[2].Text = milkkg.ToString("N2");


    //        var avgfat = dt.AsEnumerable().Where(x => x["FAT"] != DBNull.Value).Average(x => x.Field<double>("FAT"));
    //        GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
    //        GridView1.FooterRow.Cells[3].Text = avgfat.ToString("N2");



    //        var avgsnf = dt.AsEnumerable().Where(x => x["SNF"] != DBNull.Value).Average(x => x.Field<double>("SNF"));
    //        GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
    //        GridView1.FooterRow.Cells[4   ].Text = avgsnf.ToString("N2"); 

    //        //decimal avgfat = dt.AsEnumerable().Average(row => row.Field<decimal>("FAT"));
    //        //GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
    //        //GridView1.FooterRow.Cells[3].Text = avgfat.ToString("N2");


    //        //decimal AVGSNF = dt.AsEnumerable().Average(row => row.Field<decimal>("SNF"));
    //        //GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
    //        //GridView1.FooterRow.Cells[4].Text = AVGSNF.ToString("N2");





    //        GridView1.HeaderStyle.BackColor = System.Drawing.Color.Brown;
    //        GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
    //        GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
    //        GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
    //      //  GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
    //    }
    //    catch(ApplicationException exception)
    //    {

    //        lblmsg.Visible = true;
    //        lblmsg.Text = exception.Message;

    //        string script = "<script type=\"text/javascript\"> displayPopup('" + exception.Message + "'); </script>";
    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
    //    }

    //}




    public void gridview()
    {
        if (RdoSelect.SelectedValue == "1")
        {
            destachvalue = 1;
        }
        else if (RdoSelect.SelectedValue == "2")
        {
            destachvalue = 2;
        }
        else
        {
            destachvalue = 3;
        }



        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);


        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        try
        {
            SqlConnection con = new SqlConnection(connStr);
            DataTable dt = new DataTable();
            GridView2.Visible = false;
            GridView1.Visible = true;

            if (destachvalue == 1)
            {

                string str = "select convert(varchar(10),Date,101) as Date,Tanker_no as Tankerno,tcnumber as DcNumber,(MilkKg) as MILKKG,(Fat) as FAT,(Snf) as SNF, cast(round(clr,2) as numeric(36,2))     as clr  from    DespatchEntry where date between '" + d1.ToString() + "'   and '" + d2.ToString() + "' and Plant_code='" + plantcode + "' order by  Date,Session";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

            else
            {

                string str1 = "select convert(varchar(10),Date,101) as Date,Tanker_no as Tankerno,tcnumber as DcNumber,(MilkKg) as MILKKG,(Fat) as FAT,(Snf) as SNF, cast(round(clr,2) as numeric(36,2))     as clr  from Despatchnew where date between '" + d1.ToString() + "'   and '" + d2.ToString() + "' and Plant_code='" + plantcode + "' order by  Date,Session";
                SqlCommand cmd1 = new SqlCommand(str1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }


            if (dt.Rows.Count > 0)
            {
                GridView1.FooterRow.Cells[1].Text = "Total";

                decimal milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("MILKKG"));
                GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[4].Text = milkkg.ToString("N2");

                //decimal avgfat = dt.AsEnumerable().Sum(row => row.Field<decimal>("FAT"));
                //GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //GridView1.FooterRow.Cells[3].Text = avgfat.ToString("N2");



                var fat = dt.AsEnumerable().Where(x => x["FAT"] != DBNull.Value).Average(x => x.Field<decimal>("FAT"));
                GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[5].Text = fat.ToString("N2");


                var snf = dt.AsEnumerable().Where(x => x["SNF"] != DBNull.Value).Average(x => x.Field<decimal>("SNF"));
                GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[6].Text = snf.ToString("N2");



                //decimal AVGSNF = dt.AsEnumerable().Average(row => row.Field<decimal>("SNF"));
                //GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //GridView1.FooterRow.Cells[4].Text = AVGSNF.ToString("N2");





                GridView1.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
                GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
                //  GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "NO DATA...";

            }
        }
        catch (ApplicationException exception)
        {

            lblmsg.Visible = true;
            lblmsg.Text = exception.Message;

            string script = "<script type=\"text/javascript\"> displayPopup('" + exception.Message + "'); </script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "myscript", script);
        }

    }


  



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("dd/MM/yyyy");
        string d2 = dt2.ToString("dd/MM/yyyy");


        if (e.Row.RowType == DataControlRowType.Header)
        {           

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            TableCell HeaderCell4 = new TableCell();

            HeaderCell3.Text = ddl_PlantName.Text + "-FromDate: " + d1 + "-ToDate" + d2;
         //   HeaderCell4.Text = "ToDate" + d2;
           HeaderCell3.ColumnSpan = 9;
            HeaderCell3.Attributes.CssStyle["text-align"] = "center";
            HeaderRow.Cells.Add(HeaderCell3);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell3.Font.Bold = true;
            HeaderCell3.ForeColor = System.Drawing.Color.Brown;
            HeaderCell3.BackColor = System.Drawing.Color.White;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {

           



            GridViewRow HeaderRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            if (destachvalue == 1)
            {
                HeaderCell2.Text = "Despatch Report";
            }
            else
            {
                HeaderCell2.Text = "AcknowledgeMent Report";

            }
            HeaderCell2.Attributes.CssStyle["text-align"] = "center";
            HeaderCell2.ColumnSpan = 9;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.ForeColor = System.Drawing.Color.Brown;
            HeaderCell2.BackColor = System.Drawing.Color.White;

           
        }


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //   // e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        //    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
        //}

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "SRI VYSHNAVI DAIRY SPECIALITIES (P) LIMITED";
            HeaderCell2.Attributes.CssStyle["text-align"] = "center";
            HeaderCell2.ColumnSpan = 9;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.ForeColor = System.Drawing.Color.Brown;
            HeaderCell2.BackColor = System.Drawing.Color.White;


        }

        GridView1.HeaderStyle.BackColor = System.Drawing.Color.Brown;
        GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;

      //  GridView1.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
          

    }
    protected void btn_export_Click(object sender, EventArgs e)
    {

        try
        {
            if (RdoSelect.SelectedValue == "3")
            {
                destachvalue = 3;
            }
            if (destachvalue == 3)
            {
                Response.Clear();
                Response.Buffer = true;
                string filename = "'" + ddl_PlantName.Text + "'  " + DateTime.Now.ToString() + ".xls";

                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages

                    GridView2.AllowPaging = false;
                    DespatchandAcknoledgeDiffReport();

                    //      GridView1.HeaderRow.BackColor = 
                    foreach (TableCell cell in GridView2.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView2.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        //  row.BackColor = Color.White;
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
                    string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                    // string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
            else
            {
                Response.Clear();
                Response.Buffer = true;
                string filename = "'" + ddl_PlantName.Text + "'  " + DateTime.Now.ToString() + ".xls";

                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages

                    GridView1.AllowPaging = false;
                    gridview();

                    //      GridView1.HeaderRow.BackColor = 
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        //  row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                    // string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();

                }
            }
        }


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    

    public void DespatchandAcknoledgeDiffReport()
    {
        try
        {
            SqlConnection con = new SqlConnection(connStr);
            DataTable dt = new DataTable();
            DataTable dt3 = new DataTable();
          
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string str = "SELECT CONVERT(nvarchar(12),Ddate,103) AS Ddate,Dcnumber,DTanker_No,DMilkKg,DFat,DSnf,Dclr,AMilkKg,AFat,ASnf,AClr,(AMilkKg - DMilkKg) AS DiffMilkKg,(AFat -DFat) AS DiffFat,(ASnf - DSnf) AS DiffSnf,(AClr -Dclr) AS DiffClr FROM " +
" (SELECT ISNULL(DDcnumber,0) AS Dcnumber,ISNULL(DTanker_No,0) AS DTanker_No,Ddate AS Ddate,ISNULL(CAST(DMilkKg AS DECIMAL(18,2)),0) AS DMilkKg,ISNULL(CAST(DFat AS DECIMAL(18,2)),0) AS DFat,ISNULL(CAST(DSnf AS DECIMAL(18,2)),0) AS DSnf,ISNULL(CAST(Dclr AS DECIMAL(18,2)),0) AS Dclr,ISNULL(CAST(AMilkKg AS DECIMAL(18,2)),0) AS AMilkKg,ISNULL(CAST(AFat AS DECIMAL(18,2)),0) AS AFat,ISNULL(CAST(ASnf AS DECIMAL(18,2)),0) AS ASnf,ISNULL(CAST(AClr AS DECIMAL(18,2)),0) AS AClr FROM " +
" (SELECT tcnumber AS DDcnumber,Tanker_No AS DTanker_No,Date AS Ddate,SUM(MilkKg) AS DMilkKg,AVG(Fat) AS DFat,AVG(Snf) AS DSnf,AVG(Clr) AS Dclr FROM DespatchEntry where plant_code='" + plantcode + "' AND Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Tanker_No,date,tcnumber) AS t1 " +
" LEFT JOIN  " +
" (SELECT tcnumber,Tanker_No,Date,SUM(MilkKg) AS AMilkKg,AVG(Fat) AS AFat,AVG(Snf) AS ASnf,AVG(Clr) AS AClr FROM despatchnew where plant_code='" + plantcode + "' AND Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Tanker_No,date,tcnumber) AS t2 ON t1.Ddate=t2.date AND t1.DTanker_No=t2.Tanker_No AND t1.DDcnumber=t2.tcnumber ) AS f1  order by f1.Ddate ";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            GridView1.Visible = false;
            GridView2.Visible = true;
           
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt3;
                GridView1.DataBind();
                GridView2.DataSource = dt;
                GridView2.DataBind();
                //Grand Total Des
                GridView2.FooterRow.Cells[1].Text = "Total/Avg";

                decimal dmilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("DMilkKg"));
                GridView2.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[4].Text = dmilkkg.ToString("N2");

                var fat = dt.AsEnumerable().Where(x => x["DFat"] != DBNull.Value).Average(x => x.Field<decimal>("DFat"));
                GridView2.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[5].Text = fat.ToString("N2");


                var snf = dt.AsEnumerable().Where(x => x["DSnf"] != DBNull.Value).Average(x => x.Field<decimal>("DSnf"));
                GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[6].Text = snf.ToString("N2");

                var clr = dt.AsEnumerable().Where(x => x["Dclr"] != DBNull.Value).Average(x => x.Field<decimal>("Dclr"));
                GridView2.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[7].Text = clr.ToString("N2");

                //Grand Total Ack
                decimal Amilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("AMilkKg"));
                GridView2.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[8].Text = Amilkkg.ToString("N2");

                var Afat = dt.AsEnumerable().Where(x => x["AFat"] != DBNull.Value).Average(x => x.Field<decimal>("AFat"));
                GridView2.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[9].Text = Afat.ToString("N2");


                var Asnf = dt.AsEnumerable().Where(x => x["ASnf"] != DBNull.Value).Average(x => x.Field<decimal>("ASnf"));
                GridView2.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[10].Text = Asnf.ToString("N2");

                var Aclr = dt.AsEnumerable().Where(x => x["Aclr"] != DBNull.Value).Average(x => x.Field<decimal>("Aclr"));
                GridView2.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[11].Text = Aclr.ToString("N2");

                //Grand Total Ack

                decimal Diffmilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("DiffMilkKg"));
                GridView2.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[12].Text = Diffmilkkg.ToString("N2");

                var Difffat = dt.AsEnumerable().Where(x => x["DiffFat"] != DBNull.Value).Average(x => x.Field<decimal>("DiffFat"));
                GridView2.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[13].Text = Difffat.ToString("N2");


                var Diffsnf = dt.AsEnumerable().Where(x => x["DiffSnf"] != DBNull.Value).Average(x => x.Field<decimal>("DiffSnf"));
                GridView2.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[14].Text = Diffsnf.ToString("N2");

                var Diffclr = dt.AsEnumerable().Where(x => x["Diffclr"] != DBNull.Value).Average(x => x.Field<decimal>("Diffclr"));
                GridView2.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                GridView2.FooterRow.Cells[15].Text = Diffclr.ToString("N2");


                GridView2.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                GridView2.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GridView2.FooterStyle.ForeColor = System.Drawing.Color.White;
                GridView2.FooterStyle.BackColor = System.Drawing.Color.Brown;
              
            }
            else
            {
                GridView1.Visible = false;
                GridView2.DataSource = dt3;
                GridView2.DataBind();

                lblmsg.Visible = true;
                lblmsg.Text = "NO DATA...";
            }



        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.ToString();
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("dd/MM/yyyy");
        string d2 = dt2.ToString("dd/MM/yyyy");

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            TableCell HeaderCell4 = new TableCell();

            HeaderCell3.Text = ddl_PlantName.Text + "-FromDate: " + d1 + "-ToDate" + d2;          
            HeaderCell3.ColumnSpan = 16;
            HeaderCell3.Attributes.CssStyle["text-align"] = "center";
            HeaderRow.Cells.Add(HeaderCell3);
            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell3.Font.Bold = true;
            HeaderCell3.ForeColor = System.Drawing.Color.Brown;
            HeaderCell3.BackColor = System.Drawing.Color.White;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            if (RdoSelect.SelectedValue == "3")
            {
                destachvalue = 3;
            }
            if (destachvalue == 3)
            {
                HeaderCell2.Text = "Despatch & AcknowledgeMent Difference Report";
            }
           
            HeaderCell2.Attributes.CssStyle["text-align"] = "center";
            HeaderCell2.ColumnSpan = 16;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.ForeColor = System.Drawing.Color.Brown;
            HeaderCell2.BackColor = System.Drawing.Color.White;
        }
               

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "SRI VYSHNAVI DAIRY SPECIALITIES (P) LIMITED";
            HeaderCell2.Attributes.CssStyle["text-align"] = "center";
            HeaderCell2.ColumnSpan = 16;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.ForeColor = System.Drawing.Color.Brown;
            HeaderCell2.BackColor = System.Drawing.Color.White;
        }
        GridView2.HeaderStyle.BackColor = System.Drawing.Color.Brown;
        GridView2.HeaderStyle.ForeColor = System.Drawing.Color.White;
        
          
    }
}