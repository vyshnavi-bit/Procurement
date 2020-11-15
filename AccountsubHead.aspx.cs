using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class AccountsubHead : System.Web.UI.Page
{
    DbHelper db = new DbHelper();
    SqlConnection con = new SqlConnection();
    string planno;
    DateTime dtm = new DateTime();
    public static int roleid;
    public string ccode;
    public string pcode;
    string msg;
   // string nn ;
    int CHECKVAL;
    string headname;
    string subheadname;
    BLLuser Bllusers = new BLLuser();
    string d1;

    string getplantid;
    double AMOUNT;
    double EDITAMOUNT;
    string EMP;
    string tid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                dtm = System.DateTime.Now;
                //nn = Session["Name"].ToString();
                LoadPlantcode();
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                panel1.Visible = true;

                Panel22.Visible = false;

            }

        }
        else
        {


            pcode = ddl_Plantcode.SelectedItem.Value;
            getgridview();

        }
    }



    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadPlantcode(ccode.ToString());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddl_Plantcode.Items.Add(dr["Plant_Code"].ToString());
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                

                }
            }
            else
            {
                ddl_Plantcode.Items.Add("--Select PlantName--");
                ddl_Plantname.Items.Add("--Select Plantcode--");
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        
        DateTime dtr = new DateTime();
        
      
        


        dtr = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);

        d1 = dtr.ToString("MM/dd/yyyy");


        getgroupnameid();
        gettotamount();
        getgridview();
        clear();
    }

    public void clear()
    {
        txt_itemAmount.Text = "";
        txt_description.Text = "";

    }

    public void getgridview()
    {

        try
        {
            string str = "";
            con = db.GetConnection();
            //DateTime dt1 = new DateTime();
            //DateTime dt2 = new DateTime();
            //string d1 = dt1.ToString("MM/dd/yyyy");
            //string d2 = dt2.ToString("MM/dd/yyyy");
            str = "Select *   from AccountsEntry  WHERE PLANT_CODE='" + pcode + "'  ORDER BY  TID DESC ";
            SqlCommand cmd = new SqlCommand(str, con);
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
                GridView1.DataSource = "No Records";
                GridView1.DataBind();
            }

        }
        catch
        {



        }

    }





    public void getSUBHEADNAME()
    {

        string str = "";
        con = db.GetConnection();
        dtpsubhead.Items.Clear();
        //DateTime dt1 = new DateTime();
        //DateTime dt2 = new DateTime();
        //string d1 = dt1.ToString("MM/dd/yyyy");
        //string d2 = dt2.ToString("MM/dd/yyyy");
        str = "Select *   from expencesheader";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                dtpsubhead.Items.Add(dr["SubheaderName"].ToString());

            

            }
        }
        else
        {


        }




    }


    public void gettotamount()
    {



        try
        {
            string str = "";
            con = db.GetConnection();

            str = "insert into AccountsEntry(Headername,headerId,Subheadname,Plant_code,Plant_name,Amount,description,Date,InsertedBy)values('" + Session["accheadname"].ToString() + "','" + Session["accHeaderValue"].ToString() + "','" + dtpsubhead.SelectedItem.Text + "','" + ddl_Plantcode.SelectedItem.Value + "','" + ddl_Plantname.Text + "','" + txt_itemAmount.Text + "','" + txt_description.Text + "','" + d1 + "','" + Session["Name"].ToString() + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            string script = "window.onload = function(){ alert('";
            script += "Save successfully";
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            msg = ee.Message.ToString();
            string script = "window.onload = function(){ alert('";
            script += msg;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

        }




    }



    public void getgroupnameid()
    {

        string str = "";
        con = db.GetConnection();
        //DateTime dt1 = new DateTime();
        //DateTime dt2 = new DateTime();
        //string d1 = dt1.ToString("MM/dd/yyyy");
        //string d2 = dt2.ToString("MM/dd/yyyy");

        str = "Select  Headername,HeaderValue   from expencesheader  WHERE SubheaderName='" + dtpsubhead.Text + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {

            while (dr.Read())
            {
                Session["accheadname"] = dr["Headername"].ToString();
                Session["accHeaderValue"] = dr["HeaderValue"].ToString();

                string SEE = Session["accheadname"].ToString();
                string SEE1 = Session["accHeaderValue"].ToString();
            }

        }



    }
    protected void dtpsubhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        getgroupnameid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        getgridview();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        getgridview();
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        pcode = ddl_Plantcode.SelectedItem.Value;
        getSUBHEADNAME();
        getgridview();


    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

      




    }
    protected void update_Click(object sender, EventArgs e)
    {

        LoadPlantcode();
        getSUBHEADNAME();





    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {


        panel1.Visible = false;

        Panel22.Visible = true;

        
        
         tid = GridView1.SelectedRow.Cells[1].Text;
         EDITAMOUNT = Convert.ToDouble(GridView1.SelectedRow.Cells[7].Text);

         txt_itemAmountupdate.Text = EDITAMOUNT.ToString();


        

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {

            tid = GridView1.SelectedRow.Cells[1].Text;
           // EDITAMOUNT = Convert.ToDouble(GridView1.SelectedRow.Cells[7].Text);
            string str = "";
            con = db.GetConnection();

            str = "update   AccountsEntry set Plant_code='" + pcode + "',Plant_name='" + ddl_Plantname.Text + "',Amount='" + txt_itemAmountupdate.Text + "',InsertedBy='" + Session["Name"].ToString() + "' WHERE tid='" + tid + "'  ";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            getgridview();
            string script = "window.onload = function(){ alert('";
            script += "Updated successfully";
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            //cmd.ExecuteNonQuery();
            panel1.Visible = true;

            Panel22.Visible = false;

           

        }
        catch (Exception ee)
        {
            msg = ee.Message.ToString();
            string script = "window.onload = function(){ alert('";
            script += msg;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

        }
    }
    protected void txt_itemAmountupdate_TextChanged(object sender, EventArgs e)
    {

    }
}