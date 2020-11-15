using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;

public partial class PlantStatusReport : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime dtm = new DateTime();
    public string Company_code;
    public string plant_Code;
    public string cname;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                LoadPlantName();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
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
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                LoadCustomers();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        int saveindicate = 0;
        if (chk_plant.Checked==true)
        {            
            saveindicate++;
        }
        if (chk_Route.Checked == true)
        {
            saveindicate++;
        }
        if (chk_Agent.Checked == true)
        {
            saveindicate++;
        }
        if (saveindicate == 1)
        {
            LoadCustomers();
        }
        else if (saveindicate == 0)
        {
            ds = null;
            gvCustomers.DataSource = ds;
            gvCustomers.DataBind();
            WebMsgBox.Show("Please, Choose the Report Type");
        }
        else
        {
            ds = null;
            gvCustomers.DataSource = ds;
            gvCustomers.DataBind();
            WebMsgBox.Show("Please, Choose any one Report Type");
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        ExportExcel();
    }

    private void LoadPlantName()
    {
        try
        {
            ds = null;
            ddl_Plantname.Items.Clear();
            ds = BllPlant.LoadPlantNameChkLst(Company_code.ToString());
            ddl_Plantname.DataSource = ds;

            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "plant_Code";
            ddl_Plantname.DataBind();

            if (ddl_Plantname.Items.Count > 0)
            {
                
            }
            else
            {
                ddl_Plantname.Items.Add("--Select Plantname--");
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void LoadCustomers()
    {
        try
        {
            
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string query =string.Empty;
            string strConnString = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            //string query = "SELECT t3.Rid AS R_Id,t3.Route_Name AS R_Name,t2.Agent_id AS A_Id,t2.Agent_Name AS A_Name,t2.M_kg AS SMilk_Kg,t2.M_Ltr AS SMilk_Ltr,t2.Afat AS Afat ,t2.ASnf AS Asnf FROM (SELECT * FROM (SELECT Agent_id,ISNULL(CAST(SUM(Milk_kg) AS DECIMAL(18,2)),0) AS M_kg,ISNULL(CAST(SUM(Milk_ltr) AS DECIMAL(18,2)),0) AS M_Ltr,ISNULL(ROUND(SUM(fat_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS Afat,ISNULL(ROUND(SUM(snf_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS ASnf  FROM Procurement WHERE Company_Code='" + Company_code + "' AND  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "' AND Prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' GROUP BY Agent_id ) AS t1 LEFT JOIN (SELECT Agent_Id AS Aid,Agent_Name,Route_id FROM Agent_Master WHERE Company_code='" + Company_code + "' AND Plant_code='" + ddl_Plantname.SelectedItem.Value + "' AND Type=0) AS t2 ON t1.Agent_id=t2.Aid )AS t2 LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_code='" + Company_code + "' AND Plant_code='" + ddl_Plantname.SelectedItem.Value + "') AS t3 ON t2.Route_id=t3.Rid ORDER BY RAND(t2.Agent_id) ";
            if (chk_plant.Checked == true)
            {
                query = "SELECT Plant_Code,ISNULL(CAST(SUM(Milk_kg) AS DECIMAL(18,2)),0) AS M_kg,ISNULL(CAST(SUM(Milk_ltr) AS DECIMAL(18,2)),0) AS M_kg,ISNULL(ROUND(SUM(fat_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS Afat,ISNULL(ROUND(SUM(snf_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS ASnf  FROM Procurement WHERE Company_Code=@Companycode AND  Plant_Code=@Plantcode AND Prdate BETWEEN @Frmdate AND @todate GROUP BY Plant_Code ";
            }
            else if (chk_Route.Checked == true)
            {

                query = "SELECT t1.Route_id AS R_Id,t2.Route_Name AS R_Name,t1.M_kg AS SMilk_Kg,t1.M_Ltr AS SMilk_Ltr,t1.Afat AS Afat,t1.ASnf AS Asnf FROM (SELECT Route_id,ISNULL(CAST(SUM(Milk_kg) AS DECIMAL(18,2)),0) AS M_kg,ISNULL(CAST(SUM(Milk_ltr) AS DECIMAL(18,2)),0) AS M_Ltr,ISNULL(ROUND(SUM(fat_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS Afat,ISNULL(ROUND(SUM(snf_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS ASnf  FROM Procurement WHERE Company_Code=@Companycode AND  Plant_Code=@Plantcode AND Prdate BETWEEN @Frmdate AND @todate GROUP BY Route_id ) AS t1 LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_code=@Companycode AND Plant_code=@Plantcode  ) AS t2 ON t1.Route_id=t2.Rid ORDER BY RAND(t1.Route_id)";
            }
            else if (chk_Agent.Checked == true)
            {
                query = "SELECT t3.Rid AS Route_Id,t3.Route_Name AS Route_Name,t2.Agent_id AS Agent_Id,t2.Agent_Name AS Agent_Name,t2.M_kg AS SMilk_Kg,t2.M_Ltr AS SMilk_Ltr,t2.Afat AS Afat ,t2.ASnf AS Asnf FROM (SELECT * FROM (SELECT Agent_id,ISNULL(CAST(SUM(Milk_kg) AS DECIMAL(18,2)),0) AS M_kg,ISNULL(CAST(SUM(Milk_ltr) AS DECIMAL(18,2)),0) AS M_Ltr,ISNULL(ROUND(SUM(fat_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS Afat,ISNULL(ROUND(SUM(snf_kg)*100/CAST(SUM(Milk_kg) AS DECIMAL(18,2)),1,1),0) AS ASnf  FROM Procurement WHERE Company_Code=@Companycode AND  Plant_Code=@Plantcode AND Prdate BETWEEN @Frmdate AND @todate GROUP BY Agent_id ) AS t1 LEFT JOIN (SELECT Agent_Id AS Aid,Agent_Name,Route_id FROM Agent_Master WHERE Company_code=@Companycode AND Plant_code=@Plantcode AND Type=0) AS t2 ON t1.Agent_id=t2.Aid )AS t2 LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_code=@Companycode AND Plant_code=@Plantcode) AS t3 ON t2.Route_id=t3.Rid ORDER BY RAND(t2.Agent_id) ";

            }
            else
            {

            }
            
             
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Companycode", Company_code);
            cmd.Parameters.AddWithValue("@Plantcode", ddl_Plantname.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Frmdate", d1.Trim());
            cmd.Parameters.AddWithValue("@todate", d2.Trim());

            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "ks");

                        gvCustomers.DataSource = ds;
                        gvCustomers.DataBind();
                    }
                }
            }
            //SqlDataAdapter da = new SqlDataAdapter(query, strConnString);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //gvCustomers.DataSource = dt;
            //gvCustomers.DataBind();
            
        }
        catch (Exception ex)
        {
        }
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        gvCustomers.PageIndex = e.NewPageIndex;
        gvCustomers.DataBind();
    }

    private void ExportExcel()
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "AgentwiseStatus_List.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvCustomers.AllowPaging = false;
        LoadCustomers();
        gvCustomers.HeaderRow.Style.Add("background-color", "#FFFFFF");//#3AC0F2.#FFFFFF,#507CD1
        for (int a = 0; a < gvCustomers.HeaderRow.Cells.Count; a++)
        {
            gvCustomers.HeaderRow.Cells[a].Style.Add("background-color", "#3AC0F2");
        }
        int j = 1;
        foreach (GridViewRow gvrow in gvCustomers.Rows)
        {
            gvrow.BackColor = Color.White;
            if (j <= gvCustomers.Rows.Count)
            {
                if (j % 2 != 0)
                {
                    for (int k = 0; k < gvrow.Cells.Count; k++)
                    {
                        gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                    }
                }
            }
            j++;
        }
        gvCustomers.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void PrintCurrentPage(object sender, EventArgs e)
    {
        gvCustomers.PagerSettings.Visible = false;
        gvCustomers.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvCustomers.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=500,height=300,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        gvCustomers.PagerSettings.Visible = true;
        gvCustomers.DataBind();
    }

    protected void PrintAllPages(object sender, EventArgs e)
    {
        gvCustomers.AllowPaging = false;
        gvCustomers.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvCustomers.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=500,height=300,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        gvCustomers.AllowPaging = true;
        gvCustomers.DataBind();
    }

    protected void gvCustomers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
             string reporthader =string.Empty;
           

            GridView HeaderGrid2 = (GridView)sender;
            GridViewRow HeaderGridRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell2.Text = "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2.Font.Size =11;
            HeaderCell2.ColumnSpan = 8;
            HeaderCell2.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            HeaderGridRow2.Cells.Add(HeaderCell2);
            HeaderGridRow2.ForeColor = ColorTranslator.FromHtml("White");
            HeaderGridRow2.Font.Bold = true;
            gvCustomers.Controls[0].Controls.AddAt(0, HeaderGridRow2);



            GridView HeaderGrid3 = (GridView)sender;
            GridViewRow HeaderGridRow3 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
            if (chk_plant.Checked == true)
            {
                reporthader = "Plant MilkStatus Report";
            }
            else if (chk_Route.Checked == true)
            {
                reporthader = "Routewise MilkStatus Report";
            }
            else if (chk_Agent.Checked == true)
            {
                reporthader = "Agentwise MilkStatus Report";
            }
            else
            {
            }

            HeaderCell3.Text = reporthader;
            HeaderCell3.Font.Size = 11;
            HeaderCell3.ColumnSpan = 8;
            HeaderCell3.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            HeaderGridRow3.Cells.Add(HeaderCell3);
            HeaderGridRow3.ForeColor = ColorTranslator.FromHtml("White");
            HeaderGridRow3.Font.Bold = true;
            gvCustomers.Controls[0].Controls.AddAt(0, HeaderGridRow3);


            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell1.Text = ddl_Plantname.SelectedItem.Text;
            HeaderCell1.Font.Size = 11;
            HeaderCell1.ColumnSpan = 8;
            HeaderCell1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            HeaderGridRow1.Cells.Add(HeaderCell1);           
            HeaderGridRow1.ForeColor = ColorTranslator.FromHtml("White");
            HeaderGridRow1.Font.Bold = true;
            gvCustomers.Controls[0].Controls.AddAt(0, HeaderGridRow1);

            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;           
            HeaderCell.Text = cname;
            HeaderCell.Font.Size = 12;
            HeaderCell.ColumnSpan = 8;
            HeaderCell.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            HeaderGridRow.Cells.Add(HeaderCell);            
            HeaderGridRow.ForeColor = ColorTranslator.FromHtml("White");
            HeaderGridRow.Font.Bold = true;
            gvCustomers.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
   
}