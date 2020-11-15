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
/// Summary description for DALSetBillDate
/// </summary>
public class DALSetBillDate
{
    int i = 0;
    SqlConnection con = new SqlConnection();
    DbHelper dbaccess = new DbHelper();
	public DALSetBillDate()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void EditUpdateSetBill1(BOLSetBillDate setbillBOL, string sql)
    {
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter ptid, pcompanycode, pplantcode, pfrmdate, paymentflag, ptodate, pupdate, pview, pstatus, pdescrip;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;
            int inser1;

            ptid = cmd.Parameters.Add("@tid", SqlDbType.Int);   
            pcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.NVarChar, 30);
            pplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.NVarChar, 30);
            pfrmdate = cmd.Parameters.Add("@frmdate", SqlDbType.DateTime);
            ptodate = cmd.Parameters.Add("@todate", SqlDbType.DateTime);
            paymentflag = cmd.Parameters.Add("@paymetflag", SqlDbType.Int);
            pupdate = cmd.Parameters.Add("@update",SqlDbType.Int);
            pview = cmd.Parameters.Add("@view ", SqlDbType.Int);
            pstatus = cmd.Parameters.Add("@status ", SqlDbType.Int);
            pdescrip = cmd.Parameters.Add("@descrip", SqlDbType.NVarChar, 100);
            

            ptid.Value = setbillBOL.Tid;
            pcompanycode.Value = setbillBOL.Companycode;
            pplantcode.Value = setbillBOL.Plantcode;
            pfrmdate.Value = setbillBOL.Billfromdate;
            ptodate.Value = setbillBOL.Billtodate;
            paymentflag.Value = setbillBOL.PaymentFlag;
            pupdate.Value = setbillBOL.Updatestatus;
            pview.Value = setbillBOL.Viewstatus;
            pstatus.Value = setbillBOL.Status;
            pdescrip.Value = setbillBOL.Descriptions;
            inser1 = cmd.ExecuteNonQuery();
            if (inser1 == 1)
            {
                i++;
            }
            else
            {
                i++;
            }
        }
    }

}