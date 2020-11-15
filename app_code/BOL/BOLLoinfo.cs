using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOLLoinfo
/// </summary>
public class BOLLoinfo
{
    //private int _userid;
    private string _userloginId;
    //private string _userEmail;
    private string _Password;
    //private DateTime _IscreatedDate;
    //private DateTime _Lastlogin;
    private int _Roles;
    private int _Company_Id;
    private int _Plant_Id;

	public BOLLoinfo()
	{
		//
		// TODO: Add constructor logic here
		//



       // _userid = 0;
        _userloginId = string.Empty;
        //_userEmail = string.Empty;
        _Password = string.Empty;
        _Roles = 0;
        _Company_Id = 0;
        _Plant_Id = 0;
       // _IscreatedDate = System.DateTime.Now;
      //  _Lastlogin = System.DateTime.Now;


	}

    public string userloginID

    {
        get
        {
            return this._userloginId;
        }
        set
        {
            this._userloginId = value;
        }

    }
    public int roles
    {

        get
        {
            return this._Roles;
        }
        set
        {
            this._Roles = value;
        }
    }
    public string Password
    {
        get
        {
            return this._Password;

        }
        set
        {
            this._Password = value;
        }


    }
    //public string usermail
    //{

    //    get
    //    {
    //        return this._userEmail;

    //    }

    //    set
    //    {
    //        this._userEmail = value;


    //    }
    //}

    //public string password
    //{
    //    get
    //    {
    //        return this._Password;

    //    }

    //    set
    //    {
    //        this._Password = value;

    //    }
    //}
    //public int userid
    //{

    //    get
    //    {
    //        return this._userid;
    //    }
    //    set
    //    {
    //        this._userid = value;
    //    }
    //}

    public int CompanyId
    {

        get
        {
            return this._Company_Id;
        }
        set
        {
            this._Company_Id = value;
        }
    }
    public int PlantId
    {

        get
        {
            return this._Plant_Id;
        }
        set
        {
            this._Plant_Id = value;
        }
    }

}
