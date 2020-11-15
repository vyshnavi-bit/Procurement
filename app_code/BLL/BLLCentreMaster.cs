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
/// Summary description for BLLCentreMaster
/// </summary>
public class BLLCentreMaster
{
    DALCentreMaster centermasterDA = new DALCentreMaster();
    string sqlstr;
	public BLLCentreMaster()
	{
        sqlstr = string.Empty;
		//
		// TODO: Add constructor logic here
		//
	}
    public void insertcentermaster(BOLCentreMaster centermasterBO)
    {
        string sql = "insert_Centermaster";
        centermasterDA.insertCentermaster(centermasterBO, sql);

    }
    public void insertsmsagentmaster()
    {
        //SqlConnection con = new SqlConnection();
        //DbHelper dbaccess = new DbHelper();
        //using (con = dbaccess.GetConnection())
        //{
        //    //Frm_centermaster c = new Frm_centermaster();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    string SMSInsert =
        //               "INSERT INTO " +
        //               "smsinsert(centre_name,producer_name)" +
        //               "VALUES " +
        //               "('" + c.txt_CenterName.Text.Trim() + "','" + c.txt_producername.Text.Trim() + "')";
        //    cmd.CommandText = SMSInsert;
        //    cmd.ExecuteNonQuery();
       // }
    }
    public void deletecentermaster(BOLCentreMaster centermasterBO)
    {
        //string sql = "delete_centermaster";
        //centermasterDA.deleteCentermaster(centermasterBO, sql);
    }
    public void updateagentmaster()
    {

    }
    public int generatecenterid(string rid, string aid)
    {
        int bno = 0;
        try
        {
            sqlstr = "SELECT MAX(Producer_Code) FROM Centre_MasterNew where Route_ID ='" + rid + "' AND Agent_ID='" + aid + "'";

            bno = (int)centermasterDA.ExecuteScalarint(sqlstr);
            bno = ++bno;

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return bno;
    }
    public SqlDataReader getroutmasterdatareader()
    {
        SqlDataReader dr;
        sqlstr = "select Route_ID,Route_Name from Route_Master ";
        dr = centermasterDA.GetDatareader(sqlstr);
        return dr;

    }
    public SqlDataReader getstatemasterdatareader()
    {
        SqlDataReader dr;
        sqlstr = "select State_ID,State_Name from State_MasterNew ";
        dr = centermasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public DataTable getCentermastertable()
    {
        sqlstr = "select * from Centre_MasterNew";
        DataTable dt = new DataTable();
        dt = centermasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public DataTable getCentermastertablebyRid(string id)
    {
        sqlstr = "select * from Centre_MasterNew WHERE Route_ID=" + id + " ORDER BY Producer_Code,Centre_Code,Route_ID";
        DataTable dt = new DataTable();
        dt = centermasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public DataTable getCentermastertablebyaid(string rid)
    {
        sqlstr = "select * from Centre_MasterNew WHERE Agent_ID =" + rid + " ORDER BY Producer_Code";
        DataTable dt = new DataTable();
        dt = centermasterDA.GetDatatable(sqlstr);
        return dt;
    }
    public SqlDataReader getcentermasterdatareader(string str)
    {
        SqlDataReader dr;
        sqlstr = "select * from Centre_MasterNew WHERE Table_ID=" + str + "";
        dr = centermasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getcentermasterdatareader()
    {
        SqlDataReader dr;
        sqlstr = "select * from Centre_MasterNew ";
        dr = centermasterDA.GetDatareader(sqlstr);
        return dr;
    }
    public SqlDataReader getcentermasterdatareader1(string id)
    {
        SqlDataReader dr;
        sqlstr = "select * from Centre_MasterNew WHERE Route_ID=" + id + "";
        dr = centermasterDA.GetDatareader(sqlstr);
        return dr;
    }
}
