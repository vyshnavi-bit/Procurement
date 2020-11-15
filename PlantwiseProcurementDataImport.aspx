<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantwiseProcurementDataImport.aspx.cs" Inherits="PlantwiseProcurementDataImport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
<br />
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
    <center>

      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ToolkitScriptManager>
    
<div>


   

   <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>

   <asp:RadioButtonList ID="rblArchive" runat="server" style="color: #3399FF">
   <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
   
   </asp:RadioButtonList>

    
        <asp:Label ID="lblMessages1" runat="server" Text="Label" 
        style="color: #3399FF"></asp:Label>
           
       
        
        <br />
    <asp:Button ID="btn_ImportExcelData" runat="server" Text="Import ExcelData" 
        onclick="btn_ImportExcelData_Click" style="height: 26px" />
    <asp:Button ID="btn_Updatedonatemilktopaymentdata" runat="server" 
        Text="Paymentdataupdate" onclick="btn_Updatedonatemilktopaymentdata_Click" />
    <br />
    <br />
  
      
       
</div>
</center>
 
    
</asp:Content>

