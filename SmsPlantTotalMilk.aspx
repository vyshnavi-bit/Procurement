<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SmsPlantTotalMilk.aspx.cs" Inherits="SmsPlantTotalMilk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div align="center">
    <table width="50%"  >
    <tr valign="top">
    <td>
   
    <fieldset class="fontt">
    <legend >Message Testing</legend>
    <table align="center">
   
   <tr>   
   <td align="left" class="style5">
       <asp:Label ID="Label10" runat="server" Text="Date" Font-Size="Small" 
           style="font-family: Andalus"></asp:Label>
   </td>
   <td align="left" class="style5">                 
       <asp:TextBox ID="txt_FromDate" runat="server" AutoPostBack="True"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="BottomRight">
                        </asp:CalendarExtender>    </td>   
   </tr>
   <tr>   
   <td align="left">
       <asp:Label ID="Label5" runat="server" Text="Mobile No" Font-Size="Small" style="font-family: Andalus"></asp:Label>
   </td>
   <td align="left">
       <asp:TextBox ID="txt_mobile" runat="server" CssClass="tb10" Width="149px"></asp:TextBox>
   </td>   
   </tr>
   <tr>   
   <td > </td>
   <td>   
         <div align="right"> <asp:Button ID="btn_Send" runat="server" BackColor="#00CC00" 
                 BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px"   
                 Text="Send" onclick="btn_Send_Click" /> </div>
   </td>   
   </tr>
   </table>
    </fieldset>
    
    </td>    
  
    </tr>
    <tr valign="top">
    <td colspan="3">           
       <div align="center">
        <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red" Text="Label"></asp:Label>
       </div>    
    </td>
    </tr>
    </table>
</div>
</asp:Content>


