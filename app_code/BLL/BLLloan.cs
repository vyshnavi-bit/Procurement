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

 public  class BLLloan
    {
        DALloan loanDA = new DALloan();
        
        public BLLloan()
        {

        }
        public int getloanid()
        {
            int lno = 100;
            try
            {
                string sqlstr = "SELECT MAX(loan_Id) FROM loan_details";
                lno = (int)loanDA.ExecuteScalarint(sqlstr);
                lno = ++lno;
                return lno;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lno;
        }
        public DataTable getloandetailstable()
        {
            string sql = "select * from loan_details";
            DataTable dt = new DataTable();
            dt = loanDA.GetDatatable(sql);
            return dt;
        }
        public void insertloandetails(BOLloan loanBO)
        {
            string sql = "insert_loandetails";
            loanDA.insertloan(loanBO,sql);
        }

     //WEB

        public string GetLoanAgentname(string comcode,string plantcode,string rid, string aid)
        {
            try
            {
                //string Sqlstr = "SELECT Agent_Name FROM AGENT_MASTER WHERE Route_id='" + Rid + "' AND Agent_id='" + aid + "'";
                //string Sqlstr = "SELECT Agent_Name FROM AGENT_MASTER WHERE Agent_id='" + aid + "'";
                //string Sqlstr = "SELECT Agent_Name FROM (SELECT Agent_Name,Agent_id AS aid FROM AGENT_MASTER WHERE Company_code='" + comcode + "' AND plant_code='" + plantcode + "' AND Route_id='" + rid + "' AND Agent_id='" + aid + "')AS t1 LEFT JOIN (SELECT * FROM loanDetails WHERE Company_code='" + comcode + "' AND plant_code='" + plantcode + "' AND Route_id='" + rid + "' AND Agent_id='" + aid + "' AND balance<=0  )AS t2 ON t1.aid=t2.Agent_id";
                string Sqlstr = "SELECT Agent_Name AS aid FROM AGENT_MASTER WHERE Company_code='" + comcode + "' AND plant_code='" + plantcode + "' AND Route_id='" + rid + "' AND Agent_id='" + aid + "'";
                string AgentName = loanDA.ExecuteScalarstr(Sqlstr);
                return AgentName;
            }
            catch (Exception ex)
            {
               return ex.ToString();
            }

        }
        public string GetLoanid(string ccode,string pcode)
        {
            string sqlstr = string.Empty;
            sqlstr = "SELECT MAX(Loan_Id) FROM LoanDetails WHERE company_code='" + ccode + "' AND plant_code='" + pcode + "' ";
            string sno = loanDA.ExecuteScalarstr(sqlstr);
            int n = 0;
            if (!(string.IsNullOrEmpty(sno)))
                n = int.Parse(sno);
            n++;
            return n.ToString();
        }

        public SqlDataReader Getloanagentid(string comcode,string plantcode,string rid, string aid)
        {
            SqlDataReader dr;
            string Sqlstr = "SELECT Agent_id FROM Loandetails  WHERE Company_code='" + comcode + "' AND plant_code='" + plantcode + "' AND Route_id='" + rid + "' AND Agent_id='" + aid + "' AND Balance>0 ";
            dr = loanDA.GetDatareader(Sqlstr);
            return dr;
        }


    }

