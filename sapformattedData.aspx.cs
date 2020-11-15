using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class sapformattedData : System.Web.UI.Page
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
    DataTable sapmrn = new DataTable();
    DataTable charc = new DataTable();
    DataTable snoo = new DataTable();
    DataTable availornot = new DataTable();
    DataTable missingagent = new DataTable();
    DataTable sapmissvendor = new DataTable();
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
    string month;
    int Checkstatus;
    string Tallyno;
    string BILLDATE;
    double ltr;
    string conltr;
    string days;
    //int sno;

    long sno;
    int serialcount;
    string vendorcheck;
    string agents;
    int missagent;
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
    protected void Button7_Click(object sender, EventArgs e)
    {
        getmissingvendorlist();
        if (missingagent.Rows.Count > 0)
        {
            string mss = missingagent.Rows.Count + ":" + "Some Agent Not Having Vendor Code";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

            GridView3.DataSource = missingagent;
            GridView3.DataBind();

           // getagentprocurementdetails();
        }
        else
        {
            GridView3.DataSource = null;
            GridView3.DataBind();
            getagentprocurementdetails();
        }
    }

    public void getcheckdataavail()
    {
        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string check;
            con = DB.GetConnection();
            check = "Select    *   from saptransaction   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  and Createdate='" + d1 + "'  and sessions='" + ddl_shift.SelectedItem.Text + "' ";
            SqlCommand cmd = new SqlCommand(check, con);
            SqlDataAdapter av = new SqlDataAdapter(cmd);
            availornot.Rows.Clear();
            av.Fill(availornot);

        }
        catch
        {


        }
    }



    public void getagentprocurementdetails()
    {
        getcheckdataavail();
        if (availornot.Rows.Count < 1)
        {

            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string getplantcode;
            con = DB.GetConnection();
            getplantcode = "Select proagent,prdate,Sessions,days,month,year,milkltr,MilkKg,Fat,Snf,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Plant_Name,WHcode,Agent_Name,VendorCode,clr,Refenrence   from (Select proagent,prdate,Sessions,days,month,year,MilkKg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Agent_Name,VendorCode,clr,Refenrence  from (sELECT proagent,prdate,Sessions,convert(varchar,days) as days,convert(varchar,month) as month,convert(varchar,year) as year,milkkg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Agent_Name,VendorCode,Plant_Code,clr,Refenrence   FROM (sELECT *    FROM (Select   Tid, convert(varchar,Agent_id,0) AS proagent,convert(varchar,Prdate,101) as prdate,Sessions,DATEPART(day,Prdate) as days ,DATEPART(MONTH,Prdate) as month,FORMAT(prdate,'yy') as year,Sum(Fat) as Fat,Sum(Snf) as Snf,Sum(Milk_kg) as MilkKg,Sum(milk_ltr) as   milkltr,Sum(rate) as Rate,Sum(Amount) as Amount,Sum(Comrate) as Commi,Sum(CartageAmount) as CartAmt,Sum(SplBonusAmount) as SplBon,Plant_Code,Sum(Clr) as clr,Refenrence     from procurement    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and  Prdate='" + d1 + "'  AND SESSIONS='" + ddl_shift.SelectedItem.Text + "'   AND Milk_kg > 0  group by   Plant_Code,Tid,Agent_id,prdate,Sessions,Refenrence)  AS PRO LEFT JOIN (sELECT Agent_Id,Agent_Name,VendorCode   FROM Agent_Master    WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   GROUP BY  Agent_Id,Agent_Name,VendorCode) as am on PRO.proagent= am.Agent_Id) AS ONEE WHERE VendorCode IS  NOT NULL)  as rrr) as leftside left join (Select   Plant_Code as pmplantcode,Plant_Name,WHcode   from  Plant_Master    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'     group by  Plant_Code,Plant_Name,WHcode) as pmm on Plant_Code=pmm.pmplantcode";
            SqlCommand cmd = new SqlCommand(getplantcode, con);
            SqlDataAdapter dws = new SqlDataAdapter(cmd);
            sapmrn.Rows.Clear();
            dws.Fill(sapmrn);
            //foreach (DataRow checkcard in sapmrn.Rows)
            //{

            //    agents = checkcard[0].ToString();
            //    vendorcheck = checkcard[19].ToString();
            //    try
            //    {

            //        getcheckvendor();
            //        string tempsap= sapmissvendor.Rows[0][0].ToString();
           
            //    }
            //    catch
            //    {
            //        missagent = 1;


            //    }
            //}


            //if (missagent != 1)
            //{

                foreach (DataRow dio in sapmrn.Rows)
                {

                    string proagent = dio[0].ToString();
                    string prdate = dio[1].ToString();
                    string Sessions = dio[2].ToString().TrimEnd();
                    days = dio[3].ToString();
                    string mon = dio[4].ToString();
                    int CONMON = mon.Length;
                    if (CONMON == 1)
                    {

                        mon = "0" + mon;
                    }
                    string year = dio[5].ToString();
                    getdayschar();
                    getdayssno();

                    string PLANT = dio[15].ToString();
                    string charcter = charc.Rows[0][0].ToString().TrimEnd();
                    string reference = PLANT + "" + proagent + "" + charcter + "" + mon + "" + year;
                    string referencesno = PLANT + "" + proagent + "" + charcter + "" + mon + "" + year + "" + sno;
                    string LTR = dio[6].ToString();
                    string KG = dio[7].ToString();
                    string FAT = dio[8].ToString();
                    string SNF = dio[9].ToString();
                    string RATE = dio[10].ToString();
                    string AMOUNT = dio[11].ToString();
                    string COMMI;
                    string CART;
                    string SPLBON;
                    try
                    {
                        COMMI = dio[12].ToString();
                        double TEMPCOMM = Convert.ToDouble(COMMI);
                        COMMI = TEMPCOMM.ToString();

                    }
                    catch
                    {
                        COMMI = "0";
                    }
                    try
                    {
                        CART = dio[13].ToString();
                    }
                    catch
                    {
                        CART = "0";
                    }
                    try
                    {
                        SPLBON = dio[14].ToString();
                    }
                    catch
                    {
                        SPLBON = "0";
                    }

                    string PLANTNAME = dio[16].ToString();
                    string WHHOUSE = dio[17].ToString().TrimEnd();
                    string AGENTNAME = dio[18].ToString();
                    string VENDORCODE = dio[19].ToString();
                    string clr = dio[20].ToString();
                    string REFER = dio[21].ToString();
                    string ITEMCODE;


                    string ocrcode2;
                    string DESC;
                    string insert;
                    //    if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "162") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "169"))
                    if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                    {
                        DESC = "COWMILK";
                        ocrcode2 = "P0001";
                        ITEMCODE = "11120002";
                        insert = "INSERT INTO  saptransaction(agent_id,Createdate,CardCode,cardname,Taxdate,DocDate,DueDate,ReferenceNoSno,ReferenceNo,Itemcode,Descriptions,Whscode,Quantity,OcrCode,OcerCode1,QuantityKgs,Snf,Fat,Clr,Rate,Cartage,SplBon,Commissions,Amount,inserted,Status,Sessions,plant_code,SNO,B1upload,Processed,Updaterefence) values('" + proagent + "','" + prdate + "','" + VENDORCODE + "','" + AGENTNAME + "','" + prdate + "','" + prdate + "','" + prdate + "','" + referencesno + "','" + reference + "','" + ITEMCODE + "','" + DESC + "','" + WHHOUSE + "','" + LTR + "','" + WHHOUSE + "','" + ocrcode2 + "','" + KG + "','" + SNF + "','" + FAT + "','" + clr + "','" + RATE + "','" + CART + "','" + SPLBON + "','" + COMMI + "','" + AMOUNT + "','" + Session["Name"].ToString() + "','1','" + Sessions + "','" + PLANT + "','" + sno + "','N','N','" + REFER + "')";
                    }
                    else
                    {
                        DESC = "BUFFALOMILK";
                        ocrcode2 = "P0001";
                        ITEMCODE = "11120003";
                        //  insert = "INSERT INTO  saptransaction(Createdate,CardCode,cardname,Taxdate,DocDate,DueDate,ReferenceNoSno,ReferenceNo,Itemcode,Descriptions,Whscode,Quantity,OcrCode,OcerCode1,QuantityKgs,Snf,Fat,Clr,Rate,Cartage,SplBon,Commissions,Amount,inserted,Status,Sessions,plant_code,SNO) values('" + prdate + "','" + VENDORCODE + "','" + AGENTNAME + "','" + prdate + "','" + prdate + "','" + prdate + "','" + referencesno + "','" + reference + "','" + ITEMCODE + "','" + DESC + "','" + WHHOUSE + "','" + LTR + "','" + WHHOUSE + "','" + ocrcode2 + "','" + KG + "','" + SNF + "','" + FAT + "','" + clr + "','" + RATE + "','0','0','" + COMMI + "','" + AMOUNT + "','" + Session["Name"].ToString() + "','1','" + Sessions + "','" + PLANT + "','" + sno + "')";
                        insert = "INSERT INTO  saptransaction(agent_id,Createdate,CardCode,cardname,Taxdate,DocDate,DueDate,ReferenceNoSno,ReferenceNo,Itemcode,Descriptions,Whscode,Quantity,OcrCode,OcerCode1,QuantityKgs,Snf,Fat,Clr,Rate,Cartage,SplBon,Commissions,Amount,inserted,Status,Sessions,plant_code,SNO,B1upload,Processed,Updaterefence) values('" + proagent + "','" + prdate + "','" + VENDORCODE + "','" + AGENTNAME + "','" + prdate + "','" + prdate + "','" + prdate + "','" + referencesno + "','" + reference + "','" + ITEMCODE + "','" + DESC + "','" + WHHOUSE + "','" + KG + "','" + WHHOUSE + "','" + ocrcode2 + "','" + LTR + "','" + SNF + "','" + FAT + "','" + clr + "','" + RATE + "','" + CART + "','" + SPLBON + "','" + COMMI + "','" + AMOUNT + "','" + Session["Name"].ToString() + "','1','" + Sessions + "','" + PLANT + "','" + sno + "','N','N','" + REFER + "')";
                    }


                    con = DB.GetConnection();
                    SqlCommand cmd12 = new SqlCommand(insert, con);
                    cmd12.CommandTimeout = 300;
                    cmd12.ExecuteNonQuery();

                    serialcount = serialcount + 1;
                }



                //  serialcount

                string mss = serialcount + ":" + "Data  inserted successfully";
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");
            //}
            //else
            //{
             
            //    string mss = "Agent" + agents + "Missing in Sap" + "VendorCode:" + vendorcheck;
            //    Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");


            //}
        }
        else
        {
            string mss = "Data Already inserted";

            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(mss) + "')</script>");

        }
    }
    public void getcheckvendor()
    {
        string getplantcode;
        con = DB.GetConnectionSAP();
        getplantcode = "Select   *    from  [SAPB1_Staging].[dbo].[EMROCRD]   where CardCode='" + vendorcheck + "'";
        SqlCommand cmd = new SqlCommand(getplantcode, con);
        SqlDataAdapter DSP = new SqlDataAdapter(cmd);
        sapmissvendor.Rows.Clear();
        DSP.Fill(sapmissvendor);
         



    }
    public void getmissingvendorlist()
    {
        DateTime dt1 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        //dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string getplantcode;
        con = DB.GetConnection();
        getplantcode = "Select    proagent,prdate,Sessions,days,month,year,milkltr,MilkKg,Fat,Snf,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Plant_Name,WHcode,Agent_Name,VendorCode,clr,Refenrence   from (Select proagent,prdate,Sessions,days,month,year,MilkKg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Plant_Code,Agent_Name,VendorCode,clr,Refenrence  from (sELECT proagent,prdate,Sessions,convert(varchar,days) as days,convert(varchar,month) as month,convert(varchar,year) as year,milkkg,Fat,Snf,milkltr,Rate,Amount,Commi,CartAmt,SplBon,Agent_Name,VendorCode,Plant_Code,clr,Refenrence   FROM (sELECT *    FROM (Select   Tid, convert(varchar,Agent_id,0) AS proagent,convert(varchar,Prdate,101) as prdate,Sessions,DATEPART(day,Prdate) as days ,DATEPART(MONTH,Prdate) as month,FORMAT(prdate,'yy') as year,Sum(Fat) as Fat,Sum(Snf) as Snf,Sum(Milk_kg) as MilkKg,Sum(milk_ltr) as   milkltr,Sum(rate) as Rate,Sum(Amount) as Amount,Sum(Comrate) as Commi,Sum(CartageAmount) as CartAmt,Sum(SplBonusAmount) as SplBon,Plant_Code,Sum(Clr) as clr,Refenrence     from procurement    where plant_code='" + ddl_Plantname.SelectedItem.Value + "'   and  Prdate='" + d1 + "'  AND SESSIONS='" + ddl_shift.SelectedItem.Text + "'    group by   Plant_Code,Tid,Agent_id,prdate,Sessions,Refenrence)  AS PRO LEFT JOIN (sELECT Agent_Id,Agent_Name,VendorCode   FROM Agent_Master    WHERE Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   GROUP BY  Agent_Id,Agent_Name,VendorCode) as am on PRO.proagent= am.Agent_Id) AS ONEE WHERE VendorCode IS  NULL)  as rrr) as leftside left join (Select   Plant_Code as pmplantcode,Plant_Name,WHcode   from  Plant_Master    where Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'     group by  Plant_Code,Plant_Name,WHcode) as pmm on Plant_Code=pmm.pmplantcode";
        SqlCommand cmd = new SqlCommand(getplantcode, con);
        SqlDataAdapter DSP=new SqlDataAdapter(cmd);
        missingagent.Rows.Clear();
        DSP.Fill(missingagent);

       
    }
    public void getdayschar()
    {
        con = DB.GetConnection();
        string get = "";
        if ((pcode == "155") || (pcode == "156") || (pcode == "157") || (pcode == "158") || (pcode == "162") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "169"))
        {
            get = "Select   character    from  sapcodecharacter   where     Billdays=10  and   '" + days + "' between Frm_day  and To_day   ";
        }
        else
        {
            get = "Select   character    from  sapcodecharacter   where     Billdays=15  and   '" + days + "' between Frm_day  and To_day   ";
        }
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter dft = new SqlDataAdapter(cmd);
        charc.Rows.Clear();
        dft.Fill(charc);
    }

    public void getdayssno()
    {
        con = DB.GetConnection();
        string get = "";
        get = "Select  MAX(Sno) AS SNO    from  saptransaction   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter dft = new SqlDataAdapter(cmd);
        try
        {
            snoo.Rows.Clear();
            dft.Fill(snoo);
            if (snoo.Rows.Count > 0)
            {
                sno = Convert.ToInt64(snoo.Rows[0][0]);
                sno = sno + 1;
            }
            else
            {
                sno = 1;

            }
        }
        catch
        {

            sno = 1;
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}