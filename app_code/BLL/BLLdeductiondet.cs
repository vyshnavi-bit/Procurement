using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;

  public  class BLLdeductiondet
    {
        DALdeductiondet DAdedu = new DALdeductiondet();
        BOLdeductiondet BOdedu = new BOLdeductiondet();
       
        public BLLdeductiondet()
        {

        }
       
        public void deleteloan()
        {
          //  dalloan.deleteLoan();
        }
        public void updateloan()
        {
          //  dalloan.updateLoan();
        }
        public int getloanid()
        {
            int lno = 1;
            try
            {
                string sqlstr = "SELECT MAX(loan_Id) FROM loan_details";
                lno = (int)DAdedu.ExecuteScalarint(sqlstr);
                lno = ++lno;
                return lno;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lno;
        }
        public DataTable getloandetailstable(string fromdate,string todate)
        {
            string sql = "SELECT agent_code,loanagent_id as producer ,billadvance,medicineadv,feedadv,canadv as recovery,other,tid FROM deduction_details WHERE deductiondate BETWEEN '"+ fromdate +"' AND '"+ todate +"' order By agent_code";
            DataTable dt = new DataTable();
            dt = DAdedu.GetDatatable(sql);
            return dt;
        }

      //web

        public void insertloan(BOLdeductiondet BOdedu)
        {
            string sql = "insert_Deductiondetails";
            DAdedu.insertloan(BOdedu, sql);
        }

        public string GetDeductionAgentname(string comcode, string plantcode, string rid, string aid)
        {
            try
            {                
                string Sqlstr = "SELECT Agent_Name AS aid FROM AGENT_MASTER WHERE Company_code='" + comcode + "' AND plant_code='" + plantcode + "' AND Route_id='" + rid + "' AND Agent_id='" + aid + "'";
                string AgentName = DAdedu.ExecuteScalarstr(Sqlstr);
                return AgentName;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
