<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrganizationChart.aspx.cs" Inherits="OrganizationChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <%--<script type = "text/javascript">
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
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <form>
 <table align=center> 
 <tr align="center">
 <td>
            <strong style="font-size: small; text-align: center;" __designer:mapid="5f2">Plant Name</strong><asp:DropDownList ID="ddl_Plantname" 
                    runat="server" CLASS="field3" 
                    onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
         Width="150px">
                </asp:DropDownList>
                </td>
                </tr>
 <tr align="center">
 <td>
                    <asp:Button ID="Save" runat="server" CssClass="form93" Font-Bold="true" 
                        Font-Size="X-Small" onclick="Save_Click" 
                        OnClientClick="return confirmationSave();" TabIndex="6" Text="Get Details" 
                        xmlns:asp="#unknown" />
                  <asp:Button ID="Button9" runat="server" CssClass="form93" Font-Bold="True" 
                    Font-Size="10px"  
         OnClientClick="window.open('OrgReport1.aspx', 'OtherPage');" 
         onclick="Button9_Click1" Text="print" />
                </td>
                </tr>
  </table>  
   <div>
  
     <div style="color:#0000FF"> 
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
    </div>
    <div>
     <div id="chart_div" style="color:#0000FF"> 
    </div>
    </div>    
      
   </div>
    </form>
     
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

