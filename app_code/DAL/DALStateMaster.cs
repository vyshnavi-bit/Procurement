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
/// Summary description for DALStateMaster
/// </summary>
public class DALStateMaster : DbHelper
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();      
	public DALStateMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertStatemaster(BOLStateMaster statemasterBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parstateid, parstatename, parplantcode, parcompanycode;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            parstateid = cmd.Parameters.Add("@state_Id", SqlDbType.Int);
            parstatename = cmd.Parameters.Add("@state_Name", SqlDbType.NVarChar, 25);
        //    parplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            parcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);

            parstateid.Value = statemasterBO.stateId;
            parstatename.Value = statemasterBO.stateName;
          //  parplantcode.Value = statemasterBO.Plantcode;
            parcompanycode.Value = statemasterBO.Companycode;
            cmd.ExecuteNonQuery();

        }
    }
}
