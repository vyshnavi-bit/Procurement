using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BLLProcureimport
/// </summary>
public class BLLProcureimport
{
    string Sqlstr = string.Empty;
    DALProcureimport proimDA = new DALProcureimport();
    BOLProcurement procBO = new BOLProcurement();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    DbHelper dbaccess = new DbHelper();
    SqlDataReader dar;
	public BLLProcureimport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable LoadRatechartGriddata(string pcode, string ccode,string sess,string date)
    {
        DataTable dt = new DataTable();
        //Sqlstr = "SELECT Tid,From_Rangevalue,To_Rangevalue,Rate,Commission_Amount,Bonus_Amount FROM Rate_Chart WHERE Chart_Name='" + ratechart + "'";
        Sqlstr = "select Tid,Agent_id,Milk_kg,Fat,Snf,Remark from Procurementimport where Prdate='" + date + "' and Company_Code='" + ccode + "' and Sessions='" + sess + "' and Plant_Code='" + pcode + "' and Remarkstatus='1' ";
        dt = dbaccess.GetDatatable(Sqlstr);
        return dt;
    }
    public void proimport(BOLProcurement procurementBO)
    {
        string sql = "Edit_Procureimport";
        proimDA.Procurementimport(procurementBO, sql);
    }
    public DataSet LoadProocurementRemarksDatas(string ccode,string pcode, string dat, string ses,int flag)
    {
        string sqlstr = string.Empty;
         // sqlstr = "SELECT SampleId,agent_Id,milk_Kg,fat,Snf,agent_Id AS magent_Id,milk_Kg AS mmilk_Kg,fat AS mfat,Snf AS mSnf from Procurement  where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' ORDER BY SampleId ";
        //sqlstr = "SELECT sap_sampleno AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurement WHERE sap_plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Session='" + ses + "' AND remark_status=1  ORDER BY sap_sampleno ";
        if (flag == 1)
        {
            if (pcode == "157" || pcode == "165" || pcode == "166" || pcode == "167" || pcode == "168" || pcode == "169")
            {
                //sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1 AND (DIFFFAT BETWEEN '0' AND '0.1' OR DIFFSNF BETWEEN '0.01' AND '0.1')  ORDER BY SampleId ";
                sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG>=0.01 OR DIFFFAT>=0.01 OR DIFFSNF>=0.01)  ORDER BY SampleId ";
                // sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1 AND DIFFFAT > 0 AND (DIFFKG<=0.1 OR DIFFFAT<=0.1 OR DIFFSNF<=0.1) ORDER BY SampleId ";
                //
            }
            else
            {
                sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG>=0.01 OR DIFFFAT>=0.01 OR DIFFSNF>=0.01)  ORDER BY SampleId ";
            }
        }
        else
        {
            sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG<=-0.01 OR DIFFFAT<=-0.01 OR DIFFSNF<=-0.01)  ORDER BY SampleId ";
        }
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }


    public DataSet LoadProocurementRemarksDataCCWISEAPPROVEs(string ccode, string pcode, string dat, string ses, int flag)
    {
        string sqlstr = string.Empty;
        // sqlstr = "SELECT SampleId,agent_Id,milk_Kg,fat,Snf,agent_Id AS magent_Id,milk_Kg AS mmilk_Kg,fat AS mfat,Snf AS mSnf from Procurement  where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' ORDER BY SampleId ";
        //sqlstr = "SELECT sap_sampleno AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurement WHERE sap_plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Session='" + ses + "' AND remark_status=1  ORDER BY sap_sampleno ";
        if (flag == 1)
        {
            if (pcode == "157" || pcode == "165" || pcode == "166" || pcode == "167" || pcode == "168" || pcode == "169")
            {
                sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG>=0.01 OR DIFFFAT>=0.01 OR DIFFSNF>=0.01)  ORDER BY SampleId ";
            }
            else
            {
                sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG>=0.01 OR DIFFFAT>=0.01 OR DIFFSNF>=0.01)  ORDER BY SampleId ";
            }
        }
        else
        {
            sqlstr = "SELECT 0 + ROW_NUMBER() OVER (ORDER BY SampleId)  AS serial_no, SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG<=-0.01 OR DIFFFAT<=-0.01 OR DIFFSNF<=-0.01)  ORDER BY SampleId ";
        }
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }


    public DataSet LoadBankPaymentDatas(string ccode, string pcode, string dt1, string dt2,string rid)
    {
        string sqlstr = string.Empty;
       // sqlstr = "SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F ORDER BY F.Rid,F.proAid ";
      //sqlstr = "SELECT * FROM(Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp RIGHT JOIN  (SELECT * FROM(SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F )AS F1) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL   ORDER BY F2.proAid  ";
        sqlstr = "SELECT * FROM(Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp RIGHT JOIN  (SELECT * FROM(SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,CONVERT(NVARCHAR(15),(CONVERT(Nvarchar(10),F.proAid)+'_'+F.Agent_Name)) AS Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F )AS F1) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL   ORDER BY F2.proAid  ";
        ds = dbaccess.GetDataset(sqlstr);
        return ds;
    }
    public DataTable LoadBankPaymentDatas1(string ccode, string pcode, string dt1, string dt2, string rid)
    {
        string sqlstr = string.Empty;
        sqlstr = "SELECT * FROM(Select Agent_id from BankPaymentllotment WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND billfrmdate='" + dt1 + "' AND billtodate='" + dt2 + "') AS bp RIGHT JOIN  (SELECT * FROM(SELECT F.proAid,FLOOR((ISNULL(F.SAmt,0)+ISNULL(F.ScommAmt,0)+ISNULL(F.AvRate,0)+(ISNULL(F.Smltr,0)*ISNULL(F.CarAmt,0)))-(ISNULL(F.Billadv,0)+ISNULL(F.Ai,0)+ISNULL(F.Feed,0)+ISNULL(F.instamt,0))) AS Netpay,ISNULL(F.ScommAmt,0) AS ScommAmt,ISNULL(F.Smltr,0) AS Smltr,ISNULL(F.Smkg,0) AS Smkg,ISNULL(F.AvRate,0) AS AvRate,ISNULL(F.SAmt,0) AS SAmt,ISNULL(F.ACRate,0) AS ACRate,ISNULL(F.Billadv,0) AS Billadv,ISNULL(F.Ai,0) AS Ai,ISNULL(F.Feed,0) AS Feed,ISNULL(F.can,0) AS can,ISNULL(F.Recovery,0) AS Recovery,ISNULL(F.others,0) AS others,ISNULL(F.instamt,0) AS instamt,ISNULL(F.Status,0) AS Status,ISNULL(F.CarAmt,0) AS CarAmt,F.Agent_Name,F.Bank_Id,F.Payment_mode,F.Agent_AccountNo,F.Rid,F.Route_Name FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT proAid,CAST(ScommAmt AS DECIMAL(18,2)) AS ScommAmt,CAST(Smltr AS DECIMAL(18,2)) AS Smltr,CAST(Smkg AS DECIMAL(18,2)) AS Smkg,CAST(AvgFat AS DECIMAL(18,2)) AS Afat,CAST(AvgSnf AS DECIMAL(18,2)) AS Asnf,CAST(AvgRate AS DECIMAL(18,2)) AS AvRate,CAST(Avgclr AS DECIMAL(18,2)) AS Aclr,CAST(Scans AS DECIMAL(18,2))AS Scans,CAST(SAmt AS DECIMAL(18,2)) AS SAmt,CAST(Avgcrate AS DECIMAL(18,2)) AS ACRate,CAST(Sfatkg AS DECIMAL(18,2)) AS Sfatkg,CAST(SSnfkg AS DECIMAL(18,2)) AS SSnfkg  FROM (SELECT agent_id AS SproAid FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "' GROUP BY agent_id ) AS Spro  LEFT JOIN (SELECT agent_id AS proAid,SUM(Comrate) AS ScommAmt,SUM(Milk_ltr) AS Smltr,SUM(Milk_kg) AS Smkg,AVG(FAT) AS AvgFat,AVG(SNF) AS AvgSnf,SUM(SplBonusAmount) AS AvgRate,AVG(Clr) AS Avgclr,SUM(NoofCans) AS Scans,SUM(Amount) AS SAmt,AVG(ComRate) AS Avgcrate,SUM(fat_kg) AS Sfatkg,SUM(snf_kg) AS SSnfkg  FROM Procurement WHERE PRDATE BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Plant_Code='" + pcode + "'  AND Company_Code='" + ccode + "' AND Route_Id='" + rid + "'  GROUP BY agent_id ) AS pro ON  Spro.SproAid=pro.proAid ) AS protot  LEFT JOIN (SELECT  Agent_id AS DAid ,(CAST((Billadvance) AS DECIMAL(18,2))) AS Billadv,(CAST((Ai) AS DECIMAL(18,2))) AS Ai,(CAST((Feed) AS DECIMAL(18,2))) AS Feed,(CAST((can) AS DECIMAL(18,2))) AS can,(CAST((Recovery) AS DECIMAL(18,2))) AS Recovery,(CAST((others) AS DECIMAL(18,2))) AS others FROM Deduction_Details WHERE deductiondate BETWEEN '" + dt1 + "' AND '" + dt2 + "' AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' ) AS Dedu ON protot.proAid=Dedu.DAid )AS prodedu LEFT JOIN (SELECT Agent_id AS LoAid,CAST(SUM(inst_amount) AS DECIMAL(18,2)) AS instamt,SUM(Agent_id) AS Status FROM LoanDetails WHERE Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Balance>0 Group by Agent_id ) AS Loan ON prodedu.proAid= Loan.LoAid) AS prodedulon  INNER JOIN  (SELECT Agent_Id AS cartAid,(CAST((Cartage_Amt) AS DECIMAL(18,2)))AS CarAmt,Agent_Name,Ifsc_code AS Bank_Id,Payment_mode,Agent_AccountNo,Route_id FROM  Agent_Master WHERE Type=0 AND Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Payment_mode='BANK') AS cart ON prodedulon.proAid=cart.cartAid ) AS FF   LEFT JOIN   (SELECT Route_id AS Rid,Route_Name FROM Route_Master WHERE Company_Code='" + ccode + "'AND Plant_Code='" + pcode + "')AS Rout ON FF.Route_id=Rout.Rid) AS F )AS F1) AS F2 ON bp.Agent_id=F2.proAid WHERE bp.agent_id is NULL   ORDER BY F2.proAid   ";
        dt = dbaccess.GetDatatable(sqlstr);
        return dt;
    }
    public DataTable LoadProocurementRemarksDatas1(string ccode, string pcode, string dat, string ses,int flag)
    {
        string sqlstr = string.Empty;
        // sqlstr = "SELECT SampleId,agent_Id,milk_Kg,fat,Snf,agent_Id AS magent_Id,milk_Kg AS mmilk_Kg,fat AS mfat,Snf AS mSnf from Procurement  where Company_Code='" + ccode + "' AND Plant_Code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' ORDER BY SampleId ";
       // sqlstr = "SELECT sap_sampleno AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurement WHERE sap_plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Session='" + ses + "' AND remark_status=1  ORDER BY sap_sampleno ";
        if (flag == 1)
        {
            sqlstr = "SELECT  SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG>=0.01 OR DIFFFAT>=0.01 OR DIFFSNF>=0.01)  ORDER BY SampleId ";
        }
        else
        {
            sqlstr = "SELECT SampleId AS SampleId,agent_Id,milk_Kg,fat,Snf,modify_aid AS magent_Id,modify_Kg AS mmilk_Kg,modify_fat AS mfat,modify_snf AS mSnf from Procurementimport WHERE plant_code='" + pcode + "' AND Prdate='" + dat + "' AND Sessions='" + ses + "' AND remarkstatus=1  AND (DIFFKG<=-0.01 OR DIFFFAT<=-0.01 OR DIFFSNF<=-0.01)  ORDER BY SampleId ";
        }
        dt = dbaccess.GetDatatable(sqlstr);
        return dt;
    }
}