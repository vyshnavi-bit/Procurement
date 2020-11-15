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
using System.IO;



public partial class AgentDetailsMaintains : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public int index;
    public static int rid = 0;
    public string finalClosingbalance = "0";
   
    DataTable dt1 = new DataTable();
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    public static SqlDataReader dr = null;
    DbHelper dbaccess = new DbHelper();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();


                roleid = Convert.ToInt32(Session["Role"].ToString());
                Get_GetReportTypeInfo();

                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                else
                {
                    LoadPlantName();
                }
                pcode = ddl_Plantname.SelectedItem.Value;
                loadrouteid();
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                LoadAgentid();
                Lbt_Pname.Text = ddl_Plantname.SelectedItem.Text;
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
                roleid = Convert.ToInt32(Session["Role"].ToString());
                // Get_GetReportTypeInfo();
                // Get_UsedRatechartDetailsPlantResult();
                pcode = ddl_Plantname.SelectedItem.Value;
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                Lbt_Pname.Text = ddl_Plantname.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }

    private void Get_GetReportTypeInfo()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AgentLoanDetailed]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", 1);
                sqlCmd.Parameters.AddWithValue("@sppcode", 159);                     
              
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                //
                if (dt != null)
                {
                    gvCustomers.DataSource = dt;
                    gvCustomers.DataBind(); 
                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Get_AgentAddressResult()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AgentLoanDetailedResult]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spAgentid", ddl_AgentName.SelectedItem.Value);
                sqlCmd.Parameters.AddWithValue("@spIndex", index);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                //
                if (dt != null)
                {
                    gvresult.DataSource = dt;
                    gvresult.DataBind();
                   
                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Get_AgentDetailmilkResult()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AgentDetailmilkresult]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spAgentid", ddl_AgentName.SelectedItem.Value);
                sqlCmd.Parameters.AddWithValue("@spIndex", index);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                //
                if (dt != null)
                {
                    gvmilkresult.DataSource = dt;
                    gvmilkresult.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Get_AgentDetailLoanResult()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AgentDetailLoanresult]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spAgentid", ddl_AgentName.SelectedItem.Value);
                sqlCmd.Parameters.AddWithValue("@spIndex", index);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                //
                if (dt.Rows.Count > 0)
                {
                    gvLoanresult.DataSource = dt;
                    gvLoanresult.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Get_AgentInventoryResult()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AgentDetailInventoryresult]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spAgentid", ddl_AgentName.SelectedItem.Value);               

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                //
                if (dt != null)
                {
                    gvInventory.DataSource = dt;
                    gvInventory.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    private void Get_AgentSupervisorResult()
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AgentSupervisorresult]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spAgentid", ddl_AgentName.SelectedItem.Value);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                //
                if (dt != null)
                {
                    gvSupervisor.DataSource = dt;
                    gvSupervisor.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    protected void btn_Get_Click(object sender, EventArgs e)
    {

       
    }

    //protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    gvCustomers.Columns[1].ItemStyle.Width = 50;
        //    gvCustomers.Columns[3].ItemStyle.Width = 20;
        //}
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    LinkButton lb = new LinkButton();
        //    lb.ID = "lbBooks";
        //    lb.Text = "Select";
        //    e.Row.Cells[4].Controls.Add(lb);
        //}
    //}
    
   // protected void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    gvCustomers.EditIndex = e.RowIndex;
    //    int index = gvCustomers.EditIndex;
    //    Get_UsedRatechartDetailsPlantResult();

    //}
  


    private void LoadPlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

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
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }


    private void loadrouteid()
    {
        try
        {
            SqlDataReader dr;
            dr = routmasterBL.getroutmasterdatareader(ccode, pcode);
            ddl_RouteName.Items.Clear();
            ddl_RouteID.Items.Clear();

            while (dr.Read())
            {
                ddl_RouteName.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
                ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }

    private void LoadAgentid()
    {
        try
        {
            ds = null;
            ds = LoadAgents(ccode, pcode, rid);          
            ddl_AgentName.Items.Clear();
            if (ds != null)
            {
                ddl_AgentName.DataSource = ds;
                ddl_AgentName.DataTextField = "Agent_Name";
                ddl_AgentName.DataValueField = "Agent_Id";
                ddl_AgentName.DataBind();
            }
            else
            {

            }
            
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    public DataSet LoadAgents(string ccode, string pcode, int Rid)
    {
        ds = null;
        try
        {
        
        string sqlstr1 = string.Empty;

         sqlstr1 = "SELECT Agent_Id,CONVERT(NVARCHAR(18),(CONVERT(NVARCHAR(15),Agent_Id)+'_'+Agent_Name)) AS Agent_Name FROM Agent_Master WHERE company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + Rid + "'";
        ds = dbaccess.GetDataset(sqlstr1);
        return ds;
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
            return ds;
        }
    }

    protected void gvresult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        pcode = ddl_Plantname.SelectedItem.Value;
        loadrouteid();
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        LoadAgentid();
    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        pcode = ddl_Plantname.SelectedItem.Value;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        LoadAgentid();
    }

    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
       
    }
    protected void gvLoanresult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string val = e.Row.Cells[4].Text;
        if (val == "11111.00")
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "";
            e.Row.Cells[2].Text = "";
            e.Row.Cells[3].Text = "Total";
            e.Row.Cells[4].Text = "";
        }
        else if (val == "111111.00")
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "";
            e.Row.Cells[2].Text = "";
            e.Row.Cells[3].Text = "PerLtrLoanAmount";
            e.Row.Cells[4].Text = "";

        }
    }

    protected void gvCustomers_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            index = Convert.ToInt32(gvCustomers.Rows[e.NewSelectedIndex].Cells[0].Text);

            if (index == 1)
            {
                // ALL
                index = 1;
                Get_AgentAddressResult();
                Get_AgentDetailmilkResult();
                Get_AgentDetailLoanResult();
                Get_AgentInventoryResult();
                Get_AgentSupervisorResult();
               
                gvmilkresult.Visible = true;
                gvLoanresult.Visible = true;
                gvInventory.Visible = true;
                gvSupervisor.Visible = true;
            }
            else if (index == 2)
            {
                // Inventory
                index = 2;
                Get_AgentAddressResult();
               // Get_AgentDetailmilkResult();
               // Get_AgentDetailLoanResult();
                gvInventory.Visible = true;
                gvmilkresult.Visible = false;
                gvLoanresult.Visible = false;
                gvSupervisor.Visible = false;

            }
            else if (index == 3)
            {
                // Route
                index = 3;
                Get_AgentAddressResult();
                //Get_AgentDetailmilkResult();
               // Get_AgentDetailLoanResult();
                Get_AgentSupervisorResult();
                gvSupervisor.Visible = true;          
                gvmilkresult.Visible = false;
                gvLoanresult.Visible = false;
                gvInventory.Visible = false;
               
            }
            else
            {
                // Loan
                index = 1;
                Get_AgentAddressResult();
                Get_AgentDetailmilkResult();
                Get_AgentDetailLoanResult();
                gvmilkresult.Visible = true;
                gvLoanresult.Visible = true;
                gvInventory.Visible = false;
                gvSupervisor.Visible = false;
            }
        }
        catch(Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


    protected void gvLoanresult_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
    {
        int LoanId = 0;
        try
        {
            LoanId = Convert.ToInt32(gvLoanresult.Rows[e.NewSelectedIndex].Cells[2].Text);
            Lbl_LCodeR.Text = LoanId.ToString();
            Lbl_LdateR.Text = gvLoanresult.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            Lbl_NameR.Text = ddl_AgentName.SelectedItem.Text;
            finalClosingbalance = gvLoanresult.Rows[e.NewSelectedIndex].Cells[5].Text.ToString();
            this.ModalPopupExtender1.Show();
            GetAgentSingleLoanRecoveryDetail(LoanId);
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }

    private void GetAgentSingleLoanRecoveryDetail(int Lid)
    {
        try
        {
            DataTable dt = new DataTable();           
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                string str = "SELECT ROW_NUMBER() OVER (ORDER BY Tid) AS Sno,CONVERT(Nvarchar(15),Paid_date,103) AS Paiddate,Openningbalance,Paid_Amount,Closingbalance FROM Loan_Recovery WHERE Plant_code='" + pcode + "' AND Agent_id='" + ddl_AgentName.SelectedItem.Value + "' AND Loan_id='" + Lid + "' Order by tid";
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(str, conn);                
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlCmd;
                da.Fill(dt);
                //
                if (dt.Rows.Count>0)
                {
                    gvAgentloanRecoverydetails.DataSource = dt;
                    gvAgentloanRecoverydetails.DataBind();
                    //Calculate Sum and display in Footer Row
                    decimal totalPaidamount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Paid_Amount"));                    
                    gvAgentloanRecoverydetails.FooterRow.Cells[1].Text = "Total/Avg";
                    gvAgentloanRecoverydetails.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    gvAgentloanRecoverydetails.FooterRow.Cells[3].Text = totalPaidamount.ToString("N2");

                    DataRow rows = dt.NewRow();
                    rows["Paiddate"] = "Total/Avg";
                    rows["Paid_Amount"] = totalPaidamount.ToString();
                    rows["Closingbalance"] = finalClosingbalance;
                    dt.Rows.Add(rows);

                    gvAgentloanRecoverydetails.DataSource = dt;
                    gvAgentloanRecoverydetails.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    protected void btn_Mclose_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Hide();
    }
}