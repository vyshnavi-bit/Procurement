<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProducerProcurementImport.aspx.cs" Inherits="ProducerProcurementImport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <div  >
<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>       
        <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup"  />
</div>  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>   

        <div align="center">
    <table width="50%"  >
    <tr valign="top">
    <td>
   
    <fieldset class="fontt">
    <legend >VMCC ProducerProcurementImport</legend>
    <table align="center">
    <tr> 
   <td align="left" class="style5"> 
   </td>
   <td></td>
   </tr>
    <tr> 
   <td align="left" class="style5"> 
       <asp:Label ID="Label11" runat="server" Font-Size="Small" 
           style="font-family: Andalus" Text="PlantName"></asp:Label>
        </td>
   <td>   
       <asp:DropDownList ID="ddl_Plantname" runat="server" 
           Font-Bold="True" Font-Size="Medium" 
            Width="154px">
       </asp:DropDownList>
   </td>   
   </tr>
   <tr>   
   <td align="left" class="style5">
       <asp:Label ID="Label10" runat="server" Text="Date" Font-Size="Small" 
           style="font-family: Andalus"></asp:Label>
   </td>
   <td align="left" class="style5">         
   <asp:TextBox ID="txt_FromDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                                    TargetControlID="txt_FromDate">
                                </asp:CalendarExtender>        
          </td>   
   </tr>
   <tr>   
   <td align="left">
       <asp:Label ID="Label5" runat="server" Text="Session" Font-Size="Small" style="font-family: Andalus"></asp:Label>
   </td>
   <td align="left">
       <asp:DropDownList ID="ddl_ses" runat="server"  
           Font-Bold="True" Font-Size="Medium" Width="60px">
           <asp:ListItem>AM</asp:ListItem>
           <asp:ListItem>PM</asp:ListItem>
       </asp:DropDownList>
   </td>   
   </tr>  
   <tr> 
   <td > </td>
   <td>   
         <div align="right"> 
             <asp:Button ID="btn_Import" runat="server" BackColor="#00CCFF" 
                 BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px"   
                 Text="Import" onclick="btn_Import_Click" /> </div>
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

        
 </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
