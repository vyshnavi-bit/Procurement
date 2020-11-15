using System;
using System.Collections.Generic;
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
using System.Drawing;
using System.IO;
using System.Globalization;
using DotNet.Highcharts.Helpers;
public partial class RateCalculate : System.Web.UI.Page
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
    DataSet DTG = new DataSet();
    DataSet DTGG = new DataSet();
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
    double commission;
    double bonus;
    double calbonus;
    double calcart;
    double calspl;
    double templtr;
    int COUNTI=0;
    string RATEE;
//procurement variable;
    string Getagentid; 
    string prdate  ;
    string[] GET ;
    string Sessions ;
    double milkltr  ;
    string tmilkltr;
    double FAT  ;
    double SNF;
    string PlantCode  ;
    string Route_id ; 
    int NoofCans;  
    double Milk_kg  ;
    double Clr  ;
    string tclr;
    string Company_Code ;
    string Milk_Nature  ;
    string MilkNature;
    string SampleId  ;
    string Sampleno ;
    string milk_tip_st_time  ;
    string milk_tip_end_time ;
    string usr_weigher  ;
    string usr_analizer  ;
    string fat_kg  ;
    string snf_kg ;
    string Truck_id  ;
    string Status;
    int countroute;
    int countagent;
    double RATE;
    string tempamount;
    double  amount;
    msg MESS = new msg();
    double cart  ;
    string nature;
    double splbonus;
    int agentchartmode;
    string  cchartname;
    string bchartname;
    string CHARTID;
    int datacheck;
    int rows=0;
    int procimport;
    int procurement;
    double minfat;
    double maxfat;
    double bufftocowrate;
    string charttype;
    string NATURE;
    double calcommission=0;
    double SnfCutting;
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
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                }
                else
                {

                    pname = ddl_Plantname.SelectedItem.Text;


                }

            }
            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {


        }
    }

    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (150,139,160)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTGG, ("plantname"));
            ddl_Plantname.DataSource = DTGG.Tables[0];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
        }
        catch
        {

        }
    }
    public void gettingprocurementimportdata() //func0
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
           // string stt = "SELECT DISTINCT[Tid], [Agent_id], [Prdate], [Sessions], [Milk_ltr], [Fat], [Snf], [Plant_Code], [Route_id], [NoofCans], [Milk_kg], [Clr], [Company_Code], [Milk_Nature], [SampleId], [Sampleno], [milk_tip_st_time], [milk_tip_end_time], [usr_weigher], [usr_analizer], [fat_kg], [snf_kg], [Truck_id],[Status] FROM [Procurementimport] WHERE Company_Code=1 AND Plant_Code='" + pcode + "' AND prdate between '" + d1 + "' AND '" + d1 + "' AND     Sessions='" + ddl_shift .SelectedItem.Text+ "' AND AGENT_ID='938' ORDER BY prdate,Agent_id,SESSIONS";
            string stt = "SELECT DISTINCT[Tid], [Agent_id], [Prdate], [Sessions], [Milk_ltr], [Fat], [Snf], [Plant_Code], [Route_id], [NoofCans], [Milk_kg], [Clr], [Company_Code], [Milk_Nature], [SampleId], [Sampleno], [milk_tip_st_time], [milk_tip_end_time], [usr_weigher], [usr_analizer], [fat_kg], [snf_kg], [Truck_id],[Status] FROM [Procurementimport] WHERE Company_Code=1 AND Plant_Code='" + pcode + "' AND prdate between '" + d1 + "' AND '" + d1 + "'      ORDER BY prdate,Agent_id,SESSIONS";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG,("procimport"));
            
            
        }
        catch
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.Check) + "')</script>");
        }
    }
    public void gettingdatafromagentmaster() //func1
    {
        try
        {
            string stt1 = " select  TOP 1  cartage_amt,milk_nature,splbonus_amt,agentRateChartmode,dpuagentstatus,cowchartname,buffchartname     from agent_master   where plant_code='" + pcode + "' and agent_id='" + ViewState["Getagentid"] + "'";
            SqlCommand cmd1 = new SqlCommand(stt1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            DA1.Fill(DTG, ("agentmaster"));
          
         }
        catch
        {

        }
    }
    public void gettingdatafromagentmasterBUFFTOCOW() //func1
    {
        try
        {
            string stt1 = " select TOP 1   cartage_amt,milk_nature,splbonus_amt,agentRateChartmode,dpuagentstatus,cowchartname,buffchartname     from agent_master   where plant_code='" + pcode + "' and agent_id='" + ViewState["Getagentid"] + "'";
            SqlCommand cmd1 = new SqlCommand(stt1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            DA1.Fill(DTG, ("BUFFTOCOWagentmaster"));

        }
        catch
        {

        }
    }
    public void gettingchartmaster()  //func2
    {
        try
        {
          //  string stt2 = "select  chart_name,chart_type,min_fat,min_snf,BuffFatToCowRange,plant_code,company_code,status,active    from chart_master   where plant_code='" + pcode + "' and chart_name='" + ViewState["cowchartname"] + "' ";
            string stt2 = "select TOP 1  chart_name,chart_type,min_fat,min_snf,plant_code,company_code,status,active    from chart_master   where plant_code='" + pcode + "' and chart_name='" + ViewState["cowchartname"] + "' ";
            SqlCommand cmd2 = new SqlCommand(stt2, con);
            SqlDataAdapter DA2 = new SqlDataAdapter(cmd2);
            
            DA2.Fill(DTG, ("chartmaster"));
            COUNTI = COUNTI + 1;
        }
        catch
        {

        }
    }

    public void gettingchartmasterBUFFTOCOW()  //func2
    {
        try
        {
            string stt2 = "select TOP 1   chart_name,chart_type,min_fat,min_snf,plant_code,company_code,status,active    from chart_master   where plant_code='" + pcode + "' and chart_name='" + ViewState["cowchartname"] + "' ";
            SqlCommand cmd2 = new SqlCommand(stt2, con);
            SqlDataAdapter DA2 = new SqlDataAdapter(cmd2);

            DA2.Fill(DTG, ("chartmasterBUFFTOCOW"));
            COUNTI = COUNTI + 1;
        }
        catch
        {

        }
    }

    public void gettingchartmasterforbuff()  //func2
    {
        try
        {
            string stt2 = "select TOP 1   chart_name,chart_type,min_fat,min_snf,BuffFatToCowRange,SnfCutting,plant_code,company_code,status,active    from chart_master   where plant_code='" + pcode + "' and chart_name='" + ViewState["buffchartname"] + "' ";
            SqlCommand cmd2 = new SqlCommand(stt2, con);
            SqlDataAdapter DA2 = new SqlDataAdapter(cmd2);
            DA2.Fill(DTG, ("chartmaster"));
            COUNTI = COUNTI + 1;
        }
        catch
        {

        }
    }

   public void gettingTSratechart()  //func3
    {
        try
        {
            double ffat =Convert.ToDouble(ViewState["FAT"]);
            double fsnf = Convert.ToDouble(ViewState["SNF"]);
            double fatsnf = ffat + fsnf;
            string stt3 = " select TOP 1   Rate,Comission_Amount,Bouns_Amount    from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ('" + fatsnf + "'   BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE)   AND     Chart_Name='" + ViewState["cowchartname"] + "' ";
        //    string stt3 = " select Rate,Comission_Amount,Bouns_Amount    from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ('" + fatsnf + "'   BETWEEN  AND )   AND     Chart_Name='" + ViewState["cowchartname"] + "' ";
            SqlCommand cmd3 = new SqlCommand(stt3, con);
            SqlDataAdapter DA3 = new SqlDataAdapter(cmd3);
           DA3.Fill(DTG, ("TSratechart"));
                      
        }
        catch
        {

        }
    }


   public void gettingTSratechartCOWBUFF()  //func3
   {
       try
       {
           double ffat = Convert.ToDouble(ViewState["FAT"]);
           double fsnf = Convert.ToDouble(ViewState["SNF"]);
           double fatsnf = ffat + fsnf;
           string stt3 = " select TOP 1 Rate,Comission_Amount,Bouns_Amount    from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ('" + fatsnf + "'   BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE)   AND     Chart_Name='" + ViewState["cowchartname"] + "' ";
           SqlCommand cmd3 = new SqlCommand(stt3, con);
           SqlDataAdapter DA3 = new SqlDataAdapter(cmd3);
           DA3.Fill(DTG, ("TSratechartCOWBUFF"));

       }
       catch
       {

       }
   }
   public void gettingfatratechart()  //func9
   {
       try
       {
           string stt3 = " select TOP 1  Rate,Comission_Amount,Bouns_Amount    from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ( " + ViewState["FAT"] + "    BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE)   AND     Chart_name='" + ViewState["buffchartname"] + "' ";
           SqlCommand cmd3 = new SqlCommand(stt3, con);
           SqlDataAdapter DA3 = new SqlDataAdapter(cmd3);
          DA3.Fill(DTG, ("TSratechart"));

       }
       catch
       {

       }
   }
   public void gettingSNFratechart()  //func4
   {
       try
       {
           string stt4 = "select TOP 1  Rate,Comission_Amount,Bouns_Amount    from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ( " + ViewState["SNF"] + "  BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE )   AND     Chart_name='" + ViewState["cowchartname"] + "' ";
           SqlCommand cmd4 = new SqlCommand(stt4, con);
           SqlDataAdapter DA4 = new SqlDataAdapter(cmd4);
           DA4.Fill(DTG, ("SNFratechart"));
       }
       catch
       {

       }
   }

   public void gettingFATSNFratechart()  //func5
   {
       try
       {
           string stt1 = " select  TOP 1   RATE,Comission_Amount,Bouns_Amount   from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ( " + ViewState["FAT"] + "  BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE )    AND     Chart_name='" + ViewState["cowchartname"] + "'  AND status='1' ";
           SqlCommand cmd1 = new SqlCommand(stt1, con);
           SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            
           DA1.Fill(DTG, ("FATSNFratechart"));
       }
       catch
       {

       }
   }
   public void gettingFATSNFratechartrate()  //func6
   {
       try
       {
           string stt1 = " select  top 1   Rate,Comission_Amount,Bouns_Amount   from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ( " + ViewState["SNF"] + "  BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE )   AND     Chart_Name='" + ViewState["cowchartname"] + "' AND status='2' ";
           SqlCommand cmd1 = new SqlCommand(stt1, con);
           SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
           DA1.Fill(DTG, ("FATSNFratechartSNFRATE"));
       }
       catch
       {

       }
   }
   public void gettingFATSNFMIXINGFAT()  //func7
   {
       try
       {
           string stt1 = " select  TOP 1   STATUS   from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ( " + ViewState["FAT"] + "  BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE )   AND    Chart_Name='" + ViewState["cowchartname"] + "' ";
           SqlCommand cmd1 = new SqlCommand(stt1, con);
           SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
         //  DA1.Fill(DTG);
           DA1.Fill(DTG, ("FATSNFratechartSNFRATE"));
       }
       catch
       {

       }
   }
   public void gettingFATSNFMIXINGSNF()  //func8
   {
       try
       {
           string stt1 = " select top 1    Rate,Comission_Amount,Bouns_Amount    from Rate_Chart  where Plant_Code='" + pcode + "'  AND   ( " + ViewState["SNF"] + "  BETWEEN FROM_RANGEVALUE AND TO_RANGEVALUE )   AND     Chart_Name='" + ViewState["cowchartname"] + "'  AND  STATUS = '" + ViewState["FATSTATUS"] + "'";
           SqlCommand cmd1 = new SqlCommand(stt1, con);
           SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
         
           DA1.Fill(DTG, ("FATSNFratechartSNFRATEcalc"));
       }
       catch
       {

       }
   }

   public void BuffRatetoCow()  //func4
   {
       string stt1 = "select  *   from snfcutting where plant_code='" + pcode + "'  and chart_name= '" + ViewState["cowchartname"] + "' ";
       SqlCommand cmd1 = new SqlCommand(stt1, con);
       SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
       DA1.Fill(DTG, ("snfcutting"));
   }
     public void snfcuttingplant()  //func4
    {
        string stt1 = "select  *   from snfcutting where plant_code='" + pcode + "'  and chart_name= '" + ViewState["buffchartname"] + "' and  " + SNF + "        BETWEEN SnfFromRange AND SnfToRange    ";
            SqlCommand cmd1 = new SqlCommand(stt1, con);
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
            DA1.Fill(DTG, ("snfcutting"));
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

        checkcodition();

        if (datacheck == 0)
        {
            gettingprocurementimportdata();
            foreach (DataRow importprodata in DTG.Tables[0].Rows)
            {
               
                Getagentid = importprodata[1].ToString();
             //   prdate = importprodata[2].ToString();
                string Convedate = importprodata[2].ToString();
                string[] arrdate = Convedate.Split('-');

                string dd = arrdate[0];
                string MM = arrdate[1];
                string yy = arrdate[2];

                prdate = MM +  "/" + dd  + "/" + yy;
                GET = prdate.Split(' ');
                prdate = GET[0].ToString();
                Sessions = importprodata[3].ToString();
                FAT = Convert.ToDouble(importprodata[5]);
                SNF = Convert.ToDouble(importprodata[6]);
                PlantCode = importprodata[7].ToString();
                Route_id = importprodata[8].ToString();
                NoofCans = Convert.ToInt16(importprodata[9]);
                Milk_kg = Convert.ToDouble(importprodata[4]);
                string tempkg = Milk_kg.ToString("f2");
                Milk_kg = Convert.ToDouble(tempkg);
                tmilkltr = (Milk_kg / 1.03).ToString("f2");
                milkltr = Convert.ToDouble(tmilkltr);
                if (Milk_kg < 0)
                {

                    Milk_kg = 0;
                    milkltr = 0;
                }

                //   @Clr=(((@Snf - 0.36) - (@Fat * 0.21)) * 4)
                Clr = Convert.ToDouble((((SNF - 0.36) - (FAT * 0.21)) * 4));
                tclr = Clr.ToString("f2");
                Clr = Convert.ToDouble(tclr);
                // Clr = importprodata[11].ToString();
                Company_Code = importprodata[12].ToString();
                Milk_Nature = importprodata[13].ToString();
                if (Milk_Nature == "1")
                {
                    MilkNature = "Cow";
                }
                else
                {
                    MilkNature = "Buffalo";
                }
                SampleId = importprodata[14].ToString();
                Sampleno = importprodata[15].ToString();
                milk_tip_st_time = importprodata[16].ToString();
                milk_tip_end_time = importprodata[17].ToString();
                usr_weigher = importprodata[18].ToString();
                usr_analizer = importprodata[19].ToString();
                fat_kg = ((Milk_kg / 100) * FAT).ToString("f2");
                snf_kg = ((Milk_kg / 100) * SNF).ToString("f2");
                Truck_id = importprodata[22].ToString();
                Status = importprodata[23].ToString();
                ViewState["Getagentid"] = Getagentid.ToString();
                ViewState["FAT"] = FAT.ToString();
                ViewState["SNF"] = SNF.ToString();
                getroutedetails();
                if (countroute == 1)
                {


                }
                else
                {
                    string insertroute;
                    con = DB.GetConnection();
                    insertroute = "INSERT INTO route_Master(route_Id,route_Name,tot_Distance,added_date,status,Company_Code,Plant_Code,Lstatus) values   ('" + Route_id + "' ,'" + "ROUTE-" + Route_id + "','" + 1 + "','" + System.DateTime.Now + "','" + 1 + "','" + ccode + "' ,'" + pcode + "','" + 1 + "')";
                    SqlCommand cmd = new SqlCommand(insertroute, con);
                }
                getagentdetails();
                if (countagent == 1)
                {


                }
                else
                {
                    string insertagnet;
                    con = DB.GetConnection();
                    insertagnet = " INSERT INTO Agent_Master(Agent_Id,Agent_Name,Type,Cartage_Amt,Company_code,Plant_code,Route_id,Bank_Id,Payment_mode,Agent_AccountNo,AddedDate,phone_Number,Milk_Nature,Ifsc_code,SplBonus_Amt,agentratechartmode) values ('" + Getagentid + "','" + Getagentid + "','" + 0 + "','" + 0 + "','" + ccode + "','" + pcode + "','" + Route_id + "','" + 0 + "','CASH','" + 0 + "','" + 0 + "','" + Milk_Nature + "','" + 0 + "','" + 0 + ",'" + 0 + "')";
                    SqlCommand cmd = new SqlCommand(insertagnet, con);
                }
                gettingdatafromagentmaster();
                COUNTI = COUNTI + 1;
                foreach (DataRow agentmaster in DTG.Tables[COUNTI].Rows)
                {
                    cart = Convert.ToDouble(agentmaster[0]);
                    nature = agentmaster[1].ToString();
                    splbonus = Convert.ToDouble(agentmaster[2]);
                    //int agentchartmode  = Convert.ToInt16(agentmaster[3]);
                    //int dpuagent;
                    //try
                    //{
                    //  //  dpuagent = Convert.ToInt16(agentmaster[4]);
                    //}
                    //catch
                    //{
                    //     dpuagent = 0;

                    //}
                    cchartname = (agentmaster[5]).ToString();  //going to add filed chartname
                    bchartname = (agentmaster[6]).ToString();
                    ViewState["cart"] = cart;
                    ViewState["spbonus"] = splbonus;
                    ViewState["nature"] = nature;
                    ViewState["cowchartname"] = cchartname.ToString();
                    ViewState["buffchartname"] = bchartname.ToString();
                }

                if ((MilkNature == "1")  ||  (MilkNature == "Cow"))
                {
                    gettingchartmaster();
                }

                else
                {
                    gettingchartmasterforbuff();

                }
               
                charttype = DTG.Tables[COUNTI].Rows[rows][1].ToString();
                minfat = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                maxfat =Convert.ToDouble( DTG.Tables[COUNTI].Rows[rows][3]);
                if ((MilkNature == "1") || (MilkNature == "Cow"))
                {
                    bufftocowrate = 0;
                    SnfCutting = 0;
                }
                else
                {
                    bufftocowrate = Convert.ToDouble(DTG.Tables[COUNTI].Rows[1][4]);
                    SnfCutting = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][5]);

                }
                NATURE = ViewState["nature"].ToString();
               
                if (NATURE == "Cow")
                {
                    if (charttype == "TS")
                    {
                        gettingTSratechart();

                        try
                        {
                            COUNTI = COUNTI + 1;
                            RATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                            commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][1]);
                            bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                            RATE = RATE * (FAT + SNF);
                            RATEE = RATE.ToString("F2");
                            RATE = Convert.ToDouble(RATEE);
                            tempamount = (RATE * milkltr).ToString("F2");
                            if (RATE > 0)
                            {
                                amount = Convert.ToDouble(tempamount);
                            }
                            else
                            {
                                amount = 0;
                            }

                           
                        }
                        catch(Exception EE)
                        {
                            string GET45 =    EE.ToString();

                        }
                    }
                    if (charttype == "SNF")
                    {
                        COUNTI = COUNTI + 1;
                        gettingSNFratechart();
                        double TEMPRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                        commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][1]);
                        bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                        double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                        double TEMPSNF = Convert.ToDouble(ViewState["SNF"]);
                        RATE = TEMPRATE * (TEMPFAT + TEMPSNF);
                        RATEE = RATE.ToString("F2");
                        RATE = Convert.ToDouble(RATEE);
                        tempamount = (RATE * milkltr).ToString("F2");
                        

                        if (RATE > 0)
                        {
                            amount = Convert.ToDouble(tempamount);
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    if (charttype == "FATSNF")
                    {
                        COUNTI = COUNTI + 1;
                        gettingFATSNFratechart();
                      
                        commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][1]);
                        bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                        double  FRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                        gettingFATSNFratechartrate();
                        COUNTI = COUNTI + 1;
                        double SRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                        double TEMPFATRATE = (FAT * FRATE);
                        double TEMPSNFRATE = (SNF * SRATE);
                        RATE = (TEMPFATRATE + TEMPSNFRATE);
                        RATEE = RATE.ToString("F2");
                        RATE = Convert.ToDouble(RATEE);
                        tempamount = (RATE * milkltr).ToString("F2");
                       
                        if (RATE > 0)
                        {
                            amount = Convert.ToDouble(tempamount);
                        }
                        else
                        {
                            amount = 0;
                        }
                       
                    }
                    if (charttype == "FATSNFMIXED")
                    {
                        COUNTI = COUNTI + 1;
                        gettingFATSNFMIXINGFAT();
                        COUNTI = COUNTI + 1;
                        ViewState["FATSTATUS"] = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                        gettingFATSNFMIXINGSNF();
                        COUNTI = COUNTI + 1;
                        RATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                        commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][1]);
                        bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                        commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][1]);
                        bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                        double TEMPRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                        double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                        double TEMPSNF = Convert.ToDouble(ViewState["SNF"]);
                        RATE = TEMPRATE * (TEMPFAT + TEMPSNF);
                        tempamount = (RATE * milkltr).ToString("F2");
                        if (RATE > 0)
                        {
                            amount = Convert.ToDouble(tempamount);
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    CHARTID = ViewState["cowchartname"].ToString();
                }
                if (NATURE == "Buffalo")
                {

                    if (charttype == "FAT")
                    {
                        COUNTI = COUNTI + 1;
                        gettingfatratechart();
                        RATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][0]);
                        commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][1]);
                        bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                        CHARTID = ViewState["buffchartname"].ToString();

                        if (SnfCutting == 1)
                        {
                            snfcuttingplant();
                            COUNTI = COUNTI + 1;
                            double tempcut = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][5]);
                            RATE = RATE - tempcut;

                        }
                        else
                        {

                            RATE = (FAT * RATE);
                            tempamount = (RATE * Milk_kg).ToString("F2");
                            amount = Convert.ToDouble(tempamount);


                        }
                      



                       




                    }

                    if ( FAT < bufftocowrate)
                    {



                        COUNTI = COUNTI + 1;


                    
                        gettingdatafromagentmasterBUFFTOCOW();
                      
                        foreach (DataRow agentmaster in DTG.Tables[COUNTI].Rows)
                        {
                            cart = Convert.ToDouble(agentmaster[0]);
                            nature = agentmaster[1].ToString();
                            splbonus = Convert.ToDouble(agentmaster[2]);
                            //int agentchartmode  = Convert.ToInt16(agentmaster[3]);
                            //int dpuagent;
                            //try
                            //{
                            //  //  dpuagent = Convert.ToInt16(agentmaster[4]);
                            //}
                            //catch
                            //{
                            //     dpuagent = 0;

                            //}
                            cchartname = (agentmaster[5]).ToString();  //going to add filed chartname
                            bchartname = (agentmaster[6]).ToString();
                            ViewState["cart"] = cart;
                            ViewState["spbonus"] = splbonus;
                            ViewState["nature"] = nature;
                            ViewState["cowchartname"] = cchartname.ToString();
                            ViewState["buffchartname"] = bchartname.ToString();
                        }


                    gettingchartmasterBUFFTOCOW();
                  
                    charttype = DTG.Tables[COUNTI].Rows[0][1].ToString();
                    minfat = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][2]);
                    maxfat =Convert.ToDouble( DTG.Tables[COUNTI].Rows[0][3]);
                    NATURE = ViewState["nature"].ToString();
               

                      if (charttype == "TS")
                        {
                           
                            gettingTSratechartCOWBUFF();
                            COUNTI = COUNTI + 1;
                            RATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][0]);
                            commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][1]);
                            bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][2]);
                            RATE = RATE * (FAT + SNF);
                            RATEE = RATE.ToString("F2");
                            RATE = Convert.ToDouble(RATEE);
                            tempamount = (RATE * Milk_kg).ToString("F2");
                            amount = Convert.ToDouble(tempamount);
                        }
                        if (charttype == "SNF")
                        {
                            COUNTI = COUNTI + 1;
                            gettingSNFratechart();
                            double TEMPRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][0]);
                            commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][1]);
                            bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][2]);
                            double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                            double TEMPSNF = Convert.ToDouble(ViewState["SNF"]);
                            RATE = TEMPRATE * (TEMPFAT + TEMPSNF);
                            RATEE = RATE.ToString("F2");
                            RATE = Convert.ToDouble(RATEE);
                            tempamount = (RATE * Milk_kg).ToString("F2");
                            amount = Convert.ToDouble(tempamount);
                        }
                        if (charttype == "FATSNF")
                        {
                            COUNTI = COUNTI + 1;
                            gettingFATSNFratechart();
                            gettingFATSNFratechartrate();
                            commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][1]);
                            bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][2]);
                            double FRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][0]);
                            COUNTI = COUNTI + 1;
                            double SRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][0]);

                            double TEMPFATRATE = (FAT * FRATE);
                            double TEMPSNFRATE = (SNF * SRATE);
                            RATE = (TEMPFATRATE + TEMPSNFRATE);
                            RATEE = RATE.ToString("F2");
                            RATE = Convert.ToDouble(RATEE);
                            tempamount = (RATE * Milk_kg).ToString("F2");
                            amount = Convert.ToDouble(tempamount);

                        }
                        if (charttype == "FATSNFMIXED")
                        {
                            COUNTI = COUNTI + 1;
                            gettingFATSNFMIXINGFAT();
                            ViewState["FATSTATUS"] = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][0]);
                            gettingFATSNFMIXINGSNF();
                            COUNTI = COUNTI + 1;
                            RATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][0]);
                            commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][1]);
                            bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                            commission = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][1]);
                            bonus = Convert.ToDouble(DTG.Tables[COUNTI].Rows[rows][2]);
                            double TEMPRATE = Convert.ToDouble(DTG.Tables[COUNTI].Rows[0][0]);
                            double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                            double TEMPSNF = Convert.ToDouble(ViewState["SNF"]);
                            RATE = TEMPRATE * (TEMPFAT + TEMPSNF);
                            tempamount = (RATE * Milk_kg).ToString("F2");
                            amount = Convert.ToDouble(tempamount);
                        }
                        CHARTID = ViewState["cowchartname"].ToString();

                    }
                    //if (NATURE == "Buffalo")
                    //{

                    //    if (charttype == "FAT")
                    //    {
                    //        COUNTI = COUNTI + 1;
                    //        gettingfatratechart();
                    //        RATE = Convert.ToDouble(DTG.Tables[9].Rows[rows][0]);
                    //        commission = Convert.ToDouble(DTG.Tables[9].Rows[rows][1]);
                    //        bonus = Convert.ToDouble(DTG.Tables[9].Rows[rows][2]);
                    //        CHARTID = ViewState["buffchartname"].ToString();
                    //        tempamount = (RATE * Milk_kg).ToString("F2");
                    //        amount = Convert.ToDouble(tempamount);
                    //    }

                    //}

                }

                if (NATURE == "Cow")
                {

                    if ((commission < 0.20) && (commission > 0))
                    {
                        double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                        templtr = milkltr;
                        double mkg = templtr * 1.03;
                        calcommission = (RATE * commission) * templtr;
                    }
                     if ((commission >= 0.20) && (commission > 0))
                    {
                        double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                        templtr = milkltr; ;
                        double mkg = templtr * 1.03;
                        calcommission = (commission * templtr);

                    }

                    calbonus = bonus * templtr;

                }

                else
                {
                    if ((commission < 0.20) && (commission > 0))
                    {
                        double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                        templtr = milkltr;
                        double mkg = templtr * 1.03;
                        calcommission = (TEMPFAT * commission) * mkg;
                    }
                    if ((commission >= 0.20) && (commission > 0))
                    {
                        double TEMPFAT = Convert.ToDouble(ViewState["FAT"]);
                        templtr = milkltr;
                        double mkg = templtr * 1.03;
                        calcommission = (commission * mkg);
                    }

                    calbonus = bonus * templtr;
                }
                string TEMPCOMM;
                double getcart;
                double spbonus;
                string temprate;
                if (FAT >= minfat && SNF >= maxfat)
                {
                   TEMPCOMM = calcommission.ToString("F2");
                    calcommission = Convert.ToDouble(TEMPCOMM);
                    getcart = Convert.ToDouble(ViewState["cart"]);
                    spbonus = Convert.ToDouble(ViewState["spbonus"]);
                    calcart = getcart * milkltr;
                    calspl = spbonus * milkltr;
                    temprate = (RATE).ToString("f2");
                    RATE = Convert.ToDouble(temprate);
                    if(RATE > 0)
                    {
                    amount = Convert.ToDouble(tempamount);
                    }
                    else
                    {
                    amount = 0;
                    }

                }
                else
                {
                    TEMPCOMM = "0";
                    calcommission = 0;
                    getcart = 0;
                    spbonus = 0;
                    calcart = 0;
                    calspl = 0;
                    temprate = "0";
                    RATE = 0;
                    amount = Convert.ToDouble(tempamount);

                }
                string finalinserting = " insert into procurement([Agent_id], [Prdate], [Sessions], [Milk_ltr], [Fat], [Snf], [Rate], [Amount], [Plant_Code], [Route_id], [NoofCans], [Milk_kg], [Clr], [Company_Code], [Ratechart_Id], [Milk_Nature], [ComRate], [SampleId], [Sampleno], [milk_tip_st_time], [milk_tip_end_time], [usr_weigher], [usr_analizer], [fat_kg], [snf_kg], [Truck_id],[Status],[CartageAmount],[SplBonusAmount],[RateStatus])values('" + Getagentid + "','" + prdate + "','" + Sessions + "','" + milkltr + "','" + FAT + "','" + SNF + "' ,'" + RATE + "','" + amount + "','" + pcode + "','" + Route_id + "','" + NoofCans + "','" + Milk_kg + "','" + Clr + "','" + ccode + "','" + CHARTID + "','" + Milk_Nature + "','" + calcommission + "','" + SampleId + "','" + SampleId + "','" + milk_tip_st_time + "','" + milk_tip_end_time + "','" + usr_weigher + "','" + usr_analizer + "','" + fat_kg + "','" + snf_kg + "','" + Truck_id + "','" + 0 + "','" + calcart + "','" + calspl + "','  " + ' ' + "')";
                SqlCommand cmd143 = new SqlCommand(finalinserting, con);
                cmd143.ExecuteNonQuery();
                COUNTI = 0;
              //  rows = rows + 1;
                calcommission = 0;
                DTG.Tables.Clear();
            }
            procurementimport();
            if (procimport == procurement)
            {

                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.save) + "')</script>");

            }

            else
            {
                
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string delete;
            delete = "delete   from procurement    where plant_code='" + pcode + "'   and prdate='" + d1 + "'  AND  Sessions='" + ddl_shift.SelectedItem.Text + "' "; 
            con= DB.GetConnection();
            SqlCommand cmd = new SqlCommand(delete, con);
            cmd.ExecuteNonQuery();
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.Check) + "')</script>");

            }

        }

        else
        {

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.Check) + "')</script>");
        }
    }
    public void getroutedetails()
    {
        string getroute = "";
        getroute = "Select route_id   from route_master   where plant_code='" + pcode + "'   and route_id='" + Route_id + "' ";
        con = DB.GetConnection();
        SqlCommand cmdroute = new SqlCommand(getroute, con);
        SqlDataAdapter DAroute = new SqlDataAdapter(cmdroute);
        DAroute.Fill(DTG, ("route_id"));
        COUNTI = COUNTI + 1;
        try
        {
            if (DTG.Tables[COUNTI].Rows.Count > 0)
            {
                countroute = 1;
              
            }
            else
            {
                countroute = 0;

            }
        }
        catch
        {
            countroute = 0;
            COUNTI = COUNTI - 1;

        }
    }
    public void getagentdetails()
    {
        string getagent = "";
        getagent = "Select agent_id   from Agent_Master   where plant_code='" + pcode + "' and   agent_id='" + Getagentid + "' ";
        con = DB.GetConnection();
        SqlCommand cmdagent = new SqlCommand(getagent, con);
        SqlDataAdapter DAagent = new SqlDataAdapter(cmdagent);
        DAagent.Fill(DTG, ("agent_id"));
        COUNTI = COUNTI + 1;
        if (DTG.Tables[COUNTI].Rows.Count > 0)
        {
            countagent = 1;
        }
        else
        {
            countagent = 0;
        }
    }


    public void checkcodition()
    {
        string checkpro = "";


            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            con = DB.GetConnection();
            //checkpro = "Select   *    from  Procurement where plant_code='" + pcode + "' and prdate='" + d1 + "' and  Sessions='" + ddl_shift.SelectedItem.Text + "' ";
            checkpro = "Select   *    from  Procurement where plant_code='" + pcode + "' and prdate='" + d1 + "' ";
            SqlCommand cmd = new SqlCommand(checkpro, con);
            DataTable ddt = new DataTable();
            SqlDataAdapter drt = new SqlDataAdapter(cmd);
            drt.Fill(ddt);
            if (ddt.Rows.Count > 0)
            {
                datacheck = 1;
            }
            else
            {
                datacheck = 0;

            }

    }
    public void procurementimport()
    {
        string checkpro = "";
        DateTime dt1 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        con = DB.GetConnection();
        checkpro = "select procimportrows,procrows   from (select COUNT(*) as procimportrows,Plant_Code  from   Procurementimport   where Plant_Code='"+pcode+"'  and Prdate='"+d1+"' group by Plant_Code ) as procim left join (select COUNT(*) as procrows,Plant_Code  from   Procurementimport   where Plant_Code='"+pcode+"'  and Prdate='"+d1+"' group by Plant_Code )as pro  on procim.Plant_Code=pro.Plant_Code";
        SqlCommand cmd = new SqlCommand(checkpro, con);
        DataTable ddt = new DataTable();
        SqlDataAdapter drt = new SqlDataAdapter(cmd);
        drt.Fill(ddt);
        foreach(DataRow read in ddt.Rows)
        {
            procimport = Convert.ToInt16(read[0]);
            procurement = Convert.ToInt16(read[1]);
        }
    }
    public void getagentratechartmissing()
    {



    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}