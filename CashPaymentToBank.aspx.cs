using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing;
using System.Text;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Threading;


public partial class CashPaymentToBank : System.Web.UI.Page
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

    msg getclass = new msg();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable dtr = new DataTable();
    DataSet ds = new DataSet();

    string FDATE;
    string TODATE;
    DateTime d1;
    DateTime d2;
    double CASH;
    public string frmdate;
    string Todate;
    string getvald;
    string getvalm;
    string getvaly;
    string getvaldd;
    string getvalmm;
    string getvalyy;
    double FOOTERROWCASH=0;
    int SNO;
    int SNOO=1;
    DataTable dwq = new DataTable();
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



                }
                billdate();
                Button10.Visible = false;
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;
                Button10.Visible = false;

               
                
            }
        }

        catch
        {



        }
    }
    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
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

    public void billdate()
    {
        try
        {
            
            con.Close();
            con = DB.GetConnection();
        
            string str;
            str = "select  (Bill_frmdate) as Bill_frmdate,Bill_todate   from (select  Bill_frmdate,Bill_todate   from Bill_date   where Bill_frmdate > '1-1-2016'  group by  Bill_frmdate,Bill_todate) as aff order  by  Bill_frmdate desc ";
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


    public void getpreviousdata()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            string getgrid1 = "";
            getgrid1 = "SELECT Agent_id,Agent_Name,NETAMOUNT,Bank_Id,Bank_Name,Ifsc_code,Agent_AccountNo, CONVERT(VARCHAR(19),UpdatingTime) AS   UpdatingTime   FROM ( SELECT Agent_id,Agent_Name,Bank_Id,BM.Bank_Name,Ifsc_code,Agent_AccountNo, CONVERT(VARCHAR(19),UpdatingTime) AS   UpdatingTime   FROM (SELECT AGG.Agent_id,GETDE.Agent_Name,Bank_Id,GETDE.Ifsc_code,Agent_AccountNo,AGG.Plant_code,UpdatingTime   FROM (SELECT  PAY.Agent_id,PAY.Payment_mode,PAY.Plant_code,UpdatingTime   FROM (SELECT Agent_Id,Agent_Name,Plant_code,Payment_mode   FROM Agent_Master WHERE Plant_code='" + pcode + "'  AND Payment_mode='BANK' GROUP BY Agent_Id,Agent_Name,Plant_code,Payment_mode) AS AM LEFT JOIN (sELECT  Agent_id,Payment_mode,Plant_code,UpdatingTime   FROM Paymentdata   WHERE Plant_code='" + pcode + "'   AND  Frm_date='" + FDATE + "'   AND To_date='" + TODATE + "' AND Payment_mode='BANK' and  (UpdatingTime is Not Null) GROUP BY Agent_id,Payment_mode,Plant_code,UpdatingTime) AS    PAY ON AM.Payment_mode=PAY.Payment_mode  GROUP BY PAY.Agent_Id,PAY.Payment_mode,PAY.Plant_code,UpdatingTime) AS AGG LEFT JOIN(SELECT Agent_Id,Agent_Name,Agent_AccountNo,Bank_Id,Ifsc_code,Plant_code,Payment_mode   FROM Agent_Master WHERE Plant_code='" + pcode + "'  GROUP BY Agent_Id,Agent_Name,Plant_code,Payment_mode,Bank_Id,Ifsc_code,Agent_AccountNo) AS GETDE ON AGG.Plant_code=GETDE.Plant_code AND AGG.Agent_id=GETDE.Agent_Id) AS KLGH  LEFT JOIN (SELECT BANK_ID AS BANKBANKID,Bank_Name   FROM Bank_Details     GROUP BY Bank_ID,Bank_Name) AS BM ON   KLGH.Bank_Id=BM.BANKBANKID) AS BANKING   LEFT JOIN (SELECT ISNULL(SUM(NETAMOUNT),0) AS  NETAMOUNT,Agent_id AS PAYAGENT FROM Paymentdata   WHERE Plant_code='" + pcode + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' GROUP BY Agent_id) AS PAYYY  ON BANKING.Agent_id=PAYYY.PAYAGENT";
            con = DB.GetConnection();
            SqlCommand sc2 = new SqlCommand(getgrid1, con);
            SqlDataAdapter da11 = new SqlDataAdapter(sc2);
            DataTable dtb = new DataTable();
            da11.Fill(dtb);
            dtr.Columns.Add("Agent_id");
            dtr.Columns.Add("Agent_Name");
            dtr.Columns.Add("NETAMOUNT");
            dtr.Columns.Add("Bank_Id");
            dtr.Columns.Add("Bank_Name");
            dtr.Columns.Add("Ifsc_code");
            dtr.Columns.Add("Agent_AccountNo");
            dtr.Columns.Add("UpdatingTime");

            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow dfy in dtb.Rows)
                {

                    string getagentid = dfy[0].ToString();
                    string getagentidname = dfy[1].ToString();
                    string GETNEAMT = dfy[2].ToString();
                    string getagentiBankId = dfy[3].ToString();
                    string getBankName = dfy[4].ToString();
                    string getIfsccode = dfy[5].ToString();
                    string AgentAccountNo = dfy[6].ToString();
                    string updatetime = dfy[7].ToString();
                    if ((getagentid != string.Empty) && (getagentidname != string.Empty) && (GETNEAMT != string.Empty) && (getagentiBankId != string.Empty))
                    {
                        dtr.Rows.Add(getagentid, getagentidname, GETNEAMT, getagentiBankId, getBankName, getIfsccode, AgentAccountNo, updatetime);
                    }

                }
            }


        }
        catch
        {


        }
    }

    public void getgridview()
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
            ViewState["FDATE"] = FDATE;
            ViewState["TODATE"] = TODATE;
            ViewState["FDATE1"] = getvald + "/" + getvalm + "/" + getvaly;
            ViewState["FDATE2"] = getvaldd + "/" + getvalmm + "/" + getvalyy;
            string getgrid = "";
            getgrid = "SELECT Agent_id,Agent_Name,NETAMOUNT,Bank_Id,Bank_Name,Ifsc_code,Agent_AccountNo, CONVERT(VARCHAR(19),UpdatingTime) AS   UpdatingTime   FROM ( SELECT Agent_id,Agent_Name,Bank_Id,BM.Bank_Name,Ifsc_code,Agent_AccountNo, CONVERT(VARCHAR(19),UpdatingTime) AS   UpdatingTime   FROM (SELECT AGG.Agent_id,GETDE.Agent_Name,Bank_Id,GETDE.Ifsc_code,Agent_AccountNo,AGG.Plant_code,UpdatingTime   FROM (SELECT  PAY.Agent_id,PAY.Payment_mode,PAY.Plant_code,UpdatingTime   FROM (SELECT Agent_Id,Agent_Name,Plant_code,Payment_mode   FROM Agent_Master WHERE Plant_code='" + pcode + "'  AND Payment_mode='BANK' GROUP BY Agent_Id,Agent_Name,Plant_code,Payment_mode) AS AM LEFT JOIN (sELECT  Agent_id,Payment_mode,Plant_code,UpdatingTime   FROM Paymentdata   WHERE Plant_code='" + pcode + "'   AND  Frm_date='" + FDATE + "'   AND To_date='" + TODATE + "' AND Payment_mode='CASH'  GROUP BY Agent_id,Payment_mode,Plant_code,UpdatingTime) AS    PAY ON AM.Payment_mode!=PAY.Payment_mode  GROUP BY PAY.Agent_Id,PAY.Payment_mode,PAY.Plant_code,UpdatingTime) AS AGG LEFT JOIN(SELECT Agent_Id,Agent_Name,Agent_AccountNo,Bank_Id,Ifsc_code,Plant_code,Payment_mode   FROM Agent_Master WHERE Plant_code='" + pcode + "'  GROUP BY Agent_Id,Agent_Name,Plant_code,Payment_mode,Bank_Id,Ifsc_code,Agent_AccountNo) AS GETDE ON AGG.Plant_code=GETDE.Plant_code AND AGG.Agent_id=GETDE.Agent_Id) AS KLGH  LEFT JOIN (SELECT BANK_ID AS BANKBANKID,Bank_Name   FROM Bank_Details     GROUP BY Bank_ID,Bank_Name) AS BM ON   KLGH.Bank_Id=BM.BANKBANKID) AS BANKING   LEFT JOIN (SELECT ISNULL(SUM(NETAMOUNT),0) AS  NETAMOUNT,Agent_id AS PAYAGENT FROM Paymentdata   WHERE Plant_code='" + pcode + "' AND Frm_date='" + FDATE + "' AND To_date='" + TODATE + "' GROUP BY Agent_id) AS PAYYY  ON BANKING.Agent_id=PAYYY.PAYAGENT";
            con = DB.GetConnection();

            SqlCommand sc = new SqlCommand(getgrid, con);
            SqlDataAdapter da1 = new SqlDataAdapter(sc);

            da1.Fill(dwq);
           
            foreach (DataRow drp in dwq.Rows)
            {
                string getagentid = drp[0].ToString();
                string getagentidname = drp[1].ToString();
                string GETNEAMT = drp[2].ToString();
                string getagentiBankId = drp[3].ToString();
                string getBankName = drp[4].ToString();
                string getIfsccode = drp[5].ToString();
                string AgentAccountNo = drp[6].ToString();
                string updatetimee = drp[7].ToString();
                if ((getagentid != string.Empty) && (getagentidname != string.Empty) && (GETNEAMT != string.Empty) && (getagentiBankId != string.Empty))
                {
                    dtr.Rows.Add(getagentid, getagentidname, GETNEAMT, getagentiBankId, getBankName, getIfsccode, AgentAccountNo, updatetimee);
                }
            }

            GridView1.DataSource = dtr;
            GridView1.DataBind();
            if (dtr.Rows.Count > 0)
            {
                GridView1.DataSource = dtr;
                GridView1.DataBind();

                GridView1.Visible = true;

                GridView1.FooterRow.Cells[2].Text = "Cash Total";
                GridView1.FooterRow.Cells[2].Font.Bold = true;
                GridView1.FooterRow.Cells[3].Font.Bold = true;
                GridView1.FooterRow.Cells[3].Text = (FOOTERROWCASH / 2).ToString("F2");
                GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.BackColor = System.Drawing.Color.Cyan;
                GridView1.FooterRow.ForeColor = System.Drawing.Color.DarkBlue;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = false;

            }
        }
        catch
        {
            GridView1.DataSource = dtr;
            GridView1.DataBind();

        }
    }

    //public void getupdate()
    //{
    //    try
    //    {
    //        foreach (GridViewRow row in GridView1.Rows)
    //        {
    //            if (row.RowType == DataControlRowType.DataRow)
    //            {

    //                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
    //                if (chkRow.Checked)
    //                {
    //                    string getagent = row.Cells[1].Text;
    //                    string PLANT = row.Cells[2].Text;


    //                    ViewState["FDATE"] = FDATE;
    //                    ViewState["TODATE"] = TODATE;


    //                    string getloanfinaupdate1;

    //                    con = DB.GetConnection();

    //                    getloanfinaupdate1 = "update Paymentdata set ";
    //                    SqlCommand cmd11 = new SqlCommand(getloanfinaupdate1, con);
    //                    cmd11.ExecuteNonQuery();


    //                    Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.billupdate) + "')</script>");

    //                }



    //            }
    //        }
    //    }
    //    catch
    //    {

    //    }
    //}

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_BillDate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        getpreviousdata();
        getgridview();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //On Mouse over highlight gridview
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ffff00'");

                //On Mouse out restore girdview row color based on alternate row color//Please change this color with your gridview alternate color.if (e.Row.RowIndex % 2 == 0)

                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#F7F7DE'");
            }
            else
            {
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            }

            if (GridView1.Rows.Count > 0)
            {

                GridView1.Visible = true;
            }
            else
            {
                GridView1.Visible = false;


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
            Button10.Visible = true;
            try
            {

                string getupdate = e.Row.Cells[8].Text;
                string GETBANKIG = e.Row.Cells[4].Text;
                string BankName = e.Row.Cells[5].Text;
                string Ifsccode = e.Row.Cells[6].Text;
               
                try
                {
                     CASH = Convert.ToDouble(e.Row.Cells[3].Text);
                     FOOTERROWCASH = FOOTERROWCASH + CASH;
                   
                    
                }

                catch
                {


                }
                if ((getupdate == "&nbsp;"))
                {

                    e.Row.Cells[8].Text = "Pending";
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;

                }
            
                if (((getupdate != string.Empty) && (getupdate != "&nbsp;")) || (GETBANKIG == "0") || (BankName == "0") || (Ifsccode == "0"))
                {
                    e.Row.Enabled = false;
                    e.Row.Cells[9].Text = "";
                    
                }
                else
                {

                    e.Row.Enabled = true;

                }
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                
                  string aggent =   e.Row.Cells[1].Text;

                  //if (aggent == "&nbsp;")
                  //{
                  //    e.Row.Visible = false;
                  //    Button10.Visible = false;

                  //    SNO = SNO - 1;

                  //}
                  //else
                  //{
                  //    SNO = SNO + 1;
                  //    e.Row.Cells[0].Text = (SNO).ToString();

                  //}


                


            }
            catch
            {

            }
        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        getupdateagentdetails();
        getpreviousdata();
        getgridview();

    }

    public void getupdateagentdetails()
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string getagentid = row.Cells[1].Text;
                        string getagentidname = row.Cells[2].Text;
                        string GETNEAMT = row.Cells[3].Text;
                        string getagentiBankId = row.Cells[4].Text;
                        string getBankName = row.Cells[5].Text;
                        string getIfsccode = row.Cells[6].Text;
                        string AgentAccountNo = row.Cells[7].Text;
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
                        string doupdate;
                        con = DB.GetConnection();
                        if ((getagentid != string.Empty) && (getagentidname != string.Empty) && (getagentiBankId != "0") && (getBankName != "&nbsp;") && (getIfsccode != "0") && (AgentAccountNo != "0"))
                        {
                            doupdate = "Update Paymentdata set Payment_mode='BANK', cashtobank='1',CashToBankUpdateUser='" + Session["Name"] + "',UpdatingTime='"+System.DateTime.Now+"', Agent_Name='" + getagentidname + "', Bank_Id='" + getagentiBankId + "',Bank_Name='" + getBankName + "',Ifsc_code='" + getIfsccode + "',Agent_AccountNo='" + AgentAccountNo + "'   where plant_code='" + pcode + "'  and agent_id='" + getagentid + "' and Frm_date='" + FDATE + "'   and To_date='" + TODATE + "' and Payment_mode='cash'";
                        SqlCommand cmd = new SqlCommand(doupdate, con);
                        cmd.ExecuteNonQuery();
                        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.billupdate) + "')</script>");
                        }
                        else
                        {
                        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(getclass.Check) + "')</script>");
                        }
                    }



                }
            }
        }
        catch
        {

        }
    }
    
}