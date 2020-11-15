using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
{
    string uid;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack != true)
            {
                string name = Session["Name"].ToString();
                string pass = Session["pass"].ToString();
                if ((name != string.Empty) && (pass != string.Empty))
                {
                }
                else
                {
                    Server.Transfer("LoginDefault.aspx");
                }
            }
            else
            {
                if ((Session["Name"] != null) && (Session["Password"] != null))
                {
                    string name = Session["Name"].ToString();
                    string pass = Session["pass"].ToString();
                }
                else
                {
                  //  Server.Transfer("LoginDefault.aspx");
                }

            }
        }
        catch (Exception em)
        {

        }
    }
}