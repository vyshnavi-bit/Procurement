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
public partial class DeductionEntry : System.Web.UI.Page
{

    BOLdeductiondet BOdedu = new BOLdeductiondet();
    BLLdeductiondet BLdedu = new BLLdeductiondet();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    SqlDataReader dr;
    SqlConnection con = new SqlConnection();
    static int savebtn = 0;
    public int rid = 0;
    public string companycode;
    public string plantcode;
    //Admin Check Flag
    public int Falg = 0;
    //Deduction Update
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    string date1, date2, date11, date22;
    public static string Referencedate;
    public static int GridtotalQuantity = 0;
    public string Flagdate = string.Empty;
    int Getdata;
    int Getstatus;
    string fsdate;
    string Tsdate;
    public static int roleid;
    DataTable Paymentdata = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                //txt Enable false
                try
                {
                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = false;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = false;
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    companycode = Session["Company_code"].ToString();
                    plantcode = Session["Plant_Code"].ToString();
                    btn_lock.Visible = false;
                    DateTime dtm = new DateTime();
                    dtm = System.DateTime.Now;
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
                        Session["Plant_Code"] = "170".ToString();
                        plantcode = "170";
                        loadspecialsingleplant();
                    }
                    plantcode = ddl_Plantcode.SelectedItem.Value;
                    Session["Plant_Code1"] = plantcode;
                    Bdate();
                    //txt_FromDate.Text = dtm.ToShortDateString();
                    //txt_ToDate.Text = dtm.ToShortDateString();
                    txt_AgentId.Focus();
                    // txt_AgentId.Attributes.Add("onblur", "javascript:CallMe('" + txt_AgentId.ClientID + "','" + txt_AgentName.ClientID + "')");
                    loadrouteid();
                    rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                    LoadAgentid();
                    txt_AgentId.Text = ddl_AgentId.SelectedItem.Value;
                    lbl_msg.Visible = false;
                    Lbl_Errormsg.Visible = false;
                    loadgrid1();
                    //All save purpose
                    btn_Save.Visible = false;
                    getadminapprovalstatus();
                }
                catch
                {

                }
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
                try
                {
                    rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                    companycode = Session["Company_code"].ToString();
                    plantcode = Session["Plant_Code"].ToString();
                    plantcode = ddl_Plantcode.SelectedItem.Value;
                    txt_AgentId.Text = ddl_AgentId.SelectedItem.Value;
                    //  Session["Plant_Code1"] = plantcode;
                    //txt_AgentId.Attributes.Add("onblur", "javascript:CallMe('" + txt_AgentId.ClientID + "','" + txt_AgentName.ClientID + "')");
                   loadgrid1();
                    lbl_msg.Visible = false;
                    Lbl_Errormsg.Visible = false;
                    //All save purpose
                    btn_Save.Visible = false;
                    getadminapprovalstatus();
                }
                catch
                {

                }
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
            dr = Bllusers.LoadSinglePlantcode(companycode, plantcode);
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
            dr = Bllusers.LoadSinglePlantcode(companycode, "170");
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
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("MM/dd/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("MM/dd/yyyy");
                    Falg = Convert.ToInt32(dr["UpdateStatus"].ToString());
                    if (program.Guser_role >= program.Guser_PermissionId)
                    {
                        Falg = 1;
                        //btn_Save.Visible = true;
                    }
                    else
                    {
                        if (Falg == 0)
                        {
                            //btn_Save.Visible = true;
                        }
                        else
                        {
                           // btn_Save.Visible = false;
                        }
                    }
                }
            }
            else
            {
                //  WebMsgBox.Show("Please Select the Bill_Date");

                lbl_msg.Text = "Please Select the Bill_Date";
                lbl_msg.ForeColor = System.Drawing.Color.Green;
                lbl_msg.Visible = true;
            }
        }
        catch (Exception ex)
        {
        }

    }



    [System.Web.Services.WebMethod]
    public static string GetAgentName(string Aid)
    {


        BLLdeductiondet BLdedu = new BLLdeductiondet();

        string companycode = "1";
        string plantcode = "111";
        string rid = "11";
        string AgentName = string.Empty;

        if (Aid == null || Aid.Length == 0)
            return String.Empty;

        try
        {
            AgentName = BLdedu.GetDeductionAgentname(companycode, plantcode, rid, Aid);

            if (!string.IsNullOrEmpty(AgentName))
            {
                savebtn = 1;
                return AgentName;

            }
            else
            {
                savebtn = 1;
                AgentName = "";
                return AgentName;
            }

        }

        catch (Exception ex)
        {
            return ex.ToString();
        }
        //}

    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        btn_Save.Visible = false;
        txt_AgentName.Text = "";
       
        try
        {
            int saveindicate = 0;
            if (txt_Billadvance.Text == "")
            {
                txt_Billadvance.Text = "0.0";
                saveindicate++;
            }
            if (txt_Medicines.Text == "")
            {
                txt_Medicines.Text = "0.0";
                saveindicate++;
            }

            if (txt_Feed.Text == "")
            {
                txt_Feed.Text = "0.0";
                saveindicate++;
            }

            if (txt_Can.Text == "")
            {
                txt_Can.Text = "0.0";
                saveindicate++;
            }
            if (txt_Recovery.Text == "")
            {
                txt_Recovery.Text = "0.0";
                saveindicate++;
            }
            if (txt_Others.Text == "")
            {
                txt_Others.Text = "0.0";
                saveindicate++;
            }

            ////
            if (saveindicate == 6)
            {
                //  WebMsgBox.Show("please,Fill any of the fields");
                lbl_msg.Text = "please,Fill any of the fields";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
                lbl_msg.Visible = true;


                clr();
            }
            else
            {
                if (savebtn == 0)
                {
                    if (validates())
                    {
                        SETBO();
                        BLdedu.insertloan(BOdedu);
                        //   loadgrid();

                        clr();
                        rbtLstReportItems.SelectedIndex = -1;
                        lbl_msg.Text = "Record Saved SuccessFully";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                        lbl_msg.Visible = true;
                        loadgrid1();
                    }
                }
                else
                {
                    //   WebMsgBox.Show("Check the AgentId");

                    lbl_msg.Text = "Check the AgentId";
                    lbl_msg.ForeColor = System.Drawing.Color.Red;
                    lbl_msg.Visible = true;

                }
            }
        }

        catch (Exception ex)
        {
           // rbtLstReportItems.SelectedIndex = -1;
            WebMsgBox.Show("" + ex.ToString());
        }


    }
    private void SETBO()
    {
        try
        {
            BOdedu.Companycode = int.Parse(companycode);
            BOdedu.Plantcode = int.Parse(plantcode);

            //BOdedu.Frdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //BOdedu.Todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            BOdedu.Frdate = Convert.ToDateTime(txt_FromDate.Text);
            BOdedu.Todate = Convert.ToDateTime(txt_ToDate.Text);

            BOdedu.Routeid = int.Parse(ddl_RouteID.SelectedItem.Value);
            BOdedu.Agentid = int.Parse(txt_AgentId.Text);
            BOdedu.Billadvance = double.Parse(txt_Billadvance.Text);
            BOdedu.Ai = double.Parse(txt_Medicines.Text);
            BOdedu.Feed = double.Parse(txt_Feed.Text);
            BOdedu.Can = double.Parse(txt_Can.Text);
            BOdedu.Recovery = double.Parse(txt_Recovery.Text);
            BOdedu.others = double.Parse(txt_Others.Text);
            DateTime dt1 = new DateTime();
            dt1 = Convert.ToDateTime(txt_FromDate.Text).AddDays(1);
            //dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null).AddDays(1);
            string det = dt1.ToShortDateString();
            BOdedu.DeductionDate = Convert.ToDateTime(det);

            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {
                    BOdedu.Referencedate = System.DateTime.Now.ToString(); 
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "6".Trim())
                {
                    BOdedu.Referencedate = System.DateTime.Now.ToString(); 
                }
                else
                {
                    BOdedu.Referencedate = Flagdate; 
                }

            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show("" + ex.ToString());
        }
    }
    private bool validates()
    {
        if (string.IsNullOrEmpty(txt_FromDate.Text))
        {
            WebMsgBox.Show("Enter the FromDate");
            txt_FromDate.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txt_ToDate.Text))
        {
            WebMsgBox.Show("Enter the ToDate");
            txt_ToDate.Focus();
            return false;
        }
        DateTime FromDate = DateTime.Parse(txt_FromDate.Text);
        DateTime ToDate = DateTime.Parse(txt_ToDate.Text);
        //DateTime FromDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        //DateTime ToDate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        if (ToDate.Subtract(FromDate).Days < 0)
        {
            WebMsgBox.Show("please,Check the Todate!Its not less Then From Date");
            txt_ToDate.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(txt_AgentId.Text))
        {
            WebMsgBox.Show("Enter the AgentId");
            txt_AgentId.Focus();
            return false;
        }

        return true;

    }
    private void clr()
    {
        txt_AgentId.Text = "";
        txt_AgentName.Text = "";
        txt_Billadvance.Text = "";
        txt_Medicines.Text = "";
        txt_Feed.Text = "";
        txt_Can.Text = "";
        txt_Recovery.Text = "";
        txt_Others.Text = "";

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
            WebMsgBox.Show(ex.ToString());
        }

    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteName.SelectedIndex = ddl_RouteID.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        LoadAgentid();

    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        LoadAgentid();
    }
    public void loadgrid()
    {
        gvProducts.DataBind();

    }
    public void loadgrid1()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string sqlstr = "SELECT tid AS Tid,agent_id,route_id,billadvance,ai,feed,can,recovery,others,convert(varchar,deductiondate,103) as deductiondate FROM deduction_details where plant_code='" + plantcode + "' and deductiondate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "'  order by tid  desc";
            SqlCommand COM = new SqlCommand(sqlstr, conn);
            //   SqlDataReader DR = COM.ExecuteReader();
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
            dt.Rows.Clear();
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
            else
            {
                gvProducts.DataSource = null;
                gvProducts.DataBind();

            }

        }

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private void LoadAgentid()
    {
        try
        {
            dr = null;
            dr = agentBL.GetAgentId(companycode, plantcode, rid);
            ddl_AgentId.Items.Clear();
            ddl_AgentName.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (ddl_AgentId.Items.Count < 1)
                    {
                        ddl_AgentId.Items.Add(dr["Agent_id"].ToString());
                        txt_AgentId.Text = dr["Agent_id"].ToString();
                        ddl_AgentName.Items.Add(dr["Agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
                    }
                    else
                    {
                        ddl_AgentId.Items.Add(dr["Agent_id"].ToString());
                        ddl_AgentName.Items.Add(dr["Agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());
                    }
                }
            }
            else
            {
                txt_AgentId.Text = "";
                //    WebMsgBox.Show("Agent is Not Added");


                lbl_msg.Text = "Agent is Not Added";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
                lbl_msg.Visible = true;

            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void ddl_AgentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_AgentId.SelectedIndex = ddl_AgentName.SelectedIndex;
        txt_AgentId.Text = ddl_AgentId.SelectedItem.Value;
    }
    protected void ddl_AgentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_AgentName.SelectedIndex = ddl_AgentId.SelectedIndex;
        txt_AgentId.Text = ddl_AgentId.SelectedItem.Value;
    }
    protected void txt_AgentName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            plantcode = ddl_Plantcode.SelectedItem.Value;
            Bdate();
            loadrouteid();
            rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
            LoadAgentid();
            Session["Plant_Code1"] = plantcode;
            loadgrid1();
            getadminapprovalstatus();
        }
        catch
        {



        }
    }
    protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProducts.EditIndex = -1;
        //   loadgrid();
        loadgrid1();
    }
    protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvProducts.EditIndex = e.NewEditIndex;
        //  loadgrid();  
        loadgrid1();
    }
    protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //if (program.Guser_role > program.Guser_PermissionId)
        //{
        try
        {
            //int index = gvProducts.EditIndex;
            //GridViewRow row = gvProducts.Rows[index];
            //loadgriddata();
            //int count;
            //count = dt.Rows.Count;
            //dr = dt.Rows[index];

            //rateBOL.Tid = Convert.ToInt32(dr["Table_ID"]);
            //rateBOL.Chartname = dl_RatechartName.SelectedItem.Text;
            //rateBOL.Fromrangevalue = Convert.ToDouble(((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim());
            //rateBOL.Torangrvalue = Convert.ToDouble(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim());
            //rateBOL.Rate = Convert.ToDouble(((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim());
            //rateBOL.Commissionamount = Convert.ToDouble(((TextBox)row.Cells[4].Controls[0]).Text.ToString().Trim());
            //rateBOL.Bonusamount = Convert.ToDouble(((TextBox)row.Cells[5].Controls[0]).Text.ToString().Trim());

            //rateBOL.Plantcode = plant_Code;
            //rateBOL.Companycode = Company_code;

            //rateBLL.EditUpdateRatechart(rateBOL);
            //GridView1.EditIndex = -1;
            //loadgrid(dl_RatechartName.Text);


            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            int userid = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)gvProducts.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("tid");
            //  TextBox tid = (TextBox)row.Cells[0].Controls[0];
            //      TextBox agentid = (TextBox)row.Cells[1].Controls[0];
            //    TextBox routeid = (TextBox)row.Cells[2].Controls[0];
            TextBox billadvnace = (TextBox)row.Cells[3].Controls[0];
            TextBox ai = (TextBox)row.Cells[4].Controls[0];
            TextBox feed = (TextBox)row.Cells[5].Controls[0];
            TextBox can2 = (TextBox)row.Cells[6].Controls[0];
            TextBox recover = (TextBox)row.Cells[7].Controls[0];
            TextBox others = (TextBox)row.Cells[8].Controls[0];



            //     TextBox deductdate = (TextBox)row.Cells[9].Controls[0];
            gvProducts.EditIndex = -1;
            conn.Open();

            //TextBox Name = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCustName");

            //SqlCommand cmd = new SqlCommand("update deduction_details set billadvance='" + billadvnace.Text + "',ai='" + ai.Text + "',feed='" + feed.Text + "',can='" + can2.Text + "',recovery='" + recover.Text + "',others='" + others.Text + "' where tid='" + userid + "'", conn);
            //cmd.ExecuteNonQuery();
            //  WebMsgBox.Show("inserted Successfully");
            //lbl_msg.Text = "Updated Successfully";
            //lbl_msg.ForeColor = System.Drawing.Color.Green;
            //lbl_msg.Visible = true;


            //    gvProducts.EditIndex = -1;

            //    loadgrid();
            loadgrid1();





        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
        //}
        //else
        //{
        //    WebMsgBox.Show("Permission Not Available...");


        //}
    }
    protected void gvProducts_Sorted(object sender, EventArgs e)
    {

    }
    protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (IsPostBack == true)
        {
            gvProducts.PageIndex = e.NewPageIndex;
            loadgrid1();
            //GRIDVIEWCODE();
            //totsessions();
        }
    }

    //Deduction Update

    protected void rbtLstReportItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string pcode = ddl_Plantcode.SelectedItem.Value;
            string Acode = ddl_AgentId.SelectedItem.Value;
            btn_Save.Visible = false;
            //
            txt_CategoryAvail.Text = "";
            txt_rate.Text = "";

            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "4".Trim())
                {
                    Loadstockcategorytype(Lbl_selectedReportItem.Text.Trim());

                    this.ModalPopupExtender1.Show();

                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = false;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = false;

                }
                else if (Lbl_selectedReportItem.Text.Trim() == "3".Trim())
                {
                    Loadstockcategorytype(Lbl_selectedReportItem.Text.Trim());

                    this.ModalPopupExtender1.Show();

                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = false;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = false;

                }
                else if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
                {
                    Loadstockcategorytype(Lbl_selectedReportItem.Text.Trim());
                    this.ModalPopupExtender1.Show();
                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = false;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = false;

                }
                else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {
                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = true;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = false;
                    //
                    txt_CategoryAvail.Text = "";
                    txt_rate.Text = "";
                    btn_Save.Visible = true;
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "5".Trim())
                {
                    Loadstockcategorytype(Lbl_selectedReportItem.Text.Trim());
                    this.ModalPopupExtender1.Show();
                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = false;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = false;

                }
                else if (Lbl_selectedReportItem.Text.Trim() == "6".Trim())
                {
                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = false;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = true;
                    btn_Save.Visible = true;

                }

                else if (Lbl_selectedReportItem.Text.Trim() == "6".Trim())
                {
                    txt_Can.Enabled = false;
                    txt_Medicines.Enabled = false;
                    txt_Feed.Enabled = false;
                    txt_Billadvance.Enabled = false;
                    txt_Recovery.Enabled = false;
                    txt_Others.Enabled = true;
                    btn_Save.Visible = true;
                }
                else
                {
                }
            }
        }
        catch
        {


        }

    }

    private void Loadstockcategorytype(string StockGroupId)
    {
        try
        {

            ds = null;
            ds = BllPlant.Loadstockcategorytype(StockGroupId.ToString());
            if (ds != null)
            {
                ddl_CategoryType.DataSource = ds;
                ddl_CategoryType.DataTextField = "SID";
                ddl_CategoryType.DataValueField = "SID";//ROUTE_ID 
                ddl_CategoryType.DataBind();
                GetCategoryTypeSelectedchange();
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCreateratechart_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate1())
            {
                if (ViewState["CurrentData"] != null)
                {
                    // GridQuuantity+Allotquantity=TotalQuantity EX:2+1=3
                    //And also check the same reference number items in detailmaster tables

                    DataTable dt1 = (DataTable)ViewState["CurrentData"];
                    dt1 = (DataTable)ViewState["CurrentData"];
                    int Totgridquantity = dt1.AsEnumerable().Sum(row => row.Field<int>("Quanty"));
                    int AvailcatQuantity = Convert.ToInt32(txt_CategoryAvail.Text);
                    int entercatQuantity = Convert.ToInt32(txt_Quantity.Text);
                    int TotcatbalanceQuantity = (AvailcatQuantity - Totgridquantity);

                    if (entercatQuantity <= TotcatbalanceQuantity)
                    {
                        //
                        if (ViewState["CurrentData"] != null)
                        {
                            DataTable dt = (DataTable)ViewState["CurrentData"];
                            int count = dt.Rows.Count;

                            BindGrid(count);
                            ddl_CategoryType.Focus();

                        }
                        else
                        {
                            BindGrid(0);
                            ddl_CategoryType.Focus();
                        }
                        //
                    }
                    else
                    {
                        Lbl_Errormsg.Visible = true;
                        Lbl_Errormsg.Text = "Pending, Check Already Alloted Quantity...";
                    }
                }
                else
                {

                    //
                    if (ViewState["CurrentData"] != null)
                    {
                        DataTable dt = (DataTable)ViewState["CurrentData"];
                        int count = dt.Rows.Count;
                        BindGrid(count);
                        ddl_CategoryType.Focus();
                    }
                    else
                    {
                        BindGrid(0);
                        ddl_CategoryType.Focus();
                    }
                    //             

                }

                cls();
                LoadCategoryList();
                GetCategoryTypeSelectedchange();
                this.ModalPopupExtender1.Show();
            }
            else
            {
                Lbl_Errormsg.Text = "Please,Check the Datas...";
                this.ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            this.ModalPopupExtender1.Show();
        }
    }
    private void LoadCategoryList()
    {
    }

    private void cls()
    {
        //txt_CategoryAvail.Text = string.Empty;
        //txt_rate.Text = string.Empty;
        txt_Quantity.Text = string.Empty;
        txt_Amount.Text = string.Empty;

    }

    private bool validate1()
    {
        if (String.IsNullOrEmpty(txt_CategoryAvail.Text))
        {
            //WebMsgBox.Show("Enter the Bonus Amount");
            ////txt_bonusamount.Text = "";
            //txt_bonusamount.Focus();
            //return false;
            txt_CategoryAvail.Text = "0";
            return false;
        }
        if (String.IsNullOrEmpty(txt_rate.Text))
        {
            //WebMsgBox.Show("Enter the Bonus Amount");
            ////txt_bonusamount.Text = "";
            //txt_bonusamount.Focus();
            //return false;
            txt_rate.Text = "0";
            return false;
        }
        if (String.IsNullOrEmpty(txt_Quantity.Text))
        {
            //WebMsgBox.Show("Enter the Bonus Amount");
            ////txt_bonusamount.Text = "";
            //txt_bonusamount.Focus();
            //return false;
            txt_Quantity.Text = "0";
            return false;
        }
        if (String.IsNullOrEmpty(txt_Amount.Text))
        {
            //WebMsgBox.Show("Enter the Bonus Amount");
            ////txt_bonusamount.Text = "";
            //txt_bonusamount.Focus();
            //return false;
            txt_Amount.Text = "0";
            return false;
        }

        double res = 0.0;
        Lbl_Errormsg.Text = "";
        int Availquantity, Enterquantity = 0;
        if (String.IsNullOrEmpty(txt_CategoryAvail.Text) && String.IsNullOrEmpty(txt_Quantity.Text))
        {
            txt_CategoryAvail.Text = "0";
            txt_Quantity.Text = "0";
            return false;
        }      

     
        return true;
    }

    private void BindGrid(int rowcount)
    {
        DataTable dt = new DataTable();

        DataRow dr;
        if (rowcount == 0)
        {
           // btn_Save.Visible = true;
            dt.Columns.Add(new System.Data.DataColumn("Type", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("Quanty", typeof(int)));
            dt.Columns.Add(new System.Data.DataColumn("rate", typeof(decimal)));
            dt.Columns.Add(new System.Data.DataColumn("Amount", typeof(decimal)));

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
          //  btn_Save.Visible = true;
            dr = dt.NewRow();
            dr[0] = ddl_CategoryType.SelectedItem.Text;
            dr[1] = Convert.ToInt32(txt_Quantity.Text);
            dr[2] = Convert.ToDouble(txt_rate.Text);
            dr[3] = Convert.ToDouble(txt_Amount.Text);

            dt.Rows.Add(dr);


        }
        else
        {
            //btn_Save.Visible = true;
            dr = dt.NewRow();
            dr[0] = ddl_CategoryType.SelectedItem.Text;
            dr[1] = Convert.ToInt32(txt_Quantity.Text); 
            dr[2] = Convert.ToDouble(txt_rate.Text);
            dr[3] = Convert.ToDouble(txt_Amount.Text);
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

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        try
        {
           ViewState["CurrentData"] = null;
            dt.Rows.Clear();
            dt.Clear();
            GridView1.DataSource = (DataTable)ViewState["CurrentData"];
            GridView1.DataBind();
            this.ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            dt.Clear();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            this.ModalPopupExtender1.Show();

        }
    }

    protected void btn_ModularSave_Click(object sender, EventArgs e)
    {
        try
        {
            Referencedate = System.DateTime.Now.ToString();
            Flagdate = Referencedate;
           // BOdedu.Referencedate = Referencedate;
            date1 = txt_FromDate.Text;
            date2 = txt_ToDate.Text;
            
            //date11 = DateTime.ParseExact(date1, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            //date22 = DateTime.ParseExact(date2, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            dt = (DataTable)ViewState["CurrentData"];           

            //this.ModalPopupExtender1.Hide();
         
            if (dt != null)
            {
                if (rbtLstReportItems.SelectedItem != null)
                {
                    Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;

                    if (Lbl_selectedReportItem.Text == "4")
                    {
                        decimal TotCanAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        txt_Can.Text = TotCanAmount.ToString();
                    }
                    else if (Lbl_selectedReportItem.Text == "3")
                    {
                        //Madicine=Maderial
                        decimal TotMedicinesAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        txt_Medicines.Text = TotMedicinesAmount.ToString();
                    }
                    else if (Lbl_selectedReportItem.Text == "1")
                    {
                        decimal TotFeedAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        txt_Feed.Text = TotFeedAmount.ToString();
                    }
                    else if (Lbl_selectedReportItem.Text == "2")
                    {
                        decimal TotBilladvAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        txt_Billadvance.Text = TotBilladvAmount.ToString();
                    }
                    else if (Lbl_selectedReportItem.Text == "5")
                    {
                        //Recovery=Dpu
                        decimal TotRecovDPUAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        txt_Recovery.Text = TotRecovDPUAmount.ToString();
                    }
                    else if (Lbl_selectedReportItem.Text == "6")
                    {
                        decimal TotOthersAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                        txt_Others.Text = TotOthersAmount.ToString();
                    }
                    else 
                    {
                    }
                }

                // Order by using Storeprocedure
                DataTable custOrderDT = new DataTable();
                custOrderDT = dt;
                SqlParameter param = new SqlParameter();
                param.ParameterName = "StockmasterDT";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = custOrderDT;
                param.Direction = ParameterDirection.Input;
                SqlConnection conn = null;
                using (conn = dbaccess.GetConnection())
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Insert_StockMasterDetails]");
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    sqlCmd.Parameters.AddWithValue("@spccode", companycode);
                    sqlCmd.Parameters.AddWithValue("@sppcode", plantcode);
                    sqlCmd.Parameters.AddWithValue("@spfrmdate", date1.Trim());
                    sqlCmd.Parameters.AddWithValue("@sptodate", date2.Trim());
                    sqlCmd.Parameters.AddWithValue("@spUid",roleid);
                    sqlCmd.Parameters.AddWithValue("@spRefId", Referencedate);
                    sqlCmd.Parameters.AddWithValue("@spAgentId", txt_AgentId.Text);
                    
                   // SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                   // adp.Fill(dt);
                    sqlCmd.ExecuteNonQuery();
                }

                cls();
                dt.Clear();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                btn_ModularSave.Visible = true;
                              
            }
            // btn_save defination Start
            txt_AgentName.Text = "";
           
      
            int saveindicate = 0;
            if (txt_Billadvance.Text == "")
            {
                txt_Billadvance.Text = "0.0";
                saveindicate++;
            }
            if (txt_Medicines.Text == "")
            {
                txt_Medicines.Text = "0.0";
                saveindicate++;
            }

            if (txt_Feed.Text == "")
            {
                txt_Feed.Text = "0.0";
                saveindicate++;
            }

            if (txt_Can.Text == "")
            {
                txt_Can.Text = "0.0";
                saveindicate++;
            }
            if (txt_Recovery.Text == "")
            {
                txt_Recovery.Text = "0.0";
                saveindicate++;
            }
            if (txt_Others.Text == "")
            {
                txt_Others.Text = "0.0";
                saveindicate++;
            }

            ////
            if (saveindicate == 6)
            {
                //  WebMsgBox.Show("please,Fill any of the fields");
                lbl_msg.Text = "please,Fill any of the fields";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
                lbl_msg.Visible = true;


                clr();
            }
            else
            {
                if (savebtn == 0)
                {
                    if (validates())
                    {
                        SETBO();
                        BLdedu.insertloan(BOdedu);
                        //   loadgrid();

                        clr();
                        lbl_msg.Text = "Record Saved SuccessFully";
                        lbl_msg.ForeColor = System.Drawing.Color.Green;
                        lbl_msg.Visible = true;
                        loadgrid1();
                        rbtLstReportItems.SelectedIndex = -1;
                        this.ModalPopupExtender1.Hide();
                    }
                }
                else
                {
                    //   WebMsgBox.Show("Check the AgentId");

                    lbl_msg.Text = "Check the AgentId";
                    lbl_msg.ForeColor = System.Drawing.Color.Red;
                    lbl_msg.Visible = true;
                    
                }
            }
            // btn_save defination End           
        }
        catch (Exception ex)
        {
            cls();
            this.ModalPopupExtender1.Show();

        }
    }
    protected void ddl_CategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetCategoryTypeSelectedchange();
        }
        catch (Exception ex)
        {

        }
    }

    private void GetCategoryTypeSelectedchange()
    {
         try
        {
            txt_CategoryAvail.Text = "";
            txt_rate.Text = "";
            this.ModalPopupExtender1.Show();
            string CategoryType = ddl_CategoryType.SelectedItem.Value;
            string hid = string.Empty;
            string Shid = string.Empty;
            string StockName = string.Empty;
            string[] spilvalarr = new string[3];
            spilvalarr = CategoryType.Split('_');
            hid = spilvalarr[0];
            Shid = spilvalarr[1];
            StockName = spilvalarr[2];
            GetCategoryAvail(hid, Shid);
        }
         catch (Exception ex)
         {

         }

    }


    private void GetCategoryAvail(string Hid, string SHid)
    {
        string CategoryAvail = "0";
        string Avail = string.Empty;
        string Rate = string.Empty;
        string[] spilvalarr = new string[3];
        try
        {
            CategoryAvail = BllPlant.LoadstockcategoryeAvailBalance(plantcode, Hid, SHid);
            spilvalarr = CategoryAvail.Split('_');
            Rate = spilvalarr[0];
            Avail = spilvalarr[1];         
            txt_CategoryAvail.Text = Avail;
            txt_rate.Text = Rate;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btn_Mclose_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Hide();

    }

    protected void txt_Quantity_TextChanged(object sender, EventArgs e)
  {
        double res = 0.0;
        Lbl_Errormsg.Text = "";
        int Availquantity, Enterquantity = 0;
        try
        {           
            Availquantity = Convert.ToInt32(txt_CategoryAvail.Text);
            Enterquantity = Convert.ToInt32(txt_Quantity.Text);
            //Check Avail to Entry Quantity
            if (Availquantity >= Enterquantity)
            {
                res = Convert.ToInt32(txt_Quantity.Text) * Convert.ToDouble(txt_rate.Text);
                txt_Amount.Text = res.ToString("f2");
                btnCreateratechart.Focus();
            }
            else 
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Check Avail Quantity...";
            }
            //


            this.ModalPopupExtender1.Show();
            btnCreateratechart.Focus();
        }
        catch (Exception ex)
        {
            btnCreateratechart.Focus();
        }
    }
    protected void btn_lock_Click(object sender, EventArgs e)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update AdminApproval set DeductionStatus='1' where Plant_code='" + plantcode + "'", conn);
            cmd.ExecuteNonQuery();

            getadminapprovalstatus();
            //string message;
            //message = "Deduction  Approved SuccessFully";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "')};";
            //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

            //  WebMsgBox.Show("inserted Successfully");
            //lbl_msg.Text = "Updated Successfully";
            //lbl_msg.ForeColor = System.Drawing.Color.Green;
            //lbl_msg.Visible = true;
        }
        catch (Exception ex)
        {
            btnCreateratechart.Focus();
        }

    }

    public void getadminapprovalstatus()
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string stt = "Select DeductionStatus,status    from  AdminApproval   where Plant_code='" + plantcode + "'  ";
            SqlCommand cmd = new SqlCommand(stt, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    Getdata = Convert.ToInt32(dr["DeductionStatus"]);
                    Getstatus = Convert.ToInt32(dr["status"]);
                }
                //if (Getdata == 1 && Getstatus == 1)
                //{
                //    btn_lock.Visible = true;
                //}

                //if (Getdata == 0 && Getstatus == 1)
                //{
                //    btn_lock.Visible = true;
                //}
                //if (Getdata == 1 && Getstatus == 2)
                //{
                //    btn_lock.Visible = false;
                //}

                //if (Getdata == 0 && Getstatus == 2)
                //{
                //    btn_lock.Visible = false;
                //}
                if ((Getstatus == 1)  && (roleid > 1))
                {
                    btn_lock.Visible = true;


                    if ((Getdata == 1) && (Getstatus == 1))
                    {

                    //    btn_lock.Visible = true;
                        btn_lock.Enabled = false;
                     

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
            btnCreateratechart.Focus();
        }

    }
    protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        getexistsornot();
        if (Paymentdata.Rows.Count > 0)
        {
            //string mss = "Already Bill Process Done Unable to Delete";
            //Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
        }
        else
        {

            if (roleid == 7)
            {

                Dateconvertion();
                con = dbaccess.GetConnection();
                //string username;
                string username = (string)e.Values[0].ToString();
                string strdel = "delete from Deduction_Details   where Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'   and agent_id='" + username + "'  and frdate='" + fsdate + "'   and todate='" + Tsdate + "'";
                SqlCommand cmd = new SqlCommand(strdel, con);
                cmd.ExecuteNonQuery();
                con = dbaccess.GetConnection();
                string strdel1 = "delete from DeductionDetails_Master   where  Dm_Plantcode='" + ddl_Plantcode.SelectedItem.Value + "'   and  Dm_Agent_Id='" + username + "'  and   Dm_FrmDate='" + fsdate + "'   and Dm_ToDate='" + Tsdate + "'";
                SqlCommand cmd1 = new SqlCommand(strdel1, con);
                cmd1.ExecuteNonQuery();

                //string mss = "Record  Deleted  SuccessFully";
                //Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
                loadgrid1();
            }
        }
    }
    public void deleteagentloan() //delete deductiondetails both dedutiondetails and  deduction detail master
    {

      


    }
    public void Dateconvertion()
    {

        string strr = "";
        DateTime fdate = Convert.ToDateTime(txt_FromDate.Text);
        fsdate = fdate.ToString("MM/dd/yyyy");
        DateTime Tdate = Convert.ToDateTime(txt_ToDate.Text);
        Tsdate = Tdate.ToString("MM/dd/yyyy");


    }
    public void getexistsornot()
    {
       string sttt="";
       Dateconvertion();
       con = dbaccess.GetConnection();
       sttt = "Select      *   from Paymentdata     where Plant_code='"+ddl_Plantcode.SelectedItem.Value+"'  AND Frm_date='"+fsdate+"'   AND To_date='"+Tsdate+"'";
       SqlCommand CMD = new SqlCommand(sttt, con);
       SqlDataAdapter dsk = new SqlDataAdapter(CMD);
       Paymentdata.Rows.Clear();
       dsk.Fill(Paymentdata);

    }

    public void checkdeductuondetails()
    {
      //  string 


    }
}
