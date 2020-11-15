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
/// Summary description for DALProcurement
/// </summary>
public class DALProcurement : DbHelper
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALProcurement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertPProcurement(BOLProcurement procurementBO, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parcenterid, paragentid, parwdate, parsession, parmilkkg, parmilkltr, parNo_Of_Cans, parfat, parsnf, parrate, parsample_No, parsno, paramount, parfat_Kg, parsnf_Kg, parclr, parnetamount, parnetrate, parraratechart, parmilknature, parcomamnt, parbonamnt, parrouteid, parcmpid, partipsttime, partipendtime, paruserweigherid, paruseranalyzerid, ptruck_id,parastatus;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;


           
            //WEIGHER
            paragentid = cmd.Parameters.Add("@Agent_ID", SqlDbType.Int);
            parwdate = cmd.Parameters.Add("@WDate", SqlDbType.DateTime);
            parsession = cmd.Parameters.Add("@Session", SqlDbType.NChar, 10);
            parmilkltr = cmd.Parameters.Add("@milk_Ltr", SqlDbType.Float);
            parcenterid = cmd.Parameters.Add("@Centre_ID", SqlDbType.NVarChar, 50);
            parrouteid = cmd.Parameters.Add("@Route_ID", SqlDbType.Int);
            parNo_Of_Cans = cmd.Parameters.Add("@No_Of_Cans", SqlDbType.Int);
            parmilkkg = cmd.Parameters.Add("@milk_Kg", SqlDbType.Float);
            parcmpid = cmd.Parameters.Add("@Company_Code", SqlDbType.NVarChar, 50);
            parraratechart = cmd.Parameters.Add("@ratechart", SqlDbType.VarChar, 25);
            parmilknature = cmd.Parameters.Add("@milknature", SqlDbType.VarChar, 10);
            parsample_No = cmd.Parameters.Add("@sample_No", SqlDbType.NVarChar, 15);
            parsno = cmd.Parameters.Add("@sno", SqlDbType.Int);

            partipsttime = cmd.Parameters.Add("@tipsttime",SqlDbType.DateTime);
            partipendtime = cmd.Parameters.Add("@tipendtime",SqlDbType.DateTime);
            paruserweigherid = cmd.Parameters.Add("@userweigherid",SqlDbType.Int);                      
          
           
          
            //ANALYZER    
            parfat = cmd.Parameters.Add("@fat", SqlDbType.Float);
            parsnf = cmd.Parameters.Add("@snf", SqlDbType.Float);
            parrate = cmd.Parameters.Add("@rate", SqlDbType.Money);
            paramount = cmd.Parameters.Add("@amount", SqlDbType.Money);
            parclr = cmd.Parameters.Add("@clr", SqlDbType.Float);
            parfat_Kg = cmd.Parameters.Add("@fat_Kg",SqlDbType.Float);
            parsnf_Kg = cmd.Parameters.Add("@snf_Kg",SqlDbType.Float);
            paruseranalyzerid = cmd.Parameters.Add("@useranalyzerid", SqlDbType.Int);           
            parnetrate = cmd.Parameters.Add("@net_rate",SqlDbType.Money);
            ptruck_id = cmd.Parameters.Add("@truck_id", SqlDbType.Int);
            parastatus=cmd.Parameters.Add("@status",SqlDbType.Int);






            //WEIGHER
            paragentid.Value = procurementBO.AgentID;
            parwdate.Value = procurementBO.WDate;
            parsession.Value = procurementBO.Session;
            parmilkltr.Value = procurementBO.milk_Ltr;
            parcenterid.Value = procurementBO.CentreID;
            parrouteid.Value = procurementBO.Route_ID;
            parNo_Of_Cans.Value = procurementBO.no_Of_Cans;
            parmilkkg.Value = procurementBO.Milk_Kg;
            parcmpid.Value = procurementBO.Company_ID;
            parraratechart.Value = procurementBO.rate_Chart;
            parmilknature.Value = procurementBO.milk_Nature;
            parsample_No.Value = procurementBO.sample_No;
            parsno.Value = procurementBO.s_No;
            partipsttime.Value = procurementBO.Milktipstarttime;
            partipendtime.Value = procurementBO.Milktipendtime;
            paruserweigherid.Value = procurementBO.Weigheruser;

            //ANALYZER    

            parfat.Value = procurementBO.fat;
            parsnf.Value = procurementBO.snf;
            parrate.Value = procurementBO.rate;
            paramount.Value = procurementBO.amount;
            parclr.Value = procurementBO.Clr;
            parfat_Kg.Value = procurementBO.FatKg;
            parsnf_Kg.Value = procurementBO.SnfKg;
            paruseranalyzerid.Value = procurementBO.Analyzeruser;
            parnetrate.Value = procurementBO.Netrate;
            parastatus.Value = procurementBO.STATUS;
            ptruck_id.Value = procurementBO.Truck_id;
           
            cmd.ExecuteNonQuery();

        }
    }
    public void setprocurementdata(BOLProcurement procurementBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parfat, parsnf, parrate, paramount, parfatkg, parclr, parnetrate, parsamno, parsnfkg, parcompanycode, parplantcode;//parnetamount
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;

            parfat = cmd.Parameters.Add("@fat", SqlDbType.Float);
            parsnf = cmd.Parameters.Add("@snf", SqlDbType.Float);
            parrate = cmd.Parameters.Add("@rate", SqlDbType.Float);
            paramount = cmd.Parameters.Add("@amount", SqlDbType.Float);          
            parclr = cmd.Parameters.Add("@clr", SqlDbType.Float);
            parfatkg = cmd.Parameters.Add("@fatkg", SqlDbType.Float);
            parsnfkg = cmd.Parameters.Add("@snfkg", SqlDbType.Float);
            parnetrate = cmd.Parameters.Add("@netrate", SqlDbType.Float);           
            parsamno = cmd.Parameters.Add("@sampleno", SqlDbType.VarChar, 15);
            parcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.NVarChar, 50);
            parplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.NVarChar, 50);

            //parnetamount = cmd.Parameters.Add("@netamount", SqlDbType.Float);
            //parusr = cmd.Parameters.Add("@sap_usr_analizer", SqlDbType.VarChar, 25);
            //parasmsstatus = cmd.Parameters.Add("@smsstatus", SqlDbType.NVarChar, 50);

            parfat.Value = procurementBO.fat;
            parsnf.Value = procurementBO.snf;
            parrate.Value = procurementBO.rate;
            paramount.Value = procurementBO.amount;
            parclr.Value = procurementBO.Clr;
            parfatkg.Value = procurementBO.FatKg;
            parsnfkg.Value = procurementBO.SnfKg;           
            parnetrate.Value = procurementBO.Netrate;          
            parsamno.Value = procurementBO.sample_No;
            parcompanycode.Value = procurementBO.Company_ID;
            parplantcode.Value = procurementBO.CentreID;


            //parnetamount.Value = procurementBO.NetAmount;
            //parusr.Value = procurementBO.SapuserAnalizer;
            //parasmsstatus.Value = procurementBO.smsstatus;
            cmd.ExecuteNonQuery();
        }
    }
}
