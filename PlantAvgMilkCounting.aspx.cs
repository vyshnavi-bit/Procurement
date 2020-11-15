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

public partial class PlantAvgMilkCounting : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet dt = new DataSet();
    DataSet dtp = new DataSet();
    int i = 0;
    int j = 0;
    string getplant;
    DataTable DFF = new DataTable();
    DataTable df = new DataTable();
    DataTable dts = new DataTable();
    string getrangefrom;
    string frmrange;
    string Torange;
    string frmrange1;
    string Torange1;
    string getskg;
    string sampNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {


                dtm = System.DateTime.Now;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                roleid = Convert.ToInt32(Session["Role"].ToString());

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    //loadsingleplant();

                }

                else
                {

                    //LoadPlantcode();
                }
                GridView2.Visible = false;
                GridView3.Visible = false;
            }
            else
            {



            }

        }


        else
        {
            ccode = Session["Company_code"].ToString();
          
            GridView3.Visible = false;

        }
    }



    public void getgrid()
    {

        try
        {
            DateTime dt2 = new DateTime();
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt2.ToString("MM/dd/yyyy");

            con = DB.GetConnection();

            string stt = "SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE PLANT_CODE NOT IN (139,150,160) order by Plant_Code ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);

            DA.Fill(df);
            DFF.Columns.Add("PlantCode");
            DFF.Columns.Add("PlantName");
            DFF.Columns.Add("TotMilkkg");
            DFF.Columns.Add("Totsample");
            DFF.Columns.Add("0-5");
            DFF.Columns.Add("5-10");
            DFF.Columns.Add("10-15");
            DFF.Columns.Add("15-20");
            DFF.Columns.Add("20-30");
            DFF.Columns.Add("30-50");
            DFF.Columns.Add("50-75");
            DFF.Columns.Add("75-100");
            DFF.Columns.Add("100-200");
            DFF.Columns.Add("200-300");
            DFF.Columns.Add("300-400");
            DFF.Columns.Add("400-500");
            DFF.Columns.Add("500-1000");
            foreach (DataRow dsg in df.Rows)
            {

                string pcode = dsg[0].ToString();
                string pname = dsg[1].ToString();




                DFF.Rows.Add(pcode, pname);
            }

            GridView1.DataSource = DFF;
            GridView1.DataBind();

        }
        catch
        {


        }
    }


    public void LoadGridweigher()
    {

        DateTime dt2 = new DateTime();
        dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        string d1 = dt2.ToString("MM/dd/yyyy");



        try
        {
            string stt = "Select   sUM(Milk_kg) as  TotMilkkg   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'  GROUP by Plant_Code ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            //dtp.Tables[0].Rows.Clear();
            DA.Fill(dtp, ("TotMilkkg"));
            string getwei = dtp.Tables[0].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[3].Text = getwei.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[3].Text = "0";

        }



        try
        {
            string stt = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'  GROUP by Plant_Code ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dtp, ("Totsample"));
            string getwei = dtp.Tables[1].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[4].Text = getwei.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[4].Text = "0";

        }


        try
        {
            string stt = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 0  AND Milk_kg < 5) GROUP by Plant_Code ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dtp, ("0-5"));
            string getwei = dtp.Tables[2].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[5].Text = getwei.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[5].Text = "0";

        }

        try
        {
            string stt1 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 5  AND Milk_kg < 10) GROUP by Plant_Code ";
            SqlCommand cmd1 = new SqlCommand(stt1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            DA1.Fill(dtp, ("5-10"));
            string getwei1 = dtp.Tables[3].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[6].Text = getwei1.ToString();
        }
        catch
        {

            GridView1.Rows[j].Cells[6].Text = "0";
        }
        try
        {

            string stt2 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 10  AND Milk_kg < 15) GROUP by Plant_Code ";
            SqlCommand cmd2 = new SqlCommand(stt2, con);
            SqlDataAdapter DA2 = new SqlDataAdapter(cmd2);
            DA2.Fill(dtp, ("10-15"));
            string getwei2 = dtp.Tables[4].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[7].Text = getwei2.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[7].Text = "0";

        }

        try
        {
            string stt4 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 15  AND Milk_kg < 20) GROUP by Plant_Code ";
            SqlCommand cmd4 = new SqlCommand(stt4, con);
            SqlDataAdapter DA4 = new SqlDataAdapter(cmd4);
            DA4.Fill(dtp, ("15-20"));
            string getwei4 = dtp.Tables[5].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[8].Text = getwei4.ToString();
        }
        catch
        {

            GridView1.Rows[j].Cells[8].Text = "0";

        }

        try
        {
            string stt5 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 20  AND Milk_kg < 30) GROUP by Plant_Code ";
            SqlCommand cmd5 = new SqlCommand(stt5, con);
            SqlDataAdapter DA5 = new SqlDataAdapter(cmd5);
            DA5.Fill(dtp, ("20-30"));
            string getwei5 = dtp.Tables[6].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[9].Text = getwei5.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[9].Text = "0";


        }

        try
        {
            string stt6 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 30  AND Milk_kg < 50) GROUP by Plant_Code ";
            SqlCommand cmd6 = new SqlCommand(stt6, con);
            SqlDataAdapter DA6 = new SqlDataAdapter(cmd6);
            DA6.Fill(dtp, ("30-40"));
            string getwei6 = dtp.Tables[7].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[10].Text = getwei6.ToString();

        }
        catch
        {

            GridView1.Rows[j].Cells[10].Text = "0";

        }



        try
        {
            string stt8 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 50  AND Milk_kg < 75) GROUP by Plant_Code ";
            SqlCommand cmd8 = new SqlCommand(stt8, con);
            SqlDataAdapter DA8 = new SqlDataAdapter(cmd8);
            DA8.Fill(dtp, ("50-75"));
            string getwei8 = dtp.Tables[8].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[11].Text = getwei8.ToString();

        }
        catch
        {
            GridView1.Rows[j].Cells[11].Text = "0";
        }

        try
        {

            string stt9 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 75  AND Milk_kg < 100) GROUP by Plant_Code ";
            SqlCommand cmd9 = new SqlCommand(stt9, con);
            SqlDataAdapter DA9 = new SqlDataAdapter(cmd9);
            DA9.Fill(dtp, ("75-100"));
            string getwei9 = dtp.Tables[9].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[12].Text = getwei9.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[12].Text = "0";


        }

        try
        {

            string stt10 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 100  AND Milk_kg < 200) GROUP by Plant_Code ";
            SqlCommand cmd10 = new SqlCommand(stt10, con);
            SqlDataAdapter DA10 = new SqlDataAdapter(cmd10);
            DA10.Fill(dtp, ("100-200"));
            string getwei10 = dtp.Tables[10].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[13].Text = getwei10.ToString();
        }
        catch
        {

            GridView1.Rows[j].Cells[13].Text = "0";

        }
        try
        {

            string stt11 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 200  AND Milk_kg < 300) GROUP by Plant_Code ";
            SqlCommand cmd11 = new SqlCommand(stt11, con);
            SqlDataAdapter DA11 = new SqlDataAdapter(cmd11);
            DA11.Fill(dtp, ("200-300"));
            string getwei11 = dtp.Tables[11].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[14].Text = getwei11.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[14].Text = "0";

        }

        try
        {

            string stt12 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 300  AND Milk_kg < 400) GROUP by Plant_Code ";
            SqlCommand cmd12 = new SqlCommand(stt12, con);
            SqlDataAdapter DA12 = new SqlDataAdapter(cmd12);
            DA12.Fill(dtp, ("300-400"));
            string getwei12 = dtp.Tables[12].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[15].Text = getwei12.ToString();
        }
        catch
        {

            GridView1.Rows[j].Cells[15].Text = "0";

        }
        try
        {
            string stt13 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 400  AND Milk_kg < 500) GROUP by Plant_Code ";
            SqlCommand cmd13 = new SqlCommand(stt13, con);
            SqlDataAdapter DA13 = new SqlDataAdapter(cmd13);
            DA13.Fill(dtp, ("400-500"));
            string getwei13 = dtp.Tables[13].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[16].Text = getwei13.ToString();
        }

        catch
        {

            GridView1.Rows[j].Cells[16].Text = "0";

        }
        try
        {
            string stt14 = "Select COUNT( *) No   from Procurementimport  where Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' AND Plant_Code='" + ViewState["getplant"] + "'   AND( Milk_kg >= 500  AND Milk_kg < 1000) GROUP by Plant_Code ";
            SqlCommand cmd14 = new SqlCommand(stt14, con);
            SqlDataAdapter DA14 = new SqlDataAdapter(cmd14);
            DA14.Fill(dtp, ("500-1000"));
            string getwei14 = dtp.Tables[14].Rows[0][0].ToString();
            GridView1.Rows[j].Cells[17].Text = getwei14.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[17].Text = "0";


        }
        dtp.Tables[0].Rows.Clear();
        dtp.Tables[1].Rows.Clear();
        dtp.Tables[2].Rows.Clear();
        dtp.Tables[3].Rows.Clear();
        dtp.Tables[4].Rows.Clear();
        dtp.Tables[5].Rows.Clear();
        dtp.Tables[6].Rows.Clear();
        dtp.Tables[7].Rows.Clear();
        dtp.Tables[8].Rows.Clear();
        dtp.Tables[9].Rows.Clear();
        dtp.Tables[10].Rows.Clear();
        dtp.Tables[11].Rows.Clear();
        dtp.Tables[12].Rows.Clear();
        dtp.Tables[13].Rows.Clear();
        dtp.Tables[14].Rows.Clear();



    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        GridView2.Visible = false;
        GridView3.Visible = false;
        getgrid();

        foreach (GridViewRow ross in GridView1.Rows)
        {

            ViewState["getplant"] = GridView1.Rows[j].Cells[1].Text;
            LoadGridweigher();
            j = j + 1;

        }

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Plant Milk Avg Counting" + ":Date:" + txt_FromDate.Text + "Sessions:" + RadioButtonList1.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 19;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        ViewState["GETPLANTCODE"] = GridView1.SelectedRow.Cells[1].Text;
        ViewState["GETPLANT"] = GridView1.SelectedRow.Cells[2].Text;
        //String getquery = "";
        //con = DB.GetConnection();
        //SqlCommand CMD = new SqlCommand(getquery, con);
        //SqlDataAdapter DRF = new SqlDataAdapter(CMD);
     
        dts.Columns.Add("Milkkg");
        dts.Columns.Add("MilkRange");
        dts.Columns.Add("TotSample");
        dts.Rows.Add("", "0-5");
        dts.Rows.Add("", "5-10");
        dts.Rows.Add("", "10-15");
        dts.Rows.Add("", "15-20");
        dts.Rows.Add("", "20-30");
        dts.Rows.Add("", "30-50");
        dts.Rows.Add("", "50-75");
        dts.Rows.Add("", "75-100");
        dts.Rows.Add("", "100-200");
        dts.Rows.Add("", "200-300");
        dts.Rows.Add("", "300-400");
        dts.Rows.Add("", "400-500");
        dts.Rows.Add("", "500-1000");

        GridView2.DataSource = dts;
        GridView2.DataBind();

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          string getrangefrom =  e.Row.Cells[2].Text;
          string[] spil=getrangefrom.Split('-');
            frmrange = spil[0];
            Torange =  spil[1];
            DateTime dt2 = new DateTime();
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt2.ToString("MM/dd/yyyy");
            string strk = "";
            strk = "Select Sum(milk_kg) as  milk_kg,COUNT( *) No       from Procurementimport  where   plant_code='" + ViewState["GETPLANTCODE"] + "' and  Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' and milk_kg between '" + frmrange + "' and '" + Torange + "'";
            con = DB.GetConnection();
            SqlCommand cmd=new SqlCommand(strk,con);
            SqlDataAdapter dsr = new SqlDataAdapter(cmd);
            DataTable getkgsample = new DataTable();
            dsr.Fill(getkgsample);
            

            if (getkgsample.Rows.Count > 0)
            {
                GridView2.Visible = true;
              getskg  =     getkgsample.Rows[0][0].ToString();
              if (getskg == "")
              {
                  getskg = "0";

              }
              sampNo  =     getkgsample.Rows[0][1].ToString();

            }

            e.Row.Cells[1].Text = getskg.ToString();
            e.Row.Cells[3].Text = sampNo.ToString();
        }


    }

    public void getsampledetails()
    {
        
             
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Plant Milk Avg Counting For" + ViewState["GETPLANT"] + ":Date:" + txt_FromDate.Text + "Sessions:" + RadioButtonList1.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 5;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
          DateTime dt2 = new DateTime();
          dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
          string d1 = dt2.ToString("MM/dd/yyyy");
          string gettingrange =  GridView2.SelectedRow.Cells[2].Text;
          string[] spilt = gettingrange.Split('-');
          frmrange1 = spilt[0];
          Torange1 = spilt[1];
          DataTable dtr = new DataTable();
          //dtr.Columns.Add("Agnetid");
          //dtr.Columns.Add("Milk_Kg");
          //dtr.Columns.Add("Fat");
          //dtr.Columns.Add("Snf");

          string gets = "Select  Agent_id,milk_kg,Fat,Snf    from procurementimport   where     plant_code='" + ViewState["GETPLANTCODE"] + "' and  Prdate='" + d1 + "' and  Sessions='" + RadioButtonList1.SelectedItem.Text + "' and milk_kg between '" + frmrange1 + "' and '" + Torange1 + "'";
          con = DB.GetConnection();
          SqlCommand cmd = new SqlCommand(gets, con);
          SqlDataAdapter gft = new SqlDataAdapter(cmd);
          gft.Fill(dtr);
          if (dtr.Rows.Count > 0)
          {
              GridView3.DataSource = dtr;
              GridView3.DataBind();
              GridView3.Visible = true;
          }
          else
          {
              GridView3.DataSource = null;
              GridView3.DataBind();
          }
          


    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Plant Milk Avg Counting For" + ViewState["GETPLANT"] + ":Date:" + txt_FromDate.Text + "Sessions:" + RadioButtonList1.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 5;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView3.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string getid = e.Row.Cells[0].Text;
        //    string kg = e.Row.Cells[1].Text;
        //    string fat = e.Row.Cells[2].Text;
        //    string snf = e.Row.Cells[3].Text;

        //}
    }
}
