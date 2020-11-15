using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BLLImportDB
/// </summary>
public class BLLImportDB
{
    DALImportDB impdbDA = new DALImportDB();
    BOLRateChart rateBO = new BOLRateChart();
	public BLLImportDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void deleteimport(BOLRateChart rateBO)
    {
        string sql = "update_procurementImport";
        impdbDA.deleteimport(rateBO, sql);

    }
    public void deleteimportsess(BOLRateChart rateBO)
    {
        try
        {
            string sql = "update_procurementImport_sessions";
            impdbDA.importsess(rateBO, sql);
        }
        catch
        {

        }

    }

    public void actualdeleteimportsess(BOLRateChart rateBO)
    {
        try
        {
            string sql = "update_actualprocurementImport_sessions";
            impdbDA.importsess(rateBO, sql);
        }
        catch
        {

        }

    }
}