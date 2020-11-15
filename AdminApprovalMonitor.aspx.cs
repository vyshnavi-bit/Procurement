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
using System.Collections.Generic;

public partial class AdminApprovalMonitor : System.Web.UI.Page
{
    string conn = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection();
    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    public string userid;
    DataTable dt = new DataTable();
    DbHelper DBaccess = new DbHelper();
    string getdata1;
    string plant;
    string gData;
    string gLoanStatus;
    string gDeductionStatus;
    string gDespatchStatus;
    string gClosingStatus;
    string gTransportStatus;
    string gStatus;
    string gviewStatus;
    string GDPU;
    SqlDataReader dr;
    public static int roleid;
    public static string username;
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        if (IsPostBack != true)
        {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString(); 
            pname = Session["pname"].ToString();
            username = Session["Name"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
           // managmobNo = Session["managmobNo"].ToString();
          //  userid = Session["User_ID"].ToString();
            getdata();

            if (roleid == 9)
            {
                Session["Plant_Code"] = "170".ToString();
                pcode = "170";
                getdata11();

            }



        }

        else
        {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
            username = Session["Name"].ToString();
          //  managmobNo = Session["managmobNo"].ToString();
          //  userid = Session["User_ID"].ToString();
            getdata();


            if (roleid == 9)
            {
                Session["Plant_Code"] = "170".ToString();
                pcode = "170";
                getdata11();

            }

        }
    }

