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
using System.IO;

public partial class ProducerProcurementImport : System.Web.UI.Page
{
    public string companycode,plantcode;
    public string frmdate = string.Empty;
    DateTime dat = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BOLRateChart rateBO = new BOLRateChart();
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {

            roleid = Convert.ToInt32(Session["Role"].ToString());
            companycode = "1";
            LoadPlantcode();
            dat = System.DateTime.Now;
            txt_FromDate.Text = dat.ToString("dd/MM/yyyy");
            Lbl_Errormsg.Visible = false;
        }
        else
        {
        }
    }

    private void LoadPlantcode()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(companycode.ToString());
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";
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

    private void loadsingleplant()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(companycode.ToString(), plantcode);
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";
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

    protected void btn_Import_Click(object sender, EventArgs e)
    {
        // PROCUREMENTIMPORT=VMCCDPU
        // PROCUREMENT=PRODUCERPROCUREMENT


        //Check the Data Already Transfer or Not
        plantcode = ddl_Plantname.SelectedItem.Value;
        dat = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        frmdate = dat.ToString("MM/dd/yyyy");
        int DataTrasfer = 0;
        DataTrasfer = ProducerDatatransferCheck(companycode, plantcode, frmdate);
        if (DataTrasfer >= 1)
        {
            string remarksmsg = "Already Producers DataTransfer...";
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
            Lbl_Errormsg.Text = remarksmsg;
        }
        else
        {
            try
            {
                using (con = DBaccess.GetConnection())
                {
                    setBO();
                    //
                    //SqlCommand cmdd = new SqlCommand("DELETE from   PROCUREMENT where plant_code='" + plantcode + "' and  prdate= '" + frmdate + "' ", con);                        
                    SqlCommand cmdd = new SqlCommand("DELETE from   PRODUCERPROCUREMENT where plant_code='" + plantcode + "' and  prdate= '" + frmdate + "' AND sessions='" + ddl_ses.Text + "' ", con);
                    cmdd.ExecuteNonQuery();
                    //

                    ProcucerprocurementImport(rateBO);

                    SqlCommand cmd = new SqlCommand();
                    SqlCommand cmd1 = new SqlCommand();

                    DataTable dtt = new DataTable();
                    SqlDataAdapter daa;

                    //daa = new SqlDataAdapter("SELECT * from PROCUREMENTIMPORT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);

                    daa = new SqlDataAdapter("SELECT * from  VMCCDPU where plant_code='" + plantcode + "' and  prdate= '" + frmdate + "' AND shift='" + ddl_ses.Text + "' ", con);
                    daa.Fill(dtt);
                    int count1 = dtt.Rows.Count;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da;
                    //da = new SqlDataAdapter("SELECT * from PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                    //da = new SqlDataAdapter("SELECT * from PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                    da = new SqlDataAdapter("SELECT * from PRODUCERPROCUREMENT where plant_code='" + plantcode + "' and  prdate= '" + frmdate + "' AND sessions='" + ddl_ses.Text + "' ", con);
                    da = new SqlDataAdapter("SELECT * from PRODUCERPROCUREMENT where plant_code='" + plantcode + "' and  prdate= '" + frmdate + "' AND sessions='" + ddl_ses.Text + "' ", con);
                    da.Fill(dt);
                    int count = dt.Rows.Count;

                    cmd.Connection = con;
                    cmd1.Connection = con;

                    if (count == count1)
                    {
                        con.Close();
                        string countmsg = count.ToString() + "__Rows are inserted...";
                        Lbl_Errormsg.Visible = true;
                        Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
                        Lbl_Errormsg.Text = countmsg;

                    }
                    else
                    {
                        //SqlCommand cmd2 = new SqlCommand("DELETE from   PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                        SqlCommand cmd2 = new SqlCommand("DELETE from   PRODUCERPROCUREMENT where plant_code ='" + plantcode + "' and prdate = '" + frmdate + "' AND sessions='" + ddl_ses.Text + "' ", con);
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        string countmsg = "Datas Not Inserted...";
                        Lbl_Errormsg.Visible = true;
                        Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
                        Lbl_Errormsg.Text = countmsg;
                    }
                }
            }
            catch (Exception ex)
            {
                //start123
                //con.Close();
                //using (con = DBaccess.GetConnection())
                //{
                //   SqlCommand cmd2 = new SqlCommand("SELECT * from   PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                //   //SqlCommand cmd2 = new SqlCommand("DELETE from   PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                //    cmd2.ExecuteNonQuery();
                //    con.Close();
                //    string countmsg = "Datas Not Inserted...";
                //    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                //    string countmsg1 = "Please Select Todate Near for fromdate...";
                //    uscMsgBox1.AddMessage(countmsg1, MessageBoxUsc_Message.enmMessageType.Success);
                //}
                //end123
                con.Close();
                using (con = DBaccess.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlCommand cmd1 = new SqlCommand();

                    DataTable dtt = new DataTable();
                    SqlDataAdapter daa;

                    //daa = new SqlDataAdapter("SELECT * from PROCUREMENTIMPORT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                    daa = new SqlDataAdapter("SELECT * from VMCCDPU where plant_code='" + plantcode + "' and  prdate = '" + frmdate + "' AND shift='" + ddl_ses.Text + "'  ", con);
                    daa.Fill(dtt);
                    int count1 = dtt.Rows.Count;

                    DataTable dt = new DataTable();
                    SqlDataAdapter da;
                    // da = new SqlDataAdapter("SELECT * from PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                    da = new SqlDataAdapter("SELECT * from PRODUCERPROCUREMENT where plant_code='" + plantcode + "' and  prdate = '" + frmdate + "' AND sessions='" + ddl_ses.Text + "'  ", con);
                    da.Fill(dt);
                    int count = dt.Rows.Count;

                    cmd.Connection = con;
                    cmd1.Connection = con;

                    if (count == count1)
                    {
                        con.Close();
                        string countmsg = count.ToString() + "__Rows are inserted...";
                        Lbl_Errormsg.Visible = true;
                        Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
                        Lbl_Errormsg.Text = countmsg;
                    }
                    else
                    {
                        //SqlCommand cmd2 = new SqlCommand("DELETE from   PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                        SqlCommand cmd2 = new SqlCommand("DELETE from   PRODUCERPROCUREMENT where plant_code='" + plantcode + "' and  prdate = '" + frmdate + "' AND sessions='" + ddl_ses.Text + "' ", con);
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        string countmsg = "Datas Not Inserted...";
                        Lbl_Errormsg.Visible = true;
                        Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
                        Lbl_Errormsg.Text = countmsg;
                    }
                }
            }
        }
    }

    public int ProducerDatatransferCheck(string ccode, string pcode, string fdate)
    {
        try
        {
            int satus;
            string sqlstr = "SELECT Count(Tid) FROM   PRODUCERPROCUREMENT where plant_code='" + pcode + "' and  prdate= '" + fdate + "' AND sessions='" + ddl_ses.Text + "' ";
            satus = DBaccess.ExecuteScalarint(sqlstr);
            return satus;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private void setBO()
    {
        try
        {
            rateBO.Plantcode = ddl_Plantname.SelectedItem.Value;
            rateBO.Companycode = companycode;
            dat = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            frmdate = dat.ToString("MM/dd/yyyy");
            rateBO.Fromdate = Convert.ToDateTime(frmdate);
            rateBO.Sess = ddl_ses.SelectedItem.Value;
        }
        catch (Exception ex)
        {

        }

    }

    public void ProcucerprocurementImport(BOLRateChart rateBO)
    {
        try
        {
            using (con = DBaccess.GetConnection())
            {
                string  sql = "update_producerprocurementImport_sessions";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 200;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlParameter p, c, fd, ses;
                p = cmd.Parameters.Add("@plantcode", SqlDbType.VarChar, 50);
                c = cmd.Parameters.Add("@companycode", SqlDbType.VarChar, 50);
                fd = cmd.Parameters.Add("@frdate", SqlDbType.Date);
                ses = cmd.Parameters.Add("@sess", SqlDbType.VarChar, 15);

                p.Value = rateBO.Plantcode;
                c.Value = rateBO.Companycode;
                fd.Value = rateBO.Fromdate;
                ses.Value = rateBO.Sess;

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {

        }
    }
        

}