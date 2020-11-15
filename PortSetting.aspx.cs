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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
public partial class PortSetting : System.Web.UI.Page
{
    private string SearchString = "";
    string centreid;
    SqlConnection con;
    ///SqlDataReader dr;
    DbHelper DBclass = new DbHelper();
    int countuser;
    string uid;
    BOLPortsetting portBO = new BOLPortsetting();
    BLLPortsetting portBL = new BLLPortsetting();
    DALPortsetting portDA = new DALPortsetting();
    DataTable dt = new DataTable();
    DataRow dr;
    SqlDataReader dar;
    int DeleteFlag;
    public string companycode;
    public string plantcode;

    protected void Page_Load(object sender, EventArgs e)
    {
        uscMsgBox1.MsgBoxAnswered += MessageAnswered;
        if (!IsPostBack == true)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();

               // uid = Session["User_ID"].ToString();
               //plantcode=uid
                uid = plantcode;
                centreid = uid;
                //GetID();
               // GetCurrentUserCount();
                loadgriddata();

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
        else
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                companycode = Session["Company_code"].ToString();
                plantcode = Session["Plant_Code"].ToString();               
                uid = plantcode;
                centreid = uid;               
            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int result = UserStatus();
        if (result == 1)
        {

        }
        else if (result == 0)
        {

         
            //uscMsgBox1.AddMessage("Do you confirm to save a new user?.", MessageBoxUsc_Message.enmMessageType.Attention, true, true, txtportname.Text);
            
            loadgriddata();
        ////OLD
        
        //if (result == 1)
        //{

        //}
        //else if (result == 0)
        //{

        //    Session["User_LoginId"] = centreid;
        //    if ((txtportname.Text != string.Empty && txtbaudrate.Text != string.Empty))
        //    {
        //        setBO();

        //        portBL.insertport(portBO);
        //        loadgriddata();

        //        txtportname.Text = "";
        //        txtbaudrate.Text = "";
        //    }
        //    uscMsgBox1.AddMessage("Do you confirm to save a new user?.", MessageBoxUsc_Message.enmMessageType.Attention, true, true, txtportname.Text);

        //    loadgriddata();


            uscMsgBox1.AddMessage("Saved Sucessfully.", MessageBoxUsc_Message.enmMessageType.Success);

        }
    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txtportname.Text + "and " + txtbaudrate.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
           
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
            
        }
    }
    public void GetCurrentUserCount()
    {

        try
        {
            using (con = DBclass.GetConnection())
            {

                object cc;
                string sqlstr = "select Count(User_LoginId) from UserIDInfo where User_LoginId='" + Session["User_ID"].ToString() + "' and Roles='User'";
                SqlCommand cmd = new SqlCommand(sqlstr, con);
                cc = cmd.ExecuteScalar();
                countuser = (Int32)cc;
            }
        }
        catch { }

    }
    public void GetID()
    {

        try
        {
            using (con = DBclass.GetConnection())
            {


                string sqlstr = "select User_LoginId from UserIDInfo where User_LoginId='" + Session["User_ID"].ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);


                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    centreid = dt.Rows[j]["User_LoginId"].ToString().Trim();


                }
            }
        }
        catch { }

    }
    public int UserStatus()
    {
       
        if (txtportname.Text == string.Empty && txtbaudrate.Text==string.Empty)
        {
            uscMsgBox1.AddMessage("Please Enter Required Fields", MessageBoxUsc_Message.enmMessageType.Attention);
            
            return 1;
        }
        else
        {
            SqlDataReader dr;
            string str = "select * from Port_Setting where Centre_ID='" + uid + "' and M_Type='" + Typedrp.Text + "'";

            dr = DBclass.GetDatareader(str);
            if (dr.Read())
            {
                uscMsgBox1.AddMessage("Type is AlReady Exists", MessageBoxUsc_Message.enmMessageType.Attention);
               
                return 1;
            }
            else
            {

                if ((txtportname.Text != string.Empty))
                {
                    setBO();

                    portBL.insertport(portBO);
                    loadgriddata();
                    
                    txtportname.Text = "";
                    txtbaudrate.Text = "";
                }
                return 0;
            }
        }

    }
    private void loadgriddata()
    {
        //uid = Session["User_ID"].ToString();
        int dtcount;
        dt = portBL.getportnames(companycode,plantcode);
        dtcount = dt.Rows.Count;
        if (dtcount > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            uscMsgBox1.AddMessage("No ports...", MessageBoxUsc_Message.enmMessageType.Attention);
            
        }
    }
    private void setBO()
    {
        portBO.TableID = 0;
        portBO.PortName = "COM" + txtportname.Text;
        portBO.CentreID = plantcode;
        portBO.BaudRate = txtbaudrate.Text;
        portBO.MType = Typedrp.Text;
        portBO.Companyid = companycode;

    }
    protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {
        int index = GridView1.EditIndex;
        GridViewRow row = GridView1.Rows[index];
        loadgriddata();
        int count;
        count = dt.Rows.Count;
        dr = dt.Rows[index];

        portBO.TableID= Convert.ToInt32(dr["Table_ID"]);
        portBO.PortName = ((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim();
        portBO.BaudRate = ((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim();
        portBO.CentreID = plantcode;
        portBO.MType=dr["M_Type"].ToString();
        portBO.Companyid = companycode;
        portBL.EditUpdatePort(portBO);
        //rateBLL.EditUpdateRatechart(rateBOL);
        GridView1.EditIndex = -1;
        loadgriddata();


    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;

        loadgriddata();
    }
    protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        //string message = "Do you want to Delete the record?";

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //sb.Append("return confirm('");

        //sb.Append(message);

        //sb.Append("');");

        //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
        //loadgriddata();
        //GridView1.EditIndex = e.RowIndex;
        //int index = GridView1.EditIndex;
        //GridViewRow row = GridView1.Rows[index];
        //dr = dt.Rows[index];
        //DeleteFlag = 1;
        //portBO.TableID= Convert.ToInt32(dr["Table_ID"]);
        ////rateBOL.Chartname = dl_RatechartName.SelectedItem.Text;
        ////rateBOL.Flag = DeleteFlag;

        ////rateBOL.Plantcode = plant_Code;
        ////rateBOL.Companycode = Company_code;
        //rateBLL.DlelteRow(rateBOL);
        //rateBOL.Flag = 0;
        //loadratechart();
        //loadgrid(dl_RatechartName.Text);
    }
    protected void GridView1_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        loadgriddata();
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    
}
