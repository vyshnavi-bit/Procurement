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
public partial class NewRateChartViewer : System.Web.UI.Page
{
    BLLRateChart rateBLL = new BLLRateChart();
    DbHelper dbaccess = new DbHelper();
    BOLRateChart rateBOL = new BOLRateChart();
    DataTable dt = new DataTable();
    DataRow dr;
    SqlDataReader dar;
    BLLuser Bllusers = new BLLuser();
    int DeleteFlag;
    string Company_code;
    string plant_Code;
	string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
    //    uscMsgBox1.MsgBoxAnswered += MessageAnswered;
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                btn_delete.Visible = false;
                if (roleid < 3)
                {
                    loadsingleplant();
                    Button1.Visible = false;
                    //btn_delete.Visible = false;
                }
               if ((roleid >= 3) && (roleid != 9) )
               {
                    LoadPlantcode();
                    Button1.Visible = true;
                    //btn_delete.Visible = true;
               }
               if (roleid==9)
               {
                   Session["Plant_Code"] = "170".ToString();
                   plant_Code = "170";
                   loadspecialsingleplant();
                   Button1.Visible = true;
                   //btn_delete.Visible = true;
               }


               plant_Code = ddl_plantcode.SelectedItem.Value;
               loadratechart();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                plant_Code = ddl_plantcode.SelectedItem.Value;
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }


            if ((roleid == 7) || (roleid == 9))
            {
                
                Button1.Visible = false;
                //btn_delete.Visible = false;
            }
            else
            {
               
                Button1.Visible = true;
                //btn_delete.Visible = true;
            }
        }
    }
    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(Company_code.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
                ddl_plantcode.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(Company_code, plant_Code);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(Company_code, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void loadgriddata()
    {

        int dtcount;

        dt = rateBLL.LoadRatechartGriddata(dl_RatechartName.SelectedItem.Text.Trim(), plant_Code, Company_code);
        dtcount = dt.Rows.Count;
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();

            WebMsgBox.Show("This Rate chat have Emty Rows Only...");
        }


    }

    private void loadgrid(string chartname)
    {

        int dtcount = 0;

        dt = rateBLL.LoadRatechartGriddata(dl_RatechartName.SelectedItem.Text.Trim(), plant_Code, Company_code);
        dtcount = dt.Rows.Count;
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
     //       uscMsgBox1.AddMessage("This Rate chat have Empty Rows Only...", MessageBoxUsc_Message.enmMessageType.Attention);

        }


    }

    private void loadgriddata1()
    {

        int dtcount;

        dt = rateBLL.LoadRatechartGriddata1(plant_Code, Company_code);
        dtcount = dt.Rows.Count;
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            WebMsgBox.Show("This Rate chat have Empty Rows Only...");
        }


    }
    private void loadratechart()
    {
		dl_RatechartName.Items.Clear();
        string type = string.Empty; 
        if(rbcow.Checked==true)
        {
            type = "Cow";
            rbbuff.Checked = false;
        }
        else
        {
            type = "Buffalo";
            rbcow.Checked = false;
        }
		string sqlstr;
		sqlstr = "select Chart_Name as ChartName from(select ll.Chart_Name from (select *  from Chart_Master where Plant_Code='" + plant_Code + "'  and active=1 and Milk_Nature='" + type + "') as ll left join ( select *   from  Rate_Chart where Plant_Code='"+plant_Code+"' ) as mm on ll.Plant_Code=mm.Plant_Code and ll.Chart_Name=mm.Chart_Name) as dd group by dd.Chart_Name";
		 SqlConnection con=new SqlConnection(connStr);
         con.Open();
	  	SqlCommand cmd=new SqlCommand(sqlstr,con);
		SqlDataReader dr=cmd.ExecuteReader();
		if (dr.HasRows)
        {
            while (dr.Read())
            {
				dl_RatechartName.Items.Add(dr["ChartName"].ToString());
                
            }

            if (dl_RatechartName.Items.Count <= 0)
            {
                
                btn_delete.Visible = false;
            }
            else
            {
                loadgriddata();
                if (roleid == 7) 
                {
                    btn_delete.Visible = true;
                }
                else
                {
                    btn_delete.Visible = false;

                }
            }
        }
        else
        {
    //        uscMsgBox1.AddMessage("RateChart Not Found...", MessageBoxUsc_Message.enmMessageType.Attention);
            
        }

    }
    
    private void loaddata()
    {
        int dtcount;
        SqlConnection con = new SqlConnection();
        using (con = dbaccess.GetConnection())
        {
            string str;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            str = "SELECT * FROM Rate_Chart WHERE Chart_Name='" + dl_RatechartName.SelectedItem.Text.Trim() + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da.Fill(dt);
            dtcount = dt.Rows.Count;
        }
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
        //    uscMsgBox1.AddMessage("This Rate chat have Empty Rows Only...", MessageBoxUsc_Message.enmMessageType.Attention);
        }

    }
    public void GridExport(GridView gv)
    {
        HtmlForm forms = new HtmlForm();
        string attachment = "attachment; filename=PrintDetails.xls";
        Response.ClearContent();
        Response.AppendHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.Parent.Controls.Add(forms);
        forms.Attributes["runat"] = "server";
        forms.Controls.Add(gv);
        this.Controls.Add(forms);
        Response.Write(stw.ToString());
        Response.End();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        if (roleid == 7)
        {
            GridView1.EditIndex = -1;
            loadgrid(dl_RatechartName.Text);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (program.Guser_role > program.Guser_PermissionId)
        {
            try
            {
                loadgriddata();
                GridView1.EditIndex = e.RowIndex;
                int index = GridView1.EditIndex;
                GridViewRow row = GridView1.Rows[index];
                dr = dt.Rows[index];
                DeleteFlag = 1;
                rateBOL.Tid = Convert.ToInt32(dr["Table_ID"]);
                rateBOL.Chartname = dl_RatechartName.SelectedItem.Text;
                rateBOL.Flag = DeleteFlag;

                rateBOL.Plantcode = plant_Code;
                rateBOL.Companycode = Company_code;
                rateBLL.DlelteRow(rateBOL);
                rateBOL.Flag = 0;
                loadratechart();
                loadgrid(dl_RatechartName.Text);
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.ToString());
            }
        }
        else
        {
            WebMsgBox.Show("Permission Not Available...");
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        if (roleid == 7)
        {
            GridView1.EditIndex = e.NewEditIndex;

            loadgriddata();
        }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (roleid==7)
        {
        try
        {
            int index = GridView1.EditIndex;
            GridViewRow row = GridView1.Rows[index];
            loadgriddata();
            int count;
            count = dt.Rows.Count;
            dr = dt.Rows[index];

            rateBOL.Tid = Convert.ToInt32(dr["Table_ID"]);
            rateBOL.Chartname = dl_RatechartName.SelectedItem.Text;
            rateBOL.Fromrangevalue = Convert.ToDouble(((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim());
            rateBOL.Torangrvalue = Convert.ToDouble(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim());
            rateBOL.Rate = Convert.ToDouble(((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim());
            rateBOL.Commissionamount = Convert.ToDouble(((TextBox)row.Cells[4].Controls[0]).Text.ToString().Trim());
            rateBOL.Bonusamount = Convert.ToDouble(((TextBox)row.Cells[5].Controls[0]).Text.ToString().Trim());

            rateBOL.Plantcode = plant_Code;
            rateBOL.Companycode = Company_code;

            rateBLL.EditUpdateRatechart(rateBOL);
            GridView1.EditIndex = -1;
            loadgrid(dl_RatechartName.Text);
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
        }
        else
        {
            WebMsgBox.Show("Permission Not Available...");
        }
       
    }
    protected void dl_RatechartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadgriddata();        
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        try
        {
            //uscMsgBox1.AddMessage("Do you confirm to Delete Record?.", MessageBoxUsc_Message.enmMessageType.Attention, true, true, dl_RatechartName.Text);
            if (roleid == 7)
            {
                rateBOL.Tid = 0;
                rateBOL.Chartname = dl_RatechartName.SelectedItem.Text;
                rateBOL.Flag = 0;

                rateBOL.Plantcode = plant_Code;
                rateBOL.Companycode = Company_code;
                rateBLL.DlelteRow(rateBOL);
                loadratechart();
                if (dl_RatechartName.Items.Count > 0)
                {
                    loadgrid(dl_RatechartName.Text);
                  //  btn_delete.Visible = true;
                }
                else
                {
                    loadgriddata1();
                  //  btn_delete.Visible = false;
                }

            }
    //        uscMsgBox1.AddMessage("Deleted Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }

    }
	//public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
	//{
	//    if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
	//    {
	//        uscMsgBox1.AddMessage("You have just confirmed the transaction. The RateChart Deleted successfully. You have entered " + dl_RatechartName.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
	//    }
	//    else
	//    {
	//        uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
	//    }
	//}
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridExport(GridView1);
    }
    
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("RateChart.aspx");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void rbcow_CheckedChanged(object sender, EventArgs e)
    {
        if (rbcow.Checked == true)
        {
            rbbuff.Checked = false;
            loadratechart();
        }
        
               
    }
    protected void rbbuff_CheckedChanged(object sender, EventArgs e)
    {
        if (rbbuff.Checked == true)
        {
            rbcow.Checked = false;
            loadratechart();
        }
        
    }
       
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        GridExport(GridView1);
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        plant_Code = ddl_plantcode.SelectedItem.Value;
        loadratechart();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        loadratechart();
    }
}
