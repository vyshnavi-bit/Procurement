<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantPerDayMilk.aspx.cs" Inherits="PlantPerDayMilk" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 9pt;
        }
        .Grid th
        {
            color: #fff;
            background-color: #3AC0F2;
        }
        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td
        {
            border: 1px solid #525252;
        }
        .panels
        {
            width:100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager> 
    </br>   
 <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">MilkStatus Report</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">        
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>      
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">
                                <asp:TextBox ID="txt_FromDate" runat="server" Enabled="true"  ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>                          
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server" Enabled="true"  ></asp:TextBox>     
                              <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>                    	
     <asp:Button ID="btn_ok" runat="server"   
         BackColor="#00CCFF"  ForeColor="White"
            Text="OK" onclick="btn_ok_Click" BorderStyle="Double" Font-Bold="True" /> 

                    </td>
                </tr> 
                
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
                         &nbsp;</td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	

                        &nbsp;</td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="left">
         
                         &nbsp;</td>
                    <td >                         
                    </td>
                    <td  align="left">
            <asp:Button ID="btnPrintCurrent" runat="server" Text="Print" 
                            OnClick = "PrintCurrentPage" BackColor="#00CCFF" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="White" />

                        <asp:Button ID="btn_export" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="25px" 
                            onclick="btn_export_Click" Text="Export" />

                    </td>
                               <td width="12%">
                                 
                    </td>
                </tr>   
                
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>
 
<div class="legagent">
    <fieldset class="fontt">
 
 <legend style="color: #3399FF">Plant List</legend>
<asp:Panel ID="Panel3" runat="server">

<asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1" 
                CellSpacing="1"  CaptionAlign="Top" Height="40px" Width="200px" 
        style="margin-left: 0px">
                <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="250px">
                   
                  <asp:TableCell ID="TableCell22"  runat="Server"  BorderWidth="1">
         <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                CellPadding="1" 
                CellSpacing="1" Width="250px" CaptionAlign="Top" Height="40px" >
                
                
                
<asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC" ForeColor="white" BorderColor="Silver">
                       
<asp:TableCell ID="TableCell6" runat="Server" BorderWidth="0" > 
     <asp:CheckBox ID="MChk_PlantName" Text="PlantName" runat="server" checked="true"
        AutoPostBack="True" oncheckedchanged="MChk_PlantName_CheckedChanged"/>         
</asp:TableCell>
                
</asp:TableRow>
                
<asp:TableRow ID="TableRow2" runat="Server" BorderWidth="0" BorderColor="Silver" BackColor="#ffffec">
  
  <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="0">
  <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="0"> </asp:CheckBoxList> 
</asp:TableCell>       
                
</asp:TableRow>
            
</asp:Table>     
                  
</asp:TableCell>
                </asp:TableRow>
                
         </asp:Table>
         </asp:Panel>        
</fieldset>
</div>
   <br />
      <div align="center" >
      <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="True" 
            AllowPaging="True" OnPageIndexChanging = "OnPaging" CssClass="Grid"  PageSize="20"  Font-Size="12px"  >
</asp:GridView>
   <br />
      </div>    
</asp:Content>


