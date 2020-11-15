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
using System.IO;


public partial class AgentMissingList : System.Web.UI.Page
{
    
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;

      int ccode = 1, pcode;
     string sqlstr = string.Empty;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();

    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
   
    DateTime dt1 = new DateTime();
    DateTime dt2 = new DateTime();
    string d1;
    string d2;
    string connSttr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
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
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantName();
                    pcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
                }
                if(roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = 170;
                    loadspecialsingleplant();
                }
 
                lblfrmdate.Visible = false;
          
                lblpcode.Visible = false;
                lbltodate.Visible = false;
             
                
            }

            else
            {

                pcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);


            }

        }
        else
        {
            ccode = Convert.ToInt32(Session["Company_code"]);
            if (roleid == 9)
            {
                pcode = 170;
            }
            else
            {
                pcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
            }
           
            gridview();
           
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        try
        {

                 
            pcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
         
            lblpcode.Text = ddl_Plantname.SelectedItem.Text;
            lblfrmdate.Text = dt1.ToString("dd/MM/yyyy");
            lbltodate.Text =  dt2.ToString("dd/MM/yyyy");
          
            gridview();
            AgentMilkStopedDate();

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
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
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();
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
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode.ToString(), "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    

    public void gridview()
    {

        try
        {
            
            dateformat();

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                if (pcode != 150)
                {
                    string sqlstr = "select   Agent_Id,Agent_Name,Route_id,Plant_code   from  Agent_Master where Plant_code='" + pcode + "'  and Agent_id   not in(select Agent_id from Procurement  where  plant_code='" + pcode + "' and  prdate between '" + d1 + "' and '" + d2 + "')  order by Agent_Id ";
                    SqlCommand COM = new SqlCommand(sqlstr, conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                    sqlDa.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                        lblfrmdate.Visible = true;
                        lblpcode.Visible = true;
                        lbltodate.Visible = true;
                     

                    }
                    else
                    {

                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        lblfrmdate.Visible = false;
                        lblpcode.Visible = false;
                        lbltodate.Visible = false;
                     
                    }
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblfrmdate.Visible = false;
                    lblpcode.Visible = false;
                    lbltodate.Visible = false;
                }



            }
        }
        catch
        {



        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
     
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        grid1();
        grid2();
           
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        gridview();
    }


    public void AgentMilkStopedDate()
    {
                dateformat();
                 DataTable dts = new DataTable();
                
                 String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Get_AgentMilkStopedDate]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@spccode", Convert.ToInt32(ccode));
                    sqlCmd.Parameters.AddWithValue("@sppcode", Convert.ToInt32(ddl_Plantname.SelectedItem.Value));
                    sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                    sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                    da.Fill(dts);
                    if (dts.Rows.Count > 0)
                    {

                        GridView2.DataSource = dts;
                        GridView2.DataBind();

                     

                    }
                    else
                    {

                        GridView2.DataSource = null;
                        GridView2.DataBind();


                    }
                    
                }

    }




    public void dateformat()
    {

        
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        d1 = dt1.ToString("MM/dd/yyyy");
       
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d2 = dt2.ToString("MM/dd/yyyy");

    }

    public void grid1()
    {
        HtmlForm form = new HtmlForm();

        Response.Clear();

        Response.Buffer = true;

        string filename = pcode + "-" + "Stopped Agent From:" + txt_FromDate.Text + " To: " + txt_ToDate.Text + ".xls";

        Response.AddHeader("content-disposition",

        "attachment;filename=" + filename);

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;

        GridView1.DataBind();

        form.Controls.Add(GridView1);

        this.Controls.Add(form);

        form.RenderControl(hw);

        //style to format numbers to string

        string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        Response.Write(style);

        Response.Output.Write(sw.ToString());

        //Response.Flush();

        //Response.End();

    }

public void grid2()
{

  HtmlForm form = new HtmlForm();

    Response.Clear();

    Response.Buffer = true;

   string filename1 = pcode + "-" + "Missing Agent From:" + txt_FromDate.Text + " To: " + txt_ToDate.Text + ".xls";

    Response.AddHeader("content-disposition",

    "attachment;filename=" + filename1);

    Response.Charset = "";

    Response.ContentType = "application/vnd.ms-excel";

    StringWriter sw = new StringWriter();

    HtmlTextWriter hw = new HtmlTextWriter(sw);
    GridView2.AllowPaging = false;

    GridView2.DataBind();

    form.Controls.Add(GridView2);

    this.Controls.Add(form);

    form.RenderControl(hw);

    //style to format numbers to string

    string style = @"<style> .textmode { mso-number-format:\@; } </style>";

    Response.Write(style);

    Response.Output.Write(sw.ToString());

    Response.Flush();

    Response.End();

}
    



}