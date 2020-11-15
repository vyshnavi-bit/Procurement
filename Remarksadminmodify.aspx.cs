using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;
using System.Net;
using System.IO;
using System.Net.Mail;

public partial class Remarksadminmodify : System.Web.UI.Page
{
    DateTime dtm = new DateTime();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    BLLuser Bllusers = new BLLuser();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string ses;
    public int IncdecFlag;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
               // pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
                txt_RemmarksDate.Text = dtm.ToString("dd/MM/yyyy");
                roleid = Convert.ToInt32(Session["Role"].ToString());
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170";
                    loadspecialsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }


                pcode = ddl_Plantcode.SelectedItem.Value;
                if (Chk_InDec.Checked == true)
                {
                    IncdecFlag = 1;
                }
                else
                {
                    IncdecFlag = 2;
                }
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
        if (IsPostBack == true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
               // pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_Plantcode.SelectedItem.Value;
              // BindprocurementRemarksData();
                if (Chk_InDec.Checked == true)
                {
                    IncdecFlag = 1;
                }
                else
                {
                    IncdecFlag = 2;
                }
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    protected void btnInvoke_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);       
    }

    private void BindprocurementRemarksData()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_RemmarksDate.Text, "dd/MM/yyyy", null);

            if (rd_sesAm.Checked == true)
            {
                ses = "AM";
            }
            else
            {
                ses = "PM";
            }
            string d1 = dt1.ToString("MM/dd/yyyy");
            ds = Bllproimp.LoadProocurementRemarksDatas(ccode, pcode, d1, ses, IncdecFlag);
            if (ds != null)
            {
                CheckBoxList_Sno.DataSource = ds;
                CheckBoxList_Sno.DataTextField = "serial_no";
                CheckBoxList_Sno.DataValueField = "serial_no";
                CheckBoxList_Sno.DataBind();

                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "SampleId";
                CheckBoxList1.DataValueField = "SampleId";
                CheckBoxList1.DataBind();

                CheckBoxList2.DataSource = ds;
                CheckBoxList2.DataTextField = "agent_Id";
                CheckBoxList2.DataValueField = "agent_Id";
                CheckBoxList2.DataBind();

                CheckBoxList3.DataSource = ds;
                CheckBoxList3.DataTextField = "milk_Kg";
                CheckBoxList3.DataValueField = "milk_Kg";
                CheckBoxList3.DataBind();

                CheckBoxList4.DataSource = ds;
                CheckBoxList4.DataTextField = "fat";
                CheckBoxList4.DataValueField = "fat";
                CheckBoxList4.DataBind();

                CheckBoxList5.DataSource = ds;
                CheckBoxList5.DataTextField = "snf";
                CheckBoxList5.DataValueField = "snf";
                CheckBoxList5.DataBind();

                CheckBoxList6.DataSource = ds;
                CheckBoxList6.DataTextField = "magent_Id";
                CheckBoxList6.DataValueField = "magent_Id";
                CheckBoxList6.DataBind();

                CheckBoxList7.DataSource = ds;
                CheckBoxList7.DataTextField = "mmilk_Kg";
                CheckBoxList7.DataValueField = "mmilk_Kg";
                CheckBoxList7.DataBind();

                CheckBoxList8.DataSource = ds;
                CheckBoxList8.DataTextField = "mfat";
                CheckBoxList8.DataValueField = "mfat";
                CheckBoxList8.DataBind();

                CheckBoxList9.DataSource = ds;
                CheckBoxList9.DataTextField = "msnf";
                CheckBoxList9.DataValueField = "msnf";
                CheckBoxList9.DataBind();

            }
            //Adding color to differenciate the remarks modifications Start
            SetColorRemarksData();
            //Adding color to differenciate the remarks modifications End
        }
        catch (Exception ex)
        {

        }
        CheckBoxListClear1();
    }
    private void SetColorRemarksData()
    {

        double ActualAgentid = 0.0;
        double ModifyAgentid = 0.0;
        double Actualmilkkg = 0.0;
        double Modifymilkkg = 0.0;
        double ActualFat = 0.0;
        double ModifyFat = 0.0;
        double ActualSnf = 0.0;
        double ModifySnf = 0.0;
        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        {
            ActualAgentid = 0.0;
            ModifyAgentid = 0.0;
            Actualmilkkg = 0.0;
            Modifymilkkg = 0.0;
            ActualFat = 0.0;
            ModifyFat = 0.0;
            ActualSnf = 0.0;
            ModifySnf = 0.0;

            ActualAgentid = Convert.ToDouble(CheckBoxList2.Items[i].Value);
            ModifyAgentid = Convert.ToDouble(CheckBoxList6.Items[i].Value);
            Actualmilkkg = Convert.ToDouble(CheckBoxList3.Items[i].Value);
            Modifymilkkg = Convert.ToDouble(CheckBoxList7.Items[i].Value);
            ActualFat = Convert.ToDouble(CheckBoxList4.Items[i].Value);
            ModifyFat = Convert.ToDouble(CheckBoxList8.Items[i].Value);
            ActualSnf = Convert.ToDouble(CheckBoxList5.Items[i].Value);
            ModifySnf = Convert.ToDouble(CheckBoxList9.Items[i].Value);

            //Agent_Id
            if (ActualAgentid < ModifyAgentid)
            {
                //CheckBoxList8.Items[i].Attributes.Add("color", "orange");                
                CheckBoxList6.Items[i].Attributes.Add("style", "background-color: LightPink;");
            }
            else if (ActualAgentid > ModifyAgentid)
            {
                CheckBoxList6.Items[i].Attributes.Add("style", "background-color: LightGreen");
            }
            else
            {
                CheckBoxList6.Items[i].Attributes.Add("style", "background-color: white;");

            }
            //Milk_kg
            if (Actualmilkkg < Modifymilkkg)
            {

                CheckBoxList7.Items[i].Attributes.Add("style", "background-color: LightPink;");
            }
            else if (Actualmilkkg > Modifymilkkg)
            {
                CheckBoxList7.Items[i].Attributes.Add("style", "background-color: LightGreen");
            }
            else
            {
                CheckBoxList7.Items[i].Attributes.Add("style", "background-color: white;");

            }
            //Fat
            if (ActualFat < ModifyFat)
            {

                CheckBoxList8.Items[i].Attributes.Add("style", "background-color: LightPink;");

            }
            else if (ActualFat > ModifyFat)
            {
                CheckBoxList8.Items[i].Attributes.Add("style", "background-color: LightGreen");
            }
            else
            {
                CheckBoxList8.Items[i].Attributes.Add("style", "background-color: white;");

            }
            //Snf
            if (ActualSnf < ModifySnf)
            {

                CheckBoxList9.Items[i].Attributes.Add("style", "background-color: LightPink;");
            }
            else if (ActualSnf > ModifySnf)
            {
                CheckBoxList9.Items[i].Attributes.Add("style", "background-color: LightGreen");
            }
            else
            {
                CheckBoxList9.Items[i].Attributes.Add("style", "background-color: white;");

            }


        }
    }


    private void SaveRemarksData()
    {
        try
        {
            string modifyby = Session["userid"].ToString();
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_RemmarksDate.Text, "dd/MM/yyyy", null);

            if (rd_sesAm.Checked == true)
            {
                ses = "AM";
            }
            else
            {
                ses = "PM";
            }
            string d1 = dt1.ToString("MM/dd/yyyy");
            int count = 0;
            dt = Bllproimp.LoadProocurementRemarksDatas1(ccode, pcode, d1, ses, IncdecFlag);
            count = dt.Rows.Count;
            if (count > 0)
            {
                DataTable custDT = new DataTable();
                DataColumn col = null;

                col = new DataColumn("sap_sampleno");
                custDT.Columns.Add(col);
                col = new DataColumn("agent_Id");
                custDT.Columns.Add(col);
                col = new DataColumn("milk_Kg");
                custDT.Columns.Add(col);
                col = new DataColumn("fat");
                custDT.Columns.Add(col);
                col = new DataColumn("Snf");
                custDT.Columns.Add(col);
                col = new DataColumn("modify_aid");
                custDT.Columns.Add(col);
                col = new DataColumn("modify_Kg");
                custDT.Columns.Add(col);
                col = new DataColumn("modify_fat");
                custDT.Columns.Add(col);
                col = new DataColumn("modify_snf");
                custDT.Columns.Add(col);

                for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                {
                    DataRow dr = null;
                    dr = custDT.NewRow();
                    string sampleno = "";
                    string agentid = "";
                    string milkkg = "";
                    string fat = "";
                    string snf = "";
                    string modifyagentid = "";
                    string modifykg = "";
                    string modifyfat = "";
                    string modifysnf = "";
                    String ConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                    if (CheckBoxList1.Items[i].Selected == true)
                    {
                        dr[0] = CheckBoxList1.Items[i].Value;
                        sampleno = CheckBoxList1.Items[i].Value;
                    }
                    else
                    {
                        dr[0] = CheckBoxList1.Items[i].Value;
                        sampleno = CheckBoxList1.Items[i].Value;
                    }

                    if (CheckBoxList2.Items[i].Selected == true)
                    {
                        dr[1] = CheckBoxList2.Items[i].Value;
                        agentid = CheckBoxList2.Items[i].Value;
                    }
                    else
                    {
                        dr[1] = CheckBoxList2.Items[i].Value;
                        agentid = CheckBoxList2.Items[i].Value;
                    }

                    if (CheckBoxList3.Items[i].Selected == true)
                    {
                        dr[2] = CheckBoxList3.Items[i].Value;
                        milkkg = CheckBoxList3.Items[i].Value;
                    }
                    else
                    {
                        dr[2] = CheckBoxList3.Items[i].Value;
                        milkkg = CheckBoxList3.Items[i].Value;
                    }


                    if (CheckBoxList4.Items[i].Selected == true)
                    {
                        dr[3] = CheckBoxList4.Items[i].Value;
                        fat = CheckBoxList4.Items[i].Value;
                    }
                    else
                    {
                        dr[3] = CheckBoxList4.Items[i].Value;
                        fat = CheckBoxList4.Items[i].Value;
                    }


                    if (CheckBoxList5.Items[i].Selected == true)
                    {
                        dr[4] = CheckBoxList5.Items[i].Value;
                        snf = CheckBoxList5.Items[i].Value;
                    }
                    else
                    {
                        dr[4] = CheckBoxList5.Items[i].Value;
                        snf = CheckBoxList5.Items[i].Value;
                    }


                    if (CheckBoxList6.Items[i].Selected == true)
                    {
                        dr[5] = CheckBoxList6.Items[i].Value;
                        modifyagentid = CheckBoxList6.Items[i].Value;
                    }
                    else
                    {
                        dr[5] = CheckBoxList2.Items[i].Value;
                        modifyagentid = CheckBoxList6.Items[i].Value;
                    }

                    if (CheckBoxList7.Items[i].Selected == true)
                    {
                        dr[6] = CheckBoxList7.Items[i].Value;
                        modifykg = CheckBoxList7.Items[i].Value;
                    }
                    else
                    {
                        dr[6] = CheckBoxList3.Items[i].Value;
                        modifykg = CheckBoxList3.Items[i].Value;
                    }

                    if (CheckBoxList8.Items[i].Selected == true)
                    {
                        dr[7] = CheckBoxList8.Items[i].Value;
                        modifyfat = CheckBoxList8.Items[i].Value;

                    }
                    else
                    {
                        dr[7] = CheckBoxList4.Items[i].Value;
                        modifyfat = CheckBoxList4.Items[i].Value;

                    }


                    if (CheckBoxList9.Items[i].Selected == true)
                    {
                        dr[8] = CheckBoxList9.Items[i].Value;
                        modifysnf = CheckBoxList9.Items[i].Value;
                    }
                    else
                    {
                        dr[8] = CheckBoxList5.Items[i].Value;
                        modifysnf = CheckBoxList5.Items[i].Value;
                    }
                    custDT.Rows.Add(dr);
                    SqlConnection con = new SqlConnection(ConnStr);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into modifydata(agentid, sampleno, milkkg, fat, snf, modifykg, modifyfat, modifysnf, modifiedby, Plant_code, doe) VALUES (@agentid, @sampleno, @milkkg, @fat, @snf, @modifykg, @modifyfat, @modifysnf, @modifyby, @Plant_code, @doe)", con);
                    cmd.Parameters.Add("@agentid", agentid);
                    cmd.Parameters.Add("@sampleno", sampleno);
                    cmd.Parameters.Add("@milkkg", milkkg);
                    cmd.Parameters.Add("@fat", fat);
                    cmd.Parameters.Add("@snf", snf);
                    cmd.Parameters.Add("@modifykg", modifykg);
                    cmd.Parameters.Add("@modifyfat", modifyfat);
                    cmd.Parameters.Add("@modifysnf", modifysnf);
                    cmd.Parameters.Add("@modifyby", modifyby);
                    cmd.Parameters.Add("@Plant_code", pcode);
                    cmd.Parameters.Add("@doe", d1);
                    cmd.ExecuteNonQuery();
                }

                //if (custDT.Rows.Count > 0)
                //{
                //    string no = "";
                //    string message1 = "";
                //    string strUrl = " http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=vyshnavi@123&from=VYSNVI&to=" + no + "&msg=" + custDT + "&type=1 ";
                //    WebRequest request = HttpWebRequest.Create(strUrl);
                //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //    Stream s = (Stream)response.GetResponseStream();
                //    StreamReader readStream = new StreamReader(s);
                //    string dataString = readStream.ReadToEnd();
                //    response.Close();
                //    s.Close();
                //    readStream.Close();
                //}

                SqlParameter param = new SqlParameter();
                param.ParameterName = "CustDtl";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = custDT;
                param.Direction = ParameterDirection.Input;
                String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[UpdateproDetail]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                    sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                    sqlCmd.Parameters.AddWithValue("@spdate", d1);
                    sqlCmd.Parameters.AddWithValue("@spsess", ses);
                    sqlCmd.ExecuteNonQuery();
                }
                if (custDT.Rows.Count > 0)
                {
                    string toAddress = "naveen@vyshnavi.in";
                    string subject = "Remarks Approval Details";
                    string result = "Success";
                    string senderID = "no-reply@vyshnavi.in";// use sender's email id here..
                    const string senderPassword = "Vyshnavi@123"; // sender password here...
                    try
                    {
                        SmtpClient smtp = new SmtpClient
                        {
                            Host = "czismtp.logix.in", // smtp server address here...
                            Port = 587,
                            //security type=tsl;
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                            Timeout = 30000,
                        };
                        MailMessage message = new MailMessage(senderID, toAddress, subject, "<html><body>" + custDT + "<br></body></html>");
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                    }
                    catch (Exception ex)
                    {
                        result = "Error sending data please try again.!!!";
                    }
                }
            }
            CheckBoxListClear();
        }
        catch (Exception ex)
        {

        }
    }
    private void CheckBoxListClear()
    {
        CheckBoxList1.Items.Clear();
        CheckBoxList2.Items.Clear();
        CheckBoxList3.Items.Clear();
        CheckBoxList4.Items.Clear();
        CheckBoxList5.Items.Clear();
        CheckBoxList6.Items.Clear();
        CheckBoxList7.Items.Clear();
        CheckBoxList8.Items.Clear();
        CheckBoxList9.Items.Clear();
        CheckBoxList_Sno.Items.Clear();
        MChk_Menu6.Checked = false;
        MChk_Menu7.Checked = false;
        MChk_Menu8.Checked = false;
        MChk_Menu9.Checked = false;        
    }
    private void CheckBoxListClear1()
    {
        
        MChk_Menu6.Checked = false;
        MChk_Menu7.Checked = false;
        MChk_Menu8.Checked = false;
        MChk_Menu9.Checked = false;
      
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
        SetColorRemarksData();
    }
    protected void MChk_Menu7_CheckedChanged(object sender, EventArgs e)
    {
        Menu7();
        SetColorRemarksData();
    }
    protected void MChk_Menu8_CheckedChanged(object sender, EventArgs e)
    {
        Menu8();
        SetColorRemarksData();
    }
    protected void MChk_Menu9_CheckedChanged(object sender, EventArgs e)
    {
        Menu9();
        SetColorRemarksData();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        BindprocurementRemarksData();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        SaveRemarksData();
    }
    protected void rd_sesPm_CheckedChanged(object sender, EventArgs e)
    {
        if(rd_sesPm.Checked==true)
        {
            rd_sesAm.Checked = false;
            //rd_sesPm.Checked = true;
            CheckBoxListClear();
        }
    }
    protected void rd_sesAm_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_sesAm.Checked == true)
        {
            rd_sesPm.Checked = false;
            //rd_sesAm.Checked = true;
            CheckBoxListClear();
        }
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
        pcode = ddl_Plantcode.SelectedItem.Value;
        CheckBoxListClear();
    }


    protected void Chk_InDec_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_InDec.Checked == true)
        {
            IncdecFlag = 1;
            CheckBoxListClear();
        }
        else
        {
            IncdecFlag = 2;
            CheckBoxListClear();
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

}