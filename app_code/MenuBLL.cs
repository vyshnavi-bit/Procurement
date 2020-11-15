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
using System.IO;


/// <summary>
/// Summary description for MenuBLL
/// </summary>
public class MenuBLL
{
    DbHelper dbaccess = new DbHelper();
    SqlDataReader dr;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

	public MenuBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public SqlDataReader GetMenuTitle(int ccode)
    {
        
     
        string sqlstr = string.Empty;
        sqlstr = "SELECT * FROM Menu_Name WHERE companycode=" + ccode + "  ORDER BY Tid ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
    public DataTable GetSubMenuTitle(int ccode)
    {
        string sqlstr = string.Empty;
        //sqlstr = "SELECT * FROM SubMenu_Name WHERE companycode=" + ccode + "  ORDER BY Menuloadid,submenuvalue";
        sqlstr = "SELECT Tid,CONVERT(Nvarchar(20),SubMenuName) AS SubMenuName,SubMenuValue,SubMenuimage,SubMenuurl,A1,A2,A3,A4,Status,MenuLoadid,companycode,A5,A6,A7,A8 FROM SubMenu_Name WHERE companycode='" + ccode + "'  ORDER BY Menuloadid,submenuvalue";

        dt = dbaccess.GetDatatable(sqlstr);
        return dt;
    }

    public SqlDataReader GetSubMenuTitle1()
    {
        string sqlstr = string.Empty;
        sqlstr = "SELECT * FROM SubMenu_Name ORDER BY Tid";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
    public DataSet GetSubMenuTitleCheckBoxMaster(int menuid)
    {
        string sqlstr = string.Empty;
        sqlstr = "SELECT * FROM SubMenu_Name  WHERE MenuLoadid=" + menuid + " ORDER BY submenuvalue";
        ds = dbaccess.GetDataset(sqlstr);        
        return ds;
    }

    public SqlDataReader RoleBasedSubmenuSelect(string Roleid ,int mid)
    {
        int Role = Convert.ToInt32(Roleid);
        string sqlstr = string.Empty;
        if (Role == 1)
        {
            sqlstr = "SELECT A1,MenuLoadid FROM SubMenu_Name WHERE MenuLoadid='" + mid + "'  ORDER BY submenuvalue";
        }
        else if (Role == 2)
        {
            sqlstr = "SELECT A2,MenuLoadid FROM SubMenu_Name WHERE MenuLoadid='" + mid + "'  ORDER BY submenuvalue";
        }
        else if (Role == 3)
        {
            sqlstr = "SELECT A3,MenuLoadid FROM SubMenu_Name WHERE MenuLoadid='" + mid + "'  ORDER BY submenuvalue";
        }
        else if (Role == 4)
        {
            sqlstr = "SELECT A4,MenuLoadid FROM SubMenu_Name WHERE MenuLoadid='" + mid + "'  ORDER BY submenuvalue";
        }
        else
        {

        }
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
    public void UpdateMenuRole(int A, int MenuValue,string Rol)
    {
        int Role = Convert.ToInt32(Rol);
        if (Role == 1)
        {
            string sqlstr = string.Empty;
            sqlstr = "UPDATE SubMenu_Name SET A1=" + A + " WHERE SubMenuValue=" + MenuValue + " ";
            dbaccess.ExecuteNonquorey(sqlstr);
        }
        else if (Role == 2)
        {
            string sqlstr = string.Empty;
            sqlstr = "UPDATE SubMenu_Name SET A2=" + A + " WHERE SubMenuValue=" + MenuValue + " ";
            dbaccess.ExecuteNonquorey(sqlstr);
        }
        else if (Role == 3)
        {
            string sqlstr = string.Empty;
            sqlstr = "UPDATE SubMenu_Name SET A3=" + A + " WHERE SubMenuValue=" + MenuValue + " ";
            dbaccess.ExecuteNonquorey(sqlstr);
        }
        else if (Role == 4)
        {
            string sqlstr = string.Empty;
            sqlstr = "UPDATE SubMenu_Name SET A4=" + A + " WHERE SubMenuValue=" + MenuValue + " ";
            dbaccess.ExecuteNonquorey(sqlstr);
        }
    }

}
