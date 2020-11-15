<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Changenewpassword.aspx.cs" Inherits="Changenewpassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
        .style5
        {
            width: 1501px;
            font-family: Andalus;
        }
        .style6
        {
            width: 1501px;
            font-family: Andalus;
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>



    <center style="background-color: #FFFFFF; margin-left: 40px;">
        &nbsp;<asp:Label ID="Label3" runat="server" Text="Password Change" 
            
            
            style="font-family: Andalus; font-size: medium; color: #FF3399; text-decoration: underline"></asp:Label>
        <br />
    <fieldset style="background-color: #CCFFFF; width: 331px;">
   
   <table align="center" style="width: 350px">
   
   


   <tr>
   
   <td align="right" width="40%" >
       <asp:Label ID="Label10" runat="server" Text="Date" Font-Size="Small" 
           CssClass="style1" style="font-family: Andalus" Visible="False"></asp:Label>
   </td>
   <td align="left" width="60%">

                          <asp:TextBox ID="txt_date" runat="server" TabIndex="4" Width="150px" 
                              CssClass="tb10" Font-Size="X-Small" Enabled="False" Height="24px" 
                              Visible="False"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_date">
                            </asp:CalendarExtender>

   </td>
   
   </tr>






   <tr>
   
   <td align="right" width="40%" >
     <asp:Label ID="Label2" runat="server" Text="User Name" Font-Size="Small" 
           CssClass="style6" ></asp:Label>   
   </td>
   <td align="left" width="60%">

       <asp:TextBox ID="txt_uname" runat="server" CssClass="tb10" Height="30px" 
           Width="150px"></asp:TextBox>

   </td>
   
   </tr>






   <tr>
   
   <td align="right" class="style5">
     <asp:Label ID="Label15" runat="server" Text="Old Password" Font-Size="Small" 
           CssClass="style6" ></asp:Label>   
   </td>
   <td align="left" class="style3">
     
                          </em></strong>

       <asp:TextBox ID="txt_oldpass" runat="server" CssClass="tb10" Height="30px" 
           Width="150px" TextMode="Password"></asp:TextBox>

   </td>
   
   </tr>



   <tr>
   
   <td align="right">
       <asp:Label ID="Label16" runat="server" Text="New Password" Font-Size="Small" 
           CssClass="style6"></asp:Label>
   </td>
   <td align=left class="style3">

       <asp:TextBox ID="txt_newpass" runat="server" CssClass="tb10" Height="30px" 
           Width="150px" TextMode="Password"></asp:TextBox>

   </td>
   
   </tr>




   <tr>
   
   <td align="right">
       <asp:Label ID="Label14" runat="server" Text="Confirm New Password" Font-Size="Small" 
           CssClass="style6"></asp:Label>
   </td>
   <td align=left class="style3">

       <asp:TextBox ID="txt_confirmpass" runat="server" CssClass="tb10" Height="30px" 
           Width="150px" TextMode="Password"></asp:TextBox>

   </td>
   
   </tr>




   <tr>
   
   <td align="left">

                 &nbsp;</td>
   <td align="center" class="style3">   
         <center> 
             <asp:Button ID="Button1" runat="server" CssClass="button2222" Text="Save" 
                 onclick="Button1_Click" />  </center>
   </td>
   
   </tr>




   </table>
   
   
   
    </fieldset>
        <br />
        <strong __designer:mapid="1550">
                  <em __designer:mapid="15ce">
        <br />
    </em></strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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

