using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for BLLpayment
/// </summary>
public class BLLpayment
{
    string strsql;

    //ordinary
    private int _payment_Id;
    public int Payment_Id
    {
        get
        {
            return _payment_Id;
        }
        set
        {
            this._payment_Id = value;
        }
    }
    private DateTime _fromdate;
    public DateTime Fromdate
    {
        get
        {
            return _fromdate;
        }
        set
        {
            this._fromdate = value;
        }
    }
    private DateTime _todate;
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
    private int _company_Code;
    public int Company_Code
    {
        get
        {
            return _company_Code;
        }
        set
        {
            this._company_Code = value;
        }
    }
    private string _company_Name;
    public string Company_Name
    {
        get
        {
            return _company_Name;
        }
        set
        {
            this._company_Name = value;
        }
    }
    private int _Plant_Code;
    public int Plant_Code
    {
        get
        {
            return _Plant_Code;
        }
        set
        {
            this._Plant_Code = value;
        }
    }
    private string _Plant_Name;
    public string Plant_Name
    {
        get
        {
            return _Plant_Name;
        }
        set
        {
            this._Plant_Name = value;
        }
    }
    private int _route_id;
    public int Route_id
    {
        get
        {
            return _route_id;
        }
        set
        {
            this._route_id = value;
        }
    }
    private string _route_name;
    public string Route_name
    {
        get
        {
            return _route_name;
        }
        set
        {
            this._route_name = value;
        }
    }
    private int _agent_Id;
    public int Agent_Id
    {
        get
        {
            return _agent_Id;
        }
        set
        {
            this._agent_Id = value;
        }
    }
    private string _agent_Name;
    public string Agent_Name
    {
        get
        {
            return _agent_Name;
        }
        set
        {
            this._agent_Name = value;
        }
    }
    private string _ratechart;
    public string Ratechart
    {
        get
        {
            return _ratechart;
        }
        set
        {
            this._ratechart = value;
        }
    }
    private string _milk_Nature;
    public string Milk_Nature
    {
        get
        {
            return _milk_Nature;
        }
        set
        {
            this._milk_Nature = value;
        }
    }

    //NAME

    private string _state_Id;
    public string State_Id
    {
        get
        {
            return _state_Id;
        }
        set
        {
            this._state_Id = value;
        }
    }
    private string _state_name;
    public string State_name
    {
        get
        {
            return _state_name;
        }
        set
        {
            this._state_name = value;
        }
    }
    private string _payment_Mode;
    public string Payment_Mode
    {
        get
        {
            return _payment_Mode;
        }
        set
        {
            this._payment_Mode = value;
        }
    }
    private string _bank_Id;
    public string Bank_Id
    {
        get
        {
            return _bank_Id;
        }
        set
        {
            this._bank_Id = value;
        }
    }
    private string _bank_name;
    public string Bank_name
    {
        get
        {
            return _bank_name;
        }
        set
        {
            this._bank_name = value;
        }
    }
    private string _ifsc_code;
    public string Ifsc_code
    {
        get
        {
            return _ifsc_code;
        }
        set
        {
            this._ifsc_code = value;
        }
    }
    private string _account_Number;
    public string Account_Number
    {
        get
        {
            return _account_Number;
        }
        set
        {
            this._account_Number = value;
        }
    }
    private string _plant_MobileNo;
    public string Plant_MobileNo
    {
        get
        {
            return _plant_MobileNo;
        }
        set
        {
            this._plant_MobileNo = value;
        }
    }
    private string _superwiser_MobileNo;
    public string Superwiser_MobileNo
    {
        get
        {
            return _superwiser_MobileNo;
        }
        set
        {
            this._superwiser_MobileNo = value;
        }
    }
    private string _Words;
    public string Words
    {
        get
        {
            return _Words;
        }
        set
        {
            this._Words = value;
        }
    }


    //SUM
    private int _sCan;
    public int SCan
    {
        get
        {
            return _sCan;
        }
        set
        {
            this._sCan = value;
        }
    }
    private float _sMilk_Kg;
    public float SMilk_Kg
    {
        get
        {
            return _sMilk_Kg;
        }
        set
        {
            this._sMilk_Kg = value;
        }
    }
    private float _sMilk_ltr;
    public float SMilk_ltr
    {
        get
        {
            return _sMilk_ltr;
        }
        set
        {
            this._sMilk_ltr = value;
        }
    }
    private float _aFat;
    public float AFat
    {
        get
        {
            return _aFat;
        }
        set
        {
            this._aFat = value;
        }
    }
    private float _sKg_fat;
    public float SKg_fat
    {
        get
        {
            return _sKg_fat;
        }
        set
        {
            this._sKg_fat = value;
        }
    }
    private float _aKg_fat;
    public float AKg_fat
    {
        get
        {
            return _aKg_fat;
        }
        set
        {
            this._aKg_fat = value;
        }
    }
    private float _aSnf;
    public float ASnf
    {
        get
        {
            return _aSnf;
        }
        set
        {
            this._aSnf = value;
        }
    }
    private float _sKg_Snf;
    public float SKg_Snf
    {
        get
        {
            return _sKg_Snf;
        }
        set
        {
            this._sKg_Snf = value;
        }
    }
    private float _aKg_Snf;
    public float AKg_Snf
    {
        get
        {
            return _aKg_Snf;
        }
        set
        {
            this._aKg_Snf = value;
        }
    }
    private float _aClr;
    public float AClr
    {
        get
        {
            return _aClr;
        }
        set
        {
            this._aClr = value;
        }
    }
    private float _aRate;
    public float ARate
    {
        get
        {
            return _aRate;
        }
        set
        {
            this._aRate = value;
        }
    }
    private float _sAmount;
    public float SAmount
    {
        get
        {
            return _sAmount;
        }
        set
        {
            this._sAmount = value;
        }
    }
    private float _sIncentiveAmt;
    public float SIncentiveAmt
    {
        get
        {
            return _sIncentiveAmt;
        }
        set
        {
            this._sIncentiveAmt = value;
        }
    }
    private float _sCartageAmt;
    public float SCartageAmt
    {
        get
        {
            return _sCartageAmt;
        }
        set
        {
            this._sCartageAmt = value;
        }
    }
    private float _sSplBonusAmt;
    public float SSplBonusAmt
    {
        get
        {
            return _sSplBonusAmt;
        }
        set
        {
            this._sSplBonusAmt = value;
        }
    }

    //Loan Start
    private float _totLoandueRecovery;
    public float TotLoandueRecovery
    {
        get
        {
            return _totLoandueRecovery;
        }
        set
        {
            this._totLoandueRecovery = value;
        }
    }
    private float _totClosing_Balance;
    public float TotClosing_Balance
    {
        get
        {
            return _totClosing_Balance;
        }
        set
        {
            this._totClosing_Balance = value;
        }
    }
    //Loan End

