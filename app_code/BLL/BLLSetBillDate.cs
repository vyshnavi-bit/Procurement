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
/// Summary description for BLLSetBillDate
/// </summary>
public class BLLSetBillDate
{
    DbHelper dbaccess = new DbHelper();
    DataTable dt = new DataTable();
    DALSetBillDate Dalsbilldate = new DALSetBillDate();
    string sqlstr;
    public BLLSetBillDate()
    {
        sqlstr = string.Empty;
        dt = null;
    }

    public DataTable LoadBillDate(string ccode, string pcode)
    {
        //,[Descriptions]
        // sqlstr = "SELECT  [Tid] , CONVERT(VARCHAR(50), [Bill_frmdate],103) AS Bill_frmdate,CONVERT(VARCHAR(50), [Bill_todate],103) AS [Bill_todate],[UpdateStatus] , [ViewStatus],[Status] FROM [Bill_date] WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ORDER BY TId DESC";
        sqlstr = "SELECT TOP 50  [Tid] ,CONVERT(VARCHAR(50), [Bill_frmdate],103) AS Bill_frmdate,CONVERT(VARCHAR(50), [Bill_todate],103) AS [Bill_todate],[Descriptions], [UpdateStatus] , [ViewStatus],[CurrentPaymentFlag] AS [PaymentFlag],[Status] FROM [Bill_date] WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ORDER BY TId DESC";
        dt = dbaccess.GetDatatable(sqlstr);
        return dt;
    }

    public void EditUpdateSetBill(BOLSetBillDate SetbillBOL)
    {
        sqlstr = "Update_EditSetBilldate";
        Dalsbilldate.EditUpdateSetBill1(SetbillBOL, sqlstr);
    }

}