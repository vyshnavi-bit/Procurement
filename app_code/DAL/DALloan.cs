using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


public class DALloan : DbHelper
    {
        DbHelper DBaccess = new DbHelper();
        SqlConnection con = new SqlConnection();
        
        public DALloan()
        {

        }
        public void insertloan(BOLloan loanBO, string sql)
        {
            using (con = DBaccess.GetConnection())
            {
                string mess = string.Empty;
                int ress = 0;
                SqlCommand cmd = new SqlCommand();
                SqlParameter parcompanycode, parplantcode, parloanid, paragentid, parroutid, parloandate, parexpdate, parloanamnt, parbal, parinst, pardest, parmess, parress, parInterestAmount, parNoofinstallment, ParUid;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                parcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
                parplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);
                parloandate = cmd.Parameters.Add("@loandate", SqlDbType.DateTime);
                parexpdate = cmd.Parameters.Add("@expdate", SqlDbType.DateTime);
                parroutid = cmd.Parameters.Add("@route_id", SqlDbType.Int);
                paragentid = cmd.Parameters.Add("@agentid", SqlDbType.Int);
                parloanid = cmd.Parameters.Add("@loan_id", SqlDbType.Int);
                parloanamnt = cmd.Parameters.Add("@loanamnt", SqlDbType.Money);
                parbal = cmd.Parameters.Add("@balance", SqlDbType.Money);
                parinst = cmd.Parameters.Add("@inst_amount", SqlDbType.Money);
                pardest = cmd.Parameters.Add("@description", SqlDbType.NVarChar, 100);
                parInterestAmount = cmd.Parameters.Add("@InterestAmount", SqlDbType.Money);
                parNoofinstallment = cmd.Parameters.Add("@Noofinstallment", SqlDbType.Int);
                ParUid = cmd.Parameters.Add("@UserId", SqlDbType.Int);

                parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 50);
                cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
                parress = cmd.Parameters.Add("@ress", SqlDbType.Int);
                cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

                parcompanycode.Value = loanBO.Companycode;
                parplantcode.Value = loanBO.Plantcode;
                parloandate.Value = loanBO.Loandate;
                parexpdate.Value = loanBO.Expiredate;
                parroutid.Value = loanBO.Route_id;
                paragentid.Value = loanBO.agent_id;
                parloanid.Value = loanBO.Loan_id;
                parloanamnt.Value = loanBO.Loanamount;
                parbal.Value = loanBO.Balance;
                parinst.Value = loanBO.Instamnt;
                pardest.Value = loanBO.Desc;
                parInterestAmount.Value = loanBO.InterestAmount;
                pardest.Value = loanBO.Desc;
                parNoofinstallment.Value = loanBO.NoofInstallment;
                ParUid.Value = loanBO.UserId;


                cmd.ExecuteNonQuery();
                mess = (string)cmd.Parameters["@mess"].Value;
                ress = (int)cmd.Parameters["@ress"].Value;

                //if (ress == 1)
                //{
                //    WebMsgBox.Show(mess.ToString());
                //}
                //else
                //{

                //    WebMsgBox.Show(mess.ToString());
                //}


            }
        }

    }
