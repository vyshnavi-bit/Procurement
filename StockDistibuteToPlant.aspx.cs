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
public partial class StockDistibuteToPlant : System.Web.UI.Page
{
    BLLPlantName BllPlant = new BLLPlantName();
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
    DataSet ds = new DataSet();
    DbHelper dbacess = new DbHelper(); 
    int value;
    string routeid;
    int paymentvalue;
    int routevalue;
    int validateval;
    int validateval1;
    int adminqty;
    int plantqty;
    int gteplantstockqty;
    int getqty;
    double rate;
    double total;
    int group;
    int subgroup;
    int userid;
    int preqty;
    int getstocksubgroupid;
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
             //   loginuser = Session["User_ID"].ToString();
                loginuser = roleid.ToString();
                if((roleid>=3) && (roleid!=9))
                {
                LoadPlantName();
                }
                if (roleid == 9)
                {
                loadspecialsingleplant();
                }
                getstockcategory();
                getstockname();
                getstockgroupandsubgroup();
                //   getMaterialqty();
                checkadminqty();
                checkplantqty();

                if (adminqty > plantqty)
                {
                    getqty = adminqty - plantqty;
                    //  txt_qty.Text = "0";
                    availtxt_qty.Text = getqty.ToString();

                }

                getqty = Convert.ToInt16(availtxt_qty.Text);

                //   availtxt_qty.Text = getqty.ToString();

                int qtyy = Convert.ToInt16(txt_qty.Text);



                if (qtyy <= getqty)
                {

                    availtxt_qty.Text = (getqty - qtyy).ToString();

                }
                getgriddata();
                //    getMaterialrate();
                lblmsg.Visible = false;

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

