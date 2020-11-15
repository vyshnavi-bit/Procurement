using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for BOLD_Accoun
/// </summary>
public class BOLD_Accoun
{
    string strsql;
    DbHelper dbaccess = new DbHelper();
    DataSet ds = new DataSet();

    private int _id;
    public int ID
    {
        get
        {
            return _id;
        }
        set
        {
            this._id = value;
        }
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            this._name = value;
        }
    }


	public BOLD_Accoun()
	{
        strsql=string.Empty;
        _id = 0;
        _name = string.Empty;
	}

   
   
   
}