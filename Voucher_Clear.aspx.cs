using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Voucher_Clear : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public int rdir = 0;
    DataTable dt = new DataTable();
    BLLuser Bllusers = new BLLuser();
    BLLroutmaster routeBL = new BLLroutmaster();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    BOLVoucher bovoucher = new BOLVoucher();
    BLLVoucher blvoucher = new BLLVoucher();
    DateTime dtm = new DateTime();
    public static SqlDataReader dr = null;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                // pcode = Session["Plant_Code"].ToString();
                dtm = System.DateTime.Now;
                roleid = Convert.ToInt32(Session["Role"].ToString());
                txt_ClearingDate.Text = dtm.ToString("dd/MM/yyy");
                txt_InwardDate.Text = dtm.ToString("dd/MM/yyy");
                cmb_session.SelectedIndex = 1;
                if((roleid>=3) && (roleid!=9))
                {
                LoadPlantcode();
                }
                if (roleid == 9)
                {
                Session["Plant_Code"] = "170".ToString();
                pcode = "170";
                loadspecialsingleplant();
                }
                pcode = ddl_Plantcode.SelectedItem.Value;
                loadrouteid();
                // rdir = Convert.ToInt32(cmb_RouteID.SelectedItem.Value);
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
                //rdir = Convert.ToInt32(cmb_RouteID.SelectedItem.Value);            
                // pcode = Session["Plant_Code"].ToString();
                //rdir = Convert.ToInt32(cmb_RouteID.SelectedItem.Value)
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


    private void loadgriddata()
    {
        //int dtcount;
        //dt = null;
        //dt = blvoucher.GetVoucherGriddata(ccode, pcode);
        //dtcount = dt.Rows.Count;
        //if (dtcount > 0)
        //{
        //    GridView1.DataSource = dt;
        //   GridView1.DataBind();

        //}
        //else
        //{
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();

        //    WebMsgBox.Show("This Grid have Empty Rows Only...");
        //}
        //AKBAR
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;//
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        int dtcount;
        string voucher;
        dt = blvoucher.GetVoucherGriddata(ccode, pcode);
        voucher = "select Ref_id,plant_code,plant_name,Agent_id,CONVERT(VARCHAR,Clearing_Date,103) AS ClearingDate,Inward_Date,Sess,Milk_ltr,Fat,Snf,Rate,Amount,Remarks    from(SELECT   Ref_id,plant_code,Agent_id,Clearing_Date,Inward_Date,Sess,Milk_ltr,Fat,Snf,Rate,Amount,Remarks    FROM Voucher_Clear where plant_code='" + pcode + "') as vl left join (select plant_code as pcode,plant_name     from  plant_master  where plant_code='" + pcode + "' ) as pm on vl.plant_code=pm.pcode";
        SqlCommand cmd = new SqlCommand(voucher, conn);
        SqlDataReader dr = cmd.ExecuteReader();
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
        //AKBAR

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
                    // cmb_RouteID.Items.Add(dr["Route_ID"]+"_R1".ToString().Trim());
                    //cmb_RouteName.Items.Add(dr["Route_Name"].ToString().Trim());
                    cmb_RouteName.Items.Add(dr["Route_ID"] + "_".ToString().Trim() + dr["Route_Name"].ToString().Trim());
                }
                rdir = Convert.ToInt32(cmb_RouteID.SelectedItem.Value);
                LoadAgentid();
            }
            else
            {
               // rdir = 1;
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

    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_ClearingDate.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }


    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        loadrouteid();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            int saveindicate = 0;
            if (txt_Mltr.Text == "")
            {
                txt_Mltr.Text = "0.0";
                saveindicate++;
            }
            if (txt_Fat.Text == "")
            {
                txt_Fat.Text = "0.0";
                saveindicate++;
            }
            if (txt_Snf.Text == "")
            {
                txt_Snf.Text = "0.0";
                saveindicate++;
            }
            if (txt_clr.Text == "")
            {
                txt_clr.Text = "0.0";
                saveindicate++;
            }
            if (txt_Rate.Text == "")
            {
                txt_Rate.Text = "0.0";
                saveindicate++;
            }
            if (txt_Amount.Text == "")
            {
                txt_Amount.Text = "0.0";
                saveindicate++;
            }

            if (saveindicate == 6)
            {
                uscMsgBox1.AddMessage("please,Fill any of the fields", MessageBoxUsc_Message.enmMessageType.Attention);
                Clear();
            }
            else
            {
                if (valid())
                {
                    string msg = string.Empty;
                    SETBO();
                    msg = blvoucher.InsertVoucherDetails(bovoucher);
                    loadgriddata();
                    Clear();
                    uscMsgBox1.AddMessage(msg, MessageBoxUsc_Message.enmMessageType.Attention);
                   
                }
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
            if (string.IsNullOrEmpty(txt_Mltr.Text))
            {
                uscMsgBox1.AddMessage("Check the Milk_Ltr", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Mltr.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_Fat.Text))
            {
                uscMsgBox1.AddMessage("Check the Fat", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Fat.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_Snf.Text))
            {
                uscMsgBox1.AddMessage("Check the Snf", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Snf.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_clr.Text))
            {
                uscMsgBox1.AddMessage("Check the Clr", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_clr.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_Rate.Text))
            {
                uscMsgBox1.AddMessage("Check the Rate", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Rate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txt_Amount.Text))
            {
                uscMsgBox1.AddMessage("Check the Amount", MessageBoxUsc_Message.enmMessageType.Attention);
                txt_Amount.Focus();
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
    private void Clear()
    {
        txt_Mltr.Text = "";
        txt_Fat.Text = "";
        txt_Snf.Text = "";
        txt_clr.Text = "";
        txt_Rate.Text = "";
        txt_Amount.Text = "";
        txt_Remarks.Text = "";

    }
    private void SETBO()
    {
        bovoucher.Companycode = int.Parse(ccode);
        bovoucher.Plantcode = int.Parse(pcode);
        bovoucher.Routeid =Convert.ToInt32(cmb_RouteID.SelectedItem.Value);
        bovoucher.Agentid = int.Parse(ddl_AgentId.SelectedItem.Value);
        //bovoucher.Clearingdate = DateTime.Parse(txt_ClearingDate.Text);
        //bovoucher.Inwarddate = DateTime.Parse(txt_InwardDate.Text);
        bovoucher.Clearingdate = DateTime.ParseExact(txt_ClearingDate.Text, "dd/MM/yyyy", null);
        bovoucher.Inwarddate = DateTime.ParseExact(txt_InwardDate.Text, "dd/MM/yyyy", null);
        bovoucher.Shift = cmb_session.SelectedItem.Value;
        bovoucher.Mlrt = float.Parse(txt_Mltr.Text);
        bovoucher.Fat = float.Parse(txt_Fat.Text);
        bovoucher.Snf = float.Parse(txt_Snf.Text);
        bovoucher.Clr = float.Parse(txt_clr.Text);
        bovoucher.Rate = float.Parse(txt_Rate.Text);
        bovoucher.Amount = float.Parse(txt_Amount.Text);
        bovoucher.Remarks = txt_Remarks.Text;
    }
   
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        loadgriddata();
        loadrouteid();
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
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void txt_Snf_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double snf = 0.0;
            double fat = 0.0;
            if (!(string.IsNullOrEmpty(txt_Snf.Text)))
            {
                snf = Convert.ToDouble(txt_Snf.Text);
            }
            else
            {
                txt_Snf.Text = "0.0";
            }
            if (!(string.IsNullOrEmpty(txt_Fat.Text)))
            {
                fat = Convert.ToDouble(txt_Fat.Text);
            }
        else
        {
            txt_Fat.Text = "0.0";
        }
            //if ((snf > 0) && (fat > 0))
                txt_clr.Text = Convert.ToString(((snf - 0.36) - (fat * 0.2)) * 4);
                txt_Rate.Focus();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    protected void txt_Rate_TextChanged(object sender, EventArgs e)
    {
        try
        {
          
        double mltr = 0.0;
        double rate = 0.0;
        if (!(string.IsNullOrEmpty(txt_Mltr.Text)))
        {
            mltr = Convert.ToDouble(txt_Mltr.Text);
        }
        else
        {
            txt_Mltr.Text = "0.0";
        }
        if (!(string.IsNullOrEmpty(txt_Rate.Text)))
        {
            rate = Convert.ToDouble(txt_Rate.Text);
        }
        else
        {
            txt_Rate.Text = "0.0";
        }
        double amt = (mltr * rate);
        txt_Amount.Text = amt.ToString("f2");
        txt_Remarks.Focus();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Save_Click1(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
}
