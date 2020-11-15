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
/// Summary description for BLLPlantwiseRatechart
/// </summary>
public class BLLPlantwiseRatechart
{
    private string Sqlstr;
    DALPlantwiseRatechart dalrate = new DALPlantwiseRatechart();

	public BLLPlantwiseRatechart()
	{
        Sqlstr = string.Empty;
	}

    public string InsertPlantwiseratechart(BOLPlantwiseRatechart prate)
    {
        string mes = string.Empty;
        Sqlstr = "Insert_PlantwiseRatechart";
        mes = dalrate.DInsertPlantwiseratechart(prate, Sqlstr);
        return mes;

    }
    public string InsertRoutewiseratechart(BOLPlantwiseRatechart prate)
    {
        string mes = string.Empty;
        Sqlstr = "Insert_RoutewiseRatechart";
        mes = dalrate.DInsertRoutewiseratechart(prate, Sqlstr);
        return mes;
    }
    public string InsertPlantRoutewiseratechart(BOLPlantwiseRatechart prate)
    {
        string mes = string.Empty;
        Sqlstr = "Insert_PlantRoutewiseRatechart";
        mes = dalrate.DInsertPlantRoutewiseratechart(prate, Sqlstr);
        return mes;

    }
    public string InsertPlantAgentwiseratechart(BOLPlantwiseRatechart prate)
    {
        string mes = string.Empty;
        Sqlstr = "Insert_PlantAgentwiseRatechart";
        mes = dalrate.DInsertAgentwiseratechart(prate, Sqlstr);
        return mes;

    }
}
