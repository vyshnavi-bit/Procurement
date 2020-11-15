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

public partial class Rpt_AllPlantwiseLoanDetails : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    int ccode = 1, pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string acode1 = string.Empty;
    public static string acode2 = string.Empty;
    string d1 = string.Empty;
    string d2 = string.Empty;
    public static string sd1 = string.Empty;
    public static string sd2 = string.Empty;
    string d11 = string.Empty;
    string d21 = string.Empty;
    DateTime dtt = new DateTime();
    public string Rid = string.Empty;
    public string Aid = string.Empty;

    public string frmdate = string.Empty;
    public string Todate = string.Empty;
    int k = 1;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
            //    managmobNo = Session["managmobNo"].ToString();
                dtt = System.DateTime.Now;
                txt_FromDate.Text = dtt.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtt.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;

                Label6.Visible = true;
                ddl_PlantName.Visible = true;

                //Grid info



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
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;
                //
                Label6.Visible = true;
                ddl_PlantName.Visible = true;


            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

    }

    private void LoadPlantcode()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void loadsingleplant()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void Datefunc()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            d11 = dt1.ToString("MM/dd/yyyy hh:mm:ss");
            d21 = dt2.ToString("MM/dd/yyyy hh:mm:ss");

            d1 = dt1.ToString("MM/dd/yyyy");
            d2 = dt2.ToString("MM/dd/yyyy");

            sd1 = dt1.ToString("MM/dd/yyyy");
            sd2 = dt2.ToString("MM/dd/yyyy");


        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        try
        {
            Datefunc();
            Get_AgentwiseLoan();
            AgentLoanIdwise.Visible = false;
            LoanId.Visible = false;
        }
        catch (Exception ex)
        {

        }

    }

    public void Get_AgentwiseLoan()
    {
        try
        {
            DataTable dt = new DataTable();

            string cmd = "SELECT * FROM " +
" (SELECT agent_Id,CAST(SUM(loanamount) AS DECIMAL(18,2)) AS LAmount,CAST((SUM(loanamount)-SUM(balance)) AS DECIMAL(18,2)) AS LrecoveryAmount,CAST(SUM(balance) AS DECIMAL(18,2)) AS Lbalance  FROM LoanDetails Where company_code='" + ccode + "' AND plant_code='" + pcode + "' AND LLoanFlag=1  GROUP BY agent_Id ) AS L " +
" LEFT JOIN " +
" (SELECT (CONVERT(nvarchar(15),Agent_Id)+'_'+ Agent_Name) AS Agent_Name,Agent_Id AS Aid FROM Agent_Master Where Company_code='" + ccode + "' AND Plant_code='" + pcode + "') AS A ON L.agent_Id=A.Aid ";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);
            

            if (dt.Rows.Count > 0)
            {
                AgentwiseLoan.DataSource = dt;
                AgentwiseLoan.DataBind();

                AgentwiseLoan.FooterRow.Cells[1].Text = "Total";
                AgentwiseLoan.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                decimal LAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("LAmount"));
                AgentwiseLoan.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                AgentwiseLoan.FooterRow.Cells[2].Text = LAmount.ToString("N2");

                decimal LrecoveryAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("LrecoveryAmount"));
                AgentwiseLoan.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                AgentwiseLoan.FooterRow.Cells[3].Text = LrecoveryAmount.ToString("N2");

                decimal Lbalance = dt.AsEnumerable().Sum(row => row.Field<decimal>("Lbalance"));
                AgentwiseLoan.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                AgentwiseLoan.FooterRow.Cells[4].Text = Lbalance.ToString("N2");

            }
            else
            {
                dt = null;
                AgentwiseLoan.DataSource = dt;
                AgentwiseLoan.DataBind();
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            string spilval = string.Empty;
            string[] spilvalarr = new string[2];
            //Grid info   
            Datefunc();
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            acode1 = grdrow.Cells[2].Text;
            spilval = grdrow.Cells[5].Text;
            spilvalarr = spilval.Split('_');
            acode2 = spilvalarr[0];
            Get_AgentLoanIdwise(acode2);
            AgentLoanIdwise.Visible = true;
            LoanId.Visible = false;       

        }
        catch (Exception ex)
        {

        }
    }

    protected void AgentwiseLoan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].Visible = false;      
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            //string filename = "'" + ddl_PlantName.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                AgentwiseLoan.AllowPaging = false;
                Datefunc();
                Get_AgentwiseLoan();

                AgentwiseLoan.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in AgentwiseLoan.HeaderRow.Cells)
                {
                    cell.BackColor = AgentwiseLoan.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in AgentwiseLoan.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = AgentwiseLoan.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = AgentwiseLoan.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                AgentwiseLoan.RenderControl(hw);

                //style to format numbers to string
                //  string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                string style = @"<style> .textmode { } </style>";
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


    public void Get_AgentLoanIdwise(string Aid)
    {
        try
        {
            DataTable dt = new DataTable();

            string cmd = "SELECT agent_Id,loan_Id,CAST(SUM(loanamount) AS DECIMAL(18,2)) AS LAmount,CAST((SUM(loanamount)-SUM(balance)) AS DECIMAL(18,2)) AS LrecoveryAmount,CAST(SUM(balance) AS DECIMAL(18,2)) AS Lbalance,EntryDate FROM LoanDetails Where company_code='" + ccode + "' AND plant_code='" + pcode + "' AND LLoanFlag=1 AND agent_Id='" + Aid + "'  GROUP BY loan_Id,agent_Id,EntryDate ";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;

            if (dt.Rows.Count > 0)
            {
                AgentLoanIdwise.DataSource = dt;
                AgentLoanIdwise.DataBind();

                AgentLoanIdwise.FooterRow.Cells[1].Text = "Total";
                AgentLoanIdwise.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                decimal LAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("LAmount"));
                AgentLoanIdwise.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                AgentLoanIdwise.FooterRow.Cells[3].Text = LAmount.ToString("N2");

                decimal LrecoveryAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("LrecoveryAmount"));
                AgentLoanIdwise.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                AgentLoanIdwise.FooterRow.Cells[4].Text = LrecoveryAmount.ToString("N2");

                decimal Lbalance = dt.AsEnumerable().Sum(row => row.Field<decimal>("Lbalance"));
                AgentLoanIdwise.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                AgentLoanIdwise.FooterRow.Cells[5].Text = Lbalance.ToString("N2");
                
            }
            else
            {
                dt = null;
                AgentLoanIdwise.DataSource = dt;
                AgentLoanIdwise.DataBind();
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void lnkView1_Click(object sender, EventArgs e)
    {
        try
        {
            string spilval = string.Empty;
            string LoanId1 = string.Empty;
            string[] spilvalarr = new string[2];
            //Grid info   
            Datefunc();
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;           
            LoanId1 = grdrow.Cells[6].Text;
           
            Get_LoanIdDetails(acode2, LoanId1);
            LoanId.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }


    protected void AgentLoanIdwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_AgentLoanIdwiseExport_Click(object sender, EventArgs e)
    {

    }


    public void Get_LoanIdDetails(string Aid, string Lid)
    {
        try
        {
            DataTable dt = new DataTable();

            string cmd = "SELECT CONVERT(Nvarchar,Paid_date,103) AS Paid_date,Openningbalance,Paid_Amount,Closingbalance FROM Loan_Recovery Where Company_code='" + ccode + "' AND Plant_code='" + pcode + "' AND Agent_id='" + Aid + "' AND Loan_id='" + Lid + "' Order By Tid ";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);

            decimal pMkg = 0;
            decimal CMkg = 0;
            decimal Diff_Milkkg = 0;

            if (dt.Rows.Count > 0)
            {
                LoanId.DataSource = dt;
                LoanId.DataBind();

                LoanId.FooterRow.Cells[1].Text = "Total";
                LoanId.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                //decimal Openningbalance = dt.AsEnumerable().Sum(row => row.Field<decimal>("Openningbalance"));
                //LoanId.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //LoanId.FooterRow.Cells[2].Text = Openningbalance.ToString("N2");

                decimal Paid_Amount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Paid_Amount"));
                LoanId.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                LoanId.FooterRow.Cells[3].Text = Paid_Amount.ToString("N2");

                //decimal Closingbalance = dt.AsEnumerable().Sum(row => row.Field<decimal>("Closingbalance"));
                //LoanId.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //LoanId.FooterRow.Cells[4].Text = Closingbalance.ToString("N2");



            }
            else
            {
                dt = null;
                LoanId.DataSource = dt;
                LoanId.DataBind();
            }
        }

        catch (Exception ex)
        {
        }
    }



    protected void LoanId_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
               
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btn_LoanIdExport_Click(object sender, EventArgs e)
    {

    }



    
}