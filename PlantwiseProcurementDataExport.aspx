<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantwiseProcurementDataExport.aspx.cs" Inherits="PlantwiseProcurementDataExport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            color: #3399FF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<div>
    <br />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Select Export : " CssClass="style1"></asp:Label>
    <asp:DropDownList ID="drop_table" runat="server">
    <asp:ListItem Value="Agent_Master">Agent Master</asp:ListItem>
        <asp:ListItem Value="Route_Mater">Route Mater</asp:ListItem>        
         <asp:ListItem Value="Procurementimport">Procurementimport</asp:ListItem>
    </asp:DropDownList>
    <br />
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
                   </div>
     <asp:RadioButtonList ID="rblExtension" runat="server" CssClass="style1">
            <asp:ListItem Selected="True" Value="2003">Excel 97-2003</asp:ListItem>
            <asp:ListItem Value="2007">Excel 2007</asp:ListItem>
        </asp:RadioButtonList>
         <br />
        <span class="style1">Generate the download link when finished:</span>
        <asp:RadioButtonList 
            ID="rblDownload" runat="server" style="color: #3399FF">
            <asp:ListItem Selected="True" Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
        <br />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" />
     <br />
        <br />
        <asp:HyperLink ID="hlDownload" runat="server" style="color: #3399FF"></asp:HyperLink>       
</div>
</center>
 
    
</asp:Content>

