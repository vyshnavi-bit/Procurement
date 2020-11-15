using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;

public partial class Rpt_PlantpermonthBilldateOverAllData : System.Web.UI.Page
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
    public int Valmilktype;
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
                tdt = System.DateTime.Now;
                txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
                txt_ToDate.Text = tdt.ToString("dd/MM/yyy");
                LoadReportDisplayItems();
                LoadReportDisplayItemsBuff();
                ddchkCountry.Visible = true;
                BuffDisplayItems.Visible = false;
                LoadPlantName();
                pcode = ddl_PlantName.SelectedItem.Value;
                Lbl_Errormsg.Visible = false;
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
                pcode = ddl_PlantName.SelectedItem.Value;
                Lbl_Errormsg.Visible = false;                     
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }


    private void LoadPlantName()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
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

    private void LoadReportDisplayItemsBuff()
    {
        try
        {
            BuffDisplayItems.Items.Clear();
            string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Kg Fat Rate", "Plant OverHeads PerLtr", "OverHeads Amount", "kg milk gain", "kg fat gain", "Fat Gain % perLtr", "AvgFat GainFat %", "Fat Cost ", "FatCost Ltrs", "Net_TSRate" };//28-ORG
            int cou = strarr1.Count();
            for (int i = 0; i <= cou; i++)
            {
                BuffDisplayItems.Items.Add(strarr1[i]);
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
            if (MChk_Date1.Checked == true)
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
   
    protected void MChk_Date1_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }

    private void LoadMonthBillDate()
    {
        try
        {
            Datechanged();
           // TOP 3 Records only strsql = "SELECT Plant_Code,Plant_Code AS pcode,Bill_frmdate,Bill_todate,Convert(nvarchar(15),Bill_frmdate,103)+'_'+Convert(nvarchar(15),Bill_todate,103) AS FrmTodate,CONVERT(NVARCHAR,CAST(CONVERT(Nvarchar,Bill_frmdate) AS date))+'_'+CONVERT(NVARCHAR,CAST(CONVERT(Nvarchar,Bill_todate) AS date)) AS FrmTodate1,Tid  FROM(SELECT Top 4 *  FROM Bill_date Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.ToString().Trim() + "'  AND Bill_frmdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'   order by Tid desc ) AS t1 ORDER  BY Tid";
            strsql = "SELECT Plant_Code,Plant_Code AS pcode,Bill_frmdate,Bill_todate,Convert(nvarchar(15),Bill_frmdate,103)+'_'+Convert(nvarchar(15),Bill_todate,103) AS FrmTodate,CONVERT(NVARCHAR,CAST(CONVERT(Nvarchar,Bill_frmdate) AS date))+'_'+CONVERT(NVARCHAR,CAST(CONVERT(Nvarchar,Bill_todate) AS date)) AS FrmTodate1,Tid  FROM(SELECT TOP 3  *  FROM Bill_date Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.ToString().Trim() + "'  AND Bill_frmdate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "'   order by Tid desc ) AS t1 ORDER  BY Tid";
            using (con = DBaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                da.Fill(ds);
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "FrmTodate";
                CheckBoxList1.DataValueField = "FrmTodate1";
                CheckBoxList1.DataBind();
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
        LoadMonthBillDate();
    }
    private void LoadPlantmonthlyBilldateOverAllDataResult()
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
                    dr[0] = ddl_PlantName.SelectedItem.Value.ToString().Trim();
                    strarr = CheckBoxList1.Items[i].Value.Split('_');
                    dr[1] = strarr[0];
                    dr[2] = strarr[1];
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
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);

                //
                DataTable ksdt = new DataTable();
                DataColumn ksdc = null;
                DataRow ksdr = null;
                int counts = dts.Rows.Count;


                #region First OK Start

                //// START ADDING COLUMN
                //if (counts > 0)
                //{
                //    ksdc = new DataColumn("Period");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Scans");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Smkg");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Smltr");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Sfatkg");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Ssnfkg");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Aclr");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Samt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SInsentAmt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Scaramt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SSplBonus");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SClaimAmt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SBilladv");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SAi");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SFeed");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Scan");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SRecovery");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Sinstamt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SLoanClobal");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("TotAdd");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("STotdedu");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SLoanAmt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SRoundoff");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SNetAmt");
                //    ksdt.Columns.Add(ksdc);

                //}
                //// END ADDING COLUMN

                //// START ADDING ROWS
                //if (counts > 0)
                //{
                //    object id2;
                //    id2 = 0;
                //    int idd2 = Convert.ToInt32(id2);

                //    foreach (DataRow dr2 in dts.Rows)
                //    {
                //        ksdr = ksdt.NewRow();
                //        ksdr[0] = dr2["Plant_code"].ToString();
                //        ksdr[1] = dr2["Scans"].ToString();
                //        ksdr[2] = dr2["Smkg"].ToString();
                //        ksdr[3] = dr2["Smltr"].ToString();
                //        ksdr[4] = dr2["Sfatkg"].ToString();
                //        ksdr[5] = dr2["Ssnfkg"].ToString();
                //        ksdr[6] = dr2["Aclr"].ToString();
                //        ksdr[7] = dr2["Samt"].ToString();
                //        ksdr[8] = dr2["SInsentAmt"].ToString();
                //        ksdr[9] = dr2["Scaramt"].ToString();
                //        ksdr[10] = dr2["SSplBonus"].ToString();
                //        ksdr[11] = dr2["SClaimAount"].ToString();
                //        ksdr[12] = dr2["SBilladv"].ToString();
                //        ksdr[13] = dr2["SAi"].ToString();
                //        ksdr[14] = dr2["SFeed"].ToString();
                //        ksdr[15] = dr2["Scan"].ToString();
                //        ksdr[16] = dr2["SRecovery"].ToString();
                //        ksdr[17] = dr2["Sinstamt"].ToString();
                //        ksdr[18] = dr2["SLoanClosingbalance"].ToString();
                //        ksdr[19] = dr2["TotAdditions"].ToString();
                //        ksdr[20] = dr2["STotdeductions"].ToString();
                //        ksdr[21] = dr2["SLoanAmount"].ToString();
                //        ksdr[22] = dr2["SRoundoff"].ToString();
                //        ksdr[23] = dr2["SNetAmount"].ToString();

                //        ksdt.Rows.Add(ksdr);
                //    }
                //}
                //// END ADDING ROWS
                #endregion

                #region Second OK Start

                // START ADDING COLUMN
                if (counts > 0)
                {
                    ksdc = new DataColumn("Details");
                    ksdt.Columns.Add(ksdc);
                    int colinc = 1;
                    foreach (DataRow dr1 in dts.Rows)
                    {
                        object id;
                        id = dr1[24].ToString();

                        string columnName = id.ToString();
                        DataColumnCollection columns = ksdt.Columns;
                        if (columns.Contains(columnName))
                        {
                            id = id + "  " + Convert.ToString(colinc) + "_" + "Period";
                            ksdc = new DataColumn(id.ToString());
                            ksdt.Columns.Add(ksdc);
                            colinc++;
                        }
                        else
                        {
                            if (id.ToString().Trim() == ".".Trim())
                            {
                                ksdc = new DataColumn(id.ToString());
                                ksdt.Columns.Add(ksdc);
                            }
                            else
                            {
                                ksdc = new DataColumn(id + "  " + Convert.ToString(colinc) + "_" + "Period");
                                ksdt.Columns.Add(ksdc);
                                colinc++;
                            }
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
        //LoadPlantmonthlyBilldateOverAllDataResult();
          LoadPlantmonthlyBilldateOverAllDataResultORG();
    }

    private void LoadPlantmonthlyBilldateOverAllDataResultORG()
    {
        try
        {
            string Tit = string.Empty;
            Tit = "PLANT MONTHLY PROCUREMENT BILL REPORT".Trim();
            Label1.Text = " ";
            Label1.Text = ddl_PlantName.SelectedItem.Text.Trim() + "_" + Tit;

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
                    dr[0] = ddl_PlantName.SelectedItem.Value.ToString().Trim();
                    strarr = CheckBoxList1.Items[i].Value.Split('_');
                    dr[1] = strarr[0];
                    dr[2] = strarr[1];
                    custDT.Rows.Add(dr);
                }
            }

            Valmilktype = DBaccess.GetPlantMilktype(ddl_PlantName.SelectedItem.Value);
            DataTable dts = new DataTable();
            SqlParameter param = new SqlParameter();
            param.ParameterName = "CustDtPlantcode";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = custDT;
            param.Direction = ParameterDirection.Input;
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            string sp = string.Empty;
            using (conn = new SqlConnection(dbConnStr))
            {

                if (Valmilktype == 1)
                {
                    sp = "dbo.[Get_PlantwiseSingleBilldateOverAllDataResultORG]";
                }
                else
                {
                    sp = "dbo.[Get_PlantwiseSingleBilldateOverAllDataResultORG_B]";
                }
                SqlCommand sqlCmd = new SqlCommand(sp);
               

                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                //@TsorCosting Here Not Using
                sqlCmd.Parameters.AddWithValue("@TsorCosting", 0);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);

                //
                DataTable ksdt = new DataTable();
                DataColumn ksdc = null;
                DataRow ksdr = null;
                int counts = dts.Rows.Count;


                #region First OK Start

                //// START ADDING COLUMN
                //if (counts > 0)
                //{
                //    ksdc = new DataColumn("Period");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Scans");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Smkg");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Smltr");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Sfatkg");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Ssnfkg");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Aclr");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Samt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SInsentAmt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Scaramt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SSplBonus");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SClaimAmt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SBilladv");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SAi");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SFeed");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Scan");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SRecovery");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("Sinstamt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SLoanClobal");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("TotAdd");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("STotdedu");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SLoanAmt");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SRoundoff");
                //    ksdt.Columns.Add(ksdc);
                //    ksdc = new DataColumn("SNetAmt");
                //    ksdt.Columns.Add(ksdc);

                //}
                //// END ADDING COLUMN

                //// START ADDING ROWS
                //if (counts > 0)
                //{
                //    object id2;
                //    id2 = 0;
                //    int idd2 = Convert.ToInt32(id2);

                //    foreach (DataRow dr2 in dts.Rows)
                //    {
                //        ksdr = ksdt.NewRow();
                //        ksdr[0] = dr2["Plant_code"].ToString();
                //        ksdr[1] = dr2["Scans"].ToString();
                //        ksdr[2] = dr2["Smkg"].ToString();
                //        ksdr[3] = dr2["Smltr"].ToString();
                //        ksdr[4] = dr2["Sfatkg"].ToString();
                //        ksdr[5] = dr2["Ssnfkg"].ToString();
                //        ksdr[6] = dr2["Aclr"].ToString();
                //        ksdr[7] = dr2["Samt"].ToString();
                //        ksdr[8] = dr2["SInsentAmt"].ToString();
                //        ksdr[9] = dr2["Scaramt"].ToString();
                //        ksdr[10] = dr2["SSplBonus"].ToString();
                //        ksdr[11] = dr2["SClaimAount"].ToString();
                //        ksdr[12] = dr2["SBilladv"].ToString();
                //        ksdr[13] = dr2["SAi"].ToString();
                //        ksdr[14] = dr2["SFeed"].ToString();
                //        ksdr[15] = dr2["Scan"].ToString();
                //        ksdr[16] = dr2["SRecovery"].ToString();
                //        ksdr[17] = dr2["Sinstamt"].ToString();
                //        ksdr[18] = dr2["SLoanClosingbalance"].ToString();
                //        ksdr[19] = dr2["TotAdditions"].ToString();
                //        ksdr[20] = dr2["STotdeductions"].ToString();
                //        ksdr[21] = dr2["SLoanAmount"].ToString();
                //        ksdr[22] = dr2["SRoundoff"].ToString();
                //        ksdr[23] = dr2["SNetAmount"].ToString();

                //        ksdt.Rows.Add(ksdr);
                //    }
                //}
                //// END ADDING ROWS
                #endregion

               // #region Second OK Start1

               // // START ADDING COLUMN
               // if (counts > 0)
               // {
               //     ksdc = new DataColumn("Details");
               //     ksdt.Columns.Add(ksdc);
               //     int colinc = 1;
               //     foreach (DataRow dr1 in dts.Rows)
               //     {
               //         object id;
               //         id = dr1[0].ToString();

               //         string columnName = id.ToString();
               //         DataColumnCollection columns = ksdt.Columns;
               //         if (columns.Contains(columnName))
               //         {
               //             //id = id + "  " + Convert.ToString(colinc) + "_" + "Period";
                            
               //             ksdc = new DataColumn(id.ToString());
               //             ksdt.Columns.Add(ksdc);
               //             colinc++;
               //         }
               //         else
               //         {
               //             if (id.ToString().Trim() == ".".Trim())
               //             {
               //                 ksdc = new DataColumn(id.ToString());
               //                 ksdt.Columns.Add(ksdc);
               //             }
               //             else
               //             {
               //               //  ksdc = new DataColumn(id + "  " + Convert.ToString(colinc) + "_" + "Period");
               //                  ksdc = new DataColumn(id.ToString());
               //                 ksdt.Columns.Add(ksdc);
               //                 colinc++;
               //             }
               //         }

               //     }
               // }
               // // END ADDING COLUMN


               // // START ADDING ROWS
               //// string[] strarr1 = new string[] { "Period", "Scans", "Smkg", "Smltr", "Sfatkg", "Ssnfkg", "Aclr", "Samt", "SInsentAmt", "Scaramt", "SSplBonus", "SClaimAount", "SBilladv", "SAi", "SFeed", "Scan", "SRecovery", "Sinstamt", "SLoanClobal", "TotAdd", "STotdedu", "SLoanAmt", "SRoundoff", "SNetAmt" };
               // string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "kg_Snf", "AvgSnf", "NillPayments", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Total Solid TS", "Procure Vale TS Rate", "Transport TS Rate", "Chilling Cost Per Ltr", "Chilling Cost TS Rate", "Total TS Rate", "kg milk gain", "kg fat gain", "kg snf gain", "Total Fat+Snf Gain", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %", "Fat Cost @ 175", "Snf Cost @ 150", "Total Fat+Snf Cost", "FatCost Ltrs", "SnfCost Ltrs", "Total Fat+Snf Cost Ltrs", "Fat & Snf PerTS", "Total Cost", "Net_TSRate" };//49
               // if (counts > 0)
               // {
               //     int i = 0;

               //     for (int colcount = dts.Columns.Count - 1; colcount > 0; colcount--)
               //     {
               //         ksdr = ksdt.NewRow();
               //         ksdr[0] = strarr1[i].ToString();
               //         int j = 1;
               //         foreach (DataRow dr2 in dts.Rows)
               //         {
               //             ksdr[j] = dr2[i].ToString();
               //             j++;
               //         }
               //         i++;
               //         ksdt.Rows.Add(ksdr);
               //     }
               // }
               // // END ADDING ROWS

               // #endregion

                #region Second OK Start

                // START ADDING COLUMN
                if (counts > 0)
                {
                    ksdc = new DataColumn("Details");
                    ksdt.Columns.Add(ksdc);
                    int colinc = 1;
                    foreach (DataRow dr1 in dts.Rows)
                    {
                        object id;
                        id = dr1[0].ToString();

                        string columnName = id.ToString();
                        DataColumnCollection columns = ksdt.Columns;
                        if (columns.Contains(columnName))
                        {
                            //id = id + "  " + Convert.ToString(colinc) + "_" + "Period";

                            ksdc = new DataColumn(id.ToString());
                            ksdt.Columns.Add(ksdc);
                            colinc++;
                        }
                        else
                        {
                            if (id.ToString().Trim() == ".".Trim())
                            {
                                ksdc = new DataColumn(id.ToString());
                                ksdt.Columns.Add(ksdc);
                            }
                            else
                            {
                                //  ksdc = new DataColumn(id + "  " + Convert.ToString(colinc) + "_" + "Period");
                                ksdc = new DataColumn(id.ToString());
                                ksdt.Columns.Add(ksdc);
                                colinc++;
                            }
                        }

                    }
                }
                // END ADDING COLUMN


                // START ADDING ROWS
                //COW
                if (Valmilktype == 1)
                {
                    // string[] strarr1 = new string[] { "Period", "Scans", "Smkg", "Smltr", "Sfatkg", "Ssnfkg", "Aclr", "Samt", "SInsentAmt", "Scaramt", "SSplBonus", "SClaimAount", "SBilladv", "SAi", "SFeed", "Scan", "SRecovery", "Sinstamt", "SLoanClobal", "TotAdd", "STotdedu", "SLoanAmt", "SRoundoff", "SNetAmt" };
                    string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "kg_Snf", "AvgSnf", "NillPayments", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Total Solid TS", "Procure Vale TS Rate", "Transport TS Rate", "Chilling Cost Per Ltr", "Chilling Cost TS Rate", "Total TS Rate", "kg milk gain", "kg fat gain", "kg snf gain", "Total Fat+Snf Gain", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %", "Fat Cost @ 175", "Snf Cost @ 150", "Total Fat+Snf Cost", "FatCost Ltrs", "SnfCost Ltrs", "Total Fat+Snf Cost Ltrs", "Fat & Snf PerTS", "Total Cost", "Net_TSRate" };//49
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
                }
                else
                {
                    //Buff
                   // string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "kg_Snf", "AvgSnf", "NillPayments", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Total Solid TS", "Procure Vale TS Rate", "Transport TS Rate", "Chilling Cost Per Ltr", "Chilling Cost TS Rate", "Total TS Rate", "kg milk gain", "kg fat gain", "kg snf gain", "Total Fat+Snf Gain", "Fat Gain % perLtr", "Snf Gain % perLtr", "Total Gain % perLtr", "AvgFat GainFat %", "AvgSnf GainSnf %", "Total AvgGainSnf %", "Fat Cost @ 175", "Snf Cost @ 150", "Total Fat+Snf Cost", "FatCost Ltrs", "SnfCost Ltrs", "Total Fat+Snf Cost Ltrs", "Fat & Snf PerTS", "Total Cost", "Net_TSRate" };//49
                    string[] strarr1 = new string[] { "Period", "TotalMkgs", "TotalMltrs", "PerDayMilkMltrs", "LtrCost", "kg_Fat", "AvgFat ", "MilkValueAmout", "BillAdv", "AiAdv", "FeedAdv", "CanAdv", "Recovery", "LoanAdv", "loanperltrdedu", "NetBillAmount", "TransportAmount", "Transport Ltr Cost", "Kg Fat Rate", "Plant OverHeads PerLtr", "OverHeads Amount", "kg milk gain", "kg fat gain", "Fat Gain % perLtr", "AvgFat GainFat %", "Fat Cost ", "FatCost Ltrs", "Net_TSRate" };//38-ORG
                    if (counts > 0)
                    {
                        int i = 0;

                        foreach (System.Web.UI.WebControls.ListItem item in BuffDisplayItems.Items)
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
                }
                // END ADDING ROWS

                #endregion



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


    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        plantmilktype();
    }

    private void plantmilktype()
    {
        try
        {
            Valmilktype = DBaccess.GetPlantMilktype(ddl_PlantName.SelectedItem.Value);

            if (Valmilktype == 1)
                {          
                    ddchkCountry.Visible = true;
                    BuffDisplayItems.Visible = false;
                }
                else
                {           
                    ddchkCountry.Visible = false;
                    BuffDisplayItems.Visible = true;
                }    

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

  
}