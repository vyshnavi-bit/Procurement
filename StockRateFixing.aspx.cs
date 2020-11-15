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
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.util.collections;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using System.IO;

public partial class StockRateFixing : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    public string uid;
    double getavil;
    int godownval;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    DbHelper DB = new DbHelper();
    DateTime dtm = new DateTime();
    DateTime dtm2 = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DataSet ds = new DataSet();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection();
    BLLPlantName BllPlant = new BLLPlantName();
    int getgroupid;
    int getsubgroupid;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                //    pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  uid = Session["User_ID"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                lblmsg.Visible = false;
                if((roleid>=3 ) && (roleid!=9))
                {
                LoadPlantName();
                ddl_stockto.SelectedIndex = 2;
                //  LoadPlantcode();
                getstockcategory();
                getstockname();
                gridview();
                }
                else
                {
                loadspecialsingleplant();
                ddl_stockto.SelectedIndex = 2;
                //  LoadPlantcode();
                getstockcategory();
                getstockname();
                gridview1();
                }

            }
            //else
            //{              
            //    lblmsg.Visible = false;
            //    pcode = ddl_Plantcode.SelectedItem.Value;
            //    gridview();

            //}
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
             //   uid = Session["User_ID"].ToString();
                pcode = ddl_Plantcode.SelectedItem.Value;
                lblmsg.Visible = false;
                gridview();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
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
            string str = "select   DISTINCT(StockGroup) AS StockGroup,stockgroupid  from Stock_Master Order By StockGroupID ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_materialName.Items.Add(dr["StockGroup"].ToString());
                    getgroupid = Convert.ToInt16(dr["stockgroupid"].ToString());
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

    public void getstockid()
    {
        try
        {
            //   ddl_agentcode.Items.Clear();
           // ddl_materialName1.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select   DISTINCT(stockgroupid) AS stockgroupid,StockSubGroupID  from Stock_Master  where  StockGroup='" + ddl_materialName.Text + "'  and StockSubGroup='" + ddl_materialName1.Text + "'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    getgroupid = Convert.ToInt16(dr["stockgroupid"].ToString());
                    getsubgroupid = Convert.ToInt16(dr["StockSubGroupID"].ToString());
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
            ddl_materialName1.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select   DISTINCT(StockSubGroup) AS StockSubGroup,StockSubGroupID  from Stock_Master  where  StockGroup='" + ddl_materialName.Text + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_materialName1.Items.Add(dr["StockSubGroup"].ToString());
                    getsubgroupid = Convert.ToInt32(dr["StockSubGroupID"].ToString());
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
    public void gridview()
    {

        try
        {
            string sqlstr = string.Empty;
            DateTime dt1 = new DateTime();
            //  DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //   dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            //    string d2 = dt2.ToString("MM/dd/yyyy");

            //dt2 = txt_time.Text;

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

           //     string sqlstr = "SELECT Tid,Stock_Type,Stock_Category,convert(varchar,AddedDate,103) as AddedDate,itemRate from StockRateSetting   order by tid desc ";
                if (ddl_stockto.SelectedItem.Value.ToString().Trim() == "1")
                {
                     sqlstr = "select  stockrate.Tid,StockGroup,StockSubGroup,stockrate.ItemRate,stockrate.AddedDate,stockrate.RateTypePlantorgodown,stockrate.Fixstatus,stockrate.Plant_Code from (SELECT Tid,Stock_Type,Stock_Category,convert(varchar,AddedDate,103) as AddedDate,itemRate,RateTypePlantorgodown,Fixstatus,Plant_Code from StockRateSetting WHERE  Fixstatus=1 AND AND RateTypePlantorgodown='" + ddl_stockto.SelectedItem.Value.ToString().Trim() + "'  )  as stockrate left join (select *   from Stock_Master) as smaster on  stockrate.Stock_Type=smaster.StockGroupID and stockrate.Stock_category=smaster.StockSubGroupID   where   smaster.StockGroup is not null Order By Tid desc";
                }
                else
                {
                    sqlstr = "select  stockrate.Tid,StockGroup,StockSubGroup,stockrate.ItemRate,stockrate.AddedDate,stockrate.RateTypePlantorgodown,stockrate.Fixstatus,stockrate.Plant_Code from (SELECT Tid,Stock_Type,Stock_Category,convert(varchar,AddedDate,103) as AddedDate,itemRate,RateTypePlantorgodown,Fixstatus,Plant_Code from StockRateSetting WHERE  Fixstatus=1  AND RateTypePlantorgodown='" + ddl_stockto.SelectedItem.Value.ToString().Trim() + "' AND Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "'  )  as stockrate left join (select *   from Stock_Master) as smaster on  stockrate.Stock_Type=smaster.StockGroupID and stockrate.Stock_category=smaster.StockSubGroupID   where   smaster.StockGroup is not null Order By Tid desc";
                }
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
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
        catch
        {



        }
    }
    public void gridview1()
    {
        try
        {
            string sqlstr = string.Empty;
            DateTime dt1 = new DateTime();
            //  DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //   dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            //    string d2 = dt2.ToString("MM/dd/yyyy");

            //dt2 = txt_time.Text;
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                //     string sqlstr = "SELECT Tid,Stock_Type,Stock_Category,convert(varchar,AddedDate,103) as AddedDate,itemRate from StockRateSetting   order by tid desc ";
                if (ddl_stockto.SelectedItem.Value.ToString().Trim() == "1")
                {
                    sqlstr = "select  stockrate.Tid,StockGroup,StockSubGroup,stockrate.ItemRate,stockrate.AddedDate,stockrate.RateTypePlantorgodown,stockrate.Fixstatus,stockrate.Plant_Code from (SELECT Tid,Stock_Type,Stock_Category,convert(varchar,AddedDate,103) as AddedDate,itemRate,RateTypePlantorgodown,Fixstatus,Plant_Code from StockRateSetting WHERE  Fixstatus=1 AND AND RateTypePlantorgodown='" + ddl_stockto.SelectedItem.Value.ToString().Trim() + "'  and plant_code='170' )  as stockrate left join (select *   from Stock_Master) as smaster on  stockrate.Stock_Type=smaster.StockGroupID and stockrate.Stock_category=smaster.StockSubGroupID   where   smaster.StockGroup is not null Order By Tid desc";
                }
                else
                {
                    sqlstr = "select  stockrate.Tid,StockGroup,StockSubGroup,stockrate.ItemRate,stockrate.AddedDate,stockrate.RateTypePlantorgodown,stockrate.Fixstatus,stockrate.Plant_Code from (SELECT Tid,Stock_Type,Stock_Category,convert(varchar,AddedDate,103) as AddedDate,itemRate,RateTypePlantorgodown,Fixstatus,Plant_Code from StockRateSetting WHERE  Fixstatus=1  AND RateTypePlantorgodown='" + ddl_stockto.SelectedItem.Value.ToString().Trim() + "' AND Plant_Code='170'  )  as stockrate left join (select *   from Stock_Master) as smaster on  stockrate.Stock_Type=smaster.StockGroupID and stockrate.Stock_category=smaster.StockSubGroupID   where   smaster.StockGroup is not null Order By Tid desc";
                }
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
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
        catch
        {



        }
    }

    public void INSERT()
    {

        try
        {

           // getstockid();

            DateTime dt1 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //    dt2 = DateTime.ParseExact(txt_adddate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");       
           


        //    update();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();
            if (ddl_stockto.SelectedItem.Value == "1")
            {
              //  cmd.CommandText = "INSERT INTO StockRateSetting (Stock_Type,Stock_Category,ItemRate,AddedDate,Fixstatus,RateTypePlantorgodown,LoginId,Plant_Code) VALUES ('" + ddl_materialName.Text + "','" + ddl_materialName1.Text + "','" + txt_stockrate.Text + "','" + d1 + "','" + 1 + "','" + ddl_stockto.SelectedValue + "','" + uid + "','1')";
                cmd.CommandText = "INSERT INTO StockRateSetting (Stock_Type,Stock_Category,ItemRate,AddedDate,Fixstatus,RateTypePlantorgodown,LoginId,Plant_Code) VALUES ('" + getgroupid + "','" + getsubgroupid + "','" + txt_stockrate.Text + "','" + d1 + "','" + 1 + "','" + ddl_stockto.SelectedValue + "','" + uid + "','1')";
            }
            if (ddl_stockto.SelectedItem.Value == "2")
            {
              //  cmd.CommandText = "INSERT INTO StockRateSetting (Stock_Type,Stock_Category,ItemRate,AddedDate,Fixstatus,RateTypePlantorgodown,Plant_Code,LoginId) VALUES ('" + ddl_materialName.Text + "','" + ddl_materialName1.Text + "','" + txt_stockrate.Text + "','" + d1 + "','" + 1 + "','" + ddl_stockto.SelectedValue + "', '" + ddl_PlantName.SelectedItem.Value + "','" + uid + "')";
                cmd.CommandText = "INSERT INTO StockRateSetting (Stock_Type,Stock_Category,ItemRate,AddedDate,Fixstatus,RateTypePlantorgodown,Plant_Code,LoginId) VALUES ('" + getgroupid + "','" + getsubgroupid + "','" + txt_stockrate.Text + "','" + d1 + "','" + 1 + "','" + ddl_stockto.SelectedValue + "', '" + ddl_PlantName.SelectedItem.Value + "','" + uid + "')";
             }
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            lblmsg.Text = "Registered successfully......!";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Visible = true;
         //   WebMsgBox.Show("inserted Successfully");
            //   gridview();
            ////    clear();
            //    gettid();
            conn.Close();
        }
        catch
        {

            WebMsgBox.Show("Please Check Above Empty or Wrong Fields Entry");

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if ((ddl_materialName.Text != "------Select------") && (ddl_materialName1.Text != "------Select------") && (txt_stockrate.Text != string.Empty) && (ddl_stockto.Text != "-----------Select------------"))
            {
                if ((txt_stockrate.Text != "0") && (txt_stockrate.Text != string.Empty))
                {
                    //  getRate();
                    getstockid();
                    update();
                    INSERT();
                }
                //   update();
                //   gettid();
                //   getamount();
                //     LoadPlantcode();
                //   getstocktype();
                //    getstockcategory();
                gridview();
                clear();
                txt_stockrate.Focus();
            }
            else
            {

                WebMsgBox.Show("Please Check Above Empty or Wrong Fields Entry");

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
            con =  DB.GetConnection();
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





   
    public void clear()
    {
       // ddl_stocktype.Items.Clear();
      //  ddl_stockcategory.Items.Clear();
        //  ddl_Plantname.Items.Clear();
        txt_stockrate.Text = "0";
       // txt_qty.Text = "0";
    }
    protected void ddl_plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
      pcode = ddl_Plantcode.SelectedItem.Value;
      gridview();
    }
    protected void ddl_stocktype_SelectedIndexChanged(object sender, EventArgs e)
    {
       // getstockcategory();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }
  


    public void update()
    {
        int Fixstatus1 = 0;
        try
        {
            
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Close();
            conn.Open();
            if (ddl_stockto.SelectedItem.Value == "1")
            {
                SqlCommand cmd = new SqlCommand("update StockRateSetting  set Fixstatus='" + 0 + "' where  Stock_Type='" + getgroupid.ToString().Trim() + "' and Stock_Category='" + getsubgroupid.ToString().Trim() + "' AND RateTypePlantorgodown=1 ", conn);
                cmd.ExecuteNonQuery();
            }
            if (ddl_stockto.SelectedItem.Value == "2")//for plant
            {
               // SqlCommand cmd = new SqlCommand("update StockRateSetting  set Fixstatus='" + 0 + "' where  Stock_Type='" + ddl_materialName.Text + "' and Stock_Category='" + ddl_materialName1.Text + "'  and plant_code='" + ddl_PlantName.SelectedItem.Value + "'", conn);
                SqlCommand cmd = new SqlCommand("update StockRateSetting  set Fixstatus='" + 0 + "' where  Stock_Type='" + getgroupid.ToString().Trim() + "' and Stock_Category='" + getsubgroupid.ToString().Trim() + "'  and plant_code='" + ddl_PlantName.SelectedItem.Value + "'  AND RateTypePlantorgodown=2 ", conn);
                cmd.ExecuteNonQuery();


            }
           // WebMsgBox.Show("Updated Successfully");
            //   gridview();

        }
        catch
        {


        }
    }
    protected void ddl_stockcategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void ddl_materialName_SelectedIndexChanged(object sender, EventArgs e)
    {
        getstockname();
    }
    protected void ddl_materialName1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void ddl_stockto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_stockto.SelectedItem.Value == "2")
            {
                lbl_TruckId12.Visible = true;
                ddl_PlantName.Visible = true;

            }
            if (ddl_stockto.SelectedItem.Value == "1")
            {
                lbl_TruckId12.Visible = false;
                ddl_PlantName.Visible = false;
            }
            gridview();

        }
        catch (Exception ex)
        {

        }
    }
    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        getstockcategory();
        getstockname();
        gridview();
    }
}
    