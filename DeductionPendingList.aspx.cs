using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
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
using System.IO;
using System.Collections.Generic;



public partial class DeductionPendingList : System.Web.UI.Page
{
   DataSet ds = new DataSet();
   BLLPlantName BllPlant = new BLLPlantName();
   DbHelper dbaccess = new DbHelper();
   SqlConnection con;
   int ccode = 1, pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
              //  managmobNo = Session["managmobNo"].ToString();
                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = 170;

                }
            //    pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible = false;
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
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        getgridview();
    }
    public void getgridview()
    {

        try
        {
            using (con = dbaccess.GetConnection())
            {
                string str = "select    ROW_NUMBER() OVER(ORDER BY dd.agent_id asc) AS Sno, dd.agent_id as CanNo,am.Agent_Name as AgentName,Convert(varchar,dd.deductiondate,103) as Date,dd.can as Can,dd.Ai,dd.Feed,dd.billadvance as Billadvance ,dd.others as Others,dd.recovery as Recovery from(Select agent_id,deductiondate,can,Ai,Feed,billadvance,others,recovery   from  Deduction_Details   where   Plant_Code='" + ddl_PlantName.SelectedValue + "' and (billadvance > 0  or Ai > 0 or feed > 0 or can > 0 or  recovery > 0 or others > 0 ) and status=0  )  as dd left join (select agent_id,Agent_Name  from Agent_Master where Plant_Code='" + ddl_PlantName.SelectedValue + "'  ) as am on dd.agent_id=am.Agent_Id  order  by  dd.agent_id asc ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();


                    GridView1.FooterRow.Cells[1].Text = "Total";

                    decimal Can = dt.AsEnumerable().Sum(row => row.Field<decimal>("can"));
                    GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[4].Text = Can.ToString("N2");

                    decimal Ai = dt.AsEnumerable().Sum(row => row.Field<decimal>("Ai"));
                    GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[5].Text = Ai.ToString("N2");

                    decimal Feed = dt.AsEnumerable().Sum(row => row.Field<decimal>("Feed"));
                    GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[6].Text = Feed.ToString("N2");

                    decimal Billadvance = dt.AsEnumerable().Sum(row => row.Field<decimal>("Billadvance"));
                    GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[7].Text = Billadvance.ToString("N2");

                    decimal Others = dt.AsEnumerable().Sum(row => row.Field<decimal>("Others"));
                    GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[8].Text = Others.ToString("N2");

                    decimal Recovery = dt.AsEnumerable().Sum(row => row.Field<decimal>("Recovery"));
                    GridView1.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;
                    GridView1.FooterRow.Cells[9].Text = Recovery.ToString("N2");


                    GridView1.FooterStyle.BackColor = Color.Brown;
                    GridView1.FooterStyle.ForeColor = Color.White;

                }
                else
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }
    protected void FinanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_PlantName.SelectedItem.Text + "'  " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                getgridview();
              //  Get_IncreaseDecreaseAmountDetails();

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