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
public partial class DispatchnewACK : System.Web.UI.Page
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
    string fdate,tdate;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
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


                if (roleid < 3)
                {
                    loadsingleplant();
                    DespatchGriddata();
                }
                else
                {
                    LoadPlantcode();
                    DespatchGriddata();
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


                ddl_Plantcode.SelectedIndex = DDL_Plantfrom.SelectedIndex;
                plantcode = ddl_Plantcode.SelectedItem.Value;
                //     plantcode = ddl_Plantcode.SelectedItem.Value;
                DespatchGriddata();
         
           //     openingandprocurement();
             //   DespatchGriddata();
                lbl_msg.Visible = false;
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }


        }
    }
    //private void dloadgriddata()
    //{


       

    //    int dtcount;
    //    dt = null;
    //    dt = DispatchBLL.DespatchGriddata(plantcode, companycode,fdate,tdate);
    //    dtcount = dt.Rows.Count;
    //    if (dtcount > 0)
    //    {
    //        GridView1.DataSource = dt;
    //        GridView1.DataBind();
    //    }
    //    else
    //    {
    //        GridView1.DataSource = dt;
    //        GridView1.DataBind();

          
    //    }
    //    GridView1.HeaderStyle.BackColor = Color.Brown;
    //    GridView1.HeaderStyle.ForeColor = Color.White;
    //}



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
       // dloadgriddata();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
      //  dloadgriddata();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

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
            WebMsgBox.Show(ex.ToString());
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
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void DDL_Plantfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        ddl_Plantcode.SelectedIndex = DDL_Plantfrom.SelectedIndex;
        plantcode = ddl_Plantcode.SelectedItem.Value;
        txt_PlantName.Text = DDL_Plantfrom.SelectedItem.Value;
     //   dloadgriddata();
      //  getDate();
     //   DespatchGriddata();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {


            //SETBO();
            //DispatchBLL.insertDispatch(DispatchBOL);







            //clr();

            //GridView1.Visible = true;

            //openingandprocurement();
            INSERTCOMMAND();
            clr();
            txt_Milkkg.Focus();
            //getDate();
            //DespatchGriddata();
            lbl_msg.Visible = true;
            lbl_msg.ForeColor = System.Drawing.Color.Green;
            lbl_msg.Text = "Record Inserted SuccessFully";

            txt_Milkkg.Focus();
            DespatchGriddata();

        }
        catch (Exception ex)
        {
            WebMsgBox.Show("PLease  Check Your Data ");

            txt_Milkkg.Focus();
        }
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
               
    //            rate = 0;


    //        }
    //    }

        
    //    SqlDataReader dr1 = null;
    //    dr1 = DispatchBLL.getprocumentstcok(companycode, plantcode, txt_fromdate.Text);
    //    double procurementmilk = 0.0, fat1 = 0.0, snf1 = 0.0, sumoffat = 0.0, sumsnf = 0.0, rate1 = 0.0;

    //    if (dr1.HasRows)
    //    {
    //        while (dr1.Read())
    //        {
    //            try
    //            {
    //                procurementmilk = Convert.ToDouble(dr1["milk"].ToString());
                   

    //                fat1 = 0;
    //                snf1 = 0;
    //                rate1 = 0;
    //            }

    //            catch
    //            {
    //                procurementmilk = 0;
    //                fat1 = 0;
    //                snf1 = 0;
    //                rate1 = 0;
    //            }
            


    //        }
    //    }
    //    SqlDataReader dr2 = null;
    //    dr2 = DispatchBLL.getdispstcok(companycode, plantcode, txt_fromdate.Text);
    //    double dismilk = 0.0;

    //    if (dr2.HasRows)
    //    {
    //        while (dr2.Read())
    //        {

    //            dismilk = Convert.ToDouble(dr2["MK"].ToString());


    //        }
    //    }
    //    double[] array1 = { fat, fat1 };
    //    double[] array2 = { snf, snf1 };
    //    double[] array3 = { rate, rate1 };
    //    sumofmilk = procurementmilk + milkkg - dismilk;
    //    sumofrate = array3.Average();
    //    sumoffat = array1.Average();
    //    sumsnf = array2.Average();
    //    txt_avail.Text = sumofmilk.ToString();
    //    txt_fromdate.Text = dat.ToString("dd/MM/yyyy");


    //}


    public void SETBO()
    {

       

        DispatchBOL.Companycode = int.Parse(companycode);
        DispatchBOL.Plantcode = plantcode;
        DispatchBOL.FromDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
        //DispatchBOL.Session = dl_Session.SelectedItem.Value;
        //if (ddl_Plantcode.Text != string.Empty)
        //{
        //    DispatchBOL.From_Plant = DDL_Plantfrom.SelectedItem.Value;
        //}
        //else
        //{
        //    WebMsgBox.Show("Please Select PlantName...");


        //}
        DispatchBOL.Session = dl_Session.Text;
        if (ddl_Plantcode.Text != string.Empty)
        {
            // DispatchBOL.From_Plant = DDL_Plantfrom.SelectedItem.Value;txt_PlantName.Text dl_Session
            DispatchBOL.From_Plant = txt_PlantName.Text;
        }
        else
        {
            WebMsgBox.Show("Please Select PlantName...");


        }
        DispatchBOL.To_Plant = DDl_Plantto.SelectedItem.Text;

        double varfat = double.Parse(txt_fat.Text);
        DispatchBOL.Fat = varfat;
        double varsnf = double.Parse(txt_SNF.Text);
        DispatchBOL.Snf = varsnf;
        DispatchBOL.Clr = double.Parse(txt_Clr.Text);
        DispatchBOL.TCNUMBER = txt_tcnum.Text;
        double mkg = double.Parse(txt_Milkkg.Text);
        DispatchBOL.MilkKg = mkg;
        DispatchBOL.TYPE = "despatch";
        double RATE = double.Parse(txt_rate.Text);
        DispatchBOL.Rate = RATE;
        double Assignmilkkg = double.Parse(txt_Milkkg.Text);

        double fatkg = (mkg * varfat) / 100;

      //  txt_fat.Text = fatkg.ToString();
        txt_fat.Text = "0";

        double snfkg = (mkg * varsnf) / 100;
      //  txt_SNF.Text = snfkg.ToString();
        txt_SNF.Text = "0";

        DispatchBOL.FATKG = double.Parse(txt_fat.Text);
        DispatchBOL.SNFKG = double.Parse(txt_SNF.Text);
        DispatchBOL.Amount = double.Parse(txt_Amount.Text);
       

        if (txt_tankar.Text != string.Empty)
        {


            DispatchBOL.TANKARNO = txt_tankar.Text;
        }
        else
        {
            WebMsgBox.Show("Please Enter Tanker No...");

        }

        txt_Milkkg.Focus();

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
              txt_SNF.Text = val.ToString("f2");

            
              txt_fat.Text = fatval1.ToString();
              txt_Clr.Text = clrval1.ToString();
              txt_rate.Focus();
           
          }
          catch (Exception ex)
          {

              catchmsg(ex.ToString());

          }
      }
      protected void txt_Milkkg_TextChanged(object sender, EventArgs e)
      {
          double avil = Convert.ToDouble(txt_avail.Text);
          double millk = Convert.ToDouble(txt_Milkkg.Text);
        

          txt_fat.Focus();
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
      private void clr()
      {

          txt_Milkkg.Text = "";
          txt_fat.Text = "";
          txt_SNF.Text = "";
          txt_Clr.Text = "";

          txt_rate.Text = "";
          txt_Amount.Text = "";

          DDL_Plantfrom.SelectedItem.Value = "";
          DDl_Plantto.SelectedItem.Value = "";
          dl_Session.SelectedItem.Value = "";
          txt_PlantName.Text = "";
          txt_tankar.Text = "";
          txt_tcnum.Text = "";
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
                  txt_tankar.Focus();
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


     
      protected void dl_Session_SelectedIndexChanged(object sender, EventArgs e)
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
          try
          {
              SqlConnection conn = new SqlConnection(connStr);

           

              int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
              conn.Open();
              SqlCommand cmd = new SqlCommand("delete from Despatchnew where    plant_code='" + plantcode + "' and  Tid='" + userid + "'", conn);
              cmd.ExecuteNonQuery();
            


              lbl_msg.ForeColor = System.Drawing.Color.Red;
              lbl_msg.Text = "Record Deleted SuccessFully";
              lbl_msg.Visible = true;
             // getDate();
              DespatchGriddata();
           
          }
          catch(Exception ex)
          {

              catchmsg(ex.ToString());

          } 

      }

      private void catchmsg(string ex1)
      {

          lbl_msg.ForeColor = System.Drawing.Color.Red;
          lbl_msg.Text = ex1.ToString().Trim();
          lbl_msg.Visible = true;


      }
      protected void txt_fromdate_TextChanged(object sender, EventArgs e)
      {

      }
      protected void txt_avail_TextChanged(object sender, EventArgs e)
      {

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
              tdate  = dr["Bill_todate"].ToString();

              }


          }

          catch(Exception ex)
          {

              catchmsg(ex.ToString().Trim());

          }



      }

    

      public void DespatchGriddata()
      {

          getDate();

          try
          {

              GridView1.HeaderStyle.BackColor = Color.Brown;
              GridView1.HeaderStyle.ForeColor = Color.White;

              DateTime dt1 = new DateTime();
              DateTime dt2 = new DateTime();
             
              string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
              using (SqlConnection conn = new SqlConnection(connStr))
              {
                  conn.Open();

                  string Sqlstr = "SELECT [Tid],[Plant_To], CONVERT(VARCHAR(10),[Date],103) AS [Date],tcnumber as DcNumber,[Ack_milkkg], [Ack_fat], [Ack_snf], [Ack_rate], [Ack_amount],CAST( [Ack_clr] AS DECIMAL(18,2)) AS Clr FROM [Despatchnew] WHERE   [Plant_Code] ='" + plantcode + "' and date between '" + fdate + "' and '" + tdate + "' and type='Ack'  ORDER BY DcNumber,Tid DESC";
              
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
          catch
          {



          }

          
      }


      public void INSERTCOMMAND()
      {


          try
          {



            

              dat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
              txt_fromdate.Text = dat.ToString("MM/dd/yyyy");


              //  txt_fat.Text = fatkg.ToString();
          //    txt_fat.Text = "0";

             
              SqlConnection CON = new SqlConnection(connStr);
            //  CON.Open();
              //   SqlCommand CMD = new SqlCommand("INSERT INTO vehicles (make, model_name, model_year, engine_type, transmission_type) " + "VALUES (@make, @model_name, @model_year, @engine_type, @transmission_type)", CON);
              SqlCommand CMD = new SqlCommand("INSERT INTO Despatchnew (Date, Plant_From,Plant_To, Session, tcnumber,Ack_milkkg,Ack_fat,Ack_snf,Ack_clr,Ack_rate,Ack_amount,Tanker_No,Fat_Kg,Snf_Kg,Type,plant_code,Company_code) " + "VALUES (@Date,@Plant_From,@Plant_To,@Session,@tcnumber,@Ack_milkkg,@Ack_fat,@Ack_snf,@Ack_clr,@Ack_rate,@Ack_amount,@Tanker_No,@Fat_Kg,@Snf_Kg,@Type,@plant_code,@Company_code)", CON);
              CMD.Parameters.AddWithValue("@Date", txt_fromdate.Text);
              CMD.Parameters.AddWithValue("@Plant_From", DDL_Plantfrom.Text);
              CMD.Parameters.AddWithValue("@Plant_To", DDl_Plantto.SelectedItem.Text);
              CMD.Parameters.AddWithValue("@Session", dl_Session.Text);
              CMD.Parameters.AddWithValue("@tcnumber", txt_tcnum.Text);

              double Assignmilkkg = double.Parse(txt_Milkkg.Text);
              CMD.Parameters.AddWithValue("@Ack_milkkg", Assignmilkkg);
              double varfat = double.Parse(txt_fat.Text);
              CMD.Parameters.AddWithValue("@Ack_fat", varfat);
              double fatkg = (Assignmilkkg * varfat) / 100;
              double varsnf = double.Parse(txt_SNF.Text);
              CMD.Parameters.AddWithValue("@Ack_snf", varsnf);
              double snfkg = (Assignmilkkg * varsnf) / 100;
              CMD.Parameters.AddWithValue("@Ack_clr", txt_Clr.Text);

              CMD.Parameters.AddWithValue("@Ack_rate", txt_rate.Text);
              CMD.Parameters.AddWithValue("@Ack_amount", txt_Amount.Text);
              CMD.Parameters.AddWithValue("@Tanker_No", txt_tankar.Text);
              CMD.Parameters.AddWithValue("@Fat_Kg", fatkg);
              CMD.Parameters.AddWithValue("@Snf_Kg", snfkg);
              CMD.Parameters.AddWithValue("@Type", "Ack");
              CMD.Parameters.AddWithValue("@plant_code",plantcode);
              CMD.Parameters.AddWithValue("@plant_code", companycode);
              CON.Open();
              CMD.ExecuteNonQuery();

          }



          catch(Exception EX)
          {


              EX.Message.ToString();
          }
}

      
}