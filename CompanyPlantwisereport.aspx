<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompanyPlantwisereport.aspx.cs" Inherits="CompanyPlantwisereport" Title="Untitled Page" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    
<table border="0" cellpadding="0" cellspacing="1" width="100%" __designer:mapid="31">
        <tr __designer:mapid="32">
            <td width="100%" __designer:mapid="33"><br __designer:mapid="34" />
                <p class="subheading" style="line-height: 150%" __designer:mapid="35">
                    &nbsp;&nbsp;COMPANY PLANTWISE REPORT</p>
            </td>
        </tr>
        <tr __designer:mapid="36">
            <td width="100%" height="3px" __designer:mapid="37">
            </td>
        </tr>
        <tr __designer:mapid="38">
            <td width="100%" class="line" height="1px" __designer:mapid="39">
            </td>
        </tr>
        <tr __designer:mapid="3a">
            <td width="100%" height="7" __designer:mapid="3b">
                
            </td>
        </tr>
        </table>
    <center>
    <br />
<div>
<table   width="100%">
                  <tr>
                  <td width="18%"></td>
                   <td width="10%" class="fontt">
										From Date:</td>
										
                    <td width="15%" class="fontt" align="right">
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>

                            </td>
                                <td width="10%">
                                 
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="MM/dd/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                               </td>
                       <td width="9%" class="fontt">
       
                         To Date:</td>
                             
                               <td width="15%" class="fontt" align="right">
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox></td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="MM/dd/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                          </td> 
                          <td width="20%"></td>    
                       </tr>
                   </table>
  <br />                   
<asp:Button ID="btn_CompanywisePlantreport" runat="server"  
            Text="CompanywisePlantreport"  BackColor="#6F696F"  ForeColor="White"
        onclick="btn_CompanywisePlantreport_Click" />
        
            <br />
            <br />
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        oninit="CrystalReportViewer1_Init" />    

</div>
</center>
 
</asp:Content>
