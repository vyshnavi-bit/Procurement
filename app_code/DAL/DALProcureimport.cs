using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for DALProcureimport
/// </summary>
public class DALProcureimport
{
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
	public DALProcureimport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Procurementimport(BOLProcurement procurementBO, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter partid,parmilkkg, parmilkltr, parfat, parsnf,parclr;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;

            partid = cmd.Parameters.Add("@Tid", SqlDbType.Int);
            parmilkltr = cmd.Parameters.Add("@milk_Ltr", SqlDbType.Float);
            parmilkkg = cmd.Parameters.Add("@milk_Kg", SqlDbType.Float);
            parfat = cmd.Parameters.Add("@fat", SqlDbType.Float);
            parsnf = cmd.Parameters.Add("@snf", SqlDbType.Float);
            parclr = cmd.Parameters.Add("@clr", SqlDbType.Float);

            partid.Value = procurementBO.Tid;
            parmilkltr.Value = procurementBO.milk_Ltr;
            parmilkkg.Value = procurementBO.Milk_Kg;
            parfat.Value = procurementBO.fat;
            parsnf.Value = procurementBO.snf;
            parclr.Value = procurementBO.Clr;
            
            cmd.ExecuteNonQuery();

        }
    }
}