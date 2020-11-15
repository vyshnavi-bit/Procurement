using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class LiveWeigher : System.Web.UI.Page
{
    string date;
    string sess;
    double totalMonthlySalaries = 0;
    DbHelper dbaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
   string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {

            getdetails();

            lblmsg.Visible = false;


        }
        else
        {
            lblmsg.Visible = false;
        }
    }



    public void getdetails()
    {

        try
        {
            date = System.DateTime.Now.ToShortDateString();
            Label3.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            sess = System.DateTime.Now.ToString("tt");
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //  string str = "Select plantcode,convert(varchar,prdate,103) as prdate,sessions,CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,count(*) as sampleno,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='"+date+"' and  sessions='am' group by  plantcode,prdate,sessions ";

            //string str = "select * from (Select  ROW_NUMBER() OVER (ORDER BY plantcode) AS [sno],plantcode,convert(varchar,prdate,103) as prdate,sessions,CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,count(*) as sampleno,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='" + date + "' and  sessions='" + sess + "' group by  plantcode,prdate,sessions) as a left join (select plant_code,plant_Name  from Plant_Master) as b on a.plantcode=b.Plant_Code";
            string str = "SELECT sno,t1.PlantCode,prdate,Sessions,milkkg,sampleno,fat,snf,Plant_Name,Stime,Etime FROM " +
" (select * from (Select  ROW_NUMBER() OVER (ORDER BY plantcode) AS [sno],plantcode,convert(varchar,prdate,103) as prdate,sessions,CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,count(*) as sampleno,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where   prdate='" + date + "' and  sessions='" + sess + "' group by  plantcode,prdate,sessions) as a left join (select plant_code,plant_Name  from Plant_Master) as b on a.plantcode=b.Plant_Code ) AS t1 " +
"  LEFT JOIN  " +
" (SELECT MIN(StartingTime) AS Stime,MAX(EndingTime) AS Etime,PlantCode  FROM Lp Where   prdate='" + date + "' and  sessions='" + sess + "' GROUP BY PlantCode) AS t2 ON t1.PlantCode=t2.PlantCode ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            datalstProfileCount.DataSource = ds.Tables[0].DefaultView;
            datalstProfileCount.DataBind();
        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    public void GrandTotal()
    {

        try
        {
            date = System.DateTime.Now.ToShortDateString();
            Label3.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            sess = System.DateTime.Now.ToString("tt");
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            //  string str = "Select plantcode,convert(varchar,prdate,103) as prdate,sessions,CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,count(*) as sampleno,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='"+date+"' and  sessions='am' group by  plantcode,prdate,sessions ";

            string str = "Select  CONVERT(DECIMAL(10,2),sum(milkkg)) as milkkg,CONVERT(DECIMAL(10,2),AVG(fat)) as fat, CONVERT(DECIMAL(10,2),AVG(snf)) as snf  from lp  where prdate='" + date + "' and  sessions='" + sess + "' group by  Prdate";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataReader dr = null;
            dr = dbaccess.GetDatareader(str);
            if (dr.HasRows)
            {
                dr.Read();
                //totalMilk.Text = "";
                //Afat.Text = "";
                //Asnf.Text = "";
                totalMilk.Text = dr["milkkg"].ToString().Trim();
                totalMilk.Attributes.Add("style", "font-size:50px; color:Green; font-weight:bold;");
                Afat.Text = dr["fat"].ToString().Trim();
                Afat.Attributes.Add("style", "font-size:50px; color:Red; font-weight:bold;");
                Asnf.Text = dr["snf"].ToString().Trim();
                Asnf.Attributes.Add("style", "font-size:50px; color:Green; font-weight:bold;");

            }
            
        }
        catch (Exception Ex)
        {
            //lblmsg.Text = Ex.Message;
            //lblmsg.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }






protected void  Timer1_Tick(object sender, EventArgs e)
{
    getdetails();
    GrandTotal();
    //Label2.Text = System.DateTime.Now.ToString();
   // Label3.Text = System.DateTime.Now.ToLongDateString();
    Label6.Text = System.DateTime.Now.ToLongTimeString();
    Label6.Attributes.Add("style", "font-size:50px; color:Red; font-weight:bold;");
    Label3.Attributes.Add("style", "font-size:50px; color:Red; font-weight:bold;");
   // Label4.Attributes.Add("style", "font-size:50px; color:white; font-weight:bold;");
  
    
}
protected void datalstProfileCount_SelectedIndexChanged(object sender, EventArgs e)
{

}
protected void datalstProfileCount_ItemDataBound(object sender, DataListItemEventArgs e)
{

    
    //   double sum=0;
    // double agree;
       
    //    Label CustomerID = (Label)e.Item.FindControl("lbl_id");
    //  sum= Convert.ToDouble(CustomerID.Text);

    //agree = sum + sum;

    //    //add your dblTotal to the global variable on each row its used
    //    totalMonthlySalaries += dblTotal;
   
     



  
}
protected void datalstProfileCount_ItemDataBound1(object sender, DataListItemEventArgs e)
{

}
}