<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantLineGraph.aspx.cs" Inherits="PlantLineGraph" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            font-weight: bold;
        }
    </style>

      <style type="text/css">
.body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}
</style>
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
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
         <div class="form-style-9" style="height: auto">
<div class="form-style-3">
<table width=100% style="height: 93px">

<tr  ALIGN=LEFT>
<td WIDTH="98%">

<fieldset style="height: auto"  ><legend><span>Plant  Summary Report </span></legend>
    <table class="style1">
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%">
                    <span>From Date </span>
                <label>    <asp:TextBox ID="txt_FromDate" runat="server"   Width="75px"></asp:TextBox></label>
                    <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                        Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                        TargetControlID="txt_FromDate">
                    </asp:CalendarExtender>
                    <span>To Date </span>
                     <span> <asp:TextBox ID="txt_ToDate" runat="server" Width="75px"></asp:TextBox></span>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                        TargetControlID="txt_ToDate">
                    </asp:CalendarExtender>
                    <br />
                    <br />

                </td>
            </tr>
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%">
           
               <asp:Button ID="Button5" runat="server" CssClass="form93" Font-Bold="False"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"   
                    OnClientClick="return confirmationSave();" onclick="Button5_Click" Text="Get Report" 
                  TabIndex="6" />
           
               <asp:Button ID="Button7" runat="server" CssClass="form93" Font-Bold="False"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"    OnClientClick="return PrintPanel();" 
                    onclick="Button7_Click" Text="Print" 
                  TabIndex="6" />
                </td>
            </tr>
        </table>
 </fieldset>


</td>
 
</tr>

</table>
</div>

</div>   
               
    <asp:Panel id="pnlContents" runat = "server">
  
    <asp:GridView ID="GridView1" runat="server" Font-Size="Small" 
            style="text-align: center" onrowcreated="GridView1_RowCreated">
    </asp:GridView>
         
              <br /> 
                 
          
        <div>
            <table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="style3" 
                            style="font-size: small; color: #3333CC" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="style3" 
                            style="font-size: small; color: #FF0000" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Font-Size="Small" style="color: #009933" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Size="Small" style="color: #CC6699" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Font-Size="Small" style="color: #3399FF" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Font-Size="Small" style="color: #CC0066" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Size="Small" style="color: #00CC00" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Font-Size="Small" style="color: #FF6666" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Font-Size="Small" style="color: #CC0066" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Font-Size="Small" style="color: #000099" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Font-Size="Small" style="color: #990099" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Font-Size="Small" style="color: #669999" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Font-Size="Small" 
                            style="font-size: small; color: #00CC99;" Text="Label" CssClass="style3"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Font-Size="Small" style="color: #3366CC" 
                            Text="Label" CssClass="style3"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="15" align=center>
                        <asp:Label ID="Label17" runat="server" CssClass="style3" 
                            style="font-size: small; color: #333300; text-align: center;"></asp:Label>
                        <asp:Label ID="Label16" runat="server" CssClass="style3" 
                            style="font-size: small; color: #660066; text-align: center;"></asp:Label>
                        <asp:Label ID="Label15" runat="server" CssClass="style3" 
                            style="font-size: small; color: #000066; text-align: center;"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="style1">
                <tr>
               
                    <td align="left" valign="top" width="1000px">
                        &nbsp;<div ID="chart_div" style="width: 1000px">
                         
                        </div>
                           <asp:Literal ID="lt" runat="server"></asp:Literal>
                    </td>
                    
                </tr>
            </table>
        </div>
   
 
   </asp:Panel>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

