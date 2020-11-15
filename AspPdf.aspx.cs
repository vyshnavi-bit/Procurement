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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

using System.IO;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class AspPdf : System.Web.UI.Page
{
    DALProcurement procurementDA = new DALProcurement();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string id, date, sessiontime;
        ////id = Session["txtAgentId"].ToString();
        ////date=Session["TextBox1"].ToString();
        ////sessiontime = Session["cmb_session"].ToString();
        //string filename = "current";
        //string sqlstr = "select WeightDemoTable.Agent_Name AS AgentID_Name,WeightDemoTable.milk_Kg AS Milk_Kg,WeightDemoTable.milk_Ltr AS Milk_Ltr,WeightDemoTable.fat AS Fat,WeightDemoTable.snf As Snf,WeightDemoTable.clr AS Clr,WeightDemoTable.rate AS Rate,WeightDemoTable.amount AS Amount,WeightDemoTable.net_amount AS Net_Amount from WeightDemoTable ";
        //DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        //dt = procurementDA.GetDatatable(sqlstr);
        //ds.Tables.Add(dt);
        //CreatePdf pd = new CreatePdf(ds, filename);
        ////id = string.Empty;
        
        //pd.Execute();
        ////Response.Redirect("~/110.pdf");
    }
}
