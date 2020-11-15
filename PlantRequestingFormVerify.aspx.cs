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
public partial class PlantRequestingFormVerify : System.Web.UI.Page
{


    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    DbHelper db = new DbHelper();
    int I;
    string getplant;
    string getAgent;
    string Date;
    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    msg MESS = new msg();
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    string FDATE;
    string TODATE;
    DateTime d1;
    DateTime d2;
    string DATE;
    string DATE1;
    string DATE2; // JAN
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
                    Session["tempp"] = Convert.ToString(pcode);
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    //    GETGRID();

                    btn_Save.Visible = false;
                    if (roleid < 3)
                    {
                        loadsingleplant();
                        GETGRID();
                     
                        btn_Save.Visible = false;
                    }

                    if ((roleid >= 3) && (roleid != 9))
                    {

                        LoadPlantcode();
                        GETGRID();
                       btn_Save.Visible = true;
                       
                    }
                    if (roleid == 9)
                    {

                        Session["Plant_Code"] = "170".ToString();
                        pcode = "170";
                        loadspecialsingleplant();
                        GETGRID1();
                        btn_Save.Visible = true;

                    }
                }
                else
                {



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
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
         
        if (roleid < 3)
        {

        
            btn_Save.Visible = false;
        }

        else
        {

            
           
            btn_Save.Visible = true;

        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {

                    int refid = Convert.ToInt16(row.Cells[1].Text);
                    string plantcode = row.Cells[2].Text;
                    string plantname = row.Cells[3].Text;
                    con = DB.GetConnection();
                    string get = "Update PlantRegularRequest set  Status=1,Authorised='" + Session["Name"] + "'    where  tid='" + refid + "'  and plant_code='" + plantcode + "' ";
                    SqlCommand cmd = new SqlCommand(get, con);
                    cmd.ExecuteNonQuery();
                   
                     
                }
            }
        }
        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.update) + "')</script>");
        if (roleid == 9)
        {
            GETGRID1();
        }
        else
        {
            GETGRID();
        }
     }
    public void GETGRID()
    {
        con = db.GetConnection();


        //  string loanamt = "SELECT  Plant_code,Plant_code as PlantName,Agent_id,Agent_id as AgentName,CONVERT(VARCHAR,RequestingDate,103) AS RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi   FROM LoanRequestEntry    WHERE  ManStatus='1' AND RmStatus=0   order by tid  desc ";
        // string loanamt = "select Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT  Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE PLANT_CODE='"+pcode+"' AND ManStatus=1 AND RmStatus!=1 ) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='"+pcode+"' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='165' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        //   string loanamt = " select Tid AS RefId, Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT Tid,lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT Tid,Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE Plant_code='" + pcode + "' AND ManStatus=1 and  RmStatus NOT IN(1)) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='" + pcode + "' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='" + pcode + "' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        string loanamt;
        if (roleid > 3)
        {
            loanamt = "select TID AS RefId,pp.Plant_Code,Plant_Name,Dept,remarks,CONVERT(VARCHAR,RequesterDate,103) as RequesterDate,convert(varchar(20),Date,113) AS  Date,Login AS LoginUser,Requester     from (select TID,plant_code,Dept,remarks,RequesterDate,Date,Login,Requester    from    PlantRegularRequest where status='0' group by plant_code,remarks,RequesterDate,Date,Login,Requester,TID,Dept) as pp left join (Select Plant_Code,Plant_Name    from Plant_Master  group by Plant_Code,Plant_Name) as pm on pp.plant_code= pm.Plant_Code ";
        }
        else
        {
            loanamt = "select TID AS RefId,pp.Plant_Code,Plant_Name,Dept,remarks,CONVERT(VARCHAR,RequesterDate,103) as RequesterDate,convert(varchar(20),Date,113) AS  Date,Login AS LoginUser,Requester     from (select   TID,plant_code,Dept,remarks,RequesterDate,Date,Login,Requester    from    PlantRegularRequest where status='0'  and plant_code='" + pcode + "' group by plant_code,remarks,RequesterDate,Date,Login,Requester,TID,Dept) as pp left join (Select Plant_Code,Plant_Name    from Plant_Master  where plant_code='" + pcode + "' group by Plant_Code,Plant_Name) as pm on pp.plant_code= pm.Plant_Code ";

        }
        SqlCommand sc = new SqlCommand(loanamt, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        da1.Fill(ds, "TABLEop");
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {

            btn_Save.Visible = true  ;
        }
        else
        {

            btn_Save.Visible = false;
        }
    }

    public void GETGRID1()
    {
        con = db.GetConnection();


        //  string loanamt = "SELECT  Plant_code,Plant_code as PlantName,Agent_id,Agent_id as AgentName,CONVERT(VARCHAR,RequestingDate,103) AS RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi   FROM LoanRequestEntry    WHERE  ManStatus='1' AND RmStatus=0   order by tid  desc ";
        // string loanamt = "select Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT  Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE PLANT_CODE='"+pcode+"' AND ManStatus=1 AND RmStatus!=1 ) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='"+pcode+"' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='165' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        //   string loanamt = " select Tid AS RefId, Plant_Name,Agent_Id,Agent_Name,CONVERT(VARCHAR, RequestingDate,103) AS Date,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest AS RatePerinst,TotalinterestAmount TotInstAmt,TotalAmount as TotAmt,InstallAmount as InstAmt       from (SELECT Tid,lrf.Plant_code,AM.Agent_Id,Agent_Name,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,UpdateAmt,UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM(  SELECT Tid,Plant_code,Agent_id,RequestingDate,TotLoan,CompletedLoan,PendingLoan,LoanAmount,Emi,Description,RmUpdateLoanAmount as UpdateAmt,RmUpdateEmi as UpdateEmi,Rateperinterest,TotalinterestAmount,TotalAmount,InstallAmount   FROM LoanRequestEntry WHERE Plant_code='" + pcode + "' AND ManStatus=1 and  RmStatus NOT IN(1)) AS LRF LEFT JOIN(SELECT Agent_Id,Agent_Name   FROM Agent_Master WHERE Plant_code='" + pcode + "' GROUP BY Agent_Id,Agent_Name)AS AM ON LRF.AGENT_ID=AM.Agent_Id) as leftside left join (select plant_code,plant_name   from plant_master where plant_code='" + pcode + "' group by plant_code,plant_name) as pm on leftside.Plant_code = pm.Plant_Code";
        string loanamt;
        //if (roleid > 3)
        //{
        //    loanamt = "select TID AS RefId,pp.Plant_Code,Plant_Name,Dept,remarks,CONVERT(VARCHAR,RequesterDate,103) as RequesterDate,convert(varchar(20),Date,113) AS  Date,Login AS LoginUser,Requester     from (select TID,plant_code,Dept,remarks,RequesterDate,Date,Login,Requester    from    PlantRegularRequest where status='0' group by plant_code,remarks,RequesterDate,Date,Login,Requester,TID,Dept) as pp left join (Select Plant_Code,Plant_Name    from Plant_Master  group by Plant_Code,Plant_Name) as pm on pp.plant_code= pm.Plant_Code ";
        //}
        //else
        //{
            loanamt = "select TID AS RefId,pp.Plant_Code,Plant_Name,Dept,remarks,CONVERT(VARCHAR,RequesterDate,103) as RequesterDate,convert(varchar(20),Date,113) AS  Date,Login AS LoginUser,Requester     from (select   TID,plant_code,Dept,remarks,RequesterDate,Date,Login,Requester    from    PlantRegularRequest where status='0'  and plant_code='" + pcode + "' group by plant_code,remarks,RequesterDate,Date,Login,Requester,TID,Dept) as pp left join (Select Plant_Code,Plant_Name    from Plant_Master  where plant_code='" + pcode + "' group by Plant_Code,Plant_Name) as pm on pp.plant_code= pm.Plant_Code ";

        //}
        SqlCommand sc = new SqlCommand(loanamt, con);
        SqlDataAdapter da1 = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        da1.Fill(ds, "TABLEop");
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {

            btn_Save.Visible = true;
        }
        else
        {

            btn_Save.Visible = false;
        }
    }


    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        GETGRID();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Plant Requesting Form"; 
            HeaderCell2.ColumnSpan = 11;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GETGRID();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged2(object sender, EventArgs e)
    {

    }
}




