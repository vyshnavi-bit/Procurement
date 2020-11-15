<%@ Page Title="OnlineMilkTest|BillCheckReports" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BillCheckReports.aspx.cs" Inherits="BillCheckReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                    &nbsp;&nbsp;BillCheck Process
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

 


<div class="legendBilCheckprocess">
<fieldset class="fontt">
<legend class="fontt">BillCheckProcess Reports</legend>
<table width="100%">

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
          <asp:HyperLink ID="HyperLink16" NavigateUrl="~/Rpt_AgentmilkgLineGraph.aspx"
                 runat="server">AgentMilk LineGraph</asp:HyperLink>
             </td>     
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
          <asp:HyperLink ID="HyperLink26" NavigateUrl="~/MilkStatusAgentWise.aspx"
                 runat="server">Milk Status AgentWise</asp:HyperLink>
             </td>     
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
          <asp:HyperLink ID="HyperLink25" NavigateUrl="~/LoanCheckListAgent.aspx"
                 runat="server">LOAN CHECK LIST REPORT</asp:HyperLink>
             </td>     
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
          <asp:HyperLink ID="HyperLink15" NavigateUrl="~/Rpt_AllPlantwiseLoanDetails.aspx"
                 runat="server">Outside LoanReport</asp:HyperLink>
             </td>     
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
          <asp:HyperLink ID="HyperLink24" NavigateUrl="~/DonatedMilkReports.aspx"
                 runat="server">DONATED MILK REPORTS</asp:HyperLink>
             </td>         
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink6" NavigateUrl="~/Frm_PlantwiseMilkmonitoring.aspx"  runat="server">Milk Monitoring Report</asp:HyperLink>
             </td>         
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink12" NavigateUrl="~/AgentsProcurementList.aspx"  runat="server">Procurement CC reports</asp:HyperLink>
             </td>         
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink2" NavigateUrl="~/NillpaymentCheck.aspx"  runat="server">NillpaymentCheck</asp:HyperLink>
             </td>         
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink3" NavigateUrl="~/DeductionPendingList.aspx"  
                 runat="server">NegativeBillAmountCheck</asp:HyperLink>
             </td>         
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink4" NavigateUrl="~/TsFatSnfRangeCheck.aspx"  
                 runat="server">Fat&Snf RangeCheck</asp:HyperLink>
             </td>         
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink1" NavigateUrl="~/CartageSplbonus.aspx"  
                 runat="server">Cartage @ SplBonus Check</asp:HyperLink>
             </td>         
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink5" NavigateUrl="~/CurrentRateChartCheck.aspx"  
                 runat="server">CurrentRatechart</asp:HyperLink>
             </td>        
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >
             <asp:HyperLink ID="HyperLink7" NavigateUrl="~/IncreaseDecrease.aspx"  
                 runat="server">IncreaseDecrease</asp:HyperLink>
             </td>         
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink9" NavigateUrl="~/PlantStatusReport.aspx"  
                 runat="server">Plant Staus</asp:HyperLink>
             </td>         
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink10" NavigateUrl="~/SuperwiserIncentives.aspx"  
                 runat="server">Incentive Report</asp:HyperLink>
             </td>                      
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink11" NavigateUrl="~/RouteSupervisorAllotment.aspx"  
                 runat="server">AllotRouteSupervisor </asp:HyperLink>
             </td>                      
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink13" NavigateUrl="~/PlantPerDayMilk.aspx"  
                 runat="server">PlantPerDayMilk </asp:HyperLink>
             </td>                   
         
</tr>
<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink14" NavigateUrl="~/AMOUNTALLOTEMENTSaspx.aspx"  
                 runat="server">Admin Amount Allotment </asp:HyperLink>
             </td>                      
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink17" NavigateUrl="~/AgentsRemarks.aspx"  
                 runat="server">Agent   Remark Status</asp:HyperLink>
             </td>                      
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink18" NavigateUrl="~/PLANTOVERALLREPORT1.aspx"
                 runat="server">Plant Session Report</asp:HyperLink>
             </td>                      
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink19" NavigateUrl="~/PlantRatechartUsagewithmilkkg.aspx"
                 runat="server">Plant Ratechart Usage With Milkkg</asp:HyperLink>
             </td>                      
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink20" NavigateUrl="~/PlantMonthlySheet.aspx"
                 runat="server">Plant Monthly Report</asp:HyperLink>
             </td>                      
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink21" NavigateUrl="~/Agentcharttype.aspx"
                 runat="server">Agent chart type</asp:HyperLink>
             </td>                      
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink22" NavigateUrl="~/StopedLoanAgents.aspx"
                 runat="server">Stopped Loan Agents details</asp:HyperLink>
             </td>                      
         
</tr>

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink81" NavigateUrl="~/smstestingpage.aspx"    runat="server">Message Balance details</asp:HyperLink>
             </td>                      
         
</tr> 










<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink82" NavigateUrl="~/ProcurementDataBulkBackup.aspx"    
                 runat="server">Procurement Data Backup</asp:HyperLink>
             </td>                      
         
</tr> 










<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink83" NavigateUrl="~/RouteWisePerformence.aspx"    
                 runat="server">Supervisor Performance</asp:HyperLink>
             </td>                      
         
</tr> 




<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink84" NavigateUrl="~/DpuMilkCommision.aspx"    
                 runat="server">Agent and Farmer Report</asp:HyperLink>
             </td>                      
         
</tr> 

<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink85" NavigateUrl="~/Deductionamount.aspx"    
                 runat="server">Deduction Recovery Amount</asp:HyperLink>
             </td>                      
         
</tr> 








<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink86" NavigateUrl="~/AdminApproval.aspx"    
                 runat="server">AdminApproval</asp:HyperLink>
             </td>                      
         
</tr> 








<tr>
         <td width="40%">
             &nbsp;</td>
         <td width="60%" >             
          <asp:HyperLink ID="HyperLink87" NavigateUrl="~/AdminApprovalMonitor.aspx"    
                 runat="server">AdminApproval Monitor</asp:HyperLink>
             </td>                      
         
</tr> 








</table>
</fieldset>


</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

