<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SiloBalanceReport.aspx.cs" Inherits="SiloBalanceReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
        .style2
        {
            float: right;
            width: 31%;
        }
        .stylefiledset
        {
           width:40%; 
        } 
    </style>
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
                    &nbsp;&nbsp;SILO BALANCE
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
                
            </td>
        </tr>
        </table>
 </ContentTemplate>
 </asp:UpdatePanel> 

   <br />
   <center>
    <div class="stylefiledset">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">SiloBalance Report</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
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
                     <td align="right"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name"></asp:Label>      
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                                          	
 <asp:DropDownList ID="ddl_PlantName" 
        runat="server" Width="200px" 
         AutoPostBack="True" Font-Bold="True" Font-Size="Large" 
                            onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" >
       
    </asp:DropDownList>

                    </td>
                </tr> 
                  
            <tr>
                     <td  align="left">
         
                         &nbsp;</td>
                    <td >                         
                    </td>
                    <td  align="left">
                                          	
         <asp:DropDownList ID="ddl_PlantID" 
        runat="server" Width="16px" 
        
        AutoPostBack="True" Enabled="False" Visible="False" Height="16px">
    </asp:DropDownList>

                    </td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                    </td>
                </tr>   
                 <tr>
                     <td>                   
         
                         &nbsp;</td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	

        <asp:Button ID="Button2" runat="server" onclick="Button2_Click"  BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="70px" /> 
        
                    </td>
                </tr>  
                     
                
                 <tr align="center">
                    <td colspan="3">                                    

        				<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
							RepeatDirection="Horizontal">
							<asp:ListItem Value="1">Acknowledgement</asp:ListItem>
							<asp:ListItem Value="2">Without Rate</asp:ListItem>
							<asp:ListItem Value="3">Dispatch </asp:ListItem>
						</asp:RadioButtonList>
        
                     </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>  

   </center>






<br />


  <table width=100% ALIGN="center">
       <tr>
    <td  >
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None" 
            onunload="CrystalReportViewer1_Unload"/>

           </td>
            </tr>
       </table>
    
</asp:Content>

