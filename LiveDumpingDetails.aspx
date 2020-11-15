<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LiveDumpingDetails.aspx.cs" Inherits="LiveDumpingDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <style type="text/css">
        label
        {
            display: block;
            padding: 10px;
            margin: 10px 0px;
        }
        
        .Items
        {
            background: black;
            color: White;
            border: 1px #ff006e solid;
        }
         .bcolor
        {
            background: #e0e0e0;
           
        }
         .dheight
        {
            height:590px;
        }
        .linkNoUnderline
        {
         text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<body>

    
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
       
       
                <div align="center">
                 <div style="width: 13%; float: left;">
                                    <img src="Image/VLogo.png" alt="Vyshnavi" width="100px" height="52px" />
                                </div>
 <div style="width: 77%; float: left;">
               <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Size="22px" ForeColor=" #0252aa" Text="SRI VYSHNAVI DAIRY SPECIALITIES PVT LIMITED"></asp:Label>
               <br />
                <span style="font-size: 20px; font-weight: bold; color: #0252aa;">Live 
               Procurement Data</span>
                <asp:TextBox ID="txt_fromdate" runat="server" Visible="false" 
                   Height="16px" Width="16px"   
                    ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fromdate"
                    PopupButtonID="txt_fromdate" Format="dd/MM/yyyy" 
        PopupPosition="TopRight">
                </cc1:CalendarExtender>  
                   <hr /> 
               <div align="center>
               <cc1:LineChart ID="LineChart1" runat="server" BorderStyle="None" 
        ChartType="Basic" ChartTitleColor="#0E426C" Visible = "false"
        CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">    
    </cc1:LineChart>

               </div>
              

                    <table>
    <tr>
    <td valign="top">
      <asp:GridView ID="grdweight" runat="server" ForeColor="White"  CssClass="gridcls"
        GridLines="Both" Font-Bold="true"  AutoGenerateColumns="false">
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
        <HeaderStyle BackColor="#FF6600" Font-Bold="true" ForeColor="White" Font-Italic="False"
            Font-Size="Large" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="false" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="Wtime" HeaderText="WTime" ItemStyle-Width="20" />
            <asp:BoundField DataField="WSampleNo" HeaderText="WsampleNo" ItemStyle-Width="40" />
           
        </Columns>
    </asp:GridView>
    </td>
    <td valign="top">
       <asp:GridView ID="grdAnalyzer" runat="server" ForeColor="White"  CssClass="gridcls"
        GridLines="Both" Font-Bold="true"  AutoGenerateColumns="false">
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
        <HeaderStyle BackColor="#6A8347" Font-Bold="false" ForeColor="White" Font-Italic="False"
            Font-Size="Large" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="Atime" HeaderText="ATime" ItemStyle-Width="20" />
            <asp:BoundField DataField="ASampleNo" HeaderText="AsampleNo" ItemStyle-Width="40" />
           
        </Columns>
    </asp:GridView>
    </td>
    </tr>
    </table>      
               
   
    
    
</body>
</asp:Content>


