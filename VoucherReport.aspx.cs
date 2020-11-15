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

public partial class VoucherReport : System.Web.UI.Page
{

    int ccode = 1, pcode;
    string sqlstr = string.Empty;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    SqlCommand cmd = new SqlCommand();
    DateTime dat = new DateTime();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public string managmobNo;
    public string pname;
    public string cname;
    string d1 = string.Empty;
    string d2 = string.Empty;
    int count, count1, count2;
    DateTime dt1 = new DateTime();
    DateTime dt2 = new DateTime();
    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();
                dat = System.DateTime.Now;
                txt_FromDate.Text = dat.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dat.ToString("dd/MM/yyyy");
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
                   loadspecialsingleplant();
                   Session["Plant_Code"] = "170".ToString();
                   pcode = 170;
               }
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
             //   lblpname.Text = ddl_PlantName.SelectedItem.Text;
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
                Lbl_Errormsg.Visible = false;
                
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
    }
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        grtgrid2();
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
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = dbaccess.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            DataTable getdata = new DataTable();
            SqlDataAdapter dsii = new SqlDataAdapter(cmd);
            getdata.Rows.Clear();
            dsii.Fill(getdata);
            ddl_PlantName.DataSource = getdata;
            ddl_PlantName.DataTextField = "Plant_Name";
            ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
            ddl_PlantName.DataBind();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    public void grtgrid2()
    {

        try
        {



            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

          //  lbldate.Text = "From Date:" + dt1.ToString("dd/MM/yyyy") + "To Date:" + dt2.ToString("dd/MM/yyyy");

                  
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            SqlConnection con = new SqlConnection(connStr);
            //  string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
            string str = "SELECT  ROW_NUMBER() OVER(ORDER BY Ref_Id asc) AS Sno,convert(nvarchar,Clearing_Date,103) as ClearDate,convert(nvarchar,Inward_Date,103) as InwDate,Sess,CAST(Milk_Ltr AS DECIMAL(18,2)) AS MilkLtr,CAST(Fat AS DECIMAL(18,2)) AS Fat,CAST(Snf AS DECIMAL(18,2)) AS Snf,CAST(Rate AS DECIMAL(18,2)) AS Rate,CAST(Amount AS DECIMAL(18,2)) AS Amount  FROM Voucher_Clear  where Plant_code='" + ddl_PlantName.Text + "' and   Clearing_Date between '" + d1 + "' and '" + d2 + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            cmd.CommandTimeout = 300;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.FooterRow.Cells[2].Text = "Total";
                decimal Amilkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("MilkLtr"));
                GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[4].Text = Amilkkg.ToString("N2");

                decimal fat = dt.AsEnumerable().Sum(row => row.Field<decimal>("fat"));
                GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[5].Text = fat.ToString("N2");

                decimal snf = dt.AsEnumerable().Sum(row => row.Field<decimal>("snf"));
                GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[6].Text = snf.ToString("N2");

                decimal Rate = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rate"));
                GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[7].Text = Rate.ToString("N2");

                decimal amount = dt.AsEnumerable().Sum(row => row.Field<decimal>("amount"));
                GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[8].Text = amount.ToString("N2");


            }
            else
            {
                //string message = "Your request is being processed.";
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("alert('");
                //sb.Append(message);
                //sb.Append("');");
                //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());


             //   ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

            }
        }
        catch(Exception EX)
        {
         //   ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

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
       

      

    }
    protected void DeductionmasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
       // Datefunc();
        if (e.Row.RowType == DataControlRowType.Header)
        {



            string d1 = dt1.ToString("dd/MM/yyyy");
            string d2 = dt2.ToString("dd/MM/yyyy");
            GridViewRow HeaderRow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text =  "From Date:" + d1 + "To Date:" + d2 ;
            HeaderCell.ColumnSpan = 9;
            HeaderRow1.Cells.Add(HeaderCell);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow1);
            HeaderCell.Font.Bold = true;

            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Plant Name:" + ddl_PlantName.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 9;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
          

        }

            }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_PlantName.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                grtgrid2();

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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}