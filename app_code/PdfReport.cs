using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

/// <summary>
/// Summary description for PdfReport
/// </summary>
public class PdfReport
{

    private string _Imagepath;




    public int agentcount = 1;

    private string _reportTitle;

    //PDF REPORT 

    private string _companyName;
    private string _plantName;
    private DateTime _date1;
    private DateTime _date2;
    private string _session;

    private string _Routeid;
    private string _RouteName;
    private string _Agentid;
    private string _Cans;
    private string _MilkLtr;
    private string _MilkKg;
    private string _Fat;
    private string _Snf;
    private string _Clr;
    private string _Rate;
    private string _Amount;

    //TOT OBJECTS
    private string _Smltr;
    private string _Smkg;
    private string _Afat;
    private string _Asnf;
    private string _Arate;
    private string _Samt;
    private string _SCans;

    //PERIOD OBJECTS
    private int _PeriodObj;

    public PdfReport()
    {

        _Imagepath = string.Empty;

        //PDF REPORT 
        _companyName = string.Empty;
        _plantName = string.Empty;
        _Routeid = string.Empty;
        _RouteName = string.Empty;
        _Agentid = string.Empty;
        _Cans = string.Empty;
        _MilkLtr = string.Empty;
        _MilkKg = string.Empty;
        _Fat = string.Empty;
        _Snf = string.Empty;
        _Clr = string.Empty;
        _Rate = string.Empty;
        _Amount = string.Empty;
        _session = string.Empty;


        //TOT OBJECTS
        _Smltr = string.Empty;
        _Afat = string.Empty;
        _Asnf = string.Empty;
        _Arate = string.Empty;
        _Samt = string.Empty;
        _SCans = string.Empty;

        //PERIOD OBJECTS
        _PeriodObj = 0;
    }
    public string Routeid
    {
        get
        {
            return _Routeid;
        }
        set
        {
            this._Routeid = value;
        }
    }
    public string Session
    {
        get
        {
            return _session;
        }
        set
        {
            this._session = value;
        }

    }

    public string CompanyName
    {
        get
        {
            return _companyName;
        }
        set
        {
            this._companyName = value;
        }
    }
    public string PlantName
    {
        get
        {
            return _plantName;
        }
        set
        {
            this._plantName = value;
        }
    }
    public DateTime Date1
    {
        get
        {
            return _date1;
        }
        set
        {
            this._date1 = value;
        }
    }
    public DateTime Date2
    {
        get
        {
            return _date2;
        }
        set
        {
            this._date2 = value;
        }
    }
    public string Imagepath
    {
        get
        {
            return _Imagepath;
        }
        set
        {
            this._Imagepath = value;
        }
    }
    public int PeriodObject
    {
        get
        {
            return _PeriodObj;
        }
        set
        {
            this._PeriodObj = value;
        }
    }


    public PdfReport(DataSet ds, string name, PdfReport Cpdf)
    {
        this.ds = ds;
        this.name = name;

this._companyName = Cpdf.CompanyName;
        this._plantName = Cpdf.PlantName;
        this._date1 = Cpdf.Date1;
        this._date2 = Cpdf.Date2;
        this._Routeid = Cpdf.Routeid;
        this._session = Cpdf._session;
        this._Imagepath = Cpdf.Imagepath;

        this._PeriodObj = Cpdf.PeriodObject;

    }
    private readonly DataSet ds;
    private readonly string name;
    private readonly int agentno2;
    private int agentno1;


