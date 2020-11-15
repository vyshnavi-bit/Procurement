using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Logoutnew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["User_ID"] = "";
        Session.Clear();
        Session.Abandon();
       
        //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetNoStore();
    }
}
