using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;

public partial class LoanMaster : System.Web.UI.Page
{

    BOLloan BOloan = new BOLloan();
    BLLloan BLloan = new BLLloan();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    DateTime dt = new DateTime();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DataSet ds = new DataSet();
    DbHelper DBaccess = new DbHelper();
   
    public static SqlDataReader dr = null;
    public static string companycode;
    public static string plantcode;
    public static int rid = 0;
    public static int savebtn = 0;
      int Data;
    int status;
    //Admin Check Flag
    public int Falg = 0;
    public static int roleid;
    public static int buttonviewstatus;
    string message;
    string frmdate;
    string todate;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            uscMsgBox1.MsgBoxAnswered += MessageAnswered;
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                dt = System.DateTime.Now;
                btn_lock.Visible = false;
                //txt_LoanDate.Text = dt.ToString("dd/MM/yyyy");
                //txt_LoanExpireDate.Text = dt.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                plantcode = ddl_Plantcode.SelectedItem.Value;
                Session["Plant_Code1"] = plantcode;
                Bdate();

                txt_AgentId.Attributes.Add("onblur", "javascript:CallMe('" + txt_AgentId.ClientID + "','" + txt_AgentName.ClientID + "')");
                loadrouteid();
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);

                loadgrid();
                txt_loanId.Text = "";
                txt_loanId.Text = GetLoanID();
                LoadAgentid();
                //txt_AgentId.Text = ddl_AgentId.SelectedItem.Value;
                Lbl_Errormsg.Visible = false;

                getadminapprovalstatus();
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
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);

                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                plantcode = ddl_Plantcode.SelectedItem.Value;
                Session["Plant_Code1"] = plantcode;
                if (savebtn == 0)
                {
                    txt_AgentId.Attributes.Add("onblur", "javascript:CallMe('" + txt_AgentId.ClientID + "','" + txt_AgentName.ClientID + "')");
                }
                txt_loanId.Text = "";
                txt_loanId.Text = GetLoanID();
                //txt_AgentId.Text = ddl_AgentId.SelectedItem.Value;
                Lbl_Errormsg.Visible = false;
                getadminapprovalstatus();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }            

    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(companycode);
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
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(companycode, plantcode);
            if (dr.HasRows)
            {
                while (dr.Read())                {
                    
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


   


    private void Bdate()
    {
        try
        {
            dr = null;
            dr = BLLBill.Billdate(companycode, plantcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_LoanDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_LoanExpireDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");


                    frmdate = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("MM/dd/yyyy");
                    todate = Convert.ToDateTime(dr["Bill_todate"]).ToString("MM/dd/yyyy");
                   
                    Falg = Convert.ToInt32(dr["ViewStatus"].ToString());
                  
                          
                            btn_Save.Visible = true;
                        
                 
                }
            }
            else
            {
              
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please Select the Bill_Date";
            }
        }
        catch (Exception ex)
        {
        }

    }

    private string GetLoanID()
    {
        string retstr;
        int sno1 = 0;
        string sno = string.Empty;
        sno = BLloan.GetLoanid(companycode, plantcode);
        sno1 = Convert.ToInt32(sno);
        if (sno1 == 1)
        {
            retstr = "10" + sno;
        }
        else
        {
            retstr = sno;
        }
        return retstr;
    }


    [System.Web.Services.WebMethod]
    public static string GetAgentName(string Aid)
    {
        try
        {

            BLLloan Blloan = new BLLloan();
            string AgentName = string.Empty;

            dr = Blloan.Getloanagentid(companycode, plantcode, rid.ToString(), Aid);
            if (dr.HasRows)
            {
                string resmes = "Already having Loan for thish Id-" + Aid;
                // string resmes = string.Empty;              
                return resmes;
            }
            else
            {

                if (Aid == null || Aid.Length == 0)
                    return String.Empty;

                try
                {
                    AgentName = Blloan.GetLoanAgentname(companycode, plantcode, rid.ToString(), Aid);

                    if (!string.IsNullOrEmpty(AgentName))
                    {
                        savebtn = 1;
                        return AgentName;

                    }
                    else
                    {
                        savebtn = 2;
                        AgentName = "";
                        return AgentName;
                    }

                }


                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

    }

    [WebMethod]
    public static string loanmasterClre(string name, string add)
    {
        LoanMaster lonmas = new LoanMaster();
        lonmas.clr();
        name = string.Empty;
        return name;
    }


    //[WebMethod]
    //public static string InsertData(string LoanDate,string LoanExpireDate,string RouteId,string AgentId,string loanId,string LoanAmount,string InstalAmount,string NoofInstalment,string loanDescription,string companycode,string plantcode)
    //{

    //    BOLloan BOloan = new BOLloan();
    //    BLLloan BLloan = new BLLloan();

    //    //SETBO 
    //    BOloan.Companycode = int.Parse(companycode);
    //    BOloan.Plantcode = int.Parse(plantcode);
    //    BOloan.Loandate = DateTime.Parse(LoanDate);
    //    BOloan.Expiredate = DateTime.Parse(LoanExpireDate);
    //    BOloan.Route_id = int.Parse(RouteId);
    //    BOloan.agent_id = int.Parse(AgentId);
    //    BOloan.Loan_id = int.Parse(loanId);
    //    BOloan.Loanamount = double.Parse(LoanAmount);
    //    BOloan.Balance = double.Parse(LoanAmount);        

    //    double LoanAmount1 = Convert.ToDouble(LoanAmount);
    //    int NoofInstalment1 = Convert.ToInt32(NoofInstalment);
    //    double Instamnt1 = LoanAmount1 / NoofInstalment1;

    //    BOloan.Instamnt = Instamnt1;
    //    BOloan.Desc = loanDescription;
    //    //
    //    //BLL
    //    BLloan.insertloandetails(BOloan);
    //    //

    //    string result = "Insert Data" + AgentId + "address" + loanId;


    //    return result;

    //}



    public void InsertData()
    {
        if (validates())
        {
            SETBO();
            BLloan.insertloandetails(BOloan);


        }


    }
    private bool validates()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_LoanDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_LoanExpireDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            DateTime loandate = DateTime.Parse(d1);
            DateTime loanexpiredate = DateTime.Parse(d2);
            if (loanexpiredate.Subtract(loandate).Days < 0)
            {
                WebMsgBox.Show("please,Check the Todate!Its not less Then From Date");
                txt_LoanExpireDate.Focus();
                return false;
            }

           
            //if (string.IsNullOrEmpty(txt_AgentName.Text))
            //{
            //    WebMsgBox.Show("Enter the AgentName");
            //    txt_AgentName.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txt_loanId.Text))
            {
                WebMsgBox.Show("Enter the loanId");
                txt_loanId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_LoanAmount.Text))
            {
                WebMsgBox.Show("Enter the LoanAmount");
                txt_LoanAmount.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_NoofInstalment.Text))
            {
                WebMsgBox.Show("Enter the NoofInstalment");
                txt_NoofInstalment.Focus();
                return false;
            }
            //if (string.IsNullOrEmpty(txt_InstalAmount.Text))
            //{
            //    WebMsgBox.Show("Enter the InstalAmount");
            //    txt_InstalAmount.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txt_loanDescription.Text))
            {
                WebMsgBox.Show("Enter the loanDescription");
                txt_loanDescription.Focus();
                return false;
            }


            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    //private void SETBO()
    //{
    //    BOloan.Companycode = int.Parse(companycode);
    //    BOloan.Plantcode = int.Parse(plantcode);
    //    BOloan.Loandate = DateTime.Parse(LoanDate);
    //    BOloan.Expiredate = DateTime.Parse(LoanExpireDate);
    //    BOloan.Route_id = int.Parse(RouteId);
    //    BOloan.agent_id = int.Parse(AgentId);
    //    BOloan.Loan_id = int.Parse(loanId);
    //    BOloan.Loanamount = double.Parse(LoanAmount);
    //    BOloan.Balance = double.Parse(LoanAmount);

    //    double LoanAmount1 = Convert.ToDouble(LoanAmount);
    //    int NoofInstalment1 = Convert.ToInt32(NoofInstalment);
    //    double Instamnt1 = LoanAmount1 / NoofInstalment1;

    //    BOloan.Instamnt = Instamnt1;
    //    BOloan.Desc = loanDescription;
    //}

    private void SETBO()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_LoanDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_LoanExpireDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            BOloan.Companycode = int.Parse(companycode);
            BOloan.Plantcode = int.Parse(plantcode);
            BOloan.Loandate = DateTime.Parse(d1);
            BOloan.Expiredate = DateTime.Parse(d2);
            BOloan.Route_id = int.Parse(ddl_RouteID.SelectedItem.Value);
            BOloan.agent_id = int.Parse(ddl_AgentName.SelectedItem.Value);
            BOloan.Loan_id = int.Parse(txt_loanId.Text);
            BOloan.Loanamount = double.Parse(txt_LoanAmount.Text);
            BOloan.Balance = double.Parse(txt_LoanAmount.Text);

            //double LoanAmount1 = Convert.ToDouble(txt_LoanAmount.Text);
            //int NoofInstalment1 = Convert.ToInt32(txt_NoofInstalment.Text);
            //double Instamnt1 = LoanAmount1 / NoofInstalment1;
            //txt_InstalAmount.Text = Instamnt1.ToString();

            //after
            int NoofInstalment1 = Convert.ToInt32(txt_NoofInstalment.Text);
           // double NoofDays = (NoofInstalment1 * 7);
           // double instalmonth = NoofDays / 30;
            double LoanAmount1 = Convert.ToDouble(txt_LoanAmount.Text);
            double Interest = Convert.ToDouble(txt_InterestRate.Text);
            double Interestrate = (Interest );
            double InterestAmount = LoanAmount1 + Interestrate;
            double Instamnt1 = InterestAmount / NoofInstalment1;
            txt_InstalAmount.Text = Instamnt1.ToString("f2");
            BOloan.Desc = txt_loanDescription.Text;
            BOloan.Instamnt = Instamnt1;
            BOloan.Desc = txt_loanDescription.Text;
            BOloan.InterestAmount = Convert.ToDouble(txt_InterestRate.Text);
            BOloan.NoofInstallment = Convert.ToInt32(txt_NoofInstalment.Text);
            BOloan.UserId = roleid;
            txt_loanDescription.Focus();
            //

           
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
           
        }
    }

    //protected void btn_Reset_Click(object sender, EventArgs e)
    //{
    //    clr();

    //}
    private void clr()
    {
        txt_AgentId.Text = "";
        txt_AgentName.Text = "";
        txt_loanId.Text = "";
        txt_LoanAmount.Text = "";
        txt_InterestRate.Text = "";
        txt_NoofInstalment.Text = "";
        txt_InstalAmount.Text = "";
        txt_loanDescription.Text = "";        
        txt_loanId.Text = GetLoanID();

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (savebtn == 0)
            {
                if (validates())
                {
                    //dr = BLloan.Getloanagentid(companycode, plantcode, ddl_RouteName.Text, txt_AgentId.Text);
                    //if (dr.HasRows)
                    //{
                    //    WebMsgBox.Show("Already Having Loan for This ID-" + txt_AgentId.Text.Trim());
                    //    clr();

                    //}
                    //else
                    //{

                    SETBO();                    
                    BLloan.insertloandetails(BOloan);
                    clr();
                    loadgrid();                  
                    txt_LoanAmount.Focus();


                    message = "Record Save Successfully";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


                    //}
                }
            }
            else
            {               
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Check the AgentId";

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();            
        }
    }
    protected void txt_NoofInstalment_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    double LoanAmount1 = Convert.ToDouble(txt_LoanAmount.Text);
        //    int NoofInstalment1 = Convert.ToInt32(txt_NoofInstalment.Text);
        //    double Instamnt1 = LoanAmount1 / NoofInstalment1;
        //    txt_InstalAmount.Text = Instamnt1.ToString();
        //    txt_loanDescription.Focus();
        //}
        //catch (Exception ex)
        //{
        //    WebMsgBox.Show("" + ex.ToString());
        //}
        ///////

        try
        {
            int NoofInstalment1 = Convert.ToInt32(txt_NoofInstalment.Text);
            //double NoofDays = (NoofInstalment1 * 7);
            //double instalmonth = NoofDays / 30;
            double LoanAmount1 = Convert.ToDouble(txt_LoanAmount.Text);
            double Interest = Convert.ToDouble(txt_InterestRate.Text);
            double Interestrate = (Interest);
            double InterestAmount = LoanAmount1 + Interestrate;
            double Instamnt1 = InterestAmount / NoofInstalment1;
            txt_InstalAmount.Text = Instamnt1.ToString("f2");
            txt_loanDescription.Focus();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void loadrouteid()
    {
        try
        {
            SqlDataReader dr;
            dr = routmasterBL.getroutmasterdatareader(companycode, plantcode);

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

    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteName.SelectedIndex = ddl_RouteID.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        
    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        LoadAgentid();
        loadgrid();   
            
    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_AgentName.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);

        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);

        }
    }
    public void loadgrid()
    {
        gvProducts.DataBind();

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    private void loadloangrid()
    {
        gvProducts.DataBind();
    }
   
  
    //private void LoadAgentid() OLD W
    //{
    //    try
    //    {
    //        dr = null;
    //        dr = agentBL.GetAgentId(companycode, plantcode, rid);
    //        ddl_AgentId.Items.Clear();
    //        ddl_AgentName.Items.Clear();
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                ddl_AgentId.Items.Add(dr["Agent_id"].ToString());
    //                ddl_AgentName.Items.Add(dr["Agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
    //            }
    //        }
    //        else
    //        {
    //            txt_AgentId.Text = "";
    //            WebMsgBox.Show("Agent is Not Added");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}
    private void LoadAgentid()
    {
        try
        {
            ds = null;
            ds = LoadAgents(companycode, plantcode, rid);
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

            sqlstr1 = "SELECT Agent_Id,CONVERT(NVARCHAR(18),(CONVERT(NVARCHAR(15),Agent_Id)+'_'+Agent_Name)) AS Agent_Name FROM Agent_Master WHERE company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + Rid + "' order by rand(Agent_Id)  ";
            ds = DBaccess.GetDataset(sqlstr1);
            return ds;
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
            return ds;
        }
    }

   
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            plantcode = ddl_Plantcode.SelectedItem.Value;
            getadminapprovalstatus();
            Bdate();
            txt_loanId.Text = GetLoanID();
            loadrouteid();
            rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
            LoadAgentid();
            Session["Plant_Code1"] = plantcode;
         //   loadgrid();
            getgridview();

            //loadloangrid();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
            
        }

    }
    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_AgentName.SelectedIndex;

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {

        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=gvtoexcel.xls");
        Response.ContentType = "application/excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvProducts.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btn_lock_Click(object sender, EventArgs e)
    {

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update AdminApproval set Loanstatus='1' where Plant_code='" + plantcode + "'", conn);
            cmd.ExecuteNonQuery();
            getadminapprovalstatus();
            //string message;
            //message = "Loan  Approved SuccessFully";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "')};";
            //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();

        }
        //  WebMsgBox.Show("inserted Successfully");
        //lbl_msg.Text = "Updated Successfully";
        //lbl_msg.ForeColor = System.Drawing.Color.Green;
        //lbl_msg.Visible = true;
    }


         public void getadminapprovalstatus()
    {

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string stt = "Select LoanStatus,status    from  AdminApproval   where Plant_code='" + plantcode + "'  ";
            SqlCommand cmd = new SqlCommand(stt, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                // btn_lock.Visible = false;

                while (dr.Read())
                {

                    Data = Convert.ToInt32(dr["LoanStatus"]);
                    status = Convert.ToInt32(dr["status"]);
                }
                //if (Data == 1 && status == 1)
                //{
                //    btn_lock.Visible = true;
                //}

                //if (Data == 0 && status == 1)
                //{
                //    btn_lock.Visible = true;
                //}
                //if (Data == 1 && status == 2)
                //{
                //    btn_lock.Visible = false;
                //}

                //if (Data == 0 && status == 2)
                //{
                //    btn_lock.Visible = false;
                //}

                if ((status == 1) && (roleid > 1))
                {
                    btn_lock.Visible = true;

                    if ((Data == 1) && (status == 1))
                    {

                        btn_lock.Visible = true;
                        btn_lock.Enabled = false;
                        //btn_lock.Visible = false;

                    }
                }
                else
                {
                    btn_lock.Visible = false;

                }
          }



        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();

        }

    }



         public void getgridview()
         {
             string getval = "";
             SqlConnection con = DBaccess.GetConnection();
             getval = "SELECT agent_Id,loan_Id,route_id ,CONVERT(VARCHAR(10),loandate,103) AS loandate,CAST(loanamount AS DECIMAL(18,2)) AS loanamount,CAST(balance AS DECIMAL(18,2)) AS balance,CAST(inst_amount AS DECIMAL(18,2)) AS inst_amount FROM LoanDetails WHERE [Plant_code]='" + plantcode + "' AND ([Company_code]='" + companycode + "') and  loandate between '"+frmdate+"' AND '"+todate+"' ORDER BY loan_Id DESC";
             SqlCommand cmd = new SqlCommand(getval, con);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             da.Fill(dt);
             if (dt.Rows.Count > 0)
             {

                 gvProducts.DataSource = dt;
                 gvProducts.DataBind();

             }
             else
             {
                 gvProducts.DataSource = "No Records";
                 gvProducts.DataBind();
             }



         }
         protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {
             gvProducts.PageIndex = e.NewPageIndex;
             getgridview();
         }
         protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
         {
             gvProducts.EditIndex = -1;
             getgridview();
         }



}
