using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
public partial class BillTotalAmount : System.Web.UI.Page
{
    DbHelper db = new DbHelper();
    SqlConnection con = new SqlConnection();
    string planno;
    DateTime dtm = new DateTime();
    string[] getbankid1;
    string get;
    string EE;
    string message;
    int msgvalue;
    string amount;
    string GETPLANTCODE;
    DataTable dt = new DataTable();
    DataTable dtt = new DataTable();
    int countcow;
    int countBuff;
    int countsubsumcow;
    int countsubsumBuff;
    double countsubsumEXCESSCOW = 0;
    double countsubsumEXCESSBuff = 0;

    double countsubsumTOTCOW = 0;
    double countsubsumTOTBuff = 0;


    double cowgrandtotal = 0;
    double buffgrandtotal = 0;

    double cowgrandtotalTOT = 0;
    double buffgrandtotalTOT = 0;


    double cowgrandtotalexcess = 0;
    double buffgrandtotalexcess = 0;


    string milktype;
    string plantname;
    int netamt;
    string getvar = "COW TOTAL";
    string getvar1 = "BUFFALO TOTAL";
    string getvar2 = "TOTAL AMOUNT";
    int SNO=0;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            roleid = Convert.ToInt32(Session["Role"].ToString());
            dtm = System.DateTime.Now;
            txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
            txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
            Button2.Visible = false;
            if ((roleid >= 3) && (roleid != 9))
            {
                GETPLANT();
            }
            else
            {
                GETPLANT1();

            }
        }
        else
        {
            //  gettotamount();
            Button2.Visible = false;

        }
    }




    public void GETPLANT()
    {

        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            string sqlstr = "select Convert(nvarchar(50),PLANT_CODE)+'_'+Convert(nvarchar(50),plant_name) as plant_name from PLANT_MASTER   where plant_code >  154  AND plant_code NOT IN (160,170) ";
            SqlDataAdapter adp = new SqlDataAdapter(sqlstr, con);
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddchkCountry.DataSource = dt;
                ddchkCountry.DataTextField = "plant_name";
                //  ddchkCountry.DataValueField = "PLANT_CODE";
                ddchkCountry.DataBind();



                //  ddchkCountry.Items.Add("0");


            }

        }
        catch (Exception EE)
        {
            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }
    }
    public void GETPLANT1()
    {

        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection con = db.GetConnection();
            string sqlstr = "select Convert(nvarchar(50),PLANT_CODE)+'_'+Convert(nvarchar(50),plant_name) as plant_name from PLANT_MASTER   where plant_code >  154  AND plant_code   IN (170) ";
            SqlDataAdapter adp = new SqlDataAdapter(sqlstr, con);
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddchkCountry.DataSource = dt;
                ddchkCountry.DataTextField = "plant_name";
                //  ddchkCountry.DataValueField = "PLANT_CODE";
                ddchkCountry.DataBind();



                //  ddchkCountry.Items.Add("0");


            }

        }
        catch (Exception EE)
        {
            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }
    }

    // for bill tot amount
    public void gettotamount()
    {
        try
        {


            string str = "";

            con = db.GetConnection();


            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();

            dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);



            string d1 = dt1.ToString("MM/dd/yyyy");
            string d2 = dt2.ToString("MM/dd/yyyy");

            //   str = "select PLANTNAME,(fdate + '_' + Todate)  as BillDate,NETAMOUNT,   from (SELECT PLANT_NAME AS PLANTNAME,CONVERT(VARCHAR,FRM_DATE,103) AS 	FDate,CONVERT(VARCHAR,To_DATE,103) AS 	ToDate,CONVERT(INT,AMOUNT) AS NETAMOUNT   FROM (SELECT   FRM_DATE,TO_DATE,SUM(NETAMOUNT) AS Amount,PLANT_CODE  FROM   PAYMENTDATA  WHERE plant_code in(" + GETPLANTCODE + ")  AND FRM_DATE BETWEEN '" + d1 + "' AND '" + d2 + "'    GROUP BY FRM_DATE,TO_DATE,PLANT_CODE) AS PAY LEFT JOIN  (select plant_code,plant_name from Plant_Master where plant_code in (" + GETPLANTCODE + ")  group by  plant_code,plant_name)  AS PM ON  PAY.plant_code=PM.plant_code) as Dat";

         //   str = "select   milktype,PLANTNAME,NETAMOUNT   from (SELECT PLANT_NAME AS PLANTNAME,CONVERT(VARCHAR,FRM_DATE,103) AS 	FDate,CONVERT(VARCHAR,To_DATE,103) AS 	ToDate,CONVERT(INT,AMOUNT) AS NETAMOUNT   FROM (SELECT   FRM_DATE,TO_DATE,SUM(NETAMOUNT) AS Amount,PLANT_CODE  FROM   PAYMENTDATA  WHERE plant_code in(" + GETPLANTCODE + ")  AND FRM_DATE BETWEEN '" + d1 + "' AND '" + d2 + "'    GROUP BY FRM_DATE,TO_DATE,PLANT_CODE) AS PAY LEFT JOIN  (select plant_code,plant_name from Plant_Master where plant_code in (" + GETPLANTCODE + ")  group by  plant_code,plant_name)  AS PM ON  PAY.plant_code=PM.plant_code) as Dat";
           // str = "select   milktype,PLANTNAME,NETAMOUNT   from (SELECT milktype,PLANT_NAME AS PLANTNAME,CONVERT(VARCHAR,FRM_DATE,103) AS 	FDate,CONVERT(VARCHAR,To_DATE,103) AS 	ToDate,CONVERT(INT,AMOUNT) AS NETAMOUNT   FROM (SELECT   FRM_DATE,TO_DATE,SUM(NETAMOUNT) AS Amount,PLANT_CODE  FROM   PAYMENTDATA  WHERE plant_code in(" + GETPLANTCODE + ")  AND FRM_DATE BETWEEN '" + d1 + "' AND '" + d2 + "'    GROUP BY FRM_DATE,TO_DATE,PLANT_CODE) AS PAY LEFT JOIN  (select plant_code,plant_name,milktype from Plant_Master where plant_code in (" + GETPLANTCODE + ")  group by  plant_code,plant_name,milktype)  AS PM ON  PAY.plant_code=PM.plant_code) as Dat   ORDER BY milktype ASC";
            str = "select   milktype,PLANTNAME,NETAMOUNT, PLANTCODE   from (SELECT milktype,PLANT_NAME AS PLANTNAME,CONVERT(VARCHAR,FRM_DATE,103) AS 	FDate,CONVERT(VARCHAR,To_DATE,103) AS 	ToDate,CONVERT(INT,AMOUNT) AS NETAMOUNT,PLANTCODE   FROM (SELECT   FRM_DATE,TO_DATE,  SUM(FLOOR(NetAmount)) AS Amount,PLANT_CODE  AS PLANTCODE FROM   PAYMENTDATA     WHERE plant_code in(" + GETPLANTCODE + ")  AND FRM_DATE BETWEEN '" + d1 + "' AND '" + d2 + "'     GROUP BY FRM_DATE,TO_DATE,PLANT_CODE) AS PAY LEFT JOIN  (select plant_code,plant_name,milktype from Plant_Master where plant_code in (" + GETPLANTCODE + ")   group by  plant_code,plant_name,milktype)  AS PM ON  PAY.PLANTCODE=PM.plant_code) as Dat   ORDER BY milktype,PLANTCODE ASC";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            da.Fill(dt);


            string excessstr1 = "SELECT SUM(TotAmount) AS ExcesAmount, Plant_code   FROM AgentExcesAmount  WHERE        (Frm_date = '" + d1 + "') AND (To_date = '" + d2 + "') AND Plant_code in(" + GETPLANTCODE + ") Group by  Plant_code";
            SqlCommand cmd1 = new SqlCommand(excessstr1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable DTEXCESS = new DataTable();
            da1.Fill(DTEXCESS);


           // dtt.Columns.Add("MILKTYPE");
            dtt.Columns.Add("PLANTNAME");
            dtt.Columns.Add("NETAMOUNT").DataType = typeof(int);
            dtt.Columns.Add("ExcessAmount").DataType = typeof(int);
            dtt.Columns.Add("Total").DataType = typeof(int);
            double excessamt = 0;
            foreach (DataRow dr in dt.Rows)
            {
                 excessamt = 0;
                 milktype = dr[0].ToString();
                 plantname = dr[1].ToString();
                 netamt = Convert.ToInt32(dr[2].ToString());
                 string plantcode = dr[3].ToString();
                 if (milktype == "1")
                 {

                     foreach (DataRow dra in DTEXCESS.Select("Plant_code='" + plantcode + "'"))
                     {
                         string eamt = dra["ExcesAmount"].ToString();
                         if (eamt != "" || eamt != null)
                         {
                             excessamt = Convert.ToDouble(dra["ExcesAmount"].ToString());
                         }
                     }
                     countcow = countcow + 1;
                     countsubsumcow = countsubsumcow + netamt;
                     countsubsumEXCESSCOW = countsubsumEXCESSCOW + excessamt;
                     double total = netamt + excessamt;
                     countsubsumTOTCOW = countsubsumTOTCOW + total;
                     dtt.Rows.Add(plantname, netamt, excessamt, total);
                 }
                 else
                 {
                     //  string get="Total";
                     if (SNO == 0)
                     {
                         dtt.Rows.Add(getvar, countsubsumcow, countsubsumEXCESSCOW, countsubsumTOTCOW);
                         cowgrandtotal = countsubsumcow;
                         cowgrandtotalTOT = countsubsumTOTCOW;
                         cowgrandtotalexcess = countsubsumEXCESSCOW;
                         SNO = 1;
                     }
                     if (milktype == "2")
                     {
                         foreach (DataRow dra in DTEXCESS.Select("Plant_code='" + plantcode + "'"))
                         {
                             string eamt = dra["ExcesAmount"].ToString();
                             if (eamt != "" || eamt != null)
                             {
                                 excessamt = Convert.ToDouble(dra["ExcesAmount"].ToString());
                             }
                         }
                         countcow = countBuff + 1;
                         countsubsumBuff = countsubsumBuff + netamt;
                         countsubsumEXCESSBuff = countsubsumEXCESSBuff + excessamt;
                         double total = netamt + excessamt;
                         countsubsumTOTBuff = countsubsumTOTBuff + total;
                         dtt.Rows.Add(plantname, netamt, excessamt, total);
                     }
                 }
            }
            dtt.Rows.Add(getvar1, countsubsumBuff, countsubsumEXCESSBuff, countsubsumTOTBuff);
            buffgrandtotal = countsubsumBuff;
            buffgrandtotalTOT = countsubsumTOTBuff;
            buffgrandtotalexcess = countsubsumEXCESSBuff;


            double gt = buffgrandtotal + cowgrandtotal;
            double gtexcs = buffgrandtotalexcess + cowgrandtotalexcess;
            double gtt = buffgrandtotalTOT + cowgrandtotalTOT;
            dtt.Rows.Add(getvar2, gt, gtexcs, gtt);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dtt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = "No Records";
                GridView1.DataBind();
            }

        }
        catch (Exception EE)
        {

            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);



        }

    }
 
    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {


            for (int i = 0; i < ddchkCountry.Items.Count; i++)
            {

                if (i == 0)
                {
                    get = "";
                }


                if (ddchkCountry.Items[i].Selected == true)
                {
                    string GETT = ddchkCountry.Items[i].Text;
                    string stt = GETT.ToString();
                    //Regex rgx = new Regex("[^a-zA-Z]");
                    //string  str = rgx.Replace(stt, ",");
                    // string getbankid = stt.Substring(0, stt.LastIndexOf('</br>'));




                    // string getbankid = stt.Substring(0, stt.LastIndexOf('</br>'));
                    // getbankid1 = stt.Split('_');



                    // get = getbankid1[0].ToString() + "," + get;
                    // GETPLANTCODE = get.ToString().TrimEnd(',');
                    // string gesplit = GETPLANTCODE;

                    // string res = Regex.Replace(gesplit, "CASH<br />", "0");
                    //GETPLANTCODE = res.ToString();
                    getbankid1 = stt.Split('_');
                    get = getbankid1[0].ToString() + "," + get;
                    GETPLANTCODE = get.ToString().TrimEnd(',');
                    string gesplit = GETPLANTCODE.ToString();
                    string res = gesplit.ToString();
                    GETPLANTCODE = res.ToString();

                }





            }

            gettotamount();
            Button2.Visible = false;
        }

        catch (Exception EE)
        {
            Button2.Visible = false;
            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = txt_FromDate.Text + txt_ToDate.Text + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        catch (Exception EE)
        {
            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ddchkCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            try
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                string GET = e.Row.Cells[2].Text + ".00";
                e.Row.Cells[2].Text = GET;

                string sttt = e.Row.Cells[1].Text;
                if (sttt == "COW TOTAL" || sttt == "BUFFALO TOTAL")
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[2].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;

                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[3].Font.Bold = true;
                    e.Row.Cells[4].Font.Bold = true;
                }

                if (sttt == "TOTAL AMOUNT")
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Purple;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Purple;
                    e.Row.Cells[2].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;

                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Purple;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Purple;
                    e.Row.Cells[3].Font.Bold = true;
                    e.Row.Cells[4].Font.Bold = true;
                }

            }
            catch (Exception EE)
            {
                message = EE.ToString();
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

            }

           // e.Row.Cells[3].Font.Bold = true;

        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell3 = new TableCell();
                TableCell HeaderCell4 = new TableCell();

                HeaderCell3.Text = "CC TOTAL PAYMENT:" + txt_FromDate.Text + "To" + txt_ToDate.Text;
                HeaderCell3.ColumnSpan = 4;
                HeaderCell3.Attributes.CssStyle["text-align"] = "center";
                HeaderRow.Cells.Add(HeaderCell3);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
                HeaderCell3.Font.Bold = true;
                HeaderCell3.ForeColor = System.Drawing.Color.BlueViolet;
                HeaderCell3.BackColor = System.Drawing.Color.White;
            }
        }
        catch (Exception EE)
        {
            message = EE.ToString();
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


        }

    }


    public void GETCATCH(Exception EE)
    {

        message = EE.ToString();
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }
}