using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using System.Net;

public partial class Test : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            
           
        }
    }


    protected void btnInvoke_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable custDT = new DataTable();
        DataColumn col = null;

        col = new DataColumn("CustomerId");
        custDT.Columns.Add(col);
        col = new DataColumn("FirstName");
        custDT.Columns.Add(col);
        col = new DataColumn("LastName");
        custDT.Columns.Add(col);
        col = new DataColumn("Phone");
        custDT.Columns.Add(col);

        for (int i = 0; i < 5; i++)
        {
            DataRow dr = null;
            dr = custDT.NewRow();
            dr[0] = "3" + i.ToString();
            dr[1] = "Mani" + i.ToString(); 
            dr[2] = "maran" + i.ToString(); 
            dr[3] = "8899878767";
            custDT.Rows.Add(dr);
        }
        SqlParameter param = new SqlParameter();
        param.ParameterName = "CustDtl";
        param.SqlDbType = SqlDbType.Structured;
        param.Value = custDT;
        param.Direction = ParameterDirection.Input;
        String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = null;
        using (conn = new SqlConnection(dbConnStr))
        {
            SqlCommand sqlCmd = new SqlCommand("dbo.SaveCustomerDetail");
            conn.Open();
            sqlCmd.Connection = conn;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(param);
            sqlCmd.ExecuteNonQuery();
        }


    }
    protected void MChk_Menu1_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void MChk_Menu2_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void MChk_Menu3_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void MChk_Menu4_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void MChk_Menu5_CheckedChanged(object sender, EventArgs e)
    {

    }
   

    protected void MChk_Menu6_CheckedChanged(object sender, EventArgs e)
    {
        Menu6();
    }
    protected void MChk_Menu7_CheckedChanged(object sender, EventArgs e)
    {
        Menu7();
    }
    protected void MChk_Menu8_CheckedChanged(object sender, EventArgs e)
    {
        Menu8();
    }
    protected void MChk_Menu9_CheckedChanged(object sender, EventArgs e)
    {
        Menu9();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);        
        BindprocurementRemarksData();
    }

    private void BindprocurementRemarksData()
    {
        //try
        //{
        //    ds = Bllproimp.LoadProocurementRemarksDatas("1", "156", "03-30-2014", "AM");
        //    if (ds != null)
        //    {
        //        CheckBoxList1.DataSource = ds;
        //        CheckBoxList1.DataTextField = "SampleId";
        //        CheckBoxList1.DataValueField = "SampleId";
        //        CheckBoxList1.DataBind();

        //        CheckBoxList2.DataSource = ds;
        //        CheckBoxList2.DataTextField = "agent_Id";
        //        CheckBoxList2.DataValueField = "agent_Id";
        //        CheckBoxList2.DataBind();

        //        CheckBoxList3.DataSource = ds;
        //        CheckBoxList3.DataTextField = "milk_Kg";
        //        CheckBoxList3.DataValueField = "milk_Kg";
        //        CheckBoxList3.DataBind();

        //        CheckBoxList4.DataSource = ds;
        //        CheckBoxList4.DataTextField = "fat";
        //        CheckBoxList4.DataValueField = "fat";
        //        CheckBoxList4.DataBind();

        //        CheckBoxList5.DataSource = ds;
        //        CheckBoxList5.DataTextField = "snf";
        //        CheckBoxList5.DataValueField = "snf";
        //        CheckBoxList5.DataBind();

        //        CheckBoxList6.DataSource = ds;
        //        CheckBoxList6.DataTextField = "magent_Id";
        //        CheckBoxList6.DataValueField = "magent_Id";
        //        CheckBoxList6.DataBind();

        //        CheckBoxList7.DataSource = ds;
        //        CheckBoxList7.DataTextField = "mmilk_Kg";
        //        CheckBoxList7.DataValueField = "mmilk_Kg";
        //        CheckBoxList7.DataBind();

        //        CheckBoxList8.DataSource = ds;
        //        CheckBoxList8.DataTextField = "mfat";
        //        CheckBoxList8.DataValueField = "mfat";
        //        CheckBoxList8.DataBind();

        //        CheckBoxList9.DataSource = ds;
        //        CheckBoxList9.DataTextField = "msnf";
        //        CheckBoxList9.DataValueField = "msnf";
        //        CheckBoxList9.DataBind();

        //    }
        //}
        //catch (Exception ex)
        //{

        //}
    }

   
    private void Menu6()
    {
        if (MChk_Menu6.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList6.Items.Count - 1; i++)
            {
                CheckBoxList6.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList6.Items.Count - 1; i++)
            {
                CheckBoxList6.Items[i].Selected = false;
            }
        }
    }
    private void Menu7()
    {
        if (MChk_Menu7.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList7.Items.Count - 1; i++)
            {
                CheckBoxList7.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList7.Items.Count - 1; i++)
            {
                CheckBoxList7.Items[i].Selected = false;
            }
        }
    }
    private void Menu8()
    {
        if (MChk_Menu8.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList8.Items.Count - 1; i++)
            {
                CheckBoxList8.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList8.Items.Count - 1; i++)
            {
                CheckBoxList8.Items[i].Selected = false;
            }
        }
    }
    private void Menu9()
    {
        if (MChk_Menu9.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList9.Items.Count - 1; i++)
            {
                CheckBoxList9.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList9.Items.Count - 1; i++)
            {
                CheckBoxList9.Items[i].Selected = false;
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //int count = 0;
        //dt = Bllproimp.LoadProocurementRemarksDatas1("1", "156", "03-30-2014", "AM");
        //count = dt.Rows.Count;
        //if (count > 0)
        //{
        //    DataTable custDT = new DataTable();
        //    DataColumn col = null;

        //    col = new DataColumn("sap_sampleno");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("agent_Id");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("milk_Kg");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("fat");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("Snf");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("modify_aid");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("modify_Kg");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("modify_fat");
        //    custDT.Columns.Add(col);
        //    col = new DataColumn("modify_snf");
        //    custDT.Columns.Add(col);

        //    for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        //    {
        //        DataRow dr = null;
        //        dr = custDT.NewRow();

        //        if (CheckBoxList1.Items[i].Selected == true)
        //        {
        //            dr[0] = CheckBoxList1.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[0] = CheckBoxList1.Items[i].Value;
        //        }

        //        if (CheckBoxList2.Items[i].Selected == true)
        //        {
        //            dr[1] = CheckBoxList2.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[1] = CheckBoxList2.Items[i].Value;
        //        }

        //        if (CheckBoxList3.Items[i].Selected == true)
        //        {
        //            dr[2] = CheckBoxList3.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[2] = CheckBoxList3.Items[i].Value;
        //        }


        //        if (CheckBoxList4.Items[i].Selected == true)
        //        {
        //            dr[3] = CheckBoxList4.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[3] = CheckBoxList4.Items[i].Value;
        //        }


        //        if (CheckBoxList5.Items[i].Selected == true)
        //        {
        //            dr[4] = CheckBoxList5.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[4] = CheckBoxList5.Items[i].Value;
        //        }


        //        if (CheckBoxList6.Items[i].Selected == true)
        //        {
        //            dr[5] = CheckBoxList6.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[5] = CheckBoxList2.Items[i].Value;
        //        }

        //        if (CheckBoxList7.Items[i].Selected == true)
        //        {
        //            dr[6] = CheckBoxList7.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[6] = CheckBoxList3.Items[i].Value;
        //        }

        //        if (CheckBoxList8.Items[i].Selected == true)
        //        {
        //            dr[7] = CheckBoxList8.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[7] = CheckBoxList4.Items[i].Value;
        //        }


        //        if (CheckBoxList9.Items[i].Selected == true)
        //        {
        //            dr[8] = CheckBoxList9.Items[i].Value;
        //        }
        //        else
        //        {
        //            dr[8] = CheckBoxList5.Items[i].Value;
        //        }

        //        custDT.Rows.Add(dr);
        //    }
        //    SqlParameter param = new SqlParameter();
        //    param.ParameterName = "CustDtl";
        //    param.SqlDbType = SqlDbType.Structured;
        //    param.Value = custDT;
        //    param.Direction = ParameterDirection.Input;
        //    String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        //    SqlConnection conn = null;
        //    using (conn = new SqlConnection(dbConnStr))
        //    {
        //        SqlCommand sqlCmd = new SqlCommand("dbo.[UpdateproDetail]");
        //        conn.Open();
        //        sqlCmd.Connection = conn;
        //        sqlCmd.CommandType = CommandType.StoredProcedure;
        //        sqlCmd.Parameters.Add(param);
        //        sqlCmd.Parameters.AddWithValue("@spccode", "1");
        //        sqlCmd.Parameters.AddWithValue("@sppcode", "156");
        //        sqlCmd.Parameters.AddWithValue("@spdate", "03-30-2014");
        //        sqlCmd.Parameters.AddWithValue("@spsess", "AM");
        //        sqlCmd.ExecuteNonQuery();
        //    }
        //}
    }
    protected void btn_netconnection_Click(object sender, EventArgs e)
    {
       
        WebClient client = new WebClient();
        byte[] datasize = null;
        try
        {
            datasize = client.DownloadData("http://www.google.com");
        }
        catch (Exception ex)
        {
        }
        if (datasize != null && datasize.Length > 0)
            lbltxt.Text = "Internet Connection Available.";
        else
            lbltxt.Text = "Internet Connection Not Available.";
    }
    public static bool IsConnectedToInternet
    {
        get
        {
            try
            {
                HttpWebRequest hwebRequest = (HttpWebRequest)WebRequest.Create("http://www.google.com");
                hwebRequest.Timeout = 10000;
                HttpWebResponse hWebResponse = (HttpWebResponse)hwebRequest.GetResponse();
                if (hWebResponse.StatusCode == HttpStatusCode.OK)
                {                    
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }
    }
}