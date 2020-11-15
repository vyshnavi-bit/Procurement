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
using System.IO;
using System.Collections.Generic;

public partial class AgentsCansdpuReport : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string routeid;
    public string managmobNo;
    public string pname;
    public string cname;
    DataSet ds = new DataSet();
    DateTime tdt = new DateTime();
    string strsql = string.Empty;
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    int returnvalue;
    string plantname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    string rid;
    string path;
    int validateval;
    int mode = 0;
    int mode1;
    int total = 0;
    int total1 = 0;
    int tottal2 = 0;
    int tottal3 = 0;
    int TotPendingCans = 0;
    int Totpendingddpu = 0;
    string oldValue=string.Empty;
    string newValue=string.Empty;
    String value;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {

                Session["Image"] = null;

                ccode = Session["Company_code"].ToString();
                   pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                managmobNo = Session["managmobNo"].ToString();
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    loadsingleplant();
                }
                else
                {
                    LoadPlantcode();
                }
                //  pcode = ddl_Plantcode.SelectedItem.Value;
                //  gridview();

                GridView1.Visible = true;
                lbl_pname.Visible = false;
                //dtm = System.DateTime.Now;
                //txt_joining.Text = dtm.ToShortDateString();
                //txt_joining.Text = dtm.ToString("dd/MM/yyyy");
                //    Button1.Visible = false;
                //GridView2.Visible = false;
                //ddl_Plantnam.Visible = false;
                //Button2.Visible = false;
                //Label17.Visible = false;
                //Label18.Visible = false;
                //dtp_buff.Visible = false;
                //dtp_cow.Visible = false;

                //    Label20.Visible = false;

                if (rdosingle.Checked == true)
                {

                    Label21.Visible = true;
                    ddl_Agentid.Visible = true;
                }
                else
                {

                    Label21.Visible = false;
                    ddl_Agentid.Visible = false;

                }

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }
        else
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                ccode = Session["Company_code"].ToString();
                //  pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
                managmobNo = Session["managmobNo"].ToString();
                //ddl_Agentid.SelectedIndex = ddl_Agentid.SelectedIndex;

                pcode = ddl_Plantcode.SelectedItem.Value;

              if (rdosingle.Checked == true)
                {

                    Label21.Visible = true;
                    ddl_Agentid.Visible = true;
                }
                else
                {

                    Label21.Visible = false;
                    ddl_Agentid.Visible = false;

                }
             //   gridview();

                //  getagentid();
              lbl_pname.Visible = false;

                GridView1.Visible = true;

                //  getgrid();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;



        if (Rtoall.Checked == true)
        {

            gridview();

        }
        if (rdosingle.Checked == true)
        {
            getagentid();
            gridview();


        }


    }



    public void getagentid()
    {

        try
        {
            ddl_Agentid.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //   string str = "select * from Agent_Master where plant_code='" + pcode + "'  order by rand(agent_id)  ";
            string str = "select distinct agent_id from agent_master where plant_code='" + pcode + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddl_Agentid.Items.Add(dr["agent_id"].ToString());
                // txt_AgentName.Text = dr["Agent_Name"].ToString();


            }


        }

        catch
        {

            WebMsgBox.Show("NO MILK");

        }



    }
    protected void Rtoall_CheckedChanged(object sender, EventArgs e)
    {
        rdosingle.Checked = false;
        Rtoall.Checked = true;
        if (Rtoall.Checked == true)
        {

            Label21.Visible = false;
            ddl_Agentid.Visible = false;

            rdosingle.Checked = false;
            Rtoall.Checked = true;
            gridview();
        }

        if (rdosingle.Checked == true)
        {
            rdosingle.Checked = true;
            Rtoall.Checked = false;
            Label21.Visible = true;
            ddl_Agentid.Visible = true;


        }
    }
    protected void rdosingle_CheckedChanged(object sender, EventArgs e)
    {
        rdosingle.Checked = true;
        Rtoall.Checked = false;

        if (Rtoall.Checked == true)
        {

            Label21.Visible = false;
            ddl_Agentid.Visible = false;
            rdosingle.Checked = false;
            Rtoall.Checked = true;
            gridview();

        }

        if (rdosingle.Checked == true)
        {
            rdosingle.Checked = true;
            Rtoall.Checked = false;
            getagentid();
            Label21.Visible = true;
            ddl_Agentid.Visible = true;
            gridview();

        }
    }



    public void gridview()
    {



        try
        {


            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                //      string sqlstr = "SELECT Plant_Name,PlantType,StateName,PlaceName,Mailid,Mgrname,MgrMobile,PlantPhone,InchargeName,InchargeMobile,analyzermobile,weighermobile,securitymobile  FROM PlantDetails   order by  Tid desc";
                //      string sqlstr = "SELECT Plant_Name as PlantName,PlaceName,Mailid,Mgrname as ManagerName,MgrMobile as ManagerMobile,PlantPhone as PhoneNo,InchargeName,InchargeMobile  FROM PlantDetails   order by  Tid desc";
                //    string sqlstr = "select distinct a.Agent_Id as AgentId,a.AgentRateChartmode  as RateChartType,b.Ratechart_Id as RatechartName from (select   Agent_Id,AgentRateChartmode   from    agent_master   where   plant_code='" + pcode + "' and route_id='" + routeid + "'  ) as a left join(	select  *    from   procurement where Plant_Code='" + pcode + "' and  route_id='" + routeid + "' ) as b on a.Agent_Id=b.Agent_id order by a.Agent_Id asc ";
                //   string sqlstr = "select  a.agent_id,b.agentratechartmode  from( select distinct  agent_id    from    procurement    where   plant_code='" + pcode + "' and route_id='" + routeid + "') as a left join(select *   from   agent_master  where  plant_code='" + pcode + "' and route_id='" + routeid + "') as b on a.agent_id=b.agent_id   order by a.agent_id";

                if (Rtoall.Checked == true)
                {
                 string sqlstr = "select   Agent_id,convert(varchar,DateIssuingOrReceiving,103) as DateIssuingOrReceiving,ISNULL(CanIssuing,0)as CanIssuing,ISNULL(CanReceiving,0)as CanReceiving, ISNULL(CanType,0)as CanType,ISNULL(DpuIssuing,0)as DpuIssuing,ISNULL(DpuReceiving,0)as DpuReceiving,ISNULL(DpuType,0)as DpuType   from  AgentsCanIssuing  where plant_code='"+pcode+"'";
               //   string sqlstr = "select   Plant_Name,Agent_id,convert(varchar,DateIssuingOrReceiving,103) as DateIssuingOrReceiving,ISNULL(CanIssuing,0)as CanIssuing,ISNULL(CanReceiving,0)as CanReceiving, ISNULL(CanType,0)as CanType,ISNULL(DpuIssuing,0)as DpuIssuing,ISNULL(DpuReceiving,0)as DpuReceiving,ISNULL(DpuType,0)as DpuType   from  AgentsCanIssuing  ";

                    SqlCommand COM = new SqlCommand(sqlstr, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                    sqlDa.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {

                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        lbl_pname.Text = ddl_Plantname.Text;
                        lbl_pname.Visible = true;
                    }
                    else
                    {

                        GridView1.DataSource = null;
                        GridView1.DataBind();
                       
                    }
                }
                if (rdosingle.Checked == true)
                {

                    string sqlstr = "select   Agent_id,convert(varchar,DateIssuingOrReceiving,103) as DateIssuingOrReceiving,ISNULL(CanIssuing,0)as CanIssuing,ISNULL(CanReceiving,0)as CanReceiving, ISNULL(CanType,0)as CanType,ISNULL(DpuIssuing,0)as DpuIssuing,ISNULL(DpuReceiving,0)as DpuReceiving,ISNULL(DpuType,0)as DpuType   from  AgentsCanIssuing  where plant_code='"+pcode+"' and agent_id='"+ddl_Agentid.Text+"'";
                    SqlCommand COM = new SqlCommand(sqlstr, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
                    sqlDa.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {

                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        lbl_pname.Text = ddl_Plantname.Text;
                        lbl_pname.Visible = true;
                    }
                    else
                    {

                        GridView1.DataSource = null;
                        GridView1.DataBind();

                    }

                }





            }
        }
        catch
        {



        }


    }
    protected void ddl_Agentid_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdosingle.Checked = true;
        Rtoall.Checked = false;

        Label21.Visible = true;
        ddl_Agentid.Visible = true;
        gridview();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[0].Text.Equals(value))
            //    e.Row.Cells[0].Text = "";
            value = e.Row.Cells[0].Text;

        }


       // GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
       // TableHeaderCell cell = new TableHeaderCell();
       // cell.Text = "Customers";
       // cell.ColumnSpan = 2;
       // row.Controls.Add(cell);

       // cell = new TableHeaderCell();
       // cell.ColumnSpan = 2;
       //   cell.Text = value;
       //   if (cell.Text == "")
       //   {
       //       cell.Text = "0";


       //   }
       //   else
       //   {
       //       cell.Text = value;
       //   }
       // row.Controls.Add(cell);
       //GridView1.HeaderRow.Parent.Controls.AddAt(0, row);              
       // string nn = string.Empty;
        int qty;
        int qty1;
        int qty12;
        int qty123;
      
        //  string nn1=string.Empty;





        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy = (Label)e.Row.FindControl("lblcanissue");
            try
            {
                qty = Int32.Parse(lblqy.Text);
            }
            catch
            {

                qty = 0;
            }
            total = total + qty;

            if (qty > 0)
            {
                lblqy.ForeColor = System.Drawing.Color.Green;
                lblqy.Font.Bold = true;
            }
            else
            {

                lblqy.ForeColor = System.Drawing.Color.Red;
                lblqy.Font.Bold = true;


            }
          





        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalcanissue");
            lblTotalqty.Text = total.ToString();







        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy1 = (Label)e.Row.FindControl("lblCanReceiving");
            try
            {
                qty1 = Int32.Parse(lblqy1.Text);
            }
            catch
            {
                qty1 = 0;


            }
            total1 = total1 + qty1;





            if (qty1 > 0)
            {
                lblqy1.ForeColor = System.Drawing.Color.Green;
                lblqy1.Font.Bold = true;
            }
            else
            {

                lblqy1.ForeColor = System.Drawing.Color.Red;
                lblqy1.Font.Bold = true;


            }


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty1 = (Label)e.Row.FindControl("lblTotalCanReceiving");
            lblTotalqty1.Text = total1.ToString();
        }


         if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblpendingcan = (Label)e.Row.FindControl("Lblpending");
        //    lblpendingcan.Text = int.Parse(total - total1);
            TotPendingCans = (total - total1);
            lblpendingcan.Text = TotPendingCans.ToString();
        }


       








        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy12 = (Label)e.Row.FindControl("lbldpuissue");
            try
            {
                qty12 = Int32.Parse(lblqy12.Text);
            }
            catch
            {
                qty12 = 0;

            }
            tottal2 = tottal2 + qty12;




            if (qty12 > 0)
            {
                lblqy12.ForeColor = System.Drawing.Color.Green;
                lblqy12.Font.Bold = true;
            }
            else
            {

                lblqy12.ForeColor = System.Drawing.Color.Red;
                lblqy12.Font.Bold = true;


            }



        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty12 = (Label)e.Row.FindControl("lblTotalDpuissue");
            lblTotalqty12.Text = tottal2.ToString();
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblqy123 = (Label)e.Row.FindControl("lbldpurec");
            try
            {
                qty123 = Int32.Parse(lblqy123.Text);
            }
            catch
            {

                qty123 = 0;

            }

            tottal3 = tottal3 + qty123;




            if (qty123 > 0)
            {
                lblqy123.ForeColor = System.Drawing.Color.Green;
                lblqy123.Font.Bold = true;
            }
            else
            {

                lblqy123.ForeColor = System.Drawing.Color.Red;
                lblqy123.Font.Bold = true;


            }




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty112 = (Label)e.Row.FindControl("lblTotalDpurec");
            lblTotalqty112.Text = tottal3.ToString();
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblpendingdpu = (Label)e.Row.FindControl("LblDpupending");
            //    lblpendingcan.Text = int.Parse(total - total1);
            TotPendingCans = (tottal2 - tottal3);
            lblpendingdpu.Text = TotPendingCans.ToString();
        }





        //string oldValue = string.Empty;
        //string newValue = string.Empty;
        //for (int column = 0; column < 2; column++)
        //{
        //    for (int row = 0; row < GridView1.Rows.Count; row++)
        //    {

        //        if (GridView1.Rows[row].Cells[column].Text != "")
        //            oldValue = GridView1.Rows[row].Cells[column].Text;

        //        if (oldValue == newValue)
        //        {
        //            GridView1.Rows[row].Cells[column].Text = string.Empty;
        //        }

        //        if (row + 1 < GridView1.Rows.Count)
        //            newValue = GridView1.Rows[row + 1].Cells[column].Text;
        //    }
        //}

       

       
            //for (int count = 0; count < GridView1.Rows.Count; count++)
            //{
            //    oldValue = GridView1.Rows[count].Cells[0].Text.Trim();
            //    if (oldValue == newValue)
            //    {
            //        GridView1.Rows[count].Cells[0].Text = string.Empty;
            //    }
            //    else
            //    {
            //        newValue = oldValue;
            //    }
            //}









    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        gridview();
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
}