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
using System.IO;

public partial class addnewbank : System.Web.UI.Page 
{
    DbHelper df = new DbHelper();
    DataTable dsr = new DataTable();
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            gridview();
            gettid();
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {

            if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
            {
                INSERT();
                WebMsgBox.Show("inserted Successfully");
                TextBox2.Text = "";
            }
            else
            {

                WebMsgBox.Show("Please Check Data");
            }

            gettid();

            gridview();


          //  getvendor();

        }
    }


    //public void getvendor()
    //{

    //    con = df.GetConnection();

    //    String set = "";
    //    set = "Select Vedorcode,Vedorid,plant_code FROM  Vendor    order by Vedorid asc";
    //    SqlCommand cmd=new SqlCommand(set,con);
    //    SqlDataAdapter dfr=new SqlDataAdapter(cmd);
    //    dsr.Rows.Clear();
    //    dfr.Fill(dsr);
    //    foreach (DataRow Drt in dsr.Rows)
    //    {
    //       string getcode= Drt[0].ToString();
    //       string getid = Drt[1].ToString();
    //       string pcode = Drt[2].ToString();
    //       string updates = "";
    //       con = df.GetConnection();
    //       updates = "update Agent_Master set VendorCode='" + getcode + "'  where plant_code='" + pcode + "'  and agent_id='" + getid + "'";
    //       SqlCommand cmd2 =new SqlCommand(updates,con);
    //       cmd2.ExecuteNonQuery();


    //    }




    //}
    public void gettid()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            //SqlDataReader dr = null;
            //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            string sqlstr = "SELECT max(bank_id) as  tid  FROM Bank_Details";
            SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

            adp.Fill(ds);
            int id = Convert.ToInt16(ds.Tables[0].Rows[0]["tid"]);
            TextBox1.Text = (id + 1).ToString();
        }
    }

    public void gridview()
    {
        
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sqlstr = "SELECT Bank_id,Bank_name FROM Bank_Details    order by bank_id";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
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
        
        
    

    protected void Button1_Click1(object sender, EventArgs e)
    {
        //if (IsPostBack == true)
        //{

        //    if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        //    {
        //        INSERT();
        //        WebMsgBox.Show("inserted Successfully");
        //    }
        //    else
        //    {

        //        WebMsgBox.Show("Please Check Data");
        //    }

        //    gettid();

        //    gridview();
        //}
    }
    public void INSERT()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
      
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = "INSERT INTO Bank_Details (BANK_ID,BANK_NAME) VALUES ('" + TextBox1.Text + "','" + TextBox2.Text + "')";

        cmd.Connection = conn;

        conn.Open();

        cmd.ExecuteNonQuery();

        conn.Close();
        
    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
        Label lblID = (Label)row.FindControl("bank_id");
      //  TextBox bid = (TextBox)row.Cells[0].Controls[0];
        TextBox bname = (TextBox)row.Cells[1].Controls[0];
        GridView1.EditIndex = -1;
        conn.Open();

        //TextBox Name = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCustName");

       SqlCommand cmd = new SqlCommand("update bank_details set bank_name='" + bname.Text + "' where bank_id='" + userid + "'", conn);
       cmd.ExecuteNonQuery();
       WebMsgBox.Show("inserted Successfully");
     
       gridview();




    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        gridview();
       
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridview();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string value = e.Row.Cells[1].Text;
        //}
    }
}