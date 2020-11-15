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
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
public partial class AgentsProcurementList : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    BLLRateChart BLLrate = new BLLRateChart();
    BOLPlantwiseRatechart Bolprate = new BOLPlantwiseRatechart();
    BLLPlantwiseRatechart Blrate = new BLLPlantwiseRatechart();
    public string Company_code;
    public string plant_Code;
    public string cname;
    string d1;
    string d2;
    DateTime dt = new DateTime();
    DateTime dt1 = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DBclass = new DbHelper();
    string str;
    int getval;
    BLLroutmaster routmasterBL = new BLLroutmaster();
    int rid;
    BLLuser Bllusers = new BLLuser();
    public static int roleid;

    double GETMILKKG=0;
    double GETMILKMLTR = 0;
    double fatkg=0;
     double snfkg=0;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
               // LoadPlantName();
                LoadPlantcode();
                dt = System.DateTime.Now;
                dt1 = System.DateTime.Now;
                txt_FromDate.Text = dt.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dt1.ToString("dd/MM/yyyy");
                lblmsg1.Visible = false;
                lbl_Sess.Visible = false;
                ddl_Sessions.Visible = false;
                lbl_route.Visible = false;
                ddl_RouteName.Visible = false;
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
               plant_Code = ddl_Plantcode.SelectedItem.Value;
              
                cname = Session["cname"].ToString();
                lblmsg1.Visible = false;
                try
                {
                    if (Route.Checked == true)
                    {
                        lbl_route.Visible = true;
                        ddl_RouteName.Visible = true;
                        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
                    }
                    else
                    {
                        lbl_route.Visible = false;
                        ddl_RouteName.Visible = false;
                    }
                    getval = Convert.ToInt16(rdocheck.SelectedItem.Value.Trim());




                }
                catch
                {

                }
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            getgriddata();
           // Route.Checked = false;
        }
        catch (Exception Ex)
        {

            lblmsg1.Text = Ex.Message;
            lblmsg1.Visible = true;
            lblmsg1.ForeColor = System.Drawing.Color.Red;

        }
    }

   


    public void getgriddata()
    {

        try
        {

          

            d1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            d2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

          

           

            if (getval == 1)
            {
                if (Route.Checked == false)
                {
                    //str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "' and  Sessions='" + ddl_Sessions.SelectedItem.Text + "' group by Sessions,Agent_id) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' ) as agent on procu.Agent_id=agent.Agent_Id  order by procu.Agent_id";

                    str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "' and  Sessions='" + ddl_Sessions.SelectedItem.Text + "' group by Sessions,Agent_id) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' ) as agent on procu.Agent_id=agent.Agent_Id  order by procu.Agent_id";
                    lbldisplay.Text = " '" + ddl_Plantname.SelectedItem.Text + "'  Procurement List Date='" + txt_FromDate.Text + "'   " + " session='" + ddl_Sessions.SelectedItem.Text + "'";
                }
                else
                {
                    //str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "' and  Sessions='" + ddl_Sessions.SelectedItem.Text + "' and route_id='" + rid + "' group by Sessions,Agent_id) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' and route_id='" + rid + "' ) as agent on procu.Agent_id=agent.Agent_Id  order by procu.Agent_id";
                  //  str = "Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "' and  Sessions='" + ddl_Sessions.SelectedItem.Text + "' and route_id='" + rid + "' group by Sessions,Agent_id) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' and route_id='" + rid + "' ) as agent on procu.Agent_id=agent.Agent_Id  order by procu.Agent_id";
                    str = "   select   Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg ,SampleId  from Procurement  WHERE plant_code='" + plant_Code + "' and Prdate='" + d1 + "' and  Sessions='" + ddl_Sessions.SelectedItem.Text + "' and route_id='" + rid + "' group by Sessions,Agent_id,SampleId) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' and route_id='" + rid + "' ) as agent on procu.Agent_id=agent.Agent_Id  order by   procu.SampleId ASC";

                    lbldisplay.Text = " '" + ddl_Plantname.SelectedItem.Text + "'  Procurement List Date='" + txt_FromDate.Text + "'   " + " session='" + ddl_Sessions.SelectedItem.Text + "' " +" RouteName='" +ddl_RouteName.Text +"'";

                }

               
            }
            if (getval == 2)
            {
             //   str = "select  Agent_id as AgentId ,convert(varchar,prdate,103) as Date,Sessions as Shift,SUM(Milk_kg-DIFFKG)as MilkKg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modifysnf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "'    and Prdate between '" + date11 + "' and '" + date22 + "' and (DIFFKG < 0 or DIFFFAT < 0 or DIFFSNF < 0  ) and  (Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by rand(Agent_id) ";
                // str = "select  Agent_id as AgentId ,convert(varchar,prdate,103) as Date,Sessions as Shift,SUM(Milk_kg-DIFFKG)as MilkKg,Milk_kg as Modifykg,sum(Fat - DIFFFAT) as Fat,Fat as ModifyFat,sum(Snf - DIFFSNF ) as snf,snf as Modifysnf,ROUND(DIFFKG,2) As  DIFFKG, ROUND(DIFFFAT,2) AS DIFFFAT,ROUND(DIFFSNF,2) AS DIFFSNF    from procurementimport    where   plant_code='" + pcode + "'    and Prdate between '" + date11 + "' and '" + date22 + "' and (DIFFFAT > 0 or DIFFSNF > 0 or DIFFFAT < 0 or DIFFSNF < 0 ) and  ( Remarkstatus=2)  group by agent_id,Prdate,Sessions,Milk_kg,modify_Kg,Fat,modify_fat,Snf,modify_snf,DIFFKG,DIFFFAT,DIFFSNF,Remarkstatus  order by Agent_id ";
                if (Route.Checked == false)
                {
                    //str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "'  group by Agent_id,Prdate) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";

                    str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "'  group by Agent_id,Prdate) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";
                    lbldisplay.Text = " '" + ddl_Plantname.SelectedItem.Text + "'  Procurement List Date='" + txt_FromDate.Text + "'";
                }
                else
                {
                   // str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "'  and route_id='" + rid + "' group by Agent_id,Prdate) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' and route_id='" + rid + "' ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";

                    str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate='" + d1 + "'  and route_id='" + rid + "' group by Agent_id,Prdate) as procu  left join  ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' and route_id='" + rid + "' ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";
                    lbldisplay.Text = " '" + ddl_Plantname.SelectedItem.Text + "'  Procurement List Date='" + txt_FromDate.Text + "' "+"  RouteName='" +ddl_RouteName.Text +"'";
                }

            }
            if (getval == 3)
            {
                if (Route.Checked == false)
                {
                    //str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate  between '" + d1 + "'  and '" + d2 + "'  group by Agent_id) as procu  left join   ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "'  ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";

                    str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate  between '" + d1 + "'  and '" + d2 + "'  group by Agent_id) as procu  left join   ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "'  ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";
                    lbldisplay.Text = " '" + ddl_Plantname.SelectedItem.Text + "'  Procurement List Date='" + txt_FromDate.Text + "'   " + "ToDate='" + txt_ToDate.Text + "'";
                }
                else
                {
                   // str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate  between '" + d1 + "'  and '" + d2 + "' and route_id='" + rid + "' group by Agent_id) as procu  left join   ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' and  route_id='" + rid + "' ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";
                    str = "select ROW_NUMBER() OVER (ORDER BY procu.Agent_id) AS [Sno],Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),procu.Agent_id)+'-'+Convert(nvarchar(50),agent.Agent_Name) as AgentName,procu.Cans,procu.Mkg,procu.Mltr,cast(procu.Fat as decimal(10,2)) as Fat,cast(procu.Snf as decimal(10,2)) as snf,procu.Fatkg,procu.SnfKg from(Select Agent_id,isnull(sum(NoofCans),0) as Cans,isnull(sum(Milk_kg),0) as Mkg,isnull(sum(Milk_ltr),0) as Mltr,isnull(Avg(fat),0) as Fat ,isnull(avg(snf),0) as Snf,isnull(Sum(fat_kg),0) as Fatkg,isnull(Sum(snf_kg),0) as SnfKg   from Procurement  where plant_code='" + plant_Code + "' and Prdate  between '" + d1 + "'  and '" + d2 + "' and route_id='" + rid + "' group by Agent_id) as procu  left join   ( Select *   from Agent_Master  where  plant_code='" + plant_Code + "' and  route_id='" + rid + "' ) as agent on procu.Agent_id=agent.Agent_Id   order by procu.Agent_id";
                    lbldisplay.Text = " '" + ddl_Plantname.SelectedItem.Text + "'  Procurement List Date='" + txt_FromDate.Text + "'   " + "ToDate='" + txt_ToDate.Text + "' " + " RouteName='" + ddl_RouteName.Text + "'";
                }
            }

            using (con = DBclass.GetConnection())
            {

                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                   GridView1.DataSource = dt;
                   GridView1.DataBind();
                   GridView1.HeaderStyle.Font.Bold = true;
                   GridView1.FooterRow.Cells[1].Text = "Total/Avg";
                   GridView1.FooterRow.Cells[1].Font.Bold = true;
                    int can = dt.AsEnumerable().Sum(row => row.Field<int>("Cans"));
                    GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    GridView1.FooterRow.Cells[3].Text = can.ToString();
                    GridView1.FooterRow.Cells[3].Font.Bold = true;
                    //decimal mkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("Mkg"));
                    //double milkkg = dt.AsEnumerable().Sum(row => row.Field<double>("Mkg"));
                    //GridView1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    //GridView1.FooterRow.Cells[3].Text = milkkg.ToString("N2");
                    //GridView1.FooterRow.Cells[3].Font.Bold = true;
                    //double milkltr = dt.AsEnumerable().Sum(row => row.Field<double>("Mltr"));
                    //GridView1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    //GridView1.FooterRow.Cells[4].Text = milkltr.ToString("N2");
                    //GridView1.FooterRow.Cells[4].Font.Bold = true;

                    //var fat = dt.AsEnumerable().Where(x => x["Fat"] != DBNull.Value).Average(x => x.Field<decimal>("Fat"));
                    //GridView1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    //GridView1.FooterRow.Cells[5].Text = fat.ToString("N2");
                    //GridView1.FooterRow.Cells[5].Font.Bold = true;
                    //var snf = dt.AsEnumerable().Where(x => x["snf"] != DBNull.Value).Average(x => x.Field<decimal>("snf"));
                    //GridView1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    //GridView1.FooterRow.Cells[6].Text = snf.ToString("N2");
                    //GridView1.FooterRow.Cells[6].Font.Bold = true;

                    //double fatkg = dt.AsEnumerable().Sum(row => row.Field<double>("Fatkg"));
                    //GridView1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    //GridView1.FooterRow.Cells[7].Text = fatkg.ToString("N2");
                    //GridView1.FooterRow.Cells[7].Font.Bold = true;
                    //double snfkg = dt.AsEnumerable().Sum(row => row.Field<double>("snfkg"));
                    //GridView1.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    //GridView1.FooterRow.Cells[8].Text = snfkg.ToString("N2");
                    //GridView1.FooterRow.Cells[8].Font.Bold = true;
                    GridView1.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                    GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                    GridView1.FooterStyle.ForeColor = System.Drawing.Color.White;
                    GridView1.FooterStyle.BackColor = System.Drawing.Color.Brown;
                  

                    
                }
                else
                {

                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }
            }
        }

        catch(Exception Ex)
        {

            lblmsg1.Text = Ex.Message;
            lblmsg1.Visible = true;
            lblmsg1.ForeColor = System.Drawing.Color.Red;

        }

    }
   
  
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.Buffer = true;
            string filename = "'" + ddl_Plantname.SelectedItem.Text + "' " + " " + DateTime.Now.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            // Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                getgriddata();

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
                string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
                // string style = @"<style> .textmode { } </style>";
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

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // int i = RadioButtonList1.SelectedIndex;
            
                 
            if (rdocheck.SelectedValue != null)
            {
                string value = rdocheck.SelectedItem.Value.Trim();

                if (value == "1")
                {

                    lbl_todate.Visible = false;
                    txt_ToDate.Visible = false;
                    lbl_frmdate.Visible = true;
                    txt_FromDate.Visible = true;
                    lbl_Sess.Visible = true;
                    ddl_Sessions.Visible = true;
                    getval = 1;

                }


                if (value == "2")
                {

                    lbl_todate.Visible = false;
                    txt_ToDate.Visible = false;
                    lbl_frmdate.Visible = true;
                    txt_FromDate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;
                    getval = 2;

                }

                if (value == "3")
                {

                    lbl_todate.Visible = true;
                    txt_ToDate.Visible = true;
                    lbl_frmdate.Visible = true;
                    txt_FromDate.Visible = true;
                    lbl_Sess.Visible = false;
                    ddl_Sessions.Visible = false;
                    getval = 3;

                }
                                
            }
        }
        catch (Exception Ex)
        {
            lblmsg1.Text = Ex.Message;
            lblmsg1.Visible = true;
            lblmsg1.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void Route_CheckedChanged(object sender, EventArgs e)
    {

        if (Route.Checked == true)
        {
            ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
            plant_Code = ddl_Plantcode.SelectedItem.Value;
            loadrouteid();
            lbl_route.Visible = true;
            ddl_RouteName.Visible = true;
        }
       
    }
    protected void ddl_routename_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        ddl_RouteID.SelectedIndex = ddl_RouteName.SelectedIndex;
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);

    }

    private void loadrouteid()
    {
        try
        {
            SqlDataReader dr;

            dr = routmasterBL.getroutmasterdatareader(Company_code, plant_Code);

            ddl_RouteName.Items.Clear();
            ddl_RouteID.Items.Clear();

            while (dr.Read())
            {
                ddl_RouteName.Items.Add(dr["Route_ID"].ToString().Trim() + "_" + dr["Route_Name"].ToString().Trim());
                ddl_RouteID.Items.Add(dr["Route_ID"].ToString().Trim());

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
        plant_Code = ddl_Plantcode.SelectedItem.Value;
        loadrouteid();
        rid = Convert.ToInt32(ddl_RouteID.SelectedItem.Value);
    }

    private void LoadPlantcode()
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantcode.Items.Clear();
            ddl_Plantname.Items.Clear();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           

            double LOCALKG = Convert.ToDouble(e.Row.Cells[4].Text );
            GETMILKKG =  GETMILKKG + LOCALKG;
            double LTR = Convert.ToDouble(e.Row.Cells[4].Text);
            GETMILKMLTR = GETMILKKG + LTR;
            double localfatkg=  Convert.ToDouble(e.Row.Cells[8].Text );
            fatkg = fatkg + localfatkg;
            double localfsnfkg=  Convert.ToDouble(e.Row.Cells[9].Text );
            snfkg = snfkg + localfsnfkg;




        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {

            e.Row.Cells[4].Text = GETMILKKG.ToString("F2");

            e.Row.Cells[5].Text =( GETMILKMLTR  / 1.03) .ToString("F2");
            e.Row.Cells[6].Text = ((fatkg * 100) / GETMILKKG).ToString("F2");
            e.Row.Cells[7].Text = ((snfkg * 100) / GETMILKKG).ToString("F2");
            e.Row.Cells[8].Text = fatkg.ToString("F2");
            e.Row.Cells[9].Text = snfkg.ToString("F2");


        }
    }
}