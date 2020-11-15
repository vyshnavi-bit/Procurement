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
using System.Data.SqlClient;

public partial class RolePermissionSetting : System.Web.UI.Page
{
    protected System.Web.UI.WebControls.CheckBox Mchk1;
    protected System.Web.UI.WebControls.CheckBox chkList1;
    protected System.Web.UI.WebControls.CheckBox chkList2;
    MenuBLL MBLL = new MenuBLL();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataRow dr2;
    DataSet ds = new DataSet();
    SqlDataReader dr;
    SqlDataReader dr1;

    public string Company_code;
    public string plant_Code;


    public int Tid;
    public string MenuName;
    public string MenuValue;
    public string DynamicCheckBoxName;

    public string SubMenuName;
    public string SubMenuValue;
    public int SubMenuValue1;
    public int SubMenuValue2;
    public int MenuLoadid;
    public int SelectMenuValue;

    public int idinc = 1;
    public int idinc2 = 1;

    public int M1 = 11;
    public int M2 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack != true)
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                int companycode = Convert.ToInt32(Session["Company_code"]);
                int plantcode = Convert.ToInt32(Session["Plant_Code"]);


                dr = MBLL.GetMenuTitle(companycode);
                dt = MBLL.GetSubMenuTitle(companycode);


                while (dr.Read())
                {

                    Tid = Convert.ToInt32(dr["Tid"]);
                    //MenuName = dr["MenuName"].ToString();
                    //MenuValue = dr["MenuValue"].ToString();

                    //SubMenuValue1 = 11;
                    //MenuLoadid = 1;
                    //if (dt.Rows.Count > 0)
                    //{
                    //    dt.Columns.Add(new System.Data.DataColumn("From Range", typeof(string)));
                    //    dt1.Columns.Add(new System.Data.DataColumn("MASTER", typeof(string)));
                    //    for (int i = 0; i < dt.Rows.Count; i++)
                    //    {
                    //        SubMenuValue2 = Convert.ToInt32(dt.Rows[i]["A1"]);
                    //        MenuLoadid = Convert.ToInt32(dt.Rows[i]["MenuLoadid"]);
                    //        SubMenuName = dt.Rows[i]["SubMenuName"].ToString();
                    //        if (Tid == MenuLoadid)
                    //        {
                    //            if (SubMenuValue1 == SubMenuValue2)
                    //            {
                    //                dr2 = dt1.NewRow();
                    //                CheckBoxList1.Items.Add(SubMenuName);
                    //                dr2[0] = " ";
                    //                dt1.Rows.Add(dr2);


                    //            }
                    //        }

                    //    }
                    //}

                    BindCheckBoxListMaster(Tid);

                    //// chkList1 = new CheckBox();

                    // MChk_Menu.Text = MenuName;
                    // //chkList1.ID = "MChk" + Tid;
                    // //chkList1.AutoPostBack = true;

                    // MChk_Menu.Font.Name = "Verdana";
                    // MChk_Menu.Font.Size = 9;
                    // Panel1.Controls.Add(MChk_Menu);
                    // //DynamicCheckBoxName = "chkList1" + Tid.ToString();
                    // Panel1.Controls.Add(new LiteralControl("<br>"));

                    // AddCheckboxes("DD");
                }
                MChk_Menu1.Checked = true;
                MChk_Menu2.Checked = true;
                MChk_Menu3.Checked = true;
                MChk_Menu4.Checked = true;
                Menu1();
                Menu2();
                Menu3();
                Menu4();
            }
            else
            {

                //Server.Transfer("LoginDefault.aspx");
            }

        }
        else
        {
            if ((Session["User_ID"] != null) && (Session["Password"] != null))
            {
                Company_code = Session["Company_code"].ToString();
                plant_Code = Session["Plant_Code"].ToString();
                int companycode = Convert.ToInt32(Session["Company_code"]);
                int plantcode = Convert.ToInt32(Session["Plant_Code"]);
            }
            else
            {
                Server.Transfer("LoginDefault.aspx");
            }

        }

    }


    private void BindCheckBoxListMaster(int tidd)
    {
        try
        {
            ds = MBLL.GetSubMenuTitleCheckBoxMaster(tidd);
            if (tidd == 1)
            {
                CheckBoxList1.DataSource = ds;
                CheckBoxList1.DataTextField = "SubMenuName";
                CheckBoxList1.DataValueField = "SubMenuValue";
                CheckBoxList1.DataBind();
            }
            else if (tidd == 2)
            {
                CheckBoxList2.DataSource = ds;
                CheckBoxList2.DataTextField = "SubMenuName";
                CheckBoxList2.DataValueField = "SubMenuValue";
                CheckBoxList2.DataBind();
            }
            else if (tidd == 3)
            {
                CheckBoxList3.DataSource = ds;
                CheckBoxList3.DataTextField = "SubMenuName";
                CheckBoxList3.DataValueField = "SubMenuValue";
                CheckBoxList3.DataBind();
            }
            else if(tidd==4)
            {
                CheckBoxList4.DataSource = ds;
                CheckBoxList4.DataTextField = "SubMenuName";
                CheckBoxList4.DataValueField = "SubMenuValue";
                CheckBoxList4.DataBind();
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

    }

    private void AddCheckboxes(string strCheckboxText)
    {
        try
        {

            for (int intControlIndex = 1; intControlIndex <= 5; intControlIndex++)
            {
                int cid = Convert.ToInt32(intControlIndex);


                //chkList2 = new CheckBox();

                //chkList2.Text = strCheckboxText;
                //chkList2.ID = "Chk" + idinc + intControlIndex;
                //chkList2.Font.Name = "Verdana";
                //chkList2.Font.Size = 9;
                //Panel1.Controls.Add(chkList2);
                //Panel1.Controls.Add(new LiteralControl("<br>"));
                //idinc++;

            }
        }
        catch (Exception exp)
        {
            // throw new Exception(exp.Message);
            WebMsgBox.Show(exp.ToString());
        }
    }

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        Menu1();
    }
    private void Menu1()
    {
        if (MChk_Menu1.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                CheckBoxList1.Items[i].Selected = false;
            }
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        Menu2();
    }
    private void Menu2()
    {
        if (MChk_Menu2.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList2.Items.Count - 1; i++)
            {
                CheckBoxList2.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList2.Items.Count - 1; i++)
            {
                CheckBoxList2.Items[i].Selected = false;
            }
        }
    }
    protected void CheckBox1_CheckedChanged2(object sender, EventArgs e)
    {
        Menu3();
    }
    private void Menu3()
    {
        if (MChk_Menu3.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList3.Items.Count - 1; i++)
            {
                CheckBoxList3.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i <= CheckBoxList3.Items.Count - 1; i++)
            {
                CheckBoxList3.Items[i].Selected = false;
            }
        }
    }
    protected void MChk_Menu4_CheckedChanged(object sender, EventArgs e)
    {
        Menu4();
    }
    private void Menu4()
    {
        if (MChk_Menu4.Checked == true)
        {
            for (int i = 0; i <= CheckBoxList4.Items.Count - 1; i++)
            {
                CheckBoxList4.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < CheckBoxList4.Items.Count - 1; i++)
            {
                CheckBoxList4.Items[i].Selected = false;
            }
        }
           
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MChk_Menu1.Checked == true)
        {
            M1 = 11;
            M2 = 0;
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                if (CheckBoxList1.Items[i].Selected == false)
                {
                    SelectMenuValue = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                    MBLL.UpdateMenuRole(M2, SelectMenuValue, ddl_role.SelectedValue);

                }
            }
            for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
            {
                if (CheckBoxList1.Items[i].Selected == true)
                {
                    SelectMenuValue = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                    MBLL.UpdateMenuRole(M1, SelectMenuValue, ddl_role.SelectedValue);
                    M1++;

                }
            }

        }
        else
        {
            WebMsgBox.Show("please,Select the MASTER_MENU");
        }


        ////////TRANSACTION

        if (MChk_Menu2.Checked == true)
        {
            M1 = 11;
            M2 = 0;
            for (int i = 0; i <= CheckBoxList2.Items.Count - 1; i++)
            {
                if (CheckBoxList2.Items[i].Selected == false)
                {
                    SelectMenuValue = Convert.ToInt32(CheckBoxList2.Items[i].Value);
                    MBLL.UpdateMenuRole(M2, SelectMenuValue, ddl_role.SelectedValue);

                }
            }
            for (int i = 0; i <= CheckBoxList2.Items.Count - 1; i++)
            {
                if (CheckBoxList2.Items[i].Selected == true)
                {
                    SelectMenuValue = Convert.ToInt32(CheckBoxList2.Items[i].Value);


                    MBLL.UpdateMenuRole(M1, SelectMenuValue, ddl_role.SelectedValue);
                    M1++;

                }
            }

        }
        else
        {
            WebMsgBox.Show("please,Select the PROCUREMENT_MENU");
        }
        ///////REPORT

        if (MChk_Menu3.Checked == true)
        {
            M1 = 11;
            M2 = 0;
            for (int i = 0; i <= CheckBoxList3.Items.Count - 1; i++)
            {
                if (CheckBoxList3.Items[i].Selected == false)
                {
                    SelectMenuValue = Convert.ToInt32(CheckBoxList3.Items[i].Value);
                    MBLL.UpdateMenuRole(M2, SelectMenuValue, ddl_role.SelectedValue);

                }
            }
            for (int i = 0; i <= CheckBoxList3.Items.Count - 1; i++)
            {
                if (CheckBoxList3.Items[i].Selected == true)
                {
                    SelectMenuValue = Convert.ToInt32(CheckBoxList3.Items[i].Value);
                    MBLL.UpdateMenuRole(M1, SelectMenuValue, ddl_role.SelectedValue);
                    M1++;


                }
            }

        }
        else
        {
            WebMsgBox.Show("please,Select the TRANSACTION_MENU");
            
        }

        if (MChk_Menu4.Checked == true)
        {
            M1 = 11;
            M2 = 0;
            for (int i = 0; i <= CheckBoxList4.Items.Count - 1; i++)
            {
                if (CheckBoxList4.Items[i].Selected == false)
                {
                    SelectMenuValue = Convert.ToInt32(CheckBoxList4.Items[i].Value);
                    MBLL.UpdateMenuRole(M2, SelectMenuValue, ddl_role.SelectedValue);
                }
                else
                {
                    if (CheckBoxList4.Items[i].Selected == true)
                    {
                        SelectMenuValue = Convert.ToInt32(CheckBoxList4.Items[i].Value);
                        MBLL.UpdateMenuRole(M1, SelectMenuValue, ddl_role.SelectedValue);
                        M1++;
                    }

                }
            }
        }
        else
        {         
            WebMsgBox.Show("please,Select the REPORT_MENU");
        }


    }


    protected void ddl_role_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddl_role.SelectedItem.Value == "1")
        {
            RoleBasedSubmenuSelect1();
        }
        if (ddl_role.SelectedItem.Value == "2")
        {
            RoleBasedSubmenuSelect2();
        }

        if (ddl_role.SelectedItem.Value == "3")
        {
            RoleBasedSubmenuSelect3();
        }

        if (ddl_role.SelectedItem.Value == "4")
        {
            RoleBasedSubmenuSelect4();
        }
    }

    private void RoleBasedSubmenuSelect1()
    {
        // menuload id-1 and role-2
        int val1 = 0;
        int menuid = 0;
        int i = 0;

        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 1);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "1")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A1"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 1)
                    {
                        CheckBoxList1.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList1.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
        // menuload id-2 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 2);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "1")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A1"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 2)
                    {
                        CheckBoxList2.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList2.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-3 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 3);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "1")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A1"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 3)
                    {
                        CheckBoxList3.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList3.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-4 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 4);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "1")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A1"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 4)
                    {
                        CheckBoxList4.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList4.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
    }

    private void RoleBasedSubmenuSelect2()
    {
        // menuload id-1 and role-2
        int val1=0;
        int menuid = 0;       
        int i = 0;        
        
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value,1);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "2")
            {               
                while (dr.Read())
                {
                    val1=int.Parse( dr["A2"].ToString() );
                    menuid = int.Parse(dr["MenuLoadid"].ToString());               
                           
                       
                       if (val1 > 0 && menuid==1)
                       {
                           CheckBoxList1.Items[i].Selected = true;                           
                           i++;                           
                       }
                       else
                       {
                           CheckBoxList1.Items[i].Selected = false;                           
                           i++;
                       }                                      
                    
                }
            }
            
        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
        // menuload id-2 and role-2

         val1 = 0;
         menuid = 0;
         i = 0;  
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 2);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "2")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A2"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 2)
                    {
                        CheckBoxList2.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList2.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-3 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 3);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "2")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A2"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 3)
                    {
                        CheckBoxList3.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList3.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-4 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 4);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "2")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A2"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 4)
                    {
                        CheckBoxList4.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList4.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
    }

    private void RoleBasedSubmenuSelect3()
    {
        // menuload id-1 and role-2
        int val1 = 0;
        int menuid = 0;
        int i = 0;

        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 1);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "3")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A3"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 1)
                    {
                        CheckBoxList1.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList1.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
        // menuload id-2 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 2);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "3")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A3"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 2)
                    {
                        CheckBoxList2.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList2.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-3 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 3);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "3")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A3"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 3)
                    {
                        CheckBoxList3.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList3.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-4 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 4);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "3")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A3"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 4)
                    {
                        CheckBoxList4.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList4.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
    }

    private void RoleBasedSubmenuSelect4()
    {
        // menuload id-1 and role-2
        int val1 = 0;
        int menuid = 0;
        int i = 0;

        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 1);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "4")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A4"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 1)
                    {
                        CheckBoxList1.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList1.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
        // menuload id-2 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 2);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "4")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A4"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 2)
                    {
                        CheckBoxList2.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList2.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-3 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 3);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "4")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A4"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 3)
                    {
                        CheckBoxList3.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList3.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }

        // menuload id-4 and role-2

        val1 = 0;
        menuid = 0;
        i = 0;
        dr = null;
        dr = MBLL.RoleBasedSubmenuSelect(ddl_role.SelectedItem.Value, 4);
        if (dr.HasRows == true)
        {
            if (ddl_role.SelectedItem.Value == "4")
            {
                while (dr.Read())
                {
                    val1 = int.Parse(dr["A4"].ToString());
                    menuid = int.Parse(dr["MenuLoadid"].ToString());


                    if (val1 > 0 && menuid == 4)
                    {
                        CheckBoxList4.Items[i].Selected = true;
                        i++;
                    }
                    else
                    {
                        CheckBoxList4.Items[i].Selected = false;
                        i++;
                    }

                }
            }

        }
        else
        {
            WebMsgBox.Show("Permission Not given...");
        }
    }
}
