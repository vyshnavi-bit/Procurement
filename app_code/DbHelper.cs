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
/// Summary description for DbHelper
/// </summary>
public class DbHelper
{
    public string connectionstring = string.Empty;
    DataSet ds = new DataSet();
    SqlDataAdapter dr = new SqlDataAdapter();
    SqlConnection con = new SqlConnection();
    object obj_lock = new object();
    public SqlCommand cmd;
    public DbHelper()
    {

        con.ConnectionString = @" SERVER=182.18.138.228;DATABASE=AMPS;UID=sa;PASSWORD=Vyshnavi@123;";
        //con.ConnectionString = @" SERVER=223.196.32.30;DATABASE=AMPS;UID=sa;PASSWORD=sap@123;";
        //con.ConnectionString = @" SERVER=223.196.32.30;DATABASE=AMPS;UID=sa;PASSWORD=sap@123;";
          //// conn.ConnectionString = @" SERVER=106.51.3.18;DATABASE=Dairy_ERP;UID=sa;PASSWORD=Vyshnavi123;";
          //// conn.ConnectionString = @" SERVER=10.0.0.36;DATABASE=Dairy_ERP;UID=sa;PASSWORD=vyshnavi123;";
    }
    public SqlConnection GetConnection()
    {
        con = new SqlConnection(getconnectionstring());
        if (con.State == ConnectionState.Closed)
            con.Open();
        return con;
    }

    //public SqlConnection GetConnection()
    //{
    //    con = new SqlConnection(@" SERVER=49.50.65.161;DATABASE=AMPS;UID=sa;PASSWORD=GH*&^%R76Ri7*&%^);");
    //    if (con.State == ConnectionState.Closed)
    //        con.Open();
    //    return con;
    //}
    public DataSet SelectQuery(SqlCommand _cmd)
    {
        lock (obj_lock)
        {
            cmd = _cmd;

            lock (cmd)
            {
                try
                {
                    DataSet ds = new DataSet();
                    con = new SqlConnection(getconnectionstring());
                    cmd.Connection = con;
                    cmd.CommandTimeout = 220;
                    con.Open();

                    //cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds, "Table");
                    con.Close();
                    return ds;
                }
                catch (Exception ex)
                {
                    con.Close();
                    throw new ApplicationException(ex.Message);
                }
            }
        }
    }
    public SqlConnection GetConnectionSAP()
    {
        con = new SqlConnection(getconnectionstringSAP());
        if (con.State == ConnectionState.Closed)
            con.Open();
        return con;
    }
    public SqlConnection GetConnectionDpu()
    {
        con = new SqlConnection(getconnectionstringdpu());
        if (con.State == ConnectionState.Closed)
            con.Open();
        return con;
    }
    public DataSet GetDataset(string cmdstr)
    {

        SqlConnection con = GetConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmdstr, con);
        da.Fill(ds);
        return ds;
    }

    public DataTable GetDatatable(string cmdstr)
    {

        SqlConnection con = GetConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandTimeout = 600;
        cmd.Connection = con;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmdstr, con);
        da.Fill(dt);
        return dt;

    }

    public SqlDataReader GetDatareader(string cmdstr)
    {

        SqlConnection con = GetConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        cmd.Connection = con;
        cmd.CommandText = cmdstr;
        dr = cmd.ExecuteReader(); 
         return dr;

                           }
    public void ExecuteNonquorey(string cmdstr)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }
    }
    public int ExecuteScalarint(string cmdstr)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            int max = Convert.ToInt32(cmd.ExecuteScalar());
            return max;
        }
    }

    public double ExecuteScalardouble(string cmdstr)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            double max = Convert.ToInt64(cmd.ExecuteScalar());
            return max;
        }
    }
    public string ExecuteScalarstr(string cmdstr)
    {
        using (SqlConnection con = GetConnection())
        {
            string retstr;
            retstr = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdstr;
            cmd.Connection = con;
            object value = cmd.ExecuteScalar();
            if (value != null)
                retstr = value.ToString();
            return retstr;
        }
    }
    private string getconnectionstring()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
        return str;
     }

    private string getconnectionstringSAP()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["AMPSConnectionStringSAP"].ConnectionString;
        return str;
    }

    private string getconnectionstringdpu()
    {
        string str = System.Configuration.ConfigurationManager.ConnectionStrings["AMPSConnectionStringDpu"].ConnectionString;
        return str;
    }

    public void closeconnection()
    {
        con.Close();
    }
    //// Common code

    public int GetPlantMilktype(string pcode)
    {
        try
        {
            int Resultval = 0;
            string str = string.Empty;
            str = "Select milktype from  plant_master where plant_code='" + pcode + "'";
            Resultval = ExecuteScalarint(str);
            return Resultval;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

}

