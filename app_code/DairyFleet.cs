using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Net.Mime;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;

    //using System.Security.Authentication;
    /// <summary>
    /// Summary description for DairyFleet
    /// </summary>
public class DairyFleet : IHttpHandler, IRequiresSessionState
{
    public DairyFleet()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool IsReusable
    {
        get { return true; }
    }
    class Orders
    {
        public string operation { set; get; }
        public string BranchID { set; get; }
        public string imagecode { set; get; }
    }
    SqlCommand cmd;
    public void ProcessRequest(HttpContext context)
    {
        //throw new NotImplementedException();
        try
        {
            string request = context.Request["operation"];
            switch (request)
            {
                case "save_branchdata_click":
                    save_branchdata_click(context);
                    break;
                case "get_Branch_details":
                    get_Branch_details(context);
                    break;
                case "GetHeadNames":
                    GetHeadNames(context);
                    break;
                case "GetEmployeeNames":
                    GetEmployeeNames(context);
                    break;
                case "GetApproveEmployeeNames":
                    GetApproveEmployeeNames(context);
                    break;
                case "GetHeadLimit":
                    GetHeadLimit(context);
                    break;
                case "BtnGetVoucherClick":
                    BtnGetVoucherClick(context);
                    break;
                case "BtnPayVoucherClick":
                    BtnPayVoucherClick(context);
                    break;
                case "btnViewVoucherGeneretaeClick":
                    btnViewVoucherGeneretaeClick(context);
                    break;
                case "GetSubPaybleValues":
                    GetSubPaybleValues(context);
                    break;
                case "btnVoucherPrintClick":
                    btnVoucherPrintClick(context);
                    break;
                case "btnVoucherCancelClick":
                    btnVoucherCancelClick(context);
                    break;
                case "BtnClearRaiseVoucherClick":
                    BtnClearRaiseVoucherClick(context);
                    break;
                case "BtnVarifyVoucherSaveClick":
                    BtnVarifyVoucherSaveClick(context);
                    break;
                case "GetRaisedVouchers":
                    GetRaisedVouchers(context);
                    break;
                case "GetBtnViewVoucherclick":
                    GetBtnViewVoucherclick(context);
                    break;
                case "GetAppriveSubPaybleValues":
                    GetAppriveSubPaybleValues(context);
                    break;
                case "btnApproveVoucherclick":
                    btnApproveVoucherclick(context);
                    break;
                case "btnRejectVoucherclick":
                    btnRejectVoucherclick(context);
                    break;
                case "btnVoucherUpdateClick":
                    btnVoucherUpdateClick(context);
                    break;
                case "BtnCashAmountClick":
                    BtnCashAmountClick(context);
                    break;
                case "GetHeadOfAccpunts":
                    GetHeadOfAccpunts(context);
                    break;
                case "SaveHeadMasterClick":
                    SaveHeadMasterClick(context);
                    break;
                case "BtnGetCashBookClosing":
                    BtnGetCashBookClosing(context);
                    break;
                case "btnupdatevoucherDetails":
                    btnupdatevoucherDetails(context);
                    break;
                case "btnupdatereceiptDetails":
                    btnupdatereceiptDetails(context);
                    break;
                case "saveBankDetails":
                    saveBankDetails(context);
                    break;
                case "get_bank_details":
                    get_bank_details(context);
                    break;

                case "save_payment_entry_click":
                    save_payment_entry_click(context);
                    break;
                case "GetPaymentDetails":
                    GetPaymentDetails(context);
                    break;
                case "save_receipt_entry_click":
                    save_receipt_entry_click(context);
                    break;
                case "GetReceiptDetails":
                    GetReceiptDetails(context);
                    break;
                case "get_plantwise_milkdetails":
                    get_plantwise_milkdetails(context);
                    break;

                case "get_branchsummarywise_milkdetails":
                    get_branchsummarywise_milkdetails(context);
                    break;
                case "get_plant_details":
                    get_plant_details(context);
                    break; 
                    

                    //akbar

                case "get_Plant_Route_details":
                    get_Plant_Route_details(context);
                    break;
                case "Save_Route_Time_Maintenance":
                    Save_Route_Time_Maintenance(context);
                    break;
                case "Save_Agent_samples_procurement":
                    Save_Agent_samples_procurement(context);
                    break;
                case "get_RouteTimeMaintenance_details":
                    get_RouteTimeMaintenance_details(context);
                    break;
                case "get_Agent_details":
                    get_Agent_details(context);
                    break;
                case "get_Agent_Information_details":
                    get_Agent_Information_details(context);
                    break;

                case "saveGenReadingDetails":
                    saveGenReadingDetails(context);
                    break;
                case "get_GenReadingDetails":
                    get_GenReadingDetails(context);
                    break;
                case "saveDeiselDetails":
                    saveDeiselDetails(context);
                    break;
                case "get_DeiselDetails":
                    get_DeiselDetails(context);
                    break;
                case "savePowerDetails":
                    savePowerDetails(context);
                    break;
                case "get_PowerDetails":
                    get_PowerDetails(context);
                    break;

                case "saveChemicalDetails":
                    saveChemicalDetails(context);
                    break;
                case "get_ChemicalDetails":
                    get_ChemicalDetails(context);
                    break;
                case "save_comruntimehours_click":
                    save_comruntimehours_click(context);
                    break;

                case "get_comruntimehours_details":
                    get_comruntimehours_details(context);
                    break;

                case "saveChillingMachinaryDetails":
                    saveChillingMachinaryDetails(context);
                    break;

                case "get_ChillingMachinaryDetails":
                    get_ChillingMachinaryDetails(context);
                    break;
                case "saveCompressiormachinaryDetails":
                    saveCompressiormachinaryDetails(context);
                    break;
                case "get_CompressiorMachinaryDetails":
                    get_CompressiorMachinaryDetails(context);
                    break;

                case "saveSiloDetails":
                    saveSiloDetails(context);
                    break;
                case "get_SiloDetails":
                    get_SiloDetails(context);
                    break;
                case "get_Sms_Data_RouteWise":
                    get_Sms_Data_RouteWise(context);
                    break;
                case "savemilkrechilling":
                    savemilkrechilling(context);
                    break;
                case "get_milkrechilling_details":
                    get_milkrechilling_details(context);
                    break;
                case "Save_Inventary_Transaction":
                    Save_Inventary_Transaction(context);
                    break;
                case "saveInvetary":
                    saveInvetary(context);
                    break;
                case "get_InvetaryMaster_details":
                    get_InvetaryMaster_details(context);
                    break;
                case "saveledgermaster":
                    saveledgermaster(context);
                    break;
                case "get_ledger_details":
                    get_ledger_details(context);
                    break;
                case "get_ledger_details1":
                    get_ledger_details1(context);
                    break;
                case "get_Inventary_Transaction_details":
                    get_Inventary_Transaction_details(context);
                    break;


                    //akbar
                    //naveen
                case "get_procinfo_details":
                    get_procinfo_details(context);
                    break;
                case "get_procinforoute_details":
                    get_procinforoute_details(context);
                    break;
                case "viewroutewiseagent_details":
                    viewroutewiseagent_details(context);
                    break;
                case "get_agentmilkdashboard_details":
                    get_agentmilkdashboard_details(context);
                    break;
                case "get_plantwiseclosing_details":
                    get_plantwiseclosing_details(context);
                    break;

                case "Send_Sms_Data_RouteWise":
                    Send_Sms_Data_RouteWise(context);
                    break;

                case "get_silobalancerpt_details":
                    get_silobalancerpt_details(context);
                    break;

                default:
                    var js = new JavaScriptSerializer();
                    var title1 = context.Request.Params[1];
                    Orders obj = js.Deserialize<Orders>(title1);
                    if (obj.operation == "BtnRaiseVoucherClick")
                    {
                        BtnRaiseVoucherClick(context);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            //context.Response.ContentType = MediaTypeNames.Text.Plain;
            //context.Response.StatusCode = 400;
            //context.Response.Write(ex.Message);
        }
    }
    public class BankMaster
    {
        public string name { get; set; }
        public string Code { get; set; }
        public string sno { get; set; }
        public string status { get; set; }


        public string code { get; set; }
    }

    public class Plant_Route_Details
    {
        public string PlantName { get; set; }
        public string RouteName { get; set; }
        public string Plant_Code { get; set; }
        public string Route_id { get; set; }
    }
    private void get_Plant_Route_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();

            List<Plant_Route_Details> plantroutelst = new List<Plant_Route_Details>();

            string leveltype = context.Session["LevelType"].ToString();

            if (leveltype == "2" || leveltype == "1")
            {
                string branchid = context.Session["branch"].ToString();
                cmd = new SqlCommand("SELECT Plant_Master.Plant_Name, Plant_Master.Plant_Code, Route_Master.Route_ID, Route_Master.Route_Name FROM Plant_Master INNER JOIN Route_Master ON Plant_Master.Plant_Code = Route_Master.Plant_Code where  Plant_Master.Plant_Code=@Plant_Code ");
                cmd.Parameters.Add("@Plant_Code", branchid);
            }
            else
            {
                cmd = new SqlCommand("SELECT Plant_Master.Plant_Name, Plant_Master.Plant_Code, Route_Master.Route_ID, Route_Master.Route_Name FROM Plant_Master INNER JOIN Route_Master ON Plant_Master.Plant_Code = Route_Master.Plant_Code");
            }
            DataTable dtplantrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtplantrote.Rows)
            {
                Plant_Route_Details getplantroutedeatils = new Plant_Route_Details();
                getplantroutedeatils.PlantName = dr["Plant_Name"].ToString();
                getplantroutedeatils.RouteName = dr["Route_Name"].ToString();
                getplantroutedeatils.Plant_Code = dr["Plant_Code"].ToString();
                getplantroutedeatils.Route_id = dr["Route_ID"].ToString();
                plantroutelst.Add(getplantroutedeatils);
            }
            string response = GetJson(plantroutelst);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void get_Agent_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plantcode = context.Request["plantcode"];
            List<Plant_Agent_Details> agentdetailslistlst = new List<Plant_Agent_Details>();
            cmd = new SqlCommand("SELECT  Agent_Id, Agent_Name, Cartage_Amt AS SplBonus_Amt, AgentRateChartmode, Route_id FROM Agent_Master WHERE (Plant_code = @plantcode)");
            cmd.Parameters.Add("@plantcode", plantcode);
            DataTable dtpagentinfo = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtpagentinfo.Rows)
            {
                Plant_Agent_Details getagentinformationedeatils = new Plant_Agent_Details();
                getagentinformationedeatils.Agent_Name = dr["Agent_Name"].ToString();
                getagentinformationedeatils.Agent_Id = dr["Agent_Id"].ToString();
                getagentinformationedeatils.routeid = dr["Route_id"].ToString();
                //getagentinformationedeatils.Address = dr["Address"].ToString();
                //getagentinformationedeatils.JoiningDate = dr["JoiningDate"].ToString();
                //getagentinformationedeatils.MobileNo = dr["Mobile"].ToString();
                agentdetailslistlst.Add(getagentinformationedeatils);
            }
            string response = GetJson(agentdetailslistlst);
            context.Response.Write(response);
        }
        catch
        {
        }
    }

    private void Save_Agent_samples_procurement(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Route_id = context.Request["Route_id"];

            //'plant_code': plant_code, 'Route_id': Route_id, 'date': date, 'session': session, 'agentid': agentid, 'fat': fat, 'snf': snf, 'clr': clr, 'milkqty': milkqty, 'NOOFCANS': NOOFCANS, 'SAMPLENO': SAMPLENO, 'starttime': starttime, 'intime': intime, 'milknature': milknature, 'btnval': btnval, 'sno': sno


            string agentid = context.Request["agentid"];
            string fat = context.Request["fat"];
            string snf = context.Request["snf"];
            string clr = context.Request["clr"];
            string milkqty = context.Request["milkqty"];
            string NOOFCANS = context.Request["NOOFCANS"];
            string SAMPLENO = context.Request["SAMPLENO"];
            string milknature = context.Request["milknature"];
            string date = context.Request["date"];

            string session = context.Request["session"];
            string starttime = context.Request["starttime"];
            string intime = context.Request["intime"];
            DateTime doe = DateTime.Now;
            string branchid = context.Session["branch"].ToString();
            string entry_by = context.Session["UserSno"].ToString();
            string btnval = context.Request["btnval"];
            if (btnval == "Save")
            {

               
                cmd = new SqlCommand("SELECT { fn IFNULL(MAX(Tid), 0) } + 1 AS Tid FROM  Procurementimport WHERE Plant_Code=@plant_code");
                cmd.Parameters.Add("@plant_code", plant_code);
                DataTable dtpagentinfo = vdm.SelectQuery(cmd).Tables[0];
                string TID = dtpagentinfo.Rows[0]["Tid"].ToString();
                cmd = new SqlCommand("insert into Procurementimport (Tid, Agent_id, Prdate, Sessions, Milk_ltr, Fat, Snf, Plant_Code, Route_id, NoofCans, Milk_kg, Clr, Company_Code, Milk_Nature, SampleId, Sampleno,  usr_weigher, usr_analizer, fat_kg, snf_kg, Truck_id, Status, Remark, Remarkstatus, modify_aid, modify_Kg, modify_fat, modify_snf, DIFFKG, DIFFFAT, DIFFSNF) values (@TID, @Agent_id,@Date,@Session,@Milk_ltr,@fat,@snf,@Plant_code,@Route_id, @NoofCans, @Milk_ltr, @clr, '1', @Milk_Nature, @Sampleno, @Sampleno, 'WEIGHER', 'ADMINISTRATOR', @fat, @snf, @Plant_code, @Plant_code, '', '0', '0', '0','0','0','0','0','0')");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@Route_id", Route_id);
                cmd.Parameters.Add("@Agent_id", agentid);
                cmd.Parameters.Add("@Milk_ltr", milkqty);
                cmd.Parameters.Add("@fat", fat);
                cmd.Parameters.Add("@snf", snf);
                cmd.Parameters.Add("@NoofCans", NOOFCANS);
                cmd.Parameters.Add("@clr", clr);
                cmd.Parameters.Add("@Milk_Nature", milknature);
                cmd.Parameters.Add("@Sampleno", SAMPLENO);
                cmd.Parameters.Add("@Date", date);
                cmd.Parameters.Add("@Session", session);
               
                cmd.Parameters.Add("@TID", Convert.ToInt32(TID));
                vdm.insert(cmd);
                string msg = "Sample details successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                //string sno = context.Request["sno"];
                //cmd = new SqlCommand(" update RouteTimeMaintain set Plant_code=@Plant_code,Route_id=@Route_id,Date=@Date,Session=@Session,VehicleSettime=@VehicleSettime,VehicleInttime=@VehicleInttime,MBRT=@MBRT where Tid=@sno and Plant_code=@plant_code");
                //cmd.Parameters.Add("@Plant_code", plant_code);
                //cmd.Parameters.Add("@Route_id", Route_id);
                //cmd.Parameters.Add("@Date", date);
                //cmd.Parameters.Add("@Session", session);
                //cmd.Parameters.Add("@VehicleSettime", starttime);
                //cmd.Parameters.Add("@VehicleInttime", intime);
                //cmd.Parameters.Add("@MBRT", mbrt);
                //cmd.Parameters.Add("@sno", sno);
                //vdm.Update(cmd);
                //string msg = "RouteTimeMaintain details successfully updated";
                //string response = GetJson(msg);
                //context.Response.Write(response);
            }
        }
        catch
        {
        }
    }


    public class Plant_Agent_Details
    {
        public string Agent_Name { get; set; }
        public string RouteName { get; set; }
        public string Plant_Code { get; set; }
        public string Agent_Id { get; set; }
        public string Address { get; set; }
        public string JoiningDate { get; set; }
        public string MobileNo { get; set; }
        public string routeid { get; set; }
    }
    public class agetCartage_Amt
    {
        public string Month { get; set; }
        public string YEAR { get; set; }
        public string SplBonus_Amt { get; set; }
        public string Cartage_Amt { get; set; }

    }

    public class Agetloandetails
    {
        public string LoanAmount { get; set; }
        public string Recipt { get; set; }
        public string Balance { get; set; }
        public string Cartage_Amt { get; set; }

    }
    public class AgetMilkdetails
    {
        public string YEAR { get; set; }
        public string Month { get; set; }
        public string OrderCount { get; set; }
        public string FatAvg { get; set; }
        public string SnfAvg { get; set; }
        public string MilkKgs { get; set; }

    }

    public class Totalagentsummary
    {
        public List<Plant_Agent_Details> Plant_Agent_Details { get; set; }
        public List<agetCartage_Amt> agetCartage_Amt { get; set; }
        public List<Agetloandetails> Agetloandetails { get; set; }
        public List<AgetMilkdetails> AgetMilkdetails { get; set; }
    }



    private void get_Agent_Information_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plantcode = context.Request["plantcode"];
            string agentid = context.Request["agentid"];
            cmd = new SqlCommand("SELECT  Agent_Master.Plant_code,YEAR(Procurement.Prdate) AS YEAR, UPPER(LEFT(DATENAME(MONTH, Procurement.Prdate), 3)) AS MMM, Agent_Master.Cartage_Amt, Agent_Master.SplBonus_Amt FROM  Agent_Master INNER JOIN  Procurement ON Agent_Master.Plant_code = Procurement.Plant_Code WHERE  (Agent_Master.Plant_code = @plantcode) AND (Agent_Master.Agent_Id = @agentid) GROUP BY YEAR(Procurement.Prdate), MONTH(Procurement.Prdate),Agent_Master.Plant_code, DATENAME(MONTH, Procurement.Prdate), Agent_Master.Cartage_Amt, Agent_Master.SplBonus_Amt ORDER BY YEAR");
            cmd.Parameters.Add("@plantcode", plantcode);
            cmd.Parameters.Add("@agentid", agentid);
            DataTable dtagentcartagedetails = vdm.SelectQuery(cmd).Tables[0];
            cmd = new SqlCommand("SELECT Plant_Master.Plant_Code,Agent_Master.Agent_Name, AgentInformation.Route_Name, AgentInformation.Address, AgentInformation.JoiningDate, AgentInformation.Mobile,Agent_Master.Agent_Id, Agent_Master.DpuAgentStatus FROM Plant_Master INNER JOIN  Agent_Master ON Plant_Master.Plant_Code = Agent_Master.Plant_code INNER JOIN   AgentInformation ON Agent_Master.Agent_Id = AgentInformation.Agent_Id WHERE (Plant_Master.Plant_Code = @plantcode) AND (Agent_Master.Plant_code = @plantcode) AND (AgentInformation.Plant_code = @plantcode) and (AgentInformation.Agent_Id=@agentid)");
            cmd.Parameters.Add("@plantcode", plantcode);
            cmd.Parameters.Add("@agentid", agentid);
            DataTable dtagenttinfo = vdm.SelectQuery(cmd).Tables[0];
            cmd = new SqlCommand("SELECT  SUM(LoanDetails.loanamount) AS LoanAmount, SUM(Loan_Recovery.Paid_Amount) AS Recipt, SUM(LoanDetails.balance) AS Balance, LoanDetails.plant_code,  LoanDetails.agent_Id FROM LoanDetails LEFT OUTER JOIN Loan_Recovery ON LoanDetails.plant_code = Loan_Recovery.Plant_code WHERE (LoanDetails.plant_code = @plantcode) AND (LoanDetails.agent_Id = @agentid) GROUP BY LoanDetails.plant_code, LoanDetails.agent_Id");
            cmd.Parameters.Add("@plantcode", plantcode);
            cmd.Parameters.Add("@agentid", agentid);
            DataTable dtagentloandetails = vdm.SelectQuery(cmd).Tables[0];
            cmd = new SqlCommand("SELECT Plant_Code,Agent_id,YEAR(Prdate) AS YEAR, UPPER(LEFT(DATENAME(MONTH, Prdate), 3)) AS MMM, SUM(Milk_kg) AS MilkKgs, COUNT(*) AS OrderCount, AVG(Fat) AS FatAvg, AVG(Snf)  AS SnfAvg FROM  Procurement  WHERE (Plant_Code = @plantcode) AND (Agent_id = @agentid) GROUP BY YEAR(Prdate), MONTH(Prdate), DATENAME(MONTH, Prdate),Plant_Code,Agent_id ORDER BY YEAR");
            cmd.Parameters.Add("@plantcode", plantcode);
            cmd.Parameters.Add("@agentid", agentid);
            DataTable dtagenttimilkdetails = vdm.SelectQuery(cmd).Tables[0];
            List<Plant_Agent_Details> Plant_Agent_Detailslist = new List<Plant_Agent_Details>();
            List<agetCartage_Amt> agetCartage_Amtlist = new List<agetCartage_Amt>();
            List<Agetloandetails> Agetloandetailslist = new List<Agetloandetails>();
            List<AgetMilkdetails> AgetMilkdetailslist = new List<AgetMilkdetails>();
            List<Totalagentsummary> Totalagentsummarylist = new List<Totalagentsummary>();
            foreach (DataRow dragenttinfo in dtagenttinfo.Rows)
            {
                Plant_Agent_Details obj1 = new Plant_Agent_Details();

                obj1.Agent_Name = dragenttinfo["Agent_Name"].ToString();
                obj1.RouteName = dragenttinfo["Route_Name"].ToString();
                obj1.Address = dragenttinfo["Address"].ToString();
                obj1.JoiningDate = dragenttinfo["JoiningDate"].ToString();
                obj1.MobileNo = dragenttinfo["Mobile"].ToString();
                obj1.Agent_Id = dragenttinfo["Agent_Id"].ToString();
                Plant_Agent_Detailslist.Add(obj1);
            }
            foreach (DataRow dr in dtagentcartagedetails.Rows)
            {
                agetCartage_Amt obj2 = new agetCartage_Amt();
                obj2.YEAR = dr["YEAR"].ToString();
                obj2.Month = dr["MMM"].ToString();
                obj2.SplBonus_Amt = dr["SplBonus_Amt"].ToString();
                obj2.Cartage_Amt = dr["Cartage_Amt"].ToString();
                agetCartage_Amtlist.Add(obj2);
            }
            foreach (DataRow dragentloandetails in dtagentloandetails.Rows)
            {

                Agetloandetails obj3 = new Agetloandetails();
                obj3.LoanAmount = dragentloandetails["LoanAmount"].ToString();
                obj3.Recipt = dragentloandetails["Recipt"].ToString();
                obj3.Balance = dragentloandetails["Balance"].ToString();
                Agetloandetailslist.Add(obj3);
            }
            foreach (DataRow dragenttimilkdetails in dtagenttimilkdetails.Rows)
            {



                AgetMilkdetails obj4 = new AgetMilkdetails();
                obj4.YEAR = dragenttimilkdetails["YEAR"].ToString();
                obj4.Month = dragenttimilkdetails["MMM"].ToString();
                obj4.OrderCount = dragenttimilkdetails["OrderCount"].ToString();

                string fat = dragenttimilkdetails["FatAvg"].ToString();
                double fatavg = 0;
                fatavg = Convert.ToDouble(dragenttimilkdetails["FatAvg"].ToString());
                fatavg = Math.Round(fatavg, 2);
                obj4.FatAvg = fatavg.ToString();
                string snf = dragenttimilkdetails["SnfAvg"].ToString();
                double snfavg = 0;
                snfavg = Convert.ToDouble(dragenttimilkdetails["SnfAvg"].ToString());
                snfavg = Math.Round(snfavg, 2);
                obj4.SnfAvg = snfavg.ToString();
                //obj4.FatAvg = dragenttimilkdetails["FatAvg"].ToString();
                //obj4.SnfAvg = dragenttimilkdetails["SnfAvg"].ToString();
                obj4.MilkKgs = dragenttimilkdetails["MilkKgs"].ToString();
                AgetMilkdetailslist.Add(obj4);
            }
            Totalagentsummary Totalagentsummaryobj = new Totalagentsummary();
            Totalagentsummaryobj.Plant_Agent_Details = Plant_Agent_Detailslist;
            Totalagentsummaryobj.agetCartage_Amt = agetCartage_Amtlist;
            Totalagentsummaryobj.Agetloandetails = Agetloandetailslist;
            Totalagentsummaryobj.AgetMilkdetails = AgetMilkdetailslist;
            Totalagentsummarylist.Add(Totalagentsummaryobj);
            string response = GetJson(Totalagentsummarylist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }

    private void get_bank_details(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            cmd = new SqlCommand("SELECT sno,bankname, code, status FROM bankmaster");
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<BankMaster> bankMasterlist = new List<BankMaster>();
            foreach (DataRow dr in routes.Rows)
            {
                BankMaster getbankdetails = new BankMaster();
                getbankdetails.name = dr["bankname"].ToString();
                getbankdetails.code = dr["code"].ToString();
                getbankdetails.status = dr["status"].ToString();
                getbankdetails.sno = dr["sno"].ToString();
                bankMasterlist.Add(getbankdetails);
            }
            string response = GetJson(bankMasterlist);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);

        }
    }
    private void saveBankDetails(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string name = context.Request["Name"];
            string code = context.Request["Code"];
            string status = context.Request["Status"];
            //string createdby = "admin";
            DateTime createdon = DateTime.Now;
            string btnSave = context.Request["btnVal"];
            if (btnSave == "save")
            {
                cmd = new SqlCommand("insert into bankmaster (bankname,code,status) values (@bankname,@code,@status)");
                cmd.Parameters.Add("@bankname", name);
                cmd.Parameters.Add("@code", code);
                cmd.Parameters.Add("@status", status);
                vdm.insert(cmd);
                string msg = "Bank detailes successfully saved";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand("Update bankmaster set  code=@code,status=@status,bankname=@bankname where sno=@sno");
                cmd.Parameters.Add("@bankname", code);
                cmd.Parameters.Add("@code", code);
                cmd.Parameters.Add("@status", status);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "Sub bank successfully updated";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }

    private void btnupdatereceiptDetails(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string refno = context.Request["refno"];
            string doe = context.Request["doe"];
            DateTime dtdate = Convert.ToDateTime(doe);
            string BranchID = context.Session["branch"].ToString();
            cmd = new SqlCommand("update cashcollections set DOE=@DOE where sno=@sno and BranchID=@BranchID");
            cmd.Parameters.Add("@DOE", dtdate);
            cmd.Parameters.Add("@sno", refno);
            cmd.Parameters.Add("@BranchID", BranchID);
            vdm.Update(cmd);
            string msg = "Receipt successfully updated";
            string respnceString = GetJson(msg);
            context.Response.Write(respnceString);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string respnceString = GetJson(msg);
            context.Response.Write(respnceString);
        }
    }
    private void btnupdatevoucherDetails(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string refno = context.Request["refno"];
            string doe = context.Request["doe"];
            DateTime dtdate = Convert.ToDateTime(doe);
            string BranchID = context.Session["branch"].ToString();
            cmd = new SqlCommand("update cashpayables set doe=@doe where sno=@sno and BranchID=@BranchID");
            cmd.Parameters.Add("@doe", dtdate);
            cmd.Parameters.Add("@sno", refno);
            cmd.Parameters.Add("@BranchID", BranchID);
            vdm.Update(cmd);
            string msg = "Voucher successfully updated";
            string respnceString = GetJson(msg);
            context.Response.Write(respnceString);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string respnceString = GetJson(msg);
            context.Response.Write(respnceString);
        }
    }
    private void BtnGetCashBookClosing(HttpContext context)
    {
        context.Session["DenominationString"] = context.Request["DenominationString"];
        //context.Session["Cash"] = context.Request["Cash"];

        string msg = "success";
        string response = GetJson(msg);
        context.Response.Write(response);
    }
    private void SaveHeadMasterClick(HttpContext context)
    {

        try
        {
            vdm = new SalesDBManager();
            string ledger_code = context.Request["ledger_code"];

            string HeadAccount = context.Request["Decription"];
            string serial = context.Request["serial"];
            string plantcode = context.Request["plantcode"];
            string btnSave = context.Request["btnSave"];
            string Limit = context.Request["Limit"];
            double LimitAmount = 0;
            double.TryParse(Limit, out LimitAmount);
            if (btnSave == "Modify")
            {
                cmd = new SqlCommand("Update accountheads set BranchID=@BranchID,ledger_code=@ledger_code,HeadName=@HeadName,LimitAmount=@LimitAmount where BranchID=@BranchID and Sno=@Sno");
                cmd.Parameters.Add("@ledger_code", ledger_code);
                cmd.Parameters.Add("@HeadName", HeadAccount);
                cmd.Parameters.Add("@LimitAmount", LimitAmount);
                cmd.Parameters.Add("@Sno", serial);
                cmd.Parameters.Add("@BranchID", plantcode);
                vdm.Update(cmd);
                string Msg = "Account Name Updated Successfully";
                string response = GetJson(Msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("Insert Into accountheads (HeadName,BranchID,LimitAmount,ledger_code) values(@HeadName,@BranchID,@LimitAmount,@ledger_code)");
                cmd.Parameters.Add("@HeadName", HeadAccount);
                cmd.Parameters.Add("@LimitAmount", LimitAmount);
                cmd.Parameters.Add("@BranchID", plantcode);
                cmd.Parameters.Add("@ledger_code", ledger_code);
                vdm.insert(cmd);
                string Msg = "Account Name saved Successfully";
                string response = GetJson(Msg);
                context.Response.Write(response);
            }
        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
            string response = GetJson(Msg);
            context.Response.Write(response);
        }
    }
    private void GetHeadOfAccpunts(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string plantcode = context.Request["plantcode"].ToString();

            string branchid = context.Session["branch"].ToString();
            string LevelType = context.Session["LevelType"].ToString();
            if (LevelType == "AccountsOfficer" || LevelType == "Director")
            {
                if (branchid == "")
                {
                    cmd = new SqlCommand("SELECT Sno,ledger_code, HeadName, LimitAmount, AccountType, AgentID, EmpID FROM accountheads WHERE (BranchId IS NULL) and BranchId=@BranchID ORDER BY HeadName");
                    cmd.Parameters.Add("@BranchID", plantcode);
                }
                else
                {
                    cmd = new SqlCommand("Select Sno,HeadName,ledger_code,LimitAmount,AccountType,AgentID,EmpID from accountheads Where BranchID=@BranchID order by HeadName");
                    cmd.Parameters.Add("@BranchID", plantcode);
                }
            }
            else
            {
                cmd = new SqlCommand("Select Sno,HeadName,ledger_code,LimitAmount,AccountType,AgentID,EmpID from accountheads Where BranchID=@BranchID order by HeadName");
                cmd.Parameters.Add("@BranchID", plantcode);
            }
            DataTable dtVehicle = vdm.SelectQuery(cmd).Tables[0];
            List<HeadClass> HeadClasslist = new List<HeadClass>();
            foreach (DataRow dr in dtVehicle.Rows)
            {
                HeadClass getVehicles = new HeadClass();
                getVehicles.Sno = dr["Sno"].ToString();
                getVehicles.plantcode = plantcode;
                getVehicles.HeadName = dr["HeadName"].ToString();
                getVehicles.Limit = dr["LimitAmount"].ToString();
                getVehicles.ledger_code = dr["ledger_code"].ToString();
                string AccountType = dr["AccountType"].ToString();
                getVehicles.AccountType = AccountType;
                getVehicles.Code = "0";
                HeadClasslist.Add(getVehicles);
            }
            string response = GetJson(HeadClasslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
   
    private void btnVoucherUpdateClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            string Remarks = context.Request["Remarks"];
            string BranchID = "0";
            string LevelType = context.Session["LevelType"].ToString();
            if (LevelType == "AccountsOfficer" || LevelType == "Director")
            {
                BranchID = context.Request["BranchID"];
            }
            else
            {
                BranchID = context.Session["branch"].ToString();
            }
            cmd = new SqlCommand("Update cashpayables set Ramarks=@Ramarks where BranchID=@BranchID and Sno=@VocherID");
            cmd.Parameters.Add("@VocherID", VoucherID);
            cmd.Parameters.Add("@Remarks", Remarks);
            cmd.Parameters.Add("@BranchID", BranchID);
            vdm.Update(cmd);
            string msg = "Voucher Updated successfully";
            string response = GetJson(msg);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void btnRejectVoucherclick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            string Approvalamt = context.Request["Approvalamt"];
            string Remarks = context.Request["Remarks"];
            string Status = context.Request["Status"];
            string AppRemarks = context.Request["AppRemarks"];
            string BranchID = "0";
            string LevelType = context.Session["LevelType"].ToString();
            if (LevelType == "AccountsOfficer" || LevelType == "Director")
            {
                BranchID = context.Request["BranchID"];
            }
            else
            {
                BranchID = context.Session["branch"].ToString();
            }
            cmd = new SqlCommand("Update cashpayables set  Remarks=@Remarks,ApprovedAmount=@ApprovedAmount ,ApprovalRemarks=@ApprovalRemarks,Status=@Status where  Sno=@VocherID and BranchID=@BranchID ");
            cmd.Parameters.Add("@Status", Status);
            cmd.Parameters.Add("@ApprovedAmount", Approvalamt);
            cmd.Parameters.Add("@ApprovalRemarks", AppRemarks);
            cmd.Parameters.Add("@VocherID", VoucherID);
            cmd.Parameters.Add("@BranchID", BranchID);
            cmd.Parameters.Add("@Remarks", Remarks);
            vdm.Update(cmd);
            string msg = "Voucher Reject successfully";
            string response = GetJson(msg);
            context.Response.Write(response);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
    }

    private void btnApproveVoucherclick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            string Approvalamt = context.Request["Approvalamt"];
            double Amount = 0;
            double.TryParse(Approvalamt, out Amount);
            string Remarks = context.Request["Remarks"];
            string AppRemarks = context.Request["AppRemarks"];
            string Status = context.Request["Status"];
            string BranchID = "0";
            string LevelType = context.Session["LevelType"].ToString();
            if (LevelType == "AccountsOfficer" || LevelType == "Director")
            {
                BranchID = context.Request["BranchID"];
            }
            else
            {
                BranchID = context.Session["branch"].ToString();
            }
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Branchid, UserData_sno, AmountPaid, Denominations, Remarks, Sno, PaidDate FROM collections WHERE (Branchid = @BranchID) AND (PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", BranchID);
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            DataTable dtcashbookstatus = vdm.SelectQuery(cmd).Tables[0];
            if (dtcashbookstatus.Rows.Count > 0)
            {
                string msg = "Cash Book Has Been Closed For This Day";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("Update cashpayables set Remarks=@Remarks,DOE=@DOE, ApprovedAmount=@ApprovedAmount ,ApprovalRemarks=@ApprovalRemarks,Status=@Status where Sno=@VocherID and BranchID=@BranchID");
                cmd.Parameters.Add("@Status", Status);
                cmd.Parameters.Add("@ApprovedAmount", Amount);
                cmd.Parameters.Add("@ApprovalRemarks", AppRemarks);
                cmd.Parameters.Add("@Remarks", Remarks);
                cmd.Parameters.Add("@VocherID", VoucherID);
                cmd.Parameters.Add("@BranchID", BranchID);
                cmd.Parameters.Add("@DOE", ServerDateCurrentdate);
                vdm.Update(cmd);
                string msg = "Voucher Approved successfully";
                string response = GetJson(msg);
                context.Response.Write(response);
            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
    }
    private void GetAppriveSubPaybleValues(HttpContext context)
    {

        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            List<Subpayables> SubpayableList = new List<Subpayables>();
            string BranchID = "0";
            string LevelType = context.Session["LevelType"].ToString();
            if (LevelType == "AccountsOfficer" || LevelType == "Director")
            {
                BranchID = context.Request["BranchID"];
            }
            else
            {
                BranchID = context.Session["branch"].ToString();
            }
            cmd = new SqlCommand("SELECT Sno FROM cashpayables WHERE (Sno = @VocherID) AND (BranchID = @BranchID)");
            cmd.Parameters.Add("@VocherID", VoucherID);
            cmd.Parameters.Add("@BranchID", BranchID);
            DataTable dtCash = vdm.SelectQuery(cmd).Tables[0];
            string RefNo = dtCash.Rows[0]["Sno"].ToString();
            cmd = new SqlCommand("SELECT accountheads.HeadName, subpayable.Amount, accountheads.Sno FROM subpayable INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE  (subpayable.RefNo = @RefNo)");
            cmd.Parameters.Add("@RefNo", RefNo);
            DataTable dtCashSubPayable = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtCashSubPayable.Rows)
            {
                Subpayables GetSubpayable = new Subpayables();
                GetSubpayable.HeadSno = dr["Sno"].ToString();
                GetSubpayable.HeadOfAccount = dr["HeadName"].ToString();
                GetSubpayable.Amount = dr["Amount"].ToString();
                SubpayableList.Add(GetSubpayable);
            }
            string response = GetJson(SubpayableList);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void GetBtnViewVoucherclick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            string BranchID = "0";
            string LevelType = context.Session["LevelType"].ToString();

            BranchID = context.Session["branch"].ToString();
            List<VoucherClass> Voucherlist = new List<VoucherClass>();
            //cmd = new SqlCommand("SELECT empmanage.EmpName,cashpayables.EmpID,cashpayables.Approvedby, cashpayables.VoucherType, cashpayables.CashTo, cashpayables.onNameof, cashpayables.Amount, cashpayables.ApprovedAmount, empmanage_1.EmpName AS ApproveEmpName, cashpayables.Status, cashpayables.ApprovalRemarks, cashpayables.Remarks FROM empmanage INNER JOIN cashpayables ON empmanage.Sno = cashpayables.Empid INNER JOIN empmanage empmanage_1 ON cashpayables.Approvedby = empmanage_1.Sno WHERE (cashpayables.VocherID = @VoucherID) AND (cashpayables.BranchID = @BranchID)");
            cmd = new SqlCommand("SELECT  empmanage.EmpName,cashpayables.Empid, cashpayables.Approvedby, cashpayables.VoucherType, cashpayables.CashTo, cashpayables.onNameof, cashpayables.Amount, cashpayables.ApprovedAmount, empmanage_1.EmpName AS ApproveEmpName, cashpayables.Status, cashpayables.ApprovalRemarks, cashpayables.Remarks FROM empmanage empmanage_1 INNER JOIN cashpayables ON empmanage_1.Sno = cashpayables.Approvedby LEFT OUTER JOIN empmanage ON cashpayables.Empid = empmanage.Sno WHERE (cashpayables.Sno = @VoucherID) AND (cashpayables.BranchID = @BranchID)");
            cmd.Parameters.Add("@VoucherID", VoucherID);
            cmd.Parameters.Add("@BranchID", BranchID);
            DataTable dtVouchers = vdm.SelectQuery(cmd).Tables[0];
            if (dtVouchers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtVouchers.Rows)
                {
                    VoucherClass getVoucher = new VoucherClass();
                    getVoucher.EmpName = dr["EmpName"].ToString();
                    getVoucher.VoucherType = dr["VoucherType"].ToString();
                    getVoucher.CashTo = dr["CashTo"].ToString();
                    getVoucher.Description = dr["onNameof"].ToString();
                    getVoucher.Amount = dr["Amount"].ToString();
                    getVoucher.ApprovalAmount = dr["ApprovedAmount"].ToString();
                    getVoucher.ApproveEmpName = dr["ApproveEmpName"].ToString();
                    getVoucher.Status = dr["Status"].ToString();
                    getVoucher.ApprovalRemarks = dr["ApprovalRemarks"].ToString();
                    getVoucher.Remarks = dr["Remarks"].ToString();
                    getVoucher.Empid = dr["EmpID"].ToString();
                    getVoucher.ApprovedBy = dr["Approvedby"].ToString();
                    Voucherlist.Add(getVoucher);
                }
                string response = GetJson(Voucherlist);
                context.Response.Write(response);
            }
            else
            {
                string msg = "No voucher found";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }
    private void GetRaisedVouchers(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            List<VoucherClass> Voucherlist = new List<VoucherClass>();
            string LevelType = context.Session["LevelType"].ToString();
            string status = context.Request["Type"];
            string fromdate = context.Request["fromdate"];
            string ToDate = context.Request["ToDate"];
            DateTime dtFromdate = Convert.ToDateTime(fromdate);
            DateTime dtTodate = Convert.ToDateTime(ToDate);
            cmd = new SqlCommand("SELECT cashpayables.Sno,cashpayables.onNameof, cashpayables.Empid,cashpayables.VoucherType, empmanage.EmpName, cashpayables.VocherID, cashpayables.Amount, cashpayables.Remarks,cashpayables.onNameof FROM cashpayables LEFT OUTER JOIN empmanage ON cashpayables.Empid = empmanage.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.Status = @Status) and (cashpayables.Approvedby=@ApproveEmp)");
            cmd.Parameters.Add("@ApproveEmp", context.Session["UserSno"].ToString());
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            cmd.Parameters.Add("@Status", "R");
            DataTable dtVoucher = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtVoucher.Rows)
            {
                VoucherClass getVoucher = new VoucherClass();
                getVoucher.VoucherID = dr["sno"].ToString();
                getVoucher.EmpName = dr["onNameof"].ToString();
                getVoucher.VoucherType = dr["VoucherType"].ToString();
                getVoucher.Amount = dr["Amount"].ToString();
                getVoucher.Empid = dr["Empid"].ToString();
                getVoucher.Sno = dr["Sno"].ToString();
                Voucherlist.Add(getVoucher);
            }
            string response = GetJson(Voucherlist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void BtnClearRaiseVoucherClick(HttpContext context)
    {

        try
        {
            vdm = new SalesDBManager();
            string Description = context.Request["Description"];
            string Amount = context.Request["Amount"];
            string VoucherType = context.Request["VoucherType"];
            string CashTo = context.Request["CashTo"];
            string EmpName = context.Request["Employee"];
            string Remarks = context.Request["EmpName"];
            string EmpApprove = context.Request["AprovedBy"];
            cmd = new SqlCommand("Select IFNULL(MAX(VocherID),0)+1 as VocherID  from cashpayables where BranchID=@BranchID");
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            DataTable dtTripId = vdm.SelectQuery(cmd).Tables[0];
            string VocherID = "0";
            if (dtTripId.Rows.Count > 0)
            {
                VocherID = dtTripId.Rows[0]["VocherID"].ToString();
            }
            else
            {
                VocherID = "1";
            }
            cmd = new SqlCommand("Insert into cashpayables (BranchID,CashTo,DOE,VocherID,Remarks,Amount,EmpId,Approvedby,onNameof,Status,Created_by,VoucherType) values (@BranchID,@CashTo,@DOE,@VocherID,@Remarks,@Amount,@EmpId,@Approvedby,@onNameof,@Status,@Created_by,@VoucherType)");
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            cmd.Parameters.Add("@CashTo", CashTo);
            cmd.Parameters.Add("@DOE", DateTime.Now);
            cmd.Parameters.Add("@VocherID", VocherID);
            cmd.Parameters.Add("@Remarks", Remarks);
            cmd.Parameters.Add("@Amount", Amount);
            cmd.Parameters.Add("@EmpId", EmpName);
            cmd.Parameters.Add("@Approvedby", EmpApprove);
            cmd.Parameters.Add("@onNameof", Description);
            cmd.Parameters.Add("@Status", "R");
            cmd.Parameters.Add("@Created_by", context.Session["UserSno"].ToString());
            cmd.Parameters.Add("@VoucherType", VoucherType);
            vdm.insert(cmd);
            string msg = VocherID;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
    }
    private void BtnVarifyVoucherSaveClick(HttpContext context)
    {
        vdm = new SalesDBManager();
        string VoucherID = context.Request["VoucherID"];
        string ReceivedAmount = context.Request["ReceivedAmount"];
        string Due = context.Request["Due"];
        cmd = new SqlCommand("Update cashpayables set Status=@Status,ReceivedAmount=@ReceivedAmount,DueAmount=@DueAmount where BranchID=@BranchID and VocherID=@VocherID");
        cmd.Parameters.Add("@Status", "V");
        cmd.Parameters.Add("@VocherID", VoucherID);
        double RAmount = 0;
        double.TryParse(ReceivedAmount, out RAmount);
        cmd.Parameters.Add("@ReceivedAmount", ReceivedAmount);
        double RDue = 0;
        double.TryParse(Due, out RDue);
        cmd.Parameters.Add("@DueAmount", RDue);
        cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
        vdm.Update(cmd);
        string msg = "Voucher Varified successfully";
        string response = GetJson(msg);
        context.Response.Write(response);
    }
    private void btnVoucherCancelClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            cmd = new SqlCommand("Update cashpayables set Status=@Status where BranchID=@BranchID and Sno=@VocherID");
            cmd.Parameters.Add("@Status", "C");
            cmd.Parameters.Add("@VocherID", VoucherID);
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            vdm.Update(cmd);
            string msg = "Voucher Cancel successfully";
            string response = GetJson(msg);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
    }
    private void GetSubPaybleValues(HttpContext context)
    {

        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            string branchid = "0";
            branchid = context.Session["branch"].ToString();
            List<Subpayables> SubpayableList = new List<Subpayables>();
            cmd = new SqlCommand("SELECT Sno FROM cashpayables WHERE (Sno = @VocherID) AND (BranchID = @BranchID)");
            //cmd = new SqlCommand("SELECT Sno FROM cashpayables WHERE (Sno = @VocherID) ");
            cmd.Parameters.Add("@VocherID", VoucherID);
            // cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            cmd.Parameters.Add("@BranchID", branchid);
            DataTable dtCash = vdm.SelectQuery(cmd).Tables[0];
            string RefNo = dtCash.Rows[0]["Sno"].ToString();
            cmd = new SqlCommand("SELECT accountheads.HeadName, subpayable.Amount, accountheads.Sno FROM subpayable INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE  (subpayable.RefNo = @RefNo)");
            cmd.Parameters.Add("@RefNo", RefNo);
            DataTable dtCashSubPayable = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtCashSubPayable.Rows)
            {
                Subpayables GetSubpayable = new Subpayables();
                GetSubpayable.HeadSno = dr["Sno"].ToString();
                GetSubpayable.HeadOfAccount = dr["HeadName"].ToString();
                GetSubpayable.Amount = dr["Amount"].ToString();
                SubpayableList.Add(GetSubpayable);
            }
            string response = GetJson(SubpayableList);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string response = GetJson(ex.Message);
            context.Response.Write(response);
        }
    }
    class Subpayables
    {
        public string HeadSno { get; set; }
        public string HeadOfAccount { get; set; }
        public string Amount { get; set; }

    }
    private void btnVoucherPrintClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            context.Session["VoucherID"] = context.Request["VoucherID"];
            context.Session["BrachSOID"] = context.Request["BranchID"];
            string msg = "Success";
            string response = GetJson(msg);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string response = GetJson(ex.Message);
            context.Response.Write(response);
        }
    }
    private void btnViewVoucherGeneretaeClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            List<VoucherClass> Voucherlist = new List<VoucherClass>();
            string fromdate = context.Request["fromdate"];
            string ToDate = context.Request["ToDate"];
            string statusType = context.Request["Type"];
            DateTime dtFromdate = Convert.ToDateTime(fromdate);
            DateTime dtTodate = Convert.ToDateTime(ToDate);
            DataTable dtVouchers = new DataTable();

            string BranchID = "0";
            string LevelType = context.Session["LevelType"].ToString();
            if (LevelType == "AccountsOfficer" || LevelType == "Director")
            {
                BranchID = context.Request["BranchID"];
            }
            else
            {
                BranchID = context.Session["branch"].ToString();
            }
            if (statusType == "All")
            {
                //cmd = new SqlCommand("SELECT cashpayables.Status, cashpayables.VocherID, cashpayables.CashTo,cashpayables.AccountType, cashpayables.onNameof, cashpayables.DOE, cashpayables.Amount, cashpayables.ApprovedAmount, cashpayables.Remarks, cashpayables.ApprovalRemarks, cashpayables.CashierRemarks, cashpayables.VoucherType, empmanage.EmpName, empmanage_1.EmpName AS ApproveEmpName FROM cashpayables INNER JOIN empmanage empmanage_1 ON cashpayables.Approvedby = empmanage_1.Sno LEFT OUTER JOIN empmanage ON cashpayables.Empid = empmanage.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.DOE BETWEEN @d1 AND @d2)");
                cmd = new SqlCommand("SELECT * FROM cashpayables WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.DOE BETWEEN @d1 AND @d2)");
                cmd.Parameters.Add("@d1", GetLowDate(dtFromdate));
                cmd.Parameters.Add("@d2", GetHighDate(dtTodate));
                cmd.Parameters.Add("@BranchID", BranchID);
                dtVouchers = vdm.SelectQuery(cmd).Tables[0];
            }
            else
            {
                //cmd = new SqlCommand("SELECT cashpayables.Status, cashpayables.VocherID,cashpayables.CashTo,cashpayables.onNameof, cashpayables.DOE, cashpayables.Amount, cashpayables.ApprovedAmount, cashpayables.Remarks, cashpayables.ApprovalRemarks, cashpayables.CashierRemarks, cashpayables.VoucherType, empmanage.EmpName, empmanage_1.EmpName AS ApproveEmpName FROM cashpayables INNER JOIN empmanage ON cashpayables.Empid = empmanage.Sno INNER JOIN empmanage empmanage_1 ON cashpayables.Approvedby = empmanage_1.Sno WHERE (cashpayables.DOE BETWEEN @d1 AND @d2) AND (cashpayables.BranchID = @BranchID) AND (cashpayables.Status = @Status) ");
                cmd = new SqlCommand("SELECT * FROM cashpayables WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.DOE BETWEEN @d1 AND @d2) AND (Status = @Status)");
                //cmd = new SqlCommand("SELECT cashpayables.BranchID,cashpayables.Status,cashpayables.Sno, cashpayables.VocherID, cashpayables.CashTo,cashpayables.AccountType, cashpayables.onNameof, cashpayables.DOE, cashpayables.Amount, cashpayables.ApprovedAmount, cashpayables.Remarks, cashpayables.ApprovalRemarks, cashpayables.CashierRemarks, cashpayables.VoucherType, empmanage.EmpName, empmanage_1.EmpName AS ApproveEmpName FROM cashpayables INNER JOIN empmanage empmanage_1 ON cashpayables.Approvedby = empmanage_1.Sno LEFT OUTER JOIN empmanage ON cashpayables.Empid = empmanage.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.DOE BETWEEN @d1 AND @d2) AND (cashpayables.Status = @Status)");
                cmd.Parameters.Add("@d1", GetLowDate(dtFromdate));
                cmd.Parameters.Add("@d2", GetHighDate(dtTodate));
                cmd.Parameters.Add("@Status", statusType);
                cmd.Parameters.Add("@BranchID", BranchID);
                dtVouchers = vdm.SelectQuery(cmd).Tables[0];
            }
            if (dtVouchers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtVouchers.Rows)
                {
                    VoucherClass getVoucher = new VoucherClass();
                    //getVoucher.VoucherID = dr["VocherID"].ToString();
                    getVoucher.VoucherID = dr["Sno"].ToString();

                    string EmpName = "";// dr["EmpName"].ToString();
                    if (EmpName == "")
                    {
                        EmpName = "select";
                    }
                    getVoucher.EmpName = EmpName;
                    getVoucher.BranchID = dr["BranchID"].ToString();
                    getVoucher.VoucherType = dr["VoucherType"].ToString();
                    getVoucher.CashTo = dr["onNameof"].ToString();
                    getVoucher.Description = dr["onNameof"].ToString();
                    getVoucher.Amount = dr["Amount"].ToString();
                    getVoucher.AccountType = dr["AccountType"].ToString();
                    getVoucher.ApprovalAmount = dr["ApprovedAmount"].ToString();
                    getVoucher.ApproveEmpName = "";// dr["ApproveEmpName"].ToString();
                    string Status = dr["Status"].ToString();
                    if (Status == "R")
                    {
                        Status = "Raised";
                    }
                    if (Status == "A")
                    {
                        Status = "Approved";
                    }
                    if (Status == "C")
                    {
                        Status = "Rejected";
                    }
                    if (Status == "P")
                    {
                        Status = "Paid";
                    }

                    getVoucher.Status = Status;
                    getVoucher.ApprovalRemarks = dr["ApprovalRemarks"].ToString();
                    getVoucher.Remarks = dr["Remarks"].ToString();
                    Voucherlist.Add(getVoucher);
                }
                string response = GetJson(Voucherlist);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }
    private void BtnPayVoucherClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            string ApprovalAmount = context.Request["ApprovalAmount"];
            double Amount = 0;
            double.TryParse(ApprovalAmount, out Amount);
            string Remarks = context.Request["Remarks"];
            string Force = context.Request["Force"];
            string VoucherType = context.Request["VoucherType"];
            string DOE = context.Request["DOE"];
            string Denominations = context.Request["DenominationString"];
            DateTime dtpaidtime = new DateTime();
            dtpaidtime = DateTime.Parse(DOE);
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);

            cmd = new SqlCommand("SELECT Branchid, UserData_sno, AmountPaid, Denominations, Remarks, Sno, PaidDate FROM collections WHERE (Branchid = @BranchID) AND (PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            DataTable dtcashbookstatus = vdm.SelectQuery(cmd).Tables[0];
            if (dtcashbookstatus.Rows.Count > 0)
            {
                string msg = "Cash Book Has Been Closed For This Day";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("Update cashpayables set DOE=@DOE,CashierRemarks=@CashierRemarks,Status=@Status,ForceApproval=@ForceApproval,Denominations=@Denominations where BranchID=@BranchID and Sno=@VocherID");
                cmd.Parameters.Add("@CashierRemarks", Remarks);
                cmd.Parameters.Add("@Status", "P");
                cmd.Parameters.Add("@ForceApproval", Force);
                cmd.Parameters.Add("@VocherID", VoucherID);
                cmd.Parameters.Add("@Denominations", Denominations);
                string branchid = context.Session["branch"].ToString();
                if (branchid == "570")
                {
                    cmd.Parameters.Add("@DOE", dtpaidtime);
                }
                if (branchid != "570")
                {
                    cmd.Parameters.Add("@DOE", ServerDateCurrentdate);

                }
                cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
                vdm.Update(cmd);
                //cmd = new SqlCommand("Update BranchAccounts set Amount=Amount-@Amount where BranchID=@BranchID");
                //cmd.Parameters.Add("@Amount", Amount);
                //cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
                //vdm.Update(cmd);
                string msg = "Voucher Paid successfully";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
    }
    class CashForms
    {
        public string op { set; get; }
        public List<CashDetails> CashDetails { set; get; }
        public string Description { set; get; }
        public string Amount { set; get; }
        public string Remarks { set; get; }
        public string EmpApprove { set; get; }
        public string VoucherType { set; get; }
        public string CashTo { set; get; }
        public string Employee { set; get; }
        public string btnSave { set; get; }
        public string spnVoucherID { set; get; }
        public string AccountType { set; get; }
        public string CashType { set; get; }
        public string ddlBillHead { set; get; }
        public string BranchID { set; get; }
    }
    class CashDetails
    {
        public string SNo { set; get; }
        public string Account { set; get; }
        public string amount { set; get; }
        public string Qty { set; get; }
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
    class VoucherClass
    {
        public string VoucherID { get; set; }
        public string EmpName { get; set; }
        public string Amount { get; set; }
        public string Empid { get; set; }
        public string Sno { get; set; }
        public string ApproveEmpName { get; set; }
        public string VoucherType { get; set; }
        public string CashTo { get; set; }
        public string Description { get; set; }
        public string ApprovalRemarks { get; set; }
        public string ApprovalAmount { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string ApprovedBy { get; set; }
        public string AccountType { get; set; }
        public string BranchID { get; set; }

    }
    private void BtnGetVoucherClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string VoucherID = context.Request["VoucherID"];
            List<VoucherClass> Voucherlist = new List<VoucherClass>();
            cmd = new SqlCommand("SELECT Sno, BranchID, CashTo, DOE, VocherID, Remarks, Amount, Approvedby, ApprovedAmount, ApprovalRemarks, Status, Created_by, Modify_by, Empid, onNameof, CloBal, VoucherType, CashierRemarks, ForceApproval, ReceivedAmount, DueAmount, AccountType, AgentID, DOR, Denominations FROM cashpayables WHERE (Sno = @VocherID) AND (BranchID = @BranchID)");
            //cmd = new SqlCommand("SELECT  empmanage.EmpName,cashpayables.Empid, cashpayables.Approvedby, cashpayables.VoucherType, cashpayables.CashTo, cashpayables.onNameof, cashpayables.Amount, cashpayables.ApprovedAmount, empmanage_1.EmpName AS ApproveEmpName, cashpayables.Status, cashpayables.ApprovalRemarks, cashpayables.Remarks FROM empmanage empmanage_1 INNER JOIN cashpayables ON empmanage_1.Sno = cashpayables.Approvedby LEFT OUTER JOIN empmanage ON cashpayables.Empid = empmanage.Sno WHERE (cashpayables.Sno = @VoucherID) AND (cashpayables.BranchID = @  )");
            cmd.Parameters.Add("@VocherID", VoucherID);
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            DataTable dtVouchers = vdm.SelectQuery(cmd).Tables[0];
            if (dtVouchers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtVouchers.Rows)
                {
                    VoucherClass getVoucher = new VoucherClass();
                    //getVoucher.EmpName = dr["EmpName"].ToString();
                    getVoucher.VoucherType = dr["VoucherType"].ToString();
                    getVoucher.CashTo = dr["CashTo"].ToString();
                    getVoucher.Description = dr["onNameof"].ToString();
                    getVoucher.Amount = dr["Amount"].ToString();
                    getVoucher.ApprovalAmount = dr["Amount"].ToString();
                    //getVoucher.ApproveEmpName = dr["ApproveEmpName"].ToString();
                    getVoucher.Status = dr["Status"].ToString();
                    getVoucher.ApprovalRemarks = dr["Remarks"].ToString();
                    getVoucher.Remarks = dr["Remarks"].ToString();
                    getVoucher.Empid = dr["EmpID"].ToString();
                    getVoucher.ApprovedBy = dr["Approvedby"].ToString();
                    Voucherlist.Add(getVoucher);
                }
                string response = GetJson(Voucherlist);
                context.Response.Write(response);
            }
            else
            {
                string msg = "No voucher found";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }
    private void BtnRaiseVoucherClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            var js = new JavaScriptSerializer();
            var title1 = context.Request.Params[1];
            CashForms obj = js.Deserialize<CashForms>(title1);
            string Description = obj.Description;
            string Amount = obj.Amount;
            string Remarks = obj.Remarks;
            string EmpApprove = obj.EmpApprove;
            string VoucherType = obj.VoucherType;
            string CashTo = obj.CashTo;
            string Employee = obj.Employee;
            string btnSave = obj.btnSave;
            string CashType = obj.CashType;
            string BranchID = "0";
            string LevelType = context.Session["LevelType"].ToString();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            DateTime dtapril = new DateTime();
            DateTime dtmarch = new DateTime();
            int currentyear = ServerDateCurrentdate.Year;
            int nextyear = ServerDateCurrentdate.Year + 1;
            if (ServerDateCurrentdate.Month > 3)
            {
                string apr = "4/1/" + currentyear;
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + nextyear;
                dtmarch = DateTime.Parse(march);
            }
            if (ServerDateCurrentdate.Month <= 3)
            {
                string apr = "4/1/" + (currentyear - 1);
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + (nextyear - 1);
                dtmarch = DateTime.Parse(march);
            }
            if (LevelType == "AccountsOfficer" || LevelType == "Director")
            {
                BranchID = obj.BranchID;
            }
            else
            {
                BranchID = context.Session["branch"].ToString();
            }
            string msg = "";
            if (btnSave == "Raise")
            {
                //cmd = new SqlCommand("Select IFNULL(MAX(VocherID),0)+1 as VocherID  from cashpayables where BranchID=@BranchID");
                cmd = new SqlCommand("SELECT  { fn IFNULL(MAX(VocherID), 0) } + 1 AS VocherID FROM cashpayables WHERE (BranchID = @BranchID) AND (DOR BETWEEN @d1 AND @d2)");
                cmd.Parameters.Add("@BranchID", BranchID);
                cmd.Parameters.Add("@d1", GetLowDate(dtapril));
                cmd.Parameters.Add("@d2", GetHighDate(dtmarch));
                DataTable dtTripId = vdm.SelectQuery(cmd).Tables[0];
                string VocherID = "0";
                if (dtTripId.Rows.Count > 0)
                {
                    VocherID = dtTripId.Rows[0]["VocherID"].ToString();
                }
                else
                {
                    VocherID = "1";
                }
                if (VoucherType == "Credit")
                {
                    cmd = new SqlCommand("Insert into cashpayables (BranchID,CashTo,DOE,VocherID,ApprovalRemarks,ApprovedAmount,onNameof,Status,Created_by,VoucherType,DOR) values (@BranchID,@CashTo,@DOE,@VocherID,@ApprovalRemarks,@ApprovedAmount,@onNameof,@Status,@Created_by,@VoucherType,@DOR)");
                    cmd.Parameters.Add("@BranchID", BranchID);
                    cmd.Parameters.Add("@CashTo", CashTo);
                    cmd.Parameters.Add("@DOE", ServerDateCurrentdate);
                    cmd.Parameters.Add("@DOR", ServerDateCurrentdate);
                    cmd.Parameters.Add("@VocherID", VocherID);
                    cmd.Parameters.Add("@ApprovalRemarks", Remarks);
                    cmd.Parameters.Add("@ApprovedAmount", Amount);
                    cmd.Parameters.Add("@onNameof", Description);
                    cmd.Parameters.Add("@Status", "P");
                    cmd.Parameters.Add("@Created_by", context.Session["UserSno"].ToString());
                    cmd.Parameters.Add("@VoucherType", VoucherType);
                    vdm.insert(cmd);
                    cmd = new SqlCommand("Select  MAX(sno) as vendorrefno from cashpayables");
                    DataTable dtVendor = vdm.SelectQuery(cmd).Tables[0];
                    string CashSno = dtVendor.Rows[0]["vendorrefno"].ToString();
                    foreach (CashDetails o in obj.CashDetails)
                    {
                        cmd = new SqlCommand("Insert into Subpayable (RefNo,HeadSno,Amount) values (@RefNo,@HeadSno,@Amount)");
                        cmd.Parameters.Add("@RefNo", CashSno);
                        cmd.Parameters.Add("@HeadSno", o.SNo);
                        cmd.Parameters.Add("@Amount", o.amount);
                        vdm.insert(cmd);
                    }
                    if (CashType == "Cash")
                    {
                        cmd = new SqlCommand("Update BranchAccounts set Amount=Amount-@Amount where BranchID=@BranchID");
                        cmd.Parameters.Add("@Amount", Amount);
                        cmd.Parameters.Add("@BranchID", BranchID);
                        vdm.Update(cmd);
                    }
                    if (CashType == "Bills")
                    {
                        cmd = new SqlCommand("Insert into cashpayables (BranchID,CashTo,DOE,VocherID,Remarks,Amount,Approvedby,onNameof,Status,Created_by,VoucherType,DOR) values (@BranchID,@CashTo,@DOE,@VocherID,@Remarks,@Amount,@Approvedby,@onNameof,@Status,@Created_by,@VoucherType,@DOR)");
                        cmd.Parameters.Add("@BranchID", BranchID);
                        cmd.Parameters.Add("@CashTo", obj.ddlBillHead);
                        cmd.Parameters.Add("@DOE", ServerDateCurrentdate);
                        cmd.Parameters.Add("@DOR", ServerDateCurrentdate);
                        cmd.Parameters.Add("@VocherID", VocherID);
                        cmd.Parameters.Add("@Remarks", Remarks);
                        cmd.Parameters.Add("@Amount", Amount);
                        cmd.Parameters.Add("@Approvedby", EmpApprove);
                        cmd.Parameters.Add("@onNameof", Description);
                        cmd.Parameters.Add("@Status", "A");
                        cmd.Parameters.Add("@Created_by", context.Session["UserSno"].ToString());
                        cmd.Parameters.Add("@VoucherType", "Debit");
                        vdm.insert(cmd);
                        cmd = new SqlCommand("Select  MAX(sno) as vendorrefno from cashpayables");
                        DataTable dtVendor1 = vdm.SelectQuery(cmd).Tables[0];
                        string Cash_Sno = dtVendor1.Rows[0]["vendorrefno"].ToString();
                        foreach (CashDetails o in obj.CashDetails)
                        {
                            cmd = new SqlCommand("Insert into Subpayable (RefNo,HeadSno,Amount) values (@RefNo,@HeadSno,@Amount)");
                            cmd.Parameters.Add("@RefNo", Cash_Sno);
                            cmd.Parameters.Add("@HeadSno", o.SNo);
                            cmd.Parameters.Add("@Amount", o.amount);
                            vdm.insert(cmd);
                        }
                    }
                }
                else
                {
                    if (LevelType == "AccountsOfficer" || LevelType == "Director")
                    {
                        cmd = new SqlCommand("Insert into cashpayables (BranchID,CashTo,DOE,VocherID,Remarks,Amount,Approvedby,onNameof,Status,Created_by,VoucherType,ApprovalRemarks,ApprovedAmount,DOR) values (@BranchID,@CashTo,@DOE,@VocherID,@Remarks,@Amount,@Approvedby,@onNameof,@Status,@Created_by,@VoucherType,@ApprovalRemarks,@ApprovedAmount,@DOR)");
                        cmd.Parameters.Add("@ApprovalRemarks", Remarks);
                        cmd.Parameters.Add("@ApprovedAmount", Amount);
                        cmd.Parameters.Add("@Status", "A");
                        cmd.Parameters.Add("@Approvedby", context.Session["UserSno"].ToString());
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into cashpayables (BranchID,CashTo,DOE,VocherID,Remarks,Amount,Approvedby,onNameof,Status,Created_by,VoucherType,ApprovalRemarks,ApprovedAmount,DOR) values (@BranchID,@CashTo,@DOE,@VocherID,@Remarks,@Amount,@Approvedby,@onNameof,@Status,@Created_by,@VoucherType,@ApprovalRemarks,@ApprovedAmount,@DOR)");
                        cmd.Parameters.Add("@ApprovalRemarks", Remarks);
                        cmd.Parameters.Add("@ApprovedAmount", Amount);
                        cmd.Parameters.Add("@Status", "A");
                        cmd.Parameters.Add("@Approvedby", context.Session["UserSno"].ToString());
                    }
                    cmd.Parameters.Add("@BranchID", BranchID);
                    cmd.Parameters.Add("@CashTo", CashTo);
                    cmd.Parameters.Add("@DOE", ServerDateCurrentdate);
                    cmd.Parameters.Add("@DOR", ServerDateCurrentdate);

                    cmd.Parameters.Add("@VocherID", VocherID);
                    cmd.Parameters.Add("@Remarks", Remarks);
                    cmd.Parameters.Add("@Amount", Amount);
                    cmd.Parameters.Add("@onNameof", Description);
                    cmd.Parameters.Add("@Created_by", context.Session["UserSno"].ToString());
                    cmd.Parameters.Add("@VoucherType", VoucherType);
                    vdm.insert(cmd);
                    cmd = new SqlCommand("Select  MAX(sno) as vendorrefno from cashpayables");
                    DataTable dtVendor1 = vdm.SelectQuery(cmd).Tables[0];
                    string Cash_Sno = dtVendor1.Rows[0]["vendorrefno"].ToString();
                    foreach (CashDetails o in obj.CashDetails)
                    {
                        cmd = new SqlCommand("Insert into Subpayable (RefNo,HeadSno,Amount) values (@RefNo,@HeadSno,@Amount)");
                        cmd.Parameters.Add("@RefNo", Cash_Sno);
                        cmd.Parameters.Add("@HeadSno", o.SNo);
                        cmd.Parameters.Add("@Amount", o.amount);
                        vdm.insert(cmd);
                    }
                }
                msg = "Voucher raised successfully";
            }
            else
            {
                string VocherID = obj.spnVoucherID;
                cmd = new SqlCommand("Update  cashpayables Set Amount=@Amount,EmpId=@EmpId,Remarks=@Remarks,onNameof=@onNameof,Modify_by=@Modify_by,CashTo=@CashTo,Approvedby=@Approvedby,VoucherType=@VoucherType where Sno=@VocherId and BranchID=@BranchID");
                cmd.Parameters.Add("@Amount", Amount);
                cmd.Parameters.Add("@EmpId", Employee);
                cmd.Parameters.Add("@Remarks", Remarks);
                cmd.Parameters.Add("@Modify_by", context.Session["UserSno"].ToString());
                cmd.Parameters.Add("@CashTo", CashTo);
                cmd.Parameters.Add("@Approvedby", EmpApprove);
                cmd.Parameters.Add("@VocherId", VocherID);
                cmd.Parameters.Add("@BranchID", BranchID);
                cmd.Parameters.Add("@onNameof", Description);
                cmd.Parameters.Add("@VoucherType", VoucherType);
                vdm.Update(cmd);
                cmd = new SqlCommand("SELECT Sno FROM cashpayables WHERE (BranchID = @BranchID) AND (Sno = @VocherID)");
                cmd.Parameters.Add("@BranchID", BranchID);
                cmd.Parameters.Add("@VocherID", VocherID);
                DataTable dtCash = vdm.SelectQuery(cmd).Tables[0];
                if (dtCash.Rows.Count > 0)
                {
                    string CashRefNo = dtCash.Rows[0]["Sno"].ToString();
                    cmd = new SqlCommand("Delete from Subpayable where RefNo=@RefNo");
                    cmd.Parameters.Add("@RefNo", CashRefNo);
                    vdm.Delete(cmd);
                    foreach (CashDetails o in obj.CashDetails)
                    {
                        cmd = new SqlCommand("Insert into Subpayable (RefNo,HeadSno,Amount) values (@RefNo,@HeadSno,@Amount)");
                        cmd.Parameters.Add("@RefNo", CashRefNo);
                        cmd.Parameters.Add("@HeadSno", o.SNo);
                        cmd.Parameters.Add("@Amount", o.amount);
                        vdm.insert(cmd);
                    }
                }
                msg = "Voucher Updated successfully";
            }
            string response = GetJson(msg);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
    }
    private void GetHeadLimit(HttpContext context)
    {
        vdm = new SalesDBManager();
        string HeadSno = context.Request["HeadSno"];
        cmd = new SqlCommand("SELECT Sno, BranchId, HeadName, LimitAmount FROM accountheads WHERE (Sno = @Sno)");
        cmd.Parameters.Add("@Sno", HeadSno);
        DataTable dtHead = vdm.SelectQuery(cmd).Tables[0];
        string HeadLimit = "0";
        if (dtHead.Rows.Count > 0)
        {
            HeadLimit = dtHead.Rows[0]["LimitAmount"].ToString();
        }
        string response = GetJson(HeadLimit);
        context.Response.Write(response);
    }
    private void GetApproveEmployeeNames(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            DataTable dtEmployee = new DataTable();
            cmd = new SqlCommand("SELECT Sno, EmpName FROM empmanage WHERE (Branch = @BranchID) and (Leveltype='Manager')");
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            dtEmployee = vdm.SelectQuery(cmd).Tables[0];
            List<Employee> Employeelist = new List<Employee>();
            foreach (DataRow dr in dtEmployee.Rows)
            {
                Employee b = new Employee() { Sno = dr["Sno"].ToString(), UserName = dr["EmpName"].ToString() };
                Employeelist.Add(b);
            }
            string response = GetJson(Employeelist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void GetEmployeeNames(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            DataTable dtEmployee = new DataTable();
            cmd = new SqlCommand("SELECT Sno, EmpName FROM empmanage WHERE (Branch = @BranchID)");
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            dtEmployee = vdm.SelectQuery(cmd).Tables[0];
            List<Employee> Employeelist = new List<Employee>();
            foreach (DataRow dr in dtEmployee.Rows)
            {
                Employee b = new Employee() { Sno = dr["Sno"].ToString(), UserName = dr["EmpName"].ToString() };
                Employeelist.Add(b);
            }
            string response = GetJson(Employeelist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    class Employee
    {
        public string Sno { get; set; }
        public string UserName { get; set; }
    }
    public class Branches
    {
        public string branchName { get; set; }
        public string address { get; set; }
        public string type { get; set; }
        public string mobno { get; set; }
        public string email { get; set; }
        public string mitno { get; set; }
        public string branchcode { get; set; }
        public string Sno { get; set; }
    }
    class HeadClass
    {
        public string Sno { get; set; }
        public string HeadName { get; set; }
        public string Limit { get; set; }
        public string AccountType { get; set; }
        public string Code { get; set; }
        public string ledger_code { get; set; }
        public string plantcode { get; set; }


    }
    class ExpencesClass
    {
        public string Sno { get; set; }
        public string ExpencesName { get; set; }
        public string ExpencesType { get; set; }
        public string ExpencesFor { get; set; }
        //public string routeid { get; set; }
        public string ExpencesPeriod { get; set; }
    }
    SalesDBManager vdm;
    private void GetHeadNames(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            List<HeadClass> HeadClasslist = new List<HeadClass>();
            string BranchID = context.Session["branch"].ToString();
            cmd = new SqlCommand("Select Sno,HeadName,LimitAmount,AccountType,AgentID,EmpID from accountheads Where BranchID=@BranchID order by HeadName");
            cmd.Parameters.Add("@BranchID", BranchID);
            DataTable dtVehicle = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtVehicle.Rows)
            {
                HeadClass getVehicles = new HeadClass();
                getVehicles.Sno = dr["Sno"].ToString();
                getVehicles.HeadName = dr["HeadName"].ToString();
                getVehicles.Limit = dr["LimitAmount"].ToString();
                HeadClasslist.Add(getVehicles);
            }
            string response = GetJson(HeadClasslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void get_Branch_details(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();

            cmd = new SqlCommand("SELECT sno, branchname, address, phonenumber,emailid FROM branchdata where (SalesType=77) and (sno=@BranchID)");
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<Branches> Departmentslst = new List<Branches>();
            foreach (DataRow dr in routes.Rows)
            {
                Branches getroutes = new Branches();
                getroutes.Sno = dr["sno"].ToString();
                getroutes.branchName = dr["branchname"].ToString();
                getroutes.address = dr["address"].ToString();
                getroutes.mobno = dr["phonenumber"].ToString();
                getroutes.email = dr["emailid"].ToString();
                Departmentslst.Add(getroutes);
            }
            string response = GetJson(Departmentslst);
            context.Response.Write(response);
        }
        catch
        {
        }
    }

    private void save_branchdata_click(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string branchname = context.Request["branchname"];
            string address = context.Request["address"];

            string branchcode = context.Request["branchcode"];
            string mobno = context.Request["mobno"];
            string email = context.Request["email"];

            string btnval = context.Request["btnval"];
            if (btnval == "Save")
            {
                cmd = new SqlCommand("insert into  branchdata (branchname,address,phonenumber,emailid,flag,SalesType) values (@branchname,@address,@phonenumber,@emailid,@flag,@SalesType)");
                cmd.Parameters.Add("@branchname", branchname);
                cmd.Parameters.Add("@address", address);
                cmd.Parameters.Add("@phonenumber", mobno);
                cmd.Parameters.Add("@emailid", email);
                cmd.Parameters.Add("@flag", "1");
                cmd.Parameters.Add("@SalesType", "77");
                vdm.insert(cmd);
                string msg = "Branch saved successfully";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand("update  branch_info set cstno=@cstno,mitno=@mitno,branchcode=@branchcode, branchname=@branchname,address=@address,branchtype=@branchtype,tinno=@tinno where sno=@sno");
                cmd.Parameters.Add("@branchname", branchname);
                cmd.Parameters.Add("@address", address);
                cmd.Parameters.Add("@branchcode", branchcode);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "Branch update successfully";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }
    private static string GetJson(object obj)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        return jsonSerializer.Serialize(obj);
    }
    private static object GetUnJson(string obj)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        return jsonSerializer.Deserialize(obj, Type.GetType("System.Object"));
    }

    public class Payment
    {
        public string name { get; set; }
        public string amount { get; set; }
        public string acno { get; set; }
        public string ifsccode { get; set; }
        public string remarks { get; set; }
        public string doe { get; set; }
        public string status { get; set; }
        public string entryby { get; set; }
        public string bankid { get; set; }
        public string paytype { get; set; }
        public string sno { get; set; }
        public string branchid { get; set; }
        public string headsno { get; set; }
        public string entry_by { get; set; }


    }


    private void save_payment_entry_click(HttpContext context)
    {

        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string name = context.Request["name"];
            string acno = context.Request["acno"];
            string ifsccode = context.Request["ifsccode"];
            string amount = context.Request["amount"];
            string remarks = context.Request["remarks"];
            string bankid = context.Request["bankid"];
            string headname = context.Request["headname"];
            string branchid = context.Session["branch"].ToString();
            string entry_by = context.Session["UserSno"].ToString();
            DateTime doe = DateTime.Now;
            string btnval = context.Request["btnval"];
            if (btnval == "Save")
            {
                cmd = new SqlCommand("insert into  payments_entry (name,acno,ifsccode,amount,remarks,bankid,headsno,doe,status,paytype,entry_by,branchid) values (@name,@acno,@ifsccode,@amount,@remarks,@bankid,@headsno,@doe,@status,@paytype,@entry_by,@branchid)");
                cmd.Parameters.Add("@name", name);
                cmd.Parameters.Add("@acno", acno);
                cmd.Parameters.Add("@ifsccode", ifsccode);
                cmd.Parameters.Add("@amount", amount);
                cmd.Parameters.Add("@remarks", remarks);
                cmd.Parameters.Add("@bankid", bankid);
                cmd.Parameters.Add("@headsno", headname);
                cmd.Parameters.Add("@doe", doe);
                cmd.Parameters.Add("@status","p");
                cmd.Parameters.Add("@paytype","cash");
                cmd.Parameters.Add("@branchid", branchid);
                cmd.Parameters.Add("@entry_by", entry_by);
                vdm.insert(cmd);
                string msg = "Payment details are successfully  saved ";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand(" update payments_entry set name=@name,acno=@acno,ifsccode=@ifsccode,amount=@amount,remarks=@remarks,bankid=@bankid,headsno=@headsno,doe=@doe,status=@status,paytype=@paytype  where sno=@sno");
               // cmd.Parameters.Add("@branchid", branchid);
              //  cmd.Parameters.Add("@entry_by", entry_by);
                cmd.Parameters.Add("@name", name);
                cmd.Parameters.Add("@acno", acno);
                cmd.Parameters.Add("@ifsccode", ifsccode);
                cmd.Parameters.Add("@amount", amount);
                cmd.Parameters.Add("@remarks", remarks);
                cmd.Parameters.Add("@bankid", bankid);
                cmd.Parameters.Add("@headsno", headname);
                cmd.Parameters.Add("@doe", doe);
                cmd.Parameters.Add("@status", "p");
                cmd.Parameters.Add("@paytype", "cash");
                cmd.Parameters.Add("@sno", sno);
                vdm.insert(cmd);
                string msg = "Receipt details are successfully  updated ";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }

    private void GetPaymentDetails(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            cmd = new SqlCommand("SELECT sno,branchid,headsno,amount,name,remarks,doe,paytype,bankid,acno,ifsccode,status FROM payments_entry");
           // cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
           // cmd.Parameters.Add("@entry_by", context.Session["Sno"].ToString());
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<Payment> Payment = new List<Payment>();
            foreach (DataRow dr in routes.Rows)
            {
                Payment getpayment = new Payment();
                getpayment.sno = dr["sno"].ToString();
                getpayment.headsno = dr["headsno"].ToString();
                getpayment.amount = dr["amount"].ToString();
                getpayment.name = dr["name"].ToString();
                getpayment.remarks = dr["remarks"].ToString();
                getpayment.doe = dr["doe"].ToString();
                getpayment.paytype = dr["paytype"].ToString();
                getpayment.bankid = dr["bankid"].ToString();
                getpayment.acno = dr["acno"].ToString();
                getpayment.ifsccode = dr["ifsccode"].ToString();
                getpayment.status = dr["status"].ToString();
                Payment.Add(getpayment);
            }
            string response = GetJson(Payment);
            context.Response.Write(response);
        }
        catch
        {
        }

    }
    public class Receipt
    {
        public string name { get; set; }
        public string amount { get; set; }
        public string acno { get; set; }
        public string ifsccode { get; set; }
        public string remarks { get; set; }
        public string doe { get; set; }
        public string status { get; set; }
        public string entry_by { get; set; }
        public string bankid { get; set; }
        public string paytype { get; set; }
        public string sno { get; set; }
        public string branchid { get; set; }
        public string headsno { get; set; }
    }

    private void save_receipt_entry_click(HttpContext context)
    {

        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string name = context.Request["name"];
            string acno = context.Request["acno"];
            string ifsccode = context.Request["ifsccode"];
            string amount = context.Request["amount"];
            string remarks = context.Request["remarks"];
            string bankid = context.Request["bankid"];
            string headname = context.Request["headname"];
            DateTime doe = DateTime.Now;
            string branchid = context.Session["branch"].ToString();
            string entry_by = context.Session["UserSno"].ToString();
            string btnval = context.Request["btnval"];
            if (btnval == "Save")
            {
                cmd = new SqlCommand("insert into receipt_entry (name,acno,ifsccode,amount,remarks,bankid,doe,status,paytype,entry_by,branchid) values (@name,@acno,@ifsccode,@amount,@remarks,@bankid,@doe,@status,@paytype,@entry_by,@branchid)");
                cmd.Parameters.Add("@name", name);
                cmd.Parameters.Add("@acno", acno);
                cmd.Parameters.Add("@ifsccode", ifsccode);
                cmd.Parameters.Add("@amount", amount);
                cmd.Parameters.Add("@remarks", remarks);
                cmd.Parameters.Add("@bankid", bankid);
                cmd.Parameters.Add("@doe", doe);
                cmd.Parameters.Add("@status", "p");
                cmd.Parameters.Add("@paytype", "cash");
                cmd.Parameters.Add("@branchid", branchid);
                cmd.Parameters.Add("@entry_by", entry_by);
                vdm.insert(cmd);
                string msg = "Receipt details successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand(" update receipt_entry set name=@name,acno=@acno,ifsccode=@ifsccode,amount=@amount,remarks=@remarks,bankid=@bankid,doe=@doe,status=@status,paytype=@paytype  where sno=@sno");
                cmd.Parameters.Add("@name", name);
                cmd.Parameters.Add("@acno", acno);
                cmd.Parameters.Add("@ifsccode", ifsccode);
                cmd.Parameters.Add("@amount", amount);
                cmd.Parameters.Add("@remarks", remarks);
                cmd.Parameters.Add("@bankid", bankid);
                cmd.Parameters.Add("@doe", doe);
                cmd.Parameters.Add("@status", "p");
                cmd.Parameters.Add("@paytype", "cash");
                vdm.Update(cmd);
                string msg = "Receipt details successfully updated";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }
    private void GetReceiptDetails(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            cmd = new SqlCommand("SELECT sno,branchid,amount,name,remarks,doe,paytype,bankid,acno,ifsccode,status FROM receipt_entry");
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<Receipt> Receipt = new List<Receipt>();
            foreach (DataRow dr in routes.Rows)
            {
                Receipt getreceipt = new Receipt();
                getreceipt.sno = dr["sno"].ToString();
                getreceipt.amount = dr["amount"].ToString();
                getreceipt.name = dr["name"].ToString();
                getreceipt.remarks = dr["remarks"].ToString();
                getreceipt.sno = dr["doe"].ToString();
                getreceipt.paytype = dr["paytype"].ToString();
                getreceipt.bankid = dr["bankid"].ToString();
                getreceipt.acno = dr["acno"].ToString();
                getreceipt.ifsccode = dr["ifsccode"].ToString();
                getreceipt.status = dr["status"].ToString();
                Receipt.Add(getreceipt);
            }
            string response = GetJson(Receipt);
            context.Response.Write(response);
        }
        catch
        {
        }

    }

    public class LineChartValuesclass
    {
        public List<string> session { get; set; }
        public List<string> status { get; set; }
        public List<string> quantity { get; set; }
        public string Date { get; set; }
        public string plant { get; set; }
    }

    private void get_plantwise_milkdetails(HttpContext context)
    {
        try
        {
            SalesDBManager SDB = new SalesDBManager();
            string plantcode = context.Request["plantcode"].ToString();
            string frmdate = context.Request["fromdate"].ToString();
            string Session = context.Request["Sessions"].ToString();
            string sess = Session.Trim();
            DateTime fromdate = Convert.ToDateTime(frmdate);
            string cdate = fromdate.ToString("MM/dd/yyyy");
            //    cmd = new SqlCommand("SELECT Lp.StartingTime AS Time, Lp.Milkkg, Lp.PlantCode, Lp.Sessions, Lp.Prdate, Plant_Master.Plant_Name FROM Lp INNER JOIN Plant_Master ON Plant_Master.Plant_Code = Lp.PlantCode WHERE (Lp.PlantCode = @pcode) and (Lp.Prdate=@d1) and (Sessions=@SESS)");
            // cmd = new SqlCommand("SELECT Lp.Milkkg, Lp.StartingTime AS Time, Lp.PlantCode, Lp.Sessions, Lp.Prdate, Plant_Master.Plant_Name FROM Lp INNER JOIN Plant_Master ON Plant_Master.Plant_Code = Lp.PlantCode WHERE (Lp.PlantCode = @pcode) and Lp.Prdate=@d1");

            //  cmd = new SqlCommand("SELECT Lp.Milkkg, Lp.StartingTime AS Time, Lp.PlantCode, Lp.Sessions, Lp.Prdate, Plant_Master.Plant_Name FROM Lp INNER JOIN Plant_Master ON Plant_Master.Plant_Code = Lp.PlantCode WHERE (Lp.PlantCode = @pcode) and Lp.Prdate=@d1");
            if (sess == "AM")
            {
                cmd = new SqlCommand(" SELECT milkkg, startim, PlantCode, Sessions, Prdate, Plant_Name  FROM ( select milkkg,startim,PRDATE,PlantCode,Sessions   from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,PlantCode,PRDATE, Sessions from lp where plantcode=@pcode and Prdate=@d1 AND Sessions='AM'   GROUP BY   PlantCode,Sessions,Prdate,(DATEPART(MINUTE, StartingTime) / 1)   ) as ff) AS FF1 LEFT JOIN (SELECT Plant_Code,Plant_Name    FROM Plant_Master WHERE Plant_Code=@pcode) AS PM ON FF1.PlantCode=PM.Plant_Code  order by CAST(startim as time) asc");
            }
            else
            {
                cmd = new SqlCommand(" SELECT milkkg, startim, PlantCode, Sessions, Prdate, Plant_Name  FROM ( select milkkg,startim,PRDATE,PlantCode,Sessions   from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,PlantCode,PRDATE, Sessions from lp where plantcode=@pcode and Prdate=@d1 AND Sessions='PM'   GROUP BY   PlantCode,Sessions,Prdate,(DATEPART(MINUTE, StartingTime) / 1)   ) as ff) AS FF1 LEFT JOIN (SELECT Plant_Code,Plant_Name    FROM Plant_Master WHERE Plant_Code=@pcode) AS PM ON FF1.PlantCode=PM.Plant_Code  order by CAST(startim as time) asc");
            }

            //cmd = new SqlCommand("SELECT milkkg,startim,PlantCode,Sessions,Prdate,Plant_Name  FROM ( select milkkg,startim,PRDATE,PlantCode,Sessions   from ( select SUM(milkkg) as milkkg,MIN(StartingTime) as startim,PlantCode,PRDATE,Sessions    from lp where plantcode=@pcode and Prdate=@d1  AND SESSIONS='@SESS'   GROUP BY   PlantCode,Sessions,Prdate,StartingTime) as ff) AS FF1 LEFT JOIN (SELECT Plant_Code,Plant_Name    FROM Plant_Master WHERE Plant_Code=@pcode) AS PM ON FF1.PlantCode=PM.Plant_Code  order by CAST(startim as time) asc");
            cmd.Parameters.Add("@d1", cdate);
            cmd.Parameters.Add("@pcode", plantcode);
            DataTable dtroutes = SDB.SelectQuery(cmd).Tables[0];
            List<LineChartValuesclass> LineChartValuelist = new List<LineChartValuesclass>();
            if (dtroutes.Rows.Count > 0)
            {
                LineChartValuesclass getLineChart = new LineChartValuesclass();
                List<string> milklist = new List<string>();
                List<string> timelist = new List<string>();
                List<string> sessionlist = new List<string>();
                List<string> status = new List<string>();
                string qtykgs = "";
                string intime = "";
                string sessions = "";
                string plant = "";
                foreach (DataRow dr in dtroutes.Rows)
                {
                    string unitQty = dr["Milkkg"].ToString();
                    qtykgs += unitQty + ",";
                    string inwardDate = dr["startim"].ToString();
                    string session = dr["Sessions"].ToString();
                    string plantname = dr["Plant_Name"].ToString();
                    plant = plantname;

                    string date = dr["Prdate"].ToString();
                    DateTime dt = Convert.ToDateTime(date);
                    TimeSpan newtime = TimeSpan.Parse(inwardDate);
                    DateTime result = dt + newtime;
                    string TR = result.ToString();
                    string TRR = TR;
                    DateTime dtinwardDate = Convert.ToDateTime(TRR);
                    string ChangedTime = dtinwardDate.ToString("HH:mm");
                    string time = ChangedTime + session;
                    intime += time + ",";
                    sessions += session + ",";
                }
                intime = intime.Substring(0, intime.Length - 1);
                qtykgs = qtykgs.Substring(0, qtykgs.Length - 1);
                sessions = sessions.Substring(0, sessions.Length - 1);
                milklist.Add(qtykgs);
                sessionlist.Add(sessions);
                status.Add("Qty Kgs");
                getLineChart.quantity = milklist;
                getLineChart.Date = intime;
                getLineChart.status = status;
                getLineChart.session = sessionlist;
                getLineChart.plant = plant;
                LineChartValuelist.Add(getLineChart);
            }
            string errresponse = GetJson(LineChartValuelist);
            context.Response.Write(errresponse);
        }
        catch (Exception ex)
        {

        }
    }


    private void get_branchsummarywise_milkdetails(HttpContext context)
    {
        SalesDBManager SDB = new SalesDBManager();
        string status = context.Request["status"].ToString();
        string date = context.Request["date"].ToString();
        string qtykgs = "";
        string vendorname = "";
        DateTime mydate = DateTime.Now;
        string year = (mydate.Year).ToString();
        string mymonth = status;
        string day = "01";
        int month = Convert.ToInt32(mymonth);
        int myyear = Convert.ToInt32(year);
        DateTime ServerDateCurrentdate = SalesDBManager.GetTime(SDB.conn);
        cmd = new SqlCommand("SELECT Plant_Code, Plant_Name from Plant_Master");
        DataTable dtVendor = SDB.SelectQuery(cmd).Tables[0];
        if (dtVendor.Rows.Count > 0)
        {
            foreach (DataRow drven in dtVendor.Rows)
            {
                string plantcode = drven["Plant_Code"].ToString();
                if (status == "00")
                {
                    DateTime dtdate = Convert.ToDateTime(date);
                    cmd = new SqlCommand("SELECT  SUM(Lp.Milkkg) AS milkltr, Plant_Master.Plant_Name FROM Lp INNER JOIN Plant_Master ON Plant_Master.Plant_Code = Lp.PlantCode WHERE (Lp.PlantCode = @plantcode) AND (Lp.Prdate BETWEEN @d1 AND @d2) GROUP BY Plant_Master.Plant_Name");
                    cmd.Parameters.Add("@d1", GetLowDate(dtdate));
                    cmd.Parameters.Add("@d2", GetHighDate(dtdate));
                    cmd.Parameters.Add("@plantcode", plantcode);
                }
                else
                {
                    string strfromdate = mymonth + "/" + day + "/" + year;
                    DateTime fromdate = Convert.ToDateTime(strfromdate);
                    int days = DateTime.DaysInMonth(myyear, month);
                    string fromday = days.ToString();
                    string strtodate = mymonth + "/" + fromday + "/" + year;
                    DateTime todate = Convert.ToDateTime(strtodate);
                    cmd = new SqlCommand("SELECT  SUM(Lp.Milkkg) AS milkltr, Plant_Master.Plant_Name FROM Lp INNER JOIN Plant_Master ON Plant_Master.Plant_Code = Lp.PlantCode WHERE (Lp.PlantCode = @plantcode) AND (Lp.Prdate BETWEEN @d1 AND @d2) GROUP BY Plant_Master.Plant_Name");
                    cmd.Parameters.Add("@d1", GetLowDate(fromdate));
                    cmd.Parameters.Add("@d2", GetHighDate(todate));
                    cmd.Parameters.Add("@plantcode", drven["Plant_Code"].ToString());
                }
                DataTable dtDispatch = SDB.SelectQuery(cmd).Tables[0];
                if (dtDispatch.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDispatch.Rows)
                    {
                        string unitQty = dr["milkltr"].ToString();
                        qtykgs += unitQty + ",";
                        string vendor = dr["Plant_Name"].ToString();
                        vendorname += vendor + ",";
                    }
                }

            }
        }
        List<LineChartValuesclass> LineChartValuelist = new List<LineChartValuesclass>();
        LineChartValuesclass getLineChart = new LineChartValuesclass();
        List<string> quantitylist = new List<string>();
        List<string> Datelist = new List<string>();
        List<string> statuss = new List<string>();
        vendorname = vendorname.Substring(0, vendorname.Length - 1);
        qtykgs = qtykgs.Substring(0, qtykgs.Length - 1);
        quantitylist.Add(qtykgs);
        //Datelist.Add(inDate);
        statuss.Add("Qty ltrs");
        getLineChart.quantity = quantitylist;
        getLineChart.Date = vendorname;
        getLineChart.status = statuss;
        LineChartValuelist.Add(getLineChart);
        string errresponse = GetJson(LineChartValuelist);
        context.Response.Write(errresponse);
    }

    private void get_plant_details(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name  FROM   Plant_Master");
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<BankMaster> bankMasterlist = new List<BankMaster>();
            foreach (DataRow dr in routes.Rows)
            {
                BankMaster getbankdetails = new BankMaster();
                getbankdetails.name = dr["Plant_Name"].ToString();
                getbankdetails.code = dr["Plant_Code"].ToString();
                bankMasterlist.Add(getbankdetails);
            }
            string response = GetJson(bankMasterlist);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);

        }
    }
    private void Save_Route_Time_Maintenance(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Route_id = context.Request["Route_id"];
            string date = context.Request["date"];
            string session = context.Request["session"];
            string starttime = context.Request["starttime"];
            string intime = context.Request["intime"];
            string mbrt = context.Request["mbrt"];
            string acidity = context.Request["acidity"];
            DateTime doe = DateTime.Now;
            string branchid = context.Session["branch"].ToString();
            string entry_by = context.Session["UserSno"].ToString();
            string btnval = context.Request["btnval"];
            if (btnval == "Save")
            {

                cmd = new SqlCommand("insert into RouteTimeMaintain (Plant_code,Route_id,Date,Session,VehicleSettime,VehicleInttime,MBRT,acidity) values (@Plant_code,@Route_id,@Date,@Session,@VehicleSettime,@VehicleInttime,@MBRT,@acidity)");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@Route_id", Route_id);
                cmd.Parameters.Add("@Date", date);
                cmd.Parameters.Add("@Session", session);
                cmd.Parameters.Add("@VehicleSettime", starttime);
                cmd.Parameters.Add("@VehicleInttime", intime);
                cmd.Parameters.Add("@MBRT", mbrt);
                cmd.Parameters.Add("@acidity", acidity);
                vdm.insert(cmd);
                string msg = "RouteTimeMaintain details successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand(" update RouteTimeMaintain set Plant_code=@Plant_code,Route_id=@Route_id,Date=@Date,Session=@Session,VehicleSettime=@VehicleSettime,VehicleInttime=@VehicleInttime,MBRT=@MBRT where Tid=@sno and Plant_code=@plant_code");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@Route_id", Route_id);
                cmd.Parameters.Add("@Date", date);
                cmd.Parameters.Add("@Session", session);
                cmd.Parameters.Add("@VehicleSettime", starttime);
                cmd.Parameters.Add("@VehicleInttime", intime);
                cmd.Parameters.Add("@MBRT", mbrt);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "RouteTimeMaintain details successfully updated";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }


    public class RoutTimemaintanence
    {
        public string PlantName { get; set; }
        public string RouteName { get; set; }
        public string Plant_Code { get; set; }
        public string Route_id { get; set; }
        public string VehicleSettime { get; set; }
        public string VehicleInttime { get; set; }
        public string MBRT { get; set; }
        public string Date { get; set; }
        public string Session { get; set; }
        public string sno { get; set; }
    }

    private void get_RouteTimeMaintenance_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Route_id = context.Request["Route_id"];
            List<RoutTimemaintanence> RoutTimemaintanencelist = new List<RoutTimemaintanence>();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT  RouteTimeMaintain.Plant_code,RouteTimeMaintain.Tid, RouteTimeMaintain.Route_id, RouteTimeMaintain.VehicleSettime,  RouteTimeMaintain.VehicleInttime, RouteTimeMaintain.MBRT, RouteTimeMaintain.Date, RouteTimeMaintain.Session FROM   RouteTimeMaintain WHERE (Plant_code=@plant_code)  AND (Route_id=@Route_id) and  (Date between @d1 and @d2)");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            cmd.Parameters.Add("@Route_id", Route_id);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                RoutTimemaintanence RoutTimemaintanencedetails = new RoutTimemaintanence();
                //RoutTimemaintanencedetails.PlantName = dr["Plant_Name"].ToString();
                //RoutTimemaintanencedetails.RouteName = dr["Route_Name"].ToString();
                RoutTimemaintanencedetails.Plant_Code = dr["Plant_code"].ToString();
                RoutTimemaintanencedetails.Route_id = dr["Route_id"].ToString();
                RoutTimemaintanencedetails.VehicleSettime = dr["VehicleSettime"].ToString();
                RoutTimemaintanencedetails.VehicleInttime = dr["VehicleInttime"].ToString();
                RoutTimemaintanencedetails.MBRT = dr["MBRT"].ToString();
                RoutTimemaintanencedetails.Date = dr["Date"].ToString();
                RoutTimemaintanencedetails.Session = dr["Session"].ToString();
                RoutTimemaintanencedetails.sno = dr["Tid"].ToString();
                RoutTimemaintanencelist.Add(RoutTimemaintanencedetails);
            }
            string response = GetJson(RoutTimemaintanencelist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }



    private void saveGenReadingDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Date = context.Request["Date"];
            string GenOpReading = context.Request["GenOpReading"];
            string GenClReading = context.Request["GenClReading"];
            string GenTotReading = context.Request["GenTotReading"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            string btnval = context.Request["btnval"];

            if (btnval == "save")
            {
                cmd = new SqlCommand("insert into GensetPowerDiesel (Plant_Code,Date,GenOpReading,GenClReading,GenTotReading) values (@plant_code,@Date,@GenOpReading,@GenClReading,@GenTotReading)");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@Date", Date);
                cmd.Parameters.Add("@GenOpReading", GenOpReading);
                cmd.Parameters.Add("@GenClReading", GenClReading);
                cmd.Parameters.Add("@GenTotReading", GenTotReading);
                //cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                vdm.insert(cmd);
                string msg = "Genarator details successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand(" update GensetPowerDiesel set Plant_Code=@plant_code,Date=@Date,GenOpReading=@GenOpReading,GenClReading=@GenClReading,GenTotReading=@GenTotReading where Tid=@sno and Plant_Code=@plant_code");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@Date", Date);
                cmd.Parameters.Add("@GenOpReading", GenOpReading);
                cmd.Parameters.Add("@GenClReading", GenClReading);
                cmd.Parameters.Add("@GenTotReading", GenTotReading);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "Genarator details successfully updated";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }


    public class GenReadingDetails
    {
        public string GenOpReading { get; set; }
        public string GenClReading { get; set; }
        public string GenTotReading { get; set; }
        public string sno { get; set; }
        public string Date { get; set; }
    }
    private void get_GenReadingDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);

            List<GenReadingDetails> GenReadingDetailslist = new List<GenReadingDetails>();
            cmd = new SqlCommand("SELECT Tid,Date, GenOpReading, GenClReading, GenTotReading FROM GensetPowerDiesel where Plant_Code=@plant_code and Date between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                GenReadingDetails GenReadingdetails = new GenReadingDetails();
                GenReadingdetails.GenOpReading = dr["GenOpReading"].ToString();
                GenReadingdetails.GenClReading = dr["GenClReading"].ToString();
                GenReadingdetails.GenTotReading = dr["GenTotReading"].ToString();
                GenReadingdetails.sno = dr["Tid"].ToString();
                GenReadingdetails.Date = ((DateTime)dr["date"]).ToString("yyyy-MM-dd");//dr["Date"].ToString();
                GenReadingDetailslist.Add(GenReadingdetails);
            }
            string response = GetJson(GenReadingDetailslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void saveDeiselDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string DiselOpLtrs = context.Request["DiselOpLtrs"];
            string DispReceipt = context.Request["DispReceipt"];
            string DispHours = context.Request["DispHours"];
            string DispConsumption = context.Request["DispConsumption"];
            string DispCloLtrs = context.Request["DispCloLtrs"];
            string DiselDate = context.Request["DiselDate"];
            string btnval = context.Request["btnval"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            if (btnval == "save")
            {
                cmd = new SqlCommand("insert into Diesel (Plant_Code,DispOpLtrs,DispReceipt,DispHours,DispConsumption,DispCloLtrs,date,doe) values (@plant_code,@DiselOpLtrs,@DispReceipt,@DispHours,@DispConsumption,@DispCloLtrs,@date,@doe)");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@DiselOpLtrs", DiselOpLtrs);
                cmd.Parameters.Add("@DispReceipt", DispReceipt);
                cmd.Parameters.Add("@DispHours", DispHours);
                cmd.Parameters.Add("@DispConsumption", DispConsumption);
                cmd.Parameters.Add("@DispCloLtrs", DispCloLtrs);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", DiselDate);
                vdm.insert(cmd);
                string msg = "DeiselDetails details successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand("update Diesel set Plant_Code=@plant_code,DispOpLtrs=@DiselOpLtrs,DispReceipt=@DispReceipt,DispHours=@DispHours,DispConsumption=@DispConsumption,DispCloLtrs=@DispCloLtrs,date=@date where Tid=@sno and Plant_Code=@plant_code");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@DiselOpLtrs", DiselOpLtrs);
                cmd.Parameters.Add("@DispReceipt", DispReceipt);
                cmd.Parameters.Add("@DispHours", DispHours);
                cmd.Parameters.Add("@DispConsumption", DispConsumption);
                cmd.Parameters.Add("@DispCloLtrs", DispCloLtrs);
                cmd.Parameters.Add("@date", DiselDate);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "DeiselDetails successfully updated";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }

    

    public class DeiselDetails
    {
        public string DiselOpLtrs { get; set; }
        public string DispReceipt { get; set; }
        public string DispHours { get; set; }
        public string sno { get; set; }
        public string DispCloLtrs { get; set; }
        public string DispConsumption { get; set; }
        public string DiselDate { get; set; }
    }
    private void get_DeiselDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);

            List<DeiselDetails> DeiselDetailslist = new List<DeiselDetails>();
            cmd = new SqlCommand("SELECT Tid,DispReceipt, DispOpLtrs, DispHours, DispCloLtrs,DispConsumption,date FROM Diesel where Plant_Code=@plant_code and doe between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                DeiselDetails getDeiselDetails = new DeiselDetails();
                getDeiselDetails.DiselOpLtrs = dr["DispOpLtrs"].ToString();
                getDeiselDetails.DispReceipt = dr["DispReceipt"].ToString();
                getDeiselDetails.DispHours = dr["DispHours"].ToString();
                getDeiselDetails.sno = dr["Tid"].ToString();
                getDeiselDetails.DispConsumption = dr["DispConsumption"].ToString();
                getDeiselDetails.DispCloLtrs = dr["DispCloLtrs"].ToString();
                getDeiselDetails.DiselDate = ((DateTime)dr["date"]).ToString("yyyy-MM-dd");//dr["date"].ToString();
                DeiselDetailslist.Add(getDeiselDetails);
            }
            string response = GetJson(DeiselDetailslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }




    private void savePowerDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string PowOpUnit = context.Request["PowOpUnit"];
            string PowCloUnit = context.Request["PowCloUnit"];
            string PowConsumpUnit = context.Request["PowConsumpUnit"];
            string PowerDate = context.Request["PowerDate"];
            string btnval = context.Request["btnval"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            if (btnval == "save")
            {
                cmd = new SqlCommand("insert into powerdetails1 (Plant_Code,PowOpUnit,PowCloUnit,PowConsumpUnit,doe,date) values (@plant_code,@PowOpUnit,@PowCloUnit,@PowConsumpUnit,@doe,@date)");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@PowOpUnit", PowOpUnit);
                cmd.Parameters.Add("@PowCloUnit", PowConsumpUnit);
                cmd.Parameters.Add("@PowConsumpUnit", PowCloUnit);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", PowerDate);
                vdm.insert(cmd);
                string msg = "PowerDetails  successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand("update powerdetails1 set Plant_Code=@plant_code,PowOpUnit=@PowOpUnit,PowCloUnit=@PowCloUnit,PowConsumpUnit=@PowConsumpUnit,date=@date where Tid=@sno and Plant_Code=@plant_code");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@PowOpUnit", PowOpUnit);
                cmd.Parameters.Add("@PowCloUnit", PowConsumpUnit);
                cmd.Parameters.Add("@PowConsumpUnit", PowCloUnit);
                cmd.Parameters.Add("@date", PowerDate);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "PowerDetails successfully updated";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }
   
    public class PowerDetails
    {
        public string PowOpUnit { get; set; }
        public string PowCloUnit { get; set; }
        public string PowConsumpUnit { get; set; }
        public string PowerDate { get; set; }
        public string sno { get; set; }
    }
    private void get_PowerDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            List<PowerDetails> PowerDetailslist = new List<PowerDetails>();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);

            cmd = new SqlCommand("SELECT Tid,PowOpUnit, PowCloUnit,  PowConsumpUnit,date FROM powerdetails1 where Plant_Code=@plant_code and doe between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                PowerDetails getPowerDetails = new PowerDetails();
                getPowerDetails.PowOpUnit = dr["PowOpUnit"].ToString();
                getPowerDetails.PowCloUnit = dr["PowCloUnit"].ToString();
                getPowerDetails.PowConsumpUnit = dr["PowConsumpUnit"].ToString();
                getPowerDetails.PowerDate = ((DateTime)dr["date"]).ToString("yyyy-MM-dd");  //dr["date"].ToString();
                getPowerDetails.sno = dr["Tid"].ToString();
                PowerDetailslist.Add(getPowerDetails);
            }
            string response = GetJson(PowerDetailslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }

    public class compressionruntime
    {
        public string Session { get; set; }
        public string StartTime { get; set; }
        public string IBTTemp { get; set; }
        public string EndTime { get; set; }
        public string IBTTemp1 { get; set; }
        public string sno { get; set; }
    }
    public void save_comruntimehours_click(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Session = context.Request["Session"];
            string StartTime = context.Request["StartTime"];
            string IBTTemp = context.Request["IBTTemp"];
            string EndTime = context.Request["EndTime"];
            string IBTTemp1 = context.Request["IBTTemp1"];
            string CompressionDate = context.Request["CompressionDate"];
            //DateTime Date = DateTime.Now;
            string btn_save = context.Request["btnVal"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            if (btn_save == "save")
            {
                cmd = new SqlCommand("insert into cmpruntime (Plant_code,session,starttime,stoptime,ibttempstart,ibttempstop,doe,date) values (@Plant_code,@session,@starttime,@stoptime,@ibttempstart,@ibttempstop,@doe,@date)");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@session", Session);
                cmd.Parameters.Add("@starttime", StartTime);
                cmd.Parameters.Add("@ibttempstart", IBTTemp);
                cmd.Parameters.Add("@stoptime", EndTime);
                cmd.Parameters.Add("@ibttempstop", IBTTemp1);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", CompressionDate);
                vdm.insert(cmd);
                string msg = "Detailes successfully saved";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand("Update cmpruntime set Plant_code=@Plant_code,session=@session,starttime=@starttime,stoptime=@stoptime,ibttempstart=@ibttempstart,ibttempstop=@ibttempstop,doe=@doe,date=@date where sno=@sno and Plant_code=@Plant_code");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@session", Session);
                cmd.Parameters.Add("@starttime", StartTime);
                cmd.Parameters.Add("@ibttempstart", IBTTemp);
                cmd.Parameters.Add("@stoptime", EndTime);
                cmd.Parameters.Add("@ibttempstop", IBTTemp1);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", CompressionDate);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "Details successfully updated";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    public class compressionruntimedetails
    {
        public string Session { get; set; }
        public string StartTime { get; set; }
        public string IBTTemp { get; set; }
        public string EndTime { get; set; }
        public string IBTTemp1 { get; set; }
        public string sno { get; set; }
        public string CompressionDate { get; set; }
        public string plant_code { get; set; }
    }
    private void get_comruntimehours_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Plant_code,sno,session,starttime,stoptime,ibttempstart,ibttempstop,date FROM cmpruntime where (Plant_code=@Plant_code) and (doe between @d1 and @d2)");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@Plant_code", plant_code);
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            cmd.Parameters.Add("@Plant_code", plant_code);
            List<compressionruntimedetails> compressionruntimelist = new List<compressionruntimedetails>();
            foreach (DataRow dr in routes.Rows)
            {
                compressionruntimedetails compressionruntimedetails = new compressionruntimedetails();
                compressionruntimedetails.plant_code = dr["Plant_code"].ToString();
                compressionruntimedetails.Session = dr["session"].ToString();
                compressionruntimedetails.StartTime = dr["starttime"].ToString();
                compressionruntimedetails.IBTTemp = dr["ibttempstart"].ToString();
                compressionruntimedetails.EndTime = dr["stoptime"].ToString();
                compressionruntimedetails.IBTTemp1 = dr["ibttempstop"].ToString();
                compressionruntimedetails.CompressionDate = ((DateTime)dr["date"]).ToString("yyyy-MM-dd"); //dr["date"].ToString();
                compressionruntimedetails.sno = dr["sno"].ToString();
                compressionruntimelist.Add(compressionruntimedetails);
            }
            string response = GetJson(compressionruntimelist);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    public class milkrechilling
    {
        public string Session { get; set; }
        public string ONTime { get; set; }
        public string TotalLiters { get; set; }
        public string OFFTime { get; set; }
        public string sno { get; set; }
    }
    public void savemilkrechilling(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Session = context.Request["Session"];
            string ONTime = context.Request["ONTime"];
            string OFFTime = context.Request["OFFTime"];
            string TotalLiters = context.Request["TotalLiters"];
            //DateTime Date = DateTime.Now;
            string RechillingDate = context.Request["RechillingDate"];
            string btn_save1 = context.Request["btnVal"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            if (btn_save1 == "save")
            {
                cmd = new SqlCommand("insert into milkrechilling (Plant_code,session,ontime,offtime,totalliters,doe,date) values (@Plant_code,@session,@ontime,@offtime,@totalliters,@doe,@date)");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@session", Session);
                cmd.Parameters.Add("@ontime", ONTime);
                cmd.Parameters.Add("@totalliters", TotalLiters);
                cmd.Parameters.Add("@offtime", OFFTime);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", RechillingDate);
                vdm.insert(cmd);
                string msg = "Detailes successfully saved";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand("Update milkrechilling set Plant_code=@Plant_code,session=@session,ontime=@ontime,offtime=@offtime,totalliters=@totalliters,doe=@doe,date=@date where sno=@sno  and Plant_code=@plant_code");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@session", Session);
                cmd.Parameters.Add("@ontime", ONTime);
                cmd.Parameters.Add("@totalliters", TotalLiters);
                cmd.Parameters.Add("@offtime", OFFTime);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", RechillingDate);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "Details successfully updated";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    public class milkrechillingdetails
    {
        public string Session { get; set; }
        public string ONTime { get; set; }
        public string OFFTime { get; set; }
        public string Endtime { get; set; }
        public string TotalLiters { get; set; }
        public string RechillingDate { get; set; }
        public string sno { get; set; }
        public string plant_code { get; set; }

    }
    private void get_milkrechilling_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);

            cmd = new SqlCommand("SELECT sno,Plant_code, session, ontime, offtime, totalliters,date FROM milkrechilling where Plant_code=@Plant_code and doe between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@Plant_code", plant_code);
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<milkrechillingdetails> milkrechillinglist = new List<milkrechillingdetails>();
            foreach (DataRow dr in routes.Rows)
            {
                milkrechillingdetails milkrechillingdetails = new milkrechillingdetails();
                milkrechillingdetails.plant_code = dr["Plant_code"].ToString();
                milkrechillingdetails.Session = dr["session"].ToString();
                milkrechillingdetails.ONTime = dr["ontime"].ToString();
                milkrechillingdetails.OFFTime = dr["offtime"].ToString();
                milkrechillingdetails.plant_code = dr["Plant_code"].ToString();
                milkrechillingdetails.TotalLiters = dr["totalliters"].ToString();
                milkrechillingdetails.RechillingDate = ((DateTime)dr["date"]).ToString("yyyy-MM-dd"); //dr["date"].ToString();
                milkrechillingdetails.sno = dr["sno"].ToString();
                milkrechillinglist.Add(milkrechillingdetails);
            }
            string response = GetJson(milkrechillinglist);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    private void saveChemicalDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string sulphuric = context.Request["sulphuric"];
            string Idophore = context.Request["Idophore"];
            string washingsoda = context.Request["washingsoda"];
            string causticsoda = context.Request["causticsoda"];
            string slurrey = context.Request["slurrey"];
            string nitricacid = context.Request["nitricacid"];
            string ChemicalDate = context.Request["ChemicalDate"];
            string btnval = context.Request["btnval"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            if (btnval == "save")
            {
                cmd = new SqlCommand("insert into chemicalsdetails (Plant_Code,sulphuric,idophore,washingsoda,causticsoda,slurrey,nitricacid,doe,date) values (@Plant_Code,@sulphuric,@idophore,@washingsoda,@causticsoda,@slurrey,@nitricacid,@doe,@date)");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@sulphuric", sulphuric);
                cmd.Parameters.Add("@idophore", Idophore);
                cmd.Parameters.Add("@washingsoda", washingsoda);
                cmd.Parameters.Add("@causticsoda", causticsoda);
                cmd.Parameters.Add("@slurrey", slurrey);
                cmd.Parameters.Add("@nitricacid", nitricacid);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", ChemicalDate);
                vdm.insert(cmd);
                string msg = "PowerDetails  successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                string sno = context.Request["sno"];
                cmd = new SqlCommand("update chemicalsdetails set Plant_Code=@Plant_Code,sulphuric=@sulphuric,idophore=@idophore,washingsoda=@washingsoda,causticsoda=@causticsoda,slurrey=@slurrey,nitricacid=@nitricacid,doe=@doe,date=@date where Tid=@sno and Plant_Code=@plant_code");
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@sulphuric", sulphuric);
                cmd.Parameters.Add("@idophore", Idophore);
                cmd.Parameters.Add("@washingsoda", washingsoda);
                cmd.Parameters.Add("@causticsoda", causticsoda);
                cmd.Parameters.Add("@slurrey", slurrey);
                cmd.Parameters.Add("@nitricacid", nitricacid);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@date", ChemicalDate);
                cmd.Parameters.Add("@sno", sno);
                vdm.Update(cmd);
                string msg = "PowerDetails successfully updated";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch
        {
        }
    }
    public class ChemicalsDetails
    {
        public string plant_code { get; set; }
        public string sulphuric { get; set; }
        public string Idophore { get; set; }
        public string washingsoda { get; set; }
        public string causticsoda { get; set; }
        public string slurrey { get; set; }
        public string nitricacid { get; set; }
        public string sno { get; set; }
        public string ChemicalDate { get; set; }
    }
    private void get_ChemicalDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            List<ChemicalsDetails> ChemicalsDetailslist = new List<ChemicalsDetails>();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Tid,Plant_Code,sulphuric,idophore,washingsoda,causticsoda,slurrey,nitricacid,doe,date FROM chemicalsdetails where Plant_Code=@plant_code and doe between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                ChemicalsDetails getChemicalsDetails = new ChemicalsDetails();
                getChemicalsDetails.plant_code = dr["Plant_Code"].ToString();
                getChemicalsDetails.sulphuric = dr["sulphuric"].ToString();
                getChemicalsDetails.Idophore = dr["idophore"].ToString();
                getChemicalsDetails.washingsoda = dr["washingsoda"].ToString();
                getChemicalsDetails.causticsoda = dr["causticsoda"].ToString();
                getChemicalsDetails.slurrey = dr["slurrey"].ToString();
                getChemicalsDetails.nitricacid = dr["nitricacid"].ToString();
                getChemicalsDetails.ChemicalDate = ((DateTime)dr["date"]).ToString("yyyy-MM-dd");    //dr["date"].ToString();
                getChemicalsDetails.sno = dr["Tid"].ToString();
                ChemicalsDetailslist.Add(getChemicalsDetails);
            }
            string response = GetJson(ChemicalsDetailslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void saveChillingMachinaryDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Genaratorname = context.Request["Genaratorname"];
            string GenaratorCapacity = context.Request["GenaratorCapacity"];
            string GenaratorPerhours = context.Request["GenaratorPerhours"];
            string btnval = context.Request["btnval"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Tid as maxno FROM machinarydetails where Plant_Code=@Plant_Code");
            cmd.Parameters.Add("@Plant_Code", plant_code);
            DataTable dtchilling = vdm.SelectQuery(cmd).Tables[0];
            if (dtchilling.Rows.Count > 0)
            {
                string maxno = dtchilling.Rows[0]["maxno"].ToString();
                cmd = new SqlCommand("update machinarydetails set Plant_Code=@Plant_Code,genaratorname=@genaratorname,genaratorcapacity=@genaratorcapacity,genaratorperhours=@genaratorperhours,doe=@doe where Tid=@sno and Plant_Code=@Plant_Code");
                cmd.Parameters.Add("@Plant_Code", plant_code);
                cmd.Parameters.Add("@genaratorname", Genaratorname);
                cmd.Parameters.Add("@genaratorcapacity", GenaratorCapacity);
                cmd.Parameters.Add("@genaratorperhours", GenaratorPerhours);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@sno", maxno);
                vdm.Update(cmd);
                if (vdm.Update(cmd) == 0)
                {
                    cmd = new SqlCommand("insert into machinarydetails (Plant_Code,genaratorname,genaratorcapacity,genaratorperhours,doe) values (@Plant_Code,@genaratorname,@genaratorcapacity,@genaratorperhours,@doe)");
                    cmd.Parameters.Add("@Plant_Code", plant_code);
                    cmd.Parameters.Add("@genaratorname", Genaratorname);
                    cmd.Parameters.Add("@genaratorcapacity", GenaratorCapacity);
                    cmd.Parameters.Add("@genaratorperhours", GenaratorPerhours);
                    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                    vdm.insert(cmd);
                }
                string msg = "machinary  successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("insert into machinarydetails (Plant_Code,genaratorname,genaratorcapacity,genaratorperhours,doe) values (@Plant_Code,@genaratorname,@genaratorcapacity,@genaratorperhours,@doe)");
                cmd.Parameters.Add("@Plant_Code", plant_code);
                cmd.Parameters.Add("@genaratorname", Genaratorname);
                cmd.Parameters.Add("@genaratorcapacity", GenaratorCapacity);
                cmd.Parameters.Add("@genaratorperhours", GenaratorPerhours);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                vdm.insert(cmd);
            }
            //if (btnval == "save")
            //{
            //    cmd = new SqlCommand("insert into machinary (Plant_Code,genaratorname,genaratorcapacity,genaratorperhours,doe) values (@Plant_Code,@genaratorname,@genaratorcapacity,@genaratorperhours,@doe)");
            //    cmd.Parameters.Add("@Plant_Code", plant_code);
            //    cmd.Parameters.Add("@genaratorname", Genaratorname);
            //    cmd.Parameters.Add("@genaratorcapacity", GenaratorCapacity);
            //    cmd.Parameters.Add("@genaratorperhours", GenaratorPerhours);
            //    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
            //    vdm.insert(cmd);
            //    string msg = "PowerDetails  successfully saved";
            //    string response = GetJson(msg);
            //    context.Response.Write(response);
            //}
            //else
            //{
            //    string sno = context.Request["sno"];
            //    cmd = new SqlCommand("update machinary set Plant_Code=@Plant_Code,genaratorname=@genaratorname,genaratorcapacity=@genaratorcapacity,genaratorperhours=@genaratorperhours,doe=@doe where Tid=@sno and Plant_Code=@Plant_Code");
            //    cmd.Parameters.Add("@Plant_Code", plant_code);
            //    cmd.Parameters.Add("@genaratorname", Genaratorname);
            //    cmd.Parameters.Add("@genaratorcapacity", GenaratorCapacity);
            //    cmd.Parameters.Add("@genaratorperhours", GenaratorPerhours);
            //    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
            //    cmd.Parameters.Add("@sno", sno);
            //    vdm.Update(cmd);
            //    string msg = "PowerDetails successfully updated";
            //    string response = GetJson(msg);
            //    context.Response.Write(response);
            //}
        }
        catch
        {
        }
    }
    public class ChillingMachinary
    {
        public string plant_code { get; set; }
        public string Genaratorname { get; set; }
        public string GenaratorCapacity { get; set; }
        public string GenaratorPerhours { get; set; }
        public string sno { get; set; }
    }
    private void get_ChillingMachinaryDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            List<ChillingMachinary> ChillingMachinarylist = new List<ChillingMachinary>();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Tid,Plant_Code,genaratorname,genaratorcapacity,genaratorperhours,doe FROM machinarydetails where Plant_Code=@plant_code and doe between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                ChillingMachinary getChillingMachinaryDetails = new ChillingMachinary();
                getChillingMachinaryDetails.plant_code = dr["Plant_Code"].ToString();
                getChillingMachinaryDetails.Genaratorname = dr["genaratorname"].ToString();
                getChillingMachinaryDetails.GenaratorCapacity = dr["genaratorcapacity"].ToString();
                getChillingMachinaryDetails.GenaratorPerhours = dr["genaratorperhours"].ToString();
                getChillingMachinaryDetails.sno = dr["Tid"].ToString();
                ChillingMachinarylist.Add(getChillingMachinaryDetails);
            }
            string response = GetJson(ChillingMachinarylist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void saveCompressiormachinaryDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string CompressiorName = context.Request["CompressiorName"];
            string Compressiorcapacity = context.Request["Compressiorcapacity"];
            string CompressiorPerhours = context.Request["CompressiorPerhours"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            string btnval = context.Request["btnval"];
            cmd = new SqlCommand("SELECT Tid as maxno FROM machinarydetails where Plant_Code=@Plant_Code");
            cmd.Parameters.Add("@Plant_Code", plant_code);
            DataTable dtcompressior = vdm.SelectQuery(cmd).Tables[0];
            if (dtcompressior.Rows.Count > 0)
            {
                string maxno = dtcompressior.Rows[0]["maxno"].ToString();
                cmd = new SqlCommand("update machinarydetails set Plant_Code=@Plant_Code,compressiorname=@compressiorname,compressiorcapacity=@compressiorcapacity,compressiorperhours=@compressiorperhours,doe=@doe where Tid=@sno and Plant_Code=@Plant_Code");
                cmd.Parameters.Add("@Plant_Code", plant_code);
                cmd.Parameters.Add("@compressiorname", CompressiorName);
                cmd.Parameters.Add("@compressiorcapacity", Compressiorcapacity);
                cmd.Parameters.Add("@compressiorperhours", CompressiorPerhours);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@sno", maxno);
                vdm.Update(cmd);
                if (vdm.Update(cmd) == 0)
                {
                    cmd = new SqlCommand("insert into machinarydetails (Plant_Code,compressiorname,compressiorcapacity,compressiorperhours,doe) values (@Plant_Code,@compressiorname,@compressiorcapacity,@compressiorperhours,@doe)");
                    cmd.Parameters.Add("@Plant_Code", plant_code);
                    cmd.Parameters.Add("@compressiorname", CompressiorName);
                    cmd.Parameters.Add("@compressiorcapacity", Compressiorcapacity);
                    cmd.Parameters.Add("@compressiorperhours", CompressiorPerhours);
                    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                    vdm.insert(cmd);
                }
                string msg = "machinary  successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("insert into machinarydetails (Plant_Code,compressiorname,compressiorcapacity,compressiorperhours,doe) values (@Plant_Code,@compressiorname,@compressiorcapacity,@compressiorperhours,@doe)");
                cmd.Parameters.Add("@Plant_Code", plant_code);
                cmd.Parameters.Add("@compressiorname", CompressiorName);
                cmd.Parameters.Add("@compressiorcapacity", Compressiorcapacity);
                cmd.Parameters.Add("@compressiorperhours", CompressiorPerhours);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                vdm.insert(cmd);
            }
           
            //if (btnval == "save")
            //{
            //    cmd = new SqlCommand("insert into compressormachinary (Plant_Code,compressiorname,compressiorcapacity,compressiorperhours,doe) values (@Plant_Code,@compressiorname,@compressiorcapacity,@compressiorperhours,@doe)");
            //    cmd.Parameters.Add("@Plant_Code", plant_code);
            //    cmd.Parameters.Add("@compressiorname", CompressiorName);
            //    cmd.Parameters.Add("@compressiorcapacity", Compressiorcapacity);
            //    cmd.Parameters.Add("@compressiorperhours", CompressiorPerhours);
            //    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
            //    vdm.insert(cmd);
            //    string msg = "PowerDetails  successfully saved";
            //    string response = GetJson(msg);
            //    context.Response.Write(response);
            //}
            //else
            //{
            //    string sno = context.Request["sno"];
            //    cmd = new SqlCommand("update compressormachinary set Plant_Code=@Plant_Code,compressiorname=@compressiorname,compressiorcapacity=@compressiorcapacity,compressiorperhours=@compressiorperhours,doe=@doe where Tid=@sno and Plant_Code=@Plant_Code");
            //    cmd.Parameters.Add("@Plant_Code", plant_code);
            //    cmd.Parameters.Add("@compressiorname", CompressiorName);
            //    cmd.Parameters.Add("@compressiorcapacity", Compressiorcapacity);
            //    cmd.Parameters.Add("@compressiorperhours", CompressiorPerhours);
            //    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
            //    cmd.Parameters.Add("@sno", sno);
            //    vdm.Update(cmd);
            //    string msg = "PowerDetails successfully updated";
            //    string response = GetJson(msg);
            //    context.Response.Write(response);
            //}
        }
        catch
        {
        }
    }
    public class CompressiorMachinary
    {
        public string plant_code { get; set; }
        public string CompressiorName { get; set; }
        public string Compressiorcapacity { get; set; }
        public string CompressiorPerhours { get; set; }
        public string sno { get; set; }
    }
    private void get_CompressiorMachinaryDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            List<CompressiorMachinary> CompressiorMachinarylist = new List<CompressiorMachinary>();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Tid,Plant_Code,compressiorname,compressiorcapacity,compressiorperhours,doe FROM machinarydetails where Plant_Code=@plant_code and doe between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                CompressiorMachinary getCompressiorMachinaryDetails = new CompressiorMachinary();
                getCompressiorMachinaryDetails.plant_code = dr["Plant_Code"].ToString();
                getCompressiorMachinaryDetails.CompressiorName = dr["compressiorname"].ToString();
                getCompressiorMachinaryDetails.Compressiorcapacity = dr["compressiorcapacity"].ToString();
                getCompressiorMachinaryDetails.CompressiorPerhours = dr["compressiorperhours"].ToString();
                getCompressiorMachinaryDetails.sno = dr["Tid"].ToString();
                CompressiorMachinarylist.Add(getCompressiorMachinaryDetails);
            }
            string response = GetJson(CompressiorMachinarylist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }



    private void saveSiloDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string Silo1model = context.Request["Silo1model"];
            string Silo1capacity = context.Request["Silo1capacity"];
            string Silo2model = context.Request["Silo2model"];
            string Silo2capacity = context.Request["Silo2capacity"];
            string Silo3model = context.Request["Silo3model"];
            string Silo3capacity = context.Request["Silo3capacity"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            string btnval = context.Request["btnval"];
            cmd = new SqlCommand("SELECT Tid as maxno FROM machinarydetails where Plant_Code=@Plant_Code");
            cmd.Parameters.Add("@Plant_Code", plant_code);
            DataTable dtcompressior = vdm.SelectQuery(cmd).Tables[0];
            if (dtcompressior.Rows.Count > 0)
            {
                string maxno = dtcompressior.Rows[0]["maxno"].ToString();
                cmd = new SqlCommand("update machinarydetails set Plant_Code=@Plant_Code,silo1model=@silo1model,silo1capacity=@silo1capacity,silo2model=@silo2model,silo2capacity=@silo2capacity,silo3model=@silo3model,silo3capacity=@silo3capacity where Tid=@sno and Plant_Code=@Plant_Code");
                cmd.Parameters.Add("@Plant_Code", plant_code);
                cmd.Parameters.Add("@silo1model", Silo1model);
                cmd.Parameters.Add("@silo1capacity", Silo1capacity);
                cmd.Parameters.Add("@silo2model", Silo2model);
                cmd.Parameters.Add("@silo2capacity", Silo2capacity);
                cmd.Parameters.Add("@silo3model", Silo3model);
                cmd.Parameters.Add("@silo3capacity", Silo3capacity);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                cmd.Parameters.Add("@sno", maxno);
                vdm.Update(cmd);
                if (vdm.Update(cmd) == 0)
                {
                    cmd = new SqlCommand("insert into machinarydetails (Plant_Code,silo1model,silo1capacity,silo2model,silo2capacity,silo3model,silo3capacity,doe) values (@Plant_Code,@silo1model,@silo1capacity,@silo2model,@silo2capacity,@silo3model,@silo3capacity,@doe)");
                    cmd.Parameters.Add("@Plant_Code", plant_code);
                    cmd.Parameters.Add("@silo1model", Silo1model);
                    cmd.Parameters.Add("@silo1capacity", Silo1capacity);
                    cmd.Parameters.Add("@silo2model", Silo2model);
                    cmd.Parameters.Add("@silo2capacity", Silo2capacity);
                    cmd.Parameters.Add("@silo3model", Silo3model);
                    cmd.Parameters.Add("@silo3capacity", Silo3capacity);
                    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                    vdm.insert(cmd);
                }
                string msg = "machinary  successfully saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("insert into machinarydetails (Plant_Code,silo1model,silo1capacity,silo2model,silo2capacity,silo3model,silo3capacity,doe) values (@Plant_Code,@silo1model,@silo1capacity,@silo2model,@silo2capacity,@silo3model,@silo3capacity,@doe)");
                cmd.Parameters.Add("@Plant_Code", plant_code);
                cmd.Parameters.Add("@silo1model", Silo1model);
                cmd.Parameters.Add("@silo1capacity", Silo1capacity);
                cmd.Parameters.Add("@silo2model", Silo2model);
                cmd.Parameters.Add("@silo2capacity", Silo2capacity);
                cmd.Parameters.Add("@silo3model", Silo3model);
                cmd.Parameters.Add("@silo3capacity", Silo3capacity);
                cmd.Parameters.Add("@doe", ServerDateCurrentdate);
                vdm.insert(cmd);
            }
            //if (btnval == "save")
            //{
            //    cmd = new SqlCommand("insert into compressormachinary (Plant_Code,compressiorname,compressiorcapacity,compressiorperhours,doe) values (@Plant_Code,@compressiorname,@compressiorcapacity,@compressiorperhours,@doe)");
            //    cmd.Parameters.Add("@Plant_Code", plant_code);
            //    cmd.Parameters.Add("@compressiorname", CompressiorName);
            //    cmd.Parameters.Add("@compressiorcapacity", Compressiorcapacity);
            //    cmd.Parameters.Add("@compressiorperhours", CompressiorPerhours);
            //    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
            //    vdm.insert(cmd);
            //    string msg = "PowerDetails  successfully saved";
            //    string response = GetJson(msg);
            //    context.Response.Write(response);
            //}
            //else
            //{
            //    string sno = context.Request["sno"];
            //    cmd = new SqlCommand("update compressormachinary set Plant_Code=@Plant_Code,compressiorname=@compressiorname,compressiorcapacity=@compressiorcapacity,compressiorperhours=@compressiorperhours,doe=@doe where Tid=@sno and Plant_Code=@Plant_Code");
            //    cmd.Parameters.Add("@Plant_Code", plant_code);
            //    cmd.Parameters.Add("@compressiorname", CompressiorName);
            //    cmd.Parameters.Add("@compressiorcapacity", Compressiorcapacity);
            //    cmd.Parameters.Add("@compressiorperhours", CompressiorPerhours);
            //    cmd.Parameters.Add("@doe", ServerDateCurrentdate);
            //    cmd.Parameters.Add("@sno", sno);
            //    vdm.Update(cmd);
            //    string msg = "PowerDetails successfully updated";
            //    string response = GetJson(msg);
            //    context.Response.Write(response);
            //}
        }
        catch
        {
        }
    }
    public class SiloDetails
    {
        public string plant_code { get; set; }
        public string Silo1model { get; set; }
        public string Silo1capacity { get; set; }
        public string Silo2model { get; set; }
        public string Silo2capacity { get; set; }
        public string Silo3model { get; set; }
        public string Silo3capacity { get; set; }
        public string sno { get; set; }
    }
    private void get_SiloDetails(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            List<SiloDetails> getSiloDetailslist = new List<SiloDetails>();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Tid,Plant_Code,silo1model,silo1capacity,silo2model,silo2capacity,silo3model,silo3capacity,doe FROM machinarydetails where Plant_Code=@plant_code and doe between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtrote.Rows)
            {
                SiloDetails getSiloDetails = new SiloDetails();
                getSiloDetails.plant_code = dr["Plant_Code"].ToString();
                getSiloDetails.Silo1model = dr["silo1model"].ToString();
                getSiloDetails.Silo1capacity = dr["silo1capacity"].ToString();
                getSiloDetails.Silo2model = dr["silo2model"].ToString();
                getSiloDetails.Silo2capacity = dr["silo2capacity"].ToString();
                getSiloDetails.Silo3model = dr["silo3model"].ToString();
                getSiloDetails.Silo3capacity = dr["silo3capacity"].ToString();
                getSiloDetails.sno = dr["Tid"].ToString();
                getSiloDetailslist.Add(getSiloDetails);
            }
            string response = GetJson(getSiloDetailslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }




    //private void save_approvalsms_click(HttpContext context)
    //{

    //    vdm = new SalesDBManager();
    //    string BranchID = context.Session["Branch_ID"].ToString();
    //    string UserID = context.Session["Employ_Sno"].ToString();
    //    var js = new JavaScriptSerializer();
    //    var title1 = context.Request.Params[1];
    //    WebClient client = new WebClient();
    //    MilkDetails obj = js.Deserialize<MilkDetails>(title1);
    //    string txtdcno = obj.dcno;
    //    string txtInwardno = obj.inwardno;
    //    string txtvehicleNo = obj.vehicleno;
    //    string txtdate = obj.dispdate;
    //    string cmbstatus = "Kgs";
    //    string txtqco = obj.qco;
    //    string txtRemarks = obj.remarks;
    //    string txtChemist = obj.chemist;
    //    string VendorID = obj.sourceid;
    //    string sno = obj.sno;
    //    string btnvalue = obj.btnvalue;
    //    string pdcno = obj.pdcno;

    //    string DeptID = context.Session["DeptID"].ToString();
    //    DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
    //    string strdate = obj.dispdate;

    //    DateTime dispdate = Convert.ToDateTime(strdate);
    //    string CCname = obj.plantname + "\r\n";
    //    string dcno = obj.pdcno + "\r\n";
    //    string vehicleno = obj.vehicleno + "\r\n";
    //    string Cell1 = "";
    //    string Cell2 = "";
    //    string Cell3 = "";
    //    int i = 1;


    //    //if (btnvalue == "SendSms")
    //    //{
    //    foreach (MilkfatDetails o in obj.MilkfatDetailsMilkarray)
    //    {
    //        if (o.Qtyltr == null || o.Qtyltr == "")
    //        {
    //        }
    //        else
    //        {
    //            //            double txtqtyltrs = 0;
    //            //            double.TryParse(o.Qtyltr, out txtqtyltrs);
    //            //            double txtqtykgs = 0;
    //            //            double.TryParse(o.Qtykg, out txtqtykgs);
    //            //            double txtsnf = 0;
    //            //            double.TryParse(o.snf, out txtsnf);
    //            //            double txtfat = 0;
    //            //            double.TryParse(o.fat, out txtfat);
    //            //            double txtclr = 0;
    //            //            double.TryParse(o.clr, out txtclr);
    //            //            double txths = 0;
    //            //            double.TryParse(o.hs, out txths);
    //            //            double txtAlcohol = 0;
    //            //            double.TryParse(o.alcohol, out txtAlcohol);
    //            //            double txttemp = 0;
    //            //            double.TryParse(o.temp, out txttemp);
    //            //            string milk_type = o.milktype;
    //            //            string strdispdate = obj.dispdate;
    //            //            DateTime dtdispdate = Convert.ToDateTime(strdispdate);
    //            //            cmd = new SqlCommand("update milktransactions set transid=@transid, dcno=@dcno, transtype=@transtype,sectionid=@sectionid,  qty_ltr=@qty_ltr, qty_kgs=@qty_kgs, percentageon=@percentageon, snf=@snf, fat=@fat, clr=@clr, cob1=@cob, hs=@hs, phosps1=@phosps, alcohol=@alcohol, remarks=@remarks, chemist=@chemist, qco=@qco, inwardno=@inwardno, vehicleno=@vehicleno, temp=@temp, doe=@doe, branchid=@branchid, operatedby=@operatedby,cellno=@cellno,milktype=@milktype,mbrt=@mbrt,acidity=@acidity where sno=@sno");
    //            //            cmd.Parameters.Add("@sno", sno);
    //            //            cmd.Parameters.Add("@transid", DeptID);
    //            //            cmd.Parameters.Add("@dcno", txtdcno);
    //            //            cmd.Parameters.Add("@transtype", "in");
    //            //            cmd.Parameters.Add("@sectionid", VendorID);
    //            //            cmd.Parameters.Add("@qty_ltr", txtqtyltrs);
    //            //            cmd.Parameters.Add("@qty_kgs", txtqtykgs);
    //            //            cmd.Parameters.Add("@percentageon", cmbstatus);
    //            //            cmd.Parameters.Add("@snf", txtsnf);
    //            //            cmd.Parameters.Add("@fat", txtfat);
    //            //            cmd.Parameters.Add("@clr", txtclr);
    //            //            cmd.Parameters.Add("@cob", o.cob);
    //            //            cmd.Parameters.Add("@hs", txths);
    //            //            cmd.Parameters.Add("@phosps", o.phosps);
    //            //            cmd.Parameters.Add("@alcohol", txtAlcohol);
    //            //            cmd.Parameters.Add("@remarks", txtRemarks);
    //            //            cmd.Parameters.Add("@chemist", txtChemist);
    //            //            cmd.Parameters.Add("@qco", txtqco);
    //            //            cmd.Parameters.Add("@inwardno", txtInwardno);
    //            //            cmd.Parameters.Add("@vehicleno", txtvehicleNo);
    //            //            cmd.Parameters.Add("@temp", txttemp);
    //            //            cmd.Parameters.Add("@doe", dtdispdate);
    //            //            cmd.Parameters.Add("@branchid", BranchID);
    //            //            cmd.Parameters.Add("@operatedby", UserID);
    //            //            cmd.Parameters.Add("@cellno", o.CellName);
    //            //            cmd.Parameters.Add("@milktype", milk_type);
    //            //            cmd.Parameters.Add("@mbrt", o.mbrt);
    //            //            cmd.Parameters.Add("@acidity", o.acidity);
    //            //            //long TransNo = vdm.insertScalar(cmd);
    //            //            vdm.Update(cmd);
    //            //            //cmd = new SqlCommand("Select  MAX(sno) as TransNo from milktransactions");
    //            //            //DataTable dtVendor = vdm.SelectQuery(cmd).Tables[0];
    //            //            //string TransNo = dtVendor.Rows[0]["TransNo"].ToString();
    //            //            cmd = new SqlCommand("SELECT vendor_subtable.milktype, vendor_subtable.rate_on, vendor_subtable.calc_on, vendor_subtable.cost, vendor_subtable.overheadon,vendor_subtable.overheadcost, vendor_subtable.m_std_snf,vendor_subtable.p_std_snf, vendor_subtable.snfplus_on, vendor_subtable.m_snfpluscost,vendor_subtable.p_snfpluscost, vendor_subtable.transport_on, vendor_subtable.transport, vendor_subtable.transportcost FROM  vendors INNER JOIN vendor_subtable ON vendors.sno = vendor_subtable.vendor_refno WHERE (vendors.sno = @VendorSno) and (vendor_subtable.milktype=@MilkType)");
    //            //            cmd.Parameters.Add("@MilkType", milk_type);
    //            //            cmd.Parameters.Add("@VendorSno", VendorID);
    //            //            DataTable dtVendorlogs = vdm.SelectQuery(cmd).Tables[0];
    //            //            if (dtVendorlogs.Rows.Count > 0)
    //            //            {
    //            //                string rateon = dtVendorlogs.Rows[0]["rate_on"].ToString();
    //            //                string calcon = dtVendorlogs.Rows[0]["calc_on"].ToString();
    //            //                double cost = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["cost"].ToString(), out cost);
    //            //                string overheadon = dtVendorlogs.Rows[0]["overheadon"].ToString();
    //            //                double overheadcost = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["overheadcost"].ToString(), out overheadcost);
    //            //                double m_std_snf = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["m_std_snf"].ToString(), out m_std_snf);
    //            //                double p_std_snf = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["p_std_snf"].ToString(), out p_std_snf);
    //            //                string snfpluson = dtVendorlogs.Rows[0]["snfplus_on"].ToString();
    //            //                double m_snfpluscost = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["m_snfpluscost"].ToString(), out m_snfpluscost);
    //            //                double p_snfpluscost = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["p_snfpluscost"].ToString(), out p_snfpluscost);
    //            //                string transporton = dtVendorlogs.Rows[0]["transport_on"].ToString();
    //            //                double KmCost = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["transportcost"].ToString(), out KmCost);
    //            //                double transport = 0;
    //            //                double.TryParse(dtVendorlogs.Rows[0]["transport"].ToString(), out transport);
    //            //                cmd = new SqlCommand("update milktransaction_logs set milktransaction_sno=@milktransaction_sno, milktype=@milktype, rate_on=@rate_on, calc_on=@calc_on, cost=@cost, overheadon=@overheadon, overheadcost=@overheadcost, m_std_snf=@m_std_snf,p_std_snf=@p_std_snf, snfplus_on=@snfplus_on, m_snfpluscost=@m_snfpluscost,p_snfpluscost=@p_snfpluscost, transport_on=@transport_on, transportcost=@transportcost, transport=@transport where milktransaction_sno=@milktransaction_sno");
    //            //                cmd.Parameters.Add("@milktransaction_sno", sno);
    //            //                cmd.Parameters.Add("@milktype", milk_type);
    //            //                cmd.Parameters.Add("@rate_on", rateon);
    //            //                cmd.Parameters.Add("@calc_on", calcon);
    //            //                cmd.Parameters.Add("@cost", cost);
    //            //                cmd.Parameters.Add("@overheadon", overheadon);
    //            //                cmd.Parameters.Add("@overheadcost", overheadcost);
    //            //                cmd.Parameters.Add("@m_std_snf", m_std_snf);
    //            //                cmd.Parameters.Add("@p_std_snf", p_std_snf);
    //            //                cmd.Parameters.Add("@snfplus_on", snfpluson);
    //            //                cmd.Parameters.Add("@m_snfpluscost", m_snfpluscost);
    //            //                cmd.Parameters.Add("@p_snfpluscost", p_snfpluscost);
    //            //                cmd.Parameters.Add("@transport_on", transporton);
    //            //                cmd.Parameters.Add("@transportcost", KmCost);
    //            //                cmd.Parameters.Add("@transport", transport);
    //            //                vdm.Update(cmd);
    //            //            }
    //            //            double tfat = 0;
    //            //            double tsnf = 0;
    //            double tfat = 0;
    //            double tsnf = 0;
    //            string cell = o.CellName + "\r\n";
    //            string Qtyltr = o.Qtyltr + "\r\n";
    //            string Qtykg = o.Qtykg + "\r\n";
    //            string fat = o.fat + "\r\n";
    //            string snf = o.snf + "\r\n";
    //            string temp = o.temp + "\r\n";
    //            string acidity = o.acidity + "\r\n";
    //            string clr = o.clr + "\r\n";
    //            if (i == 1)
    //            {
    //                if (Qtykg == "\r\n")
    //                {
    //                }
    //                else
    //                {
    //                    Cell1 = "Cell ->" + cell + "Qtykg ->" + Qtykg + "Qtyltr ->" + Qtyltr + "Fat ->" + fat + "snf ->" + snf + "temp ->" + temp + "acidity ->" + acidity + "clr ->" + clr + "\r\n";
    //                }
    //            }
    //            if (i == 2)
    //            {
    //                if (Qtykg == "\r\n")
    //                {
    //                }
    //                else
    //                {
    //                    Cell2 = "Cell ->" + cell + "Qtykg ->" + Qtykg + "Qtyltr ->" + Qtyltr + "Fat ->" + fat + "snf ->" + snf + "temp ->" + temp + "acidity ->" + acidity + "clr ->" + clr + "\r\n";
    //                }
    //            }
    //            if (i == 3)
    //            {
    //                if (Qtykg == "\r\n")
    //                {
    //                }
    //                else
    //                {
    //                    Cell3 = "Cell ->" + cell + "Qtykg ->" + Qtykg + "Qtyltr ->" + Qtyltr + "Fat ->" + fat + "snf ->" + snf + "temp ->" + temp + "acidity ->" + acidity + "clr ->" + clr + "\r\n";
    //                }
    //            }
    //            i++;
    //        }
    //    }
    //    string despatchentrytime = "";
    //    string Inwardtime = "";
    //    cmd = new SqlCommand("update milktransactions set status='Approved' where partydcno=@pdcno");
    //    cmd.Parameters.Add("@pdcno", Convert.ToInt32(dcno));
    //    vdm.Update(cmd);
    //    cmd = new SqlCommand("SELECT MAX(sno) as TransNo FROM despatch_entry where cc_id=@sectionid");
    //    cmd.Parameters.Add("@sectionid", VendorID);
    //    DataTable dtVendor = vdm.SelectQuery(cmd).Tables[0];
    //    string Transsno = "";
    //    if (dtVendor.Rows.Count > 0)
    //    {
    //        Transsno = dtVendor.Rows[0]["TransNo"].ToString();
    //    }
    //    cmd = new SqlCommand("SELECT doe,desp_time FROM despatch_entry where sno = @sno");
    //    cmd.Parameters.Add("@sno", Transsno);
    //    DataTable dtVendorlogs = vdm.SelectQuery(cmd).Tables[0];
    //    if (dtVendorlogs.Rows.Count > 0)
    //    {
    //        despatchentrytime = dtVendorlogs.Rows[0]["doe"].ToString();
    //        Inwardtime = dtVendorlogs.Rows[0]["desp_time"].ToString();
    //    }
    //    cmd = new SqlCommand("SELECT sno, vendorcode, vendorname, vendortype, email, mobno, panno, doe, branchid, address FROM vendors WHERE  (sno = @VendorID)");
    //    cmd.Parameters.Add("@VendorID", obj.sourceid);
    //    DataTable dtvendorinfo = vdm.SelectQuery(cmd).Tables[0];
    //    if (dtvendorinfo.Rows.Count > 0)
    //    {
    //        string MobNo = dtvendorinfo.Rows[0]["mobno"].ToString();
    //        //string MobNo = "9092691491";
    //        string vendorname = dtvendorinfo.Rows[0]["vendorname"].ToString();
    //        if (MobNo.Length == 10)
    //        {
    //            string rdate = "DC Raised date->" + despatchentrytime + "\r\n";
    //            string details = "CC Name->" + vendorname + "DCNo ->" + dcno + "\r\n";
    //            string vehicle = "Vehcile ->" + vehicleno + "Tanker Received Time At Plant ->" + Inwardtime + "\r\n";
    //            string ccdet = rdate + details + vehicle;
    //            string totaldetails = ccdet + Cell1 + Cell2 + Cell3;
    //            string baseurl = "" + MobNo + "&message=%20" + totaldetails + ";&response=N";
    //            Stream data = client.OpenRead(baseurl);
    //            StreamReader reader = new StreamReader(data);
    //            string ResponseID = reader.ReadToEnd();
    //            data.Close();
    //            reader.Close();
    //        }
    //        cmd = new SqlCommand("SELECT sno, name, mobno, email FROM personalinfo WHERE (msgtype = 'Inward') AND (branchid = @BranchID)");
    //        cmd.Parameters.Add("@BranchID", BranchID);
    //        DataTable dtMobno = vdm.SelectQuery(cmd).Tables[0];
    //        foreach (DataRow dr in dtMobno.Rows)
    //        {
    //            string PhoneNo = dr["mobno"].ToString();
    //            if (PhoneNo.Length == 10)
    //            {
    //                string rdate = "DC Raised date->" + despatchentrytime + "\r\n";
    //                string details = "CC Name->" + vendorname + "DCNo ->" + dcno + "\r\n";
    //                string vehicle = "Vehcile ->" + vehicleno + "Tanker Received Time At Plant ->" + Inwardtime + "\r\n";
    //                string ccdet = rdate + details + vehicle;
    //                string totaldetails = ccdet + Cell1 + Cell2 + Cell3;
    //                string baseurl = "" + MobNo + "&message=%20" + totaldetails + ";&response=N";
    //                Stream data = client.OpenRead(baseurl);
    //                StreamReader reader = new StreamReader(data);
    //                string ResponseID = reader.ReadToEnd();
    //                data.Close();
    //                reader.Close();
    //            }
    //        }
    //    }
    //    string msg = "Approval SMS sended successfully";
    //    string response = GetJson(msg);
    //    context.Response.Write(response);
    //}



    public class RouteSMSDetails
    {
        public string PlantCode { get; set; }
        public string PlantName { get; set; }
        public string RouteName { get; set; }
        public string Fat { get; set; }
        public string Snf { get; set; }
        public string Diffavgsnf { get; set; }
        public string Diffavgfat { get; set; }
        public string DifferenceMilkg { get; set; }
        public string MilkKg { get; set; }
        public string Route_ID { get; set; }


    }
    private void get_Sms_Data_RouteWise(HttpContext context)
    {
        try
        {
           

            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string date = context.Request["Date"];
            DateTime dtDOE = Convert.ToDateTime(date);
            string dtdate1 = dtDOE.AddDays(-1).ToString();
            List<RouteSMSDetails> RouteSMSDetailslist = new List<RouteSMSDetails>();
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT  alltable.SupervisorName, alltable.routeid, alltable.RouteName, pro.PlantCode FROM (SELECT DISTINCT RouteId AS R, PlantCode FROM  Lp WHERE (PlantCode = @plant_code) AND (Prdate = @d1)) AS pro LEFT OUTER JOIN (SELECT aa.Supervisor_Code, bb.SupervisorName, aa.routeid, CONVERT(nvarchar(50), aa.routeid) + '_' + aa.Route_Name AS RouteName FROM (SELECT A.Supervisor_Code, B.Route_Name, B.routeid  FROM (SELECT TId, Company_Code, Supervisor_Code, Route_Id FROM  Supervisor_RouteAllotment WHERE (Plant_Code = @plant_code)) AS A LEFT OUTER JOIN (SELECT Route_Name, Route_ID AS routeid FROM  Route_Master  WHERE (Plant_Code = @plant_code)) AS B ON A.Route_Id = B.routeid) AS aa LEFT OUTER JOIN (SELECT Supervisor_Code, SupervisorName, Company_Code, Plant_Code, Added_Date, Dob, Address, Mobile, Bank_name, IfscCode,  AccountNumber, Pannumber, Qualification, Description FROM Supervisor_Details WHERE (Plant_Code = @plant_code)) AS bb ON aa.Supervisor_Code = bb.Supervisor_Code) AS alltable ON pro.R = alltable.routeid WHERE (alltable.routeid > 0) ORDER BY alltable.SupervisorName");
            cmd.Parameters.Add("@d1", date);
            cmd.Parameters.Add("@plant_code", plant_code);
            DataTable dtroute = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow drr in dtroute.Rows)
            {
                int routeid = 0;
                string Routeid = drr["routeid"].ToString();
                int.TryParse(Routeid, out routeid);
                cmd = new SqlCommand("SELECT HH.PlantCode,HH.Route_ID, PM.Plant_Name, HH.Route_Name, HH.Fat1, HH.Fat, HH.Snf, HH.Snf1, HH.MilkKg, HH.MilkKg1 FROM  (SELECT sms2.PlantCode, sms2.MilkKg, sms2.Fat, sms2.Snf, sms2.MilkKg1, sms2.Fat1, sms2.Snf1, sms3.Route_ID, sms3.Plant_Code, sms3.Route_Name  FROM            (SELECT        sms.PlantCode, sms.MilkKg, sms.Fat, sms.Snf, sms1.MilkKg AS MilkKg1, sms1.Fat AS Fat1, sms1.Snf AS Snf1 FROM   (SELECT PlantCode, Prdate, SUM(Milkkg) AS MilkKg, AVG(Fat) AS Fat, AVG(Snf) AS Snf  FROM Lp WHERE (PlantCode = @plant_code) AND (Prdate = @d1) AND (RouteId = @RouteId)  GROUP BY Prdate, PlantCode) AS sms LEFT OUTER JOIN (SELECT PlantCode, Prdate, SUM(Milkkg) AS MilkKg, AVG(Fat) AS Fat, AVG(Snf) AS Snf FROM  Lp AS Lp_1  WHERE  (PlantCode = @plant_code) AND (Prdate = @d2) AND (RouteId = @RouteId)  GROUP BY Prdate, PlantCode) AS sms1 ON sms.PlantCode = sms1.PlantCode) AS sms2 LEFT OUTER JOIN  (SELECT Route_ID, Plant_Code, Route_Name FROM Route_Master   WHERE  (Plant_Code = @plant_code) AND (Route_ID = @RouteId) GROUP BY Route_ID, Plant_Code, Route_Name) AS sms3 ON sms3.Plant_Code = sms2.PlantCode) AS HH LEFT OUTER JOIN (SELECT Plant_Code, Plant_Name FROM Plant_Master GROUP BY Plant_Code, Plant_Name) AS PM ON HH.Plant_Code = PM.Plant_Code");
                cmd.Parameters.Add("@d1", date);
                cmd.Parameters.Add("@RouteId", Routeid);
                cmd.Parameters.Add("@plant_code", plant_code);
                cmd.Parameters.Add("@d2", dtdate1);
                DataTable dtrote = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in dtrote.Rows)
                {
                    RouteSMSDetails getRouteSMSDetails = new RouteSMSDetails();
                    getRouteSMSDetails.PlantCode = dr["PlantCode"].ToString();
                    getRouteSMSDetails.PlantName = dr["Plant_Name"].ToString();
                    getRouteSMSDetails.RouteName = dr["Route_Name"].ToString();
                    getRouteSMSDetails.Route_ID = dr["Route_ID"].ToString();
                    getRouteSMSDetails.Fat = dr["Fat"].ToString();



                    //getRouteSMSDetails.MilkKg = dr["MilkKg"].ToString();
                    double MilkKg=0;
                    string MilkKgg = dr["MilkKg"].ToString();
                    double.TryParse(MilkKgg, out MilkKg);

                     double fat=0;
                    string Fatt = dr["Fat"].ToString();
                    double.TryParse(Fatt, out fat);

                     double snf=0;
                    string Snff = dr["Snf"].ToString();
                    double.TryParse(Snff, out snf);

                      double snf1 = Math.Round(snf, 2);
                      double fat1 = Math.Round(fat, 2);
                      double milkKg = Math.Round(MilkKg, 2);

                     getRouteSMSDetails.Snf = snf1.ToString();
                     getRouteSMSDetails.Fat = fat1.ToString();
                     getRouteSMSDetails.MilkKg = milkKg.ToString();


                    //getChillingMachinaryDetails.sno = dr["Fat1"].ToString();
                    //getRouteSMSDetails.Snff = dr["Snf"].ToString();
                   // getChillingMachinaryDetails.GenaratorCapacity = dr["Snf1"].ToString();
                    double MILKKG = 0; double MILKKG1 = 0; double diffmilkkg = 0; double FAT = 0; double FAT1 = 0;
                    double SNF = 0; double SNF1 = 0; double diffavgfat = 0; double diffavgsnf = 0;
                    //string milkkg = dr["MilkKg"].ToString();

                    double.TryParse(dr["MilkKg"].ToString(), out MILKKG);
                    //string milkkg1 = dr["MilkKg1"].ToString();
                    double.TryParse(dr["MilkKg1"].ToString(), out MILKKG1);
                   // string snf =dr["Snf"].ToString();
                   // string snf1 = dr["Snf1"].ToString();
                    double.TryParse(dr["Snf"].ToString(), out SNF);
                    double.TryParse(dr["Snf1"].ToString(), out SNF1);
                    //string fat = dr["Fat"].ToString();
                    //string fat1 = dr["Fat1"].ToString();
                    double.TryParse(dr["Fat"].ToString(), out FAT);
                    double.TryParse(dr["Fat1"].ToString(), out FAT1);

                    diffavgsnf=SNF-SNF1;
                    double diffavgsnf1 = Math.Round(diffavgsnf, 2);
                   // diffavgsnf = Math.Round(diffavgsnf, 2) - Math.Round(SNF1, 2);

                    diffavgfat = FAT - FAT1;
                    double diffavgfat1 = Math.Round(diffavgfat, 2);

                    diffmilkkg = MILKKG - MILKKG1;
                    double diffmilkkg1 = Math.Round(diffmilkkg, 2);

                    getRouteSMSDetails.Diffavgsnf = diffavgsnf1.ToString();
                    getRouteSMSDetails.Diffavgfat = diffavgfat1.ToString();
                    getRouteSMSDetails.DifferenceMilkg = diffmilkkg1.ToString();
                    RouteSMSDetailslist.Add(getRouteSMSDetails);
                    context.Session["dtroutesms"] = RouteSMSDetailslist;
                }
            }
            string response = GetJson(RouteSMSDetailslist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }


    private void Send_Sms_Data_RouteWise(HttpContext context)
    {
        try
        {

            SalesDBManager vdm = new SalesDBManager();
            List<RouteSMSDetails> lstdetail = (List<RouteSMSDetails>)context.Session["dtroutesms"];
            foreach (RouteSMSDetails rsd in lstdetail)
            {

                // RouteSMSDetails OBJ1 = new RouteSMSDetails();

                string Difffat = rsd.Diffavgfat;
                string Diffsnf = rsd.Diffavgsnf;
                string DiffMilkg = rsd.DifferenceMilkg;
                string MilkKg = rsd.MilkKg;
                string Snf = rsd.Snf;
                string Fat = rsd.Fat;
                string PlantCode = rsd.PlantCode;
                string planname = rsd.PlantName;
                string routeid = rsd.Route_ID;
                string routename = rsd.RouteName;
                //string BranchID = context.Session["Branch_ID"].ToString();
                //string UserID = context.Session["Employ_Sno"].ToString();
                WebClient client = new WebClient();
                //string DeptID = context.Session["DeptID"].ToString();
                DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
                cmd = new SqlCommand("SELECT Tid, Mobile, Status,Plant_code, Name  FROM  PlantStockSms WHERE (Plant_code = @Plant_code) and  (Status = '0') ");
                cmd.Parameters.Add("@Plant_code", PlantCode);
                //cmd.Parameters.Add("@routeid", routeid);
                DataTable dtmilkdetails = vdm.SelectQuery(cmd).Tables[0];
                if (dtmilkdetails.Rows.Count > 0)
                {
                    //foreach (DataRow dr in dtmilkdetails.Rows)
                    //{
                        string MobNo = dtmilkdetails.Rows[0]["Mobile"].ToString();
                        string Status = dtmilkdetails.Rows[0]["Status"].ToString();
                        if (Status == "0")
                        {
                            string rdate1 = planname;
                            string rdate = rdate1.Substring(0, 8) + "\r\n";
                            string details = "RName:" + routename + "\r\n";
                            string milk = "MKg:" + MilkKg + "(" + DiffMilkg + ")";
                            string ccdet = rdate + details + milk;
                            // string totaldetails = ccdet + details + milk;
                            string totaldetails2 = "test";
                            string baseurl = "http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=sulasha77&from=VYSNVI&to=" + MobNo + "&msg=" + ccdet + "&type=1 ";
                            Stream data = client.OpenRead(baseurl);
                            StreamReader reader = new StreamReader(data);
                            string ResponseID = reader.ReadToEnd();
                            data.Close();
                            reader.Close();
                        }
                    //}
                }
                    cmd = new SqlCommand("SELECT Tid, Mobile, Status,Plant_code, Name  FROM  PlantStockSms WHERE (Plant_code = @Plant_code) and (Route_id=@routeid) ");
                    cmd.Parameters.Add("@Plant_code", PlantCode);
                    cmd.Parameters.Add("@routeid", routeid);
                    DataTable dtmilkdetails1 = vdm.SelectQuery(cmd).Tables[0];
                    if (dtmilkdetails1.Rows.Count > 0)
                    {
                        string MobNo1 = dtmilkdetails1.Rows[0]["Mobile"].ToString();
                        string rdate2 = planname;
                        string rdate3 = rdate2.Substring(0, 8) + "\r\n";
                        string details1 = "RName:" + routename + "\r\n";
                        string milk1 = "MKg:" + MilkKg + ":" + DiffMilkg + "\r\n";
                        string ccdet1 = rdate3 + details1 + milk1;
                        // string totaldetails1 = ccdet + details + milk;
                        // string totaldetails2 = "test";
                        string baseurl1 = "http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=sulasha77&from=VYSNVI&to=" + MobNo1 + "&msg=" + ccdet1 + "&type=1 ";
                        Stream data1 = client.OpenRead(baseurl1);
                        StreamReader reader1 = new StreamReader(data1);
                        string ResponseID1 = reader1.ReadToEnd();
                        data1.Close();
                        reader1.Close();
                    }
            }
            string msg = "SMS sended successfully";
            string response = GetJson(msg);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void get_agentmilkdashboard_details(HttpContext context)
    {
        SalesDBManager vdm = new SalesDBManager();
        string plantcode = context.Request["plantcode"];
        string agentid = context.Request["agentid"];
        string[] strplant = plantcode.Split('_');
        string[] stragent = agentid.Split('_');
        DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
        string qtykgs = "";
        string month = "";
        cmd = new SqlCommand("SELECT YEAR(Prdate) AS YEAR, UPPER(LEFT(DATENAME(MONTH, Prdate), 3)) AS Month, SUM(Milk_kg) AS Milkkg, SUM(Milk_ltr) AS Milkltr, CONVERT(decimal(18, 2), AVG(Fat)) AS Fat, CONVERT(decimal(18, 2), AVG(Snf)) AS Snf, SUM(Amount) AS Amount, SUM(ComRate) AS Commission, CONVERT(decimal(18, 2), AVG(Rate)) AS AvgRate, Ratechart_Id AS Chart, CONVERT(decimal(18, 1), SUM(CartageAmount) / SUM(Milk_ltr)) AS cartage, CONVERT(decimal(18, 1), SUM(SplBonusAmount) / SUM(Milk_ltr)) AS SplBonus, Milk_Nature AS milknature, COUNT(*) AS MilkSession FROM Procurement WHERE        (Plant_Code = '" + strplant[0] + "') AND (Agent_id = '" + stragent[0] + "') GROUP BY YEAR(Prdate), MONTH(Prdate), DATENAME(MONTH, Prdate), Ratechart_Id, Milk_Nature ORDER BY YEAR");
        DataTable routes = vdm.SelectQuery(cmd).Tables[0];
        if (routes.Rows.Count > 0)
        {

            foreach (DataRow dr in routes.Rows)
            {
                string unitQty = dr["Milkkg"].ToString();
                qtykgs += unitQty + ",";
                string vendor = dr["Month"].ToString();
                month += vendor + ",";
            }
        }
        List<LineChartValuesclass> LineChartValuelist = new List<LineChartValuesclass>();
        LineChartValuesclass getLineChart = new LineChartValuesclass();
        List<string> quantitylist = new List<string>();
        List<string> Datelist = new List<string>();
        List<string> statuss = new List<string>();
        month = month.Substring(0, month.Length - 1);
        qtykgs = qtykgs.Substring(0, qtykgs.Length - 1);
        quantitylist.Add(qtykgs);
        //Datelist.Add(inDate);
        statuss.Add("Qty kgs");
        getLineChart.quantity = quantitylist;
        getLineChart.Date = month;
        getLineChart.status = statuss;
        LineChartValuelist.Add(getLineChart);
        string errresponse = GetJson(LineChartValuelist);
        context.Response.Write(errresponse);
    }

    private void get_plantwiseclosing_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string qtykgs = "";
            string month = "";
            string date = context.Request["date"];
            DateTime dtdate = Convert.ToDateTime(date);
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name from Plant_Master where Plant_Code not in (139,160,150)");
            DataTable dtVendor = vdm.SelectQuery(cmd).Tables[0];
            if (dtVendor.Rows.Count > 0)
            {
                foreach (DataRow drven in dtVendor.Rows)
                {
                    cmd = new SqlCommand("Select Date, sUM(MilkKg) as CbMilkKg, sum(Fat) as Fat , Sum(sNF) as Snf, Plant_code  FROM Stock_Milk WHERE Plant_code='" + drven["Plant_Code"] + "'  AND  (Date BETWEEN @d1 AND @d2) group by Date, Plant_code");
                    cmd.Parameters.Add("@d1", GetLowDate(dtdate));
                    cmd.Parameters.Add("@d2", GetHighDate(dtdate));
                    DataTable routes = vdm.SelectQuery(cmd).Tables[0];
                    if (routes.Rows.Count > 0)
                    {
                        foreach (DataRow dr in routes.Rows)
                        {
                            string unitQty = dr["CbMilkKg"].ToString();
                            qtykgs += unitQty + ",";
                            string vendor = drven["Plant_Name"].ToString();
                            month += vendor + ",";
                        }
                    }
                    else
                    {
                        string unitQty = "0";
                        qtykgs += unitQty + ",";
                        string vendor = drven["Plant_Name"].ToString();
                        month += vendor + ",";
                    }
                }
            }
            List<LineChartValuesclass> LineChartValuelist = new List<LineChartValuesclass>();
            LineChartValuesclass getLineChart = new LineChartValuesclass();
            List<string> quantitylist = new List<string>();
            List<string> Datelist = new List<string>();
            List<string> statuss = new List<string>();
            month = month.Substring(0, month.Length - 1);
            qtykgs = qtykgs.Substring(0, qtykgs.Length - 1);
            quantitylist.Add(qtykgs);
            //Datelist.Add(inDate);
            statuss.Add("Qty kgs");
            getLineChart.quantity = quantitylist;
            getLineChart.Date = month;
            getLineChart.status = statuss;
            LineChartValuelist.Add(getLineChart);
            string errresponse = GetJson(LineChartValuelist);
            context.Response.Write(errresponse);
        }
        catch (Exception ex)
        {

        }
    }


    public class InventaryTransaction
    {
        public string plant_code { get; set; }
        public string agentid { get; set; }
        public string date { get; set; }
        public string canid { get; set; }
        public string reciverorissuername { get; set; }
        public string qty { get; set; }
        public string isssueorreceive { get; set; }
        public string Inentarysno { get; set; }
    }
    private void Save_Inventary_Transaction(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            string agentid = context.Request["agentid"];
            string date = context.Request["date"];
            string canid = context.Request["canid"];
            string reciverorissuername = context.Request["reciverorissuername"];
            string qty = context.Request["qty"];
            string isssueorreceive = context.Request["isssueorreceive"];
            DateTime doe = DateTime.Now;
            string branchid = context.Session["branch"].ToString();
            string entry_by = context.Session["UserSno"].ToString();
            string btnval = context.Request["btnval"];
            string inventaryname = context.Request["inventaryname"];
            if (btnval == "Save")
            {
                if (isssueorreceive == "1")
                {
                    cmd = new SqlCommand("insert into AgentsCanIssuing(Plant_code,Agent_id,DateIssuingOrReceiving,Noofcans,IssuerOrRecesiverName,entry_by,branchid,inventarysno,isssueorreceive) values(@Plant_code,@Agent_id,@DateIssuingOrReceiving,@Noofcans,@IssuerOrRecesiverName,@entry_by,@branchid,@inventarysno,@isssueorreceive)");
                    cmd.Parameters.Add("@Plant_code", plant_code);
                    cmd.Parameters.Add("@Agent_id", agentid);
                    cmd.Parameters.Add("@DateIssuingOrReceiving", date);
                    cmd.Parameters.Add("@Noofcans", qty);
                    cmd.Parameters.Add("@isssueorreceive", isssueorreceive);
                    cmd.Parameters.Add("@IssuerOrRecesiverName", reciverorissuername);
                    // cmd.Parameters.Add("@CanType", canid);
                    cmd.Parameters.Add("@entry_by", entry_by);
                    cmd.Parameters.Add("@branchid", branchid);
                    cmd.Parameters.Add("@inventarysno", inventaryname);
                    //vdm.insert(cmd);
                    cmd = new SqlCommand("update inventorymonitor set Agent_id=@Agent_id, qty=qty-@qty where inventorysno=@inventarysno");
                    cmd.Parameters.Add("@Agent_id", agentid);
                    cmd.Parameters.Add("@qty", qty);
                    cmd.Parameters.Add("@inventarysno", inventaryname);
                    //vdm.Update(cmd);
                    string msg = "Inventary Transaction are successfully Inserted";
                    string Response = GetJson(msg);
                    context.Response.Write(Response);
                }
                else
                {
                    cmd = new SqlCommand("insert into AgentsCanIssuing(Plant_code,Agent_id,DateIssuingOrReceiving,Noofcans,IssuerOrRecesiverName,entry_by,branchid,inventarysno,isssueorreceive) values(@Plant_code,@Agent_id,@DateIssuingOrReceiving,@Noofcans,@IssuerOrRecesiverName,@entry_by,@branchid,@inventarysno,@isssueorreceive)");
                    cmd.Parameters.Add("@Plant_code", plant_code);
                    cmd.Parameters.Add("@Agent_id", agentid);
                    cmd.Parameters.Add("@DateIssuingOrReceiving", date);
                    cmd.Parameters.Add("@Noofcans", qty);
                    cmd.Parameters.Add("@isssueorreceive", isssueorreceive);
                    cmd.Parameters.Add("@IssuerOrRecesiverName", reciverorissuername);
                    // cmd.Parameters.Add("@CanType", canid);
                    cmd.Parameters.Add("@entry_by", entry_by);
                    cmd.Parameters.Add("@branchid", branchid);
                    cmd.Parameters.Add("@inventarysno", inventaryname);
                    vdm.insert(cmd);
                    cmd = new SqlCommand("update inventorymonitor set Agent_id=@Agent_id, qty=qty+@qty where inventorysno=@inventarysno");
                    cmd.Parameters.Add("@Agent_id", agentid);
                    cmd.Parameters.Add("@qty", qty);
                    cmd.Parameters.Add("@inventarysno", inventaryname);
                    vdm.Update(cmd);
                    string msg = "Inventary Transaction are successfully Inserted";
                    string Response = GetJson(msg);
                    context.Response.Write(Response);
                }
            }
            else
            {
                cmd = new SqlCommand("select * from AgentsCanIssuing where  inventarysno=@inventarysno and sno=@sno");
                cmd.Parameters.Add("@qty", qty);
                cmd.Parameters.Add("@inventarysno", inventaryname);
                DataTable dtprev = vdm.SelectQuery(cmd).Tables[0];
                float prevqty = 0;
                if (dtprev.Rows.Count > 0)
                {
                    string amount = dtprev.Rows[0]["Noofcans"].ToString();
                    float.TryParse(amount, out prevqty);
                }
                cmd = new SqlCommand("update AgentsCanIssuing set Plant_code=@Plant_code, Agent_id=@Agent_id, DateIssuingOrReceiving=@DateIssuingOrReceiving, Noofcans=@Noofcans,IssuerOrRecesiverName=@IssuerOrRecesiverName,entry_by=@entry_by,branchid=@branchid,isssueorreceive=@isssueorreceive,inventarysno=@inventarysno  where in_refno=@in_refno and sno=@sno ");
                cmd.Parameters.Add("@Plant_code", plant_code);
                cmd.Parameters.Add("@Agent_id", agentid);
                cmd.Parameters.Add("@DateIssuingOrReceiving", date);
                cmd.Parameters.Add("@Noofcans", qty);
                // cmd.Parameters.Add("@isssueorreceive", isssueorreceive);
                cmd.Parameters.Add("@IssuerOrRecesiverName", reciverorissuername);
                //cmd.Parameters.Add("@CanType", canid);
                cmd.Parameters.Add("@entry_by", entry_by);
                cmd.Parameters.Add("@branchid", branchid);
                cmd.Parameters.Add("@isssueorreceive", isssueorreceive);
                cmd.Parameters.Add("@inventarysno", inventaryname);

                float presentqty = 0;
                float.TryParse(qty, out presentqty);
                double editqty = 0;
                if (presentqty >= prevqty)
                {
                    editqty = presentqty - prevqty;
                    cmd = new SqlCommand("UPDATE inventorymonitor set qty=qty-@new_quantity where inventorysno=@inventarysno");
                    cmd.Parameters.Add("@inventarysno", inventaryname);
                    cmd.Parameters.Add("@new_quantity", editqty);
                    vdm.Update(cmd);
                }
                else
                {
                    editqty = prevqty - presentqty;
                    cmd = new SqlCommand("UPDATE inventorymonitor set qty=qty+@new_quantity where inventorysno=@inventarysno");
                    cmd.Parameters.Add("@inventarysno", inventaryname);
                    cmd.Parameters.Add("@new_quantity", editqty);
                    vdm.Update(cmd);
                }
                string msg = "Inventary Transaction successfully updated";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    private void get_Inventary_Transaction_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string plant_code = context.Request["plant_code"];
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            cmd = new SqlCommand("SELECT Tid, Plant_code, Agent_id, DateIssuingOrReceiving, IssuerOrRecesiverName, Noofcans, CanType, inventarysno FROM AgentsCanIssuing where Plant_code=@Plant_code  and  DateIssuingOrReceiving between @d1 and @d2");
            cmd.Parameters.Add("@d1", GetLowDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@d2", GetHighDate(ServerDateCurrentdate));
            cmd.Parameters.Add("@Plant_code", plant_code);
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<InventaryTransaction> InventaryTransactionlist = new List<InventaryTransaction>();
            foreach (DataRow dr in routes.Rows)
            {
                InventaryTransaction InventaryTransactionDetails = new InventaryTransaction();
                InventaryTransactionDetails.plant_code = dr["Plant_code"].ToString();
                InventaryTransactionDetails.agentid = dr["Agent_id"].ToString();
                InventaryTransactionDetails.date = dr["DateIssuingOrReceiving"].ToString();
                InventaryTransactionDetails.qty = dr["Noofcans"].ToString();
                InventaryTransactionDetails.plant_code = dr["Plant_code"].ToString();
                InventaryTransactionDetails.reciverorissuername = dr["IssuerOrRecesiverName"].ToString();
                InventaryTransactionDetails.canid = dr["CanType"].ToString();
                InventaryTransactionDetails.Inentarysno = dr["inentarysno"].ToString();
                InventaryTransactionlist.Add(InventaryTransactionDetails);
            }
            string response = GetJson(InventaryTransactionlist);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    private void saveInvetary(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            // string type = context.Request["type"];
            string inventaryname = context.Request["inventaryname"];
            string qty = context.Request["qty"];
            string inventorysno = context.Request["inventorysno"];
            //string createdby = "admin";
            //DateTime createdon = DateTime.Now;
            string btnSave = context.Request["btnVal"];
            if (btnSave == "save")
            {
                cmd = new SqlCommand("insert into inventarymaster (inventaryname,qty) values (@inventaryname,@qty)");
                cmd.Parameters.Add("@inventaryname", inventaryname);
                cmd.Parameters.Add("@qty", qty);
                vdm.insert(cmd);
                cmd = new SqlCommand("Select max(sno) as maxno from inventarymaster");
                DataTable dtroutes = vdm.SelectQuery(cmd).Tables[0];
                if (dtroutes.Rows.Count > 0)
                {
                    string maxno = dtroutes.Rows[0]["maxno"].ToString();
                    cmd = new SqlCommand("Update inventorymonitor set qty=qty+@qty where inventorysno=@inventorysno");
                    cmd.Parameters.Add("@qty", qty);
                    cmd.Parameters.Add("@inventorysno", maxno);
                    if (vdm.Update(cmd) == 0)
                    {
                        cmd = new SqlCommand("Insert INTO inventorymonitor(inventorysno, qty) values (@inventorysno, @qty)");
                        cmd.Parameters.Add("@qty", qty);
                        cmd.Parameters.Add("@inventorysno", maxno);
                        vdm.insert(cmd);
                    }
                }
                string msg = "Product Details  are successfully saved";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
            else
            {

                cmd = new SqlCommand("select * from inventorymonitor where  inventorysno=@inventorysno ");
                cmd.Parameters.Add("@inventorysno", inventorysno);
                DataTable dtprev = vdm.SelectQuery(cmd).Tables[0];
                float prevqty = 0;
                if (dtprev.Rows.Count > 0)
                {
                    string amount = dtprev.Rows[0]["qty"].ToString();
                    float.TryParse(amount, out prevqty);
                }

                cmd = new SqlCommand("Update inventarymaster set qty=@qty,inventaryname=@inventaryname where sno=@sno");
                cmd.Parameters.Add("@qty", qty);
                cmd.Parameters.Add("@inventaryname", inventaryname);
                cmd.Parameters.Add("@sno", inventorysno);
                if (vdm.Update(cmd) == 0)
                {

                    cmd = new SqlCommand("insert into inventarymaster (inventaryname,qty) values (@inventaryname,@qty)");
                    cmd.Parameters.Add("@inventaryname", inventaryname);
                    cmd.Parameters.Add("@qty", qty);
                    vdm.insert(cmd);
                }
                float presentqty = 0;
                float.TryParse(qty, out presentqty);
                double editqty = 0;
                if (presentqty >= prevqty)
                {
                    editqty = presentqty - prevqty;
                    cmd = new SqlCommand("UPDATE inventorymonitor set qty=qty+@new_quantity where inventorysno=@inventorysno");
                    cmd.Parameters.Add("@inventorysno", inventorysno);
                    cmd.Parameters.Add("@new_quantity", editqty);
                    if (vdm.Update(cmd) == 0)
                    {
                        cmd = new SqlCommand("insert into inventorymonitor (productid,qty) values(@productid,@qty)");
                        cmd.Parameters.Add("@productid", inventorysno);
                        cmd.Parameters.Add("@qty", qty);
                        vdm.insert(cmd);
                    }
                }
                else
                {
                    editqty = prevqty - presentqty;
                    cmd = new SqlCommand("UPDATE inventorymonitor set qty=qty-@new_quantity where inventorysno=@inventorysno");
                    cmd.Parameters.Add("@inventorysno", inventorysno);
                    cmd.Parameters.Add("@new_quantity", editqty);
                    if (vdm.Update(cmd) == 0)
                    {
                        cmd = new SqlCommand("insert into inventorymonitor (inventorysno,qty) values(@productid,@qty)");
                        cmd.Parameters.Add("@inventorysno", inventorysno);
                        cmd.Parameters.Add("@qty", qty);
                        vdm.insert(cmd);
                    }

                }
                string msg = "Product Details successfully updatted";
                string Response = GetJson(msg);
                context.Response.Write(Response);
            }
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    public class Invetary
    {
        public string inventaryname { get; set; }
        public string inventorysno { get; set; }
        public string qty { get; set; }
        public string monitorqty { get; set; }
        //public string sno { get; set; }
        //public string moniterqty { get; set; }
        //public string plantname { get; set; }
        //public string plantid { get; set; }
    }
    private void get_InvetaryMaster_details(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            cmd = new SqlCommand("SELECT inventarymaster.sno, inventarymaster.inventaryname, inventarymaster.qty, inventorymonitor.sno AS Expr1, inventorymonitor.Agent_id, inventorymonitor.qty AS monitorqty, inventorymonitor.inventorysno FROM inventarymaster INNER JOIN inventorymonitor ON inventarymaster.sno = inventorymonitor.inventorysno");
            DataTable routes = vdm.SelectQuery(cmd).Tables[0];
            List<Invetary> Invetarylist = new List<Invetary>();
            foreach (DataRow dr in routes.Rows)
            {
                Invetary getnvetarydetails = new Invetary();
                getnvetarydetails.inventaryname = dr["inventaryname"].ToString();
                getnvetarydetails.qty = dr["qty"].ToString();
                getnvetarydetails.inventorysno = dr["sno"].ToString();
                getnvetarydetails.monitorqty = dr["monitorqty"].ToString();
                //getnvetarydetails.moniterqty = dr["moniterqty"].ToString();
                //getnvetarydetails.plantname = dr["plant_name"].ToString();
                //getnvetarydetails.plantid = dr["plant_code"].ToString();
                //var status = dr["status"].ToString();
                //if (status == "True")
                //{
                //    getbilldetails.status = "Active";
                //}
                //if (status == "False")
                //{
                //    getbilldetails.status = "InActive";
                //}
                Invetarylist.Add(getnvetarydetails);
            }
            string response = GetJson(Invetarylist);
            context.Response.Write(response);
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }


    public class ledger
    {
        public string ledgercode { get; set; }
        public string ledgername { get; set; }
        public string userid { get; set; }

    }
    private void saveledgermaster(HttpContext context)
    {
        try
        {
            SalesDBManager vdm = new SalesDBManager();
            string ledgercode = context.Request["ledgercode"];
            string ledgername = context.Request["ledgername"];

            string btnSave = context.Request["btnVal"];
            if (btnSave == "save")
            {
                cmd = new SqlCommand("insert into LedgerDetails(LedgerName,LedgerCode) values (@LedgerName,@LedgerCode)");
                cmd.Parameters.Add("@LedgerName", ledgername);
                cmd.Parameters.Add("@LedgerCode", ledgercode);
                vdm.insert(cmd);
                string msg = "Ledger detailes successfully Saved";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("Update LedgerDetails set  LedgerName=@LedgerName,LedgerCode=@LedgerCode");
                cmd.Parameters.Add("@LedgerName", ledgername);
                cmd.Parameters.Add("@LedgerCode", ledgercode);
                vdm.Update(cmd);
                string msg = "Ledger detailes successfully Updated";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch (Exception ex)
        {
            string Response = GetJson(ex.Message);
            context.Response.Write(Response);
        }
    }
    private void get_ledger_details1(HttpContext context)
    {
        try
        {
            string userid = context.Request["userid"];
            SalesDBManager vdm = new SalesDBManager();
            cmd = new SqlCommand("SELECT  LedgerName,LedgerCode from LedgerDetails");
            DataTable dtaddress = vdm.SelectQuery(cmd).Tables[0];
            List<ledger> plantlist = new List<ledger>();
            foreach (DataRow dr in dtaddress.Rows)
            {
                ledger plant = new ledger();
                plant.ledgername = dr["LedgerName"].ToString();
                plant.ledgercode = dr["LedgerCode"].ToString();
                plantlist.Add(plant);
            }
            string response = GetJson(plantlist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }
    private void get_ledger_details(HttpContext context)
    {
        try
        {
            string userid = context.Request["userid"];
            string branchid=context.Session["branch"].ToString();
            SalesDBManager vdm = new SalesDBManager();
            cmd = new SqlCommand("SELECT  LedgerName,LedgerCode from LedgerDetails where Tid=@Tid");
            cmd.Parameters.Add("@Tid", userid);
            DataTable dtaddress = vdm.SelectQuery(cmd).Tables[0];
            List<ledger> plantlist = new List<ledger>();
            foreach (DataRow dr in dtaddress.Rows)
            {
                ledger plant = new ledger();
                plant.ledgername = dr["LedgerName"].ToString();
                plant.ledgercode = dr["LedgerCode"].ToString();
                plantlist.Add(plant);
            }
            string response = GetJson(plantlist);
            context.Response.Write(response);
        }
        catch
        {
        }
    }

    private void BtnCashAmountClick(HttpContext context)
    {
        try
        {
            vdm = new SalesDBManager();
            string Name = context.Request["Name"];
            string ledgercode = context.Request["ledgercode"];
            //string Receiptno = context.Request["Receiptno"];
            string Amount = context.Request["Amount"];
            string Remarks = context.Request["Remarks"];
            string ddlAmountType = context.Request["ddlAmountType"];
            string ChequeNo = context.Request["ChequeNo"];
            string chequeDate = context.Request["chequeDate"];
            DateTime dtchequedate = new DateTime();
            dtchequedate = SalesDBManager.GetTime(vdm.conn);
            string BankName = context.Request["BankName"];
            DateTime CurDate = SalesDBManager.GetTime(vdm.conn);
            DateTime dtapril = new DateTime();
            DateTime dtmarch = new DateTime();
            int currentyear = CurDate.Year;
            int nextyear = CurDate.Year + 1;
            if (CurDate.Month > 3)
            {
                string apr = "4/1/" + currentyear;
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + nextyear;
                dtmarch = DateTime.Parse(march);
            }
            if (CurDate.Month <= 3)
            {
                string apr = "4/1/" + (currentyear - 1);
                dtapril = DateTime.Parse(apr);
                string march = "3/31/" + (nextyear - 1);
                dtmarch = DateTime.Parse(march);
            }
            string remarks = "Other Collections";
            cmd = new SqlCommand("SELECT Branchid, UserData_sno, AmountPaid, Denominations, Remarks, Sno, PaidDate FROM collections WHERE (Branchid = @BranchID) AND (PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
            cmd.Parameters.Add("@d1", GetLowDate(CurDate));
            cmd.Parameters.Add("@d2", GetHighDate(CurDate));
            DataTable dtcashbookstatus = vdm.SelectQuery(cmd).Tables[0];
            if (dtcashbookstatus.Rows.Count > 0)
            {
                string msg = "Cash Book Has Been Closed For This Day";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
            else
            {
                cmd = new SqlCommand("SELECT { fn IFNULL(MAX(Receipt), 0) } + 1 AS Sno FROM cashreceipts WHERE (BranchId = @BranchID) AND (DOE BETWEEN @d1 AND @d2)");
                cmd.Parameters.Add("@BranchID", context.Session["branch"].ToString());
                cmd.Parameters.Add("@d1", GetLowDate(dtapril));
                cmd.Parameters.Add("@d2", GetHighDate(dtmarch));
                DataTable dtReceipt = vdm.SelectQuery(cmd).Tables[0];
                string CashReceiptNo = dtReceipt.Rows[0]["Sno"].ToString();
                cmd = new SqlCommand("insert into cashreceipts (BranchId,ReceivedFrom,AmountPaid,DOE,Create_by,Remarks,Receipt) values (@BranchId,@ReceivedFrom,@AmountPaid,@DOE, @Create_by,@Remarks,@Receipt)");
                cmd.Parameters.Add("@BranchId", context.Session["branch"].ToString());
                cmd.Parameters.Add("@ReceivedFrom", "Others");
                cmd.Parameters.Add("@AmountPaid", Amount);
                cmd.Parameters.Add("DOE", CurDate);
                cmd.Parameters.Add("@Create_by", context.Session["UserSno"].ToString());
                cmd.Parameters.Add("@Remarks", remarks);
                cmd.Parameters.Add("@Receipt", CashReceiptNo);
                vdm.insert(cmd);
                if (ddlAmountType == "Cash" || ddlAmountType == "Bank Transfer")
                {
                    cmd = new SqlCommand("insert into cashcollections (BranchID,Name,LedgerCode,Amount,Remarks,DOE,Receiptno,CollectionType) values(@BranchID,@Name,@LedgerCode,@Amount,@Remarks,@DOE,@Receiptno,@CollectionType)");
                }
                if (ddlAmountType == "Cheque" || ddlAmountType == "DD")
                {
                    cmd = new SqlCommand("insert into cashcollections (BranchID,Name,LedgerCode,Amount,Remarks,DOE,Receiptno,CollectionType,CheckStatus,ChequeNo,ChequeDate,BankName) values(@BranchID,@Name,@LedgerCode,@Amount,@Remarks,@DOE,@Receiptno,@CollectionType,@CheckStatus,@ChequeNo,@ChequeDate,@BankName)");
                }
                cmd.Parameters.Add("@BranchID", context.Session["branch"]);
                cmd.Parameters.Add("@Name", Name);
                cmd.Parameters.Add("@LedgerCode", ledgercode);
                cmd.Parameters.Add("@Amount", Amount);
                cmd.Parameters.Add("@Remarks", Remarks);
                cmd.Parameters.Add("@Receiptno", CashReceiptNo);
                cmd.Parameters.Add("@DOE", CurDate);
                cmd.Parameters.Add("@CollectionType", ddlAmountType);
                cmd.Parameters.Add("@CheckStatus", 'P');
                cmd.Parameters.Add("@ChequeNo", ChequeNo);
                cmd.Parameters.Add("@ChequeDate", dtchequedate);
                cmd.Parameters.Add("@BankName", BankName);
                vdm.insert(cmd);
                string msg = "Cash Collection Saved Successfully";
                string response = GetJson(msg);
                context.Response.Write(response);
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            string response = GetJson(msg);
            context.Response.Write(response);
        }
    }


    public class procinfodetails
    {
        public string plantcode { get; set; }
        public string plantname { get; set; }
        public string present { get; set; }
        public string Lastweek { get; set; }
        public string lastmonth { get; set; }
        public string lastyear { get; set; }
        public string doe { get; set; }
        public string routename { get; set; }
        public string Milktype { get; set; }
        public string routeid { get; set; }
        public string totalkms { get; set; }
        public string agentname { get; set; }
    }

    //
    private void get_procinfo_details(HttpContext context)
    {
        try
        {
            DataTable Report = new DataTable();
            Report.Columns.Add("Milktype");
            Report.Columns.Add("Plant Code");
            Report.Columns.Add("Plant Name");
            Report.Columns.Add("Present");
            Report.Columns.Add("last Week Same Day");
            Report.Columns.Add("last Month Same Day");
            Report.Columns.Add("last Year Same Day");
            vdm = new SalesDBManager();

            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            
            
            DateTime LDATE = GetLowDate(ServerDateCurrentdate).AddDays(-1);
            DateTime HDATE = GetHighDate(ServerDateCurrentdate).AddDays(-1);
            string cdate = LDATE.ToString("dd/MM/yyyy"); 

            DateTime WLDATE = GetLowDate(ServerDateCurrentdate).AddDays(-8);
            DateTime WHDATE = GetHighDate(ServerDateCurrentdate).AddDays(-8);

            DateTime MLDATE = GetLowDate(ServerDateCurrentdate).AddMonths(-1);
            DateTime MHDATE = GetHighDate(ServerDateCurrentdate).AddMonths(-1);

            DateTime YLDATE = GetLowDate(ServerDateCurrentdate).AddYears(-1);
            DateTime YHDATE = GetHighDate(ServerDateCurrentdate).AddYears(-1);

            string LOWDATE = LDATE.ToString("MM/dd/yyyy");
            string HIGHDATE = HDATE.ToString("MM/dd/yyyy");


            string WLOWDATE = WLDATE.ToString("MM/dd/yyyy");
            string WHIGHDATE = WHDATE.ToString("MM/dd/yyyy");

            string MLOWDATE = MLDATE.ToString("MM/dd/yyyy");
            string MHIGHDATE = MHDATE.ToString("MM/dd/yyyy");

            string YLOWDATE = YLDATE.ToString("MM/dd/yyyy");
            string YHIGHDATE = YHDATE.ToString("MM/dd/yyyy");


            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name, Milktype FROM Plant_Master Where flag='1' ORDER BY Milktype");
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code  FROM  Procurementimport  WHERE  (Prdate BETWEEN @d1 AND @d2)  GROUP BY Plant_Code");
            cmd.Parameters.Add("@d1", LOWDATE);
            cmd.Parameters.Add("@d2", HIGHDATE);
            DataTable dtpresent = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code  FROM  Procurementimport  WHERE  (Prdate BETWEEN @Wd1 AND @Wd2)  GROUP BY Plant_Code");
            cmd.Parameters.Add("@Wd1", WLOWDATE);
            cmd.Parameters.Add("@Wd2", WHIGHDATE);
            DataTable dtlastweek = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code  FROM  Procurementimport  WHERE  (Prdate BETWEEN @Md1 AND @Md2)  GROUP BY Plant_Code");
            cmd.Parameters.Add("@Md1", MLOWDATE);
            cmd.Parameters.Add("@Md2", MHIGHDATE);
            DataTable dtlastmonth = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code  FROM  Procurementimport  WHERE  (Prdate BETWEEN @Yd1 AND @Yd2)  GROUP BY Plant_Code");
            cmd.Parameters.Add("@Yd1", YLOWDATE);
            cmd.Parameters.Add("@Yd2", YHIGHDATE);
            DataTable dtlastyear = vdm.SelectQuery(cmd).Tables[0];
            List<procinfodetails> plantlist = new List<procinfodetails>();
            if (dtPlant.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPlant.Rows)
                {
                    DataRow newdr = Report.NewRow();
                    string plantcode = dr["Plant_Code"].ToString();
                    newdr["Milktype"] = dr["Milktype"].ToString();
                    newdr["Plant Code"] = plantcode;
                    newdr["Plant Name"] = dr["Plant_Name"].ToString();
                    foreach (DataRow drpresent in dtpresent.Select("Plant_Code='" + plantcode + "'"))
                    {
                        string milkltr = drpresent["Milk_ltrs"].ToString();
                        newdr["Present"] = milkltr;
                    }

                    foreach (DataRow drweek in dtlastweek.Select("Plant_Code='" + plantcode + "'"))
                    {
                        string milkltr1 = drweek["Milk_ltrs"].ToString();
                        newdr["last Week Same Day"] = milkltr1;
                    }
                    string milkltr2 = "0";
                    foreach (DataRow drmonth in dtlastmonth.Select("Plant_Code='" + plantcode + "'"))
                    {
                        milkltr2 = drmonth["Milk_ltrs"].ToString();
                        newdr["last Month Same Day"] = milkltr2;
                    }

                    if (dtlastyear.Rows.Count > 0)
                    {
                        foreach (DataRow dryear in dtlastyear.Select("Plant_Code='" + plantcode + "'"))
                        {
                            string milkltr3 = dryear["Milk_ltrs"].ToString();
                            newdr["last Year Same Day"] = milkltr3;
                        }
                    }
                    else
                    {
                        double mlk = Convert.ToDouble(milkltr2);
                        newdr["last Year Same Day"] = mlk + 86;
                    }
                    Report.Rows.Add(newdr);
                }

                if (Report.Rows.Count > 0)
                {
                    foreach (DataRow drr in Report.Rows)
                    {
                        procinfodetails procinfo = new procinfodetails();
                        procinfo.plantcode = drr["Plant Code"].ToString();
                        procinfo.plantname = drr["Plant Name"].ToString();
                        procinfo.present = drr["Present"].ToString();
                        procinfo.Lastweek = drr["last Week Same Day"].ToString();
                        procinfo.lastmonth = drr["last Month Same Day"].ToString();
                        procinfo.lastyear = drr["last Year Same Day"].ToString();
                        procinfo.doe = cdate;
                        procinfo.Milktype = drr["Milktype"].ToString();
                        plantlist.Add(procinfo);
                    }
                    string response = GetJson(plantlist);
                    context.Response.Write(response);
                }
            }
        }
        catch
        {
        }
    }

    private void get_procinforoute_details(HttpContext context)
    {
        try
        {
            DataTable Report = new DataTable();
            Report.Columns.Add("Plant Name");
            Report.Columns.Add("Route Name");
            Report.Columns.Add("RouteId");
            Report.Columns.Add("Plantcode");
            Report.Columns.Add("Present");
            Report.Columns.Add("last Week Same Day");
            Report.Columns.Add("last Month Same Day");
            Report.Columns.Add("last Year Same Day");
            vdm = new SalesDBManager();

            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            string cdate = ServerDateCurrentdate.ToString("dd/MM/yyyy");

            DateTime LDATE = GetLowDate(ServerDateCurrentdate).AddDays(-1);
            DateTime HDATE = GetHighDate(ServerDateCurrentdate).AddDays(-1);

            DateTime WLDATE = GetLowDate(ServerDateCurrentdate).AddDays(-8);
            DateTime WHDATE = GetHighDate(ServerDateCurrentdate).AddDays(-8);

            DateTime MLDATE = GetLowDate(ServerDateCurrentdate).AddMonths(-1);
            DateTime MHDATE = GetHighDate(ServerDateCurrentdate).AddMonths(-1);

            DateTime YLDATE = GetLowDate(ServerDateCurrentdate).AddYears(-1);
            DateTime YHDATE = GetHighDate(ServerDateCurrentdate).AddYears(-1);

            string LOWDATE = LDATE.ToString("MM/dd/yyyy");
            string HIGHDATE = HDATE.ToString("MM/dd/yyyy");


            string WLOWDATE = WLDATE.ToString("MM/dd/yyyy");
            string WHIGHDATE = WHDATE.ToString("MM/dd/yyyy");

            string MLOWDATE = MLDATE.ToString("MM/dd/yyyy");
            string MHIGHDATE = MHDATE.ToString("MM/dd/yyyy");

            string YLOWDATE = YLDATE.ToString("MM/dd/yyyy");
            string YHIGHDATE = YHDATE.ToString("MM/dd/yyyy");

            string replantcode = context.Request["plantcode"].ToString();

            cmd = new SqlCommand("SELECT Plant_Master.Plant_Code, Plant_Master.Plant_Name, Route_Master.Route_Name, Route_Master.Route_ID FROM Plant_Master INNER JOIN Route_Master ON Plant_Master.Plant_Code = Route_Master.Plant_Code  WHERE  (Plant_Master.flag = '1') AND (Route_Master.Plant_Code = @PLANTCODE) ORDER BY Plant_Master.Milktype");
            cmd.Parameters.Add("@PLANTCODE", replantcode);
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id  FROM  Procurementimport WHERE   (Prdate BETWEEN @d1 AND @d2) GROUP BY Plant_Code, Route_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@d1", LOWDATE);
            cmd.Parameters.Add("@d2", HIGHDATE);
            DataTable dtpresent = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id  FROM  Procurementimport WHERE   (Prdate BETWEEN @Wd1 AND @Wd2) GROUP BY Plant_Code, Route_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@Wd1", WLOWDATE);
            cmd.Parameters.Add("@Wd2", WHIGHDATE);
            DataTable dtlastweek = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id  FROM  Procurementimport WHERE   (Prdate BETWEEN @Md1 AND @Md2) GROUP BY Plant_Code, Route_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@Md1", MLOWDATE);
            cmd.Parameters.Add("@Md2", MHIGHDATE);
            DataTable dtlastmonth = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id  FROM  Procurementimport WHERE   (Prdate BETWEEN @Yd1 AND @Yd2) GROUP BY Plant_Code, Route_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@Yd1", YLOWDATE);
            cmd.Parameters.Add("@Yd2", YHIGHDATE);
            DataTable dtlastyear = vdm.SelectQuery(cmd).Tables[0];
            List<procinfodetails> plantlist = new List<procinfodetails>();
            if (dtPlant.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPlant.Rows)
                {
                    DataRow newdr = Report.NewRow();
                    string plantcode = dr["Plant_Code"].ToString();
                    string routeid = dr["Route_ID"].ToString();
                    newdr["RouteId"] = routeid;
                    newdr["Plantcode"] = plantcode;
                    newdr["Plant Name"] = dr["Plant_Name"].ToString();
                    newdr["Route Name"] = dr["Route_Name"].ToString();
                    foreach (DataRow drpresent in dtpresent.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "'"))
                    {
                        string milkltr = drpresent["Milk_ltrs"].ToString();
                        newdr["Present"] = milkltr;
                    }

                    foreach (DataRow drweek in dtlastweek.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "'"))
                    {
                        string milkltr1 = drweek["Milk_ltrs"].ToString();
                        newdr["last Week Same Day"] = milkltr1;
                    }
                    string milkltr2 = "0";
                    foreach (DataRow drmonth in dtlastmonth.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "'"))
                    {
                        milkltr2 = drmonth["Milk_ltrs"].ToString();
                        newdr["last Month Same Day"] = milkltr2;
                    }

                    if (dtlastyear.Rows.Count > 0)
                    {
                        foreach (DataRow dryear in dtlastyear.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "'"))
                        {
                            string milkltr3 = dryear["Milk_ltrs"].ToString();
                            newdr["last Year Same Day"] = milkltr3;
                        }
                    }
                    else
                    {
                        double mlk = Convert.ToDouble(milkltr2);
                        if (mlk == 0)
                        {
                            newdr["last Year Same Day"] = "";
                        }
                        else
                        {
                            newdr["last Year Same Day"] = mlk + 20;
                        }
                    }
                    Report.Rows.Add(newdr);
                }

                if (Report.Rows.Count > 0)
                {
                    foreach (DataRow drr in Report.Rows)
                    {
                        procinfodetails procinfo = new procinfodetails();
                        procinfo.plantcode = drr["Plantcode"].ToString();
                        procinfo.routeid = drr["RouteId"].ToString();
                        procinfo.routename = drr["Route Name"].ToString();
                        procinfo.plantname = drr["Plant Name"].ToString();
                        procinfo.present = drr["Present"].ToString();
                        procinfo.Lastweek = drr["last Week Same Day"].ToString();
                        procinfo.lastmonth = drr["last Month Same Day"].ToString();
                        procinfo.lastyear = drr["last Year Same Day"].ToString();
                        procinfo.doe = cdate;
                        plantlist.Add(procinfo);
                    }
                    string response = GetJson(plantlist);
                    context.Response.Write(response);
                }
            }
        }
        catch
        {
        }
    }

    private void viewroutewiseagent_details(HttpContext context)
    {
        try
        {
            DataTable Report = new DataTable();
            Report.Columns.Add("Route Name");
            Report.Columns.Add("Agent Name");
            Report.Columns.Add("Present");
            Report.Columns.Add("last Week Same Day");
            Report.Columns.Add("last Month Same Day");
            Report.Columns.Add("last Year Same Day");
            Report.Columns.Add("Total KMS To Plant");
            vdm = new SalesDBManager();

            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);
            string cdate = ServerDateCurrentdate.ToString("dd/MM/yyyy");

            DateTime LDATE = GetLowDate(ServerDateCurrentdate).AddDays(-1);
            DateTime HDATE = GetHighDate(ServerDateCurrentdate).AddDays(-1);

            DateTime WLDATE = GetLowDate(ServerDateCurrentdate).AddDays(-8);
            DateTime WHDATE = GetHighDate(ServerDateCurrentdate).AddDays(-8);

            DateTime MLDATE = GetLowDate(ServerDateCurrentdate).AddMonths(-1);
            DateTime MHDATE = GetHighDate(ServerDateCurrentdate).AddMonths(-1);

            DateTime YLDATE = GetLowDate(ServerDateCurrentdate).AddYears(-1);
            DateTime YHDATE = GetHighDate(ServerDateCurrentdate).AddYears(-1);

            string LOWDATE = LDATE.ToString("MM/dd/yyyy");
            string HIGHDATE = HDATE.ToString("MM/dd/yyyy");


            string WLOWDATE = WLDATE.ToString("MM/dd/yyyy");
            string WHIGHDATE = WHDATE.ToString("MM/dd/yyyy");

            string MLOWDATE = MLDATE.ToString("MM/dd/yyyy");
            string MHIGHDATE = MHDATE.ToString("MM/dd/yyyy");

            string YLOWDATE = YLDATE.ToString("MM/dd/yyyy");
            string YHIGHDATE = YHDATE.ToString("MM/dd/yyyy");

            string plantcode = context.Request["plantcode"].ToString();
            string routeid = context.Request["routeid"].ToString();
            string routename = "";
            string totalkms = "";
            cmd = new SqlCommand("SELECT Route_ID,Route_Name, Tot_Distance  FROM Route_Master WHERE  (Plant_Code = @prcode) AND (Route_ID = @prid)");
            cmd.Parameters.Add("@prcode", plantcode);
            cmd.Parameters.Add("@prid", routeid);
            DataTable dtrt = vdm.SelectQuery(cmd).Tables[0];
            if (dtrt.Rows.Count > 0)
            {
                routename = dtrt.Rows[0]["Route_Name"].ToString();
                totalkms = dtrt.Rows[0]["Tot_Distance"].ToString();
            }
            cmd = new SqlCommand("SELECT Tid, Agent_Id, Agent_Name, Cartage_Amt, Plant_code, Route_id, Milk_Nature FROM  Agent_Master WHERE (Plant_code = @pcode) AND (Route_id = @rid) ORDER BY Agent_Id, Milk_Nature");
            cmd.Parameters.Add("@pcode", plantcode);
            cmd.Parameters.Add("@rid", routeid);
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id, Agent_id FROM  Procurementimport WHERE   (Prdate BETWEEN @d1 AND @d2) GROUP BY Plant_Code, Route_id, Agent_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@d1", LOWDATE);
            cmd.Parameters.Add("@d2", HIGHDATE);
            DataTable dtpresent = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id, Agent_id  FROM  Procurementimport WHERE   (Prdate BETWEEN @Wd1 AND @Wd2) GROUP BY Plant_Code, Route_id, Agent_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@Wd1", WLOWDATE);
            cmd.Parameters.Add("@Wd2", WHIGHDATE);
            DataTable dtlastweek = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id, Agent_id  FROM  Procurementimport WHERE   (Prdate BETWEEN @Md1 AND @Md2) GROUP BY Plant_Code, Route_id, Agent_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@Md1", MLOWDATE);
            cmd.Parameters.Add("@Md2", MHIGHDATE);
            DataTable dtlastmonth = vdm.SelectQuery(cmd).Tables[0];

            cmd = new SqlCommand("SELECT SUM(Milk_ltr) AS Milk_ltrs, Plant_Code, Route_id, Agent_id  FROM  Procurementimport WHERE   (Prdate BETWEEN @Yd1 AND @Yd2) GROUP BY Plant_Code, Route_id, Agent_id ORDER BY Plant_Code");
            cmd.Parameters.Add("@Yd1", YLOWDATE);
            cmd.Parameters.Add("@Yd2", YHIGHDATE);
            DataTable dtlastyear = vdm.SelectQuery(cmd).Tables[0];
            List<procinfodetails> plantlist = new List<procinfodetails>();
            if (dtPlant.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPlant.Rows)
                {
                    DataRow newdr = Report.NewRow();
                    string Agent_Id = dr["Agent_Id"].ToString();
                    string Agent_Name = dr["Agent_Name"].ToString();
                    newdr["Route Name"] = routename;
                    newdr["Agent Name"] = Agent_Name;
                    newdr["Total KMS To Plant"] = totalkms;
                    foreach (DataRow drpresent in dtpresent.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "' AND Agent_id='" + Agent_Id + "'"))
                    {
                        string milkltr = drpresent["Milk_ltrs"].ToString();
                        newdr["Present"] = milkltr;
                    }

                    foreach (DataRow drweek in dtlastweek.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "' AND Agent_id='" + Agent_Id + "'"))
                    {
                        string milkltr1 = drweek["Milk_ltrs"].ToString();
                        newdr["last Week Same Day"] = milkltr1;
                    }
                    string milkltr2 = "0";
                    foreach (DataRow drmonth in dtlastmonth.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "' AND Agent_id='" + Agent_Id + "'"))
                    {
                        milkltr2 = drmonth["Milk_ltrs"].ToString();
                        newdr["last Month Same Day"] = milkltr2;
                    }

                    if (dtlastyear.Rows.Count > 0)
                    {
                        foreach (DataRow dryear in dtlastyear.Select("Plant_Code='" + plantcode + "' AND Route_id='" + routeid + "' AND Agent_id='" + Agent_Id + "'"))
                        {
                            string milkltr3 = dryear["Milk_ltrs"].ToString();
                            newdr["last Year Same Day"] = milkltr3;
                        }
                    }
                    else
                    {
                        double mlk = Convert.ToDouble(milkltr2);
                        if (mlk == 0)
                        {
                            newdr["last Year Same Day"] = "";
                        }
                        else
                        {
                            newdr["last Year Same Day"] = mlk + 20;
                        }
                    }
                    Report.Rows.Add(newdr);
                }

                if (Report.Rows.Count > 0)
                {
                    foreach (DataRow drr in Report.Rows)
                    {
                        procinfodetails procinfo = new procinfodetails();
                        procinfo.routename = drr["Route Name"].ToString();
                        procinfo.agentname = drr["Agent Name"].ToString();
                        procinfo.totalkms = drr["Total KMS To Plant"].ToString();
                        procinfo.present = drr["Present"].ToString();
                        procinfo.Lastweek = drr["last Week Same Day"].ToString();
                        procinfo.lastmonth = drr["last Month Same Day"].ToString();
                        procinfo.lastyear = drr["last Year Same Day"].ToString();
                        procinfo.doe = cdate;
                        plantlist.Add(procinfo);
                    }
                    string response = GetJson(plantlist);
                    context.Response.Write(response);
                }
            }
        }
        catch
        {
        }
    }

    private void get_silobalancerpt_details(HttpContext context)
    {
        try
        {
            DataTable Report = new DataTable();
            Report.Columns.Add("Details");
            Report.Columns.Add("Milk KGS");
            Report.Columns.Add("Mulk LTR");
            Report.Columns.Add("FAT/KGS");
            Report.Columns.Add("SNF/KGS");
            Report.Columns.Add("Milk Amount");
            vdm = new SalesDBManager();
            string planttype = context.Request["plantcode"].ToString();
            string d1 = context.Request["frmdate"].ToString();
            string d2 = context.Request["todate"].ToString();
            string type = context.Request["type"].ToString();
            string btnSave = context.Request["btnSave"].ToString();
            if (planttype == "Cow")
            {
                cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master Where Milktype='1'");
            }
            else
            {
                cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master Where Milktype='2'");
            }
            DataTable dtPlantdtls = vdm.SelectQuery(cmd).Tables[0];
            if (dtPlantdtls.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPlantdtls.Rows)
                {
                    string plantcode = "168";
                    if (plantcode == "171")
                    {
                        plantcode = "168";
                    }
                    if (plantcode == "172")
                    {
                        plantcode = "166";
                    }
                    if (plantcode == "168")
                    {
                        cmd = new SqlCommand("SELECT ak.pr_MILKKG, ak.pr_FAT, ak.pr_SNF, ak.pr_FATKG, ak.pr_SNFKG, ak.pr_RATE, ak.pr_AMOUNT, ak.dp_MILKKG, ak.dp_FAT, ak.dp_SNF, ak.dp_FATKG, ak.dp_SNFKG, ak.dp_RATE, ak.dp_AMOUNT, ak.op_MILKKG, ak.op_FAT,  ak.op_SNF, ak.op_FATKG, ak.op_SNFKG, ak.op_RATE, ak.op_AMOUNT, ak.cl_MILKKG, ak.cl_FAT, ak.cl_SNF, ak.cl_FATKG, ak.cl_SNFKG, ak.cl_RATE, ak.cl_AMOUNT, ak.Plant_code, ak.Rate FROM            (SELECT        t1.pr_MILKKG, t1.pr_FAT, t1.pr_SNF, t1.pr_FATKG, t1.pr_SNFKG, t1.pr_RATE, t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT, t2.dp_SNF, t2.dp_FATKG, t2.dp_SNFKG, t2.dp_RATE, t2.dp_AMOUNT, t3.op_MILKKG, t3.op_FAT,   t3.op_SNF, t3.op_FATKG, t3.op_SNFKG, t3.op_RATE, t3.op_AMOUNT, C.cl_MILKKG, C.cl_FAT, C.cl_SNF, C.cl_FATKG, C.cl_SNFKG, C.cl_RATE, C.cl_AMOUNT, t2.Plant_code, t1.Rate   FROM            (SELECT        ISNULL(SUM(Milk_kg), 0) AS pr_MILKKG, ISNULL(AVG(Fat), 0) AS pr_FAT, ISNULL(AVG(Snf), 0) AS pr_SNF, ISNULL(SUM(fat_kg), 0) AS pr_FATKG, ISNULL(SUM(snf_kg), 0) AS pr_SNFKG, ISNULL(SUM(Rate),  0) AS pr_RATE, ISNULL(SUM(Amount), 0) AS pr_AMOUNT, CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate  FROM            Procurement WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (168, 171))) AS t1 CROSS JOIN    (SELECT        Plant_code, ISNULL(SUM(MilkKg), 0) AS dp_MILKKG, ISNULL(AVG(Fat), 0) AS dp_FAT, ISNULL(AVG(Snf), 0) AS dp_SNF, ISNULL(SUM(Fat_Kg), 0) AS dp_FATKG, ISNULL(SUM(Snf_Kg), 0) AS dp_SNFKG,   ISNULL(SUM(Rate), 0) AS dp_RATE, ISNULL(SUM(Amount), 0) AS dp_AMOUNT     FROM            Despatchnew   WHERE        (Date BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_code = '" + plantcode + "') GROUP BY Plant_code) AS t2 CROSS JOIN    (SELECT        ISNULL(SUM(MilkKg), 0) AS op_MILKKG, ISNULL(AVG(Fat), 0) AS op_FAT, ISNULL(AVG(Snf), 0) AS op_SNF, ISNULL(SUM(Fat_Kg), 0) AS op_FATKG, ISNULL(SUM(Snf_Kg), 0) AS op_SNFKG,   ISNULL(SUM(Rate), 0) AS op_RATE, ISNULL(SUM(Amount), 0) AS op_AMOUNT  FROM            Stock_openingmilk  WHERE        (Datee = '" + d1 + "') AND (Plant_code = '" + plantcode + "')) AS t3 CROSS JOIN (SELECT        ISNULL(SUM(MilkKg), 0) AS cl_MILKKG, ISNULL(AVG(Fat), 0) AS cl_FAT, ISNULL(AVG(Snf), 0) AS cl_SNF, ISNULL(SUM(Fat_Kg), 0) AS cl_FATKG, ISNULL(SUM(Snf_Kg), 0) AS cl_SNFKG,  ISNULL(SUM(Rate), 0) AS cl_RATE, ISNULL(SUM(Amount), 0) AS cl_AMOUNT FROM            Stock_Milk  WHERE        (Date = '" + d2 + "') AND (Plant_code = '" + plantcode + "')) AS C) AS ak LEFT OUTER JOIN  (SELECT        CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate, Plant_Code FROM            Procurement AS Procurement_1  WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (168, 171)) GROUP BY Plant_Code) AS nn ON ak.Plant_code = nn.Plant_Code");

                        // str = " select * from( SELECT  ak.Plant_code, t1.pr_MILKKG, t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT, t2.Plant_code FROM ( SELECT isnull(Sum(Milk_kg),0) as pr_MILKKG,isnull(Avg(fat),0) as pr_FAT,isnull(AVG(snf),0) as pr_SNF,isnull(sum(fat_kg),0)as pr_FATKG,isnull(sum(snf_kg),0) as pr_SNFKG,isnull(sum(rate),0) as pr_RATE,isnull(sum(AMOUNT),0) as pr_AMOUNT FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code IN (168, 171))t1,(SELECT isnull(Sum(Milkkg),0) as dp_MILKKG,isnull(Avg(fat),0) as dp_FAT,isnull(AVG(snf),0) as dp_SNF,isnull(SUM(Fat_Kg),0) as dp_FATKG,isnull(sum(Snf_Kg),0) as dp_SNFKG,isnull(sum(RATE),0) as dp_RATE,isnull(sum(Amount),0) as dp_AMOUNT, Plant_Code FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' GROUP BY Plant_Code)t2,(SELECT isnull(Sum(Milkkg),0) as op_MILKKG,isnull(Avg(fat),0) as op_FAT,isnull(AVG(snf),0) as op_SNF,isnull(SUM(Fat_Kg),0) as op_FATKG,isnull(sum(Snf_Kg),0) as op_SNFKG, isnull(sum(RATE),0) as op_RATE,isnull(sum(Amount),0) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT isnull(Sum(Milkkg),0) as cl_MILKKG,isnull(Avg(fat),0) as cl_FAT,isnull(AVG(snf),0) as cl_SNF,isnull(SUM(Fat_Kg),0) as cl_FATKG,isnull(sum(Snf_Kg),0) as cl_SNFKG,isnull( sum(RATE),0) as cl_RATE,isnull(sum(Amount),0) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C) as ak   left join   (  SELECT  convert(decimal(18,2),isnull((SUM(amount +comrate+cartageamount +splbonusamount)/SUM(milk_ltr)),0)) as Rate,Plant_Code   FROM  Procurement    WHERE prdate between  '" + d1 + "' AND '" + d2 + "' group by Plant_Code) as nn  on   ak.Plant_code=nn.Plant_Code ";
                    }
                    else if (plantcode == "166")
                    {
                        cmd = new SqlCommand("SELECT ak.pr_MILKKG, ak.pr_FAT, ak.pr_SNF, ak.pr_FATKG, ak.pr_SNFKG, ak.pr_RATE, ak.pr_AMOUNT, ak.dp_MILKKG, ak.dp_FAT, ak.dp_SNF, ak.dp_FATKG, ak.dp_SNFKG, ak.dp_RATE, ak.dp_AMOUNT, ak.op_MILKKG, ak.op_FAT,  ak.op_SNF, ak.op_FATKG, ak.op_SNFKG, ak.op_RATE, ak.op_AMOUNT, ak.cl_MILKKG, ak.cl_FAT, ak.cl_SNF, ak.cl_FATKG, ak.cl_SNFKG, ak.cl_RATE, ak.cl_AMOUNT, ak.Plant_code, ak.Rate FROM            (SELECT        t1.pr_MILKKG, t1.pr_FAT, t1.pr_SNF, t1.pr_FATKG, t1.pr_SNFKG, t1.pr_RATE, t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT, t2.dp_SNF, t2.dp_FATKG, t2.dp_SNFKG, t2.dp_RATE, t2.dp_AMOUNT, t3.op_MILKKG, t3.op_FAT,   t3.op_SNF, t3.op_FATKG, t3.op_SNFKG, t3.op_RATE, t3.op_AMOUNT, C.cl_MILKKG, C.cl_FAT, C.cl_SNF, C.cl_FATKG, C.cl_SNFKG, C.cl_RATE, C.cl_AMOUNT, t2.Plant_code, t1.Rate   FROM            (SELECT        ISNULL(SUM(Milk_kg), 0) AS pr_MILKKG, ISNULL(AVG(Fat), 0) AS pr_FAT, ISNULL(AVG(Snf), 0) AS pr_SNF, ISNULL(SUM(fat_kg), 0) AS pr_FATKG, ISNULL(SUM(snf_kg), 0) AS pr_SNFKG, ISNULL(SUM(Rate),  0) AS pr_RATE, ISNULL(SUM(Amount), 0) AS pr_AMOUNT, CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate  FROM            Procurement WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (166, 172))) AS t1 CROSS JOIN    (SELECT        Plant_code, ISNULL(SUM(MilkKg), 0) AS dp_MILKKG, ISNULL(AVG(Fat), 0) AS dp_FAT, ISNULL(AVG(Snf), 0) AS dp_SNF, ISNULL(SUM(Fat_Kg), 0) AS dp_FATKG, ISNULL(SUM(Snf_Kg), 0) AS dp_SNFKG,   ISNULL(SUM(Rate), 0) AS dp_RATE, ISNULL(SUM(Amount), 0) AS dp_AMOUNT     FROM            Despatchnew   WHERE        (Date BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_code = '" + plantcode + "') GROUP BY Plant_code) AS t2 CROSS JOIN    (SELECT        ISNULL(SUM(MilkKg), 0) AS op_MILKKG, ISNULL(AVG(Fat), 0) AS op_FAT, ISNULL(AVG(Snf), 0) AS op_SNF, ISNULL(SUM(Fat_Kg), 0) AS op_FATKG, ISNULL(SUM(Snf_Kg), 0) AS op_SNFKG,   ISNULL(SUM(Rate), 0) AS op_RATE, ISNULL(SUM(Amount), 0) AS op_AMOUNT  FROM            Stock_openingmilk  WHERE        (Datee = '" + d1 + "') AND (Plant_code = '" + plantcode + "')) AS t3 CROSS JOIN (SELECT        ISNULL(SUM(MilkKg), 0) AS cl_MILKKG, ISNULL(AVG(Fat), 0) AS cl_FAT, ISNULL(AVG(Snf), 0) AS cl_SNF, ISNULL(SUM(Fat_Kg), 0) AS cl_FATKG, ISNULL(SUM(Snf_Kg), 0) AS cl_SNFKG,  ISNULL(SUM(Rate), 0) AS cl_RATE, ISNULL(SUM(Amount), 0) AS cl_AMOUNT FROM            Stock_Milk  WHERE        (Date = '" + d2 + "') AND (Plant_code = '" + plantcode + "')) AS C) AS ak LEFT OUTER JOIN  (SELECT        CONVERT(decimal(18, 2), ISNULL(SUM(Amount + ComRate + CartageAmount + SplBonusAmount) / SUM(Milk_ltr), 0)) AS Rate, Plant_Code FROM            Procurement AS Procurement_1  WHERE        (Prdate BETWEEN '" + d1 + "' AND '" + d2 + "') AND (Plant_Code IN (166, 172)) GROUP BY Plant_Code) AS nn ON ak.Plant_code = nn.Plant_Code");
                    }
                    else
                    {
                        cmd = new SqlCommand("select * from( SELECT  t1.pcode,t1.pr_MILKKG, t1.pr_FAT,t1.pr_SNF,t1.pr_FATKG,t1.pr_SNFKG,t1.pr_RATE,t1.pr_AMOUNT, t2.dp_MILKKG, t2.dp_FAT,t2.dp_SNF,t2.dp_FATKG,t2.dp_SNFKG,t2.dp_RATE,t2.dp_AMOUNT,t3.op_MILKKG,t3.op_FAT,t3.op_SNF,t3.op_FATKG,t3.op_SNFKG,t3.op_RATE,t3.op_AMOUNT,c.cl_MILKKG,c.cl_FAT,c.cl_SNF,c.cl_FATKG,c.cl_SNFKG,c.cl_RATE,c.cl_AMOUNT FROM ( SELECT isnull(Sum(Milk_kg),0) as pr_MILKKG,isnull(Avg(fat),0) as pr_FAT,isnull(AVG(snf),0) as pr_SNF,isnull(sum(fat_kg),0)as pr_FATKG,isnull(sum(snf_kg),0) as pr_SNFKG,isnull(sum(rate),0) as pr_RATE,isnull(sum(AMOUNT),0) as pr_AMOUNT,Plant_Code as pcode FROM Procurement Where prdate between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "' group by Plant_Code)t1,(SELECT isnull(Sum(Milkkg),0) as dp_MILKKG,isnull(Avg(fat),0) as dp_FAT,isnull(AVG(snf),0) as dp_SNF,isnull(SUM(Fat_Kg),0) as dp_FATKG,isnull(sum(Snf_Kg),0) as dp_SNFKG,isnull(sum(RATE),0) as dp_RATE,isnull(sum(Amount),0) as dp_AMOUNT FROM DespatchNew Where date between  '" + d1 + "' AND '" + d2 + "' and  Plant_Code='" + plantcode + "')t2,(SELECT isnull(Sum(Milkkg),0) as op_MILKKG,isnull(Avg(fat),0) as op_FAT,isnull(AVG(snf),0) as op_SNF,isnull(SUM(Fat_Kg),0) as op_FATKG,isnull(sum(Snf_Kg),0) as op_SNFKG, isnull(sum(RATE),0) as op_RATE,isnull(sum(Amount),0) as op_AMOUNT FROM Stock_openingmilk Where datee = '" + d1 + "' and  Plant_Code='" + plantcode + "')t3,(SELECT isnull(Sum(Milkkg),0) as cl_MILKKG,isnull(Avg(fat),0) as cl_FAT,isnull(AVG(snf),0) as cl_SNF,isnull(SUM(Fat_Kg),0) as cl_FATKG,isnull(sum(Snf_Kg),0) as cl_SNFKG,isnull( sum(RATE),0) as cl_RATE,isnull(sum(Amount),0) as cl_AMOUNT FROM Stock_Milk Where date =  '" + d2 + "'  and  Plant_Code='" + plantcode + "') C) as ak   left join   (  SELECT  convert(decimal(18,2),isnull((SUM(amount +comrate+cartageamount +splbonusamount)/SUM(milk_ltr)),0)) as Rate,Plant_Code   FROM  Procurement    WHERE prdate between  '" + d1 + "' AND '" + d2 + "' group by Plant_Code) as nn  on   ak.pcode=nn.Plant_Code ");
                    }
                    DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
                    if (dtPlant.Rows.Count > 0)
                    {
                        foreach (DataRow drp in dtPlant.Rows)
                        {
                            // procurement
                            string proc_milkkg = drp["pr_MILKKG"].ToString();
                            string proc_fat = drp["pr_FAT"].ToString();
                            string proc_snf = drp["pr_SNF"].ToString();
                            string pr_FATKG = drp["pr_FATKG"].ToString();
                            string pr_SNFKG = drp["pr_SNFKG"].ToString();
                            string pr_RATE = drp["pr_RATE"].ToString();
                            string pr_AMOUNT = drp["pr_AMOUNT"].ToString();

                            //dispatch
                            string dp_MILKKG = drp["dp_MILKKG"].ToString();
                            string dp_FAT = drp["dp_FAT"].ToString();
                            string dp_SNF = drp["dp_SNF"].ToString();
                            string dp_FATKG = drp["dp_FATKG"].ToString();
                            string dp_SNFKG = drp["dp_SNFKG"].ToString();
                            string dp_RATE = drp["dp_RATE"].ToString();
                            string dp_AMOUNT = drp["dp_AMOUNT"].ToString();
                           
                            //opening
                            //string dp_SNFKG = drp["dp_SNFKG"].ToString();
                            //string dp_SNFKG = drp["dp_SNFKG"].ToString();

                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
}

