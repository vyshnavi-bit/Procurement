<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LivePlantRoutewisedetails.aspx.cs" Inherits="LivePlantRoutewisedetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
    <tr>
        <td>
            <b>Id</b>
        </td>
        <td>
            <asp:Label ID="lblId" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <b>Name</b>
        </td>
        <td>
            <asp:Label ID="lblName" runat="server"></asp:Label>
        </td>
    </tr>
        <tr>
        <td>
            <b>Country</b>
        </td>
        <td>
            <asp:Label ID="lblCountry" runat="server"></asp:Label>
        </td>
    </tr>
</table>
</asp:Content>


