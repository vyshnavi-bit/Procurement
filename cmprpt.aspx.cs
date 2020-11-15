using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class cmprpt : System.Web.UI.Page
{
    SqlCommand cmd;
    string Branchid = "";
    SalesDBManager vdm;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("Login.aspx");
        else
        {
            Branchid = Session["branch"].ToString();
            vdm = new SalesDBManager();
            if (!Page.IsPostBack)
            {
                if (!Page.IsCallback)
                {
                    dtp_FromDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    dtp_Todate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    lblAddress.Text = Session["Address"].ToString();
                    lblTitle.Text = Session["TitleName"].ToString();
                    bindbranches();
                }
            }
        }
    }
    private DateTime GetLowDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        DateTime.Now.ToString("hh:mm:ss tt");
        DT = dt;
        Hour = -dt.Hour;
        Min = -dt.Minute;
        Sec = -dt.Second;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }
    private DateTime GetHighDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        DateTime.Now.ToString("hh:mm:ss tt");
        Hour = 23 - dt.Hour;
        Min = 59 - dt.Minute;
        Sec = 59 - dt.Second;
        DT = dt;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }

    void bindbranches()
    {
        vdm = new SalesDBManager();
        if (Session["LevelType"].ToString() == "2" || Session["LevelType"].ToString() == "1")
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE (Plant_Code = @BranchID)");
            cmd.Parameters.Add("@BranchID", Session["branch"].ToString());
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlplantname.DataSource = dtPlant;
            ddlplantname.DataTextField = "Plant_Name";
            ddlplantname.DataValueField = "Plant_Code";
            ddlplantname.DataBind();
        }
        else
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master ");
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlplantname.DataSource = dtPlant;
            ddlplantname.DataTextField = "Plant_Name";
            ddlplantname.DataValueField = "Plant_Code";
            ddlplantname.DataBind();
        }
    }




    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            SalesDBManager SalesDB = new SalesDBManager();
            DateTime fromdate = DateTime.Now;
            DateTime todate = DateTime.Now;
            string[] datestrig = dtp_FromDate.Text.Split(' ');
            if (datestrig.Length > 1)
            {
                if (datestrig[0].Split('-').Length > 0)
                {
                    string[] dates = datestrig[0].Split('-');
                    string[] times = datestrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            datestrig = dtp_Todate.Text.Split(' ');
            if (datestrig.Length > 1)
            {
                if (datestrig[0].Split('-').Length > 0)
                {
                    string[] dates = datestrig[0].Split('-');
                    string[] times = datestrig[1].Split(':');
                    todate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            int plantcode = Convert.ToInt32(ddlplantname.SelectedItem.Value);
            lblFromDate.Text = fromdate.ToString("dd/MMM/yyyy");
            lbltodate.Text = todate.ToString("dd/MMM/yyyy");
            DateTime.Now.ToString("hh:mm:ss tt");
            hidepanel.Visible = true;
            Datatable dtdiff = new Datatable();
               
            cmd = new SqlCommand("SELECT Tid, Agent_id, Prdate, Sessions, Milk_kg, Fat, Snf, Plant_Code, modify_Kg, modify_fat, modify_snf, DIFFKG, DIFFFAT, DIFFSNF,  Remark FROM Procurementimport WHERE Plant_Code=@plantcode AND Prdate between @d1 and @d2");
           // cmd = new SqlCommand("SELECT date,session,starttime,ibttempstart,stoptime,ibttempstop FROM cmpruntime where Plant_code=@plantcode  and  date between @d1 and @d2");
            cmd.Parameters.Add("@plantcode", plantcode);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(todate));
            DataTable dtcompresssionrunninghours = SalesDB.SelectQuery(cmd).Tables[0];
            if (dtcompresssionrunninghours.Rows.Count > 0)
            {
                grdcompressor.DataSource = dtcompresssionrunninghours;
                grdcompressor.DataBind();
                hidepanel.Visible = true;
            }
            else
            {
                lblmsg.Text = "No dasta were found";
                hidepanel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            hidepanel.Visible = false;
        }
    }
    protected void grdcompressor_DataBinding(object sender, EventArgs e)
    {
        //try
        //{
        //    GridViewGroup First = new GridViewGroup(grdcompressor, null, "date");
        //    // GridViewGroup seconf = new GridViewGroup(grdReports, First, "Location");
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    protected void grdmilkrecheller_DataBinding(object sender, EventArgs e)
    {
        //try
        //{
        //    GridViewGroup First = new GridViewGroup(grdmilkrecheller, null, "date");
        //    // GridViewGroup seconf = new GridViewGroup(grdReports, First, "Location");
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
}