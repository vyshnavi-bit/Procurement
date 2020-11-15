<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DeductionReport.aspx.cs" Inherits="DeductionReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
   
      
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;DEDUCTION REPORT</p>
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
 </ContentTemplate>
 </asp:UpdatePanel>
 
 <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Deduction Report </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>      
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">
                               
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>

                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label> 
                         <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>     
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server" ></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name"></asp:Label>      
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left">                                           	
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="200px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                 Font-Bold="True" Font-Size="Large" ></asp:DropDownList>

                    </td>
                </tr> 
                  
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="left">
         
                         &nbsp;</td>
                    <td >                         
                    </td>
                    <td  align="left">
                        &nbsp;</td>
                               <td width="12%">
                                
                    </td>
                </tr>   
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
             <asp:DropDownList ID="ddl_plantcode" 
        runat="server" Width="26px"    
        AutoPostBack="True" Enabled="False" Visible="False" Height="16px">
    </asp:DropDownList>

                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	

        <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  
            BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="75px" TabIndex="2" /> 
        
                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>   


   <br />


<table width="100%">
<tr> 
<td width="30%"></td>
         
 <td width="25%">
 
 &nbsp;</td>

</tr>
</table>
  <table>
       <tr>
    <td width="15%"></td>
            <td width="16%" align="left">
        
            </td>
            </tr>
       </table>
       <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
           AutoDataBind="true"  EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None"/>
</asp:Content>
