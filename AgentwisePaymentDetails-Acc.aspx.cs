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

public partial class AgentwisePaymentDetails_Acc : System.Web.UI.Page
{
    BLLuser Bllusers = new BLLuser();
    DbHelper db = new DbHelper();
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //DISPLAY = "ARANI BILL  FROM[11-06-2016]TO[20-06-2016] AMOUNT:540000";
                //Response.Write("<marquee>   <span style= 'color:BLUE'>     " + DISPLAY + " </span> </marquee>");
                //	managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";

                }
                //GetprourementIvoiceData();
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
                pcode = ddl_Plantcode.SelectedItem.Value;
                //	pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                //	managmobNo = Session["managmobNo"].ToString();

                //Response.Write("<marquee>   <span style= 'color:BLUE'>     " + DISPLAY + " </span> </marquee>");
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getgrid();
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
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
                ddl_Plantcode.Items.Add("--Select Plantcode--");
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
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
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


    public void getgrid()
    {


        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        string str = "";
        str = "select agent_id as  AGENTID,agent_accountno as  ACCOUNTNO, CONVERT(INT,Netamount) AS NETAMOUNT,agent_name as AGENTNAME,ifsc_code as IFSCCODE,Bank_Name  as BANKNAME from paymentData where plant_code='" + pcode + "'  and frm_date='" + d1 + "' and to_date='" + d2 + "' and payment_mode='" + Rdo1 .Text+ "'  order by agent_id asc";
        con = db.GetConnection();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        

        if (dt.Rows.Count > 0)
        {
         GridView1.DataSource = dt;
         GridView1.DataBind();

         GridView1.FooterRow.Cells[2].Text = "TOTAL AMOUNT";

         

         int milkkg = dt.AsEnumerable().Sum(row => row.Field<int>("Netamount"));
         GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
         GridView1.FooterRow.Cells[3].Text = milkkg.ToString("0.00");   



        }
        else
        {

            GridView1.DataSource = "No Records";
            GridView1.DataBind();

        }


    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)  
        {
            try
            {
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[3].Font.Bold = true;
            }
            catch
            {


            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            try
            {
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                string GET = e.Row.Cells[3].Text + ".00";
                e.Row.Cells[3].Text = GET;
            }
            catch
            {


            }

            //e.Row.Cells[3].Font.Bold = true;

        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            TableCell HeaderCell4 = new TableCell();

            HeaderCell3.Text = "BankPayment Details For"   +   ddl_Plantname.Text + ":" +    txt_FromDate.Text + "To" +  txt_ToDate.Text ;
            HeaderCell3.ColumnSpan = 16;
            HeaderCell3.Attributes.CssStyle["text-align"] = "center";
            HeaderRow.Cells.Add(HeaderCell3);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell3.Font.Bold = true;
            HeaderCell3.ForeColor = System.Drawing.Color.BlueViolet;
            HeaderCell3.BackColor = System.Drawing.Color.White;
        }


    }
}