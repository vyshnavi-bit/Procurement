using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
using System.Diagnostics;
using System.Collections.Generic;
public partial class MasterPage : System.Web.UI.MasterPage
{
    string strUserRole = string.Empty;
    int roless;
    DataTable dts = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("LoginDefault.aspx");
        }
        else
        {
            if (!IsPostBack == true)
            {
                title.Text = "VYSHNAVI DAIRY WELCOMES YOU-" + Session["name"].ToString();
                roless = Convert.ToInt32(Session["Role"]);
                if (roless == 9)
                {
                    strUserRole = "Specialadmin";
                }
                if (roless == 7)
                {
                    strUserRole = "Superadmin";
                }
                if (roless == 6)
                {
                    strUserRole = "adminmanager";
                }
                if (roless == 5)
                {
                    strUserRole = "manager";
                }
                if (roless == 4)
                {
                    strUserRole = "Account";
                }
                if (roless == 3)
                {
                    strUserRole = "finance";
                }
                if (roless == 2)
                {
                    strUserRole = "user";
                }
                if (roless == 1)
                {
                    strUserRole = "Enduser";
                }
                Menu1.DataSource = GetDataSource(strUserRole, Server.MapPath("~"));
                Menu1.DataBind();
            }
        }
    }
    public void lockstatus()
    {
 
        DbHelper db=new DbHelper();
        SqlConnection con=new SqlConnection();
        con = db.GetConnection();
        string str = "Select *  from Unlock where plant_code='" + Session["plant_code"] + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter ds = new SqlDataAdapter(cmd);
         
        ds.Fill(dts);
    }

    XmlDataSource GetDataSource(string UserRole, string ServerPath)
    {
        XmlDataSource objData = new XmlDataSource();
        objData.XPath = "siteMap/siteMapNode";
        switch (UserRole)
        {
            case "Specialadmin":
                objData.DataFile = ServerPath + @"/Specialadmin.sitemap";
                break;
            case "Superadmin":
                objData.DataFile = ServerPath + @"/superadmin.sitemap";
                break;
            case "adminmanager":
                objData.DataFile = ServerPath + @"/adminmanager.sitemap";   
                break;
            case "manager":
                objData.DataFile = ServerPath + @"/manager.sitemap";
                break;
            case "Account":
                objData.DataFile = ServerPath + @"/Account.sitemap";
                break;
            case "finance":
                objData.DataFile = ServerPath + @"/finance.sitemap";
                break;
            case "user":
                objData.DataFile = ServerPath + @"/user.sitemap";
                break;
            case "Enduser":
                objData.DataFile = ServerPath + @"/Enduser.sitemap";
                break;
        }
        objData.DataBind();
        return objData;
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {

    }
}
