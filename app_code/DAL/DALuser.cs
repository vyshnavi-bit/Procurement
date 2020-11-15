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
/// Summary description for DALuser
/// </summary>
public class DALuser : DbHelper
{
    int i = 0;
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();

    public DALuser()
    {

    }
    public void insertUser(BOLuser userBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {

            SqlCommand cmd = new SqlCommand();
            SqlParameter partid,paruserid,parcompanyid, parusrpwd, paruserroles, parfirstname, parlastname, parhits, parphone, parmobile, paremail, parfax, parorganization, paraddress, parregdate,parcity,parcountry,parusers,parplantid;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            partid = cmd.Parameters.Add("@Table_ID",SqlDbType.Int);
            paruserid = cmd.Parameters.Add("@Users_ID", SqlDbType.NVarChar, 50);
            parcompanyid = cmd.Parameters.Add("@Company_ID", SqlDbType.Int);
            parplantid = cmd.Parameters.Add("@Plant_ID", SqlDbType.Int);
            parusrpwd = cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
            paruserroles = cmd.Parameters.Add("@Roles", SqlDbType.Int);

            parfirstname = cmd.Parameters.Add("@First_Name", SqlDbType.NVarChar, 25);
            parlastname = cmd.Parameters.Add("@Last_Name", SqlDbType.NVarChar, 25);
            parhits = cmd.Parameters.Add("@Hints", SqlDbType.NVarChar, 25);
            parphone = cmd.Parameters.Add("@Phone_Number", SqlDbType.NVarChar, 30);
            parmobile = cmd.Parameters.Add("@Mobile_Number", SqlDbType.NVarChar, 30);
            paremail = cmd.Parameters.Add("@Email_ID", SqlDbType.NVarChar, 25);
            parfax = cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30);
            parorganization = cmd.Parameters.Add("@Organization_Name", SqlDbType.NVarChar, 50);
            paraddress = cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 200);
            //parpincode = cmd.Parameters.Add("@pin_code", SqlDbType.NVarChar, 30);
            parregdate = cmd.Parameters.Add("@Added_Date", SqlDbType.DateTime);
            parcity = cmd.Parameters.Add("@City", SqlDbType.NVarChar, 30);
            parcountry = cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 50);
            parusers = cmd.Parameters.Add("@No_Of_User", SqlDbType.Int);

            partid.Value = userBO.Table_ID;
            paruserid.Value = userBO.UserID;
            parcompanyid.Value = userBO.CompanyID;
            parplantid.Value = userBO.PlantID;
            parusers.Value = userBO.NoofUser;
            parusrpwd.Value = userBO.Password;
           paruserroles.Value = userBO.Roles;
           parfirstname.Value = userBO.First_Name;
           parlastname.Value = userBO.Last_Name;
           //parconfirmpass.Value = userBO.Conform_Password;
           parhits.Value = userBO.Hints;
           parphone.Value = userBO.Phone_Number;
           parmobile.Value = userBO.Mobile_Number;
           paremail.Value = userBO.Email_ID;
           parfax.Value = userBO.Fax;
           parorganization.Value = userBO.Organization_Name;
           paraddress.Value = userBO.Address;
           //parpincode.Value = userBO.Pin_Code;
           parregdate.Value = userBO.Registration_Date;
           parcountry.Value = userBO.Country;
           parcity.Value = userBO.City;

            cmd.ExecuteNonQuery();
        }
    }
    public void editUser(BOLuser userBO, string sql)
    {
        using (con = DBaccess.GetConnection())
        {

            SqlCommand cmd = new SqlCommand();
            SqlParameter partid,paruserid, parcompanyid, parusrpwd, parplantid;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            

            partid = cmd.Parameters.Add("@Table_ID", SqlDbType.Int);
            paruserid = cmd.Parameters.Add("@Users_ID", SqlDbType.NVarChar, 50);
            parcompanyid = cmd.Parameters.Add("@Company_ID", SqlDbType.Int);
            parplantid = cmd.Parameters.Add("@Plant_ID", SqlDbType.Int);
            parusrpwd = cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50);

            partid.Value = userBO.Table_ID;
            paruserid.Value = userBO.UserID;
            parcompanyid.Value = userBO.CompanyID;
            parplantid.Value = userBO.PlantID;
            parusrpwd.Value = userBO.Password;

            cmd.ExecuteNonQuery();

            
        }
    }
    public void deleteUser(string sql)
    {
        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
        }
    }
}