using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
public partial class PlantBankPaymentSelectedList : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    SqlDataReader dr;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DbHelper dbaccess = new DbHelper();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    SqlConnection con = new SqlConnection();
    string d1, d2;
    public string pname;
    DataSet DTG = new DataSet();
    DbHelper DB = new DbHelper();
    string getid;
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
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                    Button2.Visible = false;
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        Bdate();
                        //   Adddate();
                        LoadUploadedFilesDetails();

                    }

                    else
                    {

                        LoadPlantcode();
                        Bdate();
                        //   Adddate();
                        LoadUploadedFilesDetails();
                    }

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
        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }
    public void loadsingleplant()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
     //   ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantname.SelectedItem.Value;
        Bdate();
     //   Adddate();
        LoadUploadedFilesDetails();
    }
    private void Datechange()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");
    }

    private void LoadUploadedFilesDetails()
    {
        try
        {
            using (con = dbaccess.GetConnection())
            {
                Datechange();
                DataTable dt = new DataTable();
                GridView7.Dispose();
                string str = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY BankFileName) AS serial_no,COUNT(BankFileName) AS ActualNoofROws,UPPER(BankFileName) AS BankFileName,SUM(NetAmount) AS TotalAmount  FROM BankPaymentllotment Where Plant_Code='" + pcode.Trim() + "' AND Billfrmdate='" + d1.ToString().Trim() + "' Group By BankFileName ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView7.DataSource = dt;
                    GridView7.DataBind();
                }

                else
                {
                    GridView7.DataSource = null;
                    GridView7.DataBind();


                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    //private void Adddate()
    //{
    //    try
    //    {
    //        DateTime dt1 = new DateTime();
    //        DateTime dt2 = new DateTime();

    //        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
    //        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

    //        string d1 = dt1.ToString("MM/dd/yyyy");
    //        string d2 = dt2.ToString("MM/dd/yyyy");

    //        SqlDataReader dr = null;
    //        ddl_Addeddate.Items.Clear();
    //        dr = Adddate(ccode, pcode, d1, d2);
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                ddl_Addeddate.Items.Add(dr["BankFileName"].ToString());
    //            }
    //        }
    //        else
    //        {
    //            ddl_Addeddate.Items.Add("--select date--");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}
    public SqlDataReader Billdate(string ccode, string pcode)
    {
        SqlDataReader dr;
        string sqlstr = string.Empty;
        sqlstr = "SELECT Bill_frmdate,Bill_todate,UpdateStatus,ViewStatus FROM Bill_date where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND CurrentPaymentFlag='1' ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
    private void Bdate()
    {
        try
        {
            dr = null;
            dr = Billdate(ccode, ddl_Plantname.SelectedItem.Value);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr["Bill_todate"]).ToString("dd/MM/yyyy");

                }
            }
            else
            {
                WebMsgBox.Show("Please Select the Bill_Date");
            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
    {

        //GridView8.Visible = true;
        //Button2.Visible = true;

        getid = (GridView7.SelectedRow.Cells[3].Text).ToString();

        getgrid();
        Button2.Visible = true;
    }

    public void getgrid()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");
        string get;
        con = dbaccess.GetConnection();
        get = "SELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,NETAMOUNT,BANK_NAME    FROM (sELECT  AGENT_ID,AGENT_NAME,ACCOUNT_NO,IFSCCODE,NETAMOUNT,BANK_ID   FROM  BankPaymentllotment    WHERE PLANT_CODE='" + pcode + "'  AND   BANKFILENAME='" + getid + "'  AND  billfrmdate='" + d1 + "'  and billtodate='" + d2 + "') AS BANK  LEFT JOIN (sELECT  BANK_NAME,BANK_ID,IFSC_CODE  FROM  BANK_MASTER    WHERE PLANT_CODE='" +  ddl_Plantname.SelectedItem.Value + "'   GROUP BY  BANK_NAME,BANK_ID,IFSC_CODE    ) AS GG ON  BANK.BANK_ID=GG.BANK_ID AND BANK. IFSCCODE =GG.IFSC_CODE";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);


        if (dt.Rows.Count > 0)
        {
            GridView8.DataSource = dt;
            GridView8.DataBind();
            GridView8.FooterRow.Cells[3].Text = "TOTAL AMOUNT";
            decimal milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("NETAMOUNT"));
            GridView8.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            GridView8.FooterRow.Cells[5].Text = milkkg.ToString("N2");
        }
        else
        {
            GridView8.DataSource = null;
            GridView8.DataBind();


        }


    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double TEMVAL = Convert.ToDouble(e.Row.Cells[5].Text);
            e.Row.Cells[5].Text = TEMVAL.ToString("F2");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = txt_FromDate.Text + txt_ToDate.Text + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView8.GridLines = GridLines.Both;
            GridView8.HeaderStyle.Font.Bold = true;
            GridView8.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
            Button2.Visible = false;
        }
        catch
        {
            string message;
            message = "Please Check Your Data";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }
    }
}