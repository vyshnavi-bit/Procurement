using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GoogleSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     //   Response.Redirect("");
        Page.ClientScript.RegisterStartupScript(
            this.GetType(), "OpenWindow", "window.open('http://www.google.com/search?Q=');", true);
    }
}