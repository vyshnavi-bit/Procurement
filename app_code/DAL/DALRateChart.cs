using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


/// <summary>
/// Summary description for DALRateChart
/// </summary>
public class DALRateChart :DbHelper
{
    int i = 0;
    SqlConnection con = new SqlConnection();
    DbHelper dbaccess = new DbHelper();
   
    public DALRateChart()
    {
        

    }
    public void InsertData(BOLRateChart rateBOL, string Sql)
    {
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter ptid, pcharttype, pmilknature, pstateid, pminfat, pminsnf, pfromdate, ptodate, pchartname, pfromvale, ptovalue, prate, pcommissionamount, pbonusamount, pcompanycode, pplantcode;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Sql;
            cmd.Connection = con;
            int inser;

            ptid = cmd.Parameters.Add("@tid", SqlDbType.Int);
            pcharttype = cmd.Parameters.Add("@charttype", SqlDbType.NVarChar, 30);
            pmilknature = cmd.Parameters.Add("@milknature", SqlDbType.NVarChar, 30);
            pstateid = cmd.Parameters.Add("@stateid", SqlDbType.Int);
            pminfat = cmd.Parameters.Add("@minfat", SqlDbType.Float);
            pminsnf = cmd.Parameters.Add("@minsnf", SqlDbType.Float);
            pfromdate = cmd.Parameters.Add("@fromdate", SqlDbType.DateTime);
            ptodate = cmd.Parameters.Add("@todate", SqlDbType.DateTime);



            pchartname = cmd.Parameters.Add("@chartname", SqlDbType.NVarChar, 30);
            pfromvale = cmd.Parameters.Add("@fromvale", SqlDbType.Float);
            ptovalue = cmd.Parameters.Add("@tovalue", SqlDbType.Float);
            prate = cmd.Parameters.Add("@rate", SqlDbType.Float);
            pcommissionamount = cmd.Parameters.Add("@commissionamount", SqlDbType.Float);
            pbonusamount = cmd.Parameters.Add("@bonusamount", SqlDbType.Float);

            pcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.NVarChar, 30);
            pplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.NVarChar, 30);


            ptid.Value = rateBOL.Tid;
            pcharttype.Value = rateBOL.Charttype;
            pmilknature.Value = rateBOL.Milknature;
            pstateid.Value = rateBOL.Stateid;
            pminfat.Value = rateBOL.Minfat;
            pminsnf.Value = rateBOL.Minsnf;
            pfromdate.Value = rateBOL.Fromdate;
            ptodate.Value = rateBOL.Todate;

            pchartname.Value = rateBOL.Chartname;
            pfromvale.Value = rateBOL.Fromrangevalue;
            ptovalue.Value = rateBOL.Torangrvalue;
            prate.Value = rateBOL.Rate;
            pcommissionamount.Value = rateBOL.Commissionamount;
            pbonusamount.Value = rateBOL.Bonusamount;
            pplantcode.Value = rateBOL.Plantcode;
            pcompanycode.Value = rateBOL.Companycode;

            inser = cmd.ExecuteNonQuery();

            if (inser == 1)
            {
                i++;

            }
            else
            {

                i++;

            }

        }
    }


    public void EditUpdateRatechart(BOLRateChart rateBOL, string sql)
    {
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter ptid, pchartname, pfromvale, ptovalue, prate, pcommissionamount, pbonusamount, pcompanycode, pplantcode;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;
            int inser1;


            ptid = cmd.Parameters.Add("@tid", SqlDbType.Int);
            pchartname = cmd.Parameters.Add("@chartname", SqlDbType.NVarChar, 30);
            pfromvale = cmd.Parameters.Add("@fromvale", SqlDbType.Float);
            ptovalue = cmd.Parameters.Add("@tovalue", SqlDbType.Float);
            prate = cmd.Parameters.Add("@rate", SqlDbType.Float);
            pcommissionamount = cmd.Parameters.Add("@commissionamount", SqlDbType.Float);
            pbonusamount = cmd.Parameters.Add("@bonusamount", SqlDbType.Float);

            pcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.NVarChar, 30);
            pplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.NVarChar, 30);

            ptid.Value = rateBOL.Tid;
            pchartname.Value = rateBOL.Chartname;                             
            pfromvale.Value = rateBOL.Fromrangevalue;
            ptovalue.Value = rateBOL.Torangrvalue;
            prate.Value = rateBOL.Rate;
            pcommissionamount.Value = rateBOL.Commissionamount;
            pbonusamount.Value = rateBOL.Bonusamount;

            pcompanycode.Value = rateBOL.Companycode;
            pplantcode.Value = rateBOL.Plantcode;

            inser1 = cmd.ExecuteNonQuery();

            if (inser1 == 1)
            {
                i++;

            }
            else
            {

                i++;

            }

        }
    }


    public void DeleteRow(BOLRateChart rateBOL, string sql)
    {
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter ptid, pchartname, pflag, pcompanycode, pplantcode;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;
            int inser;

            ptid = cmd.Parameters.Add("@tid", SqlDbType.Int);
            pchartname = cmd.Parameters.Add("@chartname", SqlDbType.NVarChar, 30);
            pflag = cmd.Parameters.Add("@Flag", SqlDbType.Int);

            pcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.NVarChar, 30);
            pplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.NVarChar, 30);

            ptid.Value = rateBOL.Tid;
            pchartname.Value = rateBOL.Chartname;
            pflag.Value = rateBOL.Flag;
            pplantcode.Value = rateBOL.Plantcode;
            pcompanycode.Value = rateBOL.Companycode;



            inser = cmd.ExecuteNonQuery();
            

            if (inser == 1)
            {
                i++;
               // WebMsgBox.Show("RateChart Delete Successfully...");

            }
            else
            {

                i++;

            }


        }

    }



}

