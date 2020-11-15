<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill.aspx.cs" Inherits="Bill" Title="OnlineMilkTest|Bill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;Billing
                </p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7" colspan="2">
            </td>
        </tr>
        </table>
    <div align="center">
    <fieldset class="legbill">
    <legend class="fontt">Billing</legend>
    <div class="fontt">
        From Date
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:ImageButton ID="popupcal" runat="server" ImageUrl="~/calendar.gif" Height="20px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox3"
                                PopupButtonID="popupcal" Format="MM-dd-yyyy" PopupPosition="TopRight">
        </asp:CalendarExtender>
To Date<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <asp:ImageButton ID="popupcal1" runat="server" ImageUrl="~/calendar.gif" 
            Height="20px" TabIndex="1" />
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox4"
                                PopupButtonID="popupcal1" Format="MM-dd-yyyy" PopupPosition="TopRight">
        </asp:CalendarExtender>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Download" onclick="Button1_Click" 
            BackColor="#6F696F" ForeColor="White" Width="70px" TabIndex="2" />
            &nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="View-Bill"  TabIndex="3" BackColor="#6F696F" ForeColor="White" 
            Width="79px"/>    
            <br />
             <iframe id="iframShowFiles" runat="server" height="500" width="700"  frameborder="0"></iframe>
    
        </div>
     </fieldset>
    
    </div>    
    <br />
    <br />  
<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>
