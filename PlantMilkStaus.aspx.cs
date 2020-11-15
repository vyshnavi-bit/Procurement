using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class PlantMilkStaus : System.Web.UI.Page
{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime frmdat = new DateTime();
    DateTime todat = new DateTime();
    
    SqlConnection con = new SqlConnection();
    DbHelper dbaccess = new DbHelper();
    public static int roleid;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack == true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

               
                frmdat = System.DateTime.Now;
                txt_FromDate.Text = frmdat.ToString("dd/MM/yyyy");
                txt_ToDate.Text = frmdat.ToString("dd/MM/yyyy");
                 Rdl_Plantype.SelectedIndex = 0;
                Get_PlantMilkStatus(ccode, Rdl_Plantype.SelectedItem.Value);
                Lbl_Errormsg.Visible = false;
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
                Lbl_Errormsg.Visible = false;
            }
             else
             {
                 Server.Transfer("LoginDefault.aspx");
             }

        }
    }
   

   

   private void Get_PlantMilkStatus(string ccode,string PlantMilkType)
    {
        try
    {
        decimal total = 0, Fat = 0, Snf = 0;
        int Plantype = Convert.ToInt32(PlantMilkType);           
        string d1, d2;
        string str = string.Empty;
       
        frmdat = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        todat = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        d1 = frmdat.ToString("MM/dd/yyyy");
        d2 = todat.ToString("MM/dd/yyyy");
        DataTable dt = new DataTable();
        if (Plantype == 3)
        {
             str = "SELECT pcode+'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(Plant_code) AS pcode,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY Plant_code ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype IN (1,2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
        }
        else if (Plantype == 2)
        {
            str = "SELECT pcode+'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(Plant_code) AS pcode,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY Plant_code ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (2) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
        }
        else
        {
            str = "SELECT pcode+'_'+Plant_Name AS Plant,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM (SELECT DISTINCT(Plant_code) AS pcode,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "'  GROUP BY Plant_code ) AS t1 INNER JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype  IN (1) ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code ";
        }
            using (con=dbaccess.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                da.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //Calculate Sum and display in Footer Row
                    total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                    Fat = dt.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                    Snf = dt.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                    gv_PlantMilkDetail.FooterRow.Cells[1].Text = "Total/Avg";
                    gv_PlantMilkDetail.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    gv_PlantMilkDetail.FooterRow.Cells[2].Text = total.ToString("N2");
                    gv_PlantMilkDetail.FooterRow.Cells[3].Text = Fat.ToString("f2");
                    gv_PlantMilkDetail.FooterRow.Cells[4].Text = Snf.ToString("f1");

                }

                else
                {
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = "NO DATA...";
                }

                if (dt.Rows.Count > 0)
                {
                    int rowcount = dt.Rows.Count;
                    DataRow rows = dt.NewRow();
                    rows["Plant"] = "Total/Avg";
                    rows["Milkkg"] = total;
                    rows["Fat"] = Fat.ToString("f2");
                    rows["Snf"] = Snf.ToString("f2");

                    dt.Rows.Add(rows); 
                    
                    gv_PlantMilkDetail.DataSource = dt;
                    gv_PlantMilkDetail.DataBind();

                    //// color
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[1].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[2].ForeColor = Color.White;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].BackColor = Color.DarkSlateGray;
                    //gv_PlantMilkDetail.Rows[rowcount + 1].Cells[3].ForeColor = Color.White;
                }
            }
            
    }
    catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }
   protected void gv_PlantMilkDetail_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
   {
       string index = string.Empty;
       try
       {
           index = gv_PlantMilkDetail.Rows[e.NewSelectedIndex].Cells[1].Text;
           lbl_Plantcode.Text = index;
           string[] strarr = new string[2];
           strarr = index.ToString().Split('_');
           index = strarr[0];
           Get_RouteMilkStatus(ccode,index.ToString());
           if (index == "Total/Avg")
           {
               this.ModalPopupExtender1.Hide();
           }
           else
           {
               this.ModalPopupExtender1.Show();
           }
       }
       catch (Exception ex)
       {
           Lbl_Errormsg.Visible = true;
           Lbl_Errormsg.Text = ex.ToString();
       }
   }
   private void Get_RouteMilkStatus(string ccode,string pcode)
   {
       try
       {
           string d1, d2;
           decimal total = 0, Fat = 0, Snf = 0;
           frmdat = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
           todat = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
           d1 = frmdat.ToString("MM/dd/yyyy");
           d2 = todat.ToString("MM/dd/yyyy");
           DataTable dt = new DataTable();
           string str = "SELECT RouteName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
" (SELECT Route_id,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'  GROUP BY Plant_code,Route_id ) AS t1 " +
" INNER JOIN " +
" (SELECT Route_ID AS Rid,CONVERT(nvarchar(10),Route_ID)+'_'+Route_Name AS RouteName FROM Route_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Route_id=t2.Rid ORDER BY RAND(Route_id)";
           using (con = dbaccess.GetConnection())
           {
               SqlDataAdapter da = new SqlDataAdapter(str, con);
               da.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   gv_PlantRouteMilkDetail.DataSource = dt;
                   gv_PlantRouteMilkDetail.DataBind();

                   //Calculate Sum and display in Footer Row
                   total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                   Fat = dt.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                   Snf = dt.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                   gv_PlantRouteMilkDetail.FooterRow.Cells[1].Text = "Total/Avg";
                   gv_PlantRouteMilkDetail.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                   gv_PlantRouteMilkDetail.FooterRow.Cells[2].Text = total.ToString("N2");
                   gv_PlantRouteMilkDetail.FooterRow.Cells[3].Text = Fat.ToString("f2");
                   gv_PlantRouteMilkDetail.FooterRow.Cells[4].Text = Snf.ToString("f2");
               }
               else
               {
                   gv_PlantMilkDetail.DataSource = dt;
                   gv_PlantMilkDetail.DataBind();
                   Lbl_Errormsg.Visible = true;
                   Lbl_Errormsg.Text = "NO DATA...";
               }
               if (dt.Rows.Count > 0)
               {
                   DataRow rows = dt.NewRow(); 
                   rows["RouteName"] = "Total/Avg";
                   rows["Milkkg"] = total;
                   rows["Fat"] = Fat.ToString("f2");
                   rows["Snf"] = Snf.ToString("f2"); 

                   dt.Rows.Add(rows);                 

                   gv_PlantRouteMilkDetail.DataSource = dt;
                   gv_PlantRouteMilkDetail.DataBind();
               }
           }

       }
       catch (Exception ex)
       {
           Lbl_Errormsg.Visible = true;
           Lbl_Errormsg.Text = "NO DATA...";
       }
   }

   private void Get_AgentMilkStatus(string ccode, string pcode,string Rid)
   {
       try
       {
           string d1, d2;
           decimal total = 0, Fat = 0, Snf = 0;
           frmdat = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
           todat = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
           d1 = frmdat.ToString("MM/dd/yyyy");
           d2 = todat.ToString("MM/dd/yyyy");
           DataTable dt = new DataTable();
           string str = "SELECT AgentName,Smkg AS Milkkg,Afat AS Fat,ASnf AS Snf FROM " +
 " (SELECT Agent_id,ISNULL(CAST(SUM(Milk_kg) As DECIMAL(18,2)),0)  AS Smkg,ISNULL(CAST(AVG(Fat) As DECIMAL(18,1)),0) AS Afat,ISNULL(CAST(AVG(Snf)  As DECIMAL(18,1)),0) AS ASnf FROM PROCUREMENT WHERE Company_code='" + ccode + "'  AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'  GROUP BY Plant_code,Agent_id ) AS t1 " +
 " INNER JOIN " +
 " (SELECT Agent_Id AS Aid,CONVERT(nvarchar(10),Agent_Id)+'_'+Agent_Name AS AgentName FROM Agent_Master Where Company_Code='" + ccode + "' AND  Plant_Code='" + pcode + "') AS t2 ON t1.Agent_id=t2.Aid ORDER BY RAND(Agent_id)";
           using (con = dbaccess.GetConnection())
           {
               SqlDataAdapter da = new SqlDataAdapter(str, con);
               da.Fill(dt);
               if (dt.Rows.Count > 0)
               {
                   gv_PlantAgentMilkDetail.DataSource = dt;
                   gv_PlantAgentMilkDetail.DataBind();

                   //Calculate Sum and display in Footer Row
                   total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Milkkg"));
                   Fat = dt.AsEnumerable().Average(row => row.Field<decimal>("Fat"));
                   Snf = dt.AsEnumerable().Average(row => row.Field<decimal>("Snf"));
                   gv_PlantAgentMilkDetail.FooterRow.Cells[0].Text = "Total/Avg";
                   gv_PlantAgentMilkDetail.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                   gv_PlantAgentMilkDetail.FooterRow.Cells[1].Text = total.ToString("N2");
                   gv_PlantAgentMilkDetail.FooterRow.Cells[2].Text = Fat.ToString("f2");
                   gv_PlantAgentMilkDetail.FooterRow.Cells[3].Text = Snf.ToString("f2");
               }
               else
               {
                   gv_PlantAgentMilkDetail.DataSource = dt;
                   gv_PlantAgentMilkDetail.DataBind();
                   Lbl_Errormsg.Visible = true;
                   Lbl_Errormsg.Text = "NO DATA...";
               }
               if (dt.Rows.Count > 0)
               {
                   DataRow rows = dt.NewRow();
                   rows["AgentName"] = "Total/Avg";
                   rows["Milkkg"] = total;
                   rows["Fat"] = Fat.ToString("f2");
                   rows["Snf"] = Snf.ToString("f2");
                   dt.Rows.Add(rows);
                   gv_PlantAgentMilkDetail.DataSource = dt;
                   gv_PlantAgentMilkDetail.DataBind();
               }
           }

       }
       catch (Exception ex)
       {
           Lbl_Errormsg.Visible = true;
           Lbl_Errormsg.Text = "NO DATA...";
       }
   }

   protected void btn_Mclose_Click(object sender, EventArgs e)
   {
       this.ModalPopupExtender1.Hide();
   }
  
   protected void Rdl_Plantype_SelectedIndexChanged(object sender, EventArgs e)
   {
       try
       {

           Get_PlantMilkStatus(ccode, Rdl_Plantype.SelectedItem.Value);
       }
       catch (Exception ex)
       {
           Lbl_Errormsg.Visible = true;
           Lbl_Errormsg.Text = ex.ToString();
       }
   }

   protected void btn_Aclose_Click(object sender, EventArgs e)
   {
       this.ModalPopupExtender2.Hide();
       this.ModalPopupExtender1.Show();

   }
   protected void gv_PlantRouteMilkDetail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
   {
       string index = string.Empty;
    
       try
       {
           pcode = lbl_Plantcode.Text;
           string[] pcodearr = new string[2];
           pcodearr = pcode.ToString().Split('_');
           pcode = pcodearr[0];

           index = gv_PlantRouteMilkDetail.Rows[e.NewSelectedIndex].Cells[1].Text;
           Lbl_Routecode.Text = index;
           string[] strarr = new string[2];
           strarr = index.ToString().Split('_');
           index = strarr[0];
           Get_AgentMilkStatus(ccode, pcode, index.ToString());
           if (index == "Total/Avg")
           {
               this.ModalPopupExtender2.Hide();
           }
           else
           {
               this.ModalPopupExtender2.Show();
           }
       }
       catch (Exception ex)
       {
           Lbl_Errormsg.Visible = true;
           Lbl_Errormsg.Text = ex.ToString();
       }

   }
  
}