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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class StopedLoanAgents : System.Web.UI.Page
{

  
    DbHelper dbaccess = new DbHelper();
    string plantname;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    DataSet ds3 = new DataSet();
    int i = 1;
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;
    string date;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;
                txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
                txt_ToDate.Text = tdt.ToString("dd/MM/yyy");
                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantName();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";

                }

               // pcode = ddl_PlantName.SelectedItem.Value;
            }

        }

        else
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_PlantName.SelectedItem.Value;

                plantname = ddl_PlantName.SelectedItem.Text;

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }




    }



    private void LoadPlantName()
    {
        try
        {

            ds = null;
            ds =LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

                plantname = ddl_PlantName.SelectedItem.Text;

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSinglePlantName()
    {
        try
        {

            ds1 = null;
            ds1 = LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds1 != null)
            {
                ddl_PlantName.DataSource = ds1;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();
                plantname = ddl_PlantName.SelectedItem.Text;
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }



    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = dbaccess.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            DataTable getdata = new DataTable();
            SqlDataAdapter dsii = new SqlDataAdapter(cmd);
            getdata.Rows.Clear();
            dsii.Fill(getdata);
            ddl_PlantName.DataSource = getdata;
            ddl_PlantName.DataTextField = "Plant_Name";
            ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
            ddl_PlantName.DataBind();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }



    public DataSet LoadSinglePlantNameChkLst1(string ccode, string pcode)
    {
        ds2 = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code, CONVERT(NVARCHAR(15),pcode+'_'+Plant_Name) AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
        ds2 = dbaccess.GetDataset(sqlstr);
        return ds2;
    }


    public DataSet LoadPlantNameChkLst1(string ccode)
    {
        ds3 = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code,pcode+'_'+Plant_Name AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "'  ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
        ds3 = dbaccess.GetDataset(sqlstr);
        return ds3;
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        try
        {
            getloanstoppedagent();
        }
        catch
        {


        }
    }

    public void getloanstoppedagent()
    {

        SqlConnection con = new SqlConnection(connStr);
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        string str = "select agent_Id,loan_Id,CAST(loanamount AS Numeric(12, 2)) as loanamount,CAST(balance AS Numeric(12, 2)) as balance, convert(varchar,loandate,103) as loandate   from  loandetails   as ld where plant_code='" + pcode + "' and  loandate < '" + d2 + "' and  balance >= 1 and  not  EXISTS(select *  from Loan_Recovery as lr where plant_code='" + pcode + "' and Paid_date between '" + d1 + "' and '" + d2 + "' and ld.agent_Id=lr.Agent_id)";
        SqlCommand cmd=new SqlCommand(str,con);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        //SqlCommand cmd = new SqlCommand(str, con);
        //SqlDataReader dr = cmd.ExecuteReader();
        if (dt.Rows.Count > 0)
        {

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.Maroon;
            GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
            GridView1.HeaderRow.Cells[6].Text = "LastRecoveryDate";



            decimal loanamt = dt.AsEnumerable().Sum(row => row.Field<decimal>("loanamount"));
            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[3].Text = loanamt.ToString("N2");

            decimal balance = dt.AsEnumerable().Sum(row => row.Field<decimal>("balance"));
            GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[4].Text = balance.ToString("N2");

            GridView1.FooterRow.Cells[0].Text = string.Empty;
            GridView1.FooterRow.Cells[1].Text = string.Empty;
            GridView1.FooterRow.Cells[2].Text = "Total";


            GridView1.FooterRow.Cells[5].Text = string.Empty;
            GridView1.FooterRow.Cells[6].Text = string.Empty;

            GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
            GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
            GridView1.FooterStyle.Font.Bold = true;
            
        }
        else
        {

            GridView1.DataSource = "";
            GridView1.DataBind();
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.Maroon;
            GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
            GridView1.HeaderRow.Cells[6].Text = "";




            GridView1.FooterRow.Cells[3].Text = "0";

            
            GridView1.FooterRow.Cells[4].Text = "0";

            GridView1.FooterRow.Cells[0].Text = string.Empty;
            GridView1.FooterRow.Cells[1].Text = string.Empty;
            GridView1.FooterRow.Cells[2].Text = "Total";


            GridView1.FooterRow.Cells[5].Text = string.Empty;
            GridView1.FooterRow.Cells[6].Text = string.Empty;

            GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
            GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
            GridView1.FooterStyle.Font.Bold = true;




            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert","alert('No Data');", true);


        }

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
        if (i == 1)
        {

            GridViewRow HeaderRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            TableCell HeaderCell4 = new TableCell();

            HeaderCell3.Text = plantname + ": Stopped Loan Details";
            //   HeaderCell4.Text = "ToDate" + d2;
            HeaderCell3.ColumnSpan = 7;
            HeaderCell3.Attributes.CssStyle["text-align"] = "center";
            HeaderRow.Cells.Add(HeaderCell3);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell3.Font.Bold = true;


            HeaderCell3.ForeColor = System.Drawing.Color.Brown;
            HeaderCell3.BackColor = System.Drawing.Color.White;
            i = 2;

        }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string agentid = e.Row.Cells[1].Text;
                string loanid = e.Row.Cells[2].Text;


                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

                string d1 = dt1.ToString("MM/dd/yyyy");
                string d2 = dt2.ToString("MM/dd/yyyy");



                SqlConnection con = new SqlConnection(connStr);

                string str = "select top 1  convert(varchar,Paid_date,103) as Paid_date  from Loan_Recovery as lr where plant_code='" + pcode + "' and Agent_id='" + agentid + "'  and   Loan_id='" + loanid + "' AND  Paid_date < '" + d2 + "'  order by  CONVERT(DateTime, Paid_date,101) desc";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        date = dr["Paid_date"].ToString();
                    }
                }
                else
                {

                    date = "Old Pending";


                }

               
            }

           e.Row.Cells[6].Text = date;

           if (date == "Old Pending")
           {
               e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;

           }


          

        
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {


        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + plantname + "'  " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                getloanstoppedagent();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                // string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
        }


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}