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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Globalization;

/// <summary>
/// Summary description for CreatePdf
/// </summary>
public class CreatePdf
{
    private string _companyName;
    private string _plantName;
    private string _Imagepath;

    public string _routeid;

    public int _agentid;
    public double _Smltr;
    public double _Afat;
    public double _Asnf;
    public double _Arate;
    public double _Samt;
    public double _Feed;
    public double _Ai;
    public double _CartageAmt;

    public double _TotAdd;
    public double _TotDedu;
    public double _Canadv;
    public double _Others;
    public double _Netamt;


    public string _Smltr1;
    public string _Afat1;
    public string _Asnf1;
    public string _Arate1;
    public string _Samt1;
    public string _Feed1;
    public string _Ai1;
    public string _CartageAmt1;
    public string _TotAdd1;
    public string _TotDedu1;
    public string _Netamt1;


    public int _BillRow1;

    /// <summary>
    /// //
    public DateTime _prdate;
    public string _session;
    public string _milkltr;
    public string _fat;
    public string _snf;
    public string _rate;
    public string _amount;
    /// //
    /// </summary>

    private string _agentName;
    private string _reportTitle;
    private DateTime _date1;
    private DateTime _date2;
    public int countid = 0;
    public int countid1 = 0;
    public int agentcount = 1;


