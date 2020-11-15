using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class DispatchandClosing : System.Web.UI.Page
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
    double fatval, snfval, fatval1, snfval1,clrval1;
    // static int savebtn = 0;
    DataTable dt = new DataTable();
    DateTime dtt = new DateTime();
    DateTime dat = new DateTime();
    DbHelper DBclass = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    BOLDispatch DispatchBOL = new BOLDispatch();
    BLLDispatch DispatchBLL = new BLLDispatch();
    DALDispatch DispatchDAL = new DALDispatch();
    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());

                //txt_fat.Text = fatval.ToString();
                //txt_SNF.Text = snfval.ToString();

                //txt_fat1.Text = fatval1.ToString();
                //txt_SNF1.Text = snfval1.ToString();

                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();



                if (roleid < 3)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                plantcode = ddl_Plantcode.SelectedItem.Value;
                txt_PlantName.Text = DDL_Plantfrom.SelectedItem.Value;
               
                GridView1.Visible = true;
                GridView2.Visible = false;
                pager.Visible = true;
                DataPager1.Visible = false;
                
                //Ddl_Type.Focus();
                dtt = System.DateTime.Now;
                txt_fromdate.Text = dtt.ToString("dd/MM/yyyy");
                openingandprocurement();

                if (despatchstock_chk.Checked == true)
                {
                    Label1.Visible = true;
                    DDL_Plantfrom.Visible = true;
                    DDl_Plantto.Visible = true;
                    lbl_ToPlant.Visible = true;
                    Panel2.Visible = false;
                    Panel1.Visible = true;
                    closestock_chk.Checked = false;
                    GridView1.Visible = true;
                    GridView2.Visible = false;
                }
                else
                {
                    Label1.Visible = false;
                    DDL_Plantfrom.Visible = false;
                    DDl_Plantto.Visible = false;
                    lbl_ToPlant.Visible = false;
                    Panel2.Visible = true;
                    Panel1.Visible = false;
                    closestock_chk.Checked = true;
                    GridView1.Visible = false;
                    GridView2.Visible = true;

                }
                dloadgriddata();
                //fatval = Convert.ToDouble(txt_fat.Text);

                //snfval = Convert.ToDouble(txt_SNF.Text);

                //txt_fat1.Text = fatval1.ToString();
                //txt_SNF1.Text = snfval1.ToString();


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
                plantcode = Session["Plant_Code"].ToString();

                plantcode = ddl_Plantcode.SelectedItem.Value;
                txt_PlantName.Text = DDL_Plantfrom.SelectedItem.Value;

                dloadgriddata();
                GridView1.Visible = false;
                GridView2.Visible = false;
                pager.Visible = false;
                DataPager1.Visible = false;

                //txt_fromdate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
                //Ddl_Type.Focus();
                openingandprocurement();            
               
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



    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {

            if (validate1())
            {
                SETBO();
                DispatchBLL.insertDispatch(DispatchBOL);
                clr();
                //loadgriddata();
                // dloadgriddata();

                GridView1.Visible = true;
                GridView2.Visible = false;
                openingandprocurement();
                //uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
                getopening();
                dloadgriddata();
                clr1();
                txt_Milkkg.Focus();
                //if (Ddl_Type.SelectedItem.Text == "CLOSING STOCK")
                //{
                //    SETBO1();
                //    DispatchBLL.insertStockDetails(DispatchBOL);
                //    loadgriddata1();
                //    openingandprocurement();
                //    //WebMsgBox.Show("Check the MILK KG");
                //    GridView1.Visible = false;
                //    GridView2.Visible = true;
                //    uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
                //    clr1();

                //}

            }
            else
            {
                dloadgriddata();
                txt_Milkkg.Focus();
                //WebMsgBox.Show("Saved Successfully...");
               // uscMsgBox1.AddMessage("", MessageBoxUsc_Message.enmMessageType.Success);
            }
        }
        catch (Exception ex)
        {
            txt_Milkkg.Focus();
        }

    }

    //private void loadsingleplant()
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;
    //        dr = DispatchBLL.LoadSinglePlantcode(companycode, plantcode);
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {

    //                //dd = dr["Plant_Name"].ToString();
    //                //DDl_Plantto.Items.Add(dr["Plant_Name"].ToString());
    //                DDL_Plantfrom.Items.Add(dr["Plant_Name"].ToString());

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}
    

    private bool validate1()
    {



        if (String.IsNullOrEmpty(txt_fromdate.Text))
        {
            uscMsgBox1.AddMessage("Enter the From Date", MessageBoxUsc_Message.enmMessageType.Attention);

            return false;
        }
        //DateTime fromdate = DateTime.Parse(txt_fromdate.Text);





        //if (DDl_Plantto.SelectedIndex == 0)
        //{

        //    uscMsgBox1.AddMessage("Choose the Plant To", MessageBoxUsc_Message.enmMessageType.Attention);
        //    DDl_Plantto.Focus();
        //}

        //if (dl_Session.SelectedIndex == 0)
        //{

        //    uscMsgBox1.AddMessage("Choose the Session", MessageBoxUsc_Message.enmMessageType.Attention);
        //    dl_Session.Focus();
        //}




        if (string.IsNullOrEmpty(txt_Milkkg.Text))
        {


            uscMsgBox1.AddMessage("Enter the Milk KG", MessageBoxUsc_Message.enmMessageType.Attention);
            txt_Milkkg.Focus();
            return false;
        }


        if (String.IsNullOrEmpty(txt_fat.Text))
        {
            uscMsgBox1.AddMessage("Enter the Minimum FAT Value", MessageBoxUsc_Message.enmMessageType.Attention);


            txt_fat.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txt_SNF.Text))
        {
            uscMsgBox1.AddMessage("Enter the Minimum SNF Value", MessageBoxUsc_Message.enmMessageType.Attention);


            txt_SNF.Focus();
            return false;
        }
        if (String.IsNullOrEmpty(txt_Clr.Text))
        {
            uscMsgBox1.AddMessage("Enter the Clr Value", MessageBoxUsc_Message.enmMessageType.Attention);


            txt_Clr.Focus();
            return false;
        }

        if (String.IsNullOrEmpty(txt_rate.Text))
        {
            uscMsgBox1.AddMessage("Enter the Rate", MessageBoxUsc_Message.enmMessageType.Attention);


            txt_rate.Focus();

            return false;
        }
        if (String.IsNullOrEmpty(txt_Amount.Text))
        {

            txt_Amount.Text = "0";
            return false;
        }


        //if (string.IsNullOrEmpty(txt_avail.Text))
        //{

        //    uscMsgBox1.AddMessage("Stock Is Not Available", MessageBoxUsc_Message.enmMessageType.Attention);

        //    return false;

        //}

        //double mkg = Convert.ToDouble(txt_Milkkg.Text.Trim());
        //double avlmkg = Convert.ToDouble(txt_avail.Text.Trim());

        //if (mkg > avlmkg)
        //{
        //    uscMsgBox1.AddMessage("Dispatch Milkkg is Greater Available MilkKg", MessageBoxUsc_Message.enmMessageType.Attention);
        //    return false;
        //}



        return true;



    }

    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_PlantName.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }













    public void SETBO()
    {

        // string companycode = "1";
        // string plantcode = "111";

        DispatchBOL.Companycode = int.Parse(companycode);
        DispatchBOL.Plantcode = plantcode;
        DispatchBOL.FromDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
        //DispatchBOL.ToDate = DateTime.Parse(txt_todate.Text);
        DispatchBOL.Session = dl_Session.SelectedItem.Value;
        if (ddl_Plantcode.Text != string.Empty)
        {
            DispatchBOL.From_Plant = DDL_Plantfrom.SelectedItem.Value;
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
        double mkg = double.Parse(txt_Milkkg.Text);
        DispatchBOL.MilkKg = mkg;
        DispatchBOL.TYPE = "despatch";
        double RATE = double.Parse(txt_rate.Text);
        DispatchBOL.Rate = RATE;
        double Assignmilkkg = double.Parse(txt_Milkkg.Text);

        double fatkg = (mkg * varfat) / 100;
        txt_fat.Text = fatkg.ToString();
        // txt_Fatkg.Text = fatkg.ToString();

        double snfkg = (mkg * varsnf) / 100;
        txt_SNF.Text = snfkg.ToString();
        //    txt_Snfkg.Text = snfkg.ToString();

        DispatchBOL.FATKG = double.Parse(txt_fat.Text);
        DispatchBOL.SNFKG = double.Parse(txt_SNF.Text);
        DispatchBOL.Amount = double.Parse(txt_Amount.Text);
        //DispatchBOL.STATUS = "Pending";
        
        if (txt_tankar.Text!= string.Empty)
        {


            DispatchBOL.TANKARNO = txt_tankar.Text;
        }
        else
        {
            WebMsgBox.Show("Please Enter Tanker No...");

        }

        txt_Milkkg.Focus();

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

            txt_Milkkg.Focus();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }



    }







    private void clr()
    {

        // txt_fromdate.Text = "";
        //txt_todate.Text = "";
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
        txt_stockdetails.Text = "";
        txt_tankar.Text = "";
    }
    private void clr1()
    {

       // txt_Milkkg.Text = "";
        txt_avail1.Text = "";
        //txt_rate.Text = "";
        txt_fat1.Text = "";
        txt_SNF1.Text = "";
        txt_Clr1.Text = "";
        txt_tankar.Text = "";
        //  txt_PlantName.Text = "";
        //  txt_Amount.Text = "";
        //  txt_stockdetails.Text = "";
    }




    private void loadgriddata()
    {


       // GridView1.DataBind();
    }
    private void loadgriddata1()
    {

       // GridView2.DataBind();
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
            ex.ToString();
        }


    }
    protected void txt_rate_TextChanged(object sender, EventArgs e)
    {
        //double RATE = double.Parse(txt_rate.Text);
        ////DispatchBOL.Rate = RATE;
        //double Assignmilkkg = double.Parse(txt_Milkkg.Text);
        //if (txt_Milkkg.Text != null)
        //{

        //    double ToAMount = Assignmilkkg * (RATE);
        //  //  DispatchBOL.Amount = ToAMount;
        //    txt_Amount.Text = ToAMount.ToString();
        //    txt_PlantName.Focus();

        //}
        //else
        //{
        //   // WebMsgBox.Show("please Enter MILK KG");
        //}
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
    public void getopening()
    {
        SqlDataReader dr;
        dr = DispatchBLL.getopening(plantcode);
        if (dr.HasRows)
        {
            while (dr.Read())
            {

            }
        }

        else
        {

            SETBO2();
            DispatchBLL.openingtStockDetails(DispatchBOL);
        }

    }

    public void SETBO2()
    {
        try
        {


            DispatchBOL.Companycode = int.Parse(companycode);
            DispatchBOL.Plantcode = plantcode;
            DispatchBOL.PlantName = txt_PlantName.Text;
            DispatchBOL.FromDate = Convert.ToDateTime(txt_fromdate.Text);
            DispatchBOL.Fat = 0;
            DispatchBOL.Snf = 0;
            DispatchBOL.Clr = 0;
            DispatchBOL.MilkKg = 0;
            DispatchBOL.STORAGENAME = txt_stockdetails.Text;
            DispatchBOL.Rate = 0;
            DispatchBOL.Amount = 0;
            DispatchBOL.FATKG = 0;
            DispatchBOL.SNFKG = 0;

            txt_Milkkg.Focus();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }



    }



    //public void getclosingstock()
    //{

    //    SqlDataReader dr = null;
    //    dr = DispatchBLL.getprocureandispatch(plantcode, datee);
    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {


    //            txt_Milkkg.Text = dr["netmilk"].ToString();
    //            milkclose  = Convert.ToDouble( txt_Milkkg.Text);

    //            if (txt_Milkkg.Text == null)
    //            {

    //                txt_Milkkg.Text = "0";
    //            }
    //        }
    //    }

    //}


    public void openingandprocurement()
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
                //fat = Convert.ToDouble(dr["Fat"].ToString());
                //snf = Convert.ToDouble(dr["snf"].ToString());
                //rate = Convert.ToDouble(dr["rate"].ToString());


                //   fat = 0;
                //   snf = 0;
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
                    //fat1 = Convert.ToDouble(dr1["fat"].ToString());
                    //snf1 = Convert.ToDouble(dr1["snf"].ToString());
                    //rate1 = Convert.ToDouble(dr1["rates"].ToString());

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
                //milkkgclose = txt_Milkkg.Text.ToString();


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
        //txt_fat.Text = sumoffat.ToString();
        //txt_SNF.Text = sumsnf.ToString();
        //txt_rate.Text = sumofrate.ToString();

        //try
        //{
        //    if (!(string.IsNullOrEmpty(txt_SNF.Text)))
        //        snf = Convert.ToDouble(txt_SNF.Text);
        //    if (!(string.IsNullOrEmpty(txt_fat.Text)))
        //        fat = Convert.ToDouble(txt_fat.Text);
        //    if ((snf > 0) && (fat > 0))
        //        txt_Clr.Text = Convert.ToString(((snf - 0.36) - (fat * 0.2)) * 4);
        //    txt_rate.Focus();
        //}
        //catch (Exception ex)
        //{
        //    ex.ToString();
        //}

        //double ToAMount = dismilk * (sumofrate);
        ////  DispatchBOL.Amount = ToAMount;
        //txt_Amount.Text = ToAMount.ToString();
        //txt_PlantName.Focus();


        txt_fromdate.Text = dat.ToString("dd/MM/yyyy");


    }


    //public void avilmilkg()
    //{

    //    SqlDataReader dr = null;
    //    dr = DispatchBLL.getprocureandispatch(plantcode, datee);
    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {

    //            getmilk = dr["netmilk"].ToString();
    //            txt_avail.Text = getmilk.ToString();

    //        }
    //    }

    //}



    protected void txt_Milkkg_TextChanged(object sender, EventArgs e)
    {
        double avil = Convert.ToDouble(txt_avail.Text);
        double millk = Convert.ToDouble(txt_Milkkg.Text);
  //      txt_rate.Text = "0";
      //  double milkrate = Convert.ToDouble(txt_rate.Text);
       
        txt_fat.Focus();



        //if (avil < millk)
        //{

        //    uscMsgBox1.AddMessage("Enter the Available Milk", MessageBoxUsc_Message.enmMessageType.Attention);
        //    txt_Milkkg.Focus();

        //}
        //else
        //{

        //    txt_Amount.Text =( millk * milkrate).ToString();

        //}


    }

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
            DispatchBOL.Clr = double.Parse(txt_Clr.Text);
            double mkg = double.Parse(txt_avail1.Text);
            DispatchBOL.MilkKg = mkg;
            DispatchBOL.STORAGENAME = txt_stockdetails.Text;
            //DispatchBOL.PlantName=
            // DispatchBOL.TYPE = Ddl_Type.SelectedItem.Value;
            double RATE = double.Parse(txt_rate.Text);
            DispatchBOL.Rate = RATE;
            double Assignmilkkg = double.Parse(txt_Milkkg.Text);

            double fatkg = (mkg * varfat) / 100;

            txt_fat1.Text = fatkg.ToString();

            double snfkg = (mkg * varsnf) / 100;

            txt_SNF1.Text = snfkg.ToString();

            DispatchBOL.FATKG = double.Parse(txt_fat1.Text);
            DispatchBOL.SNFKG = double.Parse(txt_SNF1.Text);

            DispatchBOL.Amount = double.Parse(txt_Amount.Text);

            txt_Milkkg.Focus();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SETBO1();
            DispatchBLL.insertStockDetails(DispatchBOL);
            loadgriddata1();
            openingandprocurement();
            //WebMsgBox.Show("Check the MILK KG");
            GridView1.Visible = false;
            GridView2.Visible = true;
            //uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);

            //   getopening();
            SETBO3();
            DispatchBLL.openingtStockDetails(DispatchBOL);
            Cloadgriddata();
            clr1();
            // clr();
            openingandprocurement();
            Label1.Visible = false;
            DDL_Plantfrom.Visible = false;
            DDl_Plantto.Visible = false;
            lbl_ToPlant.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = true;
            despatchstock_chk.Checked = false;
            GridView2.Visible = true;
            GridView1.Visible = false;

            //
            lbl_session.Visible = false;
            dl_Session.Visible = false;
            lbl_session0.Visible = false;
            txt_Milkkg.Visible = false;
            lbl_session1.Visible = false;
            txt_fat.Visible = false;
            lbl_session2.Visible = false;
            txt_SNF.Visible = false;
            lbl_session3.Visible = false;
            txt_Clr.Visible = false;
            btn_Save.Visible = false;
            txt_avail.Text = "0";
            txt_avail1.Focus();
        }
        catch (Exception ex)
        {
            txt_avail1.Focus();
        }

    }


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

        if (closestock_chk.Checked == true)
        {

            DDl_Plantto.Visible = false;
            lbl_ToPlant.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = true;
        }


    }
    protected void closestock_chk_CheckedChanged(object sender, EventArgs e)
    {
        Cloadgriddata();

        if (closestock_chk.Checked == true)
        {
            Label1.Visible = false;
            DDL_Plantfrom.Visible = false;
            DDl_Plantto.Visible = false;
            lbl_ToPlant.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = true;
            despatchstock_chk.Checked = false;
            GridView2.Visible = true;
            GridView1.Visible = false;

            //
            lbl_session.Visible = false;
            dl_Session.Visible = false;
            lbl_session0.Visible = false;
            txt_Milkkg.Visible = false;
            lbl_session1.Visible = false;
            txt_fat.Visible = false;
            lbl_session2.Visible = false;
            txt_SNF.Visible = false;
            lbl_session3.Visible = false;
            txt_Clr.Visible = false;
            btn_Save.Visible = false;

           
           

        }
        else
        {
            Label1.Visible = true;
            DDL_Plantfrom.Visible = true;
            DDl_Plantto.Visible = true;
            lbl_ToPlant.Visible = true;
            Panel1.Visible = true;
            Panel2.Visible = false;
            despatchstock_chk.Checked = true;
            GridView2.Visible = false;
            GridView1.Visible = true;
            //
            lbl_session.Visible = true;
            dl_Session.Visible = true;
            lbl_session0.Visible = true;
            txt_Milkkg.Visible = true;
            lbl_session1.Visible = true;
            txt_fat.Visible = true;
            lbl_session2.Visible = true;
            txt_SNF.Visible = true;
            lbl_session3.Visible = true;
            txt_Clr.Visible = true;
            btn_Save.Visible = true;
         
        }
    }
    protected void dl_Session_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void despatchstock_chk_CheckedChanged(object sender, EventArgs e)
    {

        if (despatchstock_chk.Checked == true)
        {
            Label1.Visible = true;
            DDL_Plantfrom.Visible = true;
            DDl_Plantto.Visible = true;
            lbl_ToPlant.Visible = true;
            Panel2.Visible = false;
            Panel1.Visible = true;
            closestock_chk.Checked = false;
            GridView1.Visible = true;
            GridView2.Visible = false;

            //
            lbl_session.Visible = true;
            dl_Session.Visible = true;
            lbl_session0.Visible = true;
            txt_Milkkg.Visible = true;
            lbl_session1.Visible = true;
            txt_fat.Visible = true;
            lbl_session2.Visible = true;
            txt_SNF.Visible = true;
            lbl_session3.Visible = true;
            txt_Clr.Visible = true;
            btn_Save.Visible = true;
        }
        else
        {
            Label1.Visible = false;
            DDL_Plantfrom.Visible = false;
            DDl_Plantto.Visible = false;
            lbl_ToPlant.Visible = false;
            Panel2.Visible = true;
            Panel1.Visible = false;
            closestock_chk.Checked = true;
            GridView1.Visible = false;
            GridView2.Visible = true;

            //
            lbl_session.Visible = false;
            dl_Session.Visible = false;
            lbl_session0.Visible = false;
            txt_Milkkg.Visible = false;
            lbl_session1.Visible = false;
            txt_fat.Visible = false;
            lbl_session2.Visible = false;
            txt_SNF.Visible = false;
            lbl_session3.Visible = false;
            txt_Clr.Visible = false;
            btn_Save.Visible = false;
        }
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
            ex.ToString();
        }


    }
    protected void txt_avail1_TextChanged(object sender, EventArgs e)
    {
        txt_fat1.Focus();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dloadgriddata();
        GridView1.EditIndex = e.RowIndex;
        int index = GridView1.EditIndex;
        GridViewRow row = GridView1.Rows[index];
        dr = dt.Rows[index];
        DeleteFlag = 1;

        DispatchBOL.Tid = Convert.ToInt32(dr["Tid"]);
        DispatchBOL.Flag = DeleteFlag;
        DispatchBOL.Plantcode = plantcode;
        DispatchBOL.Companycode = Convert.ToInt32(companycode);
        DispatchBLL.DespatchDlelteRow(DispatchBOL);
        DispatchBOL.Flag = 0;
        dloadgriddata();
    }

    private void dloadgriddata()
    {
        int dtcount;
        dt = null;
        dt = DispatchBLL.DespatchGriddata(plantcode, companycode);
        dtcount = dt.Rows.Count;
        //GridView1.Dispose();
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //WebMsgBox.Show("Despatch have Empty Row Only...");
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            Button1.Focus();
        }
        catch (Exception ex)
        {
            ex.ToString();
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
            txt_SNF.Text = val.ToString("f2");

            //txt_Clr1.Focus();
            // txt_SNF1.Text = snfval1.ToString();
            txt_fat.Text = fatval1.ToString();
            txt_Clr.Text = clrval1.ToString();
            txt_rate.Focus();
         //   btn_Save.Focus();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
   
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Cloadgriddata();
        GridView2.EditIndex = e.RowIndex;
        int index = GridView2.EditIndex;
        GridViewRow row = GridView2.Rows[index];
        dr = dt.Rows[index];
        DeleteFlag = 2;

        DispatchBOL.Tid = Convert.ToInt32(dr["Tid"]);
        DispatchBOL.Flag = DeleteFlag;
        DispatchBOL.Plantcode = plantcode;
        DispatchBOL.Companycode = Convert.ToInt32(companycode);       
        DispatchBLL.CloseDlelteRow(DispatchBOL);
        Cloadgriddata();
        DispatchBOL.Flag = 0;
       
    }
    private void Cloadgriddata()
    {
        int dtcount;
        dt = null;
        dt = DispatchBLL.CloseGriddata(plantcode, companycode);
        dtcount = dt.Rows.Count;
        //GridView1.Dispose();
        if (dtcount > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();

            //WebMsgBox.Show("Despatch have Empty Row Only...");
        }

    }
    protected void DDL_Plantfrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = DDL_Plantfrom.SelectedIndex;
        plantcode = ddl_Plantcode.SelectedItem.Value;
        txt_PlantName.Text = DDL_Plantfrom.SelectedItem.Value;
        dloadgriddata();
    }
   
   
}