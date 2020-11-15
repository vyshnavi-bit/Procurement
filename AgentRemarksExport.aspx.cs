using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
public partial class AgentRemarksExport : System.Web.UI.Page
{

    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string date1;
    string date2;
    string date11;
    string date22;
    DbHelper dbaccess = new DbHelper();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack!= true)
        {

            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
            dtm = System.DateTime.Now;
            dtm1 = System.DateTime.Now;
            txt_FromDate.Text = dtm.ToShortDateString();
            txt_ToDate.Text = dtm.ToShortDateString();
            txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
            txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
            if (roleid < 3)
            {
                LoadSinglePlantName();
            }
            if((roleid >= 3) && (roleid!=9))
            {
                LoadPlantName();
            }
            if (roleid == 9)
            {
                loadspecialsingleplant();
                Session["Plant_Code"] = "170".ToString();
                pcode = "170";
            }

        }
        else
        {
            ccode = Session["Company_code"].ToString();
            pcode = ddl_PlantName.SelectedItem.Value;
            Label44.Text = ddl_PlantName.SelectedItem.Text;

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



    public void getgriddata()
    {

        try
        {


            date1 = txt_FromDate.Text;
            date2 = txt_ToDate.Text;

            date11 = DateTime.ParseExact(date1, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            date22 = DateTime.ParseExact(date2, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

           //date11 = DateTime.ParseExact(date1, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
           //date22 = DateTime.ParseExact(date2, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connStr);
            string str;
            if (CheckBox1.Checked == true)
            {
              //  str = "select  Agent_id as AgentId ,convert(varchar,prdate,103) as Date,Sessions as Shift,SUM(Milk_kg-DIFFKG)as MilkKg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modifysnf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "'    and Prdate between '" + date11 + "' and '" + date22 + "' and (DIFFFAT > 0 or DIFFSNF > 0 or DIFFFAT < 0 or DIFFSNF < 0 ) and  (Remarkstatus=1  or Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by Agent_id ";
                str = "select  Agent_id as AgentId ,convert(varchar,prdate,103) as Date,Sessions as Shift,SUM(Milk_kg-DIFFKG)as MilkKg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modifysnf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "'    and Prdate between '" + date11 + "' and '" + date22 + "' and (DIFFKG > 0 or DIFFFAT > 0 or DIFFSNF > 0  ) and  (Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by rand(Agent_id) ";
            }
            else
            {
                str = "select  Agent_id as AgentId ,convert(varchar,prdate,103) as Date,Sessions as Shift,SUM(Milk_kg-DIFFKG)as MilkKg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modifysnf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "'    and Prdate between '" + date11 + "' and '" + date22 + "' and (DIFFKG < 0 or DIFFFAT < 0 or DIFFSNF < 0  ) and  (Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by rand(Agent_id) ";
                // str = "select  Agent_id as AgentId ,convert(varchar,prdate,103) as Date,Sessions as Shift,SUM(Milk_kg-DIFFKG)as MilkKg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modifysnf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "'    and Prdate between '" + date11 + "' and '" + date22 + "' and (DIFFFAT > 0 or DIFFSNF > 0 or DIFFFAT < 0 or DIFFSNF < 0 ) and  ( Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by Agent_id ";

            }
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GridView1.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                GridView1.HeaderStyle.Font.Bold = true;
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }

        catch
        {




        }

    }
    private void Get_IncreaseDecreaseAmountDetails()
    {
        try
        {
            string rpt_type = string.Empty;
            if (CheckBox1.Checked == true)
            {
                rpt_type = "1";
            }
            else
            {
                rpt_type = "2";
            }
            date1 = txt_FromDate.Text;
            date2 = txt_ToDate.Text;

            date11 = DateTime.ParseExact(date1, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            date22 = DateTime.ParseExact(date2, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            // Order by using Storeprocedure
            DataTable custdt = new DataTable();
            DataTable resdt = new DataTable();
            SqlConnection conn = null;
            using (conn = dbaccess.GetConnection())
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_IncreaseRateDifferents]");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", pcode);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", date11.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", date22.Trim());
                sqlCmd.Parameters.AddWithValue("@spReportType", rpt_type);
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);

                adp.Fill(resdt);

                if (resdt.Rows.Count > 0)
                {
                    GridView1.DataSource = resdt;
                    GridView1.DataBind();
                    GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                    GridView1.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                    GridView1.HeaderStyle.Font.Bold = true;

                    GridView1.FooterRow.Cells[0].Text = "Total";
                    decimal billadv = resdt.AsEnumerable().Sum(row => row.Field<decimal>("DAmount"));
                    GridView1.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[16].Text = billadv.ToString("N2");
                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }

            }
            //
        }
        catch (Exception ex)
        {
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_PlantName.SelectedItem.Text + "' " + " '"+ Label45.Text + "' " + " " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename );
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
               // getgriddata();
                Get_IncreaseDecreaseAmountDetails();

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
    protected void Button1_Click(object sender, EventArgs e)
    {


        getgriddata();
     //   Get_IncreaseDecreaseAmountDetails();
        Label45.Text="From Date:" + date1 + "To Date:" + date2  ;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double diffkg = Convert.ToDouble(e.Row.Cells[9].Text);
            double difffat = Convert.ToDouble(e.Row.Cells[10].Text);
            double diffsnf = Convert.ToDouble(e.Row.Cells[11].Text);

            
            if (CheckBox1.Checked == true)
            {

                if (difffat > 0)
                {

                    e.Row.Cells[10].Text = difffat.ToString();

                }
                else
                {
                    e.Row.Cells[10].Text = "";

                }
                if (diffsnf > 0)
                {

                    e.Row.Cells[11].Text = diffsnf.ToString();

                }
                else
                {
                    e.Row.Cells[11].Text = "";

                }
                if (diffkg > 0)
                {

                    e.Row.Cells[9].Text = diffkg.ToString();

                }
                else
                {
                    e.Row.Cells[9].Text = "";

                }



            }


            else
            {

                if (difffat < 0)
                {

                    e.Row.Cells[10].Text = difffat.ToString();

                }
                else
                {
                    e.Row.Cells[10].Text = "";

                }
                if (diffsnf < 0)
                {

                    e.Row.Cells[11].Text = diffsnf.ToString();

                }
                else
                {
                    e.Row.Cells[11].Text = "";

                }

                if (diffkg < 0)
                {

                    e.Row.Cells[9].Text = diffkg.ToString();

                }
                else
                {
                    e.Row.Cells[9].Text = "";

                }




            }
        }
        
    }

}