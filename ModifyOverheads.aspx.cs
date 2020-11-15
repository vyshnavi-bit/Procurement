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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

public partial class ModifyOverheads : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string frmdate;
    public string todate;
    string getrouteusingagent;
    string routeid;
    string dt1;
    string dt2;
    string ddate;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    int totalrow;
    string connSttr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;

    string VOUCHERTYPE;
    double Debitamount;
    double Creditamount;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {

                ccode = Session["Company_code"].ToString();
                //    pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();


                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToShortDateString();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");


                LoadPlantcode();
                gridview();
                lbl_ref.Visible = false;
                txt_ref.Visible = false;
                voucherty.Visible = false;
                lbl_ref.Visible = false;
                txt_amount.Visible = false;
                vouchertype.Visible = false;
                editsave.Visible = false;
                AMT.Visible = false;
                lblnarr.Visible = false;
                txt_narration.Visible = false;
                PanelHIDE.Visible = false;
                Panelshow.Visible = true;

            }

            else
            {

                pcode = ddl_Plantcode.SelectedItem.Value;
                gridview();


            }

        }
        else
        {


            pcode = ddl_Plantcode.SelectedItem.Value;
            gridview();
        }


    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode);
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
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        gridview();

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {









            gridview();

        }
    }

    public void gridview()
    {

        try
        {
            DateTime dt1 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");


            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sqlstr = " select  a.Reference_No,convert(varchar,a.EntryDate,103) as EntryDate,b.LedgerName,a.Voucher_Type,a.Narration,a.Reference_Name,a.CreditAmount,a.DebitAmount from(select  * from   Account_Transaction   where   Plant_Code='" + pcode + "' and  Entrydate='" + d1 + "' ) as  a left join (select  * from   ChillingLedger  )  as b  on a.GroupHead_Id=b.GroupHead_ID AND A.Head_Id=b.Head_Id AND A.SubHead_Id=b.Ledger_Id ";

                //   string sqlstr = "SELECT Supervisor_Code,Plant_Code,SupervisorName,convert(varchar,Dob,103) as Dob,Address,Mobile,Bank_name,IfscCode,AccountNumber,Pannumber  FROM Supervisor_Details WHERE plant_code='" + pcode + "'  order by  Supervisor_Code desc";
                // string sqlstr = "SELECT agent_id,convert(varchar,prdate,103) as prdate,sessions,fat FROM procurementimport  where plant_code='" + pcode + "' and prdate between '" + d1 + "' and '" + d2 + "' order by Agent_id,prdate,sessions ";
                SqlCommand COM = new SqlCommand(sqlstr, conn);
                //   SqlDataReader DR = COM.ExecuteReader();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                sqlDa.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    GridView1.DataSource = dt;
                    GridView1.DataBind();


                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }



            }
        }
        catch
        {



        }
    }



    //    public void GRIDVIEWCODE()
    //    {

    //        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        using (SqlConnection conn = new SqlConnection(connStr))
    //        {
    //            conn.Open();

    //            string sqlstr = "select  * from(select  * from   Account_Transaction   where   Plant_Code=158 and  Entrydate='2-1-2015' ) as  a left join (select  * from   ChillingLedger  )  as b  on a.GroupHead_Id=b.GroupHead_ID AND A.Head_Id=b.Head_Id AND A.SubHead_Id=b.Ledger_Id";
    //;
    //            SqlCommand COM = new SqlCommand(sqlstr, conn);
    //            //   SqlDataReader DR = COM.ExecuteReader();
    //            DataTable dt = new DataTable();
    //            SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
    //            sqlDa.Fill(dt);
    //            if (dt.Rows.Count > 0)
    //            {
    //                GridView1.DataSource = dt;
    //                GridView1.DataBind();
    //                int totalrow = Convert.ToInt32(GridView1.Rows.Count);

    //            }
    //            else
    //            {

    //                GridView1.DataSource = null;
    //                GridView1.DataBind();
    //                int totalrow = Convert.ToInt32(GridView1.Rows.Count);

    //            }
    //            //GridView1.DataSource = DR;
    //            //GridView1.DataBind();

    //        }

    //    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        lbl_ref.Visible = true;
        txt_ref.Visible = true;
        voucherty.Visible = true;
        lbl_ref.Visible = true;
        txt_amount.Visible = true;
        vouchertype.Visible = true;
        editsave.Visible = true;
        AMT.Visible = true;
        lblnarr.Visible = true;

        txt_ref.Text = GridView1.SelectedRow.Cells[1].Text;
        string vtype = GridView1.SelectedRow.Cells[4].Text;
        txt_narration.Text = GridView1.SelectedRow.Cells[5].Text;
        lblnarr.Visible = true;
        txt_narration.Visible = true;
        PanelHIDE.Visible = true;
        Panelshow.Visible = false;
        //if (vouchertype.Text == "cp")
        //{
            if (vtype == "Debit")
            {

                vouchertype.Text = "CP";
                txt_amount.Text = GridView1.SelectedRow.Cells[8].Text;

                Debitamount = Convert.ToDouble(txt_amount.Text);

                 //  Creditamount = 0;

            }
        //}
        //if (vouchertype.Text == "CR")
        //{
            if (vtype == "Credit")
            {
                // vouchertype.Text = "CR";
                txt_amount.Text = GridView1.SelectedRow.Cells[7].Text;
                // lbl_ref.Text = "0";
                vouchertype.Text = "CR";
                Creditamount = Convert.ToDouble(txt_amount.Text);
            //     Debitamount = 0;


            }
        //}


    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Panelshow.Visible = true;
        PanelHIDE.Visible = false;
    }
    protected void editsave_Click(object sender, EventArgs e)
    {


        if (vouchertype.Text == "CR")
        {

            VOUCHERTYPE = "Credit";
              Debitamount = 0;
            Creditamount = Convert.ToDouble(txt_amount.Text);

            //  Creditamount = 0;

        }

        if (vouchertype.Text == "CP")
        {

            VOUCHERTYPE = "Debit";
            Debitamount = Convert.ToDouble(txt_amount.Text);
             Creditamount = 0;

        }
        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand cmd = new SqlCommand("update Account_Transaction  set Voucher_Type='" + VOUCHERTYPE + "',Debitamount='" + Debitamount + "',Creditamount='" + Creditamount + "',Narration='" + txt_narration.Text + "' where plant_code='" + pcode + "' and Reference_No='" + txt_ref.Text + "'", conn);
        cmd.ExecuteNonQuery();
        WebMsgBox.Show("Updated Successfully");
        gridview();

        txt_amount.Text = "";
        txt_narration.Text = "";


    }
}