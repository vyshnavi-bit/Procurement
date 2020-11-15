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

public partial class ClosingStockNew : System.Web.UI.Page
{
    DataRow dr;
    SqlDataReader dar;
    int DeleteFlag;

    double snf, fat;
    double a, b;
    string uid, datee;
    string milkkgclose, getmilk;
    double milkclose;
    //string dd;
    //int cmpcode , pcode;
    public string companycode;
    public string plantcode, plantname;
    double fatval, snfval, fatval1, snfval1, clrval1;
    // static int savebtn = 0;
    DataTable dt = new DataTable();
    DateTime dtt = new DateTime();
    DateTime dat = new DateTime();
    DbHelper DBclass = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    BOLDispatch DispatchBOL = new BOLDispatch();
    BLLDispatch DispatchBLL = new BLLDispatch();
    DALDispatch DispatchDAL = new DALDispatch();
    string fdate, tdate;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    int Data;
    int status;

    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
         if (IsPostBack != true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                roleid = Convert.ToInt32(Session["Role"].ToString());
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_code"].ToString();
                btn_lock.Visible = false;

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

                    Session["Plant_Code"] = "170".ToString();
                    plantcode = "170";
                    loadspecialsingleplant();
                }


                Closinggridview();

                dtt = System.DateTime.Now;
                txt_fromdate.Text = dtt.ToString("dd/MM/yyyy");

                lbl_msg.Visible = false;
                getadminapprovalstatus();

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

                companycode = Session["Company_code"].ToString();

                roleid = Convert.ToInt32(Session["Role"].ToString());
            
