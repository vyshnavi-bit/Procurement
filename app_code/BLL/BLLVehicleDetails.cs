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

/// <summary>
/// Summary description for BLLVehicleDetails
/// </summary>
public class BLLVehicleDetails
{
    BOLvehicle vehicleBO = new BOLvehicle();
    DALVehicleDetails vdetailDA = new DALVehicleDetails();
    DbHelper dbaccess = new DbHelper();
    public BLLVehicleDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string insertvdetails(BOLvehicle vehicleBO)
    {
        string mes = string.Empty;
        string sql = "Insert_VehicleDetails";
        mes = vdetailDA.insertVdetails(vehicleBO, sql);
        return mes;
    }
    public string UpdateDistanceTruckPresent(BOLvehicle vehicleBO)
    {
        string mes = string.Empty;
        string sql = "Upadte_UpdateDistanceTruckPresen";
        mes = vdetailDA.UpdateDistanceTruckPresentDAL(vehicleBO, sql);
        return mes;
    }
    public int getmaxtruckid(int ccode, int pcode)
    {

        int lno = 0;

        try
        {

            string sqlstr = "select max(Truck_Id) from Vehicle_Details where Plant_Code='" + pcode + "' AND Company_Code='" + ccode + "'";
            lno = (int)vdetailDA.ExecuteScalarint(sqlstr);
            lno = ++lno;
            return lno;
        }
        catch
        {
            lno = 0;
        }
        return lno;

    }
    public DataTable LoadGriddata(int pcode, int ccode)
    {
        DataTable dt = new DataTable();

        string Sqlstr = "select Vehicle_no,Truck_Name,Truck_Id,Phone_No,Bank_Id,Ltr_Cost from Vehicle_Details WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' order by Truck_Id";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }

    public DataTable LoadGriddatavehicleDistance(string pcode, string ccode)
    {
        DataTable dt = new DataTable();

        //string Sqlstr = "select Truck_Id,CONVERT(VARCHAR(50),pdate,101) AS Pdate,Tdistance from Truck_Present WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' AND pdate BETWEEN '" + dat1 + "' AND '" + dat2 + "' order by pdate,Truck_Id";
        string Sqlstr = "select Truck_Id,Tdistance from Truck_Present WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' order by pdate,Truck_Id";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }
    public DataTable LoadGriddatavehicleDistance1(string pcode, string ccode, string dat1, string dat2)
    {
        DataTable dt = new DataTable();

        // string Sqlstr = "select Truck_Id,Tdistance,CONVERT(NVARCHAR(50), Pdate,103) AS Pdate from Truck_Present WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' order by pdate,Truck_Id";
        //string Sqlstr = "select Truck_Id,Tdistance, CONVERT(NVARCHAR(50), Pdate,101) AS Pdate,Route_Id from Truck_Present WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' AND pdate BETWEEN '" + dat1 + "' AND '" + dat2 + "' order by pdate,Truck_Id";
        //string Sqlstr = "SELECT t1.*,R1.Route_Name FROM (select Truck_Id,Tdistance, CONVERT(NVARCHAR(50), Pdate,101) AS Pdate,Route_Id AS  Route_Id from Truck_Present WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' AND pdate BETWEEN '" + dat1 + "' AND '" + dat2 + "' ) AS t1 LEFT JOIN (SELECT Route_ID AS Rid,CONVERT(Nvarchar(15), Route_ID)+'_'+Route_Name AS Route_Name  FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS R1 ON t1.Route_Id=R1.Rid order by pdate,Truck_Id";
        //string Sqlstr = "SELECT t2.*,tr.Vehicle_No AS Vehicle_No  FROM (SELECT t1.*,R1.Route_Name FROM (select Truck_Id,Tdistance, CONVERT(NVARCHAR(50), Pdate,101) AS Pdate,Route_Id AS  Route_Id from Truck_Present WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' AND pdate BETWEEN '" + dat1 + "' AND '" + dat2 + "' ) AS t1 LEFT JOIN (SELECT Route_ID AS Rid,CONVERT(Nvarchar(15), Route_ID)+'_'+Route_Name AS Route_Name  FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS R1 ON t1.Route_Id=R1.Rid ) AS t2 LEFT JOIN (SELECT Truck_Id AS Trid,CONVERT(NVARCHAR(20),Truck_Id)+'_'+Vehicle_No AS Vehicle_No FROM Vehicle_Details Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS tr ON t2.Truck_Id=tr.Trid";
        string Sqlstr = "SELECT t2.*,tr.Vehicle_No AS Vehicle_No  FROM (SELECT t1.*,R1.Route_Name FROM (select Truck_Id,Tdistance,ISNULL(VTSDistance,0) AS Vtsdisance,ISNULL(AdminDistance,0) AS Admindisance, CONVERT(NVARCHAR(50), Pdate,101) AS Pdate,Route_Id AS  Route_Id from Truck_Present WHERE Plant_Code='" + pcode + "' and Company_code='" + ccode + "' AND pdate BETWEEN '" + dat1 + "' AND '" + dat2 + "' ) AS t1 LEFT JOIN (SELECT Route_ID AS Rid,CONVERT(Nvarchar(15), Route_ID)+'_'+Route_Name AS Route_Name  FROM Route_Master Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS R1 ON t1.Route_Id=R1.Rid ) AS t2 LEFT JOIN (SELECT Truck_Id AS Trid,CONVERT(NVARCHAR(20),Truck_Id)+'_'+Vehicle_No AS Vehicle_No FROM Vehicle_Details Where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "') AS tr ON t2.Truck_Id=tr.Trid ORDER BY Truck_Id";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }
}
