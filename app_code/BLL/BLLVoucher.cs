using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLVoucher
/// </summary>
public class BLLVoucher
{
    DataTable dt = new DataTable();
    DALVoucher davoucher = new DALVoucher();
    DbHelper dbaccess = new DbHelper();
    public string sql, mes;
	public BLLVoucher()
	{
        sql = string.Empty;
        mes = string.Empty;
	}
    public string InsertVoucherDetails(BOLVoucher bocoucher)
    {
        sql = string.Empty;
        mes = string.Empty;
        sql = "Insert_VoucherDetails";
        mes = davoucher.InsertVoucherDetails(bocoucher, sql);
        return mes;
    }
    public DataTable GetVoucherGriddata(string ccode, string pcode)
    {
        dt = null;
        sql = string.Empty;
		sql = "SELECT Ref_Id,Plant_code,Route_id,agent_id,Clearing_Date,Inward_Date,Sess,CAST(Milk_Ltr AS DECIMAL(18,2)) AS Milk_Ltr,CAST(Fat AS DECIMAL(18,2)) AS Fat,CAST(Snf AS DECIMAL(18,2)) AS Snf,CAST(Rate AS DECIMAL(18,2)) AS Rate,CAST(Amount AS DECIMAL(18,2)) AS Amount FROM Voucher_Clear WHERE Company_code='" + ccode + "' AND Plant_code='" + pcode + "' order by  Ref_Id desc ";
        dt = dbaccess.GetDatatable(sql);
        return dt;
    }
}