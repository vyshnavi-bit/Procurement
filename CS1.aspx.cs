using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

public partial class CS : System.Web.UI.Page
{
    int asize = 0;
    DateTime frmdat = new DateTime();
    string frmdates = string.Empty;
    string Rid = string.Empty;
    string TempRid = string.Empty;
    string pcode = string.Empty;
    string date;
    string sess;
    DbHelper dbaccess = new DbHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getdatestring();
            frmdat = System.DateTime.Now;
            frmdates = frmdat.ToString("MM/dd/yyyy");  
             Rid = Request.QueryString["id"];
             TempRid = Rid;
             string[] spilvalarr = new string[3];
             spilvalarr = Rid.Split('_');
             pcode = spilvalarr[0];
             Rid = spilvalarr[1];           
             Lbl_PlantTitle.Text = TempRid;
             Compare(Rid, pcode);
        }
        else
        {
            Compare(Rid,pcode);
        }
    }
    public void getdatestring()
    {
        string str = string.Empty;
        int sesscount = 0;
        date = System.DateTime.Now.ToShortDateString();
        sess = System.DateTime.Now.ToString("tt");
        str = "SELECT  COUNT(*) AS Counts FROM  lp Where Prdate='" + date + "' AND Sessions='" + sess + "'";
        sesscount = dbaccess.ExecuteScalarint(str);

        if (sesscount > 0)
        {
            sesscount = 0;
        }
        else
        {
            if (sess == "AM")
            {
                sess = "PM";
            }
            else
            {
                sess = "AM";
            }
        }


    }


    private void Compare(string Rid, string pcode)
    {
        try
        {
            //string query = string.Format("select datepart(year, Agent_Id) Year, count(datepart(year, Agent_Id)) TotalOrders  from Agent_Master where Plant_code = '{0}' group by datepart(year, Agent_Id)", ddlCountry1.SelectedItem.Value);
            string query = string.Format("SELECT (REPLACE( Convert(Nvarchar(5),EndingTime), ':', '.' ))AS StartingTime,REPLACE( Convert(Nvarchar(5),LabCaptureTime), ':', '.' )AS LabCaptureTime ,sampleno FROM Lp Where PlantCode='" + pcode + "' AND Prdate='" + date.ToString().Trim() + "' AND RouteId IS NOT NULL AND RouteId='" + Rid + "'   AND sessions='" + sess + "' order by sampleno");
            DataTable dtA = GetData(query);

            // ArrayList x = new ArrayList(12);

            string[] x = new string[asize];
            ///decimal[] y = new decimal[asize];
            string[] y = new string[asize];
            decimal[] dArray = new decimal[asize];

            decimal[] y1 = new decimal[asize];

            string strvari = string.Empty;
            string strvariRes = string.Empty;
            string[] strlengharr = new string[4];
            string[] strlengharrRes = new string[4];
            string Loadsampleno = string.Empty;
            string CumLoadsampleno = string.Empty;
            string LoadA1 = string.Empty;
            string LoadA2 = string.Empty;

            string xTimechk = "0";

            string valarr = string.Empty;
            string[] tempvalarr = new string[2];
            int Resvalarr0 = 0;
            int Resvalarr1 = 0;
            string temp = string.Empty;
            //
            //Sametime SampleNo
            DataTable Sametime = new DataTable();
            DataRow RSametime = null;
            DataColumn CSametime = null;
            Sametime.Columns.Add("Wtime", typeof(decimal));
            Sametime.Columns.Add("WSampleNo", typeof(string));
            Sametime.Columns.Add("WCumSampleNo", typeof(string));

            Sametime.Columns.Add("Atime", typeof(decimal));
            Sametime.Columns.Add("ACumSampleNo", typeof(string));

            DataTable RdtA = new DataTable();
            DataRow RdrA = null;
            DataColumn RcolA = null;
            RdtA.Columns.Add("Wtime", typeof(decimal));
            RdtA.Columns.Add("SampleNo", typeof(string));
            RdtA.Columns.Add("A1", typeof(string));
            RdtA.Columns.Add("A2", typeof(string));

            //RcolA = new DataColumn("Wtime");
            //RdtA.Columns.Add(RcolA);
            //RcolA = new DataColumn("SampleNo");
            //RdtA.Columns.Add(RcolA);
            //RcolA = new DataColumn("A1");
            //RdtA.Columns.Add(RcolA);
            //RcolA = new DataColumn("A2");
            //RdtA.Columns.Add(RcolA);


            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][0].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
                //  strvariRes = strlengharrRes[2];          

                if (xTimechk == strvariRes)
                {

                }
                else
                {
                    RdrA = RdtA.NewRow();
                    RdrA[0] = Convert.ToDecimal(strvariRes);
                    RdrA[1] = Loadsampleno;
                    RdrA[2] = "1-" + Loadsampleno;
                    RdrA[3] = "0-0";
                    RdtA.Rows.Add(RdrA);
                    xTimechk = strvariRes;
                }
            }

            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][1].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];
                //  strvariRes = strlengharrRes[2];          

                if (xTimechk == strvariRes)
                {

                }
                else
                {
                    RdrA = RdtA.NewRow();
                    RdrA[0] = Convert.ToDecimal(strvariRes);
                    RdrA[1] = Loadsampleno;
                    RdrA[2] = "0-0";
                    RdrA[3] = "1-" + Loadsampleno;
                    RdtA.Rows.Add(RdrA);
                    xTimechk = strvariRes;
                }


            }

            //Load SameTime SampleNo Start
            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][0].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];


                RSametime = Sametime.NewRow();
                RSametime[0] = Convert.ToDecimal(strvariRes);
                RSametime[1] = Loadsampleno;
                //RSametime[2] = CumLoadsampleno;
                Sametime.Rows.Add(RSametime);
                xTimechk = strvariRes;
            }




            for (int i = 0; i < Sametime.Rows.Count; i++)
            {

                strvari = Sametime.Rows[i][0].ToString().Trim();
                CumLoadsampleno = string.Empty;
                DataRow[] result = Sametime.Select("Wtime=" + strvari);
                int coma = 0;
                foreach (DataRow DR1 in result)
                {
                    if (coma == 0)
                    {
                        Loadsampleno = DR1["Wsampleno"].ToString().Trim();
                        CumLoadsampleno = Loadsampleno;
                        coma++;
                    }
                    else
                    {
                        Loadsampleno = DR1["Wsampleno"].ToString().Trim();
                        CumLoadsampleno = CumLoadsampleno + ',' + Loadsampleno;
                    }
                }
                Sametime.Rows[i][2] = CumLoadsampleno;
            }

            CumLoadsampleno = string.Empty;
            for (int i = 0; i < dtA.Rows.Count; i++)
            {
                strvari = dtA.Rows[i][1].ToString().Trim();
                Loadsampleno = dtA.Rows[i][2].ToString().Trim();
                strlengharr = strvari.Split('.');
                strlengharrRes[0] = strlengharr[0];
                strlengharrRes[1] = ".";
                strlengharrRes[2] = strlengharr[1];
                strvariRes = strlengharrRes[0] + strlengharrRes[1] + strlengharrRes[2];

                Sametime.Rows[i][3] = Convert.ToDecimal(strvariRes);
                Sametime.Rows[i][4] = CumLoadsampleno;


            }

            for (int i = 0; i < Sametime.Rows.Count; i++)
            {

                strvari = Sametime.Rows[i][3].ToString().Trim();
                CumLoadsampleno = string.Empty;
                DataRow[] result = Sametime.Select("Atime=" + strvari);
                int coma = 0;
                foreach (DataRow DR1 in result)
                {
                    if (coma == 0)
                    {
                        Loadsampleno = DR1["wsampleno"].ToString().Trim();
                        CumLoadsampleno = Loadsampleno;
                        coma++;
                    }
                    else
                    {
                        Loadsampleno = DR1["wsampleno"].ToString().Trim();
                        CumLoadsampleno = CumLoadsampleno + ',' + Loadsampleno;
                    }
                }
                Sametime.Rows[i][4] = CumLoadsampleno;
            }


            //Result Weight Table Start
            DataView view = new DataView(Sametime);
            DataTable distinctValues = new DataTable();
            distinctValues = view.ToTable(true, "Wtime");


            DataTable ResultWeight = new DataTable();
            DataRow ResultRow = null;
            ResultWeight.Columns.Add("Wtime", typeof(decimal));
            ResultWeight.Columns.Add("WSampleNo", typeof(string));
            strvari = string.Empty;
            for (int j = 0; j < distinctValues.Rows.Count; j++)
            {
                ResultRow = ResultWeight.NewRow();
                strvari = distinctValues.Rows[j][0].ToString().Trim();
                int coma = 0;
                for (int i = 0; i < Sametime.Rows.Count; i++)
                {

                    DataRow[] result = Sametime.Select("Wtime=" + strvari);

                    foreach (DataRow DR1 in result)
                    {
                        if (coma == 0)
                        {
                            ResultRow[0] = DR1[0];
                            ResultRow[1] = DR1[2];
                            ResultWeight.Rows.Add(ResultRow);
                            coma++;
                        }
                        else
                        {

                        }
                    }
                }
            }
            grdweight.DataSource = ResultWeight;
            grdweight.DataBind();
            //Result Weight Table END


            //Result Analyzer Table Start
            DataView Aview = new DataView(Sametime);
            DataTable AdistinctValues = new DataTable();
            AdistinctValues = view.ToTable(true, "Atime");


            DataTable ResultAnalyzer = new DataTable();
            DataRow ResultAnaRow = null;
            ResultAnalyzer.Columns.Add("Atime", typeof(decimal));
            ResultAnalyzer.Columns.Add("ASampleNo", typeof(string));
            strvari = string.Empty;

            for (int j = 0; j < AdistinctValues.Rows.Count; j++)
            {
                ResultAnaRow = ResultAnalyzer.NewRow();
                strvari = AdistinctValues.Rows[j][0].ToString().Trim();
                int coma = 0;
                for (int i = 0; i < Sametime.Rows.Count; i++)
                {

                    DataRow[] result = Sametime.Select("Atime=" + strvari);

                    foreach (DataRow DR1 in result)
                    {
                        if (coma == 0)
                        {
                            ResultAnaRow[0] = DR1[3];
                            ResultAnaRow[1] = DR1[4];
                            ResultAnalyzer.Rows.Add(ResultAnaRow);
                            coma++;
                        }
                        else
                        {

                        }
                    }
                }
            }
            //Result Analyzer Table END
            grdAnalyzer.DataSource = ResultAnalyzer;
            grdAnalyzer.DataBind();

            //Load SameTime SampleNo End


            //Sort Table Value
            //RdtA.DefaultView.Sort = "Wtime ASC ";
            //RdtA = RdtA.DefaultView.ToTable(true);

            // Order by using Storeprocedure
            DataTable custOrderDT = new DataTable();
            custOrderDT = RdtA;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "custOrderDT";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = custOrderDT;
            param.Direction = ParameterDirection.Input;
            SqlConnection conn = null;
            string constr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
            using (conn = new SqlConnection(constr))
            {
                SqlCommand sqlCmd = new SqlCommand("[dbo].[LineChart_OrderBy]");
                sqlCmd.Connection = conn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(param);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);

                DataTable Sptable = new DataTable();
                adp.Fill(Sptable);
                RdtA = Sptable;

            }
            //


            asize = RdtA.Rows.Count;
            //Resize Array Value
            Array.Resize(ref x, asize);
            for (int i = 0; i < RdtA.Rows.Count; i++)
            {
                string v = RdtA.Rows[i][0].ToString().Trim();
                //Load X axis Value
                x[i] = v;
            }


            //


            //load A1 Series
            asize = RdtA.Rows.Count;
            //Resize Array Value
            Array.Resize(ref y, asize);

            for (int i = 0; i < RdtA.Rows.Count; i++)
            {
                temp = RdtA.Rows[i][2].ToString().Trim();
                valarr = temp;
                tempvalarr = valarr.Split('-');
                Resvalarr0 = Convert.ToInt32(tempvalarr[0]);
                Resvalarr1 = Convert.ToInt32(tempvalarr[1]);

                if (Resvalarr0 == 0)
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y[i] = "0";
                }
                else
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y[i] = Resvalarr1.ToString();
                }
            }

            //Remove array elemet
            Array.Resize(ref dArray, asize);


            for (int i = 0; i < y.Length; i++)
            {
                //if (y[i].ToString().Trim().Length == 0)
                //{
                //}
                //else
                //{


                //}
                dArray[i] = Convert.ToDecimal(y[i]);
            }



            LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = "Weigher", Data = dArray });
            ///LineChart1.CategoriesAxis = string.Join(",", x);
            LineChart1.CategoriesAxis = string.Join(",", x);
           


            //load A2 Series

            asize = RdtA.Rows.Count;
            // y = new decimal[asize];
            y = new string[asize];
            Array.Resize(ref y1, asize);


            for (int i = 0; i < RdtA.Rows.Count; i++)
            {

                temp = RdtA.Rows[i][3].ToString().Trim();
                valarr = temp;
                tempvalarr = valarr.Split('-');
                Resvalarr0 = Convert.ToInt32(tempvalarr[0]);
                Resvalarr1 = Convert.ToInt32(tempvalarr[1]);

                if (Resvalarr0 == 0)
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y1[i] = 0;
                }
                else
                {
                    x[i] = RdtA.Rows[i][0].ToString().Trim();
                    y1[i] = Resvalarr1;
                }
            }

            LineChart1.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = "Analyzer", Data = y1 });


            //LineChart1.ChartTitle = string.Format("{0} and {1} Order Distribution", ddlCountry1.SelectedItem.Value, ddlCountry2.SelectedItem.Value);


            if (x.Length > 3)
            {
                LineChart1.ChartWidth = (x.Length * 33).ToString();
                //  LineChart1.ChartHeight = "700px";
                //LineChart1.ChartHeight = (y.Length * 10).ToString();
            }
            LineChart1.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

   
    private static DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }

    

}


//http://ajaxcontroltoolkit.devexpress.com/LineChart/LineChart.aspx