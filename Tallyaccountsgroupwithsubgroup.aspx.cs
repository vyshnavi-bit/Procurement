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

public partial class Tallyaccountsgroupwithsubgroup : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    public string  getsubaccname;
    
    string strsql = string.Empty;
    DbHelper DBaccess = new DbHelper();
    DataSet ds = new DataSet();
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            dtm = System.DateTime.Now;
            txt_date.Text = dtm.ToString("dd/MM/yyyy");
            LoadAccountGroup("1");

        }
        else
        {
           
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            getsubaccountname();
            if ((string.IsNullOrEmpty(getsubaccname)))
            {
                getsubaccname = "123KS";
            }
            
            if (txt_ledger.Text.Trim() == getsubaccname.Trim())
            {
                txt_ledger.Text = "";
                txt_ledger.Focus();
            }
            else
            {
                if ((txt_date.Text != string.Empty) && (ddl_accsubgroup.Text != string.Empty) && (txt_ledger.Text != string.Empty) && (txt_desc.Text != string.Empty))
                {
                    INSERT();
                    clear();
                }
                else
                {
                    WebMsgBox.Show("Please Fill Above Empty Fields");
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
   
 public void clear()
    {

        //    ddl_Plantname.Text = "--------Select--------";
       // ddl_vouchertype.Text = "--------Select--------";
    //    ddl_acctype.Text = "--------Select--------";
        txt_ledger.Text = "";
        txt_desc.Text = "";
        DateTime dt1 = new DateTime();
      //  DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
     //   dt2 = DateTime.ParseExact(txt_mandate.Text, "dd/MM/yyyy", null);
    

    }

 private void LoadAccountGroup(string Ccode)
 {
     try
     {
         ds = null;
         ds = GetAccountGroup(Ccode.ToString());
         ddl_accsubgroup.DataSource = ds;
         ddl_accsubgroup.DataTextField = "GroupHead_Name";
         ddl_accsubgroup.DataValueField = "GroupHead_Id";
         ddl_accsubgroup.DataBind();
         if (ddl_accsubgroup.Items.Count > 0)
         {
         }
         else
         {
             ddl_accsubgroup.Items.Add("--Add AccountHead Name--");
         }
     }
     catch (Exception ex)
     {
     }
 }
 public DataSet GetAccountGroup(string ccode)
 {
     DataSet ds = new DataSet();
     ds = null;
     strsql = "SELECT GroupHead_Id,CONVERT(nvarchar(50),GroupHead_Id)+'_'+ GroupHead_Name AS GroupHead_Name FROM Accounts_GroupName Order by GroupHead_Id";
     ds = DBaccess.GetDataset(strsql);
     return ds;
 }

    public void INSERT()
    {

        DateTime dt1 = new DateTime();     
        dt1 = DateTime.ParseExact(txt_date.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
       
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand();
        int currentheadId = GetcurrentHeadId();
        cmd.CommandText = "INSERT INTO Account_HeadName (head_id,Head_Name,Datee,Descriptions,GroupHead_ID) VALUES ('" + currentheadId + "','" + txt_ledger.Text + "','" + d1.ToString() + "','" + txt_desc.Text + "','" + ddl_accsubgroup.SelectedItem.Value + "')";
      
        cmd.Connection = conn;
        conn.Open();
        cmd.ExecuteNonQuery();
        WebMsgBox.Show("inserted Successfully");
        conn.Close();
       
    }

    private int GetcurrentHeadId()
    {
        int maxid = 0;
        try
        {
            string sql = "SELECT ISNULL(MAX(head_id),0) AS Currenthead_id from Account_HeadName Where grouphead_id='" + ddl_accsubgroup.SelectedItem.Value + "'";
            maxid = DBaccess.ExecuteScalarint(sql);
            maxid = maxid + 1;
            return maxid;
        }
        catch (Exception ex)
        {
            return maxid;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
         Response.Redirect("OverHeadEntry.aspx");
    }
    protected void ddl_accsubgroup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txt_ledger_TextChanged(object sender, EventArgs e)
    {
        
    }


    public void getsubaccountname()
    {      
        try
        {      
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "SELECT  Head_Name from Account_HeadName Where Head_Name ='" + txt_ledger.Text.Trim() + "'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    getsubaccname = dr["Head_Name"].ToString();
                }
            }
           
        }
        catch
        {
            WebMsgBox.Show("ERROR");
        }

    }


}