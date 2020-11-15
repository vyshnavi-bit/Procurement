<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RateChartRoutewise.aspx.cs" Inherits="RateChartRoutewise" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2">
                <br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;RouteWise Ratechart
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

<div class="legPlantratechart">
<fieldset class="fontt">
<legend>RouteWise Ratechart</legend>


<table border="0" width="100%" id="table3" cellspacing="1">
           <tr>
                    <td width="20%">
                    </td>
                   <td width="15%">

                    <asp:Label ID="lbp_Plantname" runat="server" Text="Plantname"></asp:Label>
                    </td>
                  <td width="25%">
                      
            <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
         Width="150px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Font-Size="Small"></asp:DropDownList>      
                    </td>
                    <td width="20%">
                    
                    
                    </td>
                </tr>

</table>
<table border="0" width="100%" id="table12" cellspacing="1">
           <tr>
                    <td width="20%">
                    </td>
                   <td width="15%">

                       &nbsp;</td>
                  <td width="25%">
                      
                      <asp:CheckBox ID="Chk_Agentwise" runat="server" 
                          oncheckedchanged="Chk_Agentwise_CheckedChanged" Text="Agentwise" 
                          AutoPostBack="True" />
                    </td>
                    <td width="20%">
                    
                    
                    </td>
                </tr>
                           <tr>
                    <td width="20%">
                    </td>
                   <td width="15%">
                     
                      <asp:Label ID="lbl_RouteName" runat="server" Text="RouteName"></asp:Label>
                     
                    </td>
                  <td width="25%">                      
            
             <asp:DropDownList ID="ddl_RouteName" runat="server" AutoPostBack="true" 
        Width="150px" onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" Font-Size="Small"></asp:DropDownList>
            
                    </td>
                    </tr>
                      <tr>

                    <td width="20%">
                    
                    
    <asp:DropDownList ID="ddl_AgentId" runat="server" AutoPostBack="True"  Visible="False" 
        Width="49px" Height="16px">
    </asp:DropDownList>
             
                    
                    </td>
                
              
                        </td>
                   <td width="15%">

                      <asp:Label ID="lbl_AgentName" runat="server" Text="AgentName"></asp:Label>
                     
                          </td>
                  <td width="25%">                      
                          
                    
                         <asp:DropDownList ID="ddl_AgentName" runat="server" AutoPostBack="True" 
                  onselectedindexchanged="ddl_AgentName_SelectedIndexChanged" Width="150px" 
                             Font-Bold="False" Font-Size="Small">
              </asp:DropDownList>
                           
                    
                    </td>
                    <td width="20%">
                    
                    
                         &nbsp;</td>
                </tr>
                
</table>
<table border="0" width="100%" id="table2" cellspacing="1">
                  <tr>
                  <td width="18%"></td>
                   <td width="10%" class="fontt">
										 <asp:Label ID="lbl_Ratechart" runat="server" Text="CowRatechart"></asp:Label></td>
										
                    <td width="15%" class="fontt" align="right">
                               <asp:DropDownList ID="ddl_Ratechart" runat="server" AutoPostBack="true" Width="150px"></asp:DropDownList>

                            </td>
                                <td width="10%">
                                 
                        
                              
                               </td>
                       <td width="9%" class="fontt">
       
                          <asp:Label ID="lbl_RatechartBuff" runat="server" Text="BuffaloRatechart"></asp:Label></td>
                             
                               <td width="15%" class="fontt" align="right">
                           
                              <asp:DropDownList ID="ddl_RatechartBuff" runat="server" AutoPostBack="true" Width="150px"></asp:DropDownList>
                              </td>
                               
                          <td width="20%"></td>    
                       </tr>
                   </table>
 

<table border="0" width="100%" id="table1" cellspacing="1">
                <tr>
                    <td width="20%">
                    </td>
                    <td width="30%">
                    </td>
                    <td width="20%" align="right">
                        <asp:Button ID="btn_RouteSave" Text="SAVE" Height="25px" BackColor="#6F696F" 
        ForeColor="White"  runat="server"  
        OnClientClick="return Confirm(Are You Sure Save the RoutewiseRatechart)" 
         onclick="btn_RouteSave_Click"/>
                                 <asp:Button ID="btn_AgentSave" Text="SAVE." Height="25px" BackColor="#6F696F" 
        ForeColor="White"  runat="server"  
        OnClientClick="return Confirm(Are You Sure Save the AgentwiseRatechart)" 
                            onclick="btn_AgentSave_Click" />
                    
                    </td>
                </tr>
</table>
</fieldset>
</div>
<br />
 <cr:crystalreportviewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None"/>

<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
  
</asp:Content>


