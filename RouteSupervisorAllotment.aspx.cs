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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

using iTextSharp.text.html.simpleparser;

public partial class RouteSupervisorAllotment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();   
    DbHelper dbaccess = new DbHelper();
    SqlDataReader dr;
    BLLroutmaster BLroute = new BLLroutmaster();
    BLLTruck BLtruck = new BLLTruck();
    BLLtroute BLtroute = new BLLtroute();
    BOLvehicle vehicleBO = new BOLvehicle();
    public int companycode;
    public int plantcode;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    BLLuser Bllusers = new BLLuser();
    string frmdate, todate;
    int val;
    public static int roleid;
    DataTable rr = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                companycode = Convert.ToInt32(Session["Company_code"]);
                plantcode = Convert.ToInt32(Session["Plant_Code"]);
                if (roleid < 3)
                {
                    loadsingleplant();
                    LoadSupervisorId();
                    MChk_RouteName.Checked = true;
                   // LoadRouteName1();
                    LoadRouteName();
                    Mroute_RouteName();
                    RadioButtonList1.SelectedValue = "2";

                }
                if ((roleid >= 3)  && (roleid != 9))
                {
                  //  RadioButtonList1.SelectedValue = "2";
                    RadioButtonList1.SelectedValue = "2";
                    LoadPlantcode();
                    LoadSupervisorId();
                    MChk_RouteName.Checked = true;
                 //   LoadRouteName1();
                    LoadRouteName();
                    Mroute_RouteName();
                }
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    plantcode = 170;

                    RadioButtonList1.SelectedValue = "2";
                    loadspecialsingleplant();
                    LoadSupervisorId();
                    MChk_RouteName.Checked = true;
                    //   LoadRouteName1();
                    LoadRouteName();
                    Mroute_RouteName();



                }







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
                // uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                companycode = Convert.ToInt32(Session["Company_code"]);
                ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
                plantcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);
                //plantcode = Convert.ToInt32(Session["Plant_Code"]);
                if (program.Guser_role < program.Guser_PermissionId)
                {
                    //  loadsingleplant();                    
                    //  GetBankinfo();                   
                    ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
                    plantcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);


                    //getgriddata();
                }
                else
                {
                    ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
                    plantcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);


                    //  GetBankinfo();

                }
                //  getgriddata();

            }
        }

    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_PlantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(companycode.ToString(), "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }




    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    //private void LoadRouteName1()
    //{
    //    try
    //    {
    //        CheckBoxList2.Items.Clear();
    //        dr = null;
    //        dr = BLroute.getroutmasterdatareader4(companycode.ToString(), plantcode.ToString());
    //        if (dr.HasRows)
    //        {
    //            while (dr.Read())
    //            {
    //                CheckBoxList2.Items.Add(dr["Tot_Distance"].ToString() + '_' + dr["ROUTE_ID"].ToString());

    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    private void LoadRouteName()
    {
        try
        {
            ds = null;
            //ds = BLroute.getroutmasterdatareader3(companycode.ToString(), plantcode.ToString());
              con =  dbaccess.GetConnection();
            DataTable DS = new DataTable();
            string sqlstr;
            sqlstr = "select Route_ID,CAST(Route_ID as NVARCHAR)+'_'+Route_Name as Route_Name from Route_Master WHERE  status=1 and Company_code='" + companycode + "' AND Plant_code='" + plantcode + "' ORDER BY ROUTE_ID  ";
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataAdapter dsr=new SqlDataAdapter(cmd);
            rr.Rows.Clear();
            dsr.Fill(rr);

            if (rr.Rows.Count > 0 )
            {
                CheckBoxList1.DataSource = rr;
                CheckBoxList1.DataTextField = "ROUTE_NAME";
                CheckBoxList1.DataValueField = "ROUTE_ID";//ROUTE_ID 
                CheckBoxList1.DataBind();

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSupervisorId()
    {
        try
        {
            ds = null;
            ds = BLtruck.LoadSupervisorDetails(companycode, plantcode);
           if (ds != null)
            {
                ddl_SupervisorName.DataSource = ds;
                ddl_SupervisorName.DataTextField = "SupervisorName";
                ddl_SupervisorName.DataValueField = "Supervisor_Code";
                ddl_SupervisorName.DataBind();

            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }

    }

    private void Mroute_RouteName()
    {
        if (MChk_RouteName.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
                //CheckBoxList2.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
                //CheckBoxList2.Items[i].Selected = false;
            }

        }
    }

    private bool Validates()
    {
        if (ddl_SupervisorName.Text == "--Select Superviosor--")
        {
            uscMsgBox1.AddMessage("Select Supervisor_ID", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
       
        return true;

    }

   
    private void SETBO()
    {
        try
        {
            vehicleBO.Companycode = companycode;
            vehicleBO.Plantcode = plantcode;
            vehicleBO.Truckid = Convert.ToInt32(ddl_SupervisorName.SelectedItem.Value);
            
        }
        catch (Exception ex)
        {

        }
    }
    protected void MChk_RouteName_CheckedChanged(object sender, EventArgs e)
    {
        Mroute_RouteName();
    }


    private void loadsingleplant()
    {
        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadSinglePlantcode (Convert.ToString(companycode),Convert.ToString(plantcode));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

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

            dr = Bllusers.LoadPlantcode(companycode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    public void getbillperiod()
    {
        try
        {
            using (con = dbaccess.GetConnection())
            {
                string getdate = string.Empty;
                getdate = " Select Bill_frmdate,Bill_Todate  from Bill_date where plant_code=155 and status=0";
                SqlCommand cmd = new SqlCommand(getdate,con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                    frmdate= dr["Bill_frmdate"].ToString(); 
                    todate = dr["Bill_Todate"].ToString(); 

                    }


                }
                else
                {
                }

            }

        }
        catch
        {


        }


    }

    public void getgrid()
    {
       
        string grid;
        getbillperiod();
        using(con=dbaccess.GetConnection())
        {
           if(RadioButtonList1.SelectedValue=="1")
           {

                grid = "  select supervisorname,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + plantcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid  FROM  Route_Master WHERE Plant_Code='" + plantcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + plantcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code";
            }
            else
            {


                grid = "select supervisorname,RouteName from(select distinct(Route_id) as R from procurement where plant_code='" + plantcode + "' and prdate between '" + frmdate + "' and '" + todate + "' )as pro left join( select aa.Supervisor_Code,supervisorname,routeid,Convert(nvarchar(50),routeid)+ '_' + Route_Name as RouteName from( SELECT Supervisor_Code,Route_Name,routeid FROM (  SELECT  *  FROM  Supervisor_RouteAllotment WHERE Plant_Code='" + plantcode + "') AS A LEFT JOIN (  SELECT  Route_Name,route_id as routeid  FROM  Route_Master WHERE Plant_Code='" + plantcode + "') AS B  ON A.Route_Id=B.routeid) as aa  left join(SELECT  *  FROM  Supervisor_Details WHERE Plant_Code='" + plantcode + "')as bb on aa.Supervisor_Code=bb.Supervisor_Code) as alltable on pro.R=alltable.routeid   where alltable.routeid > 0 order by alltable.supervisorname ";

            }
            //  billroute //    
        SqlCommand cmd = new SqlCommand(grid, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
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


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (Validates())
            {
                if (MChk_RouteName.Checked == true)
                {                    

                    for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                    {
                        if (CheckBoxList1.Items[i].Selected == true)
                        {                                        
                            vehicleBO.Companycode = companycode;
                            vehicleBO.Plantcode = plantcode;
                            vehicleBO.Truckid = Convert.ToInt32(ddl_SupervisorName.SelectedItem.Value);
                            vehicleBO.Routeid = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                            BLtroute.InsertRouteSupervisorAllotment(vehicleBO);
                        }                       
                    }

                }
                else
                {
                    uscMsgBox1.AddMessage("please,Select the RouteName_Master", MessageBoxUsc_Message.enmMessageType.Attention);


                }
            }
            getgrid();
            MChk_RouteName.Checked = false;
            CheckBoxList1.SelectedValue = "0";
        }
        catch (Exception ex)
        {

        }

    }
  
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        try
        {
            if (roleid > 6)
            {
                con = null;
                using (con = dbaccess.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Supervisor_RouteAllotment WHERE Company_Code='" + companycode + "' AND Plant_Code='" + plantcode + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                getgrid();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getgrid();
    }


    protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantcode.SelectedIndex = ddl_PlantName.SelectedIndex;
        plantcode = Convert.ToInt32(ddl_Plantcode.SelectedItem.Text);

    

        getgrid();
       

        LoadSupervisorId();
        MChk_RouteName.Checked = true;
       // LoadRouteName1();
        LoadRouteName();
        Mroute_RouteName();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
           val=1;
        }
        else
        {
           val = 2;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int i = 1; i < GridView1.Rows.Count; i++)
        //{

        //    if (grdUniqueNames.Rows[i].Cells[cellno].Text == initialnamevalue)
        //        grdUniqueNames.Rows[i].Cells[cellno].Text = string.Empty;
        //    else
        //        initialnamevalue = grdUniqueNames.Rows[i].Cells[cellno].Text;
        //}



        try
        {
            string initialnamevalue = GridView1.Rows[0].Cells[1].Text;

            //Step 2:

            for (int i = 1; i < GridView1.Rows.Count; i++)
            {

                if (GridView1.Rows[i].Cells[1].Text == initialnamevalue)
                    GridView1.Rows[i].Cells[1].Text = string.Empty;
                else
                    initialnamevalue = GridView1.Rows[i].Cells[1].Text;
            }
        }
        catch
        {


        }


    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            string get = "SuperVisor Allotment";
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = get;
            HeaderCell2.ColumnSpan = 3;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            HeaderCell2.Font.Bold = true;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;


            GridViewRow HeaderRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell21 = new TableCell();
            HeaderCell21.Text = " Plant Name:" + ddl_PlantName.Text  ;
            HeaderCell21.ColumnSpan = 3;
            HeaderRow1.Cells.Add(HeaderCell21);
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow1);
            HeaderCell21.Font.Bold = true;
            HeaderCell21.HorizontalAlign = HorizontalAlign.Center;







            //HeaderCell2.ForeColor = System.Drawing.Color.Brown;

        }
    }
    protected void btn_Save_Click1(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                getgrid();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }


        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}