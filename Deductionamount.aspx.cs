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
using System.Drawing;
using System.IO;

public partial class Deductionamount : System.Web.UI.Page
{

	public string ccode;
	public string pcode;
	public string pname;
	public string cname;
	public string frmdate;
	public string todate;
	string getrouteusingagent;
	string routeid;
	string dt1;
	string dt2;
	string ddate;
	DateTime dtm = new DateTime();
	BLLuser Bllusers = new BLLuser();
	string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
	string agentname;

	string getfrmdate;
	string gettodate;
	string comamt;
	string milkltr;
	string dpumilkltr;
	string dpuampount;
	string plantmilkltr;
	string plantamount;
	double plantamount1 = 0;
	double plantamount2;

	int countdeductname;
	string[] getstr;
	string[] itemcol;
	string[] getdate;
	DataTable dt = new DataTable();
	int i = 0;
	string s;
	string s1;
	string s3;
	int countgridrows;
	int countgridrows1;
	double sum;
	double sum1;
	double sum2;
	double sum3;
	int gridcol;
	string[] arrayname;
	int r = 1;
	int r1 = 1;
	int sno = 1;
	string getna1 = "";
	string[] aa;
	string var1;
	string var2;
	string item;
    public static int roleid;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
            if ((Session["Name"] != null) && (Session["pass"] != null))
			{

				ccode = Session["Company_code"].ToString();
				//    pcode = Session["Plant_Code"].ToString();
				cname = Session["cname"].ToString();
				pname = Session["pname"].ToString();
				dtm = System.DateTime.Now;
				txt_FromDate.Text = dtm.ToShortDateString();
				txt_ToDate.Text = dtm.ToShortDateString();
				txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
				txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                roleid = Convert.ToInt32(Session["Role"].ToString());
				//  gettid();
                if ((roleid >= 3) && (roleid != 9))
                {
                    LoadPlantcode();
                }
                if (roleid == 9)
                {
                    loadspecialsingleplant();
                    Session["Plant_Code"] = "170".ToString();
                    pcode = "170";
                }
				gridview1();
			}

