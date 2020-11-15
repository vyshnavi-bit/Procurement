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

public partial class StateMaster : System.Web.UI.Page
{
    
    DbHelper DBclass = new DbHelper();
    BLLStateMaster statemasterBL = new BLLStateMaster();
    BOLStateMaster statemasterBO = new BOLStateMaster();
    DALStateMaster statemasterDA = new DALStateMaster();
    int pcode, cmpcode;
    string uid;
    string centreid;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            uscMsgBox1.MsgBoxAnswered += MessageAnswered;
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                //uid = Session["User_ID"].ToString();
                //if (Request.QueryString["id"] != null)

                //    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                //else
                //    Response.Write("");

                //Session["Route_ID"] = cmb_RouteID.Text;
               // pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                //Session["Aid"] = txtAgentID.Text;
                //Aid = Convert.ToInt32(Session["Aid"]);
                //Session["Aid"] = Aid;
                //Session["Route_Name"] = cmb_RouteName.Text;
                txt_statename.Focus();

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

                //uid = Session["User_ID"].ToString();
                //if (Request.QueryString["id"] != null)

                //    Response.Write("querystring passed in: " + Request.QueryString["id"]);

                //else
                //    Response.Write("");

                //Session["Route_ID"] = cmb_RouteID.Text;
             //   pcode = Convert.ToInt32(Session["Plant_Code"]);
                cmpcode = Convert.ToInt32(Session["Company_code"]);
                //Session["Aid"] = txtAgentID.Text;
                //Aid = Convert.ToInt32(Session["Aid"]);
                //Session["Aid"] = Aid;
                //Session["Route_Name"] = cmb_RouteName.Text;
                txt_statename.Focus();

            }
            else
            {

                Server.Transfer("LoginDefault.aspx");
            }

        }

        loadstateid();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        int result = UserStatus();
        if (result == 1)
        {

        }
        else if (result == 0)
        {

            Session["User_LoginId"] = centreid;

            uscMsgBox1.AddMessage("Saved Sucessfully", MessageBoxUsc_Message.enmMessageType.Success);
        }


    }
    public void loadstateid()
    {
        try
        {
            int mx;
        //    string sqlstr = "select MAX(State_ID) From State_Master WHERE Company_Code=" + cmpcode + " AND Plant_Code=" + pcode + "";
          //  for menu
            string sqlstr = "select MAX(State_ID) From State_Master WHERE Company_Code=" + cmpcode + " ";
            mx = Convert.ToInt32(DBclass.ExecuteScalarstr(sqlstr));
            mx = mx + 1;
            txt_StateId.Text = mx.ToString();
        }
        catch
        {

            txt_StateId.Text = "1";
        }



    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return default(string[]);
    }
    public void cleare()
    {
        //txt_StateId.Text = "";
        txt_statename.Text = "";
    }
    public void loadgrid()
    {
        gvProducts.DataBind();

    }
    public void MessageAnswered(object sender, MessageBoxUsc_Message.MsgBoxEventArgs e)
    {
        if (e.Answer == MessageBoxUsc_Message.enmAnswer.OK)
        {
            uscMsgBox1.AddMessage("You have just confirmed the transaction. The user saved successfully. You have entered " + txt_statename.Text + " as argument.", MessageBoxUsc_Message.enmMessageType.Info);
        }
        else
        {
            uscMsgBox1.AddMessage("You have just cancelled the transaction.", MessageBoxUsc_Message.enmMessageType.Info);
        }
    }

    public int UserStatus()
    {
        if ( txt_statename.Text == string.Empty)
        {
            uscMsgBox1.AddMessage("Please Enter State Name", MessageBoxUsc_Message.enmMessageType.Attention);

            return 1;
        }
        else
        {
            SqlDataReader dr;

       //     string str = "SELECT * from State_Master where State_Name='" + txt_statename.Text + "' AND Company_Code=" + cmpcode + " AND Plant_Code=" + pcode + "";
          //  for menu
            string str = "SELECT * from State_Master where State_Name='" + txt_statename.Text + "' AND Company_Code=" + cmpcode + "";
            dr = DBclass.GetDatareader(str);
            if (dr.Read())
            {
                uscMsgBox1.AddMessage("State Name AlReady Exists", MessageBoxUsc_Message.enmMessageType.Attention);

                return 1;
            }
            else
            {

                if ((txt_statename.Text != string.Empty))
                {
                    setBO();
                    statemasterBL.insertstatemaster(statemasterBO);
                  
                    //gvProducts.DataBind();
                    loadgrid();
                    loadstateid();
                    cleare();

                }
                return 0;
            }
        }
    }
    public void setBO()
    {
        if (txt_statename.Text != string.Empty)

            statemasterBO.stateName = txt_statename.Text;
        if (txt_StateId.Text != string.Empty)
            statemasterBO.stateId = txt_StateId.Text;

       
        //Assign Plantcode and companycode

    //  statemasterBO.Plantcode = pcode;
      statemasterBO.Companycode = cmpcode;
      

        //  bankmasterBO.Tid = deleteid.ToString();
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
