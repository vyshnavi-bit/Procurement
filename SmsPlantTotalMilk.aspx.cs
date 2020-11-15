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
using System.Text;
public partial class SmsPlantTotalMilk : System.Web.UI.Page
{   
    string message1,MobileNo;
    public string frmdate = string.Empty;
    DateTime dti = new DateTime();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    StringBuilder builder = new StringBuilder();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dti = System.DateTime.Now;
                txt_FromDate.Text = dti.ToString("dd/MM/yyy");
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


            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }

    }
    public void sendsms()
    {
        try
        {
            if (!string.IsNullOrEmpty(txt_mobile.Text))
            {              
                GetMilkDetails();
                MobileNo = txt_mobile.Text;
                string strUrl = " http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=sulasha77&from=VYSNVI&to=" + MobileNo + "&msg=" + builder + "&type=1 ";
                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();               
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

    public void GetMilkDetails()
    {
        try
        {
            double Cmilk=0;
            double Bmilk=0;
            double GTotmilk=0;
            int flag = 0;
            dti = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            frmdate = dti.ToString("MM/dd/yyyy");
            using (SqlConnection conn = dbaccess.GetConnection())
            {
                string Sqlstr = "SELECT t1.Plant_NAme+':'+(Convert(Nvarchar(10),t2.SMkg)) AS Pname,t1.Milktype,t2.SMkg,t2.Prdate,t1.plant_code FROM " +
" (SELECT plant_code,Convert(Nvarchar(4),plant_Name) AS Plant_NAme,milktype FROM plant_master Where milktype IS NOT NULL AND plant_code<>160 ) AS t1 " +
" LEFT JOIN " +
" (SELECT SUM(ISNULL(Milk_kg,0)) AS SMkg,CONVERT(Nvarchar(10),Prdate,103) AS Prdate,plant_code AS pcode FROM Procurement Where Prdate='" + frmdate + "' GROUP BY plant_code,Prdate) AS t2 ON t1.Plant_Code=t2.pcode ORDER BY Milktype desc";
                SqlCommand COM = new SqlCommand(Sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        if (flag == 0)
                        {
                            message1 = "SVDS:PROCUREMENT-:" + "\n";
                            builder.Append(message1);
                            builder.Append("Date :" + dtRow[3].ToString() + "\n");
                            builder.Append("----------------------" + "\n");
                            
                            flag = 1;
                        }
                        if (Convert.ToDouble(dtRow[2]) >= 0)
                        {
                            if (dtRow[1].ToString() == "1")
                            {
                                Cmilk = Cmilk + Convert.ToDouble(dtRow[2]);
                            }
                            else
                            {
                                Bmilk = Bmilk + Convert.ToDouble(dtRow[2]);
                            }

                            builder.Append(dtRow[0] + "\n");
                        }
                       
                    }
                    GTotmilk = Cmilk + Bmilk;
                    builder.Append("--------------------"+"\n");
                    builder.Append("CowTot :" + Cmilk + "\n");
                    builder.Append("BuffTot :" + Bmilk + "\n");
                    builder.Append("--------------------" + "\n");
                    builder.Append("GTotal :" + GTotmilk + "\n");
                    builder.Append("--------------------" + "\n");
                }
                else
                {
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "Please,Check Procurement Data...";                   
                }
            }

        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString(); 
        }
    }
   

    protected void btn_Send_Click(object sender, EventArgs e)
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
}