//        foreach (GridViewRow row in GridView1.Rows)
//            {
//                string d11 = Convert.ToDateTime(txt_FromDate.Text).ToString("MM/dd/yyyy");
//                string d12 = Convert.ToDateTime(txt_ToDate.Text).ToString("MM/dd/yyyy");

//                if (row.RowType == DataControlRowType.DataRow)
//                {
//                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
//                    if (chkRow.Checked)
//                    {
//                        string bankid = row.Cells[1].Text;
//                        string bankname = row.Cells[2].Text;
//                        string agentid = row.Cells[3].Text;
//                        string ifsc = row.Cells[4].Text;
//                        string accountno = row.Cells[5].Text;
//                        string prodecername = row.Cells[6].Text;
//                        producercode = row.Cells[7].Text;
//                      //  string amount = row.Cells[8].Text;



//                        string[] amount12 = row.Cells[8].Text.Split('.');
//                        amount = amount12[0].ToString() + ".00";

//                        string frmdate = d11;
//                        string todate = d12;

//                        string assdate = System.DateTime.Now.ToString();
//                        string ADDDATE = Convert.ToDateTime(assdate).ToString("MM/dd/yyy");


//                        string str;

//                        double assignamount = Convert.ToDouble(amount);

//                        checkamount = Convert.ToDouble(txt_Assignamount.Text);
//                        checkamount = checkamount + assignamount;
//                        double assign = Convert.ToDouble(txt_tot.Text);
//                        con.Close();
//                        if (checkamount <= assign)
//                        {
//                            msgvalue = 5; //lessthan Amount
//                        protected void  btn_Save_Click(object sender, EventArgs e)
//{

//}
//}
//                        else
//                        {
//                            msgvalue = 1;

//                            msgbox();

//                        }
//                        // dt.Rows.Add(bankid, bankname, prodecername, producercode, amuount);
//                    }
//                }
//}