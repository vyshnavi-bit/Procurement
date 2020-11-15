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
/// Summary description for BLLPlantMaster
/// </summary>
public class BLLPlantMaster
{
    DbHelper dbaccess = new DbHelper();
    DALPlantMaster plantmasterDA = new DALPlantMaster();
    string sqlstr;
	public BLLPlantMaster()
	{

        sqlstr = string.Empty;

		//
		// TODO: Add constructor logic here
		//
	}
    public void insertPlantmaster(BOLPlantMaster plantmasterBO)
    {
        string sql = "Insert_Plantmaster";
        plantmasterDA.insertplantmaster(plantmasterBO, sql);
        
    }
    public SqlDataReader getPlantdatareader(string rid, string type)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Plant_Master WHERE Plant_Code ='" + rid + "' AND Company_Code='" + type + "'";
        dr = plantmasterDA.GetDatareader(sqlstr);
        return dr;
    }
}
