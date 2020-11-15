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
public partial class DispatchAcknowledgement : System.Web.UI.Page
{

    DataRow dr;
    SqlDataReader dar;
    int DeleteFlag;

    double snf, fat;
    double a, b;
    string uid, datee;
    string milkkgclose, getmilk;
    double milkclose; 
    public string companycode;
    public string plantcode, plantname;
    double fatval, snfval, fatval1, snfval1, clrval1;
   
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
                if ((roleid >= 3) && (roleid != 9))
                {
                
                    LoadPlantcode();
                    DespatchGriddata();
                }
                if (roleid == 9)
                {

                    Session["Plant_Code"] = "170".ToString();
                    plantcode = "170";
                    loadspecialsingleplant();
                }
                LoadDCnumber();
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
               // DespatchGriddata();
                openingandprocurement();
                lbl_msg.Visible = false;
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

    private void catchmsg(string ex1)
    {
        try
        {
            //lbl_msg.ForeColor = System.Drawing.Color.Red;
            //lbl_msg.Text = ex1.ToString().Trim();
            //lbl_msg.Visible = true;
        }
        catch (Exception ex)
        {
            //catchmsg(ex.ToString());
        }

    }          
    
    protected void DDL_Plantfrom_SelectedIndexChanged(object sender, EventArgs e)

    {        
        //ddl_Plantcode.SelectedIndex = DDL_Plantfrom.SelectedIndex;
        //plantcode = ddl_Plantcode.SelectedItem.Value;

        ddl_Plantcode.SelectedIndex = DDL_Plantfrom.SelectedIndex;
        plantcode = ddl_Plantcode.SelectedItem.Value;
        txt_tankar.Focus();
     //   txt_PlantName.Text = DDL_Plantfrom.SelectedItem.Value;
     //   dloadgriddata();
        clr();
        LoadDCnumber();
        LoadDCnumberDetails();  
        getDate();
       DespatchGriddata();
    }

