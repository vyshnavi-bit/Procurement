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


public partial class TransportDistanceChange : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string managmobNo;
    DataRow dr;
    SqlDataReader dr1;
    BLLuser Bllusers = new BLLuser();
    Bllbillgenerate BLLBill = new Bllbillgenerate();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();

    BLLVehicleDetails vehicleBL = new BLLVehicleDetails();
    BOLvehicle vehicleBO = new BOLvehicle();
    DataTable dt = new DataTable();
    DbHelper dbaccess = new DbHelper();
    //Admin Check Flag
    public int Falg = 0;
    int Data;
    int status;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());
                btn_lock.Visible = false;
              //  managmobNo = Session["managmobNo"].ToString();
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                getadminapprovalstatus();
                if (roleid < 3)
                {
                    loadsingleplant();
                    pcode = ddl_Plantcode.SelectedItem.Value;
                    Session["Plant_Code1"] = pcode;
                    pname = ddl_Plantname.SelectedItem.Value;

                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                    pcode = ddl_Plantcode.SelectedItem.Value;
                    Session["Plant_Code1"] = pcode;
                    pname = ddl_Plantname.SelectedItem.Value;
                }

                if (roleid == 9)
                {

                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();
                }




                Bdate();
                loadgrid();
                Lbl_Errormsg.Visible = false;
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

        if (IsPostBack == true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();

                pcode = ddl_Plantcode.SelectedItem.Value;

                getadminapprovalstatus();
                Session["Plant_Code1"] = pcode;
                pname = ddl_Plantname.SelectedItem.Value;
                Lbl_Errormsg.Visible = false;
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
        pcode = ddl_Plantcode.SelectedItem.Value;
        Bdate();
        getadminapprovalstatus();
        loadgrid();
    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
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
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
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

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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
    private void Bdate()
    {
        try
        {
            dr1 = null;
            dr1 = BLLBill.Billdate(ccode, ddl_Plantcode.SelectedItem.Value);
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    txt_FromDate.Text = Convert.ToDateTime(dr1["Bill_frmdate"]).ToString("dd/MM/yyyy");
                    txt_ToDate.Text = Convert.ToDateTime(dr1["Bill_todate"]).ToString("dd/MM/yyyy");
                    Falg = Convert.ToInt32(dr1["ViewStatus"].ToString());
                   

                   btn_Ok.Visible = true;
                        
                    
                }
            }
            else
            {               
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
                Lbl_Errormsg.Text = "Please, Select Bill_Date...".ToString();
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
    public void loadgrid()
    {
        try
        {
            int dtcount;
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            DataTable dte = new DataTable();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            dt = vehicleBL.LoadGriddatavehicleDistance1(pcode, ccode, d1.ToString(), d1.ToString());
            dtcount = dt.Rows.Count;
            if (dtcount > 0)
            {
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
            else
            {
                gvProducts.Dispose();
                gvProducts.DataSource = dte;
                gvProducts.DataBind();
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
                Lbl_Errormsg.Text = "No Data...".ToString();
              
            }
        }
        catch (Exception ex)
        {

        }

    }


    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        pcode = ddl_Plantcode.SelectedItem.Value;
        loadgrid();
    }

    protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProducts.EditIndex = -1;
        loadgrid();
    }
    protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Lbl_Errormsg.Visible = true;
        Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
        Lbl_Errormsg.Text = "You have No Permission to Delete...".ToString();

    }
    protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvProducts.EditIndex = e.NewEditIndex;
        loadgrid();
    }
    protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
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
            vehicleBO.Vtsdistance = Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim());
            vehicleBO.Admindistance = Convert.ToInt32(((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim());
            vehicleBO.Pdate = Convert.ToDateTime(((TextBox)row.Cells[4].Controls[0]).Text.ToString().Trim());
            string mess = string.Empty;
            mess = vehicleBL.UpdateDistanceTruckPresent(vehicleBO);
            e.Cancel = true;
            gvProducts.EditIndex = -1;
            loadgrid();
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
            Lbl_Errormsg.Text = mess.Trim().ToString();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }
    protected void btn_Insert_Click(object sender, EventArgs e)
    {
        AutoInsert_Truckpresent();
    }
    private void AutoInsert_Truckpresent()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");

            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[AutoInsert_Truckresent]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Company_Code", ccode);
                sqlCmd.Parameters.AddWithValue("@Plant_Code", pcode);
                sqlCmd.Parameters.AddWithValue("@frdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@todate", d1.Trim());
                sqlCmd.ExecuteNonQuery();
                loadgrid();

            }
        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
            Lbl_Errormsg.Text = ex.ToString().Trim();
        }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            if (roleid >= 5)
            {
                dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                string d1 = dtm.ToString("MM/dd/yyyy");
                SqlConnection con = null;
                con = null;
                using (con = dbaccess.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE  FROM Truck_Present Where Plant_Code='" + pcode + "' AND Pdate='" + d1.ToString().Trim() + "' ", con);
                    cmd.ExecuteNonQuery();                    
                    con.Close();
                    loadgrid();
                }
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
                Lbl_Errormsg.Text = "You have No Permission to Delete...".ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_lock_Click(object sender, EventArgs e)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update AdminApproval set TransportStatus='1' where Plant_code='" + pcode + "'", conn);
            cmd.ExecuteNonQuery();
            getadminapprovalstatus();
            //string message;
            //message = "Transport  Approved SuccessFully";
            //string script = "window.onload = function(){ alert('";
            //script += message;
            //script += "')};";
            //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            //  WebMsgBox.Show("inserted Successfully");
           
        }
        catch (Exception ex)
        {
          
        }
    }


    public void getadminapprovalstatus()
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string stt = "Select TransportStatus,status    from  AdminApproval   where Plant_code='" + pcode + "'  ";
            SqlCommand cmd = new SqlCommand(stt, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                // btn_lock.Visible = false;

                while (dr.Read())
                {

                    Data = Convert.ToInt32(dr["TransportStatus"]);
                    status = Convert.ToInt32(dr["status"]);
                }
                //if (Data == 1 && status == 1)
                //{
                //    btn_lock.Visible = true;
                //}

                //if (Data == 0 && status == 1)
                //{
                //    btn_lock.Visible = true;
                //}
                //if (Data == 1 && status == 2)
                //{
                //    btn_lock.Visible = false;
                //}

                //if (Data == 0 && status == 2)
                //{
                //    btn_lock.Visible = false;
                //}

                if ((status == 1)  && (roleid > 1))
                {
                    btn_lock.Visible = true;

                    if ((Data == 1) && (status == 1))
                    {


                        //btn_lock.Enabled = false;
                        btn_lock.Enabled = false;

                    }
                }
                else
                {
                    btn_lock.Visible = false;

                }


            }
        }

        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }
}