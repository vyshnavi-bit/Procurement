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
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
public partial class Turing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bitmap objBMP = new System.Drawing.Bitmap(40, 20);
        Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
        objGraphics.Clear(Color.Green);
        objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        //' Configure font to use for text
        Font objFont = new Font("Arial", 8, FontStyle.Bold);
        string randomStr = "";
        int[] myIntArray = new int[5];
        int x;
        //That is to create the random # and add it to our string 
        Random autoRand = new Random();
        for (x = 0; x < 5; x++)
        {
            myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
            randomStr += (myIntArray[x].ToString());
        }
        //This is to add the string to session cookie, to be compared later
        Session.Add("randomStr", randomStr);
        //' Write out the text
        objGraphics.DrawString(randomStr, objFont, Brushes.White, 3, 3);
        //' Set the content type and return the image
        Response.ContentType = "image/GIF";
        objBMP.Save(Response.OutputStream, ImageFormat.Gif);
        objFont.Dispose();
        objGraphics.Dispose();
        objBMP.Dispose();
    }
}
