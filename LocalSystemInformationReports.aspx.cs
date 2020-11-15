using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class LocalSystemInformationReports : System.Web.UI.Page
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
    DataTable dtk = new DataTable();

    int I=0 ;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    dtm = System.DateTime.Now;
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");



                    dtm = System.DateTime.Now;
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                 

                    LoadPlantcode();
                    DateTime txtMyDate = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                    //DateTime dttt = DateTime.Parse(txt_FromDate.Text);

                    //DateTime cutime = DateTime.Parse(gettime);
                    //string sff = dttt.AddDays(1).ToString();

                    //GETDATE = txt_FromDate.Text;

                    DateTime date = txtMyDate.AddDays(-1);

                    string datee = date.ToString("MM/dd/yyyy");
                    // DateTime DDD = DateTime.ParseExact(date);
                    // Button2.Visible = false;
                }
                else
                {



                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();
                pcode = ddl_Plantcode.SelectedItem.Value;

            }

        }
        catch
        {


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


    public void getgrid()
    {

        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
      

        string d1 = dt1.ToString("MM/dd/yyyy");
      
        string str = "";
        con = DB.GetConnection();
        str = "SELECT  PP.Plant_code AS PlantCode,PP.Plant_Name as PlantName,PPT.OFTime as OffTime,PPT.WETime as weiTime,PPT.LABTime as LabTime  FROM (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 170 and Plant_Code!=160 ) AS PP LEFT JOIN (select  dd.Plant_code,cc.OFTime,CC.WeigherSysName,CC.WETime,dd.LabSysName,dd.LABTime from(SELECT * from (SELECT t1.Plant_code,OfficeName, OFTime =REPLACE( (SELECT  '' + RIGHT(Officesystemtime,7)   FROM SystemTime t2  WHERE t2.plant_code = t1.plant_code  and t2.OfficeName='OFFICE'  and Date='" + d1 + "'    group by  Plant_code,OfficeName,Officesystemtime   FOR XML PATH('')  ), ' ', ' ')   FROM SystemTime t1  WHERE  t1.OfficeName='OFFICE'   and Date='" + d1 + "'  GROUP BY Plant_code,OfficeName ) as aa   LEFT  JOIN ( SELECT t1.Plant_code as wPlant_code ,WeigherSysName, WETime =REPLACE( (SELECT  '' + RIGHT(Weighersystemtime,7)    FROM SystemTime t2   WHERE t2.plant_code = t1.plant_code   and t2.WeigherSysName='WEIGHER'  and Date='" + d1 + "'   group by    Plant_code,WeigherSysName,Weighersystemtime   FOR XML PATH('') ), ' ', ' ') FROM SystemTime t1  WHERE  t1.WeigherSysName='WEIGHER'   and Date='" + d1 + "'   GROUP BY Plant_code,WeigherSysName ) as bb on aa.Plant_code=bb.wPlant_code)  as cc LEFT  JOIN    (SELECT t1.Plant_code,LabSysName, LABTime =REPLACE( (SELECT  '' + RIGHT(Labsystemtime,7)    FROM SystemTime t2        WHERE t2.plant_code = t1.plant_code   and t2.LabSysName='LAB'  and Date='" + d1 + "'           group by  Plant_code,LabSysName,Labsystemtime              FOR XML PATH('')  ), ' ', ' ')     FROM SystemTime t1  WHERE    t1.LabSysName='LAB'      and Date='" + d1 + "'    GROUP BY Plant_code,LabSysName) as dd on cc.Plant_code=dd.Plant_code) AS PPT ON PP.Plant_Code=PPT.Plant_code  order by PP.Milktype,PP.Plant_Code,PPT.OFTime";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

       
       


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string   value =e.Row.Cells[1].Text;

           
            e.Row.Cells[2].Width = Unit.Pixel(150);

            e.Row.Cells[3].Width = Unit.Pixel(325);
            e.Row.Cells[4].Width = Unit.Pixel(325);
            e.Row.Cells[5].Width = Unit.Pixel(325);
            if ((value == "155") || (value == "156") || (value == "158") || (value == "159") || (value == "161") || (value == "162") || (value == "163") || (value == "164"))
            {
                e.Row.Cells[1].BackColor = System.Drawing.Color.Bisque;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.DarkBlue;
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[0].BackColor = System.Drawing.Color.Bisque;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.DarkBlue;
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[2].BackColor = System.Drawing.Color.Bisque;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.DarkBlue;
                e.Row.Cells[2].Font.Bold = true;


                e.Row.Cells[3].BackColor = System.Drawing.Color.Bisque;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.DarkBlue;
                e.Row.Cells[3].Font.Bold = true;


                e.Row.Cells[4].BackColor = System.Drawing.Color.Bisque;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.DarkBlue;
                e.Row.Cells[4].Font.Bold = true;


                e.Row.Cells[5].BackColor = System.Drawing.Color.Bisque;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.DarkBlue;
                e.Row.Cells[5].Font.Bold = true;

            }
            else
            {


                e.Row.Cells[1].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Brown;
                e.Row.Cells[1].Font.Bold = true;

                e.Row.Cells[0].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Brown;
                e.Row.Cells[0].Font.Bold = true;

                e.Row.Cells[2].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Brown;
                e.Row.Cells[2].Font.Bold = true;


                e.Row.Cells[3].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Brown;
                e.Row.Cells[3].Font.Bold = true;


                e.Row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Brown;
                e.Row.Cells[4].Font.Bold = true;


                e.Row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Brown;
                e.Row.Cells[5].Font.Bold = true;

            }
            
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header = new TableCell();
            Cell_Header.Text = "SYSTEM TIMIMG INFORMATION:" + txt_FromDate.Text;
            Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header.BackColor = System.Drawing.Color.Bisque;
            Cell_Header.ForeColor = System.Drawing.Color.Black;
            Cell_Header.ColumnSpan = 6;
            Cell_Header.Font.Bold = true;
            HeaderRow.Cells.Add(Cell_Header);

            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        try
        {
            getgrid();
        }
        catch
        {


        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}