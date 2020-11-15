using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
public partial class DownloadDatabaseFile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    private const int MyMaxContentLength = 10000;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridviewData();
        }

        if (!IsPostBack)
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/down/"));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            GridView1.DataSource = files;
            GridView1.DataBind();
        }


      
    }

    private void BindGridviewData()
    {
        con = DB.GetConnection();
        string stt = "select * from FilesTable";
        SqlCommand cmd = new SqlCommand(stt,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(Server.MapPath("down/" + filename));
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand("insert into FilesTable(FileName,FilePath) values(@Name,@Path)", con);
        cmd.Parameters.AddWithValue("@Name", filename);
        cmd.Parameters.AddWithValue("@Path", "Files/" + filename);
        cmd.ExecuteNonQuery();
        con.Close();
        BindGridviewData();
    }
    protected void UploadFile(object sender, EventArgs e)
    {
        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/down/") + fileName);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    // This button click event is used to download files from gridview
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        //LinkButton lnkbtn = sender as LinkButton;
        //GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        //string filePath = gvDetails.DataKeys[gvrow.RowIndex].Value.ToString();
        //Response.ContentType = "image/jpg";
        //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
        //Response.TransmitFile(Server.MapPath(filePath));
        //Response.End();
        if (Request.HttpMethod == "POST" && Request.ContentLength > MyMaxContentLength)
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/down/") + fileName);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        File.Delete(filePath);
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}