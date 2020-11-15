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
using System.IO;

/// <summary>
/// Summary description for BLLPlantName
/// </summary>
public class BLLPlantName
{
    DbHelper dbaccess = new DbHelper();
  
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();


	public BLLPlantName()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet LoadPlantNameChkLst(string ccode)
    {
        ds = null;
       
        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }

   
    public DataSet LoadPlantNameMilktype(string ccode, string milkval)
    {
        ds = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND Milktype='" + milkval + "') AS t1 LEFT JOIN  ( SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "'  ) AS t2 ON t1.Plant_Code=t2.pcode ORDER BY Plant_Code";
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }
    public DataSet LoadPlantNameChkLst1(string ccode)
    {
        ds = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code,pcode+'_'+Plant_Name AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "'  ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }

    public DataSet Loadstockcategorytype(string StockGroupId)
    {
        ds = null;
        string sqlstr = "SELECT CONVERT(Nvarchar,StockGroupID)+'_'+(CONVERT(Nvarchar,StockSubGroupID)+'_'+StockSubGroup)  AS SID FROM Stock_Master Where StockGroupID='" + StockGroupId + "' Order By Tid";
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }

    public string LoadstockcategoryeAvailBalance(string pcode, string StockGroupHeadId, string StockGroupSubheadId)
    {
        string val = string.Empty;
        string sqlstr = " SELECT ItemRate+'_'+CONVERT(Nvarchar,CategoryAvail) AS CategoryAvail FROM " +
" (SELECT StockGroupID,StockSubGroupID,(ISNULL(Sqty,0)-ISNULL(SDm_Quantity,0)) AS CategoryAvail  FROM " +
" (SELECT sm.StockGroupID,sm.StockSubGroupID,ISNULL(pm.Sqty,0) AS Sqty FROM " +
" (SELECT StockGroupID,StockSubGroupID FROM Stock_Master WHERE StockGroupID='" + StockGroupHeadId + "' AND StockSubGroupID='" + StockGroupSubheadId + "') AS sm " +
" LEFT JOIN " +
" (SELECT SUM(qty) AS Sqty,StockSubgroup AS pm_StockSubgroup FROM PlantStockMaster  Where Plant_code='" + pcode + "' AND stockcode='" + StockGroupHeadId + "' AND StockSubgroup='" + StockGroupSubheadId + "' GROUP BY StockSubgroup ) AS pm ON sm.StockSubGroupID=pm.pm_StockSubgroup) AS f1 " +
" LEFT JOIN " +
" (SELECT ISNULL(SUM(Dm_Quantity),0) AS SDm_Quantity,Dm_StockSubGroupId FROM DeductionDetails_Master  Where Dm_Plantcode='" + pcode + "' AND Dm_StockGroupId='" + StockGroupHeadId + "' AND Dm_StockSubGroupId='" + StockGroupSubheadId + "' AND Dm_Status=1 GROUP BY Dm_StockSubGroupId) AS dm  ON f1.StockSubGroupID=dm.Dm_StockSubGroupId ) As f2 " +
" LEFT JOIN  " +
" (SELECT CONVERT(Nvarchar,ItemRate) AS ItemRate,Stock_Type,Stock_category FROM StockRateSetting  Where Plant_code='" + pcode + "' AND  Stock_Type='" + StockGroupHeadId + "' AND Stock_category='" + StockGroupSubheadId + "' AND Fixstatus=1) AS sr ON  f2.StockGroupID=sr.Stock_Type AND f2.StockSubGroupID=Stock_category ";
        val = dbaccess.ExecuteScalarstr(sqlstr);
        return val;
    }


    public DataSet LoadSinglePlantNameChkLst1(string ccode, string pcode)
    {
        ds = null;

        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        // string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code, CONVERT(NVARCHAR(15),pcode+'_'+Plant_Name) AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' AND plant_Code='" + pcode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }
    public DataTable DTLoadPlantNameChkLst(string ccode, string d1, string d2)
    {
        dt = null;
        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        //string sqlstr = "SELECT plant_Code,CONVERT(nvarchar(50),Plant_Code)+'_'+Plant_Name AS Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT plant_Code,pcode+'_'+Plant_Name AS Plant_Name FROM (SELECT DISTINCT(Plant_code) AS pcode FROM PROCUREMENT WHERE Company_code='" + ccode + "' AND  prdate BETWEEN '" + d1.ToString() + "' AND '" + d1.ToString() + "' ) AS t1 LEFT JOIN  (SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' ) AS t2 ON t1.pcode=t2.Plant_Code ORDER BY Plant_Code";

        dt = dbaccess.GetDatatable(sqlstr);
        return dt;
    }

    public SqlDataReader LoadAdminReportPlants(string ccode,string frm,string to)
    {
        SqlDataReader dr = null;
        //string sqlstr = "SELECT plant_Code, Plant_Name FROM PLANT_MASTER WHERE Company_Code='" + ccode + "'";
        string sqlstr = "SELECT Plant_Code,ROUND(SUM(ROUND(Milk_kg,2,1)),2,1) AS Smkg,ROUND(SUM(ROUND(Milk_ltr,2,1)),2,1) AS Smltr,ROUND(AVG(ROUND(FAT,1,1)),1,1) AS AvgFat,ROUND(AVG(ROUND(SNF,1,1)),1,1) AS AvgSnf,ROUND(SUM(ROUND(Amount,2,1)),2,1) AS SAmt,ROUND((ROUND(SUM(ROUND(Amount,2,1)),2,1)/ROUND(SUM(ROUND(Milk_ltr,2,1)),2,1)),2,1) AS MRate FROM Procurement WHERE prdate BETWEEN '" + frm.Trim() + "' AND '" + to.Trim() + "' AND Company_Code='" + ccode + "' GROUP BY Plant_Code ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
}
