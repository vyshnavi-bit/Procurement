using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.IO;
public partial class PlantRequestingForm : System.Web.UI.Page
{

    public static int roleid;
    public string ccode;
    public static string routeid;
    public string routemilksum;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTGGG = new DataSet();
    BLLuser Bllusers = new BLLuser();
    DataTable DTGchart = new DataTable();
    msg MESS = new msg();
    int h = 0;
    int j = 1;
    int jG = 1;
    int jhp = 0;
    double GETSUM = 0;
    DataSet DTG = new DataSet();
    string d1;
    string d2;
    string MSG;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((Session["Name"] != null) && (Session["pass"] != null))
                {
                    dtm = System.DateTime.Now;
                    ccode = Session["Company_code"].ToString();
                    pcode = Session["Plant_Code"].ToString();
                    txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");



                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;

                    txt_Routeid2.Text = dtm.ToString();
                //    LoadPlantcode();
                    txt_Routeid3.Text = Session["Name"].ToString();



                    if (roleid < 3)
                    {
                       loadsingleplant();

                    }

                    if ((roleid >= 3) && (roleid != 9))
                    {

                        LoadPlantcode();
                    }

                    if (roleid == 9)
                    {
                        Session["Plant_Code"] = "170".ToString();
                        pcode = "170";
                        loadspecialsingleplant();

                    }


                }
                else
                {


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();
                pcode = ddl_Plantname.SelectedItem.Value;


                

            }
        }
        catch
        {


        }
    }



    public void LoadPlantcode()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (139)";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }


    public void  loadsingleplant()
    {

        con = DB.GetConnection();
        string stt = "SELECT Plant_Code, Plant_Name FROM Plant_Master where plant_code not in (139) and plant_code  in ("+pcode+") ";
        SqlCommand cmd = new SqlCommand(stt, con);
        SqlDataAdapter DA = new SqlDataAdapter(cmd);
        DA.Fill(DTG);
        ddl_Plantname.DataSource = DTG.Tables[0];
        ddl_Plantname.DataTextField = "Plant_Name";
        ddl_Plantname.DataValueField = "Plant_Code";
        ddl_Plantname.DataBind();

    }

    private void loadspecialsingleplant()//form load method
    {
        try
        {
            SqlDataReader dr = null;
        
            ddl_Plantname.Items.Clear();
            dr = Bllusers.LoadSinglePlantcode(ccode, "170");
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
    protected void Button6_Click(object sender, EventArgs e)
    {


    }


    public void clear()
    {

       

    }
    public void getgrid()
    {

        try
        {
            //DateTime dt2 = new DateTime();
            //dt2 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
            ////dt2 = DateTime.ParseExact(txt_ToDate.Text, "dd/MM/yyyy", null);
            //string d1 = dt2.ToString("MM/dd/yyyy");

            con = DB.GetConnection();

            string stt = "select Remarks,Date   FROM  PlantRegularRequest where  plant_code='"+pcode+"'";
            SqlCommand cmd = new SqlCommand(stt, con);
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataTable tf = new DataTable();
            DA.Fill(tf);
            if (tf.Rows.Count > 0)
            {
                GridView1.DataSource = tf;
                GridView1.DataBind();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();

            }

        }
        catch
        {


        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
         try
        {

            if ((txt_qualifications.Text != string.Empty) && (txtcondent.Text != string.Empty))
            {
                DateTime dt1 = new DateTime();
                dt1 = DateTime.ParseExact(txt_FromDate.Text, "dd/MM/yyyy", null);
                d1 = dt1.ToString("MM/dd/yyyy");
                string get = "";
                get = "Insert into PlantRegularRequest(Plant_code,Remarks,Date,Login,Requester,RequesterDate,Status,Dept)values('" + pcode + "','" + txtcondent.Text + "','" + System.DateTime.Now + "','" + Session["Name"].ToString() + "','" + txt_qualifications.Text + "','" + d1 + "','0','" + ddl_depart.SelectedItem.Text+ "')  ";
                con = DB.GetConnection();
                SqlCommand cmd = new SqlCommand(get, con);
                cmd.ExecuteNonQuery();
                txt_qualifications.Text = "";
                txtcondent.Text = "";
           
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.save) + "')</script>");
                getgrid();
            }
                
            else
            {
                Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(MESS.Check) + "')</script>");

            }
 
        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(ex.Message) + "')</script>");

        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddl_Plantname_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}