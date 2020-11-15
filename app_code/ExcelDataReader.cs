using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ExcelDataReader
{
    private System.IO.Stream stream;

    public ExcelDataReader(System.IO.Stream stream)
    {
        // TODO: Complete member initialization
        this.stream = stream;
    }
    public System.Data.DataSet WorkbookData { get; set; }
}
