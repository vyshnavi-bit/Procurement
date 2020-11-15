using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
public partial class Rpt_StockDetail : System.Web.UI.Page
{
    int ccode = 1, pcode;
    string sqlstr = string.Empty;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    SqlCommand cmd = new SqlCommand();
    DateTime dat = new DateTime();

    public string managmobNo;
    public string pname;
    public string cname;

    string d1 = string.Empty;
    string d2 = string.Empty;
    int count, count1, count2;
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
              //  managmobNo = Session["managmobNo"].ToString();
                dat = System.DateTime.Now;
                txt_FromDate.Text = dat.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dat.ToString("dd/MM/yyyy");
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
                rbtLstReportItems.SelectedIndex = 0;
                ddl_PlantName.Visible = false;
                Label13.Visible = false;
                lblpname.Visible = false;
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
                Label13.Visible = false;
                lblpname.Visible = false;
              
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

    protected void rbtLstReportItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
                {
                    Label13.Visible = false;
                    lblpname.Visible = false;
                    GodownStockDetails.Visible = true;
                    PlantStockDetails.Visible = false;
                    GetGodownStockdetails();
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {
                    Label13.Visible = true;
                    lblpname.Visible = true;
                    PlantStockDetails.Visible = true;
                    GodownStockDetails.Visible = false;
                    
                    ddl_PlantName.Visible = true;
                    GetPlantStockdetails();
                }
                else
                {
                }
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Select the Type...";
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }



    private void Datefunc()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();


        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        lbldate.Text = "Entry Date:" + dt1.ToString("dd/MM/yyyy") + "To:" + dt2.ToString("dd/MM/yyyy");

        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");

    }

    private void GetGodownStockdetails()
    {
        DataTable dt = new DataTable();
        con = new SqlConnection();
        Datefunc();
        try
        {
            using (con = dbaccess.GetConnection())
            {
                if (Chk_AllorStock.Checked == true)
                {
                 
                    sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY StockGroupID) AS Sno,CONVERT(nvarchar(15),StockGroupID)+'_'+StockGroup AS Header,CONVERT(nvarchar(15),StockSubGroupID)+'_'+StockSubGroup1 AS SubGroup,Sqty FROM " +
" (SELECT stockcode,StockSubgroup,SUM(Qty) AS Sqty FROM AdminStockMaster Where  AddedDate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY StockSubgroup,stockcode) AS t1  " +
" LEFT JOIN " +
" (SELECT StockGroupID,StockSubGroupID,StockGroup,StockSubGroup AS StockSubGroup1  FROM Stock_Master ) AS t2 ON  t1.stockcode=t2.StockGroupID AND t1.StockSubgroup=t2.StockSubGroupID ORDER BY stockcode,StockSubgroup ";
                }
                else
                {
                    sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY AddedDate) AS Sno,CONVERT(nvarchar(15),stockcode)+'_'+StockGroup AS Header,CONVERT(nvarchar(15),StockSubgroup)+'_'+ StockSubGroup1 AS SubGroup,Qty,AddedDate FROM  " +
    " (SELECT stockcode,StockSubgroup,Qty,AddedDate FROM AdminStockMaster Where AddedDate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') AS t1 " +
    " LEFT JOIN " +
    " (SELECT StockGroupID,StockSubGroupID,StockGroup,StockSubGroup AS StockSubGroup1  FROM Stock_Master ) AS t2 ON t1.stockcode=t2.StockGroupID AND t1.StockSubgroup=t2.StockSubGroupID ORDER BY AddedDate,stockcode,StockSubgroup ";

                }

                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                da.Fill(dt);
                count1 = dt.Rows.Count;
                GodownStockDetails.Visible = false;
                GodownStockDetails.Visible = true;
                GodownStockDetails.DataSource = dt;
                GodownStockDetails.DataBind();                
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }


    }

    private void GetPlantStockdetails()
    {
        DataTable dt = new DataTable();
        con = new SqlConnection();
        Datefunc();
        try
        {
            using (con = dbaccess.GetConnection())
            {
                if (Chk_AllorStock.Checked == true)
                {
                    sqlstr = " SELECT  ROW_NUMBER() OVER(ORDER BY StockGroupID) AS Sno,CONVERT(nvarchar(15),StockGroupID)+'_'+StockGroup AS Header,CONVERT(nvarchar(15),StockSubGroupID)+'_'+StockSubGroup1 AS SubGroup,Sqty  FROM " +
" (SELECT stockcode,StockSubgroup,SUM(ISNULL(Qty,0)) AS Sqty FROM PlantStockMaster Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND AddedDate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "' GROUP BY stockcode,StockSubgroup) AS t1 " +
" LEFT JOIN " +
" (SELECT StockGroupID,StockSubGroupID,StockGroup,StockSubGroup AS StockSubGroup1  FROM Stock_Master ) AS t2 ON t1.stockcode=t2.StockGroupID AND t1.StockSubgroup=t2.StockSubGroupID ORDER BY stockcode,StockSubgroup ";
                   
                }
                else
                {
                    sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY AddedDate) AS Sno,CONVERT(nvarchar(15),stockcode)+'_'+StockGroup AS Header,CONVERT(nvarchar(15),StockSubgroup)+'_'+ StockSubGroup1 AS SubGroup,Qty,AddedDate FROM " +
" (SELECT stockcode,StockSubgroup,Qty,AddedDate FROM PlantStockMaster Where Plant_Code='" + ddl_PlantName.SelectedItem.Value.Trim() + "' AND AddedDate BETWEEN '" + d1.ToString().Trim() + "' AND '" + d2.ToString().Trim() + "') AS t1 " +
" LEFT JOIN " +
" (SELECT StockGroupID,StockSubGroupID,StockGroup,StockSubGroup AS StockSubGroup1  FROM Stock_Master ) AS t2 ON t1.stockcode=t2.StockGroupID AND t1.StockSubgroup=t2.StockSubGroupID ORDER BY AddedDate,stockcode,StockSubgroup";

                }

                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                da.Fill(dt);
                count1 = dt.Rows.Count;
                PlantStockDetails.Visible = false;
                PlantStockDetails.Visible = true;
                PlantStockDetails.DataSource = dt;
                PlantStockDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }


    }

    protected void btn_Generate_Click(object sender, EventArgs e)
    {

        try
        {
            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
                {

                    GetGodownStockdetails();
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {
                    ddl_PlantName.Visible = true;
                    GetPlantStockdetails();
                }
                else
                {
                }
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Select the Type...";
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    
}