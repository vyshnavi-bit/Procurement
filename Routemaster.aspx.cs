using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

public partial class Routemaster : System.Web.UI.Page
{
    DateTime dtime = new DateTime();
    BOLroutmaster routmasterBO = new BOLroutmaster();
    public string Company_code;
    public string plantcode;
    static string kk;
    public bool currentstatus;
    BOLdeductiondet BOdedu = new BOLdeductiondet();
    BLLdeductiondet BLdedu = new BLLdeductiondet();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    SqlDataReader dr;
    DbHelper db = new DbHelper();
    DataSet DTG = new DataSet();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            uscMsgBox1.MsgBoxAnswered += MessageAnswered;
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                roleid =Convert.ToInt32( Session["Role"].ToString());
                GetRouteId();


                Chk_CurrentStatus.Checked = true;
                if (Chk_CurrentStatus.Checked == true)
                {
                    txt_RouteCurrentstatus.Text = "Active";
                    currentstatus = true;
                }
                else
                {
                    txt_RouteCurrentstatus.Text = "De_active";
                    currentstatus = false;
                }
                dtime = System.DateTime.Now;
                txt_Routeaddeddate.Text = dtime.ToShortDateString();
                if (roleid < 3)
                {
                    loadsingleplant();
                    gridview();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                    gridview();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    plantcode = "170";
                    gridview();
                }
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            //if ((Session["Name"] != null) && (Session["Pass"] != null))
            //{
            //    Company_code = Session["Company_code"].ToString();
            //    if (Chk_CurrentStatus.Checked == true)
            //    {
            //        txt_RouteCurrentstatus.Text = "Active";
            //        currentstatus = true;
            //    }
            //    else
            //    {
            //        txt_RouteCurrentstatus.Text = "De_active";
            //        currentstatus = false;
            //    }
            //   // plantcode = ddl_Plantcode.SelectedItem.Value;
            //    gridview();
            //}
            //else
            //{
            //    Server.Transfer("LoginDefault.aspx");
            //}
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

    protected void Chk_CurrentStatus_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_CurrentStatus.Checked == true)
        {
            txt_RouteCurrentstatus.Text = "Active";
        }
        else
        {
            txt_RouteCurrentstatus.Text = "De_active";
        }
    }
    public void SaveData()
    {
        try
        {
            if (validates())
            {
                string mess = string.Empty;
                SETBO();
                mess = routmasterBL.insertroutmaster(routmasterBO);
                Clr();
                loadgrid();
                GetRouteId();
                uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Success);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void SETBO()
    {
        routmasterBO.routId = int.Parse(txt_Routeid.Text);
        routmasterBO.routName = txt_Routename.Text;
        routmasterBO.totDistance = int.Parse(txt_Routedistance.Text);
        routmasterBO.addedDate = DateTime.Parse(txt_Routeaddeddate.Text);
        bool currentstatus;
        if (Chk_CurrentStatus.Checked == true)
        {
            currentstatus = true;
        }
        else
        {
            currentstatus = false;
        }
        routmasterBO.status = currentstatus;
        Company_code = Session["Company_code"].ToString();
        plantcode = Session["Plant_Code"].ToString();
        routmasterBO.Companyid = int.Parse(Company_code);
        routmasterBO.Plantid = int.Parse(plantcode);
        routmasterBO.Lstatus = false;
        routmasterBO.Phoneno = txt_PhoneNo.Text.Trim();
    }
    private void Clr()
    {
        txt_Routename.Text = "";
        txt_Routedistance.Text = "";
        txt_PhoneNo.Text = "";
    }

    private bool validates()
    {
        if (string.IsNullOrEmpty(txt_Routeid.Text))
        {
            uscMsgBox1.AddMessage("Enter the Route_id", MessageBoxUsc_Message.enmMessageType.Attention);
            txt_Routeid.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txt_Routename.Text))
        {
            uscMsgBox1.AddMessage("Enter the  Route_name", MessageBoxUsc_Message.enmMessageType.Attention);
            txt_Routename.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txt_Routedistance.Text))
        {
            uscMsgBox1.AddMessage("Enter the  Route_Distancee", MessageBoxUsc_Message.enmMessageType.Attention);
            txt_Routedistance.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txt_PhoneNo.Text))
        {
            uscMsgBox1.AddMessage("Enter the Phone_No", MessageBoxUsc_Message.enmMessageType.Attention);
            txt_PhoneNo.Focus();
            return false;
        }
        return true;
    }
    public void messs()
    {
        uscMsgBox1.AddMessage("Insert Successfully", MessageBoxUsc_Message.enmMessageType.Success);

    }
    public static void mess()
    {

        kk = "Saved Successfully";
    }

    [WebMethod]
    public static string InsertData(string name, string add)
    {
        string result = "Insert Data" + name + "address" + add;
        return result;
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        SaveData();     
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Clr();
    }
    private void GetRouteId()
    {
        int srid;
        srid = routmasterBL.getrouteid(Company_code, plantcode);
        txt_Routeid.Text = srid.ToString();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_Routename.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    public void loadgrid()
    {
        gridview();

    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(Company_code, plantcode);
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

    //public void LoadPlantcode()
    //{
    //    try
    //    {
    //        SqlConnection  con = db.GetConnection();
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
  
   
    public void gridview()
    {

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                plantcode = ddl_Plantname.SelectedItem.Value;
                string sqlstr = "SELECT Table_ID,Route_ID, Route_Name,plant_code,Tot_Distance,convert(varchar,Added_Date,103) as Added_Date,Phone_No,Status FROM Route_Master WHERE   plant_code='" + plantcode + "'   ORDER BY Route_ID DESC";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                  GridView1.DataSource = null;
                  GridView1.DataBind();
                }
            }
        }
        catch
        {



        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_Plantcode.SelectedItem.Value = ddl_Plantname.SelectedItem.Value;
        plantcode = ddl_Plantname.SelectedItem.Value;
        gridview();
    }
    protected void ddl_plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }
    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridview();
    }
    
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            TextBox routename = (TextBox)row.Cells[2].Controls[0];
            TextBox supmobile = (TextBox)row.Cells[6].Controls[0];
            TextBox active = (TextBox)row.Cells[7].Controls[0];
            GridView1.EditIndex = -1;
            conn.Open();
            if (roleid > 6)
            {

                SqlCommand cmd = new SqlCommand("update Route_Master  set  Status='" + active.Text + "', route_name='" + routename.Text + "' , Phone_No='" + supmobile.Text + "' where Table_ID='" + userid + "' and plant_code='" + plantcode + "'", conn);
                cmd.ExecuteNonQuery();
                GridView1.EditIndex = -1;
                gridview();
            }
        }
        catch
        {

            WebMsgBox.Show("Please Check updated Data");
        }
        GridView1.EditIndex = -1;
        gridview();


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        gridview();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gridview();
    }
}


