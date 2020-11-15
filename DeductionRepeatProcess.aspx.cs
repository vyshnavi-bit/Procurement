using System;
using System.Collections.Generic;
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
using System.Drawing;
using System.IO;
using System.Globalization;

public partial class DeductionRepeatProcess : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public DateTime dtm;
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTGG = new DataSet();
    msg getclass = new msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    dtm = System.DateTime.Now;
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                }
                else
                {

                    pname = ddl_Plantname.SelectedItem.Text;


                }

            }
            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {


        }
    }
    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTGG, ("plantname"));
            ddl_Plantname.DataSource = DTGG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            using (SqlCommand cmd = new SqlCommand("[update_DeductionRecoverytoDeductiondetails]", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@companycode", ccode);
                cmd.Parameters.AddWithValue("@plantcode", pcode);
                cmd.Parameters.AddWithValue("@crdeductiondate", d1);
                con.Open();
                cmd.ExecuteNonQuery();

                getdeductiondetails();

                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.update) + "')</script>");
            }
        }
        catch
        {



        }

    }


    public void getdeductiondetails()
    {
       
        DateTime dt1 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string str = "";
        con = DB.GetConnection();
        str = "Select  *    from Deduction_Recovery  where RPlant_Code='" + pcode + "' and RDeduction_RecoveryDate='" + d1 + "'";
        SqlCommand cmdselect = new SqlCommand(str,con);
        SqlDataAdapter datemp = new SqlDataAdapter(cmdselect);
        DataTable dttemp = new DataTable();
        datemp.Fill(dttemp);
        SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con);
        sqlBulkCopy.DestinationTableName = "dbo.Deductionbackup_Recovery";
        sqlBulkCopy.WriteToServer(dttemp);
        con = DB.GetConnection();
        string strdelete;
        strdelete = "delete * fdgdf     from Deduction_Recovery  where RPlant_Code='" + pcode + "' and RDeduction_RecoveryDate='" + d1 + "'";
        SqlCommand cmddelete = new SqlCommand(strdelete, con);
        cmddelete.ExecuteNonQuery();


    }


    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}