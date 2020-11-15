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


public partial class DataReceivedAndImportTime_ : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;
    public int refNo = 0;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
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
              //  managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;
                txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
                txt_ToDate.Text = tdt.ToString("dd/MM/yyy");

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
                Server.Transfer("LoginDefault.aspx");
            }

        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_PlantName.SelectedItem.Value;

            //    Lbl_Errormsg.Visible = false;
              

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }


        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {


        getDate();
        gridview();
        gridview1();





    }

    public void gridview()
    {
        //GridView1.HeaderStyle.BackColor = Color.Silver;
        //GridView1.HeaderStyle.ForeColor = Color.White;
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
              //  CONVERT(VARCHAR(17),GETDATE(),113) as rdate

                string sqlstr = "select   Distinct(CONVERT(nvarchar(15),Pdate,103)) AS pdate,Sess,convert(varchar(17),rdate,113) as rdate  from  proimportinfo where PlantCode='" + ddl_PlantName.SelectedItem.Value + "' and Pdate between '" + d1 + "' and '" + d2 + "'   ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {






                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    GridView1.HeaderStyle.BackColor = Color.Brown;
                    GridView1.HeaderStyle.ForeColor = Color.White;



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

    public void gridview1()
    {

        try
        {

            //GridView2.HeaderStyle.BackColor = Color.Silver;
            //GridView2.HeaderStyle.ForeColor = Color.White;

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


                string sqlstr = "select   Distinct(CONVERT(nvarchar(15),Pdate,103)) AS pdate,Sess, CONVERT(VARCHAR(17),rdate,113) as rdate  from  proinfo where PlantCode='" + ddl_PlantName.SelectedItem.Value + "' and Pdate between '" + d1 + "' and '" + d2 + "' ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {






                    GridView2.DataSource = dt;
                    GridView2.DataBind();

                    GridView2.HeaderStyle.BackColor = Color.Brown;
                    GridView2.HeaderStyle.ForeColor = Color.White;



                }
                else
                {

                    GridView2.DataSource = null;
                    GridView2.DataBind();

                }



            }
        }
        catch
        {



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


    public void getDate()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
        
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from BANK_MASTER   where plant_code='" + pcode + "' and Bank_id='" + ddl_bankid.Text + "'";

            string str = "select   top 1   convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate from Bill_date   where plant_code='" + ddl_PlantName.SelectedItem.Value + "'   order  by tid desc";
            //  string str= = "select * from Bill_date   where plant_code='" + pcode + "' and Bill_frmdate='"+txt_FromDate.Text+"'    Bill_todate='" + txt_ToDate.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                // ddl_ccode.Items.Add(dr["company_code"].ToString());

                txt_FromDate.Text = dr["Bill_frmdate"].ToString();
                txt_ToDate.Text = dr["Bill_todate"].ToString();

            }


        }

        catch
        {

            WebMsgBox.Show("ERROR");

        }



    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {

           // e.Row.Cells[0].ForeColor = System.Drawing.Color.Brown;
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);


              TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Received Data Details";
            HeaderCell2.ColumnSpan =4;
            HeaderRow.Cells.Add(HeaderCell2);
            HeaderCell2.ForeColor = System.Drawing.Color.White;
            HeaderCell2.Font.Bold = true;
           // HeaderCell2.ControlStyle.
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow); 


        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {

            // e.Row.Cells[0].ForeColor = System.Drawing.Color.Brown;
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Imported Data Details";
            HeaderCell2.ColumnSpan = 5;
            HeaderRow.Cells.Add(HeaderCell2);
            HeaderCell2.ForeColor = System.Drawing.Color.White;
            HeaderCell2.Font.Bold = true;
            // HeaderCell2.ControlStyle.
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);
            

        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}