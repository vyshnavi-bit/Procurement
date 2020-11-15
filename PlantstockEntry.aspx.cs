using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;

public partial class PlantstockEntry : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string loginuser;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    int value;
    string routeid;
    int paymentvalue;
    int routevalue;
    int validateval;
    int validateval1;
    int group;
    int subgroup;
    int userid;
    int preqty;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
         //   loginuser = Session["User_ID"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
            loginuser = roleid.ToString();
            getstockcategory();
            getstockname();
            if (program.Guser_role < program.Guser_PermissionId)
            {
              
            }
            else
            {
                
            }
            getgriddata();
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
                loginuser = roleid.ToString();
            }
             else
             {

                 Server.Transfer("LoginDefault.aspx");
             }
            
        }
    }
    
    public void insertdata()
    {
        string str;
        SqlConnection con = new SqlConnection(connStr);
        try
        {
         //   str = "Insert into AdminStockMaster(StockGroup,StockSubgroup,AddedDate,qty,StockRate,UserId,stockcode)values(@StockGroup,@StockSubgroup,@AddedDate,@StockRate,@qty,@UserId,@stockcode)";

            str = "Insert into AdminStockMaster(StockGroup,StockSubgroup,AddedDate,qty,UserId,stockcode)values(@StockGroup,@StockSubgroup,@AddedDate,@qty,@UserId,@stockcode)";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
          cmd.Parameters.AddWithValue("@StockGroup", ddl_materialName.Text);
         cmd.Parameters.AddWithValue("@StockSubgroup", subgroup);
        //    cmd.Parameters.AddWithValue("@StockSubgroup", ddl_materialType.Text);
            
            cmd.Parameters.AddWithValue("@Qty", txt_qty.Text);   
            cmd.Parameters.AddWithValue("@AddedDate", System.DateTime.Now);
        //    cmd.Parameters.AddWithValue("@StockRate", txt_rate.Text);
            cmd.Parameters.AddWithValue("@UserId", loginuser);
            cmd.Parameters.AddWithValue("@stockcode", group);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            //  ShowMessage("Registered successfully......!");
            lblmsg.Text = "Registered successfully......!";
            // clear();
            lblmsg.ForeColor = System.Drawing.Color.Green;
          //  getgriddata();

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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        getstockgroupandsubgroup();
        validate();
        if ((validateval == 1) || (validateval1 == 1))
        {
            insertdata();
            getgriddata();
            clear();
          
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Please Check Your Data";


        }
       
    }
    public void validate()
    {

        try
        {
        //    if ((ddl_materialName.SelectedValue != "-1") && (ddl_materialType.SelectedValue != "-1") && (txt_qty.Text != string.Empty) && (txt_rate.Text != string.Empty))
            if ((ddl_materialName.SelectedValue != "-1") && (ddl_materialType.SelectedValue != "-1") && (txt_qty.Text != string.Empty))
            {
                validateval = 1;
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Select Stock Name";
                validateval = 0;
            }


            int qty;
            qty = Convert.ToInt16(txt_qty.Text);
            double rate;
            double assignrate;
            rate = Convert.ToDouble(txt_rate.Text);
            txt_rate.Text = rate.ToString("f2");
            assignrate = Convert.ToDouble(txt_rate.Text);
            if ((qty >= 1))
            {
                validateval1 = 1;
            }
            else
            {
                lblmsg1.Visible = true;
                lblmsg1.ForeColor = System.Drawing.Color.Red;
                lblmsg1.Text = "Please Check Entry";
                validateval = 0;              

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
    public void clear()
    {

     
        txt_qty.Text = "0";
       txt_rate.Text = "0";
    }

    public void getgriddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str;

        //    str = "Select  StockGroup,StockSubgroup,qty,stockrate,convert(varchar,AddedDate,103) as AddedDate   from AdminStockMaster order by tid desc";
            //no rate
            str = "SELECT Tid,CONVERT(NVARCHAR(15),StockGroupId)+'_'+StockGroup AS GHeader,CONVERT(NVARCHAR(15),StockSubGroupID)+'_'+StockSubGroup1 AS GSHeader,qty,AddedDate FROM " +
" (Select  Tid,stockcode,StockSubgroup,qty,convert(varchar,AddedDate,103) as AddedDate   from AdminStockMaster WHERE StockGroup='" + ddl_materialName.SelectedItem.Text.Trim() + "' ) AS t1 " +
" LEFT JOIN " +
" (SELECT StockGroup,StockSubGroup AS StockSubGroup1,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2  ON t1.stockcode=t2.StockGroupID AND t1.StockSubgroup=t2.StockSubGroupID ORDER BY Tid desc ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GridView1.HeaderStyle.BackColor = System.Drawing.Color.DarkSlateBlue;
                GridView1.HeaderStyle.Font.Bold = true;
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();

                lblmsg.Text = "NO ROWS";
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getgriddata();
    }
   



    public void getstockcategory()
    {
        try
        {
            //   ddl_agentcode.Items.Clear();
            ddl_materialName.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select   DISTINCT(StockGroup) AS StockGroup  from Stock_Master ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    ddl_materialName.Items.Add(dr["StockGroup"].ToString());                   
                    //  ddl_Plantname.Items.Add(dr["agent_id"].ToString());
                }
            }
            else
            {

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

    public void getstockname()
    {
        try
        {
            //   ddl_agentcode.Items.Clear();
            ddl_materialType.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select   DISTINCT(StockSubGroup) AS StockSubGroup  from Stock_Master  where StockGroup='" + ddl_materialName.Text.Trim() + "'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_materialType.Items.Add(dr["StockSubGroup"].ToString());
                    //  ddl_Plantname.Items.Add(dr["agent_id"].ToString());
                }
            }
            else
            {

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

    public void getqty()
    {
        try
        {
            //   ddl_agentcode.Items.Clear();
            ddl_materialType.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select   *  from AdminStockMaster  where Tid='" + userid + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                  preqty = Convert.ToInt16(dr["Qty"].ToString());
                    //  ddl_Plantname.Items.Add(dr["agent_id"].ToString());
                }
            }
            else
            {

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

    protected void ddl_materialName_SelectedIndexChanged(object sender, EventArgs e)
    {
        getstockname();
        getgriddata();
    }
    //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView1.EditIndex = e.NewEditIndex;
    //    getgriddata();
    //}
    //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    try
    //    {
    //        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        SqlConnection conn = new SqlConnection(connStr);
    //        userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
    //        getqty();
    //        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
    //        TextBox qty = (TextBox)row.Cells[4].Controls[0];
    //        //  vehicleBO.Phoneno = ((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim();
    //        //  int i = Convert.ToInt16(paymode);
        
    //            conn.Close();
    //            GridView1.EditIndex = -1;
    //            conn.Open();
    //            SqlCommand cmd = new SqlCommand("update AdminStockMaster  set qty='" + qty.Text + "',preqty='" + preqty + "',updateUserId='" + loginuser + "',updateDate='" + System.DateTime.Now + "'  where Tid='" + userid + "'", conn);
    //            cmd.ExecuteNonQuery();
    //            //  WebMsgBox.Show("Updated Successfully");
    //            lblmsg.Text = "Updated Successfully";
    //            lblmsg.ForeColor = System.Drawing.Color.Green;
    //            lblmsg.Visible = true;
    //            getgriddata();
            
    //        // loadgrid();
       
    //        //   e.Cancel = true;
    //        //  gvProducts.EditIndex = -1;
            
    //    }
    //    catch (Exception Ex)
    //    {
    //        lblmsg.Text = Ex.Message;
    //        lblmsg.Visible = true;
    //        lblmsg.ForeColor = System.Drawing.Color.Red;
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }
    //}

    //protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GridView1.EditIndex = -1;
    //    getgriddata();
    //}
    protected void btnedit_Click(object sender, EventArgs e)
    {
        getgriddata();
    }


    public void getstockgroupandsubgroup()
    {
        try
        {
            //   ddl_agentcode.Items.Clear();
            // ddl_materialName1.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select   DISTINCT(StockSubGroup) AS StockSubGroup,StockGroupID,StockSubGroupID  from Stock_Master  where  StockGroup='" + ddl_materialName.Text + "' ";
            string str = "select StockGroupID,StockSubGroupID from Stock_Master where StockGroup='" + ddl_materialName.Text + "' and StockSubgroup='" + ddl_materialType.Text + "'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    group = Convert.ToInt16(dr["StockGroupID"].ToString());
                    subgroup = Convert.ToInt16(dr["StockSubGroupID"].ToString());
                    //  ddl_Plantname.Items.Add(dr["agent_id"].ToString());
                }
            }
            else
            {
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

}