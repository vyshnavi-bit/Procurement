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
/// <summary>
/// Summary description for DALImportDB
/// </summary>
public class DALImportDB
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    
	public DALImportDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void deleteimport(BOLRateChart rateBO, string sql)
    {
        try
        {
            using (con = DBaccess.GetConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 200;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlParameter p, c, fd, td;
                p = cmd.Parameters.Add("@plantcode", SqlDbType.VarChar, 50);
                c = cmd.Parameters.Add("@companycode", SqlDbType.VarChar, 50);
                fd = cmd.Parameters.Add("@frdate", SqlDbType.Date);
                td = cmd.Parameters.Add("@todate", SqlDbType.Date);
                p.Value = rateBO.Plantcode;
                c.Value = rateBO.Companycode;
                fd.Value = rateBO.Fromdate;
                td.Value = rateBO.Todate;
                cmd.ExecuteNonQuery();

            }
        }
        catch (Exception ex)
        {
        }
    }
    public void importsess(BOLRateChart rateBO, string sql)
    {
        try
        {
            using (con = DBaccess.GetConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 350;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlParameter p, c, fd, td, ses;
                p = cmd.Parameters.Add("@plantcode", SqlDbType.VarChar, 50);
                c = cmd.Parameters.Add("@companycode", SqlDbType.VarChar, 50);
                fd = cmd.Parameters.Add("@frdate", SqlDbType.Date);
                td = cmd.Parameters.Add("@todate", SqlDbType.Date);
                ses = cmd.Parameters.Add("@sess", SqlDbType.VarChar, 15);

                p.Value = rateBO.Plantcode;
                c.Value = rateBO.Companycode;
                fd.Value = rateBO.Fromdate;
                td.Value = rateBO.Todate;
                ses.Value = rateBO.Sess;

                cmd.ExecuteNonQuery();

            }
        }
        catch (Exception ex)
        {
        }

    }


}