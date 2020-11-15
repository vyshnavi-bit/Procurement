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
/// Summary description for Bllbillgenerate
/// </summary>
public class Bllbillgenerate
{
   
    DALbillgenerate DALbill = new DALbillgenerate();
    DbHelper dbaccess = new DbHelper();


	public Bllbillgenerate()
	{
		
	}

    public void GetLoandetailsForLoanDeduct(BOLbillgenerate BOLbill)
    {
        string sqlstr = "update_Loanamount";
        DALbill.UpdateLoanbillgenerate(BOLbill, sqlstr);
    }

    public void GetLoandetailsForLoanDeductadmin(BOLbillgenerate BOLbill)
    {
        string sqlstr = "update_Loanamount";
        DALbill.UpdateLoanbillgenerateadmin(BOLbill, sqlstr);
    }

    public void GetDeductionForDeductionDeduct(BOLbillgenerate BOLbill)
    {
        string sqlstr = "Update_Deduction";
        DALbill.UpdateDeductionbillgenerate(BOLbill, sqlstr);
    }

    public SqlDataReader Billdate(string ccode,string pcode)
    {
        SqlDataReader dr;
        string sqlstr = string.Empty;
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND ViewStatus='0' ";
        dr = dbaccess.GetDatareader(sqlstr); 
        return dr;
    }
    //public SqlDataReader Deductiondate(string ccode, string pcode)
    //{
    //    SqlDataReader dr;
    //    string sqlstr = string.Empty;
    //    sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Status=0 ";
    //    dr = dbaccess.GetDatareader(sqlstr);
    //    return dr;
    //}
}
