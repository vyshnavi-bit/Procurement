using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class PermissionRoleList : System.Web.UI.Page
{
    public string Company_code;
    public string plant_Code;
    public string cname;
    DataTable dt = new DataTable();
    DbHelper dbaccess = new DbHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                LoadpermissionRoleList();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                
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
            LoadpermissionRoleList();           
        }

        catch (Exception ex)
        {
        }
    }
    private void LoadpermissionRoleList()
    {
        try
        {
            string str = string.Empty;

            string ReportField = "Plant_Name,Name,Roles,Mobile_Number,Pmail";

            SqlConnection con = null;
            str = "SELECT " + ReportField + " FROM " +
" (SELECT Plant_Code,CONVERT(NVARCHAR(15),(CONVERT(NVARCHAR(10),Plant_Code)+'_'+Plant_Name)) AS Plant_Name,Plant_PhoneNo,Manager_Name,Mana_PhoneNo,Pmail FROM Plant_Master  WHERE Plant_Code>150) AS pm " +
" RIGHT JOIN " +
" (SELECT Plant_ID,Users_ID,CONVERT(NVARCHAR(15),(CONVERT(NVARCHAR(10),Users_ID)+'_'+First_Name)) AS Name,Roles,Mobile_Number,Email_ID,Address,Added_Date FROM Add_User  WHERE Plant_ID>150 AND Roles='" + ddl_role.SelectedItem.Value.Trim() + "' ) AS Um ON pm.Plant_Code=Um.Plant_ID ORDER BY pm.Plant_Code,Um.Users_ID";
            using (con = dbaccess.GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dt);
                gvCustomers.Dispose();
                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
   

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        gvCustomers.PageIndex = e.NewPageIndex;
        gvCustomers.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
     
}