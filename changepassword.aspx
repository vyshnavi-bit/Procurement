<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" Title="OnlineMilkTest|Change Password" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link  type="text/css" runat="server" href="SampleStyleSheet1.css"  rel="Stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
          
            <table border="0" cellpadding="0" cellspacing="1" width="140%">
        <tr>
          <td width="100%" colspan="2"><br />
          <p style="line-height: 150%; float:left;"><font class="subheading">&nbsp;&nbsp;Change Password
          </font></p>
          </td>
        </tr>
        <tr>
          <td width="100%" height="3px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" class="line" height="1px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" height="7" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" colspan="2"> 
          </td></tr></table>
                
                
                <br />
                <br />
                
               <div class="legendChangepassword">
               <fieldset class="fontt">
               <legend>Change Password</legend>
                <table width="100%">
                <tr align="right">
                 <td width="14%" style="width: 29%">
                	<asp:Label ID="uname" runat="server" Text="UserName"></asp:Label></td>
                 <td align="left" width="29%"> 
                 <asp:TextBox ID="txtUserName" runat="server" TabIndex="1" TextMode="SingleLine"   />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtUserName">
              </asp:RequiredFieldValidator> 
                 
                 </td>
                
                </tr>
          
		
         
    
              
		<tr align="right">
		<td width="14%" style="width: 29%">
		<asp:Label ID="o_pword" runat="server" Text="OldPassword"></asp:Label></td>
		
         <td align="left" width="20%">
            <asp:TextBox ID="TxtOldPassword" runat="server"   TextMode="Password" 
                 TabIndex="2" />  
                 
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="TxtOldPassword">
              </asp:RequiredFieldValidator>  </td>
              
         </tr>
         <tr align="right">
       
   <td width="14%" style="width: 29%">
            <asp:Label ID="n_pword" runat="server" Text="NewPassword"></asp:Label></td>
              <td align="left" width="20%">
             <asp:TextBox ID="TxtNewPassword" runat="server" TextMode="Password" TabIndex="3" />   
                    
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                         ErrorMessage="*" ControlToValidate="TxtNewPassword">
                         </asp:RequiredFieldValidator>
                         </td>
              
       </tr>
		
		
		<tr align="right">
		<td width="14%" style="width: 29%">
            <asp:Label ID="c_pword" runat="server" Text="Confirm" ></asp:Label></td>
           <td  align="left" width="20%">
          <asp:TextBox ID="TxtConfirmPassword" runat="server"  TextMode="Password" 
                   TabIndex="4" /> 
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="TxtConfirmPassword">
               </asp:RequiredFieldValidator>
               
                             
          </td>
        
                    
               </tr>  
        
		</table>
		</fieldset>
		</div>
		
        
         <div>
         <table width="100%">
         <tr align="center">
         <td style="width: 87%">
         <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" 
                 BackColor="#6F696F" Font-Bold="False" ForeColor="White" Width="49px" /></td>
             </tr></table>
        </div>
        
        <br />
        <br />
        
         
          

</asp:Content>