    public CreatePdf()
    {
        _companyName = string.Empty;
        _plantName = string.Empty;
        _Imagepath = string.Empty;
        _agentid = 0;
        _agentName = string.Empty;
        _reportTitle = string.Empty;


        _routeid = string.Empty;


        _Smltr = 0.0;
        _Afat = 0.0;
        _Asnf = 0.0;
        _Arate = 0.0;
        _Samt = 0.0;
        _Feed = 0.0;
        _Ai = 0.0;
        _CartageAmt = 0.0;


        _TotAdd = 0.0;
        _TotDedu = 0.0;
        _Canadv = 0.0;
        _Others = 0.0;
        _Netamt = 0.0;

        _Smltr1 = string.Empty;
        _Afat1 = string.Empty;
        _Asnf1 = string.Empty;
        _Arate1 = string.Empty;
        _Samt1 = string.Empty;
        _Feed1 = string.Empty;
        _Ai1 = string.Empty;
        _CartageAmt1 = string.Empty;
        _TotAdd1 = string.Empty;
        _TotDedu1 = string.Empty;
        _Netamt1 = string.Empty;


        _BillRow1 = 0;
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
    public string Routeid
    {
        get
        {
            return _routeid;
        }
        set
        {
            this._routeid = value;
        }
    }


    public CreatePdf(DataSet ds, string name, int agentno, DataSet ds1, CreatePdf Cpdf)
    {
        this.ds = ds;
        this.name = name;
        this.agentno2 = agentno;
        this.ds1 = ds1;
        this._companyName = Cpdf.CompanyName;
        this._plantName = Cpdf.PlantName;
        this._date1 = Cpdf.Date1;
        this._date2 = Cpdf.Date2;
        this._Imagepath = Cpdf.Imagepath;
        this._routeid = Cpdf.Routeid;

    }
    private readonly DataSet ds;
    private readonly string name;
    private readonly int agentno2;
    private int agentno1;
    private readonly DataSet ds1;



    public void Execute()
    {


        HttpResponse Response = HttpContext.Current.Response;
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        // Response.AddHeader("Content-Disposition", "inline; filename=" + name + ".pdf");
        Response.AddHeader("Content-Disposition", "attachment; filename=" + name + ".pdf");
        // step 1: creation of a document-object       
        Document document = new Document(PageSize.A4, 45f, 45f, 60f, 60f);
        using (FileStream fs = new FileStream(name + ".pdf", FileMode.Create))
        {
            // step 2: we create a writer that listens to the document
            // PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //set some header stuff
            document.AddTitle(name);
            document.AddSubject("Table of " + name);
            // document.AddCreator("This Application");
            //document.AddAuthor("Me");

            // we Add a Header that will show up on PAGE 1
            //Phrase phr = new Phrase(""); //empty phrase for page numbering
            //HeaderFooter footer = new HeaderFooter(phr, true);
            //document.Footer = footer;

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
            ///IMAGE

            //using (Stream inputPdfStream = new FileStream("input.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (Stream inputImageStream = new FileStream("some_image.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (Stream outputPdfStream = new FileStream("result.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    var reader1 = new PdfReader(inputPdfStream);
            //    var stamper = new PdfStamper(reader1, outputPdfStream);
            //    var pdfContentByte = stamper.GetOverContent(1);

            // //Image image = Image.GetInstance(inputImageStream);
            //    //image.SetAbsolutePosition(100, 100);
            //    //pdfContentByte.AddImage(image);
            //    var image=_Imagepath;
            //    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);

            //    stamper.Close();
            //}


            ///


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
            iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(MFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, pw, "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
            if (File.Exists(TargetFile.ToString()))
            {
                File.Delete(TargetFile.ToString());
            }

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

            ///////////////

        }


    }

    public void Execute1()
    {



        // step 1: creation of a document-object       
        Document document = new Document(PageSize.A4, 45f, 45f, 60f, 60f);
        using (FileStream fs = new FileStream(name + ".pdf", FileMode.Create))
        {
            // step 2: we create a writer that listens to the document
            // PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //set some header stuff
            document.AddTitle(name);
            document.AddSubject("Table of " + name);
            // document.AddCreator("This Application");
            //document.AddAuthor("Me");

            // we Add a Header that will show up on PAGE 1
            //Phrase phr = new Phrase(""); //empty phrase for page numbering
            //HeaderFooter footer = new HeaderFooter(phr, true);
            //document.Footer = footer;

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


        _BillRow1 = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["BillRow"].ToString());
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

            //_companyName = string.Empty;
            //_centreName = string.Empty;
            _agentid = 0;
            _agentName = string.Empty;
            _reportTitle = string.Empty;

            ///////

            //GET THE COUNT OF AGENTS

            foreach (DataTable dt in ds1.Tables)
            {
                int f = 1;
                foreach (DataRow row1 in dt.Rows)
                {

                    if (agentno1 == f)
                    {
                        _agentid = Convert.ToInt32(row1[0].ToString());
                        _agentid = Convert.ToInt32(row1[0].ToString());
                        agentcount = Convert.ToInt32(row1[1].ToString());

                    }
                    f++;
                }
            }


            ///////         

            _reportTitle = "Summary report for agentwise Milk Procurement";

            // _date1 = Convert.ToDateTime("02-07-2012");


            ///LOGO
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(_Imagepath);
            //logo.Border = 1;
            //logo.SetAbsolutePosition(document.Left, document.Top - 180);
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
            compname2.DefaultCell.Padding = 2;
            compname3.WidthPercentage = 85;
            compname3.DefaultCell.BorderWidth = 0;
            compname3.AddCell(cells4);
            compname2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            compname2.AddCell(FormatHeaderPhrase(" " + " " + "Route-Id/Name :" + _routeid.ToString() + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "From_Date :" + _date1.ToShortDateString() + " " + "To_Date :" + _date2.ToShortDateString()));



            PdfPTable compname4 = new PdfPTable(1);
            PdfPCell cells5 = new PdfPCell();
            cells5.BorderWidth = 0;
            compname2.DefaultCell.Padding = 2;
            compname4.WidthPercentage = 85;
            compname4.DefaultCell.BorderWidth = 0;
            compname4.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            compname4.AddCell(cells5);
            //compname4.AddCell(FormatHeaderPhrase(" " + " " + "AgentName :" + _agentid.ToString() + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + " " + " " + " " + "  " + "  " + "  " + "  " + "  " + "Session :" + "AM"));
            compname4.AddCell(FormatHeaderPhrase(" " + " " + "Agent-Id/Name :" + _agentid.ToString() + '_' + _agentid.ToString() + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + "  " + " " + " " + " " + "  " + "  " + "  " + "  " + "  "));

            //document.Add(CLogotable);
            document.Add(compname);
            document.Add(compname1);
            document.Add(compname2);
            document.Add(compname3);
            document.Add(compname4);



            // PdfPTable pdfTable = new PdfPTable(table.Columns.Count);
            PdfPTable pdfTable = new PdfPTable(7);
            pdfTable.DefaultCell.Padding = 2;
            pdfTable.WidthPercentage = 85; // percentage            
            pdfTable.DefaultCell.BorderWidth = 1;
            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            int coll = 1;
            foreach (DataColumn column in table.Columns)
            {

                if (coll >= 8)
                {
                    int col1 = 0;

                }
                else
                {
                    pdfTable.AddCell(column.ColumnName);
                }
                coll++;
            }
            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.HeaderRows = 1;  // this is the end of the table header

            pdfTable.DefaultCell.BorderWidth = 1;
            /////////////////////////////////////////      
            countid = 0;
            foreach (DataTable dt in ds1.Tables)
            {
                int f1 = 1;
                foreach (DataRow row2 in dt.Rows)
                {
                    if (agentno1 == f1)
                    {
                        object id;
                        id = row2[0].ToString();
                        int idd = Convert.ToInt32(id);


                        foreach (DataRow dr1 in table.Rows)
                        {
                            object id1;
                            id1 = dr1[7].ToString();
                            int idd1 = Convert.ToInt32(id1);

                            if (idd1 == idd)
                            {
                                countid++;
                            }
                        }
                    }
                    f1++;

                }

            }

            countid1 = 0;
            foreach (DataTable dt in ds1.Tables)
            {
                int f2 = 1;
                foreach (DataRow row2 in dt.Rows)
                {

                    if (f2 < agentno1)
                    {
                        object id;
                        id = row2[0].ToString();
                        int idd = Convert.ToInt32(id);

                        foreach (DataRow dr1 in table.Rows)
                        {
                            object id1;
                            id1 = dr1[7].ToString();
                            int idd1 = Convert.ToInt32(id1);

                            if (idd1 == idd)
                            {
                                countid1++;
                            }
                        }
                    }
                    f2++;
                }
            }

            /////////////////////
            int i = 1;
            int j = 0;
            int k = 0;
            if (agentno1 == 1)
            {
                //j = agentno1 * countid ;
                j = countid;
                // k = j + countid;
                foreach (DataRow row in table.Rows)
                {

                    if (i <= j)
                    {
                        //foreach (object cell in row.ItemArray)
                        //{                           
                        //    pdfTable.AddCell(FormatPhrase(cell.ToString()));
                        //}

                        ////
                        _prdate = Convert.ToDateTime(row[0].ToString());
                        pdfTable.AddCell(FormatPhrase(_prdate.ToShortDateString().ToString()));
                        _session = row[1].ToString();
                        pdfTable.AddCell(FormatPhrase(_session));
                        _milkltr = row[2].ToString();
                        pdfTable.AddCell(FormatPhrase(_milkltr));
                        _fat = row[3].ToString();
                        pdfTable.AddCell(FormatPhrase(_fat));
                        _snf = row[4].ToString();
                        pdfTable.AddCell(FormatPhrase(_snf));
                        _rate = row[5].ToString();
                        pdfTable.AddCell(FormatPhrase(_rate));
                        _amount = row[6].ToString();
                        pdfTable.AddCell(FormatPhrase(_amount));

                        ////
                        i++;
                    }

                }
                document.Add(pdfTable);

                //////
                foreach (DataTable dt in ds.Tables)
                {
                    int f = 1;
                    foreach (DataRow row1 in dt.Rows)
                    {

                        if (f == 1)
                        {
                            // object tot;

                            _Smltr = Convert.ToDouble(row1[8].ToString());
                            _Smltr1 = row1[8].ToString();
                            _Afat = Convert.ToDouble(row1[9].ToString());
                            _Afat1 = row1[9].ToString();
                            _Asnf = Convert.ToDouble(row1[10].ToString());
                            _Asnf1 = row1[10].ToString();
                            _Arate = Convert.ToDouble(row1[11].ToString());
                            _Arate1 = row1[11].ToString();
                            _Samt = Convert.ToDouble(row1[12].ToString());
                            _Samt1 = row1[12].ToString();
                            _Feed = Convert.ToDouble(row1[13].ToString());
                            _Feed1 = row1[13].ToString();
                            _Ai = Convert.ToDouble(row1[14].ToString());
                            _Ai1 = row1[14].ToString();
                            _CartageAmt = Convert.ToDouble(row1[15].ToString());
                            _CartageAmt = (_Smltr * _CartageAmt);
                            _CartageAmt1 = string.Format("{0:F2}", _CartageAmt);
                            PdfPTable Footertable = new PdfPTable(7);

                            Footertable.DefaultCell.Padding = 4;
                            Footertable.WidthPercentage = 85; // percentage                                
                            Footertable.DefaultCell.BorderWidth = 0;
                            Footertable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            Footertable.AddCell(FormatSpace("Tmltr:"+ _Smltr.ToString()));
                            Footertable.AddCell(FormatSpace("Tmltr:" + _Smltr.ToString()));
                            Footertable.AddCell(FormatHeaderPhrase("Tmlt:" +  _Smltr1.ToString()));
                            Footertable.AddCell(FormatHeaderPhrase("Afat:"  + _Afat1.ToString()));
                            Footertable.AddCell(FormatHeaderPhrase("Asnf:" + _Asnf1.ToString()));
                            Footertable.AddCell(FormatHeaderPhrase("Arat:" + _Arate1.ToString()));                            
                            Footertable.AddCell(FormatHeaderPhrase("Tamt:" + _Samt1.ToString().Trim()));

                            Footertable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            //Footertable.DefaultCell.BorderWidth = 0;
                            // _TotDedu = (_Feed + _Ai);
                            //Footertable.AddCell(FormatHeaderPhrase(" " + " " + "DEDUCTIONS :" + "FEED:" + _Feed.ToString() + " " + " " + " " + " " + " " + " " + "Ai:" + _Ai.ToString() + " " + " " + " " + " " + " " + " " + "TotalDeduction:" + _TotDedu.ToString() + "\n" + " " + " " + " " + " " + "CAN_ADV :" + _Canadv.ToString() + " " + " " + " " + " " + " " + " " + "OTHERS :" + _Others));
                            // Footertable.AddCell(FormatHeaderPhrase(" " + " " + "DEDUCTIONS :" + "FEED:" + _Feed.ToString() + " " + " " + " " + " " + " " + " " + "Ai:" + _Ai.ToString() + " " + " " + " " + " " + " " + " " + "TotalDeduction:" + _TotDedu.ToString()));
                            document.Add(Footertable);
                            ///Additions
                            PdfPTable Addtable = new PdfPTable(7);
                            Addtable.DefaultCell.Padding = 4;
                            Addtable.WidthPercentage = 85;
                            Addtable.DefaultCell.BorderWidth = 0;
                            Addtable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            Addtable.AddCell(FormatSpace("ADDTIONS :"));
                            Addtable.AddCell(FormatHeaderPhrase("Additions:"));
                            Addtable.AddCell(FormatHeaderPhrase("Camt:" + _CartageAmt1.ToString()));
                            Addtable.AddCell(FormatSpace("ComAmt:"  + _Ai.ToString()));
                            Addtable.AddCell(FormatSpace("Ai:"  + _Ai.ToString()));
                            Addtable.AddCell(FormatHeaderPhrase("Totadditions:"));
                            _TotAdd = (_CartageAmt);
                            _TotAdd1 = string.Format("{0:F2}", _TotAdd);
                            Addtable.AddCell(FormatHeaderPhrase(_TotAdd1.ToString()));
                            document.Add(Addtable);
                            ///Additions
                            ///Deductions
                            PdfPTable Dedtable = new PdfPTable(7);
                            Dedtable.DefaultCell.Padding = 4;
                            Dedtable.WidthPercentage = 85;
                            Dedtable.DefaultCell.BorderWidth = 0;
                            Dedtable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            Dedtable.AddCell(FormatSpace("DEDUCTIONS :"));
                            Dedtable.AddCell(FormatHeaderPhrase("Deduction:"));
                            Dedtable.AddCell(FormatHeaderPhrase("Fd:" + " " + _Feed1.ToString()));
                            Dedtable.AddCell(FormatHeaderPhrase("Ai:" + " " + _Ai1.ToString()));
                            Dedtable.AddCell(FormatSpace("Ai :" + " " + _Ai.ToString()));
                            Dedtable.AddCell(FormatHeaderPhrase("Totdeduction:"));
                            _TotDedu = (_Feed + _Ai);
                            _TotDedu1 = string.Format("{0:F2}", _TotDedu);
                            Dedtable.AddCell(FormatHeaderPhrase(_TotDedu1.ToString()));
                            document.Add(Dedtable);
                            // Footertable.AddCell(FormatHeaderPhrase(" " + " " + " " + "FEED:" + _Feed.ToString() + " " + " " + " " + " " + " " + " " + "Ai:" + _Ai.ToString() + " " + " " + " " + " " + " " + " " + "TotalDeduction:" + _TotDedu.ToString()));
                            ///Deductions
                            ///Add & Ded
                            PdfPTable ADtable = new PdfPTable(7);
                            ADtable.DefaultCell.Padding = 4;
                            ADtable.WidthPercentage = 85;
                            ADtable.DefaultCell.BorderWidth = 0;
                           // ADtable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                           // ADtable.AddCell(FormatHeaderPhrase("WORDS :"));
                            // t.AddCell(new PdfPCell(new Phrase("R3C1-4")) { Colspan = 4 });
                            // PdfPCell csp = new PdfPCell();
                            //csp.TOP_BORDER = 0;
                            //csp.BOTTOM_BORDER = 0;
                            //csp.LEFT_BORDER = 0;
                            //csp.RIGHT_BORDER = 0;
                            //csp.BorderWidth = 0;

                            // ADtable.AddCell(new PdfPCell(new Phrase("WORDS :" + _Netamt)) { Colspan = 5 });
                            _Netamt = ((_Samt + _TotAdd) - _TotDedu);
                            ///
                            var values = _Netamt.ToString(CultureInfo.InvariantCulture).Split('.');
                            int word = int.Parse(values[0]);
                           // int secondValue = int.Parse(values[1]);

                            ///
                            // int word = Convert.ToInt32(_Netamt);
                            string words = string.Empty;
                            bool isUK = true;
                            words = NumberToText(word, isUK);
                           // ADtable.AddCell(FormatHeaderPhrase(words + "Rupees Only".Trim()));
                            ADtable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            ADtable.AddCell(FormatSpace(".:"));
                            ADtable.AddCell(FormatSpace(".:"));
                            ADtable.AddCell(FormatSpace(".:"));
                            ADtable.AddCell(FormatSpace(".:"));
                            ADtable.AddCell(FormatSpace(".:"));
                            ADtable.AddCell(FormatHeaderPhrase("NetAmount :"));
                            _Netamt1 = string.Format("{0:F2}", _Netamt);
                            ADtable.AddCell(FormatHeaderPhrase(_Netamt1.ToString()));
                            document.Add(ADtable);
                            //word1
                            PdfPTable Wordstable = new PdfPTable(1);
                            Wordstable.DefaultCell.Padding = 4;
                            Wordstable.WidthPercentage = 85;
                            Wordstable.DefaultCell.BorderWidth = 0;
                            Wordstable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            Wordstable.AddCell(FormatHeaderPhrase("Words : " + words + "Rupees Only".Trim()));
                            document.Add(Wordstable);
                            //word1
                            ///Add & Ded

                            f++;


                        }
                    }

                }




                ////10ROWS
                int Tc = 0;
                int Tc1 = 0;
                Tc = _BillRow1 - j;

                PdfPTable Rrow = new PdfPTable(1);
                PdfPCell Rcell = new PdfPCell();
                Rrow.DefaultCell.Padding = 4;
                Rrow.WidthPercentage = 90; // percentage
                Rrow.DefaultCell.BorderWidth = 0;
                Rrow.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;


                int line = 0;
                while (Tc1 < Tc)
                {
                    string space = ".";
                    if (line <= 1)
                    {
                        string lin=string.Empty;
                        if (line == 0)
                        {
                            lin = "Tmlt:TotalMilklitre,A-Average:(Afat,Asnf,Arat),Tamt:TotalAmount,CAmt:CartageAmount,Fd:Feed";
                        }
                        else
                        {
                             lin = "___________________________________________________________________________________________________";
                        }
                        Rrow.AddCell(FormatPhrase(lin));
                        Tc1++;
                        line++;
                    }
                    else
                    {
                        Rrow.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //Rrow.AddCell(".".ToString());
                        Rrow.AddCell(FormatSpace(space));
                        Tc1++;
                    }

                }
                document.Add(Rrow);
                /////    
                //Footer 
                PdfPTable pageNotable = new PdfPTable(7);
                pageNotable.DefaultCell.Padding = 4;
                pageNotable.WidthPercentage = 85;
                pageNotable.DefaultCell.BorderWidth = 0;
                pageNotable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatHeaderPhrase(agentno1.ToString()));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                document.Add(pageNotable);
                // document.Add(new Paragraph(document.BottomMargin, "" + agentno1));
                //Footer
                

            }
            else
            {
                //j = ((agentno1 - 1) * countid);
                j = countid1;
                k = j + countid;

                foreach (DataRow row in table.Rows)
                {

                    if ((i > j) && (i <= k))
                    {
                        //foreach (object cell in row.ItemArray)
                        //{
                        //    pdfTable.AddCell(FormatPhrase(cell.ToString()));
                        //}
                        ////
                        _prdate = Convert.ToDateTime(row[0].ToString());
                        pdfTable.AddCell(FormatPhrase(_prdate.ToShortDateString().ToString()));
                        _session = row[1].ToString();
                        pdfTable.AddCell(FormatPhrase(_session));
                        _milkltr = row[2].ToString();
                        pdfTable.AddCell(FormatPhrase(_milkltr));
                        _fat = row[3].ToString();
                        pdfTable.AddCell(FormatPhrase(_fat));
                        _snf = row[4].ToString();
                        pdfTable.AddCell(FormatPhrase(_snf));
                        _rate = row[5].ToString();
                        pdfTable.AddCell(FormatPhrase(_rate));
                        _amount = row[6].ToString();
                        pdfTable.AddCell(FormatPhrase(_amount));

                        ////

                        ///FOOTER
                        // _Smltr = Convert.ToDouble(row[8].ToString());
                        _Smltr = Convert.ToDouble(row[8].ToString());
                        _Smltr1 = row[8].ToString();
                        _Afat = Convert.ToDouble(row[9].ToString());
                        _Afat1 = row[9].ToString();
                        _Asnf = Convert.ToDouble(row[10].ToString());
                        _Asnf1 = row[10].ToString();
                        _Arate = Convert.ToDouble(row[11].ToString());
                        _Arate1 = row[11].ToString();
                        _Samt = Convert.ToDouble(row[12].ToString());
                        _Samt1 = row[12].ToString();
                        _Feed = Convert.ToDouble(row[13].ToString());
                        _Feed1 = row[13].ToString();
                        _Ai = Convert.ToDouble(row[14].ToString());
                        _Ai1 = row[14].ToString();
                        _CartageAmt = Convert.ToDouble(row[15].ToString());
                        _CartageAmt = (_Smltr * _CartageAmt);
                        _CartageAmt1 = string.Format("{0:F2}", _CartageAmt);

                        ///FOOTER
                    }
                    i++;

                }
                document.Add(pdfTable);
                //////
                //////FF

               
                PdfPTable Footertable = new PdfPTable(7);
                Footertable.DefaultCell.Padding = 4;
                Footertable.WidthPercentage = 85; // percentage                                
                Footertable.DefaultCell.BorderWidth = 0;
                Footertable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                Footertable.AddCell(FormatSpace(". :" + " " + _Smltr.ToString()));
                Footertable.AddCell(FormatSpace(". :" + " " + _Smltr.ToString()));
                Footertable.AddCell(FormatHeaderPhrase("Tmlt:" + _Smltr1.ToString()));
                Footertable.AddCell(FormatHeaderPhrase("Afat:"+ _Afat1.ToString()));
                Footertable.AddCell(FormatHeaderPhrase("Asnf:" + _Asnf1.ToString()));
                Footertable.AddCell(FormatHeaderPhrase("Arat:" + _Arate1.ToString()));
                Footertable.AddCell(FormatHeaderPhrase("Tamt:" + _Samt1.ToString()));
                Footertable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                document.Add(Footertable);
                ///Additions
                PdfPTable Addtable = new PdfPTable(7);
                Addtable.DefaultCell.Padding = 4;
                Addtable.WidthPercentage = 85;
                Addtable.DefaultCell.BorderWidth = 0;
                Addtable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                Addtable.AddCell(FormatSpace(". :"));
                Addtable.AddCell(FormatHeaderPhrase("Addtions:"));
                Addtable.AddCell(FormatHeaderPhrase("Camt:" + _CartageAmt1.ToString()));
                Addtable.AddCell(FormatSpace(". :"+ _Ai.ToString()));
                Addtable.AddCell(FormatSpace(". :"  + _Ai.ToString()));
                Addtable.AddCell(FormatHeaderPhrase("Totadditions:"));
                _TotAdd = (_CartageAmt);
                _TotAdd1 = string.Format("{0:F2}", _TotAdd);
                Addtable.AddCell(FormatHeaderPhrase(_TotAdd1.ToString()));
                document.Add(Addtable);
                ///Additions
                ///Deduction
                PdfPTable Dedtable = new PdfPTable(7);
                Dedtable.DefaultCell.Padding = 4;
                Dedtable.WidthPercentage = 85;
                Dedtable.DefaultCell.BorderWidth = 0;
                Dedtable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                Dedtable.AddCell(FormatSpace(". :"));
                Dedtable.AddCell(FormatHeaderPhrase("Deductions:"));
                Dedtable.AddCell(FormatHeaderPhrase("Fd:"+ _Feed1.ToString()));
                Dedtable.AddCell(FormatHeaderPhrase("Ai:"+ _Ai1.ToString()));
                Dedtable.AddCell(FormatSpace(".:" + " " + _Ai.ToString()));
                Dedtable.AddCell(FormatHeaderPhrase("Totdeduction:"));
                _TotDedu = (_Feed + _Ai);
                _TotDedu1 = string.Format("{0:F2}", _TotDedu);
                Dedtable.AddCell(FormatHeaderPhrase(_TotDedu1.ToString()));
                document.Add(Dedtable);
                // Footertable.AddCell(FormatHeaderPhrase(" " + " " + " " + "FEED:" + _Feed.ToString() + " " + " " + " " + " " + " " + " " + "Ai:" + _Ai.ToString() + " " + " " + " " + " " + " " + " " + "TotalDeduction:" + _TotDedu.ToString()));
                ///Deduction
                ///Add & Ded
                PdfPTable ADtable = new PdfPTable(7);
                
                //ADcell.Colspan="
                ADtable.DefaultCell.Padding = 4;
                ADtable.WidthPercentage = 85;
                ADtable.DefaultCell.BorderWidth = 0;
                ADtable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;                
              
                _Netamt = ((_Samt + _TotAdd) - _TotDedu);
                ///
                var values = _Netamt.ToString(CultureInfo.InvariantCulture).Split('.');
                int word = int.Parse(values[0]);
               // int secondValue = int.Parse(values[1]);

                ///
              
                string words = string.Empty;
                bool isUK = true;
                words = NumberToText(word, isUK);         
               
               //Colspan ok
                //ADtable.AddCell(new PdfPCell(new Phrase("WORDS :" + words + "Rupees Only".Trim(), new Font(FontFactory.GetFont(FontFactory.HELVETICA, 8)))) { Colspan = 5 });                             

                ADtable.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                ADtable.AddCell(FormatSpace(".:"));
                ADtable.AddCell(FormatSpace(". :"));
                ADtable.AddCell(FormatSpace(".:"));
                ADtable.AddCell(FormatSpace(".:"));
                ADtable.AddCell(FormatSpace(".:"));
                ADtable.AddCell(FormatHeaderPhrase("NetAmount :"));
                _Netamt1 = string.Format("{0:F2}", _Netamt);
                ADtable.AddCell(FormatHeaderPhrase(_Netamt1.ToString()));
                document.Add(ADtable);
                //word1
                PdfPTable Wordstable = new PdfPTable(1);
                Wordstable.DefaultCell.Padding = 4;
                Wordstable.WidthPercentage = 85;
                Wordstable.DefaultCell.BorderWidth = 0;
                Wordstable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                Wordstable.AddCell(FormatHeaderPhrase("Words : " + words + "Rupees Only".Trim()));
                document.Add(Wordstable);
                //word1

                ///Add & Ded


                //  }
                //  f++;
                //  }

                // }                          


                ////10ROWS
                int Tc = 0;
                int Tc1 = 0;
                Tc = _BillRow1 - (k - j);
                PdfPTable Rrow = new PdfPTable(1);
                PdfPCell Rcell = new PdfPCell();
                Rrow.DefaultCell.Padding = 4;
                Rrow.WidthPercentage = 90; // percentage
                Rrow.DefaultCell.BorderWidth = 0;
                Rrow.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                int line = 0;
                while (Tc1 < Tc)
                {
                    string space = ".";
                    if (line <= 1)
                    {
                        string lin = string.Empty;
                        if (line == 0)
                        {
                            lin = "Tmlt:TotalMilklitre,A-Average:(Afat,Asnf,Arat),Tamt:TotalAmount,CAmt:CartageAmount,Fd:Feed";
                        }
                        else
                        {
                            lin = "___________________________________________________________________________________________________";
                        }
                        Rrow.AddCell(FormatPhrase(lin));
                        Tc1++;
                        line++;
                    }
                    else
                    {
                        Rrow.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //Rrow.AddCell(".".ToString());
                        Rrow.AddCell(FormatSpace(space));
                        Tc1++;
                    }
                }
                document.Add(Rrow);

                /////
                //Footer 
                PdfPTable pageNotable = new PdfPTable(7);
                pageNotable.DefaultCell.Padding = 4;
                pageNotable.WidthPercentage = 85;
                pageNotable.DefaultCell.BorderWidth = 0;
                pageNotable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatHeaderPhrase(agentno1.ToString()));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                pageNotable.AddCell(FormatSpace("NetAmt:"));
                document.Add(pageNotable);
               // document.Add(new Paragraph(document.BottomMargin, "" + agentno1));
                //Footer
            }
            //FF
            //TOt.AddCell(Tcell);
            //document.Add(TOt);
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

    public static string NumberToText(int number, bool isUK)
    {
        if (number == 0) return "Zero";
        string and = isUK ? "and " : ""; // deals with UK or US numbering
        if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
        "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
        "Six Hundred " + and + "Forty Eight";
        int[] num = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (number < 0)
        {
            sb.Append("Minus ");
            number = -number;
        }
        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
        string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
        string[] words3 = { "Thousand ", "Million ", "Billion " };
        num[0] = number % 1000;           // units
        num[1] = number / 1000;
        num[2] = number / 1000000;
        num[1] = num[1] - 1000 * num[2];  // thousands
        num[3] = number / 1000000000;     // billions
        num[2] = num[2] - 1000 * num[3];  // millions
        for (int i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;
            u = num[i] % 10;              // ones
            t = num[i] / 10;
            h = num[i] / 100;             // hundreds
            t = t - 10 * h;               // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i < first) sb.Append(and);
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();
    }




    public int GETPDFPageCount(string filepath)
    {
        int page_count = 0;
        string erro = string.Empty;
        try
        {
            //check for the extension 
            string extension = Path.GetExtension(filepath);
            if (extension == ".PDF" || extension == ".pdf")
            {
                //Create instance for the PDF reader
                PdfReader pdf_fie = new PdfReader(filepath);
                //read it's pagecount
                page_count = pdf_fie.NumberOfPages;
                //close the file
                pdf_fie.Close();
            }
        }
        catch (PdfException ex)
        {
            erro = ex.Message;
        }
        return page_count;
    }

}
