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
using System.IO;
public partial class localSystemTimingGridview : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string connStr = ConfigurationManager.ConnectionStrings["AMPSConnectionString"].ConnectionString;
    DateTime datte = System.DateTime.Now;
    string plantnameget;
    string name;
    string datt;
    string sess;
    DbHelper dbaccess = new DbHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
       
        Literal17.Text = datt.ToString();
        Literal19.Text = sess.ToString();
        Literal19.Visible = false;
        Session.Visible = false;
        GridView1.Visible = true;
        GridView2.Visible = false;
        GridView3.Visible = false;
        GridView4.Visible = false;
        BACK.Visible = false;

    }

    public void GetData()
    {

        datt = datte.ToString("MM/dd/yyyy");
        sess = datte.ToString("tt");
       
        SqlConnection con = new SqlConnection(connStr);
        con.Open();     
        //string str = "select pm.Plant_Code,pm.Plant_Name,localsys.OfficeSysName as NAME,localsys.Officeuserid as USERID,localsys.Officetime AS TIME,localsys.OfficeIp as IP,localsys.WeigherSysName AS WEINAME,localsys.weigherTime AS WEITIME,localsys.LabSysName AS lABNAME,localsys.LabTime AS LABTIME,localsys.LocalNetworkComputerId AS NETWORKSYSTEM from (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169  and Plant_Code!=160 ) as pm left join  ( select centre_Id,centre_name,OfficeSysName,Officeuserid,RIGHT(Officetime, 7) AS Officetime,LocalNetworkComputerId,OfficeIp,WeigherSysName,RIGHT(weigherTime, 7) AS weigherTime,weigherIp,LabSysName,RIGHT(LabTime, 7) AS LabTime,LabIp,carbertestValue,AnalyzerTestValue,GainFat from SystemIpconfiguration where    date='" + datt + "' and session='" + sess + "'   )as localsys on pm.Plant_Code=localsys.centre_Id  group by localsys.centre_Id, localsys.OfficeSysName, localsys.centre_Id, localsys.centre_name, localsys.Officeuserid, localsys.Officetime, localsys.LocalNetworkComputerId, localsys.OfficeIp, localsys.weigherTime, localsys.WeigherSysName, localsys.weigherIp, localsys.LabSysName, localsys.LabTime, localsys.LabIp, localsys.carbertestValue, localsys.AnalyzerTestValue, localsys.GainFat,pm.Milktype,pm.Plant_Code,pm.Plant_Name order by  pm.Milktype,pm.Plant_Code";
     //   string str = "select pm.Plant_Code,pm.Plant_Name,localsys.OfficeSysName as NAME,localsys.Officeuserid as USERID,localsys.Officetime AS TIME,localsys.WeigherSysName AS WEINAME,localsys.weigherTime AS WEITIME,localsys.LabSysName AS lABNAME,localsys.LabTime AS LABTIME,localsys.LocalNetworkComputerId AS NETWORKSYSTEM from (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169  and Plant_Code!=160 ) as pm left join  ( select centre_Id,centre_name,OfficeSysName,Officeuserid,RIGHT(Officetime, 7) AS Officetime,LocalNetworkComputerId,OfficeIp,WeigherSysName,RIGHT(weigherTime, 7) AS weigherTime,weigherIp,LabSysName,RIGHT(LabTime, 7) AS LabTime,LabIp,carbertestValue,AnalyzerTestValue,GainFat from SystemIpconfiguration where    date='" + datt + "' and session='" + sess + "'   )as localsys on pm.Plant_Code=localsys.centre_Id  group by localsys.centre_Id, localsys.OfficeSysName, localsys.centre_Id, localsys.centre_name, localsys.Officeuserid, localsys.Officetime, localsys.LocalNetworkComputerId, localsys.OfficeIp, localsys.weigherTime, localsys.WeigherSysName, localsys.weigherIp, localsys.LabSysName, localsys.LabTime, localsys.LabIp, localsys.carbertestValue, localsys.AnalyzerTestValue, localsys.GainFat,pm.Milktype,pm.Plant_Code,pm.Plant_Name order by  pm.Milktype,pm.Plant_Code";

        //   string str = "select pm.Plant_Code,pm.Plant_Name,localsys.OfficeSysName as NAME,localsys.Officeuserid as USERID,localsys.Officetime AS TIME,localsys.WeigherSysName AS WEINAME,localsys.weigherTime AS WEITIME,localsys.LabSysName AS lABNAME,localsys.LabTime AS LABTIME,localsys.LocalNetworkComputerId AS NETWORKSYSTEM from (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169  and Plant_Code!=160 ) as pm left join  ( select centre_Id,centre_name,OfficeSysName,Officeuserid,RIGHT(Officetime, 7) AS Officetime,LocalNetworkComputerId,OfficeIp,WeigherSysName,RIGHT(weigherTime, 7) AS weigherTime,weigherIp,LabSysName,RIGHT(LabTime, 7) AS LabTime,LabIp,carbertestValue,AnalyzerTestValue,GainFat from SystemIpconfiguration where    date='" + datt + "' and session='" + sess + "'   )as localsys on pm.Plant_Code=localsys.centre_Id  group by localsys.centre_Id, localsys.OfficeSysName, localsys.centre_Id, localsys.centre_name, localsys.Officeuserid, localsys.Officetime, localsys.LocalNetworkComputerId, localsys.OfficeIp, localsys.weigherTime, localsys.WeigherSysName, localsys.weigherIp, localsys.LabSysName, localsys.LabTime, localsys.LabIp, localsys.carbertestValue, localsys.AnalyzerTestValue, localsys.GainFat,pm.Milktype,pm.Plant_Code,pm.Plant_Name order by  pm.Milktype,pm.Plant_Code";

      //  string str = "select pm.Plant_Code,pm.Plant_Name,OfficeName,OfficesystemIp,Officetime,WeigherSysName,WeighersystemIp,Weighertime,LabSysName,LabsystemIp,Laptime from (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169 ) as pm left join  ( select plant_code,OfficeName,OfficesystemIp,RIGHT(Officesystemtime,7) as Officetime,WeigherSysName,WeighersystemIp,RIGHT(Weighersystemtime,7) as Weighertime,LabSysName,LabsystemIp,RIGHT(Labsystemtime,7) as Laptime from SystemTime where   date='" + datt + "' and session='" + sess + "'  )as localsys on pm.Plant_Code=localsys.Plant_code  group by localsys.Plant_Code, localsys.Plant_code,localsys.OfficeName,localsys.OfficesystemIp,Officetime,WeigherSysName,WeighersystemIp,Weighertime,LabSysName,LabsystemIp,Laptime,pm.Milktype,pm.Plant_Code,pm.Plant_Name order by  pm.Milktype,pm.Plant_Code";

        //string str = "select pm.Plant_Code,pm.Plant_Name,OfficeName,Officetime,WeigherSysName,Weighertime,LabSysName,Laptime from (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169 and Plant_Code!=160 ) as pm left join  ( select plant_code,OfficeName,OfficesystemIp,RIGHT(Officesystemtime,7) as Officetime,WeigherSysName,WeighersystemIp,RIGHT(Weighersystemtime,7) as Weighertime,LabSysName,LabsystemIp,RIGHT(Labsystemtime,7) as Laptime from SystemTime where   date='" + datt + "' and session='" + sess + "'  )as localsys on pm.Plant_Code=localsys.Plant_code  group by localsys.Plant_Code, localsys.Plant_code,localsys.OfficeName,localsys.OfficesystemIp,Officetime,WeigherSysName,WeighersystemIp,Weighertime,LabSysName,LabsystemIp,Laptime,pm.Milktype,pm.Plant_Code,pm.Plant_Name order by  pm.Milktype,pm.Plant_Code";


        //string str="SELECT  PP.Plant_code AS PlantCode,PP.Plant_Name as PlantName,PPT.OfficeName as OfficeSys,PPT.OFTime as Time,PPT.WeigherSysName as WeigherSys ,PPT.WETime as Time,PPT.LabSysName as LabSys,PPT.LABTime as Time  FROM (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169 and Plant_Code!=160 ) AS PP LEFT JOIN (select  dd.Plant_code,cc.OfficeName,cc.OFTime,CC.WeigherSysName,CC.WETime,dd.LabSysName,dd.LABTime from(SELECT * from (SELECT t1.Plant_code,OfficeName, OFTime =REPLACE( (SELECT  ':' + RIGHT(Officesystemtime,7)   FROM SystemTime t2  WHERE t2.plant_code = t1.plant_code  and t2.plant_code='156' and t2.OfficeName='OFFICE'  and Date='11-17-2015' and  t2.Session='PM'   ORDER BY Plant_code,Tid   FOR XML PATH('')  ), ' ', '-')   FROM SystemTime t1  WHERE t1.plant_code='156' and t1.OfficeName='OFFICE'  and  t1.Session='PM'  and Date='11-17-2015'  GROUP BY Plant_code,OfficeName ) as aa  left join ( SELECT t1.Plant_code as wPlant_code ,WeigherSysName, WETime =REPLACE( (SELECT  ':' + RIGHT(Weighersystemtime,7)    FROM SystemTime t2   WHERE t2.plant_code = t1.plant_code  and t2.plant_code='156' and t2.WeigherSysName='WEIGHER'  and Date='11-17-2015' and  t2.Session='PM'   ORDER BY Plant_code,Tid   FOR XML PATH('') ), ' ', '-') FROM SystemTime t1  WHERE t1.plant_code='156' and t1.WeigherSysName='WEIGHER'  and  t1.Session='PM'  and Date='11-17-2015'   GROUP BY Plant_code,WeigherSysName ) as bb on aa.Plant_code=bb.wPlant_code)  as cc left join    (SELECT t1.Plant_code,LabSysName, LABTime =REPLACE( (SELECT  ':' + RIGHT(Labsystemtime,7)    FROM SystemTime t2        WHERE t2.plant_code = t1.plant_code  and t2.plant_code='156' and t2.LabSysName='LAB'  and Date='11-17-2015' and  t2.Session='PM'           ORDER BY Plant_code,Tid             FOR XML PATH('')  ), ' ', '-')     FROM SystemTime t1  WHERE t1.plant_code='156' and t1.LabSysName='LAB'  and  t1.Session='PM'  and Date='11-17-2015'    GROUP BY Plant_code,LabSysName) as dd on cc.Plant_code=dd.Plant_code) AS PPT ON PP.Plant_Code=PPT.Plant_code";


     //  string str = "  SELECT  PP.Plant_code AS PlantCode,PP.Plant_Name as PlantName,PPT.OfficeName as OfficeSys,PPT.OFTime as OffTime,PPT.WeigherSysName as WeigherSys ,PPT.WETime as weiTime,PPT.LabSysName as LabSys,PPT.LABTime as LabTime  FROM (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169 and Plant_Code!=160 ) AS PP LEFT JOIN (select  dd.Plant_code,cc.OfficeName,cc.OFTime,CC.WeigherSysName,CC.WETime,dd.LabSysName,dd.LABTime from(SELECT * from (SELECT t1.Plant_code,OfficeName, OFTime =REPLACE( (SELECT  '' + RIGHT(Officesystemtime,7)   FROM SystemTime t2  WHERE t2.plant_code = t1.plant_code  and t2.OfficeName='OFFICE'  and Date='" + datt + "' and  t2.Session='" + sess + "'   ORDER BY Plant_code,Tid   FOR XML PATH('')  ), ' ', ' ')   FROM SystemTime t1  WHERE  t1.OfficeName='OFFICE'  and  t1.Session='" + sess + "'  and Date='" + datt + "'  GROUP BY Plant_code,OfficeName ) as aa   LEFT  JOIN ( SELECT t1.Plant_code as wPlant_code ,WeigherSysName, WETime =REPLACE( (SELECT  '' + RIGHT(Weighersystemtime,7)    FROM SystemTime t2   WHERE t2.plant_code = t1.plant_code   and t2.WeigherSysName='WEIGHER'  and Date='" + datt + "' and  t2.Session='" + sess + "'   ORDER BY Plant_code,Tid   FOR XML PATH('') ), ' ', ' ') FROM SystemTime t1  WHERE  t1.WeigherSysName='WEIGHER'  and  t1.Session='" + sess + "'  and Date='" + datt + "'   GROUP BY Plant_code,WeigherSysName ) as bb on aa.Plant_code=bb.wPlant_code)  as cc LEFT  JOIN    (SELECT t1.Plant_code,LabSysName, LABTime =REPLACE( (SELECT  '' + RIGHT(Labsystemtime,7)    FROM SystemTime t2        WHERE t2.plant_code = t1.plant_code   and t2.LabSysName='LAB'  and Date='" + datt + "' and  t2.Session='" + sess + "'           ORDER BY Plant_code,Tid             FOR XML PATH('')  ), ' ', ' ')     FROM SystemTime t1  WHERE    t1.LabSysName='LAB'  and  t1.Session='" + sess + "'  and Date='" + datt + "'    GROUP BY Plant_code,LabSysName) as dd on cc.Plant_code=dd.Plant_code) AS PPT ON PP.Plant_Code=PPT.Plant_code  order by PP.Milktype,PP.Plant_Code ";

      //  string str = "  SELECT  PP.Plant_code AS PlantCode,PP.Plant_Name as PlantName,PPT.OfficeName as OfficeSys,PPT.OFTime as OffTime,PPT.WeigherSysName as WeigherSys ,PPT.WETime as weiTime,PPT.LabSysName as LabSys,PPT.LABTime as LabTime  FROM (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 169 and Plant_Code!=160 ) AS PP LEFT JOIN (select  dd.Plant_code,cc.OfficeName,cc.OFTime,CC.WeigherSysName,CC.WETime,dd.LabSysName,dd.LABTime from(SELECT * from (SELECT t1.Plant_code,OfficeName, OFTime =REPLACE( (SELECT  '' + RIGHT(Officesystemtime,7)   FROM SystemTime t2  WHERE t2.plant_code = t1.plant_code  and t2.OfficeName='OFFICE'  and Date='" + datt + "' and  t2.Session='" + sess + "'   group by  Plant_code,OfficeName,Officesystemtime   FOR XML PATH('')  ), ' ', ' ')   FROM SystemTime t1  WHERE  t1.OfficeName='OFFICE'  and  t1.Session='" + sess + "'  and Date='" + datt + "'  GROUP BY Plant_code,OfficeName ) as aa   LEFT  JOIN ( SELECT t1.Plant_code as wPlant_code ,WeigherSysName, WETime =REPLACE( (SELECT  '' + RIGHT(Weighersystemtime,7)    FROM SystemTime t2   WHERE t2.plant_code = t1.plant_code   and t2.WeigherSysName='WEIGHER'  and Date='" + datt + "' and  t2.Session='" + sess + "'  group by    Plant_code,WeigherSysName,Weighersystemtime   FOR XML PATH('') ), ' ', ' ') FROM SystemTime t1  WHERE  t1.WeigherSysName='WEIGHER'  and  t1.Session='" + sess + "'  and Date='" + datt + "'   GROUP BY Plant_code,WeigherSysName ) as bb on aa.Plant_code=bb.wPlant_code)  as cc LEFT  JOIN    (SELECT t1.Plant_code,LabSysName, LABTime =REPLACE( (SELECT  '' + RIGHT(Labsystemtime,7)    FROM SystemTime t2        WHERE t2.plant_code = t1.plant_code   and t2.LabSysName='LAB'  and Date='" + datt + "' and  t2.Session='" + sess + "'          group by  Plant_code,LabSysName,Labsystemtime              FOR XML PATH('')  ), ' ', ' ')     FROM SystemTime t1  WHERE    t1.LabSysName='LAB'  and  t1.Session='" + sess + "'  and Date='" + datt + "'    GROUP BY Plant_code,LabSysName) as dd on cc.Plant_code=dd.Plant_code) AS PPT ON PP.Plant_Code=PPT.Plant_code  order by PP.Milktype,PP.Plant_Code ";

        string str = "  SELECT  PP.Plant_code AS PlantCode,PP.Plant_Name as PlantName,PPT.OfficeName as OfficeSys,PPT.OFTime as OffTime,PPT.WeigherSysName as WeigherSys ,PPT.WETime as weiTime,PPT.LabSysName as LabSys,PPT.LABTime as LabTime  FROM (select   Plant_Code,Plant_Name,Milktype  from Plant_Master  where Plant_Code > 154  and Plant_Code < 170 and Plant_Code!=160 ) AS PP LEFT JOIN (select  dd.Plant_code,cc.OfficeName,cc.OFTime,CC.WeigherSysName,CC.WETime,dd.LabSysName,dd.LABTime from(SELECT * from (SELECT t1.Plant_code,OfficeName, OFTime =REPLACE( (SELECT  '' + RIGHT(Officesystemtime,7)   FROM SystemTime t2  WHERE t2.plant_code = t1.plant_code  and t2.OfficeName='OFFICE'  and Date='" + datt + "'    group by  Plant_code,OfficeName,Officesystemtime   FOR XML PATH('')  ), ' ', ' ')   FROM SystemTime t1  WHERE  t1.OfficeName='OFFICE'   and Date='" + datt + "'  GROUP BY Plant_code,OfficeName ) as aa   LEFT  JOIN ( SELECT t1.Plant_code as wPlant_code ,WeigherSysName, WETime =REPLACE( (SELECT  '' + RIGHT(Weighersystemtime,7)    FROM SystemTime t2   WHERE t2.plant_code = t1.plant_code   and t2.WeigherSysName='WEIGHER'  and Date='" + datt + "'   group by    Plant_code,WeigherSysName,Weighersystemtime   FOR XML PATH('') ), ' ', ' ') FROM SystemTime t1  WHERE  t1.WeigherSysName='WEIGHER'   and Date='" + datt + "'   GROUP BY Plant_code,WeigherSysName ) as bb on aa.Plant_code=bb.wPlant_code)  as cc LEFT  JOIN    (SELECT t1.Plant_code,LabSysName, LABTime =REPLACE( (SELECT  '' + RIGHT(Labsystemtime,7)    FROM SystemTime t2        WHERE t2.plant_code = t1.plant_code   and t2.LabSysName='LAB'  and Date='" + datt + "'           group by  Plant_code,LabSysName,Labsystemtime              FOR XML PATH('')  ), ' ', ' ')     FROM SystemTime t1  WHERE    t1.LabSysName='LAB'      and Date='" + datt + "'    GROUP BY Plant_code,LabSysName) as dd on cc.Plant_code=dd.Plant_code) AS PPT ON PP.Plant_Code=PPT.Plant_code  order by PP.Milktype,PP.Plant_Code,PPT.OFTime ";
 
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
            GridView2.Visible = false;
            GridView1.HeaderStyle.BackColor = Color.Brown;
            GridView1.HeaderStyle.ForeColor = Color.White;

        }
        else
        {

        }
      
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowIndex == GridView1.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";
            }
        }


       
        //Accessing TemplateField Column controls
      //  string plantcode = (GridView1.SelectedRow.FindControl("plantcode") as Label).Text;


       
         
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the value in the c04_oprogrs column. You'll have to use

                string value = e.Row.Cells[1].Text;

                string value1 = e.Row.Cells[4].Text;

                string value2 = e.Row.Cells[6].Text;

                string value3 = e.Row.Cells[8].Text;

                if(value1=="&nbsp;")
                {

                    e.Row.Cells[3].Text = "NotConnect";
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    


                }

                else
                {
                     e.Row.Cells[3].Text="Connect";
                     e.Row.Cells[3].ForeColor = System.Drawing.Color.Green;


                }

                if (value2 =="&nbsp;")
                {

                    e.Row.Cells[5].Text = "NotConnect";

                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                }

                else
                {
                    e.Row.Cells[5].Text = "Connect";
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;

                }

                if (value3 =="&nbsp;")
                {

                    e.Row.Cells[7].Text = "NotConnect";
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;


                }

                else
                {
                    e.Row.Cells[7].Text = "Connect";
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Green;


                }

                //if ((value == "155") || (value == "156") || (value == "158") || (value == "159") || (value == "161") || (value == "162") || (value == "163") || (value == "164"))
                //{




                //    e.Row.Cells[1].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[1].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[1].Font.Bold = true;

                //    e.Row.Cells[0].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[0].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[0].Font.Bold = true;

                //    e.Row.Cells[2].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[2].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[2].Font.Bold = true;


                //    e.Row.Cells[3].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[3].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[3].Font.Bold = true;


                //    e.Row.Cells[4].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[4].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[4].Font.Bold = true;


                //    e.Row.Cells[5].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[5].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[5].Font.Bold = true;


                //    e.Row.Cells[6].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[6].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[6].Font.Bold = true;


                //    e.Row.Cells[7].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[7].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[7].Font.Bold = true;


                //    e.Row.Cells[8].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[8].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[8].Font.Bold = true;


                //    e.Row.Cells[9].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[9].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[9].Font.Bold = true;


                //    e.Row.Cells[10].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[10].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[10].Font.Bold = true;



                //    e.Row.Cells[11].BackColor = System.Drawing.Color.Bisque;
                //    e.Row.Cells[11].ForeColor = System.Drawing.Color.DarkBlue;
                //    e.Row.Cells[11].Font.Bold = true;



                //    //e.Row.Cells[12].BackColor = System.Drawing.Color.DarkBlue;
                //    //e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                //    //e.Row.Cells[12].Font.Bold = true;



                //    //e.Row.Cells[13].BackColor = System.Drawing.Color.DarkBlue;
                //    //e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
                //    //e.Row.Cells[13].Font.Bold = true;

                //}
                //else
                //{
                //    e.Row.Cells[0].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[0].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[0].Font.Bold = true;

                //    e.Row.Cells[1].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[1].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[1].Font.Bold = true;

                //    e.Row.Cells[2].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[2].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[2].Font.Bold = true;

                //    e.Row.Cells[3].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[3].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[3].Font.Bold = true;


                //    e.Row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[4].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[4].Font.Bold = true;


                //    e.Row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[5].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[5].Font.Bold = true;


                //    e.Row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[6].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[6].Font.Bold = true;


                //    e.Row.Cells[7].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[7].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[7].Font.Bold = true;


                //    e.Row.Cells[8].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[8].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[8].Font.Bold = true;

                //    e.Row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                //    e.Row.Cells[9].ForeColor = System.Drawing.Color.Brown;
                //    e.Row.Cells[9].Font.Bold = true;


                //    //e.Row.Cells[9].BackColor = System.Drawing.Color.LightGreen;
                //    //e.Row.Cells[9].ForeColor = System.Drawing.Color.Brown;
                //    //e.Row.Cells[9].Font.Bold = true;


                //    //e.Row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                //    //e.Row.Cells[10].ForeColor = System.Drawing.Color.Brown;
                //    //e.Row.Cells[10].Font.Bold = true;

                //    //e.Row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                //    //e.Row.Cells[11].ForeColor = System.Drawing.Color.Brown;
                //    //e.Row.Cells[11].Font.Bold = true;

                //    //e.Row.Cells[12].BackColor = System.Drawing.Color.Green;
                //    //e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                //    //e.Row.Cells[12].Font.Bold = true;

                //    e.Row.Cells[13].BackColor = System.Drawing.Color.Green;
                //    e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
                //    e.Row.Cells[13].Font.Bold = true;
                    

                //}

                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Attributes["onclick"] = "openWindow('" + e.Row.Cells[0].Text + "')";
                //    //     e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView1, "Select$" + e.Row.RowIndex);
                //    e.Row.Attributes["style"] = "cursor:pointer";


                //    //e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                //    //e.Row.Attributes.Add("onMouseOut", "this.style.background='#ffffff'");
                //}

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                    e.Row.ToolTip = "Click to select this row.";
                }


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] =
                        "javascript:setMouseOverColor(this);";
                    e.Row.Attributes["onmouseout"] =
                        "javascript:setMouseOutColor(this);";
                    e.Row.Attributes["onclick"] =
                    ClientScript.GetPostBackClientHyperlink
                        (this.GridView1, "Select$" + e.Row.RowIndex);
                }



               

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            name = grdrow.Cells[1].Text;
            getgd1();
            getgd2();
            getgd3();
            Session.Visible = false;

            
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
       

        if (e.Row.RowType == DataControlRowType.Header)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell Cell_Header = new TableCell();
                Cell_Header.Text = "PLANT INFORMATION";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.BackColor = System.Drawing.Color.Bisque;
                Cell_Header.ForeColor = System.Drawing.Color.Black;
                Cell_Header.ColumnSpan = 3;
                Cell_Header.Font.Bold = true;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "SERVER SYSTEM";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                Cell_Header.Font.Bold = true;
                Cell_Header.BackColor = System.Drawing.Color.Bisque;
                Cell_Header.ForeColor = System.Drawing.Color.Black;
            //    Cell_Header.RowSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);

                Cell_Header = new TableCell();
                Cell_Header.Text = "WEIGHER SYSTEM";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                Cell_Header.Font.Bold = true;
                Cell_Header.BackColor = System.Drawing.Color.Bisque;
                Cell_Header.ForeColor = System.Drawing.Color.Black;
                HeaderRow.Cells.Add(Cell_Header);


                Cell_Header = new TableCell();
                Cell_Header.Text = "LAB SYSTEM";
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                Cell_Header.ColumnSpan = 2;
                Cell_Header.BackColor = System.Drawing.Color.Bisque;
                Cell_Header.ForeColor = System.Drawing.Color.Black;
                Cell_Header.Font.Bold = true;
                HeaderRow.Cells.Add(Cell_Header);
                GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

                //Cell_Header = new TableCell();
                //Cell_Header.Text = "lOCAL NETWORK SYSTEM";
                //Cell_Header.HorizontalAlign = HorizontalAlign.Center;
                //Cell_Header.ColumnSpan = 1;
                //Cell_Header.BackColor = System.Drawing.Color.Bisque;
                //Cell_Header.ForeColor = System.Drawing.Color.Black;
                //Cell_Header.Font.Bold = true;
                //HeaderRow.Cells.Add(Cell_Header);
                //GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            }


        }
    }
    protected void Timer2_Tick(object sender, EventArgs e)
    {
        GetData();
        Literal17.Text = datte.ToString("dd/MM/yyyy");
        Literal19.Text = sess.ToString();
       
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmdBind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int index = Convert.ToInt32(lb.CommandArgument);
            string pkey;
            //Bind values in the text box of the pop up control
            pkey = GridView1.Rows[index].Cells[2].Text;
            Response.Redirect("LocalSystemDetailsReports1.aspx?eno=" + pkey);
        }


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onclick"] = "window.navigate('LocalSystemDetailsReports1.aspx?id=" + e.Row.Cells[2].Text.ToString().Trim() + "')";
        //}
    }

    public void getgd1()
    {

        DateTime datte = System.DateTime.Now;
        datt = datte.ToString("MM/dd/yyyy");
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd = new SqlCommand("Select distinct(OfficesystemIp) AS OFFICESYSTEMIP,RIGHT(Officesystemtime,7) AS TIME  from  SystemTime   where  Plant_code='" + name + "' and  Date='" + datt + "'  and OfficeName='OFFICE' and OfficesystemIp!='NULL' group by OfficesystemIp,Officesystemtime  ", con);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView2.DataSource = dt;
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView2.DataBind();
                BACK.Visible = true;;
                GridView2.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                GridView2.HeaderStyle.ForeColor = System.Drawing.Color.White;
                Literal19.Visible = false;
            }
            else
            {

                GridView2.DataSource = null;
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView2.DataBind();
                BACK.Visible = false;
                Literal19.Visible = false;
            }
        }


    }
    public void getgd2()
    {

        DateTime datte = System.DateTime.Now;
        datt = datte.ToString("MM/dd/yyyy");
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd = new SqlCommand("Select distinct(WeighersystemIp) AS WEIGHINGSYSTEMIP,RIGHT(Weighersystemtime,7) as TIME  from  SystemTime   where  Plant_code='" + name + "' and  Date='" + datt + "'  and WeigherSysName='WEIGHER' and WeighersystemIp!='NULL' GROUP BY  WeighersystemIp,Weighersystemtime  ", con);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView1.Visible = false;
                GridView3.Visible = true;
                GridView2.Visible = true;
                GridView3.DataBind();
                BACK.Visible = true;
                //GridView3.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                //GridView3.HeaderStyle.ForeColor = System.Drawing.Color.White;
                Literal19.Visible = false;
                Literal19.Visible = false;
            }
            else
            {
                GridView3.DataSource = null;
                GridView1.Visible = false;
                GridView3.Visible = true;
                GridView2.Visible = true;
                GridView3.DataBind();
                BACK.Visible = true;
                Literal19.Visible = false;
            }
        }

    }

    public void getgd3()
    {

        DateTime datte = System.DateTime.Now;
        datt = datte.ToString("MM/dd/yyyy");
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd = new SqlCommand("Select distinct(LabsystemIp) AS LABSYSTEMIP,RIGHT(Labsystemtime,7) as TIME  from  SystemTime   where  Plant_code='" + name + "' and  Date='" + datt + "'  and LabSysName='LAB' and LabsystemIp!='NULL'   GROUP BY  LabsystemIp,Labsystemtime ", con);
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView4.DataSource = dt;
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView3.Visible = true;
                GridView4.Visible = true;
                GridView4.DataBind();
                BACK.Visible = true;
                GridView4.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                GridView4.HeaderStyle.ForeColor = System.Drawing.Color.White;
                Literal19.Visible = false;
            }
            else
            {
                GridView4.DataSource = null;
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView3.Visible = true;
                GridView4.Visible = true;
                GridView4.DataBind();
                BACK.Visible = true;
                Literal19.Visible = false;
               

            }
        }

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       
    }
    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        try
        {
            string initialnamevalue = GridView2.Rows[0].Cells[0].Text;

            //Step 2:

            for (int i = 1; i < GridView2.Rows.Count; i++)
            {

                if (GridView2.Rows[i].Cells[0].Text == initialnamevalue)
                    GridView2.Rows[i].Cells[0].Text = string.Empty;
                else
                    initialnamevalue = GridView2.Rows[i].Cells[0].Text;
            }
        }
        catch
        {


        }
    }
    protected void GridView3_DataBound(object sender, EventArgs e)
    {
        try
        {
            string initialnamevalue = GridView3.Rows[0].Cells[0].Text;

            //Step 2:

            for (int i = 1; i < GridView3.Rows.Count; i++)
            {

                if (GridView3.Rows[i].Cells[0].Text == initialnamevalue)
                    GridView3.Rows[i].Cells[0].Text = string.Empty;
                else
                    initialnamevalue = GridView3.Rows[i].Cells[0].Text;
            }
        }
        catch
        {

        }
    }
    protected void GridView4_DataBound(object sender, EventArgs e)
    {
        try
        {
            string initialnamevalue = GridView4.Rows[0].Cells[0].Text;

            //Step 2:

            for (int i = 1; i < GridView4.Rows.Count; i++)
            {

                if (GridView4.Rows[i].Cells[0].Text == initialnamevalue)
                    GridView4.Rows[i].Cells[0].Text = string.Empty;
                else
                    initialnamevalue = GridView4.Rows[i].Cells[0].Text;
            }
        }
        catch
        {

        }
    }
    protected void lnkDummy_Click(object sender, EventArgs e)
    {


    }
    protected void BACK_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        
    }
}