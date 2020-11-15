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
using System.Windows.Forms;
using System.Text.RegularExpressions;


public partial class Agent : System.Web.UI.Page
{
    private string SearchString = "";
    string centreid;
    SqlConnection con;
    SqlDataReader dr;
    DbHelper DBclass = new DbHelper();
    int countuser;
    string uid;
    string ccode, pcode;
    int id,Aid;
    string remove;
    int rdir = 0, bdir = 0;
    public int rid = 0;
    //int txtNoofuser;
    BOLAgentmaster AgentBO = new BOLAgentmaster();
    BLLAgentmaster AgentBL = new BLLAgentmaster();
    DALAgentmaster agentDA = new DALAgentmaster();
    DALroutmaster routeDA = new DALroutmaster();
    BLLroutmaster routeBL = new BLLroutmaster();
    BLLBankMaster bankbl = new BLLBankMaster();
    DataTable listoutagent = new DataTable();
    BLLuser Bllusers = new BLLuser();
    DbHelper db = new DbHelper();
    DataSet DTG = new DataSet();
    public static int roleid;
    string agentid, Agentname,Mobile, cartage, splbonus, BankName, ifsccode, accno, plant_code;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack != true)
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {

                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    roleid = Convert.ToInt32(Session["Role"].ToString());

                    if ((roleid >= 3) && (roleid != 9))
                    {
                        //cartage
                        txt_CarAmt.Enabled = true;
                        txt_mcartage.Enabled = true;
                        txt_msplbonus.Enabled = true;
                        LoadPlantcode();
                        MLoadAgentid();
                    }

                    if (roleid == 9)
                    {
                        //cartage
                        txt_CarAmt.Enabled = false;
                        txt_mcartage.Enabled = false;
                        txt_msplbonus.Enabled = false;
                        loadspecialsingleplant();
                    }
                    pcode = ddl_Plantcode.SelectedItem.Value;
                    loadrouteid();
                    loadagentid();
                    LoadBankId();
                    LoadIfsccode();
                    if (rdir == 1)
                    {
                        Response.Redirect("Routemaster.aspx");
                    }
                    if (bdir == 1)
                    {

                        Response.Redirect("Bankmaster.aspx");
                    }
                    Session["Route_ID"] = cmb_RouteID.Text;
                    Session["Aid"] = txtAgentID.Text;
                    Aid = Convert.ToInt32(Session["Aid"]);
                    Session["Aid"] = Aid;
                    //Session["Route_Name"] = cmb_RouteName.Text;
                    txtAgentName.Focus();
                }
                else
                {
                    Server.Transfer("LoginDefault.aspx");
                }
                DateTime today = DateTime.Today; // As DateTime
                txt_fromdate.Text = today.ToString("dd/MM/yyyy");
            }
            else
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    pcode = ddl_Plantcode.SelectedItem.Value;
                    loadagentid();
                    Session["Route_ID"] = cmb_RouteID.Text;
                    Session["Aid"] = txtAgentID.Text;
                    Aid = Convert.ToInt32(Session["Aid"]);
                    Session["Aid"] = Aid;
                    txtAgentName.Focus();
                }
                else
                {
                    Server.Transfer("LoginDefault.aspx");
                }
                DateTime today = DateTime.Today; // As DateTime
                txt_fromdate.Text = today.ToString("dd/MM/yyyy");
            }
        }
        catch (Exception em)
        {
           
        }
    }
    //public void LoadPlantcode()
    //{
    //    try
    //    {
    //        SqlConnection con = db.GetConnection();
    //        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master";
    //        SqlCommand cmd = new SqlCommand(stt, con);
    //        SqlDataAdapter DA = new SqlDataAdapter(cmd);
    //        DA.Fill(DTG);
    //        ddl_Plantname.DataSource = DTG.Tables[0];
    //        ddl_Plantname.DataTextField = "Plant_Name";
    //        ddl_Plantname.DataValueField = "Plant_Code";
    //        ddl_Plantname.DataBind();
    //    }
    //    catch
    //    {
    //    }
    //}
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
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
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
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
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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
            WebMsgBox.Show(ex.ToString());
        }
    }




    public void GetCurrentUserCount()
    {

        try
        {
            using (con = DBclass.GetConnection())
            {

                object cc;
                string sqlstr = "select Count(User_LoginId) from UserIDInfo where User_LoginId='" + Session["User_ID"].ToString() + "' and Roles='User'";
                SqlCommand cmd = new SqlCommand(sqlstr, con);
                cc = cmd.ExecuteScalar();
                countuser = (Int32)cc;
            }
        }
        catch { }

    }
    public void GetID()
    {

        try
        {
            using (con = DBclass.GetConnection())
            {


                string sqlstr = "select User_LoginId from UserIDInfo where User_LoginId='" + Session["User_ID"].ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);


                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    centreid = dt.Rows[j]["User_LoginId"].ToString().Trim();


                }
            }
        }
        catch { }

    }
    public int UserStatus()
    {
        if (txtAgentName.Text == string.Empty)
        {
            uscMsgBox1.AddMessage("Please Enter Agent Name", MessageBoxUsc_Message.enmMessageType.Attention);
            
            return 1;
        }
        else
        {
            SqlDataReader dr;
            string str = "select Agent_ID from Agent_Master where Agent_Id='" + txtAgentID.Text + "' AND Route_ID='" + cmb_RouteID.SelectedItem.Value + "' and Plant_code='" + pcode + "' AND Company_Code='" + ccode + "' ";

            dr = DBclass.GetDatareader(str);
            if (dr.Read())
            {
                uscMsgBox1.AddMessage("Agent AlReady Exists", MessageBoxUsc_Message.enmMessageType.Attention);
               
                return 1;
            }
            else
            {

                if ((txtAgentName.Text != string.Empty))
                {                  
                        setBO();
                        AgentBL.insertagent(AgentBO);
                        loadagentid();
                        gvProducts.DataBind();
                        txtAgentName.Text = "";
                        txt_CarAmt.Text = "";
                        txt_AgentAccNo.Text = "";
                        txt_agentmobile.Text = "";
                    }


               
                return 0;
            }
        }

    }
    private string getconnectionstring()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        return str;
    }
    public SqlConnection GetConnection()
    {
        con = new SqlConnection(getconnectionstring());
        if (con.State == ConnectionState.Closed)
            con.Open();
        return con;
    }
    private void loadgrid()
    {
        //SqlConnection conn = GetConnection();
        ////string sqlstr = "select * from SampleAgentDemo where Centre_ID='" + uid + "'";
        //string sqlstr = "select * from SampleAgentDemo ORDER BY [Agent_ID ]DESC";
        //DataSet dt = new DataSet();
        //SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, conn);
        //adapter.Fill(dt);
        //GridView1.DataSource = dt.Tables[0];
        //GridView1.DataBind();
        
    }
    private void setBO()
    {
        string ag = txtAgentID.Text;
        string type = string.Empty;
        if (rbcow.Checked == true)
        {
            type = "Cow";
            rbbuff.Checked = false;
        }
        else
        {
            type = "Buffalo";
            rbcow.Checked = false;
        }
        remove = ag.Remove(0, 2);
        AgentBO.Milktype = type;
        AgentBO.AgentID = int.Parse(txtAgentID.Text);
        AgentBO.AgentName = txtAgentName.Text;
        AgentBO.CentreID = pcode;
        AgentBO.Companycode = Convert.ToInt32(ccode);
        AgentBO.AgDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
        AgentBO.AGENTMOBILE = txt_agentmobile.Text;

        AgentBO.AgentType = Convert.ToInt32(Cmb_AgentType.SelectedItem.Value);
        AgentBO.RouteID = int.Parse(cmb_RouteID.Text);
        if (txt_CarAmt.Text == "")
        {
            txt_CarAmt.Text = "0.0";
            AgentBO.CartageAmount = double.Parse(txt_CarAmt.Text);
        }
        else
        {
            AgentBO.CartageAmount = double.Parse(txt_CarAmt.Text);
        }
        Session["Route_ID"] = cmb_RouteID.Text;
       
        //
        if (Chk_BankAccount.Checked == true)
        {
            AgentBO.Bankid = ddl_BankId.SelectedItem.Value;
            AgentBO.Ifsccode = ddl_Ifsc.SelectedItem.Value;
            AgentBO.Paymentmode = "BANK";
            if (!string.IsNullOrEmpty(txt_AgentAccNo.Text))
            {
                AgentBO.AgentaccountNo = txt_AgentAccNo.Text;
            }
            else
            {
                WebMsgBox.Show("Please Enter Agent AccountNo");
            }
           
        }
        else
        {
            AgentBO.Bankid = "0";
            AgentBO.Paymentmode = "CASH";
            txt_AgentAccNo.Text = "0";
            AgentBO.AgentaccountNo = txt_AgentAccNo.Text;
            AgentBO.Ifsccode = "0";

        }



    }

    private void MsetBO()
    {
        try
        {
            string ag = ddl_mAgentid.SelectedItem.Value;
            string type = string.Empty;
            if (rd_mcom.Checked == true)
            {
                type = "Cow";
                rd_mbuff.Checked = false;
            }
            else
            {
                type = "Buffalo";
                rd_mbuff.Checked = true;
                rd_mcom.Checked = false;
            }
            AgentBO.Milktype = type;
            AgentBO.AgentID = int.Parse(ddl_mAgentid.SelectedItem.Value);
            //Agent Name Trim
            string name = txt_magentname.Text.Replace('.', ' ').ToUpper();
            name = name.Replace(',', ' ').ToUpper();
            name = name.Replace('"', ' ').ToUpper();
            name = name.Replace('&', ' ').ToUpper();
            name = name.Replace(' ', ' ').ToUpper();
            AgentBO.AgentName = name.Trim();

            AgentBO.AGENTMOBILE = txt_magentmobno.Text;

            if (rd_mbulk.Checked == true)
            {
                AgentBO.AgentType = 0;
            }
            else
            {
                AgentBO.AgentType = 1;
            }

            if (txt_mcartage.Text == "")
            {
                txt_mcartage.Text = "0.0";
                AgentBO.CartageAmount = double.Parse(txt_mcartage.Text);
            }
            else
            {
                AgentBO.CartageAmount = double.Parse(txt_mcartage.Text);
            }

            if (txt_msplbonus.Text == "")
            {
                txt_msplbonus.Text = "0.0";
                AgentBO.Splbonusamount = double.Parse(txt_msplbonus.Text);
            }
            else
            {
                AgentBO.Splbonusamount = double.Parse(txt_msplbonus.Text);
            }

            if (chk_maccountstatus.Checked == true)
            {
                int mbid = Convert.ToInt32(ddl_MBankId.SelectedItem.Value);
                AgentBO.Bankid = (mbid).ToString();
                AgentBO.Ifsccode = ddl_mIfsc.SelectedItem.Value;
                AgentBO.Paymentmode = "BANK";
                if (!string.IsNullOrEmpty(txt_mAgentAccNo.Text))
                {
                    AgentBO.AgentaccountNo = txt_mAgentAccNo.Text;
                }
                else
                {
                    WebMsgBox.Show("Please Enter Agent AccountNo");
                }

            }
            else
            {
                AgentBO.Bankid = "0";
                AgentBO.Paymentmode = "CASH";
                txt_AgentAccNo.Text = "0";
                AgentBO.AgentaccountNo = txt_AgentAccNo.Text;
                AgentBO.Ifsccode = "0";

            }

            remove = ag.Remove(0, 2);
            AgentBO.Companycode = Convert.ToInt32(ccode);
            AgentBO.CentreID = pcode;
            AgentBO.AgDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
            AgentBO.RouteID = int.Parse(cmb_RouteID.Text);
            Session["Route_ID"] = cmb_RouteID.Text;
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("Please Enter the Details correctly");
        }

    }
    private void loadagentid()
    {
        try
        {
          //  uid = Session["User_ID"].ToString();
            id = AgentBL.getmaxAgentid(cmb_RouteID.Text, ccode, pcode);
            
            if (id == 0)
            {
                txtAgentID.Text = cmb_RouteID.Text.Trim() + "01";
            }
            else
            {
                txtAgentID.Text = id.ToString();
            }
            
                
        }
        catch (Exception em)
        {
            uscMsgBox1.AddMessage(""+em, MessageBoxUsc_Message.enmMessageType.Attention);
        }
       
    }
   
    private void loadrouteid()
    {
        try
        {
        SqlDataReader dr;


        dr = routeBL.getroutmasterdatareader(ccode,pcode);

        cmb_RouteID.Items.Clear();
        cmb_RouteName.Items.Clear();
        if (dr.HasRows)
        {
            while (dr.Read())
            {

                cmb_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());

                // cmb_RouteID.Items.Add(dr["Route_ID"]+"_R1".ToString().Trim());

                //cmb_RouteName.Items.Add(dr["Route_Name"].ToString().Trim());
                cmb_RouteName.Items.Add(dr["Route_ID"] + "_".ToString().Trim() + dr["Route_Name"].ToString().Trim());
            }
        }
        else
        {
            rdir = 1;
        }
        
        }
        catch (Exception em)
        {
            
            uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        }
    }
    private bool validates()
    {
        if (Chk_BankAccount.Checked == true)
        {
            if (string.IsNullOrEmpty(txt_AgentAccNo.Text))
            {
                AgentBO.AgentaccountNo = txt_AgentAccNo.Text;
                WebMsgBox.Show("Please Enter Agent AccountNo");
                txt_AgentAccNo.Focus();
                return false;
            }           
        }
        return true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
       
    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txtAgentName.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    protected void txt_fromdate_TextChanged(object sender, EventArgs e)
    {

    }
    //public string HighlightText(string InputTxt)
    //{
    // //   string Search_Str = Agtxtsearch.Text;
    //    // Setup the regular expression and add the Or operator.
    ////    Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
    //    // Highlight keywords by calling the 
    //    //delegate each time a keyword is found.
    //  //  return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
    //}
    public string ReplaceKeyWords(Match m)
    {
        return ("<span class=highlight>" + m.Value + "</span>");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
      //  SearchString = Agtxtsearch.Text;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
      //  Agtxtsearch.Text = "";
        SearchString = "";
        gvProducts.DataBind();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }

    protected void Agtxtsearch_DataBinding(object sender, EventArgs e)
    {

    }
    protected void VendorListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

    }
    protected void VendorListView_Sorting(object sender, ListViewSortEventArgs e)
    {

    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void cmb_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cmb_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        
      cmb_RouteID.SelectedIndex = cmb_RouteName.SelectedIndex;
      loadagentid();
      MLoadAgentid();
        //cmb_RouteID_SelectedIndexChanged(sender, e);
    }

    private void LoadIfsccode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = bankbl.GetBankIfscode(ccode, pcode, ddl_BankId.SelectedItem.Value);
            ddl_Ifsc.Items.Clear();
            ddl_branchname.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Ifsc.Items.Add(dr["Ifsc_code"].ToString().Trim());
                    ddl_branchname.Items.Add(dr["Branch_Name"].ToString().Trim());
                }
                if (ddl_Ifsc.Items.Count > 0)
                {
                    ddl_Ifsc.SelectedIndex = 0;
                    ddl_branchname.SelectedIndex = 0;
                    //txt_branchname.Text = ddl_Ifsc.SelectedItem.Value;
                }               
            }
            else
            {
                uscMsgBox1.AddMessage("Ifscode Not Found", MessageBoxUsc_Message.enmMessageType.Attention);
            }

        }
        catch (Exception em)
        {
            uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        }

    }
    private void MLoadIfsccode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = bankbl.GetBankIfscode(ccode, pcode, ddl_MBankId.SelectedItem.Value);
            ddl_mbranchname.Items.Clear();
            ddl_mIfsc.Items.Clear();
            
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_mIfsc.Items.Add(dr["Ifsc_code"].ToString().Trim());
                    ddl_mbranchname.Items.Add(dr["Branch_Name"].ToString().Trim());
                }
                if (ddl_Ifsc.Items.Count > 0)
                {
                    ddl_mbranchname.SelectedIndex = 0; 
                    ddl_mIfsc.SelectedIndex = 0;                   
                }

            }
            else
            {
                uscMsgBox1.AddMessage("Ifscode Not Found", MessageBoxUsc_Message.enmMessageType.Attention);
            }
            txt_mAgentAccNo.Text = "";
        }
        catch (Exception em)
        {
            uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        }

    }
    private void LoadBankId()
    {
        try
        {
            SqlDataReader dr=null;
            dr = AgentBL.GetBankIDComm(ccode);
            ddl_BankId.Items.Clear();
            if(dr.HasRows)
            
            {
                while (dr.Read())
                {
                    ddl_BankId.Items.Add(dr["Bank_ID"].ToString().Trim());
                    ddl_BankName.Items.Add(dr["Bank_ID"].ToString().Trim() + "_" + dr["Bank_Name"].ToString().Trim());

                    // ddl_BankId.Items.Insert(0, new ListItem("--Select--", "0"));

                }
                if (ddl_BankId.Items.Count > 0)
                {
                    ddl_BankId.SelectedIndex = 0;
                    ddl_BankName.SelectedIndex = 0;
                }
            
            }
            else
            {
                bdir = 1;
            }
            
        }
        catch(Exception em)
        {
            uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        }

    }
    private void MLoadBankId()
    {
        try
        {
            SqlDataReader dr = null;
            dr = AgentBL.GetBankIDComm(ccode);
            ddl_MBankId.Items.Clear();
            ddl_mBankName.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_MBankId.Items.Add(dr["Bank_ID"].ToString().Trim());
                    ddl_mBankName.Items.Add(dr["Bank_ID"].ToString().Trim() + "_" + dr["Bank_Name"].ToString().Trim());

                    // ddl_BankId.Items.Insert(0, new ListItem("--Select--", "0"));

                }
                if (ddl_MBankId.Items.Count > 0)
                {
                    ddl_MBankId.SelectedIndex = 0;
                    ddl_mBankName.SelectedIndex = 0;
                }

            }
            else
            {
                bdir = 1;
            }

        }
        catch (Exception em)
        {
            uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        }

    }
    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BankId.SelectedIndex = ddl_BankName.SelectedIndex;
        LoadIfsccode();
    }
    protected void ddl_BankId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BankName.SelectedIndex = ddl_BankId.SelectedIndex;
    }
    protected void Chk_BankAccount_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_BankAccount.Checked == true)
        {
            ddl_BankName.Enabled = true;
            ddl_branchname.Enabled = true;
            ddl_Ifsc.Enabled = true;
            txt_AgentAccNo.Enabled = true;
        }
        else
        {
            ddl_BankName.Enabled = false;
            ddl_branchname.Enabled = false;
            ddl_Ifsc.Enabled = false;
            txt_AgentAccNo.Enabled = false;
        }
    }
    protected void rbcow_CheckedChanged(object sender, EventArgs e)
    {
        if (rbcow.Checked == true)
        {
            rbbuff.Checked = false;
            loadrouteid();
            loadagentid();
        }
        
    }
    protected void rbbuff_CheckedChanged(object sender, EventArgs e)
    {
        if (rbbuff.Checked == true)
        {
            rbcow.Checked = false;
            loadrouteid();
            loadagentid();
        }
    }
    protected void ddl_Ifsc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_branchname.SelectedIndex = ddl_Ifsc.SelectedIndex;
        //txt_branchname.Text = ddl_branchname.SelectedItem.Value;
    }
    protected void ddl_branchname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Ifsc.SelectedIndex = ddl_branchname.SelectedIndex;
        //txt_branchname.Text = ddl_Ifsc.SelectedItem.Value;
    }

    private void MLoadAgentid()
    {
        try
        {
            dr = null;
            rid = Convert.ToInt32(cmb_RouteID.SelectedItem.Value);
            // note this functions GetAgentId or GetAgentId1
            dr = AgentBL.GetAgentId1(ccode, pcode, rid);
            string milktype = string.Empty;
            ddl_mAgentname.Items.Clear();
            ddl_mAgentid.Items.Clear();
            ddl_mAgentmobno.Items.Clear();     
            txt_magentmobno.Text ="0";
            int acctype = 0;            
            rd_mbulk.Checked = true;
            txt_CarAmt.Text = string.Empty;
            chk_maccountstatus.Checked = false;
            string bank = string.Empty;
            int mbankid = 0;
            string mbankids = string.Empty;
            

            MLoadBankId();
            MLoadIfsccode();
          
            txt_mAgentAccNo.Text = string.Empty;
          
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (ddl_mAgentname.Items.Count < 1)
                    {
                        ddl_mAgentid.Items.Add(dr["Agent_id"].ToString());
                        ddl_mAgentname.Items.Add(dr["Agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
                        txt_magentname.Text = dr["Agent_Name"].ToString();
                        txt_magentmobno.Text = dr["phone_number"].ToString();
                        acctype = Convert.ToInt32(dr["Type"].ToString());
                        milktype = dr["Milk_Nature"].ToString();
                        if (milktype == "Cow")
                        {
                            rd_mbuff.Checked = false;
                            rd_mcom.Checked = true;                          
                            //rd_mbulk.Checked = false; 
                        }
                        else
                        {
                            rd_mcom.Checked = false;
                            rd_mbuff.Checked = true;
                           // rd_mbulk.Checked = true;
                        }

                        if (acctype == 0)
                        {
                            rd_mbulk.Checked = true;
                            rd_mcentre.Checked = false;
                        }
                        else
                        {
                            rd_mcentre.Checked = true;
                            rd_mbulk.Checked = false;  
                        }


                        txt_mcartage.Text = dr["cartage_Amt"].ToString();
                        txt_msplbonus.Text = dr["SplBonus_Amt"].ToString();
                        bank = dr["payment_mode"].ToString();
                        if (bank == "BANK")
                        {
                            chk_maccountstatus.Checked = true;
                            lbl_mbank.Visible = true;
                            ddl_mBankName.Visible = true;
                            lbl_mbranname.Visible = true;
                            ddl_mbranchname.Visible = true;
                            lbl_mifsccode.Visible = true;
                            ddl_mIfsc.Visible = true;
                            lbl_maccno.Visible = true;
                            txt_mAgentAccNo.Visible = true;
                            //
                            mbankid = Convert.ToInt32(dr["bank_id"].ToString());
                            //mbankid = mbankid - 1;
                            mbankids = mbankid.ToString();
                            for (int i = 0; i <= ddl_MBankId.Items.Count; i++)
                            {
                                if (ddl_MBankId.Items[i].ToString() == mbankids)
                                {
                                    ddl_mBankName.SelectedIndex = i;
                                    ddl_MBankId.SelectedIndex = i;
                                    break;
                                }
                            }                   
                            ddl_mbranchname.Items.Clear();
                            ddl_mbranchname.Items.Add(dr["branch_name"].ToString());
                            ddl_mIfsc.Items.Clear();
                            ddl_mIfsc.Items.Add(dr["ifsc_code"].ToString());
                        }
                        else 
                        {
                            chk_maccountstatus.Checked = false;
                            lbl_mbank.Visible = false;
                            ddl_mBankName.Visible = false;
                            lbl_mbranname.Visible = false;
                            ddl_mbranchname.Visible = false;
                            lbl_mifsccode.Visible = false;
                            ddl_mIfsc.Visible = false;
                            lbl_maccno.Visible = false;
                            txt_mAgentAccNo.Visible = false;
                            //
                            mbankid = Convert.ToInt32(dr["bank_id"].ToString());
                            //mbankid = mbankid - 1;
                            for (int i = 1; i < ddl_MBankId.Items.Count; i++)
                            {
                                if (ddl_MBankId.Items[i].ToString() == mbankid.ToString())
                                {
                                    ddl_mBankName.SelectedIndex = i;
                                    ddl_MBankId.SelectedIndex = i;
                                    break;
                                }
                            }                       
                            ddl_mbranchname.Items.Clear();
                            ddl_mbranchname.Items.Add(dr["branch_name"].ToString());
                            ddl_mIfsc.Items.Clear();
                            ddl_mIfsc.Items.Add(dr["ifsc_code"].ToString());
                       
                        }
                       

                        txt_mAgentAccNo.Text = dr["agent_AccountNo"].ToString();
                        ddl_mAgentmobno.Items.Add(dr["Agent_Name"].ToString() + "_" + dr["phone_number"].ToString() + "_" + dr["Type"].ToString() + "_" + dr["cartage_Amt"].ToString() + "_" + dr["payment_mode"].ToString() + "_" + dr["bank_id"].ToString() + "_" + dr["ifsc_code"].ToString() + "_" + dr["agent_AccountNo"].ToString() + "_" + dr["Milk_Nature"].ToString() + "_" + dr["branch_name"].ToString() + "_" + dr["SplBonus_Amt"].ToString());
                    }
                    else
                    {
                        ddl_mAgentname.Items.Add(dr["Agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
                        ddl_mAgentid.Items.Add(dr["Agent_id"].ToString());
                        ddl_mAgentmobno.Items.Add(dr["Agent_Name"].ToString() + "_" + dr["phone_number"].ToString() + "_" + dr["Type"].ToString() + "_" + dr["cartage_Amt"].ToString() + "_" + dr["payment_mode"].ToString() + "_" + dr["bank_id"].ToString() + "_" + dr["ifsc_code"].ToString() + "_" + dr["agent_AccountNo"].ToString() + "_" + dr["Milk_Nature"].ToString() + "_" + dr["branch_name"].ToString() + "_" + dr["SplBonus_Amt"].ToString());
                    }
                }
            }
            else
            {
                ddl_mAgentname.Items.Clear();
                ddl_mAgentid.Items.Clear();
                txt_magentname.Text = "";
                WebMsgBox.Show("Agent is Not Added");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void chk_modifyagent_CheckedChanged(object sender, EventArgs e)
    {
    //    if (chk_modifyagent.Checked == true)
    //    {
    //        //this.ModalPopupExtender1.Show();
    //        //MLoadAgentid();
    //    }
    //    else
    //    {
    //        this.ModalPopupExtender1.Hide();
    //    }
    }
    protected void rd_mcom_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_mcom.Checked == true)
        {
            rd_mbuff.Checked = false;
           // this.ModalPopupExtender1.Show();
          
        }
    }
    protected void rd_mbuff_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_mbuff.Checked == true)
        {
            rd_mcom.Checked = false;
          //  this.ModalPopupExtender1.Show();           
        }
    }
    protected void ddl_mAgentname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string magentdetailsAll = string.Empty;
            string[] magentdetails = new string[10];
            int acctype = 0;
            string bank = string.Empty;
            int mbankid = 0;
            ddl_mAgentid.SelectedIndex = ddl_mAgentname.SelectedIndex;
            ddl_mAgentmobno.SelectedIndex = ddl_mAgentname.SelectedIndex;
            magentdetailsAll = ddl_mAgentmobno.SelectedItem.Value;
            //ddl_mAgentmobno.Items.Add(dr["Agent_Name"].ToString() + "_" + dr["phone_number"].ToString() + "_" + dr["Type"].ToString() + "_" + dr["cartage_Amt"].ToString() + "_" + dr["payment_mode"].ToString() + "_" + dr["bank_id"].ToString() + "_" + dr["ifsc_code"].ToString() + "_" + dr["agent_AccountNo"].ToString());
            magentdetails = magentdetailsAll.Split('_');

            txt_magentname.Text = magentdetails[0];
            txt_magentmobno.Text = magentdetails[1];
            acctype = Convert.ToInt32(magentdetails[2]);
            if (acctype == 0)
            {
                rd_mbulk.Checked = true;
                rd_mcentre.Checked = false;
            }
            else
            {
                rd_mcentre.Checked = true;
                rd_mbulk.Checked = false;
            }
            txt_mcartage.Text = magentdetails[3];
            txt_msplbonus.Text = magentdetails[10];
            bank = magentdetails[4];
            if (bank == "BANK")
            {
                chk_maccountstatus.Checked = true;
                lbl_mbank.Visible = true;
                ddl_mBankName.Visible = true;
                lbl_mbranname.Visible = true;
                ddl_mbranchname.Visible = true;
                lbl_mifsccode.Visible = true;
                ddl_mIfsc.Visible = true;
                lbl_maccno.Visible = true;
                txt_mAgentAccNo.Visible = true;
                mbankid = Convert.ToInt32(magentdetails[5]);
                // mbankid = mbankid - 1;
                for (int i = 0; i <= ddl_MBankId.Items.Count; i++)
                {
                    if (ddl_MBankId.Items[i].ToString() == mbankid.ToString())
                    {
                        ddl_mBankName.SelectedIndex = i;
                        ddl_MBankId.SelectedIndex = i;
                        break;
                    }
                }
                ddl_mbranchname.Items.Clear();
                ddl_mbranchname.Items.Add(magentdetails[9]);
                ddl_mIfsc.Items.Clear();
                ddl_mIfsc.Items.Add(magentdetails[6]);
                txt_mAgentAccNo.Text = magentdetails[7];
                
            }
            else
            {
                chk_maccountstatus.Checked = false;
                lbl_mbank.Visible = false;
                ddl_mBankName.Visible = false;
                lbl_mbranname.Visible = false;
                ddl_mbranchname.Visible = false;
                lbl_mifsccode.Visible = false;
                ddl_mIfsc.Visible = false;
                lbl_maccno.Visible = false;
                txt_mAgentAccNo.Visible = false;
                mbankid = Convert.ToInt32(magentdetails[5]);
                // mbankid = mbankid - 1;
                //for (int i = 0; i <= ddl_MBankId.Items.Count; i++)
                //{
                //    if (ddl_MBankId.Items[i].ToString() == mbankid.ToString())
                //    {
                //        ddl_mBankName.SelectedIndex = i;
                //        ddl_MBankId.SelectedIndex = i;
                //        break;
                //    }
                //}       
                ddl_mbranchname.Items.Clear();
                ddl_mbranchname.Items.Add(magentdetails[9]);
                ddl_mIfsc.Items.Clear();
                ddl_mIfsc.Items.Add(magentdetails[6]);
            }

            if (magentdetails[8] == "Cow")
            {
                rd_mcom.Checked = true;
                rd_mbuff.Checked = false;
            }
            else
            {
                rd_mbuff.Checked = true;
                rd_mcom.Checked = false;
            }


            txt_mcartage.Focus();
           // this.ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {

        }
    }
    protected void rd_mbulk_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_mbulk.Checked == true)
        {
            rd_mcentre.Checked = false;
           // this.ModalPopupExtender1.Show();
        }
    }
    protected void rd_mcentre_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_mcentre.Checked == true)
        {
            rd_mbulk.Checked = false;
          //  this.ModalPopupExtender1.Show();
        }
    }
    protected void ddl_MBankId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_mBankName.SelectedIndex = ddl_MBankId.SelectedIndex;
    }
    protected void ddl_mBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_MBankId.SelectedIndex = ddl_mBankName.SelectedIndex;
        MLoadIfsccode();
        txt_mAgentAccNo.Text = "";
       // this.ModalPopupExtender1.Show();
    }
    protected void chk_maccountstatus_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_maccountstatus.Checked == true)
        {           
            lbl_mbank.Visible = true;
            ddl_mBankName.Visible = true;
            ddl_mBankName.Visible = true;
            lbl_mbranname.Visible = true;
            ddl_mbranchname.Visible = true;
            lbl_mifsccode.Visible = true;
            ddl_mIfsc.Visible = true;
            lbl_maccno.Visible = true;
            txt_mAgentAccNo.Visible = true;
            MLoadIfsccode();
          //  this.ModalPopupExtender1.Show();

        }
        else
        {
            lbl_mbank.Visible = false;
            ddl_mBankName.Visible = false;
            ddl_mBankName.Visible = false;
            lbl_mbranname.Visible = false;
            ddl_mbranchname.Visible = false;
            lbl_mifsccode.Visible = false;          
            ddl_mIfsc.Visible = false;
            lbl_maccno.Visible = false;
            txt_mAgentAccNo.Visible = false;
          //  this.ModalPopupExtender1.Show();
        }
    }
    
    private bool mvalidates()
    {
        if (string.IsNullOrEmpty(txt_magentname.Text))
        {
            WebMsgBox.Show("Please Enter the AgentName");
            txt_magentname.Focus();
         //   this.ModalPopupExtender1.Show();
            return false;            
        }
        if (chk_maccountstatus.Checked == true)
        {
            if (string.IsNullOrEmpty(txt_mAgentAccNo.Text))
            {
                WebMsgBox.Show("Please Enter the Account_No");
                txt_magentname.Focus();
             //   this.ModalPopupExtender1.Show();
                return false;
            }
        }
        return true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //if (mvalidates())
        //{
        //    MsetBO();
        //    AgentBL.insertagent(AgentBO);
        //    loadagentid();
        //    gvProducts.DataBind();
        //    txt_magentname.Text = "";
        //    txt_magentmobno.Text = "";
        //    txt_mcartage.Text = "";
        //    txt_mAgentAccNo.Text = "";
        //    WebMsgBox.Show("mvalidates");
        //}
        WebMsgBox.Show("mvalidates out");
    }
    protected void gvProducts_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        MLoadAgentid();
      //  this.ModalPopupExtender1.Show();
    }
    protected void rd_update_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            rd_update.Checked = false;
            if (mvalidates())
            {

                try
                {
                    getagentlist();
                    getupdate();
                }
                catch
                {


                }
                MsetBO();
                AgentBL.insertagent(AgentBO);
                loadagentid();
                MLoadAgentid();
                gvProducts.DataBind();
                txt_magentname.Text = "";
                txt_magentmobno.Text = "";
                txt_mcartage.Text = "";
                txt_mAgentAccNo.Text = "";
                txt_msplbonus.Text = "";
                MLoadAgentid();

                WebMsgBox.Show("success");
              //  this.ModalPopupExtender1.Show();








            }
        }
        catch (Exception ex)
        {

        }

    }

    public void getagentlist()
    {
        try
        {
            string[] FIAGENT = ddl_mAgentname.SelectedItem.Text.Split('_');

            string agentli = "Select    Agent_Id,Agent_Name,phone_Number,Cartage_Amt,SplBonus_Amt,Bank_Id,Ifsc_code,Agent_AccountNo,Plant_code   from Agent_Master    where Plant_code='" + pcode + "'  and Agent_Id='" + FIAGENT[0] + "'";
            con = DBclass.GetConnection();
            SqlCommand cmd = new SqlCommand(agentli, con);
            SqlDataAdapter dsr = new SqlDataAdapter(cmd);
            listoutagent.Rows.Clear();
            dsr.Fill(listoutagent);
            if (listoutagent.Rows.Count > 0)
            {
              
                   
                 agentid   =  listoutagent.Rows[0][0].ToString();
                 Agentname =  listoutagent.Rows[0][1].ToString();
                 Mobile =     listoutagent.Rows[0][2].ToString();
                 cartage =    listoutagent.Rows[0][3].ToString();
                 splbonus =   listoutagent.Rows[0][4].ToString();
                 BankName =   listoutagent.Rows[0][5].ToString();
                 ifsccode =   listoutagent.Rows[0][6].ToString();
                 accno =      listoutagent.Rows[0][7].ToString();
                 plant_code = listoutagent.Rows[0][8].ToString();
            }
            else
            {



            }
        }
        catch
        {

        }
    }

    public void getupdate()
    {
        try
        {
            string[] FIAGENT1 = ddl_mAgentname.SelectedItem.Text.Split('_');
            string insert;
            con = DBclass.GetConnection();
            insert = "insert into agent_masterLogs(Agent_Id,Agent_Name,Mobile,cartage,splbonus,BankName,ifsccode,accno,Plant_code,NAgent_Id,NAgent_Name,NMobile,Ncartage,Nsplbonus,NBankName,Nifsccode,Naccno,NPlant_code,NBranchName,Username) values('" + agentid + "','" + Agentname + "','" + Mobile.Trim() + "','" + cartage + "','" + splbonus + "','" + BankName + "','" + ifsccode + "','" + accno + "','" + plant_code + "','" + FIAGENT1[0].ToString() + "','" + txt_magentname.Text + "','" + txt_magentmobno.Text + "','" + txt_mcartage.Text + "','" + txt_msplbonus.Text + "','" + ddl_mBankName.Text + "','" + ddl_mIfsc.Text + "','" + txt_mAgentAccNo.Text.Trim() + "','"+pcode+"','" + ddl_mbranchname.Text + "','" + Session["Name"].ToString() + "')";
            SqlCommand cmd = new SqlCommand(insert, con);
            cmd.ExecuteNonQuery();
        }
        catch
        {


        }
    }

    protected void ddl_mbranchname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_mIfsc.SelectedIndex = ddl_mbranchname.SelectedIndex;
       // this.ModalPopupExtender1.Show();
    }
    protected void ddl_mIfsc_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_mbranchname.SelectedIndex = ddl_mIfsc.SelectedIndex;
     //   this.ModalPopupExtender1.Show();
    }
    protected void btn_Addagent_Click(object sender, EventArgs e)
    {
        if (validates())
        {
            int result = UserStatus();
            if (result == 1)
            {

            }
            else if (result == 0)
            {

                Session["User_LoginId"] = centreid;

                uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);

            }
        }

    }
    protected void btn_updatecancel_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        loadrouteid();
        loadagentid();
        LoadBankId();
        LoadIfsccode();
        MLoadAgentid();
    }
}

