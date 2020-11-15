using System;
using System.Collections.Generic;
using System.Text;
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


public class BOLroutmaster
{
    private int _routid;
    private string _routname;
    private int _totdistance;
    private DateTime _addeddate;
    private bool _status;
    private bool _lstatus;
    private int _companyid;
    private int _Plantid;
    private string _PhoneNo;



    public BOLroutmaster()
    {


        _routid = 0;
        _routname = string.Empty;
        _totdistance = 0;
        _addeddate = System.DateTime.Now;
        _status = false;
        _lstatus = false;
        _companyid = 0;
        _Plantid = 0;
        _PhoneNo = string.Empty;

    }
    public int routId
    {
        get
        {
            return _routid;
        }
        set
        {
            this._routid = value;
        }
    }


    public string routName
    {
        get
        {
            return _routname;
        }
        set
        {
            this._routname = value;
        }
    }

    public int totDistance
    {
        get
        {
            return _totdistance;
        }
        set
        {
            this._totdistance = value;
        }
    }
    public DateTime addedDate
    {
        get
        {
            return _addeddate;
        }
        set
        {
            this._addeddate = value;
        }

    }
    public bool status
    {
        get
        {
            return _status;
        }
        set
        {
            this._status = value;
        }
    }
    public bool Lstatus
    {
        get
        {
            return _lstatus;
        }
        set
        {
            this._lstatus = value;
        }
    }
    public int Companyid
    {
        get
        {
            return _companyid;
        }
        set
        {
            this._companyid = value;
        }
    }
    public int Plantid
    {
        get
        {
            return _Plantid;
        }
        set
        {
            this._Plantid = value;
        }
    }
    public string Phoneno
    {
        get
        {
            return _PhoneNo;
        }
        set
        {
            this._PhoneNo = value;
        }
    }
}

