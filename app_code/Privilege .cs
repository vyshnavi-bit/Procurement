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
/// Summary description for Privilege
/// </summary>
public static class Privilege
{
    private static int _adminhome;
    private static int _companyhome;
    private static int _userhome;
    private static int _addcompany;
    private static int _adduser;
    private static int _registerationpage;
    //private static int _displayratechart;
    //private static int _importratechart;

    //private static int _registerbill;
    //private static int _customerbillcopy;
    //private static int _customerwisesummary;
    //private static int _customerwisepaymentsummary;
    //private static int _salesentry;
    //public Privilege()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
    public static void setprivilage()
    {
        DbHelper dbaccess = new DbHelper();
        SqlDataReader dr;
        dr = dbaccess.GetDatareader("SELECT * FROM privilege");
        while (dr.Read())
        {
            Privilege.adminHome = Convert.ToInt32(dr[0]);
            Privilege.companyHome = Convert.ToInt32(dr[1]);
            Privilege.userHome = Convert.ToInt32(dr[2]);
            Privilege.addCompany = Convert.ToInt32(dr[3]);
            Privilege.addUser = Convert.ToInt32(dr[4]);
            Privilege.registerationPage = Convert.ToInt32(dr[5]);
            //Privilege.displayRatechart = Convert.ToInt32(dr[6]);
            //Privilege.importRatechart = Convert.ToInt32(dr[7]);
            //Privilege.customerwiseSummary = Convert.ToInt32(dr[8]);
            //Privilege.customerwisePaysummary = Convert.ToInt32(dr[9]);
            //Privilege.billcopy = Convert.ToInt32(dr[10]);
            //Privilege._registerbill = Convert.ToInt32(dr[11]);
        }

    }
    public static int adminHome
    {
        get
        {
            return _adminhome;
        }
        set
        {
            _adminhome = value;
        }
    }
    public static int companyHome
    {
        get
        {
            return _companyhome;
        }
        set
        {
            _companyhome = value;
        }
    }
    public static int userHome
    {
        get
        {
            return _userhome;
        }
        set
        {
            _userhome = value;
        }
    }
    public static int addCompany
    {
        get
        {
            return _addcompany;
        }
        set
        {
            _addcompany = value;
        }
    }
    public static int addUser
    {
        get
        {
            return _adduser;
        }
        set
        {
            _adduser = value;
        }
    }
    public static int registerationPage
    {
        get
        {
            return _registerationpage;
        }
        set
        {
            _registerationpage = value;
        }
    }
}
