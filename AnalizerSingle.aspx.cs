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
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Globalization;
public partial class AnalizerSingle : System.Web.UI.Page
{
    public string rtbText;
    public string Weightdata;
    public int splitflag = 0;
    string centreid;
    int countuser;
    public static string txtboxagent_id, txtboxagent_name, txtboxweightdata1, txtboxweightdata2;
    //   double rate = 0.0, fat = 0.0, snf = 0.0, amount = 0.0;
    string agentid, sampleno, promilktype1;
    SqlDataReader dr;
    double clr, qty, rcamnt, comrate, comamnt, bonus, bonamnt, comper, drate, amount, prod_comm,tablemilkkg;
    double rate, snf,fat;
    double milkkg, milkltr;
    //   string sno = "0";
    public static string flag;
    public string portname;
    public string baudrate;
    string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlCommand sqlcmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    DataTable dt = new DataTable();
    BOLProcurement procurementBO = new BOLProcurement();
    BLLProcurement procurementBL = new BLLProcurement();
    DALProcurement procurementDA = new DALProcurement();
    BLLAgentmaster agentBL = new BLLAgentmaster();
    DbHelper DBclass = new DbHelper();
    BLLPortsetting portBL = new BLLPortsetting();
    protected System.Timers.Timer tmr_analizer;
    public bool IsPageRefresh;
    string prochartname = string.Empty;

    public string companycode, sess, cudate;
    public string plantcode;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        DateTime today = DateTime.Today; // As DateTime
        dtp_DateTime.Text = today.ToString("MM/dd/yyyy");
        cudate = dtp_DateTime.Text.Trim();
        if (!IsPostBack)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
             //   uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();

                loadport();
                cmb_session.Text = DateTime.Now.ToString("tt");
                sess = cmb_session.Text;
                if (auto.Checked == true)
                {
                    txt_fat.Enabled = false;
                    txt_snf.Enabled = false;
                }
                GetID();
                GetCurrentUserCount();
                Weightdata = inpHide.Value;
                loadsampleno();

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
         //       uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();

