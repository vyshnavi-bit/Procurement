using System;
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
/// Summary description for DALbillgenerate
/// </summary>
public class DALbillgenerate
{
    DbHelper DBaccess = new DbHelper();
    BOLbillgenerate BOLbill = new BOLbillgenerate();
    SqlConnection con = new SqlConnection();

	public DALbillgenerate()
	{
	}
    public void UpdateLoanbillgenerate(BOLbillgenerate BOLbill, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            string mess = string.Empty;
            int ress = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter spcompanycode, spplantcode, sproute_id, spfrmdate, sptodate, spmess, spress;
            
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            
            spcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
            spplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);
            sproute_id = cmd.Parameters.Add("@route_id", SqlDbType.Int);
            spfrmdate = cmd.Parameters.Add("@frdate", SqlDbType.DateTime);
            sptodate = cmd.Parameters.Add("@todate", SqlDbType.DateTime);

            spmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 100);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            spress = cmd.Parameters.Add("@ress", SqlDbType.Int);
            cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

            spcompanycode.Value = BOLbill.Companycode;
            spplantcode.Value = BOLbill.Plantcode;
            sproute_id.Value = BOLbill.Route_id;
            spfrmdate.Value = BOLbill.Frmdate.ToShortDateString();
            sptodate.Value = BOLbill.Todate.ToShortDateString();           
           

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



    public void UpdateLoanbillgenerateadmin(BOLbillgenerate BOLbill, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            string mess = string.Empty;
            int ress = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter spcompanycode, spplantcode, sproute_id, spfrmdate, sptodate, spmess, spress;

            cmd.CommandText = sql;
            cmd.CommandTimeout = 400;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            spcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
            spplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);
            sproute_id = cmd.Parameters.Add("@route_id", SqlDbType.Int);
            spfrmdate = cmd.Parameters.Add("@frdate", SqlDbType.DateTime);
            sptodate = cmd.Parameters.Add("@todate", SqlDbType.DateTime);

            spmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 100);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            spress = cmd.Parameters.Add("@ress", SqlDbType.Int);
            cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

            spcompanycode.Value = BOLbill.Companycode;
            spplantcode.Value = BOLbill.Plantcode;
            sproute_id.Value = BOLbill.Route_id;
            spfrmdate.Value = BOLbill.Frmdate.ToShortDateString();
            sptodate.Value = BOLbill.Todate.ToShortDateString();


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

    public void UpdateDeductionbillgenerate(BOLbillgenerate BOLbill, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            string mess = string.Empty;
            int ress = 0;
            SqlCommand cmd = new SqlCommand();
            SqlParameter spcompanycode, spplantcode, spfrmdate, sptodate, spmess, spress;

            cmd.CommandText = sql;
            cmd.CommandTimeout = 400;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            spcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
            spplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);
            spfrmdate = cmd.Parameters.Add("@frdate", SqlDbType.DateTime);
            sptodate = cmd.Parameters.Add("@todate", SqlDbType.DateTime);

            spmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 100);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            spress = cmd.Parameters.Add("@ress", SqlDbType.Int);
            cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

            spcompanycode.Value = BOLbill.Companycode;
            spplantcode.Value = BOLbill.Plantcode;
            spfrmdate.Value = BOLbill.Frmdate.ToShortDateString();
            sptodate.Value = BOLbill.Todate.ToShortDateString();


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
