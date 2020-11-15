using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;
public partial class PlantRatechartUsagewithmilkkg : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlConnection con = new SqlConnection();
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    BLLuser Bllusers = new BLLuser();
    DateTime tdt = new DateTime();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    string strsql = string.Empty;

    public int refNo = 0;
    public static int roleid;
    double getassignval;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                tdt = System.DateTime.Now;
                txt_FromDate.Text = tdt.ToString("dd/MM/yyy");
                txt_ToDate.Text = tdt.ToString("dd/MM/yyy");
                if (roleid < 3)
                {
                    LoadSinglePlantName();
                }
                if ((roleid >= 7) || (roleid == 9))
                {
                    LoadPlantName();
                }
                if (roleid == 9)
                {
                    
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();
                }
                pcode = ddl_PlantName.SelectedItem.Value;
                Lbl_Errormsg.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                Label6.Visible = false;
                Label7.Visible = false;
                Label8.Visible = false;
                Image1.Visible = false;
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
                ccode = Session["Company_code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                pcode = ddl_PlantName.SelectedItem.Value;

                Lbl_Errormsg.Visible = false;
                Label1.Visible = false;
                Label4.Visible = false;
                Label6.Visible = false;
                Label7.Visible = false;
                Label8.Visible = false;
                Image1.Visible = false;

            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
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

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;

            ddl_PlantName.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void Get_UsedRatechartDetailsPlant()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            dt = null;
            int count = 0;

            DataTable dts = new DataTable();
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_TsRateDisplay]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", ddl_PlantName.SelectedItem.Value);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                sqlCmd.Parameters.AddWithValue("@spratechartmodeltype", 1);
                sqlCmd.Parameters.AddWithValue("@spcount", 0);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);

                //
                DataTable ksdt = new DataTable();
                DataColumn ksdc = null;
                DataRow ksdr = null;
                int counts = dts.Rows.Count;


                // START ADDING COLUMN
                if (counts > 0)
                {
                    Image1.Visible = true;
                    Label1.Visible = true;
                    Label7.Visible = true;
                    Label7.Text = "From :" + txt_FromDate.Text.Trim() + "  To :" + txt_ToDate.Text.Trim();
                    Label8.Visible = true;
                    Label8.Text = ddl_PlantName.SelectedItem.Text.Trim();
                    ksdc = new DataColumn("From");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("To");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Rate");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Comm");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Bonus");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Smilk_kg");
                    ksdt.Columns.Add(ksdc);
                }
                // END ADDING COLUMN

                // START ADDING ROWS
                if (counts > 0)
                {
                    object id2;
                    id2 = 0;
                    int idd2 = Convert.ToInt32(id2);

                    foreach (DataRow dr2 in dts.Rows)
                    {


                        object id1;
                        id1 = dr2[0].ToString().Trim();
                        int idd1 = Convert.ToInt32(id1);
                        if (idd1 == idd2)
                        {

                        }
                        else
                        {
                            int cc = 0;
                            foreach (DataRow dr3 in dts.Rows)
                            {
                                object id3;
                                id3 = dr3[0].ToString().Trim();
                                int idd3 = Convert.ToInt32(id3);
                                if (idd1 == idd3)
                                {
                                    if (cc == 0)
                                    {
                                        ksdr = ksdt.NewRow();
                                        ksdr[cc] = dr3[1].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[2].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[3].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[4].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[5].ToString();

                                        ksdt.Rows.Add(ksdr);

                                        ksdr = ksdt.NewRow();
                                        cc = 0;
                                        ksdr[cc] = dr3[6].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[7].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[8].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[9].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[10].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[11].ToString();
                                        cc++;
                                        ksdt.Rows.Add(ksdr);

                                    }
                                    else
                                    {
                                        ksdr = ksdt.NewRow();
                                        cc = 0;

                                        ksdr[cc] = dr3[6].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[7].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[8].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[9].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[10].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[11].ToString();
                                        cc++;
                                        ksdt.Rows.Add(ksdr);

                                    }
                                    idd2 = idd3;
                                }
                            }
                        }

                    }
                }
                // END ADDING ROWS

                GridView1.DataSource = ksdt;
                GridView1.DataBind();
                CallGridcolor(dts, 1);

            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Get_UsedRatechartDetailsRoutewise()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            dt = null;
            int count = 0;

            DataTable dts = new DataTable();
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_TsRateDisplay]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", ddl_PlantName.SelectedItem.Value);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                sqlCmd.Parameters.AddWithValue("@spratechartmodeltype", 2);
                sqlCmd.Parameters.AddWithValue("@spcount", 0);
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);

                //
                DataTable ksdt = new DataTable();
                DataColumn ksdc = null;
                DataRow ksdr = null;
                int counts = dts.Rows.Count;


                // START ADDING COLUMN
                if (counts > 0)
                {
                    Image1.Visible = true;
                    Label4.Visible = true;
                    Label7.Visible = true;
                    Label7.Text = "From :" + txt_FromDate.Text.Trim() + "  To :" + txt_ToDate.Text.Trim();
                    Label8.Visible = true;
                    Label8.Text = ddl_PlantName.SelectedItem.Text.Trim();
                    ksdc = new DataColumn("From");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("To");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Rate");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Comm");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Bonus");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Smilk_kg");
                    ksdt.Columns.Add(ksdc);
                }
                // END ADDING COLUMN

                // START ADDING ROWS
                if (counts > 0)
                {
                    object id2;
                    id2 = 0;
                    int idd2 = Convert.ToInt32(id2);

                    foreach (DataRow dr2 in dts.Rows)
                    {


                        object id1;
                        id1 = dr2[0].ToString().Trim();
                        int idd1 = Convert.ToInt32(id1);
                        if (idd1 == idd2)
                        {

                        }
                        else
                        {
                            int cc = 0;
                            foreach (DataRow dr3 in dts.Rows)
                            {
                                object id3;
                                id3 = dr3[0].ToString().Trim();
                                int idd3 = Convert.ToInt32(id3);
                                if (idd1 == idd3)
                                {
                                    if (cc == 0)
                                    {
                                        ksdr = ksdt.NewRow();
                                        ksdr[cc] = dr3[1].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[2].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[3].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[4].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[5].ToString();
                                        ksdt.Rows.Add(ksdr);

                                        ksdr = ksdt.NewRow();
                                        cc = 0;
                                        ksdr[cc] = dr3[6].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[7].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[8].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[9].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[10].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[11].ToString();
                                        cc++;
                                        ksdt.Rows.Add(ksdr);

                                    }
                                    else
                                    {
                                        ksdr = ksdt.NewRow();
                                        cc = 0;

                                        ksdr[cc] = dr3[6].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[7].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[8].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[9].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[10].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[11].ToString();
                                        ksdt.Rows.Add(ksdr);

                                    }
                                    idd2 = idd3;
                                }
                            }
                        }

                    }
                }
                // END ADDING ROWS

                GridView2.DataSource = ksdt;
                GridView2.DataBind();
                CallGridcolor(dts, 2);

            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }
    private void Get_UsedRatechartDetailsAgentwise()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            dt = null;
            int count = 0;

            DataTable dts = new DataTable();
            String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            SqlConnection conn = null;
            using (conn = new SqlConnection(dbConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand("dbo.[Get_TsRateDisplay]");
                conn.Open();
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@spccode", ccode);
                sqlCmd.Parameters.AddWithValue("@sppcode", ddl_PlantName.SelectedItem.Value);
                sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
                sqlCmd.Parameters.AddWithValue("@spratechartmodeltype", 3);
                sqlCmd.Parameters.AddWithValue("@spcount", 0);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dts);

                //
                DataTable ksdt = new DataTable();
                DataColumn ksdc = null;
                DataRow ksdr = null;
                int counts = dts.Rows.Count;


                // START ADDING COLUMN
                if (counts > 0)
                {
                    Image1.Visible = true;
                    Label6.Visible = true;
                    Label7.Visible = true;
                    Label7.Text = "From :" + txt_FromDate.Text.Trim() + "  To :" + txt_ToDate.Text.Trim();
                    Label8.Visible = true;
                    Label8.Text = ddl_PlantName.SelectedItem.Text.Trim();
                    ksdc = new DataColumn("From");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("To");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Rate");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Comm");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Bonus");
                    ksdt.Columns.Add(ksdc);
                    ksdc = new DataColumn("Smilk_kg");
                    ksdt.Columns.Add(ksdc);


                }
                // END ADDING COLUMN

                // START ADDING ROWS
                if (counts > 0)
                {
                    object id2;
                    id2 = 0;
                    int idd2 = Convert.ToInt32(id2);

                    foreach (DataRow dr2 in dts.Rows)
                    {


                        object id1;
                        id1 = dr2[0].ToString().Trim();
                        int idd1 = Convert.ToInt32(id1);
                        if (idd1 == idd2)
                        {

                        }
                        else
                        {
                            int cc = 0;

                            foreach (DataRow dr3 in dts.Rows)
                            {
                                object id3;
                                id3 = dr3[0].ToString().Trim();
                                int idd3 = Convert.ToInt32(id3);
                                if (idd1 == idd3)
                                {
                                    if (cc == 0)
                                    {
                                        ksdr = ksdt.NewRow();
                                        ksdr[cc] = dr3[1].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[2].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[3].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[4].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[5].ToString();
                                        ksdt.Rows.Add(ksdr);


                                        ksdr = ksdt.NewRow();
                                        cc = 0;
                                        ksdr[cc] = dr3[6].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[7].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[8].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[9].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[10].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[11].ToString();

                                        ksdt.Rows.Add(ksdr);

                                    }
                                    else
                                    {
                                        ksdr = ksdt.NewRow();
                                        cc = 0;

                                        ksdr[cc] = dr3[6].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[7].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[8].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[9].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[10].ToString();
                                        cc++;
                                        ksdr[cc] = dr3[11].ToString();
                                        ksdt.Rows.Add(ksdr);

                                    }
                                    idd2 = idd3;
                                }
                            }
                        }

                    }
                }
                // END ADDING ROWS

                GridView3.DataSource = ksdt;
                GridView3.DataBind();
                CallGridcolor(dts, 3);
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void CallGridcolor(DataTable dts, int ratechartmodel)
    {
        try
        {
            //
            DataTable ksdt = new DataTable();
            DataColumn ksdc = null;
            DataRow ksdr = null;
            int counts = dts.Rows.Count;


            // START ADDING COLUMN
            if (counts > 0)
            {

                ksdc = new DataColumn("From");
                ksdt.Columns.Add(ksdc);
                ksdc = new DataColumn("To");
                ksdt.Columns.Add(ksdc);
                ksdc = new DataColumn("Rate");
                ksdt.Columns.Add(ksdc);
                ksdc = new DataColumn("Comm");
                ksdt.Columns.Add(ksdc);
                ksdc = new DataColumn("Bonus");
                ksdt.Columns.Add(ksdc);
                ksdc = new DataColumn("milk_kg");
                ksdt.Columns.Add(ksdc);
            }
            // END ADDING COLUMN

            // START ADDING ROWS
            if (counts > 0)
            {
                object id2;
                id2 = 0;
                int idd2 = Convert.ToInt32(id2);

                foreach (DataRow dr2 in dts.Rows)
                {


                    object id1;
                    id1 = dr2[0].ToString().Trim();
                    int idd1 = Convert.ToInt32(id1);
                    if (idd1 == idd2)
                    {

                    }
                    else
                    {
                        int cc = 0;
                        int rowcount = 0;
                        foreach (DataRow dr3 in dts.Rows)
                        {
                            object id3;
                            id3 = dr3[0].ToString().Trim();
                            int idd3 = Convert.ToInt32(id3);
                            if (idd1 == idd3)
                            {
                                if (cc == 0)
                                {
                                    ksdr = ksdt.NewRow();
                                    ksdr[cc] = dr3[1].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[2].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[3].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[4].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[5].ToString();
                                    ksdt.Rows.Add(ksdr);
                                    rowcount = ksdt.Rows.Count;
                                    Gridcolor(rowcount - 1, ratechartmodel);

                                    ksdr = ksdt.NewRow();
                                    cc = 0;
                                    ksdr[cc] = dr3[6].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[7].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[8].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[9].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[10].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[11].ToString();

                                    ksdt.Rows.Add(ksdr);

                                }
                                else
                                {
                                    ksdr = ksdt.NewRow();
                                    cc = 0;

                                    ksdr[cc] = dr3[6].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[7].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[8].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[9].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[10].ToString();
                                    cc++;
                                    ksdr[cc] = dr3[11].ToString();
                                    ksdt.Rows.Add(ksdr);

                                }
                                idd2 = idd3;
                            }
                        }
                    }

                }
            }
            // END ADDING ROWS
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void Gridcolor(int rowcount, int ratechartmodel)
    {
        try
        {
            if (ratechartmodel == 1)
            {
                GridView1.Rows[rowcount].Cells[0].BackColor = Color.DarkSlateGray;
                GridView1.Rows[rowcount].Cells[0].ForeColor = Color.White;
                GridView1.Rows[rowcount].Cells[1].BackColor = Color.DarkSlateGray;
                GridView1.Rows[rowcount].Cells[1].ForeColor = Color.White;
                GridView1.Rows[rowcount].Cells[2].BackColor = Color.DarkSlateGray;
                GridView1.Rows[rowcount].Cells[2].ForeColor = Color.White;
                GridView1.Rows[rowcount].Cells[3].BackColor = Color.DarkSlateGray;
                GridView1.Rows[rowcount].Cells[3].ForeColor = Color.White;
                GridView1.Rows[rowcount].Cells[4].BackColor = Color.DarkSlateGray;
                GridView1.Rows[rowcount].Cells[4].ForeColor = Color.White;
                GridView1.Rows[rowcount].Cells[5].BackColor = Color.DarkSlateGray;
                GridView1.Rows[rowcount].Cells[5].ForeColor = Color.White;

            }
            if (ratechartmodel == 2)
            {
                GridView2.Rows[rowcount].Cells[0].BackColor = Color.DarkSlateGray;
                GridView2.Rows[rowcount].Cells[0].ForeColor = Color.White;
                GridView2.Rows[rowcount].Cells[1].BackColor = Color.DarkSlateGray;
                GridView2.Rows[rowcount].Cells[1].ForeColor = Color.White;
                GridView2.Rows[rowcount].Cells[2].BackColor = Color.DarkSlateGray;
                GridView2.Rows[rowcount].Cells[2].ForeColor = Color.White;
                GridView2.Rows[rowcount].Cells[3].BackColor = Color.DarkSlateGray;
                GridView2.Rows[rowcount].Cells[3].ForeColor = Color.White;
                GridView2.Rows[rowcount].Cells[4].BackColor = Color.DarkSlateGray;
                GridView2.Rows[rowcount].Cells[4].ForeColor = Color.White;
            }
            if (ratechartmodel == 3)
            {
                GridView3.Rows[rowcount].Cells[0].BackColor = Color.DarkSlateGray;
                GridView3.Rows[rowcount].Cells[0].ForeColor = Color.White;
                GridView3.Rows[rowcount].Cells[1].BackColor = Color.DarkSlateGray;
                GridView3.Rows[rowcount].Cells[1].ForeColor = Color.White;
                GridView3.Rows[rowcount].Cells[2].BackColor = Color.DarkSlateGray;
                GridView3.Rows[rowcount].Cells[2].ForeColor = Color.White;
                GridView3.Rows[rowcount].Cells[3].BackColor = Color.DarkSlateGray;
                GridView3.Rows[rowcount].Cells[3].ForeColor = Color.White;
                GridView3.Rows[rowcount].Cells[4].BackColor = Color.DarkSlateGray;
                GridView3.Rows[rowcount].Cells[4].ForeColor = Color.White;
            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }



    protected void btn_ok_Click(object sender, EventArgs e)
    {
        try
        {
            Get_UsedRatechartDetailsPlant();
            Get_UsedRatechartDetailsRoutewise();
            Get_UsedRatechartDetailsAgentwise();
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }


    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            try
            {


                double getvalue = Convert.ToDouble(e.Row.Cells[5].Text);


               

                if (getvalue == getassignval)
                {

                    e.Row.Cells[5].Text = "0.00";
                }

                getassignval = getvalue;
            }
            catch
            {


            }
        }
    }
}