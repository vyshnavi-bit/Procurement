<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PermissionRoleList.aspx.cs" Inherits="PermissionRoleList" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .Grid
        {
            border: 1px solid #525252;
        }
        .Grid th
        {
            border: 1px solid #525252;
        }
        .Grid th
        {
            color: #fff;
            background-color: #3AC0F2;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
      <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
            <ProgressTemplate >                             
                <div align="center" class="legendloadimg">
                 <img alt="progress" src="loading.gif" border="1" height="100px" width="100px"/>
                 Processing...                     
                 </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2">
                <br />
                <p class="subheading" style="line-height: 150%">
                    PermissionRoleList
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
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="fontt" NavigateUrl="~/RolePermissionSetting.aspx">Go to permissionsettings</asp:HyperLink>
            </td>
        </tr>
    </table>
    <br />
   
   <div class="legagentsms">
   <fieldset class="fontt">
   <legend class="fontt">PermissionRoleList</legend>
   <br />  
         <table border="0" width="100%" id="table4" cellspacing="1" align="center">
          
         <tr>
                  <td>
                      &nbsp;</td>
                   <td >
					   &nbsp;</td>										
                    <td  align="right">
					    Role Type :</td>
                               
                       <td >                             
                    	<asp:DropDownList ID="ddl_role" runat="server"  Width="110px" 
                               Font-Bold="True" >
                            <asp:ListItem Value="1">1_SuperAdmin</asp:ListItem>
                            <asp:ListItem Value="2">2_Admin</asp:ListItem>
                            <asp:ListItem Value="3">3_User</asp:ListItem>
                            <asp:ListItem Value="4">4_EndUser</asp:ListItem>
                           </asp:DropDownList>&nbsp;
                            <asp:Button ID="Button1" runat="server" BackColor="#00CCFF" ForeColor="White" 
                               Text="Search" Font-Bold="True" onclick="Button1_Click" />
                               <br />
                            </td>                             
                               <td >
                                </td>                   
                       </tr> 
                                   
                   </table>
                  <br />
                       <div align="center">
                           <br />
                       </div>
      
                   </fieldset>

                   </div>             
    <br />
    <div align="center">
           <table>
           <tr>
           <td align="center">
            <asp:GridView ID="gvCustomers" runat="server"             
        style=" text-align: left" Font-Size="Medium" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True" 
            Height="79px" PageSize="30">
            <RowStyle ForeColor="#000066" />
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
           </td>
           </tr>
           </table>
           </div>
        
               
    <br />
    <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
    </ContentTemplate>
        </asp:UpdatePanel>        
</asp:Content>


