<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" Title="OnlineMilkTest|Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><br />
    <h1 class="subheading">&nbsp;&nbsp; LOGOUT</h1>
        <br />
        <center>
    <p class="fontt" >
        You have been logged out of the system. To log in, please return to the 
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="LoginDefault.aspx">Login Page</asp:LinkButton>
            <br />
            <br />
            <br />
            <br /></p>
            </center>
</asp:Content>

