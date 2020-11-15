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

public partial class captcha : System.Web.UI.Page
{
    static string prevPage = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            txtTuring.Focus();
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && (txtTuring.Text.ToString() == Session["randomStr"].ToString()))
        {

            //object refUrl = ViewState["RefUrl"];
            //if (refUrl != null)
            //    Response.Redirect((string)refUrl);    

            Response.Redirect("home.aspx");
        }
        else
        {
            capthast.Text="Please Enter Above Code ";
          //  uscMsgBox1.AddMessage("Please Enter Above Code", MessageBoxUsc_Message.enmMessageType.Error);
        }
    }
}
