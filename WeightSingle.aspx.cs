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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;


public partial class WeightSingle : System.Web.UI.Page
{
    string centreid;
    int countuser;
    public static string txtboxagent_id, txtboxagent_name, txtboxweightdata1, txtboxweightdata2;
    double rate = 0.0, fat = 0.0, snf = 0.0, amount = 0.0;
    string sno = "0";
    public static string flag;
    public string portname;
    public string baudrate;
    SqlConnection con = new SqlConnection();
    SqlCommand sqlcmd = new SqlCommand();
    SqlDataReader dr;
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();
    BOLProcurement procurementBO = new BOLProcurement();
    BLLProcurement procurementBL = new BLLProcurement();
    DALProcurement procurementDA = new DALProcurement();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    DbHelper DBclass = new DbHelper();
    BLLRateChart chartmaster = new BLLRateChart();
    BLLPortsetting portBL = new BLLPortsetting();
    DALroutmaster routeDA = new DALroutmaster();
    BLLroutmaster routeBL = new BLLroutmaster();
    //string fname;
    public bool IsPageRefresh;
    string plant_Code;
    public string companycode;
    public string plantcode;
    public int RateChartmode;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {

                uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                //
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                plant_Code = plantcode;
                loadport();
                LoadPlantRouteChartmode();
                //


                //.
                Session["txtAgentId"] = txtAgentId1.Text;

                Session["cmb_session"] = cmb_session.Text;
                DateTime dt = DateTime.Now;
                TextBox1.Text = String.Format("{0:MM/dd/yyyy}", dt);



                string milkkg = txtWeight.Text.Trim();
                txtWeight.Attributes.Add("value", milkkg);
                string milkltr = txt_Milkltr.Text.Trim();
                txt_Milkltr.Attributes.Add("value", milkltr);

                cmb_session.Text = DateTime.Now.ToString("tt");
                //GetID();
                //GetCurrentUserCount();
                loadrouteid();
                loadagentid();
                //loadagentid1();

                if (rd_Cow.Checked == true)
                {
                    txt_milktype.Text = rd_Cow.Text;
                   // LoadPlantRouteChartmode();
                    loadchartname1();
                }
                else
                {
                    txt_milktype.Text = rd_Buffalo.Text;
                   // LoadPlantRouteChartmode();
                    loadchartname1();
                }

                if (auto.Checked == true)
                {
                    Label1.Visible = false;
                    Label2.Visible = false;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    txt_Milkltr.Visible = false;
                    txtWeight.Visible = false;
                }
                else
                {
                    Label1.Visible = true;
                    Label2.Visible = true;
                    Label3.Visible = true;
                    Label4.Visible = true;
                    txt_Milkltr.Visible = true;
                    txtWeight.Visible = true;
                }



                string Weightdata;
                Weightdata = inpHide.Value;
                txtWeight.Text = Weightdata.Trim();
                //if (cmb_ratechart.Text == "--select ratechart--")
                //{
                //    Response.Redirect("RateChart.aspx");
                //}
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }
        else
        {

            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();

                plant_Code = plantcode;
                Session["txtAgentId"] = txtAgentId1.Text;
                loadport();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }

    }
    private void LoadPlantRouteChartmode()
    {
        dr = null;
        dr = chartmaster.PlantRouteChartId(companycode.ToString(), plant_Code.ToString());
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                RateChartmode = Convert.ToInt32(dr["RateChartPlantRoute_ID"]);
            }
        }
        else
        {
            WebMsgBox.Show("Choose RatechartMode[Plantwise or Routewise]");

        }
    }
    public void getsample()
    {
        DbtoLinqDataContext db = new DbtoLinqDataContext();
        var p = from pp in db.GetTable<WeightDemoTable>()
                select pp;
        gvProducts.DataSource = p;
        gvProducts.DataBind();
    }
    private void loadagentid()
    {
        SqlDataReader dr;
        string type = string.Empty;
        if (rd_Cow.Checked == true)
        {
            type = "Cow";
            rd_Buffalo.Checked = false;
        }
        else
        {
            type = "Buffalo";
            rd_Cow.Checked = false;
        }

        dr = agentBL.GetWeightAgentId(type, companycode, plant_Code, Convert.ToInt32(ddl_RouteID.SelectedItem.Value));

        txtAgentId.Items.Clear();
        txtAgentName.Items.Clear();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                txtAgentId.Items.Add(dr["Agent_ID"].ToString().Trim());
                txtAgentName.Items.Add(dr["Agent_ID"].ToString().Trim() + "-" + dr["Agent_Name"].ToString().Trim());
                txt_TruckID.Text = "";
                txt_TruckID.Text = dr["TRuck_id"].ToString().Trim();
            }
        }
        else
        {
            WebMsgBox.Show("Please, Add the Agent");
        }

    }
    private void loadport()
    {
        SqlDataReader dr;
        dr = portBL.getportdatareader(companycode, plant_Code, "Weigher");

        while (dr.Read())
        {
            portname = (dr["Port_Name"].ToString().Trim());
            baudrate = (dr["Baud_Rate"].ToString().Trim());
        }

        if (portname == null)
        {
            Response.Redirect("PortSetting.aspx");
        }


    }
    private void loadagentid1()
    {
        SqlDataReader dr;

        //GetCurrentUserCount();
        //GetID();
        dr = agentBL.getagentmasterdatareader3(ddl_RouteID.SelectedItem.Value);
        //dr = routmasterBL.getroutmasterdatareader1(cmb_stateId.Text);
        //txtAgentId.Items.Clear();
        txtAgentName.Items.Clear();
        while (dr.Read())
        {
            txtAgentId.Items.Add(dr["Agent_ID"].ToString().Trim());
            txtAgentName.Items.Add(dr["Agent_ID"].ToString().Trim() + "-" + dr["Agent_Name"].ToString().Trim());
        }
        //if (txtAgentId.Items.Count >= 1)
        //    txtAgentName.SelectedIndex = 0;
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
    private void loadchartname1()
    {

        //dr = chartmaster.getratechartname1(txt_milktype.Text.Trim(), plant_Code,companycode);
        //cmb_ratechart.Items.Clear();
        //while (dr.Read())
        //{
        //    cmb_ratechart.Items.Add(dr["chart_Name"].ToString().Trim());
        //}
        //if (cmb_ratechart.Items.Count >= 1)
        //    cmb_ratechart.SelectedIndex = 0;

        /////
        cmb_ratechart.Items.Clear();
        if (RateChartmode == 1)
        {
            dr = null;
            if (txt_milktype.Text.Trim() == "Cow")
            {
                dr = chartmaster.RateChartmode(companycode, plant_Code);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmb_ratechart.Items.Add(dr["RateChartId"].ToString().Trim());
                    }
                    int cc = cmb_ratechart.Items.Count;
                    if (cc == 1)
                    {

                    }
                    else
                    {
                        cmb_ratechart.Items.Add("--select ratechart--");
                        WebMsgBox.Show("Cow RateChart is Not Select");
                    }
                }

            }
            else
            {
                dr = chartmaster.RateChartmode(companycode, plant_Code);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmb_ratechart.Items.Add(dr["RateChartIdBuff"].ToString().Trim());
                    }
                    int cc = cmb_ratechart.Items.Count;
                    if (cc == 1)
                    {

                    }
                    else
                    {
                        cmb_ratechart.Items.Add("--select ratechart--");
                        WebMsgBox.Show("Buffalo RateChart is Not Select");
                    }
                }


            }
        }
        if (RateChartmode == 2)
        {
            dr = null;
            if (txt_milktype.Text.Trim() == "Buffalo")
            {
                dr = chartmaster.RateChartmodeBuff(companycode, plant_Code, ddl_RouteID.Text.Trim());
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmb_ratechart.Items.Add(dr["Ratechart_IdBuff"].ToString().Trim());
                    }
                }
                int cc = cmb_ratechart.Items.Count;
                if (cc == 1)
                {

                }
                else
                {
                    cmb_ratechart.Items.Add("--select ratechart--");
                    WebMsgBox.Show("Cow RateChart is Not Select");
                }

            }
            else
            {
                dr = chartmaster.RateChartmodeBuff(companycode, plant_Code, ddl_RouteID.Text.Trim());
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmb_ratechart.Items.Add(dr["Ratechart_Id"].ToString().Trim());
                    }
                }
                int cc = cmb_ratechart.Items.Count;
                if (cc == 1)
                {

                }
                else
                {
                    cmb_ratechart.Items.Add("--select ratechart--");
                    WebMsgBox.Show("Buffalo RateChart is Not Select");
                }

            }


        }
        if (cmb_ratechart.Items.Count == 0)
        {
            cmb_ratechart.Items.Add("--select ratechart--");
        }

    }
    public int UserStatus()
    {
        string weight = txtWeight.Text;
        string ltr = txt_Milkltr.Text;

        if ((txtWeight.Text.Trim() == "") && (txt_Milkltr.Text.Trim() == ""))
        {


            return 1;


        }


        else
        {

            return 0;




        }

    }
    private void loadgrid()
    {
        ////string sqlstr = "SELECT * FROM WeightDemoTable";
        //string sqlstr = "SELECT  WeightDemoTable.Agent_Name AS NAME, WeightDemoTable.milk_Ltr AS MILKLTR, WeightDemoTable.milk_Kg AS MILKKG,WeightDemoTable.fat AS FAT, WeightDemoTable.snf AS SNF FROM WeightDemoTable Where Centre_ID='" + Session["User_ID"] + "' and Session='" + cmb_session.Text + "' and WDate='" + TextBox1.Text + "'";
        //DataTable dt = new DataTable();
        //dt = procurementDA.GetDatatable(sqlstr);
        //gvProducts.DataSource = dt;
        //gvProducts.DataBind();

    }
    private void loadgrid1()
    {

        //string sqlstr = "SELECT  * from WeightDemoTable";
        //DataTable dt = new DataTable();
        //dt = procurementDA.GetDatatable(sqlstr);
        //gvProducts.DataSource = dt;
        //gvProducts.DataBind();

    }
    private void setBO()
    {

        procurementBO.AgentID = int.Parse(txtAgentId.Text);
        DateTime wdt = new DateTime();
        wdt = DateTime.Parse(TextBox1.Text);
        if (cmb_session.Text == "AM")
        {
            //DATE1
            procurementBO.WDate = DateTime.Parse(wdt.ToShortDateString());
        }
        else
        {
            procurementBO.WDate = DateTime.Parse(wdt.ToShortDateString());
        }
        procurementBO.Session = cmb_session.Text;
        procurementBO.milk_Ltr = double.Parse(txt_Milkltr.Text.Trim());
        // procurementBO.CentreID = Session["User_ID"].ToString();
        procurementBO.CentreID = plantcode;
        string routeid = ddl_RouteID.SelectedItem.Value;
        procurementBO.Route_ID = int.Parse(routeid);
        procurementBO.no_Of_Cans = Convert.ToInt32(txt_Noofcans.Text);
        procurementBO.Milk_Kg = double.Parse(txtWeight.Text.Trim());
        procurementBO.Company_ID = companycode;
        //


        //

        procurementBO.rate_Chart = cmb_ratechart.Text;
        if (txt_milktype.Text == "Cow")
        {
            procurementBO.milk_Nature = "1";

        }
        else
        {
            procurementBO.milk_Nature = "2";
        }
        procurementBO.sample_No = getsampleno();
        procurementBO.s_No = int.Parse(sno);
        //SET THIS VALUE 
        string tipstart = string.Format("{0:HH:mm:ss tt}", wdt);
        procurementBO.Milktipstarttime = DateTime.Parse(tipstart);
        //DateTime.Now.ToString("HH:mm:ss tt")
        //string tipend = string.Format("{0:HH:mm:ss tt}", DateTime.Now);
        string tipend = DateTime.Now.ToString("HH:mm:ss tt");
        procurementBO.Milktipendtime = DateTime.Parse(tipend);
        procurementBO.Weigheruser = Convert.ToInt32(plantcode);
        int a = 1;
        procurementBO.STATUS = a;

        procurementBO.Truck_id = Convert.ToInt32(txt_TruckID.Text.Trim());



        procurementBO.fat = 0.0;
        procurementBO.snf = 0.0;
        procurementBO.rate = 0.0;
        procurementBO.amount = 0.0;
        procurementBO.Clr = 0.0;
        procurementBO.FatKg = 0.0;
        procurementBO.SnfKg = 0.0;
        procurementBO.Analyzeruser = 0;
        procurementBO.Netrate = 0.0;


    }
    private string getsampleno()
    {
        string dt, mon, yr, sess, retstr;
        dt = System.DateTime.Now.ToString("dd");
        mon = System.DateTime.Now.ToString("MM");
        yr = System.DateTime.Now.ToString("yy");
        sess = System.DateTime.Now.ToString("tt");

        sno = procurementBL.getsampleno(TextBox1.Text, cmb_session.Text, companycode, plant_Code);
        retstr = dt + mon + yr + sess + sno;
        return retstr;
    }


    public static String[] split(string s, string separator)
    {
        string j;
        List<string> list = new List<string>();
        int currindex = 0;

        for (bool more = true; more; )
        {
            int nextindex = s.IndexOf(separator, currindex);

            if (nextindex == -1)
            {
                nextindex = s.Length;
                more = false;
            }

            if (currindex >= 1 && currindex <= 5000)
            {
                j = (s.Substring(currindex, nextindex - (currindex)));
                if (!string.IsNullOrEmpty(j))
                {
                    list.Add(s.Substring(currindex, nextindex - (currindex)));
                }
                currindex = nextindex + separator.Length;
            }
            else
            {
                currindex = nextindex + separator.Length;
            }
        }

        return (String[])list.ToArray();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        //string filename = txtAgentId.Text + TextBox1.Text + cmb_session.Text;
        //string sqlstr = "select WeightDemoTable.Agent_Name AS AgentID_Name,WeightDemoTable.milk_Kg AS Milk_Kg,WeightDemoTable.milk_Ltr AS Milk_Ltr,WeightDemoTable.fat AS Fat,WeightDemoTable.snf As Snf,WeightDemoTable.clr AS Clr,WeightDemoTable.rate AS Rate,WeightDemoTable.amount AS Amount,WeightDemoTable.net_amount AS Net_Amount from WeightDemoTable Where WeightDemoTable.WDate='" + TextBox1.Text + "' and WeightDemoTable.Session='" + cmb_session.Text+ "'";
        //DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        //dt = procurementDA.GetDatatable(sqlstr);
        //ds.Tables.Add(dt);
        //CreatePdf pd = new CreatePdf(ds, filename);
        //TextBox1.Text = string.Empty;
        //TextBox1.Text = " ";
        //pd.Execute();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void rd_Cow_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_Cow.Checked == true)
        {
            rd_Buffalo.Checked = false;
            txt_milktype.Text = " ";
            txt_milktype.Text = rd_Cow.Text;
            LoadPlantRouteChartmode();
            loadchartname1();
            loadagentid();
        }
        else
        {
            txt_milktype.Text = " ";
            txt_milktype.Text = rd_Buffalo.Text;
            LoadPlantRouteChartmode();
            loadchartname1();
            loadagentid();
        }
    }
    protected void rd_Buffalo_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_Buffalo.Checked == true)
        {
            rd_Cow.Checked = false;
            txt_milktype.Text = " ";
            txt_milktype.Text = rd_Buffalo.Text;
            LoadPlantRouteChartmode();
            loadchartname1();
            loadagentid();
        }
        else
        {
            txt_milktype.Text = " ";
            txt_milktype.Text = rd_Cow.Text;
            LoadPlantRouteChartmode();
            loadchartname1();
            loadagentid();
        }
    }

    protected void txtAgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAgentId.SelectedIndex = txtAgentName.SelectedIndex;
        //if (!(string.IsNullOrEmpty(txt_agentId.Text)))
        //{
        //    tipsttime = System.DateTime.Now.ToString("h:mm:ss");
        //    loadstateid(txt_agentId.Text, cmb_RoutId.Text);
        //    DisplaysampleNO();

        //    if (chk_Manual.Checked == false)
        //    {
        //        openport();
        //    }
        //    rtb_milkkg.Text = string.Empty;
        //}
    }


    protected void txtAgentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //loadagentid1();
        txtAgentName.SelectedIndex = txtAgentId.SelectedIndex;
    }
    protected void BtnLock_Click(object sender, EventArgs e)
    {
        if (auto.Checked == true)
        {
            string Weightdata;
            Weightdata = inpHide.Value;
            txtWeight.Text = Weightdata;
            string tmp = string.Empty;
            string[] strr = new string[25];
            if (!(string.IsNullOrEmpty(Weightdata)))
            {

                tmp = txtWeight.Text;
                tmp = tmp.Replace("+", " ");
                tmp = tmp.Replace("Kg", " ");
                strr = tmp.Split('\n');

                txtWeight.Text = strr[0].ToString();
                try
                {

                    string decimalno;
                    decimalno = "1234567890.";
                    string temp, temp1;
                    temp1 = string.Empty;
                    temp = txtWeight.Text;
                    foreach (char ch in temp)
                    {
                        if ((decimalno.IndexOf(ch) != -1))
                        {
                            temp1 += ch;
                        }
                    }

                    if (!(string.IsNullOrEmpty(temp1)))
                    {
                        double kg, ltr;
                        kg = 0.00;
                        ltr = 0.00;
                        kg = Convert.ToDouble(temp1);
                        ltr = kg / 1.03;
                        txt_Milkltr.Text = ltr.ToString("f2");
                    }

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

            }
        }
        else if ((manual.Checked == true) && (txtWeight.Text != string.Empty))
        {

            //txtWeight.Text = Weightdata;
            string tmp = string.Empty;
            string[] strr = new string[25];
            if (!(string.IsNullOrEmpty(txtWeight.Text.Trim())))
            {

                tmp = txtWeight.Text;
                tmp = tmp.Replace("+", " ");
                tmp = tmp.Replace("Kg", " ");
                strr = tmp.Split('\n');

                txtWeight.Text = strr[0].ToString();
                try
                {

                    string decimalno;
                    decimalno = "1234567890.";
                    string temp, temp1;
                    temp1 = string.Empty;
                    temp = txtWeight.Text;
                    foreach (char ch in temp)
                    {
                        if ((decimalno.IndexOf(ch) != -1))
                        {
                            temp1 += ch;
                        }
                    }

                    if (!(string.IsNullOrEmpty(temp1)))
                    {
                        double kg, ltr;
                        kg = 0.00;
                        ltr = 0.00;
                        kg = Convert.ToDouble(temp1);
                        ltr = kg / 1.03;

                        txt_Milkltr.Text = ltr.ToString("f2");
                    }

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

            }
        }
        else if ((manual.Checked == true) && (txtWeight.Text == string.Empty) && (txt_Milkltr.Text != string.Empty))
        {
            string ss = txt_Milkltr.Text;
            txt_Milkltr.Text = ss.ToString();
            txtWeight.Text = "0";
        }
        int result = UserStatus();
        if ((result == 1) && (auto.Checked == true))
        {
            uscMsgBox1.AddMessage("Please Lock Weight", MessageBoxUsc_Message.enmMessageType.Attention);



        }
        else if ((result == 1) && (manual.Checked == true) && (txtWeight.Text == ""))
        {
            uscMsgBox1.AddMessage("Please Lock Weight", MessageBoxUsc_Message.enmMessageType.Attention);
        }
        else if ((result == 1) && (manual.Checked == true) && (txt_Milkltr.Text == ""))
        {
            uscMsgBox1.AddMessage("Please Lock Weight", MessageBoxUsc_Message.enmMessageType.Attention);
        }
        else if ((cmb_ratechart.Text == "--select ratechart--"))
        {
            uscMsgBox1.AddMessage("Choose RatechartMode[Plantwise or Routewise]", MessageBoxUsc_Message.enmMessageType.Attention);
        }
        else if ((txt_TruckID.Text == ""))
        {
            uscMsgBox1.AddMessage("Route[ " + ddl_RouteID.Text + " ]has No Truck_id]", MessageBoxUsc_Message.enmMessageType.Attention);
        }
        else if (result == 0)
        {

            Session["User_LoginId"] = centreid;
            //uscMsgBox1.AddMessage("Do you confirm to save a Record?.", MessageBoxUsc_Message.enmMessageType.Attention, true, true, txtWeight.Text);

            setBO();

            procurementBL.insertprocurement(procurementBO);
            //loadagentid();
            //loadagentid1();
            gvProducts.DataBind();
            txtWeight.Text = "";
            txt_Milkltr.Text = "";
            txtAgentId1.Text = "";
            txtAgentName1.Text = "";
            txt_Noofcans.Text = "";

        }
    }




    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txtWeight.Text + "Kg and " + txt_Milkltr.Text + " Ltr as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        //string filename = txtAgentId.Text + TextBox1.Text + cmb_session.Text;
        //string sqlstr = "select WeightDemoTable.Agent_Name AS AgentID_Name,WeightDemoTable.milk_Kg AS Milk_Kg,WeightDemoTable.milk_Ltr AS Milk_Ltr,WeightDemoTable.fat AS Fat,WeightDemoTable.snf As Snf,WeightDemoTable.clr AS Clr,WeightDemoTable.rate AS Rate,WeightDemoTable.amount AS Amount,WeightDemoTable.net_amount AS Net_Amount from WeightDemoTable Where WeightDemoTable.WDate='" + TextBox1.Text + "' and WeightDemoTable.Session='" + cmb_session.Text + "'";
        //DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        //dt = procurementDA.GetDatatable(sqlstr);
        //ds.Tables.Add(dt);
        //CreatePdf pd = new CreatePdf(ds, filename);
        //TextBox1.Text = string.Empty;
        //TextBox1.Text = " ";
        //pd.Execute();
    }
    protected void GridView1_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvProducts.PageIndex = e.NewPageIndex;
        gvProducts.DataBind();
    }
    protected void auto_CheckedChanged(object sender, EventArgs e)
    {
        Label1.Visible = false;
        Label2.Visible = false;
        Label3.Visible = false;
        Label4.Visible = false;
        txt_Milkltr.Visible = false;
        txtWeight.Visible = false;
        manual.Checked = false;

    }
    protected void manual_CheckedChanged(object sender, EventArgs e)
    {
        Label1.Visible = true;
        Label2.Visible = true;
        Label3.Visible = true;
        Label4.Visible = true;
        txt_Milkltr.Visible = true;
        txtWeight.Visible = true;
        txt_Milkltr.Enabled = true;
        txtWeight.Enabled = true;
        auto.Checked = false;

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private void loadrouteid()
    {
        SqlDataReader dr;

        dr = routeBL.getroutmasterdatareader(companycode, plantcode);
        ddl_RouteID.Items.Clear();
        txtroutename.Items.Clear();

        while (dr.Read())
        {
            txtroutename.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
            ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());

        }

    }

    protected void txtroutename_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = txtroutename.SelectedIndex;
        LoadPlantRouteChartmode();
        loadchartname1();
        loadagentid();
    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtroutename.SelectedIndex = ddl_RouteID.SelectedIndex;
    }

    protected void Button2_Click2(object sender, EventArgs e)
    {

    }



   

}
