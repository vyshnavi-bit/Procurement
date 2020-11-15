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

public partial class AgentMilkDashboard : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    DataTable DTT = new DataTable();
    DataTable FT = new DataTable();
    SqlConnection con = new SqlConnection();
    DbHelper db = new DbHelper();
    DataTable AGENTLOANCOUNT = new DataTable();
    string[] SINGLEAGENT;
    string PARTICUAGENT;
    string GETLOANID;
    int ii = 0;
    int jj = 6;
    int i = 0;
    int j = 0;
    string[] GETAGENTIDWITHNAME;
    string[] GETPLANTNAME;
    StringBuilder str = new StringBuilder();
     StringBuilder str1 = new StringBuilder();
     string coo;
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
                    Label6.Visible = false;
                    ddl_charttype.Visible = false;
              

                    dtm = System.DateTime.Now;
                    coo = Session["COUNT"].ToString();

                    try
                    {

                        coo = Session["COUNT"].ToString();

                    }
                    catch
                    {

                        coo = "0";

                    }

                    if (roleid < 3)
                    {
                        loadsingleplant();
                    
                     
                      if (coo == "1")
                      {
                 
                     ddl_Plantname.SelectedItem.Text = Session["DDLPLANT"].ToString();
                     getagnetdropdown();
                     ddl_AgentName.SelectedItem.Text = Session["DDLAGE"].ToString();
                     pcode = Session["pkkode"].ToString();  

                      }

                    }

                    if ((roleid >= 3) && ( roleid != 9))
                    {
                        LoadPlantcode();
                        if (coo == "1")
                        {

                            ddl_Plantname.SelectedItem.Text = Session["DDLPLANT"].ToString();
                            pcode = Session["pkkode"].ToString();

                            getagnetdropdown();
                            ddl_AgentName.SelectedItem.Text = Session["DDLAGE"].ToString();
                            pcode = Session["pkkode"].ToString();  
                         
                        }

                    }


                    if (roleid == 9)
                    {
                        loadspecialsingleplant();
                        if (coo == "1")
                        {

                            ddl_Plantname.SelectedItem.Text = Session["DDLPLANT"].ToString();
                            pcode = Session["pkkode"].ToString();

                            getagnetdropdown();
                            ddl_AgentName.SelectedItem.Text = Session["DDLAGE"].ToString();
                            pcode = Session["pkkode"].ToString();

                        }

                    }

                }
                else
                {



                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                //plant = ddl_Plantname.Text;
                //GET = plant.Split('_');
                pcode = ddl_Plantcode.SelectedItem.Value;

                if (coo == "1")
                {
                    pcode = Session["pkkode"].ToString(); 

                }


            }
        }

        catch
        {

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
                loadspecialsingleplant();


            }

            if (coo == "1")
            {

                ddl_Plantname.SelectedItem.Text = Session["DDLPLANT"].ToString();
                ddl_AgentName.SelectedItem.Text = Session["DDLAGE"].ToString();
                getagnetdropdown();

                getagetloandetails1();
                getagetloandetails2();
                getagetloandetails3();
                getagetloandetails4();
                getchartgrid();
            }

        }
    }

    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, pcode);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantcode.Items.Add("--Select PlantName--");
                ddl_Plantname.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void ddl_AgentName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
       // pcode = ddl_Plantcode.SelectedItem.Value;
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        getagnetdropdown();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {

        try
        {
            string GETID = ddl_AgentName.SelectedItem.Text;
            GETAGENTIDWITHNAME = GETID.Split('_');

            string PLANT = ddl_Plantname.SelectedItem.Text;
            GETPLANTNAME = PLANT.Split('_');
            getagetloandetails1();
            getagetloandetails2();
            getagetloandetails3();
            getagetloandetails4();
            getchartgrid();
        }
        catch
        {



        }

        Session["COUNT"] = "0".ToString();
    }
    public void getagnetdropdown()
    {
        con = db.GetConnection();
        ddl_AgentName.Items.Clear();
        string str = "";
        str = "select (AgentId  + '_' + Agent_Name) as AgentName    from ( Select CONVERT(varchar,Agent_Id) as AgentId,Agent_Name   from agent_master where plant_code='" + pcode + "') as am    order by RAND(AgentId)  asc";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddl_AgentName.Items.Add((dr["AgentName"]).ToString());

            }

        }
        else
        {


        }
        

    }
    public void getagetloandetails1()
    {
        con = db.GetConnection();
        string str = "";
        str = "select Agentid,Agent_Name,Route_Name,Bank_Name,Ifsc_code,Agent_AccountNo,Milk_Nature,phone_Number,Totloan,PendingLoan,Completedloan from (select Agentid,Agent_Name,Route_Name,phone_Number,Totloan,PendingLoan,Completedloan,Agent_AccountNo,Ifsc_code,Bank_Id,Milk_Nature  from(select * from (select totagent_Id as Agentid,Totloan,Completedloan,PendingLoan  from(select totagent_Id,Totloan,pendingloan  from ( select  COUNT(*)  as Totloan,agent_Id  as totagent_Id from LoanDetails   where plant_code='" + GETPLANTNAME[0][0] + "' and agent_Id='" + GETAGENTIDWITHNAME[0] + "' group by agent_Id ) as ld  left join (select  COUNT(*)  as pendingloan,agent_Id as cagents   from LoanDetails   where plant_code='" + GETPLANTNAME[0] + "' and agent_Id='" + GETAGENTIDWITHNAME[0] + "'  and balance	> 1 group by agent_Id) as lc on ld.totagent_Id=lc.cagents ) as totcage  left join(select  COUNT(*)  as Completedloan,agent_Id as penagent   from LoanDetails   where plant_code='" + pcode + "' and agent_Id='" + GETAGENTIDWITHNAME[0] + "'  and balance	< 1  GROUP by agent_Id) as lp  on totcage.totagent_Id= lp.penagent ) asldd left join(select Agent_Id,Agent_Name,Route_id,phone_Number,Agent_AccountNo,Ifsc_code,Bank_Id,Milk_Nature  from Agent_Master where Plant_code='" + GETPLANTNAME[0] + "' and Agent_Id='" + GETAGENTIDWITHNAME[0] + "' group by Agent_Id,Agent_Name,phone_Number,Route_id,Agent_AccountNo,Ifsc_code,Bank_Id,Milk_Nature) as am on asldd.Agentid=am.Agent_Id) as loanag left join(select Route_ID,Route_Name from Route_Master where plant_code='" + pcode + "' group by Route_ID,Route_Name ) as rm  on loanag.Route_id=rm.Route_ID        ) as leftside left join (select bank_id,bank_name from BANK_MASTER where plant_code='" + GETPLANTNAME[0][0] + "' group by bank_id,bank_name ) as bm on leftside.Bank_Id=bm.Bank_ID ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "Loanlist");
        DataTable DF= ds.Tables[0];
        if (DF.Rows.Count > 0)
        {
            DataTable DF1 = new DataTable();


            DF1.Columns.Add("Agents");
            DF1.Columns.Add("Details");
            DF1.Rows.Add("Canno");
            DF1.Rows.Add("AgentName");
            DF1.Rows.Add("Route_Name");
            DF1.Rows.Add("Bank_Name");
            DF1.Rows.Add("Ifsccode");
            DF1.Rows.Add("AccountNo");
            DF1.Rows.Add("MilkNature");
            DF1.Rows.Add("phone_Number");
            DF1.Rows.Add("Totloan");
            DF1.Rows.Add("PendingLoan");
            DF1.Rows.Add("Completedloan");
            GridView1.DataSource = DF1;
            GridView1.DataBind();

            for (int k = 0; k <= 10; k++)
            {

                GridView1.Rows[k].Cells[1].Text = DF.Rows[0][k].ToString();
                // j = j + 1;
            }


        }

        else
        {
             string str1 = "";
             str1 = "select Agent_Id,Agent_Name,Route_Name,Bank_Name,Ifsc_code,Agent_AccountNo,Milk_Nature,phone_Number from(select  *  from(select Agent_Id,Agent_Name,Route_id,phone_Number,Agent_AccountNo,Ifsc_code,Bank_Id,Milk_Nature  from Agent_Master where Plant_code='" + GETPLANTNAME[0] + "' and Agent_Id='" + GETAGENTIDWITHNAME[0] + "' group by Agent_Id,Agent_Name,phone_Number,Route_id,Agent_AccountNo,Ifsc_code,Bank_Id,Milk_Nature) as am  left join(select Route_ID as RouteID,Route_Name from Route_Master where plant_code='" + GETPLANTNAME[0] + "' group by Route_ID,Route_Name ) as rm  on am.Route_id=rm.RouteID  ) as leftside left join (select bank_id,bank_name from BANK_MASTER where plant_code='" + GETPLANTNAME[0] + "' group by bank_id,bank_name ) as bm on leftside.Bank_Id=bm.Bank_ID    ";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(ds, "Loanlist21");
        DataTable DF21= ds.Tables[1];
        
            DataTable DF112 = new DataTable();


            DF112.Columns.Add("Agents");
            DF112.Columns.Add("Details");
            DF112.Rows.Add("Canno");
            DF112.Rows.Add("AgentName");
            DF112.Rows.Add("Route_Name");
            DF112.Rows.Add("Bank_Name");
            DF112.Rows.Add("Ifsccode");
            DF112.Rows.Add("AccountNo");
            DF112.Rows.Add("MilkNature");
            DF112.Rows.Add("phone_Number");
            //DF1.Rows.Add("Totloan");
            //DF1.Rows.Add("PendingLoan");
            //DF1.Rows.Add("Completedloan");
            GridView1.DataSource = DF112;
            GridView1.DataBind();

            for (int k = 0; k <= 7; k++)
            {

                GridView1.Rows[k].Cells[1].Text = DF21.Rows[0][k].ToString();
                // j = j + 1;
            }



        }


    }



    public void getagetloandetails3()
    {
        con = db.GetConnection();
        string str = "";
        str = "   SELECT YEAR = YEAR(PRDATE), Month = UPPER(left(DATENAME(MONTH,PRDATE),3)),          Milkkg = (sum(mILK_KG)),            Milkltr = (sum(Milk_ltr)),         Fat = convert(decimal(18,2),(avg(Fat))),         Snf = convert(decimal(18,2),(avg(snf))),         Amount = (sum(amount)),         Commission = (sum(ComRate)),          AvgRate = convert(decimal(18,2),(avg(Rate))),         Chart=ratechart_id,          cartage = convert(decimal(18,1),((sum(CartageAmount))/SUM(Milk_ltr))),           SplBonus =  convert(decimal(18,1),((sum(SplBonusAmount))/SUM(Milk_ltr))),           milknature=milk_nature,         MilkSession = count(* ) FROM    Procurement WHERE   Plant_Code='" + GETPLANTNAME[0] + "'  AND  Agent_id='" + GETAGENTIDWITHNAME[0] + "' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE),ratechart_id,milk_nature ORDER BY YEAR ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "Loanlist12");
        GridView3.DataSource = ds.Tables[0];
        GridView3.DataBind();


    }

    public void getagetloandetails4()
    {
        con = db.GetConnection();
        string str = "";
        str = "  SELECT YEAR = YEAR(Frm_date), Month = UPPER(left(DATENAME(MONTH,Frm_date),3)),              Bank_Name =        Bank_Name,         AcNumber = (Agent_accountNo),                Ifsccode= Ifsc_code  FROM Paymentdata    WHERE  Plant_code='" + GETPLANTNAME[0] + "' AND Agent_id='" + GETAGENTIDWITHNAME[0] + "' and Frm_date > '12-30-2015'    GROUP BY YEAR(Frm_date),          MONTH(Frm_date),          DATENAME(MONTH,Frm_date),Agent_accountNo,Bank_Name,Agent_accountNo,Ifsc_code         ORDER BY YEAR ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "Loanlist121");
        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();


    }

    public void getagetloandetails2()
    {

        con = db.GetConnection();
        string op = "select  ( lm.agent_Id) as Canno,CONVERT(VARCHAR,loandate,103) AS LoanDate,loan_Id as LoanId,CONVERT(DECIMAL(18,2),loanamount) AS IssuedAmount,NoofInstallment  as  NoofEmI  from (select  CONVERT(varchar,(agent_Id)) as Agent_Id ,loandate,loan_Id,loanamount,NoofInstallment  from LoanDetails   where plant_code='" + GETPLANTNAME[0] + "' and agent_Id='" + GETAGENTIDWITHNAME[0] + "' ) as lm  left join (select   Agent_Id,Agent_Name    from  Agent_Master where plant_code='" + GETPLANTNAME[0] + "' and agent_Id='" + GETAGENTIDWITHNAME[0] + "') as  am on lm.agent_Id= am.Agent_Id  ORDER BY LoanId  ";
        SqlCommand cmd = new SqlCommand(op, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // da.Fill(
        da.Fill(AGENTLOANCOUNT);
        AGENTLOANCOUNT.Columns.Add("paidAmt");
        AGENTLOANCOUNT.Columns.Add("CashRecAmt");
        AGENTLOANCOUNT.Columns.Add("PaidEmI");
        AGENTLOANCOUNT.Columns.Add("PendingEmI");
        AGENTLOANCOUNT.Columns.Add("Balance");


        if (AGENTLOANCOUNT.Rows.Count > 0)
        {
            GridView2.DataSource = AGENTLOANCOUNT;
            GridView2.DataBind();


        }
        else
        {
            GridView2.DataSource = "";
            GridView2.DataBind();

        }

        foreach (DataRow dr in AGENTLOANCOUNT.Rows)
        {

            string str = dr[0].ToString();
            SINGLEAGENT = str.Split('_');
            PARTICUAGENT = SINGLEAGENT[0];
            GETLOANID = dr[2].ToString();




            string AGENTpaidamount = "Select  isnull(SUM(paid_Amount),0) as paidAmount  from Loan_Recovery   where plant_code='" + GETPLANTNAME[0] + "'  and agent_Id='" + PARTICUAGENT + "' AND Loan_id='" + GETLOANID + "'";
            SqlCommand sc1 = new SqlCommand(AGENTpaidamount, con);
            SqlDataAdapter da2 = new SqlDataAdapter(sc1);
            DataSet DS1 = new DataSet();
            da2.Fill(DS1, "PAIDLOAN");
            Session["AGENTpaidamount"] = DS1.Tables[0].Rows[0][0].ToString();
            string cashreceipt = "Select  isnull(SUM(LoanDueRecovery_Amount),0) as CashReceived_Amount  from LoanDue_Recovery WHERE   plant_code='" + GETPLANTNAME[0] + "'  and agent_Id='" + PARTICUAGENT + "' AND Loan_id='" + GETLOANID + "'";
            SqlCommand sc2 = new SqlCommand(cashreceipt, con);
            SqlDataAdapter da3 = new SqlDataAdapter(sc2);
            //  DataSet ds2 = new DataSet();
            da3.Fill(DS1, "CASHAMOUNT");
            Session["AGENTcashreceipt"] = DS1.Tables[1].Rows[0][0].ToString();

            double PAID = Convert.ToDouble(Session["AGENTpaidamount"].ToString());
            double CASH = Convert.ToDouble(Session["AGENTcashreceipt"].ToString());

            GridView2.Rows[ii].Cells[jj].Text = PAID.ToString("f2");
            jj = jj + 1;
            GridView2.Rows[ii].Cells[jj].Text = CASH.ToString("f2");
            int install;
            double LOANAMT = Convert.ToDouble(GridView2.Rows[ii].Cells[4].Text);

            try
            {
                install = Convert.ToInt16(GridView2.Rows[ii].Cells[5].Text);
            }
            catch
            {
                install = 0;

            }
            double PAIDAMT = Convert.ToDouble(GridView2.Rows[ii].Cells[6].Text);
            double CASHREC = Convert.ToDouble(GridView2.Rows[ii].Cells[7].Text);

            double pendininst = Convert.ToDouble(LOANAMT / install);
            double addpaiamtCASHREC = (PAIDAMT + CASHREC);
            string GETRES = (LOANAMT - (PAIDAMT + CASHREC)).ToString("f2");
            double getres1 = Convert.ToDouble(GETRES);

            int assingemi = Convert.ToInt32(getres1 / pendininst);

            GridView2.Rows[ii].Cells[8].Text = (install - assingemi).ToString();

            GridView2.Rows[ii].Cells[9].Text = assingemi.ToString();
            GridView2.Rows[ii].Cells[10].Text = GETRES.ToString();

            GridView2.Rows[ii].Cells[10].Font.Bold = true;

            if (getres1 > 1)
            {
                GridView2.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkOrange;

            }

            if (getres1 > 100000.00)
            {
                GridView2.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkRed;

            }
            if (getres1 < 1)
            {
                GridView2.Rows[ii].Cells[10].ForeColor = System.Drawing.Color.DarkGreen;


            }

            ii = ii + 1;
            jj = 6;

        }

    }


//    public void getchartgrid()
//    {
//         con = db.GetConnection();
      
//         string[] dpl = ddl_AgentName.SelectedItem.Text.Split('_');
//         string getagen = dpl[0].ToString();
//        string get = "";
//        get = "SELECT YEAR = YEAR(PRDATE),        MONTH = MONTH(PRDATE),        MMM = UPPER(left(DATENAME(MONTH,PRDATE),3)),        Sales = (sum(mILK_KG)),        OrderCount = count(* ) FROM    Procurement WHERE   Plant_Code='" + pcode + "'  AND  Agent_id='" + getagen + "' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE) ORDER BY YEAR,          MONTH ";
//        SqlCommand cmd = new SqlCommand(get, con);
//        SqlDataAdapter da = new SqlDataAdapter(cmd);
//      //  datatable dt = new datatable();
      
//        da.Fill(DTT);
      
//        FT.Columns.Add("MONTH");
//        FT.Columns.Add("mILKKG");
//        //FT.Columns.Add("sesscount");

//        foreach (DataRow dr in DTT.Rows)
//        {

//            string getmonth = dr[2].ToString();
//            string milkkg = dr[3].ToString();
//            //string totsess = dr[4].ToString();
//         //   FT.Rows.Add(getmonth, milkkg, totsess);
//            FT.Rows.Add(getmonth, milkkg);
//        }

//        if (DTT.Rows.Count > 0)
//        {

//            Label6.Visible = true;
//            ddl_charttype.Visible = true;

//        }

//              str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
//                google.setOnLoadCallback(drawChart);
//                function drawChart() {
//               
//                    var data = new google.visualization.DataTable();
//                    data.addColumn('string', 'Milk Kg');
//                    data.addColumn('number', 'MonthWise');
////                  data.addColumn('number', 'Expenses');
//                    data.addRows(" + FT.Rows.Count + ");");

//        Int32 i; 


//           for (i = 0; i <= FT.Rows.Count - 1; i++)
//            {
//                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + FT.Rows[i]["MONTH"].ToString() + "');");
//                str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["mILKKG"].ToString() + ") ;");
//                //str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["sesscount"].ToString() + ") ;");
//                //str.Append(" data.setValue(" + i + "," + 2 + "," + FT.Rows[i]["expences"].ToString() + ");");
//            }
//            //  str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});

//          // str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
//           // 
//           // draw(data, { title: "Google Chart demo" });

//           if (ddl_charttype.Text == "ColumnChart")
//         {
            


//          //  str.Append("var barchart = new google.visualization.ComboChart(document.getElementById('bar_element'));");
           

//         }
//         //if (ddl_charttype.Text == "PieChart")
//         //{
//         //    str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

//         //}
//         //if (ddl_charttype.Text == "LineChart")
//         //{

//         //    str.Append("var chart =   new google.visualization.LineChart(document.getElementById('visualization'));");
//         //}
//         //if (ddl_charttype.Text == "ColumnChart")
//         //{
//         //    str.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('visualization'));");

//         //}
//         //if (ddl_charttype.SelectedItem.Value == "AreaChart")
//         //{
//         //    str.Append("var chart =   new google.visualization.AreaChart(document.getElementById('visualization'));");

//         //}
//         //if (ddl_charttype.SelectedItem.Value == "SteppedAreaChart")
//         //{
//         //    str.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('visualization'));");

//         //}
//         //if (ddl_charttype.SelectedItem.Value == "0")
//         //{
//         //    str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

//         //}



//   //   str.Append("  var chart =   new google.visualization.ColumnChart(document.getElementById('visualization'));");
     

//   //   str.Append("chart.draw(draw(data,{title:"Parc localisé par région et par offre", width:500, height:300,series: {0:{color: '#CF0980', visibleInLegend: true}, 1:{color: '#999999', visibleInLegend: true}, 2:{color: '#990099', visibleInLegend: true}, 3:{color: '#FF99CC', visibleInLegend: true}},isStacked:"true"});}");


//      str.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
//          str.Append("chart.draw(data , {width: 750,   colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'], visibleInLegend: true,lineWidth:5,rotatevalues:1, showplotborder:1,  height: 300, legend: 'bottom', animation: false,is3D: true,pointSize:5,isStacked:'true',title: 'Agent Milk Performance',");
            
//            str.Append("vAxis: {title: 'Milk Kg',  titleTextStyle: {color: 'green'}}");
//            str.Append("}); }");
//          //  str.Append("var options = {series: [{ type: 'bars' }, { lineWidth: 0, visibleInLegend:false, pointSize: 0}]};");

//            str.Append("</script>");

//            lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');

            
//        }

    public void getchartgrid()
    {
        con = db.GetConnection();
        string PLANT = ddl_Plantname.SelectedItem.Text;
        GETPLANTNAME = PLANT.Split('_');
        string[] dpl = ddl_AgentName.SelectedItem.Text.Split('_');
        string getagen = dpl[0].ToString();
       // string get = "";
        string get = "SELECT YEAR = YEAR(PRDATE),        MONTH = MONTH(PRDATE),        MMM = UPPER(left(DATENAME(MONTH,PRDATE),3)),        Sales = (sum(mILK_KG)),        OrderCount = count(* ) FROM    Procurement WHERE   Plant_Code='" + GETPLANTNAME[0] + "'  AND  Agent_id='" + getagen + "' GROUP BY YEAR(PRDATE),          MONTH(PRDATE),          DATENAME(MONTH,PRDATE) ORDER BY YEAR,          MONTH ";
        SqlCommand cmd = new SqlCommand(get, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  datatable dt = new datatable();

        da.Fill(DTT);

        FT.Columns.Add("MONTH");
        FT.Columns.Add("mILKKG");
        //FT.Columns.Add("sesscount");

        foreach (DataRow dr in DTT.Rows)
        {

            string getmonth = dr[2].ToString();
            string milkkg = dr[3].ToString();
            //string totsess = dr[4].ToString();
            //   FT.Rows.Add(getmonth, milkkg, totsess);
            FT.Rows.Add(getmonth, milkkg);
        }

        if (DTT.Rows.Count > 0)
        {

            Label6.Visible = true;
            ddl_charttype.Visible = true;

        }

        str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
                google.setOnLoadCallback(drawChart);
                function drawChart() {
               
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Milk Kg');
                    data.addColumn('number', 'MonthWise');
//                    data.addColumn('number', 'Expenses');
                    data.addRows(" + FT.Rows.Count + ");");

        Int32 i;


        for (i = 0; i <= FT.Rows.Count - 1; i++)
        {
            str.Append("data.setValue( " + i + "," + 0 + "," + "'" + FT.Rows[i]["MONTH"].ToString() + "');");
            str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["mILKKG"].ToString() + ") ;");
            //str.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["sesscount"].ToString() + ") ;");
            //str.Append(" data.setValue(" + i + "," + 2 + "," + FT.Rows[i]["expences"].ToString() + ");");
        }
        //  str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});

        // str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
        // 
        // draw(data, { title: "Google Chart demo" });

        if (ddl_charttype.Text == "BarChart")
        {
            str.Append("var chart =   new google.visualization.BarChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.Text == "PieChart")
        {
            str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.Text == "LineChart")
        {

            str.Append("var chart =   new google.visualization.LineChart(document.getElementById('visualization'));");
        }
        if (ddl_charttype.Text == "ColumnChart")
        {
            str.Append("var chart =   new google.visualization.ColumnChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "AreaChart")
        {
            str.Append("var chart =   new google.visualization.AreaChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "SteppedAreaChart")
        {
            str.Append("var chart =   new google.visualization.SteppedAreaChart(document.getElementById('visualization'));");

        }
        if (ddl_charttype.SelectedItem.Value == "0")
        {
            str.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");

        }

        //str.Append("var chart =   new google.visualization.LineChart(document.getElementById('visualization'));");

        //  str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
      //  str.Append("chart.draw(data, {width: 750, colors: ['#FFA500', '#0000ff', '#ff0000', '#00ff00'],lineWidth:5, height: 300, legend: 'bottom',is3D: true,pointSize:7,title: 'Performance',");

        str.Append("chart.draw(data, {width: 750,lineWidth:5, height: 300, legend: 'bottom',is3D: true,pointSize:7,title: 'Performance',");

        str.Append("vAxis: {title: 'Milk Kg', titleTextStyle: {color: 'green'}}");
        str.Append("}); }");


        str.Append("</script>");

        lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');



        //for (i = 0; i <= FT.Rows.Count - 1; i++)
        //{
        //    str1.Append("data.setValue( " + i + "," + 0 + "," + "'" + FT.Rows[i]["MONTH"].ToString() + "');");
        //    str1.Append("data.setValue(" + i + "," + 1 + "," + FT.Rows[i]["mILKKG"].ToString() + ") ;");
        //    //str.Append(" data.setValue(" + i + "," + 2 + "," + FT.Rows[i]["expences"].ToString() + ");");
        //}
        ////  str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});

        //// str.Append("var chart = new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
        //// 
        //// draw(data, { title: "Google Chart demo" });
        //// str1.Append("var chart =   new google.visualization.PieChart(document.getElementById('visualization'));");
        //str1.Append("var chart = new google.visualization.ColumnChart(document.getElementById('divLineChart'));");
        //str.Append("chart.draw(data, {width: 650, height: 300, legend: 'bottom',is3D: true,title: 'Performance',");

        //str1.Append("vAxis: {title: 'Year', titleTextStyle: {color: 'green'}}");
        //str1.Append("}); }");
        //str1.Append("</script>");

        //lt1.Text = str1.ToString().TrimEnd(',').Replace('*', '"');

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_AgentName_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

                HeaderCell2.Text ="PlantName:" + ddl_Plantname.Text;
                HeaderCell2.ColumnSpan = 2;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    protected void GridView4_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

                HeaderCell2.Text = "Bank Account Number Details";
                HeaderCell2.ColumnSpan = 6;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView4.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

                HeaderCell2.Text = "Loan Running And Stopped Details";
                HeaderCell2.ColumnSpan = 11;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView2.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    protected void GridView3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                //   HeaderCell2.Text = ddl_Plantname.Text +":"+ Session["ID"].ToString() + "_" + Session["NAME"].ToString() + "Loan Details:";

                HeaderCell2.Text = "Milk OverAll  Details";
                HeaderCell2.ColumnSpan = 14;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);
                GridView3.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string getmil = e.Row.Cells[12].Text;
            if (getmil == "1")
            {


                e.Row.Cells[12].Text ="Cow";
            }
            else
            {
                 e.Row.Cells[12].Text= "Buffalo";

            }
        }
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
       
    }
    protected void ddl_charttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        getchartgrid();
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      


        //decimal d = (decimal)1.123;
        //if ((d % 1) > 0)
        //{
        //    //is decimal
        //}
        //else
        //{
        //    //is int
        //}
    }
    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GETID = ddl_AgentName.Text;
        GETAGENTIDWITHNAME = GETID.Split('_');

        string PLANT = ddl_Plantname.SelectedItem.Text;
        GETPLANTNAME = PLANT.Split('_');

        Session["YEAR"] =   GridView4.SelectedRow.Cells[1].Text.ToString();
        Session["MONTH"] = GridView4.SelectedRow.Cells[2].Text.ToString();
        Session["pkkode"] = GETPLANTNAME[0].ToString();

        Session["Agentidc"] = ddl_AgentName.SelectedItem.Text;
        Session["pnametxt"] = ddl_Plantname.SelectedItem.Text;
        Response.Redirect("AgentsBills.aspx");
    }
}