    public void getDate()
    {
        try
        {
            SqlConnection con = DBclass.GetConnection();
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

    public void DespatchGriddata()
    {

        try
        {
            getDate();
            GridView1.HeaderStyle.BackColor = Color.Brown;
            GridView1.HeaderStyle.ForeColor = Color.White;

            using (SqlConnection conn = DBclass.GetConnection())
            {
              //  conn.Open();

                string Sqlstr = "SELECT [Tid] AS [Sno],[Plant_To], CONVERT(VARCHAR(10),[Date],103) AS [Date],[tcnumber],[Tanker_No],[MilkKg], [Fat], [Snf], [Rate], [Amount],CAST( [Clr] AS DECIMAL(18,2)) AS Clr FROM [Despatchnew] WHERE [Company_code] = '" + companycode + "' AND [Plant_Code] ='" + plantcode + "' and date between '" + fdate + "' and '" + tdate + "'  ORDER BY  Date,sno";

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

    public void SETBO()
    {
        try
        {

            DispatchBOL.Companycode = int.Parse(companycode);
            DispatchBOL.Plantcode = ddl_Plantcode.SelectedItem.Value;
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
                DispatchBOL.From_Plant = DDL_Plantfrom.SelectedItem.Value;
               // DispatchBOL.From_Plant = txt_PlantName.Text;
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
            txt_fat.Text = fatkg.ToString();

            double snfkg = (mkg * varsnf) / 100;
            //  txt_SNF.Text = snfkg.ToString();
            txt_SNF.Text = snfkg.ToString();

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
        catch (Exception ex)
        {
            catchmsg(ex.ToString());

        }

    }      

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(txt_Milkkg.Text)))
            {
                SETBO();
                insertDispatch(DispatchBOL);                             
                openingandprocurement();         
                getDate();
                GridView1.Visible = true;
                DespatchGriddata();
                txt_tankar.Focus();
            }
            else
            {
                lbl_msg.Visible = true;
                lbl_msg.ForeColor = System.Drawing.Color.Green;
                lbl_msg.Text = "Please, Check Your Data";
            }
        }

        catch (Exception ex)
        {
            WebMsgBox.Show("PLease  Check Your Data ");

            txt_Milkkg.Focus();
        }
    }


    public void insertDispatch(BOLDispatch DispatchBOL)
    {
        string sql = "Insert_DispatchMilk";
       InsertDispatch(DispatchBOL, sql);

    }


    public void InsertDispatch(BOLDispatch DispatchBOL, string sql)
    {
        try
        {
            using (SqlConnection con = DBclass.GetConnection())
            {
                string mess = string.Empty;
                int ress = 0;
                SqlCommand cmd = new SqlCommand();
                SqlParameter parfromplant, partopalant, pardate, parsession, parMilkkg, parfat, parsnf, parclr, parrate, paramount, parcompanycode, parplantcode, partype, parfatkg, parsnfkg, partankerno, ackmkg, ackfat, acksnf, ackclr, ackrate, ackamount, diffkg, diffat, diffsnf, diffclr, diffrate, diffamount, Status, tcnumber, parmess, parress;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                parfatkg = cmd.Parameters.Add("@Fat_Kg", SqlDbType.Float);
                parsnfkg = cmd.Parameters.Add("@Snf_Kg", SqlDbType.Float);
                parfromplant = cmd.Parameters.Add("@Plant_From", SqlDbType.NVarChar, 50);
                partopalant = cmd.Parameters.Add("@Plant_To", SqlDbType.NVarChar, 50);
                pardate = cmd.Parameters.Add("@Date", SqlDbType.DateTime);
                paramount = cmd.Parameters.Add("@Amount", SqlDbType.Float);
                parsession = cmd.Parameters.Add("@Session", SqlDbType.NVarChar, 20);
                parplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.NVarChar, 50);
                parcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
                partype = cmd.Parameters.Add("@type", SqlDbType.NVarChar, 50);
                parMilkkg = cmd.Parameters.Add("@MilkKg", SqlDbType.Float);
                parfat = cmd.Parameters.Add("@Fat", SqlDbType.Float);
                parsnf = cmd.Parameters.Add("@Snf", SqlDbType.Float);
                parclr = cmd.Parameters.Add("@clr", SqlDbType.Float);
                parrate = cmd.Parameters.Add("@Rate", SqlDbType.Float);
                partankerno = cmd.Parameters.Add("@Tanker_No", SqlDbType.NVarChar, 50);

                ackmkg = cmd.Parameters.Add("@Ack_milkkg", SqlDbType.Float);
                ackfat = cmd.Parameters.Add("@Ack_fat", SqlDbType.Float);
                acksnf = cmd.Parameters.Add("@Ack_snf", SqlDbType.Float);
                ackclr = cmd.Parameters.Add("@Ack_clr", SqlDbType.Float);
                ackrate = cmd.Parameters.Add("@Ack_rate", SqlDbType.Float);
                ackamount = cmd.Parameters.Add("@Ack_amount", SqlDbType.Float);
                diffkg = cmd.Parameters.Add("@diff_kg", SqlDbType.Float);
                diffat = cmd.Parameters.Add("@diff_fat", SqlDbType.Float);
                diffsnf = cmd.Parameters.Add("@diff_snf", SqlDbType.Float);
                diffclr = cmd.Parameters.Add("@diff_clr", SqlDbType.Float);
                diffrate = cmd.Parameters.Add("@diff_rate", SqlDbType.Float);
                diffamount = cmd.Parameters.Add("@diff_amount", SqlDbType.Float);
                Status = cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
                //   partankerno = cmd.Parameters.Add("@Tanker_No", SqlDbType.NVarChar, 50);
                tcnumber = cmd.Parameters.Add("@tcnumber", SqlDbType.NVarChar, 50);

                parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 50);
                cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
                parress = cmd.Parameters.Add("@ress", SqlDbType.Int);
                cmd.Parameters["@ress"].Direction = ParameterDirection.Output;

                parfatkg.Value = DispatchBOL.FATKG;
                parsnfkg.Value = DispatchBOL.SNFKG;
                parfromplant.Value = DispatchBOL.From_Plant;
                partopalant.Value = DispatchBOL.To_Plant;
                pardate.Value = DispatchBOL.FromDate;
                parsession.Value = DispatchBOL.Session;
                parplantcode.Value = DispatchBOL.Plantcode;
                parcompanycode.Value = Convert.ToInt32(DispatchBOL.Companycode);
                parMilkkg.Value = DispatchBOL.MilkKg;
                partype.Value = DispatchBOL.TYPE;
                parfat.Value = DispatchBOL.Fat;
                parsnf.Value = DispatchBOL.Snf;
                parclr.Value = DispatchBOL.Clr;
                parrate.Value = DispatchBOL.Rate;
                paramount.Value = DispatchBOL.Amount;
                partankerno.Value = DispatchBOL.TANKARNO;
                tcnumber.Value = DispatchBOL.TCNUMBER;

                ackmkg.Value = "0";
                ackfat.Value = "0";
                acksnf.Value = "0";
                ackclr.Value = "0";
                ackrate.Value = "0";
                ackamount.Value = "0";
                diffkg.Value = "0";
                diffat.Value = "0";
                diffsnf.Value = "0";
                diffclr.Value = "0";
                diffrate.Value = "0";
                diffamount.Value = "0";
                Status.Value = "Pending";

                cmd.ExecuteNonQuery();

                mess = (string)cmd.Parameters["@mess"].Value;
                ress = (int)cmd.Parameters["@ress"].Value;

                if (ress == 1)
                {
                    clr(); 
                    lbl_msg.Visible = true;
                    lbl_msg.ForeColor = System.Drawing.Color.Green;
                    lbl_msg.Text = "Record Inserted SuccessFully";
                }
                else
                {
                    lbl_msg.Visible = true;
                    lbl_msg.ForeColor = System.Drawing.Color.Green;
                    lbl_msg.Text = "Please Check Despatch Details Enter or Not ...";                    
                }               
               
            }
        }
        catch (Exception ex)
        {
            catchmsg(ex.ToString());
        }

    }


