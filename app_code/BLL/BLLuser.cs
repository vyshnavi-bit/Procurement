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
/// Summary description for BLLuser
/// </summary>
public class BLLuser
{
    BOLuser userBO = new BOLuser();
    DbHelper dbaccess = new DbHelper();
    DALuser userDA = new DALuser();
    public BLLuser()
    {
    }
    public void insertuser(BOLuser userBO)
    {
        string sql = "Insert_Adduser";
        userDA.insertUser(userBO, sql);
    }
    public void edituser(BOLuser userBO)
    {
        string sql = "Edit_Adduser";
        userDA.editUser(userBO, sql);
    }
    public string getmaxuserid()
    {
        string userid;
        string sqlstr = "SELECT MAX(user_Id) FROM users";
        userid = userDA.ExecuteScalarstr(sqlstr);
        return userid;
    }
    public DataTable getusertable()
    {
        string sql = "SELECT * FROM users";
        DataTable dt = new DataTable();
        dt = userDA.GetDatatable(sql);
        return dt;
    }
    public SqlDataReader getuserdatareader()
    {
        string sql = "SELECT * FROM users WHERE role='staff'";
        return userDA.GetDatareader(sql);
    }
    public SqlDataReader getrloesdatareader()
    {
        SqlDataReader dr;
        string sqlstr = "SELECT * FROM role";
        dr = userDA.GetDatareader(sqlstr);
        return dr;

    }
    public void deleteuser(string id, int pcode, int ccode)
    {
        string sqlstr = "DELETE FROM add_user WHERE Users_ID ='" + id + "' and Company_Id='" + ccode + "' and plant_Id='" + pcode + "'";
        userDA.deleteUser(sqlstr);
    }

