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
using System.Text;

public partial class PlantLineGraph : System.Web.UI.Page
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
    DataSet DTGGG = new DataSet();
    StringBuilder strP = new StringBuilder();
    DataTable DTGchart = new DataTable();
    int h = 0;
    int j = 1;

    int jG = 1;
    int jhp = 0;

    int plantc = 1;
    double arani;
    double sumarani = 0;
    string stt;
    double GETSUM = 0;
    double getcow=0;
    double getbuffalo = 0;
    double tottalSUM = 0;
    double tottalSUM1 = 0;
    double ssumarani;
    double ssumkaveri;
    double ssumgud;
    double ssumwala;
    double ssumvkota;
    double ssumrc;
    double ssumbomm;
    double ssumtari;
    double ssumkal;
    double ssumcs;
    double ssumkon;
    double ssumkava;
    double ssumgudi;
    double ssumkali;
    
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
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;

                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");




                    Label1.Visible=false;
                    Label2.Visible=false; 
                    Label3.Visible=false; 
                    Label4.Visible=false;
                    Label5.Visible=false;
                    Label6.Visible=false;
                    Label7.Visible=false; 
                    Label8.Visible=false;
                    Label9.Visible=false; 
                    Label10.Visible=false;
                    Label11.Visible=false;
                    Label12.Visible=false;
                    Label13.Visible=false;
                    Label14.Visible= false;
                    Label15.Visible = false;
                    Label16.Visible = false;
                    Label17.Visible = false;

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
    protected void Button5_Click(object sender, EventArgs e)
    {

       chart_bind();


       Label1.Visible = true;
       Label2.Visible = true;
       Label3.Visible = true;
       Label4.Visible = true;
       Label5.Visible = true;
       Label6.Visible = true;
       Label7.Visible = true;
       Label8.Visible = true;
       Label9.Visible = true;
       Label10.Visible = true;
       Label11.Visible = true;
       Label12.Visible = true;
       Label13.Visible = true;
       Label14.Visible = true;

       Label15.Visible = true;
       Label16.Visible = true;
       Label17.Visible = true;
    }



    private void chart_bind()
    {      
        DataTable dt = new DataTable();
        try
        {
            GETPLANTCODE();
            GETDATA();

              DTGchart.Columns.Add("MONTH");
              DTGchart.Columns.Add("ARANI");
              DTGchart.Columns.Add("KAVERI");
              DTGchart.Columns.Add("GUDLUR");
              DTGchart.Columns.Add("WALAJA");
              DTGchart.Columns.Add("V_KOTA");
              DTGchart.Columns.Add("RCPURAM");
              DTGchart.Columns.Add("BOMMA");
              DTGchart.Columns.Add("TARIGONDA");
              DTGchart.Columns.Add("KALASTHIRI");
              DTGchart.Columns.Add("CSPURAM");
              DTGchart.Columns.Add("KONDEPI");
              DTGchart.Columns.Add("KAVALI");
              DTGchart.Columns.Add("GUDIPALLIPADU");
              DTGchart.Columns.Add("KALIGIRI");


              foreach (DataRow DRPLANT in DTGGG.Tables[1].Rows)
              {


                 // ViewState["getvaluee"] = DRPLANT[0].ToString();
    
                  string STT="";

                      DTGchart.Rows.Add(STT,STT,STT,STT,STT,STT,STT,STT,STT,STT,STT,STT,STT,STT,STT);

                  
                  
                  h = h + 1;
                  j = j + 1;


              }

              GridView1.DataSource = DTGchart;
              GridView1.DataBind();
              h = 0;
              foreach (DataRow DD in DTGGG.Tables[1].Rows)
              {

                  GridView1.Rows[h].Cells[0].Text = DD[0].ToString();
              h = h + 1 ;

              }


              foreach (DataRow DD in DTGGG.Tables[0].Rows)
              {

                 ViewState["GETTT"] = DD[0].ToString();

                 h = 0;
                 for (int KJ = 0; KJ < DTGGG.Tables[1].Rows.Count; KJ++)
                 {

                     GETDATA1();

                     GridView1.Rows[KJ].Cells[jG].Text = DTGGG.Tables[2].Rows[h][1].ToString();
                     DTGGG.Tables[2].Rows.Clear();

                     if (plantc == 1)
                     {

                         arani = Convert.ToDouble(GridView1.Rows[KJ].Cells[jG].Text);
                         sumarani = sumarani + arani;
                     }


                     h = h + 1;
                     plantc = plantc + 1;
                 }

                 plantc = 1;

                jG =jG +  1;


              }

            DTGchart.Rows.Clear();
              foreach (GridViewRow GRVIEW in GridView1.Rows)
              {
                   string MONTH= GRVIEW.Cells[0].Text;
                    string ST= GRVIEW.Cells[1].Text;
                    string ST1= GRVIEW.Cells[2].Text;
                    string ST2= GRVIEW.Cells[3].Text;
                    string ST3= GRVIEW.Cells[4].Text;
                    string ST4= GRVIEW.Cells[5].Text;
                    string ST5= GRVIEW.Cells[6].Text;
                    string ST6= GRVIEW.Cells[7].Text;
                    string ST7= GRVIEW.Cells[8].Text;
                    string ST8= GRVIEW.Cells[9].Text;
                    string ST9= GRVIEW.Cells[10].Text;
                    string ST10= GRVIEW.Cells[11].Text;

                   string ST11= GRVIEW.Cells[12].Text;
                    string ST12= GRVIEW.Cells[13].Text;
                    string ST13= GRVIEW.Cells[14].Text;
                   


                    DTGchart.Rows.Add(MONTH, ST, ST1, ST2, ST3, ST4, ST5, ST6, ST7, ST8, ST9, ST10, ST11, ST12, ST13);



                    double val1 = Convert.ToDouble( GRVIEW.Cells[1].Text);
                    double val2 = Convert.ToDouble(GRVIEW.Cells[2].Text);
                    double val3 = Convert.ToDouble(GRVIEW.Cells[3].Text);
                    double val4 = Convert.ToDouble(GRVIEW.Cells[4].Text);
                    double val5 = Convert.ToDouble(GRVIEW.Cells[5].Text);
                    double val6 = Convert.ToDouble(GRVIEW.Cells[6].Text);
                    double val7 = Convert.ToDouble(GRVIEW.Cells[7].Text);
                    double val8 = Convert.ToDouble(GRVIEW.Cells[8].Text);
                    double val9 = Convert.ToDouble(GRVIEW.Cells[9].Text);
                    double val10 = Convert.ToDouble(GRVIEW.Cells[10].Text);
                    double val11 = Convert.ToDouble(GRVIEW.Cells[11].Text);
                    double val12 = Convert.ToDouble(GRVIEW.Cells[12].Text);
                    double val13 = Convert.ToDouble(GRVIEW.Cells[13].Text);
                    double val14 = Convert.ToDouble(GRVIEW.Cells[14].Text);



                    if ((val1 > val2) && (val1 > val3) && (val1 > val4) && (val1 > val5) && (val1 > val6) && (val1 > val7) && (val1 > val8) && (val1 > val9) && (val1 > val10) && (val1 > val11) && (val1 > val12) && (val1 > val13) && (val1 > val14))
                    {

                        GridView1.Rows[jhp].Cells[1].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[1].Font.Bold = true;
                    }

                    if ((val2 > val1) && (val2 > val3) && (val2 > val4) && (val2 > val5) && (val2 > val6) && (val2 > val7) && (val2 > val8) && (val2 > val9) && (val2 > val10) && (val2 > val11) && (val2 > val12) && (val2 > val13) && (val2 > val14))
                    {

                        GridView1.Rows[jhp].Cells[2].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[2].Font.Bold = true;
                    }

                    if ((val3 > val1) && (val3 > val2) && (val3 > val4) && (val3 > val5) && (val3 > val6) && (val3 > val7) && (val3 > val8) && (val3 > val9) && (val3 > val10) && (val3 > val11) && (val3 > val12) && (val3 > val13) && (val3 > val14))
                    {

                        GridView1.Rows[jhp].Cells[3].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[3].Font.Bold = true;
                    }
                    if ((val4 > val1) && (val4 > val2) && (val4 > val3) && (val4 > val5) && (val4 > val6) && (val4 > val7) && (val4 > val8) && (val4 > val9) && (val4 > val10) && (val4 > val11) && (val4 > val12) && (val4 > val13) && (val4 > val14))
                    {

                        GridView1.Rows[jhp].Cells[4].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[4].Font.Bold = true;
                    }
                    if ((val5 > val1) && (val5 > val3) && (val5 > val4) && (val5 > val2) && (val5 > val6) && (val5 > val7) && (val5 > val8) && (val5 > val9) && (val5 > val10) && (val5 > val11) && (val5 > val12) && (val5 > val13) && (val5 > val14))
                    {

                        GridView1.Rows[jhp].Cells[5].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[5].Font.Bold = true;
                    }
                    if ((val6 > val1) && (val6 > val3) && (val6 > val4) && (val6 > val5) && (val6 > val2) && (val6 > val7) && (val6 > val8) && (val6 > val9) && (val6 > val10) && (val6 > val11) && (val6 > val12) && (val6 > val13) && (val6 > val14))
                    {

                        GridView1.Rows[jhp].Cells[6].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[6].Font.Bold = true;
                    }
                    if ((val7 > val1) && (val7 > val3) && (val7 > val4) && (val7 > val5) && (val7 > val6) && (val7 > val2) && (val2 > val8) && (val2 > val9) && (val2 > val10) && (val2 > val11) && (val2 > val12) && (val2 > val13) && (val2 > val14))
                    {

                        GridView1.Rows[jhp].Cells[7].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[7].Font.Bold = true;
                    }
                    if ((val8 > val1) && (val8 > val3) && (val8 > val4) && (val8 > val5) && (val8 > val6) && (val8 > val7) && (val8 > val2) && (val8 > val9) && (val8 > val10) && (val8 > val11) && (val8 > val12) && (val8 > val13) && (val8 > val14))
                    {

                        GridView1.Rows[jhp].Cells[8].ForeColor = System.Drawing.Color.LimeGreen;
                         GridView1.Rows[jhp].Cells[8].Font.Bold = true;
                    }
                    if ((val9 > val1) && (val9 > val3) && (val9 > val4) && (val9 > val5) && (val9 > val6) && (val9 > val7) && (val2 > val8) && (val9 > val2) && (val9 > val10) && (val9 > val11) && (val9 > val12) && (val9 > val13) && (val9 > val14))
                    {

                        GridView1.Rows[jhp].Cells[9].ForeColor = System.Drawing.Color.LimeGreen;
                      GridView1.Rows[jhp].Cells[9].Font.Bold = true;
                    }
                    if ((val10 > val1) && (val10 > val3) && (val10 > val4) && (val10 > val5) && (val10 > val6) && (val10 > val7) && (val10 > val8) && (val10 > val9) && (val10 > val2) && (val10 > val11) && (val10 > val12) && (val10 > val13) && (val10 > val14))
                    {

                        GridView1.Rows[jhp].Cells[10].ForeColor = System.Drawing.Color.LimeGreen;
                       GridView1.Rows[jhp].Cells[10].Font.Bold = true;

                    }
                    if ((val11 > val1) && (val11 > val3) && (val11 > val4) && (val11 > val5) && (val11 > val6) && (val11 > val7) && (val11 > val8) && (val11 > val9) && (val11 > val10) && (val11 > val2) && (val11 > val12) && (val11 > val13) && (val11 > val14))
                    {

                        GridView1.Rows[jhp].Cells[11].ForeColor = System.Drawing.Color.LimeGreen;
                     GridView1.Rows[jhp].Cells[11].Font.Bold = true;
                    }
                    if ((val12 > val1) && (val12 > val3) && (val12 > val4) && (val12 > val5) && (val12 > val6) && (val12 > val7) && (val12 > val8) && (val12 > val9) && (val12 > val10) && (val12 > val11) && (val12 > val2) && (val12 > val13) && (val12 > val14))
                    {

                        GridView1.Rows[jhp].Cells[12].ForeColor = System.Drawing.Color.LimeGreen;
                        GridView1.Rows[jhp].Cells[12].Font.Bold = true;
                    }
                    if ((val13 > val1) && (val13 > val3) && (val13 > val4) && (val13 > val5) && (val13 > val6) && (val13 > val7) && (val13 > val8) && (val13 > val9) && (val13 > val10) && (val13 > val11) && (val13 > val12) && (val13 > val2) && (val13 > val14))
                    {

                        GridView1.Rows[jhp].Cells[13].ForeColor = System.Drawing.Color.LimeGreen;
                     GridView1.Rows[jhp].Cells[13].Font.Bold = true;
                    }
                    if ((val14 > val1) && (val14 > val3) && (val14 > val4) && (val14 > val5) && (val14 > val6) && (val14 > val7) && (val14 > val8) && (val14 > val9) && (val14 > val10) && (val14 > val11) && (val14 > val12) && (val14 > val13) && (val14 > val2))
                    {

                        GridView1.Rows[jhp].Cells[14].ForeColor = System.Drawing.Color.LimeGreen;
                        GridView1.Rows[jhp].Cells[14].Font.Bold = true;
                    }




                    if ((val1 < val2) && (val1 < val3) && (val1 < val4) && (val1 < val5) && (val1 < val6) && (val1 < val7) && (val1 < val8) && (val1 < val9) && (val1 < val10) && (val1 < val11) && (val1 < val12) && (val1 < val13))
                    {

                        GridView1.Rows[jhp].Cells[1].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[1].Font.Bold = true;
                    }

                    if ((val2 < val1) && (val2 < val3) && (val2 < val4) && (val2 < val5) && (val2 < val6) && (val2 < val7) && (val2 < val8) && (val2 < val9) && (val2 < val10) && (val2 < val11) && (val2 < val12) && (val2 < val13))
                    {

                        GridView1.Rows[jhp].Cells[2].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[2].Font.Bold = true;
                    }

                    if ((val3 < val1) && (val3 < val2) && (val3 < val4) && (val3 < val5) && (val3 < val6) && (val3 < val7) && (val3 < val8) && (val3 < val9) && (val3 < val10) && (val3 < val11) && (val3 < val12) && (val3 < val13))
                    {

                        GridView1.Rows[jhp].Cells[3].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[3].Font.Bold = true;
                    }
                    if ((val4 < val1) && (val4 < val2) && (val4 < val3) && (val4 < val5) && (val4 < val6) && (val4 < val7) && (val4 < val8) && (val4 < val9) && (val4 < val10) && (val4 < val11) && (val4 < val12) && (val4 < val13) && (val4 < val14))
                    {

                        GridView1.Rows[jhp].Cells[4].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[4].Font.Bold = true;
                    }
                    if ((val5 < val1) && (val5 < val3) && (val5 < val4) && (val5 < val2) && (val5 < val6) && (val5 < val7) && (val5 < val8) && (val5 < val9) && (val5 < val10) && (val5 < val11) && (val5 < val12) && (val5 < val13) && (val5 < val14))
                    {

                        GridView1.Rows[jhp].Cells[5].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[5].Font.Bold = true;
                    }
                    if ((val6 < val1) && (val6 < val3) && (val6 < val4) && (val6 < val5) && (val6 < val2) && (val6 < val7) && (val6 < val8) && (val6 < val9) && (val6 < val10) && (val6 < val11) && (val6 < val12) && (val6 < val13) && (val6 < val14))
                    {

                        GridView1.Rows[jhp].Cells[6].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[6].Font.Bold = true;
                    }
                    if ((val7 < val1) && (val7 < val3) && (val7 < val4) && (val7 < val5) && (val7 < val6) && (val7 < val2) && (val2 < val8) && (val2 < val9) && (val2 < val10) && (val2 < val11) && (val2 < val12) && (val2 < val13) && (val2 < val14))
                    {

                        GridView1.Rows[jhp].Cells[7].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[7].Font.Bold = true;
                    }
                    if ((val8 < val1) && (val8 < val3) && (val8 < val4) && (val8 < val5) && (val8 < val6) && (val8 < val7) && (val8 < val2) && (val8 < val9) && (val8 < val10) && (val8 < val11) && (val8 < val12) && (val8 < val13) && (val8 < val14))
                    {

                        GridView1.Rows[jhp].Cells[8].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[8].Font.Bold = true;
                    }
                    if ((val9 < val1) && (val9 < val3) && (val9 < val4) && (val9 < val5) && (val9 < val6) && (val9 < val7) && (val2 < val8) && (val9 < val2) && (val9 < val10) && (val9 < val11) && (val9 < val12) && (val9 < val13) && (val9 < val14))
                    {

                        GridView1.Rows[jhp].Cells[9].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[9].Font.Bold = true;

                    }
                    if ((val10 < val1) && (val10 < val3) && (val10 < val4) && (val10 < val5) && (val10 < val6) && (val10 < val7) && (val10 < val8) && (val10 < val9) && (val10 < val2) && (val10 < val11) && (val10 < val12) && (val10 < val13) && (val10 < val14))
                    {

                        GridView1.Rows[jhp].Cells[10].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[10].Font.Bold = true;
                    }
                    if ((val11 < val1) && (val11 < val3) && (val11 < val4) && (val11 < val5) && (val11 < val6) && (val11 < val7) && (val11 < val8) && (val11 < val9) && (val11 < val10) && (val11 < val2) && (val11 < val12) && (val11 < val13) && (val11 < val14))
                    {

                        GridView1.Rows[jhp].Cells[11].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[11].Font.Bold = true;
                    }
                    if ((val12 < val1) && (val12 < val3) && (val12 < val4) && (val12 < val5) && (val12 < val6) && (val12 < val7) && (val12 < val8) && (val12 < val9) && (val12 < val10) && (val12 < val11) && (val12 < val2) && (val12 < val13) && (val12 < val14))
                    {

                        GridView1.Rows[jhp].Cells[12].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[12].Font.Bold = true;

                    }
                    if ((val13 < val1) && (val13 < val3) && (val13 < val4) && (val13 < val5) && (val13 < val6) && (val13 < val7) && (val13 < val8) && (val13 < val9) && (val13 < val10) && (val13 < val11) && (val13 < val12) && (val13 < val2) && (val13 < val14))
                    {

                        GridView1.Rows[jhp].Cells[13].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[13].Font.Bold = true;
                    }
                    if ((val14 < val1) && (val14 < val3) && (val14 < val4) && (val14 < val5) && (val14 < val6) && (val14 < val7) && (val14 < val8) && (val14 < val9) && (val14 < val10) && (val14 < val11) && (val14 < val12) && (val14 < val13) && (val14 < val2))
                    {

                        GridView1.Rows[jhp].Cells[14].ForeColor = System.Drawing.Color.Red;
                        GridView1.Rows[jhp].Cells[14].Font.Bold = true;
                    }



                    jhp = jhp + 1;




              }
            
              //string ggg = "ARANI-" + sumarani.ToString();

              //stt = ggg + "<font color='red'>   </font>"; 
              

          
            strP.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            
            data.addColumn('string', 'MONTH');
            data.addColumn('number', 'ARANI');
            data.addColumn('number', 'KAVERI');
            data.addColumn('number', 'GUDLUR');
            data.addColumn('number', 'WALAJA');
            data.addColumn('number', 'V_KOTA' );  
            data.addColumn('number', 'RCPURAM');
            data.addColumn('number', 'BOMMA');
            data.addColumn('number', 'TARIGONDA');
            data.addColumn('number', 'KALASTHIRI');
            data.addColumn('number', 'CSPURAM');
            data.addColumn('number', 'KONDEPI');
            data.addColumn('number', 'KAVALI');
            data.addColumn('number', 'GUDIPALLIPADU');
            data.addColumn('number', 'KALIGIRI');
            data.addColumn('number', 'Average');
            data.addColumn({type: 'string', role: 'annotation'});
            var view = new google.visualization.DataView(data);
            view.setColumns([0, 1, 1, 2]);
            data.addRows(" + DTGchart.Rows.Count + ");");
            for (int i = 0; i <= DTGchart.Rows.Count; i++)
            {
                try
                {
                    strP.Append("data.setValue( " + i + "," + 0 + "," + "'" + DTGchart.Rows[i]["MONTH"].ToString() + "');");
                    //   ssumarani = sumarani.ToString("f2");

                    //    strP.Append("data.setValue(" + i + "," + 1 + "," + "NEWS"  +", " + DTGchart.Rows[i]["ARANI"].ToString()  + ") ;");

                    double ARANI = Convert.ToDouble(DTGchart.Rows[i]["ARANI"]);

                    ssumarani = ssumarani + ARANI;

                    double KAVERI = Convert.ToDouble(DTGchart.Rows[i]["KAVERI"]);



                    ssumkaveri = ssumkaveri + KAVERI;


                    double GUDLUR = Convert.ToDouble(DTGchart.Rows[i]["GUDLUR"]);

                    ssumgud = ssumgud + GUDLUR;

                    double walaja = Convert.ToDouble(DTGchart.Rows[i]["WALAJA"]);

                    ssumwala = ssumwala +walaja;
                    double VKOTA = Convert.ToDouble(DTGchart.Rows[i]["V_KOTA"]);
                    ssumvkota= ssumvkota+ VKOTA;

                    double RCPURAM = Convert.ToDouble(DTGchart.Rows[i]["RCPURAM"]);

                    ssumrc = ssumrc + RCPURAM;
                    double BOMMA = Convert.ToDouble(DTGchart.Rows[i]["BOMMA"]);
                    ssumbomm = ssumbomm + BOMMA;
                    double TARIGONDA = Convert.ToDouble(DTGchart.Rows[i]["TARIGONDA"]);
                    ssumtari = ssumtari + TARIGONDA;
                    double KALASTHIRI = Convert.ToDouble(DTGchart.Rows[i]["KALASTHIRI"]);
                    ssumkal = ssumkal +KALASTHIRI;
                    double CSPURAM = Convert.ToDouble(DTGchart.Rows[i]["CSPURAM"]);
                    ssumcs = ssumcs + CSPURAM;
                    double KONDEPI = Convert.ToDouble(DTGchart.Rows[i]["KONDEPI"]);
                    ssumkon = ssumkon + KONDEPI;
                    double KAVALI = Convert.ToDouble(DTGchart.Rows[i]["KAVALI"]);
                    ssumkava = ssumkava + KAVALI;
                    double GUDIPALLIPADU = Convert.ToDouble(DTGchart.Rows[i]["GUDIPALLIPADU"]);
                    ssumgudi = ssumgudi + GUDIPALLIPADU;
                    double KALIGIRI = Convert.ToDouble(DTGchart.Rows[i]["KALIGIRI"]);
                    ssumkali = ssumkal +KALIGIRI;

                    strP.Append("data.setValue(" + i + "," + 1 + "," + DTGchart.Rows[i]["ARANI"].ToString() +  ") ;");
                    strP.Append("data.setValue(" + i + "," + 2 + "," + DTGchart.Rows[i]["KAVERI"].ToString() + ") ;");
                    strP.Append("data.setValue(" + i + "," + 3 + "," + DTGchart.Rows[i]["GUDLUR"].ToString() + ");");


                    strP.Append("data.setValue( " + i + "," + 4 + "," + "'" + DTGchart.Rows[i]["WALAJA"].ToString() + "');");
                    strP.Append("data.setValue(" + i + "," + 5 + "," + DTGchart.Rows[i]["V_KOTA"].ToString() + ") ;");
                    strP.Append("data.setValue(" + i + "," + 6 + "," + DTGchart.Rows[i]["RCPURAM"].ToString() + ") ;");
                    strP.Append("data.setValue(" + i + "," + 7 + "," + DTGchart.Rows[i]["BOMMA"].ToString() + ");");


                    strP.Append("data.setValue( " + i + "," + 8 + "," + "'" + DTGchart.Rows[i]["TARIGONDA"].ToString() + "');");
                    strP.Append("data.setValue(" + i + "," + 9 + "," + DTGchart.Rows[i]["KALASTHIRI"].ToString() + ") ;");
                    strP.Append("data.setValue(" + i + "," + 10 + "," + DTGchart.Rows[i]["CSPURAM"].ToString() + ") ;");
                    strP.Append("data.setValue(" + i + "," + 11 + "," + DTGchart.Rows[i]["KONDEPI"].ToString() + ");");


                    strP.Append("data.setValue( " + i + "," + 12 + "," + "'" + DTGchart.Rows[i]["KAVALI"].ToString() + "');");
                    strP.Append("data.setValue(" + i + "," + 13 + "," + DTGchart.Rows[i]["GUDIPALLIPADU"].ToString() + ") ;");
                    strP.Append("data.setValue(" + i + "," + 14 + "," + DTGchart.Rows[i]["KALIGIRI"].ToString() + ") ;");







                    GETSUM = (GETSUM + ARANI + KAVERI + walaja + GUDLUR + VKOTA + RCPURAM + BOMMA + TARIGONDA + KALASTHIRI + CSPURAM + KONDEPI + KAVALI + GUDIPALLIPADU + KALIGIRI) / 14;
                  
                   
                    strP.Append("data.setValue(" + i + "," + 15 + "," + GETSUM + ") ;");
                  

                  
                    double getcow1 = (getcow + ssumarani + ssumkaveri + ssumwala + ssumvkota + ssumrc + ssumbomm + ssumtari + ssumkal);

                    Label17.Text = "TotalL Cow MILK:" + getcow1.ToString("f2");

                    double getbuffalo1 = (getbuffalo + ssumgud + ssumcs + ssumkon + ssumkava + ssumgudi + ssumkali);

                    Label16.Text = "Total Baffalo MILK:" + getbuffalo1.ToString("f2");


                    tottalSUM = (tottalSUM1 + ssumarani + ssumkaveri + ssumwala + ssumvkota + ssumrc + ssumbomm + ssumtari + ssumkal + ssumgud + ssumcs + ssumkon + ssumkava + ssumgudi + ssumkali);
                    Label15.Text = "TotalL  MILK:" + tottalSUM.ToString("F2");
                }
                catch
                {


                }
                
            }

            Label1.Text = "ARANI-" + ssumarani.ToString();
            Label2.Text = "KAVERI-" + ssumkaveri.ToString();
            Label3.Text = "GUDLUR-" + ssumgud.ToString();
            Label4.Text = "WALAJA-" + ssumwala.ToString();
            Label5.Text = "VKOTA-" + ssumvkota.ToString();
            Label6.Text = "RCP-" + ssumrc.ToString();
            Label7.Text = "BOMMA-" + ssumbomm.ToString();
            Label8.Text = "TARI-" + ssumtari.ToString();
            Label9.Text = "RMRD-" + ssumkal.ToString();
            Label10.Text = "CSPURAM-" + ssumcs.ToString();
            Label11.Text = "KONDEPI-" + ssumkon.ToString();
            Label12.Text = "KAVALI-" + ssumkava.ToString();
            Label13.Text = "GUDI-" + ssumgudi.ToString();
            Label14.Text = "KALIGIRI-" + ssumkali.ToString();

          //strP.Append("   var chart =   new google.visualization.ColumnChart(document.getElementById('chart_div'));");

          //strP.Append(" chart.draw(data,{width:500, height:300,series: {0:{color: '#CF0980', visibleInLegend: 'true'}, 1:{color: '#999999', isStacked:'true', visibleInLegend: 'true'}, 2:{color: '#990099', visibleInLegend: 'true'}, 3:{color: '#FF99CC', visibleInLegend: 'true'}},");

          //strP.Append("}); }");
        
          //  strP.Append("</script>");



            strP.Append("   var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");

            //strP.Append(" chart.draw(data, {width:1200,height: 600,pointSize:2,lineWidth:1,bar: {groupWidth: '100'},   legend: { position: 'TOP', alignment: 'start' },title: 'Company Performance',");

            //strP.Append(" chart.draw(data, {width:1000,height: 600,pointSize:5,lineWidth:2,seriesType: 'bars',  series: {14: {type: 'line'}},  legend: { position: 'buttom', alignment: 'start' },title: '" + ggg + "',");
            //strP.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}}");

            strP.Append(" chart.draw(data, {width:1000,height: 600,pointSize:5,lineWidth:2,  seriesType: 'bars',  series: {14: {type: 'line'}},   legend: { position: 'buttom', alignment: 'start' }, ");
            strP.Append("hAxis: {title: 'Year', titleTextStyle: {color: 'green'}}");
            strP.Append("}); }");
            strP.Append("</script>");


            lt.Text = strP.ToString().Replace('*', '"');         
        }
        catch
        { }   
    }



    public void GETPLANTCODE()
    {
        string GET = "SELECT PLANT_CODE   FROM PLANT_MASTER  WHERE PLANT_CODE NOT IN (139,150,160)  ORDER BY PLANT_CODE";
        con = DB.GetConnection();
        SqlCommand CMD = new SqlCommand(GET, con);
        SqlDataAdapter DAA = new SqlDataAdapter(CMD);
        DAA.Fill(DTGGG,( "PLANTCODE"));




    }



    public void GETDATA()
    {

        try
        {

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            con.Close();
           
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str12 = "select MMM , CONVERT(DECIMAL(18,2),(MILK)) AS  MILK  from (SELECT YEAR = YEAR(PRDATE), MONTH = MONTH(PRDATE),MMM = UPPER(left(DATENAME(MONTH,PRDATE),3)),        MILK = (sum(mILK_KG)),plant_code  FROM    Procurement  WHERE  PRDATE  BETWEEN '"+d1+"' AND '"+d2+"' and Plant_Code='155' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE),plant_code) as am left join (Select Plant_Code,Plant_Name   from Plant_Master where Plant_Code='" + pcode + "') AS PM ON AM.Plant_Code=PM.Plant_Code ORDER BY MONTH";

            SqlCommand CMD1 = new SqlCommand(str12, con);
            SqlDataAdapter DAA1 = new SqlDataAdapter(CMD1);
            DAA1.Fill(DTGGG, ("DATA"));

        }
        catch
        {


        }
    }


    public void GETDATA1()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            //con.Close();
            //con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";

          //  DTGGG.Tables[2].Rows.Clear();
            string str12 = "select MMM ,  CONVERT(DECIMAL(18,2),(MILK)) AS  MILK  from (SELECT YEAR = YEAR(PRDATE), MONTH = MONTH(PRDATE),MMM = UPPER(left(DATENAME(MONTH,PRDATE),3)),        MILK = (sum(mILK_KG)),plant_code  FROM    Procurement  WHERE  PRDATE  BETWEEN '"+d1+" ' AND '"+d2+"' and Plant_Code='" + ViewState["GETTT"] + "' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE),plant_code) as am left join (Select Plant_Code,Plant_Name   from Plant_Master where Plant_Code='" + ViewState["GETTT"] + "') AS PM ON AM.Plant_Code=PM.Plant_Code ORDER BY MONTH";
           // string str12 = "select MMM ,  CONVERT(DECIMAL(18,2),(MILK)) AS  MILK  from (SELECT YEAR = YEAR(PRDATE), MONTH = MONTH(PRDATE),MMM = UPPER(left(DATENAME(MONTH,PRDATE),3)),        MILK = (sum(mILK_KG)),plant_code  FROM    Procurement  WHERE  PRDATE  BETWEEN '1-1-2016 ' AND '8-30-2016' and Plant_Code='" + ViewState["GETTT"] + "' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE),plant_code) as am left join (Select Plant_Code,Plant_Name   from Plant_Master where Plant_Code='" + ViewState["GETTT"] + "') AS PM ON AM.Plant_Code=PM.Plant_Code ORDER BY MONTH";

            SqlCommand CMD1 = new SqlCommand(str12, con);
            SqlDataAdapter DAA1 = new SqlDataAdapter(CMD1);
            DAA1.Fill(DTGGG, ("DATA1"));

        }
        catch
        {


        }
    }


   
    protected void Button7_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Overall Chilling Plant  Performance:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2.ColumnSpan = 15;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
}