                try
                {

                 //   loginuser = Session["User_ID"].ToString();
                    loginuser = roleid.ToString();

                    lblmsg.Visible = false;

                    //getstockgroupandsubgroup();

                     ////--

                    //if (adminqty > plantqty)
                    //{
                    //    getqty = adminqty - plantqty;
                    //    //  txt_qty.Text = "0";
                    //    availtxt_qty.Text = getqty.ToString();

                    //}

                    //getqty = Convert.ToInt16(availtxt_qty.Text);

                    ////   availtxt_qty.Text = getqty.ToString();

                    //int qtyy = Convert.ToInt16(txt_qty.Text);



                    //if (qtyy <= getqty)
                    //{

                    //    availtxt_qty.Text = (getqty - qtyy).ToString();

                    //}
                    ////--

                }
                catch (Exception ex)
                {
                }
            }

            else
            {

                Server.Transfer("LoginDefault.aspx");
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


    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = dbacess.GetConnection();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }


    public void validate()
    {

        try
        {
            if ((txt_qty.Text != string.Empty) && (txt_qty.Text != "0"))
            {
                validateval = 1;
            }
            else
            {   lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Select Stock Name";
                validateval = 0;

            }


            int qty;
            qty = Convert.ToInt16(txt_qty.Text);
            double rate;
            double assignrate;
         //   rate = Convert.ToDouble(txt_rate.Text);
         //   txt_rate.Text = rate.ToString("f2");
          //  assignrate = Convert.ToDouble(txt_rate.Text);
            if (qty >= 1) 
            {
                validateval1 = 1;
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please Check Entry";
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

    public void insertdata()
    {
        string str;
        SqlConnection con = new SqlConnection(connStr);
        try
        {
            //   cut rate   str = "Insert into PlantStockMaster(StockGroup,StockSubgroup,AddedDate,qty,plant_code,StockRate,UserId)values(@StockGroup,@StockSubgroup,@AddedDate,@qty,@plant_code,@StockRate,@UserId)";

            str = "Insert into PlantStockMaster(StockGroup,StockSubgroup,stockcode,AddedDate,qty,plant_code,UserId)values(@StockGroup,@StockSubgroup,@stockcode,@AddedDate,@qty,@plant_code,@UserId)";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            cmd.Parameters.AddWithValue("@StockGroup", ddl_materialName.SelectedItem.Text.Trim());
            cmd.Parameters.AddWithValue("@StockSubgroup", ddl_materialName1.SelectedItem.Value.Trim());
         //   cmd.Parameters.AddWithValue("@StockSubgroup", ddl_materialName1.Text);
            cmd.Parameters.AddWithValue("@stockcode", ddl_materialName.SelectedItem.Value.Trim());
            cmd.Parameters.AddWithValue("@AddedDate", System.DateTime.Now);
            cmd.Parameters.AddWithValue("@Qty", txt_qty.Text);
            cmd.Parameters.AddWithValue("@plant_code", ddl_PlantName.SelectedItem.Value);
        //    cmd.Parameters.AddWithValue("@StockRate", txt_rate.Text);
            cmd.Parameters.AddWithValue("@UserId", loginuser);
          //  cmd.Parameters.AddWithValue("@stockcode", ddl_materialName.SelectedIndex);
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

    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
        //pcode = ddl_Plantcode.SelectedItem.Text;
        getgriddata();
        txt_qty.Text = "";
    }
    //public void getMaterialqty()
    //{

    //    try
    //    {
    //       // ddl_materialName1.Items.Clear();
           
    //        availtxt_qty.Text = "0";
    //        availtxt_rate.Text = "0";
    //        SqlConnection con = new SqlConnection(connStr);
    //        con.Open();
    //        //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
    //        string str = "select StockSubgroup,sum(qty) as qty,avg(stockrate) as stockrate  from AdminStockMaster where StockGroup='" + ddl_materialName.Text + "' and StockSubgroup='" + ddl_materialName1.Text + "' group by  StockSubgroup  ";
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {

    //                availtxt_qty.Text = dr["qty"].ToString();

    //                availtxt_rate.Text = dr["stockrate"].ToString();

    //            }
    //        }
    //        else
    //        {

    //            lblmsg.Visible = true;
    //            lblmsg.ForeColor = System.Drawing.Color.Red;
    //            lblmsg.Text = "No Agents";
    //          //  ddl_materialName1.Items.Clear();
    //            availtxt_qty.Text = "0";
    //            availtxt_rate.Text = "0";
    //        }



    //    }

    //    catch
    //    {

    //        lblmsg.Visible = true;
    //        lblmsg.ForeColor = System.Drawing.Color.Red;
    //        lblmsg.Text = "Please Check";

    //    }



    //}
    public void getplantstoxkMaterialqty()
    {

        try
        {
            // ddl_materialName1.Items.Clear();

            availtxt_qty.Text = "0";
            availtxt_rate.Text = "0";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select StockSubgroup,sum(qty) as qty  from PlantStockMaster where StockGroup='" + ddl_materialName.Text + "' and StockSubgroup='" + ddl_materialName1.Text + "' group by  StockSubgroup  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    gteplantstockqty =Convert.ToInt16(dr["qty"].ToString());
                  //  availtxt_rate.Text = dr["stockrate"].ToString();
                }
            }
            else
            {

                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "No Agents";
                //  ddl_materialName1.Items.Clear();
                availtxt_qty.Text = "0";
                availtxt_rate.Text = "0";
            }



        }

        catch
        {

            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Please Check";

        }



    }

    public void getMaterialrate()
    {

        try
        {
            // ddl_materialName1.Items.Clear();

         //   availtxt_qty.Text = "0";
            availtxt_rate.Text = "0";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select itemrate  from StockRateSetting where StockGroup='" + ddl_materialName.Text + "' and StockSubgroup='" + ddl_materialName1.Text + "' where fixstatus='1' and plant_code='" + pcode + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                 //   availtxt_qty.Text = dr["qty"].ToString();
                    availtxt_rate.Text = dr["stockrate"].ToString();

                }
            }
            else
            {

                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "No Agents";
                //  ddl_materialName1.Items.Clear();
                availtxt_qty.Text = "0";
                availtxt_rate.Text = "0";
            }



        }

        catch
        {

            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Stock Rate Not Fixing";

        }



    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        getgriddata();
    }

    protected void ddl_materialName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txt_qty.Text = "";
            getstockname();
            checkadminqty();
            checkplantqty();
            if (adminqty > plantqty)
            {
                getqty = adminqty - plantqty;
                availtxt_qty.Text = getqty.ToString();
            }
            getgriddata();
        }
        catch (Exception ex)
        {

        }

    }
    protected void ddl_materialName1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txt_qty.Text = "";
            checkadminqty();
            checkplantqty();
            if (adminqty > plantqty)
            {
                getqty = adminqty - plantqty;
                availtxt_qty.Text = getqty.ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }

   

   

    public void clear()
    {      
        txt_qty.Text = "0";   
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        validate();
        if ((validateval == 1))
        {
            checkadminqty();
            checkplantqty();
            if (adminqty > plantqty)
            {
                getqty = adminqty - plantqty;
                availtxt_qty.Text = getqty.ToString();

                if (Convert.ToInt32(txt_qty.Text) <= Convert.ToInt32(availtxt_qty.Text))
                {

                    insertdata();
                    getgriddata();
                   
                    clear();
                }
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please, Check the Availability...";
            }
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Please, Check Your Data...";
        }

    }


    public void getgriddata()
    {
        try
        {
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str;

            str = "SELECT CONVERT(NVARCHAR(15),StockGroupId)+'_'+StockGroup AS GHeader,CONVERT(NVARCHAR(15),StockSubGroupID)+'_'+StockSubGroup1 AS GSHeader,qty,AddedDate FROM  " +
" (Select  Tid,stockcode ,StockSubgroup,qty,convert(varchar,AddedDate,103) as AddedDate   from PlantStockMaster  where plant_code='" + ddl_PlantName.SelectedItem.Value.ToString().Trim() + "' AND StockGroup='" + ddl_materialName.SelectedItem.Text.Trim() + "' )  AS t1 " +
" LEFT JOIN " +
" (SELECT StockGroup,StockSubGroup AS StockSubGroup1,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2  ON t1.stockcode=t2.StockGroupID AND t1.StockSubgroup=t2.StockSubGroupID ORDER BY Tid desc";
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
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        getgriddata();
    }


    //-----code start

    public void getstockcategory()
    {
        try
        {           
            ddl_materialName.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select   DISTINCT(StockGroup) AS StockGroup,StockGroupID  from Stock_Master ORDER BY StockGroupID ";
         
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(ds);
            if (ds != null)
            {                
                ddl_materialName.DataSource = ds;
                ddl_materialName.DataTextField = "StockGroup";
                ddl_materialName.DataValueField = "StockGroupID";
                ddl_materialName.DataBind();  
            

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
            ddl_materialName1.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "SELECT   DISTINCT(StockSubGroup) AS StockSubGroup,StockSubGroupID  from Stock_Master  where  StockGroupID='" + ddl_materialName.SelectedItem.Value.ToString().Trim() + "' ";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(ds);
            if (ds != null)
            {
                ddl_materialName1.DataSource = ds;
                ddl_materialName1.DataTextField = "StockSubGroup";
                ddl_materialName1.DataValueField = "StockSubGroupID";
                ddl_materialName1.DataBind();

            }
            
                    //ddl_materialName1.Items.Add(dr["StockSubGroup"].ToString());
                    //getstocksubgroupid = Convert.ToInt16(dr["StockSubGroupID"].ToString());               

                
                     
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

    public void checkadminqty()
    {
        try
        {          
            availtxt_qty.Text = "0";
            availtxt_rate.Text = "0";
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select sum(qty) as qty from AdminStockMaster where stockcode='" + ddl_materialName.SelectedItem.Value.Trim() + "' and StockSubgroup='" + ddl_materialName1.SelectedItem.Value.Trim() + "' group by  StockSubgroup  ";
            adminqty = dbacess.ExecuteScalarint(str);
        }
        catch
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Please Check";
        }
    }
    public void checkplantqty()
    {
        try
        {            
            availtxt_qty.Text = "0";
            availtxt_rate.Text = "0";
            string str = "select sum(qty) as qty from PlantStockMaster where stockcode='" + ddl_materialName.SelectedItem.Value.Trim() + "' and StockSubgroup='" + ddl_materialName1.SelectedItem.Value.Trim() + "'    group by  StockSubgroup  ";
            plantqty = dbacess.ExecuteScalarint(str);
        }
        catch
        {
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Please Check";
        }
    }


    //-------code End

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
            string str = "select StockGroupID,StockSubGroupID from Stock_Master where StockGroup='" + ddl_materialName.Text + "' and StockSubgroup='" + ddl_materialName1.Text + "'  ";
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


    public void getpreqty()
    {
        try
        {
            //   ddl_agentcode.Items.Clear();
          //  ddl_materialType.Items.Clear();
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

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            getpreqty();
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            TextBox qty = (TextBox)row.Cells[4].Controls[0];
            //  vehicleBO.Phoneno = ((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim();
            //  int i = Convert.ToInt16(paymode);

            conn.Close();
            GridView1.EditIndex = -1;
            conn.Open();
            SqlCommand cmd = new SqlCommand("update AdminStockMaster  set qty='" + qty.Text + "',preqty='" + preqty + "',updateUserId='" + loginuser + "',updateDate='" + System.DateTime.Now + "'  where Tid='" + userid + "'", conn);
            cmd.ExecuteNonQuery();
            //  WebMsgBox.Show("Updated Successfully");
            lblmsg.Text = "Updated Successfully";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Visible = true;
            getgriddata();


            // loadgrid();

            //   e.Cancel = true;
            //  gvProducts.EditIndex = -1;

        }
        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            con.Close();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        getgriddata();
    }
}