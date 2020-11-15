using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class TransportImportStatus : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    BLLPlantName BllPlant = new BLLPlantName();
    DateTime dtm = new DateTime();
    DateTime dtm1 = new DateTime();
    public string Company_code;
    public string plant_Code;
    public string cname;
    public string d1, d2;
    int Data;
    int status;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();

                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                LoadPlantName();
                Menu1();
                Lbl_Errormsg.Visible = false;
                Label1.Visible = false;
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
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                LoadTransportplantstatus();
                Lbl_Errormsg.Visible = false;
                Label1.Visible = true;
                Image1.Visible = true;

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
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            string d1 = dtm.ToString("MM/dd/yyyy");
            string d2 = dtm1.ToString("MM/dd/yyyy");
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(Company_code.ToString());
            if (ds != null)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "Plant_Name";
                CheckBoxList1.DataValueField = "plant_Code";//ROUTE_ID 
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

    private void Menu1()
    {
        if (MChk_PlantName.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
        }
    }

    private void CheckBoxListClear()
    {
        CheckBoxList1.Items.Clear();
    }

    protected void MChk_PlantName_CheckedChanged(object sender, EventArgs e)
    {
        Menu1();
    }


    private void Datechanged()
    {
        try
        {
            dtm = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dtm1 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            d1 = dtm.ToString("MM/dd/yyyy");
            d2 = dtm1.ToString("MM/dd/yyyy");

          
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }

    private void LoadTransportplantstatus()
    {
        try
        {
            Datechanged();
            dt = null;
            int count = 0;
            dt = BllPlant.DTLoadPlantNameChkLst(Company_code, d1, d2);
            count = dt.Rows.Count;
            if (count > 0)
            {
                DataTable custDT = new DataTable();
                DataColumn col = null;

                col = new DataColumn("plant_Code");
                custDT.Columns.Add(col);

                for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
                {
                    DataRow dr = null;
                    dr = custDT.NewRow();
                    if (CheckBoxList1.Items[i].Selected == true)
                    {
                        dr[0] = CheckBoxList1.Items[i].Value.ToString();
                        custDT.Rows.Add(dr);
                    }

                }
                DataTable dts = new DataTable();
                SqlParameter param = new SqlParameter();
                param.ParameterName = "CustDtPlantcode";
                param.SqlDbType = SqlDbType.Structured;
                param.Value = custDT;
                param.Direction = ParameterDirection.Input;
                String dbConnStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
                SqlConnection conn = null;
                using (conn = new SqlConnection(dbConnStr))
                {
                    SqlCommand sqlCmd = new SqlCommand("dbo.[Get_Transportplantstatus]");
                    conn.Open();
                    sqlCmd.Connection = conn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(param);
                    sqlCmd.Parameters.AddWithValue("@spccode", Company_code);
                    sqlCmd.Parameters.AddWithValue("@spfrmdate", d1.Trim());
                    sqlCmd.Parameters.AddWithValue("@sptodate", d2.Trim());
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
                        ksdc = new DataColumn("plant");
                        ksdt.Columns.Add(ksdc);
                        foreach (DataRow dr1 in dts.Rows)
                        {
                            object id;
                            id = dr1[1].ToString();
                            string columnName = "D-" + id;
                            DataColumnCollection columns = ksdt.Columns;
                            if (columns.Contains(columnName))
                            {

                            }
                            else
                            {
                                ksdc = new DataColumn("D-" + id);
                                ksdt.Columns.Add(ksdc);
                            }

                        }
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
                            ksdr = ksdt.NewRow();

                            object id1;
                            id1 = dr2[2].ToString();
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
                                    id3 = dr3[2].ToString();
                                    int idd3 = Convert.ToInt32(id3);
                                    if (idd1 == idd3)
                                    {
                                        if (cc == 0)
                                        {
                                            ksdr[cc] = dr3[0].ToString();
                                            cc++;
                                            ksdr[cc] = dr3[3].ToString();
                                            // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
                                            cc++;
                                        }
                                        else
                                        {
                                            ksdr[cc] = dr3[3].ToString();
                                            // ksdr[cc] = dr3[2].ToString() + "\n _" + dr3[3].ToString() + "\n _" + dr3[4].ToString();
                                            cc++;
                                        }
                                        idd2 = idd3;
                                    }
                                }
                                ksdt.Rows.Add(ksdr);
                            }

                        }
                    }
                    // END ADDING ROWS

                    Transport_ImportStatus.DataSource = ksdt;
                    Transport_ImportStatus.DataBind();
                    Label1.Visible = true;
                    Image1.Visible = true;

                }
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
        LoadTransportplantstatus();
    }



    


    protected void Transport_ImportStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Transport_ImportStatus.PageIndex = e.NewPageIndex;
        Transport_ImportStatus.DataBind();
    }
    protected void btn_lock_Click(object sender, EventArgs e)
    {

        //try
        //{
        //    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connStr);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("update AdminApproval set ClosingStatus='1' where Plant_code='" + plant_Code + "'", conn);
        //    cmd.ExecuteNonQuery();
        //}
        //catch (Exception ex)
        //{
        //    Lbl_Errormsg.Visible = true;
        //    Lbl_Errormsg.Text = ex.ToString();
        //}
    }

    //public void getadminapprovalstatus()
    //{
    //    try
    //    {
    //        string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    //        SqlConnection conn = new SqlConnection(connStr);
    //        conn.Open();
    //        string stt = "Select TransportStatus,status    from  AdminApproval   where Plant_code='" + plant_Code + "'  ";
    //        SqlCommand cmd = new SqlCommand(stt, conn);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.HasRows)
    //        {

    //            // btn_lock.Visible = false;

    //            while (dr.Read())
    //            {

    //                Data = Convert.ToInt32(dr["TransportStatus"]);
    //                status = Convert.ToInt32(dr["status"]);
    //            }
    //            if (Data == 1 && status == 1)
    //            {
    //                btn_Lock.Visible = true;
    //            }

    //            if (Data == 0 && status == 1)
    //            {
    //                btn_Lock.Visible = true;
    //            }
    //            if (Data == 1 && status == 2)
    //            {
    //                btn_Lock.Visible = false;
    //            }

    //            if (Data == 0 && status == 2)
    //            {
    //                btn_Lock.Visible = false;
    //            }


    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        Lbl_Errormsg.Visible = true;
    //        Lbl_Errormsg.Text = ex.ToString();
    //    }

    //}
}