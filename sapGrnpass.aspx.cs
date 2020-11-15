using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class sapGrnpass : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DbHelper DB = new DbHelper();
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    SqlConnection con1 = new SqlConnection();
    DataSet DTG = new DataSet();
    DataTable availornot = new DataTable();
    DataTable sapgrn = new DataTable();
    DataTable sapinvoice = new DataTable();
    DataTable checkdatagrn = new DataTable();
    DataTable checkdatainvoice = new DataTable();
    DataTable getdatefiled = new DataTable();
    DataTable sapexistornotgrn = new DataTable();
    DataTable sapexistornoinvoice = new DataTable();
    DataTable checkgrn = new DataTable();
    DataTable checkinvoce = new DataTable();
    int serialcountgrn=0;
    int serialcountinvoice=0;
    string Dates;
    int CART, SPL,comm;
    double tcart, tspl, tcomm;
    SqlCommand cmdapinsert = new SqlCommand();
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
                    pcode = ddl_Plantname.SelectedItem.Value;
                    lbltodate.Visible = true;
                    txt_todate.Visible = true;
                    //  billdate();
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
    public void grn()
    {
        try
        {
            getgrn();
            if (availornot.Rows.Count < 1)
            {

                DateTime dt1 = new DateTime();
                  DateTime dt2 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
                string d1 = dt1.ToString("MM/dd/yyyy");
                 string d2 = dt2.ToString("MM/dd/yyyy");
                string getplantcode;
                con = DB.GetConnection();
            //    getplantcode = "Select  Createdate,CardCode,cardname,Taxdate,DocDate,DueDate,ReferenceNoSno,Itemcode,Descriptions,Whscode,Quantity,OcrCode,OcerCode1,QuantityKgs,Snf,Fat,Clr,Sessions,B1Upload,Processed  from  saptransaction  WHERE Createdate ='" + d1 + "'   and sessions='" + ddl_shift.SelectedItem.Text + "'    and plant_code='" + ddl_Plantname.SelectedItem.Value + "'      order by  RAND(Sno) asc ";

                getplantcode = "Select  Createdate,CardCode,cardname,Taxdate,DocDate,DueDate,ReferenceNoSno,Itemcode,Descriptions,Whscode,Quantity,OcrCode,OcerCode1,QuantityKgs,Snf,Fat,Clr,Sessions,B1Upload,Processed  from  saptransaction  WHERE Createdate BETWEEN '" + d1 + "'  AND '"+ d2+"'     and plant_code='" + ddl_Plantname.SelectedItem.Value + "'      order by  RAND(Sno) asc ";
                SqlCommand cmd = new SqlCommand(getplantcode, con);
                SqlDataAdapter dws = new SqlDataAdapter(cmd);
                sapgrn.Rows.Clear();
                dws.Fill(sapgrn);
                string Createdate, CardCode, cardname, Taxdate, DocDate, DueDate, ReferenceNoSno, Itemcode, Descriptions, Whscode, Quantity, OcrCode, OcerCode1, QuantityKgs, Snf, Fat, Clr, Sessions, B1Upload, Processed;
                foreach (DataRow dio in sapgrn.Rows)
                {
                    Createdate = dio[0].ToString();
                    CardCode = dio[1].ToString();
                    cardname = dio[2].ToString();
                    Taxdate = dio[3].ToString();
                    DocDate = dio[4].ToString();
                    DueDate = dio[5].ToString();
                    ReferenceNoSno = dio[6].ToString();
                    Itemcode = dio[7].ToString();
                    Descriptions = dio[8].ToString();
                    Whscode = dio[9].ToString();
                    Quantity = dio[10].ToString();
                    OcrCode = dio[11].ToString();
                    OcerCode1 = dio[12].ToString();
                    QuantityKgs = dio[13].ToString();
                    Snf = dio[14].ToString();
                    Fat = dio[15].ToString();
                    Clr = dio[16].ToString();
                    Sessions = dio[17].ToString();
                    B1Upload = dio[18].ToString();
                    Processed = dio[19].ToString();
                    string insertgrn;
                    //if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "162") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "169"))
                    if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                    {
                        insertgrn = "insert  into EMROPDN(Createdate,CardCode,cardname,Taxdate,DocDate,DocDueDate,ReferenceNo,Itemcode,Dscription,Whscode,Quantity,OcrCode,OcrCode2,Quantity_Kgs,Snf,Fat,Clr,Session,B1Upload,Processed,U_Status,PURCHASETYPE,company) values ('" + Createdate + "','" + CardCode + "','" + cardname + "','" + Taxdate + "','" + DocDate + "','" + DueDate + "','" + ReferenceNoSno + "','" + Itemcode + "','" + Descriptions + "','" + Whscode + "','" + Quantity + "','" + OcrCode + "','" + OcerCode1 + "','" + QuantityKgs + "','" + Snf + "','" + Fat + "','" + Clr + "','" + Sessions + "','N','N','" + ddl_Plantname.SelectedItem.Value + "','219','SVDS')";
                    }
                    else
                    {
                        insertgrn = "insert  into EMROPDN(Createdate,CardCode,cardname,Taxdate,DocDate,DocDueDate,ReferenceNo,Itemcode,Dscription,Whscode,Quantity,OcrCode,OcrCode2,Quantity_Kgs,Snf,Fat,Clr,Session,B1Upload,Processed,U_Status,PURCHASETYPE,company) values ('" + Createdate + "','" + CardCode + "','" + cardname + "','" + Taxdate + "','" + DocDate + "','" + DueDate + "','" + ReferenceNoSno + "','" + Itemcode + "','" + Descriptions + "','" + Whscode + "','" + Quantity + "','" + OcrCode + "','" + OcerCode1 + "','" + QuantityKgs + "','" + Snf + "','" + Fat + "','" + Clr + "','" + Sessions + "','N','N','" + ddl_Plantname.SelectedItem.Value + "','219','SVD')";
                    }
                    con1 = DB.GetConnectionSAP();
                    SqlCommand cmd12 = new SqlCommand(insertgrn, con1);
                    cmd12.ExecuteNonQuery();
                    serialcountgrn = serialcountgrn + 1;
                }

              
                string NEWS = serialcountgrn + "" + "Record Saved SuccessFully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(NEWS) + "')</script>");
            }
            else
            {
                string mss = "Data Already inserted";

                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
            }
        }
        catch
        {

        }
    }
    public void Apinvoice()
    {
        try
        {
            getinvoiceaavail();
            if (availornot.Rows.Count < 1)
            {

                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
                string d1 = dt1.ToString("MM/dd/yyyy");
                string d2 = dt2.ToString("MM/dd/yyyy");
                //getdate();
                //Dates = getdatefiled.Rows[0][0].ToString();
                string getplantcode;
                con = DB.GetConnection();
                getplantcode = "Select  convert(varchar,Createdate,101) as Createdate,CardCode,cardname,Taxdate,DocDate,DueDate,ReferenceNoSno,ReferenceNo,Itemcode,Descriptions,Whscode,Quantity,OcrCode,OcerCode1,QuantityKgs,Snf,Fat,Clr,Rate,Cartage,SplBon,Commissions,Sessions,B1Upload,Processed  from  saptransaction     where Createdate between '" + d1 + "'   and  '" + d2 + "'   and plant_code='" + ddl_Plantname.SelectedItem.Value + "'     order by  RAND(Sno) asc";
                SqlCommand cmd = new SqlCommand(getplantcode, con);
                SqlDataAdapter dws = new SqlDataAdapter(cmd);
                sapinvoice.Rows.Clear();
                dws.Fill(sapinvoice);
                string apCreatedate, apCardCode, apcardname, apTaxdate, apDocDate, apDueDate, apReferenceNoSno, apReferenceNo, apItemcode, apDescriptions, apWhscode, apQuantity, apOcrCode, apOcerCode1, apQuantityKgs, apSnf, apFat, apClr, apRate, apCartage, apSplBon, apCommissions, apSessions, apB1Upload, apProcessed;
                string insertinvoice;
                foreach (DataRow inv in sapinvoice.Rows)
                {
                    //apCreatedate = inv[0].ToString();
                    apCreatedate = inv[0].ToString();            //"03-20-2017";
                    string check1;
                    con = DB.GetConnection();
                    check1 = "seLECT convert(varchar,Bill_todate,101) as todate FROM Bill_date WHERE plant_code=" + ddl_Plantname.SelectedItem.Value + " and '" + apCreatedate + "' between Bill_frmdate and  Bill_todate";
                    SqlCommand cmd1 = new SqlCommand(check1, con);
                    string todate = cmd1.ExecuteScalar().ToString();
                    apCardCode = inv[1].ToString();
                    apcardname = inv[2].ToString();
                    apTaxdate = inv[3].ToString();
                    apDocDate = todate;  //inv[4].ToString();
                    apDueDate = inv[5].ToString();
                    apReferenceNoSno = inv[6].ToString();
                    apReferenceNo = inv[7].ToString();
                    apItemcode = inv[8].ToString();
                    apDescriptions = inv[9].ToString();
                    apWhscode = inv[10].ToString();
                    apQuantity = inv[11].ToString();
                    apOcrCode = inv[12].ToString();
                    apOcerCode1 = inv[13].ToString();
                    apQuantityKgs = inv[14].ToString();
                    apSnf = inv[15].ToString();
                    apFat = inv[16].ToString();
                    apClr = inv[17].ToString();
                    apRate = inv[18].ToString();
                    apCartage = inv[19].ToString();

                    //decimal param;
                    //param.Value = DBNull.Value;
                      

                    try
                    {
                        double tCartage = Convert.ToDouble(apCartage);

                        if (tCartage == 0)
                        {
                            apCartage = DBNull.Value.ToString();
                            CART = 1;
                        }
                        else
                        {

                            apCartage = tCartage.ToString("f2");
                        }
                    }
                    catch
                    {

                        apCartage = DBNull.Value.ToString();
                        CART = 1;
                    }

                    apSplBon = inv[20].ToString();


                    try
                    {
                        double tapSplBon = Convert.ToDouble(apSplBon);

                        if (tapSplBon == 0)
                        {
                            apSplBon = DBNull.Value.ToString();
                            SPL = 1;
                        }
                        else
                        {
                            apSplBon = tapSplBon.ToString("f2");
                        }
                    }
                    catch
                    {

                        apSplBon = DBNull.Value.ToString();
                        SPL = 1;
                    }

                    apCommissions = inv[21].ToString();
                    try
                    {
                        double tapCommissions = Convert.ToDouble(apCommissions);

                        if (tapCommissions == 0)
                        {
                            apCommissions = DBNull.Value.ToString();
                            comm = 1;
                        }
                        else
                        {
                            apCommissions = tapCommissions.ToString("f2");
                        }
                        
                    }
                    catch
                    {

                        apCommissions = DBNull.Value.ToString();
                        comm = 1;
                    }

                    apSessions = inv[22].ToString();
                    apB1Upload = inv[23].ToString();
                    apProcessed = inv[24].ToString();
                    if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                    //if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "162") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "169"))
                    {
                       
                       // insertinvoice = "insert into EMROPCH(Createdate,CardCode,cardname,Taxdate,DocDate,DocDueDate,GRNRefNo,ReferenceNo,Itemcode,Dscription,Whscode,Quantity,OcrCode,OcrCode2,Quantity_Kgs,Snf,Fat,Clr,Price,CartageAmount2,SpecialBonus3,Incentive4,Session,B1Upload,Processed,TaxCode,U_Status,D_Date,PURCHASETYPE) values('" + apCreatedate + "','" + apCardCode + "','" + apcardname + "','" + apTaxdate + "','" + apDocDate + "','" + apDueDate + "','" + apReferenceNoSno + "','" + apReferenceNo + "','" + apItemcode + "','" + apDescriptions + "','" + apWhscode + "','" + apQuantity + "','" + apOcrCode + "','" + apOcerCode1 + "','" + apQuantityKgs + "','" + apSnf + "','" + apFat + "','" + apClr + "','" + apRate + "','" + DBNull.Value + "','" + apSplBon + "','" + apCommissions + "','" + apSessions + "','N','N','EXEMPT','" + ddl_Plantname.SelectedItem.Value + "','" + apTaxdate + "','9')";
                         insertinvoice = "insert into EMROPCH(Createdate,CardCode,cardname,Taxdate,DocDate,DocDueDate,GRNRefNo,ReferenceNo,Itemcode,Dscription,Whscode,Quantity,OcrCode,OcrCode2,Quantity_Kgs,Snf,Fat,Clr,Price,CartageAmount2,SpecialBonus3,Incentive4,Session,B1Upload,Processed,TaxCode,U_Status,D_Date,PURCHASETYPE) values(@Createdate,@CardCode,@cardname,@Taxdate,@DocDate,@DocDueDate,@GRNRefNo,@ReferenceNo,@Itemcode,@Dscription,@Whscode,@Quantity,@OcrCode,@OcrCode2,@Quantity_Kgs,@Snf,@Fat,@Clr,@Price,@CartageAmount2,@SpecialBonus3,@Incentive4,@Session,@B1Upload,@Processed,@TaxCode,@U_Status,@D_Date,@PURCHASETYPE)";
                         con1 = DB.GetConnectionSAP();
                         cmdapinsert = new SqlCommand(insertinvoice, con1);
                       //  cmd.Parameters.Add("bar", SqlDbType.NVarChar, 30).Value = bar;
                       //  cmd.Parameters.AddWithValue("@Createdate", SqlDbType.Date,apCreatedate);
                         cmdapinsert.Parameters.Add("@Createdate", apCreatedate);
                         cmdapinsert.Parameters.AddWithValue("@CardCode", apCardCode);
                         cmdapinsert.Parameters.AddWithValue("@cardname", apcardname);
                         cmdapinsert.Parameters.AddWithValue("@Taxdate", apTaxdate);
                         cmdapinsert.Parameters.AddWithValue("@DocDate", apDocDate);
                         cmdapinsert.Parameters.AddWithValue("@DocDueDate", apDueDate);
                         cmdapinsert.Parameters.AddWithValue("@GRNRefNo", apReferenceNoSno);
                         cmdapinsert.Parameters.AddWithValue("@ReferenceNo", apReferenceNo);
                         cmdapinsert.Parameters.AddWithValue("@Itemcode", apItemcode);
                         cmdapinsert.Parameters.AddWithValue("@Dscription", apDescriptions);
                         cmdapinsert.Parameters.AddWithValue("@Whscode", apWhscode);
                         cmdapinsert.Parameters.AddWithValue("@Quantity", apQuantity);
                         cmdapinsert.Parameters.AddWithValue("@OcrCode", apOcrCode);
                         cmdapinsert.Parameters.AddWithValue("@OcrCode2", apOcerCode1);
                         cmdapinsert.Parameters.AddWithValue("@Quantity_Kgs", apQuantityKgs);
                         cmdapinsert.Parameters.AddWithValue("@Snf", apSnf);
                         cmdapinsert.Parameters.AddWithValue("@Fat", apFat);
                         cmdapinsert.Parameters.AddWithValue("@Clr", apClr);
                         cmdapinsert.Parameters.AddWithValue("@Price", apRate);
                     
                         try
                         {
                             tcart = Convert.ToDouble(apCartage);
                             cmdapinsert.Parameters.AddWithValue("@CartageAmount2", apCartage);
                         }
                         catch
                         {
                             cmdapinsert.Parameters.AddWithValue("@CartageAmount2", DBNull.Value);
                         }
                         try
                         {
                             tspl = Convert.ToDouble(apSplBon);
                             cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", apSplBon);
                         }
                         catch
                         {
                             cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", DBNull.Value);
                         }
                         try
                         {
                             tcomm = Convert.ToDouble(apCommissions);
                             cmdapinsert.Parameters.AddWithValue("@Incentive4", apCommissions);
                         }
                         catch
                         {

                             cmdapinsert.Parameters.AddWithValue("@Incentive4", DBNull.Value);

                         }

                         //if (tcart > 0)
                         //{
                         //    cmdapinsert.Parameters.AddWithValue("@CartageAmount2", apCartage);
                         //}
                         //else
                         //{
                         //    cmdapinsert.Parameters.AddWithValue("@CartageAmount2", DBNull.Value);


                         //}
                         //if (tspl > 0)
                         //{
                         //    cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", apSplBon);
                         //}
                         //else
                         //{
                         //    cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", DBNull.Value);

                         //}
                         //if (tcomm > 0)
                         //{
                         //    cmdapinsert.Parameters.AddWithValue("@Incentive4", apCommissions);
                         //}
                         //else
                         //{
                         //    cmdapinsert.Parameters.AddWithValue("@Incentive4", DBNull.Value);

                         //}


                         cmdapinsert.Parameters.AddWithValue("@Session", apSessions);
                         cmdapinsert.Parameters.AddWithValue("@B1Upload", 'N');
                         cmdapinsert.Parameters.AddWithValue("@Processed", 'N');
                         cmdapinsert.Parameters.AddWithValue("@TaxCode", "EXEMPT");
                         cmdapinsert.Parameters.AddWithValue("@U_Status", ddl_Plantname.SelectedItem.Value);
                         cmdapinsert.Parameters.AddWithValue("@D_Date", apTaxdate);
                         cmdapinsert.Parameters.AddWithValue("@PURCHASETYPE", "211");
                       
                        //// SqlCommand cmd = new SqlCommand(insertinvoice, con1);
                         cmdapinsert.ExecuteNonQuery();
                         serialcountinvoice = serialcountinvoice + 1;

                    //    cmd.Parameters.AddWithValue("@Value1", if(string.IsNullOrEmpty(apCartage), DBNull.Value, apCartage));
                    }
                    else
                    {
                        //insertinvoice = "insert into EMROPCH(Createdate,CardCode,cardname,Taxdate,DocDate,DocDueDate,GRNRefNo,ReferenceNo,Itemcode,Dscription,Whscode,Quantity,OcrCode,OcrCode2,Quantity_Kgs,Snf,Fat,Clr,Price,CartageAmount2,SpecialBonus3,Incentive4,Session,B1Upload,Processed,TaxCode,U_Status,D_Date,PURCHASETYPE) values('" + apCreatedate + "','" + apCardCode + "','" + apcardname + "','" + apTaxdate + "','" + apDocDate + "','" + apDueDate + "','" + apReferenceNoSno + "','" + apReferenceNo + "','" + apItemcode + "','" + apDescriptions + "','" + apWhscode + "','" + apQuantityKgs + "','" + apOcrCode + "','" + apOcerCode1 + "','" + apQuantity + "','" + apSnf + "','" + apFat + "','" + apClr + "','" + apRate + "','" + apCartage + "','" + apSplBon + "','" + apCommissions + "','" + apSessions + "','N','N','EXEMPT','" + ddl_Plantname.SelectedItem.Value + "','" + apTaxdate + "','9')";


                        // insertinvoice = "insert into EMROPCH(Createdate,CardCode,cardname,Taxdate,DocDate,DocDueDate,GRNRefNo,ReferenceNo,Itemcode,Dscription,Whscode,Quantity,OcrCode,OcrCode2,Quantity_Kgs,Snf,Fat,Clr,Price,CartageAmount2,SpecialBonus3,Incentive4,Session,B1Upload,Processed,TaxCode,U_Status,D_Date,PURCHASETYPE) values('" + apCreatedate + "','" + apCardCode + "','" + apcardname + "','" + apTaxdate + "','" + apDocDate + "','" + apDueDate + "','" + apReferenceNoSno + "','" + apReferenceNo + "','" + apItemcode + "','" + apDescriptions + "','" + apWhscode + "','" + apQuantity + "','" + apOcrCode + "','" + apOcerCode1 + "','" + apQuantityKgs + "','" + apSnf + "','" + apFat + "','" + apClr + "','" + apRate + "','" + DBNull.Value + "','" + apSplBon + "','" + apCommissions + "','" + apSessions + "','N','N','EXEMPT','" + ddl_Plantname.SelectedItem.Value + "','" + apTaxdate + "','9')";
                        insertinvoice = "insert into EMROPCH(Createdate,CardCode,cardname,Taxdate,DocDate,DocDueDate,GRNRefNo,ReferenceNo,Itemcode,Dscription,Whscode,Quantity,OcrCode,OcrCode2,Quantity_Kgs,Snf,Fat,Clr,Price,CartageAmount2,SpecialBonus3,Incentive4,Session,B1Upload,Processed,TaxCode,U_Status,D_Date,PURCHASETYPE) values(@Createdate,@CardCode,@cardname,@Taxdate,@DocDate,@DocDueDate,@GRNRefNo,@ReferenceNo,@Itemcode,@Dscription,@Whscode,@Quantity,@OcrCode,@OcrCode2,@Quantity_Kgs,@Snf,@Fat,@Clr,@Price,@CartageAmount2,@SpecialBonus3,@Incentive4,@Session,@B1Upload,@Processed,@TaxCode,@U_Status,@D_Date,@PURCHASETYPE)";
                        con1 = DB.GetConnectionSAP();
                        cmdapinsert = new SqlCommand(insertinvoice, con1);
                        //  cmd.Parameters.Add("bar", SqlDbType.NVarChar, 30).Value = bar;
                        //  cmd.Parameters.AddWithValue("@Createdate", SqlDbType.Date,apCreatedate);
                        cmdapinsert.Parameters.Add("@Createdate", apCreatedate);
                        cmdapinsert.Parameters.AddWithValue("@CardCode", apCardCode);
                        cmdapinsert.Parameters.AddWithValue("@cardname", apcardname);
                        cmdapinsert.Parameters.AddWithValue("@Taxdate", apTaxdate);
                        cmdapinsert.Parameters.AddWithValue("@DocDate", apDocDate);
                        cmdapinsert.Parameters.AddWithValue("@DocDueDate", apDueDate);
                        cmdapinsert.Parameters.AddWithValue("@GRNRefNo", apReferenceNoSno);
                        cmdapinsert.Parameters.AddWithValue("@ReferenceNo", apReferenceNo);
                        cmdapinsert.Parameters.AddWithValue("@Itemcode", apItemcode);
                        cmdapinsert.Parameters.AddWithValue("@Dscription", apDescriptions);
                        cmdapinsert.Parameters.AddWithValue("@Whscode", apWhscode);
                        cmdapinsert.Parameters.AddWithValue("@Quantity",apQuantity  );
                        cmdapinsert.Parameters.AddWithValue("@OcrCode", apOcrCode);
                        cmdapinsert.Parameters.AddWithValue("@OcrCode2", apOcerCode1);
                        cmdapinsert.Parameters.AddWithValue("@Quantity_Kgs", apQuantityKgs);
                        cmdapinsert.Parameters.AddWithValue("@Snf", apSnf);
                        cmdapinsert.Parameters.AddWithValue("@Fat", apFat);
                        cmdapinsert.Parameters.AddWithValue("@Clr", apClr);
                        cmdapinsert.Parameters.AddWithValue("@Price", apRate);

                        ////try
                        ////{
                        ////    tcart = Convert.ToDouble(apCartage);
                        ////}
                        ////catch
                        ////{
                        ////}
                        ////try
                        ////{
                        ////    tspl = Convert.ToDouble(apSplBon);
                        ////}
                        ////catch
                        ////{

                        ////}
                        ////try
                        ////{
                        ////    tcomm = Convert.ToDouble(apCommissions);
                        ////}
                        ////catch
                        ////{



                        ////}

                        ////if (tcart > 0)
                        ////{
                        ////    cmdapinsert.Parameters.AddWithValue("@CartageAmount2", apCartage);
                        ////}
                        ////else
                        ////{
                        ////    cmdapinsert.Parameters.AddWithValue("@CartageAmount2", DBNull.Value);


                        ////}
                        ////if (tspl > 0)
                        ////{
                        ////    cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", apSplBon);
                        ////}
                        ////else
                        ////{
                        ////    cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", DBNull.Value);

                        ////}
                        ////if (tcomm > 0)
                        ////{
                        ////    cmdapinsert.Parameters.AddWithValue("@Incentive4", apCommissions);
                        ////}
                        ////else
                        ////{
                        ////    cmdapinsert.Parameters.AddWithValue("@Incentive4", DBNull.Value);

                        ////}

                        try
                        {
                            tcart = Convert.ToDouble(apCartage);
                            cmdapinsert.Parameters.AddWithValue("@CartageAmount2", apCartage);
                        }
                        catch
                        {
                            cmdapinsert.Parameters.AddWithValue("@CartageAmount2", DBNull.Value);
                        }
                        try
                        {
                            tspl = Convert.ToDouble(apSplBon);
                            cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", apSplBon);
                        }
                        catch
                        {
                            cmdapinsert.Parameters.AddWithValue("@SpecialBonus3", DBNull.Value);
                        }
                        try
                        {
                            tcomm = Convert.ToDouble(apCommissions);
                            cmdapinsert.Parameters.AddWithValue("@Incentive4", apCommissions);
                        }
                        catch
                        {

                            cmdapinsert.Parameters.AddWithValue("@Incentive4", DBNull.Value);
                        }
                        cmdapinsert.Parameters.AddWithValue("@Session", apSessions);
                        cmdapinsert.Parameters.AddWithValue("@B1Upload", 'N');
                        cmdapinsert.Parameters.AddWithValue("@Processed", 'N');
                        cmdapinsert.Parameters.AddWithValue("@TaxCode", "EXEMPT");
                        cmdapinsert.Parameters.AddWithValue("@U_Status", ddl_Plantname.SelectedItem.Value);
                        cmdapinsert.Parameters.AddWithValue("@D_Date", apTaxdate);
                        cmdapinsert.Parameters.AddWithValue("@PURCHASETYPE", "211");
                        //// SqlCommand cmd = new SqlCommand(insertinvoice, con1);
                        cmdapinsert.ExecuteNonQuery();
                        serialcountinvoice = serialcountinvoice + 1;

                    }
                    //con1 = DB.GetConnectionSAP();
                    //SqlCommand cmd12 = new SqlCommand(insertinvoice, con1);
                    //cmd12.ExecuteNonQuery();
                    //serialcountinvoice = serialcountinvoice + 1;
                 }
               
                string mss1 = serialcountinvoice + "" + "Record Saved SuccessFully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss1) + "')</script>");
            }
            else
            {
                string mss = "Data Already inserted";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

            }
        }
        catch
        {

       }
    }
    //public void getDocDate(string fromdate)
    //{
    //    try
    //    {
    //        DateTime dtfromdate = new DateTime();
    //        dtfromdate = DateTime.ParseExact(fromdate, "dd/MM/yyyy", null);
    //        string d_from_date = dtfromdate.ToString("MM/dd/yyyy");
    //        string check1;
    //        con1 = DB.GetConnectionSAP();
    //        check1 = "seLECT convert(varchar,Bill_todate,101) as todate FROM Bill_date WHERE plant_code=" + ddl_Plantname.SelectedItem.Value + " and '" + d_from_date + "' between Bill_frmdate and  Bill_todate";
    //        SqlCommand cmd = new SqlCommand(check1, con1);
    //        SqlDataReader reader = cmd.ExecuteReader();
    //        string todate = reader["todate"].ToString();
    //        //SqlDataAdapter av = new SqlDataAdapter(cmd);
    //        //availornot.Rows.Clear();
    //        //av.Fill(availornot);
    //    }
    //    catch
    //    { 
        
    //    }
    //}
    public void getinvoiceaavail()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string check;
            con1 = DB.GetConnectionSAP();
          //  check = "Select    *   from EMROPCH   where    Taxdate='" + d1 + "'  and session='" + ddl_shift.SelectedItem.Text + "'    AND U_Status='" + ddl_Plantname.SelectedItem.Value + "' ";
            check = "Select    *   from EMROPCH   where    CreateDate BETWEEN '" + d1 + "'  AND '" + d2 + "'  AND      U_Status='" + ddl_Plantname.SelectedItem.Value + "' ";
            SqlCommand cmd = new SqlCommand(check, con1);
            SqlDataAdapter av = new SqlDataAdapter(cmd);
            availornot.Rows.Clear();
            av.Fill(availornot);

        }
        catch
        {


        }
    }
    public void getgrn()
    {
        try
        {
            //DateTime dt1 = new DateTime();
            //dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            ////dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            //string d1 = dt1.ToString("MM/dd/yyyy");

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            string check;
            con1 = DB.GetConnectionSAP();
          //  check = "Select    *   from EMROPDN   where    CreateDate='" + d1 + "'  and session='" + ddl_shift.SelectedItem.Text + "'  AND U_Status='"+ddl_Plantname.SelectedItem.Value+"' ";
            check = "Select    *   from EMROPDN   where    CreateDate BETWEEN '" + d1 + "'  and '"+d2+"'  AND U_Status='" + ddl_Plantname.SelectedItem.Value + "' ";
            SqlCommand cmd = new SqlCommand(check, con1);
            SqlDataAdapter av = new SqlDataAdapter(cmd);
            availornot.Rows.Clear();
            av.Fill(availornot);

        }
        catch
        {


        }
    }
    public void getdate()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string dateempty = "";
            con = DB.GetConnection();
            dateempty = "Select   convert(varchar,Bill_todate,101) as ToDate     from      Bill_date  where plant_code='" + pcode + "'    AND   Bill_frmdate   between '" + d1 + "'  and  '" + d1 + "'";
            SqlCommand cmdcss = new SqlCommand(dateempty, con);
            SqlDataAdapter dcc = new SqlDataAdapter(cmdcss);
            dcc.Fill(getdatefiled);
        }
        catch
        {

        }

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
      
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        getcheckdatagrninvoice();
        if (checkdatainvoice.Rows.Count > 0)
        {
            try
            {
                if (rdo_btn.SelectedItem.Value == "1")
                {
                    existornotgrninstaging();
                    if (sapexistornotgrn.Rows.Count > 0)
                    {
                         string mss = "Already Data Inserted";
                         Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
                      
                    }
                    else
                    {
                          grn();
                    }
                }
                else
                {
                    existornotinvoiceinstaging();
                    if (sapexistornoinvoice.Rows.Count > 0)
                    {
                        string mss = "Already Data Inserted";
                        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
                    }
                    else
                    {
                        Apinvoice();
                        sapgrrn();
                        sapappinvoice();
                        if (checkgrn.Rows.Count != checkinvoce.Rows.Count)
                        {
                            DELETEOPCH();
                        }
                       

                    }
                }
            }
            catch
            {

            }
        }
        else
        {
            string mss = "No Data Particular Data";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    public void getcheckdatagrninvoice()
    {
        string strr;
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
        strr = "Select  *    from    saptransaction  where   plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and  Createdate between '" + d1 + "' and '" + d2 + "'   ";
         con = DB.GetConnection();
         SqlCommand cmd = new SqlCommand(strr, con);
         SqlDataAdapter dsp = new SqlDataAdapter(cmd);
         checkdatainvoice.Rows.Clear();
         dsp.Fill(checkdatainvoice);

    }
    public void existornotgrninstaging()
    {
        string strr = "";
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");
     //   strr = "Select  *    from    EMROPDN  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  D_Date='" + d1 + "'   and SESSION='" + ddl_shift.SelectedItem.Text + "'";
        strr = "Select  *    from    EMROPDN  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  D_Date BETWEEN '" + d1 + "'   and '"+d2+"'";
        con1 = DB.GetConnectionSAP();
        SqlCommand cmd = new SqlCommand(strr, con1);
        SqlDataAdapter grndsp = new SqlDataAdapter(cmd);
        sapexistornotgrn.Rows.Clear();
        grndsp.Fill(sapexistornotgrn);

    }

    public void existornotinvoiceinstaging()
    {
        string strr = "";
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt1.ToString("MM/dd/yyyy");
        //strr = "Select  *    from    EMROPCH  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  D_Date='" + d1 + "'   and SESSION='" + ddl_shift.SelectedItem.Text + "'";
        strr = "Select  *    from    EMROPCH  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  Createdate between '" + d1 + "' and '" + d2 + "' ";
        con1 = DB.GetConnectionSAP();
        SqlCommand cmd = new SqlCommand(strr, con1);
        SqlDataAdapter dspsap = new SqlDataAdapter(cmd);
        sapexistornoinvoice.Rows.Clear();
        dspsap.Fill(sapexistornoinvoice);

    }

    protected void rdo_btn_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (rdo_btn.SelectedItem.Value == "2")
        {
            lbltodate.Visible = true;
            txt_todate.Visible = true;
            ddl_shift.Visible = false;

        }
        if (rdo_btn.SelectedItem.Value == "1")
        {
            ddl_shift.Visible = true;
            lbltodate.Visible = true;
            txt_todate.Visible = true;
        }
    }
    public void sapgrrn()
    {
        string strr = "";
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt1.ToString("MM/dd/yyyy");
        //strr = "Select  *    from    EMROPCH  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  D_Date='" + d1 + "'   and SESSION='" + ddl_shift.SelectedItem.Text + "'";
        strr = "Select  *    from    EMROPDN  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  Createdate between '" + d1 + "' and '" + d2 + "' ";
        con1 = DB.GetConnectionSAP();
        SqlCommand cmd = new SqlCommand(strr, con1);
        SqlDataAdapter chgrn = new SqlDataAdapter(cmd);
        checkgrn.Rows.Clear();
        chgrn.Fill(checkgrn);
    }
    public void sapappinvoice()
    {
        string strr = "";
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt1.ToString("MM/dd/yyyy");
        //strr = "Select  *    from    EMROPCH  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  D_Date='" + d1 + "'   and SESSION='" + ddl_shift.SelectedItem.Text + "'";
        strr = "Select  *    from    EMROPCH  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  Createdate between '" + d1 + "' and '" + d2 + "' ";
        con1 = DB.GetConnectionSAP();
        SqlCommand cmd = new SqlCommand(strr, con1);
        SqlDataAdapter chap = new SqlDataAdapter(cmd);
        checkinvoce.Rows.Clear();
        chap.Fill(checkinvoce);

    }
    public void DELETEOPCH()
    {
        string strr = "";
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt1.ToString("MM/dd/yyyy");
        //strr = "Select  *    from    EMROPCH  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  D_Date='" + d1 + "'   and SESSION='" + ddl_shift.SelectedItem.Text + "'";
        strr = "DELETE  from    EMROPCH  where   U_Status='" + ddl_Plantname.SelectedItem.Value + "'   and  Createdate between '" + d1 + "' and '" + d2 + "' ";
        con1 = DB.GetConnectionSAP();
        SqlCommand cmd = new SqlCommand(strr, con1);
        cmd.ExecuteNonQuery();

    }
}