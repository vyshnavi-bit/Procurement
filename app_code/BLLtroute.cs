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
/// Summary description for BLLtroute
/// </summary>
public class BLLtroute
{
    BOLvehicle vehicleBO = new BOLvehicle();
    DALtroute trouteDA = new DALtroute();
	public BLLtroute()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InsertRouteSupervisorAllotment(BOLvehicle vehicleBO)
    {
        string sql = "Insert_RouteSupervisorAllotment";
        trouteDA.insertRouteSupervisorAllotment(vehicleBO, sql);
    }
    public void inserttroute(BOLvehicle vehicleBO)
    {
        string sql = "Insert_TruckRouteAllotment";
        trouteDA.insertTroute(vehicleBO, sql);
    }
    public string insertTroutedistance(BOLvehicle vehicleBO)
    {
        string mes = string.Empty;
        string sql = "Insert_TruckRouteDistance";
        mes = trouteDA.insertRroutedistance(vehicleBO, sql);
        return mes;
    }
}