    //Dedu Add Start
    private float _claimAmt;
    public float ClaimAmt
    {
        get
        {
            return _claimAmt;
        }
        set
        {
            this._claimAmt = value;
        }
    }
    private float _billadvance;
    public float Billadvance
    {
        get
        {
            return _billadvance;
        }
        set
        {
            this._billadvance = value;
        }
    }
    private float _canadvance;
    public float Canadvance
    {
        get
        {
            return _canadvance;
        }
        set
        {
            this._canadvance = value;
        }
    }
    private float _feed;
    public float Feed
    {
        get
        {
            return _feed;
        }
        set
        {
            this._feed = value;
        }
    }
    private float _materials;
    public float Materials
    {
        get
        {
            return _materials;
        }
        set
        {
            this._materials = value;
        }
    }
    private float _recovery;
    public float Recovery
    {
        get
        {
            return _recovery;
        }
        set
        {
            this._recovery = value;
        }
    }
    private float _others;
    public float Others
    {
        get
        {
            return _others;
        }
        set
        {
            this._others = value;
        }
    }

    //Dedu Add END

    //Grand
    private float _gTotAmount;
    public float GTotAmount
    {
        get
        {
            return _gTotAmount;
        }
        set
        {
            this._gTotAmount = value;
        }
    }
    private float _gTotAdditions;
    public float GTotAdditions
    {
        get
        {
            return _gTotAdditions;
        }
        set
        {
            this._gTotAdditions = value;
        }
    }
    private float _gTotDeductions;
    public float GTotDeductions
    {
        get
        {
            return _gTotDeductions;
        }
        set
        {
            this._gTotDeductions = value;
        }
    }
    private float _gAvgrate;
    public float GAvgrate
    {
        get
        {
            return _gAvgrate;
        }
        set
        {
            this._gAvgrate = value;
        }
    }
    private float _gAvgIncentivesRate;
    public float GAvgIncentivesRate
    {
        get
        {
            return _gAvgIncentivesRate;
        }
        set
        {
            this._gAvgIncentivesRate = value;
        }
    }
    private float _gGrossRate;
    public float GGrossRate
    {
        get
        {
            return _gGrossRate;
        }
        set
        {
            this._gGrossRate = value;
        }
    }
    private float _netAmount;
    public float NetAmount
    {
        get
        {
            return _netAmount;
        }
        set
        {
            this._netAmount = value;
        }
    }
    private float _roundOff;
    public float RoundOff
    {
        get
        {
            return _roundOff;
        }
        set
        {
            this._roundOff = value;
        }
    }




