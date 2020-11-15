<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SMSTESTINGPAGE.aspx.cs" Inherits="SMSTESTINGPAGE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .tb10
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <br />
    <br />
    <br />
   
   
<div align="center">
    <table width="100%"  >
    <tr valign="top">
    <td>
   
    <fieldset class="fontt">
    <legend >Message Testing</legend>
    <table align="center">
   
   <tr>   
   <td align="left" class="style5">
       <asp:Label ID="Label10" runat="server" Text="Message Content" Font-Size="Small" 
           style="font-family: Andalus"></asp:Label>
   </td>
   <td align="left" class="style5">                 
       <asp:TextBox ID="txt_smscontend" runat="server" CssClass="tb10" Height="47px" TextMode="MultiLine" Width="155px"></asp:TextBox>
   </td>   
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
         <div align="right"> <asp:Button ID="Button1" runat="server" BackColor="#00CC00" BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px"   Text="Send" onclick="Button1_Click" /> </div>
   </td>   
   </tr>
   </table>
    </fieldset>
    
    </td>
    <td>
    <fieldset class="fontt">
    <legend>Message Balance</legend>
    <table>
    <tr>
    <td>
   <asp:Label ID="lbl_Msgbalanace" runat="server" Text="Message Balanace"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="txt_Msgbalanace" runat="server" CssClass="tb10" Width="149px" ></asp:TextBox>
    </td>
    </tr>    
    <tr>
    <td colspan="2">
          <div align="right"> <asp:Button ID="btn_smsbalance" runat="server" BackColor="#00CCFF" 
                  BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px"   
                  Text="Msg Balance" onclick="btn_smsbalance_Click"  />
                  <asp:Button ID="btn_clearall" runat="server" BackColor="#00CCFF" 
                  BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px"   
                  Text="ClearAll" onclick="btn_clearall_Click"  /> 
                   </div>
    </td>
    </tr>
    </table>
    </fieldset> 
        
    </td>
    <td>
     <fieldset class="fontt">
    <legend>DND Check</legend>
    <table>   
   <tr>
    <td>
   <asp:Label ID="Lbl_Dndcheck" runat="server" Text="DND Check Number"></asp:Label>
    </td>
    <td>
     <asp:TextBox ID="txt_Dndcheck" runat="server" CssClass="tb10" Width="149px" ></asp:TextBox>
    </td>
    </tr>    
    <tr>
    <td colspan="2">
           <div align="right"> <asp:Button ID="btn_Dndcheck" runat="server" 
                   BackColor="Red" BorderStyle="Double" Font-Bold="True" ForeColor="White" 
                   Height="26px"   Text="DND Check" onclick="btn_Dndcheck_Click"  /> </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">



</asp:Content>

