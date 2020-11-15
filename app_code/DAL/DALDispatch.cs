using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


public class DALDispatch
{

    int i = 0;
    DbHelper DBaccess = new DbHelper();
    SqlConnection con = new SqlConnection();
    DbHelper dbaccess = new DbHelper();
    public DALDispatch()
    {

    }
    public void InsertDispatch(BOLDispatch DispatchBOL, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter parfromplant, partopalant, pardate, parsession, parMilkkg, parfat, parsnf, parclr, parrate, paramount, parcompanycode, parplantcode, partype, parfatkg, parsnfkg, partankerno, ackmkg, ackfat, acksnf, ackclr, ackrate, ackamount, diffkg, diffat, diffsnf, diffclr, diffrate, diffamount, Status,tcnumber;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            parfatkg = cmd.Parameters.Add("@Fat_Kg", SqlDbType.Float);
            parsnfkg = cmd.Parameters.Add("@Snf_Kg", SqlDbType.Float);
            parfromplant = cmd.Parameters.Add("@Plant_From", SqlDbType.NVarChar, 50);
            partopalant = cmd.Parameters.Add("@Plant_To", SqlDbType.NVarChar, 50);
            pardate = cmd.Parameters.Add("@Date", SqlDbType.DateTime);
            paramount = cmd.Parameters.Add("@Amount", SqlDbType.Float);
            parsession = cmd.Parameters.Add("@Session", SqlDbType.NVarChar, 20);
            parplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.NVarChar, 50);
            parcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            partype = cmd.Parameters.Add("@type", SqlDbType.NVarChar, 50);
            parMilkkg = cmd.Parameters.Add("@MilkKg", SqlDbType.Float);
            parfat = cmd.Parameters.Add("@Fat", SqlDbType.Float);
            parsnf = cmd.Parameters.Add("@Snf", SqlDbType.Float);
            parclr = cmd.Parameters.Add("@clr", SqlDbType.Float);
            parrate = cmd.Parameters.Add("@Rate", SqlDbType.Float);
            partankerno = cmd.Parameters.Add("@Tanker_No", SqlDbType.NVarChar, 50);
         
            ackmkg = cmd.Parameters.Add("@Ack_milkkg", SqlDbType.Float);
            ackfat = cmd.Parameters.Add("@Ack_fat", SqlDbType.Float);
            acksnf = cmd.Parameters.Add("@Ack_snf", SqlDbType.Float);
            ackclr = cmd.Parameters.Add("@Ack_clr", SqlDbType.Float);
            ackrate = cmd.Parameters.Add("@Ack_rate", SqlDbType.Float);
            ackamount = cmd.Parameters.Add("@Ack_amount", SqlDbType.Float);
            diffkg = cmd.Parameters.Add("@diff_kg", SqlDbType.Float);
            diffat = cmd.Parameters.Add("@diff_fat", SqlDbType.Float);
            diffsnf = cmd.Parameters.Add("@diff_snf", SqlDbType.Float);
            diffclr = cmd.Parameters.Add("@diff_clr", SqlDbType.Float);
            diffrate = cmd.Parameters.Add("@diff_rate", SqlDbType.Float);
            diffamount = cmd.Parameters.Add("@diff_amount", SqlDbType.Float);
            Status = cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
         //   partankerno = cmd.Parameters.Add("@Tanker_No", SqlDbType.NVarChar, 50);
            tcnumber = cmd.Parameters.Add("@tcnumber", SqlDbType.NVarChar, 50);
            parfatkg.Value = DispatchBOL.FATKG;
            parsnfkg.Value = DispatchBOL.SNFKG;
            parfromplant.Value = DispatchBOL.From_Plant;
            partopalant.Value = DispatchBOL.To_Plant;
            pardate.Value = DispatchBOL.FromDate;
            parsession.Value = DispatchBOL.Session;
            parplantcode.Value = DispatchBOL.Plantcode;
            parcompanycode.Value = Convert.ToInt32(DispatchBOL.Companycode);
            parMilkkg.Value = DispatchBOL.MilkKg;
            partype.Value = DispatchBOL.TYPE;
            parfat.Value = DispatchBOL.Fat;
            parsnf.Value = DispatchBOL.Snf;
            parclr.Value = DispatchBOL.Clr;
            parrate.Value = DispatchBOL.Rate;
            paramount.Value = DispatchBOL.Amount;
            partankerno.Value = DispatchBOL.TANKARNO;
            tcnumber.Value = DispatchBOL.TCNUMBER;