    //Extra_1 Start
    private DateTime _prdate;
    public DateTime Prdate
    {
        get
        {
            return _prdate;
        }
        set
        {
            this._prdate = value;
        }
    }
    private string _sessions;
    public string Sessions
    {
        get
        {
            return _sessions;
        }
        set
        {
            this._sessions = value;
        }
    }
    private int _can;
    public int Can
    {
        get
        {
            return _can;
        }
        set
        {
            this._can = value;
        }
    }
    private float _Milk_Kg;
    public float Milk_Kg
    {
        get
        {
            return _Milk_Kg;
        }
        set
        {
            this._Milk_Kg = value;
        }
    }
    private float _milk_ltr;
    public float Milk_ltr
    {
        get
        {
            return _milk_ltr;
        }
        set
        {
            this._milk_ltr = value;
        }
    }
    private float _fat;
    public float Fat
    {
        get
        {
            return _fat;
        }
        set
        {
            this._fat = value;
        }
    }
    private float _kg_fat;
    public float Kg_fat
    {
        get
        {
            return _kg_fat;
        }
        set
        {
            this._kg_fat = value;
        }
    }
    private float _snf;
    public float Snf
    {
        get
        {
            return _snf;
        }
        set
        {
            this._snf = value;
        }
    }
    private float _Kg_Snf;
    public float Kg_Snf
    {
        get
        {
            return _Kg_Snf;
        }
        set
        {
            this._Kg_Snf = value;
        }
    }
    private float _clr;
    public float Clr
    {
        get
        {
            return _clr;
        }
        set
        {
            this._clr = value;
        }
    }
    private float _rate;
    public float Rate
    {
        get
        {
            return _rate;
        }
        set
        {
            this._rate = value;
        }
    }
    private float _amount;
    public float Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            this._amount = value;
        }
    }
    private float _incentiveAmt;
    public float IncentiveAmt
    {
        get
        {
            return _incentiveAmt;
        }
        set
        {
            this._incentiveAmt = value;
        }
    }
    private float _cartageAmt;
    public float CartageAmt
    {
        get
        {
            return _cartageAmt;
        }
        set
        {
            this._cartageAmt = value;
        }
    }
    private float _splBonusAmt;
    public float SplBonusAmt
    {
        get
        {
            return _splBonusAmt;
        }
        set
        {
            this._splBonusAmt = value;
        }
    }
    //Extra_1 End




    DbHelper dbaccess = new DbHelper();
    DataTable dt = new DataTable();
    SqlConnection con = new SqlConnection();
    public BLLpayment()
    {
        string strsql = string.Empty;
        //ordinary
        _payment_Id = 0;
        _company_Code = 0;
        _company_Name = string.Empty;
        _Plant_Code = 0;
        _Plant_Name = string.Empty;
        _route_id = 0;
        _route_name = string.Empty;
        _agent_Id = 0;
        _agent_Name = string.Empty;
        _ratechart = string.Empty;
        _milk_Nature = string.Empty;

        //NAME
        _state_Id = string.Empty;
        _state_name = string.Empty;
        _payment_Mode = string.Empty;
        _bank_Id = string.Empty;
        _bank_name = string.Empty;
        _ifsc_code = string.Empty;
        _account_Number = string.Empty;
        _plant_MobileNo = string.Empty;
        _superwiser_MobileNo = string.Empty;
        _Words = string.Empty;


        //SUM
        _sCan = 0;
        _sMilk_Kg = 0;
        _sMilk_ltr = 0;
        _aFat = 0;
        _sKg_fat = 0;
        _aKg_fat = 0;
        _aSnf = 0;
        _sKg_Snf = 0;
        _aKg_Snf = 0;
        _aClr = 0;
        _aRate = 0;
        _sAmount = 0;
        _sIncentiveAmt = 0;
        _sCartageAmt = 0;
        _sSplBonusAmt = 0;

        _totLoandueRecovery = 0;
        _totClosing_Balance = 0;

        //Grand
        _gTotAmount = 0;
        _gTotAdditions = 0;
        _gTotDeductions = 0;
        _gAvgrate = 0;
        _gAvgIncentivesRate = 0;
        _gGrossRate = 0;
        _netAmount = 0;
        _roundOff = 0;


        //Extra_1 Start
        _sessions = string.Empty;
        _can = 0;
        _Milk_Kg = 0;
        _milk_ltr = 0;
        _fat = 0;
        _kg_fat = 0;
        _snf = 0;
        _Kg_Snf = 0;
        _clr = 0;
        _rate = 0;
        _amount = 0;
        _incentiveAmt = 0;
        _cartageAmt = 0;
        _splBonusAmt = 0;
        _claimAmt = 0;
        _billadvance = 0;
        _canadvance = 0;
        _feed = 0;
        _materials = 0;
        _recovery = 0;
        _others = 0;

        //Extra_1 End
    }

    //Rupees Inwords
    public string rupees(double rupees1)
    {
        int rupees;
        rupees = Convert.ToInt32(Math.Truncate(rupees1));
        double pais = rupees1 % 1;
        string result = "";
        int res;

        if ((rupees / 10000000) > 0)
        {
            res = rupees / 10000000;
            rupees = rupees % 10000000;
            result = result + ' ' + rupeestowords(res) + " Crore";
        }
        if ((rupees / 100000) > 0)
        {
            res = rupees / 100000;
            rupees = rupees % 100000;
            result = result + ' ' + rupeestowords(res) + " Lack";
        }
        if ((rupees / 1000) > 0)
        {
            res = rupees / 1000;
            rupees = rupees % 1000;
            result = result + ' ' + rupeestowords(res) + " Thousand";
        }
        if ((rupees / 100) > 0)
        {
            res = rupees / 100;
            rupees = rupees % 100;
            result = result + ' ' + rupeestowords(res) + " Hundred";
        }
        if ((rupees % 10) > 0)
        {
            res = rupees % 100;
            result = result + " " + rupeestowords(res);

        }

        result = result + ' ' + " Rupees Only ";

        return result;
    }
    private string rupeestowords(int rupees)
    {
        string result = "";
        if ((rupees >= 1) && (rupees <= 10))
        {
            if ((rupees % 10) == 1) result = "One";
            if ((rupees % 10) == 2) result = "Two";
            if ((rupees % 10) == 3) result = "Three";
            if ((rupees % 10) == 4) result = "Four";
            if ((rupees % 10) == 5) result = "Five";
            if ((rupees % 10) == 6) result = "Six";
            if ((rupees % 10) == 7) result = "Seven";
            if ((rupees % 10) == 8) result = "Eight";
            if ((rupees % 10) == 9) result = "Nine";
            if ((rupees % 10) == 0) result = "Ten";
        }
        if (rupees > 9 && rupees < 20)
        {
            if (rupees == 11) result = "Eleven";
            if (rupees == 12) result = "Twelve";
            if (rupees == 13) result = "Thirteen";
            if (rupees == 14) result = "Forteen";
            if (rupees == 15) result = "Fifteen";
            if (rupees == 16) result = "Sixteen";
            if (rupees == 17) result = "Seventeen";
            if (rupees == 18) result = "Eighteen";
            if (rupees == 19) result = "Nineteen";
        }
        if (rupees > 20 && (rupees / 10) == 2 && (rupees % 10) == 0) result = "Twenty";
        if (rupees > 20 && (rupees / 10) == 3 && (rupees % 10) == 0) result = "Thirty";
        if (rupees > 20 && (rupees / 10) == 4 && (rupees % 10) == 0) result = "Forty";
        if (rupees > 20 && (rupees / 10) == 5 && (rupees % 10) == 0) result = "Fifty";
        if (rupees > 20 && (rupees / 10) == 6 && (rupees % 10) == 0) result = "Sixty";
        if (rupees > 20 && (rupees / 10) == 7 && (rupees % 10) == 0) result = "Seventy";
        if (rupees > 20 && (rupees / 10) == 8 && (rupees % 10) == 0) result = "Eighty";
        if (rupees > 20 && (rupees / 10) == 9 && (rupees % 10) == 0) result = "Ninty";

        if (rupees > 20 && (rupees / 10) == 2 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Twenty One";
            if ((rupees % 10) == 2) result = "Twenty Two";
            if ((rupees % 10) == 3) result = "Twenty Three";
            if ((rupees % 10) == 4) result = "Twenty Four";
            if ((rupees % 10) == 5) result = "Twenty Five";
            if ((rupees % 10) == 6) result = "Twenty Six";
            if ((rupees % 10) == 7) result = "Twenty Seven";
            if ((rupees % 10) == 8) result = "Twenty Eight";
            if ((rupees % 10) == 9) result = "Twenty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 3 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Thirty One";
            if ((rupees % 10) == 2) result = "Thirty Two";
            if ((rupees % 10) == 3) result = "Thirty Three";
            if ((rupees % 10) == 4) result = "Thirty Four";
            if ((rupees % 10) == 5) result = "Thirty Five";
            if ((rupees % 10) == 6) result = "Thirty Six";
            if ((rupees % 10) == 7) result = "Thirty Seven";
            if ((rupees % 10) == 8) result = "Thirty Eight";
            if ((rupees % 10) == 9) result = "Thirty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 4 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Forty One";
            if ((rupees % 10) == 2) result = "Forty Two";
            if ((rupees % 10) == 3) result = "Forty Three";
            if ((rupees % 10) == 4) result = "Forty Four";
            if ((rupees % 10) == 5) result = "Forty Five";
            if ((rupees % 10) == 6) result = "Forty Six";
            if ((rupees % 10) == 7) result = "Forty Seven";
            if ((rupees % 10) == 8) result = "Forty Eight";
            if ((rupees % 10) == 9) result = "Forty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 5 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Fifty One";
            if ((rupees % 10) == 2) result = "Fifty Two";
            if ((rupees % 10) == 3) result = "Fifty Three";
            if ((rupees % 10) == 4) result = "Fifty Four";
            if ((rupees % 10) == 5) result = "Fifty Five";
            if ((rupees % 10) == 6) result = "Fifty Six";
            if ((rupees % 10) == 7) result = "Fifty Seven";
            if ((rupees % 10) == 8) result = "Fifty Eight";
            if ((rupees % 10) == 9) result = "Fifty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 6 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Sixty One";
            if ((rupees % 10) == 2) result = "Sixty Two";
            if ((rupees % 10) == 3) result = "Sixty Three";
            if ((rupees % 10) == 4) result = "Sixty Four";
            if ((rupees % 10) == 5) result = "Sixty Five";
            if ((rupees % 10) == 6) result = "Sixty Six";
            if ((rupees % 10) == 7) result = "Sixty Seven";
            if ((rupees % 10) == 8) result = "Sixty Eight";
            if ((rupees % 10) == 9) result = "Sixty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 7 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Seventy One";
            if ((rupees % 10) == 2) result = "Seventy Two";
            if ((rupees % 10) == 3) result = "Seventy Three";
            if ((rupees % 10) == 4) result = "Seventy Four";
            if ((rupees % 10) == 5) result = "Seventy Five";
            if ((rupees % 10) == 6) result = "Seventy Six";
            if ((rupees % 10) == 7) result = "Seventy Seven";
            if ((rupees % 10) == 8) result = "Seventy Eight";
            if ((rupees % 10) == 9) result = "Seventy Nine";
        }
        if (rupees > 20 && (rupees / 10) == 8 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Eighty One";
            if ((rupees % 10) == 2) result = "Eighty Two";
            if ((rupees % 10) == 3) result = "Eighty Three";
            if ((rupees % 10) == 4) result = "Eighty Four";
            if ((rupees % 10) == 5) result = "Eighty Five";
            if ((rupees % 10) == 6) result = "Eighty Six";
            if ((rupees % 10) == 7) result = "Eighty Seven";
            if ((rupees % 10) == 8) result = "Eighty Eight";
            if ((rupees % 10) == 9) result = "Eighty Nine";
        }
        if (rupees > 20 && (rupees / 10) == 9 && (rupees % 10) != 0)
        {
            if ((rupees % 10) == 1) result = "Ninty One";
            if ((rupees % 10) == 2) result = "Ninty Two";
            if ((rupees % 10) == 3) result = "Ninty Three";
            if ((rupees % 10) == 4) result = "Ninty Four";
            if ((rupees % 10) == 5) result = "Ninty Five";
            if ((rupees % 10) == 6) result = "Ninty Six";
            if ((rupees % 10) == 7) result = "Ninty Seven";
            if ((rupees % 10) == 8) result = "Ninty Eight";
            if ((rupees % 10) == 9) result = "Ninty Nine";
        }
        return result;
    }




    public DataTable getpaymentdatareader(string fromdate, string todate, string ccode, string pcode)
    {
        //SqlDataReader dr;
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();
        dt1 = DateTime.ParseExact(fromdate, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(todate, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        // strsql = "SELECT * FROM (SELECT t3.*,loan_details.inst_amount as loanamnt,t3.netamnt-loan_details.inst_amount as tnetamnt FROM(SELECT t1.*,t2.*,t1.snet_amount as namnt,t1.snet_amount-t2.totdeduc as netamnt FROM (SELECT agent_id, SUM(milk_kg) as smilk_kg,SUM(milk_ltr)as smilk_ltr,AVG(fat) as afat,AVG(snf) as asnf,AVG(rate) as arate,SUM(amount) as samount,SUM(com_amnt) as scomamnt,AVG(net_rate) as anetrate,SUM(fat_kg) as sfat_kg,SUM(snf_kg) as ssnf_kg,SUM(clr) as sclr,SUM(net_amount) as snet_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' group by agent_id) as t1 LEFT JOIN  (SELECT loanagent_id, billadvance,medicineadv,feedadv,canadv,other ,billadvance+medicineadv+feedadv+canadv+other as totdeduc from deduction_details WHERE deductiondate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t2 ON  t1.agent_id = t2.loanagent_id ) as t3 LEFT JOIN loan_details ON t3.agent_id = loan_details.agent_id) as t4 LEFT JOIN (SELECT route_id,state_id, agent_id as aid, prdate,session,milk_ltr,fat,snf,net_rate,net_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t5 ON t4.agent_id = t5.aid";
        //strsql = "SELECT F.ccode,F.pcode,F.Rid,F.Route_name,F.SproAid,F.Agent_Name,F.MilkType,F.RateChart_id,F.State_id,F.State_name,F.Payment_mode,F.Bank_Id,ISNULL(Ban.Bank_Name,0) AS Bank_Name,F.Ifsc_code,F.Agent_AccountNo,phone_No AS Super_PhoneNo,ISNULL(F.Scans,0) AS Scans,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS SInsentAmt,ISNULL(F.Scaramt,0) AS Scaramt,ISNULL(F.AvRate,0) AS SSplBonus,ISNULL(F.ClaimAount,0) AS ClaimAount,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS Sinstamt,ISNULL(F.balance,0) AS SLoanClosingbalance,(ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0)) AS TotAdditions,(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)) AS TotDeductions,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0))) AS NetAmount,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))-(FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))) AS Roundoff,ISNULL(F.CarAmt,0) AS AgentCarAmt,ISNULL(F.LoanAmount,0) AS SLoanAmount FROM " +
        //         "(SELECT * FROM (SELECT * FROM (SELECT * FROM  (SELECT * FROM  (SELECT * FROM " +
        //         "(SELECT SproAid,RateChart_id,MilkType,State_id,State_name,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg,CAST(Scaramt AS DECIMAL(18,2)) AS Scaramt FROM  " +
        //         "(SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,SUM(ComRate) AS Avgcrate,SUM(CAST(fat_kg AS DECIMAL(18,2))) AS Sfatkg,SUM(CAST(snf_kg AS DECIMAL(18,2))) AS SSnfkg,SUM(CAST(ISNULL(CartageAmount,0) AS DECIMAL(18,2))) AS Scaramt FROM Procurement WHERE prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'    AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro   " +
        //         "INNER JOIN  (SELECT DISTINCT(Agent_Id) AS proAid ,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid,Milk_Nature AS State_id,Milk_Nature AS State_name FROM Procurement WHERE PRDATE BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "'  ) AS pro ON Spro.SproAid=pro.proAid ) AS protot  " +
        //         "LEFT JOIN  (SELECT  Ragent_id AS DAid ,Rbilladvance AS Billadv,RAi AS Ai,RFeed AS Feed,Rcan AS can,Rrecovery AS Recovery,Rothers AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Rcompany_code='" + ccode + "' AND RPlant_Code='" + pcode + "' ) AS Dedu ON protot.SproAid=Dedu.DAid )AS prodedu " +
        //         "LEFT JOIN (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM  " +
        //         "(SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn " +
        //         " LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  " +
        //         "LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid) AS Loan  ON prodedu.SproAid=Loan.LoAid) AS pdl " +
        //         "LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS ClaimAount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_Id) AS vou ON pdl.SproAid=vou.VouAid )AS pdlv  " +
        //         "INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Ifsc_code FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON pdlv.SproAid=cart.cartAid ) AS F2 " +
        //         "LEFT JOIN  (SELECT route_id,Route_name,plant_code,company_code,phone_No FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F2.Rid=R.Route_ID ) AS F LEFT JOIN " +
        //         "(SELECT Company_code AS Bccode,Bank_id,Bank_Name  FROM Bank_Details Where Company_Code='" + ccode + "') AS Ban  ON F.Bank_Id=Ban.Bank_id ORDER  BY F.Route_ID,F.SproAid";
        //dt = dbaccess.GetDatatable(strsql);
        //return dt;



        // strsql = "SELECT * FROM (SELECT t3.*,loan_details.inst_amount as loanamnt,t3.netamnt-loan_details.inst_amount as tnetamnt FROM(SELECT t1.*,t2.*,t1.snet_amount as namnt,t1.snet_amount-t2.totdeduc as netamnt FROM (SELECT agent_id, SUM(milk_kg) as smilk_kg,SUM(milk_ltr)as smilk_ltr,AVG(fat) as afat,AVG(snf) as asnf,AVG(rate) as arate,SUM(amount) as samount,SUM(com_amnt) as scomamnt,AVG(net_rate) as anetrate,SUM(fat_kg) as sfat_kg,SUM(snf_kg) as ssnf_kg,SUM(clr) as sclr,SUM(net_amount) as snet_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' group by agent_id) as t1 LEFT JOIN  (SELECT loanagent_id, billadvance,medicineadv,feedadv,canadv,other ,billadvance+medicineadv+feedadv+canadv+other as totdeduc from deduction_details WHERE deductiondate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t2 ON  t1.agent_id = t2.loanagent_id ) as t3 LEFT JOIN loan_details ON t3.agent_id = loan_details.agent_id) as t4 LEFT JOIN (SELECT route_id,state_id, agent_id as aid, prdate,session,milk_ltr,fat,snf,net_rate,net_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t5 ON t4.agent_id = t5.aid";
        strsql = "SELECT F.ccode,F.pcode,F.Rid,F.Route_name,F.SproAid,F.Agent_Name,F.MilkType,F.RateChart_id,F.State_id,F.State_name,F.Payment_mode,F.Bank_Id,ISNULL(Ban.Bank_Name,0) AS Bank_Name,F.Ifsc_code,F.Agent_AccountNo,phone_No AS Super_PhoneNo,ISNULL(F.Scans,0) AS Scans,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS SInsentAmt,ISNULL(F.Scaramt,0) AS Scaramt,ISNULL(F.AvRate,0) AS SSplBonus,ISNULL(F.ClaimAount,0) AS ClaimAount,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS Sinstamt,ISNULL(F.balance,0) AS SLoanClosingbalance,(ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0)) AS TotAdditions,(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)) AS TotDeductions,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0))) AS NetAmount,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))-(FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))) AS Roundoff,ISNULL(F.CarAmt,0) AS AgentCarAmt,ISNULL(F.LoanAmount,0) AS SLoanAmount FROM " +
                "(SELECT * FROM (SELECT * FROM (SELECT * FROM  (SELECT * FROM  (SELECT * FROM " +
                "(SELECT SproAid,RateChart_id,MilkType,State_id,State_name,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg,CAST(Scaramt AS DECIMAL(18,2)) AS Scaramt FROM  " +
                "(SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,SUM(ComRate) AS Avgcrate,SUM(CAST(fat_kg AS DECIMAL(18,2))) AS Sfatkg,SUM(CAST(snf_kg AS DECIMAL(18,2))) AS SSnfkg,SUM(CAST(ISNULL(CartageAmount,0) AS DECIMAL(18,2))) AS Scaramt FROM Procurement WHERE prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'    AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro   " +
                "INNER JOIN  (SELECT DISTINCT(Agent_Id) AS proAid ,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid,Milk_Nature AS State_id,Milk_Nature AS State_name FROM Procurement WHERE PRDATE BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "'  ) AS pro ON Spro.SproAid=pro.proAid ) AS protot  " +
                "LEFT JOIN  (SELECT  Ragent_id AS DAid ,SUM(Rbilladvance) AS Billadv,SUM(RAi) AS Ai,SUM(RFeed) AS Feed,SUM(Rcan) AS can,SUM(Rrecovery) AS Recovery,SUM(Rothers) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Rcompany_code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY Ragent_id) AS Dedu ON protot.SproAid=Dedu.DAid )AS prodedu " +
                "LEFT JOIN (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM  " +
                "(SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn " +
                " LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  " +
                "LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid) AS Loan  ON prodedu.SproAid=Loan.LoAid) AS pdl " +
                "LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS ClaimAount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_Id) AS vou ON pdl.SproAid=vou.VouAid )AS pdlv  " +
                "INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Ifsc_code FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON pdlv.SproAid=cart.cartAid ) AS F2 " +
                "LEFT JOIN  (SELECT route_id,Route_name,plant_code,company_code,phone_No FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F2.Rid=R.Route_ID ) AS F LEFT JOIN " +
                "(SELECT Company_code AS Bccode,Bank_id,Bank_Name  FROM Bank_Details Where Company_Code='" + ccode + "') AS Ban  ON F.Bank_Id=Ban.Bank_id ORDER  BY F.Route_ID,F.SproAid";
        dt = dbaccess.GetDatatable(strsql);
        return dt;
    }
    
    public DataTable getpaymentdatareaderadmin(string fromdate, string todate, string ccode, string pcode)
    {
        //SqlDataReader dr;
        //DateTime dt1 = new DateTime();
        //DateTime dt2 = new DateTime();
        //dt1 = DateTime.ParseExact(fromdate, "dd/MM/yyyy", null);
        //dt2 = DateTime.ParseExact(todate, "dd/MM/yyyy", null);

        //string d1 = dt1.ToString("MM/dd/yyyy");
        //string d2 = dt2.ToString("MM/dd/yyyy");

        // strsql = "SELECT * FROM (SELECT t3.*,loan_details.inst_amount as loanamnt,t3.netamnt-loan_details.inst_amount as tnetamnt FROM(SELECT t1.*,t2.*,t1.snet_amount as namnt,t1.snet_amount-t2.totdeduc as netamnt FROM (SELECT agent_id, SUM(milk_kg) as smilk_kg,SUM(milk_ltr)as smilk_ltr,AVG(fat) as afat,AVG(snf) as asnf,AVG(rate) as arate,SUM(amount) as samount,SUM(com_amnt) as scomamnt,AVG(net_rate) as anetrate,SUM(fat_kg) as sfat_kg,SUM(snf_kg) as ssnf_kg,SUM(clr) as sclr,SUM(net_amount) as snet_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' group by agent_id) as t1 LEFT JOIN  (SELECT loanagent_id, billadvance,medicineadv,feedadv,canadv,other ,billadvance+medicineadv+feedadv+canadv+other as totdeduc from deduction_details WHERE deductiondate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t2 ON  t1.agent_id = t2.loanagent_id ) as t3 LEFT JOIN loan_details ON t3.agent_id = loan_details.agent_id) as t4 LEFT JOIN (SELECT route_id,state_id, agent_id as aid, prdate,session,milk_ltr,fat,snf,net_rate,net_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t5 ON t4.agent_id = t5.aid";
        //strsql = "SELECT F.ccode,F.pcode,F.Rid,F.Route_name,F.SproAid,F.Agent_Name,F.MilkType,F.RateChart_id,F.State_id,F.State_name,F.Payment_mode,F.Bank_Id,ISNULL(Ban.Bank_Name,0) AS Bank_Name,F.Ifsc_code,F.Agent_AccountNo,phone_No AS Super_PhoneNo,ISNULL(F.Scans,0) AS Scans,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS SInsentAmt,ISNULL(F.Scaramt,0) AS Scaramt,ISNULL(F.AvRate,0) AS SSplBonus,ISNULL(F.ClaimAount,0) AS ClaimAount,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS Sinstamt,ISNULL(F.balance,0) AS SLoanClosingbalance,(ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0)) AS TotAdditions,(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)) AS TotDeductions,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0))) AS NetAmount,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))-(FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))) AS Roundoff,ISNULL(F.CarAmt,0) AS AgentCarAmt,ISNULL(F.LoanAmount,0) AS SLoanAmount FROM " +
        //         "(SELECT * FROM (SELECT * FROM (SELECT * FROM  (SELECT * FROM  (SELECT * FROM " +
        //         "(SELECT SproAid,RateChart_id,MilkType,State_id,State_name,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg,CAST(Scaramt AS DECIMAL(18,2)) AS Scaramt FROM  " +
        //         "(SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,SUM(ComRate) AS Avgcrate,SUM(CAST(fat_kg AS DECIMAL(18,2))) AS Sfatkg,SUM(CAST(snf_kg AS DECIMAL(18,2))) AS SSnfkg,SUM(CAST(ISNULL(CartageAmount,0) AS DECIMAL(18,2))) AS Scaramt FROM Procurement WHERE prdate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'    AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro   " +
        //         "INNER JOIN  (SELECT DISTINCT(Agent_Id) AS proAid ,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid,Milk_Nature AS State_id,Milk_Nature AS State_name FROM Procurement WHERE PRDATE BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "'  ) AS pro ON Spro.SproAid=pro.proAid ) AS protot  " +
        //         "LEFT JOIN  (SELECT  Ragent_id AS DAid ,Rbilladvance AS Billadv,RAi AS Ai,RFeed AS Feed,Rcan AS can,Rrecovery AS Recovery,Rothers AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' AND Rcompany_code='" + ccode + "' AND RPlant_Code='" + pcode + "' ) AS Dedu ON protot.SproAid=Dedu.DAid )AS prodedu " +
        //         "LEFT JOIN (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM  " +
        //         "(SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn " +
        //         " LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  " +
        //         "LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid) AS Loan  ON prodedu.SproAid=Loan.LoAid) AS pdl " +
        //         "LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS ClaimAount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + d1.ToString() + "' AND '" + d2.ToString() + "' GROUP BY Agent_Id) AS vou ON pdl.SproAid=vou.VouAid )AS pdlv  " +
        //         "INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Ifsc_code FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON pdlv.SproAid=cart.cartAid ) AS F2 " +
        //         "LEFT JOIN  (SELECT route_id,Route_name,plant_code,company_code,phone_No FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F2.Rid=R.Route_ID ) AS F LEFT JOIN " +
        //         "(SELECT Company_code AS Bccode,Bank_id,Bank_Name  FROM Bank_Details Where Company_Code='" + ccode + "') AS Ban  ON F.Bank_Id=Ban.Bank_id ORDER  BY F.Route_ID,F.SproAid";
        //dt = dbaccess.GetDatatable(strsql);
        //return dt;



        // strsql = "SELECT * FROM (SELECT t3.*,loan_details.inst_amount as loanamnt,t3.netamnt-loan_details.inst_amount as tnetamnt FROM(SELECT t1.*,t2.*,t1.snet_amount as namnt,t1.snet_amount-t2.totdeduc as netamnt FROM (SELECT agent_id, SUM(milk_kg) as smilk_kg,SUM(milk_ltr)as smilk_ltr,AVG(fat) as afat,AVG(snf) as asnf,AVG(rate) as arate,SUM(amount) as samount,SUM(com_amnt) as scomamnt,AVG(net_rate) as anetrate,SUM(fat_kg) as sfat_kg,SUM(snf_kg) as ssnf_kg,SUM(clr) as sclr,SUM(net_amount) as snet_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' group by agent_id) as t1 LEFT JOIN  (SELECT loanagent_id, billadvance,medicineadv,feedadv,canadv,other ,billadvance+medicineadv+feedadv+canadv+other as totdeduc from deduction_details WHERE deductiondate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t2 ON  t1.agent_id = t2.loanagent_id ) as t3 LEFT JOIN loan_details ON t3.agent_id = loan_details.agent_id) as t4 LEFT JOIN (SELECT route_id,state_id, agent_id as aid, prdate,session,milk_ltr,fat,snf,net_rate,net_amount FROM procurement WHERE prdate BETWEEN '" + fromdate.ToShortDateString() + "' AND '" + todate.ToShortDateString() + "' ) as t5 ON t4.agent_id = t5.aid";
        strsql = "SELECT F.ccode,F.pcode,F.Rid,F.Route_name,F.SproAid,F.Agent_Name,F.MilkType,F.RateChart_id,F.State_id,F.State_name,F.Payment_mode,F.Bank_Id,ISNULL(Ban.Bank_Name,0) AS Bank_Name,F.Ifsc_code,F.Agent_AccountNo,phone_No AS Super_PhoneNo,ISNULL(F.Scans,0) AS Scans,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Afat,0)AS Afat,ISNULL(F.Sfatkg,0) AS Sfatkg,ISNULL(F.Asnf,0) AS Asnf,ISNULL(F.SSnfkg,0) AS SSnfkg,ISNULL(F.Aclr,0) AS Aclr,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS SInsentAmt,ISNULL(F.Scaramt,0) AS Scaramt,ISNULL(F.AvRate,0) AS SSplBonus,ISNULL(F.ClaimAount,0) AS ClaimAount,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS Sinstamt,ISNULL(F.balance,0) AS SLoanClosingbalance,(ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0)) AS TotAdditions,(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)) AS TotDeductions,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0))) AS NetAmount,((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))-(FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ACRate,0)+ISNULL(F.Scaramt,0)+ISNULL(F.AvRate,0)+ISNULL(F.ClaimAount,0))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.can,0)+ISNULL(F.Recovery,0)+ISNULL(F.others,0)+ISNULL(F.instamt,0)))) AS Roundoff,ISNULL(F.CarAmt,0) AS AgentCarAmt,ISNULL(F.LoanAmount,0) AS SLoanAmount FROM " +
                "(SELECT * FROM (SELECT * FROM (SELECT * FROM  (SELECT * FROM  (SELECT * FROM " +
                "(SELECT SproAid,RateChart_id,MilkType,State_id,State_name,ccode,pcode,Rid,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg,CAST(Scaramt AS DECIMAL(18,2)) AS Scaramt FROM  " +
                "(SELECT agent_id AS SproAid,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,SUM(ComRate) AS Avgcrate,SUM(CAST(fat_kg AS DECIMAL(18,2))) AS Sfatkg,SUM(CAST(snf_kg AS DECIMAL(18,2))) AS SSnfkg,SUM(CAST(ISNULL(CartageAmount,0) AS DECIMAL(18,2))) AS Scaramt FROM Procurement WHERE prdate BETWEEN '" + fromdate + "' AND '" + todate + "' AND Plant_Code='" + pcode + "'    AND Company_Code='" + ccode + "' GROUP BY agent_id ) AS Spro   " +
                "INNER JOIN  (SELECT DISTINCT(Agent_Id) AS proAid ,RateChart_id,Milk_Nature AS MilkType,Company_Code AS ccode,Plant_Code AS Pcode,Route_id AS Rid,Milk_Nature AS State_id,Milk_Nature AS State_name FROM Procurement WHERE PRDATE BETWEEN '" + fromdate + "' AND '" + todate + "' AND Plant_Code='" + pcode + "'   AND Company_Code='" + ccode + "'  ) AS pro ON Spro.SproAid=pro.proAid ) AS protot  " +
                "LEFT JOIN  (SELECT  Ragent_id AS DAid ,SUM(Rbilladvance) AS Billadv,SUM(RAi) AS Ai,SUM(RFeed) AS Feed,SUM(Rcan) AS can,SUM(Rrecovery) AS Recovery,SUM(Rothers) AS others FROM Deduction_Recovery WHERE RDeduction_RecoveryDate BETWEEN '" + fromdate + "' AND '" + todate + "' AND Rcompany_code='" + ccode + "' AND RPlant_Code='" + pcode + "' GROUP BY Ragent_id) AS Dedu ON protot.SproAid=Dedu.DAid )AS prodedu " +
                "LEFT JOIN (SELECT ISNULL(LoAid,0) AS LoAid,ISNULL(balance,0) AS balance,ISNULL(LoanAmount,0) AS LoanAmount,(ISNULL(loanRecAmount1,0)+ ISNULL(0,0)) AS instamt FROM  " +
                "(SELECT LoAid1 AS LoAid,balance1 AS balance,LoanAmount1 AS LoanAmount,loanRecAmount1 FROM (SELECT Agent_id AS LoAid1,CAST(SUM(balance) AS DECIMAL(18,2)) AS balance1,CAST(SUM(LoanAmount) AS DECIMAL(18,2)) AS LoanAmount1 FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  GROUP BY Agent_id) AS Lonn " +
                " LEFT JOIN (SELECT Agent_id AS LoRecAid,CAST(SUM(Paid_Amount) AS DECIMAL(18,2)) AS loanRecAmount1 FROM Loan_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND Paid_date between '" + fromdate + "' AND '" + todate + "' GROUP BY Agent_id) AS LonRec ON Lonn.LoAid1=LonRec.LoRecAid ) AS LoF  " +
                "LEFT JOIN (SELECT Agent_Id AS LoDuAid,CAST(SUM(LoanDueRecovery_Amount) AS DECIMAL(18,2)) AS loanDueRecAmount1 FROM LoanDue_Recovery WHERE Company_Code='" + ccode + "' AND Plant_code ='" + pcode + "' AND LoanRecovery_Date between '" + fromdate + "' AND '" + todate + "' GROUP BY Agent_id ) AS LonDRec ON LoF.LoAid=LonDRec.LoDuAid) AS Loan  ON prodedu.SproAid=Loan.LoAid) AS pdl " +
                "LEFT JOIN (select Agent_Id AS VouAid,CAST(SUM(Amount) AS DECIMAL(18,2))  AS ClaimAount  from Voucher_Clear where Plant_Code='" + pcode + "' AND Clearing_Date BETWEEN '" + fromdate + "' AND '" + todate + "' GROUP BY Agent_Id) AS vou ON pdl.SproAid=vou.VouAid )AS pdlv  " +
                "INNER JOIN (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Bank_Id,Payment_mode,Agent_AccountNo,Ifsc_code FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "'  ) AS cart ON pdlv.SproAid=cart.cartAid ) AS F2 " +
                "LEFT JOIN  (SELECT route_id,Route_name,plant_code,company_code,phone_No FROM Route_Master Where company_code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS R ON F2.Rid=R.Route_ID ) AS F LEFT JOIN " +
                "(SELECT Company_code AS Bccode,Bank_id,Bank_Name  FROM Bank_Details Where Company_Code='" + ccode + "') AS Ban  ON F.Bank_Id=Ban.Bank_id ORDER  BY F.Route_ID,F.SproAid";
        dt = dbaccess.GetDatatable(strsql);
        return dt;
    }

    public void insertpayment(BLLpayment paymentBO)
    {
        string str = "Insert_paymentdata";
        insertpaymentdata(paymentBO, str);
    }

    public void insertpaymentdata(BLLpayment paymentBO, string sql)
    {
        using (con = dbaccess.GetConnection())
        {

            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            SqlParameter parpayment_Id, parcompany_Code, parcompany_Name, parPlant_Code, parPlant_Name, parroute_id, parroute_name, paragent_Id, paragent_Name, parratechart, parmilk_Nature, parsessions, parcan, parMilk_Kg, parmilk_ltr, parfat, parkg_fat, parsnf, parKg_Snf, parclr, parrate, paramount, parincentiveAmt, parcartageAmt, parsplBonusAmt, parclaimAmt, parbilladvance, parcanadvance, parfeed, parmaterials, parrecovery, parothers, parsCan, parsMilk_Kg, parsMilk_ltr, paraFat, parsKg_fat, paraKg_fat, paraSnf, parsKg_Snf, paraKg_Snf, paraClr, paraRate, parsAmount, parsIncentiveAmt, parsCartageAmt, parsSplBonusAmt, partotLoandueRecovery, partotClosing_Balance, pargTotAmount, pargTotAdditions, pargTotDeductions, pargAvgrate, pargAvgIncentivesRate, pargGrossRate, parnetAmount, parroundOff, parstate_Id, parstate_name, parpayment_Mode, parbank_Id, parbank_name, parifsc_code, paraccount_Number, parplant_MobileNo, parsuperwiser_MobileNo, parWords;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;


            parpayment_Id = cmd.Parameters.Add("@payment_Id", SqlDbType.Int);
            parcompany_Code = cmd.Parameters.Add("@company_Code", SqlDbType.Int);
            parcompany_Name = cmd.Parameters.Add("@company_Name", SqlDbType.NVarChar, 50);
            parPlant_Code = cmd.Parameters.Add("@Plant_Code", SqlDbType.Int);
            parPlant_Name = cmd.Parameters.Add("@Plant_Name", SqlDbType.NVarChar, 50);
            parroute_id = cmd.Parameters.Add("@route_id", SqlDbType.Int);
            parroute_name = cmd.Parameters.Add("@route_name", SqlDbType.NVarChar, 50);
            paragent_Id = cmd.Parameters.Add("@agent_Id", SqlDbType.Int);
            paragent_Name = cmd.Parameters.Add("@agent_Name", SqlDbType.NVarChar, 50);
            parratechart = cmd.Parameters.Add("@ratechart", SqlDbType.NVarChar, 30);
            parmilk_Nature = cmd.Parameters.Add("@milk_Nature", SqlDbType.NVarChar, 10);

            parstate_Id = cmd.Parameters.Add("@state_Id", SqlDbType.NVarChar, 10);
            parstate_name = cmd.Parameters.Add("@state_name", SqlDbType.NVarChar, 50);
            parpayment_Mode = cmd.Parameters.Add("@payment_Mode", SqlDbType.NVarChar, 10);
            parbank_Id = cmd.Parameters.Add("@bank_Id", SqlDbType.NVarChar, 10);
            parbank_name = cmd.Parameters.Add("@bank_name", SqlDbType.NVarChar, 50);
            parifsc_code = cmd.Parameters.Add("@ifsc_code", SqlDbType.NVarChar, 50);
            paraccount_Number = cmd.Parameters.Add("@account_Number", SqlDbType.NVarChar, 50);
            parplant_MobileNo = cmd.Parameters.Add("@plant_MobileNo", SqlDbType.NVarChar, 50);
            parsuperwiser_MobileNo = cmd.Parameters.Add("@superwiser_MobileNo", SqlDbType.NVarChar, 50);
            parWords = cmd.Parameters.Add("@Words", SqlDbType.NVarChar, 300);

            parsCan = cmd.Parameters.Add("@sCan", SqlDbType.Float);
            parsMilk_Kg = cmd.Parameters.Add("@sMilk_Kg", SqlDbType.Float);
            parsMilk_ltr = cmd.Parameters.Add("@sMilk_ltr", SqlDbType.Float);
            paraFat = cmd.Parameters.Add("@aFat", SqlDbType.Float);
            parsKg_fat = cmd.Parameters.Add("@sKg_fat", SqlDbType.Float);
            paraSnf = cmd.Parameters.Add("@aSnf", SqlDbType.Float);
            parsKg_Snf = cmd.Parameters.Add("@sKg_Snf", SqlDbType.Float);
            paraClr = cmd.Parameters.Add("@aClr", SqlDbType.Float);
            paraRate = cmd.Parameters.Add("@aRate", SqlDbType.Float);
            parsAmount = cmd.Parameters.Add("@sAmount", SqlDbType.Float);
            parsIncentiveAmt = cmd.Parameters.Add("@sIncentiveAmt", SqlDbType.Float);
            parsCartageAmt = cmd.Parameters.Add("@sCartageAmt", SqlDbType.Float);
            parsSplBonusAmt = cmd.Parameters.Add("@sSplBonusAmt", SqlDbType.Float);

            parclaimAmt = cmd.Parameters.Add("@claimAmt", SqlDbType.Float);

            parbilladvance = cmd.Parameters.Add("@billadvance", SqlDbType.Float);
            parcanadvance = cmd.Parameters.Add("@canadvance", SqlDbType.Float);
            parfeed = cmd.Parameters.Add("@feed", SqlDbType.Float);
            parmaterials = cmd.Parameters.Add("@materials", SqlDbType.Float);
            parrecovery = cmd.Parameters.Add("@recovery", SqlDbType.Float);
            parothers = cmd.Parameters.Add("@others", SqlDbType.Float);

            partotLoandueRecovery = cmd.Parameters.Add("@totLoandueRecovery", SqlDbType.Float);
            partotClosing_Balance = cmd.Parameters.Add("@totClosing_Balance", SqlDbType.Float);


            pargTotAdditions = cmd.Parameters.Add("@gTotAdditions", SqlDbType.Float);
            pargTotDeductions = cmd.Parameters.Add("@gTotDeductions", SqlDbType.Float);
            pargAvgrate = cmd.Parameters.Add("@gAvgrate", SqlDbType.Float);
            pargAvgIncentivesRate = cmd.Parameters.Add("@gAvgIncentivesRate", SqlDbType.Float);
            pargGrossRate = cmd.Parameters.Add("@gGrossRate", SqlDbType.Float);
            parnetAmount = cmd.Parameters.Add("@netAmount", SqlDbType.Float);
            parroundOff = cmd.Parameters.Add("@roundOff", SqlDbType.Float);



            //parsessions = cmd.Parameters.Add("@sessions", SqlDbType.NVarChar, 10);
            //parcan = cmd.Parameters.Add("@can", SqlDbType.Int);
            //parMilk_Kg = cmd.Parameters.Add("@Milk_Kg", SqlDbType.Float);
            //parmilk_ltr = cmd.Parameters.Add("@milk_ltr", SqlDbType.Float);
            //parfat = cmd.Parameters.Add("@fat", SqlDbType.Float);
            //parkg_fat = cmd.Parameters.Add("@kg_fat", SqlDbType.Float);
            //parsnf = cmd.Parameters.Add("@snf", SqlDbType.Float);
            //parKg_Snf = cmd.Parameters.Add("@Kg_Snf", SqlDbType.Float);
            //parclr = cmd.Parameters.Add("@clr", SqlDbType.Float);
            //parrate = cmd.Parameters.Add("@rate", SqlDbType.Float);
            //paramount = cmd.Parameters.Add("@amount", SqlDbType.Float);
            //parincentiveAmt = cmd.Parameters.Add("@incentiveAmt", SqlDbType.Float);
            //parcartageAmt = cmd.Parameters.Add("@cartageAmt", SqlDbType.Float);
            //parsplBonusAmt = cmd.Parameters.Add("@splBonusAmt", SqlDbType.Float);  

            //paraKg_fat = cmd.Parameters.Add("@aKg_fat", SqlDbType.Float);
            //paraKg_Snf = cmd.Parameters.Add("@aKg_Snf", SqlDbType.Float);

            //pargTotAmount = cmd.Parameters.Add("@gTotAmount", SqlDbType.Float);



            parpayment_Id.Value = Payment_Id;
            parcompany_Code.Value = Company_Code;
            parcompany_Name.Value = Company_Name;
            parPlant_Code.Value = Plant_Code;
            parPlant_Name.Value = Plant_Name;
            parroute_id.Value = Route_id;
            parroute_name.Value = Route_name;
            paragent_Id.Value = Agent_Id;
            paragent_Name.Value = Agent_Name;
            parratechart.Value = Ratechart;
            parmilk_Nature.Value = Milk_Nature;

            parstate_Id.Value = State_Id;
            parstate_name.Value = State_name;
            parpayment_Mode.Value = Payment_Mode;
            parbank_Id.Value = Bank_Id;
            parbank_name.Value = Bank_name;
            parifsc_code.Value = Ifsc_code;
            paraccount_Number.Value = Account_Number;
            parplant_MobileNo.Value = Plant_MobileNo;
            parsuperwiser_MobileNo.Value = Superwiser_MobileNo;
            parWords.Value = Words;

            parsCan.Value = SCan;
            parsMilk_Kg.Value = SMilk_Kg;
            parsMilk_ltr.Value = SMilk_ltr;
            paraFat.Value = AFat;
            parsKg_fat.Value = SKg_fat;
            paraSnf.Value = ASnf;
            parsKg_Snf.Value = SKg_Snf;
            paraClr.Value = AClr;
            paraRate.Value = ARate;
            parsAmount.Value = SAmount;
            parsIncentiveAmt.Value = SIncentiveAmt;
            parsCartageAmt.Value = SCartageAmt;
            parsSplBonusAmt.Value = SSplBonusAmt;

            parclaimAmt.Value = ClaimAmt;

            parbilladvance.Value = Billadvance;
            parcanadvance.Value = Canadvance;
            parfeed.Value = Feed;
            parmaterials.Value = Materials;
            parrecovery.Value = Recovery;
            parothers.Value = Others;

            partotLoandueRecovery.Value = TotLoandueRecovery;
            partotClosing_Balance.Value = TotClosing_Balance;

            pargTotAdditions.Value = GTotAdditions;
            pargTotDeductions.Value = GTotDeductions;
            pargAvgrate.Value = GAvgrate;
            pargAvgIncentivesRate.Value = GAvgIncentivesRate;
            pargGrossRate.Value = GGrossRate;
            parnetAmount.Value = NetAmount;
            parroundOff.Value = RoundOff;



            //parsessions.Value = Sessions;
            //parcan.Value = Can;
            //parMilk_Kg.Value = Milk_Kg;
            //parmilk_ltr.Value = Milk_ltr;
            //parfat.Value = Fat;
            //parkg_fat.Value = Kg_fat;
            //parsnf.Value = Snf;
            //parKg_Snf.Value = Kg_Snf;
            //parclr.Value = Clr;
            //parrate.Value = Rate;
            //paramount.Value = Amount;
            //parincentiveAmt.Value = IncentiveAmt;
            //parcartageAmt.Value = CartageAmt;
            //parsplBonusAmt.Value = SplBonusAmt;

            //paraKg_fat.Value = AKg_fat;
            //paraKg_Snf.Value = AKg_Snf;

            //pargTotAmount.Value = GTotAmount;

            cmd.ExecuteNonQuery();
        }
    }

}


