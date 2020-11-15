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
using System.Data.SqlClient;

public partial class TestGrid : System.Web.UI.Page
{
   
    string uid;

    public int ccode;
    public int pcode;
    public string pname;
    public string cname;
    public string managmobNo;
    DataRow dr;
    BLLuser Bllusers = new BLLuser();
    DateTime dtm = new DateTime();
    BLLVehicleDetails vehicleBL = new BLLVehicleDetails();
    BOLvehicle vehicleBO = new BOLvehicle();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                uid = Session["User_ID"].ToString();
                if (Request.QueryString["id"] != null)

                    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                else
                    Response.Write("");
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_ToDate.Text = dtm.ToShortDateString();
                //txt_FromDate.Text = "08/01/2014";
                //txt_ToDate.Text = "08/10/2014";
                LoadPlantcode();
                loadgrid();
               
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                //`loadgrid();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }


    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
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

    public void loadgrid()
    {
        try
        {
            int dtcount;
            //DateTime dt1 = new DateTime();
            //DateTime dt2 = new DateTime();

            //dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            dt = vehicleBL.LoadGriddatavehicleDistance(pcode.ToString(), ccode.ToString());
            dtcount = dt.Rows.Count;
            if (dtcount > 0)
            {
                gvProducts.DataBind();
            }
            else
            {

                gvProducts.DataBind();

                WebMsgBox.Show("This Rate chat have Emty Rows Only...");
            }
        }
        catch (Exception ex)
        {

        }

    }

    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
        
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    //protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvProducts.EditIndex = e.NewEditIndex;
    //    loadgrid();
    //}
    //protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    int index = gvProducts.EditIndex;
    //    GridViewRow row = gvProducts.Rows[index];
    //    loadgrid();
    //    int count;
    //    count = dt.Rows.Count;
    //    dr = dt.Rows[index];
    //    vehicleBO.Companycode = Convert.ToInt32(ccode);
    //    vehicleBO.Plantcode = Convert.ToInt32(pcode);
    //    vehicleBO.Truckid = Convert.ToInt32(((TextBox)row.Cells[0].Controls[0]).Text.ToString().Trim());
    //    vehicleBO.Distance = Convert.ToInt32(((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim());
    //    vehicleBO.Pdate = Convert.ToDateTime(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim());
        
    //    string mess = string.Empty;
    //    mess = vehicleBL.UpdateDistanceTruckPresent(vehicleBO);
    //    e.Cancel = true;
    //    gvProducts.EditIndex = -1;
    //    loadgrid();
    //    uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Success);
    //}
    //protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gvProducts.EditIndex = -1;
    //    loadgrid();
        
    //}
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        loadgrid();
    }
    protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = gvProducts.EditIndex;
        GridViewRow row = gvProducts.Rows[index];
        loadgrid();
        int count;
        count = dt.Rows.Count;
        dr = dt.Rows[index];
        vehicleBO.Companycode = Convert.ToInt32(ccode);
        vehicleBO.Plantcode = Convert.ToInt32(pcode);
        vehicleBO.Truckid = Convert.ToInt32(((TextBox)row.Cells[0].Controls[0]).Text.ToString().Trim());
        vehicleBO.Distance = Convert.ToInt32(((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim());
        vehicleBO.Pdate = Convert.ToDateTime(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim());

        string mess = string.Empty;
        mess = vehicleBL.UpdateDistanceTruckPresent(vehicleBO);
        e.Cancel = true;
        gvProducts.EditIndex = -1;
        loadgrid();
        uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Success);
    }
    protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProducts.EditIndex = -1;
        loadgrid();
    }
    protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvProducts.EditIndex = e.NewEditIndex;
        loadgrid();
    }
    protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProducts.EditIndex = -1;
        loadgrid();
    }
}