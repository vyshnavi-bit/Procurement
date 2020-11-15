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

public partial class RptDeduction_detailmaster : System.Web.UI.Page
{
    int ccode = 1, pcode;
    string sqlstr = string.Empty;
    DataSet ds = new DataSet();
    BLLPlantName BllPlant = new BLLPlantName();
    DbHelper dbaccess = new DbHelper();
    SqlConnection con;
    SqlCommand cmd = new SqlCommand();
    DateTime dat = new DateTime();
    BLLuser Bllusers = new BLLuser();
    public string managmobNo;
    public string pname;
    public string cname;
    decimal totalStock;
    string d1 = string.Empty;
    string d2 = string.Empty;
    int count, count1, count2;
    int i = 2;
    int k;
    int l;
    int lcolumncount;
    DataRow dk;
   string[] BANK = new string[100];
   string get;
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
             //   managmobNo = Session["managmobNo"].ToString();
                dat = System.DateTime.Now;
                txt_FromDate.Text = dat.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dat.ToString("dd/MM/yyyy");
                if (roleid < 3)
                {
                    loadsingleplant();
                    pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                }
                if((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                    pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                }
                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    pcode = 170;
                    loadspecialsingleplant();

                }
               
                lblpname.Text = ddl_PlantName.SelectedItem.Text;
                Lbl_Errormsg.Visible=false;
                rbtLstReportItems.SelectedIndex = 0;
                ddl_AgentName.Visible = false;
                Label14.Visible = false;
                lblpagname.Visible = false;
                DeductionmasterDetails3.Visible = false;
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
                  ccode = Convert.ToInt32(Session["Company_code"]);
                  pcode = Convert.ToInt32(ddl_PlantName.SelectedItem.Value);
                  lblpname.Text = ddl_PlantName.SelectedItem.Text;
                  Lbl_Errormsg.Visible=false;
                  ddl_AgentName.Visible = false;
                  Label14.Visible = false;
                  lblpagname.Visible = false;
                  DeductionmasterDetails3.Visible = false;
            }
             else
             {

                 Server.Transfer("LoginDefault.aspx");
             }

        }
    }

    private void LoadPlantcode()
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

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }

    private void loadsingleplant()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

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
            //SqlDataReader dr = null;
            ////ddl_Plantcode.Items.Clear();
            //ddl_PlantName.Items.Clear();
            //dr = Bllusers.LoadSinglePlantcode(ccode.ToString(), "170");
            //if (dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
                 
            //        ddl_PlantName.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

            //    }
            //}


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


    private void Datefunc()
    {
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
      

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        lbldate.Text = "Entry Date:" + dt1.ToString("dd/MM/yyyy") + "To:" + dt2.ToString("dd/MM/yyyy");

        d1 = dt1.ToString("MM/dd/yyyy");
        d2 = dt2.ToString("MM/dd/yyyy");

    }
   

    private void RpyGroupBoxChange()
    {
        try
        {
        if (rbtLstReportItems.SelectedItem != null)
        {
            Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
            if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
            {
                GetPlantdeductionMasterdetails();
            }
            else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
            {
                ddl_AgentName.Visible = true;
             //   GetAgentdeductionMasterdetails();
                GetAgentdeductionMasterdetails3();
               // GetAgentdeductionMasterdetails4();
                lblpagname.Text = ddl_AgentName.SelectedItem.Text;
            }
            else
            {
            }
        }
        else
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = "Please, Select Report Type";
        }
        }
            catch (Exception ex)
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = ex.ToString();
            }
    }

    private void GetPlantdeductionMasterdetails()
    {
        string sqlstr1 = string.Empty;
        string CheckColName = string.Empty;
        SqlDataReader dr;
        DataTable custdt1 = new DataTable();
        DataColumn col1 = null;       
        con = new SqlConnection();
        Datefunc();

        DataTable custdt = new DataTable();
        DataRow custdr = null;
        try
        {
            using (con = dbaccess.GetConnection())
            {
                //sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' ORDER BY RAND(Dm_agent_Id)";
//                sqlstr = "SELECT StockGroup,HeaderName FROM  " +
//" (SELECT DISTINCT(StockSubGroup) AS HeaderName FROM " +
//" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "') AS t1 " +
//" LEFT JOIN " +
//" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID GROUP BY StockSubGroup,t1.Dm_Agent_Id ) AS t3 " +
//" LEFT JOIN " +
//" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master) AS t4 ON t3.HeaderName=t4.StockSubGroup Order By StockGroupID ,StockSubGroupID";
                //ANAND


                sqlstr = "SELECT StockGroup,HeaderName FROM  " +
" (SELECT DISTINCT(StockSubGroup) AS HeaderName FROM " +
" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "') AS t1 " +
" LEFT JOIN " +
" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID GROUP BY StockSubGroup,t1.Dm_Agent_Id ) AS t3 " +
" LEFT JOIN " +
" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master) AS t4 ON t3.HeaderName=t4.StockSubGroup GROUP BY StockGroup,HeaderName ";

                dr = dbaccess.GetDatareader(sqlstr);
                //Add ColumnValues Start
                col1 = new DataColumn("Sno".ToString());
                custdt1.Columns.Add(col1);
                col1 = new DataColumn("Agent_Id".ToString());
                custdt1.Columns.Add(col1);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        col1 = new DataColumn(dr["HeaderName"].ToString());
                        custdt1.Columns.Add(col1);
                    }
                }
                //Add ColumnValues End

                //Add RowValues Start
                sqlstr1 = "SELECT ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_Agent_Id AS Aid,SUM(Dm_Amount) AS SAmount,StockSubGroup FROM " +
" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='1' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "') AS t1 " +
" LEFT JOIN " +
" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID GROUP BY StockSubGroup,t1.Dm_Agent_Id";
             SqlDataAdapter adp = new SqlDataAdapter(sqlstr1, con);
             adp.Fill(custdt);
             object id1;
            id1 = "0";
            int idd1 = Convert.ToInt32(id1);
            int Sno = 0;
            foreach (DataRow dr1 in custdt.Rows)
            {
                object id;

                id = dr1[1].ToString();
                int idd = Convert.ToInt32(id);                
                int i = 0;

                if (idd1 == idd)
                {
                    CheckColName = dr1[3].ToString();
                    int ks = 0;
                    foreach (DataColumn dc in custdt1.Columns)
                    {
                        if (CheckColName == dc.ToString())
                        {
                            custdr[ks] = dr1["SAmount"].ToString();
                            //var field1 = dtRow[dc].ToString();
                        }
                        ks++;
                    }
                    
                    id1 = dr1["Aid"].ToString();
                    idd1 = Convert.ToInt32(id1);
                    i++;
                }
                else
                {
                    Sno++;
                    custdr = custdt1.NewRow();
                   // custdr[0] = dr1["Sno"].ToString();
                    custdr[0] = Sno.ToString();
                    custdr[1] = dr1["Aid"].ToString();
                    CheckColName = dr1[3].ToString();
                    int ks = 0;
                    foreach (DataColumn dc in custdt1.Columns)
                    {
                        if (CheckColName == dc.ToString())
                        {
                            custdr[ks] = dr1["SAmount"].ToString();
                            //var field1 = dtRow[dc].ToString();
                        }
                        ks++;
                    }
                    
                    custdt1.Rows.Add(custdr);
                    id1 = dr1["Aid"].ToString();
                    idd1 = Convert.ToInt32(id1);
                    i++;
                }
            }

                //Add RowValues End
            DeductionmasterDetails.Visible = false;
            plantDeductionmasterDetails.Visible = true;
            plantDeductionmasterDetails.DataSource = custdt1;
            plantDeductionmasterDetails.DataBind();
            if (custdt1.Rows.Count > 0)
            {
                int sumcountcheck = 0;
                foreach (DataColumn dc in custdt1.Columns)
                {
                    if (sumcountcheck == 1)
                    {
                        plantDeductionmasterDetails.FooterRow.Cells[1].Text = "Total";
                        plantDeductionmasterDetails.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                    }
                    else if (sumcountcheck > 1)
                    { 
                        decimal Gtotalcommon = 0;
                        foreach (DataRow dr1 in custdt1.Rows)
                        {
                            object id;
                            id = dr1[sumcountcheck].ToString();
                            if (id == "")
                            {
                                id = 0;
                            }
                            decimal idd = Convert.ToDecimal(id);
                            Gtotalcommon = Gtotalcommon + idd;
                            plantDeductionmasterDetails.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Right;
                            plantDeductionmasterDetails.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
                            plantDeductionmasterDetails.FooterRow.HorizontalAlign = HorizontalAlign.Right;                                                  
                        }
                    }
                    else
                    {

                    }
                    sumcountcheck++;                }

                plantDeductionmasterDetails.FooterStyle.ForeColor = System.Drawing.Color.Black;
                plantDeductionmasterDetails.FooterStyle.BackColor = System.Drawing.Color.Silver;
                plantDeductionmasterDetails.FooterStyle.Font.Bold = false;
            }



            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }

    }


    private void GetAgentdeductionMasterdetails()
    {
        
            DataTable dt = new DataTable();
            con = new SqlConnection();
            Datefunc();
            try
            {
                using (con = dbaccess.GetConnection())
                {
                    //sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' ORDER BY RAND(Dm_agent_Id)";
                    sqlstr = "SELECT * FROM " +
" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,13) AS  Dm_EntryDate   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' and Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "') AS t1 " +
" LEFT JOIN " +
"(SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID ORDER BY RAND(Dm_agent_Id)";


                    SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                    da.Fill(dt);
                    count1 = dt.Rows.Count;
                    plantDeductionmasterDetails.Visible = false;
                    DeductionmasterDetails.Visible = true;
                    DeductionmasterDetails.DataSource = dt;
                    DeductionmasterDetails.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        DeductionmasterDetails.FooterRow.Cells[0].Text = "Total";
                        DeductionmasterDetails.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;


                        decimal Samt = dt.AsEnumerable().Sum(row => row.Field<decimal>("Dm_Amount"));
                        DeductionmasterDetails.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                        DeductionmasterDetails.FooterRow.Cells[1].Text = Samt.ToString("N2");
                        DeductionmasterDetails.FooterRow.HorizontalAlign = HorizontalAlign.Right;



                        DeductionmasterDetails.FooterStyle.ForeColor = System.Drawing.Color.Black;
                        DeductionmasterDetails.FooterStyle.BackColor = System.Drawing.Color.Silver;
                        DeductionmasterDetails.FooterStyle.Font.Bold = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = ex.ToString();
            }
       

    }


    private void GetAgentdeductionMasterdetails3()
    {

//        DataTable dt = new DataTable();
//        con = new SqlConnection();
//        Datefunc();
//        try
//        {
//            using (con = dbaccess.GetConnection())
//            {
//                //sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' ORDER BY RAND(Dm_agent_Id)";
////                sqlstr = "SELECT * FROM " +
////" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,13) AS  Dm_EntryDate   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' and Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "') AS t1 " +
////" LEFT JOIN " +
////"(SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID ORDER BY RAND(Dm_agent_Id)";
//             //   sqlstr = "SELECT convert(varchar,Dm_EntryDate,103) as Date,stocksubgroup AS STOCK ,Dm_amount AS AMOUNT   FROM (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,103) AS  Dm_EntryDate   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' and Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "') AS t1  LEFT JOIN  (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID ORDER BY RAND(Dm_agent_Id)";
//                sqlstr = "SELECT convert(varchar,Dm_EntryDate,103) as Date,stocksubgroup AS STOCK ,Dm_amount AS AMOUNT   FROM (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,103) AS  Dm_EntryDate   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' and Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "') AS t1  LEFT JOIN  (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID ORDER BY RAND(Dm_agent_Id)";

//                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
//                da.Fill(dt);
//                count1 = dt.Rows.Count;

//                if (count1 > 1)
//                {

//                    int l =0;

//                    for (int a = 3; a <= count1; a++)
//                    {

                       
//                        l=l+0;
//                           string bank = "Header" + i;

//                           //BANK = bank.Split();
//                           //if (BANK[l] != "")
//                           // {
//                           string getname = bank;
//                           dt.Columns.Add(new DataColumn(getname).ToString());
//                           i++;
                           

//                    }


//                }
//                plantDeductionmasterDetails.Visible = false;
//                DeductionmasterDetails3.Visible = true;
//                DeductionmasterDetails3.DataSource = dt;
//                DeductionmasterDetails3.DataBind();

               
//                ////string[] add=

//                //foreach( GridView gk  in DeductionmasterDetails3.Rows)
//                //{
//                //    if (count1 > 2)
//                //    {
//                //        dt.Rows.Add("", "", null);
//                //    }

//                //}
//                //DeductionmasterDetails3.DataSource = dt;
//                //DeductionmasterDetails3.DataBind();
//        //Add some rows

//        //First Row
       
      



//                //DeductionmasterDetails3.DataBind();
//                //DeductionmasterDetails3.DataSource = ConvertColumnsAsRows(dt);
//                //DeductionmasterDetails3.DataBind();
//                //DeductionmasterDetails3.HeaderRow.Visible = false;

//                Label14.Visible = true;
//                lblpagname.Visible = true;
//                    if (dt.Rows.Count > 0)
//                    {
//                        DeductionmasterDetails3.FooterRow.Cells[1].Text = "Total";
//                        DeductionmasterDetails3.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;


//                        decimal Samt = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
//                        DeductionmasterDetails3.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
//                        DeductionmasterDetails3.FooterRow.Cells[2].Text = Samt.ToString("N2");
//                        DeductionmasterDetails3.FooterRow.HorizontalAlign = HorizontalAlign.Left;

//                        DeductionmasterDetails3.HeaderStyle.ForeColor = System.Drawing.Color.Black;
//                        DeductionmasterDetails3.HeaderStyle.BackColor = System.Drawing.Color.Silver;
//                        DeductionmasterDetails3.HeaderStyle.Font.Bold = true;

//                        DeductionmasterDetails3.FooterStyle.ForeColor = System.Drawing.Color.Black;
//                        DeductionmasterDetails3.FooterStyle.BackColor = System.Drawing.Color.Silver;
//                        DeductionmasterDetails3.FooterStyle.Font.Bold = true;
//                    }
                
//            }

//        }
//        catch (Exception ex)
//        {
//            Lbl_Errormsg.Visible = true;
//            Lbl_Errormsg.Text = ex.ToString();
//        }





        string sqlstr1 = string.Empty;
        string CheckColName = string.Empty;
        SqlDataReader dr;
        DataTable custdt1 = new DataTable();
        DataColumn col1 = null;
        con = new SqlConnection();
        Datefunc();

        DataTable custdt = new DataTable();
        DataRow custdr = null;
        try
        {
            using (con = dbaccess.GetConnection())
            {
                //sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' ORDER BY RAND(Dm_agent_Id)";
                sqlstr = "SELECT StockGroup,HeaderName FROM  " +
" (SELECT DISTINCT(StockSubGroup) AS HeaderName FROM " +
" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_EntryDate ) AS Sno,CONVERT(Nvarchar(50),Dm_EntryDate,103) AS  Date,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "'  and  Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "' AND Dm_DeductionDate Between '" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "') AS t1 " +
" LEFT JOIN " +
" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID GROUP BY StockSubGroup ) AS t3 " +
" LEFT JOIN " +
" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master) AS t4 ON t3.HeaderName=t4.StockSubGroup Order By StockGroupID ,StockSubGroupID";

                dr = dbaccess.GetDatareader(sqlstr);
                //Add ColumnValues Start
                col1 = new DataColumn("Sno".ToString());
                custdt1.Columns.Add(col1);
                col1 = new DataColumn("Date".ToString());
                custdt1.Columns.Add(col1);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        col1 = new DataColumn(dr["HeaderName"].ToString());
                        custdt1.Columns.Add(col1);
                    }
                }
                //Add ColumnValues End

                //Add RowValues Start
//                sqlstr1 = "SELECT ROW_NUMBER() OVER(ORDER BY Dm_EntryDate ) AS Sno,Dm_EntryDate as Date,SUM(Dm_Amount) AS SAmount,StockSubGroup FROM " +
//" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_EntryDate ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,103) AS  Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='1' AND     Dm_Plantcode='" + pcode + "' and   Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "'  AND Dm_DeductionDate Between '" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "') AS t1 " +
//" LEFT JOIN " +
//" (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID GROUP BY StockSubGroup,t1.Dm_EntryDate";

                sqlstr1 = "SELECT sno,SAmount,CONVERT(VARCHAR,Dm_EntryDate,103) AS  Date,StockSubGroup  FROM (SELECT ROW_NUMBER() OVER(ORDER BY Dm_EntryDate ) AS Sno,Dm_EntryDate,SUM(Dm_Amount) AS SAmount,StockSubGroup FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_EntryDate ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,103) AS  Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='1' AND     Dm_Companycode='1' AND     Dm_Plantcode='" + pcode + "' and   Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "'  AND Dm_DeductionDate Between '" + d1.ToString().Trim() + "' AND  '" + d2.ToString().Trim() + "') AS t1  LEFT JOIN  (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID GROUP BY StockSubGroup,t1.Dm_EntryDate ) AS GG  ORDER BY convert(datetime, Dm_EntryDate, 103)  DESC";
                SqlDataAdapter adp = new SqlDataAdapter(sqlstr1, con);
                adp.Fill(custdt);
                object id1;
                id1 = "0";
                string idd1 = id1.ToString();
                int Sno = 0;
                foreach (DataRow dr1 in custdt.Rows)
                {
                    object id;

                    id = dr1[1].ToString();
                    string idd = (id).ToString();
                    int i = 0;

                    if (idd1 == idd)
                    {
                        CheckColName = dr1[3].ToString();
                        int ks = 0;
                        foreach (DataColumn dc in custdt1.Columns)
                        {
                            if (CheckColName == dc.ToString())
                            {
                                custdr[ks] = dr1["SAmount"].ToString();
                                //var field1 = dtRow[dc].ToString();
                            }
                            ks++;
                        }

                        id1 = dr1["Date"].ToString();
                        idd1 = (id1).ToString();
                        i++;
                    }
                    else
                    {
                        Sno++;
                        custdr = custdt1.NewRow();
                        // custdr[0] = dr1["Sno"].ToString();
                        custdr[0] = Sno.ToString();
                        custdr[1] = dr1["Date"].ToString();
                        CheckColName = dr1[3].ToString();
                        int ks = 0;
                        foreach (DataColumn dc in custdt1.Columns)
                        {
                            if (CheckColName == dc.ToString())
                            {
                                custdr[ks] = dr1["SAmount"].ToString();
                                //var field1 = dtRow[dc].ToString();
                            }
                            ks++;
                        }

                        custdt1.Rows.Add(custdr);
                        id1 = dr1["Date"].ToString();
                        idd1 = (id1).ToString();
                        i++;
                    }
                }

                //Add RowValues End
                DeductionmasterDetails.Visible = false;
                DeductionmasterDetails3.Visible = true;
                DeductionmasterDetails3.DataSource = custdt1;
                DeductionmasterDetails3.DataBind();
                Label14.Visible = true;
                lblpagname.Visible = true;
                plantDeductionmasterDetails.Visible = false;
                //decimal Samt = custdt1.AsEnumerable().Sum(row => row.Field<decimal>("SAmount"));
                //DeductionmasterDetails3.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                //DeductionmasterDetails3.FooterRow.Cells[2].Text = Samt.ToString("N2");

                if (custdt1.Rows.Count > 0)
                {
                    int sumcountcheck = 0;
                    foreach (DataColumn dc in custdt1.Columns)
                    {
                        if (sumcountcheck == 1)
                        {
                            DeductionmasterDetails3.FooterRow.Cells[1].Text = "Total";
                            DeductionmasterDetails3.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;

                        }
                        else if (sumcountcheck > 1)
                        {
                            decimal Gtotalcommon = 0;
                            foreach (DataRow dr1 in custdt1.Rows)
                            {
                                object id;
                                id = dr1[sumcountcheck].ToString();
                                if (id == "")
                                {
                                    id = 0;
                                }
                                decimal idd = Convert.ToDecimal(id);
                                Gtotalcommon = Gtotalcommon + idd;
                                DeductionmasterDetails3.FooterRow.Cells[sumcountcheck].HorizontalAlign = HorizontalAlign.Left;
                                DeductionmasterDetails3.FooterRow.Cells[sumcountcheck].Text = Gtotalcommon.ToString("N2");
                                DeductionmasterDetails3.FooterRow.HorizontalAlign = HorizontalAlign.Left;

                            

                            }
                        }
                        else
                        {

                        }
                        sumcountcheck++;
                    }

                    DeductionmasterDetails3.FooterStyle.ForeColor = System.Drawing.Color.Black;
                    DeductionmasterDetails3.FooterStyle.BackColor = System.Drawing.Color.Silver;
                    DeductionmasterDetails3.FooterStyle.Font.Bold = false;
                }



            }

        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }





    }

    //private void GetAgentdeductionMasterdetails4()
    //{

    //    DataTable dt = new DataTable();
    //    con = new SqlConnection();
    //    Datefunc();
    //    try
    //    {
    //        using (con = dbaccess.GetConnection())
    //        {
    //            //sqlstr = "SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,Dm_EntryDate  FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' ORDER BY RAND(Dm_agent_Id)";
    //            //                sqlstr = "SELECT * FROM " +
    //            //" (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,13) AS  Dm_EntryDate   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' and Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "') AS t1 " +
    //            //" LEFT JOIN " +
    //            //"(SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID ORDER BY RAND(Dm_agent_Id)";
    //            //   sqlstr = "SELECT convert(varchar,Dm_EntryDate,103) as Date,stocksubgroup AS STOCK ,Dm_amount AS AMOUNT   FROM (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,103) AS  Dm_EntryDate   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' and Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "') AS t1  LEFT JOIN  (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID ORDER BY RAND(Dm_agent_Id)";
    //            sqlstr = "SELECT convert(varchar,Dm_EntryDate,103) as Date   FROM (SELECT  ROW_NUMBER() OVER(ORDER BY Dm_agent_Id ) AS Sno,Dm_agent_Id,Dm_StockGroupId ,Dm_StockSubGroupId ,Dm_Quantity ,Dm_Rate ,Dm_Amount ,CONVERT(Nvarchar(50),Dm_EntryDate,103) AS  Dm_EntryDate   FROM DeductionDetails_Master Where Dm_Companycode='" + ccode + "' AND Dm_Plantcode='" + pcode + "' AND Dm_DeductionDate Between '" + d1.ToString() + "' AND '" + d2.ToString() + "' and Dm_agent_Id='" + ddl_AgentName.SelectedItem.Value.Trim() + "') AS t1  LEFT JOIN  (SELECT StockGroup,StockSubGroup,StockGroupId,StockSubGroupID FROM Stock_Master ) AS t2 ON t1.Dm_StockGroupId=t2.StockGroupID AND t1.Dm_StockSubGroupId=t2.StockSubGroupID ORDER BY RAND(Dm_agent_Id)";

    //            SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
    //            da.Fill(dt);
    //            count1 = dt.Rows.Count;
    //            plantDeductionmasterDetails.Visible = false;
    //            DeductionmasterDetails4.Visible = true;
    //            DeductionmasterDetails4.DataSource = dt;
    //            DeductionmasterDetails4.DataBind();


    //            DeductionmasterDetails4.DataBind();
    //            DeductionmasterDetails4.DataSource = ConvertColumnsAsRows(dt);
    //            DeductionmasterDetails4.DataBind();
    //            DeductionmasterDetails4.HeaderRow.Visible = false;
    //            DeductionmasterDetails4.Visible = true;

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Lbl_Errormsg.Visible = true;
    //        Lbl_Errormsg.Text = ex.ToString();
    //    }


    //}
    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        RpyGroupBoxChange();




    }


    protected void DeductionmasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
       
    }




    protected void plantDeductionmasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
            }
        }
        catch (Exception ex)
        {
            //Lbl_Errormsg.Visible = true;
            //Lbl_Errormsg.Text = ex.ToString();
        }
    }
   
    protected void rbtLstReportItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtLstReportItems.SelectedItem != null)
            {
                Lbl_selectedReportItem.Text = rbtLstReportItems.SelectedItem.Value;
                if (Lbl_selectedReportItem.Text.Trim() == "1".Trim())
                {
                    ddl_AgentName.Visible = false;
                }
                else if (Lbl_selectedReportItem.Text.Trim() == "2".Trim())
                {
                    ddl_AgentName.Visible = true;
                    LoadAgentds();
                }
                else
                {
                }
            }
            else
            {
                Lbl_Errormsg.Visible = true;
                Lbl_Errormsg.Text = "Please, Select the Type...";
            }
        }
        catch (Exception ex)
        {
            Lbl_Errormsg.Visible = true;
            Lbl_Errormsg.Text = ex.ToString();
        }
    }


    private void LoadAgentds()
    {
        try
        {
            ddl_AgentName.Items.Clear();
            string str = string.Empty;
            ds = null;
            str = "SELECT Agent_Id,Convert(Nvarchar(15),Agent_Id)+'_'+ Agent_Name AS AgentName  FROM Agent_Master Where Company_code='" + ccode + "' AND Plant_code='" + pcode + "' Order By Agent_Id ";
            ds = dbaccess.GetDataset(str);
            if (ds != null)
            {
                ddl_AgentName.DataSource = ds;
                ddl_AgentName.DataTextField = "AgentName";
                ddl_AgentName.DataValueField = "Agent_Id";
                ddl_AgentName.DataBind();
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }

    }
    //public DataTable ConvertColumnsAsRows(DataTable dt)
    //{
    //    //DataTable dtnew = new DataTable();
    //    ////Convert all the rows to columns
    //    //for (int i = 0; i <= dt.Rows.Count; i++)
    //    //{
    //    //    dtnew.Columns.Add(Convert.ToString(i));
    //    //}
    //    //DataRow dr;
    //    //// Convert All the Columns to Rows
    //    //for (int j = 0; j < dt.Columns.Count; j++)
    //    //{
    //    //    dr = dtnew.NewRow();
    //    //    dr[0] = dt.Columns[j].ToString();
    //    //    for (int k = 1; k <= dt.Rows.Count; k++)
    //    //        dr[k] = dt.Rows[k - 1][j];
    //    //    dtnew.Rows.Add(dr);
    //    //}
    //    //return dtnew;
    //}
    protected void DeductionmasterDetails3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
       


    
    }

    protected void DeductionmasterDetails3_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.Header)
        //{

        //    int tm = count1 + 1;
        //    GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //    TableCell HeaderCell2 = new TableCell();
        //    HeaderCell2.Text = "Agent Stock List";
        //    HeaderCell2.ColumnSpan = tm;
        //    HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderRow.Cells.Add(HeaderCell2);
        //    DeductionmasterDetails3.Controls[0].Controls.AddAt(0, HeaderRow);
        //    HeaderCell2.Font.Bold = true;


        //}
    }

    //protected void gv_DataBinding(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridViewGroup First = new GridViewGroup(DeductionmasterDetails3, null, "BranchCode");
    //        GridViewGroup Second = new GridViewGroup(DeductionmasterDetails3, First, "Branch");
    //    }
    //    catch (Exception ex)
    //    {
    //      //  WriteLogItem(ex.Message, ex.StackTrace, Convert.ToString(Session["EmpId"]));
    //    }
    //}



}
