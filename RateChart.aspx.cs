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
using System.Data.SqlTypes;

public partial class RateChart : System.Web.UI.Page
{
    //DBHelper dbaccess = new DBHelper();
    BLLRateChart rateBLL = new BLLRateChart();
    BOLRateChart rateBOL = new BOLRateChart();
    BLLStateMaster stateBLL = new BLLStateMaster();
    BLLuser Bllusers = new BLLuser();
    //SqlConnection con = new SqlConnection(); 


    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DataTable dt1 = new DataTable();
    DataTable dt = new DataTable();
    int tidd;
    string Company_code;
    string plant_Code;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //TextBox1.Text = Request.QueryString["Name"].ToString();    

        //Response.Write("<script type='text/javascript'>window.open('RateChart.aspx','_blank');</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "PopupWindow", "<script language='javascript'>window.open('RateChart.aspx','Title','width=600,height=300')</script>");
        uscMsgBox1.MsgBoxAnswered += MessageAnswered;
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();

                txt_fromdate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
                txt_todate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");

                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                }
                plant_Code = ddl_Plantcode.SelectedItem.Value;
                rd_cow.Checked = true;
                //Calendar1.Visible = false;
                //Calendar2.Visible = false;
                loadstateid();
                txt_fromrangevalue.Text = string.Empty;
                txt_torangevalue.Text = string.Empty;
                txt_rate.Text = string.Empty;
                txt_commissionamount.Text = string.Empty;
                txt_bonusamount.Text = string.Empty;

                txt_chartname.Focus();
                btn_Save.Visible = false;
                panel123.Visible = false;
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

                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                plant_Code = ddl_Plantcode.SelectedItem.Value;
                loadstateid();

                if (dt.Rows.Count > 0)
                {
                    btn_Save.Visible = true;
                }
            }
            else
            {

                Server.Transfer("Error.aspx");
            }
            panel123.Visible = true;

        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(Company_code);
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
    private void loadsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(Company_code, plant_Code);
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
            dr = Bllusers.LoadSinglePlantcode(Company_code, "170");
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

    protected void btnCreateratechart_Click(object sender, EventArgs e)
    {
        if (validate1())
        {

            if (ViewState["CurrentData"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentData"];
                int count = dt.Rows.Count;

                BindGrid(count);
                txt_fromrangevalue.Focus();

            }
            else
            {
                BindGrid(0);
                txt_fromrangevalue.Focus();
            }
            cls();
        }
        else
        {

        }
    }
    private bool validate1()
    {


        if (String.IsNullOrEmpty(txt_fromdate.Text))
        {
            uscMsgBox1.AddMessage("Enter the From Date", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            // LinkButton1.Focus();

            //txt_fromdate.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txt_todate.Text))
        {
            uscMsgBox1.AddMessage("Enter the To Date", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            //LinkButton2.Focus();

            //txt_todate.Focus();
            return false;
        }
        //FROM DATE AND TO DATE CHECK
        //DateTime fromdate;
        //DateTime todate;
        //fromdate = Convert.ToDateTime(txt_fromdate.Text);
        //todate = Convert.ToDateTime(txt_todate.Text);
        DateTime fromdate = DateTime.Parse(txt_fromdate.Text);
        DateTime todate = DateTime.Parse(txt_todate.Text);

        if (todate.Subtract(fromdate).Days < 0)
        {
            uscMsgBox1.AddMessage("please,Check the Todate!Its not less Then From Date", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            //LinkButton2.Focus();
            return false;
        }


        if (dl_charttype.SelectedIndex == 0)
        {
            uscMsgBox1.AddMessage("Choose the Chart type", MessageBoxUsc_Message.enmMessageType.Attention);

            // txt_bonusamount.Text = "";
            dl_charttype.Focus();
            return false;
        }

        if (String.IsNullOrEmpty(txt_chartname.Text))
        {
            uscMsgBox1.AddMessage("Enter the RateChart Name", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            txt_chartname.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txt_minfat.Text))
        {
            uscMsgBox1.AddMessage("Enter the Minimum FAT Value", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            txt_minfat.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txt_minsnf.Text))
        {
            uscMsgBox1.AddMessage("Enter the Minimum SNF Value", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            txt_minsnf.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txt_fromrangevalue.Text))
        {
            uscMsgBox1.AddMessage("Enter the From Range Value", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            txt_fromrangevalue.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txt_torangevalue.Text))
        {
            uscMsgBox1.AddMessage("Enter the To Range Value", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            txt_torangevalue.Focus();
            return false;
        }

        //FROM RANGE VALUE AND TO RANGE VALUE CHECK
        //double fromrange, torange;
        //fromrange = Convert.ToDouble(txt_fromrangevalue.Text);
        //torange = Convert.ToDouble(txt_torangevalue.Text);
        ////if (fromrange < torange)
        //if ((torange - fromrange) <= 0)
        //{
        //    uscMsgBox1.AddMessage("Please! Check To Range Value...(Its Not < or = From Range Value", MessageBoxUsc_Message.enmMessageType.Attention);

        //    //txt_bonusamount.Text = "";
        //    txt_torangevalue.Focus();
        //    return false;
        //}

        if (String.IsNullOrEmpty(txt_rate.Text))
        {
            uscMsgBox1.AddMessage("Enter the Rate", MessageBoxUsc_Message.enmMessageType.Attention);

            //txt_bonusamount.Text = "";
            //txt_bonusamount.Text = "0.0";
            //txt_bonusamount.Text = "0.0";
            txt_rate.Focus();

            return false;
        }
        if (String.IsNullOrEmpty(txt_commissionamount.Text))
        {
            //WebMsgBox.Show("Enter the Commission Amount");
            // txt_bonusamount.Text = "";
            //txt_commissionamount.Focus();
            //return false;
            txt_commissionamount.Text = "0";
        }
        if (String.IsNullOrEmpty(txt_bonusamount.Text))
        {
            //WebMsgBox.Show("Enter the Bonus Amount");
            ////txt_bonusamount.Text = "";
            //txt_bonusamount.Focus();
            //return false;
            txt_bonusamount.Text = "0";
        }
        return true;

    }
    private void SETBO()
    {
        DataRow dr;
        DataTable dt2 = new DataTable();
        dt2 = dt;
        int rowcount = dt2.Rows.Count;

        for (int i = 0; i < rowcount; i++)
        {

            dr = dt2.Rows[i];

            rateBOL.Tid = tidd;

            rateBOL.Charttype = dl_charttype.SelectedItem.Text;
            ////rateBOL.Charttype = dl_charttype.Text;
            if (rd_cow.Checked == true)
            {
                rateBOL.Milknature = "Cow";
            }
            else
            {
                rateBOL.Milknature = "Buffalo";
            }
            rateBOL.Stateid = Convert.ToInt32(ddl_stateid.Text);
            rateBOL.Minfat = Convert.ToDouble(txt_minfat.Text);
            rateBOL.Minsnf = Convert.ToDouble(txt_minsnf.Text);
            rateBOL.Fromdate = Convert.ToDateTime(txt_fromdate.Text);
            rateBOL.Todate = Convert.ToDateTime(txt_todate.Text);
            rateBOL.Companycode = Company_code.ToString().Trim();
            rateBOL.Plantcode = plant_Code.ToString().Trim();

            rateBOL.Chartname = txt_chartname.Text;
            rateBOL.Fromrangevalue = Convert.ToDouble(dr["FromRange"]);
            rateBOL.Torangrvalue = Convert.ToDouble(dr["ToRange"]);
            rateBOL.Rate = Convert.ToDouble(dr["Rate"]);
            rateBOL.Commissionamount = Convert.ToDouble(dr["CommAmt"]);

            rateBOL.Bonusamount = Convert.ToDouble(dr["BonusAmt"]);
            rateBLL.InsertRatechartData(rateBOL);
        }
    }
    private void cls()
    {

        txt_fromrangevalue.Text = string.Empty;
        txt_torangevalue.Text = string.Empty;
        txt_rate.Text = string.Empty;
        txt_commissionamount.Text = string.Empty;
        txt_bonusamount.Text = string.Empty;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {


        //GridView1.DataSource = dt;
        //GridView1.DataBind();
        //int rowcount = 0;       
        //rowcount = dt.Rows.Count;
        if ((!(string.IsNullOrEmpty(txt_minfat.Text))) && (!(string.IsNullOrEmpty(txt_minsnf.Text))))
        {
            if (!(string.IsNullOrEmpty(txt_chartname.Text)))
            {
                dt = (DataTable)ViewState["CurrentData"];
                if (dt != null)
                {

                    SETBO();
                    uscMsgBox1.AddMessage("Do you confirm to save a new RateChart?.", MessageBoxUsc_Message.enmMessageType.Attention, true, true, txt_chartname.Text);

                    txt_chartname.Text = "";
                    txt_minfat.Text = "";
                    txt_minsnf.Text = "";
                    dt.Clear();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    btn_Save.Visible = false;
                }
                else
                {
                    uscMsgBox1.AddMessage("Please,Fill Ratechart Grid Value...", MessageBoxUsc_Message.enmMessageType.Attention);

                }

            }
            else
            {
                uscMsgBox1.AddMessage("Please,Fill Required Value...", MessageBoxUsc_Message.enmMessageType.Attention);

            }
        }

        else
        {
            uscMsgBox1.AddMessage("Please,Fill Required Value...", MessageBoxUsc_Message.enmMessageType.Attention);
        }
    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_chartname.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    private void BindGrid(int rowcount)
    {
        DataTable dt = new DataTable();

        DataRow dr;
        if (rowcount == 0)
        {
            btn_Save.Visible = true;
            //dt.Columns.Add(new System.Data.DataColumn("Chart Name", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("FromRange", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("ToRange", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("CommAmt", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("BonusAmt", typeof(string)));
        }
        if (ViewState["CurrentData"] != null)
        {

            for (int i = 0; i < rowcount + 1; i++)
            {
                dt = (DataTable)ViewState["CurrentData"];
                if (dt.Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr[0] = dt.Rows[0][0].ToString();
                }
            }
            btn_Save.Visible = true;
            dr = dt.NewRow();
            //dr[0] = txt_chartname.Text;
            dr[0] = txt_fromrangevalue.Text;
            dr[1] = txt_torangevalue.Text;
            dr[2] = txt_rate.Text;
            dr[3] = txt_commissionamount.Text;
            dr[4] = txt_bonusamount.Text;
            dt.Rows.Add(dr);
        }
        else
        {
            btn_Save.Visible = true;
            dr = dt.NewRow();
            //dr[0] = txt_chartname.Text;
            dr[0] = txt_fromrangevalue.Text;
            dr[1] = txt_torangevalue.Text;
            dr[2] = txt_rate.Text;
            dr[3] = txt_commissionamount.Text;
            dr[4] = txt_bonusamount.Text;
            dt.Rows.Add(dr);
        }
        if (ViewState["CurrentData"] != null)
        {
            GridView1.DataSource = (DataTable)ViewState["CurrentData"];
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        ViewState["CurrentData"] = dt;
    }
    protected void rd_cow_CheckedChanged(object sender, EventArgs e)
    {
        rd_buffalo.Checked = false;
        rd_cow.Checked = true;
    }
    protected void rd_buffalo_CheckedChanged(object sender, EventArgs e)
    {
        rd_cow.Checked = false;
        rd_buffalo.Checked = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewRateChartViewer.aspx");
    }
    private void loadstateid()//formload
    {
        SqlDataReader dr = null;
        ddl_stateid.Items.Clear();
        dl_statename.Items.Clear();
        dr = stateBLL.getstatemasterdatareader1(Company_code, plant_Code);
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddl_stateid.Items.Add(dr["State_ID"].ToString().Trim());
                dl_statename.Items.Add(dr["State_Name"].ToString().Trim());
                //dl_statename.Items.Add(dr["State_ID"].ToString().Trim() + "_" + dr["State_Name"].ToString().Trim());
            }
        }
        else
        {
            Response.Redirect("StateMaster.aspx");
            //uscMsgBox1.AddMessage("Please Create State Master...", MessageBoxUsc_Message.enmMessageType.Attention);

        }
    }
    protected void ddl_stateid_SelectedIndexChanged(object sender, EventArgs e)
    {
        dl_statename.SelectedIndex = ddl_stateid.SelectedIndex;
    }
    protected void dl_statename_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_stateid.SelectedIndex = dl_statename.SelectedIndex;
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        plant_Code = ddl_Plantcode.SelectedItem.Value;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
