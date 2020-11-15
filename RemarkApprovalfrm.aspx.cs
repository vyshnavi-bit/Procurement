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


public partial class RemarkApprovalfrm : System.Web.UI.Page
{

    string Company_code;
    string plant_Code;
    BLLProcureimport proimpBLL = new BLLProcureimport();
    BOLProcurement proBO = new BOLProcurement();
    BLLuser Bllusers = new BLLuser();
    DataTable dt = new DataTable();
    DataRow dr;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
         //   Company_code = Session["Company_code"].ToString();

            Company_code = "1";


             LoadPlantcode();
             Label6.Visible = false;
             Label7.Visible = false;


        }

        Label6.Visible = false;
        Label7.Visible = false;

    }




    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {

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
                    ddl_plantcode.Items.Add(dr["Plant_Code"].ToString());
                 //   ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }

        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
        //    e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
        //}

        //if (e.Row.RowType == DataControlRowType.DataRow &&
        //     (e.Row.RowState == DataControlRowState.Normal ||
        //      e.Row.RowState == DataControlRowState.Alternate))
        //{

            //if (e.Row.Cells[7].FindControl("Approval") == null)
            //{
            //    CheckBox selectCheckbox = new CheckBox();
            //    //Give id to check box whatever you like to
            //    selectCheckbox.ID = "Approval";
            //    e.Row.Cells[7].Controls.Add(selectCheckbox);

            //}


        






    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].Cells[7].FindControl("Approval");
            if (chk.Checked)
            {
                TextBox2.Text = GridView1.Rows[i].Cells[0].Text;
                TextBox3.Text = GridView1.Rows[i].Cells[1].Text;
                TextBox4.Text = GridView1.Rows[i].Cells[2].Text;
                TextBox5.Text=GridView1.Rows[i].Cells[3].Text;
                TextBox6.Text = GridView1.Rows[i].Cells[4].Text;
              //  string approve = "update table set status ='Approved' where ID=" + GridView1.Rows[i].Cells[1].Text + "";

                //      //  SqlCommand scmapprove = new SqlCommand(approve, con);
                //     //   scmapprove.ExecuteNonQuery();


                System.Web.HttpContext.Current.Response.Write(" check box selected ");
               
            }

        }



        //for (int i = 1; i < GridView1.Rows.Count; i++)
        //{
        //    if (((CheckBox)GridView1.Rows[i].FindControl("newSelectCheckbox")).Checked)
        //    {
        //        //I thought if the loop finds a checked check box, it will execute the following to that row:
        //      //  con.Open();
        //        string approve = "update table set status ='Approved' where ID=" + GridView1.Rows[i].Cells[1].Text + "";

        //      //  SqlCommand scmapprove = new SqlCommand(approve, con);
        //     //   scmapprove.ExecuteNonQuery();

        //        //view(); //Donot rebind the gridview now.
        //       // con.Close();
        //    }
        //}



        

    }



    protected void ddl_plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {

            Label7.Text = "";
            // int totalRowsCount = 0;
            GridView1.DataBind();
            int totalRowsCount = GridView1.Rows.Count;

            Label7.Text = totalRowsCount.ToString();
            Label6.Visible = true;
            Label7.Visible = true;

            // GridView1.DataBind();
        }
    }
}