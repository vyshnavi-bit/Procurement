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
using System.Net.Mail;
public partial class Contact_us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void clear()
    {
        txtMail.Text = "";
        txtMessage.Text = "";
        txtName.Text = "";
        DropDownList1.ClearSelection();
        txtPh.Text = "";
        TxtCmp.Text = "";
        txtTuring.Text = "";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
            if (Page.IsValid && (txtTuring.Text.ToString() == Session["randomStr"].ToString()))
            {

                object refUrl = ViewState["RefUrl"];
                if (refUrl != null)
                    Response.Redirect((string)refUrl);

            }
            else
            {
                capthast.Text = "Please enter info correctly";
                uscMsgBox1.AddMessage("Please enter info correctly", MessageBoxUsc_Message.enmMessageType.Error);
            }
            MailMessage feedBack = new MailMessage();
            feedBack.To.Add("one0solution@gmail.com");
            feedBack.From = new MailAddress("one0solution@gmail.com");
            feedBack.Subject = DropDownList1.Text;

            feedBack.Body = "Sender Company:" + TxtCmp.Text + "<br/><br/>Sender Name: " + txtName.Text + "<br /><br />Sender Email: " + txtMail.Text + "<br /><br />Sender Phone: " + txtPh.Text + "" + txtMessage.Text;
            feedBack.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("one0solution@gmail.com", "complete10sol");
            //Or your Smtp Email ID and Password
            smtp.Send(feedBack);
            clear();
            //Label1.Text = "Thanks for contacting us";
            //uscMsgBox1.AddMessage("Thanks for contacting us", MessageBoxUsc_Message.enmMessageType.Success);
            Response.Redirect("Thank forcontactingus.aspx");
        
    }
}
