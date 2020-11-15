using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Sapconsolidated : System.Web.UI.Page
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
    SqlConnection con1 = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable allcol = new DataTable();
    DataTable sapmrn = new DataTable();
    DataTable showplant = new DataTable();
    DataTable Dates = new DataTable();
    DataTable charc = new DataTable();
    DataTable snoo = new DataTable();
    DataTable availornot = new DataTable();
    DataTable missingagent = new DataTable();
    DateTime d1;
    DateTime d2;
    DateTime d11;
    DateTime d22;
    string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    int colcount=0;
    int i=0;
    string getprocdata;
    string getprocsess;


    string grnapp;
    string sapGRNpost;
    string grninvoice;
    string grnSAPinvoice;
    



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    dtm = System.DateTime.Now;
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_todate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    LoadPlantcode();
                 
                    //  billdate();

                }
                else
                {

                   


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

               
            }
        }
        catch
        {


        }
    }

    public void Load1Plantcode()
    
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Name as Date FROM Plant_Master where plant_code not in (150,139,160)  AND PLANT_CODE='"+ddl_Plantname.SelectedItem.Value+"'";
            SqlCommand cmd = new SqlCommand(stt, con);
            showplant.Rows.Clear();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(showplant);
            int getcol = showplant.Rows.Count;
            foreach (DataRow row in showplant.Rows)
            {

                if (colcount == 0)
                {
                    allcol.Columns.Add("Date");
                    allcol.Columns.Add("shift");
                }

                string name = row[0].ToString();
                allcol.Columns.Add("ProcData");
                allcol.Columns.Add("FormatData");
                allcol.Columns.Add("GrnData");
                allcol.Columns.Add("SapPosted");
                allcol.Columns.Add("InvoiceData");
                allcol.Columns.Add("grnSAPinvoice");
                colcount++;
            }

            string date = txt_FromDate.Text;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            //getvaldd = p[3];
            //getvalmm = p[4];
            //getvalyy = p[5];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
           

            string date1 = txt_todate.Text;
            string[] p1 = date1.Split('/', '-');
            //getvald = p[0];
            //getvalm = p[1];
            //getvaly = p[2];
            getvaldd = p1[0];
            getvalmm = p1[1];
            getvalyy = p1[2];
           // FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            con = DB.GetConnection();
            string sttrow = "Select  convert(varchar,prdate,103) as Date,sessions as shift  from  procurement    where prdate between '" + FDATE + "'  and '" + TODATE + "'  group by  prdate,sessions  order by    convert(datetime,prdate,101),sessions asc";
            SqlCommand cmdrow = new SqlCommand(sttrow, con);
            Dates.Rows.Clear();
            SqlDataAdapter DAda = new SqlDataAdapter(cmdrow);
            DAda.Fill(Dates);
            foreach (DataRow readd in Dates.Rows)
            {

                allcol.Rows.Add(readd[0].ToString(), readd[1].ToString());
            }


            GridView1.DataSource = allcol;
            GridView1.DataBind();
        }


        catch
        {


        }

    }
    public void columnadd()
    {

 


    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        Load1Plantcode();
    }

    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160) ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG);
            ddl_Plantname.DataSource = DTG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {


        }

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "Sap consolidated Reports   For plant:" + ddl_Plantname.SelectedItem.Text + "Bill  Form:" + txt_FromDate.Text + "To:" + txt_todate.Text ;
                HeaderCell2.ColumnSpan = 8;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            }
        }
        catch
        {


        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string splitdate = e.Row.Cells[0].Text;
            string sessions = e.Row.Cells[1].Text;
            string[] p = splitdate.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            

            con = DB.GetConnection();
            string sttrow = "sELECT PROCTID,FORTID  FROM ( sELECT COUNT(Tid) AS PROCTID,Plant_Code as FORplantcode FROM  PROCUREMENT  WHERE Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   AND Prdate='" + FDATE + "' and Sessions='" + sessions.Trim() + "'    group by Plant_Code) AS PROCU LEFT JOIN (sELECT COUNT(Tid) AS FORTID,Plant_Code asFORplantcode FROM saptransaction  WHERE       Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'   AND Createdate='" + FDATE + "' and Sessions='" + sessions.Trim() + "'     group by Plant_Code)  AS SAPTRNA ON PROCU.FORplantcode=SAPTRNA.asFORplantcode";
            SqlCommand cmdrow = new SqlCommand(sttrow, con);
            DataTable prData=new DataTable();
            prData.Rows.Clear();
            SqlDataAdapter DAda = new SqlDataAdapter(cmdrow);
            DAda.Fill(prData);
            
            try
            {
                 getprocdata = prData.Rows[0][0].ToString();
            }
            catch
            {
                 getprocdata ="";

            }
             try
            {
                 getprocsess = prData.Rows[0][1].ToString();
            }
            catch
            {
                getprocsess = "";

            }
             con1 = DB.GetConnectionSAP();
          //   string sapps = "Select grnsno,invoicesno,sapup  from (select *  from (sELECT     count(U_Status) as grnsno,U_Status as grnplantcode   FROM EMROPDN   where U_Status='"+ddl_Plantname.SelectedItem.Value+"' and CreateDate='" + FDATE + "' and SESSION='" + sessions + "'  group by  U_Status) as opden  left join (sELECT     count(U_Status) as invoicesno,U_Status as invoiceplantcode   FROM  EMROPCH  where U_Status='" + ddl_Plantname.SelectedItem.Value + "' and CreateDate='" + FDATE + "' and SESSION='" + sessions.Trim() + "'  group by  U_Status) as  opch  on opden.grnplantcode=opch.invoiceplantcode) as opdnopch left join (sELECT     count(U_Status) as sapup,U_Status as sapupplantcode   FROM EMROPDN   where U_Status='" + ddl_Plantname.SelectedItem.Value + "' and CreateDate='" + FDATE + "' and SESSION='" + sessions.Trim() + "' AND B1Upload='Y' and Processed='Y'    group by  U_Status) as sap on opdnopch.grnplantcode=sap.sapupplantcode";
             string sapps = "Select grnsno,sapup,invoicesno,sapinvoicesno  from (Select *  from (select *  from (sELECT     count(U_Status) as grnsno,U_Status as grnplantcode   FROM EMROPDN   where U_Status='" + ddl_Plantname.SelectedItem.Value + "' and CreateDate='" + FDATE + "' and SESSION='" + sessions.Trim() + "'  group by  U_Status) as opden  left join (sELECT     count(U_Status) as invoicesno,U_Status as invoiceplantcode   FROM  EMROPCH  where U_Status='" + ddl_Plantname.SelectedItem.Value + "' and CreateDate='" + FDATE + "' and SESSION='" + sessions.Trim() + "'  group by  U_Status) as  opch  on opden.grnplantcode=opch.invoiceplantcode) as opdnopch left join (sELECT     count(U_Status) as sapup,U_Status as sapupplantcode   FROM EMROPDN   where U_Status='" + ddl_Plantname.SelectedItem.Value + "' and CreateDate='" + FDATE + "' and SESSION='" + sessions.Trim() + "' AND B1Upload='Y' and Processed='Y'    group by  U_Status) as sap on opdnopch.grnplantcode=sap.sapupplantcode ) as fg  left join (sELECT     count(U_Status) as sapinvoicesno,U_Status as sapinvoiceplantcode   FROM  EMROPCH  where U_Status='" + ddl_Plantname.SelectedItem.Value + "' and CreateDate='" + FDATE + "' and SESSION='" + sessions.Trim() + "'   AND B1Upload='Y' and Processed='Y'    group by  U_Status) as  rifg on   fg.grnplantcode=rifg.sapinvoiceplantcode";
             SqlCommand cmdrowsap = new SqlCommand(sapps, con1);
             DataTable sapData = new DataTable();
             sapData.Rows.Clear();
             SqlDataAdapter DAdasap = new SqlDataAdapter(cmdrowsap);
             DAdasap.Fill(sapData);
            
             try
             {
                 grnapp = sapData.Rows[0][0].ToString();
             }
             catch
             {
                 grnapp = "";

             }


             try
             {
                 sapGRNpost = sapData.Rows[0][1].ToString();
             }
             catch
             {
                 sapGRNpost = "";

             }

             try
             {
                 grninvoice = sapData.Rows[0][2].ToString();
             }
             catch
             {
                 grninvoice = "";

             }

             try
             {
                 grnSAPinvoice = sapData.Rows[0][3].ToString();
             }
             catch
             {
                 grnSAPinvoice = "";

             }


            

            Label myLabel = (Label)e.Row.FindControl("ProcData");
            string text = myLabel.Text;
            Image myImage = (Image)e.Row.FindControl("ProcDataimg");

        

            if (getprocdata != string.Empty)
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/TICK.jpg";
            }
            else
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/untick.jpg";

            }
            //if (text == null || text.Length < 1)
            //{

            //    myImage.Visible = true;

            //}

        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label myLabel = (Label)e.Row.FindControl("FormatData");

            string text = myLabel.Text;

            Image myImage = (Image)e.Row.FindControl("FormatDataimg");

            //myImage.ImageUrl = "Image/TICK.jpg";

            //if (text == null || text.Length < 1)
            //{

            //    myImage.Visible = true;

            //}


            if (getprocsess != string.Empty)
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/TICK.jpg";
            }
            else
            {
                myImage.ImageUrl = "Image/untick.jpg";
                myImage.Visible = true;
            }


        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label myLabel = (Label)e.Row.FindControl("GrnData");

            string text = myLabel.Text;

            Image myImage = (Image)e.Row.FindControl("GrnDataimg");

            //myImage.ImageUrl = "Image/TICK.jpg";

            //if (text == null || text.Length < 1)
            //{

            //    myImage.Visible = true;

            //}

            if (grnapp != string.Empty)
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/TICK.jpg";
            }
            else
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/untick.jpg";

            }

        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label myLabel = (Label)e.Row.FindControl("SapPosted");
            string text = myLabel.Text;
            Image myImage = (Image)e.Row.FindControl("SapPostedimg");
            myImage.ImageUrl = "Image/TICK.jpg";
            if (sapGRNpost != string.Empty)
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/TICK.jpg";
            }
            else
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/untick.jpg";

            }
            //if (text == null || text.Length < 1)
            //{

            //    myImage.Visible = true;

            //}

        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label myLabel = (Label)e.Row.FindControl("InvoiceData");

            string text = myLabel.Text;

            Image myImage = (Image)e.Row.FindControl("InvoiceDataimg");

            //myImage.ImageUrl = "Image/TICK.jpg";

            //if (text == null || text.Length < 1)
            //{

            //    myImage.Visible = true;

            //}

            if (grninvoice != string.Empty)
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/TICK.jpg";
            }
            else
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/untick.jpg";

            }

        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label myLabel = (Label)e.Row.FindControl("grnSAPinvoice");
            string text = myLabel.Text;
            Image myImage = (Image)e.Row.FindControl("grnSAPinvoiceimg");
            myImage.ImageUrl = "Image/TICK.jpg";
            if (grnSAPinvoice != string.Empty)
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/TICK.jpg";
            }
            else
            {
                myImage.Visible = true;
                myImage.ImageUrl = "Image/untick.jpg";

            }
            //if (text == null || text.Length < 1)
            //{

            //    myImage.Visible = true;

            //}

        }


    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}