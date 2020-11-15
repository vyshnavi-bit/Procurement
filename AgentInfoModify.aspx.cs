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

public partial class AgentInfoModify : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable agentdataviews = new DataTable();
    DataTable filename = new DataTable();
    DataTable viewloanrecovery = new DataTable();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
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
                    GridView2.Visible = true;
                    LoadPlantcode();
                    getagentlist();
                  //  pcode = ddl_Plantname.SelectedItem.Value;
              
                }
                else
                {

                   // pname = ddl_Plantname.SelectedItem.Text;


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

              //  pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {


        }
    }
    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)  order by  Plant_Code asc";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }

    protected void btn_get_Click(object sender, EventArgs e)
    {
        getagentlist();
    }
    public void getagentlist()
    {

        string value = "";
        con = DB.GetConnection();
      //   value = "Select   agent_id,Agent_name,Mobile,cartage,splbonus,BankName,ifsccode,accno,Nagent_id,NAgent_name,NMobile,Ncartage,Nsplbonus,NBankName,NBranchName,Nifsccode,Naccno,Username,convert(varchar,Datetime) as date    from agent_masterLogs     order by Tid   desc";
        if (RadioButtonList1.SelectedItem.Value == "1")
        {
            value = "sELECT agent_id,Agent_name,Mobile,cartage,splbonus,BMBANK_NAME as  BankName,ifsccode,accno,Nagent_id,NAgent_name,NMobile,Ncartage,Nsplbonus,NBankName,NBranchName,Nifsccode,Naccno,Username,Date   FROM (Select  tid,agent_id,Agent_name,Mobile,cartage,splbonus,BankName,ifsccode,accno,Nagent_id,NAgent_name,NMobile,Ncartage,Nsplbonus,NBankName,NBranchName,Nifsccode,Naccno,Username,convert(varchar,Datetime,109) as date    from agent_masterLogs ) AS LL  LEFT JOIN (sELECT Bank_ID,BANK_NAME as  BMBANK_NAME   FROM BANK_MASTER   GROUP BY Bank_ID,BANK_NAME ) AS BM  ON LL.BankName=BM.Bank_ID  order by   tid   desc";
        }
        else
        {
            value = "sELECT agent_id,Agent_name,Mobile,cartage,splbonus,BMBANK_NAME as  BankName,ifsccode,accno,Nagent_id,NAgent_name,NMobile,Ncartage,Nsplbonus,NBankName,NBranchName,Nifsccode,Naccno,Username,Date   FROM (Select  tid,agent_id,Agent_name,Mobile,cartage,splbonus,BankName,ifsccode,accno,Nagent_id,NAgent_name,NMobile,Ncartage,Nsplbonus,NBankName,NBranchName,Nifsccode,Naccno,Username,convert(varchar,Datetime,109) as date    from agent_masterLogs  where plant_code='"+ddl_Plantname.SelectedItem.Value+"') AS LL  LEFT JOIN (sELECT Bank_ID,BANK_NAME as  BMBANK_NAME   FROM BANK_MASTER   GROUP BY Bank_ID,BANK_NAME ) AS BM  ON LL.BankName=BM.Bank_ID  order by   tid   desc";

        }
         SqlCommand cmd = new SqlCommand(value, con);
         SqlDataAdapter dst = new SqlDataAdapter(cmd);
         agentdataviews.Rows.Clear();
         dst.Fill(agentdataviews);
         if (agentdataviews.Rows.Count > 0)
         {

             GridView2.DataSource = agentdataviews;
             GridView2.DataBind();

         }
         else
         {

             GridView2.DataSource = "";
             GridView2.DataBind();

         }

    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_export_Click(object sender, EventArgs e)
    {

        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "' " + DateTime.Now.ToString() + ".xls";
            getagentlist();
            //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.AddHeader("content-disposition", "attachment; filename= '"+filename+"'");
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;
                getagentlist();
                GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView2.HeaderRow.Cells)
                {
                    cell.BackColor = GridView2.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView2.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView2.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView2.RenderControl(hw);

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

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedItem.Value == "1")
        {
            Panel1.Visible = false;
        }
        else
        {
            Panel1.Visible = true;
        }
    }
}