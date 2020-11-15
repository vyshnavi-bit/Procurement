using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class EMPLOYEEMASTER : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_ExcelFileUpload_Click(object sender, EventArgs e)
    {


        string Filepath = string.Empty;
        try
        {

            if (Excel_FileUpload.FileName != "")
            {
                Filepath = string.Concat(Server.MapPath("~/EXData/" + Excel_FileUpload.FileName.Trim()));
                if (Filepath != null)
                {

                    if (File.Exists(Filepath))
                    {
                        WebMsgBox.Show("File Name already Exists");
                    }
                    else
                    {
                        Excel_FileUpload.SaveAs(Filepath);
                        //   GetUploadExcelFileName();

                    }
                }
            }
            else
            {
                WebMsgBox.Show("Choose the File to Upload");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void auto_CheckedChanged(object sender, EventArgs e)
    {

        if (auto2.Checked == true)
        {
            manual2.Checked = false;
        }


    }
    protected void manual_CheckedChanged(object sender, EventArgs e)
    {
        if (manual2.Checked == true)
        {
            auto2.Checked = false;
        }

       
    }
    protected void auto3_CheckedChanged(object sender, EventArgs e)
    {
        if(auto3.Checked==true)
        {

            manual3.Checked = false;

        }


    }
    protected void manual3_CheckedChanged(object sender, EventArgs e)
    {

        if (manual3.Checked == true)
        {

            auto3.Checked = false;
        }
    }
    protected void btn_Ok_Click(object sender, EventArgs e)
    {

    }
}
    
