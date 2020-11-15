<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OpeningStockReport.aspx.cs" Inherits="OpeningStockReport" %>

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
            width: 29%;
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
                    &nbsp;&nbsp;OPENING STOCK
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
 

 
   
<div style="width:100%;">
                  <table   width="100%">
                                    <tr>
                  <td width="18%"></td>
                   <td width="10%" class="fontt">
										</td>
										
                   
                                <td width="10%">
                                 
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="rbt_openingstock" Format="MM/dd/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                               </td>
                       <td width="9%" class="fontt">
       
                      </td>
                             
                              
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="rpt_closingstock" Format="MM/dd/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                          </td> 
                          <td width="20%"></td>    
                       </tr>

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
<legend class="fontt">Opening Stock</legend>
<table width="100%">
<tr>
  <td class="style1"></td>
         <td class="style2">
              <asp:Label ID="lbl_PlantID" runat="server" Text="Plant ID" Visible="False"></asp:Label>

         </td>
         <td width="25%">
         <asp:DropDownList ID="ddl_PlantID" 
        runat="server" Width="120px" 
        
        AutoPostBack="True" Enabled="False" Visible="False">
    </asp:DropDownList>
         </td>
</tr>
<tr> 
<td class="style1"></td>
         <td class="style2">
         <asp:Label ID="lbl_PlantName" runat="server" Text="PlantName"></asp:Label>

         </td>
 <td width="25%">
 <asp:DropDownList ID="ddl_PlantName" 
        runat="server" Width="120px" 
         AutoPostBack="True" >
       
    </asp:DropDownList>
 </td>

</tr>
<tr> 
<td class="style1"></td>
         <td class="style2">
         
         </td>
 <td width="25%">
 
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click"  BackColor="#6F696F"  ForeColor="White"
            Text="Plant Wise" Width="100px" /> 
        
 </td>

</tr>

</table>
</fieldset>


</div>






<br />


  <table ALIGN="center" width="100%">
       <tr>
    <td >
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False"  
            EnableParameterPrompt="False" ToolPanelView="None" 
            onunload="CrystalReportViewer1_Unload" />

           </td>
            </tr>
       </table>
    
</asp:Content>


