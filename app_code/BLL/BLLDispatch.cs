using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BLLDispatch
/// </summary>
public class BLLDispatch
{

    DALDispatch DispatchDAL = new DALDispatch();
    DbHelper dbaccess = new DbHelper();
    string sqlstr;
	public BLLDispatch()
	{
		sqlstr = string.Empty;
	}

    public void insertDispatch(BOLDispatch DispatchBOL)
    {
        string sql = "Insert_DispatchMilk";
        DispatchDAL.InsertDispatch(DispatchBOL, sql);

    }

    //public DataTable getcompalnt(string ccode, string pcode)
    //{
    //    DataTable dt = new DataTable();
    //    sqlstr = "SELECT * FROM Port_Setting WHERE Company_ID='" + ccode + "' AND Centre_ID ='" + pcode + "'ORDER BY Table_ID";
    //    dt = dbaccess.GetDatatable(sqlstr);
    //    return dt;
    //}

    public SqlDataReader LoadPlantcode()
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT Plant_Code,Plant_Name FROM Plant_Master  ORDER BY Plant_Code";
        dr =dbaccess.GetDatareader(sqlstr);
        return dr;

    }
    public SqlDataReader LoadSinglePlantcode(string ccode, string pcode)
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT Plant_Code,Plant_Name FROM Plant_Master where Company_Code='" + ccode + "' and Plant_code='" + pcode + "' ORDER BY Plant_Code";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }

    public void insertStockDetails(BOLDispatch DispatchBOL)
    {
        string sql = "Insert_StockMilk";
        DispatchDAL.InsertStock(DispatchBOL, sql);

    }

    public void openingtStockDetails(BOLDispatch DispatchBOL)
    {
        string sql = "Insert_openingStockMilk";
        DispatchDAL.InsertStock(DispatchBOL, sql);

    }

    public SqlDataReader LoadPlantcode(string ccode,string pcode)
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT Plant_Code,Plant_Name FROM Plant_Master WHERE Company_Code='" + ccode + "' and Plant_code='"+pcode+"' ORDER BY Plant_Code";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;

    }
    public SqlDataReader getopening(string rid)
    {
        SqlDataReader dr = null;
        string sqlstr = "SELECT  * from Stock_openingmilk where plant_code='" + rid + "'  ";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;

    }
    public SqlDataReader getprocureandispatch(string rid, string datee)
    {
        SqlDataReader dr = null;
        string sqlstr = "select  ( isnull(pmilkkg,0)  - isnull(dMilkKg,0)) as netmilk  from (select distinct sum(milk_kg)as pmilkkg, plant_code  from   Procurement where plant_code='" + rid + "' and prdate='" + datee + "'  group by plant_code) as procure left join (SELECT distinct sum(MilkKg) as dMilkKg,plant_code  FROM  Despatchnew WHERE plant_code='" + rid + "' and   date='" + datee + "' group by plant_code) as despat on procure.plant_code=despat.plant_code";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }

    public SqlDataReader getclosingstcok(string rid)
    {
        SqlDataReader dr = null;
        string sqlstr = " SELECT Top 1 MilkKg  as milk   FROM  Stock_Milk WHERE plant_code='" + rid + "'   order by tid desc";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }

    public SqlDataReader getopeningstcok(string ccode,string pcode,string dates)
    {
        SqlDataReader dr = null;
        string sqlstr = "select CAST(milkkg AS DECIMAL(18,2)) AS milkkg  from Stock_openingmilk where company_code='" + ccode + "'  and plant_code='" + pcode + "' and datee='" + dates + "'";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }

    public SqlDataReader getprocumentstcok(string ccode, string pcode, string dates)
    {
        SqlDataReader dr = null;
        string sqlstr = "select sum(milk_kg) as milk,round(sum(fat),2) as fat,round(sum(snf),2) as snf,round(sum(rate),2) as rates from procurement where  company_code='" + ccode + "' and plant_code='" + pcode + "' and prdate='" + dates + "'";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getdispstcok(string ccode, string pcode, string dates)
    {
        SqlDataReader dr = null;
        string sqlstr = "select isnull (Round(sum(MilkKg),2),0) as MK from Despatchnew where  company_code='" + ccode + "' and plant_code='" + pcode + "' and Date='" + dates + "'";
        dr = dbaccess.GetDatareader(sqlstr);
        return dr;
    }
    //public SqlDataReader avilmilk(string rid, string datee)
    //{
    //    SqlDataReader dr = null;
    //    string sqlstr = "select isnull(x.pmilkkg,0) as availmilk from (select distinct sum(milk_kg)as pmilkkg, plant_code  from   Procurement where plant_code='" + rid + "' and prdate='" + datee + "'  group by plant_code) x left join(SELECT top 1 * FROM Stock_openingmilk WHERE Datee ='"+datee+"' AND Plant_Code='"+rid+"') as y on x.plant_code=y.plant_code";
    //    dr = dbaccess.GetDatareader(sqlstr);
    //    return dr;
    //}

    public DataTable DespatchGriddata(string pcode, string ccode)
    {
        string Sqlstr = string.Empty;
        DataTable dt = new DataTable();
        Sqlstr = "SELECT [Tid],[Plant_To], CONVERT(VARCHAR(10),[Date],103) AS [Date],[MilkKg], [Fat], [Snf], [Rate], [Amount],CAST( [Clr] AS DECIMAL(18,2)) AS Clr FROM [Despatchnew] WHERE [Company_code] = '" + ccode + "' AND [Plant_Code] ='" + pcode + "'  ORDER BY Tid DESC";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }
    public void DespatchDlelteRow(BOLDispatch despatBOL)
    {
        string Sqlstr1 = string.Empty;
        Sqlstr1 = "Delete_Despatch";
        DispatchDAL.DespatchDeleteRow(despatBOL, Sqlstr1);
    }


    public DataTable CloseGriddata(string pcode, string ccode)
    {
        string Sqlstr = string.Empty;
        DataTable dt = new DataTable();
        Sqlstr = "SELECT [Tid],CONVERT(VARCHAR(10),[Date],103) AS [Date], [MilkKg], [Fat], [Snf], [Clr], [Rate], [Amount] FROM [Stock_Milk] WHERE [Company_code] = '" + ccode + "' AND [Plant_Code] ='" + pcode + "' ORDER BY Tid DESC";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }

    public void CloseDlelteRow(BOLDispatch despatBOL)
    {
        string Sqlstr1 = string.Empty;
        Sqlstr1 = "Delete_Despatch";
        DispatchDAL.DespatchDeleteRow(despatBOL, Sqlstr1);
    }
}