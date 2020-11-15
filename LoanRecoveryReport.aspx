<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoanRecoveryReport.aspx.cs" Inherits="LoanRecoveryReport" Title="OnlineMilkTest|LoanRecoveryReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;LOAN RECOVERY REPORT
                </p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7">
                
            </td>
        </tr>
        </table>
 </ContentTemplate>
 </asp:UpdatePanel> 

   <br />
   
 <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Loan Recovery Report</legend>
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
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>

                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label> 
                         
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>     
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
                         &nbsp;</td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                                       	
   
             <asp:CheckBox ID="chk_Allloan" runat="server" AutoPostBack="True" 
                 Checked="True" oncheckedchanged="chk_Allloan_CheckedChanged" Text="ALL" />
   
                    &nbsp;<asp:CheckBox ID="chk_CurrentLoan" runat="server" Text="Current Check" />
   
                    </td>
                </tr> 
                  
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">
         
             <asp:Label ID="Label1" runat="server" Text="Plant Name"></asp:Label>      
                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
        <asp:DropDownList ID="ddl_Plantname" AutoPostBack="true" runat="server"  
             onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
            Width="190px" Font-Bold="True" Font-Size="Large">
                                    </asp:DropDownList>    
                    </td>
                               <td width="12%">                             
                    </td>
                </tr>   
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
         <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name"></asp:Label>

                     </td>
                    <td  align="right">
                  
                        &nbsp;</td>
                    <td  align="left">                    	

 <asp:DropDownList ID="ddl_RouteName" 
        runat="server" Width="190px" 
        onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" 
         AutoPostBack="True" Font-Size="Large" Font-Bold="True" 
       >
    </asp:DropDownList>
                     </td>
                </tr>  
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
          <asp:Label ID="lbl_AgentName" runat="server" Text="Agent Name" ></asp:Label>

                     </td>
                    <td  align="right">
                  
                        &nbsp;</td>
                    <td  align="left">                    	

         <asp:DropDownList ID="ddl_AgentName" runat="server" Width="190px" 
                 Font-Size="Large" 
         onselectedindexchanged="ddl_AgentName_SelectedIndexChanged" AutoPostBack="true" 
                            Font-Bold="True" >
    </asp:DropDownList>    
                     </td>
                </tr>      
                <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
                         &nbsp;</td>
                    <td  align="right">
                  
                        &nbsp;</td>
                    <td  align="left">                    	

         <asp:DropDownList ID="ddl_AgentID" runat="server" Width="24px"  Enabled="False" 
                 Visible="False" Font-Size="Large" Height="16px">
    </asp:DropDownList>    
         <asp:DropDownList ID="ddl_RouteID" 
        runat="server" Width="16px" 
        onselectedindexchanged="ddl_RouteID_SelectedIndexChanged" 
        AutoPostBack="True" Enabled="False" Visible="False" Font-Size="Large" Height="16px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddl_Plantcode" 
            AutoPostBack="true" runat="server" 
             Width="30px" Visible="false">
                                    </asp:DropDownList>         
 <asp:Button ID="btn_Loanrecoveryreport" runat="server"   
         BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="70px" Height="26px" onclick="btn_Loanrecoveryreport_Click" 
          />
                    </td>
                </tr>  
            </table>
            <br />
          
   </fieldset>
   </div>   


<br />


  <table ALIGN="center" width="100%">
       <tr>
    <td width="100%"> 
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None" 
            onunload="CrystalReportViewer1_Unload"/>
    

   
            </tr>
       </table>
            

   
</asp:Content>