                loadport();
                cmb_session.Text = DateTime.Now.ToString("tt");
                sess = cmb_session.Text;
                if (auto.Checked == true)
                {
                    txt_fat.Enabled = false;
                    txt_snf.Enabled = false;
                }
                GetID();
                GetCurrentUserCount();
                Weightdata = inpHide.Value;
                loadsampleno();

            }
            else
            {
                
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    private void loadport()
    {
        SqlDataReader dr;
        dr = portBL.getportdatareader(companycode,plantcode, "Analyzer");

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
    public int UserStatus()
    {

        if (string.IsNullOrEmpty(txt_fat.Text))
        {
            return 1;

        }
        else
        {


            //procurementBO.fat = Convert.ToDouble(txt_fat.Text);
            //procurementBO.snf = Convert.ToDouble(txt_snf.Text);
            //procurementBO.rate = drate;
            //procurementBO.amount = damount;

            //procurementBO.Netrate = dcomrate + drate;
            //procurementBO.Clr = Convert.ToDouble(txt_clr.Text);
            //procurementBO.FatKg = milkkg / 100 * Convert.ToDouble(txt_fat.Text);
            //procurementBO.SnfKg = milkkg / 100 * Convert.ToDouble(txt_snf.Text);
            //procurementBO.NetAmount = (dcomrate + drate) * dqty + prod_comm;
            //procurementBO.sample_No = cmb_sampleno.Text;

            ////procurementBL.insertprocurement(procurementBO);
            //procurementBL.setprocurementdata(procurementBO);
            //loadgrid();
            //loadagentid();


           
        }
        return 0;

    }
    private void loadgrid()
    {
        //string uid = Session["User_ID"].ToString();
        //string sqlstr = "SELECT fat as FAT ,snf as SNF,sample_No FROM WeightDemoTable WHERE WDate='" + dtp_DateTime.Text + "'AND session='" + cmb_session.Text + "' and Centre_ID='" + uid + "' ";
        //DataTable dt = new DataTable();
        //dt = procurementDA.GetDatatable(sqlstr);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();

    }
    private void loadagentid()
    {
        //GetCurrentUserCount();
        //GetID();
        //SqlDataReader dr;
        //dr = agentBL.getagentmasterdatareader1(centreid);
        ////dr = routmasterBL.getroutmasterdatareader1(cmb_stateId.Text);
        //txtAgentId.Items.Clear();
        //txtAgentName.Items.Clear();
        //while (dr.Read())
        //{
        //    txtAgentId.Items.Add(dr["Agent_ID"].ToString().Trim());
        //    txtAgentName.Items.Add(dr["Agent_Name"].ToString().Trim());
        //}
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
    private void loadsampleno()
    {

        dr = procurementBL.sessionsamples(companycode, plantcode, dtp_DateTime.Text, cmb_session.Text);
        if (dr.HasRows)
        {
            cmb_sampleno.Items.Clear();
            while (dr.Read())
            {
                cmb_sampleno.Items.Add(dr["sampleno"].ToString().Trim());
            }
            if (cmb_sampleno.Items.Count >= 1)
                cmb_sampleno.SelectedIndex = 0;
        }
        else
        {
            cmb_sampleno.Items.Clear();
            cmb_sampleno.Text = "No Sample.";
        }
    }
    string delid, samp;
    int promilktype, cmbsampleFlag;
    private void loadvalues()
    {
        dr = procurementBL.getvaluebysampleno(cmb_sampleno.Text, companycode, plantcode);
        while (dr.Read())
        {
            // cmb_ratechart.SelectedIndex = cmb_ratechart.Items.IndexOf(dr["rate_chart"]);
            prochartname = dr["Ratechart_Id"].ToString();
            agentid = dr["Agent_id"].ToString();
            milkltr = Convert.ToDouble(dr["Milk_ltr"]);
            qty = milkltr;
            milkkg = Convert.ToDouble(dr["Milk_kg"]);
            //prod_comm = Convert.ToDouble(dr["commission"]);
            promilktype = Convert.ToInt32(dr["Milk_Nature"]);
            if (promilktype == 1)
            {
                promilktype1 = "Cow";
            }
            else
            {
                promilktype1 = "Buffalo";
            }
        }
        //dr = procurementBL.getvaluebysampleno(cmb_sampleno.Text);
        //while (dr.Read())
        //{
        //    // cmb_ratechart.SelectedIndex = cmb_ratechart.Items.IndexOf(dr["rate_chart"]);
        //    //ratechart = dr["rate_chart"].ToString();
        //    agentid = dr["agent_id"].ToString();
        //    milkltr = Convert.ToDouble(dr["milk_Ltr"]);
        //    dqty = milkltr;
        //    milkkg = Convert.ToDouble(dr["milk_Kg"]);

        //}

    }
    private bool checkratechart(string rc)
    {
        DateTime datenow = new DateTime();

        string tempdate = dtp_DateTime.Text;
        if (!(string.IsNullOrEmpty(tempdate)))
        {
            datenow = DateTime.ParseExact(dtp_DateTime.Text, "dd/MM/yyyy", null);
            return procurementBL.checkratechart(rc, datenow.ToString(),companycode ,plantcode );
        }
        return false;
    }
    private void savedata()
    {
        int result = UserStatus();
        if ((result == 1)&&(auto.Checked == true))
        {
           // uscMsgBox1.AddMessage("Please Lock Value", MessageBoxUsc_Message.enmMessageType.Attention);
                
           
            
        }
        else if ((result == 1) && (manual.Checked == true) && (txt_fat.Text == "") && (txt_snf.Text == ""))
        {
          //  uscMsgBox1.AddMessage("Please Enter Values", MessageBoxUsc_Message.enmMessageType.Attention);
            
        }
        else if (result == 0)
        {

            double minfat, minsnf, bminsnf, bmaxsnf, rangesnf, bminfat, cal2, snfcomamt, bcalc;

            string rosnf1;
            snfcomamt = 0;
            rangesnf = 0;


            bminsnf = 1;
            bmaxsnf = 8;
            bminfat = 1;
            bcalc = 8;


            minfat = 1.0;
            minsnf = 1.0;

            loadvalues();


            if (!(string.IsNullOrEmpty(txt_snf.Text)))
            {
                try
                {
                    if (!(string.IsNullOrEmpty(txt_snf.Text)))
                        snf = Convert.ToDouble(txt_snf.Text);
                    if (!(string.IsNullOrEmpty(txt_fat.Text)))
                        fat = Convert.ToDouble(txt_fat.Text);
                    if ((snf > 0) && (fat > 0))
                        txt_clr.Text = Convert.ToString(((snf - 0.36) - (fat * 0.2)) * 4);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                try
                {
                    if (!(string.IsNullOrEmpty(txt_fat.Text)))
                    {
                        double Netrate = 0;
                        fat = Convert.ToDouble(txt_fat.Text);
                        snf = Convert.ToDouble(txt_snf.Text);

                        //if ((snf > 0) && (fat > 0))
                        //clr = ((snf - 0.36) - (fat * 0.2)) * 4;


                        //if (promilktype == 1)
                        //{

                        //    //COW CAL START
                        //    double rcval = fat + snf;

                        //    if (fat >= minfat && snf >= minsnf)
                        //    {
                        //        double ts = fat + snf;
                        //        if (ts <= 11.9)
                        //        {
                        //            rcval = fat + snf;
                        //        }
                        //        else
                        //        {
                        //            if (ts >= 12 && snf >= 8)
                        //            {
                        //                rcval = fat + snf;
                        //            }
                        //            else
                        //            {
                        //                rcval = fat + snf;
                        //                //rcval = 11.8;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        rcval = 0;
                        //    }


                        //    dr = procurementBL.getratechartdatareader(rcval.ToString(), prochartname);
                        //    while (dr.Read())
                        //    {
                        //        rcamnt = Convert.ToDouble(dr["tsRate"]);
                        //        comrate = Convert.ToDouble(dr["comm_rate"]);
                        //        comper = Convert.ToDouble(dr["comm_per"]);
                        //        bonus = Convert.ToDouble(dr["bonus"]);
                        //    }
                        //    rate = (fat + snf) * rcamnt;
                        //} //COW CAL END


                        if (promilktype == 1)
                        {

                            //COW CAL START
                            double rcval;
                            rcval = 0;

                            if (fat >= minfat && snf >= minsnf)
                            {
                               // rcval = fat + snf;
                                rcval = snf;

                            }
                            else
                            {
                                rcval = 0;
                            }


                            dr = procurementBL.getratechartdatareader(snf.ToString(), prochartname, companycode, plantcode);
                            while (dr.Read())
                            {
                                rcamnt = Convert.ToDouble(dr["Rate"]);
                                comrate = Convert.ToDouble(dr["Comission_Amount"]);
                                //comper = Convert.ToDouble(dr["comm_per"]);
                                bonus = Convert.ToDouble(dr["Bouns_Amount"]);
                            }
                            rate = (fat + snf) * rcamnt;
                        } //COW CAL END



                        else
                        { //BUFF CAL START

                            if (fat >= bminfat && snf >= bminsnf)
                            {
                                double rcval = fat;
                                dr = procurementBL.getratechartdatareader(rcval.ToString(), prochartname.Trim(), companycode, plantcode);
                                while (dr.Read())
                                {
                                    rcamnt = Convert.ToDouble(dr["Rate"]);
                                    comrate = Convert.ToDouble(dr["Comission_Amount"]);
                                    //comper = Convert.ToDouble(dr["comm_per"]);
                                    bonus = Convert.ToDouble(dr["Bouns_Amount"]);
                                }
                                rate = rcval * rcamnt;

                                rangesnf = snf;
                                dr = procurementBL.getbuffsubdata(rangesnf.ToString());
                                while (dr.Read())
                                {
                                    snfcomamt = Convert.ToDouble(dr["rate"]);
                                }
                                if (rangesnf >= bminsnf && rangesnf < bmaxsnf)
                                {
                                    cal2 = (bcalc - snf);
                                    rosnf1 = cal2.ToString("f2");
                                    if (cal2 >= 1)
                                    {
                                        cal2 = (Convert.ToDouble(rosnf1)) * snfcomamt;
                                    }
                                    else
                                    {
                                        cal2 = (Convert.ToDouble(rosnf1) * 10) * snfcomamt;
                                    }


                                    rate = rate - cal2;
                                }
                                else
                                {
                                    if (rangesnf >= bmaxsnf)
                                    {
                                        cal2 = (snf - bcalc);
                                        rosnf1 = cal2.ToString("f2");
                                        if (cal2 >= 1)
                                        {
                                            cal2 = (Convert.ToDouble(rosnf1)) * snfcomamt;
                                        }
                                        else
                                        {
                                            cal2 = (Convert.ToDouble(rosnf1) * 10) * snfcomamt;
                                        }
                                        rate = rate + cal2;
                                    }

                                }
                            }
                            else
                            {
                                rate = 0;
                            }
                        }  //BUFF CAL END    



                        double check = 0.00;
                        double temp1;
                        double prcomm = 0;
                        if (comper > check)
                        {
                            temp1 = 0;
                            temp1 = (rate / 100) * comper;
                            comper = rate + temp1;
                            Netrate = comper;
                            comamnt = temp1 * qty;
                        }
                        if (comrate > check)
                        {
                            temp1 = 0;
                            temp1 = comrate;
                            Netrate = comrate + rate;
                            //comrate = temp1 * qty;
                            comamnt = comrate * qty;
                        }
                        if (bonus > check)
                        {
                            temp1 = 0;
                            temp1 = bonus + rate;
                            //rate = temp1;
                            bonamnt = bonus;
                        }
                        amount = rate * qty;



                        procurementBO.fat = fat;
                        procurementBO.snf = snf;
                        clr = Convert.ToDouble(txt_clr.Text);
                        procurementBO.Clr = clr;
                        procurementBO.rate = rate;
                        procurementBO.amount = amount;
                        procurementBO.Netrate = comrate + rate;

                        tablemilkkg = qty * 1.03;
                        procurementBO.FatKg = tablemilkkg / 100 * fat;
                        procurementBO.SnfKg = tablemilkkg / 100 * snf;

                        if (cmbsampleFlag == 1)
                        {
                            procurementBO.sample_No = samp;
                            cmbsampleFlag = 0;
                        }
                        else
                        {
                            procurementBO.sample_No = cmb_sampleno.Text;
                        }
                        procurementBO.Company_ID = companycode;
                        procurementBO.CentreID = plantcode;



                        //procurementBO.bonus_Amnt = bonamnt;
                        //procurementBO.comm_Amnt = comamnt;


                        //if (comper > comrate)
                        //{
                        //    procurementBO.Commission = comper.ToString();
                        //}
                        //else
                        //{
                        //    procurementBO.Commission = comrate.ToString();
                        //}
                        prcomm = qty * prod_comm;                    
                        //CLR           
                       

                        procurementBO.NetAmount = (comrate + rate) * qty + prcomm;                      
                       
                        //procurementBO.SapuserAnalizer = Program.user_name;

                        procurementBL.setprocurementdata(procurementBO);

                        cmb_sampleno.SelectedIndex = -1;
                        txt_fat.Text = string.Empty;
                        txt_clr.Text = string.Empty;
                        //txt_agentId.Text = string.Empty;
                        txt_snf.Text = string.Empty;
                        gvProducts.DataBind();
                        loadsampleno();
                    }
               //     uscMsgBox1.AddMessage("Saved Successfully", MessageBoxUsc_Message.enmMessageType.Success);
                   
                    
                  
                    txt_fat.Text = " ";
                    txt_snf.Text = " ";
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            txt_fat.Focus();
        }
       
    }
    //public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    //{
    //    if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
    //    {
    //     //   uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_fat.Text + "and "+txt_snf.Text+" as argument.", MessageBoxUsc_Message.enmMessageType.Info);
    //    }
    //    else
    //    {
    //      //  uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
    //    }
    //}
    public void split(string strin)
    {
        Weightdata = inpHide.Value;
        strin = Weightdata;
        //try
        //{
        //    if (!(string.IsNullOrEmpty(strin)))
        //    {

        //        rtbText = strin;
        //        rtbText.Trim();
        //        rtbText= rtbText.Replace("\n", " ");                   
        //        string str = rtbText.Trim();
        //        string[] strarr = new string[10];
        //        strarr = str.Split(' ');
        //        if (strarr[0].ToString()!="00")
        //        {
        //            if (strarr.Length >= 6)
        //            {
        //                txt_Fat.Text = strarr[0] + "." + strarr[1];
        //                txt_SNF.Text = strarr[2] + "." + strarr[3];
        //            }
        //        }
        //        splitflag = 0;
        //        //txt_clr.Focus();
        //        rtb_fatsnf.Text = string.Empty;

        //    }
        //}
        //catch(Exception ex)
        //{
        //    ex.ToString();
        //}
        //===========
        try
        {
            string tfat = string.Empty;
            string tsnf = string.Empty;
            double tfat2 = 0.0;
            double tsnf2 = 0.0;

            double tfat1 = 0.0;
            double tsnf1 = 0.0;
            double diff2 = 0.0;
            tfat1 = 3.00;
            tsnf1 = 0.00;

            if (!(string.IsNullOrEmpty(strin)))
            {

                rtbText = strin;
                rtbText.Trim();
                rtbText = rtbText.Replace("\n", " ");
                string str = rtbText.Trim();
                string[] strarr = new string[10];
                strarr = str.Split(' ');
                if (strarr[0].ToString()!= "00")
                {
                    if (strarr.Length >= 6)
                    {
                        tfat = strarr[0] + "." + strarr[1];
                        tsnf = strarr[2] + "." + strarr[3];
                        tfat2 = Convert.ToDouble(tfat);
                        tsnf2 = Convert.ToDouble(tsnf);
                        diff2 = tsnf2 - tfat2;
                        if (diff2 <= 6.5)
                        {
                            if ((tsnf1 == tsnf2))
                            {
                                //                                       int hh = 0;
                                BtnLock.Focus();
                            }
                            else
                            {
                                //if (tfat1 == tfat2)
                                //{
                                //    int hh1 = 0;
                                //}
                                //else
                                //{
                                txt_fat.Text = strarr[0] + "." + strarr[1];
                                txt_snf.Text = strarr[2] + "." + strarr[3];
                                //}
                            }

                        }
                        else
                        {
                            BtnLock.Focus();
                        }
                    }
                }
                splitflag = 0;
                //txt_clr.Focus();
                //Weightdata = string.Empty;

            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

    }

    protected void BtnLock_Click(object sender, EventArgs e)
    {
        if (auto.Checked == true)
        {
            loadvalues();
            Weightdata = inpHide.Value;
            split(Weightdata);

            savedata();
            prod_comm = 0;
            loadsampleno();
         //   System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "OpenActiveX();", true); 
        }
        else
        {
            loadvalues();
            savedata();
            prod_comm = 0;
            loadsampleno();
           // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "OpenActiveX();", true);
            
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //procurementBO.fat = Convert.ToDouble(txt_fat.Text);
        //procurementBO.snf = Convert.ToDouble(txt_snf.Text);
        //procurementBO.rate = drate;
        //procurementBO.amount = damount;

        //procurementBO.Netrate = dcomrate + drate;
        //procurementBO.Clr = Convert.ToDouble(txt_clr.Text);
        //procurementBO.FatKg = milkkg / 100 * Convert.ToDouble(txt_fat.Text);
        //procurementBO.SnfKg = milkkg / 100 * Convert.ToDouble(txt_snf.Text);
        //procurementBO.NetAmount = (dcomrate + drate) * dqty + prod_comm;
        //procurementBO.sample_No = cmb_sampleno.Text;


        //procurementBL.setprocurementdata(procurementBO);
        //loadgrid();




        //loadsampleno();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void auto_CheckedChanged(object sender, EventArgs e)
    {
        txt_fat.Enabled = false;
        txt_snf.Enabled = false;
        manual.Checked = false;
       
    }
    protected void manual_CheckedChanged(object sender, EventArgs e)
    {
        txt_fat.Enabled =  true;
        txt_snf.Enabled = true;
        auto.Checked = false;
        
    }

    protected void txt_snf_TextChanged(object sender, EventArgs e)
    {

    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
