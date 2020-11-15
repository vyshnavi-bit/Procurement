using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLLLoginfo
/// </summary>
public class BLLLoginfo
{


    DbHelper dbaccess = new DbHelper();
    DALLoginfo loginfoDA = new DALLoginfo();
    string sqlstr;
	public BLLLoginfo()
	{

        sqlstr = string.Empty;
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertloginfo(BOLLoinfo loginfoBO)
    {

        string sql = "Insert_Loginfo";
        loginfoDA.InsertLoginfo(loginfoBO,sql);

    }
    public void editloginfo(BOLLoinfo loginfoBO)
    {

        string sql = "Edit_Loginfo";
        loginfoDA.EditLoginfo(loginfoBO, sql);

    }
    public int GetNoofuser(string ccode,string pcode)
    {
         int userno = 0;
        try
        {           
            string sqlstr = "SELECT COUNT(*) AS NOOFUSER FROM USERIDINFO WHERE COMPANY_ID='" + ccode + "' AND PLANT_ID='" + pcode + "'";
            userno = dbaccess.ExecuteScalarint(sqlstr);
            userno = ++userno;
        }
        catch(Exception ex)
        {
            ex.ToString();
        }
        return userno;

    }
    public void deleteinfo(string id, int pcode, int ccode)
    {
        string sqlstr = "DELETE FROM UserIDInfo WHERE User_LoginId ='" + id + "' and Company_Id='" + ccode + "' and plant_Id='" + pcode + "'";
        loginfoDA.deleteInfo(sqlstr);
    }
}
