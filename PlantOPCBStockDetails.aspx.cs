using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;

public partial class PlantOPCBStockDetails : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    DataTable dt = new DataTable();
    DataTable datamobile = new DataTable();
    DataTable datamDate = new DataTable();
    DataSet ds = new DataSet();
    string msg;
   // Exception ex;
    string plant;
    string[] GET;
    double getmilk;
    double fat;
    double Snf;


    string uid;
    string password;
    string senderId;
    string message;
    string message1, messagesave;
   
    int count = 0;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                dtm = System.DateTime.Now;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");

                roleid = Convert.ToInt32(Session["Role"].ToString());

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                //txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();

                }

                else
                {

                    LoadPlantcode();
                }
                DateTime txtMyDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                //DateTime dttt = DateTime.Parse(txt_FromDate.Text);

                //DateTime cutime = DateTime.Parse(gettime);
                //string sff = dttt.AddDays(1).ToString();

                //GETDATE = txt_FromDate.Text;

                DateTime date = txtMyDate.AddDays(-1);

                string datee = date.ToString("MM/dd/yyyy");
                // DateTime DDD = DateTime.ParseExact(date);
                Button7.Visible = false;
            }
            else
            {



            }

        }


        else
        {
            ccode = Session["Company_code"].ToString();
            //  LoadPlantcode();

            plant = ddl_Plantname.Text;
            GET = plant.Split('_');


        }
    }


    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());



                }
            }
            else
            {
                ddl_Plantcode.Items.Add("--Select PlantName--");
                ddl_Plantname.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");


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



    protected void Button5_Click(object sender, EventArgs e)
    {
        getgrid();
       
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddl_Plantname.Text + ":Stock Details:" + "Date:" + txt_FromDate.Text;
            HeaderCell2.ColumnSpan = 10;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    public void getgrid()
    {
        try
        {
            dt.Rows.Clear();

            //Session["op"] = "";
            //Session["am"] = "";
            //Session["pm"] = "";
            //Session["tot"] = "";
            //Session["des"] = "";
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            DateTime txtMyDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            Session["Date"] = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            DateTime date = txtMyDate.AddDays(-1);
            string datee = date.ToString("MM/dd/yyyy");
            string str = "";
            con = DB.GetConnection();
            str = "select  convert(varchar,Date,103) as Date     from Stock_Milk  where plant_code='" + GET[0] + "' and date  between '" + d1 + "' and '" + d1 + "'  ORDER by Date asc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            //foreach (datarow dd in dt.Rows)
            //{

            dt.Columns.Add("OpMilk");
            dt.Columns.Add("ProAm");
            dt.Columns.Add("ProPm");
            dt.Columns.Add("TotProMilk");
            dt.Columns.Add("DespMilk");
            dt.Columns.Add("ClMilk");
            dt.Columns.Add("ClFat");
            dt.Columns.Add("ClSnf");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            string op = "  select OpMilkKg   from(Select    DATEADD(day,-1,Date ) AS Date,sUM(MilkKg) as OpMilkKg,sum(Fat) as Fat ,Sum(sNF) as Snf FROM Stock_Milk   WHERE Plant_code='" + GET[0] + "'  AND DATE='" + datee + "' group by Date) as op";
            SqlCommand sc = new SqlCommand(op, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);
            //DataSet ds = new DataSet();
            //da1.Fill(ds);
            da1.Fill(ds, "TABLEop");

            Session["op"] = ds.Tables[0].Rows[0][0].ToString();
            string proam = "Select    isnull(SUM(Milkkg),0) as AmMilkkg FROM Lp   WHERE PlantCode='" + GET[0] + "'  AND prdate='" + d1 + "'   and  Sessions='Am' group by Prdate";
            SqlCommand sc1 = new SqlCommand(proam, con);
            SqlDataAdapter da2 = new SqlDataAdapter(sc1);
      //      DataSet ds1 = new DataSet();
            da2.Fill(ds, "PROCAM");
            Session["am"] = ds.Tables[1].Rows[0][0].ToString();
            string propm = "Select   isnull(SUM(Milkkg),0)as AmMilkkg FROM Lp   WHERE PlantCode='" + GET[0] + "'  AND prdate='" + d1 + "'   and  Sessions='pm' group by Prdate";
            SqlCommand sc2 = new SqlCommand(propm, con);
            SqlDataAdapter da3 = new SqlDataAdapter(sc2);
          //  DataSet ds2 = new DataSet();
            da3.Fill(ds, "PROCPM");
            Session["pm"] = ds.Tables[2].Rows[0][0].ToString();
            string proc = "Select  CONVERT(DECIMAL(18,2),isnull(SUM(Milkkg),0)) as AmMilkkg FROM Lp   WHERE PlantCode='" + GET[0] + "'  AND prdate='" + d1 + "'  group by Prdate";
            SqlCommand sc3 = new SqlCommand(proc, con);
            SqlDataAdapter da4 = new SqlDataAdapter(sc3);
          //  DataSet ds3 = new DataSet();

            da4.Fill(ds, "PROCTOT");
            Session["tot"] = ds.Tables[3].Rows[0][0].ToString();
            string desp = "Select  ISNULL( Sum(MilkKg),0) as  DesMilkKg from Despatchnew   WHERE Plant_code='" + GET[0] + "'  AND Date='" + d1 + "'";
            SqlCommand sc4 = new SqlCommand(desp, con);
            SqlDataAdapter da5 = new SqlDataAdapter(sc4);
          //  DataSet ds4 = new DataSet();
            da5.Fill(ds, "DESP");
            Session["des"] = ds.Tables[4].Rows[0][0].ToString();
            string close = " select CbMilkKg,Fat,Snf   from(Select    Date,sUM(MilkKg) as CbMilkKg,sum(Fat) as Fat ,Sum(sNF) as Snf FROM Stock_Milk   WHERE Plant_code='" + GET[0] + "'  AND  Date='" + d1 + "' group by Date) as Cb";
            SqlCommand sc5 = new SqlCommand(close, con);
            SqlDataAdapter da6 = new SqlDataAdapter(sc5);
          //  DataSet ds5 = new DataSet();
            da6.Fill(ds, "cLOSE");
       

            //Session["op"] = ds.Tables[0];
            //Session["am"] = ds.Tables[1];
            //Session["pm"] = ds.Tables[2];
            //Session["tot"] = ds.Tables[3];
            //Session["des"] = ds.Tables[4];
            DataTable Close = ds.Tables[5];
            GridView1.Rows[0].Cells[2].Text = Session["op"].ToString();
            GridView1.Rows[0].Cells[3].Text = Session["am"].ToString();
            GridView1.Rows[0].Cells[4].Text = Session["pm"].ToString();
            GridView1.Rows[0].Cells[5].Text = Session["tot"].ToString();
            GridView1.Rows[0].Cells[6].Text = Session["des"].ToString();
            foreach (DataRow dtk in Close.Rows)
            {
                getmilk = Convert.ToDouble(dtk[0].ToString());
                fat = Convert.ToDouble(dtk[1].ToString());
                Snf = Convert.ToDouble(dtk[2].ToString());
            }
            GridView1.Rows[0].Cells[7].Text = getmilk.ToString("F2");
            Session["Close"] = getmilk.ToString("F2");

            GridView1.Rows[0].Cells[8].Text = fat.ToString("F2");
            GridView1.Rows[0].Cells[9].Text = Snf.ToString("F2");
            Session["fat"] = fat.ToString("F2");
            Session["snf"] = Snf.ToString("F2");

        if ((ds.Tables[1].Rows.Count > 0) && (ds.Tables[2].Rows.Count > 0))
        {

            Button7.Visible = true;

        }
        else
        {

            Button7.Visible = false;

        }


        }

        catch (Exception ex)
        {
            //  getcatmsg();

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
            Button7.Visible = false;
        }
    }
    public void getmobile()
    {

        try
        {
            con = DB.GetConnection();
            string getphone;
            getphone = "Select *   from PlantStockSms   where plant_code='" + GET[0] + "'  ";
            SqlCommand cmd=new SqlCommand(getphone,con);
            SqlDataAdapter moblie = new SqlDataAdapter(cmd);
            moblie.Fill(datamobile);
        }
        catch(Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }

    }
    public void getDate()
    {

        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            string getphone;
            getphone = "Select *   from PlantStockSmsDate   where plant_code='" + GET[0] + "' and Date='" + d1 + "'  ";
            SqlCommand cmd = new SqlCommand(getphone, con);
            SqlDataAdapter moblie = new SqlDataAdapter(cmd);
            moblie.Fill(datamDate);
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }

    }
    public void getcatmsg()
    {

     //   Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        getmobile();
        getDate();

        if (datamDate.Rows.Count < 1)
        {
            foreach (DataRow dr in datamobile.Rows)
            {
                Session["plantname"] = GET[1].ToString();
                Session["mobile"] = dr[2].ToString();
                Session["Date"] = txt_FromDate.Text;
                msg = "\n VYSHNAVI- " + Session["plantname"].ToString() + "\nDate:" + Session["Date"].ToString() + "\nopeMilk:" + Session["op"].ToString() + "\nprocAmMilk:" + Session["am"].ToString() + "\nProcPmMilk:" + Session["pm"].ToString() + "\nTotProMilk:" + Session["tot"].ToString() + "\nDespMilk:" + Session["des"].ToString() + "\nClosMilk:" + Session["Close"].ToString() + "\nFat:" + Session["fat"].ToString() + "\nSnf:" + Session["Snf"].ToString() + " ";
                count = count + 1;
                string strUrl = "http://www.smsstriker.com/API/sms.php?username=vaishnavidairy&password=sulasha77&from=VYSNVI&to=" + Session["mobile"].ToString() + "&msg=" + msg + "&type=1 ";
                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
            }
            if (count == datamobile.Rows.Count)
            {
                string insert;
                DateTime dt1 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                string d1 = dt1.ToString("MM/dd/yyyy");
                insert = "Insert into  PlantStockSmsDate(plant_code,Date) values ('" + GET[0] + "', '" + d1 + "')";
                SqlCommand CMD = new SqlCommand(insert, con);
                CMD.ExecuteNonQuery();

                string ex="Message Send SuccessFuly";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex) + "')</script>");
            }

        }

    }
}