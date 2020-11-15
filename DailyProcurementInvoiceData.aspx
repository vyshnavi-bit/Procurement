<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DailyProcurementInvoiceData.aspx.cs" Inherits="DailyProcurementInvoiceData" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
        .style2
        {
            float: right;
            width: 32%;
        }
    </style>
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
                    &nbsp;&nbsp;DAILY PROCUREMENT INVOICE DATA</p>
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
 
<div style="width:100%;">
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
   </div>
   <br />


<div class="legendloan">
<fieldset class="fontt">
<legend class="fontt">Procurement InvoiceData</legend>
<table width="100%">
<tr>
  <td class="style1"></td>
         <td class="style2">
             <asp:Label ID="lbl_RouteID" runat="server" Text="Route ID" Visible="False"></asp:Label>

         </td>
         <td width="25%">
         <asp:DropDownList ID="ddl_RouteID" 
        runat="server" Width="120px" 
        onselectedindexchanged="ddl_RouteID_SelectedIndexChanged" 
        AutoPostBack="True" Enabled="False" Visible="False">
    </asp:DropDownList>
         </td>
</tr>
<tr> 
<td class="style1"></td>
         <td class="style2">
         <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name"></asp:Label>

         </td>
 <td width="25%">
 <asp:DropDownList ID="ddl_RouteName" 
        runat="server" Width="120px" 
        onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" AutoPostBack="True" 
       >
    </asp:DropDownList>
 </td>

</tr>
<tr> 
<td class="style1"></td>
         <td class="style2">
         
         </td>
 <td width="25%">
 <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  
         BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="100px" Height="26px" />
        &nbsp;<br />
 </td>

</tr>

</table>
</fieldset>


</div>






<br />


  <table>
       <tr>
    <td width="15%"></td>
            <td width="16%" align="left">
        
            </td>
            </tr>
       </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
          EnableParameterPrompt="False" ToolPanelView="None"/>
    

   
  
</asp:Content>



