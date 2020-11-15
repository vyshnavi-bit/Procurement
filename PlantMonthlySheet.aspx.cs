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


public partial class PlantMonthlySheet : System.Web.UI.Page
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
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
      //  grtgrid();

        if (IsPostBack!= true)
        {


            //    getgrid2();

            roleid = Convert.ToInt32(Session["Role"].ToString());

            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();


            dtm = System.DateTime.Now;
            txt_FromDate.Text = dtm.ToShortDateString();
            txt_ToDate.Text = dtm.ToShortDateString();
            txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
            txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

            Button3.Visible = true;
            if (roleid < 3)
            {
                LoadSinglePlantName();
            }
            else
            {
                LoadPlantName();
            }
            pcode = ddl_PlantName.SelectedItem.Value;
        }
        else
        {
          
            pcode = ddl_PlantName.SelectedItem.Value;

            
                //    getgrid2();
            if (CheckBox1.Checked == true)
            {
                Button3.Visible = true;



            }
            else
            {
                Button3.Visible = true;


            }
           



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

    public void grtgrid()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        SqlConnection con = new SqlConnection(connStr);
   //   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
        string str = " SELECT plant_code,YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement  WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and  Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement  WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code='" + pcode + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement  where Plant_Code='" + pcode + "'  and  prdate between '"+d1+"' and '"+d2+"'   GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
       SqlCommand cmd = new SqlCommand(str, con);
      DataTable dt = new DataTable();
      cmd.CommandTimeout = 100;
      cmd.CommandType = CommandType.Text;
      SqlDataAdapter da = new SqlDataAdapter(cmd);
       da.Fill(dt);


       if (dt.Rows.Count > 0)
       {

           GridView1.DataSource = dt;
           GridView1.DataBind();




           GridView1.FooterRow.Cells[2].Text = "Total Milkkg";
           decimal milkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("JAN"));
           GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[4].Text = milkg.ToString("N2");
           if (GridView1.FooterRow.Cells[4].Text == "0.00")
           {
               GridView1.FooterRow.Cells[4].Visible = false;

           }

           decimal milkg1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("FEB"));
           GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[5].Text = milkg1.ToString("N2");

           if (GridView1.FooterRow.Cells[5].Text == "0.00")
           {
               GridView1.FooterRow.Cells[5].Visible = false;

           }
           decimal milkg2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("MAR"));
           GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[6].Text = milkg2.ToString("N2");
           if (GridView1.FooterRow.Cells[6].Text == "0.00")
           {
               GridView1.FooterRow.Cells[6].Visible = false;

           }
           decimal milkg3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("APR"));
           GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[7].Text = milkg3.ToString("N2");
           if (GridView1.FooterRow.Cells[7].Text == "0.00")
           {
               GridView1.FooterRow.Cells[7].Visible = false;

           }
           decimal milkg4 = dt.AsEnumerable().Sum(row => row.Field<decimal>("MAY"));
           GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[8].Text = milkg4.ToString("N2");
           if (GridView1.FooterRow.Cells[8].Text == "0.00")
           {
               GridView1.FooterRow.Cells[8].Visible = false;

           }
           decimal milkg5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("JUN"));
           GridView1.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[9].Text = milkg5.ToString("N2");
           if (GridView1.FooterRow.Cells[9].Text == "0.00")
           {
               GridView1.FooterRow.Cells[9].Visible = false;

           }
           decimal milkg6 = dt.AsEnumerable().Sum(row => row.Field<decimal>("JUL"));
           GridView1.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[10].Text = milkg6.ToString("N2");
           if (GridView1.FooterRow.Cells[10].Text == "0.00")
           {
               GridView1.FooterRow.Cells[10].Visible = false;

           }

           decimal milkg7 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AUG"));
           GridView1.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[11].Text = milkg7.ToString("N2");
           if (GridView1.FooterRow.Cells[11].Text == "0.00")
           {
               GridView1.FooterRow.Cells[11].Visible = false;

           }


           decimal milkg8 = dt.AsEnumerable().Sum(row => row.Field<decimal>("SEP"));
           GridView1.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[12].Text = milkg8.ToString("N2");
           if (GridView1.FooterRow.Cells[12].Text == "0.00")
           {
               GridView1.FooterRow.Cells[12].Visible = false;

           }
           decimal milkg9 = dt.AsEnumerable().Sum(row => row.Field<decimal>("OCT"));
           GridView1.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[13].Text = milkg9.ToString("N2");
           if (GridView1.FooterRow.Cells[13].Text == "0.00")
           {
               GridView1.FooterRow.Cells[13].Visible = false;

           }
           decimal milkg10 = dt.AsEnumerable().Sum(row => row.Field<decimal>("NOV"));
           GridView1.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[14].Text = milkg10.ToString("N2");
           if (GridView1.FooterRow.Cells[14].Text == "0.00")
           {
               GridView1.FooterRow.Cells[14].Visible = false;

           }
           decimal milkg11 = dt.AsEnumerable().Sum(row => row.Field<decimal>("DEC"));
           GridView1.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Left;
           GridView1.FooterRow.Cells[15].Text = milkg11.ToString("N2");


           if (GridView1.FooterRow.Cells[15].Text == "0.00")
           {
               GridView1.FooterRow.Cells[15].Visible = false;

           }


           GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
           GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
           GridView1.FooterStyle.Font.Bold = true;
       }
       else
       {
           //string message = "Your request is being processed.";
           //System.Text.StringBuilder sb = new System.Text.StringBuilder();
           //sb.Append("alert('");
           //sb.Append(message);
           //sb.Append("');");
           //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());


           ScriptManager.RegisterStartupScript(this, GetType(), "Please Check Your Data", "Showalert();", true);

       }

    }



    public void grtgrid1()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        SqlConnection con = new SqlConnection(connStr);
        //   string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
        string str = " SELECT plant_code,YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement  WHERE   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE   prdate between '" + d1 + "' and '" + d2 + "'  and  Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement  WHERE   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement  where  prdate between '" + d1 + "' and '" + d2 + "'   GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
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




            GridView1.FooterRow.Cells[2].Text = "Total Milkkg";
            decimal milkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("JAN"));
            GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[4].Text = milkg.ToString("N2");
            if (GridView1.FooterRow.Cells[4].Text == "0.00")
            {
                GridView1.FooterRow.Cells[4].Visible = false;

            }

            decimal milkg1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("FEB"));
            GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[5].Text = milkg1.ToString("N2");

            if (GridView1.FooterRow.Cells[5].Text == "0.00")
            {
                GridView1.FooterRow.Cells[5].Visible = false;

            }
            decimal milkg2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("MAR"));
            GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[6].Text = milkg2.ToString("N2");
            if (GridView1.FooterRow.Cells[6].Text == "0.00")
            {
                GridView1.FooterRow.Cells[6].Visible = false;

            }
            decimal milkg3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("APR"));
            GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[7].Text = milkg3.ToString("N2");
            if (GridView1.FooterRow.Cells[7].Text == "0.00")
            {
                GridView1.FooterRow.Cells[7].Visible = false;

            }
            decimal milkg4 = dt.AsEnumerable().Sum(row => row.Field<decimal>("MAY"));
            GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[8].Text = milkg4.ToString("N2");
            if (GridView1.FooterRow.Cells[8].Text == "0.00")
            {
                GridView1.FooterRow.Cells[8].Visible = false;

            }
            decimal milkg5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("JUN"));
            GridView1.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[9].Text = milkg5.ToString("N2");
            if (GridView1.FooterRow.Cells[9].Text == "0.00")
            {
                GridView1.FooterRow.Cells[9].Visible = false;

            }
            decimal milkg6 = dt.AsEnumerable().Sum(row => row.Field<decimal>("JUL"));
            GridView1.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[10].Text = milkg6.ToString("N2");
            if (GridView1.FooterRow.Cells[10].Text == "0.00")
            {
                GridView1.FooterRow.Cells[10].Visible = false;

            }

            decimal milkg7 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AUG"));
            GridView1.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[11].Text = milkg7.ToString("N2");
            if (GridView1.FooterRow.Cells[11].Text == "0.00")
            {
                GridView1.FooterRow.Cells[11].Visible = false;

            }


            decimal milkg8 = dt.AsEnumerable().Sum(row => row.Field<decimal>("SEP"));
            GridView1.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[12].Text = milkg8.ToString("N2");
            if (GridView1.FooterRow.Cells[12].Text == "0.00")
            {
                GridView1.FooterRow.Cells[12].Visible = false;

            }
            decimal milkg9 = dt.AsEnumerable().Sum(row => row.Field<decimal>("OCT"));
            GridView1.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[13].Text = milkg9.ToString("N2");
            if (GridView1.FooterRow.Cells[13].Text == "0.00")
            {
                GridView1.FooterRow.Cells[13].Visible = false;

            }
            decimal milkg10 = dt.AsEnumerable().Sum(row => row.Field<decimal>("NOV"));
            GridView1.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[14].Text = milkg10.ToString("N2");
            if (GridView1.FooterRow.Cells[14].Text == "0.00")
            {
                GridView1.FooterRow.Cells[14].Visible = false;

            }
            decimal milkg11 = dt.AsEnumerable().Sum(row => row.Field<decimal>("DEC"));
            GridView1.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[15].Text = milkg11.ToString("N2");


            if (GridView1.FooterRow.Cells[15].Text == "0.00")
            {
                GridView1.FooterRow.Cells[15].Visible = false;

            }


            GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
            GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
            GridView1.FooterStyle.Font.Bold = true;
        }
        else
        {
            //string message = "Your request is being processed.";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("alert('");
            //sb.Append(message);
            //sb.Append("');");
            //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());


            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

        }

    }



    public void grtgrid2()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        SqlConnection con = new SqlConnection(connStr);
         //  string str="SELECT plant_code, YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
       string str = " SELECT plant_code,YEAR,ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE plant_code='" + ddl_PlantName.Text + "' and  prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=1),0) as 'JAN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  plant_code='" + ddl_PlantName.Text + "' and prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=2),0) as 'FEB',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement  WHERE plant_code='" + ddl_PlantName.Text + "' and   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=3),0) as 'MAR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  plant_code='" + ddl_PlantName.Text + "' and  prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=4),0) as 'APR',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE plant_code='" + ddl_PlantName.Text + "' and   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=5),0) as 'MAY',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE plant_code='" + ddl_PlantName.Text + "' and   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=6),0) as 'JUN',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  plant_code='" + ddl_PlantName.Text + "' and   prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=7),0) as 'JUL',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  plant_code='" + ddl_PlantName.Text + "' and   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=8),0) as 'AUG',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  plant_code='" + ddl_PlantName.Text + "' and   prdate between '" + d1 + "' and '" + d2 + "' and Plant_Code=X.Plant_Code  AND YEAR=YEAR(prdate) AND MONTH(prdate)=9),0) as 'SEP',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE plant_code='" + ddl_PlantName.Text + "' and    prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=10),0) as 'OCT',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement WHERE  plant_code='" + ddl_PlantName.Text + "' and  prdate between '" + d1 + "' and '" + d2 + "'  and  Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=11),0) as 'NOV',ISNULL((SELECT CONVERT(DECIMAL(10,2),SUM(milk_kg)) AS milk_kg FROM Procurement  WHERE  plant_code='" + ddl_PlantName.Text + "' and prdate between '" + d1 + "' and '" + d2 + "'  and Plant_Code=X.Plant_Code AND YEAR=YEAR(prdate) AND MONTH(prdate)=12),0) as 'DEC'FROM (SELECT plant_code, YEAR(Prdate) AS YEAR FROM Procurement  where plant_code='" + ddl_PlantName.Text + "' and   prdate between '" + d1 + "' and '" + d2 + "'   GROUP BY Plant_Code, YEAR(Prdate)) X order by X.Plant_Code";
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




            GridView1.FooterRow.Cells[2].Text = "Total Milkkg";
            decimal milkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("JAN"));
            GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[4].Text = milkg.ToString("N2");
            if (GridView1.FooterRow.Cells[4].Text == "0.00")
            {
                GridView1.FooterRow.Cells[4].Visible = false;

            }

            decimal milkg1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("FEB"));
            GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[5].Text = milkg1.ToString("N2");

            if (GridView1.FooterRow.Cells[5].Text == "0.00")
            {
                GridView1.FooterRow.Cells[5].Visible = false;

            }
            decimal milkg2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("MAR"));
            GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[6].Text = milkg2.ToString("N2");
            if (GridView1.FooterRow.Cells[6].Text == "0.00")
            {
                GridView1.FooterRow.Cells[6].Visible = false;

            }
            decimal milkg3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("APR"));
            GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[7].Text = milkg3.ToString("N2");
            if (GridView1.FooterRow.Cells[7].Text == "0.00")
            {
                GridView1.FooterRow.Cells[7].Visible = false;

            }
            decimal milkg4 = dt.AsEnumerable().Sum(row => row.Field<decimal>("MAY"));
            GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[8].Text = milkg4.ToString("N2");
            if (GridView1.FooterRow.Cells[8].Text == "0.00")
            {
                GridView1.FooterRow.Cells[8].Visible = false;

            }
            decimal milkg5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("JUN"));
            GridView1.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[9].Text = milkg5.ToString("N2");
            if (GridView1.FooterRow.Cells[9].Text == "0.00")
            {
                GridView1.FooterRow.Cells[9].Visible = false;

            }
            decimal milkg6 = dt.AsEnumerable().Sum(row => row.Field<decimal>("JUL"));
            GridView1.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[10].Text = milkg6.ToString("N2");
            if (GridView1.FooterRow.Cells[10].Text == "0.00")
            {
                GridView1.FooterRow.Cells[10].Visible = false;

            }

            decimal milkg7 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AUG"));
            GridView1.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[11].Text = milkg7.ToString("N2");
            if (GridView1.FooterRow.Cells[11].Text == "0.00")
            {
                GridView1.FooterRow.Cells[11].Visible = false;

            }


            decimal milkg8 = dt.AsEnumerable().Sum(row => row.Field<decimal>("SEP"));
            GridView1.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[12].Text = milkg8.ToString("N2");
            if (GridView1.FooterRow.Cells[12].Text == "0.00")
            {
                GridView1.FooterRow.Cells[12].Visible = false;

            }
            decimal milkg9 = dt.AsEnumerable().Sum(row => row.Field<decimal>("OCT"));
            GridView1.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[13].Text = milkg9.ToString("N2");
            if (GridView1.FooterRow.Cells[13].Text == "0.00")
            {
                GridView1.FooterRow.Cells[13].Visible = false;

            }
            decimal milkg10 = dt.AsEnumerable().Sum(row => row.Field<decimal>("NOV"));
            GridView1.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[14].Text = milkg10.ToString("N2");
            if (GridView1.FooterRow.Cells[14].Text == "0.00")
            {
                GridView1.FooterRow.Cells[14].Visible = false;

            }
            decimal milkg11 = dt.AsEnumerable().Sum(row => row.Field<decimal>("DEC"));
            GridView1.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[15].Text = milkg11.ToString("N2");


            if (GridView1.FooterRow.Cells[15].Text == "0.00")
            {
                GridView1.FooterRow.Cells[15].Visible = false;

            }


            GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
            GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
            GridView1.FooterStyle.Font.Bold = true;
        }
        else
        {
            //string message = "Your request is being processed.";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("alert('");
            //sb.Append(message);
            //sb.Append("');");
            //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());


            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);

        }

    }






    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        foreach (GridViewRow row in GridView1.Rows)
        {

            string plantName = row.Cells[2].Text;
            if (plantName == "155")
            {
                row.Cells[2].Text="ARANI";


            }
            if (plantName == "156")
            {
                row.Cells[2].Text = "KAVARIPATNAM";


            }
            if (plantName == "157")
            {
                row.Cells[2].Text = "GUDLUR";


            }
            if (plantName == "158")
            {
                row.Cells[2].Text = "WALAJA";


            }
            if (plantName == "159")
            {
                row.Cells[2].Text = "VKOTA";



            }
            if (plantName == "160")
            {
                row.Cells[2].Text = "PALLIPATTU";


            }
            if (plantName == "161")
            {
                row.Cells[2].Text = "RCPURAM";


            }
            if (plantName == "162")
            {
                row.Cells[2].Text = "BOMMASAMUDRAM";


            }
            if (plantName == "163")
            {
                row.Cells[2].Text = "TARIGONDA";


            }
            if (plantName == "164")
            {
                row.Cells[2].Text = "KALASTRI";


            }
            if (plantName == "165")
            {
                row.Cells[2].Text = "CSPURAM";


            }
            if (plantName == "166")
            {
                row.Cells[2].Text = "KONDEPI";


            }
            if (plantName == "167")
            {
                row.Cells[2].Text = "KAVALI";


            }
            if (plantName == "168")
            {
                row.Cells[2].Text = "GUDIPALLIPADU";


            }



            if (row.Cells[4].Text == "0.00")
            {
                    GridView1.HeaderRow.Cells[4].Visible = false;
                   // GridView1.FooterRow.Cells[4].Visible = false;
                row.Cells[4].Visible = false;

            }
            else
            {
               GridView1.HeaderRow.Cells[4].Visible = true;
            //   GridView1.FooterRow.Cells[4].Visible = true;
                row.Cells[4].Visible = true;


            }


            if (row.Cells[5].Text == "0.00")
            {
                    GridView1.HeaderRow.Cells[5].Visible = false;
                //    GridView1.FooterRow.Cells[5].Visible = false;
                row.Cells[5].Visible = false;

            }
            else
            {
                GridView1.HeaderRow.Cells[5].Visible = true;
             //   GridView1.FooterRow.Cells[5].Visible = true;
                row.Cells[5].Visible = true;


            }



            if (row.Cells[6].Text == "0.00")
            {
                   GridView1.HeaderRow.Cells[6].Visible = false;
                 //  GridView1.FooterRow.Cells[6].Visible = false;
                row.Cells[6].Visible = false;

            }
            else
            {
                  GridView1.HeaderRow.Cells[6].Visible = true;
                //  GridView1.FooterRow.Cells[4].Visible = true;
                  row.Cells[6].Visible = true;


            }



            if (row.Cells[7].Text == "0.00")
            {
                    GridView1.HeaderRow.Cells[7].Visible = false;

                row.Cells[7].Visible = false;
           //     GridView1.FooterRow.Cells[7].Visible = false;
            }
            else
            {
                GridView1.HeaderRow.Cells[7].Visible = true;
            //    GridView1.FooterRow.Cells[7].Visible = true;
                row.Cells[7].Visible = true;


            }



            if (row.Cells[8].Text == "0.00")
            {
                  GridView1.HeaderRow.Cells[8].Visible = false;
       //           GridView1.FooterRow.Cells[8].Visible = false;
                row.Cells[8].Visible = false;

            }
            else
            {
                GridView1.HeaderRow.Cells[8].Visible = true;
         //       GridView1.FooterRow.Cells[8].Visible = true;
                row.Cells[8].Visible = true;


            }



            if (row.Cells[9].Text == "0.00")
            {
                 GridView1.HeaderRow.Cells[9].Visible = false;
          //       GridView1.FooterRow.Cells[9].Visible = false;
                row.Cells[9].Visible = false;

            }
            else
            {  
                GridView1.HeaderRow.Cells[9].Visible = true;
            //    GridView1.FooterRow.Cells[9].Visible = true;
                row.Cells[9].Visible = true;


            }



            if (row.Cells[10].Text == "0.00")
            {
                   GridView1.HeaderRow.Cells[10].Visible = false;
              //     GridView1.FooterRow.Cells[10].Visible = false;
                row.Cells[10].Visible = false;

            }
            else
            {
                 GridView1.HeaderRow.Cells[10].Visible = true;
          //       GridView1.FooterRow.Cells[10].Visible = true;
                row.Cells[10].Visible = true;


            }



            if (row.Cells[11].Text == "0.00")
            {
                  GridView1.HeaderRow.Cells[11].Visible = false;
            //      GridView1.FooterRow.Cells[11].Visible = false;
                row.Cells[11].Visible = false;

            }
            else
            {
                  GridView1.HeaderRow.Cells[11].Visible = true;
              //    GridView1.FooterRow.Cells[11].Visible = true;
                row.Cells[11].Visible = true;


            }


            if (row.Cells[12].Text == "0.00")
            {
                  GridView1.HeaderRow.Cells[12].Visible = false;
                //  GridView1.FooterRow.Cells[12].Visible = false;
                row.Cells[12].Visible = false;

            }
            else
            {
                GridView1.HeaderRow.Cells[12].Visible = true;
               // GridView1.FooterRow.Cells[12].Visible = true;
                row.Cells[12].Visible = true;


            }



            if (row.Cells[13].Text == "0.00")
            { 
                GridView1.HeaderRow.Cells[13].Visible = false;
              //  GridView1.FooterRow.Cells[13].Visible = false;
                row.Cells[13].Visible = false;

            }
            else
            {
                GridView1.HeaderRow.Cells[13].Visible = true;
              //  GridView1.FooterRow.Cells[13].Visible = true;
                row.Cells[13].Visible = true;


            }



            if (row.Cells[14].Text == "0.00")
            {
                    GridView1.HeaderRow.Cells[14].Visible = false;
                //    GridView1.FooterRow.Cells[14].Visible = false;
                row.Cells[14].Visible = false;

            }
            else
            {
                GridView1.HeaderRow.Cells[14].Visible = true;
              //  GridView1.FooterRow.Cells[14].Visible = true;
                row.Cells[14].Visible = true;


            }



            if (row.Cells[15].Text == "0.00")
            {
                   GridView1.HeaderRow.Cells[15].Visible = false;
              //     GridView1.FooterRow.Cells[15].Visible = false;
                row.Cells[15].Visible = false;

            }
            else
            {
                 GridView1.HeaderRow.Cells[15].Visible = true;
                // GridView1.FooterRow.Cells[15].Visible = true;
                row.Cells[15].Visible = true;


            }






           


        }



    }


   

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btn_export_Click(object sender, EventArgs e)
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
                GridView2.AllowPaging = false;
                getgrid2();
              //  grtgrid1();

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

        }


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


   

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {

            //    getgrid2();
            if (CheckBox1.Checked == true)
            {
                Button3.Visible = true;

                grtgrid1();
                GridView1.Visible = true;
                GridView2.Visible = false;

            }
            else
            {
                Button3.Visible = true;
                grtgrid();
                GridView1.Visible = true;
                GridView2.Visible = false;

            }
        }

        catch
        {



            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Check Your Data !!');", true);
            GridView1.Visible = false;

        }


        //grtgrid();
        //GridView1.Visible = true;
        //GridView2.Visible = false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }




    protected void GetSelectedRecords(object sender, EventArgs e)
    {


        getgrid2();






    }

    public void getgrid2()
    {

        GridView1.Visible = false;
        GridView2.Visible = true;
        grtgrid2();
        //DataTable dtt = new DataTable();
        dtt.Columns.AddRange(new DataColumn[14] { new DataColumn("Plant_code"), new DataColumn("YEAR"), new DataColumn("JAN"), new DataColumn("FEB"), new DataColumn("MAR"), new DataColumn("APR"), new DataColumn("MAY"), new DataColumn("JUN"), new DataColumn("JUL"), new DataColumn("AUG"), new DataColumn("SEP"), new DataColumn("OCT"), new DataColumn("NOV"), new DataColumn("DEC") });
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                //if (chkRow.Checked)
                //{
               // string SNO = row.Cells[0].Text;
                string PLANTCODE = row.Cells[2].Text;
                string YEAR = row.Cells[3].Text;
                double JAN = Convert.ToDouble(row.Cells[4].Text);
                string FEB = row.Cells[5].Text;
                string MAR = row.Cells[6].Text;
                string APR = row.Cells[7].Text;
                string MAY = row.Cells[8].Text;
                string JUNE = row.Cells[9].Text;
                string JULY = row.Cells[10].Text;
                string AUG = row.Cells[11].Text;
                string SEP = row.Cells[12].Text;
                string OCT = row.Cells[13].Text;
                string NOV = row.Cells[14].Text;
                string DEC = row.Cells[15].Text;

                //string PLANTCODE = row.Cells[1].Text;
                //string YEAR = row.Cells[2].Text;
                //double JAN = Convert.ToDouble(row.Cells[3].Text);
                //string FEB = row.Cells[4].Text;
                //string MAR = row.Cells[5].Text;
                //string APR = row.Cells[6].Text;
                //string MAY = row.Cells[7].Text;
                //string JUNE = row.Cells[8].Text;
                //string JULY = row.Cells[9].Text;
                //string AUG = row.Cells[10].Text;
                //string SEP = row.Cells[11].Text;
                //string OCT = row.Cells[12].Text;
                //string NOV = row.Cells[13].Text;
                //string DEC = row.Cells[14].Text;
                    dtt.Rows.Add(PLANTCODE, YEAR, JAN, FEB, MAR, APR, MAY, JUNE, JULY, AUG, SEP, OCT, NOV, DEC);
                //}
            }
        }
        try
        {

            GridView2.DataSource = dtt;
            GridView2.DataBind();
            GridView2.FooterRow.Cells[2].Text = "Total Milkkg";

            double sum = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum += Convert.ToDouble(this.GridView2.Rows[index].Cells[4].Text);
            GridView2.FooterRow.Cells[4].Text = sum.ToString();
            if (GridView2.FooterRow.Cells[4].Text == "0")
            {
                GridView2.FooterRow.Cells[4].Visible = false;

            }
            double sum1 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum1 += Convert.ToDouble(this.GridView2.Rows[index].Cells[5].Text);
            GridView2.FooterRow.Cells[5].Text = sum1.ToString();
            if (GridView2.FooterRow.Cells[5].Text == "0")
            {
                GridView2.FooterRow.Cells[5].Visible = false;

            }
            double sum2 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum2 += Convert.ToDouble(this.GridView2.Rows[index].Cells[6].Text);
            GridView2.FooterRow.Cells[6].Text = sum2.ToString();
            if (GridView2.FooterRow.Cells[6].Text == "0")
            {
                GridView2.FooterRow.Cells[6].Visible = false;

            }
            double sum3 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum3 += Convert.ToDouble(this.GridView2.Rows[index].Cells[7].Text);
            GridView2.FooterRow.Cells[7].Text = sum3.ToString();
            if (GridView2.FooterRow.Cells[7].Text == "0")
            {
                GridView2.FooterRow.Cells[7].Visible = false;

            }
            double sum4 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum4 += Convert.ToDouble(this.GridView2.Rows[index].Cells[8].Text);
            GridView2.FooterRow.Cells[8].Text = sum4.ToString();
            if (GridView2.FooterRow.Cells[8].Text == "0")
            {
                GridView2.FooterRow.Cells[8].Visible = false;

            }
            double sum5 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum5 += Convert.ToDouble(this.GridView2.Rows[index].Cells[9].Text);
            GridView2.FooterRow.Cells[9].Text = sum5.ToString();
            if (GridView2.FooterRow.Cells[9].Text == "0")
            {
                GridView2.FooterRow.Cells[9].Visible = false;

            }
            double sum6 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum6 += Convert.ToDouble(this.GridView2.Rows[index].Cells[10].Text);
            GridView2.FooterRow.Cells[10].Text = sum6.ToString();
            if (GridView2.FooterRow.Cells[10].Text == "0")
            {
                GridView2.FooterRow.Cells[10].Visible = false;

            }
            double sum7 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum7 += Convert.ToDouble(this.GridView2.Rows[index].Cells[11].Text);
            GridView2.FooterRow.Cells[11].Text = sum7.ToString();

            if (GridView2.FooterRow.Cells[11].Text == "0")
            {
                GridView2.FooterRow.Cells[11].Visible = false;

            }
            double sum8 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum8 += Convert.ToDouble(this.GridView2.Rows[index].Cells[12].Text);
            GridView2.FooterRow.Cells[12].Text = sum8.ToString();
            if (GridView2.FooterRow.Cells[12].Text == "0")
            {
                GridView2.FooterRow.Cells[12].Visible = false;

            }
            double sum9 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum9 += Convert.ToDouble(this.GridView2.Rows[index].Cells[13].Text);
            GridView2.FooterRow.Cells[13].Text = sum9.ToString();
            if (GridView2.FooterRow.Cells[13].Text == "0")
            {
                GridView2.FooterRow.Cells[13].Visible = false;

            }
            double sum10 = 0;
            for (int index = 0; index < this.GridView2.Rows.Count; index++)
                sum10 += Convert.ToDouble(this.GridView2.Rows[index].Cells[14].Text);
            GridView2.FooterRow.Cells[14].Text = sum10.ToString();
            //if (GridView2.FooterRow.Cells[14].Text == "0")
            //{
            //    GridView2.FooterRow.Cells[14].Visible = false;

            //}
            //double sum11 = 0;
            //for (int index = 0; index < this.GridView2.Rows.Count; index++)
            //    sum11 += Convert.ToDouble(this.GridView2.Rows[index].Cells[15].Text);
            //GridView2.FooterRow.Cells[15].Text = sum11.ToString();
            //if (GridView2.FooterRow.Cells[15].Text == "0")
            //{
            //    GridView2.FooterRow.Cells[15].Visible = false;

            //}
            //double sum12 = 0;
            //for (int index = 0; index < this.GridView2.Rows.Count; index++)
            //    sum12 += Convert.ToDouble(this.GridView2.Rows[index].Cells[16].Text);
            //GridView2.FooterRow.Cells[16].Text = sum12.ToString();
            //if (GridView1.FooterRow.Cells[16].Text == "0.00")
            //{
            //    GridView1.FooterRow.Cells[16].Visible = false;

            //}
            GridView2.FooterStyle.ForeColor = System.Drawing.Color.White;
            GridView2.FooterStyle.BackColor = System.Drawing.Color.Brown;
            GridView2.FooterStyle.Font.Bold = true;

        }
        catch (Exception ex)
        {

      //      string display = ex.ToString();
      ////      ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
      //      ScriptManager.RegisterStartupScript(
      //     this,
      //     this.GetType(),
      //     "MessageBox",
      //     "alert('this is message Box');",
      //     true);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyScript", "alert('Please Select Any One Plant !!');", true);

        }

    }



    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        foreach (GridViewRow row in GridView2.Rows)
        {

            if (row.Cells[3].Text == "0")
            {
                GridView2.HeaderRow.Cells[3].Visible = false;
                //  GridView2.FooterRow.Cells[4].Visible = false;
                row.Cells[3].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[3].Visible = true;
                //  GridView2.FooterRow.Cells[4].Visible = true;
                row.Cells[3].Visible = true;


            }


            if (row.Cells[4].Text == "0")
            {
                GridView2.HeaderRow.Cells[4].Visible = false;
              //  GridView2.FooterRow.Cells[4].Visible = false;
                row.Cells[4].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[4].Visible = true;
              //  GridView2.FooterRow.Cells[4].Visible = true;
                row.Cells[4].Visible = true;


            }


            if (row.Cells[5].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[5].Visible = false;
               // GridView2.FooterRow.Cells[5].Visible = false;
                row.Cells[5].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[5].Visible = true;
               // GridView2.FooterRow.Cells[5].Visible = true;
                row.Cells[5].Visible = true;


            }



            if (row.Cells[6].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[6].Visible = false;
             //   GridView2.FooterRow.Cells[6].Visible = false;
                row.Cells[6].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[6].Visible = true;
            //    GridView2.FooterRow.Cells[6].Visible = true;
                row.Cells[6].Visible = true;


            }



            if (row.Cells[7].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[7].Visible = false;
             //   GridView2.FooterRow.Cells[7].Visible = false;
                row.Cells[7].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[7].Visible = true;
            //    GridView1.FooterRow.Cells[7].Visible = true;
                row.Cells[7].Visible = true;


            }



            if (row.Cells[8].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[8].Visible = false;
           //     GridView2.FooterRow.Cells[8].Visible = false;
                row.Cells[8].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[8].Visible = true;
             //   GridView2.FooterRow.Cells[8].Visible = true;
                row.Cells[8].Visible = true;


            }



            if (row.Cells[9].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[9].Visible = false;
          //      GridView2.FooterRow.Cells[9].Visible = false;
                row.Cells[9].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[9].Visible = true;
           //     GridView2.FooterRow.Cells[9].Visible = true;
                row.Cells[9].Visible = true;


            }



            if (row.Cells[10].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[10].Visible = false;
          //      GridView2.FooterRow.Cells[10].Visible = false;
                row.Cells[10].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[10].Visible = true;
             //   GridView2.FooterRow.Cells[10].Visible = true;
                row.Cells[10].Visible = true;


            }



            if (row.Cells[11].Text == "0.00")
            {
               GridView2.HeaderRow.Cells[11].Visible = false;
             //   GridView2.FooterRow.Cells[11].Visible = false;
                row.Cells[11].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[11].Visible = true;
            //    GridView2.FooterRow.Cells[11].Visible = true;
                row.Cells[11].Visible = true;


            }


            if (row.Cells[12].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[12].Visible = false;
               // GridView2.FooterRow.Cells[12].Visible = false;
                row.Cells[12].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[12].Visible = true;
              //  GridView2.FooterRow.Cells[12].Visible = true;
                row.Cells[12].Visible = true;


            }



            if (row.Cells[13].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[13].Visible = false;
              //  GridView2.FooterRow.Cells[13].Visible = false;
                row.Cells[13].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[13].Visible = true;
              //  GridView2.FooterRow.Cells[13].Visible = true;
                row.Cells[13].Visible = true;


            }



            if (row.Cells[14].Text == "0.00")
            {
                GridView2.HeaderRow.Cells[14].Visible = false;
                //   GridView2.FooterRow.Cells[14].Visible = false;
                row.Cells[14].Visible = false;

            }
            else
            {
                GridView2.HeaderRow.Cells[14].Visible = true;
                //   GridView2.FooterRow.Cells[14].Visible = false;
                row.Cells[14].Visible = true;


            }



            //if (row.Cells[15].Text == "0.00")
            //{
            //    GridView2.HeaderRow.Cells[15].Visible = false;
            // //   GridView2.FooterRow.Cells[15].Visible = false;
            //    row.Cells[15].Visible = false;

            //}
            //else
            //{
            //    GridView2.HeaderRow.Cells[15].Visible = true;
            // //   GridView2.FooterRow.Cells[15].Visible = false;
            //    row.Cells[15].Visible = true;


            //}
        }

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {



        if (CheckBox1.Checked == true)
        {
            Label9.Visible = false;
            ddl_PlantName.Visible = false;
            Button3.Visible = true;
          //  grtgrid1();

        }
        else
        {
            Label9.Visible = true;
            ddl_PlantName.Visible = true;

            Button3.Visible = true;


        }


    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "PlantName:" + ddl_PlantName.SelectedItem.Text + "Monthly Report";
            HeaderCell2.ColumnSpan = 6;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;


        }
    }
}