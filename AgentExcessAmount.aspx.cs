using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class AgentExcessAmount : System.Web.UI.Page
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
    DataTable checkdt = new DataTable();
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
    int plantstatus;
    string narration;

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

            string fn = ddl_Plantname.SelectedItem.Text + "" + FDATE + "" + TODATE;
            Session["filename"] = fn;
            Response.Redirect("exporttoxl.aspx");


            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment; filename= '" + ddl_Plantname.SelectedItem.Text + ":Bill Period" + FDATE + "To:" + TODATE + "'.xls");
            //Response.ContentType = "application/excel";
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //GridView2.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();
        }
        catch
        {
        }
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        getreports();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

        try
        {
            checkdata();
            if (checkdt.Rows.Count > 1)
            {
                getreports();
            }
            else
            {
                getagentlist();
                getreports();
            }
        }
        catch
        {

        }
    }

    public void getreports()
    { 
        DataTable Report = new DataTable();
        Report.Columns.Add("vchno");
        Report.Columns.Add("VchDate");
        Report.Columns.Add("SuppinvNo");
        Report.Columns.Add("SupplierDate");
        Report.Columns.Add("SupplierName");
        Report.Columns.Add("purchaseLedger");
        Report.Columns.Add("ItemName");
        Report.Columns.Add("kgs");
        Report.Columns.Add("Addedpaise");
        Report.Columns.Add("Amount");
        Report.Columns.Add("Cortage");
        Report.Columns.Add("OtherIncome");
        Report.Columns.Add("Material");
        Report.Columns.Add("Feed");
        Report.Columns.Add("Cans");
        Report.Columns.Add("Dpu");
        Report.Columns.Add("LedgerAmount");
        Report.Columns.Add("Narration");

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


        SqlConnection con = new SqlConnection();
        con = DB.GetConnection();
        string sttt = "";
        sttt = "Select   vchno,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],SuppinvNo, REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [SupplierDate], SupplierName, purchaseLedger, ItemName, TotFatkg as Kgs, Addedpaise, Amount,Cortage,OtherIncome,Material,Feed,Cans,Dpu,Amount AS LedgerAmount,Narration  from   TallyExcessAmount where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and frm_date='" + FDATE + "'   and to_date='" + TODATE + "'";
       SqlCommand cmd = new SqlCommand(sttt, con);
        SqlDataAdapter drt = new SqlDataAdapter(cmd);
        DataTable displaygrid = new DataTable();
        displaygrid.Rows.Clear();
        drt.Fill(displaygrid);
        if (displaygrid.Rows.Count > 0)
        {
            foreach (DataRow dr in displaygrid.Rows)
            {
                DataRow newrow = Report.NewRow();
                newrow["vchno"] = dr["vchno"].ToString();
                newrow["VchDate"] = dr["VchDate"].ToString();
                newrow["SuppinvNo"] = dr["SuppinvNo"].ToString();
                newrow["SupplierDate"] = dr["SupplierDate"].ToString();
                newrow["SupplierName"] = dr["SupplierName"].ToString();
                newrow["purchaseLedger"] = dr["purchaseLedger"].ToString();
                newrow["ItemName"] = dr["ItemName"].ToString();
                newrow["kgs"] = dr["Kgs"].ToString();
                newrow["Addedpaise"] = dr["Addedpaise"].ToString();
                newrow["Amount"] = dr["Amount"].ToString();
                newrow["Cortage"] = dr["Cortage"].ToString();
                newrow["OtherIncome"] = dr["OtherIncome"].ToString();
                newrow["Material"] = dr["Material"].ToString();
                newrow["Feed"] = dr["Feed"].ToString();
                newrow["Cans"] = dr["Cans"].ToString();
                newrow["Dpu"] = dr["Dpu"].ToString();
                newrow["LedgerAmount"] = dr["LedgerAmount"].ToString();
                newrow["Narration"] = dr["Narration"].ToString();
                Report.Rows.Add(newrow);
            }
            Session["xportdata"] = Report;
            GridView2.DataSource = Report;
            GridView2.DataBind();
        }

        else
        {
            GridView2.DataSource = "";
            GridView2.DataBind();
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

        ViewState["ffdate"] = FDATE;
        ViewState["ttdate"] = TODATE;
        string str = "";
        //Paymentdata
        str = " Select   agent_id,(Agent_id + '-'+ Agent_Name + '-' + Plant_Name) as Suppliername,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],Plant_Name as  Purchaseledgerofmilk,totfat_kg,Added_paise,TotAmount,floor(TotAmount) as RTotAmount,Frm_date,To_date,Plant_Code from(  sELECT convert(varchar,Agent_id) as Agent_id, convert(varchar,Agent_Name) as Agent_Name,totfat_kg,Added_paise,TotAmount,Frm_date,To_date,Plant_code    FROM (Select  Agent_id,Plant_code,totfat_kg,Added_paise,TotAmount,Frm_date,To_date   from AgentExcesAmount  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + FDATE + "'   and To_date='" + TODATE + "'   ) AS  agexee  left join (Select Agent_Id as amagentid,Agent_Name,Plant_code as ampcode  from Paymentdata   where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + FDATE + "'   and To_date='" + TODATE + "') as am  on agexee.Plant_code=am.ampcode and agexee.Agent_id=am.amagentid) as leftside left join (select Plant_Code as pmcode,Plant_Name   from Plant_Master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by Plant_Code,Plant_Name ) as pm  on leftside.Plant_code=pm.pmcode";



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



                string sno = ViewState["maxtid"].ToString();
                string agentid = tally[0].ToString();
                string tempsupply = tally[1].ToString();
                string Supplierinvno = tempsupply + "(" + getvald + "-" + getvaldd + ")" + month + getvaly;
                string vchdate = tally[2].ToString();
                string Suppliername = tempsupply;
                string purchasemilk ="Purchase Of Milk-" + tally[3].ToString();
                

                string ItemName;
                if ((pcode == "155") || (pcode == "156") || (pcode == "158") || (pcode == "159") || (pcode == "161") || (pcode == "162") || (pcode == "163") || (pcode == "164"))
                {
                    ItemName = "COW MILK";
                    plantstatus = 1;
                  //  string narration = "Being The Svds  arani cc Procurement Milk Bill Excess Rate Variation Amount Paid For the Period   Of  Date  ,Paid through bank    svdd punapaka hdfc account no";
                  
                }
                else
                {

                    ItemName = "WHOLE MILK";
                    plantstatus = 2;
                }

                string totfatkg = tally[4].ToString();
                string addedpaise = tally[5].ToString();
                string amount = tally[6].ToString();
                string roundamount = tally[7].ToString();
               
                if (plantstatus == 1)
                {
                    narration = "Being the Svds Milk Purchase Amount Excess Amount Paid Period Of:" + ddl_BillDate.Text + "Fatkg:" + totfatkg + "Rate:" + addedpaise + "TotalAmount:" + amount;
                }
                if (plantstatus == 2)
                {
                    narration = "Being the Svd Milk   Purchase Amount Excess Amount Paid Period Of:" + ddl_BillDate.Text + "Fatkg:" + totfatkg + "Rate:" + addedpaise + "TotalAmount:" + amount;
                }
                string gettp = ViewState["maxtid"] + Tallyno +"EXRATE";
                string GETINSERT = "";
                GETINSERT = "INSERT INTO TallyExcessAmount(sno,vchno,vchdate,SuppinvNo,SupplierDate,SupplierName,purchaseLedger,ItemName,TotFatkg,Addedpaise,Amount,Cortage,OtherIncome,Material,Feed,Cans,Dpu,LedgerAmount,Narration,InsertedUser,plant_code,agent_id,Frm_date,To_date)  values                   ('" + ViewState["maxtid"] + "','" + gettp + "','" + vchdate + "','" + Supplierinvno + "','" + vchdate + "','" + Suppliername + "','" + purchasemilk + "','" + ItemName + "','" + totfatkg + "','" + addedpaise + "','" + roundamount + "','0','0','0','0','0','0','" + roundamount + "','" + narration + "','" + Session["Name"].ToString() + "','" + ddl_Plantname.SelectedItem.Value + "','" + agentid + "','" + ViewState["ffdate"].ToString() + "','" + ViewState["ttdate"].ToString() + "');";
                con = DB.GetConnection();
                SqlCommand COMM = new SqlCommand(GETINSERT, con);
                COMM.ExecuteNonQuery();

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
            //string strrr = "Select * from TallypurchaseImportAgentWsie  where plant_code='" + pcode + "'  and  FrmDate='" + FDATE + "'  and Todate='" + TODATE + "' ";
            string strrr = "Select * from TallyExcessAmount  where plant_code='" + pcode + "'  and  frm_date='" + FDATE + "'  and to_date='" + TODATE + "'";
            SqlCommand cmdnew = new SqlCommand(strrr, con);
            SqlDataAdapter DAnew = new SqlDataAdapter(cmdnew);
            checkdt.Rows.Clear();
            DAnew.Fill(checkdt);
            //if (checkdt.Rows.Count > 1)
            //{

            //    Checkstatus = 1;

            //}
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
                string tid = "Select max(Sno)  as VchNo  from  TallyExcessAmount  WHERE plant_code='" + pcode + "' ";
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

                ViewState["maxtid"] = 100;

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
            //string GETSTR = "SELECT TallyVchNo ,REPLACE(CONVERT(VARCHAR(9), VchDate, 6), ' ', '-') AS [VchDate],SupplierinvNo,SupplierinvDate,SupplierName,PurchaseLedger,ItemName,Billedqty,Rate,Amount,Cartage,otherincome,Material,Feed,CanorStock,BillAdvance,AvgFat,Avgsnf,Ts,Rts,Kgfat,KgSnf,Kgrate   FROM TallypurchaseImportAgentWsie WHERE PLANT_CODE='" + pcode + "' AND  FrmDate='" + FDATE + "' and Todate='" + TODATE + "'  ORDER BY VchNo asc   ";
            string GETSTR = " Select   agent_id,(Agent_id + '-'+ Agent_Name + '-' + Plant_Name) as Suppliername,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],Plant_Name as  Purchaseledgerofmilk,totfat_kg,Added_paise,TotAmount,Frm_date,To_date,Plant_Code from(  sELECT convert(varchar,Agent_id) as Agent_id, convert(varchar,Agent_Name) as Agent_Name,totfat_kg,Added_paise,TotAmount,Frm_date,To_date,Plant_code    FROM (Select  Agent_id,Plant_code,totfat_kg,Added_paise,TotAmount,Frm_date,To_date   from AgentExcesAmount  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + FDATE + "'   and To_date='" + TODATE + "'   ) AS  agexee  left join (Select Agent_Id as amagentid,Agent_Name,Plant_code as ampcode  from Agent_Master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "') as am  on agexee.Plant_code=am.ampcode and agexee.Agent_id=am.amagentid) as leftside left join (select Plant_Code as pmcode,Plant_Name   from Plant_Master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by Plant_Code,Plant_Name ) as pm  on leftside.Plant_code=pm.pmcode   ";
           
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
            //string ProcurementCortage1 = "ProcurementCortage-" + ddl_Plantname.SelectedItem.Text;
            //tall1.Columns.Add(ProcurementCortage1);
            //string OtherIncome1 = "OtherIncome-" + ddl_Plantname.SelectedItem.Text;
            //tall1.Columns.Add(OtherIncome1);
            //string ProcurementMaterialDebtor = "ProcurementMaterial-Debtor-" + ddl_Plantname.SelectedItem.Text;
            //tall1.Columns.Add(ProcurementMaterialDebtor);
            //string CatteleFeedSalesAgentscs = "CattleFeedSales-Agents-" + ddl_Plantname.SelectedItem.Text;
            //tall1.Columns.Add(CatteleFeedSalesAgentscs);
            //string getstringval = "Cans&LidsStock-" + ddl_Plantname.SelectedItem.Text;
            //tall1.Columns.Add(getstringval);

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

                //string no = drtally[0].ToString();
                //string VchDate = drtally[1].ToString();
                //string SupplierInvNo = drtally[2].ToString();
                //string SupDate = drtally[1].ToString();
                ////    string SupplierName = drtally[4].ToString() + "Route Milk Bills" + ddl_Plantname.SelectedItem.Text;
                //string SupplierName = drtally[4].ToString();
                //string convSupplierName = SupplierName;
                //string PurchaseLedger = drtally[5].ToString();

                //BILLDATE = drtally[4].ToString() + "(" + getvald + "-" + getvaldd + ")" + month + getvaly;
                //ViewState["BILLDATE"] = BILLDATE.ToString();

                //     string ItemName = drtally[6].ToString();


                string vchno = drtally[0].ToString();
                string vchdate = drtally[1].ToString();
                string SupplierInvNo = drtally[2].ToString();
                string SupDate = drtally[3].ToString();
                string convSupplierName = drtally[4].ToString();
                string PurchaseLedger = drtally[5].ToString();
                string itemname = drtally[6].ToString();
                string totfatkg = drtally[7].ToString();
                string rate = drtally[8].ToString();
                string amount = drtally[9].ToString();
                string ledger = drtally[10].ToString();
                string narrartion = drtally[11].ToString();
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
                tall1.Rows.Add(vchno, vchdate, SupplierInvNo, SupDate, convSupplierName, PurchaseLedger, ItemName, BilledQty, Rate, conamount, ProcurementCortage, OtherIncome, ProcurementMaterialDebtorcsp, CatteleFeedSalesAgentscsp, CansLidsStock, BillAdvance, ledamount, Narration);

            }

            if (tall1.Rows.Count > 1)
            {
                Session["xportdata"] = tall1;
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
           // string GETSTR = "SELECT TallyVchNo ,REPLACE(CONVERT(VARCHAR(9), VchDate, 6), ' ', '-') AS [VchDate],SupplierinvNo,SupplierinvDate,SupplierName,PurchaseLedger,ItemName,Billedqty,Rate,Amount,Cartage,otherincome,Material,Feed,CanorStock,BillAdvance,AvgFat,Avgsnf,Ts,Rts,Kgfat,KgSnf,Kgrate   FROM TallypurchaseImportAgentWsie WHERE PLANT_CODE='" + pcode + "' AND  FrmDate='" + ViewState["FD"] + "' and Todate='" + ViewState["TTODATE"] + "'  ORDER BY VchNo asc   ";
            string GETSTR = " Select   agent_id,(Agent_id + '-'+ Agent_Name + '-' + Plant_Name) as Suppliername,REPLACE(CONVERT(VARCHAR(9), To_date, 6), ' ', '-') AS [VchDate],Plant_Name as  Purchaseledgerofmilk,totfat_kg,Added_paise,TotAmount,Frm_date,To_date,Plant_Code from(  sELECT convert(varchar,Agent_id) as Agent_id, convert(varchar,Agent_Name) as Agent_Name,totfat_kg,Added_paise,TotAmount,Frm_date,To_date,Plant_code    FROM (Select  Agent_id,Plant_code,totfat_kg,Added_paise,TotAmount,Frm_date,To_date   from AgentExcesAmount  where plant_code='" + ddl_Plantname.SelectedItem.Value + "' and Frm_date='" + FDATE + "'   and To_date='" + TODATE + "'   ) AS  agexee  left join (Select Agent_Id as amagentid,Agent_Name,Plant_code as ampcode  from Agent_Master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "') as am  on agexee.Plant_code=am.ampcode and agexee.Agent_id=am.amagentid) as leftside left join (select Plant_Code as pmcode,Plant_Name   from Plant_Master   where plant_code='" + ddl_Plantname.SelectedItem.Value + "'  group by Plant_Code,Plant_Name ) as pm  on leftside.Plant_code=pm.pmcode  ORDER BY VchNo asc   ";
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
                Session["xportdata"] = tall;
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}