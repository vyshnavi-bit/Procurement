<%@ Page Title="OnlineMilkTest|AvailableLloans" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AvailableLloans.aspx.cs" Inherits="AvailableLloans" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
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
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;AvailableLoan Report
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
 
<div style="width:100%;">
                  <table   width="100%">
                  <tr>
                  <td width="18%"></td>
                   <td width="10%" class="fontt">
										&nbsp;</td>
										
                    <td width="15%" class="fontt" align="right">
                                &nbsp;</td>
                                <td width="10%">
                                 
                     
                               </td>
                       <td width="9%" class="fontt">
       
                           &nbsp;</td>
                             
                               <td width="15%" class="fontt" align="right">
                                   &nbsp;</td>
                               <td width="12%">
                                
                         
                          </td> 
                          <td width="20%"></td>    
                       </tr>
                   </table>
   </div>
    <div class="legagentsms">
        <fieldset class="fontt">
            <legend style="color: #3399FF">AvailableLoan </legend>
            <table id="table4" align="center" border="0" cellspacing="1" width="100%">
                 <tr>
                    <td>
                    </td>
                    <td align="right">
   
             <asp:CheckBox ID="chk_print" runat="server" Text="Print"  />
   
                     </td>
                    <td>
   
             <asp:CheckBox ID="chk_Allloan" runat="server" AutoPostBack="True" 
                 Checked="True" Text="ALL" oncheckedchanged="chk_Allloan_CheckedChanged" />
   
             <asp:CheckBox ID="chk_CurrentLoan" runat="server" AutoPostBack="True" Text="Current Check" 
                            oncheckedchanged="chk_CurrentLoan_CheckedChanged" />
   
                     </td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                        &nbsp;<asp:Label ID="lbl_frmdate" runat="server" Text="From"></asp:Label>
                    </td>
                    <td>
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>

                    </td>
                    <td align="left">
                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                            TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="lbl_todate" runat="server" Text="To"></asp:Label>
                    </td>
                    <td align="left">
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                            TargetControlID="txt_ToDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
         <asp:Label ID="lbl_PlantName" runat="server" Text="Plant Name" ></asp:Label>

                    </td>
                    <td>
         
 <asp:DropDownList ID="ddl_plantName" 
        runat="server" Width="190px" 
      
                 Font-Bold="False" Font-Size="Large" onselectedindexchanged="ddl_plantName_SelectedIndexChanged" 
       >
    </asp:DropDownList>
                    </td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
         
             <%--<asp:DropDownList ID="ddl_plantcode" 
        runat="server" Width="16px"    
        AutoPostBack="True" Enabled="False" Visible="False" Height="16px">
    </asp:DropDownList>--%>
                    </td>
                    <td align="right">
 <asp:Button ID="btn_Loanrecoveryreport" runat="server"   
         BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="70px" Height="26px" onclick="btn_Loanrecoveryreport_Click" 
          />

             <asp:Button ID="btn_Export" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export" Width="70px" Height="26px" onclick="btn_Export_Click" />

                    </td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        &nbsp;</td>
                    <td>
                    </td>
                    <td align="left">
                        &nbsp;</td>
                    <td width="12%">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                    </td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
            </table>
            <br />
        </fieldset>
    </div>
    <table  class="style4">
        <tr align="center">
            <td>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None" 
                    onunload="CrystalReportViewer1_Unload"/>
            </td>
        </tr>
    </table>
   <br />

<br />


  </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

