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


public partial class SameAccountnoList : System.Web.UI.Page
{
    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public int rid;
    public string managmobNo;
    public string pname;
    public string cname;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    //Admin Check Flag
    public int Falg = 0;
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();


    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;

    public int refNo = 0;
    string agentcode;

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



                if (roleid < 3)
                {
                    loadsingleplant();
                }
                if ((roleid >= 3) && (roleid != 9) )
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";


                }

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                if ((Lbl_msg.Text != string.Empty) && (Lbl_msg.Text!= "Label"))
                {

                    Lbl_msg.Visible = true;
                }
                else
                {

                    Lbl_msg.Visible = false;

                }
            //    Label111.Visible = false;

              //  Label6.Visible = false;
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
                ddl_plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
                pcode = ddl_plantcode.SelectedItem.Value;


                if ((Lbl_msg.Text != string.Empty) && (Lbl_msg.Text != "Label"))
                {

                    Lbl_msg.Visible = true;
                }
                else
                {

                    Lbl_msg.Visible = false;

                }

             //   Label111.Visible = false;

             
                //if (ddl_Agentname.SelectedValue == "1")
                //{
                //    Label111.Text = "AccountName";
                //    Label111.Visible = true;
                //}
                //if (ddl_Agentname.SelectedValue == "2")
                //{

                //    Label111.Text = "AgentName";
                //    Label111.Visible = true;
                //}
               // getgrid();
              // getgrid();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
         //   Label6.Visible = true;

        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
                ddl_plantcode.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

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

        ddl_plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_plantcode.SelectedItem.Value;


    }
    protected void txt_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (ddl_Agentname.Text != "0")
        {
            getgrid();
        }
        else
        {
            Lbl_msg11.Text = "Please Select AccountNo or Name";
            Lbl_msg11.Visible = true;

        }

    }





    public void getgrid()
    {



        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

       

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        String connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Get_SameAccountNoListofAgents";
        cmd.Connection = con;
        
        cmd.Parameters.Add("@Company_Code", SqlDbType.Int).Value = ccode;
        cmd.Parameters.Add("@Plant_Code", SqlDbType.Int).Value = pcode;
        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = ddl_Agentname.Text;
        cmd.Parameters.Add("@frmdate", SqlDbType.NVarChar).Value = d1;
        cmd.Parameters.Add("@todate", SqlDbType.NVarChar).Value = d2;

        

        cmd.Connection = con;
        try
        {
            con.Open();
            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            //GridView1.HeaderStyle.BackColor = Color.Silver;
            //GridView1.HeaderStyle.ForeColor = Color.White;
            GridView1.HeaderStyle.BackColor = Color.White;
            GridView1.HeaderStyle.ForeColor = Color.Brown;

            Lbl_msg.Text = "Plant Name:" + ddl_Plantname.Text;
            if(ddl_Agentname.SelectedValue == "1")
            {
                Lbl_msg11.Text = "Selecting Type:AccountName";
                Lbl_msg11.Visible = true;
            }
            if(ddl_Agentname.SelectedValue == "2")
            {

                Lbl_msg11.Text = "Selecting Type:Account Number";
                Lbl_msg11.Visible = true;
            }

            if ((Lbl_msg.Text != string.Empty) && (Lbl_msg.Text != "Label"))
            {

                Lbl_msg.Visible = true;
            }
            else
            {

                Lbl_msg.Visible = false;

            }

        }
        catch (Exception ex)
        {
            throw ex;
            Lbl_msg.Visible = false;
        }
        finally
        {
          
            con.Close();
            con.Dispose();
           
        }
    }
    protected void btn_print_Click(object sender, EventArgs e)
    {

    }
    protected void d(object sender, EventArgs e)
    {

    }
    protected void btn_print_Click1(object sender, EventArgs e)
    {

    }
    protected void btn_print12_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
      


        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_Plantname.Text + "'  " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                getgrid();

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
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Agents Same Account or Name List";
            HeaderCell2.ColumnSpan = 6;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;


        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Width = 250;
        e.Row.Cells[5].Width = 150;


    }
    protected void txt_FromDate_TextChanged1(object sender, EventArgs e)
    {

    }
}