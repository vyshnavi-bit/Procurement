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
/// Summary description for BOLbillgenerate
/// </summary>
public class BOLbillgenerate
{
    private int _companycode;
    private int _plantcode;
    private int _rout_id;

    //private DateTime _frmdate;
    //private DateTime _todate;

    private DateTime _frmdate;
    private DateTime _todate;
	public BOLbillgenerate()
	{
		
	}
    public int Companycode
    {
        get
        {
            return _companycode;
        }
        set
        {
            this._companycode = value;

        }
    }
    public int Plantcode
    {
        get
        {
            return _plantcode;
        }
        set
        {
            this._plantcode = value;
        }
    }
    public int Route_id
    {
        get
        {
            return _rout_id;
        }
        set
        {
            this._rout_id = value;
        }
    }
    public DateTime Frmdate
    {
        get
        {
            return _frmdate;
        }
        set
        {
            this._frmdate = value;
        }
    }
    public DateTime Todate
    {
        get
        {
            return _todate;
        }
        set
        {
            this._todate = value;
        }
    }

}
