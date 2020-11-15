<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ExeLink.aspx.cs" Inherits="Frm_ExeLink" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<br />
<br />
 <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Exe DownLoad </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="center">
                       &nbsp;</td>
                    <td align="center">                  
                                        <asp:RadioButtonList ID="rbtLstReportItems"   RepeatDirection="Horizontal" 
                                                  RepeatLayout="Table"  runat="server">
                                                   <asp:ListItem Text="Weigher" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Fatomatic" Value="2"></asp:ListItem>
                                                   <asp:ListItem Text="Analyzer" Value="3"></asp:ListItem>    
                                                                      
                                         </asp:RadioButtonList>
                    </td>
                    <td  align="left">
                                &nbsp;</td>
                </tr> 
                 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
                         &nbsp;</td>
                    <td  >                  
                                           <asp:Label ID="Lbl_selectedReportItem" runat="server" 
                            visible="false"></asp:Label>
                                          </td>
                    <td  align="left"> 
                                            	
                    	&nbsp;</td>
                </tr> 
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
                         &nbsp;</td>
                    <td  align="center">
                   <asp:Button ID="btn_DownLoad" runat="server" Text="DownLoad"  ForeColor="White"  Font-Bold="True"   
       BackColor="#006699" onclick="btn_DownLoad_Click" />
                    </td>
                    <td  align="left">                    	
                        &nbsp;</td>
                </tr> 
            
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
                         &nbsp;</td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                        &nbsp;</td>
                </tr>  
                     
                
            </table>
  <asp:Label ID="Lbl_Errormsg" runat="server" ForeColor="Red"  ></asp:Label>
            
          
   </fieldset>
   </div>
   <br />
              <br />
                <br />
                  <br />
                    <br />
  <br />
    <br />
      <br />
        <br />
          <br />
            <br />
              <br />
                <br />
                  <br />
  <br />
                      <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

