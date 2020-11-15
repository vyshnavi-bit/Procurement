using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SelectLoan : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    
    BLLLoanPlant loanpBL = new BLLLoanPlant();
    DALLoanPlant loanpDA = new DALLoanPlant();
    BOLLoanPlant loanpBO = new BOLLoanPlant();
    BLLroutmaster BLroute = new BLLroutmaster();
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess = new DbHelper();
    public int Company_code, plant_Code;
    string routen;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                Company_code = Convert.ToInt32(Session["Company_code"]);
                
                LoadPlantName();
                plant_Code = Convert.ToInt32(Session[ddl_Plantcode.Text]);
                
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
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                Company_code = Convert.ToInt32(Session["Company_code"]);
                plant_Code = Convert.ToInt32(Session[ddl_Plantcode.Text]);
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }

    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }

   
    private void LoadPlantName()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(Convert.ToString(Company_code));
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());  

                }
            }
            else
            {
                ddl_Plantname.Items.Add("-Select Plant-");
            }
            
        }
        catch (Exception ex)
        {
            uscMsgBox1.AddMessage("" + ex, MessageBoxUsc_Message.enmMessageType.Attention);

        }
        

    }

    private bool validates()
    {
        if (ddl_Plantname.Text == string.Empty || ddl_Plantname.Text == "-Select Plant-")
        {
            uscMsgBox1.AddMessage("Select Plant", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        return true;
    }

    protected void btn_SaveData_Click(object sender, EventArgs e)
    {
        try
        {
            validates();
        if(rd_Plantwiseratechart.Checked==true)
        {
            rd_Routewiseratechart.Checked = false;
            saveplant();
        }
        else
        {
            rd_Plantwiseratechart.Checked = false;
            saveroute();
        }
        }
        catch (Exception ex)
        {

        }

    }

    private void saveroute()
    {
        try
        {
            if (MChk_RouteName.Checked == true)
            {
                
                for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                {
                    if (CheckBoxList1.Items[i].Selected == true)
                    {

                        loanpBO.Companycode = Company_code;
                        loanpBO.Plantcode = Convert.ToInt32(ddl_Plantcode.Text);
                        loanpBO.Routeid = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                        loanpBO.Lstatus = true;
                        loanpBO.Loanmode = 2;
                        loanpBL.inserttlstatus(loanpBO);
                        loanpBL.insertloanplant(loanpBO);

                    }
                    else
                    {
                        loanpBO.Companycode = Company_code;
                        loanpBO.Plantcode = Convert.ToInt32(ddl_Plantcode.Text);
                        loanpBO.Routeid = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                        loanpBO.Lstatus = false;
                        loanpBL.inserttlstatus(loanpBO);
                    }
                    
                }
                chktrue();
                uscMsgBox1.AddMessage("Loan added successfully", MessageBoxUsc_Message.enmMessageType.Success);
                LoadPlantName();
               

            }
            else
            {
                uscMsgBox1.AddMessage("please,Select the RouteName_Master", MessageBoxUsc_Message.enmMessageType.Attention);

            }
        }

        catch (Exception ex)
        {

        }

    }
    private void saveplant()
    {
        try
        {

            SETBO1();
            loanpBL.insertloanplant(loanpBO);
            uscMsgBox1.AddMessage("Loan added successfully", MessageBoxUsc_Message.enmMessageType.Success);
            LoadPlantName();
        }
        catch (Exception ex)
        {

        }
    }
    private void SETBO1()
    {
        try
        {
            loanpBO.Companycode=Company_code;
            loanpBO.Plantcode = Convert.ToInt32(ddl_Plantcode.Text);
            loanpBO.Loanmode=1;
            
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString);
            con.Open();
            string str = "UPDATE ROUTE_MASTER SET LSTATUS='1' WHERE Plant_Code='" + ddl_Plantcode.Text + "' AND Company_Code='" + Company_code + "'";
            SqlCommand cmd = new SqlCommand(str,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {

        }
    }
    private void chktrue()
    {
        rd_Plantwiseratechart.Checked = true;
        if (rd_Plantwiseratechart.Checked == true)
        {
            rd_Routewiseratechart.Checked = false;
            MChk_RouteName.Checked = false;
            Table2.Visible = false;

        }
    }
    protected void rd_Plantwiseratechart_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chktrue();

        }
        catch (Exception ex)
        {
        }

    }
    protected void rd_Routewiseratechart_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rd_Routewiseratechart.Checked == true)
            {
            rd_Plantwiseratechart.Checked = false;
            Table2.Visible = true;
            MChk_RouteName.Checked = true;
            LoadRouteName();
            Mroute_RouteName();
            
            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadRouteName()
    {
        try
        {
            ds = null;
            ds = BLroute.getroutmasterdatareader3(Company_code.ToString(),ddl_Plantcode.Text);
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "ROUTE_NAME";
                CheckBoxList1.DataValueField = "ROUTE_ID";//ROUTE_ID 
                CheckBoxList1.DataBind();

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
        }
    }
    private void Mroute_RouteName()
    {
        if (MChk_RouteName.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }

        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        chktrue();
    }
    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void MChk_RouteName_CheckedChanged(object sender, EventArgs e)
    {
        Mroute_RouteName();
    }
}