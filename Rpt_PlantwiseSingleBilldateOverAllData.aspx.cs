using System.Collections.Generic;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;



public partial class Rpt_PlantwiseSingleBilldateOverAllData : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    BLLPlantName BllPlant = new BLLPlantName();
    public string Company_code;
    public string plant_Code;
    public string cname;
    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    public string d1, d2;
    BLLuser Bllusers = new BLLuser();

    string[] strarr = new string[2];
    string[] strarr1 = new string[60];

    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                tdt = System.DateTime.Now;
                txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
                txt_ToDate.Text = tdt.ToString("dd/MM/yyy");
               
                LoadPlantName();
                LoadReportDisplayItems();
                Menu1();                
                Lbl_Errormsg.Visible = false;

                lbl_frmdate.Visible = false;
                txt_FromDate.Visible = false;
                lbl_todate.Visible = false;
                txt_ToDate.Visible = false;
                btn_Load.Visible = false;
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
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                LoadPlantwiseSingleBilldateOverAllDataResult();
                Lbl_Errormsg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
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
    private void LoadPlantName()
    {
        try
        {
            Datechanged();      
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_PlantwiseSingleBilldateOverAllDataReport]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", Company_code);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.ToString());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.ToString());
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
             
                if (ds != null)
                {
                    CheckBoxList1.DataSource = ds;
                    CheckBoxList1.DataTextField = "PlantName";
                    CheckBoxList1.DataValueField = "pcode";
                    CheckBoxList1.DataBind();

                    CheckBoxList2.DataSource = ds;
                    CheckBoxList2.DataTextField = "Frmtodate2";
                    CheckBoxList2.DataValueField = "Frmtodate3";
                    CheckBoxList2.DataBind();

                    CheckBoxList3.DataSource = ds;
                    CheckBoxList3.DataTextField = "Frmtodate1";
                    CheckBoxList3.DataValueField = "Frmtodate4";
                    CheckBoxList3.DataBind();

                }
                else
                {

                }

            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
       
    }

    private void LoadReportDisplayItems()
    {
        try
        {
            ddchkCountry.Items.Clear();
            string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "kg_Snf", "AvgSnf", "NillPayments", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Total Solid TS", "Procure Vale TS Rate", "Transport TS Rate", "Chilling Cost Per Ltr", "Chilling Cost TS Rate", "Total TS Rate", "kg milk gain", "kg fat gain", "kg snf gain", "Total Fat+Snf Gain", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %", "Fat Cost @ 175", "Snf Cost @ 150", "Total Fat+Snf Cost", "FatCost Ltrs", "SnfCost Ltrs", "Total Fat+Snf Cost Ltrs", "Fat & Snf PerTS", "Total Cost", "Net_TSRate" };//49
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
    private void Menu2()
    {
        try
        {
            if (MChk_Date1.Checked == true)
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
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    private void Menu3()
    {
        try
        {
            if (MChk_Date2.Checked == true)
            {
                for (int i = 0; i <= CheckBoxList3.Items.Count - 1; i++)
                {
                    CheckBoxList3.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i <= CheckBoxList3.Items.Count - 1; i++)
                {
                    CheckBoxList3.Items[i].Selected = false;
                }
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void CheckBoxListClear()
    {
        CheckBoxList1.Items.Clear();
        CheckBoxList2.Items.Clear();
        CheckBoxList3.Items.Clear();
    }

    protected void MChk_PlantName_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }
    protected void MChk_Date1_CheckedChanged(object sender, EventArgs e)
    {
        Menu2();
    }
    protected void MChk_Date2_CheckedChanged(object sender, EventArgs e)
    {
        Menu3();
    }

    private void LoadPlantwiseSingleBilldateOverAllDataResult()
    {
        try
        {
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
                    dr[0] = CheckBoxList1.Items[i].Value.ToString();

                    if (CheckBoxList2.Items[i].Selected == true)
                    {
                        dr[1] = CheckBoxList2.Items[i].Value.ToString();
                        strarr = CheckBoxList2.Items[i].Value.Split('_');
                        dr[1] = strarr[0];
                        dr[2] = strarr[1];
                    }
                    else
                    {
                        if (CheckBoxList3.Items[i].Selected == true)
                        {
                            dr[1] = CheckBoxList3.Items[i].Value.ToString();
                            strarr = CheckBoxList3.Items[i].Value.Split('_');
                            dr[1] = strarr[0];
                            dr[2] = strarr[1];
                        }
                    }

                    custDT.Rows.Add(dr);
                }

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
                sqlCmd.Parameters.AddWithValue("@spccode", Company_code);         
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
    protected void btn_ok_Click(object sender, EventArgs e)
    {
       // LoadPlantwiseSingleBilldateOverAllDataResult();
        LoadPlantwiseSingleBilldateOverAllDataResultORG();
    }
    private void LoadPlantwiseSingleBilldateOverAllDataResultORG()
    {
        try
        {
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
                    dr[0] = CheckBoxList1.Items[i].Value.ToString();

                    if (CheckBoxList2.Items[i].Selected == true)
                    {
                        dr[1] = CheckBoxList2.Items[i].Value.ToString();
                        strarr = CheckBoxList2.Items[i].Value.Split('_');
                        dr[1] = strarr[0];
                        dr[2] = strarr[1];
                    }
                    else
                    {
                        if (CheckBoxList3.Items[i].Selected == true)
                        {
                            dr[1] = CheckBoxList3.Items[i].Value.ToString();
                            strarr = CheckBoxList3.Items[i].Value.Split('_');
                            dr[1] = strarr[0];
                            dr[2] = strarr[1];
                        }
                    }

                    custDT.Rows.Add(dr);
                }

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
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_PlantwiseSingleBilldateOverAllDataResultORG]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                sqlCmd.Parameters.AddWithValue("@spccode", Company_code);
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
                                id = dr1[46].ToString();

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
                        string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "kg_Snf", "AvgSnf", "NillPayments", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Total Solid TS", "Procure Vale TS Rate", "Transport TS Rate", "Chilling Cost Per Ltr", "Chilling Cost TS Rate", "Total TS Rate", "kg milk gain", "kg fat gain", "kg snf gain", "Total Fat+Snf Gain", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %", "Fat Cost @ 175", "Snf Cost @ 150", "Total Fat+Snf Cost", "FatCost Ltrs", "SnfCost Ltrs", "Total Fat+Snf Cost Ltrs", "Fat & Snf PerTS", "Total Cost", "Net_TSRate" };//49
                      //  ReportDisplayItems();
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
               // Gridcolor();
            }
        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


    protected void btn_Load_Click(object sender, EventArgs e)
    {
        LoadPlantName();
    }

    private void Gridcolor()
    {
        try
        {
            int rowcount = 0;
            int[] colr = new int[5] {22, 27,31,44,45};
            for (int i = 0; i < 5; i++)
            {
                rowcount = colr[i];
                gvCustomers.Rows[rowcount].Cells[0].BackColor = Color.DarkSlateGray;
                gvCustomers.Rows[rowcount].Cells[0].ForeColor = Color.White;
                gvCustomers.Rows[rowcount].Cells[1].BackColor = Color.DarkSlateGray;
                gvCustomers.Rows[rowcount].Cells[1].ForeColor = Color.White;
                gvCustomers.Rows[rowcount].Cells[2].BackColor = Color.DarkSlateGray;
                gvCustomers.Rows[rowcount].Cells[2].ForeColor = Color.White;              
                
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }



    private void ReportDisplayItems()
    {
        List<String> CountryID_list = new List<string>();
        List<String> CountryName_list = new List<string>();
        int index = 0;
        foreach (System.Web.UI.WebControls.ListItem item in ddchkCountry.Items)
        {
            strarr1[index] = item.Value.ToString();
            //if (item.Selected)
            //{
            //    strarr1[index] = item.Value.ToString();
            //    //CountryID_list.Add(item.Value);
            //    //CountryName_list.Add(item.Text);
            //}
            //Lbl_Errormsg.Visible = true;
            //Lbl_Errormsg.Text = "Country ID: " + String.Join(",", CountryID_list.ToArray());
        }
    }


    


    protected void Button1_Click(object sender, EventArgs e)
    {


        try
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvCustomers.AllowPaging = false;
                LoadPlantwiseSingleBilldateOverAllDataResultORG();
                gvCustomers.DataBind();

                gvCustomers.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvCustomers.HeaderRow.Cells)
                {
                    cell.BackColor = gvCustomers.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvCustomers.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvCustomers.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvCustomers.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvCustomers.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                // HttpContext.Current.ApplicationInstance.CompleteRequest();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                string strRedirectUrl = Session["ReturnUrl"].ToString();
                Response.Redirect(strRedirectUrl, false);

            }
        }
        catch (Exception ee)
        {

            //   WebMsgBox.Show(ee.Message);
            //  Response.Redirect("Default.aspx");
        }






    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

           
}