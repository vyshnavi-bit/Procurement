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

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string title = "Confirm";
        string text = @"Hello everyone, I am an Asp.net MessageBox. You can set MessageBox.SuccessEvent and MessageBox.FailedEvent and Click Yes(OK) or No(Cancel) buttons for calling them. The Methods must be a WebMethod because client-side application will call web services.";
        MessageBox messageBox = new MessageBox(text, title, MessageBox.MessageBoxIcons.Question, MessageBox.MessageBoxButtons.OKCancel, MessageBox.MessageBoxStyle.StyleB);
        messageBox.SuccessEvent.Add("OkClick");
        messageBox.SuccessEvent.Add("OkClick");
        messageBox.FailedEvent.Add("CancalClick");
        Literal1.Text = messageBox.Show(this); 
    }
    public static string OkClick(object sender, EventArgs e)
    {
        return "You have clicked Ok button";
    }
    public static string CancalClick(object sender, EventArgs e)
    {
        return "You have clicked Cancel button.";
    }
}
