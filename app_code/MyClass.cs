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

/// <summary>
/// Summary description for MyClass
/// </summary>
public static class MyClass
{
    public static void MyAlert(System.Web.UI.Page AspxPage, string StrMessage, string StrKey)
    {
        string StrScript;
        StrScript = "<script language=javaScript>alert('" + StrMessage + "')</script>";
        //if (AspxPage.IsStartupScriptRegistered(StrKey) == false)
        //{
        //    AspxPage.RegisterStartupScript(StrKey, StrScript);
        //}
    }
}