    public void Execute()
    {


        HttpResponse Response = HttpContext.Current.Response;
        Response.Clear();
            
        //Response.ContentType = "application/octet-stream";
        // Response.AddHeader("Content-Disposition", "inline; filename=" + name + ".pdf");
        Response.AddHeader("Content-Disposition", "attachment; filename=" + name + ".pdf");
       // Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "text/plain";  
        // step 1: creation of a document-object       
        Document document = new Document(PageSize.A4, 45f, 45f, 60f, 60f);
        using (FileStream fs = new FileStream(name + ".pdf", FileMode.Create))
        {
            // step 2: we create a writer that listens to the document           
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddTitle(name);
            document.AddSubject("Table of " + name);
            // step 3: we open the document
            document.Open();
            // step 4: we add content to the document
            for (int i = 1; i <= agentcount; i++)
            {
                agentno1 = i;
                CreatePages(document, writer);
            }
            // step 5: we close the document
            document.Close();
           
            ///////////////
            //pasword
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(name + ".pdf".ToString());
            string MFileName = string.Empty;
            object TargetFile = name + ".pdf".ToString();
            MFileName = TargetFile.ToString();
            string ModifiedFileName = string.Empty;
            ModifiedFileName = TargetFile.ToString();
            MFileName = MFileName.Insert(ModifiedFileName.Length - 4, "k");
            string pw = "pw";
             string opw = "opw";
            //iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(MFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, pw, "", iTextSharp.text.pdf.PdfWriter.ALLOW_PRINTING);
            iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(MFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, pw, opw, iTextSharp.text.pdf.PdfWriter.ALLOW_PRINTING);
            if (File.Exists(TargetFile.ToString()))
            {
                File.Delete(TargetFile.ToString());
            }
            
            ///////////////


            //.
            System.IO.FileInfo file = new System.IO.FileInfo(MFileName);
            if (File.Exists(MFileName.ToString()))
            {
                //
                FileStream sourceFile = new FileStream(file.FullName, FileMode.Open);
                float FileSize;
                FileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)FileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                //
                Response.ClearContent(); // neded to clear previous (if any) written content
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.BinaryWrite(getContent);
                File.Delete(file.FullName.ToString());
                Response.Flush();
                Response.End();
            }
            {

                Response.Write("File Not Found...");
            }
            //.



        }


    }

    public void Execute1()
    {


       
        // step 1: creation of a document-object       
        Document document = new Document(PageSize.A4, 45f, 45f, 60f, 60f);
        using (FileStream fs = new FileStream(name + ".pdf", FileMode.Create))
        {
            // step 2: we create a writer that listens to the document           
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddTitle(name);
            document.AddSubject("Table of " + name);
            // step 3: we open the document
            document.Open();
            // step 4: we add content to the document
            for (int i = 1; i <= agentcount; i++)
            {
                agentno1 = i;
                CreatePages(document, writer);
            }
            // step 5: we close the document
            document.Close();
        }


    }

    private void CreatePages(Document document, PdfWriter writer)
    {


        //_BillRow1 = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["BillRow"].ToString());
        bool first = true;
        foreach (DataTable table in ds.Tables)
        {
            if (first)
            {

                //if (agentno1 % 2 == 0)
                //{

                //    first = false;
                //}
                //else
                //{

                //    document.NewPage();

                //}

                document.NewPage();
            }
            else
            {
                document.NewPage();

            }

            _reportTitle = string.Empty;



            _reportTitle = "Summary report for agentwise Milk Procurement";




            ///LOGO
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(_Imagepath);
            logo.Alignment = Element.ALIGN_LEFT;
            logo.ScaleToFit(40, 40);
            document.Add(logo);
            ///

            PdfPTable compname = new PdfPTable(1);
            PdfPCell cells = new PdfPCell();
            cells.BorderWidth = 0;
            compname.DefaultCell.Padding = 2;
            compname.WidthPercentage = 55;
            compname.DefaultCell.BorderWidth = 0;
            compname.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            compname.AddCell(cells);
            compname.AddCell(FormatHeaderPhrase(_companyName.ToString()));

            PdfPTable compname1 = new PdfPTable(1);
            PdfPCell cells2 = new PdfPCell();
            cells2.BorderWidth = 0;
            compname1.DefaultCell.Padding = 2;
            compname1.WidthPercentage = 55;
            compname1.DefaultCell.BorderWidth = 0;
            compname1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            compname1.AddCell(cells2);
            compname1.AddCell(FormatHeaderPhrase(_plantName.ToString() + "_ 111"));
            //compname1.AddCell(FormatHeaderPhrase(" " + " " + "AgentId :" + _agentid.ToString() + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " +  _centreName + " " + " " + " " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "Date :" + _date1));


            PdfPTable compname2 = new PdfPTable(1);
            PdfPCell cells3 = new PdfPCell();
            cells3.BorderWidth = 0;
            compname2.DefaultCell.Padding = 2;
            compname2.WidthPercentage = 85;
            compname2.DefaultCell.BorderWidth = 0;
            compname2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            compname2.AddCell(cells3);
            compname2.AddCell(FormatHeaderPhrase(_reportTitle.ToString()));
            //compname2.AddCell(FormatHeaderPhrase(" " + " " + "AgentName :" + _agentName.ToString() + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + _reportTitle + " " + " " + " " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + " Session :" + "AM"));


            PdfPTable compname3 = new PdfPTable(1);
            PdfPCell cells4 = new PdfPCell();
            cells4.BorderWidth = 0;
            compname3.DefaultCell.Padding = 2;
            compname3.WidthPercentage = 85;
            compname3.DefaultCell.BorderWidth = 0;
            compname3.AddCell(cells4);
            compname3.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            if (PeriodObject == 0)
            {
                compname3.AddCell(FormatHeaderPhrase(" " + " " + "RouteId :" + _Routeid.ToString() + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "From_Date :" + _date1.ToShortDateString() + " " + " " + " " + " " + "Session :" + _session));
            }
            else
            {
                compname3.AddCell(FormatHeaderPhrase(" " + " " + "RouteId :" + _Routeid.ToString() + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "From_Date :" + _date1.ToShortDateString() + " " + " " + " " + "To_Date :" + _date2.ToShortDateString()));
            }


            //document.Add(CLogotable);
            document.Add(compname);
            document.Add(compname1);
            document.Add(compname2);
            document.Add(compname3);



            // PdfPTable pdfTable = new PdfPTable(table.Columns.Count);
            PdfPTable pdfTable = new PdfPTable(8);
            pdfTable.DefaultCell.Padding = 2;
            pdfTable.WidthPercentage = 85; // percentage            
            pdfTable.DefaultCell.BorderWidth = 1;
            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            int coll = 1;
            foreach (DataColumn column in table.Columns)
            {
                //SPECIFIED COLUMN NAME 
                if (coll >= 9)
                {
                    int col1 = 0;

                }
                else
                {
                    //DEFAULT COLUMN NAME
                    pdfTable.AddCell(column.ColumnName);
                }
                coll++;


            }
            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.HeaderRows = 1;  // this is the end of the table header

            pdfTable.DefaultCell.BorderWidth = 1;
            ///////////////////////////////////////// 



            foreach (DataRow row in table.Rows)
            {


                _Agentid = row[0].ToString().Trim() + '-' + row[8].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_Agentid.ToString().Trim()));
                _Cans = row[1].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_Cans.ToString().Trim()));
                _MilkKg = row[2].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_MilkKg.ToString().Trim()));
                _MilkLtr = row[3].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_MilkLtr.ToString().Trim()));
                _Fat = row[4].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_Fat.ToString().Trim()));
                _Snf = row[5].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_Snf.ToString().Trim()));
                _Rate = row[6].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_Rate.ToString().Trim()));
                _Amount = row[7].ToString().Trim();
                pdfTable.AddCell(FormatPhrase(_Amount.ToString().Trim()));

                ////

            }
            document.Add(pdfTable);

            //////

            #region SUM

           
                foreach (DataTable dt in ds.Tables)
                {

                    int i = 0;
                    foreach (DataRow row1 in dt.Rows)
                    {
                        // object tot;
                        if (i == 0)
                        {
                            _Smkg = row1[12].ToString().Trim();
                            _Smltr = row1[13].ToString().Trim();
                            _Afat = row1[14].ToString().Trim();
                            _Asnf = row1[15].ToString().Trim();
                            _SCans = row1[17].ToString().Trim();
                            _Arate = row1[18].ToString().Trim();
                            _Samt = row1[19].ToString().Trim();

                            PdfPTable Footertable = new PdfPTable(8);

                            Footertable.DefaultCell.Padding = 4;
                            Footertable.WidthPercentage = 85; // percentage                                
                            Footertable.DefaultCell.BorderWidth = 0;
                            Footertable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;                            
                            Footertable.AddCell(FormatHeaderPhrase("SUM :"));
                            Footertable.AddCell(FormatHeaderPhrase(_SCans.ToString()));
                            Footertable.AddCell(FormatHeaderPhrase(_Smkg.ToString()));
                            Footertable.AddCell(FormatHeaderPhrase(_Smltr.ToString()));
                            Footertable.AddCell(FormatSpace(_Afat.ToString()));
                            Footertable.AddCell(FormatSpace(_Asnf.ToString()));
                            Footertable.AddCell(FormatSpace(_Arate.ToString()));
                            Footertable.AddCell(FormatHeaderPhrase(_Samt.ToString()));
                            Footertable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;                            
                            document.Add(Footertable);
                            ////
                            PdfPTable Footertable1 = new PdfPTable(8);

                            Footertable1.DefaultCell.Padding = 4;
                            Footertable1.WidthPercentage = 85; // percentage                                
                            Footertable1.DefaultCell.BorderWidth = 0;
                            Footertable1.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            Footertable1.AddCell(FormatHeaderPhrase("AVG :"));
                            Footertable1.AddCell(FormatSpace(_SCans.ToString()));
                            Footertable1.AddCell(FormatSpace(_Smkg.ToString()));
                            Footertable1.AddCell(FormatSpace(_Smltr.ToString()));
                            Footertable1.AddCell(FormatHeaderPhrase(_Afat.ToString()));
                            Footertable1.AddCell(FormatHeaderPhrase(_Asnf.ToString()));
                            Footertable1.AddCell(FormatHeaderPhrase(_Arate.ToString()));
                            Footertable1.AddCell(FormatSpace(_Samt.ToString()));
                            Footertable1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            document.Add(Footertable1);
                            ////
                            i++;
                        }
                    }
                }            
               
            #endregion


        }
    }

    private static Phrase FormatSpace(string value)
    {
        return new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE));
    }
    private static Phrase FormatPhrase(string value)
    {
        return new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 8));
    }

    private static Phrase FormatHeaderPhrase(string value)
    {
        return new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK));
    }
}
