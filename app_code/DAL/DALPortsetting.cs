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
/// Summary description for DALPortsetting
/// </summary>
public class DALPortsetting : DbHelper
{
    int i = 0;
    int inser;
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALPortsetting()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertPortSetting(BOLPortsetting portsettingBO, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parcenterid, partid, parportname, parbaudrate, parmtype, parcompanyid;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;


            parcenterid = cmd.Parameters.Add("@Centre_ID", SqlDbType.NVarChar, 50);

            partid = cmd.Parameters.Add("@Table_ID", SqlDbType.Int);
            parportname = cmd.Parameters.Add("@Port_Name", SqlDbType.NVarChar, 50);
            
            parbaudrate = cmd.Parameters.Add("@Baud_Rate", SqlDbType.NVarChar, 50);
            parmtype = cmd.Parameters.Add("@M_Type",SqlDbType.NVarChar,50);

            parcompanyid = cmd.Parameters.Add("@company_ID",SqlDbType.NVarChar,50);

            parcenterid.Value = portsettingBO.CentreID;

            partid.Value = portsettingBO.TableID;
            parportname.Value = portsettingBO.PortName;

            parbaudrate.Value = portsettingBO.BaudRate;
            parmtype.Value = portsettingBO.MType;

            parcompanyid.Value=portsettingBO.Companyid;
            cmd.ExecuteNonQuery();

        }
    }
    public void EditUpdatePort(BOLPortsetting portsettingBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parcenterid, partid, parportname, parbaudrate, parmtype, parcompanyid;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;


            parcenterid = cmd.Parameters.Add("@Centre_ID", SqlDbType.NVarChar, 50);

            partid = cmd.Parameters.Add("@Table_ID", SqlDbType.Int);
            parportname = cmd.Parameters.Add("@Port_Name", SqlDbType.NVarChar, 50);

            parbaudrate = cmd.Parameters.Add("@Baud_Rate", SqlDbType.NVarChar, 50);
            parmtype = cmd.Parameters.Add("@M_Type", SqlDbType.NVarChar, 50);

            parcompanyid = cmd.Parameters.Add("@company_ID", SqlDbType.NVarChar, 50);

            parcenterid.Value = portsettingBO.CentreID;

            partid.Value = portsettingBO.TableID;
            parportname.Value = portsettingBO.PortName;

            parbaudrate.Value = portsettingBO.BaudRate;
            parmtype.Value = portsettingBO.MType;

            parcompanyid.Value = portsettingBO.Companyid;

            inser= cmd.ExecuteNonQuery();
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
}
