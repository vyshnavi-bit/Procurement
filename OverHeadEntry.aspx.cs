using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class OverHeadEntry : System.Web.UI.Page

{
    public string ccode;
    public string pcode;
    public string managmobNo;
    public string pname;
    public string cname;
    DataSet ds = new DataSet();
    BOLD_Accoun acc = new BOLD_Accoun();
    SqlConnection con = new SqlConnection();   
    DbHelper DBaccess = new DbHelper();
    BLLPlantName BllPlant = new BLLPlantName();
    string str;
    string[] strarr = new string[10];
    string headid = string.Empty;
    string Groupheadid = string.Empty;
    public double CreditAmount, DebitAmount;

    DateTime tdt = new DateTime();
    string strsql = string.Empty;

    public int refNo = 0;
    public static int roleid;
    protected void Page_Load(object sender, EventArgs e)
    {
        CreditAmount = 0.0;
        DebitAmount = 0.0;
         if (IsPostBack != true)
        {
            if ((Session["Name"] != null) && (Session["pass"] != null))
            {

                ccode = Session["Company_code"].ToString();
                pcode = Session["Plant_Code"].ToString();
                cname = Session["cname"].ToString();
                pname = Session["pname"].ToString();
             //   managmobNo = Session["managmobNo"].ToString();

                tdt = System.DateTime.Now;
                txt_EntryDate.Text = tdt.ToString("dd/MM/yyy");

                 if (roleid < 3)
               
                {
                    LoadSinglePlantName();
                }
                else
                {
                    LoadPlantName();
                }
                

                pcode = ddl_PlantName.SelectedItem.Value;
                LoadAccountHead(ccode);
                headid = ddl_HeadName.SelectedItem.Value;
                LoadGiverName(ccode);

                str = headid;
                strarr = str.Split('_');
                if (strarr[0].ToString() != "00")
                {
                    Groupheadid = strarr[0];
                    headid = strarr[1];
                }
                //  Groupheadid = ddl_GroupHeadName.SelectedItem.Value;
                LoadAccountSubHead(ccode, headid, Groupheadid);
                if (rd_Cash.Checked == true)
                {
                    rd_Cheque.Checked = false;
                    rd_Journal.Checked = false;

                    Lbl_Giver.Visible = false;
                    ddl_Giver.Visible = false;
                    Lbl_ChequeDate.Visible = false;
                    txt_ChequeDate.Visible = false;
                    Lbl_favourof.Visible = false;
                    txt_favourof.Visible = false;
                    Lbl_BankName.Visible = false;
                    txt_BankName.Visible = false;
                    Lbl_ClearingDate.Visible = false;
                    txt_ClearingDate.Visible = false;
                }
                Lbl_Errormsg.Visible = false;
                
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
                 ccode = Session["Company_code"].ToString();
                 //  pcode = Session["Plant_Code"].ToString();
                 cname = Session["cname"].ToString();
                 pname = Session["pname"].ToString();
             //    managmobNo = Session["managmobNo"].ToString();
                 pcode = ddl_PlantName.SelectedItem.Value;
                 headid = ddl_HeadName.SelectedItem.Value;
                 str = headid;
                 strarr = str.Split('_');
                 if (strarr[0].ToString() != "00")
                 {
                     Groupheadid = strarr[0];
                     headid = strarr[1];
                 }
                 Lbl_Errormsg.Visible = false;
             }
             else
             {
                 Server.Transfer("LoginDefault.aspx");
             }
            
         }
    }


   
    private void LoadAccountHead(string Ccode)
    {
        try
        {
            ds = null;
            ds = GetAccountHead(Ccode.ToString());
            ddl_HeadName .DataSource = ds;
            ddl_HeadName.DataTextField = "Head_Name";
            ddl_HeadName.DataValueField = "Head_Id";
            ddl_HeadName.DataBind();
            ddl_GroupHeadName.DataSource = ds;
            ddl_GroupHeadName.DataTextField = "GroupHead_ID";
            ddl_GroupHeadName.DataValueField = "GroupHead_ID";           
            ddl_GroupHeadName.DataBind();
            if (ddl_HeadName.Items.Count > 0)
            {
            }
            else
            {
                ddl_HeadName.Items.Add("--Add AccountHead Name--");
            }
        }
        catch (Exception ex)
        {
        }
    }
    public DataSet GetAccountHead(string ccode)
    {
        DataSet ds = new DataSet();
        ds = null;
       // strsql = "SELECT Head_Id,CONVERT(nvarchar(50),Head_Id)+'_'+ Head_Name AS Head_Name FROM Account_HeadName Order by Head_Id";
       // strsql = "SELECT Head_Id,Head_Name AS Head_Name FROM Account_HeadName Order by Head_Id";
       // strsql = "SELECT ah.Gid AS GroupHead_ID,ah.Head_Id AS Head_Id,ah.Head_Name AS Head_Name FROM (SELECT GroupHead_ID FROM accounts_groupname) AS gh INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id,Head_Name AS Head_Name FROM Account_HeadName)AS ah ON gh.GroupHead_ID=ah.Gid order by ah.Gid,Head_Id";
        strsql = "SELECT CONVERT(Nvarchar(15),ah.Gid)+'_'+CONVERT(Nvarchar(15),ah.Head_Id) AS Head_Id,ah.Head_Name AS Head_Name FROM (SELECT GroupHead_ID FROM accounts_groupname) AS gh INNER JOIN (SELECT GroupHead_ID AS Gid,Head_Id,Head_Name AS Head_Name FROM Account_HeadName)AS ah ON gh.GroupHead_ID=ah.Gid order by ah.Gid,Head_Id";
        ds = DBaccess.GetDataset(strsql);
        return ds;
    }

    private void LoadAccountSubHead(string Ccode, string headid, string groupheadcode)
    {
        try
        {
            ds = null;
            ds = GetAccountSubHead(Ccode, headid, groupheadcode);
            ddl_SubHeadName.DataSource = ds;
            ddl_SubHeadName.DataTextField = "Head_Name";
            ddl_SubHeadName.DataValueField = "Ledger_Id";
            ddl_SubHeadName.DataBind();
            if (ddl_SubHeadName.Items.Count > 0)
            {
            }
            else
            {
                ddl_SubHeadName.Items.Add("--Add AccountSubHead Name--");
            }
        }
        catch (Exception ex)
        {
        }
    }
    public DataSet GetAccountSubHead(string ccode, string Headid, string groupheadid)
    {
        pcode = ddl_PlantName.SelectedItem.Value;
        DataSet ds = new DataSet();
        

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand("[dbo].[Get_Ledger]");
            SqlParameter parcompanycode, parplantcode, parGroupId, parHeadId;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            parcompanycode = cmd.Parameters.Add("@spccode", SqlDbType.NVarChar,15);
            parplantcode = cmd.Parameters.Add("@sppcode", SqlDbType.NVarChar, 15);
            parGroupId = cmd.Parameters.Add("@spGroupId", SqlDbType.NVarChar, 15);
            parHeadId = cmd.Parameters.Add("@spHeadId", SqlDbType.NVarChar, 15);

            parcompanycode.Value = ccode;
            parplantcode.Value = pcode;
            parGroupId.Value = Groupheadid.Trim();
            parHeadId.Value = Headid.Trim();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
    }
  private void LoadGiverName(string Ccode)
  {
      try
      {
          ds = null;
          ds = GetGiverName(Ccode);
          ddl_Giver.DataSource = ds;
          ddl_Giver.DataTextField = "Head_Name";
          ddl_Giver.DataValueField = "Giver_Id";
          ddl_Giver.DataBind();
          if (ddl_Giver.Items.Count > 0)
          {
          }
          else
          {
              ddl_Giver.Items.Add("--Add GiverName--");
          }
      }
      catch (Exception ex)
      {
      }
  }
  public DataSet GetGiverName(string ccode)
  {
      DataSet ds = new DataSet();
      ds = null;
      strsql = "SELECT Giver_Id,CONVERT(nvarchar(50),Giver_Id)+'_'+ Giver_Name AS Head_Name FROM GiverMaster Order by Giver_Id";
      ds = DBaccess.GetDataset(strsql);
      return ds;
  }

    protected void rd_Cash_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_Cash.Checked == true)
        {
            rd_Cheque.Checked = false;
            rd_Journal.Checked = false;

            Lbl_Giver.Visible = false;
            ddl_Giver.Visible = false;
            Lbl_ChequeDate.Visible = false;
            txt_ChequeDate.Visible = false;
            Lbl_favourof.Visible = false;
            txt_favourof.Visible = false;
            Lbl_BankName.Visible = false;
            txt_BankName.Visible = false;
            Lbl_ClearingDate.Visible = false;
            txt_ClearingDate.Visible = false;
        }     
    }
    protected void rd_Cheque_CheckedChanged(object sender, EventArgs e)
    {
         if (rd_Cheque.Checked == true)
        {
            rd_Cash.Checked = false;
            rd_Journal.Checked = false;

            Lbl_Giver.Visible = false;
            ddl_Giver.Visible = false;

            Lbl_ChequeDate.Visible = true;
            txt_ChequeDate.Visible = true;
            Lbl_favourof.Visible = true;
            txt_favourof.Visible = true;
            Lbl_BankName.Visible = true;
            txt_BankName.Visible = true;
            Lbl_ClearingDate.Visible = true;
            txt_ClearingDate.Visible = true;
        }
    }
    protected void rd_Journal_CheckedChanged(object sender, EventArgs e)
    {
        if (rd_Journal.Checked == true)
        {
            rd_Cheque.Checked = false;
            rd_Cash.Checked = false;

            Lbl_Giver.Visible = true;
            ddl_Giver.Visible = true;

            Lbl_ChequeDate.Visible = false;
            txt_ChequeDate.Visible = false;
            Lbl_favourof.Visible = false;
            txt_favourof.Visible = false;
            Lbl_BankName.Visible = false;
            txt_BankName.Visible = false;
            Lbl_ClearingDate.Visible = false;
            txt_ClearingDate.Visible = false;
        }
    }


    protected void ddl_HeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddl_GroupHeadName.SelectedIndex = ddl_HeadName.SelectedIndex;
        // Groupheadid = ddl_GroupHeadName.SelectedItem.Value.Trim();
        headid = ddl_HeadName.SelectedItem.Value.Trim();
        
        strarr = str.Split('_');
        if (strarr[0].ToString() != "00")
        {
            Groupheadid = strarr[0];
            headid = strarr[1];
        }
        LoadAccountSubHead(ccode, headid, Groupheadid);
    }
    private void Clear()
    {
        txt_Name.Text = "";
        txt_Amount.Text = "";
        txt_Narration.Text = "";
        txt_ChequeDate.Text = "";
        txt_favourof.Text = "";
        txt_BankName.Text = "";
        txt_ClearingDate.Text = "";
    }
    protected void ddl_GroupHeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    private void LoadPlantName()
    {
        try
        {           
        
            ds = null;
            ds = BllPlant.LoadPlantNameChkLst1(ccode.ToString());
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    private void LoadSinglePlantName()
    {
        try
        {

            ds = null;
            ds = BllPlant.LoadSinglePlantNameChkLst1(ccode.ToString(), pcode);
            if (ds != null)
            {
                ddl_PlantName.DataSource = ds;
                ddl_PlantName.DataTextField = "Plant_Name";
                ddl_PlantName.DataValueField = "plant_Code";//ROUTE_ID 
                ddl_PlantName.DataBind();

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    public void gettid()
    {       
            try
            {
                string sqlstr = "SELECT max(Reference_No) as  Tid  FROM account_transaction  WHERE Plant_Code='" + ddl_PlantName.SelectedItem.Value + "' ";         
                refNo = DBaccess.ExecuteScalarint(sqlstr);
                if (refNo != null)
                {
                    refNo = refNo + 1;
                }
                else
                {
                    refNo = 1;
                }

            }
            catch
            {
                refNo = 1;
            }
       
    }
    public double CheckAvaileBalance()
    {
        double val1 = 0.0;
        try
        {
            con = null;
            using (con = DBaccess.GetConnection())
            {
                DateTime dt1 = new DateTime();
                string d1 = string.Empty;
                dt1 = DateTime.ParseExact(txt_EntryDate.Text, "dd/MM/yyyy", null);
                d1 = dt1.ToString("MM/dd/yyyy");

                string mess = string.Empty;
                string Val = string.Empty;
                int ress = 0;
                SqlCommand cmd = new SqlCommand("Check_AvailAmount");
                SqlParameter parcompanycode, parplantcode, parfrmdate, partodate, parmess, parress, parVal;
                //  cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                parcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
                parplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);
                parfrmdate = cmd.Parameters.Add("@fromdate", SqlDbType.DateTime);
                partodate = cmd.Parameters.Add("@todate", SqlDbType.DateTime);

                parmess = cmd.Parameters.Add("@mess", SqlDbType.Char, 50);
                cmd.Parameters["@mess"].Direction = ParameterDirection.Output;
                parress = cmd.Parameters.Add("@ress", SqlDbType.Int);
                cmd.Parameters["@ress"].Direction = ParameterDirection.Output;
                parVal = cmd.Parameters.Add("@Val", SqlDbType.NVarChar, 50);
                cmd.Parameters["@Val"].Direction = ParameterDirection.Output;

                parcompanycode.Value = ccode;
                parplantcode.Value = pcode;
                parfrmdate.Value = d1.ToString().Trim();
                partodate.Value = d1.ToString().Trim();


                cmd.ExecuteNonQuery();
                mess = (string)cmd.Parameters["@mess"].Value;
                ress = (int)cmd.Parameters["@ress"].Value;
                Val = (string)cmd.Parameters["@Val"].Value;
                val1 = Convert.ToDouble(Val);

                return val1;
            }
        }
        catch (Exception ex)
        {
            return val1;
        }

    }

    public void IouDueRecovery()
    {
       
        try
        {
            con = null;
            using (con = DBaccess.GetConnection())
            {
                DateTime dt1 = new DateTime();
                string d1 = string.Empty;
                dt1 = DateTime.ParseExact(txt_EntryDate.Text, "dd/MM/yyyy", null);
                d1 = dt1.ToString("MM/dd/yyyy");              
               
                SqlCommand cmd = new SqlCommand("Insert_IouDueRecoveryAmount");
                SqlParameter parcompanycode, parplantcode, parEntrydate, parcashReceiptAmount, parGhead, parhead, parledgerhead, parReferenceno;                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                parcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.Int);
                parplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.Int);
                parEntrydate = cmd.Parameters.Add("@Entrydate", SqlDbType.DateTime);
                parcashReceiptAmount = cmd.Parameters.Add("@cashReceiptAmount", SqlDbType.Money);
                parGhead = cmd.Parameters.Add("@Ghead", SqlDbType.Int);
                parhead = cmd.Parameters.Add("@head", SqlDbType.Int);
                parledgerhead = cmd.Parameters.Add("@ledgerhead", SqlDbType.Int);
                parReferenceno = cmd.Parameters.Add("@Referenceno", SqlDbType.Int);

                parcompanycode.Value = ccode;
                parplantcode.Value = pcode;
                parEntrydate.Value = d1.ToString().Trim();
                parcashReceiptAmount.Value = txt_Amount.Text;
                parGhead.Value = Groupheadid;
                parhead.Value = headid;
                parledgerhead.Value = ddl_SubHeadName.SelectedItem.Value.Trim();

                parReferenceno.Value = refNo;

                cmd.ExecuteNonQuery();          
              
            }
        }
        catch (Exception ex)
        {
           
        }

    }

    private void Save()
    {
        try
        {
            using (con = DBaccess.GetConnection())
            {

                DateTime dt1 = new DateTime();
                DateTime dt2 = new DateTime();
                DateTime dt3 = new DateTime();
                string d1 = string.Empty;
                string d2 = string.Empty;
                string d3 = string.Empty;

                dt1 = DateTime.ParseExact(txt_EntryDate.Text, "dd/MM/yyyy", null);
                d1 = dt1.ToString("MM/dd/yyyy");
                if (rd_Cheque.Checked == true)
                {
                    dt2 = DateTime.ParseExact(txt_ChequeDate.Text, "dd/MM/yyyy", null);
                    dt3 = DateTime.ParseExact(txt_ClearingDate.Text, "dd/MM/yyyy", null);

                    d2 = dt2.ToString("MM/dd/yyyy");
                    d3 = dt3.ToString("MM/dd/yyyy");
                }

                string mode = string.Empty;

                if (rd_Cash.Checked == true)
                {
                    mode = rd_Cash.Text;

                    txt_ChequeDate.Text = string.Empty;
                    txt_favourof.Text = string.Empty;
                    txt_BankName.Text = string.Empty;
                    txt_ClearingDate.Text = string.Empty;
                    d2 = string.Empty;
                    d3 = string.Empty;
                }
                else if (rd_Cheque.Checked == true)
                {
                    mode = rd_Cheque.Text;
                }
                else
                {
                    mode = rd_Journal.Text;
                    txt_ChequeDate.Text = "";
                    txt_favourof.Text = "";
                    txt_BankName.Text = "";
                    txt_ClearingDate.Text = "";
                    d2 = string.Empty;
                    d3 = string.Empty;
                }

                headid = ddl_HeadName.SelectedItem.Value.Trim();
                str = headid;
                strarr = str.Split('_');
                if (strarr[0].ToString() != "00")
                {
                    Groupheadid = strarr[0];
                    headid = strarr[1];
                }
                gettid();
                //if (ddl_Type.SelectedIndex == 0)
                //{
                //    CreditAmount = Convert.ToDouble(txt_Amount.Text);
                //    DebitAmount = 0.0;
                //}
                //else
                //{
                //    DebitAmount = Convert.ToDouble(txt_Amount.Text);
                //    CreditAmount = 0.0;
                //}

                if (ddl_Type.SelectedIndex == 0)
                {

                    DebitAmount = Convert.ToDouble(txt_Amount.Text);
                    CreditAmount = 0.0;
                }
                else
                {
                    CreditAmount = Convert.ToDouble(txt_Amount.Text);
                    DebitAmount = 0.0;
                }



                if (rd_Journal.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Account_transaction(Reference_No,EntryDate,Head_Id,SubHead_Id,ModeofEntry,CreditAmount,Narration,Check_Date,Faviour_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,plant_code,GroupHead_Id,Reference_Name,DebitAmount,IouRef_No) VALUES('" + refNo + "','" + d1.ToString() + "','" + headid + "','" + ddl_SubHeadName.SelectedItem.Value + "','" + mode + "','" + CreditAmount + "','" + txt_Narration.Text + "','" + d2.ToString() + "','" + txt_favourof.Text + "','" + txt_BankName.Text + "','" + d3.ToString() + "','" + ddl_Giver.SelectedItem.Value + "','" + ddl_Type.SelectedItem.Value + "','" + ddl_PlantName.SelectedItem.Value + "','" + Groupheadid + "','" + txt_Name.Text + "','" + DebitAmount + "','" + 0.00 + "') ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Account_transaction(Reference_No,EntryDate,Head_Id,SubHead_Id,ModeofEntry,CreditAmount,Narration,Check_Date,Faviour_Name,Bank_Name,CheckClearing_Date,Giver_Id,Voucher_Type,plant_code,GroupHead_Id,Reference_Name,DebitAmount,IouRef_No ) VALUES('" + refNo + "','" + d1.ToString() + "','" + headid + "','" + ddl_SubHeadName.SelectedItem.Value + "','" + mode + "','" + CreditAmount + "','" + txt_Narration.Text + "','" + d2.ToString() + "','" + txt_favourof.Text + "','" + txt_BankName.Text + "','" + d3.ToString() + "','" + 0 + "','" + ddl_Type.SelectedItem.Value + "','" + ddl_PlantName.SelectedItem.Value + "','" + Groupheadid + "','" + txt_Name.Text + "','" + DebitAmount + "','" + 0.00 + "') ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (ddl_Type.SelectedIndex == 0)
                {
                    IouDueRecovery();
                    //sp update the IOU Start
                    //SqlCommand sqlCmd1 = new SqlCommand("dbo.[Insert_IouDueRecoveryAmount]");
                    //sqlCmd1.Connection = con;
                    //sqlCmd1.CommandType = CommandType.StoredProcedure;
                    //sqlCmd1.Parameters.AddWithValue("@companycode", ccode);
                    //sqlCmd1.Parameters.AddWithValue("@plantcode", ddl_PlantName.SelectedItem.Value);
                    //sqlCmd1.Parameters.AddWithValue("@Entrydate", d1.Trim());
                    //sqlCmd1.Parameters.AddWithValue("@cashReceiptAmount", CreditAmount);
                    //sqlCmd1.Parameters.AddWithValue("@Ghead", Groupheadid);
                    //sqlCmd1.Parameters.AddWithValue("@head", headid);
                    //sqlCmd1.Parameters.AddWithValue("@ledgerhead", ddl_SubHeadName.SelectedItem.Value);
                    //sqlCmd1.Parameters.AddWithValue("@Referenceno", refNo);
                    //sqlCmd1.ExecuteNonQuery();
                    //sp update the IOU End
                }
                string countmsg = "Data Inserted Successfully...";
                uscMsgBox1.AddMessage(countmsg, MessageBoxUsc_Message.enmMessageType.Success);
                Clear();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        double AvailClosingBalance = 0.0;
        try
        {
            if (ddl_Type.SelectedIndex == 1)
            {
                //IouDueRecovery From the Cashpayment               
                Save();
               // IouDueRecovery();
            }
            else
            {
                AvailClosingBalance = CheckAvaileBalance();

                if (AvailClosingBalance > 0 && Convert.ToDouble(txt_Amount.Text) <= AvailClosingBalance)
                {
                    Save();
                }
                else
                {
                    Lbl_Errormsg.Visible = true;
                    Lbl_Errormsg.Text = " Note : Your AvailableBalance is :" + AvailClosingBalance.ToString();

                    //WebMsgBox.Show("Please Check the Available Balance_[ " + AvailClosingBalance + "]");
                }
            }
          
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(" Note :Please Check the Available Balance_[ " + AvailClosingBalance + "]");
        }
    }
}