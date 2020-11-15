using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class ChartBelowRange : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    DataTable dt = new DataTable();
    string msg;
    Exception ex;
    string plant;
    string[] GET;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                dtm = System.DateTime.Now;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                dtm = System.DateTime.Now;
                Geplant();

               
            }
            else
            {



            }

        }


        else
        {
            ccode = Session["Company_code"].ToString();
            //  LoadPlantcode();
          
            pcode = ddl_Plantname.SelectedValue.ToString();
            pname = ddl_Plantname.SelectedItem.ToString();
        }
    }

    public void getgridviewrows()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string get = "";
            con = DB.GetConnection();
            get = "select (aa.Agent_id  + '_' + Agent_Name) as AgentName,Date,Sessions,Fat,Snf,Milk_kg,rate as Rate  from ( sELECT  CONVERT(varchar,Agent_id) as Agent_id,convert(varchar,Prdate,103) as Date,Sessions,fat,snf,Milk_kg,rate    from PROCUREMENT   WHERE PLANT_CODE='"+pcode+"' AND PRDATE BETWEEN '"+d1+"' AND '"+d2+"' and fat < 4.0   ) as aa left join (Select Agent_Id,Agent_Name  from Agent_Master where plant_code='"+pcode+"' ) as am on  aa.Agent_id=am.Agent_Id   order by Date";
            SqlCommand cmd = new SqlCommand(get, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();

                double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("Milk_kg"));
                GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[6].Text = milkkg.ToString("0.00");

                double Rate = dt.AsEnumerable().Sum(row => row.Field<double>("Rate"));
                GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[7].Text = Rate.ToString("0.00");


             
            }
            else
            {
                GridView1.DataSource = "No Records";
                GridView1.DataBind();

            }

        }
        catch (Exception ex)
        {
            //  getcatmsg();
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }

    public void Geplant()
    {
        con = DB.GetConnection();
        string stt = "";
        stt = "select Plant_Name,plant_code from Plant_Master where plant_code not in(139,150)";
        SqlCommand cmd = new SqlCommand(stt, con);
        DataSet dg = new DataSet();
        SqlDataAdapter dap = new SqlDataAdapter(cmd);
        dap.Fill(dg, ("RmupdateStatus"));
        ddl_Plantname.DataSource = dg.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "plant_code";
        ddl_Plantname.DataBind();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            getgridviewrows();
        }
        catch (Exception ex)
        {
            //  getcatmsg();
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddl_Plantname.SelectedItem.Text + ":Low Fat Range:" + "Date between:" + txt_FromDate.Text+" To"+ txt_ToDate.Text ;
            HeaderCell2.ColumnSpan = 8;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
           
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

           
            e.Row.Cells[6].Font.Bold = true;
            e.Row.Cells[7].Font.Bold = true;

        }
    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
}