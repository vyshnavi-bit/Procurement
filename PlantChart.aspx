<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantChart.aspx.cs" Inherits="PlantChart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
 <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1"]>
<title>Multi series Line chart using Google Visualization</title>    
     <script type="text/javascript" src="https://www.google.com/jsapi"></script>
</head>
<body>
    <form id="form1" >
  <div>
        <asp:Literal ID="lt" runat="server"></asp:Literal>
        <div id="chart_div"></div>
  </div>   
</form>
</body>
</html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

