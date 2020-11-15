using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
public partial class CompareDpuProcureData : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string strsql2 = string.Empty;
    string connStr2 = ConfigurationManager.ConnectionStrings["AMPSConnectionStringDpu"].ConnectionString;
    string strsql1 = string.Empty;
    string connStr1 = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DataSet ds;
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    string message;
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    SqlDataReader dr;
    string d1;
    string d2;
    string d3;
    string producers;
    int count1;
    int count2;
    string agentcode;

    string GETDATE;
    string session;
    string splitdate;
    string addsession = "12:00:00";
    string finaldate;
    string ffseconddate;
    string ftseconddate;
    string sess = "12:00:00";
    string addfsecond = "01:01:00";
    string addtsecond = "23:59:59";

    string QUERY;
    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            ccode = Session["Company_code"].ToString();
            pcode = Session["Plant_Code"].ToString();
            cname = Session["cname"].ToString();
            pname = Session["pname"].ToString();
            roleid = Convert.ToInt32(Session["Role"].ToString());
        //    managmobNo = Session["managmobNo"].ToString();
            dtm = System.DateTime.Now;
            dtm = System.DateTime.Now;
            txt_FromDate.Text = dtm.ToShortDateString();
            txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");

            txt_ToDate.Text = dtm.ToShortDateString();
            txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
            lbldpunos.Visible = false;
            lbldpunos1.Visible = false;
            lblpcode.Visible = false;
            lbldputrandnos.Visible = false;
            lblagent.Visible = false;
            ddl_AgentId.Visible = false;
            lblmsg.Visible = false;
            if (roleid < 3)
            {
                LoadSinglePlantName();
            }
            else
            {

                LoadPlantName();

            }

            if (CheckBox1.Checked == true)
            {

                RadioButtonList1.Visible = true;
                Save.Visible = true;
            }
            else
            {

                RadioButtonList1.Visible = false;
                Save.Visible = false;
            }

        }

        else
        {

            pcode = ddl_PlantName.SelectedItem.Value;
            lblmsg.Visible = false;

            if (ddl_AgentId.Text != string.Empty)
            {

                agentcode = ddl_AgentId.Text;
            }

            if (Chk_single.Checked == true)
            {

                lblagent.Visible = true;
                ddl_AgentId.Visible = true;
            }
            else
            {

                lblagent.Visible = false;
                ddl_AgentId.Visible = false;

            }
        }
    }


    private void LoadPlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
        
    protected void btn_ok_Click(object sender, EventArgs e)
    {


        


        getdpuData();
        getdpuData1();
        getdpuTransferdData();
        

        if (count1 == count2)
        {

            lbldputrandnos.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lbldputrandnos.ForeColor = System.Drawing.Color.Red;
        }




    }

    public void getdpuData()
    {

        try
        {


            SqlConnection con = new SqlConnection(connStr2);
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d11 = dt1.ToString("MM/dd/yyyy hh:mm:ss");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string d22 = dt2.ToString("MM/dd/yyyy hh:mm:ss");

            string fseconds = d1 + " " + addfsecond;
            string fteconds = d2 + " " + addtsecond;

            //DateTime datee1 = DateTime.Parse(txt_FromDate.Text);

            //string datee = datee1.ToString("MM/dd/yyyy");
            //finaldate = datee1.ToString("MM/dd/yyyy") + " " + addsession;
            //ffseconddate = datee1.ToString("MM/dd/yyyy") + " " + addfsecond;
            //ftseconddate = datee1.ToString("MM/dd/yyyy") + " " + addtsecond;



            // string str;
            //if (Chk_single.Checked == false)
            //{
            //    QUERY = "select agent_code,shift,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "' and ((prdate between '" + d11 + "' and '" + d22 + "') or (prdate between '" + d1 + "' and '" + d2 + "')  or (prdate between '" + fseconds + "' and '" + fteconds + "'))   order by prdate,agent_code,producer_code,shift";


            //}
            if (Rdolistperiod.SelectedValue == "1")
            {
                //   QUERY = "select agent_code,shift,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "'       and ((prdate between '" + d11 + "' and '" + d22 + "') or (prdate between '" + d1 + "' and '" + d2 + "')  or (prdate between '" + fseconds + "' and '" + fteconds + "')) order by prdate,agent_code,producer_code,shift";
                QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "'  and status is null    and  (prdate='" + d1 + "'   OR  prdate='" + d11 + "') and  (fat not in('0.00') and  snf not in('0.00') and milk_kg   not in('0.00'))  order by rand(producer_code),agent_code,shift";
            }
            if (Rdolistperiod.SelectedValue == "2")
            {

                QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "' and  status is null   and shift='AM' and  (prdate='" + d1 + "'   OR  prdate='" + d11 + "')  and (fat not in('0.00') and  snf not in('0.00') and milk_kg   not in('0.00'))  order by rand(producer_code),agent_code,shift";

            }

            if (Rdolistperiod.SelectedValue == "3")
            {

                QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "' and    status is null  and shift='PM' and  (prdate='" + d1 + "'   OR  prdate='" + d11 + "')  and (fat not in('0.00') and  snf not in('0.00') and milk_kg   not in('0.00')) order by rand(producer_code),agent_code,shift";

            }



            con.Open();
            SqlCommand cmd = new SqlCommand(QUERY, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                count1 = dt.Rows.Count;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lbldpunos.Visible = true;
                lblpcode.Visible = true;
                lbldputrandnos.Visible = true;
                lbldpunos.Text = "No Of Rows=" + dt.Rows.Count.ToString();
                if (Rdolistperiod.SelectedValue == "1")
                {
                    lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text;
                }

                if (Rdolistperiod.SelectedValue == "2")
                {
                    lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text + "Session:AM";
                }

                if (Rdolistperiod.SelectedValue == "3")
                {
                    lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text + "Session:AM";
                }

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lbldpunos.Text = "No Of Rows=" + dt.Rows.Count.ToString();
                //  lblmsg.Text = "No Rows In GridView";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Visible = false;
                //  GridView2.Rows[0].Cells[0].Text = "No Data Found";
                //   GridView1.DataBind();
            }

        }

        catch (Exception ee)
        {

            //lblmsg.Text = ee.ToString();
            //lblmsg.ForeColor = System.Drawing.Color.Red;
            //lblmsg.Visible = true;

        }


    }



    public void getdpuData1()
    {


        try
        {
            SqlConnection con = new SqlConnection(connStr2);
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dt1.ToString("MM/dd/yyyy");
            string d11 = dt1.ToString("MM/dd/yyyy hh:mm:ss");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string d22 = dt2.ToString("MM/dd/yyyy hh:mm:ss");

            string fseconds = d1 + " " + addfsecond;
            string fteconds = d2 + " " + addtsecond;

            //DateTime datee1 = DateTime.Parse(txt_FromDate.Text);

            //string datee = datee1.ToString("MM/dd/yyyy");
            //finaldate = datee1.ToString("MM/dd/yyyy") + " " + addsession;
            //ffseconddate = datee1.ToString("MM/dd/yyyy") + " " + addfsecond;
            //ftseconddate = datee1.ToString("MM/dd/yyyy") + " " + addtsecond;



            // string str;
            //if (Chk_single.Checked == false)
            //{
            //    QUERY = "select agent_code,shift,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "' and ((prdate between '" + d11 + "' and '" + d22 + "') or (prdate between '" + d1 + "' and '" + d2 + "')  or (prdate between '" + fseconds + "' and '" + fteconds + "'))  and status=1  order by prdate,agent_code,producer_code,shift";


            //}
            if (Rdolistperiod.SelectedValue == "1")
            {
                QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "'    and status=1   and  (prdate='" + d1 + "'   OR  prdate='" + d11 + "')  and fat >  '0.00' and  snf >    '0.00' and milk_kg >  '0.00' order by rand(producer_code),agent_code,shift";
            }
            if (Rdolistperiod.SelectedValue == "2")
            {

                QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "'   and status=1    and shift='AM' and  (prdate='" + d1 + "'   OR  prdate='" + d11 + "')   and fat >  '0.00' and  snf >    '0.00' and milk_kg >  '0.00' order by rand(producer_code),agent_code,shift";

            }

            if (Rdolistperiod.SelectedValue == "3")
            {

                QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from THIRUMALABILLSNEW   where plant_code='" + pcode + "'     and status=1   and shift='PM' and    (prdate='" + d1 + "'   OR  prdate='" + d11 + "')  and fat >  '0.00' and  snf >    '0.00' and milk_kg >  '0.00'  order by rand(producer_code),agent_code,shift";

            }



            con.Open();
            SqlCommand cmd = new SqlCommand(QUERY, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                count1 = dt.Rows.Count;
                GridView3.DataSource = dt;
                GridView3.DataBind();
               

                lbldpunos1.Visible = true;
                lblpcode.Visible = true;
                lbldputrandnos.Visible = true;
                lbldpunos1.Text = "No Of Rows=" + dt.Rows.Count.ToString();
                if (Rdolistperiod.SelectedValue == "1")
                {
                    lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text;
                }

                if (Rdolistperiod.SelectedValue == "2")
                {
                    lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text + "Session:AM";
                }

                if (Rdolistperiod.SelectedValue == "3")
                {
                    lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text + "Session:AM";
                }


            }
            else
            {
                GridView3.DataSource = null;
                GridView3.DataBind();
                lbldpunos1.Visible = false;
                lbldpunos1.Text = "No Of Rows=" + dt.Rows.Count.ToString();
               // lblmsg.Text = "No Rows In GridView";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Visible = true;
                //  GridView2.Rows[0].Cells[0].Text = "No Data Found";
                //   GridView1.DataBind();
            }
        }
        catch(Exception ee)
        {

            //lblmsg.Text = ee.ToString();
            //lblmsg.ForeColor = System.Drawing.Color.Red;
            //lblmsg.Visible = true;

        }

    }



    public void getdpuTransferdData()
    {

        SqlConnection con = new SqlConnection(connStr1);
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        string d1 = dt1.ToString("MM/dd/yyyy");
        string d11 = dt1.ToString("MM/dd/yyyy hh:mm:ss");
        string d2 = dt2.ToString("MM/dd/yyyy");
        string d22 = dt2.ToString("MM/dd/yyyy hh:mm:ss");
        string str;

        string fseconds = d1 + " " + addfsecond;
        string fteconds = d2 + " " + addtsecond;

       
        if (Rdolistperiod.SelectedValue == "1")
        {
            QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from VMCCDPU   where plant_code='" + pcode + "' and (prdate='" + d1 + "'   OR  prdate='" + d11 + "')  and fat >  '0.00' and  snf >    '0.00' and milk_kg >  '0.00' order by rand(producer_code),agent_code,shift";
        }

        if (Rdolistperiod.SelectedValue == "2")
        {

            QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from VMCCDPU   where plant_code='" + pcode + "'    and shift='AM' and  (prdate='" + d1 + "'   OR  prdate='" + d11 + "')  and fat >  '0.00' and  snf >    '0.00' and milk_kg >  '0.00'  order by rand(producer_code),agent_code,shift";

        }

        if (Rdolistperiod.SelectedValue == "3")
        {

            QUERY = "select agent_code,producer_code,fat,snf,milk_kg  from VMCCDPU   where plant_code='" + pcode + "'    and shift='PM' and    (prdate='" + d1 + "'   OR  prdate='" + d11 + "')  and fat >  '0.00' and  snf >    '0.00' and milk_kg >  '0.00'  order by rand(producer_code),agent_code,shift";

        }


     
        con.Open();
        SqlCommand cmd = new SqlCommand(QUERY, con);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            count2 = dt.Rows.Count;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            lbldpunos.Visible = true;
            lblpcode.Visible = true;
            lbldputrandnos.Visible = true;
            lbldputrandnos.Text = "No Of Rows=" + dt.Rows.Count.ToString();
            if (Rdolistperiod.SelectedValue == "1")
            {
                lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text;
            }

            if (Rdolistperiod.SelectedValue == "2")
            {
                lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text + "Session:AM";
            }

            if (Rdolistperiod.SelectedValue == "3")
            {
                lblpcode.Text = ddl_PlantName.SelectedItem.Text + "<br>" + "From Date:" + txt_FromDate.Text + "Session:AM";
            }

           /// lblpcode.Text = ddl_PlantName.SelectedItem.Text + "From Date:" + dt1 + "To Date" + dt2;
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
         //   GridView2.Rows[0].Cells[0].Text = "No Data Found";
            lbldputrandnos.Text = "No Of Rows" + dt.Rows.Count.ToString();
          //  lblmsg.Text = "No Rows In GridView";
           // lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

           
        }


    }





    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }  
    protected void Chk_single_CheckedChanged(object sender, EventArgs e)
    {
        getdpuagentlist();
        agentcode = ddl_AgentId.Text;

        if (Chk_single.Checked == true)
        {

            lblagent.Visible = true;
            ddl_AgentId.Visible = true;
        }
        else
        {

            lblagent.Visible = false;
            ddl_AgentId.Visible = false;

        }
      


    }


    public void getdpuagentlist()
    {
        ddl_AgentId.Items.Clear();
        try
        {
            string str = "";
            SqlConnection con = new SqlConnection(connStr2);
            con.Open();
            str = "select distinct(agent_code) as agent_code   from THIRUMALABILLSNEW  where plant_code='" + pcode + "' order by  agent_code ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    ddl_AgentId.Items.Add(dr["agent_code"].ToString());

                }

            }
            else
            {
              //  messagebox();

            }
        }
        catch (Exception ee)
        {

          //  message = ee.ToString();
           // messagebox();
//
        }
    }


    public void GetAgentdetailsAMPS()
    {
        string str = "";
        dt1.Rows.Clear();
        SqlConnection con = new SqlConnection(connStr1);
        con.Open();

        //    str = " select *   from VMCCDPU where plant_code='" + pcode + "' and agent_code='" + DdlAgentlist.Text + "'    and prdate='" + d3 + "' and   shift='" + ddl_shift.Text + "'";
        //For All agents



        if ((Rdolistperiod.SelectedValue == "2") && (RadioButtonList1.SelectedValue == "1"))
        {
            str = "select *   from VMCCDPU where plant_code='" + pcode + "'  AND     ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='AM'";
        }

        if ((Rdolistperiod.SelectedValue == "2") && (RadioButtonList1.SelectedValue == "2"))
        {
            str = "select *   from VMCCDPU where plant_code='" + pcode + "'  AND    ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='AM'";
        }

        if ((Rdolistperiod.SelectedValue== "3") && (RadioButtonList1.SelectedValue == "1"))
        {
            str = "select *   from VMCCDPU where plant_code='" + pcode + "'  AND  ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='PM'";
        }

        if ((Rdolistperiod.SelectedValue == "3") && (RadioButtonList1.SelectedValue == "2"))
        {
            str = "select *   from VMCCDPU where plant_code='" + pcode + "'  AND ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='PM'";
        }










        //if (Rdolistperiod.SelectedValue == "2")
        //{

        //    str = " select *   from VMCCDPU where plant_code='" + pcode + "'     and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "'))   and  shift='AM'";
        //}
        //if (Rdolistperiod.SelectedValue == "3")
        //{
        //    str = " select *   from VMCCDPU where plant_code='" + pcode + "'     and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "'))   and  shift='PM'";

        //}
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt1);

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            if (RadioButtonList1.SelectedValue != string.Empty && Rdolistperiod.SelectedValue!="1")
            {

                DateTime dt = new DateTime();
                dt = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                d1 = dt.ToString("MM/dd/yyyy");
                d2 = " 12:00:00";
                d3 = d1 + d2;
                lblmsg.Visible = false;
                GetAgentdetailsAMPS();

                if (dt1.Rows.Count == 0)
                {
                    bulkinsert();
                }
                else
                {
                    lblmsg.Text = "Data Already  inserted";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Visible = true;
                }

            }
            else
            {

                lblmsg.Text = "Please Select Insert Mode";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Visible = true;


            }
        }
    }

    public void bulkinsert()
    {
        GetAgentdetails();

        SqlConnection con = new SqlConnection(connStr1);
        con.Open();
        SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con);
        sqlBulkCopy.DestinationTableName = "dbo.VMCCDPU";
        sqlBulkCopy.ColumnMappings.Add("plant_code", "plant_code");
        sqlBulkCopy.ColumnMappings.Add("prdate", "prdate");
        sqlBulkCopy.ColumnMappings.Add("shift", "shift");
        sqlBulkCopy.ColumnMappings.Add("agent_code", "agent_code");
        sqlBulkCopy.ColumnMappings.Add("producer_code", "producer_code");
        sqlBulkCopy.ColumnMappings.Add("fat", "fat");
        sqlBulkCopy.ColumnMappings.Add("snf", "snf");
        sqlBulkCopy.ColumnMappings.Add("milk_kg", "milk_kg");
        sqlBulkCopy.WriteToServer(dt);
        con.Close();
        GetAgentdetailsAMPS();
        if (dt.Rows.Count == dt1.Rows.Count && dt.Rows.Count != 0)
        {

            lblmsg.Text = "Records successfully inserted";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            lblmsg.Visible = true;
          //  insertProduceragentlist();
        }
        else
        {

            DeleteAgentdetailsAMPS();
            lblmsg.Text = "Please Check Data";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            lblmsg.Visible = true;

        }


    }


    //public void insertProduceragentlist()
    //{

    //    try
    //    {
    //        getdpuaproducer();
    //        SqlConnection con = new SqlConnection(connStr2);
    //        SqlCommand cmd = new SqlCommand("insert_DpuProducerdetails", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.Add("@plantcode", SqlDbType.VarChar).Value = pcode;
    //        cmd.Parameters.Add("@agent_code", SqlDbType.VarChar).Value = DdlAgentlist.Text;
    //        cmd.Parameters.Add("@producer_code", SqlDbType.VarChar).Value = producers;
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //    }
    //    catch (SqlException ex)
    //    {
    //        Console.WriteLine("SQL Error" + ex.Message.ToString());

    //    }


    //}


    public void DeleteAgentdetailsAMPS()
    {
        string str = "";

        SqlConnection con = new SqlConnection(connStr1);
        con.Open();

        //    str = "delete   from VMCCDPU where plant_code='" + pcode + "' and agent_code='" + DdlAgentlist.Text + "'     and prdate='" + d3 + "' and  shift='" + ddl_shift.Text + "'";
        // for All agentss
        if (Rdolistperiod.SelectedValue == "2")
        {

            str = "delete   from VMCCDPU where plant_code='" + pcode + "'     and prdate='" + d3 + "' and  shift='AM";

        }
        if (Rdolistperiod.SelectedValue == "3")
        {

            str = "delete   from VMCCDPU where plant_code='" + pcode + "'     and prdate='" + d3 + "' and  shift='PM";

        }

        SqlCommand cmd = new SqlCommand(str, con);
        cmd.ExecuteNonQuery();

    }


    public void GetAgentdetails()
    {
        string str = "";
        dt.Rows.Clear();
        SqlConnection con = new SqlConnection(connStr2);
        con.Open();

        //   str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "' and agent_code='" + DdlAgentlist.Text + "'   and prdate='" + d3 + "'  and  shift='" + ddl_shift.Text + "'";
        //  str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'    and ((prdate='" + d1 + "' ) or (prdate='" + d3 + "') or  (prdate='" + d4 + "') or (prdate='" + d5 + "'))  and  shift='" + ddl_shift.Text + "'";
        if ((Rdolistperiod.SelectedValue == "2") && (RadioButtonList1.SelectedValue=="1"))
        {
            str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'  AND status is null   and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='AM'";
        }

        if ((Rdolistperiod.SelectedValue == "2") && (RadioButtonList1.SelectedValue=="2"))
        {
            str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'  AND status=1  and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='AM'";
        }

        if ((Rdolistperiod.SelectedValue == "3") && (RadioButtonList1.SelectedValue == "1"))
        {
            str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'  AND status is null   and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='PM'";
        }

        if ((Rdolistperiod.SelectedValue == "3") && (RadioButtonList1.SelectedValue == "2"))
        {
            str = "select *   from THIRUMALABILLSNEW where plant_code='" + pcode + "'  AND status=1  and ((prdate between '" + d1 + "' and '" + d1 + "') or (prdate between '" + d3 + "' and '" + d3 + "')  )   and  shift='PM'";
        }

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

    }



    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {


        if (roleid > 2)

            if (CheckBox1.Checked == true)
            {
                Save.Visible = true;
                RadioButtonList1.Visible = true;

            }
            else
            {
                Save.Visible = false;
                RadioButtonList1.Visible = false;

            }
        else
        {




        }
    }
}