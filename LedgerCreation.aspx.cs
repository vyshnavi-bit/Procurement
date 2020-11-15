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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
public partial class LedgerCreation : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    string gettingledgername;
  

    string str;
    string[] strarr = new string[10];
   public  string headid = string.Empty;
   public string Groupheadid = string.Empty;
   public int Ledgerid = 0;
    string strsql = string.Empty;
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper DBaccess = new DbHelper();
    DataSet ds = new DataSet();
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {

                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();


                dtm = System.DateTime.Now;
                txt_date.Text = dtm.ToShortDateString();
                //  txt_mandate.Text = dtm.ToShortDateString();
                txt_date.Text = dtm.ToString("dd/MM/yyyy");
                //    txt_mandate.Text = dtm.ToString("dd/MM/yyyy");
                gettid();

                if (program.Guser_role < program.Guser_PermissionId)
                {
                    LoadSinglePlantName();
                }
                else
                {
                    LoadPlantName();
                }
                gridview();
                LoadAccountHead(ccode);
                headid = ddl_HeadName.SelectedItem.Value;
                str = headid;
                strarr = str.Split('_');
                if (strarr[0].ToString() != "00")
                {
                    Groupheadid = strarr[0];
                    headid = strarr[1];
                }
                if (chk_Allplant.Checked == true)
                {
                    ddl_Plantname.Visible = true;
                }
                else
                {
                    ddl_Plantname.Visible = false;
                }
            }

            else
            {              
                pcode = ddl_Plantcode.SelectedItem.Value;               
                gridview();               
                headid = ddl_HeadName.SelectedItem.Value;
                str = headid;
                strarr = str.Split('_');
                if (strarr[0].ToString() != "00")
                {
                    Groupheadid = strarr[0];
                    headid = strarr[1];
                }
            }
        }
        else
        {
            ccode = Session["Company_code"].ToString();
            pcode = ddl_Plantcode.SelectedItem.Value;
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
            
            headid = ddl_HeadName.SelectedItem.Value;
            str = headid;
            strarr = str.Split('_');
            if (strarr[0].ToString() != "00")
            {
                Groupheadid = strarr[0];
                headid = strarr[1];
            }
            //  LoadPlantcode();
            //   ddl_agentcode.Text = getrouteusingagent;
            //   gridview();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if ((ddl_Plantname.Text != string.Empty) && (txt_ledger.Text != string.Empty) && (txt_desc.Text != string.Empty))
        {
            INSERT();
        }
        else
        {
            WebMsgBox.Show("Please Fill Above Empty Fields");
        }
        gridview();
        gettid();
        clear();
    }

    public void gettid()
    {
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            //SqlDataReader dr = null;
            //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            try
            {
                string sqlstr = "SELECT max(Tid) as  Tid  FROM ChillingLedger  ";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                int id = Convert.ToInt16(ds.Tables[0].Rows[0]["tid"]);
                tid.Text = (id + 1).ToString();
            }
            catch
            {
                tid.Text = "1";


            }
        }
    }
    private void LoadPlantName()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    public void gridview()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
            //   dt2 = DateTime.ParseExact(txt_mandate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                //  Plant_code,Plant_Name,convert(varchar,PermissionDate,103) as PermissionDate,convert(varchar,ManualDate,103) as ManualDate,MannualSession,RequsterName,GivererName,ReasonForMannual
                string sqlstr = "SELECT Tid,Plant_code,LedgerName,convert(varchar,Date,103) as Date,Narration  FROM ChillingLedger WHERE plant_code='" + pcode + "'  order by  tid desc";
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
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
    public void INSERT()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
        //   dt2 = DateTime.ParseExact(txt_mandate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        //  string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand();
        Ledgerid = GetcurrentLedgerId(Groupheadid, headid);
        string ledgname = string.Empty;
        string Plantname = string.Empty;
        string PlantCode = string.Empty;
        if (chk_Allplant.Checked == true)
        {
            ledgname = txt_ledger.Text.Trim() + "_" + ddl_Plantname.SelectedItem.Text;
            cmd.CommandText = "INSERT INTO ChillingLedger (Plant_Name,Plant_code,GroupHead_ID,Head_Id,Ledger_Id,LedgerName,Date,Narration) VALUES ('" + ddl_Plantname.SelectedItem.Text + "','" + ddl_Plantname.SelectedItem.Value + "','" + Groupheadid + "','" + headid + "','" + Ledgerid.ToString() + "','" + ledgname.Trim() + "','" + d1.Trim() + "','" + txt_desc.Text + "')";
        }
        else
        {
            Plantname = "150_VYSHNAVI ADMIN";
            PlantCode = "150";
            ledgname = txt_ledger.Text.Trim();
            cmd.CommandText = "INSERT INTO ChillingLedger (Plant_Name,Plant_code,GroupHead_ID,Head_Id,Ledger_Id,LedgerName,Date,Narration) VALUES ('" +Plantname+ "','" + PlantCode + "','" + Groupheadid + "','" + headid + "','" + Ledgerid.ToString() + "','" + ledgname.Trim() + "','" + d1.Trim() + "','" + txt_desc.Text + "')";
        }

       
        cmd.Connection = conn;
        conn.Open();
        cmd.ExecuteNonQuery();
        WebMsgBox.Show("inserted Successfully");
        conn.Close();
        gettid();
    }

    private int GetcurrentLedgerId(string gid,string hid)
    {
        int maxid = 0;
        try
        {
            string sql = "SELECT ISNULL(MAX(Ledger_Id),0) AS Currenthead_id from ChillingLedger Where grouphead_id='" + gid + "' AND head_id='" + hid + "'";
            maxid = DBaccess.ExecuteScalarint(sql);
            maxid = maxid + 1;
            return maxid;
        }
        catch (Exception ex)
        {
            return maxid;
        }
    }


    public void clear()
    {

        //    ddl_Plantname.Text = "--------Select--------";
        //ddl_vouchertype.Text = "--------Select--------";
        //ddl_acctype.Text = "--------Select--------";
        txt_ledger.Text = "";
        txt_desc.Text = "";
        DateTime dt1 = new DateTime();
        //  DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);

        //   dt2 = DateTime.ParseExact(txt_mandate.Text, "dd/MM/yyyy", null);
    }
    protected void txt_ledger_TextChanged(object sender, EventArgs e)
    {

          getledgername();
        if (txt_ledger.Text == gettingledgername)
        {
            WebMsgBox.Show("Already Avail same Ledger Name");
            txt_ledger.Text = "";
            txt_ledger.Focus();
        }
    }

    public void getledgername()
    {

        try
        {
            //   ddl_agentcode.Items.Clear();
            //   string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select * from ChillingLedger   where plant_code='" + pcode + "' and LedgerName='" + txt_ledger.Text + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    // ddl_ccode.Items.Add(dr["company_code"].ToString());

                    gettingledgername = dr["LedgerName"].ToString();


                }
            }


        }

        catch
        {

            WebMsgBox.Show("ERROR");

        }



    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
       // pcode = ddl_Plantcode.SelectedItem.Value;
       // gridview();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("OverHeadEntry.aspx");
    }


    private void LoadAccountHead(string Ccode)
    {
        try
        {
            ds = null;
            ds = GetAccountHead(Ccode.ToString());
            ddl_HeadName.DataSource = ds;
            ddl_HeadName.DataTextField = "Head_Name";
            ddl_HeadName.DataValueField = "Head_Id";
            ddl_HeadName.DataBind();
           
            if (ddl_HeadName.Items.Count > 0)
            {
            }
            else
            {
                ddl_HeadName.Items.Add("--Add AccountHead Name--");
            }
        }
        catch (Exception ex)
        {
        }
    }
    public DataSet GetAccountHead(string ccode)
    {
        DataSet ds = new DataSet();
        ds = null;
        // strsql = "SELECT Head_Id,CONVERT(nvarchar(50),Head_Id)+'_'+ Head_Name AS Head_Name FROM Account_HeadName Order by Head_Id";
        // strsql = "SELECT Head_Id,Head_Name AS Head_Name FROM Account_HeadName Order by Head_Id";
        // strsql = "SELECT ah.Gid AS GroupHead_ID,ah.Head_Id AS Head_Id,ah.Head_Name AS Head_Name FROM (SELECT GroupHead_ID FROM accounts_groupname) AS gh INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id,Head_Name AS Head_Name FROM Account_HeadName)AS ah ON gh.GroupHead_ID=ah.Gid order by ah.Gid,Head_Id";
        strsql = "SELECT CONVERT(Nvarchar(15),ah.Gid)+'_'+CONVERT(Nvarchar(15),ah.Head_Id) AS Head_Id,ah.Head_Name AS Head_Name FROM (SELECT GroupHead_ID FROM accounts_groupname) AS gh INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id,Head_Name AS Head_Name FROM Account_HeadName)AS ah ON gh.GroupHead_ID=ah.Gid order by ah.Gid,Head_Id";
        ds = DBaccess.GetDataset(strsql);
        return ds;
    }
    protected void ddl_HeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //headid = ddl_HeadName.SelectedItem.Value.Trim();
        //str = headid;
        //strarr = str.Split('_');
        //if (strarr[0].ToString() != "00")
        //{
        //    Groupheadid = strarr[0];
        //    headid = strarr[1];

        //}
    }
    protected void chk_Allplant_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_Allplant.Checked == true)
        {
            ddl_Plantname.Visible = true;
        }
        else
        {
            ddl_Plantname.Visible = false;
        }
    }
}