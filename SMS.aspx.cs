using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;

public partial class SMS : System.Web.UI.Page
{
    string uid;
    string password;
    string senderId;
    string message;
    string message1,messagesave;
    string no, cname, pname;
    int pcode, cmpcode;
     DateTime dti = new DateTime();
     
     string dti1 = string.Empty;
     string dti2 = string.Empty;
     DateTime dti3 = new DateTime();
     DataTable dt1 = new DataTable();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    SqlCommand cmd2 = new SqlCommand();
    BLLuser Bllusers = new BLLuser();
    public  string[] strrpname = new string[10];
    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            uscMsgBox1.MsgBoxAnswered += MessageAnswered;
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                cname = Session["cname"].ToString();
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                dti = System.DateTime.Now;
                txt_FromDate.Text = dti.ToString("dd/MM/yyy");
                txt_ToDate.Text = dti.ToString("dd/MM/yyy");
                if (roleid < 3)
                {
                    LoadPlantcode1();
                    pcode = Convert.ToInt16(ddl_Plantcode.Text);
                    pname = ddl_Plantname.SelectedItem.Text;
                }
                if (roleid >= 3)
                {
                    LoadPlantcode();
                    pcode = Convert.ToInt16(ddl_Plantcode.Text);
                    pname = ddl_Plantname.SelectedItem.Text;
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    pcode = 170;
                    pname = ddl_Plantname.SelectedItem.Text;
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
                cname = Session["cname"].ToString();
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                if (roleid < 3)
                {
                    pname = ddl_Plantname.SelectedItem.Text;
                    ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
                    pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Value);
                }
                if (roleid >= 3)
                {
                    pname = ddl_Plantname.SelectedItem.Text;
                    ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
                    pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Value);
                }
                if (roleid == 9)
                {
                    pname = ddl_Plantname.SelectedItem.Text;
                    ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
                    pcode = 170;
                }
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
            dr = Bllusers.LoadSinglePlantcode(cmpcode.ToString(), "170");
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
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }

    private bool Validation()
    {
        if (Convert.ToInt32(message.Length) > 130)
        {

            Label1.Text = "can enter only 130 characters...";
            TextBox2.Focus();
            return false;
        }
        return true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            //uid = "ASnTech";
            //password = "Kap@user!23";
            //senderId = "VSNAVI";
            sendsms();
            //uid = "";
            //password = "";
            //TextBox2.Text = "";
            //TextBox1.Text = "";
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private DateTime GetLowDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        DT = dt;
        Hour = -dt.Hour;
        Min = -dt.Minute;
        Sec = -dt.Second;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }
    private DateTime GetHighDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        Hour = 23 - dt.Hour;
        Min = 59 - dt.Minute;
        Sec = 59 - dt.Second;
        DT = dt;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }
    public void sendsms()
    {
        int j = 0;
        string pnameset = string.Empty;
        pname = ddl_Plantname.SelectedItem.Text;
        //pname split
        strrpname = pname.Split('_');
        pname = strrpname[1];
        try
        {
            lblmsg.Text = "";
            DateTime fromdate = DateTime.Now.AddDays(-5);
            dti = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string[] datestrig = txt_FromDate.Text.Split(' ');
            if (datestrig.Length > 1)
            {
                if (datestrig[0].Split('-').Length > 0)
                {
                    string[] dates = datestrig[0].Split('-');
                    string[] times = datestrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            //string sqlstr = "SELECT Top 100 phone_Number,Agent_Id,ISNULL(Ases,0) AS Ases,ISNULL(Aliter,0) AS Aliter,ISNULL(AFAT,0) AS  AFAT,ISNULL(ASNF,0) AS ASNF,ISNULL(ACLR,0) AS ACLR,ISNULL(ARate,0) AS ARate,ISNULL(ANetAmount,0) AS ANetAmount,ISNULL(Pses,0) AS Pses,ISNULL(Pliter,0) AS Pliter,ISNULL(PFAT,0) AS  PFAT,ISNULL(PSNF,0) AS PSNF,ISNULL(PCLR,0) AS PCLR,ISNULL(PRate,0) AS PRate,ISNULL(PNetAmount,0) AS PNetAmount  FROM (select t1.phone_Number,t1.agent_Id,round(t2.liter,2) AS Aliter,round(t2.FAT,2) As AFAT,round(t2.SNF,2) as ASNF,round(t2.CLR,2) as ACLR,round(t2.Rate,2) As ARate,round(t2.NetAmount,2) As ANetAmount,Ases from (select distinct agent_Id,phone_Number from Agent_Master where Company_code='" + cmpcode + "' and plant_code='" + pcode + "' and agent_Id in(select distinct agent_Id from procurement where Company_code='" + cmpcode + "' and Plant_Code='" + pcode + "' and  prdate between '" + dti.ToString() + "' and '" + dti.ToString() + "' and Status='0' and phone_Number>'0' group by agent_Id)  ) as t1 left join (select distinct agent_Id,sum(milk_Ltr) as liter,avg(fat) as FAT,avg(snf) as SNF,AVG(clr) as CLR,avg(rate) as Rate,sum(Amount) as NetAmount,Sessions as Ases from procurement where Company_code='" + cmpcode + "' and Plant_Code='" + pcode + "'  and  prdate between  '" + dti.ToString() + "' and '" + dti.ToString() + "' and Sessions='AM' group by agent_Id,Sessions) as t2 on  t1.agent_Id=t2.agent_Id) AS t3 left join (select distinct agent_Id AS PAid,sum(milk_Ltr) as Pliter,avg(fat) as PFAT,avg(snf) as PSNF,AVG(clr) as PCLR,avg(rate) as PRate,sum(Amount) as PNetAmount,Sessions as Pses from procurement where Company_code='" + cmpcode + "' and Plant_Code='" + pcode + "'  and  prdate between  '" + dti.ToString() + "' and '" + dti.ToString() + "' and Sessions='PM' group by agent_Id,Sessions) as t4 ON t3.Agent_Id=t4.PAid order by  Agent_Id";
            // Ravindra
            dbaccess = new DbHelper();
            cmd = new SqlCommand("SELECT TOP (100) t3.phone_Number, t3.Agent_Id, ISNULL(t3.Ases, 0) AS Ases, ISNULL(t3.Aliter, 0) AS Aliter, ISNULL(t3.AFAT, 0) AS AFAT, ISNULL(t3.ASNF, 0) AS ASNF, ISNULL(t3.ACLR, 0) AS ACLR, ISNULL(t3.ARate, 0) AS ARate, ISNULL(t3.ANetAmount, 0) AS ANetAmount, ISNULL(t4.Pses, 0) AS Pses, ISNULL(t4.Pliter, 0) AS Pliter, ISNULL(t4.PFAT, 0) AS PFAT, ISNULL(t4.PSNF, 0) AS PSNF, ISNULL(t4.PCLR, 0) AS PCLR, ISNULL(t4.PRate, 0) AS PRate, ISNULL(t4.PNetAmount, 0) AS PNetAmount FROM            (SELECT        t1.phone_Number, t1.Agent_Id, ROUND(t2.liter, 2) AS Aliter, ROUND(t2.FAT, 2) AS AFAT, ROUND(t2.SNF, 2) AS ASNF, ROUND(t2.CLR, 2) AS ACLR, ROUND(t2.Rate, 2) AS ARate, ROUND(t2.NetAmount, 2) AS ANetAmount, t2.Ases FROM (SELECT DISTINCT Agent_Id, phone_Number FROM Agent_Master WHERE (Company_code = @c) AND (Plant_code = @p) AND (Agent_Id IN (SELECT DISTINCT Agent_id FROM Procurement WHERE (Company_Code = @c) AND (Plant_Code = @p) AND (Prdate BETWEEN @d1 AND @d2) AND (Status = '0') AND (Agent_Master.phone_Number > '0') GROUP BY Agent_id))) AS t1 LEFT OUTER JOIN  (SELECT DISTINCT Agent_id, SUM(Milk_ltr) AS liter, AVG(Fat) AS FAT, AVG(Snf) AS SNF, AVG(Clr) AS CLR, AVG(Rate) AS Rate, SUM(Amount) AS NetAmount, Sessions AS Ases FROM Procurement AS Procurement_2 WHERE (Company_Code = @c) AND (Plant_Code = @p) AND (Prdate BETWEEN @d1 AND @d2) AND (Sessions = 'AM') GROUP BY Agent_id, Sessions) AS t2 ON t1.Agent_Id = t2.Agent_id) AS t3 LEFT OUTER JOIN (SELECT DISTINCT Agent_id AS PAid, SUM(Milk_ltr) AS Pliter, AVG(Fat) AS PFAT, AVG(Snf) AS PSNF, AVG(Clr) AS PCLR, AVG(Rate) AS PRate, SUM(Amount) AS PNetAmount, Sessions AS Pses FROM Procurement AS Procurement_1 WHERE        (Company_Code = @c) AND (Plant_Code = @p) AND (Prdate BETWEEN @d1 AND @d2) AND (Sessions = 'PM') GROUP BY Agent_id, Sessions) AS t4 ON t3.Agent_Id = t4.PAid ORDER BY t3.Agent_Id");
            cmd.Parameters.Add("@c", cmpcode);
            cmd.Parameters.Add("@p", pcode);
            cmd.Parameters.Add("@d1", GetLowDate(dti));
            cmd.Parameters.Add("@d2", GetHighDate(dti));
            DataTable dt = dbaccess.SelectQuery(cmd).Tables[0];


            string strUrll = "http://www.smsstriker.com/API/get_balance.php?username=vaishnavidairy&password=vyshnavi@123";
            WebRequest request1 = HttpWebRequest.Create(strUrll);
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            Stream ss = (Stream)response1.GetResponseStream();
            StreamReader readStream1 = new StreamReader(ss);
            string dataString1 = readStream1.ReadToEnd();
            response1.Close();
            ss.Close();
            readStream1.Close();
            string count = dataString1.Trim();
            int smscount = Convert.ToInt32(count);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows.Count < smscount)
                {
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            no = string.Empty;
                            message1 = string.Empty;
                            string agentid = string.Empty;
                            string sess = string.Empty;
                            string litter = string.Empty;
                            string fat = string.Empty;
                            string snf = string.Empty;
                            string clr = string.Empty;
                            string rate = string.Empty;
                            string netamount = string.Empty;
                            double ltr;
                            double kg;
                            string sess1 = string.Empty;
                            string litter1 = string.Empty;
                            string fat1 = string.Empty;
                            string snf1 = string.Empty;
                            string clr1 = string.Empty;
                            string rate1 = string.Empty;
                            string netamount1 = string.Empty;
                            double ltr1;
                            double kg1;

                            no = dt.Rows[i]["phone_Number"].ToString();

                            // no = "8825738248";



                            agentid = dt.Rows[i]["Agent_Id"].ToString();
                            TextBox3.Text = agentid.ToString();

                            sess = dt.Rows[i]["Ases"].ToString();
                            litter = dt.Rows[i]["Aliter"].ToString();

                            ltr = Convert.ToDouble(litter);//kg convertion
                            kg = ltr * 1.03;
                            string kgs = kg.ToString("F2");

                            fat = dt.Rows[i]["AFAT"].ToString();
                            snf = dt.Rows[i]["ASNF"].ToString();
                            clr = dt.Rows[i]["ACLR"].ToString();
                            rate = dt.Rows[i]["ARate"].ToString();
                            netamount = dt.Rows[i]["ANetAmount"].ToString();

                            sess1 = dt.Rows[i]["Pses"].ToString();
                            litter1 = dt.Rows[i]["Pliter"].ToString();

                            ltr1 = Convert.ToDouble(litter1);
                            kg1 = ltr1 * 1.03;
                            string kgs1 = kg1.ToString("F2");

                            fat1 = dt.Rows[i]["PFAT"].ToString();
                            snf1 = dt.Rows[i]["PSNF"].ToString();
                            clr1 = dt.Rows[i]["PCLR"].ToString();
                            rate1 = dt.Rows[i]["PRate"].ToString();
                            netamount1 = dt.Rows[i]["PNetAmount"].ToString();
                            txt_FromDate.Text = dti.ToString("dd/MM/yyy");
                            if (Chk_Rate.Checked == true)
                            {
                                message = "\n VYSHNAVI- " + pname + "\n Date:" + txt_FromDate.Text + "_" + sess + "\n Ltr:" + litter + "\n Fat:" + fat + "\n Snf:" + snf + "\n Clr:" + clr + "\n Rate:" + rate + "\n ***" + "_" + sess1 + "\n Ltr:" + litter1 + "\n Fat:" + fat1 + "\n Snf:" + snf1 + "\n Clr:" + clr1 + "\n Rate:" + rate1;
                                message1 = "\n VYSHNAVI- " + pname + "\n Date:" + txt_FromDate.Text + "_" + sess + "\n Id:" + agentid + "\n Ltr:" + litter + "\n Fat:" + fat + "\n Snf:" + snf + "\n Clr:" + clr + "\n Rate:" + rate + "\n ***" + "_" + sess1 + "\n Ltr:" + litter1 + "\n Fat:" + fat1 + "\n Snf:" + snf1 + "\n Clr:" + clr1 + "\n Rate:" + rate1;
                                messagesave = "\n VYSHNAVI- " + pname + "Date:" + txt_FromDate.Text + "_" + sess + "Id:" + agentid + "\nLtr:" + litter + "Fat:" + fat + "Snf:" + snf + "Clr:" + clr + "Rate:" + rate + "\n***" + "_" + sess1 + "Ltr:" + litter1 + "Fat:" + fat1 + "Snf:" + snf1 + "Clr:" + clr1 + "Rate:" + rate1;
                            }
                            else
                            {
                                message = "\n VYSHNAVI- " + pname + "\n Date:" + txt_FromDate.Text + "_" + sess + "\n Ltr:" + litter + "\n Fat:" + fat + "\n Snf:" + snf + "\n Clr:" + clr + "\n ***" + "_" + sess1 + "\n Ltr:" + litter1 + "\n Fat:" + fat1 + "\n Snf:" + snf1 + "\n Clr:" + clr1;
                                message1 = "\n  VYSHNAVI- " + pname + "\n Date:" + txt_FromDate.Text + "_" + sess + "\n Id:" + agentid + "\n Ltr:" + litter + "\n Kg:" + kgs + "\n Fat:" + fat + "\n Snf:" + snf + "\n Clr:" + clr + "\n ***" + "_" + sess1 + "\n Ltr:" + litter1 + "\n Kg:" + kgs1 + "\n Fat:" + fat1 + "\n Snf:" + snf1 + "\n Clr:" + clr1;
                                messagesave = " VYSHNAVI- " + pname + "Date:" + txt_FromDate.Text + "_" + sess + "Id:" + agentid + "Ltr:" + litter + "Kg:" + kgs + "Fat:" + fat + "Snf:" + snf + "Clr:" + clr + "\n***" + "_" + sess1 + "Ltr:" + litter1 + "Kg:" + kgs1 + "Fat:" + fat1 + "Snf:" + snf1 + "Clr:" + clr1;
                            }
                            string strUrl = " http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=vyshnavi@123&from=VYSNVI&to=" + no + "&msg=" + message1 + "&type=1 ";
                            WebRequest request = HttpWebRequest.Create(strUrl);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream s = (Stream)response.GetResponseStream();
                            StreamReader readStream = new StreamReader(s);
                            string dataString = readStream.ReadToEnd();
                            response.Close();
                            s.Close();
                            readStream.Close();
                            con.Close();
                            using (con = dbaccess.GetConnection())
                            {
                                txt_FromDate.Text = dti.ToString("MM/dd/yyy");
                                dti3 = System.DateTime.Now;
                                dti1 = String.Format("{0:MM/dd/yyyy hh:mm.ss}", dti3);
                                string str1 = "update procurement set status=1 where Company_code='" + cmpcode + "' and Plant_Code='" + pcode + "' and  prdate between '" + txt_FromDate.Text + "' and '" + txt_FromDate.Text + "' and  agent_Id='" + TextBox3.Text + "' ";
                                string str2 = "INSERT INTO Message_Histroy (Company_code,Plant_code,Send_date,Send_datetime,Agent_mobileNo,Message) VALUES ('" + cmpcode + "','" + pcode + "','" + txt_FromDate.Text + "','" + dti1 + "','" + no + "','" + messagesave + "')";
                                cmd1.CommandText = str1;
                                cmd1.Connection = con;
                                cmd1.ExecuteNonQuery();
                                cmd.CommandText = str2;
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                                j++;
                                txt_currentsms.Text = j.ToString();
                            }
                        }
                    }
                    uscMsgBox1.AddMessage("Message Send Successfully", MessageBoxUsc_Message.enmMessageType.Success);
                    lblmsg.Text = "Message Send Successfully";
                }
                else
                {
                    lblmsg.Text = "Please Check The Message Balance";
                }
            }
            else
            {
                txt_currentsms.Text = "0";
                uscMsgBox1.AddMessage("No Procurement data", MessageBoxUsc_Message.enmMessageType.Success);
            }
            txt_FromDate.Text = dti.ToString("dd/MM/yyy");
        }
        catch (Exception ex)
        {
            txt_currentsms.Text = j.ToString();
            uscMsgBox1.AddMessage(ex.ToString(), MessageBoxUsc_Message.enmMessageType.Success);
        }

    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(cmpcode.ToString());
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

    private void LoadPlantcode1()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode1(cmpcode.ToString(),pcode);
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
    }
    protected void btn_smsstsus_Click(object sender, EventArgs e)
    {
        txt_currentsms.Text = "";
        Smsstatus();
    }
    private void Smsstatus()
    {
        DataTable dt = new DataTable();
        int c = 0;
        try
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_Smsstatus]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", cmpcode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                c = dt.Rows.Count;
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
        catch (Exception ex)
        {
           
        }
    }
}