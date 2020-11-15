<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ServerlocalProImport.aspx.cs" Inherits="ServerlocalProImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center>
<div>
    <br />
    <asp:DropDownList ID="ddl_TableName" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <asp:FileUpload ID="Excel_FileUpload" runat="server" />
    <br />
    <br />
    <asp:Button ID="btn_ExcelFileUpload" runat="server" 
        onclick="btn_ExcelFileUpload_Click" Text="ExcelFile_Upload" />
    <br />
    <br />
    <asp:Button ID="btn_ImportExcelData" runat="server" Text="Import ExcelData" 
        onclick="btn_ImportExcelData_Click" style="height: 26px" />
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" 
        Visible="False" />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="57px" TextMode="MultiLine" 
        Width="291px"></asp:TextBox>
    <br />
    <br />
</div>
</center>
</asp:Content>


