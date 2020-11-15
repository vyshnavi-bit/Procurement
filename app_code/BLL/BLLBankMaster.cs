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
/// Summary description for BLLBankMaster
/// </summary>
public class BLLBankMaster
{
    DALBankMaster bankmasterDA = new DALBankMaster();
    string sqlstr;
	public BLLBankMaster()
	{
        sqlstr = string.Empty;
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertbankmaster(BOLBankMaster bankmasterBO)
    {
        //Stored procudure Name
        string sql = "Insert_Bankmaster";
        bankmasterDA.insertBankmaster(bankmasterBO, sql);
    }
    public DataTable getBankmastertable()
    {
        sqlstr = "select * from Bank_Master ";
        DataTable dt = new DataTable();
        dt = bankmasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public SqlDataReader getbankmasterdatareader(string str)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * from Bank_Master where Bank_ID=" + str + "";
        dr = bankmasterDA.GetDatareader(sqlstr);
        return dr;

    }
    public SqlDataReader getbankmasterdatareader1(string st)
    {

        SqlDataReader dr;
        sqlstr = "select * from Bank_Master where Plant_code='" + st + "' AND Company_code='" + st + "' ORDER BY Bank_ID DESC";
        dr = bankmasterDA.GetDatareader(sqlstr);
        return dr;
    }

    public SqlDataReader GetBankIfscode(string ccode ,string pcode,string bankid)
    {
        SqlDataReader dr;
        sqlstr = "SELECT Ifsc_code,Branch_Name FROM BANK_MASTER Where Company_code='" + ccode + "' AND plant_code='" + pcode + "' AND Bank_id='" + bankid + "' order By Branch_Name";
        dr = bankmasterDA.GetDatareader(sqlstr);
        return dr;
    }

}
