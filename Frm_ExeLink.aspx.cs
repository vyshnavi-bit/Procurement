using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Frm_ExeLink : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            Lbl_Errormsg.Visible = false;
        }
        else
        {
            Lbl_Errormsg.Visible = false;
        }
    }
    protected void btn_DownLoad_Click(object sender, EventArgs e)
    {
        try
        {

            //  string MFileName = @"C:/BILL VYSHNAVI/exe.rar";
            string MFileName = string.Empty;
            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
                {
                    MFileName = Server.MapPath("~/VYSHNAVIBILL/Weigher.rar");

                }
                else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {
                    MFileName = Server.MapPath("~/VYSHNAVIBILL/Fatomatic.rar");
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "3".Trim())
                {
                    MFileName = Server.MapPath("~/VYSHNAVIBILL/Analyzer.rar");
                }
                else
                {
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "Please,Select the Exe Type...";
                }

                System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
                if (File.Exists(MFileName.ToString()))
                {
                    //
                    FileStream sourceFile = new FileStream(file.FullName, FileMode.Open);
                    float FileSize;
                    FileSize = sourceFile.Length;
                    byte[] getContent = new byte[(int)FileSize];
                    sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                    sourceFile.Close();
                    //
                    Response.ClearContent(); // neded to clear previous (if any) written content
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.BinaryWrite(getContent);
                    //File.Delete(file.FullName.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please,Select the Exe Type...";
            }

        }
        catch (Exception ex)
        {

        }
            
       
    }
    


}