using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class CashBook : System.Web.UI.Page
{
    SqlCommand cmd;
    string BranchID = "";
    SalesDBManager vdm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["branch"] == null)
        {
            Response.Redirect("LoginDefault.aspx");
        }
        else
        {
            BranchID = Session["branch"].ToString();
        }
        //vdm = new SalesDBManager();
        if (!this.IsPostBack)
        {
            if (!Page.IsCallback)
            {
                txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                lblTitle.Text = Session["TitleName"].ToString();
                FillRouteName();

            }
        }
    }
    void FillRouteName()
    {
        vdm = new SalesDBManager();
        if (Session["LevelType"].ToString() == "2" || Session["LevelType"].ToString() == "1")
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master WHERE (Plant_Code = @BranchID)");
            cmd.Parameters.Add("@BranchID", Session["branch"].ToString());
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlSalesOffice.DataSource = dtPlant;
            ddlSalesOffice.DataTextField = "Plant_Name";
            ddlSalesOffice.DataValueField = "Plant_Code";
            ddlSalesOffice.DataBind();
        }
        else
        {
            cmd = new SqlCommand("SELECT Plant_Code, Plant_Name FROM Plant_Master ");
            DataTable dtPlant = vdm.SelectQuery(cmd).Tables[0];
            ddlSalesOffice.DataSource = dtPlant;
            ddlSalesOffice.DataTextField = "Plant_Name";
            ddlSalesOffice.DataValueField = "Plant_Code";
            ddlSalesOffice.DataBind();
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GetReport();
    }
    private DateTime GetLowDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        DT = dt;
        Hour = -dt.Hour;
        Min = -dt.Minute;
        Sec = -dt.Second;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }
    private DateTime GetHighDate(DateTime dt)
    {
        double Hour, Min, Sec;
        DateTime DT = DateTime.Now;
        Hour = 23 - dt.Hour;
        Min = 59 - dt.Minute;
        Sec = 59 - dt.Second;
        DT = dt;
        DT = DT.AddHours(Hour);
        DT = DT.AddMinutes(Min);
        DT = DT.AddSeconds(Sec);
        return DT;
    }
    DataTable RouteReport = new DataTable();
    DataTable CashPayReport = new DataTable();
    DataTable IOUReport = new DataTable();
    void GetReport()
    {
        try
        {
            pnlfoter.Visible = true;
            pnlHide.Visible = true;
            lblSalesOffice.Text = "";
            lblOppBal.Text = "";
            lblpreparedby.Text = "";
            lblmsg.Text = "";
            lblCash.Text = "";
            lblTotalAmout.Text = "";
            lblDiffernce.Text = "";
            vdm = new SalesDBManager();
            RouteReport = new DataTable();
            CashPayReport = new DataTable();
            IOUReport = new DataTable();
            DateTime fromdate = DateTime.Now;
            string[] fromdatestrig = txtFromdate.Text.Split(' ');
            if (fromdatestrig.Length > 1)
            {
                if (fromdatestrig[0].Split('-').Length > 0)
                {
                    string[] dates = fromdatestrig[0].Split('-');
                    string[] times = fromdatestrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            Session["filename"] = "Cash Book ->" + Session["branchname"].ToString();
            lblSalesOffice.Text = ddlSalesOffice.SelectedItem.Text;
            lbl_fromDate.Text = txtFromdate.Text;
            string BranchID = ddlSalesOffice.SelectedValue;
            RouteReport.Columns.Add("DispName");
            RouteReport.Columns.Add("Reciept No");
            RouteReport.Columns.Add("Received Amount").DataType = typeof(Double);
            cmd = new SqlCommand("SELECT Branchid, AmountPaid FROM collections WHERE (Branchid = @BranchID) AND (PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate).AddDays(-1));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate).AddDays(-1));
            DataTable dtOpp = vdm.SelectQuery(cmd).Tables[0];
            if (dtOpp.Rows.Count > 0)
            {
                lblOppBal.Text = dtOpp.Rows[0]["AmountPaid"].ToString();
            }
            cmd = new SqlCommand("SELECT Branchid, AmountPaid,VarifyDate FROM collections WHERE (Branchid = @BranchID) AND (PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            DataTable dtclosedtime = vdm.SelectQuery(cmd).Tables[0];
            if (dtclosedtime.Rows.Count > 0)
            {
                lbl_ClosingDate.Text = dtclosedtime.Rows[0]["VarifyDate"].ToString();
            }
            cmd = new SqlCommand("SELECT Branchid, Amount, Remarks, DOE, Receiptno, Name,PaymentType FROM cashcollections WHERE (Branchid = @Branchid) AND (DOE BETWEEN @d1 AND @d2)  AND (CollectionType = 'Cash')");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            DataTable dtCashCollection = vdm.SelectQuery(cmd).Tables[0];
            if (dtCashCollection.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCashCollection.Rows)
                {
                    DataRow newrow = RouteReport.NewRow();
                    newrow["DispName"] = dr["Name"].ToString();
                    newrow["Reciept No"] = dr["Receiptno"].ToString();
                    string Amount = dr["Amount"].ToString();
                    if (Amount == "0")
                    {
                    }
                    else
                    {
                        newrow["Received Amount"] = dr["Amount"].ToString();
                        RouteReport.Rows.Add(newrow);
                    }
                }
            }
            DataView dv = RouteReport.DefaultView;
            dv.Sort = "Reciept No ASC";
            DataTable sortedDT = dv.ToTable();
            cmd = new SqlCommand("SELECT CashTo as Payments,ApprovedAmount as Amount,VocherID FROM cashpayables WHERE  (BranchID = @BranchID) AND (DOE BETWEEN @d1 AND @d2) AND (Status = 'P') AND (VoucherType = 'Debit') and (Status <>'C')");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            DataTable dtCashPayble = vdm.SelectQuery(cmd).Tables[0];
            DataRow newvartical = sortedDT.NewRow();
            newvartical["DispName"] = "Total";
            double val = 0.0;
            foreach (DataColumn dc in sortedDT.Columns)
            {
                if (dc.DataType == typeof(Double))
                {
                    val = 0.0;
                    double.TryParse(sortedDT.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out val);
                    newvartical[dc.ToString()] = val;
                }
            }
            sortedDT.Rows.Add(newvartical);
            DataRow newrowBal = sortedDT.NewRow();
            newrowBal["DispName"] = "Clo Balance";
            double OppBal = 0;
            if (dtOpp.Rows.Count > 0)
            {
                double.TryParse(dtOpp.Rows[0]["AmountPaid"].ToString(), out OppBal);
            }
            double TotalAmount = val + OppBal;
            newrowBal["Received Amount"] = val + OppBal;
            sortedDT.Rows.Add(newrowBal);
            grdRouteCash.DataSource = sortedDT;
            grdRouteCash.DataBind();
            CashPayReport.Columns.Add("Vocher ID");
            CashPayReport.Columns.Add("Payments");
            CashPayReport.Columns.Add("Amount").DataType = typeof(Double);
            cmd = new SqlCommand("SELECT cashpayables.Sno, cashpayables.BranchID, cashpayables.CashTo, subpayable.HeadSno, cashpayables.DOE, cashpayables.VocherID, cashpayables.Remarks, SUM(cashpayables.ApprovedAmount) AS Amount, accountheads.HeadName FROM cashpayables INNER JOIN subpayable ON cashpayables.Sno = subpayable.RefNo INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.Status = 'P') AND (cashpayables.VoucherType = 'Credit') GROUP BY cashpayables.Sno, cashpayables.BranchID, cashpayables.CashTo, subpayable.HeadSno, cashpayables.DOE, cashpayables.VocherID, cashpayables.Remarks,  cashpayables.ApprovedAmount, accountheads.HeadName ORDER BY accountheads.HeadName");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            DataTable dtCredit = vdm.SelectQuery(cmd).Tables[0];
            foreach (DataRow dr in dtCashPayble.Rows)
            {
                DataRow newrow = CashPayReport.NewRow();
                newrow["Vocher ID"] = dr["VocherID"].ToString();
                newrow["Payments"] = dr["Payments"].ToString();
                double Amount = 0;
                double.TryParse(dr["Amount"].ToString(), out Amount);
                newrow["Amount"] = Amount;
                CashPayReport.Rows.Add(newrow);
            }
            DataRow newCash = CashPayReport.NewRow();
            newCash["Payments"] = "Total";
            double valnewCash = 0.0;
            foreach (DataColumn dc in CashPayReport.Columns)
            {
                if (dc.DataType == typeof(Double))
                {
                    valnewCash = 0.0;
                    double.TryParse(CashPayReport.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out valnewCash);
                    newCash[dc.ToString()] = valnewCash;
                }
            }
            CashPayReport.Rows.Add(newCash);
            double DebitCash = 0;
            DataRow newDebitBal = CashPayReport.NewRow();
            newDebitBal["Payments"] = "Closing Balance";
            newDebitBal["Amount"] = TotalAmount - valnewCash;
            DebitCash = TotalAmount - valnewCash;
            CashPayReport.Rows.Add(newDebitBal);
            lblhidden.Text = DebitCash.ToString();
            grdCashPayable.DataSource = CashPayReport;
            grdCashPayable.DataBind();
            DataTable dtIOU = new DataTable();
            IOUReport.Columns.Add("Sno");
            IOUReport.Columns.Add("IOU");
            IOUReport.Columns.Add("Amount").DataType = typeof(Double);
            cmd = new SqlCommand("SELECT IOU as onNameof, Amount  FROM ioutable WHERE (BranchID = @BranchID) AND (DOE BETWEEN @d1 AND @d2) ");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            dtIOU = vdm.SelectQuery(cmd).Tables[0];
            if (dtIOU.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dtIOU.Rows)
                {
                    DataRow newrow = IOUReport.NewRow();
                    newrow["Sno"] = i++.ToString();
                    //newrow["IOU"] = dr["onNameof"].ToString();
                    newrow["IOU"] = dr["onNameof"].ToString();
                    string Amount = dr["Amount"].ToString();
                    if (Amount == "0")
                    {
                    }
                    else
                    {
                        newrow["Amount"] = dr["Amount"].ToString();
                        IOUReport.Rows.Add(newrow);
                    }
                }
            }
            else
            {
                cmd = new SqlCommand("SELECT cashpayables.onNameof, cashpayables.CashTo, SUM(cashpayables.ApprovedAmount) AS Amount, subpayable.HeadSno, accountheads.HeadName FROM cashpayables INNER JOIN subpayable ON cashpayables.Sno = subpayable.RefNo INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.VoucherType = 'Due') AND (cashpayables.Status <> 'C') AND (cashpayables.Status = 'P') GROUP BY cashpayables.onNameof, cashpayables.CashTo, cashpayables.Amount, subpayable.HeadSno, accountheads.HeadName ORDER BY accountheads.HeadName");
                cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
                dtIOU = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in dtIOU.Rows)
                {
                    DataRow newrow = IOUReport.NewRow();
                    newrow["Sno"] = dr["HeadSno"].ToString();
                    newrow["IOU"] = dr["CashTo"].ToString();
                    newrow["Amount"] = dr["Amount"].ToString();
                    IOUReport.Rows.Add(newrow);
                }
                foreach (DataRow dr in IOUReport.Rows)
                {
                    foreach (DataRow drcredit in dtCredit.Rows)
                    {
                        if (dr["Sno"].ToString() == drcredit["HeadSno"].ToString())
                        {
                            double CAmount = 0;
                            double.TryParse(drcredit["Amount"].ToString(), out CAmount);
                            double Amount = 0;
                            double.TryParse(dr["Amount"].ToString(), out Amount);
                            double TAmount = Amount - CAmount;
                            dr["Amount"] = TAmount;
                        }
                    }
                }
                DataTable Report = new DataTable();
                Report.Columns.Add("Sno");
                Report.Columns.Add("IOU");
                Report.Columns.Add("Amount").DataType = typeof(Double);
                foreach (DataRow dr in IOUReport.Rows)
                {
                    DataRow newrow = Report.NewRow();
                    newrow["Sno"] = dr["Sno"].ToString();
                    newrow["IOU"] = dr["IOU"].ToString();
                    string Amount = dr["Amount"].ToString();
                    if (Amount == "0")
                    {
                    }
                    else
                    {
                        newrow["Amount"] = dr["Amount"].ToString();
                        Report.Rows.Add(newrow);
                    }
                }
                IOUReport = Report;
            }
            DataRow newIOUCash = IOUReport.NewRow();
            newIOUCash["IOU"] = "IOU'S Total";
            double valIOUCash = 0.0;
            foreach (DataColumn dc in IOUReport.Columns)
            {
                if (dc.DataType == typeof(Double))
                {
                    valIOUCash = 0.0;
                    double.TryParse(IOUReport.Compute("sum([" + dc.ToString() + "])", "[" + dc.ToString() + "]<>'0'").ToString(), out valIOUCash);
                    newIOUCash[dc.ToString()] = valIOUCash;
                }
            }
            lblIou.Text = valIOUCash.ToString();
            IOUReport.Rows.Add(newIOUCash);
            grdDue.DataSource = IOUReport;
            grdDue.DataBind();
            Session["IOUReport"] = IOUReport;
            double TotNetAmount = 0;
            DiffPanel.Visible = true;
            cmd = new SqlCommand("SELECT collections.Branchid, collections.AmountPaid, collections.Denominations, collections.EmpID, Newusers.UserName FROM collections INNER JOIN Newusers ON collections.EmpID = Newusers.UserId WHERE  (collections.Branchid = @BranchID) AND (collections.PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            DataTable dtClo = vdm.SelectQuery(cmd).Tables[0];
            if (dtClo.Rows.Count > 0)
            {
                panelGrid.Visible = true;
                PanelDen.Visible = false;
                DataTable dtDenom = new DataTable();
                dtDenom.Columns.Add("Cash");
                dtDenom.Columns.Add("Count");
                dtDenom.Columns.Add("Amount");
                string strDenomin = dtClo.Rows[0]["Denominations"].ToString();
                double denominationtotal = 0;
                foreach (string str in strDenomin.Split('+'))
                {
                    if (str.Trim() != "")
                    {
                        DataRow newDeno = dtDenom.NewRow();
                        string[] price = str.Split('x');
                        newDeno["Cash"] = price[0];
                        newDeno["Count"] = price[1];
                        float denamount = 0;

                        float.TryParse(price[0], out denamount);

                        float DencAmount = 0;
                        float.TryParse(price[1], out DencAmount);
                        newDeno["Amount"] = Convert.ToDecimal(denamount * DencAmount).ToString("#,##0.00");
                        denominationtotal += denamount * DencAmount;
                        dtDenom.Rows.Add(newDeno);
                    }
                }
                DataRow newDenoTotal = dtDenom.NewRow();
                newDenoTotal["Cash"] = "Total";
                newDenoTotal["Amount"] = denominationtotal;
                dtDenom.Rows.Add(newDenoTotal);
                string IOU = lblIou.Text;
                double DIOU = 0;
                double.TryParse(IOU, out DIOU);
                double TotalCash = 0;
                TotalCash = denominationtotal + DIOU;
                lblTotalAmout.Text = TotalCash.ToString();
                double Differnce = 0;
                Differnce = TotalCash - DebitCash;
                lblDiffernce.Text = Differnce.ToString();
                lblpreparedby.Text = dtClo.Rows[0]["UserName"].ToString();
                double Zerodiff = 0;
                Zerodiff = TotNetAmount - denominationtotal;
                Zerodiff = Math.Round(Zerodiff, 0);
                lblZeroDiffence.Text = Zerodiff.ToString();
                grdDenomination.DataSource = dtDenom;
                grdDenomination.DataBind();
                lblCash.Text = denominationtotal.ToString();
            }
            else
            {
                PanelDen.Visible = true;
                panelGrid.Visible = false;
                string IOU = lblIou.Text;
                double DIOU = 0;
                double.TryParse(IOU, out DIOU);
                double dif = 0;
                double totdif = 0;
                totdif = DIOU + dif;
                lblDiffernce.Text = totdif.ToString();

            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            vdm = new SalesDBManager();
            string totIOU = lblIou.Text;
            //string DenCash = Session["Cash"].ToString();
            double IOU = 0;
            double.TryParse(totIOU, out IOU);
            //double Cash = 0;
            //double.TryParse(DenCash, out Cash);
            // double TotalAmount = 0;
            double Totalclosing = 0;
            //TotalAmount = Cash + IOU;
            //double diffamount = 0;
            //double.TryParse(lblDiffernce.Text, out diffamount);
            double.TryParse(lblhidden.Text, out Totalclosing);

            DataTable dt = (DataTable)Session["IOUReport"];
            lblmsg.Text = "";
            DateTime fromdate = new DateTime();
            string[] datestrig = txtFromdate.Text.Split(' ');
            if (datestrig.Length > 1)
            {
                if (datestrig[0].Split('-').Length > 0)
                {
                    string[] dates = datestrig[0].Split('-');
                    string[] times = datestrig[1].Split(':');
                    fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
                }
            }
            fromdate = fromdate;
            DateTime ServerDateCurrentdate = SalesDBManager.GetTime(vdm.conn);

            string DenominationString = Session["DenominationString"].ToString();
            cmd = new SqlCommand("SELECT BranchID FROM  Collections WHERE (BranchId = @BranchId) AND (PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            DataTable dtCol = vdm.SelectQuery(cmd).Tables[0];
            if (dtCol.Rows.Count > 0)
            {
                lblmsg.Text = "Cash Book already closed";

            }
            else
            {
                /////////////////....................Ravindra.....................////////////////
                //cmd = new SqlCommand("SELECT cashpayables.Sno, cashpayables.BranchID, cashpayables.CashTo,subpayable.HeadSno, cashpayables.DOE, cashpayables.VocherID, cashpayables.Remarks, SUM(cashpayables.ApprovedAmount) AS Amount, accountheads.HeadName FROM cashpayables INNER JOIN subpayable ON cashpayables.Sno = subpayable.RefNo INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.Status='P') AND (cashpayables.VoucherType = 'Credit') GROUP BY accountheads.Sno ORDER BY accountheads.HeadName");
                cmd = new SqlCommand("SELECT cashpayables.Sno, cashpayables.BranchID, cashpayables.CashTo, subpayable.HeadSno, cashpayables.DOE, cashpayables.VocherID, cashpayables.Remarks, SUM(cashpayables.ApprovedAmount) AS Amount, accountheads.HeadName FROM cashpayables INNER JOIN subpayable ON cashpayables.Sno = subpayable.RefNo INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.Status = 'P') AND (cashpayables.VoucherType = 'Credit') GROUP BY cashpayables.Sno, cashpayables.BranchID, cashpayables.CashTo, subpayable.HeadSno, cashpayables.DOE, cashpayables.VocherID, cashpayables.Remarks,  cashpayables.Amount, accountheads.HeadName ORDER BY accountheads.HeadName");
                cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
                DataTable dtCredit = vdm.SelectQuery(cmd).Tables[0];
                /////////////////....................Ravindra.....................////////////////
                //cmd = new SqlCommand("SELECT cashpayables.onNameof,cashpayables.CashTo, SUM(cashpayables.ApprovedAmount) AS Amount, subpayable.HeadSno, accountheads.HeadName FROM cashpayables INNER JOIN subpayable ON cashpayables.Sno = subpayable.RefNo INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.VoucherType = 'Due') AND (cashpayables.Status<> 'C') AND (cashpayables.Status='P') GROUP BY  accountheads.HeadName ORDER BY accountheads.HeadName");
                cmd = new SqlCommand("SELECT cashpayables.onNameof, cashpayables.CashTo, SUM(cashpayables.ApprovedAmount) AS Amount, subpayable.HeadSno, accountheads.HeadName  FROM cashpayables INNER JOIN subpayable ON cashpayables.Sno = subpayable.RefNo INNER JOIN accountheads ON subpayable.HeadSno = accountheads.Sno WHERE (cashpayables.BranchID = @BranchID) AND (cashpayables.VoucherType = 'Due') AND (cashpayables.Status <> 'C') AND (cashpayables.Status = 'P') GROUP BY cashpayables.onNameof, cashpayables.CashTo, cashpayables.Amount, subpayable.HeadSno, accountheads.HeadName ORDER BY accountheads.HeadName");
                cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
                DataTable dtDebit = vdm.SelectQuery(cmd).Tables[0];
                foreach (DataRow dr in dtDebit.Rows)
                {
                    string IouName = dr["HeadName"].ToString();
                    double iouamtdebit = 0;
                    double iouamtcredit = 0;
                    double TotIouBal = 0;
                    double.TryParse(dr["Amount"].ToString(), out iouamtdebit);
                    foreach (DataRow drcredit in dtCredit.Select("HeadSno='" + dr["HeadSno"].ToString() + "'"))
                    {
                        double.TryParse(drcredit["Amount"].ToString(), out iouamtcredit);
                    }
                    TotIouBal = iouamtdebit - iouamtcredit;
                    if (TotIouBal == 0)
                    {
                    }
                    else
                    {
                        cmd = new SqlCommand("Insert into ioutable (BranchID,IOU,Amount,DOE) values(@BranchID,@IOU,@Amount,@DOE)");
                        cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
                        cmd.Parameters.Add("@Amount", TotIouBal);
                        cmd.Parameters.Add("@IOU", IouName);
                        cmd.Parameters.Add("@DOE", fromdate);
                        vdm.insert(cmd);
                    }
                }
                cmd = new SqlCommand("Insert into Collections (BranchID,AmountPaid,UserData_sno,PaidDate,PaymentType,Denominations,EmpID,VarifyDate) values(@BranchID,@AmountPaid,@UserData_sno,@PaidDate,@PaymentType,@Denominations,@EmpID,@VarifyDate)");
                cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
                //cmd.Parameters.Add("@AmountPaid", TotalAmount);
                cmd.Parameters.Add("@AmountPaid", Math.Round(Totalclosing, 2));
                cmd.Parameters.Add("@Denominations", DenominationString);
                cmd.Parameters.Add("@UserData_sno", "1");
                cmd.Parameters.Add("@PaidDate", fromdate);
                cmd.Parameters.Add("@VarifyDate", ServerDateCurrentdate);
                cmd.Parameters.Add("@PaymentType", "Cash");
                cmd.Parameters.Add("@EmpID", Session["UserSno"].ToString());
                vdm.insert(cmd);
                lblmsg.Text = "Cash Book saved successfully";
                GetReport();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdDue, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int index = grdDue.SelectedRow.RowIndex;
        string headsno = grdDue.SelectedRow.Cells[0].Text;
        string name = grdDue.SelectedRow.Cells[1].Text;
        string country = grdDue.SelectedRow.Cells[2].Text;
        DateTime fromdate = new DateTime();
        string[] datestrig = txtFromdate.Text.Split(' ');
        if (datestrig.Length > 1)
        {
            if (datestrig[0].Split('-').Length > 0)
            {
                string[] dates = datestrig[0].Split('-');
                string[] times = datestrig[1].Split(':');
                fromdate = new DateTime(int.Parse(dates[2]), int.Parse(dates[1]), int.Parse(dates[0]), int.Parse(times[0]), int.Parse(times[1]), 0);
            }
        }
        //string message = "Row Index: " + index + "\\nName: " + name + "\\nCountry: " + country;
        try
        {
            vdm = new SalesDBManager();
            DataTable Report = new DataTable();

            cmd = new SqlCommand("SELECT BranchID FROM  Collections WHERE (BranchId = @BranchId) AND (PaidDate BETWEEN @d1 AND @d2)");
            cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
            cmd.Parameters.Add("@d1", GetLowDate(fromdate));
            cmd.Parameters.Add("@d2", GetHighDate(fromdate));
            DataTable dtCol = vdm.SelectQuery(cmd).Tables[0];
            if (dtCol.Rows.Count > 0)
            {
                lblmsg.Text = "Please Select Details On Current Day Cash Book";

            }
            else
            {
                //cmd = new SqlCommand("SELECT DATE_FORMAT(cashpayables.DOE, '%d %b %y') AS EntryDate, cashpayables.VocherID, accountheads.HeadName,cashpayables.VoucherType, cashpayables.Amount, cashpayables.ApprovedAmount as ApprovedAmount FROM accountheads INNER JOIN subpayable ON accountheads.Sno = subpayable.HeadSno INNER JOIN cashpayables ON subpayable.RefNo = cashpayables.Sno WHERE (cashpayables.BranchID = @BranchID)  AND (subpayable.HeadSno = @HeadSno) and (cashpayables.Status=@Status)  ORDER BY cashpayables.DOE");
                cmd = new SqlCommand("SELECT DATE_FORMAT(cashpayables.DOE, '%d %b %y') AS EntryDate, cashpayables.VocherID, accountheads.HeadName, cashpayables.VoucherType,cashpayables.Amount, cashpayables.ApprovedAmount FROM accountheads INNER JOIN subpayable ON accountheads.Sno = subpayable.HeadSno INNER JOIN cashpayables ON subpayable.RefNo = cashpayables.Sno WHERE (subpayable.HeadSno = @HeadSno) AND (cashpayables.Status = @Status) AND (cashpayables.VoucherType<>'Debit')  ORDER BY cashpayables.DOE");
                cmd.Parameters.Add("@Status", 'P');
                //cmd.Parameters.Add("@BranchID", ddlSalesOffice.SelectedValue);
                cmd.Parameters.Add("@HeadSno", headsno);
                DataTable dtCredit = vdm.SelectQuery(cmd).Tables[0];
                Report.Columns.Add("Date");
                Report.Columns.Add("VoucherID");
                Report.Columns.Add("AccountName");
                Report.Columns.Add("IOUAmount").DataType = typeof(Double);
                Report.Columns.Add("CreditAmount").DataType = typeof(Double);
                double DueAmount = 0;
                double CreditAmount = 0;
                if (dtCredit.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCredit.Rows)
                    {
                        DataRow newrow = Report.NewRow();
                        newrow["Date"] = dr["EntryDate"].ToString();
                        newrow["VoucherID"] = dr["VocherID"].ToString();
                        newrow["AccountName"] = dr["HeadName"].ToString();
                        string VoucherType = dr["VoucherType"].ToString();
                        if (VoucherType == "Due")
                        {
                            double ReceivedAmount = 0;
                            double.TryParse(dr["Amount"].ToString(), out ReceivedAmount);
                            newrow["IOUAmount"] = ReceivedAmount;
                            DueAmount += ReceivedAmount;
                        }
                        if (VoucherType == "Credit")
                        {
                            double ApprovedAmount = 0;
                            double.TryParse(dr["ApprovedAmount"].ToString(), out ApprovedAmount);
                            newrow["CreditAmount"] = ApprovedAmount;
                            CreditAmount += ApprovedAmount;
                        }
                        Report.Rows.Add(newrow);
                    }
                }
                double Amount = 0;
                Amount = DueAmount - CreditAmount;
                //lblmsg.Text = "Due Amount: " + Amount.ToString();
                DataRow newrowbalance = Report.NewRow();
                newrowbalance["AccountName"] = "Due Amount: ";
                newrowbalance["IOUAmount"] = Amount;
                Report.Rows.Add(newrowbalance);
                GrdProducts.DataSource = Report;
                GrdProducts.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "PopupOpen();", true);
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
}