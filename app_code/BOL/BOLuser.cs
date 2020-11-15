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
/// Summary description for BOLuser
/// </summary>
public class BOLuser
{
    public int _tid;
    private int _roles;
    private int _companyid;
    private int _Plantid;
    private string _firstname;
    private string _lastname;
    private string _password;
    private string _hints;
    private string _phonenumber;
    private string _mobilenumber;
    private string _emailid;
    private string _fax;
    private string _organizationname;
    private string _address;
    private string _city;
    private DateTime _registrationdate;
    private string _country;
    private int _noofuser;
    private string _userid;

    
    
    
    
    
    private string _conformpassword;
    
    
    //private string _pincode;
   
    
    

    public BOLuser()
    {
        _userid = string.Empty;
        _companyid = 0;
        _Plantid = 0;
        _password = string.Empty;
        _roles = 0;
        _firstname = string.Empty;
        _lastname = string.Empty;
        _conformpassword = string.Empty;
        _hints = string.Empty;
        _phonenumber = string.Empty;
        _mobilenumber = string.Empty;
        _emailid = string.Empty;
        _fax = string.Empty;
        _organizationname = string.Empty;
        _address = string.Empty;
        //_pincode = string.Empty;
        _tid = 0;
       
        _country = string.Empty;
        _city = string.Empty;
        _noofuser = 0;
    }
    public string UserID
    {
        get
        {
            return this._userid;
        }
        set
        {
            this._userid = value;
        }
    }
    public int CompanyID
    {
        get
        {
            return this._companyid;
        }
        set
        {
            this._companyid = value;
        }
    }
    public int PlantID
    {

        get
        {
            return this._Plantid;

        }
        set
        {
            this._Plantid = value;

        }
    }
    public int Table_ID
    {
        get
        {
            return this._tid;
        }
        set
        {
            this._tid = value;
        }
    }
    public int NoofUser
    {
        get
        {
            return this._noofuser;
        }
        set
        {
            this._noofuser = value;
        }
    }
   
    public string Password
    {
        get
        {
            return this._password;
        }
        set
        {
            this._password = value;
        }
    }
    public int Roles
    {
        get
        {
            return this._roles;
        }
        set
        {
            this._roles= value;
        }
    }

    public string First_Name
    {
        get
        {
            return this._firstname;
        }
        set
        {
            this._firstname = value;
        }
    }

    public string Last_Name
    {
        get
        {
            return this._lastname;
        }
        set
        {
            this._lastname = value;
        }
    }
    //public string Conform_Password
    //{
    //    get
    //    {
    //        return this.Conform_Password;
    //    }
    //    set
    //    {
    //        this.Conform_Password = value;
    //    }
    //}
    public string Hints
    {
        get
        {
            return this._hints;
        }
        set
        {
            this._hints = value;
        }
    }
    public string Phone_Number
    {
        get
        {
            return this._phonenumber;
        }
        set
        {
            this._phonenumber = value;
        }
    }
    public string Mobile_Number
    {
        get
        {
            return this._mobilenumber;
        }
        set
        {
            this._mobilenumber = value;
        }
    }
    public string Email_ID
    {
        get
        {
            return this._emailid;
        }
        set
        {
            this._emailid = value;
        }
    }
    public string Fax
    {
        get
        {
            return this._fax;
        }
        set
        {
            this._fax = value;
        }
    }

    public string Organization_Name
    {
        get
        {
            return this._organizationname;
        }
        set
        {
            this._organizationname = value;
        }
    }

    public string Address
    {
        get
        {
            return this._address;
        }
        set
        {
            this._address = value;
        }
    }
    public string City
    {
        get
        {
            return this._city;
        }
        set
        {
            this._city = value;
        }
    }
    public string Country
    {
        get
        {
            return this._country;
        }
        set
        {
            this._country = value;
        }
    }

    //public string Pin_Code
    //{
    //    get
    //    {
    //        return this._pincode;
    //    }
    //    set
    //    {
    //        this._pincode = value;
    //    }
    //}

    public DateTime Registration_Date
    {
        get
        {
            return this._registrationdate;
        }
        set
        {
            this._registrationdate = value;
        }
    }
}