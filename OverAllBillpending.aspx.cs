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
using System.Drawing;

public partial class OverAllBillpending : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    public string Company_code;
    public string plant_Code;
    public string cname;
    public string d1, d2;
    int Data;
    int status;
    public static int roleid;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                LoadPlantName();
              
                Lbl_Errormsg.Visible = false;
                Label1.Visible = false;
                Image1.Visible = false;
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
               // LoadTransportplantstatus();
                Lbl_Errormsg.Visible = false;
                Label1.Visible = true;
                Image1.Visible = true;

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
            ds = BllPlant.LoadPlantNameChkLst1(Company_code.ToString());
            if (ds != null)
            {
                ddl_plantName.DataSource = ds;
                ddl_plantName.DataTextField = "Plant_Name";
                ddl_plantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_plantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        loadBillAmount();
    }

    private void Datechanged()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            d1 = dtm.ToString("MM/dd/yyyy");
            d2 = dtm1.ToString("MM/dd/yyyy");


        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void loadBillAmount()
    {
        {
            
            Datechanged();
            DataTable dt = new DataTable();
            double Gtotal = 0.0;

          //ok string cmd = "select CONVERT(NVARCHAR(50),Frm_date,101) AS  Frm_date,Route_id,Agent_id,sum(NetAmount) as NetAmount from Paymentdata WHERE Company_code='1' and  Plant_code='" + ddl_plantName.SelectedItem.Value + "' and Frm_date between '" + d1.Trim() + "' and '" + d2.Trim() + "'   group by Frm_date,Agent_id,Route_id ORDER BY Agent_id,Frm_date";
            string cmd = "SELECT t1.Frm_date AS Frm_date,t1.Route_id,t1.Agent_id,(t1.NetAmount-ISNULL(t2.Payamount,0)) As NetAmount FROM  " +
 "(SELECT CONVERT(NVARCHAR(50),Frm_date,101) AS Frm_date,Route_id,Agent_id,floor(SUM(NetAmount)) AS NetAmount from Paymentdata WHERE Company_code='1' and  Plant_code='" + ddl_plantName.SelectedItem.Value + "' and Frm_date between '" + d1.Trim() + "' and '" + d2.Trim() + "' group by Frm_date,Agent_id,Route_id) AS t1  " +
" LEFT JOIN " +
" (SELECT Agent_Id AS Aid,CONVERT(NVARCHAR(50),Billfrmdate,101) AS Billfrmdate,SUM(NetAmount) AS Payamount FROM BankPaymentllotment where plant_code='" + ddl_plantName.SelectedItem.Value + "'  AND Billfrmdate between '" + d1.Trim() + "' and '" + d2.Trim() + "' group by Billfrmdate,Agent_Id) AS t2 ON t1.Agent_id=t2.Aid AND t1.Frm_date=t2.Billfrmdate ORDER BY t1.Agent_id,t1.Frm_date";

            SqlDataAdapter adp = new SqlDataAdapter(cmd, connStr);
            adp.Fill(dt);   

            DataTable ksdt = new DataTable();
            DataColumn ksdc = null;
            DataRow ksdr = null;
            int counts = dt.Rows.Count;
          
            // START ADDING COLUMN
            if (counts > 0)
            {
                ksdc = new DataColumn("Agent");
                ksdt.Columns.Add(ksdc);
                foreach (DataRow dr1 in dt.Rows)
                {
                    object id;
                    id = dr1[0].ToString();//id = dr1[1].ToString();
                    string columnName = id.ToString();
                    DataColumnCollection columns = ksdt.Columns;
                    if (columns.Contains(columnName))
                    {

                    }
                    else
                    {
                        ksdc = new DataColumn(id.ToString());
                        ksdt.Columns.Add(ksdc);
                        
                    }

                }
                ksdc = new DataColumn("Total");
                ksdt.Columns.Add(ksdc);
            }
            // END ADDING COLUMN

            // START ADDING ROWS
            if (counts > 0)
            {
                object id2;
                id2 = 0;
                int idd2 = Convert.ToInt32(id2);

                foreach (DataRow dr2 in dt.Rows)
                {
                    ksdr = ksdt.NewRow();

                    object id1;
                    id1 = dr2[2].ToString();
                    int idd1 = Convert.ToInt32(id1);
                    if (idd1 == idd2)
                    {

                    }
                    else
                    {
                        int cc = 0;
                        foreach (DataRow dr3 in dt.Rows)
                        {
                            object id3;
                            id3 = dr3[2].ToString();
                            int idd3 = Convert.ToInt32(id3);
                            if (idd1 == idd3)
                            {
                                if (cc == 0)
                                {
                                   
                                    ksdr[cc] = dr3[2].ToString();//dr3[0].ToString();
                                    cc++;

                                    string C = dr3[0].ToString();
                                    ksdr[C] = dr3[3].ToString();
                                    // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
                                    cc++;
                                   
                                }
                                else
                                {

                                    string C = dr3[0].ToString();
                                    ksdr[C] = dr3[3].ToString();//ksdr[cc]
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




            if (dt.Rows.Count > 0)
            {
                Transport_ImportStatus.DataSource = ksdt;
                Transport_ImportStatus.DataBind();
            }
            else
            {
                Transport_ImportStatus.DataSource = dt;                
                Transport_ImportStatus.DataBind();
            }
        }
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        Exportxl();
    }

    private void Exportxl()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_plantName.Text + "'  " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                Transport_ImportStatus.AllowPaging = false;
                loadBillAmount();

                Transport_ImportStatus.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in Transport_ImportStatus.HeaderRow.Cells)
                {
                    cell.BackColor = Transport_ImportStatus.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in Transport_ImportStatus.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = Transport_ImportStatus.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = Transport_ImportStatus.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                Transport_ImportStatus.RenderControl(hw);

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