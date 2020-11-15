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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;

public partial class BankPaymentAllotment : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;

    public int rid;
    public int flag = 0;
    public int flag1 = 0;

    SqlDataReader dr;
    BLLuser Bllusers = new BLLuser();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    DateTime dtm = new DateTime();
    DbHelper dbaccess = new DbHelper();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLProcureimport Bllproimp = new BLLProcureimport();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    SqlConnection con=new SqlConnection();
    public static int roleid;

    DataTable dttt = new DataTable();
    DataTable dtttq = new DataTable();

    double getamt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                LoadPlantcode();
                GetPlantAllottedAmount();
                Bdate();
                loadrouteid();
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                btn_save.Visible = false;
                // Load BankName
                LoadBankId();
                // OldFileName
                chk_OldFileName.Checked = true;
                if (chk_OldFileName.Checked == true)
                {
                    ddl_oldfilename.Visible = true;
                    txt_FileName.Visible = false;
                    txt_FileName.Text = "ddl";
                    Loadoldfilename();
                }
                else
                {
                    ddl_oldfilename.Visible = false;
                    txt_FileName.Visible = true;
                    txt_FileName.Text = " ";
                }
                AllRoute();
                lbl_ErrorMsg.Visible = false;
                //
                Chk_Cash.Visible = false;
                ddl_BankName.Visible = false;
                //lbl_RouteName.Visible = false;
                //ddl_RouteName.Visible = false;
                //chk_Allroute.Visible = false;
                lbl_RouteName.Visible = true;
                ddl_RouteName.Visible = true;
                chk_Allroute.Visible = true;
                txt_assignamt1.Text = "";
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
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
             //   managmobNo = Session["managmobNo"].ToString();

                pcode = ddl_Plantcode.SelectedItem.Value;
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                // BindBankPaymentData();
                if (flag == 1)
                {
                    btn_save.Visible = true;
                }
                else
                {
                    btn_save.Visible = false;
                }
                lbl_ErrorMsg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

            txt_assignamt1.Text = "";
        }
    }
    private void GetPlantAllottedAmount()
    {
        try
        {
            pcode = ddl_Plantcode.SelectedItem.Value;
            SqlDataReader dr = null;
            dr = Bllusers.GetPlantAllottedAmount(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_Allotamount.Text = dr["TotAmount"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void btn_load_Click(object sender, EventArgs e)
    {
        //rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        //BindBankPaymentData();
        //txt_balance.Text = "";
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        BindBank();
        txt_balance.Text = "";
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
            // else
            //{
            //    ddl_Plantname.Items.Add("--Select PlantName--");
            //}
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    private void loadrouteid()
    {
        SqlDataReader dr;
        dr = routmasterBL.getroutmasterdatareader(ccode, pcode);
        ddl_RouteName.Items.Clear();
        ddl_RouteID.Items.Clear();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddl_RouteName.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
                ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());
            }
        }
        // else
        //{
        //    ddl_Plantname.Items.Add("--Select PlantName--");
        //}
    }

    private void Bdate()
    {
        try
        {
            dr = null;
            dr = Billdate(ccode, ddl_Plantcode.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");
                }
            }
            else
            {
                txt_FromDate.Text = "10/10/1983";
                txt_ToDate.Text = "10/10/1983";
                WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }

    public SqlDataReader Billdate(string ccode, string pcode)
    {
        SqlDataReader dr;
        string sqlstr = string.Empty;
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND CurrentPaymentFlag='1' ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Allotamount.Text = "";
        txt_balance.Text = "";
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        GetPlantAllottedAmount();
        LoadBankId();
        loadrouteid();
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        Bdate();

       // BindBankPaymentData();
        BindBank();
        Loadoldfilename();
        GETTOTBILLAMOUNT();
        GETbankassignamount();

   //     getassignedamt();


        string temptotamt = ViewState["txttotbankbillamount"].ToString();
        double tempbillamt = Convert.ToDouble(temptotamt);
        double Tempvar = Convert.ToDouble(ViewState["txtassignamt"]);
        double getpendingamt = (tempbillamt - Tempvar);
        txt_balance.Text = getpendingamt.ToString("f2");
     

       // txt_balance.Text = "";


    }
    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        //BindBankPaymentData();
        BindBank();
    }


    private void BindBankPaymentData()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);


            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            ds = LoadBankPaymentDatas(ccode, pcode, d1, d2, rid.ToString(), ddl_BankName.SelectedItem.Value.Trim());
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Agent_Name";
                CheckBoxList1.DataValueField = "proAid";
                CheckBoxList1.DataBind();

                CheckBoxList2.DataSource = ds;
                CheckBoxList2.DataTextField = "Netpay";
                CheckBoxList2.DataValueField = "Netpay";
                CheckBoxList2.DataBind();

            }
            else
            {
                WebMsgBox.Show("Agents Not Available in this Selection...");
            }
        }
        catch (Exception ex)
        {

        }
    }   

    private void CheckData()
    {
        try
        {
            double orgamount = 0.0;
            orgamount = Convert.ToDouble(txt_Allotamount.Text);
            if (orgamount > 0)
            {

                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
                double TotalSelectAmount = 0.0;
                double TotalSelectAmount1 = 0.0;
                double TotalSelectAmount2 = 0.0;



                string d1 = dt1.ToString("MM/dd/yyyy");
                string d2 = dt2.ToString("MM/dd/yyyy");
                int count = 0;


               // dt =LoadBankPaymentDatas1(ccode, pcode, d1, d2, rid.ToString(),ddl_BankName.SelectedItem.Value.Trim());
                dt = LoadBank2(ccode, pcode, d1, d2, rid.ToString(), ddl_BankName.SelectedItem.Value.Trim());
                count = dt.Rows.Count;
                if (count > 0)
                {
                    DataTable custDT = new DataTable();
                    DataColumn col = null;

                    col = new DataColumn("Agent_id");
                    custDT.Columns.Add(col);
                    col = new DataColumn("NetPay");
                    custDT.Columns.Add(col);


                    for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                    {
                        DataRow dr = null;
                        dr = custDT.NewRow();


                        if (CheckBoxList2.Items[i].Selected == true)
                        {
                            dr[1] = CheckBoxList2.Items[i].Value;
                            TotalSelectAmount1 = Convert.ToDouble(CheckBoxList2.Items[i].Value);
                            TotalSelectAmount = TotalSelectAmount1 + TotalSelectAmount;

                            getamt = getamt + TotalSelectAmount1;
                            if (getamt > 0)
                            {
                                txt_assignamt1.Text = (getamt).ToString("f2");

                            }
                            else
                            {
                                txt_assignamt1.Text = "0";

                            }
                            flag1 = 1;
                        }
                        custDT.Rows.Add(dr);
                    }
                    TotalSelectAmount2 = Convert.ToDouble(txt_Allotamount.Text) - TotalSelectAmount;
                    if (TotalSelectAmount2 >= 0)
                    {
                        txt_balance.Text = (Convert.ToDouble(txt_Allotamount.Text) - TotalSelectAmount).ToString();
                        flag = 1;
                        if (flag1 == 1)
                        {
                            flag1 = 0;
                            btn_save.Visible = true;
                        }
                        else
                        {
                            flag1 = 0;
                            btn_save.Visible = false;
                        }
                        // WebMsgBox.Show("Please Select the Agents for Remaining Amounts...");
                    }
                    else
                    {
                        txt_Allotamount.Text = orgamount.ToString();
                        flag = 0;
                        btn_save.Visible = false;
                        // WebMsgBox.Show("Please Select the Agents for Givent Amounts Only...");
                    }

                }
            }
            else
            {
                WebMsgBox.Show("Please Check the Allotment Amounts...");
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void SaveCheckData()
    {
        try
        {
            pcode = ddl_Plantcode.SelectedItem.Value;
            double orgamount = 0.0;
            orgamount = Convert.ToDouble(txt_Allotamount.Text);
            if (orgamount > 0)
            {

                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
                double TotalSelectAmount = 0.0;
                double TotalSelectAmount1 = 0.0;
                double TotalSelectAmount2 = 0.0;



                string d1 = dt1.ToString("MM/dd/yyyy");
                string d2 = dt2.ToString("MM/dd/yyyy");
                int count = 0;


              //  dt = LoadBankPaymentDatas1(ccode, pcode, d1, d2, rid.ToString(), ddl_BankName.SelectedItem.Value.Trim());
                dt = LoadBank2(ccode, pcode, d1, d2, rid.ToString(), ddl_BankName.SelectedItem.Value.Trim());
                count = dt.Rows.Count;
                if (count > 0)
                {
                    DataTable custDT = new DataTable();
                    DataColumn col = null;

                    col = new DataColumn("Agent_id");
                    custDT.Columns.Add(col);
                    col = new DataColumn("NetPay");
                    custDT.Columns.Add(col);


                    for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                    {
                        DataRow dr = null;
                        dr = custDT.NewRow();


                        if (CheckBoxList2.Items[i].Selected == true)
                        {
                            dr[0] = CheckBoxList1.Items[i].Value;
                            dr[1] = CheckBoxList2.Items[i].Value;
                            TotalSelectAmount1 = Convert.ToDouble(CheckBoxList2.Items[i].Value);
                            TotalSelectAmount = TotalSelectAmount1 + TotalSelectAmount;
                            flag1 = 1;
                            custDT.Rows.Add(dr);
                        }


                    }
                    TotalSelectAmount2 = Convert.ToDouble(txt_Allotamount.Text) - TotalSelectAmount;
                    if (TotalSelectAmount2 >= 0)
                    {
                        txt_balance.Text = (Convert.ToDouble(txt_Allotamount.Text) - TotalSelectAmount).ToString();
                        flag = 1;
                        if (flag1 == 1)
                        {
                            flag1 = 0;
                            btn_save.Visible = true;
                        }
                        else
                        {
                            flag1 = 0;
                            btn_save.Visible = false;
                        }
                        // WebMsgBox.Show("Please Select the Agents for Remaining Amounts...");
                    }
                    else
                    {
                        txt_Allotamount.Text = orgamount.ToString();
                        flag = 0;
                        btn_save.Visible = false;
                        // WebMsgBox.Show("Please Select the Agents for Givent Amounts Only...");
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
                        SqlCommand sqlCmd = new SqlCommand("dbo.[Insert_BankPaymentAllotmentDetails]");
                        conn.Open();
                        sqlCmd.CommandTimeout = 300;
                        sqlCmd.Connection = conn;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(param);
                        sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                        sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                        sqlCmd.Parameters.AddWithValue("@spfrmdate", d1);
                        sqlCmd.Parameters.AddWithValue("@sptodate", d2);
                        if (chk_OldFileName.Checked == true)
                        {
                            sqlCmd.Parameters.AddWithValue("@spFileName", ddl_oldfilename.SelectedItem.Text.Trim());
                        }
                        else
                        {
                            sqlCmd.Parameters.AddWithValue("@spFileName", txt_FileName.Text.Trim());
                        }

                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Please Check the Allotment Amounts...");
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void MChk_Menu1_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }
    protected void MChk_Menu2_CheckedChanged(object sender, EventArgs e)
    {
        Menu2();
    }
    private void Menu1()
    {
        if (MChk_Menu1.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
        }
    }

    private void Menu2()
    {
        if (MChk_Menu2.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList2.Items.Count - 1; i++)
            {
                CheckBoxList2.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList2.Items.Count - 1; i++)
            {
                CheckBoxList2.Items[i].Selected = false;
            }
        }
    }

    protected void btn_Check_Click(object sender, EventArgs e)
    {

        CheckData();
    }


    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            //Check the Same FileName Avail or Not
            int coun = FileNamecheckstatus();
            if (coun == 0)
            {
                //Check the File Status Closed or Not
                int counts = Saveoldfilenamecheckstatus();
                if (counts == 0)
                {
                    SaveCheckData();
                    //flag set
                    flag = 0;
                    btn_save.Visible = false;
                    rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                    // BindBankPaymentData();
                    BindBank();
                    GetPlantAllottedAmount();
                    txt_balance.Text = "";
                    MChk_Menu2.Checked = false;
                    if (ddl_oldfilename.Items.Count > 0)
                    {
                        lbl_ErrorMsg.Visible = true;
                        lbl_ErrorMsg.Text = "Payment File Updated...";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txt_FileName.Text))
                        {
                            lbl_ErrorMsg.Visible = true;
                            lbl_ErrorMsg.Text = "Payment File Updated...";
                        }
                        else
                        {
                            lbl_ErrorMsg.Visible = true;
                            lbl_ErrorMsg.Text = "Please Check the FileName...";
                        }
                    }
                }
                else
                {
                    lbl_ErrorMsg.Visible = true;
                    lbl_ErrorMsg.Text = "File Status Closed...";
                }

            }
            else
            {
                lbl_ErrorMsg.Visible = true;
                lbl_ErrorMsg.Text = "FileName Already Exists...";
            }
        }
        catch (Exception ex)
        {
            lbl_ErrorMsg.Visible = true;
            lbl_ErrorMsg.Text = ex.ToString().Trim();
        }
    }


    protected void chk_OldFileName_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_OldFileName.Checked == true)
        {
            ddl_oldfilename.Visible = true;
            txt_FileName.Visible = false;
            txt_FileName.Text = "ddl";
            Loadoldfilename();
        }
        else
        {
            ddl_oldfilename.Visible = false;
            txt_FileName.Visible = true;
            txt_FileName.Text = " ";
        }
    }

    private void Loadoldfilename()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            ddl_oldfilename.Items.Clear();

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string str = "SELECT DISTINCT(UPPER(BankFileName)) AS BankFileName  FROM BankPaymentllotment Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "' order by BankFileName";
            ds = dbaccess.GetDataset(str);
            if (ds != null)
            {
                ddl_oldfilename.DataSource = ds;
                ddl_oldfilename.DataTextField = "BankFileName";
                ddl_oldfilename.DataValueField = "BankFileName";
                ddl_oldfilename.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }


    public void GETTOTBILLAMOUNT()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            ddl_oldfilename.Items.Clear();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            con = dbaccess.GetConnection();
            string STTY = "";
            STTY = "select totnetamount,banknetamount,cashnetamount    from ( select totnetamount,banknetamount,totplantcode  from (SELECT ISNULL(SUM(NetAmount),0) as totnetamount,Plant_code as totplantcode FROM Paymentdata Where Plant_code='" + pcode + "' AND   Frm_date='" + d1 + "' AND To_date='" + d2 + "'  group by Plant_code   ) as tot left join (SELECT  ISNULL(SUM(NetAmount),0) as banknetamount,Plant_code as bankplant FROM Paymentdata Where Plant_code='" + pcode + "' AND   Frm_date='" + d1 + "' AND To_date='" + d2 + "' and Payment_mode='bank' group by Plant_code ) as bank on tot.totplantcode=bank.bankplant) as totbank left join (SELECT  ISNULL(SUM(NetAmount),0) as cashnetamount,plant_code as cashplantcode FROM Paymentdata Where Plant_code='" + pcode + "' AND   Frm_date='" + d1 + "' AND To_date='" + d2 + "' and Payment_mode='CASH'  group by Plant_code) as cash on totbank.totplantcode=cash.cashplantcode";
            SqlCommand cmd = new SqlCommand(STTY, con);
            DataTable dttt = new DataTable();
            SqlDataAdapter dss = new SqlDataAdapter(cmd);
             dss.Fill(dttt);
            txt_totbillamount.Text =  dttt.Rows[0][0].ToString();
            txt_bank.Text          =  dttt.Rows[0][1].ToString();
            txt_cash.Text          =  dttt.Rows[0][2].ToString();


            ViewState["txttotbankbillamount"] = txt_bank.Text.ToString();

        }
        catch
        {

            txt_cash.Text = "0";

        }

    }





    public void GETbankassignamount()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            ddl_oldfilename.Items.Clear();
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            con = dbaccess.GetConnection();
            string STTYq = "";
            STTYq ="SELECT  SUM(NetAmount) AS NATAMT    FROM  BankPaymentllotment    WHERE   Plant_code='" + pcode + "'    AND Billfrmdate='"+d1+"' AND Billtodate='"+d2+"'";
            SqlCommand cmdq = new SqlCommand(STTYq, con);
            DataTable dtttq = new DataTable();
            SqlDataAdapter dssq = new SqlDataAdapter(cmdq);
            dssq.Fill(dtttq);
           // txt_assignamt.Text = dtttq.Rows[0][0].ToString("F2");
            double Tempvar =Convert.ToDouble(dtttq.Rows[0][0]);
            txt_assignamt.Text = Tempvar.ToString("f2");
            ViewState["txtassignamt"] = txt_assignamt.Text;
        }
        catch
        {



        }

    }


    private void LoadBankId()
    {
        try
        {
            DataSet ds = null;
            //string sqlstr = "SELECT Bank_ID,UPPER(CONVERT(Nvarchar(8),Bank_ID)+'_'+Bank_Name) AS Bank_Name  FROM Bank_Details WHERE Company_code='" + ccode + "'  ORDER BY Bank_ID";
            string sqlstr = "SELECT Distinct(Bank_Id),Bank_Name FROM (SELECT Bank_ID,UPPER(CONVERT(Nvarchar(8),Bank_ID)+'_'+Bank_Name) AS Bank_Name  FROM Bank_Details WHERE Company_code='" + ccode + "' ) AS BD INNER JOIN (SELECT Bank_Id AS Bid FROM Agent_Master Where Plant_code='" + pcode + "' ) AS AM ON BD.Bank_id=AM.Bid ORDER BY BD.Bank_id";
            ds = dbaccess.GetDataset(sqlstr);
            ddl_BankName.Items.Clear();
            ddchkCountry.Items.Clear();
            if (ds != null)
            {
                ddl_BankName.DataSource = ds;
                ddl_BankName.DataTextField = "Bank_Name";
                ddl_BankName.DataValueField = "Bank_ID";
                ddl_BankName.DataBind();

               
                ddchkCountry.DataSource = ds;
                ddchkCountry.DataTextField = "Bank_Name";
                ddchkCountry.DataValueField = "Bank_ID";
                ddchkCountry.DataBind();
              //  ddchkCountry.Items.Add("0");
                ddchkCountry.Items.Add(new ListItem("CASH".ToString(), "0".ToString()));
                
            }
            if (ddl_BankName.Items.Count > 0)
            {
                //int cou = ddl_BankName.Items.Count;
                //cou = cou + 1;
                //ddl_BankName.Items[cou].Text = "CASH";
                //ddl_BankName.Items[cou].Value = "0";
                ddl_BankName.SelectedIndex = 0;
            }

        }
        catch (Exception em)
        {

        }
    }

    

    public DataSet LoadBankPaymentDatas(string ccode, string pcode, string dt1, string dt2, string rid, string bankid)
    {
        string sqlstr = string.Empty;
        // sqlstr = "SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
        //1-8-2015 11.30 AM sqlstr = "SELECT * FROM(Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp RIGHT JOIN  (SELECT * FROM(SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,CONVERT(NVARCHAR(15),(CONVERT(Nvarchar(10),F.proAid)+'_'+F.Agent_Name)) AS Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F )AS F1) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL   ORDER BY F2.proAid  ";
        // 17-02-2015  sqlstr = "SELECT * FROM " +
        // " (SELECT * FROM(Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp RIGHT JOIN  (SELECT * FROM(SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,CONVERT(NVARCHAR(15),(CONVERT(Nvarchar(10),F.proAid)+'_'+F.Agent_Name)) AS Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F )AS F1) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL) AS  final1 " +
        // " INNER JOIN " +
        // " (SELECT agent_id FROM Agent_Master Where Company_Code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "'  AND Bank_Id='" + bankid + "') AS agent  ON final1.proAid=agent.Agent_Id ORDER BY final1.proAid";
        if (Chk_Cash.Checked == true)
        {
            sqlstr = "SELECT * FROM " +
    " (Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp " +
    " RIGHT JOIN " +
    " (SELECT *FROM " +
    " (SELECT cart.ARid AS Rid,cart.cartAid AS proAid,(CONVERT(nvarchar(15),(CONVERT(nvarchar(9),cart.cartAid)+'_'+ cart.Agent_Name))) AS Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS Netpay,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM(SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg FROM Procurement WHERE prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "'  AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  RAgent_id AS DAid ,(CAST((SUM(RBilladvance)) AS DECIMAL(18,2))) AS Billadv,(CAST((SUM(RAi)) AS DECIMAL(18,2))) AS Ai,(CAST((SUM(RFeed)) AS DECIMAL(18,2))) AS Feed,(CAST((SUM(Rcan)) AS DECIMAL(18,2))) AS can,(CAST((SUM(RRecovery)) AS DECIMAL(18,2))) AS Recovery,(CAST((SUM(Rothers)) AS DECIMAL(18,2))) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND RCompany_Code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY RAgent_id) AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "'  AND Clearing_Date BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_Id='0') AS cart ON prdelo.SproAid=cart.cartAid ) AS FF  " +
    " LEFT JOIN  " +
    " (SELECT Route_id AS RRid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS Rout ON FF.Rid=Rout.RRid) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL ORDER BY F2.Rid,bp.Agent_Id ";
        }

        if (chk_Allroute.Checked == true)
        {
            sqlstr = "SELECT * FROM " +
      " (Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp " +
      " RIGHT JOIN " +
      " (SELECT *FROM " +
      " (SELECT cart.ARid AS Rid,cart.cartAid AS proAid,(CONVERT(nvarchar(15),(CONVERT(nvarchar(9),cart.cartAid)+'_'+ cart.Agent_Name))) AS Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS Netpay,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM(SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg FROM Procurement WHERE prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "'  AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  RAgent_id AS DAid ,(CAST((SUM(RBilladvance)) AS DECIMAL(18,2))) AS Billadv,(CAST((SUM(RAi)) AS DECIMAL(18,2))) AS Ai,(CAST((SUM(RFeed)) AS DECIMAL(18,2))) AS Feed,(CAST((SUM(Rcan)) AS DECIMAL(18,2))) AS can,(CAST((SUM(RRecovery)) AS DECIMAL(18,2))) AS Recovery,(CAST((SUM(Rothers)) AS DECIMAL(18,2))) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND RCompany_Code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY RAgent_id) AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "'  AND Clearing_Date BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_Id='" + ddl_BankName.SelectedItem.Value.Trim() + "') AS cart ON prdelo.SproAid=cart.cartAid ) AS FF  " +
      " LEFT JOIN  " +
      " (SELECT Route_id AS RRid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS Rout ON FF.Rid=Rout.RRid) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL ORDER BY F2.Rid,bp.Agent_Id ";
        }
        else
        {
            sqlstr = "SELECT * FROM " +
     " (Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp " +
     " RIGHT JOIN " +
     " (SELECT *FROM " +
     " (SELECT cart.ARid AS Rid,cart.cartAid AS proAid,(CONVERT(nvarchar(15),(CONVERT(nvarchar(9),cart.cartAid)+'_'+ cart.Agent_Name))) AS Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS Netpay,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM(SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg FROM Procurement WHERE prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "'  AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  RAgent_id AS DAid ,(CAST((SUM(RBilladvance)) AS DECIMAL(18,2))) AS Billadv,(CAST((SUM(RAi)) AS DECIMAL(18,2))) AS Ai,(CAST((SUM(RFeed)) AS DECIMAL(18,2))) AS Feed,(CAST((SUM(Rcan)) AS DECIMAL(18,2))) AS can,(CAST((SUM(RRecovery)) AS DECIMAL(18,2))) AS Recovery,(CAST((SUM(Rothers)) AS DECIMAL(18,2))) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND RCompany_Code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY RAgent_id) AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "'  AND Clearing_Date BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_Id='" + ddl_BankName.SelectedItem.Value.Trim() + "' AND Route_id='" + ddl_RouteID.SelectedItem.Value.Trim() + "') AS cart ON prdelo.SproAid=cart.cartAid ) AS FF  " +
     " LEFT JOIN  " +
     " (SELECT Route_id AS RRid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS Rout ON FF.Rid=Rout.RRid) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL ORDER BY F2.Rid,bp.Agent_Id ";

        }

        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }

    public DataTable LoadBankPaymentDatas1(string ccode, string pcode, string dt1, string dt2, string rid, string bankid)
    {
        string sqlstr = string.Empty;
        //      sqlstr = "SELECT * FROM  " +
        //"(SELECT * FROM(Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp RIGHT JOIN  (SELECT * FROM(SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,CONVERT(NVARCHAR(15),(CONVERT(Nvarchar(10),F.proAid)+'_'+F.Agent_Name)) AS Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F )AS F1) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL) AS  final1 " +
        //" INNER JOIN " +
        //" (SELECT agent_id FROM Agent_Master Where Company_Code='" + ccode + "' AND Plant_code='" + pcode + "' AND Route_id='" + rid + "'  AND Bank_Id='" + bankid + "') AS agent  ON final1.proAid=agent.Agent_Id ORDER BY final1.proAid";
        if (Chk_Cash.Checked == true)
        {
            sqlstr = "SELECT * FROM " +
    " (Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp " +
    " RIGHT JOIN " +
    " (SELECT *FROM " +
    " (SELECT cart.ARid AS Rid,cart.cartAid AS proAid,(CONVERT(nvarchar(15),(CONVERT(nvarchar(9),cart.cartAid)+'_'+ cart.Agent_Name))) AS Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS Netpay,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM(SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg FROM Procurement WHERE prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "'  AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  RAgent_id AS DAid ,(CAST((SUM(RBilladvance)) AS DECIMAL(18,2))) AS Billadv,(CAST((SUM(RAi)) AS DECIMAL(18,2))) AS Ai,(CAST((SUM(RFeed)) AS DECIMAL(18,2))) AS Feed,(CAST((SUM(Rcan)) AS DECIMAL(18,2))) AS can,(CAST((SUM(RRecovery)) AS DECIMAL(18,2))) AS Recovery,(CAST((SUM(Rothers)) AS DECIMAL(18,2))) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND RCompany_Code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY RAgent_id) AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "'  AND Clearing_Date BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_Id='0') AS cart ON prdelo.SproAid=cart.cartAid ) AS FF  " +
    " LEFT JOIN  " +
    " (SELECT Route_id AS RRid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS Rout ON FF.Rid=Rout.RRid) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL ORDER BY F2.Rid,bp.Agent_Id ";
        }

        if (chk_Allroute.Checked == true)
        {
            sqlstr = "SELECT * FROM " +
      " (Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp " +
      " RIGHT JOIN " +
      " (SELECT *FROM " +
      " (SELECT cart.ARid AS Rid,cart.cartAid AS proAid,(CONVERT(nvarchar(15),(CONVERT(nvarchar(9),cart.cartAid)+'_'+ cart.Agent_Name))) AS Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS Netpay,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM(SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg FROM Procurement WHERE prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "'  AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  RAgent_id AS DAid ,(CAST((SUM(RBilladvance)) AS DECIMAL(18,2))) AS Billadv,(CAST((SUM(RAi)) AS DECIMAL(18,2))) AS Ai,(CAST((SUM(RFeed)) AS DECIMAL(18,2))) AS Feed,(CAST((SUM(Rcan)) AS DECIMAL(18,2))) AS can,(CAST((SUM(RRecovery)) AS DECIMAL(18,2))) AS Recovery,(CAST((SUM(Rothers)) AS DECIMAL(18,2))) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND RCompany_Code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY RAgent_id) AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "'  AND Clearing_Date BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_Id='" + ddl_BankName.SelectedItem.Value.Trim() + "') AS cart ON prdelo.SproAid=cart.cartAid ) AS FF  " +
      " LEFT JOIN  " +
      " (SELECT Route_id AS RRid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS Rout ON FF.Rid=Rout.RRid) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL ORDER BY F2.Rid,bp.Agent_Id ";
        }
        else
        {
            sqlstr = "SELECT * FROM " +
     " (Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp " +
     " RIGHT JOIN " +
     " (SELECT *FROM " +
     " (SELECT cart.ARid AS Rid,cart.cartAid AS proAid,(CONVERT(nvarchar(15),(CONVERT(nvarchar(9),cart.cartAid)+'_'+ cart.Agent_Name))) AS Agent_Name,ISNULL(prdelo.Smkg,0) AS Smkg,ISNULL(prdelo.Smltr,0) AS Smltr,ISNULL(prdelo.AvgFat ,0) AS AvgFat,ISNULL(prdelo.AvgSnf,0) AS AvgSnf,ISNULL(prdelo.AvgRate,0) AS AvgRate,ISNULL(prdelo.Avgclr,0) AS Avgclr,ISNULL(prdelo.Scans,0) AS Scans,ISNULL(prdelo.SAmt,0) AS SAmt,ISNULL(prdelo.ScommAmt,0) AS ScommAmt,ISNULL(CAST((ISNULL(prdelo.Smltr,0) * ISNULL(cart.CarAmt,0)) AS DECIMAL(18,2)),0) AS Scatamt,ISNULL(prdelo.Ssplbonamt,0) AS Ssplbonamt, ISNULL(prdelo.AvgcRate,0) AS AvgcRate,ISNULL(prdelo.Sfatkg,0) AS Sfatkg,ISNULL(prdelo.Ssnfkg,0) AS Ssnfkg,ISNULL(prdelo.Billadv,0) AS SBilladv,ISNULL(prdelo.Ai,0) AS SAiamt,ISNULL(prdelo.Feed,0) AS SFeedamt,ISNULL(prdelo.Can,0) AS Scanamt,ISNULL(prdelo.Recovery,0) AS SRecoveryamt,ISNULL(prdelo.others,0) AS Sothers,ISNULL(prdelo.instamt,0) AS Sinstamt,ISNULL(prdelo.balance,0) AS Sbalance,ISNULL(prdelo.LoanAmount,0) AS SLoanAmount,ISNULL(prdelo.VouAmount,0) AS Sclaim,CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)) AS SRNetAmt,FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2))) AS Netpay,CAST((((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0) + ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0)) )- (FLOOR(CAST( ((ISNULL(prdelo.SAmt,0) + ISNULL(prdelo.ScommAmt,0) + ISNULL(prdelo.Ssplbonamt,0)+ ISNULL(prdelo.VouAmount,0) + ISNULL(prdelo.Scatamt,0)) - (ISNULL(prdelo.Billadv,0)+ISNULL(prdelo.Ai,0)+ISNULL(prdelo.Feed,0)+ISNULL(prdelo.Can,0)+ISNULL(prdelo.Recovery,0)+ISNULL(prdelo.others,0)+ISNULL(prdelo.instamt,0))) AS DECIMAL(18,2)))) ) AS DECIMAL(18,2)) AS SRound,cart.Bank_Id,cart.Payment_mode,cart.Agent_AccountNo FROM(SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT agent_id AS SproAid,SUM(Milk_kg) AS Smkg,SUM(Milk_ltr) AS Smltr,CAST(AVG(FAT) AS DECIMAL(18,2)) AS AvgFat,CAST(AVG(SNF) AS DECIMAL(18,2)) AS AvgSnf,CAST(AVG(Rate) AS DECIMAL(18,2)) AS AvgRate,CAST(AVG(Clr) AS DECIMAL(18,2)) AS Avgclr,CAST(SUM(NoofCans) AS DECIMAL(18,2)) AS Scans,SUM(Amount)  AS SAmt,SUM(Comrate)  AS ScommAmt,SUM(CartageAmount) AS Scatamt,SUM(SplBonusAmount) AS Ssplbonamt,CAST(AVG(ComRate) AS DECIMAL(18,2)) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg FROM Procurement WHERE prdate BETWEEN '" + dt1 + "' AND '" + dt2 + "'  AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY agent_id ) AS Spro LEFT JOIN (SELECT  RAgent_id AS DAid ,(CAST((SUM(RBilladvance)) AS DECIMAL(18,2))) AS Billadv,(CAST((SUM(RAi)) AS DECIMAL(18,2))) AS Ai,(CAST((SUM(RFeed)) AS DECIMAL(18,2))) AS Feed,(CAST((SUM(Rcan)) AS DECIMAL(18,2))) AS can,(CAST((SUM(RRecovery)) AS DECIMAL(18,2))) AS Recovery,(CAST((SUM(Rothers)) AS DECIMAL(18,2))) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND RCompany_Code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY RAgent_id) AS dedu ON Spro.SproAid=dedu.DAid) AS proded LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "'  AND Clearing_Date BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_Id) AS vou ON proded.SproAid=vou.VouAid) AS pdv LEFT JOIN  (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1 + "' AND '" + dt2 + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Lon ON pdv.SproAid=Lon.LoAid ) AS prdelo  INNER JOIN   (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Route_id AS ARid  FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Bank_Id='" + ddl_BankName.SelectedItem.Value.Trim() + "' AND Route_id='" + ddl_RouteID.SelectedItem.Value.Trim() + "') AS cart ON prdelo.SproAid=cart.cartAid ) AS FF  " +
     " LEFT JOIN  " +
     " (SELECT Route_id AS RRid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "')AS Rout ON FF.Rid=Rout.RRid) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL ORDER BY F2.Rid,bp.Agent_Id ";

        }
        dt = dbaccess.GetDatatable(sqlstr);
        return dt;
    }

    protected void chk_Allroute_CheckedChanged(object sender, EventArgs e)
    {
        AllRoute();
       // BindBankPaymentData();
        BindBank();
    }
  
    private void AllRoute()
    {
        if (chk_Allroute.Checked == true)
        {
            lbl_RouteName.Enabled = false;
            ddl_RouteName.Enabled = false;
           
        }
        else
        {
            lbl_RouteName.Enabled = true;
            ddl_RouteName.Enabled = true;

        }
    }

    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        BindBankPaymentData();

    }

    protected void Chk_Cash_CheckedChanged(object sender, EventArgs e)
    {
        AllCash();
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        //BindBankPaymentData();
        BindBank();

    }
    private void AllCash()
    {
        if (Chk_Cash.Checked == true)
        {
            chk_Allroute.Checked = false;
            chk_Allroute.Enabled = false;
            lbl_BankName.Enabled = false;
            ddl_BankName.Enabled = false;
            lbl_RouteName.Enabled = false;
            ddl_RouteName.Enabled = false;

        }
        else
        {
            chk_Allroute.Enabled = true;
            lbl_BankName.Enabled = true;
            ddl_BankName.Enabled = true;
            lbl_RouteName.Enabled = true;
            ddl_RouteName.Enabled = true;

        }
    }

    private void BindBank()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);


            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");           
            ds = LoadBank1(ccode, pcode, d1, d2, rid.ToString(), ddl_BankName.SelectedItem.Value.Trim());
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Agent_Name";
                CheckBoxList1.DataValueField = "proAid";
                CheckBoxList1.DataBind();
                CheckBoxList2.DataSource = ds;
                CheckBoxList2.DataTextField = "Netpay";
                CheckBoxList2.DataValueField = "Netpay";
                CheckBoxList2.DataBind();

            }
            else
            {
                WebMsgBox.Show("Agents Not Available in this Selection...");
            }
        }
        catch (Exception ex)
        {

        }
    }
    private DataTable DisplayselectedBankid()
    {
        DataTable DtBid = new DataTable();
        try
        {            
            DataColumn DcBid = new DataColumn();
            DataRow ksdr = null;
            DcBid = new DataColumn("Bankid");
            DtBid.Columns.Add(DcBid);

            foreach (System.Web.UI.WebControls.ListItem item in ddchkCountry.Items)
            {
                if (item.Selected)
                {
                    ksdr = DtBid.NewRow();
                    ksdr[0] = item.Value.ToString();

                    DtBid.Rows.Add(ksdr);
                }
            }
            return DtBid;
        }
        catch (Exception ex)
        {
            return DtBid;
        }
    }
    
   
    public DataSet LoadBank1(string ccode, string pcode, string dt1, string dt2, string rid, string bankid)
    {
       
            DataTable custDT = DisplayselectedBankid();
            DataSet dts = new DataSet();
            SqlParameter param = new SqlParameter();
            param.ParameterName = "CustDtPlantcode";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = custDT;
            param.Direction = ParameterDirection.Input;
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                if (chk_Allroute.Checked == true)
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AllottedBankid]");
                    conn.Open();
                    sqlCmd.CommandTimeout = 200;
                    sqlCmd.Connection = conn;

                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                    sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                    sqlCmd.Parameters.AddWithValue("@spfrmdate", dt1);
                    sqlCmd.Parameters.AddWithValue("@sptodate", dt2);
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    da.Fill(dts);
                }
                else
                {
                    SqlCommand sqlCmd1 = new SqlCommand("dbo.[Get_AllottedBankidParticlarRouteId]");
                    conn.Open();
                    sqlCmd1.Connection = conn;
                    sqlCmd1.CommandTimeout = 300;
                    sqlCmd1.CommandType = CommandType.StoredProcedure;
                    sqlCmd1.Parameters.Add(param);
                    sqlCmd1.Parameters.AddWithValue("@spccode", ccode);
                    sqlCmd1.Parameters.AddWithValue("@sppcode", pcode);
                    sqlCmd1.Parameters.AddWithValue("@spfrmdate", dt1);
                    sqlCmd1.Parameters.AddWithValue("@sptodate", dt2);
                    sqlCmd1.Parameters.AddWithValue("@sprid", rid);
                    SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                    da1.Fill(dts);
                }
            }
            //              
            return dts;
       
    }
    public DataTable LoadBank2(string ccode, string pcode, string dt1, string dt2, string rid, string bankid)
    {

        DataTable custDT = DisplayselectedBankid();

        DataTable dts = new DataTable();
        SqlParameter param = new SqlParameter();
        param.ParameterName = "CustDtPlantcode";
        param.SqlDbType = SqlDbType.Structured;
        param.Value = custDT;
        param.Direction = ParameterDirection.Input;
        String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = null;
        using (conn = new SqlConnection(dbConnStr))
        {
            if (chk_Allroute.Checked == true)
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AllottedBankid]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandTimeout = 300;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", dt1);
                sqlCmd.Parameters.AddWithValue("@sptodate", dt2);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);
            }
            else
            {
                SqlCommand sqlCmd1 = new SqlCommand("dbo.[Get_AllottedBankidParticlarRouteId]");
                conn.Open();
                sqlCmd1.Connection = conn;
                sqlCmd1.CommandTimeout = 300;
                sqlCmd1.CommandType = CommandType.StoredProcedure;
                sqlCmd1.Parameters.Add(param);
                sqlCmd1.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd1.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd1.Parameters.AddWithValue("@spfrmdate", dt1);
                sqlCmd1.Parameters.AddWithValue("@sptodate", dt2);
                sqlCmd1.Parameters.AddWithValue("@sprid", rid);
                SqlDataAdapter da1 = new SqlDataAdapter(sqlCmd1);
                da1.Fill(dts);
            }
        }
        //              
        return dts;
    }
  

    protected void ddchkCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_updatestatus_Click(object sender, EventArgs e)
    {
        updatefilestatus();
    }


    public void getassignedamt()
    {
        try
        {

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string assignamt = "";
            assignamt = "Select top 1 Amount    FROM  AdminAmountAllottoplant    WHERE   Plant_code='" + pcode + "'    AND Billfrmdate='" + d1 + "' AND Billtodate='" + d2 + "' order by Tid desc ";
            con = dbaccess.GetConnection();
            SqlCommand cmd = new SqlCommand(assignamt, con);
            SqlDataAdapter dsp = new SqlDataAdapter(cmd);
            DataSet dfr = new DataSet();
            dsp.Fill(dfr);
            txt_assignamt1.Text = dfr.Tables[0].Rows[0][0].ToString();
        }
        catch
        {


        }

    }
    private void updatefilestatus()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string sqlstr = "Update  BankPaymentllotment set PStatus=1 WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Billfrmdate='" + d1.ToString().Trim() + "' AND Billtodate='" + d2.ToString().Trim() + "' AND BankFileName='" + ddl_oldfilename.SelectedItem.Value.ToString().Trim() + "'";
            dbaccess.ExecuteNonquorey(sqlstr);
            lbl_ErrorMsg.Visible = true;
            lbl_ErrorMsg.Text = "File Status Updated and Closed...";
        }
        catch (Exception ex)
        {
            lbl_ErrorMsg.Visible = true;
            lbl_ErrorMsg.Text = ex.ToString().Trim();
        }
    }

    private int Saveoldfilenamecheckstatus()
    {
        int count = 0;
        string sqlstr = string.Empty;
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            if (chk_OldFileName.Checked == true)
            {
                 sqlstr = "SELECT Count(*) AS counts FROM BankPaymentllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Billfrmdate='" + d1.ToString().Trim() + "' AND Billtodate='" + d2.ToString().Trim() + "' AND BankFileName='" + ddl_oldfilename.SelectedItem.Value.ToString().Trim() + "' AND PStatus=1";
            }
            else
            {
                 sqlstr = "SELECT Count(*) AS counts FROM BankPaymentllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Billfrmdate='" + d1.ToString().Trim() + "' AND Billtodate='" + d2.ToString().Trim() + "' AND BankFileName='" + txt_FileName.Text.ToString().Trim() + "' AND PStatus=1";
            }

            count = dbaccess.ExecuteScalarint(sqlstr);
            return count;
        }
        catch (Exception em)
        {
            return count;
        }
    }

    private int FileNamecheckstatus()
    {
        int count = 0;
        string sqlstr = string.Empty;
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            if (chk_OldFileName.Checked == true)
            {
                
            }
            else
            {
                sqlstr = "SELECT Count(*) AS counts FROM BankPaymentllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Billfrmdate='" + d1.ToString().Trim() + "' AND Billtodate='" + d2.ToString().Trim() + "' AND BankFileName='" + txt_FileName.Text.ToString().Trim() + "' ";
            }

            count = dbaccess.ExecuteScalarint(sqlstr);
            return count;
        }
        catch (Exception em)
        {
            return count;
        }
    }

    private void DeleteProblemFiles()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string deletefilename = ddl_oldfilename.SelectedItem.Value.ToString().Trim();
            string sqlstr1 = "SELECT count(*) From BankPaymentllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Billfrmdate='" + d1.ToString().Trim() + "' AND Billtodate='" + d2.ToString().Trim() + "' AND BankFileName='" + ddl_oldfilename.SelectedItem.Value.ToString().Trim() + "' AND PStatus=1";
            int count=dbaccess.ExecuteScalarint(sqlstr1);
            if (count > 0)
            {
                lbl_ErrorMsg.Visible = true;
                lbl_ErrorMsg.Text = "File Already Closed...FileName[" + deletefilename + "]";
            }
            else
            {
                string sqlstr = "DELETE From BankPaymentllotment WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Billfrmdate='" + d1.ToString().Trim() + "' AND Billtodate='" + d2.ToString().Trim() + "' AND BankFileName='" + ddl_oldfilename.SelectedItem.Value.ToString().Trim() + "' AND PStatus=0";
                dbaccess.ExecuteNonquorey(sqlstr);
                lbl_ErrorMsg.Visible = true;
                lbl_ErrorMsg.Text = "File Deleted Successfully...FileName[" + deletefilename + "]";
            }
          
        }
        catch (Exception ex)
        {
            lbl_ErrorMsg.Visible = true;
            lbl_ErrorMsg.Text = ex.ToString().Trim();
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        DeleteProblemFiles();
        Loadoldfilename();
    }
    protected void ddl_oldfilename_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txt_assignamt_TextChanged(object sender, EventArgs e)
    {

    }
}