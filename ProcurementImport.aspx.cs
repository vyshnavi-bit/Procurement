using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class ProcurementImport : System.Web.UI.Page
{
    string Company_code;
    string plant_Code;
    BLLProcureimport proimpBLL = new BLLProcureimport();
    BOLProcurement proBO = new BOLProcurement();
    BLLuser Bllusers = new BLLuser();
    DataTable dt = new DataTable();
    DataRow dr;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        uscMsgBox1.MsgBoxAnswered += MessageAnswered;
        if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());

                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                LoadPlantcode();

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

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }

    }

    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered ", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }
    private void LoadPlantcode()
    {

        try
        {
            SqlDataReader dr = null;
            dr = Bllusers.LoadPlantcode(Company_code);
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
        LoadPlantcode();
    }
    protected void ddl_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Plantname.SelectedIndex = ddl_Plantcode.SelectedIndex;
    }

    protected void rbam_CheckedChanged(object sender, EventArgs e)
    {
        rbpm.Checked = false;

    }
    protected void rbpm_CheckedChanged(object sender, EventArgs e)
    {

        rbam.Checked = false;

    }

    private void loadgriddata()
    {

        int dtcount;
        string sess = string.Empty;
        if (rbam.Checked == true)
        {
            sess = "AM";
            rbpm.Checked = false;
        }
        else
        {
            sess = "PM";
            rbam.Checked = false;
        }
        dt = proimpBLL.LoadRatechartGriddata(ddl_Plantcode.Text, Company_code, sess, txt_ToDate.Text);
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
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        loadgriddata();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        loadgriddata();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        loadgriddata();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = GridView1.EditIndex;
        GridViewRow row = GridView1.Rows[index];
        loadgriddata();
        int count;
        count = dt.Rows.Count;
        dr = dt.Rows[index];

        proBO.Tid = Convert.ToInt32(dr["Tid"]);
        proBO.milk_Ltr = Convert.ToDouble(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim());
        proBO.Milk_Kg = Math.Round(Convert.ToDouble(((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim()) * 1.03, 2);
        double fat = Convert.ToDouble(((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim());
        proBO.fat = fat;
        double snf = Convert.ToDouble(((TextBox)row.Cells[4].Controls[0]).Text.ToString().Trim());
        proBO.snf = snf;
        proBO.Clr = ((snf - 0.36) - (fat * 0.2)) * 4;
        proimpBLL.proimport(proBO);
        GridView1.EditIndex = -1;
        loadgriddata();
    }
}


