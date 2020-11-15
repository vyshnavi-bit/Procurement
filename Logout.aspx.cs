using System;
using System.Collections;
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

public partial class Logout : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    DbHelper db = new DbHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            insertsession();


            Session["User_ID"] = "";
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("LoginDefault.aspx");
            //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();
        }
        catch
        {

        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
    }

    public void insertsession()
    {
        con = db.GetConnection();
        string stt = "";
        stt = "Insert Into SessionLogin(UserName,LogoutTime) values('" + Session["Name"].ToString() + "','" + System.DateTime.Now + "')";
        SqlCommand cmd = new SqlCommand(stt, con);
        cmd.ExecuteNonQuery();

    }
}
