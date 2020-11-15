<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SelectLoan.aspx.cs" Inherits="SelectLoan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <br />
    <div align="center">
&nbsp;<asp:Panel runat="server" Height="30px" ID="Panel1" Width="858px">
            <asp:Label ID="lbl_Plantname" runat="server" Text="Plantname : " 
                style="color: #3399FF"></asp:Label>
            <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="170px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddl_Plantcode_SelectedIndexChanged" Visible="false">
            </asp:DropDownList>
            <br />
            <br />
            <br />
            <asp:RadioButton runat="server"  ID="rd_Plantwiseratechart" AutoPostBack="true" 
        Text="PlantwiseLoanUpdate" Checked="True" 
        oncheckedchanged="rd_Plantwiseratechart_CheckedChanged" ForeColor="#3399FF"/>
            &nbsp;
            <asp:RadioButton runat="server" ID="rd_Routewiseratechart" AutoPostBack="true" 
        Text="RoutewiseLoanUpdate" 
        oncheckedchanged="rd_Routewiseratechart_CheckedChanged" ForeColor="#3399FF"  />
            <br />
            <br />
            <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" 
                CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" 
                Width="200px" Visible="False">
                <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="200px">
                    <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">

         <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                CellPadding="1" 
                CellSpacing="1" Width="200px" CaptionAlign="Top" Height="40px" >
                
                
                

             <asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC" 
                 ForeColor="white" BorderColor="Silver">
                       
<asp:TableCell ID="TableCell6" runat="Server" BorderWidth="2" > 
<asp:CheckBox ID="MChk_RouteName" Text="Route_Name" runat="server" 
        AutoPostBack="True" oncheckedchanged="MChk_RouteName_CheckedChanged" />                    
    
</asp:TableCell>
                
    
             </asp:TableRow>
             



             <asp:TableRow ID="TableRow2" runat="Server" BorderWidth="1" BorderColor="Silver" 
                 BackColor="Lavender">
  
  <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="1">
  <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="1"> 
                 </asp:CheckBoxList> 
    
</asp:TableCell>
</asp:TableRow>
       
</asp:Table>     
                  
                   
</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <br />
            <asp:Button ID="btn_SaveData" runat="server" Text="Save" 
        Width="70px" onclick="btn_SaveData_Click" />
        &nbsp;</asp:Panel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>

