<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Daily_Report.aspx.cs" Inherits="Daily_Report" Title="OnlineMilkTest|Daily Report" %>

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
                    &nbsp;&nbsp;Daily Report
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
    <fieldset class="legdaily">
    <legend class="fontt">Daily Report</legend>
    <div class="fontt">

<table width="100%">
<tr> <td width="30%"></td>
                   <td width="35%" class="fontt"><asp:DropDownList ID="dl_Routeid" runat="server" AutoPostBack="True" 
            Height="25px" Width="125px" Visible="False">
            <asp:ListItem Value="11">11-Pondicherry</asp:ListItem>
            <asp:ListItem Value="12">12-Villupuram</asp:ListItem>
        </asp:DropDownList></td>
                   
        
        <td width="45%" class="fontt"></td>
        </tr></table>
        <br />
        <br />
    <table width="100%">
  <tr> <td width="15%"></td>
                   <td width="15%" class="fontt">
        From Date</td>
         <td width="15%" class="fontt">
        <asp:TextBox ID="txt_FromDate" runat="server"></asp:TextBox></td>
        <td width="5%">
        <asp:ImageButton ID="popupcal" runat="server" ImageUrl="~/calendar.gif" 
                Height="20px" TabIndex="1" />
        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="popupcal" Format="MM-dd-yyyy" PopupPosition="TopRight">
        </asp:CalendarExtender></td>
         <td width="15%" class="fontt">
       <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
        </td>
         <td width="15%" class="fontt">
        <asp:TextBox ID="txt_ToDtate" runat="server"></asp:TextBox></td>
         <td width="5%">
        <asp:ImageButton ID="popupcal1" runat="server" ImageUrl="~/calendar.gif" 
                 Height="20px" TabIndex="2" />
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_ToDtate"
                                PopupButtonID="popupcal1" Format="MM-dd-yyyy" PopupPosition="TopRight">
        </asp:CalendarExtender></td>
        <td width="15%"></td>
        </tr>
        
        </table>
        <br />
       <table width="100%">
       <tr>
       
        <td width="25%"></td>
       <td width="10%" class="fontt">
        <asp:RadioButton ID="rd_Am" runat="server" Text="Am" AutoPostBack="True" 
            oncheckedchanged="rd_Am_CheckedChanged" TabIndex="3" /></td>
            <td width="10%">
        <asp:RadioButton ID="rd_Pm" runat="server" Text="Pm" AutoPostBack="True" 
            oncheckedchanged="rd_Pm_CheckedChanged1" TabIndex="4" /></td>
            <td width="10%">
        <asp:RadioButton ID="rd_Period" runat="server" Text="Period" 
            AutoPostBack="True" oncheckedchanged="rd_Period_CheckedChanged" TabIndex="5" /></td>
            <td width="5%">
        <asp:RadioButton ID="rd_Bill" runat="server" Text="Bill" AutoPostBack="True" 
            oncheckedchanged="rd_Bill_CheckedChanged" Visible="False" /></td><td width="25%"></td></tr>
       
       </table>
      
       <br />
        <asp:Button ID="btn_Report" runat="server" onclick="btn_Report_Click" 
            Text="Download " style="height: 26px" BackColor="#6F696F" ForeColor="White" 
            Width="70px" ToolTip="Daily Report" TabIndex="6" />
   
        &nbsp;<asp:Button ID="btn_View" runat="server" onclick="btn_View_Click" 
            Text="View-Report" style="height: 26px" BackColor="#6F696F" ForeColor="White" 
            Width="79px" ToolTip="Daily Report" TabIndex="6" />
            <br />
             <iframe id="iframShowFiles" runat="server" height="500" width="700"  frameborder="0"></iframe>
    
   
    </div>
    </fieldset>
    </div>
    <br />
    <br />
    <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />

</asp:Content>


