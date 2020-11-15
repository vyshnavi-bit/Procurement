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

public partial class DonatedMilkReports : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DataTable dtt = new DataTable();
    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    DateTime dt1 = new DateTime();
    DateTime dt2 = new DateTime();
    string d1;
    string d2;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
          //    getgrid2();
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();

            roleid = Convert.ToInt32(Session["Role"].ToString());
            dtm = System.DateTime.Now;
            txt_FromDate.Text = dtm.ToShortDateString();
            txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");



            if (roleid > 2)
            {
                LoadPlantName();
            }

            // pcode = ddl_PlantName.SelectedItem.Value;
        }
        else
        {

            pcode = ddl_PlantName.SelectedItem.Value;


            //    getgrid2();





        }
    }

    private void LoadPlantName()
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

    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        getgrid();
    }

    public void getgrid()
    {

        try
        {



            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            d1 = dt1.ToString("MM/dd/yyyy");
            SqlConnection con = new SqlConnection(connStr);
            //   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
            string str;
          //  str = "SELECT  ROW_NUMBER() OVER (ORDER BY ManualDate) AS [Sno], convert(varchar,ManualDate,103) as ManualDate,MannualSession as Session,GivererName as Given,RequsterName AS Request,ReasonForMannual AS ReasonForManual    FROM  MannualSettings  where plant_code='" + pcode + "' and    ManualDate between '" + d1 + "'   and  '" + d2 + "'  order by ManualDate";
            str = "select *  from (select  AgentId,agent_name as AgentName,DonateDate,DonatedSession,ActualMilk,DonatedMilk from(Select  AgentId,convert(varchar,DonateDate,103) as DonateDate,DonatedSession,ActualMilk,DonatedMilk  from  DonatedMilk  where PlantCode='" + pcode + "' and DonateDate='" + d1 + "') as a  left join(select agent_id,agent_name  from agent_master where plant_code='" + pcode + "') as am  on  a.AgentId=am.agent_id) as aaa  order by rand(AgentId)";
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            cmd.CommandTimeout = 500;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.FooterRow.Cells[4].Text = "Total";
                GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                decimal ActualMilk = dt.AsEnumerable().Sum(row => row.Field<decimal>("ActualMilk"));
                GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[5].Text = ActualMilk.ToString("N2");

                decimal DonatedMilk = dt.AsEnumerable().Sum(row => row.Field<decimal>("DonatedMilk"));
                GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[6].Text = DonatedMilk.ToString("N2");
               

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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string get1 = dt1.ToString("dd/MM/yyyy");
            GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell21 = new TableCell();
            HeaderCell21.Text = " Date:" + get1  ;
            HeaderCell21.ColumnSpan = 7;
            HeaderRow1.Cells.Add(HeaderCell21);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow1);
            HeaderCell21.Font.Bold = true;
            HeaderCell21.HorizontalAlign = HorizontalAlign.Center;
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            string get = ddl_PlantName.SelectedItem.Text;
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddl_PlantName.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 7;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;

            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

        }


    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                getgrid();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
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
                string style = @"<style> .textmode { } </style>";
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


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}