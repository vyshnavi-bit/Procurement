<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgReport1.aspx.cs" Inherits="OrgReport1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
 <style type="text/css">
 
 .styleapp
 {
     height:100%;
     width:50%;
     text-align:CENTER;

 }
 
 </style>
      <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            //       printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
    </script>
</head>
<body>
 
    <form>
    <LEFT>
    <div class="styleapp">
     <asp:Panel id="pnlContents" align="LEFT" runat = "server">
     
    <div class="styleapp" align=LEFT>
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
    </div> 
    <div>
     <div id="chart_div" style="color:#0000FF"> 
    </div>
    </div>
    </asp:Panel>
    </div>
    </LEFT>
    </form>
    
</body>
</html>