            ackmkg.Value = "0";
            ackfat.Value = "0";
            acksnf.Value = "0";
            ackclr.Value = "0";
            ackrate.Value = "0";
            ackamount.Value = "0";
            diffkg.Value = "0";
            diffat.Value = "0";
            diffsnf.Value = "0";
            diffclr.Value = "0";
            diffrate.Value = "0";
            diffamount.Value = "0";
            Status.Value = "Pending";

            cmd.ExecuteNonQuery();
            //WebMsgBox.Show("Inserted SucessFully");
            //WebMsgBox.Show("Inserted");
        }

    }


    public void InsertStock(BOLDispatch DispatchBOL, string sql)
    {

        using (con = DBaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter  pardate,  parMilkkg, parfat, parsnf, parclr, parrate, paramount, parcompanycode, parplantcode,parStorageName,parplantname,parfatkg,parsnfkg;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            parfatkg = cmd.Parameters.Add("@Fat_Kg", SqlDbType.Float);
            parsnfkg = cmd.Parameters.Add("@Snf_Kg", SqlDbType.Float);
            parplantname = cmd.Parameters.Add("@Plant_Name", SqlDbType.NVarChar, 50);
            parStorageName = cmd.Parameters.Add("@Storage_Name", SqlDbType.NVarChar, 50);
            pardate = cmd.Parameters.Add("@Date", SqlDbType.DateTime);
            paramount = cmd.Parameters.Add("@Amount", SqlDbType.Float);
            
            parplantcode = cmd.Parameters.Add("@Plant_Code", SqlDbType.NVarChar, 50);
            parcompanycode = cmd.Parameters.Add("@Company_Code", SqlDbType.Int);
            
            parMilkkg = cmd.Parameters.Add("@MilkKg", SqlDbType.Float);
            parfat = cmd.Parameters.Add("@Fat", SqlDbType.Float);
            parsnf = cmd.Parameters.Add("@Snf", SqlDbType.Float);
            parclr = cmd.Parameters.Add("@clr", SqlDbType.Float);
            parrate = cmd.Parameters.Add("@Rate", SqlDbType.Float);


            parfatkg.Value = DispatchBOL.FATKG;
            parsnfkg.Value = DispatchBOL.SNFKG;
            parStorageName.Value = DispatchBOL.STORAGENAME;
            parplantname.Value = DispatchBOL.PlantName;
            pardate.Value = DispatchBOL.FromDate;
            parplantcode.Value = DispatchBOL.Plantcode;
            parcompanycode.Value = Convert.ToInt32(DispatchBOL.Companycode);
            parMilkkg.Value = DispatchBOL.MilkKg;
            
            parfat.Value = DispatchBOL.Fat;
            parsnf.Value = DispatchBOL.Snf;
            parclr.Value = DispatchBOL.Clr;
            parrate.Value = DispatchBOL.Rate;
            paramount.Value = DispatchBOL.Amount;
            cmd.ExecuteNonQuery();
            //WebMsgBox.Show("Inserted");
        }

    }


    public void DespatchDeleteRow(BOLDispatch dispatBOL, string sql)
    {
        using (con = dbaccess.GetConnection())
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter ptid, pflag, pcompanycode, pplantcode;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            cmd.Connection = con;
            int inser;

            ptid = cmd.Parameters.Add("@tid", SqlDbType.Int);      
            pflag = cmd.Parameters.Add("@Flag", SqlDbType.Int);

            pcompanycode = cmd.Parameters.Add("@companycode", SqlDbType.NVarChar, 30);
            pplantcode = cmd.Parameters.Add("@plantcode", SqlDbType.NVarChar, 30);

            ptid.Value = dispatBOL.Tid;           
            pflag.Value = dispatBOL.Flag;
            pplantcode.Value = dispatBOL.Plantcode;
            pcompanycode.Value = dispatBOL.Companycode;


            inser = cmd.ExecuteNonQuery();


            if (inser == 1)
            {
                i++;
                // WebMsgBox.Show("RateChart Delete Successfully...");

            }
            else
            {

                i++;

            }


        }

    }





  
}