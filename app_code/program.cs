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
/// Summary description for program
/// </summary>
public static class program
{
    public static int Guser_role = 0;///User role will set when user get login to the application.
    public static int Guser_privilage = 0; ///User privilage will set when user get login to the application.
    public static int Guser_PermissionId = 3;
    public static int Guser_User_Id = 0;
    public static DataTable prosdt = new DataTable();
    public static SqlDataReader prosdr;
  

    //public program()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

}