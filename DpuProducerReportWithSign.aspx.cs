using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;

public partial class DpuProducerReportWithSign : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    BLLuser Bllusers = new BLLuser();
    string fatkg;
    string snfkg;
    DateTime dtm;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    string agentid;
    string date1;
    string date11;
    string date2;
    string date22;
    SqlConnection con = new SqlConnection();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
           // managmobNo = Session["managmobNo"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());

            dtm = System.DateTime.Now;
            txt_FromDate.Text = dtm.ToShortDateString();
            txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
            txt_ToDate.Text = dtm.ToShortDateString();
            txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
            lblmsg.Visible = false;
            if (roleid < 3)
            {
                loadsingleplant();
            }
            else
            {

                LoadPlantcode();

            }
        }

        else
        {

            pcode = ddl_Plantcode.SelectedItem.Text;
            lblmsg.Visible = false;
        }
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        getgriddata();
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;

            dr = Bllusers.LoadPlantcode(ccode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Text;
        getagentid();
    }

    public void getagentid()
    {

        try
        {
            ddl_Agentname.Items.Clear();
            //     txt_AgentName.Text = "";

            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select distinct agent_id,Agent_Name from agent_master where plant_code='" + pcode + "'      order by Agent_id  asc ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                  
                    // txt_AgentName.Text = dr["Agent_Name"].ToString();
                    ddl_agentid.Items.Add(dr["agent_id"].ToString());
                    ddl_Agentname.Items.Add(dr["agent_id"].ToString() + "_" + dr["Agent_Name"].ToString());

                  

                }
            }
            else
            {

                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "No Agents";


            }



        }

        catch
        {

            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Text = "Please Check";

        }



    }
    protected void ddl_AgentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  agentid = ddl_AgentId.SelectedValue;

    }
    public void getgriddata()
    {

        try
        {
            date1 = txt_FromDate.Text;
            date2 = txt_ToDate.Text;

            date11 = DateTime.ParseExact(date1, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            date22 = DateTime.ParseExact(date2, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str;
           // str = "select   Convert(nvarchar(50),ProducerCode)+'_'+ISNULL(aa.Producer_Name,'') as ProducerName,aa.milkkg as Milkkg,aa.milkltr as MilkLtr,fat as fat,snf as snf,Rate as Rate,Amount,Amount as  Producer_Signature from(select distinct(Producer_Id) as ProducerCode,Producer_Name,c.milkkg as Milkkg,c.milkltr as MilkLtr,fat as fat,snf as snf,Rate as Rate,Amount,Amount as  Producer_Signature from(select * from(select Agent_id,Producer_Id,Prdate,Route_id,sum(Milk_kg) as milkkg,sum(Milk_ltr)as milkltr ,sum(fat) as fat,sum(snf) as snf,sum(Rate) as Rate,sum(Amount) as Amount  from ProducerProcurement   where Plant_Code='"+pcode+"'  and prdate between '" + date11 + "' and '" + date22 + "' and Agent_id='" + ddl_agentid.SelectedItem.Value + "' group by Agent_id,Producer_Id,prdate,Route_id,fat,snf,milk_kg )as a left join (select agent_id as proagent,Producer_Name from DPUPRODUCERMASTER   where Plant_code='" + pcode + "' and  Agent_Id='" + ddl_agentid.SelectedItem.Value + "') as b  on a.Agent_id=b.proagent) as c left join(select Route_ID,Route_Name from Route_Master  where Plant_Code='" + pcode + "') as d on c.Route_id=d.Route_ID) as aa";
            str = "select   Convert(nvarchar(50),ProducerCode)+'_'+ISNULL(aa.Producer_Name,'') as ProducerName,aa.milkkg as Milkkg,aa.milkltr as MilkLtr,fat as fat,snf as snf,Rate as Rate,Amount,Amount as  Producer_Signature from(select distinct(Producer_Id) as ProducerCode,Producer_Name,c.milkkg as Milkkg,c.milkltr as MilkLtr,fat as fat,snf as snf,Rate as Rate,Amount,Amount as  Producer_Signature from " +
 " (select * from " +
 " (select Producer_Id,sum(Milk_kg) as milkkg,sum(Milk_ltr)as milkltr ,sum(fat) as fat,sum(snf) as snf,sum(Rate) as Rate,sum(Amount) as Amount  from ProducerProcurement   where Plant_Code='" + pcode + "'  and prdate between '" + date11 + "' and '" + date22 + "' and Agent_id='" + ddl_agentid.SelectedItem.Value + "' group by Producer_Id )as a " +
 " left join " +
 " (select Producer_Code as proagent,Producer_Name,Route_id from DPUPRODUCERMASTER   where Plant_code='" + pcode + "' and  Agent_Id='" + ddl_agentid.SelectedItem.Value + "') as b  on a.Producer_Id=b.proagent) as c " +
 " left join " +
 " (select Route_ID AS Rid,Route_Name from Route_Master  where Plant_code='" + pcode + "') as d on c.Route_id=d.Rid) as aa";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
               //// GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GridView1.HeaderStyle.BackColor = System.Drawing.Color.DarkSlateBlue;
                GridView1.HeaderStyle.Font.Bold = true;
                GridView1.FooterRow.Cells[2].Text = dt.Compute("Sum(MILKKG)", "").ToString();
                GridView1.FooterRow.Cells[3].Text = dt.Compute("Sum(Milkltr)", "").ToString();
                GridView1.FooterRow.Cells[7].Text = dt.Compute("Sum(Amount)", "").ToString();
                GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                double fatkg1 = Convert.ToDouble(dt.Compute("Avg(fat)", "").ToString());
                GridView1.FooterRow.Cells[4].Text = fatkg1.ToString("N2");
                double snfkg2 = Convert.ToDouble(dt.Compute("Avg(snf)", "").ToString());
                GridView1.FooterRow.Cells[5].Text = snfkg2.ToString("N2");
                double rate1 = Convert.ToDouble(dt.Compute("Avg(rate)", "").ToString());
                GridView1.FooterRow.Cells[6].Text = rate1.ToString("N2");
                GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                GridView1.FooterRow.Cells[0].Text = string.Empty;
                GridView1.FooterRow.Cells[0].Text = string.Empty;
                GridView1.FooterRow.Cells[1].Text = "Total";

                //GridView1.FooterRow.ForeColor = System.Drawing.Color.White;
                //GridView1.FooterRow.BackColor = System.Drawing.Color.DarkSlateBlue;
                GridView1.FooterRow.Font.Bold = true;
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
                lblmsg.Text = "NO ROWS";
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;

            }
        }


        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell3 = new TableCell();
            TableCell HeaderCell4 = new TableCell();

            HeaderCell3.Text = "Agents code:" + ddl_Agentname.Text + "--From Date:" + date1 + " To Date:" + date2 + "";
            HeaderCell3.ColumnSpan = 10;
            HeaderCell3.Attributes.CssStyle["text-align"] = "center";
            HeaderRow.Cells.Add(HeaderCell3);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell3.Font.Bold = true;


            //HeaderCell3.ForeColor = System.Drawing.Color.White;
            //HeaderCell3.BackColor = System.Drawing.Color.DarkSlateBlue;



        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           string str = e.Row.Cells[8].Text;
           e.Row.Cells[8].Text = "";

          
        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {
           // e.Row.Cells[9].Text = dVal.ToString("c");
        }



    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
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
                getgriddata();

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
    protected void ddl_Agentname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_agentid.SelectedIndex = ddl_Agentname.SelectedIndex;
        agentid = ddl_agentid.SelectedItem.Text;

    }
}