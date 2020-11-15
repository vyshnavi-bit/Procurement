using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PlantAvailabilityStock : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dtm = new DateTime();
    BLLuser Bllusers = new BLLuser();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    DataTable collect = new DataTable();
    int datasetcount = 0;
    string getgroupname;
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
                    Session["tempp"] = Convert.ToString(pcode);
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                    if (roleid < 3)
                    {
                        loadsingleplant();

                    }

                     if (roleid >= 3)
                    {

                        LoadPlantcode();

                    }
                    if (roleid == 9)
                    {
                        loadspecialsingleplant();
                        Session["Plant_Code"] = "170";
                    }
                    
                }
                else
                {



                }
              
            }


            else
            {
                ccode = Session["Company_code"].ToString();

                pcode = ddl_Plantname.SelectedItem.Value;
              



            }
        }

        catch
        {



        }
    }

    public void LoadPlantcode()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code  not in (150,139)";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("singleplant"));
            ddl_Plantname.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
         //   datasetcount = datasetcount + 1;
        }
        catch
        {
        }

    }
    public void loadsingleplant()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE Plant_Code = '" + pcode + "'";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("allplant"));
            ddl_Plantname.DataSource = DTG.Tables[datasetcount];
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "Plant_Code";
            ddl_Plantname.DataBind();
          //  datasetcount = datasetcount + 1;
        }
        catch
        {
        }
    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
       
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddl_Plantname.Items.Add(dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }


    public void gettingstockgroup()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "Select   StockGroup,null as Subgroup,null as Plantqty,null AS IssudQty,null as ItemRate,null as AvailStock    from Stock_Master  group by StockGroup order by   StockGroup asc  ";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("stockgroup"));
          
        }
        catch
        {
        }
    }

    public void getdata()
    {
        try
        {

            collect.Columns.Add("StockGroup");
            collect.Columns.Add("Subgroup");
            collect.Columns.Add("Plantqty");
            collect.Columns.Add("IssudQty");
        //    collect.Columns.Add("ItemRate");
            collect.Columns.Add("AvailStock");
            gettingstockgroup();
            foreach (DataRow getdata in DTG.Tables[datasetcount].Rows)
            {

                getgroupname = getdata[0].ToString();
                collect.Rows.Add(getgroupname, "", "", "", "");
                gettingstockgroupdetails();
                foreach (DataRow getdatadetails in DTG.Tables[datasetcount].Rows)
                {
             
                    string getsubgroupname = getdatadetails[0].ToString();
                    string plantqty = getdatadetails[1].ToString();
                    string IssuedQty = getdatadetails[2].ToString();
                    string Itemrate = getdatadetails[3].ToString();
                    string Avilstock = getdatadetails[4].ToString();
                    double conplantqty = Convert.ToDouble(plantqty);
                    double conIssuedQty = Convert.ToDouble(IssuedQty);
                    Avilstock = (conplantqty - conIssuedQty).ToString();
                    collect.Rows.Add("", getsubgroupname, plantqty, IssuedQty, Avilstock);
                }
                DTG.Tables[datasetcount].Clear();
                datasetcount = 0;
            }


            if (collect.Rows.Count > 0)
            {
                GridView1.DataSource = collect;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = collect;

            }
        }
        catch
        {


        }

    }

    public void gettingstockgroupdetails()
    {
        try
        {
            con = DB.GetConnection();
            string stt = "";
             stt = "select ssStockSubGroup,plantqty,IssuedQty,itemrate,AvailStock  from (select  StockGroup,StockGroupID,ssStockSubGroup,StockSubgroup,isnull(QTY,0)  as plantqty,isnull(IssuedQty,0) as IssuedQty ,Isnull((QTY - IssuedQty),0) as AvailStock    from (select StockGroup,dd.ssStockSubGroup,isnull(sUM(Qty),0) AS QTY,jj.StockSubgroup,StockGroupID   from (Select     StockGroup , isnull(SUM(Qty),0) as Qty, CONVERT(varchar, AddedDate, 100) as Date,Plant_Code,StockSubgroup  from PlantStockMaster where  PLANT_CODE='" + pcode + "' AND stockgroup='" + getgroupname + "'  group by StockGroup,AddedDate,stockcode,Plant_Code,StockSubgroup ) as jj left join	(Select    StockGroupID,StockSubGroupID,StockSubGroup as ssStockSubGroup   from Stock_Master  where StockGroup='" + getgroupname + "') as dd on jj.StockSubgroup=dd.StockSubGroupID  GROUP BY  StockGroup,JJ.StockSubgroup,dd.ssStockSubGroup,StockGroupID  ) as plantstocklist left join (select    Dm_StockGroupId as IssStockGroupId,Dm_StockSubGroupId as IssStockSubGroupId,ISNULL( sUM(Dm_Quantity),0) AS IssuedQty  from  DeductionDetails_Master where  Dm_Plantcode='" + pcode + "'    GROUP BY Dm_StockGroupId,Dm_StockSubGroupId) as  issstock on plantstocklist.StockGroupID=issstock.IssStockGroupId and plantstocklist.StockSubgroup=issstock.IssStockSubGroupId) as leftside left join (Select    Stock_Type,Stock_category,Sum(ItemRate) as itemrate   from StockRateSetting      where Plant_Code='" + pcode + "' and Fixstatus=1   group by  Stock_Type,Stock_category) as rightside on leftside.StockGroupID=rightside.Stock_Type and leftside.StockSubgroup=rightside.Stock_category";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(DTG, ("stockDETAILS"));
            datasetcount = datasetcount + 1;
        }
        catch
        {
        }
    }

    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        getdata();
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Plant Stock Availability For--" +  ddl_Plantname.SelectedItem.Text;
            HeaderCell2.ColumnSpan = 6;
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

                string getstring = e.Row.Cells[2].Text;

                if (getstring == "&nbsp;")
                {

                    e.Row.BackColor = System.Drawing.Color.Cyan;
                }

                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;

            }
        }
        catch
        {


        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button9_Click(object sender, EventArgs e)
    {

    }
}