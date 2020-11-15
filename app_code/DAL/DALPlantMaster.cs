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
/// Summary description for DALPlantMaster
/// </summary>
public class DALPlantMaster : DbHelper 
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALPlantMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertplantmaster( BOLPlantMaster plantmasterBO,string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parplantcode, parplantname, parplantaddress, parplantphoneno, parcompanycode,parmanagername,parmanaphone,parpmail;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            parplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            parplantname = cmd.Parameters.Add("@Plant_Name", SqlDbType.NVarChar, 50);
            parplantaddress = cmd.Parameters.Add("@Plant_Address", SqlDbType.NVarChar, 100);
            parplantphoneno = cmd.Parameters.Add("@Plant_PhoneNo", SqlDbType.NVarChar, 50);
            parcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            parmanagername = cmd.Parameters.Add("@Manager_Name", SqlDbType.NVarChar, 50);
            parmanaphone = cmd.Parameters.Add("@Mana_PhoneNo", SqlDbType.NVarChar, 50);
            parpmail = cmd.Parameters.Add("@Pmail", SqlDbType.NVarChar, 50);

         
            parplantcode.Value = Convert.ToInt32(plantmasterBO.Plantcode);
            parcompanycode.Value = Convert.ToInt32(plantmasterBO.Companycode);
            parplantname.Value = plantmasterBO.Plantname;
            parplantaddress.Value = plantmasterBO.Plantaddress;
            parplantphoneno.Value = plantmasterBO.plantphoneno;
            parmanagername.Value = plantmasterBO.Managername;
            parmanaphone.Value = plantmasterBO.Manaphoneno;
            parpmail.Value = plantmasterBO.Pmail;

            cmd.ExecuteNonQuery();
        }

    }
}
