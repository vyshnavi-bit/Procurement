using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for LoanDeductionDetails
/// </summary>
public partial class LoanDeductionDetails : System.Web.UI.Page
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
    string msg;
    Exception ex;
    string plant;
    string[] GET;
    DataSet abc = new DataSet();
    DataSet abc1 = new DataSet();
    DataSet abc2 = new DataSet();
    DataSet abc3 = new DataSet();
    DataSet abc4 = new DataSet();
    DataSet abc4op = new DataSet();
    int i = 0;
    string  GETPLANT;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                dtm = System.DateTime.Now;
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                roleid = Convert.ToInt32(Session["Role"].ToString());


                dtm = System.DateTime.Now;
                txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dtm.ToString("dd/MM/yyyy");
            }
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if((roleid >=3) && (roleid !=9))
        {
        gettotplant();
        opbalance();
        }
       if((roleid !=9))
        {
            gettotplant1();
            opbalance();

        }


    }


    public void gettotplant()
    {
        string stt = "";
        con = DB.GetConnection();
        stt = "Select plant_code,plant_name  from plant_master where plant_code not in (150,139,160) order by plant_code";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
    
        da.Fill(dt);
        dt.Columns.Add("OpBalance");
        dt.Columns.Add("IssuedAmount");
        dt.Columns.Add("RecoveryAmount");
        dt.Columns.Add("CashRecovery");
         dt.Columns.Add("ClosingBalance");
         if (dt.Rows.Count > 0)
         {
             GridView1.DataSource = dt;
             GridView1.DataBind();
             con = DB.GetConnection();
             foreach (GridViewRow dtP in GridView1.Rows)
             {
                 DateTime dt1 = new DateTime();
                 DateTime dt2 = new DateTime();
                 dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                 dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
               
                 string d1 = dt1.ToString("MM/dd/yyyy");
                 string d2 = dt2.ToString("MM/dd/yyyy");

                 GETPLANT = GridView1.Rows[i].Cells[1].Text;

                 string op = "SELECT OP.Openningbalance FROM (SELECT PLANT_CODE,"
        + "   YEAR = YEAR(Paid_date),"
        + "  MONTH = MONTH(Paid_date), "
        + "  MMM = UPPER(left(DATENAME(MONTH,Paid_date),3)), "
        + "  Openningbalance =  (sum(Openningbalance)), "
        + "  OrderCount = count(* ) "
        + "  FROM    Loan_Recovery    WHERE    Paid_date BETWEEN '" + d1 + "' AND '" + d2 + "' AND PLANT_CODE='" + GETPLANT + "'"
        + "  GROUP BY "
        + "   YEAR(Paid_date),"
        + "   MONTH(Paid_date), "
        + "   DATENAME(MONTH,Paid_date),PLANT_CODE )"
        + "   AS OP";


                 SqlCommand cmd1 = new SqlCommand(op, con);
                 SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                 da1.Fill(abc, ("op"));
              //  string GETOPE =    abc.Tables[0].Rows[i][0].GetHashCode();
                 try
                 {

                     double GETVALUE = Convert.ToDouble(abc.Tables[0].Rows[0][0]);
                     GridView1.Rows[i].Cells[3].Text = GETVALUE.ToString("f2");
                     abc.Tables.Clear();
                 }
                 catch
                 {
                    // GridView1.Rows[i].Cells[3].Text = "0";

                     try
                     {
                         string getopt = " sELECT   SUM(Openningbalance) as op ,Paid_date   from Loan_Recovery where Plant_code='"+GETPLANT+"'   group by Paid_date ORDER BY  convert(datetime, Paid_date, 103) desc    ";
                         SqlCommand cmd22 = new SqlCommand(getopt, con);
                         SqlDataAdapter da22 = new SqlDataAdapter(cmd22);
                         da22.Fill(abc4op, ("tempop"));
                         double getiisueee = Convert.ToDouble(abc4op.Tables[0].Rows[0][0]);
                         GridView1.Rows[i].Cells[3].Text = getiisueee.ToString("f2");
                         abc4op.Tables.Clear();
                     }
                     catch
                     {
                         GridView1.Rows[i].Cells[3].Text = "0";


                     }




                     

                 }




                 string Issue = "SELECT  ISSUE.ISSUEaMT   FROM (        SELECT PLANT_CODE,"
           + "  YEAR = YEAR(loandate), "
           + "  MONTH = MONTH(loandate), "
            + "   MMM = UPPER(left(DATENAME(MONTH,loandate),3)), "
         + "  ISSUEaMT =  (sum(loanamount)), "
          + " TotalLoans = count(* ) "
          + "  FROM    LoanDetails    WHERE    loandate BETWEEN '" + d1 + "' AND '" + d2 + "' and plant_code='" + GETPLANT + "'"
          + "  GROUP BY "

           + "  YEAR(loandate), "
           + "  MONTH(loandate), "
           + "  DATENAME(MONTH,loandate),PLANT_CODE  )   AS ISSUE ";


                 SqlCommand cmd2 = new SqlCommand(Issue, con);
                 SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                 da2.Fill(abc1, ("Issue"));

                 try
                 {

                     double getiisue = Convert.ToDouble(abc1.Tables[0].Rows[0][0]);
                     GridView1.Rows[i].Cells[4].Text = getiisue.ToString("f2");
                     abc1.Tables.Clear();
                 }
                 catch
                 {

                     GridView1.Rows[i].Cells[4].Text = "0";


                 }



                 string pay = "   SELECT PAIAMOUNT  FROM ( SELECT PLANT_CODE,"
          + "  YEAR = YEAR(Paid_date), "
           + " MONTH = MONTH(Paid_date), "
          + "  MMM = UPPER(left(DATENAME(MONTH,Paid_date),3)), "
           + " PAIAMOUNT =  (sum(Paid_Amount)), "
          + "  OrderCount = count(* ) "
          + " FROM    Loan_Recovery    WHERE    Paid_date BETWEEN '" + d1 + "' AND '" + d2 + "' and plant_code='" + GETPLANT + "'"
          + "  GROUP BY "
            + " YEAR(Paid_date), "
            + "  MONTH(Paid_date), "
            + "  DATENAME(MONTH,Paid_date),PLANT_CODE ) AS PAID ";


                 SqlCommand cmd3 = new SqlCommand(pay, con);
                 SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                 da3.Fill(abc2, ("pay"));

                 try
                 {

                     double getiisue = Convert.ToDouble(abc2.Tables[0].Rows[0][0]);
                     GridView1.Rows[i].Cells[5].Text = getiisue.ToString("f2");
                     abc2.Tables.Clear();
                 }
                 catch
                 {
                     GridView1.Rows[i].Cells[5].Text = "0";


                 }




                 string cash = "select CashAmt   from (        SELECT PLANT_CODE,"
     + "  YEAR = YEAR(LoanRecovery_Date), "
     + " MONTH = MONTH(LoanRecovery_Date), "
    + "  MMM = UPPER(left(DATENAME(MONTH,LoanRecovery_Date),3)), "
    + "  CashAmt =  (sum(LoanDueRecovery_Amount)), "
    + " TotalRecipt = count(* ) "
    + "       FROM    LoanDue_Recovery    WHERE    LoanRecovery_Date BETWEEN '" + d1 + "' AND '" + d2 + "'   and plant_code='" + GETPLANT + "'"
    + "  GROUP BY YEAR(LoanRecovery_Date),    MONTH(LoanRecovery_Date),  DATENAME(MONTH,LoanRecovery_Date),PLANT_CODE ) AS CASH ";



                 SqlCommand cmd4 = new SqlCommand(cash, con);
                 SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                 da4.Fill(abc3, ("pay"));
                 try
                 {

                     double getiisue = Convert.ToDouble(abc3.Tables[0].Rows[0][0]);
                     GridView1.Rows[i].Cells[6].Text = getiisue.ToString("f2");
                     abc3.Tables.Clear();
                 }
                 catch
                 {
                     GridView1.Rows[i].Cells[6].Text = "0";


                 }




                 string cl = "SELECT OP.Closingbalance FROM (SELECT PLANT_CODE,"
               + "   YEAR = YEAR(Paid_date),"
               + "  MONTH = MONTH(Paid_date), "
               + "  MMM = UPPER(left(DATENAME(MONTH,Paid_date),3)), "
               + "  Closingbalance =  (sum(Closingbalance)), "
               + "  OrderCount = count(* ) "
               + "  FROM    Loan_Recovery    WHERE    Paid_date BETWEEN '" + d1 + "' AND '" + d2 + "' AND PLANT_CODE='" + GETPLANT + "'"
               + "  GROUP BY "
               + "   YEAR(Paid_date),"
               + "   MONTH(Paid_date), "
               + "   DATENAME(MONTH,Paid_date),PLANT_CODE )"
               + "   AS OP";


                 SqlCommand cmd5 = new SqlCommand(cl, con);
                 SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                 da5.Fill(abc4, ("cl"));
                 //  string GETOPE =    abc.Tables[0].Rows[i][0].GetHashCode();
                 try
                 {

                     double GETVALUE = Convert.ToDouble(abc4.Tables[0].Rows[0][0]);
                     GridView1.Rows[i].Cells[7].Text = GETVALUE.ToString("f2");
                     abc4.Tables.Clear();
                 }
                 catch
                 {
                     GridView1.Rows[i].Cells[7].Text = "0";


                 }
                 double getcellop = Convert.ToDouble(GridView1.Rows[i].Cells[3].Text);
                 double getcelliis = Convert.ToDouble(GridView1.Rows[i].Cells[4].Text);
                 double getcellrec = Convert.ToDouble(GridView1.Rows[i].Cells[5].Text);
                 double getcellcash = Convert.ToDouble(GridView1.Rows[i].Cells[6].Text);
                 double getcellclo = (getcellop - getcellrec - getcellcash);
                 GridView1.Rows[i].Cells[7].Text = getcellclo.ToString("f2"); 
                


                 i = i + 1;
               
             }

         }
    }

    public void gettotplant1()
    {
        string stt = "";
        con = DB.GetConnection();
        stt = "Select plant_code,plant_name  from plant_master where plant_code   in (170) order by plant_code";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dt);
        dt.Columns.Add("OpBalance");
        dt.Columns.Add("IssuedAmount");
        dt.Columns.Add("RecoveryAmount");
        dt.Columns.Add("CashRecovery");
        dt.Columns.Add("ClosingBalance");
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con = DB.GetConnection();
            foreach (GridViewRow dtP in GridView1.Rows)
            {
                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

                string d1 = dt1.ToString("MM/dd/yyyy");
                string d2 = dt2.ToString("MM/dd/yyyy");

                GETPLANT = GridView1.Rows[i].Cells[1].Text;

                string op = "SELECT OP.Openningbalance FROM (SELECT PLANT_CODE,"
       + "   YEAR = YEAR(Paid_date),"
       + "  MONTH = MONTH(Paid_date), "
       + "  MMM = UPPER(left(DATENAME(MONTH,Paid_date),3)), "
       + "  Openningbalance =  (sum(Openningbalance)), "
       + "  OrderCount = count(* ) "
       + "  FROM    Loan_Recovery    WHERE    Paid_date BETWEEN '" + d1 + "' AND '" + d2 + "' AND PLANT_CODE='" + GETPLANT + "'"
       + "  GROUP BY "
       + "   YEAR(Paid_date),"
       + "   MONTH(Paid_date), "
       + "   DATENAME(MONTH,Paid_date),PLANT_CODE )"
       + "   AS OP";


                SqlCommand cmd1 = new SqlCommand(op, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(abc, ("op"));
                //  string GETOPE =    abc.Tables[0].Rows[i][0].GetHashCode();
                try
                {

                    double GETVALUE = Convert.ToDouble(abc.Tables[0].Rows[0][0]);
                    GridView1.Rows[i].Cells[3].Text = GETVALUE.ToString("f2");
                    abc.Tables.Clear();
                }
                catch
                {
                    // GridView1.Rows[i].Cells[3].Text = "0";

                    try
                    {
                        string getopt = " sELECT   SUM(Openningbalance) as op ,Paid_date   from Loan_Recovery where Plant_code='170'   group by Paid_date ORDER BY  convert(datetime, Paid_date, 103) desc    ";
                        SqlCommand cmd22 = new SqlCommand(getopt, con);
                        SqlDataAdapter da22 = new SqlDataAdapter(cmd22);
                        da22.Fill(abc4op, ("tempop"));
                        double getiisueee = Convert.ToDouble(abc4op.Tables[0].Rows[0][0]);
                        GridView1.Rows[i].Cells[3].Text = getiisueee.ToString("f2");
                        abc4op.Tables.Clear();
                    }
                    catch
                    {
                        GridView1.Rows[i].Cells[3].Text = "0";


                    }






                }




                string Issue = "SELECT  ISSUE.ISSUEaMT   FROM (        SELECT PLANT_CODE,"
          + "  YEAR = YEAR(loandate), "
          + "  MONTH = MONTH(loandate), "
           + "   MMM = UPPER(left(DATENAME(MONTH,loandate),3)), "
        + "  ISSUEaMT =  (sum(loanamount)), "
         + " TotalLoans = count(* ) "
         + "  FROM    LoanDetails    WHERE    loandate BETWEEN '" + d1 + "' AND '" + d2 + "' and plant_code='" + GETPLANT + "'"
         + "  GROUP BY "

          + "  YEAR(loandate), "
          + "  MONTH(loandate), "
          + "  DATENAME(MONTH,loandate),PLANT_CODE  )   AS ISSUE ";


                SqlCommand cmd2 = new SqlCommand(Issue, con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(abc1, ("Issue"));

                try
                {

                    double getiisue = Convert.ToDouble(abc1.Tables[0].Rows[0][0]);
                    GridView1.Rows[i].Cells[4].Text = getiisue.ToString("f2");
                    abc1.Tables.Clear();
                }
                catch
                {

                    GridView1.Rows[i].Cells[4].Text = "0";


                }



                string pay = "   SELECT PAIAMOUNT  FROM ( SELECT PLANT_CODE,"
         + "  YEAR = YEAR(Paid_date), "
          + " MONTH = MONTH(Paid_date), "
         + "  MMM = UPPER(left(DATENAME(MONTH,Paid_date),3)), "
          + " PAIAMOUNT =  (sum(Paid_Amount)), "
         + "  OrderCount = count(* ) "
         + " FROM    Loan_Recovery    WHERE    Paid_date BETWEEN '" + d1 + "' AND '" + d2 + "' and plant_code='170'"
         + "  GROUP BY "
           + " YEAR(Paid_date), "
           + "  MONTH(Paid_date), "
           + "  DATENAME(MONTH,Paid_date),PLANT_CODE ) AS PAID ";


                SqlCommand cmd3 = new SqlCommand(pay, con);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(abc2, ("pay"));

                try
                {

                    double getiisue = Convert.ToDouble(abc2.Tables[0].Rows[0][0]);
                    GridView1.Rows[i].Cells[5].Text = getiisue.ToString("f2");
                    abc2.Tables.Clear();
                }
                catch
                {
                    GridView1.Rows[i].Cells[5].Text = "0";


                }




                string cash = "select CashAmt   from (        SELECT PLANT_CODE,"
    + "  YEAR = YEAR(LoanRecovery_Date), "
    + " MONTH = MONTH(LoanRecovery_Date), "
   + "  MMM = UPPER(left(DATENAME(MONTH,LoanRecovery_Date),3)), "
   + "  CashAmt =  (sum(LoanDueRecovery_Amount)), "
   + " TotalRecipt = count(* ) "
   + "       FROM    LoanDue_Recovery    WHERE    LoanRecovery_Date BETWEEN '" + d1 + "' AND '" + d2 + "'   and plant_code='170'"
   + "  GROUP BY YEAR(LoanRecovery_Date),    MONTH(LoanRecovery_Date),  DATENAME(MONTH,LoanRecovery_Date),PLANT_CODE ) AS CASH ";



                SqlCommand cmd4 = new SqlCommand(cash, con);
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                da4.Fill(abc3, ("pay"));
                try
                {

                    double getiisue = Convert.ToDouble(abc3.Tables[0].Rows[0][0]);
                    GridView1.Rows[i].Cells[6].Text = getiisue.ToString("f2");
                    abc3.Tables.Clear();
                }
                catch
                {
                    GridView1.Rows[i].Cells[6].Text = "0";


                }




                string cl = "SELECT OP.Closingbalance FROM (SELECT PLANT_CODE,"
              + "   YEAR = YEAR(Paid_date),"
              + "  MONTH = MONTH(Paid_date), "
              + "  MMM = UPPER(left(DATENAME(MONTH,Paid_date),3)), "
              + "  Closingbalance =  (sum(Closingbalance)), "
              + "  OrderCount = count(* ) "
              + "  FROM    Loan_Recovery    WHERE    Paid_date BETWEEN '" + d1 + "' AND '" + d2 + "' AND PLANT_CODE='170'"
              + "  GROUP BY "
              + "   YEAR(Paid_date),"
              + "   MONTH(Paid_date), "
              + "   DATENAME(MONTH,Paid_date),PLANT_CODE )"
              + "   AS OP";


                SqlCommand cmd5 = new SqlCommand(cl, con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                da5.Fill(abc4, ("cl"));
                //  string GETOPE =    abc.Tables[0].Rows[i][0].GetHashCode();
                try
                {

                    double GETVALUE = Convert.ToDouble(abc4.Tables[0].Rows[0][0]);
                    GridView1.Rows[i].Cells[7].Text = GETVALUE.ToString("f2");
                    abc4.Tables.Clear();
                }
                catch
                {
                    GridView1.Rows[i].Cells[7].Text = "0";


                }
                double getcellop = Convert.ToDouble(GridView1.Rows[i].Cells[3].Text);
                double getcelliis = Convert.ToDouble(GridView1.Rows[i].Cells[4].Text);
                double getcellrec = Convert.ToDouble(GridView1.Rows[i].Cells[5].Text);
                double getcellcash = Convert.ToDouble(GridView1.Rows[i].Cells[6].Text);
                double getcellclo = (getcellop - getcellrec - getcellcash);
                GridView1.Rows[i].Cells[7].Text = getcellclo.ToString("f2");



                i = i + 1;

            }

        }
    }


    public void opbalance()
    {
       
        

        


       
       


    }

    protected void Button6_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Loan Recovery Details:" + " For: " + txt_FromDate.Text + " :To" + txt_ToDate.Text;
            HeaderCell2.ColumnSpan = 8;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderRow.Cells.Add(HeaderCell2);



            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
}