			else
			{


				pcode = ddl_Plantcode.SelectedItem.Value;


			}

		}
		else
		{


			pcode = ddl_Plantcode.SelectedItem.Value;
			//  LoadPlantcode();
			//   ddl_agentcode.Text = getrouteusingagent;
			//	gridview();
		}

	}
	protected void btn_ok_Click(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		pcode = ddl_Plantcode.SelectedItem.Value;
		Label7.Visible = false;
		gridview1();
		Getrecoveryname();
		//BIlldate();
	}
	public void gridview1()
	{

		try
		{
			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");

			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
				string sqlstr = "select (Fdate + '_'+ Tdate) as Date   from (select  CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and Dm_FrmDate between '" + d1 + "' and '" + d2 + "'  group by  Dm_FrmDate,Dm_ToDate) as fd";
				SqlCommand COM = new SqlCommand(sqlstr, conn);
				SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
				sqlDa.Fill(dt);

			}
		}
		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}
	}
	private void catchmsg(string ex1)
	{

		Label7.ForeColor = System.Drawing.Color.Red;
		Label7.Text = ex1.ToString().Trim();
		Label7.Visible = true;


	}
	private void LoadPlantcode()
	{
		try
		{
			SqlDataReader dr = null;
			ddl_Plantcode.Items.Clear();
			ddl_Plantname.Items.Clear();
			dr = Bllusers.LoadPlantcode(ccode);
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

			catchmsg(ex.ToString());

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

	protected void ddl_PlantName_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		pcode = ddl_Plantcode.SelectedItem.Value;
		//BIlldate();
		//	gridview1();
	}
	protected void btn_export_Click(object sender, EventArgs e)
	{

		try
		{
			Response.Clear();
			Response.Buffer = true;
			string filename = "'" + ddl_Plantname.SelectedItem.Text + "' " + DateTime.Now.ToString() + ".xls";

			Response.AddHeader("content-disposition", "attachment;filename=" + filename);
			// Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
			Response.Charset = "";
			Response.ContentType = "application/vnd.ms-excel";
			using (StringWriter sw = new StringWriter())
			{
				HtmlTextWriter hw = new HtmlTextWriter(sw);

				//To Export all pages
				GridView2.AllowPaging = false;
			//	gridview1();
				Getrecoveryname();


				//GridView2.HeaderRow.BackColor = Color.White;
				//foreach (TableCell cell in GridView2.HeaderRow.Cells)
				//{
				//    cell.BackColor = GridView2.HeaderStyle.BackColor;
				//}
				//foreach (GridViewRow row in GridView2.Rows)
				//{
				//    row.BackColor = Color.White;
				//    foreach (TableCell cell in row.Cells)
				//    {
				//        if (row.RowIndex % 2 == 0)
				//        {
				//            cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
				//        }
				//        else
				//        {
				//            cell.BackColor = GridView2.RowStyle.BackColor;
				//        }
				//        cell.CssClass = "textmode";
				//    }
				//}

				GridView2.RenderControl(hw);

				//style to format numbers to string
				//string style = @"<style> td { mso-number-format:""" + "\\@" + @"""; }</style>";
				//// string style = @"<style> .textmode { } </style>";
				//Response.Write(style);
				//Response.Output.Write(sw.ToString());
				Response.Flush();
				Response.End();
			}
		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}

	}
	public override void VerifyRenderingInServerForm(Control control)
	{
		/* Verifies that the control is rendered */
	}
	protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
	{

		try
		{

			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (r == 1)
				{
					sum = 0;
					sum1 = 0;
					sum2 = 0;


				}

				if (r1 == 2)
{

	sum3 = 0;


}



				countgridrows = GridView2.HeaderRow.Cells.Count;

				countgridrows1 = countgridrows - 1;



				for (int j = 1; j < countgridrows1; j++)
				{


					if (j == 1)
					{
						//s = GridView2.HeaderRow.Cells[j].Text.ToString();
						s = e.Row.Cells[j].Text;


						//string assigndate = e.Row.Cells[1].Text;
						//string checkml = e.Row.Cells[2].Text;
						if (s != "")
						{

							getdate = s.Split('_');  //01/02/2016_10/02/2016
							string valu = getdate[0].Substring(0, 2);
							string valu1 = getdate[0].Substring(3, 2); //01
							string valu2 = getdate[0].Substring(6, 4);  //
							string valu3 = getdate[1].Substring(0, 2);
							string valu4 = getdate[1].Substring(3, 2);
							string valu5 = getdate[1].Substring(6, 4);

							getfrmdate = valu1 + "/" + valu + "/" + valu2;
							gettodate = valu4 + "/" + valu3 + "/" + valu5;
							//	 getagentprocdetails(getfrmdate, gettodate);

						}


					}
					else
					{

						s1 = GridView2.HeaderRow.Cells[j].Text.ToString();

						if (r1 == 1)
						{
							s3 = s1;
						}

						r1 = r1 + 1;
						//	getstockamount(getfrmdate, gettodate, s1);


						SqlConnection con = new SqlConnection(connStr);
						string str = "";
						//	str = "select  AMt  as Amount from (select (Fdate + '_'+ Tdate) as Date ,StockSubGroup,AMt from (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and Dm_FrmDate between '" + getfrmdate + "' and '" + gettodate + "''   group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate  ) as dm left join ( SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master where StockSubGroup='" + s1 + "' group by StockGroupID,StockSubGroupID,StockSubGroup) AS SM ON  DM.Dm_StockGroupId=SM.StockGroupID AND DM.Dm_StockSubGroupId=SM.StockSubGroupID) as aa";

						str = "select   convert(decimal(18,2),sum((AMT))) as Amount from (SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master where StockSubGroup='" + s1 + "' group by StockGroupID,StockSubGroupID,StockSubGroup) as ll  left join (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and Dm_FrmDate between '" + getfrmdate + "' and '" + gettodate + "'   group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate) as akk   on ll.StockSubGroupID=akk.Dm_StockSubGroupId and ll.StockGroupID =akk.Dm_StockGroupId   ";
						con.Open();
						SqlCommand cmd = new SqlCommand(str, con);
						SqlDataReader dr = cmd.ExecuteReader();
						if (dr.HasRows)
						{

							while (dr.Read())
							{



								plantamount = dr["Amount"].ToString();
								if (plantamount != string.Empty)
								{
									if (r == 1)
									{
										plantamount1 = Convert.ToDouble(plantamount);
										plantamount2 = plantamount2 + plantamount1;
									//	GridView2.FooterRow.Cells[j].Text = plantamount2.ToString();
										//	GridView1.FooterRow.Cells[0].Text = total.ToString();


									}
								}


								r = r + 1;



							}
						}









						if (plantamount != string.Empty)
						{
							e.Row.Cells[j].Text = plantamount.ToString();
							e.Row.Cells[j].HorizontalAlign = HorizontalAlign.Right;
							sum = sum + Convert.ToDouble(e.Row.Cells[j].Text);

						


						}


					}





				}
				//	sum1 = sum1 + sum;

				//e.Row.Cells[countgridrows1].Text = sum.ToString() + ".00";
				//e.Row.Cells[countgridrows1].ForeColor = System.Drawing.Color.Red;
				//e.Row.Cells[countgridrows1].Font.Bold = true;
				//e.Row.Cells[countgridrows1].HorizontalAlign = HorizontalAlign.Right;

				//sum2 = sum2 + sum;

				e.Row.Cells[countgridrows1].Text = sum.ToString() + ".00";
				e.Row.Cells[countgridrows1].ForeColor = System.Drawing.Color.Red;
				e.Row.Cells[countgridrows1].Font.Bold = true;
				e.Row.Cells[countgridrows1].HorizontalAlign = HorizontalAlign.Right;

				sum2 = sum2 + sum;

				
				sno = sno + 1;
				sum3 = sum3 + sum;


			}
			r = 1;
		//	sum3 = sum3 + sum;

			r1 = r1 +  1;

			GridViewRow gvRow = e.Row;
			if (gvRow.RowType == DataControlRowType.Header)
			{




				GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
				TableCell cell0 = new TableCell();
				cell0.Text = "Plant Name:" + ddl_Plantname.SelectedItem.Text;
				cell0.HorizontalAlign = HorizontalAlign.Center;
				int coo = dt.Columns.Count + 1;
				cell0.ColumnSpan = coo;
				gvrow.Cells.Add(cell0);
				GridView2.Controls[0].Controls.AddAt(0, gvrow);



			}


		}

		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}




	}



	//    public void BIlldate()
	//    {

	//try
	//{

	//        DateTime dt1 = new DateTime();
	//        DateTime dt2 = new DateTime();
	//        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
	//        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

	//        string d1 = dt1.ToString("MM/dd/yyyy");
	//        string d2 = dt2.ToString("MM/dd/yyyy");

	//        SqlConnection con = new SqlConnection(connStr);
	//        string str = "";
	//        //string sqlstr = "select (FrmDate + ' ' + ToDate) as BillDate  from (select CONVERT(varchar,Bill_frmdate,103) as FrmDate  from Bill_date where Plant_Code='" + pcode + "'  and Bill_frmdate between '" + d1 + "' and '" + d2 + "'  group by Bill_frmdate";
	//        string sqlstr = "select (FrmDate + ' ' + ToDate) as BillDate  from (select CONVERT(varchar,Bill_frmdate,103) as FrmDate,CONVERT(varchar,Bill_todate,103) as ToDate,Plant_Code  from Bill_date where Plant_Code='" + pcode + "'  and Bill_frmdate between '" + d1 + "' and '" + d2 + "'  group by Bill_frmdate,Bill_todate,Plant_Code) as hh";
	//        con.Open();
	//        SqlCommand cmd = new SqlCommand(sqlstr, con);
	//        SqlDataReader dr = cmd.ExecuteReader();
	//        if (dr.HasRows)
	//        {

	//            while (dr.Read())
	//            {

	//                plantmilkltr = dr["BillDate"].ToString();



	//            }

	//            string assigndate = plantmilkltr;
	//            string checkml = assigndate;
	//            if (checkml != "")
	//            {

	//                getdate = assigndate.Split('_');  //01/02/2016_10/02/2016
	//                string valu = getdate[0].Substring(0, 2);
	//                string valu1 = getdate[0].Substring(3, 2); //01
	//                string valu2 = getdate[0].Substring(6, 4);  //
	//                string valu3 = getdate[0].Substring(11, 2);
	//                string valu4 = getdate[0].Substring(14, 2);
	//                string valu5 = getdate[0].Substring(17, 4);

	//                getfrmdate = valu1 + "/" + valu + "/" + valu2;
	//                gettodate = valu4 + "/" + valu3 + "/" + valu5;
	//                //	getplantmilkandamount(getfrmdate, gettodate);


	//                gridview1();

	//            }
	//        }

	//}

	//catch (Exception ex)
	//{

	//    catchmsg(ex.ToString());

	//}


	//    }

	//    public void getstockamount(string getfrmdate, string gettodate, string s1)
	//    {

	//try
	//{
	//        SqlConnection con = new SqlConnection(connStr);
	//        string str = "";
	//        //	str = "select  AMt  as Amount from (select (Fdate + '_'+ Tdate) as Date ,StockSubGroup,AMt from (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and Dm_FrmDate between '" + getfrmdate + "' and '" + gettodate + "''   group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate  ) as dm left join ( SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master where StockSubGroup='" + s1 + "' group by StockGroupID,StockSubGroupID,StockSubGroup) AS SM ON  DM.Dm_StockGroupId=SM.StockGroupID AND DM.Dm_StockSubGroupId=SM.StockSubGroupID) as aa";

	//        str = "select   convert(decimal(18,2),sum((AMT))) as Amount from (SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master where StockSubGroup='" + s1 + "' group by StockGroupID,StockSubGroupID,StockSubGroup) as ll  left join (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and Dm_FrmDate between '" + getfrmdate + "' and '" + gettodate + "'   group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate) as akk   on ll.StockSubGroupID=akk.Dm_StockSubGroupId and ll.StockGroupID =akk.Dm_StockGroupId   ";
	//        con.Open();
	//        SqlCommand cmd = new SqlCommand(str, con);
	//        SqlDataReader dr = cmd.ExecuteReader();
	//        if (dr.HasRows)
	//        {

	//            while (dr.Read())
	//            {


	//                plantamount = dr["Amount"].ToString();



	//            }
	//        }

	//}

	//catch (Exception ex)
	//{

	//    catchmsg(ex.ToString());

	//}



	//    }


	public void Getrecoveryname()
	{

		try
		{

			DateTime dt1 = new DateTime();
			DateTime dt2 = new DateTime();
			dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
			dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

			string d1 = dt1.ToString("MM/dd/yyyy");
			string d2 = dt2.ToString("MM/dd/yyyy");

			string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
			//	string str = "select distinct(name)  from (select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY date ORDER BY date desc) = 1   THEN Date ELSE NULL END AS 'Date',StockSubGroup as name,AMt  from (select (Fdate + '_'+ Tdate) as Date ,StockSubGroup,AMt from (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and Dm_FrmDate between '" + d1 + "' and '" + d2 + "'  group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate  ) as dm left join ( SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master group by StockGroupID,StockSubGroupID,StockSubGroup) AS SM ON  DM.Dm_StockGroupId=SM.StockGroupID AND DM.Dm_StockSubGroupId=SM.StockSubGroupID) as aa) as akl";
				string str = "select distinct(name)  from (select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY date ORDER BY date desc) = 1   THEN Date ELSE NULL END AS 'Date',StockSubGroup as name,AMt  from (select (Fdate + '_'+ Tdate) as Date ,StockSubGroup,AMt from (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and ((Dm_FrmDate between  '" + d1 + "' and '" + d2 + "') or (Dm_ToDate  between  '" + d1 + "' and '" + d2 + "'))  group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate  ) as dm left join ( SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master group by StockGroupID,StockSubGroupID,StockSubGroup) AS SM ON  DM.Dm_StockGroupId=SM.StockGroupID AND DM.Dm_StockSubGroupId=SM.StockSubGroupID) as aa) as akl";
				SqlCommand COM = new SqlCommand(str, conn);
				//   SqlDataReader DR = COM.ExecuteReader();
				DataTable getdeducname = new DataTable();
				SqlDataAdapter sqlDa = new SqlDataAdapter(COM);
				sqlDa.Fill(getdeducname);
				if (getdeducname.Rows.Count > 0)
				{
					countdeductname = getdeducname.Rows.Count;
					int l = 0;
					foreach (DataRow row in getdeducname.Rows)
					{


						string value = row[0].ToString();
						getstr = value.Split('-');
						string getna = getstr[i];
						dt.Columns.Add(getna);

						getna1 = getna1 + "/" + getna;

						aa = getna1.Split('/');


					}

					string geting = "Total";
					dt.Columns.Add(geting);
					GridView2.DataSource = dt;
					GridView2.DataBind();

					GridView2.FooterRow.Cells[countgridrows1].Text = sum3.ToString() + ".00";

					int jj = 2;
					for (int ii = 1; ii < aa.Length; ii++)
					{

						item = aa[ii];


						getfootertot();
						GridView2.FooterRow.Cells[1].Text = "Total";
						GridView2.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
						GridView2.FooterRow.Cells[jj].Text = var2;
						GridView2.FooterRow.Cells[jj].HorizontalAlign = HorizontalAlign.Right;
						jj = jj + 1;

						// work with item here
					}

					//for (int h = 2; h < countgridrows1; h++)
					//{


					//}
				}
				else
				{
					GridView2.DataSource = null;
					GridView2.DataBind();

				}
			}



		}


		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}

	}



	public void getfootertot()
	{

		try
		{

			using (SqlConnection conn = new SqlConnection(connStr))
			{

				DateTime dt1 = new DateTime();
				DateTime dt2 = new DateTime();
				dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
				dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

				string d1 = dt1.ToString("MM/dd/yyyy");
				string d2 = dt2.ToString("MM/dd/yyyy");
				conn.Open();
				string qry = "select *   from (select distinct(name),sum(Amt) as amt  from (select  CASE WHEN ROW_NUMBER() OVER(PARTITION BY date ORDER BY date desc) = 1   THEN Date ELSE NULL END AS 'Date',StockSubGroup as name,AMt  from (select (Fdate + '_'+ Tdate) as Date ,StockSubGroup,AMt from (select   Dm_StockGroupId,Dm_StockSubGroupId,SUM(Dm_Amount) AS AMT,Dm_Plantcode,CONVERT(varchar,Dm_FrmDate,103) as Fdate,CONVERT(varchar,Dm_ToDate,103) as Tdate     from   DeductionDetails_Master  where Dm_Plantcode='" + pcode + "' and ((Dm_FrmDate between  '" + d1 + "' and '" + d2 + "') or (Dm_ToDate  between  '" + d1 + "' and '" + d2 + "'))  group by  Dm_StockGroupId,Dm_StockSubGroupId,Dm_Plantcode,Dm_FrmDate,Dm_ToDate  ) as dm left join ( SELECT StockGroupID,StockSubGroupID,StockSubGroup   FROM      Stock_Master  where StockSubGroup='" + item + "' group by StockGroupID,StockSubGroupID,StockSubGroup) AS SM ON  DM.Dm_StockGroupId=SM.StockGroupID AND DM.Dm_StockSubGroupId=SM.StockSubGroupID) as aa) as akl    group by name   ) as bb   where name is not null";
				SqlCommand cmd = new SqlCommand(qry, conn);
				SqlDataReader dr = cmd.ExecuteReader();
				if (dr.HasRows)
				{

					while (dr.Read())
					{

						//string var1 = dr["name"].ToString();
						var2 = dr["Amt"].ToString();




					}

				}


			}



		}


		catch (Exception ex)
		{

			catchmsg(ex.ToString());

		}

	}



}