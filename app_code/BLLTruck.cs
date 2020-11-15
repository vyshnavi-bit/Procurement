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
/// Summary description for BLLTruck
/// </summary>
public class BLLTruck
{
    DataSet ds = new DataSet();
    SqlDataReader dr;
    DbHelper dbaccess = new DbHelper();
    public string Sqlstr;
	public BLLTruck()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet LoadTruckDetails(int ccode, int pcode)
    {
        Sqlstr = null;
        Sqlstr = "SELECT Truck_id,Truck_Name FROM Vehicle_Details WHERE Company_Code=" + ccode + " AND Plant_Code=" + pcode + "  ORDER BY Truck_id";
        ds = dbaccess.GetDataset(Sqlstr);
        return ds;
    }
    public SqlDataReader LoadTruckDetails1(int ccode, int pcode)
    {
        dr = null;
        Sqlstr = null;
        Sqlstr = "SELECT Truck_id,Vehicle_No AS Truck_Name FROM Vehicle_Details WHERE Company_Code=" + ccode + " AND Plant_Code=" + pcode + "   ORDER BY Truck_id";
        dr = dbaccess.GetDatareader(Sqlstr);
        return dr;
    }
    public DataSet LoadTruck(int ccode, int pcode)
    {       
        Sqlstr = null;
        Sqlstr = "SELECT Truck_id,Convert(Nvarchar(5),Truck_id)+'_'+ Vehicle_No AS Truck_Name FROM Vehicle_Details WHERE Company_Code=" + ccode + " AND Plant_Code=" + pcode + "   ORDER BY Truck_id";
        ds = dbaccess.GetDataset(Sqlstr);
        return ds;
    }

    public DataSet LoadSupervisorDetails(int ccode, int pcode)
    {
        DataSet ds = new DataSet();
        ds = null;
        Sqlstr = "SELECT Supervisor_Code,CAST(Supervisor_Code as NVARCHAR)+'_'+ SupervisorName AS SupervisorName FROM Supervisor_Details WHERE Company_Code=" + ccode + " AND Plant_Code=" + pcode + "  ORDER BY Supervisor_Code ";
        ds = dbaccess.GetDataset(Sqlstr);
        return ds;
    }
}
