using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class LoanDueRecovery : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public int rdir = 0;
  
    DateTime dtm = new DateTime();
    SqlDataReader dr = null;
    DataRow dr1;
    DataTable dt = new DataTable();
    BLLuser Bllusers = new BLLuser();
    BLLroutmaster routeBL = new BLLroutmaster();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    BOLLoanDueRecovery boldr = new BOLLoanDueRecovery();
    BLLLoanDueRecovery blldr = new BLLLoanDueRecovery();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                dtm = System.DateTime.Now;
                txt_LoanRecoveryDate.Text = dtm.ToString("dd/MM/yyy");
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                }
                if(roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                }
                LoadRefNo();
                loadrouteid();
                LoadAgentid();
                loadgriddata();
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
                pcode = ddl_Plantcode.SelectedItem.Value;
               

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
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
    private void LoadRefNo()
    {
        txt_RefNo.Text = blldr.GetLoanRefNo(ccode, pcode).ToString();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (valid())
            {
                string mess = string.Empty;
                SETBO();
                mess = blldr.InsertLoanDueRecovery(boldr);
                Reset();
                loadgriddata();
                LoadRefNo();
                txt_DueRecoveryAmount.Focus();               
                uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Attention);
            }
        }
        catch (Exception ex)
        {
            uscMsgBox1.AddMessage(ex.ToString(), MessageBoxUsc_Message.enmMessageType.Attention);
        }
    }
    private bool valid()
    {
        try
        {
            if(string.IsNullOrEmpty(txt_RefNo.Text))
            {
                uscMsgBox1.AddMessage("Check the ReferenceNo", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_RefNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_LoanRecoveryDate.Text))
            {
                uscMsgBox1.AddMessage("Check the LoanRecoveryDate", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_LoanRecoveryDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_LoanId.Text))
            {
                uscMsgBox1.AddMessage("Check the LoanId", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_LoanId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_DueBalance.Text))
            {
                uscMsgBox1.AddMessage("Check the DueBalance", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_DueBalance.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_DueRecoveryAmount.Text))
            {
                uscMsgBox1.AddMessage("Check the DueRecoveryAmount", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_DueRecoveryAmount.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_Balance.Text))
            {
                uscMsgBox1.AddMessage("Check the Balance", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Balance.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_Remarks.Text))
            {
                uscMsgBox1.AddMessage("Check the Remarks", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Remarks.Focus();
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private void SETBO()
    {
        try
        {
            boldr.LoanDueRefid = int.Parse(txt_RefNo.Text);
            boldr.Companycode = int.Parse(ccode);
            boldr.Plantcode = int.Parse(pcode);
            boldr.RouteId = int.Parse(cmb_RouteID.SelectedItem.Value);
            boldr.AgentId = int.Parse(ddl_AgentId.SelectedItem.Value);
            boldr.LoanRecoveryDate = DateTime.ParseExact(txt_LoanRecoveryDate.Text,"dd/MM/yyyy", null);
            boldr.Loan_Id = int.Parse(txt_LoanId.Text);
            boldr.LoanDueBalance = float.Parse(txt_DueBalance.Text);
            boldr.LoanDueRecoveryAmount = float.Parse(txt_DueRecoveryAmount.Text);
            boldr.LoanBalance = float.Parse(txt_Balance.Text);
            boldr.Remarks = txt_Remarks.Text;
        }
        catch (Exception ex)
        {
            uscMsgBox1.AddMessage(ex.ToString(), MessageBoxUsc_Message.enmMessageType.Attention);
        }
    }
    private void Reset()
    {
        txt_LoanId.Text = "";
        txt_DueBalance.Text = "";
        txt_DueRecoveryAmount.Text = "";
        txt_Balance.Text = "";
        txt_Remarks.Text = "";
        txt_InstalAmount.Text="";
    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
                ddl_Plantname.SelectedIndex = 1;
                ddl_Plantcode.SelectedIndex = 1;
                pcode = ddl_Plantcode.SelectedItem.Value;
                
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void loadrouteid()
    {
        try
        {
            SqlDataReader dr;
            dr = routeBL.getroutmasterdatareader(ccode, pcode);
            cmb_RouteID.Items.Clear();
            cmb_RouteName.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cmb_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());
                    cmb_RouteName.Items.Add(dr["Route_ID"] + "_".ToString().Trim() + dr["Route_Name"].ToString().Trim());
                }
                rdir = Convert.ToInt32(cmb_RouteID.SelectedItem.Value);
                LoadAgentid();
                LoadRefNo();
            }
            else
            {
                ddl_AgentId.Items.Clear();
                ddl_AgentName.Items.Clear();
                uscMsgBox1.AddMessage("Route Not Available...", MessageBoxUsc_Message.enmMessageType.Attention);
            }
        }
        catch (Exception em)
        {

            uscMsgBox1.AddMessage("" + em, MessageBoxUsc_Message.enmMessageType.Attention);
        }
    }

    private void LoadAgentid()
    {
        try
        {
            dr = null;
            dr = agentBL.GetAgentId(ccode, pcode, rdir);
            ddl_AgentId.Items.Clear();
            ddl_AgentName.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_AgentId.Items.Add(dr["Agent_id"].ToString());
                    ddl_AgentName.Items.Add(dr["Agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
                }
                GetAgentLoanId();
            }
            else
            {
                //txt_AgentId.Text = "";
                WebMsgBox.Show("Agent is Not Added");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void loadgriddata()
    {
        int dtcount;
        dt = null;
        dt = blldr.GetLoanDueRecoveryData(ccode,pcode);
        dtcount = dt.Rows.Count;
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();

            WebMsgBox.Show("This Grid have Empty Rows Only...");
        }
    }


    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        loadrouteid();
        loadgriddata();
    }
    protected void cmb_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmb_RouteID.SelectedIndex = cmb_RouteName.SelectedIndex;
        rdir = Convert.ToInt32(cmb_RouteID.SelectedItem.Value);
        LoadAgentid();
    }
    protected void ddl_AgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_AgentId.SelectedIndex = ddl_AgentName.SelectedIndex;
        GetAgentLoanId();
    }
    private void GetAgentLoanId()
    {
        try
        {
            dr = null;
            txt_LoanId.Text = "";
            txt_DueBalance.Text = "";
            dr = blldr.GetAgentLoanId(ccode, pcode, ddl_AgentId.SelectedItem.Value);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_LoanId.Text = dr["loan_Id"].ToString();
                    txt_DueBalance.Text = dr["balance"].ToString();
                    txt_InstalAmount.Text = dr["inst_amount"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Thread.Sleep(3000);
        Reset();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        loadgriddata();
        //GridView1.EditIndex = e.RowIndex;
        //int index = GridView1.EditIndex;
        //GridViewRow row = GridView1.Rows[index];
        //dr1 = dt.Rows[index];
        //boldr.LoanDueRefid = Convert.ToInt32(dr1[0]);
        //boldr.Companycode = Convert.ToInt32(ccode);
        //boldr.Plantcode = Convert.ToInt32(dr1[1]);

        //blldr.DeleteLoanDueRecoveryData(boldr);
        //loadgriddata();
        //uscMsgBox1.AddMessage("Delete Successfully", MessageBoxUsc_Message.enmMessageType.Attention);

    }

    protected void txt_LoanId_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_DueBalance_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_DueRecoveryAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double DueBalance = Convert.ToDouble(txt_DueBalance.Text);
            double DueRecoveryAmount = Convert.ToDouble(txt_DueRecoveryAmount.Text);
            if (DueRecoveryAmount > DueBalance)
            {
                txt_DueRecoveryAmount.Focus();
                uscMsgBox1.AddMessage("Check DueRecovery Amount", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_DueRecoveryAmount.Focus();
            }
            else
            {
                txt_Balance.Text = (DueBalance - DueRecoveryAmount).ToString();
                txt_Remarks.Focus();
            }
        }
        catch (Exception ex)
        {
        }
        
    }
    protected void txt_Balance_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txt_RefNo_TextChanged(object sender, EventArgs e)
    {

    }
}