using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InfoSoftGlobal;
using System.Text;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;

public partial class Test2 : System.Web.UI.Page
{
    public static int roleid;
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    public string agentName;
    public string agentid;
    DateTime dtm = new DateTime();
    SqlConnection con = new SqlConnection();
    DbHelper DB = new DbHelper();
    DataSet DTG = new DataSet();
    string GETNAME;
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
                    //txt_FromDate.Text = dtm.ToString("dd/MM/yyyy");
                    roleid = Convert.ToInt32(Session["Role"].ToString());
                    dtm = System.DateTime.Now;
                     GETNAME = Session["Name"].ToString();

                     populateMenuItem(); 


                    //if (roleid < 3)
                    //{
                    //    loadsingleplant();

                    //}

                    //else
                    //{

                    //    LoadPlantcode();
                    //}


                }
                else
                {

                    //pname = ddl_Plantname.SelectedItem.Text;


                }

            }


            else
            {
                ccode = Session["Company_code"].ToString();
                //  LoadPlantcode();

                //pcode = ddl_Plantname.SelectedItem.Value;
            }
        }
        catch
        {


        }
    }


    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        try
        {

            con = DB.GetConnection();

            string STT = "";
            STT = "ALTER TABLE NewMenu ADD " + Session["Name"] + "  nvarchar(max) ";
            SqlCommand CMD = new SqlCommand(STT, con);
            CMD.ExecuteNonQuery();
        }

        catch
        {
            string EX = "Name Already Added";
            Response.Write("<script language='javascript'>alert('" + Server.HtmlEncode(EX) + "')</script>");


        }


   




    }

    private void populateMenuItem()
    {

        DataTable menuData = GetMenuData();
       
        AddTopMenuItems(menuData);

    }

    private DataTable GetMenuData()
    {
        using (SqlConnection con = new SqlConnection("Data Source=223.196.32.28;Initial Catalog=AMPS;User ID=sa;Password=Vyshnavi123"))
        {
           
           string GETTT = "SELECT menu_id,menu_name,Menu_parentid,Menu_url," + Session["Name"] + " FROM NewMenu WHERE " + Session["Name"] + " = '1' ";
           SqlCommand cmd = new SqlCommand(GETTT,con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
              
                da.Fill(dt);
                return dt;
            }

        
    }

    /// Filter the data to get only the rows that have a
    /// null ParentID (This will come on the top-level menu items)

    private void AddTopMenuItems(DataTable menuData)
    {
        //DataView view = new DataView(menuData);
        //view.RowFilter = "menu_parent_id IS NULL";
        //foreach (DataRowView row in view)
        //{
        //    MenuItem newMenuItem = new MenuItem(row["menu_name"].ToString(), row["menu_parent_id"].ToString());
        //    Menu1.Items.Add(newMenuItem);
        //    AddChildMenuItems(menuData, newMenuItem);
        //}


        DataTable table = new DataTable();
       
        //string strCon = System.Configuration.ConfigurationManager.ConnectionStrings
        //["cnstring"].ConnectionString;
        //  SqlConnection conn = new SqlConnection(strCon);
        con = DB.GetConnection();
        string sql = "select menu_id, menu_name, menu_parentid, menu_url," + Session["Name"] + " from NewMenu WHERE " + Session["Name"] + "='1'  ";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        
        DataView view = new DataView(table);
        
        view.RowFilter = "menu_parentid is NULL";
        foreach (DataRowView row in view)
        {
            MenuItem menuItem = new MenuItem(row["menu_name"].ToString(), row["menu_id"].ToString());
            menuItem.NavigateUrl = row["menu_url"].ToString();
            
            string GETNAME = Session["Name"].ToString();

            int ID = Convert.ToInt32(row[GETNAME]);
            try
            {
                if (ID != 0)
                {
                    Menu1.Items.Add(menuItem);
                    AddChildMenuItems(table, menuItem);
                }
            }
            catch
            {

            }
        }




    }

    //This code is used to recursively add child menu items by filtering by ParentID

    private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem)
    {
        //DataView view = new DataView(menuData);
        //view.RowFilter = "menu_parent_id =" + parentMenuItem.Value;
        //foreach (DataRowView row in view)
        //{
        //    MenuItem newMenuItem = new MenuItem(row["menu_name"].ToString(), row["menu_id"].ToString());
        //    parentMenuItem.ChildItems.Add(newMenuItem);
        //    AddChildMenuItems(menuData, newMenuItem);
        //}




        DataView viewItem = new DataView(menuData);
        viewItem.RowFilter = "menu_parentid=" + parentMenuItem.Value;
        foreach (DataRowView childView in viewItem)
        {
            MenuItem childItem = new MenuItem(childView["menu_name"].ToString(),
            childView["menu_id"].ToString());
            childItem.NavigateUrl = childView["menu_url"].ToString();
            GETNAME  = Session["Name"].ToString();
            //if (childItem.NavigateUrl == '#'.ToString())
            //{
            int ID = Convert.ToInt32(childView[GETNAME].ToString());
            if (ID != 0)
            {
                parentMenuItem.ChildItems.Add(childItem);
                AddChildMenuItems(menuData, childItem);
            }
        }

    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        try
        {
            string name = Menu1.SelectedItem.Text;
            string pageurl = name + ".aspx";
            Response.Redirect(pageurl);
        }
        catch (Exception ex)
        {
        }
    }


    //private void PopulateMenuDataTable()
    //{
    //    DataTable dt = new DataTable();
    //    using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Experiment\ASPTreeViewBinding\ASPTreeViewBinding\App_Data\MyDatabase.mdf;Integrated Security=True;User Instance=True"))
    //    {

    //        SqlCommand cmd = new SqlCommand("Select * from MenuMaster", con);
    //        if (con.State != ConnectionState.Open)
    //        {
    //            con.Open();
    //            dt.Load(cmd.ExecuteReader());
    //        }
    //    }
    //    List<MenuMaster> allMenu = new List<MenuMaster>();
    //    //using (MyDatabaseEntities dc = new MyDatabaseEntities())
    //    //{
    //    //    allMenu = dc.MenuMasters.ToList();
    //    //}
    //    CreateTreeViewDataTable(dt, 0, null);
    //}

    //private void CreateTreeViewDataTable(DataTable dt, int parentID, TreeNode parentNode)
    //{
    //    DataRow[] drs = dt.Select("ParentID = " + parentID.ToString());
    //    foreach (DataRow i in drs)
    //    {
    //        TreeNode newNode = new TreeNode(i["MenuName"].ToString(), i["MenuID"].ToString());
    //        if (parentNode == null)
    //        {
    //            tvMenu.Nodes.Add(newNode);
    //        }
    //        else
    //        {
    //            parentNode.ChildNodes.Add(newNode);
    //        }
    //        CreateTreeViewDataTable(dt, Convert.ToInt32(i["MenuID"]), newNode);
    //    }
    //}


   
}