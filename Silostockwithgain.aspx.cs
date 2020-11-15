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

public partial class Silostockwithgain : System.Web.UI.Page
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
	int i = 0;
	int j = 0;
	int K = 0;
	int L = 0;
	int M = 0;
	int N = 0;
	double SUMOPPRO = 0;
	double SUMOPPRO1 = 0;
	double ASSIGNVALUE;
	int JK;
	int I;
	int JHG = 0;
	int z = 0;
	double PROSUMKG = 0;
	double PROAVGFAT = 0;
	int COUNTI = 0;
	double PROAVGSNF = 0;
	int SNFCOUNTI = 0;
	double TOTDIS = 0;
	double DESSPAT = 0;
	int DISCOUNFAT = 0;
	int DISCOUNSNF = 0;
	double DESSPSNF = 0;
	double GAINKG;
	int GAINFATCOUNT;
	double GAINFATAVG;
	int GAINSNFCOUNT;
	double GAINSNFAVG;
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
                    dtm = System.DateTime.Now;
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
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
                    DateTime date = txtMyDate.AddDays(-1);
                    string datee = date.ToString("MM/dd/yyyy");
                    Button2.Visible = false;
                }
                else
                {
                }
            }
            else
            {
                ccode = Session["Company_code"].ToString();
                pcode = ddl_Plantcode.SelectedItem.Value;
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
            string str = "";
            con = DB.GetConnection();
            if (pcode == "171")
            {
                pcode = "168";
            }
            str = "select  convert(varchar,Date,103) as Date from Stock_Milk  where plant_code='" + pcode + "' and date  between '" + d1 + "' and '" + d2 + "'  ORDER by Date asc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dt.Columns.Add("OpStock");
            dt.Columns.Add("Fat");
            dt.Columns.Add("Snf");
            dt.Columns.Add("FKg");
            dt.Columns.Add("SKg");
            dt.Columns.Add("ProAM");
            dt.Columns.Add("ProPM");
            dt.Columns.Add("TotPro");
            dt.Columns.Add("PFat");
            dt.Columns.Add("PSnf");
            dt.Columns.Add("PFKg");
            dt.Columns.Add("PSKg");
            dt.Columns.Add("Desp");
            dt.Columns.Add("DFat");
            dt.Columns.Add("DSnf");
            dt.Columns.Add("DFKg");
            dt.Columns.Add("DSKg");
            dt.Columns.Add("Close");
            dt.Columns.Add("CFat");
            dt.Columns.Add("CSnf");
            dt.Columns.Add("CFKg");
            dt.Columns.Add("CSKg");
            dt.Columns.Add("ActKg");
            dt.Columns.Add("GainKg");
            dt.Columns.Add("GainFat");
            dt.Columns.Add("GainSnf");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            foreach (DataRow row in dt.Rows)
            {
                string get = GridView1.Rows[i].Cells[1].Text;
                DateTime txtMyDate = DateTime.ParseExact(get, "dd/MM/yyyy", null);
                DateTime date = txtMyDate.AddDays(-1);
                string datee = date.ToString("MM/dd/yyyy");
                str = "select   sum(milkkg) as kg,SUM(fat) as Fat,SUM(snf) as Snf from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee + "' ";
                con = DB.GetConnection();
                SqlCommand cmd1 = new SqlCommand(str, con);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        GridView1.Rows[i].Cells[2].Text = dr["kg"].ToString();
                        double kg = Convert.ToDouble(dr["kg"].ToString());
                        GridView1.Rows[i].Cells[3].Text = dr["Fat"].ToString();
                        double fat = Convert.ToDouble(dr["Fat"].ToString());
                        GridView1.Rows[i].Cells[4].Text = dr["Snf"].ToString();
                        double snf = Convert.ToDouble(dr["Snf"].ToString());
                        string fatkg = (kg * fat / 100).ToString("F2");
                        string snfkg = (kg * snf / 100).ToString("F2");
                        GridView1.Rows[i].Cells[5].Text = fatkg;
                        GridView1.Rows[i].Cells[6].Text = snfkg;
                    }
                }
                i = i + 1;
            }
            foreach (DataRow row in dt.Rows)
            {
                string get = GridView1.Rows[j].Cells[1].Text;
                DateTime txtMyDate = DateTime.ParseExact(get, "dd/MM/yyyy", null);
                DateTime date = txtMyDate;
                string datee = date.ToString("MM/dd/yyyy");
                string sss;
                if (pcode == "168")
                {
                    sss = "select isnull(sum(milk_kg),0) as  prMilk    from procurementimport  where plant_code IN (168,171) and prdatE='" + datee + "' AND SESSIONS='AM'";
                }
                else
                {
                    sss = "select     isnull(sum(milk_kg),0) as  prMilk    from procurementimport  where plant_code='" + pcode + "'  and prdatE='" + datee + "' AND SESSIONS='AM'";
                }
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd1 = new SqlCommand(sss, con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            GridView1.Rows[j].Cells[7].Text = dr1["prMilk"].ToString();
                        }
                    }
                }
                j = j + 1;
                string get1 = GridView1.Rows[K].Cells[1].Text;
                DateTime txtMyDate1 = DateTime.ParseExact(get1, "dd/MM/yyyy", null);
                DateTime date1 = txtMyDate;
                string datee1 = date.ToString("MM/dd/yyyy");
                string sss1;
                if (pcode == "168")
                {
                    sss1 = "select isnull(sum(milk_kg),0) as  prMilk    from procurementimport  where plant_code IN (168,171) and prdatE='" + datee + "' AND SESSIONS='PM'";
                }
                else
                {
                    sss1 = "select isnull(sum(milk_kg),0) as  prMilk    from procuremenTimport  where plant_code='" + pcode + "'  and prdatE='" + datee + "' AND SESSIONS='PM'";
                }
                
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd11 = new SqlCommand(sss1, con);
                    SqlDataReader dr11 = cmd11.ExecuteReader();
                    if (dr11.HasRows)
                    {
                        while (dr11.Read())
                        {
                            GridView1.Rows[K].Cells[8].Text = dr11["prMilk"].ToString();
                        }
                    }
                }

                K = K + 1;
                string totpro = GridView1.Rows[N].Cells[1].Text;
                DateTime txtMyDatepro = DateTime.ParseExact(totpro, "dd/MM/yyyy", null);
                DateTime datepro = txtMyDate;
                string datee1pro = date.ToString("MM/dd/yyyy");
                string sss1pro;

                if (pcode == "168")
                {
                    sss1pro = "select     isnull(sum(milk_kg),0) as  prMilk, CONVERT(decimal(18,2),AVG(fat)) as Fat,CONVERT(decimal(18,2),AVG(snf)) as  snf    from procuremenTimport  where plant_code IN (168, 171) and prdatE='" + datee + "'";
                }
                else
                {
                    sss1pro = "select     isnull(sum(milk_kg),0) as  prMilk, CONVERT(decimal(18,2),AVG(fat)) as Fat,CONVERT(decimal(18,2),AVG(snf)) as  snf    from procuremenTimport  where plant_code='" + pcode + "'  and prdatE='" + datee + "'";
                }
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd11pro = new SqlCommand(sss1pro, con);
                    SqlDataReader dr11pro = cmd11pro.ExecuteReader();
                    if (dr11pro.HasRows)
                    {
                        while (dr11pro.Read())
                        {
                            double pro = Convert.ToDouble(dr11pro["prMilk"].ToString());
                            GridView1.Rows[N].Cells[9].Text = pro.ToString("F2");
                            double kg = Convert.ToDouble(dr11pro["prMilk"].ToString());
                            double fat = Convert.ToDouble(dr11pro["Fat"].ToString());
                            double snf = Convert.ToDouble(dr11pro["Snf"].ToString());
                            GridView1.Rows[N].Cells[10].Text = fat.ToString();
                            GridView1.Rows[N].Cells[11].Text = snf.ToString();
                            string fatkg = (kg * fat / 100).ToString("F2");
                            string snfkg = (kg * snf / 100).ToString("F2");
                            GridView1.Rows[N].Cells[12].Text = fatkg;
                            GridView1.Rows[N].Cells[13].Text = snfkg;
                        }
                    }
                }
                N = N + 1;
                string get2 = GridView1.Rows[L].Cells[1].Text;
                DateTime txtMyDate2 = DateTime.ParseExact(get2, "dd/MM/yyyy", null);
                DateTime date2 = txtMyDate;
                string datee2 = date.ToString("MM/dd/yyyy");
                string sss2;
                sss2 = " SELECT  DesMilk,ISNULL(fat,0) AS Fat,ISNULL(snf,0) AS snf  FROM (select  isnull(sum(milkkg),0) as  DESMilk,CONVERT(decimal(18,2),AVG(fat)) as fat,CONVERT(decimal(18,2),AVG(snf)) as snf      from DespatchEntry where plant_code='" + pcode + "'  and date='" + datee2 + "') AS DD";
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd2 = new SqlCommand(sss2, con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {

                            GridView1.Rows[L].Cells[14].Text = dr2["DesMilk"].ToString();
                            double kg = Convert.ToDouble(dr2["DesMilk"].ToString());
                            double fat = Convert.ToDouble(dr2["Fat"].ToString());
                            double snf = Convert.ToDouble(dr2["Snf"].ToString());
                            GridView1.Rows[L].Cells[15].Text = fat.ToString();
                            GridView1.Rows[L].Cells[16].Text = snf.ToString();
                            string fatkg = (kg * fat / 100).ToString("F2");
                            string snfkg = (kg * snf / 100).ToString("F2");
                            GridView1.Rows[L].Cells[17].Text = fatkg;
                            GridView1.Rows[L].Cells[18].Text = snfkg;
                        }
                    }
                }
                L = L + 1;
                string get21 = GridView1.Rows[M].Cells[1].Text;
                DateTime txtMyDate21 = DateTime.ParseExact(get2, "dd/MM/yyyy", null);
                DateTime date21 = txtMyDate;
                string datee21 = date.ToString("MM/dd/yyyy");
                string sss21;
                sss21 = "select   sum(milkkg) as kg,CONVERT(decimal(18,2),AVG(FAT)) AS FAT,CONVERT(decimal(18,2),AVG(SNF)) AS SNF    from Stock_Milk  where plant_code='" + pcode + "' and date ='" + datee21 + "' ";
                using (con = DB.GetConnection())
                {
                    SqlCommand cmd21 = new SqlCommand(sss21, con);
                    SqlDataReader dr21 = cmd21.ExecuteReader();
                    if (dr21.HasRows)
                    {
                        while (dr21.Read())
                        {
                            GridView1.Rows[M].Cells[19].Text = dr21["kg"].ToString();
                            double kg = Convert.ToDouble(dr21["kg"].ToString());
                            double fat = Convert.ToDouble(dr21["Fat"].ToString());
                            double snf = Convert.ToDouble(dr21["Snf"].ToString());
                            GridView1.Rows[M].Cells[20].Text = fat.ToString();
                            GridView1.Rows[M].Cells[21].Text = snf.ToString();
                            string fatkg = (kg * fat / 100).ToString("F2");
                            string snfkg = (kg * snf / 100).ToString("F2");
                            GridView1.Rows[M].Cells[22].Text = fatkg;
                            GridView1.Rows[M].Cells[23].Text = snfkg;
                        }
                    }
                }
                M = M + 1;
            }
            int HHH = GridView1.HeaderRow.Cells.Count;
            int HH = GridView1.Rows.Count;
            foreach (GridViewRow DT in GridView1.Rows)
            {

                double get = Convert.ToDouble(GridView1.Rows[z].Cells[2].Text);
                double get1 = Convert.ToDouble(GridView1.Rows[z].Cells[9].Text);
                double get2 = Convert.ToDouble(GridView1.Rows[z].Cells[14].Text);

                double gerres = (get + get1) - get2;
                double CLO = Convert.ToDouble(GridView1.Rows[z].Cells[19].Text);
                double RES = CLO - gerres;
                GridView1.Rows[z].Cells[24].Text = gerres.ToString("F2");
                GridView1.Rows[z].Cells[25].Text = RES.ToString("F2");
                double FATKOP = Convert.ToDouble(GridView1.Rows[z].Cells[5].Text);
                double FATKPR = Convert.ToDouble(GridView1.Rows[z].Cells[12].Text);
                double FATKDES = Convert.ToDouble(GridView1.Rows[z].Cells[17].Text);
                double FATKCLO = Convert.ToDouble(GridView1.Rows[z].Cells[22].Text);
                double ADDFATKG = (FATKOP + FATKPR) - (FATKDES + FATKCLO);

                double ADDFATKG1 = ADDFATKG * (-1);
                GridView1.Rows[z].Cells[26].Text = ADDFATKG1.ToString("F2");

                double SNFKOP = Convert.ToDouble(GridView1.Rows[z].Cells[6].Text);
                double SNFKPR = Convert.ToDouble(GridView1.Rows[z].Cells[13].Text);
                double SNFKDES = Convert.ToDouble(GridView1.Rows[z].Cells[18].Text);
                double SNFKCLO = Convert.ToDouble(GridView1.Rows[z].Cells[23].Text);
                double ADDSNFKG = (SNFKOP + SNFKPR) - (SNFKDES + SNFKCLO);
                double ADDSNFKG1 = ADDSNFKG * (-1);

                GridView1.Rows[z].Cells[27].Text = ADDSNFKG1.ToString("F2");



                double GETPRKG = Convert.ToDouble(GridView1.Rows[z].Cells[9].Text);
                PROSUMKG = PROSUMKG + GETPRKG;

                //decimal milkkg = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotPro"));
                GridView1.Rows[z].Cells[9].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[9].Text = PROSUMKG.ToString("0.00");


                double PROFAT = Convert.ToDouble(GridView1.Rows[z].Cells[10].Text);

                double PROSNF = Convert.ToDouble(GridView1.Rows[z].Cells[11].Text);



                if (PROFAT > 1)
                {
                    COUNTI = COUNTI + 1;
                    PROAVGFAT = PROAVGFAT + PROFAT;



                }
                double GETTOOPROFAT = PROAVGFAT / COUNTI;
                GridView1.Rows[z].Cells[10].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[10].Text = GETTOOPROFAT.ToString("0.00");



                if (PROSNF > 1)
                {
                    SNFCOUNTI = SNFCOUNTI + 1;
                    PROAVGSNF = PROAVGSNF + PROSNF;



                }
                double GETTOOPROSNF = PROAVGSNF / SNFCOUNTI;
                GridView1.Rows[z].Cells[11].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[11].Text = GETTOOPROSNF.ToString("0.00");



                double TOTTALDESPATCH = Convert.ToDouble(GridView1.Rows[z].Cells[14].Text);

                TOTDIS = TOTDIS + TOTTALDESPATCH;
                GridView1.Rows[z].Cells[14].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[14].Text = TOTDIS.ToString("0.00");


                double DESPARCHPRO = Convert.ToDouble(GridView1.Rows[z].Cells[15].Text);
                if (DESPARCHPRO > 1)
                {
                    DISCOUNFAT = DISCOUNFAT + 1;
                    DESSPAT = DESSPAT + DESPARCHPRO;



                }
                double DESPATCHFA = DESSPAT / DISCOUNFAT;
                GridView1.Rows[z].Cells[15].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[15].Text = DESPATCHFA.ToString("0.00");



                double DESPARCHSNF = Convert.ToDouble(GridView1.Rows[z].Cells[16].Text);
                if (DESPARCHSNF > 1)
                {
                    DISCOUNSNF = DISCOUNSNF + 1;
                    DESSPSNF = DESSPSNF + DESPARCHSNF;



                }
                double DESPATCHSNF = DESSPSNF / DISCOUNSNF;
                GridView1.Rows[z].Cells[16].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[16].Text = DESPATCHSNF.ToString("0.00");




                double GAINKGING = Convert.ToDouble(GridView1.Rows[z].Cells[25].Text);


                GAINKG = GAINKG + GAINKGING;
                GridView1.Rows[z].Cells[25].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[25].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[25].Text = GAINKG.ToString("0.00");



                double GAIINFAT = Convert.ToDouble(GridView1.Rows[z].Cells[26].Text);
                if (GAIINFAT > 1)
                {
                    GAINFATCOUNT = GAINFATCOUNT + 1;
                    GAINFATAVG = GAINFATAVG + GAIINFAT;



                }

                double GAINFATT = GAINFATAVG / GAINFATCOUNT;
                GridView1.Rows[z].Cells[26].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[26].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[26].Text = GAINFATT.ToString("0.00");




                double GAIINSNF = Convert.ToDouble(GridView1.Rows[z].Cells[27].Text);
                if (GAIINSNF > 1)
                {
                    GAINSNFCOUNT = GAINSNFCOUNT + 1;
                    GAINSNFAVG = GAINSNFAVG + GAIINSNF;



                }

                double GAIINSNFF = GAINSNFAVG / GAINSNFCOUNT;
                GridView1.Rows[z].Cells[27].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[27].HorizontalAlign = HorizontalAlign.Right;
                GridView1.FooterRow.Cells[27].Text = GAIINSNFF.ToString("0.00");

                z = z + 1;





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
	protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
	{
		ddl_Plantcode.SelectedIndex = ddl_Plantname.SelectedIndex;
		pcode = ddl_Plantcode.SelectedItem.Value;
	}
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = ddl_Plantname.Text + ":Stock Detils:" + "From:" + txt_FromDate.Text + "To:" + txt_ToDate.Text;
                HeaderCell2.ColumnSpan = 28;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderRow.Cells.Add(HeaderCell2);



                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            }
        }
        catch
        {

        }
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{

		}
	}
	protected void Button5_Click(object sender, EventArgs e)
	{
		try
		{
			getgridviewrows();
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
	protected void Button6_Click(object sender, EventArgs e)
	{

	}
	protected void Button2_Click(object sender, EventArgs e)
	{

	}
}