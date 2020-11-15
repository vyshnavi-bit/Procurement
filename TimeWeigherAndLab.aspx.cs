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
public partial class TimeWeigherAndLab : System.Web.UI.Page
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
    DataSet DTGGG = new DataSet();
    DataSet DTG = new DataSet();
    string stt;
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
                    loadsingleplant();

                }

                if ((roleid >= 3) && (roleid != 9))
                {

                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                     Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();

                }

            }
            else
            {



            }

        }


        else
        {
            ccode = Session["Company_code"].ToString();
            //pcode = ddl_Plantname.SelectedItem.Value;
            //pname = ddl_Plantname.SelectedItem.Text;
            pcode = ddl_Plantname.SelectedItem.Value;

            //   pname = ddl_Plantname.SelectedItem.Text;


        }
    }

    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }


    public void loadsingleplant()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139) and plant_code  in (" + pcode + ") ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }


    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = DB.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            DataTable getdata = new DataTable();
            SqlDataAdapter dsii = new SqlDataAdapter(cmd);
            getdata.Rows.Clear();
            dsii.Fill(getdata);
            ddl_Plantname.DataSource = getdata;
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
            ddl_Plantname.DataBind();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            getgrid();

            foreach (GridViewRow ross in GridView1.Rows)
            {


                ViewState["getplant"] = GridView1.Rows[j].Cells[1].Text;

                LoadGridweigher();
                LoadGridweigherending();
                LoadGridLab();
                LoadGridLabend();
                j = j + 1;

            }
        }
        catch
        {

        }
    }


    //public void getplant()
    //{
    //    DataTable dtj = new DataTable();
    //    dtj.Columns.Add("plantName");
    //    foreach (DataRow drr in dt.Tables[0].Rows)
    //    {
    //        string getid = dt.Tables[0].Rows[i][1].ToString();
    //        dtj.Rows.Add(getid);
    //        i = i + 1;
    //    }

    //    GridView1.DataSource = dtj;
    //    GridView1.DataBind();
    //}

    public void getgrid()
    {

        try
        {
            DateTime dt2 = new DateTime();
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt2.ToString("MM/dd/yyyy");

            con = DB.GetConnection();
          
            if (roleid < 3)
            {
                stt = "SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE PLANT_CODE NOT IN (139,150,160) and plant_code in ("+pcode+") order by Plant_Code ";
            }
            if ((roleid >= 3) && (roleid != 9))
            {
                stt = "SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE PLANT_CODE NOT IN (139,150,160) order by Plant_Code ";

            }
            if ((roleid == 9))
            {
                stt = "SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE PLANT_CODE   IN (170) order by Plant_Code ";
            }


            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);

            DA.Fill(df);
            DFF.Columns.Add("PlantCode");
            DFF.Columns.Add("PlantName");
            DFF.Columns.Add("Weigher StartingTime");
            DFF.Columns.Add("Weigher EndingTime");
            DFF.Columns.Add("Analyzer StartingTime");
            DFF.Columns.Add("Analyzer EndingTime");
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
        try
        {
            DateTime dt2 = new DateTime();
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt2.ToString("MM/dd/yyyy");
            string stt = "select  top 1 StartingTime   from lp where plantcode='" + ViewState["getplant"] + "' and Prdate='" + d1 + "' AND Sessions='" + RadioButtonList1.SelectedItem.Text + "' ORDER BY   rand(sampleno) asc";
            SqlCommand cmd1 = new SqlCommand(stt, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            DA1.Fill(dtp, ("StartingTime"));
            string getwei = dtp.Tables[0].Rows[0][0].ToString();

            DateTime StartingTime = Convert.ToDateTime(getwei);
            string GET = StartingTime.ToString("hh:mm");


            GridView1.Rows[j].Cells[3].Text = GET.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[3].Text = "";

        }

        dtp.Tables[0].Rows.Clear();
    }


    public void LoadGridweigherending()
    {
        try
        {
            DateTime dt2 = new DateTime();
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt2.ToString("MM/dd/yyyy");
            string stt = "select  top 1 StartingTime   from lp where plantcode='" + ViewState["getplant"] + "' and Prdate='" + d1 + "' AND Sessions='" + RadioButtonList1.SelectedItem.Text + "' ORDER BY   rand(sampleno) desc";
            SqlCommand cmd1 = new SqlCommand(stt, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            DA1.Fill(dtp, ("StartingTime"));
            string getwei = dtp.Tables[0].Rows[0][0].ToString();

            DateTime StartingTime = Convert.ToDateTime(getwei);
            string GET = StartingTime.ToString("hh:mm");

            GridView1.Rows[j].Cells[4].Text = GET.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[4].Text = "";

        }

        dtp.Tables[0].Rows.Clear();
    }

    public void LoadGridLab()
    {
        try
        {

            DateTime dt2 = new DateTime();
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt2.ToString("MM/dd/yyyy");

            string stt = "  select  top 1 EndingTime   from lp where plantcode='" + ViewState["getplant"] + "' and Prdate='" + d1 + "' AND Sessions='" + RadioButtonList1.SelectedItem.Text + "' ORDER BY   rand(sampleno) asc";
            SqlCommand cmd2 = new SqlCommand(stt, con);
            SqlDataAdapter DA2 = new SqlDataAdapter(cmd2);
            DA2.Fill(dtp, ("LabCaptureTime"));

            string getweii = dtp.Tables[1].Rows[0][0].ToString();


            DateTime StartingTime = Convert.ToDateTime(getweii);
            string GET = StartingTime.ToString("hh:mm");



            GridView1.Rows[j].Cells[5].Text = GET.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[5].Text = "";


        }

        dtp.Tables[1].Rows.Clear();
    }

    public void LoadGridLabend()
    {
        try
        {

            DateTime dt2 = new DateTime();
            dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt2.ToString("MM/dd/yyyy");

            string stt = "  select  top 1 EndingTime   from lp where plantcode='" + ViewState["getplant"] + "' and Prdate='" + d1 + "' AND Sessions='" + RadioButtonList1.SelectedItem.Text + "' ORDER BY   rand(sampleno) desc";
            SqlCommand cmd2 = new SqlCommand(stt, con);
            SqlDataAdapter DA2 = new SqlDataAdapter(cmd2);
            DA2.Fill(dtp, ("LabCaptureTime"));

            string getweii = dtp.Tables[1].Rows[0][0].ToString();

            DateTime StartingTime = Convert.ToDateTime(getweii);
            string GET = StartingTime.ToString("hh:mm");

            GridView1.Rows[j].Cells[6].Text = GET.ToString();
        }
        catch
        {
            GridView1.Rows[j].Cells[6].Text = "";


        }

        dtp.Tables[1].Rows.Clear();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "CC WEIGHING AMD LAB STARING AND ENDING TIME" + ":Date:" + txt_FromDate.Text + "Sessions:" + RadioButtonList1.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 5;
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
    protected void GridView1_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "CC WEIGHING AND LAB STARTING AND ENDING TIME" + ":Date:" + txt_FromDate.Text + "Sessions:" + RadioButtonList1.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 7;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

    }
}