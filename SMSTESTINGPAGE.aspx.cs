using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
public partial class SMSTESTINGPAGE : System.Web.UI.Page
{
   
    string message1;
    string no, cname, pname;
    int pcode, cmpcode;

    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                Lbl_Errormsg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                Lbl_Errormsg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            sendsms();           
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }
    public void clear()
    {
        txt_smscontend.Text = "";
        txt_mobile.Text = "";
    }
    public void sendsms()
    {
        try
        {
            if (!string.IsNullOrEmpty(txt_mobile.Text))
            {
                no = txt_mobile.Text;
                message1 = "From Vyshnavi Dairy:" + txt_smscontend.Text;
                //message = "\n SRI VYSHNAVI - " + pname + "\nDate:" + txt_FromDate.Text + "_" + sess + "\nLtr:" + litter + "\nFat:" + fat + "\nSnf:" + snf + "\nClr:" + clr + "\nRate:" + rate + "\n*****" + "_" + sess1 + "\nLtr:" + litter1 + "\nFat:" + fat1 + "\nSnf:" + snf1 + "\nClr:" + clr1 + "\nRate:" + rate1;
       // 29-7-2017       //  string strUrl = " http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=vyshnavi@123&from=VYSNVI&to=" + no + "&msg=" + message1 + "&type=1 ";
                string strUrl = " http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=vyshnavi@123&from=VYSNVI&to=" + no + "&msg=" + message1 + "&type=1 ";
          //  https://www.smsstriker.com/API/sms.php?username=[xxxxxx]&password=[xxxxxx]&from=[xxxxxxxx]&to=xxxxxx, xxxxxx, xxxx, xxxxx&msg=[xxxx]&type=1 
                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
                clear();
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Message Send Successfully...";
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Enter the Mobile Number...";
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }
    protected void btn_Dndcheck_Click(object sender, EventArgs e)
    {
        try
        {
            Lbl_Errormsg.Text = "";
            string strUrl = " http://www.smsstriker.com/API/dnd_check.php?username=vaishnavidairy&password=vyshnavi@123&to=" + txt_Dndcheck.Text.Trim();
            WebRequest request = HttpWebRequest.Create(strUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = dataString.Trim();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }
    protected void btn_smsbalance_Click(object sender, EventArgs e)
    {
        try
        {
            string strUrl = "http://www.smsstriker.com/API/get_balance.php?username=vaishnavidairy&password=vyshnavi@123";
            WebRequest request = HttpWebRequest.Create(strUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            txt_Msgbalanace.Text = dataString.Trim();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
       
    }
    private void Clear()
    {
        txt_smscontend.Text = "";
        txt_mobile.Text = "";
        txt_Msgbalanace.Text = "";
        txt_Dndcheck.Text = "";
    }
    protected void btn_clearall_Click(object sender, EventArgs e)
    {
        Clear();
    }
}