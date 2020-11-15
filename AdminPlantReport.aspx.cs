using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

public partial class AdminPlantReport : System.Web.UI.Page
{

    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DateTime dt1 = new DateTime();

    public  int plant_code;
    public double Milkkg, Milkkg1, Milkkg2, Milkkg3, Milkkg4, Milkkg5, Milkkg6, Milkkg7, Milkkg8, Milkkg9, Milkkg10, Milkkg11, Milkkg12, Milkkg13;
    public double Fat, Fat1, Fat2, Fat3, Fat4, Fat5, Fat6, Fat7, Fat8, Fat9, Fat10, Fat11, Fat12, Fat13;
    public double Snf, Snf1, Snf2, Snf3, Snf4, Snf5, Snf6, Snf7, Snf8, Snf9, Snf10, Snf11, Snf12, Snf13;
    public double MAmt, MAmt1, MAmt2, MAmt3, MAmt4, MAmt5, MAmt6, MAmt7, MAmt8, MAmt9, MAmt10, MAmt11, MAmt12, MAmt13;
    public double Rate, Rate1, Rate2, Rate3, Rate4, Rate5, Rate6, Rate7, Rate8, Rate9, Rate10, Rate11, Rate12, Rate13;


    BLLPlantName BllPlant = new BLLPlantName();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();

                dt1 = System.DateTime.Now;
                txt_FromDate.Text = dt1.ToString("dd/MM/yyyy");
                txt_ToDate.Text = dt1.ToString("dd/MM/yyyy"); 

                loadport();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
       

        if (IsPostBack == true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();

                loadport();
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
    }

    private void loadport()
    {

        SqlDataReader dr = null;
        DateTime dt1 = new DateTime();
        DateTime dt2 = new DateTime();

        dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
        dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);

        string d1 = dt1.ToString("MM/dd/yyyy");
        string d2 = dt2.ToString("MM/dd/yyyy");

        dr = BllPlant.LoadAdminReportPlants(ccode, d1.Trim(),d2.Trim());
       

        while (dr.Read())
        {           
            plant_code=Convert.ToInt32(dr["Plant_Code"]);
            if (plant_code == 155)
            {
                Milkkg = Convert.ToDouble(dr["Smkg"]);
                Fat = Convert.ToDouble(dr["AvgFat"]);
                Snf = Convert.ToDouble(dr["AvgSnf"]);
                MAmt = Convert.ToDouble(dr["SAmt"]);
                Rate = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 156)
            {
                Milkkg1 = Convert.ToDouble(dr["Smkg"]);
                Fat1 = Convert.ToDouble(dr["AvgFat"]);
                Snf1 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt1 = Convert.ToDouble(dr["SAmt"]);
                Rate1 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 157)
            {
                Milkkg2 = Convert.ToDouble(dr["Smkg"]);
                Fat2 = Convert.ToDouble(dr["AvgFat"]);
                Snf2 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt2 = Convert.ToDouble(dr["SAmt"]);
                Rate2 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 158)
            {
                Milkkg3 = Convert.ToDouble(dr["Smkg"]);
                Fat3 = Convert.ToDouble(dr["AvgFat"]);
                Snf3 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt3 = Convert.ToDouble(dr["SAmt"]);
                Rate3 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 159)
            {
                Milkkg4 = Convert.ToDouble(dr["Smkg"]);
                Fat4 = Convert.ToDouble(dr["AvgFat"]);
                Snf4 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt4 = Convert.ToDouble(dr["SAmt"]);
                Rate4 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 160)
            {
                Milkkg5 = Convert.ToDouble(dr["Smkg"]);
                Fat5 = Convert.ToDouble(dr["AvgFat"]);
                Snf5 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt5 = Convert.ToDouble(dr["SAmt"]);
                Rate5 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 161)
            {
                Milkkg6 = Convert.ToDouble(dr["Smkg"]);
                Fat6 = Convert.ToDouble(dr["AvgFat"]);
                Snf6 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt6 = Convert.ToDouble(dr["SAmt"]);
                Rate6 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 162)
            {
                Milkkg7 = Convert.ToDouble(dr["Smkg"]);
                Fat7 = Convert.ToDouble(dr["AvgFat"]);
                Snf7 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt7 = Convert.ToDouble(dr["SAmt"]);
                Rate7 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 163)
            {
                Milkkg8 = Convert.ToDouble(dr["Smkg"]);
                Fat8 = Convert.ToDouble(dr["AvgFat"]);
                Snf8 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt8 = Convert.ToDouble(dr["SAmt"]);
                Rate8 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 164)
            {
                Milkkg9 = Convert.ToDouble(dr["Smkg"]);
                Fat9 = Convert.ToDouble(dr["AvgFat"]);
                Snf9 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt9 = Convert.ToDouble(dr["SAmt"]);
                Rate9 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 165)
            {
                Milkkg10 = Convert.ToDouble(dr["Smkg"]);
                Fat10 = Convert.ToDouble(dr["AvgFat"]);
                Snf10 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt10 = Convert.ToDouble(dr["SAmt"]);
                Rate10 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 166)
            {
                Milkkg11 = Convert.ToDouble(dr["Smkg"]);
                Fat11 = Convert.ToDouble(dr["AvgFat"]);
                Snf11 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt11 = Convert.ToDouble(dr["SAmt"]);
                Rate11 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 167)
            {
                Milkkg12 = Convert.ToDouble(dr["Smkg"]);
                Fat12 = Convert.ToDouble(dr["AvgFat"]);
                Snf12 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt12 = Convert.ToDouble(dr["SAmt"]);
                Rate12 = Convert.ToDouble(dr["MRate"]);
            }
            else if (plant_code == 168)
            {
                Milkkg13 = Convert.ToDouble(dr["Smkg"]);
                Fat13 = Convert.ToDouble(dr["AvgFat"]);
                Snf13 = Convert.ToDouble(dr["AvgSnf"]);
                MAmt13 = Convert.ToDouble(dr["SAmt"]);
                Rate13 = Convert.ToDouble(dr["MRate"]);
            }
        }
       
    }

   
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        loadport();
    }
}