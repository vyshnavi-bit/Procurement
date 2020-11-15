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
using System.Text.RegularExpressions;
public partial class Bankmaster : System.Web.UI.Page
{
    string uid;
    string centreid;
    int cmpcode, pcode;
    string getbranchname;
    SqlConnection con = new SqlConnection();
    DbHelper DBclass = new DbHelper();
    BLLAgentmaster AgentBL = new BLLAgentmaster();
    BLLBankMaster bankmasterBL = new BLLBankMaster();
    BOLBankMaster bankmasterBO = new BOLBankMaster();
    public static int roleid;
    BLLuser Bllusers = new BLLuser();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            uscMsgBox1.MsgBoxAnswered += MessageAnswered;
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                cmpcode = Convert.ToInt32(Session["Company_code"]);
                roleid = Convert.ToInt32(Session["Role"].ToString());
                if (roleid < 3)
                {
                    pcode = Convert.ToInt32(Session["Plant_Code"]);
                    loadsingleplant();
                    txt_Bankname.Focus();
                    LoadBank();
                    loadgrid();
                }
                if ((roleid >= 3) && (roleid != 9))
                {

                    LoadPlantcode();
                    txt_Bankname.Focus();
                    LoadBank();
                    loadgrid();
                }

                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    txt_Bankname.Focus();
                    LoadBank();
                    loadgrid();
                }
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
            LoadBank();
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Value);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                txt_Bankname.Focus();
                loadgrid();
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

            dr = Bllusers.LoadPlantcode(cmpcode.ToString());
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
            dr = Bllusers.LoadSinglePlantcode(cmpcode.ToString(), pcode.ToString());
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
            dr = Bllusers.LoadSinglePlantcode(cmpcode.ToString(), "170");
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


    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btn_Save_Click(object sender, EventArgs e)
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
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    public void setBO()
    {
        if (txt_BankId.Text != string.Empty)

            bankmasterBO.BankID = txt_BankId.Text;
        if (txt_Bankname.Text != string.Empty)

            bankmasterBO.BankName = txt_Bankname.Text;

        if (txt_Branchname.Text != string.Empty)

            bankmasterBO.BranchName = txt_Branchname.Text;

        if (txt_Ifsccode.Text != string.Empty)

            bankmasterBO.Ifsccode = txt_Ifsccode.Text;

        if (!string.IsNullOrEmpty(txt_Phoneno.Text))

            bankmasterBO.Phonenumber = txt_Phoneno.Text.Trim();

        if (txt_Branchcode.Text != string.Empty)
            bankmasterBO.BranchCode = txt_Branchcode.Text;

        //Assign Plantcode and companycode

        bankmasterBO.Plantcode = pcode;
        bankmasterBO.Companycode = cmpcode;

        //  bankmasterBO.Tid = deleteid.ToString();
    }

    public void cleare()
    {

        //txt_Bankname.Text = "";
        txt_Branchname.Text = "";
        txt_Branchcode.Text = "";
        txt_Ifsccode.Text = "";
        txt_Phoneno.Text = "";
        //loadbankid();
        //loadgrid();

    }
    public void loadgrid()
    {
       con =   DBclass.GetConnection();
        string str = "";
        str = "SELECT Bank_name,Branch_Name,Ifsc_code FROM BANK_MASTER WHERE([Plant_code]='" +   pcode + "') AND ([Company_code]= 1) and bank_id='" + ddl_BankId.SelectedItem.Value + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dsa = new SqlDataAdapter(cmd);
        DataTable dtfg = new DataTable();
        dsa.Fill(dtfg);
        if (dtfg.Rows.Count > 0)
        {
            GridView1.DataSource = dtfg;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "No records";
            GridView1.DataBind();

        }
    }
    private void LoadBank()
    {
        try
        {
            SqlDataReader dr = null;
            dr = AgentBL.GetBankIDComm(cmpcode.ToString());
            ddl_BankId.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_BankId.Items.Add(dr["Bank_ID"].ToString().Trim());
                    ddl_BankName.Items.Add(dr["Bank_ID"].ToString().Trim() + "_" + dr["Bank_Name"].ToString().Trim());
                    // ddl_BankId.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                if (ddl_BankId.Items.Count > 0)
                {
                    txt_BankId.Text = ddl_BankId.SelectedItem.Value;
                    txt_Bankname.Text = ddl_BankName.SelectedItem.Value;
                }

            }
            else
            {
                uscMsgBox1.AddMessage("Bank Not Found", MessageBoxUsc_Message.enmMessageType.Attention);
            }

        }
        catch (Exception em)
        {
            uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        }

    }
    public void loadbankid()
    {

        try
        {

            int mx = 0;
            string sqlstr = "SELECT MAX(Bank_ID) FROM Bank_Master WHERE Company_code=" + cmpcode + " AND Plant_code=" + pcode + "";
            mx = Convert.ToInt32(DBclass.ExecuteScalarstr(sqlstr));
            //int iid = Convert.ToInt32(mx);
            mx = mx + 1;


            txt_BankId.Text = mx.ToString();
            //txtcompanyuserid.Text = mx.ToString() + "CMPUSER";
        }
        catch
        {
            txt_BankId.Text = "1";
            // txtcompanyuserid.Text = "1001" + "CMPUSER";
        }

    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_Bankname.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    public int UserStatus()
    {
        try
        {
            if (txt_Bankname.Text == string.Empty)
            {
                uscMsgBox1.AddMessage("Please Enter Bank Name", MessageBoxUsc_Message.enmMessageType.Attention);

                return 1;
            }
            else
            {

                if ((txt_Bankname.Text != string.Empty))
                {
                    setBO();
                    bankmasterBL.insertbankmaster(bankmasterBO);
                    loadgrid();
                    //loadbankid();
                    cleare();

                }
                return 0;
            }
            //else
            //{
            //    SqlDataReader dr;

            //    string str = "SELECT * from Bank_Master where Bank_Name='" + txt_Bankname.Text + "' AND Company_code=" + cmpcode + " AND Plant_code=" + pcode + "";
            //    dr = DBclass.GetDatareader(str);
            //    if (dr.Read())
            //    {
            //        uscMsgBox1.AddMessage("Bank Name AlReady Exists", MessageBoxUsc_Message.enmMessageType.Attention);

            //        return 1;
            //    }
            //    else
            //    {

            //        if ((txt_Bankname.Text != string.Empty))
            //        {
            //            setBO();
            //            bankmasterBL.insertbankmaster(bankmasterBO);
            //            loadgrid();
            //            //loadbankid();
            //            cleare();

            //        }
            //        return 0;
            //    }
            //}
        }
        catch (Exception ex)
        {
            uscMsgBox1.AddMessage(ex.ToString(), MessageBoxUsc_Message.enmMessageType.Attention);

            return 1;
        }

    }
    protected void ddl_BankId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BankName.SelectedIndex = ddl_BankId.SelectedIndex;
    }
    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_BankId.SelectedIndex = ddl_BankName.SelectedIndex;
        txt_BankId.Text = ddl_BankId.SelectedItem.Value;
        txt_Bankname.Text = ddl_BankName.SelectedItem.Value;
        loadgrid();
    }
    public void getbankentrydetails()
    {

        //try
        //{
        //    SqlDataReader dr = null;
        //    dr = AgentBL.GetBankIDComm(pcode,ddl_BankId.Text,ddl_BankName.Text);
        //    ddl_BankId.Items.Clear();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            ddl_BankId.Items.Add(dr["Bank_ID"].ToString().Trim());
        //            ddl_BankName.Items.Add(dr["Bank_ID"].ToString().Trim() + "_" + dr["Bank_Name"].ToString().Trim());
        //            // ddl_BankId.Items.Insert(0, new ListItem("--Select--", "0"));
        //        }
        //        if (ddl_BankId.Items.Count > 0)
        //        {
        //            txt_BankId.Text = ddl_BankId.SelectedItem.Value;
        //            txt_Bankname.Text = ddl_BankName.SelectedItem.Value;
        //        }

        //    }
        //    else
        //    {
        //        uscMsgBox1.AddMessage("Bank Not Found", MessageBoxUsc_Message.enmMessageType.Attention);
        //    }

        //}
        //catch (Exception em)
        //{
        //    uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        //}

        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            DataSet ds = new DataSet();
            // TextBox7.Text = "";
            string sqlstr = "SELECT     count(*) as tot     FROM bank_master WHERE   plant_code='" + pcode + "'  and BANK_ID='" + ddl_BankId.Text + "' and branch_name='" + txt_Branchname.Text + "' ";
            SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

            adp.Fill(ds);
            getbranchname = ds.Tables[0].Rows[0]["tot"].ToString();


        }
    }
    protected void txt_Branchname_TextChanged(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {
            getbankentrydetails();
            if (getbranchname == "0")
            {


            }
            else
            {
                uscMsgBox1.AddMessage("Branch Name Must Be Unique", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Branchname.Text = "";
                txt_Branchname.Focus();
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Value);
        LoadBank();
        loadgrid();
    }
    protected void btn_CALC_Click(object sender, EventArgs e)
    {
       // System.Diagnostics.Process.Start("C:\\Windows\\System32\\calc.exe");
        System.Diagnostics.Process.Start("calc.exe");

    }
}