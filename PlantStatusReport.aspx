<%@ Page Title="OnlineMilkTest|PlantStatus" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantStatusReport.aspx.cs" Inherits="PlantStatusReport" %>
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>    

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
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name"></asp:Label>      
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                                            	
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" Width="200px" 
                 Font-Bold="True" Font-Size="Large" ></asp:DropDownList>

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

        <asp:CheckBox ID="chk_plant" runat="server" 
            Text="Plant" />   

        <asp:CheckBox ID="chk_Route" runat="server" 
            Text="Route" />   

        <asp:CheckBox ID="chk_Agent" runat="server" 
            Text="Agent" />   

                    </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="left">
         
                         &nbsp;</td>
                    <td >                         
                    </td>
                    <td  align="left">
     <asp:Button ID="btn_ok" runat="server"   
         BackColor="#00CCFF"  ForeColor="White"
            Text="Generate" onclick="btn_ok_Click" BorderStyle="Double" Font-Bold="True" /> 

             <asp:Button ID="btn_Export" runat="server" 
         BackColor="#00CCFF"  ForeColor="White"
            Text="Export_XL" Height="26px" onclick="btn_Export_Click" BorderStyle="Double" 
                            Font-Bold="True"  />
                    </td>
                               <td width="12%">
                                 
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
            <asp:Button ID="btnPrintAll" runat="server" Text="Print All Pages" 
                            OnClick = "PrintAllPages" BackColor="#00CCFF" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="White" />
            <asp:Button ID="btnPrintCurrent" runat="server" Text="Print Current Page" 
                            OnClick = "PrintCurrentPage" BackColor="#00CCFF" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="White" />
                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>
   <br />
      <div align="center">
      <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="True" 
            AllowPaging="True" OnPageIndexChanging = "OnPaging" CssClass="Grid" 
              OnRowCreated="gvCustomers_RowCreated" PageSize="20"   >
</asp:GridView>
   <br />
      </div>
   
<br />


</asp:Content>


