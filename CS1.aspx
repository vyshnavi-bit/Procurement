<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS1.aspx.cs" Inherits="CS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style type="text/css">
        .margleft
        {	
	     margin-left:256px;
        }
          .bcolor
        {
            background: #e0e0e0;
           
        }
     </style>
    <title></title>
    </head>
<body class="bcolor">
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager> 
    <div align="center">
               <div style="width: 13%; float: left;">
                                    <img src="Image/VLogo.png" alt="Vyshnavi" width="100px" height="52px" />
                                </div>
 <div style="width: 70%; float: left;">
               <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Size="22px" ForeColor="#ff1493" Text="SRI VYSHNAVI DAIRY SPECIALITIES PVT LIMITED"></asp:Label>
               <br />
                <span style="font-size: 20px; font-weight: bold; color: #0252aa;">Live Procurement RoutewiseDetails</span>
                </div>
                 <div style="width: 17%; float: left;">
                 <span style="font-size: 20px; font-weight: bold; color: Red;">
    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Blue" Font-Bold="true"  NavigateUrl="~/LiveDumping.aspx">Back</asp:HyperLink>
    </span>
                 </div>
                <br />
     
    
    <table width="100px">
    
    <tr>  
    <td colspan="2" align="justify">
     <hr />
        <asp:Label ID="Lbl_PlantTitle" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
    <cc1:LineChart ID="LineChart1" runat="server" BorderStyle="None" 
        ChartType="Basic" ChartTitleColor="#0E426C" Visible = "false"
        CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
       <Series></Series>
       
    </cc1:LineChart>
    </td>
    </tr>
    <tr>
     <td valign="top" align="right"> 
      <asp:GridView ID="grdweight" runat="server" ForeColor="White"  CssClass="gridcls"
        GridLines="Both" Font-Bold="true"  AutoGenerateColumns="false">
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
        <HeaderStyle BackColor="#FF6600" Font-Bold="true" ForeColor="White" Font-Italic="False"
            Font-Size="Large" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="false" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="Wtime" HeaderText="WTime" ItemStyle-Width="20" />
            <asp:BoundField DataField="WSampleNo" HeaderText="WsampleNo" ItemStyle-Width="40" />
           
        </Columns>
    </asp:GridView>
    </td>
    <td valign="top" align="left">
       <asp:GridView ID="grdAnalyzer" runat="server" ForeColor="White"  CssClass="gridcls"
        GridLines="Both" Font-Bold="true"  AutoGenerateColumns="false">
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
        <HeaderStyle BackColor="#6A8347" Font-Bold="false" ForeColor="White" Font-Italic="False"
            Font-Size="Large" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="Atime" HeaderText="ATime" ItemStyle-Width="20" />
            <asp:BoundField DataField="ASampleNo" HeaderText="AsampleNo" ItemStyle-Width="40" />
           
        </Columns>
    </asp:GridView>
    </td>
    </tr>
    </table>
   
   
    
   
    
    
    </form>
</body>
</html>
