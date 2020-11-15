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
using System.Text.RegularExpressions;

public partial class OpeningStock : System.Web.UI.Page
{

    double snf, fat;
    string uid;
    string dd,datee;
   // int cmpcode = 1, pcode;
    public string companycode;
    public string plantcode, plantname;
    static int savebtn = 0;
    DataTable dt = new DataTable();
    DateTime dtm = new DateTime(); 
    DbHelper DBclass = new DbHelper();
    BOLDispatch DispatchBOL = new BOLDispatch();
    BLLDispatch DispatchBLL = new BLLDispatch();
    DALDispatch DispatchDAL = new DALDispatch();
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {


        //openingmilkg();
       
        if (IsPostBack != true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                roleid = Convert.ToInt32(Session["Role"].ToString());
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
                plantname = Session["Plant_Name"].ToString();
                txt_PlantName.Text = plantname;
                dtm = System.DateTime.Now;
                txt_fromdate.Text = dtm.ToString("dd/MM/yyyy");
                datee = txt_fromdate.Text;
                txt_stockdetails.Focus();
                // txt_Milkkg.Focus();
                //LoadPlantcode();
                openingmilkg();


               

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
                //uid = Session["User_ID"].ToString();

                //if (Request.QueryString["id"] != null)

                //    Response.Write("querystring passed in: " + Request.QueryString["id"]);
                //else
                //    Response.Write("");
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();
              
                txt_Milkkg.Focus();
                openingmilkg();

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
            

        }

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {


        if (validate1())
        {
            //LoadPlantcode();
            SETBO();
            DispatchBLL.openingtStockDetails(DispatchBOL);
            loadgriddata();
            clr();
            uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
            //WebMsgBox.Show("Inserted SucessFully");



        }
        else
        {

            //uscMsgBox1.AddMessage("Check the MILK KG", MessageBoxUsc_Message.enmMessageType.Attention);
            //WebMsgBox.Show("Check the MILK KG");

        }


    }

    private bool validate1()
    {


        if (String.IsNullOrEmpty(txt_fromdate.Text))
        {
            uscMsgBox1.AddMessage("Enter the From Date", MessageBoxUsc_Message.enmMessageType.Attention);

            return false;
        }
        //DateTime fromdate = DateTime.Parse(txt_fromdate.Text);
        
       


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
        if (String.IsNullOrEmpty(txt_PlantName.Text))
        {
            uscMsgBox1.AddMessage("Enter the Plantname", MessageBoxUsc_Message.enmMessageType.Attention);
            txt_PlantName.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txt_stockdetails.Text))
        {
            uscMsgBox1.AddMessage("Enter the Store Name", MessageBoxUsc_Message.enmMessageType.Attention);
            txt_stockdetails.Focus();
            return false;

        }
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
        try
        {

            //string companycode = "1";
            //string plantcode = "111";
           // string plantname = "PONDY";
            DispatchBOL.Companycode = int.Parse(companycode);
            DispatchBOL.Plantcode = plantcode;
            DispatchBOL.PlantName = txt_PlantName.Text;
            //txt_PlantName.Text = plantname;
            DispatchBOL.FromDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
            //DispatchBOL.ToDate = DateTime.Parse(txt_todate.Text);
            //DispatchBOL.Session = dl_Session.SelectedItem.Value;
            //DispatchBOL.From_Plant = DDL_Plantfrom.SelectedItem.Value;
            //DispatchBOL.To_Plant = DDl_Plantto.SelectedItem.Value;
            double varfat = double.Parse(txt_fat.Text);
            DispatchBOL.Fat = varfat;
            double varsnf = double.Parse(txt_SNF.Text);
            DispatchBOL.Snf = varsnf;
            DispatchBOL.Clr = double.Parse(txt_Clr.Text);
            double mkg = double.Parse(txt_Milkkg.Text);
            DispatchBOL.MilkKg = mkg;
            DispatchBOL.STORAGENAME = txt_stockdetails.Text;
            //DispatchBOL.PlantName=
            // DispatchBOL.TYPE = Ddl_Type.SelectedItem.Value;
            double RATE = double.Parse(txt_rate.Text);
            DispatchBOL.Rate = RATE;
            double Assignmilkkg = double.Parse(txt_Milkkg.Text);

            double fatkg = (mkg * varfat) / 100;
            txt_Fatkg.Text = fatkg.ToString();

            double snfkg = (mkg * varsnf) / 100;
            txt_Snfkg.Text = snfkg.ToString();

            DispatchBOL.FATKG = double.Parse(txt_Fatkg.Text);
            DispatchBOL.SNFKG = double.Parse(txt_Snfkg.Text);

            DispatchBOL.Amount = double.Parse(txt_Amount.Text);

            txt_Milkkg.Focus();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }



    }

    private void clr()
    {
        txt_Milkkg.Text = "";

        txt_rate.Text = "";
        txt_fat.Text = "";
        txt_SNF.Text = "";
        txt_Clr.Text = "";

        txt_Amount.Text = "";
        txt_stockdetails.Text = "";
        //txt_PlantName.Text = "";
    }

    //private void LoadPlantcode()
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;
    //        dr = DispatchBLL.LoadPlantcode();
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {

    //                //dd = dr["Plant_Name"].ToString();
    //                DDl_Plantto.Items.Add(dr["Plant_Name"].ToString());
    //                DDL_Plantfrom.Items.Add(dr["Plant_Name"].ToString());

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WebMsgBox.Show(ex.ToString());
    //    }
    //}

    private void loadgriddata()
    {

        GridView1.DataBind();
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
                txt_Clr.Text = Convert.ToString(((snf - 0.36) - (fat * 0.2)) * 4);
            txt_rate.Focus();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

    }
    protected void txt_rate_TextChanged(object sender, EventArgs e)
    {
        double RATE = double.Parse(txt_rate.Text);
        //DispatchBOL.Rate = RATE;
        double Assignmilkkg = double.Parse(txt_Milkkg.Text);
        if (txt_Milkkg.Text != null)
        {

            double ToAMount = Assignmilkkg * (RATE);
            //  DispatchBOL.Amount = ToAMount;
            txt_Amount.Text = ToAMount.ToString();

        }
        else
        {
            WebMsgBox.Show("please Enter MILK KG");
        }

    }

    public void openingmilkg()
    {
        //string getmilk;
        //SqlDataReader dr = null;
        //dr = DispatchBLL.getclosingstcok(plantcode, datee);
        //if (dr.HasRows)
        //{
        //    while (dr.Read())
        //    {
        //        getmilk = dr["milk"].ToString();
        //        txt_Milkkg.Text = Convert.ToDouble(getmilk).ToString();
        //        if (txt_Milkkg.Text == null)
        //        {

        //            txt_Milkkg.Text = "0";
        //        }
        //    }

        //}
    }
    protected void txt_fat_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(txt_fat.Text)))
                snf = Convert.ToDouble(txt_SNF.Text);
            if (!(string.IsNullOrEmpty(txt_fat.Text)))
                fat = Convert.ToDouble(txt_fat.Text);
            if ((snf > 0) && (fat > 0))
                txt_Clr.Text = Convert.ToString(((snf - 0.36) - (fat * 0.2)) * 4);
            txt_rate.Focus();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}