    public SqlDataReader Loadcompanycode()
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT Company_Code,Company_Name FROM Company_Master ";
        dr = userDA.GetDatareader(sqlstr);
        return dr;
    }

    public SqlDataReader LoadPlantcode(string ccode)
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT Plant_Code,Plant_Name,Mana_PhoneNo FROM Plant_Master WHERE Company_Code='" + ccode + "'   ORDER BY Plant_Code";
        dr = userDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader GetPlantAllottedAmount(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT t1.*,t2.*,ISNULL((ISNULL(t1.TotAllotAmount,0)-ISNULL(t2.TotPaymentAmount,0)),0) AS TotAmount FROM (SELECT plant_code,ISNULL(SUM(Amount),0) AS TotAllotAmount FROM AdminAmountAllottoplant Where Plant_code='" + pcode + "' AND  Amount>0 GROUP By plant_code)AS t1 LEFT JOIN (SELECT ISNULL(plant_code,0) AS Bpcode,ISNUll(SUM(NetAmount),0) AS TotPaymentAmount  FROM BankPaymentllotment Where Plant_Code='" + pcode + "'  GROUP By plant_code) AS t2 ON t1.plant_code=t2.Bpcode ";
        dr = userDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader LoadPlantcode1(string ccode,int pcode)
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_code='" + pcode + "' ORDER BY Plant_Code";
        dr = userDA.GetDatareader(sqlstr);
        return dr;

    }
    public SqlDataReader LoadSinglePlantcode(string ccode,string pcode)
    {
        SqlDataReader dr = null;
        string sqlstr = "";
        if (pcode == "168")
        {
            sqlstr = "SELECT Plant_Code,Plant_Name,Mana_PhoneNo FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_code IN (168,171) ORDER BY Plant_Code";
        }
        else if (pcode == "166")
        {
            sqlstr = "SELECT Plant_Code,Plant_Name,Mana_PhoneNo FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_code IN (166,172) ORDER BY Plant_Code";
        }
        else
        {
            sqlstr = "SELECT Plant_Code,Plant_Name,Mana_PhoneNo FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Plant_code='" + pcode + "' ORDER BY Plant_Code";
        }
        dr = userDA.GetDatareader(sqlstr);
        return dr;

    }
    
    public SqlDataReader Adddate(string ccode,string pcode,string d1,string d2)
    {
        SqlDataReader dr = null;
        string sqlstr = "Select DISTINCT(CONVERT(NVARCHAR(35),added_date,103)) As adddate from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + d1 + "' AND billtodate='" + d2 + "' ORDER BY adddate";
        dr = userDA.GetDatareader(sqlstr);
        return dr;

    }
    public DataTable LoaduserGriddata(int pcode, int ccode)
    {
        DataTable dt = new DataTable();

        string Sqlstr = "select Plant_Id,Users_ID,Roles,First_Name,Last_Name,Password from add_user where Company_Id='" + ccode + "' and plant_Id='" + pcode + "'";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }
    public int Remarksstatus(string ccode,string pcode,string d1,string d2)
    {
        int satus;
        string sqlstr = "SELECT Count(Remarkstatus) FROM ProcurementImport where Prdate between '" + d1.ToString() + "' and '" + d2.ToString() + "' and Plant_Code='" + pcode + "' And company_code='" + ccode + "' and Remarkstatus=1";
        satus = userDA.ExecuteScalarint(sqlstr);
        return satus;
    }
    public int Remarksstatussess(string ccode, string pcode, string d1, string d2,string sess)
    {
        int satus;
        string sqlstr = "SELECT Count(Remarkstatus) FROM ProcurementImport where Prdate between '" + d1.ToString() + "' and '" + d2.ToString() + "' and Plant_Code='" + pcode + "' And company_code='" + ccode + "' and Sessions='" + sess + "' and Remarkstatus=1";
        satus = userDA.ExecuteScalarint(sqlstr);
        return satus;
    }

    public int actualRemarksstatussess(string ccode, string pcode, string d1, string d2, string sess)
    {
        int satus;
        string sqlstr = "SELECT Count(Remarkstatus) FROM ProcurementImport where Prdate between '" + d1.ToString() + "' and '" + d2.ToString() + "' and Plant_Code='" + pcode + "' And company_code='" + ccode + "' and Sessions='" + sess + "' and Remarkstatus=10";
        satus = userDA.ExecuteScalarint(sqlstr);
        return satus;
    }




    public int DatatransferCheck(string ccode, string pcode, string d1, string d2)
    {
        int satus;
        string sqlstr = "SELECT Count(Tid) FROM Procurement where Prdate between '" + d1.ToString() + "' and '" + d2.ToString() + "' and Plant_Code='" + pcode + "' And company_code='" + ccode + "' ";
        satus = userDA.ExecuteScalarint(sqlstr);
        return satus;
    }
    public int DatatransferChecksess(string ccode, string pcode, string d1, string d2, string sess)
    {
        int satus;
        string sqlstr = "SELECT Count(Tid) FROM Procurement where Prdate between '" + d1.ToString() + "' and '" + d2.ToString() + "' and Plant_Code='" + pcode + "' And company_code='" + ccode + "' AND Sessions='" + sess + "' ";
        satus = userDA.ExecuteScalarint(sqlstr);
        return satus;
    }

    public int actualDatatransferChecksess(string ccode, string pcode, string d1, string d2, string sess)
    {
        int satus;
        string sqlstr = "SELECT Count(Tid) FROM ccProcurement where Prdate between '" + d1.ToString() + "' and '" + d2.ToString() + "' and Plant_Code='" + pcode + "' And company_code='" + ccode + "' AND Sessions='" + sess + "' ";
        satus = userDA.ExecuteScalarint(sqlstr);
        return satus;
    }


    public SqlDataReader getmilkkg(string fdate, string tdate, string pcode)
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT    sum(Milk_Kg) as Kg,avg(fat) as fat,avg(snf) as snf  FROM PROCUREMENT WHERE   plant_code='" + pcode + "'       AND PRDATE BETWEEN '" + fdate + "' AND '" + tdate + "' ";
        dr = userDA.GetDatareader(sqlstr);
        return dr;

    }

}