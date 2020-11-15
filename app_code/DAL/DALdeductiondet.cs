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



  public class DALdeductiondet : DbHelper
    {

        DbHelper DBaccess = new DbHelper();
        SqlConnection con = new SqlConnection();
        BOLdeductiondet loanBO = new BOLdeductiondet();
        public DALdeductiondet()
        {
        }
        public void insertloan(BOLdeductiondet loanBO,string sql)
        {
            using (con = DBaccess.GetConnection())
            {
                string mess = string.Empty;
                int ress = 0;

                SqlCommand cmd = new SqlCommand();
                SqlParameter pcompanycode, pplantcode, proute_id, pagent_id, pbilladv, pmedicineadv, pfeedadv, pcanadv, precovery, pothers, pdedudate, pfrmdate, ptodate, pmess, press,prefid;
                //SqlParameter parloanid, paragentid, pardeddate, parbiladv,parlamount, parcanamnt, parfeedadv, parothers, parfrdate, partodate;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;


                pcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
                pplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int); 
           
                pfrmdate = cmd.Parameters.Add("@frdate", SqlDbType.DateTime);
                ptodate = cmd.Parameters.Add("@todate", SqlDbType.DateTime);
                proute_id = cmd.Parameters.Add("@route_id", SqlDbType.Int);        
                pagent_id = cmd.Parameters.Add("@agent_id", SqlDbType.Int);              
                pbilladv = cmd.Parameters.Add("@billadvance", SqlDbType.Money);
                pmedicineadv = cmd.Parameters.Add("@medicineadv", SqlDbType.Money);
                pfeedadv = cmd.Parameters.Add("@feedadv", SqlDbType.Money);
                pcanadv = cmd.Parameters.Add("@canadv", SqlDbType.Money);
                precovery=cmd.Parameters.Add("@recovery",SqlDbType.Money);
                pothers = cmd.Parameters.Add("@other", SqlDbType.Money);
                pdedudate = cmd.Parameters.Add("@deductiondate", SqlDbType.DateTime);
                prefid = cmd.Parameters.Add("@refid", SqlDbType.NVarChar,25);

                pmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 100);
                cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
                press = cmd.Parameters.Add("@ress",SqlDbType.Int);
                cmd.Parameters["@ress"].Direction = ParameterDirection.Output;



                pcompanycode.Value = loanBO.Companycode;
                pplantcode.Value = loanBO.Plantcode;

                pfrmdate.Value = loanBO.Frdate.ToShortDateString();
                ptodate.Value = loanBO.Todate.ToShortDateString();
                proute_id.Value = loanBO.Routeid;
                pagent_id.Value = loanBO.Agentid;               
                pbilladv.Value = loanBO.Billadvance;
                pmedicineadv.Value = loanBO.Ai;
                pfeedadv.Value = loanBO.Feed;
                pcanadv.Value = loanBO.Can;
                precovery.Value = loanBO.Recovery;
                pothers.Value = loanBO.others;
                pdedudate.Value = loanBO.DeductionDate.ToShortDateString();
                prefid.Value = loanBO.Referencedate;

                cmd.ExecuteNonQuery();
                mess = (string)cmd.Parameters["@mess"].Value;
                ress = (int)cmd.Parameters["@ress"].Value;
                
                if (ress == 1)
                {
                   // WebMsgBox.Show("" + mess);
                }
                else
                {
                   // WebMsgBox.Show("" + mess);
                }

            }
        }
    }

