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

public partial class CowBuffaloTotalAbstract : System.Web.UI.Page
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
    double AvfRate;
    double AvfRateASS;
    int IJK = 0;
    int IJKK = 1;

    double AvfFAT;
    double AvfFATASS;

    double AvfSNF;
    double AvfSNFASS;

    double AvfFATKG;
    double AvfFATKGASS;

    double AvfSNFKG;
    double AvfSNFKGASS;

    double AvfTS;
    double AvfTSASS;

    double AvfRTS;
    double AvfRTSSASS;

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



                    if (roleid < 3)
                    {
                        loadsingleplant();

                    }

                    else
                    {

                        LoadPlantcode();
                    }


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
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {

            if (RadioButtonList1.SelectedItem.Value == "1")
            {
                GRID();
                GridView1.Visible = true;
                GridView2.Visible = false;
            }
            if (RadioButtonList1.SelectedItem.Value == "2")
            {
                GRID1();
                GridView1.Visible = false;
                GridView2.Visible = true;
            }

            if (RadioButtonList1.SelectedItem.Value == "3")
            {
                GRID2();
                GridView1.Visible = false;
                GridView2.Visible = true;
               
            }
        }
        catch
        {


        }
        
    }
    public void GRID()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string GETCOW;
            con = DB.GetConnection();
            // GETCOW = "select llll.Route_id,Route_Name,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,SUM(Ts) as Ts ,SUM(Rts) as Rts  from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as Cartage,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,(SUM(AvgFat) + SUM(Avgsnf)) as Ts ,convert(decimal(18,2),( Sum(AvfRate) / (SUM(AvgFat) + SUM(Avgsnf)))) as Rts from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code=168 AND Prdate BETWEEN '"+d1+"' AND '"+d2+"'  AND fat < 4.5  GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as llll left join(select Route_ID,Route_Name   from Route_Master where Plant_Code='"+pcode+"'  group by Route_ID,Route_Name ) as rm on llll.Route_id=rm.Route_ID group by llll.Route_id,Route_Name";
          //  GETCOW = "select Route_Name,convert(decimal(18,2), SUM(kG)) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as Cartage,Sum(SplBonusAmount) as SplBonus,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,SUM(Ts) as Ts ,SUM(Rts) as Rts  from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,(SUM(AvgFat) + SUM(Avgsnf)) as Ts ,convert(decimal(18,2),( Sum(AvfRate) / (SUM(AvgFat) + SUM(Avgsnf)))) as Rts from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + d1 + "' AND '" + d2 + "'  AND fat < 4.5  GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as llll left join(select Route_ID,Route_Name   from Route_Master where Plant_Code='" + pcode + "'  group by Route_ID,Route_Name ) as rm on llll.Route_id=rm.Route_ID group by llll.Route_id,Route_Name";

            GETCOW = "select Route_Name,convert(decimal(18,2), SUM(kG)) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as Cartage,Sum(SplBonusAmount) as SplBonus,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,SUM(Ts) as Ts ,SUM(Rts) as Rts  from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,(SUM(AvgFat) + SUM(Avgsnf)) as Ts ,convert(decimal(18,2),( Sum(AvfRate) / (SUM(AvgFat) + SUM(Avgsnf)))) as Rts from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code='" + pcode + "' AND Prdate BETWEEN '" + d1 + "' AND '" + d2 + "'  AND fat < 4.5  GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as llll left join(select Route_ID,Route_Name   from Route_Master where Plant_Code='" + pcode + "'  group by Route_ID,Route_Name ) as rm on llll.Route_id=rm.Route_ID group by llll.Route_id,Route_Name";
            SqlCommand cmd = new SqlCommand(GETCOW, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(DTG);
            GridView1.DataSource = DTG.Tables[0];
            GridView1.DataBind();





            GridView1.FooterRow.Cells[1].Text = "TOTAL";

            decimal milkkg = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("kg"));
            GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            GridView1.FooterRow.Cells[2].Text = milkkg.ToString("0.00");

            double milkLTR = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("Ltr"));
            GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            GridView1.FooterRow.Cells[3].Text = milkLTR.ToString("0.00");


            double CARTAGE = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("Cartage"));
            GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            GridView1.FooterRow.Cells[4].Text = CARTAGE.ToString("0.00");



            double SplBonus = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("SplBonus"));
            GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
            GridView1.FooterRow.Cells[5].Text = SplBonus.ToString("0.00");



            double TotAmount = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("TotAmount"));
            GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            GridView1.FooterRow.Cells[6].Text = TotAmount.ToString("0.00");




            foreach(GridViewRow DRR in GridView1.Rows)
            {





                 AvfRate = Convert.ToDouble(DRR.Cells[7].Text);
                 AvfRateASS = AvfRateASS + AvfRate;

                 double AvfRateASSAVG = AvfRateASS / IJKK;
                 GridView1.FooterRow.Cells[7].Text = AvfRateASSAVG.ToString("F2");
                


                 
                 AvfFAT = Convert.ToDouble(DRR.Cells[8].Text);
                 AvfFATASS = AvfFATASS + AvfFAT;
                 double AvfRateASSFATAVG = AvfFATASS / IJKK;
                 GridView1.FooterRow.Cells[8].Text = AvfRateASSFATAVG.ToString("F2");
                

                 AvfSNF = Convert.ToDouble(DRR.Cells[9].Text);
                 AvfSNFASS = AvfSNFASS + AvfSNF;
                 double AvfRateASSSNAFVG = AvfSNFASS / IJKK;
                 GridView1.FooterRow.Cells[9].Text = AvfRateASSSNAFVG.ToString("F2");

  

               

                

                 



                 AvfFATKG = Convert.ToDouble(DRR.Cells[10].Text);
                 AvfFATKGASS = AvfFATKGASS + AvfFATKG;
                 double AVGFATKGSUM = AvfFATKGASS;
                 GridView1.FooterRow.Cells[10].Text = AVGFATKGSUM.ToString("F2");


                 AvfSNFKG = Convert.ToDouble(DRR.Cells[11].Text);
                 AvfSNFKGASS = AvfSNFKGASS + AvfSNFKG;
                 double AVGFATSNFKGG = AvfSNFKGASS;
                 GridView1.FooterRow.Cells[11].Text = AVGFATSNFKGG.ToString("F2");

                 AvfTS = Convert.ToDouble(DRR.Cells[12].Text);
                 AvfTSASS = AvfTSASS + AvfTS;
                 double AVGTSSUM = AvfTSASS / IJKK; ;
                 GridView1.FooterRow.Cells[12].Text = AVGTSSUM.ToString("F2");
             

                 AvfRTS = Convert.ToDouble(DRR.Cells[13].Text);
                 AvfRTSSASS = AvfRTSSASS + AvfRTS;
                 double AVGRTSSUM = AvfRTSSASS / IJKK; ;
                 GridView1.FooterRow.Cells[13].Text = AVGRTSSUM.ToString("F2");



                 IJK = IJK + 1;
                 IJKK = IJKK + 1;

            }
            //decimal AvGRate = DTG.Tables[0].AsEnumerable().Average(row => row.Field<decimal>("AvfRate "));
            //GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            //GridView1.FooterRow.Cells[7].Text = AvGRate.ToString("0.00");


            //double AVGFAT = DTG.Tables[0].AsEnumerable().Average(row => row.Field<double>("AvgFat "));
            //GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            //GridView1.FooterRow.Cells[8].Text = AVGFAT.ToString("0.00");

            //decimal AVGSNF = DTG.Tables[0].AsEnumerable().Average(row => row.Field<decimal>("Avgsnf "));
            //GridView1.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            //GridView1.FooterRow.Cells[9].Text = AVGSNF.ToString("0.00");



            //decimal Fatkg = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Fatkg "));
            //GridView1.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            //GridView1.FooterRow.Cells[10].Text = Fatkg.ToString("0.00");

            //decimal Snfkg = DTG.Tables[0].AsEnumerable().Average(row => row.Field<decimal>("Snfkg"));
            //GridView1.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
            //GridView1.FooterRow.Cells[11].Text = AVGSNF.ToString("0.00");



        }
        catch
        {

        }

    }

    public void GRID1()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string GETCOW;
            con = DB.GetConnection();
            // GETCOW = "select llll.Route_id,Route_Name,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,SUM(Ts) as Ts ,SUM(Rts) as Rts  from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as Cartage,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,(SUM(AvgFat) + SUM(Avgsnf)) as Ts ,convert(decimal(18,2),( Sum(AvfRate) / (SUM(AvgFat) + SUM(Avgsnf)))) as Rts from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code=168 AND Prdate BETWEEN '"+d1+"' AND '"+d2+"'  AND fat < 4.5  GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as llll left join(select Route_ID,Route_Name   from Route_Master where Plant_Code='"+pcode+"'  group by Route_ID,Route_Name ) as rm on llll.Route_id=rm.Route_ID group by llll.Route_id,Route_Name";
            //    GETCOW = "select lllll.Route_id,Route_Name,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code=168 AND Prdate BETWEEN '"+d1+"' AND '"+d2+"'  AND   fat >= 4.5   GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as lllll left join (select Route_ID,Route_Name   from route_master where plant_code='"+pcode+"' group by Route_ID,Route_Name) as rm on lllll.Route_id=rm.Route_ID group by lllll.Route_id,Route_Name";
            GETCOW = "select Route_Name,convert(decimal(18,2), SUM(kG)) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as Cartage,Sum(SplBonusAmount) as SplBonus,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,convert(decimal(18,2),(SUM(TotAmount) /  SUM(fatkg))) as KgfatRate from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code='"+pcode+"' AND Prdate BETWEEN '" + d1 + "' AND '" + d2 + "' AND   fat >= 4.5   GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as lllll left join (select Route_ID,Route_Name   from route_master where plant_code='" + pcode + "' group by Route_ID,Route_Name) as rm on lllll.Route_id=rm.Route_ID group by lllll.Route_id,Route_Name";
            SqlCommand cmd = new SqlCommand(GETCOW, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(DTG);
            GridView2.DataSource = DTG.Tables[0];
            GridView2.DataBind();






            GridView2.FooterRow.Cells[1].Text = "TOTAL";

            decimal milkkg = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("kg"));
            GridView2.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[2].Text = milkkg.ToString("0.00");

            double milkLTR = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("Ltr"));
            GridView2.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[3].Text = milkLTR.ToString("0.00");


            double CARTAGE = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("Cartage"));
            GridView2.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            GridView2.FooterRow.Cells[4].Text = CARTAGE.ToString("0.00");



            double SplBonus = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("SplBonus"));
            GridView2.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            GridView2.FooterRow.Cells[5].Text = SplBonus.ToString("0.00");



            double TotAmount = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("TotAmount"));
            GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            GridView2.FooterRow.Cells[6].Text = TotAmount.ToString("0.00");

            foreach (GridViewRow DRR in GridView2.Rows)
            {

                AvfRate = Convert.ToDouble(DRR.Cells[7].Text);
                AvfRateASS = AvfRateASS + AvfRate;

                double AvfRateASSAVG = AvfRateASS / IJKK;
                GridView2.FooterRow.Cells[7].Text = AvfRateASSAVG.ToString("F2");




                AvfFAT = Convert.ToDouble(DRR.Cells[8].Text);
                AvfFATASS = AvfFATASS + AvfFAT;
                double AvfRateASSFATAVG = AvfFATASS / IJKK;
                GridView2.FooterRow.Cells[8].Text = AvfRateASSFATAVG.ToString("F2");


                AvfSNF = Convert.ToDouble(DRR.Cells[9].Text);
                AvfSNFASS = AvfSNFASS + AvfSNF;
                double AvfRateASSSNAFVG = AvfSNFASS / IJKK;
                GridView2.FooterRow.Cells[9].Text = AvfRateASSSNAFVG.ToString("F2");











                AvfFATKG = Convert.ToDouble(DRR.Cells[10].Text);
                AvfFATKGASS = AvfFATKGASS + AvfFATKG;
                double AVGFATKGSUM = AvfFATKGASS;
                GridView2.FooterRow.Cells[10].Text = AVGFATKGSUM.ToString("F2");


                AvfSNFKG = Convert.ToDouble(DRR.Cells[11].Text);
                AvfSNFKGASS = AvfSNFKGASS + AvfSNFKG;
                double AVGFATSNFKGG = AvfSNFKGASS;
                GridView2.FooterRow.Cells[11].Text = AVGFATSNFKGG.ToString("F2");



                double getamount = Convert.ToDouble(GridView2.FooterRow.Cells[6].Text);
                double getfat = Convert.ToDouble(GridView2.FooterRow.Cells[10].Text);

                double gettotkgfat = (getamount / getfat);
                GridView2.FooterRow.Cells[12].Text = gettotkgfat.ToString("F2");
              

            

                IJK = IJK + 1;
                IJKK = IJKK + 1;

            }




        }
        catch
        {


        }
    }
    public void GRID2()
    {

        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string GETCOW;
            con = DB.GetConnection();
            // GETCOW = "select llll.Route_id,Route_Name,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,SUM(Ts) as Ts ,SUM(Rts) as Rts  from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as Cartage,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,(SUM(AvgFat) + SUM(Avgsnf)) as Ts ,convert(decimal(18,2),( Sum(AvfRate) / (SUM(AvgFat) + SUM(Avgsnf)))) as Rts from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code=168 AND Prdate BETWEEN '"+d1+"' AND '"+d2+"'  AND fat < 4.5  GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as llll left join(select Route_ID,Route_Name   from Route_Master where Plant_Code='"+pcode+"'  group by Route_ID,Route_Name ) as rm on llll.Route_id=rm.Route_ID group by llll.Route_id,Route_Name";
            //    GETCOW = "select lllll.Route_id,Route_Name,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code=168 AND Prdate BETWEEN '"+d1+"' AND '"+d2+"'  AND   fat >= 4.5   GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as lllll left join (select Route_ID,Route_Name   from route_master where plant_code='"+pcode+"' group by Route_ID,Route_Name) as rm on lllll.Route_id=rm.Route_ID group by lllll.Route_id,Route_Name";
            GETCOW = "select Route_Name,convert(decimal(18,2), SUM(kG)) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as Cartage,Sum(SplBonusAmount) as SplBonus,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg,convert(decimal(18,2),(SUM(TotAmount) /  SUM(fatkg))) as KgfatRate from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,SUM(fatkg) as Fatkg,SUM(snfkg) as Snfkg from (select Route_id,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,Sum(AvgFat) as AvgFat,sum(Avgsnf) as Avgsnf,convert(decimal(18,2),SUM(fatkg)) as Fatkg,convert(decimal(18,2),SUM(snfkg)) as Snfkg from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount, Sum(AvfRate ) as AvfRate ,convert(decimal(18,2),((SUM(FAT_KG)) / (SUM(kG)) *100 )) AS AvgFat,convert(decimal(18,2),((SUM(snf_kg)) / (SUM(kG)) *100)) AS Avgsnf,SUM(fat_kg) as Fatkg,SUM(snf_kg) as Snfkg   from (select Route_id ,SUM(kG) as kg,SUM(Ltr) as  Ltr,sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(TotAmount) as TotAmount,CONVERT(decimal(18,2), SUM(TotAmount) / sum(Ltr))   as AvfRate,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  from (SELECT Route_id,Sum(kG) as kG,Sum(LTR) as Ltr,Sum(Amt) as Amt,Sum(MR) as MR,Sum(CommRate) as CommRate,Sum(CartageAmount) as CartageAmount,Sum(SplBonusAmount) as SplBonusAmount,SUM(Amt + CommRate +  CartageAmount + SplBonusAmount ) AS TotAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg FROM (select Route_id, sUM(KG) AS kG,SUM(LTR) AS LTR,SUM(AMT) AS Amt,CONVERT(DECIMAL(18,2),(SUM(AMT) / SUM(ltr))) as MR,Sum(CommRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS  SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg    from (SELECT  Route_id,SUM(MILK_KG) AS KG,SUM(MILK_LTR) AS LTR,SUM(AMOUNT) AS AMT,Sum(ComRate) as CommRate,SUM(CartageAmount) AS CartageAmount,sUM(SplBonusAmount) AS SplBonusAmount,SUM(FAT_KG) AS FAT_KG,SUM(snf_kg) AS snf_kg  FROM   Procurement WHERE Plant_Code='"+pcode+"' AND Prdate BETWEEN '" + d1 + "' AND '" + d2 + "'   GROUP BY Route_id) as ahh GROUP BY Route_id) AS FF group by Route_id) as kk   group by  Route_id) as hh group by Route_id) as kkk group by Route_id) as aa group by Route_id) as lllll left join (select Route_ID,Route_Name   from route_master where plant_code='" + pcode + "' group by Route_ID,Route_Name) as rm on lllll.Route_id=rm.Route_ID group by lllll.Route_id,Route_Name";
            SqlCommand cmd = new SqlCommand(GETCOW, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(DTG);
            GridView2.DataSource = DTG.Tables[0];
            GridView2.DataBind();



            GridView2.FooterRow.Cells[1].Text = "TOTAL";
            decimal milkkg = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("kg"));
            GridView2.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[2].Text = milkkg.ToString("0.00");

            double milkLTR = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("Ltr"));
            GridView2.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            GridView2.FooterRow.Cells[3].Text = milkLTR.ToString("0.00");


            double CARTAGE = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("Cartage"));
            GridView2.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            GridView2.FooterRow.Cells[4].Text = CARTAGE.ToString("0.00");



            double SplBonus = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("SplBonus"));
            GridView2.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            GridView2.FooterRow.Cells[5].Text = SplBonus.ToString("0.00");



            double TotAmount = DTG.Tables[0].AsEnumerable().Sum(row => row.Field<double>("TotAmount"));
            GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            GridView2.FooterRow.Cells[6].Text = TotAmount.ToString("0.00");

            foreach (GridViewRow DRR in GridView2.Rows)
            {

                AvfRate = Convert.ToDouble(DRR.Cells[7].Text);
                AvfRateASS = AvfRateASS + AvfRate;

                double AvfRateASSAVG = AvfRateASS / IJKK;
                GridView2.FooterRow.Cells[7].Text = AvfRateASSAVG.ToString("F2");




                AvfFAT = Convert.ToDouble(DRR.Cells[8].Text);
                AvfFATASS = AvfFATASS + AvfFAT;
                double AvfRateASSFATAVG = AvfFATASS / IJKK;
                GridView2.FooterRow.Cells[8].Text = AvfRateASSFATAVG.ToString("F2");


                AvfSNF = Convert.ToDouble(DRR.Cells[9].Text);
                AvfSNFASS = AvfSNFASS + AvfSNF;
                double AvfRateASSSNAFVG = AvfSNFASS / IJKK;
                GridView2.FooterRow.Cells[9].Text = AvfRateASSSNAFVG.ToString("F2");











                AvfFATKG = Convert.ToDouble(DRR.Cells[10].Text);
                AvfFATKGASS = AvfFATKGASS + AvfFATKG;
                double AVGFATKGSUM = AvfFATKGASS;
                GridView2.FooterRow.Cells[10].Text = AVGFATKGSUM.ToString("F2");


                AvfSNFKG = Convert.ToDouble(DRR.Cells[11].Text);
                AvfSNFKGASS = AvfSNFKGASS + AvfSNFKG;
                double AVGFATSNFKGG = AvfSNFKGASS;
                GridView2.FooterRow.Cells[11].Text = AVGFATSNFKGG.ToString("F2");


                double getamount = Convert.ToDouble(GridView2.FooterRow.Cells[6].Text);
                double getfat = Convert.ToDouble(GridView2.FooterRow.Cells[10].Text);

                 double gettotkgfat = (getamount / getfat);
                GridView2.FooterRow.Cells[12].Text = gettotkgfat.ToString("F2");
              


                IJK = IJK + 1;
                IJKK = IJKK + 1;

            }


         

        }
        catch
        {


        }
    }


    public void  LoadPlantcode() 
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
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }



    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddl_Plantname.SelectedItem.Text + ":Cow Milk Detils:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2.ColumnSpan = 14;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddl_Plantname.SelectedItem.Text + ":Buffalo Milk Detils:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2.ColumnSpan = 13;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}