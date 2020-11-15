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
/// Summary description for BLLPortsetting
/// </summary>
public class BLLPortsetting
{
    DALPortsetting portDA = new DALPortsetting();
    DbHelper dbaccess = new DbHelper();
    string sqlstr;
	public BLLPortsetting()
	{
		//
		// TODO: Add constructor logic here
		//
        sqlstr = string.Empty;
	}
    public void insertport(BOLPortsetting portBO)
    {
        string sql = "Insert_Port";
        portDA.insertPortSetting(portBO, sql);

    }
    public void EditUpdatePort(BOLPortsetting portBOL)
    {

        sqlstr = "Edit_Port";
        portDA.EditUpdatePort(portBOL, sqlstr);

    }

    public DataTable getportnames(string ccode,string pcode)
    {
        DataTable dt = new DataTable();
        sqlstr = "SELECT * FROM Port_Setting WHERE Company_ID='" + ccode + "' AND Centre_ID ='" + pcode + "'ORDER BY Table_ID";
        dt = dbaccess.GetDatatable(sqlstr);
        return dt;
    }
    public SqlDataReader getportdatareader(string ccode, string pcode,string type)
    {
        SqlDataReader dr;
        sqlstr = "SELECT * FROM Port_Setting WHERE Company_ID='" + ccode + "' AND Centre_ID ='" + pcode + "' AND M_Type='" + type + "' ORDER BY Table_ID";
        dr = portDA.GetDatareader(sqlstr);
        return dr;
    }
}
