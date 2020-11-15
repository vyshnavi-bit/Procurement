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
public partial class Dispatch : System.Web.UI.Page
{
    double snf, fat;   
    public string companycode;
    public string plantcode, plantname;
    double  fatval1, clrval1;
    public string frmdate = string.Empty;

    DataTable dt = new DataTable();
    DateTime dtt = new DateTime();
    DateTime dat = new DateTime();
    DbHelper DBclass = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    BOLDispatch DispatchBOL = new BOLDispatch();
    BLLDispatch DispatchBLL = new BLLDispatch();
    DALDispatch DispatchDAL = new DALDispatch();
    BLLPlantName BllPlant = new BLLPlantName();
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    string fdate,tdate;
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
                    plantcode = DDL_Plantfrom.SelectedItem.Value;  
                    DespatchEntryGriddata();
                    getadminapprovalstatus();
                }
                if ((roleid >= 3) && (roleid != 9))
                {

                    LoadPlantcode();
                    plantcode = DDL_Plantfrom.SelectedItem.Value;
                    DespatchEntryGriddata();
                    getadminapprovalstatus();
                }
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    plantcode = "170";
                    loadspecialsingleplant();
                    DespatchEntryGriddata();
                    getadminapprovalstatus();
                }
                dtt = System.DateTime.Now;
                txt_fromdate.Text = dtt.ToString("dd/MM/yyyy");
                lbl_msg.Visible = false;
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
                plantcode = DDL_Plantfrom.SelectedItem.Value;               
                lbl_msg.Visible = false;
                getadminapprovalstatus();
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
              ds = BllPlant.LoadPlantNameChkLst1(companycode.ToString());
              if (ds != null)
              {
                  DDL_Plantfrom.DataSource = ds;
                  DDL_Plantfrom.DataTextField = "Plant_Name";
                  DDL_Plantfrom.DataValueField = "plant_Code";//ROUTE_ID 
                  DDL_Plantfrom.DataBind();

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
            ds = BllPlant.LoadSinglePlantNameChkLst1(companycode.ToString(), plantcode);
            if (ds != null)
            {
                DDL_Plantfrom.DataSource = ds;
                DDL_Plantfrom.DataTextField = "Plant_Name";
                DDL_Plantfrom.DataValueField = "plant_Code";//ROUTE_ID 
                DDL_Plantfrom.DataBind();
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
              string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
              con = DBclass.GetConnection();
              SqlCommand cmd = new SqlCommand(STR, con);
              DataTable getdata = new DataTable();
              SqlDataAdapter dsii = new SqlDataAdapter(cmd);
              getdata.Rows.Clear();
              dsii.Fill(getdata);
              DDL_Plantfrom.DataSource = getdata;
              DDL_Plantfrom.DataTextField = "Plant_Name";
              DDL_Plantfrom.DataValueField = "plant_Code";//ROUTE_ID 
              DDL_Plantfrom.DataBind();
          }
          catch (Exception ex)
          {
              WebMsgBox.Show(ex.ToString());
          }
      }

      protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
             string duplicateFlag = string.Empty;
            //Check Entry Date Beween BillDate Status=0
             duplicateFlag = CheckDateBetweenBillDate();             
             if (duplicateFlag == "1")
             {
                 if (!(string.IsNullOrEmpty(txt_Milkkg.Text)))
                 {
                     InsertDespatchEntry();
                 }
                 else
                 {
                     lbl_msg.Visible = true;
                     lbl_msg.ForeColor = System.Drawing.Color.Green;
                     lbl_msg.Text = "Please, Check Your Data";
                 }
             }
             else
             {
                 lbl_msg.Visible = true;
                 lbl_msg.ForeColor = System.Drawing.Color.Green;
                 lbl_msg.Text = "Please, Check Your Date";
             }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("Please, Check Your Data");

            txt_Milkkg.Focus();
        }
    }

      private void clr()
    {
        try
        {
            txt_tcnum.Text = "";
            txt_Milkkg.Text = "";
            txt_fat.Text = "";
            txt_Clr.Text = "";
            txt_SNF.Text = "";
            txt_rate.Text = "";
            txt_Amount.Text = "";
            txt_tankar.Text = "";
        }
        catch (Exception ex)
        {
            catchmsg(ex.ToString());
        }
    }

      protected void txt_Milkkg_TextChanged(object sender, EventArgs e)
      {
          try
          {
              double avil = 0.0;
              double millk = 0.0;
              if (!(string.IsNullOrEmpty(txt_avail.Text)))
              {
                  avil = Convert.ToDouble(txt_avail.Text);
              }
              else
              {
                  txt_avail.Text = "0.0";
                  avil = Convert.ToDouble(txt_avail.Text);
              }
              if (!(string.IsNullOrEmpty(txt_avail.Text)))
              {
                  millk = Convert.ToDouble(txt_Milkkg.Text);
              }

              txt_fat.Focus();
          }
          catch (Exception ex)
          {
              catchmsg(ex.ToString());
          }
      }   
    
      protected void txt_Clr_TextChanged(object sender, EventArgs e)
      {
          try
          {
              double val = 0;
              clrval1 = 0;
              if (!(string.IsNullOrEmpty(txt_Clr.Text)))
                  clrval1 = Convert.ToDouble(txt_Clr.Text);
              if (!(string.IsNullOrEmpty(txt_fat.Text)))
                  fatval1 = Convert.ToDouble(txt_fat.Text);
              if ((clrval1 > 0) && (fatval1 > 0))
                  val = (fatval1 * 0.21) + ((clrval1 / 4) + 0.36);
              txt_SNF.Text = val.ToString("f1");

              txt_fat.Text = fatval1.ToString();
              txt_Clr.Text = clrval1.ToString();
             // txt_rate.Focus();
              if (txt_Milkkg.Text != string.Empty)
              {
                  txt_rate.Text = "20.00";
                  double mmkg = Convert.ToDouble(txt_Milkkg.Text);
                  double mmltr = mmkg / 1.03;
                  double mrate = Convert.ToDouble(txt_rate.Text);
                  double totcalamount = mmltr * mrate;
                  txt_Amount.Text = totcalamount.ToString("f2");
                  btn_Save1.Focus();
              }
              else
              {
                  WebMsgBox.Show("Please Enter Milk Kg...");
                  txt_Milkkg.Focus();
              }


          }
          catch (Exception ex)
          {
              catchmsg(ex.ToString());
          }
      }

      protected void txt_SNF_TextChanged(object sender, EventArgs e)
      {
          try
          {
              if (!(string.IsNullOrEmpty(txt_SNF.Text)))
                  snf = Convert.ToDouble(txt_SNF.Text);
              if (!(string.IsNullOrEmpty(txt_fat.Text)))
                  fat = Convert.ToDouble(txt_fat.Text);
              if ((snf > 0) && (fat > 0))

                  txt_Clr.Text = Convert.ToString(((snf - 0.36) - (fat * 0.21)) * 4);
              txt_Clr.Focus();
          }
          catch (Exception ex)
          {

              catchmsg(ex.ToString());
          }
      }

      protected void txt_rate_TextChanged(object sender, EventArgs e)
      {
          try
          {
              if (txt_Milkkg.Text != string.Empty)
              {
                  double mmkg = Convert.ToDouble(txt_Milkkg.Text);
                  double mmltr = mmkg / 1.03;
                  double mrate = Convert.ToDouble(txt_rate.Text);
                  double totcalamount = mmltr * mrate;
                  txt_Amount.Text = totcalamount.ToString("f2");
                  btn_Save1.Focus();
              }
              else
              {
                  WebMsgBox.Show("Please Enter Milk Kg...");
              }

          }
          catch (Exception ex)
          {
              catchmsg(ex.ToString());
               
          }
      }

      protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
      {
          try
          {
              if (e.Row.RowType == DataControlRowType.DataRow)
              {
                  LinkButton db = (LinkButton)e.Row.Cells[9].Controls[0];
                  db.OnClientClick = "return confirm('Do you want to delete this Record?');";
              }
          }
          catch (Exception ex)
          {
              catchmsg(ex.ToString());
          }

      }

      protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
      {
          try
          {
              SqlConnection conn = new SqlConnection(connStr);           

              int Tid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
              conn.Open();
              SqlCommand cmd = new SqlCommand("DELETE from DespatchEntry Where plant_code='" + plantcode + "' AND  Tid='" + Tid + "'", conn);
              cmd.ExecuteNonQuery();          
              
              lbl_msg.ForeColor = System.Drawing.Color.Red;
              lbl_msg.Text = "Record Deleted SuccessFully";
              lbl_msg.Visible = true;
              DespatchEntryGriddata();
          }
          catch(Exception ex)
          {
              catchmsg(ex.ToString());
          } 
      }

      private void catchmsg(string ex1)
      {
          try
          {
          lbl_msg.ForeColor = System.Drawing.Color.Red;
          lbl_msg.Text = ex1.ToString().Trim();
          lbl_msg.Visible = true;
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
              string str = "select   top 1    Bill_frmdate, Bill_todate from Bill_date   where plant_code='" + plantcode + "'  and status=0 order  by tid desc";
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

      public void DespatchEntryGriddata()
      {
          try
          {
              getDate();
              GridView1.HeaderStyle.BackColor = Color.Brown;
              GridView1.HeaderStyle.ForeColor = Color.White;
              plantcode = DDL_Plantfrom.SelectedItem.Value;
            
              using (SqlConnection conn =DBclass.GetConnection())
              {
                  string Sqlstr = "SELECT Tid AS Sno,Plant_To,CONVERT(Nvarchar(12),Date,103) AS Date,tcnumber,Tanker_No,MilkKg,Fat,Snf,Clr FROM [DespatchEntry] WHERE   [Plant_Code] ='" + plantcode + "' and date between '" + fdate + "' and '" + tdate + "' and type='Entry'  ORDER BY Date,sno ";              
                  SqlCommand COM = new SqlCommand(Sqlstr, conn);               
                  DataTable dt = new DataTable();
                  SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                  sqlDa.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      GridView1.DataSource = dt;
                      GridView1.DataBind();
                      GridView1.HeaderStyle.BackColor = Color.Brown;
                      GridView1.HeaderStyle.ForeColor = Color.White;
                  }
                  else
                  {
                      GridView1.DataSource = null;
                      GridView1.DataBind();
                  }
              }
          }
          catch (Exception ex)
          {
              catchmsg(ex.ToString());
          }          
      }

      public string CheckTcNoDuplicate()
      {
          try
          {            
              GridView1.HeaderStyle.BackColor = Color.Brown;
              GridView1.HeaderStyle.ForeColor = Color.White;
              dat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
              frmdate = dat.ToString("MM/dd/yyyy");
              using (SqlConnection conn = DBclass.GetConnection())
              {
                  string Sqlstr = "SELECT tcnumber FROM [DespatchEntry] WHERE   [Plant_Code] ='" + plantcode + "' and tcnumber='" + txt_tcnum.Text.Trim() + "' and date='" + frmdate + "'  ORDER BY tcnumber DESC";
                  SqlCommand COM = new SqlCommand(Sqlstr, conn);
                  DataTable dt = new DataTable();
                  SqlDataAdapter sqlDa = new SqlDataAdapter(COM); 
                  sqlDa.Fill(dt);
                  if (dt.Rows.Count > 0)
                  {
                      return "0";
                  }
                  else
                  {
                      return "0";
                  }
              }
          }
          catch (Exception ex)
          {
              return ex.ToString();
              
          }
      }

      //public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
      //{
      //    return dateToCheck >= startDate && dateToCheck < endDate;
      //}

    public string CheckDateBetweenBillDate()
    {
        try
        {     
            dat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
            frmdate = dat.ToString("MM/dd/yyyy");
            using (SqlConnection conn = DBclass.GetConnection())
            {
                string Sqlstr = "SELECT TOP 1 * FROM Bill_date WHERE  '" + frmdate + "' between bill_frmdate and Bill_todate AND status=0 AND Plant_Code='" + plantcode + "' ";
                SqlCommand COM = new SqlCommand(Sqlstr, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();

        }
    }
    
      public void InsertDespatchEntry()
      {
          try
          {
              string duplicateFlag = string.Empty;
            
              duplicateFlag = CheckTcNoDuplicate();
              if (Convert.ToInt16(duplicateFlag) == 0)
              {
                  dat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
                  frmdate = dat.ToString("MM/dd/yyyy");
                  SqlConnection CON = DBclass.GetConnection();
                  SqlCommand CMD = new SqlCommand("INSERT INTO DespatchEntry (Plant_From,Plant_To,Date,Session,MilkKg,Fat,Snf,Clr,Rate,Amount,Plant_code,Company_Code,Type,Fat_Kg,Snf_Kg,tcnumber,Tanker_No,Uid,Cdate) " + "VALUES (@Plant_From,@Plant_To,@Date,@Session,@MilkKg,@Fat,@Snf,@Clr,@Rate,@Amount,@plant_code,@Company_Code,@Type,@Fat_Kg,@Snf_Kg,@tcnumber,@Tanker_No,@Uid,@Cdate)", CON);

                  CMD.Parameters.AddWithValue("@Plant_From", DDL_Plantfrom.SelectedItem.Value);
                  CMD.Parameters.AddWithValue("@Plant_To", DDl_Plantto.SelectedItem.Text);
                  CMD.Parameters.AddWithValue("@Date", frmdate);
                  CMD.Parameters.AddWithValue("@Session", dl_Session.Text);
                  double Assignmilkkg = double.Parse(txt_Milkkg.Text);
                  CMD.Parameters.AddWithValue("@MilkKg", Assignmilkkg);
                  double varfat = double.Parse(txt_fat.Text);
                  CMD.Parameters.AddWithValue("@Fat", varfat);
                  double fatkg = (Assignmilkkg * varfat) / 100;
                  double varsnf = double.Parse(txt_SNF.Text);
                  CMD.Parameters.AddWithValue("@Snf", varsnf);
                  double snfkg = (Assignmilkkg * varsnf) / 100;
                  CMD.Parameters.AddWithValue("@Clr", txt_Clr.Text);
                  CMD.Parameters.AddWithValue("@Rate", txt_rate.Text);
                  CMD.Parameters.AddWithValue("@Amount", txt_Amount.Text);
                  CMD.Parameters.AddWithValue("@plant_code", plantcode);
                  CMD.Parameters.AddWithValue("@Company_Code", companycode);
                  CMD.Parameters.AddWithValue("@Type", "Entry");
                  CMD.Parameters.AddWithValue("@Fat_Kg", fatkg);
                  CMD.Parameters.AddWithValue("@Snf_Kg", snfkg);
                  CMD.Parameters.AddWithValue("@tcnumber", txt_tcnum.Text);
                  CMD.Parameters.AddWithValue("@Tanker_No", txt_tankar.Text);
                  CMD.Parameters.AddWithValue("@Uid", Session["Name"].ToString());
                  DateTime cdt = System.DateTime.Now;
                  CMD.Parameters.AddWithValue("@Cdate", cdt.ToString("MM/dd/yyyy hh:mm:ss"));

                  CMD.ExecuteNonQuery();

                  DespatchEntryGriddata();
                  clr();
                  txt_tankar.Focus();    

                  lbl_msg.Visible = true;
                  lbl_msg.ForeColor = System.Drawing.Color.Green;
                  lbl_msg.Text = "Record Inserted SuccessFully";  
              }
              else
              {
                  lbl_msg.Visible = true;
                  lbl_msg.ForeColor = System.Drawing.Color.Red;
                  lbl_msg.Text = "DC_NUMBER Already Avail";   
              }
          }
          catch (Exception EX)
          {
              lbl_msg.Visible = true;
              lbl_msg.ForeColor = System.Drawing.Color.Red;
              lbl_msg.Text = EX.ToString().Trim();             
          }
      }
      protected void DDL_Plantfrom_SelectedIndexChanged(object sender, EventArgs e)
      {

          DespatchEntryGriddata();
          getadminapprovalstatus();
      }
      protected void btn_lock_Click(object sender, EventArgs e)
      {

          try
          {
              string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
              SqlConnection conn = new SqlConnection(connStr);
              conn.Open();
              SqlCommand cmd = new SqlCommand("update AdminApproval set DespatchStatus='1' where Plant_code='" + plantcode + "'", conn);
              cmd.ExecuteNonQuery();
              getadminapprovalstatus();

              //string message;
              //message = "Despatch  Approved SuccessFully";
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
              string stt = "Select DespatchStatus,status    from  AdminApproval   where Plant_code='" + plantcode + "'  ";
              SqlCommand cmd = new SqlCommand(stt, conn);
              SqlDataReader dr = cmd.ExecuteReader();
              if (dr.HasRows)
              {

                  // btn_lock.Visible = false;

                  while (dr.Read())
                  {

                      Data = Convert.ToInt32(dr["DespatchStatus"]);
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
             
          }

      }
}