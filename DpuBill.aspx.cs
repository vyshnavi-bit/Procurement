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


public partial class DpuBill : System.Web.UI.Page
{
    SqlDataReader dr;
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    BOLbillgenerate BOLBill = new BOLbillgenerate();
    BLLroutmaster routmasterBL = new BLLroutmaster();
    BLLpayment Bllpay = new BLLpayment();
    BLLuser Bllusers = new BLLuser();
    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new ReportDocument();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string frmdate;
    public string Todate;
    public int btnvalloanupdate = 0;
    DateTime dtm = new DateTime();
    public static int buttonviewstatus;
    //Admin Check Flag
    public int Falg = 0;
    public static int roleid;
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;

    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    string getid;
    string getvald;
    string getvalm;
    string getvaly;
    string getvald1;
    string getvalm1;
    string getvaly1;

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
            //    managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");


                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }

                billdate();

                getupdatestatus();

                if (roleid >= 4)
                {
                    //  Falg = 1;
                    btn_ok.Visible = true;
                    btn_Export.Visible = true;

                    if (roleid == 4)
                    {
                    txt_FromDate.Enabled = false;
                    txt_ToDate.Enabled = false;
                    }
                    else
                    {
                    txt_FromDate.Enabled = false;
                    txt_ToDate.Enabled = false;
                    }
                }

                
                else
                {
                    if (buttonviewstatus == 2)
                    {
                        btn_ok.Visible = true;
                        btn_Export.Visible = true;
                    }
                    else
                    {

                        btn_ok.Visible = false;
                        btn_Export.Visible = false;
                    }
                }






              //  Bdate();

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
              //  managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_Plantcode.SelectedItem.Value;

                //getupdatestatus();
                //billdate();
                if (roleid >= 4)
                {
                    //  Falg = 1;
                   
                    btn_ok.Visible = true;
                    btn_Export.Visible = true;


                    if (roleid == 4)
                    {
                        txt_FromDate.Enabled = false;
                        txt_ToDate.Enabled = false;
                    }
                    else
                    {
                        txt_FromDate.Enabled = true;
                        txt_ToDate.Enabled = true;
                    }


                }
                else
                {
                    if (buttonviewstatus == 2)
                    {
                        btn_ok.Visible = true;
                        btn_Export.Visible = true;
                    }
                    else
                    {

                        btn_ok.Visible = false;
                        btn_Export.Visible = false;
                    }
                }

                if (btnvalloanupdate == 1)
                {
                    LoadBill();

                }
                else
                {
                    LoadBill();
                }

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
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
            txt_PlantPhoneNo.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                    txt_PlantPhoneNo.Items.Add(dr["Mana_PhoneNo"].ToString());
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
            txt_PlantPhoneNo.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                    txt_PlantPhoneNo.Items.Add(dr["Mana_PhoneNo"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    public void getupdatestatus()
    {
        string str = "";
        str = "Select  *    from  adminapproval   where plant_code='" + pcode + "'";
        SqlConnection con = new SqlConnection();
        con = dbaccess.GetConnection();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                buttonviewstatus = Convert.ToInt16(dr["Dpustatus"].ToString());

            }
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        LoadBill();
    }

    private void SETBO()
    {
        BOLBill.Companycode = int.Parse(ccode);
        BOLBill.Plantcode = int.Parse(ddl_Plantcode.SelectedItem.Value);
        BOLBill.Frmdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        BOLBill.Todate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        txt_PlantPhoneNo.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        billdate();
      //  Bdate();
        LoadBill();
    }

    private void LoadBill()
    {
        try
        {
            SETBO();

            //if (Chk_MilkType.Checked == true)
            //{
            //    cr.Load(Server.MapPath("Report//BillDBuff.rpt"));
            //}
            //else
            //{
            //    cr.Load(Server.MapPath("Report//BillD.rpt"));
            //}
            pcode = ddl_Plantcode.SelectedItem.Value;
            //Plantmilktype = dbaccess.GetPlantMilktype(pcode);
            //if (Plantmilktype == 2)
            //{
            //    cr.Load(Server.MapPath("Report//BillDBuff.rpt"));
            //}
            //else
            //{
            //    cr.Load(Server.MapPath("Report//BillD.rpt"));
            //}
            cr.Load(Server.MapPath("Report//DpuBill.rpt"));
            cr.SetDatabaseLogon("onlinemilktest.in", "AMPS");
            CrystalDecisions.CrystalReports.Engine.TextObject t1;
            CrystalDecisions.CrystalReports.Engine.TextObject t2;
            CrystalDecisions.CrystalReports.Engine.TextObject t3;
            CrystalDecisions.CrystalReports.Engine.TextObject t4;
            CrystalDecisions.CrystalReports.Engine.TextObject t5;

            t1 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_CompanyName"];
            t2 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_plantName"];
            t3 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Fromdate"];
            t4 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_Todate"];
            //t5 = (TextObject)cr.ReportDefinition.Sections[0].ReportObjects["txt_phoneno"];                             

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            //dt1 = DateTime.ParseExact(FDATE, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(TODATE, "dd/MM/yyyy", null);

            t1.Text = ccode + "_" + cname;
            t2.Text = ddl_Plantname.SelectedItem.Value + "_PhoneNo :" + txt_PlantPhoneNo.Text.Trim();
           // t5.Text = managmobNo;
            //FDATE = getvalm + "/" + getvald + "/" + getvaly;
            //TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            getdatefuntion();
            //dt1 = DateTime.ParseExact(FDATE, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(TODATE, "dd/MM/yyyy", null);
            //string d1 = dt1.ToString("MM/dd/yyyy");
            //string d2 = dt2.ToString("MM/dd/yyyy");

            t3.Text = FDATE;
         //   t4.Text = "To : " + d2.Trim();
            t4.Text = "To : " + TODATE;

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);

            //31-7-2014 work str = "SELECT R.Route_name,F.proAid,CONVERT(VARCHAR,Prdate,103) AS prdate,F.Sessions,ISNULL(F.Mltr,0) AS Mltr,ISNULL(F.Milk_kg,0) AS Milk_kg,ISNULL(F.Fat,0) AS Fat,ISNULL(F.Snf,0) AS snf,ISNULL(F.Clr,0) AS Clr,ISNULL(F.Rate,0) AS Rate,ISNULL(F.ComRate,0) AS ComRate,ISNULL(F.Amount,0) AS Amount,ISNULL(F.Fatkg,0) AS Fatkg,ISNULL(F.Snfkg,0) AS Snfkg,F.Sampleid,F.RateChart_id,F.MilkType,F.ccode,F.pcode,F.Rid,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.Scans,0) AS Scans,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,Prdate,Sessions,Mltr,Milk_kg,Fat,Snf,Clr,Rate,ComRate,Amount,Fatkg,Snfkg,Sampleid,RateChart_id,MilkType,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT Agent_Id AS proAid ,Prdate,Sessions,(CAST((Milk_ltr) AS DECIMAL(18,2))) AS Mltr,CAST(Milk_kg AS DECIMAL(18,2)) AS Milk_kg,(CAST((Fat) AS DECIMAL(18,2))) AS Fat,(CAST((Snf)AS DECIMAL(18,2))) Snf,CAST(Clr AS DECIMAL(18,2)) AS clr,CAST(NoofCans AS DECIMAl(18,2)) AS Cans,(CAST((Rate) AS DECIMAL(18,2))) AS Rate,CAST(ComRate AS DECIMAL(18,2)) AS ComRate,(CAST((Amount) AS DECIMAL(18,2)))AS Amount,CAST(Fat_kg AS DECIMAL(18,2)) AS Fatkg,CAST(Snf_kg AS DECIMAL(18,2)) AS Snfkg,NoofCans AS SampleId,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid FROM Procurement WHERE PRDATE BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "'  ) AS pro LEFT JOIN (SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE prdate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' GROUP BY agent_id ) AS Spro ON pro.proAid=Spro.SproAid ) AS protot LEFT JOIN  (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + d1.Trim() + "' AND '" + d2.Trim() + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,CAST(SUM(balance) AS DECIMAL(18,2)) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>1 GROUP BY agent_id) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON prodedulon.proAid=cart.cartAid ) AS F LEFT JOIN (SELECT route_id,Route_name,plant_code,company_code FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F.Rid=R.route_id  ORDER BY  F.proAid,F.Prdate,F.Sessions,R.route_id ";
 //        //   str = "SELECT t1.*,t2.*  FROM " +
 //" (SELECT  Route_id,Route_name,Agent_id,Agent_name,Bank_id,Bank_name,Agent_accountNo,Ifsc_code,Milktype,Super_PhoneNo,SInsentAmt,Scaramt,SSplBonus,ClaimAount,SLoanAmount,Billadv,Ai,Feed,can,Recovery,others,SLoanClosingbalance,SAmt,TotAdditions,TotDeductions,Sinstamt,UPPER(Words) AS Words,Roundoff,Smkg,Smltr,Sfatkg,SSnfkg,Aclr,Scans ,NetAmount FROM paymentdata WHERE PLANT_CODE='" + ddl_Plantcode.SelectedItem.Value.Trim() + "' AND Frm_date='" + d1.ToString() + "' AND To_date='" + d2.ToString() + "') AS t1 " +
 //" INNER JOIN " +
 //" (SELECT Agent_id,CONVERT(VARCHAR,Prdate,103) AS Prdate,Sessions,ISNULL(NoofCans,0) AS NoofCans,ISNULL(Milk_kg,0) AS Milk_kg,ISNULL(Milk_ltr,0) AS Milk_ltr,ISNULL(Fat,0) AS Fat,ISNULL(fat_kg,0) AS fat_kg,ISNULL(Snf,0) AS Snf,ISNULL(snf_kg,0) AS snf_kg,ISNULL(Clr,0) AS Clr,ISNULL(Rate,0) AS Rate,ISNULL(Amount,0) AS Amount,ISNULL(ComRate,0) AS ComRate  from Procurement Where Plant_code='" + ddl_Plantcode.SelectedItem.Value.Trim() + "' AND Prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "') AS t2 ON t1.Agent_id=t2.Agent_id ORDER BY  t1.Route_id,t1.Agent_id,t2.Prdate,t2.Sessions ";


 //str = "select table1.Tid,table1.Agent_id,prdate,Sessions,milk_ltr,Fat,Snf,Rate,Amount,table1.Plant_code,table1.Route_id,Noofcans,Milk_kg,clr,table1.company_code,Ratechart_Id,comrate,sampleId,sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,truck_id,status,cartageAmount,splBonusAmount,RateStatus,Producer_Id,Route_Name,producer_name,type,bank_Id,payment_mode,Agent_AccountNo,AddedDate,Phone_number,dpu.Milk_nature,ifsc_code from( select  Tid,Agent_id,prdate,Sessions,milk_ltr,Fat,Snf,Rate,Amount,pprm.Plant_code,Route_id,Noofcans,Milk_kg,clr,company_code,Ratechart_Id,Milk_nature,comrate,sampleId,sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,truck_id,status,cartageAmount,splBonusAmount,RateStatus,Producer_Id,Route_Name from( select Tid,Agent_id,prdate,Sessions,milk_ltr,Fat,Snf,Rate,Amount,pp.Plant_code,pp.Route_id,Noofcans,Milk_kg,clr,company_code,Ratechart_Id,Milk_nature,comrate,sampleId,sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,truck_id,status,cartageAmount,splBonusAmount,RateStatus,Producer_Id,Route_Name from(select    *   from  ProducerProcurement  where prdate between '"+d1+"' and '"+d2+"' and plant_code='"+pcode+"' ) as pp left join(select route_Name,route_id   from route_master  where plant_code='"+pcode+"') as rm on pp.route_id=rm.route_id) as pprm left join (select plant_code,plant_Name from plant_master where plant_code='"+pcode+"') as pm on pprm.plant_code=pm.plant_code) as table1 left join ( select  *   from  DPUPRODUCERMASTER   where plant_code='"+pcode+"' ) as dpu on  table1.Producer_Id=dpu.producer_code   order by table1.agent_id,table1.prdate";
        //    str = "select table1.Tid,table1.Agent_id,prdate,Sessions,milk_ltr,Fat,Snf,Rate,Amount,table1.Plant_code,table1.Route_id,Noofcans,Milk_kg,clr,table1.company_code,Ratechart_Id,comrate,sampleId,sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,truck_id,cartageAmount,splBonusAmount,RateStatus,Producer_Id,Route_Name,producer_name,type,bank_Id,payment_mode,Agent_AccountNo,AddedDate,Phone_number,dpu.Milk_nature,ifsc_code from( select  Tid,Agent_id,prdate,Sessions,milk_ltr,Fat,Snf,Rate,Amount,pprm.Plant_code,Route_id,Noofcans,Milk_kg,clr,company_code,Ratechart_Id,Milk_nature,comrate,sampleId,sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,truck_id,cartageAmount,splBonusAmount,RateStatus,Producer_Id,Route_Name from( select Tid,Agent_id,prdate,Sessions,milk_ltr,Fat,Snf,Rate,Amount,pp.Plant_code,pp.Route_id,Noofcans,Milk_kg,clr,company_code,Ratechart_Id,Milk_nature,comrate,sampleId,sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,truck_id,cartageAmount,splBonusAmount,RateStatus,Producer_Id,Route_Name from(select    *   from  ProducerProcurement  where prdate between '" + d1 + "' and '" + d2 + "' and plant_code='" + pcode + "'  AND ((Fat  not in (0)) or (Snf not in(0))) ) as pp left join(select route_Name,route_id   from route_master  where plant_code='" + pcode + "') as rm on pp.route_id=rm.route_id) as pprm left join (select plant_code,plant_Name from plant_master where plant_code='" + pcode + "') as pm on pprm.plant_code=pm.plant_code) as table1 left join ( select  *   from  DPUPRODUCERMASTER   where plant_code='" + pcode + "' ) as dpu on  table1.Producer_Id=dpu.producer_code   order by table1.agent_id,table1.prdate";

        //    getdatefuntion();
           
            str = " select Tid,Agent_id,prdate,Sessions,milk_ltr,Fat,Snf,Rate,Amount,Plant_code,Route_id,Noofcans,Milk_kg,clr,company_code,Ratechart_Id,comrate,sampleId,sampleno,milk_tip_st_time,milk_tip_end_time,usr_weigher,usr_analizer,fat_kg,snf_kg,truck_id,cartageAmount,splBonusAmount,RateStatus,Producer_Id,Route_Name,producer_name,type,bank_Id,payment_mode,Agent_AccountNo,AddedDate,Phone_number,dpumasmilnature AS Milk_nature,ifsc_code from (select * from(select *  from (select    *   from  ProducerProcurement  where prdate between '" + FDATE + "' and '" + TODATE + "' and plant_code='" + pcode + "'  AND ((Fat  not in (0)) or (Snf not in(0))))    as pp  left join (select route_Name,route_id as routerid,plant_code  as pcode  from route_master  where plant_code='" + pcode + "' group by route_Name,route_id,plant_code ) as rm on pp.Plant_Code=rm.pcode and pp.Route_id=rm.routerid  ) as ff left join (select Plant_code as dpumaspcode,Producer_Code,Agent_Id as dpumasagentid,producer_name,type,bank_Id,payment_mode,Agent_AccountNo,AddedDate,Phone_number,Milk_nature as dpumasmilnature,ifsc_code   from  DPUPRODUCERMASTER    where plant_code='" + pcode + "' group by Producer_Code,producer_name,type,bank_Id,payment_mode,Agent_AccountNo,AddedDate,Phone_number,Milk_nature,ifsc_code,Plant_code,Agent_Id) as dpumas on ff.Plant_Code=dpumas.dpumaspcode and ff.Agent_id=dpumas.dpumasagentid and ff.Producer_Id=dpumas.Producer_Code) as leftside left join  (select plant_code as pmpcode,plant_Name from plant_master  where plant_code='" + pcode + "' group by  plant_code,plant_Name) as pm on leftside.Plant_Code = pm.pmpcode            order by      agent_id,Producer_Id,prdate,Sessions"; 

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cr.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = cr;
        }
        catch (Exception ex)
        {

            WebMsgBox.Show(ex.ToString());
        }

    }

    //public void billdate()
    //{
    //    try
    //    {
    //        DateTime d1;
    //        DateTime d2;
    //        DateTime d11;
    //        DateTime d22;

    //        con.Close();
    //        con =  dbaccess.GetConnection();
    //        //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
    //        string str;
    //        if (roleid == 4)
    //        {
    //            str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'  and Adminupdate=1  order by  tid asc";
    //        }
    //        else
    //        {

    //            str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'  order by  tid asc";

    //        }
    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {
    //            txt_FromDate.Text = "";
    //            txt_ToDate.Text = "";
    //            while (dr.Read())
    //            {

    //                d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
    //                d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
    //                txt_FromDate.Text = d1.ToString("dd/MM/yyy");
    //                txt_ToDate.Text =  d2.ToString("dd/MM/yyy");
                 
    //            }
    //        }
    //    }
    //    catch
    //    {

    //    }
    //}


    public void billdate()
    {
        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            con = dbaccess.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str;
            if (roleid == 4)
            {
                str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'  and Adminupdate=1  order by  TID DESC";
            }
            else
            {

                str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantcode.SelectedItem.Value + "'  order by  TID DESC";

            }
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d1.ToString("dd/MM/yyy");
                    Todate = d2.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);
                }
            }
        }
        catch
        {

        }
    }

    public void getdatefuntion()
    {
        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];
        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;


    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            LoadBill();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            DateTime frmdate = DateTime.ParseExact(FDATE, "dd/MM/yyyy", null);
            string fdate = frmdate.ToString("dd" + "_" + "MM" + "_" + "yyyy");
            DateTime todate = DateTime.ParseExact(TODATE, "dd/MM/yyyy", null);
            string tdate = todate.ToString("dd" + "_" + "MM" + "_" + "yyyy");

            string CurrentCreateFolderName = fdate + "_" + tdate + "_" + DateTime.Now.ToString("ddMMyyyy");
            string path = @"C:\BILL VYSHNAVI\" + ccode + "_" + "_" + ddl_Plantcode.SelectedItem.Value + CurrentCreateFolderName + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            CrDiskFileDestinationOptions.DiskFileName = path + ccode + "_" + ddl_Plantcode.SelectedItem.Value + "_" + "Invoice.pdf";
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
            string MFileName = @"C:/BILL VYSHNAVI/" + ccode + "_" + "_" + ddl_Plantcode.SelectedItem.Value + CurrentCreateFolderName + "/" + ccode + "_" + ddl_Plantcode.SelectedItem.Value + "_" + "Invoice.pdf";
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
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        if (cr != null)
        {

            cr.Close();

            cr.Dispose();

            GC.Collect();

        }
    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}