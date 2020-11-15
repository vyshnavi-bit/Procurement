using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.util.collections;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using System.IO;

public partial class SelectRatechart : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    BOLPlantwiseRatechart Borate = new BOLPlantwiseRatechart();
    BLLPlantwiseRatechart Blrate = new BLLPlantwiseRatechart();
    BLLPlantName BllPlant = new BLLPlantName();
    public int Company_code;
    public int plant_Code;
    public static int roleid;
    BLLuser Bllusers = new BLLuser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                //uscMsgBox1.MsgBoxAnswered += MessageAnswered;
                roleid = Convert.ToInt32(Session["Role"].ToString());
                Company_code = Convert.ToInt32(Session["Company_code"]);
               // plant_Code = Convert.ToInt32(Session["Plant_Code"]);

                if (roleid == 9)
                {
                    Session["Plant_Code"] = "170".ToString();
                    plant_Code =170;
                    loadspecialsingleplant();
                }
                else
                {
                    LoadPlantName();
                    plant_Code = Convert.ToInt32(ddl_Plantname.SelectedValue);
                }


                

            
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {
                //uscMsgBox1.MsgBoxAnswered += MessageAnswered;

                Company_code = Convert.ToInt32(Session["Company_code"]);
                plant_Code = Convert.ToInt32(ddl_Plantname.SelectedValue);
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }
        }


    }
    //public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    //{
    //    if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
    //    {
    //        uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered as argument.", MessageBoxUsc_Message.enmMessageType.Info);
    //    }
    //    else
    //    {
    //        uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
    //    }
    //}

    private void LoadPlantName()
    {
        try
        {
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst(Company_code.ToString());
            ddl_Plantname.DataSource = ds;
            ddl_Plantname.DataTextField = "Plant_Name";
            ddl_Plantname.DataValueField = "plant_Code";
            ddl_Plantname.DataBind();
            if (ddl_Plantname.Items.Count > 0)
            {
            }
            else
            {
                ddl_Plantname.Items.Add("--Select PlantName--");
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(Company_code.ToString(), "170");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    ddl_Plantname.Items.Add(dr["Plant_Code"].ToString() + "_" + dr["Plant_Name"].ToString());

                }
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.ToString());
        }
    }

    protected void rd_Plantwiseratechart_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rd_Plantwiseratechart.Checked == true)
            {
                rd_Routewiseratechart.Checked = false;
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void rd_Routewiseratechart_CheckedChanged(object sender, EventArgs e)
    {
      
       
    }
    private bool validates()
    {
        if (ddl_Plantname.Text == string.Empty)
        {
            //uscMsgBox1.AddMessage("select Plantname", MessageBoxUsc_Message.enmMessageType.Attention);
            return false;
        }
        return true;
    }
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        if (Rate_type.SelectedItem.Value == "1")
        {
            Server.Transfer("RatechartPlantwise.aspx");
        }
        if (Rate_type.SelectedItem.Value == "2")
        {
            Server.Transfer("RateChartRoutewise.aspx");
        }
    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void SaveData()
    {
        try
        {
            string mess = string.Empty;
            SETBO();
            mess = Blrate.InsertPlantRoutewiseratechart(Borate);
            //uscMsgBox1.AddMessage(mess, MessageBoxUsc_Message.enmMessageType.Success);
        }
        catch (Exception ex)
        {

        }
    }
    private void SETBO()
    {
        try
        {
            Borate.Companycode = Company_code;
            Borate.Plantcode = plant_Code;
            if (rd_Plantwiseratechart.Checked == true)
            {
                Borate.ChartName = "1";
            }
            else
            {
                Borate.ChartName = "2";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_SaveData_Click(object sender, EventArgs e)
    {
        try
        {
            validates();
            if (rd_Plantwiseratechart.Checked == true)
            {
                rd_Routewiseratechart.Checked = false;
                SaveData();
                //Server.Transfer("RatechartPlantwise.aspx");
            }
            else
            {
                rd_Plantwiseratechart.Checked = false;
                SaveData();
               // Server.Transfer("RateChartRoutewise.aspx");
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void rd_Plantwiser_CheckedChanged(object sender, EventArgs e)
    {
        //if (rd_Plantwiser.Checked == true)
        //{
        //    rd_Routewise.Checked = false;

        //}
    }
    protected void rd_Routewise_CheckedChanged(object sender, EventArgs e)
    {
       
    }
}
