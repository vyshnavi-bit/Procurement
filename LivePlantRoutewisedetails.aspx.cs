using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LivePlantRoutewisedetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            lblId.Text = HttpUtility.UrlDecode(Request.QueryString["Id"]);
            lblName.Text = HttpUtility.UrlDecode(Request.QueryString["Name"]);
            lblCountry.Text = HttpUtility.UrlDecode(Request.QueryString["Pname"]);
        }
    }
}