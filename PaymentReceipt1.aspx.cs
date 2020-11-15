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
using System.IO;

public partial class PaymentReceipt1 : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public int rid;
    DateTime dtm = new DateTime();
    public string managmobNo;
    public string pname;
    public string cname;
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    SqlDataReader dr;
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    DbHelper DB = new DbHelper();
    //Admin Check Flag
    public int Falg = 0;
    public static int roleid;
    public static int buttonviewstatus;
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
               // txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
               // txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                getupdatestatus();
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && ( roleid!=9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();
                }

                Bdate();
                try
                {
                    loadrouteid();
                    rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                }
                catch
                {


                }
              //  GetPaymentReceipt();
                if (chk_All.Checked == true)
                {
                    lbl_RouteName.Visible = false;
                    ddl_RouteName.Visible = false;
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
                ccode = Session["Company_code"].ToString();
              //  pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
               // managmobNo = Session["managmobNo"].ToString();
               
                pcode = ddl_Plantcode.SelectedItem.Value;
                rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
               GetPaymentReceipt();
               getupdatestatus();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }


    }


    public void getupdatestatus()
    {

        string str = "";
        str = "Select  *    from  adminapproval   where plant_code='" + pcode + "'";
        SqlConnection con = new SqlConnection();
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {

                buttonviewstatus = Convert.ToInt16(dr["buttonviewstatus"].ToString());



            }



        }


    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
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
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode,pcode);
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

    private void Bdate()
    {
        try
        {
            dr = null;
            dr = BLLBill.Billdate(ccode, ddl_Plantcode.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");

                    Falg = Convert.ToInt32(dr["ViewStatus"].ToString());
                    //if (program.Guser_role >= program.Guser_PermissionId)
                    //{
                    //    Falg = 1;
                    //    Button1.Visible = true;
                    //    btn_Export.Visible = true;
                    //    btn_ExcelExport.Visible = true;

                    //}
                    //else
                    //{
                    //    if (Falg == 0)
                    //    {
                    //        Button1.Visible = false;
                    //        btn_Export.Visible = false;
                    //        btn_ExcelExport.Visible = false;

                    //    }
                    //    else
                    //    {
                    //        Button1.Visible = true;
                    //        btn_Export.Visible = true;
                    //        btn_ExcelExport.Visible = true;

                    //    }
                    //}


                    if (roleid > 3)
                    {
                        //  Falg = 1;
                        Button1.Visible = true;
                        btn_Export.Visible = true;
                        btn_ExcelExport.Visible = true;
                        chk_All.Visible=true;
                        Chk_bankcash.Visible=true;
                        chk_print.Visible = true;

                    }
                    else
                    {
                        if (buttonviewstatus == 2)
                        {
                            Button1.Visible = true;
                            btn_Export.Visible = true;
                            btn_ExcelExport.Visible = true;
                            chk_All.Visible = true;
                            Chk_bankcash.Visible = true;
                            chk_print.Visible = true;
                        }
                        else
                        {

                            Button1.Visible = false;
                            btn_Export.Visible = false;
                            btn_ExcelExport.Visible = false;
                            chk_All.Visible = false;
                            Chk_bankcash.Visible = false;
                            chk_print.Visible = false;
                        }
                    }

                }
            }
            else
            {
                WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        pcode = ddl_Plantcode.SelectedItem.Value;
        GetPaymentReceipt();
    }

    private void GetPaymentReceipt()
    {
        try
        {
            if (chk_All.Checked == true)
            {
                cr.Load(Server.MapPath("Report//Crpt_DBankPaymentabstract.rpt"));
                //cr.Load(Server.MapPath("Crpt_PaymentReceipt1.rpt"));
            }
            else
            {
                cr.Load(Server.MapPath("Report//Crpt_DBankPaymentabstractExcel.rpt"));
                // cr.Load(Server.MapPath("Crpt_PaymentReceiptExcel.rpt"));
            }
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            t1.Text = ccode + "_" + cname;
            t2.Text = ddl_Plantname.SelectedItem.Value;
            t3.Text = txt_FromDate.Text.Trim();
            t4.Text = "To : " + txt_ToDate.Text.Trim();
            
            
            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            
            // Payment_mode='BANK'


            //   code hide 20-3-2015'
//            if (Chk_bankcash.Checked == true)
//            {
//               //  recovery loan
//                //lastworking 02-03-2015  str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
//                str = "SELECT F.*,Plant.Pmail FROM " +
//               " (SELECT Plant_code,Route_id,Route_name,Agent_id,UPPER(REPLACE(Agent_name,'.',' ')) AS Agent_name,Payment_mode,Smltr,Smkg,SAmt,SInsentAmt,Scaramt,SSplBonus,TotDeductions,CAST(FLOOR(NetAmount) AS DECIMAL(18,2)) AS NetAmount,UPPER(Ifsc_code) AS Ifsc_code,Agent_accountNo,Bank_name FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "' AND Payment_mode='BANK' AND NetAmount>=1) AS F " +
//" INNER JOIN " +
//" (SELECT Plant_Code,Pmail FROM Plant_Master WHERE Plant_Code='" + pcode + "') AS Plant ON F.Plant_code=Plant.Plant_Code ORDER BY F.Route_id,F.Agent_id";
//            }
//            else
//            {
//                // recovery loan
//                //lastworking 02-03-2015  str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='CASH') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";                 
//                str = "SELECT F.*,Plant.Pmail FROM " +
//             " (SELECT Plant_code,Route_id,Route_name,Agent_id,UPPER(REPLACE(Agent_name,'.',' ')) AS Agent_name,Payment_mode,Smltr,Smkg,SAmt,SInsentAmt,Scaramt,SSplBonus,TotDeductions,CAST(FLOOR(NetAmount) AS DECIMAL(18,2)) AS NetAmount,UPPER(Ifsc_code) AS Ifsc_code,Agent_accountNo,Bank_name FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "' AND Payment_mode='CASH' AND NetAmount>=1) AS F " +
//" INNER JOIN " +
//" (SELECT Plant_Code,Pmail FROM Plant_Master WHERE Plant_Code='" + pcode + "') AS Plant ON F.Plant_code=Plant.Plant_Code ORDER BY F.Route_id,F.Agent_id";
//            }



            if (Chk_bankcash.Checked == true)
            {
                //  recovery loan
                //lastworking 02-03-2015  str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
                str = "SELECT F.*,Plant.Pmail FROM " +
               " (SELECT Plant_code,Route_id,Route_name,Agent_id,UPPER(REPLACE(CONVERT(NVARCHAR(35),Agent_name),'.',' ')) AS Agent_name,Payment_mode,Smltr,Smkg,SAmt,SInsentAmt,Scaramt,SSplBonus,TotDeductions,CAST(FLOOR(NetAmount) AS DECIMAL(18,2)) AS NetAmount,UPPER(REPLACE(Ifsc_code,'.',' ')) AS Ifsc_code,UPPER(REPLACE(Agent_accountNo,'.',' ')) AS Agent_accountNo,Bank_name FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "' AND Payment_mode='BANK' AND NetAmount>=1) AS F " +
" INNER JOIN " +
" (SELECT Plant_Code,Pmail FROM Plant_Master WHERE Plant_Code='" + pcode + "') AS Plant ON F.Plant_code=Plant.Plant_Code ORDER BY F.Route_id,F.Agent_id";
            }
            else
            {
                // recovery loan
                //lastworking 02-03-2015  str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='CASH') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";                 
                str = "SELECT F.*,Plant.Pmail FROM " +
             " (SELECT Plant_code,Route_id,Route_name,Agent_id,UPPER(REPLACE(CONVERT(NVARCHAR(35),Agent_name),'.',' ')) AS Agent_name,Payment_mode,Smltr,Smkg,SAmt,SInsentAmt,Scaramt,SSplBonus,TotDeductions,CAST(FLOOR(NetAmount) AS DECIMAL(18,2)) AS NetAmount,UPPER(REPLACE(Ifsc_code,'.',' ')) AS Ifsc_code,UPPER(REPLACE(Agent_accountNo,'.',' ')) AS Agent_accountNo,Bank_name FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "' AND Payment_mode='CASH' AND NetAmount>=1) AS F " +
" INNER JOIN " +
" (SELECT Plant_Code,Pmail FROM Plant_Master WHERE Plant_Code='" + pcode + "') AS Plant ON F.Plant_code=Plant.Plant_Code ORDER BY F.Route_id,F.Agent_id";
            }





            //if (Chk_bankcash.Checked == true)
            //{
            //    if (chk_All.Checked == true)
            //    {
            //        // recovery loan
            //        str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(loanDueRecAmount1,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";


            //        //    if (pcode == "160")
            //        //    {

            //        //        str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id<>8 GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id<>8  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";

            //        //    }
            //        //    else
            //        //    {
            //        //        //31-7-2014 work loandetails     str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //        //        // recovery loan
            //        //        str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(loanDueRecAmount1,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='CASH') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";

            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //        //}
            //    }
            //}
            //else
            //{
            //    if (chk_All.Checked == true)
            //    {
            //        // recovery loan
            //          str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(loanDueRecAmount1,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='CASH') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //    //    if (pcode == "160")
            //    //    {
            //    //        str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id<>8 GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id<>8  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='CASH') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //    //    }
            //    //    else
            //    //    {
            //    //        //31-7-2014 work loandetails     str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='CASH') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //    //        // recovery loan
            //    //       // str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro LEFT JOIN  (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN   (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(loanDueRecAmount1,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN     (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON Fin.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //    //          str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,ISNULL(F.VouAmount,0) AS ClaimAmoumt,F.Rid,F.Route_Name,F.BankID,F.Bankname,F.Pmail FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot   LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu   LEFT JOIN    (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS Status,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(loanDueRecAmount1,0)) AS instamt FROM (SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY Agent_id) AS Lonn LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + dt1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon    INNER JOIN    (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Bank_Id AS BankID,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF    LEFT JOIN    (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS VouAmount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + dt1.ToString().Trim() + "' AND '" + dt2.ToString().Trim() + "' GROUP BY Agent_Id ) AS VClaim ON FF.proAid=VClaim.VouAid) AS Fin LEFT JOIN (Select Bank_id AS Bid,Bank_Name AS Bankname from Bank_Details WHERE Company_code='" + ccode + "')AS Ban ON Fin.BankID=Ban.Bid) AS  proBan LEFT JOIN (Select plant_code,pmail from Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS Plant ON plant.Plant_Code>1 ) AS proBanPla LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON proBanPla.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    str = "SELECT F.proAid,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' and Route_id='" + rid + "' GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='CASH') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF LEFT JOIN (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
            //    //}
            //    }
            //}
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;

            if (chk_print.Checked == true)
            {
                cr.PrintToPrinter(1, true, 0, 0);
            }

            con.Close();
          
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

        while (dr.Read())
        {
            ddl_RouteName.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
            ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());

        }

    }
    protected void ddl_RouteID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteName.SelectedIndex = ddl_RouteID.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        GetPaymentReceipt();
    }

    protected void ddl_RouteName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
        GetPaymentReceipt();
    }
    protected void Chk_bankcash_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            GetPaymentReceipt();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

            DateTime frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string fdate = frmdate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
            DateTime todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string tdate = todate.ToString("dd" + "_" + "MM" + "_" + "yyyy");

            string CurrentCreateFolderName = fdate + "_" + tdate + "_" + DateTime.Now.ToString("ddMMyyyy");
            string path = @"C:\BILL VYSHNAVI\" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if(Chk_bankcash.Checked==true)
            {
                 CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "BankStatement.pdf";
            }
            else
            {
                 CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + pcode + "_" + "CashStatement.pdf";
            }

            CrExportOptions = cr.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            cr.Export();
            WebMsgBox.Show("Report Export Successfully...");

            //
             string MFileName=string.Empty;
              if(Chk_bankcash.Checked==true)
            {
                  MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "BankStatement.pdf";
            }
            else
            {
                  MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + pcode + CurrentCreateFolderName + "/" + ccode + "_" + pcode + "_" + "CashStatement.pdf";
            }
          
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(MFileName.ToString()))
            {
                //
                FileStream sourceFile = new FileStream(file.FullName, FileMode.Open);
                float FileSize;
                FileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)FileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                //
                Response.ClearContent(); // neded to clear previous (if any) written content
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.BinaryWrite(getContent);
                File.Delete(file.FullName.ToString());
                Response.Flush();
                Response.End();

            }
            else
            {

                Response.Write("File Not Found...");
            }
            //
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("Please Check the ExportPath...");
        }
    }
    protected void chk_All_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_All.Checked == true)
        {
            lbl_RouteName.Visible = false;
            ddl_RouteName.Visible = false;
        }
        else
        {
            //lbl_RouteName.Visible = true;
            //ddl_RouteName.Visible = true;
            lbl_RouteName.Visible = false;
            ddl_RouteName.Visible = false;

        }
    }
    protected void btn_ExcelExport_Click(object sender, EventArgs e)
    {
        try
        {
            GetPaymentReceipt();
            ExportOptions CrExportOptions;

            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = "C:\\kss.xls";
            CrExportOptions = cr.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.Excel;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            cr.Export();
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
        Bdate();
        GetPaymentReceipt();

    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        if (cr != null)
        {

            cr.Close();

            cr.Dispose();

            GC.Collect();

        }
    }
}
