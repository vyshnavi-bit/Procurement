<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChartTest.aspx.cs" Inherits="ChartTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <asp:DropDownList ID="ddlCountry1" runat="server"></asp:DropDownList>
    <asp:DropDownList ID="ddlCountry2" runat="server"></asp:DropDownList>
    <asp:Button ID="btnCompare" runat="server" Text="Compare" OnClick="Compare" Height="26px" />
    
   
   <hr />
    <cc1:LineChart ID="LineChart1" runat="server" BorderStyle="None"
        ChartType="Stacked" ChartTitleColor="#0E426C" Visible = "false"
        CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
    </cc1:LineChart>  
    
   
</asp:Content>

