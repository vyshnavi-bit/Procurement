<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RateChartViewer.aspx.cs" Inherits="RateChart" Title="RATECHART VIEWER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
          <td width="100%" colspan="2">
          <p style="line-height: 150%"><font class="Highlight">RATE CHART
          </font></p>
          </td>
        </tr>
        <tr>
          <td width="100%" height="3px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" class="line" height="1px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" height="7" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" colspan="2"> 
          </td></tr></table>


    <div align="center">
    <table width="100%">
    <tr>
    <td width="35%"></td>
    <td width="15%"> 
    <asp:HyperLink ID="HyperLink1" runat="server"  
NavigateUrl="~/RateChart.aspx" ImageUrl= "Image/create.gif"></asp:HyperLink>
    
       </td>
      
    <td width="15%"> 
    <asp:HyperLink ID="HyperLink2" runat="server"  
NavigateUrl="~/NewRateChartViewer.aspx" ImageUrl= "Image/viewn.gif"></asp:HyperLink>
   </td>
      <td width="35%"></td>
   
    </tr></table></div>
</asp:Content>

