using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class Rpt_PerDayPandL : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();

    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    public string strsql = string.Empty;
    public string d1, d2;
    string[] strarr = new string[2];
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;
                txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
                txt_ToDate.Text = tdt.ToString("dd/MM/yyy");
                LoadPlantName();
                LoadReportDisplayItems();
                Lbl_Errormsg.Visible = false;
                Label5.Text = txt_FromDate.Text.Trim();
                Label7.Text = txt_ToDate.Text.Trim();
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
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                Lbl_Errormsg.Visible = false;
                Label5.Text = txt_FromDate.Text.Trim();
                Label7.Text = txt_ToDate.Text.Trim();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    private void LoadReportDisplayItems()
    {
        try
        {
            ddchkCountry.Items.Clear();
            string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "kg_Snf", "AvgSnf", "NillPayments", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Total Solid TS", "Procure Vale TS Rate", "Transport TS Rate", "Chilling Cost Per Ltr", "Chilling Cost TS Rate", "Total TS Rate", "kg milk gain", "kg fat gain", "kg snf gain", "Total Fat+Snf Gain", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %", "Fat Cost @ 175", "Snf Cost @ 150", "Total Fat+Snf Cost", "FatCost Ltrs", "SnfCost Ltrs", "Total Fat+Snf Cost Ltrs", "Fat & Snf PerTS", "Total Cost", "Net_TSRate", "TopGainpercent", "TopLtrcost", "TopTransportLtrcost" };//49
            int cou = strarr1.Count();
            for (int i = 0; i <= cou; i++)
            {
                ddchkCountry.Items.Add(strarr1[i]);
            }


        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void LoadPlantName()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Plant_Name";
                CheckBoxList1.DataValueField = "plant_Code";//ROUTE_ID 
                CheckBoxList1.DataBind();
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void Datechanged()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            d1 = dtm.ToString("MM/dd/yyyy");
            d2 = dtm1.ToString("MM/dd/yyyy");
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    private void LoadMonthBillDate()
    {
        try
        {

            //strsql = "SELECT Plant_Code,Plant_Code AS pcode,Bill_frmdate,Bill_todate,Convert(nvarchar(15),Bill_frmdate,103)+'_'+Convert(nvarchar(15),Bill_todate,103) AS FrmTodate,CONVERT(NVARCHAR,CAST(CONVERT(Nvarchar,Bill_frmdate) AS date))+'_'+CONVERT(NVARCHAR,CAST(CONVERT(Nvarchar,Bill_todate) AS date)) AS FrmTodate1  FROM(SELECT Top 4 *  FROM Bill_date Where Plant_Code='" + pcode.Trim() + "'  AND Bill_frmdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'   order by Tid desc ) AS t1";
            //using (con = DBaccess.GetConnection())
            //{
            //    SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            //    da.Fill(ds);
            //    CheckBoxList1.DataSource = ds;
            //    CheckBoxList1.DataTextField = "FrmTodate";
            //    CheckBoxList1.DataValueField = "FrmTodate1";
            //    CheckBoxList1.DataBind();
            //}

            Datechanged();
            int count = 0;
            dt = BllPlant.DTLoadPlantNameChkLst(ccode, d1, d2);
            count = dt.Rows.Count;
            if (count > 0)
            {
                DataTable custDT = new DataTable();
                DataColumn col = null;

                col = new DataColumn("plant_Code");
                custDT.Columns.Add(col);

                for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                {
                    DataRow dr = null;
                    dr = custDT.NewRow();
                    if (CheckBoxList1.Items[i].Selected == true)
                    {
                        dr[0] = CheckBoxList1.Items[i].Value.ToString();
                        custDT.Rows.Add(dr);
                    }

                }
                DataSet ds1 = new DataSet();
                SqlParameter param = new SqlParameter();
                param.ParameterName = "CustDtPlantcode";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = custDT;
                param.Direction = ParameterDirection.Input;
                String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Get_PlantPerDayMilk]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                    sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                    sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    da.Fill(ds1);

                    //
                }
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    private void Menu1()
    {
        try
        {
            if (MChk_PlantName.Checked == true)
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
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    protected void MChk_PlantName_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }

    private void LoadPlantwisepermonthOverAllDataResult()
    {
        try
        {
            Datechanged();
            DataTable custDT = new DataTable();
            DataColumn col = null;

            col = new DataColumn("plant_Code");
            custDT.Columns.Add(col);
            col = new DataColumn("FrmDate");
            custDT.Columns.Add(col);
            col = new DataColumn("ToDate");
            custDT.Columns.Add(col);

            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                DataRow dr = null;
                dr = custDT.NewRow();
                if (CheckBoxList1.Items[i].Selected == true)
                {
                    dr[0] = CheckBoxList1.Items[i].Value.ToString().Trim();
                    dr[1] = d1.ToString().Trim();
                    dr[2] = d2.ToString().Trim();

                }
                custDT.Rows.Add(dr);
            }

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
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_PlantwiseSingleBilldateOverAllDataResult]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                sqlCmd.Parameters.AddWithValue("@spccode", 2);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);

                //
                DataTable ksdt = new DataTable();
                DataColumn ksdc = null;
                DataRow ksdr = null;
                int counts = dts.Rows.Count;

                if (rd_RportViewType.SelectedItem != null)
                {
                    Lbl_selectedReportItem.Text = rd_RportViewType.SelectedItem.Value;
                    if (Lbl_selectedReportItem.Text == "Horizontal")
                    {
                        #region First OK Start

                        // START ADDING COLUMN
                        if (counts > 0)
                        {
                            ksdc = new DataColumn("Period");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Scans");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Smkg");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Smltr");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Sfatkg");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Ssnfkg");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Aclr");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Samt");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SInsentAmt");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Scaramt");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SSplBonus");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SClaimAmt");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SBilladv");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SAi");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SFeed");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Scan");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SRecovery");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Sinstamt");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SLoanClobal");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("TotAdd");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("STotdedu");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SLoanAmt");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SRoundoff");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SNetAmt");
                            ksdt.Columns.Add(ksdc);

                        }
                        // END ADDING COLUMN

                        // START ADDING ROWS
                        if (counts > 0)
                        {
                            object id2;
                            id2 = 0;
                            int idd2 = Convert.ToInt32(id2);

                            foreach (DataRow dr2 in dts.Rows)
                            {
                                ksdr = ksdt.NewRow();
                                ksdr[0] = dr2["Plant_code"].ToString();
                                ksdr[1] = dr2["Scans"].ToString();
                                ksdr[2] = dr2["Smkg"].ToString();
                                ksdr[3] = dr2["Smltr"].ToString();
                                ksdr[4] = dr2["Sfatkg"].ToString();
                                ksdr[5] = dr2["Ssnfkg"].ToString();
                                ksdr[6] = dr2["Aclr"].ToString();
                                ksdr[7] = dr2["Samt"].ToString();
                                ksdr[8] = dr2["SInsentAmt"].ToString();
                                ksdr[9] = dr2["Scaramt"].ToString();
                                ksdr[10] = dr2["SSplBonus"].ToString();
                                ksdr[11] = dr2["SClaimAount"].ToString();
                                ksdr[12] = dr2["SBilladv"].ToString();
                                ksdr[13] = dr2["SAi"].ToString();
                                ksdr[14] = dr2["SFeed"].ToString();
                                ksdr[15] = dr2["Scan"].ToString();
                                ksdr[16] = dr2["SRecovery"].ToString();
                                ksdr[17] = dr2["Sinstamt"].ToString();
                                ksdr[18] = dr2["SLoanClosingbalance"].ToString();
                                ksdr[19] = dr2["TotAdditions"].ToString();
                                ksdr[20] = dr2["STotdeductions"].ToString();
                                ksdr[21] = dr2["SLoanAmount"].ToString();
                                ksdr[22] = dr2["SRoundoff"].ToString();
                                ksdr[23] = dr2["SNetAmount"].ToString();

                                ksdt.Rows.Add(ksdr);
                            }
                        }
                        // END ADDING ROWS
                        #endregion
                    }
                    else if (Lbl_selectedReportItem.Text == "Vertical")
                    {
                        #region Second OK Start

                        // START ADDING COLUMN
                        if (counts > 0)
                        {
                            ksdc = new DataColumn("Details");
                            ksdt.Columns.Add(ksdc);
                            foreach (DataRow dr1 in dts.Rows)
                            {
                                object id;
                                id = dr1[24].ToString();

                                string columnName = id.ToString();
                                DataColumnCollection columns = ksdt.Columns;
                                if (columns.Contains(columnName))
                                {

                                }
                                else
                                {
                                    ksdc = new DataColumn(id.ToString());
                                    ksdt.Columns.Add(ksdc);
                                }

                            }
                        }
                        // END ADDING COLUMN


                        // START ADDING ROWS
                        string[] strarr1 = new string[] { "Period", "Scans", "Smkg", "Smltr", "Sfatkg", "Ssnfkg", "Aclr", "Samt", "SInsentAmt", "Scaramt", "SSplBonus", "SClaimAount", "SBilladv", "SAi", "SFeed", "Scan", "SRecovery", "Sinstamt", "SLoanClobal", "TotAdd", "STotdedu", "SLoanAmt", "SRoundoff", "SNetAmt" };
                        if (counts > 0)
                        {
                            int i = 0;

                            for (int colcount = dts.Columns.Count - 1; colcount > 0; colcount--)
                            {
                                ksdr = ksdt.NewRow();
                                ksdr[0] = strarr1[i].ToString();
                                int j = 1;
                                foreach (DataRow dr2 in dts.Rows)
                                {
                                    ksdr[j] = dr2[i].ToString();
                                    j++;
                                }
                                i++;
                                ksdt.Rows.Add(ksdr);
                            }
                        }
                        // END ADDING ROWS

                        #endregion
                    }
                    else
                    {
                    }
                }
                else
                {
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "Please Select the Report View Type";
                }


                gvCustomers.DataSource = ksdt;
                gvCustomers.DataBind();

            }
        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    protected void btn_Get_Click(object sender, EventArgs e)
    {
        // LoadPlantwisepermonthOverAllDataResult();
        LoadPlantwisepermonthOverAllDataResultORG();
    }

    private void LoadPlantwisepermonthOverAllDataResultORG()
    {
        try
        {
            Datechanged();
            DataTable custDT = new DataTable();
            DataColumn col = null;

            col = new DataColumn("plant_Code");
            custDT.Columns.Add(col);
            col = new DataColumn("FrmDate");
            custDT.Columns.Add(col);
            col = new DataColumn("ToDate");
            custDT.Columns.Add(col);

            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                DataRow dr = null;
                dr = custDT.NewRow();
                if (CheckBoxList1.Items[i].Selected == true)
                {
                    dr[0] = CheckBoxList1.Items[i].Value.ToString().Trim();
                    dr[1] = d1.ToString().Trim();
                    dr[2] = d2.ToString().Trim();

                }
                custDT.Rows.Add(dr);
            }

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
                // SqlCommand sqlCmd = new SqlCommand("dbo.[Get_PlantwiseSingleBilldateOverAllDataResultORG]");
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_PerdayPLReport]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                //if company code 2 means group by plant data between frmdate permonth (OR) 1 means  group by plant data frmdate and todate
                sqlCmd.Parameters.AddWithValue("@spccode", 2);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);

                //
                DataTable ksdt = new DataTable();
                DataColumn ksdc = null;
                DataRow ksdr = null;
                int counts = dts.Rows.Count;

                if (rd_RportViewType.SelectedItem != null)
                {
                    Lbl_selectedReportItem.Text = rd_RportViewType.SelectedItem.Value;
                    if (Lbl_selectedReportItem.Text == "Horizontal")
                    {
                        #region First OK Start

                        // START ADDING COLUMN
                        if (counts > 0)
                        {
                            ksdc = new DataColumn("Period");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("TotalMkgs");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("TotalMltrs");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("PerDayMilkMltrs");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("LtrCost");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("kg_Fat");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("AvgFat");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("kg_Snf");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("AvgSnf");
                            ksdt.Columns.Add(ksdc);
                            //ksdc = new DataColumn("A.Fat+snf=TS");
                            //ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("NillPayments");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("MilkValueAmout");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("BillAdv");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("AiAdv");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("FeedAdv");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("CanAdv");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Recovery");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("LoanAdv");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("loanperltrdedu");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("NetBillAmount");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("TransportAmount");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Transport Ltr Cost");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total Solid TS");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Procure Vale TS Rate");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Transport TS Rate");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Chilling Cost Per Ltr");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Chilling Cost TS Rate");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total TS Rate");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("kg milk gain");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("kg fat gain");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("kg snf gain");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total Fat+Snf Gain");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Fat Gain % perLtr");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Snf Gain % perLtr");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total Gain % perLtr");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("AvgFat GainFat %");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("AvgSnf GainSnf %");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total AvgGainSnf %");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Fat Cost @ 175");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Snf Cost @ 150");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total Fat+Snf Cost");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("FatCost Ltrs");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("SnfCost Ltrs");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total Fat+Snf Cost Ltrs");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Fat & Snf PerTS");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Total Cost");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("Net_TSRate");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("TopGainpercent");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("TopLtrcost");
                            ksdt.Columns.Add(ksdc);
                            ksdc = new DataColumn("TopTransportLtrcost");
                            ksdt.Columns.Add(ksdc);

                        }
                        // END ADDING COLUMN

                        // START ADDING ROWS
                        if (counts > 0)
                        {
                            object id2;
                            id2 = 0;
                            int idd2 = Convert.ToInt32(id2);

                            foreach (DataRow dr2 in dts.Rows)
                            {
                                ksdr = ksdt.NewRow();
                                ksdr[0] = dr2["Plant_code"].ToString();
                                ksdr[1] = dr2["Smkg"].ToString();
                                ksdr[2] = dr2["Smltr"].ToString();
                                ksdr[3] = dr2["PerDayMilkMltrs"].ToString();
                                ksdr[4] = dr2["LtrCost"].ToString();
                                ksdr[5] = dr2["Sfatkg"].ToString();
                                ksdr[6] = dr2["AvgFat"].ToString();
                                ksdr[7] = dr2["Ssnfkg"].ToString();
                                ksdr[8] = dr2["AvgSnf"].ToString();
                                ////ksdr[9] = dr2["TS"].ToString();
                                ksdr[9] = dr2["NillPayments"].ToString();
                                ksdr[10] = dr2["Samt"].ToString();
                                ksdr[11] = dr2["SBilladv"].ToString();
                                ksdr[12] = dr2["SAi"].ToString();
                                ksdr[13] = dr2["SFeed"].ToString();
                                ksdr[14] = dr2["Scan"].ToString();
                                ksdr[15] = dr2["SRecovery"].ToString();
                                ksdr[16] = dr2["Sinstamt"].ToString();
                                ksdr[17] = dr2["loanperltrdedu"].ToString();
                                ksdr[18] = dr2["SNetAmount"].ToString();
                                ksdr[19] = dr2["STransportAmt"].ToString();
                                ksdr[20] = dr2["TransportLtrcost"].ToString();
                                ksdr[21] = dr2["TotalsolidTs"].ToString();
                                ksdr[22] = dr2["ProcureValeTSRate"].ToString();
                                ksdr[23] = dr2["TransportTSRate"].ToString();
                                ksdr[24] = dr2["ChillingCostPerLtr"].ToString();
                                ksdr[25] = dr2["ChillingCostTSRate"].ToString();
                                ksdr[26] = dr2["TotalTSRate"].ToString();
                                ksdr[27] = dr2["kgmilkgain"].ToString();
                                ksdr[28] = dr2["kgfatgain"].ToString();
                                ksdr[29] = dr2["kgsnfgain"].ToString();
                                ksdr[30] = dr2["Total_FatSnfGain"].ToString();
                                ksdr[31] = dr2["Fat_GainpercentperLtr"].ToString();
                                ksdr[32] = dr2["Snf_GainpercentperLtr"].ToString();
                                ksdr[33] = dr2["Total_GainpercentperLtr"].ToString();
                                ksdr[34] = dr2["AvgFat_GainFatpercent"].ToString();
                                ksdr[35] = dr2["AvgSnf_GainSnfpercent"].ToString();
                                ksdr[36] = dr2["Total_AvgGainSnfpercent"].ToString();
                                ksdr[37] = dr2["Fat_Cost"].ToString();
                                ksdr[38] = dr2["Snf_Cost"].ToString();
                                ksdr[39] = dr2["Total_FSCost"].ToString();
                                ksdr[40] = dr2["FatCost_Ltrs"].ToString();
                                ksdr[41] = dr2["SnfCost_Ltrs"].ToString();
                                ksdr[42] = dr2["Total_FSCost_Ltrs"].ToString();
                                ksdr[43] = dr2["Fat_SnfPerTS"].ToString();
                                ksdr[44] = dr2["Total_Cost"].ToString();
                                ksdr[45] = dr2["Net_TSRate"].ToString();
                                ksdr[46] = dr2["TopGainpercent"].ToString();
                                ksdr[47] = dr2["TopLtrcost"].ToString();
                                ksdr[48] = dr2["TopTransportLtrcost"].ToString();
                                ksdt.Rows.Add(ksdr);
                            }
                        }
                        // END ADDING ROWS
                        #endregion
                    }
                    else if (Lbl_selectedReportItem.Text == "Vertical")
                    {
                        #region Second OK Start

                        // START ADDING COLUMN
                        if (counts > 0)
                        {
                            ksdc = new DataColumn("Details");
                            ksdt.Columns.Add(ksdc);
                            foreach (DataRow dr1 in dts.Rows)
                            {
                                object id;
                                id = dr1[49].ToString();

                                string columnName = id.ToString();
                                DataColumnCollection columns = ksdt.Columns;
                                if (columns.Contains(columnName))
                                {

                                }
                                else
                                {
                                    ksdc = new DataColumn(id.ToString());
                                    ksdt.Columns.Add(ksdc);
                                }

                            }
                        }
                        // END ADDING COLUMN


                        // START ADDING ROWS
                        //string[] strarr1 = new string[] { "Period", "Scans", "Smkg", "Smltr", "Sfatkg", "Ssnfkg", "Aclr", "Samt", "SInsentAmt", "Scaramt", "SSplBonus", "SClaimAount", "SBilladv", "SAi", "SFeed", "Scan", "SRecovery", "Sinstamt", "SLoanClobal", "TotAdd", "STotdedu", "SLoanAmt", "SRoundoff", "SNetAmt" };//24
                        string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "kg_Snf", "AvgSnf", "NillPayments", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Total Solid TS", "ProcureVale TSRate", "Transport TS Rate", "Chilling Cost Per Ltr", "ChillCost TSRate", "Total TS Rate", "kg milk gain", "kg fat gain", "kg snf gain", "Tot Fat+Snf Gain", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %", "Fat Cost @ 175", "Snf Cost @ 150", "Total Fat+Snf Cost", "FatCost Ltrs", "SnfCost Ltrs", "Tot Fat+Snf CostLtrs", "Fat & Snf PerTS", "Total Cost", "Net_TSRate", "TopGainpercent", "TopLtrcost", "TopTransportLtrcost" };//52
                        if (counts > 0)
                        {
                            int i = 0;
                            foreach (System.Web.UI.WebControls.ListItem item in ddchkCountry.Items)
                            {
                                if (item.Selected)
                                {
                                    ksdr = ksdt.NewRow();
                                    strarr1[i] = item.Value.ToString();
                                    ksdr[0] = strarr1[i].ToString();
                                    int j = 1;
                                    foreach (DataRow dr2 in dts.Rows)
                                    {
                                        ksdr[j] = dr2[i].ToString();
                                        j++;
                                    }
                                    i++;
                                    ksdt.Rows.Add(ksdr);
                                }
                                else
                                {
                                    i++;
                                }

                            }

                        }
                        // END ADDING ROWS

                        #endregion
                    }
                    else
                    {
                    }
                }
                else
                {
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "Please Select the Report View Type";
                }


                gvCustomers.DataSource = ksdt;
                gvCustomers.DataBind();
                int columncoount = ksdt.Columns.Count;

                if (Lbl_selectedReportItem.Text == "Horizontal")
                {

                }
                else
                {
                    Gridcolor(columncoount);
                }
            }
        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Gridcolor(int colcount)
    {
        try
        {
            int rowcount = 0;
            int celcount = 0;
            int[] colr = new int[5] { 22, 27, 31, 44, 46 };

            for (int i = 0; i < colcount; i++)
            {
                rowcount = colr[i];
                for (int j = 0; j < colcount; j++)
                {

                    gvCustomers.Rows[rowcount].Cells[j].BackColor = Color.DarkSlateGray;
                    gvCustomers.Rows[rowcount].Cells[j].ForeColor = Color.White;
                    celcount++;
                }
            }
        }
        catch (Exception ex)
        {
            //Lbl_Errormsg.Visible = true;
            //Lbl_Errormsg.Text = ex.ToString();
        }
    }
}