    public void openingandprocurement()
    {
        try
        {
            dat = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
            txt_fromdate.Text = dat.ToString("MM/dd/yyyy");
            SqlDataReader dr = null;
            dr = DispatchBLL.getopeningstcok(companycode, plantcode, txt_fromdate.Text);

            double milkkg = 0.0, fat = 0.0, snf = 0.0, sumofmilk = 0.0, rate = 0.0, sumofrate = 0.0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    milkkg = Convert.ToDouble(dr["milkkg"].ToString());
                    rate = 0;
                }
            }

            SqlDataReader dr1 = null;
            dr1 = DispatchBLL.getprocumentstcok(companycode, plantcode, txt_fromdate.Text);
            double procurementmilk = 0.0, fat1 = 0.0, snf1 = 0.0, sumoffat = 0.0, sumsnf = 0.0, rate1 = 0.0;

            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    try
                    {
                        procurementmilk = Convert.ToDouble(dr1["milk"].ToString());
                        fat1 = 0;
                        snf1 = 0;
                        rate1 = 0;
                    }

                    catch
                    {
                        procurementmilk = 0;
                        fat1 = 0;
                        snf1 = 0;
                        rate1 = 0;
                    }
                }
            }
            SqlDataReader dr2 = null;
            dr2 = DispatchBLL.getdispstcok(companycode, plantcode, txt_fromdate.Text);
            double dismilk = 0.0;


            if (dr2.HasRows)
            {
                while (dr2.Read())
                {

                    dismilk = Convert.ToDouble(dr2["MK"].ToString());


                }
            }
            double[] array1 = { fat, fat1 };
            double[] array2 = { snf, snf1 };
            double[] array3 = { rate, rate1 };
            sumofmilk = procurementmilk + milkkg - dismilk;
            sumofrate = array3.Average();
            sumoffat = array1.Average();
            sumsnf = array2.Average();
            txt_avail.Text = sumofmilk.ToString();
            txt_fromdate.Text = dat.ToString("dd/MM/yyyy");
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
              double avil = Convert.ToDouble(txt_avail.Text);
              double millk = Convert.ToDouble(txt_Milkkg.Text);

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
              decimal clr = Convert.ToDecimal(txt_Clr.Text);
              txt_Clr.Text = clr.ToString("#,0.00");

              decimal fatt = Convert.ToDecimal(txt_fat.Text);
              txt_fat.Text = fatt.ToString("#,0.00");


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
              double Clrval = 0;
              if (!(string.IsNullOrEmpty(txt_SNF.Text)))
                  snf = Convert.ToDouble(txt_SNF.Text);
              if (!(string.IsNullOrEmpty(txt_fat.Text)))
                  fat = Convert.ToDouble(txt_fat.Text);
              if ((snf > 0) && (fat > 0))
                  Clrval = (((snf - 0.36) - (fat * 0.21)) * 4);
              txt_Clr.Text = Clrval.ToString("f2");
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
              SqlConnection conn = DBclass.GetConnection();
              int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());             
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

      private void clr()
      {
          try
          {
              txt_tcnum.Text = "";
              txt_Milkkg.Text = "";
              txt_fat.Text = "";
              txt_SNF.Text = "";
              txt_Clr.Text = "";
              txt_rate.Text = "";
              txt_Amount.Text = "";
              txt_tankar.Text = "";
              txt_fromdate.Text = "";

          }
          catch (Exception ex)
          {
              catchmsg(ex.ToString());

          }
      }

      private void LoadDCnumber()
      {
          try
          {
              getDate();

              SqlDataReader dr = null;
              DDl_Dcnumber.Items.Clear();
              plantcode = ddl_Plantcode.SelectedItem.Value;
              string sqlstr = "SELECT tcnumber AS Dcnumber FROM DespatchEntry where plant_code='" + plantcode + "' AND Date between '" + fdate + "'  and '" + tdate + "' ORDER BY Dcnumber";
             
              dr = DBclass.GetDatareader(sqlstr);
              if (dr.HasRows)
              {
                  while (dr.Read())
                  {
                      DDl_Dcnumber.Items.Add(dr["Dcnumber"].ToString());                      
                  }
              }
          }
          catch (Exception ex)
          {
              lbl_msg.Visible = true;
              lbl_msg.ForeColor = System.Drawing.Color.Green;
              lbl_msg.Text = "Please, Enter Despatch Entry[NO DCNUMBER AVAIL]...";
          }
      }

      protected void DDl_Dcnumber_SelectedIndexChanged(object sender, EventArgs e)
      {
          LoadDCnumberDetails();  
      }


      private void LoadDCnumberDetails()
      {
          try
          {
              clr();
              txt_fromdate.Text = "";
              txt_tankar.Text = "";
              txt_tcnum.Text = "";
              getDate();

              SqlDataReader dr = null;
              plantcode = ddl_Plantcode.SelectedItem.Value;
              string sqlstr = "SELECT date,plant_To,session,Tanker_No,tcnumber FROM DespatchEntry where plant_code='" + plantcode + "' AND Date between '" + fdate + "'  and '" + tdate + "' AND tcnumber='" + DDl_Dcnumber.SelectedItem.Value + "' ";

              dr = DBclass.GetDatareader(sqlstr);
              if (dr.HasRows)
              {
                  while (dr.Read())
                  {
                      txt_fromdate.Text = (Convert.ToDateTime(dr["date"]).ToString("dd/MM/yyyy"));

                      if (dr["plant_To"].ToString() == "KALASTHIRI")
                      {
                          DDl_Plantto.SelectedIndex = 0;
                      }
                      else if (dr["plant_To"].ToString() == "SANGAM")
                      {
                          DDl_Plantto.SelectedIndex = 1;
                      }
                      else if (dr["plant_To"].ToString() == "VIRA")
                      {
                          DDl_Plantto.SelectedIndex = 2;
                      }
                      else if (dr["plant_To"].ToString() == "ONGOLE")
                      {
                          DDl_Plantto.SelectedIndex = 3;
                      }
                      else
                      {
                          DDl_Plantto.SelectedIndex = 0;
                      }

                      if (dr["session"].ToString() == "AM")
                      {
                          DDl_Plantto.SelectedIndex = 0;
                      }
                      else
                      {
                          DDl_Plantto.SelectedIndex = 1;
                      }

                      txt_tankar.Text = (dr["Tanker_No"].ToString());
                      txt_tcnum.Text = (dr["tcnumber"].ToString());
                      txt_Milkkg.Focus();
                  }
              }
          }
          catch (Exception ex)
          {
              lbl_msg.Visible = true;
              lbl_msg.ForeColor = System.Drawing.Color.Green;
              lbl_msg.Text = "Please, Enter Despatch Entry..."; 
              txt_Milkkg.Focus();
          }
      }




}