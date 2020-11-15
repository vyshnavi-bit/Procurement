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


public partial class TruckRouteAllotment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();    
    DbHelper dbaccess = new DbHelper();
    SqlDataReader dr;
    DataSet ds = new DataSet();
    BLLroutmaster BLroute = new BLLroutmaster();
    BLLTruck BLtruck = new BLLTruck();
    BLLtroute BLtroute = new BLLtroute();
    BOLvehicle vehicleBO = new BOLvehicle();
    BLLPlantName BllPlant = new BLLPlantName();
    public int companycode;
    public int plantcode;
    public static int roleid;
    DataTable rr = new DataTable();
    DataTable rr1 = new DataTable();
    BLLuser Bllusers = new BLLuser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                companycode = Convert.ToInt32(Session["Company_code"]);
                plantcode = Convert.ToInt32(Session["Plant_Code"]);

                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                if ((roleid >= 3)  && (roleid != 9))
                {
                    LoadPlantName();
                }
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    plantcode = 170;
                    loadspecialsingleplant();

                }

                plantcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
                LoadTruckId();
                MChk_RouteName.Checked = true;
                LoadRouteName1();
                LoadRouteName();
                Mroute_RouteName();
                LoadTruckGrid();
                Lbl_Errormsg.Visible = false;
               
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
                companycode = Convert.ToInt32(Session["Company_code"]);
                plantcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
                Lbl_Errormsg.Visible = false;
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }

    }

    
    private void LoadRouteName1()
    {
        try
        {           
            //dr = null;
            //dr = BLroute.getroutmasterdatareader4(companycode.ToString(), plantcode.ToString());
            con = dbaccess.GetConnection();
            string sqlstr;
            sqlstr = "Select Tot_Distance, ROUTE_ID,Route_Name from Route_Master WHERE  status=1 and Company_code='" + companycode + "' AND Plant_code='" + plantcode + "' ORDER BY ROUTE_ID  ";
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataReader dr = cmd.ExecuteReader();

            CheckBoxList2.Items.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CheckBoxList2.Items.Add(dr["Tot_Distance"].ToString() + '_' + dr["ROUTE_ID"].ToString());
                    
                }               
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
        }
    }
    private void LoadRouteName()
    {
        try
        {
            ds = null;
            ds = BLroute.getroutmasterdatareader3(companycode.ToString(), plantcode.ToString());
            con = dbaccess.GetConnection();
            string sqlstr;
            sqlstr = "select Route_ID,CAST(Route_ID as NVARCHAR)+'_'+Route_Name as Route_Name from Route_Master WHERE   STATUS=1 AND Company_code='" + companycode + "' AND Plant_code='" + plantcode + "' ORDER BY ROUTE_ID  ";
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataAdapter dsr = new SqlDataAdapter(cmd);

            rr.Rows.Clear();
            dsr.Fill(rr1);


            CheckBoxList1.Items.Clear();
            if (rr1 != null)
            {
                CheckBoxList1.DataSource = rr1;
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

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = dbaccess.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            DataTable getdata = new DataTable();
            SqlDataAdapter dsii = new SqlDataAdapter(cmd);
            getdata.Rows.Clear();
            dsii.Fill(getdata);
            ddl_Plantname.DataSource = getdata;
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
            ddl_Plantname.DataBind();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void Mroute_RouteName()
    {
        try
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
        catch (Exception ex)
        {

        }
        

    }
    private bool Validates()
    {
        if (ddl_VehicleName.Text == "--Select Truck--")
        {          
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
            Lbl_Errormsg.Text = "Select Truck_ID".ToString();
                
            return false;
        }
        if (txt_TotDistance.Text == "")
        {           
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
            Lbl_Errormsg.Text = "please, Enter the TotDistance".ToString();
            return false;
        }
        return true;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Validates())
            {
                if (MChk_RouteName.Checked == true)
                {
                    // int Distance = 0;
                    int Totdistance = 0;

                    for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                    {
                        if (CheckBoxList1.Items[i].Selected == true)
                        {
                            // Distance = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                            // Totdistance = Totdistance + Distance;
                            //SETBO()               
                            vehicleBO.Companycode = companycode;
                            vehicleBO.Plantcode = plantcode;
                            vehicleBO.Truckid = Convert.ToInt32(ddl_VehicleName.SelectedItem.Value);
                            vehicleBO.Routeid = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                            BLtroute.inserttroute(vehicleBO);
                        }
                        if (i == CheckBoxList1.Items.Count - 1)
                        {
                            SaveData();
                            //Totdistance = Convert.ToInt32(txt_TotDistance.Text.Trim());
                            //string sqlstr = null;
                            //int Tid = Convert.ToInt32(ddl_VehicleName.SelectedItem.Value);

                            //sqlstr = "INSERT INTO TRUCK_ROUTEDISTANCE (Company_Code,Plant_Code,Truck_Id,Distance) VALUES (" + companycode + "," + plantcode + "," + Tid + "," + Totdistance + ") ";
                            // dbaccess.ExecuteNonquorey(sqlstr);
                        }
                    }

                }
                else
                {                    
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
                    Lbl_Errormsg.Text = "please,Select the RouteName_Master".ToString();
                }
            }
           
        }
        catch (Exception ex)
        {

        }
        
    }
    private void SaveData()
    {
        try
        {
            string mess = string.Empty;
            SETBO();
            mess = BLtroute.insertTroutedistance(vehicleBO);
            txt_TotDistance.Text = "";
            LoadTruckGrid();

            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.ForeColor = System.Drawing.Color.Green;
            Lbl_Errormsg.Text = mess.ToString();
        }
        catch (Exception ex)
        {

        }
    }
    private void SETBO()
    {
        try
        {
            vehicleBO.Companycode = companycode;
            vehicleBO.Plantcode = plantcode;
            vehicleBO.Truckid = Convert.ToInt32(ddl_VehicleName.SelectedItem.Value);
            vehicleBO.Distance = Convert.ToDouble(txt_TotDistance.Text);
        }
        catch (Exception ex)
        {

        }
    }
    protected void MChk_RouteName_CheckedChanged(object sender, EventArgs e)
    {
        Mroute_RouteName();
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        try
        {
          //  int Uid = Convert.ToInt32(Session["User_ID"]);
            if (roleid > 5)
            {               
                    con = null;
                    using (con = dbaccess.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Truck_RouteAllotment WHERE Company_Code='" + companycode + "' AND Plant_Code='" + plantcode + "' ", con);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM Truck_RouteDistance WHERE Company_Code='" + companycode + "' AND Plant_Code='" + plantcode + "' ", con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        LoadTruckGrid();
                    }             

            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.ForeColor = System.Drawing.Color.Red;
                Lbl_Errormsg.Text = "You have No Permission to Delete...".ToString();
            }

        }

        catch (Exception ex)
        {

        }
    }
    private void LoadPlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(companycode.ToString());
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

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
            ds = BllPlant.LoadSinglePlantNameChkLst1(companycode.ToString(), plantcode.ToString());
            if (ds != null)
            {
                ddl_Plantname.DataSource = ds;
                ddl_Plantname.DataTextField = "Plant_Name";
                ddl_Plantname.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_Plantname.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadTruckGrid()
    {
        try
        {
            plantcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);

            string str = string.Empty;
            SqlConnection con = null;
            string connection = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            con = new SqlConnection(connection);
            str = "SELECT ROW_NUMBER() OVER  (ORDER BY Route_Id ) AS Sno,Route_Name,Truck_Id,TotDistance FROM " +
" (SELECT CONVERT(Nvarchar(15),t1.Truck_Id)+'_'+t1.Vehicle_No AS Truck_Id,t1.Route_Id AS Route_Id,Tdis.Distance AS TotDistance,t1.Company_Code AS Company_Code,t1.Plant_Code AS Plant_Code FROM " +
" (SELECT Company_Code,Plant_Code,Truck_Id,Vehicle_No,tal.Route_Id FROM " +
" (SELECT Company_Code,Plant_Code,Truck_Id,Vehicle_No FROM Vehicle_Details WHERE Company_Code='" + companycode + "' AND Plant_Code='" + plantcode + "' and status=1 )AS vd " +
" LEFT JOIN " +
" (SELECT Company_Code AS Allcode,Plant_Code AS Allpcode,Truck_Id AS AllTid,Route_Id FROM TRUCK_RouteAllotment WHERE Company_Code='" + companycode + "' AND Plant_Code='" + plantcode + "') AS tal ON vd.Truck_Id=tal.AllTid ) AS t1 " +
" INNER JOIN " +
" (SELECT Truck_Id AS Td,Distance FROM TRUCK_ROUTEDISTANCE WHERE Company_Code='" + companycode + "' AND Plant_Code='" + plantcode + "') AS Tdis ON t1.Truck_Id=Tdis.Td ) AS F1 " +
" LEFT JOIN " +
"(SELECT Route_ID AS Rid,CONVERT(Nvarchar(15),Route_ID)+'_'+Route_Name AS Route_Name  FROM Route_Master WHERE Company_Code='" + companycode + "' AND Plant_Code='" + plantcode + "'  and status=1) AS R1 ON F1.Route_Id=R1.Rid ORDER BY F1.Route_Id ";

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            AgentwisePaymentDetails.DataSource = dt;
            AgentwisePaymentDetails.DataBind();
        }
        catch (Exception ex)
        {

        }

    }
    private void LoadTruckId()
    {
        try
        {
            //ds = null;
            //ds = BLtruck.LoadTruck(companycode, plantcode);
            con = dbaccess.GetConnection();
         
            string Sqlstr = null;
            Sqlstr = "SELECT Truck_id,Convert(Nvarchar(5),Truck_id)+'_'+ Vehicle_No AS Truck_Name FROM Vehicle_Details WHERE Company_Code=" + companycode + " AND Plant_Code=" + plantcode + " and status=1   ORDER BY Truck_id";
            SqlCommand cmd = new SqlCommand(Sqlstr,con);
            SqlDataAdapter dst=new SqlDataAdapter(cmd);
            DataTable drt = new DataTable();
            drt.Rows.Clear();
            dst.Fill(drt);
            if (drt.Rows.Count > 0)
            {
                ddl_VehicleName.DataSource = drt;
                ddl_VehicleName.DataTextField = "Truck_Name";
                ddl_VehicleName.DataValueField = "Truck_id";//ROUTE_ID 
                ddl_VehicleName.DataBind();

            }
            else 
            {
                ddl_VehicleName.Items.Add("--Select Truck--");
            }

        }
        catch (Exception ex)
        {
        }             

    }
    public void LoadTruck()
    {
       
       
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        plantcode = Convert.ToInt32(ddl_Plantname.SelectedItem.Value);
        MChk_RouteName.Checked = true;
        LoadRouteName1();
        LoadRouteName();
        Mroute_RouteName();
        LoadTruckId();
        LoadTruckGrid();

    }


}
