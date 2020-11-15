<%@ Page Title="OnlineMilkTest|CurrentRateCheck" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CurrentRateChartCheck.aspx.cs" Inherits="CurrentRateChartCheck" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
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
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;NillpaymentCheck
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

 


<div class="legendDataprocess">
<fieldset class="fontt">
<legend class="fontt">Report Procurement</legend>
<table width="100%">
<tr>
         <td width="25%">
             &nbsp;</td>
         <td width="25%" align="right">
             &nbsp;</td>
         <td width="5%"></td>
         <td width="45%"></td>
</tr>
<tr>
         <td width="25%">
             <asp:DropDownList ID="ddl_plantcode" 
        runat="server" Width="50px"    
        AutoPostBack="True" Enabled="False" Visible="False">
    </asp:DropDownList>
         </td>
         <td width="25%" align="right">
         <asp:Label ID="lbl_RouteName" runat="server" Text="Plant Name" ></asp:Label>

         </td>
         <td width="5%">
         
             &nbsp;</td>
         <td width="45%">
         
 <asp:DropDownList ID="ddl_plantName" 
        runat="server" Width="200px" 
         AutoPostBack="True" 
                 Font-Bold="True" Font-Size="Large" onselectedindexchanged="ddl_plantName_SelectedIndexChanged" 
       >
    </asp:DropDownList>
         </td>
</tr>
<tr>
         <td width="25%" align="right">
             &nbsp;</td>
         <td width="25%"  align="right">
         <asp:Label ID="lbl_RatechartName" runat="server" Text="Ratechart Name" ></asp:Label>

                            </td>
         <td width="5%" align="right">
             &nbsp;</td>
         <td width="45%">
         
 <asp:DropDownList ID="ddl_ChartName" 
        runat="server" Width="200px" 
        onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" AutoPostBack="True" 
                 Font-Bold="True" Font-Size="Large" 
       >
    </asp:DropDownList>
         </td>
</tr>
<tr>
         <td width="25%"></td>
         <td width="25%" align="right">
             &nbsp;</td>
         <td width="5%"> 

             &nbsp;</td>
         <td width="45%"> 

     <asp:Button ID="btn_ok" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="60px" onclick="btn_ok_Click" /> 

             <asp:Button ID="btn_Export" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export" Width="70px" Height="26px" onclick="btn_Export_Click" />

         </td>
</tr>

</table>
</fieldset>


</div>


<br />


  
        <table ALIGN=center width="100%">
            <tr>
                <td>


  
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None" onunload="CrystalReportViewer1_Unload"/>
                </td>
            </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

