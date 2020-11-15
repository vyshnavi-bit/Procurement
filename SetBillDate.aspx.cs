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
using System.IO;
public partial class SetBillDate : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string pname;
    public string cname;
    public string managmobNo;

    DataRow dr;
    DataTable dt = new DataTable();
    BLLuser Bllusers = new BLLuser();
    BOLSetBillDate BolSB = new BOLSetBillDate();
    BLLSetBillDate bllsetbil = new BLLSetBillDate();
    //AgentBO.AgDate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", null);
    DateTime dtm = new DateTime();
    DbHelper dbaccess = new DbHelper();
    int counting;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        uscMsgBox1.MsgBoxAnswered += MessageAnswered;
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                ccode = Session["Company_code"].ToString();               
                cname = Session["cname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
              
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                roleid = Convert.ToInt32(Session["Role"].ToString());
                if (roleid >= 3)
                {
                    LoadPlantcode();           
                    pcode = ddl_Plantcode.SelectedItem.Value;
                    Session["Plant_Code1"] = pcode;
                    pname = ddl_Plantname.SelectedItem.Value;
                    Lbl_PlantName.Visible = true;
                    ddl_Plantname.Visible = true;
                }
                if (roleid < 3)
                {
                    pcode = Session["Plant_Code"].ToString();
                    Session["Plant_Code1"] = pcode;
                    pname = Session["pname"].ToString();
                    //Lbl_PlantName.Visible = false;
                    //ddl_Plantname.Visible = false;
                }
                if (roleid == 9)
                {
                    pcode = "170";
                    Session["Plant_Code1"] = pcode;
                    loadspecialsingleplant();


                }



                LoadGriddata();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

        if (IsPostBack == true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();
              
                //pcode = Session["Plant_Code"].ToString();                
                //pname = Session["pname"].ToString();
                if (program.Guser_role >= 3)
                {
                    pcode = ddl_Plantcode.SelectedItem.Value;
                    Session["Plant_Code1"] = pcode;
                    pname = ddl_Plantname.SelectedItem.Value;
                    Lbl_PlantName.Visible = true;
                    ddl_Plantname.Visible = true;
                }
                else
                {
                    //pcode = Session["Plant_Code"].ToString();
                    pcode = ddl_Plantcode.SelectedItem.Value;
                     Session["Plant_Code1"] = pcode;
                    pname = Session["pname"].ToString();

                }

                                   //Lbl_PlantName.Visible = false;
                    //ddl_Plantname.Visible = false;
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
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
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The RateChart Deleted successfully. You have entered  as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    protected void btn_SetBilldate_Click(object sender, EventArgs e)
    {
        try
        {
            if (validates())
            {
                SETBO();
                bllsetbil.EditUpdateSetBill(BolSB);
                adminupdate();
                Clear();
                LoadGriddata();
                setbillingdpudate();
                WebMsgBox.Show("Record Insert Successfully...");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }




    }
    private void Clear()
    {
        txt_BillDescription.Text = "";
    }
    private bool validates()
    {
        if ((string.IsNullOrEmpty(txt_FromDate.Text)))
        {
            WebMsgBox.Show("Please,Check the Bill_FromDate");
            return false;
        }
        if ((string.IsNullOrEmpty(txt_ToDate.Text)))
        {
            WebMsgBox.Show("Please,Check the Bill_ToDate");
            return false;
        }
        if ((string.IsNullOrEmpty(txt_BillDescription.Text)))
        {
            WebMsgBox.Show("Please,Check the Bill_Descriptions");
            return false;
        }
        return true;
    }

    private void SETBO()
    {
        try
        {
            BolSB.Tid = Convert.ToInt32("0");
            BolSB.Billfromdate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            BolSB.Billtodate = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            BolSB.Updatestatus = Convert.ToInt32("0");
            BolSB.Viewstatus = Convert.ToInt32("0");
            BolSB.Status = Convert.ToInt32("0");
            BolSB.Companycode = ccode;
            BolSB.Plantcode = pcode;
            BolSB.Descriptions = txt_BillDescription.Text;
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
        pname = ddl_Plantname.SelectedItem.Value;
        LoadGriddata();
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
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

    private void LoadGriddata()
    {
        try
        {
            dt = null;
            dt = bllsetbil.LoadBillDate(ccode, pcode);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
               // uscMsgBox1.AddMessage("This Rate chat have Empty Rows Only...", MessageBoxUsc_Message.enmMessageType.Attention);
            }

        }
        catch (Exception ex)
        {

        }

    }
   
    
    
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int index = GridView1.EditIndex;
            GridViewRow row = GridView1.Rows[index];
            LoadGriddata();
            int count;
            count = dt.Rows.Count;
            dr = dt.Rows[index];

            BolSB.Tid = Convert.ToInt32(dr["Tid"]);
            BolSB.Billfromdate = Convert.ToDateTime("1-1-2000");
            BolSB.Billtodate = Convert.ToDateTime("1-1-2000");
            BolSB.PaymentFlag = Convert.ToInt32(((TextBox)row.Cells[3].Controls[0]).Text.ToString());
            BolSB.Updatestatus = Convert.ToInt32(((TextBox)row.Cells[5].Controls[0]).Text.ToString());
            BolSB.Viewstatus = Convert.ToInt32(((TextBox)row.Cells[6].Controls[0]).Text.ToString());
            BolSB.Status = Convert.ToInt32(((TextBox)row.Cells[7].Controls[0]).Text.ToString());
            BolSB.Companycode = ccode;
            BolSB.Plantcode = pcode;
            BolSB.Descriptions = " ";
            bllsetbil.EditUpdateSetBill(BolSB);
            GridView1.EditIndex = -1;
            LoadGriddata();



            string val1 = (((TextBox)row.Cells[1].Controls[0]).Text.ToString());
            string vval1 = (((TextBox)row.Cells[2].Controls[0]).Text.ToString());
            string[] val2 = val1.Split('_');
            string[] vval2 = vval1.Split('_');
            // fd.Text = (val2[0]).ToString();

            string first = (val2[0]).ToString();
            string[] val11 = first.Split('/');
            string fmd = val11[1].ToString();
            string fdd = val11[0].ToString();
            string fyyy = val11[2].ToString();
            txt_FromDate.Text = fmd + "/" + fdd + "/" + fyyy;



            string second = (vval2[0]).ToString();
            string[] val12 = second.Split('/');
            string tmd = val12[1].ToString();
            string tdd = val12[0].ToString();
            string tyyy = val12[2].ToString();
            txt_ToDate.Text = tmd + "/" + tdd + "/" + tyyy;

            counting = 1;


            setbillingdpudate();
            WebMsgBox.Show("Record Updated Successfully...");
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        LoadGriddata();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        LoadGriddata();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        LoadGriddata();
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=gridviewdata.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sWriter = new StringWriter();
            HtmlTextWriter hWriter = new HtmlTextWriter(sWriter);
            GridView1.RenderControl(hWriter);
            Response.Output.Write(sWriter.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }


    public void setbillingdpudate()
    {

        try
        {

           

            if (counting != 1)
            {
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();

                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);



                string d1 = dt1.ToString("MM/dd/yyyy");
                string d2 = dt2.ToString("MM/dd/yyyy");




                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string stt1 = "update dpuAdminAmountAllotToPlant set payflag='0' where plant_code='" + pcode + "'  ";
                SqlCommand cmd1 = new SqlCommand(stt1,conn);
                cmd1.ExecuteNonQuery();
                conn.Close();

                string stt = "update dpuAdminAmountAllotToPlant set payflag='1' where plant_code='" + pcode + "' and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "' ";
                SqlCommand cmd = new SqlCommand(stt,conn);
                cmd.ExecuteNonQuery();

                string stt2 = "update ProducerProcurement set payflag='0' where plant_code='" + pcode + "'  ";
                SqlCommand cmd2 = new SqlCommand(stt2,conn);
                cmd2.ExecuteNonQuery();


                string stt3 = "update ProducerProcurement set payflag='1' where plant_code='" + pcode + "' and Billfrmdate='" + d1 + "' and Billtodate='" + d2 + "' ";
                SqlCommand cmd3 = new SqlCommand(stt3,conn);
                cmd3.ExecuteNonQuery();
                conn.Close();
            }

            else
            {


                 string frdate = (txt_FromDate.Text).ToString();
                 string todate =(txt_ToDate.Text).ToString();
                 string dk = frdate + " "+ "00:00:00.000";
                 string  dk1= todate + " "+"00:00:00.000";
                string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string stt1 = "update dpuAdminAmountAllotToPlant set payflag='0' where plant_code='" + pcode + "'  ";
                SqlCommand cmd1 = new SqlCommand(stt1,conn);
                cmd1.ExecuteNonQuery();

                string stt = "update dpuAdminAmountAllotToPlant set payflag='1' where plant_code='" + pcode + "' and ((Billfrmdate='" + frdate + "' and Billtodate='" + todate+ "') or (Billfrmdate='"+dk+"' and Billtodate='"+dk1+"')) ";
                SqlCommand cmd = new SqlCommand(stt,conn);
                cmd.ExecuteNonQuery();

                string stt2 = "update ProducerProcurement set payflag='0' where plant_code='" + pcode + "'  ";
                SqlCommand cmd2 = new SqlCommand(stt2,conn);
                cmd2.ExecuteNonQuery();

                string stt3 = "update ProducerProcurement set payflag='1' where plant_code='" + pcode + "' and ((prdate between '" + txt_FromDate.Text + "' and '" + txt_ToDate.Text + "') OR ( prdate between '"+dk+"' and  '"+dk1+"' )) ";
                SqlCommand cmd3 = new SqlCommand(stt3,conn);
                cmd3.ExecuteNonQuery();
                conn.Close();

            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }


       



    }



    public void adminupdate()
    {

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("update AdminApproval set Status='0',buttonviewstatus='1' where Plant_code='" + pcode + "'", conn);
            cmd.ExecuteNonQuery();
            //  WebMsgBox.Show("inserted Successfully");

        }
        catch (Exception ex)
        {

        }



    }
}