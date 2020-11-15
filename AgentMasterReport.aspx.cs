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

public partial class AgentMasterReport : System.Web.UI.Page
{

    SqlDataReader dr;
    public string ccode;
    public string pcode;
    public int rid;
    public string managmobNo;
    public string pname;
    public string cname;
    public string plantname;
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


    string sqlstr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
             //   managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

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
                pcode = ddl_PlantName.SelectedItem.Value;
                plantname = ddl_PlantName.SelectedItem.Text;
                getagetlist();
                


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
                roleid = Convert.ToInt32(Session["Role"].ToString());
             //   managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_PlantName.SelectedItem.Value;
                plantname = ddl_PlantName.SelectedItem.Text;
                getagetlist();
            

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = DBaccess.GetConnection();
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
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        getagetlist();
    }

    public void getagetlist()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        SqlConnection con = new SqlConnection(sqlstr);
        string str;
        con.Open();
     //   str = "select  c.Agent_Id as CanNo,c.Agent_Name  as Name,d.Route_Name  as RouteName,c.RatechartType as ChartType,c.Mobile,c.Type,c.Cartage_Amt as CartAmt,c.SplBonus_Amt as SPlAmt,c.Bank_Name as BankName,c.Ifsc_code as Ifsc,c.Agent_AccountNo as Acc.No,c.Milk_Nature as MilkMode  from (select * from(select Agent_Id as CanNo,Agent_Name as Name,Bank_Id as Bankid,phone_Number as Mobile,Milk_Nature,Agent_AccountNo,Ifsc_code as ifsccode,Route_id,AgentRateChartmode  as ChartType,Type,Cartage_Amt as CartAmt,SplBonus_Amt as SPlAmt from agent_master where plant_code='" + pcode + "') as a  left join (select *  from  BANK_MASTER where Plant_code='" + pcode + "'   ) as b on a.Bankid=b.Bank_ID and a.ifsccode=b.Ifsc_code ) as c left join(select *   from Route_Master  where Plant_Code='" + pcode + "') as d on c.Route_id=d.Route_ID  order by c.agent_id ";
        //str = "select  c.Agent_Id ,c.Agent_Name ,d.Route_Name ,c.AgentRateChartmode ,c.Mobile,c.Type,c.Cartage_Amt,c.SplBonus_Amt,c.Bank_Name ,c.ifsc,c.Agent_AccountNo ,c.Milk_Nature from (select * from(select Agent_Id,Agent_Name ,Bank_Id as bankid ,phone_Number  as Mobile,Milk_Nature,Agent_AccountNo,Ifsc_code as ifsc,Route_id,AgentRateChartmode  ,Type,Cartage_Amt ,SplBonus_Amt  from agent_master where plant_code='" + pcode + "' group by  Agent_Id,Agent_Name ,Bank_Id ,phone_Number ,Milk_Nature,Agent_AccountNo, Ifsc_code,Route_id,AgentRateChartmode  ,Type,Cartage_Amt ,SplBonus_Amt) as a  left join (select *  from  BANK_MASTER where Plant_code='" + pcode + "'   ) as b on a.bankid=b.Bank_ID and a.ifsc=b.Ifsc_code ) as c left join(select *   from Route_Master  where Plant_Code='" + pcode + "') as d on c.Route_id=d.Route_ID  order by c.agent_id ";
     //   str = "select  c.Agent_Id ,c.Agent_Name ,d.Route_Name ,c.AgentRateChartmode ,c.Mobile,c.Type,c.Cartage_Amt,c.SplBonus_Amt,c.Bank_Name ,c.ifsc,c.Agent_AccountNo ,c.Milk_Nature from (select * from(select Agent_Id,Agent_Name ,Bank_Id as bankid ,phone_Number  as Mobile,Milk_Nature,Agent_AccountNo,Ifsc_code as ifsc,Route_id,AgentRateChartmode  ,Type,Cartage_Amt ,SplBonus_Amt  from agent_master where plant_code='" + pcode + "' group by  Agent_Id,Agent_Name ,Bank_Id ,phone_Number ,Milk_Nature,Agent_AccountNo, Ifsc_code,Route_id,AgentRateChartmode  ,Type,Cartage_Amt ,SplBonus_Amt) as a  left join (select *  from  BANK_MASTER where Plant_code='" + pcode + "'   ) as b on a.bankid=b.Bank_ID and a.ifsc=b.Ifsc_code ) as c left join(select *   from Route_Master  where Plant_Code='" + pcode + "') as d on c.Route_id=d.Route_ID  order by c.agent_id ";

        str = "SELECT DISTINCT(AGEN.Agent_Id) as AgentId ,AGEN.Agent_Name as Name ,AGEN.Route_Name as Routename,AGEN.AgentRateChartmode as chart,AGEN.Mobile as Mobile,AGEN.Type as Type,AGEN.Cartage_Amt as Cart,AGEN.SplBonus_Amt as Spl,AGEN.Bank_Name as Bankname ,AGEN.ifsc as Ifsc,AGEN.Agent_AccountNo as Accno ,AGEN.Milk_Nature as Nature FROM (SELECT *   FROM Paymentdata WHERE PLANT_CODE='" + pcode + "' AND  Frm_date >= '" + d1 + "' AND  To_date <= '" + d2 + "' ) AS PRO LEFT JOIN  ( select  c.Agent_Id ,c.Agent_Name ,d.Route_Name ,c.AgentRateChartmode ,c.Mobile,c.Type,c.Cartage_Amt,c.SplBonus_Amt,c.Bank_Name ,c.ifsc,c.Agent_AccountNo ,c.Milk_Nature from (select * from(select Agent_Id,Agent_Name ,Bank_Id as bankid ,phone_Number  as Mobile,Milk_Nature,Agent_AccountNo,Ifsc_code as ifsc,Route_id,AgentRateChartmode  ,Type,Cartage_Amt ,SplBonus_Amt  from agent_master where plant_code='" + pcode + "' group by  Agent_Id,Agent_Name ,Bank_Id ,phone_Number ,Milk_Nature,Agent_AccountNo, Ifsc_code,Route_id,AgentRateChartmode  ,Type,Cartage_Amt ,SplBonus_Amt) as a  left join (select *  from  BANK_MASTER where Plant_code='" + pcode + "'   ) as b on a.bankid=b.Bank_ID and a.ifsc=b.Ifsc_code ) as c left join(select *   from Route_Master  where Plant_Code='" + pcode + "') as d on c.Route_id=d.Route_ID   ) AS AGEN ON  PRO.Agent_id=AGEN.Agent_Id  ";
        DataTable dt =new  DataTable();
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //Lbl_msg.Visible = true;
            //Lbl_msg.Text = ddl_PlantName.SelectedItem.Text;
            //Lbl_msg.ForeColor = System.Drawing.Color.Green;
            //Lbl_Title.Visible = true;
            //Lbl_Title.Text = "Agents Detailed Report";
            //Lbl_Title.ForeColor = System.Drawing.Color.Green;

        }
        else
        {
            //Lbl_msg.Visible = false;
            //Lbl_Title.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();

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
    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_print_Click1(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //GridView1.Columns[8].ItemStyle.Width = 2000;
            //GridView1.Columns[8].ControlStyle.Width = 2000;
         //   grdListings.Columns[2].ItemStyle.Width = 150;
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = plantname + ":Procurement Agent List";
            HeaderCell2.ColumnSpan = 13;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
          
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
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
                getagetlist();

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



    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}