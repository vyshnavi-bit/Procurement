<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tallyaccountsgroupwithsubgroup.aspx.cs" Inherits="Tallyaccountsgroupwithsubgroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style1
        {
            font-family: Andalus;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

     <strong __designer:mapid="1550">
        <asp:LinkButton ID="LinkButton1" runat="server" align="left" onclick="LinkButton1_Click" 
            style="text-align: left; font-family: Andalus">Back</asp:LinkButton> </strong>
    <center style="background-color: #FFFFFF; margin-left: 40px;">
        &nbsp;<asp:Label ID="Label3" runat="server" Text="Accounting Sub Header Creation" 
            
            
            style="font-family: Andalus; font-size: medium; color: #FF3399; text-decoration: underline"></asp:Label>
        <br />
    <fieldset style="background-color: #CCFFFF; width: 331px;">
   
   <table align="center" style="width: 404px">
   
   




   <tr>
   
   <td align="left" class="style5">
       <asp:Label ID="Label10" runat="server" Text="Date" Font-Size="Small" 
           CssClass="style1"></asp:Label>
   </td>
   <td align="left" class="style3">
     
                          <asp:TextBox ID="txt_date" runat="server" TabIndex="4" Width="150px" 
                              CssClass="tb10" Font-Size="X-Small" Enabled="False" Height="24px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_date">
                            </asp:CalendarExtender>
                          </em></strong>
   </td>
   
   </tr>




   <tr>
   
   <td align="left">
       <asp:Label ID="Label14" runat="server" Text="Accounts Group" Font-Size="Small" 
           CssClass="style1"></asp:Label>
   </td>
   <td align=left class="style3">

                 <asp:DropDownList ID="ddl_accsubgroup" runat="server" CssClass="tb10" 
                     Height="40px" onselectedindexchanged="ddl_accsubgroup_SelectedIndexChanged" 
                     Width="140px">
                 </asp:DropDownList>

   </td>
   
   </tr>




   <tr>
   
   <td align="left">
       <asp:Label ID="Label15" runat="server" Text="Accounts Head Name" Font-Size="Small" 
           CssClass="style1"></asp:Label>
   </td>
   <td align=left class="style3">

       <asp:TextBox ID="txt_ledger" runat="server" CssClass="tb10" Height="30px" 
           Width="150px" ontextchanged="txt_ledger_TextChanged"></asp:TextBox>

   </td>
   
   </tr>




   <tr>
   
   <td align="left">
       <asp:Label ID="Label13" runat="server" Text="Description" 
           Font-Size="Small" CssClass="style1"></asp:Label>
   </td>
   <td ALIGN="left" class="style3">
       <asp:TextBox ID="txt_desc" runat="server" CssClass="tb10" Height="40px" 
           TextMode="MultiLine" Font-Size="X-Small" Width="150px"></asp:TextBox>
   </td>
   
   </tr>

   


   <tr>
   
   <td align="left">

                 &nbsp;</td>
   <td align="CENTER" class="style3">   
         <center> 
             <asp:Button ID="Button1" runat="server" CssClass="button2222" Text="Save" 
                 onclick="Button1_Click" />  </center>
   </td>
   
   </tr>




   </table>
   
   
   
    </fieldset>
        `<br />
        <strong __designer:mapid="1550">
                  <em __designer:mapid="15ce">
        <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </em></strong>
       <br />
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <br />
    </center>

 
   <center>  
   
   
   
   
   
   
       <br />
   
   
   
   
   
   
      </center>







</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

