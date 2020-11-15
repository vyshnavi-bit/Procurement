using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class GarbarTest : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    BLLuser Bllusers = new BLLuser();
    DataTable dt = new DataTable();
    string msg;
    Exception ex;
    string plant ;
    string[] GET;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                dtm = System.DateTime.Now;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                roleid = Convert.ToInt32(Session["Role"].ToString());
                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");

                if (roleid < 3)
                {
                    loadsingleplant();

                }

                if ((roleid >= 3) && (roleid != 9))
                {

                    LoadPlantcode();
                }

                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                    loadspecialsingleplant();

                }

                DateTime txtMyDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                //DateTime dttt = DateTime.Parse(txt_FromDate.Text);

                //DateTime cutime = DateTime.Parse(gettime);
                //string sff = dttt.AddDays(1).ToString();

                //GETDATE = txt_FromDate.Text;

                DateTime date = txtMyDate.AddDays(-1);

                string datee = date.ToString("MM/dd/yyyy");
                // DateTime DDD = DateTime.ParseExact(date);
                Button2.Visible = false;
            }
            else
            {



            }

        }


        else
        {
            ccode = Session["Company_code"].ToString();
            //  LoadPlantcode();

             plant = ddl_Plantname.Text;
             GET = plant.Split('_');
            pcode = ddl_Plantcode.SelectedItem.Value;

        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            getgridviewrows();
        }
        catch(Exception ex)
        {
          Response.Write("<script language='javascript'>alert('" +   Server.HtmlEncode(ex.Message) + "')</script>");
         //   Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", "<script>alert(' " + ex.Message.ToString() + " ');location.href='AddTender.aspx';</script>");
        }
    }
    public void getgridviewrows()
    {
        try
        {
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");
            string get = "";
            con = DB.GetConnection();
            get = "select Date,Session,GarbarFat ,GarbarSnf,GarbarClr,EKOCuttingFat AS EKOFat ,EKOCuttingSnf AS  EKOSnf,EKOCuttingClr as EKOClr,GainFat,GainSnf,GainClr,CONVERT(varchar(7), Time ,100) AS Time,CONVERT(varchar(7), SendingTime ,100) AS LabStartingTime    from (select plant_code,Date,Session,isnull(Sum(GarbarFat),0) as GarbarFat ,isnull(Sum(GarbarSnf),0) as GarbarSnf,isnull(Sum(GarbarClr),0) as GarbarClr,isnull(Sum(EKOCuttingFat),0) as EKOCuttingFat ,isnull(Sum(EKOCuttingSnf),0) as EKOCuttingSnf,isnull(sum(EKOCuttingClr),0) as EKOCuttingClr,isnull(SUM(GarbarFat-EKOCuttingFat),0) as GainFat,SUM(GarbarSnf-EKOCuttingSnf) as GainSnf,isnull(SUM(GarbarClr-EKOCuttingClr),0) as GainClr,Time,SendingTime   from (Select plant_code,CONVERT(varchar,Date,103) as Date,Session,GarbarFat,GarbarSnf,GarbarClr,EKOCuttingFat,EKOCuttingSnf,EKOCuttingClr,CONVERT(varchar,Time,100) as Time,CONVERT(varchar,SendingTime,100) as SendingTime from   Garbartest   where plant_code='" + GET[0] + "' and Date between '" + d1 + "' and '" + d2 + "' ) as gar   group by Date,plant_code,Session,Time,SendingTime) as garrg left join (select Plant_Code as pcode,Plant_Name  from Plant_Master   where Plant_Code='" + GET[0] + "' group by Plant_Code,Plant_Name) as pm on garrg.Plant_code=pm.pcode order by  Date,Session";
            SqlCommand cmd = new SqlCommand(get, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
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
        catch (Exception ex)
        {
            //  getcatmsg();
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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

  
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
        //pcode = ddl_Plantcode.SelectedItem.Value;
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = ddl_Plantname.Text + ":Garbar Details:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
            HeaderCell2.ColumnSpan = 14;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string DATE = e.Row.Cells[1].Text;
                string SESSS = e.Row.Cells[2].Text;

                string[] GETDATETIME = DATE.Split('/');

                string ASSIGNDATE = GETDATETIME[1] + "/" + GETDATETIME[0] + "/" + GETDATETIME[2];

                // DateTime.ParseExact(SESSS, "dd/MM/YYYY", CultureInfo.InvariantCulture)

                string GETTING = "";
                con = DB.GetConnection();
                GETTING = "select top 1  LabCaptureTime   from Lp where PlantCode='" + GET[0] + "' and Prdate='" + ASSIGNDATE + "' and Sessions='" + SESSS + "'  order by Tid asc";
                SqlCommand cmd = new SqlCommand(GETTING, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsa = new DataSet();
                da.Fill(dsa, ("CaptureTime"));

                e.Row.Cells[13].Text = dsa.Tables[0].Rows[0][0].ToString();
            }
        }
            catch
           {


            }
        

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
        catch (Exception EE)
        {
            string message;
            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }


    public void getcatmsg()
    {

        Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");


    }
}