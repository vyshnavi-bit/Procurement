using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for DALVoucher
/// </summary>
public class DALVoucher
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();

	public DALVoucher()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string InsertVoucherDetails(BOLVoucher bovoucher,string sql)
    {
        using (con = DBaccess.GetConnection())
        {

            int result = 0;
            string message = string.Empty;
            SqlCommand cmd = new SqlCommand();
            SqlParameter cmpcode, plantcode, rid, agentid, cleardate, inwarddate, shift, mltr, fat, snf, clr, rate, amount, remarks, pmess, press;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            cmpcode = cmd.Parameters.Add("@cmpcode", SqlDbType.Int);
            plantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);
            rid = cmd.Parameters.Add("@rid", SqlDbType.Int);
            agentid = cmd.Parameters.Add("@agentid", SqlDbType.Int);
            cleardate = cmd.Parameters.Add("@cleardate", SqlDbType.DateTime);
            inwarddate = cmd.Parameters.Add("@inwarddate", SqlDbType.DateTime);
            shift = cmd.Parameters.Add("@shift", SqlDbType.NVarChar, 5);
            mltr = cmd.Parameters.Add("@mltr", SqlDbType.Float);
            fat = cmd.Parameters.Add("@fat", SqlDbType.Float);
            snf = cmd.Parameters.Add("@snf", SqlDbType.Float);
            clr = cmd.Parameters.Add("@clr", SqlDbType.Float);
            rate = cmd.Parameters.Add("@rate", SqlDbType.Float);
            amount = cmd.Parameters.Add("@amount", SqlDbType.Float);
            remarks = cmd.Parameters.Add("@remarks", SqlDbType.NVarChar, 250);

            pmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 500);
            cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
            press = cmd.Parameters.Add("@ress", SqlDbType.Int);
            cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

            cmpcode.Value = bovoucher.Companycode;
            plantcode.Value = bovoucher.Plantcode;
            rid.Value = bovoucher.Routeid;
            agentid.Value = bovoucher.Agentid;
            cleardate.Value = bovoucher.Clearingdate;
            inwarddate.Value = bovoucher.Inwarddate;
            shift.Value = bovoucher.Shift;
            mltr.Value = bovoucher.Mlrt;
            fat.Value = bovoucher.Fat;
            snf.Value = bovoucher.Snf;
            clr.Value = bovoucher.Clr;
            rate.Value = bovoucher.Rate;
            amount.Value = bovoucher.Amount;
            remarks.Value = bovoucher.Remarks;

            cmd.ExecuteNonQuery();

            message = (string)cmd.Parameters["@mess"].Value;
            result = (int)cmd.Parameters["@ress"].Value;

            if (result == 1)
            {
                //WebMsgBox.Show(message);
                return message;
            }
            else
            {
                //WebMsgBox.Show(message);
                return message;
            }
            

        }
    }
}