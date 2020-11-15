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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class PlantPerDayMilk : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    public string Company_code;
    public string plant_Code;
    public string cname;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                LoadPlantName();
                Menu1();
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
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                Loadperdayplantmilk();
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
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(Company_code.ToString());
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Plant_Name";
                CheckBoxList1.DataValueField = "plant_Code";//ROUTE_ID 
                CheckBoxList1.DataBind();

            }
            else
            {

            }
           
        }
        catch (Exception ex)
        {
        }
    }
    private void Loadperdayplantmilk()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            dt = null;
            int count = 0;
            dt = BllPlant.DTLoadPlantNameChkLst(Company_code, d1, d2);
            count = dt.Rows.Count;
            if (count > 0)
            {
                DataTable custDT = new DataTable();
                DataColumn col = null;

                col = new DataColumn("plant_Code");
                custDT.Columns.Add(col);

                for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                {
                    DataRow dr = null;
                    dr = custDT.NewRow();
                    if (CheckBoxList1.Items[i].Selected == true)
                    {
                        dr[0] = CheckBoxList1.Items[i].Value.ToString();
                        custDT.Rows.Add(dr);
                    }

                }
                DataTable dts = new DataTable();
                SqlParameter param = new SqlParameter();
                param.ParameterName = "CustDtPlantcode";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = custDT;
                param.Direction = ParameterDirection.Input;
                String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Get_PlantPerDayMilk]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    sqlCmd.Parameters.AddWithValue("@spccode", Company_code);
                    sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                    sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());                   
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    da.Fill(dts);

                    //
                    DataTable ksdt = new DataTable();                   
                    DataColumn ksdc = null;
                    DataRow ksdr = null;
                    int counts = dts.Rows.Count;

                    // START ADDING COLUMN
                    if (counts > 0)
                    {
                        ksdc = new DataColumn("plant");
                        ksdt.Columns.Add(ksdc);
                        foreach (DataRow dr1 in dts.Rows)
                        {
                            object id;
                            id = dr1[1].ToString();
                            string columnName = "" + id ;
                            DataColumnCollection columns = ksdt.Columns;
                            if (columns.Contains(columnName))
                            {
                                
                            }
                            else
                            {
                                ksdc = new DataColumn("" + id);
                                ksdt.Columns.Add(ksdc);
                            }
                            
                        }
                    }
                    // END ADDING COLUMN

                   // START ADDING ROWS
                    if (counts > 0)
                    {
                        object id2;
                        id2 = 0;
                        int idd2 = Convert.ToInt32(id2);                      

                        foreach (DataRow dr2 in dts.Rows)
                        {
                            ksdr = ksdt.NewRow();

                            object id1;
                            id1 = dr2[5].ToString();
                            int idd1 = Convert.ToInt32(id1);
                            if (idd1 == idd2)
                            {                                

                            }
                            else
                            {
                                int cc = 0;
                                foreach (DataRow dr3 in dts.Rows)
                                {
                                    object id3;
                                    id3 = dr3[5].ToString();
                                    int idd3 = Convert.ToInt32(id3);
                                    if (idd1 == idd3)
                                    {
                                        if (cc == 0)
                                        {
                                            ksdr[cc] = dr3[0].ToString();
                                            cc++;
                                            ksdr[cc] = dr3[2].ToString();
                                           // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
                                            cc++;
                                        }
                                        else
                                        {
                                            ksdr[cc] = dr3[2].ToString();
                                           // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
                                            cc++;
                                        }                                        
                                        idd2 = idd3;
                                    }                                    
                                }
                                ksdt.Rows.Add(ksdr);
                            }
                           
                        }
                    }
                    // END ADDING ROWS

                    gvCustomers.DataSource = ksdt;
                    gvCustomers.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        Loadperdayplantmilk();
    }
    protected void PrintCurrentPage(object sender, EventArgs e)
    {
        gvCustomers.AllowPaging = false;
        gvCustomers.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvCustomers.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=500,height=300,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        gvCustomers.AllowPaging = true;
        gvCustomers.DataBind();
    }
    protected void MChk_PlantName_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }
    private void Menu1()
    {
        if (MChk_PlantName.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
        }
    }
    private void CheckBoxListClear()
    {
        CheckBoxList1.Items.Clear();
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
         try
        {


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=PLANTPERDAYMILK.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvCustomers.AllowPaging = false;
                gvCustomers.DataBind();

                gvCustomers.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvCustomers.HeaderRow.Cells)
                {
                    cell.BackColor = gvCustomers.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvCustomers.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvCustomers.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvCustomers.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvCustomers.RenderControl(hw);

                //style to format numbers to string
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
    
}