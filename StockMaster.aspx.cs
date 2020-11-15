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
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;

public partial class StockMaster : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    public string uid;
    double getavil;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    DateTime dtm = new DateTime();
    DateTime dtm2 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection();
    string stocktype;
    string stockname;
    string stockweight;
    public string loginuser;
    int GROUPID;
    int SUBGROUPID;
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
              //  loginuser = Session["User_ID"].ToString();
                loginuser = roleid.ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                lblmsg.Visible = false;

                gridview();
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
            lblmsg.Visible = false;
            gridview();
          //  loginuser = roleid.ToString();
            loginuser = roleid.ToString();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
              
    
   
    public void gridview()
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlstr = "SELECT ROW_NUMBER() OVER(ORDER BY date desc ) AS Sno,StockGroup,StockSubGroup,convert(varchar,date,103) as AddedDate from Stock_Master Where StockGroup='" + ddl_stocktype.SelectedItem.Text.Trim() + "'    order By  tid desc ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);                
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
     protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if ((ddl_stocktype.SelectedItem.Text != "------Select------") && (txt_stockname.Text != string.Empty))
            {
                INSERT();
                txt_stockname.Text = "";
                txt_stockname.Focus();
            }
            else
            {
                txt_stockname.Focus();
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Please Check Your Data";

            }
        }

        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
        }
    }

    public void INSERT()
    {    
    
        GETSUBGROUPID();
        try
        {          
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();     
            cmd.CommandText = "INSERT INTO Stock_Master (StockGroup,StockSubGroup,date,userid,StockGroupID,StockSubGroupID) VALUES ('" + ddl_stocktype.SelectedItem.Text + "','" + txt_stockname.Text + "','" + System.DateTime.Now + "','" + loginuser + "','" + ddl_stocktype.SelectedItem.Value + "','" + SUBGROUPID + "')";
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Text = "Inserted SuccesFully";
            gridview();       
            GETSUBGROUPID();
            clear();         
            conn.Close();
        }
        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }

    public void clear()
    {    
        txt_stockname.Text = "";    
    }
    protected void txt_stockname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getstockcategory();

            if ((ddl_stocktype.SelectedItem.Text == stocktype.Trim()) && (txt_stockname.Text == stockname.Trim()))
            {
                txt_stockname.Text = "";
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "StockName Already Available...";
                txt_stockname.Focus();
            }
            else
            {
                Button1.Focus();
            }
        }

        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
        }
    }


    public void getstockcategory()
    {
        try
        {         
            stockname = string.Empty;
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select distinct  StockGroup,StockSubGroup from Stock_Master  where StockGroup='" + ddl_stocktype.SelectedItem.Text+ "' and  StockSubGroup='" + txt_stockname.Text + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    stocktype = dr["StockGroup"].ToString();
                    stockname = dr["StockSubGroup"].ToString();               

                }
            }
            else
            {
                stockname = "";
            }
        }

        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }        

    }
    protected void txt_stockweight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getstockcategory();
            if ((stockname == txt_stockname.Text) && (stockweight == txt_stockweight.Text) && (stocktype == ddl_stocktype.Text))
            {
                WebMsgBox.Show("Altready Available This Item");

                txt_stockweight.Text = "";
                txt_stockweight.Focus();
            }
        }

        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
        }

    }

    //public void GETGROUPID()
    //{
    //    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    {
    //        conn.Open();
    //        //SqlDataReader dr = null;
    //        //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
    //        DataSet ds = new DataSet();
    //        DataSet ds1 = new DataSet();

    //        try
    //        {
    //            string sqlstr = "select MAX(StockGroup) AS TID from Stock_Master";
    //            SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

    //            adp.Fill(ds);
    //            int id = Convert.ToInt16(ds.Tables[0].Rows[0]["TID"]);
    //            GROUPID = (id + 1);
    //        }
    //        catch
    //        {
    //            GROUPID = 1;


    //        }
    //    }
    //}

    public void GETSUBGROUPID()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();       
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            try
            {
                string sqlstr = "select MAX(StockSubGroupID) AS TID from Stock_Master where   StockGroup='" + ddl_stocktype.SelectedItem.Text + "' ";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);
                adp.Fill(ds);
                int id = Convert.ToInt16(ds.Tables[0].Rows[0]["TID"]);
                SUBGROUPID = (id + 1);
            }
            catch
            {
                SUBGROUPID = 1;

            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("DeductionsForms.aspx");
    }
    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            gridview();
        }
        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
        }

    }
    protected void ddl_stocktype_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridview();
    }
}