using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;


public partial class RatechartAgentList : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();

    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    public string strsql = string.Empty;
    public string d1, d2;
    public int ratechartmodeltype;
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
             //   managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;
                txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
                txt_ToDate.Text = tdt.ToString("dd/MM/yyy");

                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                else
                {
                    LoadPlantName();
                }
                pcode = ddl_PlantName.SelectedItem.Value;

                Lbl_Errormsg.Visible = false;

                Label1.Visible = false;
                Label4.Visible = false;
                Label7.Visible = false;
                Image1.Visible = false;
                Label8.Visible = false;
                

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
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_PlantName.SelectedItem.Value;

                Lbl_Errormsg.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                Label7.Visible = false;
                Image1.Visible = false;
                Label8.Visible = false;
                
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
    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
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

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        try
        {
            Get_UsedRatechartDetailsPlant();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
        
    }

    private void Datechanged()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            d1 = dtm.ToString("MM/dd/yyyy");
            d2 = dtm1.ToString("MM/dd/yyyy");

            string ratechartemode = Chk_ratechartemode.SelectedItem.Value;
            if (ratechartemode == "Plant")
            {
                ratechartmodeltype = 1;
            }
            else if (ratechartemode == "Route")
            {
                ratechartmodeltype = 2;
            }
            else if (ratechartemode == "Agent")
            {
                ratechartmodeltype = 3;
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
   
     private void Get_UsedRatechartDetailsPlant()
    {
        try
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Datechanged();   
           
            DataSet ds = new DataSet();
            
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_UsedRatechartDetails]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", ddl_PlantName.SelectedItem.Value);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                sqlCmd.Parameters.AddWithValue("@spratechartmodeltype", ratechartmodeltype);
                sqlCmd.Parameters.AddWithValue("@spcount", 1);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                //             
                 
                 if (ds!=null)
                 {
                    
                     string ratechartemode = Chk_ratechartemode.SelectedItem.Value;
                     if (ratechartemode == "Plant")
                     {
                         Label1.Text = "PlantRatechart AgentList";
                         Label4.Text = "From :" + txt_FromDate.Text.Trim() + "  To :" + txt_ToDate.Text.Trim();
                     }
                     else if (ratechartemode == "Route")
                     {
                         Label1.Text = "RoutewiseRatechart AgentList";
                         Label4.Text = "From :" + txt_FromDate.Text.Trim() + "  To :" + txt_ToDate.Text.Trim();
                     }
                     else if (ratechartemode == "Agent")
                     {
                         Label1.Text = "AgentwiseRatechart AgentList";
                         Label4.Text = "From :" + txt_FromDate.Text.Trim() + "  To :" + txt_ToDate.Text.Trim();
                     }
                     else
                     {
                     }

                     if (ds != null)
                     {
                         ddl_RateChartName.DataSource = ds;
                         ddl_RateChartName.DataTextField = "Chart_Name";
                         ddl_RateChartName.DataValueField = "Id";
                         ddl_RateChartName.DataBind();
                     }           
                    
                 }
                 else
                 {

                 }
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

     protected void btn_Get_Click(object sender, EventArgs e)
     {
         try
         {
             Datechanged();
             strsql = "SELECT Distinct(agent_id) AS Agent_Id FROM Procurement Where Plant_Code='" + pcode + "' AND Prdate between'" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' AND RateStatus='" + ratechartmodeltype + "' AND Ratechart_Id='" + ddl_RateChartName.SelectedItem.Text.Trim() + "'";
             using (con = DBaccess.GetConnection())
             {
                 SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                 da.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     Label1.Visible = true;
                     Label4.Visible = true;
                     Label7.Visible = true;
                     Image1.Visible = true;
                     Label7.Text = ddl_RateChartName.SelectedItem.Text.Trim();
                     Label8.Visible = true;
                     Label8.Text = ddl_PlantName.SelectedItem.Text.Trim();
                     GridView1.DataSource = dt;
                     GridView1.DataBind();
                 }
             }
         }
         catch (Exception ex)
         {
             GridView1.DataSource = dt;
             GridView1.DataBind();
             Lbl_Errormsg.Visible = true;
             Lbl_Errormsg.Text = ex.ToString();
         }
     }
     protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         GridView1.PageIndex = e.NewPageIndex;
         GridView1.DataBind();
     }
     protected void btn_okPRINT_Click(object sender, EventArgs e)
     {

         GridView1.UseAccessibleHeader = true;
         GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
         GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
         GridView1.Attributes["style"] = "border-collapse:separate";
         foreach (GridViewRow row in GridView1.Rows)
         {
             if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
             {
                 row.Attributes["style"] = "page-break-after:always;";
             }
         }
         StringWriter sw = new StringWriter();
         HtmlTextWriter hw = new HtmlTextWriter(sw);
         GridView1.RenderControl(hw);
         string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
         StringBuilder sb = new StringBuilder();
         sb.Append("<script type = 'text/javascript'>");
         sb.Append("window.onload = new function(){");
         sb.Append("var printWin = window.open('', '', 'left=0");
         sb.Append(",top=0,width=1000,height=600,status=0');");
         sb.Append("printWin.document.write(\"");
         string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
         sb.Append(style + gridHTML);
         sb.Append("\");");
         sb.Append("printWin.document.close();");
         sb.Append("printWin.focus();");
         sb.Append("printWin.print();");
         sb.Append("printWin.close();");
         sb.Append("};");
         sb.Append("</script>");
         ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
         GridView1.DataBind();


     }
}