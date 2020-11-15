using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class DpuDataTrasfer : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string strsql2 = string.Empty;
    string connStr2 = ConfigurationManager.ConnectionStrings["AMPSConnectionStringDpu"].ConnectionString;
    string strsql1 = string.Empty;
    string connStr1 = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DataSet ds;
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    string message;
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt11 = new DataTable();
    string d1;
    string d2;
    string d3;
    string d4;
    string d5;
    string producers;
    //string addfsecond = "12:01:00";
    //string addtsecond = "12:59:59";

    string addfsecond = "01:01:00";
    string addtsecond = "23:59:59";
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
         //   managmobNo = Session["managmobNo"].ToString();
            dtm = System.DateTime.Now;
            dtm = System.DateTime.Now;
            txt_date.Text = dtm.ToShortDateString();
             txt_date.Text = dtm.ToString("dd/MM/yyyy");

             lblmsg.Visible = false;



             if (roleid < 3)
            {
                LoadSinglePlantName();

            }
            else
            {
              
                LoadPlantName();
  
            }



        }

        else
        {
            ccode = Session["Company_code"].ToString();
            pcode = ddl_PlantName.SelectedItem.Value;
            lblmsg.Visible = false;


        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        

        DateTime dt = new DateTime();
        dt = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
        d1 = dt.ToString("MM/dd/yyyy");
        d2 = " 12:00:00";
        d3 = d1 + d2;
        d4 = d1 + " " + addfsecond;
        d5 = d1 + " " + addtsecond; 

        GetAgentdetailsAMPS();

        if (dt1.Rows.Count ==  0)
        {          
            bulkinsert();
        }
        else
        {                      
            lblmsg.Text = "Data Already  inserted";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
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
    protected void Single_CheckedChanged(object sender, EventArgs e)
    {
     getdpuagentlist();
    }
    public void getdpuagentlist()
    {
        DdlAgentlist.Items.Clear();
        try
        {
            string str = "";
            SqlConnection con = new SqlConnection(connStr2);
            con.Open();
            if (Single.Checked == true)
            {
              //  str = "select *   from agentmaster where plant_code='" + pcode + "'  and  agent_code='" + DdlAgentlist.Text + "'";\
                str = "select distinct(agent_code) as  agent_code   from THIRUMALABILLSNEW where plant_code='" + pcode + "'  order by agent_code asc ";
            }
            else
            {
                str = "select distinct(agent_code) as agent_code   from THIRUMALABILLSNEW  where plant_code='" + pcode + "' ";
            }
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    DdlAgentlist.Items.Add(dr["agent_code"].ToString());

                }

            }
            else
            {
                messagebox();

            }
        }
        catch (Exception ee)
        {

            message = ee.ToString();
            messagebox();

        }
    }
    public void getdpuaproducer()
    {
     try
        {
            string str = "";
            SqlConnection con = new SqlConnection(connStr2);
            con.Open();
            if (Single.Checked == true)
            {
                //  str = "select *   from agentmaster where plant_code='" + pcode + "'  and  agent_code='" + DdlAgentlist.Text + "'";\
                str = "select distinct(producer_code) as  producer_code   from THIRUMALABILLSNEW where plant_code='" + pcode + "' and agent_id='" + DdlAgentlist.Text + "' order by agent_code asc ";
            }
            else
            {
                str = "select distinct(producer_code) as producer_code   from THIRUMALABILLSNEW  where plant_code='" + pcode + "'  and agent_id='" + DdlAgentlist.Text + "' ";
            }
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                     producers =dr["producer_code"].ToString();

                }

            }
            else
            {
                messagebox();

            }
        }
        catch (Exception ee)
        {

            message = ee.ToString();
            messagebox();

        }
    }
    public void messagebox()
    {

      
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload=function(){");
        sb.Append("alert('");
        sb.Append(message);
        sb.Append("')};");
        sb.Append("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
    }
    public void GetAgentdetails()
    {
        string str = "";
        
        SqlConnection con = new SqlConnection(connStr2);
        con.Open();

     //   str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "' and agent_code='" + DdlAgentlist.Text + "'   and prdate='" + d3 + "'  and  shift='" + ddl_shift.Text + "'";
       // str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'    and prdate='" + d3 + "'  and  shift='" + ddl_shift.Text + "'";
     //   str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'    and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  or (prdate between '" + d4 + "' and '" + d5 + "'))  and  shift='" + ddl_shift.Text + "'";
        str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'    and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )  and  shift='" + ddl_shift.Text + "'";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da=new SqlDataAdapter(cmd);
        da.Fill(dt);
        
    }


    public void GetAgentdetailsAMPS()
    {
        string str = "";

        SqlConnection con = new SqlConnection(connStr1);
        con.Open();

    //    str = " select *   from VMCCDPU where plant_code='" + pcode + "' and agent_code='" + DdlAgentlist.Text + "'    and prdate='" + d3 + "' and   shift='" + ddl_shift.Text + "'";
        //For All agents
     //   str = " select *   from VMCCDPU where plant_code='" + pcode + "'     and prdate='" + d3 + "' and   shift='" + ddl_shift.Text + "'";

        str = " select *   from VMCCDPU where plant_code='" + pcode + "'     and ((prdate='" + d1 + "' ) or (prdate='" + d3 + "') ) and   shift='" + ddl_shift.Text + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt1);

    }

    public void DeleteAgentdetailsAMPS()
    {
        string str = "";

        SqlConnection con = new SqlConnection(connStr1);
        con.Open();

    //    str = "delete   from VMCCDPU where plant_code='" + pcode + "' and agent_code='" + DdlAgentlist.Text + "'     and prdate='" + d3 + "' and  shift='" + ddl_shift.Text + "'";
   // for All agentss
        str = "delete   from VMCCDPU where plant_code='" + pcode + "'     and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  ) and  shift='" + ddl_shift.Text + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        cmd.ExecuteNonQuery();

    }

    public void bulkinsert()
    {
        GetAgentdetails();

        SqlConnection con = new SqlConnection(connStr1);
        con.Open();
        SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con);
        sqlBulkCopy.DestinationTableName = "dbo.VMCCDPU";
        sqlBulkCopy.ColumnMappings.Add("plant_code", "plant_code");
        sqlBulkCopy.ColumnMappings.Add("prdate", "prdate");
        sqlBulkCopy.ColumnMappings.Add("shift", "shift");
        sqlBulkCopy.ColumnMappings.Add("agent_code", "agent_code");
        sqlBulkCopy.ColumnMappings.Add("producer_code", "producer_code");
        sqlBulkCopy.ColumnMappings.Add("fat", "fat");
        sqlBulkCopy.ColumnMappings.Add("snf", "snf");
        sqlBulkCopy.ColumnMappings.Add("milk_kg", "milk_kg");
        sqlBulkCopy.WriteToServer(dt);
        con.Close();
        GetAgentdetailsAMPS();
        if (dt.Rows.Count == dt1.Rows.Count && dt.Rows.Count!=0)
        {

            lblmsg.Text = "Records successfully inserted";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Visible = true;
            insertProduceragentlist();
        }
        else
        {

             DeleteAgentdetailsAMPS();
            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }


    }

    public void insertProduceragentlist()
    {

        try
        {
           getdpuaproducer();
           SqlConnection con = new SqlConnection(connStr2);
           SqlCommand cmd = new SqlCommand("insert_DpuProducerdetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@plantcode", SqlDbType.VarChar).Value = pcode;
            cmd.Parameters.Add("@agent_code", SqlDbType.VarChar).Value = DdlAgentlist.Text;
            cmd.Parameters.Add("@producer_code", SqlDbType.VarChar).Value = producers;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (SqlException ex)
        {
            Console.WriteLine("SQL Error" + ex.Message.ToString());
           
        }


    }
     

    

    public void ImportVmcc()
    {
        try
        {
            DateTime dt = new DateTime();
            dt = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
            d1 = dt.ToString("MM/dd/yyyy");
            d2 = " 12:00:00";
            d3 = d1 + d2;
         String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            SqlCommand sqlCmd;
            using (conn = new SqlConnection(dbConnStr))
            {
                //if (ddl_PlantName.SelectedItem.Value == "164")
                //{
                //      sqlCmd = new SqlCommand("[dbo].[update_producerprocurementImport_sessionsEKODPU]");
                //}
                //else
                //{
                //      sqlCmd = new SqlCommand("[dbo].[update_producerprocurementImport_sessions]");
                //}

                ////if (ddl_PlantName.SelectedItem.Value == "164")
                ////{
                //    sqlCmd = new SqlCommand("[dbo].[update_producerprocurementImport_sessionsEKODPU]");
                //}
                //else
                //{
                    sqlCmd = new SqlCommand("[dbo].[update_producerprocurementImport_sessions]");
                //}
               
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;         
                sqlCmd.Parameters.AddWithValue("@plantcode", pcode);
                 sqlCmd.Parameters.AddWithValue("@companycode", ccode);
                sqlCmd.Parameters.AddWithValue("@frdate", d1);
                sqlCmd.Parameters.AddWithValue("@sess", ddl_shift.Text);
                sqlCmd.ExecuteNonQuery();
                lblmsg.Text = "Data ImportSuccessfully...";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Visible = true;
                
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Data ImportSuccessfully...";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
        }
       
    }
    protected void btn_ImportVmcc_Click(object sender, EventArgs e)
    {
        Getproducerprocurement();
        if (dt11.Rows.Count == 0)
        {
            ImportVmcc();
        }

        else
        {

            lblmsg.Text = "Data Already ImportSuccessfully...";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }
    }



    public void Getproducerprocurement()
    {
        try
        {
            DateTime dt = new DateTime();
            dt = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
            d1 = dt.ToString("MM/dd/yyyy");
            d2 = " 12:00:00";
            d3 = d1 + d2;
            d4 = d1 + " " + addfsecond;
            d5 = d1 + " " + addtsecond;
            string str = "";
            dt11.Rows.Clear();
            SqlConnection con = new SqlConnection(connStr1);
            con.Open();

            //    str = " select *   from VMCCDPU where plant_code='" + pcode + "' and agent_code='" + DdlAgentlist.Text + "'    and prdate='" + d3 + "' and   shift='" + ddl_shift.Text + "'";
            //For All agents
            str = " select *   from ProducerProcurement where plant_code='" + pcode + "'     and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  sessions='" + ddl_shift.Text + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt11);
        }

        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
        }



    }
}