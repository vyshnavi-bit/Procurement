using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
public partial class Deduction_Rpt : System.Web.UI.Page
{

    int ccode = 1, pcode;
    string sqlstr = string.Empty;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    SqlCommand cmd = new SqlCommand();
    DateTime dat = new DateTime();
    string d1 = string.Empty;
    string d2 = string.Empty;
    string d3 = string.Empty;
    string deductdate;
    public string managmobNo;
    public string pname;
    public string cname;
    int count, count1, count2;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                roleid = Convert.ToInt32(Session["Role"].ToString());
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(Session["Plant_Code"]);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
              //  managmobNo = Session["managmobNo"].ToString();
                dat = System.DateTime.Now;
                txt_FromDate.Text = dat.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dat.ToString("dd/MM/yyyy");
                if((roleid>=3) && (roleid!=9))
                {
                LoadPlantName();
                }
                if(roleid==9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = 170;

                }
                if ((roleid < 3))
                {
                    loadsingleplant();

                }

              //  pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                lblamount.Visible = false;
                Lbldeduct.Visible = false;
                ldl_deducdetails.Visible = false;
                Lbldeduct1.Visible = false;
                lblpname.Visible = false;
                Label13.Visible = false;
                Image1.Visible = false;
                Label12.Visible = false;
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }           
        }

        if (IsPostBack == true)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Convert.ToInt32(Session["Company_code"]);
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

              //  Label4.Text = " ";
                pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                Datefunc();

               // LoadPlantName();
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }       
    }   

  
    protected void btn_GetData_Click(object sender, EventArgs e)
    {
        try
        {

            lblpname.Visible = true;
            Label13.Visible = true;
            lblpname.Text = ddl_PlantName.SelectedItem.Text;
            lblpname.Visible = true;
            Image1.Visible = true;
            Label12.Visible = true;

            if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true) && (CheckBox3.Checked == true))
            {
                DeductionAllotList();
                DeductionPendingList();
                DeductionRecoveryList();
                DeductionAllotted.Visible = true;
                DeductionPending.Visible = true;
                DeductionRecovery.Visible = true;

                if (count > 0)
                {
                    lblamount.Visible = true;
                }
                else
                {
                    lblamount.Visible = false;
                }
                if (count1 > 0)
                {
                    Lbldeduct.Visible = true;
                }
                else
                {
                    Lbldeduct.Visible = false;
                }
                if (count2 > 0)
                {
                    ldl_deducdetails.Visible = true;
                }
                else
                {
                    ldl_deducdetails.Visible = false;

                }

            }






            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionRecoveryList1();
        }
        catch (Exception ex)
        {
          //  Label4.Text = ex.ToString();
        }
    }

  


    private void DeductionAllotList()
    {
        DataTable dt = new DataTable();
        con = new SqlConnection();

        try
        {

            using (con = dbaccess.GetConnection())
            {
                //            sqlstr = "SELECT ded.agent_id,(SUM(ded.billadvance)+SUM(DedR.billadvance)) AS billadvance,(SUM(ded.Ai)+SUM(dedR.Ai)) AS Ai,(SUM(ded.Feed)+SUM(DedR.Feed)) AS Feed,(SUM(ded.can)+SUM(DedR.can)) AS can,(SUM(ded.recovery)+SUM(DedR.recovery)) AS recovery,(SUM(ded.others)+SUM(DedR.others)) AS others   FROM " +
                //" (SELECT agent_id,route_id,CAST(billadvance AS DECIMAL(18,2)) AS billadvance,CAST(Ai AS DECIMAL(18,2)) AS Ai,CAST(Feed AS DECIMAL(18,2)) AS Feed,CAST(can AS DECIMAL(18,2)) AS can,CAST(recovery AS DECIMAL(18,2))  AS recovery,CAST(others AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,deductiondate,103) AS deductiondate FROM Deduction_Details Where Plant_Code='" + pcode + "' AND deductiondate Between '" + d1 + "' AND '" + d2 + "' ) AS ded " +
                //" LEFT JOIN " +
                //" (SELECT Ragent_id AS RAid,Rroute_id AS route_id,CAST(Rbilladvance AS DECIMAL(18,2)) AS billadvance,CAST(RAi AS DECIMAL(18,2)) AS Ai,CAST(RFeed AS DECIMAL(18,2)) AS Feed,CAST(Rcan AS DECIMAL(18,2)) AS can,CAST(Rrecovery AS DECIMAL(18,2))  AS recovery,CAST(Rothers AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,RDeduction_RecoveryDate,103) AS Rcoverydate FROM Deduction_Recovery Where RPlant_Code='" + pcode + "' AND Rdeductiondate='" + d3 + "' ) AS DedR ON ded.agent_id=DedR.RAid GROUP BY ded.agent_id Order By ded.agent_id ";

                // avoid zero anand




                //            sqlstr = "    SELECT ded.agent_id,(SUM(ded.billadvance)+SUM(DedR.billadvance)) AS billadvance,(SUM(ded.Ai)+SUM(dedR.Ai)) AS Ai,(SUM(ded.Feed)+SUM(DedR.Feed)) AS Feed,(SUM(ded.can)+SUM(DedR.can)) AS can,(SUM(ded.recovery)+SUM(DedR.recovery)) AS recovery,(SUM(ded.others)+SUM(DedR.others)) AS others   FROM " +
                //" (SELECT agent_id,route_id,CAST(billadvance AS DECIMAL(18,2)) AS billadvance,CAST(Ai AS DECIMAL(18,2)) AS Ai,CAST(Feed AS DECIMAL(18,2)) AS Feed,CAST(can AS DECIMAL(18,2)) AS can,CAST(recovery AS DECIMAL(18,2))  AS recovery,CAST(others AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,deductiondate,103) AS deductiondate FROM Deduction_Details Where Plant_Code='" + pcode + "' AND deductiondate Between '" + d1 + "' AND '" + d2 + "' ) AS ded " +
                //" LEFT JOIN " +
                //" (SELECT Ragent_id AS RAid,Rroute_id AS route_id,CAST(Rbilladvance AS DECIMAL(18,2)) AS billadvance,CAST(RAi AS DECIMAL(18,2)) AS Ai,CAST(RFeed AS DECIMAL(18,2)) AS Feed,CAST(Rcan AS DECIMAL(18,2)) AS can,CAST(Rrecovery AS DECIMAL(18,2))  AS recovery,CAST(Rothers AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,RDeduction_RecoveryDate,103) AS Rcoverydate FROM Deduction_Recovery Where RPlant_Code='" + pcode + "' AND Rdeductiondate='" + d3 + "' ) AS DedR ON ded.agent_id=DedR.RAid GROUP BY ded.agent_id Order By ded.agent_id ";



                sqlstr = "    SELECT ded.agent_id,(SUM(ISNULL(ded.billadvance,0))+SUM(ISNULL(DedR.billadvance,0))) AS billadvance,(SUM(ISNULL(ded.Ai,0))+SUM(ISNULL(dedR.Ai,0))) AS Ai,(SUM(ISNULL(ded.Feed,0))+SUM(ISNULL(DedR.Feed,0))) AS Feed,(SUM(ISNULL(ded.can,0))+SUM(ISNULL(DedR.can,0))) AS can,(SUM(ISNULL(ded.recovery,0))+SUM(ISNULL(DedR.recovery,0))) AS recovery,(SUM(ISNULL(ded.others,0))+SUM(ISNULL(DedR.others,0))) AS others     FROM " +
    " (SELECT agent_id,route_id,CAST(billadvance AS DECIMAL(18,2)) AS billadvance,CAST(Ai AS DECIMAL(18,2)) AS Ai,CAST(Feed AS DECIMAL(18,2)) AS Feed,CAST(can AS DECIMAL(18,2)) AS can,CAST(recovery AS DECIMAL(18,2))  AS recovery,CAST(others AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,deductiondate,103) AS deductiondate FROM Deduction_Details Where Plant_Code='" + pcode + "' AND deductiondate Between '" + d1 + "' AND '" + d2 + "' ) AS ded " +
    " LEFT JOIN " +
    " (SELECT Ragent_id AS RAid,Rroute_id AS route_id,CAST(Rbilladvance AS DECIMAL(18,2)) AS billadvance,CAST(RAi AS DECIMAL(18,2)) AS Ai,CAST(RFeed AS DECIMAL(18,2)) AS Feed,CAST(Rcan AS DECIMAL(18,2)) AS can,CAST(Rrecovery AS DECIMAL(18,2))  AS recovery,CAST(Rothers AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,RDeduction_RecoveryDate,103) AS Rcoverydate FROM Deduction_Recovery Where RPlant_Code='" + pcode + "' AND Rdeductiondate='" + d3 + "' ) AS DedR ON ded.agent_id=DedR.RAid GROUP BY ded.agent_id Order By ded.agent_id ";


                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                da.Fill(dt);
                count = dt.Rows.Count;
                DeductionAllotted.DataSource = dt;
                DeductionAllotted.DataBind();
                DeductionAllotted.FooterRow.Cells[0].Text = "Total";
                decimal billadv = dt.AsEnumerable().Sum(row => row.Field<decimal>("billadvance"));
                DeductionAllotted.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                DeductionAllotted.FooterRow.Cells[1].Text = billadv.ToString("N2");

                //DeductionAllotted.FooterRow.Cells[1].BackColor = System.Drawing.Color.Brown;
                //DeductionAllotted.FooterRow.Cells[1].ForeColor = System.Drawing.Color.White;
                //DeductionAllotted.FooterRow.Cells[1].Font.Bold = true;


                decimal ai = dt.AsEnumerable().Sum(row => row.Field<decimal>("Ai"));
                DeductionAllotted.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                DeductionAllotted.FooterRow.Cells[2].Text = ai.ToString("N2");
                //DeductionAllotted.FooterRow.Cells[2].BackColor = System.Drawing.Color.Brown;
                //DeductionAllotted.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
                //DeductionAllotted.FooterRow.Cells[2].Font.Bold = true;

                decimal Feed = dt.AsEnumerable().Sum(row => row.Field<decimal>("Feed"));
                DeductionAllotted.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                DeductionAllotted.FooterRow.Cells[3].Text = Feed.ToString("N2");
                //DeductionAllotted.FooterRow.Cells[3].BackColor = System.Drawing.Color.Brown;
                //DeductionAllotted.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;
                //DeductionAllotted.FooterRow.Cells[3].Font.Bold = true;

                decimal can = dt.AsEnumerable().Sum(row => row.Field<decimal>("can"));
                DeductionAllotted.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                DeductionAllotted.FooterRow.Cells[4].Text = can.ToString("N2");
                //DeductionAllotted.FooterRow.Cells[4].BackColor = System.Drawing.Color.Brown;
                //DeductionAllotted.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
                //DeductionAllotted.FooterRow.Cells[4].Font.Bold = true;

                decimal recovery = dt.AsEnumerable().Sum(row => row.Field<decimal>("recovery"));
                DeductionAllotted.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                DeductionAllotted.FooterRow.Cells[5].Text = recovery.ToString("N2");

                //DeductionAllotted.FooterRow.Cells[5].BackColor = System.Drawing.Color.Brown;
                //DeductionAllotted.FooterRow.Cells[5].ForeColor = System.Drawing.Color.White;
                //DeductionAllotted.FooterRow.Cells[5].Font.Bold = true;

                decimal others = dt.AsEnumerable().Sum(row => row.Field<decimal>("others"));
                DeductionAllotted.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                DeductionAllotted.FooterRow.Cells[6].Text = others.ToString("N2");

                DeductionAllotted.FooterStyle.ForeColor = System.Drawing.Color.Black;
                DeductionAllotted.FooterStyle.BackColor = System.Drawing.Color.Silver;
                DeductionAllotted.FooterStyle.Font.Bold = true;





            }
        }
        catch(Exception ex)
        {

            WebMsgBox.Show(ex.ToString());

        }
    }
    private void DeductionPendingList()
    {
        DataTable dt = new DataTable();
        con = new SqlConnection();

        try
        {
            using (con = dbaccess.GetConnection())
            {
                //    sqlstr = "SELECT agent_id,route_id,CAST(billadvance AS DECIMAL(18,2)) AS billadvance,CAST(Ai AS DECIMAL(18,2)) AS Ai,CAST(Feed AS DECIMAL(18,2)) AS Feed,CAST(can AS DECIMAL(18,2)) AS can,CAST(recovery AS DECIMAL(18,2))  AS recovery,CAST(others AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,deductiondate,103) AS deductiondate FROM Deduction_Details Where Plant_Code='" + pcode + "' AND deductiondate Between '" + d1 + "' AND '" + d2 + "' ORDER BY  agent_id";

                sqlstr = "SELECT agent_id,route_id,CAST(billadvance AS DECIMAL(18,2)) AS billadvance,CAST(Ai AS DECIMAL(18,2)) AS Ai,CAST(Feed AS DECIMAL(18,2)) AS Feed,CAST(can AS DECIMAL(18,2)) AS can,CAST(recovery AS DECIMAL(18,2))  AS recovery,CAST(others AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,deductiondate,103) AS deductiondate FROM Deduction_Details Where Plant_Code='" + pcode + "' AND  ( Ai > 0 or Feed > 0 or can > 0 or recovery > 0 or  others > 0) and deductiondate Between '" + d1 + "' AND '" + d2 + "' ORDER BY  agent_id";



                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                da.Fill(dt);
                count1 = dt.Rows.Count;
                DeductionPending.DataSource = dt;
                DeductionPending.DataBind();

                decimal billadv = dt.AsEnumerable().Sum(row => row.Field<decimal>("billadvance"));



                DeductionPending.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                DeductionPending.FooterRow.Cells[1].Text = billadv.ToString("N2");
                DeductionPending.FooterRow.HorizontalAlign = HorizontalAlign.Left;

                DeductionPending.FooterRow.Cells[0].Text = "Total";

                decimal ai = dt.AsEnumerable().Sum(row => row.Field<decimal>("Ai"));
                DeductionPending.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                DeductionPending.FooterRow.Cells[2].Text = ai.ToString("N2");

                decimal Feed = dt.AsEnumerable().Sum(row => row.Field<decimal>("Feed"));
                DeductionPending.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                DeductionPending.FooterRow.Cells[3].Text = Feed.ToString("N2");

                decimal can = dt.AsEnumerable().Sum(row => row.Field<decimal>("can"));
                DeductionPending.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                DeductionPending.FooterRow.Cells[4].Text = can.ToString("N2");

                decimal recovery = dt.AsEnumerable().Sum(row => row.Field<decimal>("recovery"));
                DeductionPending.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                DeductionPending.FooterRow.Cells[5].Text = recovery.ToString("N2");


                decimal others = dt.AsEnumerable().Sum(row => row.Field<decimal>("others"));
                DeductionPending.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                DeductionPending.FooterRow.Cells[6].Text = others.ToString("N2");

                DeductionPending.FooterStyle.ForeColor = System.Drawing.Color.Black;
                DeductionPending.FooterStyle.BackColor = System.Drawing.Color.Silver;
                DeductionPending.FooterStyle.Font.Bold = true;


            }

        }
        catch (Exception ex)
        {

            WebMsgBox.Show(ex.ToString());

        }







    }

    private void DeductionRecoveryList()
    {
        DataTable dt = new DataTable();
        con = new SqlConnection();
        try
        {
            using (con = dbaccess.GetConnection())
            {
                sqlstr = "SELECT Ragent_id AS agent_id,Rroute_id AS route_id,CAST(Rbilladvance AS DECIMAL(18,2)) AS billadvance,CAST(RAi AS DECIMAL(18,2)) AS Ai,CAST(RFeed AS DECIMAL(18,2)) AS Feed,CAST(Rcan AS DECIMAL(18,2)) AS can,CAST(Rrecovery AS DECIMAL(18,2))  AS recovery,CAST(Rothers AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,RDeduction_RecoveryDate,103) AS Rcoverydate,Rdeductiondate FROM Deduction_Recovery Where RPlant_Code='" + pcode + "' AND RDeduction_RecoveryDate='" + d3 + "' ORDER BY agent_id";
                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                da.Fill(dt);
                count2 = dt.Rows.Count;
                DeductionRecovery.DataSource = dt;
                DeductionRecovery.DataBind();
                DeductionRecovery.FooterRow.Cells[0].Text = "Total";

                decimal billadv = dt.AsEnumerable().Sum(row => row.Field<decimal>("billadvance"));
                DeductionRecovery.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery.FooterRow.Cells[1].Text = billadv.ToString("N2");



                DeductionRecovery.FooterRow.HorizontalAlign = HorizontalAlign.Left;



                decimal ai = dt.AsEnumerable().Sum(row => row.Field<decimal>("Ai"));
                DeductionRecovery.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery.FooterRow.Cells[2].Text = ai.ToString("N2");

                decimal Feed = dt.AsEnumerable().Sum(row => row.Field<decimal>("Feed"));
                DeductionRecovery.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery.FooterRow.Cells[3].Text = Feed.ToString("N2");

                decimal can = dt.AsEnumerable().Sum(row => row.Field<decimal>("can"));
                DeductionRecovery.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery.FooterRow.Cells[4].Text = can.ToString("N2");

                decimal recovery = dt.AsEnumerable().Sum(row => row.Field<decimal>("recovery"));
                DeductionRecovery.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery.FooterRow.Cells[5].Text = recovery.ToString("N2");


                decimal others = dt.AsEnumerable().Sum(row => row.Field<decimal>("others"));
                DeductionRecovery.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery.FooterRow.Cells[6].Text = others.ToString("N2");





                DeductionRecovery.FooterStyle.ForeColor = System.Drawing.Color.Black;
                DeductionRecovery.FooterStyle.BackColor = System.Drawing.Color.Silver;
                DeductionRecovery.FooterStyle.Font.Bold = true;
            }
        }

            catch(Exception ex)
        {

                WebMsgBox.Show(ex.ToString());

            }


        
    }


    private void DeductionRecoveryList1()
    {
        DataTable dt = new DataTable();
        con = new SqlConnection();
        try
        {
            using (con = dbaccess.GetConnection())
            {
              //  sqlstr = "SELECT Ragent_id AS agent_id,Rroute_id AS route_id,CAST(Rbilladvance AS DECIMAL(18,2)) AS billadvance,CAST(RAi AS DECIMAL(18,2)) AS Ai,CAST(RFeed AS DECIMAL(18,2)) AS Feed,CAST(Rcan AS DECIMAL(18,2)) AS can,CAST(Rrecovery AS DECIMAL(18,2))  AS recovery,CAST(Rothers AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,RDeduction_RecoveryDate,103) AS Rcoverydate FROM Deduction_Recovery Where RPlant_Code='" + pcode + "' AND Rdeductiondate='" + d3 + "' ORDER BY agent_id";
            //    sqlstr = "SELECT Ragent_id AS agent_id,Rroute_id AS route_id,CAST(Rbilladvance AS DECIMAL(18,2)) AS billadvance,CAST(RAi AS DECIMAL(18,2)) AS Ai,CAST(RFeed AS DECIMAL(18,2)) AS Feed,CAST(Rcan AS DECIMAL(18,2)) AS can,CAST(Rrecovery AS DECIMAL(18,2))  AS recovery,CAST(Rothers AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,RDeduction_RecoveryDate,103) AS Rcoverydate FROM Deduction_Recovery Where RPlant_Code='" + pcode + "' AND    RDeduction_recoveryDate='" + d3 + "' ORDER BY agent_id";
                sqlstr = "SELECT Ragent_id AS agent_id,Rroute_id AS route_id,CAST(Rbilladvance AS DECIMAL(18,2)) AS billadvance,CAST(RAi AS DECIMAL(18,2)) AS Ai,CAST(RFeed AS DECIMAL(18,2)) AS Feed,CAST(Rcan AS DECIMAL(18,2)) AS can,CAST(Rrecovery AS DECIMAL(18,2))  AS recovery,CAST(Rothers AS DECIMAL(18,2)) AS others,CONVERT(NVARCHAR,RDeduction_RecoveryDate,103) AS RDeduction_RecoveryDate,CONVERT(NVARCHAR,Rdeductiondate,103) AS Rdeductiondate FROM Deduction_Recovery Where RPlant_Code='"+pcode+"' AND RDeduction_RecoveryDate='"+d3+"' ORDER BY agent_id";
                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                da.Fill(dt);
                count2 = dt.Rows.Count;
                DeductionRecovery1.DataSource = dt;
                DeductionRecovery1.DataBind();
                DeductionRecovery1.FooterRow.Cells[0].Text = "Total";
                if (count2 > 0)
                {

                    Lbldeduct1.Visible = true;
                }


                else
                {

                    Lbldeduct1.Visible = false;

                }
                decimal billadv = dt.AsEnumerable().Sum(row => row.Field<decimal>("billadvance"));
                DeductionRecovery1.FooterRow.HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery1.FooterRow.Cells[1].Text = billadv.ToString("N2");


                decimal ai = dt.AsEnumerable().Sum(row => row.Field<decimal>("Ai"));
                DeductionRecovery1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery1.FooterRow.Cells[2].Text = ai.ToString("N2");

                decimal Feed = dt.AsEnumerable().Sum(row => row.Field<decimal>("Feed"));
                DeductionRecovery1.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery1.FooterRow.Cells[3].Text = Feed.ToString("N2");

                decimal can = dt.AsEnumerable().Sum(row => row.Field<decimal>("can"));
                DeductionRecovery1.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery1.FooterRow.Cells[4].Text = can.ToString("N2");

                decimal recovery = dt.AsEnumerable().Sum(row => row.Field<decimal>("recovery"));
                DeductionRecovery1.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery1.FooterRow.Cells[5].Text = recovery.ToString("N2");



                decimal others = dt.AsEnumerable().Sum(row => row.Field<decimal>("others"));
                DeductionRecovery1.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                DeductionRecovery1.FooterRow.Cells[6].Text = others.ToString("N2");



                DeductionRecovery1.FooterStyle.ForeColor = System.Drawing.Color.Black;
                DeductionRecovery1.FooterStyle.BackColor = System.Drawing.Color.Silver;
                DeductionRecovery1.FooterStyle.Font.Bold = true;
            }
        }

        catch (Exception ex)
        {

            WebMsgBox.Show(ex.ToString());

        }



    }

    private void Datefunc()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        DateTime dt3 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
        dt3 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
       // deductdate = dt1.AddDays(1).ToString("dd/MM/yyyy");
        deductdate = "Deduction Date:" + dt1.ToString("dd/MM/yyyy") + "To:" + dt2.ToString("dd/MM/yyyy");
        lbldate.Text = deductdate; ;
        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");
        d3 = dt1.AddDays(1).ToString("MM/dd/yyyy");
      


    }

    private void LoadPlantName()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='170' group by plant_code,plant_name";
            con = dbaccess.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            DataTable getdata = new DataTable();
            SqlDataAdapter dsii = new SqlDataAdapter(cmd);
            getdata.Rows.Clear();
            dsii.Fill(getdata);
            ddl_PlantName.DataSource = getdata;
            ddl_PlantName.DataTextField = "Plant_Name";
            ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
            ddl_PlantName.DataBind();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    private void loadsingleplant()//form load method
    {
        try
        {
            string STR = "Select   plant_code,plant_name   from plant_master where plant_code='"+pcode+"' group by plant_code,plant_name";
            con = dbaccess.GetConnection();
            SqlCommand cmd = new SqlCommand(STR, con);
            DataTable getdata = new DataTable();
            SqlDataAdapter dsii = new SqlDataAdapter(cmd);
            getdata.Rows.Clear();
            dsii.Fill(getdata);
            ddl_PlantName.DataSource = getdata;
            ddl_PlantName.DataTextField = "Plant_Name";
            ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
            ddl_PlantName.DataBind();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }
    protected void HyperLink1_DataBinding(object sender, EventArgs e)
    {
       
      
    }
    protected void deduc_allot_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            DeductionAllotList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = false;

            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }

            Lbldeduct.Visible = false;
            ldl_deducdetails.Visible = false;
        }

        if (CheckBox3.Checked == true)
        {
            DeductionRecoveryList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = true;

            lblamount.Visible = false;
            Lbldeduct.Visible = false;
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }
        }

        if (CheckBox2.Checked == true)
        {
            DeductionPendingList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = false;
            lblamount.Visible = false;
            if(count1 > 0)
            {
            Lbldeduct.Visible = true;
            }
            else
            {
            Lbldeduct.Visible = false;
            }

            ldl_deducdetails.Visible = false;
        }
        if ((CheckBox1.Checked == true) &&  (CheckBox2.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = false;

            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }


            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            ldl_deducdetails.Visible = false;

        }

        if ((CheckBox2.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = true;
            lblamount.Visible = false;
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }
          

        }

        if ((CheckBox1.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = true;


            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            Lbldeduct.Visible = false;
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }
        }



        if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true) && (CheckBox3.Checked==true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = true;

            if (count < 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }

        }



    }
    protected void deduct_pen_CheckedChanged(object sender, EventArgs e)
    {
        //if (CheckBox2.Checked == true)
        //{
        //    DeductionAllotList();
        //    DeductionAllotted.Visible = false;
        //    DeductionPending.Visible = true;
        //    DeductionRecovery.Visible = false;
        //}
        //if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true))
        //{
        //    DeductionAllotList();
        //    DeductionPendingList();
        //    DeductionAllotted.Visible = true;
        //    DeductionPending.Visible = true;
        //    DeductionRecovery.Visible = false;

        //}

        //if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true) && (CheckBox3.Checked == true))
        //{
        //    DeductionAllotList();
        //    DeductionPendingList();
        //    DeductionRecoveryList();
        //    DeductionAllotted.Visible = true;
        //    DeductionPending.Visible = true;
        //    DeductionRecovery.Visible = true;

        //}

        if (CheckBox1.Checked == true)
        {
            DeductionAllotList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = false;

            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }

            Lbldeduct.Visible = false;
            ldl_deducdetails.Visible = false;
        }

        if (CheckBox3.Checked == true)
        {
            DeductionRecoveryList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = true;

            lblamount.Visible = false;
            Lbldeduct.Visible = false;
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }
        }

        if (CheckBox2.Checked == true)
        {
            DeductionPendingList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = false;


            if (count < 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            ldl_deducdetails.Visible = false;
        }
        if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = false;

            if (count < 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            Lbldeduct.Visible = true;
            ldl_deducdetails.Visible = false;

        }

        if ((CheckBox2.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = true;

            lblamount.Visible = false;
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }

        }

        if ((CheckBox1.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = true;
            if (count < 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            Lbldeduct.Visible = false;
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }

        }

        if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = true;

            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }

        }
      
    }
    protected void deduct_details_CheckedChanged(object sender, EventArgs e)
    {
        //DeductionRecoveryList();
        //DeductionAllotted.Visible = false;
        //DeductionPending.Visible = false;
        //DeductionRecovery.Visible = true;


        if (CheckBox1.Checked == true)
        {
            DeductionAllotList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = false;

            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }

            Lbldeduct.Visible = false;
            ldl_deducdetails.Visible = false;
        }

        if (CheckBox3.Checked == true)
        {
            DeductionRecoveryList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = true;

            lblamount.Visible = false;
            Lbldeduct.Visible = false;
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }
        }

        if (CheckBox2.Checked == true)
        {
            DeductionPendingList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = false;


            lblamount.Visible = false;
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            ldl_deducdetails.Visible = false;
        }
        if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = false;

            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            ldl_deducdetails.Visible = false;
        }

        if ((CheckBox2.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = false;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = true;

            lblamount.Visible = false;
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }

        }

        if ((CheckBox1.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = false;
            DeductionRecovery.Visible = true;


            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            Lbldeduct.Visible = false;
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }
        }



        if ((CheckBox1.Checked == true) && (CheckBox2.Checked == true) && (CheckBox3.Checked == true))
        {
            DeductionAllotList();
            DeductionPendingList();
            DeductionRecoveryList();
            DeductionAllotted.Visible = true;
            DeductionPending.Visible = true;
            DeductionRecovery.Visible = true;

            if (count > 0)
            {
                lblamount.Visible = true;
            }
            else
            {
                lblamount.Visible = false;
            }
            if (count1 > 0)
            {
                Lbldeduct.Visible = true;
            }
            else
            {
                Lbldeduct.Visible = false;
            }
            if (count2 > 0)
            {
                ldl_deducdetails.Visible = true;
            }
            else
            {
                ldl_deducdetails.Visible = false;

            }

        }
       
    }
    protected void AlDate_CheckedChanged(object sender, EventArgs e)
    {
        //DeductionAllotted.Visible = true;
        //DeductionPending.Visible = true;
        //DeductionRecovery.Visible = true;
        //DeductionAllotList();
        //DeductionPendingList();
        //DeductionRecoveryList();

       


    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        //    DeductionAllotList();
        //    DeductionPendingList();
        //    DeductionRecoveryList();
        //    DeductionAllotted.Visible = true;
        //    DeductionPending.Visible = true;
        //    DeductionRecovery.Visible = true;


        //    if (count > 0)
        //    {
        //        lblamount.Visible = true;
        //    }
        //    else
        //    {
        //        lblamount.Visible = false;
        //    }
        //    Lbldeduct.Visible = true;
        //    ldl_deducdetails.Visible = true;

        //}
    }
   
    protected void btn_print_Click(object sender, EventArgs e)
    {

    }
}