    public void getdata()
    {
        SqlConnection con = new SqlConnection(conn);
        //  string str = "Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,Date from AdminApproval";
        dt.Rows.Clear();
        string str = "  select distinct(plant_code)  as plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,buttonviewstatus,Dpustatus,(fdate +'_' + todate) as Date   from (Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,buttonviewstatus,Dpustatus from AdminApproval  where  plant_code not in(160)  ) as aa left join (select plant_code  as pcode,convert(varchar,bill_frmdate,103) as Fdate,convert(varchar,bill_Todate,103) as Todate    from bill_date where status=0)  as bd on aa.plant_code=bd.pcode";
        con.Open();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();


    }
    public void getdata11()
    {
        SqlConnection con = new SqlConnection(conn);
        //  string str = "Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,Date from AdminApproval";
        dt.Rows.Clear();
        string str = "  select distinct(plant_code)  as plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,buttonviewstatus,Dpustatus,(fdate +'_' + todate) as Date   from (Select plant_code,plant_name,Data,Loanstatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,buttonviewstatus,Dpustatus from AdminApproval  where  plant_code not in(160)  and plant_code='170') as aa left join (select plant_code  as pcode,convert(varchar,bill_frmdate,103) as Fdate,convert(varchar,bill_Todate,103) as Todate    from bill_date where status=0   and   plant_code='170' )  as bd on aa.plant_code=bd.pcode";
        con.Open();
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
       // GridView1.EditIndex = e.NewEditIndex;
       // getdata();


        GridView1.EditIndex = e.NewEditIndex;
        getdata();
        
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        getdata();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            DropDownList dData, dLoan, ddeduc, ddespatch, dclose, dtransport, dstatus, dviewstatus, DPU1;

            dData = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("Data1"));
            dLoan = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("Loanstatus1"));
            ddeduc = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("DedStatus1"));
            ddespatch = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("DespStatus1"));
            dclose = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("ClosStatus1"));
            dtransport = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("TranStatus1"));
            dstatus = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("Status1"));
            dviewstatus = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("viewstatus1"));
            DPU1 = (DropDownList)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("DPU1"));
           


            string val1 = dData.Text;
            string val2 = dLoan.Text;
            string val3 = ddeduc.Text;
            string val4 = ddespatch.Text;
            string val5 = dclose.Text;
            string val6 = dtransport.Text;
            string val7 = dstatus.Text;
            string val8 = dviewstatus.Text;
            string val9 = DPU1.Text;

        

        //    DropDownList ddlStatus = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("Data");

            //DropDownList cs = (DropDownList)row.FindControl("Data");
            ////     DropDownList ddl = 	(DropDownList)GridView1.Rows[e.RowIndex].Cells[0].Text;
            //TextBox Data = (TextBox)row.Cells[3].Controls[0];
            //TextBox Loan = (TextBox)row.Cells[4].Controls[0];
            //TextBox deduc = (TextBox)row.Cells[5].Controls[0];
            //TextBox despatch = (TextBox)row.Cells[6].Controls[0];
            //TextBox close = (TextBox)row.Cells[7].Controls[0];
            //TextBox transport = (TextBox)row.Cells[8].Controls[0];
            //TextBox status = (TextBox)row.Cells[9].Controls[0];
            // TextBox remark1 = (TextBox)row.Cells[8].Controls[0];
         
            conn.Open();


            SqlCommand cmd = new SqlCommand("update AdminApproval  set Data='" + val1 + "',LoanStatus='" + val2 + "',DeductionStatus='" + val3 + "',DespatchStatus='" + val4 + "',ClosingStatus='" + val5 + "',TransportStatus='" + val6 + "',Status='" + val7 + "',buttonviewstatus='" + dviewstatus.Text + "',Dpustatus='" + val9 + "' where plant_code='" + userid + "' ", conn);
            cmd.ExecuteNonQuery();
            WebMsgBox.Show("Updated Successfully");
          // 

            GridView1.EditIndex = -1;
            
            getdata();
            
        }
        catch
        {

            WebMsgBox.Show("Please Check updated Data");
            
        }
       // getdata();
       
    }


  
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

          
            //make sure the row is on edit mode
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                                
                plant = e.Row.Cells[1].Text;
                //access the dropdownlist
              //  DropDownList Data1 = (DropDownList)ctrl;
                DropDownList Data1 = (DropDownList)e.Row.FindControl("Data1");
               //  Data1.Items.Clear();
                Data1.DataSource = GetDropDownData(); // set the datasource
                Data1.DataTextField = "Data";
                Data1.DataBind();
                Data1.Items.FindByValue(gData).Selected = true;


                DropDownList Loanstatus1 = (DropDownList)e.Row.FindControl("Loanstatus1");
                //  Data1.Items.Clear();
                Loanstatus1.DataSource = GetDropDownData(); // set the datasource
                Loanstatus1.DataTextField = "LoanStatus";
                Loanstatus1.DataBind();
                Loanstatus1.Items.FindByValue(gLoanStatus).Selected = true;


                DropDownList DedStatus1 = (DropDownList)e.Row.FindControl("DedStatus1");
                //  Data1.Items.Clear();
                DedStatus1.DataSource = GetDropDownData(); // set the datasource
                DedStatus1.DataTextField = "DeductionStatus";
                DedStatus1.DataBind();
                DedStatus1.Items.FindByValue(gDeductionStatus).Selected = true;


                DropDownList DespStatus1 = (DropDownList)e.Row.FindControl("DespStatus1");
                //  Data1.Items.Clear();
                DespStatus1.DataSource = GetDropDownData(); // set the datasource
                DespStatus1.DataTextField = "DespatchStatus";
                DespStatus1.DataBind();
                DespStatus1.Items.FindByValue(gDespatchStatus).Selected = true;



                DropDownList ClosStatus1 = (DropDownList)e.Row.FindControl("ClosStatus1");
                //  Data1.Items.Clear();
                ClosStatus1.DataSource = GetDropDownData(); // set the datasource
                ClosStatus1.DataTextField = "ClosingStatus";
                ClosStatus1.DataBind();
                ClosStatus1.Items.FindByValue(gClosingStatus).Selected = true;


                DropDownList TranStatus1 = (DropDownList)e.Row.FindControl("TranStatus1");
                //  Data1.Items.Clear();
                TranStatus1.DataSource = GetDropDownData(); // set the datasource
                TranStatus1.DataTextField = "TransportStatus";
                TranStatus1.DataBind();
                TranStatus1.Items.FindByValue(gTransportStatus).Selected = true;



                DropDownList Status1 = (DropDownList)e.Row.FindControl("Status1");
                //  Data1.Items.Clear();
                Status1.DataSource = GetDropDownData(); // set the datasource
                Status1.DataTextField = "Status";
                Status1.DataBind();
                Status1.Items.FindByValue(gStatus).Selected = true;


                DropDownList viewstatus1 = (DropDownList)e.Row.FindControl("viewstatus1");
                //  Data1.Items.Clear();
                viewstatus1.DataSource = GetDropDownData(); // set the datasource
                viewstatus1.DataTextField = "buttonviewstatus";
                viewstatus1.DataBind();
                viewstatus1.Items.FindByValue(gviewStatus).Selected = true;

                DropDownList DPU1 = (DropDownList)e.Row.FindControl("DPU1");
                //  Data1.Items.Clear();
                DPU1.DataSource = GetDropDownData(); // set the datasource
                DPU1.DataTextField = "buttonviewstatus";
                DPU1.DataBind();
                DPU1.Items.FindByValue(GDPU).Selected = true;


              
                

               

            }
        }




        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Control ctrl = e.Row.FindControl("Data1");
        //    if (ctrl != null)
        //    {
        //        DropDownList ddl = (DropDownList)ctrl;
        //        VehicleList vehicle = new VehicleList();
        //        List<Vehicle> lv = InVehicle.GetList(vehicle);
        //        ddl.DataSource = lv;
        //        ddl.DataTextField = "VehicleName";
        //        ddl.DataValueField = "VehicleID";
        //        if (ddl != null)
        //        {
        //            ddl.SelectedValue = ddl.DataValueField;
        //            ddl.DataBind();
        //        }
        //    }
        //}
    }

    protected DataTable GetDropDownData()
    {
            DataTable dt = new DataTable();
           string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);

            string sql = "SELECT Data,LoanStatus,DeductionStatus,DespatchStatus,ClosingStatus,TransportStatus,Status,buttonviewstatus,Dpustatus FROM AdminApproval where plant_code='" + plant + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {

                        gData = dr[0].ToString();
                        gLoanStatus = dr[1].ToString();
                        gDeductionStatus = dr[2].ToString();
                        gDespatchStatus = dr[3].ToString();
                        gClosingStatus = dr[4].ToString();
                        gTransportStatus = dr[5].ToString();
                        gStatus = dr[6].ToString();
                        gviewStatus = dr[7].ToString();
                        GDPU = dr[8].ToString();

                    }
                }
                 
            
            return dt;
}

    
}