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
public partial class VehicleDetails : System.Web.UI.Page
{
    BLLAgentmaster AgentBL = new BLLAgentmaster();
    BOLvehicle vehicleBO = new BOLvehicle();
    BLLVehicleDetails vehicleBL = new BLLVehicleDetails();
    DataTable dt = new DataTable();
    DataRow dr;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    string bank;
    int iid;
    int newid;
    string bankid;
    int vehicleno;
    SqlConnection con = new SqlConnection();
    int id;
    string uid;
    string ifsccodefromgrid;
    public int companycode;
    public int plantcode;
    public int ccode;
    public int pcode;
    BLLuser Bllusers = new BLLuser();
    int value;
    public static int roleid;
    string name;
    string pass;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
             //name = Session["Name"].ToString();
             //pass = Session["pass"].ToString();
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                uid = Session["userid"].ToString();
                //if (Request.QueryString["id"] != null)
                roleid = Convert.ToInt32(Session["Role"].ToString());
                //    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                //else
                //    Response.Write("");
                //uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                lblmsg.Visible = false;
                if (roleid < 3)
                {
                    loadsingleplant();
                    gettrucktid();
                    GetBankinfo();
                    getgriddata();
                }
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                    gettrucktid();
                    GetBankinfo();
                    getgriddata();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    gettrucktid();
                    GetBankinfo();
                    getgriddata();

                }
               // txt_VehicleNo.Focus();
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
               // uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Convert.ToInt32(Session["Company_code"]);
                 ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
                 pcode =Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);
                 //plantcode = Convert.ToInt32(Session["Plant_Code"]);
                 if (roleid < 3)
                 {
                   //  loadsingleplant();                    
                   //  GetBankinfo();                   
                     ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
                     pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);
                     gettrucktid();
                    // GetBankinfo();
                     if (newid == 1)
                     {
                         ddl_BankId.SelectedIndex = ddl_BankName.SelectedIndex;
                         bankid = ddl_BankId.SelectedItem.Text;
                     }
                    
                     //getgriddata();
                 }
                 else
                 {
                     ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
                     pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);
                     gettrucktid();

                   //  GetBankinfo();

                     if (newid == 1)
                     {
                         ddl_BankId.SelectedIndex = ddl_BankName.SelectedIndex;
                         bankid = ddl_BankId.SelectedItem.Text;
                     }
                     
                    // LoadPlantcode();
                    
                    // GetBankinfo();
                   //  getgriddata();
                 }





               //  getgriddata();
                 lblmsg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    public void gettrucktid()
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                //SqlDataReader dr = null;
                //dr = Bllusers.REACHIMPORT(Convert.ToInt16(pcode),txt_FromDate.Text,txt_ToDate.Text);
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();

                string sqlstr = "select max(Truck_Id) as Truck_Id from Vehicle_Details where Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr, conn);

                adp.Fill(ds);
                int id = Convert.ToInt16(ds.Tables[0].Rows[0]["Truck_Id"]);
                txt_TruckId.Text = (id + 1).ToString();
            }
        }

        catch
        {
            txt_TruckId.Text = "1";

        }

    }

    //public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    //{
    //    if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
    //    {
    //        uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered as argument.", MessageBoxUsc_Message.enmMessageType.Info);
    //    }
    //    else
    //    {
    //        uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
    //    }
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {       
            SaveData();
    }
    //private bool validates()
    //{
    //    if (txt_VehicleNo.Text == "")
    //    {            
    //     //   uscMsgBox1.AddMessage("please, Enter the VehicleNo", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }
    //    if (txt_VehicleName.Text == "")
    //    {
    //        uscMsgBox1.AddMessage("please, Enter the VehicleName", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }
    //    if (txt_TruckId.Text == "")
    //    {
    //        uscMsgBox1.AddMessage("please, Enter the TruckId", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }
    //    if (txt_phoneNo.Text=="")
    //    {
    //        uscMsgBox1.AddMessage("please, Enter the phoneNo", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }
    //    if (ddl_BankId.Text == "--select Bank--")
    //    {
    //        uscMsgBox1.AddMessage("select BankId", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }
    //    if (txt_LitreCost.Text=="")
    //    {
    //        uscMsgBox1.AddMessage("please, Enter the LitreCost", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }
    //    if (ddl_ifsccode.Text == "")
    //    {
    //        uscMsgBox1.AddMessage("please, Enter the Ifsc Code", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }
    //    if (txt_accountnum.Text == "")
    //    {
    //        uscMsgBox1.AddMessage("please, Enter the AccountNo", MessageBoxUsc_Message.enmMessageType.Attention);
    //        return false;
    //    }

    //    return true;
    //}
    private void SETBO()
    {
        try
        {
            vehicleBO.Companycode = ccode;
            vehicleBO.Plantcode = pcode;
            vehicleBO.Vehicleno = txt_VehicleNo.Text;
            vehicleBO.Truckname = txt_VehicleName.Text;
            vehicleBO.Truckid = Convert.ToInt32(txt_TruckId.Text);
            vehicleBO.Phoneno = txt_phoneNo.Text;

            vehicleBO.Bankid = Convert.ToInt32(ddl_BankId.Text);            
           
            //if (ddl_ifsccode.Text == "")
            //{

            //    WebMsgBox.Show("Your Ifsc is Empty...");
            //    ddl_ifsccode.Focus();
            //}
            //else
            //{
                vehicleBO.IFSC = ddl_ifsccode.Text;
            //}


            //if (txt_accountnum.Text == "")
            //{

            //    WebMsgBox.Show("Your Ifsc is Empty...");
            //    txt_accountnum.Focus();
            //}
            //else
            //{
                vehicleBO.ACCOUNTNUM = txt_accountnum.Text;
            //}
            vehicleBO.Ltrcost = Convert.ToDouble(txt_LitreCost.Text);
            //vehicleBO.COSTTYPE =Convert.ToDouble(ddl_costtype.Text);
            //if ((ddl_costtype.SelectedItem.Value == "1") && (Convert.ToDouble(txt_LitreCost.Text) > 15.00))
            //{
            //    vehicleBO.COSTTYPE = Convert.ToDouble(ddl_costtype.Text);

            //}
            //else
            //{
            //    lblmsg.Text = "Fixed Value Amount check";
            //    lblmsg.Visible = true;
            //    lblmsg.ForeColor = System.Drawing.Color.Red;
            //}

            //if ((ddl_costtype.SelectedItem.Value == "0") && (ddl_costtype.SelectedItem.Value == "2") && (Convert.ToDouble(txt_LitreCost.Text) < 15.00))
            //{
            //    vehicleBO.COSTTYPE = Convert.ToDouble(ddl_costtype.Text);

            //}
            //else
            //{
            //    lblmsg.Text = "Ltr Or Kms Value check";
            //    lblmsg.Visible = true;
            //    lblmsg.ForeColor = System.Drawing.Color.Red;

            //}   

            if (Convert.ToDouble(txt_LitreCost.Text) > 15)
            {
                ddl_Vehiclepaymentmode.SelectedIndex = 0;
                vehicleBO.Paymentmode = Convert.ToInt32(ddl_Vehiclepaymentmode.SelectedItem.Value);
            }
            else
            {
                if (ddl_Vehiclepaymentmode.SelectedItem.Text == "Fixed")
                {
                    ddl_Vehiclepaymentmode.SelectedIndex = 1;
                }
                else
                {
                    vehicleBO.Paymentmode = Convert.ToInt32(ddl_Vehiclepaymentmode.SelectedItem.Value);
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    private void SaveData()
    {
         try
        {
        string mess = string.Empty;
        SETBO();
        validate();
        if (value == 1)
        {
            vehicleBL.insertvdetails(vehicleBO);
            clr();
            //if (mess == "Values is Inserted Successfully".Trim()) 
            //{
            lblmsg.Text = "Inserted SuccessFully";
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblmsg.Text = "Please Fill Missing Data ";
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        GetBankinfo();
        getifsccode();
        getgriddata();
        
        gettrucktid();
        txt_VehicleNo.Focus();
  
        }
         catch (Exception ex)
         {

         }
    }
    public void validate()
    {

        if ((txt_VehicleNo.Text != string.Empty) && (txt_VehicleName.Text != string.Empty) && (txt_phoneNo.Text != string.Empty) && (txt_LitreCost.Text != string.Empty) && (ddl_ifsccode.Text != string.Empty))
        {
            value = 1;
        }
    }
    public void getgriddata()
    {

        try
        {           

            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str;
            str = "select Vehicle_no,Truck_Name,Truck_Id,Phone_No,Bank_Id,Ifsc_code,AccountNo,cast(Ltr_Cost as decimal(10,2)) as Ltr_Cost,fixed,paymenttype,STATUS from Vehicle_Details WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' order by Truck_Id desc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GridView1.HeaderStyle.BackColor = System.Drawing.Color.DarkSlateBlue;
                GridView1.HeaderStyle.Font.Bold = true;
                //GridView1.FooterRow.Cells[2].Text = dt.Compute("Sum(MILKKG)", "").ToString();
                //GridView1.FooterRow.Cells[3].Text = dt.Compute("Sum(Milkltr)", "").ToString();
                //GridView1.FooterRow.Cells[7].Text = dt.Compute("Sum(Amount)", "").ToString();
                //GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //double fatkg1 = Convert.ToDouble(dt.Compute("Avg(fat)", "").ToString());
                //GridView1.FooterRow.Cells[3].Text = fatkg1.ToString("N2");
                //double snfkg2 = Convert.ToDouble(dt.Compute("Avg(snf)", "").ToString());
                //GridView1.FooterRow.Cells[4].Text = snfkg2.ToString("N2");
                //double rate1 = Convert.ToDouble(dt.Compute("Avg(rate)", "").ToString());
                //GridView1.FooterRow.Cells[5].Text = rate1.ToString("N2");
                //GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                //GridView1.FooterRow.Cells[0].Text = string.Empty;
                //GridView1.FooterRow.Cells[0].Text = string.Empty;
                //GridView1.FooterRow.Cells[1].Text = "Total";

                //GridView1.FooterRow.ForeColor = System.Drawing.Color.White;
                //GridView1.FooterRow.BackColor = System.Drawing.Color.DarkSlateBlue;
                //GridView1.FooterRow.Font.Bold = true;
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

    private void clr()
    {
        txt_VehicleNo.Text = "";
        txt_VehicleName.Text = "";
      //  txt_TruckId.Text = "";
        txt_phoneNo.Text = "";        
        txt_LitreCost.Text = "";
        txt_accountnum.Text = "";
      //  ddl_BankId.Items.Clear();
      //  ddl_ifsccode.Items.Clear();


    }
    private void GetBankinfo()
    {
        try
        {
            ddl_BankName.Items.Clear();
            ddl_BankId.Items.Clear();
            SqlDataReader dr = null;
            dr = AgentBL.GetBankID(ccode.ToString(),pcode.ToString());
         
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_BankId.Items.Add(dr["Bank_ID"].ToString().Trim());
                    ddl_BankName.Items.Add(dr["Bank_ID"].ToString().Trim() + "_" + dr["Bank_Name"].ToString().Trim());

                }
            }
            else
            {
                ddl_BankId.Items.Add("--select Bank--");
                ddl_BankName.Items.Add("--select Bank--");
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
            SqlDataReader dr = null;
            dr = Bllusers.LoadSinglePlantcode(ccode.ToString(),(pcode).ToString());
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

            dr = Bllusers.LoadPlantcode(ccode.ToString());
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
    protected void ddl_BankName_SelectedIndexChanged(object sender, EventArgs e)
    {
         
            ddl_BankId.SelectedIndex = ddl_BankName.SelectedIndex;
             bankid = ddl_BankId.SelectedItem.Text;
            newid = 1;
            getifsccode();
       
    }
    public void getifsccode()
    {
        ddl_ifsccode.Items.Clear();
        try
        {
            //   ddl_agentcode.Items.Clear();
             string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select * from BANK_MASTER   where plant_code='" + pcode + "' and Bank_id='" + bankid + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                // ddl_ccode.Items.Add(dr["company_code"].ToString());

                ddl_ifsccode.Items.Add(dr["ifsc_code"].ToString());
              //  txt_bank.Text = dr["Bank_name"].ToString();

            }


        }

        catch
        {

            WebMsgBox.Show("ERROR");

        }



    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        clr();
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        gettrucktid();
        GetBankinfo();

    }
    protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            int index = GridView1.EditIndex;
            GridViewRow row = GridView1.Rows[index];
            getgriddata();
            int count;
            count = dt.Rows.Count;
            dr = dt.Rows[index];

          //  vehicleBO.Companycode = companycode;
          //  vehicleBO.Plantcode = plantcode;
            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            //    vehicleBO.Truckid = Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim());
            //    vehicleBO.Vehicleno = ((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim();
            TextBox Vehicleno = (TextBox)row.Cells[1].Controls[0];
            //   vehicleBO.Truckname = ((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim();
            TextBox Truckname = (TextBox)row.Cells[2].Controls[0];
            //  vehicleBO.Phoneno = ((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim();
            TextBox Phoneno = (TextBox)row.Cells[3].Controls[0];
            // vehicleBO.Bankid = Convert.ToInt32(((TextBox)row.Cells[4].Controls[0]).Text.ToString().Trim());

            TextBox IFSC = (TextBox)row.Cells[5].Controls[0];
            //  vehicleBO.ACCOUNTNUM= ((TextBox)row.Cells[6].Controls[0]).Text.ToString().Trim();
            TextBox ACCOUNTNUM = (TextBox)row.Cells[6].Controls[0];
            //   vehicleBO.Ltrcost = Convert.ToDouble(((TextBox)row.Cells[7].Controls[0]).Text.ToString().Trim());
            TextBox Ltrcost = (TextBox)row.Cells[7].Controls[0];
            TextBox status = (TextBox)row.Cells[11].Controls[0];

            double LTRC = Convert.ToDouble(Ltrcost.Text);
            double conltr = Math.Round(LTRC, 2);
            ifsccodefromgrid = IFSC.Text;
            //   ddl_agentcode.Text = getrouteusingagent;
            //  ddl_agentcode.Text = getrouteusingagent;
            getbankcheck();


            string mess = string.Empty;

            if (id == 1)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Vehicle_Details  set status='" + status + "', Vehicle_No='" + Vehicleno.Text + "',Truck_Name='" + Truckname.Text + "',Phone_No='" + Phoneno.Text + "',Bank_Id='" + bank + "',Ifsc_code='" + IFSC.Text + "',AccountNo='" + ACCOUNTNUM.Text + "',Ltr_Cost='" + conltr + "'  where Truck_Id='" + userid + "' and plant_code='" + plantcode + "'", conn);
           //     cmd.ExecuteNonQuery();
                WebMsgBox.Show("Updated Successfully");
            }
            else
            {
                WebMsgBox.Show("Please Check Ifsc code");

            }
            // loadgrid();


            mess = vehicleBL.insertvdetails(vehicleBO);
            //   e.Cancel = true;
            //  gvProducts.EditIndex = -1;
            getgriddata();
        }
        catch
        {

            WebMsgBox.Show("Please Check Your Data");

        }
       
    }
    protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gettrucktid();
        GetBankinfo();
    }
    public void getbankcheck()
    {

        try
        {
            //  ddl_agentcode.Items.Clear();

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select DISTINCT(Bank_Id) from bank_master where Plant_code='" + pcode + "'  and ifsc_code='" + ifsccodefromgrid + "'   ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                bank = dr["Bank_Id"].ToString();
                id = 1;
            }
            else
            {

                //  WebMsgBox.Show("ERROR");
                id = 0;

            }
        }

        catch
        {

            WebMsgBox.Show("Please Check");

        }



    }



    public void vehiclecheck()
    {

        try
        {
            //  ddl_agentcode.Items.Clear();

            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select  * from Vehicle_Details where Plant_code='" + pcode + "'  and Vehicle_no='" + txt_VehicleNo.Text + "'   ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                vehicleno = 1;
            }
            else
            {

                //  WebMsgBox.Show("ERROR");
                vehicleno = 0;

            }
        }
        catch
        {

            WebMsgBox.Show("Please Check");

        }
    }
    protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getgriddata();
    }
    protected void ddl_BankId_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
        pcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);
        GetBankinfo();
        getgriddata();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getgriddata();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string i = (e.Row.Cells[10].Text);
        //    if (i == "True")
        //    {
        //        e.Row.Cells[10].Text = "1".ToString();
        //    }
        //    else
        //    {
        //        e.Row.Cells[10].Text = "0".ToString();
        //    }
        //}
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        getgriddata();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        getgriddata();
    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_PlantName.Items.Clear();
            string cccode = ccode.ToString();
            dr = Bllusers.LoadSinglePlantcode(cccode, "170");
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
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

         

            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
                     
            TextBox Truckname = (TextBox)row.Cells[2].Controls[0];
            //  vehicleBO.Phoneno = ((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim();
            TextBox Phoneno = (TextBox)row.Cells[4].Controls[0];
            TextBox IFSC = (TextBox)row.Cells[6].Controls[0];
            //  vehicleBO.ACCOUNTNUM= ((TextBox)row.Cells[6].Controls[0]).Text.ToString().Trim();
            TextBox ACCOUNTNUM = (TextBox)row.Cells[7].Controls[0];
            //   vehicleBO.Ltrcost = Convert.ToDouble(((TextBox)row.Cells[7].Controls[0]).Text.ToString().Trim());
            TextBox Ltrcost = (TextBox)row.Cells[8].Controls[0];
            TextBox Fixed = (TextBox)row.Cells[9].Controls[0];
            TextBox paymode = ((TextBox)row.Cells[10].Controls[0]);
          //  int i = Convert.ToInt16(paymode);
            TextBox status = (TextBox)row.Cells[11].Controls[0];
            double Cost = Convert.ToDouble(Ltrcost.Text);
            if (Cost > 15.00)
            {
                Fixed.Text = "1";
                if ((paymode.Text == "1") || (paymode.Text == "0"))
                {
                    iid = 1;
                }
            }
            else
            {
                Fixed.Text = "0";
                if ((paymode.Text == "1") || (paymode.Text == "0"))
                {
                    iid = 1;
                }
                else
                {
                    lblmsg.Text = "Please Check your Data";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Visible = true;
                }
            }
            double LTRC = Convert.ToDouble(Ltrcost.Text);
            double conltr = Math.Round(LTRC, 2);
            ifsccodefromgrid = IFSC.Text;

            GridView1.EditIndex = -1;
            conn.Open();
            //   ddl_agentcode.Text = getrouteusingagent;
            //  ddl_agentcode.Text = getrouteusingagent;
            getbankcheck();

          
            string mess = string.Empty;

            if (roleid > 6)
            {
                conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Vehicle_Details  set  status='"+status.Text+"', Truck_Name='" + Truckname.Text + "',Phone_No='" + Phoneno.Text + "',Bank_Id='" + bank + "',Ifsc_code='" + IFSC.Text + "',AccountNo='" + ACCOUNTNUM.Text + "',Ltr_Cost='" + conltr + "',Fixed='" + Fixed.Text + "',paymenttype='" + paymode.Text + "'  where Truck_Id='" + userid + "' and plant_code='" + pcode + "'", conn);
                cmd.ExecuteNonQuery();
              //  WebMsgBox.Show("Updated Successfully");
                lblmsg.Text = "Updated Successfully";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Visible = true;
            }
            else
            {
                lblmsg.Text = "Please Check Ifsc code or Payment Data";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Visible = true;
              

            }
            // loadgrid();


            mess = vehicleBL.insertvdetails(vehicleBO);
            //   e.Cancel = true;
            //  gvProducts.EditIndex = -1;
            getgriddata();
        }
        catch (Exception Ex)
        {
            lblmsg.Text = Ex.Message;
            lblmsg.Visible = true;
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            con.Close();
        }
    }
    protected void txt_VehicleNo_TextChanged(object sender, EventArgs e)
    {
        vehiclecheck();
        if (vehicleno == 0)
        {
            txt_VehicleName.Focus();
        }
        else
        {
            lblmsg.Text = "Same Vehicle Already Avail";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;
            txt_VehicleNo.Text = "";
            txt_VehicleNo.Focus();

        }
    }
    protected void txt_VehicleNo_TextChanged1(object sender, EventArgs e)
    {

    }
}