                txt_PlantName.Text = DDL_Plantfrom.SelectedItem.Value;
                plantcode = ddl_Plantcode.SelectedItem.Value;
                Closinggridview();
              //  openingandprocurement();
                lbl_msg.Visible = false;
                getadminapprovalstatus();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }


        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            SETBO1();
            DispatchBLL.insertStockDetails(DispatchBOL);
         //   loadgriddata1();
           // openingandprocurement();
            //WebMsgBox.Show("Check the MILK KG");
            //GridView1.Visible = false;
            //GridView2.Visible = true;
            //uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);

            //   getopening();
            SETBO3();
            DispatchBLL.openingtStockDetails(DispatchBOL);
            Closinggridview();
            clr1();
            // clr();
            lbl_msg.Visible = true;
            lbl_msg.ForeColor = System.Drawing.Color.Green;
            lbl_msg.Text = "Record Inserted SuccessFully";
            txt_avail1.Focus();

        }
       catch (Exception ex)
         {

             catchmsg(ex.ToString());
             txt_avail1.Focus();

         }

          
        

    }



    private void clr1()
    {

       
        txt_avail1.Text = "";
        txt_fat1.Text = "";
        txt_SNF1.Text = "";
        txt_Clr1.Text = "";
     
    }


    public void SETBO1()
    {
        try
        {


            DispatchBOL.Companycode = int.Parse(companycode);
            DispatchBOL.Plantcode = plantcode;
            DispatchBOL.PlantName = txt_PlantName.Text;
            // txt_PlantName.Text = plantname;
            DispatchBOL.FromDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);

            DispatchBOL.Fat = double.Parse(txt_fat1.Text);
            DispatchBOL.Snf = double.Parse(txt_SNF1.Text);
            DispatchBOL.Clr = double.Parse(txt_Clr1.Text);

            double varfat = double.Parse(txt_fat1.Text);
            DispatchBOL.Fat = varfat;
            double varsnf = double.Parse(txt_SNF1.Text);
            DispatchBOL.Snf = varsnf;
            //    DispatchBOL.Clr = double.Parse(txt_Clr.Text);
            //    double mkg = double.Parse(txt_Milkkg.Text);
            double mkg = double.Parse(txt_avail1.Text);
            DispatchBOL.MilkKg = mkg;
            double fatkg = (mkg * varfat) / 100;
            txt_fat1.Text = txt_fat1.Text;
            double snfkg = (mkg * varsnf) / 100;
            txt_SNF1.Text = txt_SNF1.Text;
            //   txt_Snfkg1.Text = snfkg.ToString();

            DispatchBOL.FATKG = fatkg;
            DispatchBOL.SNFKG = snfkg;

            //DispatchBOL.FATKG = (mkg * varfat) / 100;
            //DispatchBOL.SNFKG = (mkg * varsnf) / 100;
            DispatchBOL.MilkKg = double.Parse(txt_avail1.Text);
            DispatchBOL.STORAGENAME = txt_stockdetails.Text;
            //DispatchBOL.PlantName=
            // DispatchBOL.TYPE = Ddl_Type.SelectedItem.Value;
            double RATE = double.Parse(txt_rate.Text);
            DispatchBOL.Rate = RATE;
            DispatchBOL.Amount = double.Parse(txt_Amount.Text);
            //double Assignmilkkg = double.Parse(txt_Milkkg.Text);

       //     txt_Milkkg.Focus();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }



    }



    public void Closinggridview()
    {
        try
        {
            getDate();
            GridView1.HeaderStyle.BackColor = Color.Brown;
            GridView1.HeaderStyle.ForeColor = Color.White;
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

             //   string Sqlstr = "SELECT [Tid],[Plant_To], CONVERT(VARCHAR(10),[Date],103) AS [Date],[MilkKg], [Fat], [Snf], [Rate], [Amount],CAST( [Clr] AS DECIMAL(18,2)) AS Clr FROM [Despatchnew] WHERE [Company_code] = '" + companycode + "' AND [Plant_Code] ='" + plantcode + "' and date between '" + fdate + "' and '" + tdate + "' ORDER BY Tid DESC";
                string Sqlstr = "SELECT [Tid],CONVERT(VARCHAR(10),[Date],103) AS [Date], [MilkKg], [Fat], [Snf], [Clr] FROM [Stock_Milk] WHERE [Company_code] = '" + companycode + "' AND [Plant_Code] ='" + plantcode + "' and Date between '" + fdate + "' and '" + tdate + "' ORDER BY Tid DESC";
                SqlCommand COM = new SqlCommand(Sqlstr, conn);

                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.HeaderStyle.BackColor = Color.Brown;
                    GridView1.HeaderStyle.ForeColor = Color.White;                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    lbl_msg.ForeColor = System.Drawing.Color.Red;
                    lbl_msg.Text = "NO DATA...";
                    lbl_msg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

            catchmsg(ex.ToString());

        }
    }

    public void getDate()
    {

        try
        {
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            string str = "select   top 1    Bill_frmdate, Bill_todate from Bill_date   where plant_code='" + plantcode + "' and status=0  order  by tid desc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                fdate = dr["Bill_frmdate"].ToString();
                tdate = dr["Bill_todate"].ToString();
            }
        }
        catch (Exception ex)
        {

            catchmsg(ex.ToString().Trim());

        }

       



    }



     private void catchmsg(string ex1)
      {

          lbl_msg.ForeColor = System.Drawing.Color.Red;
          lbl_msg.Text = ex1.ToString().Trim();
          lbl_msg.Visible = true;


      }

     //public void openingandprocurement()
     //{


     //    dat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
     //    txt_fromdate.Text = dat.ToString("MM/dd/yyyy");
     //    SqlDataReader dr = null;
     //    dr = DispatchBLL.getopeningstcok(companycode, plantcode, txt_fromdate.Text);

     //    double milkkg = 0.0, fat = 0.0, snf = 0.0, sumofmilk = 0.0, rate = 0.0, sumofrate = 0.0;

     //    if (dr.HasRows)
     //    {
     //        while (dr.Read())
     //        {

     //            milkkg = Convert.ToDouble(dr["milkkg"].ToString());
     //            //fat = Convert.ToDouble(dr["Fat"].ToString());
     //            //snf = Convert.ToDouble(dr["snf"].ToString());
     //            //rate = Convert.ToDouble(dr["rate"].ToString());


     //            //   fat = 0;
     //            //   snf = 0;
     //            rate = 0;


     //        }
     //    }
     //}



     public void SETBO3()
     {
         try
         {

             //string companycode = "1";
             //string plantcode = "111";
             // string plantname = "PONDY";
             DispatchBOL.Companycode = int.Parse(companycode);
             DispatchBOL.Plantcode = plantcode;
             DispatchBOL.PlantName = txt_PlantName.Text;
             //txt_PlantName.Text = plantname;
             dat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
             //DispatchBOL.FromDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());
             DispatchBOL.FromDate = dat.AddDays(1);
             //DispatchBOL.ToDate = DateTime.Parse(txt_todate.Text);
             //DispatchBOL.Session = dl_Session.SelectedItem.Value;
             //DispatchBOL.From_Plant = DDL_Plantfrom.SelectedItem.Value;
             //DispatchBOL.To_Plant = DDl_Plantto.SelectedItem.Value;
             double varfat = double.Parse(txt_fat1.Text);
             DispatchBOL.Fat = varfat;
             double varsnf = double.Parse(txt_SNF1.Text);
             DispatchBOL.Snf = varsnf;
             DispatchBOL.Clr = double.Parse(txt_Clr1.Text);
             double mkg = double.Parse(txt_avail1.Text);
             DispatchBOL.MilkKg = mkg;
             DispatchBOL.STORAGENAME = txt_stockdetails.Text;
             //DispatchBOL.PlantName=
             // DispatchBOL.TYPE = Ddl_Type.SelectedItem.Value;
             double RATE = double.Parse(txt_rate.Text);
             DispatchBOL.Rate = RATE;
         //    double Assignmilkkg = double.Parse(txt_Milkkg.Text);

             double fatkg = (mkg * varfat) / 100;

             txt_fat1.Text = fatkg.ToString();

             double snfkg = (mkg * varsnf) / 100;

             txt_SNF1.Text = snfkg.ToString();

             DispatchBOL.FATKG = double.Parse(txt_fat1.Text);
             DispatchBOL.SNFKG = double.Parse(txt_SNF1.Text);

             DispatchBOL.Amount = double.Parse(txt_Amount.Text);

     //        txt_Milkkg.Focus();
         }
         catch (Exception ex)
         {

             catchmsg(ex.ToString());

         }



     }



    


     private void LoadPlantcode()
     {
         try
         {
             SqlDataReader dr = null;
             DDL_Plantfrom.Items.Clear();
             ddl_Plantcode.Items.Clear();
             dr = Bllusers.LoadPlantcode(companycode);
             if (dr.HasRows)
             {
                 while (dr.Read())
                 {
                     ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                     DDL_Plantfrom.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                 }
             }
         }
         catch (Exception ex)
         {

             catchmsg(ex.ToString());

         }
     }
     private void loadsingleplant()
     {
         try
         {
             SqlDataReader dr = null;
             ddl_Plantcode.Items.Clear();
             DDL_Plantfrom.Items.Clear();
             dr = Bllusers.LoadSinglePlantcode(companycode, plantcode);
             if (dr.HasRows)
             {
                 while (dr.Read())
                 {
                     ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                     DDL_Plantfrom.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                 }
             }
         }
         catch (Exception ex)
         {

             catchmsg(ex.ToString());

         }
     }

     private void loadspecialsingleplant()//form load method
     {
         try
         {
             SqlDataReader dr = null;
             ddl_Plantcode.Items.Clear();
             DDL_Plantfrom.Items.Clear();
             dr = Bllusers.LoadSinglePlantcode(companycode, "170");
             if (dr.HasRows)
             {
                 while (dr.Read())
                 {
                     ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                     DDL_Plantfrom.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                 }
             }
         }
         catch (Exception ex)
         {
             WebMsgBox.Show(ex.ToString());
         }
     }



     protected void DDL_Plantfrom_SelectedIndexChanged(object sender, EventArgs e)
     {
         ddl_Plantcode.SelectedIndex = DDL_Plantfrom.SelectedIndex;
         plantcode = ddl_Plantcode.SelectedItem.Value;
         txt_PlantName.Text = DDL_Plantfrom.SelectedItem.Value;
         getadminapprovalstatus();
         //   dloadgriddata();
         getDate();
         Closinggridview();
     }
     protected void txt_avail1_TextChanged(object sender, EventArgs e)
     {
         txt_fat1.Focus();
     }
     protected void txt_Clr1_TextChanged(object sender, EventArgs e)
     {
         try
         {
             double val = 0;
             clrval1 = 0;
             if (!(string.IsNullOrEmpty(txt_Clr1.Text)))
                 clrval1 = Convert.ToDouble(txt_Clr1.Text);
             if (!(string.IsNullOrEmpty(txt_fat1.Text)))
                 fatval1 = Convert.ToDouble(txt_fat1.Text);
             if ((clrval1 > 0) && (fatval1 > 0))
                 val = (fatval1 * 0.21) + ((clrval1 / 4) + 0.36);
             txt_SNF1.Text = val.ToString("f2");

             //txt_Clr1.Focus();
             // txt_SNF1.Text = snfval1.ToString();
             txt_fat1.Text = fatval1.ToString();
             txt_Clr1.Text = clrval1.ToString();
             btn_Save1.Focus();
         }
         catch (Exception ex)
         {

             catchmsg(ex.ToString());

         }
     }
     protected void txt_fromdate_TextChanged(object sender, EventArgs e)
     {

     }
     protected void txt_rate_TextChanged(object sender, EventArgs e)
     {

     }
     protected void txt_SNF1_TextChanged(object sender, EventArgs e)
     {
         try
         {
             if (!(string.IsNullOrEmpty(txt_SNF1.Text)))
                 snfval1 = Convert.ToDouble(txt_SNF1.Text);
             if (!(string.IsNullOrEmpty(txt_fat1.Text)))
                 fatval1 = Convert.ToDouble(txt_fat1.Text);
             if ((snfval1 > 0) && (fatval1 > 0))
                 //  txt_Clr.Text = Convert.ToString(((snfval - 0.36) - (fatval * 0.2)) * 4);
                 txt_Clr1.Text = Convert.ToString(((snfval1 - 0.36) - (fatval1 * 0.21)) * 4);
             txt_Clr1.Focus();
             txt_SNF1.Text = snfval1.ToString();
             txt_fat1.Text = fatval1.ToString();
         }
         catch (Exception ex)
         {

             catchmsg(ex.ToString());

         }
     }
     protected void dl_Session_SelectedIndexChanged(object sender, EventArgs e)
     {

     }
     protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {

     }
     protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
     {

     }
     protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {

             LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

             db.OnClientClick = "return confirm('Are you certain you want to delete this questionnaire?');";
         }

     }
     protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
     {
         //try
         //{
         //    //SqlConnection conn = new SqlConnection(connStr);
         //    //int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
         //    //conn.Open();
         //    //SqlCommand cmd = new SqlCommand("delete from Stock_Milk where    plant_code='" + plantcode + "' and  Tid='" + userid + "'", conn);
         //    //cmd.ExecuteNonQuery();

         //    lbl_msg.ForeColor = System.Drawing.Color.Red;
         // //   lbl_msg.Text = "Record Deleted SuccessFully";
         //      lbl_msg.Text = "Record  Not Deleted... ";
         //    lbl_msg.Visible = true;
         //  //  getDate();
         //    Closinggridview();

             
         //}
         //catch (Exception ex)
         //{

         //    catchmsg(ex.ToString());

         //}

         try
         {
             SqlConnection conn = new SqlConnection(connStr);
             int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
             conn.Open();
             //   SqlCommand cmd = new SqlCommand("delete from Stock_Milk where    plant_code='" + plantcode + "' and  Tid='" + userid + "'", conn);
             SqlCommand cmd = new SqlCommand("Delete_Despatch", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@tid", userid);
             cmd.Parameters.AddWithValue("@Flag", 2);
             cmd.Parameters.AddWithValue("@companycode", companycode);
             cmd.Parameters.AddWithValue("@plantcode", plantcode);
             cmd.ExecuteNonQuery();

             lbl_msg.ForeColor = System.Drawing.Color.Red;
             lbl_msg.Text = "Record Deleted SuccessFully";
             lbl_msg.Visible = true;
             //  getDate();
             Closinggridview();
         }
         catch (Exception ex)
         {

             catchmsg(ex.ToString());

         }




     }
     protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
     {

     }
     protected void btn_lock_Click(object sender, EventArgs e)
     {
         try
         {
             string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
             SqlConnection conn = new SqlConnection(connStr);
             conn.Open();
             SqlCommand cmd = new SqlCommand("update AdminApproval set ClosingStatus='1' where Plant_code='" + plantcode + "'", conn);
             cmd.ExecuteNonQuery();
             getadminapprovalstatus();
             //string message;
             //message = "Closing Stock Approved SuccessFully";
             //string script = "window.onload = function(){ alert('";
             //script += message;
             //script += "')};";
             //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
         }
         catch (Exception ex)
         {

             catchmsg(ex.ToString());

         }
     }

     public void getadminapprovalstatus()
     {

         try
         {
             string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
             SqlConnection conn = new SqlConnection(connStr);
             conn.Open();
             string stt = "Select ClosingStatus,status    from  AdminApproval   where Plant_code='" + plantcode + "'  ";
             SqlCommand cmd = new SqlCommand(stt, conn);
             SqlDataReader dr = cmd.ExecuteReader();
             if (dr.HasRows)
             {

                 // btn_lock.Visible = false;

                 while (dr.Read())
                 {

                     Data = Convert.ToInt32(dr["ClosingStatus"]);
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


                         btn_lock.Enabled = false;
                       //  btn_lock.Visible = false;

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

             catchmsg(ex.ToString());

         }

     }
}