using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class tallyimportplrpt : System.Web.UI.Page
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

                    LoadPlantcode();
                    pcode = ddl_Plantname.SelectedItem.Value;
                    billdate();

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
    public void billdate()
    {

        try
        {
            ddl_BillDate.Items.Clear();
            con.Close();
            con = DB.GetConnection();
            //   string str = "SELECT convert(varchar,Bill_frmdate,103) as Bill_frmdate,convert(varchar,Bill_todate,103) as Bill_todate FROM Bill_date where  Plant_Code='"+pcode+"' AND Status=0  ";
            string str = "SELECT  TID,Bill_frmdate,Bill_todate FROM Bill_date where  Plant_Code='" + ddl_Plantname.SelectedItem.Value + "'  order by  Bill_frmdate desc  ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {

                    d1 = Convert.ToDateTime(dr["Bill_frmdate"].ToString());
                    d2 = Convert.ToDateTime(dr["Bill_todate"].ToString());
                    frmdate = d1.ToString("dd/MM/yyy");
                    Todate = d2.ToString("dd/MM/yyy");
                    ddl_BillDate.Items.Add(frmdate + "-" + Todate);
                }
            }
        }
        catch
        {

        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        try
        {
            string date = ddl_BillDate.Text;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];

            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + ":Bill Period" + FDATE + "To:" + TODATE + "'.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView2.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch
        {


        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Tell the compiler that the control is rendered
         * explicitly by overriding the VerifyRenderingInServerForm event.*/
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');


        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];

        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

        ViewState["FD"] = FDATE.ToString();
        ViewState["TTODATE"] = TODATE.ToString();

        GETTALLYREPORT1();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {


        try
        {

            getagentlist();
            if (Checkstatus != 1)
            {
                GETTALLYREPORT();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();

                string message = "Data Already Inserted";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);




            }
        }
        catch
        {

        }
    }
    public void getagentlist()
    {


        string date = ddl_BillDate.Text;
        string[] p = date.Split('/', '-');

        if (p[1] == "01")
        {
            month = "Jan";

        }

        if (p[1] == "02")
        {

            month = "Feb";
        }

        if (p[1] == "03")
        {
            month = "Mar";

        }

        if (p[1] == "04")
        {
            month = "Apr";

        }

        if (p[1] == "05")
        {
            month = "May";
        }

        if (p[1] == "06")
        {

            month = "June";
        }

        if (p[1] == "07")
        {
            month = "July";

        }

        if (p[1] == "08")
        {

            month = "Aug";
        }

        if (p[1] == "09")
        {
            month = "Sep";


        }


        if (p[1] == "10")
        {
            month = "Oct";

        }

        if (p[1] == "11")
        {

            month = "Nov";
        }

        if (p[1] == "12")
        {
            month = "Dec";

        }
        getvald = p[0];
        getvalm = p[1];
        getvaly = p[2];
        getvaldd = p[3];
        getvalmm = p[4];
        getvalyy = p[5];

        FDATE = getvalm + "/" + getvald + "/" + getvaly;
        TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;

        string str = "";
        if ((pcode == "157") || (pcode == "165") || (pcode == "166") || (pcode == "167") || (pcode == "168") || (pcode == "169"))
        {
            //  str = "select VchDate,(Agent_id + '-'+ Agent_Name + '-' + Plant_Name) as Suppliername,VchDate as SupDate,Plant_Name as Purchaseledgerofmilk,Plant_Name as Itemname,BilledQTy,Rate,Amount,procurementcortageDR,otherincome,procurementMaterialDebtocsp,cateFeedSalesAgentscsp,Canslidsstock,billadvancekbp,Sfatkg,SSnfkg     from (Select *  from (sELECT convert(varchar,Agent_id) as Agent_id,convert(varchar,Frm_date,101) as Frm_date,convert(varchar,To_date,101) as To_date ,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],Route_id,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [Vch Date],Plant_code,SUM(Smkg) as BilledQTy,Convert(decimal(18,2),(SUM(SAmt) +  SUM(TotAdditions)) / SUM(Smltr)) as Rate,(SUM(SAmt) + SUM(TotAdditions)) as Amount,SUM(Scaramt) as procurementcortageDR,SUM(Roundoff) as otherincome,SUM(Ai) as procurementMaterialDebtocsp,SUM(Feed) AS cateFeedSalesAgentscsp,SUM(can) as Canslidsstock,SUM(Billadv) as billadvancekbp,SUM(Sfatkg) as Sfatkg,SUM(SSnfkg) as SSnfkg,plant_code as payplantcode FROM  Paymentdata    WHERE Plant_code='" + pcode + "'  AND   Frm_date='" + FDATE + "' AND  To_date='" + TODATE + "'  group by   Agent_id,Route_id,plant_code,Frm_date,To_date,Plant_code) as paydata left join (Select Agent_Id as Amagent,convert(varchar,Agent_Name) as  Agent_Name  from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by Agent_Id,Agent_Name) as am on paydata.Agent_id=am.Amagent) as ll left join (Select  plant_code as pmpcode,convert(varchar,Plant_Name) as Plant_Name    from Plant_Master where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by plant_code,Plant_Name) as pm on ll.Plant_code = pm.pmpcode";
            str = "select VchDate,(Agent_id + '-'+ Agent_Name + '-' + Plant_Name) as Suppliername,VchDate as SupDate,Plant_Name as Purchaseledgerofmilk,Plant_Name as Itemname,BilledQTy,Rate,Amount,procurementcortageDR,otherincome,procurementMaterialDebtocsp,cateFeedSalesAgentscsp,Canslidsstock,billadvancekbp,Sfatkg,SSnfkg, loan from (Select *  from (sELECT convert(varchar,Agent_id) as Agent_id,convert(varchar,Frm_date,101) as Frm_date,convert(varchar,To_date,101) as To_date ,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],Route_id,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [Vch Date],Plant_code,SUM(Smkg) as BilledQTy,Convert(decimal(18,2),(SUM(SAmt) +  SUM(TotAdditions)) / SUM(Smltr)) as Rate,(SUM(SAmt) + SUM(TotAdditions)) as Amount,SUM(Scaramt) as procurementcortageDR,SUM(Roundoff) as otherincome,SUM(Ai) as procurementMaterialDebtocsp,SUM(Feed) AS cateFeedSalesAgentscsp,SUM(can) as Canslidsstock,SUM(Billadv) as billadvancekbp,SUM(Sfatkg) as Sfatkg,SUM(SSnfkg) as SSnfkg, plant_code as payplantcode, SUM(Sinstamt) AS loan FROM  Paymentdata    WHERE Plant_code='" + pcode + "'  AND   Frm_date='" + FDATE + "' AND  To_date='" + TODATE + "'  group by   Agent_id,Route_id,plant_code,Frm_date,To_date,Plant_code) as paydata left join (Select Agent_Id as Amagent,convert(varchar,Agent_Name) as  Agent_Name  from Paymentdata   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and  Frm_date='" + FDATE + "'  AND To_date= '" + TODATE + "'  group by Agent_Id,Agent_Name) as am on paydata.Agent_id=am.Amagent) as ll left join (Select  plant_code as pmpcode,convert(varchar,Plant_Name) as Plant_Name    from Plant_Master where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by plant_code,Plant_Name) as pm on ll.Plant_code = pm.pmpcode";

        }
        else
        {
            //cow
            //   str = "select VchDate,(Agent_id + '-'+ Agent_Name + '-' + Plant_Name) as Suppliername,VchDate as SupDate,Plant_Name as Purchaseledgerofmilk,Plant_Name as Itemname,BilledQTy,Rate,Amount,procurementcortageDR,otherincome,procurementMaterialDebtocsp,cateFeedSalesAgentscsp,Canslidsstock,billadvancekbp,Sfatkg,SSnfkg     from (Select *  from (sELECT convert(varchar,Agent_id) as Agent_id,convert(varchar,Frm_date,101) as Frm_date,convert(varchar,To_date,101) as To_date ,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],Route_id,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [Vch Date],Plant_code,SUM(Smltr) as BilledQTy,Convert(decimal(18,2),(SUM(SAmt) +  SUM(TotAdditions)) / SUM(Smltr)) as Rate,(SUM(SAmt) + SUM(TotAdditions)) as Amount,SUM(Scaramt) as procurementcortageDR,SUM(Roundoff) as otherincome,SUM(Ai) as procurementMaterialDebtocsp,SUM(Feed) AS cateFeedSalesAgentscsp,SUM(can) as Canslidsstock,SUM(Recovery) as billadvancekbp,SUM(Sfatkg) as Sfatkg,SUM(SSnfkg) as SSnfkg,plant_code as payplantcode FROM  Paymentdata    WHERE Plant_code='" + pcode + "'  AND   Frm_date='" + FDATE + "' AND  To_date='" + TODATE + "'  group by   Agent_id,Route_id,plant_code,Frm_date,To_date,Plant_code) as paydata left join (Select Agent_Id as Amagent,convert(varchar,Agent_Name) as  Agent_Name  from Agent_Master   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "'   group by Agent_Id,Agent_Name) as am on paydata.Agent_id=am.Amagent) as ll left join (Select  plant_code as pmpcode,convert(varchar,Plant_Name) as Plant_Name    from Plant_Master where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by plant_code,Plant_Name) as pm on ll.Plant_code = pm.pmpcode";

            str = "select VchDate,(Agent_id + '-'+ Agent_Name + '-' + Plant_Name) as Suppliername,VchDate as SupDate,Plant_Name as Purchaseledgerofmilk,Plant_Name as Itemname,BilledQTy,Rate,Amount,procurementcortageDR,otherincome,procurementMaterialDebtocsp,cateFeedSalesAgentscsp,Canslidsstock,billadvancekbp,Sfatkg,SSnfkg, loan    from (Select *  from (sELECT convert(varchar,Agent_id) as Agent_id,convert(varchar,Frm_date,101) as Frm_date,convert(varchar,To_date,101) as To_date ,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],Route_id,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [Vch Date],Plant_code,SUM(Smltr) as BilledQTy,Convert(decimal(18,2),(SUM(SAmt) +  SUM(TotAdditions)) / SUM(Smltr)) as Rate,(SUM(SAmt) + SUM(TotAdditions)) as Amount,SUM(Scaramt) as procurementcortageDR,SUM(Roundoff) as otherincome,SUM(Ai) as procurementMaterialDebtocsp,SUM(Feed) AS cateFeedSalesAgentscsp,SUM(can) as Canslidsstock,SUM(Recovery) as billadvancekbp,SUM(Sfatkg) as Sfatkg,SUM(SSnfkg) as SSnfkg,plant_code as payplantcode,SUM(Sinstamt) AS loan FROM  Paymentdata    WHERE Plant_code='" + pcode + "'  AND   Frm_date='" + FDATE + "' AND  To_date='" + TODATE + "'  group by   Agent_id,Route_id,plant_code,Frm_date,To_date,Plant_code) as paydata left join (Select Agent_Id as Amagent,convert(varchar,Agent_Name) as  Agent_Name  from Paymentdata   where Plant_code='" + ddl_Plantname.SelectedItem.Value + "' and  Frm_date='" + FDATE + "'  AND To_date= '" + TODATE + "'  group by Agent_Id,Agent_Name) as am on paydata.Agent_id=am.Amagent) as ll left join (Select  plant_code as pmpcode,convert(varchar,Plant_Name) as Plant_Name    from Plant_Master where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by plant_code,Plant_Name) as pm on ll.Plant_code = pm.pmpcode";
        }

        SqlConnection con = new SqlConnection();
        con = DB.GetConnection();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter drt = new SqlDataAdapter(cmd);
        DataTable tallyagent = new DataTable();
        tallyagent.Rows.Clear();
        drt.Fill(tallyagent);
        checkdata();
        if (Checkstatus != 1)
        {

            foreach (DataRow tally in tallyagent.Rows)
            {

                GETTID();
                string vchDate = tally[0].ToString();
                string Suppliername = tally[1].ToString();
                string Supplierdate = tally[2].ToString();
                string Ledgerofpurcharseplant = "Purchase of Milk-" + tally[3].ToString();
                double Billedqty = Convert.ToDouble(tally[5]);
                ltr = Billedqty / 1.03;
                conltr = ltr.ToString("F2");
                //ViewState["ltr"] = conltr;
                double Rate = Convert.ToDouble(tally[6]);
                double Amount = Convert.ToDouble(tally[7]);
                string Cartage = tally[8].ToString();
                string Others = tally[9].ToString();
                string material = tally[10].ToString();
                string Feed = tally[11].ToString();
                string Canorstock = tally[12].ToString();
                string BillAdvance = tally[13].ToString();
                double fatkg = Convert.ToDouble(tally[14]); //kgfat
                double snfkg = Convert.ToDouble(tally[15]); //kgsnf
                double loan = Convert.ToDouble(tally[16]); //kgsnf
                double avgfat = (fatkg / Billedqty) * 100; //avgfat
                double kgfatrate = Amount / fatkg;
                double avgSnf = (snfkg / Billedqty) * 100; //avfsnf
                double ts = avgfat + avgSnf; //tottalts
                double rts = Rate / ts;
                string GETINSERT = "";
                string fatkg1 = fatkg.ToString("F2");
                string snfkg1 = snfkg.ToString("F2");
                string avgfat1 = avgfat.ToString("F2");
                string avgSnf1 = avgSnf.ToString("F2");
                string ts1 = ts.ToString("F2");
                string rts1 = rts.ToString("F2");
                string ItemName;
                if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                {
                    ItemName = "COW MILK";
                    double TEMPKG = (Billedqty * 1.03);
                    string STTEMKH = TEMPKG.ToString("F2");
                    TEMPKG = Convert.ToDouble(STTEMKH);
                    avgfat = (fatkg / TEMPKG) * 100; //avgfat
                    kgfatrate = Amount / fatkg;
                    avgSnf = (snfkg / TEMPKG) * 100; //avfsnf
                    ts = avgfat + avgSnf; //tottalts
                    rts = Rate / ts;
                    GETINSERT = "";
                    fatkg1 = fatkg.ToString("F2");
                    snfkg1 = snfkg.ToString("F2");
                    avgfat1 = avgfat.ToString("F2");
                    avgSnf1 = avgSnf.ToString("F2");
                    ts1 = ts.ToString("F2");
                    rts1 = rts.ToString("F2");

                }
                else
                {
                    ItemName = "WHOLE MILK";
                    avgfat = (fatkg / Billedqty) * 100; //avgfat
                    kgfatrate = Amount / fatkg;
                    avgSnf = (snfkg / Billedqty) * 100; //avfsnf
                    ts = avgfat + avgSnf; //tottalts
                    rts = Rate / ts;
                    GETINSERT = "";
                    fatkg1 = fatkg.ToString("F2");
                    snfkg1 = snfkg.ToString("F2");
                    avgfat1 = avgfat.ToString("F2");
                    avgSnf1 = avgSnf.ToString("F2");
                    ts1 = ts.ToString("F2");
                    rts1 = rts.ToString("F2");
                }
                string Supplierinvno = Suppliername + "(" + getvald + "-" + getvaldd + ")" + month + getvaly;
                //    ViewState["billdate"] = Supplierinvno;
                BILLDATE = Supplierinvno;
                string tempkgrate = kgfatrate.ToString("f2");
                kgfatrate = Convert.ToDouble(tempkgrate);
                if (ddl_Plantname.SelectedItem.Text == "ARANI")
                {
                    Tallyno = "ARA";
                }
                if (ddl_Plantname.SelectedItem.Text == "KAVERIPATNAM")
                {
                    Tallyno = "KVP";
                }
                if (ddl_Plantname.SelectedItem.Text == "GUDLUR")
                {
                    Tallyno = "GUD";
                }
                if (ddl_Plantname.SelectedItem.Text == "WALAJA")
                {
                    Tallyno = "WAL";
                }
                if (ddl_Plantname.SelectedItem.Text == "V_KOTA")
                {
                    Tallyno = "VKT";
                }
                if (ddl_Plantname.SelectedItem.Text == "RCPURAM")
                {
                    Tallyno = "RCP";
                }
                if (ddl_Plantname.SelectedItem.Text == "BOMMASAMUTHIRAM")
                {
                    Tallyno = "BOMMA";
                }
                if (ddl_Plantname.SelectedItem.Text == "TARIGONDA")
                {
                    Tallyno = "TARI";
                }
                if (ddl_Plantname.SelectedItem.Text == "CSPURAM")
                {
                    Tallyno = "CSP";
                }
                if (ddl_Plantname.SelectedItem.Text == "KONDEPI")
                {
                    Tallyno = "KDP";

                }
                if (ddl_Plantname.SelectedItem.Text == "KAVALI")
                {

                    Tallyno = "KVL";
                }
                if (ddl_Plantname.SelectedItem.Text == "GUDIPALLI PADU")
                {

                    Tallyno = "GUDI";
                }
                if (ddl_Plantname.SelectedItem.Text == "KALIGIRI")
                {
                    Tallyno = "KLG";
                }
                string gettp = ViewState["maxtid"] + Tallyno;
                try
                {

                    GETINSERT = "INSERT INTO TallypurchaseImportAgentWsie(VchNo,TallyVchNo,VchDate,SupplierinvNo,SupplierinvDate,SupplierName,PurchaseLedger,ItemName,Billedqty,Rate,Amount,Cartage,otherincome,Material,Feed,CanorStock,BillAdvance,UserName,Datetime,plant_code,AvgFat,Avgsnf,Kgfat,KgSnf,Kgrate,Ts,Rts,FrmDate,Todate, loandedamt)  values ('" + ViewState["maxtid"] + "','" + gettp + "','" + vchDate + "','" + Supplierinvno + "','" + Supplierdate + "','" + Suppliername + "','" + Ledgerofpurcharseplant + "','" + ItemName + "','" + Billedqty + "','" + Rate + "','" + Amount + "','" + Cartage + "','" + Others + "','" + material + "','" + Feed + "','" + Canorstock + "','" + BillAdvance + "','" + Session["Name"].ToString() + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + pcode + "','" + avgfat1 + "','" + avgSnf1 + "','" + fatkg1 + "','" + snfkg1 + "','" + kgfatrate + "','" + ts1 + "','" + rts1 + "','" + FDATE + "','" + TODATE + "','" + loan + "');";
                    con = DB.GetConnection();
                    SqlCommand COMM = new SqlCommand(GETINSERT, con);
                    COMM.ExecuteNonQuery();
                }
                catch
                {
                    GETINSERT = "INSERT INTO TallypurchaseImportAgentWsie(VchNo,TallyVchNo,VchDate,SupplierinvNo,SupplierinvDate,SupplierName,PurchaseLedger,ItemName,Billedqty,Rate,Amount,Cartage,otherincome,Material,Feed,CanorStock,BillAdvance,UserName,Datetime,plant_code,AvgFat,Avgsnf,Kgfat,KgSnf,Kgrate,Ts,Rts,FrmDate,Todate,loandedamt)  values ('" + ViewState["maxtid"] + "','" + gettp + "','" + vchDate + "','" + Supplierinvno + "','" + Supplierdate + "','" + Suppliername + "','" + Ledgerofpurcharseplant + "','" + ItemName + "','" + Billedqty + "','" + Rate + "','" + Amount + "','" + Cartage + "','" + Others + "','" + material + "','" + Feed + "','" + Canorstock + "','" + BillAdvance + "','" + Session["Name"].ToString() + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + pcode + "','" + avgfat1 + "','" + avgSnf1 + "','" + fatkg1 + "','" + snfkg1 + "','0','" + ts1 + "','0','" + FDATE + "','" + TODATE + "', '" + loan + "');";
                    con = DB.GetConnection();
                    SqlCommand COMM = new SqlCommand(GETINSERT, con);
                    COMM.ExecuteNonQuery();
                }
            }
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }


    }


    public void checkdata()
    {
        try
        {
            string date = ddl_BillDate.Text;
            string[] p = date.Split('/', '-');
            getvald = p[0];
            getvalm = p[1];
            getvaly = p[2];
            getvaldd = p[3];
            getvalmm = p[4];
            getvalyy = p[5];

            FDATE = getvalm + "/" + getvald + "/" + getvaly;
            TODATE = getvalmm + "/" + getvaldd + "/" + getvalyy;
            con = DB.GetConnection();
            string strrr = "Select * from TallypurchaseImportAgentWsie  where plant_code='" + pcode + "'  and  FrmDate='" + FDATE + "'  and Todate='" + TODATE + "' ";
            SqlCommand cmdnew = new SqlCommand(strrr, con);
            SqlDataAdapter DAnew = new SqlDataAdapter(cmdnew);
            DataTable checkdt = new DataTable();
            DAnew.Fill(checkdt);

            if (checkdt.Rows.Count > 1)
            {

                Checkstatus = 1;

            }
        }
        catch
        {


        }
    }


    public void GETTID()
    {
        try
        {
            try
            {
                con = DB.GetConnection();
                string tid = "Select max(VchNo)  as VchNo  from  TallypurchaseImportAgentWsie  WHERE PLANT_CODE='" + pcode + "' ";
                SqlCommand dmk = new SqlCommand(tid, con);
                SqlDataReader dr = dmk.ExecuteReader();
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {

                        int gettidd = Convert.ToInt32(dr["VchNo"]);
                        ViewState["maxtid"] = gettidd + 1;

                    }

                }

            }
            catch
            {

                ViewState["maxtid"] = 1;

            }
        }
        catch
        {


        }


    }




    public void GETTALLYREPORT()
    {
        try
        {
            con = DB.GetConnection();
            string GETSTR = "SELECT TallyVchNo ,REPLACE(CONVERT(VARCHAR(9), VchDate, 6), ' ', '-') AS [VchDate],SupplierinvNo,SupplierinvDate,SupplierName,PurchaseLedger,ItemName,Billedqty,Rate,Amount,Cartage,otherincome,Material,Feed,CanorStock,BillAdvance,AvgFat,Avgsnf,Ts,Rts,Kgfat,KgSnf,Kgrate   FROM TallypurchaseImportAgentWsie WHERE PLANT_CODE='" + pcode + "' AND  FrmDate='" + FDATE + "' and Todate='" + TODATE + "'  ORDER BY VchNo asc   ";
            SqlCommand cmd3 = new SqlCommand(GETSTR, con);
            SqlDataAdapter DA3 = new SqlDataAdapter(cmd3);
            DataTable tallRC = new DataTable();
            tallRC.Rows.Clear();
            DA3.Fill(tallRC);
            DA3.Fill(DTG, ("Tallyimport"));
            DataTable tall1 = new DataTable();
            tall1.Columns.Add("VchNo");
            tall1.Columns.Add("VchDate");
            tall1.Columns.Add("SupplierInvNo");
            tall1.Columns.Add("SupDate");
            tall1.Columns.Add("SupplierName");
            tall1.Columns.Add("PurchaseLedger");
            tall1.Columns.Add("ItemName");
            tall1.Columns.Add("BilledQty");
            tall1.Columns.Add("Rate");
            tall1.Columns.Add("Amount");
            string ProcurementCortage1 = "ProcurementCortage-" + ddl_Plantname.SelectedItem.Text;
            tall1.Columns.Add(ProcurementCortage1);
            string OtherIncome1 = "OtherIncome-" + ddl_Plantname.SelectedItem.Text;
            tall1.Columns.Add(OtherIncome1);
            string ProcurementMaterialDebtor = "ProcurementMaterial-Debtor-" + ddl_Plantname.SelectedItem.Text;
            tall1.Columns.Add(ProcurementMaterialDebtor);
            string CatteleFeedSalesAgentscs = "CattleFeedSales-Agents-" + ddl_Plantname.SelectedItem.Text;
            tall1.Columns.Add(CatteleFeedSalesAgentscs);
            string getstringval = "Cans&LidsStock-" + ddl_Plantname.SelectedItem.Text;
            tall1.Columns.Add(getstringval);

            string BillAdvance1;

            if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
            {
                BillAdvance1 = "Dpu Maintenance Charges-" + ddl_Plantname.SelectedItem.Text;
            }
            else
            {
                BillAdvance1 = "BillAdvance-" + ddl_Plantname.SelectedItem.Text;
            }
            tall1.Columns.Add(BillAdvance1);
            tall1.Columns.Add("LedgerAmount");
            tall1.Columns.Add("Narration");
            foreach (DataRow drtally in tallRC.Rows)
            {

                string no = drtally[0].ToString();
                string VchDate = drtally[1].ToString();
                string SupplierInvNo = drtally[2].ToString();
                string SupDate = drtally[1].ToString();
                //    string SupplierName = drtally[4].ToString() + "Route Milk Bills" + ddl_Plantname.SelectedItem.Text;
                string SupplierName = drtally[4].ToString();
                string convSupplierName = SupplierName;
                string PurchaseLedger = drtally[5].ToString();

                BILLDATE = drtally[4].ToString() + "(" + getvald + "-" + getvaldd + ")" + month + getvaly;
                ViewState["BILLDATE"] = BILLDATE.ToString();

                //     string ItemName = drtally[6].ToString();

                string ItemName;

                if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                {
                    ItemName = "COW MILK";
                }
                else
                {

                    ItemName = "WHOLE MILK";
                }


                string BilledQty = drtally[7].ToString();

                double mmmltr = Convert.ToDouble(BilledQty);

                ViewState["ltr12"] = (mmmltr / 1.03).ToString("F2");
                string Rate = drtally[8].ToString();

                double Amount = Convert.ToDouble(drtally[9]);
                double ProcurementCortage = Convert.ToDouble(drtally[10]);
                string conamount = (Amount - ProcurementCortage).ToString("f2");

                double CONAMTD = Convert.ToDouble(conamount);
                double TOTAmount = CONAMTD + ProcurementCortage;


                double OtherIncomeE = Convert.ToDouble(drtally[11]);
                double ProcurementMaterialDebtorcspP = Convert.ToDouble(drtally[12]);
                double CatteleFeedSalesAgentscspP = Convert.ToDouble(drtally[13]);
                double CansLidsStockK = Convert.ToDouble(drtally[14]);
                double BillAdvanceE = Convert.ToDouble(drtally[15]);

                string avgfat = drtally[16].ToString();

                string Avgsnf = (drtally[17]).ToString();

                string TS = drtally[18].ToString();

                string RTS = (drtally[19]).ToString();

                string Kgfat = drtally[20].ToString();

                string KgSnf = drtally[21].ToString();

                string Kgrate = drtally[22].ToString();

                string OtherIncome = OtherIncomeE.ToString("F2");
                string ProcurementMaterialDebtorcsp = ProcurementMaterialDebtorcspP.ToString("F2");
                string CatteleFeedSalesAgentscsp = CatteleFeedSalesAgentscspP.ToString("F2");
                string CansLidsStock = CansLidsStockK.ToString("F2");
                string BillAdvance = BillAdvanceE.ToString("F2");


                double minusamount = Convert.ToDouble(OtherIncomeE + ProcurementMaterialDebtorcspP + CatteleFeedSalesAgentscspP + CansLidsStockK + BillAdvanceE);

                string ledamount = (TOTAmount - minusamount).ToString("f2");

                string Narration;
                //  string Narration = "Being PurChase Of MilkFrom " + ViewState["billdate"] + "TotalQty=" + BilledQty + " Kgs/ltr:" + ltr;

                if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                {

                    double ltr = Convert.ToDouble(BilledQty);
                    string kgg = (ltr * 1.03).ToString("f2");
                    Narration = "Being PurChase Of MilkFrom " + BILLDATE + "TotalQty=" + BilledQty + " Kgs/ltr:" + kgg + "A/f:" + avgfat + "A/S:" + Avgsnf + "T/S:" + TS + "R/TS:" + RTS;
                }
                else
                {
                    Narration = "Being PurChase Of MilkFrom " + BILLDATE + "TotalQty=" + BilledQty + " Kgs/ltr:" + ViewState["ltr12"].ToString() + "A/f:" + avgfat + "A/S:" + Avgsnf + "Kgfat:" + Kgfat + "KgSnf:" + KgSnf + "Kgrate:" + Kgrate;
                }
                tall1.Rows.Add(no, VchDate, SupplierInvNo, SupDate, convSupplierName, PurchaseLedger, ItemName, BilledQty, Rate, conamount, ProcurementCortage, OtherIncome, ProcurementMaterialDebtorcsp, CatteleFeedSalesAgentscsp, CansLidsStock, BillAdvance, ledamount, Narration);

            }

            if (tall1.Rows.Count > 1)
            {

                GridView2.DataSource = tall1;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();

            }
        }
        catch
        {

        }

    }
    public void GETTALLYREPORT1()
    {
        try
        {
            con = DB.GetConnection();

            string str = "SELECT pay .*,Gpay.* FROM " + " (SELECT  Route_id,Route_name,CONVERT(VARCHAR(13),(CONVERT(NVARCHAR(10),Agent_id)+'_'+Agent_name)) AS Agent_name,Smkg,Smltr,SAmt,CAST((SAmt/Smltr) AS DECIMAL(18,2)) AS Mrate,SInsentAmt,Scaramt,SSplBonus,TotAdditions+SAmt AS TotAdditions,CAST(((SAmt+SInsentAmt+Scaramt+SSplBonus)/Smltr) AS DECIMAL(18,2)) AS Arate,ClaimAount,Sinstamt,Billadv,Ai,Feed,can,Roundoff,NetAmount,CAST(((Sfatkg*100)/Smkg) AS DECIMAL(18,1)) AS Afat,CAST(((SSnfkg*100)/Smkg) AS DECIMAL(18,1)) AS Asnf,Sfatkg,SSnfkg,CAST(((SAmt+SInsentAmt+Scaramt+SSplBonus)/Sfatkg) AS  DECIMAL(18,2)) AS kgFatrate  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + ViewState["FD"] + "' AND To_date='" + ViewState["TTODATE"] + "') AS pay " + " LEFT JOIN " + " (SELECT  Route_id,SUM(Smkg) AS GSmkg,SUM(Smltr) AS GSmltr,SUM(SAmt) AS GSAmt,CAST((SUM(SAmt)/SUM(Smltr)) AS DECIMAL(18,2)) AS GMrate,SUM(SInsentAmt) AS GSInsentAmt,SUM(Scaramt) AS GScaramt,SUM(SSplBonus) AS GSSplBonus,SUM(TotAdditions)+SUM(SAmt) AS GTotAdditions,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Smltr)) AS DECIMAL(18,2)) AS GArate,SUM(ClaimAount) AS GClaimAount,SUM(Sinstamt) AS GSinstamt,SUM(Billadv) AS GBilladv,SUM(Ai) AS GAi,SUM(Feed) AS GFeed,SUM(can) AS Gcan,SUM(Roundoff) AS GRoundoff,SUM(NetAmount) AS GNetAmount,CAST(((SUM(Sfatkg)*100)/SUM(Smkg)) AS DECIMAL(18,1)) AS GAfat,CAST(((SUM(SSnfkg)*100)/SUM(Smkg)) AS DECIMAL(18,1)) AS GAsnf,SUM(Sfatkg) AS GSfatkg,SUM(SSnfkg) AS GSSnfkg,CAST(((SUM(SAmt)+SUM(SInsentAmt)+SUM(Scaramt)+SUM(SSplBonus))/SUM(Sfatkg)) AS  DECIMAL(18,2)) AS GkgFatrate  FROM Paymentdata Where Plant_code='" + pcode + "' AND Frm_date='" + ViewState["FD"] + "' AND To_date='" + ViewState["TTODATE"] + "' GROUP BY Route_id) AS Gpay ON pay.Route_id=Gpay.Route_id  order by  pay.Agent_name ";


            string GETSTR = "SELECT TallyVchNo ,REPLACE(CONVERT(VARCHAR(9), VchDate, 6), ' ', '-') AS [VchDate],SupplierinvNo,SupplierinvDate,SupplierName,PurchaseLedger,ItemName,Billedqty,Rate,Amount,Cartage,otherincome,Material,Feed,CanorStock,BillAdvance,AvgFat,Avgsnf,Ts,Rts,Kgfat,KgSnf,Kgrate, loandedamt   FROM TallypurchaseImportAgentWsie WHERE PLANT_CODE='" + pcode + "' AND  FrmDate='" + ViewState["FD"] + "' and Todate='" + ViewState["TTODATE"] + "'  ORDER BY VchNo asc   ";
            SqlCommand cmd3 = new SqlCommand(GETSTR, con);
            SqlDataAdapter DA3 = new SqlDataAdapter(cmd3);
            DataTable TALLREPORT = new DataTable();
            //DA3.Fill(DTG, ("Tallyimport"));
            DA3.Fill(TALLREPORT);
            DataTable tall = new DataTable();
            tall.Columns.Add("VchNo");
            tall.Columns.Add("VchDate");
            tall.Columns.Add("SupplierInvNo");
            tall.Columns.Add("SupDate");
            tall.Columns.Add("SupplierName");
            tall.Columns.Add("PurchaseLedger");
            tall.Columns.Add("ItemName");
            tall.Columns.Add("BilledQty");
            tall.Columns.Add("Rate");
            tall.Columns.Add("Amount");
            string ProcurementCortage1 = "ProcurementCortage-" + ddl_Plantname.SelectedItem.Text;
            tall.Columns.Add(ProcurementCortage1);

            string OtherIncome1 = "OtherIncome-" + ddl_Plantname.SelectedItem.Text;
            tall.Columns.Add(OtherIncome1);
            string ProcurementMaterialDebtor = "ProcurementMaterial-Debtor-" + ddl_Plantname.SelectedItem.Text;
            tall.Columns.Add(ProcurementMaterialDebtor);
            string CatteleFeedSalesAgentscs = "CattleFeedSales-Agents-" + ddl_Plantname.SelectedItem.Text;
            tall.Columns.Add(CatteleFeedSalesAgentscs);
            string getstringval = "Cans&LidsStock-" + ddl_Plantname.SelectedItem.Text;
            tall.Columns.Add(getstringval);

            string BillAdvance1;

            if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
            {
                BillAdvance1 = "Dpu Maintenance Charges-" + ddl_Plantname.SelectedItem.Text;
            }
            else
            {
                BillAdvance1 = "BillAdvance-" + ddl_Plantname.SelectedItem.Text;
            }


            tall.Columns.Add(BillAdvance1);
            tall.Columns.Add("LedgerAmount");

            tall.Columns.Add("Narration");
            foreach (DataRow drtally in TALLREPORT.Rows)
            {

                string no = drtally[0].ToString();
                string VchDate = drtally[1].ToString();
                string SupplierInvNo = drtally[2].ToString();
                string SupDate = drtally[1].ToString();
                //   string SupplierName = drtally[4].ToString() + "Route Milk Bills" + ddl_Plantname.SelectedItem.Text;
                string SupplierName = drtally[4].ToString();

                string convSupplierName = SupplierName;
                string PurchaseLedger = drtally[5].ToString();
                //    string ItemName = drtally[6].ToString();

                string ItemName;

                if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                {
                    ItemName = "COW MILK";
                }
                else
                {

                    ItemName = "WHOLE MILK";
                }


                string BilledQty = drtally[7].ToString();

                double mmmltr = Convert.ToDouble(BilledQty);

                ViewState["lltr"] = (mmmltr / 1.03).ToString("F2");


                string Rate = drtally[8].ToString();

                double Amount = Convert.ToDouble(drtally[9]);
                double ProcurementCortage = Convert.ToDouble(drtally[10]);
                string conamount = (Amount - ProcurementCortage).ToString("f2");
                // string conamount = (Amount).ToString("f2");

                double CONAMTD = Convert.ToDouble(conamount);
                double TOTAmount = CONAMTD + ProcurementCortage;


                double OtherIncomeE = Convert.ToDouble(drtally[11]);
                double ProcurementMaterialDebtorcspP = Convert.ToDouble(drtally[12]);
                double CatteleFeedSalesAgentscspP = Convert.ToDouble(drtally[13]);
                double CansLidsStockK = Convert.ToDouble(drtally[14]);
                double BillAdvanceE = Convert.ToDouble(drtally[15]);

                string avgfat = drtally[16].ToString();

                string Avgsnf = (drtally[17]).ToString();

                string TS = drtally[18].ToString();

                string RTS = (drtally[19]).ToString();

                string Kgfat = drtally[20].ToString();

                string KgSnf = drtally[21].ToString();

                string Kgrate = drtally[22].ToString();

                string loanamt = drtally[23].ToString();

                double loan = Convert.ToDouble(loanamt);
                string OtherIncome = OtherIncomeE.ToString("F2");
                string ProcurementMaterialDebtorcsp = ProcurementMaterialDebtorcspP.ToString("F2");
                string CatteleFeedSalesAgentscsp = CatteleFeedSalesAgentscspP.ToString("F2");
                string CansLidsStock = CansLidsStockK.ToString("F2");
                string BillAdvance = BillAdvanceE.ToString("F2");
                double minusamount = Convert.ToDouble(OtherIncomeE + ProcurementMaterialDebtorcspP + CatteleFeedSalesAgentscspP + CansLidsStockK + BillAdvanceE);
                //double minusamount = Convert.ToDouble(OtherIncomeE + ProcurementMaterialDebtorcspP + CatteleFeedSalesAgentscspP + CansLidsStockK + BillAdvanceE + ProcurementCortage + loan);

                string ledamount = (TOTAmount - minusamount).ToString("f2");

                BILLDATE = drtally[4].ToString() + "(" + getvald + "-" + getvaldd + ")" + month + getvaly;
                ViewState["BILLDATE"] = BILLDATE.ToString();

                string Narration;
                //  string Narration = "Being PurChase Of MilkFrom " + ViewState["billdate"] + "TotalQty=" + BilledQty + " Kgs/ltr:" + ltr;

                if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                {

                    double ltr = Convert.ToDouble(BilledQty);
                    string kgg = (ltr * 1.03).ToString("f2");
                    Narration = "Being PurChase Of MilkFrom " + ViewState["BILLDATE"] + "TotalQty=" + BilledQty + " Kgs/ltr:" + kgg + "A/f:" + avgfat + "A/S:" + Avgsnf + "T/S:" + TS + "R/TS:" + RTS;
                }
                else
                {
                    Narration = "Being PurChase Of MilkFrom " + ViewState["BILLDATE"] + "TotalQty=" + BilledQty + " Kgs/ltr:" + ViewState["lltr"].ToString() + "A/f:" + avgfat + "A/S:" + Avgsnf + "Kgfat:" + Kgfat + "KgSnf:" + KgSnf + "Kgrate:" + Kgrate;
                }
                //  string Narration = "Being PurChase Of MilkFrom " + ViewState["billdate"] + "TotalQty=" + BilledQty + " Kgs/ltr:" + ltr;
                tall.Rows.Add(no, VchDate, SupplierInvNo, SupDate, convSupplierName, PurchaseLedger, ItemName, BilledQty, Rate, conamount, ProcurementCortage, OtherIncome, ProcurementMaterialDebtorcsp, CatteleFeedSalesAgentscsp, CansLidsStock, BillAdvance, ledamount, Narration);
            }
            if (tall.Rows.Count > 1)
            {
                GridView2.DataSource = tall;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }
        catch
        {

        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        billdate();
    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}