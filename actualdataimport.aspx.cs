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

public partial class actualdataimport : System.Web.UI.Page
{
    string Company_code;
    string plant_Code;
    SqlConnection con = new SqlConnection();
    BLLuser Bllusers = new BLLuser();
    BLLImportDB impdbBL = new BLLImportDB();
    BOLRateChart rateBO = new BOLRateChart();
    DbHelper DBaccess = new DbHelper();
    DataTable getmissagent = new DataTable();
    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {
        uscMsgBox1.MsgBoxAnswered += MessageAnswered;
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Session["Company_code"].ToString();
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    plant_Code = "170";
                    loadspecialsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                plant_Code = ddl_Plantcode.SelectedItem.Value;
                if (Chk_periodsess.Checked == true)
                {
                    lbl_sess.Visible = false;
                    ddl_ses.Visible = false;
                    btn_sess.Visible = false;
                    btn_show.Visible = true;
                }
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
            GridView2.Visible = false;
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = ddl_Plantcode.SelectedItem.Value;
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

    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered ", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    private void CheckRemarksUpdate()
    {
        try
        {
            SqlDataReader dr = null;
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

    private void LoadPlantcode()
    {

        try
        {
            SqlDataReader dr = null;
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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        plant_Code = ddl_Plantcode.SelectedItem.Value;
        //LoadPlantcode();
    }
    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantname.SelectedIndex = ddl_Plantcode.SelectedIndex;
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            con = DBaccess.GetConnection();
            string getperiodstr = "";
            getperiodstr = "select *  from( Select procagent,Agent_Name  from (sELECT Agent_id as procagent   FROM Procurementimport   where Plant_Code='" + plant_Code + "'  and Prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' GROUP BY Agent_id ) as procimport  left  join(Select agent_id,Agent_Name   from Agent_Master  where Plant_code=" + plant_Code + ") as  am on procimport.procagent=am.Agent_Id ) as gg  where gg.procagent=Agent_Name";
            SqlCommand cmdperiod = new SqlCommand(getperiodstr, con);
            SqlDataAdapter sall = new SqlDataAdapter(cmdperiod);
            getmissagent.Rows.Clear();
            sall.Fill(getmissagent);
            if (getmissagent.Rows.Count > 0)
            {
                GridView2.DataSource = getmissagent;
                GridView2.DataBind();
                GridView2.Visible = true;
            }
            else
            {
                GridView2.Visible = false;
                //Check remmarks Update or Not
                int Rstatus = 0;
                Rstatus = Bllusers.Remarksstatus(Company_code, plant_Code, txt_FromDate.Text, txt_ToDate.Text);
                if (Rstatus >= 1)
                {
                    string remarksmsg = "Check the RemarksUpdate...";
                    uscMsgBox1.AddMessage(remarksmsg, MessageBoxUsc_Message.enmMessageType.Success);
                }
                else
                {
                    //Check the Data Already Transfer or Not
                    int DataTrasfer = 0;
                    DataTrasfer = Bllusers.DatatransferCheck(Company_code, plant_Code, txt_FromDate.Text, txt_ToDate.Text);
                    if (DataTrasfer >= 1)
                    {
                        string remarksmsg = "Already DataTransfer...";
                        uscMsgBox1.AddMessage(remarksmsg, MessageBoxUsc_Message.enmMessageType.Success);
                    }
                    else
                    {
                        try
                        {
                            using (con = DBaccess.GetConnection())
                            {
                                setBO();
                                SqlCommand cmdd = new SqlCommand("DELETE from   PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                                cmdd.ExecuteNonQuery();
                                impdbBL.deleteimport(rateBO);
                                SqlCommand cmd = new SqlCommand();
                                SqlCommand cmd1 = new SqlCommand();
                                DataTable dtt = new DataTable();
                                SqlDataAdapter daa;
                                daa = new SqlDataAdapter("SELECT * from PROCUREMENTIMPORT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                                daa.Fill(dtt);
                                int count1 = dtt.Rows.Count;
                                DataTable dt = new DataTable();
                                SqlDataAdapter da;
                                da = new SqlDataAdapter("SELECT * from PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                                da.Fill(dt);
                                int count = dt.Rows.Count;
                                cmd.Connection = con;
                                cmd1.Connection = con;
                                if (count == count1)
                                {
                                    con.Close();
                                    string countmsg = count.ToString() + "__Rows are inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                                else
                                {
                                    SqlCommand cmd2 = new SqlCommand("DELETE from   PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                                    cmd2.ExecuteNonQuery();
                                    con.Close();
                                    string countmsg = "Datas Not Inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            using (con = DBaccess.GetConnection())
                            {
                                SqlCommand cmd = new SqlCommand();
                                SqlCommand cmd1 = new SqlCommand();
                                DataTable dtt = new DataTable();
                                SqlDataAdapter daa;
                                daa = new SqlDataAdapter("SELECT * from PROCUREMENTIMPORT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                                daa.Fill(dtt);
                                int count1 = dtt.Rows.Count;
                                DataTable dt = new DataTable();
                                SqlDataAdapter da;
                                da = new SqlDataAdapter("SELECT * from PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                                da.Fill(dt);
                                int count = dt.Rows.Count;
                                cmd.Connection = con;
                                cmd1.Connection = con;
                                if (count == count1)
                                {
                                    con.Close();
                                    string countmsg = count.ToString() + "__Rows are inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                                else
                                {
                                    SqlCommand cmd2 = new SqlCommand("DELETE from   PROCUREMENT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "' ", con);
                                    cmd2.ExecuteNonQuery();
                                    con.Close();
                                    string countmsg = "Datas Not Inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception EX)
        {
        }
    }

    private void setBO()
    {
        rateBO.Plantcode = ddl_Plantcode.Text;
        rateBO.Companycode = Company_code;
        rateBO.Fromdate = Convert.ToDateTime(txt_FromDate.Text);
        rateBO.Todate = Convert.ToDateTime(txt_ToDate.Text);
        rateBO.Sess = ddl_ses.SelectedItem.Value;
    }


    protected void Chk_periodsess_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_periodsess.Checked == true)
        {
            lbl_sess.Visible = false;
            ddl_ses.Visible = false;
            btn_sess.Visible = false;
            btn_show.Visible = true;
        }
        else
        {
            lbl_sess.Visible = true;
            ddl_ses.Visible = true;
            btn_sess.Visible = true;
            btn_show.Visible = false;
        }
    }
    protected void btn_sess_Click(object sender, EventArgs e)
    {
        try
        {
            //Check remmarks Update or Not
            con = DBaccess.GetConnection();
            string getperiodstr = "";
            getperiodstr = "select *  from( Select procagent,Agent_Name  from (sELECT Agent_id as procagent   FROM Procurementimport   where Plant_Code='" + plant_Code + "'  and Prdate='" + txt_FromDate.Text + "' and sessions='" + ddl_ses.SelectedItem.Text + "' GROUP BY Agent_id  ) as procimport  left  join(Select agent_id,Agent_Name   from Agent_Master  where Plant_code='" + plant_Code + "') as  am on procimport.procagent=am.Agent_Id ) as gg  where gg.procagent=Agent_Name";
            SqlCommand cmdperiod = new SqlCommand(getperiodstr, con);
            SqlDataAdapter sall = new SqlDataAdapter(cmdperiod);
            getmissagent.Rows.Clear();
            sall.Fill(getmissagent);
            if (getmissagent.Rows.Count > 0)
            {
                GridView2.DataSource = getmissagent;
                GridView2.DataBind();
                GridView2.Visible = true;
            }
            else
            {
                GridView2.Visible = false;
                int Rstatus = 0;
                Rstatus = Bllusers.actualRemarksstatussess(Company_code, plant_Code, txt_FromDate.Text, txt_FromDate.Text, ddl_ses.SelectedItem.Value);
                if (Rstatus >= 1)
                {
                    string remarksmsg = "Check the RemarksUpdate...";
                    uscMsgBox1.AddMessage(remarksmsg, MessageBoxUsc_Message.enmMessageType.Success);
                }
                else
                {
                    //Check the Data Already Transfer or Not
                    int DataTrasfer = 0;
                    DataTrasfer = Bllusers.actualDatatransferChecksess(Company_code, plant_Code, txt_FromDate.Text, txt_FromDate.Text, ddl_ses.SelectedItem.Value);
                    if (DataTrasfer >= 1)
                    {
                        string remarksmsg = "Already DataTransfer...";
                        uscMsgBox1.AddMessage(remarksmsg, MessageBoxUsc_Message.enmMessageType.Success);
                    }
                    else
                    {
                        try
                        {

                            using (con = DBaccess.GetConnection())
                            {
                                setBO();
                                SqlCommand cmdd = new SqlCommand("DELETE from   ccProcurement where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and Sessions='" + ddl_ses.SelectedItem.Value + "' ", con);
                                cmdd.ExecuteNonQuery();
                                impdbBL.actualdeleteimportsess(rateBO);
                                SqlCommand cmd = new SqlCommand();
                                SqlCommand cmd1 = new SqlCommand();
                                DataTable dtt = new DataTable();
                                SqlDataAdapter daa;
                                daa = new SqlDataAdapter("SELECT * from PROCUREMENTIMPORT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and Sessions='" + ddl_ses.SelectedItem.Value + "' ", con);
                                daa.Fill(dtt);
                                int count1 = dtt.Rows.Count;
                                DataTable dt = new DataTable();
                                SqlDataAdapter da;
                                da = new SqlDataAdapter("SELECT * from ccProcurement where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and Sessions='" + ddl_ses.SelectedItem.Value + "' ", con);
                                da.Fill(dt);
                                int count = dt.Rows.Count;
                                cmd.Connection = con;
                                cmd1.Connection = con;
                                if (count == count1)
                                {
                                    con.Close();
                                    string countmsg = count.ToString() + "__Rows are inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                                else
                                {
                                    SqlCommand cmd2 = new SqlCommand("DELETE from   ccProcurement where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and Sessions='" + ddl_ses.SelectedItem.Value + "' ", con);
                                    cmd2.ExecuteNonQuery();
                                    con.Close();
                                    string countmsg = "Datas Not Inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            using (con = DBaccess.GetConnection())
                            {
                                SqlCommand cmd = new SqlCommand();
                                SqlCommand cmd1 = new SqlCommand();
                                DataTable dtt = new DataTable();
                                SqlDataAdapter daa;
                                daa = new SqlDataAdapter("SELECT * from PROCUREMENTIMPORT where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and Sessions='" + ddl_ses.SelectedItem.Value + "' ", con);
                                daa.Fill(dtt);
                                int count1 = dtt.Rows.Count;
                                DataTable dt = new DataTable();
                                SqlDataAdapter da;
                                da = new SqlDataAdapter("SELECT * from ccProcurement where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and Sessions='" + ddl_ses.SelectedItem.Value + "' ", con);
                                da.Fill(dt);
                                int count = dt.Rows.Count;
                                cmd.Connection = con;
                                cmd1.Connection = con;
                                if (count == count1)
                                {
                                    con.Close();
                                    string countmsg = count.ToString() + "__Rows are inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                                else
                                {
                                    SqlCommand cmd2 = new SqlCommand("DELETE from   ccProcurement where plant_code='" + plant_Code + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and Sessions='" + ddl_ses.SelectedItem.Value + "' ", con);
                                    cmd2.ExecuteNonQuery();
                                    con.Close();
                                    string countmsg = "Datas Not Inserted...";
                                    uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    
}