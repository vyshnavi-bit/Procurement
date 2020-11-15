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


public class BOLDispatch
{
    private int _tid;
    private int _flag;
    private DateTime _Date;
    private string _From_plant;
    private string _To_Plant;
    private string _Session;
    private double _Milk_Kg;
    //private double _Dispatch_MilkKg;
    private double _Fat;
    private double _Snf;
    private double _clr;
    private double _Rate;
    //private double _TotalMilklg;
    private int _Company_code;
    private string _Plant_Code;
   // private double _Close_Milkkg;
    private double _amount;
    private string _Type;
    private string _plantname;
    private string _StorageName;

    private double _fatkg;
    private double _snfkg;
    private string _Tankerno;
    private string _status;
    private string _tcnum;
    //Stock Details

    
     

	public BOLDispatch()
	{
        _tid = 0;
        _flag = 0;
        _From_plant = string.Empty;
        _To_Plant = string.Empty;
        _Date = System.DateTime.Now;
         _Session = string.Empty;
        _Company_code = 0;
        _Plant_Code=  string.Empty;
        _Milk_Kg = 0;
        _Fat = 0;
        _Snf = 0;
        _clr = 0;
        _Rate = 0;
        _amount = 0;
        _Type = string.Empty;
        _StorageName = string.Empty;
        _plantname = string.Empty;
        _fatkg = 0;
        _snfkg = 0;
        _Tankerno = string.Empty;
        _status = string.Empty;
        _tcnum = string.Empty;

	}


    public int Tid
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

    public int Flag
    {
        get
        {
            return this._flag;
        }
        set
        {
            this._flag = value;
        }
    }

    public double FATKG
    {
        get
        {
            return this._fatkg;
        }
        set
        {
            this._fatkg = value;
        }
    }

    public double SNFKG
    {

        get
        {
            return this._snfkg;
        }
        set
        {
            this._snfkg = value;
        }

    }

    public String PlantName
    {
        get
        {
            return this._plantname;
        }
        set
        {
            this._plantname = value;

        }
    }

    public string STORAGENAME
    {
        get
        {
            return this._StorageName;
        }
        set
        {
            this._StorageName = value;
        }

    }



    public DateTime FromDate
    {
        get
        {
            return this._Date;
        }
        set
        {
           this._Date = value;
        }
    }
    public string TYPE
    {

        get
        {
            return this._Type;
        }
        set
        {
            this._Type = value;
        }
    }

 

    public string Session
    {
        get
        {

            return this._Session;
        }

        set
        {
            this._Session = value;
        }

    }
    public int Companycode
    {

        get
        {
            return this._Company_code;
        }
        set
        {
            this._Company_code = value;
        }


    }
    public string Plantcode
    {
        get
        {
            return this._Plant_Code;
        }
        set
        {
            this._Plant_Code = value;
        }
    }
    public double MilkKg
    {
        get
        {
            return this._Milk_Kg;
        }
        set
        {
            this._Milk_Kg = value;
        }

    }
    public string From_Plant
    {

        get
        {

            return this._From_plant;
        }
        set
        {
            this._From_plant = value;
        }
    }
    public string To_Plant
    {
        get
        {

            return this._To_Plant;
        }
        set
        {
            this._To_Plant = value;

        }

    }
    public double Fat
    {
        get
        {
            return this._Fat;
        }
        set
        {
            this._Fat = value;
        }


    }
    public double Snf
    {
        get
        {
            return this._Snf;
        }
        set
        {
            this._Snf = value;
        }



    }
    public double Clr
    {
        get
        {
            return this._clr;
        }

        set
        {
            this._clr = value;
        }

    }

    public double Rate
    {
        get
        {
            return this._Rate;
        }
        set
        {
            this._Rate = value;
        }

    }

   
    public double Amount
    {
        get
        {
            return this._amount;
        }
        set
        {
            this._amount = value;
        }
    }

    public string TANKARNO
    {
        get
        {
            return this._Tankerno;
        }
        set
        {
            this._Tankerno= value;
        }

    }
    //public string STATUS
    //{
    //    get
    //    {
    //        return this._status;
    //    }
    //    set
    //    {
    //        this._status = value;
    //    }

    //}


    public string TCNUMBER
    {

        get
        {
            return this._tcnum;
        }
        set
        {
            this._tcnum = value;
        }


    }

   
}